using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.MF
{
    public class RecipeItemGroup
    {
        private List<RecipeItemBase> m_RecipeItemList;
        protected FA.DEF.eRecipeCategory m_eMainRcpCategory;
        public FA.DEF.eRecipeCategory eMainRcpCategory
        {
            get { return m_eMainRcpCategory; }
        }
        protected string m_strSubCategory;
        public string strSubCategory
        {
            get { return m_strSubCategory; }
        }

        public bool AddItem(RecipeItemBase a_Value)
        {
            if (m_RecipeItemList.Count <= 0)
            {
                m_RecipeItemList.Add(a_Value);
            }
            else
            {
                var TypeFilter =m_RecipeItemList.Where(x=>x.GetType()==a_Value.GetType()).ToList();
                if( TypeFilter.Count>0  )
                {
                    var FilterMainCategory = m_RecipeItemList.Where(x => x.eMainRcpCategory == a_Value.eMainRcpCategory).ToList();
                    if (FilterMainCategory.Count > 0)
                    {
                        var FilterSubCategory = FilterMainCategory.Where(x => x.strSubCategory == a_Value.strSubCategory).ToList();
                        if (FilterSubCategory.Count > 0)
                        {
                            m_RecipeItemList.Add(a_Value);
                            return true;
                        }
                    }
                }                              
            }
            return false;
        } 
				public object GetValue(uint a_IDX)
				{
						if (m_RecipeItemList.Count > 0 && a_IDX < m_RecipeItemList.Count)
						{
								return m_RecipeItemList[(int)a_IDX].Value;
						}
						return null;
				}
				public RecipeItemBase GetRecipeValue(uint a_IDX)
				{
						if (m_RecipeItemList.Count > 0 && a_IDX < m_RecipeItemList.Count)
						{
								return m_RecipeItemList[(int)a_IDX];
						}
						return null;
				}
				public List<RecipeItemBase> RecipeItemList
				{
						get { return m_RecipeItemList;}
				}
        public RecipeItemGroup(FA.DEF.eRecipeCategory a_MainCategory,string a_strSubCategory)
        {
            m_RecipeItemList=new List<RecipeItemBase>();
            m_eMainRcpCategory=a_MainCategory;
            m_strSubCategory=a_strSubCategory;
        }
        public RecipeItemGroup(FA.DEF.eRecipeCategory a_MainCategory,string a_strSubCategory,params RecipeItemBase[] args)
        {
            m_RecipeItemList = new List<RecipeItemBase>();
            m_eMainRcpCategory = a_MainCategory;
            m_strSubCategory = a_strSubCategory;
            for (int i=0;i<args.Length;i++)
            {
                AddItem(args[i]);
            }
        }
    }
}
