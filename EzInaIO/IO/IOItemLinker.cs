using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EzIna.IO
{
    [Serializable]
    [XmlRoot("IOItems")]
    public class IOItemLinker : IXmlSerializable
    {

        protected List<EzIna.IO.IOData> m_IOList = new List<EzIna.IO.IOData>();
        protected List<EzIna.IO.CylinderIOData> m_CylinderList = new List<CylinderIOData>();
        public List<EzIna.IO.IOData> IOList
        {
            get { return m_IOList;}

        }
        public List<EzIna.IO.CylinderIOData> CylinderList
        {
            get { return m_CylinderList;}
        }

        public void Test()
        {
            m_IOList.Add(new IOData(IOType.DI,EContact.A, "1", "Test1", "EzIna.IO.AXT.BoardTypeDIO"));
            m_IOList.Add(new IOData(IOType.DI,EContact.A, "2", "Test3", "EzIna.IO.AXT.BoardTypeDIO"));
            m_IOList.Add(new IOData(IOType.DO,EContact.A, "3", "Test2", "EzIna.IO.AXT.BoardTypeDIO"));
            m_IOList.Add(new IOData(IOType.DO,EContact.A, "4", "Test2", "EzIna.IO.AXT.BoardTypeDIO"));

            m_CylinderList.Add(new CylinderIOData(
                "1",
                "2",
                "3",
                "4",
                "1",
                "Test",
                true,
                true,
                100,
                CylinderType.DOUBLE
                ));
            m_CylinderList.Add(new CylinderIOData(
                "1",
                "2",
                "3",
                "4",
                "2",
                "Test",
                true,
                true,
                100,
                CylinderType.SINGLE
                ));           
            // CylinderList.Add(new CylinderSingle());
            // CylinderList.Add(new Cylinderdouble());
        }
        public XmlSchema GetSchema()
        {
            return null;
        }
        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(IOItemLinker));
            using (var stream = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                serializer.Serialize(stream, this);
            }
        }
        public static IOItemLinker Load(string path)
        {
            var serializer = new XmlSerializer(typeof(IOItemLinker));
            using (var stream = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                return serializer.Deserialize(stream) as IOItemLinker;
            }
        }
     
        private void ReadXmlListIO(XmlReader reader)
        {
            m_IOList.Clear();
            IOData pTemp = null;          
            reader.ReadToFollowing("IOList");
            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
            {
                using (var innerReader = reader.ReadSubtree())
                {
                    while (innerReader.ReadToFollowing("IO"))
                    {
                        pTemp = new IOData();
                        pTemp.ReadXml(innerReader);
                        m_IOList.Add(pTemp); 
                    }
                    innerReader.Close();
                }
            }
        }
        private void ReadXmlCylinderList(XmlReader reader)
        {
            m_CylinderList.Clear();
            CylinderIOData pTemp = null;
            reader.ReadToFollowing("CylinderList");
            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
            {
                 using (var innerReader = reader.ReadSubtree())
                {
                    while (innerReader.ReadToFollowing("Cylinder"))
                    {
                       pTemp = new CylinderIOData();                       
                       pTemp.ReadXml(innerReader);
                       m_CylinderList.Add(pTemp);    
                    }
                    innerReader.Close();
                }
            }                                             
        }
        public void ReadXml(XmlReader reader)
        {
            ReadXmlListIO(reader);
            ReadXmlCylinderList(reader);
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteComment("IO Type    : DI , DO , AI , AO");
            writer.WriteComment("EContact   : A or B ");
            writer.WriteComment("Device(IO)");
            writer.WriteComment("AXT");     
            writer.WriteComment("IO Board           :   EzIna.IO.AXT.BoardTypeDIO");
            writer.WriteComment("[AddrInfo          :   Addr00 - Module No , Addr01 - Channel No]"); 
            writer.WriteComment("IO Board           :   EzIna.IO.AXT.BoardTypeAIO"); 
            writer.WriteComment("[AddrInfo          :   Addr00 - ChannelNo , Addr01 - MinVoltage , Addr02 - MaxVoltage]"); 
            writer.WriteComment("IO in MotionBoard  :   EzIna.IO.AXT.MotionAXT_DIO");     
            writer.WriteComment("[AddrInfo          :   Addr00 - AxisNo , Addr01 - Channel No ]"); 
            writer.WriteComment("Aerotech");                       
            writer.WriteComment("IO in MotionBoard  :   EzIna.IO.AEROTECH.MotionA3200_IO");    
            writer.WriteComment("[AddrInfo DIO      :   Addr00 - AxisNo , Addr01 - Channel No ]");                                             
            writer.WriteComment("[AddrInfo AIO      :   Addr00 - AxisNo , Addr01 - Channel No , Addr02 - MinVoltage , Addr03 - MaxVoltgae]");             
            writer.WriteComment("ScanLab");
            writer.WriteComment("[AddrInfo DIO      :   Addr00 - CardNo , Addr01 - Channel No(0~15)]");
            IOData.WriteXmlComment(writer);            
            writer.WriteStartElement("IOList");                        
            foreach (EzIna.IO.IOData item in m_IOList)
            {
                item.WriteXml(writer);
            }
            writer.WriteEndElement();
            CylinderIOData.WriteXmlComment(writer);
            writer.WriteStartElement("CylinderList");
            foreach (EzIna.IO.CylinderIOData item in m_CylinderList)
            {
                item.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

    }
}
