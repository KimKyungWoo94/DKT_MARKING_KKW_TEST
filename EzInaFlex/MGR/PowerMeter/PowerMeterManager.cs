using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.PowerMeter
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PowerMeterManager:SingleTone<PowerMeterManager>,IDisposable
    {
        private bool IsDisposed = false;
        private bool IsDisposing = false;
        string  m_strXmlFilePath;
        string  m_strFileName;
        private List<EzIna.PowerMeter.PowerMeterInterface> m_DeviceList;
        /// <summary>
        /// 
        /// </summary>
        ~PowerMeterManager()
        {
            Dispose(false);
        }
        /// <summary>
        /// 
        /// </summary>
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
                if(m_DeviceList.Count>0)
                {
                    foreach(EzIna.PowerMeter.PowerMeterInterface item in m_DeviceList)
                    {
                        item.DisposePM();
                    }
                }
                m_DeviceList.Clear();                
            }
            IsDisposed = true;
            IsDisposing = false;
        }

       
        /// <summary>
        /// 생성자 
        /// </summary>
        protected override void OnCreateInstance()
        {
            m_DeviceList=new List<EzIna.PowerMeter.PowerMeterInterface>();
            
            base.OnCreateInstance();
        }
        public void SaveConfigFromDeviceList()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Comm\\PowerMeter\\";
            m_strFileName = "PowerMeterDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            if (m_DeviceList.Count > 0)
            {
                PowerMeterDeviceDataPaser pDataPaser=new PowerMeterDeviceDataPaser();
                Commucation.CommunicationBase pItem=null;
                foreach (EzIna.PowerMeter.PowerMeterInterface item in m_DeviceList)
                {
                    pItem=item as Commucation.CommunicationBase;
                    if(pItem!=null)
                    {
                        pDataPaser.AddLinkData(item.strID,item.DeviceType.FullName);
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
            m_strXmlFilePath = FA.DIR.CFG + "Comm\\PowerMeter\\";
            m_strFileName="PowerMeterDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
			if (DirInfo.Exists == false)
					DirInfo.Create();
            m_DeviceList.Clear();
            PowerMeterDeviceDataPaser pDataPaser=null;
            FileInfo XmlFileInfo=new FileInfo(m_strXmlFilePath+m_strFileName);
            if (XmlFileInfo.Exists == true)
            {
                pDataPaser = PowerMeterDeviceDataPaser.Load(XmlFileInfo.FullName);
            }
            else
            {
                pDataPaser=new PowerMeterDeviceDataPaser();
                pDataPaser.Save(XmlFileInfo.FullName);
            }           
            foreach(PowerMeterDeviceLinkData Item in pDataPaser.DeviceList)
            {
                if(Item!=null)
                {
                    switch (Item.strDeviceType)
                    {
                        #region Ohpir.SPC
                        case "EzIna.PowerMeter.Ohpir.SPC":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    m_DeviceList.Add(new Ohpir.SPC(Item.strID,
                                                                        Item.SerialComInfo,
                                                                        Item.iPacketDoneTimeout
                                                                        ));
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    m_DeviceList.Add(new Ohpir.SPC(Item.strID,
                                                                        Item.SocketInfo,
                                                                        Item.iPacketDoneTimeout
                                                                        ));
                                }
                            }
                            break;
                            #endregion Ohpir.SPC

                    }
                }
               
            }
        }        
        public void TestFileSave()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Comm\\PowerMeter\\";
            m_strFileName="PowerMeterDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
				if (DirInfo.Exists == false)
					DirInfo.Create();
            PowerMeterDeviceDataPaser pTemp=new PowerMeterDeviceDataPaser();
            pTemp.AddLinkData("1","2");
            pTemp.Save(m_strXmlFilePath+m_strFileName);    
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
            foreach (PowerMeterInterface item in m_DeviceList)
            {
                pNode = null;
                pChild = null;
                switch (item.DeviceType.Name)
                {
                    //Ohpir
                    case "SPC":
                        {
                            if (strBrandList.Contains("MKS") == false)
                            {
                                strBrandList.Add("MKS");
                                a_pTreeView.Nodes.Add("Ohpir", "MKS");
                            }
                            pNode = a_pTreeView.Nodes["Ohpir"];
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Item"></param>
        public void AddItem(EzIna.PowerMeter.PowerMeterInterface a_Item)
        {

            m_DeviceList.Add(a_Item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Index"></param>
        /// <returns></returns>
        public EzIna.PowerMeter.PowerMeterInterface GetItem(ushort a_Index)
        {

            if (m_DeviceList.Count > 0 && a_Index < m_DeviceList.Count)
                return m_DeviceList[a_Index];
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Index"></param>
        /// <returns>Laser Item </returns>
        public EzIna.PowerMeter.PowerMeterInterface this[ushort Index]
        {
           get
            {
               return GetItem(Index);
            }            
        }            
    }
}
