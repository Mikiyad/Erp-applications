using System;

namespace LicenseManager
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
