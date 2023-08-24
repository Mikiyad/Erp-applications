using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AssemblyBuildData : DataSet
	{
		public const string ASSEMBLYBUILD_TABLE = "Assembly_Build";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYBUILD_FIELD = "QuantityBuild";

		public const string UNITCOST_FIELD = "UnitCost";

		public const string UNITWEIGHT_FIELD = "UnitWeight";

		public const string ASSEMBLYPRODUCTID_FIELD = "AssemblyProductID";

		public const string NOTE_FIELD = "Note";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string WORKCOMPDATE_FIELD = "WorkCompDate";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ASSEMBLYBUILDDETAIL_TABLE = "Assembly_Build_Detail";

		public const string BOMPRODUCTID_FIELD = "BOMProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string SUBUNITCOST_FIELD = "SubUnitCost";

		public const string COGS_FIELD = "COGS";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITID_FIELD = "UnitID";

		public const string FACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string ASSEMBLYBUILDEXPENSE_TABLE = "Assembly_Build_Expense";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string RATETYPE_FIELD = "RateType";

		public const string AMOUNT_FIELD = "Amount";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public DataTable AssemblyBuildTable => base.Tables["Assembly_Build"];

		public DataTable AssemblyBuildDetailTable => base.Tables["Assembly_Build_Detail"];

		public DataTable AssemblyBuildExpenseTable => base.Tables["Assembly_Build_Expense"];

		public AssemblyBuildData()
		{
			BuildDataTables();
		}

		public AssemblyBuildData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Assembly_Build");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("AssemblyProductID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("WorkCompDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("QuantityBuild", typeof(decimal));
			columns.Add("UnitWeight", typeof(decimal));
			columns.Add("UnitCost", typeof(decimal));
			columns.Add("Note", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Assembly_Build_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("BOMProductID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Cost", typeof(decimal));
			columns.Add("SubUnitCost", typeof(decimal));
			columns.Add("COGS", typeof(decimal));
			columns.Add("Quantity", typeof(float));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitFactor", typeof(float));
			columns.Add("FactorType", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Assembly_Build_Expense");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ExpenseID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
