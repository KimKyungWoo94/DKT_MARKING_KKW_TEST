using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.MF
{
	#region [OptionItem]
	public class OptItem
	{
		private int m_iKey;
		public static List<List<OptItem>> vecItems = new List<List<OptItem>>();
		public static List<List<OptItem>> vecItemsBak = new List<List<OptItem>>();

		public FA.DEF.eRcpCategory		m_eOptCategory;
		public FA.DEF.eRcpSubCategory	m_eOptSubCategory;
		public string					m_strName;
		public bool						m_bState;
		public bool						m_bUseValue;
		public string					m_strValue;
		public FA.DEF.eUnit				m_eUnit;


		public OptItem(FA.DEF.eRcpCategory a_eCategory, FA.DEF.eRcpSubCategory a_eSubCategory, string a_strName, bool a_bState, bool a_bUseValue = false, string a_strValue = "", FA.DEF.eUnit a_eUnit = FA.DEF.eUnit.none)
		{

			if (vecItems.Count == 0)
			{
				for(int i = 0; i < (int)FA.DEF.eRcpSubCategory.Max; i++)
					vecItems.Add(new List<OptItem>());	
			}

			m_eOptCategory		= a_eCategory;
			m_eOptSubCategory	= a_eSubCategory;
			m_strName			= a_strName;
			m_bState			= a_bState;
			m_bUseValue			= a_bUseValue;
			m_strValue			= a_strValue;
			m_eUnit				= a_eUnit;

			vecItems[(int)a_eSubCategory].Add(this);
		}
		public void On()
		{
			m_bState = true;
		}
		public void Off()
		{
			m_bState = false;
		}
		public void Toggle()
		{
			m_bState = !m_bState;
		}

		public static implicit operator bool(OptItem item)
		{
			return item.m_bState;
		}

		public static implicit operator string(OptItem item)
		{
			return item.m_strValue;
		}

		public static implicit operator int(OptItem item)
		{
			int nRtn = 0;
			try
			{
				nRtn = Convert.ToInt32(item.m_strValue);
			}
			catch(Exception exc)
			{
				
			}
			return nRtn;
		}

		public static implicit operator double(OptItem item)
		{
			double dRtn = 0.0;
			try
			{
				dRtn = Convert.ToDouble(item.m_strValue);
			}
			catch (Exception exc)
			{

			}
			return dRtn;
		}
		public static implicit operator float(OptItem item)
		{
			float fRtn = 0;
			try
			{
				fRtn = Convert.ToSingle(item.m_strValue);
			}
			catch (Exception exc)
			{

			}
			return fRtn;
		}


	}
	#endregion

	#region [Options]
	public static class OPT
	{
		public static List<List<OptItem>> vecItems = OptItem.vecItems;

		public static void Init()
		{
			Trace.WriteLine("MF.Options.Initialize Completed");
		}
		public static void Init(string a_strPath)
		{
			FA.LOG.Debug("SYSTEM", "OPT.Init Completed");
			//LoadSettings(a_strPath);
			Trace.WriteLine("OPT.Init Completed");
		}

		public static void LoadFromFile(string a_strPath, bool a_bInitProc)
		{
			IniFile ini = new IniFile(a_strPath);
			for(int i = 0; i < (int)FA.DEF.eRcpSubCategory.Max; i++)
			{
				foreach (OptItem item in vecItems[i])
				{
					if(a_bInitProc)
					{
						if(item.m_eOptCategory != FA.DEF.eRcpCategory.InitialProcess)
							continue;
					}
					else
					{
						if (item.m_eOptCategory == FA.DEF.eRcpCategory.InitialProcess)
							continue;
					}


					string s = ini.Read(item.m_eOptSubCategory.ToString(), 
					item.m_strName, 
					item.m_bState.ToString() + '|' + item.m_bUseValue.ToString() + '|' + item.m_strValue + '|' + item.m_eUnit.ToString());
					string[] words = s.Split('|');
					if (words.Length > 3)
					{
						bool.TryParse(words[0], out item.m_bState);
						bool.TryParse(words[1], out item.m_bUseValue);
						item.m_strValue = words[2];
						item.m_eUnit = (FA.DEF.eUnit)Enum.Parse(typeof(FA.DEF.eUnit), words[3], true);
					}
				}
			}
		}
		public static void SaveToFile(string a_strPath, bool a_bInitProc)
		{
			IniFile ini = new IniFile(a_strPath);
			for (int i = 0; i < (int)FA.DEF.eRcpSubCategory.Max; i++)
			{
				foreach (OptItem item in vecItems[i])
				{
					if (a_bInitProc)
					{
						if (item.m_eOptCategory != FA.DEF.eRcpCategory.InitialProcess)
							continue;
					}
					else
					{
						if (item.m_eOptCategory == FA.DEF.eRcpCategory.InitialProcess)
							continue;
					}

					ini.Write(item.m_eOptSubCategory.ToString(), 
					item.m_strName, 
					item.m_bState.ToString() + '|' + item.m_bUseValue.ToString() + '|' + item.m_strValue + '|' + item.m_eUnit.ToString());
				}
			}
		}

		public static void Read_Form(FA.DEF.eRcpSubCategory a_eSubCategory, DataGridView a_grd)
		{
			try
			{
				int iRow = 0;
				foreach(OptItem item in vecItems[(int)a_eSubCategory])
				{
					bool.TryParse(a_grd[0, iRow].Value.ToString(), out item.m_bState);
					item.m_strName = a_grd[1, iRow].Value.ToString();

					if (item.m_bUseValue)
					{
						item.m_strValue = a_grd[2, iRow].Value.ToString();
                        foreach (FA.DEF.eUnit eItem in Enum.GetValues(typeof(FA.DEF.eUnit)))
                        {
                            if(FA.DEF.GetUnitString(eItem).Equals(a_grd[3, iRow].Value.ToString()))
                            {
                                item.m_eUnit = eItem;
                            }
                        }					
					}
					iRow++;
				}
			}
			catch(Exception exc)
			{

			}
		}

		public static void Write_To(DataGridView a_grd, FA.DEF.eRcpSubCategory a_eSubCategory)
		{
			

			a_grd.RowCount = Math.Max(vecItems[(int)a_eSubCategory].Count, a_grd.RowCount);

			for(int i = 0; i < vecItems[(int)a_eSubCategory].Count; i++)
			{
				a_grd[0,i].Value = vecItems[(int)a_eSubCategory][i].m_bState;
				a_grd[1,i].Value = vecItems[(int)a_eSubCategory][i].m_strName;
				if(vecItems[(int)a_eSubCategory][i].m_bUseValue)
				{
 					a_grd[2, i].Value = vecItems[(int)a_eSubCategory][i].m_strValue;
 					a_grd[3, i].Value = FA.DEF.GetUnitString((vecItems[(int)a_eSubCategory][i].m_eUnit));
				}
				else
				{
					a_grd[2, i].Value = "";
					a_grd[3, i].Value = "";
				}
			}

			a_grd.ClearSelection(); // 선택하면 배경색이 바뀌니까 아예 없애자
			//a_grd.AutoResizeColumns();
		}
	}
	
	#endregion
}
