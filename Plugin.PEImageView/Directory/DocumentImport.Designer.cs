namespace Plugin.PEImageView.Directory
{
	partial class DocumentImport
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
			System.Windows.Forms.ColumnHeader colDLLName;
			this.splitToc = new System.Windows.Forms.SplitContainer();
			this.splitDLL = new System.Windows.Forms.SplitContainer();
			this.lvDll = new System.Windows.Forms.ListView();
			this.lvDescriptor = new Plugin.PEImageView.Controls.ReflectionListView();
			this.lvImports = new AlphaOmega.Windows.Forms.SortableListView();
			this.colHint = new System.Windows.Forms.ColumnHeader();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colByOrdinal = new System.Windows.Forms.ColumnHeader();
			colDLLName = new System.Windows.Forms.ColumnHeader();
			this.splitToc.Panel1.SuspendLayout();
			this.splitToc.Panel2.SuspendLayout();
			this.splitToc.SuspendLayout();
			this.splitDLL.Panel1.SuspendLayout();
			this.splitDLL.Panel2.SuspendLayout();
			this.splitDLL.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitToc
			// 
			this.splitToc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitToc.Location = new System.Drawing.Point(0, 0);
			this.splitToc.Name = "splitToc";
			// 
			// splitToc.Panel1
			// 
			this.splitToc.Panel1.Controls.Add(this.splitDLL);
			// 
			// splitToc.Panel2
			// 
			this.splitToc.Panel2.Controls.Add(this.lvImports);
			this.splitToc.Size = new System.Drawing.Size(260, 150);
			this.splitToc.SplitterDistance = 75;
			this.splitToc.TabIndex = 1;
			// 
			// splitDLL
			// 
			this.splitDLL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitDLL.Location = new System.Drawing.Point(0, 0);
			this.splitDLL.Name = "splitDLL";
			this.splitDLL.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitDLL.Panel1
			// 
			this.splitDLL.Panel1.Controls.Add(this.lvDll);
			// 
			// splitDLL.Panel2
			// 
			this.splitDLL.Panel2.Controls.Add(this.lvDescriptor);
			this.splitDLL.Size = new System.Drawing.Size(75, 150);
			this.splitDLL.SplitterDistance = 92;
			this.splitDLL.TabIndex = 2;
			// 
			// lvDll
			// 
			this.lvDll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            colDLLName});
			this.lvDll.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvDll.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvDll.HideSelection = false;
			this.lvDll.Location = new System.Drawing.Point(0, 0);
			this.lvDll.MultiSelect = false;
			this.lvDll.AllowDrop = true;
			this.lvDll.Name = "lvDll";
			this.lvDll.Size = new System.Drawing.Size(75, 92);
			this.lvDll.TabIndex = 0;
			this.lvDll.UseCompatibleStateImageBehavior = false;
			this.lvDll.View = System.Windows.Forms.View.Details;
			this.lvDll.SelectedIndexChanged += new System.EventHandler(this.lvDll_SelectedIndexChanged);
			this.lvDll.DoubleClick += new System.EventHandler(this.lvDll_DoubleClick);
			this.lvDll.DragEnter+=new System.Windows.Forms.DragEventHandler(lvDll_DragEnter);
			this.lvDll.DragDrop += new System.Windows.Forms.DragEventHandler(lvDll_DragDrop);
			this.lvDll.ContextMenuStrip = new AlphaOmega.Windows.Forms.ContextMenuStripCopy();
			// 
			// colDLLName
			// 
			colDLLName.Text = "Name";
			// 
			// lvDescriptor
			// 
			this.lvDescriptor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvDescriptor.FullRowSelect = true;
			this.lvDescriptor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvDescriptor.HideSelection = false;
			this.lvDescriptor.Location = new System.Drawing.Point(0, 0);
			this.lvDescriptor.Name = "lvDescriptor";
			this.lvDescriptor.Plugin = null;
			this.lvDescriptor.Size = new System.Drawing.Size(75, 54);
			this.lvDescriptor.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvDescriptor.TabIndex = 1;
			this.lvDescriptor.UseCompatibleStateImageBehavior = false;
			this.lvDescriptor.View = System.Windows.Forms.View.Details;
			// 
			// lvImports
			// 
			this.lvImports.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHint,
            this.colName,
            this.colByOrdinal});
			this.lvImports.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvImports.FullRowSelect = true;
			this.lvImports.HideSelection = false;
			this.lvImports.Location = new System.Drawing.Point(0, 0);
			this.lvImports.Name = "lvImports";
			this.lvImports.Size = new System.Drawing.Size(181, 150);
			this.lvImports.TabIndex = 0;
			this.lvImports.UseCompatibleStateImageBehavior = false;
			this.lvImports.View = System.Windows.Forms.View.Details;
			this.lvImports.DoubleClick += new System.EventHandler(this.lvImports_DoubleClick);
			// 
			// colHint
			// 
			this.colHint.Text = "Hint";
			// 
			// colName
			// 
			this.colName.Text = "Name";
			// 
			// colByOrdinal
			// 
			this.colByOrdinal.Text = "ByOrdinal";
			// 
			// DocumentImport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitToc);
			this.Name = "DocumentImport";
			this.Size = new System.Drawing.Size(260, 150);
			this.splitToc.Panel1.ResumeLayout(false);
			this.splitToc.Panel2.ResumeLayout(false);
			this.splitToc.ResumeLayout(false);
			this.splitDLL.Panel1.ResumeLayout(false);
			this.splitDLL.Panel2.ResumeLayout(false);
			this.splitDLL.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private AlphaOmega.Windows.Forms.SortableListView lvImports;
		private Plugin.PEImageView.Controls.ReflectionListView lvDescriptor;
		private System.Windows.Forms.ColumnHeader colHint;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colByOrdinal;
		private System.Windows.Forms.ListView lvDll;
		private System.Windows.Forms.SplitContainer splitDLL;
		private System.Windows.Forms.SplitContainer splitToc;
	}
}
