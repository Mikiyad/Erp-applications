using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyIncomeCodeData : DataSet
	{
		public const string PROPERTYINCOMECODE_TABLE = "PropertyIncome_Code";

		public const string INCOMEID_FIELD = "IncomeID";

		public const string INCOMENAME_FIELD = "IncomeName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string INCOMETYPE_FIELD = "IncomeType";

		public const string INCOMERATE_FIELD = "IncomeRate";

		public const string INACTIVE_FIELD = "Inactive";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PropertyIncomeCodeTable => base.Tables["PropertyIncome_Code"];

		public PropertyIncomeCodeData()
		{
			BuildDataTables();
		}

		public PropertyIncomeCodeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("PropertyIncome_Code");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("IncomeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("IncomeName", typeof(string)).AllowDBNull = false;
			columns.Add("AccountID", typeof(string)).AllowDBNull = false;
			columns.Add("IncomeType", typeof(short)).AllowDBNull = false;
			columns.Add("IncomeRate", typeof(decimal));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Description", typeof(string));
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
