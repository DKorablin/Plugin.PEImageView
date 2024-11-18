using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using AlphaOmega.Debug;
using AlphaOmega.Debug.NTDirectory;
using AlphaOmega.Windows.Forms;
using Plugin.PEImageView.Bll;
using Plugin.PEImageView.Properties;
using SAL.Windows;
using ImportPe = AlphaOmega.Debug.NTDirectory.Import;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentImport : DocumentBase
	{
		private SystemImageList SmallImageList { get; set; }

		private ImportModule? SelectedModule => lvDll.SelectedItems.Count == 0 ? (ImportModule?)null : (ImportModule)lvDll.SelectedItems[0].Tag;

		private WinNT.IMAGE_IMPORT_BY_NAME? SelectedImport => lvImports.SelectedItems.Count == 0 ? (WinNT.IMAGE_IMPORT_BY_NAME?)null : (WinNT.IMAGE_IMPORT_BY_NAME)lvImports.SelectedItems[0].Tag;

		public DocumentImport()
			: base(PeHeaderType.DIRECTORY_IMPORT)
		{
			InitializeComponent();
			this.SmallImageList = new SystemImageList(SystemImageListSize.SmallIcons);
			SystemImageListHelper.SetImageList(lvDll, this.SmallImageList, false);
		}

		protected override void ShowFile(PEFile info)
		{
			lvDescriptor.Plugin = base.Plugin;

			this.ShowImportFunctions(info.Import);
			this.ShowImportDescriptor(info.Import);
		}

		private void ShowImportFunctions(ImportPe import)
		{
			lvDll.Items.Clear();
			lvDll.Groups.Clear();

			lvImports.Items.Clear();
			lvImports.Groups.Clear();
			if(!import.IsEmpty)
			{
				List<ListViewItem> items = new List<ListViewItem>();
				foreach(var module in import)
				{
					ListViewItem item = new ListViewItem($"{module.ModuleName} ({module.Count()})")
					{
						Tag = module,
						ImageIndex = this.SmallImageList.IconIndex(module.ModuleName),
					};
					items.Add(item);
				}
				lvDll.Items.AddRange(items.ToArray());
				if(lvDll.Items.Count == 1)
					lvDll.Items[0].Selected = true;
				lvDll.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			}
		}

		private void ShowImportDescriptor(ImportPe import)
		{
			splitToc.Panel2Collapsed = !import.Header.HasValue;

			if(!splitToc.Panel2Collapsed)
				lvDescriptor.DataBind(import.Header.Value);
		}

		private void lvDll_DragEnter(Object sender, DragEventArgs e)
			=> e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;

		private void lvDll_DragDrop(Object sender, DragEventArgs e)
		{
			String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
			if(files.Length > 0)
				base.OpenFile(files[0]);
		}

		private void lvDll_SelectedIndexChanged(Object sender, EventArgs e)
		{
			lvImports.Items.Clear();
			if(lvDll.SelectedItems.Count == 0)
				return;

			//String moduleName = lvDll.SelectedItems[0].Text;
			ImportModule? lookupModule = this.SelectedModule;
			if(lookupModule == null)
				throw new ApplicationException();

			PEFile info = base.GetFile();

			foreach(var module in info.Import)
				//if(moduleName.Equals(module.ModuleName))
				if(module.Header.Name == lookupModule.Value.Header.Name)
				{
					lvImports.SuspendLayout();
					List<ListViewItem> items = new List<ListViewItem>();

					foreach(var row in module)
					{
						ListViewItem item = new ListViewItem() { Tag = row, };
						String[] subItems = Array.ConvertAll<String, String>(new String[lvImports.Columns.Count], delegate(String a) { return String.Empty; });
						item.SubItems.AddRange(subItems);
						item.SubItems[colHint.Index].Text = row.Hint.ToString();
						item.SubItems[colName.Index].Text = row.Name ?? Resources.NullString;
						item.SubItems[colByOrdinal.Index].Text = row.IsByOrdinal.ToString();
						if(String.IsNullOrEmpty(row.Name))
							item.SetNull();
						items.Add(item);
					}
					lvImports.Items.AddRange(items.ToArray());
					lvImports.AutoResizeColumn(colHint.Index, ColumnHeaderAutoResizeStyle.HeaderSize);
					lvImports.AutoResizeColumn(colName.Index, ColumnHeaderAutoResizeStyle.ColumnContent);
					lvImports.AutoResizeColumn(colByOrdinal.Index, ColumnHeaderAutoResizeStyle.HeaderSize);
					lvImports.ResumeLayout();
					break;
				}
		}

		private void lvDll_DoubleClick(Object sender, EventArgs e)
		{
			if(lvDll.SelectedItems.Count > 0)
			{
				Boolean isOpened = false;
				foreach(ListViewItem item in lvDll.SelectedItems)
				{
					ImportModule module = (ImportModule)item.Tag;
					String path = base.GetFilePath(module.ModuleName);
					if(path != null && this.Plugin.Binaries.OpenFile(path))
						isOpened = true;
				}
				if(isOpened)
					if(this.Plugin.CreateWindow(typeof(PanelTOC).ToString(), true, null) == null)
						this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "{0} not found!", typeof(PanelTOC).ToString());
			}
		}
		private void lvImports_DoubleClick(Object sender, EventArgs e)
		{
			WinNT.IMAGE_IMPORT_BY_NAME? import = this.SelectedImport;
			if(import.HasValue && import.Value.IsByOrdinal)
			{
				ImportModule? module = this.SelectedModule;
				if(!module.HasValue)
					throw new ApplicationException();

				String path = base.GetFilePath(module.Value.ModuleName);
				if(path != null)
				{
					if(this.Plugin.CreateWindow<DocumentExport, DocumentBaseSettings>(
						new DocumentExportSettings() { FilePath = path, Ordinal = import.Value.Hint }) == null)
						this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "{0} not found!", typeof(DocumentExport).ToString());
				}
			}
		}
	}
}