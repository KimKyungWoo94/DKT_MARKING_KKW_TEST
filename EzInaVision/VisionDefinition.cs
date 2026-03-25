using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzInaVision.GDV
{
	public struct stClosePointToCenter
	{
		public double fPosX;
		public double fPosY;
		public double fAngle;
		public double fScore;

		public double fSetR;
		public double fMaxR;
		public double fAVGAngle;

		public void Clear()
		{
			fPosX = fPosY = fAngle = 0.0;
			fScore = 0.0;
			fSetR = 0.0;
			fMaxR = 9999999.0;
			fAVGAngle = 0.0;
		}

		public void Initialize()
		{
			fPosX = fPosY = fAngle = 0.0;
			fScore = 0.0;
			fSetR = 0.0;
			fMaxR = 9999999.0;
			fAVGAngle = 0.0;
		}

	}
	public struct stRoi
	{
		public int nOrgX;
		public int nOrgY;
		public int Width;
		public int Height;

	}

	public enum eImageType
	{
		Regular,
		Filter,
		Calibrate,
	}
	//ks2
	//{{
	public enum eChipStatus
	{
		CHIP_BLANK = 0,
		CHIP_EXISTS = 1,   // none
	};
    public class MatchResult : ICloneable, IDisposable
    {
        public int m_nMatchNumber;
        public int m_nMatchRoiNumber;
        public double m_fPosX;
        public double m_fPosY;
        public double m_fScore;
        public double m_fAngle;
        public float m_fDispPosX;
        public float m_fDispPosY;
        //add by smpark 
        public float m_fSensorXPos;
        public float m_fSensorYPos;
        public float m_fIMGCenterX;
        public float m_fIMGCenterY;
        public double m_fLensScaleX;
        public double m_fLensScaleY;
        public float m_fMatchWidth;
        public float m_fMatchHeight;
        public int m_iX;
        public int m_iY;

        public double dDescartesPosX
        {
            get { return m_fSensorXPos - m_fIMGCenterX; }
        }
        public double dReversetDescartesPosX
        {
            get { return m_fIMGCenterX - m_fSensorXPos; }
        }
        public double dDescartesPosY
        {
            get { return m_fSensorYPos - m_fIMGCenterY; }
        }
        public double dReversetDescartesPosY
        {
            get { return m_fIMGCenterY - m_fSensorYPos; }
        }

        public double dDescartesDistanceX
        {
            get { return dDescartesPosX * m_fLensScaleX; }
        }
        public double dDescartesDistanceY
        {
            get { return dDescartesPosY * m_fLensScaleY; }
        }
        //ks2
        //{{
        public eChipStatus m_eStatus;
        //}}	 

        public MatchResult()
        {
            Initialize();
        }
        public void Initialize()
        {
            Clear();
        }
        public void Clear()
        {
            m_nMatchNumber = 0;
            m_nMatchRoiNumber = 0;
            m_fPosX = 0;
            m_fPosY = 0;
            m_fScore = 0.0;
            m_fAngle = 0.0;
            m_eStatus = eChipStatus.CHIP_BLANK;

            m_fDispPosX = m_fDispPosY = 0.0f;
            m_fSensorXPos = 0.0f;
            m_fSensorYPos = 0.0f;
            m_fIMGCenterX = 0.0f;
            m_fIMGCenterY = 0.0f;
            m_fLensScaleX = 0.0f;
            m_fLensScaleY = 0.0f;
            m_fMatchWidth = 0;
            m_fMatchHeight = 0;
            m_iX = -1;
            m_iY = -1;
        }

        public object Clone()
        {
            MatchResult pRet = new MatchResult();

            pRet.m_nMatchNumber = this.m_nMatchNumber;
            pRet.m_nMatchRoiNumber = this.m_nMatchRoiNumber;
            pRet.m_fPosX = this.m_fPosX;
            pRet.m_fPosY = this.m_fPosY;
            pRet.m_fScore = this.m_fScore;
            pRet.m_fAngle = this.m_fAngle;
            pRet.m_eStatus = this.m_eStatus;
            pRet.m_fDispPosX = this.m_fDispPosX;
            pRet.m_fDispPosY = this.m_fDispPosY;
            pRet.m_fSensorXPos = this.m_fSensorXPos;
            pRet.m_fSensorYPos = this.m_fSensorYPos;

            pRet.m_fIMGCenterX = this.m_fIMGCenterX;
            pRet.m_fIMGCenterY = this.m_fIMGCenterY;
           
			pRet.m_fLensScaleX = this.m_fLensScaleX;
            pRet.m_fLensScaleY = this.m_fLensScaleY;


            pRet.m_fMatchWidth = this.m_fMatchWidth;
            pRet.m_fMatchHeight = this.m_fMatchHeight;


            pRet.m_fMatchWidth = this.m_fMatchWidth;
            pRet.m_fMatchHeight = this.m_fMatchHeight;

            pRet.m_iX = this.m_iX;
            pRet.m_iY = this.m_iY;       
            return pRet;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        ~MatchResult()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(false);
        }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    public class MatcherConfig : ICloneable, IDisposable
    {
        public double	m_fMinScale;
        public double	m_fMaxScale;
        public double	m_fScore;
        public double	m_fAngle;
        public double	m_fMaxPosition;
        public int		m_iCorrelationMode;
        public int		m_iMatchContrastMode;

		public MatcherConfig()
		{
			Initialize();
		}

        public void Initialize()
        {
            m_fMinScale			= 0.0;
            m_fMaxScale			= 0.0;
            m_fScore			= 0.0;
            m_fAngle			= 0.0;
            m_fMaxPosition		= 0.0;
            m_iCorrelationMode	= 0;
            m_iMatchContrastMode= 0;
        }

		public void Clear()
		{
			m_fMinScale			= 0.0;
			m_fMaxScale			= 0.0;
			m_fScore			= 0.0;
			m_fAngle			= 0.0;
			m_fMaxPosition		= 0.0;
			m_iCorrelationMode	= 0;
			m_iMatchContrastMode= 0;
		}

		public object Clone()
		{
			MatcherConfig pRet = new MatcherConfig();
			pRet.m_fMinScale			= this.m_fMinScale			;			
			pRet.m_fMaxScale			= this.m_fMaxScale			;
			pRet.m_fScore				= this.m_fScore				;
			pRet.m_fAngle				= this.m_fAngle				;
			pRet.m_fMaxPosition			= this.m_fMaxPosition		;
			pRet.m_iCorrelationMode		= this.m_iCorrelationMode	;
			pRet.m_iMatchContrastMode	= this.m_iMatchContrastMode	;	
			return pRet;
		}

		#region IDisposable Support
		private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.

				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		~MatcherConfig() {
		  // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
		  Dispose(false);
		}

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		public void Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
    public class FindConfig : ICloneable, IDisposable
    {
        /// <summary>
        /// Scle Bias
        /// </summary>
        public double m_fScaleBias;
        /// <summary>
        /// Scale Tolerance
        /// </summary>
        public double m_fScaleTolerance;

        public double m_fScore;
        /// <summary>
        /// Angle Bias
        /// </summary>
        public double m_fAngleBias;
        /// <summary>
        /// Angle Tolerance
        /// </summary>
        public double m_fAngleTolerance;

        /// <summary>
        /// Max Instance ( Max Search Count)
        /// </summary>
        public uint m_uiMaxPosition;
        /// <summary>
        /// ContrastMode               
        /// <para>public enum EFindContrastMode</para>
        /// <para>{</para>
        /// <para>   Normal = 0,</para>
        /// <para>   Inverse = 1,</para>
        /// <para>   Any = 2,</para>
        /// <para>   PointByPointNormal = 3,</para>
        /// <para>   PointByPointInverse = 4,</para>
        /// <para>   PointByPointAny = 5,</para>
        /// <para>   reserved0 = 5,</para>
        /// <para>   Unknown = int.MaxValue</para>
        /// <para>}</para>
        /// </summary> 
        public int m_iContrastMode;

        public FindConfig()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_fScaleBias = 1.0;
            m_fScaleTolerance = 0.0;
            m_fScore = 0.0;
            m_fAngleBias = 0.0;
            m_uiMaxPosition = 1;
            m_iContrastMode = 0;
        }

        public void Clear()
        {
            m_fScaleBias = 1.0;
            m_fScaleTolerance = 0.0;
            m_fScore = 0.0;
            m_fAngleBias = 0.0;
            m_uiMaxPosition = 1;
            m_iContrastMode = 0;
        }

        public object Clone()
        {
            FindConfig pRet = new FindConfig();
            pRet.m_fScaleBias = this.m_fScaleBias;
            pRet.m_fScaleTolerance = this.m_fScaleTolerance;
            pRet.m_fScore = this.m_fScore;
            pRet.m_fAngleBias = this.m_fAngleBias;
            pRet.m_fAngleTolerance = this.m_fAngleTolerance;
            pRet.m_uiMaxPosition = this.m_uiMaxPosition;
            pRet.m_iContrastMode = this.m_iContrastMode;
            return pRet;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        ~FindConfig()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(false);
        }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    public class FindResult : ICloneable, IDisposable
    {
        public int m_nFindNumber;
        public int m_nFindRoiNumber;
        public double m_fPosX;
        public double m_fPosY;
        public double m_fScore;
        public double m_fAngle;
        public float m_fDispPosX;
        public float m_fDispPosY;
        //add by smpark 
        public float m_fSensorXPos;
        public float m_fSensorYPos;
        public float m_fIMGCenterX;
        public float m_fIMGCenterY;
        public double m_fLensScaleX;
        public double m_fLensScaleY;


        public double dDescartesPosX
        {
            get { return m_fSensorXPos - m_fIMGCenterX; }
        }
        public double dReversetDescartesPosX
        {
            get { return m_fIMGCenterX - m_fSensorXPos; }
        }
        public double dDescartesPosY
        {
            get { return m_fSensorYPos - m_fIMGCenterY; }
        }
        public double dReversetDescartesPosY
        {
            get { return m_fIMGCenterY - m_fSensorYPos; }
        }

        public double dDescartesDistanceX
        {
            get { return dDescartesPosX * m_fLensScaleX; }
        }
        public double dDescartesDistanceY
        {
            get { return dDescartesPosY * m_fLensScaleY; }
        }


        public FindResult()
        {
            Initialize();
        }
        public void Initialize()
        {
            Clear();
        }
        public void Clear()
        {
            m_nFindNumber = 0;
            m_nFindRoiNumber = 0;
            m_fPosX = 0;
            m_fPosY = 0;
            m_fScore = 0.0;
            m_fAngle = 0.0;

            m_fDispPosX = m_fDispPosY = 0.0f;
            m_fSensorXPos = 0.0f;
            m_fSensorYPos = 0.0f;
            m_fIMGCenterX = 0.0f;
            m_fIMGCenterY = 0.0f;
            m_fLensScaleX = 0.0f;
            m_fLensScaleY = 0.0f;

        }

        public object Clone()
        {
            FindResult pRet = new FindResult();
            pRet.m_nFindNumber = this.m_nFindNumber;
            pRet.m_nFindRoiNumber = this.m_nFindRoiNumber;
            pRet.m_fPosX = this.m_fPosX;
            pRet.m_fPosY = this.m_fPosY;
            pRet.m_fScore = this.m_fScore;
            pRet.m_fAngle = this.m_fAngle;

            pRet.m_fDispPosX = this.m_fDispPosX;

            return pRet;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        ~FindResult()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(false);
        }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    public class BlobConfig : ICloneable, IDisposable
    {
        public int		m_nPadBlobMethod; // single , double

        public uint		m_nPadGrayMinVal; //if double , min, max
        public uint		m_nPadGrayMaxVal;

        public float	m_fPadHeightMin; //
        public float	m_fPadHeightMax;

        public float	m_fPadWidthMin; //
        public float	m_fPadWidthMax;

        public float	m_fPadAreaMin;
        public float	m_fPadAreaMax;

		public BlobConfig()
		{
			Initialize();
		}
        public void Clear()
        {
            m_nPadBlobMethod	= 0;

            m_nPadGrayMinVal	= 120;
            m_nPadGrayMaxVal	= 255;

            m_fPadHeightMin		= 0.01f;
            m_fPadHeightMax		= 1.0f;

            m_fPadWidthMin		= 0.01f;
            m_fPadWidthMax		= 1.0f;

            m_fPadAreaMin		= 0.01f;
            m_fPadAreaMax		= 1.0f;
        }

        public void Initialize()
        {
			Clear();
        }

		public object Clone()
		{
			BlobConfig pRet = new BlobConfig();
			pRet.m_nPadBlobMethod	= this.m_nPadBlobMethod	;	
			pRet.m_nPadGrayMinVal	= this.m_nPadGrayMinVal	;
			pRet.m_nPadGrayMaxVal	= this.m_nPadGrayMaxVal	;
			pRet.m_fPadHeightMin	= this.m_fPadHeightMin	;
			pRet.m_fPadHeightMax	= this.m_fPadHeightMax	;
			pRet.m_fPadWidthMin		= this.m_fPadWidthMin	;
			pRet.m_fPadWidthMax		= this.m_fPadWidthMax	;
			pRet.m_fPadAreaMin		= this.m_fPadAreaMin	;	
			pRet.m_fPadAreaMax		= this.m_fPadAreaMax	;	
			return pRet;
		}

		#region IDisposable Support
		private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.

				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		~BlobConfig() 
		{
		  // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(false);
		}

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		public void Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
	public class BlobResult : ICloneable, IDisposable
	{
		public bool bResultOK;
		public float fPosX;
		public float fPosY;
		public float fWidth;
		public float fHeight;
		public float fDispPosX;     // Unit Pixel
		public float fDispPosY;     // Unit Pixel 
		public float fDispWidth;     // Unit Pixel
		public float fDispHeight;     // Unit Pixel 
		public uint	 nArea;          // Unit mm	
		public uint  nObjectIndex;
		public uint  nLayerIndex;
		public Color BlobColor;
		//add by smpark 
		public float  fSensorXPos;
		public float  fSensorYPos;
		public float  fIMGCenterX;
		public float  fIMGCenterY;
		public double fLensScaleX;
		public double fLensScaleY;
		
		public double dDescartesPosX
		{
				get { return fSensorXPos - fIMGCenterX; }
		}
		public double dDescartesPosY
		{
				get { return fIMGCenterY - fSensorYPos; }
		}
		public double dDescartesDistanceX
		{
				get { return dDescartesPosX * fLensScaleX; }
		}
		public double dDescartesDistanceY
		{
				get { return dDescartesPosY * fLensScaleY; }
		}
		//add by smpark 
		public BlobResult()
		{
			Initialize();
		}
		public void Initialize()
		{
			bResultOK = false;
			fPosX = fPosX = fWidth = fHeight = 0.0f;
			fDispPosX = fDispPosY = 0.0f;
			nArea = 0;
			fDispHeight = 0;
			fDispWidth = 0;
			nObjectIndex = 0;
			nLayerIndex = 0;
		}
		public void clear()
		{
			bResultOK = false;
			fPosX = fPosX = fWidth = fHeight = 0.0f;
			fDispPosX = fDispPosY = 0.0f;
			nArea = 0;
			fDispHeight = 0;
			fDispWidth = 0;
			nObjectIndex = 0;
			nLayerIndex = 0;
			BlobColor = Color.Blue;
		}


		public object Clone()
		{
			BlobResult pRet = new BlobResult();
			pRet.bResultOK		 = this.bResultOK	;	
			pRet.fPosX				 = this.fPosX		;	
			pRet.fPosY				 = this.fPosY		;	
			pRet.fWidth				 = this.fWidth		;	
			pRet.fHeight			 = this.fHeight		;
			pRet.fDispPosX     = this.fDispPosX    ; 	
			pRet.fDispPosY     = this.fDispPosY    ; 	
			pRet.fDispWidth    = this.fDispWidth   ; 	
			pRet.fDispHeight   = this.fDispHeight  ; 	
			pRet.nArea         = this.nArea        ; 		   
			pRet.nObjectIndex	 = this.nObjectIndex	;
			pRet.nLayerIndex	 = this.nLayerIndex	;
			pRet.BlobColor		 = this.BlobColor	;
			pRet.fSensorXPos   = this.fSensorXPos;
			pRet.fSensorYPos   = this.fSensorYPos;
			pRet.fIMGCenterX   = this.fIMGCenterX;
			pRet.fIMGCenterY   = this.fIMGCenterY;
			pRet.fLensScaleX   = this.fLensScaleX;
			pRet.fLensScaleY   = this.fLensScaleY;
			return pRet;	
		}

		#region IDisposable Support
		private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.

				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		~BlobResult() {
		  // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
		  Dispose(false);
		}

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		public void Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
	public class FilterConfig : ICloneable, IDisposable
    {
        public uint m_nThresHoldValue;
        public uint m_nOpenDiskValue;
        public uint m_nCloseDiskValue;
        public uint m_nErodeDiskValue;
        public uint m_nDilateDiskValue;

		public FilterConfig()
		{
			Initialize();
		}
        public void Initialize()
        {
			Clear();
        }

		public void Clear()
		{
			m_nThresHoldValue	= 200;
			m_nOpenDiskValue	= 5;
			m_nCloseDiskValue	= 5;
			m_nErodeDiskValue	= 5;
			m_nDilateDiskValue	= 15;
		}

		public object Clone()
		{
			FilterConfig pRet = new FilterConfig();

			pRet.m_nThresHoldValue	= this.m_nThresHoldValue;
			pRet.m_nOpenDiskValue	= this.m_nOpenDiskValue;
			pRet.m_nCloseDiskValue	= this.m_nCloseDiskValue;
			pRet.m_nErodeDiskValue	= this.m_nErodeDiskValue;
			pRet.m_nDilateDiskValue	= this.m_nDilateDiskValue;

			return pRet;
		}

		#region IDisposable Support
		private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.

				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		~FilterConfig() 
		{
		  // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(false);
		}

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		public void Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			GC.SuppressFinalize(this);
		}
		#endregion
	}

	public class MatrixCodeConfig : ICloneable, IDisposable
	{
		public bool m_bcomputeGrading;
		public int	m_nMaxNumCodes;
		public int	m_nTimeout;
		/// <summary>
		/// 0 : Speed, 1 : Quality
		/// </summary>
		public int m_nReadMode;
		public MatrixCodeConfig()
		{
		}

		public void Initialize()
		{
			Clear();
		}

		public void Clear()
		{
			m_bcomputeGrading = true;
			m_nMaxNumCodes	  = 100;
			m_nTimeout		  = 3000;
		}

		public object Clone()
		{
			MatrixCodeConfig pRet	= new MatrixCodeConfig();
			pRet.m_bcomputeGrading	= this.m_bcomputeGrading;
			pRet.m_nMaxNumCodes		= this.m_nMaxNumCodes;
			pRet.m_nTimeout			= this.m_nTimeout;
			pRet.m_nReadMode		= this.m_nReadMode;
			return pRet;
		}

		#region IDisposable Support
		private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.

				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		~MatrixCodeConfig() 
		{
		  // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(false);
		}

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		public void Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			GC.SuppressFinalize(this);
		}
		#endregion
	}

	public class MatrixCodeResult :  IDisposable
	{
		public bool  m_bFound=false;
		public string m_strDecodedString;
		public int  m_iLogicalSizeRow;
		public int  m_iLogicalSizeCol;
		public int  m_iErrorsCorrected;
		public string m_strFamily;
		public bool m_bGS1Encoded;
		public bool m_bFlipping;
		public List<PointF> m_pCornerPoint;
		public MatrixCodeResult()
		{
				m_pCornerPoint=new List<PointF>();
				Clear();
		}
		~MatrixCodeResult()
		{
			Dispose();
		}

		public MatrixCodeResult Clone()
		{
			MatrixCodeResult pRet			= new MatrixCodeResult();
			pRet.m_bFound							= this.m_bFound;
			pRet.m_strDecodedString		= this.m_strDecodedString;
			pRet.m_iLogicalSizeRow		= this.m_iLogicalSizeRow;
			pRet.m_iLogicalSizeCol		= this.m_iLogicalSizeCol;
			pRet.m_iErrorsCorrected		= this.m_iErrorsCorrected;
			pRet.m_strFamily					= this.m_strFamily;
			pRet.m_bGS1Encoded			  = this.m_bGS1Encoded;
			pRet.m_bFlipping					= this.m_bFlipping;
			pRet.m_pCornerPoint				= this.m_pCornerPoint.ConvertAll(o=>o);
			return pRet;
		}
		public void Clear()
		{
				m_strDecodedString = "";
				m_iLogicalSizeRow = 0;
				m_iLogicalSizeCol = 0;
				m_iErrorsCorrected = 0;
				m_strFamily = "";
				m_bGS1Encoded = false;
				m_bFlipping=false;
				m_bFound=false;
		   if(m_pCornerPoint!=null)
				m_pCornerPoint.Clear();
				
		}

		#region IDisposable Support
		private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.

				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		// ~MatrixCodeResult() {
		//   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
		//   Dispose(false);
		// }

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		public void Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
    public enum eLibOption
    {
        MATCH = 0,
        MATCH_RESULT = 1,
        BLOB,
        BLOB_RESULT,
		MATRIX_CODE,
		MATRIX_CODE_RESULT,
        FIND,
        FIND_RESULT,
        CROSS,
        ROI,
        INSPECT_INSIDE,
        INSPECT_OUTSIDE,
        INSPECT_LOOP,
        ENABLE_FILTERS,
		ENABLE_THRESHOLD,
		ENABLE_ERODE_DISK,
		ENABLE_DILATE_DISK,
		ENABLE_OPEN_DISK,
		ENABLE_CLOSE_DISK,
		ENABLE_CALIBRATION,
		CALIBRATION,
		UNWRAPPING,
        CAMERA_INFO,
        MEASURE
    }
    public enum eOption
    {
        CrossLineVisible = 0 
        ,RoiBoxVisible
        ,FiltersVisible

        ,DisplayMatchResult
        ,DisplayBlobResult
        ,DisplayROIs
    }
    public enum eRoiItems
    {
        None = -1
        , ROI_No0
        , ROI_No1
        , ROI_No2
        , ROI_No3
        , ROI_No4
        , ROI_No5
        , ROI_No6
        , ROI_No7
        , ROI_No8
        , ROI_No9
        , Max
    }
		//add by smpark

    public enum eGoldenImages
    {
        None = -1
        , Fiducial_No1
				, Fiducial_No2
        , Fiducial_No3
        , Fiducial_No4
        , Object_No1
        , Object_No2
        , Object_No3
        , Object_No4
        , Max
    }

    public struct stBlobDisplay
    {
        public string strVisionName;
        public bool bOK;
        public int nQuantity;

        public void Init()
        {
            Clear();
        }

        public void Clear()
        {
            strVisionName = "";
            bOK = false;
            nQuantity = 0;
		}
            
    }

	public struct stCamInfo
	{
		//Camera Information

		public string	strCameraName;
		public string	strCamFile;
		public string	strSerialNumber;

		public int		iDriverIndex;
		public string   strBoardTopology;
		public string	strConnectorType;
		public string   strColorFormat;

		public string	strFlipX;
		public string	strFlipY;

		//trigger setup

		public string	strAcquisitionMode;
		public string   strTrigMode;
		public string   strNextTrigMode;
		public int		iSeqLength_Fr;

		public string   strTrigLine;
		public string   strTrigEdge;
		public string   strTrigFilter;
		public string   strTrigCtl;

		//Status
		public bool		bLive;
		public bool		bIdle;
		public bool		bGrabbing;
		public bool		bGrabbed;

		//GetParam Items
		public float	fGetExposeTime;
		public float	fGetGain;
		public float	fGetFrameRate;
		public int		nGrayVal;
		public int		nSizeX;
		public int		nSizeY;
		//SetParam Items
		public float	fSetExposeTime;
		public float	fSetGain;
		public float	fSetFrameRate;

		public void Init()
		{
			Clear();
		}
		public void Clear()
		{
			iDriverIndex		= 0			;
			strBoardTopology	= "MONO"	;
			strConnectorType	= "A"		;
			strColorFormat		= "Y8"		;

			strCamFile			= ""		;
			strCameraName		= ""		;
			strSerialNumber		= ""		;

			strFlipX			= "OFF"		;
			strFlipY			= "OFF"		;

			nSizeX				= 0			;
			nSizeY				= 0			;

			strAcquisitionMode	= "SNAPSHOT";
			strTrigMode			= "COMBINED";
			strNextTrigMode		= "COMBINED";
			iSeqLength_Fr		= -1		;//MC.INDETERMINATE;

			strTrigLine			= "IIN1"	;
			strTrigEdge			= "GOHIGH"	;
			strTrigFilter		= "ON"		;
			strTrigCtl			= "ISO"		;

			bGrabbing			= false		;
			bGrabbed			= false		;
			bLive				= false		;
			bIdle				= false		;


			fGetExposeTime		= 0.0f		;
			fGetGain			= 0.0f		;
			fGetFrameRate		= 0.0f		;
			nGrayVal			= 0			;

			fSetExposeTime		= 0.0f		;
			fSetGain			= 0.0f		;
			fSetFrameRate		= 0.0f		;
		}
		
	}

	public struct stLibInfo
	{
		public string	strName;
		public int		iDriverIndex;
		public bool		bColor;
		public float	fCenterCoordinateOfX;
		public float	fCenterCoordinateOfY;
		public double	dLensScaleX;
		public double	dLensScaleY;
		public float	fImageW;
		public float	fImageH;
		

		public void Initialize()
		{
			Clear();
		}

		public void Clear()
		{
			strName					= "";
			iDriverIndex			= -1;
			bColor					= false;
			fCenterCoordinateOfX	= 0.0f;
			fCenterCoordinateOfY	= 0.0f;
			dLensScaleX				= 0.0;
			dLensScaleY				= 0.0;
			fImageW					= 0.0f;
			fImageH					= 0.0f;
		}

	}
	public class LibInfo
	{
		#region [ LIB ]
		public stLibInfo m_stLibInfo;

		public ConcurrentDictionary<int, Rectangle> m_dicRoiSize;
		public ConcurrentDictionary<EzInaVision.GDV.eLibOption, bool> m_dicLibOption;
		#endregion[ LIB ]
		#region [ MATCHER ]
		public int		m_iMatcherIndex;
		public int		m_iRoiOfFiducial;
		public int		m_iRoiOfInspection;
		public double[]  m_dFiducialsPos1stX;
		public double[]  m_dFiducialsPos1stY;
		public double[]  m_dFiducialsPos2ndX;
		public double[]  m_dFiducialsPos2ndY;
		public double[]  m_dAngleOfFiducials;

		public ConcurrentDictionary<int, EzInaVision.GDV.MatchResult> m_dicMatchResult;
		public ConcurrentDictionary<int, EzInaVision.GDV.MatchResult> m_dicMatchResultForCalculation;

		public List<List<ConcurrentDictionary<int, EzInaVision.GDV.MatchResult>>> m_vecMatchResults;
		public List<List<ConcurrentDictionary<int, EzInaVision.GDV.MatchResult>>> m_vecMatchResultsForCalculation;
		
		
		public EzInaVision.GDV.MatcherConfig m_MatchConfig;

		public List<List<EzInaVision.GDV.stClosePointToCenter>> m_vecClosePointToCenter;
		public EzInaVision.GDV.stClosePointToCenter m_stClosePointToCenter;
        #endregion[ MATCHER ]

        #region [ FIND ]

        public ConcurrentDictionary<int, EzInaVision.GDV.FindResult> m_dicFindResult;
        public ConcurrentDictionary<int, EzInaVision.GDV.FindResult> m_dicFindResultForCalculation;

        public List<List<ConcurrentDictionary<int, EzInaVision.GDV.FindResult>>> m_vecFindResults;
        public List<List<ConcurrentDictionary<int, EzInaVision.GDV.FindResult>>> m_vecFindResultsForCalculation;
        public FindConfig m_FindConig;
        #endregion [ FIND ]

        #region [ FILTERS ]
        public EzInaVision.GDV.FilterConfig m_Filterconfig;
		#endregion[ FILTERS ]
		#region [ BLOB ]
		public uint m_nEObjectSelectionTotalCount;
		public EzInaVision.GDV.BlobConfig m_BlobParam;

		public ConcurrentDictionary<int, EzInaVision.GDV.BlobResult> m_dicBlobResult;
		public ConcurrentDictionary<int, EzInaVision.GDV.BlobResult> m_dicBlobResultForCalculation;

		public List<ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>> m_vecBlobResults;
		public List<ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>> m_vecBlobResultsForCalculation;

		public int m_iCodedImage2Index;
		#endregion [ BLOB ]

		#region [ MatrixCodeRead ]
		public EzInaVision.GDV.MatrixCodeConfig m_MatrixCodeConfig;

		public ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult> m_dicMatrixCodeResult;
		public ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult> m_dicMatrixCodeResultForCalculation;

		public List<ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult>> m_vecMatrixCodeResults;
		public List<ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult>> m_vecMatrixCodeResultsForCalculation;
		#endregion[ MatrixCodeRead ]

		public LibInfo(EzInaVision.GDV.stLibInfo a_stLibInfo)
		{
			Initialize();
			a_stLibInfo.strName = a_stLibInfo.strName.ToUpper();
			m_stLibInfo = a_stLibInfo;
		}

		~LibInfo()
		{
			Terminate();
		}
		public void Initialize()
		{
			#region [ LIB ]
			m_stLibInfo = new EzInaVision.GDV.stLibInfo();
			m_stLibInfo.Initialize();

			m_dicRoiSize = new ConcurrentDictionary<int, Rectangle>();
			//For Options
			m_dicLibOption = new ConcurrentDictionary<GDV.eLibOption, bool>();
			#endregion [ LIB ]
			#region [ MATCHER ]
			m_iMatcherIndex		= 0;
			m_iRoiOfFiducial	= 0;
			m_iRoiOfInspection	= 0;

			m_dFiducialsPos1stX = new double[(int)EzInaVision.GDV.eRoiItems.Max];
			m_dFiducialsPos1stY = new double[(int)EzInaVision.GDV.eRoiItems.Max];
			m_dFiducialsPos2ndX = new double[(int)EzInaVision.GDV.eRoiItems.Max];
			m_dFiducialsPos2ndY = new double[(int)EzInaVision.GDV.eRoiItems.Max];
			m_dAngleOfFiducials = new double[(int)EzInaVision.GDV.eRoiItems.Max];

			m_dicMatchResult				 = new ConcurrentDictionary<int, MatchResult>();
			m_dicMatchResultForCalculation	 = new ConcurrentDictionary<int, MatchResult>();

			m_vecMatchResults = new List<List<ConcurrentDictionary<int, GDV.MatchResult>>>();
			//Save matching results of each ROIs for data alignment.
			m_vecMatchResultsForCalculation = new List<List<ConcurrentDictionary<int, GDV.MatchResult>>>();

			for (int i = 0; i < (int)GDV.eRoiItems.Max; i++)
			{
				m_vecMatchResults.Add(new List<ConcurrentDictionary<int, GDV.MatchResult>>());
				m_vecMatchResultsForCalculation.Add(new List<ConcurrentDictionary<int, GDV.MatchResult>>());
				for (int j = 0; j < (int)GDV.eGoldenImages.Max; j++)
				{
					m_vecMatchResults[i].Add(new ConcurrentDictionary<int, GDV.MatchResult>());
					m_vecMatchResultsForCalculation[i].Add(new ConcurrentDictionary<int, GDV.MatchResult>());

					m_vecMatchResults[i][j] = new ConcurrentDictionary<int, GDV.MatchResult>();
					m_vecMatchResultsForCalculation[i][j] = new ConcurrentDictionary<int, GDV.MatchResult>();
				}
			}



			//figure out the center that grabbed from a camera
			m_vecClosePointToCenter = new List<List<GDV.stClosePointToCenter>>();
			for (int i = 0; i < (int)GDV.eRoiItems.Max; i++)
			{
				m_vecClosePointToCenter.Add(new List<GDV.stClosePointToCenter>());
				for (int j = 0; j < (int)GDV.eGoldenImages.Max; j++)
				{
					m_vecClosePointToCenter[i].Add(new GDV.stClosePointToCenter());
					m_vecClosePointToCenter[i][j].Initialize();
				}
			}
			m_stClosePointToCenter = new GDV.stClosePointToCenter();
			m_stClosePointToCenter.Initialize();

			//this is configure of matching function.
			m_MatchConfig = new GDV.MatcherConfig();
			m_MatchConfig.Initialize();
			#endregion [ MATCHER ]
			#region [ FILTERS ]
			m_Filterconfig = new EzInaVision.GDV.FilterConfig();
			m_Filterconfig.Initialize();
			#endregion [ FILTERS ]
			#region [ BLOB ]
			m_nEObjectSelectionTotalCount = 0;

			m_BlobParam = new EzInaVision.GDV.BlobConfig();
			m_BlobParam.Initialize();

			//Save blob result of each ROIs
			m_vecBlobResults = new List<ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>>();

			//Save blob results of each ROI for data aligment;
			m_vecBlobResultsForCalculation = new List<ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>>();

			for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
			{
				m_vecBlobResults.Add(new ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>());
				m_vecBlobResultsForCalculation.Add(new ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>());
			}

			m_dicBlobResult = new ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>();
			m_dicBlobResultForCalculation = new ConcurrentDictionary<int, EzInaVision.GDV.BlobResult>();

			m_iCodedImage2Index = 0;
			#endregion[ BLOB ]

			#region [MatrixCodeRead]
			m_MatrixCodeConfig = new EzInaVision.GDV.MatrixCodeConfig();
			m_MatrixCodeConfig.Initialize();

			//Save blob result of each ROIs
			m_dicMatrixCodeResult = new ConcurrentDictionary<int, MatrixCodeResult>();
			m_dicMatrixCodeResultForCalculation = new ConcurrentDictionary<int, MatrixCodeResult>();

			m_vecMatrixCodeResults = new List<ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult>>();
			m_vecMatrixCodeResultsForCalculation = new List<ConcurrentDictionary<int, MatrixCodeResult>>();

			for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
			{
				m_vecMatrixCodeResults.Add(new ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult>());
				m_vecMatrixCodeResultsForCalculation.Add(new ConcurrentDictionary<int, EzInaVision.GDV.MatrixCodeResult>());
			}
			#endregion[MatrixCodeRead]

		}
		public void Clear()
		{
			#region [ LIB ]
			m_stLibInfo.Clear();

			//ROIs
			for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
			{
				m_dicRoiSize[i] = new Rectangle(0,0,0,0);
			}
			#endregion [ LIB ]
			#region [ MATCHER ]
			m_iMatcherIndex	= 0;
			m_iRoiOfFiducial	= 0;
			m_iRoiOfInspection= 0;
			for(int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
			{
				m_dFiducialsPos1stX[i] = 0.0;
				m_dFiducialsPos1stY[i] = 0.0;
				m_dFiducialsPos2ndX[i] = 0.0;
				m_dFiducialsPos2ndY[i] = 0.0;
				m_dAngleOfFiducials[i] = 0.0;

			}

			m_dicMatchResult.Clear();
			m_dicMatchResultForCalculation.Clear();

			for (int i = 0; i < (int)GDV.eRoiItems.Max; i++)
			{
				for (int j = 0; j < (int)GDV.eGoldenImages.Max; j++)
				{
					m_vecMatchResults[i][j].Clear();
					m_vecMatchResultsForCalculation[i][j].Clear();
				}
			}

			//figure out the center that grabbed from a camera
			for (int i = 0; i < (int)GDV.eRoiItems.Max; i++)
			{
				for (int j = 0; j < (int)GDV.eGoldenImages.Max; j++)
				{
					m_vecClosePointToCenter[i][j].Clear();
				}
			}
			m_stClosePointToCenter.Clear();

			//this is configure of matching function.
			m_MatchConfig.Clear();
			#endregion [ MATCHER ]
			#region [ FILTERS ]
			m_Filterconfig.Clear();
			#endregion [ FILTERS ]
			#region [ BLOB ]
			m_nEObjectSelectionTotalCount = 0;

			m_BlobParam.Clear();

			for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
			{
				m_vecBlobResults[i].Clear();
				m_vecBlobResultsForCalculation[i].Clear();
			}

			m_dicBlobResult.Clear();
			m_dicBlobResultForCalculation.Clear();

			#endregion[ BLOB ]	

			#region [ MatrixCodeRead ]
			m_MatrixCodeConfig.Clear();

			for(int i = 0; i < (int)eRoiItems.Max; i++)
			{	
				m_vecMatrixCodeResults[i].Clear();
				m_vecMatrixCodeResultsForCalculation[i].Clear();
			}

			m_dicMatrixCodeResult.Clear();
			m_dicMatrixCodeResultForCalculation.Clear();

			#endregion [ MatrixCodeRead ]
		}

		public void Terminate()
		{
			#region [ LIB ]
			if (m_dicRoiSize != null)
			{
				m_dicRoiSize.Clear();
				m_dicRoiSize = null;
			};

			if(m_dicLibOption != null)
				m_dicLibOption.Clear();

			#endregion [ LIB ]
			#region [ MATCHER ]
			if (m_dFiducialsPos1stX	.Length > 0) { Array.Clear(m_dFiducialsPos1stX, 0, m_dFiducialsPos1stX.Length); };
			if (m_dFiducialsPos1stY	.Length > 0) { Array.Clear(m_dFiducialsPos1stY, 0, m_dFiducialsPos1stY.Length); };
			if (m_dFiducialsPos2ndX	.Length > 0) { Array.Clear(m_dFiducialsPos2ndX, 0, m_dFiducialsPos2ndX.Length); };
			if (m_dFiducialsPos2ndY	.Length > 0) { Array.Clear(m_dFiducialsPos2ndY, 0, m_dFiducialsPos2ndY.Length); };
			if (m_dAngleOfFiducials	.Length > 0) { Array.Clear(m_dAngleOfFiducials, 0, m_dAngleOfFiducials.Length); };


			m_dicMatchResult.Clear();
			m_dicMatchResultForCalculation.Clear();

			for (int i = 0; i < (int)GDV.eRoiItems.Max; i++)
			{
				for (int j = 0; j < (int)GDV.eGoldenImages.Max; j++)
				{
					m_vecMatchResults[i][j].Clear();
					m_vecMatchResultsForCalculation[i][j].Clear();
				}
			}

			//figure out the center that grabbed from a camera
			for (int i = 0; i < (int)GDV.eRoiItems.Max; i++)
			{
				for (int j = 0; j < (int)GDV.eGoldenImages.Max; j++)
				{
					m_vecClosePointToCenter[i][j].Clear();
				}
			}
			m_stClosePointToCenter.Clear();

			//this is configure of matching function.
			m_MatchConfig.Clear();
			#endregion [ MATCHER ]
			#region [ FILTERS ]
			m_Filterconfig.Clear();
			#endregion [ FILTERS ]
			#region [ BLOB ]

			m_BlobParam.Clear();

			for (int i = 0; i < (int)EzInaVision.GDV.eRoiItems.Max; i++)
			{
				m_vecBlobResults[i].Clear();
				m_vecBlobResultsForCalculation[i].Clear();
			}

			m_dicBlobResult.Clear();
			m_dicBlobResultForCalculation.Clear();

			#endregion[ BLOB ]	
		}
	}

	public enum eEuresysBlobThresHold : Int16 {  Single, Double };
	public enum eStatus : Int16 { STATUS_ERROR = -1, STATUS_OK = 1};
	/*trigger mode*/
	public enum eTriggerToggle : Int16 { TRIGGER_OFF = 0, TRIGGER_ON = 1};
	/*trigger source*/
	public enum eTriggerMode : Int16 { SOFTWAREMODE = 7, HARDWAREMODE = 0};
}
