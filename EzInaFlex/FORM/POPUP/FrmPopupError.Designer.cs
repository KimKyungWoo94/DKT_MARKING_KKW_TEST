namespace EzIna
{
    partial class FrmPopupError
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
						System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupError));
						this.btnBuzzerOn = new System.Windows.Forms.Button();
						this.imageList1 = new System.Windows.Forms.ImageList(this.components);
						this.label1 = new System.Windows.Forms.Label();
						this.btnclose = new System.Windows.Forms.Button();
						this.panel1 = new System.Windows.Forms.Panel();
						this.lbl_ErrNo = new System.Windows.Forms.Label();
						this.ucDadaFlicker = new EzIna.GUI.UserControls.ucFlicker();
						this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
						this.lbl_ErrorMsg = new System.Windows.Forms.Label();
						this.lbl_Err_Descr = new System.Windows.Forms.Label();
						this.panel1.SuspendLayout();
						this.tableLayoutPanel1.SuspendLayout();
						this.SuspendLayout();
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
						this.btnBuzzerOn.TabIndex = 6;
						this.btnBuzzerOn.UseVisualStyleBackColor = false;
						this.btnBuzzerOn.Click += new System.EventHandler(this.btnBuzzerOn_Click);
						// 
						// imageList1
						// 
						this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
						this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
						this.imageList1.Images.SetKeyName(0, "speaker off.png");
						this.imageList1.Images.SetKeyName(1, "speaker on.png");
						this.imageList1.Images.SetKeyName(2, "close.png");
						// 
						// label1
						// 
						this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
						this.label1.Dock = System.Windows.Forms.DockStyle.Top;
						this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label1.ForeColor = System.Drawing.Color.White;
						this.label1.Location = new System.Drawing.Point(0, 0);
						this.label1.Name = "label1";
						this.label1.Size = new System.Drawing.Size(800, 35);
						this.label1.TabIndex = 5;
						this.label1.Text = "Error Message";
						this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// btnclose
						// 
						this.btnclose.BackColor = System.Drawing.Color.White;
						this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
						this.btnclose.ImageKey = "close.png";
						this.btnclose.ImageList = this.imageList1;
						this.btnclose.Location = new System.Drawing.Point(760, 1);
						this.btnclose.Name = "btnclose";
						this.btnclose.Size = new System.Drawing.Size(38, 32);
						this.btnclose.TabIndex = 7;
						this.btnclose.UseVisualStyleBackColor = false;
						this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
						// 
						// panel1
						// 
						this.panel1.BackColor = System.Drawing.Color.White;
						this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel1.Controls.Add(this.tableLayoutPanel1);
						this.panel1.Controls.Add(this.ucDadaFlicker);
						this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
						this.panel1.Location = new System.Drawing.Point(0, 35);
						this.panel1.Name = "panel1";
						this.panel1.Size = new System.Drawing.Size(800, 321);
						this.panel1.TabIndex = 8;
						// 
						// lbl_ErrNo
						// 
						this.lbl_ErrNo.AutoSize = true;
						this.lbl_ErrNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
						this.lbl_ErrNo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lbl_ErrNo.ForeColor = System.Drawing.Color.White;
						this.lbl_ErrNo.Location = new System.Drawing.Point(12, 9);
						this.lbl_ErrNo.Name = "lbl_ErrNo";
						this.lbl_ErrNo.Size = new System.Drawing.Size(57, 17);
						this.lbl_ErrNo.TabIndex = 9;
						this.lbl_ErrNo.Text = "Error No.";
						// 
						// ucDadaFlicker
						// 
						this.ucDadaFlicker.flickerImg = EzIna.GUI.UserControls.ucFlicker.eImage.Error;
						this.ucDadaFlicker.Interval = 500;
						this.ucDadaFlicker.Location = new System.Drawing.Point(44, 114);
						this.ucDadaFlicker.Name = "ucDadaFlicker";
						this.ucDadaFlicker.Size = new System.Drawing.Size(103, 99);
						this.ucDadaFlicker.TabIndex = 13;
						// 
						// tableLayoutPanel1
						// 
						this.tableLayoutPanel1.ColumnCount = 1;
						this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
						this.tableLayoutPanel1.Controls.Add(this.lbl_Err_Descr, 0, 1);
						this.tableLayoutPanel1.Controls.Add(this.lbl_ErrorMsg, 0, 0);
						this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
						this.tableLayoutPanel1.Location = new System.Drawing.Point(194, 0);
						this.tableLayoutPanel1.Name = "tableLayoutPanel1";
						this.tableLayoutPanel1.RowCount = 2;
						this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.33333F));
						this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
						this.tableLayoutPanel1.Size = new System.Drawing.Size(604, 319);
						this.tableLayoutPanel1.TabIndex = 14;
						// 
						// lbl_ErrorMsg
						// 
						this.lbl_ErrorMsg.BackColor = System.Drawing.Color.White;
						this.lbl_ErrorMsg.Dock = System.Windows.Forms.DockStyle.Right;
						this.lbl_ErrorMsg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
						this.lbl_ErrorMsg.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lbl_ErrorMsg.Location = new System.Drawing.Point(3, 0);
						this.lbl_ErrorMsg.Name = "lbl_ErrorMsg";
						this.lbl_ErrorMsg.Size = new System.Drawing.Size(598, 186);
						this.lbl_ErrorMsg.TabIndex = 13;
						this.lbl_ErrorMsg.Text = "Message";
						this.lbl_ErrorMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// lbl_Err_Descr
						// 
						this.lbl_Err_Descr.BackColor = System.Drawing.Color.White;
						this.lbl_Err_Descr.Dock = System.Windows.Forms.DockStyle.Right;
						this.lbl_Err_Descr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
						this.lbl_Err_Descr.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lbl_Err_Descr.Location = new System.Drawing.Point(3, 186);
						this.lbl_Err_Descr.Name = "lbl_Err_Descr";
						this.lbl_Err_Descr.Size = new System.Drawing.Size(598, 133);
						this.lbl_Err_Descr.TabIndex = 14;
						this.lbl_Err_Descr.Text = "Message";
						this.lbl_Err_Descr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// FrmPopupError
						// 
						this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
						this.BackColor = System.Drawing.SystemColors.Control;
						this.ClientSize = new System.Drawing.Size(800, 356);
						this.Controls.Add(this.lbl_ErrNo);
						this.Controls.Add(this.panel1);
						this.Controls.Add(this.btnclose);
						this.Controls.Add(this.btnBuzzerOn);
						this.Controls.Add(this.label1);
						this.DoubleBuffered = true;
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
						this.Name = "FrmPopupError";
						this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
						this.Text = "Error";
						this.VisibleChanged += new System.EventHandler(this.FrmPopupError_VisibleChanged);
						this.panel1.ResumeLayout(false);
						this.tableLayoutPanel1.ResumeLayout(false);
						this.ResumeLayout(false);
						this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnBuzzerOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private GUI.UserControls.ucFlicker ucDadaFlicker;
        private System.Windows.Forms.Label lbl_ErrNo;
				private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
				private System.Windows.Forms.Label lbl_Err_Descr;
				private System.Windows.Forms.Label lbl_ErrorMsg;
		}
}