using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace EzIna.IO.AEROTECH
{
    public class MotionA3200_DIOAddrInfo:IOAddrInfo
    {
        int     m_iAxisIndex;
        int     m_iPosition;

        public int    iAxisIndex {get { return m_iAxisIndex; } }
        public int    iPosition {get { return m_iPosition;} }
          /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">iModuleNumber, iPosition </param>
        public MotionA3200_DIOAddrInfo(params string[] parameters)
            : base(parameters)
        {
            if(parameters.Length>0)
            {
                int Temp=0;
                int.TryParse(parameters[0],out Temp);
                m_iAxisIndex=Temp;                
                int.TryParse(parameters[1],out Temp);
                m_iPosition=Temp;
            }
            else
            {
                m_strParamList=new string [] {m_iAxisIndex.ToString(),m_iPosition.ToString(),"","",""};   
                m_iAxisIndex=-1;
                m_iPosition=-1;
            }
            
        }
    
    }
}
