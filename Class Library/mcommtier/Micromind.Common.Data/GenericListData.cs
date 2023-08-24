using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class GenericListData : DataSet
	{
		public const string GENERICLIST_TABLE = "Generic_List";

		public const string GENERICLISTTYPE_FIELD = "GenericListType";

		public const string GENERICLISTID_FIELD = "GenericListID";

		public const string GENERICLISTNAME_FIELD = "GenericListName";

		public const string GENERICLISTSHORTNAME_FIELD = "GenericListShortName";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable GenericListTable => base.Tables["Generic_List"];

		public GenericListData()
		{
			BuildDataTables();
		}

		public GenericListData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Generic_List");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("GenericListID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("GenericListType", typeof(byte));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("GenericListName", typeof(string)).AllowDBNull = false;
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			columns.Add("GenericListShortName", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
