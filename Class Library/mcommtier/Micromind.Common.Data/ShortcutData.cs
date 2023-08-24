using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ShortcutData : DataSet
	{
		public const string SHORTCUTKEY_FIELD = "ShortcutKey";

		public const string SHORTCUTTEXT_FIELD = "ShortcutText";

		public const string SHORTCUTTYPE_FIELD = "ShortcutType";

		public const string USERID_FIELD = "UserID";

		public const string SHORTCUT_TABLE = "Shortcut";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ShortcutTable => base.Tables["Shortcut"];

		public ShortcutData()
		{
			BuildDataTables();
		}

		public ShortcutData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Shortcut");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("ShortcutKey", typeof(string)).AllowDBNull = false;
			columns.Add("ShortcutText", typeof(string));
			columns.Add("ShortcutType", typeof(byte));
			columns.Add("UserID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
