using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EzIna.IO
{
    [Serializable]
    public class BaseIO:NotifyProperyChangedBase
    {     
        protected IOType m_IOType;
        protected EContact m_ECContactType;
        protected string m_strID;
        protected string m_strDesrc;   
        protected Type m_DeviceType;     
        protected IOAddrInfo m_IOAddrInfo;
        protected enumValueFrom m_enumValueFrom;
        protected int m_iLoadingOrder;
        protected BaseIO()
        {
            m_enumValueFrom=enumValueFrom.GETTING_BUFFER;
        }
        protected BaseIO(IOData a_Data,Type a_Devicetype,IOAddrInfo a_AddrInfo)
        {
            m_strID = a_Data.ID;
            m_strDesrc = a_Data.Description;
            m_ECContactType =a_Data.enumEContact;
            m_DeviceType = a_Devicetype;  
            m_IOAddrInfo= a_AddrInfo;
            m_enumValueFrom=enumValueFrom.GETTING_BUFFER;   
        }
        protected BaseIO(string a_strID, string a_strDesrc,Type a_Devicetype,IOAddrInfo a_AddrInfo)
        {
            m_strID = a_strID;
            m_strDesrc = a_strDesrc;
            m_ECContactType=EContact.A;
            m_DeviceType = a_Devicetype;  
            m_IOAddrInfo= a_AddrInfo;
            m_enumValueFrom=enumValueFrom.GETTING_BUFFER;   
        }
        protected BaseIO(string a_strID, string a_strDesrc,EContact a_EContactType,Type a_Devicetype,IOAddrInfo a_AddrInfo)
        {
            m_strID = a_strID;
            m_strDesrc = a_strDesrc;
            m_DeviceType = a_Devicetype;  
            m_ECContactType=a_EContactType;
            m_IOAddrInfo= a_AddrInfo;
            m_enumValueFrom=enumValueFrom.GETTING_BUFFER;   
        }
        
        public Type DeviceType
        {
            get { return m_DeviceType; }
        }
        public string ID
        {
            get { return m_strID; }
        }
        public int LoadingOrder
        {
            get { return m_iLoadingOrder;}
            set { m_iLoadingOrder=value;}
        }
        /// <summary>
        /// IO설명을 가져온다
        /// </summary>
        public string Description
        {
            get { return m_strDesrc; }
            set
            {               
                 base.CheckPropertyChanged<string>("Description",ref m_strDesrc,value);
            }
        }
        public enumValueFrom ValueFrom
        {
            get { return m_enumValueFrom; }
            set
            {
                m_enumValueFrom=value;
            }
        }
        public EContact EContact
        {
            get { return m_ECContactType;}
            set { base.CheckPropertyChanged<EContact>("EContact",ref m_ECContactType,value);}
        }
        public IOAddrInfo AddressInfo
        {
            get { return m_IOAddrInfo; }
        }
      
        public IOData CreateIOData()
        {
           return new IOData(m_IOType,m_ECContactType, m_strID,m_strDesrc,m_DeviceType.FullName,m_IOAddrInfo.m_strParamList);
        }
        public override string ToString()
        {
            return this.m_strID;
        }
        protected Type GetDeiveType(string typeName)
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

    }
}

