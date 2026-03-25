using System.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EzIna.FA
{
		public delegate void Event_Str(string s);
		public delegate void Event_Int(int n);
		public delegate void Event_None();


		public static class FRM
		{
				public static Form FrmMainForm;
				public static FrmInforOperationAuto_SXGA FrmInforOperationAuto;
				public static FrmInforOperAuto_ProcessStatus FrmInforOperProcessStatus;
				public static FrmInforOperAuto_TactTimeStatus FrmInforOperTactTimeStatus;
				public static FrmInforOperationSemi_SXGA FrmInforOperationSemi;

				
				public static FrmInforRecipePanel_SXGA FrmInforRecipeMain;
				public static FrmInforManualVision_SXGA FrmInforManualVision;
				public static FrmInforSetupInitialProcess_SXGA FrmInforSetupInitialProcess;
				public static FrmInforSetupMotion_SXGA FrmInforSetupMotion;
				public static FrmInforSetupIO_SXGA FrmInforSetupIO;
				public static FrmInforSetupCylinder_SXGA FrmInforSetupCylinder;
				public static FrmInforSetupLaser_SXGA FrmInforSetupLaser;
				public static FrmInforSetupAttenuator_SXGA FrmInforSetupAttenuator;
				public static FrmInforSetupScanner_SXGA FrmInforSetupScanner;
				public static FrmInforSetupPowerMeter_SXGA FrmInforSetupPOMeter;


				public static FrmInforLogEventlog_SXGA FrmInforLogEventLog;
				public static FrmInforLogWorklog_SXGA FrmInforLogWorkLog;

				public static FrmPopupLogMC FrmLogMC;
				public static FrmPopupLogSW FrmLogSW;
				public static FrmPopupLogPM FrmLogPM;
				public static FrmPopupLaserWarmupAlarm FrmLaserWarmupAlarm;
				public static FrmPopupInitialize FrmInitialize;
				public static FrmPopupAlarm FrmAlarm;
				public static FrmPopupError FrmError;
				public static FrmVision_SXGA FrmVision;

				#region [ Tab For Initial Process ]
				public static FrmTabInitialProcessConfigSet FrmTabInitProcConfigSet;
				public static FrmTabInitialProcessVisionCal FrmTabInitProcVisionCal;
				public static FrmTabInitialProcessPowerTable FrmTabInitProcPowerTable;
				public static FrmTabInitialProcessStageCal FrmTabInitProcStageCal;
				public static FrmTabInitialProcessCrossHair FrmTabInitProcCrossHair;
				public static FrmTabInitialProcessFindFocus FrmTabInitProcFindFocus;
				public static FrmTabInitialProcessFindCPU FrmTabInitProcFindCPU;
				public static FrmTabInitialProcessGalvoCal FrmTabInitProcGalvoCal;
				public static FrmTabInitialProcessTowerLamp FrmTabInitProcTowerLamp;
				#endregion
				#region [ Tab For Recipe ]
				public static FrmTabRecipeConfiguration FrmTabRecipeConfig;
				public static FrmTabRecipeProcessRecipe FrmTabRecipeProcRecipe;
				#endregion


				#region [ Tab For New Recipe ]
				public static FrmTabRecipePanelCommon FrmTabNewRecipeCommon;
				public static FrmTabRecipePanelVision FrmTabNewRecipeVision;
				public static FrmTabRecipePanelProcess FrmTabNewRecipeProcess;
				#endregion

				public static void Init()
				{


						FrmInforOperationAuto = new FrmInforOperationAuto_SXGA();
						FrmInforOperProcessStatus= new FrmInforOperAuto_ProcessStatus();
						FrmInforOperTactTimeStatus= new FrmInforOperAuto_TactTimeStatus();
						FrmInforOperationSemi = new FrmInforOperationSemi_SXGA();

						
						FrmInforRecipeMain = new FrmInforRecipePanel_SXGA();
						#region [Manual]
						FrmInforManualVision = new FrmInforManualVision_SXGA();
						#endregion
						#region [Setup]	  
						FrmInforSetupInitialProcess = new FrmInforSetupInitialProcess_SXGA(); ;
						FrmInforSetupMotion = new FrmInforSetupMotion_SXGA();
						FrmInforSetupIO = new FrmInforSetupIO_SXGA();
						FrmInforSetupCylinder = new FrmInforSetupCylinder_SXGA();
						FrmInforSetupLaser = new FrmInforSetupLaser_SXGA();
						FrmInforSetupAttenuator = new FrmInforSetupAttenuator_SXGA();
						FrmInforSetupScanner = new FrmInforSetupScanner_SXGA();
						FrmInforSetupPOMeter = new FrmInforSetupPowerMeter_SXGA();
						#endregion
						#region [Log]
						FrmInforLogEventLog = new FrmInforLogEventlog_SXGA();
						FrmInforLogWorkLog = new FrmInforLogWorklog_SXGA();
						#endregion

						#region [Popup]
						FrmLogMC = new FrmPopupLogMC();
						FrmLogSW = new FrmPopupLogSW();
						FrmLogPM = new FrmPopupLogPM();
						FrmInitialize = new FrmPopupInitialize();
						FrmLaserWarmupAlarm = new FrmPopupLaserWarmupAlarm();
						FrmVision = new FrmVision_SXGA();

						FrmAlarm = new FrmPopupAlarm();
						FrmError = new FrmPopupError();
						#endregion


						#region [ Tab For Initial Process ]
						FrmTabInitProcConfigSet = new FrmTabInitialProcessConfigSet();
						FrmTabInitProcVisionCal = new FrmTabInitialProcessVisionCal();
						FrmTabInitProcPowerTable = new FrmTabInitialProcessPowerTable();
						FrmTabInitProcStageCal = new FrmTabInitialProcessStageCal();
						FrmTabInitProcCrossHair = new FrmTabInitialProcessCrossHair();
						FrmTabInitProcFindFocus = new FrmTabInitialProcessFindFocus();
						FrmTabInitProcFindCPU = new FrmTabInitialProcessFindCPU();
						FrmTabInitProcGalvoCal = new FrmTabInitialProcessGalvoCal();
						FrmTabInitProcTowerLamp = new FrmTabInitialProcessTowerLamp();
						#endregion

						#region [ Tab For Recipe ]
						FrmTabRecipeConfig = new FrmTabRecipeConfiguration();
						FrmTabRecipeProcRecipe = new FrmTabRecipeProcessRecipe();
						#endregion

						#region [ Tab For New Recipe ]
						FrmTabNewRecipeCommon = new FrmTabRecipePanelCommon();
						FrmTabNewRecipeVision = new FrmTabRecipePanelVision();
						FrmTabNewRecipeProcess = new FrmTabRecipePanelProcess();
						#endregion


						

						RegularFormSetup(FrmInforOperationAuto);
						RegularFormSetup(FrmInforOperProcessStatus,false);

						RegularFormSetup(FrmInforOperationSemi);

						
						RegularFormSetup(FrmInforRecipeMain);



						RegularFormSetup(FrmInforManualVision);

						RegularFormSetup(FrmInforSetupInitialProcess);
						RegularFormSetup(FrmInforSetupMotion);
						RegularFormSetup(FrmInforSetupIO);
						RegularFormSetup(FrmInforSetupCylinder);
						RegularFormSetup(FrmInforSetupLaser);
						RegularFormSetup(FrmInforSetupAttenuator);
						RegularFormSetup(FrmInforSetupScanner);
						RegularFormSetup(FrmInforSetupPOMeter);


						RegularFormSetup(FrmInforLogEventLog);
						RegularFormSetup(FrmInforLogWorkLog);

						TabFormSetup(FrmTabInitProcConfigSet);
						TabFormSetup(FrmTabInitProcVisionCal);
						TabFormSetup(FrmTabInitProcPowerTable);
						TabFormSetup(FrmTabInitProcStageCal);
						TabFormSetup(FrmTabInitProcCrossHair);
						TabFormSetup(FrmTabInitProcFindFocus);
						TabFormSetup(FrmTabInitProcFindCPU);
						TabFormSetup(FrmTabInitProcGalvoCal);
						TabFormSetup(FrmTabInitProcTowerLamp);


						TabFormSetup(FrmTabNewRecipeCommon);
						TabFormSetup(FrmTabNewRecipeVision);
						TabFormSetup(FrmTabNewRecipeProcess);

						TabFormSetup(FrmTabRecipeConfig);
						TabFormSetup(FrmTabRecipeProcRecipe);



						PopupFormSetup(FrmLogMC);
						PopupFormSetup(FrmLogSW);
						PopupFormSetup(FrmLogPM);
						PopupFormSetup(FrmInitialize);
						PopupFormSetup(FrmLaserWarmupAlarm);

				}

				/// <summary>
				/// 
				/// </summary>
				/// <param name="containerControl"></param>
				/// <returns></returns>
				public static Control[] GetAllControl(Control containerControl)
				{
						List<Control> vecControls = new List<Control>();

						foreach (Control ctl in containerControl.Controls)
						{
								vecControls.Add(ctl);
								if (ctl.Controls.Count > 0)
								{
										vecControls.AddRange(GetAllControl(ctl));
								}
						}

						return vecControls.ToArray();
				}
				public static void RegularFormSetup(Form a_Frm,bool a_bFormSizeChange=true)
				{
						a_Frm.AutoScaleMode = AutoScaleMode.None;
						a_Frm.FormBorderStyle = FormBorderStyle.None;
						if(a_bFormSizeChange)
						{
								a_Frm.Width = FA.DEF.SXGA_WIDTH;
								a_Frm.Height = FA.DEF.SXGA_HEIGHT;
						}
						a_Frm.StartPosition = FormStartPosition.Manual;
						a_Frm.ShowInTaskbar = false;
						a_Frm.BackColor = Color.White;
						a_Frm.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Pixel);
						//a_Frm.TopLevel = false;

						//frm.Location				= new Point(0, Main.panTop.Height);
						a_Frm.DoubleBufferingAllDataGrid();

						Control[] arrCtls = GetAllControl(a_Frm);
						foreach (Control ctl in arrCtls)
						{
								#region [Button]
								if (ctl.GetType() == typeof(Button))
								{
										if (ctl.Tag == null)
										{
												(ctl as Button).FlatStyle = FlatStyle.Flat;
												(ctl as Button).FlatAppearance.BorderColor = Color.SteelBlue;
												(ctl as Button).FlatAppearance.MouseOverBackColor = Color.SteelBlue;
												(ctl as Button).FlatAppearance.MouseDownBackColor = Color.SteelBlue;
												(ctl as Button).MouseHover += new EventHandler(delegate (object obj, EventArgs e)
												{
														(obj as Button).ForeColor = Color.White;
												});
												// // 					Action<object, EventArgs> act1 = (object obj, EventArgs e) => 
												// // 					{
												// // 						(obj as Button).ForeColor = Color.White;
												// // 					};
												// 
												(ctl as Button).MouseLeave += new EventHandler(delegate (object obj, EventArgs e)
												{
														(obj as Button).ForeColor = Color.Black;
												});

												(ctl as Button).ForeColor = Color.Black;
												(ctl as Button).FlatAppearance.BorderSize = 1;

												(ctl as Button).BackColor = Color.White;
										}

								}
								#endregion

								#region [DataGridView]
								if (ctl.GetType() == typeof(DataGridView) || ctl.GetType().BaseType == typeof(DataGridView))
								{
										(ctl as DataGridView).BackgroundColor = Color.White;
										(ctl as DataGridView).ForeColor = Color.Black;
										(ctl as DataGridView).Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
								}
								#endregion

						}
				}
				public static void PopupFormSetup(Form a_Frm)
				{
						a_Frm.AutoScaleMode = AutoScaleMode.None;
						a_Frm.FormBorderStyle = FormBorderStyle.None;
						//a_Frm.Width			= FA.DEF.SXGA_WIDTH;
						//a_Frm.Height			= FA.DEF.SXGA_HEIGHT;
						a_Frm.StartPosition = FormStartPosition.Manual;
						a_Frm.ShowInTaskbar = false;
						a_Frm.BackColor = Color.White;
						a_Frm.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Pixel);
						a_Frm.StartPosition = FormStartPosition.CenterScreen;

						//frm.Location				= new Point(0, Main.panTop.Height);
						a_Frm.DoubleBufferingAllDataGrid();

						Control[] arrCtls = GetAllControl(a_Frm);
						foreach (Control ctl in arrCtls)
						{
								#region [Button]
								if (ctl.GetType() == typeof(Button))
								{

										if ((ctl as Button).Tag == null || (ctl as Button).Tag.ToString().Equals("WithoutCommonStyle") == false)
										{
												(ctl as Button).FlatStyle = FlatStyle.Flat;
												(ctl as Button).FlatAppearance.BorderColor = Color.SteelBlue;
												(ctl as Button).FlatAppearance.MouseOverBackColor = Color.SteelBlue;
												(ctl as Button).FlatAppearance.BorderSize = 0;

												//(ctl as Button).BackColor = Color.White;
										}
								}
								#endregion
								#region [DataGridView]
								if (ctl.GetType() == typeof(DataGridView))
								{
										(ctl as DataGridView).BackgroundColor = Color.White;
										(ctl as DataGridView).ForeColor = Color.Black;
										(ctl as DataGridView).Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
								}
								#endregion
								#region [ListBox]
								if (ctl.GetType() == typeof(ListBox))
								{
										(ctl as ListBox).BackColor = Color.White;
										(ctl as ListBox).ForeColor = Color.Black;
										(ctl as ListBox).Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
								}
								#endregion
								#region [PictureBox]
								if (ctl.GetType() == typeof(PictureBox))
								{
										(ctl as PictureBox).BackColor = Color.White;
										(ctl as PictureBox).ForeColor = Color.Black;
										(ctl as PictureBox).Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
								}
								#endregion
								#region [Label]
								if (ctl.GetType() == typeof(Label))
								{
										if ((ctl as Label).Tag == null)
										{
												(ctl as Label).BackColor = Color.White;
												(ctl as Label).ForeColor = Color.Black;
												(ctl as Label).Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
										}
										else
										{
												if ((ctl as Label).Tag.ToString().ToUpper().Equals("TITLE"))
												{
														(ctl as Label).BackColor = Color.FromArgb(45, 45, 48);
														(ctl as Label).ForeColor = Color.White;
														(ctl as Label).Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
												}
												else if ((ctl as Label).Tag.ToString().ToUpper().Equals("TAIL"))
												{
														(ctl as Label).BackColor = Color.White;
														(ctl as Label).ForeColor = Color.Black;
														(ctl as Label).Font = new Font("Century Gothic", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
												}
										}
								}
								#endregion
						}
				}
				public static void TabFormSetup(Form a_Frm)
				{
						a_Frm.AutoScaleMode = AutoScaleMode.None;
						a_Frm.FormBorderStyle = FormBorderStyle.None;
						a_Frm.Width = FA.DEF.SXGA_WIDTH;
						a_Frm.Height = FA.DEF.SXGA_HEIGHT;
						a_Frm.StartPosition = FormStartPosition.Manual;
						a_Frm.ShowInTaskbar = false;
						a_Frm.BackColor = Color.White;
						a_Frm.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Pixel);
						a_Frm.TopLevel = false;

						//frm.Location				= new Point(0, Main.panTop.Height);
						a_Frm.DoubleBufferingAllDataGrid();

						Control[] arrCtls = GetAllControl(a_Frm);
						foreach (Control ctl in arrCtls)
						{
								#region [Button]
								if (ctl.GetType() == typeof(Button))
								{

										if ((ctl as Button).Tag == null || (ctl as Button).Tag.ToString().Equals("WithoutCommonStyle") == false)
										{
												(ctl as Button).FlatStyle = FlatStyle.Flat;
												(ctl as Button).FlatAppearance.BorderColor = Color.SteelBlue;
												(ctl as Button).FlatAppearance.MouseOverBackColor = Color.SteelBlue;
												(ctl as Button).FlatAppearance.BorderSize = 1;

												(ctl as Button).BackColor = Color.White;
										}
								}
								#endregion
								#region [DataGridView]
								if (ctl.GetType() == typeof(DataGridView) ||
										ctl.GetType().BaseType == typeof(DataGridView)
										)
								{
										(ctl as DataGridView).BackgroundColor = Color.White;
										(ctl as DataGridView).ForeColor = Color.Black;
										(ctl as DataGridView).Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
								}
								#endregion

						}

				}

		}
		public static class DIR
		{
				public static string CFG;
				public static string SYSTEM;
				public static string LOG;
				public static string LOG_SW;
				public static string LOG_MC;
				public static string LOG_PM;


				public static string PJT;
				public static string RCP;
				public static string ALM;

				public static string INIT_PROC;
				public static string INIT_PROC_POWERTABLE;
				public static string INIT_PROC_GALVO_CAL;
				public static string INIT_PROC_STAGE_CAL;
				public static string INIT_PGM;
				public static string MANUAL_VISION;


		}

		public static class FILE
		{


				public static string Project;
				public static string Recipe;

				public static string RecipeIni;
				public static string Option;

				public static string Alarm;
				public static string Error;
				public static string LogEvent;
				public static string LogAlarm;
				public static string InitProc;
				public static string InitProcOption;
				public static string InitProcTowerNBuzzer;
				public static string InitProcPowerTable;
				public static string VisionCalbIMG;

				public static string ManualVision;

		}


		public static class SYS
		{
				public static bool Initialize()
				{
						#region [ Set Directory Path ]

						DIR.SYSTEM = APP.Path + @"SYSTEM\";
						DIR.CFG = DIR.SYSTEM + @"CFG\";
						DIR.LOG = DIR.SYSTEM + @"LOG\";
						DIR.PJT = DIR.SYSTEM + @"PJT\";
						DIR.RCP = DIR.SYSTEM + @"RCP\";
						DIR.ALM = DIR.CFG + @"ALM\";
						DIR.LOG_MC = DIR.LOG + @"MC\";
						DIR.LOG_SW = DIR.LOG + @"SW\";
						DIR.LOG_PM = DIR.LOG + @"PM\";

						DIR.INIT_PROC = DIR.CFG + @"INIT_PROC\";
						DIR.INIT_PROC_POWERTABLE = DIR.INIT_PROC + @"POWER_TABLE\";
						DIR.INIT_PROC_GALVO_CAL = DIR.INIT_PROC + @"GALVO_CAL\";
						DIR.INIT_PROC_STAGE_CAL = DIR.INIT_PROC + @"STAGE_CAL\";
						DIR.INIT_PGM = DIR.INIT_PROC + @"PGM\";

						DIR.MANUAL_VISION = DIR.CFG + @"VISION\MANUAL_VISION";

						Directory.CreateDirectory(DIR.SYSTEM);
						Directory.CreateDirectory(DIR.CFG);
						Directory.CreateDirectory(DIR.LOG);
						Directory.CreateDirectory(DIR.PJT);
						Directory.CreateDirectory(DIR.RCP);
						Directory.CreateDirectory(DIR.LOG_MC);
						Directory.CreateDirectory(DIR.LOG_SW);
						Directory.CreateDirectory(DIR.LOG_PM);
						Directory.CreateDirectory(DIR.ALM);

						Directory.CreateDirectory(DIR.INIT_PROC);
						Directory.CreateDirectory(DIR.INIT_PROC_POWERTABLE);
						Directory.CreateDirectory(DIR.INIT_PROC_GALVO_CAL);
						Directory.CreateDirectory(DIR.INIT_PROC_STAGE_CAL);
						Directory.CreateDirectory(DIR.INIT_PGM);

						Directory.CreateDirectory(DIR.MANUAL_VISION);

						#endregion

						#region [ Set File Path ]
						// 파일경로
						FILE.Project = DIR.PJT + "Project.ini";
						FILE.RecipeIni = DIR.RCP + "Recipe.ini";
						FILE.Alarm = DIR.ALM + "Alarm.ini";
						FILE.Error = DIR.ALM + "Error.ini";


						FILE.InitProc = DIR.INIT_PROC + "InitialProc.rcp";
						FILE.InitProcOption = DIR.INIT_PROC + "CommonOption.ini";
						FILE.InitProcTowerNBuzzer = DIR.INIT_PROC + "TowerNBuzzer.ini";
						FILE.InitProcPowerTable = DIR.INIT_PROC_POWERTABLE + "PowerTable.ini";
						FILE.ManualVision = DIR.MANUAL_VISION + "ManualVision.rcp";
						FILE.VisionCalbIMG = DIR.CFG + "Vision\\VisionCAL_DefaultIMG.bmp";
						#endregion

						#region [ Initialize class]


						FA.RCP.Init();
						FA.OPT.Init();

						MF.OPT.Init();
						MF.RCP.Init();
						MF.RCP_Modify.Init();
						MF.TOWERLAMP.Init(FA.FILE.InitProcTowerNBuzzer);
						//MF.TOWERLAMP.DoThreadProc = SYS.DoTowerLamp;                             
						FA.FRM.Init();
						FA.FRM.FrmInitialize.bInit = true;
						//로그초기화.
						APP.InitLogger(FRM.FrmLogMC.listBox_MC, FRM.FrmLogSW.listBox_SW, FRM.FrmLogPM.listBox_PM, FA.DIR.LOG + "LogConfig.xml");


						#endregion

						return true;
				}
				public static void Static_MGR_Memory_LINK()
				{
						FA.AXIS.Init();
						FA.RTC5.Init();
						FA.LASER.Init();
						FA.LIGHTSOURCE.Init();
						FA.VISION.Init();
				}

				#region [BUTTON SWITCH]
				public static volatile FA.DEF.eButtonSW eButtonSWState = FA.DEF.eButtonSW.OFF;
				public static void SetButtonSwitch(FA.DEF.eButtonSW a_eSW)
				{
						eButtonSWState = a_eSW;
				}
				public static void ButtonSwitchScan()
				{
						switch (eButtonSWState)
						{
								case FA.DEF.eButtonSW.INIT:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = true;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = true;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = true;
										break;
								case FA.DEF.eButtonSW.START:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = true;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = false;
										break;
								case FA.DEF.eButtonSW.STOP:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = true;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = false;
										break;
								case FA.DEF.eButtonSW.RESET:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = true;
										break;
								case FA.DEF.eButtonSW.OFF:
								default:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = false;
										break;

								case FA.DEF.eButtonSW.INIT_FLICKER:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = FLICKER.IsOn ? true : false;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = FLICKER.IsOn ? true : false; ;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = FLICKER.IsOn ? true : false; ;
										break;
								case FA.DEF.eButtonSW.START_FLICKER:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = FLICKER.IsOn ? true : false;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = false;
										break;
								case FA.DEF.eButtonSW.STOP_FLICKER:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = FLICKER.IsOn ? true : false;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = false;
										break;
								case FA.DEF.eButtonSW.RESET_FLICKER:
										FA.DEF.GetDO(DEF.eDO.SW_START_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_STOP_LAMP).Value = false;
										FA.DEF.GetDO(DEF.eDO.SW_RESET_LAMP).Value = FLICKER.IsOn ? true : false;
										break;


						}

				}
				#endregion [BUTTON SWITCH]

				#region [TOWER LAMP]
				public static void DoTowerLamp()
				{
						switch (MF.TOWERLAMP.Lamps[(int)FA.MGR.RunMgr.eRunMode, (int)EzIna.MF.eLampItem.Red])
						{
								case MF.eLampAction.Off: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_RED).Value = false; break;
								case MF.eLampAction.On: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_RED).Value = true; break;
								case MF.eLampAction.Blink: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_RED).Value = FLICKER.IsOn; break;
						}
						switch (MF.TOWERLAMP.Lamps[(int)FA.MGR.RunMgr.eRunMode, (int)EzIna.MF.eLampItem.Green])
						{
								case MF.eLampAction.Off: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_GREEN).Value = false; break;
								case MF.eLampAction.On: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_GREEN).Value = true; break;
								case MF.eLampAction.Blink: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_GREEN).Value = FLICKER.IsOn; break;
						}
						switch (MF.TOWERLAMP.Lamps[(int)FA.MGR.RunMgr.eRunMode, (int)EzIna.MF.eLampItem.Yellow])
						{
								case MF.eLampAction.Off: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_YELLOW).Value = false; break;
								case MF.eLampAction.On: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_YELLOW).Value = true; break;
								case MF.eLampAction.Blink: FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_YELLOW).Value = FLICKER.IsOn; break;
						}

						if (MF.TOWERLAMP.StartBuzzer)
						{
								FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_BUZZER).Value = MF.TOWERLAMP.Buzzers[(int)FA.MGR.RunMgr.eRunMode, (int)MF.eBuzzerItem.b1];
						}
						else
						{
								FA.DEF.GetDO(FA.DEF.eDO.TOWER_LAMP_BUZZER).Value = false;
						}
				}
				#endregion

		}
		public static class RTC5
		{
				public static EzIna.Scanner.ScanlabRTC5 Instance = null;
				public static void Init()
				{
						Instance = EzIna.Scanner.ScanlabRTCMgr.Instance[(int)DEF.RTC5Scanner.Main];
				}

		}
		public static class AXIS
		{
				public static EzIna.Motion.MotionInterface RX = null;
				public static EzIna.Motion.MotionInterface Y = null;
				public static EzIna.Motion.MotionInterface RZ = null;
				public static EzIna.Motion.MotionInterface RAIL_ADJUST = null;
				/// <summary>
				/// Pinch Roller Left Up
				/// </summary>
				public static EzIna.Motion.MotionInterface PR_L_U = null;
				/// <summary>
				/// Pinch Roller Left Down
				/// </summary>
				public static EzIna.Motion.MotionInterface PR_L_B = null;
				/// <summary>
				/// Pinch Roller Left Up
				/// </summary>
				public static EzIna.Motion.MotionInterface PR_R_U = null;
				/// <summary>
				/// Pinch Roller Left Down
				/// </summary>
				public static EzIna.Motion.MotionInterface PR_R_B = null;

				public static EzIna.Motion.MotionInterface RA = null;
				public static EzIna.Motion.MotionInterface RB = null;

				public static void Init()
				{
						RX = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.RX);
						Y = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.Y);
						RZ = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.RZ);
						RAIL_ADJUST = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.RAIL_ADJUST);
						PR_L_U = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.PINCH_ROLLER_L_U);
						PR_L_B = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.PINCH_ROLLER_L_B);
						PR_R_U = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.PINCH_ROLLER_R_U);
						PR_R_B = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.PINCH_ROLLER_R_B);
						//RA = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.RA);
						//RB = FA.MGR.MotionMgr?.GetItem((int)DEF.eAxesName.RB);
				}

				public static void AllSDStop()
				{
						RX.SD_STOP();
						Y.SD_STOP();
						RZ.SD_STOP();
						RAIL_ADJUST.SD_STOP();
						PR_L_U.SD_STOP();
						PR_L_B.SD_STOP();
						PR_R_U.SD_STOP();
						PR_R_B.SD_STOP();
						// RA.SD_STOP();
						// RB.SD_STOP();
				}

				public static void AllJogStop()
				{
						RX.JOG_STOP();
						Y.JOG_STOP();
						RZ.JOG_STOP();
						RAIL_ADJUST.JOG_STOP();
						PR_L_U.JOG_STOP();
						PR_L_B.JOG_STOP();
						PR_R_U.JOG_STOP();
						PR_R_B.JOG_STOP();
						//RA.JOG_STOP();
						//RB.JOG_STOP();
				}
				public static void AllStepMotorStop()
				{
						RAIL_ADJUST.JOG_STOP();
						PR_L_U.JOG_STOP();
						PR_L_B.JOG_STOP();
						PR_R_U.JOG_STOP();
						PR_R_B.JOG_STOP();
				}
				public static EzIna.Motion.MotionItem Status(this EzIna.Motion.MotionInterface ctrl)
				{
						return ctrl as Motion.MotionItem;
				}
		}

		public static class LIGHTSOURCE
		{
				public static EzIna.Light.LightInterface RING = null;
				public static EzIna.Light.LightInterface SPOT = null;
				public static EzIna.Light.LightInterface BAR = null;
				public static void Init()
				{
						//RING = FA.MGR.LightMgr?[(ushort)FA.DEF.lightSource.RING];
						//SPOT = FA.MGR.LightMgr?[(ushort)FA.DEF.lightSource.SPOT];
						BAR = FA.MGR.LightMgr?[(ushort)FA.DEF.lightSource.BAR];
				}

		}
		public static class ATT
		{
				public static EzIna.Attenuator.OPTOGAMA.LPA LPA;

				public static void Init()
				{
						LPA = (FA.MGR.AttenuatorMgr?[(ushort)FA.DEF.Attenuator.LPA] as EzIna.Attenuator.OPTOGAMA.LPA);
				}
		}
		public static class LASER
		{
				public enum eStatus
				{
						SYSTEM_READY,
						ALPS_REPLACEMENT,
						THG_WARNING_SPOT_HR,
						WARMUP_TIME_OVER,
						THG_SOPT_HOURS_LIMIT_OVER
				}
				public static EzIna.Laser.LaserInterface Instance = null;

				public static void Init()
				{
						Instance = FA.MGR.LaserMgr?[(ushort)FA.DEF.Laser.MAIN_LASER];
				}

				public static eStatus CheckLaserEnable()
				{
						eStatus eRtn = eStatus.SYSTEM_READY;

						return eRtn;

				}
		}
		public static class POWERMETER
		{
				public static EzIna.PowerMeter.Ohpir.SPC OPHIR;

				public static void Init()
				{
						// OPHIR = (FA.MGR.PowerMeterMgr?[(ushort)FA.DEF.Powermeter.Ophir] as EzIna.PowerMeter.Ohpir.SPC);
				}
		}
		public static class VISION
		{
				public static EzInaVisionLibrary.VisionLibEuresys FINE_LIB = null;
				public static EzInaVisionLibrary.VisionLibEuresys COARSE_LIB = null;
				public static EzInaVisionMultiCam.GrabLinkCam FINE_CAM = null;
				public static EzInaVisionMultiCam.GrabLinkCam COARSE_CAM = null;

				public static void Init()
				{
						FINE_LIB = (EzInaVisionLibrary.VisionLibEuresys)FA.MGR.VisionMgr?.GetLib(FA.DEF.eVision.FINE.ToString().ToUpper());
						COARSE_LIB = (EzInaVisionLibrary.VisionLibEuresys)FA.MGR.VisionMgr?.GetLib(FA.DEF.eVision.COARSE.ToString().ToUpper());

						FINE_CAM = (EzInaVisionMultiCam.GrabLinkCam)FA.MGR.VisionMgr?.GetCam(FA.DEF.eVision.FINE.ToString().ToUpper());
						COARSE_CAM = (EzInaVisionMultiCam.GrabLinkCam)FA.MGR.VisionMgr?.GetCam(FA.DEF.eVision.COARSE.ToString().ToUpper());

				}

		}
}
