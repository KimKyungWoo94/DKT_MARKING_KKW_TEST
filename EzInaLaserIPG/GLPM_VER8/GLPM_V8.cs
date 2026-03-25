using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Threading;
using System.Diagnostics;

namespace EzIna.Laser.IPG
{
    public sealed partial class GLPM_V8 : CommunicationBase, LaserInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public GLPM_V8(string a_strName, Commucation.SerialCom.stSerialSetting a_Setting, int a_iPacketDoneTimeout) : base(a_Setting, a_iPacketDoneTimeout)
        {
            m_strConfigSection = a_strName;
            Connection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>

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
            AddPacket(GLPM_V8_CMD.STATUS, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.READ_INT_TRIGGER_FREQ, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.READ_EXT_TRIGGER_FREQ, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.CASE_TEMP, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.HEAD_TEMP, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.SET_POINT_CRYSTAL_TEMP, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.ACTUAL_CRYSTAL_TEMP, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.READ_EMISSION_TIME, "", false, enumPacketType.GetValue, true);
            AddPacket(GLPM_V8_CMD.READ_CURRENT_SET_POINT, "", false, enumPacketType.GetValue, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override void ConnectSuceessAfterWork()
        {
            AddPacket(GLPM_V8_CMD.READ_FIRMWARE_VERSION, "", false, enumPacketType.GetValue, false);
            AddPacket(GLPM_V8_CMD.READ_EMIISION_CONTROL, "", false, enumPacketType.GetValue, false);
            AddPacket(GLPM_V8_CMD.READ_POWER_SOURCE, "", false, enumPacketType.GetValue, false);
            AddPacket(GLPM_V8_CMD.READ_MIN_TRIGGER_FREQ, "", false, enumPacketType.GetValue, false);
            AddPacket(GLPM_V8_CMD.READ_MAX_TRIGGER_FREQ, "", false, enumPacketType.GetValue, false);
            base.ConnectSuceessAfterWork();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DisConnectSuceessAfterWork()
        {
            this.m_bHandShakeSuccess = false;
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
        private void AddPacket<T>(GLPM_V8_CMD a_CMD, T a_Value, bool a_bExistSetValue, enumPacketType a_Type, bool a_bLoop)
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
                if (a_bExistSetValue == true)
                {
                    pPacket.StrSetValue = ValueString(a_Value);
                    m_MakePacketStringBuilder.Append(string.Format(" {0}", pPacket.StrSetValue));
                }
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
                            Ret = string.Format("{0:0.0}", a_Value);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecvData"></param>
        /// <returns></returns>
        protected override bool ParsingPacketData(byte[] RecvData)
        {
            // base.m_pCurrentCmd=new stCommPacket();
            if (base.CurrentPacket == null)
                return true;
            try
            {
                string[] PacketstrList;
                string[] PacketValue;
                string[] strValues;
                GLPM_V8_CMD CurrentCmd;
                CurrentCmd = (GLPM_V8_CMD)base.CurrentPacket.iCMD;
                PacketAttribute CmdAttr = CurrentCmd.GetPacketAttrFrom();
                m_RecvDataStringBuilder.Clear();
                m_RecvDataStringBuilder.Append(base.CurrentPacket.StrReciveMsg);
                m_RecvDataStringBuilder.Append(CurrentRecvEncodeing.GetString(RecvData));

                base.CurrentPacket.StrReciveMsg = m_RecvDataStringBuilder.ToString();
                if (base.CurrentPacket.StrReciveMsg.Contains('\r'))
                {
                    PacketstrList = base.CurrentPacket.StrReciveMsg.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    if (PacketstrList.Length <= 0)
                    {

                        return false;
                    }
                    if (base.CurrentPacket.bEchoCMD)
                    {
                        foreach (string strPacket in PacketstrList)
                        {
                            PacketValue = base.CurrentPacket.StrReciveMsg.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                            if (CurrentCmd == GLPM_V8_CMD.READ_EXT_TRIGGER_FREQ)
                            {
                                if ((CurrentCmd.GetPacketAttrstrCMD()) == PacketValue[0] ||
                                    (PacketValue[0] == "RET")

                                    )
                                {
                                    strValues = PacketValue.Where((val, idx) => idx != 0).ToArray();
                                    ParsingValue(CurrentCmd, base.CurrentPacket.PacketType, base.CurrentPacket.StrSetValue, strValues);
                                    this.m_bHandShakeSuccess = true;
                                    return true;
                                }
                            }
                            else
                            {
                                if (CurrentCmd.GetPacketAttrstrCMD() == PacketValue[0])
                                {
                                    strValues = PacketValue.Where((val, idx) => idx != 0).ToArray();
                                    ParsingValue(CurrentCmd, base.CurrentPacket.PacketType, base.CurrentPacket.StrSetValue, strValues);
                                    this.m_bHandShakeSuccess = true;
                                    return true;
                                }
                            }

                        }
                    }
                    else
                    {
                        return true;
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
        private void ParsingValue(GLPM_V8_CMD a_CMD, enumPacketType a_Type, string a_SetValue, string[] a_RecvValue)
        {
            if (a_RecvValue.Length <= 0)
                return;
            PacketAttribute CMDAttr = a_CMD.GetPacketAttrFrom();
            int iCovertTemp = 0;
            
            switch (a_CMD)
            {
                case GLPM_V8_CMD.STATUS:
                    {
                        int.TryParse(a_RecvValue[0], out iCovertTemp);
                        m_Status.Data = (uint)iCovertTemp;
                    }
                    break;
                case GLPM_V8_CMD.READ_EMISSION_TIME:
                    {
                       long.TryParse(a_RecvValue[0],out m_iEmissionTime);
                    }
                    break;
                case GLPM_V8_CMD.EMISSION_ON:
                    break;
                case GLPM_V8_CMD.EMISSION_OFF:
                    break;
                case GLPM_V8_CMD.CASE_TEMP:
                    {
                        float.TryParse(a_RecvValue[0], out m_fCaseTemp);
                    }
                    break;
                case GLPM_V8_CMD.HEAD_TEMP:
                    {
                        float.TryParse(a_RecvValue[0], out m_fHeadTemp);
                    }
                    break;
                case GLPM_V8_CMD.LOWER_DECK_TEMP:
                    {
                        float.TryParse(a_RecvValue[0], out m_fLowerDeckTemp);
                    }
                    break;
                case GLPM_V8_CMD.SET_POINT_CRYSTAL_TEMP:
                    {
                        float.TryParse(a_RecvValue[0], out m_fSetPointCrystalTemp);                        
                    }
                    break;

                case GLPM_V8_CMD.ACTUAL_CRYSTAL_TEMP:
                    {
                        float.TryParse(a_RecvValue[0], out m_fActualCrystalTemp);          
                    }
                    break;
                case GLPM_V8_CMD.SET_CURRENT_SET_POINT:
                    {
                        float.TryParse(a_RecvValue[0], out m_fSetPoint);
                    }
                    break;
                case GLPM_V8_CMD.READ_CURRENT_SET_POINT:
                    {
                        float.TryParse(a_RecvValue[0], out m_fSetPoint);
                    }
                    break;
                case GLPM_V8_CMD.SET_TRIGGER_MODE:
                    break;
                case GLPM_V8_CMD.READ_TRIGGER_MODE:
                    break;
                case GLPM_V8_CMD.SET_TRIGGER_FREQ:
                    break;
                case GLPM_V8_CMD.READ_INT_TRIGGER_FREQ:
                    {
                        double.TryParse(a_RecvValue[0], out m_fINT_Trigger_FREQ);
                    }
                    break;
                case GLPM_V8_CMD.READ_EXT_TRIGGER_FREQ:
                    {
                        double.TryParse(a_RecvValue[0], out m_fEXT_Trigger_FREQ);
                    }
                    break;
                case GLPM_V8_CMD.READ_MIN_TRIGGER_FREQ:
                    {
                        int.TryParse(a_RecvValue[0], out m_iMinTriggerFREQ);
                    }
                    break;
                case GLPM_V8_CMD.READ_MAX_TRIGGER_FREQ:
                    {
                        int.TryParse(a_RecvValue[0], out m_iMaxTriggerFREQ);
                    }
                    break;
                case GLPM_V8_CMD.SET_SOURCE_MODULATION:
                    break;
                case GLPM_V8_CMD.READ_SOURCE_MODULATION:
                    break;
                case GLPM_V8_CMD.SET_POWER_SOURCE:
                case GLPM_V8_CMD.READ_POWER_SOURCE:
                    {
                        int.TryParse(a_RecvValue[0], out iCovertTemp);
                        m_bPowerSourceCtrl = iCovertTemp > 0;
                    }
                    break;
                case GLPM_V8_CMD.SET_EMIISION_CONTROL:
                case GLPM_V8_CMD.READ_EMIISION_CONTROL:
                    {
                        int.TryParse(a_RecvValue[0], out iCovertTemp);
                        m_bEmissionCtrl = iCovertTemp > 0;
                    }
                    break;
                case GLPM_V8_CMD.RESET_ERR:
                    break;
                case GLPM_V8_CMD.READ_FIRMWARE_VERSION:
                    {
                        m_strFirmwareVersion = a_RecvValue[0];
                    }
                    break;
                default:
                    break;
            }
        }
        #region Laser Interface
        public bool IsEmissionOn
        {
            get
            {
                return m_Status[(bit)STATUS.EMISSION];
            }
        }
        public bool EmissionOnOff
        {
            set
            {
                if (m_bEmissionCtrl == true)
                {
                    if (value == true)
                    {
                        AddPacket(GLPM_V8_CMD.EMISSION_ON, "", false, enumPacketType.SetValue, false);
                    }
                    else
                    {
                        AddPacket(GLPM_V8_CMD.EMISSION_OFF, "", false, enumPacketType.SetValue, false);
                    }
                }
            }
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
        /// Not Support
        /// </summary>
        public bool QSW_OnOff
        {
            set
            {

            }
        }

        /// <summary>
        /// Gate Not Support
        /// </summary>
        public bool IsGateOpen
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gate Not Support
        /// </summary>
        public bool GateOpen
        {
            set
            {
                //AddPacket(GLPM_CMD.SET_SOURCE_MODULATION,true,true,enumPacketType.SetValue,false);
            }
        }

        public bool IsShutterOpen
        {
            get
            {
                return false;
            }
        }
        public bool ShutterOpen
        {
            set
            {

            }
        }


        public TRIG_MODE TriggerMode
        {
            get { return m_Status[(bit)STATUS.TRIGGER] == true ? TRIG_MODE.EXT : TRIG_MODE.INT; }
            set
            {

                switch (value)
                {
                    case TRIG_MODE.INT:
                        AddPacket(GLPM_V8_CMD.SET_TRIGGER_MODE, 1, true, enumPacketType.SetValue, false);
                        break;
                    case TRIG_MODE.EXT:
                        AddPacket(GLPM_V8_CMD.SET_TRIGGER_MODE, 0, true, enumPacketType.SetValue, false);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Not Support
        /// </summary>
        public GATE_MODE GateMode
        {
            get
            {
                return m_Status[(bit)STATUS.MODULATION] == false ? GATE_MODE.EXT : GATE_MODE.INT;
            }
            set
            {
                if (value == GATE_MODE.INT)
                    Modulation = true;
                else
                    Modulation = false;
            }
        }
        public bool IsLaserReady
        {
            get
            {
                return (m_Status[(bit)STATUS.IS_WARM_UP_MODE] == false) &
                             IsSystemFault == false;
            }
        }



        public bool IsSystemFault
        {
            get
            {
                return (m_Status[(bit)STATUS.VOLT_24_PS_STATUS] == false) &
                                (m_Status[(bit)STATUS.LASER_TEMP_STATUS] == false) &
                                (m_Status[(bit)STATUS.BACK_REFLECTION_LEV_STATUS] == true) &
                                (m_Status[(bit)STATUS.HEAD_TEMP_STATUS] == false);
            }
        }

        public Type DeviceType
        {
            get
            {
                return this.GetType();
            }
        }

        public TimeSpan RemainWarmUptime
        {
            get
            {
                if (m_Status[(bit)STATUS.IS_WARM_UP_MODE] == true)
                {
                    m_WarmUpRemain = TimeSpan.FromMinutes(10);
                }
                else
                {
                    m_WarmUpRemain = TimeSpan.FromSeconds(0);
                }
                return m_WarmUpRemain;
            }
        }
        public bool IsWarmupMode
        {
            get { return m_Status[(bit)STATUS.IS_WARM_UP_MODE]; }
        }
        public double RepetitionRate
        {
            get
            {
                return m_Status[(bit)STATUS.TRIGGER] == true ? m_fEXT_Trigger_FREQ : m_fINT_Trigger_FREQ;
            }

            set
            {
                if (value >= m_iMinTriggerFREQ && m_iMaxTriggerFREQ >= value)
                    AddPacket(GLPM_V8_CMD.SET_TRIGGER_FREQ, (int)value, true, enumPacketType.SetValue, false);
            }
        }

        /// <summary>
        /// Not Used
        /// </summary>
        public double EPRF
        {
            get
            {
                return 0.0;
            }
            set
            {

            }
        }

        /// <summary>
        /// Not Used
        /// </summary>
        public double DiodeCurrent
        {
            get { return 0.0; }

        }
        /// <summary>
        /// 
        /// </summary>
        public double SetDiodeCurrent
        {
            get
            {
                return m_fSetPoint;
            }
            set
            {
                if (m_fSetPoint != value && value >= 0 && value <= 100)
                {
                    AddPacket(GLPM_V8_CMD.SET_CURRENT_SET_POINT, value, true, enumPacketType.SetValue, false);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strID
        {
            get { return m_strConfigSection; }
        }
        public string strModelName
        {
            get
            {
                return m_strFirmwareVersion;
            }
        }
        public bool IsCommConnected
        {
            get
            {
                return base.IsConnected();
            }
        }

        public new bool IsConnected
        {
            get
            {
                return base.IsConnected() & this.m_bHandShakeSuccess;
            }
        }
        #endregion Laser Interface

        public float CaseTemp
        {
            get { return m_fCaseTemp; }
        }
        public float HeadTemp
        {
            get { return m_fHeadTemp; }
        }
        public float fActualCrystalTemp
        {
            get { return m_fActualCrystalTemp;}
        }
        public float fSetPointCrystalTemp
        {
            get { return m_fSetPointCrystalTemp;}
        }
        /// <summary>
        /// Minute
        /// </summary>
        public long iEmssionTime
        {
            get {  return m_iEmissionTime;}
        }
        

        public float SetPoint
        {
            get { return m_fSetPoint; }
            set
            {
                if (m_fSetPoint != value && value >= 0 && value <= 100)
                {
                    AddPacket(GLPM_V8_CMD.SET_CURRENT_SET_POINT, value, true, enumPacketType.SetValue, false);
                }
            }
        }


        public Commucation.BitField32Helper Status
        {
            get { return m_Status; }
        }
        public double INT_TriggerFREQ
        {
            get { return m_fINT_Trigger_FREQ; }
            set
            {
                if (m_fINT_Trigger_FREQ != value)
                    AddPacket(GLPM_V8_CMD.SET_TRIGGER_FREQ, (int)value, true, enumPacketType.SetValue, false);
            }
        }
        public double EXT_TriggerFREQ
        {
            get { return m_fEXT_Trigger_FREQ; }
        }
        public int MinTriggerFREQ
        {
            get { return m_iMinTriggerFREQ; }
        }
        public int MaxTriggerFREQ
        {
            get { return m_iMaxTriggerFREQ; }
        }
        /// <summary>
        /// false : EXT , true =INT
        /// </summary>

        /// <summary>
        /// false : EXT , true =INT
        /// </summary>
        public bool Modulation
        {
            get { return m_Status[(bit)STATUS.MODULATION]; }
            set
            {
                AddPacket(GLPM_V8_CMD.SET_SOURCE_MODULATION, value == true ? 1 : 0, true, enumPacketType.SetValue, false);
            }
        }
        /// <summary>
        /// false : EXT , true =INT
        /// </summary>
        public bool EmissionControl
        {
            get { return m_bEmissionCtrl; }
            set
            {
                AddPacket(GLPM_V8_CMD.SET_EMIISION_CONTROL, value == true ? 1 : 0, true, enumPacketType.SetValue, false);
            }
        }
        /// <summary>
        /// false : EXT , true =INT
        /// </summary>
        public bool PowerSourceControl
        {
            get { return m_bPowerSourceCtrl; }
            set
            {
                AddPacket(GLPM_V8_CMD.SET_POWER_SOURCE, value == true ? 1 : 0, true, enumPacketType.SetValue, false);
            }
        }
        public void Reset()
        {
            AddPacket(GLPM_V8_CMD.RESET_ERR, "", false, enumPacketType.SetValue, false);
        }

    }
}
