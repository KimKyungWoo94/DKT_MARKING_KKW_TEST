namespace EzIna.GUI.UserControls
{
    partial class ucFlicker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFlicker));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label_Image = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Error.png");
            this.imageList.Images.SetKeyName(1, "Alarm.png");
            this.imageList.Images.SetKeyName(2, "Question.png");
            // 
            // label_Image
            // 
            this.label_Image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Image.ImageList = this.imageList;
            this.label_Image.Location = new System.Drawing.Point(0, 0);
            this.label_Image.Name = "label_Image";
            this.label_Image.Size = new System.Drawing.Size(102, 84);
            this.label_Image.TabIndex = 0;
            // 
            // ucDadaFlicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Image);
            this.Name = "ucDadaFlicker";
            this.Size = new System.Drawing.Size(102, 84);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label_Image;
    }
}
