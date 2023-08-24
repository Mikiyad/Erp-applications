using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Micromind.Utilities.HTTP.Spider
{
	public class DocumentWorker
	{
		private Uri m_uri;

		private Spider m_spider;

		private Thread m_thread;

		private int m_number;

		public const string IndexFile = "index.html";

		public int Number
		{
			get
			{
				return m_number;
			}
			set
			{
				m_number = value;
			}
		}

		public DocumentWorker(Spider spider)
		{
			m_spider = spider;
		}

		private string convertFilename(Uri uri)
		{
			string text = m_spider.OutputPath;
			if (text[text.Length - 1] != '\\')
			{
				text += "\\";
			}
			string text2 = uri.PathAndQuery;
			int num = text2.IndexOf("?");
			if (num != -1)
			{
				text2 = text2.Substring(0, num);
			}
			int num2 = text2.LastIndexOf('/');
			int num3 = text2.LastIndexOf('.');
			if (text2[text2.Length - 1] != '/' && num2 > num3)
			{
				text2 += "/index.html";
			}
			num2 = text2.LastIndexOf('/');
			string text3 = "";
			if (num2 != -1)
			{
				text3 = text2.Substring(1 + num2);
				text2 = text2.Substring(0, 1 + num2);
				if (text3.Equals(""))
				{
					text3 = "index.html";
				}
			}
			int num4 = 1;
			int num5;
			do
			{
				num5 = text2.IndexOf('/', num4);
				if (num5 != -1)
				{
					string str = text2.Substring(num4, num5 - num4);
					text += str;
					text += "\\";
					Directory.CreateDirectory(text);
					num4 = num5 + 1;
				}
			}
			while (num5 != -1);
			return text + text3;
		}

		private void SaveBinaryFile(WebResponse response)
		{
			byte[] array = new byte[1024];
			if (m_spider.OutputPath == null)
			{
				return;
			}
			Stream stream = File.Create(convertFilename(response.ResponseUri));
			Stream responseStream = response.GetResponseStream();
			int num;
			do
			{
				num = responseStream.Read(array, 0, array.Length);
				if (num > 0)
				{
					stream.Write(array, 0, num);
				}
			}
			while (num > 0);
			stream.Close();
			responseStream.Close();
		}

		private void SaveTextFile(string buffer)
		{
			if (m_spider.OutputPath != null)
			{
				StreamWriter streamWriter = new StreamWriter(convertFilename(m_uri));
				streamWriter.Write(buffer);
				streamWriter.Close();
			}
		}

		private string GetPage()
		{
			WebResponse webResponse = null;
			Stream stream = null;
			StreamReader streamReader = null;
			try
			{
				webResponse = ((HttpWebRequest)WebRequest.Create(m_uri)).GetResponse();
				stream = webResponse.GetResponseStream();
				if (!webResponse.ContentType.ToLower().StartsWith("text/"))
				{
					SaveBinaryFile(webResponse);
					return null;
				}
				string text = "";
				streamReader = new StreamReader(stream);
				string str;
				while ((str = streamReader.ReadLine()) != null)
				{
					text = text + str + "\r\n";
				}
				SaveTextFile(text);
				return text;
			}
			catch (WebException arg)
			{
				Console.WriteLine("Can't download:" + arg);
				return null;
			}
			catch (IOException arg2)
			{
				Console.WriteLine("Can't download:" + arg2);
				return null;
			}
			finally
			{
				streamReader?.Close();
				stream?.Close();
				webResponse?.Close();
			}
		}

		private void ProcessLink(string link)
		{
			Uri uri;
			try
			{
				uri = new Uri(m_uri, link, dontEscape: false);
			}
			catch (UriFormatException)
			{
				Console.WriteLine("Invalid URI:" + link);
				return;
			}
			if ((uri.Scheme.ToLower().Equals("http") || uri.Scheme.ToLower().Equals("https")) && uri.Host.ToLower().Equals(m_uri.Host.ToLower()))
			{
				m_spider.addURI(uri);
			}
		}

		private void ProcessPage(string page)
		{
			ParseHTML parseHTML = new ParseHTML();
			parseHTML.Source = page;
			while (!parseHTML.Eof())
			{
				if (parseHTML.Parse() == '\0')
				{
					Attribute attribute = parseHTML.GetTag()["HREF"];
					if (attribute != null)
					{
						ProcessLink(attribute.Value);
					}
					attribute = parseHTML.GetTag()["SRC"];
					if (attribute != null)
					{
						ProcessLink(attribute.Value);
					}
				}
			}
		}

		public void Process()
		{
			while (!m_spider.Quit)
			{
				m_uri = m_spider.ObtainWork();
				m_spider.SpiderDone.WorkerBegin();
				Console.WriteLine("Download(" + Number + "):" + m_uri);
				string page = GetPage();
				if (page != null)
				{
					ProcessPage(page);
				}
				m_spider.SpiderDone.WorkerEnd();
			}
		}

		public void start()
		{
			ThreadStart start = Process;
			m_thread = new Thread(start);
			m_thread.Start();
		}
	}
}
