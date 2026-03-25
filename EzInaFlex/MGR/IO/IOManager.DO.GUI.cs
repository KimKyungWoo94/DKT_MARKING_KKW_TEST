using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.IO
{
    public sealed partial class IOManager
    {
        public void InitializeDODataGridView(DataGridView a_DatagirdView)
        {
            InitializeDataGridViewDefalutSet(a_DatagirdView);
            a_DatagirdView.DataSource = new BindingList<EzIna.IO.DO>(this.DOList);       
            DataGridViewTextBoxColumn IDX = CreateDataGridViewTextColumn("IDX","LoadingOrder",60);     
            DataGridViewTextBoxColumn ID = CreateDataGridViewTextColumn("ID", "ID",280);  
            DataGridViewTextBoxColumn Desrc = CreateDataGridViewTextColumn("Desrc", "Description",300);                                    
            DataGridViewTextBoxColumn Addr0 = CreateDataGridViewTextColumn("Addr0", "AddressInfo.m_strParamList[0]",65);
            DataGridViewTextBoxColumn Addr1 = CreateDataGridViewTextColumn("Addr1", "AddressInfo.m_strParamList[1]",65);
            DataGridViewTextBoxColumn Addr2 = CreateDataGridViewTextColumn("Addr2", "AddressInfo.m_strParamList[2]",60);
            DataGridViewTextBoxColumn Addr3 = CreateDataGridViewTextColumn("Addr3", "AddressInfo.m_strParamList[3]",60);
            DataGridViewTextBoxColumn Addr4 = CreateDataGridViewTextColumn("Addr4", "AddressInfo.m_strParamList[4]",60);
            EzIna.GUI.UserControls.DGVToggleColumn Status = CreateDataGridViewToggleButton("Status", "Value",100);            
            a_DatagirdView.Columns.AddRange(IDX,ID,Desrc, Addr0, Addr1, Addr2, Addr3, Addr4,Status);                
        }
        /// <summary>
        /// Link Event DataGridView - DataBindingComplate
        /// </summary>
        /// <param name="a_DatagirdView"></param>
        /// 
        public void UpdateDataGrid_DOColumn(DataGridView a_DatagirdView)
        {
            if (a_DatagirdView != null)
            {
                foreach (DataGridViewRow Item in a_DatagirdView.Rows)
                {
                    Item.Cells["Addr0"].Value = this.DOList[Item.Index].AddressInfo.m_strParamList[0];
                    Item.Cells["Addr1"].Value = this.DOList[Item.Index].AddressInfo.m_strParamList[1];
                    Item.Cells["Addr2"].Value = this.DOList[Item.Index].AddressInfo.m_strParamList[2];
                    Item.Cells["Addr3"].Value = this.DOList[Item.Index].AddressInfo.m_strParamList[3];
                    Item.Cells["Addr4"].Value = this.DOList[Item.Index].AddressInfo.m_strParamList[4];
                }
            }
        }
        /// <summary>
        /// Update Timer in Form
        /// </summary>
        /// <param name="a_DatagirdView"></param>
        public void UpdateDataGrid_DOValue(DataGridView a_DatagirdView)
        {
            if (a_DatagirdView != null)
            {
                if (a_DatagirdView.Rows.Count == this.DIList.Count)
                {
                    foreach (DataGridViewRow Item in a_DatagirdView.Rows)
                    {
                        Item.Cells["Status"].Value = this.DOList[Item.Index].Value;
                    }
                }
            }
        }
    }
}
