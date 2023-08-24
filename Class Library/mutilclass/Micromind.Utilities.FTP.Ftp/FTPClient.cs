using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;

namespace Micromind.Utilities.FTP.Ftp
{
	public class FTPClient
	{
		private const int DEFAULT_MONITOR_INTERVAL = 4096;

		private const int DEFAULT_BUFFER_SIZE = 4096;

		private static string majorVersion;

		private static string middleVersion;

		private static string minorVersion;

		private static int[] version;

		private static string buildTimestamp;

		private static string BINARY_CHAR;

		private static string ASCII_CHAR;

		private static readonly string tsFormat;

		internal FTPControlSocket control;

		internal FTPDataSocket data;

		internal int timeout;

		private bool strictReturnCodes = true;

		private bool cancelTransfer;

		private bool resume;

		private bool deleteOnFailure = true;

		private long resumeMarker;

		private long monitorInterval = 4096L;

		private int transferBufferSize = 4096;

		private FTPFileFactory fileFactory;

		private FTPTransferType transferType = FTPTransferType.ASCII;

		private FTPConnectMode connectMode = FTPConnectMode.PASV;

		internal FTPReply lastValidReply;

		internal int controlPort = -1;

		internal string remoteHost;

		public static int[] Version => version;

		public static string BuildTimestamp => buildTimestamp;

		public virtual bool StrictReturnCodes
		{
			get
			{
				return strictReturnCodes;
			}
			set
			{
				strictReturnCodes = value;
				if (control != null)
				{
					control.StrictReturnCodes = value;
				}
			}
		}

