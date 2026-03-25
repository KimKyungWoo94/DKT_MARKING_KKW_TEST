using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Commucation.SocketCom
{
    /// <summary>
    /// 
    /// </summary>
    public struct stSocketSetting
    {
       /// <summary>
       /// 
       /// </summary>
        public String strIPName;
        /// <summary>
        /// 
        /// </summary>
        public int   iPort;
        /// <summary>
        /// 
        /// </summary>
        public int ReadTimeout;
        /// <summary>
        /// 
        /// </summary>
        public int WriteTimeout;
        /// <summary>
        /// 
        /// </summary>
      
        public void Init()
        {
            Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            strIPName = "127.0.0.1";
            iPort = 8000;
            ReadTimeout = 2000;
            WriteTimeout = 2000;            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public stSocketSetting Clone()
        {
            stSocketSetting pRet=new stSocketSetting();
            pRet.strIPName=this.strIPName;
            pRet.iPort=this.iPort;
            pRet.ReadTimeout=this.ReadTimeout;
            pRet.WriteTimeout=this.WriteTimeout;
            return pRet;
        }
    }
}
