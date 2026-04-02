# EzInaFlex 2.0 — Project Guide for Claude

## Project Overview

레이저 마킹/드릴링 설비 제어 소프트웨어.
DKT MultiArray 설비용 HMI + 하드웨어 통합 제어 시스템.

- **Language:** C# (.NET Framework 4.5.2)
- **UI:** Windows Forms (SXGA 1280×1024)
- **IDE:** Visual Studio 2017+
- **Solution:** `EzInaFlex2.0.sln` (30+ 프로젝트)

---

## Architecture

### 레이어 구조

```
FA/          - 전역 싱글톤, 상수, 설정 (MGR.cs, DEF.cs, SYS.cs, OPT.cs, LOG.cs)
MGR/         - 디바이스 매니저 (Motion, Laser, IO, Light, PowerMeter, Vision)
RUN/         - 공정 시퀀스 실행 (RunningManager + 모듈별 상태머신)
FORM/        - WinForms UI (팝업, 설정화면, SXGA 레이아웃)
MF/          - 레시피, 유저, 알람 마스터 데이터
UC/          - 공통 유틸리티 클래스
```

### 핵심 파일

| 파일 | 역할 |
|------|------|
| `EzInaFlex/FA/MGR.cs` | 전역 싱글톤 매니저 레지스트리 |
| `EzInaFlex/FA/DEF.cs` | 축/DI/DO/Cylinder/Task 열거형 정의 |
| `EzInaFlex/FA/SYS.cs` | 전역 시스템 상태 |
| `EzInaFlex/RUN/RunningManager.cs` | 공정 실행 상태머신 최상위 |
| `EzInaFlex/RUN/Modules/RunningScanner.cs` | 스캐너/마킹 시퀀스 (70+ 상태) |
| `EzInaFlex/RUN/Modules/RunningStage.cs` | XY 스테이지 이동 시퀀스 |
| `EzInaFlex/MGR/IO/IOManager.cs` | DI/DO/AI/AO/Cylinder 통합 관리 |
| `EzInaScanLAB/ScanlabRTC5.cs` | RTC5 스캐너 카드 추상화 |
| `EzInaMotion/MotionInterface.cs` | 모션 컨트롤러 인터페이스 |
| `EzInaCommucation/CommunicationBase.cs` | Serial/Socket 통신 베이스 |
| `EzInaThread/ThreadManager.cs` | 쓰레드 수명 관리 |

---

## Hardware

### 모션 컨트롤러
- **Aerotech A3200** (`EzInaMotionAerotech/`)
- **AXT** (`EzInaMotionAXT/`)
- 지원 축: RX, Y, RZ, RAIL_ADJUST, PINCH_ROLLER (L/R, U/B) 등 최대 10축

### 레이저
- **IPG Photonics GLPM/GLPM_VER8** (`EzInaLaserIPG/`)
- **Lumentum PicoBlade2** (`EzInaLaserLumentum/`)
- **Talon 355nm** (`EzInaLaserTalon/`)

### 스캐너 카드
- **Scanlab RTC5** (`EzInaScanLAB/`) — P/Invoke via `RTC5Import.RTC5Wrap`
- 듀얼 헤드 지원: HEAD_A, HEAD_B
- `BitField32Helper`로 상태 레지스터 파싱

### 조명
- **DaeShin LP205R** (`EzInaLightDaeShin/`)
- **JoySystem JPSeries** (`EzInaLightJoySystem/`)

### 파워미터
- **Ohpir SPC** (`EzInaPowerMeterOhpir/`)
- **Zentec PLink** (`EzInaPowerMeterZentec/`)

### 기타
- **Attenuator:** OPTOGAMA (`EzInaAttenuatorOPTOGAMA/`)
- **온도:** OMEGA Thermocouple (`EzInaOMEGA_TC/`)
- **카메라:** Euresys, MultiCam (`EzInaVision*/`)
- **I/O:** DI/DO 64채널, AI/AO, 공압 실린더

### 통신 프로토콜
- Serial (RS-232/485): 보드레이트/패리티/데이터비트 설정
- TCP/IP Socket: IP/포트 설정
- 패킷 타임아웃: `StopWatchTimer` 기반

---

## Naming Conventions

### 헝가리안 표기법 (전체 코드베이스 적용)

| 접두사 | 타입 |
|--------|------|
| `m_` | 멤버 변수 |
| `a_` | 메서드 파라미터 |
| `p` | 참조/포인터 |
| `str` | string |
| `i` | int |
| `b` | bool |
| `d` | double |
| `l` | long |
| `f` | float |
| `n` | numeric (일반) |
| `vec` | List/Collection |
| `dic` | Dictionary |

### 클래스/타입 명명
- 공개 클래스: PascalCase (`MotionManager`, `LaserInterface`)
- 내부 구현 클래스: `C` 접두사 (`CMotionA3200`, `CMotionAXT`)
- 열거형: `e` 접두사 (`eAxesName`, `eDI`, `eDO`, `eTasks`)
- 구조체: `st` 접두사 (`stRunInfo`, `stCylinder`, `stMotionPositionStatus`)
- 열거형 값: ALL_CAPS_UNDERSCORE (`EMERGENCY`, `JIG_EXIST_CHK`)

