using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;

namespace EzIna.PowerMeter.Ohpir
{
    public sealed partial class SPC : CommunicationBase, PowerMeterInterface
    {
        public bool IsDeviceConnected
        {
            get
            {
                return base.IsConnected()&m_bHandShakeSuccess;
            }
        }

        public double fMeasuredPower
        {
            get
            {
                return m_fMeasureValue;
            }
        }
        public double fMeasureMinPower {
            get { return m_fMeasureMinValue; }
        }
        public double fMeasureMaxPower {
            get { return m_fMeasureMaxValue;}
            }
        public double fMeasureAvgPower {
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
                return m_pMeasureThread!=null ? m_pMeasureThread.IsAlive|m_bMeasureEnable : m_bMeasureEnable;
            }
        }

        public bool IsZeroSetExecuting
        {
            get
            {
               return m_bZeroSetCMDPreset|m_bZeroSetExecute;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_NumOfCh"></param>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        public SPC(string a_strName,Commucation.SerialCom.stSerialSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {        
             m_strConfigSection= a_strName;   
             Connection();
		 }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="a_NumOfCh"></param>
       /// <param name="a_strName"></param>
       /// <param name="a_Setting"></param>
        public SPC(string a_strName,Commucation.SocketCom.stSocketSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {           
           m_strConfigSection= a_strName;
           Connection();
        }
      
        public void DisposePM()
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

            if (this.m_pLoopThread.IsAlive)
            {
                this.m_bLoopEnable=false;
                this.m_pLoopThread.Join(100);
                if (m_pLoopThread.IsAlive)
                    m_pLoopThread.Abort();
            }
            if(this.m_pMeasureThread!=null)
            {
                if(m_pMeasureThread.IsAlive)
                {
                    this.m_bMeasureEnable=false;
                    this.m_pMeasureThread.Join(100);
                    if (m_pMeasureThread.IsAlive)
                        m_pMeasureThread.Abort();
                }
            }

            if (a_Disposeing)
            {
                this.IsDisposing = true;

            }
            //Free Unmanage Objects here           
            this.IsDisposed = true;
            this.IsDisposing = false;
            base.Dispose(a_Disposeing);
        }
        protected override void Initialize()
        {
            base.Initialize();
						AddLoopPacket();
            m_eZeroSetStatus=eZeroSetStataus.NOT_STARTED;
            m_bZeroSetCMDPreset=false;
            m_bZeroSetExecute=false;
            m_bMeasureEnable=false;
            m_iMeasureInterval=10;

            m_fMeasureValue=0.0;
            m_fMeasureAvgValue=0.0;
            m_fMeasureMinValue=0.0;
            m_fMeasureMaxValue=0.0;
            m_bFirstMeasureAvgSet = false;
            m_iMeasureCount = 0;

            m_fMeasureAvgValueList =new List<double>();
            m_iMeasureAvgCountDefault=10;
            m_pLoopThread = new Thread(LoopExecute);
            m_pLoopThread.IsBackground = true;
            m_bLoopEnable = true;
            m_pLoopThread.Start();
            m_pLoopThread.Priority = ThreadPriority.Normal;
        }
        protected override void LoopExecute()
        {
            while(m_bLoopEnable)
            {
                Thread.Sleep(1);
                base.Execute();
            }
        }
        protected override void ConnectSuceessAfterWork()
        {
			AddPacket(SPC_CMD.VERSION			, "", enumPacketType.GetValue, false); 
			AddPacket(SPC_CMD.HEAD_INFO			, "", enumPacketType.GetValue, false);
			AddPacket(SPC_CMD.READ_WAVE_LENGTH	, "", enumPacketType.GetValue, false);
            AddPacket(SPC_CMD.HEAD_RANGE	    , "", enumPacketType.GetValue, false);
            AddPacket(SPC_CMD.MAINS_SETTING_HEAD, "", enumPacketType.GetValue, false);            
            base.ConnectSuceessAfterWork();
        }

		protected override void DisConnectSuceessAfterWork()
		{
			m_bHandShakeSuccess = false;
            m_bZeroSetCMDPreset = false;
            m_bZeroSetExecute   = false;
            m_bMeasureEnable    = false;
			base.DisConnectSuceessAfterWork();
		}
		protected override bool ParsingPacketData(byte[] RecvData)
        {
           // base.m_pCurrentCmd=new stCommPacket();
            if(base.CurrentPacket==null)
                return true;
            try
            {                  
                 string[] PacketstrList;
                 SPC_CMD CurrentCmd;
                 CurrentCmd =(SPC_CMD)base.CurrentPacket.iCMD;
                 m_RecvDataStringBuilder.Clear();
                 m_RecvDataStringBuilder.Append(base.CurrentPacket.StrReciveMsg);
                 m_RecvDataStringBuilder.Append(CurrentRecvEncodeing.GetString(RecvData));
                 
                base.CurrentPacket.StrReciveMsg = m_RecvDataStringBuilder.ToString();
                if(base.CurrentPacket.StrReciveMsg.Contains(m_strPacketEndSpliter) && base.CurrentPacket.StrReciveMsg.Contains("*") )
                {
					//Standard Error Messages
					if(base.CurrentPacket.StrSetValue.Contains('?'))
					{
						PacketstrList = base.CurrentPacket.StrReciveMsg.Split(new char[] {'?', '*', '\r'}, StringSplitOptions.RemoveEmptyEntries);
						if(PacketstrList.Length > 0)
							m_strErrorMSG = PacketstrList[0];
					}
					else
					{
						PacketstrList = base.CurrentPacket.StrReciveMsg.Split(new char[] {'*', '\r'}, StringSplitOptions.RemoveEmptyEntries);
						if (PacketstrList.Length <= 0)
						{
							return false;
						}
						Trace.WriteLine(string.Format("{0} : {1}", CurrentCmd, PacketstrList[0]));
						ParsingValue(CurrentCmd, base.CurrentPacket.PacketType, base.CurrentPacket.StrSetValue, PacketstrList[0]);
					}

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

		private void ParsingValue(SPC_CMD a_CMD,enumPacketType a_Type,string a_SetValue,string a_RecvValue)
        {
			try
			{
				PacketAttribute CMDAttr = a_CMD.GetPacketAttrFrom();
                int iTemp=0;
				switch (a_CMD)
				{
					case SPC_CMD.PING_TEST:
						break;
					case SPC_CMD.VERSION:
						m_strVersion = a_RecvValue;
						break;
					case SPC_CMD.RESET_DEVICE:
						break;
					case SPC_CMD.MAINS_SETTING_HEAD:
                        {
                            if(a_Type==enumPacketType.SetValue)
                            {
                                int.TryParse(a_SetValue,out iTemp);
                                m_eMainHeadSetting=(enumMainHeadSetting)iTemp;
                            }
                            else
                            {
                                int.TryParse(a_RecvValue,out iTemp);
                                m_eMainHeadSetting=(enumMainHeadSetting)iTemp;
                            }
                        }
						break;
					case SPC_CMD.ZERO_HEAD:
                        {
                            m_bZeroSetCMDPreset=false;
                        }
						break;
					case SPC_CMD.ZERO_HEAD_STATUS:
                        {
                            m_strZeroHeadStatus = a_RecvValue;
                            #region ZeroSetStatus
                            switch (a_RecvValue)
                            {
                                case "ZEROING NOT STARTED":
                                    {
                                        m_eZeroSetStatus=eZeroSetStataus.NOT_STARTED;
                                        m_bZeroSetExecute=false;
                                    }
                                    break;
                                case "ZEROING IN PROGRESS":
                                    {
                                        m_eZeroSetStatus=eZeroSetStataus.IN_PROGRESS;
                                        m_bZeroSetExecute=true;
                                    }
                                    break;                                   
                                case "ZEROING FAILED":
                                    {
                                        m_eZeroSetStatus=eZeroSetStataus.NOT_STARTED;
                                        m_bZeroSetExecute=false;
                                    }
                                    break;
                                case "ZEROING COMPLETED":
                                    {
                                        m_eZeroSetStatus=eZeroSetStataus.COMPLETED;
                                        m_bZeroSetExecute=false;                                         
                                    }
                                    break;
                            }
                            #endregion ZeroSetStatus
                        }						
						break;
					case SPC_CMD.HEAD_INFO:
                        {
                            m_strHeadInformation = a_RecvValue;
                        }						
						break;
                    case SPC_CMD.MEASURE_VALUE:
                        {
                            if (a_RecvValue != "OVER" && a_RecvValue != "SAT")
                            {
                                
                                double.TryParse(a_RecvValue, out m_fMeasureValue);
                                if(m_bMeasureEnable)
                                {
                                    if(m_fMeasureValue< m_fMeasureMinValue)
                                       m_fMeasureMinValue=m_fMeasureValue;
                                    if(m_fMeasureValue> m_fMeasureMaxValue)
                                        m_fMeasureMaxValue=m_fMeasureValue;
                                    m_iMeasureCount++;
                                    if (m_fMeasureAvgValueList.Count < m_iMeasureAvgCountDefault)
                                    {
                                        m_fMeasureAvgValueList.Add(m_fMeasureValue);
                                    }
                                    else
                                    {
                                        if(m_bFirstMeasureAvgSet==false)
                                        {
                                            m_fMeasureAvgValue=m_fMeasureAvgValueList.Average();
                                        }
                                        else
                                        {
                                            m_fMeasureAvgValue=cumulativeAverage1(m_fMeasureAvgValue,m_fMeasureAvgValue,m_iMeasureCount);
                                        }
                                    }                                   
                                }
                            }
                            else
                            {
                                m_strErrorMSG = a_RecvValue;
                                m_fMeasureValue = 0.0;
                            }
                        }
                        break;
					case SPC_CMD.HEAD_RANGE:
                        {
                            int.TryParse(a_SetValue, out iTemp);
                            m_eHeadRange=(enumPowerScale)iTemp;
                        }
						
						break;                        
					case SPC_CMD.READ_HEAD_RANGE:
                        {
                            int.TryParse(a_RecvValue, out iTemp);
                            m_eHeadRange=(enumPowerScale)iTemp;
                        }
						
						break;
					case SPC_CMD.SET_WAVE_LENGTH:
                        {
                            int.TryParse(a_SetValue, out iTemp);
                            m_eWaveLength = (enumWaveLength)iTemp;
                        }
						break;
					case SPC_CMD.READ_WAVE_LENGTH:
                        {
                            int.TryParse(a_RecvValue, out iTemp);
                            m_eWaveLength = (enumWaveLength)iTemp;
                        }						
						break;
					case SPC_CMD.READ_ALL_WAVE_LENGTHS:
						break;
					case SPC_CMD.SAVE_CURRENT_SET:
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
		/// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Value (bool int string)</typeparam>
        /// <param name="a_CMD">CMD</param>
        /// <param name="a_Value">Value</param>
        /// <param name="a_Type">0: Get 1: Set Packet </param>
        /// <param name="a_bLoop">Loop or Execute Now</param>
        private void AddPacket<T>(SPC_CMD a_CMD,T a_Value,enumPacketType a_Type,bool a_bLoop)
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
                m_MakePacketStringBuilder.Append(CMD.strCMD);
                m_MakePacketStringBuilder.Append(a_Type==enumPacketType.SetValue?CMD.strSetMark:CMD.strGettingMark);
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

		private string ValueString<T>(T a_Value)
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
        private double cumulativeAverage(double a_prevAvg, double a_newNumber, uint a_MeasureCount)
        {
            double oldWeight = (a_MeasureCount - 1) / (double)a_MeasureCount;
            double newWeight = 1.0 / (a_MeasureCount!=0 ? a_MeasureCount: 1);
            return (a_prevAvg * oldWeight) + (a_newNumber * newWeight);
        }
        private double cumulativeAverage1(double a_prevAvg, double a_newNumber, uint a_MeasureCount)
        {
            return (a_prevAvg * (a_MeasureCount - 1) + a_newNumber) / a_MeasureCount;
        }
        private void AddLoopPacket()
		{
			AddPacket(SPC_CMD.ZERO_HEAD_STATUS, "", enumPacketType.GetValue, true); 		
		}
		
		public bool SetZero()
		{
            if(IsMeasuring==false)
            {
                m_bZeroSetCMDPreset = true;
                AddPacket(SPC_CMD.ZERO_HEAD, "", enumPacketType.SetValue, false);
                return true;
            }        
            return false;    
		}
		public void SetWaveLength(eWaveLength a_value)
        {
            if(m_bZeroSetCMDPreset==false&&m_bZeroSetExecute==false)
            {
                int aValue=1;
                #region WaveLength
                switch (a_value)
                {
                    case eWaveLength.UV:
                        {
                            return;
                        }
                        break;
                    case eWaveLength.VIS:
                        {
                            aValue=1;
                        }
                        break;
                    case eWaveLength.YAG:
                        {
                            aValue=2;
                        }
                        break;
                    case eWaveLength.CO2:
                        {
                            aValue=3;
                        }
                        break;
                    default:
                        {
                            return;
                        }
                        break;
                }
                #endregion WaveLength
                AddPacket(SPC_CMD.SET_WAVE_LENGTH, aValue, enumPacketType.SetValue, false);
            }
			
        }	
        public string GetHeadInfo()
        {
            return m_strHeadInformation;
        }
        public string GetVersion()
        {
            return m_strVersion;
        }
		public eWaveLength GetWaveLength()
        {
            eWaveLength pRet=eWaveLength.UNKNOWN;
            switch (m_eWaveLength)
            {
                case enumWaveLength.VIS:
                    {
                        pRet=eWaveLength.VIS;
                    }
                    break;
                case enumWaveLength.YAG:
                    {
                        pRet=eWaveLength.YAG;
                    }
                    break;
                case enumWaveLength.CO2:
                    {
                        pRet=eWaveLength.CO2;
                    }
                    break;
                default:
                    {
                        pRet=eWaveLength.UNKNOWN;
                    }
                    break;
            }
            return pRet;
        }
		public void SetPowerOffset(double a_value)
        {
            
        }
		public void ResetDevice()
        {
             if(m_bZeroSetCMDPreset==false&&m_bZeroSetExecute==false)
            AddPacket(SPC_CMD.RESET_DEVICE, "", enumPacketType.SetValue, false);
        }
		#region [Special Functions]
		public string GetZeroHeadStatus()
		{
			return m_strZeroHeadStatus;
		}
		public void SetMainSetting(enumMainHeadSetting a_value)
		{
            if(m_bZeroSetCMDPreset==false&&m_bZeroSetExecute==false)
			AddPacket(SPC_CMD.MAINS_SETTING_HEAD, (int)a_value, enumPacketType.SetValue, false);
		}
        public enumMainHeadSetting GetMainSetting()
        {
            return m_eMainHeadSetting;
        }
        
        public void SetHeadRange(enumPowerScale a_value)
        {
            if (m_bZeroSetCMDPreset == false && m_bZeroSetExecute == false)
             AddPacket(SPC_CMD.MAINS_SETTING_HEAD, (int)a_value, enumPacketType.SetValue, false);
        }
        public enumPowerScale GetHeadRange()
        {
            return m_eHeadRange;
        }
		public void SaveCurrentSet()
		{
            if(m_bZeroSetCMDPreset==false&&m_bZeroSetExecute==false)
			AddPacket(SPC_CMD.SAVE_CURRENT_SET, "", enumPacketType.SetValue, false);
		}
        public eZeroSetStataus GetZeroSetStatus()
        {
            return m_eZeroSetStatus;
        }
        public bool MeasureStart(int a_Interval=10,uint a_InitAVGCount=100)
        {
            if(m_bZeroSetCMDPreset==false&&m_bZeroSetExecute==false)
            {
                if (m_pMeasureThread != null)
                {
                    if (m_pMeasureThread.IsAlive)
                        return false;
                }
                m_bMeasureEnable = true;
                m_iMeasureAvgCountDefault=a_InitAVGCount>4 ? a_InitAVGCount : 100;
                m_iMeasureInterval = a_Interval <= 10 ? 10 : a_Interval;
                m_fMeasureMinValue=0.0;
                m_fMeasureMaxValue=0.0;
                m_fMeasureAvgValue=0.0;
                m_bFirstMeasureAvgSet=false;
                m_iMeasureCount=0;
                m_fMeasureAvgValueList.Clear();
                m_pMeasureThread = new Thread(MeasureAutoSendCMD);
                m_pMeasureThread.Start();
                return true;
            }
            return false;
        }
        public void MeasureStop()
        {
            m_bMeasureEnable=false;
        }
        public bool IsOverMeasureDefaultCount
        {
            get
            {
                return m_bMeasureEnable==true ? m_iMeasureAvgCountDefault>0 ?m_iMeasureCount>m_iMeasureAvgCountDefault:false:false;                
            }
        }

     

        public uint GetMeasureCount()
        {
            return m_iMeasureCount;
        }
        private void MeasureAutoSendCMD()
        {
            while(m_bMeasureEnable)
            {
                Thread.Sleep(m_iMeasureInterval);
                AddPacket(SPC_CMD.MEASURE_VALUE, "", enumPacketType.GetValue, false);
            }
        }      
        #endregion
    }
}
