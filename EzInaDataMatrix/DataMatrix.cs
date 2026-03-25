using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZintNet;
using EzCAM_Ver2;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;

namespace EzIna.DataMatrix
{
    public sealed class DM
    {

        private List<DMItem[]> m_pCoordinateDataList;
        private Hatch_Option m_HatchOption;
        public Hatch_Option HatchOption
        {
            get
            {
                return m_HatchOption;
            }
            set
            {
                m_HatchOption = value;
            }
        }
        private bool m_bCreateCoordinate;
        public bool bCreateCoordinate
        {
            get { return m_bCreateCoordinate; }
        }

        private GraphicsPath m_CodeGraphicsPath;
        public GraphicsPath CodeGraphicsPath
        {
            get { return this.m_CodeGraphicsPath; }
        }

        private CellShape m_Shape;
        public CellShape Shape
        {
            get
            {
                return m_Shape;
            }
            set
            {
                m_Shape = value;
            }
        }
        public HATCH_TYPE Type { get { return this.HatchOption.Type; } set { this.m_HatchOption.Type = value; } }

        public bool Outline { get { return this.HatchOption.bOutline; } set { this.m_HatchOption.bOutline = value; } }

        public float Angle { get { return this.HatchOption.fAngle; } set { this.m_HatchOption.fAngle = value; } }

        public float Pitch { get { return this.HatchOption.fPitch; } set { this.m_HatchOption.fPitch = value; } }

        public float Offset { get { return this.HatchOption.fOffset; } set { this.m_HatchOption.fOffset = value; } }


        private float m_fDrawZoomMultiplier;
        public float fDrawZoomMultiplier
        {
            get { return m_fDrawZoomMultiplier; }
            set
            {
                if (value >= 1.0f)
                {
                    m_fDrawZoomMultiplier = value;
                }
            }
        }
        string m_DataMatrixString;
        public string DatamatrixText
        {
            get
            {
                return m_DataMatrixString;
            }
            set
            {
                m_DataMatrixString = value;
            }
        }
        private SizeF m_Size;
        private PointF m_CenterPoint;
        public float Width
        {
            get { return m_Size.Width; }
            set { m_Size.Width = value; }
        }
        public float Height
        {
            get { return m_Size.Height; }
            set { m_Size.Height = value; }
        }
        private ZeroPosition m_CenterPosDefalut;
        public ZeroPosition CenterPosDefalut
        {
            get { return m_CenterPosDefalut; }
            set { m_CenterPosDefalut = value; }
        }
        private Rotate m_RotateAngle;
        public Rotate RotateAngle
        {
            get { return m_RotateAngle; }
            set { m_RotateAngle = value; }
        }
        public DM(string a_str)
        {
            Initialize();
            m_DataMatrixString = a_str;

        }
        public DM(string a_str, Hatch_Option a_Option)
        {
            Initialize();
            m_DataMatrixString = a_str;
            m_HatchOption = a_Option;

        }
        private void Initialize()
        {
            m_pCoordinateDataList = new List<DMItem[]>();
            this.m_bCreateCoordinate = false;
            this.m_CodeGraphicsPath = new GraphicsPath();
            this.m_Size = new SizeF();
            this.m_CenterPoint = new PointF();
            this.m_Size.Width = 10f;
            this.m_Size.Height = 10f;
            this.m_fDrawZoomMultiplier = 1.0f;
            this.m_Shape = CellShape.Rectangle;
            this.HatchOption.Clear();
            this.m_CenterPosDefalut = ZeroPosition.Center;
            this.m_RotateAngle = Rotate.R_0;
            ClearVariable();
        }
        public void ClearVariable(bool a_IncludePointData = true)
        {
            if (a_IncludePointData == true)
                m_pCoordinateDataList.Clear();
            this.m_CodeGraphicsPath.Reset();
            this.m_bCreateCoordinate = false;
            this.m_Size.Width = 10.0f;
            this.m_Size.Height = 10.0f;
            this.m_CenterPoint.X = 0.0f;
            this.m_CenterPoint.Y = 0.0f;
        }
        public DM Clone()
        {
            DM pRet = new DM(this.m_DataMatrixString);
            pRet.m_pCoordinateDataList = new List<DMItem[]>(this.m_pCoordinateDataList);
            pRet.m_bCreateCoordinate = this.m_bCreateCoordinate;
            pRet.m_CodeGraphicsPath = (GraphicsPath)m_CodeGraphicsPath.Clone();

            pRet.m_Size.Width = this.m_Size.Width;
            pRet.m_Size.Height = this.m_Size.Height;

            pRet.m_CenterPoint.X = this.m_CenterPoint.X;
            pRet.m_CenterPoint.Y = this.m_CenterPoint.Y;
            pRet.m_Size.Width = this.m_Size.Width;
            pRet.m_Size.Height = this.m_Size.Height;
            pRet.m_fDrawZoomMultiplier = this.m_fDrawZoomMultiplier;
            pRet.m_Shape = this.m_Shape;
            pRet.HatchOption = this.HatchOption.Clone();
            pRet.m_CenterPosDefalut = this.m_CenterPosDefalut;
            pRet.m_RotateAngle = this.m_RotateAngle;


            return pRet;
        }

