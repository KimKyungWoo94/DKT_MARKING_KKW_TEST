using System;
using System.Collections.ObjectModel;
using System.Drawing;
namespace EzIna.DataMatrix
{
    public class DMItem
    {
        private PointF    m_CenterPoint;
        private SizeF     m_CenterOffset;
        private bool      m_bHatch;   
        private float     m_fWidth;
        private float     m_fHeight;
        public  DMItem(PointF a_CenterPos,bool a_bHatch)
        {
            m_CenterPoint=a_CenterPos;
            m_CenterOffset=new SizeF();
            m_bHatch=a_bHatch;
            m_fWidth=0.0f;
            m_fHeight=0.0f;            
        }
        public DMItem(bool a_bHatch)
        {
            m_CenterPoint = new PointF();
            m_CenterOffset = new SizeF();
            m_bHatch = a_bHatch;
            m_fWidth = 0.0f;
            m_fHeight = 0.0f;
        }
        public SizeF CenterOffset
        {
            get { return m_CenterOffset;}
            set { m_CenterOffset=value; }
        }
        public PointF OrginCenterPos
        {
            get { return m_CenterPoint;}
            set { m_CenterPoint=value; }
        }
        public float fWidth
        {
            get { return m_fWidth;}
            set { m_fWidth=value;}
        }
        public float fHeight
        {
            get { return m_fHeight; }
            set { m_fHeight = value; }
        }
        public PointF CenterPos
        {
            get { return PointF.Add(m_CenterPoint,m_CenterOffset); }            
        }

        public bool bHatch
        {
            get { return m_bHatch; }
            set { m_bHatch = value; }
        }


        public Collection<PointF> GetRectgleOuterLine()
        {
            Collection<PointF> pRet=new Collection<PointF>();
            float fHalfWidth=m_fWidth/2.0f;
            float fHalfHeight=m_fHeight/2.0f;
            pRet.Add(new PointF(CenterPos.X-fHalfWidth,CenterPos.Y+fHalfHeight));
            pRet.Add(new PointF(CenterPos.X+fHalfWidth,CenterPos.Y+fHalfHeight));
            pRet.Add(new PointF(CenterPos.X+fHalfWidth,CenterPos.Y-fHalfHeight));
            pRet.Add(new PointF(CenterPos.X-fHalfWidth,CenterPos.Y-fHalfHeight));
            return pRet;
        }

        public Collection<PointF> GetCircleOuterLine(float a_ResolutionAngle)
        {
            Collection<PointF> pRet = new Collection<PointF>();
            float fHalfWidth = m_fWidth / 2.0f;
            float fHalfHeight = m_fHeight / 2.0f;
            float fRadian=0.0f;
            float fX=0.0f;
            float fY=0.0f;
            for (float Angle=0; Angle<=360;Angle+=a_ResolutionAngle)
            {
                fRadian=(float)(Math.PI/180*Angle);
                fX=(float)(CenterPos.X+fHalfWidth*Math.Cos(fRadian));
                fY=(float)(CenterPos.X+fHalfWidth*Math.Sin(fRadian));
                pRet.Add(new PointF(fX,fY));
            }                 
            return pRet;
        }

        public PointF[] GetRectgleOuterLinePointF()
        {
            PointF[] pointFArray = new PointF[4];

            float fHalfWidth = (float)Math.Truncate(m_fWidth / 2.0f * 1000) / 1000;
            float fHalfHeight = (float)Math.Truncate(m_fHeight / 2.0f * 1000) / 1000;

            pointFArray[0] = new PointF(CenterPos.X - fHalfWidth, CenterPos.Y + fHalfHeight);
            pointFArray[1] = new PointF(CenterPos.X + fHalfWidth, CenterPos.Y + fHalfHeight);
            pointFArray[2] = new PointF(CenterPos.X + fHalfWidth, CenterPos.Y - fHalfHeight);
            pointFArray[3] = new PointF(CenterPos.X - fHalfWidth, CenterPos.Y - fHalfHeight);						
            return pointFArray;
        }

        public PointF[] GetCircleOuterLinePointF(float a_ResolutionAngle)
        {
            int Count = (int)(360.0f / a_ResolutionAngle) + 1;
            PointF[] pointFArray = new PointF[Count];

            float fHalfWidth = (float)Math.Truncate(m_fWidth / 2.0f * 1000) / 1000;
            float fHalfHeight = (float)Math.Truncate(m_fHeight / 2.0f * 1000) / 1000;
            float fRadian = 0.0f;
            float fX = 0.0f;
            float fY = 0.0f;

            int i = 0;
            for (float Angle = 0; Angle <= 360; Angle += a_ResolutionAngle, i++)
            {
                fRadian = (float)Math.Truncate(Math.PI / 180 * Angle * 1000) / 1000;
                fX = (float)Math.Truncate((CenterPos.X + fHalfWidth * Math.Cos(fRadian)) * 1000) / 1000;
                fY = (float)Math.Truncate((CenterPos.Y + fHalfHeight * Math.Sin(fRadian)) * 1000) / 1000;
                pointFArray[i] = new PointF(fX, fY);
            }
            return pointFArray;
        }

    }
}
