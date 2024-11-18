namespace Plugin.PEImageView.Directory
{
	partial class DocumentCorResources
	{
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>Clean up any resources being used.</summary>
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
			System.Windows.Forms.TabControl tabHeader;
			System.Windows.Forms.TabPage tabPageHeader;
			System.Windows.Forms.TabPage tabPageRuntimeHeader;
			this.lvHeader = new Plugin.PEImageView.Controls.ReflectionListView();
			this.lvRuntimeHeader = new Plugin.PEImageView.Controls.ReflectionListView();
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.splitHeaders = new System.Windows.Forms.SplitContainer();
			this.tvResource = new System.Windows.Forms.TreeView();
			this.splitResource = new System.Windows.Forms.SplitContainer();
			this.lvResource = new Plugin.PEImageView.Controls.ReflectionArrayListView();
			this.tsResourceType = new System.Windows.Forms.ToolStrip();
			this.bvBytes = new System.ComponentModel.Design.ByteViewer();
			tabHeader = new System.Windows.Forms.TabControl();
			tabPageHeader = new System.Windows.Forms.TabPage();
			tabPageRuntimeHeader = new System.Windows.Forms.TabPage();
			tabHeader.SuspendLayout();
			tabPageHeader.SuspendLayout();
			tabPageRuntimeHeader.SuspendLayout();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			this.splitHeaders.Panel1.SuspendLayout();
			this.splitHeaders.Panel2.SuspendLayout();
			this.splitHeaders.SuspendLayout();
			this.splitResource.Panel1.SuspendLayout();
			this.splitResource.Panel2.SuspendLayout();
			this.splitResource.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabHeader
			// 
			tabHeader.Controls.Add(tabPageHeader);
			tabHeader.Controls.Add(tabPageRuntimeHeader);
			tabHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			tabHeader.Location = new System.Drawing.Point(0, 0);
			tabHeader.Name = "tabHeader";
			tabHeader.SelectedIndex = 0;
			tabHeader.Size = new System.Drawing.Size(104, 101);
			tabHeader.TabIndex = 0;
			// 
			// tabPageHeader
			// 
			tabPageHeader.Controls.Add(this.lvHeader);
			tabPageHeader.Location = new System.Drawing.Point(4, 22);
			tabPageHeader.Name = "tabPageHeader";
			tabPageHeader.Padding = new System.Windows.Forms.Padding(3);
			tabPageHeader.Size = new System.Drawing.Size(96, 75);
			tabPageHeader.TabIndex = 0;
			tabPageHeader.Text = "Header";
			tabPageHeader.UseVisualStyleBackColor = true;
			// 
			// lvHeader
			// 
			this.lvHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvHeader.FullRowSelect = true;
			this.lvHeader.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvHeader.HideSelection = false;
			this.lvHeader.Location = new System.Drawing.Point(3, 3);
			this.lvHeader.MultiSelect = false;
			this.lvHeader.Name = "lvHeader";
			this.lvHeader.Plugin = null;
			this.lvHeader.Size = new System.Drawing.Size(90, 69);
			this.lvHeader.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvHeader.TabIndex = 0;
			this.lvHeader.UseCompatibleStateImageBehavior = false;
			this.lvHeader.View = System.Windows.Forms.View.Details;
			// 
			// tabPageRuntimeHeader
			// 
			tabPageRuntimeHeader.Controls.Add(this.lvRuntimeHeader);
			tabPageRuntimeHeader.Location = new System.Drawing.Point(4, 22);
			tabPageRuntimeHeader.Name = "tabPageRuntimeHeader";
			tabPageRuntimeHeader.Padding = new System.Windows.Forms.Padding(3);
			tabPageRuntimeHeader.Size = new System.Drawing.Size(96, 75);
			tabPageRuntimeHeader.TabIndex = 1;
			tabPageRuntimeHeader.Text = "Runtime Header";
			tabPageRuntimeHeader.UseVisualStyleBackColor = true;
			// 
			// lvRuntimeHeader
			// 
			this.lvRuntimeHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRuntimeHeader.FullRowSelect = true;
			this.lvRuntimeHeader.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvRuntimeHeader.HideSelection = false;
			this.lvRuntimeHeader.Location = new System.Drawing.Point(3, 3);
			this.lvRuntimeHeader.Name = "lvRuntimeHeader";
			this.lvRuntimeHeader.Plugin = null;
			this.lvRuntimeHeader.Size = new System.Drawing.Size(90, 69);
			this.lvRuntimeHeader.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvRuntimeHeader.TabIndex = 0;
			this.lvRuntimeHeader.UseCompatibleStateImageBehavior = false;
			this.lvRuntimeHeader.View = System.Windows.Forms.View.Details;
			// 
			// splitMain
			// 
			this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitMain.Location = new System.Drawing.Point(0, 0);
			this.splitMain.Name = "splitMain";
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(this.splitHeaders);
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.splitResource);
			this.splitMain.Size = new System.Drawing.Size(312, 209);
			this.splitMain.SplitterDistance = 104;
			this.splitMain.TabIndex = 0;
			// 
			// splitHeaders
			// 
			this.splitHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitHeaders.Location = new System.Drawing.Point(0, 0);
			this.splitHeaders.Name = "splitHeaders";
			this.splitHeaders.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitHeaders.Panel1
			// 
			this.splitHeaders.Panel1.Controls.Add(this.tvResource);
			// 
			// splitHeaders.Panel2
			// 
			this.splitHeaders.Panel2.Controls.Add(tabHeader);
			this.splitHeaders.Size = new System.Drawing.Size(104, 209);
			this.splitHeaders.SplitterDistance = 104;
			this.splitHeaders.TabIndex = 1;
			// 
			// tvResource
			// 
			this.tvResource.AllowDrop = true;
			this.tvResource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvResource.HideSelection = false;
			this.tvResource.Location = new System.Drawing.Point(0, 0);
			this.tvResource.Name = "tvResource";
			this.tvResource.ShowRootLines = false;
			this.tvResource.Size = new System.Drawing.Size(104, 104);
			this.tvResource.TabIndex = 0;
			this.tvResource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvResource_AfterSelect);
			this.tvResource.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvResource_DragDrop);
			this.tvResource.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvResource_DragEnter);
			// 
			// splitResource
			// 
			this.splitResource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitResource.Location = new System.Drawing.Point(0, 0);
			this.splitResource.Name = "splitResource";
			this.splitResource.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitResource.Panel1
			// 
			this.splitResource.Panel1.Controls.Add(this.lvResource);
			this.splitResource.Panel1.Controls.Add(this.tsResourceType);
			// 
			// splitResource.Panel2
			// 
			this.splitResource.Panel2.Controls.Add(this.bvBytes);
			this.splitResource.Panel2Collapsed = true;
			this.splitResource.Size = new System.Drawing.Size(204, 209);
			this.splitResource.SplitterDistance = 104;
			this.splitResource.TabIndex = 2;
			// 
			// lvResource
			// 
			this.lvResource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvResource.Location = new System.Drawing.Point(0, 25);
			this.lvResource.MultiSelect = false;
			this.lvResource.Name = "lvResource";
			this.lvResource.Plugin = null;
			this.lvResource.Size = new System.Drawing.Size(204, 184);
			this.lvResource.TabIndex = 1;
			this.lvResource.UseCompatibleStateImageBehavior = false;
			this.lvResource.SelectedIndexChanged += new System.EventHandler(lvResource_SelectedIndexChanged);
			// 
			// tsResourceType
			// 
			this.tsResourceType.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsResourceType.Location = new System.Drawing.Point(0, 0);
			this.tsResourceType.Name = "tsResourceType";
			this.tsResourceType.Size = new System.Drawing.Size(204, 25);
			this.tsResourceType.TabIndex = 0;
			// 
			// bvBytes
			// 
			this.bvBytes.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
			this.bvBytes.ColumnCount = 1;
			this.bvBytes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.bvBytes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bvBytes.Location = new System.Drawing.Point(0, 0);
			this.bvBytes.Name = "bvBytes";
			this.bvBytes.RowCount = 1;
			this.bvBytes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.bvBytes.Size = new System.Drawing.Size(204, 101);
			this.bvBytes.TabIndex = 0;
			// 
			// DocumentCorResources
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitMain);
			this.Name = "DocumentCorResources";
			this.Size = new System.Drawing.Size(312, 209);
			tabHeader.ResumeLayout(false);
			tabPageHeader.ResumeLayout(false);
			tabPageRuntimeHeader.ResumeLayout(false);
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			this.splitMain.ResumeLayout(false);
			this.splitHeaders.Panel1.ResumeLayout(false);
			this.splitHeaders.Panel2.ResumeLayout(false);
			this.splitHeaders.ResumeLayout(false);
			this.splitResource.Panel1.ResumeLayout(false);
			this.splitResource.Panel1.PerformLayout();
			this.splitResource.Panel2.ResumeLayout(false);
			this.splitResource.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitHeaders;
		private Plugin.PEImageView.Controls.ReflectionListView lvHeader;
		private Plugin.PEImageView.Controls.ReflectionListView lvRuntimeHeader;
		private System.Windows.Forms.TreeView tvResource;
		private System.Windows.Forms.SplitContainer splitMain;
		private Plugin.PEImageView.Controls.ReflectionArrayListView lvResource;
		private System.Windows.Forms.ToolStrip tsResourceType;
		private System.Windows.Forms.SplitContainer splitResource;
		private System.ComponentModel.Design.ByteViewer bvBytes;
	}
}
