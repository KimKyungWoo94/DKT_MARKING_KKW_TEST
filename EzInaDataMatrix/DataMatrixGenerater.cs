using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZintNet;
namespace EzIna.DataMatrix
{
    public sealed partial class DMGenerater : SingleTone<DMGenerater>
    {
       static int [,] Capacity  = {
            {6   ,  3   ,   1   },//10 x 10		
            {10  ,  6   ,   3   },//12 x 12		
            {16  ,  10  ,   6   },//14 x 14		
            {24  ,  16  ,   10  },//16 x 16		
            {36  ,  25  ,   16  },//18 x 18		
            {44  ,  31  ,   20  },//20 x 20		
            {60  ,  43  ,   28  },//22 x 22		
            {72  ,  52  ,   34  },//24 x 24		
            {88  ,  64  ,   42  },//26 x 26		
            {124 ,  91  ,   60  },//32 x 32		
            {172 ,  127 ,   84  },//36 x 36		
            {228 ,  169 ,   112 },//40 x 40		
            {288 ,  214 ,   142 },//44 x 44		
            {348 ,  259 ,   172 },//48 x 48		
            {408 ,  304 ,   202 },//52 x 52		
            {560 ,  418 ,   278 },//64 x 64		
            {736 ,  550 ,   366 },//72 x 72		
            {912 ,  682 ,   454 },//80 x 80		
            {1152,  862 ,   574 },//88 x 88		
            {1392,  1042,   694 },//96 x 96		
            {1632,  1222,   814 },//104 x 104	
            {2100,  1573,   1048},//120 x 120	
            {2608,  1954,   1302},//132 x 132	
            {3116,  2335,   1556},//144 x 144	
            {10  ,  6   ,   3   },//8 x 18		
            {20  ,  13  ,   8   },//8 x 32		
            {32  ,  22  ,   14  },//12 x 26		
            {44  ,  31  ,   20  },//12 x 36		
            {64  ,  46  ,   30  },//16 x 36		
            {98  ,  72  ,   47  } //16 x 48		
        };
        
        protected override void OnCreateInstance()
        {
            Initialize();
            base.OnCreateInstance();
        }
        private void Initialize()
        {
            m_BarCodeLib = new ZintNetLib();
            m_BarCodeLib.ElementXDimension = 0.264583f;
            m_BarCodeLib.Multiplier = 1.0f;
            m_BarCodeLib.TextMargin = 0.0f;
            m_BarCodeLib.BarcodeColor = Color.Black;
            m_BarCodeLib.BarcodeTextColor = Color.Black;
            m_BarCodeLib.Font = new Font("Arial", 10.0f, FontStyle.Regular);
            m_BarCodeLib.Rotation = 0;
            m_BarCodeLib.BarcodeHeight = 20.0f;
            m_BarCodeLib.TextVisible = true;
            m_BarCodeLib.TextAlignment = TextAlignment.Center;
            m_BarCodeLib.TextPosition = TextPosition.UnderBarcode;
            m_BarCodeLib.DataMatrixSize = DataMatrixSize.DM20X20;
            m_BarCodeLib.DataMatrixRectExtn = false;
            m_BarCodeLib.DataMatrixSquare = true;
            m_BarCodeLib.EncodingMode = EncodingMode.Standard;  
            m_MarkingRealSize=new SizeF(10.0f,10.0f);   
            m_HatchOption.Clear();    
        }
		public void SetDataMatrixSize(eDataMatrixSize a_Value)
		{
				 m_BarCodeLib.DataMatrixSize=(DataMatrixSize)((int)a_Value);
		}
		public int GetAlphabetMaxCapacity(eDataMatrixSize a_Value)
		{
				return Capacity[(int)(a_Value-1),1];
		}
        public bool IsavailableData(DataMatrixSize a_Size,string a_Input)            
        {
            if(a_Size==DataMatrixSize.Automatic)
                return false;
            if(a_Size>=DataMatrixSize.DM16X48)
                return false;
            if(string.IsNullOrEmpty(a_Input)||string.IsNullOrWhiteSpace(a_Input))
                return false;

           string strAlpha = Regex.Replace(a_Input, @"[^a-zA-Z\s]", "");
           string strNumber = Regex.Replace(a_Input, @"[^0-9]", "");
           if(strAlpha.Length==0)
           {
                if(Capacity[(int)a_Size-1,(int)CapacityIndex.Number]>=strNumber.Length)
                {
                    return true;
                }
           }
           else
           {
               if (Capacity[(int)a_Size - 1, (int)CapacityIndex.Alphabet] >= strAlpha.Length+strNumber.Length)
               {
                   return true;
               }
           }                              
           return false;
        }
        public DataMatrixSize[] GetavilableSizeList(string a_str)
        {
            List<DataMatrixSize> pRet= new List<DataMatrixSize>();          
            if(string.IsNullOrEmpty(a_str)||string.IsNullOrWhiteSpace(a_str))
            {
                pRet.AddRange(Enum.GetValues(typeof(DataMatrixSize)).Cast<DataMatrixSize>());
            }
            else
            {
                string strAlpha = Regex.Replace(a_str, @"[^a-zA-Z\s]", "");
                string strNumber = Regex.Replace(a_str, @"[^0-9]", "");
                if (strAlpha.Length == 0)
                {
                    foreach(DataMatrixSize Item in Enum.GetValues(typeof(DataMatrixSize)))
                    {
                        if(Capacity[(int)Item - 1, (int)CapacityIndex.Number]>=strAlpha.Length)
                        {
                           pRet.Add(Item);                            
                        }
                    }                                      
                }
                else
                {
                   foreach(DataMatrixSize Item in Enum.GetValues(typeof(DataMatrixSize)))
                    {
                        if(Capacity[(int)Item - 1, (int)CapacityIndex.Alphabet]>=strAlpha.Length)
                        {
                            pRet.Add(Item);
                        }
                    }                                        
                }
            }
            return pRet.ToArray();
        }
        public Collection<SymbolData> EncodedDatas
        {
            get { return m_BarCodeLib.encodedBarCodeData; }
        }
        public bool IsVaild
        {
           get { return m_BarCodeLib.IsValid;}
        }
        private void SetDataMatrixSetting()
        {
            
        }
        public DM CreateDataMatrix(string a_Text)
        {
            DM pRet = null;
            if (m_BarCodeLib != null && !String.IsNullOrEmpty(a_Text))
            {
                try
                {
                    m_BarCodeLib.DataMatrixSize = m_DatamatrixSize;
                    if (m_BarCodeLib.CreateBarcode(m_SymbolID, a_Text))
                    {
                        pRet = new DM(a_Text, m_HatchOption);                  
                        pRet.CopyData(m_BarCodeLib.encodedBarCodeData);
                    }
                }
                catch (ZintNetDLLException ex)
                {
                    string errorMessage = ex.Message;
                    if (ex.InnerException != null)
                        errorMessage += ex.InnerException.Message;

                    //System.Windows.Forms.MessageBox.Show(errorMessage, "ZintNet Barcode Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //UpdateMenus();
                }
            }
            return pRet;
        }
        public DM CreateDataMatrix(string a_Text,SizeF a_Size,ZeroPosition a_CenterDefault,Rotate a_Rotate)
        {
            DM pRet=null;
            if (m_BarCodeLib != null && !String.IsNullOrEmpty(a_Text))
            {
                try
                {
                    m_BarCodeLib.DataMatrixSize=m_DatamatrixSize;
                    m_BarCodeLib.Rotation=(int)a_Rotate;
                    if(m_BarCodeLib.CreateBarcode(m_SymbolID, a_Text))
                    {
                        pRet=new DM(a_Text,m_HatchOption);  
                        pRet.Width= a_Size.Width;
                        pRet.Height=a_Size.Height;      
                        pRet.CenterPosDefalut=a_CenterDefault;
                        pRet.RotateAngle=a_Rotate;                                
                        pRet.CopyData(m_BarCodeLib.encodedBarCodeData);                        
                    }
                }
                catch (ZintNetDLLException ex)
                {
                    string errorMessage = ex.Message;
                    if (ex.InnerException != null)
                        errorMessage += ex.InnerException.Message;

                    //System.Windows.Forms.MessageBox.Show(errorMessage, "ZintNet Barcode Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //UpdateMenus();
                }
            }
            return pRet;
        }

