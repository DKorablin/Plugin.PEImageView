using System;
using System.Windows.Forms;
using TemplateMenu = AlphaOmega.Debug.NTDirectory.Resources.ResourceMenu.MenuItem;
using TemplateSubMenu = AlphaOmega.Debug.NTDirectory.Resources.ResourceMenu.MenuPopupItem;
using System.Collections.Generic;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal partial class MenuCtrl : UserControl
	{
		private MenuStrip _resourceMenu;

		public MenuCtrl()
			=> InitializeComponent();

		public void BindControl(IEnumerable<TemplateMenu> template)
		{
			if(this._resourceMenu == null)
			{
				this._resourceMenu = new MenuStrip()
				{
					GripStyle = ToolStripGripStyle.Visible,
				};
				base.Controls.Add(this._resourceMenu);
			} else
				this._resourceMenu.Items.Clear();

			foreach(TemplateMenu item in template)
			{
				if(item.IsSeparator)
					this._resourceMenu.Items.Add(new ToolStripSeparator());
				else
				{
					ToolStripMenuItem menuItem = MenuCtrl.CreateToolStripMenu(item.Title);
					if(item.SubItems != null)
					{
						foreach(var menuSubItem in this.CreateSubMenu(item.SubItems))
							menuItem.DropDownItems.Add(menuSubItem);

					}
					this._resourceMenu.Items.Add(menuItem);
				}
			}
		}

		private IEnumerable<ToolStripItem> CreateSubMenu(TemplateSubMenu[] template)
		{
			foreach(var item in template)
			{
				if(item.IsSeparator)
					yield return new ToolStripSeparator();
				else
				{
					ToolStripMenuItem menuItem = MenuCtrl.CreateToolStripMenu(item.Title);
					if(item.SubItems != null)
						foreach(var menuSubItem in this.CreateSubMenu(item.SubItems))
							menuItem.DropDownItems.Add(menuSubItem);
					yield return menuItem;
				}
			}
		}

		private static ToolStripMenuItem CreateToolStripMenu(String title)
		{
			ToolStripMenuItem result = new ToolStripMenuItem();

			Int32 indexOfT = title.IndexOf('\t');
			if(indexOfT == -1)
				result.Text = title;
			else
			{
				result.Text = title.Substring(0, indexOfT);
				result.ShortcutKeyDisplayString = title.Substring(indexOfT + 1);
			}
			return result;
		}

		/// <summary>Clean up any resources being used.</summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(Boolean disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
				if(this._resourceMenu != null)
				{
					this._resourceMenu.Dispose();
					this._resourceMenu = null;
				}
			}
			base.Dispose(disposing);
		}
	}
}