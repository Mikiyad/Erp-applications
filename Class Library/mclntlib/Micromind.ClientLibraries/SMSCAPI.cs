using System;
using System.IO;
using System.Net;

namespace Micromind.ClientLibraries
{
	public sealed class SMSCAPI
	{
		private WebProxy objProxy1;

		public string SendSMS(string User, string password, string Mobile_Number, string Message, string option1, string option2, string option3, string SID)
		{
			string text = null;
			text = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&SID=" + SID;
			HttpWebRequest httpWebRequest = null;
			StreamWriter streamWriter = null;
			StreamReader streamReader = null;
			try
			{
				string text2 = null;
				httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice_bulk.aspx");
				httpWebRequest.Method = "POST";
				if (objProxy1 != null)
				{
					httpWebRequest.Proxy = objProxy1;
				}
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
				streamWriter.Write(text);
				streamWriter.Flush();
				streamWriter.Close();
				streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
				text2 = streamReader.ReadToEnd();
				streamReader.Close();
				return text2;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				streamWriter?.Close();
				streamReader?.Close();
				httpWebRequest = null;
				objProxy1 = null;
			}
		}

		private void StreamReader(Stream stream)
		{
			throw new NotImplementedException();
		}

		public string SendSMS(string User, string password, string Mobile_Number, string Message, string Mtype)
		{
			object value = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&MType=" + Mtype;
			HttpWebRequest httpWebRequest = null;
			StreamWriter streamWriter = null;
			StreamReader streamReader = null;
			try
			{
				string text = null;
				httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice_bulk.aspx?");
				httpWebRequest.Method = "POST";
				if (objProxy1 != null)
				{
					httpWebRequest.Proxy = objProxy1;
				}
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
				streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
				text = streamReader.ReadToEnd();
				streamReader.Close();
				return text;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				streamWriter?.Close();
				streamReader?.Close();
				httpWebRequest = null;
				objProxy1 = null;
			}
		}

		public string SendSMS(string User, string password, string Mobile_Number, string Message, string Mtype, string DR)
		{
			object value = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&MType=" + Mtype + "&DR=" + DR;
			HttpWebRequest httpWebRequest = null;
			StreamWriter streamWriter = null;
			StreamReader streamReader = null;
			try
			{
				string text = null;
				httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice_bulk.aspx?");
				httpWebRequest.Method = "POST";
				if (objProxy1 != null)
				{
					httpWebRequest.Proxy = objProxy1;
				}
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
				streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
				text = streamReader.ReadToEnd();
				streamReader.Close();
				return text;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				streamWriter?.Close();
				streamReader?.Close();
				httpWebRequest = null;
				objProxy1 = null;
			}
		}

		public string SendSMS(string User, string password, string Mobile_Number, string Message, string Mtype, string DR, string SID)
		{
			object value = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&MType=" + Mtype + "&DR=" + DR + "&SID=" + SID;
			HttpWebRequest httpWebRequest = null;
			StreamWriter streamWriter = null;
			StreamReader streamReader = null;
			try
			{
				string text = null;
				httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice_bulk.aspx?");
				httpWebRequest.Method = "POST";
				if (objProxy1 != null)
				{
					httpWebRequest.Proxy = objProxy1;
				}
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
				streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
				text = streamReader.ReadToEnd();
				streamReader.Close();
				return text;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				streamWriter?.Close();
				streamReader?.Close();
				httpWebRequest = null;
				objProxy1 = null;
			}
		}
	}
}
