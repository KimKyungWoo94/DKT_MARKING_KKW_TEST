namespace EzIna
{
	partial class FrmTabRecipeConfiguration
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
						System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
						System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTabRecipeConfiguration));
						this.dataGridView_Options = new System.Windows.Forms.DataGridView();
						this.dataGridView_Datas = new System.Windows.Forms.DataGridView();
						this.treeView_Menu = new System.Windows.Forms.TreeView();
						this.imageList_For_TreeMenu = new System.Windows.Forms.ImageList(this.components);
						((System.ComponentModel.ISupportInitialize)(this.dataGridView_Options)).BeginInit();
						((System.ComponentModel.ISupportInitialize)(this.dataGridView_Datas)).BeginInit();
						this.SuspendLayout();
						// 
						// dataGridView_Options
						// 
						this.dataGridView_Options.BackgroundColor = System.Drawing.Color.White;
						this.dataGridView_Options.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
						this.dataGridView_Options.Location = new System.Drawing.Point(365, 575);
						this.dataGridView_Options.Name = "dataGridView_Options";
						this.dataGridView_Options.RowTemplate.Height = 23;
						this.dataGridView_Options.Size = new System.Drawing.Size(677, 320);
						this.dataGridView_Options.TabIndex = 99;
						// 
						// dataGridView_Datas
						// 
						this.dataGridView_Datas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
						dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
						dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
						dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
						dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
						dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
						dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
						dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
						this.dataGridView_Datas.DefaultCellStyle = dataGridViewCellStyle1;
						this.dataGridView_Datas.GridColor = System.Drawing.Color.SteelBlue;
						this.dataGridView_Datas.Location = new System.Drawing.Point(365, 12);
						this.dataGridView_Datas.Name = "dataGridView_Datas";
						this.dataGridView_Datas.RowTemplate.Height = 23;
						this.dataGridView_Datas.Size = new System.Drawing.Size(677, 554);
						this.dataGridView_Datas.TabIndex = 98;
						// 
						// treeView_Menu
						// 
						this.treeView_Menu.Location = new System.Drawing.Point(12, 12);
						this.treeView_Menu.Name = "treeView_Menu";
						this.treeView_Menu.Size = new System.Drawing.Size(347, 883);
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
						// FrmTabRecipeConfiguration
						// 
						this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
						this.ClientSize = new System.Drawing.Size(1264, 985);
						this.Controls.Add(this.dataGridView_Options);
						this.Controls.Add(this.dataGridView_Datas);
						this.Controls.Add(this.treeView_Menu);
						this.Name = "FrmTabRecipeConfiguration";
						this.Text = "                    ";
						this.Load += new System.EventHandler(this.FrmTabRecipeConfiguration_Load);
						this.VisibleChanged += new System.EventHandler(this.FrmTabRecipeConfiguration_VisibleChanged);
						((System.ComponentModel.ISupportInitialize)(this.dataGridView_Options)).EndInit();
						((System.ComponentModel.ISupportInitialize)(this.dataGridView_Datas)).EndInit();
						this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView_Options;
		private System.Windows.Forms.DataGridView dataGridView_Datas;
		private System.Windows.Forms.TreeView treeView_Menu;
		private System.Windows.Forms.ImageList imageList_For_TreeMenu;
	}
}