using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using EzIna.Scanner;
using System.IO;

namespace EzIna.Scanner
{
    [Serializable]
    public partial class ScanlabRTC5LinkData:XmlSerializer
    {
        public ScanlabRTC5LinkData()
        {
            InitializeParam();
        }
        public ScanlabRTC5LinkData(string a_strName)
        {
            InitializeParam();
            
        }
        protected void InitializeParam()
        {
  
            m_iCardNo = 0;
            m_iHeadACorrTableNum = 0;
            m_iHeadBCorrTableNum = 0;
          
            m_strCorrectionTableFileNames=new string[4];
            m_iCorrectionDimemsion=new uint[4];
            m_bEnableHeadA=true;
            m_bEnableHeadB=false;
            m_iList1stMemorySize = 1000;
            m_iList2ndMemorySize = 1000;

            m_bIntelliscan = false;
            m_iPolygonDelay = 0;
            m_iDirectMove3D = 0;
            m_iEdgeLevel = 0;
            m_iMinimumJumpDelay = 0;
            m_iJumpLengthLimit = uint.MaxValue;

            m_iLaserMode = 0;

            m_iFirstPulseKillerLen = 0;
            m_iQSwitchDelay = 0;
            m_iStandbyHalfPeriod = 0;
            m_iStandbyPulseLength = 0;
            m_iAngleCompensation = 0;
            m_iSkyWritingMode = 3;
            m_iSkyWritingTimelag = 320;
            m_iSkyWritingLaserOnShift = 0;
            m_iSkyWritingNPrev = 48;
            m_iSkyWritingNPost = 32;
            m_iSkyWritingLimit = 0;
            m_iGalvaMirrorMinTemp = 350;
            m_iGalvaMirrorMaxTemp = 400;
            m_iServoBoardMinTemp = 400;
            m_iServoBoardMaxTemp = 600;
            m_fXPosScale=1.0;
            m_fYPosScale=1.0;
            m_fScale=1.0;
            m_iHalfPeriod=0;
            m_iPulseLength=0;
            m_fJumpSpeed=100000;
            m_fMarkSpeed=10000;
            m_LaserCtrlValue = new BitField32Helper();
            m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.ERR_AUTO_MONITORING] = false;
        }
        public void InitializeFunction(string a_strPGMfilePath="")
        {   
            RTC5Import.RTC5Wrap.select_rtc(m_iCardNo);
            RTC5Import.RTC5Wrap.n_load_program_file(m_iCardNo,string.IsNullOrEmpty(a_strPGMfilePath)? null:a_strPGMfilePath);//ScanlabRTC5.ConfigScanLabFilePath);           
            FileInfo FileInfor;
            for (int i = 0; i < m_strCorrectionTableFileNames.Length; i++)
            {
                if(string.IsNullOrWhiteSpace(m_strCorrectionTableFileNames[i])==false)
                {
                    FileInfor=new FileInfo(ScanlabRTC5.ConfigScanlabFolderPath+m_strCorrectionTableFileNames[i]);
                    if (FileInfor.Exists == true)
                    {
                        RTC5Import.RTC5Wrap.n_load_correction_file(m_iCardNo,FileInfor.FullName,(uint)i+1,m_iCorrectionDimemsion[i]);
                    } 
                }                
            }
            RTC5Import.RTC5Wrap.set_jump_speed_ctrl(m_fJumpSpeed);
            RTC5Import.RTC5Wrap.set_mark_speed_ctrl(m_fMarkSpeed);
             
        }

