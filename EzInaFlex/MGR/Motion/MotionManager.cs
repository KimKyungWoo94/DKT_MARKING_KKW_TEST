using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace EzIna.Motion
{
		public class MotionManager
		{

				public int iSelectedAxisNo { get; set; }
				public GDMotion.eMotionBrand eSelectedMotionBrand { get; set; }
				public int nAxisCounts
				{
						get
						{
								if (m_vecMotions.Count >= 0)
								{
										return m_vecMotions.Count;
								}
								else
								{
										return 0;
								}
						}
				}
				List<MotionInterface> m_vecMotions = null;
				Dictionary<Type, MotionInterface> m_DicMotionDevice = null;
				//        MotionAero m_MotionAerotech = null;
				//	Dictionary<int, string> m_dicAxisInfor = null;
				int m_nAxisCounts;                          // 제어 가능한 축갯수 선언 및 초기화		
				public GDMotion.stGantryInfo m_stGantryInfo;
				public GDMotion.stGantryInfo m_stGantryStatus;



				public MotionManager()
				{
						m_vecMotions = new List<MotionInterface>();
						m_DicMotionDevice = new Dictionary<Type, MotionInterface>();
						m_nAxisCounts = 0;

						m_stGantryInfo = new GDMotion.stGantryInfo();
				}
				~MotionManager()
				{
				}

				public void Terminate()
				{
						try
						{
#if !_SIMULATION
								DeleteItems();
#endif
						}
						catch (Exception ex)
						{

						}

				}
				public bool AddItem(MotionInterface a_pItem)
				{
						if (a_pItem == null)
								return false;

						m_vecMotions.Add(a_pItem);

						return true;
				}

				private void DeleteItems()
				{
						foreach (MotionItem item in m_vecMotions)
						{
								item.Dispose();
						}
						if (m_vecMotions.Count > 0)
						{
								m_vecMotions.Clear();
								m_vecMotions = null;
						}
				}
				public void OpenDevice()
				{
						m_vecMotions.Clear();
						MotionAxisDataPaser Paser = null;
						FileInfo XmlFileInfo = new FileInfo(EzIna.Motion.GDMotion.ConfigMotionAxisDataFileFullname);
						if (XmlFileInfo.Exists == true)
						{
								Paser = MotionAxisDataPaser.Load(XmlFileInfo.FullName);
						}
						else
						{
								Paser = new MotionAxisDataPaser();
								Paser.Save(XmlFileInfo.FullName);
						}
						int iIDX = -1;
						foreach (MotionAxisLinkData Item in Paser.AxisDataLinkList)
						{
								switch (Item.strDeviceType)
								{
										case "EzIna.Motion.CMotionA3200":
												{
														if (EzIna.Motion.CMotionA3200.bInitialized == false)
																EzIna.Motion.CMotionA3200.InitializeDriver();
														Int32.TryParse(Item.strAxisID, out iIDX);
														// if(EzIna.Motion.CMotionA3200.m_DicListAxises.Count>0 &&
														//     EzIna.Motion.CMotionA3200.m_DicListAxises.ContainsKey(Item.strAxisID)                  
														if (EzIna.Motion.CMotionA3200.m_listAxises.Count > 0 && iIDX < EzIna.Motion.CMotionA3200.m_listAxises.Count)
														{
																m_vecMotions.Add(EzIna.Motion.CMotionA3200.m_listAxises[iIDX]);
														}
												}
												break;
										case "EzIna.Motion.CMotionAXT":
												{
														if (EzIna.Motion.CMotionAXT.bInitialized == false)
																EzIna.Motion.CMotionAXT.InitializeDriver(true);
														Int32.TryParse(Item.strAxisID, out iIDX);
														if (EzIna.Motion.CMotionAXT.plistAxies.Count > 0 && iIDX < EzIna.Motion.CMotionAXT.plistAxies.Count)
														{
																EzIna.Motion.CMotionAXT pMotion=EzIna.Motion.CMotionAXT.plistAxies[iIDX] as EzIna.Motion.CMotionAXT;
																if(pMotion!=null)
																{
																		pMotion.strAxisName = Item.strAxisName;
																		pMotion.OpenMotionSpeedProfile();
																		m_vecMotions.Add(pMotion);
																}																
														}
												}
												break;
								}
						}
				}
				public void TestFileSave()
				{
						MotionAxisDataPaser Paser = new MotionAxisDataPaser();
						string SavePath = EzIna.Motion.GDMotion.ConfigMotionAxisDataFileFullname;
						Paser.AddLinkData("0", typeof(EzIna.Motion.CMotionA3200).FullName);
						Paser.AddLinkData("1", typeof(EzIna.Motion.CMotionA3200).FullName);
						Paser.AddLinkData("2", typeof(EzIna.Motion.CMotionA3200).FullName);
						Paser.AddLinkData("3", typeof(EzIna.Motion.CMotionA3200).FullName);
						Paser.Save(SavePath);
				}
				public MotionInterface GetItem(int a_iAxis)
				{
						if (a_iAxis < 0 || a_iAxis >= m_vecMotions.Count)
								return null;
						return m_vecMotions[a_iAxis];
				}

				public void ApplyConfig_AllAxisSetting()
				{
						foreach (MotionInterface item in m_vecMotions)
						{
								if (item == null)
										continue;
								MotionItem VariableItem = item as MotionItem;
								VariableItem.SaveConfiguration(false);
								VariableItem.SaveMotionSpeedProfile();
						}
				}


				public void TreeView_Clear(TreeView a_pTreeView_Motors)
				{
						a_pTreeView_Motors.Nodes.Clear();
				}
				public void TreeView_Init(TreeView a_pTreeView_Motors)
				{

						a_pTreeView_Motors.Nodes.Clear();
						List<string> strBrandList = new List<string>();
						strBrandList.Clear();
						if (m_vecMotions.Count <= 0) return;
						TreeNode pNode = null;
						TreeNode pChild = null;
						foreach (MotionInterface item in m_vecMotions)
						{
								pNode = null;
								pChild = null;
								switch (item.DeviceType.Name)
								{
										case "CMotionAXT":
												{
														if (strBrandList.Contains("AJINEXTEK") == false)
														{
																strBrandList.Add("AJINEXTEK");
																a_pTreeView_Motors.Nodes.Add("MotionAXT", "AJINEXTEK");

														}
														pNode = a_pTreeView_Motors.Nodes["MotionAXT"];
														goto Create;
												}
												break;
										case "CMotionA3200":
												{
														if (strBrandList.Contains("AEROTECH") == false)
														{
																strBrandList.Add("AEROTECH");
																a_pTreeView_Motors.Nodes.Add("MotionA3200", ("AEROTECH"));
														}
														pNode = a_pTreeView_Motors.Nodes["MotionA3200"];
														goto Create;
												}
												break;
										default:
												{

												}
												break;


								}
								Create:
								{

										if (pNode != null)
										{
												MotionItem ItemVariable = item as MotionItem;
												pNode.ImageIndex = 2;
												pNode.SelectedImageIndex = 2;
												pChild = pNode.Nodes.Add(ItemVariable.iAxisNo.ToString(), string.Format("{0:D2}: {1}", ItemVariable.iAxisNo, ItemVariable.strAxisName), 0);
												pChild.Tag = item;
												pChild.ImageIndex = 0;
												pNode.SelectedImageIndex = 0;
										}

								}
						}//end foreach


				}


				public bool OpenGantry()
				{
						IniFile Ini = new IniFile(GDMotion.ConfigMotionGantryIniFileFullName);

						m_stGantryInfo.bGantryUse = Ini.Read(GDMotion.GANTRY_SECTION_NAME, "GantryUse", false);
						m_stGantryInfo.iSlaveHomeUse = (uint)Ini.Read(GDMotion.GANTRY_SECTION_NAME, "SlaveHomeUse", 1);
						m_stGantryInfo.fSlaveHomeOffset = Ini.Read(GDMotion.GANTRY_SECTION_NAME, "SlaveHomeOffset", 0.0);
						m_stGantryInfo.fSlaveHomeRange = Ini.Read(GDMotion.GANTRY_SECTION_NAME, "SlaveHomeRange", 1000.0);
						m_stGantryInfo.iMasterAxis = Ini.Read(GDMotion.GANTRY_SECTION_NAME, "MasterAxis", 0);
						m_stGantryInfo.iSlaveAxis = Ini.Read(GDMotion.GANTRY_SECTION_NAME, "SlaveAxis", 1);


						return true;
				}
				public bool SaveGantry()
				{
						IniFile Ini = new IniFile(GDMotion.ConfigMotionGantryIniFileFullName);

						Ini.Write(GDMotion.GANTRY_SECTION_NAME, "GantryUse", m_stGantryInfo.bGantryUse);
						Ini.Write(GDMotion.GANTRY_SECTION_NAME, "SlaveHomeUse", m_stGantryInfo.iSlaveHomeUse);
						Ini.Write(GDMotion.GANTRY_SECTION_NAME, "SlaveHomeOffset", m_stGantryInfo.fSlaveHomeOffset);
						Ini.Write(GDMotion.GANTRY_SECTION_NAME, "SlaveHomeRange", m_stGantryInfo.fSlaveHomeRange);
						Ini.Write(GDMotion.GANTRY_SECTION_NAME, "MasterAxis", m_stGantryInfo.iMasterAxis);
						Ini.Write(GDMotion.GANTRY_SECTION_NAME, "SlaveAxis", m_stGantryInfo.iSlaveAxis);

						return true;
				}
				public void GetMotionStatus()
				{
						if (m_vecMotions == null)
								return;
						if (m_vecMotions.Count < 1)
								return;


						for (int i = 0; i < m_vecMotions.Count; i++)
						{
								m_vecMotions[i].ReadPositionStatus();
								m_vecMotions[i].ReadMotorStatus();
								m_vecMotions[i].HomeReadStatus();
						}
				}

				public int GetAxesCount()
				{
						if (m_vecMotions == null)
								return -1;

						return m_vecMotions.Count + (int)GDMotion.eMotorNameAero.MAX;
				}

				public void AllEStop()
				{
						if (m_vecMotions != null && m_vecMotions.Count > 0)
						{
								for (int i = 0; i < m_vecMotions.Count; i++)
								{
										m_vecMotions[i].E_STOP();
								}
						}
				}

				public void AllSDStop()
				{
						if (m_vecMotions != null && m_vecMotions.Count > 0)
						{
								for (int i = 0; i < m_vecMotions.Count; i++)
								{
										m_vecMotions[i].SD_STOP();
								}
						}
				}

				public XmlSchema GetSchema()
				{
						throw new NotImplementedException();
				}

				public void ReadXml(XmlReader reader)
				{
						throw new NotImplementedException();
				}

				public void WriteXml(XmlWriter writer)
				{
						throw new NotImplementedException();
				}
		} //end of class


}// end of name space

