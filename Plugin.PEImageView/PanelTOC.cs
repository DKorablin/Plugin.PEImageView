using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AlphaOmega.Debug;
using AlphaOmega.Windows.Forms;
using Plugin.PEImageView.Bll;
using Plugin.PEImageView.Directory;
using Plugin.PEImageView.Properties;
using Plugin.PEImageView.Source;
using SAL.Windows;

namespace Plugin.PEImageView
{
	public partial class PanelTOC : UserControl
	{
		private const String Caption = "PE/CLI View";
		private SystemImageList _smallImageList= new SystemImageList(SystemImageListSize.SmallIcons);

		private PluginWindows Plugin => (PluginWindows)this.Window.Plugin;

		private IWindow Window => (IWindow)base.Parent;

		private PeHeaderType? SelectedHeader => tvToc.SelectedNode.Tag as PeHeaderType?;

		private String SelectedPE
		{
			get
			{
				TreeNode node = tvToc.SelectedNode;
				while(node.Parent != null)
					node = node.Parent;
				return (String)node.Tag;
			}
		}

		public PanelTOC()
		{
			this.InitializeComponent();
			gridSearch.TreeView = tvToc;
			SystemImageListHelper.SetImageList(tvToc, this._smallImageList, false);
		}

		protected override void OnCreateControl()
		{
			this.Window.SetTabPicture(Resources.iconPe);
			this.Window.Closing += new EventHandler<CancelEventArgs>(this.Window_Closing);
			lvInfo.Plugin = this.Plugin;

			String[] loadedFiles = this.Plugin.Settings.LoadedFiles;
			foreach(String file in loadedFiles)
				this.FillToc(file);
			this.ChangeTitle();

			this.Plugin.Binaries.PeListChanged += new EventHandler<PeListChangedEventArgs>(this.Plugin_PeListChanged);
			this.Plugin.Settings.PropertyChanged += this.Settings_PropertyChanged;
			base.OnCreateControl();
		}

		private void Window_Closing(Object sender, CancelEventArgs e)
		{
			this.Plugin.Binaries.PeListChanged -= new EventHandler<PeListChangedEventArgs>(this.Plugin_PeListChanged);
			this.Plugin.Settings.PropertyChanged -= this.Settings_PropertyChanged;
		}

		/// <summary>Change window title</summary>
		private void ChangeTitle()
			=> this.Window.Caption = tvToc.Nodes.Count > 0
				? $"{PanelTOC.Caption} ({tvToc.Nodes.Count})"
				: this.Window.Caption = PanelTOC.Caption;

		/// <summary>Search for a node in a tree by file path</summary>
		/// <param name="filePath">File path</param>
		/// <returns>The found node in the tree or null</returns>
		private TreeNode FindNode(String filePath)
		{
			foreach(TreeNode node in tvToc.Nodes)
				if(filePath.Equals(node.Tag))
					return node;
			return null;
		}

		protected override Boolean ProcessDialogKey(Keys keyData)
		{
			switch(keyData)
			{
			case Keys.Control | Keys.O:
				this.tsbnOpen_Click(this, EventArgs.Empty);
				return true;
			default:
				return base.ProcessDialogKey(keyData);
			}
		}

		private void Plugin_PeListChanged(Object sender, PeListChangedEventArgs e)
		{
			if(base.InvokeRequired)
				base.Invoke((MethodInvoker)delegate { this.Plugin_PeListChanged(sender, e); });
			else
				switch(e.Type)
				{
				case PeListChangeType.Added:
					TreeNode root = this.FillToc(e.FilePath);
					if(root != null)
						tvToc.SelectedNode = root;
					break;
				case PeListChangeType.Changed:
					TreeNode node = this.FindNode(e.FilePath);
					if(node.IsSelected)
						this.tvToc_AfterSelect(sender, new TreeViewEventArgs(node));
					break;
				case PeListChangeType.Removed:
					this.FindNode(e.FilePath).Remove();
					break;
				default:
					throw new NotImplementedException(e.Type.ToString());
				}
			this.ChangeTitle();
		}

		private void Settings_PropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			switch(e.PropertyName)
			{
			case nameof(PluginSettings.MaxArrayDisplay):
			case nameof(PluginSettings.ShowAsHexValue):
				if(tvToc.SelectedNode != null)
					this.tvToc_AfterSelect(sender, new TreeViewEventArgs(tvToc.SelectedNode));
				break;
			}
		}

		private void OpenBinaryDocument(PeHeaderType type, String nodeName, String filePath)
			=> this.Plugin.CreateWindow<DocumentBinary, DocumentBaseSettings>(
				new DocumentBinarySettings() { FilePath = filePath, Header = type, NodeName = nodeName });

