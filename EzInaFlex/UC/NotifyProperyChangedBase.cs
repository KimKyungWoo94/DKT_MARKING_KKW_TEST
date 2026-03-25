using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace EzIna.GUI
{
		public abstract class NotifyProperyChangedBase : INotifyPropertyChanged
		{
				#region INotifyPropertyChanged Members

				public event PropertyChangedEventHandler PropertyChanged;

				#endregion

				#region methods

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
				protected bool CheckPropertyChanged<T>(string propertyName, T a_oldValue, T a_newValue, Action<T> a_SetValue)
				{
						if (a_oldValue == null && a_newValue == null)
						{
								return false;
						}

						if ((a_oldValue == null && a_newValue != null) || !a_oldValue.Equals((T)a_newValue))
						{
								if (a_SetValue != null)
								{
										a_SetValue(a_newValue);
										FirePropertyChanged(propertyName);
										return true;
								}
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

		}
}
