using EzIna.Commucation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.TempMeasure.OMEGA
{
		/// <summary>
		/// Coding By Omega6 Protocal , modbus RTU Not Support
		/// </summary>
		public sealed partial class CN8PT: EzIna.Commucation.CommunicationBase,EzIna.TempMeasure.ITempMeasure
		{
				public CN8PT(string a_strName, Commucation.SocketCom.stSocketSetting a_Setting, int a_iPacketDoneTimeout) : base(a_Setting, a_iPacketDoneTimeout)
				{
						m_strConfigSection = a_strName;
						Connection();
				}
			
				protected override void Dispose(bool a_Disposeing)
				{
						if (this.IsDisposed)
								return;

						if (m_pMeasureThread != null && m_pMeasureThread.IsAlive)
						{
								MeasureStop();								
								m_pLoopThread.Join(100);
								if (m_pMeasureThread.IsAlive)
										m_pMeasureThread.Abort();
						}

						if (m_pLoopThread.IsAlive)
						{
								m_bLoopEnable = false;
								m_pLoopThread.Join(100);
								if (m_pLoopThread.IsAlive)
										m_pLoopThread.Abort();
						}
						
						if (a_Disposeing)
						{
								this.IsDisposing = true;
								// Free any other managed objects here.                
						}
						//Free Unmanage Objects here               
						this.IsDisposing = false;
						this.IsDisposed = true;
						base.Dispose(a_Disposeing);
				}
				public void TestMessage()
				{
						stCommPacket pPacket;
						pPacket = new stCommPacket();
						pPacket.StrSendMsg="*R100\r";	
						pPacket.bSendMode=true;			
						pPacket.bSendingWait=false;
						pPacket.bRecvUse=true;		
						base.AddExcutePacket(pPacket);
				}
				protected override void Initialize()
				{
						base.Initialize();

						m_bMeasureEnable = false;
						m_iMeasureInterval = 10;

						m_fMeasureValue = 0.0;
						m_fMeasureAvgValue = 0.0;
						m_fMeasureMinValue = 0.0;
						m_fMeasureMaxValue = 0.0;
						m_bFirstMeasureAvgSet = false;
						m_iMeasureCount = 0;

						m_fMeasureAvgValueList = new List<double>();
						m_iMeasureAvgCountDefault = 100;
						m_pLoopThread = new Thread(LoopExecute);
						m_pLoopThread.IsBackground = true;
						m_bLoopEnable = true;
						m_pLoopThread.Start();
						m_pLoopThread.Priority = ThreadPriority.Normal;
				}
				protected override void LoopExecute()
				{
						while (m_bLoopEnable)
						{
								Thread.Sleep(10);
								base.Execute();
						}
				}
				protected override void ConnectSuceessAfterWork()
				{
				    AddPacket(CN8PT_CMD_OMEGA.VALUE_CONTINOUS_SEND_MODE,false,enumPacketType.SetValue,false);
						base.ConnectSuceessAfterWork();
				}

				protected override void DisConnectSuceessAfterWork()
				{
						m_bHandShakeSuccess = false;						
						m_bMeasureEnable = false;
						base.DisConnectSuceessAfterWork();
				}
				private void AddPacket<T>(CN8PT_CMD_OMEGA a_CMD, T a_Value, enumPacketType a_Type, bool a_bLoop)
				{

						PacketAttribute CMD = a_CMD.GetPacketAttrFrom();
						stCommPacket pPacket;
						m_MakePacketStringBuilder.Clear();
						m_MakeValueStringBuilder.Clear();
						if (CMD != null)
						{

								if (a_Type == enumPacketType.SetValue)
								{
										if (CMD.PacketMode == enumPacketMode.GettingOnly)
												return;
								}
								else
								{
										if (CMD.PacketMode == enumPacketMode.SetOnly)
												return;
								}

								//Ex Set [ G:<n> ] Get [?G or G?]
								
								pPacket = new stCommPacket();
								m_MakePacketStringBuilder.Append("*");																
								m_MakePacketStringBuilder.Append(a_Type == enumPacketType.SetValue ? CMD.strSetMark : CMD.strGettingMark);
								m_MakePacketStringBuilder.Append(CMD.strCMD);
								pPacket.StrCMDValue=string.Format("{0}{1}",a_Type == enumPacketType.SetValue ? CMD.strSetMark : CMD.strGettingMark,CMD.strCMD);
								if (a_Type == enumPacketType.SetValue)
								{
										switch (a_CMD)
										{
												case CN8PT_CMD_OMEGA.SENSOR_INFO:
														break;
												case CN8PT_CMD_OMEGA.CURRENT_VALUE:
														/// Read Only 
													
														break;
												case CN8PT_CMD_OMEGA.VALUE_CONTINOUS_SEND_MODE:
														m_MakeValueStringBuilder.Append(" ");
														m_MakeValueStringBuilder.Append(ValueString(a_Value));																												
														m_MakeValueStringBuilder.Append(" ");
														m_MakeValueStringBuilder.Append("0.0");
														pPacket.StrSetValue=m_MakeValueStringBuilder.ToString();
														
														break;
												default:
														break;
										}
								}						
								m_MakePacketStringBuilder.Append(pPacket.StrSetValue);									
								m_MakePacketStringBuilder.Append("\r");
								pPacket.StrSendMsg = m_MakePacketStringBuilder.ToString();
								pPacket.bEchoCMD = a_Type == enumPacketType.SetValue == true ? CMD.IsSetEchoEnable : CMD.IsGettingEchoEnable;
								pPacket.iCMD = (int)a_CMD;
								pPacket.bSendMode = true;
								pPacket.bRecvMode = false;
								pPacket.PacketType = a_Type;
								pPacket.bRecvUse = a_Type == enumPacketType.SetValue == true ? CMD.ExistSetFeedbackPacket : CMD.ExistGettingFeedbackPacket;
								if (a_bLoop)
								{
										base.ResistLoopPacket(pPacket);
								}
								else
								{
										base.AddExcutePacket(pPacket);
								}
						}
				}
				private string ValueString<T>(T a_Value)
				{
						string Ret = "";
						var Var = a_Value.GetType();
						if (Var.IsValueType == true)
						{
								switch (Type.GetTypeCode(Var))
								{
										case TypeCode.Single:
										case TypeCode.Double:
												{
														Ret = string.Format("{0:0.00}", a_Value);
												}
												break;
										case TypeCode.Boolean:
												{
														Ret = string.Format("{0}", (a_Value is bool) ? 1 : 0);
												}
												break;
										default:
												{
														Ret = a_Value.ToString();
												}
												break;
								}
						}
						return Ret;
				}
				protected override bool ParsingPacketData(byte[] RecvData)
				{
						if (base.CurrentPacket == null)
								return true;
						try
						{

								string[] PacketstrList;
								string strRecv="";
								
								string strRecvValue="";
								CN8PT_CMD_OMEGA CurrentCmd;
								CurrentCmd = (CN8PT_CMD_OMEGA)base.CurrentPacket.iCMD;
								m_RecvDataStringBuilder.Clear();
								m_RecvDataStringBuilder.Append(base.CurrentPacket.StrReciveMsg);
								m_RecvDataStringBuilder.Append(CurrentRecvEncodeing.GetString(RecvData));
								
								base.CurrentPacket.StrReciveMsg = m_RecvDataStringBuilder.ToString();
								if (base.CurrentPacket.StrReciveMsg.Contains('\r'))
								{
										if (base.CurrentPacket.StrReciveMsg == "Command Failed Decode 0")
										{
												this.m_bHandShakeSuccess = true;
												return true;
										}
										else
										{
												strRecv = base.CurrentPacket.StrReciveMsg.TrimEnd(new char[] { '\r' });
												if (strRecv.Contains(base.CurrentPacket.StrCMDValue))
												{
														strRecvValue = strRecv.TrimStart(base.CurrentPacket.StrCMDValue.ToCharArray());
														ParsingValue(CurrentCmd, base.CurrentPacket.PacketType, base.CurrentPacket.StrSetValue, strRecvValue);
														this.m_bHandShakeSuccess = true;
														//Trace.WriteLine(base.CurrentPacket.StrReciveMsg);
														return true;
												}

										}

								}
						}
						catch (Exception ex)
						{
								return false;
						}
						finally
						{
								//Memory Dispose

						}
						return false;
				}
				private void ParsingValue(CN8PT_CMD_OMEGA a_CMD, enumPacketType a_Type, string a_SetValue, string a_RecvValue)
				{
						try
						{
								int IntTemp=0;
								switch (a_CMD)
								{
										case CN8PT_CMD_OMEGA.SENSOR_INFO:
												break;
										case CN8PT_CMD_OMEGA.CURRENT_VALUE:
												{
														double.TryParse(a_RecvValue, out m_fMeasureValue);
														if (m_bMeasureEnable)
														{
																if (m_fMeasureValue < m_fMeasureMinValue)
																		m_fMeasureMinValue = m_fMeasureValue;
																if (m_fMeasureValue > m_fMeasureMaxValue)
																		m_fMeasureMaxValue = m_fMeasureValue;
																m_iMeasureCount++;
																if (m_fMeasureAvgValueList.Count < m_iMeasureAvgCountDefault)
																{
																		m_fMeasureAvgValueList.Add(m_fMeasureValue);
																}
																else
																{
																		if (m_bFirstMeasureAvgSet == false)
																		{
																				m_fMeasureAvgValue = m_fMeasureAvgValueList.Average();
																				m_bFirstMeasureAvgSet = true;
																		}
																		else
																		{
																				m_fMeasureAvgValue = cumulativeAverage1(m_fMeasureAvgValue, m_fMeasureValue, m_iMeasureCount);
																		}
																}
														}
												}
												break;
										case CN8PT_CMD_OMEGA.VALUE_CONTINOUS_SEND_MODE:
												if(a_Type==enumPacketType.SetValue)
												{
														string []strTemp=a_SetValue.Split(new char []{' ',},StringSplitOptions.RemoveEmptyEntries);
														if(strTemp.Length>0)
														{
																int.TryParse(strTemp[0], out IntTemp);
																m_bDeviceContinousPacketMode = IntTemp <= 0 ? false : true;
																double.TryParse(strTemp[1],out m_fDeviceContinousPacketPeriod);																
														}
														
												}
												else
												{

												}
												break;
										default:
												break;
								}
						}
						catch (Exception exc)
						{
								Trace.WriteLine(exc.Message);
						}
				}
				public bool MeasureStart(int a_Interval = 10, uint a_InitAVGCount = 100)
				{
					
								if (m_pMeasureThread != null)
								{
										if (m_pMeasureThread.IsAlive)
												return false;
								}
								m_bMeasureEnable = true;
								m_iMeasureAvgCountDefault = a_InitAVGCount > 4 ? a_InitAVGCount : 100;
								m_iMeasureInterval = a_Interval <= 10 ? 10 : a_Interval;
								m_fMeasureMinValue = 0.0;
								m_fMeasureMaxValue = 0.0;
								m_fMeasureAvgValue = 0.0;
								m_bFirstMeasureAvgSet = false;
								m_iMeasureCount = 0;
								m_fMeasureAvgValueList.Clear();
								m_pMeasureThread = new Thread(MeasureAutoSendCMD);
								m_pMeasureThread.Start();
								return true;					
				}
				public void MeasureStop()
				{
						m_bMeasureEnable = false;
				}
				public bool IsOverMeasureDefaultCount
				{
						get
						{
								return m_bMeasureEnable == true ? m_iMeasureAvgCountDefault > 0 ? m_iMeasureCount > m_iMeasureAvgCountDefault : true : false;
						}
				}
				
				private void MeasureAutoSendCMD()
				{
						while (m_bMeasureEnable)
						{
								Thread.Sleep(m_iMeasureInterval);
								AddPacket(CN8PT_CMD_OMEGA.CURRENT_VALUE, "", enumPacketType.GetValue, false);								
						}
				}

				private double cumulativeAverage(double a_prevAvg, double a_newNumber, uint a_MeasureCount)
				{
						double oldWeight = (a_MeasureCount - 1) / (double)a_MeasureCount;
						double newWeight = 1.0 / (a_MeasureCount != 0 ? a_MeasureCount : 1);
						return (a_prevAvg * oldWeight) + (a_newNumber * newWeight);
				}
				private double cumulativeAverage1(double a_prevAvg, double a_newNumber, uint a_MeasureCount)
				{
						return (a_prevAvg * (a_MeasureCount - 1) + a_newNumber) / a_MeasureCount;
				}

				// Not Support , No Action
				public void ResetDevice()
				{
						// No Action
				}

				public void DisposeTM()
				{
						base.Dispose();
				}

				#region Interface
				public double fMeasuredValue
				{
						get
						{
								return m_fMeasureValue;
						}
				}
				public double fMeasureMinValue
				{
						get { return m_fMeasureMinValue; }
				}
				public double fMeasureMaxValue
				{
						get { return m_fMeasureMaxValue; }
				}
				public double fMeasureAvgValue
				{
						get
						{
								return m_fMeasureAvgValue;
						}
				}
				public string strID
				{
						get
						{
								return m_strConfigSection;
						}
				}

				public Type DeviceType
				{
						get
						{
								return this.GetType();
						}
				}

				public bool IsMeasuring
				{
						get
						{
								return m_pMeasureThread != null ? m_pMeasureThread.IsAlive | m_bMeasureEnable : m_bMeasureEnable;
						}
				}

				public bool IsDeviceConnected
				{
						get
						{
								 return base.IsConnected()&m_bHandShakeSuccess;
						}
				}
				#endregion


		}
}
