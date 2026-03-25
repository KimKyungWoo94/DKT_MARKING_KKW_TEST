using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace EzIna.Commucation
{
    /// <summary>
    /// 
    /// </summary>
	public class IniFile
	{
		#region property
        /// <summary>
        /// 
        /// </summary>
		public string FileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
		public int MaxBufferSize { get; set; }

		#endregion property

		#region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iniFileName"></param>
        /// <param name="maxBufferSize"></param>
		public IniFile(string iniFileName, int maxBufferSize = 255)
		{
			FileName = iniFileName;
			MaxBufferSize = maxBufferSize;
		}
		#endregion constructor

		#region API

		[DllImport("kernel32.dll")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string file);

		[DllImport("kernel32.dll")]
		private static extern long WritePrivateProfileString(string section, string key, string value, string file);

		#endregion API

		#region writeing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="sValue"></param>
		public void Write(string section, string key, string sValue)
		{
			WritePrivateProfileString(section, key, sValue, FileName);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="dValue"></param>
		public void Write(string section, string key, double dValue)
		{
			WritePrivateProfileString(section, key, dValue.ToString("F5"), FileName);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="color"></param>
		public void Write(string section, string key, Color color)
		{
			WritePrivateProfileString(section, key, ColorTranslator.ToHtml(color), FileName);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
		public void Write(string section, string key, TimeSpan defaultValue)
		{
			Write(section, key, defaultValue.Ticks);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="e"></param>
		public void Write<T>(string section, string key, T e)
		{
			WritePrivateProfileString(section, key, e.ToString(), FileName);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="o"></param>
		public void Write(string section, object o)
		{
			IList<PropertyInfo> props = new List<PropertyInfo>(o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty));

			foreach (PropertyInfo prop in props)
				if (prop.CanRead && prop.CanWrite && prop.GetCustomAttributes(true).Contains(BrowsableAttribute.Yes))
				{
					Write(section, prop.Name, prop.GetValue(o, null).ToString());
				}
		}

		#endregion writeing

		#region reading
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
		public string Read(string section, string key, string sDefault)
		{
			StringBuilder sb = new StringBuilder(MaxBufferSize);
			if (GetPrivateProfileString(section, key, sDefault, sb, MaxBufferSize, FileName) > 0)
				return sb.ToString();
			else
				return sDefault;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public int Read(string section, string key, int defaultValue)
		{
			return Convert.ToInt32(Read(section, key, defaultValue.ToString()));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public byte Read(string section, string key, byte defaultValue)
		{
			return Convert.ToByte(Read(section, key, defaultValue.ToString()));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public TimeSpan Read(string section, string key, TimeSpan defaultValue)
		{
			return TimeSpan.FromTicks(Read(section, key, defaultValue.Ticks));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public DateTime Read(string section, string key, DateTime defaultValue)
		{
			return Convert.ToDateTime(Read(section, key, defaultValue.ToString()));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public long Read(string section, string key, long defaultValue)
		{
			return Convert.ToInt64(Read(section, key, defaultValue.ToString()));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public double Read(string section, string key, double defaultValue)
		{
			return Convert.ToDouble(Read(section, key, defaultValue.ToString("F4")));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public bool Read(string section, string key, bool defaultValue)
		{
			return Convert.ToBoolean(Read(section, key, defaultValue.ToString()));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public Color Read(string section, string key, Color defaultValue)
		{
			return ColorTranslator.FromHtml(Read(section, key, ColorTranslator.ToHtml(defaultValue)));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
		public T Read<T>(string section, string key, T defaultValue)
		{
			return (T)Enum.Parse(typeof(T), Read(section, key, defaultValue.ToString()));
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="o"></param>
		public void Read(string section, object o)
		{
			IList<PropertyInfo> props = new List<PropertyInfo>(o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty));

			foreach (PropertyInfo prop in props)
				if (prop.CanRead && prop.CanWrite && prop.GetCustomAttributes(true).Contains(BrowsableAttribute.Yes))
				{
					Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

					StringBuilder sb = new StringBuilder(MaxBufferSize);
					if (GetPrivateProfileString(section, prop.Name, "", sb, MaxBufferSize, FileName) > 0)
					{
						if (t.IsEnum)
						{
							prop.SetValue(o, Enum.Parse(t, sb.ToString()), null);
						}
						else
						{
							object safeValue = (sb.ToString() == null) ? null : Convert.ChangeType(sb.ToString(), t);

							prop.SetValue(o, safeValue, null);
						}
					}
				}
		}

		#endregion reading
	}
}
