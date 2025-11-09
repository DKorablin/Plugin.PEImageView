namespace Plugin.PEImageView
{
	partial class PanelTOC
	{
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code
		/// <summary> Clean up any resources being used.</summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStrip tsHead;
			this.tsbnOpen = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenProcess = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenGac = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenBase64 = new System.Windows.Forms.ToolStripMenuItem();
			this.gridSearch = new AlphaOmega.Windows.Forms.SearchGrid();
			this.splitToc = new System.Windows.Forms.SplitContainer();
			this.tvToc = new System.Windows.Forms.TreeView();
			this.cmsToc = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiTocUnload = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTocBinView = new System.Windows.Forms.ToolStripMenuItem();
			this.splitInfo = new System.Windows.Forms.SplitContainer();
			this.lvInfo = new Plugin.PEImageView.Controls.ReflectionListView();
			this.txtTocInfo = new System.Windows.Forms.TextBox();
			this.tsmiTocExplorerView = new System.Windows.Forms.ToolStripMenuItem();
			tsHead = new System.Windows.Forms.ToolStrip();
			tsHead.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitToc)).BeginInit();
			this.splitToc.Panel1.SuspendLayout();
			this.splitToc.Panel2.SuspendLayout();
			this.splitToc.SuspendLayout();
			this.cmsToc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitInfo)).BeginInit();
			this.splitInfo.Panel1.SuspendLayout();
			this.splitInfo.Panel2.SuspendLayout();
			this.splitInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsHead
			// 
			tsHead.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			tsHead.ImageScalingSize = new System.Drawing.Size(20, 20);
			tsHead.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbnOpen});
			tsHead.Location = new System.Drawing.Point(0, 0);
			tsHead.Name = "tsHead";
			tsHead.Size = new System.Drawing.Size(288, 31);
			tsHead.TabIndex = 2;
			// 
			// tsbnOpen
			// 
			this.tsbnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenFile,
            this.tsmiOpenProcess,
            this.tsmiOpenGac,
            this.tsmiOpenBase64});
			this.tsbnOpen.Image = global::Plugin.PEImageView.Properties.Resources.iconOpen;
			this.tsbnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnOpen.Name = "tsbnOpen";
			this.tsbnOpen.Size = new System.Drawing.Size(39, 24);
			this.tsbnOpen.Text = "Open";
			this.tsbnOpen.ButtonClick += new System.EventHandler(this.tsbnOpen_Click);
			this.tsbnOpen.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsbnOpen_DropDownItemClicked);
			// 
			// tsmiOpenFile
			// 
			this.tsmiOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
			this.tsmiOpenFile.Image = global::Plugin.PEImageView.Properties.Resources.iconOpenFile;
			this.tsmiOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiOpenFile.Name = "tsmiOpenFile";
			this.tsmiOpenFile.Size = new System.Drawing.Size(150, 26);
			this.tsmiOpenFile.Text = "&File...";
			// 
			// tsmiOpenProcess
			// 
			this.tsmiOpenProcess.Image = global::Plugin.PEImageView.Properties.Resources.iconOpenProcess;
			this.tsmiOpenProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiOpenProcess.Name = "tsmiOpenProcess";
			this.tsmiOpenProcess.Size = new System.Drawing.Size(150, 26);
			this.tsmiOpenProcess.Text = "&Process...";
			// 
			// tsmiOpenGac
			// 
			this.tsmiOpenGac.Image = global::Plugin.PEImageView.Properties.Resources.iconOpenGAC;
			this.tsmiOpenGac.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiOpenGac.Name = "tsmiOpenGac";
			this.tsmiOpenGac.Size = new System.Drawing.Size(150, 26);
			this.tsmiOpenGac.Text = "&GAC...";
			// 
			// tsmiOpenBase64
			// 
			this.tsmiOpenBase64.Name = "tsmiOpenBase64";
			this.tsmiOpenBase64.Size = new System.Drawing.Size(150, 26);
			this.tsmiOpenBase64.Text = "&Base64...";
			// 
			// gridSearch
			// 
			this.gridSearch.DataGrid = null;
			this.gridSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.gridSearch.EnableFindCase = true;
			this.gridSearch.EnableFindHighlight = true;
			this.gridSearch.EnableFindPrevNext = true;
			this.gridSearch.EnableSearchHighlight = false;
			this.gridSearch.ListView = null;
			this.gridSearch.Location = new System.Drawing.Point(4, 191);
			this.gridSearch.Margin = new System.Windows.Forms.Padding(5);
			this.gridSearch.Name = "gridSearch";
			this.gridSearch.Size = new System.Drawing.Size(587, 36);
			this.gridSearch.TabIndex = 1;
			this.gridSearch.TreeView = null;
			this.gridSearch.Visible = false;
			// 
			// splitToc
			// 
			this.splitToc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitToc.Location = new System.Drawing.Point(0, 31);
			this.splitToc.Margin = new System.Windows.Forms.Padding(4);
			this.splitToc.Name = "splitToc";
			this.splitToc.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitToc.Panel1
			// 
			this.splitToc.Panel1.Controls.Add(this.tvToc);
			this.splitToc.Panel1.Controls.Add(this.gridSearch);
			// 
			// splitToc.Panel2
			// 
			this.splitToc.Panel2.Controls.Add(this.splitInfo);
			this.splitToc.Panel2Collapsed = true;
			this.splitToc.Size = new System.Drawing.Size(288, 317);
			this.splitToc.SplitterDistance = 137;
			this.splitToc.SplitterWidth = 5;
			this.splitToc.TabIndex = 1;
			// 
			// tvToc
			// 
			this.tvToc.AllowDrop = true;
			this.tvToc.ContextMenuStrip = this.cmsToc;
			this.tvToc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvToc.HideSelection = false;
			this.tvToc.Location = new System.Drawing.Point(0, 0);
			this.tvToc.Margin = new System.Windows.Forms.Padding(4);
			this.tvToc.Name = "tvToc";
			this.tvToc.ShowNodeToolTips = true;
			this.tvToc.Size = new System.Drawing.Size(288, 317);
			this.tvToc.TabIndex = 3;
			this.tvToc.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvToc_BeforeExpand);
			this.tvToc.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvToc_AfterSelect);
			this.tvToc.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvToc_DragDrop);
			this.tvToc.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvToc_DragEnter);
			this.tvToc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvToc_KeyDown);
			this.tvToc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvToc_MouseClick);
			this.tvToc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvToc_MouseDoubleClick);
			// 
			// cmsToc
			// 
			this.cmsToc.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmsToc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTocUnload,
            this.tsmiTocBinView,
            this.tsmiTocExplorerView});
			this.cmsToc.Name = "cmsToc";
			this.cmsToc.Size = new System.Drawing.Size(211, 104);
			this.cmsToc.Opening += new System.ComponentModel.CancelEventHandler(this.cmsToc_Opening);
			this.cmsToc.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsToc_ItemClicked);
			// 
			// tsmiTocUnload
			// 
			this.tsmiTocUnload.Name = "tsmiTocUnload";
			this.tsmiTocUnload.Size = new System.Drawing.Size(210, 24);
			this.tsmiTocUnload.Text = "&Unload";
			// 
			// tsmiTocBinView
			// 
			this.tsmiTocBinView.Name = "tsmiTocBinView";
			this.tsmiTocBinView.Size = new System.Drawing.Size(210, 24);
			this.tsmiTocBinView.Text = "&Bin View";
			// 
			// splitInfo
			// 
			this.splitInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitInfo.Location = new System.Drawing.Point(0, 0);
			this.splitInfo.Margin = new System.Windows.Forms.Padding(4);
			this.splitInfo.Name = "splitInfo";
			this.splitInfo.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitInfo.Panel1
			// 
			this.splitInfo.Panel1.Controls.Add(this.lvInfo);
			// 
			// splitInfo.Panel2
			// 
			this.splitInfo.Panel2.Controls.Add(this.txtTocInfo);
			this.splitInfo.Panel2Collapsed = true;
			this.splitInfo.Size = new System.Drawing.Size(150, 46);
			this.splitInfo.SplitterDistance = 25;
			this.splitInfo.SplitterWidth = 5;
			this.splitInfo.TabIndex = 1;
			// 
			// lvInfo
			// 
			this.lvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvInfo.FullRowSelect = true;
			this.lvInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvInfo.HideSelection = false;
			this.lvInfo.Location = new System.Drawing.Point(0, 0);
			this.lvInfo.Margin = new System.Windows.Forms.Padding(4);
			this.lvInfo.Name = "lvInfo";
			this.lvInfo.Plugin = null;
			this.lvInfo.Size = new System.Drawing.Size(200, 57);
			this.lvInfo.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvInfo.TabIndex = 2;
			this.lvInfo.UseCompatibleStateImageBehavior = false;
			this.lvInfo.View = System.Windows.Forms.View.Details;
			// 
			// txtTocInfo
			// 
			this.txtTocInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTocInfo.Location = new System.Drawing.Point(0, 0);
			this.txtTocInfo.Margin = new System.Windows.Forms.Padding(4);
			this.txtTocInfo.Multiline = true;
			this.txtTocInfo.Name = "txtTocInfo";
			this.txtTocInfo.ReadOnly = true;
			this.txtTocInfo.Size = new System.Drawing.Size(150, 46);
			this.txtTocInfo.TabIndex = 0;
			// 
			// tsmiTocExplorerView
			// 
			this.tsmiTocExplorerView.Name = "tsmiTocExplorerView";
			this.tsmiTocExplorerView.Size = new System.Drawing.Size(210, 24);
			this.tsmiTocExplorerView.Text = "Show in &Folder";
			// 
			// PanelTOC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitToc);
			this.Controls.Add(tsHead);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "PanelTOC";
			this.Size = new System.Drawing.Size(288, 348);
			tsHead.ResumeLayout(false);
			tsHead.PerformLayout();
			this.splitToc.Panel1.ResumeLayout(false);
			this.splitToc.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitToc)).EndInit();
			this.splitToc.ResumeLayout(false);
			this.cmsToc.ResumeLayout(false);
			this.splitInfo.Panel1.ResumeLayout(false);
			this.splitInfo.Panel2.ResumeLayout(false);
			this.splitInfo.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitInfo)).EndInit();
			this.splitInfo.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitToc;
		private System.Windows.Forms.TreeView tvToc;
		private System.Windows.Forms.SplitContainer splitInfo;
		private Plugin.PEImageView.Controls.ReflectionListView lvInfo;
		private System.Windows.Forms.TextBox txtTocInfo;
		private System.Windows.Forms.ContextMenuStrip cmsToc;
		private System.Windows.Forms.ToolStripMenuItem tsmiTocUnload;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenProcess;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenGac;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenBase64;
		private System.Windows.Forms.ToolStripSplitButton tsbnOpen;
		private AlphaOmega.Windows.Forms.SearchGrid gridSearch;
		private System.Windows.Forms.ToolStripMenuItem tsmiTocBinView;
		private System.Windows.Forms.ToolStripMenuItem tsmiTocExplorerView;
	}
}