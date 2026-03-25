using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI.UserControls
{
  
    public class ExpandDataGridView : DataGridView 
    {
        private EventHandler<ExpandDGVEventArgs>  _ExpandChange;
        protected readonly int rowDefaultHeight = 30;
        protected readonly int rowExpandedHeight = 300;
        protected readonly int rowDefaultDivider = 0;
        protected readonly int rowExpandedDivider =0;
        protected readonly int rowDividerMargin = 5;
        protected List<int> rowCurrent=new List<int>();
        private DataGridView pChildDataView;        
        private System.ComponentModel.IContainer components = null;
        private Panel pChildVeiw;
        internal ImageList RowHeaderIconList;
        private bool collapseRow=false;
        private int ExpandOldIDX=-1;
        private int ExpandCurrentIDX=-1;
        public event EventHandler<ExpandDGVEventArgs> ExpandChange
        {
            add
            {
                _ExpandChange+=value;
            }
            remove
            {
                _ExpandChange-=value;
            }
        }
        private enum rowHeaderIcons:int
        {
            expand = 0,
            collapse = 1
        }
        
        public DataGridView ChildDataGridView
        {
            get {return pChildDataView; }
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
      

        public ExpandDataGridView()
        {
            
           InitializeComponent();
           pChildDataView = new DataGridView();           
          
           InitializeDataGridVeiwStyle(this);    
           InitializeDataGridVeiwStyle(pChildDataView);
           //this.DoubleBuffered=true;
           rowExpandedDivider=rowExpandedHeight-rowDefaultHeight;
           pChildVeiw=new Panel();
                
           pChildVeiw.BorderStyle=BorderStyle.None;
           pChildDataView.BorderStyle=BorderStyle.None;
           pChildVeiw.Height=rowExpandedDivider - rowDividerMargin * 2;
           pChildVeiw.Visible=false;
           typeof(Panel).InvokeMember(
                     "DoubleBuffered",
                     BindingFlags.NonPublic |
                     BindingFlags.Instance |
                     BindingFlags.SetProperty,
                     null,
                     pChildVeiw,
                     new object[] { true });
           // pChildVeiw.DoubleBuffered(true);            
           pChildVeiw.Controls.Add(pChildDataView);
           
           this.Controls.Add(pChildVeiw);
           this.RowPostPaint+=dataGridView_RowPostPaint;
        }
        private void InitializeDataGridVeiwStyle(DataGridView a_Value)
        {
           if (a_Value.RowCount > 0 || a_Value.ColumnCount > 0)
           {
                a_Value.Columns.Clear();
                a_Value.Rows.Clear();
           }
           a_Value.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
           a_Value.ReadOnly = false;
           a_Value.AllowUserToAddRows = true;
           a_Value.AllowUserToDeleteRows = false;
           a_Value.AllowUserToOrderColumns = false;
           a_Value.AllowUserToResizeColumns = false;
           a_Value.AllowUserToResizeRows = false;
           a_Value.ColumnHeadersVisible = true;
           a_Value.RowHeadersVisible = false;
           a_Value.MultiSelect = false;
           a_Value.EditMode = DataGridViewEditMode.EditOnEnter;
           a_Value.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           a_Value.AutoGenerateColumns = false;    
           a_Value.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
           a_Value.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
           a_Value.ColumnHeadersHeight = 30;
           a_Value.RowTemplate.Height = 30;
           a_Value.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
           a_Value.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
           a_Value.Dock=DockStyle.Fill;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpandDataGridView));
            this.RowHeaderIconList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // RowHeaderIconList
            // 
            this.RowHeaderIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("RowHeaderIconList.ImageStream")));
            this.RowHeaderIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.RowHeaderIconList.Images.SetKeyName(0, "expand.png");
            this.RowHeaderIconList.Images.SetKeyName(1, "collapse.png");
            // 
            // ExpandDataGridView
            // 
           // this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        public bool ChildDataGridViewVisble
        {
            get { return pChildVeiw.Visible;}
        }
        private void EventFireExpandStatusChange(bool a_value,int a_OldRowIndex,int a_RowIndex)
        {
            collapseRow =a_value;
            pChildVeiw.Visible=collapseRow;                     
            if (_ExpandChange != null)
            {                
                _ExpandChange(this, new ExpandDGVEventArgs(collapseRow,a_OldRowIndex, a_RowIndex));
            }
            
                                           
        }        
				
        protected override void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnRowHeaderMouseClick(e);

            //Rectangle rect = new Rectangle(this.Rows[e.RowIndex].HeaderCell.ContentBounds.((rowDefaultHeight - 16) / 2),((rowDefaultHeight - 16) / 2), 16, 16);//new Rectangle(((rowDefaultHeight - 16) / 2),((rowDefaultHeight - 16) / 2), 16, 16);

         
                if(rowCurrent.Count<=0)
                {
                    //Init Expand
										EventFireExpandStatusChange(true,-1, e.RowIndex);
                    rowCurrent.Add(e.RowIndex);                   
                    this.Rows[e.RowIndex].Height = this.RowTemplate.Height+ChildDataGridView.RowTemplate.Height+ChildDataGridView.RowTemplate.Height*ChildDataGridView.RowCount+rowDividerMargin;//rowExpandedHeight;
                    this.Rows[e.RowIndex].DividerHeight = this.Rows[e.RowIndex].Height-this.RowTemplate.Height;//rowExpandedDivider;
                    this.ClearSelection();
                    this.Rows[e.RowIndex].Selected = true;
                        
                        
                    //collapseRow = true;
                    this.InvalidateRow(e.RowIndex);
                }
                else
                {
                    
                    if(rowCurrent.Contains(e.RowIndex)==false)
                    {
                        if(collapseRow)
                        {                           
                                                      
                            this.Rows[rowCurrent[0]].Height = this.RowTemplate.Height;//rowDefaultHeight;
                            this.Rows[rowCurrent[0]].DividerHeight = rowDefaultDivider;                            
                            this.Rows[e.RowIndex].Height = this.RowTemplate.Height+ChildDataGridView.RowTemplate.Height+ChildDataGridView.RowTemplate.Height*ChildDataGridView.RowCount+rowDividerMargin;//rowExpandedHeight;
                            this.Rows[e.RowIndex].DividerHeight = this.Rows[e.RowIndex].Height-this.RowTemplate.Height;//rowExpandedDivider;
                            this.ClearSelection();
                            this.Rows[e.RowIndex].Selected = true;
                            EventFireExpandStatusChange(true,rowCurrent[0], e.RowIndex);                                     
                            rowCurrent[0]= e.RowIndex;
                            this.InvalidateRow(e.RowIndex);
                        }                        
                    }
                    else
                    {             
                        if(collapseRow)
                        {
                            this.Rows[rowCurrent[0]].Height = this.RowTemplate.Height;
                            this.Rows[rowCurrent[0]].DividerHeight = rowDefaultDivider;
                            EventFireExpandStatusChange(false,-1, e.RowIndex);
                            rowCurrent.Clear();
                            this.InvalidateRow(e.RowIndex);
                            //this.Invalidate();
                        }                                   
                    }

                }              
                /*if(rowCurrent.Contains(e.RowIndex))
                {
                    rowCurrent.Clear();
                    this.Rows[e.RowIndex].Height=rowDefaultHeight;
                    this.Rows[e.RowIndex].DividerHeight=rowDefaultDivider;
                }  
                else
                {
                    if(rowCurrent.Count>0)
                    {
                        int eRow=rowCurrent[0];
                        rowCurrent.Clear();
                        this.Rows[eRow].Height = rowDefaultHeight;
                        this.Rows[eRow].DividerHeight = rowDefaultDivider;
                        this.ClearSelection();
                        EventFireExpandStatusChange(true,eRow);
                        //collapseRow=true;
                        this.Rows[eRow].Selected=true;
                    }
                    else
                    {
                        rowCurrent.Add(e.RowIndex);
                        this.Rows[e.RowIndex].Height = rowExpandedHeight;
                        this.Rows[e.RowIndex].DividerHeight = rowExpandedDivider;
                    }                   
                }*/
            Expand:
                {
                
                   //this.Invalidate();
                }                                     
        }
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) 
        {
            
            Rectangle rect = new Rectangle(e.RowBounds.X + ((rowDefaultHeight - 16) / 2), e.RowBounds.Y + ((rowDefaultHeight - 16) / 2), 16, 16);
            ExpandDataGridView DGV=sender as ExpandDataGridView;
            if(collapseRow)
            {
                if (rowCurrent.Contains(e.RowIndex))
                {
                    //DGV.Rows[e.RowIndex].DividerHeight=DGV.Rows[e.RowIndex].Height-rowDefaultHeight;
                    e.Graphics.DrawImage(RowHeaderIconList.Images[(int)rowHeaderIcons.collapse], rect);
                    pChildVeiw.Location = new Point (1,e.RowBounds.Top + rowDefaultHeight+1);//new Point(e.RowBounds.Left + DGV.RowHeadersWidth, e.RowBounds.Top + rowDefaultHeight + 5);
                    pChildVeiw.Width = e.RowBounds.Right-1 ;//- DGV.RowHeadersWidth;
                    pChildVeiw.Height = DGV.Rows[e.RowIndex].DividerHeight-2;// - 10;
                    //pChildVeiw.Visible=true;
                }
                else
                {
                   // pChildVeiw.Visible=false;
                    e.Graphics.DrawImage(RowHeaderIconList.Images[(int)rowHeaderIcons.expand], rect);
                     Trace.WriteLine("collapse1");
                    //EventFireExpandStatusChange(false,e.RowIndex);
                }                
                //collapseRow=false;
            }
            else
            {
               
                if (rowCurrent.Contains(e.RowIndex))
                {
                    //DGV.Rows[e.RowIndex].DividerHeight = DGV.Rows[e.RowIndex].Height - rowDefaultHeight;
                    e.Graphics.DrawImage(RowHeaderIconList.Images[(int)rowHeaderIcons.collapse], rect);
                    pChildVeiw.Location = new Point (1,e.RowBounds.Top + rowDefaultHeight+1);//new Point(e.RowBounds.Left + DGV.RowHeadersWidth, e.RowBounds.Top + rowDefaultHeight + 5);
                    pChildVeiw.Width = e.RowBounds.Right-1 ;//- DGV.RowHeadersWidth;
                    pChildVeiw.Height = DGV.Rows[e.RowIndex].DividerHeight-2;// - 10;
                    //pChildVeiw.Visible = true;
                }
                else
                {
                    //pChildVeiw.Visible=false;
                    e.Graphics.DrawImage(RowHeaderIconList.Images[(int)rowHeaderIcons.expand], rect);
                    Trace.WriteLine("collapse2");
                }
            }            
        }
        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);
            if(rowCurrent.Count>0)
            {
                //EventFireExpandStatusChange(true,-1,rowCurrent[0]);
                ////collapseRow=true;
                //this.ClearSelection();
                //this.Rows[rowCurrent[0]].Selected=true;              
                this.InvalidateRow(rowCurrent[0]);
            }
        }
        
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);            
        }
        
    }

    public class ExpandDGVEventArgs : EventArgs
    {
        public int RowIndex { get; private set; }
        public int OldRowIndex { get; private set; }
        public bool IsExpand { get; private set; }
        public ExpandDGVEventArgs(bool a_Expand,int a_OldRowIndex,int a_RowIndex)
        {
            RowIndex = a_RowIndex;
            IsExpand = a_Expand;  
            OldRowIndex=      a_OldRowIndex;   
        }
    }
}
