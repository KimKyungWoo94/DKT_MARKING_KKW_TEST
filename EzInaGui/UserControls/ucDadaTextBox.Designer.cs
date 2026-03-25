namespace EzIna.GUI.UserControls
{
	partial class ucDadaTextBox
	{
		/// <summary> 
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDadaTextBox));
            this.panel_title = new System.Windows.Forms.Panel();
            this.panel_textbox = new System.Windows.Forms.Panel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panel_unit = new System.Windows.Forms.Panel();
            this.label_unit = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.panel_title.SuspendLayout();
            this.panel_textbox.SuspendLayout();
            this.panel_unit.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_title
            // 
            this.panel_title.Controls.Add(this.label_title);
            resources.ApplyResources(this.panel_title, "panel_title");
            this.panel_title.Name = "panel_title";
            // 
            // panel_textbox
            // 
            this.panel_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_textbox.Controls.Add(this.textBox);
            resources.ApplyResources(this.panel_textbox, "panel_textbox");
            this.panel_textbox.Name = "panel_textbox";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.Control;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox, "textBox");
            this.textBox.Name = "textBox";
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // panel_unit
            // 
            this.panel_unit.Controls.Add(this.label_unit);
            resources.ApplyResources(this.panel_unit, "panel_unit");
            this.panel_unit.Name = "panel_unit";
            // 
            // label_unit
            // 
            resources.ApplyResources(this.label_unit, "label_unit");
            this.label_unit.Name = "label_unit";
            // 
            // label_title
            // 
            resources.ApplyResources(this.label_title, "label_title");
            this.label_title.Name = "label_title";
            // 
            // ucDadaTextBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_textbox);
            this.Controls.Add(this.panel_unit);
            this.Controls.Add(this.panel_title);
            this.DoubleBuffered = true;
            this.Name = "ucDadaTextBox";
            this.Resize += new System.EventHandler(this.ucDadaTextBox_Resize);
            this.panel_title.ResumeLayout(false);
            this.panel_textbox.ResumeLayout(false);
            this.panel_textbox.PerformLayout();
            this.panel_unit.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel_title;
		private System.Windows.Forms.Label label_title;
		private System.Windows.Forms.Panel panel_textbox;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Panel panel_unit;
		private System.Windows.Forms.Label label_unit;
	}
}
