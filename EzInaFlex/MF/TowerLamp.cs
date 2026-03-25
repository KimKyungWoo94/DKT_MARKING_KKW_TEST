using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace EzIna.MF
{
	public enum eLampAction { Off		, On		, Blink							}
	//public enum eLampMode	{ Stop		, Run		, Error		, Initial, Max		}
	public enum eLampItem	{ Red		, Yellow	, Green		, Max				}
	public enum eBuzzerItem { b1 = 3	, b2 = 4	, b3 = 5	, Max				}

	public static class TOWERLAMP
	{
		#region Base
		/// <summary>
		/// Array of [TPMStatus(Stop/Run/Initial/Error), Red/Yellow/Green]
		/// </summary>
		
		public static eLampAction[,] Lamps = new eLampAction[(int)FA.DEF.eRunMode.Max, (int)eLampItem.Max];
		/// <summary>
		/// Array of [TPMStatus(Stop/Run/Initial/Error), b1/b2/b3]
		/// </summary>
		public static bool[,] Buzzers = new bool[(int)FA.DEF.eRunMode.Max, (int)eBuzzerItem.Max];

		public static void Init(string iniFile)
		{
			LoadFromFile(iniFile);
		}

		private static void AssignFrom(FA.DEF.eRunMode a_eMode, string s)
		{ 
			string[] words = s.Split(',');

			foreach(eLampItem item in Enum.GetValues(typeof(eLampItem)))
			{
				if(item == eLampItem.Max)
					break;
				Enum.TryParse(words[(int)item], out Lamps[(int)a_eMode, (int)item]);
			}

			foreach (eBuzzerItem item in Enum.GetValues(typeof(eBuzzerItem)))
			{
				if (item == eBuzzerItem.Max)
					break;
				bool.TryParse(words[(int)item], out Buzzers[(int)a_eMode, (int)item -3]);
			}
		}

		public static void LoadFromFile(string cfgFile)
		{
			if (!File.Exists(cfgFile)) SaveToFile(cfgFile);

			IniFile ini = new IniFile(cfgFile);

			// Red/Yellow/Green //b1/b2/b3
			foreach(FA.DEF.eRunMode item in Enum.GetValues(typeof(FA.DEF.eRunMode)))
			{
				if(item == FA.DEF.eRunMode.Max)
					break;
				switch(item)
				{
					case FA.DEF.eRunMode.Jam:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Blink,Off,Off,false,false,false"));
						break;
					case FA.DEF.eRunMode.Init:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "off,Blink,Blink,false,false,false"));
						break;
					case FA.DEF.eRunMode.Ready:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,Off,On,false,false,false"));
						break;
					case FA.DEF.eRunMode.ToRun:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,Off,On,false,false,false"));
						break;
					case FA.DEF.eRunMode.Run:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,Off,On,false,false,false"));
						break;
					case FA.DEF.eRunMode.ToStop:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,Off,On,false,false,false"));
						break;
					case FA.DEF.eRunMode.Stop:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,On,On,false,false,false"));
						break;
					case FA.DEF.eRunMode.ToManual:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,Off,On,false,false,false"));
						break;
					case FA.DEF.eRunMode.Manual:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,Off,On,false,false,false"));
						break;
					case FA.DEF.eRunMode.ToStopManual:
						AssignFrom(item, ini.Read("Lamp", item.ToString(), "Off,Off,On,false,false,false"));
						break;
					default:
						break;


				}
			}
		}

		private static string MakeString(FA.DEF.eRunMode a_eLampMode)
		{
			return string.Format("{0},{1},{2},{3},{4},{5}",
				Lamps	[(int)a_eLampMode, (int)eLampItem.Red		].ToString(),
				Lamps	[(int)a_eLampMode, (int)eLampItem.Yellow	].ToString(),
				Lamps	[(int)a_eLampMode, (int)eLampItem.Green		].ToString(),
				Buzzers	[(int)a_eLampMode, (int)eBuzzerItem.b1		].ToString(), 
				Buzzers	[(int)a_eLampMode, (int)eBuzzerItem.b2		].ToString(),
				Buzzers	[(int)a_eLampMode, (int)eBuzzerItem.b3		].ToString());
		}

		public static void SaveToFile(string filename)
		{
			IniFile ini = new IniFile(filename);

			foreach(FA.DEF.eRunMode item in Enum.GetValues(typeof(FA.DEF.eRunMode)))
			{
				if(item == FA.DEF.eRunMode.Max)
					continue;

				ini.Write("Lamp", item.ToString(), MakeString(item));
			}
		}

		public static void WriteTo(DataGridView grdDst)
		{
			if (grdDst.ColumnCount != 7)
			{
				System.Drawing.Color ForeColor			= System.Drawing.Color.Black;
				System.Drawing.Color BackColor			= System.Drawing.Color.White;
				System.Drawing.Color SelectionBackColor = System.Drawing.Color.SteelBlue;
				System.Drawing.Color SelectionForeColor = System.Drawing.Color.White;
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
				grdDst.DefaultCellStyle.Font	= new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
				grdDst.ReadOnly					= false;
				grdDst.AllowUserToAddRows		= false;
				grdDst.AllowUserToDeleteRows	= false;
				grdDst.AllowUserToOrderColumns	= false;
				grdDst.AllowUserToResizeColumns = false;
				grdDst.AllowUserToResizeRows	= false;
				grdDst.ColumnHeadersVisible		= true;
				grdDst.RowHeadersVisible		= false;
				grdDst.MultiSelect				= false;
				grdDst.EditMode					= DataGridViewEditMode.EditOnKeystrokeOrF2;
				grdDst.SelectionMode			= DataGridViewSelectionMode.CellSelect;
				grdDst.BackgroundColor			= System.Drawing.Color.White;
				grdDst.GridColor				= System.Drawing.Color.SteelBlue;

				grdDst.Columns.Clear();
				grdDst.ColumnHeadersHeightSizeMode	= DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
				grdDst.ColumnHeadersHeight			= 30;
				grdDst.RowTemplate.Height			= 30;
				grdDst.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
				#endregion [Common]


				grdDst.Columns.Add(new DataGridViewTextBoxColumn());
				grdDst.Columns.Add(new DataGridViewComboBoxColumn());
				grdDst.Columns.Add(new DataGridViewComboBoxColumn());
				grdDst.Columns.Add(new DataGridViewComboBoxColumn());
				grdDst.Columns.Add(new DataGridViewCheckBoxColumn());
				grdDst.Columns.Add(new DataGridViewCheckBoxColumn());
				grdDst.Columns.Add(new DataGridViewCheckBoxColumn());

				for (int i = 0; i < grdDst.ColumnCount; i++)
					grdDst.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

				grdDst.Columns[0].HeaderText = "MODE";
				grdDst.Columns[1].HeaderText = "RED";
				grdDst.Columns[2].HeaderText = "YELLOW";
				grdDst.Columns[3].HeaderText = "GREEN";
				grdDst.Columns[4].HeaderText = "BUZZER 1";
				grdDst.Columns[5].HeaderText = "BUZZER 2";
				grdDst.Columns[6].HeaderText = "BUZZER 3";

				grdDst.Columns[0].ReadOnly = true;
				for (int i = 1; i <= 3; i++)
				{
					((DataGridViewComboBoxColumn)grdDst.Columns[i]).Items.AddRange("Off", "On", "Blink");
				}

				//grdDst.Columns[0].ReadOnly = true;
				//grdDst.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				//grdDst.Columns[0].FillWeight = 100;

				grdDst.CellClick += (object sender, DataGridViewCellEventArgs e) =>
				{
					DataGridView grd = (DataGridView)sender;

					if (e.ColumnIndex.In((int)eBuzzerItem.b1 + 1, (int)eBuzzerItem.b2 + 1, (int)eBuzzerItem.b3 + 1) && e.RowIndex >= 0)
					{
						bool state = (bool)grd[e.ColumnIndex, e.RowIndex].Value;
						grd[e.ColumnIndex, e.RowIndex].Value = !state;
						grd.CurrentCell = null;
					}

				};
				grdDst.CellEnter += (object sender, DataGridViewCellEventArgs e) =>
				{
					DataGridView grd = (DataGridView)sender;

					if (e.ColumnIndex.In(1, 2, 3) && e.RowIndex >= 0)
					{
						grd.BeginEdit(true);
						((ComboBox)grd.EditingControl).DroppedDown = true;
					}
				};
			}

			grdDst.RowCount = (int)FA.DEF.eRunMode.Max;
			for (int i = 0; i < grdDst.RowCount; i++)
			    grdDst.Rows[i].SetValues(((FA.DEF.eRunMode)i).ToString(), Lamps[i, 0].ToString(), Lamps[i, 1].ToString(), Lamps[i, 2].ToString(), Buzzers[i, 0], Buzzers[i, 1], Buzzers[i, 2]);


			#region [ Head Setting ]
			//Headers setting
			foreach (DataGridViewColumn col in grdDst.Columns)
			{
				col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
				col.HeaderCell.Style.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			}

			//a_pDataGridView_Modules.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;  
			grdDst.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			grdDst.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.HotTrack;
			grdDst.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
			grdDst.EnableHeadersVisualStyles = false;
			grdDst.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			grdDst.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			//a_pDataGridView_Modules.RowsDefaultCellStyle.BackColor = SystemColors.HotTrack;
			//a_pDataGridView_Modules.RowsDefaultCellStyle.ForeColor = Color.White;
			#endregion [ head Setting ]

			grdDst.FitCells();
			//grdDst.FitRows();
		}

		public static void ReadFrom(DataGridView grdDst)
		{
			for (int i = 0; i < grdDst.RowCount; i++)
			{
				Enum.TryParse((string)grdDst[(int)eLampItem.Red		+ 1, i].Value, out Lamps[i, 0]);
				Enum.TryParse((string)grdDst[(int)eLampItem.Yellow	+ 1, i].Value, out Lamps[i, 1]);
				Enum.TryParse((string)grdDst[(int)eLampItem.Green	+ 1, i].Value, out Lamps[i, 2]);

				Buzzers	[i, (int)eBuzzerItem.b1		] = (bool)grdDst[(int)eBuzzerItem.b1 + 1, i].Value;
				Buzzers	[i, (int)eBuzzerItem.b2		] = (bool)grdDst[(int)eBuzzerItem.b2 + 1, i].Value;
				Buzzers	[i, (int)eBuzzerItem.b3		] = (bool)grdDst[(int)eBuzzerItem.b3 + 1, i].Value;
			}
		}
        public static void ResistLampIO(IO.DO a_LAMP_R,
                                        IO.DO a_LAMP_Y,
                                        IO.DO a_LAMP_G,
                                        IO.DO a_LAMP_BUZZER1,
                                        IO.DO a_LAMP_BUZZER2,
                                        IO.DO a_LAMP_BUZZER3
                                        )
        {
            m_DO_Lamp_R=a_LAMP_R;
            m_DO_Lamp_Y=a_LAMP_Y;
            m_DO_Lamp_G=a_LAMP_G;
            m_DO_Buzzer1=a_LAMP_BUZZER1;
            m_DO_Buzzer2=a_LAMP_BUZZER2;
            m_DO_Buzzer3=a_LAMP_BUZZER3;
        }
		#endregion

		public static volatile bool StartBuzzer = true;

		public static FA.Event_None DoThreadProc;

        private static IO.DO  m_DO_Lamp_R;
        private static IO.DO  m_DO_Lamp_Y;
        private static IO.DO  m_DO_Lamp_G;
        private static IO.DO  m_DO_Buzzer1;
        private static IO.DO  m_DO_Buzzer2;
        private static IO.DO  m_DO_Buzzer3;

        public static bool GetLampStatus(eLampItem a_Value)
        {
            bool bRet=false;
            switch (a_Value)
            {
                case eLampItem.Red:
                    {
                        bRet=m_DO_Lamp_R!=null ? m_DO_Lamp_R.Value: false;
                    }
                    break;
                case eLampItem.Yellow:
                    {
                        bRet =m_DO_Lamp_Y!=null ? m_DO_Lamp_Y.Value: false;
                    }
                    break;
                case eLampItem.Green:
                    {
                        bRet=m_DO_Lamp_G!=null ? m_DO_Lamp_G.Value: false;
                    }
                    break;               
                default:
                    break;
            }
            return bRet;
        }
        public static bool GetBuzzerStatus(eBuzzerItem a_Value)
        {
            bool bRet=false;

            switch (a_Value)
            {
                case eBuzzerItem.b1:
                    {
                          bRet=m_DO_Buzzer1!=null ? m_DO_Buzzer1.Value: false;
                    }
                    break;
                case eBuzzerItem.b2:
                    {
                         bRet=m_DO_Buzzer2!=null ? m_DO_Buzzer2.Value: false;
                    }
                    break;
                case eBuzzerItem.b3:
                    {
                         bRet=m_DO_Buzzer3!=null ? m_DO_Buzzer3.Value: false;
                    }
                    break;               
                default:
                    break;
            }
            return bRet;
        }
        #region [TOWER LAMP]
        public static void DoTowerLamp()
        {
            switch (MF.TOWERLAMP.Lamps[(int)FA.MGR.RunMgr.eRunMode, (int)EzIna.MF.eLampItem.Red])
            {
               case MF.eLampAction.Off		:
                    {
                        if(m_DO_Lamp_R!=null)
                        {
                            m_DO_Lamp_R.Value = false;
                        }                        
                    }
                    break;                    
               case MF.eLampAction.On		:
                    {
                        if (m_DO_Lamp_R != null)
                        {
                            m_DO_Lamp_R.Value = true;;
                        }
                    }
                    break;  
               case MF.eLampAction.Blink	:
                    {
                        if (m_DO_Lamp_R != null)
                        {
                            m_DO_Lamp_R.Value = FLICKER.IsOn; 
                        }
                        
                    }
                    break;                      
            }
            switch (MF.TOWERLAMP.Lamps[(int)FA.MGR.RunMgr.eRunMode, (int)EzIna.MF.eLampItem.Green])
            {
                case MF.eLampAction.Off:
                    {
                        if (m_DO_Lamp_G != null)
                        {
                            m_DO_Lamp_G.Value = false;
                        }
                    }
                    break;
                case MF.eLampAction.On:
                    {
                        if (m_DO_Lamp_G != null)
                        {
                            m_DO_Lamp_G.Value = true; ;
                        }
                    }
                    break;
                case MF.eLampAction.Blink:
                    {
                        if (m_DO_Lamp_G != null)
                        {
                            m_DO_Lamp_G.Value = FLICKER.IsOn;
                        }

                    }
                    break;
            }
            switch (MF.TOWERLAMP.Lamps[(int)FA.MGR.RunMgr.eRunMode, (int)EzIna.MF.eLampItem.Yellow])
            {
                case MF.eLampAction.Off:
                    {
                        if (m_DO_Lamp_Y != null)
                        {
                            m_DO_Lamp_Y.Value = false;
                        }
                    }
                    break;
                case MF.eLampAction.On:
                    {
                        if (m_DO_Lamp_Y != null)
                        {
                            m_DO_Lamp_Y.Value = true; ;
                        }
                    }
                    break;
                case MF.eLampAction.Blink:
                    {
                        if (m_DO_Lamp_Y != null)
                        {
                            m_DO_Lamp_Y.Value = FLICKER.IsOn;
                        }

                    }
                    break;
            }

            if (MF.TOWERLAMP.StartBuzzer)
            {
                if(m_DO_Buzzer1!=null)
                m_DO_Buzzer1.Value = MF.TOWERLAMP.Buzzers[(int)FA.MGR.RunMgr.eRunMode, (int)MF.eBuzzerItem.b1];
            }
            else
            {
                if(m_DO_Buzzer1!=null)
                m_DO_Buzzer1.Value = false;
            }
        }
        #endregion
        public static void ThreadProc()
		{
			//DoThreadProc();
            DoTowerLamp();
		}
	}
}