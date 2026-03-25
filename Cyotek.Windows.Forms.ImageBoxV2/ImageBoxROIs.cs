using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyotek.Windows.Forms
{
	public enum ImageBoxROIs : int
	{
		/// <summary>
		///	없음
		/// </summary>
		None,

		/// <summary>
		/// Searching ROIs
		/// </summary>
		SearchingROIs,

		/// <summary>
		/// Golden ROI
		/// </summary>
		GoldenROI = 99,
	}
}
