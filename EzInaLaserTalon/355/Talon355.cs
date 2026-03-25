using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

namespace EzIna.Laser.Talon
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Talon355:CommunicationBase,LaserInterface
    {
       
       /// <summary>
       /// 
       /// </summary>
       /// <param name="a_strName"></param>
       /// <param name="a_Setting"></param>
       /// <param name="a_iPacketDoneTimeout"></param>
        public Talon355(string a_strName,Commucation.SerialCom.stSerialSetting a_Setting,int a_iPacketDoneTimeout):base(a_Setting,a_iPacketDoneTimeout)
        {
           m_strConfigSection= a_strName;
           Connection();           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public Talon355(string a_strName,Commucation.SocketCom.stSocketSetting a_Setting,int a_iPacketDoneTimeout):base(a_Setting,a_iPacketDoneTimeout)
        {
            m_strConfigSection= a_strName;
            Connection(); 
        }
        /// <summary>
        /// Interface Dispose
        /// </summary>
        public void DisposeLaser()
        {
            base.Dispose();
        }

        /// <summary>
        /// <see href="https://m.blog.naver.com/seokcrew/221700422916"/>
        /// <see href="https://docs.microsoft.com/ko-kr/dotnet/standard/garbage-collection/implementing-dispose"/>
        /// </summary>
        /// <param name="a_Disposeing"></param>
        protected override void Dispose(bool a_Disposeing)
        {
            if (this.IsDisposed)
                return;


            if (m_pLoopThread.IsAlive)
            {
                m_bLoopEnable =false;
                m_pLoopThread.Join(100);
                if (m_pLoopThread.IsAlive)
                    m_pLoopThread.Abort();
            }
            if (a_Disposeing)
            {
                this.IsDisposing=true;           
                // Free any other managed objects here.                
            }
            //Free Unmanage Objects here               
            this.IsDisposing=false; 
            this.IsDisposed = true;             
            base.Dispose(a_Disposeing);
        }
        
        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize(); 
            AddLoopPacket();
            m_pLoopThread = new Thread(LoopExecute);
            m_pLoopThread.IsBackground = true;
            m_bLoopEnable = true;
            m_pLoopThread.Start();
            m_pLoopThread.Priority = ThreadPriority.Normal;
            m_DicSystemHistorystr.Add(00,"System Ready");
            m_DicSystemHistorystr.Add(11,"SYS ILK");
            m_DicSystemHistorystr.Add(12,"TEST ILK");
            m_DicSystemHistorystr.Add(13,"KEY ILK");
            m_DicSystemHistorystr.Add(31,"DIODE 1 TEMP ERROR");
            m_DicSystemHistorystr.Add(36,"TOWER TEMP ERROR");
            m_DicSystemHistorystr.Add(37,"CHASSIS TEMP ERROR");
            m_DicSystemHistorystr.Add(66,"WATCHDOG EXPIRED");
            m_DicSystemHistorystr.Add(128,"TOWER TEMPWARNING");
            m_DicSystemHistorystr.Add(134,"SHUTTER IN UNEXPECTED STATE");
            m_DicSystemHistorystr.Add(137,"THG RECOVERY");
            m_DicSystemHistorystr.Add(181,"DIODE TEMP WARNING");                                       
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void LoopExecute()
        {
            while (m_bLoopEnable)
            {
                Thread.Sleep(10);
                base.Execute();
            }
        }
        private void AddLoopPacket()
        {
            AddPacket(TALON355_CMD.SYSTEM_STSTUS_BYTE,"",enumPacketType.GetValue,true); 
            AddPacket(TALON355_CMD.SYSTEM_STATUS_HISTORY,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.SYSTEM_STATUS_ASCII,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.REMAIN_WARM_UP_TIME,"",enumPacketType.GetValue,true);  
            AddPacket(TALON355_CMD.MOVE_THG_CRYSTAL_SPOT_POS,"",enumPacketType.GetValue,true);                                 
            AddPacket(TALON355_CMD.THG_CRYSTAL_SPOT_HOURS,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.POWER_EMITTED,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.DIODE_TEMP,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.TOWER_TEMP,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.CHASSIS_TEMP,"",enumPacketType.GetValue,true);                      
            AddPacket(TALON355_CMD.DIODE1_HOURS,"",enumPacketType.GetValue,true); 
            AddPacket(TALON355_CMD.LASER_HEAD_EMITTING_HOUR,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.EPRF_VALUE, "", enumPacketType.GetValue, true);
            AddPacket(TALON355_CMD.REPETITION_RATE, "", enumPacketType.GetValue, true);
            AddPacket(TALON355_CMD.DIODE_CURRENT,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.GATE_ENABLE_SIGNAL_LEVEL,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.TRIGGER_EDGE_SIGNAL_LEVEL,"",enumPacketType.GetValue,true);
            AddPacket(TALON355_CMD.SHG_CRYSTAL_TEMP, "", enumPacketType.GetValue, true);           
            AddPacket(TALON355_CMD.THG_OVEN_TEMP, "", enumPacketType.GetValue, true);    
            AddPacket(TALON355_CMD.SHG_CRYSTAL_TEMP_LAST_SET, "", enumPacketType.GetValue, true);
            AddPacket(TALON355_CMD.THG_OVEN_TEMP_LAST_SET, "", enumPacketType.GetValue, true);
            AddPacket(TALON355_CMD.DIODE_CURRENT_HEADROOM,"",enumPacketType.GetValue,true);     
            AddPacket(TALON355_CMD.SHUTTER_ENABLE_LAST_SET, "", enumPacketType.GetValue, true);
            AddPacket(TALON355_CMD.THG_ALL_SPOT_HOURS, "", enumPacketType.GetValue, true);
            	
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override void ConnectSuceessAfterWork()
        {            
            AddPacket(TALON355_CMD.IDENT_PRODECT,"",enumPacketType.GetValue,false); 
            AddPacket(TALON355_CMD.SYSTEM_STSTUS_BYTE,"",enumPacketType.GetValue,false);            
            AddPacket(TALON355_CMD.DIODE1_CURRENT_LIMIT,"",enumPacketType.GetValue,false);                                  
            AddPacket(TALON355_CMD.SHUTTER_EXIST,"",enumPacketType.GetValue,false); 
            AddPacket(TALON355_CMD.DIODE1_CURRENT_LAST_SET,"",enumPacketType.GetValue,false);
            base.ConnectSuceessAfterWork();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DisConnectSuceessAfterWork()
        {
            this.m_bHandShakeSuccess=false;
            base.DisConnectSuceessAfterWork();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="bRecvUse"></param>
        public void SendTestMsg(string str, bool bRecvUse = true)
        {
            addTestMsg(str, bRecvUse);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Value (bool int string)</typeparam>
        /// <param name="a_CMD">CMD</param>
        /// <param name="a_Value">Value</param>
        /// <param name="a_Type">0: Get 1: Set Packet </param>
        /// <param name="a_bLoop">Loop or Execute Now</param>
        private void AddPacket<T>(TALON355_CMD a_CMD,T a_Value,enumPacketType a_Type,bool a_bLoop)
        {
          
            PacketAttribute CMD=a_CMD.GetPacketAttrFrom();
            stCommPacket pPacket;                      
            m_MakePacketStringBuilder.Clear();
             
            if (CMD != null)
            {
                
                if(a_Type==enumPacketType.SetValue)
                {
                    if(CMD.PacketMode==enumPacketMode.GettingOnly)
                        return;
                }
                else
                {
                    if (CMD.PacketMode == enumPacketMode.SetOnly)
                        return;
                }

                //Ex Set [ G:<n> ] Get [?G or G?]
                pPacket = new stCommPacket();   
                if(a_Type==enumPacketType.GetValue)    
                m_MakePacketStringBuilder.Append(CMD.strGettingMark);            
                m_MakePacketStringBuilder.Append(CMD.strCMD);
                if(a_Type==enumPacketType.SetValue)
                m_MakePacketStringBuilder.Append(CMD.strSetMark);    
                pPacket.StrSetValue=ValueString(a_Value);
                m_MakePacketStringBuilder.Append(pPacket.StrSetValue);
                m_MakePacketStringBuilder.Append("\r");
                pPacket.StrSendMsg=m_MakePacketStringBuilder.ToString();
                pPacket.bEchoCMD=a_Type==enumPacketType.SetValue==true ? CMD.IsSetEchoEnable : CMD.IsGettingEchoEnable;
                pPacket.iCMD=(int)a_CMD;                
                pPacket.bSendMode=true;
                pPacket.bRecvMode=false;
                pPacket.PacketType=a_Type;
                pPacket.bRecvUse=a_Type==enumPacketType.SetValue==true ? CMD.ExistSetFeedbackPacket : CMD.ExistGettingFeedbackPacket;
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
        private string ValueString(object a_Value)
        {
            string Ret="";
            var Var=a_Value.GetType();
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
                             
                            Ret = string.Format("{0}",(bool)a_Value==true ? 1 : 0);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecvData"></param>
        /// <returns></returns>
        protected override bool ParsingPacketData(byte[] RecvData)
        {
           // base.m_pCurrentCmd=new stCommPacket();
            if(base.CurrentPacket==null)
                return true;
            try
            {                  
                 string[] PacketstrList;
                 TALON355_CMD CurrentCmd;
                 CurrentCmd =(TALON355_CMD)base.CurrentPacket.iCMD;
                 m_RecvDataStringBuilder.Clear();
                 m_RecvDataStringBuilder.Append(base.CurrentPacket.StrReciveMsg);
                 m_RecvDataStringBuilder.Append(CurrentRecvEncodeing.GetString(RecvData));
                 
                base.CurrentPacket.StrReciveMsg = m_RecvDataStringBuilder.ToString();
                if(base.CurrentPacket.StrReciveMsg.Contains(m_strPacketEndSpliter))
                {
                    PacketstrList = base.CurrentPacket.StrReciveMsg.Split(new string[] { m_strPacketEndSpliter }, StringSplitOptions.RemoveEmptyEntries);
                    if (PacketstrList.Length <= 0)
                    {
                        return false;
                    }
                    Trace.WriteLine(string.Format("{0} : {1}", CurrentCmd, PacketstrList[0]));
                    ParsingValue(CurrentCmd, base.CurrentPacket.PacketType, base.CurrentPacket.StrSetValue, PacketstrList[0]);
                    this.m_bHandShakeSuccess = true;
                    return true;
                }                
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
               //Memory Dispose
         
            }                  
            return false;   
        }
        private void ParsingValue(TALON355_CMD a_CMD,enumPacketType a_Type,string a_SetValue,string a_RecvValue)
        {
            PacketAttribute CMDAttr=a_CMD.GetPacketAttrFrom();                        
            switch(a_CMD)
            {
                case TALON355_CMD.DIODE_CURRENT:
                    {												

                        double.TryParse(a_RecvValue,out this.m_fDiodeCurrent);																								
												if(a_Type==enumPacketType.SetValue)
												double.TryParse(a_SetValue,out this.m_fDiodeCurrentLastSet);
                    }
                    break;
                case TALON355_CMD.DIODE_EMISSION_STATE:
                    {
                        bool temp = false;
                        Boolean.TryParse(a_RecvValue, out temp);
                        this.m_SystemStatus[(bit)(enumSystemStatus.EMSSIMON_ONOFF)] = temp;
                    }
                    break;
                case TALON355_CMD.EPRF_VALUE:
                    {
                        double.TryParse(a_RecvValue,out this.m_fEPRF);
                    }
                    break;
                case TALON355_CMD.SYSTEM_STATUS_HISTORY:
                    {
                        string [] ListHistroy=a_RecvValue.Split(new char[] {';'},StringSplitOptions.RemoveEmptyEntries);
                        for(int i=0;i<ListHistroy.Length;i++)
                        {
                            int.TryParse(ListHistroy[i],out m_iStatusHistory[i]);
                        }
                    }
                    break;
                case TALON355_CMD.GATE_SWITCH:
                    {
                        bool temp = false;
                        Boolean.TryParse(a_RecvValue, out temp);                        
                        this.m_SystemStatus[(bit)(enumSystemStatus.GATE)] = temp;
                    }
                    break;
                case TALON355_CMD.ENABLE_EXT_GATE:
                    {
                        Boolean.TryParse(a_RecvValue,out this.m_bEXTGateControlEnable);
                    }
                    break;
                case TALON355_CMD.THG_CRYSTAL_SPOT_HOURS:
                    {
                        double.TryParse(a_RecvValue,out this.m_fTHGCrstalSpotHours);
                    }
                    break;
                case TALON355_CMD.MOVE_THG_CRYSTAL_SPOT_POS:
                    {
                        int.TryParse(a_RecvValue,out this.m_iTHGCrystalSpotPos);
                    }
                    break;
                case TALON355_CMD.POWER_EMITTED:
                    {
                        double.TryParse(a_RecvValue,out this.m_fLaserEmittedPower);
                    }
                    break;
                case TALON355_CMD.REPETITION_RATE:
                    {
                        int.TryParse(a_RecvValue,out this.m_iRepetitionRate);
                    }
                    break;
                case TALON355_CMD.SHG_AUTO_TUNE:
                    {
                        bool temp = false;
                        Boolean.TryParse(a_RecvValue, out temp);
                        this.m_SystemStatus[(bit)(enumSystemStatus.SHG_AUTO_TUNE)] = temp;
                    }
                    break;
                case TALON355_CMD.SHG_CRYSTAL_TEMP:
                    {
                        if(a_Type==enumPacketType.SetValue)
                        {
                            int.TryParse(a_RecvValue,out this.m_iSHGCrystalTempLastSet);
                        }
                        else
                        {
                            int.TryParse(a_RecvValue,out this.m_iSHGTempRegulationPoint);
                        }                                     
                    }
                    break;
                case TALON355_CMD.SHG_CRYSTAL_TEMP_LAST_SET:
                    {
                         int.TryParse(a_RecvValue,out this.m_iSHGCrystalTempLastSet);
                    }
                    break;
                case TALON355_CMD.SHUTTER_ENABLE:
                    {
                        uint temp = 0;
                        uint.TryParse(a_RecvValue, out temp);
                        this.m_SystemStatus[(bit)(enumSystemStatus.SHUTTER)] = temp>0;  
                             
                    }
                    break;
                case TALON355_CMD.SHUTTER_ENABLE_LAST_SET:
                    {
                        uint temp = 0;
                        uint.TryParse(a_RecvValue, out temp);
                        this.m_bShutterEnableLastSet=temp>0;
                    }
                    break;
                case TALON355_CMD.SYSTEM_STSTUS_BYTE:
                    {
                        uint Temp=0;
                       
                        uint.TryParse(a_RecvValue,out Temp);
                        m_SystemStatus.Data=Temp;
                      
                    }
                    break;
                case TALON355_CMD.DIODE_TEMP:
                    {
                        double.TryParse(a_RecvValue,out this.m_fDiodeTemp);
                    }
                    break;
             
                case TALON355_CMD.THG_AUTO_TUNE:
                    {
                        uint temp = 0;
                        uint.TryParse(a_RecvValue, out temp);
                        this.m_SystemStatus[(bit)(enumSystemStatus.THG_AUTO_TUNE)] = temp>0;
                    }
                    break;
                case TALON355_CMD.THG_OVEN_TEMP:
                    {
                        if (a_Type == enumPacketType.SetValue)
                        {
                             int.TryParse(a_RecvValue,out this.m_iTHGOvenTempLastSet);
                        }
                        else
                        {
                             int.TryParse(a_RecvValue,out this.m_iTHGOvenTemp);
                        }
                    }
                    break;
                case TALON355_CMD.THG_OVEN_TEMP_LAST_SET:
                    {
                        int.TryParse(a_RecvValue,out this.m_iTHGOvenTempLastSet);
                    }
                    break;
                case TALON355_CMD.TOWER_TEMP:
                    {
                        double.TryParse(a_RecvValue,out this.m_fTowerTemp);
                    }
                    break;
                case TALON355_CMD.REMAIN_WARM_UP_TIME:
                    {
                        uint temp = 0;
                        uint.TryParse(a_RecvValue, out temp);
                        this.m_WarmupRemainTime=TimeSpan.FromSeconds(temp);                        
                    }
                    break;
                case TALON355_CMD.GATE_ENABLE_SIGNAL_LEVEL:
                    {
                        int Temp=0;
                        int.TryParse(a_RecvValue,out Temp);
                        m_GateEnableSignalLevel=(enumGateEnableLevel)Temp;
                    }
                    break;
                case TALON355_CMD.TRIGGER_EDGE_SIGNAL_LEVEL:
                    {
                        int Temp = 0;
                        int.TryParse(a_RecvValue, out Temp);
                        m_TriggerEdgeSignalLevel = (enumTriggerEdgeLevel)Temp;
                    }
                    break;
                case TALON355_CMD.DIODE_CALB_CURRENT:
                    {
                        double.TryParse(a_RecvValue,out this.m_fDiodeCalibrationCurrent);
                    }
                    break;
               
                case TALON355_CMD.RECALB_POWER_MONITOR:
                    {
                        double.TryParse(a_RecvValue,out this.m_fReCalibratePowerMonitor);
                    }
                    break;
                case TALON355_CMD.CHASSIS_TEMP:
                    {
                        double.TryParse(a_RecvValue,out this.m_fChassisTemp);
                    }
                    break;
                case TALON355_CMD.DIODE_CURRENT_HEADROOM:
                    {
                        double.TryParse(a_RecvValue,out this.m_fDiodeCurrentHeadRoom);
                    }
                    break;
                case TALON355_CMD.DIODE1_CURRENT_LIMIT:
                    {
                        double.TryParse(a_RecvValue,out this.m_fDiodeCurrentLimit);
                    }
                    break;
               case TALON355_CMD.DIODE1_CURRENT_LAST_SET:
                    {
                        double.TryParse(a_RecvValue,out this.m_fDiodeCurrentLastSet);
                    }
                    break;
                case TALON355_CMD.DIODE1_HOURS:
                    {
                        double.TryParse(a_RecvValue,out this.m_fDiodeHours);
                    }
                    break;
                
                case TALON355_CMD.DIODE_SERIAL_NUMBER:
                    {
                        this.m_strDiodeSerialNum=a_RecvValue;
                    }
                    break;
                case TALON355_CMD.LASER_HEAD_EMITTING_HOUR:
                    {
                       double.TryParse(a_RecvValue, out m_fLaserHeadHours); ;
                    }
                    break;
                case TALON355_CMD.IDENT_PRODECT:
                    {
                        string [] stringlist=a_RecvValue.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries);
                        if(stringlist.Length>=4)
                        {
                            m_strManufacturer=stringlist[0];
                            m_strModel=stringlist[1];
                            m_strSerialNum=stringlist[2];
                            m_strVersion=stringlist[3];
                        }
                    }
                    break;
                case TALON355_CMD.SHUTTER_EXIST:
                    {
                        int Temp = 0;
                        int.TryParse(a_RecvValue, out Temp);
                        this.m_bShutterExist=Temp>0;
                    }
                    break;
                case TALON355_CMD.THG_ALL_SPOT_HOURS:
                    {
                        string[] ValueList=a_RecvValue.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
                        double fTemp=0.0;
                        for(int i=0;i<ValueList.Length;i++)
                        {
                            double.TryParse(ValueList[i],out fTemp);
                            if (m_THGSpotHoursList.Count <= i)
                            {
                                m_THGSpotHoursList.Add(fTemp);
                            }
                            else
                            {
                                m_THGSpotHoursList[i]=fTemp;
                            }
                        }
                    }
                    break;
                case TALON355_CMD.SYSTEM_STATUS_ASCII:
                    {
                        m_strSystemStatus = a_RecvValue;
                    }
                    break;

            }
        }    
        /// <summary>
        /// 
        /// </summary>
        public string strID
        {
            get { return m_strConfigSection;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string strModelName
        {
            get {return string.Format("{0}-{1}",this.m_strModel,this.m_strSerialNum);}
        }

        public string strSerialNum
        {
            get { return this.m_strSerialNum; }
        }

        public string strVersion
        {
            get { return this.m_strVersion; }
        }

        public string strSystemStatus
        {
            get { return this.m_strSystemStatus; }
        }
        public string strManufacturer
        {
            get { return this.m_strManufacturer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double RepetitionRate
        {
            get { return m_iRepetitionRate; }

            set
            {
                AddPacket(TALON355_CMD.REPETITION_RATE,(int)value,enumPacketType.SetValue,false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        
        public new bool IsConnected
        {
            get { return base.IsConnected() & this.m_bHandShakeSuccess; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCommConnected
        {
            get { return base.IsConnected();}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSystemFault
        {
            get { return m_SystemStatus[(bit)enumSystemStatus.SYSTEM_FAULT];}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsLaserReady
        {
            get
            {
                return (!IsSystemFault)&
                       (m_WarmupRemainTime.TotalSeconds==0)&
                       (m_strSystemStatus.ToUpper().Equals("SYSTEM READY"));                                               
            }
        }
        /// <summary>
        /// 
        /// </summary>                       
        public bool IsGateOpen
        {
            get { return m_SystemStatus[(bit)enumSystemStatus.GATE]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsShutterOpen
        {
            get { return m_SystemStatus[(bit)enumSystemStatus.SHUTTER]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEmissionOn
        {
            get { return m_SystemStatus[(bit)enumSystemStatus.EMSSIMON_ONOFF];}
        }

		
				/// <summary>
				/// Not Support
				/// </summary>
				public bool IsQSW_On
				{
						get
						{
								return false;
						}
				}
				/// <summary>
				/// 
				/// </summary>
				public bool QSW_OnOff
				{
						set
						{
								
						}
				}

				/// <summary>
				/// 
				/// </summary>
				/// 
				public Type DeviceType
        {
            get { return this.GetType(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool GateOpen
        {
            set
            {
                AddPacket(TALON355_CMD.GATE_SWITCH,value,enumPacketType.SetValue,false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
				/// 

			 public  GATE_MODE GateMode {

						get
						{
								return m_bEXTGateControlEnable==true ? GATE_MODE.EXT: GATE_MODE.INT;
						}
						set
						{
								switch (value)
								{
										case GATE_MODE.UNKNOWN:
												break;
										case GATE_MODE.INT:
												 AddPacket(TALON355_CMD.ENABLE_EXT_GATE, false, enumPacketType.SetValue, false);
												break;
										case GATE_MODE.EXT:
												 AddPacket(TALON355_CMD.ENABLE_EXT_GATE, true, enumPacketType.SetValue, false);
												break;
										default:
												break;
								}
						}
				}
      			
				/// <summary>
				/// if INT Mode RepetitionRate > 0  INT Triiger
				/// </summary>
				public TRIG_MODE TriggerMode
				{
						get
						{
								return m_iRepetitionRate==0?TRIG_MODE.EXT:TRIG_MODE.INT;
						}
						set
						{
								if(value==TRIG_MODE.EXT)
										AddPacket(TALON355_CMD.REPETITION_RATE,(int)0,enumPacketType.SetValue,false);							
						}
				}
				/// <summary>
				/// 
				/// </summary>
				public TimeSpan RemainWarmUptime
        {
            get{  return m_WarmupRemainTime;  }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ShutterOpen
        {
            set
            {
                AddPacket(TALON355_CMD.SHUTTER_ENABLE,value,enumPacketType.SetValue,false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ShutterOpenLastSet
        {
           get { return m_bShutterEnableLastSet;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool EmissionOnOff
        {
            set
            {
                if(value==true)
                {
                    AddPacket(TALON355_CMD.EMISSION_ON,value,enumPacketType.SetValue,false);
                }
                else
                {
                    AddPacket(TALON355_CMD.EMISSION_OFF,value,enumPacketType.SetValue,false);
                }                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double DiodeCurrent
        {
            get{ return m_fDiodeCurrent; }
        
        }
				/// <summary>
				/// 
				/// </summary>
				public double SetDiodeCurrent
				{
						get { return m_fDiodeCurrentLastSet; }
						set
						{
								if (value <= m_fDiodeCurrentLimit)
								{
										AddPacket(TALON355_CMD.DIODE_CURRENT, value, enumPacketType.SetValue, false);
								}
						}
				}
        /// <summary>
        /// 
        /// </summary>
        public double EPRF
        {
            get {  return m_fEPRF; }
            set
            {
                AddPacket(TALON355_CMD.EPRF_VALUE,value,enumPacketType.SetValue,false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double LaserEmittedPower
        {
            get { return m_fLaserEmittedPower;}
        }
        /// <summary>
        /// 
        /// </summary>
        public int THG_CrystalSpotPos
        {
            get { return m_iTHGCrystalSpotPos;}
            set
            {
                if(Talon355.MIN_THG_CRYITAL_POS<=value&&value<=Talon355.MAX_THG_CRYITAL_POS)
                {
                    AddPacket(TALON355_CMD.MOVE_THG_CRYSTAL_SPOT_POS,value,enumPacketType.SetValue,false);
                }
            }
        }
     
      
        /// <summary>
        /// Current Select THG Crystal Spot Hours 
        /// <see cref="THG_CrystalSpotPos"/> 
        /// </summary>
        public double THG_CrystalSpotHours
        {
            get {return m_fTHGCrstalSpotHours; }
        }
     
        /// <summary>
        /// 
        /// </summary>
        public int SHG_Temperature_REG_Point
        {
            get { return m_iSHGTempRegulationPoint;}
            set
            {
                if(value>=Talon355.MIN_SHG_TEMP_REG_POINT && value<=Talon355.MAX_SHG_TEMP_REG_POINT)
                {
                    AddPacket(TALON355_CMD.SHG_CRYSTAL_TEMP,value,enumPacketType.SetValue,false);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SHG_Temperature_REG_Point_LastSet
        {
            get { return m_iSHGCrystalTempLastSet; }            
        }
        /// <summary>
        /// 
        /// </summary>
        public double DiodeTemperature
        {
            get {  return m_fDiodeTemp;}
        }
        /// <summary>
        /// 
        /// </summary>
        public int THG_OvenTemprature
        {
            get {return m_iTHGOvenTemp; }
            set
            {
                if(value>=Talon355.MIN_THG_TEMP_REG_POINT && value<=Talon355.MAX_THG_TEMP_REG_POINT)
                {
                    AddPacket(TALON355_CMD.THG_OVEN_TEMP,value,enumPacketType.SetValue,false);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int THG_OvenTempratureLastSet
        {
            get { return m_iTHGOvenTempLastSet; }            
        }
        /// <summary>
        /// 
        /// </summary>
        public double TowerTemperature
        {
            get { return m_fTowerTemp;}
        }
        /// <summary>
        /// 
        /// </summary>
        public enumGateEnableLevel GateEnableSignalLevel
        {
            get { return m_GateEnableSignalLevel;}
            set
            {
                AddPacket(TALON355_CMD.GATE_ENABLE_SIGNAL_LEVEL,(int)value,enumPacketType.SetValue,false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public enumTriggerEdgeLevel TriggerEdgeSignalLevel
        {
            get { return m_TriggerEdgeSignalLevel; }
            set
            {
                AddPacket(TALON355_CMD.TRIGGER_EDGE_SIGNAL_LEVEL, (int)value, enumPacketType.SetValue, false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ChassisTemperature
        {
            get { return m_fChassisTemp;}
        }
        /// <summary>
        /// 
        /// </summary>
        public double DiodeCurrentLimit
        {
            get { return m_fDiodeCurrentLimit;}
        }
        /// <summary>
        /// 
        /// </summary>
        public double DiodeEmittedHours
        {
            get { return m_fDiodeHours;}
        }
        /// <summary>
        /// 
        /// </summary>
        public double LaserHeadEmittedHours
        {
            get { return m_fLaserHeadHours;}
        }
        public bool IsShutterExist
        {
            get { return m_bShutterExist;}
        }
        public double AllTHGSpotToalHours
        {
            get
            {
                return m_THGSpotHoursList.Sum();
            }
        }
        public int  MaxTHGSpotCount
        {
            get
            {
                return m_THGSpotHoursList.Count;
            }
        }

			
				/// <summary>
				/// 
				/// </summary>
				/// <param name="a_Index"></param>
				/// <returns></returns>
				public string GetSystemHistory(int a_Index)
        {
            if(m_iStatusHistory.Contains(a_Index)&&m_DicSystemHistorystr.ContainsKey(m_iStatusHistory[a_Index]))
            {
                return m_DicSystemHistorystr[m_iStatusHistory[a_Index]];
            }
            return "UnKnown";
        }
        public void Reset()
        {
        }

       
    }
}
