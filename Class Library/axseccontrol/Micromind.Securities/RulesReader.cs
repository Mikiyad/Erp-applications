using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;

using System.Text;
using System.Threading;

namespace Micromind.Securities
{
	
	public sealed class RulesReader
	{
		private static int versionNumber = 2;

		public int Edition = Editions.Basic;

		public int NumUser = 1;

		public bool IsTrial = true;

		public bool IsMulti;

		public bool IsBeta;

		private string[] dummyChars = new string[18]
		{
			"A",
			"B",
			"E",
			"F",
			"I",
			"J",
			"L",
			"N",
			"P",
			"Q",
			"S",
			"T",
			"U",
			"V",
			"W",
			"X",
			"Y",
			"Z"
		};

		public static int VersionNumber
		{
			get
			{
				return versionNumber;
			}
			set
			{
				versionNumber = value;
			}
		}

		private int Offset => Global.GetOffset();

		private int IDPrime => Global.GetIDPrime();

		private bool ReadKeyWithSerial(string key)
		{
			try
			{
				if (key.Trim() == string.Empty)
				{
					throw new InvalidKeyException();
				}
				key = UnReplaceData(key);
				string[] array = key.Split('-');
				int num = int.Parse(array[int.Parse("1")]) - Offset;
				if (num % IDPrime != int.Parse("0"))
				{
					throw new InvalidKeyException();
				}
				int num2 = int.Parse(array[int.Parse("2")]) - Offset;
				if (num2 % Editions.Basic == int.Parse("0"))
				{
					Edition = Editions.Basic;
				}
				else if (num2 % Editions.Standard == int.Parse("0"))
				{
					Edition = Editions.Standard;
				}
				else if (num2 % Editions.Professional == int.Parse("0"))
				{
					Edition = Editions.Professional;
				}
				else if (num2 % Editions.Premium == int.Parse("0"))
				{
					Edition = Editions.Premium;
				}
				else
				{
					if (num2 % Editions.Enterprise != int.Parse("0"))
					{
						throw new InvalidKeyException();
					}
					Edition = Editions.Enterprise;
				}
				num2 = int.Parse(array[int.Parse("3")]) - Offset;
				if (num2 % Uses.Trial == int.Parse("0"))
				{
					IsTrial = true;
				}
				else if (num2 % Uses.Beta == int.Parse("0"))
				{
					IsBeta = true;
				}
				else
				{
					if (num2 % Uses.Full != int.Parse("0"))
					{
						throw new InvalidKeyException();
					}
					IsTrial = false;
				}
				num2 = int.Parse(array[int.Parse("4")]) - Offset;
				if (num2 % NumUsers.Single == int.Parse("0"))
				{
					IsMulti = false;
				}
				else
				{
					if (num2 % NumUsers.Multi != int.Parse("0"))
					{
						throw new InvalidKeyException();
					}
					IsMulti = true;
				}
				num2 = int.Parse(array[int.Parse("5")]);
				if (num2 >= 32767)
				{
					NumUser = num2 / 32767;
				}
				else
				{
					NumUser = int.Parse("1");
				}
				return true;
			}
			catch
			{
				throw new InvalidKeyException();
			}
		}

		public bool ReadKey(string key)
		{
			try
			{
				if (key.Trim() == string.Empty)
				{
					throw new InvalidKeyException();
				}
				key = UnReplaceData(key);
				string[] array = key.Split('-');
				if (array.Length > int.Parse("5"))
				{
					return ReadKeyWithSerial(key);
				}
				int num = int.Parse(array[int.Parse("0")]) - Offset;
				if (num % IDPrime != int.Parse("0"))
				{
					throw new InvalidKeyException();
				}
				int num2 = int.Parse(array[int.Parse("1")]) - Offset;
				if (num2 % Editions.Basic == int.Parse("0"))
				{
					Edition = Editions.Basic;
				}
				else if (num2 % Editions.Standard == int.Parse("0"))
				{
					Edition = Editions.Standard;
				}
				else if (num2 % Editions.Professional == int.Parse("0"))
				{
					Edition = Editions.Professional;
				}
				else if (num2 % Editions.Premium == int.Parse("0"))
				{
					Edition = Editions.Premium;
				}
				else
				{
					if (num2 % Editions.Enterprise != int.Parse("0"))
					{
						throw new InvalidKeyException();
					}
					Edition = Editions.Enterprise;
				}
				num2 = int.Parse(array[int.Parse("2")]) - Offset;
				if (num2 % Uses.Trial == int.Parse("0"))
				{
					IsTrial = true;
				}
				else if (num2 % Uses.Beta == int.Parse("0"))
				{
					IsBeta = true;
				}
				else
				{
					if (num2 % Uses.Full != int.Parse("0"))
					{
						throw new InvalidKeyException();
					}
					IsTrial = false;
				}
				num2 = int.Parse(array[int.Parse("3")]) - Offset;
				if (num2 % NumUsers.Single == int.Parse("0"))
				{
					IsMulti = false;
				}
				else
				{
					if (num2 % NumUsers.Multi != int.Parse("0"))
					{
						throw new InvalidKeyException();
					}
					IsMulti = true;
				}
				num2 = int.Parse(array[int.Parse("4")]);
				if (num2 >= 32767)
				{
					NumUser = num2 / 32767;
				}
				else
				{
					NumUser = 1;
				}
				return true;
			}
			catch
			{
				throw new InvalidKeyException();
			}
		}

