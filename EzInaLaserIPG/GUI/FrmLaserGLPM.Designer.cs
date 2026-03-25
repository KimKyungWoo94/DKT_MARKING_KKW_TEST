namespace EzIna.Laser.IPG.GUI
{
    partial class FrmLaserGLPM
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.GAUGE_SetPowerPercent = new EzIna.GUI.UserControls.AGauge();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnEmssionOff = new EzIna.GUI.UserControls.GlassButton();
            this.LED_EMISSION = new EzIna.GUI.UserControls.LedControls();
            this.BtnEmssionOn = new EzIna.GUI.UserControls.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.LED_WarmupMode = new EzIna.GUI.UserControls.LedControls();
            this.label7 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.LED_SYSTEM_READY = new EzIna.GUI.UserControls.LedControls();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LED_CONNECT = new EzIna.GUI.UserControls.LedControls();
            this.label_RecipeList = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.DGV_LaserParam = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_LaserParam)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 908);
            this.panel1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.GAUGE_SetPowerPercent);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 382);
            this.panel7.Margin = new System.Windows.Forms.Padding(1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(183, 125);
            this.panel7.TabIndex = 114;
            // 
            // GAUGE_SetPowerPercent
            // 
            this.GAUGE_SetPowerPercent.BackColor = System.Drawing.SystemColors.Control;
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
            this.GAUGE_SetPowerPercent.Location = new System.Drawing.Point(20, 39);
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
            this.GAUGE_SetPowerPercent.Size = new System.Drawing.Size(144, 78);
            this.GAUGE_SetPowerPercent.TabIndex = 89;
            this.GAUGE_SetPowerPercent.Text = "aGauge7";
            this.GAUGE_SetPowerPercent.Value = 0F;
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
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.BtnEmssionOff);
            this.panel3.Controls.Add(this.LED_EMISSION);
            this.panel3.Controls.Add(this.BtnEmssionOn);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 270);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 112);
            this.panel3.TabIndex = 110;
            // 
            // BtnEmssionOff
            // 
            this.BtnEmssionOff.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEmssionOff.Location = new System.Drawing.Point(84, 73);
            this.BtnEmssionOff.Name = "BtnEmssionOff";
            this.BtnEmssionOff.Size = new System.Drawing.Size(92, 33);
            this.BtnEmssionOff.TabIndex = 94;
            this.BtnEmssionOff.Text = "Off";
            // 
            // LED_EMISSION
            // 
            this.LED_EMISSION.Location = new System.Drawing.Point(3, 37);
            this.LED_EMISSION.Name = "LED_EMISSION";
            this.LED_EMISSION.OffColor = System.Drawing.Color.DarkGray;
            this.LED_EMISSION.OnColor = System.Drawing.Color.Lime;
            this.LED_EMISSION.Size = new System.Drawing.Size(61, 61);
            this.LED_EMISSION.TabIndex = 90;
            this.LED_EMISSION.Value = false;
            // 
            // BtnEmssionOn
            // 
            this.BtnEmssionOn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEmssionOn.Location = new System.Drawing.Point(84, 35);
            this.BtnEmssionOn.Name = "BtnEmssionOn";
            this.BtnEmssionOn.Size = new System.Drawing.Size(92, 33);
            this.BtnEmssionOn.TabIndex = 93;
            this.BtnEmssionOn.Text = "On";
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
            this.label1.Text = "Laser (Emission) On / Off";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.LED_WarmupMode);
            this.panel9.Controls.Add(this.label7);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 180);
            this.panel9.Margin = new System.Windows.Forms.Padding(1);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(183, 90);
            this.panel9.TabIndex = 109;
            // 
            // LED_WarmupMode
            // 
            this.LED_WarmupMode.Location = new System.Drawing.Point(55, 29);
            this.LED_WarmupMode.Name = "LED_WarmupMode";
            this.LED_WarmupMode.OffColor = System.Drawing.Color.DarkGray;
            this.LED_WarmupMode.OnColor = System.Drawing.Color.DarkOrange;
            this.LED_WarmupMode.Size = new System.Drawing.Size(61, 61);
            this.LED_WarmupMode.TabIndex = 92;
            this.LED_WarmupMode.Value = true;
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
            this.label7.Size = new System.Drawing.Size(181, 30);
            this.label7.TabIndex = 88;
            this.label7.Text = "WarmUpMode";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.LED_SYSTEM_READY);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 90);
            this.panel8.Margin = new System.Windows.Forms.Padding(1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(183, 90);
            this.panel8.TabIndex = 102;
            // 
            // LED_SYSTEM_READY
            // 
            this.LED_SYSTEM_READY.Location = new System.Drawing.Point(55, 30);
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
            this.label6.Size = new System.Drawing.Size(181, 30);
            this.label6.TabIndex = 88;
            this.label6.Text = "System Ready";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LED_CONNECT);
            this.panel2.Controls.Add(this.label_RecipeList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(183, 90);
            this.panel2.TabIndex = 101;
            // 
            // LED_CONNECT
            // 
            this.LED_CONNECT.Location = new System.Drawing.Point(55, 29);
            this.LED_CONNECT.Name = "LED_CONNECT";
            this.LED_CONNECT.OffColor = System.Drawing.Color.Red;
            this.LED_CONNECT.OnColor = System.Drawing.Color.Lime;
            this.LED_CONNECT.Size = new System.Drawing.Size(61, 61);
            this.LED_CONNECT.TabIndex = 90;
            this.LED_CONNECT.Value = true;
            // 
            // label_RecipeList
            // 
            this.label_RecipeList.BackColor = System.Drawing.Color.SteelBlue;
            this.label_RecipeList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_RecipeList.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_RecipeList.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RecipeList.ForeColor = System.Drawing.Color.White;
            this.label_RecipeList.Location = new System.Drawing.Point(0, 0);
            this.label_RecipeList.Name = "label_RecipeList";
            this.label_RecipeList.Size = new System.Drawing.Size(181, 30);
            this.label_RecipeList.TabIndex = 88;
            this.label_RecipeList.Text = "Comm Connect";
            this.label_RecipeList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.DGV_LaserParam);
            this.panel11.Controls.Add(this.label8);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(183, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(832, 908);
            this.panel11.TabIndex = 2;
            // 
            // DGV_LaserParam
            // 
            this.DGV_LaserParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_LaserParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_LaserParam.Location = new System.Drawing.Point(0, 30);
            this.DGV_LaserParam.Margin = new System.Windows.Forms.Padding(1);
            this.DGV_LaserParam.Name = "DGV_LaserParam";
            this.DGV_LaserParam.RowTemplate.Height = 23;
            this.DGV_LaserParam.Size = new System.Drawing.Size(832, 878);
            this.DGV_LaserParam.TabIndex = 94;
            this.DGV_LaserParam.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGV_LaserParam_CellFormatting);
            this.DGV_LaserParam.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_LaserParam_CellMouseClick);
            this.DGV_LaserParam.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DGV_LaserParam_CellPainting);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.SteelBlue;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(832, 30);
            this.label8.TabIndex = 93;
            this.label8.Text = "Parameter";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmLaserGLPM
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1015, 908);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLaserGLPM";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.FrmLaserTalon_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_LaserParam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel8;
        private EzIna.GUI.UserControls.LedControls LED_SYSTEM_READY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private EzIna.GUI.UserControls.LedControls LED_CONNECT;
        private System.Windows.Forms.Label label_RecipeList;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.DataGridView DGV_LaserParam;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private EzIna.GUI.UserControls.GlassButton BtnEmssionOff;
        private EzIna.GUI.UserControls.LedControls LED_EMISSION;
        private EzIna.GUI.UserControls.GlassButton BtnEmssionOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel9;
        private EzIna.GUI.UserControls.LedControls LED_WarmupMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel7;
        private EzIna.GUI.UserControls.AGauge GAUGE_SetPowerPercent;
        private System.Windows.Forms.Label label5;
    }
}