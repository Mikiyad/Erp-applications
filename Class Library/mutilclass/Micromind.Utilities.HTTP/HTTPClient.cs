using System.IO;
using System.Net;
using System.Text;

namespace Micromind.Utilities.HTTP
{
	public class HTTPClient
	{
		private string userID;

		private string password;

		public string UserID
		{
			set
			{
				userID = value;
			}
		}

		public string Password
		{
			set
			{
				password = value;
			}
		}

		public HTTPClient(string userID, string password)
		{
			UserID = userID;
			Password = password;
		}

		public HTTPClient()
		{
		}

		public Stream GetStream(string url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Headers.Add("Translate: f");
			if (userID != null && userID != "")
			{
				httpWebRequest.Credentials = new NetworkCredential(userID, password);
			}
			else
			{
				httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			}
			httpWebRequest.Proxy = WebProxy.GetDefaultProxy();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			Stream stream = null;
			stream = httpWebResponse.GetResponseStream();
			httpWebResponse?.Close();
			httpWebRequest = null;
			return stream;
		}

		public string GetPage(string url)
		{
			if (url.IndexOf("http://", 0, 7) < 0)
			{
				url = "http://" + url;
			}
			WebResponse webResponse = null;
			Stream stream = null;
			StreamReader streamReader = null;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				if (userID != null && userID != "")
				{
					httpWebRequest.Credentials = new NetworkCredential(userID, password);
				}
				else
				{
					httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
				}
				httpWebRequest.Proxy = WebProxy.GetDefaultProxy();
				webResponse = httpWebRequest.GetResponse();
				stream = webResponse.GetResponseStream();
				if (!webResponse.ContentType.ToLower().StartsWith("text/"))
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				streamReader = new StreamReader(stream);
				string value;
				while ((value = streamReader.ReadLine()) != null)
				{
					stringBuilder.Append(value).Append("\r\n");
				}
				return stringBuilder.ToString();
			}
			catch
			{
				throw;
			}
			finally
			{
				streamReader?.Close();
				stream?.Close();
				webResponse?.Close();
			}
		}
	}
}
