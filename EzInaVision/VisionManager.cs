using EzInaVision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using EzInaVision.GDV;

namespace EzInaVision
{
    public partial class VisionManager
    {
        //
        public bool bIntialized
        {
            get
            {
                foreach (VisionCamBaseClass cam in m_vecCameras)
                {
                    if (cam.IsConnected() == false)
                        return false;
                }

                foreach (VisionLibBaseClass lib in m_vecLibraries)
                {
                    if (lib.IsInitialized() == false)
                        return false;
                }

                return true;

            }
        }
        public bool bLIBInitialized
        {
            get
            {
                foreach (VisionLibBaseClass lib in m_vecLibraries)
                {
                    if (lib.IsInitialized() == false)
                        return false;
                }

                return true;
            }
        }
        private List<VisionCamBaseClass> m_vecCameras;
        private List<VisionLibBaseClass> m_vecLibraries;

        public List<VisionCamBaseClass> vecCameras
        {
            get
            {
                return m_vecCameras;
            }
        }
        public List<VisionLibBaseClass> vecLibraries
        {
            get
            {
                return m_vecLibraries;
            }
        }

        public int CameraCount
        {
            get
            {
                if (m_vecCameras == null)
                    return -1;
                return m_vecCameras.Count;
            }
        }
        public int LibraryCount
        {
            get
            {
                if (m_vecLibraries == null)
                    return -1;

                return m_vecLibraries.Count;
            }
        }

        public VisionManager()
        {
            Initialize();
        }

        ~VisionManager()
        {

        }

        public void Initialize()
        {
            m_vecCameras = new List<VisionCamBaseClass>();
            m_vecLibraries = new List<VisionLibBaseClass>();
        }
        public void Terminate()
        {
            DeleteItems();
        }

