using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices; 


namespace EzIna
{
    public class PowerTable : IDisposable
    {
        #region DllImport
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string appName, byte[] returnValue, int size, string filePath);
        #endregion
        
        public enum emFunctionError_t
        {
            Success = 1,
            PathError,
            FileExtensionError,
            NoData,
            MinValueError,
            MaxValueError,
            Fail,
        }

        const int STEP = 100;

        private string m_strFilePath;

        private Dictionary<float, float> m_dicPowerCurve;

        #region 외부 함수
        public PowerTable(string a_strPath)
        {
            this.m_strFilePath = a_strPath;
            this.m_dicPowerCurve = new Dictionary<float, float>();
        }

        ~PowerTable()
        {

        }

        public void Dispose()
        {

        }

        public string GetReturnMessage(int ErrorCode)
        {
            string strReturn = string.Empty;

            switch (ErrorCode)
            {
                case (int)emFunctionError_t.Success:
                    strReturn = "Success";
                    break;
                case (int)emFunctionError_t.PathError:
                    strReturn = "PowerTable File Path Error";
                    break;
                case (int)emFunctionError_t.FileExtensionError:
                    strReturn = "PowerTable File Extension Error";
                    break;
                case (int)emFunctionError_t.NoData:
                    strReturn = "No Data";
                    break;
                case (int)emFunctionError_t.MinValueError:
                    strReturn = "MinValue Error";
                    break;
                case (int)emFunctionError_t.MaxValueError:
                    strReturn = "MaxValue Error";
                    break;
                case (int)emFunctionError_t.Fail:
                    strReturn = "Fail";
                    break;
            }

            return strReturn;
        }

        /// <summary>
        /// 파워테이블 파일경로 지정
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public int SetFilePath(string strPath)
        {
            int iResult = -1;

            try
            {
                if (true == string.IsNullOrEmpty(strPath))
                {
                    iResult = (int)emFunctionError_t.PathError;
                    return iResult;
                }

                string strPath_upper = strPath.ToUpper();
                if (-1 == strPath_upper.IndexOf(".TXT"))
                {
                    iResult = (int)emFunctionError_t.FileExtensionError;
                    return iResult;
                }

                FileInfo fileInfo = new FileInfo(strPath);
                if (!fileInfo.Exists)
                {
                    System.IO.File.Create(strPath);
                }

                this.m_strFilePath = strPath;
                iResult = (int)emFunctionError_t.Success;
            }
            catch (Exception ex)
            {

            }

            return iResult;
        }

        /// <summary>
        /// 파워테이블에 측정값 추가
        /// </summary>
        /// <param name="iFreq_kHz"></param>
        /// <param name="iPulseWidth_percentage"></param>
        /// <param name="dPower_watt"></param>
        /// <returns></returns>
        public int Add(int iFreq_kHz, int iPulseWidth_percentage, double dPower_watt)
        {
            int iResult = -1;

            try
            {
                if (true == string.IsNullOrEmpty(this.m_strFilePath))
                {
                    iResult = (int)emFunctionError_t.PathError;
                    return iResult;
                }

                WritePrivateProfileString(iFreq_kHz.ToString(), iPulseWidth_percentage.ToString(), dPower_watt.ToString(), m_strFilePath);
                iResult = (int)emFunctionError_t.Success;
            }
            catch (Exception ex)
            {

            }

            return iResult;
        }

        /// <summary>
        /// 해당 프리퀀시의 최대파워
        /// </summary>
        /// <param name="iFreq_kHz"></param>
        /// <param name="dPower_watt"></param>
        /// <returns></returns>
        public int GetMaxPower(int iFreq_kHz, out double dPower_watt)
        {
            int iResult = -1;
            dPower_watt = -1.0;

            try
            {
                if (true == string.IsNullOrEmpty(this.m_strFilePath))
                {
                    iResult = (int)emFunctionError_t.PathError;
                    return iResult;
                }

                string strSectionName = iFreq_kHz.ToString();

                Dictionary<float, float> dicPowerTable = new Dictionary<float, float>();
                dicPowerTable = GetKeyValuesInSection(this.m_strFilePath, strSectionName);

                if (0 >= dicPowerTable.Count)
                {
                    iResult = (int)emFunctionError_t.NoData;
                    return iResult;
                }

                var PowerTable_sorted = from pair in dicPowerTable orderby pair.Key ascending select pair;
                KeyValuePair<float, float> MaxValue = PowerTable_sorted.ElementAt(PowerTable_sorted.Count() - 1);

                dPower_watt = MaxValue.Value;
                iResult = (int)emFunctionError_t.Success;
            }
            catch (Exception ex)
            {

            }

            return iResult;
        }

