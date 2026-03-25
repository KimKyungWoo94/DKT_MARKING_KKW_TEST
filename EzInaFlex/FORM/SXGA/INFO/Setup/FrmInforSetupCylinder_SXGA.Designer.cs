namespace EzIna
{
    partial class FrmInforSetupCylinder_SXGA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforSetupCylinder_SXGA));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.DGV_CYLINDER = new EzIna.GUI.UserControls.ExpandDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_CYLINDER)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Tag = "0";
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "apply button.ico");
            this.imageList1.Images.SetKeyName(1, "open button.ico");
            this.imageList1.Images.SetKeyName(2, "save button.ico");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.SystemColors.Control;
            this.imageList2.Images.SetKeyName(0, "unchecked.png");
            this.imageList2.Images.SetKeyName(1, "checked.png");
            this.imageList2.Images.SetKeyName(2, "unchecked.png");
            this.imageList2.Images.SetKeyName(3, "PIC.png");
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1262, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "Cylinder";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DGV_CYLINDER
            // 
            this.DGV_CYLINDER.AllowUserToDeleteRows = false;
            this.DGV_CYLINDER.AllowUserToResizeColumns = false;
            this.DGV_CYLINDER.AllowUserToResizeRows = false;
            this.DGV_CYLINDER.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_CYLINDER.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_CYLINDER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_CYLINDER.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DGV_CYLINDER.Location = new System.Drawing.Point(0, 30);
            this.DGV_CYLINDER.MultiSelect = false;
            this.DGV_CYLINDER.Name = "DGV_CYLINDER";
            this.DGV_CYLINDER.RowHeadersVisible = false;
            this.DGV_CYLINDER.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_CYLINDER.RowTemplate.Height = 30;
            this.DGV_CYLINDER.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_CYLINDER.Size = new System.Drawing.Size(1262, 878);
            this.DGV_CYLINDER.TabIndex = 10;
            this.DGV_CYLINDER.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CYLINDER_CellClick);
            this.DGV_CYLINDER.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DGV_CYLINDER_DataBindingComplete);
            this.DGV_CYLINDER.SelectionChanged += new System.EventHandler(this.DataGridVeiwNoSelection_SelectionChanged);
            // 
            // FrmInforSetupCylinder_SXGA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1262, 908);
            this.Controls.Add(this.DGV_CYLINDER);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInforSetupCylinder_SXGA";
            this.Tag = "FORM_ID_INFOR_SETUP_CYLINDER";
            this.Text = "FrmInforSetupIO";
            this.Load += new System.EventHandler(this.FrmInforSetupCylinder_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmInforSetupCylinder_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_CYLINDER)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Label label2;
        private GUI.UserControls.ExpandDataGridView DGV_CYLINDER;
    }
}