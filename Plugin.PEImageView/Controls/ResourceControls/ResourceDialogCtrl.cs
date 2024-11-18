using System;
using AlphaOmega.Debug.NTDirectory.Resources;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceDialogCtrl : IResourceCtrl
	{
		private readonly DialogCtrl _control;

		public VisualizerType Type => VisualizerType.Dialog;

		public System.Windows.Forms.Control Control => this._control;

		public ResourceDialogCtrl(PluginWindows plugin)
			=> this._control = new DialogCtrl(plugin);

		public void BindControl(Object data)
			=> this._control.BindTemplate((DialogTemplate)data);

		public void Dispose()
			=> this._control.Dispose();
	}
}