		private void OpenDirectoryDocument(PeHeaderType type, String nodeName, String filePath)
		{
			switch(type)
			{
			case PeHeaderType.DIRECTORY_SECURITY:
				PEFile pe1 = this.Plugin.Binaries.LoadFile(filePath);
				var cert = pe1.Certificate.X509;
				if(cert != null)
					NativeMethods.ViewCertificate(cert);
				break;
			case PeHeaderType.IMAGE_SECTION:
				PEFile pe2 = this.Plugin.Binaries.LoadFile(filePath);
				ISectionData section = pe2.Sections.GetSection(nodeName);
				if(section == null)
					this.Plugin.Trace.TraceInformation("Viewer {0}. Section '{1}' not found", type, nodeName);
				else
					this.OpenBinaryDocument(type, nodeName, filePath);
				break;
			default:
				if(this.Plugin.CreateWindow(type, new DocumentBaseSettings() { FilePath = filePath, }) == null)
					this.Plugin.Trace.TraceInformation("Viewer {0} not implemented", type);
				break;
			}
		}

		private TreeNode FillToc(String filePath)
		{
			//Checking for files already added to the tree
			TreeNode n = this.FindNode(filePath);
			if(n != null)
			{
				tvToc.SelectedNode = n;
				return null;
			}

			TreeNode result = new TreeNode(filePath) { Tag = filePath, };
			result.Nodes.Add(new TreeNode(String.Empty) { ImageIndex = -1, SelectedImageIndex = -1, });
			result.ImageIndex = result.SelectedImageIndex = this._smallImageList.IconIndex(filePath);
			tvToc.Nodes.Add(result);
			return result;
		}

		private static TreeNode CreateDirectoryNode(IDirectory dir, PeHeaderType type)
			=> PanelTOC.CreateDirectoryNode(dir, dir == null || dir.IsEmpty, type);

		private static TreeNode CreateDirectoryNode(Object tag, Boolean isEmpty, PeHeaderType type)
		{
			TreeNode result = new TreeNode() { Tag = type, ImageIndex = -1, SelectedImageIndex = -1, };
			if(isEmpty)
				result.SetNull();
			else
				result.ForeColor = Color.Empty;

			result.Text = Constant.GetHeaderName(type);
			if(tag != null)
				result.ToolTipText = XmlReflectionReader.Instance.FindDocumentation(tag.GetType());
			return result;
		}

		private void tvToc_AfterSelect(Object sender, TreeViewEventArgs e)
		{
			lvInfo.Items.Clear();

			splitToc.Panel2Collapsed = false;
			String filePath = this.SelectedPE;
			if(e.Node.Parent == null)//File description
				lvInfo.DataBind(new FileInfo(filePath));

			try
			{
				base.Cursor = Cursors.WaitCursor;
				PeHeaderType? type = this.SelectedHeader;
				if(type.HasValue)//PE file directory
				{
					Object target = this.Plugin.GetSectionData(type.Value, e.Node.Text, filePath);
					lvInfo.DataBind(target);
				} else if(e.Node.Tag != null && e.Node.Parent != null)
					lvInfo.DataBind(e.Node.Tag);//Generic object
			} finally
			{
				base.Cursor = Cursors.Default;
			}
		}

		private void tvToc_MouseClick(Object sender, MouseEventArgs e)
		{
			switch(e.Button)
			{
			case MouseButtons.Right:
				TreeViewHitTestInfo info = tvToc.HitTest(e.Location);
				if(info.Node != null)
				{
					tvToc.SelectedNode = info.Node;
					cmsToc.Show(tvToc, e.Location);
				}
				break;
			}
		}
		private void tvToc_MouseDoubleClick(Object sender, MouseEventArgs e)
		{
			TreeViewHitTestInfo info = tvToc.HitTest(e.Location);
			PeHeaderType? type = info.Node == null ? null : info.Node.Tag as PeHeaderType?;
			if(type.HasValue && !info.Node.IsNull())
			{
				String filePath = this.SelectedPE;
				this.OpenDirectoryDocument(type.Value, info.Node.Text, filePath);
			}
		}

