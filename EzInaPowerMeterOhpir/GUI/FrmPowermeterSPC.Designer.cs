namespace EzIna.PowerMeter.Ohpir.GUI
{
	partial class FrmPowermeterSPC
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lb_OutPower = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_RecipeList = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.DGV_Param = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.PowerChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GAUGE_OutPower = new EzIna.GUI.UserControls.AGauge();
            this.BtnMeasureStop = new EzIna.GUI.UserControls.GlassButton();
            this.BtnMeasureStart = new EzIna.GUI.UserControls.GlassButton();
            this.LED_MEASURE = new EzIna.GUI.UserControls.LedControls();
            this.BtnZeroSetStart = new EzIna.GUI.UserControls.GlassButton();
            this.LED_ZERO_SET = new EzIna.GUI.UserControls.LedControls();
            this.LED_CONNECT = new EzIna.GUI.UserControls.LedControls();
            this.panel1.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Param)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerChart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 985);
            this.panel1.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.panel7);
            this.panel11.Controls.Add(this.panel3);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 205);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(183, 780);
            this.panel11.TabIndex = 110;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.lb_OutPower);
            this.panel7.Controls.Add(this.GAUGE_OutPower);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 105);
            this.panel7.Margin = new System.Windows.Forms.Padding(1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(183, 239);
            this.panel7.TabIndex = 110;
            // 
            // lb_OutPower
            // 
            this.lb_OutPower.BackColor = System.Drawing.Color.White;
            this.lb_OutPower.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_OutPower.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_OutPower.Location = new System.Drawing.Point(0, 191);
            this.lb_OutPower.Name = "lb_OutPower";
            this.lb_OutPower.Size = new System.Drawing.Size(181, 46);
            this.lb_OutPower.TabIndex = 90;
            this.lb_OutPower.Text = "label9";
            this.lb_OutPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.RoyalBlue;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 30);
            this.label5.TabIndex = 88;
            this.label5.Text = "Output Power";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.BtnMeasureStop);
            this.panel3.Controls.Add(this.BtnMeasureStart);
            this.panel3.Controls.Add(this.LED_MEASURE);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 105);
            this.panel3.TabIndex = 107;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 30);
            this.label1.TabIndex = 88;
            this.label1.Text = "Measure";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.BtnZeroSetStart);
            this.panel6.Controls.Add(this.LED_ZERO_SET);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 100);
            this.panel6.Margin = new System.Windows.Forms.Padding(1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(183, 105);
            this.panel6.TabIndex = 106;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 30);
            this.label4.TabIndex = 88;
            this.label4.Text = "ZeroSet";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel2.Size = new System.Drawing.Size(183, 100);
            this.panel2.TabIndex = 101;
            // 
            // label_RecipeList
            // 
            this.label_RecipeList.BackColor = System.Drawing.Color.RoyalBlue;
            this.label_RecipeList.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_RecipeList.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RecipeList.ForeColor = System.Drawing.Color.White;
            this.label_RecipeList.Location = new System.Drawing.Point(0, 0);
            this.label_RecipeList.Name = "label_RecipeList";
            this.label_RecipeList.Size = new System.Drawing.Size(181, 30);
            this.label_RecipeList.TabIndex = 88;
            this.label_RecipeList.Text = "Comm Connect";
            this.label_RecipeList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.PowerChart);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(183, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1081, 443);
            this.panel4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1081, 30);
            this.label2.TabIndex = 93;
            this.label2.Text = "Power Intensity";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.DGV_Param);
            this.panel10.Controls.Add(this.label9);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(183, 443);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1081, 603);
            this.panel10.TabIndex = 5;
            // 
            // DGV_Param
            // 
            this.DGV_Param.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Param.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Param.Location = new System.Drawing.Point(0, 30);
            this.DGV_Param.Margin = new System.Windows.Forms.Padding(1);
            this.DGV_Param.Name = "DGV_Param";
            this.DGV_Param.RowTemplate.Height = 23;
            this.DGV_Param.Size = new System.Drawing.Size(1081, 573);
            this.DGV_Param.TabIndex = 94;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.RoyalBlue;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1081, 30);
            this.label9.TabIndex = 93;
            this.label9.Text = "Parameter";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PowerChart
            // 
            this.PowerChart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.PowerChart.BackSecondaryColor = System.Drawing.Color.PaleGreen;
            this.PowerChart.BorderlineColor = System.Drawing.Color.ForestGreen;
            this.PowerChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.PowerChart.BorderlineWidth = 2;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.PaleGreen;
            chartArea1.Name = "ChartArea1";
            this.PowerChart.ChartAreas.Add(chartArea1);
            this.PowerChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.White;
            legend1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            legend1.BackSecondaryColor = System.Drawing.Color.PaleGreen;
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.PowerChart.Legends.Add(legend1);
            this.PowerChart.Location = new System.Drawing.Point(0, 30);
            this.PowerChart.Name = "PowerChart";
            series1.BorderColor = System.Drawing.Color.Red;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Firebrick;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.PowerChart.Series.Add(series1);
            this.PowerChart.Size = new System.Drawing.Size(1081, 413);
            this.PowerChart.TabIndex = 94;
            // 
            // GAUGE_OutPower
            // 
            this.GAUGE_OutPower.BackColor = System.Drawing.Color.White;
            this.GAUGE_OutPower.BaseArcColor = System.Drawing.Color.Gray;
            this.GAUGE_OutPower.BaseArcRadius = 40;
            this.GAUGE_OutPower.BaseArcStart = 90;
            this.GAUGE_OutPower.BaseArcSweep = 270;
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
            this.GAUGE_OutPower.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GAUGE_OutPower.Location = new System.Drawing.Point(19, 42);
            this.GAUGE_OutPower.MaxValue = 150F;
            this.GAUGE_OutPower.MinValue = 0F;
            this.GAUGE_OutPower.Name = "GAUGE_OutPower";
            this.GAUGE_OutPower.NeedleColor1 = EzIna.GUI.UserControls.AGauge.NeedleColorEnum.Green;
            this.GAUGE_OutPower.NeedleColor2 = System.Drawing.Color.GreenYellow;
            this.GAUGE_OutPower.NeedleRadius = 50;
            this.GAUGE_OutPower.NeedleType = 0;
            this.GAUGE_OutPower.NeedleWidth = 3;
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
            this.GAUGE_OutPower.ScaleLinesMajorStepValue = 30F;
            this.GAUGE_OutPower.ScaleLinesMajorWidth = 2;
            this.GAUGE_OutPower.ScaleLinesMinorColor = System.Drawing.Color.DimGray;
            this.GAUGE_OutPower.ScaleLinesMinorInnerRadius = 43;
            this.GAUGE_OutPower.ScaleLinesMinorNumOf = 5;
            this.GAUGE_OutPower.ScaleLinesMinorOuterRadius = 50;
            this.GAUGE_OutPower.ScaleLinesMinorWidth = 1;
            this.GAUGE_OutPower.ScaleNumbersColor = System.Drawing.Color.Black;
            this.GAUGE_OutPower.ScaleNumbersFormat = null;
            this.GAUGE_OutPower.ScaleNumbersRadius = 62;
            this.GAUGE_OutPower.ScaleNumbersRotation = 90;
            this.GAUGE_OutPower.ScaleNumbersStartScaleLine = 1;
            this.GAUGE_OutPower.ScaleNumbersStepScaleLines = 2;
            this.GAUGE_OutPower.Size = new System.Drawing.Size(144, 146);
            this.GAUGE_OutPower.TabIndex = 89;
            this.GAUGE_OutPower.Value = 0F;
            // 
            // BtnMeasureStop
            // 
            this.BtnMeasureStop.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMeasureStop.Location = new System.Drawing.Point(84, 69);
            this.BtnMeasureStop.Name = "BtnMeasureStop";
            this.BtnMeasureStop.Size = new System.Drawing.Size(92, 33);
            this.BtnMeasureStop.TabIndex = 96;
            this.BtnMeasureStop.Text = "Stop";
            this.BtnMeasureStop.Click += new System.EventHandler(this.BtnMeasureStop_Click);
            // 
            // BtnMeasureStart
            // 
            this.BtnMeasureStart.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMeasureStart.Location = new System.Drawing.Point(84, 33);
            this.BtnMeasureStart.Name = "BtnMeasureStart";
            this.BtnMeasureStart.Size = new System.Drawing.Size(92, 33);
            this.BtnMeasureStart.TabIndex = 95;
            this.BtnMeasureStart.Text = "Start";
            this.BtnMeasureStart.Click += new System.EventHandler(this.BtnMeasureStart_Click);
            // 
            // LED_MEASURE
            // 
            this.LED_MEASURE.Location = new System.Drawing.Point(3, 37);
            this.LED_MEASURE.Name = "LED_MEASURE";
            this.LED_MEASURE.OffColor = System.Drawing.Color.DarkGray;
            this.LED_MEASURE.OnColor = System.Drawing.Color.Lime;
            this.LED_MEASURE.Size = new System.Drawing.Size(61, 61);
            this.LED_MEASURE.TabIndex = 90;
            this.LED_MEASURE.Value = true;
            // 
            // BtnZeroSetStart
            // 
            this.BtnZeroSetStart.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnZeroSetStart.Location = new System.Drawing.Point(85, 51);
            this.BtnZeroSetStart.Name = "BtnZeroSetStart";
            this.BtnZeroSetStart.Size = new System.Drawing.Size(92, 33);
            this.BtnZeroSetStart.TabIndex = 95;
            this.BtnZeroSetStart.Text = "Start";
            this.BtnZeroSetStart.Click += new System.EventHandler(this.BtnZeroSetStart_Click);
            // 
            // LED_ZERO_SET
            // 
            this.LED_ZERO_SET.Location = new System.Drawing.Point(3, 37);
            this.LED_ZERO_SET.Name = "LED_ZERO_SET";
            this.LED_ZERO_SET.OffColor = System.Drawing.Color.DarkGray;
            this.LED_ZERO_SET.OnColor = System.Drawing.Color.Lime;
            this.LED_ZERO_SET.Size = new System.Drawing.Size(61, 61);
            this.LED_ZERO_SET.TabIndex = 90;
            this.LED_ZERO_SET.Value = true;
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
            // FrmPowermeterSPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPowermeterSPC";
            this.Text = "FrmPowermeterSPC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPowermeterSPC_FormClosed);
            this.Load += new System.EventHandler(this.FrmPowermeterSPC_Load);
            this.panel1.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Param)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerChart)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private EzIna.GUI.UserControls.GlassButton BtnZeroSetStart;
        private EzIna.GUI.UserControls.LedControls LED_ZERO_SET;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private EzIna.GUI.UserControls.LedControls LED_CONNECT;
        private System.Windows.Forms.Label label_RecipeList;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lb_OutPower;
        private EzIna.GUI.UserControls.AGauge GAUGE_OutPower;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private EzIna.GUI.UserControls.GlassButton BtnMeasureStop;
        private EzIna.GUI.UserControls.GlassButton BtnMeasureStart;
        private EzIna.GUI.UserControls.LedControls LED_MEASURE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataVisualization.Charting.Chart PowerChart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.DataGridView DGV_Param;
        private System.Windows.Forms.Label label9;
    }
}