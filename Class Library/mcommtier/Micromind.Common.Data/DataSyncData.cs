using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DataSyncData : DataSet
	{
		public const string DATASYNC_TABLE = "Data_Sync";

		public const string DATASYNCID_FIELD = "Code";

		public const string DATASYNCNAME_FIELD = "Name";

		public const string NOTE_FIELD = "Note";

		public const string TYPE_FIELD = "Type";

		public const string DATABASENAME_FIELD = "DatabaseName";

		public const string SERVERID_FIELD = "ServerID";

		public const string USERID_FIELD = "UserID";

		public const string PASSWORD_FIELD = "Password";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string DATASYNCDETAIL_TABLE = "Data_Sync_Detail";

		public const string SYNCTYPE_FIELD = "SyncType";

		public const string RECORDTYPE_FIELD = "RecordType";

		public const string NAME_FIELD = "Name";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string DEFAULTSYSDOCID_FIELD = "DefaultSysDocID";

		public const string DEFAULTREGISTERID_FIELD = "DefaultRegisterID";

		public const string LASTSYNCTIME_FIELD = "LastSyncTime";

		public const string SYNCINTERVAL_FIELD = "SyncInterval";

		public const string STATUS_FIELD = "Status";

		public DataTable DataSyncTable => base.Tables["Data_Sync"];

		public DataTable DataSyncTableDetail => base.Tables["Data_Sync_Detail"];

		public DataSyncData()
		{
			BuildDataTables();
		}

		public DataSyncData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Data_Sync");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("Code", typeof(string));
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Name", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Type", typeof(short));
			columns.Add("DatabaseName", typeof(string));
			columns.Add("ServerID", typeof(string));
			columns.Add("UserID", typeof(string));
			columns.Add("Password", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Data_Sync_Detail");
			columns = dataTable.Columns;
			columns.Add("Code", typeof(string));
			columns.Add("SyncType", typeof(short));
			columns.Add("RecordType", typeof(short));
			columns.Add("Name", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("DefaultSysDocID", typeof(string));
			columns.Add("DefaultRegisterID", typeof(string));
			columns.Add("LastSyncTime", typeof(DateTime));
			columns.Add("SyncInterval", typeof(short));
			columns.Add("Status", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
