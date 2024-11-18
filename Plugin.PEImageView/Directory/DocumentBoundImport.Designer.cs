namespace Plugin.PEImageView.Directory
{
	partial class DocumentBoundImport
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
			this.lvBoundImport = new Plugin.PEImageView.Controls.ReflectionArrayListView();
			this.SuspendLayout();
			// 
			// lvBoundImport
			// 
			this.lvBoundImport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvBoundImport.Location = new System.Drawing.Point(0, 0);
			this.lvBoundImport.MultiSelect = false;
			this.lvBoundImport.Name = "lvBoundImport";
			this.lvBoundImport.Plugin = null;
			this.lvBoundImport.Size = new System.Drawing.Size(242, 150);
			this.lvBoundImport.TabIndex = 0;
			this.lvBoundImport.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
			this.lvBoundImport.UseCompatibleStateImageBehavior = false;
			this.lvBoundImport.View = System.Windows.Forms.View.Details;
			// 
			// DocumentBoundImport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lvBoundImport);
			this.Name = "DocumentBoundImport";
			this.Size = new System.Drawing.Size(242, 150);
			this.ResumeLayout(false);

		}

		#endregion

		private Plugin.PEImageView.Controls.ReflectionArrayListView lvBoundImport;
	}
}