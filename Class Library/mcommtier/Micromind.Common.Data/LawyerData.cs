using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LawyerData : DataSet
	{
		public const string LAWYER_TABLE = "Lawyer";

		public const string LAWYERID_FIELD = "LawyerID";

		public const string LAWYERNAME_FIELD = "LawyerName";

		public const string SELECTIONTYPE_FIELD = "SelectionType";

		public const string PARTYID_FIELD = "PartyID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable LawyerTable => base.Tables["Lawyer"];

		public LawyerData()
		{
			BuildDataTables();
		}

		public LawyerData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Lawyer");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LawyerID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LawyerName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("SelectionType", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("PartyID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
