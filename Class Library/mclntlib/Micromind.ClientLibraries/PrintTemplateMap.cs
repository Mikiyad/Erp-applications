using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using Micromind.Common.Data;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Micromind.ClientLibraries
{
	public static class PrintTemplateMap
	{
		public static DataSet templateMapData = new DataSet();

		private static string printTemplateFilterByFormID = "";

		public static DataSet PrintTemplateMapData
		{
			get
			{
				ReadFromXml();
				if (PrintTemplateFilterByFormID != "")
				{
					DataRow[] rows = templateMapData.Tables[0].Select("FormID ='" + PrintTemplateFilterByFormID + "'");
					DataSet dataSet = templateMapData.Clone();
					dataSet.Merge(rows);
					templateMapData = dataSet;
				}
				return templateMapData;
			}
		}

		public static string PrintTemplateFilterByFormID
		{
			get
			{
				return printTemplateFilterByFormID;
			}
			set
			{
				printTemplateFilterByFormID = value;
			}
		}

		public static void LoadPrintTemplateMap()
		{
			templateMapData.Tables.Clear();
			SetupMapTable();
			MapFromXml();
		}

		private static void SetupMapTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Code");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("FormID");
			dataTable.Columns.Add("ID");
			templateMapData.Tables.Add(dataTable);
		}

		private static string GetDirectory()
		{
			CompanyInformationData companyInformation = Global.CompanyInformation;
			string str = "\\Print Templates";
			if (companyInformation.CompanyInformationTable.Rows[0]["TemplatePathFolder"] != DBNull.Value)
			{
				str = companyInformation.CompanyInformationTable.Rows[0]["TemplatePathFolder"].ToString();
			}
			return Path.GetDirectoryName(Application.ExecutablePath) + str + "\\Reports";
		}

		public static void ReadFromXml()
		{
			try
			{
				templateMapData.Tables[0].Rows.Clear();
				string text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Axolon Local Settings\\PrintTemplateMap.xml";
				if (File.Exists(text))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(text);
					foreach (XmlNode item in xmlDocument.SelectNodes("//Templates/Template"))
					{
						string text2 = item.Attributes["ID"].Value.ToString();
						string text3 = item.Attributes["FormID"].Value.ToString();
						string text4 = item.Attributes["Title"].Value.ToString();
						templateMapData.Tables[0].Rows.Add(text4, text4, text3, text2);
					}
				}
			}
			catch
			{
			}
		}

		public static void CreateXml()
		{
			string Xmlpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Axolon Local Settings\\PrintTemplateMap.xml";
			string[] files = Directory.GetFiles(GetDirectory(), "*.repx*", SearchOption.AllDirectories);
			XmlDocument xmlDoc = new XmlDocument();
			XmlNode rootNode = xmlDoc.CreateElement("Templates");
			xmlDoc.AppendChild(rootNode);
			Task.Run(delegate
			{
				XtraReport xtraReport = new XtraReport();
				string[] array = files;
				foreach (string path in array)
				{
					xtraReport = XtraReport.FromFile(path);
					if (xtraReport.Parameters.Count == 3)
					{
						string value = xtraReport.Parameters[0].Value.ToString();
						string value2 = xtraReport.Parameters[1].Value.ToString();
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
						XmlNode xmlNode = xmlDoc.CreateElement("Template");
						XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("ID");
						xmlAttribute.Value = value;
						xmlNode.Attributes.Append(xmlAttribute);
						xmlAttribute = xmlDoc.CreateAttribute("FormID");
						xmlAttribute.Value = value2;
						xmlNode.Attributes.Append(xmlAttribute);
						xmlAttribute = xmlDoc.CreateAttribute("Title");
						xmlAttribute.Value = fileNameWithoutExtension;
						xmlNode.Attributes.Append(xmlAttribute);
						xmlNode.InnerText = fileNameWithoutExtension;
						rootNode.AppendChild(xmlNode);
					}
				}
			}).ContinueWith(delegate
			{
				xmlDoc.Save(Xmlpath);
				ReadFromXml();
			});
		}

		private static void MapFromXml()
		{
			try
			{
				if (File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\Axolon Local Settings\\PrintTemplateMap.xml"))
				{
					ReadFromXml();
				}
				else
				{
					CreateXml();
				}
			}
			catch
			{
			}
		}

		public static void AddXmlNode(string Title, string FormID, string ID)
		{
			try
			{
				string text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Axolon Local Settings\\PrintTemplateMap.xml";
				if (File.Exists(text))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(text);
					XmlElement documentElement = xmlDocument.DocumentElement;
					XmlNode xmlNode = xmlDocument.CreateElement("Template");
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("ID");
					xmlAttribute.Value = ID;
					xmlNode.Attributes.Append(xmlAttribute);
					xmlAttribute = xmlDocument.CreateAttribute("FormID");
					xmlAttribute.Value = FormID;
					xmlNode.Attributes.Append(xmlAttribute);
					xmlAttribute = xmlDocument.CreateAttribute("Title");
					xmlAttribute.Value = Title;
					xmlNode.Attributes.Append(xmlAttribute);
					xmlNode.InnerText = Title;
					documentElement.AppendChild(xmlNode);
					xmlDocument.Save(text);
					ReadFromXml();
				}
			}
			catch
			{
			}
		}

		public static void AddParameters(string Title, string FormID, string ID, string file)
		{
			try
			{
				XtraReport xtraReport = new XtraReport();
				xtraReport = XtraReport.FromFile(file);
				if (xtraReport.Parameters.Count < 3)
				{
					Parameter parameter = new Parameter();
					parameter.Name = "FormID";
					parameter.Visible = false;
					parameter.Value = FormID;
					xtraReport.Parameters.Add(parameter);
					parameter.Name = "ID";
					parameter.Visible = false;
					parameter.Value = ID;
					xtraReport.Parameters.Add(parameter);
					parameter.Name = "Title";
					parameter.Visible = false;
					parameter.Value = Title;
					xtraReport.Parameters.Add(parameter);
				}
			}
			catch
			{
			}
		}
	}
}
