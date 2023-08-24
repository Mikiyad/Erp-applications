using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobSystem : MarshalByRefObject, IJobSystem, IDisposable
	{
		private Config config;

		public JobSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJob(JobData data)
		{
			return new Job(config).InsertJob(data);
		}

		public bool UpdateJob(JobData data)
		{
			return UpdateJob(data, checkConcurrency: false);
		}

		public bool UpdateJob(JobData data, bool checkConcurrency)
		{
			return new Job(config).UpdateJob(data);
		}

		public JobData GetJob()
		{
			using (Job job = new Job(config))
			{
				return job.GetJob();
			}
		}

		public JobData GetJobFeeDetailsByID(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobFeeDetailsByID(jobID);
			}
		}

		public JobData GetJobBudgetDetailsByID(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobBudgetDetailsByID(jobID);
			}
		}

		public JobData GetJobEquipmentDetailsByID(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobEquipmentDetailsByID(jobID);
			}
		}

		public bool DeleteJob(string groupID)
		{
			using (Job job = new Job(config))
			{
				return job.DeleteJob(groupID);
			}
		}

		public bool DeleteJobFee(string feeID)
		{
			using (Job job = new Job(config))
			{
				return job.DeleteJobFee(feeID);
			}
		}

		public JobData GetJobByID(string id)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobByID(id);
			}
		}

		public JobData GetJobFeeByID(string id)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobFeeByID(id);
			}
		}

		public DataSet GetJobByFields(params string[] columns)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobByFields(columns);
			}
		}

		public DataSet GetProjectStatusReport(string fromJob, string toJob, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Job job = new Job(config))
			{
				return job.GetProjectStatusReport(fromJob, toJob, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetJobByFields(string[] ids, params string[] columns)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobByFields(ids, columns);
			}
		}

		public DataSet GetJobByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobByFields(ids, isInactive, columns);
			}
		}

		public bool CreateJobFeeDetails(JobData data, bool isNewRecord)
		{
			using (new Job(config))
			{
				return new Job(config).InsertUpdateJobFeeDetails(data, isNewRecord);
			}
		}

		public bool CreateJobBudgetDetails(JobData data, bool isNewRecord)
		{
			using (new Job(config))
			{
				return new Job(config).InsertUpdateJobBudgetDetails(data, isNewRecord);
			}
		}

		public bool CreateJobEquipmentDetails(JobData data, bool isNewRecord)
		{
			using (new Job(config))
			{
				return new Job(config).InsertUpdateJobEquipmentDetails(data, isNewRecord);
			}
		}

		public bool CreateJobFee(JobData data)
		{
			return new Job(config).InsertJobFee(data);
		}

		public bool UpdateJobFee(JobData data)
		{
			using (Job job = new Job(config))
			{
				return job.UpdateJobFee(data);
			}
		}

		public DataSet GetJobList()
		{
			using (Job job = new Job(config))
			{
				return job.GetJobList();
			}
		}

		public DataSet GetClosedJobList(DateTime from, DateTime to, bool showVoid)
		{
			using (Job job = new Job(config))
			{
				return job.GetClosedJobList(from, to, showVoid);
			}
		}

		public DataSet GetJobComboList()
		{
			using (Job job = new Job(config))
			{
				return job.GetJobComboList();
			}
		}

		public DataSet GetJobFeeList()
		{
			using (Job job = new Job(config))
			{
				return job.GetJobFeeList();
			}
		}

		public DataSet GetJobFeeComboList()
		{
			using (Job job = new Job(config))
			{
				return job.GetJobFeeComboList();
			}
		}

		public DataSet GetProjectFeesByID(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetProjectFeesByID(jobID);
			}
		}

		public DataSet GetProjectVehicleByID(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetProjectVehicleByID(jobID);
			}
		}

		public DataSet GetJobSummary(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobSummary(jobID);
			}
		}

		public DataSet GetBillableItems(string jobID, bool includeInventory, bool includeTimesheet, bool includeExpense)
		{
			using (Job job = new Job(config))
			{
				return job.GetBillableItems(jobID, includeInventory, includeTimesheet, includeExpense);
			}
		}

		public DataSet GetSalesOrderItemsOnJobID(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetSalesOrderItemsOnJobID(jobID);
			}
		}

		public DataSet GetJobBudgetVsActualReport(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobBudgetVsActualReport(jobID);
			}
		}

		public DataSet GetServiceCallTrackReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromJob, string toJob, string customerIDs)
		{
			using (Job job = new Job(config))
			{
				return job.GetServiceCallTrackReport(fromDate, toDate, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromArea, toArea, fromCountry, toCountry, fromEmployee, toEmployee, fromDepartment, toDepartment, fromJob, toJob, customerIDs);
			}
		}

		public DataSet GetPRojectInvoiceReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromJob, string toJob, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Job job = new Job(config))
			{
				return job.GetPRojectInvoiceReport(fromDate, toDate, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromJob, toJob, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetProjectMaterialVarianceReport(string fromJob, string toJob, DateTime asOfDate)
		{
			using (Job job = new Job(config))
			{
				return job.GetProjectMaterialVarianceReport(fromJob, toJob, asOfDate);
			}
		}

		public bool CloseJob(JobData jobData)
		{
			using (Job job = new Job(config))
			{
				return job.CloseJob(jobData);
			}
		}

		public DataSet GetProjectFeesComboByJob(string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetProjectFeesComboByJob(jobID);
			}
		}

		public DataSet GetJobSummaryReport(DateTime fromDate, DateTime toDate, string jobID)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobSummaryReport(fromDate, toDate, jobID);
			}
		}

		public DataSet GetProjectDueReport(string fromJob, string toJob, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime fromDate, DateTime toDate, string jobType, string customerIDs)
		{
			using (Job job = new Job(config))
			{
				return job.GetProjectDueReport(fromJob, toJob, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromArea, toArea, fromCountry, toCountry, fromDate, toDate, jobType, customerIDs);
			}
		}

		public DataSet GetProjectManPowerReport(string Job, string Employee, DateTime fromDate, DateTime toDate)
		{
			using (Job job = new Job(config))
			{
				return job.GetProjectManPowerReport(Job, Employee, fromDate, toDate);
			}
		}

		public DataSet GetMaterialRequisitionReport(string fromJob, string toJob, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime fromDate, DateTime toDate, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Job job = new Job(config))
			{
				return job.GetMaterialRequisitionReport(fromJob, toJob, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromLocation, toLocation, fromDate, toDate, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetJobReport(string fromJob, string toJob, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			using (Job job = new Job(config))
			{
				return job.GetJobReport(fromJob, toJob, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, showInactive);
			}
		}
	}
}
