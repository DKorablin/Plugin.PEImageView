namespace Plugin.PEImageView.Directory
{
	partial class DocumentRelocation
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lvDirectory = new System.Windows.Forms.ListView();
			this.lvAddress = new Plugin.PEImageView.Controls.ReflectionArrayListView();
			this.colSize = new System.Windows.Forms.ColumnHeader();
			this.colType = new System.Windows.Forms.ColumnHeader();
			this.colVA = new System.Windows.Forms.ColumnHeader();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lvDirectory);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lvAddress);
			this.splitContainer1.Size = new System.Drawing.Size(150, 150);
			this.splitContainer1.TabIndex = 0;
			// 
			// lvDirectory
			// 
			this.lvDirectory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSize,
            this.colType,
            this.colVA});
			this.lvDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvDirectory.FullRowSelect = true;
			this.lvDirectory.HideSelection = false;
			this.lvDirectory.Location = new System.Drawing.Point(0, 0);
			this.lvDirectory.Name = "lvDirectory";
			this.lvDirectory.Size = new System.Drawing.Size(50, 150);
			this.lvDirectory.TabIndex = 0;
			this.lvDirectory.UseCompatibleStateImageBehavior = false;
			this.lvDirectory.View = System.Windows.Forms.View.Details;
			this.lvDirectory.SelectedIndexChanged += new System.EventHandler(lvDirectory_SelectedIndexChanged);
			this.lvDirectory.ContextMenuStrip = new AlphaOmega.Windows.Forms.ContextMenuStripCopy();
			// 
			// lvAddress
			// 
			this.lvAddress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvAddress.Location = new System.Drawing.Point(0, 0);
			this.lvAddress.Name = "lvAddress";
			this.lvAddress.Size = new System.Drawing.Size(96, 150);
			this.lvAddress.TabIndex = 0;
			this.lvAddress.UseCompatibleStateImageBehavior = false;
			// 
			// colSize
			// 
			this.colSize.Text = "SizeOfBlock";
			// 
			// colType
			// 
			this.colType.Text = "TypeOffset";
			// 
			// colVA
			// 
			this.colVA.Text = "VirtualAddress";
			// 
			// DocumentRelocation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "DocumentRelocation";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListView lvDirectory;
		private Plugin.PEImageView.Controls.ReflectionArrayListView lvAddress;
		private System.Windows.Forms.ColumnHeader colSize;
		private System.Windows.Forms.ColumnHeader colType;
		private System.Windows.Forms.ColumnHeader colVA;
	}
}
