using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
		public class Find_Focus_RTC : PGM_Interface
		{

				private AxisParam AxisParameter = new AxisParam();
				private ProcessParam ProcessParameter = new ProcessParam();
				private ProcessLaserMode ProcessLaserMode = new ProcessLaserMode();
				private PGM_Path m_Path = new PGM_Path();

				private int LaserMode;
				private double Distance;
				private bool PosMode;
				private double LaserFreq;
				private double DutyCyclePercent;
			
				private double GalvoRotate;
				private double FocusZ;
				private double SearchZ;
				private double SearchZ_Pitch;
				private double Beam_Pitch_X;
				private double Beam_Pitch_Y;
				private int NumOfShots;
				private int iScannerIDX=-1;

				public Find_Focus_RTC()
				{
						m_Path.PgmPath = EzIna.FA.DIR.INIT_PGM + "Find_Focus.pgm";
				}
				public void Set_Focus_Condition_Search(double _PosZ, double _SearchZ, double _SerarchPitch)
				{
						FocusZ = _PosZ;
						SearchZ = _SearchZ;
						SearchZ_Pitch = _SerarchPitch;
				}
				public void Set_Focus_Condition_BeamPitchXY(double _pitchX, double _pitchY)
				{
						Beam_Pitch_X = _pitchX;
						Beam_Pitch_Y = _pitchY;
				}

				public void Set_NumOfShots(int nShots)
				{
						NumOfShots = nShots;
				}
				// File 
				public override void SetGalvoCalPath(string path)
				{
						m_Path.GalvoCalibrationPath = path.Trim();
				}
				public void Set_ScannerIDX(int a_IDX)
				{
						iScannerIDX = a_IDX;
				}

				public override void SetLaserFreq(double _fFreq, double _DutyPercent)
				{
						LaserFreq = _fFreq;
						DutyCyclePercent = _DutyPercent;
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
            Scanner.ScanlabRTC5 pControl=Scanner.ScanlabRTCMgr.Instance[iScannerIDX];
						int nNumberOfShot=0;			
            if(pControl!=null)
            {
								if(pControl.IsExecuteList_BUSY ||pControl.GetListStatus_BUSY(Scanner.ScanlabRTC5.RTC_LIST._1st))
										return false;
                if(NumOfShots<=1)
										return false;
								nNumberOfShot=	NumOfShots%2==0? 	NumOfShots+1:		NumOfShots;							
								pControl.ConfigData.FreQuency = LaserFreq;
								pControl.ConfigData.FreQPulseLength = (pControl.ConfigData.FreQHalfPeriod * 2) * DutyCyclePercent;
								pControl.ConfigData.JumpDelay = ProcessParameter.Jump_GalvoDelay;
								pControl.ConfigData.MarkDelay = ProcessParameter.Marking_GalveDelay;

								if (pControl.GetListStatus_Load(Scanner.ScanlabRTC5.RTC_LIST._1st)==false)
								{
										pControl.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
								}
								pControl.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);								
								pControl.GateOnNumberOfShot(1);
								return pControl.GetListStatus_READY(Scanner.ScanlabRTC5.RTC_LIST._1st);
						}
						return false;
				}
		}

}
