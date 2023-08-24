using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RecurringInvoiceData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TYPE_FIELD = "SysDocType";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string REPEATEEVERY_FIELD = "RepeateEvery";

		public const string INTERVAL_FIELD = "Interval";

		public const string LASTRUNDATE_FIELD = "LastRunDate";

		public const string LASTVOUCHERID_FIELD = "LastVoucherID";

		public const string LASTSYSDOCID_FIELD = "LastSysDocID";

		public const string PROCESSEDBY_FIELD = "ProcessedBy";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string STATUS_FIELD = "Status";

		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string CREATEDSYSDOCID_FIELD = "CreatedSysDocID";

		public const string CREATEDVOUCHERID_FIELD = "CreatedVoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string INVOICENOTE_FIELD = "InvoiceNote";

		public const string RECURRINGTRANSACTIONS_TABLE = "Recurring_Transaction";

		public const string RECURRINGTRANSACTIONDETAILS_TABLE = "Recurring_Transaction_Detail";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable RecurringInvoiceTransactionTable => base.Tables["Recurring_Transaction"];

		public DataTable RecurringTransactionDetailsTable => base.Tables["Recurring_Transaction_Detail"];

		public RecurringInvoiceData()
		{
			BuildDataTables();
		}

		public RecurringInvoiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Recurring_Transaction");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TransactionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SysDocType", typeof(int));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("RepeateEvery", typeof(string));
			columns.Add("Interval", typeof(int));
			columns.Add("LastRunDate", typeof(DateTime));
			columns.Add("LastVoucherID", typeof(string));
			columns.Add("LastSysDocID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("ProcessedBy", typeof(string));
			columns.Add("Status", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Recurring_Transaction_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("TransactionID", typeof(string));
			dataColumn.AllowDBNull = false;
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("CreatedSysDocID", typeof(string));
			columns.Add("CreatedVoucherID", typeof(string));
			columns.Add("InvoiceNote", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
