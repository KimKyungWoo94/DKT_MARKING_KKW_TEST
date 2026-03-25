using EzInaVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euresys.Open_eVision_2_14;

namespace EzInaVisionLibrary
{
	public sealed partial class VisionLibEuresys : VisionLibBaseClass
	{
		object lockAF = new object();
		public float AutoFocusing(EzInaVision.GDV.eRoiItems a_eRoiItem, bool a_bSave = false, string a_strFileName = "")
		{
			try
			{
				lock(lockAF)
				{
					
					EROIBW8 eSearchingROI = null;
					m_dicRoisForInspectionBW8.TryGetValue((int)a_eRoiItem, out eSearchingROI);
					if(eSearchingROI == null)
						throw new Exception("eSearchingROI is null");
					if (a_bSave)
					{
						eSearchingROI.Save(a_strFileName, EImageFileType.Bmp);
					}
					AttachOneROIToTheSrcImg( EzInaVision.GDV.eImageType.Regular, a_eRoiItem);
					
					return EasyImage.Focusing(eSearchingROI);
				}
			}
			catch (System.Exception ex)
			{
				MsgBox.Error(ex.Message);
				return 0.0f;
			}
			
		}
	}
}
