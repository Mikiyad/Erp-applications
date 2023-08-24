using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LCData : DataSet
	{
		public const string LCID_FIELD = "LCID";

		public const string LCNAME_FIELD = "LCName";

		public const string LCDATE_FIELD = "LCDate";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string APACCOUNTID_FIELD = "APAccountID";

		public const string PARTNERID_FIELD = "PartnerID";

		public const string REFERENCE_FIELD = "Reference";

		public const string AMOUNT_FIELD = "Amount";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string LENDERID_FIELD = "LenderID";

		public const string ISIRREVOCABLE_FIELD = "IsIrrevocable";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string LENDERS_ALIAS = "LendersAlias";

		public const string PAYMENTACCOUNTS_ALIAS = "PaymentAccountsAlias";

		public const string LCS_TABLE = "LCs";

		public DataTable LCTable => base.Tables["LCs"];

		public LCData()
		{
			BuildDataTables();
		}

		public LCData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("LCs");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LCID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LCName", typeof(string)).AllowDBNull = false;
			columns.Add("LCDate", typeof(DateTime)).AllowDBNull = false;
			columns.Add("PartnerID", typeof(int)).AllowDBNull = false;
			columns.Add("AccountID", typeof(int));
			columns.Add("APAccountID", typeof(int));
			columns.Add("Reference", typeof(string));
			columns.Add("Amount", typeof(decimal)).AllowDBNull = false;
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("LenderID", typeof(int));
			columns.Add("IsIrrevocable", typeof(bool));
			columns.Add("Note", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
