namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPReply
	{
		private string replyCode;

		private string replyText;

		private string[] data;

		public virtual string ReplyCode => replyCode;

		public virtual string ReplyText => replyText;

		public virtual string[] ReplyData => data;

		internal FTPReply(string replyCode, string replyText)
		{
			this.replyCode = replyCode;
			this.replyText = replyText;
		}

		internal FTPReply(string replyCode, string replyText, string[] data)
		{
			this.replyCode = replyCode;
			this.replyText = replyText;
			this.data = data;
		}

		internal FTPReply(string rawReply)
		{
			rawReply = rawReply.Trim();
			replyCode = rawReply.Substring(0, 3);
			if (rawReply.Length > 3)
			{
				replyText = rawReply.Substring(4);
			}
			else
			{
				replyText = "";
			}
		}
	}
}
