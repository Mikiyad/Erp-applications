using Micromind.Common.Data;
using System;

namespace Micromind.ClientLibraries
{
	public class NewAccountEventArg : EventArgs
	{
		public string AccountName;

		public CompanyAccountTypes AccountType = CompanyAccountTypes.Bank;

		public NewAccountEventArg()
		{
		}

		public NewAccountEventArg(string accountName)
		{
			AccountName = accountName;
		}

		public NewAccountEventArg(string accountName, CompanyAccountTypes accountType)
		{
			AccountName = accountName;
			AccountType = accountType;
		}
	}
}
