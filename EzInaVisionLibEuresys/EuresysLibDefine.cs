using Euresys.Open_eVision_2_14;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzInaVisionLibrary
{
	public sealed partial class VisionLibEuresys
    {
        private bool CheckOrCreateDir(string a_strDirPath)
        {
            if (!Directory.Exists(a_strDirPath))
            {
                try
                {
                    Directory.CreateDirectory(a_strDirPath);
                }
                catch
                {
                    MessageBox.Show("Failed to create Aging Directory !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private bool DeleteFilesInDir(string a_strDirPath, int a_nLineRegion)
        {
            string[] files = Directory.GetFiles(a_strDirPath);

            string strDeleteKey = "";
            strDeleteKey = "DL_" + a_nLineRegion.ToString();
            foreach (string file in files)
            {
                if (file.Contains(strDeleteKey))
                    File.Delete(file);
            }

            return false;
        }
    }

}
