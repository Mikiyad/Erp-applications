using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobTypeData : DataSet
	{
		public const string JOBTYPE_TABLE = "Job_Type";

		public const string JOBTYPEID_FIELD = "JobTypeID";

		public const string JOBTYPENAME_FIELD = "JobTypeName";

		public const string JOBTYPEDESC_FIELD = "Description";

		public const string AMCENABLED_FIELD = "AMCEnabled";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable JobTypeTable => base.Tables["Job_Type"];

		public JobTypeData()
		{
			BuildDataTables();
		}

		public JobTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("JobTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("JobTypeName", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("AMCEnabled", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
