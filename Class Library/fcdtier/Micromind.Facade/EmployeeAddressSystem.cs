using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeAddressSystem : MarshalByRefObject, IEmployeeAddressSystem, IDisposable
	{
		private Config config;

		public EmployeeAddressSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeAddress(EmployeeAddressData data)
		{
			return new EmployeeAddresses(config).InsertEmployeeAddress(data);
		}

		public bool UpdateEmployeeAddress(EmployeeAddressData data)
		{
			return UpdateEmployeeAddress(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeAddress(EmployeeAddressData data, bool checkConcurrency)
		{
			return new EmployeeAddresses(config).UpdateEmployeeAddress(data);
		}

		public EmployeeAddressData GetEmployeeAddress()
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.GetEmployeeAddress();
			}
		}

		public bool DeleteEmployeeAddress(string addressID, string employeeID)
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.DeleteEmployeeAddress(addressID, employeeID);
			}
		}

		public EmployeeAddressData GetEmployeeAddressByID(string employeeID, string addressID)
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.GetEmployeeAddressByID(employeeID, addressID);
			}
		}

		public DataSet GetEmployeeAddressByFields(params string[] columns)
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.GetEmployeeAddressByFields(columns);
			}
		}

		public DataSet GetEmployeeAddressByFields(string[] ids, params string[] columns)
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.GetEmployeeAddressByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeAddressByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.GetEmployeeAddressByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeAddressList()
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.GetEmployeeAddressList();
			}
		}

		public bool IsPrimaryAddress(string addresssID, string employeeID)
		{
			using (EmployeeAddresses employeeAddresses = new EmployeeAddresses(config))
			{
				return employeeAddresses.IsPrimaryAddress(addresssID, employeeID);
			}
		}
	}
}
