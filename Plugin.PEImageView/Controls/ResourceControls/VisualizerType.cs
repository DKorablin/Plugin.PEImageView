using System;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal enum VisualizerType
	{
		/// <summary>No visualization</summary>
		None,
		/// <summary>List visualization</summary>
		ListView,
		/// <summary>Browser visualization</summary>
		WebBrowser,
		/// <summary>List visualization created via reflection</summary>
		ListViewArray,
		/// <summary>Grid visualization</summary>
		GridView,
		/// <summary>Dialog</summary>
		Dialog,
		/// <summary>Menu</summary>
		Menu,
		/// <summary>Toolbar</summary>
		ToolBar,
		/// <summary>Version</summary>
		Version,
		/// <summary>Byte array</summary>
		BinView,
		/// <summary>COM+</summary>
		TypeLib,
		/// <summary>Picture</summary> 
		Bitmap
	}
}