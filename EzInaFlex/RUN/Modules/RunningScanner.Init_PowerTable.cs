using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.FA;
namespace EzIna
{
		public partial class RunningScanner
		{
				#region [CREATE POWER TABLE]
				int m_nQFreqMeasurementCount;
				int m_iQFreqMeasurementIndex;
				int m_iPowerMeasureRetryIndex;

				int m_nAttCurrentAngle;
				int m_nAttMaxAngleRange;

				int m_nStartQFreq;
				int m_nEFreq;

				int m_nLPAAttPos;

				double m_dCurrentPower;

				// 		struct stPowerMeasValue
				// 		{
				// 			double dATT_Angle;
				// 			double dPowerValue;
				// 		}
				// 
				// 		Dictionary<string, List<stPowerMeasValue>> m_dicPowerTable = null;
				// 		List<stPowerMeasValue> m_vecPowerMeasValue = null;



				public enum eMODULE_SEQ_CREATE_POWER_TABLE
				{
						Finish = 0
					, Basic_Settings_start = 10
					, variables
					, Basic_Settings_finish

					//Move to powermeter position
					, MoveTo_StartZPos_prechk = 20
					, MoveTo_StartZPos_start
					, MoveTo_StartZPos_finish

					, MoveTo_StartXPos_prechk = 30
					, MoveTo_StartXPos_start
					, MoveTo_StartXPos_finish

					, ShutterOpen_prechk = 40
					, ShutterOpen_start
					, ShutterOpen_Finish

					//loop start
					, loop_prechk = 50
					, loop_start
					, Set_LaserQFreq_start
					, Set_LaserQFreq_finish
					, Set_LaserEFreq_start
					, Set_LaserEFreq_finish

					, MoveToAttPos_prechk = 60
					, MoveToAttPos_start
					, MoveToAttPos_finish

					, Measurement_prechk = 70
					, Measurement_start
					, Measurement_finish
					, loop_condition_chk
					, QFreq_Set_Zero_start
					, QFreq_Set_Zero_finish
					, MoveToAttHomePos_start
					, MoveToAttHomePos_finish
					, loop_finish
					, MakePowerTalbe_prechk = 80
					, MakePowerTable_start
					, MakePowerTable_finish



						//loop finish

				}
				#endregion

				#region [ POWERTABLE ]

