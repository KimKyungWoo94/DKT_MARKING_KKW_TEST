using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
    public partial class DGVToggleButtonCell
    {
        private DGVToggleButtonRenderBase _renderer;
        private ToggleSwitchStyle _style = ToggleSwitchStyle.Metro;
        
        private bool _moving = false;                
        private bool _Enabled=true;
        private bool _isLeftFieldHovered = false;
        private bool _isButtonHovered = false;
        private bool _isRightFieldHovered = false;
        private bool _isLeftFieldPressed = false;
        private bool _isButtonPressed = false;
        private bool _isRightFieldPressed = false;
        private string _Text;
        private int _buttonValue = 0;
        private int _savedButtonValue = 0;
        private int _xOffset = 0;
        private int _xValue = 0;
        private int _thresholdPercentage = 50;
        private bool _grayWhenDisabled = true;
        private bool _toggleOnButtonClick = true;
        private bool _toggleOnSideClick = true;
        
        private ToggleSwitchButtonAlignment _buttonAlignment = ToggleSwitchButtonAlignment.Center;   
        private string _offText = "";
        private Color _offForeColor = Color.Black;
        private Font _offFont;
        private ToggleSwitchAlignment _offSideAlignment = ToggleSwitchAlignment.Center;
        private ToggleSwitchButtonAlignment _offButtonAlignment = ToggleSwitchButtonAlignment.Center;
        private string _onText = "";
        private Color _onForeColor = Color.Black;
        private Font _onFont;                
        private ToggleSwitchAlignment _onSideAlignment = ToggleSwitchAlignment.Center;             
        private ToggleSwitchButtonAlignment _onButtonAlignment = ToggleSwitchButtonAlignment.Center;
        #region Public Properties

       
        public ToggleSwitchStyle ButtonStyle
        {
            get { return _style; }
            set
            {
                if (value != _style)
                {
                    _style = value;

                    switch (_style)
                    {
                        case ToggleSwitchStyle.Metro:
                            SetRenderer(new DGVToggleButtonMetroRender());
                            break;
                        case ToggleSwitchStyle.Android:
                            SetRenderer(new DGVToggleButtonAndroidRender());
                            break;
                        case ToggleSwitchStyle.IOS5:
                            SetRenderer(new DGVToggleButtonIOS5Render());
                            break;
                      
                  
                        case ToggleSwitchStyle.Iphone:
                            SetRenderer(new DGVToggleButtonIphoneRender());
                            break;
                     
                        case ToggleSwitchStyle.Modern:
                            SetRenderer(new DGVToggleButtonModernRender());
                            break;                     
                    }
                }

               // Refresh();
            }
        }

//         [Bindable(true)]
//         [DefaultValue(false)]
//         [Category("Data")]
//         [Description("Gets or sets the Checked value of the ToggleSwitch")]     
     
        public bool AllowUserChange
        {
            get { return !this.ReadOnly; }
            //private set { this.ReadOnly = !value; }
        }
     
        public string CheckedString
        {
            get
            {
                return (bool)this.Value ? (string.IsNullOrEmpty(OnText) ? "ON" : OnText) : (string.IsNullOrEmpty(OffText) ? "OFF" : OffText);
            }
        }
    
        public Rectangle ButtonRectangle
        {
            get
            {
                return new Rectangle();//_renderer.GetButtonRectangle();
            }
        }
        public bool Enabled
        {
            get { return _Enabled;}
            set {_Enabled=value; }
        }
        public bool GrayWhenDisabled
        {
            get { return _grayWhenDisabled; }
            set
            {
                if (value != _grayWhenDisabled)
                {
                    _grayWhenDisabled = value;
                    if (!Enabled)
                       this.RefreshCell();
                }
            }
        }

        public bool ToggleOnButtonClick
        {
            get { return _toggleOnButtonClick; }
            set { _toggleOnButtonClick = value; }
        }
       
        public bool ToggleOnSideClick
        {
            get { return _toggleOnSideClick; }
            set { _toggleOnSideClick = value; }
        }

        public int ThresholdPercentage
        {
            get { return _thresholdPercentage; }
            set { _thresholdPercentage = value; }
        }
        public Color OffForeColor
        {
            get { return _offForeColor; }
            set
            {
                if (value != _offForeColor)
                {
                    _offForeColor = value;
                    this.RefreshCell();
                }
            }
        }

       public Font OffFont
        {
            get { return _offFont; }
            set
            {
                if (!value.Equals(_offFont))
                {
                    _offFont = value;
                     this.RefreshCell();
                }
            }
        }

        public string OffText
        {
            get { return _offText; }
            set
            {
                if (value != _offText)
                {
                    _offText = value;
                    this.RefreshCell();
                }
            }
        }
    

     
        public ToggleSwitchAlignment OffSideAlignment
        {
            get { return _offSideAlignment; }
            private set
            {
                if (value != _offSideAlignment)
                {
                    _offSideAlignment = value;
                     this.RefreshCell();
                }
            }
        }

        public ToggleSwitchButtonAlignment OffButtonAlignment
        {
            get { return _offButtonAlignment; }
            private set
            {
                if (value != _offButtonAlignment)
                {
                    _offButtonAlignment = value;
                     this.RefreshCell();
                }
            }
        }

     
        public Color OnForeColor
        {
            get { return _onForeColor; }
            set
            {
                if (value != _onForeColor)
                {
                    _onForeColor = value;
                     this.RefreshCell();
                }
            }
        }

   
        public Font OnFont
        {
            get { return _onFont; }
            set
            {
                if (!value.Equals(_onFont))
                {
                    _onFont = value;
                     this.RefreshCell();
                }
            }
        }

     
        public string OnText
        {
            get { return _onText; }
            set
            {
                if (value != _onText)
                {
                    _onText = value;
                     this.RefreshCell();
                }
            }
        }


        public ToggleSwitchButtonAlignment ButtonAlignment
        {
            get { return _buttonAlignment; }
            private set
            {
                if (value != _buttonAlignment)
                {
                    _buttonAlignment = value;
                     this.RefreshCell();
                }
            }
        }

     
        public ToggleSwitchAlignment OnSideAlignment
        {
            get { return _onSideAlignment; }
            private set
            {
                if (value != _onSideAlignment)
                {
                    _onSideAlignment = value;
                     this.RefreshCell();
                }
            }
        }

     

       public ToggleSwitchButtonAlignment OnButtonAlignment
        {
            get { return _onButtonAlignment; }
            private set
            {
                if (value != _onButtonAlignment)
                {
                    _onButtonAlignment = value;
                     this.RefreshCell();
                }
            }
        }


        #region Hidden Base Properties



        #endregion Hidden Base Properties

        #endregion Public Properties


        #region Internal Properties

      
        internal bool IsButtonHovered
        {
            get { return _isButtonHovered && !_isButtonPressed; }
        }

        internal bool IsButtonPressed
        {
            get { return _isButtonPressed; }
        }
        internal bool IsMouseMoving
        {
            get { return _moving; }
        }
        internal bool IsLeftSideHovered
        {
            get { return _isLeftFieldHovered && !_isLeftFieldPressed; }
        }

        internal bool IsLeftSidePressed
        {
            get { return _isLeftFieldPressed; }
        }

        internal bool IsRightSideHovered
        {
            get { return _isRightFieldHovered && !_isRightFieldPressed; }
        }
        internal bool IsRightSidePressed
        {
            get { return _isRightFieldPressed; }
        }

      
        internal int ButtonValue
        {
            get
            {
                return _buttonValue;
                
            }
            set
            {
                if (value != _buttonValue)
                {
                    _buttonValue = value;
                    this.RefreshCell();
                }
            }
        }

      
        internal bool IsButtonOnLeftSide(int xValue)
        {
            return (xValue <= 0); 
        }

        internal bool IsButtonOnRightSide(int xValue)
        {
            return (xValue >= (_renderer.ControlRect.Width - _renderer.GetButtonWidth())); 
        }

        #endregion Private Properties
    }
}
