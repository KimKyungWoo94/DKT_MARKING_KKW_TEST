using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
		public class CorrMappingMgr
		{
				private Dictionary<string,CorrMappingItem> m_DicMappingList;
				public CorrMappingMgr()
				{
						m_DicMappingList=new Dictionary<string, CorrMappingItem>();
				}
				public void AddMappingList(string a_strTableName,CorrMappingItem a_value)
				{
						if (m_DicMappingList.ContainsKey(a_strTableName))
						{
								m_DicMappingList[a_strTableName] = a_value;
						}
						else
						{
								m_DicMappingList.Add(a_strTableName,a_value);
						}														
				}
				public CorrMappingItem this[string a_strKey]
				{
						get
						{
								if(m_DicMappingList.ContainsKey(a_strKey))
								{
										return m_DicMappingList[a_strKey];
								}
								return null;
						}
				}
		}
}
