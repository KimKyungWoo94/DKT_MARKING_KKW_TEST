namespace EzIna
{
	partial class FrmInforOperAuto_ProcessStatus
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
						System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforOperAuto_ProcessStatus));
						System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
						this.panelFrmTitleBar = new System.Windows.Forms.Panel();
						this.lbl_Name = new System.Windows.Forms.Label();
						this.btn_Frm_Close = new System.Windows.Forms.Button();
						this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
						this.DGV_ProcessInfo = new System.Windows.Forms.DataGridView();
						this.label2 = new System.Windows.Forms.Label();
						this.lb_Status_StartNum = new System.Windows.Forms.Label();
						this.StatusUpdateTimer = new System.Windows.Forms.Timer(this.components);
						this.panelFrmTitleBar.SuspendLayout();
						this.tableLayoutPanel1.SuspendLayout();
						((System.ComponentModel.ISupportInitialize)(this.DGV_ProcessInfo)).BeginInit();
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
						this.panelFrmTitleBar.Size = new System.Drawing.Size(542, 32);
						this.panelFrmTitleBar.TabIndex = 127;
						this.panelFrmTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFrmTitleBar_MouseDown);
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
						this.lbl_Name.Size = new System.Drawing.Size(119, 21);
						this.lbl_Name.TabIndex = 3;
						this.lbl_Name.Tag = "FormName";
						this.lbl_Name.Text = "Process Status";
						this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
						// 
						// btn_Frm_Close
						// 
						this.btn_Frm_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
						this.btn_Frm_Close.AutoSize = true;
						this.btn_Frm_Close.FlatAppearance.BorderSize = 0;
						this.btn_Frm_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.btn_Frm_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Close.Image")));
						this.btn_Frm_Close.Location = new System.Drawing.Point(508, 1);
						this.btn_Frm_Close.Name = "btn_Frm_Close";
						this.btn_Frm_Close.Size = new System.Drawing.Size(30, 30);
						this.btn_Frm_Close.TabIndex = 4;
						this.btn_Frm_Close.Tag = "WithoutCommonStyle";
						this.btn_Frm_Close.UseVisualStyleBackColor = true;
						this.btn_Frm_Close.Click += new System.EventHandler(this.btn_Frm_Close_Click);
						// 
						// tableLayoutPanel1
						// 
						this.tableLayoutPanel1.ColumnCount = 2;
						this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
						this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
						this.tableLayoutPanel1.Controls.Add(this.lb_Status_StartNum, 1, 0);
						this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
						this.tableLayoutPanel1.Controls.Add(this.DGV_ProcessInfo, 0, 1);
						this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
						this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 32);
						this.tableLayoutPanel1.Name = "tableLayoutPanel1";
						this.tableLayoutPanel1.RowCount = 2;
						this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
						this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
						this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
						this.tableLayoutPanel1.Size = new System.Drawing.Size(542, 711);
						this.tableLayoutPanel1.TabIndex = 128;
						// 
						// DGV_ProcessInfo
						// 
						this.DGV_ProcessInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
						this.tableLayoutPanel1.SetColumnSpan(this.DGV_ProcessInfo, 2);
						dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
						dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
						dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
						dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
						dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
						dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
						dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
						this.DGV_ProcessInfo.DefaultCellStyle = dataGridViewCellStyle1;
						this.DGV_ProcessInfo.Dock = System.Windows.Forms.DockStyle.Fill;
						this.DGV_ProcessInfo.GridColor = System.Drawing.Color.SteelBlue;
						this.DGV_ProcessInfo.Location = new System.Drawing.Point(0, 30);
						this.DGV_ProcessInfo.Margin = new System.Windows.Forms.Padding(0);
						this.DGV_ProcessInfo.Name = "DGV_ProcessInfo";
						this.DGV_ProcessInfo.RowTemplate.Height = 23;
						this.DGV_ProcessInfo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
						this.DGV_ProcessInfo.Size = new System.Drawing.Size(542, 681);
						this.DGV_ProcessInfo.TabIndex = 98;
						// 
						// label2
						// 
						this.label2.BackColor = System.Drawing.SystemColors.HotTrack;
						this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
						this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label2.ForeColor = System.Drawing.Color.White;
						this.label2.Location = new System.Drawing.Point(0, 0);
						this.label2.Margin = new System.Windows.Forms.Padding(0);
						this.label2.Name = "label2";
						this.label2.Size = new System.Drawing.Size(271, 30);
						this.label2.TabIndex = 102;
						this.label2.Text = "Start IDX";
						this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
						// 
						// lb_Status_StartNum
						// 
						this.lb_Status_StartNum.BackColor = System.Drawing.SystemColors.ControlDark;
						this.lb_Status_StartNum.Dock = System.Windows.Forms.DockStyle.Fill;
						this.lb_Status_StartNum.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lb_Status_StartNum.ForeColor = System.Drawing.Color.Black;
						this.lb_Status_StartNum.Location = new System.Drawing.Point(271, 0);
						this.lb_Status_StartNum.Margin = new System.Windows.Forms.Padding(0);
						this.lb_Status_StartNum.Name = "lb_Status_StartNum";
						this.lb_Status_StartNum.Size = new System.Drawing.Size(271, 30);
						this.lb_Status_StartNum.TabIndex = 105;
						this.lb_Status_StartNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.lb_Status_StartNum.Click += new System.EventHandler(this.lb_Status_StartNum_Click);
						// 
						// FrmInforOperAuto_ProcessStatus
						// 
						this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
						this.ClientSize = new System.Drawing.Size(542, 743);
						this.Controls.Add(this.tableLayoutPanel1);
						this.Controls.Add(this.panelFrmTitleBar);
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
						this.Name = "FrmInforOperAuto_ProcessStatus";
						this.Text = "FrmPopupLogSW";
						this.Load += new System.EventHandler(this.FrmFrmInforOperAuto_ProcessStatus_Load);
						this.VisibleChanged += new System.EventHandler(this.FrmInforOperAuto_ProcessStatus_VisibleChanged);
						this.panelFrmTitleBar.ResumeLayout(false);
						this.panelFrmTitleBar.PerformLayout();
						this.tableLayoutPanel1.ResumeLayout(false);
						((System.ComponentModel.ISupportInitialize)(this.DGV_ProcessInfo)).EndInit();
						this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Label lbl_Name;
				private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
				private System.Windows.Forms.DataGridView DGV_ProcessInfo;
				private System.Windows.Forms.Label label2;
				private System.Windows.Forms.Label lb_Status_StartNum;
				private System.Windows.Forms.Timer StatusUpdateTimer;
		}
}