        /// <summary>
        /// Must call when param Value Changed
        /// </summary>
        /// 
        public void DoChanged()
        {
           
            RTC5Import.RTC5Wrap.n_select_cor_table(m_iCardNo, m_bEnableHeadA == true ? m_iHeadACorrTableNum : 0, m_bEnableHeadB == true ? m_iHeadBCorrTableNum : 0);
            RTC5Import.RTC5Wrap.n_set_laser_control(m_iCardNo, m_LaserCtrlValue.Data);
            RTC5Import.RTC5Wrap.n_enable_laser(m_iCardNo);
            RTC5Import.RTC5Wrap.n_config_list(m_iCardNo, m_iList1stMemorySize, m_iList2ndMemorySize);
            RTC5Import.RTC5Wrap.n_set_laser_mode(m_iCardNo, m_iLaserMode); 
            RTC5Import.RTC5Wrap.n_set_angle(m_iCardNo, 1, m_iAngleCompensation, 1);
            RTC5Import.RTC5Wrap.n_set_qswitch_delay(m_iCardNo, m_iQSwitchDelay);
            RTC5Import.RTC5Wrap.n_set_firstpulse_killer(m_iCardNo, m_iFirstPulseKillerLen);
            RTC5Import.RTC5Wrap.n_set_standby(m_iCardNo, m_iStandbyHalfPeriod, m_iStandbyPulseLength);
            //RTC5Import.RTC5Wrap.n_set_laser_delays(m_iCardNo,m_iLaserOnDelay,m_iLaserOffDelay);
            //RTC5Import.RTC5Wrap.n_set_laser_pulses_ctrl(m_iCardNo,m_iHalfPeriod,m_iPulseWidth);                                
            //RTC5Import.RTC5Wrap.n_set_sky_writing_mode(m_iCardNo, m_iSkyWritingMode);
            //RTC5Import.RTC5Wrap.n_set_sky_writing_para(m_iCardNo, m_iSkyWritingTimelag, m_iSkyWritingLaserOnShift, m_iSkyWritingNPrev, m_iSkyWritingNPost);
            //RTC5Import.RTC5Wrap.n_set_sky_writing_limit(m_iCardNo, Math.Cos(m_iSkyWritingLimit * Math.PI / 180.0));
        }    
        public string strID
        {
            get { return m_strID;}
            set
            {
                m_strID=value;
            }
        }
        /// <summary>
        /// Input Only File Name 
        /// <para>Path Fixed ../system/CFG/Scanner/RCT5/ </para>
        /// </summary>
        public string CorrTable1FileName
        {
            get { return m_strCorrectionTableFileNames[0];}
            set
            {
                m_strCorrectionTableFileNames[0]=value;
            }
        }
        /// <summary>
        /// Input Only File Name 
        /// <para>Path Fixed ../system/CFG/Scanner/RCT5/ </para>
        /// </summary>
        public string CorrTable2FileName
        {
            get { return m_strCorrectionTableFileNames[1];}
            set
            {
                m_strCorrectionTableFileNames[1] = value;
            }
        }
        /// <summary>
        /// Input Only File Name 
        /// <para>Path Fixed ../system/CFG/Scanner/RCT5/ </para>
        /// </summary>
        public string CorrTable3FileName
        {
            get { return m_strCorrectionTableFileNames[2]; }
            set
            {
                m_strCorrectionTableFileNames[2] = value;
            }
        }
        /// <summary>
        /// Input Only File Name 
        /// <para>Path Fixed ../system/CFG/Scanner/RCT5/ </para>
        /// </summary>
        public string CorrTable4FileName
        {
            get { return m_strCorrectionTableFileNames[3]; }
            set
            {
                m_strCorrectionTableFileNames[3] = value;
            }
        }
        public uint CorrTableNum_Head_A
        {
            get {return m_iHeadACorrTableNum; }
            set {m_iHeadACorrTableNum=value; DoChanged();}
        }
        public uint CorrTableNum_Head_B
        {
            get {return m_iHeadBCorrTableNum; }
            set {m_iHeadBCorrTableNum=value; DoChanged();}
        }
        public uint CardNo
        {
            get { return m_iCardNo;}
            set
            {
                if (m_iCardNo != value && 0 <= value && value < 32 && m_iCardNo.Equals(value)==false)
                {
                    m_iCardNo=value;
                }
            }
        }               
        public uint List_1stMemorySize
        {
            get { return m_iList1stMemorySize;}
            set
            {
                if (m_iList1stMemorySize != value && 0 <= value && value + m_iList2ndMemorySize <= 1048576 /* 2^20 */)
                {
                    m_iList1stMemorySize = value;
                    DoChanged();
                    //Change();
                }
            }
        }
        public uint List_2ndMemorySize
        {
            get { return m_iList2ndMemorySize; }
            set
            {
                if (m_iList2ndMemorySize != value && 0 <= value && value + m_iList1stMemorySize <= 1048576 /* 2^20 */)
                {
                    m_iList2ndMemorySize = value;
                    DoChanged();
                    //Change();
                }
            }
        }
        public bool IntelliScanMode
        {
            get{ return m_bIntelliscan;}
            set
            {
                if(m_bIntelliscan!=value )
                {
                    m_bIntelliscan=value;
                }
            }
        }

				/// <summary>
				/// mm or bit /sec
				/// </summary>
		public double JumpSpeed
        {
            get { return m_fScale>0?(m_fJumpSpeed/m_fScale)*1000 : m_fJumpSpeed*1000;}
            set
            {
                double fInput = m_fScale > 0 ? (value * m_fScale)/1000 : value/1000;
                if (m_fJumpSpeed != fInput &&
                    fInput >= ScanlabRTC5.MIN_JUMP_MARK_SPEED &&
                    fInput <= ScanlabRTC5.MAX_JUMP_MARK_SPEED)
                {
                    m_fJumpSpeed = fInput;
                }
            }
        }
				/// <summary>
				/// bit / ms
				/// </summary>
        public double OrginJumpSpeed
        {
            get { return m_fJumpSpeed;}
            set
            {
                if (m_fJumpSpeed != value &&
                   value >= ScanlabRTC5.MIN_JUMP_MARK_SPEED &&
                   value <= ScanlabRTC5.MAX_JUMP_MARK_SPEED)
                {
                    m_fJumpSpeed = value;
                }

            }
        }

				/// <summary>
				/// mm or bit /sec
				/// </summary>
        public double MarkSpeed
        {
            get { return m_fScale>0?(m_fMarkSpeed/m_fScale)*1000 : m_fMarkSpeed*1000;}
            set
            {
                double fInput =m_fScale>0?(value*m_fScale)/1000:value/1000;
                if (m_fMarkSpeed != fInput && 
                    fInput >= ScanlabRTC5.MIN_JUMP_MARK_SPEED && 
                    fInput <= ScanlabRTC5.MAX_JUMP_MARK_SPEED)
                {
                    m_fMarkSpeed = fInput;                    
                }                
            }
        }
        public double OrginMarkSpeed
        {
            get { return m_fMarkSpeed; }
            set
            {
                if (m_fMarkSpeed != value &&
                   value >= ScanlabRTC5.MIN_JUMP_MARK_SPEED &&
                   value <= ScanlabRTC5.MAX_JUMP_MARK_SPEED)
                {
                    m_fMarkSpeed = value;
                }
            }
        }
        /// <summary>
        /// Set_delay_mode
        /// </summary>
        public bool IsPolygonDelayEnable
        {
            get { return m_iPolygonDelay>0;}
        
        }

