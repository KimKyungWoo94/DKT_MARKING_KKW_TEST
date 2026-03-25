using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.MF
{

    public class RcpItem
    {
        public static List<List<RcpItem>> vecItems = new List<List<RcpItem>>();
        public static List<List<RcpItem>> vecItemsBak = new List<List<RcpItem>>();

        public FA.DEF.eRcpCategory m_eRcpCategory;
        public bool m_IsInitialProc = false;
        public string m_strCategory = "NONE";
        public string m_strCaption = "NONE";
        public string m_strValue = "";
        public FA.DEF.eUnit m_eUnit = FA.DEF.eUnit.none;
        public int m_iAxis = -1;
        public bool m_bSetEnable = false;
        public bool m_bStatusEnable = false;
        public bool m_bMoveEnable = false;
				public bool m_bEnable =true;
        private int m_iKey;
				public string m_strFormat;
				public Type   m_pValueType;
        public int iKey { get { return m_iKey; } }
				
        public double AsDouble
        {
            get
            {
                double dRtn = 0.0;
                try
                {
                    double.TryParse(m_strValue, out dRtn);
                }
                catch (Exception exc)
                {

                }

                return dRtn;
            }
        }
				public float AsSingle
				{
						get
						{
								float fRtn = 0.0f;
								try
								{
										float.TryParse(m_strValue, out fRtn);
								}
								catch (Exception exc)
								{

								}

								return fRtn;
						}
				}
        public int AsInt
        {
            get
            {
                int nRtn = 0;
                try
                {
                    decimal toDecimal;
                    decimal.TryParse(m_strValue, out toDecimal);
                    nRtn = (int)toDecimal;

                }
                catch (Exception exc)
                {

                }
                return nRtn;
            }
        }

        public uint AsUint
        {
            get
            {
                int nRtn = 0;
                try
                {
                    decimal toDecimal;
                    decimal.TryParse(m_strValue, out toDecimal);
                    nRtn = (int)toDecimal;

                }
                catch (Exception exc)
                {

                }

                return (uint)nRtn;
            }
        }

      
        public RcpItem(FA.DEF.eRcpCategory a_eRcpItem, int a_iKey,Type a_ValueType,string a_strFormat,bool a_bEnable=true)
        {
            m_eRcpCategory = a_eRcpItem;
            m_iKey = a_iKey;
						m_bEnable=a_bEnable;
						m_pValueType=a_ValueType;
						m_strFormat=a_strFormat;
						if (m_bEnable)
						{
								vecItems[(int)a_eRcpItem].Add(this);
								vecItemsBak[(int)a_eRcpItem].Add(this);
						}
				}
        public RcpItem(FA.DEF.eRcpCategory a_eRcpItem, int a_iKey, string a_strCategory, string a_strName, string a_strValue,Type a_ValueType,string a_strFormat, FA.DEF.eUnit a_eUnit, bool a_IsInitProc, int a_iAxis, params bool[] a_params)
        {
            if (vecItems.Count == 0)
            {
                for (int i = 0; i < (int)FA.DEF.eRcpCategory.Max; i++)
                {
                    vecItems.Add(new List<RcpItem>());
                    vecItemsBak.Add(new List<RcpItem>());
                }
            }

            m_eRcpCategory = a_eRcpItem;
            m_iKey = a_iKey;
            m_strCategory = a_strCategory;
            m_strCaption = a_strName;
            m_strValue = a_strValue;
            m_eUnit = a_eUnit;
            m_IsInitialProc = a_IsInitProc;
            m_iAxis = a_iAxis;
            m_bSetEnable = m_bStatusEnable = m_bMoveEnable = false;
						m_pValueType=a_ValueType;
						m_strFormat=a_strFormat;
            if (a_params.Length >= 1)
            {
                m_bSetEnable = a_params[0];
            }
            if (a_params.Length >= 2)
            {
             
                m_bStatusEnable = a_params[1];
            }
            if (a_params.Length >= 3)
            {
                m_bMoveEnable = a_params[2];
            }
						if(a_params.Length >= 4)
						{
								m_bEnable=a_params[3];
						}
						if(m_bEnable)
						{
								vecItems[(int)a_eRcpItem].Add(this);
								vecItemsBak[(int)a_eRcpItem].Add(this);
						}            
        }

        public void SetDouble(double a_dValue)
        {
            m_strValue = string.Format("{0:F4}", a_dValue);
        }

        public void SetInt(int a_nValue)
        {
            m_strValue = string.Format("{0:D}", a_nValue);
        }
				public override string ToString()
				{
						return m_strValue;
				}

		}
    public static class RCP
    {
        public static List<List<RcpItem>> vecItems = RcpItem.vecItems;
        public static List<List<RcpItem>> vecItemsBak = RcpItem.vecItemsBak;

        public static void Init()
        {
            Trace.WriteLine("MF.Recipes.Initialize Completed");
        }

        public static void Init(string a_strPath)
        {
            Trace.WriteLine("RCP.Init Completed");
        }
        /// <summary>
        /// 현재의 레시피 데이터를 백업한다.
        /// </summary>
        public static void Backup(bool a_bInitiProc)
        {
            for (int i = 0; i < (int)FA.DEF.eRcpCategory.Max; i++)
            {
                for (int j = 0; j < (int)vecItems[i].Count; j++)
                {
                    if (a_bInitiProc && !vecItems[i][j].m_IsInitialProc)
                        continue;
                    if (!a_bInitiProc && vecItems[i][j].m_IsInitialProc)
                        continue;

                    vecItemsBak[i][j].m_strValue = vecItems[i][j].m_strValue;
                    vecItemsBak[i][j].m_eUnit = vecItems[i][j].m_eUnit;
                }
            }
        }
        /// <summary>
        /// 백업한 레시피 데이터를 복원한다.
        /// </summary>
        public static void Restore()
        {
            for (int i = 0; i < (int)FA.DEF.eRcpCategory.Max; i++)
            {
                for (int j = 0; j < (int)vecItems[i].Count; j++)
                {
                    vecItems[i][j].m_strValue = vecItemsBak[i][j].m_strValue;
                    vecItems[i][j].m_eUnit = vecItemsBak[i][j].m_eUnit;
                }
            }
        }

        /// <summary>
        /// 키값으로 Recipe Item을 찾아서 Item을 리턴한다. 찾지 못할 경우 null을 리턴한다.
        /// </summary>
        /// <param name="a_iKey"></param>
        /// <returns></returns>
        public static RcpItem GetRecipeItem(int a_iKey)
        {
            if (CheckValid() == false) return null;

            RcpItem RtnItem = null;
            for (int i = 0; i < (int)FA.DEF.eRcpCategory.Max; i++)
            {
                RtnItem = vecItems[i].Find(item => a_iKey == item.iKey);
                if (RtnItem != null)
                    break;
            }
            return RtnItem;
        }

        public static RcpItem GetRecipeItem_OnlyInitialProcess(int a_iKey)
        {
            if (CheckValid_OnlyInitialProcess() == false) return null;

            RcpItem RtnItem = null;
            RtnItem = vecItems[(int)FA.DEF.eRcpCategory.InitialProcess].Find(item => a_iKey == item.iKey);

            return RtnItem;
        }

        /// <summary>
        /// 레시피 데이터의 중복 체크.
        /// </summary>
        /// <returns></returns>
        public static bool CheckValid()
        {
            for (int i = 0; i < (int)FA.DEF.eRcpCategory.Max; i++)
            {
                for (int j = 0; j < (int)vecItems[i].Count; j++)
                {
                    var list = vecItems[i].FindAll(item => vecItems[i][j].iKey == item.iKey);
                    if (list.Count > 1)
                    {
                        string s = "Duplicated Datas";
                        foreach (var a in list) s += '\n' + a.iKey.ToString() + " : " + a.m_eRcpCategory.ToString() + "-" + a.m_strCaption;
                        MsgBox.Error(s);
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 레시피 데이터의 중복 체크.
        /// </summary>
        /// <returns></returns>
        public static bool CheckValid_OnlyInitialProcess()
        {
            for (int j = 0; j < (int)vecItems[(int)FA.DEF.eRcpCategory.InitialProcess].Count; j++)
            {
                var list = vecItems[(int)FA.DEF.eRcpCategory.InitialProcess].FindAll(item => vecItems[(int)FA.DEF.eRcpCategory.InitialProcess][j].iKey == item.iKey);
                if (list.Count > 1)
                {
                    string s = "Duplicated Datas";
                    foreach (var a in list) s += '\n' + a.iKey.ToString() + " : " + a.m_eRcpCategory.ToString() + "-" + a.m_strCaption;
                    MsgBox.Error(s);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 파일로 부터 레시피 변수의 값을 업데이트한다.
        /// </summary>
        /// <param name="a_strPath"></param>
        private static void DoLoad(string a_strPath, bool a_bInitProc)
        {
            if (File.Exists(a_strPath))
            {
                List<string> items = StringHelper.GetDataSourceTypes<FA.DEF.eRcpCategory>();

                using (StreamReader sr = new StreamReader(a_strPath, Encoding.UTF8))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length < 1) continue;

                        string[] words = line.Split('|');
                        if (words.Length < 2) continue;


                        var list = items.FindAll(item => item.Equals(words[0].Trim()));
                        if (list.Count != 1)
                            continue;

                        FA.DEF.eRcpCategory eItem = list[0].ToEnum<FA.DEF.eRcpCategory>();

                        int nKey = -1;
                        // eCategory | Key | Category | Caption | Value | eUnit | Axis
												if (!int.TryParse(words[1], out nKey)) continue;
												RcpItem rcpitem = vecItems[(int)eItem].Find(findItem => nKey == findItem.iKey);
												if (rcpitem == null) continue;
												if (!a_bInitProc && rcpitem.m_IsInitialProc) continue;

												if (words.Length > 4) rcpitem.m_strValue = words[4];
												if (words.Length > 5) rcpitem.m_eUnit = words[5].ToEnum<FA.DEF.eUnit>();
											
                    }
                }
            }
            else
            {
                string strDir = Utils.GetParentDir(a_strPath + "\\");
                Directory.CreateDirectory(strDir);
                StreamWriter rcp = File.CreateText(a_strPath);
                rcp.Dispose();
            }
        }
        /// <summary>
        /// 파일로 부터 레시피 변수의 값을 업데이트하는 함수와 백업함수를 실행한다.
        /// </summary>
        /// <param name="a_strPath"></param>
        public static void LoadFromFile(string a_strPath, bool a_bInitProc)
        {
            DoLoad(a_strPath, a_bInitProc);
            Backup(a_bInitProc);
        }
        /// <summary>
        /// 레시피 파일로 저장.
        /// </summary>
        /// <param name="a_strPath">저장할 파일 경로</param>
        /// <param name="a_bIsAll">true 일 경우 모든 항목저장</param>
        private static void DoSave(string a_strPath, bool a_bInitProc)
        {
            using (StreamWriter sw = new StreamWriter(a_strPath, false, Encoding.UTF8))
            {
                //		1		2		3		  4		   5	   6	  7	   
                //eCategory | Key | Category | Caption | Value | eUnit | Axis
                const string fmtP = " | {0:D3} | {1} | {2} | {3} | {4} | {5:D3}";


                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine("#" + fmtP, "KEY", "CATEGORY", "CAPTION", "VALUE", "UNIT", "AXIS");
                sw.WriteLine("-------------------------------------------------------------------------------------------");

                for (int i = 0; i < (int)FA.DEF.eRcpCategory.Max; i++)
                {
                    foreach (var item in vecItems[i])
                    {
                        if (!a_bInitProc && item.m_IsInitialProc)
                            continue;
                        if (a_bInitProc && !item.m_IsInitialProc)
                            continue;

                        sw.WriteLine(item.m_eRcpCategory.ToString() //1 
                        + fmtP,
                        item.iKey, //2
                        item.m_strCategory, //3
                        item.m_strCaption, //4
                        item.m_strValue, //5
                        item.m_eUnit.ToString(), //6
                        item.m_iAxis        //7
                        );
                    }
                }
            }
        }

        /// <summary>
        /// 레시피 파일로 저장.
        /// </summary>
        /// <param name="a_strPath">저장할 파일 경로(모든 항목 저장)</param>
        /// <param name="a_bInitProc">공통으로 사용할 레시피 항목 저장 유무</param>
        public static void SaveToFile(string a_strPath, bool a_bInitProc)
        {
            DoSave(a_strPath, a_bInitProc);
            Backup(a_bInitProc);
        }

        public static void Write_To(FA.DEF.eRcpCategory a_eRcpCategory, string a_strCategory, DataGridView a_grid)
        {
            if (a_grid.RowCount == 0)
            {
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
                a_grid.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
                a_grid.ReadOnly = false;
                a_grid.AllowUserToAddRows = false;
                a_grid.AllowUserToDeleteRows = false;
                a_grid.AllowUserToOrderColumns = false;
                a_grid.AllowUserToResizeColumns = false;
                a_grid.AllowUserToResizeRows = false;
                a_grid.ColumnHeadersVisible = true;
                a_grid.RowHeadersVisible = false;
                a_grid.MultiSelect = false;
                a_grid.EditMode = DataGridViewEditMode.EditOnEnter;
                a_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                a_grid.Columns.Clear();
                a_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                a_grid.ColumnHeadersHeight = 30;
                a_grid.RowTemplate.Height = 30;
                a_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                #endregion [Common]

                //No | Category | Name | Key | Value | Unit | Set button | Images | Move Button
                //0    1          2      3     4          5      6            7        8
                #region [No] [0]
                DataGridViewTextBoxColumn No = new DataGridViewTextBoxColumn();
                No.HeaderText = "No";
                No.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                No.Resizable = DataGridViewTriState.False;
                No.ReadOnly = true;
                No.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                No.DefaultCellStyle.BackColor = SystemColors.Control;
                #endregion [No] [0]
                #region [Category] [1]
                DataGridViewTextBoxColumn Category = new DataGridViewTextBoxColumn();
                Category.HeaderText = "Category";
                Category.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Category.Resizable = DataGridViewTriState.False;
                Category.ReadOnly = true;
                Category.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Category.DefaultCellStyle.BackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Category] [1]
                #region [Name] [2]
                DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
                Name.HeaderText = "Name";
                Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Name.Resizable = DataGridViewTriState.False;
                Name.ReadOnly = true;
                Name.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                Name.DefaultCellStyle.BackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Name] 
                #region [Key] [3]
                DataGridViewTextBoxColumn Key = new DataGridViewTextBoxColumn();
                Key.HeaderText = "Key";
                Key.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Key.Resizable = DataGridViewTriState.False;
                Key.ReadOnly = true;
                Key.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Key.DefaultCellStyle.BackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Key] 
                #region [Value] [4]
                DataGridViewTextBoxColumn Position = new DataGridViewTextBoxColumn();
                Position.HeaderText = "Position";
                Position.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Position.Resizable = DataGridViewTriState.False;
                Position.ReadOnly = true;
                Position.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Position.DefaultCellStyle.BackColor = SystemColors.Control;
                //Input_Off.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                //Input_Off.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Value]
                #region [Unit] [5]
                DataGridViewTextBoxColumn Unit = new DataGridViewTextBoxColumn();
                Unit.HeaderText = "Unit";
                Unit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Unit.Resizable = DataGridViewTriState.False;
                Unit.ReadOnly = true;
                Unit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Unit.DefaultCellStyle.BackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                //Input_On.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Key]
                #region [Set button] [6]
                DataGridViewButtonColumn Set = new DataGridViewButtonColumn();
                Set.HeaderText = "Set";
                Set.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Set.Resizable = DataGridViewTriState.False;
                Set.ReadOnly = true;
                Set.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Set.DefaultCellStyle.BackColor = SystemColors.Control;
                Set.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                Set.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Set button]
                #region [Status] [7]
                DataGridViewColumn Status = new DataGridViewColumn(new DataGridViewImageCell());
                Status.HeaderText = "";
                Status.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Status.Resizable = DataGridViewTriState.False;
                Status.ReadOnly = true;
                Status.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Status.DefaultCellStyle.BackColor = SystemColors.Control;
                Status.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                Status.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Status]
                #region [Move button] [8]
                DataGridViewButtonColumn Move = new DataGridViewButtonColumn();
                Move.HeaderText = "Move";
                Move.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Move.Resizable = DataGridViewTriState.False;
                Move.ReadOnly = true;
                Move.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Move.DefaultCellStyle.BackColor = SystemColors.Control;
                Move.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                Move.DefaultCellStyle.SelectionForeColor = Color.Black;
                #endregion [Move button]
                #region [Add]
                a_grid.Columns.AddRange(new DataGridViewTextBoxColumn[] {
                No, Category, Name, Key, Position, Unit});

                a_grid.Columns.Add(Set);
                a_grid.Columns.Add(Status);
                a_grid.Columns.Add(Move);
                #endregion [Add]
                #region [ Head Setting ]
                //Headers setting
                foreach (DataGridViewColumn col in a_grid.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
                }
                #endregion [ head Setting ]

            }
       

						var list = vecItems[(int)a_eRcpCategory].FindAll(item => a_strCategory == item.m_strCategory);
						RcpItem pRcpItem=null;
						int iConvertKey=-1;
						for(int i=0;i<a_grid.RowCount;i++)
						{
								a_grid.AutoResizeColumn(i);
								if (int.TryParse(a_grid[3, i].Value.ToString(), out iConvertKey))
								{
									  pRcpItem =list.Find(item=>item.iKey==iConvertKey);
										if (pRcpItem != null)
										{
												a_grid[4, i].Value = pRcpItem; // implicit operator 값 가져오는지 확인.
												a_grid[5, i].Value = FA.DEF.GetUnitString(pRcpItem.m_eUnit); // implicit operator 값 가져오는지 확인.
										}
								}									
						}
						/*
            for (int i = 0; i < vecItems[(int)a_eRcpCategory].Count; i++)
            {
                var list = vecItems[(int)a_eRcpCategory].FindAll(item => a_strCategory == item.m_strCategory);
                if (list.Count < 1) break;

                for (int j = 0; j < list.Count; j++)
                {
                    a_grid[4, j].Value = (list[j] as RcpItem); // implicit operator 값 가져오는지 확인.
                    a_grid[5, j].Value = FA.DEF.GetUnitString((list[j] as RcpItem).m_eUnit); // implicit operator 값 가져오는지 확인.
                }
            }*/
        }

        public static void Read_From(FA.DEF.eRcpCategory a_eRcpCategory, FA.DEF.eRcpSubCategory a_eSubCategory, DataGridView a_grid)
        {
            if (a_eRcpCategory == FA.DEF.eRcpCategory.Max)
                return;
            // 
            // 			for(int i = 0; i < vecItems[(int)a_eRcpCategory].Count; i++)
            // 			{
            var list = vecItems[(int)a_eRcpCategory].FindAll(item => a_eSubCategory.ToString() == item.m_strCategory);
            if (list.Count < 1)
                return;


            if (a_eSubCategory == FA.DEF.eRcpSubCategory.M100_PATH)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!string.IsNullOrEmpty(a_grid[3, i]?.Value.ToString()))
                    {
                        (list[i] as RcpItem).m_strValue = a_grid[3, i].Value.ToString();
                    }
                }
            }
            else
            {

								RcpItem pRcpItem=null;
								int iConvertKey=-1;
								for(int i =0;i<a_grid.RowCount;i++)
								{
										if (int.TryParse(a_grid[3, i].Value.ToString(), out iConvertKey))
										{
												pRcpItem = list.Find(item => item.iKey == iConvertKey);
												if (pRcpItem != null)
												{
														if (!string.IsNullOrEmpty(a_grid[4, i]?.Value.ToString()))
														{
																pRcpItem.m_strValue = a_grid[4, i].Value.ToString();
														}
												}
												pRcpItem=null;
										}
								}
								/*
                for (int j = 0; j < list.Count; j++)
                {									
                    if (!string.IsNullOrEmpty(a_grid[4, j]?.Value.ToString()))
                    {
                        (list[j] as RcpItem).m_strValue = a_grid[4, j].Value.ToString();
                    }
                }*/
            }

            //}
        }


    }
}
