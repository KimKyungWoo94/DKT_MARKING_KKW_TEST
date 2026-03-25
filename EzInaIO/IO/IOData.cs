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
    public class IOData : IXmlSerializable
    {
        protected IOType  m_enumIOType; 
        protected EContact m_enumEContactType;
        protected string  m_strDeviceType;
        protected string  m_strID;
        protected string  m_strDesrc;
        protected string  [] m_strAddrInfoParam=new string[5];
        
        internal IOData()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_Type">DI,DO,AI,AO IOType</param>
        /// <param name="a_ECType">A(Normal Open),B(Normal Close)</param>
        /// <param name="a_strID"></param>
        /// <param name="a_strDesrc"></param>
        /// <param name="a_strDevicetype"></param>
        /// <param name="a_strAddrInfoPararms"></param>
        public IOData(IOType a_Type,EContact a_ECType,string a_strID, string a_strDesrc, string a_strDevicetype,params string [] a_strAddrInfoPararms)
        {
            m_enumIOType=a_Type;
            m_enumEContactType=a_ECType;
            m_strID = a_strID;
            m_strDesrc = a_strDesrc;
            m_strDeviceType = a_strDevicetype;   
            m_strAddrInfoParam=a_strAddrInfoPararms;     
        }       
        public string ID
        {
            get { return m_strID; }
        }
        /// <summary>
        /// IO설명을 가져온다
        /// </summary>
        public string Description
        {
            get { return m_strDesrc; }
        }
        public string DeviceType
        {
            get { return m_strDeviceType;}
        }
        public string [] strAddrInfoParams
        {
            get { return m_strAddrInfoParam; }
        }
        public IOType enumIOType
        {
            get { return m_enumIOType;}
        }
        public EContact enumEContact
        {
            get { return m_enumEContactType;}
        }
        public override string ToString()
        {
            return this.m_strID;
        }
        public virtual XmlSchema GetSchema()
        {
            return null;
        }
        public virtual void ReadXml(XmlReader reader)
        {
            if (reader.HasAttributes)
            {
                Enum.TryParse(reader.GetAttribute("IOType").ToUpper(),out m_enumIOType); 
                Enum.TryParse(reader.GetAttribute("EContact").ToUpper(),out m_enumEContactType);                 
                m_strID = reader.GetAttribute("ID");
                m_strDesrc = reader.GetAttribute("Desrc");
                m_strDeviceType=reader.GetAttribute("Device");
                for(int i=0;i<m_strAddrInfoParam.Length;i++)
                {
                    m_strAddrInfoParam[i]=reader.GetAttribute(string.Format("Addr{0:D2}",i));
                }
            }
        }
        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("IO");
            writer.WriteAttributeString("IOType", this.m_enumIOType.ToString());
            writer.WriteAttributeString("EContact",this.m_enumEContactType.ToString());
            writer.WriteAttributeString("ID", this.m_strID);
            writer.WriteAttributeString("Desrc", this.m_strDesrc);
            writer.WriteAttributeString("Device", this.m_strDeviceType);
            for(int i=0;i<m_strAddrInfoParam.Length;i++)
            {
                writer.WriteAttributeString(string.Format("Addr{0:D2}",i), m_strAddrInfoParam[i]);
            }
            writer.WriteEndElement();
        }
        public static void WriteXmlComment(XmlWriter writer)
        {
            StringBuilder Comment= new StringBuilder();
            writer.WriteComment("IO Example");
            Comment.Append("IO IOType=\"DI\"");
            Comment.Append(" EContact=\"A\"");
            Comment.Append(" ID=\"1\"");
            Comment.Append(" Desrc=\"1\"");
            Comment.Append(" Device=\"1\"");
            Comment.Append(" Addr00=\"1\"");
            Comment.Append(" Addr01=\"1\"");
            Comment.Append(" Addr02=\"1\"");
            Comment.Append(" Addr03=\"1\"");
            Comment.Append(" Addr04=\"1\"");
            writer.WriteComment(Comment.ToString());
        }
       
    }
}
