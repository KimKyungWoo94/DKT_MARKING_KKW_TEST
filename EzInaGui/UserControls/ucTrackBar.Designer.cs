namespace EzIna.GUI.UserControls
{
	partial class ucTrackBar
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
            this.label_TrackbarValue = new System.Windows.Forms.Label();
            this.label_Unit = new System.Windows.Forms.Label();
            this.label_CurrValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_TrackbarValue
            // 
            this.label_TrackbarValue.BackColor = System.Drawing.SystemColors.Control;
            this.label_TrackbarValue.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            this.label_TrackbarValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TrackbarValue.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TrackbarValue.ForeColor = System.Drawing.Color.Black;
            this.label_TrackbarValue.Location = new System.Drawing.Point(60, 0);
            this.label_TrackbarValue.MinimumSize = new System.Drawing.Size(20, 20);
            this.label_TrackbarValue.Name = "label_TrackbarValue";
            this.label_TrackbarValue.Size = new System.Drawing.Size(232, 50);
            this.label_TrackbarValue.TabIndex = 41;
            this.label_TrackbarValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Unit
            // 
            this.label_Unit.BackColor = System.Drawing.SystemColors.Control;
            this.label_Unit.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_Unit.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Unit.ForeColor = System.Drawing.Color.Black;
            this.label_Unit.Location = new System.Drawing.Point(292, 0);
            this.label_Unit.MinimumSize = new System.Drawing.Size(20, 20);
            this.label_Unit.Name = "label_Unit";
            this.label_Unit.Size = new System.Drawing.Size(60, 50);
            this.label_Unit.TabIndex = 41;
            this.label_Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_CurrValue
            // 
            this.label_CurrValue.BackColor = System.Drawing.SystemColors.Control;
            this.label_CurrValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_CurrValue.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CurrValue.ForeColor = System.Drawing.Color.Black;
            this.label_CurrValue.Location = new System.Drawing.Point(0, 0);
            this.label_CurrValue.MinimumSize = new System.Drawing.Size(20, 20);
            this.label_CurrValue.Name = "label_CurrValue";
            this.label_CurrValue.Size = new System.Drawing.Size(60, 50);
            this.label_CurrValue.TabIndex = 41;
            this.label_CurrValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucDadaTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label_TrackbarValue);
            this.Controls.Add(this.label_CurrValue);
            this.Controls.Add(this.label_Unit);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucDadaTrackBar";
            this.Size = new System.Drawing.Size(352, 50);
            this.VisibleChanged += new System.EventHandler(this.ucDadaTrackBar_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucDadaTrackBar_Paint);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label_TrackbarValue;
		private System.Windows.Forms.Label label_Unit;
		private System.Windows.Forms.Label label_CurrValue;
	}
}
