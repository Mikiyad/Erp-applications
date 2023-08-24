using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CustomerAddressSystem : MarshalByRefObject, ICustomerAddressSystem, IDisposable
	{
		private Config config;

		public CustomerAddressSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomerAddress(CustomerAddressData data)
		{
			return new CustomerAddresses(config).InsertCustomerAddress(data);
		}

		public bool UpdateCustomerAddress(CustomerAddressData data)
		{
			return UpdateCustomerAddress(data, checkConcurrency: false);
		}

		public bool UpdateCustomerAddress(CustomerAddressData data, bool checkConcurrency)
		{
			return new CustomerAddresses(config).UpdateCustomerAddress(data);
		}

		public CustomerAddressData GetCustomerAddress()
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.GetCustomerAddress();
			}
		}

		public bool DeleteCustomerAddress(string addressID, string customerID)
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.DeleteCustomerAddress(addressID, customerID);
			}
		}

		public CustomerAddressData GetCustomerAddressByID(string customerID, string addressID)
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.GetCustomerAddressByID(customerID, addressID);
			}
		}

		public DataSet GetCustomerAddressByFields(params string[] columns)
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.GetCustomerAddressByFields(columns);
			}
		}

		public DataSet GetCustomerAddressByFields(string[] ids, params string[] columns)
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.GetCustomerAddressByFields(ids, columns);
			}
		}

		public DataSet GetCustomerAddressByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.GetCustomerAddressByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCustomerAddressList()
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.GetCustomerAddressList();
			}
		}

		public bool IsPrimaryAddress(string addresssID, string customerID)
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.IsPrimaryAddress(addresssID, customerID);
			}
		}

		public DataSet GetCustomerAddressComboList()
		{
			using (CustomerAddresses customerAddresses = new CustomerAddresses(config))
			{
				return customerAddresses.GetCustomerAddressComboList();
			}
		}
	}
}
