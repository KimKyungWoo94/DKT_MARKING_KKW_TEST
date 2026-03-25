namespace EzIna.Motion.AXT.GUI
{
    partial class CMotionAXT_Config_GUI
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
            this.dBufferTableLayoutPanel3 = new DBufferTableLayoutPanel(this.components);
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.edtDecel = new NumbericTextBox(this.components);
            this.label33 = new System.Windows.Forms.Label();
            this.edtAccel = new NumbericTextBox(this.components);
            this.edtVelocity = new NumbericTextBox(this.components);
            this.edtPosition = new NumbericTextBox(this.components);
            this.dBufferTableLayoutPanel2 = new DBufferTableLayoutPanel(this.components);
            this.label29 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.edtSWLimit_PosP = new SizeableTextBox(this.components);
            this.label98 = new System.Windows.Forms.Label();
            this.edtSWLimit_PosN = new SizeableTextBox(this.components);
            this.cboSWLimit_Selection = new System.Windows.Forms.ComboBox();
            this.cboSWLimit_StopMode = new System.Windows.Forms.ComboBox();
            this.cboSWLimit_Use = new System.Windows.Forms.ComboBox();
            this.dBufferTableLayoutPanel1 = new DBufferTableLayoutPanel(this.components);
            this.cboSignal_ELimitN = new System.Windows.Forms.ComboBox();
            this.label97 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cboSignal_EncoderType = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cboSignal_StopLevel = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cboSignal_StopMode = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.cboSignal_INP = new System.Windows.Forms.ComboBox();
            this.label95 = new System.Windows.Forms.Label();
            this.cboSignal_Alarm = new System.Windows.Forms.ComboBox();
            this.cboSignal_ZPhaseLEV = new System.Windows.Forms.ComboBox();
            this.cboSignal_ELimitP = new System.Windows.Forms.ComboBox();
            this.cboSignal_ServoOn = new System.Windows.Forms.ComboBox();
            this.cboSignal_AlarmReset = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new DBufferTableLayoutPanel(this.components);
            this.cboParam_Profile = new System.Windows.Forms.ComboBox();
            this.edtParam_MoveUnit = new SizeableTextBox(this.components);
            this.label93 = new System.Windows.Forms.Label();
            this.cboParam_AbsRel = new System.Windows.Forms.ComboBox();
            this.edtParam_MovePulse = new SizeableTextBox(this.components);
            this.label91 = new System.Windows.Forms.Label();
            this.edtParam_MaxVel = new SizeableTextBox(this.components);
            this.label89 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.edtParam_MinVel = new SizeableTextBox(this.components);
            this.label85 = new System.Windows.Forms.Label();
            this.cboParam_Encoder = new System.Windows.Forms.ComboBox();
            this.label83 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cbParam_PulseOut = new System.Windows.Forms.ComboBox();
            this.dBufferTableLayoutPanel3.SuspendLayout();
            this.dBufferTableLayoutPanel2.SuspendLayout();
            this.dBufferTableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dBufferTableLayoutPanel3
            // 
            this.dBufferTableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.dBufferTableLayoutPanel3.ColumnCount = 5;
            this.dBufferTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.dBufferTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.dBufferTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.dBufferTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.dBufferTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dBufferTableLayoutPanel3.Controls.Add(this.label27, 2, 2);
            this.dBufferTableLayoutPanel3.Controls.Add(this.label28, 2, 1);
            this.dBufferTableLayoutPanel3.Controls.Add(this.label30, 0, 2);
            this.dBufferTableLayoutPanel3.Controls.Add(this.label31, 0, 0);
            this.dBufferTableLayoutPanel3.Controls.Add(this.edtDecel, 3, 2);
            this.dBufferTableLayoutPanel3.Controls.Add(this.label33, 0, 1);
            this.dBufferTableLayoutPanel3.Controls.Add(this.edtAccel, 3, 1);
            this.dBufferTableLayoutPanel3.Controls.Add(this.edtVelocity, 1, 2);
            this.dBufferTableLayoutPanel3.Controls.Add(this.edtPosition, 1, 1);
            this.dBufferTableLayoutPanel3.Location = new System.Drawing.Point(1, 449);
            this.dBufferTableLayoutPanel3.Name = "dBufferTableLayoutPanel3";
            this.dBufferTableLayoutPanel3.RowCount = 4;
            this.dBufferTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dBufferTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dBufferTableLayoutPanel3.Size = new System.Drawing.Size(485, 80);
            this.dBufferTableLayoutPanel3.TabIndex = 67;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(264, 54);
            this.label27.Margin = new System.Windows.Forms.Padding(1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(88, 23);
            this.label27.TabIndex = 26;
            this.label27.Text = "DCC";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(264, 28);
            this.label28.Margin = new System.Windows.Forms.Padding(1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(88, 23);
            this.label28.TabIndex = 25;
            this.label28.Text = "ACC";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(2, 54);
            this.label30.Margin = new System.Windows.Forms.Padding(1);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(88, 23);
            this.label30.TabIndex = 23;
            this.label30.Text = "VELOCITY";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.dBufferTableLayoutPanel3.SetColumnSpan(this.label31, 4);
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(2, 2);
            this.label31.Margin = new System.Windows.Forms.Padding(1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(481, 23);
            this.label31.TabIndex = 0;
            this.label31.Text = "< User Move Parameter Setting >";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edtDecel
            // 
            this.edtDecel.BackColor = System.Drawing.SystemColors.Control;
            this.edtDecel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtDecel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtDecel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtDecel.ForeColor = System.Drawing.Color.Black;
            this.edtDecel.Location = new System.Drawing.Point(354, 53);
            this.edtDecel.Margin = new System.Windows.Forms.Padding(0);
            this.edtDecel.Name = "edtDecel";
            this.edtDecel.Size = new System.Drawing.Size(130, 25);
            this.edtDecel.TabIndex = 32;
            this.edtDecel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(2, 28);
            this.label33.Margin = new System.Windows.Forms.Padding(1);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(88, 23);
            this.label33.TabIndex = 22;
            this.label33.Text = "POSITION";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // edtAccel
            // 
            this.edtAccel.BackColor = System.Drawing.SystemColors.Control;
            this.edtAccel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtAccel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtAccel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtAccel.ForeColor = System.Drawing.Color.Black;
            this.edtAccel.Location = new System.Drawing.Point(354, 27);
            this.edtAccel.Margin = new System.Windows.Forms.Padding(0);
            this.edtAccel.Name = "edtAccel";
            this.edtAccel.Size = new System.Drawing.Size(130, 25);
            this.edtAccel.TabIndex = 31;
            this.edtAccel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // edtVelocity
            // 
            this.edtVelocity.BackColor = System.Drawing.SystemColors.Control;
            this.edtVelocity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtVelocity.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtVelocity.ForeColor = System.Drawing.Color.Black;
            this.edtVelocity.Location = new System.Drawing.Point(92, 53);
            this.edtVelocity.Margin = new System.Windows.Forms.Padding(0);
            this.edtVelocity.Name = "edtVelocity";
            this.edtVelocity.Size = new System.Drawing.Size(170, 25);
            this.edtVelocity.TabIndex = 30;
            this.edtVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // edtPosition
            // 
            this.edtPosition.BackColor = System.Drawing.SystemColors.Control;
            this.edtPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtPosition.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtPosition.ForeColor = System.Drawing.Color.Black;
            this.edtPosition.Location = new System.Drawing.Point(92, 27);
            this.edtPosition.Margin = new System.Windows.Forms.Padding(0);
            this.edtPosition.Name = "edtPosition";
            this.edtPosition.Size = new System.Drawing.Size(170, 25);
            this.edtPosition.TabIndex = 29;
            this.edtPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dBufferTableLayoutPanel2
            // 
            this.dBufferTableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.dBufferTableLayoutPanel2.ColumnCount = 5;
            this.dBufferTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.dBufferTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.dBufferTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.dBufferTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.dBufferTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.dBufferTableLayoutPanel2.Controls.Add(this.label29, 0, 3);
            this.dBufferTableLayoutPanel2.Controls.Add(this.label32, 2, 2);
            this.dBufferTableLayoutPanel2.Controls.Add(this.label35, 2, 1);
            this.dBufferTableLayoutPanel2.Controls.Add(this.label36, 0, 2);
            this.dBufferTableLayoutPanel2.Controls.Add(this.label37, 0, 0);
            this.dBufferTableLayoutPanel2.Controls.Add(this.edtSWLimit_PosP, 3, 2);
            this.dBufferTableLayoutPanel2.Controls.Add(this.label98, 0, 1);
            this.dBufferTableLayoutPanel2.Controls.Add(this.edtSWLimit_PosN, 3, 1);
            this.dBufferTableLayoutPanel2.Controls.Add(this.cboSWLimit_Selection, 1, 3);
            this.dBufferTableLayoutPanel2.Controls.Add(this.cboSWLimit_StopMode, 1, 2);
            this.dBufferTableLayoutPanel2.Controls.Add(this.cboSWLimit_Use, 1, 1);
            this.dBufferTableLayoutPanel2.Location = new System.Drawing.Point(1, 334);
            this.dBufferTableLayoutPanel2.Name = "dBufferTableLayoutPanel2";
            this.dBufferTableLayoutPanel2.RowCount = 5;
            this.dBufferTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.dBufferTableLayoutPanel2.Size = new System.Drawing.Size(485, 106);
            this.dBufferTableLayoutPanel2.TabIndex = 66;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(2, 80);
            this.label29.Margin = new System.Windows.Forms.Padding(1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(88, 23);
            this.label29.TabIndex = 29;
            this.label29.Text = "SELECTION";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(264, 54);
            this.label32.Margin = new System.Windows.Forms.Padding(1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(88, 23);
            this.label32.TabIndex = 26;
            this.label32.Text = "+ SW LIMIT";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(264, 28);
            this.label35.Margin = new System.Windows.Forms.Padding(1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(88, 23);
            this.label35.TabIndex = 25;
            this.label35.Text = "- SW LIMIT";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(2, 54);
            this.label36.Margin = new System.Windows.Forms.Padding(1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(88, 23);
            this.label36.TabIndex = 23;
            this.label36.Text = "STOP MODE";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.dBufferTableLayoutPanel2.SetColumnSpan(this.label37, 4);
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(2, 2);
            this.label37.Margin = new System.Windows.Forms.Padding(1);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(481, 23);
            this.label37.TabIndex = 0;
            this.label37.Text = "< Software Limit Setting >";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edtSWLimit_PosP
            // 
            this.edtSWLimit_PosP.BackColor = System.Drawing.SystemColors.Control;
            this.edtSWLimit_PosP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtSWLimit_PosP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtSWLimit_PosP.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtSWLimit_PosP.ForeColor = System.Drawing.Color.Black;
            this.edtSWLimit_PosP.Location = new System.Drawing.Point(354, 53);
            this.edtSWLimit_PosP.Margin = new System.Windows.Forms.Padding(0);
            this.edtSWLimit_PosP.Name = "edtSWLimit_PosP";
            this.edtSWLimit_PosP.Size = new System.Drawing.Size(130, 25);
            this.edtSWLimit_PosP.TabIndex = 26;
            this.edtSWLimit_PosP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label98.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(2, 28);
            this.label98.Margin = new System.Windows.Forms.Padding(1);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(88, 23);
            this.label98.TabIndex = 22;
            this.label98.Text = "USE";
            this.label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // edtSWLimit_PosN
            // 
            this.edtSWLimit_PosN.BackColor = System.Drawing.SystemColors.Control;
            this.edtSWLimit_PosN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtSWLimit_PosN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtSWLimit_PosN.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtSWLimit_PosN.ForeColor = System.Drawing.Color.Black;
            this.edtSWLimit_PosN.Location = new System.Drawing.Point(354, 27);
            this.edtSWLimit_PosN.Margin = new System.Windows.Forms.Padding(0);
            this.edtSWLimit_PosN.Name = "edtSWLimit_PosN";
            this.edtSWLimit_PosN.Size = new System.Drawing.Size(130, 25);
            this.edtSWLimit_PosN.TabIndex = 25;
            this.edtSWLimit_PosN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboSWLimit_Selection
            // 
            this.cboSWLimit_Selection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSWLimit_Selection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSWLimit_Selection.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSWLimit_Selection.ForeColor = System.Drawing.Color.Black;
            this.cboSWLimit_Selection.FormattingEnabled = true;
            this.cboSWLimit_Selection.Location = new System.Drawing.Point(92, 79);
            this.cboSWLimit_Selection.Margin = new System.Windows.Forms.Padding(0);
            this.cboSWLimit_Selection.Name = "cboSWLimit_Selection";
            this.cboSWLimit_Selection.Size = new System.Drawing.Size(170, 25);
            this.cboSWLimit_Selection.TabIndex = 7;
            // 
            // cboSWLimit_StopMode
            // 
            this.cboSWLimit_StopMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSWLimit_StopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSWLimit_StopMode.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSWLimit_StopMode.ForeColor = System.Drawing.Color.Black;
            this.cboSWLimit_StopMode.FormattingEnabled = true;
            this.cboSWLimit_StopMode.Location = new System.Drawing.Point(92, 53);
            this.cboSWLimit_StopMode.Margin = new System.Windows.Forms.Padding(0);
            this.cboSWLimit_StopMode.Name = "cboSWLimit_StopMode";
            this.cboSWLimit_StopMode.Size = new System.Drawing.Size(170, 25);
            this.cboSWLimit_StopMode.TabIndex = 7;
            // 
            // cboSWLimit_Use
            // 
            this.cboSWLimit_Use.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSWLimit_Use.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSWLimit_Use.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSWLimit_Use.ForeColor = System.Drawing.Color.Black;
            this.cboSWLimit_Use.FormattingEnabled = true;
            this.cboSWLimit_Use.Location = new System.Drawing.Point(92, 27);
            this.cboSWLimit_Use.Margin = new System.Windows.Forms.Padding(0);
            this.cboSWLimit_Use.Name = "cboSWLimit_Use";
            this.cboSWLimit_Use.Size = new System.Drawing.Size(170, 25);
            this.cboSWLimit_Use.TabIndex = 7;
            // 
            // dBufferTableLayoutPanel1
            // 
            this.dBufferTableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.dBufferTableLayoutPanel1.ColumnCount = 5;
            this.dBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.dBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.dBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.dBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.dBufferTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_ELimitN, 1, 3);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label97, 2, 5);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label96, 0, 5);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label18, 0, 4);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label19, 0, 3);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_EncoderType, 3, 5);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label20, 2, 4);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label21, 2, 3);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_StopLevel, 3, 3);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label22, 2, 2);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_StopMode, 3, 2);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label23, 2, 1);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label24, 0, 2);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label84, 0, 0);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_INP, 1, 1);
            this.dBufferTableLayoutPanel1.Controls.Add(this.label95, 0, 1);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_Alarm, 1, 2);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_ZPhaseLEV, 3, 1);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_ELimitP, 1, 4);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_ServoOn, 1, 5);
            this.dBufferTableLayoutPanel1.Controls.Add(this.cboSignal_AlarmReset, 3, 4);
            this.dBufferTableLayoutPanel1.Location = new System.Drawing.Point(3, 155);
            this.dBufferTableLayoutPanel1.Name = "dBufferTableLayoutPanel1";
            this.dBufferTableLayoutPanel1.RowCount = 7;
            this.dBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.dBufferTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.dBufferTableLayoutPanel1.Size = new System.Drawing.Size(485, 157);
            this.dBufferTableLayoutPanel1.TabIndex = 65;
            // 
            // cboSignal_ELimitN
            // 
            this.cboSignal_ELimitN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_ELimitN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_ELimitN.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_ELimitN.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_ELimitN.FormattingEnabled = true;
            this.cboSignal_ELimitN.Location = new System.Drawing.Point(92, 79);
            this.cboSignal_ELimitN.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_ELimitN.Name = "cboSignal_ELimitN";
            this.cboSignal_ELimitN.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_ELimitN.TabIndex = 6;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label97.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.Location = new System.Drawing.Point(244, 132);
            this.label97.Margin = new System.Windows.Forms.Padding(1);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(88, 23);
            this.label97.TabIndex = 32;
            this.label97.Text = "ENC TYPE";
            this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label96.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.Location = new System.Drawing.Point(2, 132);
            this.label96.Margin = new System.Windows.Forms.Padding(1);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(88, 23);
            this.label96.TabIndex = 31;
            this.label96.Text = "SERVO ON";
            this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(2, 106);
            this.label18.Margin = new System.Windows.Forms.Padding(1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 23);
            this.label18.TabIndex = 30;
            this.label18.Text = "+LIMIT";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(2, 80);
            this.label19.Margin = new System.Windows.Forms.Padding(1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 23);
            this.label19.TabIndex = 29;
            this.label19.Text = "-LIMIT";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSignal_EncoderType
            // 
            this.cboSignal_EncoderType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_EncoderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_EncoderType.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_EncoderType.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_EncoderType.FormattingEnabled = true;
            this.cboSignal_EncoderType.Location = new System.Drawing.Point(334, 131);
            this.cboSignal_EncoderType.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_EncoderType.Name = "cboSignal_EncoderType";
            this.cboSignal_EncoderType.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_EncoderType.TabIndex = 16;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(244, 106);
            this.label20.Margin = new System.Windows.Forms.Padding(1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(88, 23);
            this.label20.TabIndex = 28;
            this.label20.Text = "ALARM RESET";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(244, 80);
            this.label21.Margin = new System.Windows.Forms.Padding(1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(88, 23);
            this.label21.TabIndex = 27;
            this.label21.Text = "EMS LEVEL";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSignal_StopLevel
            // 
            this.cboSignal_StopLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_StopLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_StopLevel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_StopLevel.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_StopLevel.FormattingEnabled = true;
            this.cboSignal_StopLevel.Location = new System.Drawing.Point(334, 79);
            this.cboSignal_StopLevel.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_StopLevel.Name = "cboSignal_StopLevel";
            this.cboSignal_StopLevel.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_StopLevel.TabIndex = 16;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(244, 54);
            this.label22.Margin = new System.Windows.Forms.Padding(1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 23);
            this.label22.TabIndex = 26;
            this.label22.Text = "EMS MODE";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSignal_StopMode
            // 
            this.cboSignal_StopMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_StopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_StopMode.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_StopMode.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_StopMode.FormattingEnabled = true;
            this.cboSignal_StopMode.Location = new System.Drawing.Point(334, 53);
            this.cboSignal_StopMode.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_StopMode.Name = "cboSignal_StopMode";
            this.cboSignal_StopMode.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_StopMode.TabIndex = 22;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(244, 28);
            this.label23.Margin = new System.Windows.Forms.Padding(1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(88, 23);
            this.label23.TabIndex = 25;
            this.label23.Text = "Z-PHASE";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(2, 54);
            this.label24.Margin = new System.Windows.Forms.Padding(1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 23);
            this.label24.TabIndex = 23;
            this.label24.Text = "ALARM";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.dBufferTableLayoutPanel1.SetColumnSpan(this.label84, 4);
            this.label84.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label84.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(2, 2);
            this.label84.Margin = new System.Windows.Forms.Padding(1);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(481, 23);
            this.label84.TabIndex = 0;
            this.label84.Text = "< Input/Output Signal Setting >";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSignal_INP
            // 
            this.cboSignal_INP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_INP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_INP.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_INP.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_INP.FormattingEnabled = true;
            this.cboSignal_INP.Location = new System.Drawing.Point(92, 27);
            this.cboSignal_INP.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_INP.Name = "cboSignal_INP";
            this.cboSignal_INP.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_INP.TabIndex = 4;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label95.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.Location = new System.Drawing.Point(2, 28);
            this.label95.Margin = new System.Windows.Forms.Padding(1);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(88, 23);
            this.label95.TabIndex = 22;
            this.label95.Text = "IN-POS";
            this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSignal_Alarm
            // 
            this.cboSignal_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_Alarm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_Alarm.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_Alarm.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_Alarm.FormattingEnabled = true;
            this.cboSignal_Alarm.Location = new System.Drawing.Point(92, 53);
            this.cboSignal_Alarm.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_Alarm.Name = "cboSignal_Alarm";
            this.cboSignal_Alarm.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_Alarm.TabIndex = 5;
            // 
            // cboSignal_ZPhaseLEV
            // 
            this.cboSignal_ZPhaseLEV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_ZPhaseLEV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_ZPhaseLEV.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_ZPhaseLEV.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_ZPhaseLEV.FormattingEnabled = true;
            this.cboSignal_ZPhaseLEV.Location = new System.Drawing.Point(334, 27);
            this.cboSignal_ZPhaseLEV.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_ZPhaseLEV.Name = "cboSignal_ZPhaseLEV";
            this.cboSignal_ZPhaseLEV.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_ZPhaseLEV.TabIndex = 13;
            // 
            // cboSignal_ELimitP
            // 
            this.cboSignal_ELimitP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_ELimitP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_ELimitP.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_ELimitP.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_ELimitP.FormattingEnabled = true;
            this.cboSignal_ELimitP.Location = new System.Drawing.Point(92, 105);
            this.cboSignal_ELimitP.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_ELimitP.Name = "cboSignal_ELimitP";
            this.cboSignal_ELimitP.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_ELimitP.TabIndex = 7;
            // 
            // cboSignal_ServoOn
            // 
            this.cboSignal_ServoOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_ServoOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_ServoOn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_ServoOn.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_ServoOn.FormattingEnabled = true;
            this.cboSignal_ServoOn.Location = new System.Drawing.Point(92, 131);
            this.cboSignal_ServoOn.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_ServoOn.Name = "cboSignal_ServoOn";
            this.cboSignal_ServoOn.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_ServoOn.TabIndex = 7;
            // 
            // cboSignal_AlarmReset
            // 
            this.cboSignal_AlarmReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSignal_AlarmReset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignal_AlarmReset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignal_AlarmReset.ForeColor = System.Drawing.Color.Black;
            this.cboSignal_AlarmReset.FormattingEnabled = true;
            this.cboSignal_AlarmReset.Location = new System.Drawing.Point(334, 105);
            this.cboSignal_AlarmReset.Margin = new System.Windows.Forms.Padding(0);
            this.cboSignal_AlarmReset.Name = "cboSignal_AlarmReset";
            this.cboSignal_AlarmReset.Size = new System.Drawing.Size(150, 25);
            this.cboSignal_AlarmReset.TabIndex = 16;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.Controls.Add(this.cboParam_Profile, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.edtParam_MoveUnit, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label93, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboParam_AbsRel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.edtParam_MovePulse, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label91, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.edtParam_MaxVel, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label89, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label87, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label86, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.edtParam_MinVel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label85, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboParam_Encoder, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label83, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label82, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbParam_PulseOut, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 132);
            this.tableLayoutPanel1.TabIndex = 64;
            // 
            // cboParam_Profile
            // 
            this.cboParam_Profile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboParam_Profile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParam_Profile.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboParam_Profile.FormattingEnabled = true;
            this.cboParam_Profile.ItemHeight = 17;
            this.cboParam_Profile.Location = new System.Drawing.Point(92, 105);
            this.cboParam_Profile.Margin = new System.Windows.Forms.Padding(0);
            this.cboParam_Profile.Name = "cboParam_Profile";
            this.cboParam_Profile.Size = new System.Drawing.Size(170, 25);
            this.cboParam_Profile.TabIndex = 20;
            // 
            // edtParam_MoveUnit
            // 
            this.edtParam_MoveUnit.BackColor = System.Drawing.SystemColors.Control;
            this.edtParam_MoveUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtParam_MoveUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtParam_MoveUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtParam_MoveUnit.Location = new System.Drawing.Point(354, 105);
            this.edtParam_MoveUnit.Margin = new System.Windows.Forms.Padding(0);
            this.edtParam_MoveUnit.Name = "edtParam_MoveUnit";
            this.edtParam_MoveUnit.Size = new System.Drawing.Size(130, 25);
            this.edtParam_MoveUnit.TabIndex = 28;
            this.edtParam_MoveUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label93.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(2, 106);
            this.label93.Margin = new System.Windows.Forms.Padding(1);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(88, 23);
            this.label93.TabIndex = 30;
            this.label93.Text = "Profile";
            this.label93.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboParam_AbsRel
            // 
            this.cboParam_AbsRel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboParam_AbsRel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParam_AbsRel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboParam_AbsRel.FormattingEnabled = true;
            this.cboParam_AbsRel.ItemHeight = 17;
            this.cboParam_AbsRel.Location = new System.Drawing.Point(92, 79);
            this.cboParam_AbsRel.Margin = new System.Windows.Forms.Padding(0);
            this.cboParam_AbsRel.Name = "cboParam_AbsRel";
            this.cboParam_AbsRel.Size = new System.Drawing.Size(170, 25);
            this.cboParam_AbsRel.TabIndex = 20;
            // 
            // edtParam_MovePulse
            // 
            this.edtParam_MovePulse.BackColor = System.Drawing.SystemColors.Control;
            this.edtParam_MovePulse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtParam_MovePulse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtParam_MovePulse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtParam_MovePulse.Location = new System.Drawing.Point(354, 79);
            this.edtParam_MovePulse.Margin = new System.Windows.Forms.Padding(0);
            this.edtParam_MovePulse.Name = "edtParam_MovePulse";
            this.edtParam_MovePulse.Size = new System.Drawing.Size(130, 25);
            this.edtParam_MovePulse.TabIndex = 27;
            this.edtParam_MovePulse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label91.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(2, 80);
            this.label91.Margin = new System.Windows.Forms.Padding(1);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(88, 23);
            this.label91.TabIndex = 29;
            this.label91.Text = "ABS/REL";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // edtParam_MaxVel
            // 
            this.edtParam_MaxVel.BackColor = System.Drawing.SystemColors.Control;
            this.edtParam_MaxVel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtParam_MaxVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtParam_MaxVel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtParam_MaxVel.Location = new System.Drawing.Point(354, 53);
            this.edtParam_MaxVel.Margin = new System.Windows.Forms.Padding(0);
            this.edtParam_MaxVel.Name = "edtParam_MaxVel";
            this.edtParam_MaxVel.Size = new System.Drawing.Size(130, 25);
            this.edtParam_MaxVel.TabIndex = 9;
            this.edtParam_MaxVel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label89.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(264, 106);
            this.label89.Margin = new System.Windows.Forms.Padding(1);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(88, 23);
            this.label89.TabIndex = 28;
            this.label89.Text = "Move Unit";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label87.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(264, 80);
            this.label87.Margin = new System.Windows.Forms.Padding(1);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(88, 23);
            this.label87.TabIndex = 27;
            this.label87.Text = "MOVE PULSE";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label86.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(264, 54);
            this.label86.Margin = new System.Windows.Forms.Padding(1);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(88, 23);
            this.label86.TabIndex = 26;
            this.label86.Text = "MAX VEL";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // edtParam_MinVel
            // 
            this.edtParam_MinVel.BackColor = System.Drawing.SystemColors.Control;
            this.edtParam_MinVel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtParam_MinVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtParam_MinVel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtParam_MinVel.Location = new System.Drawing.Point(354, 27);
            this.edtParam_MinVel.Margin = new System.Windows.Forms.Padding(0);
            this.edtParam_MinVel.Name = "edtParam_MinVel";
            this.edtParam_MinVel.Size = new System.Drawing.Size(130, 25);
            this.edtParam_MinVel.TabIndex = 8;
            this.edtParam_MinVel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label85.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(264, 28);
            this.label85.Margin = new System.Windows.Forms.Padding(1);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(88, 23);
            this.label85.TabIndex = 25;
            this.label85.Text = "MIN VEL";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboParam_Encoder
            // 
            this.cboParam_Encoder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboParam_Encoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParam_Encoder.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboParam_Encoder.FormattingEnabled = true;
            this.cboParam_Encoder.ItemHeight = 17;
            this.cboParam_Encoder.Location = new System.Drawing.Point(92, 53);
            this.cboParam_Encoder.Margin = new System.Windows.Forms.Padding(0);
            this.cboParam_Encoder.Name = "cboParam_Encoder";
            this.cboParam_Encoder.Size = new System.Drawing.Size(170, 25);
            this.cboParam_Encoder.TabIndex = 3;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label83.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.Location = new System.Drawing.Point(2, 54);
            this.label83.Margin = new System.Windows.Forms.Padding(1);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(88, 23);
            this.label83.TabIndex = 23;
            this.label83.Text = "ENC IN";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label82, 4);
            this.label82.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label82.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(2, 2);
            this.label82.Margin = new System.Windows.Forms.Padding(1);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(481, 23);
            this.label82.TabIndex = 0;
            this.label82.Text = "< Pulse/Encoder Method && Move Parameter Setting >";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(2, 28);
            this.label25.Margin = new System.Windows.Forms.Padding(1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(88, 23);
            this.label25.TabIndex = 22;
            this.label25.Text = "PULSE OUT";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbParam_PulseOut
            // 
            this.cbParam_PulseOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbParam_PulseOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParam_PulseOut.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbParam_PulseOut.FormattingEnabled = true;
            this.cbParam_PulseOut.ItemHeight = 17;
            this.cbParam_PulseOut.Location = new System.Drawing.Point(92, 27);
            this.cbParam_PulseOut.Margin = new System.Windows.Forms.Padding(0);
            this.cbParam_PulseOut.Name = "cbParam_PulseOut";
            this.cbParam_PulseOut.Size = new System.Drawing.Size(170, 25);
            this.cbParam_PulseOut.TabIndex = 0;
            // 
            // CMotionAXT_Config_GUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dBufferTableLayoutPanel3);
            this.Controls.Add(this.dBufferTableLayoutPanel2);
            this.Controls.Add(this.dBufferTableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "CMotionAXT_Config_GUI";
            this.Size = new System.Drawing.Size(528, 571);
            this.dBufferTableLayoutPanel3.ResumeLayout(false);
            this.dBufferTableLayoutPanel3.PerformLayout();
            this.dBufferTableLayoutPanel2.ResumeLayout(false);
            this.dBufferTableLayoutPanel2.PerformLayout();
            this.dBufferTableLayoutPanel1.ResumeLayout(false);
            this.dBufferTableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DBufferTableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cboParam_Profile;
        private SizeableTextBox edtParam_MoveUnit;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.ComboBox cboParam_AbsRel;
        private SizeableTextBox edtParam_MovePulse;
        private System.Windows.Forms.Label label91;
        private SizeableTextBox edtParam_MaxVel;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label86;
        private SizeableTextBox edtParam_MinVel;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.ComboBox cboParam_Encoder;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbParam_PulseOut;
        private DBufferTableLayoutPanel dBufferTableLayoutPanel1;
        private System.Windows.Forms.ComboBox cboSignal_ELimitN;
        private System.Windows.Forms.ComboBox cboSignal_StopLevel;
        private System.Windows.Forms.ComboBox cboSignal_EncoderType;
        private System.Windows.Forms.ComboBox cboSignal_StopMode;
        private System.Windows.Forms.ComboBox cboSignal_Alarm;
        private System.Windows.Forms.ComboBox cboSignal_ZPhaseLEV;
        private System.Windows.Forms.ComboBox cboSignal_ELimitP;
        private System.Windows.Forms.ComboBox cboSignal_ServoOn;
        private System.Windows.Forms.ComboBox cboSignal_AlarmReset;
        private System.Windows.Forms.ComboBox cboSignal_INP;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;

        private System.Windows.Forms.Label label22;

        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label84;

        private System.Windows.Forms.Label label95;

        private DBufferTableLayoutPanel dBufferTableLayoutPanel2;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label98;
        private SizeableTextBox edtSWLimit_PosP;
        private SizeableTextBox edtSWLimit_PosN;
        private System.Windows.Forms.ComboBox cboSWLimit_Selection;
        private System.Windows.Forms.ComboBox cboSWLimit_StopMode;
        private System.Windows.Forms.ComboBox cboSWLimit_Use;
        private DBufferTableLayoutPanel dBufferTableLayoutPanel3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;

        private System.Windows.Forms.Label label33;
        private NumbericTextBox edtDecel;
        private NumbericTextBox edtAccel;
        private NumbericTextBox edtVelocity;
        private NumbericTextBox edtPosition;
    }
}
