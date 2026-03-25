using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EzIna.Motion
{

    [Serializable]
    [XmlRoot("Motion")]  
    public class MotionAxisDataPaser:IXmlSerializable
    {

        private List<MotionAxisLinkData> m_AxisLinkLists=new List<MotionAxisLinkData>();
        public List<MotionAxisLinkData> AxisDataLinkList {get { return m_AxisLinkLists;} }

        public MotionAxisLinkData this[int a_IDX]
        {
            get
            {
                if (a_IDX >= 0 && a_IDX < m_AxisLinkLists.Count)
                {
                    return m_AxisLinkLists[a_IDX];
                }
                return null;
            }
        }
        public int AxisListCount
        {
            get {  return m_AxisLinkLists.Count;}
        }
        public void AddLinkData(string a_strAxisID,string a_strDeviceType)
        {
            if(m_AxisLinkLists.Any(item=> item.strAxisID==a_strAxisID)==false)
            {
                m_AxisLinkLists.Add(new MotionAxisLinkData(a_strAxisID,a_strDeviceType));
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(MotionAxisDataPaser));            
            using (var stream = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                serializer.Serialize(stream, this);               
            }
        }
        public static MotionAxisDataPaser Load(string path)
        {          
            var serializer = new XmlSerializer(typeof(MotionAxisDataPaser));
            using (var stream = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                return serializer.Deserialize(stream) as MotionAxisDataPaser;
            }
        }
        public XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
            reader.ReadToFollowing("AxisItems");            
            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
            {
                 ReadXmlListAxisData(reader);
            }
        }
        private void ReadXmlListAxisData(XmlReader reader)
        {
            m_AxisLinkLists.Clear();                    
            while(reader.ReadToFollowing("AxisItem"))
            {                
               if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
               {
                    MotionAxisLinkData ptemp=new MotionAxisLinkData();
                    ptemp.ReadXml(reader);
                    m_AxisLinkLists.Add(ptemp);
                }
                //reader.Read();
            }           
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteComment("ID         : Input Axis Index");
            writer.WriteComment("Device(Motion)Class");
            writer.WriteComment("Aerotech   : EzIna.Motion.CMotionA3200");
            writer.WriteComment("AJIN       : EzIan.Motion.CMotionAXT");                      
            writer.WriteStartElement("AxisItems");
            foreach(MotionAxisLinkData item in m_AxisLinkLists)
            {                
                item.WriteXml(writer);
            }
            writer.WriteEndElement();
        }
    }

    [Serializable]    
    public class MotionAxisLinkData:IXmlSerializable
    {
				string  m_strAxisName;
        string  m_strAxisID;
        string  m_strDeviceType;
				public string strAxisName
				{
						get { return m_strAxisName;}
						set
						{
								m_strAxisName=value;
						}
				}
        public  string  strAxisID
        {
            get { return m_strAxisID; }
            set
            {
                m_strAxisID=value;
            }
        }
        public  string  strDeviceType
        {
            get { return m_strDeviceType; }
            set
            {
                m_strDeviceType=value;
            }
        }

        public MotionAxisLinkData()
        {

        }
        public MotionAxisLinkData(string a_strAxisID,string a_strDeviceType)
        {
            m_strAxisID=a_strAxisID;
            m_strDeviceType=a_strDeviceType;          
        }
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if(reader.HasAttributes)
            {
								m_strAxisName=reader.GetAttribute("Name");
                m_strAxisID=reader.GetAttribute("ID");
                m_strDeviceType=reader.GetAttribute("Device");                            
            }
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("AxisItem");
						writer.WriteAttributeString("Name",m_strAxisName);
            writer.WriteAttributeString("ID",m_strAxisID);
            writer.WriteAttributeString("Device",m_strDeviceType);
            writer.WriteEndElement();
        }
     
    }
}
