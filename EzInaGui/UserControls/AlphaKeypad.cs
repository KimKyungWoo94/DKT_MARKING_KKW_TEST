using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
    public partial class AlphaKeypad : Form
    {
        private readonly uint DEFAULT_MAXLENGTH = 25;
        private readonly bool DEFAULT_ASTERISK = false;

        uint m_uMaxLength;

        /// <summary>
        /// * 표시
        /// </summary>
        public bool Asterisk { get; set; }

        /// <summary>
        /// 기존 값.
        /// </summary>
        public string OldValue { get; set; }

        ///<summary>
        /// 현재 값.
        ///</summary>
        public string NewValue { get; set; }


        public bool bCapitalMode=false;
        ///<summary>
        /// 입력 최대자리수
        ///</summary>
        public uint MaxLength
        {
            get
            {
                return m_uMaxLength;
            }
            set
            {
                if (value == 0)
                {
                    m_uMaxLength = DEFAULT_MAXLENGTH;
                }
                else
                {
                    m_uMaxLength = value;
                }
            }
        }

        public AlphaKeypad()
        {
            InitializeComponent();
            CreateInputEvent();
            //CreateKeypressEvent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaxLength = DEFAULT_MAXLENGTH;
            this.Asterisk = DEFAULT_ASTERISK;
            this.OldValue = "";
            this.NewValue = "";           
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);


        }


        private void CreateInputEvent()
        {
            btn0.Click += new EventHandler(OnKeyInput);
            btn1.Click += new EventHandler(OnKeyInput);
            btn2.Click += new EventHandler(OnKeyInput);
            btn3.Click += new EventHandler(OnKeyInput);
            btn4.Click += new EventHandler(OnKeyInput);
            btn5.Click += new EventHandler(OnKeyInput);
            btn6.Click += new EventHandler(OnKeyInput);
            btn7.Click += new EventHandler(OnKeyInput);
            btn8.Click += new EventHandler(OnKeyInput);
            btn9.Click += new EventHandler(OnKeyInput);
            btn0.Click += new EventHandler(OnKeyInput);

            btnQ.Click += new EventHandler(OnKeyInput);
            btnW.Click += new EventHandler(OnKeyInput);
            btnE.Click += new EventHandler(OnKeyInput);
            btnR.Click += new EventHandler(OnKeyInput);
            btnT.Click += new EventHandler(OnKeyInput);
            btnY.Click += new EventHandler(OnKeyInput);
            btnU.Click += new EventHandler(OnKeyInput);
            btnI.Click += new EventHandler(OnKeyInput);
            btnO.Click += new EventHandler(OnKeyInput);
            btnP.Click += new EventHandler(OnKeyInput);

            btnA.Click += new EventHandler(OnKeyInput);
            btnS.Click += new EventHandler(OnKeyInput);
            btnD.Click += new EventHandler(OnKeyInput);
            btnF.Click += new EventHandler(OnKeyInput);
            btnG.Click += new EventHandler(OnKeyInput);
            btnH.Click += new EventHandler(OnKeyInput);
            btnJ.Click += new EventHandler(OnKeyInput);
            btnK.Click += new EventHandler(OnKeyInput);
            btnL.Click += new EventHandler(OnKeyInput);

            btnZ.Click += new EventHandler(OnKeyInput);
            btnX.Click += new EventHandler(OnKeyInput);
            btnC.Click += new EventHandler(OnKeyInput);
            btnV.Click += new EventHandler(OnKeyInput);
            btnB.Click += new EventHandler(OnKeyInput);
            btnN.Click += new EventHandler(OnKeyInput);
            btnM.Click += new EventHandler(OnKeyInput);

            btnhyphen.Click += new EventHandler(OnKeyInput);
            btnUnderbar.Click += new EventHandler(OnKeyInput);
            btnComma.Click += new EventHandler(OnKeyInput);
            btnDot.Click += new EventHandler(OnKeyInput);
        }

        private void OnKeyInput(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (NewValue.Length <= MaxLength)
                NewValue += btn.Text;

            lbl_NewData.Text = "";

            if (Asterisk)
            {
                for (int i = 0; i < NewValue.Length; i++)
                    lbl_NewData.Text += "*";
            }
            else
                lbl_NewData.Text = NewValue;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {              
                btnEnter.Focus();
                OnCloseEvent(DialogResult.OK);                
            }
            else if(keyData ==Keys.Escape)
            {
                btnCancel.Focus();
                OnCloseEvent(DialogResult.Cancel);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            string cKey = "";
            //lbl_NewData.Text = "";
            //nexBtnOK.Focus();
            //
            
            bool bCapital= bCapitalMode==true ? (e.Shift==true ? false: true) :(e.Shift==true ? true:false);
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            || (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            || (e.KeyCode == Keys.Shift && e.KeyCode == Keys.OemMinus)
            || (e.KeyCode == Keys.OemMinus) || (e.KeyCode == Keys.OemPeriod) || (e.KeyCode == Keys.Oemcomma)
            || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                bool bCaps= Control.IsKeyLocked(Keys.CapsLock);
                if (e.KeyCode == Keys.Oemcomma) { cKey = ","; btnComma.Focus(); }
                else if (e.Shift && (e.KeyCode == Keys.OemMinus)) { cKey = "_"; btnUnderbar.Focus(); }
                else if (e.KeyCode == Keys.OemMinus) { cKey = "-"; btnhyphen.Focus(); }
                else if (e.KeyCode == Keys.OemPeriod) { cKey = "."; btnDot.Focus(); }
                else if (e.KeyCode == Keys.D1|| e.KeyCode == Keys.NumPad1) { cKey = "1"; btn1.Focus(); }
                else if (e.KeyCode == Keys.D2|| e.KeyCode == Keys.NumPad2) { cKey = "2"; btn2.Focus(); }
                else if (e.KeyCode == Keys.D3|| e.KeyCode == Keys.NumPad3) { cKey = "3"; btn3.Focus(); }
                else if (e.KeyCode == Keys.D4|| e.KeyCode == Keys.NumPad4) { cKey = "4"; btn4.Focus(); }
                else if (e.KeyCode == Keys.D5|| e.KeyCode == Keys.NumPad5) { cKey = "5"; btn5.Focus(); }
                else if (e.KeyCode == Keys.D6|| e.KeyCode == Keys.NumPad6) { cKey = "6"; btn6.Focus(); }
                else if (e.KeyCode == Keys.D7|| e.KeyCode == Keys.NumPad7) { cKey = "7"; btn7.Focus(); }
                else if (e.KeyCode == Keys.D8|| e.KeyCode == Keys.NumPad8) { cKey = "8"; btn8.Focus(); }
                else if (e.KeyCode == Keys.D9|| e.KeyCode == Keys.NumPad9) { cKey = "9"; btn9.Focus(); }
                else if (e.KeyCode == Keys.D0|| e.KeyCode == Keys.NumPad0) { cKey = "0"; btn0.Focus(); }                
                //
                else if (e.KeyCode == Keys.Q) { cKey = bCapital ? "Q" :"q" ; btnQ.Focus(); }
                else if (e.KeyCode == Keys.W) { cKey = bCapital ? "W" :"w" ; ; btnW.Focus(); }
                else if (e.KeyCode == Keys.E) { cKey = bCapital ? "E" :"e" ; btnE.Focus(); }
                else if (e.KeyCode == Keys.R) { cKey = bCapital ? "R" :"r" ; btnR.Focus(); }
                else if (e.KeyCode == Keys.T) { cKey = bCapital ? "T" :"t" ; btnT.Focus(); }
                else if (e.KeyCode == Keys.Y) { cKey = bCapital ? "Y" :"y" ; btnY.Focus(); }
                else if (e.KeyCode == Keys.U) { cKey = bCapital ? "U" :"u" ; btnU.Focus(); }
                else if (e.KeyCode == Keys.I) { cKey = bCapital ? "I" :"i" ; btnI.Focus(); }
                else if (e.KeyCode == Keys.O) { cKey = bCapital ? "O" :"o" ; btnO.Focus(); }
                else if (e.KeyCode == Keys.P) { cKey = bCapital ? "P" :"p" ; btnP.Focus(); }
                //
                else if (e.KeyCode == Keys.A) { cKey = bCapital ?  "A" :"a"; btnA.Focus(); }
                else if (e.KeyCode == Keys.S) { cKey = bCapital ?  "S" :"s"; btnS.Focus(); }
                else if (e.KeyCode == Keys.D) { cKey = bCapital ?  "D" :"d"; btnD.Focus(); }
                else if (e.KeyCode == Keys.F) { cKey = bCapital ?  "F" :"f"; btnF.Focus(); }
                else if (e.KeyCode == Keys.G) { cKey = bCapital ?  "G" :"g"; btnG.Focus(); }
                else if (e.KeyCode == Keys.H) { cKey = bCapital ?  "H" :"h"; btnH.Focus(); }
                else if (e.KeyCode == Keys.J) { cKey = bCapital ?  "J" :"j"; btnJ.Focus(); }
                else if (e.KeyCode == Keys.K) { cKey = bCapital ?  "K" :"k"; btnK.Focus(); }
                else if (e.KeyCode == Keys.L) { cKey = bCapital ?  "L" :"l"; btnL.Focus(); }
                //                                          
                else if (e.KeyCode == Keys.Z) { cKey = bCapital ? "Z" :"z" ; btnZ.Focus(); }
                else if (e.KeyCode == Keys.X) { cKey = bCapital ? "X" :"x" ; btnX.Focus(); }
                else if (e.KeyCode == Keys.C) { cKey = bCapital ? "C" :"c" ; btnC.Focus(); }
                else if (e.KeyCode == Keys.V) { cKey = bCapital ? "V" :"v" ; btnV.Focus(); }
                else if (e.KeyCode == Keys.B) { cKey = bCapital ? "B" :"b" ; btnB.Focus(); }
                else if (e.KeyCode == Keys.N) { cKey = bCapital ? "N" :"n" ; btnN.Focus(); }
                else if (e.KeyCode == Keys.M) { cKey = bCapital ? "M" :"m" ; btnM.Focus(); }

                if (NewValue.Length <= MaxLength)
                    NewValue += cKey;

                if (Asterisk)
                {
                    lbl_NewData.Text = null;

                    for (int i = 0; i < NewValue.Length; i++)
                        lbl_NewData.Text += "*";
                }
                else
                {
                    lbl_NewData.Text = NewValue;
                }

            }
            else if (e.KeyCode == Keys.Back) //BackSpace
            {
                if (!string.IsNullOrEmpty(NewValue))
                {
                    string strData = "";

                    strData = NewValue;

                    int nLength = NewValue.Length;
                    if (nLength == 1) NewValue = "";
                    else NewValue = strData.Remove(nLength - 1, 1);

                    if (Asterisk)
                    {
                        lbl_NewData.Text = null;
                        for (int i = 0; i < NewValue.Length; i++)
                            lbl_NewData.Text += "*";
                    }
                    else
                    {
                        lbl_NewData.Text = NewValue;
                    }
                }
            }           
        }

        private void OnCloseEvent(DialogResult a_DlgResult)
        {
            this.DialogResult = a_DlgResult;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            NewValue = "";
            lbl_NewData.Text = "";
            btnClear.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Focus();
            OnCloseEvent(DialogResult.Cancel);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            btnEnter.Focus();
            OnCloseEvent(DialogResult.OK);
        }

        private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelFrmTitleBar.Capture = false;
            // Create and send a WM_NCLBUTTONDOWN message.
            const int WM_NCLBUTTONDOWN = 0x00A1;
            const int HTCAPTION = 2;
            Message msg =
                    Message.Create(this.Handle, WM_NCLBUTTONDOWN,
                            new IntPtr(HTCAPTION), IntPtr.Zero);
        }

        private void btn_Frm_Close_Click(object sender, EventArgs e)
        {
            btnCancel.Focus();
            OnCloseEvent(DialogResult.Cancel);
        }

        private void AlphaKeypad_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //this.KeyPreview = true;

            }
        }
        public DialogResult ShowDialog(uint a_uiMaxLength, string a_strOldText,bool a_bCaptialMode=true)
        {
            m_uMaxLength = a_uiMaxLength;
            OldValue = a_strOldText;
            bCapitalMode =a_bCaptialMode;
            return ShowDialog();
        }
        private void AlphaKeypad_Load(object sender, EventArgs e)
        {
            lbl_OldData.Text = OldValue;
        }

        private void lbl_OldData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lbl_NewData.Text = lbl_OldData.Text;
        }
       
    }
}
