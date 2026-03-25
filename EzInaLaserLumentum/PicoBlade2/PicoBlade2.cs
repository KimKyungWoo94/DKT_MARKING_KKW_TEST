using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Threading;
using EzIna.Laser;
using System.Xml;
using System.Xml.Schema;

namespace EzIna.Laser.Lumentum
{
    
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class PicoBlade2 :CommunicationBase, LaserInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public PicoBlade2(string a_strName,Commucation.SerialCom.stSerialSetting a_Setting,int a_iPacketDoneTimeout):base(a_Setting,a_iPacketDoneTimeout)
        {
            m_strConfigSection=a_strName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public PicoBlade2(string a_strName,Commucation.SocketCom.stSocketSetting a_Setting,int a_iPacketDoneTimeout):base(a_Setting,a_iPacketDoneTimeout)
        {
             m_strConfigSection=a_strName;
        }
        /// <summary>
        /// 
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
            if (IsDisposed)
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
                IsDisposing = true;               
                // Free any other managed objects here.                
            }
            //Free Unmanage Objects here               
            IsDisposed = true;
            IsDisposing = false;
            base.Dispose(a_Disposeing);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
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
                Thread.Sleep(1);
                base.Execute();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void ConnectSuceessAfterWork()
        {
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
        /// <param name="RecvData"></param>
        /// <returns></returns>
        protected override bool ParsingPacketData(byte[] RecvData)
        {
            if (base.CurrentPacket == null)
                return true;
            try
            {

                 this.m_bHandShakeSuccess=true;
                 return true;
            }
            catch
            {

            }
            return false;
            
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
            get { return "";}
        }
        /// <summary>
        /// 
        /// </summary>
        public double RepetitionRate
        {
            get
            {
                return 0.0;
            }

            set
            {
                
            }
        }
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
				public double DiodeCurrent
				{
						get
						{
								return 0.0;
						}
						set
						{

						}
				}

				public double SetDiodeCurrent
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
				/// 
				/// </summary>
				public new bool IsConnected
        {
            get
            {
               return base.IsConnected()&this.m_bHandShakeSuccess;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCommConnected
        {
            get { return base.IsConnected(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSystemFault
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsLaserReady
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsShutterOpen
        {
            get
            {
                return false;
            }
        }


			
				/// <summary>
				/// 
				/// </summary>
				public Type DeviceType
        {
            get
            {
                return this.GetType();
            }
        }


				/// <summary>
				/// 
				/// </summary>
				public TRIG_MODE TriggerMode
				{
						get { return TRIG_MODE.INT;}
						set { }
				}
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan RemainWarmUptime
        {
            get
            {
                return new TimeSpan();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ShutterOpen
        {
            set
            {
                
            }
        }

				public bool IsEmissionOn
				{
						get
						{
								throw new NotImplementedException();
						}
				}

				public bool EmissionOnOff
				{
						set
						{
								throw new NotImplementedException();
						}
				}

				public bool IsQSW_On
				{
						get
						{
								throw new NotImplementedException();
						}
				}

				public bool QSW_OnOff
				{
						set
						{
								throw new NotImplementedException();
						}
				}

				public bool IsGateOpen
				{
						get
						{
								throw new NotImplementedException();
						}
				}

				public bool GateOpen
				{
						set
						{
								throw new NotImplementedException();
						}
				}

				public GATE_MODE GateMode
				{
						get
						{
								throw new NotImplementedException();
						}

						set
						{
								throw new NotImplementedException();
						}
				}
				/// <summary>
				/// 
				/// </summary>
				public void Reset()
        {

        }
    }
}
