using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    public class DKT_ConvertCode : SingleTone<DKT_ConvertCode>
    {

				#region static Define
				public static int MAX_CODE_LEGNTH = 4;  // 최대 데이터 길이
				public static int MAX_CODE_DATA = 32; // 진법

				public static string CODE_TYPE_VARIABLE = "V";
				public static string CODE_TYPE_EXTRA_VARIABLE = "E";
				public static string CODE_TYPE_FIXED = "F";
				public static string CODE_TYPE_EXTRA_CHARACTER = "C";

				public static string[] SERIAL_VARIABLE = new string[32] {
																										"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
																										"A", "B", "C", "D", "E", "F", "G", "H", "J", "K",
																										"L", "M", "N", "R", "S", "T", "U", "V", "W", "X",
																										"Y", "Z" };
				#endregion static Define
				public enum eCodeType
				{
						Variable,       // 가변식
						ExtraVariable,  // 추가 가변식
						Fixed,          // 고정식
						Character,      // 문자식

						CodeType_Count,
				};
				private string[] StrCodeData;
				protected override void OnCreateInstance()
				{
						base.OnCreateInstance();
				}



        private void SetStringCodeData(eCodeType CodeType)
        {
            switch (CodeType)
            {
                case eCodeType.Variable:
                    StrCodeData = SERIAL_VARIABLE;
                    break;
                case eCodeType.ExtraVariable:
                case eCodeType.Fixed:
                case eCodeType.Character:
                    break;
            }
        }

        private bool GetSerial(int valueDecimal, out string strSerial)
        {
            strSerial = string.Empty;

            if (MAX_CODE_DATA <= valueDecimal || 0 > valueDecimal)
                return false;
            else
                strSerial = StrCodeData[valueDecimal];

            return true;
        }

        private bool GetDecimal(char chSerial, out int iVal)
        {
            iVal = Array.FindIndex(StrCodeData, i => i == chSerial.ToString());

            if (iVal < 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 32진법 코드를 10진수로 변환
        /// </summary>
        /// <param name="CodeType"></param>
        /// <param name="strSerial32"></param>
        /// <param name="iSerial10"></param>
        /// <returns></returns>
        public bool ConvertSerialNumber_32To10(eCodeType CodeType, string strSerial32, out int iSerial10)
        {
            iSerial10 = 0;
            int strSerial32Length = strSerial32.Length;

            if (strSerial32Length <1 ||  CodeType >= eCodeType.CodeType_Count)
            {
                return false;
            }

            char[] charArray = strSerial32.ToCharArray();
            Array.Reverse(charArray);
            strSerial32 = new string(charArray);

            SetStringCodeData(CodeType);

            for (int i = 0; i < strSerial32Length; i++)
            {
                int iVal;
                if (this.GetDecimal(strSerial32[i], out iVal))
                    iSerial10 += (int)Math.Pow(MAX_CODE_DATA, i) * iVal;
                else
                    return false;
            }

            return true;
        }

				/// <summary>
				/// 10진수 시리얼넘버를 32진법의 시리얼넘버로 변환
				/// </summary>
				/// <param name="CodeType"></param>
				/// <param name="iSerial10"></param>
				/// <param name="strSerial32"></param>
				/// <returns></returns>
				public bool ConvertSerialNumber_10To32(eCodeType CodeType, int iSerial10, int a_iNumberOfZeroPad, out string strSerial32)
				{
						strSerial32 = string.Empty;

						if (CodeType >= eCodeType.CodeType_Count)
								return false;



						SetStringCodeData(CodeType);

						bool bLoop = true;
						string strTemp = string.Empty;
						int iTempSerial10 = iSerial10;

						while (bLoop)
						{
								int iValue = iTempSerial10 % MAX_CODE_DATA;
								iTempSerial10 = (int)(iTempSerial10 / MAX_CODE_DATA);

								if (!this.GetSerial(iValue, out strTemp))
										return false;

								strSerial32 += strTemp;

								if (iTempSerial10 == 0)
										bLoop = false;
						}

						char[] charArray = strSerial32.ToCharArray();
						Array.Reverse(charArray);

						strSerial32 = new string(charArray);

						if (strSerial32.Length < a_iNumberOfZeroPad)
						{
								strSerial32 = strSerial32.PadLeft(a_iNumberOfZeroPad, '0');
						}
						return true;
				}
		}
}
