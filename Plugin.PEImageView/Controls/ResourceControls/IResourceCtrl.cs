using System;
using System.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	/// <summary>Resource Element Displays</summary>
	internal interface IResourceCtrl : IDisposable
	{
		/// <summary>Control type</summary>
		VisualizerType Type { get; }

		/// <summary>Custom Control</summary>
		Control Control { get; }

		/// <summary>Apply data to the control</summary>
		/// <param name="data">Data</param>
		/// <param name="index">Index from <see cref="T:SelectableData"/> or null</param>
		void BindControl(Object data);
	}
}