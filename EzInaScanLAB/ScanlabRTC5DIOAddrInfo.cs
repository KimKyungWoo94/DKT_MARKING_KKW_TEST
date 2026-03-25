using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO.ScanLAB
{
    public sealed class ScanlabRTC5DIOAddrInfo:IOAddrInfo
    {
        private int m_iCardNo;
        private int m_iPosition;
        private bool m_bLaserConnector;
        public int iCardNo{  get {return m_iCardNo; }}
        public int iPosition {get {return m_iPosition; } }
        public bool bLaserConnector {get { return m_bLaserConnector;} }
        public void SetCardNo(int a_value)
        {
            m_iCardNo=a_value;
        }
        public void SetPosition(int a_value)
        {
            m_iPosition=a_value;
        }
        public void SetIsLaserConnector(bool a_value)
        {
            m_bLaserConnector=a_value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">iModuleNumber, iPosition </param>
        public ScanlabRTC5DIOAddrInfo(params string[] parameters)
            : base(parameters)
        {
            if(parameters.Length>0)
            {   
                int bLaserConnector=0; 
                int.TryParse(parameters[0],out m_iCardNo);               
                int.TryParse(parameters[1],out m_iPosition);   
                int.TryParse(parameters[2],out bLaserConnector);   
                m_bLaserConnector=bLaserConnector>0;        
            }
            else
            {
                m_iCardNo=-1;
                m_iPosition=-1;
                m_bLaserConnector=false;
            }
            
        }
    }
}
