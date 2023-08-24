using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DeductionData : DataSet
	{
		public const string DEDUCTION_TABLE = "Deduction";

		public const string DEDUCTIONID_FIELD = "DeductionID";

		public const string DEDUCTIONNAME_FIELD = "DeductionName";

		public const string NOTE_FIELD = "Note";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable DeductionTable => base.Tables["Deduction"];

		public DeductionData()
		{
			BuildDataTables();
		}

		public DeductionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Deduction");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("DeductionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("DeductionName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("AccountID", typeof(string)).AllowDBNull = false;
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
