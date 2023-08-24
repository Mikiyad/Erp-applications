using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AccountGroupsData : DataSet
	{
		public const string GROUPID_FIELD = "GroupID";

		public const string GROUPNAME_FIELD = "GroupName";

		public const string TYPEID_FIELD = "TypeID";

		public const string PARENTID_FIELD = "ParentID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ACCOUNTGROUPS_TABLE = "Account_Group";

		public DataTable AccountGroupsTable => base.Tables["Account_Group"];

		public AccountGroupsData()
		{
			BuildDataTables();
		}

		public AccountGroupsData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Account_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("GroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("GroupName", typeof(string));
			columns.Add("TypeID", typeof(int));
			columns.Add("ParentID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
