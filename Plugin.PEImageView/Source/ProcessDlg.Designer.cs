namespace Plugin.PEImageView.Source
{
	partial class ProcessDlg
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
			System.Windows.Forms.Button bnCacncel;
			System.Windows.Forms.SplitContainer splitMain;
			this.tvProcess = new System.Windows.Forms.TreeView();
			this.lvInfo = new Plugin.PEImageView.Controls.ReflectionListView();
			this.bnOk = new System.Windows.Forms.Button();
			this.processTimer = new System.Windows.Forms.Timer(this.components);
			bnCacncel = new System.Windows.Forms.Button();
			splitMain = new System.Windows.Forms.SplitContainer();
			splitMain.Panel1.SuspendLayout();
			splitMain.Panel2.SuspendLayout();
			splitMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// bnCacncel
			// 
			bnCacncel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			bnCacncel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			bnCacncel.Location = new System.Drawing.Point(197, 327);
			bnCacncel.Name = "bnCacncel";
			bnCacncel.Size = new System.Drawing.Size(75, 23);
			bnCacncel.TabIndex = 1;
			bnCacncel.Text = "&Cancel";
			bnCacncel.UseVisualStyleBackColor = true;
			// 
			// splitMain
			// 
			splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			splitMain.Location = new System.Drawing.Point(12, 12);
			splitMain.Name = "splitMain";
			splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitMain.Panel1
			// 
			splitMain.Panel1.Controls.Add(this.tvProcess);
			// 
			// splitMain.Panel2
			// 
			splitMain.Panel2.Controls.Add(this.lvInfo);
			splitMain.Size = new System.Drawing.Size(260, 309);
			splitMain.SplitterDistance = 216;
			splitMain.TabIndex = 2;
			// 
			// tvProcess
			// 
			this.tvProcess.CheckBoxes = true;
			this.tvProcess.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvProcess.HideSelection = false;
			this.tvProcess.Location = new System.Drawing.Point(0, 0);
			this.tvProcess.Name = "tvProcess";
			this.tvProcess.Size = new System.Drawing.Size(260, 216);
			this.tvProcess.TabIndex = 0;
			this.tvProcess.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvProcess_BeforeExpand);
			this.tvProcess.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProcess_AfterSelect);
			this.tvProcess.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvProcess_BeforeCheck);
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
			this.lvInfo.Size = new System.Drawing.Size(260, 89);
			this.lvInfo.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvInfo.TabIndex = 0;
			this.lvInfo.UseCompatibleStateImageBehavior = false;
			this.lvInfo.View = System.Windows.Forms.View.Details;
			// 
			// bnOk
			// 
			this.bnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bnOk.Enabled = false;
			this.bnOk.Location = new System.Drawing.Point(116, 327);
			this.bnOk.Name = "bnOk";
			this.bnOk.Size = new System.Drawing.Size(75, 23);
			this.bnOk.TabIndex = 0;
			this.bnOk.Text = "&OK";
			this.bnOk.UseVisualStyleBackColor = true;
			// 
			// processTimer
			// 
			this.processTimer.Enabled = true;
			this.processTimer.Interval = 1000;
			this.processTimer.Tick += new System.EventHandler(this.processTimer_Tick);
			// 
			// ProcessDlg
			// 
			this.AcceptButton = this.bnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = bnCacncel;
			this.ClientSize = new System.Drawing.Size(284, 362);
			this.Controls.Add(splitMain);
			this.Controls.Add(bnCacncel);
			this.Controls.Add(this.bnOk);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(550, 900);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(196, 146);
			this.Name = "ProcessDlg";
			this.Text = "Processes";
			splitMain.Panel1.ResumeLayout(false);
			splitMain.Panel2.ResumeLayout(false);
			splitMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bnOk;
		private System.Windows.Forms.TreeView tvProcess;
		private System.Windows.Forms.Timer processTimer;
		private Plugin.PEImageView.Controls.ReflectionListView lvInfo;
	}
}