using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EzIna.Scanner
{
    [Serializable]
    [XmlRoot("ScannerDevices")]
    public class ScanlabRTC5LinkDataPaser:IXmlSerializable
    {
        private string []  m_strCorrectionTableFileNames=new string[4];
        private List<ScanlabRTC5LinkData> m_DeviceList=new List<ScanlabRTC5LinkData>();

        public List<ScanlabRTC5LinkData> DeviceList
        {
            get
            {
                return m_DeviceList;
            }
        }
        public void Test()
        {
            m_DeviceList.Add(new ScanlabRTC5LinkData());
        }
        public void AddLinkData(ScanlabRTC5LinkData a_Item)
        {
            m_DeviceList.Add(a_Item);
        }
        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(ScanlabRTC5LinkDataPaser));
            using (var stream = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                serializer.Serialize(stream, this);
            }
        }
        public static ScanlabRTC5LinkDataPaser Load(string path)
        {
            var serializer = new XmlSerializer(typeof(ScanlabRTC5LinkDataPaser));
            using (var stream = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                return serializer.Deserialize(stream) as ScanlabRTC5LinkDataPaser;
            }
        }

        public XmlSchema GetSchema()
        {
           return null;
        }

        public void ReadXml(XmlReader reader)
        {          
            reader.ReadToFollowing("ScannerList");
            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
            {
                ReadXmlScannerData(reader);
            }
        }
        private void ReadXmlScannerData(XmlReader reader)
        {
            m_DeviceList.Clear();
            while (reader.ReadToFollowing("ScannerItem"))
            {
                if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                {
                    ScanlabRTC5LinkData ptemp = new ScanlabRTC5LinkData();
                    ptemp.ReadXml(reader);
                    m_DeviceList.Add(ptemp);
                }
            }
        }
        public void WriteXml(XmlWriter writer)
        {
           writer.WriteComment("Table1~4 : FileName With out Path");
           writer.WriteComment(@"Path Fixed \System\CFG\Scanner\RTC5\ in Execute path");          
           writer.WriteStartElement("ScannerList");
           foreach(ScanlabRTC5LinkData Item in m_DeviceList)
           {
                Item.WriteXml(writer);
           }
           writer.WriteEndElement();
        }
    }
}
