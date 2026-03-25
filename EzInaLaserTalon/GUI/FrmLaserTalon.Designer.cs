namespace EzIna.Laser.Talon.GUI
{
    partial class FrmLaserTalon
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
						this.GAUGE_OutPower = new EzIna.GUI.UserControls.AGauge();
						this.label5 = new System.Windows.Forms.Label();
						this.panel9 = new System.Windows.Forms.Panel();
						this.lb_WarmUpTime = new System.Windows.Forms.Label();
						this.label7 = new System.Windows.Forms.Label();
						this.panel6 = new System.Windows.Forms.Panel();
						this.BtnExtGateDisable = new EzIna.GUI.UserControls.GlassButton();
						this.BtnExtGateEnable = new EzIna.GUI.UserControls.GlassButton();
						this.LED_EXT_GATE = new EzIna.GUI.UserControls.LedControls();
						this.label4 = new System.Windows.Forms.Label();
						this.panel5 = new System.Windows.Forms.Panel();
						this.BtnGateClose = new EzIna.GUI.UserControls.GlassButton();
						this.BtnGateOpen = new EzIna.GUI.UserControls.GlassButton();
						this.LED_GATE = new EzIna.GUI.UserControls.LedControls();
						this.label3 = new System.Windows.Forms.Label();
						this.panel4 = new System.Windows.Forms.Panel();
						this.BtnShutterClose = new EzIna.GUI.UserControls.GlassButton();
						this.BtnShutterOpen = new EzIna.GUI.UserControls.GlassButton();
						this.LED_SHUTTER = new EzIna.GUI.UserControls.LedControls();
						this.label2 = new System.Windows.Forms.Label();
						this.panel3 = new System.Windows.Forms.Panel();
						this.BtnEmssionOff = new EzIna.GUI.UserControls.GlassButton();
						this.LED_EMISSION = new EzIna.GUI.UserControls.LedControls();
						this.BtnEmssionOn = new EzIna.GUI.UserControls.GlassButton();
						this.label1 = new System.Windows.Forms.Label();
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
						this.panel9.SuspendLayout();
						this.panel6.SuspendLayout();
						this.panel5.SuspendLayout();
						this.panel4.SuspendLayout();
						this.panel3.SuspendLayout();
						this.panel8.SuspendLayout();
						this.panel2.SuspendLayout();
						this.panel11.SuspendLayout();
						((System.ComponentModel.ISupportInitialize)(this.DGV_LaserParam)).BeginInit();
						this.SuspendLayout();
						// 
						// panel1
						// 
						this.panel1.Controls.Add(this.panel7);
						this.panel1.Controls.Add(this.panel9);
						this.panel1.Controls.Add(this.panel6);
						this.panel1.Controls.Add(this.panel5);
						this.panel1.Controls.Add(this.panel4);
						this.panel1.Controls.Add(this.panel3);
						this.panel1.Controls.Add(this.panel8);
						this.panel1.Controls.Add(this.panel2);
						this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
						this.panel1.Location = new System.Drawing.Point(0, 0);
						this.panel1.Name = "panel1";
						this.panel1.Size = new System.Drawing.Size(183, 864);
						this.panel1.TabIndex = 0;
						// 
						// panel7
						// 
						this.panel7.BackColor = System.Drawing.Color.White;
						this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel7.Controls.Add(this.GAUGE_OutPower);
						this.panel7.Controls.Add(this.label5);
						this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel7.Location = new System.Drawing.Point(0, 725);
						this.panel7.Margin = new System.Windows.Forms.Padding(1);
						this.panel7.Name = "panel7";
						this.panel7.Size = new System.Drawing.Size(183, 125);
						this.panel7.TabIndex = 109;
						// 
						// GAUGE_OutPower
						// 
						this.GAUGE_OutPower.BackColor = System.Drawing.Color.White;
						this.GAUGE_OutPower.BaseArcColor = System.Drawing.Color.Gray;
						this.GAUGE_OutPower.BaseArcRadius = 40;
						this.GAUGE_OutPower.BaseArcStart = 180;
						this.GAUGE_OutPower.BaseArcSweep = 180;
						this.GAUGE_OutPower.BaseArcWidth = 2;
						this.GAUGE_OutPower.Cap_Idx = ((byte)(1));
						this.GAUGE_OutPower.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
						this.GAUGE_OutPower.CapPosition = new System.Drawing.Point(10, 10);
						this.GAUGE_OutPower.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
						this.GAUGE_OutPower.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
						this.GAUGE_OutPower.CapText = "";
						this.GAUGE_OutPower.Center = new System.Drawing.Point(70, 70);
						this.GAUGE_OutPower.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.GAUGE_OutPower.Location = new System.Drawing.Point(20, 39);
						this.GAUGE_OutPower.MaxValue = 25F;
						this.GAUGE_OutPower.MinValue = 0F;
						this.GAUGE_OutPower.Name = "GAUGE_OutPower";
						this.GAUGE_OutPower.NeedleColor1 = EzIna.GUI.UserControls.AGauge.NeedleColorEnum.Green;
						this.GAUGE_OutPower.NeedleColor2 = System.Drawing.Color.GreenYellow;
						this.GAUGE_OutPower.NeedleRadius = 40;
						this.GAUGE_OutPower.NeedleType = 0;
						this.GAUGE_OutPower.NeedleWidth = 2;
						this.GAUGE_OutPower.Range_Idx = ((byte)(0));
						this.GAUGE_OutPower.RangeColor = System.Drawing.Color.LightGreen;
						this.GAUGE_OutPower.RangeEnabled = false;
						this.GAUGE_OutPower.RangeEndValue = 300F;
						this.GAUGE_OutPower.RangeInnerRadius = 70;
						this.GAUGE_OutPower.RangeOuterRadius = 80;
						this.GAUGE_OutPower.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
						this.GAUGE_OutPower.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
						this.GAUGE_OutPower.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
						this.GAUGE_OutPower.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
						this.GAUGE_OutPower.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
						this.GAUGE_OutPower.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
						this.GAUGE_OutPower.RangeStartValue = -100F;
						this.GAUGE_OutPower.ScaleLinesInterColor = System.Drawing.Color.DimGray;
						this.GAUGE_OutPower.ScaleLinesInterInnerRadius = 42;
						this.GAUGE_OutPower.ScaleLinesInterOuterRadius = 50;
						this.GAUGE_OutPower.ScaleLinesInterWidth = 1;
						this.GAUGE_OutPower.ScaleLinesMajorColor = System.Drawing.Color.Black;
						this.GAUGE_OutPower.ScaleLinesMajorInnerRadius = 40;
						this.GAUGE_OutPower.ScaleLinesMajorOuterRadius = 50;
						this.GAUGE_OutPower.ScaleLinesMajorStepValue = 5F;
						this.GAUGE_OutPower.ScaleLinesMajorWidth = 2;
						this.GAUGE_OutPower.ScaleLinesMinorColor = System.Drawing.Color.DimGray;
						this.GAUGE_OutPower.ScaleLinesMinorInnerRadius = 43;
						this.GAUGE_OutPower.ScaleLinesMinorNumOf = 4;
						this.GAUGE_OutPower.ScaleLinesMinorOuterRadius = 50;
						this.GAUGE_OutPower.ScaleLinesMinorWidth = 1;
						this.GAUGE_OutPower.ScaleNumbersColor = System.Drawing.Color.Black;
						this.GAUGE_OutPower.ScaleNumbersFormat = null;
						this.GAUGE_OutPower.ScaleNumbersRadius = 62;
						this.GAUGE_OutPower.ScaleNumbersRotation = 90;
						this.GAUGE_OutPower.ScaleNumbersStartScaleLine = 1;
						this.GAUGE_OutPower.ScaleNumbersStepScaleLines = 2;
						this.GAUGE_OutPower.Size = new System.Drawing.Size(144, 78);
						this.GAUGE_OutPower.TabIndex = 89;
						this.GAUGE_OutPower.Text = "aGauge7";
						this.GAUGE_OutPower.Value = 0F;
						// 
						// label5
						// 
						this.label5.BackColor = System.Drawing.Color.RoyalBlue;
						this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label5.Dock = System.Windows.Forms.DockStyle.Top;
						this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label5.ForeColor = System.Drawing.Color.White;
						this.label5.Location = new System.Drawing.Point(0, 0);
						this.label5.Name = "label5";
						this.label5.Size = new System.Drawing.Size(181, 30);
						this.label5.TabIndex = 88;
						this.label5.Text = "Output Power";
						this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel9
						// 
						this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel9.Controls.Add(this.lb_WarmUpTime);
						this.panel9.Controls.Add(this.label7);
						this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel9.Location = new System.Drawing.Point(0, 620);
						this.panel9.Margin = new System.Windows.Forms.Padding(1);
						this.panel9.Name = "panel9";
						this.panel9.Size = new System.Drawing.Size(183, 105);
						this.panel9.TabIndex = 108;
						// 
						// lb_WarmUpTime
						// 
						this.lb_WarmUpTime.BackColor = System.Drawing.Color.White;
						this.lb_WarmUpTime.Dock = System.Windows.Forms.DockStyle.Fill;
						this.lb_WarmUpTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lb_WarmUpTime.Location = new System.Drawing.Point(0, 30);
						this.lb_WarmUpTime.Name = "lb_WarmUpTime";
						this.lb_WarmUpTime.Size = new System.Drawing.Size(181, 73);
						this.lb_WarmUpTime.TabIndex = 89;
						this.lb_WarmUpTime.Text = "label9";
						this.lb_WarmUpTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// label7
						// 
						this.label7.BackColor = System.Drawing.Color.RoyalBlue;
						this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label7.Dock = System.Windows.Forms.DockStyle.Top;
						this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label7.ForeColor = System.Drawing.Color.White;
						this.label7.Location = new System.Drawing.Point(0, 0);
						this.label7.Name = "label7";
						this.label7.Size = new System.Drawing.Size(181, 30);
						this.label7.TabIndex = 88;
						this.label7.Text = "Warm Up Time";
						this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel6
						// 
						this.panel6.BackColor = System.Drawing.Color.White;
						this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel6.Controls.Add(this.BtnExtGateDisable);
						this.panel6.Controls.Add(this.BtnExtGateEnable);
						this.panel6.Controls.Add(this.LED_EXT_GATE);
						this.panel6.Controls.Add(this.label4);
						this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel6.Location = new System.Drawing.Point(0, 515);
						this.panel6.Margin = new System.Windows.Forms.Padding(1);
						this.panel6.Name = "panel6";
						this.panel6.Size = new System.Drawing.Size(183, 105);
						this.panel6.TabIndex = 106;
						// 
						// BtnExtGateDisable
						// 
						this.BtnExtGateDisable.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnExtGateDisable.Location = new System.Drawing.Point(84, 69);
						this.BtnExtGateDisable.Name = "BtnExtGateDisable";
						this.BtnExtGateDisable.Size = new System.Drawing.Size(92, 33);
						this.BtnExtGateDisable.TabIndex = 96;
						this.BtnExtGateDisable.Text = "Disable";
						// 
						// BtnExtGateEnable
						// 
						this.BtnExtGateEnable.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnExtGateEnable.Location = new System.Drawing.Point(84, 33);
						this.BtnExtGateEnable.Name = "BtnExtGateEnable";
						this.BtnExtGateEnable.Size = new System.Drawing.Size(92, 33);
						this.BtnExtGateEnable.TabIndex = 95;
						this.BtnExtGateEnable.Text = "Enable";
						// 
						// LED_EXT_GATE
						// 
						this.LED_EXT_GATE.Location = new System.Drawing.Point(3, 42);
						this.LED_EXT_GATE.Name = "LED_EXT_GATE";
						this.LED_EXT_GATE.OffColor = System.Drawing.Color.DarkGray;
						this.LED_EXT_GATE.OnColor = System.Drawing.Color.Lime;
						this.LED_EXT_GATE.Size = new System.Drawing.Size(61, 61);
						this.LED_EXT_GATE.TabIndex = 90;
						this.LED_EXT_GATE.Value = true;
						// 
						// label4
						// 
						this.label4.BackColor = System.Drawing.Color.RoyalBlue;
						this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label4.Dock = System.Windows.Forms.DockStyle.Top;
						this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label4.ForeColor = System.Drawing.Color.White;
						this.label4.Location = new System.Drawing.Point(0, 0);
						this.label4.Name = "label4";
						this.label4.Size = new System.Drawing.Size(181, 30);
						this.label4.TabIndex = 88;
						this.label4.Text = "External Gate";
						this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel5
						// 
						this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel5.Controls.Add(this.BtnGateClose);
						this.panel5.Controls.Add(this.BtnGateOpen);
						this.panel5.Controls.Add(this.LED_GATE);
						this.panel5.Controls.Add(this.label3);
						this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel5.Location = new System.Drawing.Point(0, 410);
						this.panel5.Margin = new System.Windows.Forms.Padding(1);
						this.panel5.Name = "panel5";
						this.panel5.Size = new System.Drawing.Size(183, 105);
						this.panel5.TabIndex = 105;
						// 
						// BtnGateClose
						// 
						this.BtnGateClose.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnGateClose.Location = new System.Drawing.Point(84, 67);
						this.BtnGateClose.Name = "BtnGateClose";
						this.BtnGateClose.Size = new System.Drawing.Size(92, 33);
						this.BtnGateClose.TabIndex = 94;
						this.BtnGateClose.Text = "Close";
						// 
						// BtnGateOpen
						// 
						this.BtnGateOpen.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnGateOpen.Location = new System.Drawing.Point(84, 32);
						this.BtnGateOpen.Name = "BtnGateOpen";
						this.BtnGateOpen.Size = new System.Drawing.Size(92, 33);
						this.BtnGateOpen.TabIndex = 93;
						this.BtnGateOpen.Text = "Open";
						// 
						// LED_GATE
						// 
						this.LED_GATE.Location = new System.Drawing.Point(3, 42);
						this.LED_GATE.Name = "LED_GATE";
						this.LED_GATE.OffColor = System.Drawing.Color.DarkGray;
						this.LED_GATE.OnColor = System.Drawing.Color.Lime;
						this.LED_GATE.Size = new System.Drawing.Size(61, 61);
						this.LED_GATE.TabIndex = 90;
						this.LED_GATE.Value = true;
						// 
						// label3
						// 
						this.label3.BackColor = System.Drawing.Color.RoyalBlue;
						this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label3.Dock = System.Windows.Forms.DockStyle.Top;
						this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label3.ForeColor = System.Drawing.Color.White;
						this.label3.Location = new System.Drawing.Point(0, 0);
						this.label3.Name = "label3";
						this.label3.Size = new System.Drawing.Size(181, 30);
						this.label3.TabIndex = 88;
						this.label3.Text = "Gate";
						this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel4
						// 
						this.panel4.BackColor = System.Drawing.Color.White;
						this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel4.Controls.Add(this.BtnShutterClose);
						this.panel4.Controls.Add(this.BtnShutterOpen);
						this.panel4.Controls.Add(this.LED_SHUTTER);
						this.panel4.Controls.Add(this.label2);
						this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel4.Location = new System.Drawing.Point(0, 305);
						this.panel4.Margin = new System.Windows.Forms.Padding(1);
						this.panel4.Name = "panel4";
						this.panel4.Size = new System.Drawing.Size(183, 105);
						this.panel4.TabIndex = 104;
						// 
						// BtnShutterClose
						// 
						this.BtnShutterClose.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnShutterClose.Location = new System.Drawing.Point(84, 68);
						this.BtnShutterClose.Name = "BtnShutterClose";
						this.BtnShutterClose.Size = new System.Drawing.Size(92, 33);
						this.BtnShutterClose.TabIndex = 92;
						this.BtnShutterClose.Text = "Close";
						// 
						// BtnShutterOpen
						// 
						this.BtnShutterOpen.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnShutterOpen.Location = new System.Drawing.Point(84, 32);
						this.BtnShutterOpen.Name = "BtnShutterOpen";
						this.BtnShutterOpen.Size = new System.Drawing.Size(92, 33);
						this.BtnShutterOpen.TabIndex = 91;
						this.BtnShutterOpen.Text = "Open";
						// 
						// LED_SHUTTER
						// 
						this.LED_SHUTTER.Location = new System.Drawing.Point(3, 38);
						this.LED_SHUTTER.Name = "LED_SHUTTER";
						this.LED_SHUTTER.OffColor = System.Drawing.Color.DarkGray;
						this.LED_SHUTTER.OnColor = System.Drawing.Color.Lime;
						this.LED_SHUTTER.Size = new System.Drawing.Size(61, 61);
						this.LED_SHUTTER.TabIndex = 90;
						this.LED_SHUTTER.Value = true;
						// 
						// label2
						// 
						this.label2.BackColor = System.Drawing.Color.RoyalBlue;
						this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
						this.label2.Dock = System.Windows.Forms.DockStyle.Top;
						this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label2.ForeColor = System.Drawing.Color.White;
						this.label2.Location = new System.Drawing.Point(0, 0);
						this.label2.Name = "label2";
						this.label2.Size = new System.Drawing.Size(181, 30);
						this.label2.TabIndex = 88;
						this.label2.Text = "Shutter";
						this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// panel3
						// 
						this.panel3.BackColor = System.Drawing.Color.White;
						this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel3.Controls.Add(this.BtnEmssionOff);
						this.panel3.Controls.Add(this.LED_EMISSION);
						this.panel3.Controls.Add(this.BtnEmssionOn);
						this.panel3.Controls.Add(this.label1);
						this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel3.Location = new System.Drawing.Point(0, 200);
						this.panel3.Margin = new System.Windows.Forms.Padding(1);
						this.panel3.Name = "panel3";
						this.panel3.Size = new System.Drawing.Size(183, 105);
						this.panel3.TabIndex = 103;
						// 
						// BtnEmssionOff
						// 
						this.BtnEmssionOff.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnEmssionOff.Location = new System.Drawing.Point(84, 67);
						this.BtnEmssionOff.Name = "BtnEmssionOff";
						this.BtnEmssionOff.Size = new System.Drawing.Size(92, 33);
						this.BtnEmssionOff.TabIndex = 94;
						this.BtnEmssionOff.Text = "Off";
						// 
						// LED_EMISSION
						// 
						this.LED_EMISSION.Location = new System.Drawing.Point(3, 36);
						this.LED_EMISSION.Name = "LED_EMISSION";
						this.LED_EMISSION.OffColor = System.Drawing.Color.Red;
						this.LED_EMISSION.OnColor = System.Drawing.Color.Lime;
						this.LED_EMISSION.Size = new System.Drawing.Size(61, 61);
						this.LED_EMISSION.TabIndex = 90;
						this.LED_EMISSION.Value = true;
						// 
						// BtnEmssionOn
						// 
						this.BtnEmssionOn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.BtnEmssionOn.Location = new System.Drawing.Point(84, 31);
						this.BtnEmssionOn.Name = "BtnEmssionOn";
						this.BtnEmssionOn.Size = new System.Drawing.Size(92, 33);
						this.BtnEmssionOn.TabIndex = 93;
						this.BtnEmssionOn.Text = "On";
						// 
						// label1
						// 
						this.label1.BackColor = System.Drawing.Color.RoyalBlue;
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
						// panel8
						// 
						this.panel8.BackColor = System.Drawing.Color.White;
						this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel8.Controls.Add(this.LED_SYSTEM_READY);
						this.panel8.Controls.Add(this.label6);
						this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel8.Location = new System.Drawing.Point(0, 100);
						this.panel8.Margin = new System.Windows.Forms.Padding(1);
						this.panel8.Name = "panel8";
						this.panel8.Size = new System.Drawing.Size(183, 100);
						this.panel8.TabIndex = 102;
						// 
						// LED_SYSTEM_READY
						// 
						this.LED_SYSTEM_READY.Location = new System.Drawing.Point(55, 34);
						this.LED_SYSTEM_READY.Name = "LED_SYSTEM_READY";
						this.LED_SYSTEM_READY.OffColor = System.Drawing.Color.Red;
						this.LED_SYSTEM_READY.OnColor = System.Drawing.Color.Lime;
						this.LED_SYSTEM_READY.Size = new System.Drawing.Size(61, 61);
						this.LED_SYSTEM_READY.TabIndex = 90;
						this.LED_SYSTEM_READY.Value = true;
						// 
						// label6
						// 
						this.label6.BackColor = System.Drawing.Color.RoyalBlue;
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
						this.panel2.BackColor = System.Drawing.Color.White;
						this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.panel2.Controls.Add(this.LED_CONNECT);
						this.panel2.Controls.Add(this.label_RecipeList);
						this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel2.Location = new System.Drawing.Point(0, 0);
						this.panel2.Margin = new System.Windows.Forms.Padding(1);
						this.panel2.Name = "panel2";
						this.panel2.Size = new System.Drawing.Size(183, 100);
						this.panel2.TabIndex = 101;
						// 
						// LED_CONNECT
						// 
						this.LED_CONNECT.Location = new System.Drawing.Point(55, 33);
						this.LED_CONNECT.Name = "LED_CONNECT";
						this.LED_CONNECT.OffColor = System.Drawing.Color.Red;
						this.LED_CONNECT.OnColor = System.Drawing.Color.Lime;
						this.LED_CONNECT.Size = new System.Drawing.Size(61, 61);
						this.LED_CONNECT.TabIndex = 90;
						this.LED_CONNECT.Value = true;
						// 
						// label_RecipeList
						// 
						this.label_RecipeList.BackColor = System.Drawing.Color.RoyalBlue;
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
						this.panel11.Size = new System.Drawing.Size(832, 864);
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
						this.DGV_LaserParam.Size = new System.Drawing.Size(832, 834);
						this.DGV_LaserParam.TabIndex = 94;
						this.DGV_LaserParam.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGV_LaserParam_CellFormatting);
						this.DGV_LaserParam.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_LaserParam_CellMouseClick);
						this.DGV_LaserParam.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DGV_LaserParam_CellPainting);
						// 
						// label8
						// 
						this.label8.BackColor = System.Drawing.Color.RoyalBlue;
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
						// FrmLaserTalon
						// 
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
						this.ClientSize = new System.Drawing.Size(1015, 864);
						this.Controls.Add(this.panel11);
						this.Controls.Add(this.panel1);
						this.DoubleBuffered = true;
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
						this.Name = "FrmLaserTalon";
						this.Text = "Form1";
						this.SizeChanged += new System.EventHandler(this.FrmLaserTalon_SizeChanged);
						this.panel1.ResumeLayout(false);
						this.panel7.ResumeLayout(false);
						this.panel9.ResumeLayout(false);
						this.panel6.ResumeLayout(false);
						this.panel5.ResumeLayout(false);
						this.panel4.ResumeLayout(false);
						this.panel3.ResumeLayout(false);
						this.panel8.ResumeLayout(false);
						this.panel2.ResumeLayout(false);
						this.panel11.ResumeLayout(false);
						((System.ComponentModel.ISupportInitialize)(this.DGV_LaserParam)).EndInit();
						this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private EzIna.GUI.UserControls.GlassButton BtnExtGateDisable;
        private EzIna.GUI.UserControls.GlassButton BtnExtGateEnable;
        private EzIna.GUI.UserControls.LedControls LED_EXT_GATE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private EzIna.GUI.UserControls.GlassButton BtnGateClose;
        private EzIna.GUI.UserControls.GlassButton BtnGateOpen;
        private EzIna.GUI.UserControls.LedControls LED_GATE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private EzIna.GUI.UserControls.GlassButton BtnShutterClose;
        private EzIna.GUI.UserControls.GlassButton BtnShutterOpen;
        private EzIna.GUI.UserControls.LedControls LED_SHUTTER;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private EzIna.GUI.UserControls.GlassButton BtnEmssionOff;
        private EzIna.GUI.UserControls.LedControls LED_EMISSION;
        private EzIna.GUI.UserControls.GlassButton BtnEmssionOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private EzIna.GUI.UserControls.LedControls LED_SYSTEM_READY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private EzIna.GUI.UserControls.LedControls LED_CONNECT;
        private System.Windows.Forms.Label label_RecipeList;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.DataGridView DGV_LaserParam;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel7;
        private EzIna.GUI.UserControls.AGauge GAUGE_OutPower;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_WarmUpTime;
    }
}