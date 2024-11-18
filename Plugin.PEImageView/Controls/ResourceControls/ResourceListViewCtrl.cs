using System;
using System.Windows.Forms;
using AlphaOmega.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceListViewCtrl : IResourceCtrl
	{
		private readonly ListView _list;

		public VisualizerType Type => VisualizerType.ListView;

		public System.Windows.Forms.Control Control => this._list;

		public ResourceListViewCtrl()
		{
			this._list = new ListView
			{
				FullRowSelect = true,
				GridLines = true,
				HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable,
				HideSelection = false,
				UseCompatibleStateImageBehavior = false,
				View = System.Windows.Forms.View.Details,
				ContextMenuStrip = new ContextMenuStripCopy()
			};
			this._list.Columns.Add("Value");
		}
		public void BindControl(Object data)
		{
			this._list.Items.Clear();
			this._list.Items.AddRange((ListViewItem[])data);

			this._list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		public void Dispose()
			=> this._list.Dispose();
	}
}