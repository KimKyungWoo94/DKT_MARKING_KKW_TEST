using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.Laser
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class LaserManager : SingleTone<LaserManager>, IDisposable
    {
        private bool IsDisposed = false;
        private bool IsDisposing = false;
        string m_strXmlFilePath;
        string m_strFileName;
        string m_strPowerTableFileName;
        private List<EzIna.Laser.LaserInterface> m_DeviceList;
        private Dictionary<string, LaserPwrTableData> m_DicPowerTableList;

        Color m_DGV_SelectionBackColor = Color.SteelBlue;
        Color m_DGV_SelectionForeColor = Color.White;
        /// <summary>
        /// 
        /// </summary>
        ~LaserManager()

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
                    foreach (EzIna.Laser.LaserInterface item in m_DeviceList)
                    {
                        item.DisposeLaser();
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
            m_DeviceList = new List<EzIna.Laser.LaserInterface>();
            m_DicPowerTableList = new Dictionary<string, LaserPwrTableData>();
            base.OnCreateInstance();
        }
        public void SaveConfigFromDeviceList()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Laser\\";
            m_strFileName = "LaserDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            if (m_DeviceList.Count > 0)
            {
                LaserDeviceDataPaser pDataPaser = new LaserDeviceDataPaser();
                Commucation.CommunicationBase pItem = null;
                foreach (LaserInterface item in m_DeviceList)
                {
                    pItem = item as Commucation.CommunicationBase;
                    if (pItem != null)
                    {
                        pDataPaser.AddLinkData(item.strID, item.DeviceType.FullName);
                        pDataPaser[pDataPaser.DeviceListCount - 1].SerialComInfo = pItem.SerialCommInfo;
                        pDataPaser[pDataPaser.DeviceListCount - 1].SocketInfo = pItem.SocketInfo;
                        pDataPaser[pDataPaser.DeviceListCount - 1].iPacketDoneTimeout = pItem.iPacketDoneTimeout;
                    }
                }
                pDataPaser.Save(m_strXmlFilePath + m_strFileName);
            }
        }
        public void TestFileSave()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Laser\\";
            m_strFileName = "LaserDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            LaserDeviceDataPaser pTemp = new LaserDeviceDataPaser();
            pTemp.AddLinkData("1", "2");
            pTemp.Save(m_strXmlFilePath + m_strFileName);
        }
        public void OpenDecive()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Laser\\";
            m_strFileName = "LaserDeviceLinkData.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            m_DeviceList.Clear();

            LaserDeviceDataPaser pDataPaser = null;
            FileInfo XmlFileInfo = new FileInfo(m_strXmlFilePath + m_strFileName);
            if (XmlFileInfo.Exists == true)
            {
                pDataPaser = LaserDeviceDataPaser.Load(XmlFileInfo.FullName);
            }
            else
            {
                pDataPaser = new LaserDeviceDataPaser();
                pDataPaser.Save(XmlFileInfo.FullName);
            }
            foreach (LaserDeviceLinkData Item in pDataPaser.DeviceList)
            {
                if (Item != null)
                {
                    switch (Item.strDeviceType)
                    {
                        #region Talon
                        case "EzIna.Laser.Talon.Talon355":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    m_DeviceList.Add(new Talon.Talon355(Item.strID,
                                                                        Item.SerialComInfo,
                                                                        Item.iPacketDoneTimeout
                                                                        ));
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    m_DeviceList.Add(new Talon.Talon355(Item.strID,
                                                                        Item.SocketInfo,
                                                                        Item.iPacketDoneTimeout
                                                                        ));
                                }
                            }
                            break;
                        #endregion Talon
                        #region PicoBlade2
                        case "EzIna.Laser.Lumentum.PicoBlade2":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    m_DeviceList.Add(new Lumentum.PicoBlade2(Item.strID,
                                                                             Item.SerialComInfo,
                                                                             Item.iPacketDoneTimeout
                                                                             ));
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    m_DeviceList.Add(new Lumentum.PicoBlade2(Item.strID,
                                                                             Item.SocketInfo,
                                                                             Item.iPacketDoneTimeout
                                                                             ));

                                }
                            }
                            break;
                        #endregion PicoBlade2
                        #region IPG.GLPM
                        case "EzIna.Laser.IPG.GLPM":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    m_DeviceList.Add(new IPG.GLPM(Item.strID,
                                                                  Item.SerialComInfo,
                                                                  Item.iPacketDoneTimeout
                                                                  ));
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    /*m_DeviceList.Add(new IPG.GLPM(Item.strID,
                                                                  Item.SocketInfo,
                                                                  Item.iPacketDoneTimeout
                                                                  ));*/
                                    // Socket Not Support
                                }
                            }
                            break;
                        case "EzIna.Laser.IPG.GLPM_V8":
                            {
                                if (Item.CommMethod == Commucation.enumCommMethod.SERIAL)
                                {
                                    m_DeviceList.Add(new IPG.GLPM_V8(Item.strID,
                                                                  Item.SerialComInfo,
                                                                  Item.iPacketDoneTimeout
                                                                  ));
                                }
                                else if (Item.CommMethod == Commucation.enumCommMethod.SOCKET)
                                {
                                    /*m_DeviceList.Add(new IPG.GLPM(Item.strID,
                                                                  Item.SocketInfo,
                                                                  Item.iPacketDoneTimeout
                                                                  ));*/
                                    // Socket Not Support
                                }
                            }
                            break;
                            #endregion IPG.GLPM
                    }
                }
            }
        }
        public void OpenPowerTableDataes()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Laser\\";
            m_strPowerTableFileName = "LaserPowerTable.xml";
            DirectoryInfo DirInfo = new DirectoryInfo(m_strXmlFilePath);
            if (DirInfo.Exists == false)
                DirInfo.Create();
            m_DicPowerTableList.Clear();

            LaserPowerTablePaser pDataPaser = null;
            FileInfo XmlFileInfo = new FileInfo(m_strXmlFilePath + m_strPowerTableFileName);
            if (XmlFileInfo.Exists == true)
            {
                pDataPaser = LaserPowerTablePaser.Load(XmlFileInfo.FullName);
            }
            else
            {
                pDataPaser = new LaserPowerTablePaser();
                pDataPaser.Save(XmlFileInfo.FullName);
            }
            foreach (LaserPwrTableData Item in pDataPaser.DataLinkList)
            {
                if (m_DicPowerTableList.ContainsKey(Item.iDefaultFrequency.ToString()) == false)
                {
                    m_DicPowerTableList.Add(Item.iDefaultFrequency.ToString(), Item);
                }
            }
        }
        public void SavePowerTableData()
        {
            m_strXmlFilePath = FA.DIR.CFG + "Laser\\";
            m_strPowerTableFileName = "LaserPowerTable.xml";
            string strTableFilePath = m_strXmlFilePath + m_strPowerTableFileName;
            LaserPowerTablePaser pDataPaser = new LaserPowerTablePaser();
            foreach (KeyValuePair<string, LaserPwrTableData> Item in m_DicPowerTableList)
            {
                pDataPaser.AddTableData(Item.Value);
            }
            if (File.Exists(strTableFilePath))
            {
                File.Copy(strTableFilePath, string.Format("{0}{1}", m_strXmlFilePath, string.Format("LaserPowerTable{0}Bak.xml", DateTime.Now.ToString("yyyyMMddHHmmss"))), true);
            }
            pDataPaser.Save(strTableFilePath);
        }
        public List<KeyValuePair<string, LaserPwrTableData>> LaserTableList
        {
            get { return m_DicPowerTableList.ToList(); }
        }
        public LaserPwrTableData GetPwrTableData(int a_iFrequency)
        {
            if (m_DicPowerTableList.ContainsKey(a_iFrequency.ToString()))
            {
                return m_DicPowerTableList[a_iFrequency.ToString()];
            }
            return null;
        }
        public LaserPwrTableData GetPwrTableData(string a_strFrequency)
        {
            if (m_DicPowerTableList.ContainsKey(a_strFrequency))
            {
                return m_DicPowerTableList[a_strFrequency];
            }
            return null;
        }
        public bool SetPwrTableDataDefaultFrequency(string a_strFreQ, int a_ChangeValue)
        {
            if (m_DicPowerTableList.ContainsKey(a_strFreQ))
            {
                LaserPwrTableData pTemp = m_DicPowerTableList[a_strFreQ];
                pTemp.iDefaultFrequency = a_ChangeValue;
                m_DicPowerTableList.Remove(a_strFreQ);
                m_DicPowerTableList.Add(pTemp.iDefaultFrequency.ToString(), pTemp);
            }
            return false;
        }
        public bool IsExistFrequencyTable(int a_iFrequency)
        {
            if (m_DicPowerTableList.ContainsKey(a_iFrequency.ToString()))
            {
                return true;
            }
            return false;
        }
        public void AddPwrTable(LaserPwrTableData a_Value)
        {
            if (m_DicPowerTableList.ContainsKey(a_Value.iDefaultFrequency.ToString()))
            {
                m_DicPowerTableList.Remove(a_Value.iDefaultFrequency.ToString());
                m_DicPowerTableList.Add(a_Value.iDefaultFrequency.ToString(), a_Value.Clone() as LaserPwrTableData);
            }
            else
            {
                m_DicPowerTableList.Add(a_Value.iDefaultFrequency.ToString(), a_Value.Clone() as LaserPwrTableData);
            }

        }
        public bool IsDataSerializePercent_PwrTableData(string a_strFreQ)
        {
            if (m_DicPowerTableList.ContainsKey(a_strFreQ))
            {
                LaserPwrTableData pTemp = m_DicPowerTableList[a_strFreQ];
                float fCurrent = 0.0f;
                float fNext = 0.0f;
                for (int i = 0; i < pTemp.TableItemList.Count - 1; i++)
                {
                    fCurrent = (float)pTemp.TableItemList[i].PercentValue;
                    fNext = (float)pTemp.TableItemList[i + 1].PercentValue;
                    if (fCurrent >= fNext)
                        return false;
                }
                return true;
            }
            return false;
        }
        public bool IsDataSerializePower_PwrTableData(string a_strFreQ)
        {
            if (m_DicPowerTableList.ContainsKey(a_strFreQ))
            {
                LaserPwrTableData pTemp = m_DicPowerTableList[a_strFreQ];
                float fCurrent = 0.0f;
                float fNext = 0.0f;
                for (int i = 0; i < pTemp.TableItemList.Count - 1; i++)
                {
                    fCurrent = (float)pTemp.TableItemList[i].PowerValue;
                    fNext = (float)pTemp.TableItemList[i + 1].PowerValue;
                    if (fCurrent > fNext)
                        return false;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Item"></param>
        public void AddItem(EzIna.Laser.LaserInterface a_Item)
        {

            m_DeviceList.Add(a_Item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Index"></param>
        /// <returns></returns>
        public EzIna.Laser.LaserInterface GetItem(ushort a_Index)
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
        public EzIna.Laser.LaserInterface this[ushort Index]
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
            foreach (EzIna.Laser.LaserInterface item in m_DeviceList)
            {
                pNode = null;
                pChild = null;
                switch (item.DeviceType.Name)
                {
                    case "Talon355":
                        {
                            if (strBrandList.Contains("Talon") == false)
                            {
                                strBrandList.Add("Talon");
                                a_pTreeView.Nodes.Add("Talon355", "Talon");
                            }
                            pNode = a_pTreeView.Nodes["Talon355"];
                            goto Create;
                        }
                        break;

                    case "PicoBlade2":
                        {
                            if (strBrandList.Contains("Lumentum") == false)
                            {
                                strBrandList.Add("Lumentum");
                                a_pTreeView.Nodes.Add("PicoBlade2", ("Lumentum"));
                            }
                            pNode = a_pTreeView.Nodes["PicoBlade2"];
                            goto Create;
                        }
                        break;
                    case "GLPM":
                    case "GLPM_V8":
                        {
                            if (strBrandList.Contains("IPG") == false)
                            {
                                strBrandList.Add("IPG");
                                a_pTreeView.Nodes.Add("GLPM", ("IPG"));
                            }
                            pNode = a_pTreeView.Nodes["GLPM"];
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
        public void TreeView_InitPowerTable(TreeView a_pTreeView)
        {

            a_pTreeView.Nodes.Clear();

            if (m_DicPowerTableList.Count <= 0) return;
            a_pTreeView.BeginUpdate();
            a_pTreeView.Nodes.Clear();

            a_pTreeView.Font = new Font("Century Gothic", 12F, FontStyle.Regular);
            TreeNode pCategory = new TreeNode("Laser Power Table");
            pCategory.Name = "Laser Power Table";
            pCategory.NodeFont = new Font("Century Gothic", 12F, FontStyle.Regular);
            TreeNode pNode = null;
            TreeNode pChild = null;
            int idx = 0;
            foreach (KeyValuePair<string, LaserPwrTableData> item in m_DicPowerTableList)
            {

                pCategory.Nodes.Add(
                                            new TreeNode(item.Key)
                                            {
                                                Name = item.Key,
                                                NodeFont = new Font("Century Gothic", 11F, FontStyle.Regular)
                                            }
                                            );
                //Main Category - Sub Category
                //for example Vision - Matcher
                //			  Motion - Stage, Head, 						

            }//end foreach
            a_pTreeView.Nodes.Add(pCategory);
            a_pTreeView.Nodes[pCategory.Name].Expand();
            a_pTreeView.EndUpdate();

        }
        public void DGV_InitPowerTable(DataGridView a_DGV)
        {
            InitializeDataGridVeiwStyle(a_DGV);
            a_DGV.Columns.AddRange(
                                            CreateDataGridViewTextColumn("Percent", "PercentValue", "0.000%", a_DGV.Width / 2),
                                            CreateDataGridViewTextColumn("Power", "PowerValue", "0.000W", a_DGV.Width / 2)
                                            );
            a_DGV.CellClick += this.DGV_PowerTableCellClick;
        }
        #region DGV Style
        private void InitializeDataGridVeiwStyle(DataGridView a_DatagirdView)
        {

            a_DatagirdView.DataSource = null;
            if (a_DatagirdView.RowCount > 0 || a_DatagirdView.ColumnCount > 0)
            {
                a_DatagirdView.Columns.Clear();
                a_DatagirdView.Rows.Clear();
            }
            a_DatagirdView.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.BackgroundColor = Color.White;
            a_DatagirdView.ForeColor = Color.Black;
            a_DatagirdView.ReadOnly = false;
            a_DatagirdView.AllowUserToAddRows = false;
            a_DatagirdView.AllowUserToDeleteRows = false;
            a_DatagirdView.AllowUserToOrderColumns = false;
            a_DatagirdView.AllowUserToResizeColumns = false;
            a_DatagirdView.AllowUserToResizeRows = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.RowHeadersVisible = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.MultiSelect = false;
            a_DatagirdView.EditMode = DataGridViewEditMode.EditOnEnter;
            a_DatagirdView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            a_DatagirdView.AutoGenerateColumns = false;
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            a_DatagirdView.ColumnHeadersHeight = 25;
            a_DatagirdView.RowTemplate.Height = 25;

            a_DatagirdView.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            a_DatagirdView.BackgroundColor = Color.White;
            a_DatagirdView.ForeColor = Color.Black;
            a_DatagirdView.EnableHeadersVisualStyles = false;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            a_DatagirdView.ScrollBars = ScrollBars.Vertical;
            a_DatagirdView.ClearSelection();
        }
        private DataGridViewTextBoxColumn CreateDataGridViewTextColumn(string a_strHeaderTxt, string a_strBindingPropTxt, string a_strFormat, int a_Width)
        {
            DataGridViewTextBoxColumn pRet = new DataGridViewTextBoxColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name = a_strHeaderTxt;
            pRet.DividerWidth = 1;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.Width = a_Width;
            pRet.DefaultCellStyle.Format = a_strFormat;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pRet.DefaultCellStyle.ForeColor = Color.Black;
            pRet.DefaultCellStyle.BackColor = Color.White;
            pRet.DefaultCellStyle.SelectionBackColor = m_DGV_SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = m_DGV_SelectionForeColor;
            pRet.SortMode = DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        private DataGridViewButtonColumn CreateDataGridViewButtonColum(string a_strHeaderTxt, string a_strBindingPropTxt, string a_ButtonText, int a_Width = 100)
        {
            DataGridViewButtonColumn pRet = new DataGridViewButtonColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name = a_strHeaderTxt;
            pRet.DividerWidth = 1;
            pRet.Text = a_ButtonText;
            pRet.UseColumnTextForButtonValue = true;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.Width = a_Width;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pRet.DefaultCellStyle.ForeColor = Color.Black;
            pRet.DefaultCellStyle.BackColor = Color.White;
            pRet.SortMode = DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }

        private void DGV_PowerTableCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            DataGridView pControl = sender as DataGridView;
            if (pControl != null)
            {
                BindingList<LaserPowerTableItem> pDataBindingSource = pControl.DataSource as BindingList<LaserPowerTableItem>;
                double fMin = 0.0;
                double fMax = 0.0;
                if (pDataBindingSource != null && pDataBindingSource.Count > 0)
                {
                    GUI.UserControls.NumberKeypad pNumberKeyPad = new GUI.UserControls.NumberKeypad();
                    if (pControl.Columns["Percent"].Index == e.ColumnIndex)
                    {
                        /*if (e.RowIndex == 0 && pDataBindingSource.Count>1)
                        {
                            fMin=0.0;
                            fMax=(pDataBindingSource[e.RowIndex+1].PercentValue-0.001)*100;																
                        }
                        else if (e.RowIndex == pDataBindingSource.Count - 1)
                        {
                                fMin = (pDataBindingSource[e.RowIndex-1].PercentValue+0.001)*100.0;
                                fMax = 1.0*100;														
                        }
                        else
                        {
                                fMin =	(pDataBindingSource[e.RowIndex-1].PercentValue + 0.001) * 100.0;
                                fMax=		(pDataBindingSource[e.RowIndex+1].PercentValue-0.001)*100;																
                        }	*/
                        if (pNumberKeyPad.ShowDialog(0, 100, pDataBindingSource[e.RowIndex].PercentValuePer100) == DialogResult.OK)
                        {
                            pDataBindingSource[e.RowIndex].PercentValue = pNumberKeyPad.Result / 100.0;
                        }
                    }
                    else if (pControl.Columns["Power"].Index == e.ColumnIndex)
                    {
                        /*(if (e.RowIndex == 0 && pDataBindingSource.Count > 1)
                        {
                                fMin = 0.0;
                                fMax = (pDataBindingSource[e.RowIndex + 1].PowerValue - 0.0001);
                        }
                        else if (e.RowIndex == pDataBindingSource.Count - 1)
                        {
                                fMin = (pDataBindingSource[e.RowIndex - 1].PowerValue + 0.0001);
                                fMax = 1000000;
                        }
                        else
                        {
                                fMin = (pDataBindingSource[e.RowIndex - 1].PowerValue + 0.0001);
                                fMax = (pDataBindingSource[e.RowIndex + 1].PowerValue - 0.0001);
                        }*/
                        if (pNumberKeyPad.ShowDialog(0, 1000000, pDataBindingSource[e.RowIndex].PowerValue) == DialogResult.OK)
                        {
                            pDataBindingSource[e.RowIndex].PowerValue = pNumberKeyPad.Result;
                        }
                    }
                }
            }
        }
        #endregion DGV Style


    }
}
