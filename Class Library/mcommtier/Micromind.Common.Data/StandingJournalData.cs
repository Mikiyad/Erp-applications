using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class StandingJournalData : DataSet
	{
		public const string STANDINGJOURNAL_TABLE = "Standing_Journal";

		public const string STANDINGJOURNALID_FIELD = "StandingJournalID";

		public const string TRANSACTIONSYSDOCID_FIELD = "TransactionSysDocID";

		public const string REFERENCE_FIELD = "Reference";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string NARRATION_FIELD = "Narration";

		public const string NOTE_FIELD = "Note";

		public const string STARTYEAR_FIELD = "StartYear";

		public const string ENDYEAR_FIELD = "EndYear";

		public const string STARTMONTH_FIELD = "StartMonth";

		public const string ENDMONTH_FIELD = "EndMonth";

		public const string STATUS_FIELD = "Status";

		public const string STANDINGJOURNALDETAILS_TABLE = "Standing_Journal_Detail";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string DEBIT_FIELD = "Debit";

		public const string CREDIT_FIELD = "Credit";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string AMOUNT_FIELD = "Amount";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable StandingJournalTable => base.Tables["Standing_Journal"];

		public DataTable StandingJournalDetailsTable => base.Tables["Standing_Journal_Detail"];

		public StandingJournalData()
		{
			BuildDataTables();
		}

		public StandingJournalData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Standing_Journal");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("StandingJournalID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Reference", typeof(string));
			columns.Add("TransactionSysDocID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("StartYear", typeof(short));
			columns.Add("StartMonth", typeof(byte));
			columns.Add("EndYear", typeof(short));
			columns.Add("EndMonth", typeof(byte));
			columns.Add("Status", typeof(byte));
			columns.Add("Narration", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Standing_Journal_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("StandingJournalID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("AccountID", typeof(string)).AllowDBNull = true;
			columns.Add("Debit", typeof(decimal));
			columns.Add("Credit", typeof(decimal));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Reference", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("CostCenterID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
