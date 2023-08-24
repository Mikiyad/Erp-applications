using System;

namespace LicenseManager
{
	public class InvalidFileException : ApplicationException
	{
		public InvalidFileException()
			: base("Invalid file format.")
		{
		}

		public InvalidFileException(string msg)
			: base(msg)
		{
		}
	}
}
