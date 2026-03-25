using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
namespace EzIna.Scanner
{

    struct stOffsetData
    {
        public int iIndexX { get; set; }
        public int iIndexY { get; set; }

        public double dOffsetX_mm { get; set; }
        public double dOffsetY_mm { get; set; }
    }
    public class ScanLabRTC5Correction :SingleTone<ScanLabRTC5Correction>, IDisposable
    {

        public enum eFunctionError
        {
            Success = 1,
            KeyError,
            PathError,
            FileExtError,
            FieldSizeError,
            ListCountMissMatch,
            Fail,
        }

        public enum eCalPointType
        {
            Point_5x5 = 0,
            Point_9x9,
            Point_17x17,
            Point_33x33,
            Point_65x65
        }

      

        private readonly string m_strCorrectionProgram = @"correXionPro.exe";

        private bool m_bInit;

        List<stOffsetData> m_listOffset;
        List<stOffsetData> m_listSortOffset;

        private string m_strOldCalFile;
        private string m_strNewCalFile;

        private double m_dFieldSize_mm;
        private int m_iCalPoint;
        private int m_iFitOrder;
        private double m_dTolerance;
        private eCalPointType m_emCalPointType;

        private string m_strFolderPath;


        protected override void OnCreateInstance()
        {
            Initialize();
            base.OnCreateInstance();
        }
        #region 외부 함수      
        private void Initialize()
        {
            m_bInit = false;

            m_listOffset = new List<stOffsetData>();
            m_listSortOffset = new List<stOffsetData>();

            m_strOldCalFile = string.Empty;
            m_strNewCalFile = string.Empty;

            m_strFolderPath = string.Empty;

            m_dFieldSize_mm = 0.0;
            m_iCalPoint = 0;
            m_iFitOrder = 1;
            m_dTolerance = 0.01;
            m_emCalPointType = eCalPointType.Point_5x5;
           //this._CreateLogFile();
        }

        ~ScanLabRTC5Correction()
        {
            Clear();
        }

        public void Dispose()
        {
            m_listOffset.Clear();
            m_listSortOffset.Clear();
        }

        public int SetFolderPath(string strPath)
        {
            int iResult = -1;

            try
            {
                if(false == m_bInit)
                {
                    iResult = (int)eFunctionError.KeyError;
                    return iResult;
                }

                iResult = this.InternalCheckFolderPath(strPath);

                if ((int)eFunctionError.Success == iResult)
                    m_strFolderPath = strPath;
                else
                    m_strFolderPath = string.Empty;
            }
            catch(Exception ex)
            {
                //m_cLog.WriteDebug("SetFolderPath() : " + ex.Message);
            }
            
            return iResult;
        }

        public string GetFolderPath() { return m_strFolderPath; }

        public int SetOldCalFilePath(string strFile)
        {
            int iResult = -1;

            try
            {
                FileInfo fileInfo = new FileInfo(m_strFolderPath + "\\" + strFile);
                if (!fileInfo.Exists)
                {
                    iResult = (int)eFunctionError.PathError;
                    return iResult;
                }

                iResult = this.InternalCheckFilePath(strFile);

                if ((int)eFunctionError.Success == iResult)
                    m_strOldCalFile = strFile;
                else
                    m_strOldCalFile = string.Empty;
            }
            catch(Exception ex)
            {
                //m_cLog.WriteDebug("SetFolderPath() : " + ex.Message);
            }

            return iResult;
        }

        public string GetOldCalFilePath() { return m_strFolderPath; }

        public int SetNewCalFilePath(string strFile)
        {
            int iResult = -1;

            try
            {
                iResult = this.InternalCheckFilePath(strFile);

                if ((int)eFunctionError.Success == iResult)
                    m_strNewCalFile = strFile;
                else
                    m_strNewCalFile = string.Empty;
            }
            catch(Exception ex)
            {
                //m_cLog.WriteDebug("SetNewCalFilePath() : " + ex.Message);
            }

            return iResult;
        }

        public string GetNewCalFilePath() { return m_strFolderPath; }

        public void SetFieldSize(double dFieldSize_mm)
        {
            m_dFieldSize_mm = dFieldSize_mm;
        }

        public double GetFieldSize()
        {
            return m_dFieldSize_mm;
        }

        public void SetFitOrder(int iOrder)
        {
            m_iFitOrder = iOrder;
        }

        public int GetFitOrder()
        {
            return m_iFitOrder;
        }

        public void SetTolerance(double dTolerance)
        {
            m_dTolerance = dTolerance;
        }

        public double GetTolerance()
        {
            return m_dTolerance;
        }

        public void SetCalPointType(eCalPointType emType)
        {
            m_emCalPointType = emType;
        }

