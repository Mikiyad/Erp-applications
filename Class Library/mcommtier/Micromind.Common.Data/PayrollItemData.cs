using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PayrollItemData : DataSet
	{
		public const string PAYROLLITEM_TABLE = "PayrollItem";

		public const string PAYROLLITEMID_FIELD = "PayrollItemID";

		public const string PAYROLLITEMNAME_FIELD = "PayrollItemName";

		public const string PAYROLLITEMTYPE_FIELD = "PayrollItemType";

		public const string PAYCODETYPE_FIELD = "PayCodeType";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string INLEAVESALARY_FIELD = "InLeaveSalary";

		public const string INDEDUCTION_FIELD = "InDeduction";

		public const string INSERVICEBENEFIT_FIELD = "InServiceBenefit";

		public const string INOVERTIME_FIELD = "InOvertime";

		public const string INFIXED_FIELD = "InFixed";

		public const string INSALARYDEDUCTION_FIELD = "InSalaryDeduction";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PayrollItemTable => base.Tables["PayrollItem"];

		public PayrollItemData()
		{
			BuildDataTables();
		}

		public PayrollItemData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("PayrollItem");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PayrollItemID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PayrollItemName", typeof(string)).AllowDBNull = false;
			columns.Add("PayrollItemType", typeof(byte));
			columns.Add("PayCodeType", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("AccountID", typeof(string)).AllowDBNull = false;
			columns.Add("InLeaveSalary", typeof(bool));
			columns.Add("InDeduction", typeof(bool));
			columns.Add("InServiceBenefit", typeof(bool));
			columns.Add("InOvertime", typeof(bool));
			columns.Add("InFixed", typeof(bool));
			columns.Add("InSalaryDeduction", typeof(bool));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
