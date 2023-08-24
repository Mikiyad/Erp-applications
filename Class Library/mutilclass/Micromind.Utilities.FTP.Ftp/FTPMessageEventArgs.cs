using System;

namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPMessageEventArgs : EventArgs
	{
		private string message;

		public string Message => message;

		public FTPMessageEventArgs(string message)
		{
			this.message = message;
		}
	}
}
