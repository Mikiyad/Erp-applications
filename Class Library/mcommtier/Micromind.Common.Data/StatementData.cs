using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class StatementData : DataSet
	{
		public const string STATEMENT_TABLE = "Statement";

		public const string PARTYID_FIELD = "PartyID";

		public const string PARTYNAME_FIELD = "PartyName";

		public const string BALANCE_FIELD = "Balance";

		public const string EMAILADDRESS_FIELD = "EmailAddress";

		public const string PERIODFROM_FIELD = "PeriodFrom";

		public const string PERIODTO_FIELD = "PeriodTo";

		public const string ATTACHMENT_FIELD = "Attachment";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable AreaTable => base.Tables["Statement"];

		public StatementData()
		{
			BuildDataTables();
		}

		public StatementData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Statement");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("PartyID", typeof(string)).AllowDBNull = false;
			columns.Add("PartyName", typeof(string));
			columns.Add("PeriodFrom", typeof(DateTime));
			columns.Add("PeriodTo", typeof(DateTime));
			columns.Add("Balance", typeof(decimal));
			columns.Add("EmailAddress", typeof(string));
			columns.Add("Attachment", typeof(byte[]));
			base.Tables.Add(dataTable);
		}
	}
}
