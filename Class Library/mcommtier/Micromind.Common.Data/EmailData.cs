using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmailData : DataSet
	{
		private const string EMAILID_FIELD = "EmailID";

		private const string NAME_FIELD = "Name";

		private const string ISDEFAULT_FIELD = "IsDefault";

		private const string CONTACTID_FIELD = "ContactID";

		private const string EMAIL_TABLE = "Emails";

		public EmailData()
		{
			BuildDataTables();
		}

		public EmailData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Emails");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmailID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("Name", typeof(string)).AllowDBNull = false;
			columns.Add("IsDefault", typeof(bool));
			columns.Add("ContactID", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
