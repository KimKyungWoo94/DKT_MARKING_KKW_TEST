using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using EzIna.IO;
using System.Xml.Serialization;
using System.ComponentModel;

namespace EzIna.IO
{  
    public class DI:BaseIO
    {       
        private bool m_bDisPlaybool=false;
        private GetDigitValueFunc m_DelGetValue = delegate { return false; };

#if SIM
        private bool m_bValue;
#endif
        internal DI()
        {
            Initialize();
        }
        public DI(IOData a_Data,Type a_pDeivce,IOAddrInfo a_Addr):base(a_Data,a_pDeivce,a_Addr)
        {
            Initialize();
        }
        public DI(string a_strID, string a_strDesrc,EContact a_EContactType, Type a_Devicetype,IOAddrInfo a_Addr):base(a_strID,a_strDesrc,a_EContactType,a_Devicetype,a_Addr)
        {
            Initialize();
        }      
        public bool OrginValue
        {
#if SIM
            get { return m_bValue;}
            set { m_bValue=value;}
#else
            get { return m_DelGetValue(m_IOAddrInfo,m_enumValueFrom); } 
            set { }          
#endif
        }      
        public bool Value
        {
            get { return OrginValue ==(this.m_ECContactType == EContact.A);}
        }
        protected void Initialize()
        {
           m_IOType=IOType.DI;
        }           
        public void SettingGetDIFunc(GetDigitValueFunc a_Func)
        {
            m_DelGetValue=a_Func;
            m_bDisPlaybool=OrginValue;
        }

      
    }
}
