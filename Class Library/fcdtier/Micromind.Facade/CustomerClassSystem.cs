using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CustomerClassSystem : MarshalByRefObject, ICustomerClassSystem, IDisposable
	{
		private Config config;

		public CustomerClassSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomerClass(CustomerClassData data)
		{
			return new CustomerClass(config).InsertCustomerClass(data);
		}

		public bool UpdateCustomerClass(CustomerClassData data)
		{
			return UpdateCustomerClass(data, checkConcurrency: false);
		}

		public bool UpdateCustomerClass(CustomerClassData data, bool checkConcurrency)
		{
			return new CustomerClass(config).UpdateCustomerClass(data);
		}

		public CustomerClassData GetCustomerClass()
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerClass();
			}
		}

		public bool DeleteCustomerClass(string groupID)
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.DeleteCustomerClass(groupID);
			}
		}

		public CustomerClassData GetCustomerClassByID(string id)
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerClassByID(id);
			}
		}

		public DataSet GetCustomerClassByFields(params string[] columns)
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerClassByFields(columns);
			}
		}

		public DataSet GetCustomerClassByFields(string[] ids, params string[] columns)
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerClassByFields(ids, columns);
			}
		}

		public DataSet GetCustomerClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerClassByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCustomerClassList()
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerClassList();
			}
		}

		public DataSet GetCustomerClassComboList()
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerClassComboList();
			}
		}

		public DataSet GetTenantClassComboList()
		{
			using (CustomerClass customerClass = new CustomerClass(config))
			{
				return customerClass.GetCustomerTenantComboList();
			}
		}
	}
}
