using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductKitData : DataSet
	{
		public static string PRODUCTKITS_TABLE = "[Product Kits]";

		public static string KITMASTERS_TABLE = "[Kit Masters]";

		public static string KITID_FIELD = "KitID";

		public static string MASTERID_FIELD = "MasterID";

		public static string COMPONENTID_FIELD = "ComponentID";

		public static string QUANTITY_FIELD = "Quantity";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable ProductKitTable => base.Tables[PRODUCTKITS_TABLE];

		public ProductKitData()
		{
			BuildDataTables();
		}

		public ProductKitData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable(PRODUCTKITS_TABLE);
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add(KITID_FIELD, typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add(MASTERID_FIELD, typeof(int));
			columns.Add(COMPONENTID_FIELD, typeof(int));
			columns.Add(QUANTITY_FIELD, typeof(float)).DefaultValue = 0;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
