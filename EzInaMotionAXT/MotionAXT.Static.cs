using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.Motion
{
        #region  Static Func Variable
    public sealed partial class CMotionAXT 
    {
     
        static List<CMotionAXT> m_listAxises = null;
        
        static int m_nAxisTotalCount = 0;
        static uint UModuleID = 0;
        static bool m_bDriverConnect = false;
        static bool m_bInitialized = false;
        public static bool bDriverConnect
        {
            get { return m_bDriverConnect; }
        }
        public static bool bInitialized
        {
            get { return m_bInitialized; }
        }
        public static List<CMotionAXT> plistAxies {  get { return m_listAxises; } }

        #region     Init & Terminate Driver
        public static bool InitializeConnectDriver()
        {

#if SIM
            return m_bDriverConnect=true;
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
										if(File.Exists(dest)==false)
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
												if(File.Exists(dest)==false)
                        File.Copy(file, dest,true);
                    }
                }
                else
                {
                    return m_bDriverConnect = false;
                }
            }
            if (CAXL.AxlIsOpened() != 1)
            {
                if (CAXL.AxlOpen(7) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    return m_bDriverConnect = false;
                }
            }

            return m_bDriverConnect = true;
#endif
        }
        public static bool TerminateConnectedDriver()
        {
            if (m_bDriverConnect)
            {
#if !SIM
								if (CAXL.AxlIsOpened() == (int)AXT_BOOLEAN.TRUE)
                    CAXL.AxlClose();
#endif
                m_bDriverConnect = false;
            }
            return m_bDriverConnect;
        }

        public static bool InitializeDriver(bool bUseMotFile)
        {
            try
            {                
                if (m_bInitialized == false)
                {
                    m_listAxises = null;
                    m_listAxises = new List<CMotionAXT>();
                }

                InitializeConnectDriver();
#if SIM
								for(int i=0;i<16;i++)
								{
										m_listAxises.Add(new CMotionAXT(i,string.Format("SIM_Motion{0}",i)));  
								}
                return true;
#else

                if (m_bDriverConnect)
                {
                    String strAjinInfo = "";
                    //++ 유효한 전체 모션축수를 반환합니다.
                    CAXM.AxmInfoGetAxisCount(ref m_nAxisTotalCount);

                    // populate axis names
                    int iBoardNo = 0, iModulePos = 0;
                    UModuleID = 0;

                    for (int i = 0; i < m_nAxisTotalCount; i++)
                    {
                        CAXM.AxmInfoGetAxis(i, ref iBoardNo, ref iModulePos, ref UModuleID);
                        
                        switch (UModuleID)
                        {
                            //++ 지정한 축의 정보를 반환합니다.
                            case (uint)AXT_MODULE.AXT_SMC_4V04:
                                {
                                    strAjinInfo = String.Format("{0:00}-[PCIB-QI4A]", i); 
                                }
                                break;
                            case (uint)AXT_MODULE.AXT_SMC_2V04:
                                {
                                    strAjinInfo = String.Format("{0:00}-[PCIB-QI2A]", i);
                                }
                                break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04:
                                strAjinInfo = String.Format("{0:00}-(RTEX-PM)", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04PM2Q:
                                strAjinInfo = String.Format("{0:00}-(RTEX-PM2Q)", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04PM2QE:
                                strAjinInfo = String.Format("{0:00}-(RTEX-PM2QE)", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04A4:
                                strAjinInfo = String.Format("{0:00}-[RTEX-A4N]", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04A5:
                                strAjinInfo = String.Format("{0:00}-[RTEX-A5N]", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04MLIISV:
                                strAjinInfo = String.Format("{0:00}-[MLII-SGDV]", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIPM:
                                strAjinInfo = String.Format("{0:00}-(MLII-PM)", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICL:
                                strAjinInfo = String.Format("{0:00}-[MLII-CSDL]", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04MLIICR:
                                strAjinInfo = String.Format("{0:00}-[MLII-CSDH]", i); break;
                            case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV:
                                strAjinInfo = String.Format("{0:00}-[SIIIH-MRJ4]", i); break;
                            default: strAjinInfo = String.Format("{0:00}-[Unknown]", i); break;
                        }
                        m_listAxises.Add(new CMotionAXT(i, strAjinInfo));                       
                    }

                    if(bUseMotFile)
                    {
                        if (CAXM.AxmMotLoadParaAll(GDMotion.ConfigMotionMotFileFullName) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                        {                          
                            // Add Err Action Later
                        }
                    }
                    m_bInitialized = true;                  
                    return true;
                }
#endif
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
            return false;
        }
        public static void TerminateDriver()
        {
            m_listAxises.Clear();
            m_listAxises = null;           
            m_bInitialized = false;
            TerminateConnectedDriver();          
        }
#endregion  Init & Terminate Driver

        public static CAXT_AxisModuleInfo GetAxisInformation(int a_iAxisNo)
        {
            int iBoardNo=-1;
            int iModuePos=-1;
            uint iMoudleID=0;
#if !SIM
						CAXM.AxmInfoGetAxis(a_iAxisNo,ref iBoardNo, ref iModuePos , ref iMoudleID);
#endif
            return new CAXT_AxisModuleInfo(iBoardNo,iModuePos,iMoudleID);
						
				}
    }
#endregion Static Func Variable
}
