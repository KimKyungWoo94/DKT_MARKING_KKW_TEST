using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;

namespace EzIna.Commucation
{
		/// <summary>
		/// 
		/// </summary>
		public abstract class CommunicationBase : IDisposable
		{

				private bool IsDisposed = false;
				private bool IsDisposing = false;

				SerialPort m_pComPort;
				Socket m_pSockPort;
				/// <summary>
				/// 
				/// </summary>
				protected enumCommMethod m_CommMethod;
				/// <summary>
				/// 
				/// </summary>
				public enumCommMethod CommMethod
				{
						get { return m_CommMethod; }
				}
				/// <summary>
				/// 
				/// </summary>
				protected SerialCom.stSerialSetting m_SerialCommInfo;
				/// <summary>
				/// 
				/// </summary>
				public SerialCom.stSerialSetting SerialCommInfo
				{
						get { return m_SerialCommInfo; }
				}
				/// <summary>
				/// 
				/// </summary>
				protected SocketCom.stSocketSetting m_SocketInfo;
				/// <summary>
				/// 
				/// </summary>
				public SocketCom.stSocketSetting SocketInfo
				{
						get { return m_SocketInfo; }
				}
				/// <summary>
				/// 
				/// </summary>
				protected int m_iPacketDoneTimeout; //ms
																						/// <summary>
																						/// 
																						/// </summary>
				public int iPacketDoneTimeout
				{
						get { return m_iPacketDoneTimeout; }
				}
				StopWatchTimer m_TimeOutWatcher = null;
				StopWatchTimer m_ReconnectTimeWatcher = null;
				/// <summary>
				/// 
				/// </summary>
				LinkedList<stCommPacket> m_veclinkedLoopCmd = null;
				/// <summary>
				/// 
				/// </summary>
				LinkedList<stCommPacket> m_vecStorelinkedLoopCmd = null;
				/// <summary>
				///         
				/// </summary>
				LinkedList<stCommPacket> m_veclinkedExecuteCmd = null;
				/// <summary>
				/// 
				/// </summary>
				stCommPacket m_pCurrentCmd = null;

				byte[] m_pSocketSendBuffer;
				byte[] m_pSocketReceiveBuffer;


				/// <summary>
				/// 
				/// </summary>
				public int RetryConnectLimit { get; private set; }
				/// <summary>
				/// 
				/// </summary>
				public int ReceiveRetryLimit { get; private set; }
				/// <summary>
				/// 
				/// </summary>
				public int PacketFailRetryLimit { get; private set; }
				/// <summary>
				/// 
				/// </summary>
				public int m_nRetryConnectCurr = 0;
				/// <summary>
				/// 
				/// </summary>
				public int m_nPacketFailCount = 0;
				/// <summary>
				/// 
				/// </summary>
				object m_lock = null;
				/// <summary>
				/// 
				/// </summary>
				bool m_bInitialized = false;

				/// <summary>
				/// 
				/// </summary>
				Encoding m_SerialPortEncoding;
				/// <summary>
				/// Socket Send 시 Data Enconding 타입, 기본 : Encoding.UTF8
				/// </summary>
				Encoding m_SocketSendEncoding;
				/// <summary>
				/// Socket Receive 시 Data Enconding 타입, 기본 : Encoding.ASCII       
				/// </summary>
				Encoding m_SocketReceiveEncoding;
			

				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_Setting"></param>
				/// <param name="a_iPacketDoneTimeout"></param>

