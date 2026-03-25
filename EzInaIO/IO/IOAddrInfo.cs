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
    public abstract class IOAddrInfo
    {
        public string [] m_strParamList {get; protected set;}

        public IOAddrInfo(params string [] a_ParamList)
        {
            this.m_strParamList=a_ParamList;
        }                   
    }
}
