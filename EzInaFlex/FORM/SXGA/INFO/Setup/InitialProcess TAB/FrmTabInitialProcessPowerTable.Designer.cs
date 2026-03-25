namespace EzIna
{
    partial class FrmTabInitialProcessPowerTable
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
						System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTabInitialProcessPowerTable));
						System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
						System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
						System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
						this.treeView_Menu = new System.Windows.Forms.TreeView();
						this.imageList_For_TreeMenu = new System.Windows.Forms.ImageList(this.components);
						this.panel1 = new System.Windows.Forms.Panel();
						this.btn_TableSave = new System.Windows.Forms.Button();
						this.imageList_Recipe = new System.Windows.Forms.ImageList(this.components);
						this.DGV_PowerTableData = new System.Windows.Forms.DataGridView();
						this.PowerTableChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
						this.lb_SelSubCategory = new System.Windows.Forms.Label();
						this.panel7 = new System.Windows.Forms.Panel();
						this.GAUGE_SetPowerPercent = new EzIna.GUI.UserControls.AGauge();
						this.label5 = new System.Windows.Forms.Label();
						this.panel8 = new System.Windows.Forms.Panel();
						this.LED_SYSTEM_READY = new EzIna.GUI.UserControls.LedControls();
						this.label6 = new System.Windows.Forms.Label();
						this.panel3 = new System.Windows.Forms.Panel();
						this.btn_EmssionOff = new System.Windows.Forms.Button();
						this.LED_EMISSION = new EzIna.GUI.UserControls.LedControls();
						this.btn_EmssionOn = new System.Windows.Forms.Button();
						this.label1 = new System.Windows.Forms.Label();
						this.panel9 = new System.Windows.Forms.Panel();
						this.LED_LaserON = new EzIna.GUI.UserControls.LedControls();
						this.label7 = new System.Windows.Forms.Label();
						this.panel2 = new System.Windows.Forms.Panel();
						this.lb_OutTrigger_FREQ = new System.Windows.Forms.Label();
						this.label2 = new System.Windows.Forms.Label();
						this.panel4 = new System.Windows.Forms.Panel();
						this.lb_SettingTrigger_FREQ = new System.Windows.Forms.Label();
						this.label8 = new System.Windows.Forms.Label();
						this.Btn_ScannerLaserOn = new System.Windows.Forms.Button();
						this.Btn_ScannerLaserOff = new System.Windows.Forms.Button();
						this.Timer_Display = new System.Windows.Forms.Timer(this.components);
						this.panel1.SuspendLayout();
						((System.ComponentModel.ISupportInitialize)(this.DGV_PowerTableData)).BeginInit();
						((System.ComponentModel.ISupportInitialize)(this.PowerTableChart)).BeginInit();
						this.panel7.SuspendLayout();
						this.panel8.SuspendLayout();
						this.panel3.SuspendLayout();
						this.panel9.SuspendLayout();
						this.panel2.SuspendLayout();
						this.panel4.SuspendLayout();
						this.SuspendLayout();
						// 
						// treeView_Menu
						// 
						this.treeView_Menu.Location = new System.Drawing.Point(1, 1);
						this.treeView_Menu.Name = "treeView_Menu";
						this.treeView_Menu.Size = new System.Drawing.Size(287, 368);
						this.treeView_Menu.TabIndex = 97;
						this.treeView_Menu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Menu_NodeMouseClick);
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
						// panel1
						// 
						this.panel1.Controls.Add(this.btn_TableSave);
						this.panel1.Controls.Add(this.DGV_PowerTableData);
						this.panel1.Controls.Add(this.PowerTableChart);
						this.panel1.Controls.Add(this.lb_SelSubCategory);
						this.panel1.Location = new System.Drawing.Point(289, 0);
						this.panel1.Name = "panel1";
						this.panel1.Size = new System.Drawing.Size(993, 836);
						this.panel1.TabIndex = 99;
						// 
						// btn_TableSave
						// 
						this.btn_TableSave.BackColor = System.Drawing.Color.White;
						this.btn_TableSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.btn_TableSave.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.btn_TableSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.btn_TableSave.ImageIndex = 1;
						this.btn_TableSave.ImageList = this.imageList_Recipe;
						this.btn_TableSave.Location = new System.Drawing.Point(826, 775);
						this.btn_TableSave.Name = "btn_TableSave";
						this.btn_TableSave.Size = new System.Drawing.Size(164, 58);
						this.btn_TableSave.TabIndex = 106;
						this.btn_TableSave.Tag = "SAVE";
						this.btn_TableSave.Text = "Save";
						this.btn_TableSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
						this.btn_TableSave.UseVisualStyleBackColor = false;
						this.btn_TableSave.Click += new System.EventHandler(this.btn_TableSave_Click);
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
						// 
						// DGV_PowerTableData
						// 
						this.DGV_PowerTableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
						this.DGV_PowerTableData.Dock = System.Windows.Forms.DockStyle.Left;
						this.DGV_PowerTableData.Location = new System.Drawing.Point(0, 454);
						this.DGV_PowerTableData.Name = "DGV_PowerTableData";
						this.DGV_PowerTableData.RowTemplate.Height = 23;
						this.DGV_PowerTableData.Size = new System.Drawing.Size(823, 382);
						this.DGV_PowerTableData.TabIndex = 105;
						this.DGV_PowerTableData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DGV_PowerTableData_DataBindingComplete);
						// 
						// PowerTableChart
						// 
						this.PowerTableChart.BorderlineColor = System.Drawing.Color.SteelBlue;
						this.PowerTableChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
						this.PowerTableChart.BorderlineWidth = 3;
						this.PowerTableChart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
						chartArea1.Name = "ChartArea1";
						this.PowerTableChart.ChartAreas.Add(chartArea1);
						this.PowerTableChart.Dock = System.Windows.Forms.DockStyle.Top;
						legend1.Enabled = false;
						legend1.Name = "Legend1";
						this.PowerTableChart.Legends.Add(legend1);
						this.PowerTableChart.Location = new System.Drawing.Point(0, 39);
						this.PowerTableChart.Margin = new System.Windows.Forms.Padding(0);
						this.PowerTableChart.Name = "PowerTableChart";
						series1.BorderColor = System.Drawing.Color.Red;
						series1.BorderWidth = 2;
						series1.ChartArea = "ChartArea1";
						series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
						series1.Color = System.Drawing.Color.Red;
						series1.Legend = "Legend1";
						series1.Name = "Series1";
						this.PowerTableChart.Series.Add(series1);
						this.PowerTableChart.Size = new System.Drawing.Size(993, 415);
						this.PowerTableChart.TabIndex = 104;
						this.PowerTableChart.Text = "chart1";
						// 
						// lb_SelSubCategory
						// 
						this.lb_SelSubCategory.BackColor = System.Drawing.Color.SteelBlue;
						this.lb_SelSubCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.lb_SelSubCategory.Dock = System.Windows.Forms.DockStyle.Top;
						this.lb_SelSubCategory.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lb_SelSubCategory.ForeColor = System.Drawing.Color.White;
						this.lb_SelSubCategory.Location = new System.Drawing.Point(0, 0);
						this.lb_SelSubCategory.Margin = new System.Windows.Forms.Padding(0);
						this.lb_SelSubCategory.Name = "lb_SelSubCategory";
						this.lb_SelSubCategory.Size = new System.Drawing.Size(993, 39);
						this.lb_SelSubCategory.TabIndex = 103;
						this.lb_SelSubCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel7
						// 
						this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel7.Controls.Add(this.GAUGE_SetPowerPercent);
						this.panel7.Controls.Add(this.label5);
						this.panel7.Location = new System.Drawing.Point(103, 484);
						this.panel7.Margin = new System.Windows.Forms.Padding(1);
						this.panel7.Name = "panel7";
						this.panel7.Size = new System.Drawing.Size(183, 114);
						this.panel7.TabIndex = 112;
						// 
						// GAUGE_SetPowerPercent
						// 
						this.GAUGE_SetPowerPercent.BackColor = System.Drawing.Color.White;
						this.GAUGE_SetPowerPercent.BaseArcColor = System.Drawing.Color.Gray;
						this.GAUGE_SetPowerPercent.BaseArcRadius = 40;
						this.GAUGE_SetPowerPercent.BaseArcStart = 180;
						this.GAUGE_SetPowerPercent.BaseArcSweep = 180;
						this.GAUGE_SetPowerPercent.BaseArcWidth = 2;
						this.GAUGE_SetPowerPercent.Cap_Idx = ((byte)(1));
						this.GAUGE_SetPowerPercent.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
						this.GAUGE_SetPowerPercent.CapPosition = new System.Drawing.Point(10, 10);
						this.GAUGE_SetPowerPercent.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
						this.GAUGE_SetPowerPercent.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
						this.GAUGE_SetPowerPercent.CapText = "";
						this.GAUGE_SetPowerPercent.Center = new System.Drawing.Point(70, 70);
						this.GAUGE_SetPowerPercent.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.GAUGE_SetPowerPercent.Location = new System.Drawing.Point(19, 31);
						this.GAUGE_SetPowerPercent.MaxValue = 100F;
						this.GAUGE_SetPowerPercent.MinValue = 0F;
						this.GAUGE_SetPowerPercent.Name = "GAUGE_SetPowerPercent";
						this.GAUGE_SetPowerPercent.NeedleColor1 = EzIna.GUI.UserControls.AGauge.NeedleColorEnum.Green;
						this.GAUGE_SetPowerPercent.NeedleColor2 = System.Drawing.Color.GreenYellow;
						this.GAUGE_SetPowerPercent.NeedleRadius = 40;
						this.GAUGE_SetPowerPercent.NeedleType = 0;
						this.GAUGE_SetPowerPercent.NeedleWidth = 2;
						this.GAUGE_SetPowerPercent.Range_Idx = ((byte)(0));
						this.GAUGE_SetPowerPercent.RangeColor = System.Drawing.Color.LightGreen;
						this.GAUGE_SetPowerPercent.RangeEnabled = false;
						this.GAUGE_SetPowerPercent.RangeEndValue = 300F;
						this.GAUGE_SetPowerPercent.RangeInnerRadius = 70;
						this.GAUGE_SetPowerPercent.RangeOuterRadius = 80;
						this.GAUGE_SetPowerPercent.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
						this.GAUGE_SetPowerPercent.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
						this.GAUGE_SetPowerPercent.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
						this.GAUGE_SetPowerPercent.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
						this.GAUGE_SetPowerPercent.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
						this.GAUGE_SetPowerPercent.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
						this.GAUGE_SetPowerPercent.RangeStartValue = -100F;
						this.GAUGE_SetPowerPercent.ScaleLinesInterColor = System.Drawing.Color.DimGray;
						this.GAUGE_SetPowerPercent.ScaleLinesInterInnerRadius = 42;
						this.GAUGE_SetPowerPercent.ScaleLinesInterOuterRadius = 50;
						this.GAUGE_SetPowerPercent.ScaleLinesInterWidth = 1;
						this.GAUGE_SetPowerPercent.ScaleLinesMajorColor = System.Drawing.Color.Black;
						this.GAUGE_SetPowerPercent.ScaleLinesMajorInnerRadius = 40;
						this.GAUGE_SetPowerPercent.ScaleLinesMajorOuterRadius = 50;
						this.GAUGE_SetPowerPercent.ScaleLinesMajorStepValue = 25F;
						this.GAUGE_SetPowerPercent.ScaleLinesMajorWidth = 2;
						this.GAUGE_SetPowerPercent.ScaleLinesMinorColor = System.Drawing.Color.DimGray;
						this.GAUGE_SetPowerPercent.ScaleLinesMinorInnerRadius = 43;
						this.GAUGE_SetPowerPercent.ScaleLinesMinorNumOf = 4;
						this.GAUGE_SetPowerPercent.ScaleLinesMinorOuterRadius = 50;
						this.GAUGE_SetPowerPercent.ScaleLinesMinorWidth = 1;
						this.GAUGE_SetPowerPercent.ScaleNumbersColor = System.Drawing.Color.Black;
						this.GAUGE_SetPowerPercent.ScaleNumbersFormat = null;
						this.GAUGE_SetPowerPercent.ScaleNumbersRadius = 62;
						this.GAUGE_SetPowerPercent.ScaleNumbersRotation = 90;
						this.GAUGE_SetPowerPercent.ScaleNumbersStartScaleLine = 1;
						this.GAUGE_SetPowerPercent.ScaleNumbersStepScaleLines = 2;
						this.GAUGE_SetPowerPercent.Size = new System.Drawing.Size(146, 80);
						this.GAUGE_SetPowerPercent.TabIndex = 89;
						this.GAUGE_SetPowerPercent.Text = "aGauge7";
						this.GAUGE_SetPowerPercent.Value = 0F;
						this.GAUGE_SetPowerPercent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GAUGE_SetPowerPercent_MouseClick);
						// 
						// label5
						// 
						this.label5.BackColor = System.Drawing.Color.SteelBlue;
						this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label5.Dock = System.Windows.Forms.DockStyle.Top;
						this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label5.ForeColor = System.Drawing.Color.White;
						this.label5.Location = new System.Drawing.Point(0, 0);
						this.label5.Name = "label5";
						this.label5.Size = new System.Drawing.Size(181, 30);
						this.label5.TabIndex = 88;
						this.label5.Text = "Set Power Percent";
						this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel8
						// 
						this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel8.Controls.Add(this.LED_SYSTEM_READY);
						this.panel8.Controls.Add(this.label6);
						this.panel8.Location = new System.Drawing.Point(0, 371);
						this.panel8.Margin = new System.Windows.Forms.Padding(1);
						this.panel8.Name = "panel8";
						this.panel8.Size = new System.Drawing.Size(103, 112);
						this.panel8.TabIndex = 113;
						// 
						// LED_SYSTEM_READY
						// 
						this.LED_SYSTEM_READY.Location = new System.Drawing.Point(16, 38);
						this.LED_SYSTEM_READY.Name = "LED_SYSTEM_READY";
						this.LED_SYSTEM_READY.OffColor = System.Drawing.Color.Red;
						this.LED_SYSTEM_READY.OnColor = System.Drawing.Color.Lime;
						this.LED_SYSTEM_READY.Size = new System.Drawing.Size(61, 61);
						this.LED_SYSTEM_READY.TabIndex = 90;
						this.LED_SYSTEM_READY.Value = true;
						// 
						// label6
						// 
						this.label6.BackColor = System.Drawing.Color.SteelBlue;
						this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label6.Dock = System.Windows.Forms.DockStyle.Top;
						this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label6.ForeColor = System.Drawing.Color.White;
						this.label6.Location = new System.Drawing.Point(0, 0);
						this.label6.Name = "label6";
						this.label6.Size = new System.Drawing.Size(101, 30);
						this.label6.TabIndex = 88;
						this.label6.Text = "System Ready";
						this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel3
						// 
						this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel3.Controls.Add(this.btn_EmssionOff);
						this.panel3.Controls.Add(this.LED_EMISSION);
						this.panel3.Controls.Add(this.btn_EmssionOn);
						this.panel3.Controls.Add(this.label1);
						this.panel3.Location = new System.Drawing.Point(103, 371);
						this.panel3.Margin = new System.Windows.Forms.Padding(1);
						this.panel3.Name = "panel3";
						this.panel3.Size = new System.Drawing.Size(183, 112);
						this.panel3.TabIndex = 114;
						// 
						// btn_EmssionOff
						// 
						this.btn_EmssionOff.BackColor = System.Drawing.Color.White;
						this.btn_EmssionOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.btn_EmssionOff.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.btn_EmssionOff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.btn_EmssionOff.ImageIndex = 1;
						this.btn_EmssionOff.Location = new System.Drawing.Point(83, 72);
						this.btn_EmssionOff.Name = "btn_EmssionOff";
						this.btn_EmssionOff.Size = new System.Drawing.Size(92, 33);
						this.btn_EmssionOff.TabIndex = 119;
						this.btn_EmssionOff.Tag = "";
						this.btn_EmssionOff.Text = "Off";
						this.btn_EmssionOff.UseVisualStyleBackColor = false;
						this.btn_EmssionOff.Click += new System.EventHandler(this.btn_EmssionOff_Click);
						// 
						// LED_EMISSION
						// 
						this.LED_EMISSION.Location = new System.Drawing.Point(8, 37);
						this.LED_EMISSION.Name = "LED_EMISSION";
						this.LED_EMISSION.OffColor = System.Drawing.Color.DarkGray;
						this.LED_EMISSION.OnColor = System.Drawing.Color.Lime;
						this.LED_EMISSION.Size = new System.Drawing.Size(61, 61);
						this.LED_EMISSION.TabIndex = 90;
						this.LED_EMISSION.Value = false;
						// 
						// btn_EmssionOn
						// 
						this.btn_EmssionOn.BackColor = System.Drawing.Color.White;
						this.btn_EmssionOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.btn_EmssionOn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.btn_EmssionOn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.btn_EmssionOn.ImageIndex = 1;
						this.btn_EmssionOn.Location = new System.Drawing.Point(83, 33);
						this.btn_EmssionOn.Name = "btn_EmssionOn";
						this.btn_EmssionOn.Size = new System.Drawing.Size(92, 33);
						this.btn_EmssionOn.TabIndex = 118;
						this.btn_EmssionOn.Tag = "";
						this.btn_EmssionOn.Text = "On";
						this.btn_EmssionOn.UseVisualStyleBackColor = false;
						this.btn_EmssionOn.Click += new System.EventHandler(this.btn_EmssionOn_Click);
						// 
						// label1
						// 
						this.label1.BackColor = System.Drawing.Color.SteelBlue;
						this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label1.Dock = System.Windows.Forms.DockStyle.Top;
						this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label1.ForeColor = System.Drawing.Color.White;
						this.label1.Location = new System.Drawing.Point(0, 0);
						this.label1.Name = "label1";
						this.label1.Size = new System.Drawing.Size(181, 30);
						this.label1.TabIndex = 88;
						this.label1.Text = "Emission On / Off";
						this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel9
						// 
						this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel9.Controls.Add(this.LED_LaserON);
						this.panel9.Controls.Add(this.label7);
						this.panel9.Location = new System.Drawing.Point(0, 484);
						this.panel9.Margin = new System.Windows.Forms.Padding(1);
						this.panel9.Name = "panel9";
						this.panel9.Size = new System.Drawing.Size(103, 114);
						this.panel9.TabIndex = 115;
						// 
						// LED_LaserON
						// 
						this.LED_LaserON.Location = new System.Drawing.Point(16, 40);
						this.LED_LaserON.Name = "LED_LaserON";
						this.LED_LaserON.OffColor = System.Drawing.Color.DarkGray;
						this.LED_LaserON.OnColor = System.Drawing.Color.DarkOrange;
						this.LED_LaserON.Size = new System.Drawing.Size(61, 61);
						this.LED_LaserON.TabIndex = 92;
						this.LED_LaserON.Value = true;
						// 
						// label7
						// 
						this.label7.BackColor = System.Drawing.Color.SteelBlue;
						this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label7.Dock = System.Windows.Forms.DockStyle.Top;
						this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label7.ForeColor = System.Drawing.Color.White;
						this.label7.Location = new System.Drawing.Point(0, 0);
						this.label7.Name = "label7";
						this.label7.Size = new System.Drawing.Size(101, 30);
						this.label7.TabIndex = 88;
						this.label7.Text = "Laser On";
						this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel2
						// 
						this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel2.Controls.Add(this.lb_OutTrigger_FREQ);
						this.panel2.Controls.Add(this.label2);
						this.panel2.Location = new System.Drawing.Point(0, 599);
						this.panel2.Margin = new System.Windows.Forms.Padding(1);
						this.panel2.Name = "panel2";
						this.panel2.Size = new System.Drawing.Size(286, 63);
						this.panel2.TabIndex = 116;
						// 
						// lb_OutTrigger_FREQ
						// 
						this.lb_OutTrigger_FREQ.Dock = System.Windows.Forms.DockStyle.Fill;
						this.lb_OutTrigger_FREQ.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lb_OutTrigger_FREQ.Location = new System.Drawing.Point(0, 30);
						this.lb_OutTrigger_FREQ.Name = "lb_OutTrigger_FREQ";
						this.lb_OutTrigger_FREQ.Size = new System.Drawing.Size(284, 31);
						this.lb_OutTrigger_FREQ.TabIndex = 118;
						this.lb_OutTrigger_FREQ.Text = "label3";
						this.lb_OutTrigger_FREQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// label2
						// 
						this.label2.BackColor = System.Drawing.Color.SteelBlue;
						this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label2.Dock = System.Windows.Forms.DockStyle.Top;
						this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label2.ForeColor = System.Drawing.Color.White;
						this.label2.Location = new System.Drawing.Point(0, 0);
						this.label2.Name = "label2";
						this.label2.Size = new System.Drawing.Size(284, 30);
						this.label2.TabIndex = 88;
						this.label2.Text = "Out Trigger Frequency";
						this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel4
						// 
						this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel4.Controls.Add(this.lb_SettingTrigger_FREQ);
						this.panel4.Controls.Add(this.label8);
						this.panel4.Location = new System.Drawing.Point(1, 663);
						this.panel4.Margin = new System.Windows.Forms.Padding(1);
						this.panel4.Name = "panel4";
						this.panel4.Size = new System.Drawing.Size(286, 63);
						this.panel4.TabIndex = 117;
						// 
						// lb_SettingTrigger_FREQ
						// 
						this.lb_SettingTrigger_FREQ.Dock = System.Windows.Forms.DockStyle.Fill;
						this.lb_SettingTrigger_FREQ.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lb_SettingTrigger_FREQ.Location = new System.Drawing.Point(0, 30);
						this.lb_SettingTrigger_FREQ.Name = "lb_SettingTrigger_FREQ";
						this.lb_SettingTrigger_FREQ.Size = new System.Drawing.Size(284, 31);
						this.lb_SettingTrigger_FREQ.TabIndex = 118;
						this.lb_SettingTrigger_FREQ.Text = "label4";
						this.lb_SettingTrigger_FREQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						this.lb_SettingTrigger_FREQ.Click += new System.EventHandler(this.lb_SettingTrigger_FREQ_Click);
						// 
						// label8
						// 
						this.label8.BackColor = System.Drawing.Color.SteelBlue;
						this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label8.Dock = System.Windows.Forms.DockStyle.Top;
						this.label8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label8.ForeColor = System.Drawing.Color.White;
						this.label8.Location = new System.Drawing.Point(0, 0);
						this.label8.Name = "label8";
						this.label8.Size = new System.Drawing.Size(284, 30);
						this.label8.TabIndex = 88;
						this.label8.Text = "Setting Trigger Frequency";
						this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// Btn_ScannerLaserOn
						// 
						this.Btn_ScannerLaserOn.BackColor = System.Drawing.Color.White;
						this.Btn_ScannerLaserOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.Btn_ScannerLaserOn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.Btn_ScannerLaserOn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.Btn_ScannerLaserOn.ImageIndex = 1;
						this.Btn_ScannerLaserOn.Location = new System.Drawing.Point(1, 730);
						this.Btn_ScannerLaserOn.Name = "Btn_ScannerLaserOn";
						this.Btn_ScannerLaserOn.Size = new System.Drawing.Size(139, 45);
						this.Btn_ScannerLaserOn.TabIndex = 120;
						this.Btn_ScannerLaserOn.Tag = "";
						this.Btn_ScannerLaserOn.Text = "Laser On";
						this.Btn_ScannerLaserOn.UseVisualStyleBackColor = false;
						this.Btn_ScannerLaserOn.Click += new System.EventHandler(this.Btn_ScannerLaserOn_Click);
						// 
						// Btn_ScannerLaserOff
						// 
						this.Btn_ScannerLaserOff.BackColor = System.Drawing.Color.White;
						this.Btn_ScannerLaserOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.Btn_ScannerLaserOff.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.Btn_ScannerLaserOff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.Btn_ScannerLaserOff.ImageIndex = 1;
						this.Btn_ScannerLaserOff.Location = new System.Drawing.Point(146, 730);
						this.Btn_ScannerLaserOff.Name = "Btn_ScannerLaserOff";
						this.Btn_ScannerLaserOff.Size = new System.Drawing.Size(139, 45);
						this.Btn_ScannerLaserOff.TabIndex = 121;
						this.Btn_ScannerLaserOff.Tag = "";
						this.Btn_ScannerLaserOff.Text = "Laser Off";
						this.Btn_ScannerLaserOff.UseVisualStyleBackColor = false;
						this.Btn_ScannerLaserOff.Click += new System.EventHandler(this.Btn_ScannerLaserOff_Click);
						// 
						// FrmTabInitialProcessPowerTable
						// 
						this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
						this.ClientSize = new System.Drawing.Size(1280, 870);
						this.Controls.Add(this.Btn_ScannerLaserOff);
						this.Controls.Add(this.Btn_ScannerLaserOn);
						this.Controls.Add(this.panel4);
						this.Controls.Add(this.panel2);
						this.Controls.Add(this.panel9);
						this.Controls.Add(this.panel3);
						this.Controls.Add(this.panel8);
						this.Controls.Add(this.panel7);
						this.Controls.Add(this.panel1);
						this.Controls.Add(this.treeView_Menu);
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
						this.Name = "FrmTabInitialProcessPowerTable";
						this.Text = "Form1";
						this.Load += new System.EventHandler(this.Form_Load);
						this.VisibleChanged += new System.EventHandler(this.Form_VisibleChanged);
						this.panel1.ResumeLayout(false);
						((System.ComponentModel.ISupportInitialize)(this.DGV_PowerTableData)).EndInit();
						((System.ComponentModel.ISupportInitialize)(this.PowerTableChart)).EndInit();
						this.panel7.ResumeLayout(false);
						this.panel8.ResumeLayout(false);
						this.panel3.ResumeLayout(false);
						this.panel9.ResumeLayout(false);
						this.panel2.ResumeLayout(false);
						this.panel4.ResumeLayout(false);
						this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView treeView_Menu;
        private System.Windows.Forms.ImageList imageList_For_TreeMenu;
				private System.Windows.Forms.Panel panel1;
				private System.Windows.Forms.DataGridView DGV_PowerTableData;
				private System.Windows.Forms.DataVisualization.Charting.Chart PowerTableChart;
				private System.Windows.Forms.Label lb_SelSubCategory;
				private System.Windows.Forms.ImageList imageList_Recipe;
				private System.Windows.Forms.Button btn_TableSave;
				private System.Windows.Forms.Panel panel7;
				private GUI.UserControls.AGauge GAUGE_SetPowerPercent;
				private System.Windows.Forms.Label label5;
				private System.Windows.Forms.Panel panel8;
				private GUI.UserControls.LedControls LED_SYSTEM_READY;
				private System.Windows.Forms.Label label6;
				private System.Windows.Forms.Panel panel3;
				private GUI.UserControls.LedControls LED_EMISSION;
				private System.Windows.Forms.Label label1;
				private System.Windows.Forms.Panel panel9;
				private GUI.UserControls.LedControls LED_LaserON;
				private System.Windows.Forms.Label label7;
				private System.Windows.Forms.Panel panel2;
				private System.Windows.Forms.Label lb_OutTrigger_FREQ;
				private System.Windows.Forms.Label label2;
				private System.Windows.Forms.Panel panel4;
				private System.Windows.Forms.Label lb_SettingTrigger_FREQ;
				private System.Windows.Forms.Label label8;
				private System.Windows.Forms.Button btn_EmssionOn;
				private System.Windows.Forms.Button btn_EmssionOff;
				private System.Windows.Forms.Button Btn_ScannerLaserOn;
				private System.Windows.Forms.Button Btn_ScannerLaserOff;
				private System.Windows.Forms.Timer Timer_Display;
		}
}