using EzIna.FA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
		public enum eStageMappingResult
		{
				NONE,
				OK,
				NG_MATCH,
				NG_SPEC_OUT

		}
		public partial class RunningStage
		{

				#region [SEQ_STAGE_MAPPING]
				public enum eSeqStageMapping
				{
						Finish = 0
					, Basic_Settings_start = 10
					, VisionLive_start
					, VisionLive_finish
					, variables
					, illumination_start
					, illumination_finish
					, camera_expose_start
					, camera_expose_finish
					, vacuum
					, chk_GridClass_on_the_table
					, Basic_Settings_finish

					//Mapping 1st Pos
					, MoveTo_StartPos_prechk = 30
					, MoveTo_StartPos_start
					, MoveTo_StartPos_finish

					, StartPos_Grab_start
					, StartPos_Grab_finish
					, StartPos_Matching
					, MoveTo_NewStartPos_start
					, MoveTo_NewStartPos_finish

					//Mapping
					, Mapping_prechk = 40
					, Mapping_start

					//X Direction first
					, Mapping_X_Dir_First_Rows_loop_prechk = 50
					, Mapping_X_Dir_First_Rows_loop_start
					, Mapping_X_Dir_First_Cols_loop_prechk = 60
					, Mapping_X_Dir_First_Cols_loop_start
					, Mapping_X_Dir_First_Cols_loop_Move_start
					, Mapping_X_Dir_First_Cols_loop_Move_finish
					, Mapping_X_Dir_First_Grab_start
					, Mapping_X_Dir_First_Grab_finish
					, Mapping_X_Dir_First_Matching
					, Mapping_X_DIR_First_ManualTeaching
					, Mapping_X_Dir_First_Cols_loop_finish
					, Mapping_X_Dir_First_Rows_loop_finish

					//Y Direction first
					, Mapping_Y_Dir_First_Cols_loop_prechk = 70
					, Mapping_Y_Dir_First_Cols_loop_start
					, Mapping_Y_Dir_First_Rows_loop_prechk = 80
					, Mapping_Y_Dir_First_Rows_loop_start
					, Mapping_Y_Dir_First_Rows_loop_Move_start
					, Mapping_Y_Dir_First_Rows_loop_Move_finish
					, Mapping_Y_Dir_First_Grab_start
					, Mapping_Y_Dir_First_Grab_finish
					, Mapping_Y_Dir_First_Matching
					, Mapping_Y_DIR_First_ManualTeaching
					, Mapping_Y_Dir_First_Rows_loop_finish
					, Mapping_Y_Dir_First_Cols_loop_finish

					, Mapping_finish
				}
				#endregion


				#region [stage mapping]
				List<List<CMapping>> m_vecStageMap = null;

				double m_dStage2DCal_StartX;
				double m_dStage2DCal_StartY;
				int m_nStage2DCal_ColIndex;
				int m_nStage2DCal_RowIndex;
				int m_nStage2DCal_CurrCol;
				int m_nStage2DCal_CurrRow;
				int m_nStage2DCal_Columns;
				int m_nStage2DCal_Rows;


				bool m_bStage2DCal_Pause;
				bool m_bStage2DCal_Continue;


				//Manual teaching할 경우 사용되는 변수.
				public double dStage2DCal_ManualTeachingPosX { get; set; }
				public double dStage2DCal_ManualTeachingPosY { get; set; }

				public bool bStage2DCal_Pause
				{
						get { return m_bStage2DCal_Pause; }
						set { m_bStage2DCal_Pause = value; }
				}

				public bool bStage2DCal_continue
				{
						get { return m_bStage2DCal_Continue; }
						set { m_bStage2DCal_Continue = value; }
				}
				#endregion [stage mapping]

				#region [STAGE MAPPING]
				public void CreateStageMap()
				{
						DeleteStageMap();

						m_vecStageMap = new List<List<CMapping>>();

						m_nStage2DCal_Columns = (int)(FA.RCP.M100_StageCalParamMaxDistX.AsDouble / FA.RCP.M100_StageCalParamPitchX.AsDouble);
						m_nStage2DCal_Rows = (int)(FA.RCP.M100_StageCalParamMaxDistY.AsDouble / FA.RCP.M100_StageCalParamPitchY.AsDouble);
						for (int iRow = 0; iRow < m_nStage2DCal_Rows; iRow++)
						{
								m_vecStageMap.Add(new List<CMapping>());
								for (int iCol = 0; iCol < m_nStage2DCal_Columns; iCol++)
								{
										m_vecStageMap[iRow].Add(new CMapping());
										m_vecStageMap[iRow][iCol].Init();
								}
						}

						WinAPIs._PostMessageM(DEF.MSG_STAGE_2D_CAL_CREATE_MAP, m_nStage2DCal_Columns, m_nStage2DCal_Rows);
				}
				public void DeleteStageMap()
				{
						if (m_vecStageMap?.Count > 0)
								m_vecStageMap.Clear();

						m_vecStageMap = null;
				}

				public CMapping GetStage2DCalMap(int colidx, int rowidx)
				{
						if (m_vecStageMap == null)
								return null;

						if (m_vecStageMap.Count == 0)
								return null;

						if (rowidx >= m_vecStageMap.Count)
								return null;
						if (m_vecStageMap[rowidx].Count <= colidx)
								return null;

						return m_vecStageMap?[rowidx][colidx];
				}
				public bool SubSeq_StageMapping_XY()
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eSeqStageMapping>.From(m_stRun.nSubStep))
						{
								case eSeqStageMapping.Finish:
										{
												FA.LOG.Debug("SEQ", "Stage mapping finish");
												return true;
										}
								#region [STEP1 Basic_Settings]
								case eSeqStageMapping.Basic_Settings_start:
										{
												if (base.IsRunModeStopped()) break;
												FA.LOG.Debug("SEQ", "Stage mapping start");
												base.NextSubStep(eSeqStageMapping.VisionLive_start);
												break;
										}
								case eSeqStageMapping.VisionLive_start:
										{
												VISION.FINE_CAM.Live();
												base.NextSubStep(eSeqStageMapping.VisionLive_finish);
												break;
										}
								case eSeqStageMapping.VisionLive_finish:
										{
												if (!VISION.FINE_CAM.IsLive())
														break;

												base.NextSubStep(eSeqStageMapping.variables);
												break;
										}
								case eSeqStageMapping.variables:
										{
												base.NextSubStep(eSeqStageMapping.illumination_start);
												break;
										}
								case eSeqStageMapping.illumination_start:
										{
												//[To Do List] 옵션에 따라서 Coarse, Fine 구분할 것.
												FA.LIGHTSOURCE.RING.SetIntensity((ushort)FA.DEF.eVision.FINE, RCP.F_LIGHT_Source_Ring_For_Run.AsInt);
												FA.LIGHTSOURCE.SPOT.SetIntensity((ushort)FA.DEF.eVision.FINE, RCP.F_LIGHT_Source_Spot_For_Run.AsInt);
												m_stRun.stwatchForSub.SetDelay = 100;
												base.NextSubStep(eSeqStageMapping.illumination_finish);
												break;
										}
								case eSeqStageMapping.illumination_finish:
										{
												if (!m_stRun.stwatchForSub.IsDone)
														break;
												base.NextSubStep(eSeqStageMapping.camera_expose_start);
												break;
										}
								case eSeqStageMapping.camera_expose_start:
										{
												FA.VISION.FINE_CAM.SetExposeTime(FA.RCP.F_CAM_ExposeTime.AsInt);
												base.NextSubStep(eSeqStageMapping.camera_expose_finish);
												break;
										}
								case eSeqStageMapping.camera_expose_finish:
										{
												if (!VISION.FINE_CAM.GetExpose().IsSame(FA.RCP.F_CAM_ExposeTime.AsInt))
														break;
												base.NextSubStep(eSeqStageMapping.vacuum);
												break;
										}
								case eSeqStageMapping.vacuum:
										{
												//ACT.StageVauccmOn.Run(100);
												base.NextSubStep(eSeqStageMapping.chk_GridClass_on_the_table);
												break;
										}
								case eSeqStageMapping.chk_GridClass_on_the_table:
										{
												//if (!ACT.StageVauccmOn.Check() && !FA.OPT.DryRunningEnable)
												//	break;
												//base.NextSubStep(eSeqStageMapping.Basic_Settings_finish);
												break;
										}
								case eSeqStageMapping.Basic_Settings_finish:
										{
												base.NextSubStep(eSeqStageMapping.MoveTo_StartPos_prechk);
												break;
										}
								#endregion [STEP1 Basic_Settings]
								#region[STEP2 The Axes movet to start position ]
								case eSeqStageMapping.MoveTo_StartPos_prechk:
										{
												if (base.IsRunModeStopped()) break;
												m_dStage2DCal_StartX = RCP.M100_StageCalParamStartPosX.AsDouble;//AXIS.RX.Status().m_stPositionStatus.fActPos;
												m_dStage2DCal_StartY = RCP.M100_StageCalParamStartPosY.AsDouble;//AXIS.Y.Status().m_stPositionStatus.fActPos;
												base.NextSubStep(eSeqStageMapping.MoveTo_StartPos_start);
												break;
										}
								case eSeqStageMapping.MoveTo_StartPos_start:
										{
												if (!ACT.MoveABS(DEF.eAxesName.RX, m_dStage2DCal_StartX, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;
												if (!ACT.MoveABS(DEF.eAxesName.Y, m_dStage2DCal_StartY, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;
												base.NextSubStep(eSeqStageMapping.MoveTo_StartPos_finish);
												break;
										}
								case eSeqStageMapping.MoveTo_StartPos_finish:
										{
												if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
														break;
												if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS) ||
													!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
														break;

												m_stRun.stwatchForSub.SetDelay = FA.RCP.M100_StageCalParamMoveDelay.AsInt * 1000;
												base.NextSubStep(eSeqStageMapping.StartPos_Grab_start);
												break;
										}
								case eSeqStageMapping.StartPos_Grab_start:
										{
												if (!m_stRun.stwatchForSub.IsDone)
														break;
												VISION.FINE_CAM.Grab();
												base.NextSubStep(eSeqStageMapping.StartPos_Grab_finish);
												break;
										}
								case eSeqStageMapping.StartPos_Grab_finish:
										{
												if (!VISION.FINE_CAM.IsGrab())
														break;
												base.NextSubStep(eSeqStageMapping.StartPos_Matching);
												break;
										}
								case eSeqStageMapping.StartPos_Matching:
										{
												if (FA.OPT.DryRunningEnable)
												{
														RCP.M100_StageCalParamStartPosX.m_strValue = AXIS.RX.Status().m_stPositionStatus.fActPos.ToString();
														RCP.M100_StageCalParamStartPosY.m_strValue = AXIS.Y.Status().m_stPositionStatus.fActPos.ToString();
														base.NextSubStep(eSeqStageMapping.Mapping_prechk);
												}
												else if (VISION.FINE_LIB.MatchRun(FA.MGR.ProjectMgr.SelectedModelPath, EzInaVision.GDV.eGoldenImages.Fiducial_No1, EzInaVision.GDV.eRoiItems.ROI_No0) > 0)
														base.NextSubStep(eSeqStageMapping.MoveTo_NewStartPos_start);
												else
														m_stRun.TimeoutNow();
												break;
										}
								case eSeqStageMapping.MoveTo_NewStartPos_start:
										{
												if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
														break;
												double dPosX = 0.0, dPosY = 0.0;
												dPosX = AXIS.RX.Status().m_stPositionStatus.fActPos;
												dPosY = AXIS.Y.Status().m_stPositionStatus.fActPos;

												double dOffsetX = 0.0, dOffsetY = 0.0;
												dOffsetX = VISION.FINE_LIB.m_LibInfo.m_vecClosePointToCenter[(int)EzInaVision.GDV.eRoiItems.ROI_No0][(int)EzInaVision.GDV.eGoldenImages.Fiducial_No1].fPosX;
												dOffsetY = VISION.FINE_LIB.m_LibInfo.m_vecClosePointToCenter[(int)EzInaVision.GDV.eRoiItems.ROI_No0][(int)EzInaVision.GDV.eGoldenImages.Fiducial_No1].fPosY;

												dPosX += dOffsetX;
												dPosY += dOffsetY;

												if (!ACT.MoveABS(DEF.eAxesName.RX, dPosX, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;
												if (!ACT.MoveABS(DEF.eAxesName.Y, dPosY, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;

												m_stRun.stwatchForSub.SetDelay = RCP.M100_StageCalParamMoveDelay.AsInt;
												base.NextSubStep(eSeqStageMapping.MoveTo_NewStartPos_finish);
												break;
										}
								case eSeqStageMapping.MoveTo_NewStartPos_finish:
										{
												if (!m_stRun.stwatchForSub.IsDone)
														break;
												if (!AXIS.RX.Status().IsMotionDone || !AXIS.Y.Status().IsMotionDone)
														break;

												if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, FA.DEF.IN_POS) ||
													!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, FA.DEF.IN_POS))
														break;

												RCP.M100_StageCalParamStartPosX.m_strValue = AXIS.RX.Status().m_stPositionStatus.fActPos.ToString();
												RCP.M100_StageCalParamStartPosY.m_strValue = AXIS.Y.Status().m_stPositionStatus.fActPos.ToString();
												base.NextSubStep(eSeqStageMapping.Mapping_prechk);
												break;
										}
								#endregion[STEP2 The Axes movet to start position ]
								#region[STEP3 Create stage map]
								case eSeqStageMapping.Mapping_prechk:
										{
												if (base.IsRunModeStopped()) break;
												//여기서 맵핑데이터를 생성한다.
												CreateStageMap();
												base.NextSubStep(eSeqStageMapping.Mapping_start);
												break;
										}
								case eSeqStageMapping.Mapping_start:
										{
												if (OPT.StageCalParam_Y_Dir_FirstEnable)
														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Cols_loop_prechk);
												else
														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Rows_loop_prechk);

												m_nStage2DCal_RowIndex = 0;
												m_nStage2DCal_ColIndex = 0;
												m_nStage2DCal_CurrRow = 0;
												m_nStage2DCal_CurrCol = 0;
												break;
										}
								#endregion[STEP3 Create stage map]
								#region[STEP4 X direction first]
								case eSeqStageMapping.Mapping_X_Dir_First_Rows_loop_prechk:
										{
												if (m_nStage2DCal_CurrRow >= m_nStage2DCal_Rows)
														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Rows_loop_finish);
												else
														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Rows_loop_start);

												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Rows_loop_start:
										{
												m_nStage2DCal_CurrCol = 0;

												base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_prechk);
												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_prechk:
										{
												if (IsRunModeStopped())
														break;
												if (m_nStage2DCal_CurrCol >= m_nStage2DCal_Columns)
												{
														m_nStage2DCal_CurrRow++;
														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Rows_loop_prechk);
												}
												else
												{
														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_start);
												}

												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_start:
										{
												//Start Position : left, top 
												//zigzag moving
												double dMX = 0.0, dMY = 0.0;
												if (m_nStage2DCal_CurrRow % 2 == 0) //even
												{
														m_nStage2DCal_ColIndex = m_nStage2DCal_CurrCol;
														m_nStage2DCal_RowIndex = m_nStage2DCal_CurrRow;
												}
												else//odd
												{
														m_nStage2DCal_ColIndex = (m_nStage2DCal_Columns - m_nStage2DCal_CurrCol - 1);
														m_nStage2DCal_RowIndex = m_nStage2DCal_CurrRow;
												}

												dMX = m_nStage2DCal_ColIndex * RCP.M100_StageCalParamPitchX.AsDouble;
												dMX += RCP.M100_StageCalParamStartPosX.AsDouble;
												dMY = m_nStage2DCal_RowIndex * RCP.M100_StageCalParamPitchY.AsDouble;
												dMY += RCP.M100_StageCalParamStartPosY.AsDouble;

												if (!ACT.MoveABS(DEF.eAxesName.RX, dMX, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;
												if (!ACT.MoveABS(DEF.eAxesName.Y, dMY, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;

												m_stRun.stwatchForSub.SetDelay = RCP.M100_StageCalParamMoveDelay.AsInt;
												base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_Move_start);
												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_Move_start:
										base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_Move_finish);
										break;
								case eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_Move_finish:
										if (!AXIS.RX.Status().IsMotionDone) break;
										if (!AXIS.Y.Status().IsMotionDone) break;

										if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, DEF.IN_POS))
												break;
										if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, DEF.IN_POS))
												break;
										m_stRun.stwatchForSub.SetDelay = FA.RCP.M100_StageCalParamMoveDelay.AsInt * 1000;
										base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Grab_start);
										break;
								case eSeqStageMapping.Mapping_X_Dir_First_Grab_start:
										{
												if (!m_stRun.stwatchForSub.IsDone)
														break;

												if (!VISION.FINE_CAM.Grab())
														break;
												base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Grab_finish);
												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Grab_finish:
										{
												if (!VISION.FINE_CAM.IsGrab())
														break;
												base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Matching);
												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Matching:
										{
												if (FA.OPT.DryRunningEnable)
												{
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetX = 0;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetY = 0;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncX = AXIS.RX.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncY = AXIS.Y.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncZ = AXIS.RZ.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iRow = m_nStage2DCal_RowIndex;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iCol = m_nStage2DCal_ColIndex;

														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_INSPECTION;

														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_finish);
												}
												else if (VISION.FINE_LIB.MatchRun(FA.MGR.ProjectMgr.SelectedModelPath, EzInaVision.GDV.eGoldenImages.Fiducial_No1, EzInaVision.GDV.eRoiItems.ROI_No0) > 0)
												{

														double dOffsetX = 0.0, dOffsetY = 0.0;
														dOffsetX = VISION.FINE_LIB.m_LibInfo.m_vecClosePointToCenter[(int)EzInaVision.GDV.eRoiItems.ROI_No0][(int)EzInaVision.GDV.eGoldenImages.Fiducial_No1].fPosX;
														dOffsetY = VISION.FINE_LIB.m_LibInfo.m_vecClosePointToCenter[(int)EzInaVision.GDV.eRoiItems.ROI_No0][(int)EzInaVision.GDV.eGoldenImages.Fiducial_No1].fPosY;


														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetX = dOffsetX;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetY = dOffsetY;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncX = AXIS.RX.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncY = AXIS.Y.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncZ = AXIS.RZ.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iRow = m_nStage2DCal_RowIndex;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iCol = m_nStage2DCal_ColIndex;


														if (Math.Abs(dOffsetX) > RCP.M100_StageCalParamAccuracy.AsDouble ||
															 Math.Abs(dOffsetY) > RCP.M100_StageCalParamAccuracy.AsDouble)
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_SPEC_OUT;
														else
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.OK;

														WinAPIs._PostMessageM(DEF.MSG_STAGE_2D_CAL_SET_MEASURED_VALUE, m_nStage2DCal_ColIndex, m_nStage2DCal_RowIndex);
														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_finish);
												}
												else
												{
														m_bStage2DCal_Pause = true;
														WinAPIs._PostMessageM(FA.DEF.MSG_SHOW_ALARM, DEF.ALM_STAGE_2D_CAL_PAUSE);
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_INSPECTION;
														base.NextSubStep(eSeqStageMapping.Mapping_X_DIR_First_ManualTeaching);

												}
												WinAPIs._PostMessageM(DEF.MSG_STAGE_2D_CAL_SET_MEASURED_VALUE, m_nStage2DCal_ColIndex, m_nStage2DCal_RowIndex);
												break;
										}
								case eSeqStageMapping.Mapping_X_DIR_First_ManualTeaching:
										{
												m_stRun.bHoldOnTimeout = true;
												if (m_bStage2DCal_Continue)
												{
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetX = dStage2DCal_ManualTeachingPosX;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetY = dStage2DCal_ManualTeachingPosY;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncX = AXIS.RX.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncY = AXIS.Y.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iRow = m_nStage2DCal_RowIndex;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iCol = m_nStage2DCal_ColIndex;

														if (Math.Abs(dStage2DCal_ManualTeachingPosX) > RCP.M100_StageCalParamAccuracy.AsDouble ||
															 Math.Abs(dStage2DCal_ManualTeachingPosX) > RCP.M100_StageCalParamAccuracy.AsDouble)
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_SPEC_OUT;
														else
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.OK;

														WinAPIs._PostMessageM(DEF.MSG_STAGE_2D_CAL_SET_MEASURED_VALUE, m_nStage2DCal_ColIndex, m_nStage2DCal_RowIndex);
														WinAPIs._PostMessageM(DEF.MSG_HIDE_ALARM);
														base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_finish);

												}

												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_finish:
										{
												m_nStage2DCal_CurrCol++;
												m_bStage2DCal_Pause = false;
												m_bStage2DCal_Continue = false;
												base.NextSubStep(eSeqStageMapping.Mapping_X_Dir_First_Cols_loop_prechk);
												break;
										}
								case eSeqStageMapping.Mapping_X_Dir_First_Rows_loop_finish:
										{
												base.NextSubStep(eSeqStageMapping.Mapping_finish);
												break;
										}
								#endregion[STEP4 X direction first] 
								#region[STEP5 Y direction first]
								case eSeqStageMapping.Mapping_Y_Dir_First_Cols_loop_prechk:
										{
												if (m_nStage2DCal_CurrCol >= m_nStage2DCal_Columns)
														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Cols_loop_finish);
												else
														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Cols_loop_start);
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Cols_loop_start:
										{
												m_nStage2DCal_CurrRow = 0;

												base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_prechk);
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_prechk:
										{
												if (IsRunModeStopped())
														break;
												if (m_nStage2DCal_CurrRow >= m_nStage2DCal_Rows)
												{
														m_nStage2DCal_CurrCol++;
														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Cols_loop_prechk);
												}
												else
												{
														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_start);
												}
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_start:
										{
												//Start Position : left, top 
												//zigzag moving
												double dMX = 0.0, dMY = 0.0;
												if (m_nStage2DCal_CurrCol % 2 == 0) //even
												{
														m_nStage2DCal_ColIndex = m_nStage2DCal_CurrCol;
														m_nStage2DCal_RowIndex = m_nStage2DCal_CurrRow;
												}
												else//odd
												{
														m_nStage2DCal_ColIndex = m_nStage2DCal_CurrCol;
														m_nStage2DCal_RowIndex = (m_nStage2DCal_Rows - m_nStage2DCal_CurrRow - 1); ;
												}

												dMX = m_nStage2DCal_ColIndex * RCP.M100_StageCalParamPitchX.AsDouble;
												dMX += RCP.M100_StageCalParamStartPosX.AsDouble;
												dMY = m_nStage2DCal_RowIndex * RCP.M100_StageCalParamPitchY.AsDouble;
												dMY += RCP.M100_StageCalParamStartPosY.AsDouble;

												if (!ACT.MoveABS(DEF.eAxesName.RX, dMX, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;
												if (!ACT.MoveABS(DEF.eAxesName.Y, dMY, RCP.M100_StageCalParamStageVelocity.AsDouble, RCP.M100_StageCalParamStageAccel.AsDouble, RCP.M100_VisionCalParamStageAccel.AsDouble))
														break;

												m_stRun.stwatchForSub.SetDelay = RCP.M100_StageCalParamMoveDelay.AsInt;
												base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_Move_start);
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_Move_start:
										{
												base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_Move_finish);

										}
										break;
								case eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_Move_finish:
										{
												if (!AXIS.RX.Status().IsMotionDone) break;
												if (!AXIS.Y.Status().IsMotionDone) break;

												if (!AXIS.RX.Status().m_stPositionStatus.fActPos.IsSame(AXIS.RX.Status().dTargetCmd, DEF.IN_POS))
														break;
												if (!AXIS.Y.Status().m_stPositionStatus.fActPos.IsSame(AXIS.Y.Status().dTargetCmd, DEF.IN_POS))
														break;

												m_stRun.stwatchForSub.SetDelay = FA.RCP.M100_StageCalParamMoveDelay.AsInt * 1000;
												base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Grab_start);
										}
										break;
								case eSeqStageMapping.Mapping_Y_Dir_First_Grab_start:
										{
												if (!m_stRun.stwatchForSub.IsDone)
														break;

												if (!VISION.FINE_CAM.Grab())
														break;
												base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Grab_finish);
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Grab_finish:
										{
												if (!VISION.FINE_CAM.IsGrab())
														break;
												base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Matching);
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Matching:
										{
												if (FA.OPT.DryRunningEnable)
												{
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetX = 0;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetY = 0;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncX = AXIS.RX.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncY = AXIS.Y.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncZ = AXIS.RZ.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iRow = m_nStage2DCal_RowIndex;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iCol = m_nStage2DCal_ColIndex;

														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_INSPECTION;

														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_finish);
												}
												else if (VISION.FINE_LIB.MatchRun(FA.MGR.ProjectMgr.SelectedModelPath, EzInaVision.GDV.eGoldenImages.Fiducial_No1, EzInaVision.GDV.eRoiItems.ROI_No0) > 0)
												{
														double dOffsetX = 0.0, dOffsetY = 0.0;
														dOffsetX = VISION.FINE_LIB.m_LibInfo.m_vecClosePointToCenter[(int)EzInaVision.GDV.eRoiItems.ROI_No0][(int)EzInaVision.GDV.eGoldenImages.Fiducial_No1].fPosX;
														dOffsetY = VISION.FINE_LIB.m_LibInfo.m_vecClosePointToCenter[(int)EzInaVision.GDV.eRoiItems.ROI_No0][(int)EzInaVision.GDV.eGoldenImages.Fiducial_No1].fPosY;


														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetX = dOffsetX;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetY = dOffsetY;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncX = AXIS.RX.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncY = AXIS.Y.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iRow = m_nStage2DCal_RowIndex;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iCol = m_nStage2DCal_ColIndex;


														if (Math.Abs(dOffsetX) > RCP.M100_StageCalParamAccuracy.AsDouble ||
															 Math.Abs(dOffsetY) > RCP.M100_StageCalParamAccuracy.AsDouble)
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_SPEC_OUT;
														else
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.OK;

														WinAPIs._PostMessageM(DEF.MSG_STAGE_2D_CAL_SET_MEASURED_VALUE, m_nStage2DCal_ColIndex, m_nStage2DCal_RowIndex);
														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_finish);
												}
												else
												{
														m_bStage2DCal_Pause = true;
														WinAPIs._PostMessageM(FA.DEF.MSG_SHOW_ALARM, DEF.ALM_STAGE_2D_CAL_PAUSE);
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_INSPECTION;
														base.NextSubStep(eSeqStageMapping.Mapping_Y_DIR_First_ManualTeaching);

												}

												WinAPIs._PostMessageM(DEF.MSG_STAGE_2D_CAL_SET_MEASURED_VALUE, m_nStage2DCal_ColIndex, m_nStage2DCal_RowIndex);
												break;
										}
								case eSeqStageMapping.Mapping_Y_DIR_First_ManualTeaching:
										{
												m_stRun.bHoldOnTimeout = true;
												if (m_bStage2DCal_Continue)
												{
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetX = dStage2DCal_ManualTeachingPosX;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dMappingOffsetY = dStage2DCal_ManualTeachingPosY;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncX = AXIS.RX.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_dEncY = AXIS.Y.Status().m_stPositionStatus.fActPos;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iRow = m_nStage2DCal_RowIndex;
														m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_iCol = m_nStage2DCal_ColIndex;

														if (Math.Abs(dStage2DCal_ManualTeachingPosX) > RCP.M100_StageCalParamAccuracy.AsDouble ||
															 Math.Abs(dStage2DCal_ManualTeachingPosX) > RCP.M100_StageCalParamAccuracy.AsDouble)
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.NG_SPEC_OUT;
														else
																m_vecStageMap[m_nStage2DCal_RowIndex][m_nStage2DCal_ColIndex].m_eStatus = EzIna.GUI.UserControls.eCellStatus.OK;

														base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_finish);

												}
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_finish:
										{
												m_nStage2DCal_CurrRow++;
												m_bStage2DCal_Pause = false;
												m_bStage2DCal_Continue = false;
												base.NextSubStep(eSeqStageMapping.Mapping_Y_Dir_First_Rows_loop_prechk);
												break;
										}
								case eSeqStageMapping.Mapping_Y_Dir_First_Cols_loop_finish:
										{
												base.NextSubStep(eSeqStageMapping.Mapping_finish);
												break;
										}
								#endregion[STEP5 Y direction first]
								#region[STEP6 Make Correction table]
								case eSeqStageMapping.Mapping_finish:
										{
												MakeCorrectionTable();
												base.NextSubStep(eSeqStageMapping.Finish);
												break;
										}
										#endregion[STEP6 Make Correction table]
						}

						SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_Stage_Mapping_XY + m_stRun.nSubStep);
						return false;

				}

				private void MakeCorrectionTable()
				{
						string fp = string.Format("{0}{1}.cal", FA.DIR.INIT_PROC, System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
						using (System.IO.FileStream fs = new System.IO.FileStream(fp, System.IO.FileMode.Create, System.IO.FileAccess.Write))
						{
								using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Unicode))
								{
										//For example
										// 					' Comment.
										// 					:START2D RowAxis ColumnAxis OutputAxis1 OutputAxis2 SampDistRow SampDistCol NumCols
										// 					:START2D [OUTAXIS3=outaxis3] [POSUNIT=posunit] [CORUNIT=corunit] [OFFSETROW=offsetrow] [OFFSETCOL=offsetcol]
										// 					:START2D [ABSOLUTEFEEDBACKOFFSETROW=absolutefeedbackoffsetrow] [ABSOLUTEFEEDBACKOFFSETCOL=absolutefeedbackoffsetcol]
										// 					:START2D [NEGCOR] [INTABLE] [SERIALNUMBER="serialnumber"] 
										// 					' Each CorrectionValue contains two or three columns of Output Axis data.
										// 					CorrectionValue    CorrectionValue    CorrectionValue
										// 					CorrectionValue    CorrectionValue    CorrectionValue
										// 					:END

										sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
											":Start2D",//0
											((EzIna.Motion.CMotionA3200)AXIS.Y).iAxisNo + 1,  //RowAxis 1
											((EzIna.Motion.CMotionA3200)AXIS.RX).iAxisNo + 1, //ColumnAxis 2
											((EzIna.Motion.CMotionA3200)AXIS.RX).iAxisNo + 1, //OutputAxis1 3
											((EzIna.Motion.CMotionA3200)AXIS.Y).iAxisNo + 1,  //OutputAxis2 4
											RCP.M100_StageCalParamPitchY.AsDouble.ToString("F3"),//SampDistRow 5
											RCP.M100_StageCalParamPitchX.AsDouble.ToString("F3"),//SampDistCol 6
											RCP.M100_StageCalParamMaxDistX.AsInt / RCP.M100_StageCalParamPitchX.AsInt //NumCols 7
										));
										sw.WriteLine(string.Format("{0}={1} {2} {3}={4} {5}={6}",
											":Start2D OUTAXIS3",//0
											((EzIna.Motion.CMotionA3200)AXIS.RZ).iAxisNo + 1,//[OUTAXIS3=outaxis3]  1
											"POSUNIT=PRIMARY CORUNIT=PRIMARY",//2
											"OFFSETROW",//3
											RCP.M100_StageCalParamStartPosY.AsDouble * -1,//4
											"OFFSETCOL",//5
											RCP.M100_StageCalParamStartPosX.AsDouble * -1         //6
										));

										for (int i = 0; i < m_nStage2DCal_Rows; i++)
										{
												for (int j = 0; j < m_nStage2DCal_Columns; j++)
												{
														sw.Write(string.Format("{0}\t{1}\t{2}\t",
															m_vecStageMap[i][j].m_dMappingOffsetX.ToString("F3"),
															m_vecStageMap[i][j].m_dMappingOffsetY.ToString("F3"),
															m_vecStageMap[i][j].m_dMappingOffsetZ.ToString("F3")
														));
												}
												sw.Write("\r\n");
										}
										sw.WriteLine(":END");
								};
						};

				}
				#endregion[STAGE MAPPING]
		}
}
