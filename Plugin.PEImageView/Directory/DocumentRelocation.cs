using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AlphaOmega.Debug.NTDirectory;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentRelocation : DocumentBase
	{
		public DocumentRelocation()
			: base(PeHeaderType.DIRECTORY_RELOCATION)
			=> this.InitializeComponent();

		protected override void ShowFile(AlphaOmega.Debug.PEFile info)
		{
			lvAddress.Plugin = base.Plugin;

			List<ListViewItem> items = new List<ListViewItem>();
			foreach(var block in info.Relocations)
			{
				ListViewItem item = new ListViewItem() { Tag = block, };
				String[] subItems = Array.ConvertAll<String, String>(new String[lvDirectory.Columns.Count], a => String.Empty);
				item.SubItems.AddRange(subItems);

				item.SubItems[colSize.Index].Text = base.Plugin.FormatValue(block.Block.SizeOfBlock);
				item.SubItems[colType.Index].Text = base.Plugin.FormatValue(block.Block.TypeOffest);
				item.SubItems[colVA.Index].Text = base.Plugin.FormatValue(block.Block.VirtualAddress);

				items.Add(item);
			}
			lvDirectory.Items.Clear();
			lvDirectory.Items.AddRange(items.ToArray());
			lvDirectory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			this.lvDirectory_SelectedIndexChanged(null, EventArgs.Empty);
		}

		private void lvDirectory_SelectedIndexChanged(Object sender, EventArgs e)
		{
			if(lvDirectory.SelectedItems.Count == 1)
				lvAddress.DataBind((RelocationBlock)lvDirectory.SelectedItems[0].Tag);
			else
			{
				lvAddress.SuspendLayout();
				lvAddress.Items.Clear();
				lvAddress.ResumeLayout(false);
			}
		}
	}
}