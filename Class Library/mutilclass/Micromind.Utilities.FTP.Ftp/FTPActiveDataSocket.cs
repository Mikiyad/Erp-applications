using System.IO;

namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPActiveDataSocket : FTPDataSocket
	{
		internal BaseSocket acceptedSock;

		internal override int Timeout
		{
			set
			{
				timeout = value;
				SetSocketTimeout(sock, value);
				if (acceptedSock != null)
				{
					SetSocketTimeout(acceptedSock, value);
				}
			}
		}

		internal override Stream DataStream
		{
			get
			{
				AcceptConnection();
				return acceptedSock.GetStream();
			}
		}

		internal FTPActiveDataSocket(BaseSocket sock)
		{
			base.sock = sock;
		}

		internal virtual void AcceptConnection()
		{
			if (acceptedSock == null)
			{
				acceptedSock = sock.Accept();
				SetSocketTimeout(acceptedSock, timeout);
			}
		}

		internal override void Close()
		{
			if (acceptedSock != null)
			{
				acceptedSock.Close();
				acceptedSock = null;
			}
			sock.Close();
		}
	}
}
