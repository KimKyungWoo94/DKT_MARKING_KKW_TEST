using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.TempMeasure
{
    public sealed class TempMeasureManager : SingleTone<TempMeasureManager>, IDisposable
    {
        private bool IsDisposed = false;
        private bool IsDisposing = false;
        private List<EzIna.TempMeasure.ITempMeasure > m_DeviceList;
        private string  m_strXmlFilePath;
        private string m_strFileName;
        ~TempMeasureManager()
        {
            Dispose(false);
        }
        protected override void OnCreateInstance()
        {
            m_DeviceList=new List<EzIna.TempMeasure.ITempMeasure>();
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
                    foreach (EzIna.TempMeasure.ITempMeasure item in m_DeviceList)
                    {
                        item.DisposeTM();
                    }
                }
                m_DeviceList.Clear();
            }
            IsDisposed = true;
            IsDisposing = false;
        }
        public void SaveConfigFromDeviceList()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Comm\\TempMeasure\\";
            m_strFileName = "TempMeasureDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            if (m_DeviceList.Count > 0)
            {
                TempMeasureDeviceDataPaser pDataPaser=new TempMeasureDeviceDataPaser();
                Commucation.CommunicationBase pItem=null;
                foreach (ITempMeasure item in m_DeviceList)
                {
                    pItem=item as Commucation.CommunicationBase;
                    if(pItem!=null)
                    {
                        pDataPaser.AddLinkData(item.strID,item.DeviceType.FullName);
                        //pDataPaser[pDataPaser.DeviceListCount-1].iChannelNo=item.ChannelCount;
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
            m_strXmlFilePath=FA.DIR.CFG+"Comm\\TempMeasure\\";          
            m_strFileName="TempMeasureDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
				   if (DirInfo.Exists == false)
					   DirInfo.Create();
            m_DeviceList.Clear();

            TempMeasureDeviceDataPaser pDataPaser = null;
            FileInfo XmlFileInfo = new FileInfo(m_strXmlFilePath + m_strFileName);
            if (XmlFileInfo.Exists == true)
            {
                pDataPaser = TempMeasureDeviceDataPaser.Load(XmlFileInfo.FullName);
            }
            else
            {
                pDataPaser = new TempMeasureDeviceDataPaser();
                pDataPaser.Save(XmlFileInfo.FullName);
            }           
            foreach(TempMeasureDeviceLinkData Item in pDataPaser.DeviceList)
            {
                if (Item != null)
                {
                    switch (Item.strDeviceType)
                    {
                        case "EzIna.TempMeasure.OMEGA.CN8PT":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    /*m_DeviceList.Add(new EzIna.Light.DaeShin.LP205R(Item.iChannelNo,
                                                                                    Item.strID,
                                                                                    Item.SerialComInfo,
                                                                                    Item.iPacketDoneTimeout
                                                                                    ));*/
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    m_DeviceList.Add(new EzIna.TempMeasure.OMEGA.CN8PT(Item.strID,
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
            m_strXmlFilePath=FA.DIR.CFG+"Comm\\TempMeasure\\";          
            m_strFileName="TempMeasureDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
						if (DirInfo.Exists == false)
						DirInfo.Create();
            TempMeasureDeviceDataPaser pTemp=new TempMeasureDeviceDataPaser();
            pTemp.AddLinkData("1","2");
            pTemp.Save(m_strXmlFilePath+m_strFileName);
        }
        public void AddItem(EzIna.TempMeasure.ITempMeasure a_Item)
        {
            m_DeviceList.Add(a_Item);
        }
        public EzIna.TempMeasure.ITempMeasure GetItem(int a_IDX)
        {
            if (m_DeviceList.Count > 0 && a_IDX < m_DeviceList.Count)
                return m_DeviceList[a_IDX];
            return null;
        }    
        public EzIna.TempMeasure.ITempMeasure this[ushort Index]
        {
           get
            {
               return GetItem(Index);
            }            
        }     
    }
}
