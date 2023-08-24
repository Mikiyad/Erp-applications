using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class WorkLocationData : DataSet
	{
		public const string WORKLOCATION_TABLE = "Work_Location";

		public const string WORKLOCATIONID_FIELD = "WorkLocationID";

		public const string WORKLOCATIONNAME_FIELD = "WorkLocationName";

		public const string JOBID_FIELD = "JobID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable WorkLocationTable => base.Tables["Work_Location"];

		public WorkLocationData()
		{
			BuildDataTables();
		}

		public WorkLocationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Work_Location");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("WorkLocationID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("WorkLocationName", typeof(string)).AllowDBNull = false;
			columns.Add("JobID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
