
using EzIna.FA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
		public partial class RunningStage
		{
				#region [SEQ_AUTO_FOCUS]
				double m_dSeqAFTargetPosZ = 0.0;
				struct stAF
				{
						public float dFocusValue;
						public double dPosZ;

						public void Init()
						{
								Clear();
						}

						public void Clear()
						{
								dFocusValue = 0.0f;
								dPosZ = 0.0;
						}

				}
				List<stAF> m_listMeasAF = new List<stAF>();
				public enum eSeqAutoFocus
				{
						Finish = 0
					, Basic_Settings_start = 10
					, variables
					, illumination_start
					, illumination_finish
					, camera_expose_start
					, camera_expose_finish
					, Basic_Settings_finish

					//Searching 
					, MoveTo_StartPos_prechk = 20
					, MoveTo_StartPos_start
					, MoveTo_StartPos_finish

					//Scanning & Inspection
					, Focusing_prechk = 30
					, Searching_start
					, Focusing_Grab_start
					, Focusing_Grab_finish
					, Focusing_Inspection
					, Searching_finish

					// Move to focusing Position
					, MoveToFocusingPos_prechk = 40
					, MoveToFocusingPos_start
					, MoveToFocusingPos_finish
				}
				#endregion

				#region [AUTO FOCUSING]
				public bool SubSeq_AutoFocus()
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eSeqAutoFocus>.From(m_stRun.nSubStep))
						{
								case eSeqAutoFocus.Finish:
										FA.LOG.Debug("SEQ", "Auto focusing finish");
										return true;

								case eSeqAutoFocus.Basic_Settings_start:
										if (base.IsRunModeStopped()) break;
										FA.LOG.Debug("SEQ", "Auto focusing start");
										base.NextSubStep();
										break;

								case eSeqAutoFocus.variables:
										{
												if (m_listMeasAF == null)
												{
														m_listMeasAF = new List<stAF>();
												}
												m_listMeasAF.Clear();

												base.NextSubStep(eSeqAutoFocus.illumination_start);
												break;
										}
								case eSeqAutoFocus.illumination_start:
										{
												//[To Do List] 옵션에 따라서 Coarse, Fine 구분할 것.
												FA.LIGHTSOURCE.RING.SetIntensity((ushort)FA.DEF.eVision.FINE, RCP.F_LIGHT_Source_Ring_For_AF.AsInt);
												FA.LIGHTSOURCE.SPOT.SetIntensity((ushort)FA.DEF.eVision.FINE, RCP.F_LIGHT_Source_Spot_For_AF.AsInt);
												m_stRun.stwatchForSub.SetDelay = 100;
												base.NextSubStep(eSeqAutoFocus.illumination_finish);
												break;
										}
								case eSeqAutoFocus.illumination_finish:
										{
												if (!m_stRun.stwatchForSub.IsDone)
														break;
												base.NextSubStep(eSeqAutoFocus.camera_expose_start);
												break;
										}
								case eSeqAutoFocus.camera_expose_start:
										{
												FA.VISION.FINE_CAM.SetExposeTime(FA.RCP.F_CAM_ExposeTime.AsInt);
												base.NextSubStep(eSeqAutoFocus.camera_expose_finish);
												break;
										}
								case eSeqAutoFocus.camera_expose_finish:
										{
												if (!VISION.FINE_CAM.GetExpose().IsSame(FA.RCP.F_CAM_ExposeTime.AsInt))
														break;
												base.NextSubStep(eSeqAutoFocus.Basic_Settings_finish);
												break;
										}

								case eSeqAutoFocus.Basic_Settings_finish:
										base.NextSubStep(eSeqAutoFocus.MoveTo_StartPos_prechk);
										break;
								case eSeqAutoFocus.MoveTo_StartPos_prechk:
										if (base.IsRunModeStopped()) break;

										base.NextSubStep();
										break;
								case eSeqAutoFocus.MoveTo_StartPos_start:
										m_dSeqAFTargetPosZ = AXIS.RZ.Status().m_stPositionStatus.fActPos - (FA.RCP.F_AutoFocus_Range.AsDouble / 2.0);
										FA.ACT.MoveABS(FA.DEF.eAxesName.RZ, m_dSeqAFTargetPosZ, Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();
										break;
								case eSeqAutoFocus.MoveTo_StartPos_finish:
										if (!FA.AXIS.RZ.Status().IsMotionDone)
												break;
										if (!FA.AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(m_dSeqAFTargetPosZ, FA.DEF.IN_POS))
												break;

										base.NextSubStep(eSeqAutoFocus.Focusing_prechk);
										break;
								case eSeqAutoFocus.Focusing_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eSeqAutoFocus.Searching_start:
										if (base.IsRunModeStopped()) break;
										m_dSeqAFTargetPosZ += FA.RCP.F_AutoFocus_Range.AsDouble;
										FA.ACT.MoveABS(FA.DEF.eAxesName.RZ, m_dSeqAFTargetPosZ, FA.RCP.F_Autofocus_Velocity.AsDouble, FA.RCP.F_Autofocus_Accel.AsDouble, FA.RCP.F_Autofocus_Accel.AsDouble);
										base.NextSubStep();
										break;
								case eSeqAutoFocus.Focusing_Grab_start:
										VISION.FINE_CAM.Grab();
										base.NextSubStep();
										break;
								case eSeqAutoFocus.Focusing_Grab_finish:
										{
												if (!VISION.FINE_CAM.IsGrab()) break;
												stAF item = new stAF();
												item.dFocusValue = VISION.FINE_LIB.AutoFocusing((EzInaVision.GDV.eRoiItems)FA.RCP.F_AutoFocus_Roi_No.AsInt);
												item.dPosZ = AXIS.RZ.Status().m_stPositionStatus.fActPos;
												m_listMeasAF.Add(item);
												base.NextSubStep();
										}
										break;
								case eSeqAutoFocus.Focusing_Inspection:
										if (base.IsRunModeStopped()) break;

										if (!FA.AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(m_dSeqAFTargetPosZ, FA.DEF.IN_POS))
												base.NextSubStep(eSeqAutoFocus.Focusing_Grab_start);
										else
												base.NextSubStep(eSeqAutoFocus.Searching_finish);

										break;
								case eSeqAutoFocus.Searching_finish:
										if (base.IsRunModeStopped()) break;
										if (!FA.AXIS.RZ.Status().IsMotionDone)
												break;

										base.NextSubStep(eSeqAutoFocus.MoveToFocusingPos_prechk);
										break;
								case eSeqAutoFocus.MoveToFocusingPos_prechk:
										{
												if (base.IsRunModeStopped()) break;
												float fMax = -99999.0f;
												int iSearched = -1;

												for (int i = 0; i < m_listMeasAF.Count; i++)
												{
														if (m_listMeasAF[i].dFocusValue > fMax)
														{
																fMax = m_listMeasAF[i].dFocusValue;
																iSearched = i;
														}

												}
												if (iSearched == -1)
												{
														m_stRun.TimeoutNow();
														break;
												}

												m_dSeqAFTargetPosZ = m_listMeasAF[iSearched].dPosZ;
												base.NextSubStep();
										}
										break;
								case eSeqAutoFocus.MoveToFocusingPos_start:
										FA.ACT.MoveABS(FA.DEF.eAxesName.RZ, m_dSeqAFTargetPosZ, Motion.GDMotion.eSpeedType.RUN);
										base.NextSubStep();
										break;
								case eSeqAutoFocus.MoveToFocusingPos_finish:
										if (!AXIS.RZ.Status().IsMotionDone)
												break;
										if (!AXIS.RZ.Status().m_stPositionStatus.fActPos.IsSame(m_dSeqAFTargetPosZ, FA.DEF.IN_POS))
												break;

										FA.RCP.M100_AutoFocusOffset.SetDouble(FA.RCP.M100_FineVisionFocusZPos.AsDouble - m_dSeqAFTargetPosZ - FA.RCP.M100_JigHeight.AsDouble);
										base.NextSubStep(eSeqAutoFocus.Finish);
										break;
						}

						SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_AutoFocus + m_stRun.nSubStep);
						return false;

				}
				#endregion [AUTO FOCUSING]
		}
}