				public CommunicationBase(SerialCom.stSerialSetting a_Setting, int a_iPacketDoneTimeout)
				{
						Initialize();
						m_CommMethod = enumCommMethod.SERIAL;
						m_SerialCommInfo = a_Setting;
						m_iPacketDoneTimeout = a_iPacketDoneTimeout;
						m_bInitialized = true;
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_Setting"></param>
				/// <param name="a_iPacketDoneTimeout"></param>
				public CommunicationBase(SocketCom.stSocketSetting a_Setting, int a_iPacketDoneTimeout)
				{
						Initialize();
						m_CommMethod = enumCommMethod.SOCKET;
						m_SocketInfo = a_Setting;
						m_iPacketDoneTimeout = a_iPacketDoneTimeout;
						m_bInitialized = true;
				}
				/// <summary>
				/// <see href="https://m.blog.naver.com/seokcrew/221700422916"/>
				/// </summary>
				~CommunicationBase()
				{
						Dispose(false);
				}

				/// <summary>
				/// <see href="https://m.blog.naver.com/seokcrew/221700422916"/>
				/// </summary>        
				public void Dispose()
				{
						Dispose(true);
						GC.SuppressFinalize(this);
				}
				/// <summary>
				/// <see href="https://m.blog.naver.com/seokcrew/221700422916"/>
				/// <see href="https://docs.microsoft.com/ko-kr/dotnet/standard/garbage-collection/implementing-dispose"/>
				/// </summary>
				/// <param name="a_Disposeing"></param>
				protected virtual void Dispose(bool a_Disposeing)
				{
						if (this.IsDisposed)
								return;

						if (a_Disposeing)
						{
								this.IsDisposing = true;

								Disconnection();
								m_veclinkedLoopCmd.Clear();
								m_vecStorelinkedLoopCmd.Clear();
								m_veclinkedExecuteCmd.Clear();
								m_TimeOutWatcher.DeleyStop();
								m_ReconnectTimeWatcher.DeleyStop();
								if(m_pSockPort!=null)
										m_pSockPort.Dispose();
								// Free any other managed objects here.                
						}
						//Free Unmanage Objects here  
						this.IsDisposing = false;
						this.IsDisposed = true;

				}

				/// <summary>
				/// 상속 후 별도의 Init , 상속된 생성자에 별도 실행하지말것
				/// </summary>                              
				protected virtual void Initialize()
				{
						m_ReconnectTimeWatcher = new StopWatchTimer();
						m_TimeOutWatcher = new StopWatchTimer();
						m_veclinkedLoopCmd = new LinkedList<stCommPacket>();
						m_vecStorelinkedLoopCmd = new LinkedList<stCommPacket>();
						m_veclinkedExecuteCmd = new LinkedList<stCommPacket>();
						m_lock = new object();
						m_pComPort = new SerialPort();
						m_pSockPort = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
						m_pSocketReceiveBuffer = new byte[1024];
						m_pSocketSendBuffer=null;
						m_nRetryConnectCurr = 0;
						RetryConnectLimit = 5;
						ReceiveRetryLimit = 5;
						PacketFailRetryLimit = 5;
						m_SerialCommInfo.Init();
						m_SocketInfo.Init();

						m_SerialPortEncoding = Encoding.UTF8;
						m_SocketSendEncoding = Encoding.UTF8;
						m_SocketReceiveEncoding = Encoding.ASCII;
						m_ReconnectTimeWatcher.SetDelay = 5000;
						m_ReconnectTimeWatcher.TimeReStart();
				}
				/// <summary>
				/// 
				/// </summary>
				protected Encoding CurrentRecvEncodeing
				{
						get
						{
								switch (m_CommMethod)
								{
										case enumCommMethod.SOCKET:
												{
														return m_SocketReceiveEncoding;
												}
												break;
								}
								return Encoding.ASCII;
						}
				}
				/// <summary>
				/// 
				/// </summary>
				protected stCommPacket CurrentPacket
				{
						get
						{
								return m_pCurrentCmd;
						}
				}


				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_str"></param>
				/// <param name="bRecvUse"></param>
				protected void addTestMsg(string a_str, bool bRecvUse)
				{
						lock (m_lock)
						{
								stCommPacket tmp = new stCommPacket();
								tmp.StrSendMsg = a_str;
								tmp.bSendMode = true;
								tmp.bRecvUse = bRecvUse;
								m_veclinkedExecuteCmd.AddLast(tmp);
						}
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_Packet"></param>
				protected void ResistLoopPacket(stCommPacket a_Packet)
				{
						lock (m_lock)
						{
								m_vecStorelinkedLoopCmd.AddLast(a_Packet);
						}
				}
				/// <summary>
				/// 
				/// </summary>
				protected void ClearResistLoopPacket()
				{
						lock (m_lock)
						{
								m_vecStorelinkedLoopCmd.Clear();
						}
				}
				/// <summary>
				/// 실행해야되는 명령이 있으면 이 Func 이용 
				/// </summary>
				/// <param name="a_Packet">패킷 내용</param>
				protected void AddExcutePacket(stCommPacket a_Packet)
				{
						lock (m_lock)
						{
								m_veclinkedExecuteCmd.AddLast(a_Packet);
						}
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_Packet"></param>
				protected void ClearExcutePacket(stCommPacket a_Packet)
				{
						lock (m_lock)
						{
								m_veclinkedExecuteCmd.Clear();
						}
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="RecvData">Receive Data , Encoding 필요 </param>
				/// <returns>데이터 Parsing 성공여부 , 데이터가 잘려오거나 Retry 가 필요한경우 false</returns>                     
				/// 
				protected abstract bool ParsingPacketData(byte[] RecvData);
				/// <summary>
				/// 
				/// </summary>
				/// <returns></returns>
				protected virtual void ConnectSuceessAfterWork()
				{
						m_ReconnectTimeWatcher.DeleyStop();
						m_nRetryConnectCurr = 0;
						m_nPacketFailCount = 0;
				}
				/// <summary>
				/// 
				/// </summary>
				protected virtual void DisConnectSuceessAfterWork()
				{

				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_nCnt"></param>
				protected void SetReciveRetryLimitCnt(int a_nCnt)
				{
						RetryConnectLimit = a_nCnt;
				}
				/// <summary>
				/// 
				/// </summary>
				public void ResetRetryConnectCount()
				{
						m_nRetryConnectCurr = 0;
				}
				/// <summary>
				/// 
				/// </summary>
				protected abstract void LoopExecute();

				/*
        {
            while(m_bLoopEnable)
            {
                Thread.Sleep(1);
                Execute();
            }
        }*/
				/// <summary>
				/// 
				/// </summary>
				protected void Execute()
				{
						try
						{
								lock (m_lock)
								{
										if (IsConnected() == true)
										{
												if (m_veclinkedLoopCmd == null)
														return;

												if (m_veclinkedExecuteCmd == null)
														return;
												SendLoop();
												AddLoopPacket();
										}
										else
										{
												if (m_bInitialized == true && IsDisposing == false && IsDisposed == false)
												{
														if (m_ReconnectTimeWatcher.IsDone)
														{
																Connection();
																//Trace.WriteLine(string.Format("Reconnect : {0}",this.GetType()));
														}
												}
										}
								}
						}
						catch (Exception ex)
						{

						}
				}
				/// <summary>
				/// 
				/// </summary>
				private void AddLoopPacket()
				{
						lock (m_lock)
						{
								if (IsConnected())
								{
										if (m_veclinkedLoopCmd.Count <= 0)
										{
												if (m_vecStorelinkedLoopCmd.Count > 0)
												{
														foreach (var item in m_vecStorelinkedLoopCmd)
														{
																m_veclinkedLoopCmd.AddLast(item.Clone());
														}
												}
										}
								}

						}
				}
				/// <summary>
				/// 
				/// </summary>
				private void ClearCurrentCmd()
				{
						lock (m_lock)
						{
								if (m_pCurrentCmd != null)
								{
										m_TimeOutWatcher.DeleyStop();
										m_pCurrentCmd.nReceiveReTryCnt = 0;
										m_pCurrentCmd.Clear();
										m_pCurrentCmd = null;
								}
						}
				}
				/// <summary>
				/// 
				/// </summary>
				protected void Connection()
				{
						lock (m_lock)
						{
								Disconnection();
								switch (m_CommMethod)
								{
										case enumCommMethod.SERIAL:
												{
														
														SerialCommConnection(m_SerialCommInfo);
												}
												break;
										case enumCommMethod.SOCKET:
												{
														SocketCommConnection(m_SocketInfo);
												}
												break;
								}
						}
				}
				private void SerialCommConnection(SerialCom.stSerialSetting a_SerialInfo)
				{
						m_SerialCommInfo = a_SerialInfo;
						m_pComPort.PortName = m_SerialCommInfo.strComPortName;
						m_pComPort.BaudRate = m_SerialCommInfo.nBaudRate;
						m_pComPort.DataBits = m_SerialCommInfo.nDataBits;
						m_pComPort.Parity = m_SerialCommInfo.Parity;
						m_pComPort.StopBits = m_SerialCommInfo.StopBits;
						m_pComPort.ReadTimeout = m_SerialCommInfo.ReadTimeout;
						m_pComPort.WriteTimeout = m_SerialCommInfo.WriteTimeout;
						m_pComPort.Encoding = m_SerialPortEncoding;
						if (m_nRetryConnectCurr < RetryConnectLimit)
						{
								if (!m_pComPort.IsOpen)
								{
										try
										{
#if SIM
#else
												m_pComPort.Open();
												m_pComPort.DataReceived += new SerialDataReceivedEventHandler(SerialComReceiveMsg);

												if (m_pComPort.IsOpen)
												{
														ConnectSuceessAfterWork();
												}
#endif
										}
										catch (Exception ex)
										{
												String Strings = "";
												Strings = String.Format("[ERR] {0}", ex.Message);
										}
								}
								else
								{
										try
										{
												m_pComPort.Close();
										}
										catch (Exception ex)
										{

										}
										m_nRetryConnectCurr++;
								}

						}
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_SocketInfo"></param>
				private void SocketCommConnection(SocketCom.stSocketSetting a_SocketInfo)
				{
						m_SocketInfo = a_SocketInfo;
						if (m_nRetryConnectCurr < RetryConnectLimit)
						{
								try
								{
										//Socket _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
										IPEndPoint _ipep = new IPEndPoint(IPAddress.Parse(m_SocketInfo.strIPName), m_SocketInfo.iPort);
										m_pSockPort.Connect(_ipep);
										if (m_pSockPort.Connected == true)
										{
												//_receiveArgs = new SocketAsyncEventArgs();
												//_receiveArgs.UserToken = this;
												//Array.Clear(m_pSocketReceiveBuffer, 0, m_pSocketReceiveBuffer.Length);
												//_receiveArgs.SetBuffer(m_pSocketReceiveBuffer, 0, m_pSocketReceiveBuffer.Length);
												//_receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketRecieve_Completed);												
												ConnectSuceessAfterWork();
										}

										/* 
										 * SocketAsyncEventArgs _args = new SocketAsyncEventArgs();                    
										 _args.RemoteEndPoint = _ipep;
										 _args.Completed += new EventHandler<SocketAsyncEventArgs>(SocketConnected_Completed);										
										 m_pSockPort.ConnectAsync(_args); 
										 */
										m_nRetryConnectCurr++;
								}
								catch (Exception ex)
								{
										String Strings = "";
										Strings = String.Format("[ERR] {0}", ex.Message);
										m_nRetryConnectCurr++;
								}
						}
				}
				/// <summary>
				/// 
				/// </summary>
				protected void Disconnection()
				{
						switch (m_CommMethod)
						{
								case enumCommMethod.SERIAL:
										{
												if (m_pComPort == null)
														return;

												if (m_pComPort.IsOpen)
														m_pComPort.Close();
												m_pComPort.Dispose();
										}
										break;
								case enumCommMethod.SOCKET:
										{
												if (m_pSockPort != null)
												{
														if (m_pSockPort.Connected)
														{
																m_pSockPort.Shutdown(SocketShutdown.Both);
																m_pSockPort.Close();																
														}																										
												}
										}
										break;
						}
						if (m_ReconnectTimeWatcher.IsRunning == false)
						{
								m_ReconnectTimeWatcher.TimeReStart();
						}
						DisConnectSuceessAfterWork();
				}

				/// <summary>
				/// 
				/// </summary>
				/// <returns></returns>
				protected bool IsConnected()
				{
						bool bRet = false;
						switch (m_CommMethod)
						{
								case enumCommMethod.SERIAL:
										{
												if (m_pComPort != null)
												{
														bRet = m_pComPort.IsOpen;
												}
										}
										break;
								case enumCommMethod.SOCKET:
										{
												if (m_pSockPort != null)
												{
														bRet = m_pSockPort.Connected;
												}
										}
										break;
						}
						return bRet;
				}
				/// <summary>
				/// 
				/// </summary>
				/// <returns></returns>
				private bool SendLoop()
				{
						try
						{
								if (m_pCurrentCmd == null)
								{
										lock (m_lock)
										{
												if (m_veclinkedExecuteCmd.Count > 0)
												{
														m_pCurrentCmd = m_veclinkedExecuteCmd.First();
														m_veclinkedExecuteCmd.RemoveFirst();
												}
												else
												{
														if (m_veclinkedLoopCmd.Count > 0)
														{
																m_pCurrentCmd = m_veclinkedLoopCmd.First();
																m_veclinkedLoopCmd.RemoveFirst();
														}
												}
										}

								}
								else
								{
										if (m_pCurrentCmd.bRecvMode == true)
										{
												if (m_TimeOutWatcher.IsRunning)
												{
														if (m_TimeOutWatcher.IsDone)
														{
																m_pCurrentCmd.bSendMode = true;
																m_pCurrentCmd.bRecvMode = false;
																m_pCurrentCmd.nReceiveReTryCnt++;
																if (m_pCurrentCmd.nReceiveReTryCnt > ReceiveRetryLimit)
																{
																		m_pCurrentCmd.nReceiveReTryCnt = 0;
																		m_nPacketFailCount++;
																		ClearCurrentCmd();
																		// 일정 횟수 지나면 Disconnect
																		if (m_nPacketFailCount >= PacketFailRetryLimit)
																		{
																				Disconnection();
																		}
																}
														}
												}
										}
								}
								if (m_pCurrentCmd == null)
										return false;

								if (m_pCurrentCmd.bRecvUse)
								{
										if (m_pCurrentCmd.bSendMode == true && m_pCurrentCmd.bSendingWait == false)
										{
												m_TimeOutWatcher.SetDelay = m_iPacketDoneTimeout;
												SendPacket(m_pCurrentCmd.StrSendMsg);
												m_TimeOutWatcher.TimeReStart();
										}
								}
								else
								{
										if (m_pCurrentCmd.bSendMode == true)
										{
												SendPacket(m_pCurrentCmd.StrSendMsg);
												switch (m_CommMethod)
												{
														case enumCommMethod.SERIAL:
																{
																		ClearCurrentCmd();
																}
																break;
												}
										}
								}
						}
						catch (Exception ex)
						{

						}
						return true;
				}
				private bool SendPacket(string strPacket)
				{
						if (string.IsNullOrEmpty(strPacket))
								return false;
						switch (m_CommMethod)
						{
								case enumCommMethod.SERIAL:
										{
												m_pComPort.Write(strPacket);
												m_pCurrentCmd.bSendMode = false;
												m_pCurrentCmd.bRecvMode = true;
										}
										break;
								case enumCommMethod.SOCKET:
										{

												
												if (m_pCurrentCmd.bRecvUse == true)
												{
														if (m_pCurrentCmd.bRecvUse == true)
														{
																SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
																_receiveArgs.UserToken = this;
																_receiveArgs.SetBuffer(m_pSocketReceiveBuffer, 0, m_pSocketReceiveBuffer.Length);
																_receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketRecieve_Completed);
																m_pSockPort.ReceiveAsync(_receiveArgs);
														}
												}
												m_pSocketSendBuffer=m_SocketSendEncoding.GetBytes(m_pCurrentCmd.StrSendMsg);
												if(m_pSockPort.Send(m_pSocketSendBuffer, SocketFlags.None)
														== m_pCurrentCmd.StrSendMsg.Length
														)
												{
														m_pCurrentCmd.bRecvMode = true;
														m_pCurrentCmd.bSendMode = false;
														m_pCurrentCmd.bSendingWait = false;
													
												}
																																	
												/* m_pSocketSendBuffer = m_SocketSendEncoding.GetBytes(strPacket);
												 SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
												 _sendArgs.SetBuffer(m_pSocketSendBuffer, 0, m_pSocketSendBuffer.Length);
												 _sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketSend_Completed);
												 _sendArgs.UserToken = m_pCurrentCmd;
												 
												 if (m_pSockPort != null)
												 {
														if (m_pSockPort.SendAsync(_sendArgs))
														{
															 m_pCurrentCmd.bSendingWait = true;																
														}																
														
												 }*/
										}
										break;
						}
						return true;
				}
				#region     SerialReceive Event  ( EAP  )
				/// <summary>
				/// 
				/// </summary>
				/// <param name="sender">SerailPort </param>
				/// <param name="e">SerialDataReceivedEventArgs </param>
				private void SerialComReceiveMsg(object sender, SerialDataReceivedEventArgs e)
				{
						try
						{
								SerialPort Port = sender as SerialPort;
								if (Port.IsOpen)
								{

										int nLength = Port.BytesToRead;
										byte[] array = new byte[nLength];
										Port.Read(array, 0, array.Length);
										if (ParsingPacketData(array))
										{
												m_nPacketFailCount = 0;
												ClearCurrentCmd();
										}
								}

						}
						catch (Exception ex)
						{

						}
				}
				#endregion  SerialReceive Event  ( EAP  )

				#region     Socket Event ( EAP )
				/// <summary>
				/// 
				/// </summary>
				/// <param name="sender"> Socket ( m_pSoeket ) </param>
				/// <param name="e"> SocketAsyncEventArgs </param>
				private void SocketConnected_Completed(object sender, SocketAsyncEventArgs e)
				{
						Socket Port = sender as Socket;
						if (Port.Connected)
						{
								//m_pSockPort=Port;
								// For Async Receive Test        
								// SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
								// _receiveArgs.UserToken = this;
								// _receiveArgs.SetBuffer(m_pSocketReceiveBuffer, 0, m_pSocketReceiveBuffer.Length);
								// _receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketRecieve_Completed);                
								// m_pSockPort.ReceiveAsync(_receiveArgs);                   


						}
						else
						{
								// m_pSockPort = null;  
								// if(IsDisposing==false&&IsDisposed==false)
								//   Connection();
								// Retry Connect            
						}
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="sender">Socket ( m_pSoeket )</param>
				/// <param name="e">SocketAsyncEventArgs</param>
				private void SocketSend_Completed(object sender, SocketAsyncEventArgs e)
				{
						Socket Port = sender as Socket;
						if (Port.Connected)
						{
								stCommPacket packet = e.UserToken as stCommPacket;
								if(e.BytesTransferred>0)
								{
										Trace.WriteLine(string.Format("Sended {0}",Encoding.ASCII.GetString(e.Buffer, 0, e.BytesTransferred)));
										if (packet != null)
										{
												if (packet.bRecvUse == true)
												{
														SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
														_receiveArgs.UserToken = this;
														_receiveArgs.SetBuffer(m_pSocketReceiveBuffer, 0, m_pSocketReceiveBuffer.Length);
														_receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketRecieve_Completed);
														m_pSockPort.ReceiveAsync(_receiveArgs);
												}
												//Port.Send(m_SocketSendEncoding.GetBytes(packet.StrSendMsg), SocketFlags.None);
										}
										if (m_pCurrentCmd != null)
										{
												if (m_pCurrentCmd.bRecvUse == false)
												{
														ClearCurrentCmd();
												}
												else
												{
														m_pCurrentCmd.bRecvMode = true;
														m_pCurrentCmd.bSendMode = false;
														m_pCurrentCmd.bSendingWait = false;

												}
										}
								}							
								//Port.Send(_telegram.Data);
						}
						else
						{
								// m_pSockPort = null;
								// Connection();
								// Retry Connect
						}
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="sender">Socket ( m_pSoeket )</param>
				/// <param name="e">SocketAsyncEventArgs </param> 
				string msg = "";
				private void SocketRecieve_Completed(object sender, SocketAsyncEventArgs e)
				{
						Socket Port = sender as Socket;
						if (Port.Connected && e.BytesTransferred > 0)
						{
								try
								{
										byte[] data = new byte[e.BytesTransferred];
										Array.Copy(e.Buffer, data, e.BytesTransferred);
										msg="";
										msg = string.Format("{0}{1}", msg, Encoding.ASCII.GetString(data, 0, e.BytesTransferred));
										
										Trace.WriteLine(string.Format("Received {0}",msg));	
										if (ParsingPacketData(data))
										{
												m_nPacketFailCount = 0;
												ClearCurrentCmd();
											  Array.Clear(e.Buffer, 0, e.BytesTransferred);																						
										}
										else
										{
												Array.Clear(e.Buffer, 0, e.BytesTransferred);
												Port.ReceiveAsync(e);
										}
								}
								catch (Exception ex)
								{

								}
								finally
								{

								}
								// 무한 반복 리시브                                                           
						}
						else
						{

						}
				}
				private void DataReceived(IAsyncResult result)
				{
						//call receive functionality
						if (m_pSockPort.Connected)
						{
								//m_pSockPort.End
								ClearCurrentCmd();
								m_pSockPort.EndReceive(result);
								Trace.WriteLine("Receive");
						}
					
				}
				#endregion  Socket Event ( EAP )
				/// <summary>
				///
				/// </summary>
				/// <param name="strSection"></param>
				/* protected virtual void SaveConfig(string strSection)
				 {
						 IniFile Ini = new IniFile(Common.ConfigCommFileFullName);
						 Ini.Write(strSection, "CommMethod",m_CommMethod.ToString().ToUpper());
						 Ini.Write(strSection, "PortName", m_SerialCommInfo.strComPortName);
						 Ini.Write(strSection, "BaudRate", m_SerialCommInfo.nBaudRate);
						 Ini.Write(strSection, "DataBits", m_SerialCommInfo.nDataBits);
						 Ini.Write(strSection, "Parity", m_SerialCommInfo.Parity);
						 Ini.Write(strSection, "StopBits", m_SerialCommInfo.StopBits);
						 Ini.Write(strSection, "ReadTimeout", m_SerialCommInfo.ReadTimeout);
						 Ini.Write(strSection, "WriteTimeout", m_SerialCommInfo.WriteTimeout);
						 Ini.Write(strSection, "ReadTimeout", m_SerialCommInfo.ReadTimeout);
						 Ini.Write(strSection, "WriteTimeout", m_SerialCommInfo.WriteTimeout);
						 Ini.Write(strSection, "IPAddress", m_SocketInfo.strIPName);
						 Ini.Write(strSection, "PortNum", m_SocketInfo.iPort); 
						 Ini.Write(strSection, "PacketDoneTimeout", m_SocketInfo.iPacketDoneTimeout);             
						 Ini = null;
				 }
				 /// <summary>
				 ///         
				 /// </summary>
				 /// <param name="strSection"></param>
				 protected virtual void OpenConfig(string strSection)
				 {
						 IniFile Ini = new IniFile(Common.ConfigCommFileFullName);
						 string strCommMethod;
						 strCommMethod =Ini.Read(strSection, "CommMethod", "SOCKET");

						 m_CommMethod=(enumCommMethod)Enum.Parse(typeof(enumCommMethod),strCommMethod.ToUpper());
						 m_SerialCommInfo.strComPortName = Ini.Read(strSection, "PortName", "COM2");
						 m_SerialCommInfo.nBaudRate = Ini.Read(strSection, "BaudRate", 38400);
						 m_SerialCommInfo.nDataBits = Ini.Read(strSection, "DataBits", 8);
						 m_SerialCommInfo.Parity = Ini.Read(strSection, "Parity", Parity.None);
						 m_SerialCommInfo.StopBits = Ini.Read(strSection, "StopBits", StopBits.One);
						 m_SerialCommInfo.ReadTimeout = Ini.Read(strSection, "ReadTimeout", 5000);
						 m_SerialCommInfo.WriteTimeout = Ini.Read(strSection, "WriteTimeout", 5000);
						 m_SocketInfo.strIPName = Ini.Read(strSection, "IPAddress", "127.0.0.1");
						 m_SocketInfo.iPort = Ini.Read(strSection, "PortNum", 8000);
						 m_iPacketDoneTimeout= Ini.Read(strSection, "PacketDoneTimeout", 5000);
						 Ini = null;
				 }*/
				/// <summary>
				/// 
				/// </summary>
				/// <param name="strByte"></param>
				/// <returns></returns>
				protected virtual string ByteToString(byte[] strByte)
				{
						string str = Encoding.Default.GetString(strByte);
						return str;
				}
				/// <summary>
				/// 
				/// </summary>
				/// <param name="str"></param>
				/// <returns></returns>
				protected virtual byte[] StringToByte(string str)
				{
						byte[] StrByte = Encoding.Unicode.GetBytes(str);
						return StrByte;
				}
		}
}
