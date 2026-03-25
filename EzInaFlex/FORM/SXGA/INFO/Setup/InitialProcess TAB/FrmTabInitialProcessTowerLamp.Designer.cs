namespace EzIna
{
	partial class FrmTabInitialProcessTowerLamp
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTabInitialProcessTowerLamp));
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.ucRoundedPanel1 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.btn_TowerLampSave = new System.Windows.Forms.Button();
			this.imageList_Recipe = new System.Windows.Forms.ImageList(this.components);
			this.btn_TowerLampOpen = new System.Windows.Forms.Button();
			this.dataGridView_LampNBuzzer = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_LampNBuzzer)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.ucRoundedPanel1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.dataGridView_LampNBuzzer, 0, 1);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(640, 496);
			this.tableLayoutPanel2.TabIndex = 16;
			this.tableLayoutPanel2.Tag = "WithoutCommonStyle";
			// 
			// ucRoundedPanel1
			// 
			this.ucRoundedPanel1.BorderRadius = 32;
			this.ucRoundedPanel1.BorderSize = 1;
			this.ucRoundedPanel1.Caption = "TOWER LAMP & BUZZER";
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
			// btn_TowerLampSave
			// 
			this.btn_TowerLampSave.BackColor = System.Drawing.Color.White;
			this.btn_TowerLampSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_TowerLampSave.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_TowerLampSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_TowerLampSave.ImageIndex = 1;
			this.btn_TowerLampSave.ImageList = this.imageList_Recipe;
			this.btn_TowerLampSave.Location = new System.Drawing.Point(549, 514);
			this.btn_TowerLampSave.Name = "btn_TowerLampSave";
			this.btn_TowerLampSave.Size = new System.Drawing.Size(100, 50);
			this.btn_TowerLampSave.TabIndex = 106;
			this.btn_TowerLampSave.Tag = "";
			this.btn_TowerLampSave.Text = "Save";
			this.btn_TowerLampSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_TowerLampSave.UseVisualStyleBackColor = false;
			this.btn_TowerLampSave.Click += new System.EventHandler(this.btn_TowerLampSave_Click);
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
			// btn_TowerLampOpen
			// 
			this.btn_TowerLampOpen.BackColor = System.Drawing.Color.White;
			this.btn_TowerLampOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_TowerLampOpen.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_TowerLampOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_TowerLampOpen.ImageIndex = 0;
			this.btn_TowerLampOpen.ImageList = this.imageList_Recipe;
			this.btn_TowerLampOpen.Location = new System.Drawing.Point(443, 514);
			this.btn_TowerLampOpen.Name = "btn_TowerLampOpen";
			this.btn_TowerLampOpen.Size = new System.Drawing.Size(100, 50);
			this.btn_TowerLampOpen.TabIndex = 105;
			this.btn_TowerLampOpen.Tag = "";
			this.btn_TowerLampOpen.Text = "Open";
			this.btn_TowerLampOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_TowerLampOpen.UseVisualStyleBackColor = false;
			this.btn_TowerLampOpen.Click += new System.EventHandler(this.btn_TowerLampOpen_Click);
			// 
			// dataGridView_LampNBuzzer
			// 
			this.dataGridView_LampNBuzzer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_LampNBuzzer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView_LampNBuzzer.Location = new System.Drawing.Point(3, 43);
			this.dataGridView_LampNBuzzer.Name = "dataGridView_LampNBuzzer";
			this.dataGridView_LampNBuzzer.RowTemplate.Height = 23;
			this.dataGridView_LampNBuzzer.Size = new System.Drawing.Size(634, 450);
			this.dataGridView_LampNBuzzer.TabIndex = 11;
			// 
			// FrmTabInitialProcessTowerLamp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1145, 611);
			this.Controls.Add(this.btn_TowerLampSave);
			this.Controls.Add(this.btn_TowerLampOpen);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "FrmTabInitialProcessTowerLamp";
			this.Text = "FrmTabInitialProcessTowerLamp";
			this.VisibleChanged += new System.EventHandler(this.FrmTabInitialProcessTowerLamp_VisibleChanged);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_LampNBuzzer)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel1;
		private System.Windows.Forms.Button btn_TowerLampSave;
		private System.Windows.Forms.Button btn_TowerLampOpen;
		private System.Windows.Forms.ImageList imageList_Recipe;
		private System.Windows.Forms.DataGridView dataGridView_LampNBuzzer;
	}
}