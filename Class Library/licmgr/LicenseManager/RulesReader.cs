using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace LicenseManager
{
	
	public sealed class RulesReader
	{
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

		public LicenseKey ReadKey(string key)
		{
			try
			{
				string text = DecryptKey(key);
				int productID = int.Parse(text.Substring(0, 2)) / 2;
				int editionID = int.Parse(text.Substring(2, 3)) / 5;
				int versionMajor = int.Parse(text.Substring(5, 2)) / 7;
				int versionMinor = int.Parse(text.Substring(7, 2));
				int users = int.Parse(text.Substring(9, 3)) / 3;
				int num = int.Parse(text.Substring(12, 2));
				int expiryDays = int.Parse(text.Substring(14, 3)) / 11;
				string text2 = "";
				string text3 = "";
				bool isTrial = num == 41;
				if (text.Length > 20)
				{
					for (int i = 18; i < 29; i++)
					{
						text3 = text.Substring(i, 1);
						if (!(text3 != "") || !(text3 != "&") || !(text3 != "(") || !(text3 != ")"))
						{
							break;
						}
						text2 += text3;
					}
				}
				return new LicenseKey(key, productID, editionID, versionMajor, versionMinor, users, expiryDays, isTrial, text2);
			}
			catch
			{
				throw new InvalidKeyException();
			}
		}

		private string DecryptKey(string key)
		{
			int result = 0;
			if (!int.TryParse(key.Substring(key.Length - 1, 1), out result))
			{
				throw new InvalidKeyException();
			}
			key = key.Substring(0, key.Length - 1);
			byte[] bytes = Encoding.ASCII.GetBytes(key);
			string text = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				text += bytes[i].ToString();
			}
			int num = 0;
			for (int j = 0; j < text.Length; j++)
			{
				int num2 = int.Parse(text.Substring(j, 1));
				num += num2;
			}
			while (num > 9)
			{
				string text2 = num.ToString();
				num = 0;
				for (int k = 0; k < text2.Length; k++)
				{
					num += int.Parse(text2.Substring(k, 1));
				}
			}
			if (result != num)
			{
				throw new InvalidKeyException();
			}
			string text3 = "";
			for (int l = 0; l < key.Length; l++)
			{
				string str = key.Substring(l, 1);
				text3 += GetCharDecNum(str).ToString();
			}
			key = "";
			for (int m = 0; m < text3.Length; m += 2)
			{
				int value = int.Parse(text3.Substring(m, 2));
				key += Convert.ToChar(value).ToString();
			}
			return key;
		}

		private int GetCharDecNum(string str)
		{
			int result = 0;
			if (int.TryParse(str, out result))
			{
				return result;
			}
			if (str == "A" || str == "H" || str == "J")
			{
				return 0;
			}
			if (str == "N" || str == "T")
			{
				return 1;
			}
			if (str == "G" || str == "M")
			{
				return 2;
			}
			if (str == "F" || str == "S")
			{
				return 3;
			}
			if (str == "D" || str == "E" || str == "K" || str == "Y")
			{
				return 4;
			}
			if (str == "V" || str == "Z")
			{
				return 5;
			}
			if (str == "P" || str == "R")
			{
				return 6;
			}
			if (str == "X")
			{
				return 7;
			}
			if (str == "Q" || str == "U" || str == "L" || str == "B")
			{
				return 8;
			}
			if (str == "W" || str == "C")
			{
				return 9;
			}
			return -1;
		}

		public bool ReadFile(string fileName)
		{
			StreamReader streamReader = null;
			streamReader = new StreamReader(fileName);
			bool result = true;
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

		public string[] GetKey(int productID, int edition, int majorVersion, int minorVersion, int expiryDays, int userNumber, bool isTrial, string userData, int count, string loginKey)
		{
			if (loginKey != "sjk37f892m9g6nls78%@jf0")
			{
				return new string[0];
			}
			string[] array = new string[count];
			string text = "";
			string text2 = (productID * 2).ToString("00");
			string text3 = (edition * 5).ToString("000");
			string text4 = (majorVersion * 7).ToString("00");
			string text5 = minorVersion.ToString("00");
			string text6 = (userNumber * 3).ToString("000");
			string text7 = (!isTrial) ? "89" : "41";
			string text8 = (expiryDays * 11).ToString("000");
			string str = (userData + GetDummyUserData()).Substring(0, 10);
			text = text2 + text3 + text4 + text5 + text6 + text7 + text8;
			if (userData != "")
			{
				text += str;
			}
			int num = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
			num += new Random().Next(1, 2147383647);
			Random rand = new Random(num);
			string text9 = "";
			for (int i = 0; i < count; i++)
			{
				text9 = (array[i] = EncryptKey(text, rand));
			}
			return array;
		}

		public string EncryptKey(string key, Random rand)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(key);
			string text = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				text += bytes[i].ToString();
			}
			int num = 0;
			key = "";
			for (int j = 0; j < text.Length; j++)
			{
				int digit = int.Parse(text.Substring(j, 1));
				key += GetDigitEncChar(digit, rand);
				if (j != 0)
				{
					Math.IEEERemainder(j, 2.0);
					_ = 0.0;
				}
			}
			bytes = Encoding.ASCII.GetBytes(key);
			text = "";
			for (int k = 0; k < bytes.Length; k++)
			{
				text += bytes[k].ToString();
			}
			for (int l = 0; l < text.Length; l++)
			{
				int num2 = int.Parse(text.Substring(l, 1));
				num += num2;
			}
			while (num > 9)
			{
				string text2 = num.ToString();
				num = 0;
				for (int m = 0; m < text2.Length; m++)
				{
					num += int.Parse(text2.Substring(m, 1));
				}
			}
			key += num;
			return key;
		}

		public bool IsValidSystemBoundKey(string systemCode)
		{
			systemCode = systemCode.Replace("-", "");
			try
			{
				DecryptKey(systemCode);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private string GetDigitEncChar(int digit, Random rand)
		{
			int result = 0;
			int a = rand.Next(1, int.MaxValue);
			Math.DivRem(a, 5, out result);
			if (digit != 0 && result == 0)
			{
				return digit.ToString();
			}
			switch (digit)
			{
			case 0:
				if (result == 0)
				{
					return "J";
				}
				if (result == 1 || result == 2)
				{
					return "H";
				}
				if (result >= 3)
				{
					return "A";
				}
				break;
			case 1:
				if (result == 1)
				{
					return "N";
				}
				if (result == 2)
				{
					return "N";
				}
				if (result >= 3)
				{
					return "T";
				}
				break;
			case 2:
				if (result == 1)
				{
					return "G";
				}
				if (result == 2)
				{
					return digit.ToString();
				}
				if (result >= 3)
				{
					return "M";
				}
				break;
			case 3:
				if (result == 1)
				{
					return "F";
				}
				if (result == 2)
				{
					return "F";
				}
				if (result >= 3)
				{
					return "S";
				}
				break;
			case 4:
				switch (result)
				{
				case 1:
					return "D";
				case 2:
					return "E";
				case 3:
					return "K";
				case 4:
					return "Y";
				}
				break;
			case 5:
				if (result == 1)
				{
					return "V";
				}
				if (result == 2)
				{
					return digit.ToString();
				}
				if (result >= 3)
				{
					return "Z";
				}
				break;
			case 6:
				if (result == 1)
				{
					return "P";
				}
				if (result == 2)
				{
					return "P";
				}
				if (result >= 3)
				{
					return "R";
				}
				break;
			case 7:
				if (result == 1)
				{
					return "X";
				}
				if (result == 2)
				{
					return "X";
				}
				if (result >= 3)
				{
					return "X";
				}
				break;
			case 8:
				switch (result)
				{
				case 1:
					return "Q";
				case 2:
					return "U";
				case 3:
					return "L";
				case 4:
					return "B";
				}
				break;
			case 9:
				if (result == 1)
				{
					return "W";
				}
				if (result == 2)
				{
					return digit.ToString();
				}
				if (result >= 3)
				{
					return "C";
				}
				break;
			}
			return "";
		}

		private string GetDummyUserData()
		{
			string text = "";
			return "&()()&&()&";
		}

		private string GetDummyData()
		{
			string text = "";
			text = "";
			Random random = new Random(DateTime.Now.Millisecond);
			while (text.Length < 5)
			{
				int num = random.Next(65, 90);
				if (num != 73 && num != 79)
				{
					text += Convert.ToChar(num).ToString();
				}
			}
			return text;
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

		public string GetActivationKey(string key, string systemKey)
		{
			LicenseKey licenseKey = ReadKey(key);
			if (licenseKey.IsTrial)
			{
				throw new Exception("Trial key cannot be activated.");
			}
			byte[] bytes = Encoding.ASCII.GetBytes(key);
			string text = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				text += bytes[i].ToString();
			}
			int num = 0;
			for (int j = 0; j < text.Length; j++)
			{
				int num2 = int.Parse(text.Substring(j, 1));
				num += num2;
			}
			int num3 = num + new Random(num).Next(145, 999);
			bytes = Encoding.ASCII.GetBytes(systemKey);
			text = "";
			for (int k = 0; k < bytes.Length; k++)
			{
				text += bytes[k].ToString();
			}
			num = 0;
			for (int l = 0; l < text.Length; l++)
			{
				int num4 = int.Parse(text.Substring(l, 1));
				num += num4;
			}
			int num5 = new Random(num).Next(145, 999);
			return num3.ToString() + (num + num5).ToString();
		}
	}
}
