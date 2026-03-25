namespace EzIna
{
  partial class FrmInforOperationSemi_SXGA
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
      this.ucRoundedPanel3 = new EzIna.GUI.UserControls.ucRoundedPanel();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // ucRoundedPanel3
      // 
      this.ucRoundedPanel3.BorderRadius = 32;
      this.ucRoundedPanel3.BorderSize = 1;
      this.ucRoundedPanel3.Caption = "Configuration Lists";
      this.ucRoundedPanel3.clBegin = System.Drawing.Color.CornflowerBlue;
      this.ucRoundedPanel3.clBorder = System.Drawing.Color.AliceBlue;
      this.ucRoundedPanel3.clEnd = System.Drawing.Color.RoyalBlue;
      this.ucRoundedPanel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ucRoundedPanel3.ForeColor = System.Drawing.Color.AliceBlue;
      this.ucRoundedPanel3.Location = new System.Drawing.Point(12, 12);
      this.ucRoundedPanel3.Name = "ucRoundedPanel3";
      this.ucRoundedPanel3.Size = new System.Drawing.Size(217, 32);
      this.ucRoundedPanel3.TabIndex = 12;
      this.ucRoundedPanel3.TextAlignHorizontal = System.Drawing.StringAlignment.Center;
      this.ucRoundedPanel3.TextAlignVertical = System.Drawing.StringAlignment.Center;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(122, 251);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(130, 35);
      this.button1.TabIndex = 14;
      this.button1.Text = "MES TEST DB PROC";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.btn_DM_Marking_INSP_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(122, 302);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(130, 35);
      this.button2.TabIndex = 14;
      this.button2.Text = "DM Marking + INSP";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.btn_DM_Marking_And_INSP_Click);
      // 
      // FrmInforOperationSemi_SXGA
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1246, 869);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.ucRoundedPanel3);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "FrmInforOperationSemi_SXGA";
      this.Tag = "FORM_ID_INFOR_OPERATION_SEMI";
      this.Text = "FrmInforOperationSemi_SXGA";
      this.ResumeLayout(false);

    }

    #endregion

    private GUI.UserControls.ucRoundedPanel ucRoundedPanel3;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
  }
}