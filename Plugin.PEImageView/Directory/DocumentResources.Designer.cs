namespace Plugin.PEImageView.Directory
{
	partial class DocumentResources
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.SplitContainer splitMain;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentResources));
			System.Windows.Forms.ToolStripSeparator sepDataType;
			this.splitResourceTree = new System.Windows.Forms.SplitContainer();
			this.tvResource = new System.Windows.Forms.TreeView();
			this.ilResources = new System.Windows.Forms.ImageList(this.components);
			this.lvInfo = new Plugin.PEImageView.Controls.ReflectionListView();
			this.pnlResources = new Plugin.PEImageView.Controls.ResourceControls.ResourceCtrlHost();
			this.tsResourceAction = new System.Windows.Forms.ToolStrip();
			this.tsddlAlternateType = new System.Windows.Forms.ToolStripComboBox();
			this.tsddlSelectableData = new System.Windows.Forms.ToolStripComboBox();
			this.tsbnSave = new System.Windows.Forms.ToolStripButton();
			splitMain = new System.Windows.Forms.SplitContainer();
			sepDataType = new System.Windows.Forms.ToolStripSeparator();
			splitMain.Panel1.SuspendLayout();
			splitMain.Panel2.SuspendLayout();
			splitMain.SuspendLayout();
			this.splitResourceTree.Panel1.SuspendLayout();
			this.splitResourceTree.Panel2.SuspendLayout();
			this.splitResourceTree.SuspendLayout();
			this.tsResourceAction.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitMain
			// 
			splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
			splitMain.Location = new System.Drawing.Point(0, 0);
			splitMain.Name = "splitMain";
			// 
			// splitMain.Panel1
			// 
			splitMain.Panel1.Controls.Add(this.splitResourceTree);
			// 
			// splitMain.Panel2
			// 
			splitMain.Panel2.Controls.Add(this.pnlResources);
			splitMain.Panel2.Controls.Add(this.tsResourceAction);
			splitMain.Size = new System.Drawing.Size(312, 209);
			splitMain.SplitterDistance = 104;
			splitMain.TabIndex = 0;
			// 
			// splitResourceTree
			// 
			this.splitResourceTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitResourceTree.Location = new System.Drawing.Point(0, 0);
			this.splitResourceTree.Name = "splitResourceTree";
			this.splitResourceTree.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitResourceTree.Panel1
			// 
			this.splitResourceTree.Panel1.Controls.Add(this.tvResource);
			// 
			// splitResourceTree.Panel2
			// 
			this.splitResourceTree.Panel2.Controls.Add(this.lvInfo);
			this.splitResourceTree.Panel2Collapsed = true;
			this.splitResourceTree.Size = new System.Drawing.Size(104, 209);
			this.splitResourceTree.SplitterDistance = 97;
			this.splitResourceTree.TabIndex = 0;
			// 
			// tvResource
			// 
			this.tvResource.AllowDrop = true;
			this.tvResource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvResource.HideSelection = false;
			this.tvResource.ImageIndex = 0;
			this.tvResource.ImageList = this.ilResources;
			this.tvResource.Location = new System.Drawing.Point(0, 0);
			this.tvResource.Name = "tvResource";
			this.tvResource.SelectedImageIndex = 0;
			this.tvResource.Size = new System.Drawing.Size(104, 209);
			this.tvResource.TabIndex = 0;
			this.tvResource.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvResource_AfterCollapse);
			this.tvResource.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvResource_AfterExpand);
			this.tvResource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvResource_AfterSelect);
			this.tvResource.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvResource_DragDrop);
			this.tvResource.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvResource_DragEnter);
			// 
			// ilResources
			// 
			this.ilResources.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilResources.ImageStream")));
			this.ilResources.TransparentColor = System.Drawing.Color.Magenta;
			this.ilResources.Images.SetKeyName(0, "t.Folder.Closed.bmp");
			this.ilResources.Images.SetKeyName(1, "t.Folder.Opened.bmp");
			this.ilResources.Images.SetKeyName(2, "t.Binary.bmp");
			this.ilResources.Images.SetKeyName(3, "t.Accelerator.bmp");
			this.ilResources.Images.SetKeyName(4, "t.Bitmap.bmp");
			this.ilResources.Images.SetKeyName(5, "t.Dialog.bmp");
			this.ilResources.Images.SetKeyName(6, "t.Html.bmp");
			this.ilResources.Images.SetKeyName(7, "t.Icon.bmp");
			this.ilResources.Images.SetKeyName(8, "t.Menu.bmp");
			this.ilResources.Images.SetKeyName(9, "t.StringTable.bmp");
			this.ilResources.Images.SetKeyName(10, "t.ToolBar.bmp");
			this.ilResources.Images.SetKeyName(11, "t.Version.bmp");
			// 
			// lvInfo
			// 
			this.lvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvInfo.FullRowSelect = true;
			this.lvInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvInfo.HideSelection = false;
			this.lvInfo.Location = new System.Drawing.Point(0, 0);
			this.lvInfo.Name = "lvInfo";
			this.lvInfo.Plugin = null;
			this.lvInfo.Size = new System.Drawing.Size(150, 46);
			this.lvInfo.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvInfo.TabIndex = 0;
			this.lvInfo.UseCompatibleStateImageBehavior = false;
			this.lvInfo.View = System.Windows.Forms.View.Details;
			// 
			// pnlResources
			// 
			this.pnlResources.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlResources.Location = new System.Drawing.Point(0, 27);
			this.pnlResources.Name = "pnlResources";
			this.pnlResources.Plugin = null;
			this.pnlResources.Size = new System.Drawing.Size(204, 182);
			this.pnlResources.TabIndex = 6;
			// 
			// tsResourceAction
			// 
			this.tsResourceAction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsResourceAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddlAlternateType,
            sepDataType,
            this.tsddlSelectableData,
            this.tsbnSave});
			this.tsResourceAction.Location = new System.Drawing.Point(0, 0);
			this.tsResourceAction.Name = "tsResourceAction";
			this.tsResourceAction.Size = new System.Drawing.Size(204, 27);
			this.tsResourceAction.TabIndex = 5;
			this.tsResourceAction.Text = "tsResourceAction";
			this.tsResourceAction.Visible = false;
			// 
			// tsddlAlternateType
			// 
			this.tsddlAlternateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsddlAlternateType.Name = "tsddlAlternateType";
			this.tsddlAlternateType.Size = new System.Drawing.Size(121, 27);
			this.tsddlAlternateType.Visible = false;
			this.tsddlAlternateType.SelectedIndexChanged += new System.EventHandler(this.tsddbAlternateType_SelectedIndexChanged);
			// 
			// sepDataType
			// 
			sepDataType.Name = "sepDataType";
			sepDataType.Size = new System.Drawing.Size(6, 27);
			sepDataType.Visible = false;
			// 
			// tsddlSelectableData
			// 
			this.tsddlSelectableData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsddlSelectableData.Name = "tsddlSelectableData";
			this.tsddlSelectableData.Size = new System.Drawing.Size(121, 23);
			this.tsddlSelectableData.Visible = false;
			this.tsddlSelectableData.SelectedIndexChanged += new System.EventHandler(this.tsddlSelectableData_SelectedIndexChanged);
			// 
			// tsbnSave
			// 
			this.tsbnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnSave.Image = global::Plugin.PEImageView.Properties.Resources.FileSave;
			this.tsbnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnSave.Name = "tsbnSave";
			this.tsbnSave.Size = new System.Drawing.Size(23, 20);
			this.tsbnSave.Text = "Save...";
			this.tsbnSave.Click += new System.EventHandler(this.tsbnSave_Click);
			// 
			// DocumentResources
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(splitMain);
			this.Name = "DocumentResources";
			this.Size = new System.Drawing.Size(312, 209);
			splitMain.Panel1.ResumeLayout(false);
			splitMain.Panel2.ResumeLayout(false);
			splitMain.Panel2.PerformLayout();
			splitMain.ResumeLayout(false);
			this.splitResourceTree.Panel1.ResumeLayout(false);
			this.splitResourceTree.Panel2.ResumeLayout(false);
			this.splitResourceTree.ResumeLayout(false);
			this.tsResourceAction.ResumeLayout(false);
			this.tsResourceAction.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.TreeView tvResource;
		private System.Windows.Forms.SplitContainer splitResourceTree;
		private Plugin.PEImageView.Controls.ReflectionListView lvInfo;
		private System.Windows.Forms.ToolStrip tsResourceAction;
		private Plugin.PEImageView.Controls.ResourceControls.ResourceCtrlHost pnlResources;
		private System.Windows.Forms.ToolStripComboBox tsddlSelectableData;
		private System.Windows.Forms.ToolStripButton tsbnSave;
		private System.Windows.Forms.ToolStripComboBox tsddlAlternateType;
		private System.Windows.Forms.ImageList ilResources;
	}
}
