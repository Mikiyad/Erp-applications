using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BudgetingData : DataSet
	{
		public const string BUDGET_TABLE = "Budget";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string DATEFROM_FIELD = "DateFrom";

		public const string DATETO_FIELD = "DateTo";

		public const string BUDGETTYPE_FIELD = "BudgetType";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string BUDGETDETAILS_TABLE = "Budget_Details";

		public const string GROUPID_FIELD = "GroupID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string JOBID_FIELD = "JobID";

		public const string CREDIT_FIELD = "Credit";

		public const string DEBIT_FIELD = "Debit";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string AMOUNT_FIELD = "Amount";

		public DataTable BudgetTable => base.Tables["Budget"];

		public DataTable BudgetDetailsTable => base.Tables["Budget_Details"];

		public BudgetingData()
		{
			BuildDataTables();
		}

		public BudgetingData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Budget");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("VoucherID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("BudgetType", typeof(string));
			columns.Add("DateFrom", typeof(DateTime));
			columns.Add("DateTo", typeof(DateTime));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Budget_Details");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("GroupID", typeof(string));
			columns.Add("AccountID", typeof(string)).AllowDBNull = true;
			columns.Add("JobID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("Credit", typeof(decimal));
			columns.Add("Debit", typeof(decimal));
			columns.Add("RowIndex", typeof(long));
			base.Tables.Add(dataTable);
		}
	}
}