		public bool ReadFile(string fileName)
		{
			StreamReader streamReader = null;
			streamReader = new StreamReader(fileName);
			bool result = ReadKey(streamReader.ReadLine());
			streamReader.Close();
			return result;
		}

		public bool IsActiveKeyFile(string fileName)
		{
			StreamReader streamReader = null;
			streamReader = new StreamReader(fileName);
			bool result = IsActiveKey(streamReader.ReadLine());
			streamReader.Close();
			return result;
		}

		public string GetSerialFromKey(string key)
		{
			string[] array = key.Split('-');
			if (array.Length < int.Parse("7"))
			{
				throw new ApplicationException("Invalid key.");
			}
			return array[int.Parse("0")] + array[int.Parse("7")];
		}

		[DllImport("kernel32.dll")]
		private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, uint VolumeNameSize, ref uint VolumeSerialNumber, ref uint MaximumComponentLength, ref uint FileSystemFlags, StringBuilder FileSystemNameBuffer, uint FileSystemNameSize);

		private static string GetVolumeSerial(string strDriveLetter)
		{
			uint VolumeSerialNumber = 0u;
			uint MaximumComponentLength = 0u;
			StringBuilder stringBuilder = new StringBuilder(256);
			uint FileSystemFlags = 0u;
			StringBuilder stringBuilder2 = new StringBuilder(256);
			GetVolumeInformation(strDriveLetter, stringBuilder, (uint)stringBuilder.Capacity, ref VolumeSerialNumber, ref MaximumComponentLength, ref FileSystemFlags, stringBuilder2, (uint)stringBuilder2.Capacity);
			return Convert.ToString(VolumeSerialNumber);
		}

		private string GetSerial()
		{
			string[] logicalDrives = Environment.GetLogicalDrives();
			string text = "";
			int num = 0;
			string[] array = logicalDrives;
			foreach (string text2 in array)
			{
				if (text2.IndexOf("C") >= 0)
				{
					text = logicalDrives[num];
					break;
				}
				num++;
			}
			if (text == string.Empty)
			{
				text = ((logicalDrives.Length <= 2) ? logicalDrives[0] : logicalDrives[1]);
			}
			if (text.Length == 1)
			{
				text += ":\\";
			}
			try
			{
				return GetVolumeSerial(text);
			}
			catch
			{
			}
			return GetMotherBoardSerial();
		}

