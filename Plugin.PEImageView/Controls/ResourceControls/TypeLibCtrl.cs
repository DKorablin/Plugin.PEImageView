using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser;
using Plugin.PEImageView.Controls.ResourceControls.TypeLib.UI;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal partial class TypeLibCtrl : UserControl
	{
		private readonly PluginWindows _plugin;

		public TypeLibCtrl(PluginWindows plugin)
		{
			this._plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
			this.InitializeComponent();
		}

		public void AttachTypeLib(String typeLibPath)
		{
#if NETFRAMEWORK
			TypeLibImporterFlags flags = TypeLibImporterFlags.ReflectionOnlyLoading;
			ComTypeAnalyzer analyzer = new ComTypeAnalyzer(this._plugin, typeLibPath, flags, Environment.CurrentDirectory);

			TreeNodeTypeLib node = new TreeNodeTypeLib(analyzer);
			tvReflection.BindAssembly(node);
#else
			MessageBox.Show(this, "Type library import is only supported when running under .NET Framework.", "TypeLib", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
		}
	}
}