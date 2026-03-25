namespace EzIna
{
    partial class FrmInforSetupIO_SXGA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInforSetupIO_SXGA));
            this.TabControl_IO = new System.Windows.Forms.TabControl();
            this.TabPage_DIO = new System.Windows.Forms.TabPage();
            this.Panel_DO = new System.Windows.Forms.Panel();
            this.DGV_DO = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_DI = new System.Windows.Forms.Panel();
            this.DGV_DI = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.TabPage_AIO = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btn_IO_CONFIG_SAVE = new System.Windows.Forms.Button();
            this.Panel_AI = new System.Windows.Forms.Panel();
            this.DGV_AI = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.Panel_AO = new System.Windows.Forms.Panel();
            this.DGV_AO = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.TabControl_IO.SuspendLayout();
            this.TabPage_DIO.SuspendLayout();
            this.Panel_DO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_DO)).BeginInit();
            this.Panel_DI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_DI)).BeginInit();
            this.TabPage_AIO.SuspendLayout();
            this.Panel_AI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_AI)).BeginInit();
            this.Panel_AO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_AO)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl_IO
            // 
            this.TabControl_IO.Controls.Add(this.TabPage_DIO);
            this.TabControl_IO.Controls.Add(this.TabPage_AIO);
            this.TabControl_IO.Dock = System.Windows.Forms.DockStyle.Left;
            this.TabControl_IO.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl_IO.Location = new System.Drawing.Point(0, 0);
            this.TabControl_IO.Name = "TabControl_IO";
            this.TabControl_IO.SelectedIndex = 0;
            this.TabControl_IO.Size = new System.Drawing.Size(1148, 908);
            this.TabControl_IO.TabIndex = 1;
            this.TabControl_IO.Tag = "";
            // 
            // TabPage_DIO
            // 
            this.TabPage_DIO.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_DIO.Controls.Add(this.Panel_DO);
            this.TabPage_DIO.Controls.Add(this.Panel_DI);
            this.TabPage_DIO.Location = new System.Drawing.Point(4, 28);
            this.TabPage_DIO.Name = "TabPage_DIO";
            this.TabPage_DIO.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_DIO.Size = new System.Drawing.Size(1140, 876);
            this.TabPage_DIO.TabIndex = 0;
            this.TabPage_DIO.Text = "Digital IO";
            // 
            // Panel_DO
            // 
            this.Panel_DO.Controls.Add(this.DGV_DO);
            this.Panel_DO.Controls.Add(this.label1);
            this.Panel_DO.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_DO.Location = new System.Drawing.Point(3, 438);
            this.Panel_DO.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_DO.Name = "Panel_DO";
            this.Panel_DO.Size = new System.Drawing.Size(1134, 435);
            this.Panel_DO.TabIndex = 12;
            // 
            // DGV_DO
            // 
            this.DGV_DO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_DO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_DO.Location = new System.Drawing.Point(0, 30);
            this.DGV_DO.Name = "DGV_DO";
            this.DGV_DO.RowTemplate.Height = 23;
            this.DGV_DO.Size = new System.Drawing.Size(1134, 405);
            this.DGV_DO.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1134, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "Output";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_DI
            // 
            this.Panel_DI.Controls.Add(this.DGV_DI);
            this.Panel_DI.Controls.Add(this.label2);
            this.Panel_DI.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_DI.Location = new System.Drawing.Point(3, 3);
            this.Panel_DI.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_DI.Name = "Panel_DI";
            this.Panel_DI.Size = new System.Drawing.Size(1134, 435);
            this.Panel_DI.TabIndex = 2;
            // 
            // DGV_DI
            // 
            this.DGV_DI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_DI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_DI.Location = new System.Drawing.Point(0, 30);
            this.DGV_DI.Name = "DGV_DI";
            this.DGV_DI.RowTemplate.Height = 23;
            this.DGV_DI.Size = new System.Drawing.Size(1134, 405);
            this.DGV_DI.TabIndex = 9;
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
            this.label2.Size = new System.Drawing.Size(1134, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Input";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabPage_AIO
            // 
            this.TabPage_AIO.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_AIO.Controls.Add(this.Panel_AO);
            this.TabPage_AIO.Controls.Add(this.Panel_AI);
            this.TabPage_AIO.Location = new System.Drawing.Point(4, 28);
            this.TabPage_AIO.Name = "TabPage_AIO";
            this.TabPage_AIO.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_AIO.Size = new System.Drawing.Size(1140, 876);
            this.TabPage_AIO.TabIndex = 1;
            this.TabPage_AIO.Text = "Analog IO";
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
            // btn_IO_CONFIG_SAVE
            // 
            this.btn_IO_CONFIG_SAVE.BackColor = System.Drawing.Color.White;
            this.btn_IO_CONFIG_SAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IO_CONFIG_SAVE.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IO_CONFIG_SAVE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_IO_CONFIG_SAVE.ImageIndex = 1;
            this.btn_IO_CONFIG_SAVE.Location = new System.Drawing.Point(1146, 28);
            this.btn_IO_CONFIG_SAVE.Name = "btn_IO_CONFIG_SAVE";
            this.btn_IO_CONFIG_SAVE.Size = new System.Drawing.Size(111, 51);
            this.btn_IO_CONFIG_SAVE.TabIndex = 95;
            this.btn_IO_CONFIG_SAVE.Tag = "SAVE";
            this.btn_IO_CONFIG_SAVE.Text = "Save";
            this.btn_IO_CONFIG_SAVE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IO_CONFIG_SAVE.UseVisualStyleBackColor = false;
            this.btn_IO_CONFIG_SAVE.Click += new System.EventHandler(this.btn_IO_CONFIG_SAVE_Click);
            // 
            // Panel_AI
            // 
            this.Panel_AI.Controls.Add(this.DGV_AI);
            this.Panel_AI.Controls.Add(this.label3);
            this.Panel_AI.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_AI.Location = new System.Drawing.Point(3, 3);
            this.Panel_AI.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_AI.Name = "Panel_AI";
            this.Panel_AI.Size = new System.Drawing.Size(1134, 435);
            this.Panel_AI.TabIndex = 2;
            // 
            // DGV_AI
            // 
            this.DGV_AI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_AI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_AI.Location = new System.Drawing.Point(0, 30);
            this.DGV_AI.Name = "DGV_AI";
            this.DGV_AI.RowTemplate.Height = 23;
            this.DGV_AI.Size = new System.Drawing.Size(1134, 405);
            this.DGV_AI.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.RoyalBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1134, 30);
            this.label3.TabIndex = 8;
            this.label3.Text = "Input";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_AO
            // 
            this.Panel_AO.Controls.Add(this.DGV_AO);
            this.Panel_AO.Controls.Add(this.label4);
            this.Panel_AO.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_AO.Location = new System.Drawing.Point(3, 438);
            this.Panel_AO.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_AO.Name = "Panel_AO";
            this.Panel_AO.Size = new System.Drawing.Size(1134, 435);
            this.Panel_AO.TabIndex = 11;
            // 
            // DGV_AO
            // 
            this.DGV_AO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_AO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_AO.Location = new System.Drawing.Point(0, 30);
            this.DGV_AO.Name = "DGV_AO";
            this.DGV_AO.RowTemplate.Height = 23;
            this.DGV_AO.Size = new System.Drawing.Size(1134, 405);
            this.DGV_AO.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1134, 30);
            this.label4.TabIndex = 9;
            this.label4.Text = "Output";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmInforSetupIO_SXGA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1262, 908);
            this.Controls.Add(this.btn_IO_CONFIG_SAVE);
            this.Controls.Add(this.TabControl_IO);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInforSetupIO_SXGA";
            this.Tag = "FORM_ID_INFOR_SETUP_IO";
            this.Text = "FrmInforSetupIO";
            this.Load += new System.EventHandler(this.FrmInforSetupIO_Load);
            this.Shown += new System.EventHandler(this.FrmInforSetupIO_SXGA_Shown);
            this.VisibleChanged += new System.EventHandler(this.FrmInforSetupIO_VisibleChanged);
            this.Resize += new System.EventHandler(this.FrmInforSetupIO_SXGA_Resize);
            this.TabControl_IO.ResumeLayout(false);
            this.TabPage_DIO.ResumeLayout(false);
            this.Panel_DO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_DO)).EndInit();
            this.Panel_DI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_DI)).EndInit();
            this.TabPage_AIO.ResumeLayout(false);
            this.Panel_AI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_AI)).EndInit();
            this.Panel_AO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_AO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl_IO;
        private System.Windows.Forms.TabPage TabPage_AIO;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.TabPage TabPage_DIO;
        private System.Windows.Forms.Panel Panel_DO;
        private System.Windows.Forms.DataGridView DGV_DO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Panel_DI;
        private System.Windows.Forms.DataGridView DGV_DI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel Panel_AO;
        private System.Windows.Forms.DataGridView DGV_AO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel Panel_AI;
        private System.Windows.Forms.DataGridView DGV_AI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_IO_CONFIG_SAVE;
    }
}