using EzIna.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace EzIna.IO
{
    [Serializable]
    public class DO:BaseIO
    {

     
        private GetDigitValueFunc m_DelGetValue = delegate { return false; };
        private SetDigitValueFunc m_DelSetValue ;
#if SIM
        private bool m_bValue;
#endif 
        internal DO()
        {
            Initialize();
        }
        public DO(IOData a_Data, Type a_pDeivce, IOAddrInfo a_Addr):base(a_Data.ID,a_Data.Description,a_pDeivce,a_Addr)
        {
            Initialize();
        }
        public DO(string a_strID, string a_strDesrc, Type a_Devicetype,IOAddrInfo a_Addr):base(a_strID,a_strDesrc,a_Devicetype,a_Addr)
        {
            Initialize();
        }

        protected void Initialize()
        {
           m_IOType=IOType.DO;
        }   
        public bool Value
        {
#if SIM
            get { return m_bValue;}
            set { m_bValue=value; }
#else
            get { return m_DelGetValue(m_IOAddrInfo,m_enumValueFrom);}
            set { m_DelSetValue(m_IOAddrInfo,value,m_enumValueFrom);}
#endif
        }       
        public void SettingGetDOFunc(GetDigitValueFunc a_Func)
        {
            m_DelGetValue=a_Func;
        } 
        public void SettingSetDOFunc(SetDigitValueFunc a_Func)
        {
            m_DelSetValue=a_Func;
        } 

    }
}
