using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Net.NetworkInformation;

// ODAC 다운로드, 설치
// https://www.oracle.com/database/technologies/dotnet-odacmsi-downloads.html
// 참조 추가 - Oracle.ManagedDataAccess


namespace EzIna
{

    public struct PROCEDURE_PARAM_START
    {
        public object SendData;
        public object Result;
        public object MarkingInfo;
        public object Message;
    }

    public struct PROCEDURE_PARAM_END
    {
        public object SendData;
        public object Result;
        public object Message;
    }
    public class DKT_MES_Manager : SingleTone<DKT_MES_Manager>
    {
        public string LastExceptionString = string.Empty;
        public string LastRawPValue = string.Empty; //2026.03.12 KKW MES 통신 테스트 함수 추가
        public string ConnectionString = string.Empty;
        public string Address = string.Empty;
        public string Port = string.Empty;

        private OracleCommand LastExcutedCommand = null;
        private int RetryCount = 0;
        private Queue<OracleCommand> m_pSendCmdList;
        public OracleConnection Connection { get; set; }
        private bool m_bConnectionAlive = false;
        public bool bConnectionAlive { get { return m_bConnectionAlive; } }

        protected override void OnCreateInstance()
        {
            DBConfigManager.InitDBConfig();
            m_pSendCmdList = new Queue<OracleCommand>();
            base.OnCreateInstance();
        }
        /// <summary>
        /// DB 접속
        /// </summary>
        /// <returns></returns>
        public bool DoConnect()
        {
            try
            {
                if (this.Connection != null)
                {
                    this.Connection.Close();
                    this.Connection.Dispose();
                    this.Connection = null;
                }

                if (ConnectionString == string.Empty)
                    SetConnectionInfo();

                Connection = new OracleConnection(ConnectionString);

                if (this.Address != string.Empty)
                    Connection.Open();
            }
            catch (System.Exception ex)
            {
                //System.Reflection.MemberInfo Info = System.Reflection.MethodInfo.GetCurrentMethod();
                //string Id = string.Format("{0}.{1}", Info.ReflectedType.Name, Info.Name);

                LastExceptionString = ex.ToString();
                return false;
            }

            if (Connection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }
        public bool IsConnected
        {
            get
            {
                bool bRet = false;
                if (Connection != null)
                {
                    switch (Connection.State)
                    {
                        case ConnectionState.Open:
                        case ConnectionState.Connecting:
                        case ConnectionState.Executing:
                        case ConnectionState.Fetching:
                            bRet = true;
                            break;
                        case ConnectionState.Closed:
                        case ConnectionState.Broken:
                            bRet = false;
                            break;
                        default:
                            break;
                    }
                }

                return bRet;
            }
        }
        public bool IsCmdExecutingAble
        {
            get
            {
                bool bRet = false;
                if (Connection != null)
                {
                    bRet = Connection.State == ConnectionState.Open;
                }
                return bRet;
            }
        }
        public void ExecuteQueue(object a_pObj)
        {
            try
            {
                if (m_pSendCmdList != null && m_pSendCmdList.Count > 0)
                {
                    if (this.IsConnected)
                    {
                        OracleCommand cmd = m_pSendCmdList.Dequeue();
                        if (cmd != null)
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
            }
        }
        /// <summary>
        /// Send Command
        /// </summary>
        /// <param name="strCommand"></param>
        /// <returns></returns>
        public bool SendCommand(string strCommand)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = this.Connection;
                cmd.CommandText = strCommand;

                // test 필요
                OracleDataReader reader = cmd.ExecuteReader();
                this.LastExcutedCommand = cmd;

                // To do
                // return message 확인               
                return true;
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
                return false;
            }


        }
        public bool SendMarkingStart(string a_strLotCode, string a_strJJIGCode, out DKT_MES_MarkingStartInfo a_BufMarkingStartInfo)
        {
            try
            {
                a_BufMarkingStartInfo = null;
                OracleCommand cmd = new OracleCommand();

                cmd.Connection = this.Connection;
                cmd.InitialLONGFetchSize = 1000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PKG_DATA_TRANSFER.SET_MARKING_START";

                cmd.Parameters.Add("P_DATA_VALUE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_VALUE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;  // 2026.03.12 KKW 100→1000 (BV 응답 ~109자, 버퍼 초과 시 잘림 확인)
                cmd.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 1020).Direction = ParameterDirection.Output; // 2026.03.12 KKW 100→1020 (P_MESSAGE = "RETURN VALUE : " + P_VALUE)
                cmd.Parameters["P_DATA_VALUE"].Value = string.Format("[{0},{1}]", a_strLotCode, a_strJJIGCode);//procStart.SendData;

                // test 필요
                //OracleDataAdapter oraAdapter = new OracleDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                this.LastExcutedCommand = cmd;
                object strCmdExeResult = cmd.Parameters["P_RESULT"].Value;
                object strMarkingInfo = cmd.Parameters["P_VALUE"].Value;
                object strMessage = cmd.Parameters["P_MESSAGE"].Value;
                //procStart.Result = cmd.Parameters["P_RESULT"].Value;
                //procStart.MarkingInfo = cmd.Parameters["P_VALUE"].Value;
                //procStart.Message = cmd.Parameters["P_MESSAGE"].Value;
                LastRawPValue = strMarkingInfo.ToString();// 2026.03.12 KKW MES 통신 테스트 함수 추가
                a_BufMarkingStartInfo = new DKT_MES_MarkingStartInfo();
                a_BufMarkingStartInfo.SetCommMSG(strMessage.ToString());
                if (strCmdExeResult.ToString() == "OK")
                {
                    if (a_BufMarkingStartInfo.SetMarkingInfoData(strMarkingInfo.ToString()))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
                a_BufMarkingStartInfo = null;
                return false;
            }
        }
        public bool SendMarkingReport(string a_strLotCode, string a_strJIGCode, RunningDataItem a_MarkingData, string a_strMarkingText, out DKT_MES_MarkingReportInfo a_BufMarkingReportInfo)
        {
            try
            {
                a_BufMarkingReportInfo = null;
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = this.Connection;
                cmd.InitialLONGFetchSize = 1000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PKG_DATA_TRANSFER.SET_MARKING_DATA";

                cmd.Parameters.Add("P_MARKING_TIME", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_MARKING_DATA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_LOT_NO", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_JIG_NO", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_MARKING_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;

                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;


                //cmd.Parameters["P_DATA_VALUE"].Value = string.Format("[{0},{1},{2},{3}]", a_strLotCode, a_strJIGCode, strStartNo, strEndNo);
                //Marking Time (YYYY-MM-DD HH:mm:dd)
                cmd.Parameters["P_MARKING_TIME"].Value = a_MarkingData.pDataMatrix != null ? a_MarkingData.MarkingDoneTime.ToString("yyyy-MM-dd HH:mm:ss") :
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Laser Marking str

                // Laser LOT NO
                cmd.Parameters["P_LOT_NO"].Value = a_strLotCode;
                // Laser 
                cmd.Parameters["P_JIG_NO"].Value = a_strJIGCode;

                if (a_MarkingData != null && a_MarkingData.pDataMatrix != null)
                {
                    cmd.Parameters["P_MARKING_DATA"].Value = a_MarkingData.pDataMatrix.DatamatrixText;
                    //cmd.Parameters["P_MARKING_DATA"].Value ="NONE";
                }
                else
                {
                    cmd.Parameters["P_MARKING_DATA"].Value = "NONE";
                    //cmd.Parameters["P_MARKING_DATA"].Value =a_strMarkingText;
                }
                if (a_MarkingData != null && a_MarkingData.bMarkingDone && a_MarkingData.pDataMatrix != null)
                {
                    if (a_MarkingData.pMatrixCodeResult != null)
                    {
                        if (a_MarkingData.pMatrixCodeResult.m_bFound)
                        {
                            cmd.Parameters["P_MARKING_RESULT"].Value =
                                    string.Equals(a_MarkingData.pDataMatrix.DatamatrixText,
                                                                a_MarkingData.pMatrixCodeResult.m_strDecodedString
                                    ) == true ? "OK" : "MISS";
                        }
                        else
                        {
                            cmd.Parameters["P_MARKING_RESULT"].Value = "NG";
                        }
                    }
                    else
                    {
                        cmd.Parameters["P_MARKING_RESULT"].Value = "NG";
                    }
                }
                else
                {
                    cmd.Parameters["P_MARKING_RESULT"].Value = "NONE";
                }
                // test 필요
                //OracleDataAdapter oraAdapter = new OracleDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                this.LastExcutedCommand = cmd;

                object strCmdExeResult = cmd.Parameters["P_RESULT"].Value;
                object strMessage = cmd.Parameters["P_MESSAGE"].Value;

                if (strCmdExeResult.ToString() == "OK")
                {
                    a_BufMarkingReportInfo = new EzIna.DKT_MES_MarkingReportInfo();
                    a_BufMarkingReportInfo.SetCommMSG(strMessage.ToString());
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
                a_BufMarkingReportInfo = null;
                return false;
            }
        }


        public bool SendMarkingReportForUnit(string a_strLotCode, string a_strJIGCode, string a_strMarkingCode, RunningDataItem[] a_MarkingDataes, out DKT_MES_MarkingReportInfo a_BufMarkingReportInfo)
        {
            try
            {
                a_BufMarkingReportInfo = null;
                if (a_MarkingDataes == null || a_MarkingDataes.Length <= 0)
                    return false;

                string strMarkingSN = "";
                string strMarkingResults = "";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = this.Connection;
                cmd.InitialLONGFetchSize = 1000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PKG_DATA_TRANSFER.SET_MARKING_DATA";

                cmd.Parameters.Add("P_MARKING_TIME", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_MARKING_DATA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_LOT_NO", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_JIG_NO", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_MARKING_SN", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Input;     // 2026.03.12 KKW 유닛 40개 × 7자 = 280자, 100→500
                cmd.Parameters.Add("P_MARKING_RESULT", OracleDbType.Varchar2, 300).Direction = ParameterDirection.Input; // 2026.03.12 KKW 유닛 40개 × "MISS," = 200자, 100→300
                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                //cmd.Parameters["P_DATA_VALUE"].Value = string.Format("[{0},{1},{2},{3}]", a_strLotCode, a_strJIGCode, strStartNo, strEndNo);
                //Marking Time (YYYY-MM-DD HH:mm:dd)
                cmd.Parameters["P_MARKING_TIME"].Value = a_MarkingDataes[0].pDataMatrix != null ? a_MarkingDataes[0].MarkingDoneTime.ToString("yyyy-MM-dd HH:mm:ss") :
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Laser Marking str
                cmd.Parameters["P_MARKING_DATA"].Value = a_strMarkingCode;
                // Laser LOT NO
                cmd.Parameters["P_LOT_NO"].Value = a_strLotCode;
                // Laser 
                cmd.Parameters["P_JIG_NO"].Value = a_strJIGCode;

                for (int i = 0; i < a_MarkingDataes.Length; i++)
                {
                    if (a_MarkingDataes[i] != null)
                    {
                        if (i == 0)
                        {
                            strMarkingSN = a_MarkingDataes[i].strMarkingIDX;
                        }
                        else
                        {
                            strMarkingSN = string.Format("{0},{1}", strMarkingSN, a_MarkingDataes[i].strMarkingIDX);
                        }
                        if (a_MarkingDataes[i].bMarkingDone && a_MarkingDataes[i].pDataMatrix != null)
                        {
                            if (a_MarkingDataes[i].pMatrixCodeResult != null)
                            {
                                if (a_MarkingDataes[i].pMatrixCodeResult.m_bFound)
                                {
                                    if (i == 0)
                                    {
                                        strMarkingResults = string.Equals(a_MarkingDataes[i].pDataMatrix.DatamatrixText,
                                                                                a_MarkingDataes[i].pMatrixCodeResult.m_strDecodedString
                                                                                ) == true ? "OK" : "MISS";
                                    }
                                    else
                                    {
                                        strMarkingResults = string.Format("{0},{1}",
                                                strMarkingResults,
                                                string.Equals(a_MarkingDataes[i].pDataMatrix.DatamatrixText,
                                                                                a_MarkingDataes[i].pMatrixCodeResult.m_strDecodedString
                                                                                ) == true ? "OK" : "MISS");
                                    }
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        strMarkingResults = "NG";
                                    }
                                    else
                                    {
                                        strMarkingResults = string.Format("{0},{1}", strMarkingResults, "NG");
                                    }
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    strMarkingResults = "NG";
                                }
                                else
                                {
                                    strMarkingResults = string.Format("{0},{1}", strMarkingResults, "NG");
                                }
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                strMarkingResults = "NONE";
                            }
                            else
                            {
                                strMarkingResults = string.Format("{0},{1}", strMarkingResults, "NONE");
                            }
                        }
                    }
                }
                cmd.Parameters["P_MARKING_SN"].Value = strMarkingSN;
                cmd.Parameters["P_MARKING_RESULT"].Value = strMarkingResults;
                // test 필요
                //OracleDataAdapter oraAdapter = new OracleDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                this.LastExcutedCommand = cmd;
                object strCmdExeResult = cmd.Parameters["P_RESULT"].Value;
                object strMessage = cmd.Parameters["P_MESSAGE"].Value;
                a_BufMarkingReportInfo = new EzIna.DKT_MES_MarkingReportInfo();
                a_BufMarkingReportInfo.SetCommMSG(strMessage.ToString());
                if (strCmdExeResult.ToString() == "OK")
                {
                    for (int i = 0; i < a_MarkingDataes.Length; i++)
                    {
                        a_MarkingDataes[i].bMESUnitReport = true;
                    }
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
                a_BufMarkingReportInfo = null;
                return false;
            }
        }
        public void SendMarkingReportQueue(string a_strLotCode, string a_strJIGCode, string a_strMarkingText, RunningDataItem a_MarkingData)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = this.Connection;
                cmd.InitialLONGFetchSize = 1000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PKG_DATA_TRANSFER.SET_MARKING_DATA";

                cmd.Parameters.Add("P_MARKING_TIME", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_MARKING_DATA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_LOT_NO", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_JIG_NO", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_MARKING_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Input;

                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;


                //cmd.Parameters["P_DATA_VALUE"].Value = string.Format("[{0},{1},{2},{3}]", a_strLotCode, a_strJIGCode, strStartNo, strEndNo);
                //Marking Time (YYYY-MM-DD HH:mm:dd)
                cmd.Parameters["P_MARKING_TIME"].Value = a_MarkingData.pDataMatrix != null ? a_MarkingData.MarkingDoneTime.ToString("yyyy-MM-dd HH:mm:ss") :
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Laser Marking str

                // Laser LOT NO
                cmd.Parameters["P_LOT_NO"].Value = a_strLotCode;
                // Laser 
                cmd.Parameters["P_JIG_NO"].Value = a_strJIGCode;

                if (a_MarkingData != null && a_MarkingData.pDataMatrix != null)
                {
                    cmd.Parameters["P_MARKING_DATA"].Value = a_MarkingData.pDataMatrix.DatamatrixText;
                    //cmd.Parameters["P_MARKING_DATA"].Value ="NONE";
                }
                else
                {
                    cmd.Parameters["P_MARKING_DATA"].Value = "NONE";
                    //cmd.Parameters["P_MARKING_DATA"].Value =a_strMarkingText;
                }
                if (a_MarkingData != null && a_MarkingData.bMarkingDone && a_MarkingData.pDataMatrix != null)
                {
                    if (a_MarkingData.pMatrixCodeResult != null)
                    {
                        if (a_MarkingData.pMatrixCodeResult.m_bFound)
                        {
                            cmd.Parameters["P_MARKING_RESULT"].Value =
                                    string.Equals(a_MarkingData.pDataMatrix.DatamatrixText,
                                                                a_MarkingData.pMatrixCodeResult.m_strDecodedString
                                    ) == true ? "OK" : "MISS";
                        }
                        else
                        {
                            cmd.Parameters["P_MARKING_RESULT"].Value = "NG";
                        }
                    }
                    else
                    {
                        cmd.Parameters["P_MARKING_RESULT"].Value = "NG";
                    }
                }
                else
                {
                    cmd.Parameters["P_MARKING_RESULT"].Value = "NONE";
                }
                m_pSendCmdList.Enqueue(cmd);
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
            }
        }
        // 2026.03.23 KKW MES END Stored Procedure 변경에 따른 함수 수정
        public bool SendMarkingEnd(string a_strLotCode, string a_strJIGCode,
            string a_strCommand, string a_strMarkingBody,
            int a_iStartNo, int a_iEndNo,
            IReadOnlyList<string> a_serialList,
            out DKT_MES_MarkingEndInfo a_BufMarkingEndInfo)
        {
            try
            {
                a_BufMarkingEndInfo = null;
                OracleCommand cmd = new OracleCommand();

                string strDataValue = "";
                if (a_strCommand.ToUpper() == "BV" && a_serialList != null && a_serialList.Count > 0)
                {
                    // [BV,LOT_NO,JIG_NO,MARKING_BODY,SN1,SN2,...,SNn]
                    string strSerials = string.Join(",", a_serialList);
                    strDataValue = string.Format("[{0},{1},{2},{3},{4}]",
                        a_strCommand, a_strLotCode, a_strJIGCode, a_strMarkingBody, strSerials);
                }
                else
                {
                    // [V,LOT_NO,JIG_NO,MARKING_BODY,START_NO,END_NO]
                    string strStartNo = string.Format("{0:D5}", a_iStartNo);
                    string strEndNo = string.Format("{0:D5}", a_iEndNo);
                    strDataValue = string.Format("[{0},{1},{2},{3},{4},{5}]",
                        a_strCommand, a_strLotCode, a_strJIGCode, a_strMarkingBody, strStartNo, strEndNo);
                }

                cmd.Connection = this.Connection;
                cmd.InitialLONGFetchSize = 1000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PKG_DATA_TRANSFER.SET_MARKING_END";

                cmd.Parameters.Add("P_DATA_VALUE", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters["P_DATA_VALUE"].Value = strDataValue;

                cmd.ExecuteNonQuery();

                this.LastExcutedCommand = cmd;

                object strCmdExeResult = cmd.Parameters["P_RESULT"].Value;
                object strMessage = cmd.Parameters["P_MESSAGE"].Value;

                a_BufMarkingEndInfo = new EzIna.DKT_MES_MarkingEndInfo();
                a_BufMarkingEndInfo.SetCommMSG(strMessage.ToString());
                if (strCmdExeResult.ToString() == "OK")
                {
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
                a_BufMarkingEndInfo = null;
                return false;
            }
        }
        public void SendMarkingEndQueue(string a_strLotCode, string a_strJIGCode,
            string a_strCommand, string a_strMarkingBody,
            int a_iStartNo, int a_iEndNo,
            IReadOnlyList<string> a_serialList)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                string strDataValue = "";
                if (a_strCommand.ToUpper() == "BV" && a_serialList != null && a_serialList.Count > 0)
                {
                    // [BV,LOT_NO,JIG_NO,MARKING_BODY,SN1,SN2,...,SNn]
                    string strSerials = string.Join(",", a_serialList);
                    strDataValue = string.Format("[{0},{1},{2},{3},{4}]",
                        a_strCommand, a_strLotCode, a_strJIGCode, a_strMarkingBody, strSerials);
                }
                else
                {
                    // [V,LOT_NO,JIG_NO,MARKING_BODY,START_NO,END_NO]
                    string strStartNo = string.Format("{0:D5}", a_iStartNo);
                    string strEndNo = string.Format("{0:D5}", a_iEndNo);
                    strDataValue = string.Format("[{0},{1},{2},{3},{4},{5}]",
                        a_strCommand, a_strLotCode, a_strJIGCode, a_strMarkingBody, strStartNo, strEndNo);
                }

                cmd.Connection = this.Connection;
                cmd.InitialLONGFetchSize = 1000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PKG_DATA_TRANSFER.SET_MARKING_END";

                cmd.Parameters.Add("P_DATA_VALUE", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                cmd.Parameters["P_DATA_VALUE"].Value = strDataValue;
                m_pSendCmdList.Enqueue(cmd);
            }
            catch (System.Exception ex)
            {
                this.LastExceptionString = ex.ToString();
            }

        }
        /// <summary>
        /// Config.xml 에서 접속 정보를 읽어온다
        /// </summary>
        private void SetConnectionInfo()
        {
            string user = DBConfigManager.GetValue("DATABASE", "USER");
            string pwd = DBConfigManager.GetValue("DATABASE", "PWD");
            string port = DBConfigManager.GetValue("DATABASE", "PORT");
            string sid = DBConfigManager.GetValue("DATABASE", "SID");
            string svr = DBConfigManager.GetValue("DATABASE", "SERVICE_NAME");
            string add01 = DBConfigManager.GetValue("DATABASE", "D_ADDR01");
            string add02 = DBConfigManager.GetValue("DATABASE", "D_ADDR02");

            svr = string.Empty;

            string address01 = string.Format("(ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))", add01, port);
            string address02 = string.Format("(ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))", add02, port);

            string dataSource = string.Format(@"(DESCRIPTION = (ADDRESS_LIST = {0}{1})(CONNECT_DATA = (", address01, address02);
            dataSource += svr == string.Empty ? string.Format("SID = {0})))", sid) : string.Format("SERVICE_NAME = {0})))", svr);

            this.Address = add01;
            this.Port = port;
            this.ConnectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + dataSource;
        }
        /// <summary>
        /// 2026.03.12 KKW MES 통신 테스트 함수 추가
        /// MES 통신 전체 흐름 테스트 (Ping → Connect → Start → End)
        /// </summary>
        public string RunMESConnectionTest(string a_strLotCode, string a_strJIGCode)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                sb.AppendLine("=== MES TEST START ===");

                // 1. Ping 테스트 (네트워크 도달 여부)
                string pingAddr = DBConfigManager.GetValue("DATABASE", "D_ADDR01");
                sb.AppendLine($"[1] Ping → {pingAddr}");
                GetServoPingTest(null);
                sb.AppendLine(m_bConnectionAlive ? "    → Ping 성공" : "    → Ping 실패 (VPN 연결 또는 IP 확인 필요)");
                if (!m_bConnectionAlive)
                {
                    sb.AppendLine("=== MES TEST ABORT ===");
                    return sb.ToString();
                }

                // 2. DB 연결
                sb.AppendLine("[2] DoConnect...");
                bool bConn = DoConnect();
                sb.AppendLine(bConn ? "    → 연결 성공" : $"    → 연결 실패: {LastExceptionString}");
                if (!bConn) return sb.ToString();

                // 3. SET_MARKING_START (LOT/JIG 코드가 있을 때만 실행)
                if (!string.IsNullOrEmpty(a_strLotCode) && !string.IsNullOrEmpty(a_strJIGCode))
                {
                    sb.AppendLine($"[3] SET_MARKING_START (LOT={a_strLotCode}, JIG={a_strJIGCode})");
                    DKT_MES_MarkingStartInfo startInfo;
                    bool bStart = SendMarkingStart(a_strLotCode, a_strJIGCode, out startInfo);
                    // 원본 P_VALUE 문자열 출력 (파싱 전 raw 값)
                    sb.AppendLine($"    [RAW P_VALUE] {LastRawPValue}");

                    if (bStart && startInfo != null)
                    {
                        sb.AppendLine($"    → OK");
                        sb.AppendLine($"    Type       : {startInfo.strMarkkingInfo_Type}");
                        sb.AppendLine($"    ModelName  : {startInfo.strMarkingInfo_ModelName}");
                        sb.AppendLine($"    MarkingData: {startInfo.strMarkingInfo_MarkingData}");
                        sb.AppendLine($"    Cipher     : {startInfo.iMarkingInfo_Cipher}");
                        sb.AppendLine($"    ZeroPad    : {startInfo.iMarkingInfo_ZeroPad}");
                        if (startInfo.bTargetDMCstringExist)
                        {
                            sb.AppendLine($"    NumList ({startInfo.pMarkingTargetNumList.Count}개):");
                            for (int i = 0; i < startInfo.pMarkingTargetNumList.Count; i++)
                                sb.AppendLine($"      [{i}] '{startInfo.pMarkingTargetNumList[i]}' (len={startInfo.pMarkingTargetNumList[i].Length})");
                        }
                        else
                        {
                            sb.AppendLine($"    StartNo: {startInfo.iMarkingInfo_StartNo}, EndNo: {startInfo.iMarkingInfo_EndNo}");
                        }
                    }
                    else
                    {
                        sb.AppendLine($"    → 실패: {LastExceptionString}");
                        sb.AppendLine($"    CommMSG: {startInfo?.strCommMSG}");
                    }

                    // 4. SET_MARKING_END
                    sb.AppendLine($"[4] SET_MARKING_END (StartNo=1, EndNo=1)");
                    DKT_MES_MarkingEndInfo endInfo;
                    // bool bEnd = SendMarkingEnd(a_strLotCode, a_strJIGCode, "V", "", 1, 11, null, out endInfo);
                    bool bEnd = SendMarkingEnd("C-6260001", "VSSEINS 2601 A 0003-B8 SUB V2.0 ET 40CH", "V", "P01100070ADUC8BB0AM991010", 701, 720, null, out endInfo);
                    sb.AppendLine(bEnd ? $"    → OK / MSG: {endInfo?.strCommMSG}" : $"    → 실패: {LastExceptionString}");
                }
                else
                {
                    sb.AppendLine("[3] SET_MARKING_START → LOT/JIG 코드 없음, 생략");
                }

                sb.AppendLine("=== MES TEST END ===");
            }
            catch (Exception ex)
            {
                sb.AppendLine($"[EXCEPTION] {ex.Message}");
            }
            return sb.ToString();
        }

        public void GetServoPingTest(object a_pObj)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions(600, false);
            System.Net.IPAddress pIPAddr;
            byte[] bufferArray = new byte[32];


            string port = DBConfigManager.GetValue("DATABASE", "PORT");
            string add01 = DBConfigManager.GetValue("DATABASE", "D_ADDR01");


            if (System.Net.IPAddress.TryParse(add01, out pIPAddr))
            {
                int timeout = 1000;

                try
                {
                    PingReply reply = pingSender.Send(pIPAddr, timeout, bufferArray, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        m_bConnectionAlive = true;
                    }
                    else
                    {
                        m_bConnectionAlive = false;
                    }
                }
                catch
                {
                    m_bConnectionAlive = false;
                }

            }


        }



        /*
private bool CheckDBConnected()
{
    string query = "";
    OracleDataReader result = null;

    try
    {
        OracleCommand cmd   = new OracleCommand();
        cmd.Connection      = this.Connection;
        cmd.CommandText     = query;
        result              = cmd.ExecuteReader();
    }
    catch (System.Exception ex)
    {
        LastExceptionString = ex.ToString();
    }

    if (result != null && result.HasRows)
        return true;

    return false;
}
*/

    }
}

