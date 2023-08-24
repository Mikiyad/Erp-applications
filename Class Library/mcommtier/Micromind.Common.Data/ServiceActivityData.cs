using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ServiceActivityData : DataSet
	{
		public const string SERVICEACTIVITY_TABLE = "Service_Activity";

		public const string SERVICEACTIVITYID_FIELD = "ServiceActivityID";

		public const string SERVICEACTIVITYNAME_FIELD = "ServiceActivityName";

		public const string SERVICEACTIVITYDESC_FIELD = "Description";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ServiceActivityTable => base.Tables["Service_Activity"];

		public ServiceActivityData()
		{
			BuildDataTables();
		}

		public ServiceActivityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Service_Activity");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ServiceActivityID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ServiceActivityName", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
