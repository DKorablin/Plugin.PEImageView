using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using Plugin.PEImageView.Directory;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceBinaryViewCtrl : IResourceSelectorCtrl
	{
		private readonly ByteViewer _control;

		public VisualizerType Type => VisualizerType.BinView;

		public Control Control => this._control;

		public String[] SelectableData
			=> Array.ConvertAll(DocumentBinary.DisplayModes, delegate(DisplayMode mode) { return mode.ToString(); });

		public ResourceBinaryViewCtrl()
			=> this._control = new ByteViewer();

		public void SelectData(Int32 index)
		{
			DisplayMode mode = DocumentBinary.DisplayModes[index];
			this._control.SetDisplayMode(mode);
		}

		public void BindControl(Object data)
		{
			Byte[] bytes = (Byte[])data;

			this._control.SetBytes(bytes);
		}

		public void Dispose()
			=> this._control.Dispose();
	}
}