namespace EzIna
{
	partial class FrmInforSetupInitialProcess_SXGA
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
			if (disposing && (components != null))
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
						this.ucTabControlX1 = new EzIna.GUI.UserControls.ucTabControlX();
						this.SuspendLayout();
						// 
						// ucTabControlX1
						// 
						this.ucTabControlX1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
						this.ucTabControlX1.CtrlPanelColor = System.Drawing.Color.White;
						this.ucTabControlX1.Dock = System.Windows.Forms.DockStyle.Fill;
						this.ucTabControlX1.Location = new System.Drawing.Point(0, 0);
						this.ucTabControlX1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
						this.ucTabControlX1.MouseClkTabColor = System.Drawing.Color.RoyalBlue;
						this.ucTabControlX1.MouseHrTabColor = System.Drawing.Color.DodgerBlue;
						this.ucTabControlX1.Name = "ucTabControlX1";
						this.ucTabControlX1.RibbonColor = System.Drawing.Color.RoyalBlue;
						this.ucTabControlX1.SelectedTabIndex = -1;
						this.ucTabControlX1.SelTabBackColor = System.Drawing.Color.RoyalBlue;
						this.ucTabControlX1.SelTabForeColor = System.Drawing.Color.White;
						this.ucTabControlX1.Size = new System.Drawing.Size(1280, 902);
						this.ucTabControlX1.TabIndex = 0;
						this.ucTabControlX1.TabPanelColor = System.Drawing.Color.White;
						this.ucTabControlX1.TabSize = new System.Drawing.Size(130, 32);
						this.ucTabControlX1.UnSelTabBackColor = System.Drawing.Color.CornflowerBlue;
						this.ucTabControlX1.UnSelTabForeColor = System.Drawing.Color.White;
						this.ucTabControlX1.X_TextLoc = 10;
						this.ucTabControlX1.Y_TextLoc = 5;
						// 
						// FrmInforSetupInitialProcess_SXGA
						// 
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
						this.ClientSize = new System.Drawing.Size(1280, 902);
						this.Controls.Add(this.ucTabControlX1);
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
						this.Name = "FrmInforSetupInitialProcess_SXGA";
						this.Tag = "FORM_ID_INFOR_SETUP_INIT_PROCESS";
						this.Text = "FrmInforSetupInitialProcess_SXGA";
						this.Load += new System.EventHandler(this.FrmInforSetupInitialProcess_SXGA_Load);
						this.ResumeLayout(false);

		}

		#endregion

		private EzIna.GUI.UserControls.ucTabControlX ucTabControlX1;
	}
}