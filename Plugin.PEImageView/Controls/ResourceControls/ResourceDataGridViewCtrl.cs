using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceDataGridViewCtrl : IResourceSelectorCtrl
	{
		private readonly DataGridView _gridView;
		private DataSet _data = null;

		public String[] SelectableData
		{
			get
			{
				if(this._data != null)
				{
					String[] result = new String[this._data.Tables.Count];
					for(Int32 loop = 0;loop < result.Length;loop++)
						result[loop] = this._data.Tables[loop].ToString();
					return result;
				} else return null;
			}
		}
		public VisualizerType Type => VisualizerType.GridView;

		public Control Control => this._gridView;

		public ResourceDataGridViewCtrl()
		{
			this._gridView = new DataGridView()
			{
				ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize,
			};
		}

		public void BindControl(Object data)
		{
			this._data = (DataSet)data;

			if(this._gridView.DataSource != null && this._gridView.DataSource is IDisposable)
				((IDisposable)this._gridView.DataSource).Dispose();

			this.SelectData(0);
		}

		public void SelectData(Int32 index)
		{
			this._gridView.DataSource = this._data.Tables[index];
			this._gridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
		}

		public void Dispose()
		{
			this._gridView.Dispose();
			if(this._data != null)
			{
				this._data.Dispose();
				this._data = null;
			}
		}
	}
}