using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser;
using Plugin.PEImageView.Controls.ResourceControls.TypeLib.UI;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal partial class TypeLibCtrl : UserControl
	{
		public TypeLibCtrl()
			=> InitializeComponent();

		public void AttachTypeLib(String typeLibPath)
		{
			TypeLibImporterFlags flags = TypeLibImporterFlags.ReflectionOnlyLoading;
			ComTypeAnalyzer analyzer = new ComTypeAnalyzer(typeLibPath, flags, Environment.CurrentDirectory);

			TreeNodeTypeLib node = new TreeNodeTypeLib(analyzer);
			tvReflection.BindAssembly(node);
		}
	}
}