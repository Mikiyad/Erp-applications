using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RegisterData : DataSet
	{
		public const string REGISTER_TABLE = "Register";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string REGISTERNAME_FIELD = "RegisterName";

		public const string NOTE_FIELD = "Note";

		public const string CASHACCOUNTID_FIELD = "CashAccountID";

		public const string PDCRECEIVEDACCOUNTID_FIELD = "PDCReceivedAccountID";

		public const string PDCISSUEDACCOUNTID_FIELD = "PDCIssuedAccountID";

		public const string CARDRECEIVEDACCOUNTID_FIELD = "CardReceivedAccountID";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable RegisterTable => base.Tables["Register"];

		public RegisterData()
		{
			BuildDataTables();
		}

		public RegisterData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Register");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("RegisterID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("RegisterName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("CashAccountID", typeof(string));
			columns.Add("PDCReceivedAccountID", typeof(string));
			columns.Add("PDCIssuedAccountID", typeof(string));
			columns.Add("CardReceivedAccountID", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
