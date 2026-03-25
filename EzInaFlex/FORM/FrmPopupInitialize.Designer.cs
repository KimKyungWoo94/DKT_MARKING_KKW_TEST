namespace EzIna
{
	partial class FrmPopupInitialize
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listBox_Data = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblLoading = new System.Windows.Forms.Label();
			this.ucProcessProgressBar_Initialize = new EzIna.GUI.UserControls.ucProcessProgressBar();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.pictureBoxLogo);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.listBox_Data);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.lblLoading);
			this.panel1.Controls.Add(this.ucProcessProgressBar_Initialize);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(508, 237);
			this.panel1.TabIndex = 0;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pictureBoxLogo.Location = new System.Drawing.Point(0, 158);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(506, 47);
			this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxLogo.TabIndex = 34;
			this.pictureBoxLogo.TabStop = false;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.Control;
			this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Gray;
			this.label2.Location = new System.Drawing.Point(0, 205);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(506, 30);
			this.label2.TabIndex = 32;
			this.label2.Tag = "TAIL";
			this.label2.Text = "Designed by Animotion Tech Co.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listBox_Data
			// 
			this.listBox_Data.BackColor = System.Drawing.SystemColors.Control;
			this.listBox_Data.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBox_Data.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBox_Data.ForeColor = System.Drawing.Color.Gray;
			this.listBox_Data.FormattingEnabled = true;
			this.listBox_Data.ItemHeight = 21;
			this.listBox_Data.Location = new System.Drawing.Point(146, 38);
			this.listBox_Data.Name = "listBox_Data";
			this.listBox_Data.Size = new System.Drawing.Size(357, 105);
			this.listBox_Data.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(506, 30);
			this.label1.TabIndex = 3;
			this.label1.Tag = "TITLE";
			this.label1.Text = "EzInaFlex2.0";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLoading
			// 
			this.lblLoading.AutoSize = true;
			this.lblLoading.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLoading.ForeColor = System.Drawing.Color.Gray;
			this.lblLoading.Location = new System.Drawing.Point(46, 115);
			this.lblLoading.Name = "lblLoading";
			this.lblLoading.Size = new System.Drawing.Size(37, 23);
			this.lblLoading.TabIndex = 30;
			this.lblLoading.Text = "0%";
			this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ucProcessProgressBar_Initialize
			// 
			this.ucProcessProgressBar_Initialize.BackColor = System.Drawing.Color.Transparent;
			this.ucProcessProgressBar_Initialize.CircleColor = System.Drawing.Color.Gray;
			this.ucProcessProgressBar_Initialize.CircleDiameter = 12;
			this.ucProcessProgressBar_Initialize.Interval = 50;
			this.ucProcessProgressBar_Initialize.Location = new System.Drawing.Point(24, 48);
			this.ucProcessProgressBar_Initialize.MinimumSize = new System.Drawing.Size(32, 32);
			this.ucProcessProgressBar_Initialize.Name = "ucProcessProgressBar_Initialize";
			this.ucProcessProgressBar_Initialize.Rotation = EzIna.GUI.UserControls.ucProcessProgressBar.eRotation.CW;
			this.ucProcessProgressBar_Initialize.Size = new System.Drawing.Size(71, 61);
			this.ucProcessProgressBar_Initialize.TabIndex = 5;
			// 
			// FrmPopupInitialize
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(508, 237);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmPopupInitialize";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmPopupInitialize";
			this.Load += new System.EventHandler(this.FrmPopupInitialize_Load);
			this.Shown += new System.EventHandler(this.FrmPopupInitialize_Shown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListBox listBox_Data;
		private System.Windows.Forms.Label label1;
		private EzIna.GUI.UserControls.ucProcessProgressBar ucProcessProgressBar_Initialize;
		private System.Windows.Forms.Label lblLoading;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
	}
}