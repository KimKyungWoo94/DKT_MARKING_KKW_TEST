using EzIna.FA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EzIna.FA.DEF;
namespace EzIna
{
		public partial class RunningStage
		{
				public enum eM_SEQ_MOVING
				{
						Finish = 0
						, IO_Check_Start = FA.DEF.StartActionOfSub
						, IO_Check_Finish
						, Cylinder_Check_Start
						, Cylinder_Check_Finish
						, Moving_Check_Start
						, Moving_Check_Finish
						, Moving_Start
						, Moving_Finish
				}
				public bool SubSeq_Moving(double a_XPos,double a_YPos,double a_ZPos)
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eM_SEQ_MOVING>.From(m_stRun.nSubStep))
						{
								case eM_SEQ_MOVING.Finish:
										return true;
								case eM_SEQ_MOVING.IO_Check_Start:
										if (eDI.XYZ_MOTOR_PWR_MC.GetDI().Value == false)
										{
												m_stRun.TimeoutNow();
												break;
										}
										if (eDI.XYZ_MOTOR_PWR_MC.GetDI().Value == false)
										{
												m_stRun.TimeoutNow();
												break;
										}
										
										NextSubStep();
										break;
								case eM_SEQ_MOVING.IO_Check_Finish:
										NextSubStep();
										break;
								case eM_SEQ_MOVING.Cylinder_Check_Start:
										FA.ACT.CYL_STOPPER_L_DOWN.Run();
										FA.ACT.CYL_STOPPER_R_DOWN.Run();
										FA.ACT.CYL_STOPPER_CENTER_DOWN.Run();
										NextSubStep();
										break;
								case eM_SEQ_MOVING.Cylinder_Check_Finish:
										if (!FA.ACT.CYL_STOPPER_L_DOWN.Check()) break;
										if (!FA.ACT.CYL_STOPPER_R_DOWN.Check()) break;
										if (!FA.ACT.CYL_STOPPER_CENTER_DOWN.Check()) break;

										NextSubStep();
										break;
								case eM_SEQ_MOVING.Moving_Check_Start:
										if (!AXIS.RX.Status().IsServoOn)
										{
												m_stRun.TimeoutNow();
												break;
										}
										if (!AXIS.Y.Status().IsServoOn)
										{
												m_stRun.TimeoutNow();
												break;
										}
										if (!AXIS.RZ.Status().IsServoOn)
										{
												m_stRun.TimeoutNow();
												break;
										}
										NextSubStep();
										break;
								case eM_SEQ_MOVING.Moving_Check_Finish:
										if(!AXIS.RX.Status().IsMotionDone && !AXIS.RX.Status().IsInposition)
												break;
										if (!AXIS.Y.Status().IsMotionDone && !AXIS.Y.Status().IsInposition)
												break;
										if (!AXIS.RZ.Status().IsMotionDone && !AXIS.RZ.Status().IsInposition)
												break;
										NextSubStep();
										break;
								case eM_SEQ_MOVING.Moving_Start:
										 if(!AXIS.RX.Move_Absolute(a_XPos,Motion.GDMotion.eSpeedType.RUN))
												break;
										if (!AXIS.Y.Move_Absolute(a_YPos, Motion.GDMotion.eSpeedType.RUN))
												break;
										if (!AXIS.RZ.Move_Absolute(a_ZPos, Motion.GDMotion.eSpeedType.RUN))
												break;
										m_stRun.stwatchForSub.SetDelay=10;
										NextSubStep();
										break;
								case eM_SEQ_MOVING.Moving_Finish:
										if(!m_stRun.stwatchForSub.IsDone)
												break;
										if (!AXIS.RX.Status().IsMotionDone && !AXIS.RX.Status().IsInposition)
												break;
										if (!AXIS.Y.Status().IsMotionDone && !AXIS.Y.Status().IsInposition)
												break;
										if (!AXIS.RZ.Status().IsMotionDone && !AXIS.RZ.Status().IsInposition)
												break;
										FinishSubStep();
										break;
						}

						SubSeqCheckTimeout(FA.DEF.Timeout_Run,FA.DEF.Error_Run_AutoFocus + m_stRun.nSubStep);
						return false;
				}
		}
}
