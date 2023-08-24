using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class NoteData : DataSet
	{
		public const string NOTESTABLE_TABLE = "Notes";

		public const string NOTEID_FIELD = "NoteID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string NOTETEXT_FIELD = "NoteText";

		public const string TITLETEXT_FIELD = "TitleText";

		public const string NOTECOLOR_FIELD = "NoteColor";

		public const string NOTEFLAG_FIELD = "NoteFlag";

		public const string NOTEHEIGHT_FIELD = "NoteHeight";

		public const string NOTEWIDTH_FIELD = "NoteWidth";

		public const string NOTEXLOCATION_FIELD = "NoteXLocation";

		public const string NOTEYLOCATION_FIELD = "NoteYLocation";

		public const string NOTEISRIGHTTOLEFT_FIELD = "NoteIsRightToLeft";

		public const string NOTESCREENID_FIELD = "NoteScreenID";

		public const string NOTEUSERS_TABLE = "[Note Users]";

		public const string USERID_FIELD = "UserID";

		public const string REMINDERDATE_FIELD = "ReminderDate";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string ISALARM_FIELD = "IsAlarm";

		public const string NOTETYPE_FIELD = "NoteType";

		public const string REFERENCEID_FIELD = "ReferenceID";

		public DataTable NoteTable => base.Tables["Notes"];

		public DataTable NoteUsersTable => base.Tables["[Note Users]"];

		public NoteData()
		{
			BuildDataTables();
		}

		public NoteData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Notes");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("NoteID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CreatedBy", typeof(short));
			columns.Add("NoteText", typeof(string));
			columns.Add("TitleText", typeof(string));
			columns.Add("NoteColor", typeof(string));
			columns.Add("NoteHeight", typeof(ushort));
			columns.Add("NoteWidth", typeof(ushort));
			columns.Add("NoteXLocation", typeof(ushort));
			columns.Add("NoteYLocation", typeof(ushort));
			columns.Add("NoteIsRightToLeft", typeof(bool));
			columns.Add("NoteScreenID", typeof(string));
			columns.Add("ReminderDate", typeof(DateTime));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			columns.Add("NoteType", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("ReferenceID", typeof(int));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsAlarm", typeof(bool)).DefaultValue = true;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Note Users]");
			columns = dataTable.Columns;
			columns.Add("NoteID", typeof(int));
			columns.Add("UserID", typeof(short)).AllowDBNull = false;
			columns.Add("NoteFlag", typeof(byte)).DefaultValue = (byte)0;
			base.Tables.Add(dataTable);
		}
	}
}
