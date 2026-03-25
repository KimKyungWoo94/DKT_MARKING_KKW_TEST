using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTC5Import;
using System.IO;

namespace EzIna.Scanner
{
    public sealed partial class ScanlabRTC5
    {
        private static bool m_bDllInit = false;
        public static bool bDllInit { get { return m_bDllInit; } }
        private static uint m_iDllVersion = 0;
        public static uint iDllVersion { get { return m_iDllVersion; } }
        public static bool InitializeDriver()
        {
            DRIVER_ERROR_CODE Ret = DRIVER_ERROR_CODE.NO_CARD;
            Ret = (DRIVER_ERROR_CODE)RTC5Wrap.init_rtc5_dll();
            m_iDllVersion = RTC5Wrap.get_dll_version();
            RTC5Import.RTC5Wrap.set_rtc5_mode();
            return m_bDllInit = Ret == DRIVER_ERROR_CODE.NO_ERR;
        }
        public static void ResetAllDevice()
        {
            if(m_bDllInit)
            {
                uint iCardCount= RTC5Import.RTC5Wrap.rtc5_count_cards();
                uint iErrorCode=0;
                for(uint i=0 ;i<iCardCount;i++)
                { 
                    while((iErrorCode=RTC5Import.RTC5Wrap.n_get_error(i+1))!=0)
                    {
                        RTC5Import.RTC5Wrap.n_reset_error(i+1,iErrorCode);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strFileName">Without Path , Path Fixed (System\CFG\Scanner\RTC5)</param>
        /// <param name="a_TableNumber"></param>
        /// <returns></returns>      
        public static void TerminateDriver(bool a_bLaserOff = true)
        {
            if (m_bDllInit == true)
            {

                uint Count = RTC5Import.RTC5Wrap.rtc5_count_cards();
                for (uint i = 0; i < Count; i++)
                {
                    RTC5Import.RTC5Wrap.n_stop_execution(i + 1);
                    if (a_bLaserOff)
                    {
                        RTC5Import.RTC5Wrap.n_disable_laser(i + 1);
                    }
                }
                RTC5Wrap.free_rtc5_dll();
								m_bDllInit=false;
            }
        }
        private static string ConfigFolderPath
        {
            get
            {
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                strPath = System.IO.Path.GetDirectoryName(strPath);
                string strConfigFolderPath = Path.GetFullPath(Path.Combine(strPath, @"..\System\CFG\"));

                DirectoryInfo DirInfo = new DirectoryInfo(strConfigFolderPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();
                return strConfigFolderPath;
            }
        }

        public static string ConfigScanlabFolderPath
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strConfigFolderPath = Path.GetFullPath(Path.Combine(strPath, @"Scanner\RTC5\"));
                //string strConfigVisionFilePath = Path.GetFullPath(Path.Combine(strPath, @"Comm\ComSetting.ini"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();

                return strConfigFolderPath;
            }
        }

        public static string ConfigScanlabXMLFileFullPath
        {
            get
            {
                string strConfigFolderPath = ConfigScanlabFolderPath;
                string strConfigFilePath = Path.GetFullPath(Path.Combine(strConfigFolderPath, @"ScablabLinkData.xml"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();
                return strConfigFilePath;
            }
        }
        public static string ConfigCorreXionProPath
        {
            get
            {
                string strConfigFolderPath = ConfigScanlabFolderPath;
                string strCorreXionProPath = Path.GetFullPath(Path.Combine(strConfigFolderPath, @"correXionPro.exe"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();

                return strCorreXionProPath;
            }
        }
        public static string ConfigScanlabFileName
        {
            get { return "ScablabLinkData.xml"; }
        }
        public static string ConfigCorreXionProFileName
        {
            get { return "correXionPro.exe"; }
        }
        public static uint RTCBoardCount
        {
            get { return RTC5Import.RTC5Wrap.rtc5_count_cards(); }
        }

        public static int MOVE_Negative_LIMIT
        {
            get { return -524288;}
        }
        public static int MOVE_Positive_LIMIT
        {
            get {return  524287;}
        }
		public static int MOVE_TOTAL_AMOUNT
		{
				get { return 1048576/*2^20*/;}
		}
		/// <summary>
				/// bit / ms
				/// </summary>
        public static double MIN_JUMP_MARK_SPEED
        {
            get { return 1.6; }
        }
				/// <summary>
				/// bit / ms
				/// </summary>
		public static double MAX_JUMP_MARK_SPEED
        {
            get {return 800000.0;}
        }

        #region FistpulseKiller ( set_firstpulse_killer )
        /// <summary>
        /// set_firstpulse_killer  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
     
        public static double MinFirstPulseKillerLength
        {
            get { return 0.0; }
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays 
        /// <para>unit :us </para>
        /// </summary>      
        public static double MaxFirstPulseKillerLength
        {
            get { return MaxOrginFirstPulseKillerLength / 64.0; }
        }
        public static uint MinOrginFirstPulseKillerLength
        {
            get { return 0; }
        }
        public static uint MaxOrginFirstPulseKillerLength
        {
            get { return (67108864/*2^26*/ - 1); }
        }       
        #endregion FistpulseKiller ( set_firstpulse_killer )

        #region jump, Mark , Polygon ( set_scanner_delays )
        /// <summary>
        /// set_scanner_delays , jump, Mark , Polygon ,Unit: us        
        /// </summary>
        public static double MinScannerDelays
        {
            get { return 0; }
        }
        /// <summary>
        /// set_scanner_delays , jump, Mark , Polygon ,Unit: us
        /// </summary>
        public static double MaxScannerDelays
        {
            get { return MaxOrginScannerDelays *10.0; }
        }
        public static uint MinOrginScannerDelays
        {
            get { return 0; }
        }
        /// <summary>
        /// us
        /// </summary>
        public static uint MaxOrginScannerDelays
        {
            get { return (uint.MaxValue - 1); }
        }
        #endregion jump, Mark , Polygon ( set_scanner_delays )

        #region   LaserOn Off Delay (set_Laser_delays)
        public static double MinLaserOnDelay
        {
            get { return MinOrginLaserOnDelay*0.5; }
        }
        public static double MaxLaserOnDelay
        {
            get { return MaxOrginLaserOnDelay*0.5; }
        }
        public static double MinOrginLaserOnDelay
        {
            get { return -(2147483648)/*-2^31*/; }
        }
        public static double MaxOrginLaserOnDelay
        {
            get { return 2097151/*(2^21)-1*/; }
        }
        /// <summary>
        /// set_laser_delays  , Unit : us
        /// </summary>
        public static double MinLaserOffDelay
        {
            get { return 0.0; }
        }

        /// <summary>
        /// set_laser_delays  , Unit : us
        /// </summary>
        public static double MaxLaserOffDelay
        {
            get { return MaxOrginLaserOffDelay* 0.5; }
        }
        public static double MinOrginLaserOffDelay
        {
            get { return 0; }
        }
        public static double MaxOrginLaserOffDelay
        {
            get { return 2097151/*2^21*/; }
        }
        #endregion   LaserOn Off Delay (set_Laser_delays)

        #region QSwitchDelay ( set_qswitch_delay )
        /// <summary>
        /// set_qswitch_delay  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>                
        public static double MinQSwitchDelay
        {
            get { return 0.0; }
        }
        /// <summary>
        /// set_delay_mode ,set_scannder_delays 
        /// <para>unit :us </para>
        /// </summary>      
        public static double MaxQSwitchDelay
        {
            get { return MaxOrginQSwitchDelay / 64.0; }
        }
        public static uint MinOrginQSwitchDelay
        {
            get { return 0; }
        }
        public static uint MaxOrginQSwitchDelay
        {
            get { return (67108864/*2^26*/ - 1); }
        }
        #endregion QSwitchDelay ( set_qswitch_delay )

        #region StandbyHalfPeriod ( set_standby )

        public static double MinStandByFrequency
        {
            get { return 0.0; }
        }
        public static double MaxStandByFrequency
        {
            get { return 1.0 / (1.0 / 64.0 / 1000000.0 * 2.0); }
        }
        public static double MinStandbyHalfPeriod
        {
            get { return 0.0; }
        }
        public static double MaxStandbyHalfPeriod
        {
            get { return MaxOrginStandbyHalfPeriod / 64.0; }
        }
        public static uint MinOrginStandbyHalfPeriod
        {
            get { return 0; }
        }
        public static uint MaxOrginStandbyHalfPeriod
        {
            get { return (UInt32.MaxValue-1); }
        }
      
        #endregion StandbyHalfPeriod ( set_standby )

        #region StandbyPulseLength ( set_standby )
        /// <summary>
        /// set_standby  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
      
        public static double MinStandbyPulseLength
        {
            get { return 0.0; }
        }
        public static double MaxStandbyPulseLength
        {
            get { return MaxOrginStandbyPulseLength / 64.0; }
        }
        public static uint MinOrginStandbyPulseLength
        {
            get { return 0; }
        }
        public static uint MaxOrginStandbyPulseLength
        {
            get { return (UInt32.MaxValue-1); }
        }

        #endregion StandbyPulseLength ( set_standby )

        #region FreQPulseLength ( set_pulse_ctrl )
        /// <summary>
        /// set_standby  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>               
        
        public static double MinFreQuency
        {
            get { return 0.0; }
        }
        public static double MaxFreQuency
        {
            get { return 1.0 / (1.0 /*HalfPeriod*// 64.0 / 1000000.0 * 2.0); }
        }
        public static double MinFreQHalfPeriod
        {
            get { return 0.0; }
        }
        public static double MaxFreQHalfPeriod
        {
            get { return MaxOrginFreQHalfPeriod / 64.0; }
        }
        public static uint MinOrginFreQHalfPeriod
        {
            get { return 0; }
        }
        public static uint MaxOrginFreQHalfPeriod
        {
            get { return (UInt32.MaxValue - 1); }
        }
      
        #endregion FreQPulseLength ( set_pulse_ctrl )

        #region FreQPulseLength ( set_pulse_ctrl )
        /// <summary>
        /// set_standby  ,1=1/64us 
        /// <para>unit :us </para>
        /// </summary>          
      
        public static double MinFreQPulseLength
        {
            get { return 0.0; }
        }
        public static double MaxFreQPulseLength
        {
            get { return MaxOrginFreQPulseLength / 64.0; }
        }
        public static uint MinOrginFreQPulseLength
        {
            get { return 0; }
        }
        public static uint MaxOrginFreQPulseLength
        {
            get { return (UInt32.MaxValue - 1); }
        }    
        #endregion FreQPulseLength ( set_pulse_ctrl )

    }
}
