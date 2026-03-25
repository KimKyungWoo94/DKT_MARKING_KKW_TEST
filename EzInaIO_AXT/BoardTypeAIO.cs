using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.IO.AXT
{
    public sealed class BoardTypeAIO : DeviceModule
    {
        static bool m_bBoardInitialize = false;
        static int m_iAIChannelCount = 0;
        static int m_iAOChannelCount = 0;
        static int[] m_iAIChanelNumList;
        static int[] m_iAOChanelNumList;
        static double[] m_fAIValueList;
        static double[] m_fAOValueList;
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
            if (m_bBoardInitialize == true)
                return;
#if SIM
           
#else
            string strEZUC = (Marshal.SizeOf(typeof(IntPtr)) == 4) ?
                     @"C:\Program Files\EzSoftware UC\AXL(Library)\Library\32Bit\" :
                     @"C:\Program Files (x86)\EzSoftware UC\AXL(Library)\Library\64Bit\";
            string strEZRM = (Marshal.SizeOf(typeof(IntPtr)) == 4) ?
               @"c:\Program Files\EzSoftware RM\AXL(Library)\library\32Bit\" :
               @"c:\Program Files (x86)\EzSoftware RM\AXL(Library)\library\64Bit\";
            string strPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            strPath = System.IO.Path.GetDirectoryName(strPath);
            strPath += "\\";
            string[] files;
            if (Directory.Exists(strEZUC))
            {
                files = Directory.GetFiles(strEZUC);
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(strPath, name);
                    File.Copy(file, dest,true);
                }
            }
            else
            {
                if (Directory.Exists(strEZRM))
                {
                    files = Directory.GetFiles(strEZRM);
                    foreach (string file in files)
                    {
                        string name = Path.GetFileName(file);
                        string dest = Path.Combine(strPath, name);
                        File.Copy(file, dest,true);
                    }
                }
                else
                {
                    throw new Exception("Isn't Exist AXL Library File");
                }
            }
            if (CAXL.AxlIsOpened() != 1)
            {
                if (CAXL.AxlOpen(7) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                     throw new Exception("AXL Library Initialize Fail");                
                }
            }
            uint uStatus = 0;          
            if (CAXA.AxaInfoIsAIOModule(ref uStatus) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                if ((AXT_EXISTENCE)uStatus == AXT_EXISTENCE.STATUS_EXIST)
                {
                    CAXA.AxaiInfoGetChannelCount(ref m_iAIChannelCount);
                    CAXA.AxaoInfoGetChannelCount(ref m_iAOChannelCount);
                    if (m_iAIChannelCount > 0)
                    {
                        m_fAIValueList = new double[m_iAIChannelCount];
                        m_iAIChanelNumList = new int[m_iAIChannelCount];
                        for (int i = 0; i < m_iAIChannelCount; i++)
                        {
                            m_iAIChanelNumList[i] = i;
                        }
                    }
                    if (m_iAOChannelCount > 0)
                    {
                        m_fAOValueList = new double[m_iAOChannelCount];
                        m_iAOChanelNumList = new int[m_iAOChannelCount];
                        for (int i = 0; i < m_iAOChannelCount; i++)
                        {
                            m_iAOChanelNumList[i] = i;
                        }
                    }
                }
            }
#endif
        }
        public void TerminateDevice()
        {
            if (m_bBoardInitialize)
            {
                if (CAXL.AxlIsOpened() == 1)
                {
                    CAXL.AxlClose();
                }
                m_iAIChannelCount = 0;
                m_iAOChannelCount = 0;
                m_bBoardInitialize = false;                
            }
        }
        public IOAddrInfo CreateAddressInfo(params string [] a_Params)
        {
            return new BoardAIOAddrInfoAXT(a_Params);
        }
        public void ReadAllAI()
        {
            if (m_iAIChannelCount > 0)
            {
                CAXA.AxaiSwReadMultiVoltage(m_iAIChannelCount, m_iAIChanelNumList, m_fAIValueList);
            }
        }
        public void ReadAllAO()
        {
            if (m_iAOChannelCount > 0)
            {
                CAXA.AxaoReadMultiVoltage(m_iAOChannelCount, m_iAOChanelNumList, m_fAOValueList);
            }
        }
        public double GetAI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            BoardAIOAddrInfoAXT addr = a_AddresInfo as BoardAIOAddrInfoAXT;
            if (addr.iChannelNum >= 0 && addr.iChannelNum < m_iAIChannelCount)
            {
                if (a_ValueFrom == enumValueFrom.API)
                {
                    CAXA.AxaiSwReadVoltage(addr.iChannelNum, ref m_fAIValueList[addr.iChannelNum]);
                }
                return m_fAIValueList[addr.iChannelNum];
            }
            return 0.0;
        }

        public void SetAO(IOAddrInfo a_AddresInfo, double a_value, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            BoardAIOAddrInfoAXT addr = a_AddresInfo as BoardAIOAddrInfoAXT;
            if (m_iAOChannelCount > 0 && addr.iChannelNum >= 0 && addr.iChannelNum < m_iAOChannelCount)
            {
                if (a_ValueFrom == enumValueFrom.API)
                {
                    CAXA.AxaoWriteVoltage(addr.iChannelNum, a_value);                    
                }
                else
                {
                    m_fAOValueList[addr.iChannelNum]=a_value;
                }
            }
        }
        public double GetAO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            BoardAIOAddrInfoAXT addr = a_AddresInfo as BoardAIOAddrInfoAXT;
             if (m_iAOChannelCount > 0 && addr.iChannelNum >= 0 && addr.iChannelNum < m_iAOChannelCount)
            {
                if (a_ValueFrom == enumValueFrom.API)
                {
                    CAXA.AxaoReadVoltage(addr.iChannelNum,ref m_fAOValueList[addr.iChannelNum]);
                }
                return m_fAOValueList[addr.iChannelNum];
            }
            return 0.0;
        }
#region Not available
        public void ReadAllDI()
        {

        }
        public void ReadAllDO()
        {

        }
        public void WriteAllDO()
        {

        }
        public bool GetDI(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            return false;
        }

        public bool GetDO(IOAddrInfo a_AddresInfo, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {
            return false;
        }
        public void SetDO(IOAddrInfo a_AddresInfo, bool a_value, enumValueFrom a_ValueFrom = enumValueFrom.GETTING_BUFFER)
        {

        }

        public void SetAIRange(IOAddrInfo a_AddresInfo)
        {
           BoardAIOAddrInfoAXT addr = a_AddresInfo as BoardAIOAddrInfoAXT;
           if (m_iAOChannelCount > 0 && addr.iChannelNum >= 0 && addr.iChannelNum < m_iAOChannelCount)
           {
                CAXA.AxaiSetRange(addr.iChannelNum,addr.fMinVolt,addr.fMaxVolt);
           }
        }

        public void SetAORange(IOAddrInfo a_AddresInfo)
        {
            BoardAIOAddrInfoAXT addr = a_AddresInfo as BoardAIOAddrInfoAXT;
            if (m_iAOChannelCount > 0 && addr.iChannelNum >= 0 && addr.iChannelNum < m_iAOChannelCount)
            {
                CAXA.AxaoSetRange(addr.iChannelNum,addr.fMinVolt,addr.fMaxVolt);
            }
        }
#endregion Not available

    }
}
