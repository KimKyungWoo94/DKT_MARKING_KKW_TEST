using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.IO
{
    public sealed partial class IOManager
    {
        public void InitializeCylinderDataGridView(EzIna.GUI.UserControls.ExpandDataGridView a_DatagirdView)
        {
            this.InitializeDataGridViewDefalutSet(a_DatagirdView);
            this.InitializeDataGridViewDefalutSet(a_DatagirdView.ChildDataGridView);

            a_DatagirdView.RowHeadersVisible=true;
            a_DatagirdView.ChildDataGridView.RowHeadersVisible=true;
            a_DatagirdView.ChildDataGridView.RowHeadersWidth=100;
            a_DatagirdView.DataSource=new BindingList<EzIna.IO.Cylinder>(this.CylinderList);
            
            DataGridViewTextBoxColumn ID = CreateDataGridViewTextColumn("ID", "strID",170);
            DataGridViewTextBoxColumn Type = CreateDataGridViewTextColumn("Type", "OperType",65);          
            DataGridViewTextBoxColumn Status = CreateDataGridViewTextColumn("Status", "",90);
            DataGridViewTextBoxColumn SolDelay = CreateDataGridViewTextColumn("SolCheckDelay", "SolCheckDelay",125);            
            SolDelay.DefaultCellStyle.Format="0 ms";
            EzIna.GUI.UserControls.DGVToggleColumn FSensorCheck = CreateDataGridViewToggleButton("F.SensorCheck", "bForwardSersorCheck", 125);
            EzIna.GUI.UserControls.DGVToggleColumn BSensorCheck = CreateDataGridViewToggleButton("B.SensorCheck", "bBackwardSersorCheck", 125);
            DataGridViewButtonColumn ForwardAction =CreateDataGridViewButtonColum("Forward", "","Action",75);            
            DataGridViewButtonColumn BackwardAction =CreateDataGridViewButtonColum("Backward"," ","Action",88);        
            DataGridViewTextBoxColumn RepeatCount = CreateDataGridViewTextColumn("RepeatCount", "RepeatCount",125);              
            DataGridViewTextBoxColumn ExecuteRepeatCount =CreateDataGridViewTextColumn("ExecuteCount", "",125); 
            EzIna.GUI.UserControls.DGVToggleColumn RepeatStatus =CreateDataGridViewToggleButton("RepeatStatus", "",125);                      
                                 
            a_DatagirdView.Columns.AddRange(ID,Type,Status,SolDelay,FSensorCheck,BSensorCheck,ForwardAction,BackwardAction,RepeatCount,ExecuteRepeatCount,RepeatStatus);
        }

        public void CreateColumnCylinderIODataGridView(DataGridView a_DatagirdView)
        {                                                            
            DataGridViewTextBoxColumn IDX = CreateDataGridViewTextColumn("IDX","LoadingOrder",60);
            DataGridViewTextBoxColumn ID = CreateDataGridViewTextColumn("ID","ID" ,250);
            DataGridViewTextBoxColumn Desrc = CreateDataGridViewTextColumn("Desrc", "Description",300);
            DataGridViewTextBoxColumn Contact = CreateDataGridViewTextColumn("EC","EContact" ,35);
            DataGridViewTextBoxColumn Addr0 = CreateDataGridViewTextColumn("Addr0","" ,60);
            DataGridViewTextBoxColumn Addr1 = CreateDataGridViewTextColumn("Addr1","" ,60);
            DataGridViewTextBoxColumn Addr2 = CreateDataGridViewTextColumn("Addr2","" ,60);
            DataGridViewTextBoxColumn Addr3 = CreateDataGridViewTextColumn("Addr3","" ,60);
            DataGridViewTextBoxColumn Addr4 = CreateDataGridViewTextColumn("Addr4","" ,60);
            EzIna.GUI.UserControls.DGVToggleColumn Value = CreateDataGridViewToggleButton("Status","",85);
            a_DatagirdView.Columns.AddRange(IDX,ID,Desrc,Contact, Addr0, Addr1, Addr2, Addr3, Addr4,Value);                 
        }
        public void UpdateCylinderInfor(DataGridView a_DatagirdView)
        {
            if (a_DatagirdView != null)
            {

                if (a_DatagirdView.Rows.Count == this.CylinderList.Count)
                {
                    List<Cylinder> plist=this.CylinderList;
                    foreach (DataGridViewRow Item in a_DatagirdView.Rows)
                    {
                        Item.Cells["Status"].Value = plist[Item.Index].CurrentState;
                        Item.Cells["RepeatStatus"].Value = plist[Item.Index].IsRepeatActionExcute;
                        Item.Cells["ExecuteCount"].Value = plist[Item.Index].ExecuteRepeatCount;
                    }
                }
            }
        }       
    }
}
