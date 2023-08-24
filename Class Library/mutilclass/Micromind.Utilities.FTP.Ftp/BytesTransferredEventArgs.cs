using System;

namespace Micromind.Utilities.FTP.Ftp
{
	public class BytesTransferredEventArgs : EventArgs
	{
		private long byteCount;

		public long ByteCount => byteCount;

		public BytesTransferredEventArgs(long byteCount)
		{
			this.byteCount = byteCount;
		}
	}
}
