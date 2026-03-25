using Euresys.Open_eVision_2_14;
using EzInaVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzInaVisionLibrary
{
	public sealed partial class VisionLibEuresys : VisionLibBaseClass
	{
		EImageBW8 m_eFilteredImgBW8;
		public override void FiltersInit()
		{
			m_eFilteredImgBW8 = new EImageBW8();
        }
		public override void FiltersTerminate()
		{
			if(m_eFilteredImgBW8 != null)
			{
				m_eFilteredImgBW8.Dispose();
				m_eFilteredImgBW8 = null;
			}
		}
		public override void ThresholdOfFiltersWithoutCam()
		{
			try
			{
				if (m_eProcessImgBW8.IsVoid)
					return;
				if (m_eFilteredImgBW8.IsVoid)
					return;
				
				EasyImage.Threshold(m_eProcessImgBW8, m_eFilteredImgBW8, base.m_LibInfo.m_Filterconfig.m_nThresHoldValue);
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().ReflectedType.FullName.ToString()
					+ MethodBase.GetCurrentMethod().Name.ToString());
			}
			finally
			{
			}
		}
		public override void ThresholdOfFilters ()
		{
			try
			{
				if (m_eProcessImgBW8.IsVoid)
					return;
				if (m_eFilteredImgBW8.IsVoid)
					return;

				EasyImage.Threshold(m_eProcessImgBW8, m_eFilteredImgBW8, base.m_LibInfo.m_Filterconfig.m_nThresHoldValue);
			}
			catch (Exception ex)
			{
				
				MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().ReflectedType.FullName.ToString() 
					+ MethodBase.GetCurrentMethod().Name.ToString());
			}
			finally
			{
			}
		}
		public override void OpenDiskOfFilters()
		{
			try
			{
				if (m_eFilteredImgBW8.IsVoid)
					return;

				EasyImage.OpenDisk(m_eFilteredImgBW8, m_eFilteredImgBW8, base.m_LibInfo.m_Filterconfig.m_nOpenDiskValue);
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().ReflectedType.FullName.ToString()
					+ MethodBase.GetCurrentMethod().Name.ToString());
			}
			finally
			{
			}
		}
		public  override void CloseDiskOfFilters()
		{
			try
			{
				if (m_eFilteredImgBW8.IsVoid)
					return;
				EasyImage.CloseDisk(m_eFilteredImgBW8, m_eFilteredImgBW8, base.m_LibInfo.m_Filterconfig.m_nCloseDiskValue);
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().ReflectedType.FullName.ToString()
					+ MethodBase.GetCurrentMethod().Name.ToString());
			}
			finally
			{
			}
		}
		public override void ErodeDiskOfFilters()
		{
			try
			{
				if (m_eFilteredImgBW8.IsVoid)
					return;

				EasyImage.ErodeDisk(m_eFilteredImgBW8, m_eFilteredImgBW8, base.m_LibInfo.m_Filterconfig.m_nErodeDiskValue);
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().ReflectedType.FullName.ToString()
					+ MethodBase.GetCurrentMethod().Name.ToString());
			}
			finally
			{
			}
		}
		public override  void DilateDiskOfFilters()
		{
			try
			{
				if (m_eFilteredImgBW8.IsVoid)
					return;

				EasyImage.DilateDisk(m_eFilteredImgBW8, m_eFilteredImgBW8, base.m_LibInfo.m_Filterconfig.m_nDilateDiskValue);
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().ReflectedType.FullName.ToString()
					+ MethodBase.GetCurrentMethod().Name.ToString());
			}
			finally
			{
			}
		}
	}
}
