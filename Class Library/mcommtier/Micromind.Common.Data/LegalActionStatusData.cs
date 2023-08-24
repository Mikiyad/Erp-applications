using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LegalActionStatusData : DataSet
	{
		public const string LEGALACTIONSTATUSID_FIELD = "LegalActionStatusID";

		public const string LEGALACTIONSTATUSNAME_FIELD = "LegalActionStatusName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string ISFINALIZED_FIELD = "IsFinalized";

		public const string NOTE_FIELD = "Note";

		public const string LEGALACTIONSTATUS_TABLE = "Legal_Action_Status";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable LegalActionStatusTable => base.Tables["Legal_Action_Status"];

		public LegalActionStatusData()
		{
			BuildDataTables();
		}

		public LegalActionStatusData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Legal_Action_Status");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LegalActionStatusID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LegalActionStatusName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsFinalized", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
