using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class UDFData : DataSet
	{
		public const string UDF_TABLE = "UDF_Setup";

		public const string ENTITYID_FIELD = "EntityID";

		public const string UDFTABLENAME_FIELD = "UDFTableName";

		public const string FORMNAME_FIELD = "FormName";

		public const string FIELDNAME_FIELD = "FieldName";

		public const string DATATYPE_FIELD = "DataType";

		public const string FIELDSIZE_FIELD = "FieldSize";

		public const string DISPLAYNAME_FIELD = "DisplayName";

		public const string LISTTYPE_FIELD = "ListType";

		public const string UDLISTNAME_FIELD = "UDListName";

		public const string ENTITYID1_FIELD = "EntityID1";

		public const string ENTITYID2_FIELD = "EntityID2";

		public const string ENTITYID3_FIELD = "EntityID3";

		public DataTable UDFTable => base.Tables["UDF_Setup"];

		public UDFData()
		{
			BuildDataTables();
		}

		public UDFData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("UDF_Setup");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("FieldName", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("UDFTableName", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			columns.Add("FormName", typeof(string));
			columns.Add("DataType", typeof(byte));
			columns.Add("FieldSize", typeof(short));
			columns.Add("DisplayName", typeof(string));
			columns.Add("ListType", typeof(string));
			columns.Add("UDListName", typeof(string));
			base.Tables.Add(dataTable);
		}

		public static string GetUDFTableName(string udfTable)
		{
			return "UDF_" + udfTable;
		}
	}
}
