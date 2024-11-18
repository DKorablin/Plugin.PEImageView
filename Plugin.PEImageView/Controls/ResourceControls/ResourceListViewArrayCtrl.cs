using System;
using System.Collections;
using System.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceListViewArrayCtrl : IResourceCtrl
	{
		private readonly ReflectionArrayListView _list;

		public VisualizerType Type => VisualizerType.ListViewArray;

		public System.Windows.Forms.Control Control => this._list;

		public ResourceListViewArrayCtrl(PluginWindows plugin)
		{
			this._list = new ReflectionArrayListView()
				{
					AllowColumnReorder = true,
					FullRowSelect = true,
					GridLines = true,
					HideSelection = false,
					Plugin = plugin,
					Sorting = System.Windows.Forms.SortOrder.Ascending,
					UseCompatibleStateImageBehavior = false,
					View = System.Windows.Forms.View.Details,
				};
		}
		public void BindControl(Object data)
		{
			this._list.DataBind((IEnumerable)data);
			this._list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		public void Dispose()
			=> this._list.Dispose();
	}
}