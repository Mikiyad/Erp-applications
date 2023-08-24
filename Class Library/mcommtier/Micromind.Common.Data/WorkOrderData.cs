using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class WorkOrderData : DataSet
	{
		public const string WORKORDER_TABLE = "Mfg_Work_Order";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string WORKORDERDETAIL_TABLE = "Mfg_Work_Order_Detail";

		public const string BOMPRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string BOMID_FIELD = "BOMID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITID_FIELD = "UnitID";

		public const string ROUTEGROUPID_FIELD = "RouteGroupID";

		public const string REMARKS_FIELD = "Remarks";

		public const string WORKORDEREXPENSE_TABLE = "Mfg_Work_Order_Expense";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string RATETYPE_FIELD = "RateType";

		public const string AMOUNT_FIELD = "Amount";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public DataTable WorkOrderTable => base.Tables["Mfg_Work_Order"];

		public DataTable WorkOrderDetailTable => base.Tables["Mfg_Work_Order_Detail"];

		public DataTable WorkOrderExpenseTable => base.Tables["Mfg_Work_Order_Expense"];

		public WorkOrderData()
		{
			BuildDataTables();
		}

		public WorkOrderData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Mfg_Work_Order");
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
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Mfg_Work_Order_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Quantity", typeof(float));
			columns.Add("UnitID", typeof(string));
			columns.Add("BOMID", typeof(string));
			columns.Add("RouteGroupID", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Mfg_Work_Order_Expense");
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