        private void AllocDMItemArray(int a_Row, int a_Col)
        {
            if (m_pCoordinateDataList.Count > 0)
            {
                ClearVariable();
            }
            for (int i = 0; i < a_Row; i++)
            {
                m_pCoordinateDataList.Add(new DMItem[a_Col]);
                for (int j = 0; j < a_Col; j++)
                {
                    m_pCoordinateDataList[i][j] = new DMItem(false);
                }
            }
        }
        public void CopyData(Collection<SymbolData> a_Data)
        {
            if (m_pCoordinateDataList.Count > 0)
                m_pCoordinateDataList.Clear();
            for (int Row = 0; Row < a_Data.Count; Row++)
            {
                m_pCoordinateDataList.Add(new DMItem[a_Data[Row].RowCount]);
                for (int Col = 0; Col < a_Data[Row].RowCount; Col++)
                {
                    m_pCoordinateDataList[Row][Col] = new DMItem(a_Data[Row].GetRowData()[Col] == 1);
                }
            }
        }
        public List<DMItem[]> CoordinateDataList
        {
            get { return m_pCoordinateDataList; }
        }





        #region Orgin

        public bool CreateCodrdinates(PointF a_CenterPos, SizeF a_Size, ZeroPosition a_CenterDefault = ZeroPosition.Center, Rotate a_Rotate = Rotate.R_0)
        {
            try
            {
                if (m_pCoordinateDataList.Count > 0 && m_pCoordinateDataList[0].Length > 0)
                {
                    ClearVariable(false);
                    this.m_CenterPosDefalut = a_CenterDefault;
                    this.m_RotateAngle = a_Rotate;
                    PointF startPos = new PointF();
                    switch (this.m_CenterPosDefalut)
                    {
                        case ZeroPosition.Center:
                            startPos.X = a_Size.Width / 2 * -1;
                            startPos.Y = a_Size.Height / 2 * -1;
                            break;
                        case ZeroPosition.LT:
                            startPos.X = 0;
                            startPos.Y = 0;
                            break;
                        case ZeroPosition.LB:
                            startPos.X = 0;
                            startPos.Y = -a_Size.Height;
                            break;
                    }
                    startPos.X += a_CenterPos.X;
                    startPos.Y += a_CenterPos.Y;
                    m_CenterPoint = a_CenterPos;

                    if (!this.InternalCreateCodrdinates(startPos, a_Size))
                        return false;

                    // hatch 여부
                    if (this.HatchOption.Type == HATCH_TYPE.HATCH_TYPE_NONE)
                    {
                        //Hatch hatch = new Hatch();
                        //GraphicsPath hatchPath = new GraphicsPath();
                        this.m_CodeGraphicsPath.AddPath(this.Get2dCodeGrapchicsPath(), false);
                        //hatch.CreateShapeHatch(this.Get2dCodeGrapchicsPath(), ref this.m_CodeGraphicsPath);
                    }
                    else
                    {
                        Hatch hatch = new Hatch();
                        hatch.Option = this.m_HatchOption;
                        GraphicsPath hatchPath = new GraphicsPath();
                        //hatch.CreateShapeHatch(this.Get2dCodeGrapchicsPath(), ref this.m_CodeGraphicsPath);
                        hatch.CreateShapeHatch(this.Get2dCodeGrapchicsPath_ADjoin(), ref this.m_CodeGraphicsPath);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch
            {

            }
            return false;
        }

        /* private bool InternalCreateCodrdinates(PointF a_StartPos, SizeF a_Size)
        {
                if (m_pCoordinateDataList == null)
                        return false;
                if (m_pCoordinateDataList.Count <= 0)
                        return false;
                if (m_pCoordinateDataList[0].Length <= 0)
                        return false;

                m_Size.Width = a_Size.Width;
                m_Size.Height = a_Size.Height;
                float fWidth = (float)Math.Truncate(m_Size.Width * 1000) / 1000;
                float fHeight = (float)Math.Truncate(m_Size.Height * 1000) / 1000;
                float fHalfWidth=(float)Math.Truncate(m_Size.Width/2.0f * 1000) / 1000;
                float fHalfHeight=(float)Math.Truncate(m_Size.Height/2.0f * 1000) / 1000;
                float fElementWidth = 0.0f;
                float fElementHeight = 0.0f;
                float fHalfElementWidth = 0.0f;
                float fHalfElementHeight = 0.0f;

                fElementHeight = (float)Math.Truncate(fHeight / (m_pCoordinateDataList.Count) * 1000) / 1000;
                fHalfElementHeight = (float)Math.Truncate(fElementHeight / 2 * 1000) / 1000;
                SizeF StartOffset = new SizeF(a_StartPos);
                // this.m_pCoordinateDataList.Clear();
                for (int i = 0; i < this.m_pCoordinateDataList.Count; i++)
                {
                        fElementWidth = (float)Math.Truncate(fWidth / this.m_pCoordinateDataList[i].Length * 1000) / 1000;
                        fHalfElementWidth = (float)Math.Truncate(fElementWidth / 2 * 1000) / 1000;
                        for (int j = 0; j < this.m_pCoordinateDataList[i].Length; j++)
                        {
                                if (i == 0 && j == 0)
                                {
                                        this.m_pCoordinateDataList[i][j].OrginCenterPos = new PointF(fHalfElementWidth, fHalfElementHeight);
                                        this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                                        this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                                        this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
                                }
                                else if (j == 0 && i > 0)
                                {
                                        this.m_pCoordinateDataList[i][j].OrginCenterPos = PointF.Add(this.m_pCoordinateDataList[i - 1][j].OrginCenterPos, new SizeF(0, fElementHeight));
                                        this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                                        this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                                        this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
                                }
                                else
                                {
                                        this.m_pCoordinateDataList[i][j].OrginCenterPos = PointF.Add(this.m_pCoordinateDataList[i][j - 1].OrginCenterPos, new SizeF(fElementWidth, 0));
                                        this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                                        this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                                        this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
                                }
                        }
                }
                this.m_bCreateCoordinate = true;
                return true;
         }*/
        private bool InternalCreateCodrdinates(PointF a_StartPos, SizeF a_Size)
        {
            if (m_pCoordinateDataList == null)
                return false;
            if (m_pCoordinateDataList.Count <= 0)
                return false;
            if (m_pCoordinateDataList[0].Length <= 0)
                return false;

            m_Size.Width = a_Size.Width;
            m_Size.Height = a_Size.Height;
            float fWidth = (float)Math.Truncate(m_Size.Width * 1000) / 1000;
            float fHeight = (float)Math.Truncate(m_Size.Height * 1000) / 1000;
            float fHalfWidth = (float)Math.Truncate(m_Size.Width / 2.0f * 1000) / 1000;
            float fHalfHeight = (float)Math.Truncate(m_Size.Height / 2.0f * 1000) / 1000;
            float fElementWidth = 0.0f;
            float fElementHeight = 0.0f;
            float fHalfElementWidth = 0.0f;
            float fHalfElementHeight = 0.0f;

            fElementHeight = (float)Math.Truncate(fHeight / (m_pCoordinateDataList.Count) * 1000) / 1000;
            fHalfElementHeight = (float)Math.Truncate(fElementHeight / 2 * 1000) / 1000;
            SizeF StartOffset = new SizeF(a_StartPos);
            // this.m_pCoordinateDataList.Clear();
            for (int i = 0; i < this.m_pCoordinateDataList.Count; i++)
            {
                fElementWidth = (float)Math.Truncate(fWidth / this.m_pCoordinateDataList[i].Length * 1000) / 1000;
                fHalfElementWidth = (float)Math.Truncate(fElementWidth / 2 * 1000) / 1000;
                for (int j = 0; j < this.m_pCoordinateDataList[i].Length; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        this.m_pCoordinateDataList[i][j].OrginCenterPos = new PointF(fHalfElementWidth, fHeight - fHalfElementHeight);
                        this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                        this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                        this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
                    }
                    else if (j == 0 && i > 0)
                    {
                        this.m_pCoordinateDataList[i][j].OrginCenterPos = PointF.Add(this.m_pCoordinateDataList[i - 1][j].OrginCenterPos, new SizeF(0, -fElementHeight));
                        this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                        this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                        this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
                    }
                    else
                    {
                        this.m_pCoordinateDataList[i][j].OrginCenterPos = PointF.Add(this.m_pCoordinateDataList[i][j - 1].OrginCenterPos, new SizeF(fElementWidth, 0));
                        this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                        this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                        this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
                    }
                }
            }
            this.m_bCreateCoordinate = true;
            return true;
        }
        #endregion Orgin
        #region descarte coodinate 수정중
        /* 
        public bool CreateCodrdinates(PointF a_CenterPos, SizeF a_Size, ZeroPosition a_CenterDefault = ZeroPosition.Center, Rotate a_Rotate = Rotate.R_0)
         {
                 try
                 {
                         if (m_pCoordinateDataList.Count > 0 && m_pCoordinateDataList[0].Length > 0)
                         {
                                 ClearVariable(false);
                                 this.m_CenterPosDefalut = a_CenterDefault;
                                 this.m_RotateAngle = a_Rotate;
                                 PointF startPos = new PointF();

                                 float fWidth = (float)Math.Truncate(a_Size.Width * 1000) / 1000;
                                 float fHeight = (float)Math.Truncate(a_Size.Height * 1000) / 1000;
                                 float fHalfWidth = (float)Math.Truncate(a_Size.Width / 2.0f * 1000) / 1000;
                                 float fHalfHeight = (float)Math.Truncate(a_Size.Height / 2.0f * 1000) / 1000;
                                 switch (this.m_CenterPosDefalut)
                                 {
                                         case ZeroPosition.Center:
                                                 //startPos.X = a_Size.Width / 2 * -1;
                                                 //startPos.Y = a_Size.Height / 2 * -1;
                                                 startPos.X = 0;
                                                 startPos.Y = 0;
                                                 break;
                                         case ZeroPosition.LT:
                                                 startPos.X = fHalfWidth ;
                                                 startPos.Y = -fHalfHeight;
                                                 break;
                                         case ZeroPosition.LB:
                                                 startPos.X = fHalfWidth;
                                                 startPos.Y = fHalfHeight;
                                                 break;
                                 }
                                 startPos.X += a_CenterPos.X;
                                 startPos.Y += a_CenterPos.Y;
                                 m_CenterPoint = a_CenterPos;

                                 if (!this.InternalCreateCodrdinates(startPos, a_Size))
                                         return false;

                                 // hatch 여부
                                 if (this.HatchOption.Type == HATCH_TYPE.HATCH_TYPE_NONE)
                                 {
                                         //Hatch hatch = new Hatch();
                                         //GraphicsPath hatchPath = new GraphicsPath();
                                         this.m_CodeGraphicsPath.AddPath(this.Get2dCodeGrapchicsPath(), false);
                                         //hatch.CreateShapeHatch(this.Get2dCodeGrapchicsPath(), ref this.m_CodeGraphicsPath);
                                 }
                                 else
                                 {
                                         Hatch hatch = new Hatch();
                                         hatch.Option = this.m_HatchOption;
                                         GraphicsPath hatchPath = new GraphicsPath();
                                         //hatch.CreateShapeHatch(this.Get2dCodeGrapchicsPath(), ref this.m_CodeGraphicsPath);
                                         hatch.CreateShapeHatch(this.Get2dCodeGrapchicsPath_ADjoin(), ref this.m_CodeGraphicsPath);
                                 }
                                 return true;
                         }
                         else
                                 return false;
                 }
                 catch
                 {

                 }
                 return false;
         }

        private bool InternalCreateCodrdinates(PointF a_StartPos, SizeF a_Size)
{
    if (m_pCoordinateDataList == null)
        return false;
    if (m_pCoordinateDataList.Count <= 0)
        return false;
    if (m_pCoordinateDataList[0].Length <= 0)
        return false;

    m_Size.Width = a_Size.Width;
    m_Size.Height = a_Size.Height;
    float fWidth = (float)Math.Truncate(m_Size.Width * 1000) / 1000;
    float fHeight = (float)Math.Truncate(m_Size.Height * 1000) / 1000;
                float fHalfWidth=(float)Math.Truncate(m_Size.Width/2.0f * 1000) / 1000;
                float fHalfHeight=(float)Math.Truncate(m_Size.Height/2.0f * 1000) / 1000;
    float fElementWidth = 0.0f;
    float fElementHeight = 0.0f;
    float fHalfElementWidth = 0.0f;
    float fHalfElementHeight = 0.0f;

    fElementHeight = (float)Math.Truncate(fHeight / (m_pCoordinateDataList.Count) * 1000) / 1000;
    fHalfElementHeight = (float)Math.Truncate(fElementHeight / 2 * 1000) / 1000;
    SizeF StartOffset = new SizeF(a_StartPos);
    // this.m_pCoordinateDataList.Clear();
    for (int i = 0; i < this.m_pCoordinateDataList.Count; i++)
    {
        fElementWidth = (float)Math.Truncate(fWidth / this.m_pCoordinateDataList[i].Length * 1000) / 1000;
        fHalfElementWidth = (float)Math.Truncate(fElementWidth / 2 * 1000) / 1000;
        for (int j = 0; j < this.m_pCoordinateDataList[i].Length; j++)
        {
            if (i == 0 && j == 0)
            {
                this.m_pCoordinateDataList[i][j].OrginCenterPos = new PointF(-fHalfWidth+fHalfElementWidth, fHalfHeight-fHalfElementHeight);
                this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
            }
            else if (j == 0 && i > 0)
            {
                this.m_pCoordinateDataList[i][j].OrginCenterPos = PointF.Add(this.m_pCoordinateDataList[i - 1][j].OrginCenterPos, new SizeF(0, -fElementHeight));
                this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
            }
            else
            {
                this.m_pCoordinateDataList[i][j].OrginCenterPos = PointF.Add(this.m_pCoordinateDataList[i][j - 1].OrginCenterPos, new SizeF(fElementWidth, 0));
                this.m_pCoordinateDataList[i][j].fWidth = fElementWidth;
                this.m_pCoordinateDataList[i][j].fHeight = fElementHeight;
                this.m_pCoordinateDataList[i][j].CenterOffset = StartOffset;
            }
        }
    }
    this.m_bCreateCoordinate = true;
    return true;
}*/
        #endregion

        /// <summary>
        /// private void SaveTo(string fileName, ImageFormat format)
        // {
        //      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        //      {
        //           saveFileDialog.DefaultExt = Path.GetExtension(fileName);
        //           saveFileDialog.FileName = fileName;
        //           saveFileDialog.Title = "Save To Image";
        //           if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //               SaveToImage(saveFileDialog.FileName, format);
        //      }
        //  }   
        /// </summary>
        /// <param name="a_filePath"></param>
        /// <param name="imageFormat"></param>

        private Bitmap CopyBitMapSection(Bitmap sourceBitmap, Rectangle section)
        {
            // Create the new bitmap and associated graphics object
            Bitmap bitmap = new Bitmap(section.Width, section.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Copy the specified section of the source bitmap to the new one
                graphics.DrawImage(sourceBitmap, 0, 0, section, GraphicsUnit.Pixel);
            }
            return bitmap;
        }
        public void ImagePanelPaint(object sender, PaintEventArgs e)
        {

            if (m_CodeGraphicsPath != null && m_bCreateCoordinate == true)
            {
                Control pControl = sender as Control;
                Graphics graphics = e.Graphics;
                using (BufferedGraphics pDBufferdGraphics = BufferedGraphicsManager.Current.Allocate(graphics, pControl.ClientRectangle))
                {
                    using (GraphicsPath pDrawPath = m_CodeGraphicsPath.Clone() as GraphicsPath)
                    using (Matrix pOffset = new Matrix())
                    {
                        pOffset.Translate(-m_CenterPoint.X, -m_CenterPoint.Y);
                        //pOffset.Rotate(180);

                        pDrawPath.Transform(pOffset);
                        using (Brush BackColor = new SolidBrush(pControl.BackColor))
                        {
                            using (Pen pen = new Pen(Color.Black))
                            {
                                PointF location = new PointF();
                                RectangleF rect = pDrawPath.GetBounds();
                                float scaleX = pControl.Width / rect.Width;
                                float scaleY = pControl.Height / rect.Height;
                                float scaleVal = (scaleX < scaleY) ? scaleX : scaleY;
                                pen.Width = 0.05f;
                                switch (this.m_CenterPosDefalut)
                                {
                                    case ZeroPosition.LT:
                                        location.X = (float)Math.Round(rect.Width / 2);
                                        location.Y = (float)Math.Round(rect.Height / 2 * -1);
                                        break;
                                    case ZeroPosition.LB:
                                        location.X = (float)Math.Round(rect.Width / 2);
                                        location.Y = (float)Math.Round(rect.Height / 2);
                                        break;
                                }

                                pDBufferdGraphics.Graphics.Clear(pControl.BackColor);
                                pDBufferdGraphics.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                                pDBufferdGraphics.Graphics.TranslateTransform(pControl.Width / 2.0f - location.X * scaleVal * 0.9f, pControl.Height / 2.0f + location.Y * scaleVal * 0.9f);
                                //descarte coodinate translate -> Graphics Path  x=x     y=-y
                                pDBufferdGraphics.Graphics.ScaleTransform(this.fDrawZoomMultiplier * scaleVal * 0.9f, this.fDrawZoomMultiplier * scaleVal * 0.9f * -1);
                                pDBufferdGraphics.Graphics.DrawPath(pen, pDrawPath);
                                pDBufferdGraphics.Render(graphics);
                            }
                        }
                    }
                }
            }
            //GraphicsPath path = new GraphicsPath();
            //for (int i = 0; i < this.EncodedDatas.Count; i++)
            //{
            //    for (int j = 0; j < this.EncodedDatas[i].RowCount; j++)
            //    {
            //        path.AddPolygon(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointF());
            //    }
            //}            
        }
        public void ImagePanelPaint(Control a_Control)
        {
            if (a_Control != null && m_CodeGraphicsPath != null && m_bCreateCoordinate == true)
            {
                Control pControl = a_Control;
                using (Graphics graphics = a_Control.CreateGraphics())
                {
                    using (BufferedGraphics pDBufferdGraphics = BufferedGraphicsManager.Current.Allocate(graphics, a_Control.DisplayRectangle))
                    {
                        using (GraphicsPath pDrawPath = m_CodeGraphicsPath.Clone() as GraphicsPath)
                        using (Matrix pOffset = new Matrix())
                        {

                            pOffset.Translate(-m_CenterPoint.X, -m_CenterPoint.Y);
                            //pOffset.RotateAt(180,new PointF(-m_CenterPoint.X, -m_CenterPoint.Y));
                            pDrawPath.Transform(pOffset);
                            using (Brush BackColor = new SolidBrush(a_Control.BackColor))
                            {
                                using (Pen pen = new Pen(Color.Black))
                                {
                                    PointF location = new PointF();
                                    RectangleF rect = pDrawPath.GetBounds();
                                    float scaleX = pControl.Width / rect.Width;
                                    float scaleY = pControl.Height / rect.Height;
                                    float scaleVal = (scaleX < scaleY) ? scaleX : scaleY;
                                    switch (this.m_CenterPosDefalut)
                                    {
                                        case ZeroPosition.LT:
                                            location.X = (float)Math.Round(rect.Width / 2);
                                            location.Y = (float)Math.Round(rect.Height / 2 * -1);
                                            break;
                                        case ZeroPosition.LB:
                                            location.X = (float)Math.Round(rect.Width / 2);
                                            location.Y = (float)Math.Round(rect.Height / 2);
                                            break;
                                    }
                                    pen.Width = 0.05f;
                                    pDBufferdGraphics.Graphics.Clear(a_Control.BackColor);
                                    pDBufferdGraphics.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                                    //pDBufferdGraphics.Graphics.FillRectangle(BackColor, a_Control.DisplayRectangle);
                                    pDBufferdGraphics.Graphics.TranslateTransform(pControl.Width / 2 - location.X * scaleVal * 0.9f, pControl.Height / 2 + location.Y * scaleVal * 0.9f);
                                    pDBufferdGraphics.Graphics.ScaleTransform(this.fDrawZoomMultiplier * scaleVal * 0.9f, this.fDrawZoomMultiplier * scaleVal * 0.9f);
                                    pDBufferdGraphics.Graphics.DrawPath(pen, pDrawPath);
                                    pDBufferdGraphics.Render(graphics);
                                }
                            }
                        }
                    }
                }
            }
            //GraphicsPath path = new GraphicsPath();
            //for (int i = 0; i < this.EncodedDatas.Count; i++)
            //{
            //    for (int j = 0; j < this.EncodedDatas[i].RowCount; j++)
            //    {
            //        path.AddPolygon(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointF());
            //    }
            //}            
        }
        /*private GraphicsPath Get2dCodeGrapchicsPath()
        {
            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < this.m_pCoordinateDataList.Count; i++)
            {
                for (int j = 0; j < this.m_pCoordinateDataList[i].Length; j++)
                {
                    if (this.m_pCoordinateDataList[i][j].bHatch)
                    {
                        if (this.m_Shape == CellShape.Rectangle)
                            path.AddPolygon(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointF());
                        else if (this.m_Shape == CellShape.Circle)
                            path.AddPolygon(this.m_pCoordinateDataList[i][j].GetCircleOuterLinePointF(45.0f));
                    }
                }
            }
            return path;
        }*/
        public GraphicsPath Get2dCodeGrapchicsPath()
        {
            GraphicsPath path = new GraphicsPath();

            for (int i = 0; i < this.m_pCoordinateDataList.Count; i++)
            {
                for (int j = 0; j < this.m_pCoordinateDataList[i].Length; j++)
                {
                    if (this.m_pCoordinateDataList[i][j].bHatch)
                    {
                        if (this.Shape == CellShape.Rectangle)
                        {
                            path.AddLines(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointF());
                            // path.AddRectangle(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointFRect());
                            path.CloseFigure();
                        }
                        else if (this.Shape == CellShape.Circle)
                        {
                            path.AddLines(this.m_pCoordinateDataList[i][j].GetCircleOuterLinePointF(45.0f));
                            path.CloseFigure();
                        }
                    }
                }
            }

            // CenterPos -> 0,0
            RectangleF rect = path.GetBounds();
            PointF Center = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            Matrix matrix = new Matrix();
            matrix.Translate(-Center.X, -Center.Y);
            path.Transform(matrix);

            // Rotate
            matrix.Reset();
            matrix.Rotate((int)this.m_RotateAngle);
            path.Transform(matrix);

            // Shift
            matrix.Reset();
            matrix.Translate(Center.X, Center.Y);
            path.Transform(matrix);

            return path;
        }


        /// <summary>
        ///  To be continue 
        /// </summary>
        /// <returns></returns>
        public GraphicsPath Get2dCodeGrapchicsPath_ADjoin()
        {
            GraphicsPath path = new GraphicsPath();
            PointF[] ADjoinStartPoint = null;
            PointF[] ADjoinEndPoint = null;
            int iIgnoreIDX = -1;
            for (int i = 0; i < this.m_pCoordinateDataList.Count; i++)
            {
                for (int j = 0; j < this.m_pCoordinateDataList[i].Length; j++)
                {

                    if (this.m_pCoordinateDataList[i][j].bHatch)
                    {
                        if (this.Shape == CellShape.Rectangle)
                        {
                            if (iIgnoreIDX > -1)
                            {
                                if (iIgnoreIDX >= j)
                                {
                                    continue;
                                }
                                else
                                {
                                    iIgnoreIDX = -1;
                                }
                            }
                            if (iIgnoreIDX < 0)
                            {

                                if (j + 1 < this.m_pCoordinateDataList[i].Length)
                                {
                                    ADjoinStartPoint = this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointF();
                                    for (int Adjoin = j + 1; Adjoin < this.m_pCoordinateDataList[i].Length; Adjoin++)
                                    {
                                        if (this.m_pCoordinateDataList[i][Adjoin].bHatch)
                                        {
                                            iIgnoreIDX = Adjoin;
                                            ADjoinEndPoint = this.m_pCoordinateDataList[i][Adjoin].GetRectgleOuterLinePointF();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (iIgnoreIDX > -1)
                                    {
                                        path.AddLines(new PointF[] {
                                                                                ADjoinStartPoint[0],
                                                                                ADjoinEndPoint[1],
                                                                                ADjoinEndPoint[2],
                                                                                ADjoinStartPoint[3],
                                                                                        }
                                        );
                                        // path.AddRectangle(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointFRect());
                                        path.CloseFigure();
                                    }
                                    else
                                    {
                                        path.AddLines(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointF());
                                        // path.AddRectangle(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointFRect());
                                        path.CloseFigure();
                                        iIgnoreIDX = -1;
                                    }
                                }
                                else
                                {
                                    path.AddLines(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointF());
                                    // path.AddRectangle(this.m_pCoordinateDataList[i][j].GetRectgleOuterLinePointFRect());
                                    path.CloseFigure();
                                    iIgnoreIDX = -1;
                                }
                            }
                        }
                        else if (this.Shape == CellShape.Circle)
                        {
                            path.AddLines(this.m_pCoordinateDataList[i][j].GetCircleOuterLinePointF(45.0f));
                            path.CloseFigure();
                        }
                    }
                }
                iIgnoreIDX = -1;
            }

            // CenterPos -> 0,0
            RectangleF rect = path.GetBounds();
            PointF Center = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            Matrix matrix = new Matrix();
            matrix.Translate(-Center.X, -Center.Y);
            path.Transform(matrix);

            // Rotate
            matrix.Reset();
            matrix.Rotate((int)this.m_RotateAngle);
            path.Transform(matrix);

            // Shift
            matrix.Reset();
            matrix.Translate(Center.X, Center.Y);
            path.Transform(matrix);

            return path;
        }

        public override System.String ToString()
        {
            StringBuilder binaryString = new StringBuilder();
            for (int i = 0; i < this.m_pCoordinateDataList.Count; i++)
            {
                for (int j = 0; j < this.m_pCoordinateDataList[i].Length; j++)
                {
                    binaryString.Append(m_pCoordinateDataList[i][j].bHatch == true ? "1" : "0");
                }
                binaryString.Append(Environment.NewLine);
            }
            return binaryString.ToString();
        }
    }


}
