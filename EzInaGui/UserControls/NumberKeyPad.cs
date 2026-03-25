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
    public partial class NumberKeypad : Form, INotifyPropertyChanged
    {
        private readonly uint DEFAULT_MAXLENGTH = 15;
        private readonly bool DEFAULT_ASTERISK = false;
        private string m_strValue;
        private string m_strOperator;
        private double m_fResult;
        private double m_fMin;
        private double m_fMax;
        private double m_fCurrentSet;
        uint m_uMaxLength;
        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<Keys, string> m_DicConvertOperator;
        /// <summary>
        /// * 표시
        /// </summary>
        public bool Asterisk { get; set; }

        /// <summary>
        /// 기존 값.
        /// </summary>
        public double Result
        {
            get { return m_fResult; }
            set
            {
                this.CheckPropertyChanged<double>("Result", ref m_fResult, value);
            }
        }
        public string strValue
        {
            get { return m_strValue; }
            private set
            {
                this.CheckPropertyChanged<string>("strValue", ref m_strValue, value);
            }

        }
        public string strOperator
        {
            get { return m_strOperator; }
            private set
            {
                this.CheckPropertyChanged<string>("strOperator", ref m_strOperator, value);
            }

        }
        ///<summary>
        /// 현재 값.
        ///</summary>
        public double Min
        {
            get { return m_fMin; }
            private set
            {
                this.CheckPropertyChanged<double>("Min", ref m_fMin, value);
            }
        }

        public double Max
        {

            get { return m_fMax; }
            private set
            {
                this.CheckPropertyChanged<double>("Max", ref m_fMax, value);
            }

        }
        public double CurrentSet
        {
            get { return m_fCurrentSet; }
            private set
            {
                this.CheckPropertyChanged<double>("CurrentSet", ref m_fCurrentSet, value);
            }
        }
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

        public NumberKeypad()
        {
            InitializeComponent();
            CreateInputEvent();
            InitializeBinding();
            InitializeOperatorNumberPadKey();
            this.strValue = "";
            this.strOperator = "";
            this.Result = 0.0;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaxLength = DEFAULT_MAXLENGTH;
            this.Asterisk = DEFAULT_ASTERISK;

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyPress);
        }
        protected void InitializeBinding()
        {
            CreateBindingTextBox(lbl_MIN, "Min");
            CreateBindingTextBox(lbl_MAX, "Max");
            CreateBindingTextBox(lbl_CurrentSet, "CurrentSet");
            CreateBindingTextBox(lbl_InputValue, "strValue");
            CreateBindingTextBox(lbl_Operator, "strOperator");
            CreateBindingTextBox(lbl_Result, "Result");
        }
        protected void InitializeOperatorNumberPadKey()
        {
            m_DicConvertOperator = new Dictionary<Keys, string>();
            m_DicConvertOperator.Add(Keys.Add, "+");
            m_DicConvertOperator.Add(Keys.Oemplus, "+");
            m_DicConvertOperator.Add(Keys.Subtract, "-");
            m_DicConvertOperator.Add(Keys.OemMinus, "-");
            m_DicConvertOperator.Add(Keys.Multiply, "*");
            m_DicConvertOperator.Add(Keys.Divide, "/");
            m_DicConvertOperator.Add(Keys.OemQuestion, "/");
            m_DicConvertOperator.Add(Keys.OemPeriod, ".");
            m_DicConvertOperator.Add(Keys.Decimal, ".");

        }
        protected void CreateBindingTextBox(Control pControl, string BindingStr)
        {
            pControl.DataBindings.Add(new Binding("Text", this, BindingStr, false, DataSourceUpdateMode.OnPropertyChanged, 0));
        }
        protected bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (oldValue == null && newValue == null)
            {
                return false;
            }

            if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
            {
                oldValue = newValue;

                FirePropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        protected void FirePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void CreateInputEvent()
        {

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

            btnhyphen.Click += new EventHandler(OnKeyInput);
            btnDot.Click += new EventHandler(OnKeyInput);
            btnMultiply.Click += new EventHandler(OnKeyInput);
            btnDiv.Click += new EventHandler(OnKeyInput);
            btnPlus.Click += new EventHandler(OnKeyInput);
            this.panelFrmTitleBar.MouseDown += lblMoveForm_MouseDown;
        }
        public DialogResult ShowDialog(double a_Min, double a_Max, double a_CurrentSet = 0.0)
        {
            Min = a_Min;
            Max = a_Max;
            CurrentSet = a_CurrentSet;
            return base.ShowDialog();
        }
        private void lblMoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            {
                // Release the mouse capture started by the mouse down.

                this.panelFrmTitleBar.Capture = false;
                // Create and send a WM_NCLBUTTONDOWN message.
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                Message msg =
                    Message.Create(this.Handle, WM_NCLBUTTONDOWN,
                        new IntPtr(HTCAPTION), IntPtr.Zero);
                this.DefWndProc(ref msg);

            }
        }
        private void OnKeyInput(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btnEnter.Focus();
            if (IsOperatorlttter(btn.Text))
            {
                double ftemp = 0.0;
                double.TryParse(strValue, out ftemp);
                if (string.IsNullOrWhiteSpace(strOperator))
                {
                    Result = ftemp;
                }
                else
                {
                    CalculateResult(btn.Text, ftemp);
                }
                strOperator = btn.Text;
                strValue = "";
            }
            else
            {

                strValue = strValue + btn.Text;

                //lbl_Value.Text = "";              
            }
            //this.ActiveControl=null;
        }
        private bool IsOperatorlttter(string a_value)
        {
            bool bRet = false;
            switch (a_value)
            {
                case "-":
                case "+":
                case "*":
                case "/":
                case "Add":
                case "Subtract":
                case "Multiply":
                case "Divide":
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void OnPreviewKeyPress(object sender, PreviewKeyDownEventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(strOperator))
                {
                    if (string.IsNullOrWhiteSpace(strValue) == false)
                    {
                        double ftemp = 0.0;
                        double.TryParse(strValue, out ftemp);
                        Result = ftemp;
                    }
                    if (Result >= Min && Result <= Max)
                    {
                        OnCloseEvent(DialogResult.OK);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Result Value isn't between the MIN & Min Value\nResult: {0} Min :{1} Max:{2}", Result, Min, Max));
                    }
                }
                else
                {
                    double ftemp = 0.0;
                    double.TryParse(strValue, out ftemp);
                    CalculateResult(strOperator, ftemp);
                    strValue = "";
                    strOperator = "";
                }
            }
            else if (keyData == Keys.Escape)
            {
                OnCloseEvent(DialogResult.Cancel);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            string cKey = "";

            //lbl_NewData.Text = "";

            //nexBtnOK.Focus();

            if (
                (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            || (e.KeyCode == Keys.OemQuestion)
            || (e.KeyCode == Keys.Oemplus)
            || (e.KeyCode == Keys.Add)
            || (e.KeyCode == Keys.OemMinus)
            || (e.KeyCode == Keys.Subtract)
            || (e.KeyCode == Keys.Multiply)
            || (e.KeyCode == Keys.Divide)
            || (e.KeyCode == Keys.OemPeriod)
            || (e.KeyCode == Keys.Decimal))
            {

                if ((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
                {
                    cKey = e.KeyCode.ToString().Replace("NumPad", "");
                }
                else if (e.KeyCode == Keys.Add ||
                        e.KeyCode == Keys.Subtract ||
                        e.KeyCode == Keys.Multiply ||
                        e.KeyCode == Keys.Divide ||
                        e.KeyCode == Keys.OemQuestion ||
                        e.KeyCode == Keys.OemPeriod ||
                        e.KeyCode == Keys.Decimal ||
                        e.KeyCode == Keys.OemMinus ||
                        e.KeyCode == Keys.Oemplus
                    )
                {
                    cKey = m_DicConvertOperator[e.KeyCode];
                }

                else
                {
                    cKey = Convert.ToChar(e.KeyValue).ToString();
                }
                if (IsOperatorlttter(cKey))
                {
                    double ftemp = 0.0;

                    double.TryParse(strValue, out ftemp);
                    if (string.IsNullOrWhiteSpace(strOperator))
                    {
                        Result = ftemp;
                    }
                    else
                    {
                        CalculateResult(cKey, ftemp);
                    }
                    strOperator = cKey;
                    strValue = "";
                }
                else
                {
                    if (cKey == ".")
                    {
                        if (string.IsNullOrWhiteSpace(strValue) == false &&
                            strValue.Contains('.') == false)
                        {
                            strValue = strValue + cKey;
                        }
                    }
                    else
                    {
                        strValue = strValue + cKey;
                    }

                }
            }
            else if (e.KeyCode == Keys.Back) //BackSpace
            {
                if (!string.IsNullOrEmpty(m_strValue))
                {
                    string strData = "";

                    strData = strValue;

                    int nLength = strValue.Length;
                    if (nLength == 1) strValue = "";
                    else strValue = strValue.Remove(nLength - 1, 1);
                }
            }
        }

        private void OnCloseEvent(DialogResult a_DlgResult)
        {
            this.DialogResult = a_DlgResult;
            strOperator = "";
            strValue = "";
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(strValue))
            {
                strValue = "";
                strOperator = "";
                Result = 0.0;
            }
            else
            {
                strValue = "";
            }
            //btnClear.Focus();
        }
        private void CalculateResult(string a_Operator, double a_Input)
        {
            switch (a_Operator)
            {
                case "-":
                    {
                        Result = Result - a_Input;
                    }
                    break;
                case "+":
                    {
                        Result = Result + a_Input;
                    }
                    break;
                case "*":
                    {
                        Result = Result * a_Input;
                    }
                    break;
                case "/":
                    {
                        if (a_Input != 0)
                        {
                            Result = Result / a_Input;
                        }
                    }
                    break;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //btnCancel.Focus();
            OnCloseEvent(DialogResult.Cancel);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(strOperator))
            {
                if (string.IsNullOrWhiteSpace(strValue) == false)
                {
                    double ftemp = 0.0;
                    double.TryParse(strValue, out ftemp);
                    strValue = "";
                    strOperator = "";
                    Result = ftemp;
                }
                if (Result >= Min && Result <= Max)
                {
                    OnCloseEvent(DialogResult.OK);
                }
                else
                {
                    MessageBox.Show(string.Format("Result Value isn't between the MIN & Min Value\nResult: {0} Min :{1} Max:{2}", Result, Min, Max));
                }
            }
            else
            {
                double ftemp = 0.0;
                double.TryParse(strValue, out ftemp);
                CalculateResult(strOperator, ftemp);
                strOperator = "";
                strValue = "";
            }

        }


        private void btn_Frm_Close_Click(object sender, EventArgs e)
        {
            //btnCancel.Focus();
            OnCloseEvent(DialogResult.Cancel);
        }

        private void Keypad_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //this.KeyPreview = true;
                btnEnter.Focus();
                this.ActiveControl = btnEnter;
            }
        }

        private void Keypad_Load(object sender, EventArgs e)
        {
        }

    }
}
