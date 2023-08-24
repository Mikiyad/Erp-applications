using Micromind.Common.Data;
using System;

namespace Micromind.Common.Interfaces
{
	public interface IPartnerStatementSystem
	{
		PartnerStatementData GetCustomerStatment(int partnerID, DateTime from, DateTime to, DateTime statementDate);

		PartnerStatementData GetCustomerStatment(int partnerID, DateTime from, DateTime to, DateTime statementDate, StatementDateFilter dateFilter);

		PartnerStatementData[] GetCustomersStatment(int[] partnersID, DateTime from, DateTime to, DateTime statementDate);

		PartnerStatementData[] GetCustomersStatment(int[] partnersID, DateTime from, DateTime to, DateTime statementDate, bool isZeroBalance, bool isInactiveCustomers, StatementDateFilter dateFilter);

		PartnerStatementData[] GetCustomersStatment(int[] partnersID, DateTime from, DateTime to, DateTime statementDate, bool isZeroBalance, bool isInactiveCustomers);
	}
}
