namespace EzIna.GUI.UserControls
{
	partial class ucTabControlX
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
			this.BackTopPanel = new System.Windows.Forms.Panel();
			this.TabButtonPanel = new System.Windows.Forms.Panel();
			this.RibbonPanel = new System.Windows.Forms.Panel();
			this.TabPanel = new System.Windows.Forms.Panel();
			this.BackTopPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// BackTopPanel
			// 
			this.BackTopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.BackTopPanel.Controls.Add(this.TabButtonPanel);
			this.BackTopPanel.Controls.Add(this.RibbonPanel);
			this.BackTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.BackTopPanel.Location = new System.Drawing.Point(0, 0);
			this.BackTopPanel.Name = "BackTopPanel";
			this.BackTopPanel.Size = new System.Drawing.Size(534, 50);
			this.BackTopPanel.TabIndex = 0;
			// 
			// TabButtonPanel
			// 
			this.TabButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TabButtonPanel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TabButtonPanel.Location = new System.Drawing.Point(0, 0);
			this.TabButtonPanel.Name = "TabButtonPanel";
			this.TabButtonPanel.Size = new System.Drawing.Size(534, 34);
			this.TabButtonPanel.TabIndex = 1;
			// 
			// RibbonPanel
			// 
			this.RibbonPanel.BackColor = System.Drawing.Color.Red;
			this.RibbonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.RibbonPanel.Location = new System.Drawing.Point(0, 48);
			this.RibbonPanel.Name = "RibbonPanel";
			this.RibbonPanel.Size = new System.Drawing.Size(534, 2);
			this.RibbonPanel.TabIndex = 0;
			// 
			// TabPanel
			// 
			this.TabPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.TabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabPanel.Location = new System.Drawing.Point(0, 50);
			this.TabPanel.Name = "TabPanel";
			this.TabPanel.Size = new System.Drawing.Size(534, 238);
			this.TabPanel.TabIndex = 1;
			// 
			// ucTabControlX
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TabPanel);
			this.Controls.Add(this.BackTopPanel);
			this.Name = "ucTabControlX";
			this.Size = new System.Drawing.Size(534, 288);
			this.BackTopPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel BackTopPanel;
		private System.Windows.Forms.Panel TabButtonPanel;
		private System.Windows.Forms.Panel RibbonPanel;
		private System.Windows.Forms.Panel TabPanel;
	}
}
