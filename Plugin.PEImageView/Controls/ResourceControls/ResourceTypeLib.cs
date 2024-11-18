using System;
using System.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceTypeLib : IResourceCtrl
	{
		private readonly TypeLibCtrl _control;

		public VisualizerType Type => VisualizerType.TypeLib;

		public Control Control => this._control;

		public ResourceTypeLib()
			=> this._control = new TypeLibCtrl();

		public void BindControl(Object data)
		{
			String typeLibPath = (String)data;
			this._control.AttachTypeLib(typeLibPath);
		}

		public void Dispose()
			=> this._control.Dispose();
	}
}