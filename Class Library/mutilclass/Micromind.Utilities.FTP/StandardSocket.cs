using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Micromind.Utilities.FTP
{
	public class StandardSocket : BaseSocket
	{
		private Socket socket;

		public override EndPoint LocalEndPoint => socket.LocalEndPoint;

		public StandardSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			socket = new Socket(addressFamily, socketType, protocolType);
		}

		protected StandardSocket(Socket socket)
		{
			this.socket = socket;
		}

		public override BaseSocket Accept()
		{
			return new StandardSocket(socket.Accept());
		}

		public override void Bind(EndPoint localEP)
		{
			socket.Bind(localEP);
		}

		public override void Close()
		{
			socket.Close();
		}

		public override void Connect(EndPoint remoteEP)
		{
			socket.Connect(remoteEP);
		}

		public override void Listen(int backlog)
		{
			socket.Listen(backlog);
		}

		public override Stream GetStream()
		{
			return new NetworkStream(socket, ownsSocket: true);
		}

		public override int Receive(byte[] buffer)
		{
			return socket.Receive(buffer);
		}

		public override int Send(byte[] buffer)
		{
			return socket.Send(buffer);
		}

		public override void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
		{
			socket.SetSocketOption(optionLevel, optionName, optionValue);
		}
	}
}
