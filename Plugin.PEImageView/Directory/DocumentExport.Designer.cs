namespace Plugin.PEImageView.Directory
{
	partial class DocumentExport
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
			System.Windows.Forms.SplitContainer splitToc;
			this.lvFunctions = new AlphaOmega.Windows.Forms.SortableListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colAddress = new System.Windows.Forms.ColumnHeader();
			this.colOrdinal = new System.Windows.Forms.ColumnHeader();
			this.lvHeader = new Plugin.PEImageView.Controls.ReflectionListView();
			splitToc = new System.Windows.Forms.SplitContainer();
			splitToc.Panel1.SuspendLayout();
			splitToc.Panel2.SuspendLayout();
			splitToc.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvFunctions
			// 
			this.lvFunctions.AllowColumnReorder = true;
			this.lvFunctions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colAddress,
            this.colOrdinal});
			this.lvFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFunctions.FullRowSelect = true;
			this.lvFunctions.GridLines = true;
			this.lvFunctions.AllowDrop = true;
			this.lvFunctions.Location = new System.Drawing.Point(0, 0);
			this.lvFunctions.MultiSelect = false;
			this.lvFunctions.Name = "lvFunctions";
			this.lvFunctions.Size = new System.Drawing.Size(96, 150);
			this.lvFunctions.TabIndex = 0;
			this.lvFunctions.UseCompatibleStateImageBehavior = false;
			this.lvFunctions.View = System.Windows.Forms.View.Details;
			this.lvFunctions.DragEnter+=new System.Windows.Forms.DragEventHandler(ListView_DragEnter);
			this.lvFunctions.DragDrop+=new System.Windows.Forms.DragEventHandler(ListView_DragDrop);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			// 
			// colAddress
			// 
			this.colAddress.Text = "Address";
			// 
			// colOrdinal
			// 
			this.colOrdinal.Text = "Ordinal";
			// 
			// splitToc
			// 
			splitToc.Dock = System.Windows.Forms.DockStyle.Fill;
			splitToc.Location = new System.Drawing.Point(0, 0);
			splitToc.Name = "splitToc";
			splitToc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			// 
			// splitToc.Panel1
			// 
			splitToc.Panel1.Controls.Add(this.lvHeader);
			// 
			// splitToc.Panel2
			// 
			splitToc.Panel2.Controls.Add(this.lvFunctions);
			splitToc.Size = new System.Drawing.Size(150, 150);
			splitToc.SplitterDistance = 240;
			splitToc.TabIndex = 1;
			// 
			// lvHeader
			// 
			this.lvHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvHeader.Location = new System.Drawing.Point(0, 0);
			this.lvHeader.Name = "lvHeader";
			this.lvHeader.Size = new System.Drawing.Size(50, 150);
			this.lvHeader.TabIndex = 0;
			this.lvHeader.AllowDrop = true;
			this.lvHeader.DragEnter += new System.Windows.Forms.DragEventHandler(ListView_DragEnter);
			this.lvHeader.DragDrop += new System.Windows.Forms.DragEventHandler(ListView_DragDrop);
			// 
			// DocumentExport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(splitToc);
			this.Name = "DocumentExport";
			splitToc.Panel1.ResumeLayout(false);
			splitToc.Panel2.ResumeLayout(false);
			splitToc.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private AlphaOmega.Windows.Forms.SortableListView lvFunctions;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colAddress;
		private System.Windows.Forms.ColumnHeader colOrdinal;
		private Plugin.PEImageView.Controls.ReflectionListView lvHeader;
	}
}
