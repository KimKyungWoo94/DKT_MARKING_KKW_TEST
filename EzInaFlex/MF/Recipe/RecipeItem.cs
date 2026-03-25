using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.FA;

namespace EzIna.MF
{
    public sealed class RecipeItem : RecipeItemBase
    {
        public override double DisplayValue
        {
            get
            {
                return 0.0;
            }

            set
            {

            }
        }

        public override double DisplaySettingValue
        {
            get
            {
                return 0.0;
            }

            set
            {

            }
        }

        public RecipeItem(DEF.eRecipeCategory a_MainCategory, string a_strSubCategory, string a_strCaption, int a_iKey, object a_InitValue, DEF.eUnit a_ValueUnit) : base(a_MainCategory, a_strSubCategory, a_strCaption, a_iKey, a_InitValue, a_ValueUnit)
        {
        }
    }
}
