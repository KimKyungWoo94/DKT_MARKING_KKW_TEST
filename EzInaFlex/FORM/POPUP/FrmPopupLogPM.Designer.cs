namespace EzIna
{
	partial class FrmPopupLogPM
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupLogPM));
			this.listBox_PM = new System.Windows.Forms.ListBox();
			this.panelFrmTitleBar = new System.Windows.Forms.Panel();
			this.btn_Frm_Close = new System.Windows.Forms.Button();
			this.lbl_Name = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelFrmTitleBar.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox_PM
			// 
			this.listBox_PM.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBox_PM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox_PM.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBox_PM.FormattingEnabled = true;
			this.listBox_PM.ItemHeight = 17;
			this.listBox_PM.Location = new System.Drawing.Point(0, 0);
			this.listBox_PM.Name = "listBox_PM";
			this.listBox_PM.Size = new System.Drawing.Size(282, 259);
			this.listBox_PM.TabIndex = 129;
			// 
			// panelFrmTitleBar
			// 
			this.panelFrmTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Close);
			this.panelFrmTitleBar.Controls.Add(this.lbl_Name);
			this.panelFrmTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelFrmTitleBar.Location = new System.Drawing.Point(0, 0);
			this.panelFrmTitleBar.Name = "panelFrmTitleBar";
			this.panelFrmTitleBar.Size = new System.Drawing.Size(284, 32);
			this.panelFrmTitleBar.TabIndex = 131;
			// 
			// btn_Frm_Close
			// 
			this.btn_Frm_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btn_Frm_Close.AutoSize = true;
			this.btn_Frm_Close.FlatAppearance.BorderSize = 0;
			this.btn_Frm_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Frm_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Close.Image")));
			this.btn_Frm_Close.Location = new System.Drawing.Point(250, 1);
			this.btn_Frm_Close.Name = "btn_Frm_Close";
			this.btn_Frm_Close.Size = new System.Drawing.Size(30, 30);
			this.btn_Frm_Close.TabIndex = 4;
			this.btn_Frm_Close.Tag = "FrmClose";
			this.btn_Frm_Close.UseVisualStyleBackColor = true;
			// 
			// lbl_Name
			// 
			this.lbl_Name.AutoSize = true;
			this.lbl_Name.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Name.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Name.ForeColor = System.Drawing.Color.White;
			this.lbl_Name.ImageIndex = 2;
			this.lbl_Name.Location = new System.Drawing.Point(7, 7);
			this.lbl_Name.Name = "lbl_Name";
			this.lbl_Name.Size = new System.Drawing.Size(73, 21);
			this.lbl_Name.TabIndex = 3;
			this.lbl_Name.Tag = "FormName";
			this.lbl_Name.Text = "LOG-SW";
			this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.listBox_PM);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(284, 261);
			this.panel1.TabIndex = 132;
			// 
			// FrmPopupLogPM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.panelFrmTitleBar);
			this.Controls.Add(this.panel1);
			this.Name = "FrmPopupLogPM";
			this.Text = "FrmPopupLogPM";
			this.panelFrmTitleBar.ResumeLayout(false);
			this.panelFrmTitleBar.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.ListBox listBox_PM;
		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Label lbl_Name;
		private System.Windows.Forms.Panel panel1;
	}
}