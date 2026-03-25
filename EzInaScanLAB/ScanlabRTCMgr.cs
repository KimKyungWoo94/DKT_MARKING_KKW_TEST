using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Scanner
{
    public sealed class ScanlabRTCMgr : SingleTone<ScanlabRTCMgr>
    {

        private bool IsDisposed = false;
        private bool IsDisposing = false;

        protected override void OnCreateInstance()
        {
            m_DeviceList = new List<ScanlabRTC5>();
            base.OnCreateInstance();
        }

        private List<ScanlabRTC5> m_DeviceList;
        /// <summary>
        /// 
        /// </summary>
        ~ScanlabRTCMgr()
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
                if (m_DeviceList.Count > 0)
                {
                    foreach (ScanlabRTC5 item in m_DeviceList)
                    {
                        item.Dispose();
                    }
                    SaveConfigFromDeviceList();
                }
                m_DeviceList.Clear();
            }
            IsDisposed = true;
            IsDisposing = false;
        }
        public void OpenDecive()
        {

            m_DeviceList.Clear();
            ScanlabRTC5LinkDataPaser pDataPaser = ScanlabRTC5LinkDataPaser.Load(ScanlabRTC5.ConfigScanlabXMLFileFullPath);
            if (ScanlabRTC5.bDllInit == false)
                ScanlabRTC5.InitializeDriver();
            uint BoardCount = ScanlabRTC5.RTCBoardCount;
            foreach (ScanlabRTC5LinkData Item in pDataPaser.DeviceList)
            {

#if SIM
                m_DeviceList.Add(new ScanlabRTC5(Item));
#else
		   					if(BoardCount>0 && Item.CardNo <=BoardCount)
                {                  
                    m_DeviceList.Add(new ScanlabRTC5(Item));                      
                }              
      
#endif
            }
        }
        public void SaveConfigFromDeviceList()
        {
            if (m_DeviceList.Count > 0)
            {
                ScanlabRTC5LinkDataPaser pDataPaser = new ScanlabRTC5LinkDataPaser();
                foreach (ScanlabRTC5 item in m_DeviceList)
                {
                    pDataPaser.AddLinkData(item.ConfigData);
                }
                pDataPaser.Save(ScanlabRTC5.ConfigScanlabXMLFileFullPath);
            }
        }
        public void Execute(object pobj)
        {
            foreach (ScanlabRTC5 Item in m_DeviceList)
            {
                Item.Execute();
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
            strBrandList.Add("ScanLab");
            if (m_DeviceList.Count <= 0) return;
            TreeNode pNode = null;
            TreeNode pChild = null;
            int idx = 0;
            foreach (ScanlabRTC5 item in m_DeviceList)
            {
                pNode = null;
                pChild = null;

                a_pTreeView.Nodes.Add("ScanlabRTC5", "ScanLab");
                pNode = a_pTreeView.Nodes["ScanlabRTC5"];
                goto Create;
            Create:
                {
                    if (pNode != null)
                    {
                        pNode.ImageIndex = 2;
                        pNode.SelectedImageIndex = 2;
                        pChild = pNode.Nodes.Add((idx++).ToString(), item.ToString(), 0);
                        pChild.Tag = item;
                        pChild.ImageIndex = 0;
                        pNode.SelectedImageIndex = 0;
                    }
                }
            }//end foreach


        }
        public void GetAllDeviceStatus()
        {
            foreach (ScanlabRTC5 item in m_DeviceList)
            {
                item.Execute();
            }
        }
        public ScanlabRTC5 this[int i]
        {
            get
            {
                if (i > -1 && i < this.m_DeviceList.Count)
                {
                    return this.m_DeviceList[i];
                }
                return null;
            }
        }
    }
}
