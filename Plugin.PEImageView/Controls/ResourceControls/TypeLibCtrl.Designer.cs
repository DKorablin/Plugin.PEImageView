﻿namespace Plugin.PEImageView.Controls.ResourceControls
{
	partial class TypeLibCtrl
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
			this.tvReflection = new AlphaOmega.Windows.Forms.AssemblyTreeView();
			this.SuspendLayout();
			// 
			// tvReflection
			// 
			this.tvReflection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvReflection.HideSelection = false;
			this.tvReflection.Location = new System.Drawing.Point(0, 0);
			this.tvReflection.Name = "tvReflection";
			this.tvReflection.Size = new System.Drawing.Size(150, 150);
			this.tvReflection.TabIndex = 0;
			this.tvReflection.ShowNonPublicMembers = false;
			// 
			// TypeLibCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tvReflection);
			this.Name = "TypeLibCtrl";
			this.ResumeLayout(false);

		}

		#endregion

		private AlphaOmega.Windows.Forms.AssemblyTreeView tvReflection;
	}
}