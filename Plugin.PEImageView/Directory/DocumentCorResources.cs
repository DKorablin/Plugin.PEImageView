using System;
using System.Windows.Forms;
using AlphaOmega.Debug;
using AlphaOmega.Debug.CorDirectory;
using Plugin.PEImageView.Bll;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentCorResources : DocumentBase
	{
		public DocumentCorResources()
			: base(PeHeaderType.DIRECTORY_COR_RESOURCE)
			=> this.InitializeComponent();

		protected override void ShowFile(PEFile info)
		{
			lvHeader.Plugin = base.Plugin;
			lvRuntimeHeader.Plugin = base.Plugin;
			lvResource.Plugin = base.Plugin;

			tvResource.Nodes.Clear();
			ResourceTable resource = info.ComDescriptor.Resources;
			if(resource.Header.IsValid)
			{
				lvHeader.DataBind(resource.Header);
				lvRuntimeHeader.DataBind(resource.RuntimeHeader);
				foreach(var item in resource)
					tvResource.Nodes.Add(new TreeNode(item.Name) { Tag = item, });
			} else
				tvResource.Nodes.Add(String.Empty).SetException("Invalid magic");
		}

		private void tvResource_DragEnter(Object sender, DragEventArgs e)
			=> e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;

		private void tvResource_DragDrop(Object sender, DragEventArgs e)
		{
			String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
			if(files.Length > 0)
				base.OpenFile(files[0]);
		}

		private void tvResource_AfterSelect(Object sender, TreeViewEventArgs e)
		{
			splitResource.Panel2Collapsed = true;

			if(!(e.Node.Tag is ResourceTableReader row))
				lvResource.Clear();
			else if(row.CanRead)
				lvResource.DataBind(row);
			else
				throw new NotImplementedException("I don't know how to read streaming resources.");
		}

		private void lvResource_SelectedIndexChanged(Object sender, EventArgs e)
		{
			ListViewItem lv = lvResource.SelectedItems.Count == 0 ? null : lvResource.SelectedItems[0];
			ResourceTableItem item = lv == null ? null : (ResourceTableItem)lv.Tag;

			if(item == null)
				bvBytes.SetBytes(new Byte[] { });
			else
			{
				bvBytes.SetBytes(item.Data);
				splitResource.Panel2Collapsed = false;
			}
		}
	}
}