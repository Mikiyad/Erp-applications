using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Micromind.Common.Libraries
{
	public class XMLReader
	{
		private static string filePath;

		public static string FilePath
		{
			get
			{
				return filePath;
			}
			set
			{
				filePath = value;
				if (!File.Exists(filePath))
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlWriter xmlWriter = new XmlTextWriter(filePath, Encoding.ASCII);
					xmlWriter.WriteStartElement("xml");
					xmlWriter.WriteAttributeString("xmlns", "x", null, "urn:1");
					xmlWriter.WriteEndElement();
					xmlWriter.Flush();
					xmlWriter.Close();
					xmlDocument.Save(xmlWriter);
				}
			}
		}

		static XMLReader()
		{
			filePath = "";
			filePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
		}

		public static string GetKey(string filePath, string key)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(filePath);
				}
				catch
				{
				}
				return xmlDocument.SelectSingleNode("//" + key).InnerText;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static string GetKey(string key)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(filePath);
				}
				catch
				{
				}
				return xmlDocument.SelectSingleNode("//" + key)?.InnerText;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void CreateKey(string key, string val)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(filePath);
				}
				catch
				{
				}
				if (GetKey(key) != null)
				{
					xmlDocument.SelectSingleNode("//" + key).InnerText = val;
				}
				else
				{
					XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, key, "");
					xmlNode.InnerText = val;
					xmlDocument.DocumentElement.AppendChild(xmlNode);
				}
				xmlDocument.Save(filePath);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
