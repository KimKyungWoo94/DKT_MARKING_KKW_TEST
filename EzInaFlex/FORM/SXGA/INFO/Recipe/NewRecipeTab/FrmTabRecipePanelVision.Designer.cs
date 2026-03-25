namespace EzIna
{
	partial class FrmTabRecipePanelVision
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
						System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTabRecipePanelVision));
						this.treeView_Menu = new System.Windows.Forms.TreeView();
						this.imageList_For_TreeMenu = new System.Windows.Forms.ImageList(this.components);
						this.lb_SelSubCategory = new System.Windows.Forms.Label();
						this.Panel_DataGridview = new System.Windows.Forms.Panel();
						this.SuspendLayout();
						// 
						// treeView_Menu
						// 
						this.treeView_Menu.Dock = System.Windows.Forms.DockStyle.Left;
						this.treeView_Menu.Location = new System.Drawing.Point(0, 0);
						this.treeView_Menu.Name = "treeView_Menu";
						this.treeView_Menu.Size = new System.Drawing.Size(287, 856);
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
						// lb_SelSubCategory
						// 
						this.lb_SelSubCategory.BackColor = System.Drawing.Color.SteelBlue;
						this.lb_SelSubCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.lb_SelSubCategory.Dock = System.Windows.Forms.DockStyle.Top;
						this.lb_SelSubCategory.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.lb_SelSubCategory.ForeColor = System.Drawing.Color.White;
						this.lb_SelSubCategory.Location = new System.Drawing.Point(287, 0);
						this.lb_SelSubCategory.Name = "lb_SelSubCategory";
						this.lb_SelSubCategory.Size = new System.Drawing.Size(752, 30);
						this.lb_SelSubCategory.TabIndex = 100;
						this.lb_SelSubCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// Panel_DataGridview
						// 
						this.Panel_DataGridview.Dock = System.Windows.Forms.DockStyle.Fill;
						this.Panel_DataGridview.Location = new System.Drawing.Point(287, 30);
						this.Panel_DataGridview.Name = "Panel_DataGridview";
						this.Panel_DataGridview.Size = new System.Drawing.Size(752, 826);
						this.Panel_DataGridview.TabIndex = 101;
						// 
						// FrmTabRecipePanelVision
						// 
						this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
						this.ClientSize = new System.Drawing.Size(1039, 856);
						this.Controls.Add(this.Panel_DataGridview);
						this.Controls.Add(this.lb_SelSubCategory);
						this.Controls.Add(this.treeView_Menu);
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
						this.Name = "FrmTabRecipePanelVision";
						this.Text = "Form1";
						this.Load += new System.EventHandler(this.FrmTabRecipeConfiguration_Load);
						this.VisibleChanged += new System.EventHandler(this.FrmTabRecipeConfiguration_VisibleChanged);
						this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TreeView treeView_Menu;
		private System.Windows.Forms.ImageList imageList_For_TreeMenu;
        private System.Windows.Forms.Label lb_SelSubCategory;
        private System.Windows.Forms.Panel Panel_DataGridview;
    }
}