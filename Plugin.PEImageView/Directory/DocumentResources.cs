using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlphaOmega.Debug.NTDirectory;
using Plugin.PEImageView.Controls.ResourceControls;
using AlphaOmega.Debug.NTDirectory.Resources;
using System.Collections.Generic;
using AlphaOmega.Debug;
using System.Data;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentResources : DocumentBase
	{
		private enum TreeImages
		{
			FolderClosed = 0,
			FolderOpened = 1,
			Binary = 2,
			RT_ACCELERATOR = 3,
			RT_BITMAP = 4,
			RT_DIALOG = 5,
			RT_HTML = 6,
			RT_ICON = 7,
			RT_MENU = 8,
			RT_STRING = 9,
			RT_TOOLBAR = 10,
			RT_VERSION = 11,
		}

		public DocumentResources()
			: base(PeHeaderType.DIRECTORY_RESOURCE)
			=> this.InitializeComponent();

		protected override void ShowFile(AlphaOmega.Debug.PEFile info)
		{
			lvInfo.Plugin = base.Plugin;
			pnlResources.Plugin = base.Plugin;

			Boolean isDirty = tvResource.Nodes.Count > 0;
			tvResource.Nodes.Clear();

			Resource resource = info.Resource;

			TreeNode root = new TreeNode
			{
				Text = Path.GetFileName(info.Source),
				Tag = resource.Header.Value
			};
			root.ImageIndex = root.SelectedImageIndex = (Int32)TreeImages.FolderClosed;

			foreach(ResourceDirectory dir in resource)
				CreateResourceNodeRecursive(root, dir);

			tvResource.Nodes.Add(root);
			if(isDirty)
				tvResource.SelectedNode = root;
		}

		private void tvResource_DragEnter(Object sender, DragEventArgs e)
			=> e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;

		private void tvResource_DragDrop(Object sender, DragEventArgs e)
		{
			String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
			if(files.Length > 0)
				base.OpenFile(files[0]);
		}

		private void tvResource_AfterExpand(Object sender, TreeViewEventArgs e)
		{
			if(e.Node.ImageIndex == (Int32)TreeImages.FolderClosed)
				e.Node.ImageIndex = e.Node.SelectedImageIndex = (Int32)TreeImages.FolderOpened;
		}

		private void tvResource_AfterCollapse(Object sender, TreeViewEventArgs e)
		{
			if(e.Node.ImageIndex == (Int32)TreeImages.FolderOpened)
				e.Node.ImageIndex = e.Node.SelectedImageIndex = (Int32)TreeImages.FolderClosed;
		}

		private void tvResource_AfterSelect(Object sender, TreeViewEventArgs e)
		{
			base.SuspendLayout();

			try
			{
				Boolean clearView = true;
				lvInfo.Items.Clear();

				if(e.Node.Tag == null)
					splitResourceTree.Panel2Collapsed = true;
				else
				{
					splitResourceTree.Panel2Collapsed = false;

					if(e.Node.Parent == null)
						lvInfo.DataBind(e.Node.Tag);
					else
					{
						ResourceDirectory directory = (ResourceDirectory)e.Node.Tag;
						if(directory.Directory.HasValue)
							lvInfo.DataBind(directory.Directory.Value);
						else if(directory.DataEntry.HasValue)
						{
							lvInfo.DataBind(directory.DataEntry.Value);

							if(directory.Parent != null && directory.Parent.Parent != null)
							{
								clearView = false;
								this.ShowDataControls();
							}
						}
					}
				}

				if(clearView)
				{
					pnlResources.RemoveControl();
					this.HideDataControls();
				}
			} finally
			{
				base.ResumeLayout(false);
				tvResource.Focus();
			}
		}

		private void tsddlSelectableData_SelectedIndexChanged(Object sender, EventArgs e)
			=> ((IResourceSelectorCtrl)pnlResources.Control).SelectData(tsddlSelectableData.SelectedIndex);

		private void tsbnSave_Click(Object sender, EventArgs e)
		{
			ResourceDirectory directory = (ResourceDirectory)tvResource.SelectedNode.Tag;
			using(SaveFileDialog dlg = new SaveFileDialog() { FileName = directory.Parent.Parent.DirectoryEntry.NameType.ToString() + ".bin", OverwritePrompt = true, AddExtension = true, DefaultExt = "bin", Filter = "BIN file (*.bin)|*.bin|All files (*.*)|*.*", })
				if(dlg.ShowDialog() == DialogResult.OK)
				{
					Byte[] directoryContents = directory.GetData();
					File.WriteAllBytes(dlg.FileName, directory.GetData());
				}
		}

		private void tsddbAlternateType_SelectedIndexChanged(Object sender, EventArgs e)
		{
			VisualizerType view = (VisualizerType)Enum.Parse(typeof(VisualizerType), (String)tsddlAlternateType.SelectedItem);
			ResourceDirectory directory = (ResourceDirectory)tvResource.SelectedNode.Tag;

			//The "Save" button is only for binary view.
			tsbnSave.Visible = view == VisualizerType.BinView;
			pnlResources.ShowControl(view, this.ExtractData(view, directory));
			this.ShowDataSelectorControl();
		}

		private void ShowDataSelectorControl()
		{
			if(pnlResources.Control is IResourceSelectorCtrl ctrl)
			{
				tsddlSelectableData.Items.Clear();
				tsddlSelectableData.Items.AddRange(ctrl.SelectableData);
				if(tsddlSelectableData.Items.Count > 0)
					tsddlSelectableData.SelectedIndex = 0;
				tsddlSelectableData.Visible = true;
			} else
				tsddlSelectableData.Visible = false;
		}

		private void HideDataControls()
		{
			if(pnlResources.Control == null)
				tsResourceAction.Visible = false;
		}

		/// <summary>Display controls with data</summary>
		private void ShowDataControls()
		{
			tsResourceAction.Visible = true;
			foreach(ToolStripItem ctrl in tsResourceAction.Items)
				ctrl.Visible = false;

			ResourceDirectory directory = (ResourceDirectory)tvResource.SelectedNode.Tag;

			Int32 selectedIndex = 0;
			String[] visualizers = GetCompatibleVisualizers(directory).Select(p => p.ToString()).ToArray();
			if(tsddlAlternateType.Items.Cast<String>().SequenceEqual(visualizers))
				selectedIndex = tsddlAlternateType.SelectedIndex;

			tsddlAlternateType.Items.Clear();
			tsddlAlternateType.Items.AddRange(visualizers);
			tsddlAlternateType.Visible = tsddlAlternateType.Items.Count > 1;
			tsddlAlternateType.SelectedIndex = selectedIndex;

			foreach(ToolStripItem ctrl in tsResourceAction.Items)
				if(ctrl.Visible)
				{
					tsResourceAction.Visible = true;
					return;
				}
			tsResourceAction.Visible = false;
		}

		/// <summary>Create a resource directory hierarchy</summary>
		/// <param name="parent">Parent tree node</param>
		/// <param name="root">Root directory of the subdirectory to be populated</param>
		private static void CreateResourceNodeRecursive(TreeNode parent, ResourceDirectory root)
		{
			TreeNode result = null;
			foreach(TreeNode node in parent.Nodes)
				if(node.Text == root.Name)
				{
					result = node;
					break;
				}
			if(result == null)
			{
				String text = root.Name;
				TreeImages img;
				if(root.Parent == null || root.Parent.Parent == null)
					img = TreeImages.FolderClosed;
				else
				{
					Int32 nameAddress = (Int32)root.DirectoryEntry.NameAddress;
					text = $"{nameAddress} [{ResourceDirectory.GetLangName(nameAddress)}]";
					img = Utils.EnumParse<TreeImages>(root.Parent.Parent.DirectoryEntry.NameType.ToString(), TreeImages.Binary);
				}

				result = new TreeNode(text);
				result.ImageIndex = result.SelectedImageIndex = (Int32)img;
				parent.Nodes.Add(result);
			}
			result.Tag = root;

			Int32 count = 0;//Counting the total number of nodes
			foreach(ResourceDirectory dir in root)
			{
				CreateResourceNodeRecursive(result, dir);
				count++;
			}

			//Removing old nodes, if any remain
			if(count != result.Nodes.Count)
				for(Int32 loop = result.Nodes.Count - 1;loop >= 0;loop--)
				{
					Boolean isFound = false;
					foreach(ResourceDirectory dir in root)
						if(dir.Name == result.Nodes[loop].Text)
						{
							isFound = true;
							break;
						}
					if(!isFound)
						result.Nodes[loop].Remove();
				}
		}

		private static IEnumerable<VisualizerType> GetCompatibleVisualizers(ResourceDirectory directory)
		{
			ResourceDirectory parentDirectory = directory.Parent.Parent;
			if(parentDirectory.Name == "TYPELIB")
				yield return VisualizerType.TypeLib;

			foreach(VisualizerType type in GetCompatibleVisualizers(parentDirectory.DirectoryEntry.NameType))
				yield return type;
		}

		private static IEnumerable<VisualizerType> GetCompatibleVisualizers(WinNT.Resource.RESOURCE_DIRECTORY_TYPE type)
		{
			switch(type)
			{
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_ACCELERATOR:
				yield return VisualizerType.ListViewArray;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_DIALOG:
				yield return VisualizerType.Dialog;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_DLGINIT:
				yield return VisualizerType.ListViewArray;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_HTML:
				yield return VisualizerType.WebBrowser;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_MANIFEST:
				yield return VisualizerType.GridView;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_MENU:
				yield return VisualizerType.Menu;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_MESSAGETABLE:
				yield return VisualizerType.ListViewArray;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_STRING:
				yield return VisualizerType.ListViewArray;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_TOOLBAR:
				yield return VisualizerType.ToolBar;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_BITMAP:
				yield return VisualizerType.Bitmap;
				break;
			case WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_VERSION:
				yield return VisualizerType.Version;
				break;
			}
			yield return VisualizerType.BinView;
		}

		private Object ExtractData(VisualizerType type, ResourceDirectory directory)
		{
			var nameType = directory.Parent.Parent.DirectoryEntry.NameType;
			switch(type)
			{
			case VisualizerType.TypeLib:
				return base.FilePath;
			case VisualizerType.BinView:
				return directory.GetData();
			case VisualizerType.Dialog:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_DIALOG)
					return new ResourceDialog(directory).GetDialogTemplate();
				break;
			case VisualizerType.Menu:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_MENU)
					return new ResourceMenu(directory).GetMenuTemplate();
				break;
			case VisualizerType.Bitmap:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_BITMAP)
					return new ResourceBitmap(directory).GetBitmapStream();
				break;
			case VisualizerType.ToolBar:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_TOOLBAR)
					return new ResourceToolBar(directory).GetToolBarTemplate();
				break;
			case VisualizerType.Version:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_VERSION)
					return new ResourceVersion(directory);
				break;
			case VisualizerType.GridView:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_MANIFEST)
				{
					DataSet ds = new DataSet();
					using(Stream stream = new ResourceManifest(directory).GetXmlStream())
						ds.ReadXml(stream);
					return ds;
				}
				break;
			case VisualizerType.ListViewArray:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_STRING)
					return new ResourceString(directory)
						.Select(p => new ResourceString.StringItem() { ID = p.ID, Value = p.Value.Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r"), });
				else if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_ACCELERATOR)
					return new ResourceAccelerator(directory);
				else if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_MESSAGETABLE)
					return new ResourceMessageTable(directory);
				else if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_DLGINIT)
					return new ResourceDialogInit(directory);
				break;
			case VisualizerType.WebBrowser:
				if(nameType == WinNT.Resource.RESOURCE_DIRECTORY_TYPE.RT_HTML)
					return $"res://{this.FilePath}/{directory.Parent.Name}";
				break;
			}
			throw new NotImplementedException($"Visualizer: {type} can't render {nameType} directory");
		}
	}
}