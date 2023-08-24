using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeePayrollItemDetailSystem : MarshalByRefObject, IEmployeePayrollItemDetailSystem, IDisposable
	{
		private Config config;

		public EmployeePayrollItemDetailSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeePayrollItemDetail(EmployeePayrollItemDetailData data)
		{
			return new EmployeePayrollItemDetails(config).InsertEmployeePayrollItem(data);
		}

		public bool UpdateEmployeePayrollItemDetail(EmployeePayrollItemDetailData data)
		{
			return UpdateEmployeePayrollItemDetail(data, checkConcurrency: false);
		}

		public bool UpdateEmployeePayrollItemDetail(EmployeePayrollItemDetailData data, bool checkConcurrency)
		{
			return new EmployeePayrollItemDetails(config).UpdateEmployeePayrollItem(data);
		}

		public EmployeePayrollItemDetailData GetEmployeePayrollItemDetail()
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItem();
			}
		}

		public bool DeleteEmployeePayrollItemDetail(string groupID)
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.DeleteEmployeePayrollItem(groupID);
			}
		}

		public EmployeePayrollItemDetailData GetEmployeePayrollItemDetailByID(string id)
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItemByID(id);
			}
		}

		public EmployeePayrollItemDetailData GetEmployeePayrollItemDetailsByEmployeeID(string employeeID)
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItemsByEmployeeID(employeeID);
			}
		}

		public DataSet GetEmployeePayrollItemDetailByFields(params string[] columns)
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItemByFields(columns);
			}
		}

		public DataSet GetEmployeePayrollItemDetailByFields(string[] ids, params string[] columns)
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItemByFields(ids, columns);
			}
		}

		public DataSet GetEmployeePayrollItemDetailByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItemByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeePayrollItemDetailList()
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItemList();
			}
		}

		public DataSet GetEmployeePayrollItemDetailComboList()
		{
			using (EmployeePayrollItemDetails employeePayrollItemDetails = new EmployeePayrollItemDetails(config))
			{
				return employeePayrollItemDetails.GetEmployeePayrollItemComboList();
			}
		}
	}
}
