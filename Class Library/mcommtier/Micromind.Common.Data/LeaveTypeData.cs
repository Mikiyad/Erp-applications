using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LeaveTypeData : DataSet
	{
		public const string LEAVETYPE_TABLE = "Leave_Type";

		public const string LEAVETYPEID_FIELD = "LeaveTypeID";

		public const string LEAVETYPENAME_FIELD = "LeaveTypeName";

		public const string INACTIVE_FIELD = "Inactive";

		public const string DAYS_FIELD = "Days";

		public const string ISPAYABLE_FIELD = "IsPayable";

		public const string ISCUMULATIVE_FIELD = "IsCumulative";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ISANNUAL_FIELD = "IsAnnual";

		public const string ACTIVATEHC_FIELD = "ActivateHC";

		public const string DEDUCTIONPROPORTION_FIELD = "DeductionProportion";

		public const string MONTHGREATER1_FIELD = "MonthGreater1";

		public const string MONTHLESSER1_FIELD = "MonthLesser1";

		public const string ALLOWEDDAYS1_FIELD = "AllowedDays1";

		public const string MONTHGREATER2_FIELD = "MonthGreater2";

		public const string MONTHLESSER2_FIELD = "MonthLesser2";

		public const string ALLOWEDDAYS2_FIELD = "AllowedDays2";

		public const string MONTHGREATER3_FIELD = "MonthGreater3";

		public const string MONTHLESSER3_FIELD = "MonthLesser3";

		public const string ALLOWEDDAYS3_FIELD = "AllowedDays3";

		public const string ISENCASH_FIELD = "IsEncash";

		public const string ISLEAVESETTLE_FIELD = "IsLeaveSettle";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable LeaveTypeTable => base.Tables["Leave_Type"];

		public LeaveTypeData()
		{
			BuildDataTables();
		}

		public LeaveTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Leave_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LeaveTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LeaveTypeName", typeof(string)).AllowDBNull = false;
			columns.Add("Days", typeof(short));
			columns.Add("IsPayable", typeof(bool));
			columns.Add("IsCumulative", typeof(bool));
			columns.Add("IsAnnual", typeof(bool));
			columns.Add("ActivateHC", typeof(bool));
			columns.Add("DeductionProportion", typeof(decimal));
			columns.Add("Inactive", typeof(bool));
			columns.Add("AccountID", typeof(string));
			columns.Add("MonthGreater1", typeof(int));
			columns.Add("MonthLesser1", typeof(int));
			columns.Add("AllowedDays1", typeof(decimal));
			columns.Add("MonthGreater2", typeof(int));
			columns.Add("MonthLesser2", typeof(int));
			columns.Add("AllowedDays2", typeof(decimal));
			columns.Add("MonthGreater3", typeof(double));
			columns.Add("MonthLesser3", typeof(double));
			columns.Add("AllowedDays3", typeof(decimal));
			columns.Add("IsEncash", typeof(bool));
			columns.Add("IsLeaveSettle", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