### UI 컴포넌트
- Form: `Frm` + 기능 + 해상도 (`FrmPopupAlarm`, `FrmInforSetupMotion_SXGA`)
- 컨트롤: `btn_`, `lbl_`, `dgv_`, `listBox_` 접두사

---

## Domain Glossary

| 용어 | 설명 |
|------|------|
| **FA** | Factory Automation — 코어 네임스페이스 |
| **MGR** | Manager — 디바이스/서브시스템 싱글톤 오케스트레이터 |
| **RUN** | Running — 공정 시퀀스 실행 모듈 |
| **Recipe** | 가공 파라미터 세트 (레이저 파워, 속도, 위치 등) |
| **Jog** | 수동 축 이동 (Slow/Fast/Run/User 속도 레벨) |
| **Home/Homing** | 원점 복귀 시퀀스 |
| **InPos** | In-Position — 모션 서보가 목표 위치 도달 확인 |
| **E_STOP** | 비상정지 (즉시 정지) |
| **SD_STOP** | Slow-Down Stop (감속 정지) |
| **Galvo** | 갈바노미터 스캐너 미러 (X/Y축) |
| **RTC5** | Scanlab RTC5 스캐너 제어 카드 |
| **Head A/B** | 듀얼 스캐너 헤드 |
| **List** | Scanlab 내부 커맨드 버퍼 |
| **DI** | Digital Input (센서 입력) |
| **DO** | Digital Output (솔레노이드/램프 제어) |
| **AI/AO** | Analog Input/Output |
| **Cylinder** | 공압 실린더 (전진/후진 센서 + 솔레노이드) |
| **Chattering** | 디지털 입력 채터링 (카운터 기반 디바운스) |
| **Jig** | 워크피스 고정 지그 |
| **Marking** | 레이저 마킹 동작 |
| **Process Position** | 마킹 동작을 위한 특정 XY 좌표 |
| **Alarm Code** | 번호화된 알람 메시지 (최대 1000개) |

---

## Build Configurations

| 구성 | 용도 |
|------|------|
| `Debug` | 개발/디버깅 |
| `Release` | 배포 |
| `SIM_Debug` | 하드웨어 없이 시뮬레이션 (`#define SIM`) |
| `Remote_Debug` | 원격 디버깅 |

> **SIM 모드**: 하드웨어 연결 없이 UI/로직 테스트 시 사용. 실제 디바이스 명령 차단됨.

---

## Configuration Files

| 파일 | 경로 | 형식 |
|------|------|------|
| 레이저 디바이스 | `../Config/Laser/LaserDeviceLinkData.xml` | XML |
| 모션 축 | `{GDMotion.ConfigMotionAxisDataFileFullname}` | XML |
| IO 매핑 | `{FolderPath.ConfigIOItemDataFileFullname}` | XML |
| 모션 속도 프로파일 | INI 파일 | INI |
| 로그 설정 | `DLL/log4net.xml` | XML |

---

## Threading Model

- **전통적 Thread 모델** — async/await 미사용
- `ThreadManager`: 모든 관리 쓰레드의 수명 관리
- `ThreadItem`: 개별 쓰레드 상태 (Start → Loop → Stop → Join)
- `ConcurrentDictionary<FA.DEF.eTasks, bool>`: 태스크 완료 추적
- 동기화: `object m_lock = new object()`, `LinkedList` 큐

### 상태머신 패턴
```csharp
// 각 RUN 모듈은 enum으로 상태 정의
public enum eMODULE_SEQ_PROC { IDLE, START, ..., DONE }  // 180+ 상태
// eSEQ_Status: DONE / BUSY / JAM 으로 모듈 간 동기화
```

---

## Safety-Critical Code — 수정 시 주의

> 아래 영역은 설비 안전과 직결됨. 변경 전 반드시 확인 요청.

- **E-STOP / SD_STOP 핸들러** (`MotionInterface.cs`)
- **인터락 DI 정의** (`DEF.cs` — `eDI.EMERGENCY` 등)
- **알람 코드 배열** (`Alarms.Items[1000]`)
- **실린더 제어 로직** (전진/후진 센서 검증 후 솔레노이드 출력)
- **모션 전 사전 검증** — IO 체크 → 실린더 체크 → 센서 검증 순서

---

## Logging

log4net 기반, 3개 독립 로그:
- **MC** (Motion Control): 모션/IO/하드웨어 이벤트
- **SW** (Software): 애플리케이션 로직
- **PM** (Power Meter): 파워미터 측정값

알람 로그 형식: `[ALARM],{code:D4},{message}`

---

## Database

- **Oracle** (ODP.NET: `Oracle.ManagedDataAccess 19.12.0`)
- **MES 연동**: `EzInaFlex/MGR/DKT_MES/DKT_DBManager.cs`

---

## Key Patterns to Follow

1. **싱글톤 접근**: `MGR.cs`의 전역 인스턴스를 통해 디바이스 접근
2. **IDisposable**: finalizer + `Dispose(bool)` + `IsDisposed` 플래그 패턴 유지
3. **인터페이스 구현**: 새 디바이스는 반드시 해당 `Interface` 클래스 상속
4. **열거형**: 새 축/DI/DO/태스크는 `DEF.cs`에 추가
5. **XML 설정**: 하드코딩 대신 Config XML 파일 참조
6. **헝가리안 표기법**: 기존 코드 컨벤션 유지 필수
