namespace EzIna
{
	partial class FrmPopupLogMC
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupLogMC));
			this.panelFrmTitleBar = new System.Windows.Forms.Panel();
			this.btn_Frm_Close = new System.Windows.Forms.Button();
			this.lbl_Name = new System.Windows.Forms.Label();
			this.listBox_MC = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelFrmTitleBar.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelFrmTitleBar
			// 
			this.panelFrmTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Close);
			this.panelFrmTitleBar.Controls.Add(this.lbl_Name);
			this.panelFrmTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelFrmTitleBar.Location = new System.Drawing.Point(0, 0);
			this.panelFrmTitleBar.Name = "panelFrmTitleBar";
			this.panelFrmTitleBar.Size = new System.Drawing.Size(784, 32);
			this.panelFrmTitleBar.TabIndex = 127;
			this.panelFrmTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFrmTitleBar_MouseDown);
			// 
			// btn_Frm_Close
			// 
			this.btn_Frm_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btn_Frm_Close.AutoSize = true;
			this.btn_Frm_Close.FlatAppearance.BorderSize = 0;
			this.btn_Frm_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Frm_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Close.Image")));
			this.btn_Frm_Close.Location = new System.Drawing.Point(750, 1);
			this.btn_Frm_Close.Name = "btn_Frm_Close";
			this.btn_Frm_Close.Size = new System.Drawing.Size(30, 30);
			this.btn_Frm_Close.TabIndex = 4;
			this.btn_Frm_Close.Tag = "WithoutCommonStyle";
			this.btn_Frm_Close.UseVisualStyleBackColor = true;
			this.btn_Frm_Close.Click += new System.EventHandler(this.btn_Frm_Close_Click);
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
			this.lbl_Name.Size = new System.Drawing.Size(78, 21);
			this.lbl_Name.TabIndex = 3;
			this.lbl_Name.Tag = "FormName";
			this.lbl_Name.Text = "LOG-MC";
			this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listBox_MC
			// 
			this.listBox_MC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listBox_MC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox_MC.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBox_MC.FormattingEnabled = true;
			this.listBox_MC.ItemHeight = 17;
			this.listBox_MC.Location = new System.Drawing.Point(0, 0);
			this.listBox_MC.Name = "listBox_MC";
			this.listBox_MC.ScrollAlwaysVisible = true;
			this.listBox_MC.Size = new System.Drawing.Size(782, 527);
			this.listBox_MC.TabIndex = 128;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.listBox_MC);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(784, 529);
			this.panel1.TabIndex = 129;
			// 
			// FrmPopupLogMC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panelFrmTitleBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmPopupLogMC";
			this.Text = "FrmPopupLogMC";
			this.panelFrmTitleBar.ResumeLayout(false);
			this.panelFrmTitleBar.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Label lbl_Name;
		public	System.Windows.Forms.ListBox listBox_MC;
		private System.Windows.Forms.Panel panel1;
	}
}