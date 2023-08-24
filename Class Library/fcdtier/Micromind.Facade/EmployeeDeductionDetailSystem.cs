using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeDeductionDetailSystem : MarshalByRefObject, IEmployeeDeductionDetailSystem, IDisposable
	{
		private Config config;

		public EmployeeDeductionDetailSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeDeductionDetail(EmployeeDeductionDetailData data)
		{
			return new EmployeeDeductionDetails(config).InsertEmployeeDeduction(data);
		}

		public bool UpdateEmployeeDeductionDetail(EmployeeDeductionDetailData data)
		{
			return UpdateEmployeeDeductionDetail(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeDeductionDetail(EmployeeDeductionDetailData data, bool checkConcurrency)
		{
			return new EmployeeDeductionDetails(config).UpdateEmployeeDeduction(data);
		}

		public EmployeeDeductionDetailData GetEmployeeDeductionDetail()
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeduction();
			}
		}

		public bool DeleteEmployeeDeductionDetail(string groupID)
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.DeleteEmployeeDeduction(groupID);
			}
		}

		public EmployeeDeductionDetailData GetEmployeeDeductionDetailByID(string id)
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeductionByID(id);
			}
		}

		public EmployeeDeductionDetailData GetEmployeeDeductionDetailsByEmployeeID(string employeeID)
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeductionsByEmployeeID(employeeID);
			}
		}

		public DataSet GetEmployeeDeductionDetailByFields(params string[] columns)
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeductionByFields(columns);
			}
		}

		public DataSet GetEmployeeDeductionDetailByFields(string[] ids, params string[] columns)
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeductionByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeDeductionDetailByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeductionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeDeductionDetailList()
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeductionList();
			}
		}

		public DataSet GetEmployeeDeductionDetailComboList()
		{
			using (EmployeeDeductionDetails employeeDeductionDetails = new EmployeeDeductionDetails(config))
			{
				return employeeDeductionDetails.GetEmployeeDeductionComboList();
			}
		}
	}
}