				public bool SubSeq_CreatePowerTableWithAttenuator()
				{
						if (!base.SetRecoveryRunInfo())
								return false;

						switch (CastTo<eMODULE_SEQ_CREATE_POWER_TABLE>.From(m_stRun.nSubStep))
						{
								case eMODULE_SEQ_CREATE_POWER_TABLE.Finish:
										FA.LOG.Debug("SEQ", "Create power table finish");
										return true;

								case eMODULE_SEQ_CREATE_POWER_TABLE.Basic_Settings_start:
										if (!FA.LASER.Instance.IsLaserReady || !FA.LASER.Instance.IsEmissionOn)
										{
												base.m_stRun.TimeoutNow();
												break;
										}

										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.variables);
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.variables:

										//QFreq Setting 
										m_nQFreqMeasurementCount = ((FA.RCP.M100_Powermeter_QFrqMax.AsInt - FA.RCP.M100_Powermeter_QFrqMin.AsInt) / FA.RCP.M100_Powermeter_QFrqStep.AsInt) + 1;
										m_iQFreqMeasurementIndex = 0;

										//Attenuator Setting
										m_nAttMaxAngleRange = (FA.RCP.M100_ATTENUATOR_AangleMAX.AsInt - FA.RCP.M100_ATTENUATOR_AangleMIN.AsInt) + 1;
										m_nAttCurrentAngle = FA.RCP.M100_ATTENUATOR_AangleMIN.AsInt;
										m_nLPAAttPos = 0;
										//Powermeter Setting
										m_iPowerMeasureRetryIndex = 0;

										//측정 결과 저장.
										if (m_IniPowerTable != null)
												m_IniPowerTable = null;

										m_IniPowerTable = new IniFile(FA.FILE.InitProcPowerTable);//string.Format("{0}{1}.ini", DIR.INIT_PROC_POWERTABLE, System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") ));


										// 					//데이터 저장용 백터 생성.
										// 					foreach(var item in m_dicPowerTable)
										// 					{
										// 						item.Value.Clear();
										// 					}
										// 					m_dicPowerTable = null;
										// 					m_dicPowerTable = new Dictionary<string, List<stPowerMeasValue>>();

										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.Basic_Settings_finish);
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.Basic_Settings_finish:
										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartZPos_prechk);
										break;
								//파워 측정 위치로 Z 축이동.
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartZPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartZPos_start:
										if (!ACT.MoveABS(FA.RCP.M100_PowerMeter_MeasPosStartZ))
												break;

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartZPos_finish:
										if (!ACT.InPostion(FA.RCP.M100_PowerMeter_MeasPosStartZ))
												break;
										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartXPos_prechk);
										break;
								// 파워메터 위치로 X축이동.
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartXPos_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartXPos_start:
										if (!ACT.MoveABS(FA.RCP.M100_PowerMeter_MeasPosStartX))
												break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveTo_StartXPos_finish:
										if (!ACT.InPostion(FA.RCP.M100_PowerMeter_MeasPosStartX))
												break;
										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.ShutterOpen_prechk);
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.ShutterOpen_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.ShutterOpen_start:
										ACT.CYL_LaserShutter_Open.Run();
										ACT.CYL_PowerMeter_Open.Run();
										FA.LASER.Instance.GateOpen = true;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.ShutterOpen_Finish:
										if (!ACT.CYL_LaserShutter_Open.Check())
												break;
										if (!ACT.CYL_PowerMeter_Open.Check())
												break;
										if (!FA.LASER.Instance.IsGateOpen)
												break;
										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.loop_prechk);
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.loop_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;

								case eMODULE_SEQ_CREATE_POWER_TABLE.loop_start:
										m_nStartQFreq = FA.RCP.M100_Powermeter_QFrqMin.AsInt + (FA.RCP.M100_Powermeter_QFrqStep.AsInt * m_iQFreqMeasurementIndex);
										m_nEFreq = m_nStartQFreq;

										base.NextSubStep();
										break;

								case eMODULE_SEQ_CREATE_POWER_TABLE.Set_LaserQFreq_start:
										FA.LASER.Instance.RepetitionRate = m_nStartQFreq * 1000;
										base.NextSubStep();
										break;

								//Set Q-Frequency
								case eMODULE_SEQ_CREATE_POWER_TABLE.Set_LaserQFreq_finish:
										if (!FA.LASER.Instance.RepetitionRate.IsSame(m_nStartQFreq * 1000))
												break;
										base.NextSubStep();
										break;
								//Set E-Frequency
								case eMODULE_SEQ_CREATE_POWER_TABLE.Set_LaserEFreq_start:
										FA.LASER.Instance.EPRF = m_nEFreq * 1000;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.Set_LaserEFreq_finish:
										if (!FA.LASER.Instance.EPRF.IsSame(m_nEFreq * 1000))
												break;

										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.MoveToAttPos_prechk);
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveToAttPos_prechk:
										if (base.IsRunModeStopped()) break;

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveToAttPos_start:
										m_nLPAAttPos = m_nAttCurrentAngle;
										FA.ATT.LPA.fAngle = m_nLPAAttPos;

										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveToAttPos_finish:
										if (!FA.ATT.LPA.IsMotionDone || !FA.ATT.LPA.IsMotionDone)
												break;
										if (!FA.ATT.LPA.fAngle.IsSame(m_nLPAAttPos, 0.02))
												break;


										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.Measurement_prechk);
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.Measurement_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.Measurement_start:
										FA.POWERMETER.OPHIR.MeasureStart();
										m_stRun.stwatchForSub.SetDelay = FA.RCP.M100_POwermeter_MeasureDelay.AsInt * 1000;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.Measurement_finish:
										if (!m_stRun.stwatchForSub.IsDone)
												break;
										FA.POWERMETER.OPHIR.MeasureStop();
										m_dCurrentPower = FA.POWERMETER.OPHIR.fMeasuredPower;
										m_IniPowerTable.Write(m_nStartQFreq.ToString(), m_nAttCurrentAngle.ToString(), m_dCurrentPower.ToString("F2"));
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.loop_condition_chk:
										m_nAttCurrentAngle += FA.RCP.M100_ATTENUATOR_AngleStep.AsInt;
										if (m_nAttCurrentAngle > FA.RCP.M100_ATTENUATOR_AangleMAX.AsInt)//m_nAttMaxAngleRange)
										{
												m_iQFreqMeasurementIndex++;
												if (m_iQFreqMeasurementIndex >= m_nQFreqMeasurementCount)
												{
														base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.QFreq_Set_Zero_start);
												}
												else
												{
														m_nAttCurrentAngle = FA.RCP.M100_ATTENUATOR_AangleMIN.AsInt;
														base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.loop_start);
												}
										}
										else
										{
												base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.MoveToAttPos_prechk);
										}
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.QFreq_Set_Zero_start:
										FA.LASER.Instance.RepetitionRate = 0.0;
										ACT.CYL_LaserShutter_Close.Run();
										ACT.CYL_PowerMeter_Close.Run();
										FA.LASER.Instance.GateOpen = false;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.QFreq_Set_Zero_finish:
										if (!FA.LASER.Instance.RepetitionRate.IsSame(0.0))
												break;
										if (!ACT.CYL_LaserShutter_Close.Check())
												break;
										if (!ACT.CYL_PowerMeter_Close.Check())
												break;
										if (FA.LASER.Instance.IsGateOpen)
												break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveToAttHomePos_start:
										FA.ATT.LPA.fAngle = 0.0;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MoveToAttHomePos_finish:
										if (!FA.ATT.LPA.IsMotionDone || !FA.ATT.LPA.IsInPosition)
												break;
										if (!FA.ATT.LPA.fAngle.IsSame(0.0, 0.02))
												break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.loop_finish:
										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.MakePowerTalbe_prechk);
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MakePowerTalbe_prechk:
										if (base.IsRunModeStopped()) break;
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MakePowerTable_start:
										GetMaxPowerTable();
										base.NextSubStep();
										break;
								case eMODULE_SEQ_CREATE_POWER_TABLE.MakePowerTable_finish:
										base.NextSubStep(eMODULE_SEQ_CREATE_POWER_TABLE.Finish);
										break;
						}


						SubSeqCheckTimeout(DEF.Timeout_Run, DEF.Error_Run_Scanner_PowerTable + m_stRun.nSubStep);
						return false;
				}


				private void GetMaxPowerTable()
				{
						int nMinFreq = 0, nMaxFreq = 0, nFreqStep = 0, nIndex = 0;
						double dMinPower = 0.0, dMaxPower = 0.0;

						string strTmp = "";

						nMinFreq = FA.RCP.M100_Powermeter_QFrqMin.AsInt;
						nMaxFreq = FA.RCP.M100_Powermeter_QFrqMax.AsInt;
						nFreqStep = FA.RCP.M100_Powermeter_QFrqStep.AsInt;

						List<string> vecPowerTable_Max = new List<string>();
						List<string> vecFrequency = new List<string>();
						List<string> vecPower = new List<string>();

						for (nIndex = nMinFreq; nIndex < nMaxFreq + 1; nIndex += nFreqStep)
						{
								GetMinMaxPower_ByFrequency(nIndex, ref dMinPower, ref dMaxPower);
								vecPowerTable_Max.Add(string.Format("{0}={1:F2}", nIndex, dMaxPower));
								vecFrequency.Add(nIndex.ToString());
								vecPower.Add(dMaxPower.ToString("F2"));
						}

						string fp = string.Format("{0}{1}.txt", FA.DIR.INIT_PROC_POWERTABLE, "PowerTable_Max");
						using (System.IO.FileStream fs = new System.IO.FileStream(fp, System.IO.FileMode.Create, System.IO.FileAccess.Write))
						{
								using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Unicode))
								{
										foreach (string item in vecPowerTable_Max)
										{
												sw.WriteLine(item);
										}
								}
						}

						fp = string.Format("{0}{1}.txt", FA.DIR.INIT_PROC_POWERTABLE, "LB_Frequency1");
						using (System.IO.FileStream fs = new System.IO.FileStream(fp, System.IO.FileMode.Create, System.IO.FileAccess.Write))
						{
								using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Unicode))
								{
										foreach (string item in vecFrequency)
										{
												sw.WriteLine(item);
										}
								}
						}

						fp = string.Format("{0}{1}.txt", FA.DIR.INIT_PROC_POWERTABLE, "LB_Power1");
						using (System.IO.FileStream fs = new System.IO.FileStream(fp, System.IO.FileMode.Create, System.IO.FileAccess.Write))
						{
								using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Unicode))
								{
										foreach (string item in vecPower)
										{
												sw.WriteLine(item);
										}
								}
						}
				}

				private void GetMinMaxPower_ByFrequency(int a_Freq, ref double a_dMinPower, ref double a_dMaxPower)
				{
						int nStartQFreq = 0;
						int nAttMaxAngleRange = (FA.RCP.M100_ATTENUATOR_AangleMAX.AsInt - FA.RCP.M100_ATTENUATOR_AangleMIN.AsInt) + 1;
						int nAttCurrentAngle = 0;

						int nQFreqMeasurementCount = ((FA.RCP.M100_Powermeter_QFrqMax.AsInt - FA.RCP.M100_Powermeter_QFrqMin.AsInt) / FA.RCP.M100_Powermeter_QFrqStep.AsInt) + 1;
						int iQFreqMeasurementIndex = 0;

						IniFile IniPowerTable = new IniFile(FA.FILE.InitProcPowerTable);
						List<double> vecPower = new List<double>();
						//Q-Freq
						for (iQFreqMeasurementIndex = 0; iQFreqMeasurementIndex < nQFreqMeasurementCount; iQFreqMeasurementIndex++)
						{
								nStartQFreq = FA.RCP.M100_Powermeter_QFrqMin.AsInt + (FA.RCP.M100_Powermeter_QFrqStep.AsInt * iQFreqMeasurementIndex);
								//Attenuator
								if (nStartQFreq == a_Freq)
								{
										vecPower.Clear();
										for (nAttCurrentAngle = FA.RCP.M100_ATTENUATOR_AangleMIN.AsInt; nAttCurrentAngle < nAttMaxAngleRange; nAttCurrentAngle += FA.RCP.M100_ATTENUATOR_AngleStep.AsInt)
										{
												vecPower.Add(IniPowerTable.Read(a_Freq.ToString(), nAttCurrentAngle.ToString(), 0.0));
										}
										a_dMinPower = vecPower.Min();
										a_dMaxPower = vecPower.Max();
										break;
								}
						}
				}
				#endregion

		}
}
