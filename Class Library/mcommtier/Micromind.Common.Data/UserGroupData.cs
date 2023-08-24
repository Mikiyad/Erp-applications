using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class UserGroupData : DataSet
	{
		public const string USERGROUP_TABLE = "User_Group";

		public const string USERGROUPID_FIELD = "GroupID";

		public const string USERGROUPNAME_FIELD = "GroupName";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string USERGROUPDETAIL_TABLE = "User_Group_Detail";

		public const string USERID_FIELD = "UserID";

		public DataTable UserGroupTable => base.Tables["User_Group"];

		public DataTable UserGroupDetailTable => base.Tables["User_Group_Detail"];

		public UserGroupData()
		{
			BuildDataTables();
		}

		public UserGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("User_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("GroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("GroupName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("User_Group_Detail");
			columns = dataTable.Columns;
			columns.Add("GroupID", typeof(string));
			columns.Add("UserID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
