using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DocumentCategoryData : DataSet
	{
		public static string CATEGORYID_FIELD = "CategoryID";

		public static string NAME_FIELD = "Name";

		public static string DESCRIPTION_FIELD = "Description";

		public static string NOTE_FIELD = "NOTE";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public static string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public static string DOCUMENTCATEGORIES_TABLE = "[Document Categories]";

		public DataTable DocumentCategoryTable => base.Tables[DOCUMENTCATEGORIES_TABLE];

		public DocumentCategoryData()
		{
			BuildDataTables();
		}

		public DocumentCategoryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable(DOCUMENTCATEGORIES_TABLE);
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add(CATEGORYID_FIELD, typeof(short));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add(NAME_FIELD, typeof(string)).AllowDBNull = false;
			columns.Add(DESCRIPTION_FIELD, typeof(string));
			columns.Add(NOTE_FIELD, typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add(DATETIMESTAMP_FIELD, typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
