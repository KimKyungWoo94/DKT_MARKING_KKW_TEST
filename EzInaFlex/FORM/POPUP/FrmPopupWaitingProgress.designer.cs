namespace EzIna
{
	partial class FrmPopupWaitingProgress
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
			this.ucWaitingProgressBar1 = new EzIna.GUI.UserControls.ucWaitingProgressBar();
			this.SuspendLayout();
			// 
			// ucWaitingProgressBar1
			// 
			this.ucWaitingProgressBar1.BackColor = System.Drawing.Color.Transparent;
			this.ucWaitingProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucWaitingProgressBar1.Interval = 60;
			this.ucWaitingProgressBar1.Location = new System.Drawing.Point(0, 0);
			this.ucWaitingProgressBar1.MinimumSize = new System.Drawing.Size(28, 28);
			this.ucWaitingProgressBar1.Name = "ucWaitingProgressBar1";
			this.ucWaitingProgressBar1.Rotation = EzIna.GUI.UserControls.ucWaitingProgressBar.Direction.CLOCKWISE;
			this.ucWaitingProgressBar1.Size = new System.Drawing.Size(150, 150);
			this.ucWaitingProgressBar1.StartAngle = 270;
			this.ucWaitingProgressBar1.TabIndex = 0;
			this.ucWaitingProgressBar1.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			// 
			// FrmPopupWaitingProgress
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(150, 150);
			this.Controls.Add(this.ucWaitingProgressBar1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmPopupWaitingProgress";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WaittingProgress";
			this.TransparencyKey = System.Drawing.SystemColors.Control;
			this.VisibleChanged += new System.EventHandler(this.FrmPopupWaitingProgress_VisibleChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private GUI.UserControls.ucWaitingProgressBar ucWaitingProgressBar1;
	}
}