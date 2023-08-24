using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Job : StoreObject
	{
		private const string JOBID_PARM = "@JobID";

		private const string JOBNAME_PARM = "@JobName";

		private const string STATUS_PARM = "@Status";

		private const string CUSTOMERID_PARM = "@CustomerID";

		public const string SALESPERSONID_PARM = "@SalesPersonID";

		public const string PROJECTMGRID_PARM = "@ProjectManagerID";

		public const string JOBTYPEID_PARM = "@JobTypeID";

		public const string REFERENCE_PARM = "@Reference";

		public const string CURRENCYID_PARM = "@CurrencyID";

		public const string PONUMBER_PARM = "@PONumber";

		public const string WIPACCOUNTID_PARM = "@WIPAccountID";

		public const string INCOMEACCOUNTID_PARM = "@IncomeAccountID";

		public const string TIMESHEETCONTRAACCOUNTID_PARM = "@TimesheetContraAccountID";

		public const string RETENTIONACCOUNTID_PARM = "@RetentionAccountID";

		public const string ADVANCEACCOUNTID_PARM = "@AdvanceAccountID";

		public const string COSTACCOUNTID_PARM = "@CostAccountID";

		public const string RETENTIONITEMID_PARM = "@RetentionItemID";

		public const string RETENTIONDESCRIPTION_PARM = "@RetentionDescription";

		public const string RETENTIONPERCENT_PARM = "@RetentionPercent";

		public const string RETENTIONAMOUNT_PARM = "@RetentionAmount";

		public const string RETENTIONPAID_PARM = "@RetentionPaid";

		public const string RETENTIONDAYS_PARAM = "@RetentionDays";

		public const string RETENTIONDATE_PARAM = "@RetentionDate";

		public const string COMPLETEDPERCENT_PARM = "@CompletedPercent";

		public const string PROJECTAMOUNT_PARM = "@ProjectAmount";

		public const string PROJECTBUDGET_PARM = "@ProjectBudget";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		public const string NOTE_PARM = "@Note";

		public const string JOB_TABLE = "Job";

		public const string JOB_FEEDETAILS_TABLE = "Job_Fee_Details";

		public const string SITELOCATIONID_PARM = "@SiteLocationID";

		public const string SITELOCATIONADDRESS_PARM = "@SiteLocationAddress";

		private const string FEEID_PARM = "@FeeID";

		private const string FEENAME_PARM = "@FeeName";

		private const string FEETYPE_PARM = "@FeeType";

		private const string FEEAMOUNT_PARM = "@FeeAmount";

		private const string INACTIVE_PARM = "@Inactive";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		public const string PRODUCTID_PARM = "@ProductID";

		public const string QUANTITY_PARM = "@Quantity";

		public const string UNITPRICE_PARM = "@UnitPrice";

		public const string DESCRIPTION_PARM = "@Description";

		public const string UNITID_PARM = "@UnitID";

		public const string AMOUNT_PARM = "@Amount";

		public const string ROWINDEX_PARM = "@RowIndex";

		public const string DUEDATE_PARM = "@DueDate";

		public const string COMPLETEDPERCENTAGE_PARM = "@CompletedPercentage";

		public const string AMOUNTPERCENT_PARM = "@AmountPercent";

		public const string TERMTYPE_PARM = "@TermType";

		public const string COSTCATEGORYID_PARM = "@CostCategoryID";

		public const string UNITCOST_PARM = "@UnitCost";

		public const string TOTALCOST_PARM = "@TotalCost";

		private const string EQUIPMENTID_PARM = "@EquipmentID";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string COLOR_PARM = "@Color";

		private const string MODEL_PARM = "@Model";

		private const string ODOMETER_PARM = "@Odometer";

		private const string REGISTRATIONNUMBER_PARM = "@RegistrationNumber";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string MISCELLANEOUSAMOUNT_PARM = "@MiscellaneousAmount";

		private const string MISCELLANEOUSVARIANCE_PARM = "@MiscellaneousVariance";

		private const string LABORAMOUNT_PARM = "@LaborAmount";

		private const string LABORVARIANCE_PARM = "@LaborVariance";

		private const string OVERHEADAMOUNT_PARM = "@OverHeadAmount";

		private const string OVERHEADVARIANCE_PARM = "@OverHeadVariance";

		private const string PROFIT_PARM = "@Profit";

		public Job(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job", new FieldValue("JobID", "@JobID", isUpdateConditionField: true), new FieldValue("JobName", "@JobName"), new FieldValue("Status", "@Status"), new FieldValue("WIPAccountID", "@WIPAccountID"), new FieldValue("IncomeAccountID", "@IncomeAccountID"), new FieldValue("TimesheetContraAccountID", "@TimesheetContraAccountID"), new FieldValue("RetentionAccountID", "@RetentionAccountID"), new FieldValue("CostAccountID", "@CostAccountID"), new FieldValue("AdvanceAccountID", "@AdvanceAccountID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesPersonID", "@SalesPersonID"), new FieldValue("ProjectManagerID", "@ProjectManagerID"), new FieldValue("JobTypeID", "@JobTypeID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Reference", "@Reference"), new FieldValue("PONumber", "@PONumber"), new FieldValue("RetentionItemID", "@RetentionItemID"), new FieldValue("RetentionDescription", "@RetentionDescription"), new FieldValue("RetentionPercent", "@RetentionPercent"), new FieldValue("RetentionAmount", "@RetentionAmount"), new FieldValue("CompletedPercent", "@CompletedPercent"), new FieldValue("RetentionPaid", "@RetentionPaid"), new FieldValue("RetentionDays", "@RetentionDays"), new FieldValue("RetentionDate", "@RetentionDate"), new FieldValue("StartDate", "@StartDate"), new FieldValue("ProjectAmount", "@ProjectAmount"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Note", "@Note"), new FieldValue("Inactive", "@Inactive"), new FieldValue("MiscellaneousAmount", "@MiscellaneousAmount"), new FieldValue("MiscellaneousVariance", "@MiscellaneousVariance"), new FieldValue("LaborAmount", "@LaborAmount"), new FieldValue("LaborVariance", "@LaborVariance"), new FieldValue("OverHeadAmount", "@OverHeadAmount"), new FieldValue("OverHeadVariance", "@OverHeadVariance"), new FieldValue("Profit", "@Profit"), new FieldValue("SiteLocationID", "@SiteLocationID"), new FieldValue("SiteLocationAddress", "@SiteLocationAddress"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@JobName", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@WIPAccountID", SqlDbType.NVarChar);
			parameters.Add("@IncomeAccountID", SqlDbType.NVarChar);
			parameters.Add("@TimesheetContraAccountID", SqlDbType.NVarChar);
			parameters.Add("@RetentionAccountID", SqlDbType.NVarChar);
			parameters.Add("@AdvanceAccountID", SqlDbType.NVarChar);
			parameters.Add("@CostAccountID", SqlDbType.NVarChar);
			parameters.Add("@SalesPersonID", SqlDbType.NVarChar);
			parameters.Add("@ProjectManagerID", SqlDbType.NVarChar);
			parameters.Add("@JobTypeID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RetentionItemID", SqlDbType.NVarChar);
			parameters.Add("@RetentionDescription", SqlDbType.NVarChar);
			parameters.Add("@RetentionPercent", SqlDbType.Decimal);
			parameters.Add("@CompletedPercent", SqlDbType.Decimal);
			parameters.Add("@RetentionAmount", SqlDbType.Money);
			parameters.Add("@RetentionPaid", SqlDbType.Money);
			parameters.Add("@RetentionDays", SqlDbType.Decimal);
			parameters.Add("@RetentionDate", SqlDbType.Date);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@ProjectAmount", SqlDbType.Money);
			parameters.Add("@ProjectBudget", SqlDbType.Money);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@SiteLocationID", SqlDbType.NVarChar);
			parameters.Add("@SiteLocationAddress", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@MiscellaneousAmount", SqlDbType.Decimal);
			parameters.Add("@MiscellaneousVariance", SqlDbType.Decimal);
			parameters.Add("@LaborAmount", SqlDbType.Decimal);
			parameters.Add("@LaborVariance", SqlDbType.Decimal);
			parameters.Add("@OverHeadAmount", SqlDbType.Decimal);
			parameters.Add("@OverHeadVariance", SqlDbType.Decimal);
			parameters.Add("@Profit", SqlDbType.Decimal);
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@JobName"].SourceColumn = "JobName";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@WIPAccountID"].SourceColumn = "WIPAccountID";
			parameters["@IncomeAccountID"].SourceColumn = "IncomeAccountID";
			parameters["@TimesheetContraAccountID"].SourceColumn = "TimesheetContraAccountID";
			parameters["@RetentionAccountID"].SourceColumn = "RetentionAccountID";
			parameters["@AdvanceAccountID"].SourceColumn = "AdvanceAccountID";
			parameters["@CostAccountID"].SourceColumn = "CostAccountID";
			parameters["@SalesPersonID"].SourceColumn = "SalesPersonID";
			parameters["@ProjectManagerID"].SourceColumn = "ProjectManagerID";
			parameters["@JobTypeID"].SourceColumn = "JobTypeID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@RetentionItemID"].SourceColumn = "RetentionItemID";
			parameters["@RetentionDescription"].SourceColumn = "RetentionDescription";
			parameters["@RetentionPercent"].SourceColumn = "RetentionPercent";
			parameters["@CompletedPercent"].SourceColumn = "CompletedPercent";
			parameters["@RetentionAmount"].SourceColumn = "RetentionAmount";
			parameters["@RetentionPaid"].SourceColumn = "RetentionPaid";
			parameters["@RetentionDays"].SourceColumn = "RetentionDays";
			parameters["@RetentionDate"].SourceColumn = "RetentionDate";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@ProjectAmount"].SourceColumn = "ProjectAmount";
			parameters["@ProjectBudget"].SourceColumn = "ProjectBudget";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@SiteLocationID"].SourceColumn = "SiteLocationID";
			parameters["@SiteLocationAddress"].SourceColumn = "SiteLocationAddress";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@MiscellaneousAmount"].SourceColumn = "MiscellaneousAmount";
			parameters["@MiscellaneousVariance"].SourceColumn = "MiscellaneousVariance";
			parameters["@LaborAmount"].SourceColumn = "LaborAmount";
			parameters["@LaborVariance"].SourceColumn = "LaborVariance";
			parameters["@OverHeadAmount"].SourceColumn = "OverHeadAmount";
			parameters["@OverHeadVariance"].SourceColumn = "OverHeadVariance";
			parameters["@Profit"].SourceColumn = "Profit";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateJobFeeDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Fee_Detail", new FieldValue("JobID", "@JobID"), new FieldValue("FeeID", "@FeeID"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Amount", "@Amount"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Fee_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateJobBudgetDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Budget", new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("Description", "@Description"), new FieldValue("UnitCost", "@UnitCost"), new FieldValue("Quantity", "@Quantity"), new FieldValue("TotalCost", "@TotalCost"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Budget", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateJobFeePaymentTermText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_FEE_PAYMENT_TERM", new FieldValue("JobID", "@JobID"), new FieldValue("FeeID", "@FeeID"), new FieldValue("Description", "@Description"), new FieldValue("DueDate", "@DueDate"), new FieldValue("CompletedPercentage", "@CompletedPercentage"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountPercent", "@AmountPercent"), new FieldValue("TermType", "@TermType"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobFeePaymentTermCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobFeePaymentTermText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobFeePaymentTermText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@FeeID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@CompletedPercentage", SqlDbType.Decimal);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountPercent", SqlDbType.Decimal);
			parameters.Add("@TermType", SqlDbType.TinyInt);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@FeeID"].SourceColumn = "FeeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@CompletedPercentage"].SourceColumn = "CompletedPercentage";
			parameters["@AmountPercent"].SourceColumn = "AmountPercent";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@TermType"].SourceColumn = "TermType";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateJobBudgetDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobBudgetDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobBudgetDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitCost", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@TotalCost", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitCost"].SourceColumn = "UnitCost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@TotalCost"].SourceColumn = "TotalCost";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateJobEquipmentDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Equipment", new FieldValue("JobID", "@JobID"), new FieldValue("EquipmentID", "@EquipmentID"), new FieldValue("Description", "@Description"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Equipment", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateJobVehicleDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Vehicle_Detail", new FieldValue("JobID", "@JobID", isUpdateConditionField: true), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("Color", "@Color"), new FieldValue("Odometer", "@Odometer"), new FieldValue("RegistrationNumber", "@RegistrationNumber"), new FieldValue("Model", "@Model"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Vehicle_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobEquipmentDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobEquipmentDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobEquipmentDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@EquipmentID"].SourceColumn = "EquipmentID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateJobFeeDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobFeeDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobFeeDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@FeeID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@FeeID"].SourceColumn = "FeeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateJobFeeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Fee", new FieldValue("FeeID", "@FeeID", isUpdateConditionField: true), new FieldValue("FeeName", "@FeeName"), new FieldValue("FeeType", "@FeeType"), new FieldValue("FeeAmount", "@FeeAmount"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Inactive", "@Inactive"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Fee", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobFeeCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobFeeText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobFeeText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@FeeID", SqlDbType.NVarChar);
			parameters.Add("@FeeName", SqlDbType.NVarChar);
			parameters.Add("@FeeType", SqlDbType.TinyInt);
			parameters.Add("@FeeAmount", SqlDbType.Money);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@FeeID"].SourceColumn = "FeeID";
			parameters["@FeeName"].SourceColumn = "FeeName";
			parameters["@FeeType"].SourceColumn = "FeeType";
			parameters["@FeeAmount"].SourceColumn = "FeeAmount";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateJobVehicleDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobVehicleDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobVehicleDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@RegistrationNumber", SqlDbType.NVarChar);
			parameters.Add("@Color", SqlDbType.NVarChar);
			parameters.Add("@Model", SqlDbType.NVarChar);
			parameters.Add("@Odometer", SqlDbType.NVarChar);
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@Color"].SourceColumn = "Color";
			parameters["@Model"].SourceColumn = "Model";
			parameters["@RegistrationNumber"].SourceColumn = "RegistrationNumber";
			parameters["@Odometer"].SourceColumn = "Odometer";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateJobFeeDetails(JobData jobData, bool isUpdate)
		{
			bool flag = true;
			decimal num = default(decimal);
			SqlCommand sqlCommand = null;
			try
			{
				DataRow dataRow = jobData.JobTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["JobID"].ToString();
				if (dataRow["ProjectAmount"] != DBNull.Value)
				{
					num = decimal.Parse(dataRow["ProjectAmount"].ToString());
				}
				foreach (DataRow row in jobData.JobFeeTable.Rows)
				{
					row["JobID"] = dataRow["JobID"];
				}
				sqlCommand = GetInsertUpdateJobFeeDetailsCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteJobFeeDetailsRows(text, sqlTransaction);
				}
				if (jobData.Tables["Job_Fee_Detail"].Rows.Count > 0)
				{
					flag &= Insert(jobData, "Job_Fee_Detail", sqlCommand);
				}
				string commandText;
				if (flag)
				{
					commandText = "UPDATE Job SET ProjectAmount = " + num + " WHERE JobID='" + text + "' ";
					flag &= Update(commandText, sqlTransaction);
				}
				if (isUpdate)
				{
					flag &= DeleteJobFeePaymentTermRows(text, sqlTransaction);
				}
				sqlCommand = GetInsertUpdateJobFeePaymentTermCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (jobData.Tables["Job_FEE_PAYMENT_TERM"].Rows.Count > 0)
				{
					flag &= Insert(jobData, "Job_FEE_PAYMENT_TERM", sqlCommand);
				}
				commandText = ((dataRow["RetentionItemID"] == DBNull.Value) ? ("UPDATE JOB SET RetentionItemID = NULL , RetentionDescription = NULL, RetentionPercent = NULL\r\n\t\t\t\t\t\t\tWHERE JobID = '" + text + "'") : ("UPDATE JOB SET RetentionItemID = '" + dataRow["RetentionItemID"].ToString() + "', RetentionDescription = '" + dataRow["RetentionDescription"].ToString() + "', RetentionPercent = " + dataRow["RetentionPercent"].ToString() + "\r\n\t\t\t\t\t\t\tWHERE JobID = '" + text + "'"));
				flag &= Update(commandText, sqlTransaction);
				commandText = ((dataRow["AdvanceItemID"] == DBNull.Value) ? ("UPDATE JOB SET AdvanceItemID = NULL , AdvanceDescription = NULL, AdvanceAmount = NULL\r\n\t\t\t\t\t\t\tWHERE JobID = '" + text + "'") : ("UPDATE JOB SET AdvanceItemID = '" + dataRow["AdvanceItemID"].ToString() + "', AdvanceDescription = '" + dataRow["AdvanceDescription"].ToString() + "', AdvanceAmount = " + dataRow["AdvanceAmount"].ToString() + "\r\n\t\t\t\t\t\t\tWHERE JobID = '" + text + "'"));
				flag &= Update(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Fee_Detail", "JobID", dataRow["JobID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Fee";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool InsertUpdateJobBudgetDetails(JobData jobData, bool isUpdate)
		{
			bool flag = true;
			decimal num = default(decimal);
			SqlCommand sqlCommand = null;
			try
			{
				DataRow dataRow = jobData.JobTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["JobID"].ToString();
				if (dataRow["ProjectBudget"] != DBNull.Value)
				{
					num = decimal.Parse(dataRow["ProjectBudget"].ToString());
				}
				foreach (DataRow row in jobData.JobBudgetTable.Rows)
				{
					row["JobID"] = dataRow["JobID"];
				}
				sqlCommand = GetInsertUpdateJobBudgetDetailsCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteJobBudgetDetailsRows(text, sqlTransaction);
				}
				flag &= Insert(jobData, "Job_Budget", sqlCommand);
				if (flag)
				{
					string commandText = "UPDATE Job SET ProjectBudget = " + num + " WHERE JobID='" + text + "' ";
					flag &= Update(commandText, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Budget", "JobID", dataRow["JobID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Budget";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool InsertUpdateJobEquipmentDetails(JobData jobData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			try
			{
				DataRow dataRow = jobData.JobTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["JobID"].ToString();
				foreach (DataRow row in jobData.JobEquipmentTable.Rows)
				{
					row["JobID"] = dataRow["JobID"];
				}
				sqlCommand = GetInsertUpdateJobEquipmentDetailsCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteJobEquipmentDetailsRows(text, sqlTransaction);
				}
				if (jobData.Tables["Job_Equipment"].Rows.Count > 0)
				{
					flag &= Insert(jobData, "Job_Equipment", sqlCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Equipment", "JobID", dataRow["JobID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Equipment";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool InsertUpdateJobVehicleDetails(JobData jobData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			try
			{
				DataRow dataRow = jobData.JobVehicleDetailTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["JobID"].ToString();
				foreach (DataRow row in jobData.JobEquipmentTable.Rows)
				{
					row["JobID"] = dataRow["JobID"];
				}
				sqlCommand = GetInsertUpdateJobVehicleDetailsCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteJobVehicleDetailsRows(text, sqlTransaction);
				}
				if (jobData.Tables["Job_Vehicle_Detail"].Rows.Count > 0)
				{
					flag &= Insert(jobData, "Job_Vehicle_Detail", sqlCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Vehicle_Detail", "JobID", dataRow["JobID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Vehicle";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool InsertJob(JobData jobData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(jobData, "Job", insertUpdateCommand);
				if (flag && jobData.JobVehicleDetailTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateJobVehicleDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(jobData, "Job_Vehicle_Detail", insertUpdateCommand);
				}
				string text = jobData.JobTable.Rows[0]["JobID"].ToString();
				AddActivityLog("Job", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job", "JobID", text, sqlTransaction, isInsert: true);
				UpdateTableRowInsertUpdateInfo("Job_Vehicle_Detail", "JobID", text, sqlTransaction, isInsert: true);
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Job, text.ToString(), "Job", "JobID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdateJob(JobData accountJobData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountJobData, "Job", insertUpdateCommand);
				if (flag && accountJobData.JobVehicleDetailTable.Rows.Count > 0)
				{
					string id = accountJobData.JobVehicleDetailTable.Rows[0]["JobID"].ToString();
					DataSet jobByID = GetJobByID(id);
					if (jobByID != null && jobByID.Tables.Count > 0 && jobByID.Tables["Job_Vehicle_Detail"].Rows.Count > 0)
					{
						insertUpdateCommand = GetInsertUpdateJobVehicleDetailsCommand(isUpdate: true);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Update(accountJobData, "Job_Vehicle_Detail", insertUpdateCommand);
					}
					else
					{
						insertUpdateCommand = GetInsertUpdateJobVehicleDetailsCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Insert(accountJobData, "Job_Vehicle_Detail", insertUpdateCommand);
					}
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountJobData.JobTable.Rows[0]["JobID"];
				UpdateTableRowByID("Job", "JobID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountJobData.JobTable.Rows[0]["JobName"].ToString();
				AddActivityLog("Job", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job", "JobID", obj, sqlTransaction, isInsert: false);
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Job, obj.ToString(), "Job", "JobID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool CloseJob(JobData jobData)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = jobData.JobTable.Rows[0];
				string text = dataRow["JobID"].ToString();
				byte b = byte.Parse(dataRow["Status"].ToString());
				string commandText = "UPDATE Job SET Status = " + b + " WHERE JobID='" + text + "' ";
				flag &= Update(commandText, sqlTransaction);
				GLData journalData = CreateJobClosingGLData(jobData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction);
				if (flag)
				{
					object obj = jobData.JobTable.Rows[0]["JobID"];
					UpdateTableRowByID("Job", "JobID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
					string entiyID = jobData.JobTable.Rows[0]["JobName"].ToString();
					AddActivityLog("Job", entiyID, ActivityTypes.Update, sqlTransaction);
					UpdateTableRowInsertUpdateInfo("Job", "JobID", obj, sqlTransaction, isInsert: false);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobClosing, "JobID", "JobName", "Job", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		private GLData CreateJobClosingGLData(JobData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.JobTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["CustomerID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string value = dataRow["VoucherID"].ToString();
				string text3 = dataRow["JobID"].ToString();
				string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(Job.IncomeAccountID,LOC.ProjectIncomeAccountID) AS IncomeAccountID,LOC.DiscountGivenAccountID,\r\n                                ISNULL(Job.WIPAccountID,LOC.ProjectWIPAccountID) AS WIPAccountID, LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,ISNULL(Job.RetentionAccountID,LOC.ProjectRetentionAccountID) AS RetentionAccountID,\r\n                                ISNULL(Job.AdvanceAccountID,LOC.ProjectAdvanceAccountID) AS AdvanceAccountID ,\r\n                                ISNULL(Job.CostAccountID, LOC.ProjectCostAccountID) AS CostAccountID \r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                 LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Job ON Job.JobID = '" + text3 + "'\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string text4 = dataRow2["LocationID"].ToString();
				string value2 = dataRow2["ARAccountID"].ToString();
				dataRow2["BaseCurrencyID"].ToString();
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.JobClosing;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Project Closing - ";
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				new Hashtable();
				new ArrayList();
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				decimal num2 = default(decimal);
				textCommand = "SELECT CASE WHEN  P.IncomeAccountID IS NULL  THEN  Loc.ProjectIncomeAccountID ELSE P.IncomeAccountID END AS IncomeAccountID, \r\n\t                                CASE WHEN  P.WIPAccountID IS NULL  THEN  Loc.ProjectWIPAccountID ELSE P.WIPAccountID END AS WIPAccountID,\r\n                                    CASE WHEN  P.RetentionAccountID IS NULL  THEN  Loc.ProjectRetentionAccountID ELSE P.RetentionAccountID END AS RetentionAccountID,\r\n                                    CASE WHEN  P.CostAccountID IS NULL  THEN  Loc.ProjectCostAccountID ELSE P.CostAccountID END AS CostAccountID,\r\n                                    CASE WHEN  P.AdvanceAccountID IS NULL  THEN  Loc.ProjectAdvanceAccountID ELSE P.AdvanceAccountID END AS AdvanceAccountID\r\n                                    FROM Job P\r\n                                    LEFT OUTER JOIN Location LOC ON LOC.LocationID = '" + text4 + "'\r\n                                    WHERE JobID = '" + text3 + "'";
				dataSet = new DataSet();
				FillDataSet(dataSet, "Job", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Project accounts information not found for project or location.");
				}
				DataRow dataRow4 = dataSet.Tables[0].Rows[0];
				string text5 = dataRow4["CostAccountID"].ToString();
				string text6 = dataRow4["WIPAccountID"].ToString();
				if (text5 == "")
				{
					throw new CompanyException("Project Cost account is not selected for the project or location");
				}
				if (text6 == "")
				{
					throw new CompanyException("Project WIP account is not selected for the project or location");
				}
				DataRow dataRow5 = GetJobSummary(text3).Tables[0].Rows[0];
				string text7 = dataRow5["WIP"].ToString();
				dataRow5["Expense"].ToString();
				string value3 = dataRow5["Billed"].ToString();
				DataRow dataRow7;
				foreach (DataRow row in transactionData.JobTable.Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(text7, out result);
					string text8 = text5;
					dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = text5;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["SysDocID"] = row["SysDocID"];
					dataRow7["VoucherID"] = row["VoucherID"];
					dataRow7["Debit"] = text7;
					dataRow7["Credit"] = DBNull.Value;
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
					dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = text6;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["SysDocID"] = row["SysDocID"];
					dataRow7["VoucherID"] = row["VoucherID"];
					dataRow7["Debit"] = DBNull.Value;
					dataRow7["Credit"] = text7;
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
					if (hashtable.ContainsKey(text8))
					{
						num = decimal.Parse(hashtable[text8].ToString());
						num += Math.Round(result, currencyDecimalPoints);
						hashtable[text8] = num;
					}
					else
					{
						num = Math.Round(result, currencyDecimalPoints);
						hashtable.Add(text8, Math.Round(num, currencyDecimalPoints));
						arrayList.Add(text8);
					}
					num2 += Math.Round(result, currencyDecimalPoints);
				}
				dataRow7 = gLData.JournalDetailsTable.NewRow();
				dataRow7.BeginEdit();
				dataRow7["JournalID"] = 0;
				dataRow7["DueDate"] = dataRow["TransactionDate"];
				dataRow7["AccountID"] = value2;
				dataRow7["PayeeID"] = text;
				dataRow7["PayeeType"] = "C";
				dataRow7["IsARAP"] = true;
				dataRow7["SysDocID"] = text2;
				dataRow7["VoucherID"] = value;
				dataRow7["Debit"] = value3;
				dataRow7["Credit"] = DBNull.Value;
				dataRow7["Reference"] = dataRow["Reference"];
				dataRow7["Description"] = dataRow["Note"];
				dataRow7.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow7);
				dataRow7 = gLData.JournalDetailsTable.NewRow();
				dataRow7.BeginEdit();
				dataRow7["JournalID"] = 0;
				dataRow7["DueDate"] = dataRow["TransactionDate"];
				dataRow7["AccountID"] = value2;
				dataRow7["PayeeID"] = text;
				dataRow7["PayeeType"] = "C";
				dataRow7["IsARAP"] = true;
				dataRow7["SysDocID"] = text2;
				dataRow7["VoucherID"] = value;
				dataRow7["Debit"] = DBNull.Value;
				dataRow7["Credit"] = value3;
				dataRow7["Reference"] = dataRow["Reference"];
				dataRow7["Description"] = dataRow["Note"];
				dataRow7.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow7);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertJobFee(JobData jobData)
		{
			bool result = true;
			SqlCommand insertUpdateJobFeeCommand = GetInsertUpdateJobFeeCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateJobFeeCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(jobData, "Job_Fee", insertUpdateJobFeeCommand);
				string text = jobData.ProjectFeeTable.Rows[0]["FeeID"].ToString();
				AddActivityLog("Job Fee", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Fee", "FeeID", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateJobFee(JobData jobData)
		{
			bool flag = true;
			SqlCommand insertUpdateJobFeeCommand = GetInsertUpdateJobFeeCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateJobFeeCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(jobData, "Job_Fee", insertUpdateJobFeeCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = jobData.ProjectFeeTable.Rows[0]["FeeID"];
				UpdateTableRowByID("Job_Fee", "FeeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = jobData.ProjectFeeTable.Rows[0]["FeeName"].ToString();
				AddActivityLog("Job", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Fee", "FeeID", obj, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public JobData GetJob()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job");
			JobData jobData = new JobData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobData, "Job", sqlBuilder);
			return jobData;
		}

		public JobData GetJobFeeDetailsByID(string jobID)
		{
			try
			{
				JobData jobData = new JobData();
				string textCommand = "SELECT * FROM Job WHERE JobID='" + jobID + "'";
				FillDataSet(jobData, "Job", textCommand);
				textCommand = "SELECT * FROM Job_Fee_Detail WHERE JobID='" + jobID + "' ORDER BY RowIndex";
				FillDataSet(jobData, "Job_Fee_Detail", textCommand);
				textCommand = "SELECT * FROM Job_Fee_Payment_Term WHERE JobID='" + jobID + "' ORDER BY RowIndex";
				FillDataSet(jobData, "Job_FEE_PAYMENT_TERM", textCommand);
				return jobData;
			}
			catch
			{
				throw;
			}
		}

		public JobData GetJobBudgetDetailsByID(string jobID)
		{
			try
			{
				JobData jobData = new JobData();
				string textCommand = "SELECT * FROM Job WHERE JobID='" + jobID + "'";
				FillDataSet(jobData, "Job", textCommand);
				textCommand = "SELECT * FROM Job_Budget WHERE JobID='" + jobID + "'";
				FillDataSet(jobData, "Job_Budget", textCommand);
				return jobData;
			}
			catch
			{
				throw;
			}
		}

		public JobData GetJobEquipmentDetailsByID(string jobID)
		{
			try
			{
				JobData jobData = new JobData();
				string textCommand = "SELECT * FROM Job WHERE JobID='" + jobID + "'";
				FillDataSet(jobData, "Job", textCommand);
				textCommand = "SELECT * FROM Job_Equipment WHERE JobID='" + jobID + "'";
				FillDataSet(jobData, "Job_Equipment", textCommand);
				return jobData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteJob(string jobID)
		{
			bool flag = true;
			using (SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction())
			{
				try
				{
					flag &= DeleteJobFeeDetailsRows(jobID, sqlTransaction);
					flag &= DeleteJobFeePaymentTermRows(jobID, sqlTransaction);
					flag &= DeleteJobBudgetDetailsRows(jobID, sqlTransaction);
					flag &= DeleteJobVehicleDetailsRows(jobID, sqlTransaction);
					string commandText = "DELETE FROM Job WHERE JobID = '" + jobID + "'";
					flag &= Delete(commandText, sqlTransaction);
					if (!flag)
					{
						return flag;
					}
					AddActivityLog("Job", jobID, ActivityTypes.Delete, null);
					return flag;
				}
				catch
				{
					flag = false;
					throw;
				}
				finally
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		public bool DeleteJobFee(string feeID)
		{
			bool flag = true;
			using (SqlTransaction trans = base.DBConfig.StartNewTransaction())
			{
				try
				{
					string commandText = "DELETE FROM Job_Fee WHERE FeeID = '" + feeID + "'";
					flag &= Delete(commandText, trans);
					if (!flag)
					{
						return flag;
					}
					AddActivityLog("Job Fee", feeID, ActivityTypes.Delete, null);
					return flag;
				}
				catch
				{
					flag = false;
					throw;
				}
				finally
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		internal bool DeleteJobFeeDetailsRows(string jobCode, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Fee_Detail WHERE JobID = '" + jobCode + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobFeePaymentTermRows(string jobCode, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_FEE_PAYMENT_TERM WHERE JobID = '" + jobCode + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobBudgetDetailsRows(string jobCode, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Budget WHERE JobID = '" + jobCode + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobEquipmentDetailsRows(string jobCode, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Equipment WHERE JobID = '" + jobCode + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobVehicleDetailsRows(string jobCode, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Vehicle_Detail WHERE JobID = '" + jobCode + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public JobData GetJobByID(string id)
		{
			JobData jobData = new JobData();
			string textCommand = "select * from Job where Job.JobID='" + id + "'";
			FillDataSet(jobData, "Job", textCommand);
			if (jobData == null || jobData.Tables.Count == 0 || jobData.Tables[0].Rows.Count == 0)
			{
				return jobData;
			}
			textCommand = "select * from Job_Vehicle_Detail where JobID='" + id + "'";
			FillDataSet(jobData, "Job_Vehicle_Detail", textCommand);
			textCommand = "SELECT * FROM UDF_Job WHERE EntityID = '" + id + "'";
			FillDataSet(jobData, "UDF", textCommand);
			jobData.Merge(GetJobSummary(id));
			return jobData;
		}

		public JobData GetJobFeeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "FeeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Job_Fee";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			JobData jobData = new JobData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobData, "Job_Fee", sqlBuilder);
			return jobData;
		}

		public DataSet GetJobByFields(params string[] columns)
		{
			return GetJobByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobByFields(string[] jobID, params string[] columns)
		{
			return GetJobByFields(jobID, isInactive: true, columns);
		}

		public DataSet GetJobByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "JobID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Job";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Job", sqlBuilder);
			return dataSet;
		}

		public DataSet GetJobList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT J.JobID [Job Code],J.JobName [Job Name], J.CustomerID,C.Customername,J.JobTypeID,\r\n                                J.StartDate [Start Date], J.EndDate [End Date], CASE Status WHEN 0 THEN 'Estimate' \r\n                                WHEN 1 THEN 'Open' WHEN 2 THEN 'On Hold' WHEN 3 THEN 'Completed' WHEN 4 THEN 'Closed' END AS Status, J.Note\r\n                                FROM Job J LEFT JOIN Customer C ON C.CustomerID=J.CustomerID\r\n                                ";
			FillDataSet(dataSet, "Job", textCommand);
			return dataSet;
		}

		public DataSet GetClosedJobList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT J.JobID [Job Code],J.JobName [Job Name], J.CustomerID,C.Customername,J.JobTypeID,\r\n                                J.StartDate [Start Date], J.EndDate [End Date], CASE Status WHEN 0 THEN 'Estimate' \r\n                                WHEN 1 THEN 'Open' WHEN 2 THEN 'On Hold' WHEN 3 THEN 'Completed' WHEN 4 THEN 'Closed' END AS Status, J.Note\r\n                                FROM Job J LEFT JOIN Customer C ON C.CustomerID=J.CustomerID where Status=4\r\n                                ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND EndDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsInactive,'False')='False' ";
			}
			FillDataSet(dataSet, "Job", text3);
			return dataSet;
		}

		public DataSet GetJobComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT JobID [Code],JobName [Name],JOB.CustomerID[CustomerID],CUS.CustomerName[CustomerName],AdvanceItemID,ISNULL(RetentionPercent,0.0)as RetentionPercent,PONumber\r\n\t\t\t\t\t\t   FROM Job INNER JOIN Customer CUS ON CUS.CustomerID=Job.CustomerID WHERE job.Inactive IS NUll or job.Inactive<>1 ORDER BY JobID,JobName";
			FillDataSet(dataSet, "Job", textCommand);
			return dataSet;
		}

		public DataSet GetJobFeeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FeeID [Fee Code],FeeName [Fee Name], \r\n\t\t\t\t\t\t\tStartDate [Start Date], EndDate [End Date], CASE FeeType WHEN 1 THEN 'Project Fee' \r\n\t\t\t\t\t\t\t\tWHEN 2 THEN 'Service' END [Fee Type], Note\r\n\t\t\t\t\t\t   FROM Job_Fee";
			FillDataSet(dataSet, "Job_Fee", textCommand);
			return dataSet;
		}

		public DataSet GetJobFeeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FeeID [Code], FeeName [Name],FeeType\r\n\t\t\t\t\t\t   FROM Job_Fee ORDER BY FeeID,FeeName";
			FillDataSet(dataSet, "Job_Fee", textCommand);
			return dataSet;
		}

		public DataSet GetJobSummary(string jobID)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Job_Summary");
			dataTable.Columns.Add("Expense", typeof(decimal));
			dataTable.Columns.Add("WIP", typeof(decimal));
			dataTable.Columns.Add("Billed", typeof(decimal));
			DataRow dataRow = dataTable.NewRow();
			string text = "";
			text = "SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) AS Expense FROM Journal_Details  JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n\t\t\t\t\t\t\t\tINNER JOIN Account_Group AG ON AC.GroupID = AG.GroupID\r\n\t\t\t\t\t\t\t\tWHERE JobID = '" + jobID + "' AND AG.TypeID = 4";
			object obj = ExecuteScalar(text);
			if (obj != null && obj.ToString() != "")
			{
				dataRow["Expense"] = decimal.Parse(obj.ToString());
			}
			else
			{
				dataRow["Expense"] = 0;
			}
			text = "SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) AS WIP FROM Journal_Details  JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n\t\t\t\t\t\tWHERE JobID = '" + jobID + "' AND AC.SubType = 9";
			obj = ExecuteScalar(text);
			if (obj != null && obj.ToString() != "")
			{
				dataRow["WIP"] = decimal.Parse(obj.ToString());
			}
			else
			{
				dataRow["WIP"] = 0;
			}
			text = "SELECT SUM(Amount) AS BilledAmount FROM Job_Invoice_Detail JID INNER JOIN Job_Invoice JI ON JID.SysDocID = JI.SysDocID AND JID.VoucherID = JI.VoucherID\r\n\t\t\t\t\t\tWHERE JI.JobID = '" + jobID + "' AND ItemType IN (1)";
			obj = ExecuteScalar(text);
			if (obj != null && obj.ToString() != "")
			{
				dataRow["Billed"] = decimal.Parse(obj.ToString());
			}
			else
			{
				dataRow["Billed"] = 0;
			}
			dataTable.Rows.Add(dataRow);
			return dataSet;
		}

		public DataSet GetJobSummaryReport(DateTime fromDate, DateTime toDate, string jobID)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT T.*,ISNULL(T.[OTH Expense],0)+ISNULL(T.[INV Expense],0) AS [Total Expense]  FROM (\r\n                            SELECT J.JobID,J.JobName,C.CustomerID,C.CustomerName,J.StartDate,J.EndDate,J.ProjectAmount,\r\n                            J.AdvanceAmount,J.RetentionPercent,CompletedPercent AS [Project Completed],\r\n                            (SELECT SUM(JFD.Amount) FROM Job_Fee_Detail JFD WHERE JFD.JobID=j.JobID  GROUP BY JFD.JobID) AS Fee,\r\n                            (SELECT SUM(JB.TotalCost) FROM Job_Budget JB WHERE JB.JobID=j.JobID  GROUP BY JB.JobID) AS Budget,\r\n                            (SELECT (SO.Total-ISNULL(SO.Discount,0)) FROM Sales_Order SO WHERE SO.JobID=J.JobID) AS [Order Value],\r\n                            ( SELECT TOP 1 SO.TransactionDate FROM Sales_Order SO WHERE SO.JobID = J.JobID ) AS [OrderDate] , \r\n                            ( SELECT TOP 1 SI.TransactionDate FROM Sales_Invoice SI WHERE SI.JobID = J.JobID ORDER BY SI.TransactionDate DESC) AS [Inv_Date] , \r\n                            ( SELECT sum ( JMED.Quantity * isnull ( UnitPrice , 0 ) ) AS [EST_Material] FROM Job_Material_Estimate JME INNER JOIN Job_Material_Estimate_Detail JMED \r\n                            ON JME.SysDocID = JMED.SysDocID AND JME.VoucherID = JMED.VoucherID \r\n                            WHERE JME.JobID = J.JobID \r\n                            GROUP BY JME.JobID ) AS [Material],J.LaborAmount,J.OverHeadAmount,J.MiscellaneousAmount,J.Profit,\r\n                            (SELECT SUM(-1*IT.AssetValue) AS [Issued Value]\r\n                            FROM Inventory_Transactions IT \r\n                            WHERE IT.Quantity < 0 AND  SysDocType IN ('67','71','24') AND IT.JobID=J.JobID) AS [ACT_Material],\r\n                            (SELECT SUM(T.Amount) AS [ACT_Manpower] FROM (SELECT (OTD.Hours-OTD.OTHours)*(SELECT (SUM(Amount)/270) FROM Employee_PayrollItem_Detail EPD WHERE EPD.EmployeeID=OTD.EmployeeID)+\r\n                            (OTD.OTHours)*(SELECT (SUM(Amount)/270) FROM Employee_PayrollItem_Detail EPD WHERE EPD.EmployeeID=OTD.EmployeeID AND EPD.PayrollItemID \r\n                            IN (SELECT PayrollItemID FROM PayrollItem WHERE InOvertime=1)) AS 'Amount'\r\n                             FROM OverTimeEntry_Detail OTD \r\n                            WHERE 1=1 AND OTD.JobID= J.JobID) T) AS [ManPower],\r\n                            (SELECT SUM(ISNULL(JD.Debit,0)-ISNULL(JD.Credit,0)) AS EXPENSE FROM Journal J LEFT JOIN\r\n                            Journal_Details JD ON J.JournalID=JD.JournalID LEFT JOIN Account A ON JD.AccountID=A.AccountID INNER JOIN Account_Group AG ON \r\n                            A.GroupID = AG.GroupID WHERE (AG.TypeID = 4 AND A.SubType <> 9) AND JD.JobID='" + jobID + "' AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' GROUP BY JD.JobID) AS  [OTH Expense],\r\n\r\n                                (SELECT SUM(ISNULL(JD.Credit,0)-ISNULL(JD.Debit,0)) AS Value\r\n                                FROM Journal J LEFT JOIN Journal_Details JD \r\n                                ON J.JournalID=JD.JournalID LEFT JOIN Account A ON JD.AccountID=A.AccountID\r\n                                INNER JOIN Account_Group AG ON A.GroupID = AG.GroupID WHERE (AG.TypeID = 3 AND A.SubType = 7) \r\n                                AND JD.JobID BETWEEN '" + jobID + "' AND '" + jobID + " '   And   J.JournalDate Between '" + text + "' AND '" + text2 + "' \r\n                                GROUP BY JD.JobID)[Invoiced Amount],\r\n                            (SELECT SUM(T.Amount) AS Value FROM \r\n                            (SELECT JIID.SysDocID, JIID.VoucherID, JIID.ProductID, JIID.JobID, JIID.LocationID, JIID.CostCategoryID, JIID.Description, JIID.Quantity, IT.AverageCost AS Cost, JIID.Quantity * IT.AverageCost AS Amount, \r\n                            Product.CategoryID, JobName, CostCategoryName, IT.TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIID.SysDocID AND IT.VoucherID = JIID.VoucherID) AS SysDocType\r\n                            FROM Job_Inventory_Issue_Detail AS JIID \r\n                            INNER JOIN Inventory_Transactions IT ON JIID.SysDocID=IT.SysDocID AND JIID.VoucherID=IT.VoucherID AND JIID.ProductID=IT.ProductID\r\n                            INNER JOIN Job_Inventory_Issue JII ON JIID.SysDocID = JII.SysDocID AND JIID.VoucherID = JII.VoucherID \r\n                            INNER JOIN Product ON JIID.ProductID = Product.ProductID\r\n                            LEFT OUTER JOIN Job J ON JIID.JobID = J.JobID\r\n                            LEFT OUTER JOIN Job_Cost_Category JCC ON JIID.CostCategoryID = JCC.CostCategoryID\r\n                            WHERE JIID.JobID <> NULL OR JIID.JobID <> ''   \r\n                            AND JIID.JobID BETWEEN '" + jobID + "' AND '" + jobID + " ' AND IT.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                        \r\n                            UNION SELECT  \r\n                            JIRD.SysDocID, JIRD.VoucherID, JIRD.ProductID, JIRD.JobID, JIRD.LocationID, JIRD.CostCategoryID, JIRD.Description, (JIRD.Quantity ) * -1 , \r\n                            JIRD.Cost AS Cost, (JIRD.Quantity * JIRD.Cost )*-1 AS Amount\r\n                            , Product.CategoryID, JobName, CostCategoryName, TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIRD.SysDocID AND IT.VoucherID = JIRD.VoucherID) AS SysDocType    \r\n                            FROM Job_Inventory_Return_Detail AS JIRD \r\n                            INNER JOIN Job_Inventory_Return JIR ON JIRD.SysDocID = JIR.SysDocID AND JIRD.VoucherID = JIR.VoucherID   \r\n                            INNER JOIN Product ON JIRD.ProductID = Product.ProductID\r\n                            LEFT OUTER JOIN Job J ON JIRD.JobID = J.JobID\r\n                            LEFT OUTER JOIN Job_Cost_Category JCC ON JIRD.CostCategoryID = JCC.CostCategoryID\r\n                            WHERE JIRD.JobID <> NULL OR JIRD.JobID <> ''  \r\n                            AND JIRD.JobID BETWEEN '" + jobID + "' AND '" + jobID + "' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "') AS T)  AS [INV Expense]\r\n                            FROM JOB J LEFT JOIN Customer C ON J.CustomerID=C.CustomerID  WHERE J.JobID='" + jobID + "' ) T";
				FillDataSet(dataSet, "Job", textCommand);
				textCommand = "SELECT * FROM Job_Fee_Detail JFD WHERE JFD.JobID='" + jobID + "'";
				FillDataSet(dataSet, "Job_Fee_Detail", textCommand);
				textCommand = "SELECT SUM(ISNULL(JD.Debit,0)-ISNULL(JD.Credit,0)) AS EXPENSE,A.AccountID,A.AccountName,JD.JobID FROM Journal J LEFT JOIN\r\n                                        Journal_Details JD ON J.JournalID=JD.JournalID LEFT JOIN Account A ON JD.AccountID=A.AccountID INNER JOIN Account_Group AG ON \r\n                                        A.GroupID = AG.GroupID WHERE (AG.TypeID = 4 AND A.SubType <> 9) AND JD.JobID='" + jobID + "' ";
				if (fromDate != DateTime.MinValue)
				{
					textCommand = textCommand + "And   J.JournalDate Between '" + text + "' AND '" + text2 + "'";
				}
				textCommand += "GROUP BY A.AccountID,A.AccountName,JD.JobID";
				FillDataSet(dataSet, "Job_Expense", textCommand);
				textCommand = "SELECT T.JobID,ISNULL(T.CategoryID,'Non Category') AS CategoryName,ISNULL(T.CategoryName,'Non Category') AS CGName,SUM(T.Amount) AS Value FROM \r\n                                    (SELECT JIID.SysDocID, JIID.VoucherID, JIID.ProductID, JIID.JobID, JIID.LocationID, JIID.CostCategoryID, JIID.Description, JIID.Quantity, IT.AverageCost AS Cost, JIID.Quantity * IT.AverageCost AS Amount, \r\n                                    Product.CategoryID,PC.CategoryName, JobName, CostCategoryName, IT.TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIID.SysDocID AND IT.VoucherID = JIID.VoucherID) AS SysDocType\r\n                                    FROM Job_Inventory_Issue_Detail AS JIID \r\n                                    INNER JOIN Inventory_Transactions IT ON JIID.SysDocID=IT.SysDocID AND JIID.VoucherID=IT.VoucherID AND JIID.ProductID=IT.ProductID\r\n                                    INNER JOIN Job_Inventory_Issue JII ON JIID.SysDocID = JII.SysDocID AND JIID.VoucherID = JII.VoucherID \r\n                                    INNER JOIN Product ON JIID.ProductID = Product.ProductID\r\n                                    LEFT OUTER JOIN Product_Category PC ON Product.CategoryID=PC.CategoryID\r\n                                    LEFT OUTER JOIN Job J ON JIID.JobID = J.JobID\r\n                                    LEFT OUTER JOIN Job_Cost_Category JCC ON JIID.CostCategoryID = JCC.CostCategoryID\r\n                                    WHERE JIID.JobID <> NULL OR JIID.JobID <> ''   \r\n                                    AND JIID.JobID BETWEEN '" + jobID + "' AND '" + jobID + "' AND IT.TransactionDate Between '" + text + "' AND '" + text2 + "' UNION SELECT  \r\n                                    JIRD.SysDocID, JIRD.VoucherID, JIRD.ProductID, JIRD.JobID, JIRD.LocationID, JIRD.CostCategoryID, JIRD.Description, (JIRD.Quantity ) * -1 , \r\n                                    JIRD.Cost AS Cost, (JIRD.Quantity * JIRD.Cost )*-1 AS Amount\r\n                                    , Product.CategoryID,PC.CategoryName, JobName, CostCategoryName, TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIRD.SysDocID AND IT.VoucherID = JIRD.VoucherID) AS SysDocType    \r\n                                    FROM Job_Inventory_Return_Detail AS JIRD \r\n                                    INNER JOIN Job_Inventory_Return JIR ON JIRD.SysDocID = JIR.SysDocID AND JIRD.VoucherID = JIR.VoucherID   \r\n                                    INNER JOIN Product ON JIRD.ProductID = Product.ProductID\r\n                                    LEFT OUTER JOIN Product_Category PC ON Product.CategoryID=PC.CategoryID\r\n                                    LEFT OUTER JOIN Job J ON JIRD.JobID = J.JobID\r\n                                    LEFT OUTER JOIN Job_Cost_Category JCC ON JIRD.CostCategoryID = JCC.CostCategoryID\r\n                                    WHERE JIRD.JobID <> NULL OR JIRD.JobID <> ''  \r\n                                     AND JIRD.JobID BETWEEN '" + jobID + "' AND '" + jobID + "' AND TransactionDate Between '" + text + "' AND '" + text2 + "') AS T ";
				textCommand += "GROUP BY T.JobID,T.CategoryID,T.CategoryName";
				FillDataSet(dataSet, "Job_Inventory", textCommand);
				textCommand = "SELECT SUM(ISNULL(JD.Credit,0)-ISNULL(JD.Debit,0)) AS Value,J.JournalDate,J.VoucherID,J.Note,JD.JobID,\r\n                                    (select PayeeTaxGroupID from Job_Invoice where SysDocID=J.SysDocID AND  VoucherID=J.VoucherID) AS PayeeTAxGroupID,\r\n                                    (select  TaxAmount from Job_Invoice where  SysDocID=J.SysDocID AND  VoucherID=J.VoucherID) AS TaxAmount\r\n                                    FROM Journal J LEFT JOIN Journal_Details JD \r\n                                    ON J.JournalID=JD.JournalID LEFT JOIN Account A ON JD.AccountID=A.AccountID\r\n                                    INNER JOIN Account_Group AG ON A.GroupID = AG.GroupID WHERE (AG.TypeID = 3 AND A.SubType = 7) AND JD.JobID=  '" + jobID + "'";
				if (fromDate != DateTime.MinValue)
				{
					textCommand = textCommand + "And   J.JournalDate Between '" + text + "' AND '" + text2 + "'";
				}
				textCommand += " GROUP BY J.JournalDate,J.VoucherID,J.Note,JD.JobID, J.SysDocID, J.VoucherID";
				FillDataSet(dataSet, "Job_Invoice", textCommand);
				textCommand = "SELECT (ISNULL(JD.Credit,0)) AS Amount,J.Note,J.JournalDate,J.VoucherID,JD.JobID FROM Journal J \r\n                                    LEFT JOIN Journal_Details JD ON J.JournalID=JD.JournalID LEFT JOIN Account A ON JD.AccountID=A.AccountID\r\n                                    INNER JOIN Account_Group AG ON A.GroupID = AG.GroupID WHERE ( A.SubType=4) AND JD.Credit IS NOT NULL AND JD.JobID='" + jobID + "' And  J.JournalDate Between '" + text + "' AND '" + text2 + "'";
				textCommand = textCommand + "UNION SELECT ISNULL(TD.AmountFC,TD.Amount) AS Amount,TD.Description,CR.ReceiptDate,TD.VoucherID,TD.JobID  FROM Cheque_Received CR \r\n                               LEFT JOIN Transaction_Details TD  ON CR.SysDocID=TD.SysDocID AND CR.VoucherID=TD.VoucherID  WHERE TD.JobID='" + jobID + "' And   CR.ReceiptDate Between '" + text + "' AND '" + text2 + "'";
				FillDataSet(dataSet, "Job_Other Income", textCommand);
				textCommand = "SELECT IT.ProductID,P.Description,IT.JobID,SUM(-1*IT.Quantity) AS [Qty] , SUM(-1*IT.AssetValue) AS [Act_Amount]\r\n                                        FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID=IT.ProductID\r\n                                        WHERE IT.Quantity < 0 AND  SysDocType IN ('67','71','24') AND IT.JobID='" + jobID + "'   AND P.ItemType=1 AND  IT.TransactionDate Between '" + text + "' AND '" + text2 + "' \r\n                                        GROUP BY IT.ProductID,P.Description,IT.JobID";
				FillDataSet(dataSet, "Inventory_Detail", textCommand);
				dataSet.Relations.Add("Job_Fee_Detail", new DataColumn[1]
				{
					dataSet.Tables["Job"].Columns["JobID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Job_Fee_Detail"].Columns["JobID"]
				}, createConstraints: false);
				dataSet.Relations.Add("Job_Expense", new DataColumn[1]
				{
					dataSet.Tables["Job"].Columns["JobID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Job_Expense"].Columns["JobID"]
				}, createConstraints: false);
				dataSet.Relations.Add("Job_Inventory", new DataColumn[1]
				{
					dataSet.Tables["Job"].Columns["JobID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Job_Inventory"].Columns["JobID"]
				}, createConstraints: false);
				dataSet.Relations.Add("Job_Invoice", new DataColumn[1]
				{
					dataSet.Tables["Job"].Columns["JobID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Job_Invoice"].Columns["JobID"]
				}, createConstraints: false);
				dataSet.Relations.Add("Job_Other Income", new DataColumn[1]
				{
					dataSet.Tables["Job"].Columns["JobID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Job_Other Income"].Columns["JobID"]
				}, createConstraints: false);
				dataSet.Relations.Add("Inventory_Rel", new DataColumn[1]
				{
					dataSet.Tables["Job"].Columns["JobID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Inventory_Detail"].Columns["JobID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBillableItems(string jobID, bool includeInventory, bool includeTimesheet, bool includeExpense)
		{
			DataSet dataSet = new DataSet();
			string text = null;
			string text2 = null;
			string text3 = "";
			text3 = "SELECT 5 AS RowType, JD.SysDocID ,VoucherID,JournalDetailID AS RowIndex,AccountID,Description,JobID,CostCategoryID,Debit AS Amount FROM Journal_Details JD\r\n                        INNER JOIN System_Document SD ON SD.SysDocID= JD.SysDocID    WHERE ISNULL(IsBilled,'False')='False' AND SD. SysDocType NOT IN ('74') AND Debit IS NOT NULL AND JOBID = '" + jobID + "' ";
			if (includeInventory)
			{
				text = "71";
			}
			else
			{
				text2 = "71";
			}
			if (!includeTimesheet || text == null)
			{
				text2 = ((includeTimesheet || text2 == null) ? "76" : (text2 + ",76"));
			}
			else
			{
				text += ",76";
			}
			if (!includeExpense || text == null)
			{
				text2 = ((includeExpense || text2 == null) ? "73,4,5" : (text2 + ",73,4,5"));
			}
			else
			{
				text += ",73,4,5";
			}
			if (text != null)
			{
				text3 = text3 + "AND SD.SysDocType IN (" + text + ")";
			}
			if (text2 != null)
			{
				text3 = text3 + "AND SD.SysDocType NOT IN (" + text2 + ")";
			}
			FillDataSet(dataSet, "Job", text3);
			return dataSet;
		}

		internal string GetJobAccountIDByLocation(string jobID, string locationID, JobAccounts accountType, SqlTransaction sqlTransaction)
		{
			string text = "";
			string text2 = "";
			switch (accountType)
			{
			case JobAccounts.WIPAccount:
				text2 = "ProjectWIPAccountID";
				break;
			case JobAccounts.CostAccount:
				text2 = "ProjectCostAccountID";
				break;
			case JobAccounts.IncomeAccount:
				text2 = "ProjectIncomeAccountID";
				break;
			case JobAccounts.TimesheetAccount:
				text2 = "ProjectTimesheetContraAccountID";
				break;
			case JobAccounts.Retention:
				text2 = "ProjectRetentionAccountID";
				break;
			case JobAccounts.Advance:
				text2 = "ProjectAdvanceAccountID";
				break;
			}
			string text3 = "";
			switch (accountType)
			{
			case JobAccounts.WIPAccount:
				text3 = "WIPAccountID";
				break;
			case JobAccounts.CostAccount:
				text3 = "CostAccountID";
				break;
			case JobAccounts.IncomeAccount:
				text3 = "IncomeAccountID";
				break;
			case JobAccounts.TimesheetAccount:
				text3 = "TimesheetContraAccountID";
				break;
			default:
				switch (accountType)
				{
				case JobAccounts.TimesheetAccount:
					text3 = "RetentionAccountID";
					break;
				case JobAccounts.Advance:
					text3 = "AdvanceAccountID";
					break;
				}
				break;
			}
			text = "SELECT ISNULL(" + text3 + ", (SELECT " + text2 + " FROM Location WHERE LocationID = '" + locationID + "')) FROM JOB  WHERE JOBID='" + jobID + "'";
			object obj = ExecuteScalar(text);
			if (obj != null && obj.ToString() != "")
			{
				return obj.ToString();
			}
			return null;
		}

		public DataSet GetProjectFeesByID(string jobID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = text + "SELECT JobID,FeeID,Description,RowIndex,Amount,0 AS Due,\r\n\t\t\t\t\t\t\tISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Job_Invoice_Detail JID INNER JOIN Job_Invoice JI\r\n\t\t\t\t\t\t\tON JID.SysDocID = JI.SysDocID AND JID.VoucherID = JI.VoucherID WHERE ISNULL(JI.IsVoid,0)=0 and JobID = '" + jobID + "' AND FeeID = JFD.FeeID),0) AS Billed\r\n\t\t\t\t\t\t\tFROM Job_Fee_Detail JFD \r\n\t\t\t\t\t\t\tWHERE JobID = '" + jobID + "'";
			FillDataSet(dataSet, "Job_Fee_Detail", text);
			return dataSet;
		}

		public DataSet GetProjectVehicleByID(string jobID)
		{
			DataSet dataSet = new DataSet();
			string str = "";
			str = str + "SELECT *\r\n\t\t\t\t\t\t\tFROM Job_Vehicle_Detail JFD \r\n\t\t\t\t\t\t\tWHERE JobID = '" + jobID + "'";
			FillDataSet(dataSet, "Job_Vehicle_Detail", str);
			return dataSet;
		}

		public DataSet GetProjectFeesComboByJob(string jobID)
		{
			DataSet dataSet = new DataSet();
			string str = "";
			str = str + "SELECT  FeeID AS Code,Description AS [Name], CustomerID\r\n\t\t\t\t\t\t\tFROM Job_Fee_Detail JFD \r\n\t\t\t\t\t\t\tWHERE JobID = '" + jobID + "'";
			FillDataSet(dataSet, "Job_Fee_Detail", str);
			return dataSet;
		}

		public DataSet GetJobBudgetVsActualReport(string jobID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT * FROM Job WHERE JobID = '" + jobID + "'";
				FillDataSet(dataSet, "Job", textCommand);
				textCommand = "SELECT * INTO TempJob001 FROM (\r\n                            SELECT JobID,CostCategoryID,TotalCost AS BudgetedCost, 0 AS ActualCost FROM Job_Budget\r\n                            UNION\r\n                            SELECT JobID,ISNULL(CostCategoryID,'None') AS CostCategoryID ,0 AS BudgetedCost,SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) AS ActualCost FROM Journal_Details  JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n\t\t\t\t\t\t\t\t                            INNER JOIN Account_Group AG ON AC.GroupID = AG.GroupID\r\n\t\t\t\t\t\t\t\t                            WHERE JobID = '" + jobID + "' AND AG.TypeID = 4\r\n\t\t\t\t\t\t\t\t                            GROUP BY JobID, CostCategoryID\r\n                            UNION\r\n                            SELECT JobID,ISNULL(CostCategoryID,'None') AS CostCategoryID,0 AS BudgetedCost, SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) AS ActualCost FROM Journal_Details  JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n\t\t\t\t\t\t    WHERE JobID = '" + jobID + "' AND AC.SubType = 9\r\n\t\t\t\t\t\t    GROUP BY JobID, CostCategoryID) AS Temp002\r\n\r\n\t\t\t\t\t\t   SELECT JobID,J.CostCategoryID,CC.CostCategoryName,SUM(BudgetedCost) AS Budget,Sum(ActualCost) AS Actual FROM TempJob001 J\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Job_Cost_Category CC ON J.CostCategoryID = Cc.CostCategoryID\r\n\t\t\t\t\t\t    GROUP BY JobID, J.CostCategoryID,CostCategoryName\r\n\r\n\t\t\t\t\t\t    DROP Table TempJob001";
				FillDataSet(dataSet, "Budget", textCommand);
				dataSet.Relations.Add("Rel", dataSet.Tables["Job"].Columns["JobID"], dataSet.Tables["Budget"].Columns["JobID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProjectStatusReport(string fromJob, string toJob, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			DataSet dataSet = new DataSet();
			string str = "";
			str += "SELECT JobID, JobName, ProjectAmount, (SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) FROM \r\n\t\t\t\t\t\t\tJournal_Details  JD\r\n\t\t\t\t\t\t\tINNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n\t\t\t\t\t\t\tINNER JOIN Account_Group AG ON AC.GroupID = AG.GroupID\r\n\t\t\t\t\t\t\tWHERE JobID = J.JobID AND AG.TypeID = 4) Expense,\r\n\t\t\t\t\t    (SELECT SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) AS WIP FROM Journal_Details  JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n\t\t\t\t\t\tWHERE JobID = J.JobID AND AC.SubType = 9) WIP,\r\n\t\t\t\t\t\t(SELECT SUM(Amount) AS BilledAmount FROM Job_Invoice_Detail JID INNER JOIN Job_Invoice JI ON JID.SysDocID = JI.SysDocID AND JID.VoucherID = JI.VoucherID\r\n\t\t\t\t\t\tWHERE JI.JobID = J.JobID AND ItemType IN (1)) Billed\r\n                        From Job J WHERE 1=1 ";
			if (fromJob != "")
			{
				str = str + "AND J.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "'";
			}
			if (customerIDs != "")
			{
				str = str + " AND J.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				str = str + " AND J.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				str = str + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				str = str + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromArea != "")
			{
				str = str + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				str = str + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			FillDataSet(dataSet, "Job", str);
			return dataSet;
		}

		public DataSet GetProjectMaterialVarianceReport(string fromJob, string toJob, DateTime asOfDate)
		{
			string str = StoreConfiguration.ToSqlDateTimeString(asOfDate);
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT MV.ProductID,P.Description, JobID, ISNULL(SUM(MV.Quantity), 0) ActualQuantity , ISNULL(SUM(IssueQty), 0) IssueQuantity ,\r\n                                ISNULL(SUM(MV.ActualValue), 0) ActualValue , ISNULL(SUM(MV.[Issued Value]), 0) IssuedValue \r\n                                FROM (\r\n                                SELECT JMED.SysDocID,JMED.VoucherID, JMED.ProductID, JME.JobID, JMED.Description,\r\n                                JMED.Quantity, 0 AS IssueQty,JMED.UnitPrice*JMED.Quantity AS [ActualValue],0 AS [Issued Value], JME.TransactionDate\r\n                                FROM Job_Material_Estimate_Detail JMED \r\n                                INNER JOIN Job_Material_Estimate JME ON JMED.SysDocID = JME.SysDocID AND\r\n                                JMED.VoucherID = JME.VoucherID\r\n\r\n                                UNION ALL\r\n\r\n                                SELECT IT.SysDocID,IT.VoucherID, IT.ProductID, IT.JobID, P.Description, 0 AS Quantity, \r\n                                -1*IT.Quantity AS IssueQty,0 AS [ActualValue],-1*IT.AssetValue AS [Issued Value], IT.TransactionDate\r\n                                FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID=IT.ProductID\r\n                                WHERE IT.Quantity < 0 AND  SysDocType IN ('67','71','24') AND P.ItemType=1\r\n                                ) as MV INNER JOIN Product P ON P.ProductID=MV.ProductID\r\n                                WHERE 1 = 1  ";
				if (fromJob != "")
				{
					text = text + " AND JobID Between '" + fromJob + "' AND '" + toJob + "'";
				}
				text = text + " AND TransactionDate <='" + str + "'";
				text += " GROUP BY MV.ProductID, JobID,P.Description";
				new SqlCommand(text);
				FillDataSet(dataSet, "Job", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProjectDueReport(string fromJob, string toJob, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime fromDate, DateTime toDate, string jobType, string customerIDs)
		{
			string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
			try
			{
				DataSet dataSet = new DataSet();
				string text3 = "SELECT J.*,C.CustomerName FROM Job J LEFT JOIN Customer C ON J.CustomerID=C.CustomerID WHERE 1=1";
				if (fromJob != "" && toJob != "")
				{
					text3 = text3 + " AND JobID Between '" + fromJob + "' AND '" + toJob + "'";
				}
				if (jobType != "")
				{
					text3 = text3 + " AND J.JobTypeID='" + jobType + "'";
				}
				if (customerIDs != "")
				{
					text3 = text3 + " AND J.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text3 = text3 + " AND J.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
				}
				if (fromCustomerGroup != "")
				{
					text3 = text3 + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
				}
				if (fromArea != "")
				{
					text3 = text3 + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
				}
				if (fromCountry != "")
				{
					text3 = text3 + " AND J.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
				}
				FillDataSet(dataSet, "Job", text3);
				text3 = "SELECT * FROM  Job_Fee_Payment_Term JPT WHERE JPT.DueDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (fromJob != "")
				{
					text3 = text3 + " AND JobID Between '" + fromJob + "' AND '" + toJob + "'";
				}
				new SqlCommand(text3);
				FillDataSet(dataSet, "Job_FEE_PAYMENT_TERM", text3);
				dataSet.Relations.Add("JobRel", dataSet.Tables["Job"].Columns["JobID"], dataSet.Tables["Job_FEE_PAYMENT_TERM"].Columns["JobID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetServiceCallTrackReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromJob, string toJob, string customerIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT JM.*,'' AS StrRepairDetails,C.CustomerName,E.FirstName+' '+E.LastName AS [EmployeeName]  FROM Service_CallTrack JM LEFT JOIN Customer C ON JM.CustomerID=C.CustomerID\r\n                        LEFT JOIN Employee E ON JM.ServiceEmployeeID=E.EmployeeID where ";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + "  JM.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (fromEmployee != "")
			{
				text3 = text3 + " AND E.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND E.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND E.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (customerIDs != "")
			{
				text3 = text3 + " AND JM.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND JM.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND JM.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND JM.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromArea != "")
			{
				text3 = text3 + " AND JM.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text3 = text3 + " AND JM.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromJob != "")
			{
				text3 = text3 + " AND JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			text3 += " ORDER BY JM.CustomerID ";
			FillDataSet(dataSet, "Service_CallTrack", text3);
			DataSet dataSet2 = new DataSet();
			text3 = " select SCD.*, CA.ClientAssetName\r\n        FROM            Service_CallTrack AS SC INNER JOIN\r\n                         Service_ClientAsset_Detail AS SCD ON SC.SysDocID = SCD.SysDocID  AND SC.VoucherID = SCD.VoucherID LEFT JOIN\r\n                         ClientAsset AS CA ON SCD.ClientAssetID = CA.ClientAssetID WHERE ";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + "  SC.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (fromEmployee != "")
			{
				text3 = text3 + " AND SC.ServiceEmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND SC.ServiceEmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND SC.ServiceEmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND SC.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromArea != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromJob != "")
			{
				text3 = text3 + " AND SC.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			text3 += " ORDER BY SC.TransactionDate ";
			FillDataSet(dataSet2, "Service_ClientAsset_Detail", text3);
			dataSet.Merge(dataSet2);
			text3 = "select SPD.*,P.Description  AS ProductName FROM Service_CallTrack SC LEFT JOIN Service_PartsReplaced_Detail\r\n                        SPD on SC.SysDocID=spd.SysDocID and sc.VoucherID=spd.VoucherID  INNER JOIN Product P ON P.ProductID=SPD.ProductID  WHERE ";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + "  SC.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (fromEmployee != "")
			{
				text3 = text3 + " AND SC.ServiceEmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text3 = text3 + " AND SC.ServiceEmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				text3 = text3 + " AND SC.ServiceEmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND SC.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromArea != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text3 = text3 + " AND SC.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			text3 += " ORDER BY SC.TransactionDate ";
			FillDataSet(dataSet2, "Service_PartsReplaced_Detail", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("ServiceAssetTrack_REL", new DataColumn[2]
			{
				dataSet.Tables["Service_CallTrack"].Columns["SysDocID"],
				dataSet.Tables["Service_CallTrack"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Service_ClientAsset_Detail"].Columns["SysDocID"],
				dataSet.Tables["Service_ClientAsset_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			dataSet.Relations.Add("ServiceTrackParts_REL", new DataColumn[2]
			{
				dataSet.Tables["Service_CallTrack"].Columns["SysDocID"],
				dataSet.Tables["Service_CallTrack"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Service_PartsReplaced_Detail"].Columns["SysDocID"],
				dataSet.Tables["Service_PartsReplaced_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetPRojectInvoiceReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromJob, string toJob, string customerIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT DISTINCT JI.*,C.CustomerName,S.FullName,J.JobName,( SELECT SUM(JFD.Amount) FROM Job_Fee_Detail JFD WHERE JFD.JobID=JI.JobID ) AS [Fee Value],\r\n                                (SELECT SUM(Ji1.Total) FROM Job_Invoice Ji1 WHERE Ji1.JobID=JI.JobID AND Ji1.TransactionDate <= JI.TransactionDate ) AS [Total Invoice],\r\n                                (SELECT SUM(Ji1.Total) FROM Job_Invoice Ji1 WHERE Ji1.JobID=JI.JobID AND Ji1.TransactionDate < JI.TransactionDate  ) AS [Previous Invoice]  \r\n                                FROM Job_Invoice JI LEFT JOIN Customer C ON JI.CustomerID=C.CustomerID\r\n                                LEFT JOIN Salesperson S ON JI.SalespersonID=S.SalespersonID    \r\n                                LEFT JOIN Job J ON J.JobID=JI.JobID\r\n                                LEFT JOIN Payment_Term PT ON PT.PaymentTermID=JI.TermID where ";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + "  JI.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (customerIDs != "")
			{
				text3 = text3 + " AND JI.CustomerID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND JI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromArea != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromJob != "")
			{
				text3 = text3 + " AND Ji.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			text3 += " ORDER BY JI.CustomerID ";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Job_Invoice", sqlCommand);
			text3 = "\tSELECT TD.*  FROM Job_Invoice_Detail TD";
			FillDataSet(dataSet, "Job_Invoice_Detail", text3);
			text3 = "SELECT TD.*,JI.Note,JI.TransactionDate  FROM Job_Invoice_Detail TD LEFT JOIN Job_Invoice JI ON TD.SysDocID=JI.SysDocID ANd TD.VoucherID=JI.VoucherID\r\n                        WHERE Amount<>0  ";
			if (fromDate != DateTime.MinValue)
			{
				text3 = text3 + "AND  JI.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (fromCustomer != "")
			{
				text3 = text3 + " AND JI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromCustomerClass != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromCustomerClass + "' AND '" + toCustomerClass + "') ";
			}
			if (fromCustomerGroup != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromCustomerGroup + "' AND '" + toCustomerGroup + "') ";
			}
			if (fromArea != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE AreaID BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text3 = text3 + " AND JI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CountryID BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromJob != "")
			{
				text3 = text3 + " AND Ji.JobID Between '" + fromJob + "' AND '" + toJob + "'";
			}
			text3 += "ORDER BY JI.VoucherID ";
			FillDataSet(dataSet, "Job_PreviousInvoice", text3);
			dataSet.Relations.Add("Customer_Invoice_REL", new DataColumn[2]
			{
				dataSet.Tables["Job_Invoice"].Columns["SysDocID"],
				dataSet.Tables["Job_Invoice"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Job_Invoice_Detail"].Columns["SysDocID"],
				dataSet.Tables["Job_Invoice_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			dataSet.Tables["Job_Invoice"].Columns.Add("TotalInWords", typeof(string));
			foreach (DataRow row in dataSet.Tables["Job_Invoice"].Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(row["Total"].ToString(), out result);
				decimal.TryParse(row["Discount"].ToString(), out result2);
				row["TotalInWords"] = NumToWord.GetNumInWords(result - result2);
			}
			return dataSet;
		}

		public DataSet GetProjectManPowerReport(string Job, string Employee, DateTime fromDate, DateTime toDate)
		{
			string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
			try
			{
				DataSet dataSet = new DataSet();
				string text3 = "SELECT  OTD.WorkDate,Hours,CASE WHEN OTD.Hours>0 THEN   (OTD.Hours-OTD.OTHours) ELSE 0 END AS [Normal Hr],\r\n                                J.JobName,OTD.EmployeeID,OTD.EmployeeName,(CASE WHEN OTD.Hours>0 THEN   (OTD.Hours-OTD.OTHours) ELSE 0 END)*(SELECT (SUM(Amount)/270) FROM Employee_PayrollItem_Detail EPD WHERE EPD.EmployeeID=OTD.EmployeeID) AS 'Normal Salay',\r\n                                ( OTD.OTHours ) * ( SELECT ( sum ( Amount ) / 270 ) FROM Employee_PayrollItem_Detail EPD WHERE EPD.EmployeeID = OTD.EmployeeID AND EPD.PayrollItemID IN ( SELECT PayrollItemID FROM PayrollItem WHERE InOvertime = 1 ) ) AS 'OT Salay',\r\n                                OTD.OTHours FROM OverTimeEntry_Detail OTD LEFT OUTER JOIN Job J ON OTD.JobID=J.JobID\r\n                            WHERE 1=1 \r\n                            AND OTD.WorkDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (Job != "")
				{
					text3 = text3 + " AND OTD.JobID= '" + Job + "'";
				}
				if (Employee != "")
				{
					text3 = text3 + " AND OTD.EmployeeID= '" + Employee + "'";
				}
				FillDataSet(dataSet, "ProjectManPower", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMaterialRequisitionReport(string fromJob, string toJob, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime fromDate, DateTime toDate, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
			try
			{
				DataSet dataSet = new DataSet();
				string text3 = "SELECT JM.*, (SELECT TOP 1 JobID FROM Job_Material_Requisition_Detail JMD WHERE JMD.SysDocID=JM.SysDocID AND JMD.VoucherID=JM.VoucherID)  AS [JobID],\r\n                                (SELECT TOP 1 JobName FROM Job_Material_Requisition_Detail JMD INNER JOIN Job J ON JMD.JobID=J.JobID WHERE JMD.SysDocID=JM.SysDocID AND JMD.VoucherID=JM.VoucherID )  AS [Job Name],\r\n                                (SELECT TOP 1 CustomerName FROM Job_Material_Requisition_Detail JMD INNER JOIN Job J ON JMD.JobID=J.JobID INNER JOIN Customer C ON C.CustomerID=J.CustomerID WHERE JMD.SysDocID=JM.SysDocID AND JMD.VoucherID=JM.VoucherID )  AS [CustomerID]  FROM Job_Material_Requisition JM INNER JOIN Job_Material_Requisition_Detail JMD ON JM.SysDocID=JMD.SysDocID And JM.VoucherID=JMD.VoucherID  LEFT JOIN Product ON Product.ProductID=JMD.ProductID  WHERE JM.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (fromJob != "")
				{
					text3 = text3 + " AND JMD.JobID= '" + fromJob + "'";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND JMD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text3 = text3 + " AND JMD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND JM.LocationID BETWEEN'" + fromLocation + "' AND '" + toLocation + "'";
				}
				FillDataSet(dataSet, "Job_Material_Requisition", text3);
				text3 = "SELECT JM.SysDocID, JM.VoucherID, JMD.ProductID, JobID, CostCategoryID, JM.Description, JMD.Quantity, UnitQuantity, JMD.UnitID, Factor,  FactorType, Cost, Amount, RowIndex + 1 RowIndex, IsBillable, IsBilled, BilledAmount, JMD.ItemType, \r\n                        SUM(JMD.Quantity * COST) AS TotalAmount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount FROM Job_Material_Requisition_Detail JMD INNER JOIN Job_Material_Requisition JM ON JMD.SysDocID=JM.SysDocID and JMD.VoucherID=JM.VoucherID  LEFT JOIN Product ON Product.ProductID=JMD.ProductID   WHERE JM.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (fromJob != "")
				{
					text3 = text3 + "AND JMD.JobID= '" + fromJob + "'";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND JMD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text3 = text3 + " AND JMD.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND JMD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND JM.LocationID BETWEEN'" + fromLocation + "' AND '" + toLocation + "'";
				}
				text3 += "GROUP BY JM.SysDocID,JM.VoucherID,JobID, CostCategoryID,JMD.ProductID,JM.Description,JMD.UnitID,JMD.Quantity,Cost,Factor, Amount, UnitQuantity,FactorType,IsBilled, IsBillable, BilledAmount,RowIndex ,  JMD.ItemType ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Material_Requisition_Detail", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobReport(string fromJob, string toJob, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT Job.*,Job.SalesPersonID + '-' + Salesperson.FullName AS Salesperson, CustomerName,\r\n                             job.SiteLocationID, LocationName, Employee.FirstName+''+ Employee.LastName AS Manager, JobTypeName, JVD.Color, JVD.VehicleID,JVD.Model, JVD.Odometer, JVD.RegistrationNumber, V.VehicleName,\r\n                            CA.Fax,CA.Phone1, CA.Phone2, CA.Mobile\r\n                            FROM Job \r\n\t\t\t\t\t\t\tLEFT JOIN Customer C ON job.CustomerID=C.CustomerID\r\n                             LEFT OUTER JOIN Customer_Address CA ON CA.AddressID='Primary' AND CA.CustomerID=job.CustomerID\r\n\t\t\t\t\t\t\tLEFT JOIN Location ON Job.SiteLocationID=Location.LocationID\r\n                            LEFT OUTER JOIN Salesperson Salesperson ON Job.SalespersonID=Salesperson.SalespersonID\r\n\t\t\t\t\t\t\tLEFT JOIN Employee ON Job.ProjectManagerID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tLEFT JOIN Job_Type ON Job.JobTypeID=Job_Type.JobTypeID\r\n\t\t\t\t\t\t\tLEFT JOIN Job_Vehicle_Detail JVD ON Job.JobID=JVD.JobID\r\n\t\t\t\t\t\t\tLEFT JOIN Vehicle V ON JVD.VehicleID=V.VehicleID where 1=1 ";
			if (fromJob != "")
			{
				text = text + " AND Job.JobID >='" + fromJob + "'";
			}
			if (toJob != "")
			{
				text = text + " AND Job.JobID<='" + toJob + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Job.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Job", text);
			return dataSet;
		}

		public DataSet GetSalesOrderItemsOnJobID(string jobID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				text = "SELECT  TD.ProductID AS [Item Code], TD.Description,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID,  TD.CostCategoryID AS [Cost Category], TD.Quantity,TD.Remarks,TD.LocationID,TD.VoucherID,\r\n                        TD.SysDocID,TD.UnitPrice,TD.Discount,TD.TaxAmount,TD.TaxOption,TD.RowIndex,TD.UnitID,TD.Cost, TD.SubunitPrice,SUM(TD.Quantity*TD.UnitPrice) AS Amount\r\n\t\t\t\t\t\t                       FROM Sales_Order_Detail TD\r\n\t\t\t\t\t\tinner join Sales_Order SO ON SO.SysDocID=TD.SysDocID and SO.VoucherID=td.VoucherID\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\t INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                        WHERE 1=1 ";
				if (jobID != "")
				{
					text = text + " AND SO.JobID ='" + jobID + "'";
				}
				text += "  Group by TD.ProductID, TD.Description,Product.TaxGroupID,PC.TaxGroupID,TD.CostCategoryID,TD.Cost,TD.UnitID,TD.SubunitPrice, TD.Quantity,TD.Remarks,TD.LocationID,TD.VoucherID,TD.SysDocID,TD.UnitPrice,TD.Discount,TD.TaxAmount,TD.TaxOption,TD.RowIndex ";
				text += " ORDER BY TD.RowIndex ";
				FillDataSet(dataSet, "Job", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
