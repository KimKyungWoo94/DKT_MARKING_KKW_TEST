using EzIna.FA;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
    public partial class FrmInforOperationSemi_SXGA : Form
    {
        bool m_bMarkingStart = false;
        Stopwatch MarkingTacktimer = new Stopwatch();
        public FrmInforOperationSemi_SXGA()
        {
            InitializeComponent();
        }
        private void MarkingTackTimerFucnc()
        {
            while (m_bMarkingStart)
            {
                Thread.Sleep(1);
                if (MarkingTacktimer.IsRunning == false && RTC5.Instance.IsExecuteList_BUSY == true)
                {
                    if (!MarkingTacktimer.IsRunning)
                        MarkingTacktimer.Restart();
                }
                else if (MarkingTacktimer.IsRunning && RTC5.Instance.IsExecuteList_BUSY == false)
                {
                    MarkingTacktimer.Stop();
                    break;
                }
            }
            m_bMarkingStart = false;
        }

        private void btn_DM_Marking_NO_INSP_Click(object sender, EventArgs e)
        {
#if SIM
                        m_bMarkingStart = false;
#endif
            if (RTC5.Instance.IsExecuteList_BUSY == false && m_bMarkingStart == false)
            {
                double fPercentTemp = 0.0;
                double dNewCenterX = 0.0;
                double dNewCenterY = 0.0;
                float SizeX = 0.0f;
                float SizeY = 0.0f;
                EzCAM_Ver2.Hatch_Option pHatchOption = new EzCAM_Ver2.Hatch_Option();
                RTC5.Instance.ConfigData.FreQuency = RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() * 1000.0;
                RTC5.Instance.ConfigData.FreQPulseLength =
                RTC5.Instance.ConfigData.FreQHalfPeriod * 2.0 *
                RCP_Modify.PROCESS_SCANNER_DUTY_RATIO.GetValue<double>();

                RTC5.Instance.ConfigData.LaserOnDelay = RCP_Modify.PROCESS_SCANNER_LASER_ON_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.LaserOffDelay = RCP_Modify.PROCESS_SCANNER_LASER_OFF_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpDelay = RCP_Modify.PROCESS_SCANNER_JUMP_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.MarkDelay = RCP_Modify.PROCESS_SCANNER_MARK_DELAY.GetValue<double>();
                RTC5.Instance.ConfigData.JumpSpeed = RCP_Modify.PROCESS_SCANNER_JUMP_SPEED.GetValue<double>();
                RTC5.Instance.ConfigData.MarkSpeed = RCP_Modify.PROCESS_SCANNER_MARK_SPEED.GetValue<double>();

                if (FA.MGR.LaserMgr.IsExistFrequencyTable(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
                {
                    Laser.LaserPwrTableData pData = FA.MGR.LaserMgr.GetPwrTableData(RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                    pData.GetPercentFromPower(

                            RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp);
                    LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;
                    EzIna.DataMatrix.DMGenerater.Instance.DatamatrixSize =
                            RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();
                    pHatchOption.Type =
                            (EzCAM_Ver2.HATCH_TYPE)
                            ((int)RCP_Modify.PROCESS_DATA_MAT_HATCH_TYPE.GetValue<EzIna.DataMatrix.DM_HATCH_TYPE>());
                    pHatchOption.fPitch = RCP_Modify.PROCESS_DATA_MAT_HATCH_LinePitch.GetValue<float>();
                    pHatchOption.fAngle = RCP_Modify.PROCESS_DATA_MAT_HATCH_LineAngle.GetValue<float>();
                    pHatchOption.fOffset = RCP_Modify.PROCESS_DATA_MAT_HATCH_OffSet.GetValue<float>();
                    pHatchOption.bOutline = RCP_Modify.PROCESS_DATA_MAT_HATCH_Outline_Enable.GetValue<bool>();
                    EzIna.DataMatrix.DM pDataMat = FA.MGR.DMGenertorMgr.CreateDataMatrix("123");
                    pDataMat.HatchOption = pHatchOption.Clone();
                    dNewCenterX -= RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
                    dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;

                    SizeX = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<float>();
                    SizeY = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<float>();
                    pDataMat.CreateCodrdinates(new PointF((float)dNewCenterX
                            , (float)dNewCenterY), new SizeF(SizeX, SizeY));
                    RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    RTC5.Instance.MakeListFromGraphicsPath(pDataMat.CodeGraphicsPath, Scanner.ScanlabRTC5.RTC_LIST._1st, false, true);
                    RTC5.Instance.ListEnd();
                    MarkingTacktimer.Reset();
                    MarkingTacktimer.Stop();
                    m_bMarkingStart = true;
                    Thread pTackThread = new Thread(MarkingTackTimerFucnc);
                    RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);
                    pTackThread.Start();
                }
            }
        }

        private void btn_DM_Marking_INSP_Click(object sender, EventArgs e)
        {
            //FA.MGR.MESMgr.GetServoPingTest();

            // 2026.03.12 KKW DB 프로시저 TEST 추가
            System.Threading.Tasks.Task.Run(() =>
            {
                // string result = FA.MGR.MESMgr.RunMESConnectionTest("SPS00064NI15830", "VPOACAR21090001-SDI_R0 REV3.0");
                string result = FA.MGR.MESMgr.RunMESConnectionTest("P-63A0001", "VSSA OP 2603 A 0003-B8 SUB V2.0 DMC MARKING");
                this.InvokeIfNeeded(() => MessageBox.Show(result, "MES Connection Test Result",
                    MessageBoxButtons.OK, MessageBoxIcon.Information));
            });
        }

        // =====================================================================
        // 2026.03.11 KKW 더미 데이터 수동 마킹 및 검사 로직 추가
        // 버튼2: DM Marking + INSP
        // 목적: 실 프로세스 전에 더미 데이터로 마킹 -> 마킹 검사 순서를 공정 테스트
        // 마킹 데이터는 실제 프로세스처럼 MES에서 받지 않고 임의 텍스트를 사용
        // =====================================================================
        private volatile bool m_bTestMarkingInspRunning = false;
        private const string TEST_MARKING_TEXT = "TEST001";

        private void btn_DM_Marking_And_INSP_Click(object sender, EventArgs e)
        {
            if (m_bTestMarkingInspRunning)
            {
                MsgBox.Error("Test is already running. Please wait.");
                return;
            }
#if SIM
                m_bMarkingStart = false;
#else
            if (RTC5.Instance.IsExecuteList_BUSY)
            {
                MsgBox.Error("Marking is already in progress.");
                return;
            }
#endif
            if (!MsgBox.Confirm(
                    string.Format("DM Marking + Inspection Test\n\nTest Text: [{0}]\n\nStart?", TEST_MARKING_TEXT), this))
                return;

            System.Threading.Tasks.Task.Run(() =>
            {
                m_bTestMarkingInspRunning = true;
                try
                {
                    ExecuteTestMarkingAndInspection();
                }
                catch (Exception ex)
                {
                    FA.LOG.InfoJIG("TestMarking Exception: {0}", ex.Message);
                    this.InvokeIfNeeded(() => MsgBox.Error(string.Format("Test Error:\n{0}", ex.Message)));
                }
                finally
                {
                    m_bTestMarkingInspRunning = false;
                }
            });
        }

        private void ExecuteTestMarkingAndInspection() // 2026.03.11 KKW 더미 데이터 수동 마킹 및 검사 로직 추가
        {
            // ================================================================
            // 레시피 선택 여부 확인
            // RCP_Modify.* 값은 RecipeOpen() 시 메모리에 로드됨
            // 레시피가 선택되지 않으면 초기 기본값(0 등)으로 마킹 시도되어 위험
            // ================================================================
            if (string.IsNullOrWhiteSpace(FA.MGR.RecipeMgr.SelectedModel))
            {
                this.InvokeIfNeeded(() =>
                        MsgBox.Error("Recipe not selected.\nPlease select a recipe first."));
                return;
            }
            if (RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() <= 0)
            {
                this.InvokeIfNeeded(() =>
                        MsgBox.Error(string.Format(
                                "Recipe parameter invalid: [{0}] = {1}\nCheck recipe settings.",
                                RCP_Modify.PROCESS_SCANNER_FREQ.strCaption,
                                RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>())));
                return;
            }

            // ================================================================
            // STEP 1: 마킹 (더미 데이터 TEST_MARKING_TEXT 사용)
            // 실 프로세스의 마킹 파라미터를 그대로 사용
            // ================================================================
            FA.LOG.InfoJIG("TestMarking Start - Recipe:[{0}] Text:[{1}]",
                    FA.MGR.RecipeMgr.SelectedModel, TEST_MARKING_TEXT);

            double fPercentTemp = 0.0;
            double dNewCenterX = 0.0;
            double dNewCenterY = 0.0;
            float SizeX = 0.0f;
            float SizeY = 0.0f;
            EzCAM_Ver2.Hatch_Option pHatchOption = new EzCAM_Ver2.Hatch_Option();

#if SIM
#else
            RTC5.Instance.ConfigData.FreQuency =
                    RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<double>() * 1000.0;
            RTC5.Instance.ConfigData.FreQPulseLength =
                    RTC5.Instance.ConfigData.FreQHalfPeriod * 2.0 *
                    RCP_Modify.PROCESS_SCANNER_DUTY_RATIO.GetValue<double>();
            RTC5.Instance.ConfigData.LaserOnDelay =
                    RCP_Modify.PROCESS_SCANNER_LASER_ON_DELAY.GetValue<double>();
            RTC5.Instance.ConfigData.LaserOffDelay =
                    RCP_Modify.PROCESS_SCANNER_LASER_OFF_DELAY.GetValue<double>();
            RTC5.Instance.ConfigData.JumpDelay =
                    RCP_Modify.PROCESS_SCANNER_JUMP_DELAY.GetValue<double>();
            RTC5.Instance.ConfigData.MarkDelay =
                    RCP_Modify.PROCESS_SCANNER_MARK_DELAY.GetValue<double>();
            RTC5.Instance.ConfigData.JumpSpeed =
                    RCP_Modify.PROCESS_SCANNER_JUMP_SPEED.GetValue<double>();
            RTC5.Instance.ConfigData.MarkSpeed =
                    RCP_Modify.PROCESS_SCANNER_MARK_SPEED.GetValue<double>();

            if (!FA.MGR.LaserMgr.IsExistFrequencyTable(
                        RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
            {
                this.InvokeIfNeeded(() =>
                        MsgBox.Error("Frequency table not found. Check laser setup."));
                return;
            }

            Laser.LaserPwrTableData pPwrData =
                    FA.MGR.LaserMgr.GetPwrTableData(
                            RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
            if (pPwrData.GetPercentFromPower(
                    RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp) != 1)
            {
                this.InvokeIfNeeded(() =>
                        MsgBox.Error("Power table lookup failed.\nCheck laser power value."));
                return;
            }
            LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;

            // 실 프로세스 MARKING_LASER_PARAM_CHECK 와 동일:
            // GateMode / TriggerMode 설정 없이는 레이저가 발사되지 않음
            Stopwatch swLaserMode = Stopwatch.StartNew();
            while (FA.LASER.Instance.GateMode != Laser.GATE_MODE.EXT)
            {
                FA.LASER.Instance.GateMode = Laser.GATE_MODE.EXT;
                System.Threading.Thread.Sleep(10);
                if (swLaserMode.ElapsedMilliseconds > 3000)
                {
                    this.InvokeIfNeeded(() => MsgBox.Error("Laser GateMode EXT set timeout"));
                    return;
                }
            }
            while (FA.LASER.Instance.TriggerMode != Laser.TRIG_MODE.EXT)
            {
                LASER.Instance.TriggerMode = Laser.TRIG_MODE.EXT;
                System.Threading.Thread.Sleep(10);
                if (swLaserMode.ElapsedMilliseconds > 3000)
                {
                    this.InvokeIfNeeded(() => MsgBox.Error("Laser TriggerMode EXT set timeout"));
                    return;
                }
            }
            swLaserMode.Stop();

            // 실 프로세스 MARKING_LASER_ENABLE_FINISH 와 동일: Emission ON 확인
            if (FA.OPT.DryRunningEnable.m_bState == false)
            {
                Stopwatch swEmission = Stopwatch.StartNew();
                while (LASER.Instance.IsEmissionOn == false)
                {
                    FA.DEF.eDO.LASER_EM_ENABLE.GetDO().Value = true;
                    System.Threading.Thread.Sleep(10);
                    if (swEmission.ElapsedMilliseconds > 3000)
                    {
                        this.InvokeIfNeeded(() => MsgBox.Error("Laser Emission ON timeout"));
                        return;
                    }
                }
                swEmission.Stop();
            }
#endif

            EzIna.DataMatrix.DMGenerater.Instance.DatamatrixSize =
                    RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();

            pHatchOption.Type =
                    (EzCAM_Ver2.HATCH_TYPE)
                    ((int)RCP_Modify.PROCESS_DATA_MAT_HATCH_TYPE
                            .GetValue<EzIna.DataMatrix.DM_HATCH_TYPE>());
            pHatchOption.fPitch =
                    RCP_Modify.PROCESS_DATA_MAT_HATCH_LinePitch.GetValue<float>();
            pHatchOption.fAngle =
                    RCP_Modify.PROCESS_DATA_MAT_HATCH_LineAngle.GetValue<float>();
            pHatchOption.fOffset =
                    RCP_Modify.PROCESS_DATA_MAT_HATCH_OffSet.GetValue<float>();
            pHatchOption.bOutline =
                    RCP_Modify.PROCESS_DATA_MAT_HATCH_Outline_Enable.GetValue<bool>();

            EzIna.DataMatrix.DM pDataMat =
                    FA.MGR.DMGenertorMgr.CreateDataMatrix(TEST_MARKING_TEXT);
            pDataMat.HatchOption = pHatchOption.Clone();

            // 스캐너/비전 옵셋 적용 (실 프로세스와 동일)
            dNewCenterX -= RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
            dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;

            SizeX = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<float>();
            SizeY = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<float>();

            pDataMat.CreateCodrdinates(
                    new PointF((float)dNewCenterX, (float)dNewCenterY),
                    new SizeF(SizeX, SizeY));

#if SIM
                System.Threading.Thread.Sleep(200);
#else
            RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
            RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
            RTC5.Instance.MakeListFromGraphicsPath(
                    pDataMat.CodeGraphicsPath,
                    Scanner.ScanlabRTC5.RTC_LIST._1st, false, false);
            RTC5.Instance.ListEnd();
            RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);

            // 마킹 완료 대기 (최대 15초)
            Stopwatch swMarking = Stopwatch.StartNew();
            while (RTC5.Instance.IsExecuteList_BUSY)
            {
                System.Threading.Thread.Sleep(10);
                if (swMarking.ElapsedMilliseconds > 15000)
                {
                    FA.LOG.InfoJIG("TestMarking Timeout (Marking)");
                    this.InvokeIfNeeded(() => MsgBox.Error("Marking Timeout (15s)"));
                    return;
                }
            }
            swMarking.Stop();
#endif

            FA.LOG.InfoJIG("TestMarking Marking Done - Text: [{0}]", TEST_MARKING_TEXT);

            this.InvokeIfNeeded(() =>
                    MsgBox.Show(string.Format("DM Marking Test Done\n\nText: [{0}]", TEST_MARKING_TEXT)));

            // ================================================================
            // STEP 2: 마킹 완료 후 검사
            // 실 프로세스의 검사 파라미터(라이트, 카메라, Vision)를 그대로 사용
            // ================================================================
            FA.LOG.InfoJIG("TestMarking INSP Start");

#if SIM
#else
            // 라이트 설정 + 실 프로세스 MARKING_INSP_Condition_LIGHT_FINISH 와 동일:
            // SetIntensity 후 실제값 폴링 확인
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.LEFT,
                    RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.RIGHT,
                    RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.UP,
                    RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity(
                    (int)FA.DEF.LIGHT_CH.BOTTOM,
                    RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>());

            Stopwatch swLight = Stopwatch.StartNew();
            while (true)
            {
                if (LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.LEFT) == RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>() &&
                        LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.RIGHT) == RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>() &&
                        LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.UP) == RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>() &&
                        LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.BOTTOM) == RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>())
                    break;
                System.Threading.Thread.Sleep(10);
                if (swLight.ElapsedMilliseconds > 3000)
                    break; // 조명 컨트롤러 응답 없으면 그냥 진행
            }
            swLight.Stop();
#endif

            VISION.FINE_LIB.ClearMatrixCode1Results();

#if SIM
                System.Threading.Thread.Sleep(100);
#else
            // 카메라 그랩
            if (!FA.VISION.FINE_CAM.Grab())
            {
                FA.LOG.InfoJIG("TestMarking INSP: Grab Failed");
                this.InvokeIfNeeded(() => MsgBox.Error("Camera Grab Failed"));
                return;
            }

            // 그랩 완료 대기 (최대 5초)
            Stopwatch swGrab = Stopwatch.StartNew();
            while (!FA.VISION.FINE_CAM.IsGrab())
            {
                System.Threading.Thread.Sleep(10);
                if (swGrab.ElapsedMilliseconds > 5000)
                {
                    FA.LOG.InfoJIG("TestMarking INSP: Grab Timeout");
                    this.InvokeIfNeeded(() => MsgBox.Error("Camera Grab Timeout (5s)"));
                    return;
                }
            }
            swGrab.Stop();
#endif

            // ----------------------------------------------------------------
            // ROI 계산: 이미지 중앙 기준
            // 실 프로세스는 위치검사(MatchResult.SensorXPos/Y) 기준으로 ROI를 잡지만,
            // 테스트는 스캐너 센터(이미지 중앙)에 마킹했으므로 이미지 중앙을 ROI 중심으로 사용
            // ----------------------------------------------------------------
            double fPixelResX = RCP.M100_VisionCalFineScaleX.AsDouble / 1000.0;
            double fPixelResY = RCP.M100_VisionCalFineScaleY.AsDouble / 1000.0;
            double fMarkWidthPx = fPixelResX > 0
                    ? RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>() / fPixelResX
                    : 0;
            double fMarkHeightPx = fPixelResY > 0
                    ? RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>() / fPixelResY
                    : 0;

            double fImgCenterX = VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageW / 2.0;
            double fImgCenterY = VISION.FINE_LIB.m_LibInfo.m_stLibInfo.fImageH / 2.0;

            Rectangle[] testROIs = new Rectangle[]
            {
                        new Rectangle(
                                (int)(fImgCenterX - fMarkWidthPx),
                                (int)(fImgCenterY - fMarkHeightPx / 1.2),
                                (int)(fMarkWidthPx * 2.0),
                                (int)(fMarkHeightPx * 1.6))
            };

            string strTestSavePath = string.Format(
                    "d:\\PROC_IMG\\TEST\\{0}\\",
                    DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            // DataMatrix 검사 실행 (실 프로세스 MARKING_INSP_RUN_START 와 동일 방식)
            VISION.FINE_LIB.SetMatrixCode1ReadTimeout(30);
            VISION.FINE_LIB.MatrixCode1MultiRun(
                    (int)FA.DEF.eROI_CUSTOM.ROI_CUSTOM_01,
                    testROIs,
                    4.0f,
                    true,
                    strTestSavePath,
                    new string[] { TEST_MARKING_TEXT });

            // 검사 결과 대기 (최대 35초, 실 프로세스 MARKING_INSP_RUN_FINISH 방식)
            Stopwatch swInsp = Stopwatch.StartNew();
            while (VISION.FINE_LIB.GetMatrixCode1TotalResultCount() <= 0)
            {
                System.Threading.Thread.Sleep(100);
                if (swInsp.ElapsedMilliseconds > 35000)
                {
                    FA.LOG.InfoJIG("TestMarking INSP: Inspection Timeout");
                    this.InvokeIfNeeded(() => MsgBox.Error("Inspection Timeout (35s)"));
                    return;
                }
            }
            swInsp.Stop();

            // 결과 수집
            List<EzInaVision.GDV.MatrixCodeResult> pResultList;
            VISION.FINE_LIB.GetMatrixCode1ResultList(out pResultList);

            string strResult = "No Result";
            if (pResultList != null && pResultList.Count > 0)
            {
                var r = pResultList[0];
                if (r.m_bFound)
                {
                    if (string.Equals(TEST_MARKING_TEXT, r.m_strDecodedString))
                    {
                        strResult = string.Format(
                                "[OK]\nExpected : {0}\nRead     : {1}",
                                TEST_MARKING_TEXT, r.m_strDecodedString);
                    }
                    else
                    {
                        strResult = string.Format(
                                "[MIS MATCH]\nExpected : {0}\nRead     : {1}",
                                TEST_MARKING_TEXT, r.m_strDecodedString);
                    }
                }
                else
                {
                    strResult = string.Format(
                            "[NOT FOUND]\nExpected : {0}", TEST_MARKING_TEXT);
                }
            }

            FA.LOG.InfoJIG("TestMarking INSP Result: {0}",
                    strResult.Replace("\n", " | "));

            this.InvokeIfNeeded(() =>
                    MsgBox.Show(string.Format(
                            "DM Marking + INSP Test Result\n\n{0}", strResult)));
        }




        private void btn_ManualRun_Click(object sender, EventArgs e)
        {
            if (MsgBox.Confirm("Would you like to execute for the Manual Process"))
                FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToManual, 0, DEF.eManualMode.MANUAL_PROCESS);
        }
    }
}
