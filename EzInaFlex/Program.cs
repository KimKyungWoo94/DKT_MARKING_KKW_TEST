
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna
{

		static class Program
		{
				/// <summary>
				/// 해당 응용 프로그램의 주 진입점입니다.
				/// </summary>
				[STAThread]
				static void Main()
				{
						bool bMutexFlag = false;
						System.Threading.Mutex pMutex = new System.Threading.Mutex(true, "EzInaFlex", out bMutexFlag);
						if (bMutexFlag)
						{
								Application.EnableVisualStyles();
								Application.SetCompatibleTextRenderingDefault(false);
								EzInaVisionLibrary.VisionLibEuresys.SelectLicensingModel(EzInaVisionLibrary.VisionLibEuresys.eLicensingModel.LegacyDongle);
								EzIna.FA.FRM.FrmMainForm = new FrmMain();
								Application.Run(EzIna.FA.FRM.FrmMainForm);
								pMutex.ReleaseMutex();
						}
						else
						{
								MessageBox.Show("EzInaFlex Already Executing", "Error",
										MessageBoxButtons.OK, MessageBoxIcon.Error);
						}

				}
		}
}
