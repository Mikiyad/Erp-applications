using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CreditCardData : DataSet
	{
		public const string CARDID_FIELD = "CardID";

		public const string SHORTNAME_FIELD = "ShortName";

		public const string NUMBER_FIELD = "Number";

		public const string EXPDATE_FIELD = "ExpDate";

		public const string PRINTEDNAME_FIELD = "PrintedName";

		public const string ADDRESS1_FIELD = "Address1";

		public const string ADDRESS2_FIELD = "Address2";

		public const string POSTALCODE_FIELD = "PostalCode";

		public const string NOTE_FIELD = "Note";

		public const string TYPEID_FIELD = "TypeID";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string CREDITCARD_TABLE = "[Credit Cards]";

		public DataTable CreditCardTable => base.Tables["[Credit Cards]"];

		public CreditCardData()
		{
			BuildDataTables();
		}

		public CreditCardData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Credit Cards]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CardID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ShortName", typeof(string)).AllowDBNull = false;
			columns.Add("TypeID", typeof(byte));
			columns.Add("Number", typeof(string));
			columns.Add("ExpDate", typeof(DateTime));
			columns.Add("PrintedName", typeof(string));
			columns.Add("Address1", typeof(string));
			columns.Add("Address2", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
