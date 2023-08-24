using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class StoreEmployeeData : DataSet
	{
		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string STOREID_FIELD = "StoreID";

		public const string DATESTARTED_FIELD = "DateStarted";

		public const string DATEENDED_FIELD = "DateEnded";

		public const string NOTES_FIELD = "Notes";

		public const string STOREEMPLOYEE_TABLE = "[Store Employees]";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public StoreEmployeeData()
		{
			BuildDataTables();
		}

		public StoreEmployeeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Store Employees]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("StoreID", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("EmployeeID", typeof(int));
			dataColumn2.AllowDBNull = true;
			dataColumn2.AutoIncrement = false;
			columns.Add("DateStarted", typeof(DateTime));
			columns.Add("DateEnded", typeof(DateTime));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Notes", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
