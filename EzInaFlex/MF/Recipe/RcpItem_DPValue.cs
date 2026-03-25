using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzIna.FA;
using System.Data;
using System.IO;
using System.Globalization;

namespace EzIna.MF
{
    public sealed class RecipeItem_DPValue : RecipeItemBase
    {
        private string m_strDPCalcExpression;

        private double m_DisplayValue;
        public override double DisplayValue
        {
            get { return m_DisplayValue;}    
            set { }
        }
        private double m_DisplaySettingValue;
        public override double DisplaySettingValue
        {
            get
            {
                return m_DisplaySettingValue;
            }

            set
            {
                
            }
        }

        private DEF.eUnit m_eDisplayValueUnit;
        public RecipeItem_DPValue(DEF.eRecipeCategory a_MainCategory, 
                               string a_strSubCategory, 
                               string a_strCaption, 
                               int a_iKey, 
                               object a_InitValue, 
                               DEF.eUnit a_ValueUnit,
                               
                               string a_strDisplayCalcExpression,
                               DEF.eUnit a_DisplayValueUnit=DEF.eUnit.none
                                ) 
        : base(a_MainCategory, a_strSubCategory, a_strCaption, a_iKey, a_InitValue, a_ValueUnit)
        {
            m_strDPCalcExpression=a_strDisplayCalcExpression;
            Value=a_InitValue;
            SettingValue=a_InitValue;
            m_eDisplayValueUnit=a_DisplayValueUnit;
        }
        public string GetDisplayValueFormatString(uint a_NumberOfZero, bool bDataGridView)
        {
            return GetFormatString(a_NumberOfZero, FA.DEF.GetUnitString(m_eDisplayValueUnit), bDataGridView);
        }
        protected override void SetValue(object a_Value)
        {
            try
            {
                Type InValueType=a_Value.GetType();
                if(InValueType.IsValueType || InValueType==typeof(string))
                {
                    if (InValueType==m_Value.GetType())
                    {
                        this.CheckPropertyChanged<object>("Value",ref m_Value , a_Value);
                        if(InValueType!=typeof(Enum)&&InValueType != typeof(string) && string.IsNullOrEmpty(m_strDPCalcExpression)==false)
                        this.CheckPropertyChanged<double>("DisplayValue",ref m_DisplayValue , GetCalcValue((double)m_Value, m_strDPCalcExpression));                       
                    }
                    else 
                    {
                        if (m_SettingValue.GetType().BaseType == typeof(System.Enum))
                        {
                            if (a_Value.GetType() == typeof(string))
                            {
                                this.CheckPropertyChanged<object>("Value", ref m_Value, Enum.Parse(m_Value.GetType(), a_Value as string));
                            }
                        }
                        else
                        {
                            this.CheckPropertyChanged<object>("Value", ref m_Value, Convert.ChangeType(a_Value, m_Value.GetType()));
                            if (InValueType != typeof(Enum) && InValueType != typeof(string) && string.IsNullOrEmpty(m_strDPCalcExpression) == false)
                                this.CheckPropertyChanged<double>("DisplayValue", ref m_DisplayValue, GetCalcValue((double)m_Value, m_strDPCalcExpression));
                        }
                            
                    }
                }    
                else
                {
                    throw new Exception("SetValue : Value Not Support Type (isn't string or Value type");
                }                                                           
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);               
            }           
        }
        protected override void SetSettingValue(object a_Value)
        {
            try
            {
                Type InValueType = a_Value.GetType();
                if (InValueType.IsValueType || InValueType == typeof(string))
                {
                    if (InValueType == m_Value.GetType())
                    {
                        this.CheckPropertyChanged<object>("SettingValue", ref m_SettingValue, a_Value);
                        if (InValueType != typeof(Enum) && InValueType != typeof(string) && string.IsNullOrEmpty(m_strDPCalcExpression) == false)
                            this.CheckPropertyChanged<double>("DisplaySettingValue", ref m_DisplaySettingValue, GetCalcValue((double)m_SettingValue, m_strDPCalcExpression));                           
                    }
                    else
                    {
                        //var ValueConvertor = TypeDescriptor.GetConverter(m_Value.GetType());
                        if (m_SettingValue.GetType().BaseType == typeof(System.Enum))
                        {
                            if(a_Value.GetType()==typeof(string))
                            {
                               this.CheckPropertyChanged<object>("SettingValue", ref m_SettingValue,Enum.Parse(m_Value.GetType(),a_Value as string));                               
                           
                            }
                        }
                        else
                        {
                            this.CheckPropertyChanged<object>("SettingValue", ref m_SettingValue, Convert.ChangeType(a_Value, m_Value.GetType()));
                            if (InValueType != typeof(string) && string.IsNullOrEmpty(m_strDPCalcExpression) == false)
                                this.CheckPropertyChanged<double>("DisplaySettingValue", ref m_DisplaySettingValue, GetCalcValue((double)m_SettingValue, m_strDPCalcExpression));
                           
                        }                                               
                    }
                }
                else
                {
                    throw new Exception("SetSettingValue : Value Not Support Type (isn't string or Value type");
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
        }
        public void GetDisplayValue<T>(out T a_outvalue)
        {
            try
            {
                Type pOutBufferType = typeof(T);
                if (typeof(T).IsValueType)
                {
                    if (pOutBufferType == m_DisplayValue.GetType())
                    {
                        a_outvalue = (T)m_Value;
                    }
                    else
                    {
                        a_outvalue = (T)Convert.ChangeType(m_DisplayValue, pOutBufferType);
                    }
                }
                else
                {
                    throw new Exception("GetDisplay : Value Isn't Value Type Buffer");
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
        }
        public override void WriteFileSave(StreamWriter Writer)
        {
            if (Writer != null)
            {
                //		1		2		3		  4		   5	   6	  7	       8
                //eCategory | Key | Category | Caption | Value | eUnit | Axis | ValueType
                string fmtP = " | {0:D3} | {1} | {2} | {3} | {4} | {5:D3} | {6}";
                Writer.WriteLine(
                 this.eMainRcpCategory.ToString() //1 
                 + fmtP,
                  this.iKey, //2
                  this.strSubCategory,        //3
                  this.strCaption,            //4
                  Convert.ToString(this.Value,CultureInfo.InvariantCulture),//5 
                  this.eValueUnit.ToString(), //6
                  0,                          //7
                  this.ValueType              //8
									
                );
            }
        }
        private double GetCalcValue(double a_Value, string a_Expressionstr)
        {
            double pRet = 0.0;
            try
            {
                using (DataTable m_pDataTableExpression = new DataTable())
                {
                    m_pDataTableExpression.Columns.Add("Value", typeof(double));
                    m_pDataTableExpression.Columns.Add("Result", typeof(double));
                    m_pDataTableExpression.Columns["Result"].Expression = a_Expressionstr;
                    DataRow DataRows = m_pDataTableExpression.Rows.Add();
                    DataRows["Value"] = a_Value;
                    m_pDataTableExpression.BeginLoadData();
                    m_pDataTableExpression.EndLoadData();
                    pRet = (double)DataRows["Result"];
                }
            }
            catch
            {

            }
            return pRet;
        }
    }
}
