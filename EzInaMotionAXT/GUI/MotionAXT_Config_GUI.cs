using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Motion.AXT.GUI
{
    public partial class CMotionAXT_Config_GUI : UserControl
    {

        private CMotionAXT_PARAM_SETTING_GUI        m_ParamSetting;
        private CMotionAXT_SIGNAL_SETTING_GUI       m_SignalSetting;
        private CMotionAXT_SWLimit_SETTING_GUI      m_SWLimitSetting;
        private CMotionAXT_HOMESEARCH_SETTING_GUI   m_HomeSearchSetting;
        
        
        public CMotionAXT_Config_GUI()
        {
            InitializeComponent();
            m_ParamSetting=new CMotionAXT_PARAM_SETTING_GUI();
            m_SignalSetting = new CMotionAXT_SIGNAL_SETTING_GUI();
            m_SWLimitSetting=new CMotionAXT_SWLimit_SETTING_GUI();
            m_HomeSearchSetting=new CMotionAXT_HOMESEARCH_SETTING_GUI();                               
        }
        protected void ClearAll()
        {
            m_ParamSetting.ClearAll();
            m_SignalSetting.ClearAll();
        }
        protected void ClearBinding()
        {
            #region     Param
            
            
            cbParam_PulseOut.DataBindings.Clear();
           
            
            cboParam_Encoder.DataBindings.Clear();
            
            cboParam_AbsRel.DataBindings.Clear();
            
            cboParam_Profile.DataBindings.Clear();
            edtParam_MinVel.DataBindings.Clear();
            edtParam_MaxVel.DataBindings.Clear();
            edtParam_MovePulse.DataBindings.Clear();
            edtParam_MoveUnit.DataBindings.Clear();
            #endregion  Param
            #region     Signal
            
            cboSignal_ELimitN.DataBindings.Clear();
            
            cboSignal_ELimitP.DataBindings.Clear();
            
            cboSignal_StopLevel.DataBindings.Clear();
            
            cboSignal_EncoderType.DataBindings.Clear();
           
            cboSignal_StopMode.DataBindings.Clear();
           
            cboSignal_Alarm.DataBindings.Clear();
           
            cboSignal_ZPhaseLEV.DataBindings.Clear();
           
            cboSignal_ServoOn.DataBindings.Clear();
           
            cboSignal_AlarmReset.DataBindings.Clear();
           
            cboSignal_INP.DataBindings.Clear();
            #endregion  Signal
            #region SWLimit
           
            cboSWLimit_Use.DataBindings.Clear();
           
            cboSWLimit_Selection.DataBindings.Clear();
           
            cboSWLimit_StopMode.DataBindings.Clear();
            edtSWLimit_PosP.DataBindings.Clear();
            edtSWLimit_PosN.DataBindings.Clear();
            #endregion SWLimit
        }
        protected void ConnectBinding()
        {
            cbParam_PulseOut.DisplayMember="Value";
            cbParam_PulseOut.ValueMember="Key";
            cboParam_Encoder.DisplayMember="Value";
            cboParam_Encoder.ValueMember="Key";
            cboParam_AbsRel.DisplayMember="Value";
            cboParam_AbsRel.ValueMember="Key";
            cboParam_Profile.DisplayMember="Value";
            cboParam_Profile.ValueMember="Key";
            cboSignal_ELimitN.DisplayMember="Value";
            cboSignal_ELimitN.ValueMember="Key";
            cboSignal_ELimitP.DisplayMember="Value";
            cboSignal_ELimitP.ValueMember="Key";
            cboSignal_StopLevel.DisplayMember="Value";
            cboSignal_StopLevel.ValueMember="Key";
            cboSignal_EncoderType.DisplayMember="Value";
            cboSignal_EncoderType.ValueMember="Key";
            cboSignal_StopMode.DisplayMember="Value";
            cboSignal_StopMode.ValueMember="Key";
            cboSignal_Alarm.DisplayMember="Value";
            cboSignal_Alarm.ValueMember="Key";
            cboSignal_ZPhaseLEV.DisplayMember="Value";
            cboSignal_ZPhaseLEV.ValueMember="Key";
            cboSignal_ServoOn.DisplayMember="Value";
            cboSignal_ServoOn.ValueMember="Key";
            cboSignal_AlarmReset.DisplayMember="Value";
            cboSignal_AlarmReset.ValueMember="Key";
            cboSignal_INP.DisplayMember="Value";
            cboSignal_INP.ValueMember="Key";
            cboSWLimit_Use.DisplayMember="Value";
            cboSWLimit_Use.ValueMember="Key";
            cboSWLimit_Selection.DisplayMember="Value";
            cboSWLimit_Selection.ValueMember="Key";
            cboSWLimit_StopMode.DisplayMember="Value";
            cboSWLimit_StopMode.ValueMember="Key";
        }
        protected void InitBinding()
        {
           ClearBinding();
           #region     Param
            
            cbParam_PulseOut.DataSource = new BindingSource(m_ParamSetting.m_DicPulseMode, null);                                            
            cbParam_PulseOut.DataBindings.Add(m_ParamSetting.BindingComboPulseMode);      
            cboParam_Encoder.DataSource = new BindingSource(m_ParamSetting.m_DicEncoderInputMode, null);          
            cboParam_Encoder.DataBindings.Add(m_ParamSetting.BindingComboEncoderInputMode);
            cboParam_AbsRel.DataSource = new BindingSource(m_ParamSetting.m_DicAbsRelMode, null);
            cboParam_AbsRel.DataBindings.Add(m_ParamSetting.BindingComboAbsRelMode); 
            cboParam_Profile.DataSource = new BindingSource(m_ParamSetting.m_DicProfileMode, null);           
            cboParam_Profile.DataBindings.Add(m_ParamSetting.BindingComboProfileMode);
            edtParam_MinVel.DataBindings.Add(m_ParamSetting.BindingTextMinVel);
            edtParam_MaxVel.DataBindings.Add(m_ParamSetting.BindingTextMaxVel);
            edtParam_MovePulse.DataBindings.Add(m_ParamSetting.BindingTextPulse);
            edtParam_MoveUnit.DataBindings.Add(m_ParamSetting.BindingTextUnit);

            #endregion  Param            
           #region     Signal
            cboSignal_ELimitN.DataSource = new BindingSource(m_SignalSetting.m_DicNegativeActiveLevel, null);         
            cboSignal_ELimitN.DataBindings.Add(m_SignalSetting.BindingComboNegativeActiveLevel);
            cboSignal_ELimitP.DataSource = new BindingSource(m_SignalSetting.m_DicPositiveActiveLevel, null);          
            cboSignal_ELimitP.DataBindings.Add(m_SignalSetting.BindingComboPositiveActiveLevel);
            cboSignal_StopLevel.DataSource = new BindingSource(m_SignalSetting.m_DicEMGStopActiveLevel, null);           
            cboSignal_StopLevel.DataBindings.Add(m_SignalSetting.BindingComboEMGStopActiveLevel); 
            cboSignal_EncoderType.DataSource = new BindingSource(m_SignalSetting.m_DicEncoderType, null);           
            cboSignal_EncoderType.DataBindings.Add(m_SignalSetting.BindingComboEncoderType);
            cboSignal_StopMode.DataSource = new BindingSource(m_SignalSetting.m_DicEMGStopMode, null);           
            cboSignal_StopMode.DataBindings.Add(m_SignalSetting.BindingComboEMGStopMode);
            cboSignal_Alarm.DataSource = new BindingSource(m_SignalSetting.m_DicServoAlarmActiveLevel, null);            
            cboSignal_Alarm.DataBindings.Add(m_SignalSetting.BindingComboServoAlarmActiveLevel);            
            cboSignal_ZPhaseLEV.DataSource = new BindingSource(m_SignalSetting.m_DicZPhaseActiveLevel, null);            
            cboSignal_ZPhaseLEV.DataBindings.Add(m_SignalSetting.BindingComboZPhaseActiveLevel);
            cboSignal_ServoOn.DataSource = new BindingSource(m_SignalSetting.m_DicServoOnActiveLevel, null);          
            cboSignal_ServoOn.DataBindings.Add(m_SignalSetting.BindingComboServoOnActiveLevel);
            cboSignal_AlarmReset.DataSource = new BindingSource(m_SignalSetting.m_DicServoAlarmResetActiveLevel, null);          
            cboSignal_AlarmReset.DataBindings.Add(m_SignalSetting.BindingComboServoAlarmResetActiveLevel);
            cboSignal_INP.DataSource = new BindingSource(m_SignalSetting.m_DicInPosActiveLevel, null);           
            cboSignal_INP.DataBindings.Add(m_SignalSetting.BindingComboInPosActiveLevel);
            #endregion  Signal
           #region SWLimit
            cboSWLimit_Use.DataSource = new BindingSource(m_SWLimitSetting.m_DicSWLimitUse, null);          
            cboSWLimit_Use.DataBindings.Add(m_SWLimitSetting.BindingComboSWLimitUse);
            cboSWLimit_Selection.DataSource = new BindingSource(m_SWLimitSetting.m_DicSWLimitSelection, null);           
            cboSWLimit_Selection.DataBindings.Add(m_SWLimitSetting.BindingComboSWLimitSelection);
            cboSWLimit_StopMode.DataSource = new BindingSource(m_SWLimitSetting.m_DicSWLimitStopMode, null);          
            cboSWLimit_StopMode.DataBindings.Add(m_SWLimitSetting.BindingComboSWLimitStopMode);
            edtSWLimit_PosP.DataBindings.Add(m_SWLimitSetting.BindingTextSWLimitP);
            edtSWLimit_PosN.DataBindings.Add(m_SWLimitSetting.BindingTextSWLimitN);
            #endregion SWLimit 

           ConnectBinding();      
      }


        public void InitControl(EzIna.Motion.MotionInterface a_pItem)
        {
            //if(a_pItem!=null)
           // {

           //     if(a_pItem.BaseType.Name!="CMotionAXT")
           //     {
           //         return;
           //     }
           if(a_pItem==null)
                return;
           if(a_pItem.DeviceType.Name==typeof(CMotionAXT).Name)
           {
                MotionAXTItem pItem=a_pItem as MotionAXTItem;
                CAXT_AxisModuleInfo pAxisInfor = null;
                pAxisInfor = CMotionAXT.GetAxisInformation(pItem.iAxisNo);
                InitializeControl(pAxisInfor.iModuleID);//pAxisInfor.iModuleID);
                m_ParamSetting.SettingValue=pItem.m_stParamSetting;                
                m_SignalSetting.SettingValue=pItem.m_stSignalSetting;
                m_SWLimitSetting.SettingValue=pItem.m_stSWLimitSetting;
                m_HomeSearchSetting.SettingValue=pItem.m_stHomeSearchSetting;
            }
                                   
        }              
        protected void InitializeControl(uint a_ModouleType)
        {
           
            switch (a_ModouleType)
            {
                
                case (uint)AXT_MODULE.AXT_SMC_4V04:
                case (uint)AXT_MODULE.AXT_SMC_R1V04:
                case (uint)AXT_MODULE.AXT_SMC_2V04:
                case (uint)AXT_MODULE.AXT_SMC_R1V04PM2Q:
                case (uint)AXT_MODULE.AXT_SMC_R1V04PM2QE:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIPM:
                    {
                        ClearAll();
                        // Param
                        #region     PulseMode                      
                        m_ParamSetting.m_DicPulseMode.Add(0, "00:OneHighLowHigh");
                        m_ParamSetting.m_DicPulseMode.Add(1, "01:OneHighHighLow");
                        m_ParamSetting.m_DicPulseMode.Add(2, "02:OneLowLowHigh");
                        m_ParamSetting.m_DicPulseMode.Add(3, "03:OneLowHighLow");
                        m_ParamSetting.m_DicPulseMode.Add(4, "04:TwoCcwCwHigh");
                        m_ParamSetting.m_DicPulseMode.Add(5, "05:TwoCcwCwLow");
                        m_ParamSetting.m_DicPulseMode.Add(6, "06:TwoCwCcwHigh");
                        m_ParamSetting.m_DicPulseMode.Add(7, "07:TwoCwCcwLow");
                        m_ParamSetting.m_DicPulseMode.Add(8, "08:TwoPhase");
                        m_ParamSetting.m_DicPulseMode.Add(9, "09:TwoPhaseReverse");
                        #endregion  PulseMode
                        #region     AbsRelMode
                        m_ParamSetting.m_DicAbsRelMode.Add(0, "00:POS_ABS_MODE");
                        m_ParamSetting.m_DicAbsRelMode.Add(1, "01:POS_REL_MODE");
                        #endregion  AbsRelMode
                        #region     Profile 
                        m_ParamSetting.m_DicProfileMode.Add(0,"00:SYTRAPEZOIDE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(1,"01:ASYTRAPEZOIDE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(2,"02:QUASI_S_CURVE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(3,"03:SYS_CURVE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(4,"04:ASYS_CURVE_MODE");
                        #endregion  Profile
                        #region     EncoderInputMode                      
                        m_ParamSetting.m_DicEncoderInputMode.Add(0,"00:ObverseUpDownMode");
                        m_ParamSetting.m_DicEncoderInputMode.Add(1,"01:ObverseSqr1Mode");
                        m_ParamSetting.m_DicEncoderInputMode.Add(2,"02:ObverseSqr2Mode");
                        m_ParamSetting.m_DicEncoderInputMode.Add(3,"03:ObverseSqr4Mode");
                        m_ParamSetting.m_DicEncoderInputMode.Add(4,"04:ReverseUpDownMode");
                        m_ParamSetting.m_DicEncoderInputMode.Add(5,"05:ReverseSqr1Mode");
                        m_ParamSetting.m_DicEncoderInputMode.Add(6,"06:ReverseSqr2Mode");
                        m_ParamSetting.m_DicEncoderInputMode.Add(7,"07:ReverseSqr4Mode");
                        #endregion  EncoderInputMode
                        

                        // Signal
                        #region     ZPhaseActiveLevel                   
                        m_SignalSetting.m_DicZPhaseActiveLevel.Add(0,"00:LOW");
                        m_SignalSetting.m_DicZPhaseActiveLevel.Add(1,"01:HIGH");
                        #endregion  ZPhaseActiveLevel       
                        #region     InPositionLevel
                      
                       m_SignalSetting.m_DicInPosActiveLevel.Add(0,"00:LOW");
                       m_SignalSetting.m_DicInPosActiveLevel.Add(1,"01:HIGH");
                       m_SignalSetting.m_DicInPosActiveLevel.Add(2,"02:UNUSED");

                        #endregion  InPositionLevel                      
                        #region     EMGStopMode                   
                        m_SignalSetting.m_DicEMGStopMode.Add(0,"00:EMERGENCY");
                        m_SignalSetting.m_DicEMGStopMode.Add(1,"01:SLOWDOWN");
                        #endregion  EMGStopMode                      
                        #region     EMGStopActiveLevel                       
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(0,"00:LOW");
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(1,"01:HIGH");
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(2,"02:UNUSED");
                        #endregion  EMGStopActiveLevel                     
                        #region     PositiveActiveLevel
                     
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(0,"00:LOW");
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(1,"01:HIGH");
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(2,"02:UNUSED");
                        #endregion  PositiveActiveLevel                  
                        #region     NegativeActiveLevel

                        m_SignalSetting.m_DicNegativeActiveLevel.Add(0, "00:LOW");
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(2, "02:UNUSED");
                        #endregion  NegativeActiveLevel
                        #region     ServoOnActiveLevel
                        m_SignalSetting.m_DicServoOnActiveLevel.Add(0, "00:LOW");
                        m_SignalSetting.m_DicServoOnActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoOnActiveLevel

                        #region     ServoAlarmActiveLevel                    
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(0,"00:LOW");
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(1,"01:HIGH");
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(2,"02:UNUSED");
                        #endregion  ServoAlarmActiveLevel
                        #region     ServoAlarmResetActiveLevel
                        m_SignalSetting.m_DicServoAlarmResetActiveLevel.Add(0,"00:LOW");
                        m_SignalSetting.m_DicServoAlarmResetActiveLevel.Add(1,"01:HIGH");
                        #endregion  ServoAlarmResetActiveLevel
                        #region     EncoderType
                        m_SignalSetting.m_DicEncoderType.Add(0,"00:INCREMENTAL");
                        m_SignalSetting.m_DicEncoderType.Add(1,"01:ABSOLUTE");
                        m_SignalSetting.m_DicEncoderType.Add(2,"02:STEPPER");
                        #endregion  EncoderType
                        // SW Limit
                        #region     SWLimitUse
                        m_SWLimitSetting.m_DicSWLimitUse.Add(0,"00:UNUSED");
                        m_SWLimitSetting.m_DicSWLimitUse.Add(1,"01:USED");
                        #endregion  SWLimitUse
                        #region     SWLimitStopMode
                        m_SWLimitSetting.m_DicSWLimitStopMode.Add(0, "00:ESTOP");
                        m_SWLimitSetting.m_DicSWLimitStopMode.Add(1, "01:SDSTOP");
                        #endregion  SWLimitStopMode
                        #region     SWLimitSelection
                        m_SWLimitSetting.m_DicSWLimitSelection.Add(0, "00:CMDPOS");
                        m_SWLimitSetting.m_DicSWLimitSelection.Add(1, "01:ACTPOS");
                        #endregion  SWLimitSelection

                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0,"00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1,"01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4,"04:HomeSensor");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(5,"05:EncodZPhase");
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(0,"00:LOW");
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1,"01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0,"00:UNUSED");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(1,"01:(+)DIR_CW");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(2,"02:(-)DIR_CCW");
                        #endregion  ZPhaseUse
                        InitBinding();
                    }
                    break;        
                case (uint)AXT_MODULE.AXT_SMC_R1V04A4:
                    {
                        ClearAll();
                        // Param
                        #region     PulseMode                      
                        m_ParamSetting.m_DicPulseMode.Add(0, "00:OneHighLowHigh");
                        #endregion  PulseMode
                        #region     AbsRelMode
                        m_ParamSetting.m_DicAbsRelMode.Clear();
                        m_ParamSetting.m_DicAbsRelMode.Add(0, "00:POS_ABS_MODE");
                        m_ParamSetting.m_DicAbsRelMode.Add(1, "01:POS_REL_MODE");
                        #endregion  AbsRelMode
                        #region     Profile
                        m_ParamSetting.m_DicProfileMode.Add(0, "00:SYTRAPEZOIDE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(1, "01:ASYTRAPEZOIDE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(2, "02:QUASI_S_CURVE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(3, "03:SYS_CURVE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(4, "04:ASYS_CURVE_MODE");

                        #endregion  Profile
                        #region     EncoderInputMode                      
                        m_ParamSetting.m_DicEncoderInputMode.Add(0, "00:ObverseUpDownMode");
                        #endregion  EncoderInputMode
                        // Signal
                        #region     ZPhaseActiveLevel                   
                        m_SignalSetting.m_DicZPhaseActiveLevel.Add(0, "00:LOW");
                        m_SignalSetting.m_DicZPhaseActiveLevel.Add(1, "01:HIGH");
                        #endregion  ZPhaseActiveLevel       
                        #region     InPositionLevel
                        m_SignalSetting.m_DicInPosActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicInPosActiveLevel.Add(2, "02:UNUSED");
                        #endregion  InPositionLevel                      
                        #region     EMGStopMode                   
                        m_SignalSetting.m_DicEMGStopMode.Add(0, "00:EMERGENCY");
                        m_SignalSetting.m_DicEMGStopMode.Add(1, "01:SLOWDOWN");
                        #endregion  EMGStopMode                      
                        #region     EMGStopActiveLevel                       
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(0, "00:LOW");                        
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(2, "02:UNUSED");
                        #endregion  EMGStopActiveLevel                     
                        #region     PositiveActiveLevel
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(0, "00:LOW");                        
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(2, "02:UNUSED");
                        #endregion  PositiveActiveLevel                  
                        #region     NegativeActiveLevel
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(0, "00:LOW");                       
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(2, "02:UNUSED");
                        #endregion  NegativeActiveLevel
                        #region     ServoOnActiveLevel
                        m_SignalSetting.m_DicServoOnActiveLevel.Add(0, "00:LOW");
                        m_SignalSetting.m_DicServoOnActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoOnActiveLevel

                        #region     ServoAlarmActiveLevel                                            
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(2, "02:UNUSED");
                        #endregion  ServoAlarmActiveLevel
                        #region     ServoAlarmResetActiveLevel                        
                        m_SignalSetting.m_DicServoAlarmResetActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoAlarmResetActiveLevel
                        #region     EncoderType
                        m_SignalSetting.m_DicEncoderType.Add(0, "00:INCREMENTAL");                  
                        #endregion  EncoderType
                 

                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0, "00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1, "01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(5, "05:EncodZPhase");
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(0, "00:LOW");
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");                    
                        #endregion  ZPhaseUse
                        InitBinding();
                    }
                    break;
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIPM:
                    {
                        ClearAll();
                        // Param
                        #region     PulseMode                      
                        m_ParamSetting.m_DicPulseMode.Add(0, "00:OneHighLowHigh");                   
                        m_ParamSetting.m_DicPulseMode.Add(6, "06:TwoCwCcwHigh");                       
                        #endregion  PulseMode
                        #region     AbsRelMode
                        m_ParamSetting.m_DicAbsRelMode.Add(0, "00:POS_ABS_MODE");
                        m_ParamSetting.m_DicAbsRelMode.Add(1, "01:POS_REL_MODE");
                        #endregion  AbsRelMode
                        #region     Profile 
                        m_ParamSetting.m_DicProfileMode.Add(0, "00:SYTRAPEZOIDE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(1, "01:ASYTRAPEZOIDE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(2, "02:QUASI_S_CURVE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(3, "03:SYS_CURVE_MODE");
                        m_ParamSetting.m_DicProfileMode.Add(4, "04:ASYS_CURVE_MODE");
                        #endregion  Profile
                        #region     EncoderInputMode                      
                        m_ParamSetting.m_DicEncoderInputMode.Add(0, "00:ObverseUpDownMode");
                        #endregion  EncoderInputMode


                        // Signal
                        #region     ZPhaseActiveLevel                   
                        m_SignalSetting.m_DicZPhaseActiveLevel.Add(2, "02:UNUSED");
                        #endregion  ZPhaseActiveLevel       
                        #region     InPositionLevel                        
                        m_SignalSetting.m_DicInPosActiveLevel.Add(2, "02:UNUSED");

                        #endregion  InPositionLevel                      
                        #region     EMGStopMode                   
                        m_SignalSetting.m_DicEMGStopMode.Add(0, "00:EMERGENCY");                        
                        #endregion  EMGStopMode                      
                        #region     EMGStopActiveLevel                       
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(2, "02:UNUSED");
                        #endregion  EMGStopActiveLevel                     
                        #region     PositiveActiveLevel
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(2, "02:UNUSED");
                        #endregion  PositiveActiveLevel                  
                        #region     NegativeActiveLevel
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(2, "02:UNUSED");
                        #endregion  NegativeActiveLevel
                        #region     ServoOnActiveLevel                        
                        m_SignalSetting.m_DicServoOnActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoOnActiveLevel

                        #region     ServoAlarmActiveLevel                    
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(2, "02:UNUSED");
                        #endregion  ServoAlarmActiveLevel
                        #region     ServoAlarmResetActiveLevel                        
                        m_SignalSetting.m_DicServoAlarmResetActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoAlarmResetActiveLevel
                        #region     EncoderType
                        m_SignalSetting.m_DicEncoderType.Add(0, "00:INCREMENTAL");
                        #endregion  EncoderType
               

                        //Home
                        #region     HomeSignal                        
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");                        
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel                       
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");
                        #endregion  ZPhaseUse
                        InitBinding();
                    }
                    break;

                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIISV:
                case (uint)AXT_MODULE.AXT_SMC_R1V04A5:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICL:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICR:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIORI:
                case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV:
                case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV_R:
                    {
                        ClearAll();
                        // Param
                        #region     PulseMode                      
                        m_ParamSetting.m_DicPulseMode.Add(0, "00:OneHighLowHigh");
                        #endregion  PulseMode
                        #region     AbsRelMode
                        m_ParamSetting.m_DicAbsRelMode.Add(0, "00:POS_ABS_MODE");
                        m_ParamSetting.m_DicAbsRelMode.Add(1, "01:POS_REL_MODE");
                        #endregion  AbsRelMode
                     
                        #region     EncoderInputMode                      
                        m_ParamSetting.m_DicEncoderInputMode.Add(0, "00:ObverseUpDownMode");
                        #endregion  EncoderInputMode


                        // Signal
                        #region     ZPhaseActiveLevel                   
                        m_SignalSetting.m_DicZPhaseActiveLevel.Add(2, "02:UNUSED");
                        #endregion  ZPhaseActiveLevel       
                        #region     InPositionLevel                        
                        m_SignalSetting.m_DicInPosActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicInPosActiveLevel.Add(2, "02:UNUSED");

                        #endregion  InPositionLevel                      
                        #region     EMGStopMode                   
                        m_SignalSetting.m_DicEMGStopMode.Add(0, "00:EMERGENCY");                        
                        #endregion  EMGStopMode                      
                        #region     EMGStopActiveLevel                                               
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(2, "02:UNUSED");
                        #endregion  EMGStopActiveLevel                     
                        #region     PositiveActiveLevel                        
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(2, "02:UNUSED");
                        #endregion  PositiveActiveLevel                  
                        #region     NegativeActiveLevel                       
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(2, "02:UNUSED");
                        #endregion  NegativeActiveLevel
                        #region     ServoOnActiveLevel                        
                        m_SignalSetting.m_DicServoOnActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoOnActiveLevel


                        #region     ServoAlarmActiveLevel                                            
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(2, "02:UNUSED");
                        #endregion  ServoAlarmActiveLevel
                        #region     ServoAlarmResetActiveLevel                        
                        m_SignalSetting.m_DicServoAlarmResetActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoAlarmResetActiveLevel

                        #region     EncoderType
                        m_SignalSetting.m_DicEncoderType.Add(0, "00:INCREMENTAL");
                        m_SignalSetting.m_DicEncoderType.Add(1, "01:ABSOLUTE");
                       
                        #endregion  EncoderType
                   

                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0, "00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1, "01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");                        
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel                       
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(1, "01:(+)DIR_CW");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(2, "02:(-)DIR_CCW");
                        #endregion  ZPhaseUse
                        InitBinding();
                    }
                    break;
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIISV:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIISV_MD:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIS7S:
                case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIS7W:
                    {
                        ClearAll();
                        // Param
                        #region     PulseMode                      
                        m_ParamSetting.m_DicPulseMode.Add(0, "00:OneHighLowHigh");
                        #endregion  PulseMode
                

                        #region     EncoderInputMode                      
                        m_ParamSetting.m_DicEncoderInputMode.Add(0, "00:ObverseUpDownMode");
                        #endregion  EncoderInputMode


                        // Signal
                        #region     ZPhaseActiveLevel                   
                        m_SignalSetting.m_DicZPhaseActiveLevel.Add(2, "02:UNUSED");
                        #endregion  ZPhaseActiveLevel       
                        #region     InPositionLevel                        
                        m_SignalSetting.m_DicInPosActiveLevel.Add(0, "01:HIGH");
                        m_SignalSetting.m_DicInPosActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicInPosActiveLevel.Add(2, "02:UNUSED");

                        #endregion  InPositionLevel                      
                        #region     EMGStopMode                   
                        m_SignalSetting.m_DicEMGStopMode.Add(0, "00:EMERGENCY");
                        #endregion  EMGStopMode                      
                        #region     EMGStopActiveLevel                                                                       
                        m_SignalSetting.m_DicEMGStopActiveLevel.Add(2, "02:UNUSED");
                        #endregion  EMGStopActiveLevel                     
                        #region     PositiveActiveLevel                        
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(0, "01:LOW");
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicPositiveActiveLevel.Add(2, "02:UNUSED");
                        #endregion  PositiveActiveLevel                  
                        #region     NegativeActiveLevel                
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(0, "01:LOW");       
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicNegativeActiveLevel.Add(2, "02:UNUSED");
                        #endregion  NegativeActiveLevel
                        #region     ServoOnActiveLevel                        
                        m_SignalSetting.m_DicServoOnActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoOnActiveLevel


                        #region     ServoAlarmActiveLevel                                            
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(0, "00:LOW");
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(1, "01:HIGH");
                        m_SignalSetting.m_DicServoAlarmActiveLevel.Add(2, "02:UNUSED");
                        #endregion  ServoAlarmActiveLevel
                        #region     ServoAlarmResetActiveLevel                        
                        m_SignalSetting.m_DicServoAlarmResetActiveLevel.Add(1, "01:HIGH");
                        #endregion  ServoAlarmResetActiveLevel

                        #region     EncoderType
                        m_SignalSetting.m_DicEncoderType.Add(0, "00:INCREMENTAL");
                        m_SignalSetting.m_DicEncoderType.Add(1, "01:ABSOLUTE");

                        #endregion  EncoderType


                        //Home
                        #region     HomeSignal
                        m_HomeSearchSetting.m_DicHomeSignal.Add(0, "00:PosEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(1, "01:NegEndLimit");
                        m_HomeSearchSetting.m_DicHomeSignal.Add(4, "04:HomeSensor");
                        #endregion  HomeSignal
                        #region     HomeSignalActiveLevel                       
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(0, "00:LOW");
                        m_HomeSearchSetting.m_DicHomeSignalActiveLevel.Add(1, "01:HIGH");
                        #endregion  HomeSignalActiveLevel
                        #region     HomeDir
                        m_HomeSearchSetting.m_DicHomeDir.Add(0, "00:(-)DIR_CCW");
                        m_HomeSearchSetting.m_DicHomeDir.Add(1, "01:(+)DIR_CW");
                        #endregion  HomeDir
                        #region     ZPhaseUse
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(0, "00:UNUSED");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(1, "01:(+)DIR_CW");
                        m_HomeSearchSetting.m_DicHomeZPhaseUse.Add(2, "02:(-)DIR_CCW");
                        #endregion  ZPhaseUse
                        InitBinding();
                    }
                    break;
                default:
                    {
                        ClearAll();
                    }
                    break;
            }

                 
        }
        



    }
}
