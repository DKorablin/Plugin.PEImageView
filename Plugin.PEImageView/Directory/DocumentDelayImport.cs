using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using AlphaOmega.Debug;
using AlphaOmega.Debug.NTDirectory;
using AlphaOmega.Windows.Forms;
using ImportPe = AlphaOmega.Debug.NTDirectory.DelayImport;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentDelayImport : DocumentBase
	{
		private SystemImageList SmallImageList { get; set; }

		private DelayImportModule SelectedModule => lvDll.SelectedItems.Count == 0 ? null : (DelayImportModule)lvDll.SelectedItems[0].Tag;

		private WinNT.IMAGE_IMPORT_BY_NAME? SelectedImport => lvImports.SelectedItems.Count == 0 ? (WinNT.IMAGE_IMPORT_BY_NAME?)null : (WinNT.IMAGE_IMPORT_BY_NAME)lvImports.SelectedItems[0].Tag;

		public DocumentDelayImport()
			: base(PeHeaderType.DIRECTORY_DELAY_IMPORT)
		{
			this.InitializeComponent();
			this.SmallImageList = new SystemImageList(SystemImageListSize.SmallIcons);
			SystemImageListHelper.SetImageList(lvDll, this.SmallImageList, false);
		}

		protected override void ShowFile(AlphaOmega.Debug.PEFile info)
		{
			lvDescriptor.Plugin = base.Plugin;

			if(!info.DelayImport.IsEmpty)
				this.ShowImportFunctions(info.DelayImport);
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
				foreach(DelayImportModule module in import)
				{
					ListViewItem item = new ListViewItem($"{module.ModuleName} ({module.GetIntAddress().Count()})")
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

		private void lvDll_DoubleClick(Object sender, EventArgs e)
		{
			WinNT.IMAGE_IMPORT_BY_NAME? import = this.SelectedImport;
			if(import.HasValue && import.Value.IsByOrdinal)
			{
				DelayImportModule module = this.SelectedModule
					?? throw new InvalidOperationException();

				String path = base.GetFilePath(module.ModuleName);
				if(path != null)
				{
					if(this.Plugin.CreateWindow<DocumentExport, DocumentBaseSettings>(
						new DocumentExportSettings() { FilePath = path, Ordinal = import.Value.Hint }) == null)
						this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "{0} not found!", typeof(DocumentExport).ToString());
				}
			}
		}

		private void lvDll_SelectedIndexChanged(Object sender, EventArgs e)
		{
			lvDescriptor.Items.Clear();
			lvImports.Items.Clear();
			if(lvDll.SelectedItems.Count == 0)
				return;

			DelayImportModule module = this.SelectedModule;

			lvImports.SuspendLayout();
			lvImports.Items.AddRange(
				Array.ConvertAll(module.ToArray(),
				delegate (WinNT.IMAGE_IMPORT_BY_NAME import)
				{
					ListViewItem item = new ListViewItem() { Tag = import, };
					String[] subItems = Array.ConvertAll<String, String>(new String[lvImports.Columns.Count], a => String.Empty);
					item.SubItems.AddRange(subItems);
					item.SubItems[colHint.Index].Text = import.Hint.ToString();
					item.SubItems[colName.Index].Text = import.Name;
					item.SubItems[colByOrdinal.Index].Text = import.IsByOrdinal.ToString();
					return item;
				}));
			lvImports.AutoResizeColumn(colHint.Index, ColumnHeaderAutoResizeStyle.HeaderSize);
			lvImports.AutoResizeColumn(colName.Index, ColumnHeaderAutoResizeStyle.ColumnContent);
			lvImports.AutoResizeColumn(colByOrdinal.Index, ColumnHeaderAutoResizeStyle.HeaderSize);
			lvImports.ResumeLayout();

			lvDescriptor.DataBind(module.Descriptor);
		}

		private void lvImports_DoubleClick(Object sender, EventArgs e)
		{
			if(lvImports.SelectedItems.Count > 0)
			{
				Boolean isOpened = false;
				DelayImportModule module = this.SelectedModule;
				foreach(ListViewItem item in lvDll.SelectedItems)
				{
					WinNT.IMAGE_IMPORT_BY_NAME proc = (WinNT.IMAGE_IMPORT_BY_NAME)item.Tag;
					String path = base.GetFilePath(module.ModuleName);
					if(path != null)
						if(proc.IsByOrdinal)
						{
							if(this.Plugin.CreateWindow<DocumentExport, DocumentBaseSettings>(
								new DocumentExportSettings() { FilePath = path, Ordinal = proc.Hint }) == null)
								this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "{0} not found!", typeof(DocumentExport).ToString());
							break;
						} else if(this.Plugin.Binaries.OpenFile(path))
							isOpened = true;
				}
				if(isOpened
					&& this.Plugin.CreateWindow(typeof(PanelTOC).ToString(), true, null) == null)
					this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "{0} not found!", typeof(PanelTOC).ToString());
			}
		}
	}
}