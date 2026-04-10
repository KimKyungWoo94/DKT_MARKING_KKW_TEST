using EzIna.FA;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text; // KKW Font Marking 폰트 추가
using System.IO; // KKW Font Marking 폰트 추가
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

            // KKW DB 프로시저 TEST 추가
            System.Threading.Tasks.Task.Run(() =>
            {
                string result = FA.MGR.MESMgr.RunMESConnectionTest("SPS00064NI15830", "VPOACAR21090001-SDI_R0 REV3.0");
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

            if (FA.VISION.FINE_CAM.IsGrab())
            {
                FA.VISION.FINE_CAM.Live();
                FA.LOG.InfoJIG("FINE_CAM Switch to Live on Grab");
            }
        }




        private void btn_ManualRun_Click(object sender, EventArgs e)
        {
            if (MsgBox.Confirm("Would you like to execute for the Manual Process"))
                FA.MGR.RunMgr.ModeChange(DEF.eRunMode.ToManual, 0, DEF.eManualMode.MANUAL_PROCESS);
        }

        // =====================================================================
        // KKW DM 마킹 + 폰트 마킹 + 검사 통합 테스트
        // 버튼4: DM+Font Marking + INSP
        // 목적: DM 마킹과 폰트 마킹을 동일 List에 묶어 한 번에 마킹 후 DM 검사 수행
        // 폰트는 DM 우측(gap 오프셋)에 배치되므로 DM 검사 ROI 범위 밖에 위치함
        // =====================================================================
        private volatile bool m_bDMFontMarkingRunning = false;

        private void btn_DMFont_Marking_INSP_Click(object sender, EventArgs e)
        {
#if !SIM
            if (RTC5.Instance.IsExecuteList_BUSY)
            {
                MsgBox.Error("Marking is already in progress.");
                return;
            }
#endif
            if (m_bDMFontMarkingRunning)
            {
                MsgBox.Error("DM+Font marking is already running.");
                return;
            }

            string fontInput = ShowTextInputDialog(
                "DM + Font Marking Test",
                "폰트 각인할 문자를 입력하세요 (최대 3글자):",
                "F5C");

            if (string.IsNullOrEmpty(fontInput))
                return;

            if (fontInput.Length > 3)
                fontInput = fontInput.Substring(0, 3);

            if (!MsgBox.Confirm(
                    string.Format(
                        "DM + Font Marking + Inspection Test\n\nDM Text : [{0}]\nFont Text: [{1}]\n\nStart?",
                        TEST_MARKING_TEXT, fontInput), this))
                return;

            System.Threading.Tasks.Task.Run(() =>
            {
                m_bDMFontMarkingRunning = true;
                try
                {
                    ExecuteDMAndFontMarkingTest(fontInput);
                }
                catch (Exception ex)
                {
                    FA.LOG.InfoJIG("DMFontMarking Exception: {0}", ex.Message);
                    this.InvokeIfNeeded(() =>
                        MsgBox.Error(string.Format("DM+Font Marking Error:\n{0}", ex.Message)));
                }
                finally
                {
                    m_bDMFontMarkingRunning = false;
                }
            });
        }

        private void ExecuteDMAndFontMarkingTest(string fontText)
        {
            // ================================================================
            // 레시피 / 파라미터 유효성 확인
            // ================================================================
            if (string.IsNullOrWhiteSpace(FA.MGR.RecipeMgr.SelectedModel))
            {
                this.InvokeIfNeeded(() =>
                    MsgBox.Error("Recipe not selected.\nPlease select a recipe first."));
                return;
            }
            if (!RCP_Modify.PROCESS_FONT_MARKING_ENABLE.GetValue<bool>())
            {
                this.InvokeIfNeeded(() =>
                    MsgBox.Error(
                        "Font Marking이 비활성화 상태입니다.\n" +
                        "Recipe → [Font Marking Enable] 을 ON으로 설정 후 다시 시도하세요."));
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

            FA.LOG.InfoJIG("DMFontMarkingTest Start - Recipe:[{0}] DMText:[{1}] FontText:[{2}]",
                FA.MGR.RecipeMgr.SelectedModel, TEST_MARKING_TEXT, fontText);

            // ================================================================
            // STEP 1: 레이저 파라미터 설정
            // ================================================================
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
            double fPercentTemp = 0.0;
            if (pPwrData.GetPercentFromPower(
                    RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPercentTemp) != 1)
            {
                this.InvokeIfNeeded(() =>
                    MsgBox.Error("Power table lookup failed.\nCheck laser power value."));
                return;
            }
            LASER.Instance.SetDiodeCurrent = (float)fPercentTemp * 100.0f;

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

            // ================================================================
            // STEP 2: DM GraphicsPath 생성
            // ================================================================
            EzCAM_Ver2.Hatch_Option pHatchOption = new EzCAM_Ver2.Hatch_Option();
            EzIna.DataMatrix.DMGenerater.Instance.DatamatrixSize =
                RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();
            pHatchOption.Type =
                (EzCAM_Ver2.HATCH_TYPE)
                ((int)RCP_Modify.PROCESS_DATA_MAT_HATCH_TYPE
                    .GetValue<EzIna.DataMatrix.DM_HATCH_TYPE>());
            pHatchOption.fPitch = RCP_Modify.PROCESS_DATA_MAT_HATCH_LinePitch.GetValue<float>();
            pHatchOption.fAngle = RCP_Modify.PROCESS_DATA_MAT_HATCH_LineAngle.GetValue<float>();
            pHatchOption.fOffset = RCP_Modify.PROCESS_DATA_MAT_HATCH_OffSet.GetValue<float>();
            pHatchOption.bOutline = RCP_Modify.PROCESS_DATA_MAT_HATCH_Outline_Enable.GetValue<bool>();

            EzIna.DataMatrix.DM pDataMat =
                FA.MGR.DMGenertorMgr.CreateDataMatrix(TEST_MARKING_TEXT);
            pDataMat.HatchOption = pHatchOption.Clone();

            double dNewCenterX = 0.0;
            double dNewCenterY = 0.0;
            dNewCenterX -= RCP.M100_CrossHairFine_ScannerAndVisionXOffset.AsSingle;
            dNewCenterY += RCP.M100_CrossHairFine_ScannerAndVisionYOffset.AsSingle;

            float SizeX = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<float>();
            float SizeY = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<float>();
            pDataMat.CreateCodrdinates(
                new PointF((float)dNewCenterX, (float)dNewCenterY),
                new SizeF(SizeX, SizeY));

            // ================================================================
            // STEP 3: Font GraphicsPath 생성 (DM 우측 배치)
            // 레이아웃: │← matWidth/2 →│← gap →│← textWidth/2 →│
            // ================================================================
            double matWidth = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>();
            double matHeight = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>();
            double textWidth = RCP_Modify.PROCESS_FONT_TEXT_WIDTH_MM.GetValue<double>();
            double gap = RCP_Modify.PROCESS_FONT_GAP_FROM_MATRIX_MM.GetValue<double>();
            double charSpacing = RCP_Modify.PROCESS_FONT_CHAR_SPACING_MM.GetValue<double>();

            int charCount = fontText.Length > 0 ? fontText.Length : 1;
            double charWidth = textWidth > 0 ? textWidth : 1.0;
            double charHeight = charCount > 1
                ? (matHeight - charSpacing * (charCount - 1)) / charCount
                : matHeight;
            if (charHeight <= 0) charHeight = 1.0;

            // DM 중심이 (dNewCenterX, dNewCenterY)이므로 폰트 X도 동일 기준으로 오프셋
            double textCenterX = dNewCenterX + matWidth / 2.0 + gap + textWidth / 2.0;

            FA.LOG.InfoJIG(
                "DMFontMarkingTest Layout - matW:{0:F2} matH:{1:F2} textW:{2:F2} gap:{3:F2} " +
                "=> charW:{4:F2} charH:{5:F2} textCenterX:{6:F2}",
                matWidth, matHeight, textWidth, gap,
                charWidth, charHeight, textCenterX);

            string rcpFontName = RCP_Modify.PROCESS_FONT_NAME != null
                ? RCP_Modify.PROCESS_FONT_NAME.GetValue<FA.DEF.eFontName>().ToString().Replace("_", "-")
                : "OCR-B";
            GraphicsPath textPath = CreateVerticalTextGraphicsPath(
                fontText,
                charWidthMM: charWidth,
                charHeightMM: charHeight,
                charSpacingMM: charSpacing,
                fontName: rcpFontName,
                bold: false,
                offsetXMM: textCenterX);

            if (textPath == null || textPath.PointCount == 0)
            {
                this.InvokeIfNeeded(() => MsgBox.Error("Font path creation failed."));
                return;
            }

            // ================================================================
            // STEP 4: 마킹 실행
            //   4-1) DM: PROCESS_SCANNER_MARK_SPEED 로 실행
            //   4-2) Font: PROCESS_FONT_MARK_SPEED 로 속도 변경 후 실행
            // ================================================================
#if SIM
            System.Threading.Thread.Sleep(300);
#else
            // --- 4-1: DM 마킹 ---
            RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
            RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
            RTC5.Instance.MakeListFromGraphicsPath(
                pDataMat.CodeGraphicsPath,
                Scanner.ScanlabRTC5.RTC_LIST._1st, false, false);
            RTC5.Instance.ListEnd();
            RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);

            Stopwatch swMarkingDM = Stopwatch.StartNew();
            while (RTC5.Instance.IsExecuteList_BUSY)
            {
                System.Threading.Thread.Sleep(10);
                if (swMarkingDM.ElapsedMilliseconds > 15000)
                {
                    FA.LOG.InfoJIG("DMFontMarkingTest Timeout (DM Marking)");
                    this.InvokeIfNeeded(() => MsgBox.Error("DM Marking Timeout (15s)"));
                    textPath.Dispose();
                    return;
                }
            }
            swMarkingDM.Stop();

            // --- 4-2: Font 마킹 (폰트 전용 속도로 변경 후 실행) ---
            RTC5.Instance.ConfigData.MarkSpeed =
                RCP_Modify.PROCESS_FONT_MARK_SPEED.GetValue<double>();

            RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
            RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
            RTC5.Instance.MakeListFromGraphicsPath(
                textPath,
                Scanner.ScanlabRTC5.RTC_LIST._1st, false, true);
            RTC5.Instance.ListEnd();
            RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);

            Stopwatch swMarkingFont = Stopwatch.StartNew();
            while (RTC5.Instance.IsExecuteList_BUSY)
            {
                System.Threading.Thread.Sleep(10);
                if (swMarkingFont.ElapsedMilliseconds > 15000)
                {
                    FA.LOG.InfoJIG("DMFontMarkingTest Timeout (Font Marking)");
                    this.InvokeIfNeeded(() => MsgBox.Error("Font Marking Timeout (15s)"));
                    textPath.Dispose();
                    return;
                }
            }
            swMarkingFont.Stop();
#endif
            textPath.Dispose();

            FA.LOG.InfoJIG("DMFontMarkingTest Marking Done - DM:[{0}] Font:[{1}]",
                TEST_MARKING_TEXT, fontText);
            this.InvokeIfNeeded(() =>
                MsgBox.Show(string.Format(
                    "DM + Font Marking Done\n\nDM Text : [{0}]\nFont Text: [{1}]",
                    TEST_MARKING_TEXT, fontText)));

            // ================================================================
            // STEP 5: DM 검사
            // 폰트는 DM ROI(matWidth×2 범위) 밖에 있으므로 검사에 간섭하지 않음
            // ================================================================
            FA.LOG.InfoJIG("DMFontMarkingTest INSP Start");

#if SIM
#else
            LIGHTSOURCE.BAR.SetIntensity((int)FA.DEF.LIGHT_CH.LEFT, RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity((int)FA.DEF.LIGHT_CH.RIGHT, RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity((int)FA.DEF.LIGHT_CH.UP, RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>());
            LIGHTSOURCE.BAR.SetIntensity((int)FA.DEF.LIGHT_CH.BOTTOM, RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>());

            Stopwatch swLight = Stopwatch.StartNew();
            while (true)
            {
                if (LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.LEFT) == RCP_Modify.LIGHT_Source_Lvl_L.GetValue<int>() &&
                    LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.RIGHT) == RCP_Modify.LIGHT_Source_Lvl_R.GetValue<int>() &&
                    LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.UP) == RCP_Modify.LIGHT_Source_Lvl_U.GetValue<int>() &&
                    LIGHTSOURCE.BAR.GetIntensity((int)FA.DEF.LIGHT_CH.BOTTOM) == RCP_Modify.LIGHT_Source_Lvl_B.GetValue<int>())
                    break;
                System.Threading.Thread.Sleep(10);
                if (swLight.ElapsedMilliseconds > 3000) break;
            }
            swLight.Stop();
