using Micromind.Common.Data;
using System;

namespace Micromind.ClientLibraries
{
	public class ChangeEventArg : EventArgs
	{
		public int ID = -1;

		public ChangeTypes ChangeType;

		public PartnerType PartnerType;

		public ChangeEventArg(int id)
		{
			ID = id;
		}

		public ChangeEventArg(int id, ChangeTypes type)
		{
			ID = id;
			ChangeType = type;
		}

		public ChangeEventArg(int id, PartnerType partnerType, ChangeTypes type)
		{
			ID = id;
			ChangeType = type;
			PartnerType = partnerType;
		}
	}
}
