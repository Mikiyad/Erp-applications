using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EOSRuleData : DataSet
	{
		public const string EOSRULE_TABLE = "Employee_EOSRule";

		public const string EOSRULEID_FIELD = "EOSRuleID";

		public const string EOSRULENAME_FIELD = "EOSRuleName";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string EOSSYSTEM_FIELD = "EOSSystem";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EOSRULEDETAIL_TABLE = "Employee_EOSRule_Detail";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string YEARFROM_FIELD = "YearFrom";

		public const string YEARTO_FIELD = "YearTo";

		public const string EOSDAY_FIELD = "EOSDay";

		public const string RESIGNEDTYPE_FIELD = "ResignedType";

		public DataTable EOSRuleTable => base.Tables["Employee_EOSRule"];

		public DataTable EOSRuleDetailTable => base.Tables["Employee_EOSRule_Detail"];

		public EOSRuleData()
		{
			BuildDataTables();
		}

		public EOSRuleData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_EOSRule");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EOSRuleID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("EOSRuleName", typeof(string)).AllowDBNull = false;
			columns.Add("AccountID", typeof(string));
			columns.Add("ResignedType", typeof(short));
			columns.Add("EOSSystem", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_EOSRule_Detail");
			columns = dataTable.Columns;
			columns.Add("EOSRuleID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("ResignedType", typeof(string));
			columns.Add("YearFrom", typeof(int));
			columns.Add("YearTo", typeof(int));
			columns.Add("EOSDay", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
