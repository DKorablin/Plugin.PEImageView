using System;
using System.Reflection;
using AlphaOmega.Windows.Forms;
using Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser;

namespace Plugin.PEImageView.Controls.ResourceControls.TypeLib.UI
{
	internal class TreeNodeTypeLib : TreeNodeAsm
	{
		private ComTypeAnalyzer _analyzer;

		public override Assembly Assembly
		{
			get
			{
				if(base.Assembly == null)
				{
					base.Assembly = this._analyzer.ImportAssembly();
					this._analyzer = null;
				}
				return base.Assembly;
			}
			set { base.Assembly = value; }
		}

		public TreeNodeTypeLib(ComTypeAnalyzer analyzer)
			: base(analyzer.InputFile)
		{
			_ = analyzer ?? throw new ArgumentNullException(nameof(analyzer));

			this._analyzer = analyzer;
			base.AssemblyPath = analyzer.InputFile;
		}
	}
}