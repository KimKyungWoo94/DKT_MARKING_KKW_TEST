using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.IO
{
    public sealed partial class IOManager:EzIna.SingleTone<IOManager>,IDisposable
    {       
        ~IOManager()
        {
            Dispose(false);
        }
        protected override void OnCreateInstance()
        {
            base.OnCreateInstance();
            // constructor
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool a_Disposing)
        {
            if (this.m_IsDisposed)
                return;
            if (a_Disposing)
            {
                m_IsDisposing=true;
                foreach(KeyValuePair<string,Cylinder> Item  in m_DicCylinderList)
                {
                    if( Item.Value.IsRepeatActionExcute)
                      Item.Value.StopRepeatAction();
                }       
                foreach(KeyValuePair<Type,DeviceModule>Item in m_DicDevices )
                {
                    Item.Value.TerminateDevice();
                }

                // Free any other managed objects here.
            }               
            m_IsDisposing= false;
            m_IsDisposed = true;
        }

        private bool m_IsDisposed=false;
        private bool m_IsDisposing=false;
     
        Dictionary<Type, DeviceModule> m_DicDevices = new Dictionary<Type, DeviceModule>();
        Dictionary<string, BaseIO> m_DicIOList = new Dictionary<string, BaseIO>();
        Dictionary<string, Cylinder> m_DicCylinderList=new Dictionary<string, Cylinder>();
        Dictionary<string, AI> m_DicAIList = new Dictionary<string, AI>();
        Dictionary<string, AO> m_DicAOList = new Dictionary<string, AO>();
        Dictionary<string, DI> m_DicDIList = new Dictionary<string, DI>();
        Dictionary<string, DO> m_DicDOList = new Dictionary<string, DO>();
        public List<DI> DIList
        {
            get { return m_DicDIList.Values.ToList(); }
        }
        public List<DO> DOList
        {
            get { return m_DicDOList.Values.ToList();}
        }
        public List<AI> AIList
        {
            get { return m_DicAIList.Values.ToList(); }
        }
        public List<AO> AOList
        {
            get { return m_DicAOList.Values.ToList(); }
        }
        public List<Cylinder> CylinderList
        {
            get { return m_DicCylinderList.Values.ToList();}
        }
        private DeviceModule GetDevice(string a_strTypeName)
        {
            Type pDevice = GetDeiveType(a_strTypeName);
            if (pDevice != null)
            {
                if (m_DicDevices.ContainsKey(pDevice))
                {
                    return m_DicDevices[pDevice];
                }
                else
                {
                    if (typeof(DeviceModule).IsAssignableFrom(pDevice))
                    {
                        DeviceModule pTemp = Activator.CreateInstance(pDevice, true) as DeviceModule;
                        pTemp.InitializeDevice();
                        m_DicDevices.Add(pDevice, pTemp);
                    }
                    if (m_DicDevices.ContainsKey(pDevice))
                    {
                        return m_DicDevices[pDevice];
                    }
                }
            }
            return null;
        }
        public void LoadIO()
        {
            try
            {
                IOItemLinker pItem = null;
                FileInfo XmlFileInfo = new FileInfo(EzIna.IO.FolderPath.ConfigIOItemDataFileFullname);
                if (XmlFileInfo.Exists == true)
                {
                    pItem = IOItemLinker.Load(XmlFileInfo.FullName);
                }
                else
                {
                    pItem = new IOItemLinker();
                    pItem.Save(XmlFileInfo.FullName);
                }
                DeviceModule pDevice = null;
                foreach (IOData IOItem in pItem.IOList)
                {
                    if (IOItem != null)
                    {
                        pDevice = GetDevice(IOItem.DeviceType);
                        if (pDevice != null)
                        {
                            CreateIO(pDevice, IOItem);
                        }
                    }
                }
                int LoopIDX = 0;
                DI DIForwardSensorTemp;
                DI DIBackwardSensorTemp;
                DO DOForwardSolTemp;
                DO DOBackwardSolTemp;
                foreach (CylinderIOData CylinderItem in pItem.CylinderList)
                {
                    if (CylinderItem != null)
                    {
                        LoopIDX = 0;
                        DIForwardSensorTemp = GetDI(CylinderItem.strForwardSensorID);
                        DIBackwardSensorTemp = GetDI(CylinderItem.strBackwardSensorID);
                        DOForwardSolTemp = GetDO(CylinderItem.strForwardSolID);
                        DOBackwardSolTemp = GetDO(CylinderItem.strBackwardSolID);

                        if (m_DicCylinderList.ContainsKey(CylinderItem.strID) == false)
                        {
                            m_DicCylinderList.Add(CylinderItem.strID,
                                                  new Cylinder(DIForwardSensorTemp,
                                                               DIBackwardSensorTemp,
                                                               DOForwardSolTemp,
                                                               DOBackwardSolTemp,
                                                               CylinderItem.strID,
                                                               CylinderItem.strDesrc,
                                                               CylinderItem.IsForwardSensorCheck,
                                                               CylinderItem.IsBackwardSensorCheck,
                                                               CylinderItem.SolCheckDelay,
                                                               CylinderItem.OperationType
                                                               )
                                                   );
                        }
                    }
                }
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message,Ex);
            }           
        }

        public void SaveIO()
        {
            IOItemLinker pItem=new IOItemLinker();
            pItem.IOList.AddRange(m_DicIOList.Select(x=>x.Value.CreateIOData()).ToList());
            pItem.CylinderList.AddRange(m_DicCylinderList.Select(x=>x.Value.CreateIOData()).ToList());
            pItem.Save(EzIna.IO.FolderPath.ConfigIOItemDataFileFullname);

        }      
        private BaseIO CreateIO(DeviceModule a_pDevice, IOData a_Data)
        {
            BaseIO pIORet = null;
            if (m_DicIOList.ContainsKey(a_Data.ID) == false)
            {

                switch (a_Data.enumIOType)
                {
                    case IOType.DI:
                        {
                            DI temp = new DI(a_Data, a_pDevice.ClassType, a_pDevice.CreateAddressInfo(a_Data.strAddrInfoParams));
                            temp.SettingGetDIFunc(a_pDevice.GetDI);
                            temp.LoadingOrder=m_DicDIList.Count+1;
                            m_DicDIList.Add(a_Data.ID, temp);
                            m_DicIOList.Add(a_Data.ID, temp);
                            pIORet = temp;
                        }
                        break;
                    case IOType.DO:
                        {

                            DO temp = new DO(a_Data, a_pDevice.ClassType, a_pDevice.CreateAddressInfo(a_Data.strAddrInfoParams));
                            temp.SettingGetDOFunc(a_pDevice.GetDO);
                            temp.SettingSetDOFunc(a_pDevice.SetDO);
                            temp.LoadingOrder=m_DicDOList.Count+1;
                            m_DicDOList.Add(a_Data.ID, temp);
                            m_DicIOList.Add(a_Data.ID, temp);
                            pIORet = temp;
                        }
                        break;
                    case IOType.AI:
                        {
                            AI temp = new AI(a_Data, a_pDevice.ClassType, a_pDevice.CreateAddressInfo(a_Data.strAddrInfoParams));
                            temp.SettingGetAIFunc(a_pDevice.GetAI);
                            temp.SettingSetRangeAIFunc(a_pDevice.SetAIRange);
                            temp.LoadingOrder=m_DicAIList.Count+1;
                            m_DicAIList.Add(a_Data.ID, temp);
                            m_DicIOList.Add(a_Data.ID, temp);
                            pIORet = temp;
                        }
                        break;
                    case IOType.AO:
                        {
                            AO temp = new AO(a_Data, a_pDevice.ClassType, a_pDevice.CreateAddressInfo(a_Data.strAddrInfoParams));
                            temp.SettingGetAOFunc(a_pDevice.GetAO);
                            temp.SettingSetAOFunc(a_pDevice.SetAO);
                            temp.SettingSetRangeAOFunc(a_pDevice.SetAORange);
                            temp.LoadingOrder=m_DicAOList.Count+1;
                            m_DicAOList.Add(a_Data.ID, temp);
                            m_DicIOList.Add(a_Data.ID, temp);
                            pIORet = temp;
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }
            else
            {
                pIORet = m_DicIOList[a_Data.ID];
            }
            return pIORet;
        }
        private BaseIO GetIO(string a_strKey)
        {
            if(m_DicIOList.ContainsKey(a_strKey))
                return m_DicIOList[a_strKey];

            return null;
        }
        private DI GetDI(string a_strKey)
        {
            if (m_DicDIList.ContainsKey(a_strKey))
                return m_DicDIList[a_strKey];

            return null;
        }
        private DO GetDO(string a_strKey)
        {
            if (m_DicDOList.ContainsKey(a_strKey))
                return m_DicDOList[a_strKey];

            return null;
        }
        private AI GetAI(string a_strKey)
        {
            if (m_DicAIList.ContainsKey(a_strKey))
                return m_DicAIList[a_strKey];

            return null;
        }
        private AO GetAO(string a_strKey)
        {
            if (m_DicAOList.ContainsKey(a_strKey))
                return m_DicAOList[a_strKey];

            return null;
        }
        private Type GetDeiveType(string typeName)
        {
            Type RetType = null;
            switch (typeName)
            {
                case "EzIna.IO.AXT.BoardTypeAIO":
                    {
                        RetType = typeof(EzIna.IO.AXT.BoardTypeAIO);
                    }
                    break;
                case "EzIna.IO.AXT.BoardTypeDIO":
                    {
                        RetType = typeof(EzIna.IO.AXT.BoardTypeDIO);
                    }
                    break;
                case "EzIna.IO.AXT.MotionAXT_DIO":
                    {
                        RetType = typeof(EzIna.IO.AXT.MotionAXT_DIO);
                    }
                    break;
                case "EzIna.IO.AEROTECH.MotionA3200_IO":
                    {
                        RetType = typeof(EzIna.IO.AEROTECH.MotionA3200_IO);
                    }
                    break;
                case "EzIna.IO.ScanLAB.ScanlabRTC5_DIO":
                    {
                        RetType=typeof(EzIna.IO.ScanLAB.ScanlabRTC5_DIO);
                    }
                    break;
                default:
                    break;
            }
            return RetType;
        }
        public void ReadAllIO()
        {
#if SIM
            return;
#else

            foreach (KeyValuePair<Type,DeviceModule> Device in m_DicDevices)
            {
                Device.Value.ReadAllAI();
                Device.Value.ReadAllAO();
                Device.Value.ReadAllDI();
                Device.Value.ReadAllDO();
            }
#endif
        }
        public void ReadAllDIO()
        {
#if SIM
            return;
#else
            foreach (KeyValuePair<Type,DeviceModule> Device in m_DicDevices)
            {              
                Device.Value.ReadAllDI();
                Device.Value.ReadAllDO();
            }
#endif
        }
        public void ReadAllAIO()
        {
#if SIM
            return;
#else
            foreach (KeyValuePair<Type,DeviceModule> Device in m_DicDevices)
            {              
                Device.Value.ReadAllAI();
                Device.Value.ReadAllAO();
            }
#endif
        }
        public void WriteAllDO()
        {
#if SIM
            return;
#else
            foreach (KeyValuePair<Type,DeviceModule> Device in m_DicDevices)
            {
                Device.Value.WriteAllDO();
            }
#endif
        }
        public void Execute(object obj)
        {
#if SIM
            return;
#else

            foreach(KeyValuePair<Type,DeviceModule> Device in m_DicDevices)
            {
                Device.Value.ReadAllAI();
                Device.Value.ReadAllAO();
                Device.Value.ReadAllDI();                
                Device.Value.WriteAllDO();
            }
#endif
        }


        public void InitializeDataGridViewDefalutSet(DataGridView a_DatagirdView)
        {
            if (a_DatagirdView.RowCount > 0 || a_DatagirdView.ColumnCount > 0)
            {
                a_DatagirdView.Columns.Clear();
                a_DatagirdView.Rows.Clear();
            }
            a_DatagirdView.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.ReadOnly = false;
            a_DatagirdView.AllowUserToAddRows = false;
            a_DatagirdView.AllowUserToDeleteRows = false;
            a_DatagirdView.AllowUserToOrderColumns = false;
            a_DatagirdView.AllowUserToResizeColumns = false;
            a_DatagirdView.AllowUserToResizeRows = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.RowHeadersVisible = false;
            a_DatagirdView.MultiSelect = false;
            a_DatagirdView.EditMode = DataGridViewEditMode.EditOnEnter;
            a_DatagirdView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            a_DatagirdView.AutoGenerateColumns = false;
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            a_DatagirdView.ColumnHeadersHeight = 30;
            a_DatagirdView.RowTemplate.Height = 30;
            
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            a_DatagirdView.EnableHeadersVisualStyles = false;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            a_DatagirdView.ClearSelection();
        }
        private DataGridViewTextBoxColumn CreateDataGridViewTextColumn(string a_strHeaderTxt, string a_strBindingPropTxt,int a_Width=100)
        {
            DataGridViewTextBoxColumn pRet = new DataGridViewTextBoxColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name=a_strHeaderTxt;
            pRet.DividerWidth=1;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;            
            pRet.Width=a_Width;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;            
            pRet.DefaultCellStyle.ForeColor = ForeColor;
            pRet.DefaultCellStyle.BackColor = BackColor;            
            pRet.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
            pRet.SortMode=DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        private DataGridViewButtonColumn CreateDataGridViewButtonColum(string a_strHeaderTxt, string a_strBindingPropTxt,string a_ButtonText,int a_Width=100)
        {
            DataGridViewButtonColumn pRet = new DataGridViewButtonColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name = a_strHeaderTxt;
            pRet.DividerWidth = 1;
            pRet.Text=a_ButtonText;
            pRet.UseColumnTextForButtonValue=true;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = true;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.Width = a_Width;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pRet.DefaultCellStyle.ForeColor = ForeColor;
            pRet.DefaultCellStyle.BackColor = BackColor;
            pRet.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
            pRet.SortMode=DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        private EzIna.GUI.UserControls.DGVToggleColumn CreateDataGridViewToggleButton(string a_strHeaderTxt, string a_strBindingPropTxt,int a_Width,bool a_bReadOnly=true)
        {
            
            EzIna.GUI.UserControls.DGVToggleColumn pRet = new EzIna.GUI.UserControls.DGVToggleColumn();
            pRet.HeaderText = a_strHeaderTxt;
            pRet.Name=a_strHeaderTxt;
            pRet.Width = a_Width;
            pRet.DividerWidth=1;
            pRet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pRet.Resizable = DataGridViewTriState.False;
            pRet.ReadOnly = a_bReadOnly;
            pRet.DataPropertyName = a_strBindingPropTxt;
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pRet.DefaultCellStyle.ForeColor = ForeColor;
            pRet.DefaultCellStyle.BackColor = BackColor;
            pRet.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
            pRet.SortMode=DataGridViewColumnSortMode.NotSortable;
            //TestTooggle.ButtonStyle = GUI.Usercontrol.DGVToggleButtonCell.ToggleSwitchStyle.IOS5;
            //GUI.Usercontrol.DGVToggleButtonIOS5Render customizedIos5Renderer = new GUI.Usercontrol.DGVToggleButtonIOS5Render();
            //customizedIos5Renderer.LeftSideUpperColor1 =SystemColors.Highlight ;
            //customizedIos5Renderer.LeftSideUpperColor2 = SystemColors.Highlight;
            //customizedIos5Renderer.LeftSideLowerColor1 = SystemColors.Highlight;
            //customizedIos5Renderer.LeftSideLowerColor2 = SystemColors.Highlight;
            //customizedIos5Renderer.RightSideUpperColor1 = Color.Silver;
            //customizedIos5Renderer.RightSideUpperColor2 = Color.Silver;
            //customizedIos5Renderer.RightSideLowerColor1 = Color.Silver;
            //customizedIos5Renderer.RightSideLowerColor2 = Color.Silver;
            //customizedIos5Renderer.ButtonNormalOuterBorderColor = Color.FromArgb(255,0,122,204);
            //customizedIos5Renderer.ButtonNormalInnerBorderColor = Color.FromArgb(255,0,122,204);
            //customizedIos5Renderer.ButtonNormalSurfaceColor1 = Color.White;
            //customizedIos5Renderer.ButtonNormalSurfaceColor2 = Color.White;
            //customizedIos5Renderer.ButtonHoverOuterBorderColor = Color.FromArgb(255,0,122,204);
            //customizedIos5Renderer.ButtonHoverInnerBorderColor = Color.FromArgb(255,0,122,204);
            //customizedIos5Renderer.ButtonHoverSurfaceColor1 = Color.White;
            //customizedIos5Renderer.ButtonHoverSurfaceColor2 = Color.White;
            //customizedIos5Renderer.ButtonPressedOuterBorderColor =Color.FromArgb(255,0,122,204);
            //customizedIos5Renderer.ButtonPressedInnerBorderColor =Color.FromArgb(255,0,122,204);
            //customizedIos5Renderer.ButtonPressedSurfaceColor1 = Color.White;
            //customizedIos5Renderer.ButtonPressedSurfaceColor2 = Color.White;
            //TestTooggle.SetToggleButtonRender(customizedIos5Renderer);
            return pRet;
        }      
    }
}
