using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace  EzIna.IO.AEROTECH
{
    class MotionA3200_AIOAddrInfo:IOAddrInfo
    {
        int m_iAxisIndex;
        int m_iChannelNum;
        double m_fMinVolt;
        double m_fMaxVolt;

        public int  iAxisIndex {get { return m_iAxisIndex;} }
        public int iChannelNum { get { return m_iChannelNum; } }
        public double fMinVolt { get { return m_fMinVolt; } }
        public double fMaxVolt { get { return m_fMaxVolt; } }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"> iChannelNum, fMinVolt, fMaxVolt</param>
        public MotionA3200_AIOAddrInfo(params string[] parameters)
            : base(parameters)
        {

            if (parameters.Length > 0)
            {
              
                int iTemp;
                double fTemp;
                int.TryParse(parameters[0], out iTemp);
                m_iAxisIndex=iTemp;
                int.TryParse(parameters[1], out iTemp);
                m_iChannelNum = iTemp;
                double.TryParse(parameters[2], out fTemp);
                m_fMinVolt = fTemp;
                double.TryParse(parameters[3], out fTemp);
                m_fMaxVolt = fTemp;
            }
            else
            {
                m_strParamList=new string [] {m_iAxisIndex.ToString(),m_iChannelNum.ToString(),m_fMinVolt.ToString("0.000"),m_fMaxVolt.ToString("0.000"),""};   
                m_iAxisIndex=-1;
                m_iChannelNum=-1;
                m_fMinVolt=0;
                m_fMaxVolt=0;
            }
        }

    }
}
