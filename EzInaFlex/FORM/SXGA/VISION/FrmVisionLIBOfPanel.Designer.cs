namespace EzIna
{
		partial class FrmVisionLIBOfPanel
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
						System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisionLIBOfPanel));
						this.panel_Top = new System.Windows.Forms.Panel();
						this.statusStrip_Top = new System.Windows.Forms.StatusStrip();
						this.toolStripStatusLabel_VisionName = new System.Windows.Forms.ToolStripStatusLabel();
						this.toolStripStatusLabel_SelectedGoldenImage = new System.Windows.Forms.ToolStripStatusLabel();
						this.toolStripStatusLabel_SelectedRoi = new System.Windows.Forms.ToolStripStatusLabel();
						this.toolStripSplitButtonSelectRoi = new System.Windows.Forms.ToolStripSplitButton();
						this.panel_Bottom = new System.Windows.Forms.Panel();
						this.statusStrip_Bottom = new System.Windows.Forms.StatusStrip();
						this.toolStripStatusLabelSelection = new System.Windows.Forms.ToolStripStatusLabel();
						this.toolStripStatusLableZoom = new System.Windows.Forms.ToolStripStatusLabel();
						this.toolStripStatusLabelCursorPosition = new System.Windows.Forms.ToolStripStatusLabel();
						this.toolStripStatusLabel_ShowMenu = new System.Windows.Forms.ToolStripStatusLabel();
						this.toolStripStatusLabel_HIdeMenu = new System.Windows.Forms.ToolStripStatusLabel();
						this.splitContainer_Center = new System.Windows.Forms.SplitContainer();
						this.imageBoxEx_VisionOfPanel = new EzIna.ImageBoxEx();
						this.rtb_ResultTextDP = new System.Windows.Forms.RichTextBox();
						this.imageList_IconsOfVision = new System.Windows.Forms.ImageList(this.components);
						this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
						this.label2 = new System.Windows.Forms.Label();
						this.treeView_GoldenImageList = new System.Windows.Forms.TreeView();
						this.imageListCheck = new System.Windows.Forms.ImageList(this.components);
						this.lbl_TrackbarValue = new System.Windows.Forms.Label();
						this.trackBar_Value = new System.Windows.Forms.TrackBar();
						this.gbtn_DataMatrix = new Glass.GlassButton();
						this.chkbox_CrossLine = new System.Windows.Forms.CheckBox();
						this.PictureBox_GoldenImage = new System.Windows.Forms.PictureBox();
						this.glassButton_PreviousMove = new Glass.GlassButton();
						this.gbtn_RoiMoveToCenter = new Glass.GlassButton();
						this.gbtn_Blob = new Glass.GlassButton();
						this.gbtn_Match = new Glass.GlassButton();
						this.chkbox_display_Filters = new System.Windows.Forms.CheckBox();
						this.chkbox_DispRois = new System.Windows.Forms.CheckBox();
						this.chkbox_DispMatchResult = new System.Windows.Forms.CheckBox();
						this.chkbox_ClickMove = new System.Windows.Forms.CheckBox();
						this.chkbox_DispBlobResult = new System.Windows.Forms.CheckBox();
						this.chkBox_BoxOfRoi = new System.Windows.Forms.CheckBox();
						this.toolStripSplitButtonGoldenImages = new System.Windows.Forms.ToolStripSplitButton();
						this.toolStripBtn_IMG_SAV = new System.Windows.Forms.ToolStripDropDownButton();
						this.toolStripBtn_IMG_LOAD = new System.Windows.Forms.ToolStripSplitButton();
						this.panel_Top.SuspendLayout();
						this.statusStrip_Top.SuspendLayout();
						this.panel_Bottom.SuspendLayout();
						this.statusStrip_Bottom.SuspendLayout();
						((System.ComponentModel.ISupportInitialize)(this.splitContainer_Center)).BeginInit();
						this.splitContainer_Center.Panel1.SuspendLayout();
						this.splitContainer_Center.Panel2.SuspendLayout();
						this.splitContainer_Center.SuspendLayout();
						((System.ComponentModel.ISupportInitialize)(this.trackBar_Value)).BeginInit();
						((System.ComponentModel.ISupportInitialize)(this.PictureBox_GoldenImage)).BeginInit();
						this.SuspendLayout();
						// 
						// panel_Top
						// 
						this.panel_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(68)))));
						this.panel_Top.Controls.Add(this.statusStrip_Top);
						this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
						this.panel_Top.Location = new System.Drawing.Point(0, 0);
						this.panel_Top.Name = "panel_Top";
						this.panel_Top.Size = new System.Drawing.Size(1024, 25);
						this.panel_Top.TabIndex = 7;
						// 
						// statusStrip_Top
						// 
						this.statusStrip_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.statusStrip_Top.Dock = System.Windows.Forms.DockStyle.Top;
						this.statusStrip_Top.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.statusStrip_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_VisionName,
            this.toolStripStatusLabel_SelectedGoldenImage,
            this.toolStripSplitButtonGoldenImages,
            this.toolStripStatusLabel_SelectedRoi,
            this.toolStripSplitButtonSelectRoi,
            this.toolStripBtn_IMG_SAV,
            this.toolStripBtn_IMG_LOAD});
						this.statusStrip_Top.Location = new System.Drawing.Point(0, 0);
						this.statusStrip_Top.Name = "statusStrip_Top";
						this.statusStrip_Top.Size = new System.Drawing.Size(1024, 29);
						this.statusStrip_Top.TabIndex = 11;
						// 
						// toolStripStatusLabel_VisionName
						// 
						this.toolStripStatusLabel_VisionName.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.toolStripStatusLabel_VisionName.ForeColor = System.Drawing.Color.Aqua;
						this.toolStripStatusLabel_VisionName.Name = "toolStripStatusLabel_VisionName";
						this.toolStripStatusLabel_VisionName.Size = new System.Drawing.Size(52, 24);
						this.toolStripStatusLabel_VisionName.Text = "FINE";
						// 
						// toolStripStatusLabel_SelectedGoldenImage
						// 
						this.toolStripStatusLabel_SelectedGoldenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.toolStripStatusLabel_SelectedGoldenImage.ForeColor = System.Drawing.Color.Lime;
						this.toolStripStatusLabel_SelectedGoldenImage.Name = "toolStripStatusLabel_SelectedGoldenImage";
						this.toolStripStatusLabel_SelectedGoldenImage.Size = new System.Drawing.Size(63, 24);
						this.toolStripStatusLabel_SelectedGoldenImage.Text = "________";
						// 
						// toolStripStatusLabel_SelectedRoi
						// 
						this.toolStripStatusLabel_SelectedRoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.toolStripStatusLabel_SelectedRoi.ForeColor = System.Drawing.Color.Lime;
						this.toolStripStatusLabel_SelectedRoi.Name = "toolStripStatusLabel_SelectedRoi";
						this.toolStripStatusLabel_SelectedRoi.Size = new System.Drawing.Size(63, 24);
						this.toolStripStatusLabel_SelectedRoi.Text = "________";
						// 
						// toolStripSplitButtonSelectRoi
						// 
						this.toolStripSplitButtonSelectRoi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
						this.toolStripSplitButtonSelectRoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.toolStripSplitButtonSelectRoi.ForeColor = System.Drawing.Color.White;
						this.toolStripSplitButtonSelectRoi.ImageTransparentColor = System.Drawing.Color.Magenta;
						this.toolStripSplitButtonSelectRoi.Name = "toolStripSplitButtonSelectRoi";
						this.toolStripSplitButtonSelectRoi.Size = new System.Drawing.Size(83, 27);
						this.toolStripSplitButtonSelectRoi.Text = "ROIs Items";
						this.toolStripSplitButtonSelectRoi.ToolTipText = "ROI";
						// 
						// panel_Bottom
						// 
						this.panel_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(68)))));
						this.panel_Bottom.Controls.Add(this.statusStrip_Bottom);
						this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
						this.panel_Bottom.Location = new System.Drawing.Point(0, 743);
						this.panel_Bottom.Name = "panel_Bottom";
						this.panel_Bottom.Size = new System.Drawing.Size(1024, 25);
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
						this.statusStrip_Bottom.Size = new System.Drawing.Size(1024, 22);
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
						this.toolStripStatusLabelCursorPosition.Size = new System.Drawing.Size(197, 17);
						this.toolStripStatusLabelCursorPosition.Text = "toolStripStatusLabelCursorPosition";
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
						this.splitContainer_Center.Location = new System.Drawing.Point(0, 25);
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
						this.splitContainer_Center.Panel2.Controls.Add(this.rtb_ResultTextDP);
						this.splitContainer_Center.Panel2.Controls.Add(this.gbtn_DataMatrix);
						this.splitContainer_Center.Panel2.Controls.Add(this.chkbox_CrossLine);
						this.splitContainer_Center.Panel2.Controls.Add(this.label2);
						this.splitContainer_Center.Panel2.Controls.Add(this.PictureBox_GoldenImage);
						this.splitContainer_Center.Panel2.Controls.Add(this.treeView_GoldenImageList);
						this.splitContainer_Center.Panel2.Controls.Add(this.glassButton_PreviousMove);
						this.splitContainer_Center.Panel2.Controls.Add(this.gbtn_RoiMoveToCenter);
						this.splitContainer_Center.Panel2.Controls.Add(this.gbtn_Blob);
						this.splitContainer_Center.Panel2.Controls.Add(this.gbtn_Match);
						this.splitContainer_Center.Panel2.Controls.Add(this.lbl_TrackbarValue);
						this.splitContainer_Center.Panel2.Controls.Add(this.trackBar_Value);
						this.splitContainer_Center.Panel2.Controls.Add(this.chkbox_display_Filters);
						this.splitContainer_Center.Panel2.Controls.Add(this.chkbox_DispRois);
						this.splitContainer_Center.Panel2.Controls.Add(this.chkbox_DispMatchResult);
						this.splitContainer_Center.Panel2.Controls.Add(this.chkbox_ClickMove);
						this.splitContainer_Center.Panel2.Controls.Add(this.chkbox_DispBlobResult);
						this.splitContainer_Center.Panel2.Controls.Add(this.chkBox_BoxOfRoi);
						this.splitContainer_Center.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.splitContainer_Center.Panel2.ForeColor = System.Drawing.Color.Black;
						this.splitContainer_Center.Panel2MinSize = 0;
						this.splitContainer_Center.Size = new System.Drawing.Size(1024, 718);
						this.splitContainer_Center.SplitterDistance = 522;
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
						this.imageBoxEx_VisionOfPanel.Location = new System.Drawing.Point(0, 0);
						this.imageBoxEx_VisionOfPanel.Name = "imageBoxEx_VisionOfPanel";
						this.imageBoxEx_VisionOfPanel.SelectionColor = System.Drawing.Color.Aqua;
						this.imageBoxEx_VisionOfPanel.Size = new System.Drawing.Size(1022, 520);
						this.imageBoxEx_VisionOfPanel.StepSize = new System.Drawing.Size(1, 1);
						this.imageBoxEx_VisionOfPanel.TabIndex = 0;
						this.imageBoxEx_VisionOfPanel.Zoom = 15;
						this.imageBoxEx_VisionOfPanel.SelectionRegionChanged += new System.EventHandler(this.imageBoxEx_VisionOfPanel_SelectionRegionChanged);
						this.imageBoxEx_VisionOfPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.imageBoxEx_VisionOfPanel_MouseWheel);
						this.imageBoxEx_VisionOfPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBoxEx_VisionOfPanel_Paint);
						this.imageBoxEx_VisionOfPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBoxEx_VisionOfPanel_MouseMove);
						// 
						// rtb_ResultTextDP
						// 
						this.rtb_ResultTextDP.Location = new System.Drawing.Point(354, 3);
						this.rtb_ResultTextDP.Name = "rtb_ResultTextDP";
						this.rtb_ResultTextDP.ReadOnly = true;
						this.rtb_ResultTextDP.Size = new System.Drawing.Size(243, 74);
						this.rtb_ResultTextDP.TabIndex = 106;
						this.rtb_ResultTextDP.Text = "";
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
						// label2
						// 
						this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.label2.ForeColor = System.Drawing.Color.Black;
						this.label2.Location = new System.Drawing.Point(224, 57);
						this.label2.Name = "label2";
						this.label2.Size = new System.Drawing.Size(90, 20);
						this.label2.TabIndex = 103;
						this.label2.Text = "Expose Time";
						// 
						// treeView_GoldenImageList
						// 
						this.treeView_GoldenImageList.BackColor = System.Drawing.Color.White;
						this.treeView_GoldenImageList.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.treeView_GoldenImageList.ImageIndex = 0;
						this.treeView_GoldenImageList.ImageList = this.imageListCheck;
						this.treeView_GoldenImageList.Indent = 30;
						this.treeView_GoldenImageList.ItemHeight = 35;
						this.treeView_GoldenImageList.LineColor = System.Drawing.Color.Gray;
						this.treeView_GoldenImageList.Location = new System.Drawing.Point(603, 3);
						this.treeView_GoldenImageList.Name = "treeView_GoldenImageList";
						this.treeView_GoldenImageList.SelectedImageIndex = 0;
						this.treeView_GoldenImageList.Size = new System.Drawing.Size(272, 136);
						this.treeView_GoldenImageList.TabIndex = 100;
						this.treeView_GoldenImageList.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_GoldenImageList_AfterExpand);
						this.treeView_GoldenImageList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_GoldenImageList_AfterSelect);
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
						// lbl_TrackbarValue
						// 
						this.lbl_TrackbarValue.ForeColor = System.Drawing.Color.Black;
						this.lbl_TrackbarValue.Location = new System.Drawing.Point(224, 117);
						this.lbl_TrackbarValue.Name = "lbl_TrackbarValue";
						this.lbl_TrackbarValue.Size = new System.Drawing.Size(124, 22);
						this.lbl_TrackbarValue.TabIndex = 27;
						this.lbl_TrackbarValue.Text = "0";
						this.lbl_TrackbarValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
						// 
						// trackBar_Value
						// 
						this.trackBar_Value.AutoSize = false;
						this.trackBar_Value.Cursor = System.Windows.Forms.Cursors.SizeWE;
						this.trackBar_Value.LargeChange = 100;
						this.trackBar_Value.Location = new System.Drawing.Point(224, 86);
						this.trackBar_Value.Maximum = 5000;
						this.trackBar_Value.Minimum = 1000;
						this.trackBar_Value.Name = "trackBar_Value";
						this.trackBar_Value.Size = new System.Drawing.Size(132, 22);
						this.trackBar_Value.SmallChange = 10;
						this.trackBar_Value.TabIndex = 0;
						this.trackBar_Value.TickFrequency = 100;
						this.trackBar_Value.Value = 2000;
						this.trackBar_Value.Scroll += new System.EventHandler(this.trackBar_Value_Scroll);
						// 
						// gbtn_DataMatrix
						// 
						this.gbtn_DataMatrix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
						this.gbtn_DataMatrix.ImageIndex = 6;
						this.gbtn_DataMatrix.ImageList = this.imageList_IconsOfVision;
						this.gbtn_DataMatrix.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_DataMatrix.Location = new System.Drawing.Point(57, 4);
						this.gbtn_DataMatrix.Name = "gbtn_DataMatrix";
						this.gbtn_DataMatrix.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_DataMatrix.Size = new System.Drawing.Size(48, 48);
						this.gbtn_DataMatrix.TabIndex = 105;
						this.gbtn_DataMatrix.Click += new System.EventHandler(this.gbtn_DataMatrix_Click);
						// 
						// chkbox_CrossLine
						// 
						this.chkbox_CrossLine.Appearance = System.Windows.Forms.Appearance.Button;
						this.chkbox_CrossLine.FlatAppearance.BorderSize = 0;
						this.chkbox_CrossLine.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
						this.chkbox_CrossLine.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
						this.chkbox_CrossLine.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
						this.chkbox_CrossLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.chkbox_CrossLine.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.chkbox_CrossLine.ForeColor = System.Drawing.Color.Black;
						this.chkbox_CrossLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.chkbox_CrossLine.ImageIndex = 1;
						this.chkbox_CrossLine.ImageList = this.ImageList1;
						this.chkbox_CrossLine.Location = new System.Drawing.Point(11, 57);
						this.chkbox_CrossLine.Name = "chkbox_CrossLine";
						this.chkbox_CrossLine.Size = new System.Drawing.Size(106, 22);
						this.chkbox_CrossLine.TabIndex = 104;
						this.chkbox_CrossLine.Tag = "0";
						this.chkbox_CrossLine.Text = "          Cross line";
						this.chkbox_CrossLine.UseVisualStyleBackColor = true;
						// 
						// PictureBox_GoldenImage
						// 
						this.PictureBox_GoldenImage.BackColor = System.Drawing.Color.Black;
						this.PictureBox_GoldenImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
						this.PictureBox_GoldenImage.Location = new System.Drawing.Point(881, 3);
						this.PictureBox_GoldenImage.Name = "PictureBox_GoldenImage";
						this.PictureBox_GoldenImage.Size = new System.Drawing.Size(136, 136);
						this.PictureBox_GoldenImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
						this.PictureBox_GoldenImage.TabIndex = 101;
						this.PictureBox_GoldenImage.TabStop = false;
						// 
						// glassButton_PreviousMove
						// 
						this.glassButton_PreviousMove.BackColor = System.Drawing.Color.White;
						this.glassButton_PreviousMove.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.glassButton_PreviousMove.ForeColor = System.Drawing.Color.Black;
						this.glassButton_PreviousMove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.glassButton_PreviousMove.ImageIndex = 3;
						this.glassButton_PreviousMove.ImageList = this.imageListCheck;
						this.glassButton_PreviousMove.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.glassButton_PreviousMove.Location = new System.Drawing.Point(494, 140);
						this.glassButton_PreviousMove.Name = "glassButton_PreviousMove";
						this.glassButton_PreviousMove.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.glassButton_PreviousMove.Size = new System.Drawing.Size(103, 48);
						this.glassButton_PreviousMove.TabIndex = 28;
						this.glassButton_PreviousMove.Text = "Previous\r\nMove";
						this.glassButton_PreviousMove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
						this.glassButton_PreviousMove.Visible = false;
						this.glassButton_PreviousMove.Click += new System.EventHandler(this.glassButton_PreviousMove_Click);
						// 
						// gbtn_RoiMoveToCenter
						// 
						this.gbtn_RoiMoveToCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
						this.gbtn_RoiMoveToCenter.ImageIndex = 5;
						this.gbtn_RoiMoveToCenter.ImageList = this.imageList_IconsOfVision;
						this.gbtn_RoiMoveToCenter.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_RoiMoveToCenter.Location = new System.Drawing.Point(111, 4);
						this.gbtn_RoiMoveToCenter.Name = "gbtn_RoiMoveToCenter";
						this.gbtn_RoiMoveToCenter.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_RoiMoveToCenter.Size = new System.Drawing.Size(48, 48);
						this.gbtn_RoiMoveToCenter.TabIndex = 28;
						// 
						// gbtn_Blob
						// 
						this.gbtn_Blob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
						this.gbtn_Blob.ImageIndex = 4;
						this.gbtn_Blob.ImageList = this.imageList_IconsOfVision;
						this.gbtn_Blob.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_Blob.Location = new System.Drawing.Point(165, 4);
						this.gbtn_Blob.Name = "gbtn_Blob";
						this.gbtn_Blob.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_Blob.Size = new System.Drawing.Size(48, 48);
						this.gbtn_Blob.TabIndex = 28;
						this.gbtn_Blob.Visible = false;
						this.gbtn_Blob.Click += new System.EventHandler(this.btnBlob_Click);
						// 
						// gbtn_Match
						// 
						this.gbtn_Match.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
						this.gbtn_Match.ImageIndex = 3;
						this.gbtn_Match.ImageList = this.imageList_IconsOfVision;
						this.gbtn_Match.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_Match.Location = new System.Drawing.Point(3, 3);
						this.gbtn_Match.Name = "gbtn_Match";
						this.gbtn_Match.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
						this.gbtn_Match.Size = new System.Drawing.Size(48, 48);
						this.gbtn_Match.TabIndex = 28;
						this.gbtn_Match.Click += new System.EventHandler(this.btnMatch_Click);
						// 
						// chkbox_display_Filters
						// 
						this.chkbox_display_Filters.Appearance = System.Windows.Forms.Appearance.Button;
						this.chkbox_display_Filters.FlatAppearance.BorderSize = 0;
						this.chkbox_display_Filters.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
						this.chkbox_display_Filters.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
						this.chkbox_display_Filters.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
						this.chkbox_display_Filters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.chkbox_display_Filters.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.chkbox_display_Filters.ForeColor = System.Drawing.Color.Black;
						this.chkbox_display_Filters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.chkbox_display_Filters.ImageIndex = 0;
						this.chkbox_display_Filters.ImageList = this.ImageList1;
						this.chkbox_display_Filters.Location = new System.Drawing.Point(129, 86);
						this.chkbox_display_Filters.Name = "chkbox_display_Filters";
						this.chkbox_display_Filters.Size = new System.Drawing.Size(106, 22);
						this.chkbox_display_Filters.TabIndex = 25;
						this.chkbox_display_Filters.Tag = "2";
						this.chkbox_display_Filters.Text = "          Filters";
						this.chkbox_display_Filters.UseVisualStyleBackColor = true;
						// 
						// chkbox_DispRois
						// 
						this.chkbox_DispRois.Appearance = System.Windows.Forms.Appearance.Button;
						this.chkbox_DispRois.FlatAppearance.BorderSize = 0;
						this.chkbox_DispRois.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
						this.chkbox_DispRois.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
						this.chkbox_DispRois.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
						this.chkbox_DispRois.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.chkbox_DispRois.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.chkbox_DispRois.ForeColor = System.Drawing.Color.Black;
						this.chkbox_DispRois.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.chkbox_DispRois.ImageIndex = 0;
						this.chkbox_DispRois.ImageList = this.ImageList1;
						this.chkbox_DispRois.Location = new System.Drawing.Point(11, 86);
						this.chkbox_DispRois.Name = "chkbox_DispRois";
						this.chkbox_DispRois.Size = new System.Drawing.Size(106, 22);
						this.chkbox_DispRois.TabIndex = 25;
						this.chkbox_DispRois.Tag = "5";
						this.chkbox_DispRois.Text = "          ROIs";
						this.chkbox_DispRois.UseVisualStyleBackColor = true;
						// 
						// chkbox_DispMatchResult
						// 
						this.chkbox_DispMatchResult.Appearance = System.Windows.Forms.Appearance.Button;
						this.chkbox_DispMatchResult.FlatAppearance.BorderSize = 0;
						this.chkbox_DispMatchResult.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
						this.chkbox_DispMatchResult.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
						this.chkbox_DispMatchResult.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
						this.chkbox_DispMatchResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.chkbox_DispMatchResult.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.chkbox_DispMatchResult.ForeColor = System.Drawing.Color.Black;
						this.chkbox_DispMatchResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.chkbox_DispMatchResult.ImageIndex = 0;
						this.chkbox_DispMatchResult.ImageList = this.ImageList1;
						this.chkbox_DispMatchResult.Location = new System.Drawing.Point(129, 117);
						this.chkbox_DispMatchResult.Name = "chkbox_DispMatchResult";
						this.chkbox_DispMatchResult.Size = new System.Drawing.Size(106, 22);
						this.chkbox_DispMatchResult.TabIndex = 25;
						this.chkbox_DispMatchResult.Tag = "3";
						this.chkbox_DispMatchResult.Text = "          Match result";
						this.chkbox_DispMatchResult.UseVisualStyleBackColor = true;
						// 
						// chkbox_ClickMove
						// 
						this.chkbox_ClickMove.Appearance = System.Windows.Forms.Appearance.Button;
						this.chkbox_ClickMove.FlatAppearance.BorderSize = 0;
						this.chkbox_ClickMove.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
						this.chkbox_ClickMove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
						this.chkbox_ClickMove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
						this.chkbox_ClickMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.chkbox_ClickMove.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.chkbox_ClickMove.ForeColor = System.Drawing.Color.Black;
						this.chkbox_ClickMove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.chkbox_ClickMove.ImageIndex = 0;
						this.chkbox_ClickMove.ImageList = this.imageListCheck;
						this.chkbox_ClickMove.Location = new System.Drawing.Point(385, 140);
						this.chkbox_ClickMove.Name = "chkbox_ClickMove";
						this.chkbox_ClickMove.Size = new System.Drawing.Size(103, 48);
						this.chkbox_ClickMove.TabIndex = 25;
						this.chkbox_ClickMove.Tag = "4";
						this.chkbox_ClickMove.Text = "Click\r\nMove\r\n";
						this.chkbox_ClickMove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
						this.chkbox_ClickMove.UseVisualStyleBackColor = true;
						this.chkbox_ClickMove.Visible = false;
						this.chkbox_ClickMove.Click += new System.EventHandler(this.chkbox_ClickMove_Click);
						// 
						// chkbox_DispBlobResult
						// 
						this.chkbox_DispBlobResult.Appearance = System.Windows.Forms.Appearance.Button;
						this.chkbox_DispBlobResult.FlatAppearance.BorderSize = 0;
						this.chkbox_DispBlobResult.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
						this.chkbox_DispBlobResult.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
						this.chkbox_DispBlobResult.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
						this.chkbox_DispBlobResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.chkbox_DispBlobResult.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.chkbox_DispBlobResult.ForeColor = System.Drawing.Color.Black;
						this.chkbox_DispBlobResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.chkbox_DispBlobResult.ImageIndex = 0;
						this.chkbox_DispBlobResult.ImageList = this.ImageList1;
						this.chkbox_DispBlobResult.Location = new System.Drawing.Point(129, 57);
						this.chkbox_DispBlobResult.Name = "chkbox_DispBlobResult";
						this.chkbox_DispBlobResult.Size = new System.Drawing.Size(106, 22);
						this.chkbox_DispBlobResult.TabIndex = 25;
						this.chkbox_DispBlobResult.Tag = "4";
						this.chkbox_DispBlobResult.Text = "          Blob Result";
						this.chkbox_DispBlobResult.UseVisualStyleBackColor = true;
						// 
						// chkBox_BoxOfRoi
						// 
						this.chkBox_BoxOfRoi.Appearance = System.Windows.Forms.Appearance.Button;
						this.chkBox_BoxOfRoi.FlatAppearance.BorderSize = 0;
						this.chkBox_BoxOfRoi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
						this.chkBox_BoxOfRoi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
						this.chkBox_BoxOfRoi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
						this.chkBox_BoxOfRoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
						this.chkBox_BoxOfRoi.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.chkBox_BoxOfRoi.ForeColor = System.Drawing.Color.Black;
						this.chkBox_BoxOfRoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						this.chkBox_BoxOfRoi.ImageIndex = 0;
						this.chkBox_BoxOfRoi.ImageList = this.ImageList1;
						this.chkBox_BoxOfRoi.Location = new System.Drawing.Point(11, 117);
						this.chkBox_BoxOfRoi.Name = "chkBox_BoxOfRoi";
						this.chkBox_BoxOfRoi.Size = new System.Drawing.Size(106, 22);
						this.chkBox_BoxOfRoi.TabIndex = 25;
						this.chkBox_BoxOfRoi.Tag = "1";
						this.chkBox_BoxOfRoi.Text = "          Box of ROI";
						this.chkBox_BoxOfRoi.UseVisualStyleBackColor = true;
						// 
						// toolStripSplitButtonGoldenImages
						// 
						this.toolStripSplitButtonGoldenImages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
						this.toolStripSplitButtonGoldenImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						this.toolStripSplitButtonGoldenImages.ForeColor = System.Drawing.Color.White;
						this.toolStripSplitButtonGoldenImages.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonGoldenImages.Image")));
						this.toolStripSplitButtonGoldenImages.ImageTransparentColor = System.Drawing.Color.Magenta;
						this.toolStripSplitButtonGoldenImages.Name = "toolStripSplitButtonGoldenImages";
						this.toolStripSplitButtonGoldenImages.Size = new System.Drawing.Size(107, 27);
						this.toolStripSplitButtonGoldenImages.Text = "Golden Images";
						// 
						// toolStripBtn_IMG_SAV
						// 
						this.toolStripBtn_IMG_SAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
						this.toolStripBtn_IMG_SAV.ForeColor = System.Drawing.Color.White;
						this.toolStripBtn_IMG_SAV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_IMG_SAV.Image")));
						this.toolStripBtn_IMG_SAV.ImageTransparentColor = System.Drawing.Color.Magenta;
						this.toolStripBtn_IMG_SAV.Name = "toolStripBtn_IMG_SAV";
						this.toolStripBtn_IMG_SAV.Size = new System.Drawing.Size(101, 27);
						this.toolStripBtn_IMG_SAV.Text = "Image Save";
						// 
						// toolStripBtn_IMG_LOAD
						// 
						this.toolStripBtn_IMG_LOAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
						this.toolStripBtn_IMG_LOAD.ForeColor = System.Drawing.Color.White;
						this.toolStripBtn_IMG_LOAD.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_IMG_LOAD.Image")));
						this.toolStripBtn_IMG_LOAD.ImageTransparentColor = System.Drawing.Color.Magenta;
						this.toolStripBtn_IMG_LOAD.Name = "toolStripBtn_IMG_LOAD";
						this.toolStripBtn_IMG_LOAD.Size = new System.Drawing.Size(105, 27);
						this.toolStripBtn_IMG_LOAD.Text = "Image Load";
						// 
						// FrmVisionLIBOfPanel
						// 
						this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
						this.ClientSize = new System.Drawing.Size(1024, 768);
						this.Controls.Add(this.splitContainer_Center);
						this.Controls.Add(this.panel_Top);
						this.Controls.Add(this.panel_Bottom);
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
						this.Name = "FrmVisionLIBOfPanel";
						this.Text = "FrmVisionOfPanel";
						this.Load += new System.EventHandler(this.FrmVisionOfPanel_Load);
						this.VisibleChanged += new System.EventHandler(this.FrmVisionOfPanel_VisibleChanged);
						this.panel_Top.ResumeLayout(false);
						this.panel_Top.PerformLayout();
						this.statusStrip_Top.ResumeLayout(false);
						this.statusStrip_Top.PerformLayout();
						this.panel_Bottom.ResumeLayout(false);
						this.panel_Bottom.PerformLayout();
						this.statusStrip_Bottom.ResumeLayout(false);
						this.statusStrip_Bottom.PerformLayout();
						this.splitContainer_Center.Panel1.ResumeLayout(false);
						this.splitContainer_Center.Panel2.ResumeLayout(false);
						((System.ComponentModel.ISupportInitialize)(this.splitContainer_Center)).EndInit();
						this.splitContainer_Center.ResumeLayout(false);
						((System.ComponentModel.ISupportInitialize)(this.trackBar_Value)).EndInit();
						((System.ComponentModel.ISupportInitialize)(this.PictureBox_GoldenImage)).EndInit();
						this.ResumeLayout(false);

				}

				#endregion

				private System.Windows.Forms.Panel panel_Top;
				private System.Windows.Forms.StatusStrip statusStrip_Top;
				private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonSelectRoi;
				private System.Windows.Forms.Panel panel_Bottom;
				private System.Windows.Forms.StatusStrip statusStrip_Bottom;
				private System.Windows.Forms.SplitContainer splitContainer_Center;
				private EzIna.ImageBoxEx imageBoxEx_VisionOfPanel;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ShowMenu;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_HIdeMenu;
				private System.Windows.Forms.CheckBox chkBox_BoxOfRoi;
				private System.Windows.Forms.CheckBox chkbox_display_Filters;
				private System.Windows.Forms.Label lbl_TrackbarValue;
				private System.Windows.Forms.TrackBar trackBar_Value;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_VisionName;
				private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonGoldenImages;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_SelectedRoi;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_SelectedGoldenImage;
				private Glass.GlassButton gbtn_RoiMoveToCenter;
				private Glass.GlassButton gbtn_Blob;
				private Glass.GlassButton gbtn_Match;
				private System.Windows.Forms.CheckBox chkbox_DispRois;
				private System.Windows.Forms.CheckBox chkbox_DispMatchResult;
				private System.Windows.Forms.CheckBox chkbox_DispBlobResult;
				private System.Windows.Forms.ImageList ImageList1;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCursorPosition;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSelection;
				private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLableZoom;
				private System.Windows.Forms.ImageList imageList_IconsOfVision;
				private System.Windows.Forms.TreeView treeView_GoldenImageList;
				private System.Windows.Forms.PictureBox PictureBox_GoldenImage;
				private System.Windows.Forms.ImageList imageListCheck;
				private System.Windows.Forms.Label label2;
				private System.Windows.Forms.CheckBox chkbox_CrossLine;
				private Glass.GlassButton glassButton_PreviousMove;
				private System.Windows.Forms.CheckBox chkbox_ClickMove;
				private System.Windows.Forms.ToolStripDropDownButton toolStripBtn_IMG_SAV;
				private Glass.GlassButton gbtn_DataMatrix;
				private System.Windows.Forms.RichTextBox rtb_ResultTextDP;
				private System.Windows.Forms.ToolStripSplitButton toolStripBtn_IMG_LOAD;
		}
}