using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.Common.Data;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Micromind.ClientUI.Libraries
{
	public class FormPermission
	{
		private string formName = "";

		private int screenID = -1;

		private ScreenAreas screenArea = ScreenAreas.General;

		private AccessRigths accessRight = AccessRigths.NoAccess;

		public string FormName
		{
			get
			{
				return formName;
			}
			set
			{
				formName = value;
			}
		}

		public int ScreenID
		{
			get
			{
				return screenID;
			}
			set
			{
				screenID = value;
			}
		}

		public ScreenAreas ScreenArea
		{
			get
			{
				return screenArea;
			}
			set
			{
				screenArea = value;
			}
		}

		public AccessRigths AccessRight
		{
			get
			{
				return accessRight;
			}
			set
			{
				accessRight = value;
			}
		}

		public FormPermission()
		{
		}

		public FormPermission(string formName, ScreenAreas screenArea, int screenID, AccessRigths accessRight)
		{
			this.formName = formName;
			this.screenArea = screenArea;
			this.screenID = screenID;
			this.accessRight = accessRight;
		}

		public void WriteFormsPermissionToXml(string filePath, ArrayList formPermissionList)
		{
			string name = "ScreensPermission";
			string str = "Screen";
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement(name);
			xmlDocument.AppendChild(xmlElement);
			int num = 1;
			foreach (FormPermission formPermission in formPermissionList)
			{
				XmlElement xmlElement2 = xmlDocument.CreateElement(str + num.ToString());
				XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("ID");
				xmlAttribute.Value = formPermission.ScreenID.ToString();
				xmlElement2.Attributes.Append(xmlAttribute);
				xmlAttribute = xmlDocument.CreateAttribute("Area");
				xmlAttribute.Value = ((int)formPermission.ScreenArea).ToString();
				xmlElement2.Attributes.Append(xmlAttribute);
				xmlAttribute = xmlDocument.CreateAttribute("Text");
				xmlAttribute.Value = formPermission.FormName;
				xmlElement2.Attributes.Append(xmlAttribute);
				xmlElement.AppendChild(xmlElement2);
				num = checked(num + 1);
			}
			XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", null, "yes");
			XmlElement documentElement = xmlDocument.DocumentElement;
			xmlDocument.InsertBefore(newChild, documentElement);
			xmlDocument.Save(filePath);
		}

		public ArrayList GetFormsPermissionList(bool createFile)
		{
			string fileName = Process.GetCurrentProcess().MainModule.FileName;
			fileName = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar.ToString() + "screens.xml";
			ArrayList arrayList = null;
			if (!createFile)
			{
				return GetFormPermissionList("Micromind.ClientUI.Documents.screens.xml");
			}
			arrayList = new ArrayList();
			Type[] types = GetType().Assembly.GetTypes();
			Form form = null;
			Type[] array = types;
			foreach (Type type in array)
			{
				if (type.BaseType == typeof(Form) || type.BaseType == typeof(DialogBoxBaseForm))
				{
					form = UIRefelector.GetForm(type);
					if (form != null)
					{
						try
						{
							try
							{
								int num = UIRefelector.GetScreenID(form);
								if (UIRefelector.GetScreenArea(form) != null)
								{
									_ = -1;
								}
							}
							catch
							{
							}
						}
						catch (Exception e)
						{
							ErrorHelper.ProcessError(e);
						}
					}
				}
			}
			try
			{
				WriteFormsPermissionToXml(fileName, arrayList);
				return arrayList;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				return arrayList;
			}
		}

		public ArrayList GetFormsPermissionList()
		{
			return GetFormsPermissionList(createFile: false);
		}

		public ArrayList GetFormPermissionList(string fileName)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(manifestResourceStream);
				}
				catch
				{
					throw;
				}
				XmlNodeList childNodes = xmlDocument.SelectSingleNode("ScreensPermission").ChildNodes;
				ArrayList arrayList = new ArrayList();
				foreach (XmlNode item in childNodes)
				{
					string text = null;
					XmlAttribute xmlAttribute = item.Attributes["ID"];
					if (xmlAttribute != null)
					{
						text = xmlAttribute.Value;
					}
					string text2 = null;
					xmlAttribute = item.Attributes["Area"];
					if (xmlAttribute != null)
					{
						text2 = xmlAttribute.Value;
					}
					string text3 = null;
					xmlAttribute = item.Attributes["Text"];
					if (xmlAttribute != null)
					{
						text3 = xmlAttribute.Value;
					}
					int num = -1;
					ScreenAreas screenAreas = ScreenAreas.NON;
					if (text != null)
					{
						try
						{
							num = int.Parse(text);
						}
						catch
						{
						}
					}
					if (text2 != null)
					{
						try
						{
							screenAreas = (ScreenAreas)int.Parse(text2);
						}
						catch
						{
						}
					}
					FormPermission value = new FormPermission(text3, screenAreas, num, AccessRigths.NoAccess);
					arrayList.Add(value);
				}
				return arrayList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
