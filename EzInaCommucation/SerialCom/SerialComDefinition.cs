using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Commucation.SerialCom
{
    /// <summary>
    /// 
    /// </summary>
    public class strPacketCmd:System.Attribute
    {
        private string _strCMD;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strCmd"></param>
        public strPacketCmd(string a_strCmd)
        {
            _strCMD=a_strCmd;
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCMD
        {
            get { return strCMD;}
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class PacketEnum
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetCmd(Enum value)
        {
            string output = null;

            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            strPacketCmd[] attrs = fi.GetCustomAttributes(typeof(strPacketCmd), false) as strPacketCmd[];
            if (attrs.Length > 0)
            {
                output = attrs[0].strCMD;
            }
            return "";
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public struct stSerialSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public String strComPortName;
        /// <summary>
        /// 
        /// </summary>
        public int nBaudRate;
        /// <summary>
        /// 
        /// </summary>
        public int nDataBits;
        /// <summary>
        /// 
        /// </summary>
        public Parity Parity;
        /// <summary>
        /// 
        /// </summary>
        public StopBits StopBits;
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
            strComPortName = "COM1";
            nBaudRate = 9600;
            nDataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
            ReadTimeout = 1000;
            WriteTimeout = 1000;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public stSerialSetting Clone()
        {
            stSerialSetting pRet=new stSerialSetting();
            pRet.strComPortName=   this.strComPortName;
            pRet.nBaudRate     =   this. nBaudRate;
            pRet.nDataBits     =   this. nDataBits;
            pRet.Parity        =   this. Parity;
            pRet.StopBits      =   this. StopBits;
            pRet.ReadTimeout   =   this. ReadTimeout;
            pRet.WriteTimeout  =   this. WriteTimeout;
            return pRet;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ComDefine
    {		
        /// <summary>
        /// 
        /// </summary>
		public const int MAX_PATH = 260;
        /// <summary>
        /// 
        /// </summary>
		public const int COMM_CMD_MAX = 100;
        /// <summary>
        /// 
        /// </summary>
		public const int COMMTIMEOUT = 500;
        /// <summary>
        /// 
        /// </summary>
		public const int MAX_BUFF = 1024;
        /// <summary>
        /// 
        /// </summary>
        public const int nDefaultLigthValue = 100;
        /// <summary>
        /// 
        /// </summary>
        public static string ConfigFolderPath
        {
            get
            {
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                strPath = System.IO.Path.GetDirectoryName(strPath);
                string strConfigFolderPath = Path.GetFullPath(Path.Combine(strPath, @"..\Config"));

                DirectoryInfo DirInfo = new DirectoryInfo(strConfigFolderPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();
                return strConfigFolderPath;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string ConfigComSerialFileFullName
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strConfigVisionFolderPath = Path.GetFullPath(Path.Combine(strPath, @"Comm"));
                string strConfigVisionFilePath = Path.GetFullPath(Path.Combine(strPath, @"Comm\ComSerial.ini"));
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
	}//end of class
}//end of namespace
