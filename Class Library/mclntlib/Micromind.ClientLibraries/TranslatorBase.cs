using Micromind.Common;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

using System.Text;
using System.Xml;

namespace Micromind.ClientLibraries
{
	
	public class TranslatorBase
	{
		protected static bool isTranslatorActive = false;

		protected static StringDictionary data = new StringDictionary();

		protected static StringDictionary prevData = new StringDictionary();

		protected static bool isRead = true;

		protected static bool overwriteExistingKey = false;

		public static string translatingFile = "";

		protected static string languageFiles = "";

		private static string defaultLanguageName = "English";

		private static XmlDocument XmlDoc = null;

		protected static bool isFileExist = false;

		private static bool isLoaded = false;

		protected bool isCreatingTranslation;

		public bool IsTranslatorActive
		{
			get
			{
				return isTranslatorActive;
			}
			set
			{
				isTranslatorActive = value;
				LocalSettings.GlobalSettings.SaveSetting("IsTranslatorActive", value);
			}
		}

		public string TranslatingFile
		{
			get
			{
				if (isCreatingTranslation)
				{
					return GetDefaultLanguageFile();
				}
				if (translatingFile == string.Empty)
				{
					IsTranslatorActive = false;
					translatingFile = GetLanguageFile(defaultLanguageName);
					if (translatingFile == null || translatingFile == string.Empty || !File.Exists(translatingFile))
					{
						string[] languages = GetLanguages();
						if (languages.Length != 0)
						{
							defaultLanguageName = languages[0];
							translatingFile = GetLanguageFile(defaultLanguageName);
						}
						else
						{
							isFileExist = false;
							translatingFile = "";
						}
					}
				}
				return translatingFile;
			}
			set
			{
				translatingFile = value;
			}
		}

		public string LanguagePath
		{
			get
			{
				if (languageFiles == string.Empty)
				{
					languageFiles = StoreConfiguration.OriginalDiectory + Path.DirectorySeparatorChar.ToString() + "Languages";
				}
				if (!Directory.Exists(languageFiles))
				{
					Directory.CreateDirectory(languageFiles);
				}
				return languageFiles;
			}
			set
			{
				languageFiles = value;
			}
		}

		public bool OverwriteExistingKey
		{
			set
			{
				overwriteExistingKey = value;
			}
		}

		protected TranslatorBase(bool isRead)
		{
			XmlDoc = new XmlDocument();
			if (!isRead)
			{
				XmlDoc.CreateXmlDeclaration("1.0", "UTF-16", null);
			}
			TranslatorBase.isRead = isRead;
			if (isLoaded)
			{
				return;
			}
			try
			{
				isTranslatorActive = bool.Parse(LocalSettings.GlobalSettings.GetSetting("IsTranslatorActive", false).ToString());
			}
			catch
			{
			}
			if (isTranslatorActive)
			{
				string languageName = LocalSettings.GlobalSettings.GetSetting("DefaultLanguage", defaultLanguageName).ToString();
				ChangeLanguage(languageName);
				if (!File.Exists(TranslatingFile) && !isRead)
				{
					CreateFile();
				}
			}
			isLoaded = true;
		}

		public string GetDefaultLanguage()
		{
			return defaultLanguageName;
		}

		private string GetDefaultLanguageFile()
		{
			return LanguagePath + Path.DirectorySeparatorChar.ToString() + GetDefaultLanguage() + ".xml";
		}

		public void ChangeLanguage(string languageName)
		{
			IsTranslatorActive = true;
			defaultLanguageName = languageName;
			try
			{
				TranslatingFile = GetLanguageFile(defaultLanguageName);
				Refresh();
				LocalSettings.GlobalSettings.SaveSetting("DefaultLanguage", defaultLanguageName);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				isFileExist = false;
			}
		}

		protected void CreateFile()
		{
			string defaultLanguageFile = GetDefaultLanguageFile();
			if (!File.Exists(defaultLanguageFile))
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(defaultLanguageFile, Encoding.UTF8);
				xmlTextWriter.WriteStartDocument();
				xmlTextWriter.WriteStartElement("Language");
				xmlTextWriter.WriteStartElement("Name");
				xmlTextWriter.WriteString(GetDefaultLanguage());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndDocument();
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
		}

		public void Refresh()
		{
			if (!File.Exists(TranslatingFile))
			{
				isFileExist = false;
				return;
			}
			isFileExist = true;
			try
			{
				XmlDoc.Load(TranslatingFile);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				isFileExist = false;
			}
		}

		public void WriteText(string text)
		{
			if (isFileExist && text != null && !(text.Trim() == string.Empty))
			{
				string text2 = "";
				text = text.Trim();
				int hashCode = text.GetHashCode();
				text2 = ((hashCode >= 0) ? ("MP" + hashCode.ToString()) : ("MN" + Math.Abs((decimal)hashCode).ToString()));
				CreateKey(text2, text);
			}
		}

