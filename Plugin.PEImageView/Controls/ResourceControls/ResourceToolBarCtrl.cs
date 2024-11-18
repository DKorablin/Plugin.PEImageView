using System;
using System.Windows.Forms;
using System.Collections.Generic;
using AlphaOmega.Debug;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceToolBarCtrl : IResourceCtrl
	{
		private readonly ToolBarCtrl _control;

		public VisualizerType Type => VisualizerType.ToolBar;

		public Control Control => this._control;

		public ResourceToolBarCtrl()
			=> this._control = new ToolBarCtrl();

		public void BindControl(Object data)
		{
			IEnumerable<CommCtrl.TBBUTTON> buttons = (IEnumerable<CommCtrl.TBBUTTON>)data;
			this._control.BindControl(buttons);
		}

		public void Dispose()
			=> this._control.Dispose();
	}
}