#endif

            VISION.FINE_LIB.ClearMatrixCode1Results();

#if SIM
            System.Threading.Thread.Sleep(100);
#else
            if (!FA.VISION.FINE_CAM.Grab())
            {
                FA.LOG.InfoJIG("DMFontMarkingTest INSP: Grab Failed");
                this.InvokeIfNeeded(() => MsgBox.Error("Camera Grab Failed"));
                return;
            }

            Stopwatch swGrab = Stopwatch.StartNew();
            while (!FA.VISION.FINE_CAM.IsGrab())
            {
                System.Threading.Thread.Sleep(10);
                if (swGrab.ElapsedMilliseconds > 5000)
                {
                    FA.LOG.InfoJIG("DMFontMarkingTest INSP: Grab Timeout");
                    this.InvokeIfNeeded(() => MsgBox.Error("Camera Grab Timeout (5s)"));
                    return;
                }
            }
            swGrab.Stop();
#endif

            // ROI: 이미지 중앙 기준 (DM이 스캐너 센터에 마킹됨)
            // 폰트는 DM 우측 오프셋 배치라 이 ROI 밖에 위치
            double fPixelResX = RCP.M100_VisionCalFineScaleX.AsDouble / 1000.0;
            double fPixelResY = RCP.M100_VisionCalFineScaleY.AsDouble / 1000.0;
            double fMarkWidthPx = fPixelResX > 0
                ? RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>() / fPixelResX : 0;
            double fMarkHeightPx = fPixelResY > 0
                ? RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>() / fPixelResY : 0;

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

            VISION.FINE_LIB.SetMatrixCode1ReadTimeout(30);
            VISION.FINE_LIB.MatrixCode1MultiRun(
                (int)FA.DEF.eROI_CUSTOM.ROI_CUSTOM_01,
                testROIs,
                4.0f,
                true,
                strTestSavePath,
                new string[] { TEST_MARKING_TEXT });

            Stopwatch swInsp = Stopwatch.StartNew();
            while (VISION.FINE_LIB.GetMatrixCode1TotalResultCount() <= 0)
            {
                System.Threading.Thread.Sleep(100);
                if (swInsp.ElapsedMilliseconds > 35000)
                {
                    FA.LOG.InfoJIG("DMFontMarkingTest INSP: Inspection Timeout");
                    this.InvokeIfNeeded(() => MsgBox.Error("Inspection Timeout (35s)"));
                    return;
                }
            }
            swInsp.Stop();

            List<EzInaVision.GDV.MatrixCodeResult> pResultList;
            VISION.FINE_LIB.GetMatrixCode1ResultList(out pResultList);

            string strResult = "No Result";
            if (pResultList != null && pResultList.Count > 0)
            {
                var r = pResultList[0];
                if (r.m_bFound)
                {
                    strResult = string.Equals(TEST_MARKING_TEXT, r.m_strDecodedString)
                        ? string.Format("[OK]\nExpected : {0}\nRead     : {1}",
                            TEST_MARKING_TEXT, r.m_strDecodedString)
                        : string.Format("[MIS MATCH]\nExpected : {0}\nRead     : {1}",
                            TEST_MARKING_TEXT, r.m_strDecodedString);
                }
                else
                {
                    strResult = string.Format("[NOT FOUND]\nExpected : {0}", TEST_MARKING_TEXT);
                }
            }

            FA.LOG.InfoJIG("DMFontMarkingTest INSP Result: {0}", strResult.Replace("\n", " | "));
            this.InvokeIfNeeded(() =>
                MsgBox.Show(string.Format(
                    "DM+Font Marking + INSP Test Result\n\nFont Text: [{0}]\n\n{1}",
                    fontText, strResult)));

            if (FA.VISION.FINE_CAM.IsGrab())
            {
                FA.VISION.FINE_CAM.Live();
                FA.LOG.InfoJIG("FINE_CAM Switch to Live on Grab");
            }
        }

        // =====================================================================
        // Font Marking Test
        // 세로로 글자를 각인하는 테스트 버튼
        // =====================================================================
        private volatile bool m_bFontMarkingRunning = false;

        private void btn_FontMarking_Click(object sender, EventArgs e)//26.03.16 KKW Font Marking Parameter
        {
#if !SIM
            if (RTC5.Instance.IsExecuteList_BUSY)
            {
                MsgBox.Error("Marking is already in progress.");
                return;
            }
#endif
            if (m_bFontMarkingRunning)
            {
                MsgBox.Error("Font marking is already running.");
                return;
            }
            if (!RCP_Modify.PROCESS_FONT_MARKING_ENABLE.GetValue<bool>())
            {
                MsgBox.Error(
                    "Font Marking이 비활성화 상태입니다.\n" +
                    "Recipe → [Font Marking Enable] 을 ON으로 설정 후 다시 시도하세요.");
                return;
            }

            string input = ShowTextInputDialog(
                "Font Marking Test",
                "각인할 문자를 입력하세요 (최대 3글자):",
                "F5C");

            if (string.IsNullOrEmpty(input))
                return;

            if (input.Length > 3)
                input = input.Substring(0, 3);

            System.Threading.Tasks.Task.Run(() => ExecuteFontMarkingTest(input));
        }

        private void ExecuteFontMarkingTest(string text) //26.03.16 KKW Font Marking Parameter
        {
            if (!RCP_Modify.PROCESS_FONT_MARKING_ENABLE.GetValue<bool>())
            {
                this.InvokeIfNeeded(() =>
                    MsgBox.Error(
                        "Font Marking이 비활성화 상태입니다.\n" +
                        "Recipe → [Font Marking Enable] 을 ON으로 설정 후 다시 시도하세요."));
                return;
            }

            m_bFontMarkingRunning = true;
            try
            {
                FA.LOG.InfoJIG("FontMarkingTest Start - Text:[{0}]", text);

#if SIM
                System.Threading.Thread.Sleep(300);
#else
                // 레이저 파라미터 설정 (기존 마킹과 동일)
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
                // 폰트 마킹 전용 속도 사용 (PROCESS_FONT_MARK_SPEED)
                RTC5.Instance.ConfigData.MarkSpeed =
                    RCP_Modify.PROCESS_FONT_MARK_SPEED.GetValue<double>();

                if (FA.MGR.LaserMgr.IsExistFrequencyTable(
                    RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000))
                {
                    Laser.LaserPwrTableData pData = FA.MGR.LaserMgr.GetPwrTableData(
                        RCP_Modify.PROCESS_SCANNER_FREQ.GetValue<int>() * 1000);
                    double fPct = 0.0;
                    pData.GetPercentFromPower(
                        RCP_Modify.PROCESS_LASER_POWER.GetValue<double>(), out fPct);
                    LASER.Instance.SetDiodeCurrent = (float)fPct * 100.0f;
                }
#endif
                // -------------------------------------------------------
                // 세로 폰트 GraphicsPath 생성 - 레시피 파라미터 기반 배치 계산
                //
                // 레이아웃 (스캐너 좌표계, 데이터 매트릭스 중심 = 원점):
                //   │← matWidth →│← gap →│← textWidth(사용자 직접 입력) →│
                //
                //   charWidth   = PROCESS_FONT_TEXT_WIDTH_MM  (직접 설정)
                //   charHeight  = (matHeight - spacing*(n-1)) / n  (매트릭스 높이에 맞춤)
                //   textCenterX = matWidth/2 + gap + textWidth/2
                // -------------------------------------------------------
                double matWidth = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>();
                double matHeight = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>();
                double textWidth = RCP_Modify.PROCESS_FONT_TEXT_WIDTH_MM.GetValue<double>();
                double gap = RCP_Modify.PROCESS_FONT_GAP_FROM_MATRIX_MM.GetValue<double>();
                double charSpacing = RCP_Modify.PROCESS_FONT_CHAR_SPACING_MM.GetValue<double>();

                int charCount = text.Length > 0 ? text.Length : 1;
                double charWidth = textWidth > 0 ? textWidth : 1.0;
                double charHeight = charCount > 1
                    ? (matHeight - charSpacing * (charCount - 1)) / charCount
                    : matHeight;
                if (charHeight <= 0) charHeight = 1.0;

                // 텍스트 블록 X 중심 (스캐너 좌표계: 데이터 매트릭스 중심 기준)
                double textCenterX = matWidth / 2.0 + gap + textWidth / 2.0;

                FA.LOG.InfoJIG(
                    "FontMarkingTest Layout - matW:{0:F2} matH:{1:F2} textW:{2:F2} gap:{3:F2} " +
                    "=> charW:{4:F2} charH:{5:F2} textCenterX:{6:F2}",
                    matWidth, matHeight, textWidth, gap,
                    charWidth, charHeight, textCenterX);

                string rcpFontName = RCP_Modify.PROCESS_FONT_NAME != null
                    ? RCP_Modify.PROCESS_FONT_NAME.GetValue<FA.DEF.eFontName>().ToString().Replace("_", "-")
                    : "OCR-B";
                GraphicsPath textPath = CreateVerticalTextGraphicsPath(
                    text,
                    charWidthMM: charWidth,
                    charHeightMM: charHeight,
                    charSpacingMM: charSpacing,
                    fontName: rcpFontName,
                    bold: false,
                    offsetXMM: textCenterX
                );

                if (textPath == null || textPath.PointCount == 0)
                {
                    this.InvokeIfNeeded(() => MsgBox.Error("Text path creation failed."));
                    return;
                }

#if SIM
                System.Threading.Thread.Sleep(200);
#else
                RTC5.Instance.ListReset(Scanner.ScanlabRTC5.RTC_LIST._1st);
                RTC5.Instance.ListBegin(Scanner.ScanlabRTC5.RTC_LIST._1st);
                // a_YDirReverse = true: .NET 폰트는 Y 아래가 + → 스캐너는 Y 위가 +
                RTC5.Instance.MakeListFromGraphicsPath(
                    textPath,
                    Scanner.ScanlabRTC5.RTC_LIST._1st,
                    false, true);
                RTC5.Instance.ListEnd();
                RTC5.Instance.ListExecute(Scanner.ScanlabRTC5.RTC_LIST._1st);

                Stopwatch sw = Stopwatch.StartNew();
                while (RTC5.Instance.IsExecuteList_BUSY)
                {
                    System.Threading.Thread.Sleep(10);
                    if (sw.ElapsedMilliseconds > 15000)
                    {
                        FA.LOG.InfoJIG("FontMarkingTest Timeout");
                        this.InvokeIfNeeded(() => MsgBox.Error("Font Marking Timeout (15s)"));
                        return;
                    }
                }
#endif
                textPath.Dispose();

                FA.LOG.InfoJIG("FontMarkingTest Done - Text:[{0}]", text);
                this.InvokeIfNeeded(() =>
                    MsgBox.Show(string.Format("Font Marking Done\n\nText: [{0}]", text)));
            }
            catch (Exception ex)
            {
                FA.LOG.InfoJIG("FontMarkingTest Exception: {0}", ex.Message);
                this.InvokeIfNeeded(() =>
                    MsgBox.Error(string.Format("Font Marking Error:\n{0}", ex.Message)));
            }
            finally
            {
                m_bFontMarkingRunning = false;
            }
        }

        /// <summary>
        /// 세로 배치 폰트 GraphicsPath 생성.
        /// 각 글자를 위에서 아래로 쌓아서 하나의 경로로 반환합니다.
        /// 좌표계: 스캐너 중심(0,0) 기준, 스크린 Y(아래+) 방향으로 생성하며
        /// MakeListFromGraphicsPath 호출 시 a_YDirReverse=true 로 Y축 반전합니다.
        /// </summary>
        private GraphicsPath CreateVerticalTextGraphicsPath(
            string text,
            double charWidthMM,
            double charHeightMM,
            double charSpacingMM,
            string fontName = "Arial",
            bool bold = false,
            double offsetXMM = 0.0)
        {
            if (string.IsNullOrEmpty(text)) return null;

            GraphicsPath result = new GraphicsPath();
            // FontFamily ff;
            // try { ff = new FontFamily(fontName); }
            // catch { ff = new FontFamily("Arial"); }
            bool isPrivateFont;// KKW Font Marking 폰트 추가
            FontFamily ff = ResolveFontFamily(fontName, out isPrivateFont);// KKW Font Marking 폰트 추가

            FontStyle style = bold ? FontStyle.Bold : FontStyle.Regular;

            // 전체 블록의 세로 중심을 스캐너 원점(0)에 맞춤
            // (글자0이 위, 마지막 글자가 아래 → 스크린 좌표에서 Y 양수가 아래)
            double totalHeightMM = text.Length * charHeightMM
                                 + (text.Length - 1) * charSpacingMM;
            double blockTopY = -(totalHeightMM / 2.0);  // 첫 글자 상단 Y (스크린 좌표)

            for (int i = 0; i < text.Length; i++)
            {
                using (GraphicsPath charPath = new GraphicsPath())
                {
                    charPath.AddString(
                        text[i].ToString(),
                        ff,
                        (int)style,
                        100f,             // emSize: 임의 큰 값, 아래에서 mm로 스케일
                        PointF.Empty,
                        StringFormat.GenericTypographic);

                    RectangleF b = charPath.GetBounds();
                    if (b.Width <= 0 || b.Height <= 0)
                        continue;

                    // 이 글자의 목표 중심 Y (스크린 좌표, 아래가 +)
                    double charCenterY = blockTopY
                                       + i * (charHeightMM + charSpacingMM)
                                       + charHeightMM / 2.0;

                    float sx = (float)(charWidthMM / b.Width);
                    float sy = (float)(charHeightMM / b.Height);

                    // [1] 글리프 중심을 원점으로 이동
                    using (Matrix mCenter = new Matrix())
                    {
                        mCenter.Translate(-(b.X + b.Width / 2f), -(b.Y + b.Height / 2f));
                        charPath.Transform(mCenter);
                    }

                    // [2] mm 단위로 스케일
                    using (Matrix mScale = new Matrix())
                    {
                        mScale.Scale(sx, sy);
                        charPath.Transform(mScale);
                    }

                    // [3] 최종 위치로 이동 (X=offsetXMM 텍스트 중심, Y=스크린 좌표)
                    using (Matrix mMove = new Matrix())
                    {
                        mMove.Translate((float)offsetXMM, (float)charCenterY);
                        charPath.Transform(mMove);
                    }

                    result.AddPath(charPath, false);
                }
            }

            // ff.Dispose();
            // PrivateFontCollection 소유 폰트는 컬렉션이 관리하므로 직접 Dispose 하지 않음
            if (!isPrivateFont) ff.Dispose(); // KKW Font Marking 폰트 추가
            return result;
        }

        // =====================================================================
        // 프라이빗 폰트 컬렉션 (OCR-B 등 시스템 미설치 폰트 런타임 로드)
        // 실행파일 옆 Fonts\ 폴더의 .ttf/.otf 를 자동 등록
        // =====================================================================
        private static readonly PrivateFontCollection s_privateFonts = new PrivateFontCollection();
        private static bool s_fontsLoaded = false;
        private static readonly object s_fontLock = new object();

        private static void EnsurePrivateFontsLoaded()
        {
            if (s_fontsLoaded) return;
            lock (s_fontLock)
            {
                if (s_fontsLoaded) return;
                string fontDir = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Fonts");
                if (Directory.Exists(fontDir))
                {
                    foreach (string file in
                        Directory.GetFiles(fontDir, "*.ttf")
                                 .Concat(Directory.GetFiles(fontDir, "*.otf")))
                    {
                        try { s_privateFonts.AddFontFile(file); }
                        catch (Exception ex)
                        {
                            FA.LOG.InfoJIG(
                                "PrivateFont load failed [{0}]: {1}", file, ex.Message);
                        }
                    }
                    FA.LOG.InfoJIG("PrivateFonts loaded ({0} families) from [{1}]",
                        s_privateFonts.Families.Length, fontDir);
                }
                else
                {
                    FA.LOG.InfoJIG("PrivateFont dir not found: [{0}]", fontDir);
                }
                s_fontsLoaded = true;
            }
        }

        /// <summary>
        /// 이름으로 FontFamily를 반환
        /// Fonts\ 폴더에서 먼저 탐색하고 없으면 시스템 폰트, 그것도 없으면 Arial.
        /// isPrivate=true 이면 PrivateFontCollection 소유이므로 Dispose X
        /// </summary>
        private static FontFamily ResolveFontFamily(string fontName, out bool isPrivate)// KKW Font Marking 폰트 추가
        {
            EnsurePrivateFontsLoaded();
            foreach (FontFamily ff in s_privateFonts.Families)
            {
                FA.LOG.InfoJIG("PrivateFonts family loaded: [{0}]", ff.Name);

                if (string.Equals(ff.Name, fontName, StringComparison.OrdinalIgnoreCase))
                {
                    isPrivate = true;
                    FA.LOG.InfoJIG("ResolveFontFamily: requested=[{0}], resolved private=[{1}]", fontName, ff.Name);
                    return ff;
                }
            }
            isPrivate = false;
            // try { return new FontFamily(fontName); }
            // catch { return new FontFamily("Arial"); }
            try
            {
                var sys = new FontFamily(fontName);
                FA.LOG.InfoJIG("ResolveFontFamily: requested=[{0}], resolved system=[{1}]", fontName, sys.Name);
                return sys;
            }
            catch
            {
                var fallback = new FontFamily("Arial");
                FA.LOG.InfoJIG("ResolveFontFamily: requested=[{0}], fallback=[{1}]", fontName, fallback.Name);
                return fallback;
            }
        }

        // =====================================================================
        // Font Marking Preview
        // 실제 설비 없이 GraphicsPath 렌더링 결과를 시각적으로 확인
        // =====================================================================
        private void btn_FontMarkingPreview_Click(object sender, EventArgs e)
        {
            string input = ShowTextInputDialog(
                "Font Marking Preview",
                "미리볼 문자를 입력하세요 (최대 3글자):",
                "F5C");

            if (string.IsNullOrEmpty(input))
                return;

            if (input.Length > 3)
                input = input.Substring(0, 3);

            ShowFontMarkingPreviewDialog(input);
        }

        private void ShowFontMarkingPreviewDialog(string text)
        {
            double matWidth = RCP_Modify.PROCESS_DATA_MAT_WIDTH.GetValue<double>();
            double matHeight = RCP_Modify.PROCESS_DATA_MAT_HEIGHT.GetValue<double>();
            double textWidth = RCP_Modify.PROCESS_FONT_TEXT_WIDTH_MM.GetValue<double>();
            double gap = RCP_Modify.PROCESS_FONT_GAP_FROM_MATRIX_MM.GetValue<double>();
            double charSpacing = RCP_Modify.PROCESS_FONT_CHAR_SPACING_MM.GetValue<double>();

            int charCount = text.Length > 0 ? text.Length : 1;
            double charWidth = textWidth > 0 ? textWidth : 1.0;
            double charHeight = charCount > 1
                ? (matHeight - charSpacing * (charCount - 1)) / charCount
                : matHeight;
            if (charHeight <= 0) charHeight = 1.0;

            double textCenterX = matWidth / 2.0 + gap + textWidth / 2.0;

            // ── DataMatrix 셀 패턴 경로 생성 ──
            System.Drawing.Drawing2D.GraphicsPath dmCellPath = null;
            try
            {
                if (FA.MGR.DMGenertorMgr != null)
                {
                    EzIna.DataMatrix.DMGenerater.Instance.DatamatrixSize =
                        RCP_Modify.PROCESS_DATA_MAT_SIZE.GetValue<EzIna.DataMatrix.eDataMatrixSize>();
                    EzIna.DataMatrix.DM dm = FA.MGR.DMGenertorMgr.CreateDataMatrix(text);
                    if (dm != null)
                    {
                        dm.CreateCodrdinates(
                            PointF.Empty,
                            new SizeF((float)matWidth, (float)matHeight),
                            EzIna.DataMatrix.ZeroPosition.Center,
                            EzIna.DataMatrix.Rotate.R_0);
                        dmCellPath = dm.Get2dCodeGrapchicsPath();
                    }
                }
            }
            catch (Exception ex)
            {
                FA.LOG.InfoJIG("ShowFontMarkingPreviewDialog: DM path 생성 실패 [{0}]", ex.Message);
            }

            string initialFontName = RCP_Modify.PROCESS_FONT_NAME != null
                ? RCP_Modify.PROCESS_FONT_NAME.GetValue<FA.DEF.eFontName>().ToString().Replace("_", "-")
                : "OCR-B";
            using (GraphicsPath textPath = CreateVerticalTextGraphicsPath(
                text,
                charWidthMM: charWidth,
                charHeightMM: charHeight,
                charSpacingMM: charSpacing,
                fontName: initialFontName,
                bold: false,
                offsetXMM: textCenterX))
            {
                if (textPath == null || textPath.PointCount == 0)
                {
                    MsgBox.Error("Text path 생성 실패 (폰트 또는 파라미터 확인 필요).");
                    return;
                }

                using (Form dlg = new Form())
                {
                    dlg.Text = string.Format("Font Marking Preview  ─  [{0}]", text);
                    dlg.Width = 820;
                    dlg.Height = 790;
                    dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.MaximizeBox = false;
                    dlg.MinimizeBox = false;
                    dlg.BackColor = Color.FromArgb(245, 247, 250);

                    // ── 파라미터 정보 라벨 ──
                    Label lblInfo = new Label
                    {
                        Text = string.Format(
                            "matW={0:F2}   matH={1:F2}   textW={2:F2}   gap={3:F2}   spacing={4:F2}   " +
                            "→  charW={5:F2}  charH={6:F2}  textCX={7:F2}   (mm)",
                            matWidth, matHeight, textWidth, gap, charSpacing,
                            charWidth, charHeight, textCenterX),
                        Left = 10,
                        Top = 10,
                        Width = 790,
                        Height = 20,
                        Font = new Font("Consolas", 8.5f),
                        ForeColor = Color.DimGray,
                        AutoSize = false
                    };

                    Label lblNote = new Label
                    {
                        Text = "● 녹색 배경 = PCB 실크 영역  ● 파란 채움 = DM 셀  ● 파란 점선 = DM 영역  ● 빨간 실선 = 폰트 각인 경로  ● 빨간 점선 = 경로 외곽 범위  ● 그리드 1 mm",
                        Left = 10,
                        Top = 32,
                        Width = 790,
                        Height = 16,
                        Font = new Font("Malgun Gothic", 7.5f),
                        ForeColor = Color.SlateGray,
                        AutoSize = false
                    };

                    // ── 캔버스 패널 ──
                    Panel pnlCanvas = new Panel
                    {
                        Left = 10,
                        Top = 56,
                        Width = 790,
                        Height = 574,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    // ── PCB 영역 입력 ──
                    double[] pcbSizeFlag = { 0.0, 0.0 }; // [W, H], 0 이면 미표시

                    Label lblPcbTitle = new Label
                    {
                        Text = "PCB 영역 :",
                        Left = 10,
                        Top = 660,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 8.5f, FontStyle.Bold),
                        ForeColor = Color.DimGray
                    };
                    Label lblPcbW = new Label
                    {
                        Text = "W",
                        Left = 80,
                        Top = 662,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 8.5f)
                    };
                    TextBox tbPcbW = new TextBox
                    {
                        Left = 94,
                        Top = 658,
                        Width = 64,
                        Text = "0",
                        Font = new Font("Consolas", 9f)
                    };
                    Label lblPcbX = new Label
                    {
                        Text = "×",
                        Left = 116,
                        Top = 662,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 8.5f)
                    };
                    Label lblPcbH = new Label
                    {
                        Text = "H",
                        Left = 164,
                        Top = 662,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 8.5f)
                    };
                    TextBox tbPcbH = new TextBox
                    {
                        Left = 178,
                        Top = 658,
                        Width = 64,
                        Text = "0",
                        Font = new Font("Consolas", 9f)
                    };
                    Label lblPcbUnit = new Label
                    {
                        Text = "mm   (0 입력 시 미표시 / PCB 좌하 모서리 = 원점 0,0)",
                        Left = 250,
                        Top = 662,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 7.5f),
                        ForeColor = Color.SlateGray
                    };

                    EventHandler pcbChanged = (s, e) =>
                    {
                        double pw = 0, ph = 0;
                        double.TryParse(tbPcbW.Text, out pw);
                        double.TryParse(tbPcbH.Text, out ph);
                        pcbSizeFlag[0] = pw > 0 ? pw : 0;
                        pcbSizeFlag[1] = ph > 0 ? ph : 0;
                        pnlCanvas.Invalidate();
                    };
                    tbPcbW.TextChanged += pcbChanged;
                    tbPcbH.TextChanged += pcbChanged;

                    // ── 마킹 위치 오프셋 입력 (DM + Font 공통) ──
                    double[] markOffFlag = { 0.0, 0.0 }; // [0]=X  [1]=Y

                    Label lblMarkOff = new Label
                    {
                        Text = "DM 위치 :",
                        Left = 10,
                        Top = 692,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 8.5f, FontStyle.Bold),
                        ForeColor = Color.DimGray
                    };
                    Label lblOffX = new Label { Text = "X", Left = 80, Top = 694, AutoSize = true, Font = new Font("Malgun Gothic", 8.5f) };
                    TextBox tbOffX = new TextBox { Left = 94, Top = 690, Width = 64, Text = "0", Font = new Font("Consolas", 9f) };
                    Label lblOffY = new Label { Text = "Y", Left = 164, Top = 694, AutoSize = true, Font = new Font("Malgun Gothic", 8.5f) };
                    TextBox tbOffY = new TextBox { Left = 178, Top = 690, Width = 64, Text = "0", Font = new Font("Consolas", 9f) };
                    Label lblOffUnit = new Label
                    {
                        Text = "mm   (PCB 설정 시: PCB 좌하 기준 DM 좌하 모서리 위치 / PCB 미설정: 원점 기준 이동)",
                        Left = 250,
                        Top = 694,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 7.5f),
                        ForeColor = Color.SlateGray
                    };

                    EventHandler offChanged = (s, e) =>
                    {
                        double ox = 0, oy = 0;
                        double.TryParse(tbOffX.Text, out ox);
                        double.TryParse(tbOffY.Text, out oy);
                        markOffFlag[0] = ox;
                        markOffFlag[1] = oy;
                        pnlCanvas.Invalidate();
                    };
                    tbOffX.TextChanged += offChanged;
                    tbOffY.TextChanged += offChanged;

                    dlg.Controls.Add(lblInfo);
                    dlg.Controls.Add(lblNote);
                    dlg.Controls.Add(pnlCanvas);
                    dlg.Controls.Add(lblPcbTitle);
                    dlg.Controls.Add(lblPcbW);
                    dlg.Controls.Add(tbPcbW);
                    dlg.Controls.Add(lblPcbX);
                    dlg.Controls.Add(lblPcbH);
                    dlg.Controls.Add(tbPcbH);
                    dlg.Controls.Add(lblPcbUnit);
                    dlg.Controls.Add(lblMarkOff);
                    dlg.Controls.Add(lblOffX);
                    dlg.Controls.Add(tbOffX);
                    dlg.Controls.Add(lblOffY);
                    dlg.Controls.Add(tbOffY);
                    dlg.Controls.Add(lblOffUnit);

                    // 캡처한 값들을 람다에서 사용하기 위해 로컬 변수로 고정
                    double _matW = matWidth, _matH = matHeight;
                    double _textCX = textCenterX, _textW = textWidth;
                    double[] _pcbSize = pcbSizeFlag;    // [0]=W [1]=H
                    double[] _markOff = markOffFlag;    // [0]=X [1]=Y  (DM 중심 위치)
                    // _paths[0] = 현재 폰트 경로  (폰트 변경 시 재생성)
                    GraphicsPath[] _paths = { (GraphicsPath)textPath.Clone() };
                    GraphicsPath _dmPath = dmCellPath != null ? (GraphicsPath)dmCellPath.Clone() : null;

                    // ── 폰트 선택 드롭다운 ──
                    Label lblFont = new Label
                    {
                        Text = "폰트 :",
                        Left = 10,
                        Top = 641,
                        AutoSize = true,
                        Font = new Font("Malgun Gothic", 8.5f, FontStyle.Bold),
                        ForeColor = Color.DimGray
                    };
                    ComboBox cmbFont = new ComboBox
                    {
                        Left = 70,
                        Top = 635,
                        Width = 160,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Font = new Font("Consolas", 9f)
                    };
                    string[] fontCandidates = new[] { "OCR-B", "Arial", "Consolas" };
                    foreach (string fn in fontCandidates)
                        cmbFont.Items.Add(fn);
                    string rcpFontInit = RCP_Modify.PROCESS_FONT_NAME != null
                        ? RCP_Modify.PROCESS_FONT_NAME.GetValue<FA.DEF.eFontName>().ToString().Replace("_", "-")
                        : "OCR-B";
                    int rcpFontIdx = Array.IndexOf(fontCandidates, rcpFontInit);
                    cmbFont.SelectedIndex = rcpFontIdx >= 0 ? rcpFontIdx : 0;

                    cmbFont.SelectedIndexChanged += (s, e) =>
                    {
                        string selFont = cmbFont.SelectedItem?.ToString() ?? "OCR-B";
                        GraphicsPath np = CreateVerticalTextGraphicsPath(
                            text,
                            charWidthMM: charWidth,
                            charHeightMM: charHeight,
                            charSpacingMM: charSpacing,
                            fontName: selFont,
                            bold: false,
                            offsetXMM: textCenterX);
                        _paths[0]?.Dispose(); _paths[0] = np;
                        pnlCanvas.Invalidate();
                    };
                    dlg.Controls.Add(lblFont);
                    dlg.Controls.Add(cmbFont);

                    pnlCanvas.Paint += (s, pe) =>
                    {
                        Graphics g = pe.Graphics;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                        GraphicsPath activePath = _paths[0];

                        // ── 월드 좌표 범위 계산 (스캐너 mm, Y+: 위) ──
                        // PCB 설정 시: PCB 좌하 모서리 = 원점(0,0)
                        //   markOff = DM 좌하 모서리 위치 (DM의 (0,0)이 PCB (0,0)에 대응)
                        //   → DM 중심 = markOff + (matW/2, matH/2)
                        // PCB 미설정: DM 중심 = 원점(0,0), markOff = 추가 이동량
                        const double kMargin = 2.0;
                        double pcbW = _pcbSize[0], pcbH = _pcbSize[1];
                        bool hasPcb = pcbW > 0 && pcbH > 0;
                        // DM 중심 월드 좌표 (PCB 모드이면 좌하 모서리 기준에서 중심으로 변환)
                        double dmCX = _markOff[0] + (hasPcb ? _matW / 2.0 : 0.0);
                        double dmCY = _markOff[1] + (hasPcb ? _matH / 2.0 : 0.0);
                        double wLeft, wRight, wTop, wBottom;
                        if (hasPcb)
                        {
                            wLeft = Math.Min(0.0, dmCX - _matW / 2.0) - kMargin;
                            wRight = Math.Max(pcbW, dmCX + _textCX + _textW / 2.0) + kMargin;
                            wTop = Math.Max(pcbH, dmCY + _matH / 2.0) + kMargin;
                            wBottom = Math.Min(0.0, dmCY - _matH / 2.0) - kMargin;
                        }
                        else
                        {
                            wLeft = dmCX - _matW / 2.0 - kMargin;
                            wRight = dmCX + _textCX + _textW / 2.0 + kMargin;
                            wTop = dmCY + _matH / 2.0 + kMargin;
                            wBottom = dmCY - _matH / 2.0 - kMargin;
                        }

                        float cW = pnlCanvas.Width - 2;
                        float cH = pnlCanvas.Height - 2;

                        float scaleX = cW / (float)(wRight - wLeft);
                        float scaleY = cH / (float)(wTop - wBottom);
                        float scale = Math.Min(scaleX, scaleY) * 0.92f;

                        // 월드 중심 → 캔버스 중심
                        float cx = cW / 2f;
                        float cy = cH / 2f;
                        double worldCX = (wLeft + wRight) / 2.0;
                        double worldCY = (wTop + wBottom) / 2.0;

                        Func<double, float> toSX = wx => cx + (float)(wx - worldCX) * scale;
                        // 스캐너 Y+: 위 → 화면 Y-: 위이므로 부호 반전
                        Func<double, float> toSY = wy => cy - (float)(wy - worldCY) * scale;

                        // ── PCB 실크 배경 (가장 먼저 그려서 DM·Font 아래에 위치) ──
                        // PCB 좌하 모서리 = 월드 원점(0,0),  우상 = (pcbW, pcbH)
                        if (hasPcb)
                        {
                            float pbgX = toSX(0.0);
                            float pbgY = toSY(pcbH);
                            float pbgW = (float)(pcbW * scale);
                            float pbgH = (float)(pcbH * scale);

                            // PCB 기판 배경 (연한 녹색 – 실제 PCB 색 느낌)
                            using (SolidBrush pcbFill = new SolidBrush(Color.FromArgb(45, 30, 140, 60)))
                                g.FillRectangle(pcbFill, pbgX, pbgY, pbgW, pbgH);

                            // PCB 테두리
                            using (Pen pcbBorder = new Pen(Color.FromArgb(220, 40, 160, 80), 1.8f))
                                g.DrawRectangle(pcbBorder, pbgX, pbgY, pbgW, pbgH);

                            using (Font pcbFont = new Font("Consolas", 7.5f))
                            using (SolidBrush pcbDimBrush = new SolidBrush(Color.FromArgb(220, 40, 160, 80)))
                            using (Pen pcbDimPen = new Pen(Color.FromArgb(220, 40, 160, 80), 1f))
                            {
                                // ── PCB 가로 치수 (아래쪽, y=0 기준) ──
                                float dimY = toSY(0.0) + 14;
                                float dimX1 = toSX(0.0);
                                float dimX2 = toSX(pcbW);
                                g.DrawLine(pcbDimPen, dimX1, dimY, dimX2, dimY);
                                // 끝 세리프
                                g.DrawLine(pcbDimPen, dimX1, dimY - 4, dimX1, dimY + 4);
                                g.DrawLine(pcbDimPen, dimX2, dimY - 4, dimX2, dimY + 4);
                                string pcbWLabel = string.Format("PCB W={0:F2}mm", pcbW);
                                SizeF pwSz = g.MeasureString(pcbWLabel, pcbFont);
                                g.DrawString(pcbWLabel, pcbFont, pcbDimBrush,
                                    (dimX1 + dimX2) / 2f - pwSz.Width / 2f, dimY + 2);

                                // ── PCB 세로 치수 (오른쪽, x=pcbW 기준) ──
                                float dimX = toSX(pcbW) + 14;
                                float dimYT = toSY(pcbH);
                                float dimYB = toSY(0.0);
                                g.DrawLine(pcbDimPen, dimX, dimYT, dimX, dimYB);
                                g.DrawLine(pcbDimPen, dimX - 4, dimYT, dimX + 4, dimYT);
                                g.DrawLine(pcbDimPen, dimX - 4, dimYB, dimX + 4, dimYB);
                                string pcbHLabel = string.Format("PCB H={0:F2}mm", pcbH);
                                SizeF phSz = g.MeasureString(pcbHLabel, pcbFont);
                                g.TranslateTransform(dimX + 2, (dimYT + dimYB) / 2f + phSz.Width / 2f);
                                g.RotateTransform(-90);
                                g.DrawString(pcbHLabel, pcbFont, pcbDimBrush, 0, 0);
                                g.ResetTransform();

                                // PCB 라벨 + 원점 표시
                                g.DrawString("PCB", pcbFont, pcbDimBrush, pbgX + 3, pbgY + 3);
                                g.DrawString("(0,0)", pcbFont, pcbDimBrush,
                                    toSX(0.0) + 3, toSY(0.0) - 14);
                            }
                        }

                        // ── 1mm 그리드 ──
                        using (Pen gridPen = new Pen(Color.FromArgb(220, 220, 220), 1f))
                        {
                            gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            for (double x = Math.Floor(wLeft); x <= wRight + 0.5; x += 1.0)
                            {
                                float sx = toSX(x);
                                g.DrawLine(gridPen, sx, 0, sx, cH);
                            }
                            for (double y = Math.Floor(wBottom); y <= wTop + 0.5; y += 1.0)
                            {
                                float sy = toSY(y);
                                g.DrawLine(gridPen, 0, sy, cW, sy);
                            }
                        }

                        // ── 원점 축 ──
                        using (Pen axisPen = new Pen(Color.FromArgb(180, 180, 180), 1f))
                        {
                            g.DrawLine(axisPen, toSX(wLeft), toSY(0), toSX(wRight), toSY(0));
                            g.DrawLine(axisPen, toSX(0), toSY(wTop), toSX(0), toSY(wBottom));
                        }

                        // ── DM 중심 좌표 (PCB 모드: 좌하 모서리 기준 → 중심 변환 완료) ──
                        double dmOX = dmCX, dmOY = dmCY;
                        double foOX = dmCX, foOY = dmCY;

                        // ── DataMatrix 사각형 (DM 오프셋 적용) ──
                        using (Pen matPen = new Pen(Color.RoyalBlue, 1.5f))
                        {
                            matPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            float rx = toSX(-_matW / 2.0 + dmOX);
                            float ry = toSY(_matH / 2.0 + dmOY);
                            float rw = (float)(_matW * scale);
                            float rh = (float)(_matH * scale);
                            g.DrawRectangle(matPen, rx, ry, rw, rh);
                            g.DrawString("DataMatrix",
                                new Font("Arial", 7f), Brushes.RoyalBlue, rx + 2, ry + 2);
                        }

                        // ── DataMatrix 셀 패턴 렌더링 (DM 오프셋 적용) ──
                        // DM path는 스캐너 Y-up 좌표계 → Y 부호 반전, 오프셋은 toSX/toSY 통해 적용
                        if (_dmPath != null && _dmPath.PointCount > 0)
                        {
                            using (GraphicsPath drawDM = (GraphicsPath)_dmPath.Clone())
                            {
                                float ox = toSX(dmOX);
                                float oy = toSY(dmOY);
                                using (Matrix m = new Matrix(scale, 0, 0, -scale, ox, oy))
                                    drawDM.Transform(m);
                                using (SolidBrush dmFill = new SolidBrush(Color.FromArgb(200, 30, 90, 210)))
                                    g.FillPath(dmFill, drawDM);
                            }
                        }

                        // ── 텍스트 경로 렌더링 (Font 오프셋 적용) ──
                        // path_x = world_x(mm), path_y = -world_y
                        // Font 오프셋은 원점 이동으로 반영: toSX(foOX), toSY(foOY)
                        using (GraphicsPath drawPath = (GraphicsPath)activePath.Clone())
                        {
                            float ox = toSX(foOX);
                            float oy = toSY(foOY);
                            using (Matrix m = new Matrix(scale, 0, 0, scale, ox, oy))
                                drawPath.Transform(m);

                            using (Pen textPen = new Pen(Color.Red, 1.5f))
                                g.DrawPath(textPen, drawPath);
                        }

                        // ── 레시피 설정 경로 외곽 박스 (빨간 점선, Font 오프셋 적용) ──
                        // path_x = world_x, path_y = -world_y
                        {
                            RectangleF pb = _paths[0].GetBounds();
                            if (pb.Width > 0 && pb.Height > 0)
                            {
                                float pbLeft = toSX((double)pb.Left + foOX);
                                float pbRight = toSX((double)pb.Right + foOX);
                                float pbTop = toSY(-(double)pb.Top + foOY);
                                float pbBottom = toSY(-(double)pb.Bottom + foOY);
                                float pbW = pbRight - pbLeft;
                                float pbH = pbBottom - pbTop;
                                if (pbW > 0 && pbH > 0)
                                {
                                    using (Pen boundsPen = new Pen(Color.Red, 1.2f))
                                    using (Font boundsFont = new Font("Arial", 7f))
                                    {
                                        boundsPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                                        g.DrawRectangle(boundsPen, pbLeft, pbTop, pbW, pbH);
                                        g.DrawString(
                                            string.Format("Font Marking  W={0:F2}  H={1:F2}", pb.Width, pb.Height),
                                            boundsFont, Brushes.Red, pbLeft + 2, pbBottom + 2);
                                    }
                                }
                            }
                        }

                        // ── 눈금 라벨 ──
                        using (Font tickFont = new Font("Arial", 7f))
                        using (SolidBrush tickBrush = new SolidBrush(Color.DimGray))
                        {
                            for (double x = Math.Ceiling(wLeft); x <= wRight; x += 1.0)
                            {
                                if (Math.Abs(x) < 0.1) continue;
                                g.DrawString(x.ToString("0"), tickFont, tickBrush,
                                    toSX(x) - 7, toSY(0) + 3);
                            }
                            for (double y = Math.Ceiling(wBottom); y <= wTop; y += 1.0)
                            {
                                if (Math.Abs(y) < 0.1) continue;
                                g.DrawString(y.ToString("0"), tickFont, tickBrush,
                                    toSX(0) + 3, toSY(y) - 8);
                            }
                        }

                        // ── 치수 표시: DataMatrix 폭/높이 (DM 오프셋 적용) ──
                        using (Font dimFont = new Font("Arial", 7.5f))
                        using (SolidBrush dimBrush = new SolidBrush(Color.CornflowerBlue))
                        {
                            float rx = toSX(-_matW / 2.0 + dmOX);
                            float ryB = toSY(-_matH / 2.0 + dmOY);
                            string dmLabel = string.Format("DM  W={0:F2}  H={1:F2}", _matW, _matH);
                            g.DrawString(dmLabel, dimFont, dimBrush, rx + 2, ryB + 2);
                        }
                    };

                    dlg.FormClosed += (s, fe) =>
                    {
                        _paths[0]?.Dispose();
                        _dmPath?.Dispose();
                    };
                    dlg.ShowDialog(this);
                }
            }
            dmCellPath?.Dispose();
        }

        /// <summary>
        /// 간단한 텍스트 입력 다이얼로그
        /// 취소하면 null 반환
        /// </summary>
        private string ShowTextInputDialog(string title, string prompt, string defaultValue)
        {
            string result = null;
            using (Form dlg = new Form())
            {
                dlg.Text = title;
                dlg.Width = 420;
                dlg.Height = 165;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                Label lbl = new Label
                {
                    Text = prompt,
                    Left = 12,
                    Top = 14,
                    Width = 390,
                    AutoSize = false
                };
                TextBox tb = new TextBox
                {
                    Text = defaultValue,
                    Left = 12,
                    Top = 38,
                    Width = 380,
                    MaxLength = 3,
                    Font = new Font("Arial", 14f, FontStyle.Bold)
                };
                Button btnOK = new Button
                {
                    Text = "확인",
                    Left = 220,
                    Top = 82,
                    Width = 80,
                    DialogResult = DialogResult.OK
                };
                Button btnCancel = new Button
                {
                    Text = "취소",
                    Left = 310,
                    Top = 82,
                    Width = 80,
                    DialogResult = DialogResult.Cancel
                };

                dlg.Controls.AddRange(new Control[] { lbl, tb, btnOK, btnCancel });
                dlg.AcceptButton = btnOK;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                    result = tb.Text.Trim();
            }
            return result;
        }
    }
}
