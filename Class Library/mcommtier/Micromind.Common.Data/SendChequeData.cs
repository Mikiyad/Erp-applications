using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SendChequeData : DataSet
	{
		public const string CHEQUESEND_TABLE = "Cheque_Send";

		public const string CHEQUESENDDETAIL_TABLE = "Cheque_Send_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CHEQUEID_FIELD = "ChequeID";

		public const string BANKACCOUNTID_FIELD = "BankAccountID";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string REFERENCE_FIELD = "Reference";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REASON_FIELD = "Reason";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ChequeSendTable => base.Tables["Cheque_Send"];

		public DataTable ChequeSendDetailTable => base.Tables["Cheque_Send_Detail"];

		public SendChequeData()
		{
			BuildDataTables();
		}

		public SendChequeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Cheque_Send");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("ChequeID", typeof(short));
			columns.Add("BankAccountID", typeof(string));
			columns.Add("Reason", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("Reference", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Cheque_Send_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ChequeID", typeof(short));
			columns.Add("Status", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
