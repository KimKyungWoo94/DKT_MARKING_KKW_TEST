using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.Motion
{


		public sealed partial class CMotionAXT : MotionAXTItem, MotionInterface
		{
				private bool IsDisposed = false;
				MOTION_INFO m_AXT_StatusInfo;
				CAXT_AxisModuleInfo m_AXT_ModuleInfor;

				public CAXT_AxisModuleInfo AxisModuleInfor
				{
						get
						{
								return m_AXT_ModuleInfor;
						}
				}
				public CMotionAXT(int a_iAxis, string a_strAxisName) : base(a_iAxis, a_strAxisName)
				{
						Initialize();
				}
				~CMotionAXT()
				{
						Dispose(false);
				}
				public override void Dispose(bool a_Disposing)
				{
						if (IsDisposed)
								return;
						if (a_Disposing)
						{

								// Free any other managed objects here.
						}
						IsDisposed = true;
				}
				public Type DeviceType
				{
						get
						{
								return this.GetType();
						}
				}
				public override void Initialize()
				{
						m_AXT_ModuleInfor = GetAxisInformation(this.iAxisNo);
						base.OpenMotionSpeedProfile();
				}
				public override void Terminate()
				{
						base.SaveMotionSpeedProfile();
				}
				// If you need , you will redefine this Func
				/*
        public new bool OpenMotionSpeedProfile()
        {
        }
        public new bool SaveMotionSpeedProfile()
        {
          

            return true;
        }
        */
				public bool ServoOn
				{
						set
						{
#if !SIM
								uint duOnOff;
								duOnOff = (uint)Convert.ToInt32(value);
								//++ 지정 축의 Servo On/Off 신호를 출력합니다.
								CAXM.AxmSignalServoOn(m_iAxisNo, duOnOff);
#else
							m_bServoOn=value;
#endif
						}
				}

				public bool SD_STOP()
				{
#if !SIM
						// 지정 축을 감속 정지한다.
						if (CAXM.AxmMoveSStop(m_iAxisNo) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#endif
						return true;
				}
				public bool E_STOP()
				{
#if !SIM
						// 지정 축을 급 정지 한다.
						if (CAXM.AxmMoveEStop(m_iAxisNo) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#endif
						return true;
				}
				public bool JOG_STOP()
				{
						return this.SD_STOP();
				}


				public void HomeReadStatus()
				{
#if !SIM
						uint duRetCode, duState = 0;
						uint duStepMain = 0, duStepSub = 0;

						//++ 지정한 축의 원점신호의 설정상태를 확인합니다.
						duRetCode = CAXM.AxmHomeReadSignal(m_iAxisNo, ref duState);
						if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS) m_stMotionHomeStatus.m_bHomeStatus_Signal = Convert.ToBoolean(duState);

						//++ 지정한 축의 원점신호의 상태를 확인합니다.
						CAXM.AxmHomeGetResult(m_iAxisNo, ref duState);
						if (m_iUPreviousHomeState != duState)
						{
								m_stMotionHomeStatus.m_strHomeStatus_Search = TranslateHomeResult(duState);
								m_iUPreviousHomeState = duState;
								if (duState == (uint)AXT_MOTION_HOME_RESULT.HOME_SUCCESS)
										m_bHomeComplete = true;
						}
						//++ 지정한 축의 원점검색 결과를 확인합니다
						duRetCode = CAXM.AxmHomeGetRate(m_iAxisNo, ref duStepMain, ref duStepSub);
						if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
						{
								m_stMotionHomeStatus.m_strHomeStatus_StepMain = Convert.ToString(duStepMain);
								m_stMotionHomeStatus.m_strHomeStatus_StepSub = Convert.ToString(duStepSub);
						}
						//++ 지정한 축의 원점검색 진행율을 확인합니다.
						duRetCode = CAXM.AxmHomeGetRate(m_iAxisNo, ref duStepMain, ref duStepSub);
						if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
						{
								m_stMotionHomeStatus.m_nHomeStatus_ProgressBar = (int)duStepSub;
						}
#endif
				}

				public bool HomeStart()
				{
#if !SIM
						m_bHomeComplete = false;
						uint duRetCode = 0;
						//++ 지정한 축에 원점검색을 진행합니다.
						duRetCode = CAXM.AxmHomeSetStart(m_iAxisNo);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#else
						m_bHomeComplete=true;
						m_stMotionInfoStatus.m_bIsInPos = true;
						m_stMotionInfoStatus.m_bIsLimitN=false;
						m_stMotionInfoStatus.m_bIsLimitP=false;
						m_stPositionStatus.fCmdPos=0;
						m_stPositionStatus.fActPos=0;
						m_stPositionStatus.fErrPos=0;
						m_stPositionStatus.fVelocity=0;
						m_bIsMotionDone=true;
#endif
						return true;
				}

				#region     Moving Func
				public bool Move_Absolute(double a_dPos, GDMotion.stMotionSpeedSetting a_stSpeed)
				{
						if (IsHomeComplete == false)
								return false;
#if !SIM
						if (IsMotionDone == false)
								return false;

						uint duRetCode = 0;

						/*
						 * 
						//if S-Curve
						//CAXM.AxmMotSetAccelJerk(m_iAxisNo, 50);
						//CAXM.AxmMotSetDecelJerk(m_iAxisNo, 50);
							CAXM.AxmMotSetProfileMode(Axis, Profile);
									CAXM.AxmMotSetAccelJerk(Axis, MoveSmooth);
									CAXM.AxmMotSetDecelJerk(Axis, MoveSmooth);
									CAXM.AxmMotSetAbsRelMode(Axis, (uint)AXT_MOTION_ABSREL.POS_ABS_MODE);
									CAXM.AxmMotSetMinVel(Axis, 0);
						*/
						//++ 지정 축의 구동 좌표계를 설정합니다. 
						// dwAbsRelMode : (0)POS_ABS_MODE - 현재 위치와 상관없이 지정한 위치로 절대좌표 이동합니다.
						//                (1)POS_REL_MODE - 현재 위치에서 지정한 양만큼 상대좌표 이동합니다.
						duRetCode = CAXM.AxmMotSetAbsRelMode(m_iAxisNo, (uint)GDMotion.eMoveMode.MOVE_ABS);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMotSetProfileMode(m_iAxisNo, m_stParamSetting.UiProfileMode);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						if (CAXM.AxmMotSetMinVel(m_iAxisNo, 0) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMoveStartPos(m_iAxisNo, a_dPos, a_stSpeed.m_dMaxSpeed, a_stSpeed.m_dAcceleration, a_stSpeed.m_dDeceleration);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#else
						if(a_dPos >=0)
						{
								m_stPositionStatus.fActPos=a_dPos;
								m_stPositionStatus.fCmdPos=a_dPos;
								m_bIsMotionDone=true;
								m_stMotionInfoStatus.m_bIsInPos = true;
						}
#endif

						return true;
				}

				public bool Move_Absolute(double a_dPos, GDMotion.eSpeedType a_eSpeedType)
				{
						if (IsHomeComplete == false)
								return false;
#if !SIM
						if (IsMotionDone == false)
								return false;

						uint duRetCode = 0;

						/*
						 * 
						//if S-Curve
						//CAXM.AxmMotSetAccelJerk(m_iAxisNo, 50);
						//CAXM.AxmMotSetDecelJerk(m_iAxisNo, 50);
							CAXM.AxmMotSetProfileMode(Axis, Profile);
									CAXM.AxmMotSetAccelJerk(Axis, MoveSmooth);
									CAXM.AxmMotSetDecelJerk(Axis, MoveSmooth);
									CAXM.AxmMotSetAbsRelMode(Axis, (uint)AXT_MOTION_ABSREL.POS_ABS_MODE);
									CAXM.AxmMotSetMinVel(Axis, 0);
						*/
						//++ 지정 축의 구동 좌표계를 설정합니다. 
						// dwAbsRelMode : (0)POS_ABS_MODE - 현재 위치와 상관없이 지정한 위치로 절대좌표 이동합니다.
						//                (1)POS_REL_MODE - 현재 위치에서 지정한 양만큼 상대좌표 이동합니다.
						duRetCode = CAXM.AxmMotSetAbsRelMode(m_iAxisNo, (uint)GDMotion.eMoveMode.MOVE_ABS);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMotSetProfileMode(m_iAxisNo, m_stParamSetting.UiProfileMode);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						if (CAXM.AxmMotSetMinVel(m_iAxisNo, 0) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						GDMotion.stMotionSpeedSetting m_stSpeed = new GDMotion.stMotionSpeedSetting();

						switch (a_eSpeedType)
						{
								case GDMotion.eSpeedType.FAST:
										m_stSpeed = m_Fast;
										break;
								case GDMotion.eSpeedType.RUN:
										m_stSpeed = m_Run;
										break;
								case GDMotion.eSpeedType.SLOW:
										m_stSpeed = m_Slow;
										break;
								case GDMotion.eSpeedType.USER:
										m_stSpeed = m_User;
										break;
						}

						duRetCode = CAXM.AxmMoveStartPos(m_iAxisNo, a_dPos, m_stSpeed.m_dMaxSpeed, m_stSpeed.m_dAcceleration, m_stSpeed.m_dDeceleration);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#else
						if (a_dPos >= 0)
						{
								m_stPositionStatus.fActPos = a_dPos;
								m_stPositionStatus.fCmdPos = a_dPos;
								m_bIsMotionDone = true;
								m_stMotionInfoStatus.m_bIsInPos = true;
						}
#endif
						return true;
				}

				public bool Move_Absolute(double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel)
				{
						if (IsHomeComplete == false)
								return false;
#if !SIM
						if (IsMotionDone == false)
								return false;

						uint duRetCode = 0;

						//if S-Curve
						//
						//CAXM.AxmMotSetAccelJerk(m_iAxisNo, 50);
						//CAXM.AxmMotSetDecelJerk(m_iAxisNo, 50);

						//++ 지정 축의 구동 좌표계를 설정합니다. 
						// dwAbsRelMode : (0)POS_ABS_MODE - 현재 위치와 상관없이 지정한 위치로 절대좌표 이동합니다.
						//                (1)POS_REL_MODE - 현재 위치에서 지정한 양만큼 상대좌표 이동합니다.
						duRetCode = CAXM.AxmMotSetAbsRelMode(m_iAxisNo, (uint)GDMotion.eMoveMode.MOVE_ABS);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;


						/*
							CAXM.AxmMotSetProfileMode(Axis, (uint)Profile);
									CAXM.AxmMotSetAccelJerk(Axis, MoveSmooth);
									CAXM.AxmMotSetDecelJerk(Axis, MoveSmooth);
									CAXM.AxmMotSetAbsRelMode(Axis, (uint)AXT_MOTION_ABSREL.POS_REL_MODE);
									CAXM.AxmMotSetMinVel(Axis, 0);
						*/
						duRetCode = CAXM.AxmMotSetProfileMode(m_iAxisNo, m_stParamSetting.UiProfileMode);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
						duRetCode = CAXM.AxmMoveStartPos(m_iAxisNo, a_dPos, a_dVelocity, a_dAcc, a_dDecel);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						if (CAXM.AxmMotSetMinVel(m_iAxisNo, 0) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#else
						if (a_dPos >= 0)
						{
								m_stPositionStatus.fActPos = a_dPos;
								m_stPositionStatus.fCmdPos = a_dPos;
								m_bIsMotionDone = true;
								m_stMotionInfoStatus.m_bIsInPos = true;
						}
#endif

						return true;
				}

				public bool Move_Jog(bool a_bDir, GDMotion.eSpeedType a_eSpeedType)
				{
#if !SIM
						uint duRetCode = 0;

						CAXM.AxmMotSetMinVel(m_iAxisNo, 0);

						GDMotion.stMotionSpeedSetting m_stSpeed = new GDMotion.stMotionSpeedSetting();

						switch (a_eSpeedType)
						{
								case GDMotion.eSpeedType.FAST:
										m_stSpeed = m_Fast;
										break;
								case GDMotion.eSpeedType.RUN:
										m_stSpeed = m_Run;
										break;
								case GDMotion.eSpeedType.SLOW:
										m_stSpeed = m_Slow;
										break;
								case GDMotion.eSpeedType.USER:
										m_stSpeed = m_User;
										break;
						}

						if (a_bDir)
						{
								duRetCode = CAXM.AxmMoveVel(m_iAxisNo, m_stSpeed.m_dMaxSpeed, m_stSpeed.m_dAcceleration, m_stSpeed.m_dDeceleration);
								if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
										return false;
						}
						else
						{
								duRetCode = CAXM.AxmMoveVel(m_iAxisNo, -m_stSpeed.m_dMaxSpeed, m_stSpeed.m_dAcceleration, m_stSpeed.m_dDeceleration);
								if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
										return false;
						}
#endif


						return true;

				}

				public bool Move_Jog(bool a_bDir, GDMotion.eSpeedType a_eSpeedType, double a_fSpeedPercent)
				{
#if !SIM
						if (a_fSpeedPercent > 1.0 || a_fSpeedPercent < 0.0)
								return false;

						CAXM.AxmMotSetMinVel(m_iAxisNo, 0);

						uint duRetCode = 0;

						GDMotion.stMotionSpeedSetting m_stSpeed = new GDMotion.stMotionSpeedSetting();

						switch (a_eSpeedType)
						{
								case GDMotion.eSpeedType.FAST:
										m_stSpeed = m_Fast;
										break;
								case GDMotion.eSpeedType.RUN:
										m_stSpeed = m_Run;
										break;
								case GDMotion.eSpeedType.SLOW:
										m_stSpeed = m_Slow;
										break;
								case GDMotion.eSpeedType.USER:
										m_stSpeed = m_User;
										break;
						}

						m_stSpeed.m_dMaxSpeed *= a_fSpeedPercent;
						m_stSpeed.m_dAcceleration *= a_fSpeedPercent;
						m_stSpeed.m_dDeceleration *= a_fSpeedPercent;

						//duRetCode = CAXM.AxmOverrideSetMaxVel(m_lAxisNo, m_Main.m_dMaxSpeed * a_fSpeedPercent);

						if (a_bDir)
						{
								duRetCode = CAXM.AxmMoveVel(m_iAxisNo, m_stSpeed.m_dMaxSpeed, m_stSpeed.m_dAcceleration, m_stSpeed.m_dDeceleration);
								if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
										return false;
						}
						else
						{
								duRetCode = CAXM.AxmMoveVel(m_iAxisNo, -m_stSpeed.m_dMaxSpeed, m_stSpeed.m_dAcceleration, m_stSpeed.m_dDeceleration);
								if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
										return false;
						}
#endif

						return true;
				}

				public bool Move_Jog(bool a_bDir, double a_dVelocity, double a_dAcc, double a_dDecel)
				{
#if !SIM
						uint duRetCode = 0;

						CAXM.AxmMotSetMinVel(m_iAxisNo, 0);

						if (a_bDir)
						{
								duRetCode = CAXM.AxmMoveVel(m_iAxisNo, a_dVelocity, a_dAcc, a_dDecel);
								if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
										return false;
						}
						else
						{
								duRetCode = CAXM.AxmMoveVel(m_iAxisNo, -a_dVelocity, a_dAcc, a_dDecel);
								if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
										return false;
						}
#endif
						return true;
				}

				public bool Move_Relative(double a_dPos, GDMotion.stMotionSpeedSetting a_stSpeed)
				{

#if !SIM
						if (IsMotionDone == false)
								return false;
						uint duRetCode = 0;

						//if S-Curve
						//
						//CAXM.AxmMotSetAccelJerk(m_iAxisNo, 50);
						//CAXM.AxmMotSetDecelJerk(m_iAxisNo, 50);

						//++ 지정 축의 구동 좌표계를 설정합니다. 
						// dwAbsRelMode : (0)POS_ABS_MODE - 현재 위치와 상관없이 지정한 위치로 절대좌표 이동합니다.
						//                (1)POS_REL_MODE - 현재 위치에서 지정한 양만큼 상대좌표 이동합니다.
						duRetCode = CAXM.AxmMotSetAbsRelMode(m_iAxisNo, (uint)GDMotion.eMoveMode.MOVE_REL);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMotSetProfileMode(m_iAxisNo, m_stParamSetting.UiProfileMode);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						if (CAXM.AxmMotSetMinVel(m_iAxisNo, 0) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMoveStartPos(m_iAxisNo, a_dPos, a_stSpeed.m_dMaxSpeed, a_stSpeed.m_dAcceleration, a_stSpeed.m_dDeceleration);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#else
						if (m_stPositionStatus.fActPos+a_dPos >= 0)
						{
								m_stPositionStatus.fActPos += a_dPos;
								m_stPositionStatus.fCmdPos  = m_stPositionStatus.fActPos;
								m_bIsMotionDone = true;
								m_stMotionInfoStatus.m_bIsInPos = true;
						}
#endif
						return true;
				}

				public bool Move_Relative(double a_dPos, GDMotion.eSpeedType a_eSpeedType)
				{

#if !SIM
						if (IsMotionDone == false)
								return false;
						uint duRetCode = 0;

						//if S-Curve
						//
						//CAXM.AxmMotSetAccelJerk(m_iAxisNo, 50);
						//CAXM.AxmMotSetDecelJerk(m_iAxisNo, 50);

						//++ 지정 축의 구동 좌표계를 설정합니다. 
						// dwAbsRelMode : (0)POS_ABS_MODE - 현재 위치와 상관없이 지정한 위치로 절대좌표 이동합니다.
						//                (1)POS_REL_MODE - 현재 위치에서 지정한 양만큼 상대좌표 이동합니다.
						duRetCode = CAXM.AxmMotSetAbsRelMode(m_iAxisNo, (uint)GDMotion.eMoveMode.MOVE_REL);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMotSetProfileMode(m_iAxisNo, m_stParamSetting.UiProfileMode);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						if (CAXM.AxmMotSetMinVel(m_iAxisNo, 0) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;


						GDMotion.stMotionSpeedSetting m_stSpeed = new GDMotion.stMotionSpeedSetting();

						switch (a_eSpeedType)
						{
								case GDMotion.eSpeedType.FAST:
										m_stSpeed = m_Fast;
										break;
								case GDMotion.eSpeedType.RUN:
										m_stSpeed = m_Run;
										break;
								case GDMotion.eSpeedType.SLOW:
										m_stSpeed = m_Slow;
										break;
								case GDMotion.eSpeedType.USER:
										m_stSpeed = m_User;
										break;
						}

						duRetCode = CAXM.AxmMoveStartPos(m_iAxisNo, a_dPos, m_stSpeed.m_dMaxSpeed, m_stSpeed.m_dAcceleration, m_stSpeed.m_dDeceleration);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#else
						if (m_stPositionStatus.fActPos + a_dPos >= 0)
						{
								m_stPositionStatus.fActPos += a_dPos;
								m_stPositionStatus.fCmdPos = m_stPositionStatus.fActPos;
								m_bIsMotionDone = true;
								m_stMotionInfoStatus.m_bIsInPos = true;
						}
#endif
						return true;
				}

				public bool Move_Relative(double a_dPos, double a_dVelocity, double a_dAcc, double a_dDecel)
				{

#if !SIM
						if (IsMotionDone == false)
								return false;

						uint duRetCode = 0;

						//if S-Curve
						//
						//CAXM.AxmMotSetAccelJerk(m_iAxisNo, 50);
						//CAXM.AxmMotSetDecelJerk(m_iAxisNo, 50);

						//++ 지정 축의 구동 좌표계를 설정합니다. 
						// dwAbsRelMode : (0)POS_ABS_MODE - 현재 위치와 상관없이 지정한 위치로 절대좌표 이동합니다.
						//                (1)POS_REL_MODE - 현재 위치에서 지정한 양만큼 상대좌표 이동합니다.

						duRetCode = CAXM.AxmMotSetAbsRelMode(m_iAxisNo, (uint)GDMotion.eMoveMode.MOVE_REL);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMotSetProfileMode(m_iAxisNo, m_stParamSetting.UiProfileMode);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						if (CAXM.AxmMotSetMinVel(m_iAxisNo, 0) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;

						duRetCode = CAXM.AxmMoveStartPos(m_iAxisNo, a_dPos, a_dVelocity, a_dAcc, a_dDecel);
						if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
								return false;
#else
						if (m_stPositionStatus.fActPos + a_dPos >= 0)
						{
								m_stPositionStatus.fActPos += a_dPos;
								m_stPositionStatus.fCmdPos = m_stPositionStatus.fActPos;
								m_bIsMotionDone = true;
								m_stMotionInfoStatus.m_bIsInPos = true;
						}
#endif
						return true;
				}
				#endregion  Moving Func

				#region     Read Motion Status

				public void ReadMotorStatus()
				{
#if !SIM
						/*
	        Structure[0], Command 위치 [Mask : 0x01]
	        Structure[1], Encorder 위치 [Mask : 0x02]
	        Structure[2], Mechanical Signal [Mask : 0x04]
	        Structure[3], Driver Status [Mask : 0x08]  -> AxmStatusReadMotion 동일내용 
	        Structure[4], Universal Signal Input [Mask : 0x10]
	        Structure[5], U niversal Signal Output [Mask : 0x 1 1]
	        Structure[6], 읽기 설정 Mask
	        */
						uint iServoOn = 0;
						m_AXT_StatusInfo.uMask = 0x1F;
						//[00h] 모션 구동 하지 않음
						//[01h] 모션 구동 중
						if (CAXM.AxmStatusReadMotionInfo(m_iAxisNo, ref m_AXT_StatusInfo) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
						{
								return;
						}
						/*
                - [00000001h]Bit 0, BUSY(드라이브 구동 중)
                - [00000002h]Bit 1, DOWN(감속 중)
                - [00000004h] Bit 2, CONST(등속 중)
                - [00000008h] Bit 3, UP(가속 중)
                - [00000010h] Bit 4, 연속 드라이브 구동 중
                - [00000020h] Bit 5, 지정 거리 드라이브 구동 중
                - [00000040h] Bit 6, MPG 드라이브 구동 중
                - [00000080h] Bit 7, 원점 검색 드라이브 구동 중
                - [00000100h] Bit 8, 신호 검색 드라이브 구동 중
                - [00000200h] Bit 9, 보간 드라이브 구동 중
                - [00000400h] Bit 10, Slave 드라이브 구동 중
                - [00000800h] Bit 11,현재 구동 드라이브 방향 (보간 드라이브에서는 표시 정보 Library User Manual Rev.3.0 434 AJINEXTEK CO.,LTD.다름)
                - [00001000h] Bit 12, 펄스 출력 후 서보 위치 완료 신호 대기 중
                - [00002000h] Bit 13, 직선 보간 드라이브 구동 중
                - [00004000h] Bit 14, 원호 보간 드라이브 구동 중
                - [00008000h] Bit 15, 펄스 출력 중
                - [00010000h] Bit 16, 구동 예약 데이터 개수(처음)(0-7)
                - [00020000h] Bit 17, 구동 예약 데이터 개수(중간)(0-7)
                - [00040000h] Bit 18, 구동 예약 데이터 개수(끝)(0-7)
                - [00080000h] Bit 19, 구동 예약 Queue 비어있음
                - [00100000h] Bit 20, 구동 예약 Queue 가득 참
                - [00200000h] Bit 21, 현재 구동 드라이브의 속도 모드(처음)
                - [00400000h] Bit 22, 현재 구동 드라이브의 속도 모드(끝)
                - [00800000h] Bit 23, MPG 버퍼 #1 Full
                - [01000000h] Bit 24, MPG 버퍼 #2 Full
                - [02000000h] Bit 25, MPG 버퍼 #3 Full
                - [04000000h] Bit 26, MPG 버퍼 데이터 OverFlow           
             */
						m_bIsMotionDone = !Convert.ToBoolean((m_AXT_StatusInfo.uDrvStat >> 0) & 0x01);
						m_bJogging = Convert.ToBoolean((m_AXT_StatusInfo.uDrvStat >> 4) & 0x01);
						/*		 
						-[00001h] Bit 0, +Limit 급정지신호현재상태 o
						-[00002h] Bit 1, -Limit 급정지신호현재상태 o
						-[00004h] Bit 2, +limit 감속정지현재상태   x
						-[00008h] Bit 3, -limit 감속정지현재상태   x
						-[00010h] Bit 4, Alarm 신호신호현재상태    o
						-[00020h] Bit 5, InPos 신호현재상태        o
						-[00040h] Bit 6, 비상정지신호(ESTOP) 현재상태
						-[00080h] Bit 7, 원점신호헌재상태
						-[00100h] Bit 8, Z 상입력신호현재상태
						*/
						m_stMotionInfoStatus.m_bIsLimitP = Convert.ToBoolean((m_AXT_StatusInfo.uMechSig >> 0) & 0x01);
						m_stMotionInfoStatus.m_bIsLimitN = Convert.ToBoolean((m_AXT_StatusInfo.uMechSig >> 1) & 0x01);
						m_stMotionInfoStatus.m_bIsAlarm = Convert.ToBoolean((m_AXT_StatusInfo.uMechSig >> 4) & 0x01);
						m_stMotionInfoStatus.m_bIsInPos = Convert.ToBoolean((m_AXT_StatusInfo.uMechSig >> 5) & 0x01);
						m_stMotionInfoStatus.m_bIsEStop = Convert.ToBoolean((m_AXT_StatusInfo.uMechSig >> 6) & 0x01);
						m_stMotionInfoStatus.m_bIsOrg = Convert.ToBoolean((m_AXT_StatusInfo.uMechSig >> 7) & 0x01);
						m_stMotionInfoStatus.m_bIsZPhase = Convert.ToBoolean((m_AXT_StatusInfo.uMechSig >> 8) & 0x01);

						if (m_stMotionInfoStatus.m_bIsAlarm)
								m_bHomeComplete = false;

						if (CAXM.AxmSignalIsServoOn(m_iAxisNo, ref iServoOn) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
						{
								m_bServoOn = iServoOn > 0 ? true : false;
								if (!m_bServoOn)
										m_bHomeComplete = false;
						}
						m_bIsMotionDone = Convert.ToBoolean(m_bIsMotionDone & m_stMotionInfoStatus.m_bIsInPos);
#endif
				}

				public void ReadPositionStatus()
				{
						double dCmdPos = 0.0, dCmdVel = 0.0, dActPos = 0.0, dpError = 0.0;
#if !SIM
						//++ 지정한 축의 지령(Command)위치를 반환합니다.
						CAXM.AxmStatusGetCmdPos(m_iAxisNo, ref dCmdPos);
						//++ 지정한 축의 실제(Feedback)위치를 반환합니다.
						CAXM.AxmStatusGetActPos(m_iAxisNo, ref dActPos);
						//++ 지정한 축의 구동 속도를 반환합니다.
						CAXM.AxmStatusReadVel(m_iAxisNo, ref dCmdVel);
						//++ Command Pos과 Actual Pos의 차를 확인
						CAXM.AxmStatusReadPosError(m_iAxisNo, ref dpError);

						if (m_stSignalSetting.UiEncoderType == 2)
						{
								dActPos = dCmdPos;
								dpError = dCmdPos - dActPos;
						}
						m_stPositionStatus.fCmdPos = dCmdPos;
						m_stPositionStatus.fActPos = dActPos;
						m_stPositionStatus.fVelocity = dCmdVel;
						m_stPositionStatus.fErrPos = dpError;
#endif


				}
				#endregion  Read Motion Status

				#region AXT Setting 
				public void AllParamSetting()
				{
						ApplyConfig_ParamSetting();
						ApplyConfig_HomeSearchSetting();
						ApplyConfig_SignalSetting();
						ApplyConfig_UserMoveParamSetting();
						ApplyConfig_SWLimitSetting();
				}

				public void ApplyConfig_ParamSetting()
				{
						//Set the motor parameter
						int nPulse = 1;
						double dUnit = 1.0, dMinVel = 1.0, dMaxVel = 100.0;
						uint duMethod = 0, duAbsRelMode = 0, duProfileMode = 0;
#if !SIM
						duMethod = m_stParamSetting.UiPulseOutput_Mode;
						//++ 지정 축의 펄스 출력 방식을 설정합니다.
						//uMethod : (0)OneHighLowHigh   - 1펄스 방식, PULSE(Active High), 정방향(DIR=Low)  / 역방향(DIR=High)
						//          (1)OneHighHighLow   - 1펄스 방식, PULSE(Active High), 정방향(DIR=High) / 역방향(DIR=Low)
						//          (2)OneLowLowHigh    - 1펄스 방식, PULSE(Active Low),  정방향(DIR=Low)  / 역방향(DIR=High)
						//          (3)OneLowHighLow    - 1펄스 방식, PULSE(Active Low),  정방향(DIR=High) / 역방향(DIR=Low)
						//          (4)TwoCcwCwHigh     - 2펄스 방식, PULSE(CCW:역방향),  DIR(CW:정방향),  Active High     
						//          (5)TwoCcwCwLow      - 2펄스 방식, PULSE(CCW:역방향),  DIR(CW:정방향),  Active Low     
						//          (6)TwoCwCcwHigh     - 2펄스 방식, PULSE(CW:정방향),   DIR(CCW:역방향), Active High
						//          (7)TwoCwCcwLow      - 2펄스 방식, PULSE(CW:정방향),   DIR(CCW:역방향), Active Low
						//          (8)TwoPhase         - 2상(90' 위상차),  PULSE lead DIR(CW: 정방향), PULSE lag DIR(CCW:역방향)
						//          (9)TwoPhaseReverse  - 2상(90' 위상차),  PULSE lead DIR(CCW: 정방향), PULSE lag DIR(CW:역방향)
						CAXM.AxmMotSetPulseOutMethod(m_iAxisNo, duMethod);

						duMethod = m_stParamSetting.UiEncoderInput_Mode;
						//++ 지정 축의 Encoder 입력 방식을 설정합니다.
						// uMethod : (0)ObverseUpDownMode - 정방향 Up/Down
						//           (1)ObverseSqr1Mode   - 정방향 1체배
						//           (2)ObverseSqr2Mode   - 정방향 2체배
						//           (3)ObverseSqr4Mode   - 정방향 4체배
						//           (4)ReverseUpDownMode - 역방향 Up/Down
						//           (5)ReverseSqr1Mode   - 역방향 1체배
						//           (6)ReverseSqr2Mode   - 역방향 2체배
						//           (7)ReverseSqr4Mode   - 역방향 4체배
						CAXM.AxmMotSetEncInputMethod(m_iAxisNo, duMethod);

						duAbsRelMode = m_stParamSetting.UiAbsRelMode;
						//++ 지정 축의 구동 좌표계를 설정합니다. 
						// duAbsRelMode : (0)POS_ABS_MODE - 현재 위치와 상관없이 지정한 위치로 절대좌표 이동합니다.
						//                (1)POS_REL_MODE - 현재 위치에서 지정한 양만큼 상대좌표 이동합니다.
						CAXM.AxmMotSetAbsRelMode(m_iAxisNo, duAbsRelMode);

						duProfileMode = m_stParamSetting.UiProfileMode;
						//++ 지정 축의 구동 속도 프로파일 모드를 설정합니다.
						// uProfileMode : (0)SYM_TRAPEZOID_MODE  - Symmetric Trapezoid
						//                (1)ASYM_TRAPEZOID_MODE - Asymmetric Trapezoid
						//                (2)QUASI_S_CURVE_MODE  - Symmetric Quasi-S Curve
						//                (3)SYM_S_CURVE_MODE    - Symmetric S Curve
						//                (4)ASYM_S_CURVE_MODE   - Asymmetric S Curve
						CAXM.AxmMotSetProfileMode(m_iAxisNo, duProfileMode);

						dMinVel = m_stParamSetting.m_dMinVel;
						//++ 지정 축의 초기 구동속도를 설정합니다.
						CAXM.AxmMotSetMinVel(m_iAxisNo, dMinVel);

						dMaxVel = m_stParamSetting.m_dMaxVel;
						//++ 지정 축의 최대 구동속도를 설정합니다.
						CAXM.AxmMotSetMaxVel(m_iAxisNo, dMaxVel);

						nPulse = m_stParamSetting.m_iPulse; //pulse
						dUnit = m_stParamSetting.m_dUnit; //um
																							//++ 지정 축의 거리/속도/가속도의 제어단위를 설정합니다
						uint nErr = CAXM.AxmMotSetMoveUnitPerPulse(m_iAxisNo, dUnit, nPulse);
#endif
				}

				// "Input/Output Signal Setting"
				public void ApplyConfig_SignalSetting()
				{
						/* Motion Signal
						 * 1. Motion Signal : 기구부의 구동에 대한 상태를 알려주는 각종 센서와 서보 드라이브의 상태를 알려주는 신호들로 구성.
						 * 2. Universal Input Signal : 사용자가 임의로 사용 가능한 입력 신호들
						 * 3. Universal Output Signal : 사용자가 임의로 사용이 가능할 출력 신호들.
						 * 
						*/

						uint duUse = 0, duLevel = 0, duEncoderType = 0, duRetCode;
						uint duStopMode = 0, duPosigitaveLevel = 0, duNegativeLevel = 0;

						duUse = m_stSignalSetting.UiInPostion_ActiveLevel;

						//++ 지정 축의 Inposition(위치결정완료) 신호 Active Level/사용유무를 설정합니다.
						// - Inposition 신호를 사용안함으로 설정하면 모션제어 칩에서 펄스출력이 완료될 때 즉시구동 종료됩니다.
						// ※ [CAUTION] Inposition 신호를 사용함으로 설정하면 모션제어 칩에서 펄스출력이 완료된 후 서보팩으로 부터 
						//              Inposition(위치결정완료) 신호가 Active될 때 까지 모션 구동중으로 됩니다.
						// ※ [CAUTION] Inposition 신호를 사용할 때 Active Level이 맞지않으면 최초 한번 구동 후 모션구동이 종료되지않아 
						//              다음 구동을 할 수 없게 됩니다. 
#if !SIM
						CAXM.AxmSignalSetInpos(m_iAxisNo, duUse);
#endif

						duLevel = m_stSignalSetting.UiServoAlarmReset_ActiveLevel;
						//++ 지정 축의 Alarm Reset 신호 Active Level을 설정합니다.

#if !SIM
						CAXM.AxmSignalSetServoAlarmResetLevel(m_iAxisNo, duLevel);
#endif

						duStopMode = m_stSignalSetting.UiEMGStop_Mode;
						duLevel = m_stSignalSetting.UiEMGStop_ActiveLevel;
						//++ 지정 축의 Emergency 신호 Active Level/사용유무를 설정합니다.
#if !SIM
						CAXM.AxmSignalSetStop(m_iAxisNo, duStopMode, duLevel);
#endif

						//++ 지정 축의 (+/-) End Limit 신호 Active Level/사용유무를 확인합니다.
						duNegativeLevel = m_stSignalSetting.UiNegative_ActiveLevel;
						duPosigitaveLevel = m_stSignalSetting.UiPositive_ActiveLevel;
						//++ 지정 축의 (-) End Limit 신호 Active Level/사용유무를 설정합니다.
#if !SIM
						CAXM.AxmSignalSetLimit(m_iAxisNo, duStopMode, duPosigitaveLevel, duNegativeLevel);
#endif

						duLevel = m_stSignalSetting.UiZPhase_ActiveLevel;
						//++ 지정 축의 Z상 Active Level을 설정합니다.
#if !SIM
						CAXM.AxmSignalSetZphaseLevel(m_iAxisNo, duLevel);
#endif



						duLevel = m_stSignalSetting.UiServoOn_ActiveLevel;
						//++ 지정 축의 Servo On/Off 신호 Active Level을 설정합니다.
#if !SIM
						CAXM.AxmSignalSetServoOnLevel(m_iAxisNo, duLevel);
#endif


						duUse = m_stSignalSetting.UiServoAlarm_ActiveLevel;
						//++ 지정 축의 Alarm Reset 신호 Active Level을 설정합니다.
#if !SIM
						CAXM.AxmSignalSetServoAlarm(m_iAxisNo, duUse);
#endif
						duEncoderType = m_stSignalSetting.UiEncoderType;
						//++ 지정 축의 Encoder Input Type를 설정합니다.
						// duEncoderType : (0)TYPE_INCREMENTAL
						//                 (1)TYPE_ABSOLUTE
#if !SIM
						//pulse type에는 사용되지 않음.
						IniFile Ini = new IniFile(GDMotion.ConfigMotionSigFileFullName);
						Ini.Write(((GDMotion.eMotorName)m_iAxisNo).ToString(), "Encoder Type", m_stSignalSetting.UiEncoderType);

						CAXDev.AxmSignalSetEncoderType(m_iAxisNo, duEncoderType);
#endif
				}
				//"Home Search Setting"
				public void ApplyConfig_HomeSearchSetting()
				{
						int lHomeDir = 0;
						uint duHomeSignal = 0, duHomeLevel = 0, duZphas = 0;
						double dHomeClrTime = 1000.0, dHomeOffset = 0.0;

						duHomeLevel = m_stHomeSearchSetting.UiHomeSignal_ActiveLevel;
						//++ 지정 축의 원점신호 Active Level을 설정합니다.
#if !SIM
						CAXM.AxmHomeSetSignalLevel(m_iAxisNo, duHomeLevel);
#endif

						duHomeSignal = m_stHomeSearchSetting.UiHomeSignal;
						lHomeDir = m_stHomeSearchSetting.m_iHomeDir;
						duZphas = m_stHomeSearchSetting.UiHomeZPhase;

						dHomeClrTime = m_stHomeSearchSetting.m_dHomeClrTime;
						dHomeOffset = m_stHomeSearchSetting.m_dHomeOffset;
						//++ 지정 축의 원점검색 관련 정보들을 설정합니다.

#if !SIM
						CAXM.AxmHomeSetMethod(m_iAxisNo, lHomeDir, duHomeSignal, duZphas, dHomeClrTime, dHomeOffset);
#endif

						uint duRetCode = 0;
						double dVelFirst, dVelSecond, dVelThird, dVelLast, dAccFirst, dAccSecond;

						// 각각의 Edit 콘트롤에서 설정값을 가져옴
						dVelFirst = m_stHomeSearchSetting.m_dVelFirst;
						dVelSecond = m_stHomeSearchSetting.m_dVelSecond;
						dVelThird = m_stHomeSearchSetting.m_dVelThird;
						dVelLast = m_stHomeSearchSetting.m_dVelLast;
						dAccFirst = m_stHomeSearchSetting.m_dAccFirst;
						dAccSecond = m_stHomeSearchSetting.m_dAccSecond;

						//++ 원점검색에 사용되는 단계별 속도를 설정합니다.
#if !SIM
						duRetCode = CAXM.AxmHomeSetVel(m_iAxisNo, dVelFirst, dVelSecond, dVelThird, dVelLast, dAccFirst, dAccSecond);
#endif

				}
				// "Software Limit Setting "
				public void ApplyConfig_SWLimitSetting()
				{
						uint uRetCode;
						uint uUse = 0, uStopMode = 1, uSelection = 1;
						double dPositivePos = 0.0, dNegativePos = 0.0;

						//++ 지정한 축에 Software Limit기능을 확인합니다.
						//duRetCode = CAXM.AxmSignalGetSoftLimit(m_iAxisNo, ref duUse, ref duStopMode, ref duSelection, ref dPositivePos, ref dNegativePos);
						//if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
						//{
						uUse = m_stSWLimitSetting.UUse;
						uStopMode = m_stSWLimitSetting.UStopMode;
						uSelection = m_stSWLimitSetting.USelection;
						dNegativePos = m_stSWLimitSetting.m_dSWLimitN;
						dPositivePos = m_stSWLimitSetting.m_dSWLimitP;
						//++ 지정 축의 소프트웨어 리미트를 설정합니다.
						// uUse       : (0)DISABLE        - 소프트웨어 리미트 기능을 사용하지 않습니다.
						//              (1)ENABLE         - 소프트웨어 리미트 기능을 사용합니다.
						// uStopMode  : (0)EMERGENCY_STOP - 소프트웨어 리미트 영역을 벗어날 경우 급정지합니다.
						//              (1)SLOWDOWN_STOP  - 소프트웨어 리미트 영역을 벗어날 경우 감속정지합니다.
						// uSelection : (0)COMMAND        - 기준위치를 지령위치로 합니다.
						//              (1)ACTUAL         - 기준위치를 엔코더 위치로 합니다.
#if !SIM
						CAXM.AxmSignalSetSoftLimit(m_iAxisNo, uUse, uStopMode, uSelection, dPositivePos, dNegativePos);
#endif
						//}
				}
				//"User Move Parameter Setting 
				public void ApplyConfig_UserMoveParamSetting()
				{
						/*
						 * AxmMotSaveParaAll
				로 저장되는 파라메타중 28 31 까지 파라메타
				dInitPos, dInitVel, dInitAccel, dInitDecel 파라메 타만
				AxmMotSetParaLoad 를 사용하여 설정한다
				*/
						double dInitPos, dInitVel, dInitAccel, dInitDecel;

						dInitPos = m_stUserMoveParamSetting.m_dInitPos;
						dInitVel = m_stUserMoveParamSetting.m_dInitVel;
						dInitAccel = m_stUserMoveParamSetting.m_dInitAccel;
						dInitDecel = m_stUserMoveParamSetting.m_dInitDecel;

						//++ 지정 축의 사용자 관련 파라메타들을 설정합니다.
#if !SIM
						CAXM.AxmMotSetParaLoad(m_iAxisNo, dInitPos, dInitVel, dInitAccel, dInitDecel);
#endif
				}
				#endregion



				public void SetAlarmReset()
				{
#if !SIM
						//++ 지정 축의 Alarm Reset 신호 Active Level을 설정합니다.
						CAXM.AxmSignalServoAlarmReset(m_iAxisNo, 1);
						Task.Run(() =>
						{
								Thread.Sleep(10);
								CAXM.AxmSignalServoAlarmReset(m_iAxisNo, 0);
						});

						//CAXM.AxmSignalServoAlarmReset(m_iAxisNo, m_stSignalSetting.UiServoAlarmReset_ActiveLevel);
#else
						m_stMotionInfoStatus.m_bIsAlarm=false;
#endif
				}

				public void SetZeroPosition()
				{
#if !SIM
						//CAXM.AxmStatusSetCmdPos(m_lAxisNo, 0);
						//CAXM.AxmStatusSetActPos(m_lAxisNo, 0);
						//++ Command위치와 Actual위치를 입력한 값으로 설정합니다.
						CAXM.AxmStatusSetPosMatch(m_iAxisNo, 0.0);
#else
						m_stPositionStatus.fActPos=0;
						m_stPositionStatus.fCmdPos=0;
#endif
				}


				public string TranslateHomeResult(uint a_uHomeResult)
				{
						switch (a_uHomeResult)
						{
								case (uint)AXT_MOTION_HOME_RESULT.HOME_SUCCESS: m_strHomeSeachingResult = ("[01H] HOME_SUCCESS"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_SEARCHING: m_strHomeSeachingResult = ("([02H] HOME_SEARCHING"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_GNT_RANGE: m_strHomeSeachingResult = ("[10H] HOME_ERR_GNT_RANGE"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_USER_BREAK: m_strHomeSeachingResult = ("[11H] HOME_ERR_USER_BREAK"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_VELOCITY: m_strHomeSeachingResult = ("[12H] HOME_ERR_VELOCITY"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_AMP_FAULT: m_strHomeSeachingResult = ("[13H] HOME_ERR_AMP_FAULT"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_NEG_LIMIT: m_strHomeSeachingResult = ("[14H] HOME_ERR_NEG_LIMIT"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_POS_LIMIT: m_strHomeSeachingResult = ("[15H] HOME_ERR_POS_LIMIT"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_NOT_DETECT: m_strHomeSeachingResult = ("[16H] HOME_ERR_NOT_DETECT"); break;
								case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_UNKNOWN: m_strHomeSeachingResult = ("[FFH] HOME_ERR_UNKNOWN"); break;
						}
						return m_strHomeSeachingResult;
				}
		}
}
