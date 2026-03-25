using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Commucation
{
    /// <summary>
    /// 
    /// </summary>
    public enum enumPacketMode
    {
        /// <summary>
        /// Command , SetValue
        /// </summary>
        SetOnly,
        /// <summary>
        /// Getting Only
        /// </summary>
        GettingOnly,
        /// <summary>
        /// Both
        /// </summary>
        Both,
    }
    /// <summary>
    /// 
    /// </summary>
    
    /// <summary>
    /// Enum packet 등록 
    /// </summary>

    public class PacketAttribute : System.Attribute
    {
       
        private string m_strCMD;
        private string m_strSetMark;
        private string m_strGettingMark;
        private bool   m_bSetEchoEnable;
        private bool   m_bGettingEchoEnable;
        private bool   m_bExistSetFeedbackPacket;
        private bool   m_bExistGettingFeedbackPacket;
        enumPacketMode     m_PacketMode;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strCmd">Command String</param>
        /// <param name="a_strSetMark">Set Command Mark String</param>
        /// <param name="a_strGettingMark">Getting Command Mark String</param>
        /// <param name="a_bSetEchoEnable">Set ReceiveData Include Command</param>
        /// <param name="a_bGettingEchoEnable">Getting ReceiveData Include Command</param>
        /// <param name="a_bExistSetFeedbackPacket">Set ReceiveData Include Command</param>
        /// <param name="a_bExistGettingFeedbackPacket">Getting ReceiveData Include Command</param>
        /// <param name="a_packetMode">SetOnly , S </param>
        public PacketAttribute(
                            string a_strCmd,
                            string a_strSetMark,
                            string a_strGettingMark,
                            bool a_bSetEchoEnable,
                            bool a_bGettingEchoEnable,
                            bool a_bExistSetFeedbackPacket,
                            bool a_bExistGettingFeedbackPacket,
                            enumPacketMode a_packetMode                            
                            )
        {
            m_strCMD = a_strCmd;
            m_strSetMark=a_strSetMark;
            m_strGettingMark=a_strGettingMark;
            m_bSetEchoEnable=a_bSetEchoEnable;
            m_bGettingEchoEnable=a_bGettingEchoEnable;
            m_PacketMode=a_packetMode;
            m_bExistSetFeedbackPacket=a_bExistSetFeedbackPacket;
            m_bExistGettingFeedbackPacket=a_bExistGettingFeedbackPacket;
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCMD
        {
            get { return m_strCMD; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strSetMark
        {
            get { return m_strSetMark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strGettingMark
        {
            get { return m_strGettingMark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSetEchoEnable
        {
            get { return m_bSetEchoEnable; }

        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsGettingEchoEnable
        {
            get { return m_bGettingEchoEnable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ExistSetFeedbackPacket
        {
            get { return m_bExistSetFeedbackPacket; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ExistGettingFeedbackPacket
        {
            get { return m_bExistGettingFeedbackPacket; }
        }
        /// <summary>
        /// 
        /// </summary>
        public enumPacketMode PacketMode
        {
            get{ return m_PacketMode;   }
        }        
    }
    

    class Common
    {     
        public static int MAX_BUFF =    1024;
        public const byte ASCII_CR =    0x0D;
        public const byte ASCII_LF =    0x0A;
        public const byte ASCII_STX=    0x02;
        public const byte ASCII_ETX=    0x03;
        public const byte ASCII_ACK=    0x06;
        public const byte ASCII_NAK=    0x15;

        public static string ConfigFolderPath
        {
            get
            {
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                strPath = System.IO.Path.GetDirectoryName(strPath);
                string strConfigFolderPath = Path.GetFullPath(Path.Combine(strPath, @"..\System\CFG\"));

                DirectoryInfo DirInfo = new DirectoryInfo(strConfigFolderPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();
                return strConfigFolderPath;

            }
        }

        public static string ConfigCommFileFullName
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strConfigVisionFolderPath = Path.GetFullPath(Path.Combine(strPath, @"Comm"));
                string strConfigVisionFilePath = Path.GetFullPath(Path.Combine(strPath, @"Comm\ComSetting.ini"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigVisionFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();

                FileInfo FileInfor = new FileInfo(strConfigVisionFilePath);
                if (FileInfor.Exists == false)
                {
                    using (FileStream s = FileInfor.Create())
                    {
                        s.Close();
                    }
                }
                return strConfigVisionFilePath;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum enumCommMethod
    {
        /// <summary>
        /// 
        /// </summary>
        SERIAL,
        /// <summary>
        /// 
        /// </summary>
        SOCKET,
    }
    /// <summary>
    /// 
    /// </summary>
    public enum enumPacketType
    {
        /// <summary>
        /// 
        /// </summary>
        SetValue,
        /// <summary>
        /// 
        /// </summary>
        GetValue,
    }
    /// <summary>
    /// 
    /// </summary>
   
    public class stCommPacket
    {
        /// <summary>
        ///  SendPacket string Byte convert require
        /// </summary>
        public string StrSendMsg;
        /// <summary>
        /// SendPacket string Byte convert require
        /// </summary>
        public string StrReciveMsg;
        /// <summary>
        /// 
        /// </summary>
				public string StrCMDValue;
				/// <summary>
				///
				/// </summary>
        public string StrSetValue;
        /// <summary>
        /// 0: Send mode // 1:Recive mode
        /// </summary>
        public bool bRecvMode; 
        /// <summary>
        /// 0:Recive mode // 1:Send mode
        /// </summary>
        public bool bSendMode;
        /// <summary>
        ///  0 : No Receive , 1: Receive Exist
				public bool bSendingWait;
        /// </summary>
        public bool bRecvUse;
        /// <summary>
        /// 0 : nonEcho , 1: Echo Command
        /// </summary>
        public bool bEchoCMD;

        /// <summary>
        /// 
        /// </summary>
        public UInt64 nTimeCal;
        /// <summary>
        /// 
        /// </summary>
        public int nSize;
        /// <summary>
        /// Retry Count
        /// </summary>
        public uint nReceiveReTryCnt;
        /// <summary>
        /// 
        /// </summary>
        public int iCMD;
        /// <summary>
        /// 
        /// </summary>
        public enumPacketType PacketType;
        /// <summary>
        /// 
        /// </summary>
        public stCommPacket()
        {
            Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            iCMD = -1;
            StrSendMsg = "";
            StrReciveMsg = "";
            StrSetValue = "";
						StrCMDValue="";
            bRecvMode = false;
            bSendMode = false;
						bSendingWait=false;
            bRecvUse = false;
            bEchoCMD=false;
            nTimeCal = 0;
            nSize = 0;
            nReceiveReTryCnt = 0;
            PacketType=enumPacketType.GetValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public stCommPacket Clone()
        {
            stCommPacket Ret=new stCommPacket();

            Ret.iCMD                = this.iCMD ;           
            Ret.StrSendMsg          = this.StrSendMsg  ;    
            Ret.StrReciveMsg        = this.StrReciveMsg ;   
            Ret.StrSetValue         = this.StrSetValue ;    
            Ret.bRecvMode           = this.bRecvMode;       
            Ret.bSendMode           = this.bSendMode ;  
						Ret.bSendingWait				= this.bSendingWait;    
            Ret.bRecvUse            = this.bRecvUse;        
            Ret.bEchoCMD            = this.bEchoCMD ;       
            Ret.nTimeCal            = this.nTimeCal ;       
            Ret.nSize               = this.nSize ;          
            Ret.nReceiveReTryCnt    = this.nReceiveReTryCnt;
            Ret.PacketType          = this.PacketType ;
            return Ret;     
        }
    }
}