        public  Collection<DM[]> CreateMatrixes(Collection<string[]> a_strArray)
        {
            Collection<DM[]> pRet = null;
            try
            {                                
                if (a_strArray.Count > 0 && a_strArray[0] != null && a_strArray[0].Length > 0)
                {
                    pRet = new Collection<DM[]>();
                    m_BarCodeLib.DataMatrixSize=m_DatamatrixSize;
                    for (int Row = 0; Row < a_strArray.Count; Row++)
                    {
                        pRet.Add(new DM[a_strArray[Row].Length]);
                        for (int Col = 0; Col < a_strArray[Row].Length; Col++)
                        {
                            pRet[Row][Col] = new DM(a_strArray[Row][Col],m_HatchOption);                            
                            if(m_BarCodeLib.CreateBarcode(m_SymbolID, a_strArray[Row][Col]))
                            {
                                pRet[Row][Col].CopyData(m_BarCodeLib.encodedBarCodeData);
                            }
                        }
                    }
                }
            }
            catch (ZintNetDLLException ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                    errorMessage += ex.InnerException.Message;
                //System.Windows.Forms.MessageBox.Show(errorMessage, "ZintNet Barcode Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return pRet;
        }
        public Collection<DM[]> CreateMatrixes(Collection<string[]> a_strArray,SizeF a_Size,ZeroPosition a_CenterDefault,Rotate a_Rotate)
        {
            Collection<DM[]> pRet = null;
            try
            {
                if (a_strArray.Count > 0 && a_strArray[0] != null && a_strArray[0].Length > 0)
                {
                    pRet = new Collection<DM[]>();
                    m_BarCodeLib.DataMatrixSize = m_DatamatrixSize;
                    for (int Row = 0; Row < a_strArray.Count; Row++)
                    {
                        pRet.Add(new DM[a_strArray[Row].Length]);
                        for (int Col = 0; Col < a_strArray[Row].Length; Col++)
                        {
                            pRet[Row][Col] = new DM(a_strArray[Row][Col], m_HatchOption);
                            pRet[Row][Col].Width=a_Size.Width;
                            pRet[Row][Col].Height=a_Size.Height;
                            pRet[Row][Col].CenterPosDefalut=a_CenterDefault;
                            pRet[Row][Col].RotateAngle=a_Rotate;
                            if (m_BarCodeLib.CreateBarcode(m_SymbolID, a_strArray[Row][Col]))
                            {
                                pRet[Row][Col].CopyData(m_BarCodeLib.encodedBarCodeData);
                            }
                        }
                    }
                }
            }
            catch (ZintNetDLLException ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                    errorMessage += ex.InnerException.Message;
                //System.Windows.Forms.MessageBox.Show(errorMessage, "ZintNet Barcode Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return pRet;
        }
    }
}
