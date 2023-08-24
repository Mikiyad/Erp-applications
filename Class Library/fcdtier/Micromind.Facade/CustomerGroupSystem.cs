using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CustomerGroupSystem : MarshalByRefObject, ICustomerGroupSystem, IDisposable
	{
		private Config config;

		public CustomerGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomerGroup(CustomerGroupData data)
		{
			return new CustomerGroup(config).InsertCustomerGroup(data);
		}

		public bool UpdateCustomerGroup(CustomerGroupData data)
		{
			return UpdateCustomerGroup(data, checkConcurrency: false);
		}

		public bool UpdateCustomerGroup(CustomerGroupData data, bool checkConcurrency)
		{
			return new CustomerGroup(config).UpdateCustomerGroup(data);
		}

		public CustomerGroupData GetCustomerGroup()
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerGroup();
			}
		}

		public bool DeleteCustomerGroup(string groupID)
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.DeleteCustomerGroup(groupID);
			}
		}

		public CustomerGroupData GetCustomerGroupByID(string id)
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerGroupByID(id);
			}
		}

		public DataSet GetCustomerGroupByFields(params string[] columns)
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerGroupByFields(columns);
			}
		}

		public DataSet GetCustomerGroupByFields(string[] ids, params string[] columns)
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerGroupByFields(ids, columns);
			}
		}

		public DataSet GetCustomerGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCustomerGroupList()
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerGroupList();
			}
		}

		public DataSet GetCustomerAssignedGroupsList(string customerID)
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerAssignedGroupsList(customerID);
			}
		}

		public DataSet GetCustomerGroupComboList()
		{
			using (CustomerGroup customerGroup = new CustomerGroup(config))
			{
				return customerGroup.GetCustomerGroupComboList();
			}
		}
	}
}
