using System;

namespace Micromind.Common.Libraries
{
	public class DBNotConnectedException : ApplicationException
	{
		public DBNotConnectedException()
			: base("You are not connected.")
		{
		}

		public DBNotConnectedException(string msg)
			: base(msg)
		{
		}
	}
}
