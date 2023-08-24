using Micromind.Common.Data;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Micromind.ClientLibraries
{
	public class MailHelper
	{
		private MailMessage mailMessage = new MailMessage();

		private string outServer = "";

		private string userName = "";

		private string password = "";

		private string displayName = "";

		private string from = "";

		private bool useSSL;

		private int smtpPort = 25;

		public AttachmentCollection Attachments => mailMessage.Attachments;

		public MailHelper(CompanyEmailConfigTypes config)
		{
			DataSet emailConfig = Factory.CompanyInformationSystem.GetEmailConfig(config);
			if (emailConfig.Tables["Email_Config"].Rows.Count > 0)
			{
				DataRow dataRow = emailConfig.Tables["Email_Config"].Rows[0];
				outServer = dataRow["OutgoingServer"].ToString();
				userName = dataRow["UserName"].ToString();
				password = dataRow["EmailPass"].ToString();
				displayName = dataRow["SenderName"].ToString();
				from = dataRow["EmailAddress"].ToString();
				useSSL = bool.Parse(dataRow["EmailUseSSL"].ToString());
				smtpPort = int.Parse(dataRow["EmailSMTPPort"].ToString());
			}
		}

		public bool SendMail(string to, string subject, string body, bool isBodyHtml)
		{
			try
			{
				mailMessage.SubjectEncoding = Encoding.UTF8;
				mailMessage.HeadersEncoding = Encoding.UTF8;
				mailMessage.Subject = subject;
				mailMessage.IsBodyHtml = isBodyHtml;
				mailMessage.BodyEncoding = Encoding.Unicode;
				mailMessage.Body = body;
				mailMessage.To.Add(to);
				mailMessage.From = new MailAddress(from, displayName);
				SmtpClient smtpClient = new SmtpClient();
				smtpClient.Port = smtpPort;
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Host = outServer;
				smtpClient.EnableSsl = useSSL;
				smtpClient.Credentials = new NetworkCredential(userName, password);
				smtpClient.Send(mailMessage);
				return true;
			}
			catch
			{
				throw;
			}
		}
	}
}
