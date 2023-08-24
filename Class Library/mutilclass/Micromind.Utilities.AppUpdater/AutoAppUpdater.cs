using Micromind.Common.Libraries;
using Micromind.Utilities.Zip;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.Utilities.AppUpdater
{
	public class AutoAppUpdater : Component
	{
		private bool isCanceled;

		private bool isMultiUser;

		private string _LoginUserName;

		private string _LoginUserPass;

		private string _ConfigURL;

		private bool _AutoRestart;

		public string executableFileName = "";

		private Form _RestartForm;

		private Form _StatusForm;

		private string _LatestConfigChanges;

		private static string filePath = null;

		private string strError = "";

		private bool isDeletePackageFile = true;

		private string stUpdateName = "Patch";

		private static string stUpdateDirName = "Downloads";

		private string stUpdateDirPath;

		public bool IsMultiUser
		{
			set
			{
				isMultiUser = value;
			}
		}

		[DefaultValue("")]
		[Description("The UserName to authenticate with.")]
		[Category("AutoAppUpdater Configuration")]
		public string LoginUserName
		{
			get
			{
				return _LoginUserName;
			}
			set
			{
				_LoginUserName = value;
			}
		}

		[DefaultValue("")]
		[Description("The Password to authenticate with.")]
		[Category("AutoAppUpdater Configuration")]
		public string LoginUserPass
		{
			get
			{
				return _LoginUserPass;
			}
			set
			{
				_LoginUserPass = value;
			}
		}

		[DefaultValue("http://localhost/UpdateConfig.axo")]
		[Description("The URL Path to the configuration file.")]
		[Category("AutoAppUpdater Configuration")]
		public string ConfigURL
		{
			get
			{
				return _ConfigURL;
			}
			set
			{
				_ConfigURL = value;
			}
		}

		[DefaultValue(false)]
		[Description("Set to True if you want the app to restart automatically, set to False if you want to use the RestartForm to prompt the user, if RestartForm is null, the app will not restart.")]
		[Category("AutoAppUpdater Configuration")]
		public bool AutoRestart
		{
			get
			{
				return _AutoRestart;
			}
			set
			{
				_AutoRestart = value;
			}
		}

		public string ExecutableFileName => executableFileName;

		public Form RestartForm
		{
			get
			{
				return _RestartForm;
			}
			set
			{
				if (value != null && !(value is IConfirmForm))
				{
					throw new ApplicationException("The form must be of type " + Assembly.GetAssembly(typeof(IConfirmForm)).FullName);
				}
				_RestartForm = value;
			}
		}

		public Form StatusForm
		{
			get
			{
				return _StatusForm;
			}
			set
			{
				_StatusForm = value;
				IStatusForm statusForm = value as IStatusForm;
				if (statusForm != null)
				{
					statusForm.CanceledByUser += form_CanceledByUser;
				}
			}
		}

		[Browsable(false)]
		public string LatestConfigChanges
		{
			get
			{
				return _LatestConfigChanges;
			}
			set
			{
				_LatestConfigChanges = value;
			}
		}

		public static string FilePath
		{
			get
			{
				return filePath;
			}
			set
			{
				filePath = value;
			}
		}

		public string ErrorMessage => strError;

		public bool IsDeletePackageFile
		{
			set
			{
				isDeletePackageFile = value;
			}
		}

		public static string UpdateDirName
		{
			get
			{
				string key = XMLReader.GetKey("DownloadDir");
				if (key != null && key.Trim() != string.Empty)
				{
					return key;
				}
				return stUpdateDirName;
			}
			set
			{
				stUpdateDirName = value;
				XMLReader.CreateKey("DownloadDir", value);
			}
		}

		public string UpdateDirectoryPath
		{
			get
			{
				return stUpdateDirPath;
			}
			set
			{
				stUpdateDirPath = value;
			}
		}

		public event EventHandler UpdateSucessfull;

		public event EventHandler UpdateCanceled;

		public event EventHandler EqualVersionExist;

		public event EventHandler FatalError;

		public event EventHandler UpdateStarted;

		public event EventHandler UpdatedLenght;

		public event EventHandler UpdateMaxLenght;

		public void TryUpdate()
		{
			try
			{
				Thread thread = new Thread(updateThread);
				thread.IsBackground = true;
				thread.Start();
			}
			catch
			{
				throw;
			}
		}

		private void updateThread()
		{
			try
			{
				AutoUpdateConfig autoUpdateConfig = new AutoUpdateConfig();
				autoUpdateConfig.IsMultiUser = isMultiUser;
				XMLReader.FilePath = FilePath;
				ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
				if (autoUpdateConfig.LoadConfig(ConfigURL, LoginUserName, LoginUserPass))
				{
					LatestConfigChanges = autoUpdateConfig.LatestChanges;
					Version version = null;
					try
					{
						string text = XMLReader.GetKey("CurrentVersion");
						if (double.Parse(text) < 1.0 && text.IndexOf("0", 0, 1) < 0)
						{
							text = "0" + text;
						}
						if (text != null)
						{
							if (text.IndexOf(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator) < 0)
							{
								text = text + Application.CurrentCulture.NumberFormat.NumberDecimalSeparator + "0";
							}
							version = new Version(text);
						}
						else
						{
							XMLReader.CreateKey("CurrentVersion", autoUpdateConfig.AvailableVersion);
							version = new Version(1, 0);
						}
					}
					catch
					{
						XMLReader.CreateKey("CurrentVersion", autoUpdateConfig.AvailableVersion);
						version = new Version(1, 0);
					}
					if (new Version(autoUpdateConfig.AvailableVersion) > version)
					{
						string text2 = "";
						if (stUpdateDirPath == null || stUpdateDirPath == string.Empty)
						{
							stUpdateDirPath = Application.StartupPath + Path.DirectorySeparatorChar.ToString() + stUpdateDirName;
							Directory.CreateDirectory(stUpdateDirPath);
						}
						DirectoryInfo directoryInfo = new DirectoryInfo(stUpdateDirPath);
						text2 = directoryInfo.FullName + Path.DirectorySeparatorChar.ToString() + stUpdateName + ".zip";
						if (downloadFile(autoUpdateConfig.AppFileURL, text2))
						{
							_ = directoryInfo.Parent.FullName + Path.DirectorySeparatorChar.ToString() + stUpdateName + Path.DirectorySeparatorChar.ToString();
							unzip(text2, directoryInfo.FullName);
							if (isDeletePackageFile)
							{
								FileUtility.DeleteFile(text2);
							}
							if (0 == 0)
							{
								if (this.UpdateSucessfull != null)
								{
									this.UpdateSucessfull(this, null);
								}
							}
							else if (this.UpdateCanceled != null)
							{
								this.UpdateCanceled(this, null);
							}
							if (RestartForm != null)
							{
								(RestartForm as IConfirmForm).DownloadPath = UpdateDirectoryPath;
								RestartForm.ShowDialog();
							}
						}
						else
						{
							strError = "Could not download files.";
							if (this.FatalError != null)
							{
								this.FatalError(this, null);
							}
						}
					}
					else if (this.EqualVersionExist != null)
					{
						this.EqualVersionExist(this, null);
					}
				}
				else
				{
					strError = "Problem loading config file. " + ConfigURL;
					if (this.FatalError != null)
					{
						this.FatalError(this, null);
					}
				}
			}
			catch (Exception ex)
			{
				strError = ex.Message;
				if (this.FatalError != null)
				{
					this.FatalError(this, null);
				}
			}
		}

		private bool downloadFile(string url, string path)
		{
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Headers.Add("Translate: f");
				if (LoginUserName != null && LoginUserName != "")
				{
					httpWebRequest.Credentials = new NetworkCredential(LoginUserName, LoginUserPass);
				}
				else
				{
					httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
				}
				HttpWebResponse obj = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream stream = null;
				stream = obj.GetResponseStream();
				byte[] array = new byte[4096];
				FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
				if (this.UpdateStarted != null)
				{
					this.UpdateStarted(fileStream.Length, null);
				}
				Application.DoEvents();
				int num = stream.Read(array, 0, 4096);
				if (this.UpdateMaxLenght != null)
				{
					this.UpdateMaxLenght(num, null);
				}
				while (num > 0)
				{
					fileStream.Write(array, 0, num);
					if (isCanceled)
					{
						return false;
					}
					num = stream.Read(array, 0, 4096);
					if (this.UpdatedLenght != null)
					{
						this.UpdatedLenght(array.Length, null);
					}
					Application.DoEvents();
				}
				fileStream.Close();
			}
			catch (Exception ex)
			{
				if (File.Exists(path))
				{
					try
					{
						FileUtility.DeleteFile(path);
					}
					catch
					{
					}
				}
				strError = ex.Message;
				if (this.FatalError != null)
				{
					this.FatalError(this, null);
				}
			}
			return true;
		}

		private bool unzip(string stZipPath, string stDestPath)
		{
			ZipInputStream zipInputStream = null;
			if (Path.GetExtension(stZipPath).ToLower() != ".exe")
			{
				zipInputStream = new ZipInputStream(File.OpenRead(stZipPath));
				bool flag = false;
				ZipEntry nextEntry;
				while ((nextEntry = zipInputStream.GetNextEntry()) != null)
				{
					string path = stDestPath + Path.GetDirectoryName(nextEntry.Name) + Path.DirectorySeparatorChar.ToString() + Path.GetFileName(nextEntry.Name);
					Directory.CreateDirectory(Path.GetDirectoryName(path));
					if (nextEntry.IsDirectory)
					{
						continue;
					}
					FileStream fileStream = File.Create(path);
					int num = 2048;
					byte[] array = new byte[2048];
					while (true)
					{
						num = zipInputStream.Read(array, 0, array.Length);
						if (num <= 0)
						{
							break;
						}
						fileStream.Write(array, 0, num);
					}
					if (!flag && Path.GetExtension(path).ToLower() == ".exe")
					{
						executableFileName = path;
					}
					fileStream.Close();
				}
			}
			else if (Path.GetExtension(stZipPath).ToLower() == ".exe")
			{
				executableFileName = stZipPath;
			}
			zipInputStream?.Close();
			return true;
		}

		private void restart()
		{
		}

		private void form_CanceledByUser(object sender, EventArgs e)
		{
			isCanceled = true;
		}

		public void CancelDownload()
		{
			isCanceled = true;
		}
	}
}
