namespace EzIna
{
	partial class FrmTabInitialProcessConfigSet
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTabInitialProcessConfigSet));
			this.ucRoundedPanel3 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.dataGridView_Datas = new System.Windows.Forms.DataGridView();
			this.treeView_Menu = new System.Windows.Forms.TreeView();
			this.ucRoundedPanel1 = new EzIna.GUI.UserControls.ucRoundedPanel();
			this.dataGridView_Options = new System.Windows.Forms.DataGridView();
			this.imageList_For_TreeMenu = new System.Windows.Forms.ImageList(this.components);
			this.btn_Save = new System.Windows.Forms.Button();
			this.imageList_Recipe = new System.Windows.Forms.ImageList(this.components);
			this.btn_Open = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Datas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Options)).BeginInit();
			this.SuspendLayout();
			// 
			// ucRoundedPanel3
			// 
			this.ucRoundedPanel3.BorderRadius = 32;
			this.ucRoundedPanel3.BorderSize = 1;
			this.ucRoundedPanel3.Caption = "Category Lists";
			this.ucRoundedPanel3.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel3.clBorder = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel3.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel3.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel3.Location = new System.Drawing.Point(12, 12);
			this.ucRoundedPanel3.Name = "ucRoundedPanel3";
			this.ucRoundedPanel3.Size = new System.Drawing.Size(217, 32);
			this.ucRoundedPanel3.TabIndex = 13;
			this.ucRoundedPanel3.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel3.TextAlignVertical = System.Drawing.StringAlignment.Center;
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
			this.dataGridView_Datas.GridColor = System.Drawing.Color.SteelBlue;
			this.dataGridView_Datas.Location = new System.Drawing.Point(365, 50);
			this.dataGridView_Datas.Name = "dataGridView_Datas";
			this.dataGridView_Datas.RowTemplate.Height = 23;
			this.dataGridView_Datas.Size = new System.Drawing.Size(677, 554);
			this.dataGridView_Datas.TabIndex = 100;
			// 
			// treeView_Menu
			// 
			this.treeView_Menu.Location = new System.Drawing.Point(12, 50);
			this.treeView_Menu.Name = "treeView_Menu";
			this.treeView_Menu.Size = new System.Drawing.Size(347, 659);
			this.treeView_Menu.TabIndex = 99;
			this.treeView_Menu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Menu_NodeMouseClick);
			// 
			// ucRoundedPanel1
			// 
			this.ucRoundedPanel1.BorderRadius = 32;
			this.ucRoundedPanel1.BorderSize = 1;
			this.ucRoundedPanel1.Caption = "Parameters";
			this.ucRoundedPanel1.clBegin = System.Drawing.Color.CornflowerBlue;
			this.ucRoundedPanel1.clBorder = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel1.clEnd = System.Drawing.Color.RoyalBlue;
			this.ucRoundedPanel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ucRoundedPanel1.ForeColor = System.Drawing.Color.AliceBlue;
			this.ucRoundedPanel1.Location = new System.Drawing.Point(365, 12);
			this.ucRoundedPanel1.Name = "ucRoundedPanel1";
			this.ucRoundedPanel1.Size = new System.Drawing.Size(217, 32);
			this.ucRoundedPanel1.TabIndex = 13;
			this.ucRoundedPanel1.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
			this.ucRoundedPanel1.TextAlignVertical = System.Drawing.StringAlignment.Center;
			// 
			// dataGridView_Options
			// 
			this.dataGridView_Options.BackgroundColor = System.Drawing.Color.White;
			this.dataGridView_Options.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_Options.Location = new System.Drawing.Point(365, 613);
			this.dataGridView_Options.Name = "dataGridView_Options";
			this.dataGridView_Options.RowTemplate.Height = 23;
			this.dataGridView_Options.Size = new System.Drawing.Size(677, 205);
			this.dataGridView_Options.TabIndex = 101;
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
			// btn_Save
			// 
			this.btn_Save.BackColor = System.Drawing.Color.White;
			this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Save.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Save.ImageIndex = 1;
			this.btn_Save.ImageList = this.imageList_Recipe;
			this.btn_Save.Location = new System.Drawing.Point(12, 768);
			this.btn_Save.Name = "btn_Save";
			this.btn_Save.Size = new System.Drawing.Size(347, 50);
			this.btn_Save.TabIndex = 103;
			this.btn_Save.Tag = "";
			this.btn_Save.Text = "Save";
			this.btn_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Save.UseVisualStyleBackColor = false;
			this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
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
			// btn_Open
			// 
			this.btn_Open.BackColor = System.Drawing.Color.White;
			this.btn_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Open.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Open.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Open.ImageIndex = 0;
			this.btn_Open.ImageList = this.imageList_Recipe;
			this.btn_Open.Location = new System.Drawing.Point(12, 715);
			this.btn_Open.Name = "btn_Open";
			this.btn_Open.Size = new System.Drawing.Size(347, 50);
			this.btn_Open.TabIndex = 102;
			this.btn_Open.Tag = "";
			this.btn_Open.Text = "Open";
			this.btn_Open.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Open.UseVisualStyleBackColor = false;
			this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
			// 
			// FrmTabInitialProcessConfigSet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 985);
			this.Controls.Add(this.btn_Save);
			this.Controls.Add(this.btn_Open);
			this.Controls.Add(this.dataGridView_Options);
			this.Controls.Add(this.dataGridView_Datas);
			this.Controls.Add(this.treeView_Menu);
			this.Controls.Add(this.ucRoundedPanel1);
			this.Controls.Add(this.ucRoundedPanel3);
			this.Name = "FrmTabInitialProcessConfigSet";
			this.Text = "FrmTabInitializeProcessConfigSet";
			this.Load += new System.EventHandler(this.FrmTabInitialProcessConfigSet_Load);
			this.VisibleChanged += new System.EventHandler(this.FrmTabInitialProcessConfigSet_VisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Datas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Options)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private GUI.UserControls.ucRoundedPanel ucRoundedPanel3;
		private System.Windows.Forms.DataGridView dataGridView_Datas;
		private System.Windows.Forms.TreeView treeView_Menu;
		private GUI.UserControls.ucRoundedPanel ucRoundedPanel1;
		private System.Windows.Forms.DataGridView dataGridView_Options;
		private System.Windows.Forms.ImageList imageList_For_TreeMenu;
		private System.Windows.Forms.Button btn_Save;
		private System.Windows.Forms.Button btn_Open;
		private System.Windows.Forms.ImageList imageList_Recipe;
	}
}