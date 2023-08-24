using Microsoft.Win32;
using System;
using System.Drawing;

using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	
	public sealed class RegistryHelper : IDisposable
	{
		private RegistryKey key4;

		private RegistryKey key3;

		private RegistryKey key2;

		private RegistryKey key5;

		private RegistryKey key;

		private readonly string subKeyCompanies = "Companies";

		public RegistryKey CurrentWindowsUserKey => Registry.CurrentUser.CreateSubKey("Software").CreateSubKey(Application.CompanyName).CreateSubKey(Application.ProductName);

		public RegistryHelper(string subKey, bool writable)
			: this(null, subKey, Application.ProductName, writable)
		{
		}

		public RegistryHelper(RegistryKey reg, string subKey, bool writable)
			: this(reg, subKey, Application.ProductName, writable)
		{
		}

		public RegistryHelper(RegistryKey reg, string subKey, string productName, bool writable)
		{
			if (reg == null)
			{
				reg = Registry.CurrentUser;
			}
			key4 = reg;
			key3 = reg.CreateSubKey("Software");
			key2 = key3.CreateSubKey(Application.CompanyName);
			key5 = key2.CreateSubKey(productName);
			key = key5.CreateSubKey(subKey);
			key.Close();
			key = key5.OpenSubKey(subKey, writable);
		}

		public RegistryHelper()
		{
		}

		public string GetRegistrySettingKey(string keyName)
		{
			string text = CurrentWindowsUserKey.ToString();
			return text + "\\" + Global.CurrentDatabaseName + "\\" + Global.CurrentUser + "\\" + keyName;
		}

		public void OpenSubKeyCompanies(string companyName, bool writable)
		{
			key4 = Registry.CurrentUser;
			key3 = key4.CreateSubKey("Software");
			key2 = key3.CreateSubKey(Application.CompanyName);
			key5 = key2.CreateSubKey(Application.ProductName);
			key = key5.CreateSubKey(subKeyCompanies + "\\" + companyName);
			key.Close();
			key = key5.OpenSubKey(subKeyCompanies + "\\" + companyName, writable);
		}

		public static void DeleteSubKeyCompany(string companyName)
		{
			RegistryKey currentUser = Registry.CurrentUser;
			RegistryKey registryKey = currentUser.CreateSubKey("Software");
			RegistryKey registryKey2 = registryKey.CreateSubKey(Application.CompanyName);
			RegistryKey registryKey3 = registryKey2.CreateSubKey(Application.ProductName);
			RegistryKey registryKey4 = registryKey3.OpenSubKey("Companies");
			registryKey4.DeleteSubKey(companyName);
			currentUser.Close();
			registryKey.Close();
			registryKey2.Close();
			registryKey3.Close();
			registryKey4.Close();
		}

		public void Dispose()
		{
			key4.Close();
			key3.Close();
			key2.Close();
			key5.Close();
			key.Close();
		}

		public string GetStringValue(RegistryKey key, string name, string defaultValue)
		{
			return key.GetValue(name, defaultValue).ToString();
		}

		public string GetStringValue(string name, string defaultValue)
		{
			return key.GetValue(name, defaultValue).ToString();
		}

		public string GetStringValue(string name)
		{
			return key.GetValue(name, "").ToString();
		}

		public byte[] GetBinaryValue(string name)
		{
			return (byte[])key.GetValue(name);
		}

		public void SetValue(string name, object val)
		{
			key.SetValue(name, val);
		}

		public void SetValue(RegistryKey key, string name, object val)
		{
			key.SetValue(name, val);
		}

		public void DeleteValue(string name)
		{
			try
			{
				key.DeleteValue(name);
			}
			catch
			{
			}
		}

		public void DeleteSubKey(string subkey)
		{
			try
			{
				key.DeleteSubKey(subkey);
			}
			catch
			{
			}
		}

		public void SaveForm(Form form)
		{
			SetValue("X", form.Location.X);
			SetValue("Y", form.Location.Y);
			SetValue("Width", form.Width);
			SetValue("Height", form.Height);
			SetValue("Top", form.Top);
		}

		public void LoadForm(Form form)
		{
			try
			{
				int x = int.Parse(GetStringValue("X", form.Location.X.ToString()));
				int y = int.Parse(GetStringValue("Y", form.Location.Y.ToString()));
				int num = int.Parse(GetStringValue("Width", form.Width.ToString()));
				int num2 = int.Parse(GetStringValue("Height", form.Height.ToString()));
				if (num2 > 0)
				{
					form.Height = num2;
				}
				if (num > 0)
				{
					form.Width = num;
				}
				form.Top = int.Parse(GetStringValue("Top", form.Top.ToString()));
				form.Location = new Point(x, y);
			}
			catch
			{
			}
		}

		public string[] GetValueNames()
		{
			return key.GetValueNames();
		}
	}
}
