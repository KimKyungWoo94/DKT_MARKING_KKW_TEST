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
    public class CylinderIOData : IXmlSerializable
    {
        private string m_strForwardSensorID;
        private string m_strBackwardSensorID;
        private string m_strForwardSolID;
        private string m_strBackwardSolID;
        private string m_strID;
        private string m_strDesrc;
        private bool m_bForwardSersorCheck;
        private bool m_bBackwardSersorCheck;
        private CylinderType m_OperationType;       
        private int  m_iSolCheckDelay;
        internal CylinderIOData()
        {                   
            m_bForwardSersorCheck = true;
            m_bBackwardSersorCheck = true;
            m_strID = "";
            m_strDesrc = "";
            m_OperationType = CylinderType.SINGLE;
            m_iSolCheckDelay=100;//ms
          
        }
        internal CylinderIOData(string a_strID, string a_strDesrc,CylinderType a_Type)
        {          
            m_bForwardSersorCheck = true;
            m_bBackwardSersorCheck = true;
            m_strID = "";
            m_strDesrc = "";
            m_OperationType = a_Type;   
             m_iSolCheckDelay=100;//ms       
        }
        public CylinderIOData(string a_ForwardSensorID,
                              string a_BackWordSensorID,
                              string a_ForwardSolID,
                              string a_BackWordSolID,
                              string a_strID, 
                              string a_strDesrc,
                              bool   a_bForwardSensorCheck,
                              bool   a_bBackwardSensorCheck,
                              int    a_iSolCheckDelay,
                              CylinderType a_Type )
        {
            m_strForwardSensorID    =  a_ForwardSensorID;
            m_strBackwardSensorID   =  a_BackWordSensorID;
            m_strForwardSolID       =  a_ForwardSolID;
            m_strBackwardSolID      =  a_BackWordSolID;
            
            m_strID = a_strID;
            m_strDesrc = a_strDesrc;
            m_bForwardSersorCheck = a_bForwardSensorCheck;
            m_bBackwardSersorCheck = a_bBackwardSensorCheck;
            m_iSolCheckDelay=a_iSolCheckDelay;
            m_OperationType = a_Type;          
        }
        public string strID
        {
            get { return m_strID; }
            set { m_strID = value; }
        }
        public string strDesrc
        {
            get {return m_strDesrc;}
            set {m_strDesrc=value;}
        }
        public string strForwardSensorID
        {
            get { return m_strForwardSensorID;}
        }
        public string strForwardSolID
        {
            get { return m_strForwardSolID;}
        }
        public string strBackwardSensorID
        {
            get { return m_strBackwardSensorID;}
        }
        public string strBackwardSolID
        {
            get { return m_strBackwardSolID;}
        }
        public CylinderType OperationType
        {
            get { return m_OperationType;} 
            set { m_OperationType=value;}
        }
        public int SolCheckDelay
        {
            get {return m_iSolCheckDelay; }
            set { m_iSolCheckDelay=value;}
        }
        public bool IsForwardSensorCheck
        {
            get { return m_bForwardSersorCheck;}
            set {m_bForwardSersorCheck=value; }
        }
        public bool IsBackwardSensorCheck
        {
            get { return m_bBackwardSersorCheck;}
            set {m_bBackwardSersorCheck=value; }
        }
       
        public XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
            if (reader.HasAttributes)
            {
                int iTemp=0;
                this.m_strID = reader.GetAttribute("ID");
                this.m_strDesrc = reader.GetAttribute("Desrc");                
                int.TryParse(reader.GetAttribute("ForwardSensorCheck"), out iTemp);
                this.m_bForwardSersorCheck=Convert.ToBoolean(iTemp);
                int.TryParse(reader.GetAttribute("BackwardSensorCheck"), out iTemp);
                this.m_bBackwardSersorCheck=Convert.ToBoolean(iTemp);
                Enum.TryParse(reader.GetAttribute("Type").ToUpper(), out this.m_OperationType);
                int.TryParse(reader.GetAttribute("SolCheckDelay"),out this.m_iSolCheckDelay);
            }
         
            int i = 0;            
            using (var innerReader = reader.ReadSubtree())
            {
                while (innerReader.ReadToFollowing("IOSet"))
                {
                    if(innerReader.HasAttributes)
                    {
                        m_strForwardSensorID=innerReader.GetAttribute("ForwardSensorID");
                        m_strBackwardSensorID=innerReader.GetAttribute("BackwardSensorID");
                        m_strForwardSolID=innerReader.GetAttribute("ForwardSolID");
                        m_strBackwardSolID=innerReader.GetAttribute("BackwardSolID");
                    }
                }
                innerReader.Close();
            }                       
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Cylinder");
            writer.WriteAttributeString("ID", this.m_strID);
            writer.WriteAttributeString("Desrc", this.m_strDesrc);
            writer.WriteAttributeString("ForwardSensorCheck", this.m_bForwardSersorCheck == true ? "1" : "0");
            writer.WriteAttributeString("BackwardSensorCheck", this.m_bBackwardSersorCheck == true ? "1" : "0");
            writer.WriteAttributeString("Type", m_OperationType.ToString());
            writer.WriteAttributeString("SolCheckDelay",this.m_iSolCheckDelay.ToString());
            writer.WriteStartElement("IOSet");
            writer.WriteAttributeString("ForwardSensorID",m_strForwardSensorID);
            writer.WriteAttributeString("BackwardSensorID",m_strBackwardSensorID);
            writer.WriteAttributeString("ForwardSolID",m_strForwardSolID);
            writer.WriteAttributeString("BackwardSolID",m_strBackwardSolID);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        public static void WriteXmlComment(XmlWriter writer)
        {
            StringBuilder Comment = new StringBuilder();
            writer.WriteComment("Cylinder Example");
            writer.WriteComment("SensorCheck = 0 or 1");
            writer.WriteComment("Type = Single or double");
            Comment.Append("Cylinder");           
            Comment.Append(" ID=\"1\"");
            Comment.Append(" Desrc=\"1\"");
            Comment.Append(" ForwardSensorCheck=\"1\"");
            Comment.Append(" BackwardSensorCheck=\"1\"");
            Comment.Append(" Type=\"1\"");
            Comment.Append(" SolCheckDelay=\"1\"");                        
            writer.WriteComment(Comment.ToString());
            writer.WriteComment("Cylinder SubElement");          
            writer.WriteComment("Input Matching IO ID");
            Comment.Clear();
            Comment.Append("IOSet");
            Comment.Append(" ForwardSensorID=\"\"");
            Comment.Append(" BackwardSensorID=\"\"");
            Comment.Append(" ForwardSolID=\"\"");
            Comment.Append(" BackwardSolID=\"\"");
            writer.WriteComment(Comment.ToString());
        }
    }
}
