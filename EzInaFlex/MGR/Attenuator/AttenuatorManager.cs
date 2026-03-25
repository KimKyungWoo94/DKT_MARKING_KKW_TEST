using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Attenuator
{
    public sealed class AttenuatorManager : SingleTone<AttenuatorManager>, IDisposable
    {
        private bool IsDisposed = false;
        private bool IsDisposing = false;
        private List<AttuenuatorInterface> m_DeviceList;
        private string  m_strXmlFilePath;
        private string m_strFileName;
        ~AttenuatorManager()
        {
            Dispose(false);
        }
        protected override void OnCreateInstance()
        {
            m_DeviceList=new List<AttuenuatorInterface>();
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
                m_strXmlFilePath=FA.DIR.CFG+"Comm\\Attenuator\\";          
                m_strFileName="AttenuatorDeviceLinkData.xml";
                DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
				if (DirInfo.Exists == false)
					DirInfo.Create();
                AttenuatorDeviceDataPaser pDataPaser=new AttenuatorDeviceDataPaser();
                if (m_DeviceList.Count > 0)
                {
                    Commucation.CommunicationBase pItem=null;
                    foreach (AttuenuatorInterface item in m_DeviceList)
                    {
                        pItem=item as Commucation.CommunicationBase;
                        if(pItem!=null)
                        {
                            pDataPaser.AddLinkData(item.strID,item.DeviceType.FullName);
                            pDataPaser[pDataPaser.DeviceListCount-1].SerialComInfo=pItem.SerialCommInfo;
                            pDataPaser[pDataPaser.DeviceListCount-1].SocketInfo=pItem.SocketInfo;
                            pDataPaser[pDataPaser.DeviceListCount-1].iPacketDoneTimeout=pItem.iPacketDoneTimeout;
                            pDataPaser[pDataPaser.DeviceListCount-1].fMinPower=item.fMinPower;
                            pDataPaser[pDataPaser.DeviceListCount-1].fMaxPower=item.fMaxPower;
                        }
                        item.DisposeDevice();
                    }
                    pDataPaser.Save(m_strXmlFilePath+m_strFileName);
                }
                m_DeviceList.Clear();
            }
            IsDisposed = true;
            IsDisposing = false;
        }
        public void OpenDecive()
        {
            m_strXmlFilePath=FA.DIR.CFG+"Comm\\Attenuator\\";          
            m_strFileName="AttenuatorDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
				if (DirInfo.Exists == false)
					DirInfo.Create();
            m_DeviceList.Clear();
            AttenuatorDeviceDataPaser pDataPaser=AttenuatorDeviceDataPaser.Load(m_strXmlFilePath+m_strFileName);
            foreach(AttenuatorDeviceLinkData Item in pDataPaser.DeviceList)
            {
                switch(Item.strDeviceType)
                {
                    case "EzIna.Attenuator.OPTOGAMA.LPA":
                        {
                            if(Item.CommMethod==Commucation.enumCommMethod.SERIAL)
                            {
                                m_DeviceList.Add(new OPTOGAMA.LPA( Item.strID,
                                                                   Item.SerialComInfo,
                                                                   Item.iPacketDoneTimeout,
                                                                   Item.fMinPower,
                                                                   Item.fMaxPower
                                                                                ));
                            }
                            else if (Item.CommMethod==Commucation.enumCommMethod.SOCKET)
                            {
                                m_DeviceList.Add(new OPTOGAMA.LPA( Item.strID,
                                                                   Item.SocketInfo,
                                                                   Item.iPacketDoneTimeout,
                                                                   Item.fMinPower,
                                                                   Item.fMaxPower
                                                                                ));
                            }                            
                        }
                        break;
                   
                }
            }
        }
        public void TestFileSave()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Comm\\Attenuator\\";
            m_strFileName = "AttenuatorDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            AttenuatorDeviceDataPaser pTemp=new AttenuatorDeviceDataPaser();
            pTemp.AddLinkData("1","2");
            pTemp.Save(m_strXmlFilePath+m_strFileName);
        }
        public void SaveConfigFromDeviceList()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Comm\\Attenuator\\";
            m_strFileName = "AttenuatorDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            if (m_DeviceList.Count > 0)
            {
                AttenuatorDeviceDataPaser pDataPaser=new AttenuatorDeviceDataPaser();
                Commucation.CommunicationBase pItem=null;
                foreach (AttuenuatorInterface item in m_DeviceList)
                {
                    pItem=item as Commucation.CommunicationBase;
                    if(pItem!=null)
                    {
                        pDataPaser.AddLinkData(item.strID,item.DeviceType.FullName);
                        pDataPaser[pDataPaser.DeviceListCount-1].SerialComInfo=pItem.SerialCommInfo;
                        pDataPaser[pDataPaser.DeviceListCount-1].SocketInfo=pItem.SocketInfo;
                        pDataPaser[pDataPaser.DeviceListCount-1].iPacketDoneTimeout=pItem.iPacketDoneTimeout;
                        pDataPaser[pDataPaser.DeviceListCount-1].fMinPower=item.fMinPower;
                        pDataPaser[pDataPaser.DeviceListCount-1].fMaxPower=item.fMaxPower;
                    }                        
                }
                pDataPaser.Save(m_strXmlFilePath+m_strFileName);
            }
        }
        public void AddItem(AttuenuatorInterface a_Item)
        {
            m_DeviceList.Add(a_Item);
        }
        public AttuenuatorInterface GetItem(int a_IDX)
        {
            if (m_DeviceList.Count > 0 && a_IDX < m_DeviceList.Count)
                return m_DeviceList[a_IDX];
            return null;
        }    
        public AttuenuatorInterface this[ushort Index]
        {
           get
            {
               return GetItem(Index);
            }            
        }

        public void TreeView_Clear(TreeView a_pTreeView)
        {
            a_pTreeView.Nodes.Clear();
        }
        public void TreeView_Init(TreeView a_pTreeView)
        {

            a_pTreeView.Nodes.Clear();
            List<string> strBrandList = new List<string>();
            strBrandList.Clear();
            if (m_DeviceList.Count <= 0) return;
            TreeNode pNode = null;
            TreeNode pChild = null;
            int idx = 0;
            foreach (AttuenuatorInterface item in m_DeviceList)
            {
                pNode = null;
                pChild = null;
                switch (item.DeviceType.Name)
                {
                    case "LPA":
                        {
                            if (strBrandList.Contains("OPTOGAMA") == false)
                            {
                                strBrandList.Add("OPTOGAMA");
                                a_pTreeView.Nodes.Add("LPA", "OPTOGAMA");
                            }
                            pNode = a_pTreeView.Nodes["LPA"];
                            goto Create;
                        }
                        break;
              
                    default:
                        {

                        }
                        break;
                }
                Create:
                {

                    if (pNode != null)
                    {
                        pNode.ImageIndex = 2;
                        pNode.SelectedImageIndex = 2;
                        pChild = pNode.Nodes.Add((idx++).ToString(), item.strID, 0);
                        pChild.Tag = item;
                        pChild.ImageIndex = 0;
                        pNode.SelectedImageIndex = 0;
                    }

                }
            }//end foreach


        }
    }
}
