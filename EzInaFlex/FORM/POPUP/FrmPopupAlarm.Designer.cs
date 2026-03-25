using EzIna.GUI.UserControls;
using EzInaGui;

namespace EzIna
{
    partial class FrmPopupAlarm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupAlarm));
			this.label1 = new System.Windows.Forms.Label();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnclose = new System.Windows.Forms.Button();
			this.btnBuzzerOn = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lbl_AlarmMsg = new System.Windows.Forms.Label();
			this.ucProcessProgressBar1 = new EzIna.GUI.UserControls.ucProcessProgressBar();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.RoyalBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(800, 35);
			this.label1.TabIndex = 3;
			this.label1.Text = "Alarm message";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "speaker off.png");
			this.imageList1.Images.SetKeyName(1, "speaker on.png");
			this.imageList1.Images.SetKeyName(2, "close.png");
			// 
			// btnclose
			// 
			this.btnclose.BackColor = System.Drawing.Color.White;
			this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnclose.ImageIndex = 2;
			this.btnclose.ImageList = this.imageList1;
			this.btnclose.Location = new System.Drawing.Point(760, 1);
			this.btnclose.Name = "btnclose";
			this.btnclose.Size = new System.Drawing.Size(38, 32);
			this.btnclose.TabIndex = 4;
			this.btnclose.UseVisualStyleBackColor = false;
			this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
			// 
			// btnBuzzerOn
			// 
			this.btnBuzzerOn.BackColor = System.Drawing.Color.White;
			this.btnBuzzerOn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnBuzzerOn.ImageIndex = 1;
			this.btnBuzzerOn.ImageList = this.imageList1;
			this.btnBuzzerOn.Location = new System.Drawing.Point(721, 1);
			this.btnBuzzerOn.Name = "btnBuzzerOn";
			this.btnBuzzerOn.Size = new System.Drawing.Size(38, 32);
			this.btnBuzzerOn.TabIndex = 4;
			this.btnBuzzerOn.UseVisualStyleBackColor = false;
			this.btnBuzzerOn.Click += new System.EventHandler(this.btnBuzzerOn_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lbl_AlarmMsg);
			this.panel1.Controls.Add(this.ucProcessProgressBar1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 35);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(800, 165);
			this.panel1.TabIndex = 11;
			// 
			// lbl_AlarmMsg
			// 
			this.lbl_AlarmMsg.BackColor = System.Drawing.Color.White;
			this.lbl_AlarmMsg.Dock = System.Windows.Forms.DockStyle.Right;
			this.lbl_AlarmMsg.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_AlarmMsg.Location = new System.Drawing.Point(160, 0);
			this.lbl_AlarmMsg.Name = "lbl_AlarmMsg";
			this.lbl_AlarmMsg.Size = new System.Drawing.Size(638, 163);
			this.lbl_AlarmMsg.TabIndex = 13;
			this.lbl_AlarmMsg.Text = "Message";
			this.lbl_AlarmMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ucProcessProgressBar1
			// 
			this.ucProcessProgressBar1.BackColor = System.Drawing.Color.Transparent;
			this.ucProcessProgressBar1.CircleColor = System.Drawing.Color.Gray;
			this.ucProcessProgressBar1.CircleDiameter = 15;
			this.ucProcessProgressBar1.Interval = 50;
			this.ucProcessProgressBar1.Location = new System.Drawing.Point(12, 26);
			this.ucProcessProgressBar1.MinimumSize = new System.Drawing.Size(32, 32);
			this.ucProcessProgressBar1.Name = "ucProcessProgressBar1";
			this.ucProcessProgressBar1.Rotation = EzIna.GUI.UserControls.ucProcessProgressBar.eRotation.CW;
			this.ucProcessProgressBar1.Size = new System.Drawing.Size(128, 98);
			this.ucProcessProgressBar1.TabIndex = 10;
			// 
			// FrmPopupAlarm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 200);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnclose);
			this.Controls.Add(this.btnBuzzerOn);
			this.Controls.Add(this.label1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmPopupAlarm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmPopupNofication";
			this.VisibleChanged += new System.EventHandler(this.FrmPopupNotification_VisibleChanged);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuzzerOn;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_AlarmMsg;
        private ucProcessProgressBar ucProcessProgressBar1;
    }
}