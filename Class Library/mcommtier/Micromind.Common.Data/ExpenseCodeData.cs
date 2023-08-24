using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ExpenseCodeData : DataSet
	{
		public const string EXPENSECODE_TABLE = "Expense_Code";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string EXPENSENAME_FIELD = "ExpenseName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string EXPENSETYPE_FIELD = "ExpenseType";

		public const string EXPENSERATE_FIELD = "ExpenseRate";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ExpenseCodeTable => base.Tables["Expense_Code"];

		public ExpenseCodeData()
		{
			BuildDataTables();
		}

		public ExpenseCodeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Expense_Code");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ExpenseID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ExpenseName", typeof(string)).AllowDBNull = false;
			columns.Add("AccountID", typeof(string)).AllowDBNull = false;
			columns.Add("ExpenseType", typeof(short)).AllowDBNull = false;
			columns.Add("ExpenseRate", typeof(decimal));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
