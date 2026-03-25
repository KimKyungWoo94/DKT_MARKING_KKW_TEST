using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EzIna
{
    public partial class FrmInforSetupIO_SXGA : Form
    {
		Timer m_Timer = null;
		
		int m_AI_iSelectedIndex = -1;
		int m_AO_iSelectedIndex = -1;

		
		public FrmInforSetupIO_SXGA()
        {
            InitializeComponent();
			m_Timer = new Timer();
			m_Timer.Interval = 50;

			m_Timer.Tick += new EventHandler(this.Display);
		}

		private void FrmInforSetupIO_Load(object sender, EventArgs e)
		{
            EzIna.FA.MGR.IOMgr.InitializeDIDataGridView(DGV_DI);
            EzIna.FA.MGR.IOMgr.InitializeDODataGridView(DGV_DO);
            EzIna.FA.MGR.IOMgr.InitializeAIDataGridView(DGV_AI);
            EzIna.FA.MGR.IOMgr.InitializeAODataGridView(DGV_AO);

            DGV_DI.SelectionChanged += DataGridVeiwNoSelection_SelectionChanged;
            DGV_DO.SelectionChanged += DataGridVeiwNoSelection_SelectionChanged;
            DGV_AI.SelectionChanged += DataGridVeiwNoSelection_SelectionChanged;
            DGV_AO.SelectionChanged +=DataGridVeiwNoSelection_SelectionChanged;
            DGV_DI.DataBindingComplete+=dataGridView_IO_DataBindingComplete;
            DGV_DI.DataBindingComplete+=dataGridView_IO_DataBindingComplete;
            DGV_DO.DataBindingComplete+=dataGridView_IO_DataBindingComplete;
            DGV_AI.DataBindingComplete+=dataGridView_IO_DataBindingComplete;
            DGV_AO.DataBindingComplete+=dataGridView_IO_DataBindingComplete;
            DGV_DI.CellDoubleClick+=dataGridView_DI_CellDoubleClick;
            DGV_DO.CellClick+=dataGridView_DO_CellClick;
            DGV_AI.CellClick+=dataGridView_AI_CellClick;
            DGV_AO.CellClick+=dataGridView_AO_CellClick;
            EzIna.ControlHelper.DoubleBuffered(DGV_DI,true);      
            EzIna.ControlHelper.DoubleBuffered(DGV_DO,true);      
            EzIna.ControlHelper.DoubleBuffered(DGV_AI,true);      
            EzIna.ControlHelper.DoubleBuffered(DGV_AO,true);               
		}
		private void FrmInforSetupIO_VisibleChanged(object sender, EventArgs e)
		{
			m_Timer.Enabled = this.Visible;
			if (Visible)
			{
								DGV_DI.CurrentCell=null;
                DGV_DO.CurrentCell=null;
                DGV_AI.CurrentCell=null;
                DGV_AO.CurrentCell=null;
                DGV_DI.ClearSelection();
                DGV_DO.ClearSelection();
                DGV_AI.ClearSelection();
                DGV_AO.ClearSelection();
			}
		}

		private void Display(object sender, EventArgs e)
		{
		   m_Timer.Enabled=false;
            switch (TabControl_IO.SelectedIndex)
            {
                case 0:
                    {                                            
                       EzIna.FA.MGR.IOMgr.UpdateDataGrid_DIValue(DGV_DI);
                       EzIna.FA.MGR.IOMgr.UpdateDataGrid_DOValue(DGV_DO);                                             
                    }
                    break;
                case 1:
                    {
                       EzIna.FA.MGR.IOMgr.UpdateDataGrid_AIValue(DGV_AI);
                       EzIna.FA.MGR.IOMgr.UpdateDataGrid_AOValue(DGV_AO);                       
                    }
                    break;
            }
            m_Timer.Enabled = this.Visible;
		}
        public void FlickrDisplay()
        {

        }
        private void dataGridView_IO_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {             
            DataGridView pControl = sender as DataGridView;
            switch (pControl.Name)
            {
                case "DGV_DI":
                    {
                        EzIna.FA.MGR.IOMgr.UpdateDataGrid_DIColumn(pControl);
                    }
                    break;
                case "DGV_DO":
                    {
                        EzIna.FA.MGR.IOMgr.UpdateDataGrid_DOColumn(pControl);
                    }
                    break;
                case "DGV_AI":
                    {
                         EzIna.FA.MGR.IOMgr.UpdateDataGrid_AIColumn(pControl);
                    }
                    break;
                case "DGV_AO":
                    {
                        EzIna.FA.MGR.IOMgr.UpdateDataGrid_AOColumn(pControl);
                    }
                    break;
            }
          Trace.WriteLine("IO dataBinding");
        }    
        private void dataGridView_DI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;					
            string ComfirmMsg;
            if(DGV_DI.Columns["EC"].Index==e.ColumnIndex)
            {
                 ComfirmMsg=string.Format("Would like Chage EContact [{0} -> {1}]?\nID:{2}",
                      EzIna.FA.MGR.IOMgr.DIList[e.RowIndex].EContact,
                      EzIna.FA.MGR.IOMgr.DIList[e.RowIndex].EContact==IO.EContact.A?IO.EContact.B : IO.EContact.A,
                      DGV_DI.Rows[e.RowIndex].Cells["ID"].Value);
                 if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                 {
                     EzIna.FA.MGR.IOMgr.DIList[e.RowIndex].EContact=EzIna.FA.MGR.IOMgr.DIList[e.RowIndex].EContact==IO.EContact.A?IO.EContact.B : IO.EContact.A;
                 }
            }
		}
		private void dataGridView_DO_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
            string ComfirmMsg;
            if(DGV_DO.Columns["Status"].Index==e.ColumnIndex)
            {
                ComfirmMsg=string.Format("Would like Operation IO?\nID:{0}\nStatus:{1} ",DGV_DO.Rows[e.RowIndex].Cells["ID"].Value ,DGV_DO.Rows[e.RowIndex].Cells["Status"].Value);
                 if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                 {
                    EzIna.FA.MGR.IOMgr.DOList[e.RowIndex].Value=!(bool)DGV_DO.Rows[e.RowIndex].Cells["Status"].Value;
                 }
            }
		}	
		private void dataGridView_AI_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
            if (DGV_AI.Columns["SetRange"].Index == e.ColumnIndex)
            {
                string ComfirmMsg;
                ComfirmMsg = string.Format("Would like Set AI Range ?");
                if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    EzIna.FA.MGR.IOMgr.AOList[e.RowIndex].SetRange();
                }
            }
        }
		private void dataGridView_AO_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
            
            if(DGV_AO.Columns["SetRange"].Index == e.ColumnIndex)
            {
                string ComfirmMsg;
                ComfirmMsg = string.Format("Would like Set AO Range ?");
                if (MessageBox.Show(ComfirmMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                     EzIna.FA.MGR.IOMgr.AOList[e.RowIndex].SetRange();
                }
            }
            else if (DGV_AO.Columns["Value"].Index == e.ColumnIndex)
            {
                GUI.UserControls.NumberKeypad NumberKeypad=new GUI.UserControls.NumberKeypad();
                if (NumberKeypad.ShowDialog(0,10,EzIna.FA.MGR.IOMgr.AOList[e.RowIndex].Value)==DialogResult.OK)
                {
                    EzIna.FA.MGR.IOMgr.AOList[e.RowIndex].Value=NumberKeypad.Result;
                }
            }
           
        }

		private void UserButtonEvent(object a_obj, EventArgs e)
		{
			Button btn = (Button)a_obj;
			
		}

        private void FrmInforSetupIO_SXGA_Shown(object sender, EventArgs e)
        {
         

        }
        private void DataGridVeiwNoSelection_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView pcontrol = sender as DataGridView;
            pcontrol.ClearSelection();
        }

        private void btn_IO_CONFIG_SAVE_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show
                   (
                   "Would you like to save this configuration??"
                   , "Question"
                   , MessageBoxButtons.YesNo
                   , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FA.MGR.IOMgr.SaveIO();
            }
        }

        private void FrmInforSetupIO_SXGA_Resize(object sender, EventArgs e)
        {
            Panel_DI.Height=TabControl_IO.TabPages[0].Height/2-5;
            Panel_DO.Height=TabControl_IO.TabPages[0].Height/2-5;
            Panel_AI.Height=TabControl_IO.TabPages[0].Height/2-5;
            Panel_AO.Height=TabControl_IO.TabPages[0].Height/2-5;
        }
    }
}
