namespace EzIna
{
    partial class FrmInforManualOperation_SXGA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforManualMarking_SXGA));
            this.imageListForTree = new System.Windows.Forms.ImageList(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // imageListForTree
            // 
            this.imageListForTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForTree.ImageStream")));
            this.imageListForTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForTree.Images.SetKeyName(0, "unchecked.png");
            this.imageListForTree.Images.SetKeyName(1, "checked.png");
            this.imageListForTree.Images.SetKeyName(2, "Variable_Optical_Attenuator_32.png");
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1262, 908);
            this.panelMain.TabIndex = 1;
            // 
            // FrmInforManualMarking_SXGA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1262, 908);
            this.Controls.Add(this.panelMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInforManualMarking_SXGA";
            this.Tag = "FORM_ID_INFOR_SETUP_ATTENUATOR";
            this.Text = "FrmInforSetupIO";
            this.Load += new System.EventHandler(this.FrmInforSetupCylinder_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmInforSetupLaser_SXGA_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ImageList imageListForTree;
    }
}