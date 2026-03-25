namespace EzIna
{
	partial class FrmInforRecipePanel_SXGA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforRecipePanel_SXGA));
            this.listBox_Recipe = new System.Windows.Forms.ListBox();
            this.imageList_Recipe = new System.Windows.Forms.ImageList(this.components);
            this.label_RecipeList = new System.Windows.Forms.Label();
            this.btn_RecipeDelete = new System.Windows.Forms.Button();
            this.btn_RecipeAdd = new System.Windows.Forms.Button();
            this.btn_RecipeSave = new System.Windows.Forms.Button();
            this.btn_RecipeRename = new System.Windows.Forms.Button();
            this.btn_RecipeOpen = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucTabControlX1 = new EzIna.GUI.UserControls.ucTabControlX();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_SelectedModel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_Recipe
            // 
            this.listBox_Recipe.BackColor = System.Drawing.Color.White;
            this.listBox_Recipe.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Recipe.FormattingEnabled = true;
            this.listBox_Recipe.ItemHeight = 17;
            this.listBox_Recipe.Location = new System.Drawing.Point(4, 99);
            this.listBox_Recipe.Name = "listBox_Recipe";
            this.listBox_Recipe.Size = new System.Drawing.Size(211, 446);
            this.listBox_Recipe.TabIndex = 93;
            this.listBox_Recipe.SelectedIndexChanged += new System.EventHandler(this.listBox_Recipe_SelectedIndexChanged);
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
            // label_RecipeList
            // 
            this.label_RecipeList.BackColor = System.Drawing.Color.SteelBlue;
            this.label_RecipeList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_RecipeList.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RecipeList.ForeColor = System.Drawing.Color.White;
            this.label_RecipeList.Location = new System.Drawing.Point(4, 69);
            this.label_RecipeList.Name = "label_RecipeList";
            this.label_RecipeList.Size = new System.Drawing.Size(211, 30);
            this.label_RecipeList.TabIndex = 87;
            this.label_RecipeList.Text = "Recipe List";
            this.label_RecipeList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_RecipeDelete
            // 
            this.btn_RecipeDelete.BackColor = System.Drawing.Color.White;
            this.btn_RecipeDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RecipeDelete.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RecipeDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RecipeDelete.ImageIndex = 4;
            this.btn_RecipeDelete.ImageList = this.imageList_Recipe;
            this.btn_RecipeDelete.Location = new System.Drawing.Point(4, 759);
            this.btn_RecipeDelete.Name = "btn_RecipeDelete";
            this.btn_RecipeDelete.Size = new System.Drawing.Size(211, 58);
            this.btn_RecipeDelete.TabIndex = 90;
            this.btn_RecipeDelete.Tag = "DELETE";
            this.btn_RecipeDelete.Text = "Delete";
            this.btn_RecipeDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RecipeDelete.UseVisualStyleBackColor = false;
            // 
            // btn_RecipeAdd
            // 
            this.btn_RecipeAdd.BackColor = System.Drawing.Color.White;
            this.btn_RecipeAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RecipeAdd.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RecipeAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RecipeAdd.ImageIndex = 2;
            this.btn_RecipeAdd.ImageList = this.imageList_Recipe;
            this.btn_RecipeAdd.Location = new System.Drawing.Point(4, 695);
            this.btn_RecipeAdd.Name = "btn_RecipeAdd";
            this.btn_RecipeAdd.Size = new System.Drawing.Size(211, 58);
            this.btn_RecipeAdd.TabIndex = 91;
            this.btn_RecipeAdd.Tag = "ADD";
            this.btn_RecipeAdd.Text = "Add";
            this.btn_RecipeAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RecipeAdd.UseVisualStyleBackColor = false;
            // 
            // btn_RecipeSave
            // 
            this.btn_RecipeSave.BackColor = System.Drawing.Color.White;
            this.btn_RecipeSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RecipeSave.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RecipeSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RecipeSave.ImageIndex = 1;
            this.btn_RecipeSave.ImageList = this.imageList_Recipe;
            this.btn_RecipeSave.Location = new System.Drawing.Point(4, 827);
            this.btn_RecipeSave.Name = "btn_RecipeSave";
            this.btn_RecipeSave.Size = new System.Drawing.Size(211, 58);
            this.btn_RecipeSave.TabIndex = 92;
            this.btn_RecipeSave.Tag = "SAVE";
            this.btn_RecipeSave.Text = "Save";
            this.btn_RecipeSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RecipeSave.UseVisualStyleBackColor = false;
            // 
            // btn_RecipeRename
            // 
            this.btn_RecipeRename.BackColor = System.Drawing.Color.White;
            this.btn_RecipeRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RecipeRename.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RecipeRename.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RecipeRename.ImageIndex = 3;
            this.btn_RecipeRename.ImageList = this.imageList_Recipe;
            this.btn_RecipeRename.Location = new System.Drawing.Point(4, 631);
            this.btn_RecipeRename.Name = "btn_RecipeRename";
            this.btn_RecipeRename.Size = new System.Drawing.Size(211, 58);
            this.btn_RecipeRename.TabIndex = 88;
            this.btn_RecipeRename.Tag = "RENAME";
            this.btn_RecipeRename.Text = "Rename";
            this.btn_RecipeRename.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RecipeRename.UseVisualStyleBackColor = false;
            // 
            // btn_RecipeOpen
            // 
            this.btn_RecipeOpen.BackColor = System.Drawing.Color.White;
            this.btn_RecipeOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RecipeOpen.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RecipeOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RecipeOpen.ImageIndex = 0;
            this.btn_RecipeOpen.ImageList = this.imageList_Recipe;
            this.btn_RecipeOpen.Location = new System.Drawing.Point(4, 567);
            this.btn_RecipeOpen.Name = "btn_RecipeOpen";
            this.btn_RecipeOpen.Size = new System.Drawing.Size(211, 58);
            this.btn_RecipeOpen.TabIndex = 89;
            this.btn_RecipeOpen.Tag = "OPEN";
            this.btn_RecipeOpen.Text = "Open";
            this.btn_RecipeOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RecipeOpen.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucTabControlX1);
            this.panel1.Location = new System.Drawing.Point(221, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 890);
            this.panel1.TabIndex = 97;
            // 
            // ucTabControlX1
            // 
            this.ucTabControlX1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucTabControlX1.CtrlPanelColor = System.Drawing.Color.White;
            this.ucTabControlX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTabControlX1.Location = new System.Drawing.Point(0, 0);
            this.ucTabControlX1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTabControlX1.MouseClkTabColor = System.Drawing.Color.RoyalBlue;
            this.ucTabControlX1.MouseHrTabColor = System.Drawing.Color.DodgerBlue;
            this.ucTabControlX1.Name = "ucTabControlX1";
            this.ucTabControlX1.RibbonColor = System.Drawing.Color.RoyalBlue;
            this.ucTabControlX1.SelectedTabIndex = -1;
            this.ucTabControlX1.SelTabBackColor = System.Drawing.Color.RoyalBlue;
            this.ucTabControlX1.SelTabForeColor = System.Drawing.Color.White;
            this.ucTabControlX1.Size = new System.Drawing.Size(1041, 890);
            this.ucTabControlX1.TabIndex = 1;
            this.ucTabControlX1.TabPanelColor = System.Drawing.Color.White;
            this.ucTabControlX1.TabSize = new System.Drawing.Size(130, 32);
            this.ucTabControlX1.UnSelTabBackColor = System.Drawing.Color.LightSkyBlue;
            this.ucTabControlX1.UnSelTabForeColor = System.Drawing.Color.White;
            this.ucTabControlX1.X_TextLoc = 10;
            this.ucTabControlX1.Y_TextLoc = 5;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 30);
            this.label1.TabIndex = 98;
            this.label1.Text = "Selected Recipe Model";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDoubleClick);
            // 
            // lb_SelectedModel
            // 
            this.lb_SelectedModel.BackColor = System.Drawing.Color.White;
            this.lb_SelectedModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_SelectedModel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_SelectedModel.ForeColor = System.Drawing.Color.Black;
            this.lb_SelectedModel.Location = new System.Drawing.Point(4, 39);
            this.lb_SelectedModel.Name = "lb_SelectedModel";
            this.lb_SelectedModel.Size = new System.Drawing.Size(211, 30);
            this.lb_SelectedModel.TabIndex = 99;
            this.lb_SelectedModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmInforRecipePanel_SXGA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 902);
            this.Controls.Add(this.lb_SelectedModel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox_Recipe);
            this.Controls.Add(this.btn_RecipeDelete);
            this.Controls.Add(this.btn_RecipeAdd);
            this.Controls.Add(this.btn_RecipeSave);
            this.Controls.Add(this.btn_RecipeRename);
            this.Controls.Add(this.btn_RecipeOpen);
            this.Controls.Add(this.label_RecipeList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInforRecipePanel_SXGA";
            this.Tag = "FORM_ID_INFOR_RECIPE";
            this.Load += new System.EventHandler(this.Form_Load);
            this.VisibleChanged += new System.EventHandler(this.Form_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBox_Recipe;
		private System.Windows.Forms.Button btn_RecipeDelete;
		private System.Windows.Forms.ImageList imageList_Recipe;
		private System.Windows.Forms.Button btn_RecipeAdd;
		private System.Windows.Forms.Button btn_RecipeSave;
		private System.Windows.Forms.Button btn_RecipeRename;
		private System.Windows.Forms.Button btn_RecipeOpen;
		private System.Windows.Forms.Label label_RecipeList;
		private System.Windows.Forms.Panel panel1;
		private EzIna.GUI.UserControls.ucTabControlX ucTabControlX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_SelectedModel;
    }
}