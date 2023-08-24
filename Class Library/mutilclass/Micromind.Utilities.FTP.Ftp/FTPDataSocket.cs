using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Micromind.Utilities.FTP.Ftp
{
	public abstract class FTPDataSocket
	{
		internal BaseSocket sock;

		internal int timeout;

		internal virtual int Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
				timeout = value;
				SetSocketTimeout(sock, value);
			}
		}

		internal int LocalPort => ((IPEndPoint)sock.LocalEndPoint).Port;

		internal abstract Stream DataStream
		{
			get;
		}

		internal void SetSocketTimeout(BaseSocket sock, int timeout)
		{
			if (timeout > 0)
			{
				sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
				sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
			}
		}

		internal abstract void Close();
	}
}
