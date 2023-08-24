using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;

using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Micromind.ClientLibraries
{
	
	public class LocalSettings : IDisposable
	{
		private XmlRegistry registry;

		private XmlRegistryKey rootKey;

		private string xmlFile = string.Empty;

		private string XML = ".xml";

		private string app = "AppConfig";

		private string fileName;

		private static LocalSettings globalSetting = null;

		private static object syncRoot = new object();

		public static LocalSettings GlobalSettings
		{
			get
			{
				if (globalSetting == null)
				{
					lock (syncRoot)
					{
						if (globalSetting == null)
						{
							new LocalSettings();
						}
					}
				}
				return globalSetting;
			}
		}

		~LocalSettings()
		{
			Dispose();
		}

		private LocalSettings()
		{
			if (globalSetting == null)
			{
				CreateDS();
				globalSetting = this;
			}
		}

		internal LocalSettings(string companyName, string fileName)
		{
			if (companyName != null || companyName.Length > 0)
			{
				app = companyName.Replace(" ", "_");
			}
			this.fileName = fileName;
			CreateDS();
		}

		internal LocalSettings(string companyName)
		{
			if (companyName != null && companyName.Length > 0)
			{
				app = companyName.Replace(" ", "_");
			}
			CreateDS();
		}

		private void CreateDS()
		{
			lock (this)
			{
				try
				{
					SetupXMLFileName(app);
					registry = new XmlRegistry(xmlFile, "LocalSettings");
					rootKey = registry.RootKey;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public void SaveSetting(string key, object val)
		{
			lock (this)
			{
				rootKey[key, true].SetValue("Value", val);
				registry.Save();
			}
		}

		public object GetSetting(string key)
		{
			try
			{
				return GetSetting(key, null);
			}
			catch
			{
				return null;
			}
		}

		public object GetSetting(string key, object defaultValue)
		{
			XmlRegistryKey xmlRegistryKey = rootKey[key, false];
			if (xmlRegistryKey == null)
			{
				return defaultValue;
			}
			return xmlRegistryKey.GetValue("Value", defaultValue);
		}

		public string GetStringSetting(string key, string defaultValue)
		{
			XmlRegistryKey xmlRegistryKey = rootKey[key, false];
			if (xmlRegistryKey == null)
			{
				return defaultValue;
			}
			return xmlRegistryKey.GetStringValue("Value", defaultValue);
		}

		private void SetupXMLFileName(string fn)
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
			directoryName = directoryName + Path.DirectorySeparatorChar.ToString() + "Axolon Local Settings";
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			if (fileName == null)
			{
				xmlFile = directoryName + Path.DirectorySeparatorChar.ToString() + fn + "_" + Environment.UserName + XML;
			}
			else
			{
				xmlFile = directoryName + Path.DirectorySeparatorChar.ToString() + fileName + XML;
			}
		}

		public void ForceSaveData()
		{
			try
			{
				lock (this)
				{
					registry.Save();
				}
			}
			catch
			{
			}
		}

		public void SaveFormProperties(Form form)
		{
			if (form != null && Global.ConStatus == ConnectionStatus.Connected)
			{
				try
				{
					if (form.FormBorderStyle != FormBorderStyle.Sizable && form.FormBorderStyle != FormBorderStyle.SizableToolWindow)
					{
						goto IL_0090;
					}
					if (form.WindowState != FormWindowState.Maximized)
					{
						SaveSetting(form.Name + "Width", form.Width);
						SaveSetting(form.Name + "Height", form.Height);
						SaveSetting(form.Name + "WindowState", checked((byte)form.WindowState));
						goto IL_0090;
					}
					goto end_IL_000c;
					IL_0090:
					SaveSetting(form.Name + "X", form.Location.X);
					SaveSetting(form.Name + "Y", form.Location.Y);
					end_IL_000c:;
				}
				catch
				{
				}
			}
		}

		public void LoadFormProperties(Form form)
		{
			if (form != null)
			{
				int num = form.Width;
				int num2 = form.Height;
				int num3 = form.Location.X;
				int num4 = form.Location.Y;
				object obj = null;
				if (form.FormBorderStyle == FormBorderStyle.Sizable || form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
				{
					try
					{
						obj = GetSetting(form.Name + "Width");
						if (obj != null)
						{
							num = int.Parse(obj.ToString());
						}
					}
					catch (Exception e)
					{
						ErrorHelper.ProcessError(e);
					}
					try
					{
						obj = GetSetting(form.Name + "Height");
						if (obj != null)
						{
							num2 = int.Parse(obj.ToString());
						}
					}
					catch
					{
					}
				}
				try
				{
					obj = GetSetting(form.Name + "X");
					if (obj != null)
					{
						num3 = int.Parse(obj.ToString());
					}
				}
				catch
				{
				}
				try
				{
					obj = GetSetting(form.Name + "Y");
					if (obj != null)
					{
						num4 = int.Parse(obj.ToString());
					}
				}
				catch
				{
				}
				FormWindowState formWindowState = FormWindowState.Normal;
				try
				{
					obj = GetSetting(form.Name + "WindowState");
					if (obj != null)
					{
						formWindowState = (FormWindowState)byte.Parse(obj.ToString());
					}
				}
				catch
				{
				}
				try
				{
					if (num3 > 0 && num4 > 0 && num > 0 && num2 > 0)
					{
						if (form.FormBorderStyle == FormBorderStyle.Sizable || form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
						{
							form.StartPosition = FormStartPosition.Manual;
							form.Width = num;
							form.Height = num2;
						}
						form.Location = new Point(num3, num4);
					}
					else
					{
						switch (formWindowState)
						{
						case FormWindowState.Normal:
							form.StartPosition = FormStartPosition.CenterParent;
							form.WindowState = formWindowState;
							form.BringToFront();
							break;
						case FormWindowState.Minimized:
							form.WindowState = FormWindowState.Normal;
							break;
						default:
							if ((form.FormBorderStyle == FormBorderStyle.Sizable || form.FormBorderStyle == FormBorderStyle.SizableToolWindow) && num > 0 && num2 > 0)
							{
								form.Width = num;
								form.Height = num2;
							}
							form.BringToFront();
							break;
						}
					}
				}
				catch
				{
				}
			}
		}

		public bool SaveTransactionDraft(DataSet data, string key, SysDocTypes sysDocType)
		{
			try
			{
				MemoryStream memoryStream = Global.SerializeToStream(data);
				ISettingSystem settingSystem = Factory.SettingSystem;
				string currentUser = Global.CurrentUser;
				int num = (int)sysDocType;
				return settingSystem.SaveSettingStream(key, currentUser, num.ToString(), memoryStream.ToArray());
			}
			catch
			{
				throw;
			}
		}

		public bool SaveTransactionDraftForDashBoard(DataSet data, string key, SysDocTypes sysDocType, string SysDocID, string VoucherID, int AutoKeyID, bool IsNewRecord)
		{
			try
			{
				MemoryStream memoryStream = Global.SerializeToStream(data);
				ISettingSystem settingSystem = Factory.SettingSystem;
				string currentUser = Global.CurrentUser;
				int num = (int)sysDocType;
				return settingSystem.SaveSettingStreamForDashBoard(key, currentUser, num.ToString(), memoryStream.ToArray(), SysDocID, VoucherID, AutoKeyID, data, IsNewRecord);
			}
			catch
			{
				throw;
			}
		}

		public DataSet LoadTransactionDraft(string key, SysDocTypes sysDocType)
		{
			object obj = Global.DeserializeFromStream(Factory.SettingSystem.GetBinaryData("", key));
			if (obj != null)
			{
				return obj as DataSet;
			}
			return null;
		}

		public DataSet LoadTransactionDraftForTemporary(string key, SysDocTypes sysDocType)
		{
			object obj = Global.DeserializeFromStream(Factory.SettingSystem.GetBinaryTemporaryData("", sysDocType.ToString(), key));
			if (obj != null)
			{
				DataSet dataSet = obj as DataSet;
				if (dataSet != null)
				{
					dataSet.Tables[0].Rows[0]["SysDocID"].ToString();
					dataSet.Tables[0].Rows[0]["VoucherID"].ToString();
				}
				return dataSet;
			}
			return null;
		}

		public bool SaveItemTransactionDraft(DataSet data, string key, SysDocTypes sysDocType)
		{
			try
			{
				WriteXmlToFile(data, key, sysDocType);
				return true;
			}
			catch
			{
				throw;
			}
		}

		public string GetTimestamp(DateTime value)
		{
			return value.ToString("yyyyMMddHHmmss");
		}

		private void WriteXmlToFile(DataSet thisDataSet, string key, SysDocTypes sysDocType)
		{
			if (thisDataSet != null)
			{
				string text = "";
				string timestamp = GetTimestamp(DateTime.Now);
				string text2 = "";
				text2 = ((sysDocType == SysDocTypes.None) ? (key + ".xml") : (key + "_" + timestamp + ".xml"));
				string executablePath = Application.ExecutablePath;
				string str = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				text = ((sysDocType != SysDocTypes.None) ? (str + "\\XmlTransactions") : (str + "\\XmlCards"));
				new FileInfo(text);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				str = text + "\\" + text2;
				if (Directory.Exists(text))
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(new FileStream(str, FileMode.Create), Encoding.Unicode);
					thisDataSet.WriteXml(xmlTextWriter);
					xmlTextWriter.Close();
				}
			}
		}

		public DataSet LoadItemTransactionDraft(string key, SysDocTypes sysDocType)
		{
			string path = key + ".xml";
			new FileStream(path, FileMode.Open);
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(path);
			return dataSet;
		}

		public void Dispose()
		{
			try
			{
				if (registry != null)
				{
					registry.Save();
				}
			}
			catch
			{
			}
		}
	}
}
