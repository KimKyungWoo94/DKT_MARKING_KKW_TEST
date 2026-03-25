using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EzIna.Laser
{

    [Serializable]
    [XmlRoot("LaserPowerTables")]  
    public class LaserPowerTablePaser:IXmlSerializable
    {

        private List<LaserPwrTableData> m_TableList=new List<LaserPwrTableData>();
        public List<LaserPwrTableData> DataLinkList {get { return m_TableList;} }

        public LaserPwrTableData this[int a_IDX]
        {
            get
            {
                if (a_IDX >= 0 && a_IDX < m_TableList.Count)
                {
                    return m_TableList[a_IDX];
                }
                return null;
            }
        }
				
        public int ListCount
        {
            get {  return m_TableList.Count;}
        }
				public bool CheckDuplicationTable(int a_iDefaultFreqency)
				{
						return m_TableList.Any(item => item.iDefaultFrequency ==a_iDefaultFreqency);
			  }
        public bool AddTableData(LaserPwrTableData a_Table)
        {
            if(CheckDuplicationTable(a_Table.iDefaultFrequency)==false)
            {
                m_TableList.Add(a_Table);
								return true;
            }
						return false;
        }
				public bool OverWriteTableData(LaserPwrTableData a_Table)
				{
						if (CheckDuplicationTable(a_Table.iDefaultFrequency) == true)
						{
								var list=m_TableList.Select(item=>item.iDefaultFrequency).ToList();
						}
						return false;
				}

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(LaserPowerTablePaser)); 
						StreamWriter stream=null;           
						try
						{
								using (stream = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
								{
										serializer.Serialize(stream, this);
								}

						}
						catch(Exception ex)
						{
								stream?.Close();
						}

				}
        public static LaserPowerTablePaser Load(string path)
        {          
            var serializer = new XmlSerializer(typeof(LaserPowerTablePaser));
						StreamReader stream=null;
						try
						{								
								using (stream = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.UTF8))
								{
										return serializer.Deserialize(stream) as LaserPowerTablePaser;
							  }
						}
						catch
						{								
								stream?.Close();
						}
						return null;
   			}
        public XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
            reader.ReadToFollowing("LaserPowerTableList");            
            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
            {
                 ReadXmlListData(reader);
            }
        }
        private void ReadXmlListData(XmlReader reader)
        {
            m_TableList.Clear();                    
            while(reader.ReadToFollowing("LaserPowerTable"))
            {                
               if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
               {
                    LaserPwrTableData ptemp=new LaserPwrTableData();
                    ptemp.ReadXml(reader);
                    m_TableList.Add(ptemp);
                }              
            }           
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteComment("LaserPowerTable : DefaultFrequency = Value");
            writer.WriteComment("TableItem : Percent = Value , Power = Value");                  
            writer.WriteStartElement("LaserPowerTableList");
            foreach(LaserPwrTableData item in m_TableList)
            {                
                item.WriteXml(writer);
            }
            writer.WriteEndElement();
        }
    }

    [Serializable]    
    public class LaserPwrTableData:IXmlSerializable,INotifyPropertyChanged,ICloneable
    {

				
				public event PropertyChangedEventHandler PropertyChanged;
				public enum emFunctionError_t
				{
						Success = 1,
						PathError,
						FileExtensionError,
						NoData,
						MinValueError,
						MaxValueError,
						Fail,
				}
				public string GetReturnMessage(int ErrorCode)
				{
						string strReturn = string.Empty;

						switch (ErrorCode)
						{
								case (int)emFunctionError_t.Success:
										strReturn = "Success";
										break;
								case (int)emFunctionError_t.PathError:
										strReturn = "PowerTable File Path Error";
										break;
								case (int)emFunctionError_t.FileExtensionError:
										strReturn = "PowerTable File Extension Error";
										break;
								case (int)emFunctionError_t.NoData:
										strReturn = "No Data";
										break;
								case (int)emFunctionError_t.MinValueError:
										strReturn = "MinValue Error";
										break;
								case (int)emFunctionError_t.MaxValueError:
										strReturn = "MaxValue Error";
										break;
								case (int)emFunctionError_t.Fail:
										strReturn = "Fail";
										break;
						}

						return strReturn;
				}
				int  m_iDefaultFrequency;

				List<LaserPowerTableItem> m_TableList;
				private Dictionary<double, double> m_dicPowerCurve;
				const int STEP = 100;
		    public LaserPwrTableData()
        {
						m_TableList=new List<LaserPowerTableItem>();
						m_dicPowerCurve=new Dictionary<double, double>();
        }
        public LaserPwrTableData(int a_iDefaultFreQ)
        {
						m_iDefaultFrequency=a_iDefaultFreQ;
            m_TableList=new List<LaserPowerTableItem>();      
						m_dicPowerCurve=new Dictionary<double, double>();
        }
				public bool CheckDuplicationPercentData(double a_Percent)
				{
						return m_TableList.Any(item=>item.PercentValue==a_Percent);
				}
				public int iDefaultFrequency
				{
						get { return m_iDefaultFrequency;}
						set
						{
								this.CheckPropertyChanged("iDefaultFrequency",ref m_iDefaultFrequency,value);
						}
				}
				public bool AddTable(double a_Percent,double a_fPower)
				{
						if(CheckDuplicationPercentData(a_Percent)==false)
						{
								m_TableList.Add(new LaserPowerTableItem(a_Percent,a_fPower));
						}
						return false;
				}
				public void ClearTables()
				{
						m_TableList.Clear();
				}
				public List<LaserPowerTableItem> TableItemList
				{
						get { return m_TableList;}
				}
				public List<KeyValuePair<double, double>> PowerCurveTableData
				{
						get { return m_dicPowerCurve.ToList();}
				}
				public double MinimumPower
				{
						get { return m_TableList.Min(item=>item.PowerValue);}
				}
				public double MaximumPower
				{
						get { return m_TableList.Max(item => item.PowerValue); }
				}
				public double MinimumPercent
				{
						get { return m_TableList.Min(item => item.PercentValue); }
				}
				public double MaximumPercent
				{
						get { return m_TableList.Max(item => item.PercentValue); }
				}
				
				public int GetPercentFromPower(double a_fPower,out double a_fPercentBuf)
				{
						int iResult = -1;
						a_fPercentBuf = -1.0;

						try
						{
								double MaxPower = 0.0;
								double MinPower = 0.0;

								if (m_TableList.Count < 2)
										return (int)emFunctionError_t.NoData;


								MinPower =m_TableList.Min(item=>item.PowerValue);
								MaxPower=m_TableList.Max(item=>item.PowerValue);
						
								MaxPower = Math.Round(MaxPower, 6);
								MinPower = Math.Round(MinPower, 6);

								if (MaxPower < a_fPower) return (int)emFunctionError_t.MaxValueError;
								if (MinPower > a_fPower) return (int)emFunctionError_t.MinValueError;

							  Dictionary<double,double> pTable=new Dictionary<double, double>();
								foreach(LaserPowerTableItem item in m_TableList)
								{
										pTable.Add(item.PercentValue*100,item.PowerValue);
								}

								// 보간테이블 생성
								if(this.CreateSplineInterpolationData(pTable)==(int)emFunctionError_t.Success)
								{

										var PittingList = this.m_dicPowerCurve.ToList();
										for (int i = 0; i < PittingList.Count - 1; i++)
										{
												if (PittingList[i].Value >= a_fPower)
												{
														a_fPercentBuf = PittingList[i - 1].Key + (a_fPower - PittingList[i - 1].Value) *
																((PittingList[i].Key - PittingList[i - 1].Key) / (PittingList[i].Value - PittingList[i - 1].Value));
														a_fPercentBuf=a_fPercentBuf/100.0;
														iResult = (int)emFunctionError_t.Success;
														break;
												}
										}
										/*
										foreach (KeyValuePair<double, double> temp in this.m_dicPowerCurve)
										{
												// For Test
												//WritePrivateProfileString(iFreq_kHz.ToString(), temp.Key.ToString(), temp.Value.ToString(), path);

												if (Math.Abs(temp.Value - a_fPower) <= 0.01)
												{
														a_fPercentBuf = temp.Key;
														a_fPercentBuf=a_fPercentBuf/100.0;
														iResult = (int)emFunctionError_t.Success;
														break;
												}
										}*/
										iResult = (int)emFunctionError_t.Success;
								}
								// For Test
								//string path = System.IO.Directory.GetCurrentDirectory() + "\\dicPowerCurve.txt";

						}
						catch (Exception ex)
						{

						}

						return iResult;
				}

				public int GetPowerFromPercent(double a_fPercecnt, out double a_fPowerBuf)
				{
						int iResult = -1;
						a_fPowerBuf = -1.0;

						try
						{
								double Max = 0.0;
								double Min = 0.0;
								double fSearchPercent;
								if (m_TableList.Count < 2)
										return (int)emFunctionError_t.NoData;


								Min = m_TableList.Min(item => item.PercentValue);
								Max = m_TableList.Max(item => item.PercentValue);

								Max = Math.Round(Max, 6);
								Min = Math.Round(Min, 6);

								if (Max < a_fPercecnt) return (int)emFunctionError_t.MaxValueError;
								if (Min > a_fPercecnt) return (int)emFunctionError_t.MinValueError;

								Dictionary<double, double> pTable = new Dictionary<double, double>();
								foreach (LaserPowerTableItem item in m_TableList)
								{
										pTable.Add(item.PowerValue, item.PercentValue*100);
								}

								// 보간테이블 생성
								if (this.CreateSplineInterpolationData(pTable) == (int)emFunctionError_t.Success)
								{
										fSearchPercent= a_fPercecnt*100;
										var PittingList=this.m_dicPowerCurve.ToList();
										for(int i=0;i<PittingList.Count-1;i++)
										{
												if(PittingList[i].Value>=fSearchPercent)
												{
														a_fPowerBuf=PittingList[i-1].Key+(fSearchPercent-PittingList[i-1].Value)*
																((PittingList[i].Key-PittingList[i-1].Key)/(PittingList[i].Value-PittingList[i-1].Value));
														iResult = (int)emFunctionError_t.Success;
														break;
												}
										}
										/*foreach (KeyValuePair<double, double> temp in this.m_dicPowerCurve)
										{
												// For Test
												//WritePrivateProfileString(iFreq_kHz.ToString(), temp.Key.ToString(), temp.Value.ToString(), path);

												if (Math.Abs(temp.Value - fSearchPercent) <= 0.01)
												{
														a_fPowerBuf = temp.Key;
														iResult = (int)emFunctionError_t.Success;
														break;
												}
										}*/
										iResult = (int)emFunctionError_t.Success;
								}
								// For Test
								//string path = System.IO.Directory.GetCurrentDirectory() + "\\dicPowerCurve.txt";

						}
						catch (Exception ex)
						{

						}

						return iResult;
				}
				private int CreateSplineInterpolationData(Dictionary<double,double> a_DataTable)
				{
						try
						{
								
								if (a_DataTable.Count < 2)
										return (int)emFunctionError_t.NoData;

								double[] arr = new double[a_DataTable.Count];
								List<double> M = new List<double>(arr);
								if (!this.EstimateTangents(a_DataTable, ref M))
										return (int)emFunctionError_t.Fail;

								List<double> X = new List<double>();
								List<double> Y = new List<double>();

								foreach (KeyValuePair<double , double> Data in a_DataTable)
								{
										X.Add(Data.Key); Y.Add(Data.Value);
								}

								this.m_dicPowerCurve.Clear();
								this.m_dicPowerCurve.Add(X[0], Y[0]);
								double h;
								double t ;
								double t2;
								double t3;
								double h00;
								double h10;
								double h01;
								double h11;
								double value;
								double key;
								for (int i = 0; i < a_DataTable.Count - 1; i++)
								{
										h = X[i + 1] - X[i];

										for (int j = 1; j <= STEP; j++)
										{
												t = j / Convert.ToDouble(STEP);
												t2 = Math.Pow(t, 2);
												t3 = Math.Pow(t, 3);
												h00 = 2 * t3 - 3 * t2 + 1;
												h10 = t3 - 2 * t2 + t;
												h01 = -2 * t3 + 3 * t2;
												h11 = t3 - t2;

												value = h00 * Y[i] + h10 * h * M[i] + h01 * Y[i + 1] + h11 * h * M[i + 1];
												key = X[i] + t * h;
												this.m_dicPowerCurve.Add(key, value);
										}
								}

								return (int)emFunctionError_t.Success;
						}
						catch (Exception ex)
						{
								return (int)emFunctionError_t.Fail;
						}
				}
				private bool EstimateTangents(Dictionary<double, double> PowerTable, ref List<double> listTangnets)
				{
						try
						{
								int nSize = PowerTable.Count;
								List<double> delta = new List<double>();

								var PowerTable_sorted = from pair in PowerTable orderby pair.Key ascending select pair;

								KeyValuePair<double, double> Data1;
								KeyValuePair<double, double> Data2;
								// slopes;
								for (int i = 0; i < nSize - 1; i++)
								{
										Data1 = PowerTable_sorted.ElementAt(i);
										Data2 = PowerTable_sorted.ElementAt(i + 1);
										//delta[i] = (Data2.Value - Data1.Value) / (Data2.Key - Data1.Key);
										delta.Add((Data2.Value - Data1.Value) / (Data2.Key - Data1.Key));
								}

								// average tangent at data points
								listTangnets[nSize - 1] = delta[nSize - 2];
								for (int i = 1; i < nSize - 1; i++)
								{
										listTangnets[i] = (delta[i - 1] + delta[i]) / 2.0f;
								}
								double a;
								double b;
								double eps;
								for (int i = 0; i < nSize - 1; i++)
								{
										if (delta[i] == .0f)
												listTangnets[i] = listTangnets[i + 1] = 0;
										else
										{
												a = listTangnets[i] / delta[i];
												b = listTangnets[i + 1] / delta[i];
												eps = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
												if (eps > 3)
												{
														double t = 3 * delta[i] / eps;
														listTangnets[i] = (float)(t * a);
														listTangnets[i + 1] = (float)(t * b);
												}
										}
								}
								return true;
						}
						catch (Exception ex)
						{
								return false;
						}
				}
				#region XML Serialize
				public XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
						m_TableList.Clear();
						int    iTemp=0;
						double fTemp=0.0;
						double fTemp1=0.0;
            if(reader.HasAttributes)
            {
                 int.TryParse(reader.GetAttribute("DefaultFrequency"),out iTemp);
								 iDefaultFrequency=iTemp;         								       
            }
					using (var innerReader = reader.ReadSubtree())
					{
							while (innerReader.ReadToFollowing("TableItem"))
							{
									double.TryParse(innerReader.GetAttribute("Percent"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out fTemp);
									double.TryParse(innerReader.GetAttribute("Power"),   System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out fTemp1);
									if(CheckDuplicationPercentData(fTemp)==false)										
									m_TableList.Add(new LaserPowerTableItem(fTemp,fTemp1));
							}
					}
				}
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("LaserPowerTable");
						writer.WriteAttributeString("DefaultFrequency",m_iDefaultFrequency.ToString());
						for(int i=0;i<m_TableList.Count;i++)
						{
								writer.WriteStartElement("TableItem");
							writer.WriteAttributeString("Percent",m_TableList[i].PercentValue.ToString("F3", System.Globalization.CultureInfo.InvariantCulture));
							writer.WriteAttributeString("Power",m_TableList[i].PowerValue.ToString("000.000", System.Globalization.CultureInfo.InvariantCulture));
								writer.WriteEndElement();
						}
            writer.WriteEndElement();
        }
				#endregion
				#region INotifyPropertyChanged
				protected bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, T newValue)
				{
						if (oldValue == null && newValue == null)
						{
								return false;
						}

						if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
						{
								oldValue = newValue;

								FirePropertyChanged(propertyName);

								return true;
						}

						return false;
				}
				protected void FirePropertyChanged(string propertyName)
				{
						if (this.PropertyChanged != null)
						{
								this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
						}
				}


				#endregion
				public object Clone()
				{
						LaserPwrTableData pRet=new LaserPwrTableData(this.iDefaultFrequency);
						foreach(LaserPowerTableItem item in this.TableItemList)
						{
								pRet.AddTable(item.PercentValue,item.PowerValue);
						}
						return pRet;
				}
		}
		[Serializable]
		public class LaserPowerTableItem : INotifyPropertyChanged
		{

				public LaserPowerTableItem()
				{

				}
				public LaserPowerTableItem(double a_fPercent, double a_fPower)
				{
						PercentValue = a_fPercent;
						PowerValue = a_fPower;
				}
				public event PropertyChangedEventHandler PropertyChanged;
				double m_fPercent;
				double m_fPower;
				/// <summary>
				/// 0~1 
				/// </summary>
				public double PercentValuePer100
				{
						get { return m_fPercent * 100; }
				}
				public double PercentValue
				{
						get { return m_fPercent; }
						set
						{
								if (value >= 0 && value <= 1)
								{
										this.CheckPropertyChanged("PercentValue", ref m_fPercent, value);
								}
						}
				}

				public double PowerValue
				{
						get { return m_fPower; }
						set
						{
								this.CheckPropertyChanged("PowerValue", ref m_fPower, value);
						}
				}
				protected bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, T newValue)
				{
						if (oldValue == null && newValue == null)
						{
								return false;
						}

						if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
						{
								oldValue = newValue;

								FirePropertyChanged(propertyName);

								return true;
						}

						return false;
				}
				protected void FirePropertyChanged(string propertyName)
				{
						if (this.PropertyChanged != null)
						{
								this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
						}
				}
		}
}
