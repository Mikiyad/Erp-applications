using Micromind.Utilities.HTTP;
using System;
using System.Xml;

namespace Micromind.Utilities.AppUpdater
{
	public class AutoUpdateConfig
	{
		private bool isMultiUser;

		private string _AvailableVersion;

		private string _AppFileURL;

		private string _LatestChanges;

		private bool hasSetup;

		public bool IsMultiUser
		{
			set
			{
				isMultiUser = value;
			}
		}

		public string AvailableVersion
		{
			get
			{
				return _AvailableVersion;
			}
			set
			{
				_AvailableVersion = value;
			}
		}

		public string AppFileURL
		{
			get
			{
				return _AppFileURL;
			}
			set
			{
				_AppFileURL = value;
			}
		}

		public string LatestChanges
		{
			get
			{
				return _LatestChanges;
			}
			set
			{
				_LatestChanges = value;
			}
		}

		public bool HasSetup
		{
			get
			{
				return hasSetup;
			}
			set
			{
				hasSetup = value;
			}
		}

		public bool LoadConfig(string url, string user, string pass)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				new HTTPClient(user, pass).GetStream(url);
				xmlDocument.Load("c:\\PXF196.xml");
				try
				{
					XmlNode xmlNode = xmlDocument.SelectSingleNode("//AvailableVersion");
					AvailableVersion = xmlNode.InnerText;
				}
				catch
				{
				}
				try
				{
					XmlNode xmlNode2 = isMultiUser ? xmlDocument.SelectSingleNode("//ServerAppPathURL") : xmlDocument.SelectSingleNode("//ClientAppPathURL");
					AppFileURL = xmlNode2.InnerText;
				}
				catch
				{
				}
				try
				{
					XmlNode xmlNode3 = xmlDocument.SelectSingleNode("//LatestChanges");
					if (xmlNode3 != null)
					{
						LatestChanges = xmlNode3.InnerText;
					}
					else
					{
						LatestChanges = "";
					}
				}
				catch
				{
				}
				try
				{
					XmlNode xmlNode4 = xmlDocument.SelectSingleNode("//HasSetup");
					hasSetup = bool.Parse(xmlNode4.InnerText);
				}
				catch
				{
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;
		}
	}
}
