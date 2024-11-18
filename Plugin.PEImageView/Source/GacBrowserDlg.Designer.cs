namespace Plugin.PEImageView.Source
{
	partial class GacBrowserDlg
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ColumnHeader colPath;
			this.tabGac = new System.Windows.Forms.TabPage();
			this.gridSearch = new AlphaOmega.Windows.Forms.SearchGrid();
			this.lvGac = new System.Windows.Forms.ListView();
			this.bnCancel = new System.Windows.Forms.Button();
			this.bnOk = new System.Windows.Forms.Button();
			this.bgGac = new System.ComponentModel.BackgroundWorker();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabBrowse = new System.Windows.Forms.TabPage();
			this.lvBrowse = new System.Windows.Forms.ListView();
			this.cmsBrowse = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiBrowse = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemove = new System.Windows.Forms.ToolStripMenuItem();
			colPath = new System.Windows.Forms.ColumnHeader();
			this.tabGac.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabBrowse.SuspendLayout();
			this.cmsBrowse.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabGac
			// 
			this.tabGac.Controls.Add(this.gridSearch);
			this.tabGac.Controls.Add(this.lvGac);
			this.tabGac.Location = new System.Drawing.Point(4, 22);
			this.tabGac.Name = "tabGac";
			this.tabGac.Padding = new System.Windows.Forms.Padding(3);
			this.tabGac.Size = new System.Drawing.Size(440, 187);
			this.tabGac.TabIndex = 0;
			this.tabGac.Text = "GAC";
			this.tabGac.UseVisualStyleBackColor = true;
			// 
			// gridSearch
			// 
			this.gridSearch.DataGrid = null;
			this.gridSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.gridSearch.EnableSearchHilight = false;
			this.gridSearch.ListView = null;
			this.gridSearch.Location = new System.Drawing.Point(3, 155);
			this.gridSearch.Name = "gridSearch";
			this.gridSearch.Size = new System.Drawing.Size(440, 29);
			this.gridSearch.TabIndex = 1;
			this.gridSearch.Visible = false;
			// 
			// lvGac
			// 
			this.lvGac.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvGac.FullRowSelect = true;
			this.lvGac.HideSelection = false;
			this.lvGac.Location = new System.Drawing.Point(3, 3);
			this.lvGac.Name = "lvGac";
			this.lvGac.Size = new System.Drawing.Size(434, 151);
			this.lvGac.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvGac.TabIndex = 0;
			this.lvGac.UseCompatibleStateImageBehavior = false;
			this.lvGac.View = System.Windows.Forms.View.Details;
			// 
			// bnCancel
			// 
			this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(385, 231);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(75, 23);
			this.bnCancel.TabIndex = 2;
			this.bnCancel.Text = "&Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			// 
			// bnOk
			// 
			this.bnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bnOk.Location = new System.Drawing.Point(304, 231);
			this.bnOk.Name = "bnOk";
			this.bnOk.Size = new System.Drawing.Size(75, 23);
			this.bnOk.TabIndex = 1;
			this.bnOk.Text = "&OK";
			this.bnOk.UseVisualStyleBackColor = true;
			// 
			// bgGac
			// 
			this.bgGac.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgGac_DoWork);
			this.bgGac.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgGac_RunWorkerCompleted);
			// 
			// tabMain
			// 
			this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabMain.Controls.Add(this.tabGac);
			this.tabMain.Controls.Add(this.tabBrowse);
			this.tabMain.Location = new System.Drawing.Point(12, 12);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(448, 213);
			this.tabMain.TabIndex = 0;
			this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
			// 
			// tabBrowse
			// 
			this.tabBrowse.Controls.Add(this.lvBrowse);
			this.tabBrowse.Location = new System.Drawing.Point(4, 22);
			this.tabBrowse.Name = "tabBrowse";
			this.tabBrowse.Size = new System.Drawing.Size(440, 187);
			this.tabBrowse.TabIndex = 1;
			this.tabBrowse.Text = "Browse";
			this.tabBrowse.UseVisualStyleBackColor = true;
			// 
			// lvBrowse
			// 
			this.lvBrowse.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            colPath});
			this.lvBrowse.ContextMenuStrip = this.cmsBrowse;
			this.lvBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvBrowse.FullRowSelect = true;
			this.lvBrowse.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvBrowse.Location = new System.Drawing.Point(0, 0);
			this.lvBrowse.Name = "lvBrowse";
			this.lvBrowse.Size = new System.Drawing.Size(440, 187);
			this.lvBrowse.TabIndex = 0;
			this.lvBrowse.UseCompatibleStateImageBehavior = false;
			this.lvBrowse.View = System.Windows.Forms.View.Details;
			// 
			// cmsBrowse
			// 
			this.cmsBrowse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBrowse,
            this.tsmiRemove});
			this.cmsBrowse.Name = "cmsBrowse";
			this.cmsBrowse.Size = new System.Drawing.Size(173, 70);
			this.cmsBrowse.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsBrowse_ItemClicked);
			this.cmsBrowse.Opening += new System.ComponentModel.CancelEventHandler(this.cmsBrowse_Opening);
			// 
			// tsmiBrowse
			// 
			this.tsmiBrowse.Name = "tsmiBrowse";
			this.tsmiBrowse.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tsmiBrowse.Size = new System.Drawing.Size(172, 22);
			this.tsmiBrowse.Text = "Br&owse...";
			// 
			// tsmiRemove
			// 
			this.tsmiRemove.Name = "tsmiRemove";
			this.tsmiRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiRemove.Size = new System.Drawing.Size(172, 22);
			this.tsmiRemove.Text = "Remove";
			// 
			// GacBrowserDlg
			// 
			this.AcceptButton = this.bnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(472, 266);
			this.Controls.Add(this.bnOk);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.tabMain);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(195, 144);
			this.Name = "GacBrowserDlg";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Assembly";
			this.tabGac.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabBrowse.ResumeLayout(false);
			this.cmsBrowse.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvGac;
		private System.Windows.Forms.TabControl tabMain;
		private AlphaOmega.Windows.Forms.SearchGrid gridSearch;
		private System.ComponentModel.BackgroundWorker bgGac;
		private System.Windows.Forms.TabPage tabGac;
		private System.Windows.Forms.Button bnCancel;
		private System.Windows.Forms.Button bnOk;
		private System.Windows.Forms.TabPage tabBrowse;
		private System.Windows.Forms.ContextMenuStrip cmsBrowse;
		private System.Windows.Forms.ListView lvBrowse;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemove;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowse;
	}
}