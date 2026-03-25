using EzIna.Commucation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EzIna.Attenuator.OPTOGAMA
{
    public sealed partial class LPA : Commucation.CommunicationBase, AttuenuatorInterface
    {

         /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public LPA(string a_strName,
                   Commucation.SerialCom.stSerialSetting a_Setting,
                   int a_iPacketDoneTimeout,
                   double a_MinPower,
                   double a_MaxPower
                  ):base(a_Setting,a_iPacketDoneTimeout)
        {
            m_strConfigSection=a_strName;
            m_fMinPower=a_MinPower;
            m_fMaxPower=a_MaxPower;
            Connection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public LPA(string a_strName,
                   Commucation.SocketCom.stSocketSetting a_Setting,
                   int a_iPacketDoneTimeout,
                   double a_MinPower,
                   double a_MaxPower
                   ) :base(a_Setting,a_iPacketDoneTimeout)
        {
             m_strConfigSection=a_strName;
             m_fMinPower=a_MinPower;
             m_fMaxPower=a_MaxPower;
            Connection();
        }
        protected override void Dispose(bool a_Disposeing)
        {
            if (this.IsDisposed)
                return;


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
        public void DisposeDevice()
        {
            base.Dispose();
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
        }

        protected override void LoopExecute()
        {
            while (m_bLoopEnable)
            {
                Thread.Sleep(1);
                base.Execute();
            }
        }
        private void AddLoopPacket()
        {
            AddPacket(LPA_CMD.POWER_PERCENT,"",false,enumPacketType.GetValue,true);
            AddPacket(LPA_CMD.STATAUS,"",false,enumPacketType.GetValue,true);
            AddPacket(LPA_CMD.ANGLE,"",false,enumPacketType.GetValue,true);
            AddPacket(LPA_CMD.POSITION,"",false,enumPacketType.GetValue,true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override void ConnectSuceessAfterWork()
        {
            AddPacket(LPA_CMD.SERIAL_NUMBER,"",false,enumPacketType.GetValue,false);
            AddPacket(LPA_CMD.FIRMWARE_VERSION,"",false,enumPacketType.GetValue,false);
            AddPacket(LPA_CMD.HOME_SEARCH_OPTION,"",false,enumPacketType.GetValue,false);
            AddPacket(LPA_CMD.WAVE_LENGTH,"",false,enumPacketType.GetValue,false);
            AddPacket(LPA_CMD.AUTO_HOME_START,"",false,enumPacketType.GetValue,false);
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
        protected override bool ParsingPacketData(byte[] RecvData)
        {
            if (base.CurrentPacket == null)
                return true;
            try
            {
                string[] PacketstrList;
                LPA_CMD CurrentCmd;
                PacketAttribute CMDAttr;
                CurrentCmd = (LPA_CMD)base.CurrentPacket.iCMD;
                CMDAttr=CurrentCmd.GetPacketAttrFrom();
                m_RecvDataStringBuilder.Clear();
                m_RecvDataStringBuilder.Append(base.CurrentPacket.StrReciveMsg);
                m_RecvDataStringBuilder.Append(CurrentRecvEncodeing.GetString(RecvData));
                base.CurrentPacket.StrReciveMsg = m_RecvDataStringBuilder.ToString();
                MatchCollection PacketList = Regex.Matches(base.CurrentPacket.StrReciveMsg, @"^(.*?)(\r\n)");
                if(PacketList.Count>0)
                {
                    foreach (Match Packet in PacketList)
                    {
                        if (base.CurrentPacket.bEchoCMD)
                        {
                            PacketstrList = base.CurrentPacket.StrReciveMsg.Split(new string[] { CMDAttr.strCMD, "_" }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        else
                        {
                            PacketstrList = base.CurrentPacket.StrReciveMsg.Split(new string[] { "LPA>", "_" }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        if (PacketstrList.Length <= 0)
                        {
                            return false;
                        }
                        Trace.WriteLine(string.Format("{0} : {1}", CurrentCmd, PacketstrList[0]));
                        ParsingValue(CurrentCmd, base.CurrentPacket.PacketType, base.CurrentPacket.StrSetValue, PacketstrList);                               
                    }
                    this.m_bHandShakeSuccess = true;
                    return true;
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
        private void ParsingValue(LPA_CMD a_CMD, enumPacketType a_Type, string a_SetValue, string [] a_RecvValue)
        {
            if (a_RecvValue.Length <= 0)
                return;
            PacketAttribute CMDAttr = a_CMD.GetPacketAttrFrom();
            switch (a_CMD)
            {
                case LPA_CMD.POWER_PERCENT:
                    {
                        double.TryParse(a_RecvValue[0],out m_fPowerPercent);
                    }
                    break;
                case LPA_CMD.ANGLE:
                    {
                        double.TryParse(a_RecvValue[0],out m_fAngle);
                    }
                    break;
                case LPA_CMD.POSITION:
                    {
                        double.TryParse(a_RecvValue[0],out m_fPosition);
                    }
                    break;
                case LPA_CMD.ZERO_SET:
                    {

                    }
                    break;
                case LPA_CMD.MOTOR_STOP:
                    {

                    }
                    break;
                case LPA_CMD.HOME_SEARCH_OPTION:
                    {
                        ushort iTemp=0;
                        ushort.TryParse(a_RecvValue[0],out iTemp);
                        m_enumHomeOption=(HomeOption)iTemp;
                    }
                    break;
                case LPA_CMD.HOME_COMMAND:
                    {
                        
                    }
                    break;
                case LPA_CMD.AUTO_HOME_START:
                    {
                        m_bAutoHomeEnable=a_RecvValue[0]=="1"?true:false;
                    }
                    break;
                case LPA_CMD.AUTO_HOME_STOP:
                    {

                    }
                    break;
                case LPA_CMD.STATAUS:
                    {
                        if(a_RecvValue.Length==2)
                        {
                            ushort iStartus=0;
                            m_bMotorEnable=a_RecvValue[0]=="1"?true:false;
                            ushort.TryParse(a_RecvValue[1],out iStartus);
                            m_Status.Data=iStartus;
                        }
                    }
                    break;
                case LPA_CMD.WAVE_LENGTH:
                    {
                        int.TryParse(a_RecvValue[0],out m_iWaveLength);
                    }
                    break;
                case LPA_CMD.FIRMWARE_VERSION:
                    {
                        m_strFirmwareVersion=a_RecvValue[0];
                    }
                    break;
                case LPA_CMD.SERIAL_NUMBER:
                    {
                        m_strSerialNumber=a_RecvValue[0];
                    }
                    break;
                case LPA_CMD.DEVICE_RESET:
                    {

                    }
                    break;
                case LPA_CMD.MOTOR_ENABLE_ON:
                    {
                        
                    }
                    break;
                case LPA_CMD.MOTOR_ENABLE_OFF:
                    {

                    }
                    break;
                default:
                    break;
            }

        }

        private void AddPacket<T>(LPA_CMD a_CMD, T a_Value,bool a_ExistValue, enumPacketType a_Type,bool a_bLoop)
        {
            PacketAttribute CMD = a_CMD.GetPacketAttrFrom();
            stCommPacket pPacket;
            m_MakePacketStringBuilder.Clear();
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
                m_MakePacketStringBuilder.Append(CMD.strCMD);
                m_MakePacketStringBuilder.Append(a_Type == enumPacketType.SetValue ? CMD.strSetMark : CMD.strGettingMark);
                pPacket.StrSetValue = ValueString(a_Value);
                if(a_Type == enumPacketType.SetValue && a_ExistValue)
                m_MakePacketStringBuilder.Append(string.Format("_{0}",pPacket.StrSetValue));
                m_MakePacketStringBuilder.Append("\r\n");
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
                            Ret = string.Format("{0:0.000}", a_Value);
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
        public Type DeviceType
        {
            get
            {
                return typeof(LPA);
            }
        }
        public new bool IsConnected
        {
            get
            {
                return base.IsConnected()&this.m_bHandShakeSuccess;
            }

        }
        public bool IsFault
        {
            get
            {
                return m_Status[(bit)(Status.DRIVER_ERR)]|
                       m_Status[(bit)(Status.DRIVER_OVER_TEMP)]|
                       m_Status[(bit)(Status.DRIVER_LOAD_ERR)]|
                       m_Status[(bit)(Status.UNDER_VOLTAGE_ERR)]|
                       m_Status[(bit)(Status.UNDER_VOLTAGE_ERR)]|
                       m_Status[(bit)(Status.EXTERNAL_MEMEORY_ERR)];
            }

        }
        public double fAngle
        {
            get
            {
                return m_fAngle;
            }

            set
            {
               AddPacket(LPA_CMD.ANGLE,value,true,enumPacketType.SetValue,false);
            }
        }

        public double fCurrentPower
        {
            get
            {
                return m_fPowerPercent;
            }

            set
            {
               AddPacket(LPA_CMD.POWER_PERCENT,value,true,enumPacketType.SetValue,false);
            }
        }
        public double fMinPower
        {
            get
            {
                return m_fMinPower;
            }

            set
            {
                m_fMinPower=value;
            }
        }

        public double fMaxPower
        {
            get
            {
                return m_fMaxPower;
            }

            set
            {
                m_fMaxPower=value;
            }
        }
        public bool IsMotorEnabled
        {
            get
            {
                return m_bMotorEnable;
            }
        }
        public string strID
        {
            get { return m_strConfigSection;}
        }
        public string strModelName
        {
            get
            {
                return string.Format("{0}-{1}",m_strSerialNumber,m_strFirmwareVersion);
            }
        }

        public int WaveLength
        {
            get
            {
               return m_iWaveLength;
            }
        }

        public double fPosition
        {
            get
            {
                return m_fPosition;
            }
        }

        public bool IsMotionDone
        {
            get
            {
                return  m_Status[(bit)(Status.MOTOR_STAND_STILL)];
            }
        }

        public bool IsHomeDone
        {
            get
            {
                return  m_Status[(bit)((int)Status.HOMING_RAN)];
            }
        }

        public bool IsInPosition
        {
            get
            {
                return m_Status[(bit)(Status.TARGET_POS_READCHED)];
            }
        }

        public bool IsPositiveLimit
        {
            get
            {
                return m_Status[(bit)Status.RIGHT_LIMIT];
            }
        }

        public bool IsNagativeLimit
        {
            get
            {
                return m_Status[(bit)Status.LEFT_LIMIT];
            }
        }
        public int HomeSearchOption
        {
            get {return (int)m_enumHomeOption; }
            set {m_enumHomeOption=(HomeOption)value; }
        }
        public void DeviceReset()
        {
           AddPacket(LPA_CMD.DEVICE_RESET,"",false,enumPacketType.SetValue,false);          
        }       
        public void   Zeroset()
        {
            AddPacket(LPA_CMD.ZERO_SET,"",false,enumPacketType.SetValue,false);          
        }    
        public void MotorEnable(bool a_value)
        {
            AddPacket(a_value==true?LPA_CMD.MOTOR_ENABLE_ON:LPA_CMD.MOTOR_ENABLE_OFF,"",false,enumPacketType.SetValue,false);          
        }

        public void StopMotion()
        {
            AddPacket(LPA_CMD.MOTOR_STOP,"",false,enumPacketType.SetValue,false);  
        }

        public void StartHoming(bool a_bAuto)
        {
           if(a_bAuto)
           {
               AddPacket(LPA_CMD.AUTO_HOME_START,"",false,enumPacketType.SetValue,false);
           }
           else
           {
               AddPacket(LPA_CMD.HOME_COMMAND,"",false,enumPacketType.SetValue,false);
           }
        }
        public void   StopHoming(bool  a_bAuto)
        {
            if (a_bAuto)
            {
                 AddPacket(LPA_CMD.AUTO_HOME_STOP,"",false,enumPacketType.SetValue,false);
            }
            else
            {
                 AddPacket(LPA_CMD.MOTOR_STOP,"",false,enumPacketType.SetValue,false); 
            }

        }
       

        public void MoveAbSolute(double a_Pos)
        {
           if(m_bMotorEnable==true)
           AddPacket(LPA_CMD.POSITION,a_Pos,true,enumPacketType.SetValue,false);
        }
        public void MoveRelative(double a_Pos)
        {
            if(this.IsConnected & m_bMotorEnable)
            {
                double fTargetPos = m_fPosition + a_Pos;
                AddPacket(LPA_CMD.POSITION, fTargetPos,true, enumPacketType.SetValue, false);
            }           
        }

        public bool GetStatsus(Status a_value)
        {
            return m_Status[(bit)a_value];
        }
    }
}
