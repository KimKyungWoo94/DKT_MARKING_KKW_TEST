namespace EzIna
{
	partial class FrmInforOperAuto_TactTimeStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforOperAuto_TactTimeStatus));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelFrmTitleBar = new System.Windows.Forms.Panel();
            this.btn_Frm_Close = new System.Windows.Forms.Button();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.StatusUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ucRoundedPanel4 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.Btn_WorkTimeReset = new System.Windows.Forms.Button();
            this.dataGridView_WorkTime = new System.Windows.Forms.DataGridView();
            this.panelFrmTitleBar.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WorkTime)).BeginInit();
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
            this.lbl_Name.Size = new System.Drawing.Size(0, 21);
            this.lbl_Name.TabIndex = 3;
            this.lbl_Name.Tag = "FormName";
            this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.ucRoundedPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.Btn_WorkTimeReset, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.dataGridView_WorkTime, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(506, 245);
            this.tableLayoutPanel3.TabIndex = 128;
            // 
            // ucRoundedPanel4
            // 
            this.ucRoundedPanel4.BorderRadius = 32;
            this.ucRoundedPanel4.BorderSize = 1;
            this.ucRoundedPanel4.Caption = "TIME";
            this.ucRoundedPanel4.clBegin = System.Drawing.Color.CornflowerBlue;
            this.ucRoundedPanel4.clBorder = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel4.clEnd = System.Drawing.Color.RoyalBlue;
            this.ucRoundedPanel4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel4.ForeColor = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel4.Location = new System.Drawing.Point(3, 3);
            this.ucRoundedPanel4.Name = "ucRoundedPanel4";
            this.ucRoundedPanel4.Size = new System.Drawing.Size(217, 32);
            this.ucRoundedPanel4.TabIndex = 102;
            this.ucRoundedPanel4.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel4.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // Btn_WorkTimeReset
            // 
            this.Btn_WorkTimeReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_WorkTimeReset.Location = new System.Drawing.Point(409, 3);
            this.Btn_WorkTimeReset.Name = "Btn_WorkTimeReset";
            this.Btn_WorkTimeReset.Size = new System.Drawing.Size(94, 34);
            this.Btn_WorkTimeReset.TabIndex = 101;
            this.Btn_WorkTimeReset.Text = "Reset";
            this.Btn_WorkTimeReset.UseVisualStyleBackColor = true;
            this.Btn_WorkTimeReset.Click += new System.EventHandler(this.Btn_WorkTimeReset_Click);
            // 
            // dataGridView_WorkTime
            // 
            this.dataGridView_WorkTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel3.SetColumnSpan(this.dataGridView_WorkTime, 2);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_WorkTime.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_WorkTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_WorkTime.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridView_WorkTime.Location = new System.Drawing.Point(3, 43);
            this.dataGridView_WorkTime.Name = "dataGridView_WorkTime";
            this.dataGridView_WorkTime.RowTemplate.Height = 23;
            this.dataGridView_WorkTime.Size = new System.Drawing.Size(500, 199);
            this.dataGridView_WorkTime.TabIndex = 96;
            // 
            // FrmInforOperAuto_TactTimeStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 276);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.panelFrmTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInforOperAuto_TactTimeStatus";
            this.Text = "FrmPopupLogSW";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmInforOperAuto_TactTimeStatus_FormClosed);
            this.Load += new System.EventHandler(this.FrmFrmInforOperAuto_ProcessStatus_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmInforOperAuto_ProcessStatus_VisibleChanged);
            this.panelFrmTitleBar.ResumeLayout(false);
            this.panelFrmTitleBar.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WorkTime)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Label lbl_Name;
				private System.Windows.Forms.Timer StatusUpdateTimer;
				private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
				private GUI.UserControls.ucRoundedPanel ucRoundedPanel4;
				private System.Windows.Forms.Button Btn_WorkTimeReset;
				private System.Windows.Forms.DataGridView dataGridView_WorkTime;
		}
}