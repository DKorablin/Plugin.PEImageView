using System;
using System.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceCtrlHost : Panel
	{
		public PluginWindows Plugin { get; set; }
		public IResourceCtrl Control { get; private set; }

		public void ShowControl(VisualizerType type, Object data)
		{
			if(type == VisualizerType.None)
				this.HideControl();
			else
			{
				if(this.Control == null || this.Control.Type != type)
				{
					this.RemoveControl();
					this.Control = this.CreateControl(type);
					this.Control.Control.Dock = DockStyle.Fill;
				}

				this.Control.Control.Visible = true;

				base.Controls.Add(this.Control.Control);
				this.Control.BindControl(data);
			}
		}

		private IResourceCtrl CreateControl(VisualizerType type)
		{
			switch(type)
			{
			case VisualizerType.GridView:
				return new ResourceDataGridViewCtrl();
			case VisualizerType.ListView:
				return new ResourceListViewCtrl();
			case VisualizerType.ListViewArray:
				return new ResourceListViewArrayCtrl(this.Plugin);
			case VisualizerType.WebBrowser:
				return new ResourceWebBrowserCtrl();
			case VisualizerType.Dialog:
				return new ResourceDialogCtrl(this.Plugin);
			case VisualizerType.Menu:
				return new ResourceMenuCtrl();
			case VisualizerType.ToolBar:
				return new ResourceToolBarCtrl();
			case VisualizerType.Bitmap:
				return new ResourceBitmapCtrl();
			case VisualizerType.Version:
				return new ResourceListViewVersionCtrl(this.Plugin);
			case VisualizerType.BinView:
				return new ResourceBinaryViewCtrl();
			case VisualizerType.TypeLib:
				return new ResourceTypeLib();
			default: throw new NotImplementedException();
			}
		}

		private void SetControl(IResourceCtrl ctrl)
		{
			this.RemoveControl();
			this.Control = ctrl;
		}

		/// <summary>Скрываю элемент управления при выборе другого элемента</summary>
		public void HideControl()
		{
			if(this.Control != null)
				this.Control.Control.Visible = false;
		}

		/// <summary>Удаление ранее приделанного элемента управления</summary>
		public void RemoveControl()
		{
			if(this.Control != null)
			{
				base.Controls.RemoveAt(0);
				this.Control.Dispose();
				this.Control = null;
			}
		}
	}
}