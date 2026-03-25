using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
	public static class ACT_STATUS
	{
// 			public static bool IsFiberSensorDetected
// 			{
// 				get
// 				{
// 					if (M.DIOMgr != null)
// 					{
// 						return M.DIOMgr.GetItem(EzInaDIO.GDIO.eDIO_Type.INPUT).GetI(EzInaDIO.GDIO.eDI.X_009_SINGULATION_POS_CHK);
// 					}
// 
// 					return false;
// 				}
// 			}
// 			public static bool IsGuideBeamOn
// 			{
// 				get
// 				{
// 					if (M.MotionMgr == null)
// 						return false;
// 					MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 					if (Aero.GetOut(GDMotion.eMotorNameAero.M_U, GDMotion.eAeroO.Y08))
// 						return true;
// 					else
// 						return false;
// 				}
// 			}
// 
// 			public static bool IsFlowSensorDetected
// 			{
// 				get
// 				{
// 					//                 //Flow Sensor
// 					//                 double dValue = M.AIOMgr.GetItem(EzInaAIO.GAIO.eAIO_Type.INPUT).GetIVoltage(EzInaAIO.GAIO.eAI.X_002);
// 					//                 dValue = dValue > Rcp.SolderBallCheck_Flow_Level ? 1 : 0;
// 					//                 RTT.FlowSensor.DataAdd(dValue, DateTime.Now);
// 
// 					if (M.AIOMgr != null)
// 					{
// 						double dValue = M.AIOMgr.GetItem(EzInaAIO.GAIO.eAIO_Type.INPUT).GetIVoltage(EzInaAIO.GAIO.eAI.X_002);
// 						return dValue > Rcp.SolderBallCheck_Flow_Level ? true : false;
// 					}
// 
// 					return false;
// 				}
// 			}
// 
// 			public static bool IsA3200_Global0_Finished
// 			{
// 				get
// 				{
// 					if (M.MotionMgr != null)
// 					{
// 						MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 						double dValue = Aero.GetGlobalVariables(0);
// 						if (dValue.IsSame(1))
// 							return true;
// 						else
// 							return false;
// 					}
// 					return false;
// 				}
// 			}
// 
// 			public static bool IsLaserShotPgmLoaded(int iTask)
// 			{
// 				if (M.MotionMgr != null)
// 				{
// 					MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 					string strFile = Path.GetFileName(Rcp.PgmPath_LaserShot).ToUpper();
// 					string strCompare = Aero.GetNameOfPGM(iTask);
// 					if (strCompare == null)
// 						return false;
// 
// 					return strFile.Equals(strCompare.ToUpper());
// 				}
// 				return false;
// 			}
// 
// 			public static bool IsSingulationDiskRotationPgmLoaded(int iTask)
// 			{
// 				if (M.MotionMgr != null)
// 				{
// 					MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 					string strFile = Path.GetFileName(Rcp.PgmPath_OneStepMove).ToUpper();
// 					string strCompare = Aero.GetNameOfPGM(iTask);
// 					if (strCompare == null)
// 						return false;
// 
// 					return strFile.Equals(strCompare.ToUpper());
// 				}
// 				return false;
// 			}
// 
// 			public static bool IsReadyTaskState(int iTask)
// 			{
// 				if (M.MotionMgr != null)
// 				{
// 					MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 					TaskState status = Aero.GetTaskState_Enum(iTask);
// 
// 					if (status == TaskState.Error)
// 						return false;
// 					else
// 						return true;
// 
// 				}
// 
// 				return false;
// 			}
// 
// 
// 			#region [ FOR GUI ]
// 			public static void GetTaskState(this Control ctrl, int iTask)
// 			{
// 				try
// 				{
// 					if (M.MotionMgr != null)
// 					{
// 
// 						MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 						TaskState status = Aero.GetTaskState_Enum(iTask);
// 						if (status == TaskState.Error)
// 						{
// 							ctrl.BackColor = Color.Red;
// 							ctrl.ForeColor = Color.Black;
// 						}
// 						else
// 						{
// 							ctrl.BackColor = Color.Lime;
// 							ctrl.ForeColor = Color.Black;
// 						}
// 					}
// 				}
// 				catch (Exception ex)
// 				{
// 
// 				}
// 			}
// 			public static void GetTaskPgmInfo(this Control ctrl, int iTask)
// 			{
// 				try
// 				{
// 					if (M.MotionMgr != null)
// 					{
// 
// 						MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 						ctrl.Text = Aero.GetNameOfPGM(iTask);
// 						if (Aero.IsPgmAssociated(iTask))
// 						{
// 							ctrl.BackColor = Color.FromArgb(45, 45, 48);
// 							ctrl.ForeColor = Color.Lime;
// 						}
// 						else
// 						{
// 							ctrl.BackColor = Color.FromArgb(45, 45, 48);
// 							ctrl.ForeColor = Color.White;
// 						}
// 					}
// 				}
// 				catch (Exception ex)
// 				{
// 
// 				}
// 			}
// 
// 			public static void GetGlobalValiablesState(this Control ctrl, int iIndex)
// 			{
// 				try
// 				{
// 					if (M.MotionMgr != null)
// 					{
// 
// 						MotionAero Aero = (MotionAero)M.MotionMgr.GetItem(GDMotion.eMotionBrand.AEROTECH, (int)GDMotion.eMotorNameAero.M_U);
// 						double dValue = Aero.GetGlobalVariables(iIndex);
// 						if (dValue.IsSame(1))
// 						{
// 							ctrl.BackColor = Color.Lime;
// 							ctrl.ForeColor = Color.Black;
// 						}
// 						else
// 						{
// 							ctrl.BackColor = Color.FromArgb(45, 45, 48);
// 							ctrl.ForeColor = Color.White;
// 						}
// 					}
// 				}
// 				catch (Exception ex)
// 				{
// 
// 				}
// 			}
// 			#endregion [ FOR GUI ]
// 
// 			#region [ Get Current PosXY ]
// 			public static int GetPosX_DummyStage_R()
// 			{
// 				int nX = (int)M.RunMgr.m_dicWorksInfo[RD.eWorksInfor.R_Soldering].m_lCurrntCount % (int)Rcp.R_Stage_DummyShotZone_Arry_X;
// 				int nY = (int)M.RunMgr.m_dicWorksInfo[RD.eWorksInfor.R_Soldering].m_lCurrntCount / (int)Rcp.R_Stage_DummyShotZone_Arry_X;
// 
// 				if (nY % 2 != 0)//even
// 				{
// 					nX = (int)(Rcp.R_Stage_DummyShotZone_Arry_X - 1) - nX;
// 				}
// 
// 				return nX;
// 			}
// 
// 			public static int GetPosY_DummyStage_R()
// 			{
// 				int nY = (int)M.RunMgr.m_dicWorksInfo[RD.eWorksInfor.R_Soldering].m_lCurrntCount / (int)Rcp.R_Stage_DummyShotZone_Arry_X;
// 				return nY;
// 			}
// 
// 			public static int GetPosX_DummyStage_Vision_R()
// 			{
// 				int nX = (int)M.RunMgr.m_dicWorksInfo[RD.eWorksInfor.R_Soldering].m_VI_lCurrntCount % (int)Rcp.R_Stage_DummyShotZone_Arry_X;
// 				int nY = (int)M.RunMgr.m_dicWorksInfo[RD.eWorksInfor.R_Soldering].m_VI_lCurrntCount / (int)Rcp.R_Stage_DummyShotZone_Arry_X;
// 
// 				if (nY % 2 != 0)//even
// 				{
// 					nX = (int)(Rcp.R_Stage_DummyShotZone_Arry_X - 1) - nX;
// 				}
// 
// 				return nX;
// 			}
// 
// 			public static int GetPosY_DummyStage_Vision_R()
// 			{
// 				int nY = (int)M.RunMgr.m_dicWorksInfo[RD.eWorksInfor.R_Soldering].m_VI_lCurrntCount / (int)Rcp.R_Stage_DummyShotZone_Arry_X;
// 				return nY;
// 			}
// 
// 			#endregion [ Get Current PosXY]
// 
// 
// 			#region [ Map Update ]
// 
// 			private delegate void UpdateMapHandler(RD.eWorksInforMap a_eWorkInfo);
// 			private static event UpdateMapHandler OnUpdataMapHandler;
// 
// 			public static void CreateEventForMap(Action<RD.eWorksInforMap> a_Fun)
// 			{
// 				OnUpdataMapHandler += new UpdateMapHandler(a_Fun);
// 			}
// 			public static void UpdateMap(RD.eWorksInforMap a_eWorkInfor)
// 			{
// 				switch (a_eWorkInfor)
// 				{
// 					case RD.eWorksInforMap.L_Soldering:
// 
// 						break;
// 					case RD.eWorksInforMap.R_Soldering:
// 						M.ChipMgr.GetChip((int)RD.eWorksInfor.R_Soldering).GetMap(ACT_STATUS.GetPosX_DummyStage_R()
// 							, ACT_STATUS.GetPosY_DummyStage_R()).nIndex = M.RunMgr.SingulationDiskNo;
// 						M.ChipMgr.GetChip((int)RD.eWorksInfor.R_Soldering).GetMap(ACT_STATUS.GetPosX_DummyStage_R()
// 							, ACT_STATUS.GetPosY_DummyStage_R()).eType = CHIP.eChipStatus.CHIP_GOOD;
// 						break;
// 					case RD.eWorksInforMap.L_VisionInsp:
// 
// 						break;
// 					case RD.eWorksInforMap.R_VisionInsp:
// 
// 						break;
// 				}
// 
// 				OnUpdataMapHandler(a_eWorkInfor);
// 
// 
// 			}
// 			#endregion [ Map Update]
// 
	}
}
