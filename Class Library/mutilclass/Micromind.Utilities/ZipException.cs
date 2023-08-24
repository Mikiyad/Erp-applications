using System;

namespace Micromind.Utilities
{
	public class ZipException : ApplicationException
	{
		public ZipException()
		{
		}

		public ZipException(string msg)
			: base(msg)
		{
		}
	}
}
