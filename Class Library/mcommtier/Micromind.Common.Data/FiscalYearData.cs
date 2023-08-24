using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FiscalYearData : DataSet
	{
		public const string FISCALYEAR_TABLE = "FiscalYear";

		public const string FISCALYEARID_FIELD = "FiscalYearID";

		public const string FISCALYEARNAME_FIELD = "FiscalYearName";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string PERIODSCOUNT_FIELD = "PeriodsCount";

		public const string STATUS_FIELD = "Status";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable FiscalYearTable => base.Tables["FiscalYear"];

		public FiscalYearData()
		{
			BuildDataTables();
		}

		public FiscalYearData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("FiscalYear");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("FiscalYearID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("FiscalYearName", typeof(string)).AllowDBNull = false;
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("PeriodsCount", typeof(byte));
			columns.Add("Status", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
