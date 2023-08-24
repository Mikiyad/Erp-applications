using System;

namespace Micromind.Common.Libraries
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
