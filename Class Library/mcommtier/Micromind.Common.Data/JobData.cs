using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobData : DataSet
	{
		public const string JOB_TABLE = "Job";

		public const string JOB_FEE_TABLE = "Job_Fee";

		public const string JOB_FEEDETAIL_TABLE = "Job_Fee_Detail";

		public const string JOB_BUDGETDETAIL_TABLE = "Job_Budget";

		public const string JOB_FEEE_PAYMENT_TERM_TABLE = "Job_FEE_PAYMENT_TERM";

		public const string JOB_EQUIPMENTDETAIL_TABLE = "Job_Equipment";

		public const string JOB_VEHICLE_DETAIL_TABLE = "Job_Vehicle_Detail";

		public const string JOBID_FIELD = "JobID";

		public const string JOBNAME_FIELD = "JobName";

		public const string STATUS_FIELD = "Status";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string CUSTOMERNAME_FIELD = "CustomerName";

		public const string ISINACTIVE_FIELD = "Inactive";

		public const string SALESPERSONID_FIELD = "SalesPersonID";

		public const string PROJECTMGRID_FIELD = "ProjectManagerID";

		public const string JOBTYPEID_FIELD = "JobTypeID";

		public const string REFERENCE_FIELD = "Reference";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string PONUMBER_FIELD = "PONumber";

		public const string SITELOCATIONID_FIELD = "SiteLocationID";

		public const string SITELOCATIONADDRESS_FIELD = "SiteLocationAddress";

		public const string WIPACCOUNTID_FIELD = "WIPAccountID";

		public const string INCOMEACCOUNTID_FIELD = "IncomeAccountID";

		public const string TIMESHEETCONTRAACCOUNTID_FIELD = "TimesheetContraAccountID";

		public const string RETENTIONACCOUNTID_FIELD = "RetentionAccountID";

		public const string ADVANCEACCOUNTID_FIELD = "AdvanceAccountID";

		public const string COSTACCOUNTID_FIELD = "CostAccountID";

		public const string ADVANCEITEMID_FIELD = "AdvanceItemID";

		public const string ADVANCEAMOUNT_FIELD = "AdvanceAmount";

		public const string ADVANCEBILLED_FIELD = "AdvanceBilled";

		public const string ADVANCEAPPLIED_FIELD = "AdvanceApplied";

		public const string ADVANCEDESCRIPTION_FIELD = "AdvanceDescription";

		public const string PROJECTAMOUNT_FIELD = "ProjectAmount";

		public const string PROJECTBUDGET_FIELD = "ProjectBudget";

		public const string RETENTIONITEMID_FIELD = "RetentionItemID";

		public const string RETENTIONDESCRIPTION_FIELD = "RetentionDescription";

		public const string RETENTIONPERCENT_FIELD = "RetentionPercent";

		public const string RETENTIONDAYS_FIELD = "RetentionDays";

		public const string RETENTIONDATE_FIELD = "RetentionDate";

		public const string RETENTIONAMOUNT_FIELD = "RetentionAmount";

		public const string RETENTIONPAID_FIELD = "RetentionPaid";

		public const string COMPLETEDPERCENT_FIELD = "CompletedPercent";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string FEEID_FIELD = "FeeID";

		public const string FEENAME_FIELD = "FeeName";

		public const string FEETYPE_FIELD = "FeeType";

		public const string FEEAMOUNT_FIELD = "FeeAmount";

		public const string INACTIVE_FIELD = "Inactive";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string AMOUNT_FIELD = "Amount";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string DUEDATE_FIELD = "DueDate";

		public const string COMPLETEDPERCENTAGE_FIELD = "CompletedPercentage";

		public const string AMOUNTPERCENT_FIELD = "AmountPercent";

		public const string TERMTYPE_FIELD = "TermType";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string UNITCOST_FIELD = "UnitCost";

		public const string TOTALCOST_FIELD = "TotalCost";

		public const string EQUIPMENTID_FIELD = "EquipmentID";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string REGISTRATIONNUMBER_FIELD = "RegistrationNumber";

		public const string COLOR_FIELD = "Color";

		public const string MODEL_FIELD = "Model";

		public const string ODOMETER_FIELD = "Odometer";

		public const string MISCELLANEOUSAMOUNT_FIELD = "MiscellaneousAmount";

		public const string MISCELLANEOUSVARIANCE_FIELD = "MiscellaneousVariance";

		public const string LABORAMOUNT_FIELD = "LaborAmount";

		public const string LABORVARIANCE_FIELD = "LaborVariance";

		public const string OVERHEADAMOUNT_FIELD = "OverHeadAmount";

		public const string OVERHEADVARIANCE_FIELD = "OverHeadVariance";

		public const string PROFIT_FIELD = "Profit";

		public DataTable JobTable => base.Tables["Job"];

		public DataTable JobFeeTable => base.Tables["Job_Fee_Detail"];

		public DataTable JobBudgetTable => base.Tables["Job_Budget"];

		public DataTable JobFeePaymentTermTable => base.Tables["Job_FEE_PAYMENT_TERM"];

		public DataTable ProjectFeeTable => base.Tables["Job_Fee"];

		public DataTable JobEquipmentTable => base.Tables["Job_Equipment"];

		public DataTable JobVehicleDetailTable => base.Tables["Job_Vehicle_Detail"];

		public JobData()
		{
			BuildDataTables();
		}

		public JobData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("JobID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("JobName", typeof(string)).AllowDBNull = false;
			columns.Add("Status", typeof(byte));
			columns.Add("CustomerID", typeof(string)).AllowDBNull = false;
			columns.Add("SalesPersonID", typeof(string));
			columns.Add("ProjectManagerID", typeof(string));
			columns.Add("WIPAccountID", typeof(string));
			columns.Add("IncomeAccountID", typeof(string));
			columns.Add("CostAccountID", typeof(string));
			columns.Add("TimesheetContraAccountID", typeof(string));
			columns.Add("RetentionAccountID", typeof(string));
			columns.Add("AdvanceAccountID", typeof(string));
			columns.Add("JobTypeID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("SiteLocationID", typeof(string));
			columns.Add("SiteLocationAddress", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("RetentionItemID", typeof(string));
			columns.Add("RetentionDescription", typeof(string));
			columns.Add("RetentionPercent", typeof(decimal));
			columns.Add("RetentionDays", typeof(decimal));
			columns.Add("RetentionAmount", typeof(decimal));
			columns.Add("RetentionPaid", typeof(decimal));
			columns.Add("RetentionDate", typeof(DateTime));
			columns.Add("CompletedPercent", typeof(decimal));
			columns.Add("ProjectAmount", typeof(decimal));
			columns.Add("ProjectBudget", typeof(decimal));
			columns.Add("StartDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("EndDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Note", typeof(string));
			columns.Add("SysDocID", typeof(string)).DefaultValue = DBNull.Value;
			columns.Add("VoucherID", typeof(string)).DefaultValue = DBNull.Value;
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Inactive", typeof(bool));
			columns.Add("MiscellaneousAmount", typeof(decimal));
			columns.Add("MiscellaneousVariance", typeof(decimal));
			columns.Add("LaborAmount", typeof(decimal));
			columns.Add("LaborVariance", typeof(decimal));
			columns.Add("OverHeadAmount", typeof(decimal));
			columns.Add("OverHeadVariance", typeof(decimal));
			columns.Add("Profit", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Fee");
			columns = dataTable.Columns;
			columns.Add("FeeID", typeof(string)).AllowDBNull = false;
			columns.Add("FeeName", typeof(string)).AllowDBNull = false;
			columns.Add("FeeType", typeof(byte));
			columns.Add("FeeAmount", typeof(decimal));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Note", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Fee_Detail");
			columns = dataTable.Columns;
			columns.Add("JobID", typeof(string));
			columns.Add("FeeID", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Quantity", typeof(float));
			columns.Add("Amount", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Budget");
			columns = dataTable.Columns;
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitCost", typeof(decimal)).DefaultValue = 0;
			columns.Add("Quantity", typeof(float));
			columns.Add("TotalCost", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			dataTable = new DataTable("Job_Equipment");
			columns = dataTable.Columns;
			columns.Add("JobID", typeof(string));
			columns.Add("EquipmentID", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_FEE_PAYMENT_TERM");
			columns = dataTable.Columns;
			columns.Add("JobID", typeof(string)).AllowDBNull = false;
			columns.Add("FeeID", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("CompletedPercentage", typeof(decimal)).DefaultValue = 0;
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountPercent", typeof(decimal));
			columns.Add("TermType", typeof(byte));
			columns.Add("RowIndex", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Vehicle_Detail");
			columns = dataTable.Columns;
			columns.Add("JobID", typeof(string)).AllowDBNull = false;
			columns.Add("VehicleID", typeof(string)).AllowDBNull = false;
			columns.Add("RegistrationNumber", typeof(string));
			columns.Add("Color", typeof(string));
			columns.Add("Model", typeof(string));
			columns.Add("Odometer", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
