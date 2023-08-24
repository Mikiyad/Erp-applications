using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductGroupData : DataSet
	{
		public static string PRODUCTGROUPS_TABLE = "[Product Groups]";

		public static string GROUPID_FIELD = "GroupID";

		public static string MASTERID_FIELD = "MasterID";

		public static string COMPONENTID_FIELD = "ComponentID";

		public static string QUANTITY_FIELD = "Quantity";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable ProductGroupTable => base.Tables[PRODUCTGROUPS_TABLE];

		public ProductGroupData()
		{
			BuildDataTables();
		}

		public ProductGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable(PRODUCTGROUPS_TABLE);
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add(GROUPID_FIELD, typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add(MASTERID_FIELD, typeof(int));
			columns.Add(COMPONENTID_FIELD, typeof(int));
			columns.Add(QUANTITY_FIELD, typeof(float));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
