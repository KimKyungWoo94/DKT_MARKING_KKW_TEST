namespace EzIna
{
	partial class FrmInforOperationAuto_SXGA
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforOperationAuto_SXGA));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.Panel_DrawMap = new System.Windows.Forms.Panel();
            this.ucRoundedPanel1 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.tb_LOT_CODE_INPUT = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_AddStatus = new System.Windows.Forms.Button();
            this.lb_Marking_Code_data = new System.Windows.Forms.Label();
            this.lb_JIG_Code_Data = new System.Windows.Forms.Label();
            this.lb_Lot_Code_Data = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DGV_ProcessInfo = new System.Windows.Forms.DataGridView();
            this.ucRoundedPanel2 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.ucRoundedPanel3 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.btn_AddTimeStatus = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnInitailize = new System.Windows.Forms.Button();
            this.btnRest = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lb_Marking_GUIDE_BAR_CODE = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ProcessInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Panel_DrawMap, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ucRoundedPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_LOT_CODE_INPUT, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 820F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(709, 888);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(200, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 38);
            this.label4.TabIndex = 102;
            this.label4.Text = "LOT NO ( Input )";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_DrawMap
            // 
            this.Panel_DrawMap.BackColor = System.Drawing.Color.White;
            this.Panel_DrawMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.Panel_DrawMap, 3);
            this.Panel_DrawMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_DrawMap.Location = new System.Drawing.Point(0, 38);
            this.Panel_DrawMap.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_DrawMap.Name = "Panel_DrawMap";
            this.tableLayoutPanel1.SetRowSpan(this.Panel_DrawMap, 2);
            this.Panel_DrawMap.Size = new System.Drawing.Size(709, 850);
            this.Panel_DrawMap.TabIndex = 12;
            this.Panel_DrawMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel_DrawMap_MouseClick);
            this.Panel_DrawMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Panel_DrawMap_MouseDoubleClick);
            this.Panel_DrawMap.MouseLeave += new System.EventHandler(this.Panel_DrawMap_MouseLeave);
            this.Panel_DrawMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_DrawMap_MouseMove);
            // 
            // ucRoundedPanel1
            // 
            this.ucRoundedPanel1.BorderRadius = 32;
            this.ucRoundedPanel1.BorderSize = 1;
            this.ucRoundedPanel1.Caption = "JIG INFORMATION";
            this.ucRoundedPanel1.clBegin = System.Drawing.Color.CornflowerBlue;
            this.ucRoundedPanel1.clBorder = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel1.clEnd = System.Drawing.Color.RoyalBlue;
            this.ucRoundedPanel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel1.ForeColor = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel1.Location = new System.Drawing.Point(3, 3);
            this.ucRoundedPanel1.Name = "ucRoundedPanel1";
            this.ucRoundedPanel1.Size = new System.Drawing.Size(194, 32);
            this.ucRoundedPanel1.TabIndex = 11;
            this.ucRoundedPanel1.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel1.TextAlignVertical = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel1.Click += new System.EventHandler(this.ucRoundedPanel1_Click);
            // 
            // tb_LOT_CODE_INPUT
            // 
            this.tb_LOT_CODE_INPUT.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tb_LOT_CODE_INPUT.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_LOT_CODE_INPUT.Location = new System.Drawing.Point(338, 0);
            this.tb_LOT_CODE_INPUT.Margin = new System.Windows.Forms.Padding(0);
            this.tb_LOT_CODE_INPUT.Multiline = true;
            this.tb_LOT_CODE_INPUT.Name = "tb_LOT_CODE_INPUT";
            this.tb_LOT_CODE_INPUT.ReadOnly = true;
            this.tb_LOT_CODE_INPUT.Size = new System.Drawing.Size(371, 38);
            this.tb_LOT_CODE_INPUT.TabIndex = 103;
            this.tb_LOT_CODE_INPUT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lb_LotNo_Data_Input_MouseDoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.btn_AddStatus, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lb_Marking_Code_data, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.lb_JIG_Code_Data, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.lb_Lot_Code_Data, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.DGV_ProcessInfo, 3, 8);
            this.tableLayoutPanel2.Controls.Add(this.ucRoundedPanel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lb_Marking_GUIDE_BAR_CODE, 2, 7);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(716, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(542, 827);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btn_AddStatus
            // 
            this.btn_AddStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.btn_AddStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_AddStatus.FlatAppearance.BorderSize = 0;
            this.btn_AddStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btn_AddStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btn_AddStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddStatus.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddStatus.ForeColor = System.Drawing.Color.White;
            this.btn_AddStatus.Location = new System.Drawing.Point(381, 3);
            this.btn_AddStatus.Name = "btn_AddStatus";
            this.btn_AddStatus.Size = new System.Drawing.Size(158, 29);
            this.btn_AddStatus.TabIndex = 107;
            this.btn_AddStatus.Tag = "WithoutCommonStyle";
            this.btn_AddStatus.Text = "Add Status >>";
            this.btn_AddStatus.UseVisualStyleBackColor = false;
            this.btn_AddStatus.Click += new System.EventHandler(this.btn_AddStatus_Click);
            // 
            // lb_Marking_Code_data
            // 
            this.lb_Marking_Code_data.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel2.SetColumnSpan(this.lb_Marking_Code_data, 2);
            this.lb_Marking_Code_data.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Marking_Code_data.ForeColor = System.Drawing.Color.Black;
            this.lb_Marking_Code_data.Location = new System.Drawing.Point(216, 99);
            this.lb_Marking_Code_data.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Marking_Code_data.Name = "lb_Marking_Code_data";
            this.lb_Marking_Code_data.Size = new System.Drawing.Size(326, 30);
            this.lb_Marking_Code_data.TabIndex = 106;
            this.lb_Marking_Code_data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_JIG_Code_Data
            // 
            this.lb_JIG_Code_Data.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel2.SetColumnSpan(this.lb_JIG_Code_Data, 2);
            this.lb_JIG_Code_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_JIG_Code_Data.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_JIG_Code_Data.ForeColor = System.Drawing.Color.Black;
            this.lb_JIG_Code_Data.Location = new System.Drawing.Point(216, 67);
            this.lb_JIG_Code_Data.Margin = new System.Windows.Forms.Padding(0);
            this.lb_JIG_Code_Data.Name = "lb_JIG_Code_Data";
            this.lb_JIG_Code_Data.Size = new System.Drawing.Size(326, 30);
            this.lb_JIG_Code_Data.TabIndex = 105;
            this.lb_JIG_Code_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Lot_Code_Data
            // 
            this.lb_Lot_Code_Data.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel2.SetColumnSpan(this.lb_Lot_Code_Data, 2);
            this.lb_Lot_Code_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Lot_Code_Data.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Lot_Code_Data.ForeColor = System.Drawing.Color.Black;
            this.lb_Lot_Code_Data.Location = new System.Drawing.Point(216, 35);
            this.lb_Lot_Code_Data.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Lot_Code_Data.Name = "lb_Lot_Code_Data";
            this.lb_Lot_Code_Data.Size = new System.Drawing.Size(326, 30);
            this.lb_Lot_Code_Data.TabIndex = 104;
            this.lb_Lot_Code_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tableLayoutPanel2.SetColumnSpan(this.label3, 2);
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 30);
            this.label3.TabIndex = 103;
            this.label3.Text = "MARKING DATA ( MES )";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tableLayoutPanel2.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 30);
            this.label1.TabIndex = 102;
            this.label1.Text = "JIG NO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 30);
            this.label2.TabIndex = 101;
            this.label2.Text = "LOT NO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DGV_ProcessInfo
            // 
            this.DGV_ProcessInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel2.SetColumnSpan(this.DGV_ProcessInfo, 4);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_ProcessInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_ProcessInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_ProcessInfo.GridColor = System.Drawing.Color.SteelBlue;
            this.DGV_ProcessInfo.Location = new System.Drawing.Point(0, 163);
            this.DGV_ProcessInfo.Margin = new System.Windows.Forms.Padding(0);
            this.DGV_ProcessInfo.Name = "DGV_ProcessInfo";
            this.DGV_ProcessInfo.RowTemplate.Height = 23;
            this.DGV_ProcessInfo.Size = new System.Drawing.Size(542, 664);
            this.DGV_ProcessInfo.TabIndex = 97;
            // 
            // ucRoundedPanel2
            // 
            this.ucRoundedPanel2.BorderRadius = 32;
            this.ucRoundedPanel2.BorderSize = 1;
            this.ucRoundedPanel2.Caption = "Product Information";
            this.ucRoundedPanel2.clBegin = System.Drawing.Color.CornflowerBlue;
            this.ucRoundedPanel2.clBorder = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel2.clEnd = System.Drawing.Color.RoyalBlue;
            this.tableLayoutPanel2.SetColumnSpan(this.ucRoundedPanel2, 2);
            this.ucRoundedPanel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel2.ForeColor = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel2.Location = new System.Drawing.Point(3, 3);
            this.ucRoundedPanel2.Name = "ucRoundedPanel2";
            this.ucRoundedPanel2.Size = new System.Drawing.Size(210, 29);
            this.ucRoundedPanel2.TabIndex = 12;
            this.ucRoundedPanel2.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel2.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tableLayoutPanel2.SetColumnSpan(this.label5, 2);
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 131);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 30);
            this.label5.TabIndex = 103;
            this.label5.Text = "GUIDE BAR CODE( MES )";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucRoundedPanel3
            // 
            this.ucRoundedPanel3.BorderRadius = 32;
            this.ucRoundedPanel3.BorderSize = 1;
            this.ucRoundedPanel3.Caption = "UPTIME";
            this.ucRoundedPanel3.clBegin = System.Drawing.Color.CornflowerBlue;
            this.ucRoundedPanel3.clBorder = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel3.clEnd = System.Drawing.Color.RoyalBlue;
            this.ucRoundedPanel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel3.ForeColor = System.Drawing.Color.AliceBlue;
            this.ucRoundedPanel3.Location = new System.Drawing.Point(3, 3);
            this.ucRoundedPanel3.Name = "ucRoundedPanel3";
            this.ucRoundedPanel3.Size = new System.Drawing.Size(217, 32);
            this.ucRoundedPanel3.TabIndex = 10;
            this.ucRoundedPanel3.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel3.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // btn_AddTimeStatus
            // 
            this.btn_AddTimeStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.btn_AddTimeStatus.FlatAppearance.BorderSize = 0;
            this.btn_AddTimeStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btn_AddTimeStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btn_AddTimeStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddTimeStatus.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddTimeStatus.ForeColor = System.Drawing.Color.White;
            this.btn_AddTimeStatus.Location = new System.Drawing.Point(1143, 843);
            this.btn_AddTimeStatus.Name = "btn_AddTimeStatus";
            this.btn_AddTimeStatus.Size = new System.Drawing.Size(112, 59);
            this.btn_AddTimeStatus.TabIndex = 110;
            this.btn_AddTimeStatus.Tag = "WithoutCommonStyle";
            this.btn_AddTimeStatus.Text = "Add Time Status >>";
            this.btn_AddTimeStatus.UseVisualStyleBackColor = false;
            this.btn_AddTimeStatus.Click += new System.EventHandler(this.btn_AddTimeStatus_Click);
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.btnReject.FlatAppearance.BorderSize = 0;
            this.btnReject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnReject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReject.ForeColor = System.Drawing.Color.White;
            this.btnReject.Image = ((System.Drawing.Image)(resources.GetObject("btnReject.Image")));
            this.btnReject.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReject.Location = new System.Drawing.Point(971, 843);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(79, 59);
            this.btnReject.TabIndex = 101;
            this.btnReject.Tag = "WithoutCommonStyle";
            this.btnReject.Text = "Reject";
            this.btnReject.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReject.UseVisualStyleBackColor = false;
            // 
            // btnInitailize
            // 
            this.btnInitailize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.btnInitailize.FlatAppearance.BorderSize = 0;
            this.btnInitailize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnInitailize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnInitailize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitailize.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitailize.ForeColor = System.Drawing.Color.White;
            this.btnInitailize.Image = ((System.Drawing.Image)(resources.GetObject("btnInitailize.Image")));
            this.btnInitailize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInitailize.Location = new System.Drawing.Point(1058, 843);
            this.btnInitailize.Name = "btnInitailize";
            this.btnInitailize.Size = new System.Drawing.Size(79, 59);
            this.btnInitailize.TabIndex = 97;
            this.btnInitailize.Tag = "WithoutCommonStyle";
            this.btnInitailize.Text = "Init";
            this.btnInitailize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInitailize.UseVisualStyleBackColor = false;
            // 
            // btnRest
            // 
            this.btnRest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.btnRest.FlatAppearance.BorderSize = 0;
            this.btnRest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRest.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRest.ForeColor = System.Drawing.Color.White;
            this.btnRest.Image = ((System.Drawing.Image)(resources.GetObject("btnRest.Image")));
            this.btnRest.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRest.Location = new System.Drawing.Point(886, 843);
            this.btnRest.Name = "btnRest";
            this.btnRest.Size = new System.Drawing.Size(79, 59);
            this.btnRest.TabIndex = 98;
            this.btnRest.Tag = "WithoutCommonStyle";
            this.btnRest.Text = "Reset";
            this.btnRest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRest.UseVisualStyleBackColor = false;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStop.Location = new System.Drawing.Point(801, 843);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(79, 59);
            this.btnStop.TabIndex = 99;
            this.btnStop.Tag = "WithoutCommonStyle";
            this.btnStop.Text = "Stop";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStop.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(230)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStart.Location = new System.Drawing.Point(716, 843);
            this.btnStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(79, 59);
            this.btnStart.TabIndex = 100;
            this.btnStart.Tag = "WithoutCommonStyle";
            this.btnStart.Text = "Start";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.UseVisualStyleBackColor = false;
            // 
            // lb_Marking_GUIDE_BAR_CODE
            // 
            this.lb_Marking_GUIDE_BAR_CODE.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel2.SetColumnSpan(this.lb_Marking_GUIDE_BAR_CODE, 2);
            this.lb_Marking_GUIDE_BAR_CODE.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Marking_GUIDE_BAR_CODE.ForeColor = System.Drawing.Color.Black;
            this.lb_Marking_GUIDE_BAR_CODE.Location = new System.Drawing.Point(216, 131);
            this.lb_Marking_GUIDE_BAR_CODE.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Marking_GUIDE_BAR_CODE.Name = "lb_Marking_GUIDE_BAR_CODE";
            this.lb_Marking_GUIDE_BAR_CODE.Size = new System.Drawing.Size(326, 30);
            this.lb_Marking_GUIDE_BAR_CODE.TabIndex = 106;
            this.lb_Marking_GUIDE_BAR_CODE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmInforOperationAuto_SXGA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1262, 908);
            this.Controls.Add(this.btn_AddTimeStatus);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnInitailize);
            this.Controls.Add(this.btnRest);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInforOperationAuto_SXGA";
            this.Tag = "FORM_ID_INFOR_OPERATION_AUTO";
            this.Text = "FrmInforOperationAuto";
            this.Load += new System.EventHandler(this.FrmInforOperationAuto_SXGA_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmInforOperationAuto_SXGA_VisibleChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ProcessInfo)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnInitailize;
		private System.Windows.Forms.Button btnRest;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnStart;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel3;
				private GUI.UserControls.ucRoundedPanel ucRoundedPanel1;
				private System.Windows.Forms.DataGridView DGV_ProcessInfo;
				private GUI.UserControls.ucRoundedPanel ucRoundedPanel2;
				private System.Windows.Forms.Label label3;
				private System.Windows.Forms.Label label1;
				private System.Windows.Forms.Label label2;
				private System.Windows.Forms.Panel Panel_DrawMap;
				private System.Windows.Forms.Label lb_Marking_Code_data;
				private System.Windows.Forms.Label lb_JIG_Code_Data;
				private System.Windows.Forms.Label lb_Lot_Code_Data;
				private System.Windows.Forms.TextBox tb_LOT_CODE_INPUT;
				private System.Windows.Forms.Button btnReject;
				private System.Windows.Forms.Button btn_AddStatus;
				private System.Windows.Forms.Button btn_AddTimeStatus;
				private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_Marking_GUIDE_BAR_CODE;
    }
}