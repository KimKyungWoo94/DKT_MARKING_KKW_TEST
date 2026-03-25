namespace EzIna
{
	partial class FrmPopupLogin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupLogin));
			this.panelFrmTitleBar = new System.Windows.Forms.Panel();
			this.btn_Frm_Close = new System.Windows.Forms.Button();
			this.lbl_Name = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lbl_Msg = new System.Windows.Forms.Label();
			this.edtPassword = new System.Windows.Forms.TextBox();
			this.btn_Supervisor = new System.Windows.Forms.Button();
			this.imageList_chkbox = new System.Windows.Forms.ImageList(this.components);
			this.btn_Logout = new System.Windows.Forms.Button();
			this.btn_Login = new System.Windows.Forms.Button();
			this.btn_Engineer = new System.Windows.Forms.Button();
			this.btn_Operator = new System.Windows.Forms.Button();
			this.panelFrmTitleBar.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelFrmTitleBar
			// 
			this.panelFrmTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.panelFrmTitleBar.Controls.Add(this.btn_Frm_Close);
			this.panelFrmTitleBar.Controls.Add(this.lbl_Name);
			this.panelFrmTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelFrmTitleBar.Location = new System.Drawing.Point(0, 0);
			this.panelFrmTitleBar.Name = "panelFrmTitleBar";
			this.panelFrmTitleBar.Size = new System.Drawing.Size(397, 32);
			this.panelFrmTitleBar.TabIndex = 131;
			this.panelFrmTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFrmTitleBar_MouseDown);
			// 
			// btn_Frm_Close
			// 
			this.btn_Frm_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btn_Frm_Close.AutoSize = true;
			this.btn_Frm_Close.FlatAppearance.BorderSize = 0;
			this.btn_Frm_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Frm_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Frm_Close.Image")));
			this.btn_Frm_Close.Location = new System.Drawing.Point(363, 1);
			this.btn_Frm_Close.Name = "btn_Frm_Close";
			this.btn_Frm_Close.Size = new System.Drawing.Size(30, 30);
			this.btn_Frm_Close.TabIndex = 4;
			this.btn_Frm_Close.Tag = "WithoutCommonStyle";
			this.btn_Frm_Close.UseVisualStyleBackColor = true;
			this.btn_Frm_Close.Click += new System.EventHandler(this.btn_Frm_Close_Click);
			// 
			// lbl_Name
			// 
			this.lbl_Name.AutoSize = true;
			this.lbl_Name.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Name.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Name.ForeColor = System.Drawing.Color.White;
			this.lbl_Name.ImageIndex = 2;
			this.lbl_Name.Location = new System.Drawing.Point(7, 7);
			this.lbl_Name.Name = "lbl_Name";
			this.lbl_Name.Size = new System.Drawing.Size(62, 21);
			this.lbl_Name.TabIndex = 3;
			this.lbl_Name.Tag = "FormName";
			this.lbl_Name.Text = "LOGIN";
			this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl_Name.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelFrmTitleBar_MouseDown);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lbl_Msg);
			this.panel1.Controls.Add(this.edtPassword);
			this.panel1.Controls.Add(this.btn_Supervisor);
			this.panel1.Controls.Add(this.btn_Logout);
			this.panel1.Controls.Add(this.btn_Login);
			this.panel1.Controls.Add(this.btn_Engineer);
			this.panel1.Controls.Add(this.btn_Operator);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(397, 259);
			this.panel1.TabIndex = 132;
			// 
			// lbl_Msg
			// 
			this.lbl_Msg.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Msg.Location = new System.Drawing.Point(11, 158);
			this.lbl_Msg.Name = "lbl_Msg";
			this.lbl_Msg.Size = new System.Drawing.Size(372, 35);
			this.lbl_Msg.TabIndex = 1374;
			this.lbl_Msg.Text = "Message";
			// 
			// edtPassword
			// 
			this.edtPassword.Location = new System.Drawing.Point(10, 134);
			this.edtPassword.Name = "edtPassword";
			this.edtPassword.PasswordChar = '*';
			this.edtPassword.Size = new System.Drawing.Size(373, 21);
			this.edtPassword.TabIndex = 1373;
			// 
			// btn_Supervisor
			// 
			this.btn_Supervisor.BackColor = System.Drawing.Color.White;
			this.btn_Supervisor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Supervisor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Supervisor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Supervisor.ImageIndex = 0;
			this.btn_Supervisor.ImageList = this.imageList_chkbox;
			this.btn_Supervisor.Location = new System.Drawing.Point(263, 48);
			this.btn_Supervisor.Name = "btn_Supervisor";
			this.btn_Supervisor.Size = new System.Drawing.Size(120, 80);
			this.btn_Supervisor.TabIndex = 1372;
			this.btn_Supervisor.Tag = "";
			this.btn_Supervisor.Text = "Supervisor";
			this.btn_Supervisor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Supervisor.UseVisualStyleBackColor = false;
			this.btn_Supervisor.Click += new System.EventHandler(this.btn_Operator_Click);
			// 
			// imageList_chkbox
			// 
			this.imageList_chkbox.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_chkbox.ImageStream")));
			this.imageList_chkbox.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList_chkbox.Images.SetKeyName(0, "unchecked.png");
			this.imageList_chkbox.Images.SetKeyName(1, "checked.png");
			this.imageList_chkbox.Images.SetKeyName(2, "icons8-padlock-32.png");
			this.imageList_chkbox.Images.SetKeyName(3, "icons8-lock-32.png");
			// 
			// btn_Logout
			// 
			this.btn_Logout.BackColor = System.Drawing.Color.White;
			this.btn_Logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Logout.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Logout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Logout.ImageIndex = 2;
			this.btn_Logout.ImageList = this.imageList_chkbox;
			this.btn_Logout.Location = new System.Drawing.Point(263, 196);
			this.btn_Logout.Name = "btn_Logout";
			this.btn_Logout.Size = new System.Drawing.Size(120, 50);
			this.btn_Logout.TabIndex = 1372;
			this.btn_Logout.Tag = "";
			this.btn_Logout.Text = "Logout";
			this.btn_Logout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Logout.UseVisualStyleBackColor = false;
			// 
			// btn_Login
			// 
			this.btn_Login.BackColor = System.Drawing.Color.White;
			this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Login.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Login.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Login.ImageIndex = 3;
			this.btn_Login.ImageList = this.imageList_chkbox;
			this.btn_Login.Location = new System.Drawing.Point(137, 196);
			this.btn_Login.Name = "btn_Login";
			this.btn_Login.Size = new System.Drawing.Size(120, 50);
			this.btn_Login.TabIndex = 1372;
			this.btn_Login.Tag = "";
			this.btn_Login.Text = "Login";
			this.btn_Login.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Login.UseVisualStyleBackColor = false;
			this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
			// 
			// btn_Engineer
			// 
			this.btn_Engineer.BackColor = System.Drawing.Color.White;
			this.btn_Engineer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Engineer.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Engineer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Engineer.ImageIndex = 0;
			this.btn_Engineer.ImageList = this.imageList_chkbox;
			this.btn_Engineer.Location = new System.Drawing.Point(137, 48);
			this.btn_Engineer.Name = "btn_Engineer";
			this.btn_Engineer.Size = new System.Drawing.Size(120, 80);
			this.btn_Engineer.TabIndex = 1372;
			this.btn_Engineer.Tag = "";
			this.btn_Engineer.Text = "Engineer";
			this.btn_Engineer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Engineer.UseVisualStyleBackColor = false;
			this.btn_Engineer.Click += new System.EventHandler(this.btn_Operator_Click);
			// 
			// btn_Operator
			// 
			this.btn_Operator.BackColor = System.Drawing.Color.White;
			this.btn_Operator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Operator.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Operator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Operator.ImageIndex = 0;
			this.btn_Operator.ImageList = this.imageList_chkbox;
			this.btn_Operator.Location = new System.Drawing.Point(11, 48);
			this.btn_Operator.Name = "btn_Operator";
			this.btn_Operator.Size = new System.Drawing.Size(120, 80);
			this.btn_Operator.TabIndex = 1372;
			this.btn_Operator.Tag = "";
			this.btn_Operator.Text = "Operator";
			this.btn_Operator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Operator.UseVisualStyleBackColor = false;
			this.btn_Operator.Click += new System.EventHandler(this.btn_Operator_Click);
			// 
			// FrmPopupLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(397, 259);
			this.Controls.Add(this.panelFrmTitleBar);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmPopupLogin";
			this.Text = "FrmPopupLogin";
			this.panelFrmTitleBar.ResumeLayout(false);
			this.panelFrmTitleBar.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panelFrmTitleBar;
		private System.Windows.Forms.Button btn_Frm_Close;
		private System.Windows.Forms.Label lbl_Name;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btn_Operator;
		private System.Windows.Forms.Button btn_Supervisor;
		private System.Windows.Forms.ImageList imageList_chkbox;
		private System.Windows.Forms.Button btn_Logout;
		private System.Windows.Forms.Button btn_Login;
		private System.Windows.Forms.Button btn_Engineer;
		private System.Windows.Forms.TextBox edtPassword;
		private System.Windows.Forms.Label lbl_Msg;
	}
}