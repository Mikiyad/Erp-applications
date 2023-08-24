using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class OverTimeData : DataSet
	{
		public const string OVERTIME_TABLE = "Employee_OverTime";

		public const string OVERTIMEID_FIELD = "OverTimeID";

		public const string OVERTIMENAME_FIELD = "OverTimeName";

		public const string ISFIXED_FIELD = "IsFixed";

		public const string FIXEDAMOUNT_FIELD = "FixedAmount";

		public const string FACTOR_FIELD = "Factor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable OverTimeTable => base.Tables["Employee_OverTime"];

		public OverTimeData()
		{
			BuildDataTables();
		}

		public OverTimeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_OverTime");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("OverTimeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("OverTimeName", typeof(string)).AllowDBNull = false;
			columns.Add("IsFixed", typeof(string));
			columns.Add("FixedAmount", typeof(string));
			columns.Add("Factor", typeof(string));
			columns.Add("FactorType", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
