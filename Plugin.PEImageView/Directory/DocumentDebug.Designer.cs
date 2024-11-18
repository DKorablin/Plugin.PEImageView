namespace Plugin.PEImageView.Directory
{
	partial class DocumentDebug
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
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.tabHDebug = new System.Windows.Forms.TabControl();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.tabPDB2 = new System.Windows.Forms.TabPage();
			this.tabPDB7 = new System.Windows.Forms.TabPage();
			this.lvMisc = new Plugin.PEImageView.Controls.ReflectionListView();
			this.lvPdb2 = new Plugin.PEImageView.Controls.ReflectionListView();
			this.lvPdb7 = new Plugin.PEImageView.Controls.ReflectionListView();
			this.lvInfo = new Plugin.PEImageView.Controls.ReflectionArrayListView();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			this.tabHDebug.SuspendLayout();
			this.tabMisc.SuspendLayout();
			this.tabPDB2.SuspendLayout();
			this.tabPDB7.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitMain
			// 
			this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitMain.Location = new System.Drawing.Point(0, 0);
			this.splitMain.Name = "splitMain";
			this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(this.lvInfo);
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.tabHDebug);
			this.splitMain.Size = new System.Drawing.Size(269, 169);
			this.splitMain.SplitterDistance = 89;
			this.splitMain.TabIndex = 0;
			// 
			// tabHDebug
			// 
			this.tabHDebug.Controls.Add(this.tabMisc);
			this.tabHDebug.Controls.Add(this.tabPDB2);
			this.tabHDebug.Controls.Add(this.tabPDB7);
			this.tabHDebug.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabHDebug.Location = new System.Drawing.Point(0, 0);
			this.tabHDebug.Name = "tabHDebug";
			this.tabHDebug.SelectedIndex = 0;
			this.tabHDebug.Size = new System.Drawing.Size(269, 76);
			this.tabHDebug.TabIndex = 0;
			// 
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.lvMisc);
			this.tabMisc.Location = new System.Drawing.Point(4, 22);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(261, 50);
			this.tabMisc.TabIndex = 0;
			this.tabMisc.Text = "Misc";
			this.tabMisc.UseVisualStyleBackColor = true;
			// 
			// tabPDB6
			// 
			this.tabPDB2.Controls.Add(this.lvPdb2);
			this.tabPDB2.Location = new System.Drawing.Point(4, 22);
			this.tabPDB2.Name = "tabPDB2";
			this.tabPDB2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPDB2.Size = new System.Drawing.Size(261, 50);
			this.tabPDB2.TabIndex = 1;
			this.tabPDB2.Text = "PDB2";
			this.tabPDB2.UseVisualStyleBackColor = true;
			// 
			// tabPDB7
			// 
			this.tabPDB7.Controls.Add(this.lvPdb7);
			this.tabPDB7.Location = new System.Drawing.Point(4, 22);
			this.tabPDB7.Name = "tabPDB7";
			this.tabPDB7.Padding = new System.Windows.Forms.Padding(3);
			this.tabPDB7.Size = new System.Drawing.Size(261, 50);
			this.tabPDB7.TabIndex = 1;
			this.tabPDB7.Text = "PDB7";
			this.tabPDB7.UseVisualStyleBackColor = true;
			// 
			// lvMisc
			// 
			this.lvMisc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMisc.Location = new System.Drawing.Point(3, 3);
			this.lvMisc.Name = "lvMisc";
			this.lvMisc.Size = new System.Drawing.Size(255, 44);
			this.lvMisc.TabIndex = 0;
			// 
			// lvPdb6
			// 
			this.lvPdb2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPdb2.Location = new System.Drawing.Point(3, 3);
			this.lvPdb2.Name = "lvPdb2";
			this.lvPdb2.Size = new System.Drawing.Size(255, 44);
			this.lvPdb2.TabIndex = 0;
			// 
			// lvPdb7
			// 
			this.lvPdb7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPdb7.Location = new System.Drawing.Point(3, 3);
			this.lvPdb7.Name = "lvPdb7";
			this.lvPdb7.Size = new System.Drawing.Size(255, 44);
			this.lvPdb7.TabIndex = 0;
			// 
			// lvInfo
			// 
			this.lvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvInfo.Location = new System.Drawing.Point(0, 0);
			this.lvInfo.Name = "lvInfo";
			this.lvInfo.Size = new System.Drawing.Size(269, 89);
			this.lvInfo.TabIndex = 0;
			// 
			// DirectoryDebug
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitMain);
			this.Name = "DirectoryDebug";
			this.Size = new System.Drawing.Size(269, 169);
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			this.splitMain.ResumeLayout(false);
			this.tabHDebug.ResumeLayout(false);
			this.tabMisc.ResumeLayout(false);
			this.tabPDB2.ResumeLayout(false);
			this.tabPDB7.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.TabControl tabHDebug;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.TabPage tabPDB2;
		private System.Windows.Forms.TabPage tabPDB7;
		private Plugin.PEImageView.Controls.ReflectionListView lvMisc;
		private Plugin.PEImageView.Controls.ReflectionListView lvPdb2;
		private Plugin.PEImageView.Controls.ReflectionListView lvPdb7;
		private Plugin.PEImageView.Controls.ReflectionArrayListView lvInfo;
	}
}
