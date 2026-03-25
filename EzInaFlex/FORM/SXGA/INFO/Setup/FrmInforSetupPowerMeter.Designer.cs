namespace EzIna
{
    partial class FrmInforSetupPowerMeter_SXGA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforSetupPowerMeter_SXGA));
            this.panel1 = new System.Windows.Forms.Panel();
            this.TV_DeviceList = new System.Windows.Forms.TreeView();
            this.imageListForTree = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TV_DeviceList);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 908);
            this.panel1.TabIndex = 0;
            // 
            // TV_DeviceList
            // 
            this.TV_DeviceList.BackColor = System.Drawing.SystemColors.Control;
            this.TV_DeviceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_DeviceList.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TV_DeviceList.ImageIndex = 0;
            this.TV_DeviceList.ImageList = this.imageListForTree;
            this.TV_DeviceList.Indent = 30;
            this.TV_DeviceList.ItemHeight = 35;
            this.TV_DeviceList.LineColor = System.Drawing.Color.Gray;
            this.TV_DeviceList.Location = new System.Drawing.Point(0, 30);
            this.TV_DeviceList.Name = "TV_DeviceList";
            this.TV_DeviceList.SelectedImageIndex = 0;
            this.TV_DeviceList.Size = new System.Drawing.Size(247, 878);
            this.TV_DeviceList.TabIndex = 41;            
            this.TV_DeviceList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TV_LaserList_NodeMouseClick);
            // 
            // imageListForTree
            // 
            this.imageListForTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForTree.ImageStream")));
            this.imageListForTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForTree.Images.SetKeyName(0, "unchecked.png");
            this.imageListForTree.Images.SetKeyName(1, "checked.png");
            this.imageListForTree.Images.SetKeyName(2, "Variable_Optical_Attenuator_32.png");
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
            this.label2.Size = new System.Drawing.Size(247, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "Power Meter";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(247, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1015, 908);
            this.panelMain.TabIndex = 1;
            // 
            // FrmInforSetupPowerMeter_SXGA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1262, 908);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInforSetupPowerMeter_SXGA";
            this.Tag = "FORM_ID_INFOR_SETUP_ATTENUATOR";
            this.Text = "FrmInforSetupIO";
            this.Load += new System.EventHandler(this.FrmInforSetupCylinder_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmInforSetupLaser_SXGA_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView TV_DeviceList;
        private System.Windows.Forms.ImageList imageListForTree;
    }
}