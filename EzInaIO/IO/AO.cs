using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO
{
    public class AO:BaseIO
    {

        private GetAnalogValueFunc m_DelGetValue = delegate { return 0.0; };
        private SetAnalogValueFunc m_DelSetValue ;
        private SetAnalogRangeFunc m_DelSetRange =delegate { };
#if SIM
        private double m_fValue;
        private double m_fSetValue;
#endif
        internal AO()
        {
            Initialize();
        }
        public AO(IOData a_Data, Type a_pDeivce, IOAddrInfo a_Addr):base(a_Data.ID,a_Data.Description,a_pDeivce,a_Addr)
        {
            Initialize();
        }
        public AO(string a_strID, string a_strDesrc, Type a_Devicetype,IOAddrInfo a_Addr):base(a_strID,a_strDesrc,a_Devicetype,a_Addr)
        {
            Initialize();
        }
        protected void Initialize()
        {
           m_IOType=IOType.AO;
        }   
        public double Value
        {
#if SIM
            get { return m_fValue;}
            set { m_fValue=value;}                
#else
            get { return m_DelGetValue(m_IOAddrInfo, m_enumValueFrom); }
            set { m_DelSetValue(m_IOAddrInfo, value, m_enumValueFrom); }
#endif
        }
        public void SetRange()
        {
            m_DelSetRange?.Invoke(m_IOAddrInfo);
        }
        public void SettingGetAOFunc(GetAnalogValueFunc a_Func)
        {
            m_DelGetValue=a_Func;
        } 
        public void SettingSetAOFunc(SetAnalogValueFunc a_Func)
        {
            m_DelSetValue=a_Func;
        }
        public void SettingSetRangeAOFunc(SetAnalogRangeFunc a_Func)
        {
            m_DelSetRange = a_Func;
        }
    }
}
