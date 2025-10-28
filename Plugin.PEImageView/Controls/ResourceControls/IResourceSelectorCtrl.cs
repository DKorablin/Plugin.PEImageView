using System;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	/// <summary>Interface supporting data index selection</summary>
	internal interface IResourceSelectorCtrl : IResourceCtrl
	{
		/// <summary>Data the user must select before displaying</summary>
		String[] SelectableData { get; }
		/// <summary>Select data by index</summary>
		/// <param name="index">Data index</param>
		void SelectData(Int32 index);
	}
}