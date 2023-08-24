using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPControlSocket
	{
		internal const string EOL = "\r\n";

		public const int CONTROL_PORT = 21;

		private const string DEBUG_ARROW = "---> ";

		private static readonly string PASSWORD_MESSAGE = "---> PASS";

		private bool strictReturnCodes = true;

		protected IPAddress remoteHost;

		protected int controlPort = -1;

		protected BaseSocket controlSock;

		protected int timeout;

		protected StreamWriter writer;

		protected StreamReader reader;

		internal virtual bool StrictReturnCodes
		{
			get
			{
				return strictReturnCodes;
			}
			set
			{
				strictReturnCodes = value;
			}
		}

		internal virtual int Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
				timeout = value;
				if (controlSock == null)
				{
					throw new SystemException("Failed to set timeout - no control socket");
				}
				SetSocketTimeout(controlSock, value);
			}
		}

		internal event FTPMessageHandler CommandSent;

		internal event FTPMessageHandler ReplyReceived;

		internal FTPControlSocket(IPAddress remoteHost, int controlPort, int timeout)
		{
			Initialize(new StandardSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), remoteHost, controlPort, timeout);
		}

		internal FTPControlSocket()
		{
		}

		internal void Initialize(BaseSocket sock, IPAddress remoteHost, int controlPort, int timeout)
		{
			this.remoteHost = remoteHost;
			this.controlPort = controlPort;
			this.timeout = timeout;
			controlSock = sock;
			ConnectSocket(controlSock, remoteHost, controlPort);
			Timeout = timeout;
			InitStreams();
			ValidateConnection();
		}

		internal virtual void ConnectSocket(BaseSocket socket, IPAddress address, int port)
		{
			socket.Connect(new IPEndPoint(address, port));
		}

		internal void ValidateConnection()
		{
			FTPReply reply = ReadReply();
			ValidateReply(reply, "220");
		}

		internal void InitStreams()
		{
			Stream stream = controlSock.GetStream();
			writer = new StreamWriter(stream, Encoding.GetEncoding("US-ASCII"));
			reader = new StreamReader(stream, Encoding.GetEncoding("US-ASCII"));
		}

		internal virtual void Logout()
		{
			SystemException ex = null;
			try
			{
				writer.Close();
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			try
			{
				reader.Close();
			}
			catch (SystemException ex3)
			{
				ex = ex3;
			}
			try
			{
				controlSock.Close();
			}
			catch (SystemException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		internal virtual FTPDataSocket CreateDataSocket(FTPConnectMode connectMode)
		{
			if (connectMode == FTPConnectMode.ACTIVE)
			{
				return CreateDataSocketActive();
			}
			return CreateDataSocketPASV();
		}

		internal virtual FTPDataSocket CreateDataSocketActive()
		{
			return NewActiveDataSocket(0);
		}

		internal void SetDataPort(IPEndPoint ep)
		{
			byte[] bytes = BitConverter.GetBytes(ep.Address.Address);
			byte[] array = ToByteArray((ushort)ep.Port);
			string command = new StringBuilder("PORT ").Append((short)bytes[0]).Append(",").Append((short)bytes[1])
				.Append(",")
				.Append((short)bytes[2])
				.Append(",")
				.Append((short)bytes[3])
				.Append(",")
				.Append((short)array[0])
				.Append(",")
				.Append((short)array[1])
				.ToString();
			FTPReply reply = SendCommand(command);
			ValidateReply(reply, "200");
		}

		internal byte[] ToByteArray(ushort val)
		{
			return new byte[2]
			{
				(byte)(val >> 8),
				(byte)(val & 0xFF)
			};
		}

		internal virtual FTPDataSocket CreateDataSocketPASV()
		{
			FTPReply fTPReply = SendCommand("PASV");
			ValidateReply(fTPReply, "227");
			string replyText = fTPReply.ReplyText;
			int num = replyText.IndexOf('(');
			int num2 = replyText.IndexOf(')');
			if (num < 0 && num2 < 0)
			{
				num = replyText.ToUpper().LastIndexOf("MODE") + 4;
				num2 = replyText.Length;
			}
			string text = replyText.Substring(num + 1, num2 - (num + 1));
			int[] array = new int[6];
			int length = text.Length;
			int num3 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				if (num3 > 6)
				{
					break;
				}
				char c = text[i];
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
				}
				else if (c != ',')
				{
					throw new FTPException("Malformed PASV reply: " + replyText);
				}
				if (c == ',' || i + 1 == length)
				{
					try
					{
						array[num3++] = int.Parse(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					catch (FormatException)
					{
						throw new FTPException("Malformed PASV reply: " + replyText);
					}
				}
			}
			string ipAddress = array[0] + "." + array[1] + "." + array[2] + "." + array[3];
			int port = (array[4] << 8) + array[5];
			return NewPassiveDataSocket(ipAddress, port);
		}

		internal virtual FTPDataSocket NewPassiveDataSocket(string ipAddress, int port)
		{
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(ipAddress), port);
			BaseSocket baseSocket = new StandardSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			SetSocketTimeout(baseSocket, timeout);
			baseSocket.Connect(remoteEP);
			return new FTPPassiveDataSocket(baseSocket);
		}

		internal virtual FTPDataSocket NewActiveDataSocket(int port)
		{
			BaseSocket baseSocket = new StandardSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint localEP = new IPEndPoint(((IPEndPoint)controlSock.LocalEndPoint).Address, 0);
			baseSocket.Bind(localEP);
			baseSocket.Listen(5);
			SetDataPort((IPEndPoint)baseSocket.LocalEndPoint);
			return new FTPActiveDataSocket(baseSocket);
		}

		public virtual FTPReply SendCommand(string command)
		{
			WriteCommand(command);
			return ReadReply();
		}

		internal virtual void WriteCommand(string command)
		{
			Log("---> " + command, command: true);
			writer.Write(command + "\r\n");
			writer.Flush();
		}

		internal virtual FTPReply ReadReply()
		{
			string text = reader.ReadLine();
			if (text == null || text.Length == 0)
			{
				throw new SystemException("Unexpected null reply received");
			}
			Log(text, command: false);
			string text2 = text.Substring(0, 3);
			StringBuilder stringBuilder = new StringBuilder("");
			if (text.Length > 3)
			{
				stringBuilder.Append(text.Substring(4));
			}
			ArrayList arrayList = null;
			if (text[3] == '-')
			{
				arrayList = ArrayList.Synchronized(new ArrayList(10));
				bool flag = false;
				while (!flag)
				{
					text = reader.ReadLine();
					if (text == null)
					{
						throw new SystemException("Unexpected null reply received");
					}
					Log(text, command: false);
					if (text.Length > 3 && text.Substring(0, 3).Equals(text2) && text[3] == ' ')
					{
						stringBuilder.Append(text.Substring(3));
						flag = true;
					}
					else
					{
						stringBuilder.Append(" ").Append(text);
						arrayList.Add(text);
					}
				}
			}
			if (arrayList != null)
			{
				string[] array = new string[arrayList.Count];
				arrayList.CopyTo(array);
				return new FTPReply(text2, stringBuilder.ToString(), array);
			}
			return new FTPReply(text2, stringBuilder.ToString());
		}

		internal virtual FTPReply ValidateReply(string reply, string expectedReplyCode)
		{
			FTPReply fTPReply = new FTPReply(reply);
			if (ValidateReplyCode(fTPReply, expectedReplyCode))
			{
				return fTPReply;
			}
			throw new FTPException(fTPReply);
		}

		public virtual FTPReply ValidateReply(string reply, string[] expectedReplyCodes)
		{
			FTPReply reply2 = new FTPReply(reply);
			return ValidateReply(reply2, expectedReplyCodes);
		}

		public virtual FTPReply ValidateReply(FTPReply reply, string[] expectedReplyCodes)
		{
			for (int i = 0; i < expectedReplyCodes.Length; i++)
			{
				if (ValidateReplyCode(reply, expectedReplyCodes[i]))
				{
					return reply;
				}
			}
			throw new FTPException(reply);
		}

		public virtual FTPReply ValidateReply(FTPReply reply, string expectedReplyCode)
		{
			if (ValidateReplyCode(reply, expectedReplyCode))
			{
				return reply;
			}
			throw new FTPException(reply);
		}

		private bool ValidateReplyCode(FTPReply reply, string expectedReplyCode)
		{
			string replyCode = reply.ReplyCode;
			if (strictReturnCodes)
			{
				if (replyCode.Equals(expectedReplyCode))
				{
					return true;
				}
				return false;
			}
			if (replyCode[0] == expectedReplyCode[0])
			{
				return true;
			}
			return false;
		}

		internal virtual void Log(string msg, bool command)
		{
			if (msg.StartsWith(PASSWORD_MESSAGE))
			{
				msg = PASSWORD_MESSAGE + " ********";
			}
			if (command)
			{
				if (this.CommandSent != null)
				{
					this.CommandSent(this, new FTPMessageEventArgs(msg));
				}
			}
			else if (this.ReplyReceived != null)
			{
				this.ReplyReceived(this, new FTPMessageEventArgs(msg));
			}
		}

		internal void SetSocketTimeout(BaseSocket sock, int timeout)
		{
			if (timeout > 0)
			{
				sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
				sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
			}
		}
	}
}