        public bool AddItem(object a_item)
        {
            bool bRet = true;
            try
            {

                if (a_item != null)
                {
                    switch (a_item.GetType().Name.ToString())
                    {
                        case "VisionLibEuresys":
                            m_vecLibraries.Add((VisionLibBaseClass)a_item);
                            break;
                        case "GrabLinkCam":
                            m_vecCameras.Add((VisionCamBaseClass)a_item);
                            break;
                    }
                    bRet = true;
                }
                else
                {
                    bRet = false;
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return bRet;
        }
        private void DeleteItems()
        {
            if (m_vecCameras != null)
            {
                for (int i = 0; i < m_vecCameras.Count; i++)
                {
                    m_vecCameras[i].Terminate();
                }

                if (m_vecCameras.Count > 0)
                {
                    m_vecCameras.Clear();
                }
            }

            if (m_vecLibraries != null)
            {
                for (int i = 0; i < m_vecLibraries.Count; i++)
                {
                    m_vecLibraries[i].Terminate();
                }

                if (m_vecLibraries.Count > 0)
                {
                    m_vecLibraries.Clear();
                }
            }

            m_vecCameras = null;
            m_vecLibraries = null;
        }
        public VisionCamBaseClass GetCam(string a_strName)
        {
            try
            {
                if (m_vecCameras == null)
                    return null;

                foreach (VisionCamBaseClass item in m_vecCameras)
                {
                    if (item.m_stCamInfo.strCameraName.Equals(a_strName.ToUpper()))
                    {
                        return item;
                    }
                }
            }
            catch (Exception exc)
            {
                return null;
            }
            return null;
        }
        public bool GetCamInfo(int a_iIdx, ref EzInaVision.GDV.stCamInfo a_strCamInfo)
        {
            try
            {
                if (m_vecCameras == null)
                    return false;
                if (m_vecCameras.Count <= a_iIdx)
                    return false;

                a_strCamInfo = m_vecCameras[a_iIdx].m_stCamInfo;

            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }
        public VisionLibBaseClass GetLib(string a_strName)
        {
            try
            {
                if (m_vecLibraries == null)
                    return null;

                foreach (VisionLibBaseClass item in m_vecLibraries)
                {
                    if (item.m_LibInfo.m_stLibInfo.strName.Equals(a_strName.ToUpper()))
                        return item;
                }

            }
            catch (Exception exc)
            {
                return null;
            }
            return null;
        }
        public bool GetLibInfo(int a_iIdx, ref EzInaVision.GDV.stLibInfo a_stLibInfo)
        {
            try
            {
                if (m_vecLibraries == null)
                    return false;
                if (m_vecLibraries.Count <= a_iIdx)
                    return false;

                a_stLibInfo = m_vecLibraries[a_iIdx].m_LibInfo.m_stLibInfo;

            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }

        #region [ Functions ]
        public void Idle()
        {
            foreach (VisionCamBaseClass item in m_vecCameras)
            {
                item.Idle();
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }

        }

        public bool IsLive()
        {
            foreach (VisionCamBaseClass item in m_vecCameras)
            {
                if (item.IsLive())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion [ Functions ]
        #region [ Open ] 
        public void OpenConfigAll(string a_strPath)
        {
            //             foreach(GDV.eVision item in Enum.GetValues(typeof(GDV.eVision)))
            //             {
            //                 if(GetLib(item) == null)
            //                     continue;
            //                 GetLib(item).OpenConfig(a_strPath);
            //             }
        }
        public void ReadData_From(DataGridView grdData, bool readOnly, int begin, int end)
        {
            //ex)
            /*
            int? a = new int();
            a = 10;
            int b = a ?? -1;
            Console.WriteLine(b);  // output: -1
            */

            for (int i = begin; i < end; i++)
            {
                //double.TryParse(grdData[3, i - begin].Value?.ToString(), "0");
                //Datas[i].Unit = grdData[4, i - begin].Value?.ToString() ?? "";
            }

        }
        #endregion [ Open]
        #region [ Tree Initialize ]
        public void TreeView_Init(TreeView a_pTreeView)
        {
            a_pTreeView.Nodes.Clear();

            TreeNode pNode = null;
            pNode = a_pTreeView.Nodes.Add("ALIGNMENT");
            pNode.ImageIndex = 2;


            foreach (EzInaVision.GDV.eGoldenImages item in Enum.GetValues(typeof(GDV.eGoldenImages)))
            {
                if (item == GDV.eGoldenImages.None)
                    continue;
                if (item == GDV.eGoldenImages.Object_No1)
                    break;

                TreeNode pChild = null;
                pChild = pNode.Nodes.Add(item.ToString(), item.ToString());
                pChild.Tag = Convert.ToInt32(item);
                pChild.ImageIndex = 0;
            }

            pNode = a_pTreeView.Nodes.Add("INSPECTION");
            pNode.ImageIndex = 2;
            foreach (GDV.eGoldenImages item in Enum.GetValues(typeof(GDV.eGoldenImages)))
            {
                if (item == GDV.eGoldenImages.None || item == GDV.eGoldenImages.Max)
                    continue;
                if (Convert.ToInt32(item) < Convert.ToInt32(GDV.eGoldenImages.Object_No1))
                    continue;

                TreeNode pChild = null;
                pChild = pNode.Nodes.Add(item.ToString(), item.ToString());
                pChild.Tag = Convert.ToInt32(item);
                pChild.ImageIndex = 0;

            }

        }
        #endregion [ Tree Initialize ]

        #region [ Measurement DataGrid Initialize ]
        public void DataGrid_Init(DataGridView grdData, bool readOnly)
        {
            if (grdData.RowCount == 0)
            {
                grdData.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
                grdData.ReadOnly = readOnly;
                grdData.AllowUserToAddRows = true;
                grdData.AllowUserToDeleteRows = false;
                grdData.AllowUserToOrderColumns = false;
                grdData.AllowUserToResizeColumns = true;
                grdData.AllowUserToResizeRows = false;
                grdData.ColumnHeadersVisible = true;
                grdData.RowHeadersVisible = false;
                grdData.MultiSelect = false;
                grdData.EditMode = DataGridViewEditMode.EditOnEnter;
                grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                grdData.Columns.Clear();
                grdData.Columns.Add("NO    ", "NO");
                grdData.Columns.Add("ADDR X", "ADDR X");
                grdData.Columns.Add("ADDR Y", "ADDR Y");
                grdData.Columns.Add("POS X", "POS X [um]");
                grdData.Columns.Add("POS Y", "POS Y [um]");
                grdData.Columns.Add("WIDTH", "WIDTH [um]");
                grdData.Columns.Add("HEIGHT", "HEIGHT [um]");
                grdData.Columns.Add("MEAS", "MEAS");

                int i = 0;

                for (i = 0; i < grdData.ColumnCount; i++)
                {
                    grdData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //i == 0 ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;
                    grdData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    grdData.Columns[i].ReadOnly = true;
                }

                grdData.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grdData.AutoResizeColumns();
            }
        }
        #endregion [ Measurement DataGrid Initialize ]
    }
}
