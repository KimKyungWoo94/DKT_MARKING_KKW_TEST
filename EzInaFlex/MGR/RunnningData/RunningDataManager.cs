using EzIna.FA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
    public class RunningDataManager : SingleTone<RunningDataManager>
    {
        protected override void OnCreateInstance()
        {
            m_pDatalist = new List<RunningData>();
            m_pCurrentProcessData = new RunningData();
            base.OnCreateInstance();
        }

        Color m_DGV_SelectionBackColor = Color.SteelBlue;
        Color m_DGV_SelectionForeColor = Color.White;


        private string m_strLotCardCode = "";
        public string strLotCardCode
        {
            get { return m_strLotCardCode; }
            set { m_strLotCardCode = value; }
        }
        private bool m_bCurrentInProcess;
        private long m_iJIGProcessedCount;
        public long iJIGProcessedCount
        {
            get
            {
                return m_iJIGProcessedCount;
            }
            set
            {
                m_iJIGProcessedCount = value;
            }
        }
        private long m_lProductStartIDX;
        public long lProductStartIDX
        {
            get { return m_lProductStartIDX; }
            set { m_lProductStartIDX = value; }
        }

        private long m_lProductProcssDoneCount;
        public long lProductProcessDoneCount
        {
            get { return m_lProductProcssDoneCount; }
            set { m_lProductProcssDoneCount = value; }
        }
        public bool bCurrentInProess
        {
            get { return m_bCurrentInProcess; }
            set { m_bCurrentInProcess = value; }
        }
        private List<RunningData> m_pDatalist;
        private RunningData m_pCurrentProcessData;
        public RunningData pCurrentProcessData
        {
            get { return m_pCurrentProcessData; }
        }
        public void AddData(RunningData a_value)
        {
            m_pDatalist.Add(a_value);
        }
        public void InsertFirstData(RunningData a_value)
        {
            m_pDatalist.Insert(0, a_value);
        }
        public void RemoveFirstData()
        {
            if (m_pDatalist.Count > 0)
                m_pDatalist.RemoveAt(0);
        }
        public void RemoveLastData()
        {
            if (m_pDatalist.Count > 0)
                m_pDatalist.RemoveAt(m_pDatalist.Count - 1);
        }

        public void RemoveData(int a_istartidx, int a_iCount)
        {
            if (m_pDatalist.Count > a_istartidx + a_iCount)
                m_pDatalist.RemoveRange(a_istartidx, a_iCount);
        }
        public int iDataListCount
        {
            get { return m_pDatalist.Count; }
        }
        public RunningData this[int a_IDX]
        {
            get
            {
                if (a_IDX > -1 && a_IDX < m_pDatalist.Count)
                    return m_pDatalist[a_IDX];
                return null;
            }
        }
        public bool InitDGV_DefaultParam(DataGridView a_DatagirdView)
        {
            InitializeDataGridVeiwStyle(a_DatagirdView);
            int iMarkingNoCellWidth = 100;
            int iHScrollbarSize = 20;
            a_DatagirdView.Columns.AddRange(
                    CreateDataGridViewTextColumn("MarkingNo", "", (int)((a_DatagirdView.Width - a_DatagirdView.RowHeadersWidth - iHScrollbarSize) * 0.25)),
                    CreateDataGridViewTextColumn("PosINSP", "", (int)((a_DatagirdView.Width - a_DatagirdView.RowHeadersWidth - iHScrollbarSize) * 0.25)),
                    CreateDataGridViewTextColumn("Marking", "", (int)((a_DatagirdView.Width - a_DatagirdView.RowHeadersWidth - iHScrollbarSize) * 0.25)),
                    CreateDataGridViewTextColumn("MarkingINSP", "", (int)((a_DatagirdView.Width - a_DatagirdView.RowHeadersWidth - iHScrollbarSize) * 0.25))
                    );
            //a_DatagirdView.Rows.Add();
            //a_DatagirdView.RowPostPaint+=dgGrid_RowPostPaint;
            a_DatagirdView.SelectionChanged += DataGridVeiwNoSelection_SelectionChanged;
            return true;
        }
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
            a_DatagirdView.ReadOnly = true;
            a_DatagirdView.AllowUserToAddRows = false;
            a_DatagirdView.AllowUserToDeleteRows = false;
            a_DatagirdView.AllowUserToOrderColumns = false;
            a_DatagirdView.AllowUserToResizeColumns = false;
            a_DatagirdView.AllowUserToResizeRows = false;
            a_DatagirdView.ColumnHeadersVisible = true;
            a_DatagirdView.RowHeadersVisible = true;
            a_DatagirdView.MultiSelect = false;
            a_DatagirdView.EditMode = DataGridViewEditMode.EditOnEnter;
            a_DatagirdView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            a_DatagirdView.AutoGenerateColumns = false;



            a_DatagirdView.RowTemplate.Height = 30;
            a_DatagirdView.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point);
            a_DatagirdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            a_DatagirdView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            a_DatagirdView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            a_DatagirdView.ColumnHeadersHeight = 30;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
            a_DatagirdView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            a_DatagirdView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            a_DatagirdView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            a_DatagirdView.RowHeadersWidth = 40;
            a_DatagirdView.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            a_DatagirdView.RowHeadersDefaultCellStyle.ForeColor = Color.Black;

            a_DatagirdView.BackgroundColor = Color.White;
            a_DatagirdView.ForeColor = Color.Black;
            a_DatagirdView.EnableHeadersVisualStyles = false;

            a_DatagirdView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            a_DatagirdView.ScrollBars = ScrollBars.Vertical;

            a_DatagirdView.ClearSelection();
        }
        public Tuple<bool, string> CheckMultiArrayVaildation(bool bSettingValueCompre = false)
        {
            int iMultiArrayCount = bSettingValueCompre == false ?
                FA.RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetValue<int>() :
                FA.RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_COUNT.GetSettingValue<int>();

            int iRowCount = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetValue<int>() :
            FA.RCP_Modify.COMMON_PRODUCT_ROW_COUNT.GetSettingValue<int>();
            int iColCount = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetValue<int>() :
            FA.RCP_Modify.COMMON_PRODUCT_COL_COUNT.GetSettingValue<int>();
            double fRowPitch = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetValue<double>() :
            FA.RCP_Modify.COMMON_PRODUCT_ROW_PITCH.GetSettingValue<double>();
            double fColPitch = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetValue<double>() :
            FA.RCP_Modify.COMMON_PRODUCT_COL_PITCH.GetSettingValue<double>();
            double fMultiArrayGap = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetValue<double>() :
            FA.RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_GAP.GetSettingValue<double>();
            FA.eRecipeRowProgressDir eRowProcessDir = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetValue<eRecipeRowProgressDir>() :
            FA.RCP_Modify.COMMON_PRODUCT_ROW_PROGRESS_DIR.GetSettingValue<eRecipeRowProgressDir>();
            FA.eRecipeColProgressDir eColProcessDir = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetValue<eRecipeColProgressDir>() :
            FA.RCP_Modify.COMMON_PRODUCT_COL_PROGRESS_DIR.GetSettingValue<eRecipeColProgressDir>();
            FA.eRecieMultiArrayDir eMultiArrayDir = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetValue<eRecieMultiArrayDir>() :
            FA.RCP_Modify.COMMON_PRODUCT_MULTI_ARRAY_DIR.GetSettingValue<eRecieMultiArrayDir>();

            double fFirstPosX = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_FIRST_PRODUCT_X_POS.GetValue<double>() :
            FA.RCP_Modify.COMMON_FIRST_PRODUCT_X_POS.GetSettingValue<double>();
            double fFirstPosY = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_FIRST_PRODUCT_Y_POS.GetValue<double>() :
            FA.RCP_Modify.COMMON_FIRST_PRODUCT_Y_POS.GetSettingValue<double>();
            double fJigWidth = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_JIG_WIDTH.GetValue<double>() :
            FA.RCP_Modify.COMMON_PRODUCT_JIG_WIDTH.GetSettingValue<double>();
            double fJigHeight = bSettingValueCompre == false ?
            FA.RCP_Modify.COMMON_PRODUCT_JIG_HEIGHT.GetValue<double>() :
            FA.RCP_Modify.COMMON_PRODUCT_JIG_HEIGHT.GetSettingValue<double>();

            double fTotalWidth = 0.0;
            double fTotalHeight = 0.0;
            int iCalRowCount = (iRowCount - 1) <= 0 ? 0 : iRowCount - 1;
            int iCalColCount = (iColCount - 1) <= 0 ? 0 : iColCount - 1;
            int iCalMultiArrayCount = (iMultiArrayCount - 1) <= 0 ? 0 : iMultiArrayCount - 1;
            double fLastPosX = fFirstPosX;
            double fLastPosY = fFirstPosY;


            if (iMultiArrayCount > 1)
            {
                if (eMultiArrayDir == eRecieMultiArrayDir.COLUMN)
                {
                    switch (eColProcessDir)
                    {
                        case eRecipeColProgressDir.LEFT:
                            fLastPosX += iCalColCount * fColPitch;
                            break;
                        case eRecipeColProgressDir.RIGHT:
                            fLastPosX -= iCalColCount * fColPitch;
                            break;
                    }
                    fLastPosY += iCalMultiArrayCount * fMultiArrayGap;
                    if (fMultiArrayGap >= 0)
                    {
                        fLastPosY += iCalMultiArrayCount * (iCalRowCount * fRowPitch);
                    }
                    else
                    {
                        fLastPosY -= iCalMultiArrayCount * (iCalRowCount * fRowPitch);
                    }
                }
                else if (eMultiArrayDir == eRecieMultiArrayDir.ROW)
                {
                    switch (eRowProcessDir)
                    {
                        case eRecipeRowProgressDir.UP:
                            {
                                fLastPosY += iCalRowCount * fRowPitch;
                            }
                            break;
                        case eRecipeRowProgressDir.DOWN:
                            {
                                fLastPosY -= iCalRowCount * fRowPitch;
                            }
                            break;
                    }
                    fLastPosX += iCalMultiArrayCount * fMultiArrayGap;

                    if (fMultiArrayGap >= 0)
                    {
                        fLastPosX -= iCalMultiArrayCount * (iCalColCount * fColPitch);
                    }
                    else
                    {
                        fLastPosX += iCalMultiArrayCount * (iCalColCount * fColPitch);
                    }
                }
            }
            else
            {
                switch (eColProcessDir)
                {
                    case eRecipeColProgressDir.LEFT:
                        fLastPosX += iCalColCount * fColPitch;
                        break;
                    case eRecipeColProgressDir.RIGHT:
                        fLastPosX -= iCalColCount * fColPitch;
                        break;
                }
                switch (eRowProcessDir)
                {
                    case eRecipeRowProgressDir.UP:
                        {
                            fLastPosY += iCalRowCount * fRowPitch;
                        }
                        break;
                    case eRecipeRowProgressDir.DOWN:
                        {
                            fLastPosY -= iCalRowCount * fRowPitch;
                        }
                        break;
                }

            }

            if (fLastPosX >= 0 && fLastPosX <= FA.RCP.M100_X_STROKE_LIMIT.AsDouble && fTotalWidth <= fJigWidth &&
                   fLastPosY >= 0 && fLastPosY <= FA.RCP.M100_Y_STROKE_LIMIT.AsDouble && fTotalHeight <= fJigHeight
                    )
            {
                return new Tuple<bool, string>(true, "");
            }
            StringBuilder strRet = new StringBuilder();
            strRet.AppendLine(string.Format($"LastPosX:{fLastPosX.ToString("0.000mm")} , MAX : {FA.RCP.M100_X_STROKE_LIMIT.AsDouble.ToString("0.000mm")}"));
            strRet.AppendLine(string.Format($"LastPosY:{fLastPosY.ToString("0.000mm")} , MAX : {FA.RCP.M100_Y_STROKE_LIMIT.AsDouble.ToString("0.000mm")}"));
            strRet.AppendLine(string.Format($"TotalWidth:{fTotalWidth.ToString("0.000mm")},JigWidth{fJigWidth.ToString("0.000mm")}"));
            strRet.AppendLine(string.Format($"TotalHeight:{fTotalHeight.ToString("0.000mm")},JigHeight{fJigHeight.ToString("0.000mm")}"));
            return new Tuple<bool, string>(false, strRet.ToString());

        }

        private void dgGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.DefaultCellStyle.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        private DataGridViewTextBoxColumn CreateDataGridViewTextColumn(string a_strHeaderTxt, string a_strBindingPropTxt, int a_Width)
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
            pRet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pRet.DefaultCellStyle.ForeColor = Color.Black;
            pRet.DefaultCellStyle.BackColor = Color.White;
            pRet.DefaultCellStyle.SelectionBackColor = m_DGV_SelectionBackColor;
            pRet.DefaultCellStyle.SelectionForeColor = m_DGV_SelectionForeColor;
            pRet.SortMode = DataGridViewColumnSortMode.NotSortable;
            return pRet;
        }
        private void DataGridVeiwNoSelection_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView pcontrol = sender as DataGridView;
            pcontrol.ClearSelection();
        }
    }
}