        eCalPointType GetCalPointType()
        {
            return m_emCalPointType;
        }
        
        public void Clear()
        {
            m_listOffset.Clear();
            m_listSortOffset.Clear();
            
            m_strFolderPath = string.Empty;

            m_strOldCalFile = string.Empty;
            m_strNewCalFile = string.Empty;
            
            m_dFieldSize_mm = 0.0;

            m_iCalPoint = 0;
            m_emCalPointType = eCalPointType.Point_5x5;
        }

        public void AddData(int iIndexX, int iIndexY, double dOffsetX_mm, double dOffsetY_mm)
        {
            try
            {
                stOffsetData stData = new stOffsetData();

                stData.iIndexX = iIndexX;
                stData.iIndexY = iIndexY;
                stData.dOffsetX_mm = dOffsetX_mm;
                stData.dOffsetY_mm = dOffsetY_mm;

                m_listOffset.Add(stData);
            }
            catch(Exception ex)
            {
                //m_cLog.WriteDebug("AddData() : " + ex.Message);
                throw new Exception (ex.Message , ex);
            }
        }

        public int MakeCorrectionFile()
        {
            int iResult = -1;

            try
            {
                iResult = InternalCheckFolderPath(m_strFolderPath);
                if ((int)eFunctionError.Success != iResult)
                {
                    return iResult;
                }

                iResult = InternalCheckFilePath(m_strOldCalFile);
                if ((int)eFunctionError.Success != iResult)
                {
                    return iResult;
                }

                iResult = InternalCheckFilePath(m_strNewCalFile);
                if ((int)eFunctionError.Success != iResult)
                {
                    return iResult;
                }

                if(0.0 >= m_dFieldSize_mm)
                {
                    iResult = (int)eFunctionError.FieldSizeError;
                    return iResult;
                }
                                
                m_iCalPoint = InternalGetCalPoint();

                int iTotalCalPoint = 0;
                iTotalCalPoint = (int)Math.Pow(m_iCalPoint, 2);

                if (m_listOffset.Count < iTotalCalPoint || iTotalCalPoint < m_listOffset.Count)
                {
                    iResult = (int)eFunctionError.ListCountMissMatch;
                    return iResult;
                }

                this.InternalSortData(m_iCalPoint);

                if(false == this.InternalMakeCorrectionFile())
                {
                    iResult = (int)eFunctionError.Fail;
                    return iResult;
                }

                iResult = (int)eFunctionError.Success;
            }
            catch(Exception ex)
            {
                //m_cLog.WriteDebug("MakeCorrectionFile() : " + ex.Message);
            }

            return iResult;
        }
        #endregion

        #region 내부 함수       
        
