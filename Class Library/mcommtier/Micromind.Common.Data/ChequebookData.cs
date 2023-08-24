using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ChequebookData : DataSet
	{
		public const string CHEQUEBOOK_TABLE = "Chequebook";

		public const string CHEQUEBOOKID_FIELD = "ChequebookID";

		public const string CHEQUEBOOKNAME_FIELD = "ChequebookName";

		public const string BANKID_FIELD = "BankID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string PDCISSUEDACCOUNTID_FIELD = "PDCIssuedAccountID";

		public const string NEXTNUMBER_FIELD = "NextNumber";

		public const string SIGNATORY_FIELD = "Signatory";

		public const string TEMPLATENAME_FIELD = "TemplateName";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CHEQUEREGISTER_TABLE = "Cheque_Register";

		public const string CHEQUENUMBER_FIELD = "ChequeNumber";

		public DataTable ChequebookTable => base.Tables["Chequebook"];

		public ChequebookData()
		{
			BuildDataTables();
		}

		public ChequebookData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Chequebook");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ChequebookID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ChequebookName", typeof(string)).AllowDBNull = false;
			columns.Add("BankID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("PDCIssuedAccountID", typeof(string));
			columns.Add("Signatory", typeof(string));
			columns.Add("TemplateName", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
