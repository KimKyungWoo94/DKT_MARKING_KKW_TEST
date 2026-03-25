using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.Commucation;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Text.RegularExpressions;

namespace EzIna.Light.JoySystem
{
    public sealed partial class JPSeries : CommunicationBase, LightInterface
    {
      /// <summary>
       /// 
       /// </summary>
       /// <param name="a_NumOfCh"></param>
       /// <param name="a_strName"></param>
       /// <param name="a_Setting"></param>
        public JPSeries(int a_NumOfCh,string a_strName,Commucation.SerialCom.stSerialSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {           
            for(int i=0;i<a_NumOfCh;i++)
            {
                m_DicIntensity.Add(i+1,0);
            }
            
            m_strConfigSection=  a_strName;     
            Connection();  
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="a_NumOfCh"></param>
       /// <param name="a_strName"></param>
       /// <param name="a_Setting"></param>
        public JPSeries(int a_NumOfCh,string a_strName,Commucation.SocketCom.stSocketSetting a_Setting,int a_iPacketDoneTimeout)
            :base(a_Setting,a_iPacketDoneTimeout)
        {
            for (int i = 0; i < a_NumOfCh; i++)
            {
                m_DicIntensity.Add(i + 1, 0);
            }
            m_strConfigSection =  a_strName;
            Connection();
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
            //SaveConfig(m_strConfigSection);

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
            while (m_bLoopEnable)
            {
                Thread.Sleep(10);
                base.Execute();
            }
        }
        protected override void ConnectSuceessAfterWork()
        {
            for(int i=0;i<m_DicIntensity.Count;i++)
            {
                AddJPSeriesPacket(i+1,0,enumPacketType.GetValue);
            }
            base.ConnectSuceessAfterWork();
        }
        protected override void DisConnectSuceessAfterWork()
        {
            this.m_bHandShakeSuccess=false;
            base.DisConnectSuceessAfterWork();
        }
        private void AddJPSeriesPacket(int a_Ch,int a_value,enumPacketType a_Type)
        {
            stCommPacket pPacket=new stCommPacket();                        
            switch(a_Type)
            {
                case enumPacketType.SetValue:
                    {
                        pPacket.StrSetValue=string.Format("{0}{1:D3}",a_Ch,a_value);                        
                    }
                    break;
               case enumPacketType.GetValue:
                    {
                        pPacket.StrSetValue=string.Format("{0}",a_Ch);                        
                    }
                    break;
            }   
            pPacket.StrSendMsg=string.Format("#{0}{1}&",a_Type==enumPacketType.SetValue?"A":"?",pPacket.StrSetValue);         
            pPacket.bSendMode=true;
            pPacket.bRecvMode=false;
            pPacket.bRecvUse=true;
            base.AddExcutePacket(pPacket);
        }
        protected override bool ParsingPacketData(byte[] RecvData)
        {
            if (base.CurrentPacket == null)
                return true;
            try
            {
                string[] PacketstrList;
                string strRecvPacket;
                string strValue;
                int iRecvValue;
                int iCh=0;
                int iValue=0;            
                m_RecvDataStringBuilder.Clear();
                m_RecvDataStringBuilder.Append(base.CurrentPacket.StrReciveMsg);
                m_RecvDataStringBuilder.Append(CurrentRecvEncodeing.GetString(RecvData));
                base.CurrentPacket.StrReciveMsg = m_RecvDataStringBuilder.ToString();
                MatchCollection PacketList=Regex.Matches(base.CurrentPacket.StrReciveMsg, @"^['#'](.*?)['&']");
                if(PacketList.Count>0)
                {
                    foreach (Match Item in PacketList)
                    {
                        PacketstrList = Item.Groups[0].Value.Split(new char[] { '#', '&' }, StringSplitOptions.RemoveEmptyEntries);
                        strRecvPacket = PacketstrList[0];
                        switch (strRecvPacket[0])
                        {
                            case 'A':
                                {
                                    strValue = strRecvPacket.TrimStart('A');
                                    int.TryParse(strValue, out iRecvValue);
                                    iCh = iRecvValue / 1000;
                                    iValue = iRecvValue % 1000;
                                }
                                break;
                            case '?':
                                {
                                    strValue = strRecvPacket.TrimStart('?');
                                    int.TryParse(strValue, out iRecvValue);
                                    iCh = iRecvValue / 1000;
                                    iValue = iRecvValue % 1000;
                                }
                                break;
                        }
                        SetIntensityFromPacket(iCh, iValue);
                    }
                    this.m_bHandShakeSuccess=true; 
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
        private bool SetIntensityFromPacket(int a_Ch,int a_value)
        {
            if(m_DicIntensity.ContainsKey(a_Ch))
            {             
                m_DicIntensity[a_Ch]=a_value;               
                return true;
            }
            return false;
        }

        public int ChannelCount
        {
            get
            {
                return m_DicIntensity.Count;
            }
        }
   
        public int GetIntensity(int a_Ch)
        {
            if (m_DicIntensity.ContainsKey(a_Ch))
            {
                return m_DicIntensity[a_Ch];
            }
            return 0;
        }

        public bool GetOnOff(int a_Ch)
        {
            if (m_DicIntensity.ContainsKey(a_Ch))
            {
                if (m_DicIntensity[a_Ch]>0)
                {
                    return true;
                }                
            }
            return false;
        }
        public void SetIntensity(int a_Ch, int a_Value)
        {
            if (m_DicIntensity.ContainsKey(a_Ch))
            {
                if (a_Value>=0)
                {
                    AddJPSeriesPacket(a_Ch, a_Value, enumPacketType.SetValue);
                }                
            }
        }

        public void SetOnOff(int a_Ch, bool a_Value)
        {
            if (m_DicIntensity.ContainsKey(a_Ch))
            {
                AddJPSeriesPacket(a_Ch,a_Value==true? 1:0,enumPacketType.SetValue);
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
    }
}
