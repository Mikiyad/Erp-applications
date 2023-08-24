using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CasePartyData : DataSet
	{
		public const string CASEPARTY_TABLE = "Case_Party";

		public const string CASEPARTYID_FIELD = "CasePartyID";

		public const string CASEPARTYNAME_FIELD = "CasePartyName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CasePartyTable => base.Tables["Case_Party"];

		public CasePartyData()
		{
			BuildDataTables();
		}

		public CasePartyData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Case_Party");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CasePartyID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CasePartyName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
