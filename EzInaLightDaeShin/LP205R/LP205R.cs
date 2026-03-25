using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

namespace EzIna.Light.DaeShin
{
    public sealed partial class LP205R : CommunicationBase, LightInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_NumOfCh"></param>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public LP205R(int a_NumOfCh,string a_strName,Commucation.SerialCom.stSerialSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {           
            m_iIntensity=new int [a_NumOfCh];            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_NumOfCh"></param>
        /// <param name="a_strName"></param>
        /// <param name="a_Setting"></param>
        /// <param name="a_iPacketDoneTimeout"></param>
        public LP205R(int a_NumOfCh,string a_strName,Commucation.SocketCom.stSocketSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {           
            m_iIntensity=new int [a_NumOfCh];
        }
        /// <summary>
        /// 
        /// </summary>
        public void DisposeLight()
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
                      
            }
            //Free Unmanage Objects here           
            //SaveConfig(m_strConfigSection);

            IsDisposed = true;
            IsDisposing = false;            
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
            while (m_bLoopEnable)
            {
                Thread.Sleep(1);
                base.Execute();
            }
        }
        /// <summary>
        /// No Receive 
        /// </summary>
        /// <param name="RecvData"></param>
        /// <returns></returns>
        protected override bool ParsingPacketData(byte[] RecvData)
        {
            // No Recevie 
            return true; 
        }

        private void ADD_LP205R_SetValue_Packet(int a_Ch,int a_Value)
        {
            stCommPacket pPacket =new stCommPacket();
            pPacket.StrSetValue=string.Format("{0}{1:000}",a_Ch,a_Value);
            pPacket.StrSendMsg=string.Format("\x02{0}\x03", pPacket.StrSetValue);
            pPacket.PacketType=enumPacketType.SetValue;
            pPacket.bSendMode=true;
            pPacket.bRecvMode=false;
            pPacket.bRecvUse=false;
            base.AddExcutePacket(pPacket);
        }
        public int ChannelCount
        {
            get
            {
                return this.m_iIntensity.Length;
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

        public int GetIntensity(int a_Ch)
        {
            if(a_Ch>0&&a_Ch<=this.m_iIntensity.Length)
            {
                 return  m_iIntensity[a_Ch-1];
            }
           return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Ch"></param>
        /// <returns></returns>
        public bool GetOnOff(int a_Ch)
        {
            if (a_Ch > 0 && a_Ch <= this.m_iIntensity.Length)
            {
               return  m_iIntensity[a_Ch-1]>0;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Ch"></param>
        /// <param name="a_Value"></param>
        public void SetIntensity(int a_Ch,int a_Value)
        {
            if (a_Ch > 0 && a_Ch <= this.m_iIntensity.Length)
            {
                m_iIntensity[a_Ch-1]=a_Value;
                ADD_LP205R_SetValue_Packet(a_Ch,m_iIntensity[a_Ch-1]);
            }
        }
        /// <summary>
        /// Light On Off , 메모리에 저장되지않고 바로 패킷 전송 
        /// <para>현재 Intensity 0 이면 계속 꺼져있다</para>
        /// </summary>
        /// <param name="a_Ch">Ch Start 1~</param>
        /// <param name="a_Value">On Off</param>
        public void SetOnOff(int a_Ch,bool a_Value)
        {
            if (a_Ch > 0 && a_Ch <= this.m_iIntensity.Length)
            {
                if(a_Value)
                {                   
                    ADD_LP205R_SetValue_Packet(a_Ch, m_iIntensity[a_Ch - 1]);
                } 
                else
                {                    
                    ADD_LP205R_SetValue_Packet(a_Ch, 0);
                }             
            }
        }
    }
}
