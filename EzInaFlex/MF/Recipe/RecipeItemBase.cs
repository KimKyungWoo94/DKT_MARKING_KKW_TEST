using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.MF
{
		public abstract class RecipeItemBase : GUI.NotifyProperyChangedBase
		{
				public delegate double GetDoubleValue();
				public delegate object GetTargetValue();
				protected FA.DEF.eRecipeCategory m_eMainRcpCategory;
				public FA.DEF.eRecipeCategory eMainRcpCategory
				{
						get { return m_eMainRcpCategory; }
				}
				protected string m_strSubCategory;
				public string strSubCategory
				{
						get { return m_strSubCategory; }
				}
				protected string m_strCaption;
				public string strCaption
				{
						get { return m_strCaption; }
						set { m_strCaption = value; }
				}
				protected int m_iKey;
				public int iKey
				{
						get { return m_iKey; }
				}
				protected FA.DEF.eUnit m_eValueUnit;
				public FA.DEF.eUnit eValueUnit
				{
						get
						{
								return m_eValueUnit;
						}
						set
						{
								m_eValueUnit = value;
						}
				}
				protected bool m_bIsInitialProc;
				public bool bIsInitialProc
				{
						get { return m_bIsInitialProc; }
						set { m_bIsInitialProc = value; }
				}
				public Type ValueType
				{
						get { return m_Value.GetType(); }
				}
				protected object m_Value;
				public object Value
				{
						get { return m_Value; }
						set
						{
								try
								{
										SetValue(value);
								}
								catch (Exception Ex)
								{
										throw new Exception(Ex.Message, Ex);
								}
						}
				}
				protected uint m_iFormatNumofZero = 0;

				public uint iFormatNumberOfZero
				{
						get { return m_iFormatNumofZero; }
						set { m_iFormatNumofZero = value; }
				}
				public bool IsSettingValueChanged
				{
						get { return !m_Value.Equals(m_SettingValue); }
				}

				protected object m_SettingValue;
				public object SettingValue
				{
						get { return m_SettingValue; }
						set
						{
								try
								{
										SetSettingValue(value);
								}
								catch (Exception Ex)
								{
										throw new Exception(Ex.Message, Ex);
								}
						}
				}
				public T GetValue<T>()
				{
						try
						{
								Type pOutBufferType = typeof(T);
								if (typeof(T).IsValueType)
								{
										if (pOutBufferType == m_Value.GetType())
										{
												return (T)m_Value;
										}
										else
										{
												return (T)Convert.ChangeType(m_Value, pOutBufferType);
										}
								}
								else
								{
										throw new Exception("Isn't Value Type Buffer");
								}
						}
						catch (Exception Ex)
						{
								throw new Exception(Ex.Message, Ex);
						}
				}

				public T GetSettingValue<T>()
				{
						try
						{
								Type pOutBufferType = typeof(T);
								if (typeof(T).IsValueType)
								{
										if (pOutBufferType == m_SettingValue.GetType())
										{
												return (T)m_SettingValue;
										}
										else
										{
												return (T)Convert.ChangeType(m_SettingValue, pOutBufferType);
										}
								}
								else
								{
										throw new Exception("Isn't Value Type Buffer");
								}
						}
						catch (Exception Ex)
						{
								throw new Exception(Ex.Message, Ex);
						}
				}
				public void GetSettingValue<T>(out T a_outvalue)
				{
						try
						{
								Type pOutBufferType = typeof(T);
								if (typeof(T).IsValueType)
								{
										if (pOutBufferType == m_SettingValue.GetType())
										{
												a_outvalue = (T)m_SettingValue;
										}
										else
										{
												a_outvalue = (T)Convert.ChangeType(m_SettingValue, pOutBufferType);
										}
								}
								else
								{
										throw new Exception("Isn't Value Type Buffer");
								}
						}
						catch (Exception Ex)
						{
								throw new Exception(Ex.Message, Ex);
						}
				}
				public void SyncValueFromSettingValue()
				{
						if (IsSettingValueChanged)
						{
								Value = m_SettingValue;
						}
				}
				protected virtual void SetSettingValue(object a_Value)
				{
						try
						{
								Type InValueType = a_Value.GetType();
								if (InValueType.IsValueType || InValueType == typeof(string))
								{
										if (a_Value.GetType() == InValueType)
										{
												CheckPropertyChanged<object>("SettingValue", ref m_SettingValue, a_Value);

										}
										else
										{
												if (m_SettingValue.GetType().BaseType == typeof(System.Enum))
												{
														if (a_Value.GetType() == typeof(string))
														{
																this.CheckPropertyChanged<object>("SettingValue", ref m_SettingValue, Enum.Parse(m_Value.GetType(), a_Value as string));
														}
												}
												else
												{
														CheckPropertyChanged<object>("SettingValue", ref m_SettingValue, Convert.ChangeType(a_Value, m_Value.GetType()));
												}
										}
								}
								else
								{
										throw new Exception("Value Not Support Type (isn't string or Value type");
								}
						}
						catch (Exception Ex)
						{
								throw new Exception(Ex.Message, Ex);
						}
				}

				protected virtual void SetValue(object a_Value)
				{
						try
						{
								Type InValueType = a_Value.GetType();
								if (InValueType.IsValueType || InValueType == typeof(string))
								{
										if (a_Value.GetType() == InValueType)
										{
												CheckPropertyChanged<object>("Value", ref m_Value, a_Value);
										}
										else
										{
												if (m_SettingValue.GetType().BaseType == typeof(System.Enum))
												{
														if (a_Value.GetType() == typeof(string))
														{
																this.CheckPropertyChanged<object>("Value", ref m_SettingValue, Enum.Parse(m_Value.GetType(), a_Value as string));

														}
												}
												else
												{
														CheckPropertyChanged<object>("Value", ref m_Value, Convert.ChangeType(a_Value, m_Value.GetType()));
												}
										}
								}
								else
								{
										throw new Exception("Value Not Support Type (isn't string or Value type");
								}
						}
						catch (Exception Ex)
						{
								throw new Exception(Ex.Message, Ex);
						}
				}
				public void GetValue<T>(out T a_outvalue)
				{
						try
						{
								Type pOutBufferType = typeof(T);
								if (typeof(T).IsValueType)
								{
										if (pOutBufferType == m_Value.GetType())
										{
												a_outvalue = (T)m_Value;
										}
										else
										{
												a_outvalue = (T)Convert.ChangeType(m_Value, pOutBufferType);
										}
								}
								else
								{
										throw new Exception("Isn't Value Type Buffer");
								}
						}
						catch (Exception Ex)
						{
								throw new Exception(Ex.Message, Ex);
						}
				}
				private GetDoubleValue m_fMinDel = delegate { return 0.0f; };


				public double fMin
				{
						get { return m_fMinDel(); }

				}
				private GetDoubleValue m_fMaxDel = delegate { return 0.0f; };
				public double fMax
				{
						get { return m_fMaxDel(); }
				}
				public GetDoubleValue fMinDelegate
				{
						set { m_fMinDel = value; }
						get { return m_fMinDel; }
				}
				public GetDoubleValue fMaxDelegate
				{
						set { m_fMaxDel = value; }
						get { return m_fMaxDel; }
				}
				private GetTargetValue m_SetButtonDel;
				public GetTargetValue SetButtonFunc
				{
						set { m_SetButtonDel = value; }
						get { return m_SetButtonDel; }
				}

				public abstract double DisplayValue { get; set; }
				public abstract double DisplaySettingValue { get; set; }
				public string GetValueFormatString(uint a_NumberOfZero, bool bDataGridView)
				{
						return GetFormatString(a_NumberOfZero, FA.DEF.GetUnitString(m_eValueUnit), bDataGridView);
				}
				protected string GetFormatString(uint a_NumberOfZero, string a_strFormatUnit, bool bDataGridView)
				{
						if (m_Value != null)
						{
								if (m_eValueUnit != FA.DEF.eUnit.none)
								{
										StringBuilder strFormatBuilder = new StringBuilder();
										string strRet = "";
										switch (Type.GetTypeCode(m_Value.GetType()))
										{
												case TypeCode.Int16:
												case TypeCode.UInt16:
												case TypeCode.Int32:
												case TypeCode.UInt32:
												case TypeCode.Int64:
												case TypeCode.UInt64:
														{
																strFormatBuilder.Append("{0");
																if (a_NumberOfZero > 0)
																{
																		strFormatBuilder.Append(string.Format(":D{0}", a_NumberOfZero));
																}
																strFormatBuilder.Append("}");
																strFormatBuilder.Append(string.Format(" {0}", a_strFormatUnit));
																strRet = bDataGridView == false ? strFormatBuilder.ToString() : string.Format(strFormatBuilder.ToString(), 0);
														}
														break;
												case TypeCode.Single:
												case TypeCode.Double:
														{
																strFormatBuilder.Append("{0");
																if (a_NumberOfZero > 0)
																{
																		strFormatBuilder.Append(string.Format(":F{0}", a_NumberOfZero));
																}
																strFormatBuilder.Append("}");
																strFormatBuilder.Append(string.Format(" {0}", a_strFormatUnit));
																strRet = bDataGridView == false ? strFormatBuilder.ToString() : string.Format(strFormatBuilder.ToString(), 0.0);
														}
														break;
												default:
														{
																strRet = "";
														}
														break;
										}

										return strRet;
								}
						}
						return "";
				}
				public virtual void WriteFileSave(StreamWriter Writer)
				{
						if (Writer != null)
						{
								//		1		2		3		  4		   5	   6	  7	       8
								//eCategory | Key | Category | Caption | Value | eUnit | Axis | ValueType
								string fmtP = " | {0:D3} | {1} | {2} | {3} | {4} | {5:D3} | {6}  ";
								Writer.WriteLine(
								 this.eMainRcpCategory.ToString() //1 
								 + fmtP,
									this.iKey, //2
									this.strSubCategory,        //3
									this.strCaption,            //4
								 Convert.ToString(this.Value,CultureInfo.InvariantCulture),      //5 Convert.ToString(this.Value,CultureInfo.InvariantCulture), 
									this.eValueUnit.ToString(), //6
									0,               //7
									this.ValueType.FullName //8

								);
						}
				}

				protected RecipeItemBase(FA.DEF.eRecipeCategory a_MainCategory, string a_strSubCategory, string a_strCaption, int a_iKey, object a_InitValue, FA.DEF.eUnit a_ValueUnit)
				{
						m_eMainRcpCategory = a_MainCategory;
						m_strSubCategory = a_strSubCategory;
						m_strCaption = a_strCaption;
						m_Value = a_InitValue;
						m_SettingValue = a_InitValue;
						m_eValueUnit = a_ValueUnit;
						m_iKey = a_iKey;
				}
		}
}
