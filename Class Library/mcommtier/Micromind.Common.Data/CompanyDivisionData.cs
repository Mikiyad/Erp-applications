using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CompanyDivisionData : DataSet
	{
		public const string DIVISION_TABLE = "Company_Division";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string DIVISIONNAME_FIELD = "DivisionName";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable DivisionTable => base.Tables["Company_Division"];

		public CompanyDivisionData()
		{
			BuildDataTables();
		}

		public CompanyDivisionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Company_Division");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("DivisionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("DivisionName", typeof(string)).AllowDBNull = false;
			columns.Add("CompanyID", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
