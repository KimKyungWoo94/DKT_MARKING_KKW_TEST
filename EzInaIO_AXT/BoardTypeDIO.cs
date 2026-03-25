using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO.AXT
{
    public sealed class BoardTypeDIO : DeviceModule
    {
        static bool m_bBoardInitialize = false;

        private Dictionary<int, BitField32Helper> m_DicInput = new Dictionary<int, BitField32Helper>();
        private Dictionary<int, BitField32Helper> m_DicOutput = new Dictionary<int, BitField32Helper>();

        public bool IsDeviceInitialized
        {
            get
            {
                return m_bBoardInitialize;
            }
        }

        public Type ClassType
        {
            get
            {
                return this.GetType();
            }
        }

        public void InitializeDevice()
        {
#if SIM
					 m_bBoardInitialize=true;
#else
            if (m_bBoardInitialize == false)
            {
                string strEZUC =(Marshal.SizeOf(typeof(IntPtr)) == 4)?
                   @"C:\Program Files\EzSoftware UC\AXL(Library)\Library\32Bit\":
                   @"C:\Program Files (x86)\EzSoftware UC\AXL(Library)\Library\64Bit\";
                string strEZRM =(Marshal.SizeOf(typeof(IntPtr)) == 4)?
                   @"c:\Program Files\EzSoftware RM\AXL(Library)\library\32Bit\":
                   @"c:\Program Files (x86)\EzSoftware RM\AXL(Library)\library\64Bit\";
                string strPath = System.Reflection.Assembly.GetEntryAssembly().Location;
								strPath = System.IO.Path.GetDirectoryName(strPath);
                strPath+="\\";
                string[] files;
                string name;
                string dest;
                string Ext;
                if (Directory.Exists(strEZUC))
                {
                    files = Directory.GetFiles(strEZUC);
                  
                    foreach (string file in files)
                    {                        
                        if(Path.GetExtension(file)==".dll")
                        {
                            name = Path.GetFileName(file);
                            dest = Path.Combine(strPath, name);
                            File.Copy(file, dest, true);
                        }
                        
                    }
                }
                else
                {
                    if (Directory.Exists(strEZRM))
                    {
                        files = Directory.GetFiles(strEZRM);
                        foreach (string file in files)
                        {
                            if(Path.GetExtension(file)==".dll")
                            {
                                name = Path.GetFileName(file);
                                dest = Path.Combine(strPath, name);
                                File.Copy(file, dest, true);
                            }                            
                        }
                    }
                    else
                    {
                       throw new Exception("Isn't Exist AXL Library File");
                    }
                }
                if (CAXL.AxlIsOpened() == 0)
                {
                    if (CAXL.AxlOpen(7) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    {
                        throw new Exception("AXL Library Initialize Fail");
                    }
                }
                uint uStatus = 0;
                if (CAXD.AxdInfoIsDIOModule(ref uStatus) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    int nModuleCount = 0;
                    int i = 0;
                    int nInputCount = 0;

                    int nOutputCount = 0;
                    uint nInputValue = 0;
                    uint nOutputValue = 0;
                    if (CAXD.AxdInfoGetModuleCount(ref nModuleCount) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    {
                        for (i = 0; i < nModuleCount; i++)
                        {
                            CAXD.AxdInfoGetInputCount(i, ref nInputCount);
                            CAXD.AxdInfoGetOutputCount(i, ref nOutputCount);
                            if (nInputCount > 0)
                            {
                                CAXD.AxdiReadInportDword(i, 0, ref nInputValue);
                                m_DicInput.Add(i, new BitField32Helper(nInputValue));
                            }
                            if (nOutputCount > 0)
                            {
                                CAXD.AxdoReadOutportDword(i, 0, ref nOutputValue);
                                m_DicOutput.Add(i, new BitField32Helper(nOutputValue));
                            }
                        }
                    }                   
                    m_bBoardInitialize = true;
                }
            }
#endif
        }
        public void TerminateDevice()
        {
            if (m_bBoardInitialize)
            {
                try
                {
                    if (CAXL.AxlIsOpened() == (int)AXT_BOOLEAN.TRUE)
                        CAXL.AxlClose();
                    m_DicInput.Clear();
                    m_DicOutput.Clear();
                    m_bBoardInitialize = false;
                }
                catch
                {

                }
            }
        }
        public IOAddrInfo CreateAddressInfo(params string [] a_Params)
        {
            return new BoardDIOAddrInfoAXT(a_Params);
        }
        public void ReadAllDI()
        {
            if (m_DicInput.Count <= 0)
                return;
            uint nValue = 0;
            uint nStatus = 0;
            try
            {
                 foreach (KeyValuePair<int, BitField32Helper> item in m_DicInput)
                {
                    nStatus = CAXD.AxdiReadInportDword(item.Key, 0, ref nValue);
                    if (nStatus == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    {
                        item.Value.Data = nValue;
                    }
                }
            }
            catch
            {
               
            }
        }
        public void ReadAllDO()
        {

            if (m_DicOutput.Count <= 0)
                return;
            uint nValue = 0;
            uint nStatus = 0;
            try
            {
#if !SIM
                 foreach (KeyValuePair<int, BitField32Helper> item in m_DicOutput)
                {
                    nStatus = CAXD.AxdoReadOutportDword(item.Key, 0, ref nValue);
                    if (nStatus == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    {
                        item.Value.Data = nValue;
                    }
                }
#endif
            }
            catch
            {
               
            }
        }
        public void WriteAllDO()
        {
            if (m_DicOutput.Count <= 0)
                return;

           uint nStatus = 0;
           try
            {
#if !SIM
                foreach (KeyValuePair<int, BitField32Helper> item in m_DicOutput)
                {
                    nStatus = CAXD.AxdoWriteOutportDword(item.Key, 0, item.Value.Data);                    
                }
#endif
            }
            catch
            {
               
            }
        }

        public bool GetDI(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER)
        {
            BoardDIOAddrInfoAXT Addr=a_AddresInfo as BoardDIOAddrInfoAXT;
            bool bRet=false;
            uint  iRet=0;
            if(m_DicInput.ContainsKey(Addr.iModuleNumber))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition))
                {
                    switch(a_ValueFrom)
                    {
                        case enumValueFrom.GETTING_BUFFER:
                            {
                                bRet=m_DicInput[Addr.iModuleNumber][(bit)Addr.iPosition];
                            }
                            break;
                        case enumValueFrom.API:
                            {
                                CAXD.AxdiReadInportBit(Addr.iModuleNumber,Addr.iPosition,ref iRet);
                                bRet=iRet==1;
                            }
                            break;
                    }
                    
                }                
            }
            return bRet;
        }

        public bool GetDO(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER)
        {
            BoardDIOAddrInfoAXT Addr=a_AddresInfo as BoardDIOAddrInfoAXT;
            bool bRet=false;
            uint iRet=0;
            if(m_DicOutput.ContainsKey(Addr.iModuleNumber))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition))
                {

                    switch(a_ValueFrom)
                    {
                        case enumValueFrom.GETTING_BUFFER:
                            {
                                bRet=m_DicOutput[Addr.iModuleNumber][(bit)Addr.iPosition];
                            }
                            break;
                        case enumValueFrom.API:
                            {
                                CAXD.AxdoReadOutportBit(Addr.iModuleNumber,Addr.iPosition,ref iRet);
                                bRet=iRet==1;
                            }
                            break;
                    }                   
                }                
            }
            return bRet;
        }
        public void SetDO(IOAddrInfo a_AddresInfo, bool a_value,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER)
        {
            BoardDIOAddrInfoAXT Addr=a_AddresInfo as BoardDIOAddrInfoAXT;
            if(m_DicOutput.ContainsKey(Addr.iModuleNumber))
            {
                if(Enum.IsDefined(typeof(bit),(bit)Addr.iPosition))
                {
                    switch(a_ValueFrom)
                    {
                        case enumValueFrom.GETTING_BUFFER:
                            {
                                 m_DicOutput[Addr.iModuleNumber][(bit)Addr.iPosition]=a_value;
                            }
                            break;
                        case enumValueFrom.API:
                            {
                                CAXD.AxdoWriteOutportBit(Addr.iModuleNumber,Addr.iPosition,a_value==true? (uint)1:(uint)0);                               
                            }
                            break;
                    }
                  
                }                
            }            
        }
#region Not available
        public void ReadAllAI()
        {
            
        }
        public void ReadAllAO()
        {            
        }
       
        public double GetAI(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER)
        {
            return 0.0;
        }
        public double GetAO(IOAddrInfo a_AddresInfo,enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER)
        {
            return 0.0;
        }
        public void SetAO(IOAddrInfo a_AddresInfo, double a_value, enumValueFrom a_ValueFrom=enumValueFrom.GETTING_BUFFER)
        {

        }

        public void SetAIRange(IOAddrInfo a_AddresInfo)
        {
            
        }

        public void SetAORange(IOAddrInfo a_AddresInfo)
        {
            
        }
#endregion Not available


    }
}