		private void tsbnOpen_Click(Object sender, EventArgs e)
			=> this.tsbnOpen_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiOpenFile));

		private void tsbnOpen_DropDownItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			tsbnOpen.DropDown.Close(ToolStripDropDownCloseReason.ItemClicked);

			if(e.ClickedItem == tsmiOpenFile)
			{
				using(OpenFileDialog dlg = new OpenFileDialog() { CheckFileExists = true, Multiselect = true, Filter = "Supported files|*.dll;*.exe|DLL (*.dll)|*.dll|Executables (*.exe)|*.exe|All files (*.*)|*.*", Title = "Choose Portable Executable", })
					if(dlg.ShowDialog() == DialogResult.OK)
						foreach(String filePath in dlg.FileNames)
							this.Plugin.Binaries.OpenFile(filePath);
			} else if(e.ClickedItem == tsmiOpenProcess)
			{
				using(ProcessDlg dlg = new ProcessDlg(this.Plugin))
					if(dlg.ShowDialog() == DialogResult.OK)
						foreach(String filePath in dlg.SelectedFiles)
							this.Plugin.Binaries.OpenFile(filePath);
			} else if(e.ClickedItem == tsmiOpenGac)
			{
				using(GacBrowserDlg dlg = new GacBrowserDlg())
					if(dlg.ShowDialog() == DialogResult.OK)
						foreach(String fileName in dlg.GetSelectedAssemblies(GacBrowserDlg.ResultType.FilePath))
							this.Plugin.Binaries.OpenFile(fileName);
			} else if(e.ClickedItem == tsmiOpenBase64)
			{
				using(HexLoadDlg dlg = new HexLoadDlg())
					if(dlg.ShowDialog() == DialogResult.OK)
						this.Plugin.Binaries.OpenFile(dlg.Result);
			} else
				throw new NotSupportedException(e.ClickedItem.ToString());
		}

		private void tvToc_KeyDown(Object sender, KeyEventArgs e)
		{
			switch(e.KeyData)
			{
			case Keys.Delete:
			case Keys.Back:
				this.cmsToc_ItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiTocUnload));
				e.Handled = true;
				break;
			case Keys.Return:
				PeHeaderType? type = this.SelectedHeader;
				if(type.HasValue && !tvToc.SelectedNode.IsNull())
					this.OpenDirectoryDocument(type.Value, tvToc.SelectedNode.Text, this.SelectedPE);
				e.Handled = true;
				break;
			case Keys.C|Keys.Control:
				if(tvToc.SelectedNode != null)
					Clipboard.SetText(tvToc.SelectedNode.Text);
				e.Handled = true;
				break;
			}
		}

		private void cmsToc_Opening(Object sender, CancelEventArgs e)
		{
			TreeNode node = tvToc.SelectedNode;
			tsmiTocUnload.Visible = tsmiTocExplorerView.Visible = tsmiTocBinView.Visible = false;
			Boolean showUnload = false;
			Boolean showBinView = false;

			if(node != null)
			{
				tsmiTocUnload.Visible = tsmiTocExplorerView.Visible = showUnload = node.Parent == null;//PE File

				PeHeaderType? type = node.Tag as PeHeaderType?;
				if(!node.IsNull() && type != null)
				{
					String filePath = this.SelectedPE;

					tsmiTocBinView.Visible = showBinView = this.Plugin.GetSectionData(type.Value, node.Text, filePath) is ISectionData;
				}
			}

			e.Cancel = !showUnload && !showBinView;
		}

		private void cmsToc_ItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			TreeNode node=tvToc.SelectedNode;
			if(e.ClickedItem == tsmiTocUnload)
			{
				if(node != null && node.Parent == null)
				{
					this.Plugin.Binaries.UnloadFile(this.SelectedPE);
					this.Plugin.Binaries.CloseFile(this.SelectedPE);
				}
			} else if(e.ClickedItem == tsmiTocBinView)
			{
				String filePath = this.SelectedPE;
				PeHeaderType type = (PeHeaderType)node.Tag;
				this.OpenBinaryDocument(type, node.Text, filePath);
			} else if(e.ClickedItem == tsmiTocExplorerView)
			{
				String filePath = this.SelectedPE;
				Shell32.OpenFolderAndSelectItem(Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
			}
			else
				throw new NotImplementedException(e.ClickedItem.ToString());
		}

		private void tvToc_BeforeExpand(Object sender, TreeViewCancelEventArgs e)
		{
			if(e.Action == TreeViewAction.Expand && e.Node.IsClosedRootNode())
			{
				String filePath = (String)e.Node.Tag;

				PEFile info;
				try
				{
					info = this.Plugin.Binaries.LoadFile(filePath);
					if(info == null)
					{
						e.Node.Nodes[0].SetException("Error opening file");
						return;
					} else
						e.Node.Nodes.Clear();
				} catch(Exception exc)
				{
					e.Node.Nodes[0].SetException(exc);
					e.Node.Nodes[0].Tag = exc;
					return;
				}

				List<TreeNode> nodes = new List<TreeNode>();
				nodes.AddRange(new TreeNode[]
				{
					PanelTOC.CreateDirectoryNode(null, false, PeHeaderType.IMAGE_DOS_HEADER),
				});
				TreeNode nt = PanelTOC.CreateDirectoryNode(null, false, PeHeaderType.IMAGE_NT_HEADERS);
				TreeNode sections = PanelTOC.CreateDirectoryNode(null, false, PeHeaderType.IMAGE_SECTION_HEADER);

				foreach(WinNT.IMAGE_SECTION_HEADER section in info.Header.Sections)
					sections.Nodes.Add(new TreeNode(section.Section) { Tag = PeHeaderType.IMAGE_SECTION, ImageIndex = -1, SelectedImageIndex = -1, });

				nt.Nodes.AddRange(new TreeNode[] {
					PanelTOC.CreateDirectoryNode(null, false, PeHeaderType.IMAGE_FILE_HEADER),
					sections,
				});

				if(info.Header.IsValid)
				{
					nt.Nodes.Add(PanelTOC.CreateDirectoryNode(null, !info.Header.SymbolTable.HasValue, PeHeaderType.IMAGE_COFF_HEADER));

					TreeNode corNode = PanelTOC.CreateDirectoryNode(info.ComDescriptor, PeHeaderType.DIRECTORY_COM_DECRIPTOR);

					if(info.ComDescriptor == null)
						corNode.SetNull();
					else
						corNode.Nodes.AddRange(new TreeNode[]
					{
						PanelTOC.CreateDirectoryNode(info.ComDescriptor.MetaData, PeHeaderType.DIRECTORY_COR_METADATA),
						PanelTOC.CreateDirectoryNode(info.ComDescriptor.Resources,PeHeaderType.DIRECTORY_COR_RESOURCE),
						PanelTOC.CreateDirectoryNode(info.ComDescriptor.VTable, PeHeaderType.DIRECTORY_COR_VTABLE),
						PanelTOC.CreateDirectoryNode(info.ComDescriptor.CodeManagerTable,PeHeaderType.DIRECTORY_COR_CMT),
						PanelTOC.CreateDirectoryNode(info.ComDescriptor.Eat,PeHeaderType.DIRECTORY_COR_EAT),
						PanelTOC.CreateDirectoryNode(info.ComDescriptor.ManagedNativeHeader,PeHeaderType.DIRECTORY_COR_MNH),
						PanelTOC.CreateDirectoryNode(info.ComDescriptor.StrongNameSignature,PeHeaderType.DIRECTORY_COR_SN),
					});

					TreeNode optional = PanelTOC.CreateDirectoryNode(null, false, PeHeaderType.IMAGE_OPTIONAL_HEADER);
					optional.Nodes.AddRange(new TreeNode[]
					{
						PanelTOC.CreateDirectoryNode(info.Architecture,PeHeaderType.DIRECTORY_ARCHITECTURE),
						PanelTOC.CreateDirectoryNode(info.BoundImport, PeHeaderType.DIRECTORY_BOUND_IMPORT),
						PanelTOC.CreateDirectoryNode(info.Certificate, PeHeaderType.DIRECTORY_SECURITY),
						corNode,
						PanelTOC.CreateDirectoryNode(info.Debug, PeHeaderType.DIRECTORY_DEBUG),
						PanelTOC.CreateDirectoryNode(info.DelayImport, PeHeaderType.DIRECTORY_DELAY_IMPORT),
						PanelTOC.CreateDirectoryNode(info.ExceptionTable,PeHeaderType.DIRECTORY_EXCEPTION),
						PanelTOC.CreateDirectoryNode(info.Export, PeHeaderType.DIRECTORY_EXPORT),
						PanelTOC.CreateDirectoryNode(info.GlobalPtr,PeHeaderType.DIRECTORY_GLOBALPTR),
						PanelTOC.CreateDirectoryNode(info.Iat,PeHeaderType.DIRECTORY_IAT),
						PanelTOC.CreateDirectoryNode(info.Import, PeHeaderType.DIRECTORY_IMPORT),
						PanelTOC.CreateDirectoryNode(info.LoadConfig, PeHeaderType.DIRECTORY_LOAD_CONFIG),
						PanelTOC.CreateDirectoryNode(info.Relocations, PeHeaderType.DIRECTORY_RELOCATION),
						PanelTOC.CreateDirectoryNode(info.Resource, PeHeaderType.DIRECTORY_RESOURCE),
						PanelTOC.CreateDirectoryNode(info.Tls, PeHeaderType.DIRECTORY_TLS),
					});

					nt.Nodes.AddRange(new TreeNode[]
					{
						optional,
					});
				}
				nodes.Add(nt);

				e.Node.Nodes.AddRange(nodes.ToArray());
			}
		}

		private void tvToc_DragEnter(Object sender, DragEventArgs e)
			=> e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;

		private void tvToc_DragDrop(Object sender, DragEventArgs e)
		{
			foreach(String filePath in (String[])e.Data.GetData(DataFormats.FileDrop))
				this.Plugin.Binaries.OpenFile(filePath);
		}
	}
}