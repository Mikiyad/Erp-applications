using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CardTypeData : DataSet
	{
		public const string TYPEID_FIELD = "TypeID";

		public const string TYPENAME_FIELD = "TypeName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string CARDTYPES_TABLE = "[Card Types]";

		public DataTable CardTypeTable => base.Tables["[Card Types]"];

		public CardTypeData()
		{
			BuildDataTables();
		}

		public CardTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Card Types]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TypeID", typeof(byte));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("TypeID", typeof(byte)).AllowDBNull = false;
			columns.Add("TypeName", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
