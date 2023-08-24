using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class OpeningBalanceBatchData : DataSet
	{
		public const string BATCHID_FIELD = "BatchID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string BATCHDATE_FIELD = "BatchDate";

		public const string BATCHTYPE_FIELD = "BatchType";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string OPENINGBALANCEBATCH_TABLE = "Opening_Balance_Batch";

		public const string TRANSACTIONSYSDOCID_FIELD = "TransactionSysDocID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string PURCHASEDATE_FIELD = "PurchaseDate";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string DUEDATE_FIELD = "DueDate";

		public const string INVOICEAMOUNT_FIELD = "InvoiceAmount";

		public const string BALANCEAMOUNT_FIELD = "BalanceAmount";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string COST_FIELD = "Cost";

		public const string TOTAL_FIELD = "Total";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string OPENINGBALANCEBATCHDETAIL_TABLE = "Opening_Balance_Batch_Detail";

		public DataTable OpeningBalanceBatchTable => base.Tables["Opening_Balance_Batch"];

		public DataTable OpeningBalanceBatchDetailsTable => base.Tables["Opening_Balance_Batch_Detail"];

		public OpeningBalanceBatchData()
		{
			BuildDataTables();
		}

		public OpeningBalanceBatchData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Opening_Balance_Batch");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BatchID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("SysDocID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("BatchDate", typeof(DateTime));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("BatchType", typeof(byte));
			columns.Add("TransactionSysDocID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Opening_Balance_Batch_Detail");
			columns = dataTable.Columns;
			columns.Add("BatchID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("TransactionSysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Quantity", typeof(float));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("PurchaseDate", typeof(DateTime));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("InvoiceAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("BalanceAmount", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
