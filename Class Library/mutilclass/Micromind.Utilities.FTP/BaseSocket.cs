using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Micromind.Utilities.FTP
{
	public abstract class BaseSocket
	{
		public abstract EndPoint LocalEndPoint
		{
			get;
		}

		public abstract BaseSocket Accept();

		public abstract void Bind(EndPoint localEP);

		public abstract void Close();

		public abstract void Connect(EndPoint remoteEP);

		public abstract void Listen(int backlog);

		public abstract Stream GetStream();

		public abstract int Receive(byte[] buffer);

		public abstract int Send(byte[] buffer);

		public abstract void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue);
	}
}
