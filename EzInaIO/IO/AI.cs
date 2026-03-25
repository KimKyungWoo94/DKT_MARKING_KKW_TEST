using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO
{
    public class AI:BaseIO
    {
        private GetAnalogValueFunc m_DelGetValue = delegate { return 0.0f; };
        private SetAnalogRangeFunc m_DelSetRange =delegate { };
        internal AI()
        {
            Initialize();
        }
        public AI(IOData a_Data, Type a_pDeivce, IOAddrInfo a_Addr):base(a_Data.ID,a_Data.Description,a_pDeivce,a_Addr)
        {
            Initialize();
        }
        public AI(string a_strID, string a_strDesrc, Type a_Devicetype,IOAddrInfo a_Addr):base(a_strID,a_strDesrc,a_Devicetype,a_Addr)
        {
            Initialize();
        }
        public double Value
        {
#if SIM
            get { return 0.0;}
#else
            get { return m_DelGetValue(m_IOAddrInfo,m_enumValueFrom);}
#endif
        }      
        public void SetRange()
        {
           m_DelSetRange?.Invoke(m_IOAddrInfo);
        }
        protected void Initialize()
        {
           m_IOType=IOType.AI;
        }           
        public void SettingGetAIFunc(GetAnalogValueFunc a_Func)
        {
            m_DelGetValue=a_Func;
        } 
        public void SettingSetRangeAIFunc(SetAnalogRangeFunc a_Func)
        {
            m_DelSetRange=a_Func;
        }
    }
}
