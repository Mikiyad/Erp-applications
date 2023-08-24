using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace LicenseManager
{
	public class LicenseManagerControl : Component
	{
		public enum LicenseErrors
		{
			NoKey,
			InvalidKey
		}

		public int ApplicationMajorVersion = 3;

		private LicenseErrors currentLicenseError;

		private bool hasLicenseError;

		private string keyuid = "D1FE647C54D5YY4BW5C60YF0741E1WF3";

		private DateTime startDate;

		private bool isValidKey;

		private LicenseKey licenseKey = new LicenseKey();

		private string hardwareSerial = "";

		private IContainer components;

		public LicenseKey License => licenseKey;

		public bool IsValidKey => isValidKey;

		public bool IsTrialKey => licenseKey.IsTrial;

		public bool IsActivated
		{
			get
			{
				return licenseKey.IsActivated;
			}
			set
			{
				licenseKey.IsActivated = value;
			}
		}

		public bool IsTrialExpired
		{
			get
			{
				return licenseKey.IsTrialExpired;
			}
			set
			{
				licenseKey.IsTrialExpired = value;
			}
		}

		public LicenseErrors CurrentLicenseError
		{
			get
			{
				return currentLicenseError;
			}
			set
			{
				currentLicenseError = value;
			}
		}

		public bool HasLicenseError
		{
			get
			{
				return hasLicenseError;
			}
			set
			{
				hasLicenseError = value;
			}
		}

		private static string ID => "F1FA657C-58D5-404B-ABC6-01F07F1E18F1";

		public event EventHandler LicenseError;

		public LicenseManagerControl()
		{
			InitializeComponent();
		}

		public LicenseManagerControl(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public bool SetKey(string key)
		{
			try
			{
				RulesReader rulesReader = new RulesReader();
				licenseKey = rulesReader.ReadKey(key);
				isValidKey = true;
				return true;
			}
			catch (Exception)
			{
				isValidKey = false;
				return false;
			}
		}

		public bool CheckLicense()
		{
			HasLicenseError = false;
			string str = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion;
			str += "\\keyelement.mky";
			if (!File.Exists(str))
			{
				RaiseError(LicenseErrors.NoKey);
				return false;
			}
			TextReader textReader = new StreamReader(str);
			string text = textReader.ReadLine();
			if (text == null)
			{
				RaiseError(LicenseErrors.NoKey);
				textReader.Close();
				return false;
			}
			int num = text.IndexOf(keyuid);
			if (num < 0)
			{
				RaiseError(LicenseErrors.InvalidKey);
				textReader.Close();
				return false;
			}
			string text2 = Decrypt(text.Substring(0, num));
			string key = text2;
			startDate = GetDatePart(text);
			if (startDate == DateTime.MinValue)
			{
				RaiseError(LicenseErrors.InvalidKey);
				textReader.Close();
				return false;
			}
			textReader.Close();
			RulesReader rulesReader = new RulesReader();
			licenseKey = rulesReader.ReadKey(text2);
			str = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion;
			str += "\\keyelementact.mky";
			if (!File.Exists(str))
			{
				IsActivated = false;
			}
			else
			{
				textReader = new StreamReader(str);
				text = textReader.ReadLine();
				if (text == null)
				{
					IsActivated = false;
				}
				else
				{
					string a = Decrypt(text);
					string text3 = "";
					try
					{
						text3 = GetActivationKey(key, GetSystemBoundKey());
					}
					catch
					{
					}
					if (text3 != "" && a == text3)
					{
						IsActivated = true;
					}
					else
					{
						IsActivated = false;
					}
				}
				textReader.Close();
			}
			DateTime t = startDate.AddDays(licenseKey.ExpiryDays);
			DateTime lastUseDate = GetLastUseDate();
			if (t < DateTime.Today || (lastUseDate != DateTime.MinValue && lastUseDate > DateTime.Today))
			{
				IsTrialExpired = true;
				return true;
			}
			SetLastUseDate();
			return true;
		}

		public int TrialDaysLeft()
		{
			TimeSpan timeSpan = startDate.AddDays(licenseKey.ExpiryDays).Subtract(DateTime.Today);
			if (timeSpan.Days < 0)
			{
				return 0;
			}
			return timeSpan.Days;
		}

		public void RaiseError(LicenseErrors errorType)
		{
			if (this.LicenseError != null)
			{
				this.LicenseError(errorType, null);
			}
			HasLicenseError = true;
			CurrentLicenseError = errorType;
		}

		private string OldKeyCheckout(string checkKey)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\axytblib.lib";
			string path2 = Path.GetDirectoryName(Application.ExecutablePath) + "\\axsyslic.dll";
			if (!File.Exists(path))
			{
				File.AppendText(path).Close();
			}
			if (!File.Exists(path2))
			{
				File.AppendText(path2).Close();
			}
			string[] array = File.ReadAllLines(path);
			string[] array2 = File.ReadAllLines(path2);
			if (array.Length == 0 && array2.Length == 0)
			{
				return "";
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IndexOf(checkKey) >= 0)
				{
					return array[i];
				}
			}
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].IndexOf(checkKey) >= 0)
				{
					return array2[j];
				}
			}
			return "";
		}

		private string GetKeyPartEncrypted(string keyLine)
		{
			int length = keyLine.IndexOf(keyuid);
			return keyLine.Substring(0, length);
		}

		private DateTime GetDatePart(string keyLine)
		{
			int num = keyLine.IndexOf(keyuid) + keyuid.Length;
			string data = keyLine.Substring(num, keyLine.Length - num);
			data = Decrypt(data);
			int result = 0;
			if (!int.TryParse(data, out result))
			{
				RaiseError(LicenseErrors.InvalidKey);
				return DateTime.MinValue;
			}
			int result2 = 0;
			result = Math.DivRem(result, 7, out result2);
			if (result2 != 0)
			{
				RaiseError(LicenseErrors.InvalidKey);
				return DateTime.MinValue;
			}
			return new DateTime(int.Parse(result.ToString().Substring(0, 4)), int.Parse(result.ToString().Substring(4, 2)), int.Parse(result.ToString().Substring(6, 2)));
		}

		private DateTime GetKeyDateFromFile(string path, string encryptedKey)
		{
			if (File.Exists(path))
			{
				TextReader textReader = new StreamReader(path);
				string[] array = File.ReadAllLines(path);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IndexOf(encryptedKey) >= 0)
					{
						textReader.Close();
						return GetDatePart(array[i]);
					}
				}
				textReader.Close();
				return DateTime.MaxValue;
			}
			return DateTime.MaxValue;
		}

		private string GetKeyLineFromFile(string path)
		{
			return "";
		}

		private DateTime GetKeyMinDate(string encryptedKey)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion + "\\keyelement.mky";
			string path2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\axytblib.lib";
			string path3 = Application.ExecutablePath + "\\axsyslic.dll";
			_ = Application.ExecutablePath + "\\clientaxint.dll";
			DateTime dateTime = DateTime.MaxValue;
			DateTime maxValue = DateTime.MaxValue;
			DateTime maxValue2 = DateTime.MaxValue;
			TextReader textReader = new StreamReader(path);
			string text = textReader.ReadLine();
			if (text != null)
			{
				dateTime = GetDatePart(text);
			}
			textReader.Close();
			maxValue = GetKeyDateFromFile(path2, encryptedKey);
			maxValue2 = GetKeyDateFromFile(path3, encryptedKey);
			if (maxValue < dateTime)
			{
				dateTime = maxValue;
			}
			if (maxValue2 < dateTime)
			{
				dateTime = maxValue2;
			}
			return dateTime;
		}

		private void SyncKeys()
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion;
			string text2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\axytblib.lib";
			string text3 = Path.GetDirectoryName(Application.ExecutablePath) + "\\axsyslic.dll";
			string destFileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\clientaxint.dll";
			Directory.CreateDirectory(text);
			text += "\\keyelement.mky";
			string text4 = "";
			if (!File.Exists(text))
			{
				return;
			}
			TextReader textReader = new StreamReader(text);
			text4 = textReader.ReadLine();
			textReader.Close();
			DateTime datePart = GetDatePart(text4);
			DateTime keyMinDate = GetKeyMinDate(GetKeyPartEncrypted(text4));
			if (keyMinDate < datePart)
			{
				WriteLicense(keyMinDate);
				return;
			}
			File.Copy(text, destFileName, overwrite: true);
			string[] array = File.ReadAllLines(text2);
			string[] array2 = File.ReadAllLines(text3);
			string[] array3 = array;
			if (array.LongLength != array2.LongLength)
			{
				array3 = ((array.LongLength <= array2.LongLength) ? array2 : array);
			}
			bool flag = true;
			for (int i = 0; i < array3.Length; i++)
			{
				if (array3[i] == text4)
				{
					flag = false;
					break;
				}
			}
			string[] array4 = new string[array3.Length + 1];
			if (flag)
			{
				for (int j = 0; j < array3.Length; j++)
				{
					array4[j] = array3[j];
				}
				array4[array3.Length] = text4;
			}
			else
			{
				array4 = array3;
			}
			File.Delete(text2);
			File.Delete(text3);
			File.WriteAllLines(text2, array4);
			File.Copy(text2, text3, overwrite: true);
		}

		private string ReadKeyFile()
		{
			string str = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion;
			str += "\\keyelement.mky";
			if (!File.Exists(str))
			{
				RaiseError(LicenseErrors.NoKey);
				return "";
			}
			TextReader textReader = new StreamReader(str);
			textReader.ReadLine();
			return "";
		}

		public string GetSystemBoundKey()
		{
			string motherBoardSerialNumber = GetMotherBoardSerialNumber();
			motherBoardSerialNumber += "#8HOS9J38FNS";
			motherBoardSerialNumber = motherBoardSerialNumber.Substring(0, 12);
			Random rand = new Random(700);
			RulesReader rulesReader = new RulesReader();
			return rulesReader.EncryptKey(motherBoardSerialNumber, rand);
		}

		private string GetMotherBoardSerialNumber()
		{
			string str = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion;
			str += "\\mbs.ser";
			try
			{
				if (hardwareSerial != "")
				{
					return hardwareSerial;
				}
				ManagementObjectCollection managementObjectCollection = null;
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
				managementObjectCollection = managementObjectSearcher.Get();
				string text = "";
				foreach (ManagementObject item in managementObjectCollection)
				{
					text += item["SerialNumber"].ToString();
				}
				managementObjectSearcher.Dispose();
				managementObjectCollection.Dispose();
				hardwareSerial = text;
				TextWriter textWriter = new StreamWriter(str);
				textWriter.WriteLine(text);
				textWriter.Close();
				return text;
			}
			catch (Exception)
			{
				if (!File.Exists(str))
				{
					return Environment.MachineName;
				}
				TextReader textReader = new StreamReader(str);
				string result = textReader.ReadLine();
				textReader.Close();
				return result;
			}
		}

		public string GetActivationKey(string key, string systemKey)
		{
			RulesReader rulesReader = new RulesReader();
			return rulesReader.GetActivationKey(key, systemKey);
		}

		public void WriteLicense()
		{
			WriteLicense(DateTime.Today);
		}

		public void WriteLicense(DateTime installDate)
		{
			if (IsValidKey)
			{
				if (installDate == DateTime.MinValue)
				{
					installDate = DateTime.Today;
				}
				string text = Encrypt(licenseKey.Key);
				OldKeyCheckout(text);
				string s = installDate.Year.ToString("0000") + installDate.Month.ToString("00") + installDate.Day.ToString("00");
				s = Encrypt((int.Parse(s) * 7).ToString());
				text = text + keyuid + s;
				string text2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion;
				Directory.CreateDirectory(text2);
				text2 += "\\keyelement.mky";
				TextWriter textWriter = new StreamWriter(text2);
				textWriter.WriteLine(text);
				textWriter.Close();
				SyncKeys();
			}
		}

		private DateTime GetLastUseDate()
		{
			try
			{
				string text = Encrypt(licenseKey.Key);
				text = text.Substring(0, 32);
				text = "{" + text.ToUpper() + "}";
				text = text.Insert(9, "-");
				text = text.Insert(14, "-");
				text = text.Insert(19, "-");
				text = text.Insert(24, "-");
				string subkey = "SOFTWARE\\Classes\\AppID\\" + text;
				RegistryKey currentUser = Registry.CurrentUser;
				currentUser = currentUser.CreateSubKey(subkey);
				object value = currentUser.GetValue("DTC");
				string text2 = "";
				if (value == null)
				{
					return DateTime.MinValue;
				}
				text2 = value.ToString();
				text2 = Decrypt(text2);
				DateTime minValue = DateTime.MinValue;
				return DateTime.Parse(text2);
			}
			catch
			{
				return DateTime.MinValue;
			}
		}

		private void SetLastUseDate()
		{
			try
			{
				if (!(GetLastUseDate() > DateTime.Today))
				{
					string text = Encrypt(licenseKey.Key);
					text = text.Substring(0, 32);
					text = "{" + text.ToUpper() + "}";
					text = text.Insert(9, "-");
					text = text.Insert(14, "-");
					text = text.Insert(19, "-");
					text = text.Insert(24, "-");
					string subkey = "SOFTWARE\\Classes\\AppID\\" + text;
					RegistryKey currentUser = Registry.CurrentUser;
					currentUser = currentUser.CreateSubKey(subkey);
					string data = DateTime.Today.ToShortDateString();
					data = Encrypt(data);
					currentUser.SetValue("DTC", data);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public void WriteActivation(string activationKey)
		{
			string value = Encrypt(activationKey);
			string text = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + Application.ProductVersion;
			Directory.CreateDirectory(text);
			text += "\\keyelementact.mky";
			TextWriter textWriter = new StreamWriter(text);
			textWriter.WriteLine(value);
			textWriter.Close();
		}

		private string Encrypt(string data)
		{
			ConfigHelper configHelper = new ConfigHelper(ID);
			return configHelper.Cryptor.Encrypt(data);
		}

		private string Decrypt(string data)
		{
			ConfigHelper configHelper = new ConfigHelper(ID);
			return configHelper.Cryptor.Decrypt(data);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new Container();
		}
	}
}
