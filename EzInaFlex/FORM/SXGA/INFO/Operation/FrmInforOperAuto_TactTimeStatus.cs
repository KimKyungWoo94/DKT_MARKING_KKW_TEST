using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
    public partial class FrmInforOperAuto_TactTimeStatus : Form
    {

        private int m_iUpdateStartSetNum = 0;
        private int m_iUpdateStartNum = 0;
        bool m_bProcessInfoRowAddEnable = false;
        private int m_iLoopDGVIDX = 0;
        Color m_DisplayOKColor = Color.LimeGreen;
        Color m_DisplayMisMatchColor = Color.Orange;
        Color m_DisplayFailColor = Color.Red;
        Color m_DisplayDefaultColor = Color.Black;
        TimeSpan m_pCurrentCycleTimeSpan;
        public FrmInforOperAuto_TactTimeStatus()
        {
            InitializeComponent();
            StatusUpdateTimer.Enabled = false;
            //StatusUpdateTimer.Interval
            StatusUpdateTimer.Tick += UpdateStatus;
        }
        private void UpdateDisplayProcessInfo()
        {
            try
            {
                #region [UPTIME]
                dataGridView_WorkTime.ClearSelection();

                if (dataGridView_WorkTime.Rows.Count >= 6)
                {
                    int index = 0;
                    m_pCurrentCycleTimeSpan = FA.MGR.RunMgr.PM.CurrentCycleTime();
                    dataGridView_WorkTime[1, index++].Value = FA.MGR.RecipeRunningData.strLotCardCode;

                    if (FA.MGR.RunMgr.PM.IsCycleTimerRunning())
                    {

                        dataGridView_WorkTime[1, index++].Value =
                                string.Format("{0:D2}:{1:D2}:{2:D3}",
                                m_pCurrentCycleTimeSpan.Minutes,
                                m_pCurrentCycleTimeSpan.Seconds,
                                m_pCurrentCycleTimeSpan.Milliseconds);

                    }
                    else
                    {

                        dataGridView_WorkTime[1, index++].Value =
                                string.Format("{0:D2}:{1:D2}:{2:D3}",
                                FA.MGR.RunMgr.PM.CycleTime.Minutes,
                                FA.MGR.RunMgr.PM.CycleTime.Seconds,
                                FA.MGR.RunMgr.PM.CycleTime.Milliseconds);
                    }


                    dataGridView_WorkTime[1, index++].Value = string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", FA.MGR.RunMgr.PM.RunningTime.Days, FA.MGR.RunMgr.PM.RunningTime.Hours, FA.MGR.RunMgr.PM.RunningTime.Minutes, FA.MGR.RunMgr.PM.RunningTime.Seconds);
                    dataGridView_WorkTime[1, index++].Value = string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", FA.MGR.RunMgr.PM.StopTime.Days, FA.MGR.RunMgr.PM.StopTime.Hours, FA.MGR.RunMgr.PM.StopTime.Minutes, FA.MGR.RunMgr.PM.StopTime.Seconds);
                    dataGridView_WorkTime[1, index++].Value = string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", FA.MGR.RunMgr.PM.ErrorTime.Days, FA.MGR.RunMgr.PM.ErrorTime.Hours, FA.MGR.RunMgr.PM.ErrorTime.Minutes, FA.MGR.RunMgr.PM.ErrorTime.Seconds);
                    dataGridView_WorkTime[1, index++].Value = string.Format("{0:F2} Hours", FA.MGR.RunMgr.PM.GetUPH());
                    //dataGridView_WorkTime[1, index++].Value = string.Format("{0} days {1:D2}:{2:D2}:{3:D2}", FA.MGR.RunMgr.PM.GetMTBF().Days, FA.MGR.RunMgr.PM.GetMTBF().Hours, FA.MGR.RunMgr.PM.GetMTBF().Minutes, FA.MGR.RunMgr.PM.GetMTBF().Seconds);
                }
                #endregion

            }
            catch (Exception ex)
            {

            }
        }

        private void btn_Frm_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panelFrmTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            WinAPIs.ReleaseCapture();
            WinAPIs.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void UpdateStatus(object sender, EventArgs e)
        {
            this.StatusUpdateTimer.Enabled = false;
            UpdateDisplayProcessInfo();
            this.StatusUpdateTimer.Enabled = this.Visible;
        }
        private void FrmFrmInforOperAuto_ProcessStatus_Load(object sender, EventArgs e)
        {
            dataGridView_WorkTime.DoubleBuffered(true);
            FA.MGR.RunMgr.PM.DataGridViewWorkTime_Init(dataGridView_WorkTime);          
        }
        private void FrmInforOperAuto_ProcessStatus_VisibleChanged(object sender, EventArgs e)
        {
            this.StatusUpdateTimer.Enabled = this.Visible;
        }

        private void FrmInforOperAuto_TactTimeStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.StatusUpdateTimer.Enabled = false;
        }

        private void Btn_WorkTimeReset_Click(object sender, EventArgs e)
        {
            if (MsgBox.Confirm("Would you like to reset the run time"))
            {
                FA.MGR.RunMgr.PM.ClearTimes();
                FA.MGR.RunMgr.PM.UpdateTime(FA.MGR.RunMgr.eRunMode);
            }
        }

    }
}