		private string GetMotherBoardSerial()
		{
			string text = "";
			try
			{
				ManagementPath managementPath = new ManagementPath();
				managementPath.Server = Environment.MachineName;
				ManagementScope scope = new ManagementScope(managementPath);
				ObjectQuery query = new ObjectQuery("select SerialNumber from Win32_BaseBoard");
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query);
				ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
				using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator())
				{
					if (managementObjectEnumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
						text = managementObject["SerialNumber"].ToString().Trim();
					}
				}
				if (text != null && text != string.Empty)
				{
					return text;
				}
			}
			catch
			{
			}
			return "MMM99999";
		}

		public bool HasCorrectSerial(string key)
		{
			string[] array = key.Split('-');
			if (array.Length != int.Parse("7"))
			{
				return false;
			}
			string b = array[int.Parse("0")] + array[int.Parse("6")];
			if (GetEncrptedSerial() != b)
			{
				return false;
			}
			return true;
		}

		private int GetRand(int seed)
		{
			Thread.Sleep(2);
			return new Random(seed).Next(int.Parse("50"), int.Parse("99"));
		}

		public string GetInactiveCode(string key)
		{
			string encrptedSerial = GetEncrptedSerial();
			int num = encrptedSerial.Length / int.Parse("2");
			string text = encrptedSerial.Substring(int.Parse("0"), num);
			string text2 = encrptedSerial.Substring(num);
			string text3 = "";
			int num2 = new Random((int)DateTime.Now.Ticks).Next(int.Parse("0"), int.Parse("6"));
			if (num2 == int.Parse("0"))
			{
				return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + text + "-" + key + "-" + text2 + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true);
			}
			if (num2 == int.Parse("1"))
			{
				return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + text + "-" + key + "-" + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + text2;
			}
			if (num2 == int.Parse("2"))
			{
				return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + text + "-" + key + "-" + text2 + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true);
			}
			if (num2 == int.Parse("3"))
			{
				return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + text + "-" + key + "-" + text2 + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true);
			}
			if (num2 == int.Parse("4"))
			{
				return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + text + "-" + key + "-" + text2 + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true);
			}
			if (num2 == int.Parse("5"))
			{
				return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + text + "-" + key + "-" + text2 + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true);
			}
			return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + text + "-" + key + "-" + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + text2;
		}

		private string GetEncrptedSerial()
		{
			int num = GetSerial().GetHashCode();
			if (num < 0)
			{
				num = Math.Abs(num);
			}
			return ReplaceData(num.ToString());
		}

		public string GetKeyPart(string key)
		{
			string[] array = key.Split('-');
			if (array.Length < int.Parse("5"))
			{
				throw new ApplicationException("Invalid key.");
			}
			string result = "";
			if (array.Length == int.Parse("5"))
			{
				result = array[0] + "-" + array[int.Parse("1")] + "-" + array[int.Parse("2")] + "-" + array[int.Parse("3")] + "-" + array[int.Parse("4")];
			}
			else if (array.Length == int.Parse("7"))
			{
				result = array[int.Parse("1")] + "-" + array[int.Parse("2")] + "-" + array[int.Parse("3")] + "-" + array[int.Parse("4")] + "-" + array[int.Parse("5")];
			}
			return result;
		}

		public string GetKey(int edition, int use, int numUsers, int numClients)
		{
			int num = 0;
			string text = "";
			int millisecond = DateTime.Now.Millisecond;
			text = (GetRand(millisecond++) * IDPrime + Offset).ToString();
			text = text + "-" + (GetRand(millisecond++) * edition + Offset).ToString();
			text = text + "-" + (GetRand(millisecond++) * use + Offset).ToString();
			text = text + "-" + (GetRand(millisecond++) * numUsers + Offset).ToString();
			text = text + "-" + ((numUsers != NumUsers.Multi) ? new Random(millisecond++).Next(10001, 31000) : (numClients * 32767)).ToString();
			return ReplaceData(text);
		}

		public bool IsActiveKey(string key)
		{
			return HasCorrectSerial(key);
		}

		public bool IsEqualSecretKey(string key1, string key2)
		{
			if (key1.Length == key2.Length)
			{
				return GetMachineID(key1) == GetMachineID(key2);
			}
			return false;
		}

		public string GetMachineID(string key)
		{
			return RemoveDummyChars(key.Replace("-", ""));
		}

		public string GetSecretKey(string key)
		{
			key = RemoveDummyChars(key);
			key = ReplaceData(key.GetHashCode().ToString()).Trim();
			string text = key.Substring(int.Parse("0"), key.Length / int.Parse("2"));
			string text2 = key.Substring(key.Length / int.Parse("2"));
			int num = new Random((int)DateTime.Now.Ticks).Next(int.Parse("0"), int.Parse("4"));
			string text3 = "";
			if (num == int.Parse("0"))
			{
				text3 = GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + text2 + GetDummyChar(check: true) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + text + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false);
			}
			if (num == int.Parse("1"))
			{
				text3 = GetDummyChar(check: false) + GetDummyChar(check: true) + text + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + text2 + GetDummyChar(check: true) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false);
			}
			if (num == int.Parse("2"))
			{
				text3 = GetDummyChar(check: false) + GetDummyChar(check: true) + text + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + text2 + GetDummyChar(check: true);
			}
			if (num == int.Parse("3"))
			{
				return GetDummyChar(check: false) + GetDummyChar(check: true) + text + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: false) + GetDummyChar(check: true) + "-" + GetDummyChar(check: false) + text2 + GetDummyChar(check: true) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false);
			}
			return GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + text + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + "-" + GetDummyChar(check: false) + text2 + GetDummyChar(check: true) + "-" + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false) + GetDummyChar(check: true) + GetDummyChar(check: false);
		}

		private string ReplaceData(string data)
		{
			data = data.Replace("0", "K");
			data = data.Replace("1", "M");
			data = data.Replace("3", "C");
			data = data.Replace("4", "D");
			data = data.Replace("6", "G");
			data = data.Replace("7", "H");
			data = data.Replace("9", "R");
			return data;
		}

		private string UnReplaceData(string data)
		{
			if (data.IndexOfAny(new char[7]
			{
				'3',
				'4',
				'6',
				'7',
				'0',
				'1',
				'9'
			}) >= 0)
			{
				return "";
			}
			data = data.Replace("C", "3");
			data = data.Replace("D", "4");
			data = data.Replace("G", "6");
			data = data.Replace("H", "7");
			data = data.Replace("K", "0");
			data = data.Replace("M", "1");
			data = data.Replace("R", "9");
			return data;
		}

		private string RemoveDummyChars(string data)
		{
			string[] array = dummyChars;
			foreach (string oldValue in array)
			{
				data = data.Replace(oldValue, "");
			}
			return data;
		}

		private string GetDummyChar(bool check)
		{
			int num = 0;
			Thread.Sleep(20);
			num = (check ? new Random((int)(~DateTime.Now.Ticks)).Next(int.Parse("0"), int.Parse("17")) : new Random((int)DateTime.Now.Ticks).Next(int.Parse("0"), int.Parse("17")));
			return dummyChars[num];
		}
	}
}
