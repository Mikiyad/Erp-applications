using Microsoft.Win32;
using System;


namespace Micromind.Common.Libraries
{
	
	public sealed class ServerRegistryHelper : IDisposable
	{
		private RegistryKey key4;

		private RegistryKey key3;

		private RegistryKey key2;

		private RegistryKey key5;

		private RegistryKey key;

		private readonly string companyName = "Micromind";

		public ServerRegistryHelper(string productName, string subKey, bool writable)
		{
			key4 = Registry.CurrentUser;
			key3 = key4.CreateSubKey("Software");
			key2 = key3.CreateSubKey(companyName);
			key5 = key2.CreateSubKey(productName);
			key = key5.CreateSubKey(subKey);
			key.Close();
			key = key5.OpenSubKey(subKey, writable);
		}

		public ServerRegistryHelper(string productName, RegistryKey reg, string subKey, bool writable)
		{
			key4 = reg;
			key3 = reg.CreateSubKey("Software");
			key2 = key3.CreateSubKey(companyName);
			key5 = key2.CreateSubKey(productName);
			key = key5.CreateSubKey(subKey);
			key.Close();
			key = key5.OpenSubKey(subKey, writable);
		}

		public ServerRegistryHelper(RegistryKey reg, string subKey, string productName, bool writable)
		{
			key4 = reg;
			key3 = reg.CreateSubKey("Software");
			key2 = key3.CreateSubKey(companyName);
			key5 = key2.CreateSubKey(productName);
			key = key5.CreateSubKey(subKey);
			key.Close();
			key = key5.OpenSubKey(subKey, writable);
		}

		public ServerRegistryHelper()
		{
		}

		public void Dispose()
		{
			key4.Close();
			key3.Close();
			key2.Close();
			key5.Close();
			key.Close();
		}

		public string GetStringValue(string name, string defaultValue)
		{
			return key.GetValue(name, defaultValue).ToString();
		}

		public string GetStringValue(string name)
		{
			return key.GetValue(name).ToString();
		}

		public byte[] GetBinaryValue(string name)
		{
			return (byte[])key.GetValue(name);
		}

		public void SetValue(string name, object val)
		{
			key.SetValue(name, val);
		}

		public void DeleteValue(string name)
		{
			key.DeleteValue(name);
		}

		public void DeleteSubKey(string subkey)
		{
			key.DeleteSubKey(subkey);
		}

		public string[] GetValueNames()
		{
			return key.GetValueNames();
		}
	}
}
