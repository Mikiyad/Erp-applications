using System;

namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPException : ApplicationException
	{
		private int replyCode = -1;

		public virtual int ReplyCode => replyCode;

		public FTPException(string msg)
			: base(msg)
		{
		}

		public FTPException(string msg, string replyCode)
			: base(msg)
		{
			try
			{
				this.replyCode = int.Parse(replyCode);
			}
			catch (FormatException)
			{
				this.replyCode = -1;
			}
		}

		public FTPException(FTPReply reply)
			: base(reply.ReplyText)
		{
			try
			{
				replyCode = int.Parse(reply.ReplyCode);
			}
			catch (FormatException)
			{
				replyCode = -1;
			}
		}
	}
}
