using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BenefitData : DataSet
	{
		public const string BENEFIT_TABLE = "Benefit";

		public const string BENEFITID_FIELD = "BenefitID";

		public const string BENEFITNAME_FIELD = "BenefitName";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ISNONFINANCIAL_FIELD = "IsNonFinancial";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BenefitTable => base.Tables["Benefit"];

		public BenefitData()
		{
			BuildDataTables();
		}

		public BenefitData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Benefit");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BenefitID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("BenefitName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("AccountID", typeof(string)).AllowDBNull = false;
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsNonFinancial", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
