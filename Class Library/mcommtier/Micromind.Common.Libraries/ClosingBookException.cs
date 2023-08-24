using System;

namespace Micromind.Common.Libraries
{
	public class ClosingBookException : ApplicationException
	{
		public ClosingBookException()
			: base("This transaction date is less than the closing date. Please provide a correct password.")
		{
		}

		public ClosingBookException(string msg)
			: base(msg)
		{
		}
	}
}