        /// <summary>
        /// 해당 프리퀀시의 최저파워 
        /// </summary>
        /// <param name="iFreq_kHz"></param>
        /// <param name="dPower_watt"></param>
        /// <returns></returns>
        public int GetMinPower(int iFreq_kHz, out double dPower_watt)
        {
            int iResult = -1;
            dPower_watt = -1.0;

            try
            {
                if (true == string.IsNullOrEmpty(this.m_strFilePath))
                {
                    iResult = (int)emFunctionError_t.PathError;
                    return iResult;
                }

                string strSectionName = iFreq_kHz.ToString();

                Dictionary<float, float> dicPowerTable = new Dictionary<float, float>();
                dicPowerTable = GetKeyValuesInSection(this.m_strFilePath, strSectionName);

                if (0 >= dicPowerTable.Count)
                {
                    iResult = (int)emFunctionError_t.NoData;
                    return iResult;
                }

                var PowerTable_sorted = from pair in dicPowerTable orderby pair.Key ascending select pair;
                KeyValuePair<float, float> minValue = PowerTable_sorted.ElementAt(0);

                dPower_watt = minValue.Value;
                iResult = (int)emFunctionError_t.Success;
            }
            catch (Exception ex)
            {

            }

            return iResult;
        }

        /// <summary>
        /// 지정 프리퀀시에서 지정된 파워를 얻기위한 펄스폭을 계산
        /// </summary>
        /// <param name="iFreq_kHz"></param>
        /// <param name="dPower_watt"></param>
        /// <param name="dCorrectPulseWidth_percentage"></param>
        /// <returns></returns>
        public int GetPulseWidthByFreqnPower(int iFreq_kHz, double dPower_watt, out double dCorrectPulseWidth_percentage)
        {
            int iResult = -1;
            dCorrectPulseWidth_percentage = -1.0;

            try
            {
                if (true == string.IsNullOrEmpty(this.m_strFilePath))
                {
                    iResult = (int)emFunctionError_t.PathError;
                    return iResult;
                }

                double MaxPower = 0.0;
                double MinPower = 0.0;
                this.GetMaxPower(iFreq_kHz, out MaxPower);
                this.GetMinPower(iFreq_kHz, out MinPower);
 
				        MaxPower = Math.Round(MaxPower, 6);
				        MinPower = Math.Round(MinPower, 6);

                if (MaxPower < dPower_watt) return (int)emFunctionError_t.MaxValueError;
                if (MinPower > dPower_watt) return (int)emFunctionError_t.MinValueError;

                string strSectionName = iFreq_kHz.ToString();

                // 보간테이블 생성
                this.CreateSplineInterpolationData(iFreq_kHz);

                // For Test
                //string path = System.IO.Directory.GetCurrentDirectory() + "\\dicPowerCurve.txt";

                foreach (KeyValuePair<float, float> temp in this.m_dicPowerCurve)
                {
                    // For Test
                    //WritePrivateProfileString(iFreq_kHz.ToString(), temp.Key.ToString(), temp.Value.ToString(), path);

                    if (Math.Abs(temp.Value - dPower_watt) <= 0.01)
                    {
                        dCorrectPulseWidth_percentage = temp.Key;
                        iResult = (int)emFunctionError_t.Success;
                        break;
                    }
                }

                iResult = (int)emFunctionError_t.Success;
            }
            catch (Exception ex)
            {

            }

            return iResult;
        }

