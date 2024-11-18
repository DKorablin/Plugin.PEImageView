using System;
using DebugPe = AlphaOmega.Debug.NTDirectory.Debug;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentDebug : DocumentBase
	{
		public DocumentDebug()
			:base(PeHeaderType.DIRECTORY_DEBUG)
			=> InitializeComponent();

		protected override void ShowFile(AlphaOmega.Debug.PEFile info)
		{
			lvInfo.Plugin = base.Plugin;
			lvPdb2.Plugin = base.Plugin;
			lvPdb7.Plugin = base.Plugin;
			lvMisc.Plugin = base.Plugin;

			this.ShowKnownDebug(info.Debug);
			lvInfo.DataBind(info.Debug);
		}

		private void ShowKnownDebug(DebugPe debug)
		{
			var misc = debug.Misc;
			if(misc.HasValue)
				lvMisc.DataBind(misc.Value);
			else
				tabHDebug.Controls.Remove(tabMisc);

			var pdb2 = debug.Pdb2CodeView;
			if(pdb2.HasValue)
			{
				lvPdb2.DataBind(pdb2.Value.Info);
				lvPdb2.Items.Add(lvPdb2.CreateListItem(pdb2.Value, pdb2.Value.GetType().GetMember("PdbFileName")[0]));
			} else tabHDebug.Controls.Remove(tabPDB2);

			var pdb7 = debug.Pdb7CodeView;
			if(pdb7.HasValue)
			{
				lvPdb7.DataBind(pdb7.Value.Info);
				lvPdb7.Items.Add(lvPdb7.CreateListItem(pdb7.Value, pdb7.Value.GetType().GetMember("PdbFileName")[0]));
			} else
				tabHDebug.Controls.Remove(tabPDB7);
		}
	}
}