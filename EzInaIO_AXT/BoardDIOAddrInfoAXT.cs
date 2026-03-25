using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EzIna.IO.AXT
{

    [Serializable]
    public sealed class BoardDIOAddrInfoAXT:IOAddrInfo
    {       

        private int m_iModuleNumber;
        private int m_iPosition;
        public int iModuleNumber{  get {return m_iModuleNumber; }}
        public int iPosition {get {return m_iPosition; } }       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">iModuleNumber, iPosition </param>
        public BoardDIOAddrInfoAXT(params string[] parameters)
            : base(parameters)
        {
            if(parameters.Length>0)
            {                
                int Temp=0;
                int.TryParse(parameters[0],out Temp);
                m_iModuleNumber=Temp;
                int.TryParse(parameters[1],out Temp);
                m_iPosition=Temp;

            }
            else
            {             
                m_strParamList=new string [] {m_iModuleNumber.ToString(),m_iPosition.ToString(),"","",""};   
                m_iModuleNumber=-1;
                m_iPosition=-1;
            }
            
        }

    
        
    }
}