        /// <summary>
        /// 지정 프리퀀시에서 지정된 파워를 얻기위한 펄스폭을 계산
        /// </summary>
        /// <param name="iFreq_kHz"></param>
        /// <param name="dPower_watt"></param>
        /// <param name="dCorrectPulseWidth_percentage"></param>
        /// <returns></returns>
        //public int GetPulseWidthByFreqnPower(int iFreq_kHz, double dPower_watt, out double dCorrectPulseWidth_percentage)
        //{
        //    int iResult = -1;
        //    dCorrectPulseWidth_percentage = -1.0;

        //    try
        //    {
        //        if (true == string.IsNullOrEmpty(this.m_strFilePath))
        //        {
        //            iResult = (int)emFunctionError_t.PathError;
        //            return iResult;
        //        }

        //        string strSectionName = iFreq_kHz.ToString();

        //        Dictionary<float, float> dicPowerTable = new Dictionary<float, float>();
        //        dicPowerTable = GetKeyValuesInSection(this.m_strFilePath, strSectionName);

        //        if (0 >= dicPowerTable.Count)
        //        {
        //            iResult = (int)emFunctionError_t.NoData;
        //            return iResult;
        //        }

        //        // Key 값을 오름차순으로 정렬하고
        //        var PowerTable_sorted = from pair in dicPowerTable orderby pair.Key ascending select pair;

        //        KeyValuePair<float, float> MinValue = PowerTable_sorted.ElementAt(0);
        //        KeyValuePair<float, float> MaxValue = PowerTable_sorted.ElementAt(PowerTable_sorted.Count() - 1);

        //        if (dPower_watt < MinValue.Value)
        //        {
        //            iResult = (int)emFunctionError_t.MinValueError;
        //            return iResult;
        //        }

        //        if (MaxValue.Value < dPower_watt)
        //        {
        //            iResult = (int)emFunctionError_t.MaxValueError;
        //            return iResult;
        //        }

        //        bool bSamePower = false;

        //        int nBefore_PulseWidth = 0;
        //        int nAfter_PulseWidth = 0;

        //        double dBefore_Power = 0.0;
        //        double dAfter_Power = 0.0;

        //        foreach (KeyValuePair<float, float> Data in PowerTable_sorted)
        //        {
        //            if (0.0001 > Math.Abs(Data.Value - dPower_watt))
        //            {
        //                bSamePower = true;
        //                dCorrectPulseWidth_percentage = Convert.ToDouble(Data.Key);
        //                break;
        //            }
        //            else if (Data.Value < dPower_watt)
        //            {
        //                nBefore_PulseWidth = Convert.ToInt32(Data.Key);
        //                dBefore_Power = Data.Value;
        //            }
        //            else if (Data.Value > dPower_watt)
        //            {
        //                nAfter_PulseWidth = Convert.ToInt32(Data.Key);
        //                dAfter_Power = Data.Value;

        //                break;
        //            }
        //        }

        //        if (false == bSamePower)
        //        {
        //            dCorrectPulseWidth_percentage = nBefore_PulseWidth + (nAfter_PulseWidth - nBefore_PulseWidth) * ((dPower_watt - dBefore_Power) / (dAfter_Power - dBefore_Power));
        //        }

        //        iResult = (int)emFunctionError_t.Success;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return iResult;
        //}
        #endregion

        #region 내부 함수
        /// <summary>
        /// 선형보간
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="strSectionName"></param>
        /// <returns></returns>
        private Dictionary<float, float> GetKeyValuesInSection(string strFilePath, string strSectionName)
        {
            Dictionary<float, float> dicKeyValues = new Dictionary<float, float>();

            try
            {
                byte[] returnValue = new byte[32768 * 2];
                int iByteCount = GetPrivateProfileSection(strSectionName, returnValue, 32768 * 2, strFilePath);

                int iPose = 0;
                while (true)
                {
                    if (iPose >= iByteCount)
                    {
                        break;
                    }

                    int iCount = 0;
                    while (true)
                    {
                        if (returnValue[iPose + iCount] == 0)
                        {
                            break;
                        }

                        iCount++;
                    }

                    string strText = Encoding.Default.GetString(returnValue, iPose, iCount);

                    int equalPos = strText.IndexOf('=');
                    if (-1 != equalPos)
                    {
                        dicKeyValues.Add(Convert.ToInt32(strText.Substring(0, equalPos)), (float)Convert.ToDouble(strText.Substring(equalPos + 1)));
                    }

                    iPose += iCount + 1;
                }
            }
            catch (Exception ex)
            {
                
            }

            return dicKeyValues;
        }

