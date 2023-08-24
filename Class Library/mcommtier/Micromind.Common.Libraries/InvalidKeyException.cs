using System;

namespace Micromind.Common.Libraries
{
	public class InvalidKeyException : ApplicationException
	{
		public InvalidKeyException()
			: base("Invalid key.")
		{
		}

		public InvalidKeyException(string msg)
			: base(msg)
		{
		}
	}
}
