using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeBenefitDetailSystem : MarshalByRefObject, IEmployeeBenefitDetailSystem, IDisposable
	{
		private Config config;

		public EmployeeBenefitDetailSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeBenefitDetail(EmployeeBenefitDetailData data)
		{
			return new EmployeeBenefitDetails(config).InsertEmployeeBenefit(data);
		}

		public bool UpdateEmployeeBenefitDetail(EmployeeBenefitDetailData data)
		{
			return UpdateEmployeeBenefitDetail(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeBenefitDetail(EmployeeBenefitDetailData data, bool checkConcurrency)
		{
			return new EmployeeBenefitDetails(config).UpdateEmployeeBenefit(data);
		}

		public EmployeeBenefitDetailData GetEmployeeBenefitDetail()
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefit();
			}
		}

		public bool DeleteEmployeeBenefitDetail(string groupID)
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.DeleteEmployeeBenefit(groupID);
			}
		}

		public EmployeeBenefitDetailData GetEmployeeBenefitDetailByID(string id)
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefitByID(id);
			}
		}

		public EmployeeBenefitDetailData GetEmployeeBenefitDetailsByEmployeeID(string employeeID)
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefitsByEmployeeID(employeeID);
			}
		}

		public DataSet GetEmployeeBenefitDetailByFields(params string[] columns)
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefitByFields(columns);
			}
		}

		public DataSet GetEmployeeBenefitDetailByFields(string[] ids, params string[] columns)
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefitByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeBenefitDetailByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefitByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeBenefitDetailList()
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefitList();
			}
		}

		public DataSet GetEmployeeBenefitDetailComboList()
		{
			using (EmployeeBenefitDetails employeeBenefitDetails = new EmployeeBenefitDetails(config))
			{
				return employeeBenefitDetails.GetEmployeeBenefitComboList();
			}
		}
	}
}
