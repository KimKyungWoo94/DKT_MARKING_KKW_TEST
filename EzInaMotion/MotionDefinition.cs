using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace EzIna.Motion
{
    public class GDMotion
    {
        #region [ enum]
        public enum eMotionBrand
        {
            None = -1
                , AJINEXTEK
                , AEROTECH
                , MAX
        }
        public enum eMotorName
        {
            None = -1
          , M_X_FRONT = 0
          , M_X_REAR
          , M_Y_LEFT
          , M_Y_RIGHT
          , M_Z_FRONT
                , M_SPARE
          , M_Z_REAR
          , MAX
        }
        public enum eMotorNameGantry
        {
            None = -1
            , M_GANTRY_M_Y
            , M_GANTRY_S_Y
            , MAX
        }

        public enum eMotorNameAero
        {
            None = -1
            , U
            , MAX

        }

        public enum eRepetitiveMotion
        {
            NONE = -1,
            RDY = 0,
            MOV_POS_0,
            CHK_POS_0,
            MOV_DLY_0,
            MOV_POS_1,
            CHK_POS_1,
            MOV_DLY_1,
            DONE
        }


        public enum eMoveMode
        {
            MOVE_ABS = 0,
            MOVE_REL
        }

        public enum eSpeedType
        {
            NONE = 0,
            SLOW,
            FAST,
            RUN,
            USER,
            INTERPOLATE,
            HOME
        };

        public enum eGantryHomeMethod
        {
            NONE = 0, //Only Master 
            BOTH = 1, //Master & Slave
            EACH = 2, //Get Home offset between master axis and slave axis.
        }

        public enum eAeroI
        {
            //output 4, 
            //Input  6
            X08 = 8,
            X09,
            X10,
            X11,
            X12,
            X13
        }

        public enum eAeroO
        {
            Y08 = 8,
            Y09,
            Y10,
            Y11

        }

        public enum eAeroIO
        {
            Off = 0,
            On
        }

        public enum DATASIGNAL
        {
            DATASIGNAL_CurrentFeedback = 7,
            DATASIGNAL_CurrentCommand = 8,
            DATASIGNAL_AnalogInput0 = 10,
            DATASIGNAL_AnalogInput1 = 11,
            DATASIGNAL_PhaseACurrentFeedback = 24,
            DATASIGNAL_PhaseBCurrentFeedback = 25,
            DATASIGNAL_EncoderSine = 26,
            DATASIGNAL_EncoderCosine = 27,
            DATASIGNAL_AnalogInput2 = 28,
            DATASIGNAL_AnalogInput3 = 29,
            DATASIGNAL_LoopTransmissionBefore = 30,
            DATASIGNAL_LoopTransmissionAfter = 31,
            DATASIGNAL_ZHSDifference = 32,
            DATASIGNAL_ZHSSum = 33,
            DATASIGNAL_ZHSUnfiltered = 34,
            DATASIGNAL_ZHSFiltered = 35,
            DATASIGNAL_AnalogOutput0 = 36,
            DATASIGNAL_AnalogOutput1 = 37,
            DATASIGNAL_AnalogOutput2 = 38,
            DATASIGNAL_AnalogOutput3 = 39,
            DATASIGNAL_DriveMemoryInt32 = 40,
            DATASIGNAL_DriveMemoryFloat = 41,
            DATASIGNAL_DriveMemoryDouble = 42,
            DATASIGNAL_PSOStatus = 43,
            DATASIGNAL_DriveTimer = 44,
            DATASIGNAL_PositionFeedbackDrive = 88,
            DATASIGNAL_PositionCommandDrive = 98,
            DATASIGNAL_DriveMemoryInt16 = 157,
            DATASIGNAL_DriveMemoryInt8 = 158,
            DATASIGNAL_DriveMemoryInt32Pointer = 202,
            DATASIGNAL_DriveMemoryInt16Pointer = 203,
            DATASIGNAL_DriveMemoryInt8Pointer = 204,
            DATASIGNAL_DriveMemoryFloatPointer = 205,
            DATASIGNAL_DriveMemoryDoublePointer = 206,
            DATASIGNAL_PSOCounter1 = 229,
            DATASIGNAL_PSOCounter2 = 230,
            DATASIGNAL_PSOCounter3 = 231,
            DATASIGNAL_PSOWindow1 = 232,
            DATASIGNAL_PSOWindow2 = 233,
            DATASIGNAL_DataAcquisitionSamples = 234,
            DATASIGNAL_PositionDetectorVoltage = 235,
            DATASIGNAL_PositionCommandGalvo = 237,
            DATASIGNAL_ResolverChannel1 = 267,
            DATASIGNAL_ResolverChannel2 = 268,
            DATASIGNAL_EnDatAbsolutePosition = 269,
            DATASIGNAL_ControlEffort = 282,
            DATASIGNAL_ModbusBitMasterInputBits = 290,
            DATASIGNAL_ModbusBitMasterOutputBits = 291,
            DATASIGNAL_ModbusRegInt16MasterInputWords = 292,
            DATASIGNAL_ModbusRegInt16MasterOutputWords = 293,
            DATASIGNAL_ModbusRegInt32MasterInputWords = 294,
            DATASIGNAL_ModbusRegInt32MasterOutputWords = 295,
            DATASIGNAL_ModbusRegSingleMasterInputWords = 296,
            DATASIGNAL_ModbusRegSingleMasterOutputWords = 297,
            DATASIGNAL_ModbusRegDoubleMasterInputWords = 298,
            DATASIGNAL_ModbusRegDoubleMasterOutputWords = 299,
            DATASIGNAL_PhaseAVoltageCommand = 300,
            DATASIGNAL_PhaseBVoltageCommand = 301,
            DATASIGNAL_PhaseCVoltageCommand = 302,
            DATASIGNAL_AmplifierPeakCurrent = 303,
            DATASIGNAL_FPGAVersion = 304,
            DATASIGNAL_DriveTypeID = 305,
            DATASIGNAL_PSOWindow1ArrayIndex = 306,
            DATASIGNAL_PSOWindow2ArrayIndex = 307,
            DATASIGNAL_PSODistanceArrayIndex = 308,
            DATASIGNAL_AmplifierTemperature = 309,
            DATASIGNAL_PSOBitArrayIndex = 310,
            DATASIGNAL_MXAbsolutePosition = 311,
            DATASIGNAL_ServoUpdateRate = 312,
            DATASIGNAL_SettlingTime = 313,
            DATASIGNAL_InternalErrorCode = 314,
            DATASIGNAL_FirmwareVersionMajor = 315,
            DATASIGNAL_FirmwareVersionMinor = 316,
            DATASIGNAL_FirmwareVersionPatch = 317,
            DATASIGNAL_FirmwareVersionBuild = 318,
            DATASIGNAL_DriveTimerMax = 319,
            DATASIGNAL_MarkerSearchDistance = 320,
            DATASIGNAL_PositionFeedbackGalvo = 327,
            DATASIGNAL_LatchedMarkerPosition = 331,
            DATASIGNAL_TemperatureSensor = 332,
            DATASIGNAL_EthernetDebuggingInformation = 365,
            DATASIGNAL_NpaqSyncoutStatus = 366,
            DATASIGNAL_ResoluteAbsolutePosition = 369,
            DATASIGNAL_FaultPositionFeedback = 372,
            DATASIGNAL_MotorCommutationAngle = 373,
            DATASIGNAL_IOBoardInstalled = 374,
            DATASIGNAL_BusVoltage = 375,
            DATASIGNAL_PiezoVoltageCommand = 376,
            DATASIGNAL_PiezoVoltageFeedback = 377,
            DATASIGNAL_TimeSinceReset = 387,
            DATASIGNAL_MaximumVoltage = 388,
            DATASIGNAL_CommandOutputType = 389,
            DATASIGNAL_DriveFeedforwardOutput = 404,
            DATASIGNAL_LastTickCounter = 405,
            DATASIGNAL_BoardRevision = 407,
            DATASIGNAL_FirmwareRevision = 408,
            DATASIGNAL_GalvoLaserOutput = 409,
            DATASIGNAL_GalvoLaserPowerCorrectionOutput = 414,
            DATASIGNAL_CapacitanceSensorRawPosition = 415,
            DATASIGNAL_PositionCalibrationGalvo = 419,
            DATASIGNAL_BusVoltageNegative = 441,
            DATASIGNAL_ProcessorTemperature = 442,
            DATASIGNAL_InternalErrorTimestamp = 444,
            DATASIGNAL_AmplifierInformation = 445,
            DATASIGNAL_AnalogSensorInput = 446,
            DATASIGNAL_MotorTemperature = 447,
            DATASIGNAL_ResoluteStatus = 449,
            DATASIGNAL_PSOExternalSyncFrequency = 454,
            DATASIGNAL_PositionFeedback = 0,
            DATASIGNAL_PositionCommand = 1,
            DATASIGNAL_PositionError = 2,
            DATASIGNAL_VelocityFeedback = 3,
            DATASIGNAL_VelocityCommand = 4,
            DATASIGNAL_VelocityError = 5,
            DATASIGNAL_AccelerationCommand = 6,
            DATASIGNAL_CurrentError = 9,
            DATASIGNAL_PositionCommandRaw = 12,
            DATASIGNAL_VelocityCommandRaw = 13,
            DATASIGNAL_PositionFeedbackAuxiliary = 14,
            DATASIGNAL_DigitalInput = 15,
            DATASIGNAL_DigitalOutput = 16,
            DATASIGNAL_FixtureOffset = 18,
            DATASIGNAL_CoordinatedPositionTarget = 46,
            DATASIGNAL_DriveStatus = 47,
            DATASIGNAL_AxisStatus = 48,
            DATASIGNAL_AxisFault = 49,
            DATASIGNAL_AccelerationCommandRaw = 50,
            DATASIGNAL_PositionCalibrationAll = 55,
            DATASIGNAL_PositionFeedbackRollover = 69,
            DATASIGNAL_PositionCommandRollover = 70,
            DATASIGNAL_PositionFeedbackAuxiliaryRollover = 71,
            DATASIGNAL_VelocityFeedbackAverage = 72,
            DATASIGNAL_CurrentFeedbackAverage = 73,
            DATASIGNAL_AxisParameter = 76,
            DATASIGNAL_PeakCurrent = 78,
            DATASIGNAL_Backlash = 81,
            DATASIGNAL_HomeState = 82,
            DATASIGNAL_PositionCalibration2D = 83,
            DATASIGNAL_NormalcyDebug = 84,
            DATASIGNAL_TotalMoveTime = 85,
            DATASIGNAL_Stability0SettleTime = 86,
            DATASIGNAL_Stability1SettleTime = 87,
            DATASIGNAL_JerkCommandRaw = 89,
            DATASIGNAL_ProgramPositionCommand = 90,
            DATASIGNAL_GantryOffset = 91,
            DATASIGNAL_PositionOffset = 92,
            DATASIGNAL_CommunicationRealTimeErrors = 93,
            DATASIGNAL_PositionCommandRawBackwardsDiff = 96,
            DATASIGNAL_VelocityCommandRawBackwardsDiffDelta = 97,
            DATASIGNAL_DriveStatusActual = 99,
            DATASIGNAL_GantryRealignState = 100,
            DATASIGNAL_TransformAutoOffset = 101,
            DATASIGNAL_ProgramPositionFeedback = 107,
            DATASIGNAL_JogTrajectoryStatus = 116,
            DATASIGNAL_PingTest = 117,
            DATASIGNAL_GainKposScale = 126,
            DATASIGNAL_GainKiScale = 127,
            DATASIGNAL_GainKpScale = 128,
            DATASIGNAL_GainKpiScale = 129,
            DATASIGNAL_GainAffScale = 130,
            DATASIGNAL_GainVffScale = 131,
            DATASIGNAL_AccelerationTime = 138,
            DATASIGNAL_DecelerationTime = 139,
            DATASIGNAL_AccelerationRate = 140,
            DATASIGNAL_DecelerationRate = 141,
            DATASIGNAL_AccelerationType = 142,
            DATASIGNAL_DecelerationType = 143,
            DATASIGNAL_AccelerationMode = 144,
            DATASIGNAL_DecelerationMode = 145,
            DATASIGNAL_ProgramPosition = 156,
            DATASIGNAL_SpeedTarget = 160,
            DATASIGNAL_PositionCommandPacket = 163,
            DATASIGNAL_DriveSMCMotionState = 168,
            DATASIGNAL_PositionCommandRawCal = 178,
            DATASIGNAL_VelocityCommandRawCal = 179,
            DATASIGNAL_VelocityCommandDrive = 180,
            DATASIGNAL_AccelerationCommandDrive = 181,
            DATASIGNAL_GalvoLaserOutputRaw = 183,
            DATASIGNAL_DriveInterfacePacketInt32 = 186,
            DATASIGNAL_DriveInterfacePacketInt16 = 187,
            DATASIGNAL_DriveInterfacePacketInt8 = 188,
            DATASIGNAL_DriveInterfacePacketDouble = 189,
            DATASIGNAL_DriveInterfacePacketFloat = 190,
            DATASIGNAL_DriveInterfaceCommandCode = 191,
            DATASIGNAL_AccelerationFeedback = 192,
            DATASIGNAL_AccelerationCommandRawCal = 193,
            DATASIGNAL_PositionCalibrationAllDrive = 194,
            DATASIGNAL_BacklashTarget = 196,
            DATASIGNAL_DriveMotionRate = 198,
            DATASIGNAL_DriveMotionDelay = 199,
            DATASIGNAL_CalibrationAdjustmentValue = 200,
            DATASIGNAL_ServoRounding = 201,
            DATASIGNAL_FeedforwardCurrent = 208,
            DATASIGNAL_DriveInterfacePacketInfoBitValue = 210,
            DATASIGNAL_AccelerationError = 223,
            DATASIGNAL_SuppressedFaults = 225,
            DATASIGNAL_DriveInterfacePacketStreamingData = 226,
            DATASIGNAL_PositionCommandRawUnfiltered = 227,
            DATASIGNAL_TransitionOffsetErrors = 228,
            DATASIGNAL_FreezeVelocityCommand = 238,
            DATASIGNAL_FreezeVelocityFeedback = 239,
            DATASIGNAL_InternalPositionOffsets = 241,
            DATASIGNAL_StatusHighLevelOffsetsLastMsec = 242,
            DATASIGNAL_ProgramVelocityCommand = 250,
            DATASIGNAL_ProgramVelocityFeedback = 251,
            DATASIGNAL_DriveMotionDelayLive = 252,
            DATASIGNAL_DriveCommunicationDelay = 253,
            DATASIGNAL_DriveCommunicationDelayLive = 254,
            DATASIGNAL_DriveInterfacePacketResponseInt32 = 256,
            DATASIGNAL_DriveInterfacePacketResponseInt16 = 257,
            DATASIGNAL_DriveInterfacePacketResponseInt8 = 258,
            DATASIGNAL_DriveInterfacePacketResponseDouble = 259,
            DATASIGNAL_DriveInterfacePacketResponseFloat = 260,
            DATASIGNAL_DriveInterfacePacketBit = 261,
            DATASIGNAL_DriveInterfacePacketResponseBit = 262,
            DATASIGNAL_SpeedTargetActual = 266,
            DATASIGNAL_CoordinatedDistanceRemaining = 272,
            DATASIGNAL_SafeZoneState = 323,
            DATASIGNAL_PositionErrorGalvo = 328,
            DATASIGNAL_GainKd1Scale = 333,
            DATASIGNAL_GainKp1Scale = 334,
            DATASIGNAL_MoveReferencePosition = 339,
            DATASIGNAL_MoveReferenceCutterOffset = 358,
            DATASIGNAL_MoveReferenceCornerOffset = 359,
            DATASIGNAL_MoveReferenceTotalOffset = 360,
            DATASIGNAL_GantryMarkerLatchPosition = 368,
            DATASIGNAL_DistanceLog = 378,
            DATASIGNAL_AutoFocusError = 410,
            DATASIGNAL_GalvoLaserOutputRawAdvance = 411,
            DATASIGNAL_GalvoLaserOnDelay = 412,
            DATASIGNAL_GalvoLaserOffDelay = 413,
            DATASIGNAL_CalibrationAdjustmentState = 416,
            DATASIGNAL_AccuracyCorrectionStartingPosition = 417,
            DATASIGNAL_AccuracyCorrectionEndingPosition = 418,
            DATASIGNAL_DriveCommandsDelayed = 424,
            DATASIGNAL_DriveCommandsLost = 425,
            DATASIGNAL_STOStatus = 443,
            DATASIGNAL_DriveAssert = 453,
            DATASIGNAL_VirtualBinaryInput = 51,
            DATASIGNAL_VirtualBinaryOutput = 52,
            DATASIGNAL_VirtualRegisterInput = 53,
            DATASIGNAL_VirtualRegisterOutput = 54,
            DATASIGNAL_Timer = 56,
            DATASIGNAL_TimerPerformance = 57,
            DATASIGNAL_GlobalVariable = 74,
            DATASIGNAL_LibraryCommand = 148,
            DATASIGNAL_DataCollectionSampleTime = 149,
            DATASIGNAL_DataCollectionSampleIndex = 161,
            DATASIGNAL_ZYGOPosition1 = 164,
            DATASIGNAL_ZYGOPosition2 = 165,
            DATASIGNAL_ZYGOPosition3 = 166,
            DATASIGNAL_ZYGOPosition4 = 167,
            DATASIGNAL_PCModbusMasterConnected = 170,
            DATASIGNAL_PCModbusSlaveConnected = 171,
            DATASIGNAL_PCModbusMasterErrorCode = 174,
            DATASIGNAL_PCModbusSlaveErrorCode = 175,
            DATASIGNAL_PCModbusMasterErrorLocation = 176,
            DATASIGNAL_PCModbusSlaveErrorLocation = 177,
            DATASIGNAL_StopWatchTimer = 197,
            DATASIGNAL_ScopetrigID = 209,
            DATASIGNAL_EstimatedProcessorUsage = 236,
            DATASIGNAL_DataCollectionStatus = 255,
            DATASIGNAL_SignalLogState = 270,
            DATASIGNAL_FieldbusConnected = 284,
            DATASIGNAL_FieldbusErrorCode = 285,
            DATASIGNAL_FieldbusErrorLocation = 286,
            DATASIGNAL_FieldbusActiveConnections = 287,
            DATASIGNAL_FieldbusInactiveConnections = 288,
            DATASIGNAL_SafeZoneViolationMask = 289,
            DATASIGNAL_SafeZoneActiveMask = 321,
            DATASIGNAL_FieldbusInputs = 340,
            DATASIGNAL_FieldbusOutputs = 341,
            DATASIGNAL_ModbusMasterInputWords = 342,
            DATASIGNAL_ModbusMasterOutputWords = 343,
            DATASIGNAL_ModbusMasterInputBits = 344,
            DATASIGNAL_ModbusMasterOutputBits = 345,
            DATASIGNAL_ModbusMasterOutputStatusBits = 346,
            DATASIGNAL_ModbusMasterOutputStatusWords = 347,
            DATASIGNAL_ModbusSlaveInputWords = 348,
            DATASIGNAL_ModbusSlaveOutputWords = 349,
            DATASIGNAL_ModbusSlaveInputBits = 350,
            DATASIGNAL_ModbusSlaveOutputBits = 351,
            DATASIGNAL_DriveModbusMasterInputWords = 352,
            DATASIGNAL_DriveModbusMasterOutputWords = 353,
            DATASIGNAL_DriveModbusMasterInputBits = 354,
            DATASIGNAL_DriveModbusMasterOutputBits = 355,
            DATASIGNAL_DriveModbusMasterOutputStatusBits = 356,
            DATASIGNAL_DriveModbusMasterOutputStatusWords = 357,
            DATASIGNAL_SystemParameter = 379,
            DATASIGNAL_ThermoCompSensorTemperature = 420,
            DATASIGNAL_ThermoCompControllingTemperature = 421,
            DATASIGNAL_ThermoCompCompensatingTemperature = 422,
            DATASIGNAL_ThermoCompStatus = 423,
            DATASIGNAL_StepperVerificationMask = 426,
            DATASIGNAL_ProgramLineNumber = 17,
            DATASIGNAL_CoordinatedFlags = 45,
            DATASIGNAL_CoordinatedArcStartAngle = 58,
            DATASIGNAL_CoordinatedArcEndAngle = 59,
            DATASIGNAL_CoordinatedArcRadius = 60,
            DATASIGNAL_CoordinatedArcRadiusError = 61,
            DATASIGNAL_CoordinatedPositionCommand = 62,
            DATASIGNAL_CoordinatedSpeedCommand = 63,
            DATASIGNAL_CoordinatedAccelerationCommand = 64,
            DATASIGNAL_CoordinatedTotalDistance = 65,
            DATASIGNAL_CoordinatedPercentDone = 66,
            DATASIGNAL_CoordinatedPositionCommandBackwardsDiff = 67,
            DATASIGNAL_ProgramVariable = 75,
            DATASIGNAL_TaskParameter = 77,
            DATASIGNAL_TaskErrorCode = 79,
            DATASIGNAL_TaskWarningCode = 80,
            DATASIGNAL_WaitCommandDuration = 103,
            DATASIGNAL_CoordinatedSpeedTargetActual = 104,
            DATASIGNAL_DependentCoordinatedSpeedTargetActual = 105,
            DATASIGNAL_ActiveFixtureOffset = 106,
            DATASIGNAL_TaskStatus0 = 108,
            DATASIGNAL_TaskStatus1 = 109,
            DATASIGNAL_TaskStatus2 = 110,
            DATASIGNAL_Spindle0SpeedTarget = 112,
            DATASIGNAL_Spindle1SpeedTarget = 113,
            DATASIGNAL_Spindle2SpeedTarget = 114,
            DATASIGNAL_Spindle3SpeedTarget = 115,
            DATASIGNAL_CoordinateSystem1I = 118,
            DATASIGNAL_CoordinateSystem1J = 119,
            DATASIGNAL_CoordinateSystem1K = 120,
            DATASIGNAL_CoordinateSystem1Plane = 121,
            DATASIGNAL_ToolNumberActive = 122,
            DATASIGNAL_MFO = 123,
            DATASIGNAL_CoordinatedSpeedTarget = 124,
            DATASIGNAL_DependentCoordinatedSpeedTarget = 125,
            DATASIGNAL_CoordinatedAccelerationRate = 132,
            DATASIGNAL_CoordinatedDecelerationRate = 133,
            DATASIGNAL_CoordinatedAccelerationTime = 134,
            DATASIGNAL_CoordinatedDecelerationTime = 135,
            DATASIGNAL_TaskMode = 137,
            DATASIGNAL_TaskState = 146,
            DATASIGNAL_TaskStateInternal = 147,
            DATASIGNAL_ExecutionMode = 151,
            DATASIGNAL_TaskErrorLocation = 152,
            DATASIGNAL_TaskWarningLocation = 153,
            DATASIGNAL_ProgramPersistent = 154,
            DATASIGNAL_ImmediateState = 155,
            DATASIGNAL_EnableAlignmentAxes = 159,
            DATASIGNAL_QueueStatus = 162,
            DATASIGNAL_CoordinatedGalvoLaserOutput = 169,
            DATASIGNAL_CoordinatedMotionRate = 184,
            DATASIGNAL_CoordinatedTaskCommand = 185,
            DATASIGNAL_FiberMoveCount = 195,
            DATASIGNAL_EnableState = 224,
            DATASIGNAL_CannedFunctionID = 240,
            DATASIGNAL_TaskDoubleVariable = 248,
            DATASIGNAL_TaskInfoVariable = 249,
            DATASIGNAL_TaskReturnVariable = 271,
            DATASIGNAL_FiberPower = 273,
            DATASIGNAL_FiberPowerOptimal = 274,
            DATASIGNAL_FiberPowerSampleCount = 275,
            DATASIGNAL_FiberSearchResult = 276,
            DATASIGNAL_LookaheadMovesExamined = 278,
            DATASIGNAL_ProgramLineNumberInternal = 322,
            DATASIGNAL_ProfileControlMask = 324,
            DATASIGNAL_QueueLineCount = 325,
            DATASIGNAL_QueueLineCapacity = 326,
            DATASIGNAL_ImmediateCommandErrorCode = 329,
            DATASIGNAL_ImmediateCommandErrorLocation = 330,
            DATASIGNAL_CoordinatedArcRadiusReciprocal = 363,
            DATASIGNAL_MotionEngineStage = 367,
            DATASIGNAL_CoordinatedTimeScale = 370,
            DATASIGNAL_CoordinatedTimeScaleDerivative = 371,
            DATASIGNAL_IFOVSpeedScale = 380,
            DATASIGNAL_IFOVSpeedScaleAverage = 381,
            DATASIGNAL_IFOVGenerationFrameCounter = 382,
            DATASIGNAL_IFOVGenerationTimeOriginal = 383,
            DATASIGNAL_IFOVGenerationTimeModified = 384,
            DATASIGNAL_IFOVCoordinatedPositionCommand = 385,
            DATASIGNAL_IFOVCoordinatedSpeedCommand = 386,
            DATASIGNAL_IFOVCenterPointH = 390,
            DATASIGNAL_IFOVCenterPointV = 391,
            DATASIGNAL_IFOVTrajectoryCount = 392,
            DATASIGNAL_IFOVTrajectoryIndex = 393,
            DATASIGNAL_IFOVAttemptCode = 394,
            DATASIGNAL_IFOVGenerationFrameIndex = 395,
            DATASIGNAL_IFOVMaximumVelocity = 396,
            DATASIGNAL_IFOVIdealVelocity = 397,
            DATASIGNAL_TaskInternalDebug = 398,
            DATASIGNAL_IFOVCoordinatedAccelerationCommand = 399,
            DATASIGNAL_IFOVFOVPositionH = 400,
            DATASIGNAL_IFOVFOVPositionV = 401,
            DATASIGNAL_IFOVFOVDimensionH = 402,
            DATASIGNAL_IFOVFOVDimensionV = 403,
            DATASIGNAL_MotionBufferElements = 427,
            DATASIGNAL_MotionBufferMoves = 428,
            DATASIGNAL_MotionLineNumber = 429,
            DATASIGNAL_MotionBufferRetraceMoves = 430,
            DATASIGNAL_MotionBufferRetraceElements = 431,
            DATASIGNAL_MotionBufferIndex = 432,
            DATASIGNAL_MotionBufferIndexLookahead = 433,
            DATASIGNAL_MotionBufferProcessingBlocked = 434,
            DATASIGNAL_ActiveMoveValid = 435,
            DATASIGNAL_TaskExecutionLines = 436,
            DATASIGNAL_SchedulerTaskHolds = 437,
            DATASIGNAL_SchedulerProgramLoopRuns = 438,
            DATASIGNAL_SchedulerTaskBlocked = 439,
            DATASIGNAL_CriticalSectionActive = 440,
            DATASIGNAL_AxesSlowdownReason = 448,
            DATASIGNAL_TaskExecutionTime = 450,
            DATASIGNAL_TaskExecutionTimeMaximum = 451,
            DATASIGNAL_TaskExecutionLinesMaximum = 452,
            DATASIGNAL_LookaheadDecelReason = 455,
            DATASIGNAL_LookaheadDecelMoves = 456,
            DATASIGNAL_LookaheadDecelDistance = 457,
        }


        #endregion [ enum ]

        public const string GANTRY_SECTION_NAME = "Gantry";


        // c# Struct not support basic Constructor -> Class 
        #region [ struct ]
        public struct stGantryInfo
        {
            public bool bGantryUse;
            public uint iSlaveHomeUse;
            public double fSlaveHomeOffset;
            public double fSlaveHomeRange;

            public int iMasterAxis;
            public int iSlaveAxis;
            /*
           public stGantryInfo()
            {
                Clear();
            }
            */
            public void Clear()
            {
                bGantryUse = false;
                iSlaveHomeUse = Convert.ToUInt32(eGantryHomeMethod.BOTH);
                fSlaveHomeOffset = 0.0;
                fSlaveHomeRange = 100.0;

                iMasterAxis = Convert.ToInt32(eMotorName.None);
                iSlaveAxis = Convert.ToInt32(eMotorName.None);
            }

        }

        //Param Setting

        public struct stAxisInfoStatus
        {
        }
        public struct stMotionInfoStatus
        {
            /*		 
                -[00001h] Bit 0, +Limit 급정지신호현재상태 o
                -[00002h] Bit 1, -Limit 급정지신호현재상태 o
                -[00004h] Bit 2, +limit 감속정지현재상태   x
                -[00008h] Bit 3, -limit 감속정지현재상태   x
                -[00010h] Bit 4, Alarm 신호신호현재상태    o
                -[00020h] Bit 5, InPos 신호현재상태        o
                -[00040h] Bit 6, 비상정지신호(ESTOP) 현재상태
                -[00080h] Bit 7, 원점신호헌재상태
                -[00100h] Bit 8, Z 상입력신호현재상태
            */
            public bool m_bIsLimitP;
            public bool m_bIsLimitN;
            public bool m_bIsAlarm;
            public bool m_bIsInPos;
            public bool m_bIsEStop;
            public bool m_bIsOrg;
            public bool m_bIsZPhase;
            /*
            public stMotionInfoStatus()
            {
                Clear();
            }
            */
            public void Clear()
            {
                m_bIsLimitP = false;
                m_bIsLimitN = false;
                m_bIsAlarm = false;
                m_bIsInPos = false;
                m_bIsEStop = false;
                m_bIsOrg = false;
                m_bIsZPhase = false;
            }
        }
        public struct stMotionPositionStatus
        {
            double m_fCmdPos;
            double m_fActPos;
            double m_fVelocity;
            double m_fErrPos;
            int m_iDecimalPoint;
            string m_strDecimalFormat;
            string m_strCmdPos;
            string m_strActPos;
            string m_strVelocity;
            string m_strErrPos;

            public string strCmdPos { get { return m_strCmdPos; } }
            public string strActPos { get { return m_strActPos; } }
            public string strVelocity { get { return m_strVelocity; } }
            public string strErrPos { get { return m_strErrPos; } }

            public double fCmdPos
            {
                get { return m_fCmdPos; }
                set
                {
                    m_fCmdPos = value;
                    m_strCmdPos = m_fCmdPos.ToString(m_strDecimalFormat);
                }
            }
            public double fActPos
            {
                get { return m_fActPos; }
                set
                {
                    m_fActPos = value;
                    m_strActPos = m_fActPos.ToString(m_strDecimalFormat);
                }
            }
            public double fVelocity
            {
                get { return m_fVelocity; }
                set
                {
                    m_fVelocity = value;
                    m_strVelocity = m_fVelocity.ToString(m_strDecimalFormat);
                }
            }
            public double fErrPos
            {
                get { return m_fErrPos; }
                set
                {
                    m_fErrPos = value;
                    m_strErrPos = m_fErrPos.ToString(m_strDecimalFormat);
                }
            }
            /*
            public stMotionPositionStatus()
            {
                Clear();
            }
            */
            public void Clear()
            {
                m_iDecimalPoint = 4;
                m_fCmdPos = 0.0;
                m_fActPos = 0.0;
                m_fVelocity = 0.0;
                m_fErrPos = 0.0;
                m_strDecimalFormat = string.Format("F{0}", m_iDecimalPoint);
                m_strCmdPos = m_fCmdPos.ToString(m_strDecimalFormat);
                m_strActPos = m_fActPos.ToString(m_strDecimalFormat);
                m_strVelocity = m_fVelocity.ToString(m_strDecimalFormat);
                m_strErrPos = m_fErrPos.ToString(m_strDecimalFormat);
            }
        }


        public struct stMotionUserMoveParamSetting
        {
            //< User Move Parameter Setting >
            public double m_dInitPos;
            public double m_dInitVel;
            public double m_dInitAccel;
            public double m_dInitDecel;

            /*
            public stMotionUserMoveParamSetting()
            {
                Clear();
            }*/
            public void Clear()
            {
                m_dInitPos = 0.0;
                m_dInitVel = 0.0;
                m_dInitAccel = 0.0;
                m_dInitDecel = 0.0;
            }
        }
        public struct stMotionHomeSearchStatus
        {
            public bool m_bHomeStatus_Signal;
            public String m_strHomeStatus_Search;
            public String m_strHomeStatus_StepMain;
            public String m_strHomeStatus_StepSub;
            public int m_nHomeStatus_ProgressBar;

            /*
            public stMotionHomeSearchStatus()
            {
                Clear();
            }*/
            public void Clear()
            {
                m_bHomeStatus_Signal = false;
                m_strHomeStatus_Search = "";
                m_strHomeStatus_StepMain = "";
                m_strHomeStatus_StepSub = "";
                m_nHomeStatus_ProgressBar = 0;
            }

        }
        public struct stMotionSpeedSetting : IXmlSerializable
        {
            public double m_dMaxSpeed;
            public double m_dAcceleration;
            public double m_dDeceleration;
            public double m_dCurveAcceleration;
            public double m_dCurveDeceleration;
            /*
            public stMotionSpeedSetting()
            {
                Clear();
            }
            */
            public void Clear()
            {
                m_dMaxSpeed = 0.0;
                m_dAcceleration = 0.0;
                m_dDeceleration = 0.0;
                m_dCurveAcceleration = 0.0;
                m_dCurveDeceleration = 0.0;
            }

            public XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(XmlReader reader)
            {
                if (reader.HasAttributes)
                {
                    double.TryParse(reader.GetAttribute("MaxSpeed"), out m_dMaxSpeed);
                    double.TryParse(reader.GetAttribute("Accel"), out m_dAcceleration);
                    double.TryParse(reader.GetAttribute("Decel"), out m_dDeceleration);
                    double.TryParse(reader.GetAttribute("CurveAccel"), out m_dCurveAcceleration);
                    double.TryParse(reader.GetAttribute("CurveDecel"), out m_dCurveDeceleration);

                }
            }

            public void WriteXml(XmlWriter writer)
            {
                writer.WriteAttributeString("MaxSpeed", string.Format("{0:0.000}", m_dMaxSpeed));
                writer.WriteAttributeString("Accel", string.Format("{0:0.000}", m_dAcceleration));
                writer.WriteAttributeString("Decel", string.Format("{0:0.000}", m_dDeceleration));
                writer.WriteAttributeString("CurveAccel", string.Format("{0:0.000}", m_dCurveAcceleration));
                writer.WriteAttributeString("CurveDecel", string.Format("{0:0.000}", m_dCurveDeceleration));
            }
        }

       
        #endregion [ struct ]

        #region [ Path ]
        public static string ConfigFolderPath
        {
            get
            {
                string strPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                strPath = System.IO.Path.GetDirectoryName(strPath);
                string strConfigFolderPath = Path.GetFullPath(Path.Combine(strPath, @"..\System\CFG\"));

                DirectoryInfo DirInfo = new DirectoryInfo(strConfigFolderPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();

                return strConfigFolderPath;

            }
        }
        public static string ConfigMotionFolderPath
        {
            get
            {
                string strPath = Path.GetFullPath(Path.Combine(ConfigFolderPath, @"Motion"));
                DirectoryInfo DirInfo = new DirectoryInfo(strPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();

                return strPath;

            }
        }
        public static string ConfigMotionAxisDataFileFullname
        {
            get
            {
                string strPath = ConfigMotionFolderPath;
                string FileName = Path.GetFullPath(Path.Combine(strPath, @"MotionAxisLinkData.xml"));
                DirectoryInfo DirInfo = new DirectoryInfo(strPath);
                if (DirInfo.Exists == false)
                    DirInfo.Create();

                return FileName;

            }
        }
        public static string ConfigMotionAxisDataXmlFileName
        {
            get { return "MotionAxisLinkData.xml"; }
        }
        public static string ConfigMotionMotFileFullName
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strConfigVisionFolderPath = Path.GetFullPath(Path.Combine(strPath, @"Motion"));
                string strConfigVisionFilePath = Path.GetFullPath(Path.Combine(strPath, @"Motion\MotionConfig.mot"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigVisionFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();


                FileInfo FileInfor = new FileInfo(strConfigVisionFilePath);
                if (FileInfor.Exists == false)
                {
                    using (FileStream s = FileInfor.Create())
                    {
                        s.Close();
                    }
                }

                return strConfigVisionFilePath;
            }
        }
        public static string ConfigMotionSpeedIniFileFullName
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strConfigVisionFolderPath = Path.GetFullPath(Path.Combine(strPath, @"Motion"));
                string strConfigVisionFilePath = Path.GetFullPath(Path.Combine(strPath, @"Motion\MotionSpeed.ini"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigVisionFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();


                FileInfo FileInfor = new FileInfo(strConfigVisionFilePath);
                if (FileInfor.Exists == false)
                {
                    using (FileStream s = FileInfor.Create())
                    {
                        s.Close();
                    }
                }
                return strConfigVisionFilePath;
            }
        }
        public static string ConfigMotionGantryIniFileFullName
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strConfigVisionFolderPath = Path.GetFullPath(Path.Combine(strPath, @"Motion"));
                string strConfigVisionFilePath = Path.GetFullPath(Path.Combine(strPath, @"Motion\Gantry.ini"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigVisionFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();


                FileInfo FileInfor = new FileInfo(strConfigVisionFilePath);
                if (FileInfor.Exists == false)
                {
                    using (FileStream s = FileInfor.Create())
                    {
                        s.Close();
                    }
                }

                return strConfigVisionFilePath;
            }
        }
        public static string ConfigMotionSigFileFullName
        {
            get
            {
                string strPath = ConfigFolderPath;
                string strConfigVisionFolderPath = Path.GetFullPath(Path.Combine(strPath, @"Motion"));
                string strConfigVisionFilePath = Path.GetFullPath(Path.Combine(strPath, @"Motion\MotorSignal.ini"));
                DirectoryInfo DirInfor = new DirectoryInfo(strConfigVisionFolderPath);
                if (DirInfor.Exists == false)
                    DirInfor.Create();


                FileInfo FileInfor = new FileInfo(strConfigVisionFilePath);
                if (FileInfor.Exists == false)
                {
                    using (FileStream s = FileInfor.Create())
                    {
                        s.Close();
                    }
                }

                return strConfigVisionFilePath;
            }
        }
        #endregion[ Path ]


    }
}
