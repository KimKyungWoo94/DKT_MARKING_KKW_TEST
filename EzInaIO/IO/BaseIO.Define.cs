using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO
{

    public enum EContact
    {
        /// <summary>
        /// Normal Open
        /// </summary>
        A,
        /// <summary>
        /// Normal Close
        /// </summary>
        B
    }
    public delegate double GetAnalogValueFunc(IOAddrInfo address,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);
    public delegate bool   GetDigitValueFunc(IOAddrInfo address,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);   
    public delegate void   SetAnalogValueFunc(IOAddrInfo address, double a_value,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);
    public delegate void   SetDigitValueFunc(IOAddrInfo address, bool a_value,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER);      
    public delegate void   SetAnalogRangeFunc(IOAddrInfo address);
    public static class FolderPath
    {
        public static string ConfigFolderPath
		{
			get
			{
				string strPath = System.Reflection.Assembly.GetEntryAssembly().Location;
				strPath = System.IO.Path.GetDirectoryName(strPath);
				string strConfigFolderPath = Path.GetFullPath(Path.Combine(strPath, @"..\System\CFG"));

				DirectoryInfo DirInfo = new DirectoryInfo(strConfigFolderPath);
				if (DirInfo.Exists == false)
					DirInfo.Create();

				return strConfigFolderPath;

			}
		}
        public static string ConfigIOFolderPath
		{
			get
			{
				string strPath = Path.GetFullPath(Path.Combine(ConfigFolderPath, @"IO"));						
				DirectoryInfo DirInfo = new DirectoryInfo(strPath);
				if (DirInfo.Exists == false)
					DirInfo.Create();

				return strPath;

			}
		}
        public static string ConfigIOItemDataFileFullname
		{
			get
			{
				string strPath = ConfigIOFolderPath;
                string FileName =	Path.GetFullPath(Path.Combine(strPath, @"IOItemLinkData.xml"));					
				DirectoryInfo DirInfo = new DirectoryInfo(strPath);
				if (DirInfo.Exists == false)
					DirInfo.Create();

				return FileName;

			}
		}
    }
}
