using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    public class CrossHair_RTC : PGM_Interface
    {
        private AxisParam AxisParameter = new AxisParam();
        private ProcessParam ProcessParameter = new ProcessParam();
        private ProcessLaserMode ProcessLaserMode = new ProcessLaserMode();
        private PGM_Path m_Path = new PGM_Path();
				/// <summary>
				/// 
				/// </summary>
				private double m_fLaserOnDelay;
				/// <summary>
				/// 
				/// </summary>
				private double m_fLaserOffDelay;
				private int LaserMode;
				private double FovSize;
        private double Distance;
        private bool PosMode;
        private double LaserFreq;
        private double DutyCyclePercent;
        private double GalvoRotate;
				private int iScannerIDX=-1;

        public CrossHair_RTC()
        {
            m_Path.PgmPath = FA.DIR.INIT_PGM + @"CrossHair.pgm";
        }
				public void Set_FovSize(double _FovSize)
				{
						FovSize = _FovSize;
				}
				public void SetCrossLine_distance(double _distance)
        {
            Distance = _distance;
        }
				public void Set_ScannerIDX(int a_IDX)
				{
						iScannerIDX = a_IDX;
				}
				// File 
				public override void SetGalvoCalPath(string path)
        {
            m_Path.GalvoCalibrationPath = path.Trim();
        }

        public override void SetLaserFreq(double _fFreq, double _DutyPercent)
        {
            LaserFreq = _fFreq;
            DutyCyclePercent = _DutyPercent;
        }
				public void SetLaserOnOffDelay(double a_OnDelay, double a_OffDelay)
				{
						m_fLaserOnDelay = a_OnDelay;
						m_fLaserOffDelay = a_OffDelay;
				}
				public override void SetGalvoRotate(double _Rtoate)
        {
            GalvoRotate = _Rtoate;
        }

        public override string Get_FilePath()
        {
            return m_Path.PgmPath;
        }
        public override string Get_RegisterToAerotechPath()
        {
            return m_Path.Path_Register;
        }



        // motor condition
        public override void SetGalvoCondition(string _NameX, string _NameY, double _Velocity, double _Accel)
        {
            AxisParameter.GalvoX_Name = _NameX;
            AxisParameter.GalvoY_Name = _NameY;

            ProcessParameter.Marking_GalvoVelocity = _Velocity;
            ProcessParameter.Marking_GalvoRampRate = _Accel;

            ProcessParameter.Jump_GalvoXVelocity = _Velocity;
            ProcessParameter.Jump_GalvoYVelocity = _Velocity;

            ProcessParameter.Jump_GalvoXRampRate = _Accel;
            ProcessParameter.Jump_GalvoYRampRate = _Accel;
        }
				public void SetGalvoProcessDelay(double a_JumpDelay, double a_MarkDelay)
				{
						ProcessParameter.Jump_GalvoDelay = a_JumpDelay;
						ProcessParameter.Marking_GalveDelay = a_MarkDelay;
				}
				public override void SetStageXYCondition(string _NameX, string _NameY, double _Velocity, double _Accel)
        {
            AxisParameter.AxisX_Name = _NameX;
            AxisParameter.AxisY_Name = _NameY;
            AxisParameter.AxisX_Velocity = _Velocity;
            AxisParameter.AxisX_RampRate = _Accel;
            AxisParameter.AxisY_Velocity = _Velocity;
            AxisParameter.AxisY_RampRate = _Accel;
        }

        public override void SetStageZCondition(string _NameZ, double _Velocity, double _Accel)
        {
            AxisParameter.AxisZ_Name = _NameZ;
            AxisParameter.AxisZ_Velocity = _Velocity;
            AxisParameter.AxisZ_RampRate = _Accel;
        }

        // Laser Condition
        public override void SetLaserMode(int _nMode)
        {
            LaserMode = _nMode;
        }

        public override void SetPSOMode(bool _bPsoMode)
        {
            PosMode = _bPsoMode;
        }

        public override bool Make_Pgm()
        {
            double fDefaultDelay = 10.0;
						Scanner.ScanlabRTC5 pControl=Scanner.ScanlabRTCMgr.Instance[iScannerIDX];						
						pControl.ConfigData.FreQuency = LaserFreq;
						pControl.ConfigData.FreQPulseLength = (pControl.ConfigData.FreQHalfPeriod * 2) * DutyCyclePercent;
						pControl.ConfigData.LaserOnDelay = m_fLaserOnDelay;
						pControl.ConfigData.LaserOffDelay = m_fLaserOffDelay;
						pControl.ConfigData.JumpDelay = ProcessParameter.Jump_GalvoDelay;
						pControl.ConfigData.MarkDelay = ProcessParameter.Marking_GalveDelay;
						pControl.ConfigData.JumpSpeed = ProcessParameter.Jump_GalvoXVelocity ;
						pControl.ConfigData.MarkSpeed = ProcessParameter.Marking_GalvoVelocity ;
						if (pControl.GetListStatus_Load(Scanner.ScanlabRTC5.RTC_LIST._1st) == false)
						{
								pControl.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
						}
						pControl.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
						pControl.ListCrossLine(0,0,Distance,Distance);
						pControl.ListEnd();
						return pControl.GetListStatus_READY(Scanner.ScanlabRTC5.RTC_LIST._1st);						
        }
    }
}