        private int InternalCheckFolderPath(string strPath)
        {
            int iResult = -1;

            try
            {
                if (0 >= strPath.Length)
                {
                    iResult = (int)eFunctionError.PathError;
                    return iResult;
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(strPath);
                if (!directoryInfo.Exists)
                {
                    iResult = (int)eFunctionError.PathError;
                    return iResult;
                }

                iResult = (int)eFunctionError.Success;
            }
            catch(Exception ex)
            {
                //m_cLog.WriteDebug("_CheckFolderPath() : " + ex.Message);
            }
            
            return iResult;
        }

        private int InternalCheckFilePath(string strFile)
        {
            int iResult = -1;

            try
            {
                if (0 >= strFile.Length)
                {
                    iResult = (int)eFunctionError.PathError;
                    return iResult;
                }

                string strFile_upper = strFile.ToUpper();
                if (0 > strFile_upper.IndexOf(".CT5"))
                {
                    iResult = (int)eFunctionError.FileExtError;
                    return iResult;
                }

                iResult = (int)eFunctionError.Success;
            }
            catch(Exception ex)
            {
                //m_cLog.WriteDebug("_CheckFilePath() : " + ex.Message);
            }
            
            return iResult;
        }

        private int InternalGetCalPoint()
        {
            int iCalPoint = -1;

            switch (m_emCalPointType)
            {
                case eCalPointType.Point_5x5:
                    iCalPoint = 5;
                    break;

                case eCalPointType.Point_9x9:
                    iCalPoint = 9;
                    break;

                case eCalPointType.Point_17x17:
                    iCalPoint = 17;
                    break;

                case eCalPointType.Point_33x33:
                    iCalPoint = 33;
                    break;

                case eCalPointType.Point_65x65:
                    iCalPoint = 65;
                    break;
            }

            return iCalPoint;
        }

        private void InternalSortData(int iOneLinePoint)
        {
            List<stOffsetData> ListBuffer = new List<stOffsetData>();

            m_listOffset.Sort(delegate (stOffsetData Data1, stOffsetData Data2)
            {
                if (Data1.iIndexY > Data2.iIndexY) return 1;
                else if (Data1.iIndexY < Data2.iIndexY) return -1;
                return 0;
            });

            int iCnt = 0;
            for (int i = 0; i < m_listOffset.Count; i++)
            {
                ListBuffer.Add(m_listOffset[i]);
                iCnt++;

                if (iCnt == iOneLinePoint)
                {
                    // 한 라인 데이터들을 모아서 X 방향으로 정렬
                    ListBuffer.Sort(delegate (stOffsetData Data1, stOffsetData Data2)
                    {
                        if (Data1.iIndexX > Data2.iIndexX) return 1;
                        else if (Data1.iIndexX < Data2.iIndexX) return -1;
                        return 0;
                    });

                    // 정렬된 값을 저장하고
                    foreach (stOffsetData Data in ListBuffer)
                    {
                        m_listSortOffset.Add(Data);
                    }

                    // 버퍼, 카운트 초기화
                    ListBuffer.Clear();
                    iCnt = 0;
                }
            }

            ListBuffer.Clear();
        }

        private bool InternalMakeCorrectionFile()
        {
            string strDataFile = String.Format($"{DateTime.Now.Year}{DateTime.Now.Month:D2}{DateTime.Now.Day:D2}-{DateTime.Now.Hour:D2}{DateTime.Now.Minute:D2}{DateTime.Now.Second:D2}.dat");
            string strDataFilePath = String.Format(m_strFolderPath + "\\" + strDataFile);

            using (var stream = new StreamWriter(strDataFilePath))
            {                
                int ik_Factor = Convert.ToInt32(Math.Pow(2, 20) / m_dFieldSize_mm);

                double dDiatance_mm = m_dFieldSize_mm / (m_iCalPoint - 1);
                double dDiatance_LSB = Convert.ToInt32((Math.Pow(2, 20) - 1) / (m_iCalPoint - 1));

                int iMinLSB = -524288;
                double dMinMM = -1 * m_dFieldSize_mm / 2.0;

                stream.WriteLine($"OLDCTFILE\t= " + m_strFolderPath + "\\" + m_strOldCalFile + "\t // file name of the correction file used for marking the measured points");
                stream.WriteLine($"NEWCTFILE\t= " + m_strFolderPath + "\\" + m_strNewCalFile + "\t // file name of the new correction file");
                stream.WriteLine($"TOLERANCE\t= " + m_dTolerance.ToString());
                stream.WriteLine($"NEWCAL\t= " + ik_Factor.ToString());
                stream.WriteLine($"FITORDER\t= " + m_iFitOrder.ToString());
                stream.WriteLine($"");
                stream.WriteLine($"");
                stream.WriteLine($"// RTC-X [bit]\tRTC-Y [bit]\tREAL-X [mm]\tREAL-Y [mm]");

                int iIndex = 0;

                for (int iY = 0; iY < m_iCalPoint; iY++)
                {
                    double dPosY_mm = dMinMM + (iY * dDiatance_mm);
                    double dOffsetY_mm = dPosY_mm + m_listSortOffset[iIndex].dOffsetY_mm;

                    for (int iX = 0; iX < m_iCalPoint; iX++)
                    {
                        int iPosX_LSB = iMinLSB + (int)(iX * dDiatance_LSB);
                        int iPosY_LSB = iMinLSB + (int)(iY * dDiatance_LSB);

                        if (0 < iPosX_LSB)
                            iPosX_LSB--;

                        if (0 < iPosY_LSB)
                            iPosY_LSB--;

                        double dPosX_mm = dMinMM + (iX * dDiatance_mm);

                        double dOffsetX_mm = dPosX_mm + m_listSortOffset[iIndex].dOffsetX_mm;

                        stream.WriteLine($"\t {iPosX_LSB} \t {iPosY_LSB} \t {dOffsetX_mm} \t {dOffsetY_mm}");

                        iIndex++;
                    }
                }

                stream.Close();
            }

            #region create correXionPro.exe process 
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = m_strCorrectionProgram;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = strDataFile;
            startInfo.WorkingDirectory= ScanlabRTC5.ConfigScanlabFolderPath;
            using (Process proc = Process.Start(startInfo))
            {
                if (!proc.WaitForExit(10 * 1000))
                    return false;
                if (0 != proc.ExitCode)
                    return false;
            }
            #endregion

            #region read result log file
            String resultLogFileFullPath = String.Format(m_strFolderPath + "\\" + m_strNewCalFile + ".txt");
            if (!File.Exists(resultLogFileFullPath))
                return false;
            #endregion

            return true;
        }
      
        #endregion
    }
}
