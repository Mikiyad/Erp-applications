using System;

namespace Micromind.Securities
{
	public class InvalidKeyException : ApplicationException
	{
		public InvalidKeyException()
			: base("Invalid key format.")
		{
		}

		public InvalidKeyException(string msg)
			: base(msg)
		{
		}
	}
}
