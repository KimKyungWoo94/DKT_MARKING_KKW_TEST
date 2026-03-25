using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.MF
{
	/// <summary>
	/// 기존 데이터 맵 칩위치 X/Y 자료의 크기를 줄이기위해 short로 처리함.
	/// </summary>
	public struct CellPoint
	{
		public short X;
		public short Y;

		public void Init()
		{
			X = 0;
			Y = 0;
		}

		public void Set(int x, int y)
		{
			X = (short)x;
			Y = (short)x;
		}
	}

	/// <summary>
	/// 데이터 맵
	/// </summary>
	public class DataMap
	{
		public const int _STRING_LENGTH = 500;
		public int m_nRows;
		public int m_nColumns;
		public int m_nArrCount; // 정사각형
		public CellPoint[]	m_ReadPt;								// [ChipAllCount] 파일에서 읽은 데이터맵의 Col, ROW 인덱스
		public EzInaGui.UserControls.eCellStatus[] m_eCellStatus;	// [ChipAllCount] 칩 랭크
		public string[] m_strValue;									// [ChipAllCount] 칩 측정 값들..(문자로)

		#region 리밋 계산후 알수 있음
		public bool m_IsLimitCalc;		// 최외곽 셀 위치 계산 여부
		public int m_iChipCount;		// 칩 총갯수(Zero Rank 포함)
		public int m_iCol_Start;		// Col 시작 위치 인덱스
		public int m_iRow_Start;		// Col 끝   위치 인덱스
		public int m_iCol_End;			// Row 시작 위치 인덱스
		public int m_iRow_End;			// Row 끝   위치 인덱스
		#endregion

		public int nArrCellCount { get { return m_nRows * m_nColumns; } } // 2차원 배열 총 길이
		public int iArrCenCol { get { return m_nColumns / 2; } } // 배열 중심 위치
		public int iArrCenRow { get { return m_nRows / 2;	} } // 배열 중심 위치
		public int MaxRank { get { return 150; } } // 최대 랭크 갯수

		/// <summary>
		/// 생성자
		/// </summary>
		/// <param name="arrcnt"> 배열 크기</param>
		public DataMap(int a_nRows, int a_nColumns)
		{
			m_nRows			= a_nRows;
			m_nColumns		= a_nColumns;
			m_nArrCount		= a_nRows * a_nColumns;
			m_ReadPt		= new CellPoint[nArrCellCount];
			m_eCellStatus	= new EzInaGui.UserControls.eCellStatus[nArrCellCount];
			m_strValue		= new string[m_nRows * m_nColumns];
			ClearAll();
		}

		/// <summary>
		/// 자료 초기화
		/// </summary>
		public void ClearAll()
		{
			for (int i = 0; i < nArrCellCount; i++)
			{
				m_strValue		[i] = "";
				m_ReadPt		[i] = new CellPoint();
				m_eCellStatus	[i] = EzInaGui.UserControls.eCellStatus.Empty;
			}

			m_IsLimitCalc	= false;
			m_iChipCount	= 0;
			m_iCol_Start	= 0;
			m_iRow_Start	= 0;
			m_iCol_End		= m_nColumns - 1;
			m_iRow_End		= m_nRows -1;
		}

		/// <summary>
		/// 특정 셀의 상태 리턴
		/// </summary>
		/// <param name="col">가로셀</param>
		/// <param name="row">세로셀</param>
		/// <returns>Rank</returns>
		public EzInaGui.UserControls.eCellStatus GetCellStatus(int a_iCol, int a_iRow)
		{
			return m_eCellStatus[a_iRow * m_nColumns + a_iCol];
		}

		/// <summary>
		/// 시작/종료셀 인덱스 계산
		/// </summary>
		public void CalcLimit()
		{
			bool exist = false;
			m_iCol_Start	= int.MaxValue;
			m_iRow_Start	= int.MaxValue;
			m_iCol_End		= int.MinValue;
			m_iRow_End		= int.MinValue;

			int i, j;

			for (i = 0; i < m_nColumns; i++)
			{
				for (j = 0; j < m_nRows; j++)
				{
					if (m_eCellStatus[j * m_nColumns + i] >= EzInaGui.UserControls.eCellStatus.Empty && m_eCellStatus[j * m_nColumns + i] <= EzInaGui.UserControls.eCellStatus.Max)
					{
						if (i < m_iCol_Start) m_iCol_Start	= i;
						if (j < m_iRow_Start) m_iRow_Start	= j;
						if (i > m_iCol_End	) m_iCol_End	= i;
						if (j > m_iRow_End	) m_iRow_End	= j;
						exist = true;
					}
				}
			}

			if (!exist)
			{
				m_iCol_Start	= 0;
				m_iRow_Start	= 0;
				m_iCol_End		= m_nColumns - 1;
				m_iRow_End		= m_nRows	 - 1;
				m_IsLimitCalc = false;
			}
			else
				m_IsLimitCalc = true;
		}

		/// <summary>
		/// 가로 리밋 길이
		/// </summary>
		public int ColLength
		{
			get { return m_iCol_End - m_iCol_Start + 1; }
		}

		/// <summary>
		/// 세로 리밋 길이
		/// </summary>
		public int RowLength
		{
			get { return m_iRow_End - m_iRow_Start + 1; }
		}

		/// <summary>
		/// X/Y/R 미러 실행
		/// </summary>
		/// <param name="XMirror"></param>
		/// <param name="YMirror"></param>
		/// <param name="RMirror"></param>
		public void Mirror(bool XMirror, bool YMirror, bool RMirror)
		{
			if (!XMirror && !YMirror && !RMirror)
				return;

			int minX, maxX, minY, maxY;
			int i, j, n;

			CellPoint[] TmpReadPt = new CellPoint[nArrCellCount];
			EzInaGui.UserControls.eCellStatus[] TmpECellStatus = new EzInaGui.UserControls.eCellStatus[nArrCellCount];
			string[] TmpStrValue = new string[nArrCellCount];

			for (i = 0; i < nArrCellCount; i++)
			{
				TmpReadPt		[i] = new CellPoint();
				TmpECellStatus	[i] = EzInaGui.UserControls.eCellStatus.Empty;
				TmpStrValue		[i] = "";
			}


			if (RMirror)
			{
				minX = m_nArrCount;
				maxX = 0;
				minY = m_nArrCount;
				maxY = 0;
				for (i = 0; i < m_nArrCount; i++)
				{
					for (j = 0, n = m_nArrCount - 1; j < m_nArrCount; j++, n--)
					{
						if (m_eCellStatus	[j * m_nArrCount + i] >= EzInaGui.UserControls.eCellStatus.Empty 
							&& m_eCellStatus[j * m_nArrCount + i] <= EzInaGui.UserControls.eCellStatus.Max)
						{
							TmpReadPt		[i * m_nArrCount + n] = m_ReadPt		[j * m_nArrCount + i];
							TmpECellStatus	[i * m_nArrCount + n] = m_eCellStatus	[j * m_nArrCount + i];
							TmpStrValue		[i * m_nArrCount + n] = m_strValue		[j * m_nArrCount + i];
																									  
							if (n >= maxX)
								maxX = n;
							if (n <= minX)
								minX = n;
							if (i >= maxY)
								maxY = i;
							if (i <= minY)
								minY = i;
						}
					}
				}
			}
			else
			{
				for (i = 0; i < m_nArrCount; i++)
				{
					for (j = 0; j < m_nArrCount; j++)
					{
						TmpReadPt		[j * m_nArrCount + i] = m_ReadPt		[j * m_nArrCount + i];
						TmpECellStatus	[j * m_nArrCount + i] = m_eCellStatus	[j * m_nArrCount + i];
						TmpStrValue		[j * m_nArrCount + i] = m_strValue		[j * m_nArrCount + i];
					}
				}
			}

			for (i = 0; i < nArrCellCount; i++)
			{
				m_ReadPt		[i] = new CellPoint();
				m_eCellStatus	[i] = EzInaGui.UserControls.eCellStatus.Empty;
				m_strValue		[i] = "";
			}

			//XY Mirror
			int ti = 0, tj = 0;
			for (i = 0; i < m_nArrCount; i++)
			{
				for (j = 0; j < m_nArrCount; j++)
				{
					if (TmpECellStatus[j * m_nArrCount + i] >= EzInaGui.UserControls.eCellStatus.Empty
						&& TmpECellStatus[j * m_nArrCount + i] <= EzInaGui.UserControls.eCellStatus.Max)
					{
						if (XMirror)
							ti = m_nColumns - i;
						else
							ti = i;

						if (YMirror)
							tj = m_nRows - j;
						else
							tj = j;

						m_ReadPt		[tj * m_nArrCount + ti] = TmpReadPt			[j * m_nArrCount + i];
						m_eCellStatus	[tj * m_nArrCount + ti] = TmpECellStatus	[j * m_nArrCount + i];
						m_strValue		[tj * m_nArrCount + ti] = m_strValue		[j * m_nArrCount + i];
					}
				}
			}
		}

		/// <summary>
		///  스트림으로 데이터맵 저장
		/// </summary>
		/// <param name="fs">Stream </param>
		/// <param name="bw">BinaryWriter</param>
		//public void SaveStream(Stream fs, BinaryWriter bw)
		//{
		//    bw.Write((Int32 )ArrCount     );
		//    for(int i=0; i<ArrCellCount; i++)
		//        bw.Write((byte  )Rank[i]    );
		//    for(int i=0; i<ArrCellCount; i++)
		//    {
		//        bw.Write((short )ReadPt[i].X);
		//        bw.Write((short )ReadPt[i].Y);
		//    }
		//    for(int i=0; i<ArrCellCount; i++)
		//        bw.Write(Encoding.ASCII.GetBytes(sValue[i].Trim().PadRight(_STRING_LENGTH)),0,_STRING_LENGTH);
		//}

		/// <summary>
		///  스트림에서 데이터맵 불러오기
		/// </summary>
		/// <param name="fs">Stream </param>
		/// <param name="bw">BinaryReader</param>
		//public bool ReadStream(Stream fs, BinaryReader br)
		//{
		//    try
		//    {
		//        int cnt =br.ReadInt32();
		//        if(ArrCount != cnt)
		//        {
		//            LOG.Info("DATA_MAP","ReadStream ArrCount Error, This ArrCount={0}, read ArrCount",ArrCount,cnt);
		//            return false;
		//        }
		//
		//        for(int i=0; i<ArrCellCount; i++)
		//            Rank[i] = (eRank)br.ReadByte();
		//
		//        for (int i=0; i<ArrCellCount; i++)
		//        {
		//            ReadPt[i].X = br.ReadInt16();
		//            ReadPt[i].Y = br.ReadInt16();
		//        }
		//
		//        for (int i=0; i<ArrCellCount; i++)
		//            sValue[i] = Encoding.Default.GetString(br.ReadBytes(_STRING_LENGTH)).Trim();
		//
		//        CalcLimit();
		//        return true;
		//    }
		//    catch(Exception ex)
		//    {
		//        LOG.Info("DATA_MAP","ReadStream Error,msg"+ex.Message);
		//        return false;
		//    }
		//}
	}
}
