using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExportPe = AlphaOmega.Debug.NTDirectory.Export;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentExport : DocumentBase
	{
		private DocumentExportSettings _settings;

		public override DocumentBaseSettings Settings => this.SettingsI;

		private DocumentExportSettings SettingsI
			=> this._settings ?? (this._settings = new DocumentExportSettings());

		public DocumentExport()
			: base(PeHeaderType.DIRECTORY_EXPORT)
			=> InitializeComponent();

		protected override void ShowFile(AlphaOmega.Debug.PEFile info)
		{
			lvHeader.Plugin = this.Plugin;

			lvHeader.DataBind(info.Export.Header.Value);

			this.ShowExportFunctions(info.Export, this.SettingsI.Ordinal);
		}

		internal void ShowExportFunctions(ExportPe export, UInt16? ordinalIndex)
		{
			lvFunctions.SuspendLayout();
			lvFunctions.Items.Clear();
			lvFunctions.Groups.Clear();
			if(!export.IsEmpty)
			{
				List<ListViewItem> items = new List<ListViewItem>();
				ListViewGroup group = lvFunctions.Groups.Add(export.DllName, String.Empty);
				foreach(var function in export.GetExportFunctions())
				{
					ListViewItem item = new ListViewItem(group);
					String[] subItems = Array.ConvertAll<String, String>(new String[lvFunctions.Columns.Count], delegate(String a) { return String.Empty; });
					item.SubItems.AddRange(subItems);
					item.SubItems[colAddress.Index].Text = $"0x{function.Address:X8}";
					item.SubItems[colName.Index].Text = function.Name;
					item.SubItems[colOrdinal.Index].Text = function.Ordinal.ToString();
					items.Add(item);
				}
				group.Header = $"{export.DllName} ({items.Count})";
				lvFunctions.Items.AddRange(items.ToArray());
				lvFunctions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

				if(ordinalIndex.HasValue)
				{
					String ordinalName=ordinalIndex.ToString();
					foreach(ListViewItem item in lvFunctions.Items)
						if(item.SubItems[colOrdinal.Index].Text == ordinalName)
						{
							item.Selected = true;
							lvFunctions.FocusedItem = item;
							item.EnsureVisible();
							break;
						}
				}
			}
			lvFunctions.ResumeLayout(false);
		}

		private void ListView_DragEnter(Object sender, DragEventArgs e)
			=> e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;

		private void ListView_DragDrop(Object sender, DragEventArgs e)
		{
			String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
			if(files.Length > 0)
				base.OpenFile(files[0]);
		}
	}
}