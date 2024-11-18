using System;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;
using AlphaOmega.Debug;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentBinary : DocumentBase
	{
		internal static DisplayMode[] DisplayModes = (DisplayMode[])Enum.GetValues(typeof(DisplayMode));
		private DocumentBinarySettings _settings;

		public override DocumentBaseSettings Settings => this.SettingsI;

		private DocumentBinarySettings SettingsI
			=> this._settings ?? (this._settings = new DocumentBinarySettings());

		public DocumentBinary()
			: base(PeHeaderType.IMAGE_SECTION)
		{
			InitializeComponent();
			tsddlView.Items.AddRange(Array.ConvertAll(DocumentBinary.DisplayModes, delegate(DisplayMode mode) { return mode.ToString(); }));
			tsddlView.SelectedIndex = 0;
		}

		protected override void ShowFile(PEFile info)
		{
			ISectionData section = this.GetSectionData();
			tsbnView.Enabled = this.Plugin.DirectoryViewers.ContainsKey(this.SettingsI.Header);

			bvBytes.SetBytes(section.GetData());
		}

		protected override void SetCaption()
		{
			String[] captions = new String[]
			{
				Constant.GetHeaderName(this.SettingsI.Header),
				this.SettingsI.NodeName,//Наименование секции
				Path.GetFileName(this.Settings.FilePath)
			};

			base.Window.Caption = String.Join(" - ", captions);
			//base.SetCaption();
		}

		private ISectionData GetSectionData()
		{
			PEFile pe = base.GetFile();
			return pe.Sections.GetSection(this.SettingsI.NodeName);
		}

		private void tsddlView_SelectedIndexChanged(Object sender, EventArgs e)
		{
			DisplayMode mode = DocumentBinary.DisplayModes[tsddlView.SelectedIndex];
			bvBytes.SetDisplayMode(mode);
		}

		private void tsbnSave_Click(Object sender, EventArgs e)
		{
			String peFilePath = Path.GetFullPath(this.Settings.FilePath);
			using(SaveFileDialog dlg = new SaveFileDialog() { InitialDirectory = peFilePath, OverwritePrompt = true, AddExtension = true, DefaultExt = "bin", Filter = "BIN file (*.bin)|*.bin|All files (*.*)|*.*", })
				if(dlg.ShowDialog() == DialogResult.OK)
					bvBytes.SaveToFile(dlg.FileName);
		}

		private void tsbnView_Click(Object sender, EventArgs e)
			=> this.Plugin.CreateWindow(this.SettingsI.Header, new DocumentBaseSettings() { FilePath = this.SettingsI.FilePath });
	}
}