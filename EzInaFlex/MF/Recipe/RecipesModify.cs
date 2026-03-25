using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.MF
{


    public static partial class RCP_Modify
    {

        private static Dictionary<int, RecipeItemBase> TotalRcpItemList = new Dictionary<int, RecipeItemBase>();
        private static Dictionary<int, RecipeItemBase> TotalRcpItemListBak = new Dictionary<int, RecipeItemBase>();
        private static Dictionary<FA.DEF.eRecipeCategory, Dictionary<string, List<RecipeItemBase>>> RcpItemList = new Dictionary<FA.DEF.eRecipeCategory, Dictionary<string, List<RecipeItemBase>>>();
        private static Dictionary<FA.DEF.eRecipeCategory, Dictionary<string, List<RecipeItemGroup>>> RcpItemGroupList = new Dictionary<FA.DEF.eRecipeCategory, Dictionary<string, List<RecipeItemGroup>>>();

        public static List<RecipeItemBase> GetParameterList(FA.DEF.eRecipeCategory a_Main,string a_strSubCategory)
        {
            if (RcpItemList.ContainsKey(a_Main))
            {
                if(RcpItemList[a_Main].ContainsKey(a_strSubCategory))
                {
                    return RcpItemList[a_Main][a_strSubCategory];
                }
            }
            return null;
        }
        public static string[] GetSubCatagoryStringList(FA.DEF.eRecipeCategory a_Main)
        {
            if (RcpItemList.ContainsKey(a_Main))
            {
                return RcpItemList[a_Main].Select(x=>x.Key).ToArray();
            }
            return null;
        }
        public static bool IsExistGroupitem(FA.DEF.eRecipeCategory a_Main,string a_strSubCategory)
        {
            if (RcpItemGroupList.ContainsKey(a_Main) == true)
            {
                if (RcpItemGroupList[a_Main].ContainsKey(a_strSubCategory))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsExistCategory(FA.DEF.eRecipeCategory a_Main,string a_strSubCategory)
        {
            if (RcpItemList.ContainsKey(a_Main))
            {
                if (RcpItemList[a_Main].ContainsKey(a_strSubCategory))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Init()
        {						
            System.Reflection.FieldInfo[] fieldInfos = typeof(FA.RCP_Modify).GetFields(BindingFlags.Public | BindingFlags.Static);
            MF.RecipeItemBase pItem = null;
            MF.RecipeItemGroup pGroupItem=null;
						FA.RCP_Modify.InitRecipeParam();
            foreach (FieldInfo Item in fieldInfos)
            {
                if (Item.FieldType.BaseType == typeof(MF.RecipeItemBase))
                {
                    pItem = (MF.RecipeItemBase)Item.GetValue(null);
                    if (pItem != null)
                    {
                        AddRecipeItem(pItem.eMainRcpCategory, pItem.strSubCategory, pItem);
                    }
                }
                if (Item.FieldType == typeof(MF.RecipeItemGroup))
                {
                    pGroupItem=(MF.RecipeItemGroup)Item.GetValue(null);
                    if (pGroupItem != null)
                    {
                        AddRecipeItemGroup(pGroupItem);
                    }
                }
            }
            Trace.WriteLine("MF.Recipes.Initialize Completed");
        }
      

        public static void Init(string a_strPath)
        {
            Trace.WriteLine("RCP.Init Completed");
        }
        /// <summary>
        /// 현재의 레시피 데이터를 백업한다.
        /// </summary>

        private static bool AddRecipeItem(FA.DEF.eRecipeCategory a_MainCatagory, string a_strSubCategory, RecipeItemBase a_Value)
        {
            if (TotalRcpItemList.ContainsKey(a_Value.iKey) == false)
            {
                TotalRcpItemList.Add(a_Value.iKey, a_Value);
                TotalRcpItemListBak.Add(a_Value.iKey, a_Value);
                if (RcpItemList.ContainsKey(a_MainCatagory) == false)
                {
                    RcpItemList.Add(a_MainCatagory, new Dictionary<string, List<RecipeItemBase>>());
                    RcpItemList[a_MainCatagory].Add(a_strSubCategory, new List<RecipeItemBase>());
                    RcpItemList[a_MainCatagory][a_strSubCategory].Add(a_Value);
                    return true;
                }
                else
                {
                    if (RcpItemList[a_MainCatagory].ContainsKey(a_strSubCategory) == false)
                    {
                        RcpItemList[a_MainCatagory].Add(a_strSubCategory, new List<RecipeItemBase>());
                        RcpItemList[a_MainCatagory][a_strSubCategory].Add(a_Value);
                        return true;
                    }
                    else
                    {
                        RcpItemList[a_MainCatagory][a_strSubCategory].Add(a_Value);
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool AddRecipeItemGroup(RecipeItemGroup a_value)
        {
            if (RcpItemGroupList.ContainsKey(a_value.eMainRcpCategory) == false)
            {
                RcpItemGroupList.Add(a_value.eMainRcpCategory, new Dictionary<string, List<RecipeItemGroup>>());
                RcpItemGroupList[a_value.eMainRcpCategory].Add(a_value.strSubCategory, new List<RecipeItemGroup>());
                RcpItemGroupList[a_value.eMainRcpCategory][a_value.strSubCategory].Add(a_value);
                return true;
            }
            else
            {
                if (RcpItemGroupList[a_value.eMainRcpCategory].ContainsKey(a_value.strSubCategory) == false)
                {
                    RcpItemGroupList[a_value.eMainRcpCategory].Add(a_value.strSubCategory, new List<RecipeItemGroup>());
                    RcpItemGroupList[a_value.eMainRcpCategory][a_value.strSubCategory].Add(a_value);
                    return true;
                }
                else
                {
                    RcpItemGroupList[a_value.eMainRcpCategory][a_value.strSubCategory].Add(a_value);
                    return true;
                }
            }

        }
        public static void Backup(bool a_bInitiProc)
        {
            foreach (KeyValuePair<int, RecipeItemBase> Item in TotalRcpItemList)
            {              
                TotalRcpItemListBak[Item.Key].Value = Item.Value.Value;
                TotalRcpItemListBak[Item.Key].eValueUnit = Item.Value.eValueUnit;
            }
        }
        public static void Restore()
        {
            foreach (KeyValuePair<int, RecipeItemBase> Item in TotalRcpItemList)
            {
                Item.Value.Value = TotalRcpItemListBak[Item.Key].Value;
                Item.Value.eValueUnit = TotalRcpItemListBak[Item.Key].eValueUnit;
            }
        }

        public static RecipeItemBase GetRecipeItem(int a_iKey)
        {
            //if (CheckValid() == false) return null;


            if (TotalRcpItemList.ContainsKey(a_iKey) == true)
                return TotalRcpItemList[a_iKey];
            return null;
        }

        private static void DoLoad(string a_strPath, bool a_bInitProc)
        {
            if (File.Exists(a_strPath))
            {
                using (StreamReader sr = new StreamReader(a_strPath, Encoding.UTF8))
                {
                    string line = "";
                    
                    RecipeItemBase item = null;
                    Type pType = null;
										Assembly pAssembly=null;
										Type pAssemlyType=null;
                    string SubCategory;
                    string[] words;
                    FA.DEF.eRecipeCategory eItem;
                    FA.DEF.eUnit eValueUnit;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length < 1) continue;

                        words = line.Split(new char[] {'|'},StringSplitOptions.RemoveEmptyEntries);
                        if (words.Length < 8) continue;


                     
                        eValueUnit = FA.DEF.eUnit.none;
                        Enum.TryParse(words[0], out eItem);
                        if (Enum.IsDefined(typeof(FA.DEF.eRecipeCategory), eItem))
                        {
                            if (RcpItemList.ContainsKey(eItem) == false)
                                continue;
                        }
                        int nKey = -1;

												if (!int.TryParse(words[1], out nKey)) continue;

												if (TotalRcpItemList.ContainsKey(nKey) == false) continue;
                                                                      
                        if (RcpItemList[eItem].ContainsKey(words[2].Trim())== false) continue;


                        // eCategory | Key | Category | Caption | Value | eUnit | Axis | ValueType                                               
                        item = null;
                        pType = null;
                        eValueUnit = FA.DEF.eUnit.none;
                        item = TotalRcpItemList[nKey];
                        if (!a_bInitProc && item.bIsInitialProc) continue;
											
                        pType = GetType(words[7].TrimEnd(' '));
                        if (pType == null) continue;
                        var ValueConvertor = TypeDescriptor.GetConverter(pType);
                        var SetValue = ValueConvertor.ConvertFromInvariantString(words[4]);
                        item.Value = SetValue;
                        item.SettingValue=SetValue;
                        Enum.TryParse(words[5], out eValueUnit);
                        item.eValueUnit = eValueUnit;
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
				public static Type GetType(string typeName)
				{
						var type = Type.GetType(typeName);
						if (type != null) return type;
						foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
						{
								type = a.GetType(typeName);
								if (type != null)
										return type;
						}
						return null;
				}
				private static void DoSave(string a_strPath, bool a_bInitProc)
        {
						try
						{
								using (StreamWriter sw = new StreamWriter(a_strPath, false, Encoding.UTF8))
								{
										//		1		2		3		  4		   5	   6	  7	       8
										//eCategory | Key | Category | Caption | Value | eUnit | Axis | ValueType
										const string fmtP = " | {0:D3} | {1} | {2} | {3} | {4} | {5:D3} | {6}";


										sw.WriteLine("-------------------------------------------------------------------------------------------");
										sw.WriteLine("#" + fmtP, "KEY", "CATEGORY", "CAPTION", "VALUE", "UNIT", "AXIS", "ValueType");
										sw.WriteLine("-------------------------------------------------------------------------------------------");

										foreach (KeyValuePair<int, RecipeItemBase> Item in TotalRcpItemList)
										{
												if (Item.Value.IsSettingValueChanged)
														Item.Value.SyncValueFromSettingValue();
												Item.Value.WriteFileSave(sw);
										}

								}
						}
						catch (Exception Ex)
						{

						}
           
        }
        public static void LoadFromFile(string a_strPath, bool a_bInitProc)
        {
            DoLoad(a_strPath, a_bInitProc);
            Backup(a_bInitProc);
        }
        public static void SaveToFile(string a_strPath, bool a_bInitProc)
        {
            DoSave(a_strPath, a_bInitProc);
            Backup(a_bInitProc);
        }
        public static string GetPreSaveConfirmMSG()
        {
            StringBuilder strConfirmMSG = new StringBuilder();
            foreach (KeyValuePair<int, RecipeItemBase> Item in TotalRcpItemList)
            {
                if(Item.Value.IsSettingValueChanged)
                strConfirmMSG.Append(string.Format("[{0}][{1}]{2} : {3} -> {4} {5}\n",
                                     Item.Value.eMainRcpCategory.ToString(),
                                     Item.Value.strSubCategory,
                                     Item.Value.strCaption,
                                     Item.Value.eValueUnit== FA.DEF.eUnit.percent ? (Item.Value.GetValue<double>()*100.0):Item.Value.Value,
                                     Item.Value.eValueUnit== FA.DEF.eUnit.percent ? (Item.Value.GetSettingValue<double>()*100.0):Item.Value.SettingValue,
                                     Item.Value.eValueUnit!=FA.DEF.eUnit.none?Item.Value.eValueUnit.ToString():""));
            }
            return strConfirmMSG.ToString();
        }

        public static void Write_To(FA.DEF.eRecipeCategory a_eRcpCategory, FA.DEF.eRcpSubCategory a_eSubCategory, DataGridView a_grid)
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
         
            if (RcpItemList.ContainsKey(a_eRcpCategory) == false)
                return;
            if (RcpItemList[a_eRcpCategory].ContainsKey(a_eSubCategory.ToString()) == false)
                return;

            var list = RcpItemList[a_eRcpCategory][a_eSubCategory.ToString()];
            if (list.Count < 1)
                return;
            RecipeItemBase Item = null;
            for (int j = 0; j < list.Count; j++)
            {
                Item = (list[j] as RecipeItemBase);
                if (Item != null && Item.Value != null)
                {
                    a_grid[4, j].Value = (list[j] as RecipeItemBase).Value;

                }
            }
        }
        public static void Read_From(FA.DEF.eRecipeCategory a_eRcpCategory, FA.DEF.eRcpSubCategory a_eSubCategory, DataGridView a_grid)
        {
         
            if (RcpItemList.ContainsKey(a_eRcpCategory) == false)
                return;
            if (RcpItemList[a_eRcpCategory].ContainsKey(a_eSubCategory.ToString()) == false)
                return;

            var list = RcpItemList[a_eRcpCategory][a_eSubCategory.ToString()];
            if (list.Count < 1)
                return;

            for (int j = 0; j < list.Count; j++)
            {
                if (a_grid[4, j].Value != null)
                {
                    (list[j] as RecipeItemBase).Value = a_grid[4, j].Value;
                }
            }
        }
    }
}
