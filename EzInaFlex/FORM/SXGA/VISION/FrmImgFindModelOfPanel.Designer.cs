namespace EzIna
{
    partial class FrmImgFindModoelOfPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImgFindModoelOfPanel));
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.statusStrip_Bottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLableZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCursorPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ShowMenu = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList_IconsOfVision = new System.Windows.Forms.ImageList(this.components);
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageListCheck = new System.Windows.Forms.ImageList(this.components);
            this.toolStripStatusLabel_HIdeMenu = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer_Center = new System.Windows.Forms.SplitContainer();
            this.imageBoxEx_VisionOfPanel = new EzIna.ImageBoxEx();
            this.panel_Bottom.SuspendLayout();
            this.statusStrip_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Center)).BeginInit();
            this.splitContainer_Center.Panel1.SuspendLayout();
            this.splitContainer_Center.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(68)))));
            this.panel_Bottom.Controls.Add(this.statusStrip_Bottom);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 572);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(820, 25);
            this.panel_Bottom.TabIndex = 8;
            // 
            // statusStrip_Bottom
            // 
            this.statusStrip_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
            this.statusStrip_Bottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip_Bottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelSelection,
            this.toolStripStatusLableZoom,
            this.toolStripStatusLabelCursorPosition,
            this.toolStripStatusLabel_ShowMenu,
            this.toolStripStatusLabel_HIdeMenu});
            this.statusStrip_Bottom.Location = new System.Drawing.Point(0, 3);
            this.statusStrip_Bottom.Name = "statusStrip_Bottom";
            this.statusStrip_Bottom.Size = new System.Drawing.Size(820, 22);
            this.statusStrip_Bottom.TabIndex = 12;
            // 
            // toolStripStatusLabelSelection
            // 
            this.toolStripStatusLabelSelection.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabelSelection.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabelSelection.Name = "toolStripStatusLabelSelection";
            this.toolStripStatusLabelSelection.Size = new System.Drawing.Size(168, 17);
            this.toolStripStatusLabelSelection.Text = "toolStripStatusLabelSelection";
            // 
            // toolStripStatusLableZoom
            // 
            this.toolStripStatusLableZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLableZoom.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLableZoom.Name = "toolStripStatusLableZoom";
            this.toolStripStatusLableZoom.Size = new System.Drawing.Size(149, 17);
            this.toolStripStatusLableZoom.Text = "toolStripStatusLableZoom";
            // 
            // toolStripStatusLabelCursorPosition
            // 
            this.toolStripStatusLabelCursorPosition.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabelCursorPosition.Name = "toolStripStatusLabelCursorPosition";
            this.toolStripStatusLabelCursorPosition.Size = new System.Drawing.Size(76, 17);
            this.toolStripStatusLabelCursorPosition.Text = "None , None";
            // 
            // toolStripStatusLabel_ShowMenu
            // 
            this.toolStripStatusLabel_ShowMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel_ShowMenu.ForeColor = System.Drawing.Color.Lime;
            this.toolStripStatusLabel_ShowMenu.Name = "toolStripStatusLabel_ShowMenu";
            this.toolStripStatusLabel_ShowMenu.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel_ShowMenu.Text = "Show";
            this.toolStripStatusLabel_ShowMenu.Click += new System.EventHandler(this.toolStripStatusLabel_ShowMenu_Click);
            // 
            // imageList_IconsOfVision
            // 
            this.imageList_IconsOfVision.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_IconsOfVision.ImageStream")));
            this.imageList_IconsOfVision.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_IconsOfVision.Images.SetKeyName(0, "Live_Cam.png");
            this.imageList_IconsOfVision.Images.SetKeyName(1, "Idle_Cam.png");
            this.imageList_IconsOfVision.Images.SetKeyName(2, "Grab_Cam.png");
            this.imageList_IconsOfVision.Images.SetKeyName(3, "Scan.png");
            this.imageList_IconsOfVision.Images.SetKeyName(4, "Blob2.png");
            this.imageList_IconsOfVision.Images.SetKeyName(5, "Center.png");
            this.imageList_IconsOfVision.Images.SetKeyName(6, "data-matrix-scan.png");
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.White;
            this.ImageList1.Images.SetKeyName(0, "colo unchecked.png");
            this.ImageList1.Images.SetKeyName(1, "color checked.png");
            // 
            // imageListCheck
            // 
            this.imageListCheck.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCheck.ImageStream")));
            this.imageListCheck.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCheck.Images.SetKeyName(0, "unchecked.png");
            this.imageListCheck.Images.SetKeyName(1, "checked.png");
            this.imageListCheck.Images.SetKeyName(2, "Grab_Cam.png");
            this.imageListCheck.Images.SetKeyName(3, "icons8-rotate-left-50.png");
            // 
            // toolStripStatusLabel_HIdeMenu
            // 
            this.toolStripStatusLabel_HIdeMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel_HIdeMenu.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel_HIdeMenu.Name = "toolStripStatusLabel_HIdeMenu";
            this.toolStripStatusLabel_HIdeMenu.Size = new System.Drawing.Size(33, 17);
            this.toolStripStatusLabel_HIdeMenu.Text = "Hide";
            this.toolStripStatusLabel_HIdeMenu.Click += new System.EventHandler(this.toolStripStatusLabel_HIdeMenu_Click);
            // 
            // splitContainer_Center
            // 
            this.splitContainer_Center.BackColor = System.Drawing.Color.White;
            this.splitContainer_Center.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Center.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_Center.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer_Center.Name = "splitContainer_Center";
            this.splitContainer_Center.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Center.Panel1
            // 
            this.splitContainer_Center.Panel1.Controls.Add(this.imageBoxEx_VisionOfPanel);
            this.splitContainer_Center.Panel1MinSize = 0;
            // 
            // splitContainer_Center.Panel2
            // 
            this.splitContainer_Center.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer_Center.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer_Center.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer_Center.Panel2MinSize = 0;
            this.splitContainer_Center.Size = new System.Drawing.Size(820, 572);
            this.splitContainer_Center.SplitterDistance = 523;
            this.splitContainer_Center.SplitterWidth = 1;
            this.splitContainer_Center.TabIndex = 10;
            // 
            // imageBoxEx_VisionOfPanel
            // 
            this.imageBoxEx_VisionOfPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imageBoxEx_VisionOfPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxEx_VisionOfPanel.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageBoxEx_VisionOfPanel.GridColor = System.Drawing.Color.Black;
            this.imageBoxEx_VisionOfPanel.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.imageBoxEx_VisionOfPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imageBoxEx_VisionOfPanel.LineSelectionRegionEnd = ((System.Drawing.PointF)(resources.GetObject("imageBoxEx_VisionOfPanel.LineSelectionRegionEnd")));
            this.imageBoxEx_VisionOfPanel.LineSelectionRegionStart = ((System.Drawing.PointF)(resources.GetObject("imageBoxEx_VisionOfPanel.LineSelectionRegionStart")));
            this.imageBoxEx_VisionOfPanel.Location = new System.Drawing.Point(0, 0);
            this.imageBoxEx_VisionOfPanel.Name = "imageBoxEx_VisionOfPanel";
            this.imageBoxEx_VisionOfPanel.SelectionColor = System.Drawing.Color.Aqua;
            this.imageBoxEx_VisionOfPanel.Size = new System.Drawing.Size(818, 521);
            this.imageBoxEx_VisionOfPanel.StepSize = new System.Drawing.Size(1, 1);
            this.imageBoxEx_VisionOfPanel.TabIndex = 0;
            this.imageBoxEx_VisionOfPanel.Zoom = 15;
            this.imageBoxEx_VisionOfPanel.SelectionRegionChanged += new System.EventHandler(this.imageBoxEx_VisionOfPanel_SelectionRegionChanged);
            this.imageBoxEx_VisionOfPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.imageBoxEx_VisionOfPanel_MouseWheel);
            this.imageBoxEx_VisionOfPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBoxEx_VisionOfPanel_Paint);
            this.imageBoxEx_VisionOfPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBoxEx_VisionOfPanel_MouseMove);
            // 
            // FrmImgFindModoelOfPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(820, 597);
            this.Controls.Add(this.splitContainer_Center);
            this.Controls.Add(this.panel_Bottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmImgFindModoelOfPanel";
            this.Text = "FrmVisionOfPanel";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.VisibleChanged += new System.EventHandler(this.Frm_VisibleChanged);
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            this.statusStrip_Bottom.ResumeLayout(false);
            this.statusStrip_Bottom.PerformLayout();
            this.splitContainer_Center.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Center)).EndInit();
            this.splitContainer_Center.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.StatusStrip statusStrip_Bottom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ShowMenu;
        private System.Windows.Forms.ImageList ImageList1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCursorPosition;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSelection;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLableZoom;
        private System.Windows.Forms.ImageList imageList_IconsOfVision;
        private System.Windows.Forms.ImageList imageListCheck;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_HIdeMenu;
        private System.Windows.Forms.SplitContainer splitContainer_Center;
        private ImageBoxEx imageBoxEx_VisionOfPanel;
    }
}