		private string GetPrevTextKey(string text)
		{
			foreach (DictionaryEntry prevDatum in prevData)
			{
				if (prevDatum.Value != null && prevDatum.Value.ToString() == text)
				{
					return (prevDatum.Key != null) ? prevDatum.Key.ToString().Trim() : "";
				}
			}
			return "";
		}

		private string GetTextKey(string text)
		{
			foreach (DictionaryEntry datum in data)
			{
				if (datum.Value != null && datum.Value.ToString() == text)
				{
					return (datum.Key != null) ? datum.Key.ToString().Trim() : "";
				}
			}
			return "";
		}

		public string GetText(string text)
		{
			if (!isFileExist)
			{
				return text;
			}
			if (text == null || text.Trim() == string.Empty)
			{
				return text;
			}
			string text2 = "";
			text = text.Trim();
			if (data.ContainsValue(text))
			{
				text2 = GetTextKey(text);
				text2 = text2.ToUpper();
			}
			else if (prevData.ContainsValue(text))
			{
				text2 = GetPrevTextKey(text);
				text2 = text2.ToUpper();
			}
			else
			{
				int hashCode = text.GetHashCode();
				text2 = ((hashCode >= 0) ? ("MP" + hashCode.ToString()) : ("MN" + Math.Abs((decimal)hashCode).ToString()));
			}
			string text3 = GetKey(text2);
			if (text3 != null && text3 != string.Empty)
			{
				if (data.ContainsKey(text2))
				{
					data[text2] = text3;
				}
				else
				{
					data.Add(text2, text3);
				}
				if (prevData.ContainsKey(text2))
				{
					prevData[text2] = text;
				}
				else
				{
					prevData.Add(text2, text);
				}
			}
			else
			{
				text3 = text;
			}
			return text3;
		}

		private string GetKey(string key)
		{
			try
			{
				return XmlDoc.SelectSingleNode("//" + key)?.InnerText;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void CreateKey(string key, string val)
		{
			try
			{
				if (GetKey(key) != null)
				{
					if (overwriteExistingKey)
					{
						XmlDoc.SelectSingleNode("//" + key).InnerText = val;
						XmlDoc.Save(TranslatingFile);
					}
				}
				else
				{
					XmlNode xmlNode = XmlDoc.CreateNode(XmlNodeType.Element, key, "");
					xmlNode.InnerText = val;
					XmlDoc.DocumentElement.AppendChild(xmlNode);
					XmlDoc.Save(TranslatingFile);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void GetSubdirFiles(string dir, string searchPattern, StringCollection strCol)
		{
			string[] directories = Directory.GetDirectories(dir);
			if (directories.Length != 0)
			{
				string[] array = directories;
				foreach (string text in array)
				{
					string[] files = Directory.GetFiles(text, searchPattern);
					strCol.AddRange(files);
					GetSubdirFiles(text, searchPattern, strCol);
				}
			}
		}

		public string[] GetLanguages()
		{
			string languagePath = LanguagePath;
			ArrayList arrayList = new ArrayList();
			StringCollection stringCollection = new StringCollection();
			string[] files = Directory.GetFiles(languagePath, "*.xml");
			stringCollection.AddRange(files);
			GetSubdirFiles(languagePath, "*.xml", stringCollection);
			XmlDocument xmlDocument = new XmlDocument();
			StringEnumerator enumerator = stringCollection.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					try
					{
						xmlDocument.Load(current);
						XmlNode xmlNode = xmlDocument.SelectSingleNode("//Name");
						if (xmlNode != null)
						{
							arrayList.Add(xmlNode.InnerText);
						}
						else
						{
							arrayList.Add(Path.GetFileNameWithoutExtension(current));
						}
					}
					catch (Exception)
					{
					}
				}
			}
			finally
			{
				(enumerator as IDisposable)?.Dispose();
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		public string GetLanguageFile(string language)
		{
			string languagePath = LanguagePath;
			StringCollection stringCollection = new StringCollection();
			string[] files = Directory.GetFiles(languagePath, "*.xml");
			stringCollection.AddRange(files);
			GetSubdirFiles(languagePath, "*.xml", stringCollection);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.SelectNodes("//Languages//Language//Name");
			StringEnumerator enumerator = stringCollection.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					try
					{
						xmlDocument.Load(current);
						XmlNode xmlNode = xmlDocument.SelectSingleNode("//Name");
						if (xmlNode != null)
						{
							if (xmlNode.InnerText.Trim().ToLower() == language.Trim().ToLower())
							{
								return current;
							}
						}
						else if (Path.GetFileNameWithoutExtension(current).ToLower() == language.Trim().ToLower())
						{
							return current;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			finally
			{
				(enumerator as IDisposable)?.Dispose();
			}
			return "";
		}
	}
}
