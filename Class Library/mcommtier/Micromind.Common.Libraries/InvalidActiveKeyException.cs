using System;

namespace Micromind.Common.Libraries
{
	public class InvalidActiveKeyException : ApplicationException
	{
		public InvalidActiveKeyException()
			: base("Invalid active key.")
		{
		}

		public InvalidActiveKeyException(string msg)
			: base(msg)
		{
		}
	}
}
