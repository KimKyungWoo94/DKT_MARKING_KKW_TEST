namespace EzIna
{
	partial class FrmMain
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panelFrmTitleBar = new System.Windows.Forms.Panel();
            this.btn_Title_Bar_Logger_MC = new System.Windows.Forms.Button();
            this.btn_Title_Bar_Information = new System.Windows.Forms.Button();
            this.btn_Title_Bar_Logger_SW = new System.Windows.Forms.Button();
            this.btn_Frm_Minimize = new System.Windows.Forms.Button();
            this.btn_Frm_Close = new System.Windows.Forms.Button();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.btn_Frm_Maximize = new System.Windows.Forms.Button();
            this.btn_Frm_Normalize = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_MES = new System.Windows.Forms.Label();
            this.ucRoundedPanel_Axis3 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.lbl_TaskStatus_04 = new System.Windows.Forms.Label();
            this.lbl_TaskStatus_03 = new System.Windows.Forms.Label();
            this.lbl_TaskStatus_02 = new System.Windows.Forms.Label();
            this.lbl_TaskStatus_01 = new System.Windows.Forms.Label();
            this.lbl_RunMode = new System.Windows.Forms.Label();
            this.lbl_Initialized = new System.Windows.Forms.Label();
            this.ucRoundedPanel_Axis5 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.ucRoundedPanel_Axis4 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.ucRoundedPanel_Axis2 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.ucRoundedPanel_Axis1 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.ucRoundedPanel_Axis0 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.menuStrip_FrmMain = new System.Windows.Forms.MenuStrip();
            this.operationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Operation_Auto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Operation_SemiAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Operation_VisionPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.RecipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Recipe_Recipe = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Recipe_Project = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Manual_Marking = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Manual_Vision = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Manual_Laser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Manual_Scanner = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Manual_Motion = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Init_Process = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Teaching = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Motion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Vision = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Laser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Scanner = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Powermeter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Attenuator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_IO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setup_Cylinder = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Log_EventLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Log_WorkLog = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_mainview = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelFrmTitleBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip_FrmMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFrmTitleBar
            // 
            this.panelFrmTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelFrmTitleBar.Controls.Add(this.btn_Title_Bar_Logger_MC);
            this.panelFrmTitleBar.Controls.Add(this.btn_Title_Bar_Information);
            this.panelFrmTitleBar.Controls.Add(this.btn_Title_Bar_Logger_SW);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Minimize);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Close);
            this.panelFrmTitleBar.Controls.Add(this.lbl_Version);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Maximize);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Normalize);
            this.panelFrmTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFrmTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelFrmTitleBar.Name = "panelFrmTitleBar";
            this.panelFrmTitleBar.Size = new System.Drawing.Size(1280, 32);
            this.panelFrmTitleBar.TabIndex = 3;
            this.panelFrmTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFrmTitleBar_MouseDown);
            // 
            // btn_Title_Bar_Logger_MC
            // 
            this.btn_Title_Bar_Logger_MC.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Title_Bar_Logger_MC.FlatAppearance.BorderSize = 0;
            this.btn_Title_Bar_Logger_MC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Title_Bar_Logger_MC.Image = ((System.Drawing.Image)(resources.GetObject("btn_Title_Bar_Logger_MC.Image")));
            this.btn_Title_Bar_Logger_MC.Location = new System.Drawing.Point(1021, 1);
            this.btn_Title_Bar_Logger_MC.Name = "btn_Title_Bar_Logger_MC";
            this.btn_Title_Bar_Logger_MC.Size = new System.Drawing.Size(28, 28);
            this.btn_Title_Bar_Logger_MC.TabIndex = 5;
            this.btn_Title_Bar_Logger_MC.UseVisualStyleBackColor = true;
            this.btn_Title_Bar_Logger_MC.Click += new System.EventHandler(this.btn_Title_Bar_Logger_MC_Click);
            // 
            // btn_Title_Bar_Information
            // 
            this.btn_Title_Bar_Information.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Title_Bar_Information.FlatAppearance.BorderSize = 0;
            this.btn_Title_Bar_Information.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Title_Bar_Information.Image = ((System.Drawing.Image)(resources.GetObject("btn_Title_Bar_Information.Image")));
            this.btn_Title_Bar_Information.Location = new System.Drawing.Point(1113, 1);
            this.btn_Title_Bar_Information.Name = "btn_Title_Bar_Information";
            this.btn_Title_Bar_Information.Size = new System.Drawing.Size(28, 28);
            this.btn_Title_Bar_Information.TabIndex = 5;
            this.btn_Title_Bar_Information.UseVisualStyleBackColor = true;
            this.btn_Title_Bar_Information.Click += new System.EventHandler(this.btn_Frm_Minimize_Click);
            // 
            // btn_Title_Bar_Logger_SW
            // 
            this.btn_Title_Bar_Logger_SW.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Title_Bar_Logger_SW.FlatAppearance.BorderSize = 0;
            this.btn_Title_Bar_Logger_SW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Title_Bar_Logger_SW.Image = ((System.Drawing.Image)(resources.GetObject("btn_Title_Bar_Logger_SW.Image")));
            this.btn_Title_Bar_Logger_SW.Location = new System.Drawing.Point(1067, 1);
            this.btn_Title_Bar_Logger_SW.Name = "btn_Title_Bar_Logger_SW";
            this.btn_Title_Bar_Logger_SW.Size = new System.Drawing.Size(28, 28);
            this.btn_Title_Bar_Logger_SW.TabIndex = 5;
            this.btn_Title_Bar_Logger_SW.UseVisualStyleBackColor = true;
            this.btn_Title_Bar_Logger_SW.Click += new System.EventHandler(this.btn_Title_Bar_Logger_SW_Click);
            // 
            // btn_Frm_Minimize
            // 
            this.btn_Frm_Minimize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Minimize.AutoSize = true;
            this.btn_Frm_Minimize.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Minimize.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Minimize.Image")));
            this.btn_Frm_Minimize.Location = new System.Drawing.Point(1174, 1);
            this.btn_Frm_Minimize.Name = "btn_Frm_Minimize";
            this.btn_Frm_Minimize.Size = new System.Drawing.Size(30, 30);
            this.btn_Frm_Minimize.TabIndex = 5;
            this.btn_Frm_Minimize.UseVisualStyleBackColor = true;
            this.btn_Frm_Minimize.Click += new System.EventHandler(this.btn_Frm_Minimize_Click);
            // 
            // btn_Frm_Close
            // 
            this.btn_Frm_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Close.AutoSize = true;
            this.btn_Frm_Close.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Close.Image")));
            this.btn_Frm_Close.Location = new System.Drawing.Point(1246, 1);
            this.btn_Frm_Close.Name = "btn_Frm_Close";
            this.btn_Frm_Close.Size = new System.Drawing.Size(30, 30);
            this.btn_Frm_Close.TabIndex = 4;
            this.btn_Frm_Close.UseVisualStyleBackColor = true;
            this.btn_Frm_Close.Click += new System.EventHandler(this.btn_Frm_Close_Click);
            // 
            // lbl_Version
            // 
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Version.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Version.ForeColor = System.Drawing.Color.White;
            this.lbl_Version.Location = new System.Drawing.Point(3, 4);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(102, 19);
            this.lbl_Version.TabIndex = 3;
            this.lbl_Version.Text = "EzInaFlex2.0";
            this.lbl_Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Frm_Maximize
            // 
            this.btn_Frm_Maximize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Maximize.AutoSize = true;
            this.btn_Frm_Maximize.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Maximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Maximize.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Maximize.Image")));
            this.btn_Frm_Maximize.Location = new System.Drawing.Point(1210, 1);
            this.btn_Frm_Maximize.Name = "btn_Frm_Maximize";
            this.btn_Frm_Maximize.Size = new System.Drawing.Size(30, 30);
            this.btn_Frm_Maximize.TabIndex = 6;
            this.btn_Frm_Maximize.UseVisualStyleBackColor = true;
            this.btn_Frm_Maximize.Visible = false;
            this.btn_Frm_Maximize.Click += new System.EventHandler(this.btn_Frm_Maximize_Click);
            // 
            // btn_Frm_Normalize
            // 
            this.btn_Frm_Normalize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Normalize.AutoSize = true;
            this.btn_Frm_Normalize.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Normalize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Normalize.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Normalize.Image")));
            this.btn_Frm_Normalize.Location = new System.Drawing.Point(1210, 1);
            this.btn_Frm_Normalize.Name = "btn_Frm_Normalize";
            this.btn_Frm_Normalize.Size = new System.Drawing.Size(30, 31);
            this.btn_Frm_Normalize.TabIndex = 7;
            this.btn_Frm_Normalize.UseVisualStyleBackColor = true;
            this.btn_Frm_Normalize.Visible = false;
            this.btn_Frm_Normalize.Click += new System.EventHandler(this.btn_Frm_Normalize_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.lbl_MES);
            this.panel1.Controls.Add(this.ucRoundedPanel_Axis3);
            this.panel1.Controls.Add(this.lbl_TaskStatus_04);
            this.panel1.Controls.Add(this.lbl_TaskStatus_03);
            this.panel1.Controls.Add(this.lbl_TaskStatus_02);
            this.panel1.Controls.Add(this.lbl_TaskStatus_01);
            this.panel1.Controls.Add(this.lbl_RunMode);
            this.panel1.Controls.Add(this.lbl_Initialized);
            this.panel1.Controls.Add(this.ucRoundedPanel_Axis5);
            this.panel1.Controls.Add(this.ucRoundedPanel_Axis4);
            this.panel1.Controls.Add(this.ucRoundedPanel_Axis2);
            this.panel1.Controls.Add(this.ucRoundedPanel_Axis1);
            this.panel1.Controls.Add(this.ucRoundedPanel_Axis0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 992);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 32);
            this.panel1.TabIndex = 12;
            // 
            // lbl_MES
            // 
            this.lbl_MES.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_MES.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MES.ForeColor = System.Drawing.Color.White;
            this.lbl_MES.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_MES.ImageIndex = 0;
            this.lbl_MES.Location = new System.Drawing.Point(934, 5);
            this.lbl_MES.Name = "lbl_MES";
            this.lbl_MES.Size = new System.Drawing.Size(52, 25);
            this.lbl_MES.TabIndex = 67;
            this.lbl_MES.Text = "MES";
            this.lbl_MES.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucRoundedPanel_Axis3
            // 
            this.ucRoundedPanel_Axis3.BorderRadius = 5;
            this.ucRoundedPanel_Axis3.BorderSize = 1;
            this.ucRoundedPanel_Axis3.Caption = "EzIna";
            this.ucRoundedPanel_Axis3.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis3.clBorder = System.Drawing.Color.Black;
            this.ucRoundedPanel_Axis3.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel_Axis3.ForeColor = System.Drawing.Color.Lime;
            this.ucRoundedPanel_Axis3.Location = new System.Drawing.Point(408, 4);
            this.ucRoundedPanel_Axis3.Name = "ucRoundedPanel_Axis3";
            this.ucRoundedPanel_Axis3.Size = new System.Drawing.Size(135, 25);
            this.ucRoundedPanel_Axis3.TabIndex = 66;
            this.ucRoundedPanel_Axis3.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel_Axis3.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // lbl_TaskStatus_04
            // 
            this.lbl_TaskStatus_04.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TaskStatus_04.BackColor = System.Drawing.Color.Red;
            this.lbl_TaskStatus_04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TaskStatus_04.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TaskStatus_04.Location = new System.Drawing.Point(893, 4);
            this.lbl_TaskStatus_04.Name = "lbl_TaskStatus_04";
            this.lbl_TaskStatus_04.Size = new System.Drawing.Size(23, 25);
            this.lbl_TaskStatus_04.TabIndex = 65;
            this.lbl_TaskStatus_04.Text = "T4";
            this.lbl_TaskStatus_04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_TaskStatus_04.Visible = false;
            // 
            // lbl_TaskStatus_03
            // 
            this.lbl_TaskStatus_03.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TaskStatus_03.BackColor = System.Drawing.Color.Red;
            this.lbl_TaskStatus_03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TaskStatus_03.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TaskStatus_03.Location = new System.Drawing.Point(867, 4);
            this.lbl_TaskStatus_03.Name = "lbl_TaskStatus_03";
            this.lbl_TaskStatus_03.Size = new System.Drawing.Size(23, 25);
            this.lbl_TaskStatus_03.TabIndex = 65;
            this.lbl_TaskStatus_03.Text = "T3";
            this.lbl_TaskStatus_03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_TaskStatus_03.Visible = false;
            // 
            // lbl_TaskStatus_02
            // 
            this.lbl_TaskStatus_02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TaskStatus_02.BackColor = System.Drawing.Color.Red;
            this.lbl_TaskStatus_02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TaskStatus_02.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TaskStatus_02.Location = new System.Drawing.Point(843, 4);
            this.lbl_TaskStatus_02.Name = "lbl_TaskStatus_02";
            this.lbl_TaskStatus_02.Size = new System.Drawing.Size(23, 25);
            this.lbl_TaskStatus_02.TabIndex = 65;
            this.lbl_TaskStatus_02.Text = "T2";
            this.lbl_TaskStatus_02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TaskStatus_01
            // 
            this.lbl_TaskStatus_01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TaskStatus_01.BackColor = System.Drawing.Color.Red;
            this.lbl_TaskStatus_01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TaskStatus_01.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TaskStatus_01.Location = new System.Drawing.Point(819, 4);
            this.lbl_TaskStatus_01.Name = "lbl_TaskStatus_01";
            this.lbl_TaskStatus_01.Size = new System.Drawing.Size(23, 25);
            this.lbl_TaskStatus_01.TabIndex = 65;
            this.lbl_TaskStatus_01.Text = "T1";
            this.lbl_TaskStatus_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RunMode
            // 
            this.lbl_RunMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_RunMode.BackColor = System.Drawing.Color.Black;
            this.lbl_RunMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_RunMode.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RunMode.ForeColor = System.Drawing.Color.Lime;
            this.lbl_RunMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_RunMode.ImageIndex = 0;
            this.lbl_RunMode.Location = new System.Drawing.Point(992, 4);
            this.lbl_RunMode.Name = "lbl_RunMode";
            this.lbl_RunMode.Size = new System.Drawing.Size(190, 25);
            this.lbl_RunMode.TabIndex = 64;
            this.lbl_RunMode.Text = "STOP";
            this.lbl_RunMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Initialized
            // 
            this.lbl_Initialized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Initialized.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Initialized.ForeColor = System.Drawing.Color.White;
            this.lbl_Initialized.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Initialized.ImageIndex = 0;
            this.lbl_Initialized.Location = new System.Drawing.Point(1183, 4);
            this.lbl_Initialized.Name = "lbl_Initialized";
            this.lbl_Initialized.Size = new System.Drawing.Size(93, 25);
            this.lbl_Initialized.TabIndex = 64;
            this.lbl_Initialized.Text = "Initailized";
            this.lbl_Initialized.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucRoundedPanel_Axis5
            // 
            this.ucRoundedPanel_Axis5.BorderRadius = 5;
            this.ucRoundedPanel_Axis5.BorderSize = 1;
            this.ucRoundedPanel_Axis5.Caption = "EzIna";
            this.ucRoundedPanel_Axis5.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis5.clBorder = System.Drawing.Color.Black;
            this.ucRoundedPanel_Axis5.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel_Axis5.ForeColor = System.Drawing.Color.Lime;
            this.ucRoundedPanel_Axis5.Location = new System.Drawing.Point(678, 4);
            this.ucRoundedPanel_Axis5.Name = "ucRoundedPanel_Axis5";
            this.ucRoundedPanel_Axis5.Size = new System.Drawing.Size(135, 25);
            this.ucRoundedPanel_Axis5.TabIndex = 0;
            this.ucRoundedPanel_Axis5.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel_Axis5.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // ucRoundedPanel_Axis4
            // 
            this.ucRoundedPanel_Axis4.BorderRadius = 5;
            this.ucRoundedPanel_Axis4.BorderSize = 1;
            this.ucRoundedPanel_Axis4.Caption = "EzIna";
            this.ucRoundedPanel_Axis4.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis4.clBorder = System.Drawing.Color.Black;
            this.ucRoundedPanel_Axis4.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel_Axis4.ForeColor = System.Drawing.Color.Lime;
            this.ucRoundedPanel_Axis4.Location = new System.Drawing.Point(543, 4);
            this.ucRoundedPanel_Axis4.Name = "ucRoundedPanel_Axis4";
            this.ucRoundedPanel_Axis4.Size = new System.Drawing.Size(135, 25);
            this.ucRoundedPanel_Axis4.TabIndex = 0;
            this.ucRoundedPanel_Axis4.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel_Axis4.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // ucRoundedPanel_Axis2
            // 
            this.ucRoundedPanel_Axis2.BorderRadius = 5;
            this.ucRoundedPanel_Axis2.BorderSize = 1;
            this.ucRoundedPanel_Axis2.Caption = "EzIna";
            this.ucRoundedPanel_Axis2.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis2.clBorder = System.Drawing.Color.Black;
            this.ucRoundedPanel_Axis2.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel_Axis2.ForeColor = System.Drawing.Color.Lime;
            this.ucRoundedPanel_Axis2.Location = new System.Drawing.Point(273, 4);
            this.ucRoundedPanel_Axis2.Name = "ucRoundedPanel_Axis2";
            this.ucRoundedPanel_Axis2.Size = new System.Drawing.Size(135, 25);
            this.ucRoundedPanel_Axis2.TabIndex = 0;
            this.ucRoundedPanel_Axis2.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel_Axis2.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // ucRoundedPanel_Axis1
            // 
            this.ucRoundedPanel_Axis1.BorderRadius = 5;
            this.ucRoundedPanel_Axis1.BorderSize = 1;
            this.ucRoundedPanel_Axis1.Caption = "EzIna";
            this.ucRoundedPanel_Axis1.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis1.clBorder = System.Drawing.Color.Black;
            this.ucRoundedPanel_Axis1.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel_Axis1.ForeColor = System.Drawing.Color.Lime;
            this.ucRoundedPanel_Axis1.Location = new System.Drawing.Point(138, 4);
            this.ucRoundedPanel_Axis1.Name = "ucRoundedPanel_Axis1";
            this.ucRoundedPanel_Axis1.Size = new System.Drawing.Size(135, 25);
            this.ucRoundedPanel_Axis1.TabIndex = 0;
            this.ucRoundedPanel_Axis1.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel_Axis1.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // ucRoundedPanel_Axis0
            // 
            this.ucRoundedPanel_Axis0.BorderRadius = 5;
            this.ucRoundedPanel_Axis0.BorderSize = 1;
            this.ucRoundedPanel_Axis0.Caption = "EzIna";
            this.ucRoundedPanel_Axis0.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis0.clBorder = System.Drawing.Color.Black;
            this.ucRoundedPanel_Axis0.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ucRoundedPanel_Axis0.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundedPanel_Axis0.ForeColor = System.Drawing.Color.Lime;
            this.ucRoundedPanel_Axis0.Location = new System.Drawing.Point(3, 4);
            this.ucRoundedPanel_Axis0.Name = "ucRoundedPanel_Axis0";
            this.ucRoundedPanel_Axis0.Size = new System.Drawing.Size(135, 25);
            this.ucRoundedPanel_Axis0.TabIndex = 0;
            this.ucRoundedPanel_Axis0.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel_Axis0.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // menuStrip_FrmMain
            // 
            this.menuStrip_FrmMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.menuStrip_FrmMain.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip_FrmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operationToolStripMenuItem,
            this.RecipeToolStripMenuItem,
            this.manualToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.logToolStripMenuItem});
            this.menuStrip_FrmMain.Location = new System.Drawing.Point(0, 32);
            this.menuStrip_FrmMain.Name = "menuStrip_FrmMain";
            this.menuStrip_FrmMain.Size = new System.Drawing.Size(1280, 52);
            this.menuStrip_FrmMain.TabIndex = 13;
            this.menuStrip_FrmMain.Text = "menuStrip1";
            // 
            // operationToolStripMenuItem
            // 
            this.operationToolStripMenuItem.AutoSize = false;
            this.operationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Operation_Auto,
            this.toolStripMenuItem_Operation_SemiAuto,
            this.toolStripMenuItem_Operation_VisionPanel});
            this.operationToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.operationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("operationToolStripMenuItem.Image")));
            this.operationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.operationToolStripMenuItem.Name = "operationToolStripMenuItem";
            this.operationToolStripMenuItem.Size = new System.Drawing.Size(130, 48);
            this.operationToolStripMenuItem.Tag = "";
            this.operationToolStripMenuItem.Text = "&Operation";
            // 
            // toolStripMenuItem_Operation_Auto
            // 
            this.toolStripMenuItem_Operation_Auto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Operation_Auto.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Operation_Auto.Name = "toolStripMenuItem_Operation_Auto";
            this.toolStripMenuItem_Operation_Auto.Size = new System.Drawing.Size(156, 26);
            this.toolStripMenuItem_Operation_Auto.Tag = "FORM_ID_INFOR_OPERATION_AUTO";
            this.toolStripMenuItem_Operation_Auto.Text = "Auto";
            // 
            // toolStripMenuItem_Operation_SemiAuto
            // 
            this.toolStripMenuItem_Operation_SemiAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Operation_SemiAuto.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Operation_SemiAuto.Name = "toolStripMenuItem_Operation_SemiAuto";
            this.toolStripMenuItem_Operation_SemiAuto.Size = new System.Drawing.Size(156, 26);
            this.toolStripMenuItem_Operation_SemiAuto.Tag = "FORM_ID_INFOR_OPERATION_SEMI";
            this.toolStripMenuItem_Operation_SemiAuto.Text = "SemiAuto";
            // 
            // toolStripMenuItem_Operation_VisionPanel
            // 
            this.toolStripMenuItem_Operation_VisionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Operation_VisionPanel.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Operation_VisionPanel.Name = "toolStripMenuItem_Operation_VisionPanel";
            this.toolStripMenuItem_Operation_VisionPanel.Size = new System.Drawing.Size(156, 26);
            this.toolStripMenuItem_Operation_VisionPanel.Text = "Vision";
            // 
            // RecipeToolStripMenuItem
            // 
            this.RecipeToolStripMenuItem.AutoSize = false;
            this.RecipeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Recipe_Recipe,
            this.toolStripMenuItem_Recipe_Project});
            this.RecipeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.RecipeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RecipeToolStripMenuItem.Image")));
            this.RecipeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RecipeToolStripMenuItem.Name = "RecipeToolStripMenuItem";
            this.RecipeToolStripMenuItem.Size = new System.Drawing.Size(94, 48);
            this.RecipeToolStripMenuItem.Text = "&Recipe";
            // 
            // toolStripMenuItem_Recipe_Recipe
            // 
            this.toolStripMenuItem_Recipe_Recipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Recipe_Recipe.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Recipe_Recipe.Name = "toolStripMenuItem_Recipe_Recipe";
            this.toolStripMenuItem_Recipe_Recipe.Size = new System.Drawing.Size(134, 26);
            this.toolStripMenuItem_Recipe_Recipe.Tag = "FORM_ID_INFOR_RECIPE";
            this.toolStripMenuItem_Recipe_Recipe.Text = "Recipe";
            // 
            // toolStripMenuItem_Recipe_Project
            // 
            this.toolStripMenuItem_Recipe_Project.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Recipe_Project.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Recipe_Project.Name = "toolStripMenuItem_Recipe_Project";
            this.toolStripMenuItem_Recipe_Project.Size = new System.Drawing.Size(134, 26);
            this.toolStripMenuItem_Recipe_Project.Tag = "FORM_ID_INFOR_RECIPE_PROJECT";
            this.toolStripMenuItem_Recipe_Project.Text = "Project";
            this.toolStripMenuItem_Recipe_Project.Visible = false;
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.AutoSize = false;
            this.manualToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.manualToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Manual_Marking,
            this.toolStripMenuItem_Manual_Vision,
            this.toolStripMenuItem_Manual_Laser,
            this.toolStripMenuItem_Manual_Scanner,
            this.toolStripMenuItem_Manual_Motion});
            this.manualToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.manualToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manualToolStripMenuItem.Image")));
            this.manualToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(110, 48);
            this.manualToolStripMenuItem.Text = "&Manual";
            // 
            // toolStripMenuItem_Manual_Marking
            // 
            this.toolStripMenuItem_Manual_Marking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Manual_Marking.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Manual_Marking.Name = "toolStripMenuItem_Manual_Marking";
            this.toolStripMenuItem_Manual_Marking.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem_Manual_Marking.Text = "&Marking";
            // 
            // toolStripMenuItem_Manual_Vision
            // 
            this.toolStripMenuItem_Manual_Vision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Manual_Vision.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Manual_Vision.Name = "toolStripMenuItem_Manual_Vision";
            this.toolStripMenuItem_Manual_Vision.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem_Manual_Vision.Text = "&Vision";
            // 
            // toolStripMenuItem_Manual_Laser
            // 
            this.toolStripMenuItem_Manual_Laser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Manual_Laser.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Manual_Laser.Name = "toolStripMenuItem_Manual_Laser";
            this.toolStripMenuItem_Manual_Laser.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem_Manual_Laser.Text = "&Laser";
            this.toolStripMenuItem_Manual_Laser.Visible = false;
            // 
            // toolStripMenuItem_Manual_Scanner
            // 
            this.toolStripMenuItem_Manual_Scanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Manual_Scanner.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Manual_Scanner.Name = "toolStripMenuItem_Manual_Scanner";
            this.toolStripMenuItem_Manual_Scanner.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem_Manual_Scanner.Text = "&Scanner";
            this.toolStripMenuItem_Manual_Scanner.Visible = false;
            // 
            // toolStripMenuItem_Manual_Motion
            // 
            this.toolStripMenuItem_Manual_Motion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Manual_Motion.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Manual_Motion.Name = "toolStripMenuItem_Manual_Motion";
            this.toolStripMenuItem_Manual_Motion.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem_Manual_Motion.Text = "&Motion";
            this.toolStripMenuItem_Manual_Motion.Visible = false;
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.AutoSize = false;
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Setup_Init_Process,
            this.toolStripMenuItem_Setup_Teaching,
            this.toolStripMenuItem_Setup_Motion,
            this.toolStripMenuItem_Setup_Vision,
            this.toolStripMenuItem_Setup_Laser,
            this.toolStripMenuItem_Setup_Scanner,
            this.toolStripMenuItem_Setup_Powermeter,
            this.toolStripMenuItem_Setup_Attenuator,
            this.toolStripMenuItem_Setup_IO,
            this.toolStripMenuItem_Setup_Cylinder});
            this.setupToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.setupToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("setupToolStripMenuItem.Image")));
            this.setupToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(94, 48);
            this.setupToolStripMenuItem.Text = "&Setup";
            // 
            // toolStripMenuItem_Setup_Init_Process
            // 
            this.toolStripMenuItem_Setup_Init_Process.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Init_Process.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Init_Process.Name = "toolStripMenuItem_Setup_Init_Process";
            this.toolStripMenuItem_Setup_Init_Process.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Init_Process.Text = "&Initial Process";
            // 
            // toolStripMenuItem_Setup_Teaching
            // 
            this.toolStripMenuItem_Setup_Teaching.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Teaching.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Teaching.Name = "toolStripMenuItem_Setup_Teaching";
            this.toolStripMenuItem_Setup_Teaching.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Teaching.Text = "&Teaching";
            this.toolStripMenuItem_Setup_Teaching.Visible = false;
            // 
            // toolStripMenuItem_Setup_Motion
            // 
            this.toolStripMenuItem_Setup_Motion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Motion.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Motion.Name = "toolStripMenuItem_Setup_Motion";
            this.toolStripMenuItem_Setup_Motion.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Motion.Text = "&Motion";
            // 
            // toolStripMenuItem_Setup_Vision
            // 
            this.toolStripMenuItem_Setup_Vision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Vision.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Vision.Name = "toolStripMenuItem_Setup_Vision";
            this.toolStripMenuItem_Setup_Vision.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Vision.Text = "&Vision";
            // 
            // toolStripMenuItem_Setup_Laser
            // 
            this.toolStripMenuItem_Setup_Laser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Laser.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Laser.Name = "toolStripMenuItem_Setup_Laser";
            this.toolStripMenuItem_Setup_Laser.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Laser.Text = "&Laser";
            // 
            // toolStripMenuItem_Setup_Scanner
            // 
            this.toolStripMenuItem_Setup_Scanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Scanner.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Scanner.Name = "toolStripMenuItem_Setup_Scanner";
            this.toolStripMenuItem_Setup_Scanner.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Scanner.Text = "&Scanner";
            // 
            // toolStripMenuItem_Setup_Powermeter
            // 
            this.toolStripMenuItem_Setup_Powermeter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Powermeter.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Powermeter.Name = "toolStripMenuItem_Setup_Powermeter";
            this.toolStripMenuItem_Setup_Powermeter.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Powermeter.Text = "&Powermeter";
            this.toolStripMenuItem_Setup_Powermeter.Visible = false;
            // 
            // toolStripMenuItem_Setup_Attenuator
            // 
            this.toolStripMenuItem_Setup_Attenuator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Attenuator.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Attenuator.Name = "toolStripMenuItem_Setup_Attenuator";
            this.toolStripMenuItem_Setup_Attenuator.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Attenuator.Text = "&Attenuator";
            this.toolStripMenuItem_Setup_Attenuator.Visible = false;
            // 
            // toolStripMenuItem_Setup_IO
            // 
            this.toolStripMenuItem_Setup_IO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_IO.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_IO.Name = "toolStripMenuItem_Setup_IO";
            this.toolStripMenuItem_Setup_IO.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_IO.Text = "&IO";
            // 
            // toolStripMenuItem_Setup_Cylinder
            // 
            this.toolStripMenuItem_Setup_Cylinder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Setup_Cylinder.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Setup_Cylinder.Name = "toolStripMenuItem_Setup_Cylinder";
            this.toolStripMenuItem_Setup_Cylinder.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItem_Setup_Cylinder.Text = "&Cylinder";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.AutoSize = false;
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Log_EventLog,
            this.toolStripMenuItem_Log_WorkLog});
            this.logToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.logToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logToolStripMenuItem.Image")));
            this.logToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(110, 48);
            this.logToolStripMenuItem.Text = "Log";
            // 
            // toolStripMenuItem_Log_EventLog
            // 
            this.toolStripMenuItem_Log_EventLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Log_EventLog.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Log_EventLog.Name = "toolStripMenuItem_Log_EventLog";
            this.toolStripMenuItem_Log_EventLog.Size = new System.Drawing.Size(158, 26);
            this.toolStripMenuItem_Log_EventLog.Tag = "FORM_ID_INFOR_LOG_EVENTLOG";
            this.toolStripMenuItem_Log_EventLog.Text = "Event Log";
            // 
            // toolStripMenuItem_Log_WorkLog
            // 
            this.toolStripMenuItem_Log_WorkLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(170)))));
            this.toolStripMenuItem_Log_WorkLog.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem_Log_WorkLog.Name = "toolStripMenuItem_Log_WorkLog";
            this.toolStripMenuItem_Log_WorkLog.Size = new System.Drawing.Size(158, 26);
            this.toolStripMenuItem_Log_WorkLog.Tag = "FORM_ID_INFOR_LOG_WORKLOG";
            this.toolStripMenuItem_Log_WorkLog.Text = "Work Log";
            // 
            // panel_mainview
            // 
            this.panel_mainview.BackColor = System.Drawing.SystemColors.Control;
            this.panel_mainview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_mainview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_mainview.Location = new System.Drawing.Point(0, 84);
            this.panel_mainview.Name = "panel_mainview";
            this.panel_mainview.Size = new System.Drawing.Size(1280, 908);
            this.panel_mainview.TabIndex = 14;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Operation.png");
            this.imageList1.Images.SetKeyName(1, "Project.png");
            this.imageList1.Images.SetKeyName(2, "Manual.png");
            this.imageList1.Images.SetKeyName(3, "Setup (2).png");
            this.imageList1.Images.SetKeyName(4, "Log.png");
            this.imageList1.Images.SetKeyName(5, "Setup.png");
            this.imageList1.Images.SetKeyName(6, "icons8-demand-30.png");
            this.imageList1.Images.SetKeyName(7, "icons8-shipping-center-32.png");
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.panel_mainview);
            this.Controls.Add(this.menuStrip_FrmMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFrmTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMain";
            this.Text = "EzInaFlex";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmMain_VisibleChanged);
            this.panelFrmTitleBar.ResumeLayout(false);
            this.panelFrmTitleBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip_FrmMain.ResumeLayout(false);
            this.menuStrip_FrmMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Button btn_Frm_Normalize;
		private System.Windows.Forms.Button btn_Frm_Maximize;
		private System.Windows.Forms.Button btn_Frm_Minimize;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Label lbl_Version;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lbl_TaskStatus_04;
		private System.Windows.Forms.Label lbl_TaskStatus_03;
		private System.Windows.Forms.Label lbl_TaskStatus_02;
		private System.Windows.Forms.Label lbl_TaskStatus_01;
		private System.Windows.Forms.Label lbl_Initialized;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel_Axis5;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel_Axis4;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel_Axis2;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel_Axis1;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel_Axis0;
		private System.Windows.Forms.MenuStrip menuStrip_FrmMain;
		private System.Windows.Forms.ToolStripMenuItem operationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RecipeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Recipe_Recipe;
		private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.Panel panel_mainview;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Operation_Auto;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Operation_SemiAuto;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Manual_Motion;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Manual_Vision;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Manual_Laser;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Manual_Scanner;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Init_Process;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Teaching;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Motion;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Vision;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Laser;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Scanner;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Powermeter;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Attenuator;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_Cylinder;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setup_IO;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button btn_Title_Bar_Logger_MC;
		private System.Windows.Forms.Button btn_Title_Bar_Information;
		private System.Windows.Forms.Button btn_Title_Bar_Logger_SW;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Log_EventLog;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Log_WorkLog;
		private System.Windows.Forms.Label lbl_RunMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Manual_Marking;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Recipe_Project;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Operation_VisionPanel;
				private GUI.UserControls.ucRoundedPanel ucRoundedPanel_Axis3;
				private System.Windows.Forms.Label lbl_MES;
		}
}

