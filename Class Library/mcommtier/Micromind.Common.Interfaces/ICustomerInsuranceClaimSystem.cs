using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerInsuranceClaimSystem
	{
		bool CreateCustomerInsuranceClaim(CustomerInsuranceClaimData customerInsuranceClaimData, bool isUpdate);

		CustomerInsuranceClaimData GetCustomerInsuranceClaimByID(string sysDocID, string voucherID);

		bool DeleteCustomerInsuranceClaim(string voucherID);

		DataSet GetList(DateTime from, DateTime to, string status);

		DataSet GetCustomerInsuranceClaimToPrint(string sysDocID, string VoucherID);
	}
}
