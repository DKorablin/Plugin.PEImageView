using System;
using System.Collections.Generic;
using AlphaOmega.Debug.NTDirectory.Resources;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceMenuCtrl : IResourceCtrl
	{
		private readonly MenuCtrl _control;

		public VisualizerType Type => VisualizerType.Menu;

		public System.Windows.Forms.Control Control => this._control;

		public ResourceMenuCtrl()
			=> this._control = new MenuCtrl();

		public void BindControl(Object data)
			=> this._control.BindControl((IEnumerable<ResourceMenu.MenuItem>)data);

		public void Dispose()
			=> this._control.Dispose();
	}
}