using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CustomerCategorySystem : MarshalByRefObject, ICustomerCategorySystem, IDisposable
	{
		private Config config;

		public CustomerCategorySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomerCategory(CustomerCategoryData data)
		{
			return new CustomerCategory(config).InsertCustomerCategory(data);
		}

		public bool UpdateCustomerCategory(CustomerCategoryData data)
		{
			return UpdateCustomerCategory(data, checkConcurrency: false);
		}

		public bool UpdateCustomerCategory(CustomerCategoryData data, bool checkConcurrency)
		{
			return new CustomerCategory(config).UpdateCustomerCategory(data);
		}

		public CustomerCategoryData GetCustomerCategory()
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerCategory();
			}
		}

		public bool DeleteCustomerCategory(string categoryID)
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.DeleteCustomerCategory(categoryID);
			}
		}

		public CustomerCategoryData GetCustomerCategoryByID(string id)
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerCategoryByID(id);
			}
		}

		public DataSet GetCustomerCategoryByFields(params string[] columns)
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerCategoryByFields(columns);
			}
		}

		public DataSet GetCustomerCategoryByFields(string[] ids, params string[] columns)
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerCategoryByFields(ids, columns);
			}
		}

		public DataSet GetCustomerCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerCategoryByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCustomerCategoryList()
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerCategoryList();
			}
		}

		public DataSet GetCustomerAssignedCategorysList(string entityID)
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerAssignedCategorysList(entityID);
			}
		}

		public DataSet GetCustomerCategoryComboList()
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.GetCustomerCategoryComboList();
			}
		}

		public bool InsertCustomerCategoryAssignment(CustomerCategoryData data, string customerID)
		{
			using (CustomerCategory customerCategory = new CustomerCategory(config))
			{
				return customerCategory.InsertCustomerCategoryAssignment(data, customerID);
			}
		}
	}
}
