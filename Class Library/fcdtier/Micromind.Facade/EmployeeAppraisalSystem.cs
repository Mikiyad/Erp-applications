using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeAppraisalSystem : MarshalByRefObject, IEmployeeAppraisalSystem, IDisposable
	{
		private Config config;

		public EmployeeAppraisalSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePosition(EmployeeAppraisalData data)
		{
			return new EmployeeAppraisal(config).InsertPosition(data);
		}

		public bool UpdatePosition(EmployeeAppraisalData data)
		{
			return UpdatePosition(data, checkConcurrency: false);
		}

		public bool UpdatePosition(EmployeeAppraisalData data, bool checkConcurrency)
		{
			return new EmployeeAppraisal(config).UpdatePosition(data);
		}

		public EmployeeAppraisalData GetPosition()
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetPosition();
			}
		}

		public bool DeletePosition(string groupID, string SysDocid)
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.DeletePosition(groupID, SysDocid);
			}
		}

		public EmployeeAppraisalData GetPositionByID(string sysDocID, string id)
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetPositionByID(sysDocID, id);
			}
		}

		public DataSet GetPositionByFields(params string[] columns)
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetPositionByFields(columns);
			}
		}

		public DataSet GetPositionByFields(string[] ids, params string[] columns)
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetPositionByFields(ids, columns);
			}
		}

		public DataSet GetPositionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetPositionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPositionList()
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetPositionList();
			}
		}

		public DataSet GetPositionComboList()
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetPositionComboList();
			}
		}

		public DataSet GetEmployeeKPIList(string EmployeeID)
		{
			using (EmployeeAppraisal employeeAppraisal = new EmployeeAppraisal(config))
			{
				return employeeAppraisal.GetEmployeeKPIList(EmployeeID);
			}
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new EmployeeAppraisal(config).GetList(fromDate, toDate, showVoid: true);
		}

		public DataSet GetEmployeeAppraisalToPrint(string sysDocID, string voucherID)
		{
			return new EmployeeAppraisal(config).GetEmployeeAppraisalToPrint(sysDocID, voucherID);
		}
	}
}