        /// <summary>
        /// 보간 데이터 생성,
        /// Monotonic Cubic Spline Interpolation
        /// </summary>
        /// <param name="iFreq_kHz"></param>
        /// <returns></returns>
        private int CreateSplineInterpolationData(int iFreq_kHz)
        {
            try
            {
                if (true == string.IsNullOrEmpty(this.m_strFilePath))
                    return (int)emFunctionError_t.PathError;

                string strSectionName = iFreq_kHz.ToString();
                Dictionary<float, float> dicPowerTable = new Dictionary<float, float>();
                dicPowerTable = GetKeyValuesInSection(this.m_strFilePath, strSectionName);

                if (dicPowerTable.Count < 2)
                    return (int)emFunctionError_t.NoData;

                float[] arr = new float[dicPowerTable.Count];
                List<float> M = new List<float>(arr);
                if (!this.EstimateTangents(dicPowerTable, ref M))
                    return (int)emFunctionError_t.Fail;

                List<float> X = new List<float>();
                List<float> Y = new List<float>();

                foreach (KeyValuePair<float, float> Data in dicPowerTable)
                {
                    X.Add(Data.Key); Y.Add(Data.Value);
                }

                this.m_dicPowerCurve.Clear();
                this.m_dicPowerCurve.Add(X[0], Y[0]);

                for (int i = 0; i < dicPowerTable.Count - 1; i++)
                {
                    float h = X[i + 1] - X[i];

                    for (int j = 1; j <= STEP; j++)
                    {
                        double t = j / Convert.ToDouble(STEP);
                        double t2 = Math.Pow(t, 2);
                        double t3 = Math.Pow(t, 3);
                        double h00 = 2 * t3 - 3 * t2 + 1;
                        double h10 = t3 - 2 * t2 + t;
                        double h01 = -2 * t3 + 3 * t2;
                        double h11 = t3 - t2;

                        double value = h00 * Y[i] + h10 * h * M[i] + h01 * Y[i + 1] + h11 * h * M[i + 1];
                        double key = X[i] + t * h;
                        this.m_dicPowerCurve.Add((float)key, (float)value);
                    }
                }

                return (int)emFunctionError_t.Success;
            }
            catch (Exception ex)
            {
                return (int)emFunctionError_t.Fail;
            }
        }

        /// <summary>
        /// Calculate Tangent value
        /// </summary>
        /// <param name="PowerTable"></param>
        /// <param name="listTangnets"></param>
        /// <returns></returns>
        private bool EstimateTangents(Dictionary<float, float> PowerTable, ref List<float> listTangnets)
        {
            try
            {
                int nSize = PowerTable.Count;
                List<float> delta = new List<float>();

                var PowerTable_sorted = from pair in PowerTable orderby pair.Key ascending select pair;

                // slopes;
                for (int i = 0; i < nSize-1; i++)
                {
                    KeyValuePair<float, float> Data1 = PowerTable_sorted.ElementAt(i);
                    KeyValuePair<float, float> Data2 = PowerTable_sorted.ElementAt(i+1);
                    //delta[i] = (Data2.Value - Data1.Value) / (Data2.Key - Data1.Key);
                    delta.Add((Data2.Value - Data1.Value) / (Data2.Key - Data1.Key));
                }

                // average tangent at data points
                listTangnets[nSize - 1] = delta[nSize - 2];
                for (int i = 1; i < nSize-1; i++)
                {
                    listTangnets[i] = (delta[i - 1] + delta[i]) / 2.0f;
                }

                for (int i = 0; i < nSize -1; i++)
                {
                    if (delta[i] == .0f)
                        listTangnets[i] = listTangnets[i + 1] = 0;
                    else
                    {
                        double a = listTangnets[i] / delta[i];
                        double b = listTangnets[i + 1] / delta[i];
                        double eps = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                        if (eps > 3)
                        {
                            double t = 3 * delta[i] / eps;
                            listTangnets[i] = (float)(t * a);
                            listTangnets[i + 1] = (float)(t * b);
                        }
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
