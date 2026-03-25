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
  

    public sealed class BoardAIOAddrInfoAXT : IOAddrInfo
    {

        int m_iChannelNum;
        double m_fMinVolt;
        double m_fMaxVolt;
        public int iChannelNum { get { return m_iChannelNum; } }
        public double fMinVolt
        {
            get { return m_fMinVolt; }
            set { m_fMinVolt=value;
                  m_strParamList[1]=m_fMinVolt.ToString("0.000");
                }
        }
        public double fMaxVolt
        {
            get{ return m_fMaxVolt; }
             set { m_fMaxVolt=value;
                  m_strParamList[1]=m_fMaxVolt.ToString("0.000");
                }
        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"> iChannelNum, fMinVolt, fMaxVolt</param>
        public BoardAIOAddrInfoAXT(params string[] parameters)
            : base(parameters)
        {

            if (parameters.Length > 0)
            {
                int iTemp;
                double fTemp;
                int.TryParse(parameters[0], out iTemp);
                m_iChannelNum = iTemp;
                double.TryParse(parameters[1], out fTemp);
                m_fMinVolt = fTemp;
                double.TryParse(parameters[2], out fTemp);
                m_fMaxVolt = fTemp;
            }
            else
            {
                m_strParamList=new string [] {m_iChannelNum.ToString(),m_fMinVolt.ToString("0.000"),m_fMaxVolt.ToString("0.000"),"",""};
                m_iChannelNum=-1;
                m_fMinVolt=0;
                m_fMaxVolt=0;
            }
        }  
    }
}
