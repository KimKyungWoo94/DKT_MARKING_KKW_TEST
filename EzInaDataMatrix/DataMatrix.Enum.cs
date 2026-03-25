using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintNet;

namespace EzIna.DataMatrix
{
    public enum Rotate : int
    {
        R_0 = 0,
        R_90 = 90,
        R_180 = 180,
        R_270 = 270,
    }
    public enum CapacityIndex : int
    {
        Number,
        Alphabet,
        Binary,
    }
    public enum CellShape : int
    {
        Rectangle,
        Circle,
    }
    public enum ZeroPosition : int
    {
        Center,
        LT,
        LB,
    }
		public enum eDataMatrixSize
		{

				/// <summary>
				/// Datamatrix square 10x10.
				/// </summary>
				DM10X10 = DataMatrixSize.DM10X10,
				/// <summary>
				/// Datamatrix square 12x12.
				/// </summary>
				DM12X12,
				/// <summary>
				/// Datamatrix square 14 x 14.
				/// </summary>
				DM14X14,
				/// <summary>
				/// Datamatrix square 16 x 16.
				/// </summary>
				DM16X16,
				/// <summary>
				/// Datamatrix square 18 x 18.
				/// </summary>
				DM18X18,
				/// <summary>
				/// Datamatrix square 20 x 20.
				/// </summary>
				DM20X20,
				/// <summary>
				/// Datamatrix square 22 x 22.
				/// </summary>
				DM22X22,
				/// <summary>
				/// Datamatrix square 24 x 24.
				/// </summary>
				DM24X24,
				/// <summary>
				/// Datamatrix square 26 x 26.
				/// </summary>
				DM26X26,
				/// <summary>
				/// Datamatrix square 32 x 32.
				/// </summary>
				DM32X32,
				/// <summary>
				/// Datamatrix square 36 x 36.
				/// </summary>
				DM36X36,
				/// <summary>
				/// Datamatrix square 40 x 40.
				/// </summary>
				DM40X40,
				/// <summary>
				/// Datamatrix square 44 x 44.
				/// </summary>
				DM44X44,
				/// <summary>
				/// Datamatrix square 48 x 48.
				/// </summary>
				DM48X48,
				/// <summary>
				/// Datamatrix square 52 x 52.
				/// </summary>
				DM52X52,
				/// <summary>
				/// Datamatrix square 64 x 64.
				/// </summary>
				DM64X64,
				/// <summary>
				/// Datamatrix square 72 x 72.
				/// </summary>
				DM72X72,
				/// <summary>
				/// Datamatrix square 80 x 80.
				/// </summary>
				DM80X80,
				/// <summary>
				/// Datamatrix square 88 x 88.
				/// </summary>
				DM88X88,
				/// <summary>
				/// Datamatrix square 96 x 96.
				/// </summary>
				DM96X96,
				/// <summary>
				/// Datamatrix square 104 x 104.
				/// </summary>
				DM104X104,
				/// <summary>
				/// Datamatrix square 120 x 120.
				/// </summary>
				DM120X120,
		}
		public enum DM_HATCH_TYPE
		{
				NONE = 0,
				LINE,
				ZIGZAG,
				CROSS,
				CONTOUR,
		}


}