		public virtual int Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
				timeout = value;
				if (control != null)
				{
					control.Timeout = value;
				}
			}
		}

		public virtual FTPConnectMode ConnectMode
		{
			get
			{
				return connectMode;
			}
			set
			{
				connectMode = value;
			}
		}

		public long TransferNotifyInterval
		{
			get
			{
				return monitorInterval;
			}
			set
			{
				monitorInterval = value;
			}
		}

		public int TransferBufferSize
		{
			get
			{
				return transferBufferSize;
			}
			set
			{
				transferBufferSize = value;
			}
		}

		public virtual string RemoteHost
		{
			get
			{
				return remoteHost;
			}
			set
			{
				CheckConnection(shouldBeConnected: false);
				remoteHost = value;
			}
		}

		public bool DeleteOnFailure
		{
			get
			{
				return deleteOnFailure;
			}
			set
			{
				deleteOnFailure = value;
			}
		}

		public int ControlPort
		{
			get
			{
				return controlPort;
			}
			set
			{
				CheckConnection(shouldBeConnected: false);
				controlPort = value;
			}
		}

		public FTPFileFactory FTPFileFactory
		{
			set
			{
				fileFactory = value;
			}
		}

		public FTPReply LastValidReply => lastValidReply;

		public FTPTransferType TransferType
		{
			get
			{
				return transferType;
			}
			set
			{
				CheckConnection(shouldBeConnected: true);
				string str = ASCII_CHAR;
				if (value.Equals(FTPTransferType.BINARY))
				{
					str = BINARY_CHAR;
				}
				FTPReply reply = control.SendCommand("TYPE " + str);
				lastValidReply = control.ValidateReply(reply, "200");
				transferType = value;
			}
		}

		public event EventHandler TransferStarted;

		public event EventHandler TransferComplete;

		public event BytesTransferredHandler BytesTransferred;

		public event FTPMessageHandler CommandSent;

		public event FTPMessageHandler ReplyReceived;

		public FTPClient(string remoteHost)
			: this(remoteHost, 21, 0)
		{
		}

		public FTPClient(string remoteHost, int controlPort)
			: this(remoteHost, controlPort, 0)
		{
		}

		public FTPClient(string remoteHost, int controlPort, int timeout)
			: this(Dns.Resolve(remoteHost).AddressList[0], controlPort, timeout)
		{
			this.remoteHost = remoteHost;
		}

		public FTPClient(IPAddress remoteAddr)
			: this(remoteAddr, 21, 0)
		{
		}

		public FTPClient(IPAddress remoteAddr, int controlPort)
			: this(remoteAddr, controlPort, 0)
		{
		}

		public FTPClient(IPAddress remoteAddr, int controlPort, int timeout)
		{
			InitBlock();
			remoteHost = remoteAddr.ToString();
			Connect(remoteAddr, controlPort, timeout);
		}

		public FTPClient()
		{
			InitBlock();
		}

		private void InitBlock()
		{
			transferType = FTPTransferType.ASCII;
			connectMode = FTPConnectMode.PASV;
			controlPort = 21;
		}

		public virtual void Connect()
		{
			CheckConnection(shouldBeConnected: false);
			Connect(Dns.Resolve(remoteHost).AddressList[0], controlPort, timeout);
		}

		internal virtual void Connect(IPAddress remoteAddr, int controlPort, int timeout)
		{
			if (controlPort < 0)
			{
				controlPort = 21;
			}
			this.controlPort = controlPort;
			Initialize(new FTPControlSocket(remoteAddr, controlPort, timeout));
		}

		internal void Initialize(FTPControlSocket control)
		{
			this.control = control;
			control.CommandSent += CommandSentControl;
			control.ReplyReceived += ReplyReceivedControl;
		}

		internal virtual void CheckConnection(bool shouldBeConnected)
		{
			if (shouldBeConnected && control == null)
			{
				throw new FTPException("The FTP client has not yet connected to the server.  The requested action cannot be performed until after a connection has been established.");
			}
			if (!shouldBeConnected && control != null)
			{
				throw new FTPException("The FTP client has already been connected to the server.  The requested action must be performed before a connection is established.");
			}
		}

		internal void CommandSentControl(object client, FTPMessageEventArgs message)
		{
			if (this.CommandSent != null)
			{
				this.CommandSent(this, message);
			}
		}

		internal void ReplyReceivedControl(object client, FTPMessageEventArgs message)
		{
			if (this.ReplyReceived != null)
			{
				this.ReplyReceived(this, message);
			}
		}

		public void DebugResponses(bool on)
		{
		}

		public virtual void CancelTransfer()
		{
			cancelTransfer = true;
		}

		public virtual void Login(string user, string password)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("USER " + user);
			string[] expectedReplyCodes = new string[2]
			{
				"230",
				"331"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
			if (!lastValidReply.ReplyCode.Equals("230"))
			{
				Password(password);
			}
		}

		public virtual void User(string user)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("USER " + user);
			string[] expectedReplyCodes = new string[2]
			{
				"230",
				"331"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
		}

		public virtual void Password(string password)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("PASS " + password);
			string[] expectedReplyCodes = new string[2]
			{
				"230",
				"202"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
		}

		public virtual string Quote(string command, string[] validCodes)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand(command);
			if (validCodes != null && validCodes.Length != 0)
			{
				lastValidReply = control.ValidateReply(reply, validCodes);
				return lastValidReply.ReplyText;
			}
			throw new FTPException("Valid reply code must be supplied");
		}

		public virtual long Size(string remoteFile)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("SIZE " + remoteFile);
			lastValidReply = control.ValidateReply(reply, "213");
			string text = lastValidReply.ReplyText;
			int num = text.IndexOf(' ');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			try
			{
				return long.Parse(text);
			}
			catch (FormatException)
			{
				throw new FTPException("Failed to parse reply: " + text);
			}
		}

		public virtual void Resume()
		{
			if (transferType.Equals(FTPTransferType.ASCII))
			{
				throw new FTPException("Resume only supported for BINARY transfers");
			}
			resume = true;
		}

		public virtual void CancelResume()
		{
			Restart(0L);
			resume = false;
		}

		private void Restart(long size)
		{
			FTPReply reply = control.SendCommand("REST " + size);
			lastValidReply = control.ValidateReply(reply, "350");
		}

		public virtual void Put(string localPath, string remoteFile)
		{
			Put(localPath, remoteFile, append: false);
		}

		public virtual void Put(Stream srcStream, string remoteFile)
		{
			Put(srcStream, remoteFile, append: false);
		}

		public virtual void Put(string localPath, string remoteFile, bool append)
		{
			if (transferType == FTPTransferType.ASCII)
			{
				PutASCII(localPath, remoteFile, append);
			}
			else
			{
				PutBinary(localPath, remoteFile, append);
			}
			ValidateTransfer();
		}

		public virtual void Put(Stream srcStream, string remoteFile, bool append)
		{
			if (transferType == FTPTransferType.ASCII)
			{
				PutASCII(srcStream, remoteFile, append);
			}
			else
			{
				PutBinary(srcStream, remoteFile, append);
			}
			ValidateTransfer();
		}

		public virtual void ValidateTransfer()
		{
			CheckConnection(shouldBeConnected: true);
			string[] expectedReplyCodes = new string[5]
			{
				"225",
				"226",
				"250",
				"426",
				"450"
			};
			FTPReply fTPReply = control.ReadReply();
			string replyCode = fTPReply.ReplyCode;
			if ((replyCode.Equals("426") || replyCode.Equals("450")) && !cancelTransfer)
			{
				throw new FTPException(fTPReply);
			}
			lastValidReply = control.ValidateReply(fTPReply, expectedReplyCodes);
		}

		private void CloseDataSocket(Stream stream)
		{
			if (stream != null)
			{
				try
				{
					stream.Close();
				}
				catch (SystemException)
				{
				}
			}
			CloseDataSocket();
		}

		private void CloseDataSocket()
		{
			if (data != null)
			{
				try
				{
					data.Close();
					data = null;
				}
				catch (SystemException)
				{
				}
			}
		}

		private void InitPut(string remoteFile, bool append)
		{
			CheckConnection(shouldBeConnected: true);
			cancelTransfer = false;
			bool flag = false;
			data = null;
			try
			{
				data = control.CreateDataSocket(connectMode);
				data.Timeout = timeout;
				if (resume)
				{
					if (transferType.Equals(FTPTransferType.ASCII))
					{
						throw new FTPException("Resume only supported for BINARY transfers");
					}
					resumeMarker = Size(remoteFile);
					Restart(resumeMarker);
				}
				string str = append ? "APPE " : "STOR ";
				FTPReply reply = control.SendCommand(str + remoteFile);
				string[] expectedReplyCodes = new string[2]
				{
					"125",
					"150"
				};
				lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
			}
			catch (SystemException ex)
			{
				flag = true;
				throw ex;
			}
			catch (FTPException ex2)
			{
				flag = true;
				throw ex2;
			}
			finally
			{
				if (flag)
				{
					resume = false;
					CloseDataSocket();
				}
			}
		}

		private void PutASCII(string localPath, string remoteFile, bool append)
		{
			Stream srcStream = new FileStream(localPath, FileMode.Open, FileAccess.Read);
			PutASCII(srcStream, remoteFile, append);
		}

		private void PutASCII(Stream srcStream, string remoteFile, bool append)
		{
			StreamReader streamReader = null;
			StreamWriter streamWriter = null;
			SystemException ex = null;
			long num = 0L;
			try
			{
				streamReader = new StreamReader(srcStream);
				InitPut(remoteFile, append);
				streamWriter = new StreamWriter(data.DataStream);
				if (this.TransferStarted != null)
				{
					this.TransferStarted(this, new EventArgs());
				}
				long num2 = 0L;
				string text = null;
				while ((text = streamReader.ReadLine()) != null && !cancelTransfer)
				{
					num += text.Length;
					num2 += text.Length;
					streamWriter.Write(text);
					streamWriter.Write("\r\n");
					if (this.BytesTransferred != null && num2 > monitorInterval)
					{
						this.BytesTransferred(this, new BytesTransferredEventArgs(num));
						num2 = 0L;
					}
				}
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			finally
			{
				try
				{
					streamReader?.Close();
				}
				catch (SystemException)
				{
				}
				try
				{
					streamWriter?.Close();
				}
				catch (SystemException)
				{
				}
				if (ex != null)
				{
					throw ex;
				}
				if (this.BytesTransferred != null)
				{
					this.BytesTransferred(this, new BytesTransferredEventArgs(num));
				}
				if (this.TransferComplete != null)
				{
					this.TransferComplete(this, new EventArgs());
				}
			}
		}

		private void PutBinary(string localPath, string remoteFile, bool append)
		{
			Stream srcStream = new FileStream(localPath, FileMode.Open, FileAccess.Read);
			PutBinary(srcStream, remoteFile, append);
		}

		private void PutBinary(Stream srcStream, string remoteFile, bool append)
		{
			BufferedStream bufferedStream = null;
			BinaryWriter binaryWriter = null;
			SystemException ex = null;
			long num = 0L;
			try
			{
				bufferedStream = new BufferedStream(srcStream);
				InitPut(remoteFile, append);
				binaryWriter = new BinaryWriter(data.DataStream);
				if (resume)
				{
					bufferedStream.Seek(resumeMarker, SeekOrigin.Current);
				}
				byte[] array = new byte[transferBufferSize];
				if (this.TransferStarted != null)
				{
					this.TransferStarted(this, new EventArgs());
				}
				long num2 = 0L;
				int num3 = 0;
				while ((num3 = bufferedStream.Read(array, 0, array.Length)) > 0 && !cancelTransfer)
				{
					binaryWriter.Write(array, 0, num3);
					num += num3;
					num2 += num3;
					if (this.BytesTransferred != null && num2 > monitorInterval)
					{
						this.BytesTransferred(this, new BytesTransferredEventArgs(num));
						num2 = 0L;
					}
				}
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			finally
			{
				resume = false;
				try
				{
					bufferedStream?.Close();
				}
				catch (SystemException)
				{
				}
				try
				{
					binaryWriter?.Close();
				}
				catch (SystemException)
				{
				}
				if (ex != null)
				{
					throw ex;
				}
				if (this.BytesTransferred != null)
				{
					this.BytesTransferred(this, new BytesTransferredEventArgs(num));
				}
				if (this.TransferComplete != null)
				{
					this.TransferComplete(this, new EventArgs());
				}
			}
		}

		public virtual void Put(byte[] bytes, string remoteFile)
		{
			Put(bytes, remoteFile, append: false);
		}

		public virtual void Put(byte[] bytes, string remoteFile, bool append)
		{
			InitPut(remoteFile, append);
			BinaryWriter binaryWriter = new BinaryWriter(data.DataStream);
			try
			{
				binaryWriter.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				try
				{
					binaryWriter.Close();
				}
				catch (SystemException)
				{
				}
			}
			ValidateTransfer();
		}

		public virtual void Get(string localPath, string remoteFile)
		{
			if (transferType == FTPTransferType.ASCII)
			{
				GetASCII(localPath, remoteFile);
			}
			else
			{
				GetBinary(localPath, remoteFile);
			}
			ValidateTransfer();
		}

		public virtual void Get(Stream destStream, string remoteFile)
		{
			if (transferType == FTPTransferType.ASCII)
			{
				GetASCII(destStream, remoteFile);
			}
			else
			{
				GetBinary(destStream, remoteFile);
			}
			ValidateTransfer();
		}

		private void InitGet(string remoteFile)
		{
			CheckConnection(shouldBeConnected: true);
			cancelTransfer = false;
			bool flag = false;
			data = null;
			try
			{
				data = control.CreateDataSocket(connectMode);
				data.Timeout = timeout;
				if (resume)
				{
					if (transferType.Equals(FTPTransferType.ASCII))
					{
						throw new FTPException("Resume only supported for BINARY transfers");
					}
					Restart(resumeMarker);
				}
				FTPReply reply = control.SendCommand("RETR " + remoteFile);
				string[] expectedReplyCodes = new string[2]
				{
					"125",
					"150"
				};
				lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
			}
			catch (SystemException ex)
			{
				flag = true;
				throw ex;
			}
			catch (FTPException ex2)
			{
				flag = true;
				throw ex2;
			}
			finally
			{
				if (flag)
				{
					resume = false;
					CloseDataSocket();
				}
			}
		}

		private void GetASCII(string localPath, string remoteFile)
		{
			InitGet(remoteFile);
			SystemException ex = null;
			long num = 0L;
			FileInfo fileInfo = new FileInfo(localPath);
			StreamWriter streamWriter = new StreamWriter(localPath);
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(data.DataStream);
				data.Timeout = timeout;
				if (this.TransferStarted != null)
				{
					this.TransferStarted(this, new EventArgs());
				}
				long num2 = 0L;
				string text = null;
				while ((text = ReadLine(streamReader)) != null && !cancelTransfer)
				{
					num += text.Length;
					num2 += text.Length;
					streamWriter.WriteLine(text);
					if (this.BytesTransferred != null && num2 > monitorInterval)
					{
						this.BytesTransferred(this, new BytesTransferredEventArgs(num));
						num2 = 0L;
					}
				}
				if (cancelTransfer)
				{
					Abort();
				}
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			try
			{
				streamWriter.Close();
			}
			catch (SystemException)
			{
			}
			try
			{
				streamReader?.Close();
			}
			catch (SystemException)
			{
			}
			if (ex != null)
			{
				if (deleteOnFailure)
				{
					fileInfo.Delete();
				}
				throw ex;
			}
			if (this.BytesTransferred != null)
			{
				this.BytesTransferred(this, new BytesTransferredEventArgs(num));
			}
			if (this.TransferComplete != null)
			{
				this.TransferComplete(this, new EventArgs());
			}
		}

		private void GetASCII(Stream destStream, string remoteFile)
		{
			InitGet(remoteFile);
			StreamWriter streamWriter = new StreamWriter(destStream);
			StreamReader streamReader = null;
			SystemException ex = null;
			long num = 0L;
			try
			{
				streamReader = new StreamReader(data.DataStream);
				data.Timeout = timeout;
				if (this.TransferStarted != null)
				{
					this.TransferStarted(this, new EventArgs());
				}
				long num2 = 0L;
				string text = null;
				while ((text = ReadLine(streamReader)) != null && !cancelTransfer)
				{
					num += text.Length;
					num2 += text.Length;
					streamWriter.WriteLine(text);
					if (this.BytesTransferred != null && num2 > monitorInterval)
					{
						this.BytesTransferred(this, new BytesTransferredEventArgs(num));
						num2 = 0L;
					}
				}
				if (cancelTransfer)
				{
					Abort();
				}
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			try
			{
				streamWriter.Close();
			}
			catch (SystemException)
			{
			}
			try
			{
				streamReader?.Close();
			}
			catch (SystemException)
			{
			}
			if (ex != null)
			{
				throw ex;
			}
			if (this.BytesTransferred != null)
			{
				this.BytesTransferred(this, new BytesTransferredEventArgs(num));
			}
			if (this.TransferComplete != null)
			{
				this.TransferComplete(this, new EventArgs());
			}
		}

		private void GetBinary(string localPath, string remoteFile)
		{
			FileInfo fileInfo = new FileInfo(localPath);
			if (fileInfo.Exists && resume)
			{
				resumeMarker = fileInfo.Length;
			}
			InitGet(remoteFile);
			FileMode mode = resume ? FileMode.Append : FileMode.Create;
			BinaryWriter binaryWriter = new BinaryWriter(new FileStream(localPath, mode));
			BinaryReader binaryReader = null;
			long num = 0L;
			SystemException ex = null;
			try
			{
				binaryReader = new BinaryReader(data.DataStream);
				data.Timeout = timeout;
				if (this.TransferStarted != null)
				{
					this.TransferStarted(this, new EventArgs());
				}
				long num2 = 0L;
				byte[] array = new byte[transferBufferSize];
				int num3;
				while ((num3 = ReadChunk(binaryReader, array, transferBufferSize)) > 0 && !cancelTransfer)
				{
					binaryWriter.Write(array, 0, num3);
					num += num3;
					num2 += num3;
					if (this.BytesTransferred != null && num2 > monitorInterval)
					{
						this.BytesTransferred(this, new BytesTransferredEventArgs(num));
						num2 = 0L;
					}
				}
				if (cancelTransfer)
				{
					Abort();
				}
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			resume = false;
			try
			{
				binaryWriter.Close();
			}
			catch (SystemException)
			{
			}
			try
			{
				binaryReader?.Close();
			}
			catch (SystemException)
			{
			}
			if (ex != null)
			{
				if (deleteOnFailure)
				{
					fileInfo.Delete();
				}
				throw ex;
			}
			if (this.BytesTransferred != null)
			{
				this.BytesTransferred(this, new BytesTransferredEventArgs(num));
			}
			if (this.TransferComplete != null)
			{
				this.TransferComplete(this, new EventArgs());
			}
		}

		private void GetBinary(Stream destStream, string remoteFile)
		{
			InitGet(remoteFile);
			BufferedStream bufferedStream = new BufferedStream(destStream);
			BinaryReader binaryReader = null;
			long num = 0L;
			SystemException ex = null;
			try
			{
				binaryReader = new BinaryReader(data.DataStream);
				data.Timeout = timeout;
				if (this.TransferStarted != null)
				{
					this.TransferStarted(this, new EventArgs());
				}
				long num2 = 0L;
				byte[] array = new byte[transferBufferSize];
				int num3;
				while ((num3 = ReadChunk(binaryReader, array, transferBufferSize)) > 0 && !cancelTransfer)
				{
					bufferedStream.Write(array, 0, num3);
					num += num3;
					num2 += num3;
					if (this.BytesTransferred != null && num2 > monitorInterval)
					{
						this.BytesTransferred(this, new BytesTransferredEventArgs(num));
						num2 = 0L;
					}
				}
				if (cancelTransfer)
				{
					Abort();
				}
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			try
			{
				bufferedStream.Close();
			}
			catch (SystemException)
			{
			}
			try
			{
				binaryReader?.Close();
			}
			catch (SystemException)
			{
			}
			if (ex != null)
			{
				throw ex;
			}
			if (this.BytesTransferred != null)
			{
				this.BytesTransferred(this, new BytesTransferredEventArgs(num));
			}
			if (this.TransferComplete != null)
			{
				this.TransferComplete(this, new EventArgs());
			}
		}

		public virtual byte[] Get(string remoteFile)
		{
			InitGet(remoteFile);
			BinaryReader binaryReader = new BinaryReader(data.DataStream);
			long num = 0L;
			SystemException ex = null;
			MemoryStream memoryStream = null;
			try
			{
				data.Timeout = timeout;
				long num2 = 0L;
				byte[] array = new byte[transferBufferSize];
				memoryStream = new MemoryStream(transferBufferSize);
				if (this.TransferStarted != null)
				{
					this.TransferStarted(this, new EventArgs());
				}
				int num3;
				while ((num3 = ReadChunk(binaryReader, array, transferBufferSize)) > 0 && !cancelTransfer)
				{
					memoryStream.Write(array, 0, num3);
					num += num3;
					num2 += num3;
					if (this.BytesTransferred != null && num2 > monitorInterval)
					{
						this.BytesTransferred(this, new BytesTransferredEventArgs(num));
						num2 = 0L;
					}
				}
				if (cancelTransfer)
				{
					Abort();
				}
			}
			catch (SystemException ex2)
			{
				ex = ex2;
			}
			try
			{
				memoryStream?.Close();
			}
			catch (SystemException)
			{
			}
			try
			{
				binaryReader.Close();
			}
			catch (SystemException)
			{
			}
			if (ex != null)
			{
				throw ex;
			}
			if (this.BytesTransferred != null)
			{
				this.BytesTransferred(this, new BytesTransferredEventArgs(num));
			}
			if (this.TransferComplete != null)
			{
				this.TransferComplete(this, new EventArgs());
			}
			ValidateTransfer();
			return memoryStream.ToArray();
		}

		public virtual bool Site(string command)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply fTPReply = control.SendCommand("SITE " + command);
			string[] expectedReplyCodes = new string[3]
			{
				"200",
				"202",
				"502"
			};
			lastValidReply = control.ValidateReply(fTPReply, expectedReplyCodes);
			if (fTPReply.ReplyCode.Equals("200"))
			{
				return true;
			}
			return false;
		}

		public virtual FTPFile[] DirDetails(string dirname)
		{
			if (fileFactory == null)
			{
				fileFactory = new FTPFileFactory(GetSystem());
			}
			return fileFactory.Parse(Dir(dirname, full: true));
		}

		public virtual string[] Dir()
		{
			return Dir(null, full: false);
		}

		public virtual string[] Dir(string dirname)
		{
			return Dir(dirname, full: false);
		}

		public virtual string[] Dir(string dirname, bool full)
		{
			CheckConnection(shouldBeConnected: true);
			data = control.CreateDataSocket(connectMode);
			data.Timeout = timeout;
			string text = full ? "LIST " : "NLST ";
			if (dirname != null)
			{
				text += dirname;
			}
			text = text.Trim();
			FTPReply reply = control.SendCommand(text);
			string[] expectedReplyCodes = new string[4]
			{
				"125",
				"150",
				"450",
				"550"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
			string[] array = new string[0];
			string replyCode = lastValidReply.ReplyCode;
			if (!replyCode.Equals("450") && !replyCode.Equals("550"))
			{
				StreamReader streamReader = new StreamReader(data.DataStream);
				ArrayList arrayList = new ArrayList(10);
				string text2 = null;
				while ((text2 = ReadLine(streamReader)) != null)
				{
					arrayList.Add(text2);
				}
				try
				{
					streamReader.Close();
				}
				catch (SystemException)
				{
				}
				CloseDataSocket();
				string[] expectedReplyCodes2 = new string[2]
				{
					"226",
					"250"
				};
				reply = control.ReadReply();
				lastValidReply = control.ValidateReply(reply, expectedReplyCodes2);
				if (arrayList.Count != 0)
				{
					array = new string[arrayList.Count];
					arrayList.CopyTo(array);
				}
			}
			else
			{
				CloseDataSocket();
			}
			return array;
		}

		internal virtual int ReadChunk(BinaryReader input, byte[] chunk, int chunksize)
		{
			return input.Read(chunk, 0, chunksize);
		}

		internal virtual int ReadChar(StreamReader input)
		{
			return input.Read();
		}

		internal virtual string ReadLine(StreamReader input)
		{
			return input.ReadLine();
		}

		public virtual void Delete(string remoteFile)
		{
			CheckConnection(shouldBeConnected: true);
			string[] expectedReplyCodes = new string[2]
			{
				"200",
				"250"
			};
			FTPReply reply = control.SendCommand("DELE " + remoteFile);
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
		}

		public virtual void Rename(string from, string to)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("RNFR " + from);
			lastValidReply = control.ValidateReply(reply, "350");
			reply = control.SendCommand("RNTO " + to);
			lastValidReply = control.ValidateReply(reply, "250");
		}

		public virtual void RmDir(string dir)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("RMD " + dir);
			string[] expectedReplyCodes = new string[3]
			{
				"200",
				"250",
				"257"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
		}

		public virtual void MkDir(string dir)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("MKD " + dir);
			string[] expectedReplyCodes = new string[3]
			{
				"200",
				"250",
				"257"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
		}

		public virtual void ChDir(string dir)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("CWD " + dir);
			lastValidReply = control.ValidateReply(reply, "250");
		}

		public virtual DateTime ModTime(string remoteFile)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("MDTM " + remoteFile);
			lastValidReply = control.ValidateReply(reply, "213");
			return DateTime.ParseExact(lastValidReply.ReplyText, tsFormat, CultureInfo.CurrentCulture.DateTimeFormat).ToUniversalTime();
		}

		public virtual string Pwd()
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("PWD");
			lastValidReply = control.ValidateReply(reply, "257");
			string replyText = lastValidReply.ReplyText;
			int num = replyText.IndexOf('"');
			int num2 = replyText.LastIndexOf('"');
			if (num >= 0 && num2 > num)
			{
				return replyText.Substring(num + 1, num2 - (num + 1));
			}
			return replyText;
		}

		public virtual string[] Features()
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("FEAT");
			string[] expectedReplyCodes = new string[3]
			{
				"211",
				"500",
				"502"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
			if (lastValidReply.ReplyCode.Equals("211"))
			{
				return lastValidReply.ReplyData;
			}
			throw new FTPException(reply);
		}

		public virtual string GetSystem()
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("SYST");
			lastValidReply = control.ValidateReply(reply, "215");
			return lastValidReply.ReplyText;
		}

		public virtual string Help(string command)
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("HELP " + command);
			string[] expectedReplyCodes = new string[2]
			{
				"211",
				"214"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
			return lastValidReply.ReplyText;
		}

		protected virtual void Abort()
		{
			CheckConnection(shouldBeConnected: true);
			FTPReply reply = control.SendCommand("ABOR");
			string[] expectedReplyCodes = new string[2]
			{
				"426",
				"226"
			};
			lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
		}

		public virtual void Quit()
		{
			CheckConnection(shouldBeConnected: true);
			fileFactory = null;
			try
			{
				FTPReply reply = control.SendCommand("QUIT");
				string[] expectedReplyCodes = new string[2]
				{
					"221",
					"226"
				};
				lastValidReply = control.ValidateReply(reply, expectedReplyCodes);
			}
			finally
			{
				control.Logout();
				control = null;
			}
		}

		public virtual void QuitImmediately()
		{
			CheckConnection(shouldBeConnected: true);
			fileFactory = null;
			control.Logout();
			control = null;
		}

		static FTPClient()
		{
			majorVersion = "1";
			middleVersion = "0";
			minorVersion = "0";
			buildTimestamp = "1/1/2000";
			BINARY_CHAR = "I";
			ASCII_CHAR = "A";
			tsFormat = "yyyyMMddHHmmss";
			try
			{
				version = new int[3];
				version[0] = int.Parse(majorVersion);
				version[1] = int.Parse(middleVersion);
				version[2] = int.Parse(minorVersion);
			}
			catch (FormatException ex)
			{
				Console.Error.WriteLine("Failed to calculate version: " + ex.Message);
			}
		}
	}
}
