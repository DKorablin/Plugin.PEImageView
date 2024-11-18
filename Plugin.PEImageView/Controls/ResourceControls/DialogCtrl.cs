using System;
using System.Drawing;
using System.Windows.Forms;
using AlphaOmega.Debug.NTDirectory.Resources;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal partial class DialogCtrl : UserControl
	{
		#region Fields
		private static Point? _baseUnits;
		private DialogTemplate _template;
		private Form _resourceDlg;
		private readonly PluginWindows _plugin;
		private const Int32 _denominatorX = 4;
		private const Int32 _denominatorY = 8;
		#endregion Fields

		private static Point BaseUnits
		{
			get
			{
				if(!DialogCtrl._baseUnits.HasValue)
				{
					Int64 baseUnits = NativeMethods.GetDialogBaseUnits();
					DialogCtrl._baseUnits = new Point(
						unchecked((Int16)baseUnits),
						unchecked((Int16)((UInt32)baseUnits >> 16)));
				}
				return DialogCtrl._baseUnits.Value;
			}
		}

		public DialogCtrl(PluginWindows plugin)
		{
			this._plugin = plugin;
			InitializeComponent();
		}

		public void BindTemplate(DialogTemplate template)
		{
			_ = template ?? throw new ArgumentNullException(nameof(template));

			//TODO: Будет работать только для системного шрифта (8px MSSansSerif)
			//http://cboard.cprogramming.com/windows-programming/98006-how-convert-dialog-units-pixels.html
			Point baseUnits = DialogCtrl.BaseUnits;

			this.RemoveDlg();
			this._template = template;
			this._resourceDlg = new Form
			{
				TopLevel = false,
				Parent = this,
				Width = MulDiv(template.CX, baseUnits.X, _denominatorX),//(template.CX * baseUnits.X) / 4;
				Height = MulDiv(template.CY, baseUnits.Y, _denominatorY)//(template.CY * baseUnits.Y) / 8;
			};

			if(template.Font != null)
				this._resourceDlg.Font = new Font(template.Font.Value.TypeFace, template.Font.Value.FontSize);

			if((template.Styles & AlphaOmega.Debug.WinUser.WS.WS_CHILD) == AlphaOmega.Debug.WinUser.WS.WS_CHILD)
				this._resourceDlg.FormBorderStyle = FormBorderStyle.None;
			if((template.StylesEx & AlphaOmega.Debug.WinUser.WS_EX.TOOLWINDOW) == AlphaOmega.Debug.WinUser.WS_EX.TOOLWINDOW)
				this._resourceDlg.FormBorderStyle = FormBorderStyle.SizableToolWindow;

			this._resourceDlg.Location = new Point(template.X, template.Y);
			this._resourceDlg.Text = template.Title;
			if(template.Font != null)
				this._resourceDlg.Font = new Font(template.Font.Value.TypeFace, template.Font.Value.FontSize);

			foreach(var control in template.Controls)
			{
				Control ctrl = null;
				String ctrlTitle = control.ItemText.HasValue && control.ItemText.Value.Type == ResourceDialog.SzInt.SzIntResult.Name ? control.ItemText.Value.Name : String.Empty;
				switch(control.ItemSystemClass)
				{
				case DialogItemTemplate.ControlSystemClass.Button:
					// HACK: Im lazy to find anoter route...
					//AlphaOmega.Debug.WinNT.Resource.BS buttonStyle = (AlphaOmega.Debug.WinNT.Resource.BS)((UInt32)control.Styles & (UInt32)AlphaOmega.Debug.WinNT.Resource.BS.TYPEMASK);

					DialogButtonTemplate tplButton = (DialogButtonTemplate)control;
					ButtonBase btn = null;
					if(tplButton.IsGroupBox)
						ctrl = new GroupBox() { Text = ctrlTitle, };
					else if(tplButton.IsCheckBox)
						ctrl = btn = new CheckBox();
					else if(tplButton.IsRadioButton || tplButton.IsAutoRadioButton)
						ctrl = btn = new RadioButton();
					else
						ctrl = btn = new Button();

					if(btn != null)
					{
						btn.Text = ctrlTitle;
						if(tplButton.IsFlat)
							btn.FlatStyle = FlatStyle.Flat;
					}
					break;
				case DialogItemTemplate.ControlSystemClass.ComboBox:
					ctrl = new ComboBox();
					break;
				case DialogItemTemplate.ControlSystemClass.Edit:
					//AlphaOmega.Debug.WinNT.Resource.ES editStyle = (AlphaOmega.Debug.WinNT.Resource.ES)((UInt32)control.Styles & 0x00000FFF);

					DialogEditTemplate tplEdit = (DialogEditTemplate)control;

					TextBox txt;
					ctrl = txt = new TextBox() { Text = "Sample edit box", };
					if(tplEdit.IsReadOnly)
						txt.ReadOnly = true;
					if(tplEdit.IsUpperCase)
						txt.CharacterCasing = CharacterCasing.Upper;
					if(tplEdit.IsLowerCase)
						txt.CharacterCasing = CharacterCasing.Lower;
					break;
				case DialogItemTemplate.ControlSystemClass.ListBox:
					ctrl = new ListBox();
					break;
				case DialogItemTemplate.ControlSystemClass.ScrollBar:
					ctrl = new VScrollBar();
					break;
				case DialogItemTemplate.ControlSystemClass.Static:
					ctrl = new Label() { Text = ctrlTitle, };
					break;
				default:
					switch(control.ItemClass)
					{
					case "SysTabControl32":
						ctrl = DialogCtrl.CreateTabCtrl();
						break;
					case "SysListView32":
						ctrl = DialogCtrl.CreateListViewCtrl();
						break;
					case "RichEdit20A":
						ctrl = DialogCtrl.CreateRichEditCtrl();
						break;
					case "msctls_progress32":
						ctrl = DialogCtrl.CreateProgressBarCtrl();
						break;
					case "SysTreeView32":
						ctrl = DialogCtrl.CreateTreeViewCtrl();
						break;
					case "msctls_updown32":
						ctrl = DialogCtrl.CreateUpDownCtrl();
						break;
					default:
						this._plugin.Trace.TraceInformation("RT_DIALOG Control class: '{0}' system class: '{1}' not implemented", control.ItemClass, control.ItemSystemClass);
						break;
					}
					break;
				}

				if(ctrl != null)
				{
					if(control.IsDisabled)
						ctrl.Enabled = false;

					Int32 x = 
						MulDiv(control.X, baseUnits.X, _denominatorX);
						//(Int32)((control.X * baseUnits.X) / 4.333f);
					Int32 y = 
						MulDiv(control.Y, baseUnits.Y, _denominatorY);
						//(Int32)((control.Y * baseUnits.Y) / 8.333f);
					ctrl.Location = new Point(x, y);

					Int32 cx = 
						MulDiv(control.CX, baseUnits.X, _denominatorX);
						//(Int32)((control.CX * baseUnits.X) / 4.333f);
					Int32 cy = 
						MulDiv(control.CY, baseUnits.Y, _denominatorY);
						//(Int32)((control.CY * baseUnits.Y) / 8.333f);
					ctrl.Font = this._resourceDlg.Font;
					ctrl.Size = new Size(cx, cy);
					ctrl.Name = control.ControlID.ToString();
					this._resourceDlg.Controls.Add(ctrl);
				}
			}
			NativeMethods.RECT rect = new NativeMethods.RECT()
			{
				bottom = template.CY,
				right = template.CX,
				left = template.X,
				top = template.Y,
			};
			/*Boolean result = NativeMethods.MapDialogRect(this.Dlg.Handle, ref rect);
			if(!result)
				throw new Win32Exception();*/
			this._resourceDlg.Show();
		}

		private static Int32 MulDiv(Int32 number, Int32 numerator, Int32 denominator)
			=> (Int32)Math.DivRem(Math.BigMul(number, numerator), denominator, out Int64 unused);

		private static NumericUpDown CreateUpDownCtrl()
			=> new NumericUpDown()
			{
				Value = 0,
			};

		private static TabControl CreateTabCtrl()
		{
			TabControl ctrl = new TabControl();
			ctrl.TabPages.AddRange(new TabPage[] { new TabPage("Tab 1"), new TabPage("Tab 2"), new TabPage("Tab 3"), new TabPage("Tab 4"), new TabPage("Tab 5"), });
			return ctrl;
		}

		private static ListView CreateListViewCtrl()
		{
			ListView ctrl = new ListView() { View = View.Details, };
			ctrl.Columns.Add("Colors");
			ctrl.Items.AddRange(new ListViewItem[] { new ListViewItem("Yellow"), new ListViewItem("Red"), new ListViewItem("Green"), new ListViewItem("Magenta"), new ListViewItem("Cyan"), new ListViewItem("Blue"), });
			return ctrl;
		}

		private static RichTextBox CreateRichEditCtrl()
			=> new RichTextBox() { Text = "RichEdit2", };

		private static TreeView CreateTreeViewCtrl()
		{
			TreeView ctrl = new TreeView();
			ctrl.Nodes.Add("Expanded Node");
			ctrl.Nodes.Add("Leaf");
			ctrl.Nodes[0].Nodes.AddRange(new TreeNode[] { new TreeNode("Expanded Node"), new TreeNode("Collapsed Node"), });
			ctrl.Nodes[0].Nodes[0].Nodes.AddRange(new TreeNode[] { new TreeNode("Leaf"), new TreeNode("Leaf"), });
			ctrl.Nodes[0].Nodes[0].Expand();
			ctrl.Nodes[0].Nodes[1].Nodes.AddRange(new TreeNode[] { new TreeNode("Leaf"), new TreeNode("Leaf"), });
			ctrl.Nodes[0].Expand();

			return ctrl;
		}

		private static ProgressBar CreateProgressBarCtrl()
			=> new ProgressBar() { Minimum = 1, Maximum = 10, Value = 5, };

		private void RemoveDlg()
		{
			if(this._resourceDlg != null)
			{
				foreach(Control ctrl in this._resourceDlg.Controls)
					ctrl.Dispose();
				this._resourceDlg.Close();
				this._resourceDlg.Font.Dispose();
				this._resourceDlg.Dispose();
				this._resourceDlg = null;
			}
		}

		/// <summary>Clean up any resources being used.</summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(Boolean disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
				this.RemoveDlg();
			}
			base.Dispose(disposing);
		}
	}
}