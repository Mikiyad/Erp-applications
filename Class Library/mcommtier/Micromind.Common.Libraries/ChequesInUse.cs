using System;

namespace Micromind.Common.Libraries
{
	public class ChequesInUse : ApplicationException
	{
		public ChequesInUse()
			: base("Cheques already in use by another transactions or already deposited and cannot be deleted.")
		{
		}
	}
}
