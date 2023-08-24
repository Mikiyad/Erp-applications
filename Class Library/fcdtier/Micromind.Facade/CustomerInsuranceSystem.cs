using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CustomerInsuranceSystem : MarshalByRefObject, ICustomerInsuranceSystem, IDisposable
	{
		private Config config;

		public CustomerInsuranceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomerInsurance(CustomerInsuranceData data, bool isUpdate)
		{
			return new CustomerInsurance(config).InsertUpdateCustomerInsurance(data, isUpdate);
		}

		public CustomerInsuranceData GetCustomerInsurance()
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsurance();
			}
		}

		public bool DeleteCustomerInsurance(string ID)
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.DeleteCustomerInsurance(ID);
			}
		}

		public CustomerInsuranceData GetCustomerInsuranceByID(string id)
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsuranceByID(id);
			}
		}

		public CustomerInsuranceData GetCustomerInsuranceIndividuals(string id)
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsuranceIndividuals(id);
			}
		}

		public CustomerInsuranceData GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new CustomerInsurance(config).GetList(from, to, showVoid);
		}

		public DataSet GetList(string customerID)
		{
			return new CustomerInsurance(config).GetList(customerID);
		}

		public DataSet GetCustomerInsuranceByFields(params string[] columns)
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsuranceByFields(columns);
			}
		}

		public DataSet GetCustomerInsuranceByFields(string[] ids, params string[] columns)
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsuranceByFields(ids, columns);
			}
		}

		public DataSet GetCustomerInsuranceByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsuranceByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCustomerInsuranceList()
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsuranceList();
			}
		}

		public DataSet GetCustomerInsuranceComboList()
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetCustomerInsuranceComboList();
			}
		}

		public string GetNextDocumentNumber(string sysDocID)
		{
			using (CustomerInsurance customerInsurance = new CustomerInsurance(config))
			{
				return customerInsurance.GetNextDocumentNumber(sysDocID);
			}
		}
	}
}
