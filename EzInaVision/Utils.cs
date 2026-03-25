/*
 * Helper Class Libraries
 * Written by kslee
 * copyright ANI. 2021, all rights reserved
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzInaVision
{
	//Using Object Extension Method
	public static class MsgBox
	{
		public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
		{
			return MessageBox.Show(new Form { TopMost = true }, text, Application.ProductName, buttons, icon, defaultButton);
		}
		public static void Show(string text)
		{
			Show(text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static void Error(string text)
		{
			Show(text, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		public static void Error(string fmt, params object[] args)
		{
			Error(string.Format(fmt, args));
		}

		public static void Warning(string text)
		{
			Show(text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		public static bool Confirm(string text)
		{
			return Show(text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
		}
		public static bool Confirm(string fmt, params object[] args)
		{
			return Confirm(string.Format(fmt, args));
		}
	}
	public static class Utils
	{
		public static T CopyObject<T>(this object objSource)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, objSource);
				stream.Position = 0;
				return (T)formatter.Deserialize(stream);
			}
		}
		public static object CloneObject(this object objSource)
		{
			//step : 1 Get the type of source object and create a new instance of that type
			Type typeSource = objSource.GetType();
			object objTarget = Activator.CreateInstance(typeSource);

			//Step2 : Get all the properties of source object type
			PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			//Step : 3 Assign all source property to taget object 's properties
			foreach (PropertyInfo property in propertyInfo)
			{
				//Check whether property can be written to
				if (property.CanWrite)
				{
					//Step : 4 check whether property type is value type, enum or string type
					if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
					{
						property.SetValue(objTarget, property.GetValue(objSource, null), null);
					}
					//else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
					else
					{
						object objPropertyValue = property.GetValue(objSource, null);

						if (objPropertyValue == null)
						{
							property.SetValue(objTarget, null, null);
						}
						else
						{
							property.SetValue(objTarget, CloneObject(objPropertyValue), null);
						}
					}
				}
			}
			return objTarget;
		}
	}
}
