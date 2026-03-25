namespace EzIna
{
	partial class FrmVision
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVision));
            this.panel_Vision = new System.Windows.Forms.Panel();
            this.panelFrmTitleBar = new System.Windows.Forms.Panel();
            this.btn_Frm_Normalize = new System.Windows.Forms.Button();
            this.imageList_Frm = new System.Windows.Forms.ImageList(this.components);
            this.btn_Frm_Max = new System.Windows.Forms.Button();
            this.btn_Frm_Min = new System.Windows.Forms.Button();
            this.btn_Frm_Close = new System.Windows.Forms.Button();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.btn_Frm_Minimize = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucRoundedPanel3 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.ucRoundedPanel2 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.lbl_Motor_Speed = new System.Windows.Forms.Label();
            this.trackBar_MotorSpeed = new System.Windows.Forms.TrackBar();
            this.chkbox_ByStep = new System.Windows.Forms.CheckBox();
            this.txtbox_distance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ucRoundedPanel1 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.btn_M_U_CCW = new System.Windows.Forms.Button();
            this.imageList_Direction = new System.Windows.Forms.ImageList(this.components);
            this.btn_M_U_CW = new System.Windows.Forms.Button();
            this.btn_Solder_Left = new System.Windows.Forms.Button();
            this.btn_Solder_Right = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btn_Solder_Bottom = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_EStop = new System.Windows.Forms.Button();
            this.btn_Solder_Top = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectCoarseVision = new System.Windows.Forms.Button();
            this.imageList_chkbox = new System.Windows.Forms.ImageList(this.components);
            this.btnSelectFineVision = new System.Windows.Forms.Button();
            this.ucRoundedPanel6 = new EzIna.GUI.UserControls.ucRoundedPanel();
            this.panelFrmTitleBar.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_MotorSpeed)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Vision
            // 
            this.panel_Vision.Location = new System.Drawing.Point(0, 35);
            this.panel_Vision.Name = "panel_Vision";
            this.panel_Vision.Size = new System.Drawing.Size(1024, 768);
            this.panel_Vision.TabIndex = 0;
            // 
            // panelFrmTitleBar
            // 
            this.panelFrmTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Normalize);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Max);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Min);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Close);
            this.panelFrmTitleBar.Controls.Add(this.lbl_Version);
            this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Minimize);
            this.panelFrmTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFrmTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelFrmTitleBar.Name = "panelFrmTitleBar";
            this.panelFrmTitleBar.Size = new System.Drawing.Size(1280, 32);
            this.panelFrmTitleBar.TabIndex = 2;
            this.panelFrmTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFrmTitleBar_MouseDown);
            // 
            // btn_Frm_Normalize
            // 
            this.btn_Frm_Normalize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Normalize.AutoSize = true;
            this.btn_Frm_Normalize.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Normalize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Normalize.ImageIndex = 3;
            this.btn_Frm_Normalize.ImageList = this.imageList_Frm;
            this.btn_Frm_Normalize.Location = new System.Drawing.Point(1210, 1);
            this.btn_Frm_Normalize.Name = "btn_Frm_Normalize";
            this.btn_Frm_Normalize.Size = new System.Drawing.Size(30, 31);
            this.btn_Frm_Normalize.TabIndex = 7;
            this.btn_Frm_Normalize.UseVisualStyleBackColor = true;
            this.btn_Frm_Normalize.Click += new System.EventHandler(this.btn_Frm_Normalize_Click);
            // 
            // imageList_Frm
            // 
            this.imageList_Frm.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Frm.ImageStream")));
            this.imageList_Frm.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Frm.Images.SetKeyName(0, "Frm_Close_24.png");
            this.imageList_Frm.Images.SetKeyName(1, "Frm_Maximize_24.png");
            this.imageList_Frm.Images.SetKeyName(2, "Frm_Minmize_24.png");
            this.imageList_Frm.Images.SetKeyName(3, "Frm_Normal_24.png");
            // 
            // btn_Frm_Max
            // 
            this.btn_Frm_Max.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Max.AutoSize = true;
            this.btn_Frm_Max.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Max.ImageIndex = 1;
            this.btn_Frm_Max.ImageList = this.imageList_Frm;
            this.btn_Frm_Max.Location = new System.Drawing.Point(1210, 1);
            this.btn_Frm_Max.Name = "btn_Frm_Max";
            this.btn_Frm_Max.Size = new System.Drawing.Size(30, 30);
            this.btn_Frm_Max.TabIndex = 6;
            this.btn_Frm_Max.UseVisualStyleBackColor = true;
            this.btn_Frm_Max.Click += new System.EventHandler(this.btn_Frm_Max_Click);
            // 
            // btn_Frm_Min
            // 
            this.btn_Frm_Min.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Min.AutoSize = true;
            this.btn_Frm_Min.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Min.ImageIndex = 2;
            this.btn_Frm_Min.ImageList = this.imageList_Frm;
            this.btn_Frm_Min.Location = new System.Drawing.Point(1174, 1);
            this.btn_Frm_Min.Name = "btn_Frm_Min";
            this.btn_Frm_Min.Size = new System.Drawing.Size(30, 30);
            this.btn_Frm_Min.TabIndex = 5;
            this.btn_Frm_Min.UseVisualStyleBackColor = true;
            this.btn_Frm_Min.Click += new System.EventHandler(this.btn_Frm_Min_Click);
            // 
            // btn_Frm_Close
            // 
            this.btn_Frm_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Close.AutoSize = true;
            this.btn_Frm_Close.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Close.ImageIndex = 0;
            this.btn_Frm_Close.ImageList = this.imageList_Frm;
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
            this.lbl_Version.ImageIndex = 2;
            this.lbl_Version.Location = new System.Drawing.Point(7, 7);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(99, 19);
            this.lbl_Version.TabIndex = 3;
            this.lbl_Version.Text = "Vision Align";
            this.lbl_Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Frm_Minimize
            // 
            this.btn_Frm_Minimize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Frm_Minimize.AutoSize = true;
            this.btn_Frm_Minimize.FlatAppearance.BorderSize = 0;
            this.btn_Frm_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frm_Minimize.Location = new System.Drawing.Point(1166, -1);
            this.btn_Frm_Minimize.Name = "btn_Frm_Minimize";
            this.btn_Frm_Minimize.Size = new System.Drawing.Size(30, 30);
            this.btn_Frm_Minimize.TabIndex = 1;
            this.btn_Frm_Minimize.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ucRoundedPanel3);
            this.panel2.Controls.Add(this.ucRoundedPanel2);
            this.panel2.Controls.Add(this.lbl_Motor_Speed);
            this.panel2.Controls.Add(this.trackBar_MotorSpeed);
            this.panel2.Controls.Add(this.chkbox_ByStep);
            this.panel2.Controls.Add(this.txtbox_distance);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.ucRoundedPanel1);
            this.panel2.Controls.Add(this.btn_M_U_CCW);
            this.panel2.Controls.Add(this.btn_M_U_CW);
            this.panel2.Controls.Add(this.btn_Solder_Left);
            this.panel2.Controls.Add(this.btn_Solder_Right);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.btn_Solder_Bottom);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.btn_EStop);
            this.panel2.Controls.Add(this.btn_Solder_Top);
            this.panel2.Location = new System.Drawing.Point(1030, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 300);
            this.panel2.TabIndex = 84;
            // 
            // ucRoundedPanel3
            // 
            this.ucRoundedPanel3.BorderRadius = 30;
            this.ucRoundedPanel3.BorderSize = 1;
            this.ucRoundedPanel3.Caption = "SET MOVEMENT SPEED";
            this.ucRoundedPanel3.clBegin = System.Drawing.Color.Gray;
            this.ucRoundedPanel3.clBorder = System.Drawing.Color.Transparent;
            this.ucRoundedPanel3.clEnd = System.Drawing.Color.Gray;
            this.ucRoundedPanel3.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.ucRoundedPanel3.ForeColor = System.Drawing.Color.White;
            this.ucRoundedPanel3.Location = new System.Drawing.Point(0, 214);
            this.ucRoundedPanel3.Name = "ucRoundedPanel3";
            this.ucRoundedPanel3.Size = new System.Drawing.Size(244, 30);
            this.ucRoundedPanel3.TabIndex = 97;
            this.ucRoundedPanel3.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel3.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // ucRoundedPanel2
            // 
            this.ucRoundedPanel2.BorderRadius = 30;
            this.ucRoundedPanel2.BorderSize = 1;
            this.ucRoundedPanel2.Caption = "SET MOVEMENT DISTANCE";
            this.ucRoundedPanel2.clBegin = System.Drawing.Color.Gray;
            this.ucRoundedPanel2.clBorder = System.Drawing.Color.Transparent;
            this.ucRoundedPanel2.clEnd = System.Drawing.Color.Gray;
            this.ucRoundedPanel2.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.ucRoundedPanel2.ForeColor = System.Drawing.Color.White;
            this.ucRoundedPanel2.Location = new System.Drawing.Point(0, 149);
            this.ucRoundedPanel2.Name = "ucRoundedPanel2";
            this.ucRoundedPanel2.Size = new System.Drawing.Size(244, 30);
            this.ucRoundedPanel2.TabIndex = 97;
            this.ucRoundedPanel2.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel2.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // lbl_Motor_Speed
            // 
            this.lbl_Motor_Speed.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lbl_Motor_Speed.Location = new System.Drawing.Point(163, 259);
            this.lbl_Motor_Speed.Name = "lbl_Motor_Speed";
            this.lbl_Motor_Speed.Size = new System.Drawing.Size(55, 21);
            this.lbl_Motor_Speed.TabIndex = 96;
            this.lbl_Motor_Speed.Text = "0%";
            this.lbl_Motor_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_MotorSpeed
            // 
            this.trackBar_MotorSpeed.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar_MotorSpeed.Location = new System.Drawing.Point(12, 250);
            this.trackBar_MotorSpeed.Maximum = 20;
            this.trackBar_MotorSpeed.Name = "trackBar_MotorSpeed";
            this.trackBar_MotorSpeed.Size = new System.Drawing.Size(153, 45);
            this.trackBar_MotorSpeed.TabIndex = 10;
            this.trackBar_MotorSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // chkbox_ByStep
            // 
            this.chkbox_ByStep.AutoSize = true;
            this.chkbox_ByStep.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.chkbox_ByStep.Location = new System.Drawing.Point(12, 187);
            this.chkbox_ByStep.Name = "chkbox_ByStep";
            this.chkbox_ByStep.Size = new System.Drawing.Size(55, 21);
            this.chkbox_ByStep.TabIndex = 95;
            this.chkbox_ByStep.Text = "Step";
            this.chkbox_ByStep.UseVisualStyleBackColor = true;
            // 
            // txtbox_distance
            // 
            this.txtbox_distance.BackColor = System.Drawing.SystemColors.Control;
            this.txtbox_distance.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txtbox_distance.Location = new System.Drawing.Point(70, 185);
            this.txtbox_distance.Name = "txtbox_distance";
            this.txtbox_distance.Size = new System.Drawing.Size(106, 22);
            this.txtbox_distance.TabIndex = 94;
            this.txtbox_distance.Text = "0";
            this.txtbox_distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(182, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 17);
            this.label5.TabIndex = 93;
            this.label5.Text = "um";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucRoundedPanel1
            // 
            this.ucRoundedPanel1.BorderRadius = 30;
            this.ucRoundedPanel1.BorderSize = 1;
            this.ucRoundedPanel1.Caption = "MOTORS";
            this.ucRoundedPanel1.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            this.ucRoundedPanel1.clBorder = System.Drawing.Color.Transparent;
            this.ucRoundedPanel1.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            this.ucRoundedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucRoundedPanel1.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.ucRoundedPanel1.ForeColor = System.Drawing.Color.White;
            this.ucRoundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.ucRoundedPanel1.Name = "ucRoundedPanel1";
            this.ucRoundedPanel1.Size = new System.Drawing.Size(244, 30);
            this.ucRoundedPanel1.TabIndex = 92;
            this.ucRoundedPanel1.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel1.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // btn_M_U_CCW
            // 
            this.btn_M_U_CCW.BackColor = System.Drawing.SystemColors.Control;
            this.btn_M_U_CCW.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_M_U_CCW.ImageIndex = 0;
            this.btn_M_U_CCW.ImageList = this.imageList_Direction;
            this.btn_M_U_CCW.Location = new System.Drawing.Point(12, 35);
            this.btn_M_U_CCW.Name = "btn_M_U_CCW";
            this.btn_M_U_CCW.Size = new System.Drawing.Size(32, 32);
            this.btn_M_U_CCW.TabIndex = 83;
            this.btn_M_U_CCW.UseVisualStyleBackColor = false;
            // 
            // imageList_Direction
            // 
            this.imageList_Direction.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Direction.ImageStream")));
            this.imageList_Direction.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Direction.Images.SetKeyName(0, "1.bmp");
            this.imageList_Direction.Images.SetKeyName(1, "2.bmp");
            this.imageList_Direction.Images.SetKeyName(2, "3.bmp");
            this.imageList_Direction.Images.SetKeyName(3, "4.bmp");
            this.imageList_Direction.Images.SetKeyName(4, "5.bmp");
            this.imageList_Direction.Images.SetKeyName(5, "6.bmp");
            this.imageList_Direction.Images.SetKeyName(6, "7.bmp");
            this.imageList_Direction.Images.SetKeyName(7, "8.bmp");
            this.imageList_Direction.Images.SetKeyName(8, "9.bmp");
            // 
            // btn_M_U_CW
            // 
            this.btn_M_U_CW.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_M_U_CW.ImageIndex = 2;
            this.btn_M_U_CW.ImageList = this.imageList_Direction;
            this.btn_M_U_CW.Location = new System.Drawing.Point(88, 35);
            this.btn_M_U_CW.Name = "btn_M_U_CW";
            this.btn_M_U_CW.Size = new System.Drawing.Size(32, 32);
            this.btn_M_U_CW.TabIndex = 86;
            this.btn_M_U_CW.UseVisualStyleBackColor = false;
            // 
            // btn_Solder_Left
            // 
            this.btn_Solder_Left.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Solder_Left.ImageIndex = 3;
            this.btn_Solder_Left.ImageList = this.imageList_Direction;
            this.btn_Solder_Left.Location = new System.Drawing.Point(12, 73);
            this.btn_Solder_Left.Name = "btn_Solder_Left";
            this.btn_Solder_Left.Size = new System.Drawing.Size(32, 32);
            this.btn_Solder_Left.TabIndex = 87;
            this.btn_Solder_Left.UseVisualStyleBackColor = false;
            // 
            // btn_Solder_Right
            // 
            this.btn_Solder_Right.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Solder_Right.ImageIndex = 5;
            this.btn_Solder_Right.ImageList = this.imageList_Direction;
            this.btn_Solder_Right.Location = new System.Drawing.Point(88, 73);
            this.btn_Solder_Right.Name = "btn_Solder_Right";
            this.btn_Solder_Right.Size = new System.Drawing.Size(32, 32);
            this.btn_Solder_Right.TabIndex = 88;
            this.btn_Solder_Right.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ImageIndex = 8;
            this.button1.ImageList = this.imageList_Direction;
            this.button1.Location = new System.Drawing.Point(88, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 89;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ImageIndex = 6;
            this.button2.ImageList = this.imageList_Direction;
            this.button2.Location = new System.Drawing.Point(12, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 32);
            this.button2.TabIndex = 89;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ImageIndex = 7;
            this.button5.ImageList = this.imageList_Direction;
            this.button5.Location = new System.Drawing.Point(186, 111);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(32, 32);
            this.button5.TabIndex = 89;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // btn_Solder_Bottom
            // 
            this.btn_Solder_Bottom.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Solder_Bottom.ImageIndex = 7;
            this.btn_Solder_Bottom.ImageList = this.imageList_Direction;
            this.btn_Solder_Bottom.Location = new System.Drawing.Point(50, 111);
            this.btn_Solder_Bottom.Name = "btn_Solder_Bottom";
            this.btn_Solder_Bottom.Size = new System.Drawing.Size(32, 32);
            this.btn_Solder_Bottom.TabIndex = 89;
            this.btn_Solder_Bottom.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ImageIndex = 4;
            this.button4.ImageList = this.imageList_Direction;
            this.button4.Location = new System.Drawing.Point(186, 73);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 32);
            this.button4.TabIndex = 90;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ImageIndex = 1;
            this.button3.ImageList = this.imageList_Direction;
            this.button3.Location = new System.Drawing.Point(186, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 32);
            this.button3.TabIndex = 91;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // btn_EStop
            // 
            this.btn_EStop.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EStop.ImageIndex = 4;
            this.btn_EStop.ImageList = this.imageList_Direction;
            this.btn_EStop.Location = new System.Drawing.Point(50, 73);
            this.btn_EStop.Name = "btn_EStop";
            this.btn_EStop.Size = new System.Drawing.Size(32, 32);
            this.btn_EStop.TabIndex = 90;
            this.btn_EStop.UseVisualStyleBackColor = false;
            // 
            // btn_Solder_Top
            // 
            this.btn_Solder_Top.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Solder_Top.ImageIndex = 1;
            this.btn_Solder_Top.ImageList = this.imageList_Direction;
            this.btn_Solder_Top.Location = new System.Drawing.Point(50, 35);
            this.btn_Solder_Top.Name = "btn_Solder_Top";
            this.btn_Solder_Top.Size = new System.Drawing.Size(32, 32);
            this.btn_Solder_Top.TabIndex = 91;
            this.btn_Solder_Top.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSelectCoarseVision);
            this.panel1.Controls.Add(this.btnSelectFineVision);
            this.panel1.Controls.Add(this.ucRoundedPanel6);
            this.panel1.Location = new System.Drawing.Point(1030, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 86);
            this.panel1.TabIndex = 84;
            // 
            // btnSelectCoarseVision
            // 
            this.btnSelectCoarseVision.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelectCoarseVision.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectCoarseVision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectCoarseVision.ImageIndex = 0;
            this.btnSelectCoarseVision.ImageList = this.imageList_chkbox;
            this.btnSelectCoarseVision.Location = new System.Drawing.Point(124, 34);
            this.btnSelectCoarseVision.Name = "btnSelectCoarseVision";
            this.btnSelectCoarseVision.Size = new System.Drawing.Size(117, 45);
            this.btnSelectCoarseVision.TabIndex = 99;
            this.btnSelectCoarseVision.Tag = "COARSE";
            this.btnSelectCoarseVision.Text = "COARSE";
            this.btnSelectCoarseVision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectCoarseVision.UseVisualStyleBackColor = false;
            // 
            // imageList_chkbox
            // 
            this.imageList_chkbox.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_chkbox.ImageStream")));
            this.imageList_chkbox.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_chkbox.Images.SetKeyName(0, "checkbox_241.png");
            this.imageList_chkbox.Images.SetKeyName(1, "checked_checkbox_24.png");
            this.imageList_chkbox.Images.SetKeyName(2, "checked.png");
            this.imageList_chkbox.Images.SetKeyName(3, "unchecked.png");
            // 
            // btnSelectFineVision
            // 
            this.btnSelectFineVision.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelectFineVision.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFineVision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectFineVision.ImageIndex = 0;
            this.btnSelectFineVision.ImageList = this.imageList_chkbox;
            this.btnSelectFineVision.Location = new System.Drawing.Point(3, 34);
            this.btnSelectFineVision.Name = "btnSelectFineVision";
            this.btnSelectFineVision.Size = new System.Drawing.Size(117, 45);
            this.btnSelectFineVision.TabIndex = 99;
            this.btnSelectFineVision.Tag = "FINE";
            this.btnSelectFineVision.Text = "FINE\r\n";
            this.btnSelectFineVision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectFineVision.UseVisualStyleBackColor = false;
            this.btnSelectFineVision.Click += new System.EventHandler(this.btnSelectFineVision_Click);
            // 
            // ucRoundedPanel6
            // 
            this.ucRoundedPanel6.BorderRadius = 30;
            this.ucRoundedPanel6.BorderSize = 1;
            this.ucRoundedPanel6.Caption = "SELECT VISION";
            this.ucRoundedPanel6.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            this.ucRoundedPanel6.clBorder = System.Drawing.Color.Transparent;
            this.ucRoundedPanel6.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            this.ucRoundedPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucRoundedPanel6.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.ucRoundedPanel6.ForeColor = System.Drawing.Color.White;
            this.ucRoundedPanel6.Location = new System.Drawing.Point(0, 0);
            this.ucRoundedPanel6.Name = "ucRoundedPanel6";
            this.ucRoundedPanel6.Size = new System.Drawing.Size(244, 30);
            this.ucRoundedPanel6.TabIndex = 92;
            this.ucRoundedPanel6.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
            this.ucRoundedPanel6.TextAlignVertical = System.Drawing.StringAlignment.Center;
            // 
            // FrmVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 980);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelFrmTitleBar);
            this.Controls.Add(this.panel_Vision);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmVision";
            this.Text = "Vision Align";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.Form1_VisibleChanged);
            this.panelFrmTitleBar.ResumeLayout(false);
            this.panelFrmTitleBar.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_MotorSpeed)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel_Vision;
		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Label lbl_Version;
		private System.Windows.Forms.Button btn_Frm_Minimize;
		private System.Windows.Forms.Button btn_Frm_Max;
		private System.Windows.Forms.Button btn_Frm_Min;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Panel panel2;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel1;
		private System.Windows.Forms.Button btn_M_U_CCW;
		private System.Windows.Forms.Button btn_M_U_CW;
		private System.Windows.Forms.Button btn_Solder_Left;
		private System.Windows.Forms.Button btn_Solder_Right;
		private System.Windows.Forms.Button btn_Solder_Bottom;
		private System.Windows.Forms.Button btn_EStop;
		private System.Windows.Forms.Button btn_Solder_Top;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel3;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel2;
		private System.Windows.Forms.Label lbl_Motor_Speed;
		private System.Windows.Forms.TrackBar trackBar_MotorSpeed;
		private System.Windows.Forms.CheckBox chkbox_ByStep;
		private System.Windows.Forms.TextBox txtbox_distance;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button btn_Frm_Normalize;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnSelectCoarseVision;
		private System.Windows.Forms.ImageList imageList_chkbox;
		private System.Windows.Forms.Button btnSelectFineVision;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel6;
		private System.Windows.Forms.ImageList imageList_Frm;
		private System.Windows.Forms.ImageList imageList_Direction;
	}
}

