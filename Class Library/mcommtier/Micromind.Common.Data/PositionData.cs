using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PositionData : DataSet
	{
		public const string POSITION_TABLE = "Position";

		public const string POSITIONID_FIELD = "PositionID";

		public const string POSITIONNAME_FIELD = "PositionName";

		public const string REPORTTO_FIELD = "ReportTo";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string JOBDESCRIPTION_FIELD = "JobDescription";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string APPRAISALINTERVAL_FIELD = "AppraisalInterval";

		public const string APPRAISALFROMDATE_FIELD = "AppraisalFromDate";

		public const string APPRAISALTODATE_FIELD = "AppraisalToDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string KPIPARAMETER_FIELD = "KPIParameter";

		public const string WEIGHTAGE_FIELD = "Weightage";

		public const string SCALE_FIELD = "Scale";

		public const string TARGET_FIELD = "Target";

		public const string POSITIONDETAILS_TABLE = "Position_Details";

		public const string PERFORMANCEPARAMETER_FIELD = "PerformanceParameter";

		public const string SCORE_FIELD = "Score";

		public const string PERFORMANCEDETAILS_TABLE = "Performance_Details";

		public DataTable PositionTable => base.Tables["Position"];

		public DataTable PositionDetailTable => base.Tables["Position_Details"];

		public DataTable PerformanceDetailTable => base.Tables["Performance_Details"];

		public PositionData()
		{
			BuildDataTables();
		}

		public PositionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Position");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PositionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PositionName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("ReportTo", typeof(string));
			columns.Add("JobDescription", typeof(string));
			columns.Add("AppraisalInterval", typeof(short));
			columns.Add("AppraisalFromDate", typeof(DateTime));
			columns.Add("AppraisalToDate", typeof(DateTime));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Position_Details");
			columns = dataTable.Columns;
			columns.Add("PositionID", typeof(string));
			columns.Add("KPIParameter", typeof(string));
			columns.Add("Weightage", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Scale", typeof(string));
			columns.Add("Target", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Performance_Details");
			columns = dataTable.Columns;
			columns.Add("PositionID", typeof(string));
			columns.Add("PerformanceParameter", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Score", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
