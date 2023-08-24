using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CheckStubData : DataSet
	{
		public const string CHECKSTUBS_TABLE = "[Check Stubs]";

		public const string CHECKID_FIELD = "CheckID";

		public const string TYPE_FIELD = "Type";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string CHECKAMOUNT_FIELD = "CheckAmount";

		public const string CHECKNUMBER_FIELD = "CheckNumber";

		public const string CHECKDATE_FIELD = "CheckDate";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string CHECKBANKID_FIELD = "CheckBankID";

		public const string ORDEREDTO_FIELD = "OrderedTo";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LASTPRINTDATE_FIELD = "LastPrintDate";

		public const string CHECKBANKNAME_FIELD = "CheckBankName";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable CheckStubTable => base.Tables["[Check Stubs]"];

		public CheckStubData()
		{
			BuildDataTables();
		}

		public CheckStubData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Check Stubs]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CheckID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Type", typeof(byte));
			columns.Add("PayeeID", typeof(int));
			columns.Add("CheckAmount", typeof(decimal));
			columns.Add("CheckNumber", typeof(string));
			columns.Add("CheckDate", typeof(DateTime));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("CheckBankID", typeof(int));
			columns.Add("OrderedTo", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LastPrintDate", typeof(DateTime));
			columns.Add("CheckBankName", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
