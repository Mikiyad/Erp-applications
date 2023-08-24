using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductionData : DataSet
	{
		public const string PRODUCTION_TABLE = "Mfg_Production";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE1_FIELD = "Reference1";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string ROUTEID_FIELD = "RouteID";

		public const string NOTE_FIELD = "Note";

		public const string JOBORDERID_FIELD = "JobOrderID";

		public const string BOMID_FIELD = "BOMID";

		public const string WORKCOMPDATE_FIELD = "WorkCompDate";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTIONDETAIL_TABLE = "Mfg_Production_Detail";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string QUANTITYBUILD_FIELD = "QuantityBuild";

		public const string COST_FIELD = "Cost";

		public const string UNITID_FIELD = "UnitID";

		public const string COSTALLOCATION_FIELD = "CostAllocation";

		public const string TOTAL_FIELD = "Total";

		public const string NEXTROUTE_FIELD = "NextRoute";

		public const string PRODUCTIONEXPENSE_TABLE = "Mfg_Production_Expense";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string RATETYPE_FIELD = "RateType";

		public const string AMOUNT_FIELD = "Amount";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string PRODUCTIONRAWMATERIALS_TABLE = "Mfg_Production_Raw_Material";

		public const string QUANTITY_FIELD = "Quantity";

		public const string PRICE_FIELD = "UnitPrice";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string PRODUCTIONRESOURCE_TABLE = "Mfg_Production_Resource";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string EMPLOYEENAME_FIELD = "EmployeeName";

		public const string POSITIONID_FIELD = "PositionID";

		public const string REMARKS_FIELD = "Remarks";

		public const string HOUR_FIELD = "Hour";

		public DataTable ProductionTable => base.Tables["Mfg_Production"];

		public DataTable ProductionDetailTable => base.Tables["Mfg_Production_Detail"];

		public DataTable ProductionExpenseTable => base.Tables["Mfg_Production_Expense"];

		public DataTable ProductionRawMaterialTable => base.Tables["Mfg_Production_Raw_Material"];

		public DataTable ProductionResourceTable => base.Tables["Mfg_Production_Resource"];

		public ProductionData()
		{
			BuildDataTables();
		}

		public ProductionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Mfg_Production");
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
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("JobOrderID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("WorkCompDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference1", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("BOMID", typeof(string));
			columns.Add("RouteID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Total", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Mfg_Production_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("QuantityBuild", typeof(float));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Cost", typeof(decimal));
			columns.Add("CostAllocation", typeof(decimal));
			columns.Add("LocationID", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("NextRoute", typeof(string));
			columns.Add("Total", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Mfg_Production_Expense");
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
			dataTable = new DataTable("Mfg_Production_Raw_Material");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Quantity", typeof(float));
			columns.Add("RowIndex", typeof(short));
			columns.Add("UnitPrice", typeof(decimal));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("Total", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Mfg_Production_Resource");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("PositionID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("Hour", typeof(float));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
