using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AlphaOmega.Debug;
using Plugin.PEImageView.Properties;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal partial class ToolBarCtrl : UserControl
	{
		private ToolStrip _resourceToolBar;

		public ToolBarCtrl()
			=> this.InitializeComponent();

		public void BindControl(IEnumerable<CommCtrl.TBBUTTON> buttons)
		{
			if(this._resourceToolBar == null)
			{
				this._resourceToolBar = new ToolStrip();
				base.Controls.Add(this._resourceToolBar);
			} else
				this._resourceToolBar.Items.Clear();

			foreach(CommCtrl.TBBUTTON button in buttons)
			{
				ToolStripItem bn;
				if(button.fsStyle == CommCtrl.TBSTYLE.BTNS_SEP)
					bn = new ToolStripSeparator();
				else
				{
					bn = new ToolStripButton
					{
						ToolTipText = button.iString,
						Image = Resources.ToolBar_Empty,
						ImageTransparentColor = Color.Magenta
					};
				}
				this._resourceToolBar.Items.Add(bn);
			}
		}

		/// <summary>Clean up any resources being used.</summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(Boolean disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
				if(this._resourceToolBar != null)
				{
					this._resourceToolBar.Dispose();
					this._resourceToolBar = null;
				}
			}
			base.Dispose(disposing);
		}
	}
}