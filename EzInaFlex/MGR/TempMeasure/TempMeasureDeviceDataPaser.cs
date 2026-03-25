using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EzIna.TempMeasure
{
    [Serializable]
    [XmlRoot("TempMeasureDevices")]
    public class TempMeasureDeviceDataPaser:IXmlSerializable
    {
        private List<TempMeasureDeviceLinkData> m_Devicelist=new List<TempMeasureDeviceLinkData>();
        public List<TempMeasureDeviceLinkData> DeviceList {get { return m_Devicelist;} }

        public TempMeasureDeviceLinkData this[int a_IDX]
        {
            get
            {
                if (a_IDX >= 0 && a_IDX < m_Devicelist.Count)
                {
                    return m_Devicelist[a_IDX];
                }
                return null;
            }
        }
        public int DeviceListCount
        {
            get {  return m_Devicelist.Count;}
        }
        public void AddLinkData(string a_strID,string a_strDeviceType)
        {
            if(m_Devicelist.Any(item=> item.strID==a_strID)==false)
            {
                m_Devicelist.Add(new TempMeasureDeviceLinkData(a_strID,a_strDeviceType));
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(TempMeasureDeviceDataPaser));            
            using (var stream = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                serializer.Serialize(stream, this);               
            }
        }
        public static TempMeasureDeviceDataPaser Load(string path)
        {          
            var serializer = new XmlSerializer(typeof(TempMeasureDeviceDataPaser));
            using (var stream = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                return serializer.Deserialize(stream) as TempMeasureDeviceDataPaser;
            }
        }
        public XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
            reader.ReadToFollowing("TempMeasureDeviceList");
            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
            {
                ReadXmlLaserData(reader);           
            }                             
        }
        private void ReadXmlLaserData(XmlReader reader)
        {
            m_Devicelist.Clear();                                                        
            while (reader.ReadToFollowing("TempMeasureDeviceItem"))
            {
                if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                {                 
                        TempMeasureDeviceLinkData ptemp = new TempMeasureDeviceLinkData();
                        ptemp.ReadXml(reader);
                        m_Devicelist.Add(ptemp);                 
                }
            }            
        }
        public void WriteXml(XmlWriter writer)
        {

            
            writer.WriteComment("Device(TempMeasure)Class");
            writer.WriteComment("OMEGA     : EzIna.TempMeasure.OMEGA.CN8PT");           
            writer.WriteComment("CommMethod : SERIAL or SOCKET");
            writer.WriteComment("SerialCom");
            StringBuilder strtmp=new StringBuilder();
            strtmp.Append("Parity : ");
            foreach(System.IO.Ports.Parity Item in Enum.GetValues(typeof(System.IO.Ports.Parity)))
            {
                strtmp.Append(string.Format("{0} ,",Item.ToString()));
            }
            writer.WriteComment(strtmp.ToString());
            strtmp.Clear();
            strtmp.Append("StopBit : ");
            foreach(System.IO.Ports.StopBits Item in Enum.GetValues(typeof(System.IO.Ports.StopBits)))
            {
                strtmp.Append(string.Format("{0} ,",Item.ToString()));
            }
            writer.WriteComment(strtmp.ToString());
            writer.WriteStartElement("TempMeasureDeviceList");
            foreach(TempMeasureDeviceLinkData item in m_Devicelist)
            {                
                item.WriteXml(writer);
            }
            writer.WriteEndElement();
        }
    }

    [Serializable]    
    public class TempMeasureDeviceLinkData:IXmlSerializable
    {
        string  m_strID;
        string  m_strDeviceType;
        int     m_iChannelNo;
        Commucation.enumCommMethod m_CommMethod;
        Commucation.SerialCom.stSerialSetting m_SerialCommInfo;
        Commucation.SocketCom.stSocketSetting m_SocketInfo;
        int    m_iPacketDoneTimeout;

         public  int     iChannelNo
        {
            get { return m_iChannelNo; }
            set { m_iChannelNo=value;}
        }
         public  string  strID
        {
            get { return m_strID; }
            set
            {
                m_strID=value;
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
        public  int     iPacketDoneTimeout
        {
            get {return m_iPacketDoneTimeout; }
            set
            {
                m_iPacketDoneTimeout=value;
            }
        }       
        public  Commucation.enumCommMethod CommMethod
        {
            get { return m_CommMethod;}
            set
            {
                m_CommMethod=value;
            }
        }
        public  Commucation.SerialCom.stSerialSetting SerialComInfo
        {
            get { return  m_SerialCommInfo; }
            set
            {
                m_SerialCommInfo=value;
            }
        }
        public  Commucation.SocketCom.stSocketSetting SocketInfo
        {
            get { return  m_SocketInfo; }
            set
            {
                m_SocketInfo=value;
            }
        }
        public TempMeasureDeviceLinkData()
        {
                
        }
        public TempMeasureDeviceLinkData(string a_strID,string a_strDeviceType)
        {
            m_strID=a_strID;
            m_strDeviceType=a_strDeviceType;          
        }
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string strTemp="";
            if(reader.HasAttributes)
            {               
                m_strID=reader.GetAttribute("ID");
                m_strDeviceType=reader.GetAttribute("Device");   
                int.TryParse(reader.GetAttribute("ChannelNo"),out m_iChannelNo);                
                Enum.TryParse(reader.GetAttribute("CommMethod").ToUpper(),out m_CommMethod);
                int.TryParse(reader.GetAttribute("PacketDoneTimeout"),out m_iPacketDoneTimeout);                                                                   
            }
            using (var innerReader = reader.ReadSubtree())
            {
                if(innerReader.ReadToFollowing("SerialCom"))
                {
                    if(innerReader.HasAttributes)
                    {
                       m_SerialCommInfo.strComPortName = innerReader.GetAttribute("PortName");   
                       int.TryParse( innerReader.GetAttribute("BaudRate"),out m_SerialCommInfo.nBaudRate  ) ;
                       int.TryParse( innerReader.GetAttribute("DataBits"),out m_SerialCommInfo.nDataBits  ) ;
                       Enum.TryParse(innerReader.GetAttribute("Parity"), out m_SerialCommInfo.Parity);
                       Enum.TryParse(innerReader.GetAttribute("StopBits"), out m_SerialCommInfo.StopBits);
                       if (m_SerialCommInfo.StopBits == System.IO.Ports.StopBits.None)
                       {
                           m_SerialCommInfo.StopBits = System.IO.Ports.StopBits.One;
                       }
                       int.TryParse( innerReader.GetAttribute("ReadTimeout"),out m_SerialCommInfo.ReadTimeout  ) ;
                       int.TryParse( innerReader.GetAttribute("WriteTimeout"),out m_SerialCommInfo.WriteTimeout  ) ;
                    }
                }
                if(innerReader.ReadToFollowing("SocketCom"))
                {
                    if(innerReader.HasAttributes)
                    {
                          m_SocketInfo.strIPName = innerReader.GetAttribute("IPAddr");
                          int.TryParse( innerReader.GetAttribute("PortNum"),out m_SocketInfo.iPort  ) ;                         
                    }
                }
                innerReader.Close();
            }                                                                               
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("TempMeasureDeviceItem");
            writer.WriteAttributeString("ID",m_strID);
            writer.WriteAttributeString("Device",m_strDeviceType);
            writer.WriteAttributeString("ChannelNo",m_iChannelNo.ToString());
            writer.WriteAttributeString("CommMethod",m_CommMethod.ToString().ToUpper());
            writer.WriteAttributeString("PacketDoneTimeout", m_iPacketDoneTimeout.ToString());  
            writer.WriteStartElement("SerialCom");
            writer.WriteAttributeString("PortName", m_SerialCommInfo.strComPortName);
            writer.WriteAttributeString("BaudRate", m_SerialCommInfo.nBaudRate.ToString());
            writer.WriteAttributeString("DataBits", m_SerialCommInfo.nDataBits.ToString());
            writer.WriteAttributeString("Parity", m_SerialCommInfo.Parity.ToString());
            writer.WriteAttributeString("StopBits", m_SerialCommInfo.StopBits.ToString());
            writer.WriteAttributeString("ReadTimeout", m_SerialCommInfo.ReadTimeout.ToString());
            writer.WriteAttributeString("WriteTimeout", m_SerialCommInfo.WriteTimeout.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("SocketCom");
            writer.WriteAttributeString("IPAddr", m_SocketInfo.strIPName);
            writer.WriteAttributeString("PortNum", m_SocketInfo.iPort.ToString()); 
            writer.WriteEndElement();    
            writer.WriteEndElement();
        }
     
    }
}
