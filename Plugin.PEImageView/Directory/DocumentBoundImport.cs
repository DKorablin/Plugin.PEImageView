namespace Plugin.PEImageView.Directory
{
	public partial class DocumentBoundImport : DocumentBase
	{
		public DocumentBoundImport()
			: base(PeHeaderType.DIRECTORY_BOUND_IMPORT)
			=> this.InitializeComponent();

		protected override void ShowFile(AlphaOmega.Debug.PEFile info)
		{
			lvBoundImport.Plugin = base.Plugin;
			lvBoundImport.DataBind(info.BoundImport);
		}
	}
}