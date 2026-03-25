using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Light
{
    public sealed class LightManager : SingleTone<LightManager>, IDisposable
    {
        private bool IsDisposed = false;
        private bool IsDisposing = false;
        private List<EzIna.Light.LightInterface > m_DeviceList;
        private string  m_strXmlFilePath;
        private string m_strFileName;
        ~LightManager()
        {
            Dispose(false);
        }
        protected override void OnCreateInstance()
        {
            m_DeviceList=new List<EzIna.Light.LightInterface>();
            base.OnCreateInstance();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool a_Disposeing)
        {
            if (IsDisposed)
                return;

            if (a_Disposeing)
            {
                IsDisposing = true;
                if (m_DeviceList.Count > 0)
                {
                    foreach (EzIna.Light.LightInterface item in m_DeviceList)
                    {
                        item.DisposeLight();
                    }
                }
                m_DeviceList.Clear();
            }
            IsDisposed = true;
            IsDisposing = false;
        }
        public void SaveConfigFromDeviceList()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Comm\\Light\\";
            m_strFileName = "LightDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            if (m_DeviceList.Count > 0)
            {
                LightDeviceDataPaser pDataPaser=new LightDeviceDataPaser();
                Commucation.CommunicationBase pItem=null;
                foreach (LightInterface item in m_DeviceList)
                {
                    pItem=item as Commucation.CommunicationBase;
                    if(pItem!=null)
                    {
                        pDataPaser.AddLinkData(item.strID,item.DeviceType.FullName);
                        pDataPaser[pDataPaser.DeviceListCount-1].iChannelNo=item.ChannelCount;
                        pDataPaser[pDataPaser.DeviceListCount-1].SerialComInfo=pItem.SerialCommInfo;
                        pDataPaser[pDataPaser.DeviceListCount-1].SocketInfo=pItem.SocketInfo;
                        pDataPaser[pDataPaser.DeviceListCount-1].iPacketDoneTimeout=pItem.iPacketDoneTimeout;                                               
                    }                        
                }
                pDataPaser.Save(m_strXmlFilePath+m_strFileName);
            }
        }
        public void OpenDecive()
        {
            m_strXmlFilePath=FA.DIR.CFG+"Comm\\Light\\";          
            m_strFileName="LightDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
				if (DirInfo.Exists == false)
					DirInfo.Create();
            m_DeviceList.Clear();

            LightDeviceDataPaser pDataPaser = null;
            FileInfo XmlFileInfo = new FileInfo(m_strXmlFilePath + m_strFileName);
            if (XmlFileInfo.Exists == true)
            {
                pDataPaser = LightDeviceDataPaser.Load(XmlFileInfo.FullName);
            }
            else
            {
                pDataPaser = new LightDeviceDataPaser();
                pDataPaser.Save(XmlFileInfo.FullName);
            }           
            foreach(LightDeviceLinkData Item in pDataPaser.DeviceList)
            {
                if (Item != null)
                {
                    switch (Item.strDeviceType)
                    {
                        case "EzIna.Light.DaeShin.LP205R":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    m_DeviceList.Add(new EzIna.Light.DaeShin.LP205R(Item.iChannelNo,
                                                                                    Item.strID,
                                                                                    Item.SerialComInfo,
                                                                                    Item.iPacketDoneTimeout
                                                                                    ));
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    m_DeviceList.Add(new EzIna.Light.DaeShin.LP205R(Item.iChannelNo,
                                                                                    Item.strID,
                                                                                    Item.SocketInfo,
                                                                                    Item.iPacketDoneTimeout
                                                                                    ));
                                }
                            }
                            break;
                        case "EzIna.Light.JoySystem.JPSeries":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    m_DeviceList.Add(new EzIna.Light.JoySystem.JPSeries(Item.iChannelNo,
                                                                                        Item.strID,
                                                                                        Item.SerialComInfo,
                                                                                        Item.iPacketDoneTimeout
                                                                                        ));
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    m_DeviceList.Add(new EzIna.Light.JoySystem.JPSeries(Item.iChannelNo,
                                                                                        Item.strID,
                                                                                        Item.SocketInfo,
                                                                                        Item.iPacketDoneTimeout
                                                                                    ));
                                }
                            }
                            break;
                    }
                }              
            }
        }
        public void TestFileSave()
        {
            m_strXmlFilePath=FA.DIR.CFG+"Comm\\Light\\";          
            m_strFileName="LightDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
				if (DirInfo.Exists == false)
					DirInfo.Create();
            LightDeviceDataPaser pTemp=new LightDeviceDataPaser();
            pTemp.AddLinkData("1","2");
            pTemp.Save(m_strXmlFilePath+m_strFileName);
        }
        public void AddItem(EzIna.Light.LightInterface a_Item)
        {
            m_DeviceList.Add(a_Item);
        }
        public EzIna.Light.LightInterface GetItem(int a_IDX)
        {
            if (m_DeviceList.Count > 0 && a_IDX < m_DeviceList.Count)
                return m_DeviceList[a_IDX];
            return null;
        }    
        public EzIna.Light.LightInterface this[ushort Index]
        {
           get
            {
               return GetItem(Index);
            }            
        }     
    }
}
