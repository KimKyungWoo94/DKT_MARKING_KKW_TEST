using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{
	public class RunningTimingChart
	{
		
		int	m_nPoints; 
		int	m_nPeriod;
		List<double> m_listMeasValues;


		public RunningTimingChart()
		{
		}
		~RunningTimingChart()
		{
			Clear();
		}
		public void AddData(double a_dValue)
		{
			if(m_listMeasValues.Count < m_nPoints)
				m_listMeasValues.Add(a_dValue);
		}
		public double GetData(int a_nPoint)
		{
			if(m_listMeasValues.Count > a_nPoint)
			{
				return m_listMeasValues[a_nPoint];
			}
			return 0;			
		}
		public int GetSize()
		{
			return m_listMeasValues.Count;
		}
		public void DataClear()
		{
			m_listMeasValues.Clear();
		}
		public void Clear()
		{
            if(m_listMeasValues != null)
            {
			    m_listMeasValues.Clear();
			    m_listMeasValues = null;
            }
			m_nPeriod = 0;
			m_nPoints = 0;
		}

		public void Init(int a_nPeriod, int a_nPoints)
		{
			m_nPeriod = a_nPeriod;
			m_nPoints = a_nPoints;
			m_listMeasValues = new List<double>();
		}

	}
	//Timing Chart
	public static class TC 
	{
		private static Color m_BackColor;
		private static Color m_LineColor;
		private static Color m_FontColor;

		private static int m_nStartIndex;
		private static int m_nEndIndex	;
		private static int m_nNoOfPoints;

		public static List<RunningTimingChart> m_listSeries;

		// The index of the array position to which new data values are added.
		private static int m_iCurrentIndex = 0;

		// The full range is initialized to 1 seconds of data. It can be extended when more data
		// are available.
		private static int m_nInitialFullRange = 1 * 1000; //Unit : msec

		// The maximum zoom in is 1 milliseconds.
		private static int m_nZoomInLimit = 10;


		// Create an XYChart object of size 640 x 350 pixels
		private static Rectangle m_rtChartArea;

		private static string m_strXAxisTitle;
		private static string m_strYAxisTitle;

		private static Point m_ptTickDensity;

		// Set the plotarea at (55, 50) with width 80 pixels less than chart width, and height 85 pixels
		// less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
		// as background. Set border to transparent and grid lines to white (ffffff).

		private static Rectangle m_rtPlotArea;
		private static Rectangle m_rtLeftLine;
		private static Rectangle m_rtRightLine;

		private static int m_nPeriod;
		private static int m_nPoints;
		public  static TimeSpan m_TimeSpan;
		public static void Init()
		{
			m_rtChartArea	= new Rectangle();
			m_rtPlotArea	= new Rectangle();
			m_rtLeftLine	= new Rectangle();
			m_rtRightLine	= new Rectangle();
			m_ptTickDensity = new Point(75, 10);

			m_BackColor		= new Color();
			m_LineColor		= new Color();
			m_FontColor		= new Color();


			m_listSeries = new List<RunningTimingChart>();
		}
		#region [ Set Series / Add data / Get data / Clear]
		public static bool allocSeries(int a_nSeries, int a_nPeriod, int a_nPoints)
		{
			if(m_listSeries.Count > 0 )
				return false;

			m_nPeriod = a_nPeriod;
			m_nPoints = a_nPoints;

			for(int i = 0; i < a_nSeries; i++)
			{
				m_listSeries.Add(new RunningTimingChart());
				m_listSeries[i].Init(a_nPeriod, a_nPoints);
			}

			return true;

		}

		public static void freeSeries()
		{
			foreach(RunningTimingChart item in m_listSeries)
			{
				item.Clear();
			}

			m_listSeries.Clear();
			m_listSeries = null;

		}

        public static bool IsDataFully()
        {
            foreach (RunningTimingChart item in m_listSeries)
            {
                if( item.GetSize() >= m_nPoints)
                    return true;
            }

            return false;
        }
		public static void AddData(int a_iSeries, double a_dData)
		{
			if(a_iSeries < m_listSeries.Count)
			{
				m_listSeries[a_iSeries].AddData(a_dData);
			}	
		}
		public static void ClearData()
		{
			foreach(RunningTimingChart item in m_listSeries)
			{
				item.DataClear();
			}

		}
        #endregion [ Set Series / Add data / Get data / Clear]
        #region [ Log Save ]
        public static void LogSave()
        {
//             string strFileName = FA.DIR.Log_Vision + ACT_STATUS.GetPosX_DummyStage_R().ToString("D3") + "_" +
//                 ACT_STATUS.GetPosY_DummyStage_R().ToString("D3") + ".csv";
// 
// 
//             FileStream f = new FileStream(strFileName, FileMode.Create, FileAccess.Write);
//             if (f.Length < 0)
//             {
//                 // Close the file
//                 f.Close();
//                 return;
//             }
// 
//             StreamWriter CsvFileExport = new StreamWriter(f, Encoding.UTF8);
//             string strDelimiter = ",";
// 
//             int nSingluationDisk = M.RunMgr.SingulationDiskNo + 1;
//             CsvFileExport.WriteLine(string.Format("{0},{1:D2}", "Singlutation Disk No", nSingluationDisk));
//             CsvFileExport.WriteLine("Total Seconds"         + strDelimiter + TC.m_TimeSpan.TotalSeconds     .ToString());
//             CsvFileExport.WriteLine("Total Milliseconds"    + strDelimiter + TC.m_TimeSpan.TotalMilliseconds.ToString());
// 
//             CsvFileExport.WriteLine(
//                   FA.DEF.eSeries.Type_ValueVot_Keller   .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_ValueVot_1        .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_ValueVot_2        .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_ValueMotorPos     .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_OnOff_SDR         .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_OnOff_U_Axis      .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_OnOff_FlowSensor  .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_OnOff_FiberSensor .ToString() + strDelimiter
//                 + FA.DEF.eSeries.Type_OnOff_LaserShot   .ToString());
// 
//             for (int i = 0; i < TC.m_listSeries[0].GetSize(); i++)
//             {
// 
//                 CsvFileExport.WriteLine(
//                       TC.m_listSeries[0].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[1].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[2].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[3].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[4].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[5].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[6].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[7].GetData(i).ToString() + strDelimiter
//                     + TC.m_listSeries[8].GetData(i).ToString());
//             }
// 
//             CsvFileExport.Flush();
//             CsvFileExport.Close();
//             f.Close();
// 
//             CsvFileExport.Dispose();
//             f.Dispose();

        }
        #endregion [ Log Save ]
        public static void DrawChart( PaintEventArgs e )
		{

			e.Graphics.Clear(m_BackColor);
			#region [ Draw chart outline]
			Pen PenMainLines = new Pen(m_LineColor, 2);
			Pen PenSubLines = new Pen(m_LineColor, 1);

			PenSubLines.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			//Draw the main lines of XY
			e.Graphics.DrawLine(PenMainLines, m_rtPlotArea.X, m_rtPlotArea.Height, m_rtPlotArea.Width	, m_rtPlotArea.Height);
			e.Graphics.DrawLine(PenMainLines, m_rtPlotArea.X, m_rtPlotArea.Height, m_rtPlotArea.X		, m_rtPlotArea.Y	 );


			//Draw the sub lines of XY
			for (int tickY = m_rtPlotArea.Height - m_ptTickDensity.Y; tickY > m_rtPlotArea.Y; tickY -= m_ptTickDensity.Y)
			{
				for (int tickX = m_rtPlotArea.X + m_ptTickDensity.X; tickX < m_rtPlotArea.Width; tickX += m_ptTickDensity.X)
				{
					//Draw X Axis
					e.Graphics.DrawLine(PenSubLines, m_rtPlotArea.X, tickY, m_rtPlotArea.Width, tickY);
					//Draw Y Axis
					e.Graphics.DrawLine(PenSubLines, tickX, m_rtPlotArea.Height, tickX, m_rtPlotArea.Y);
				}
			}


			//Draw track lines
			Pen PenLeftLine = new Pen(Color.Blue);
			e.Graphics.DrawLine(PenLeftLine, m_rtLeftLine.X, m_rtPlotArea.Y, m_rtLeftLine.X, m_rtPlotArea.Height);

			#endregion [ Draw Chart Outline ]

			#region [ Draw chart Series]

			PenSubLines.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			//Draw Series 
			/*
			Type_ValueVot_Keller
			Type_ValueVot_1
			Type_ValueVot_2
			Type_OnOff_SDR
			Type_OnOff_U_Axis
			Type_OnOff_FlowSensor
			Type_OnOff_FiberSensor
			Type_OnOff_LaserShot
			*/
			//Set Scale of X
			float fTickX = m_TimeSpan.Milliseconds / m_rtPlotArea.Width ;


			//Draw the sub lines of XY
			for (int tickY = m_rtPlotArea.Height - m_ptTickDensity.Y; tickY > m_rtPlotArea.Y; tickY -= m_ptTickDensity.Y)
			{
				for (int iSeries = 0; iSeries < m_listSeries.Count; iSeries++)
				{
					for (int item = 0; item < m_listSeries[iSeries].GetSize(); item++)
					{
						//Draw X Axis
// 						e.Graphics.DrawLine(PenSubLines ,  
// 						(float)(m_rtPlotArea.X + fTickX * item) ,		//x0
// 						tickY + (tickY * m_listSeries[iSeries].GetData(Value),											//y0
// 						//m_rtPlotArea.X + (int)fTickX * (Value + 1),		//x1
// 						tickY);											//y1
						
					}
				}
			}


			//Draw track lines
			e.Graphics.DrawLine(PenLeftLine, m_rtLeftLine.X, m_rtPlotArea.Y, m_rtLeftLine.X, m_rtPlotArea.Height);


			PenMainLines.Dispose();
			PenMainLines.Dispose();
			PenLeftLine.Dispose();
			#endregion [ Draw Chart Series ]

		}

		public static void InitChart(Control a_ctrl, string a_strXAxisTitle, string a_strYAxisTitle, Color a_BackColor, Color a_LineColor, Color a_FontColor)
		{
			m_rtChartArea.X = 0; m_rtChartArea.Width = a_ctrl.Width;
			m_rtChartArea.Y = 0; m_rtChartArea.Height = a_ctrl.Height;

			m_rtPlotArea.X = 55; m_rtPlotArea.Width = m_rtChartArea.Width - m_rtPlotArea.X;
			m_rtPlotArea.Y = 50; m_rtPlotArea.Height = m_rtChartArea.Height - m_rtPlotArea.Y;

			m_strXAxisTitle = a_strXAxisTitle;
			m_strYAxisTitle = a_strYAxisTitle;

			m_BackColor = a_BackColor;
			m_LineColor = a_LineColor;
			m_FontColor = a_FontColor;

		}

		private static void MouseMove(this Control ctrl, object sender, MouseEventArgs e)
		{
			if (m_rtPlotArea.X <= e.X && e.X <= m_rtPlotArea.Width &&
				m_rtPlotArea.Y <= e.Y && e.Y <= m_rtPlotArea.Height)
			{
				m_rtLeftLine.Location = e.Location;
				ctrl.Cursor = Cursors.Hand;
				if (e.Button == MouseButtons.Left)
					ctrl.Invalidate();
			}
			else
				ctrl.Cursor = Cursors.Default;
		}
		private static void Paint(this Control ctrl, object sender, PaintEventArgs e)
		{
			DrawChart(e);
		}
	}
}
