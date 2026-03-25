using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
		public partial class RunningProcess
		{
				public enum eProcessInitStatus
				{
						IO_Check = 0
					, CYL_Check
					, Motor_Home
					, Max
				}

				public enum eProcessInitSeq
				{
						IO_Check_Start = 40
					, IO_Check_Finish
					, CYL_Check_Start = 50
					, CYL_Check_Finish
					, Motor_Home_Start = 60
					, Motor_Home_Finish
					, MAX
				}

		}
		public partial class RunningProcess : RunningBase
		{
				public Dictionary<eProcessInitStatus, FA.DEF.stRunModuleStatus> Interface_Init { get; set; }
				System.Diagnostics.Stopwatch m_TimeStamp = null;

				RunningScanner pRunningScanner = null;
				RunningStage pRunningStage = null;

				public RunningProcess(string a_strModuleName, int a_iModuleIndex) : base(a_strModuleName, a_iModuleIndex)
				{
						Interface_Init = new Dictionary<eProcessInitStatus, FA.DEF.stRunModuleStatus>();
						foreach (eProcessInitStatus item in Enum.GetValues(typeof(eProcessInitStatus)))
						{
								Interface_Init.Add(item, new FA.DEF.stRunModuleStatus());
						}
						m_TimeStamp = new System.Diagnostics.Stopwatch();
				}

				#region override
				public override bool InterfaceInitClear()
				{
						FA.DEF.stRunModuleStatus stWork = new FA.DEF.stRunModuleStatus();
						eProcessInitStatus item;
						for (int i = 0; i < Interface_Init.Count; i++)
						{
								item = (eProcessInitStatus)i;
								stWork = Interface_Init[item];
								stWork.Clear();
								Interface_Init[item] = stWork;
						}

						//             pRunningLeftStage	= (RunningLeftStage	)FA.MGR.RunMgr.GetItem("L_STAGE");
						//             pRunningRightStage	= (RunningRightStage)FA.MGR.RunMgr.GetItem("R_STAGE");

						return true;
				}
				public override void Init()
				{
						IsDone_Init = true;
				}
				#endregion

				private bool AddTimeStamp()
				{

						//             MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
						// 
						//             TC.AddData((int)FA.DEF.eSeries.Type_ValueVot_Keller		, M.AIOMgr.GetItem(EzInaAIO.GAIO.eAIO_Type.INPUT).GetIVoltage(EzInaAIO.GAIO.eAI.X_002));
						// 			TC.AddData((int)FA.DEF.eSeries.Type_ValueVot_1			, M.AIOMgr.GetItem(EzInaAIO.GAIO.eAIO_Type.INPUT).GetIVoltage(EzInaAIO.GAIO.eAI.X_000));
						// 			TC.AddData((int)FA.DEF.eSeries.Type_ValueVot_2			, M.AIOMgr.GetItem(EzInaAIO.GAIO.eAIO_Type.INPUT).GetIVoltage(EzInaAIO.GAIO.eAI.X_001));
						//             TC.AddData((int)FA.DEF.eSeries.Type_ValueMotorPos       , Aero.GetAxis((int)GDMotion.eMotorNameAero.M_U).m_stPositionStatus.m_fActPos);
						// 
						//             if (m_stRun.nSubStep >= (int)eSeqArray_R.CHK_ReloadPgmRunAndMove && m_stRun.nSubStep < (int)eSeqArray_R.CHK_LaserShotPgmRun)
						//                 TC.AddData((int)FA.DEF.eSeries.Type_OnOff_SDR, 1);
						//             else
						//                 TC.AddData((int)FA.DEF.eSeries.Type_OnOff_SDR, 0);
						// 
						// 			TC.AddData((int)FA.DEF.eSeries.Type_OnOff_U_Axis		, Aero.GetAxis((int)GDMotion.eMotorNameAero.M_U).bIsMotionDone	? 0 : 1);
						// 			TC.AddData((int)FA.DEF.eSeries.Type_OnOff_FiberSensor	, ACT_STATUS.IsFiberSensorDetected 								? 1 : 0);
						// 			TC.AddData((int)FA.DEF.eSeries.Type_OnOff_FlowSensor	, ACT_STATUS.IsFlowSensorDetected								? 1 : 0);
						// 
						//             if (m_stRun.nSubStep >= (int)eSeqArray_R.CHK_LaserShotPgmRun && m_stRun.nSubStep < (int)eSeqArray_R.CHK_LaserShotPgmRun + 3)
						//                 TC.AddData((int)FA.DEF.eSeries.Type_OnOff_LaserShot, 1);
						//             else
						//                 TC.AddData((int)FA.DEF.eSeries.Type_OnOff_LaserShot, 0);
						//             //TC.AddData((int)FA.DEF.eSeries.Type_OnOff_LaserShot		, M.RunMgr.IsCompletedTask(3)									? 0 : 1);
						return true;
				}



				private void UpdateInitStatus(eProcessInitStatus a_eItem, bool a_bCompleted)
				{
						if ((int)a_eItem < 0 || (int)a_eItem >= Interface_Init.Count)
								return;

						FA.DEF.stRunModuleStatus stWork = Interface_Init[a_eItem];
						if (a_bCompleted)
								stWork.SetFinish();
						else
								stWork.SetStart();

						Interface_Init[a_eItem] = stWork;
				}
				private void UpdateWorkStatus(eProcessInitStatus a_eItem, bool a_bStart, bool a_bFinish)
				{
						if ((int)a_eItem < 0 || (int)a_eItem >= Interface_Init.Count)
								return;

						FA.DEF.stRunModuleStatus stWork = Interface_Init[a_eItem];
						if (a_bStart)
								stWork.SetStart();
						else if (a_bFinish)
								stWork.SetFinish();

						Interface_Init[a_eItem] = stWork;
				}

				public override void Ready()
				{

				}

				public override bool Run()
				{
						m_stRun.bRunStop = true;
						return true;
				}

				public override void Stop()
				{

				}

				public override bool FindAction()
				{
						return false;
				}

				public override void ConnectingModule()
				{
						pRunningScanner = (RunningScanner)FA.MGR.RunMgr.GetItem("SCANNER");
						pRunningStage = (RunningStage)FA.MGR.RunMgr.GetItem("STAGE");
				}
		}
}