        #region PolygonDelay  ( set_delay_mode , set_scannder_delays)
        /// <summary>
        /// set_delay_mode ,set_scannder_delays        
        /// <para>unit :us </para>
        /// </summary>      
        public double PolygonDelay
        {
            get {return m_iPolygonDelay/10.0; }
            set
            {
                uint iInput=(uint)(value*10.0);
                if(m_iPolygonDelay!=iInput && iInput>=0 && iInput< uint.MaxValue )
                {
                    m_iPolygonDelay=iInput;
                    DoChanged();
                }
            }
        }
        public static double MinPolyonDelay
        {
            get { return 0.0; }
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays 
        /// <para>unit :us </para>
        /// </summary>      
        public static double MaxPolyonDelay
        {
            get { return (uint.MaxValue - 1) / 10.0; }
        }
        public static uint MinOrginPolyonDelay
        {
            get { return 0; }
        }
        public static uint MaxOrginPolyonDelay
        {
            get { return (uint.MaxValue - 1);}
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays ,  1= 1/10 us   
        /// <para>unit :us </para>
        /// </summary>        
        public uint OrginPolygonDelay
        {
            get { return m_iPolygonDelay; }
        }

        #endregion PolygonDelay  ( set_delay_mode , set_scannder_delays)

        #region JumpDelay ( set_scanner_delays )
        /// <summary>
        /// set_scannder_delays   , 1/10 us
        /// <para>unit :us </para>
        /// </summary>          
        public double JumpDelay
        {
            get { return m_iJumpDelay/10.0; }
            set
            {
                uint iInput = (uint)(value * 10.0);
                if (m_iJumpDelay != iInput && iInput >= 0 && iInput < uint.MaxValue)
                {
                    m_iJumpDelay = iInput;
                    DoChanged();
                }
            }
        }
        public double MinJumpDelay
        {
            get { return Min_MinimumJumpDelay; }
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays 
        /// <para>unit :us </para>
        /// </summary>      
        public double MaxJumpDelay
        {
            get { return (uint.MaxValue - 1) / 10.0; }
        }
        public uint MinOrginJumpDelay
        {
            get { return Min_OrginMinimumJumpDelay; }
        }
        public uint MaxOrginJumpDelay
        {
            get { return (uint.MaxValue - 1); }
        }
        public uint  OrginJumpDelay
        {
            get { return m_iJumpDelay; }         
        }

        #endregion JumpDelay ( set_scanner_delays )

        #region MarkDelay ( set_scanner_delays )
        /// <summary>
        /// set_scannder_delays   , 1/10 us
        /// <para>unit :us </para>
        /// </summary>          
        public double MarkDelay
        {
            get { return m_iMarkDelay / 10.0; }
            set
            {
                uint iInput = (uint)(value * 10.0);
                if (m_iMarkDelay != iInput && iInput >= 0 && iInput < uint.MaxValue)
                {
                    m_iMarkDelay = iInput;
                    DoChanged();
                }
            }
        }
        public double MinMarkDelay
        {
            get { return 0.0; }
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays 
        /// <para>unit :us </para>
        /// </summary>      
        public double MaxMarkDelay
        {
            get { return (uint.MaxValue - 1) / 10.0; }
        }
        public uint MinOrginMarkDelay
        {
            get { return 0; }
        }
        public uint MaxOrginMarkDelay
        {
            get { return (uint.MaxValue - 1); }
        }
        public uint OrginMarkDelay
        {
            get { return m_iMarkDelay; }
        }

        #endregion MarkDelay ( set_scanner_delays )


        /// <summary>
        /// set_delay_mode
        /// </summary>
        public uint DirectMove3D
        {
            get {return m_iDirectMove3D; }
            set
            {
                if (m_iDirectMove3D != value && UInt32.MinValue <= value && value < UInt32.MaxValue)
                {
                    m_iDirectMove3D = value; 
                    DoChanged();                
                }
            }
        }
        #region EdgeLevel ( set_delay_mode )
        /// <summary>
        /// set_delay_mode  , 1/10 us
        /// <para>unit :us </para>
        /// </summary>          
        public double EdgeLevel
        {
            get { return m_iEdgeLevel / 10.0; }
            set
            {
                uint iInput = (uint)(value * 10.0);
                if (m_iEdgeLevel != iInput && iInput >= 0 && iInput < uint.MaxValue)
                {
                    m_iEdgeLevel = iInput;
                    DoChanged();
                }
            }
        }
        public static double MinEdgeLevel
        {
            get { return 0.0; }
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays 
        /// <para>unit :us </para>
        /// </summary>      
        public static double MaxEdgeLevel
        {
            get { return (uint.MaxValue - 1) / 10.0; }
        }
        public static uint MinOrginEdgeLevel
        {
            get { return 0; }
        }
        public static uint MaxOrginEdgeLevel
        {
            get { return (uint.MaxValue - 1); }
        }
        public uint OrginEdgeLevel
        {
            get { return m_iEdgeLevel; }
        }
        #endregion EdgeLevel( set_delay_mode )

        #region MinimumJumpDelay ( set_delay_mode )
        /// <summary>
        /// set_delay_mode  , 1/10 us
        /// <para>unit :us </para>
        /// </summary>          
        public double MinimumJumpDelay
        {
            get { return m_iMinimumJumpDelay / 10.0; }
            set
            {
                uint iInput = (uint)(value * 10.0);
                if (m_iMinimumJumpDelay != iInput && UInt32.MinValue <= iInput && iInput < UInt32.MaxValue)
                {
                    m_iMinimumJumpDelay = iInput;
                    DoChanged();
                }
            }
        }
        public static double Min_MinimumJumpDelay
        {
            get { return 0.0; }
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays 
        /// <para>unit :us </para>
        /// </summary>      
        public static double Max_MinimumJumpDelay
        {
            get { return (uint.MaxValue - 1) / 10.0; }
        }
        public static uint Min_OrginMinimumJumpDelay
        {
            get { return 0; }
        }
        public static uint Max_OrginMinimumJumpDelay
        {
            get { return (uint.MaxValue - 1); }
        }
        public uint OrginMinimumJumpDelay
        {
            get { return m_iEdgeLevel; }
        }
        #endregion MinimumJumpDelay( set_delay_mode )
       
        /// <summary>
        /// set_delay_mode
        /// </summary>
        public uint JumpLengthLimit
        {
            get { return m_iJumpLengthLimit;}
            set
            {
                if (m_iJumpLengthLimit != value && UInt32.MinValue <= value && value < UInt32.MaxValue)
                {
                    m_iJumpLengthLimit = value;   
                    DoChanged();                 
                }
            }
        }

       
        public ScanlabRTC5.LASER_MODE LaserMode
        {
            get { return (ScanlabRTC5.LASER_MODE)m_iLaserMode;}
            set
            {
                if (m_iLaserMode!=(uint)value)
                {
                    m_iLaserMode = (uint)value;   
                    DoChanged();              
                }
            }
        } 
        public bool PulseSwitchSettingEnable
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PULSE_SWITCH_SET];}
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PULSE_SWITCH_SET]=value;
                DoChanged();
            }
        }       
        public bool PhaseShiftEnable
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PHASE_SHIFT_ENABLE]; }
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PHASE_SHIFT_ENABLE] = value;
                DoChanged();
            }
        }
        public bool LaserActiveEnable
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.ENABLE_LASER_ACTIVE]; }
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.ENABLE_LASER_ACTIVE] = value;
                DoChanged();
            }
        }
        /// <summary>
        /// false : active High , true : active Low
        /// </summary>
        public bool LaserOnActiveLevel
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_ON_SIGNAL_LEV]; }
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_ON_SIGNAL_LEV] = value;
                DoChanged();
            }
        }
        /// <summary>
        /// false : active High , true : active Low
        /// </summary>
        public bool LaserOutputPortSignalLevel
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_1_2_PORT_SIGNAL_LEV]; }
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_1_2_PORT_SIGNAL_LEV] = value;
                DoChanged();
            }
        }
        /// <summary>
        /// true : Riging Edge , false : falling Edge
        /// </summary>
        public bool LaserOnEXT_SignalPulse
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_EXT_SIGNAL_PULSES]; }
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_EXT_SIGNAL_PULSES] = value;
                DoChanged();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool OutputSyncchronization
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.OUTPUT_SYNC_SWITCH]; }
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.OUTPUT_SYNC_SWITCH] = value;
                DoChanged();
            }
        }
         public bool ConstantPulseLengthMode
        {
            get { return m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.CONSTANT_PULSE_LEN_MODE]; }
            set
            {
                m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.CONSTANT_PULSE_LEN_MODE] = value;
                DoChanged();
            }
        }


        #region FistpulseKiller ( set_firstpulse_killer )
        /// <summary>
        /// set_firstpulse_killer  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
        public double FirstPulseKillerLength
        {
            get { return m_iFirstPulseKillerLen / 64.0; }
            set
            {
                uint iInput = (uint)(value * 64.0);
                if (m_iFirstPulseKillerLen != iInput && 
                    ScanlabRTC5.MinOrginFirstPulseKillerLength<=iInput &&
                    ScanlabRTC5.MaxOrginFirstPulseKillerLength>=iInput )
                {
                    m_iFirstPulseKillerLen = iInput;
                    DoChanged();
                }
            }
        }       
        public uint OrginFirstPulseKillerLength
        {
            get { return m_iFirstPulseKillerLen; }
            set
            {              
                if (m_iFirstPulseKillerLen != value &&
                    ScanlabRTC5.MinOrginFirstPulseKillerLength<=value   &&
                    ScanlabRTC5.MaxOrginFirstPulseKillerLength>=value )
                {
                    m_iFirstPulseKillerLen = value;
                    DoChanged();
                }
            }
        }
        #endregion FistpulseKiller ( set_firstpulse_killer )

        #region QSwitchDelay ( set_qswitch_delay )
        /// <summary>
        /// set_qswitch_delay  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
        public double QSwitchDelay
        {
            get { return m_iQSwitchDelay / 64.0; }
            set
            {
                uint iInput = (uint)(value * 64.0);
                if ( m_iQSwitchDelay != iInput && 
                     iInput >= ScanlabRTC5.MinOrginQSwitchDelay  && 
                     iInput <=ScanlabRTC5.MaxOrginQSwitchDelay)
                {
                    m_iQSwitchDelay = iInput;
                    DoChanged();
                }
            }
        }       
        public uint OrginQSwitchDelay
        {
            get { return m_iQSwitchDelay; }
        }
        #endregion QSwitchDelay ( set_qswitch_delay )


        #region StandbyHalfPeriod ( set_standby )

        /// <summary>
        /// Hz
        /// </summary>
        public double StandByFrequency
        {
            get { return StandbyHalfPeriod==0?0.0 : 1/(StandbyHalfPeriod*2.0/1000000);}
            set {
                if (value<=0)
                {
                    m_iStandbyHalfPeriod=0;
                }
                else
                {
                    if(ScanlabRTC5.MinStandByFrequency<=value &&
                       ScanlabRTC5.MaxStandByFrequency>=value)
                    {
                        StandbyHalfPeriod=((1.0/value)*1000000.0)/2.0;
                    }                    
                }
            }
        }
        /// <summary>
        /// set_standby  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
        public double StandbyHalfPeriod
        {
            get { return m_iStandbyHalfPeriod / 64.0; }
            set
            {
                uint iInput = (uint)(value * 64.0);
                if (m_iStandbyHalfPeriod != iInput && 
                    ScanlabRTC5.MinOrginStandbyHalfPeriod<= iInput && 
                    iInput <= ScanlabRTC5.MaxOrginStandbyHalfPeriod)
                {
                    m_iStandbyHalfPeriod = iInput;
                    DoChanged();
                }
            }
        }      
        public uint OrginStandbyHalfPeriod
        {
            get { return m_iStandbyHalfPeriod; }
            set
            {
                if (m_iStandbyHalfPeriod != value &&
                  ScanlabRTC5.MinOrginStandbyHalfPeriod <= value &&
                  value <= ScanlabRTC5.MaxOrginStandbyHalfPeriod)
                {
                    m_iStandbyHalfPeriod = value;
                    DoChanged();
                }
            }
        }
        #endregion StandbyHalfPeriod ( set_standby )

        #region StandbyPulseLength ( set_standby )
        /// <summary>
        /// set_standby  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
        public double StandbyPulseLength
        {
            get { return m_iStandbyPulseLength / 64.0; }
            set
            {
                uint iInput = (uint)(value * 64.0);
                if (m_iStandbyPulseLength != iInput && 
                    UInt32.MinValue <= iInput && 
                    iInput < UInt32.MaxValue)
                {
                    m_iStandbyPulseLength = iInput;
                    DoChanged();
                }
            }
        }
        public static double MinStandbyPulseLength
        {
            get { return 0.0; }
        }
        public static double MaxStandbyPulseLength
        {
            get { return (UInt32.MaxValue - 1) / 64.0; }
        }
        public static uint MinOrginStandbyPulseLength
        {
            get { return 0; }
        }
        public static uint MaxOrginStandbyPulseLength
        {
            get { return (UInt32.MaxValue - 1); }
        }
        public uint OrginStandbyPulseLength
        {
            get { return m_iStandbyHalfPeriod; }
        }
        #endregion StandbyPulseLength ( set_standby )

        #region FreQHalfPeriod ( set_pulse_ctrl )
        /// <summary>
        /// set_standby  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>                 
        public double FreQuency
        {
            get { return FreQHalfPeriod == 0 ? 0.0 : 1.0 / (FreQHalfPeriod*2.0 / (1000000.0)); }
            set
            {
                if (value <= 0)
                {
                    FreQHalfPeriod = 0;
                }
                else
                {
                    if(FreQHalfPeriod!=value && 
                       ScanlabRTC5.MinFreQuency<=value&&
                       ScanlabRTC5.MaxFreQuency>=value)
                    FreQHalfPeriod = ((1.0 / value)*1000000.0)/2.0;
                }
            }
        }
        public double FreQHalfPeriod
        {
            get { return m_iHalfPeriod / 64.0; }
            set
            {
                uint iInput = (uint)(value * 64.0);
                if (m_iHalfPeriod != iInput &&
                   ScanlabRTC5.MinOrginFreQHalfPeriod <= iInput &&
                   ScanlabRTC5.MaxOrginFreQHalfPeriod >= iInput)
                {
                    m_iHalfPeriod = iInput;                    
                }
            }
        }
        public uint OrginFreQHalfPeriod
        {
            get { return m_iHalfPeriod; }
            set
            {
                
                if (m_iHalfPeriod != value &&
                   ScanlabRTC5.MinOrginFreQHalfPeriod <= value &&
                   ScanlabRTC5.MaxOrginFreQHalfPeriod >= value)
                {
                    m_iHalfPeriod = value;                    
                }
            }
        }
        #endregion FreQPulseLength ( set_pulse_ctrl )

        #region FreQFrequency( set_pulse_ctrl )
        /// <summary>
        /// set_standby  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
        public double  FreQPulseLength
        {
            get { return m_iPulseLength / 64.0; }
            set
            {
                uint iInput = (uint)(value * 64.0);
                if (m_iPulseLength != iInput && 
                    ScanlabRTC5.MinOrginFreQPulseLength <= iInput && 
                    ScanlabRTC5.MaxOrginFreQPulseLength>=iInput)
                {
                    m_iPulseLength = iInput;                    
                }
            }
        }       
        public uint OrginFreQPulseLength
        {
            get { return m_iPulseLength; }
            set
            {
                
                if (m_iPulseLength != value &&
                    ScanlabRTC5.MinOrginFreQPulseLength <= value &&
                    ScanlabRTC5.MaxOrginFreQPulseLength >= value)
                {
                    m_iPulseLength = value;                    
                }
            }
        }
        #endregion FreQPulseLength ( set_pulse_ctrl )


        #region    LaserOnDelay ( set_laser_delays)

        /// <summary>
        /// us
        /// </summary>
        public double LaserOnDelay
        {
            get { return m_iLaserOnDelay * 0.5; }
            set
            {
                int iInput = (int)(value / 0.5);
                if (m_iPulseLength != iInput &&
                    ScanlabRTC5.MinOrginLaserOnDelay <= iInput &&
                    ScanlabRTC5.MaxOrginLaserOnDelay >= iInput)
                {
                    m_iLaserOnDelay = iInput;                    
                }
            }
        }
        public int OrginLaserOnDelay
        {
            get { return m_iLaserOnDelay; }
            set
            {
                if (m_iLaserOnDelay != value &&
                    ScanlabRTC5.MinOrginLaserOnDelay <= value &&
                    ScanlabRTC5.MaxOrginLaserOnDelay >= value)
                {
                    m_iLaserOnDelay = value;
                }
            }
        }
        #endregion LaserOnDelay ( set_laser_delays)

        #region    LaserOffDelay ( set_laser_delays)

        /// <summary>
        /// us
        /// </summary>
        public double LaserOffDelay
        {
            get { return m_iLaserOffDelay * 0.5; }
            set
            {
                uint iInput = (uint)(value / 0.5);
                if (m_iLaserOffDelay != iInput &&
                    ScanlabRTC5.MinOrginLaserOffDelay <= iInput &&
                    ScanlabRTC5.MaxOrginLaserOffDelay >= iInput)
                {
                    m_iLaserOffDelay = iInput;                    
                }
            }
        }
        public uint OrginLaserOffDelay
        {
            get { return m_iLaserOffDelay; }
            set
            {
                if (m_iLaserOnDelay != value &&
                    ScanlabRTC5.MinOrginLaserOffDelay <= value &&
                    ScanlabRTC5.MaxOrginLaserOffDelay >= value)
                {
                    m_iLaserOffDelay = value;
                }
            }
        }
        #endregion LaserOffDelay ( set_laser_delays)


        public double AngleCompenstation
        {
            get {  return m_iAngleCompensation;}
            set
            {
                if (m_iAngleCompensation != value)
                {
                    m_iAngleCompensation = value;
                    DoChanged();
                }
            }
        }
        public bool IsSkyWritingModeEnable
        {
            get { return m_iSkyWritingMode>0;}
        }
        public ScanlabRTC5.SKY_WRTING_MODE SkyWritingMode
        {
            get { return (ScanlabRTC5.SKY_WRTING_MODE)m_iSkyWritingMode;}
            set
            {

                m_iSkyWritingMode=(uint)value;
            }
        }
        public uint SkyWritingTimelag
        {
            get { return m_iSkyWritingTimelag;}
            set
            {
                if (m_iSkyWritingTimelag != value && value >= 0)
                {
                    m_iSkyWritingTimelag = value;               
                }
            }
        }
        public int SkyWritingLaserOnShift
        {
            get { return m_iSkyWritingLaserOnShift;}
            set
            {
                if (m_iSkyWritingLaserOnShift != value && value >= 0)
                {
                    m_iSkyWritingLaserOnShift = value;                
                }
            }
        }   
        public uint SkyWritingNPrev
        {
            get { return m_iSkyWritingNPrev;}
            set
            {
                if (m_iSkyWritingNPrev != value && value >= 0)
                {
                    m_iSkyWritingNPrev = value;                  
                }
            }
        }
        public uint SkyWrringNPost
        {
            get { return m_iSkyWritingNPost;}
            set
            {
                if (m_iSkyWritingNPost != value && value >= 0)
                {
                    m_iSkyWritingNPost = value;                 
                }
            }
        }
        public uint SkyWritingLimit
        {
            get {return m_iSkyWritingLimit; }
            set
            {
                if (m_iSkyWritingLimit != value && value >= 0)
                {
                    m_iSkyWritingLimit = value;              
                }
            }
        }     
        public uint GalvoMirrorMinTemp
        {
            get { return m_iGalvaMirrorMinTemp;}
            set
            {
                if (m_iGalvaMirrorMinTemp != value && 0 <= value && value <= 1000)
                {
                    m_iGalvaMirrorMinTemp=value;
                }
            }
        }
        public uint GalvoMirrorMaxTemp
        {
            get { return m_iGalvaMirrorMaxTemp; }
            set
            {
                if (m_iGalvaMirrorMaxTemp != value && 0 <= value && value <= 1000)
                {
                    m_iGalvaMirrorMaxTemp = value;
                }
            }
        }
        public uint ServoBoardMinTemp
        {
            get { return m_iServoBoardMinTemp;}
            set
            {
                if (m_iServoBoardMinTemp != value && 0 <= value && value <= 1000)
                {
                    m_iServoBoardMinTemp = value;                 
                }
            }
        }
        public uint ServoBoardMaxTemp
        {
            get { return m_iServoBoardMaxTemp; }
            set
            {
                if (m_iServoBoardMaxTemp != value && 0 <= value && value <= 1000)
                {
                    m_iServoBoardMaxTemp = value;
                }
            }
        }  
        public double fXPosScale
        {
            get {return m_fXPosScale; }
            set
            {
                if(m_fXPosScale!=value)
                {
                    m_fXPosScale=value;
                }
            }
        }
        public double fYPosScale
        {
            get {  return m_fYPosScale;}
            set
            {
                if(m_fYPosScale!=value)
                {
                    m_fYPosScale=value;
                }
            }
        }
        public double fScale
        {
            get { return m_fScale;}
            set
            {
                if(m_fScale!=value)
                {
                    m_fScale=value;
                }
            }
        }
        public bool EnableHeadA
        {
            get {  return m_bEnableHeadA;}
            set
            {
                if(m_bEnableHeadA!=value)
                {
                    m_bEnableHeadA=value;
                }
            }
        }
        public bool EnableHeadB
        {
            get {  return m_bEnableHeadB;}
            set
            {
                if(m_bEnableHeadB!=value)
                {
                    m_bEnableHeadB=value;
                }
            }
        }
        public XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
            uint iTemp = 0;
            if (reader.HasAttributes)
            {
                m_strID=reader.GetAttribute("ID");
                uint.TryParse(reader.GetAttribute("CardNo"), out m_iCardNo);              
                double.TryParse(reader.GetAttribute("Angle"), out m_iAngleCompensation); 
                uint.TryParse(reader.GetAttribute("ItelliScanUse"), out iTemp);
                m_bIntelliscan = iTemp > 0;              
                uint.TryParse(reader.GetAttribute("Enable_HeadA"), out iTemp);
                m_bEnableHeadA=iTemp > 0;  
                uint.TryParse(reader.GetAttribute("Enable_HeadB"), out iTemp);               
                m_bEnableHeadB=iTemp > 0;              
            }
            using (var innerReader = reader.ReadSubtree())
            {
                if (innerReader.ReadToFollowing("Correction"))
                {
                    if (innerReader.HasAttributes)
                    {
                        double.TryParse(innerReader.GetAttribute("XPosScale"),out m_fXPosScale);
                        double.TryParse(innerReader.GetAttribute("YPosScale"),out m_fYPosScale);                        
                        double.TryParse(innerReader.GetAttribute("Scale"),out m_fScale);                        
                        uint.TryParse(innerReader.GetAttribute("HeadA_CorrTableNo"), out m_iHeadACorrTableNum);
                        uint.TryParse(innerReader.GetAttribute("HeadB_CorrTableNo"), out m_iHeadBCorrTableNum);
                    }
                    using (var TableinnerReader = innerReader.ReadSubtree())
                    {
                        for(int i=0;i<m_strCorrectionTableFileNames.Length;i++)
                        {
                             if (TableinnerReader.ReadToFollowing(string.Format("Table{0}", i + 1)))
                             {
                                if (TableinnerReader.HasAttributes)
                                {
                                  m_strCorrectionTableFileNames[i]=TableinnerReader.GetAttribute("FileName");
                                  uint.TryParse(TableinnerReader.GetAttribute("DIMEMSION"),out m_iCorrectionDimemsion[i]);                                  
                                }
                             }
                        }  
                        TableinnerReader.Close();
                    }
                }
                if (innerReader.ReadToFollowing("List"))
                {
                    if (innerReader.HasAttributes)
                    {
                        uint.TryParse(reader.GetAttribute("_1stMemorySize"), out m_iList1stMemorySize);
                        uint.TryParse(reader.GetAttribute("_2ndMemorySize"), out m_iList2ndMemorySize);
                    }
                }
                if (innerReader.ReadToFollowing("set_delay_mode"))
                {
                    if (innerReader.HasAttributes)
                    {
                        uint.TryParse(reader.GetAttribute("PolygonDelay"), out m_iPolygonDelay);
                        uint.TryParse(reader.GetAttribute("Direct3D"), out m_iDirectMove3D);
                        uint.TryParse(reader.GetAttribute("EdgeLevel"), out m_iEdgeLevel);
                        uint.TryParse(reader.GetAttribute("EdgeLevel"), out m_iMinimumJumpDelay);
                        uint.TryParse(reader.GetAttribute("JumpLengthLimit"), out m_iJumpLengthLimit);
                        uint.TryParse(reader.GetAttribute("JumpDelay"), out m_iJumpDelay);
                        uint.TryParse(reader.GetAttribute("MarkDelay"), out m_iMarkDelay);
                    }
                }
                if (innerReader.ReadToFollowing("LaserCtrlValue"))
                {
                    if (innerReader.HasAttributes)
                    {
                        uint.TryParse(innerReader.GetAttribute("PulseSwitchSet"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PULSE_SWITCH_SET] = iTemp > 0;
                        uint.TryParse(innerReader.GetAttribute("PhaseShift"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PHASE_SHIFT_ENABLE] = iTemp > 0;
                        uint.TryParse(innerReader.GetAttribute("EnableLaserActive"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.ENABLE_LASER_ACTIVE] = iTemp > 0;
                        uint.TryParse(innerReader.GetAttribute("LaserOnActiveLevel"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_ON_SIGNAL_LEV] = iTemp > 0;
                        uint.TryParse(innerReader.GetAttribute("LaserPortSignalLevel"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_1_2_PORT_SIGNAL_LEV] = iTemp > 0;
                        uint.TryParse(innerReader.GetAttribute("LaserEXT_SignalLevel"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_EXT_SIGNAL_PULSES] = iTemp > 0;
                        uint.TryParse(innerReader.GetAttribute("OuputSYNC_Switch"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.OUTPUT_SYNC_SWITCH] = iTemp > 0;
                        uint.TryParse(innerReader.GetAttribute("ConstantPulseLength"), out iTemp);
                        m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.CONSTANT_PULSE_LEN_MODE] = iTemp > 0;
                    }
                }
                if (innerReader.ReadToFollowing("LaserParam"))
                {
                    if (innerReader.HasAttributes)
                    {
                        uint.TryParse(innerReader.GetAttribute("Mode"), out m_iLaserMode);
                        uint.TryParse(innerReader.GetAttribute("FirstPulseKiller"), out m_iFirstPulseKillerLen);
                        uint.TryParse(innerReader.GetAttribute("QSwitchDelay"), out m_iQSwitchDelay);
                    }
                }
                if (innerReader.ReadToFollowing("StandBy"))
                {
                    if (innerReader.HasAttributes)
                    {
                        uint.TryParse(innerReader.GetAttribute("HalfPeriod"), out m_iStandbyHalfPeriod);
                        uint.TryParse(innerReader.GetAttribute("PulseLength"), out m_iStandbyPulseLength);
                    }
                }
                if (innerReader.ReadToFollowing("LaserPulse"))
                {
                    if (innerReader.HasAttributes)
                    {
                        uint.TryParse(innerReader.GetAttribute("HalfPeriod"), out m_iHalfPeriod);
                        uint.TryParse(innerReader.GetAttribute("PulseLength"), out m_iPulseLength);
                    }
                }
                innerReader.Close();
            }
        }
        public void WriteXml(XmlWriter writer)
        {

            writer.WriteComment("CardNo : 0 (Real +1 in Coding)");
            writer.WriteStartElement("ScannerItem");
            writer.WriteAttributeString("ID", m_strID);
            writer.WriteAttributeString("CardNo", m_iCardNo.ToString());
            writer.WriteAttributeString("ItelliScanUse", (m_bIntelliscan == true ? 1 : 0).ToString());
            writer.WriteAttributeString("Angle", m_iAngleCompensation.ToString());          
            writer.WriteAttributeString("Enable_HeadA",(m_bEnableHeadA==true?1:0).ToString());
            writer.WriteAttributeString("Enable_HeadB",(m_bEnableHeadB==true?1:0).ToString());
            writer.WriteComment("Head A or B CorrtableNo 1 ~ 4");
            writer.WriteStartElement("Correction");
            writer.WriteAttributeString("XPosScale",m_fXPosScale.ToString());
            writer.WriteAttributeString("YPosScale",m_fYPosScale.ToString());
            writer.WriteAttributeString("Scale",m_fScale.ToString());            
            writer.WriteAttributeString("HeadA_CorrTableNo", m_iHeadACorrTableNum.ToString());
            writer.WriteAttributeString("HeadB_CorrTableNo", m_iHeadBCorrTableNum.ToString());          
            writer.WriteComment("FileName : (input Name) Path Fixed ../system/CFG/Scanner/RCT5/");
            for (int i = 0; i < m_strCorrectionTableFileNames.Length; i++)
            {
                writer.WriteStartElement(string.Format("Table{0}", i + 1));
                writer.WriteAttributeString("FileName", m_strCorrectionTableFileNames[i]);
                writer.WriteAttributeString("DIMEMSION", m_iCorrectionDimemsion[i].ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteComment("List 1 + List2  size <= 2^20-1");
            writer.WriteStartElement("List");
            writer.WriteAttributeString("_1stMemorySize", m_iList1stMemorySize.ToString());
            writer.WriteAttributeString("_2ndMemorySize", m_iList2ndMemorySize.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("set_delay_mode");
            writer.WriteAttributeString("PolygonDelay", m_iPolygonDelay.ToString());
            writer.WriteAttributeString("Direct3D", m_iDirectMove3D.ToString());
            writer.WriteAttributeString("EdgeLevel", m_iEdgeLevel.ToString());
            writer.WriteAttributeString("MinJumpDelay", m_iMinimumJumpDelay.ToString());
            writer.WriteAttributeString("JumpLengthLimit", m_iJumpLengthLimit.ToString());
            writer.WriteAttributeString("JumpDelay", m_iJumpDelay.ToString());
            writer.WriteAttributeString("MarkDelay", m_iMarkDelay.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("LaserCtrlValue");
            writer.WriteAttributeString("PulseSwitchSet",
                (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PULSE_SWITCH_SET] == true ? 1 : 0).ToString());
            writer.WriteAttributeString("PhaseShift",
                (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.PHASE_SHIFT_ENABLE] == true ? 1 : 0).ToString());
            writer.WriteAttributeString("EnableLaserActive",
                (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.ENABLE_LASER_ACTIVE] == true ? 1 : 0).ToString());
            writer.WriteAttributeString("LaserOnActiveLevel",
                (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_ON_SIGNAL_LEV] == true ? 1 : 0).ToString());
            writer.WriteAttributeString("LaserPortSignalLevel",
                (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_1_2_PORT_SIGNAL_LEV] == true ? 1 : 0).ToString());
            writer.WriteAttributeString("LaserEXT_SignalLevel",
                (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.LASER_EXT_SIGNAL_PULSES] == true ? 1 : 0).ToString());
            writer.WriteAttributeString("OuputSYNC_Switch",
                (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.OUTPUT_SYNC_SWITCH] == true ? 1 : 0).ToString());
            writer.WriteAttributeString("ConstantPulseLength",
               (m_LaserCtrlValue[(bit)ScanlabRTC5.LASER_CTRL_SIGNAL_IDX.CONSTANT_PULSE_LEN_MODE] == true ? 1 : 0).ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("LaserParam");
            writer.WriteAttributeString("Mode", m_iLaserMode.ToString());
            writer.WriteAttributeString("FirstPulseKiller", m_iFirstPulseKillerLen.ToString());
            writer.WriteAttributeString("QSwitchDelay", m_iQSwitchDelay.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("StandBy");
            writer.WriteAttributeString("HalfPeriod", m_iStandbyHalfPeriod.ToString());
            writer.WriteAttributeString("PulseLength", m_iStandbyPulseLength.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("LaserPulse");
            writer.WriteAttributeString("HalfPeriod", m_iHalfPeriod.ToString());
            writer.WriteAttributeString("PulseLength", m_iPulseLength.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

      
    }
}
