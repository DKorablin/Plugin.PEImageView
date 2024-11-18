using System;
using System.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceWebBrowserCtrl : IResourceCtrl
	{
		private readonly WebBrowser _browser;

		public VisualizerType Type => VisualizerType.WebBrowser;

		public Control Control => this._browser;

		public ResourceWebBrowserCtrl()
			=> this._browser = new WebBrowser()
			{
				ScriptErrorsSuppressed = true,
			};

		public void BindControl(Object data)
			=> this._browser.Navigate(data.ToString());

		public void Dispose()
			=> this._browser.Dispose();
	}
}