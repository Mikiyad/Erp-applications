using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Micromind.ClientLibraries
{
	public class XmlRegistry
	{
		private FileInfo Fileinfo;

		private XmlDocument Doc = new XmlDocument();

		private const string MsgPrefix = "LocalSettings";

		public XmlRegistryKey RootKey => new XmlRegistryKey(Doc.DocumentElement, this);

		public void Save()
		{
			try
			{
				Doc.Save(Fileinfo.FullName);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e, "Unable to save the settings.");
			}
		}

		public XmlRegistry(string filename, string rootkeyname)
		{
			try
			{
				Fileinfo = new FileInfo(filename);
				if (Fileinfo.Exists)
				{
					Doc.Load(Fileinfo.FullName);
					if (rootkeyname != null && Doc.DocumentElement.Name != rootkeyname)
					{
						DataSet dataSet = new DataSet();
						dataSet.ReadXml(Fileinfo.FullName);
						CreateDocument(rootkeyname);
						DataRow[] array = dataSet.Tables["T1"].Select();
						if (array.Length != 0)
						{
							DataRow[] array2 = array;
							foreach (DataRow dataRow in array2)
							{
								try
								{
									string text = dataRow["Key"].ToString();
									string val = dataRow["Value"].ToString();
									if (text.Trim() != string.Empty)
									{
										RootKey[text, true].SetValue("Value", val);
									}
								}
								catch
								{
								}
							}
							Save();
							dataSet.Dispose();
							dataSet = null;
						}
					}
				}
				else
				{
					CreateDocument(rootkeyname);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Unable to load " + rootkeyname + ". Please fix the error or delete the setting file which is located at 'Axolon Local Settings'.\nSetting File: " + Fileinfo.FullName + ".\n" + ex.Message);
			}
		}

		private void CreateDocument(string rootkeyname)
		{
			Doc = null;
			Doc = new XmlDocument();
			if (rootkeyname == null)
			{
				rootkeyname = "Settings";
			}
			XmlDeclaration newChild = Doc.CreateXmlDeclaration("1.0", null, "yes");
			Doc.AppendChild(newChild);
			XmlElement newChild2 = Doc.CreateElement(rootkeyname);
			Doc.AppendChild(newChild2);
			Save();
		}

		public XmlRegistry(string filename)
			: this(filename, null)
		{
		}
	}
}
