namespace EzIna
{
	partial class FrmPopupLaserWarmupAlarm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupLaserWarmupAlarm));
			this.panelFrmTitleBar = new System.Windows.Forms.Panel();
			this.btn_Frm_Close = new System.Windows.Forms.Button();
			this.lbl_Name = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ucRoundedPanel2 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel4 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel6 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel5 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel3 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel1 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.lbl_WarmupTimeRemaining = new System.Windows.Forms.Label();
			this.lbl_SystemStatus = new System.Windows.Forms.Label();
			this.lbl_THGGetTemp = new System.Windows.Forms.Label();
			this.lbl_SHGGetTemp = new System.Windows.Forms.Label();
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
			this.panelFrmTitleBar.Size = new System.Drawing.Size(506, 32);
			this.panelFrmTitleBar.TabIndex = 130;
			this.panelFrmTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFrmTitleBar_MouseDown);
			// 
			// btn_Frm_Close
			// 
			this.btn_Frm_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btn_Frm_Close.AutoSize = true;
			this.btn_Frm_Close.FlatAppearance.BorderSize = 0;
			this.btn_Frm_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Frm_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Close.Image")));
			this.btn_Frm_Close.Location = new System.Drawing.Point(472, 1);
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
			this.lbl_Name.Size = new System.Drawing.Size(207, 21);
			this.lbl_Name.TabIndex = 3;
			this.lbl_Name.Tag = "FormName";
			this.lbl_Name.Text = "LASER WARM UP DISPLAY";
			this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.ucRoundedPanel2);
			this.panel1.Controls.Add(this.ucRoundedPanel4);
			this.panel1.Controls.Add(this.ucRoundedPanel6);
			this.panel1.Controls.Add(this.ucRoundedPanel5);
			this.panel1.Controls.Add(this.ucRoundedPanel3);
			this.panel1.Controls.Add(this.ucRoundedPanel1);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.lbl_WarmupTimeRemaining);
			this.panel1.Controls.Add(this.lbl_SystemStatus);
			this.panel1.Controls.Add(this.lbl_THGGetTemp);
			this.panel1.Controls.Add(this.lbl_SHGGetTemp);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(506, 361);
			this.panel1.TabIndex = 131;
			// 
			// ucRoundedPanel2
			// 
			this.ucRoundedPanel2.BorderRadius = 1;
			this.ucRoundedPanel2.BorderSize = 1;
			this.ucRoundedPanel2.Caption = "STATUS";
			this.ucRoundedPanel2.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel2.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel2.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel2.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel2.Location = new System.Drawing.Point(234, 46);
			this.ucRoundedPanel2.Name = "ucRoundedPanel2";
			this.ucRoundedPanel2.Size = new System.Drawing.Size(261, 37);
			this.ucRoundedPanel2.TabIndex = 1361;
			this.ucRoundedPanel2.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel2.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel4
			// 
			this.ucRoundedPanel4.BorderRadius = 1;
			this.ucRoundedPanel4.BorderSize = 1;
			this.ucRoundedPanel4.Caption = "ITEMS";
			this.ucRoundedPanel4.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel4.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel4.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel4.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel4.Location = new System.Drawing.Point(11, 46);
			this.ucRoundedPanel4.Name = "ucRoundedPanel4";
			this.ucRoundedPanel4.Size = new System.Drawing.Size(217, 37);
			this.ucRoundedPanel4.TabIndex = 1362;
			this.ucRoundedPanel4.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel4.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel6
			// 
			this.ucRoundedPanel6.BackColor = System.Drawing.Color.White;
			this.ucRoundedPanel6.BorderRadius = 37;
			this.ucRoundedPanel6.BorderSize = 1;
			this.ucRoundedPanel6.Caption = "  Warmup Time Remaining";
			this.ucRoundedPanel6.clBegin = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ucRoundedPanel6.clBorder = System.Drawing.SystemColors.ActiveCaption;
			this.ucRoundedPanel6.clEnd = System.Drawing.SystemColors.GradientActiveCaption;
			this.ucRoundedPanel6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel6.ForeColor = System.Drawing.Color.Black;
			this.ucRoundedPanel6.Location = new System.Drawing.Point(10, 218);
			this.ucRoundedPanel6.Name = "ucRoundedPanel6";
			this.ucRoundedPanel6.Size = new System.Drawing.Size(217, 37);
			this.ucRoundedPanel6.TabIndex = 1350;
			this.ucRoundedPanel6.TextAlignHorizontal = System.Drawing.StringAlignment.Near;
			this.ucRoundedPanel6.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel5
			// 
			this.ucRoundedPanel5.BackColor = System.Drawing.Color.White;
			this.ucRoundedPanel5.BorderRadius = 37;
			this.ucRoundedPanel5.BorderSize = 1;
			this.ucRoundedPanel5.Caption = "  System Status";
			this.ucRoundedPanel5.clBegin = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ucRoundedPanel5.clBorder = System.Drawing.SystemColors.ActiveCaption;
			this.ucRoundedPanel5.clEnd = System.Drawing.SystemColors.GradientActiveCaption;
			this.ucRoundedPanel5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel5.ForeColor = System.Drawing.Color.Black;
			this.ucRoundedPanel5.Location = new System.Drawing.Point(11, 175);
			this.ucRoundedPanel5.Name = "ucRoundedPanel5";
			this.ucRoundedPanel5.Size = new System.Drawing.Size(217, 37);
			this.ucRoundedPanel5.TabIndex = 1351;
			this.ucRoundedPanel5.TextAlignHorizontal = System.Drawing.StringAlignment.Near;
			this.ucRoundedPanel5.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel3
			// 
			this.ucRoundedPanel3.BackColor = System.Drawing.Color.White;
			this.ucRoundedPanel3.BorderRadius = 37;
			this.ucRoundedPanel3.BorderSize = 1;
			this.ucRoundedPanel3.Caption = "   THG Temperature (ºC)";
			this.ucRoundedPanel3.clBegin = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ucRoundedPanel3.clBorder = System.Drawing.SystemColors.ActiveCaption;
			this.ucRoundedPanel3.clEnd = System.Drawing.SystemColors.GradientActiveCaption;
			this.ucRoundedPanel3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel3.ForeColor = System.Drawing.Color.Black;
			this.ucRoundedPanel3.Location = new System.Drawing.Point(11, 132);
			this.ucRoundedPanel3.Name = "ucRoundedPanel3";
			this.ucRoundedPanel3.Size = new System.Drawing.Size(217, 37);
			this.ucRoundedPanel3.TabIndex = 1352;
			this.ucRoundedPanel3.TextAlignHorizontal = System.Drawing.StringAlignment.Near;
			this.ucRoundedPanel3.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel1
			// 
			this.ucRoundedPanel1.BackColor = System.Drawing.Color.White;
			this.ucRoundedPanel1.BorderRadius = 37;
			this.ucRoundedPanel1.BorderSize = 1;
			this.ucRoundedPanel1.Caption = "   SHG Temperature (ºC)";
			this.ucRoundedPanel1.clBegin = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ucRoundedPanel1.clBorder = System.Drawing.SystemColors.ActiveCaption;
			this.ucRoundedPanel1.clEnd = System.Drawing.SystemColors.GradientActiveCaption;
			this.ucRoundedPanel1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel1.ForeColor = System.Drawing.Color.Black;
			this.ucRoundedPanel1.Location = new System.Drawing.Point(11, 89);
			this.ucRoundedPanel1.Name = "ucRoundedPanel1";
			this.ucRoundedPanel1.Size = new System.Drawing.Size(217, 37);
			this.ucRoundedPanel1.TabIndex = 1353;
			this.ucRoundedPanel1.TextAlignHorizontal = System.Drawing.StringAlignment.Near;
			this.ucRoundedPanel1.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.White;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(10, 258);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(485, 86);
			this.label6.TabIndex = 1356;
			this.label6.Tag = "";
			this.label6.Text = resources.GetString("label6.Text");
			// 
			// lbl_WarmupTimeRemaining
			// 
			this.lbl_WarmupTimeRemaining.BackColor = System.Drawing.SystemColors.Info;
			this.lbl_WarmupTimeRemaining.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_WarmupTimeRemaining.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_WarmupTimeRemaining.Location = new System.Drawing.Point(233, 218);
			this.lbl_WarmupTimeRemaining.Name = "lbl_WarmupTimeRemaining";
			this.lbl_WarmupTimeRemaining.Size = new System.Drawing.Size(261, 37);
			this.lbl_WarmupTimeRemaining.TabIndex = 1357;
			this.lbl_WarmupTimeRemaining.Tag = "";
			this.lbl_WarmupTimeRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_SystemStatus
			// 
			this.lbl_SystemStatus.BackColor = System.Drawing.SystemColors.Info;
			this.lbl_SystemStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_SystemStatus.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_SystemStatus.Location = new System.Drawing.Point(234, 175);
			this.lbl_SystemStatus.Name = "lbl_SystemStatus";
			this.lbl_SystemStatus.Size = new System.Drawing.Size(261, 37);
			this.lbl_SystemStatus.TabIndex = 1358;
			this.lbl_SystemStatus.Tag = "";
			this.lbl_SystemStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_THGGetTemp
			// 
			this.lbl_THGGetTemp.BackColor = System.Drawing.SystemColors.Info;
			this.lbl_THGGetTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_THGGetTemp.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_THGGetTemp.Location = new System.Drawing.Point(234, 132);
			this.lbl_THGGetTemp.Name = "lbl_THGGetTemp";
			this.lbl_THGGetTemp.Size = new System.Drawing.Size(261, 37);
			this.lbl_THGGetTemp.TabIndex = 1359;
			this.lbl_THGGetTemp.Tag = "";
			this.lbl_THGGetTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_SHGGetTemp
			// 
			this.lbl_SHGGetTemp.BackColor = System.Drawing.SystemColors.Info;
			this.lbl_SHGGetTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_SHGGetTemp.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_SHGGetTemp.Location = new System.Drawing.Point(234, 89);
			this.lbl_SHGGetTemp.Name = "lbl_SHGGetTemp";
			this.lbl_SHGGetTemp.Size = new System.Drawing.Size(261, 37);
			this.lbl_SHGGetTemp.TabIndex = 1360;
			this.lbl_SHGGetTemp.Tag = "";
			this.lbl_SHGGetTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FrmPopupLaserWarmupAlarm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(506, 361);
			this.Controls.Add(this.panelFrmTitleBar);
			this.Controls.Add(this.panel1);
			this.Name = "FrmPopupLaserWarmupAlarm";
			this.Text = "FrmPopupLaserWarmupAlarm";
			this.Load += new System.EventHandler(this.FrmPopupLaserWarmupAlarm_Load);
			this.VisibleChanged += new System.EventHandler(this.FrmPopupLaserWarmupAlarm_VisibleChanged);
			this.panelFrmTitleBar.ResumeLayout(false);
			this.panelFrmTitleBar.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Label lbl_Name;
		private System.Windows.Forms.Panel panel1;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel2;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel4;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel6;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel5;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel3;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lbl_WarmupTimeRemaining;
		private System.Windows.Forms.Label lbl_SystemStatus;
		private System.Windows.Forms.Label lbl_THGGetTemp;
		private System.Windows.Forms.Label lbl_SHGGetTemp;
	}
}