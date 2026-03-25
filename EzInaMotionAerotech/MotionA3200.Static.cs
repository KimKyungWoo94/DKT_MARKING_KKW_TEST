using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Aerotech.A3200;
using Aerotech.A3200.Exceptions;
using Aerotech.A3200.Information;
using Aerotech.A3200.Status;
using Aerotech.A3200.Tasks;
using System.Diagnostics;
using System.IO;

namespace EzIna.Motion
{
    #region Static Func , Vairiable
    public sealed partial class CMotionA3200
    {

        delegate void TasksInforHandler(List<stTaskInfor> a_Tasks);
        public static List<CMotionA3200> m_listAxises = null;
        public static Dictionary<string, CMotionA3200> m_DicListAxises = null;
        static List<AeroTaskInfor> m_listTasks = null;
        static TasksInforHandler TasksInforEventHandler = null;
        static int m_iTaskIndex = (int)TaskId.T01;
        static Controller myController = null;

        static bool m_bInitialized = false;

        public static Controller ConnectedController
        {
            get { return myController; }
        }
        public static bool bDriverConnect
        {
            get { return (myController != null) ? true : false; }
        }
        public static bool bInitialized
        {
            get { return m_bInitialized; }
        }
        #region Driver Init & Terminate
        public static bool InitializeConnectDevice()
        {
            if (Controller.ConnectedController != null)
            {
                myController = Controller.ConnectedController;
            }
            else
            {
                myController = Controller.Connect();

            }
            return (myController != null) ? true : false;
        }
        public static bool TerminateConnectedDriver()
        {
            if (bDriverConnect)
            {
                Controller.Disconnect();
                myController = null;
            }
            return bDriverConnect;
        }
        public static bool InitializeDriver()
        {
            try
            {
                if (m_bInitialized == false)
                {
                    m_listAxises = null;
                    m_listTasks = null;
                    m_DicListAxises = null;
                    m_listAxises = new List<CMotionA3200>();
                    m_listTasks = new List<AeroTaskInfor>();
                    m_DicListAxises = new Dictionary<string, CMotionA3200>();
                }
                InitializeConnectDevice();
                if (bDriverConnect)
                {
                    int i = 0;
                    // populate axis names
                    foreach (AxisInfo axis in myController.Information.Axes)
                    {											
                         if (m_DicListAxises.ContainsKey(axis.Name) == false)
                        {
                            m_listAxises.Add(new CMotionA3200(i++, axis.Name));
                            m_DicListAxises.Add(m_listAxises[m_listAxises.Count - 1].strAxisName, m_listAxises[m_listAxises.Count - 1]);
                        }
                    }
                    // populate task names
                    foreach (Aerotech.A3200.Tasks.Task task in myController.Tasks)
                    {
                        if (task.State != TaskState.Inactive)
                        {
                            m_listTasks.Add(new AeroTaskInfor(task, new stTaskInfor()));
                        }
                    }
                    // register task state and diagPackect arrived events
                    myController.ControlCenter.TaskStates.NewTaskStatesArrived += new EventHandler<NewTaskStatesArrivedEventArgs>(TaskStates_NewTaskStatesArrived);
                    myController.ControlCenter.Diagnostics.NewDiagPacketArrived += new EventHandler<NewDiagPacketArrivedEventArgs>(Diagnostics_NewDiagPacketArrived);
                    myController.ControlCenter.TaskStates.RefreshInterval = 10;
                    m_bInitialized = true;
                    //m_Stopwatch.Start();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public static void TerminateDriver()
        {
            m_listAxises.Clear();
            m_listTasks.Clear();
            m_DicListAxises.Clear();
            m_listAxises = null;
            m_listTasks = null;
            m_DicListAxises = null;
            m_bInitialized = false;
            TerminateConnectedDriver();
        }
        #endregion Driver Init & Terminate

        #region Getting Func
        public static void CreateEvent_For_Tasks(Action<List<stTaskInfor>> Func)
        {
            TasksInforEventHandler += new TasksInforHandler(Func);
        }
        private static void TaskStates_NewTaskStatesArrived(object sender, NewTaskStatesArrivedEventArgs e)
        {
            try
            {
                //URL: http://msdn.microsoft.com/en-us/library/ms171728.aspx
                //How to: Make Thread-Safe Calls to Windows Forms Controls
                //this.Invoke(new Action<NewTaskStatesArrivedEventArgs>(SetTaskState), e);
                List<stTaskInfor> TasksInfor = new List<stTaskInfor>();
                if (m_listTasks != null)
                {
                    for (int i = 0; i < m_listTasks.Count; i++)
                    {
                        m_listTasks[i].TaskInfor.eState = e.Statuses[i].State;
                        m_listTasks[i].TaskInfor.strState = e.Statuses[i].State.ToString();
                        m_listTasks[i].TaskInfor.strErr = e.Statuses[i].Error.Description.ToString();
                        TasksInfor.Add(m_listTasks[i].TaskInfor);
                    }
                }

                if (TasksInforEventHandler != null)
                {
                    TasksInforEventHandler(TasksInfor);
                }
            }
            catch
            {
                Trace.WriteLine("TaskStates_NewTaskStatesArrived");
            }
        }

        public static void Diagnostics_NewDiagPacket()
        {


        }
        /// <summary>
        /// Handle DiagPacket (axis state in it) arrived event. Invoke SetAxisState to process data
        /// </summary>
        private static void Diagnostics_NewDiagPacketArrived(object sender, NewDiagPacketArrivedEventArgs e)
        {
            try
            {
                //URL: http://msdn.microsoft.com/en-us/library/ms171728.aspx
                //How to: Make Thread-Safe Calls to Windows Forms Controls
                //this.Invoke(new Action<NewDiagPacketArrivedEventArgs>(SetAxisState), e);

                if (m_listAxises != null)
                {
                    for (int i = 0; i < m_listAxises.Count; i++)
                    {
                        m_listAxises[i].m_stMotionInfoStatus.m_bIsLimitP = e.Data[i].DriveStatus.CwEndOfTravelLimitInput;
                        m_listAxises[i].m_stMotionInfoStatus.m_bIsLimitN = e.Data[i].DriveStatus.CcwEndOfTravelLimitInput;
                        m_listAxises[i].m_stMotionInfoStatus.m_bIsAlarm = !e.Data[i].AxisFault.None;
                        m_listAxises[i].m_stMotionInfoStatus.m_bIsInPos = e.Data[i].DriveStatus.InPosition;
                        m_listAxises[i].m_stMotionInfoStatus.m_bIsEStop = false;
                        m_listAxises[i].m_stMotionInfoStatus.m_bIsOrg = e.Data[i].DriveStatus.HomeLimitInput;
                        m_listAxises[i].m_stMotionInfoStatus.m_bIsZPhase = e.Data[i].DriveStatus.MarkerInput;
                        m_listAxises[i].m_bServoOn = e.Data[i].DriveStatus.Enabled;
                        m_listAxises[i].m_bHomeComplete = e.Data[i].AxisStatus.Homed;
                        m_listAxises[i].m_bIsMotionDone = e.Data[i].DriveStatus.InPosition & e.Data[i].AxisStatus.MoveDone;
                        m_listAxises[i].m_bJogging = e.Data[i].AxisStatus.Jogging;
                        m_listAxises[i].m_stPositionStatus.fCmdPos = e.Data[i].PositionCommand;
                        m_listAxises[i].m_stPositionStatus.fActPos = e.Data[i].PositionFeedback;
                        m_listAxises[i].m_stPositionStatus.fVelocity = e.Data[i].VelocityFeedback;
                        m_listAxises[i].m_stPositionStatus.fErrPos = e.Data[i].PositionError;
                    }
                }

                //                 double Position = myController.ControlCenter.Diagnostics.Latest.Axes[0].PositionCommand;
                //                 //Statuses[1] == TaskID == 2
                //                 string taskString = myController.ControlCenter.TaskStates.Latest.Statuses[3].State.ToString();;
                // 
                //                 //m_Stopwatch.Stop();
                //                 //double PosU = myController.ControlCenter.Diagnostics.Latest.Axes[0].PositionCommand;
                // 
                //                 TimeSpan currentTime = m_Stopwatch.Elapsed;
                //                 m_Stopwatch.Stop();
                //                 Trace.WriteLine(string.Format("{0}:{1}", "Diagnostics_NewDiagPacketArrived [ms]", currentTime.Milliseconds.ToString()));
                //                 m_Stopwatch.Restart();

            }
            catch
            {
                Trace.WriteLine("Diagnostics_NewDiagPacketArrived");
            }
        }
        #endregion Getting Func
        #region Run/Stop Program
        ///<summary>
        ///</summary>
        public static void RunProgram(int a_iTask, string a_strProgram)
        {
            try
            {
                if (bDriverConnect)
                {
                    myController.Tasks[a_iTask].Program.Run(a_strProgram);
                }
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void StopProgram(int a_iTask)
        {
            try
            {
                myController.Tasks[a_iTask].Program.Stop();
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion RunProgram

        #region [ ProgramRun & Stop ]
        public static bool ProgramRun(int a_iTask, string a_strPath)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return false;
            try
            {
                if (!File.Exists(a_strPath))
                    throw new Exception("The file does not exists");

                if (myController.Tasks[a_iTask].State == TaskState.ProgramComplete ||
                myController.Tasks[a_iTask].State == TaskState.Idle)
                {
                    //a_strPath = @"D:\Remote\System\CFG\INIT_PROC\PGM\CrossHair.pgm";
                    myController.Tasks[a_iTask].Program.Run(a_strPath); // run the program as listed in the program path text box
                    m_iTaskIndex = a_iTask;
                    return true;
                }
                else
                    throw new Exception("It's not ready");

            }
            catch (A3200Exception ex)
            {
                return false;
            }
        }

        public bool ProgramBufferedRun(int a_iTask, string a_strPgmPath)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return false;
            try
            {
                if (!File.Exists(a_strPgmPath))
                    throw new Exception("The file does not exists");

                if (myController.Tasks[a_iTask].State == TaskState.ProgramComplete ||
                myController.Tasks[a_iTask].State == TaskState.Idle)
                {
                    myController.Tasks[a_iTask].Program.BufferedRun(a_strPgmPath); // run the program as listed in the program path text box
                    m_iTaskIndex = (int)a_iTask;
                    return true;
                }
            }
            catch (A3200Exception ex)
            {
                return false;
            }
            return false;
        }
        #region [ ProgramLoad & Start ]
        public static bool ProgramLoad(int a_iTask, string a_strPath)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return false;
            try
            {
                if (!File.Exists(a_strPath))
                    throw new Exception("It's not exists file");

                myController.Tasks[a_iTask].Program.Load(a_strPath);
                m_iTaskIndex = (int)a_iTask;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool ProgramStart(int a_iTask)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return false;
            try
            {
                myController.Tasks[a_iTask].Program.Start();
                m_iTaskIndex = a_iTask;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion [ ProgramLoad & Start]
        public static void ProgramStop()
        {
            myController.Tasks[m_iTaskIndex].Program.Stop();
        }
        #endregion[ ProgramRun & Stop ]

        #region [Tasks]

        public static string GetNameOfPGM(int a_iTask)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return "";

            return myController.Tasks[a_iTask].Program.FileName;
        }

        public static bool IsPgmAssociated(int a_iTask)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return false;

            return myController.Tasks[a_iTask].Program.Associated;
        }
        public static string GetTaskName(int a_iTask)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return "";

            return m_listTasks[a_iTask].pTask.Name.ToString();
        }

        public static int GetCurrentTaskIndex()
        {
            return m_iTaskIndex;
        }
        public static string GetTaskState_String(int a_iTask)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return "";
            return m_listTasks[a_iTask].TaskInfor.ToString();
        }
        public static TaskState GetTaskState_Enum(int a_iTask)
        {
            if (a_iTask < 0 || a_iTask >= m_listTasks.Count)
                return TaskState.Unavailable;
            return m_listTasks[a_iTask].TaskInfor.eState;
        }

        public static string GetAxisName(int a_iAxis)
        {
            if (a_iAxis < 0 || a_iAxis >= m_listAxises.Count)
                return "";
            return m_listAxises[a_iAxis].strAxisName;
        }
        #endregion GetName Tasks Axes 

        #region Enable/Disable
        public static void Enable(int a_iTask, int a_iAxis, bool a_bOn)
        {
            try
            {
                if (bDriverConnect)
                {
                    if (a_bOn)
                        myController.Commands[a_iTask].Axes[a_iAxis].Motion.Enable();
                    else
                        myController.Commands[a_iTask].Axes[a_iAxis].Motion.Disable();
                }
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void Enable(int a_iAxis, bool a_bOn)
        {
            try
            {
                if (bDriverConnect)
                {
                    if (a_bOn)
                        myController.Commands.Axes[a_iAxis].Motion.Enable();
                    else
                        myController.Commands.Axes[a_iAxis].Motion.Disable();
                }
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        #endregion Enable/ Disable

        #region [Execute Command]
        public static object ExecuteCmd(int a_iTaskID, string a_strCommand)
        {
            object obj = null;
            try
            {
                obj = myController.Commands[a_iTaskID].Execute(a_strCommand);
                string strValue = obj.ToString();
                return obj;
            }
            catch (A3200Exception exception)
            {
                return obj;
            }
        }

        #endregion [Execute Command]

        #region [IO]
        public static bool GetDI(int a_axis, int a_Positon)
        {
            return myController.Commands.IO.DigitalInputBit(a_Positon, a_axis) == 1;
        }
        public static bool GetDI(string a_strAxis, int a_Positon)
        {
            return myController.Commands.IO.DigitalInputBit(a_Positon, a_strAxis) == 1;
        }
        public static bool GetDO(int a_iAxis, int a_Positon)
        {
            double dValue = myController.Commands.Status.AxisStatus(a_iAxis, AxisStatusSignal.DigitalOutput);
            int nValue = Convert.ToInt32(dValue);
            return ((nValue >> a_Positon) & 0x01) == 1 ? true : false;
        }
        public static bool GetDO(string a_Axis, int a_Positon)
        {
            double dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.DigitalOutput);
            int nValue = Convert.ToInt32(dValue);
            return ((nValue >> a_Positon) & 0x01) == 1 ? true : false;
        }
        public static void SetDO(int a_Axis, int a_Positon, int a_iOnOff)
        {
            myController.Commands.IO.DigitalOutputBit(a_Positon, a_Axis, a_iOnOff);
        }
        public static void SetDO(string a_Axis, int a_Positon, int a_iOnOff)
        {
            myController.Commands.IO.DigitalOutputBit(a_Positon, a_Axis, a_iOnOff);
        }
        public static double GetAI(int a_Axis, int a_Ch)
        {
            return myController.Commands.IO.AnalogInput(a_Ch, a_Axis);
        }
        public static double GetAI(string a_Axis, int a_Ch)
        {
            return myController.Commands.IO.AnalogInput(a_Ch, a_Axis);
        }

        public static void SetAO(int a_Axis, int a_Ch, double a_value)
        {
            myController.Commands.IO.AnalogOutput(a_Ch, a_Axis, a_value);
        }
        public static void SetAO(string a_Axis, int a_Ch, double a_value)
        {
            myController.Commands.IO.AnalogOutput(a_Ch, a_Axis, a_value);
        }

        public static double GetAO(int a_Axis, int a_Ch)
        {

            double dValue = 0.0;
            if (a_Ch >= 0 && a_Ch < 4)
            {
                switch (a_Ch)
                {
                    case 0: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput0); } break;
                    case 1: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput1); } break;
                    case 2: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput2); } break;
                    case 3: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput3); } break;
                }

            }
            return dValue;
        }
        public static double GetAO(string a_Axis, int a_Ch)
        {
            double dValue = 0.0;
            if (a_Ch < 4)
            {
                switch (a_Ch)
                {
                    case 0: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput0); } break;
                    case 1: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput1); } break;
                    case 2: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput2); } break;
                    case 3: { dValue = myController.Commands.Status.AxisStatus(a_Axis, AxisStatusSignal.AnalogOutput3); } break;
                }
            }
            return dValue;
        }
        public static double GetGlobalVariables(int a_iIndex)
        {
            return myController.Variables.Global.Doubles[a_iIndex].Value;
        }
        public static double GetGlobalVariables(string a_Name)
        {
            return myController.Variables.Global.Doubles[a_Name].Value;
        }
        public static void SetGlobalVariables(int a_iIndex, double a_dValue)
        {
            myController.Variables.Global.Doubles[a_iIndex].Value = a_dValue;
            //             #region [ Example ]
            //             // Connect to the controller.
            //             Controller myController = Controller.Connect();
            //             // Set the value of $global[0] to PI
            //             myController.Variables.Global.Doubles[0].Value = 3.1415926535897931;
            //             // Read back the value that was set in $global[0]
            //             Console.WriteLine("Value of $global[0] : {0}", myController.Variables.Global.Doubles[0].Value);
            //             // Set the value of the consecutive global variables $global[0] through $global[4]
            //             myController.Variables.Global.Doubles.SetMultiple(0, new double[] { 5.0, 4.0, 3.0, 2.0, 1.0 });
            //             // Read back the consecutive values that were set in the global variables
            //             double[] values = myController.Variables.Global.Doubles.GetMultiple(0, 5);
            //             Console.WriteLine("Value of {{ $global[0], $global[1], $global[2], $global[3], $global[4] }} : {{ {0}, {1}, {2}, {3}, {4} }}", values[0], values[1], values[2], values[3], values[4]);
            //             // One can also access variables by name
            //             Console.WriteLine("Value of $global[0] : {0}", myController.Variables.Global["$global[0]"].Value);
            //             // Print out the value of $task[0] for Task 1
            //             Console.WriteLine("Value of $task[0] on task 1 : {0}", myController.Variables.Tasks[1]["$task[0]"].Value);
            //             #endregion [ Example ]
        }
        #endregion [IO]

        public static void AcknowlegeAll()
        {
            try
            {
                myController.Commands.AcknowledgeAll();
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public static void Reset()
        {
            try
            {
                myController.Reset();
            }
            catch (A3200Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
    #endregion Static Func , Vairiable
}
