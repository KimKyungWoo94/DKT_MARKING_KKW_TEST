using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

namespace EzIna.PowerMeter.Zentec
{
    public sealed partial class PLink:CommunicationBase,PowerMeterInterface
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="a_NumOfCh"></param>
       /// <param name="a_strName"></param>
       /// <param name="a_Setting"></param>
        public PLink(string a_strName,Commucation.SerialCom.stSerialSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {           
          
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="a_NumOfCh"></param>
       /// <param name="a_strName"></param>
       /// <param name="a_Setting"></param>
        public PLink(string a_strName,Commucation.SocketCom.stSocketSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {           
          
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
                m_bLoopEnable=false;
                m_pLoopThread.Join(100);
                if (m_pLoopThread.IsAlive)
                    m_pLoopThread.Abort();
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
        protected override bool ParsingPacketData(byte[] RecvData)
        {
           return true;
        }
        public double fMeasuredPower
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public double fMeasureMinPower
        {
            get { return 0.0; }
        }
        public double fMeasureMaxPower
        {
            get { return 0.0; }
        }
        public double fMeasureAvgPower
        {
            get
            {
                return 0.0;
            }
        }
        public bool IsDeviceConnected
        {
            get
            {
               return base.IsConnected();
            }
        }

        public string strID
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Type DeviceType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsMeasuring
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsZeroSetExecuting
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisposePM()
        {

            base.Dispose();
        }

        public eWaveLength GetWaveLength()
        {
            throw new NotImplementedException();
        }

        public void ResetDevice()
        {
            throw new NotImplementedException();
        }

        public void SetPowerOffset(double a_value)
        {
            throw new NotImplementedException();
        }

        public void SetWaveLength(int a_value)
        {
            throw new NotImplementedException();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public bool SetZero()
        {
            throw new NotImplementedException();
        }

        public eZeroSetStataus GetZeroSetStatus()
        {
            throw new NotImplementedException();
        }

        public bool MeasureStart(int a_Interval=10,uint a_InitAVGCount=100)
        {
            throw new NotImplementedException();
        }

        public void MeasureStop()
        {
            throw new NotImplementedException();
        }

        bool IsOverMeasureDefaultCount
        {
            get
            {
               return false;

            }
        }

        bool PowerMeterInterface.IsOverMeasureDefaultCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public uint GetMeasureCount()
        {
            return 0;
        }
        public void SetWaveLength(eWaveLength a_value)
        {
            throw new NotImplementedException();
        }

    }
}
