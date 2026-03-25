using EzIna.Motion;
using EzInaVision;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
    public class ProjectManager : GUI.NotifyProperyChangedBase
    {
        //public delegate void DataGridViewCellEventHandler(object sender, DataGridViewCellEventArgs e);

        private string m_strSelectedModel;
        public string SelectedModel
        {
            get
            {
                return m_strSelectedModel;
            }
            set
            {
                this.CheckPropertyChanged<string>("SelectedModel", ref m_strSelectedModel, value);
            }
        }
        public string SelectedModelPath { get; set; }

        bool m_IsbDataGridView_Recipe_Init = false;
        bool m_IsbDataGridView_Path_Init = false;
        bool m_IsbDataGridView_Option_Init = false;

        public ProjectManager()
        {
            SelectedModel = "";
            SelectedModelPath = "";
        }
        ~ProjectManager()
        {
        }

        #region [Delegate]
        DataGridViewCellEventHandler handlerCellClick_For_Recipe_OnlyInitProc = new DataGridViewCellEventHandler((object sender, DataGridViewCellEventArgs e) =>
        {
            try
            {
                DataGridView grd = (sender as DataGridView);
                if (e.ColumnIndex == 4) //키패드를 통한 포지션 값 셋팅.
                {
                    EzIna.GUI.UserControls.NumberKeypad num = new EzIna.GUI.UserControls.NumberKeypad();
                    if (num.ShowDialog(-10000000, 10000000) == DialogResult.OK)
                    {
                        grd[e.ColumnIndex, e.RowIndex].Value = num.Result.ToString();

                    }
                }
                else if (e.ColumnIndex == 6) // 선택 축의 현재 포지션값으로 셀값 변경.
                {
                    int iKey = -1;
                    MF.RcpItem item = null;
                    MotionItem pMotor = null;
                    if (Int32.TryParse(grd[3, e.RowIndex].Value.ToString(), out iKey))
                    {
                        item = MF.RCP.GetRecipeItem_OnlyInitialProcess(iKey);
                        if (item == null || !item.m_bSetEnable)
                            return;
                        if (MsgBox.Confirm(string.Format("Would you like to set \"{0}\" position??", item.m_strCaption)) == false)
                            return;
                        pMotor = FA.MGR.MotionMgr.GetItem(item.m_iAxis) as MotionItem;
                        if (pMotor == null)
                            return;

                        grd[4, e.RowIndex].Value = pMotor.m_stPositionStatus.fActPos.ToString("F4");
                    }
                }
                else if (e.ColumnIndex == 8) // Move 명령.
                {
                    int iKey = -1;
                    MF.RcpItem item = null;
                    double dTargetPos = 0.0;

                    // 3 == 키값
                    if (Int32.TryParse(grd[3, e.RowIndex].Value.ToString(), out iKey))
                    {

                        item = MF.RCP.GetRecipeItem_OnlyInitialProcess(iKey);
                        if (item == null || !item.m_bMoveEnable)
                            return;
                        if (MsgBox.Confirm(string.Format("Would you like to move to \"{0}\" ??", item.m_strCaption)) == false)
                            return;

                        if (double.TryParse(item.m_strValue, out dTargetPos))
                            FA.MGR.MotionMgr.GetItem(item.m_iAxis).Move_Absolute(dTargetPos, GDMotion.eSpeedType.FAST);
                    }
                }
            }
            catch (System.Exception exc)
            {
                FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
            }
        });
        DataGridViewCellEventHandler handlerCellClick_For_Recipe = new DataGridViewCellEventHandler((object sender, DataGridViewCellEventArgs e) =>
{
    try
    {
        DataGridView grd = (sender as DataGridView);
        if (e.ColumnIndex == 4) //키패드를 통한 포지션 값 셋팅.
        {
            EzIna.GUI.UserControls.NumberKeypad num = new EzIna.GUI.UserControls.NumberKeypad();
            if (num.ShowDialog(-10000, 10000) == DialogResult.OK)
            {
                grd[e.ColumnIndex, e.RowIndex].Value = num.Result.ToString();

            }
        }
        else if (e.ColumnIndex == 6) // 선택 축의 현재 포지션값으로 셀값 변경.
        {
            int iKey = -1;
            MF.RcpItem item = null;
            MotionItem pMotor = null;
            if (Int32.TryParse(grd[3, e.RowIndex].Value.ToString(), out iKey))
            {
                item = MF.RCP.GetRecipeItem(iKey);
                if (item == null || !item.m_bSetEnable)
                    return;
                if (MsgBox.Confirm(string.Format("Would you like to set \"{0}\" position??", item.m_strCaption)) == false)
                    return;
                pMotor = FA.MGR.MotionMgr.GetItem(item.m_iAxis) as MotionItem;
                if (pMotor == null)
                    return;

                grd[4, e.RowIndex].Value = pMotor.m_stPositionStatus.fActPos.ToString("F4");
            }
        }
        else if (e.ColumnIndex == 8) // Move 명령.
        {
            int iKey = -1;
            MF.RcpItem item = null;
            double dTargetPos = 0.0;

            // 3 == 키값
            if (Int32.TryParse(grd[3, e.RowIndex].Value.ToString(), out iKey))
            {

                item = MF.RCP.GetRecipeItem(iKey);
                if (item == null || !item.m_bMoveEnable)
                    return;
                if (MsgBox.Confirm(string.Format("Would you like to move to \"{0}\" ??", item.m_strCaption)) == false)
                    return;

                if (double.TryParse(item.m_strValue, out dTargetPos))
                    FA.MGR.MotionMgr.GetItem(item.m_iAxis).Move_Absolute(dTargetPos, GDMotion.eSpeedType.FAST);
            }
        }
    }
    catch (System.Exception exc)
    {
        FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
    }
});
        DataGridViewCellEventHandler handlerCellClick_For_Option = new DataGridViewCellEventHandler((object sender, DataGridViewCellEventArgs e) =>
   {
       try
       {
           //0 : check
           //1 : name
           //2 : value
           //3 : unit
           DataGridView grd = (DataGridView)sender;
           if (e.ColumnIndex == 0)
           {
               bool state = (bool)grd[e.ColumnIndex, e.RowIndex].Value;

               grd[0, e.RowIndex].Value = !state;
           }
           else if (e.ColumnIndex == 2)
           {
               EzIna.GUI.UserControls.NumberKeypad Num = new EzIna.GUI.UserControls.NumberKeypad();
               if (Num.ShowDialog(0, 10000) == DialogResult.OK)
               {
                   grd[e.ColumnIndex, e.RowIndex].Value = Num.Result.ToString();
               }

           }


       }
       catch (Exception exc)
       {
       }
   });
        DataGridViewCellEventHandler handlerCellClick_For_Path = new DataGridViewCellEventHandler((object sender, DataGridViewCellEventArgs e) =>
{
    try
    {
        DataGridView a_grd = (sender as DataGridView);
        if (e.ColumnIndex == 3) //키패드를 통한 Path 설정.
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.DefaultExt = "*.cal";
            openFileDlg.Filter = "Calibration Files(*.cal)|*.cal";

            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                a_grd[e.ColumnIndex, e.RowIndex].Value = openFileDlg.FileName;
            }
            // 							EzIna.GUI.UserControls.AlphaKeypad AlphakeypadValue = new EzIna.GUI.UserControls.AlphaKeypad();
            // 							if (AlphakeypadValue.ShowDialog()== DialogResult.OK)
            // 							{
            // 								a_grd[e.ColumnIndex, e.RowIndex].Value = AlphakeypadValue.NewValue;
            // 							}
        }
    }
    catch (System.Exception exc)
    {
        FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
    }
});
        DataGridViewCellPaintingEventHandler handlerCellPainting_For_Option = new DataGridViewCellPaintingEventHandler((object sender, DataGridViewCellPaintingEventArgs e) =>
        {
            Color ForeColor = Color.Black;
            Color BackColor = SystemColors.Window;
            Color GridColor = Color.SteelBlue;
            Color SelectionBackColor = Color.White;
            Color SelectionForeColor = Color.Black;

            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                e.Handled = true;
                using (Brush b = new SolidBrush(BackColor))
                {
                    e.Graphics.FillRectangle(b, e.CellBounds);
                }

                if (e.ColumnIndex == 2)
                {
                    using (Pen p = new Pen(Brushes.SteelBlue))
                    {
                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        e.Graphics.DrawLine(p, new Point(e.CellBounds.X, e.CellBounds.Bottom - 1), new Point(e.CellBounds.Right, e.CellBounds.Bottom - 1));
                    }
                }
                e.PaintContent(e.ClipBounds);
            }
        });
        DataGridViewEditingControlShowingEventHandler handlerEditingControlShowing_For_Option = new DataGridViewEditingControlShowingEventHandler((object sender, DataGridViewEditingControlShowingEventArgs e) =>
        {
            try
            {
                if (e.Control.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)e.Control).SelectedIndexChanged += (object a_sender, EventArgs a_e) =>
                    {
                        // 								DataGridView grd = (DataGridView)a_sender;
                        // 								ComboBox item = e.Control as ComboBox;
                    };
                }

            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(exc.Message.ToString());
            }
        });

        #endregion
        #region [PROJECT BUTTONS]
        public void ProjectChange(string a_strProjectChangeName)
        {
            IniFile Ini = new IniFile(FA.FILE.Project);
            Ini.Write("SELECT", "NAME", a_strProjectChangeName);
            Ini = null;

            ProjectOpen();
        }
        public void ProjectOpen()
        {
            try
            {
                IniFile Ini = new IniFile(FA.FILE.Project);
                SelectedModel = Ini.Read("SELECT", "NAME", "Default");
                SelectedModelPath = Path.GetFullPath(Path.Combine(FA.DIR.PJT, FA.MGR.ProjectMgr.SelectedModel));
                FA.FILE.Recipe = string.Format("{0}\\Recipe.rcp", SelectedModelPath);
                FA.FILE.Option = string.Format("{0}\\Option.ini", SelectedModelPath);
                RecipeOpen_PJT();
                OptionOpen_PJT();
                RecipeOpen_InitProc();
                OptionOpen_InitProc();
                WriteDatas_To_Vision();
                Ini = null;
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString() + "\n" + ex.Message.ToString());
            }
        }
        public void ProjectSave()
        {
            try
            {
                IniFile Ini = new IniFile(FA.FILE.Project);
                Ini.Write("SELECT", "NAME", SelectedModel);
                RecipeSave_PJT();
                OptionSave_PJT();
                WriteDatas_To_Vision();
                Ini = null;
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }
        }
        public bool ProjectRename(string a_strSource, string a_strChnagePjtName)
        {
            try
            {
                string strSourcePath = null, strDestPath = null;
                strSourcePath = Path.GetFullPath(Path.Combine(FA.DIR.PJT, a_strSource));
                strDestPath = Path.GetFullPath(Path.Combine(FA.DIR.PJT, a_strChnagePjtName));

                if (Directory.Exists(strSourcePath))
                {
                    Directory.Move(strSourcePath, strDestPath);
                    //Directory.Delete(strSourcePath);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

            return true;
        }
        public bool ProjectAdd(string a_strNewProjectName)
        {
            try
            {
                string strSourcePath = null, strTargetPath = null;
                strSourcePath = Path.GetFullPath(Path.Combine(FA.DIR.PJT, SelectedModel));
                strTargetPath = Path.GetFullPath(Path.Combine(FA.DIR.PJT, a_strNewProjectName));

                Directory.CreateDirectory(strTargetPath);

                string strSourceFile = null, strDestFile;
                if (Directory.Exists(strSourcePath))
                {
                    string[] files = Directory.GetFiles(strSourcePath);
                    foreach (string s in files)
                    {
                        strSourceFile = Path.GetFileName(s);
                        strDestFile = Path.Combine(strTargetPath, strSourceFile);
                        File.Copy(s, strDestFile, true);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

            return true;
        }
        public bool ProjectDelete(string a_strDeleteProjectName)
        {
            try
            {
                if (SelectedModel.ToUpper().Equals(a_strDeleteProjectName.ToUpper()))
                    return false;

                string strTarget = Path.Combine(FA.DIR.PJT, a_strDeleteProjectName);
                if (Directory.Exists(strTarget))
                {
                    string[] files = Directory.GetFiles(strTarget);
                    foreach (string s in files)
                    {
                        string fileName = Path.GetFileName(s);
                        string deletefile = Path.Combine(strTarget, fileName);
                        File.Delete(deletefile);

                    }
                    // 					DirectoryInfo dir = new DirectoryInfo(strTarget);
                    // 					FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);
                    // 					foreach (FileInfo file in files)
                    // 					{
                    // 						file.Attributes = FileAttributes.Normal;
                    // 					}



                    Directory.Delete(strTarget);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }

            return true;
        }
        #endregion[PROJECT BUTTONS]

        #region [FILE OPEN/SAVE]

        public void RecipeOpen_PJT()
        {
            MF.RCP.LoadFromFile(FA.FILE.Recipe, false);
        }
        public void RecipeSave_PJT()
        {
            MF.RCP.SaveToFile(FA.FILE.Recipe, false);
        }
        public void RecipeOpen_InitProc()
        {
            MF.RCP.LoadFromFile(FA.FILE.InitProc, true);
        }
        public void RecipeSave_InitProc()
        {
            MF.RCP.SaveToFile(FA.FILE.InitProc, true);
        }


        public void OptionOpen_PJT()
        {
            MF.OPT.LoadFromFile(FA.FILE.Option, false);
        }

        public void OptionSave_PJT()
        {
            MF.OPT.SaveToFile(FA.FILE.Option, false);
        }

        public void OptionOpen_InitProc()
        {
            MF.OPT.LoadFromFile(FA.FILE.InitProcOption, true);
        }
        public void OptionSave_InitProc()
        {
            MF.OPT.SaveToFile(FA.FILE.InitProcOption, true);

        }
        public void TreeView_Init(TreeView a_pTreeView_Modules, ImageList a_ImageList, params FA.DEF.eRcpCategory[] args)
        {
            a_pTreeView_Modules.BeginUpdate();
            a_pTreeView_Modules.Nodes.Clear();

            a_pTreeView_Modules.ImageList = a_ImageList;
            a_pTreeView_Modules.Font = new Font("Century Gothic", 9F, FontStyle.Regular);



            //Main Category - Sub Category
            //for example Vision - Matcher
            //			  Motion - Stage, Head, 
            TreeNode pCategory = new TreeNode("Category");
            pCategory.ImageIndex = a_ImageList.Images.IndexOfKey("Category.png");
            pCategory.SelectedImageIndex = a_ImageList.Images.IndexOfKey("Category.png");

            TreeNode[] arrTreeNodeMain = new TreeNode[(int)FA.DEF.eRcpCategory.Max];

            int idx = 0;

            // 			foreach (FA.DEF.eRcpCategory Main in Enum.GetValues(typeof(FA.DEF.eRcpCategory)))
            // 			{
            // 				if (Main == FA.DEF.eRcpCategory.Max)
            // 					continue;
            //			}
            foreach (FA.DEF.eRcpCategory Main in args)
            {
                arrTreeNodeMain[idx] = new TreeNode(Main.ToString(), 0, 0);
                FillValuesToTreeNode(Main, ref arrTreeNodeMain[idx], a_ImageList);
                pCategory.Nodes.Add(arrTreeNodeMain[idx]);
            }


            a_pTreeView_Modules.Nodes.Add(pCategory);
            a_pTreeView_Modules.EndUpdate();
        }

        public void FillValuesToTreeNode(FA.DEF.eRcpCategory a_eRcpcategory, ref TreeNode a_node, ImageList a_ImageList)
        {
            List<string> vecList = new List<string>();
            foreach (FA.DEF.eRcpSubCategory sub in Enum.GetValues(typeof(FA.DEF.eRcpSubCategory)))
            {
                vecList.Add(sub.ToString());
            }

            a_node.Tag = a_eRcpcategory.ToString();
            a_node.ImageIndex = a_ImageList.Images.IndexOfKey(a_eRcpcategory.ToString() + ".png");
            a_node.SelectedImageIndex = a_ImageList.Images.IndexOfKey(a_eRcpcategory.ToString() + ".png");


            List<string> Foundlist = new List<string>();

            switch (a_eRcpcategory)
            {
                case FA.DEF.eRcpCategory.Motion:
                    {

                        //Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M00") == true);
                        Foundlist = vecList.FindAll(item => item.Substring(0, 3).Equals("M00") == true);
                    }
                    break;
                case FA.DEF.eRcpCategory.Vision:
                    {
                        Foundlist = vecList.FindAll(item => item.Contains("M10"));
                        Foundlist = Foundlist.FindAll(item => item.Contains("M100") == false);
                    }
                    break;
                case FA.DEF.eRcpCategory.CAM:
                    {
                        Foundlist = vecList.FindAll(item => item.Contains("M20"));
                    }
                    break;
                case FA.DEF.eRcpCategory.Interlock:
                    {
                        Foundlist = vecList.FindAll(item => item.Contains("M30") == true);
                    }
                    break;
                case FA.DEF.eRcpCategory.Path:
                    {

                        Foundlist = vecList.FindAll(item => item.Contains("M40") == true);
                    }
                    break;
                case FA.DEF.eRcpCategory.InitialProcess:
                    {
                        Foundlist = vecList.FindAll(item => item.Contains("M100"));
                    }
                    break;
                case FA.DEF.eRcpCategory.Max:
                    {
                    }
                    break;
                default:
                    break;


            }

            for (int i = 0; i < Foundlist.Count; i++)
            {
                a_node.Nodes.Add(Foundlist[i].ToString(), Foundlist[i].ToString(), a_ImageList.Images.IndexOfKey("unchecked.png"), a_ImageList.Images.IndexOfKey("checked.png"));
            }


        }
        public void listBox_Recipe_Init(ListBox a_pList)
        {
            try
            {
                a_pList.BeginUpdate();
                a_pList.Items.Clear();
                string[] directories = Directory.GetDirectories(FA.DIR.PJT);

                for (int i = 0; i < directories.Length; i++)
                {
                    a_pList.Items.Add(directories[i].Replace(FA.DIR.PJT, ""));
                }

                if (a_pList.Items.Count > 1)
                {
                    a_pList.SelectedIndex = 0;
                }

                for (int i = 0; i < a_pList.Items.Count; i++)
                {
                    if (FA.MGR.ProjectMgr.SelectedModel.ToUpper().Equals(a_pList.Items[i].ToString().ToUpper()))
                        a_pList.SelectedIndex = i;
                }

                a_pList.EndUpdate();
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.ToString());
            }
        }
        #endregion [FILE OPEN/SAVE]

        #region [ DataGridView Initialize ]

        private void RemoveCellClickEvent(DataGridView g)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
            BindingFlags.Static | BindingFlags.NonPublic);

            if (f1 == null) return;

            object obj = f1.GetValue(g);
            PropertyInfo pi = g.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList list = (EventHandlerList)pi.GetValue(g, null);
            list.RemoveHandler(obj, list[obj]);
        }
        public void DataGridView_Init_For_Recipe(DataGridView a_grd, bool a_bInitProc)
        {

            if (a_grd.RowCount == 0)
            {

                Color ForeColor = Color.Black;
                Color BackColor = SystemColors.Window;
                Color SelectionBackColor = Color.SteelBlue;
                Color SelectionForeColor = Color.White;
                #region [Explain]
                //선택된 모든 셀의 선택해제
                //a_grid.ClearSelection();
                //첫번째 행 선택
                //a_grid.Rows[0].Selected = true;
                //마지막 행 선택
                //a_grid.Rows[a_grid.Rows.Count - 1].Selected = true;
                //선택행에 삽입.
                //a_grid.Rows.Insert(1, "test");
                //마지막행에 삽입.
                //a_grid.Rows.Add("last");
                //첫번째 선택된 행의 인덱스값.
                //a_grid.SelectedRows[0].Index;
                //특정 행 삭제
                //a_grid.Rows.RemoveAt(0);  //삭제
                //선택 행의 색 바꾸기
                //a_grid.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                //a_grid.DefaultCellStyle.SelectionForeColor = Color.Black;
                //특정 행의 색 바꾸기
                //a_grid.Rows[2].DefaultCellStyle.BackColor = Color.Red;
                //특정 행열의 색 바꾸기
                //a_grid.Rows[i].Cells[col].Style.BackColor = ColorTranslator.FromHtml(123, 123, 123, 123);
                //셀 내용 읽기 0행, 0열
                //a_grid.Rows[0].Cells[0].Value.ToString();
                //행의 총수
                //int lines = a_grid.Rows.Count
                //우측 스크롤바 현재 셀위치를 보여주게 자동 이동
                //a_grid.CurrentCell = a_grid.Rows[행].Cells[열];
                //활성화된 셀의 행 인덱스값
                //int select = a_grid.CurrentCell.RowIndex;
                #endregion Explain
                #region [Common]
                a_grd.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
                a_grd.ReadOnly = false;
                a_grd.AllowUserToAddRows = false;
                a_grd.AllowUserToDeleteRows = false;
                a_grd.AllowUserToOrderColumns = false;
                a_grd.AllowUserToResizeColumns = false;
                a_grd.AllowUserToResizeRows = false;
                a_grd.ColumnHeadersVisible = true;
                a_grd.RowHeadersVisible = false;
                a_grd.MultiSelect = false;
                a_grd.EditMode = DataGridViewEditMode.EditOnEnter;
                a_grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                a_grd.BackgroundColor = Color.White;
                a_grd.GridColor = Color.SteelBlue;

                a_grd.Columns.Clear();
                a_grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                a_grd.ColumnHeadersHeight = 30;
                a_grd.RowTemplate.Height = 30;
                a_grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                #endregion [Common]


                //No | Category | Name | Key | Value | Unit | Set button | Images | Move Button
                //0    1          2      3     4          5      6            7        8

                #region [Event]
                if (a_bInitProc)
                {
                    a_grd.CellClick -= handlerCellClick_For_Recipe_OnlyInitProc;
                    a_grd.CellClick += handlerCellClick_For_Recipe_OnlyInitProc;
                }
                else
                {
                    a_grd.CellClick -= handlerCellClick_For_Recipe;
                    a_grd.CellClick += handlerCellClick_For_Recipe;
                }

                #endregion

                #region [No] [0]
                DataGridViewTextBoxColumn No = new DataGridViewTextBoxColumn();
                No.HeaderText = "No";
                No.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                No.Resizable = DataGridViewTriState.False;
                No.ReadOnly = true;
                No.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                No.DefaultCellStyle.ForeColor = ForeColor;
                No.DefaultCellStyle.BackColor = BackColor;
                No.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                No.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [No] [0]
                #region [Category] [1]
                DataGridViewTextBoxColumn Category = new DataGridViewTextBoxColumn();
                Category.HeaderText = "Category";
                Category.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Category.Resizable = DataGridViewTriState.False;
                Category.ReadOnly = true;
                Category.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Category.DefaultCellStyle.ForeColor = ForeColor;
                Category.DefaultCellStyle.BackColor = BackColor;
                Category.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Category.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Category] [1]
                #region [Name] [2]
                DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
                Name.HeaderText = "Name";
                Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Name.Resizable = DataGridViewTriState.False;
                Name.ReadOnly = true;
                Name.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Name.DefaultCellStyle.BackColor = SystemColors.Control;
                Name.DefaultCellStyle.ForeColor = ForeColor;
                Name.DefaultCellStyle.BackColor = BackColor;
                Name.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Name.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Name] 
                #region [Key] [3]
                DataGridViewTextBoxColumn Key = new DataGridViewTextBoxColumn();
                Key.HeaderText = "Key";
                Key.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Key.Resizable = DataGridViewTriState.False;
                Key.ReadOnly = true;
                Key.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Key.DefaultCellStyle.ForeColor = ForeColor;
                Key.DefaultCellStyle.BackColor = BackColor;
                Key.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Key.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Key] 
                #region [Value] [4]
                DataGridViewTextBoxColumn Value = new DataGridViewTextBoxColumn();
                Value.HeaderText = "Value";
                Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Value.Resizable = DataGridViewTriState.False;
                Value.ReadOnly = true;
                Value.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Value.DefaultCellStyle.ForeColor = ForeColor;
                Value.DefaultCellStyle.BackColor = BackColor;
                Value.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Value.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Value]
                #region [Unit] [5]
                DataGridViewTextBoxColumn Unit = new DataGridViewTextBoxColumn();
                Unit.HeaderText = "Unit";
                Unit.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Unit.Resizable = DataGridViewTriState.False;
                Unit.ReadOnly = true;
                Unit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Unit.DefaultCellStyle.ForeColor = ForeColor;
                Unit.DefaultCellStyle.BackColor = BackColor;
                Unit.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Unit.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Key]
                #region [Set button] [6]
                //change DataGridViewbuttonColumn to DataGridViewDisableButtonColumn
                DataGridViewDisableButtonColumn Set = new DataGridViewDisableButtonColumn();
                Set.HeaderText = "Set";
                Set.Name = "SetButton";
                Set.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Set.Resizable = DataGridViewTriState.False;
                Set.ReadOnly = true;
                Set.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Set.DefaultCellStyle.BackColor = SystemColors.Control;
                Set.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                Set.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Set button]
                #region [Status] [7]
                DataGridViewColumn Status = new DataGridViewColumn(new DataGridViewImageCell());
                Status.HeaderText = "Status";
                Status.Name = "StatusImage";
                Status.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Status.Resizable = DataGridViewTriState.False;
                Status.ReadOnly = true;
                Status.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Status.DefaultCellStyle.BackColor = SystemColors.Control;
                Status.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                Status.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Status]
                #region [Move button] [8]
                //DataGridViewButtonColumn to DataGridViewDisableButtonColumn
                DataGridViewDisableButtonColumn Move = new DataGridViewDisableButtonColumn();
                Move.HeaderText = "Move";
                Move.Name = "MoveButton";
                Move.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Move.Resizable = DataGridViewTriState.False;
                Move.ReadOnly = true;
                Move.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Move.DefaultCellStyle.BackColor = SystemColors.Control;
                Move.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                Move.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Move button]
                #region [Add]
                a_grd.Columns.AddRange(new DataGridViewTextBoxColumn[] {
                    No, Category, Name, Key, Value, Unit});

                a_grd.Columns.Add(Set);
                a_grd.Columns.Add(Status);
                a_grd.Columns.Add(Move);
                #endregion [Add]
                #region [ Head Setting ]
                //Headers setting
                foreach (DataGridViewColumn col in a_grd.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
                }

                //a_pDataGridView_Modules.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;  
                a_grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                a_grd.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
                a_grd.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                a_grd.EnableHeadersVisualStyles = false;
                a_grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                a_grd.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                //a_pDataGridView_Modules.RowsDefaultCellStyle.BackColor = SystemColors.HotTrack;
                //a_pDataGridView_Modules.RowsDefaultCellStyle.ForeColor = Color.White;
                #endregion [ head Setting ]

            }
        }
        public void DataGridView_Init_For_Option(DataGridView a_grd)
        {
            if (a_grd.RowCount == 0)
            {
                Color ForeColor = Color.Black;
                Color BackColor = SystemColors.Window;
                Color GridColor = Color.SteelBlue;
                Color SelectionBackColor = Color.White;
                Color SelectionForeColor = Color.Black;

                a_grd.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
                a_grd.ReadOnly = true;
                a_grd.AllowUserToAddRows = false;
                a_grd.AllowUserToDeleteRows = false;
                a_grd.AllowUserToOrderColumns = false;
                a_grd.AllowUserToResizeColumns = false;
                a_grd.AllowUserToResizeRows = false;
                a_grd.ColumnHeadersVisible = false;
                a_grd.RowHeadersVisible = false;
                a_grd.MultiSelect = false;
                a_grd.SelectionMode = DataGridViewSelectionMode.CellSelect;
                a_grd.BackgroundColor = BackColor;
                a_grd.GridColor = GridColor;
                a_grd.Columns.Clear();
                a_grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                a_grd.ColumnHeadersHeight = 30;
                a_grd.RowTemplate.Height = 30;
                a_grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                a_grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                #region [Events]
                a_grd.CellClick -= handlerCellClick_For_Option;
                a_grd.CellClick += handlerCellClick_For_Option;

                a_grd.EditingControlShowing -= handlerEditingControlShowing_For_Option;
                a_grd.EditingControlShowing += handlerEditingControlShowing_For_Option;


                a_grd.CellPainting -= handlerCellPainting_For_Option;
                a_grd.CellPainting += handlerCellPainting_For_Option;
                // 				a_grd.CellPainting += new DataGridViewCellPaintingEventHandler(delegate (object obj, DataGridViewCellPaintingEventArgs e)
                // 				{
                // 					if (e.RowIndex > -1 && e.ColumnIndex > -1)
                // 					{
                // 						e.Handled = true;
                // 						using (Brush b = new SolidBrush(BackColor))
                // 						{
                // 							e.Graphics.FillRectangle(b, e.CellBounds);
                // 						}
                // 
                // 						if(e.ColumnIndex == 2 )
                // 						{
                // 							using (Pen p = new Pen(Brushes.SteelBlue))
                // 							{
                // 								p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                // 								e.Graphics.DrawLine(p, new Point(e.CellBounds.X, e.CellBounds.Bottom - 1), new Point(e.CellBounds.Right, e.CellBounds.Bottom - 1));
                // 							}
                // 						}
                // 						e.PaintContent(e.ClipBounds);
                // 					}
                // 				});
                #endregion[Events]

                // check box | Name | Value | Unit
                a_grd.Columns.Clear();
                a_grd.Columns.Add(new DataGridViewCheckBoxColumn()); // checkBox
                a_grd.Columns.Add(new DataGridViewTextBoxColumn());  // Name
                a_grd.Columns.Add(new DataGridViewTextBoxColumn());  // Value
                a_grd.Columns.Add(new DataGridViewTextBoxColumn());  // Unit

                /*
                //ComboBox 유형의 셀을 만듬.
                DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
                //DisplayStyle을 ComboBox롤 설정.
                combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                foreach(FA.DEF.eUnit item in Enum.GetValues(typeof(FA.DEF.eUnit)))
                {
                    if(item == FA.DEF.eUnit.Max)
                        break;
                    combo.Items.Add(FA.DEF.strUnits[(int)item]);
                }
                a_grd.Columns.Add(combo);  // unit
                */


                a_grd.Columns[0].MinimumWidth = (int)(a_grd.Width * 0.1);
                a_grd.Columns[1].MinimumWidth = (int)(a_grd.Width * 0.6);
                a_grd.Columns[2].MinimumWidth = (int)(a_grd.Width * 0.2);
                a_grd.Columns[3].MinimumWidth = (int)(a_grd.Width * 0.1);

                a_grd.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                a_grd.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                a_grd.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                a_grd.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


                a_grd.Columns[0].ReadOnly = false;
                for (int i = 0; i < a_grd.Columns.Count; i++)
                {
                    a_grd.Columns[i].DefaultCellStyle.BackColor = BackColor;
                    a_grd.Columns[i].DefaultCellStyle.SelectionBackColor = BackColor;
                    a_grd.Columns[i].DefaultCellStyle.SelectionForeColor = ForeColor;
                }

                #region [ Head Setting ]
                //Headers setting
                foreach (DataGridViewColumn col in a_grd.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
                }

                //a_pDataGridView_Modules.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;  
                a_grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                a_grd.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
                a_grd.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                a_grd.EnableHeadersVisualStyles = false;
                a_grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                a_grd.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                //a_pDataGridView_Modules.RowsDefaultCellStyle.BackColor = SystemColors.HotTrack;
                //a_pDataGridView_Modules.RowsDefaultCellStyle.ForeColor = Color.White;
                #endregion [ head Setting ]

            }

        }
        public void DataGridView_Init_For_Path(DataGridView a_grd)
        {
            if (a_grd.RowCount == 0)
            {
                Color ForeColor = Color.Black;
                Color BackColor = SystemColors.Window;
                Color SelectionBackColor = Color.SteelBlue;
                Color SelectionForeColor = Color.White;
                #region [Explain]
                //선택된 모든 셀의 선택해제
                //a_grid.ClearSelection();
                //첫번째 행 선택
                //a_grid.Rows[0].Selected = true;
                //마지막 행 선택
                //a_grid.Rows[a_grid.Rows.Count - 1].Selected = true;
                //선택행에 삽입.
                //a_grid.Rows.Insert(1, "test");
                //마지막행에 삽입.
                //a_grid.Rows.Add("last");
                //첫번째 선택된 행의 인덱스값.
                //a_grid.SelectedRows[0].Index;
                //특정 행 삭제
                //a_grid.Rows.RemoveAt(0);  //삭제
                //선택 행의 색 바꾸기
                //a_grid.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                //a_grid.DefaultCellStyle.SelectionForeColor = Color.Black;
                //특정 행의 색 바꾸기
                //a_grid.Rows[2].DefaultCellStyle.BackColor = Color.Red;
                //특정 행열의 색 바꾸기
                //a_grid.Rows[i].Cells[col].Style.BackColor = ColorTranslator.FromHtml(123, 123, 123, 123);
                //셀 내용 읽기 0행, 0열
                //a_grid.Rows[0].Cells[0].Value.ToString();
                //행의 총수
                //int lines = a_grid.Rows.Count
                //우측 스크롤바 현재 셀위치를 보여주게 자동 이동
                //a_grid.CurrentCell = a_grid.Rows[행].Cells[열];
                //활성화된 셀의 행 인덱스값
                //int select = a_grid.CurrentCell.RowIndex;
                #endregion Explain
                #region [Common]
                a_grd.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
                a_grd.ReadOnly = false;
                a_grd.AllowUserToAddRows = false;
                a_grd.AllowUserToDeleteRows = false;
                a_grd.AllowUserToOrderColumns = false;
                a_grd.AllowUserToResizeColumns = false;
                a_grd.AllowUserToResizeRows = false;
                a_grd.ColumnHeadersVisible = true;
                a_grd.RowHeadersVisible = false;
                a_grd.MultiSelect = false;
                a_grd.EditMode = DataGridViewEditMode.EditOnEnter;
                a_grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                a_grd.BackgroundColor = Color.White;
                a_grd.GridColor = Color.SteelBlue;

                a_grd.Columns.Clear();
                a_grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                a_grd.ColumnHeadersHeight = 30;
                a_grd.RowTemplate.Height = 30;
                a_grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                #endregion [Common]

                #region [Event]
                a_grd.CellClick -= handlerCellClick_For_Path;
                a_grd.CellClick += handlerCellClick_For_Path;
                //RemoveCellClickEvent(a_grd);
                // 				a_grd.CellClick += (object sender, DataGridViewCellEventArgs e) => 
                // 				{
                // 					try
                // 					{
                // 						if (e.ColumnIndex == 3) //키패드를 통한 Path 설정.
                // 						{
                // 							OpenFileDialog openFileDlg = new OpenFileDialog();
                // 							openFileDlg.DefaultExt = "*.cal";
                // 							openFileDlg.Filter = "Calibration Files(*.cal)|*.cal";
                // 							 
                // 							if(openFileDlg.ShowDialog() == DialogResult.OK)
                // 							{
                // 								a_grd[e.ColumnIndex, e.RowIndex].Value = openFileDlg.FileName;	
                // 							}
                // // 							EzIna.GUI.UserControls.AlphaKeypad AlphakeypadValue = new EzIna.GUI.UserControls.AlphaKeypad();
                // // 							if (AlphakeypadValue.ShowDialog()== DialogResult.OK)
                // // 							{
                // // 								a_grd[e.ColumnIndex, e.RowIndex].Value = AlphakeypadValue.NewValue;
                // // 							}
                // 						}
                // 					}
                // 					catch (System.Exception exc)
                // 					{
                // 						FA.LOG.Fatal("Exception", "{0}:{1}", exc.StackTrace, exc.Message);
                // 					}
                // 				};

                #endregion

                #region [No] [0]
                DataGridViewTextBoxColumn No = new DataGridViewTextBoxColumn();
                No.HeaderText = "No";
                No.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                No.Resizable = DataGridViewTriState.False;
                No.ReadOnly = true;
                No.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                No.DefaultCellStyle.ForeColor = ForeColor;
                No.DefaultCellStyle.BackColor = BackColor;
                No.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                No.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [No] [0]
                #region [Name] [1]
                DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
                Name.HeaderText = "Name";
                Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Name.Resizable = DataGridViewTriState.False;
                Name.ReadOnly = true;
                Name.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Name.DefaultCellStyle.BackColor = SystemColors.Control;
                Name.DefaultCellStyle.ForeColor = ForeColor;
                Name.DefaultCellStyle.BackColor = BackColor;
                Name.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Name.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Name] 
                #region [Key] [2]
                DataGridViewTextBoxColumn Key = new DataGridViewTextBoxColumn();
                Key.HeaderText = "Key";
                Key.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Key.Resizable = DataGridViewTriState.False;
                Key.ReadOnly = true;
                Key.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Key.DefaultCellStyle.ForeColor = ForeColor;
                Key.DefaultCellStyle.BackColor = BackColor;
                Key.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Key.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Key] 
                #region [Value] [3]
                DataGridViewTextBoxColumn Value = new DataGridViewTextBoxColumn();
                Value.HeaderText = "Value";
                Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Value.Resizable = DataGridViewTriState.False;
                Value.ReadOnly = true;
                Value.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Value.DefaultCellStyle.ForeColor = ForeColor;
                Value.DefaultCellStyle.BackColor = BackColor;
                Value.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                Value.DefaultCellStyle.SelectionForeColor = SelectionForeColor;
                #endregion [Value]

                #region [Add]
                a_grd.Columns.AddRange(new DataGridViewTextBoxColumn[] {
                    No, Key, Name, Value});
                #endregion [Add]


                a_grd.Columns[0].MinimumWidth = (int)(a_grd.Width * 0.1);
                a_grd.Columns[1].MinimumWidth = (int)(a_grd.Width * 0.3);
                a_grd.Columns[2].MinimumWidth = (int)(a_grd.Width * 0.1);
                a_grd.Columns[3].MinimumWidth = (int)(a_grd.Width * 0.5);

                a_grd.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                a_grd.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                a_grd.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                a_grd.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


                a_grd.Columns[0].ReadOnly = false;
                for (int i = 0; i < a_grd.Columns.Count; i++)
                {
                    a_grd.Columns[i].DefaultCellStyle.BackColor = BackColor;
                    a_grd.Columns[i].DefaultCellStyle.SelectionBackColor = BackColor;
                    a_grd.Columns[i].DefaultCellStyle.SelectionForeColor = ForeColor;
                }

                #region [ Head Setting ]
                //Headers setting
                foreach (DataGridViewColumn col in a_grd.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
                }

                //a_pDataGridView_Modules.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;  
                a_grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                a_grd.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.HotTrack;
                a_grd.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                a_grd.EnableHeadersVisualStyles = false;
                a_grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                a_grd.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                //a_pDataGridView_Modules.RowsDefaultCellStyle.BackColor = SystemColors.HotTrack;
                //a_pDataGridView_Modules.RowsDefaultCellStyle.ForeColor = Color.White;
                #endregion [ head Setting ]

            }

        }
        #endregion
        #region [ DataGridView Change to Modules]
        public void DataGridView_ChangeToModules(DataGridView a_GridOfDatas, DataGridView a_GridOfOptions, FA.DEF.eRcpCategory a_eRcpCategory, FA.DEF.eRcpSubCategory a_eRcpSubCategory, ImageList a_ImageList)
        {
            a_GridOfDatas.Rows.Clear();
            a_GridOfOptions.Rows.Clear();

            a_GridOfDatas.Columns.Clear();

            if (a_eRcpCategory == FA.DEF.eRcpCategory.InitialProcess)
            {
                if (a_eRcpSubCategory == FA.DEF.eRcpSubCategory.M100_PATH)
                {
                    DataGridView_Init_For_Path(a_GridOfDatas);
                    DataGridView_ModuleChangeToPath(a_GridOfDatas, a_eRcpSubCategory, a_ImageList);
                }
                else
                {
                    DataGridView_Init_For_Recipe(a_GridOfDatas, true);
                    DataGridView_ModuleChange(a_GridOfDatas, a_eRcpCategory, a_eRcpSubCategory, a_ImageList);
                }
            }
            else
            {
                DataGridView_Init_For_Recipe(a_GridOfDatas, false);
                DataGridView_Init_For_Option(a_GridOfOptions);
                DataGridView_ModuleChange(a_GridOfDatas, a_eRcpCategory, a_eRcpSubCategory, a_ImageList);
            }
            MF.OPT.Write_To(a_GridOfOptions, a_eRcpSubCategory);
            int idx = 0;
            foreach (DataGridViewRow row in a_GridOfDatas.Rows)
            {
                Color bg = row.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.BackColor = idx++ % 2 == 0 ? SystemColors.Control : bg;
            }
            a_GridOfDatas.AutoResizeColumns();
            a_GridOfOptions.AutoResizeColumns();
        }

        private void DataGridView_ModuleChange(DataGridView a_grid, FA.DEF.eRcpCategory a_eRcpCategory, FA.DEF.eRcpSubCategory a_eRcpSubCategory, ImageList a_ImageList)
        {
            var SubRcpList = MF.RCP.vecItems[(int)a_eRcpCategory].FindAll(item => item.m_strCategory == a_eRcpSubCategory.ToString());
            if (SubRcpList.Count > 0)
            {
                foreach (MF.RcpItem item in SubRcpList)
                {
                    if (item.m_bEnable)
                        DGV_RcpItemAddRow(a_grid, item, item.m_strFormat, item.m_pValueType, a_ImageList, "unchecked.png", "disable.png");
                }
            }
        }
        private void DGV_RcpItemAddRow(DataGridView a_grid, MF.RcpItem a_item, string a_strValueFormat, Type a_ValueType, ImageList a_ImageList, string a_strInitIMG, string a_strDisableIMG)
        {
            DataGridViewDisableButtonCell item = null;
            string pValue = "";
            switch (Type.GetTypeCode(a_ValueType))
            {
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    {
                        pValue = string.Format(a_item.m_strFormat, a_item.AsUint);
                    }
                    break;

                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    {
                        pValue = string.Format(a_item.m_strFormat, a_item.AsInt);
                    }

                    break;

                case TypeCode.Single:
                case TypeCode.Double:
                    {
                        pValue = string.Format(a_item.m_strFormat, a_item.AsDouble);
                    }
                    break;
                case TypeCode.String:
                    {
                        pValue = a_item.m_strValue;
                    }
                    break;

            }
            if (pValue != null)
            {
                a_grid.Rows.Add((a_grid.Rows.Count + 1).ToString("D02"), a_item.m_strCategory, a_item.m_strCaption, a_item.iKey, pValue, FA.DEF.GetUnitString(a_item.m_eUnit), "Set", a_ImageList.Images[a_strInitIMG], "Move");
                item = (DataGridViewDisableButtonCell)a_grid.Rows[a_grid.Rows.Count - 1].Cells["SetButton"];
                item.Enabled = a_item.m_bSetEnable;
                item = (DataGridViewDisableButtonCell)a_grid.Rows[a_grid.Rows.Count - 1].Cells["MoveButton"];
                item.Enabled = a_item.m_bMoveEnable;
                a_grid.Rows[a_grid.Rows.Count - 1].Cells["StatusImage"].Value = a_item.m_bStatusEnable == true ? a_ImageList.Images[a_strInitIMG] : a_ImageList.Images[a_strDisableIMG];
            }
        }

        private void DataGridView_ModuleChangeToPath(DataGridView a_grid, FA.DEF.eRcpSubCategory a_eSubCategory, ImageList a_ImageList)
        {
            int index;

            //No | Name | Key | Value 
            //0       1      2      3 			

            DataGridViewDisableButtonCell item = null;
            for (int i = 0; i < a_grid.Columns.Count; i++)
            {
                a_grid.Columns[i].ReadOnly = true;//i.In(0, 1, 2, 3, 5, 6, 7, 8);
            }

            switch (a_eSubCategory)
            {
                case FA.DEF.eRcpSubCategory.M100_PATH:
                    {
                        index = 1;
                        a_grid.Rows.Add(index++.ToString("D02"), FA.RCP.M100_GALVO_CAL_PATH.m_strCaption, FA.RCP.M100_GALVO_CAL_PATH.iKey, FA.RCP.M100_GALVO_CAL_PATH.m_strValue);
                        a_grid.Rows.Add(index++.ToString("D02"), FA.RCP.M100_STAGE_CAL_PATH.m_strCaption, FA.RCP.M100_STAGE_CAL_PATH.iKey, FA.RCP.M100_STAGE_CAL_PATH.m_strValue);
                    }
                    break;

            }

            int idx = 0;
            foreach (DataGridViewRow row in a_grid.Rows)
            {
                Color bg = row.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.BackColor = idx++ % 2 == 0 ? SystemColors.Control : bg;
            }
        }
        #endregion [ DataGridView Change to Modules]
        #region [ Get / Set Data from VisionMgr ]
        public void ReadDatas_From_Vision(FA.DEF.eVision a_eVision, FA.DEF.eVisionItem a_eItem)
        {
            if (a_eVision == FA.DEF.eVision.FINE)
            {
                if (FA.MGR.VisionMgr.GetLib(a_eVision.ToString()) != null)
                {
                    switch (a_eItem)
                    {
                        case FA.DEF.eVisionItem.Scale:
                            #region [ Scale ] 
                            FA.RCP.M100_VisionCalFineScaleX.m_strValue = FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_stLibInfo.dLensScaleX.ToString();
                            FA.RCP.M100_VisionCalFineScaleY.m_strValue = FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_stLibInfo.dLensScaleY.ToString();
                            #endregion [ Scale ]
                            break;
                        case FA.DEF.eVisionItem.Match:
                            #region [ Matcher ]
                            EzInaVision.GDV.MatcherConfig MatchConfig = new EzInaVision.GDV.MatcherConfig();
                            MatchConfig = FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_MatchConfig;

                            FA.RCP.F_Matcher_Minimum_Scale.m_strValue = MatchConfig.m_fMinScale.ToString();
                            FA.RCP.F_Matcher_Maximum_Scale.m_strValue = MatchConfig.m_fMaxScale.ToString();
                            FA.RCP.F_Matcher_Match_Score.m_strValue = MatchConfig.m_fScore.ToString();
                            FA.RCP.F_Matcher_Match_Angle.m_strValue = MatchConfig.m_fAngle.ToString();
                            FA.RCP.F_Matcher_Match_MaxCount.m_strValue = MatchConfig.m_fMaxPosition.ToString();
                            FA.RCP.F_Matcher_Match_CorrelationMode.m_strValue = MatchConfig.m_iCorrelationMode.ToString();
                            FA.RCP.F_Matcher_Match_ContrastMode.m_strValue = MatchConfig.m_iMatchContrastMode.ToString();

                            #endregion [ Matcher ]
                            break;
                        case FA.DEF.eVisionItem.Blob:
                            #region [ Blob ]
                            EzInaVision.GDV.BlobConfig BlobConfig = new EzInaVision.GDV.BlobConfig();
                            BlobConfig = FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_BlobParam;
                            FA.RCP.F_Blob_Method.m_strValue = BlobConfig.m_nPadBlobMethod.ToString();
                            FA.RCP.F_Blob_GrayMinValue.m_strValue = BlobConfig.m_nPadGrayMinVal.ToString();
                            FA.RCP.F_Blob_GrayMaxValue.m_strValue = BlobConfig.m_nPadGrayMaxVal.ToString();
                            FA.RCP.F_Blob_MinWidth.m_strValue = BlobConfig.m_fPadWidthMin.ToString();
                            FA.RCP.F_Blob_MaxWidth.m_strValue = BlobConfig.m_fPadWidthMax.ToString();
                            FA.RCP.F_Blob_MinHeight.m_strValue = BlobConfig.m_fPadHeightMin.ToString();
                            FA.RCP.F_Blob_MaxHeight.m_strValue = BlobConfig.m_fPadHeightMax.ToString();
                            FA.RCP.F_Blob_AeraMin.m_strValue = BlobConfig.m_fPadAreaMin.ToString();
                            FA.RCP.F_Blob_AeraMax.m_strValue = BlobConfig.m_fPadAreaMax.ToString();

                            #endregion [ Blob ]
                            break;
                        case FA.DEF.eVisionItem.Filters:
                            #region [ Filters ] 
                            EzInaVision.GDV.FilterConfig FilterConfig = new EzInaVision.GDV.FilterConfig();
                            FilterConfig = FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_Filterconfig;
                            FA.RCP.F_Filter_ThresHoldLevel.m_strValue = FilterConfig.m_nThresHoldValue.ToString();
                            FA.RCP.F_Filter_OpenDisk.m_strValue = FilterConfig.m_nOpenDiskValue.ToString();
                            FA.RCP.F_Filter_CloseDisk.m_strValue = FilterConfig.m_nCloseDiskValue.ToString();
                            FA.RCP.F_Filter_ErodeDisk.m_strValue = FilterConfig.m_nErodeDiskValue.ToString();
                            FA.RCP.F_Filter_Dilate.m_strValue = FilterConfig.m_nDilateDiskValue.ToString();

                            #endregion [ Filters ] 
                            break;
                        case FA.DEF.eVisionItem.ROIs:
                            #region [ ROIs ]
                            Rectangle[] rtItems = new Rectangle[(int)EzInaVision.GDV.eRoiItems.Max];

                            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                            {
                                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_dicRoiSize.TryGetValue(i, out rtItems[i]);
                            }

                            FA.RCP.F_ROI_NO1_X.m_strValue = rtItems[0].X.ToString();
                            FA.RCP.F_ROI_NO1_Y.m_strValue = rtItems[0].Y.ToString();
                            FA.RCP.F_ROI_NO1_WIDTH.m_strValue = rtItems[0].Width.ToString();
                            FA.RCP.F_ROI_NO1_HEIGHT.m_strValue = rtItems[0].Height.ToString();

                            FA.RCP.F_ROI_NO2_X.m_strValue = rtItems[1].X.ToString();
                            FA.RCP.F_ROI_NO2_Y.m_strValue = rtItems[1].Y.ToString();
                            FA.RCP.F_ROI_NO2_WIDTH.m_strValue = rtItems[1].Width.ToString();
                            FA.RCP.F_ROI_NO2_HEIGHT.m_strValue = rtItems[1].Height.ToString();

                            FA.RCP.F_ROI_NO3_X.m_strValue = rtItems[2].X.ToString();
                            FA.RCP.F_ROI_NO3_Y.m_strValue = rtItems[2].Y.ToString();
                            FA.RCP.F_ROI_NO3_WIDTH.m_strValue = rtItems[2].Width.ToString();
                            FA.RCP.F_ROI_NO3_HEIGHT.m_strValue = rtItems[2].Height.ToString();

                            FA.RCP.F_ROI_NO4_X.m_strValue = rtItems[3].X.ToString();
                            FA.RCP.F_ROI_NO4_Y.m_strValue = rtItems[3].Y.ToString();
                            FA.RCP.F_ROI_NO4_WIDTH.m_strValue = rtItems[3].Width.ToString();
                            FA.RCP.F_ROI_NO4_HEIGHT.m_strValue = rtItems[3].Height.ToString();

                            FA.RCP.F_ROI_NO5_X.m_strValue = rtItems[4].X.ToString();
                            FA.RCP.F_ROI_NO5_Y.m_strValue = rtItems[4].Y.ToString();
                            FA.RCP.F_ROI_NO5_WIDTH.m_strValue = rtItems[4].Width.ToString();
                            FA.RCP.F_ROI_NO5_HEIGHT.m_strValue = rtItems[4].Height.ToString();

                            FA.RCP.F_ROI_NO6_X.m_strValue = rtItems[5].X.ToString();
                            FA.RCP.F_ROI_NO6_Y.m_strValue = rtItems[5].Y.ToString();
                            FA.RCP.F_ROI_NO6_WIDTH.m_strValue = rtItems[5].Width.ToString();
                            FA.RCP.F_ROI_NO6_HEIGHT.m_strValue = rtItems[5].Height.ToString();

                            FA.RCP.F_ROI_NO7_X.m_strValue = rtItems[6].X.ToString();
                            FA.RCP.F_ROI_NO7_Y.m_strValue = rtItems[6].Y.ToString();
                            FA.RCP.F_ROI_NO7_WIDTH.m_strValue = rtItems[6].Width.ToString();
                            FA.RCP.F_ROI_NO7_HEIGHT.m_strValue = rtItems[6].Height.ToString();

                            FA.RCP.F_ROI_NO8_X.m_strValue = rtItems[7].X.ToString();
                            FA.RCP.F_ROI_NO8_Y.m_strValue = rtItems[7].Y.ToString();
                            FA.RCP.F_ROI_NO8_WIDTH.m_strValue = rtItems[7].Width.ToString();
                            FA.RCP.F_ROI_NO8_HEIGHT.m_strValue = rtItems[7].Height.ToString();

                            FA.RCP.F_ROI_NO9_X.m_strValue = rtItems[8].X.ToString();
                            FA.RCP.F_ROI_NO9_Y.m_strValue = rtItems[8].Y.ToString();
                            FA.RCP.F_ROI_NO9_WIDTH.m_strValue = rtItems[8].Width.ToString();
                            FA.RCP.F_ROI_NO9_HEIGHT.m_strValue = rtItems[8].Height.ToString();

                            FA.RCP.F_ROI_NO10_X.m_strValue = rtItems[9].X.ToString();
                            FA.RCP.F_ROI_NO10_Y.m_strValue = rtItems[9].Y.ToString();
                            FA.RCP.F_ROI_NO10_WIDTH.m_strValue = rtItems[9].Width.ToString();
                            FA.RCP.F_ROI_NO10_HEIGHT.m_strValue = rtItems[9].Height.ToString();
                            #endregion [ ROIs ]
                            break;
                    }
                }
            }
            else if (a_eVision == FA.DEF.eVision.COARSE)
            {
                if (FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()) != null)
                {
                    switch (a_eItem)
                    {
                        case FA.DEF.eVisionItem.Scale:
                            #region [ Scale ] 
                            FA.RCP.M100_VisionCalCoarseScaleX.m_strValue = FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_stLibInfo.dLensScaleX.ToString();
                            FA.RCP.M100_VisionCalCoarseScaleY.m_strValue = FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_stLibInfo.dLensScaleY.ToString();
                            #endregion [ Scale ]
                            break;
                        case FA.DEF.eVisionItem.Match:
                            #region [ Matcher ]
                            EzInaVision.GDV.MatcherConfig MatchConfig = new EzInaVision.GDV.MatcherConfig();
                            MatchConfig = FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_MatchConfig;

                            FA.RCP.C_Matcher_Minimum_Scale.m_strValue = MatchConfig.m_fMinScale.ToString();
                            FA.RCP.C_Matcher_Maximum_Scale.m_strValue = MatchConfig.m_fMaxScale.ToString();
                            FA.RCP.C_Matcher_Match_Score.m_strValue = MatchConfig.m_fScore.ToString();
                            FA.RCP.C_Matcher_Match_Angle.m_strValue = MatchConfig.m_fAngle.ToString();
                            FA.RCP.C_Matcher_Match_MaxCount.m_strValue = MatchConfig.m_fMaxPosition.ToString();
                            FA.RCP.C_Matcher_Match_CorrelationMode.m_strValue = MatchConfig.m_iCorrelationMode.ToString();
                            FA.RCP.C_Matcher_Match_ContrastMode.m_strValue = MatchConfig.m_iMatchContrastMode.ToString();

                            #endregion [ Matcher ]
                            break;
                        case FA.DEF.eVisionItem.Blob:
                            #region [ Blob ]
                            EzInaVision.GDV.BlobConfig BlobConfig = new EzInaVision.GDV.BlobConfig();
                            BlobConfig = FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_BlobParam;
                            FA.RCP.C_Blob_Method.m_strValue = BlobConfig.m_nPadBlobMethod.ToString();
                            FA.RCP.C_Blob_GrayMinValue.m_strValue = BlobConfig.m_nPadGrayMinVal.ToString();
                            FA.RCP.C_Blob_GrayMaxValue.m_strValue = BlobConfig.m_nPadGrayMaxVal.ToString();
                            FA.RCP.C_Blob_MinWidth.m_strValue = BlobConfig.m_fPadWidthMin.ToString();
                            FA.RCP.C_Blob_MaxWidth.m_strValue = BlobConfig.m_fPadWidthMax.ToString();
                            FA.RCP.C_Blob_MinHeight.m_strValue = BlobConfig.m_fPadHeightMin.ToString();
                            FA.RCP.C_Blob_MaxHeight.m_strValue = BlobConfig.m_fPadHeightMax.ToString();
                            FA.RCP.C_Blob_AeraMin.m_strValue = BlobConfig.m_fPadAreaMin.ToString();
                            FA.RCP.C_Blob_AeraMax.m_strValue = BlobConfig.m_fPadAreaMax.ToString();

                            #endregion [ Blob ]
                            break;
                        case FA.DEF.eVisionItem.Filters:
                            #region [ Filters ] 
                            EzInaVision.GDV.FilterConfig stFilterConfig = new EzInaVision.GDV.FilterConfig();
                            stFilterConfig = FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_Filterconfig;
                            FA.RCP.C_Filter_ThresHoldLevel.m_strValue = stFilterConfig.m_nThresHoldValue.ToString();
                            FA.RCP.C_Filter_OpenDisk.m_strValue = stFilterConfig.m_nOpenDiskValue.ToString();
                            FA.RCP.C_Filter_CloseDisk.m_strValue = stFilterConfig.m_nCloseDiskValue.ToString();
                            FA.RCP.C_Filter_ErodeDisk.m_strValue = stFilterConfig.m_nErodeDiskValue.ToString();
                            FA.RCP.C_Filter_Dilate.m_strValue = stFilterConfig.m_nDilateDiskValue.ToString();

                            #endregion [ Filters ] 
                            break;
                        case FA.DEF.eVisionItem.ROIs:
                            #region [ ROIs ]
                            Rectangle[] rtItems = new Rectangle[(int)EzInaVision.GDV.eRoiItems.Max];

                            for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                            {
                                FA.MGR.VisionMgr.GetLib(a_eVision.ToString()).m_LibInfo.m_dicRoiSize.TryGetValue(i, out rtItems[i]);
                            }

                            FA.RCP.C_ROI_NO1_X.m_strValue = rtItems[0].X.ToString();
                            FA.RCP.C_ROI_NO1_Y.m_strValue = rtItems[0].Y.ToString();
                            FA.RCP.C_ROI_NO1_WIDTH.m_strValue = rtItems[0].Width.ToString();
                            FA.RCP.C_ROI_NO1_HEIGHT.m_strValue = rtItems[0].Height.ToString();

                            FA.RCP.C_ROI_NO2_X.m_strValue = rtItems[1].X.ToString();
                            FA.RCP.C_ROI_NO2_Y.m_strValue = rtItems[1].Y.ToString();
                            FA.RCP.C_ROI_NO2_WIDTH.m_strValue = rtItems[1].Width.ToString();
                            FA.RCP.C_ROI_NO2_HEIGHT.m_strValue = rtItems[1].Height.ToString();

                            FA.RCP.C_ROI_NO3_X.m_strValue = rtItems[2].X.ToString();
                            FA.RCP.C_ROI_NO3_Y.m_strValue = rtItems[2].Y.ToString();
                            FA.RCP.C_ROI_NO3_WIDTH.m_strValue = rtItems[2].Width.ToString();
                            FA.RCP.C_ROI_NO3_HEIGHT.m_strValue = rtItems[2].Height.ToString();

                            FA.RCP.C_ROI_NO4_X.m_strValue = rtItems[3].X.ToString();
                            FA.RCP.C_ROI_NO4_Y.m_strValue = rtItems[3].Y.ToString();
                            FA.RCP.C_ROI_NO4_WIDTH.m_strValue = rtItems[3].Width.ToString();
                            FA.RCP.C_ROI_NO4_HEIGHT.m_strValue = rtItems[3].Height.ToString();

                            FA.RCP.C_ROI_NO5_X.m_strValue = rtItems[4].X.ToString();
                            FA.RCP.C_ROI_NO5_Y.m_strValue = rtItems[4].Y.ToString();
                            FA.RCP.C_ROI_NO5_WIDTH.m_strValue = rtItems[4].Width.ToString();
                            FA.RCP.C_ROI_NO5_HEIGHT.m_strValue = rtItems[4].Height.ToString();

                            FA.RCP.C_ROI_NO6_X.m_strValue = rtItems[5].X.ToString();
                            FA.RCP.C_ROI_NO6_Y.m_strValue = rtItems[5].Y.ToString();
                            FA.RCP.C_ROI_NO6_WIDTH.m_strValue = rtItems[5].Width.ToString();
                            FA.RCP.C_ROI_NO6_HEIGHT.m_strValue = rtItems[5].Height.ToString();

                            FA.RCP.C_ROI_NO7_X.m_strValue = rtItems[6].X.ToString();
                            FA.RCP.C_ROI_NO7_Y.m_strValue = rtItems[6].Y.ToString();
                            FA.RCP.C_ROI_NO7_WIDTH.m_strValue = rtItems[6].Width.ToString();
                            FA.RCP.C_ROI_NO7_HEIGHT.m_strValue = rtItems[6].Height.ToString();

                            FA.RCP.C_ROI_NO8_X.m_strValue = rtItems[7].X.ToString();
                            FA.RCP.C_ROI_NO8_Y.m_strValue = rtItems[7].Y.ToString();
                            FA.RCP.C_ROI_NO8_WIDTH.m_strValue = rtItems[7].Width.ToString();
                            FA.RCP.C_ROI_NO8_HEIGHT.m_strValue = rtItems[7].Height.ToString();

                            FA.RCP.C_ROI_NO9_X.m_strValue = rtItems[8].X.ToString();
                            FA.RCP.C_ROI_NO9_Y.m_strValue = rtItems[8].Y.ToString();
                            FA.RCP.C_ROI_NO9_WIDTH.m_strValue = rtItems[8].Width.ToString();
                            FA.RCP.C_ROI_NO9_HEIGHT.m_strValue = rtItems[8].Height.ToString();

                            FA.RCP.C_ROI_NO10_X.m_strValue = rtItems[9].X.ToString();
                            FA.RCP.C_ROI_NO10_Y.m_strValue = rtItems[9].Y.ToString();
                            FA.RCP.C_ROI_NO10_WIDTH.m_strValue = rtItems[9].Width.ToString();
                            FA.RCP.C_ROI_NO10_HEIGHT.m_strValue = rtItems[9].Height.ToString();
                            #endregion [ ROIs ]
                            break;
                    }
                }
            }
        }
        public void WriteDatas_To_Vision()
        {
            if (FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()) != null)
            {

                #region [ Scale ] 
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_stLibInfo.dLensScaleX = FA.RCP.M100_VisionCalFineScaleX.AsDouble;
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_stLibInfo.dLensScaleY = FA.RCP.M100_VisionCalFineScaleY.AsDouble;
                #endregion [ Scale ]
                #region [ Matcher ]
                //EzInaVision.GDV.MatcherConfig MatchConfig = new EzInaVision.GDV.MatcherConfig();
                //MatchConfig.m_fMinScale = FA.RCP.F_Matcher_Minimum_Scale.AsDouble;
                //MatchConfig.m_fMaxScale = FA.RCP.F_Matcher_Maximum_Scale.AsDouble;
                //MatchConfig.m_fScore = FA.RCP.F_Matcher_Match_Score.AsDouble;
                //MatchConfig.m_fAngle = FA.RCP.F_Matcher_Match_Angle.AsDouble;
                //MatchConfig.m_fMaxPosition = FA.RCP.F_Matcher_Match_MaxCount.AsDouble;
                //MatchConfig.m_iCorrelationMode = FA.RCP.F_Matcher_Match_CorrelationMode.AsInt;
                //MatchConfig.m_iMatchContrastMode = FA.RCP.F_Matcher_Match_ContrastMode.AsInt;
                //FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_MatchConfig = MatchConfig;
                #endregion [ Matcher ]
                #region [ Blob ]
                EzInaVision.GDV.BlobConfig stBlobParam = new EzInaVision.GDV.BlobConfig();
                stBlobParam.m_nPadBlobMethod = FA.RCP.F_Blob_Method.AsInt;
                stBlobParam.m_nPadGrayMinVal = FA.RCP.F_Blob_GrayMinValue.AsUint;
                stBlobParam.m_nPadGrayMaxVal = FA.RCP.F_Blob_GrayMaxValue.AsUint;
                stBlobParam.m_fPadWidthMin = FA.RCP.F_Blob_MinWidth.AsSingle;
                stBlobParam.m_fPadWidthMax = FA.RCP.F_Blob_MaxWidth.AsSingle;
                stBlobParam.m_fPadHeightMin = FA.RCP.F_Blob_MinHeight.AsSingle;
                stBlobParam.m_fPadHeightMax = FA.RCP.F_Blob_MaxHeight.AsSingle;
                stBlobParam.m_fPadAreaMin = FA.RCP.F_Blob_AeraMin.AsSingle;
                stBlobParam.m_fPadAreaMax = FA.RCP.F_Blob_AeraMax.AsSingle;
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_BlobParam = stBlobParam;
                #endregion [ Blob ]
                #region [ Filters ] 
                EzInaVision.GDV.FilterConfig FilterConfig = new EzInaVision.GDV.FilterConfig();
                FilterConfig.m_nThresHoldValue = FA.RCP.F_Filter_ThresHoldLevel.AsUint;
                FilterConfig.m_nOpenDiskValue = FA.RCP.F_Filter_OpenDisk.AsUint;
                FilterConfig.m_nCloseDiskValue = FA.RCP.F_Filter_CloseDisk.AsUint;
                FilterConfig.m_nErodeDiskValue = FA.RCP.F_Filter_ErodeDisk.AsUint;
                FilterConfig.m_nDilateDiskValue = FA.RCP.F_Filter_Dilate.AsUint;
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_Filterconfig = FilterConfig;
                #endregion [ Filters ] 
                #region [ ROIs ]
                Rectangle[] rtItems = new Rectangle[(int)EzInaVision.GDV.eRoiItems.Max];
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].X = FA.RCP.F_ROI_NO1_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Y = FA.RCP.F_ROI_NO1_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Width = FA.RCP.F_ROI_NO1_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Height = FA.RCP.F_ROI_NO1_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].X = FA.RCP.F_ROI_NO2_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Y = FA.RCP.F_ROI_NO2_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Width = FA.RCP.F_ROI_NO2_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Height = FA.RCP.F_ROI_NO2_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].X = FA.RCP.F_ROI_NO3_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Y = FA.RCP.F_ROI_NO3_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Width = FA.RCP.F_ROI_NO3_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Height = FA.RCP.F_ROI_NO3_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].X = FA.RCP.F_ROI_NO4_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Y = FA.RCP.F_ROI_NO4_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Width = FA.RCP.F_ROI_NO4_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Height = FA.RCP.F_ROI_NO4_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].X = FA.RCP.F_ROI_NO5_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Y = FA.RCP.F_ROI_NO5_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Width = FA.RCP.F_ROI_NO5_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Height = FA.RCP.F_ROI_NO5_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].X = FA.RCP.F_ROI_NO6_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Y = FA.RCP.F_ROI_NO6_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Width = FA.RCP.F_ROI_NO6_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Height = FA.RCP.F_ROI_NO6_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].X = FA.RCP.F_ROI_NO7_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Y = FA.RCP.F_ROI_NO7_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Width = FA.RCP.F_ROI_NO7_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Height = FA.RCP.F_ROI_NO7_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].X = FA.RCP.F_ROI_NO8_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Y = FA.RCP.F_ROI_NO8_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Width = FA.RCP.F_ROI_NO8_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Height = FA.RCP.F_ROI_NO8_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].X = FA.RCP.F_ROI_NO9_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Y = FA.RCP.F_ROI_NO9_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Width = FA.RCP.F_ROI_NO9_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Height = FA.RCP.F_ROI_NO9_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].X = FA.RCP.F_ROI_NO10_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Y = FA.RCP.F_ROI_NO10_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Width = FA.RCP.F_ROI_NO10_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Height = FA.RCP.F_ROI_NO10_HEIGHT.AsInt;


                for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                    FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).m_LibInfo.m_dicRoiSize.AddOrUpdate(i, rtItems[i], (k, v) => rtItems[i]);
                #endregion [ ROIs ]
                #region [ Option ] 
                //Options
                ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool> LibOption = new ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool>();
                //LibOption.TryAdd(EzInaVision.GDV.eLibOption.ROI, FA.OPT.ROIsVisible);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.CROSS, FA.OPT.CrossLineVisible);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.MATCH_RESULT, FA.OPT.MatchResultDisplay);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.BLOB_RESULT, FA.OPT.BlobResultDisplay);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, FA.OPT.FilterDisplay);

                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.FINE.ToString()).SetOptions(LibOption);
                #endregion [ Option ]

            }
            if (FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()) != null)
            {
                #region [ Scale ] 
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).m_LibInfo.m_stLibInfo.dLensScaleX = FA.RCP.M100_VisionCalCoarseScaleX.AsDouble;
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).m_LibInfo.m_stLibInfo.dLensScaleY = FA.RCP.M100_VisionCalCoarseScaleY.AsDouble;
                #endregion [ Scale ]
                #region [ Matcher ]
                EzInaVision.GDV.MatcherConfig MatchConfig = new EzInaVision.GDV.MatcherConfig();
                MatchConfig.m_fMinScale = FA.RCP.F_Matcher_Minimum_Scale.AsDouble;
                MatchConfig.m_fMaxScale = FA.RCP.F_Matcher_Maximum_Scale.AsDouble;
                MatchConfig.m_fScore = FA.RCP.F_Matcher_Match_Score.AsDouble;
                MatchConfig.m_fAngle = FA.RCP.F_Matcher_Match_Angle.AsDouble;
                MatchConfig.m_fMaxPosition = FA.RCP.F_Matcher_Match_MaxCount.AsDouble;
                MatchConfig.m_iCorrelationMode = FA.RCP.F_Matcher_Match_CorrelationMode.AsInt;
                MatchConfig.m_iMatchContrastMode = FA.RCP.F_Matcher_Match_ContrastMode.AsInt;
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).m_LibInfo.m_MatchConfig = MatchConfig;
                #endregion [ Matcher ]
                #region [ Blob ]
                EzInaVision.GDV.BlobConfig BlobConfig = new EzInaVision.GDV.BlobConfig();
                BlobConfig.m_nPadBlobMethod = FA.RCP.F_Blob_Method.AsInt;
                BlobConfig.m_nPadGrayMinVal = FA.RCP.F_Blob_GrayMinValue.AsUint;
                BlobConfig.m_nPadGrayMaxVal = FA.RCP.F_Blob_GrayMaxValue.AsUint;
                BlobConfig.m_fPadWidthMin = FA.RCP.F_Blob_MinWidth.AsSingle;
                BlobConfig.m_fPadWidthMax = FA.RCP.F_Blob_MaxWidth.AsSingle;
                BlobConfig.m_fPadHeightMin = FA.RCP.F_Blob_MinHeight.AsSingle;
                BlobConfig.m_fPadHeightMax = FA.RCP.F_Blob_MaxHeight.AsSingle;
                BlobConfig.m_fPadAreaMin = FA.RCP.F_Blob_AeraMin.AsSingle;
                BlobConfig.m_fPadAreaMax = FA.RCP.F_Blob_AeraMax.AsSingle;
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).m_LibInfo.m_BlobParam = BlobConfig;
                #endregion [ Blob ]
                #region [ Filters ] 
                EzInaVision.GDV.FilterConfig FilterConfig = new EzInaVision.GDV.FilterConfig();
                FilterConfig.m_nThresHoldValue = FA.RCP.F_Filter_ThresHoldLevel.AsUint;
                FilterConfig.m_nOpenDiskValue = FA.RCP.F_Filter_OpenDisk.AsUint;
                FilterConfig.m_nCloseDiskValue = FA.RCP.F_Filter_CloseDisk.AsUint;
                FilterConfig.m_nErodeDiskValue = FA.RCP.F_Filter_ErodeDisk.AsUint;
                FilterConfig.m_nDilateDiskValue = FA.RCP.F_Filter_Dilate.AsUint;
                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).m_LibInfo.m_Filterconfig = FilterConfig;
                #endregion [ Filters ] 
                #region [ ROIs ]
                Rectangle[] rtItems = new Rectangle[(int)EzInaVision.GDV.eRoiItems.Max];
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].X = FA.RCP.F_ROI_NO1_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Y = FA.RCP.F_ROI_NO1_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Width = FA.RCP.F_ROI_NO1_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No0].Height = FA.RCP.F_ROI_NO1_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].X = FA.RCP.F_ROI_NO2_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Y = FA.RCP.F_ROI_NO2_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Width = FA.RCP.F_ROI_NO2_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No1].Height = FA.RCP.F_ROI_NO2_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].X = FA.RCP.F_ROI_NO3_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Y = FA.RCP.F_ROI_NO3_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Width = FA.RCP.F_ROI_NO3_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No2].Height = FA.RCP.F_ROI_NO3_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].X = FA.RCP.F_ROI_NO4_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Y = FA.RCP.F_ROI_NO4_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Width = FA.RCP.F_ROI_NO4_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No3].Height = FA.RCP.F_ROI_NO4_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].X = FA.RCP.F_ROI_NO5_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Y = FA.RCP.F_ROI_NO5_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Width = FA.RCP.F_ROI_NO5_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No4].Height = FA.RCP.F_ROI_NO5_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].X = FA.RCP.F_ROI_NO6_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Y = FA.RCP.F_ROI_NO6_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Width = FA.RCP.F_ROI_NO6_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No5].Height = FA.RCP.F_ROI_NO6_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].X = FA.RCP.F_ROI_NO7_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Y = FA.RCP.F_ROI_NO7_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Width = FA.RCP.F_ROI_NO7_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No6].Height = FA.RCP.F_ROI_NO7_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].X = FA.RCP.F_ROI_NO8_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Y = FA.RCP.F_ROI_NO8_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Width = FA.RCP.F_ROI_NO8_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No7].Height = FA.RCP.F_ROI_NO8_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].X = FA.RCP.F_ROI_NO9_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Y = FA.RCP.F_ROI_NO9_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Width = FA.RCP.F_ROI_NO9_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No8].Height = FA.RCP.F_ROI_NO9_HEIGHT.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].X = FA.RCP.F_ROI_NO10_X.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Y = FA.RCP.F_ROI_NO10_Y.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Width = FA.RCP.F_ROI_NO10_WIDTH.AsInt;
                rtItems[(int)EzInaVision.GDV.eRoiItems.ROI_No9].Height = FA.RCP.F_ROI_NO10_HEIGHT.AsInt;


                for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
                    FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).m_LibInfo.m_dicRoiSize.AddOrUpdate(i, rtItems[i], (k, v) => rtItems[i]);
                #endregion [ ROIs ]
                #region [ Option ] 
                //Options
                ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool> LibOption = new ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool>();
                //LibOption.TryAdd(EzInaVision.GDV.eLibOption.ROI, FA.OPT.ROIsVisible);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.CROSS, FA.OPT.CrossLineVisible);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.MATCH_RESULT, FA.OPT.MatchResultDisplay);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.BLOB_RESULT, FA.OPT.BlobResultDisplay);
                LibOption.TryAdd(EzInaVision.GDV.eLibOption.ENABLE_FILTERS, FA.OPT.FilterDisplay);

                FA.MGR.VisionMgr.GetLib(FA.DEF.eVision.COARSE.ToString()).SetOptions(LibOption);
                #endregion [ Option ]
            }
        }

        #endregion
    }//end of class

}//end of namespace
