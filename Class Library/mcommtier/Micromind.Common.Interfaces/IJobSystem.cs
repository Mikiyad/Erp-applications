using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobSystem
	{
		bool CreateJob(JobData jobData);

		bool UpdateJob(JobData jobData);

		JobData GetJob();

		JobData GetJobFeeDetailsByID(string jobID);

		JobData GetJobBudgetDetailsByID(string jobID);

		JobData GetJobEquipmentDetailsByID(string jobID);

		bool DeleteJob(string ID);

		bool DeleteJobFee(string ID);

		JobData GetJobByID(string id);

		JobData GetJobFeeByID(string id);

		DataSet GetJobByFields(params string[] columns);

		DataSet GetJobByFields(string[] ids, params string[] columns);

		DataSet GetJobByFields(string[] ids, bool isInactive, params string[] columns);

		bool CreateJobFee(JobData jobData);

		bool UpdateJobFee(JobData jobData);

		bool CreateJobFeeDetails(JobData data, bool isNewRecord);

		bool CreateJobBudgetDetails(JobData data, bool isNewRecord);

		bool CreateJobEquipmentDetails(JobData data, bool isNewRecord);

		DataSet GetJobList();

		DataSet GetClosedJobList(DateTime from, DateTime to, bool showVoid);

		DataSet GetJobComboList();

		DataSet GetJobFeeList();

		DataSet GetJobFeeComboList();

		DataSet GetProjectStatusReport(string fromJob, string toJob, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetProjectFeesByID(string jobID);

		DataSet GetProjectVehicleByID(string jobID);

		DataSet GetJobSummary(string jobID);

		DataSet GetBillableItems(string jobID, bool includeInventory, bool includeTimesheet, bool includeExpense);

		DataSet GetProjectMaterialVarianceReport(string fromJob, string toJob, DateTime asOfDate);

		DataSet GetJobBudgetVsActualReport(string jobID);

		DataSet GetJobSummaryReport(DateTime fromDate, DateTime toDate, string jobID);

		bool CloseJob(JobData jobData);

		DataSet GetProjectFeesComboByJob(string jobID);

		DataSet GetProjectDueReport(string fromJob, string toJob, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime fromDate, DateTime toDate, string jobType, string customerIDs);

		DataSet GetProjectManPowerReport(string Job, string Employee, DateTime fromDate, DateTime toDate);

		DataSet GetServiceCallTrackReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromJob, string toJob, string customerIDs);

		DataSet GetPRojectInvoiceReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromJob, string toJob, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetMaterialRequisitionReport(string fromJob, string toJob, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime fromDate, DateTime toDate, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetJobReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive);

		DataSet GetSalesOrderItemsOnJobID(string jobID);
	}
}
