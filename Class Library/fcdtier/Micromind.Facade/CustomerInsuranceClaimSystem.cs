using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CustomerInsuranceClaimSystem : MarshalByRefObject, ICustomerInsuranceClaimSystem, IDisposable
	{
		private Config config;

		public CustomerInsuranceClaimSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomerInsuranceClaim(CustomerInsuranceClaimData data, bool isUpdate)
		{
			return new CustomerInsuranceClaim(config).InsertUpdateCustomerInsuranceClaim(data, isUpdate);
		}

		public CustomerInsuranceClaimData GetCustomerInsuranceClaimByID(string sysDocID, string voucherID)
		{
			return new CustomerInsuranceClaim(config).GetCustomerInsuranceClaimByID(sysDocID, voucherID);
		}

		public bool DeleteCustomerInsuranceClaim(string voucherID)
		{
			return new CustomerInsuranceClaim(config).DeleteCustomerInsuranceClaim(voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, string status)
		{
			return new CustomerInsuranceClaim(config).GetList(from, to, status);
		}

		public DataSet GetCustomerInsuranceClaimToPrint(string sysDocID, string voucherID)
		{
			return new CustomerInsuranceClaim(config).GetCustomerInsuranceClaimToPrint(sysDocID, voucherID);
		}
	}
}
