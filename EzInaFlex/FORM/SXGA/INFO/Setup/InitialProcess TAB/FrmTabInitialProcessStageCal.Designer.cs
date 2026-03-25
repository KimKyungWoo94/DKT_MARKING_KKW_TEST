namespace EzIna
{
	partial class FrmTabInitialProcessStageCal
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTabInitialProcessStageCal));
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.dataGridView_Datas = new System.Windows.Forms.DataGridView();
			this.ucRoundedPanel1 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.dataGridView_Options = new System.Windows.Forms.DataGridView();
			this.ucRoundedPanel3 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.btn_ProjectSave = new System.Windows.Forms.Button();
			this.imageList_Recipe = new System.Windows.Forms.ImageList(this.components);
			this.btn_ProjectOpen = new System.Windows.Forms.Button();
			this.pictureBox_ProgressBar_PercentageOfCompletion = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ucRoundedPanel2 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucCellBox_Stage2DCal = new EzIna.GUI.UserControls.ucCellBox();
			this.imageList_For_TreeMenu = new System.Windows.Forms.ImageList(this.components);
			this.btn_Manual_StageCal_Start = new System.Windows.Forms.Button();
			this.btn_Manual_StageCal_Stop = new System.Windows.Forms.Button();
			this.lblMapIndex = new System.Windows.Forms.Label();
			this.lblActive = new System.Windows.Forms.Label();
			this.lbl_XOffset = new System.Windows.Forms.Label();
			this.lbl_YOffset = new System.Windows.Forms.Label();
			this.btn_ApplyOffset = new System.Windows.Forms.Button();
			this.lbl_NewXOffset = new System.Windows.Forms.Label();
			this.lbl_NewYOffset = new System.Windows.Forms.Label();
			this.ucRoundedPanel4 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel9 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel7 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel8 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel6 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.ucRoundedPanel5 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.btn_Set1stPos = new System.Windows.Forms.Button();
			this.btn_Set2ndPos = new System.Windows.Forms.Button();
			this.btn_Manual_StageCal_Reset = new System.Windows.Forms.Button();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Datas)).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Options)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_ProgressBar_PercentageOfCompletion)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.dataGridView_Datas, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.ucRoundedPanel1, 0, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(578, 12);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(640, 496);
			this.tableLayoutPanel2.TabIndex = 15;
			// 
			// dataGridView_Datas
			// 
			this.dataGridView_Datas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
			dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_Datas.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView_Datas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView_Datas.GridColor = System.Drawing.Color.SteelBlue;
			this.dataGridView_Datas.Location = new System.Drawing.Point(3, 43);
			this.dataGridView_Datas.Name = "dataGridView_Datas";
			this.dataGridView_Datas.RowTemplate.Height = 23;
			this.dataGridView_Datas.Size = new System.Drawing.Size(634, 450);
			this.dataGridView_Datas.TabIndex = 96;
			this.dataGridView_Datas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Datas_CellContentClick);
			// 
			// ucRoundedPanel1
			// 
			this.ucRoundedPanel1.BorderRadius = 32;
			this.ucRoundedPanel1.BorderSize = 1;
			this.ucRoundedPanel1.Caption = "PARAMETERS";
			this.ucRoundedPanel1.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel1.clBorder = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel1.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel1.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel1.Location = new System.Drawing.Point(3, 3);
			this.ucRoundedPanel1.Name = "ucRoundedPanel1";
			this.ucRoundedPanel1.Size = new System.Drawing.Size(217, 32);
			this.ucRoundedPanel1.TabIndex = 10;
			this.ucRoundedPanel1.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel1.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.dataGridView_Options, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.ucRoundedPanel3, 0, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(578, 514);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(640, 223);
			this.tableLayoutPanel3.TabIndex = 15;
			// 
			// dataGridView_Options
			// 
			this.dataGridView_Options.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
			dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_Options.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView_Options.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView_Options.GridColor = System.Drawing.Color.White;
			this.dataGridView_Options.Location = new System.Drawing.Point(3, 43);
			this.dataGridView_Options.Name = "dataGridView_Options";
			this.dataGridView_Options.RowTemplate.Height = 23;
			this.dataGridView_Options.Size = new System.Drawing.Size(634, 177);
			this.dataGridView_Options.TabIndex = 96;
			// 
			// ucRoundedPanel3
			// 
			this.ucRoundedPanel3.BorderRadius = 32;
			this.ucRoundedPanel3.BorderSize = 1;
			this.ucRoundedPanel3.Caption = "OPTIONS";
			this.ucRoundedPanel3.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel3.clBorder = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel3.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel3.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel3.Location = new System.Drawing.Point(3, 3);
			this.ucRoundedPanel3.Name = "ucRoundedPanel3";
			this.ucRoundedPanel3.Size = new System.Drawing.Size(217, 32);
			this.ucRoundedPanel3.TabIndex = 10;
			this.ucRoundedPanel3.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel3.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// btn_ProjectSave
			// 
			this.btn_ProjectSave.BackColor = System.Drawing.Color.White;
			this.btn_ProjectSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_ProjectSave.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_ProjectSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_ProjectSave.ImageIndex = 1;
			this.btn_ProjectSave.ImageList = this.imageList_Recipe;
			this.btn_ProjectSave.Location = new System.Drawing.Point(1115, 743);
			this.btn_ProjectSave.Name = "btn_ProjectSave";
			this.btn_ProjectSave.Size = new System.Drawing.Size(100, 50);
			this.btn_ProjectSave.TabIndex = 94;
			this.btn_ProjectSave.Tag = "";
			this.btn_ProjectSave.Text = "Save";
			this.btn_ProjectSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_ProjectSave.UseVisualStyleBackColor = false;
			this.btn_ProjectSave.Click += new System.EventHandler(this.btn_ProjectSave_Click);
			// 
			// imageList_Recipe
			// 
			this.imageList_Recipe.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Recipe.ImageStream")));
			this.imageList_Recipe.TransparentColor = System.Drawing.SystemColors.Control;
			this.imageList_Recipe.Images.SetKeyName(0, "open.png");
			this.imageList_Recipe.Images.SetKeyName(1, "save.png");
			this.imageList_Recipe.Images.SetKeyName(2, "Add.png");
			this.imageList_Recipe.Images.SetKeyName(3, "Rename.png");
			this.imageList_Recipe.Images.SetKeyName(4, "Delete.png");
			this.imageList_Recipe.Images.SetKeyName(5, "Option.png");
			this.imageList_Recipe.Images.SetKeyName(6, "Vision.png");
			this.imageList_Recipe.Images.SetKeyName(7, "Network.png");
			this.imageList_Recipe.Images.SetKeyName(8, "apply button.ico");
			this.imageList_Recipe.Images.SetKeyName(9, "Start.png");
			this.imageList_Recipe.Images.SetKeyName(10, "Stop.png");
			this.imageList_Recipe.Images.SetKeyName(11, "reset (2).png");
			// 
			// btn_ProjectOpen
			// 
			this.btn_ProjectOpen.BackColor = System.Drawing.Color.White;
			this.btn_ProjectOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_ProjectOpen.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_ProjectOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_ProjectOpen.ImageIndex = 0;
			this.btn_ProjectOpen.ImageList = this.imageList_Recipe;
			this.btn_ProjectOpen.Location = new System.Drawing.Point(1009, 743);
			this.btn_ProjectOpen.Name = "btn_ProjectOpen";
			this.btn_ProjectOpen.Size = new System.Drawing.Size(100, 50);
			this.btn_ProjectOpen.TabIndex = 93;
			this.btn_ProjectOpen.Tag = "";
			this.btn_ProjectOpen.Text = "Open";
			this.btn_ProjectOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_ProjectOpen.UseVisualStyleBackColor = false;
			this.btn_ProjectOpen.Click += new System.EventHandler(this.btn_ProjectOpen_Click);
			// 
			// pictureBox_ProgressBar_PercentageOfCompletion
			// 
			this.pictureBox_ProgressBar_PercentageOfCompletion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox_ProgressBar_PercentageOfCompletion.Location = new System.Drawing.Point(284, 3);
			this.pictureBox_ProgressBar_PercentageOfCompletion.Name = "pictureBox_ProgressBar_PercentageOfCompletion";
			this.pictureBox_ProgressBar_PercentageOfCompletion.Size = new System.Drawing.Size(276, 34);
			this.pictureBox_ProgressBar_PercentageOfCompletion.TabIndex = 113;
			this.pictureBox_ProgressBar_PercentageOfCompletion.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.ucRoundedPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.ucCellBox_Stage2DCal, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox_ProgressBar_PercentageOfCompletion, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(563, 600);
			this.tableLayoutPanel1.TabIndex = 114;
			// 
			// ucRoundedPanel2
			// 
			this.ucRoundedPanel2.BorderRadius = 32;
			this.ucRoundedPanel2.BorderSize = 1;
			this.ucRoundedPanel2.Caption = "STAGE 2D CAL";
			this.ucRoundedPanel2.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel2.clBorder = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel2.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel2.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel2.Location = new System.Drawing.Point(3, 3);
			this.ucRoundedPanel2.Name = "ucRoundedPanel2";
			this.ucRoundedPanel2.Size = new System.Drawing.Size(217, 32);
			this.ucRoundedPanel2.TabIndex = 10;
			this.ucRoundedPanel2.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel2.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucCellBox_Stage2DCal
			// 
			this.ucCellBox_Stage2DCal.BackColor = System.Drawing.Color.Black;
			this.ucCellBox_Stage2DCal.BlockColCount = 1;
			this.ucCellBox_Stage2DCal.BlockRowCount = 1;
			this.ucCellBox_Stage2DCal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ucCellBox_Stage2DCal.ColorEmpty = System.Drawing.Color.DimGray;
			this.ucCellBox_Stage2DCal.ColorFinish = System.Drawing.Color.Blue;
			this.ucCellBox_Stage2DCal.ColorNG = System.Drawing.Color.Red;
			this.ucCellBox_Stage2DCal.ColorNG_INSPECTION = System.Drawing.Color.White;
			this.ucCellBox_Stage2DCal.ColorNG_SPEC_OUT = System.Drawing.Color.Red;
			this.ucCellBox_Stage2DCal.ColorOkay = System.Drawing.Color.Blue;
			this.ucCellBox_Stage2DCal.ColorReady = System.Drawing.Color.Lime;
			this.ucCellBox_Stage2DCal.ColorWork = System.Drawing.Color.AliceBlue;
			this.tableLayoutPanel1.SetColumnSpan(this.ucCellBox_Stage2DCal, 2);
			this.ucCellBox_Stage2DCal.DisplayNumColor = System.Drawing.Color.DarkBlue;
			this.ucCellBox_Stage2DCal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucCellBox_Stage2DCal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucCellBox_Stage2DCal.Location = new System.Drawing.Point(3, 43);
			this.ucCellBox_Stage2DCal.MouseActiveColor = System.Drawing.Color.DarkGreen;
			this.ucCellBox_Stage2DCal.MouseMode = EzIna.GUI.UserControls.eMouseMode.Pan;
			this.ucCellBox_Stage2DCal.MouseMoveChangeEnable = false;
			this.ucCellBox_Stage2DCal.Name = "ucCellBox_Stage2DCal";
			this.ucCellBox_Stage2DCal.PixelGridBlockColor = System.Drawing.Color.Black;
			this.ucCellBox_Stage2DCal.PixelGridThreshold = 1;
			this.ucCellBox_Stage2DCal.PixelNumberThreshold = 25;
			this.ucCellBox_Stage2DCal.ShowPixelGrid = true;
			this.ucCellBox_Stage2DCal.Size = new System.Drawing.Size(557, 554);
			this.ucCellBox_Stage2DCal.TabIndex = 12;
			this.ucCellBox_Stage2DCal.Zoom = 500;
			this.ucCellBox_Stage2DCal.CellSelected += new System.EventHandler<EzIna.GUI.UserControls.CellEventArgs>(this.ucCellBox_Stage2DCal_CellSelected);
			this.ucCellBox_Stage2DCal.CellActived += new System.EventHandler<EzIna.GUI.UserControls.CellEventArgs>(this.ucCellBox_Stage2DCal_CellActived);
			this.ucCellBox_Stage2DCal.DoubleClick += new System.EventHandler(this.ucCellBox_Stage2DCal_DoubleClick);
			// 
			// imageList_For_TreeMenu
			// 
			this.imageList_For_TreeMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_For_TreeMenu.ImageStream")));
			this.imageList_For_TreeMenu.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList_For_TreeMenu.Images.SetKeyName(0, "unchecked.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(1, "checked.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(2, "disable.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(3, "Category.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(4, "Attenuator.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(5, "CAM.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(6, "Interlock.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(7, "Laser.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(8, "Motion.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(9, "Path.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(10, "Pwrmeter.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(11, "Scanner.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(12, "Vision.png");
			this.imageList_For_TreeMenu.Images.SetKeyName(13, "InitialProcess.png");
			// 
			// btn_Manual_StageCal_Start
			// 
			this.btn_Manual_StageCal_Start.BackColor = System.Drawing.Color.White;
			this.btn_Manual_StageCal_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Manual_StageCal_Start.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Manual_StageCal_Start.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Manual_StageCal_Start.ImageIndex = 9;
			this.btn_Manual_StageCal_Start.ImageList = this.imageList_Recipe;
			this.btn_Manual_StageCal_Start.Location = new System.Drawing.Point(581, 743);
			this.btn_Manual_StageCal_Start.Name = "btn_Manual_StageCal_Start";
			this.btn_Manual_StageCal_Start.Size = new System.Drawing.Size(100, 50);
			this.btn_Manual_StageCal_Start.TabIndex = 93;
			this.btn_Manual_StageCal_Start.Tag = "";
			this.btn_Manual_StageCal_Start.Text = "Start";
			this.btn_Manual_StageCal_Start.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Manual_StageCal_Start.UseVisualStyleBackColor = false;
			this.btn_Manual_StageCal_Start.Click += new System.EventHandler(this.btn_Manual_StageCal_Start_Click);
			// 
			// btn_Manual_StageCal_Stop
			// 
			this.btn_Manual_StageCal_Stop.BackColor = System.Drawing.Color.White;
			this.btn_Manual_StageCal_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Manual_StageCal_Stop.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Manual_StageCal_Stop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Manual_StageCal_Stop.ImageIndex = 10;
			this.btn_Manual_StageCal_Stop.ImageList = this.imageList_Recipe;
			this.btn_Manual_StageCal_Stop.Location = new System.Drawing.Point(687, 743);
			this.btn_Manual_StageCal_Stop.Name = "btn_Manual_StageCal_Stop";
			this.btn_Manual_StageCal_Stop.Size = new System.Drawing.Size(100, 50);
			this.btn_Manual_StageCal_Stop.TabIndex = 94;
			this.btn_Manual_StageCal_Stop.Tag = "";
			this.btn_Manual_StageCal_Stop.Text = "Stop";
			this.btn_Manual_StageCal_Stop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Manual_StageCal_Stop.UseVisualStyleBackColor = false;
			this.btn_Manual_StageCal_Stop.Click += new System.EventHandler(this.btn_Manual_StageCal_Stop_Click);
			// 
			// lblMapIndex
			// 
			this.lblMapIndex.BackColor = System.Drawing.Color.White;
			this.lblMapIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblMapIndex.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMapIndex.Location = new System.Drawing.Point(15, 653);
			this.lblMapIndex.Name = "lblMapIndex";
			this.lblMapIndex.Size = new System.Drawing.Size(139, 24);
			this.lblMapIndex.TabIndex = 1350;
			this.lblMapIndex.Tag = "";
			this.lblMapIndex.Text = "X=0001, Y=0001";
			this.lblMapIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblActive
			// 
			this.lblActive.BackColor = System.Drawing.Color.White;
			this.lblActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblActive.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblActive.Location = new System.Drawing.Point(155, 653);
			this.lblActive.Name = "lblActive";
			this.lblActive.Size = new System.Drawing.Size(139, 24);
			this.lblActive.TabIndex = 1345;
			this.lblActive.Tag = "";
			this.lblActive.Text = "X=0001, Y=0001";
			this.lblActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_XOffset
			// 
			this.lbl_XOffset.BackColor = System.Drawing.Color.White;
			this.lbl_XOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_XOffset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_XOffset.Location = new System.Drawing.Point(295, 653);
			this.lbl_XOffset.Name = "lbl_XOffset";
			this.lbl_XOffset.Size = new System.Drawing.Size(139, 24);
			this.lbl_XOffset.TabIndex = 1345;
			this.lbl_XOffset.Tag = "";
			this.lbl_XOffset.Text = "X=0001, Y=0001";
			this.lbl_XOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_YOffset
			// 
			this.lbl_YOffset.BackColor = System.Drawing.Color.White;
			this.lbl_YOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_YOffset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_YOffset.Location = new System.Drawing.Point(435, 653);
			this.lbl_YOffset.Name = "lbl_YOffset";
			this.lbl_YOffset.Size = new System.Drawing.Size(139, 24);
			this.lbl_YOffset.TabIndex = 1345;
			this.lbl_YOffset.Tag = "";
			this.lbl_YOffset.Text = "X=0001, Y=0001";
			this.lbl_YOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btn_ApplyOffset
			// 
			this.btn_ApplyOffset.BackColor = System.Drawing.Color.White;
			this.btn_ApplyOffset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_ApplyOffset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_ApplyOffset.ImageIndex = 8;
			this.btn_ApplyOffset.ImageList = this.imageList_Recipe;
			this.btn_ApplyOffset.Location = new System.Drawing.Point(481, 687);
			this.btn_ApplyOffset.Name = "btn_ApplyOffset";
			this.btn_ApplyOffset.Size = new System.Drawing.Size(92, 50);
			this.btn_ApplyOffset.TabIndex = 93;
			this.btn_ApplyOffset.Tag = "";
			this.btn_ApplyOffset.UseVisualStyleBackColor = false;
			// 
			// lbl_NewXOffset
			// 
			this.lbl_NewXOffset.BackColor = System.Drawing.Color.White;
			this.lbl_NewXOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_NewXOffset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_NewXOffset.Location = new System.Drawing.Point(15, 713);
			this.lbl_NewXOffset.Name = "lbl_NewXOffset";
			this.lbl_NewXOffset.Size = new System.Drawing.Size(139, 24);
			this.lbl_NewXOffset.TabIndex = 1345;
			this.lbl_NewXOffset.Tag = "";
			this.lbl_NewXOffset.Text = "0.000";
			this.lbl_NewXOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_NewYOffset
			// 
			this.lbl_NewYOffset.BackColor = System.Drawing.Color.White;
			this.lbl_NewYOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_NewYOffset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_NewYOffset.Location = new System.Drawing.Point(155, 713);
			this.lbl_NewYOffset.Name = "lbl_NewYOffset";
			this.lbl_NewYOffset.Size = new System.Drawing.Size(139, 24);
			this.lbl_NewYOffset.TabIndex = 1345;
			this.lbl_NewYOffset.Tag = "";
			this.lbl_NewYOffset.Text = "0.000";
			this.lbl_NewYOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ucRoundedPanel4
			// 
			this.ucRoundedPanel4.BorderRadius = 1;
			this.ucRoundedPanel4.BorderSize = 1;
			this.ucRoundedPanel4.Caption = "Map Index";
			this.ucRoundedPanel4.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel4.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel4.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel4.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel4.Location = new System.Drawing.Point(15, 626);
			this.ucRoundedPanel4.Name = "ucRoundedPanel4";
			this.ucRoundedPanel4.Size = new System.Drawing.Size(139, 24);
			this.ucRoundedPanel4.TabIndex = 10;
			this.ucRoundedPanel4.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel4.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel9
			// 
			this.ucRoundedPanel9.BorderRadius = 1;
			this.ucRoundedPanel9.BorderSize = 1;
			this.ucRoundedPanel9.Caption = "New Y Offset";
			this.ucRoundedPanel9.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ucRoundedPanel9.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel9.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.ucRoundedPanel9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel9.ForeColor = System.Drawing.Color.White;
			this.ucRoundedPanel9.Location = new System.Drawing.Point(155, 687);
			this.ucRoundedPanel9.Name = "ucRoundedPanel9";
			this.ucRoundedPanel9.Size = new System.Drawing.Size(139, 24);
			this.ucRoundedPanel9.TabIndex = 10;
			this.ucRoundedPanel9.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel9.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel7
			// 
			this.ucRoundedPanel7.BorderRadius = 1;
			this.ucRoundedPanel7.BorderSize = 1;
			this.ucRoundedPanel7.Caption = "Y Offset";
			this.ucRoundedPanel7.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel7.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel7.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel7.ForeColor = System.Drawing.Color.White;
			this.ucRoundedPanel7.Location = new System.Drawing.Point(435, 626);
			this.ucRoundedPanel7.Name = "ucRoundedPanel7";
			this.ucRoundedPanel7.Size = new System.Drawing.Size(139, 24);
			this.ucRoundedPanel7.TabIndex = 10;
			this.ucRoundedPanel7.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel7.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel8
			// 
			this.ucRoundedPanel8.BorderRadius = 1;
			this.ucRoundedPanel8.BorderSize = 1;
			this.ucRoundedPanel8.Caption = "New X Offset";
			this.ucRoundedPanel8.clBegin = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ucRoundedPanel8.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel8.clEnd = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.ucRoundedPanel8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel8.ForeColor = System.Drawing.Color.White;
			this.ucRoundedPanel8.Location = new System.Drawing.Point(15, 687);
			this.ucRoundedPanel8.Name = "ucRoundedPanel8";
			this.ucRoundedPanel8.Size = new System.Drawing.Size(139, 24);
			this.ucRoundedPanel8.TabIndex = 10;
			this.ucRoundedPanel8.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel8.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel6
			// 
			this.ucRoundedPanel6.BorderRadius = 1;
			this.ucRoundedPanel6.BorderSize = 1;
			this.ucRoundedPanel6.Caption = "X Offset";
			this.ucRoundedPanel6.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel6.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel6.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel6.ForeColor = System.Drawing.Color.White;
			this.ucRoundedPanel6.Location = new System.Drawing.Point(295, 626);
			this.ucRoundedPanel6.Name = "ucRoundedPanel6";
			this.ucRoundedPanel6.Size = new System.Drawing.Size(139, 24);
			this.ucRoundedPanel6.TabIndex = 10;
			this.ucRoundedPanel6.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel6.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// ucRoundedPanel5
			// 
			this.ucRoundedPanel5.BorderRadius = 1;
			this.ucRoundedPanel5.BorderSize = 1;
			this.ucRoundedPanel5.Caption = "Map Current Index";
			this.ucRoundedPanel5.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel5.clBorder = System.Drawing.Color.Black;
			this.ucRoundedPanel5.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel5.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel5.Location = new System.Drawing.Point(155, 626);
			this.ucRoundedPanel5.Name = "ucRoundedPanel5";
			this.ucRoundedPanel5.Size = new System.Drawing.Size(139, 24);
			this.ucRoundedPanel5.TabIndex = 10;
			this.ucRoundedPanel5.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel5.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// btn_Set1stPos
			// 
			this.btn_Set1stPos.BackColor = System.Drawing.Color.White;
			this.btn_Set1stPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Set1stPos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Set1stPos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Set1stPos.ImageIndex = 0;
			this.btn_Set1stPos.ImageList = this.imageList_For_TreeMenu;
			this.btn_Set1stPos.Location = new System.Drawing.Point(295, 687);
			this.btn_Set1stPos.Name = "btn_Set1stPos";
			this.btn_Set1stPos.Size = new System.Drawing.Size(92, 50);
			this.btn_Set1stPos.TabIndex = 93;
			this.btn_Set1stPos.Tag = "";
			this.btn_Set1stPos.Text = "Set 1st";
			this.btn_Set1stPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Set1stPos.UseVisualStyleBackColor = false;
			// 
			// btn_Set2ndPos
			// 
			this.btn_Set2ndPos.BackColor = System.Drawing.Color.White;
			this.btn_Set2ndPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Set2ndPos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Set2ndPos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Set2ndPos.ImageIndex = 0;
			this.btn_Set2ndPos.ImageList = this.imageList_For_TreeMenu;
			this.btn_Set2ndPos.Location = new System.Drawing.Point(388, 687);
			this.btn_Set2ndPos.Name = "btn_Set2ndPos";
			this.btn_Set2ndPos.Size = new System.Drawing.Size(92, 50);
			this.btn_Set2ndPos.TabIndex = 93;
			this.btn_Set2ndPos.Tag = "";
			this.btn_Set2ndPos.Text = "Set 2nd";
			this.btn_Set2ndPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Set2ndPos.UseVisualStyleBackColor = false;
			// 
			// btn_Manual_StageCal_Reset
			// 
			this.btn_Manual_StageCal_Reset.BackColor = System.Drawing.Color.White;
			this.btn_Manual_StageCal_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Manual_StageCal_Reset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Manual_StageCal_Reset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Manual_StageCal_Reset.ImageIndex = 11;
			this.btn_Manual_StageCal_Reset.ImageList = this.imageList_Recipe;
			this.btn_Manual_StageCal_Reset.Location = new System.Drawing.Point(793, 743);
			this.btn_Manual_StageCal_Reset.Name = "btn_Manual_StageCal_Reset";
			this.btn_Manual_StageCal_Reset.Size = new System.Drawing.Size(100, 50);
			this.btn_Manual_StageCal_Reset.TabIndex = 94;
			this.btn_Manual_StageCal_Reset.Tag = "";
			this.btn_Manual_StageCal_Reset.Text = "Reset";
			this.btn_Manual_StageCal_Reset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Manual_StageCal_Reset.UseVisualStyleBackColor = false;
			this.btn_Manual_StageCal_Reset.Click += new System.EventHandler(this.btn_Manual_StageCal_Reset_Click);
			// 
			// FrmTabInitialProcessStageCal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 985);
			this.Controls.Add(this.ucRoundedPanel4);
			this.Controls.Add(this.lblMapIndex);
			this.Controls.Add(this.ucRoundedPanel9);
			this.Controls.Add(this.ucRoundedPanel7);
			this.Controls.Add(this.ucRoundedPanel8);
			this.Controls.Add(this.ucRoundedPanel6);
			this.Controls.Add(this.ucRoundedPanel5);
			this.Controls.Add(this.lbl_NewYOffset);
			this.Controls.Add(this.lbl_YOffset);
			this.Controls.Add(this.lbl_NewXOffset);
			this.Controls.Add(this.lbl_XOffset);
			this.Controls.Add(this.lblActive);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.btn_Manual_StageCal_Reset);
			this.Controls.Add(this.btn_Manual_StageCal_Stop);
			this.Controls.Add(this.btn_ProjectSave);
			this.Controls.Add(this.btn_Set1stPos);
			this.Controls.Add(this.btn_Set2ndPos);
			this.Controls.Add(this.btn_ApplyOffset);
			this.Controls.Add(this.btn_Manual_StageCal_Start);
			this.Controls.Add(this.btn_ProjectOpen);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "FrmTabInitialProcessStageCal";
			this.Text = "FrmTabInitialProcessStageCal";
			this.Load += new System.EventHandler(this.FrmTabInitialProcessStageCal_Load);
			this.VisibleChanged += new System.EventHandler(this.FrmTabInitialProcessStageCal_VisibleChanged);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Datas)).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Options)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_ProgressBar_PercentageOfCompletion)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.DataGridView dataGridView_Datas;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel3;
		private EzIna.GUI.UserControls.ucCellBox ucCellBox_Stage2DCal;
		private System.Windows.Forms.Button btn_ProjectSave;
		private System.Windows.Forms.Button btn_ProjectOpen;
		private System.Windows.Forms.ImageList imageList_Recipe;
		private System.Windows.Forms.PictureBox pictureBox_ProgressBar_PercentageOfCompletion;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ImageList imageList_For_TreeMenu;
		private System.Windows.Forms.DataGridView dataGridView_Options;
		private System.Windows.Forms.Button btn_Manual_StageCal_Start;
		private System.Windows.Forms.Button btn_Manual_StageCal_Stop;
		private System.Windows.Forms.Label lblMapIndex;
		private System.Windows.Forms.Label lblActive;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel4;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel5;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel6;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel7;
		private System.Windows.Forms.Label lbl_XOffset;
		private System.Windows.Forms.Label lbl_YOffset;
		private System.Windows.Forms.Button btn_ApplyOffset;
		private System.Windows.Forms.Label lbl_NewXOffset;
		private System.Windows.Forms.Label lbl_NewYOffset;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel8;
		private EzIna.GUI.UserControls.ucRoundedPanel ucRoundedPanel9;
		private System.Windows.Forms.Button btn_Set1stPos;
		private System.Windows.Forms.Button btn_Set2ndPos;
		private System.Windows.Forms.Button btn_Manual_StageCal_Reset;
	}
}