using System;
using System.Drawing;
using System.IO;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceBitmapCtrl : IResourceCtrl
	{
		private readonly System.Windows.Forms.PictureBox _control;

		public VisualizerType Type => VisualizerType.Bitmap;

		public System.Windows.Forms.Control Control => this._control;

		public ResourceBitmapCtrl()
			=> this._control = new System.Windows.Forms.PictureBox();

		public void BindControl(Object data)
			=> this._control.Image = Image.FromStream((Stream)data);

		public void Dispose()
			=> this._control.Dispose();
	}
}