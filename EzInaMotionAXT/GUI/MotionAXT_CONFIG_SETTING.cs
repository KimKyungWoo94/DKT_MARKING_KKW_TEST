using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Motion.AXT
{
    public class CDictionaryBinding<T> : NotifyProperyChangedBase
    {
      
        private T  m_iSelectValue;
        private BindingSource m_DicBindingSource;
        private Binding m_BindingComboSelectedValue;
        public Dictionary<int,string> m_DicItem;
        public BindingSource DicBingingSource
        {
            get { return m_DicBindingSource;}
        }
        public Binding DataBindingCombo
        {
            get { return m_BindingComboSelectedValue;}
        }
        public T SelectValue
        {
            get {  return this.m_iSelectValue;}
            set
            {
                base.CheckPropertyChanged<T>("SelectValue",ref m_iSelectValue,ref value);
            }
        }        
        public CDictionaryBinding (T a_value,string a_CTL_propertyName)
        {
            m_iSelectValue=a_value;
            m_DicItem=new Dictionary<int, string>();
            m_DicBindingSource=new BindingSource(m_DicItem,null);
            m_BindingComboSelectedValue=new Binding(a_CTL_propertyName,this,"SelectValue",false,DataSourceUpdateMode.OnPropertyChanged,0);
            m_BindingComboSelectedValue.BindingComplete+=new BindingCompleteEventHandler(this.BingComplete);            
        }
        protected void BingComplete(object sender, System.Windows.Forms.BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState != System.Windows.Forms.BindingCompleteState.Success)
            {
                Trace.WriteLine(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
                //System.Windows.Forms.MessageBox.Show(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
            }
        }
      
    }
    class CMotionAXT_PARAM_SETTING_GUI : NotifyProperyChangedBase
    {
       stMotionAXT_ParamSetting m_Setting;
       public stMotionAXT_ParamSetting SettingValue
       {
           get { return m_Setting;}
           set
            {
                PluseOutputMode=value.UiPulseOutput_Mode;
                EncoderInputMode=value.UiEncoderInput_Mode;
                AbsRelMode=value.UiAbsRelMode;
                ProfileMode=value.UiProfileMode;
                MinVel=value.m_dMinVel;
                MaxVel=value.m_dMaxVel;
                Unit=value.m_dUnit;
                Pulse=value.m_iPulse;
            }
       }

        #region PulseOutMode
            public Dictionary<uint,string> m_DicPulseMode;
            Binding m_BindingComboPulseMode;
            public Binding BindingComboPulseMode
            {
                 get {return m_BindingComboPulseMode;
                     
                 }
            }
            public uint PluseOutputMode
            {
                 get
                 {
                     return m_Setting.UiPulseOutput_Mode;
                 }
                 set
                 {
                     base.CheckPropertyChanged<uint>("PluseOutputMode",ref m_Setting.UiPulseOutput_Mode,ref value);
                 }
            }
        #endregion PulseOutMode

        #region EncoderInputMode
            public Dictionary<uint,string> m_DicEncoderInputMode;
            Binding m_BindingComboEncoderInputMode;
            public Binding BindingComboEncoderInputMode
            {
                 get {return m_BindingComboEncoderInputMode;}
            }
            public uint EncoderInputMode
            {
                 get
                 {
                     return m_Setting.UiEncoderInput_Mode;
                 }
                 set
                 {
                     base.CheckPropertyChanged<uint>("EncoderInputMode",ref m_Setting.UiEncoderInput_Mode,ref value);
                 }
            }
        #endregion EncoderInputMode    

        #region     AbsRelMode
            public Dictionary<uint,string> m_DicAbsRelMode;
            Binding m_BindingComboAbsRelMode;
            public Binding BindingComboAbsRelMode
            {
                 get {return m_BindingComboAbsRelMode;}
            }
            public uint AbsRelMode
            {
                 get
                 {
                     return m_Setting.UiAbsRelMode;
                 }
                 set
                 {
                     base.CheckPropertyChanged<uint>("AbsRelMode",ref m_Setting.UiAbsRelMode,ref value);
                 }
            }

        #endregion  AbsRelMode

        #region     ProfileMode
            public Dictionary<uint,string> m_DicProfileMode;
            Binding m_BindingComboProfileMode;
            public Binding BindingComboProfileMode
            {
                 get {return m_BindingComboProfileMode;}
            }
            public uint ProfileMode
            {
                 get
                 {
                     return m_Setting.UiProfileMode;
                 }
                 set
                 {
                     base.CheckPropertyChanged<uint>("ProfileMode",ref m_Setting.UiProfileMode,ref value);
                 }
             }
        #endregion  ProfileMode

        #region     Minvel 
            Binding m_BindingTextMinVel;
            public Binding BindingTextMinVel
            {
                 get {return m_BindingTextMinVel;}
            }
            public double MinVel
            {
                 get
                 {
                     return m_Setting.m_dMinVel;
                 }
                 set
                 {
                     base.CheckPropertyChanged<double>("MinVel",ref m_Setting.m_dMinVel,ref value);
                 }
             }
        #endregion  Minvel

        #region     MaxVel
            Binding m_BindingTextMaxVel;
            public Binding BindingTextMaxVel
            {
                 get {return m_BindingTextMaxVel;}
            }
            public double MaxVel
            {
                 get
                 {
                     return m_Setting.m_dMaxVel;
                 }
                 set
                 {
                     base.CheckPropertyChanged<double>("MaxVel",ref m_Setting.m_dMaxVel,ref value);
                 }
             }
        #endregion  MaxVel

        #region  UNIT
            Binding m_BindingTextUnit;
            public Binding BindingTextUnit
            {
                 get {return m_BindingTextUnit;}
            }
            public double Unit
            {
                 get
                 {
                     return m_Setting.m_dUnit;
                 }
                 set
                 {
                     base.CheckPropertyChanged<double>("Unit",ref m_Setting.m_dUnit,ref value);
                 }
             }

        #endregion  UNIT
        
        #region  Pulse
            Binding m_BindingTextPulse;
            public Binding BindingTextPulse
            {
                 get {return m_BindingTextPulse;}
            }
            public int Pulse
            {
                 get
                 {
                     return m_Setting.m_iPulse;
                 }
                 set
                 {
                     base.CheckPropertyChanged<int>("Unit",ref m_Setting.m_iPulse,ref value);
                 }
             }
        #endregion  Pulse
     
       public CMotionAXT_PARAM_SETTING_GUI()
       {
            AllocMemoryComboBoxSelectedValue(ref this.m_DicPulseMode,       
                                             ref this.m_BindingComboPulseMode,
                                             "PluseOutputMode");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicEncoderInputMode,
                                             ref this.m_BindingComboEncoderInputMode,
                                             "EncoderInputMode");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicAbsRelMode,
                                             ref this.m_BindingComboAbsRelMode,
                                             "AbsRelMode");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicProfileMode,
                                             ref this.m_BindingComboProfileMode,
                                             "ProfileMode");
            AllocMemoryText                 (ref this.m_BindingTextMinVel,
                                             "MinVel");
            AllocMemoryText                 (ref this.m_BindingTextMaxVel,
                                             "MaxVel");
            AllocMemoryText                 (ref this.m_BindingTextUnit,
                                             "Unit");
            AllocMemoryText                 (ref this.m_BindingTextPulse,
                                             "Pulse");
            m_BindingTextMinVel.FormattingEnabled   =   true;
            m_BindingTextMaxVel.FormattingEnabled   =   true;
            m_BindingTextUnit.FormattingEnabled     =   true;
            m_BindingTextMinVel.FormatString="F5";
            m_BindingTextMaxVel.FormatString="F5";
            m_BindingTextUnit.FormatString="F5";
        }
        public void ClearAll()
        {
            ClearList();
            ClearWithoutList();
        }
        public void ClearList()
        {
            m_DicPulseMode.Clear();
            m_DicEncoderInputMode.Clear();
            m_DicAbsRelMode.Clear();
            m_DicProfileMode.Clear();
        }
        public void ClearWithoutList()
        {
            MinVel = 0.0;
            MaxVel = 0.0;
            Unit = 0.0;
            Pulse = 0;
        }
        void AllocMemoryComboBoxSelectedValue<T,T1>(ref Dictionary<T,T1> a_Dic,ref Binding a_Binding,string BindingStr)
        {
            a_Dic=null;
            a_Binding=null;
            a_Dic=new Dictionary<T,T1>();
            a_Binding =new Binding("SelectedValue",this,BindingStr,false,DataSourceUpdateMode.OnPropertyChanged);
            a_Binding.BindingComplete+=new BindingCompleteEventHandler(this.BingComplete); 
        }
        void AllocMemoryText(ref Binding a_Binding,string BindingStr)
        {         
            a_Binding=null;           
            a_Binding =new Binding("Text",this,BindingStr,false,DataSourceUpdateMode.OnPropertyChanged,0);
            a_Binding.BindingComplete+=new BindingCompleteEventHandler(this.BingComplete); 
        }
        protected void BingComplete(object sender, System.Windows.Forms.BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState != System.Windows.Forms.BindingCompleteState.Success)
            {
                Trace.WriteLine(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
                //System.Windows.Forms.MessageBox.Show(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
            }
        }
    }

    class CMotionAXT_SIGNAL_SETTING_GUI : NotifyProperyChangedBase
    {
       stMotionAXT_SignalSetting m_Setting;
       public stMotionAXT_SignalSetting SettingValue
       {
           get { return m_Setting;}
           set
            {
                ZPhaseActiveLevel           =value.UiZPhase_ActiveLevel;               
                InPosActiveLevel            =value.UiInPostion_ActiveLevel;
                EMGStopMode                 =value.UiEMGStop_Mode;
                EMGStopActiveLevel          =value.UiEMGStop_ActiveLevel;
                PositiveActiveLevel         =value.UiPositive_ActiveLevel;
                NegativeActiveLevel         =value.UiNegative_ActiveLevel;
                ServoOnActiveLevel          =value.UiServoOn_ActiveLevel;
                ServoAlarmActiveLevel       =value.UiServoAlarm_ActiveLevel;
                ServoAlarmResetActiveLevel  =value.UiServoAlarmReset_ActiveLevel;
                EncoderType                 =value.UiEncoderType;
            }
       }

        #region ZPhaseActiveLevel
            public Dictionary<uint,string> m_DicZPhaseActiveLevel;
            Binding m_BindingComboZPhaseActiveLevel;
            public Binding BindingComboZPhaseActiveLevel
            {
                 get {return m_BindingComboZPhaseActiveLevel;
                     
                 }
            }
            public uint ZPhaseActiveLevel
            {
                 get
                 {
                     return m_Setting.UiZPhase_ActiveLevel;
                 }
                 set
                 {
                     base.CheckPropertyChanged<uint>("ZPhaseActiveLevel",ref m_Setting.UiZPhase_ActiveLevel,ref value);
                 }
            }
        #endregion ZPhaseActiveLevel
      
        #region     InPosActiveLevel
            public Dictionary<uint,string> m_DicInPosActiveLevel;
            Binding m_BindingComboInPosActiveLevel;
            public Binding BindingComboInPosActiveLevel
            {
                 get {return m_BindingComboInPosActiveLevel;}
            }
            public uint InPosActiveLevel
            {
                 get
                 {
                     return m_Setting.UiInPostion_ActiveLevel;
                 }
                 set
                 {
                     base.CheckPropertyChanged<uint>("InPosActiveLevel",ref m_Setting.UiInPostion_ActiveLevel,ref value);
                 }
            }

        #endregion  InPosActiveLevel

        #region     EMGStopMode
            public Dictionary<uint,string> m_DicEMGStopMode;
            Binding m_BindingComboEMGStopMode;
            public Binding BindingComboEMGStopMode
            {
                 get {return m_BindingComboEMGStopMode;}
            }
            public uint EMGStopMode
            {
                 get
                 {
                     return m_Setting.UiEMGStop_Mode;
                 }
                 set
                 {
                     base.CheckPropertyChanged<uint>("EMGStopMode",ref m_Setting.UiEMGStop_Mode,ref value);
                 }
             }
        #endregion  EMGStopMode

        #region     EMGStopActiveLevel
        public Dictionary<uint, string> m_DicEMGStopActiveLevel;
        Binding m_BindingComboEMGStopActiveLevel;
        public Binding BindingComboEMGStopActiveLevel
        {
            get { return m_BindingComboEMGStopActiveLevel; }
        }
        public uint EMGStopActiveLevel
        {
            get
            {
                return m_Setting.UiEMGStop_ActiveLevel;
            }
            set
            {
                base.CheckPropertyChanged<uint>("EMGStopActiveLevel", ref m_Setting.UiEMGStop_ActiveLevel, ref value);
            }
        }
        #endregion  EMGStopActiveLevel

        #region     PositiveActiveLevel
        public Dictionary<uint, string> m_DicPositiveActiveLevel;
        Binding m_BindingComboPositiveActiveLevel;
        public Binding BindingComboPositiveActiveLevel
        {
            get { return m_BindingComboPositiveActiveLevel; }
        }
        public uint PositiveActiveLevel
        {
            get
            {
                return m_Setting.UiPositive_ActiveLevel;
            }
            set
            {
                base.CheckPropertyChanged<uint>("PositiveActiveLevel", ref m_Setting.UiPositive_ActiveLevel, ref value);
            }
        }
        #endregion  PositiveActiveLevel

        #region     NegativeActiveLevel
        public Dictionary<uint, string> m_DicNegativeActiveLevel;
        Binding m_BindingComboNegativeActiveLevel;
        public Binding BindingComboNegativeActiveLevel
        {
            get { return m_BindingComboNegativeActiveLevel; }
        }
        public uint NegativeActiveLevel
        {
            get
            {
                return m_Setting.UiNegative_ActiveLevel;
            }
            set
            {
                base.CheckPropertyChanged<uint>("NegativeActiveLevel", ref m_Setting.UiNegative_ActiveLevel, ref value);
            }
        }
        #endregion  NegativeActiveLevel

        #region     ServoOnActiveLevel
        public Dictionary<uint, string> m_DicServoOnActiveLevel;
        Binding m_BindingComboServoOnActiveLevel;
        public Binding BindingComboServoOnActiveLevel
        {
            get { return m_BindingComboServoOnActiveLevel; }
        }
        public uint ServoOnActiveLevel
        {
            get
            {
                return m_Setting.UiServoOn_ActiveLevel;
            }
            set
            {
                base.CheckPropertyChanged<uint>("ServoOnActiveLevel", ref m_Setting.UiServoOn_ActiveLevel, ref value);
            }
        }
        #endregion  ServoOnActiveLevel

        #region ServoAlarmActiveLevel
        public Dictionary<uint, string> m_DicServoAlarmActiveLevel;
        Binding m_BindingComboServoAlarmActiveLevel;
        public Binding BindingComboServoAlarmActiveLevel
        {
            get { return m_BindingComboServoAlarmActiveLevel; }
        }
        public uint ServoAlarmActiveLevel
        {
            get
            {
                return m_Setting.UiServoAlarm_ActiveLevel;
            }
            set
            {
                base.CheckPropertyChanged<uint>("ServoAlarmActiveLevel", ref m_Setting.UiServoAlarm_ActiveLevel, ref value);
            }
        }
        #endregion ServoAlarmActiveLevel    

        #region     ServoAlarmResetActiveLevel
        public Dictionary<uint, string> m_DicServoAlarmResetActiveLevel;
        Binding m_BindingComboServoAlarmResetActiveLevel;
        public Binding BindingComboServoAlarmResetActiveLevel
        {
            get { return m_BindingComboServoAlarmResetActiveLevel; }
        }
        public uint ServoAlarmResetActiveLevel
        {
            get
            {
                return m_Setting.UiServoAlarmReset_ActiveLevel;
            }
            set
            {
                base.CheckPropertyChanged<uint>("ServoAlarmResetActiveLevel", ref m_Setting.UiServoAlarmReset_ActiveLevel, ref value);
            }
        }
        #endregion  ServoAlarmResetActiveLevel

        #region     EncoderType
        public Dictionary<uint, string> m_DicEncoderType;
        Binding m_BindingComboEncoderType;
        public Binding BindingComboEncoderType
        {
            get { return m_BindingComboEncoderType; }
        }
        public uint EncoderType
        {
            get
            {
                return m_Setting.UiEncoderType;
            }
            set
            {
                base.CheckPropertyChanged<uint>("EncoderType", ref m_Setting.UiEncoderType, ref value);
            }
        }
        #endregion  EncoderType
     
        public CMotionAXT_SIGNAL_SETTING_GUI()
       {
            AllocMemoryComboBoxSelectedValue(ref this.m_DicZPhaseActiveLevel,       
                                             ref this.m_BindingComboZPhaseActiveLevel,
                                             "ZPhaseActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicInPosActiveLevel,
                                             ref this.m_BindingComboInPosActiveLevel,
                                             "InPosActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicEMGStopMode,
                                             ref this.m_BindingComboEMGStopMode,
                                             "EMGStopMode");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicEMGStopActiveLevel,
                                             ref this.m_BindingComboEMGStopActiveLevel,
                                            "EMGStopActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicPositiveActiveLevel,
                                             ref this.m_BindingComboPositiveActiveLevel,
                                             "PositiveActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicNegativeActiveLevel,
                                             ref this.m_BindingComboNegativeActiveLevel,
                                             "NegativeActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicServoOnActiveLevel,
                                             ref this.m_BindingComboServoOnActiveLevel,
                                             "ServoOnActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicServoAlarmActiveLevel,
                                             ref this.m_BindingComboServoAlarmActiveLevel,
                                             "ServoAlarmActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicServoAlarmResetActiveLevel,
                                             ref this.m_BindingComboServoAlarmResetActiveLevel,
                                             "ServoAlarmResetActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicEncoderType,
                                             ref this.m_BindingComboEncoderType,
                                             "EncoderType");
        }
        public void ClearAll()
        {
            ClearList();
        }
        public void ClearList()
        {
            this.m_DicZPhaseActiveLevel.Clear();
            this.m_DicInPosActiveLevel.Clear();
            this.m_DicEMGStopMode.Clear();
            this.m_DicPositiveActiveLevel.Clear();
            this.m_DicNegativeActiveLevel.Clear();
            this.m_DicServoOnActiveLevel.Clear();
            this.m_DicServoAlarmActiveLevel.Clear();
            this.m_DicServoAlarmResetActiveLevel.Clear();
            this.m_DicEncoderType.Clear();
        }
        public void ClearWithoutList()
        {

        }
        void AllocMemoryComboBoxSelectedValue<T,T1>(ref Dictionary<T,T1> a_Dic,ref Binding a_Binding,string BindingStr)
        {
            a_Dic=null;
            a_Binding=null;
            a_Dic=new Dictionary<T,T1>();
            a_Binding =new Binding("SelectedValue",this,BindingStr,false,DataSourceUpdateMode.OnPropertyChanged);
            a_Binding.BindingComplete+=new BindingCompleteEventHandler(this.BingComplete); 
        }
        void AllocMemoryText(ref Binding a_Binding,string BindingStr)
        {         
            a_Binding=null;           
            a_Binding =new Binding("Text",this,BindingStr,false,DataSourceUpdateMode.OnPropertyChanged,0);
            a_Binding.BindingComplete+=new BindingCompleteEventHandler(this.BingComplete); 
        }
        protected void BingComplete(object sender, System.Windows.Forms.BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState != System.Windows.Forms.BindingCompleteState.Success)
            {
                Trace.WriteLine(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
                //System.Windows.Forms.MessageBox.Show(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
            }
        }
    }

    class CMotionAXT_HOMESEARCH_SETTING_GUI : NotifyProperyChangedBase
    {
        stMotionAXT_HomeSearchSetting m_Setting;
        public stMotionAXT_HomeSearchSetting SettingValue
        {
            get { return m_Setting; }
            set
            {
                HomeSignalActiveLevel = value.UiHomeSignal_ActiveLevel;
                HomeSignal = value.UiHomeSignal;
                HomeDir = value.m_iHomeDir;
                HomeZPhaseUse = value.UiHomeZPhase;
                HomeClearTime=value.m_dHomeClrTime;
                HomeOffset=value.m_dHomeOffset;
                HomeVelFirst=value.m_dVelFirst;
                HomeVelSecond=value.m_dVelSecond;
                HomeVelThird=value.m_dVelThird;
                HomeVelLast=value.m_dVelLast;
                HomeAccFirst=value.m_dAccFirst;
                HomeAccSecond=value.m_dAccSecond;
            }
        }

        #region HomeSignalActiveLevel
        public Dictionary<uint, string> m_DicHomeSignalActiveLevel;
        Binding m_BindingComboHomeSignalActiveLevel;
        public Binding BindingComboHomeSignalActiveLevel
        {
            get
            {
                return m_BindingComboHomeSignalActiveLevel;

            }
        }
        public uint HomeSignalActiveLevel
        {
            get
            {
                return m_Setting.UiHomeSignal_ActiveLevel;
            }
            set
            {
                base.CheckPropertyChanged<uint>("HomeSignalActiveLevel", ref m_Setting.UiHomeSignal_ActiveLevel, ref value);
            }
        }
        #endregion HomeSignalActiveLevel

        #region     HomeSignal
        public Dictionary<uint, string> m_DicHomeSignal;
        Binding m_BindingComboHomeSignal;
        public Binding BindingComboHomeSignal
        {
            get { return m_BindingComboHomeSignal; }
        }
        public uint HomeSignal
        {
            get
            {
                return m_Setting.UiHomeSignal;
            }
            set
            {
                base.CheckPropertyChanged<uint>("HomeSignal", ref m_Setting.UiHomeSignal, ref value);
            }
        }

        #endregion  HomeSignal

        #region     HomeDir
        public Dictionary<int, string> m_DicHomeDir;
        Binding m_BindingComboHomeDir;
        public Binding BindingComboHomeDir
        {
            get { return m_BindingComboHomeDir; }
        }
        public int HomeDir
        {
            get
            {
                return m_Setting.m_iHomeDir;
            }
            set
            {
                base.CheckPropertyChanged<int>("HomeDir", ref m_Setting.m_iHomeDir, ref value);
            }
        }
        #endregion  HomeDir

        #region     HomeZPhaseUse
        public Dictionary<uint, string> m_DicHomeZPhaseUse;
        Binding m_BindingComboHomeZPhaseUse;
        public Binding BindingComboHomeZPhaseUse
        {
            get { return m_BindingComboHomeZPhaseUse; }
        }
        public uint HomeZPhaseUse
        {
            get
            {
                return m_Setting.UiHomeZPhase;
            }
            set
            {
                base.CheckPropertyChanged<uint>("HomeZPhaseUse", ref m_Setting.UiHomeZPhase, ref value);
            }
        }
        #endregion  HomeZPhaseUse

        #region     HomeClearTime 
        Binding m_BindingTextHomeClearTime;
        public Binding BindingTextHomeClearTime
        {
            get { return m_BindingTextHomeClearTime; }
        }
        public double HomeClearTime
        {
            get
            {
                return m_Setting.m_dHomeClrTime;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeClearTime", ref m_Setting.m_dHomeClrTime, ref value);
            }
        }
        #endregion  HomeClearTime

        #region     HomeOffset 
        Binding m_BindingTextHomeOffset;
        public Binding BindingTextHomeOffset
        {
            get { return m_BindingTextHomeOffset; }
        }
        public double HomeOffset
        {
            get
            {
                return m_Setting.m_dHomeOffset;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeOffset", ref m_Setting.m_dHomeOffset, ref value);
            }
        }
        #endregion  HomeOffset

        #region     HomeVelFirst 
        Binding m_BindingTextHomeVelFirst;
        public Binding BindingTextHomeVelFirst
        {
            get { return m_BindingTextHomeVelFirst; }
        }
        public double HomeVelFirst
        {
            get
            {
                return m_Setting.m_dVelFirst;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeVelFirst", ref m_Setting.m_dVelFirst, ref value);
            }
        }
        #endregion  HomeVelFirst

        #region     HomeVelSecond 
        Binding m_BindingTextHomeVelSecond ;
        public Binding BindingTextHomeVelSecond 
        {
            get { return m_BindingTextHomeVelSecond ; }
        }
        public double HomeVelSecond 
        {
            get
            {
                return m_Setting.m_dVelSecond ;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeVelSecond ", ref m_Setting.m_dVelSecond, ref value);
            }
        }
        #endregion  HomeVelSecond

        #region     HomeVelThird 
        Binding m_BindingTextHomeVelThird;
        public Binding BindingTextHomeVelThird
        {
            get { return m_BindingTextHomeVelThird; }
        }
        public double HomeVelThird
        {
            get
            {
                return m_Setting.m_dVelThird;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeVelThird ", ref m_Setting.m_dVelThird, ref value);
            }
        }
        #endregion  HomeVelThird

        #region     HomeVelLast 
        Binding m_BindingTextHomeVelLast;
        public Binding BindingTextHomeVelLast
        {
            get { return m_BindingTextHomeVelLast; }
        }
        public double HomeVelLast
        {
            get
            {
                return m_Setting.m_dVelLast;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeVelLast ", ref m_Setting.m_dVelLast, ref value);
            }
        }
        #endregion  HomeVelLast

        #region     HomeAccFirst 
        Binding m_BindingTextHomeAccFirst;
        public Binding BindingTextHomeAccFirst
        {
            get { return m_BindingTextHomeAccFirst; }
        }
        public double HomeAccFirst
        {
            get
            {
                return m_Setting.m_dAccFirst;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeAccFirst ", ref m_Setting.m_dAccFirst, ref value);
            }
        }
        #endregion  HomeAccFirst

        #region     HomeAccSecond 
        Binding m_BindingTextHomeAccSecond;
        public Binding BindingTextHomeAccSecond 
        {
            get { return m_BindingTextHomeAccSecond ; }
        }
        public double HomeAccSecond 
        {
            get
            {
                return m_Setting.m_dAccSecond ;
            }
            set
            {
                base.CheckPropertyChanged<double>("HomeAccSecond  ", ref m_Setting.m_dAccSecond , ref value);
            }
        }
        #endregion  HomeAccSecond 

        public CMotionAXT_HOMESEARCH_SETTING_GUI()
        {
            AllocMemoryComboBoxSelectedValue(ref this.m_DicHomeSignalActiveLevel,
                                             ref this.m_BindingComboHomeSignalActiveLevel,
                                             "HomeSignalActiveLevel");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicHomeSignal,
                                             ref this.m_BindingComboHomeSignal,
                                             "HomeSignal");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicHomeDir,
                                             ref this.m_BindingComboHomeDir,
                                             "HomeDir");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicHomeZPhaseUse,
                                             ref this.m_BindingComboHomeZPhaseUse,
                                             "HomeZPhaseUse");
            AllocMemoryText                 (ref this.m_BindingTextHomeClearTime,
                                             "HomeClearTime");
            AllocMemoryText                 (ref this.m_BindingTextHomeOffset,
                                             "HomeOffset");
            AllocMemoryText                 (ref this.m_BindingTextHomeVelFirst,
                                            "HomeVelFirst");
            AllocMemoryText                 (ref this.m_BindingTextHomeVelSecond,
                                            "HomeVelSecond");
            AllocMemoryText                 (ref this.m_BindingTextHomeVelThird,
                                            "HomeVelThird");
            AllocMemoryText                 (ref this.m_BindingTextHomeVelLast,
                                            "HomeVelLast");
            AllocMemoryText                 (ref this.m_BindingTextHomeAccFirst,
                                            "HomeAccFist");
            AllocMemoryText                 (ref this.m_BindingTextHomeAccSecond,
                                            "HomeAccSecond");

            m_BindingTextHomeClearTime.FormattingEnabled = true;
            m_BindingTextHomeOffset.FormattingEnabled = true;
            m_BindingTextHomeVelFirst.FormattingEnabled = true;            
            m_BindingTextHomeVelSecond.FormattingEnabled = true;
            m_BindingTextHomeVelThird.FormattingEnabled = true;          
            m_BindingTextHomeVelLast.FormattingEnabled = true;
            m_BindingTextHomeAccFirst.FormattingEnabled = true;
            m_BindingTextHomeAccSecond.FormattingEnabled = true;
          
            m_BindingTextHomeClearTime.FormatString = "F5";
            m_BindingTextHomeOffset.FormatString = "F5";
            m_BindingTextHomeVelFirst.FormatString = "F5";
            m_BindingTextHomeVelSecond.FormatString = "F5";
            m_BindingTextHomeVelThird.FormatString = "F5";
            m_BindingTextHomeVelLast.FormatString = "F5";
            m_BindingTextHomeAccFirst.FormatString = "F5";
            m_BindingTextHomeAccSecond.FormatString = "F5";
        }
        public void ClearAll()
        {
            ClearList();
            ClearWithoutList();
        }
        public void ClearList()
        {
            this.m_DicHomeSignalActiveLevel.Clear();
            this.m_DicHomeSignal.Clear();
            this.m_DicHomeDir.Clear();
            this.m_DicHomeZPhaseUse.Clear();
        }
        public void ClearWithoutList()
        {
            HomeClearTime   = 0.0;
            HomeOffset      = 0.0;
            HomeVelFirst    = 0.0;
            HomeVelSecond   = 0.0;
            HomeVelThird    = 0.0;
            HomeVelLast     = 0.0;
            HomeAccFirst    = 0.0;
            HomeAccSecond   = 0.0;
        }
        void AllocMemoryComboBoxSelectedValue<T,T1>(ref Dictionary<T, T1> a_Dic, ref Binding a_Binding, string BindingStr)
        {
            a_Dic = null;
            a_Binding = null;
            a_Dic = new Dictionary<T, T1>();
            a_Binding = new Binding("SelectedValue", this, BindingStr, false, DataSourceUpdateMode.OnPropertyChanged, 0);
            a_Binding.BindingComplete += new BindingCompleteEventHandler(this.BingComplete);
        }
        void AllocMemoryText(ref Binding a_Binding, string BindingStr)
        {
            a_Binding = null;
            a_Binding = new Binding("Text", this, BindingStr, false, DataSourceUpdateMode.OnPropertyChanged, 0);
            a_Binding.BindingComplete += new BindingCompleteEventHandler(this.BingComplete);
        }
        protected void BingComplete(object sender, System.Windows.Forms.BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState != System.Windows.Forms.BindingCompleteState.Success)
            {
                Trace.WriteLine(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
                //System.Windows.Forms.MessageBox.Show(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
            }
        }
    }

    class CMotionAXT_SWLimit_SETTING_GUI : NotifyProperyChangedBase
    {
        stMotionAXT_SWLimitSetting m_Setting;
        public stMotionAXT_SWLimitSetting SettingValue
        {
            get { return m_Setting; }
            set
            {
                SWLimitUse = value.UUse;
                SWLimitStopMode = value.UStopMode;
                SWLimitSelection = value.USelection;
           
                SWLimitP = value.m_dSWLimitP;
                SWLimitN = value.m_dSWLimitN;
               
            }
        }

        #region SWLimitUse
        public Dictionary<uint, string> m_DicSWLimitUse;
        Binding m_BindingComboSWLimitUse;
        public Binding BindingComboSWLimitUse
        {
            get
            {
                return m_BindingComboSWLimitUse;

            }
        }
        public uint SWLimitUse
        {
            get
            {
                return m_Setting.UUse;
            }
            set
            {
                base.CheckPropertyChanged<uint>("SWLimitUse", ref m_Setting.UUse, ref value);
            }
        }
        #endregion SWLimitUse

        #region     SWLimitStopMode
        public Dictionary<uint, string> m_DicSWLimitStopMode;
        Binding m_BindingComboSWLimitStopMode;
        public Binding BindingComboSWLimitStopMode
        {
            get { return m_BindingComboSWLimitStopMode; }
        }
        public uint SWLimitStopMode
        {
            get
            {
                return m_Setting.UStopMode;
            }
            set
            {
                base.CheckPropertyChanged<uint>("SWLimitStopMode", ref m_Setting.UStopMode, ref value);
            }
        }

        #endregion  SWLimitStopMode

        #region     SWLimitSelection
        public Dictionary<uint, string> m_DicSWLimitSelection;
        Binding m_BindingComboSWLimitSelection;
        public Binding BindingComboSWLimitSelection
        {
            get { return m_BindingComboSWLimitSelection; }
        }
        public uint SWLimitSelection
        {
            get
            {
                return m_Setting.USelection;
            }
            set
            {
                base.CheckPropertyChanged<uint>("SWLimitSelection", ref m_Setting.USelection, ref value);
            }
        }
        #endregion  SWLimitSelection

        #region     SWLimitP 
        Binding m_BindingTextSWLimitP;
        public Binding BindingTextSWLimitP
        {
            get { return m_BindingTextSWLimitP; }
        }
        public double SWLimitP
        {
            get
            {
                return m_Setting.m_dSWLimitP;
            }
            set
            {
                base.CheckPropertyChanged<double>("SWLimitP", ref m_Setting.m_dSWLimitP, ref value);
            }
        }
        #endregion  SWLimitP

        #region     SWLimitN 
        Binding m_BindingTextSWLimitN;
        public Binding BindingTextSWLimitN
        {
            get { return m_BindingTextSWLimitN; }
        }
        public double SWLimitN
        {
            get
            {
                return m_Setting.m_dSWLimitN;
            }
            set
            {
                base.CheckPropertyChanged<double>("SWLimitN", ref m_Setting.m_dSWLimitN, ref value);
            }
        }
        #endregion  SWLimitN
     

       
        public CMotionAXT_SWLimit_SETTING_GUI()
        {
            AllocMemoryComboBoxSelectedValue(ref this.m_DicSWLimitUse,
                                             ref this.m_BindingComboSWLimitUse,
                                             "SWLimitUse");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicSWLimitStopMode,
                                             ref this.m_BindingComboSWLimitStopMode,
                                             "SWLimitStopMode");
            AllocMemoryComboBoxSelectedValue(ref this.m_DicSWLimitSelection,
                                             ref this.m_BindingComboSWLimitSelection,
                                             "SWLimitSelection");
           
            AllocMemoryText(ref this.m_BindingTextSWLimitP,
                                             "SWLimitP");
            AllocMemoryText(ref this.m_BindingTextSWLimitN,
                                             "SWLimitN");
           

            m_BindingTextSWLimitP.FormattingEnabled = true;
            m_BindingTextSWLimitN.FormattingEnabled = true;
          
            m_BindingTextSWLimitP.FormatString = "F5";
            m_BindingTextSWLimitN.FormatString = "F5";
        }
        public void ClearAll()
        {
            ClearList();
            ClearWithoutList();
        }
        public void ClearList()
        {
            this.m_DicSWLimitUse.Clear();
            this.m_DicSWLimitStopMode.Clear();
            this.m_DicSWLimitSelection.Clear();           
        }
        public void ClearWithoutList()
        {
            SWLimitP = 0.0;
            SWLimitN = 0.0;
        }
        void AllocMemoryComboBoxSelectedValue<T,T1>(ref Dictionary<T, T1> a_Dic, ref Binding a_Binding, string BindingStr)
        {
            a_Dic = null;
            a_Binding = null;
            a_Dic = new Dictionary<T, T1>();
            a_Binding = new Binding("SelectedValue", this, BindingStr, false, DataSourceUpdateMode.OnPropertyChanged);
            a_Binding.BindingComplete += new BindingCompleteEventHandler(this.BingComplete);
        }
        void AllocMemoryText(ref Binding a_Binding, string BindingStr)
        {
            a_Binding = null;
            a_Binding = new Binding("Text", this, BindingStr, false, DataSourceUpdateMode.OnPropertyChanged, 0);
            a_Binding.BindingComplete += new BindingCompleteEventHandler(this.BingComplete);
        }
        protected void BingComplete(object sender, System.Windows.Forms.BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState != System.Windows.Forms.BindingCompleteState.Success)
            {
                Trace.WriteLine(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
                //System.Windows.Forms.MessageBox.Show(e.Binding.Control.Tag + "(" + e.Binding.Control.Text + ")" + ":" + e.ErrorText);
            }
        }
    }



}
