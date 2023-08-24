using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PersonalContactData : DataSet
	{
		private const string PERSONALCONTACTID_FIELD = "PersonalContactID";

		private const string SPOUSENAME_FIELD = "SpouseName";

		private const string GENDER_FIELD = "Gender";

		private const string BIRTHDATE_FIELD = "BirthDate";

		private const string NOTES_FIELD = "Notes";

		private const string PERSONALCONTACT_TABLE = "PersonalContacts";

		public PersonalContactData()
		{
			BuildDataTables();
		}

		public PersonalContactData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("PersonalContacts");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PersonalContactID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("SpouseName", typeof(string));
			columns.Add("Gender", typeof(bool)).AllowDBNull = false;
			columns.Add("BirthDate", typeof(DateTime));
			columns.Add("Notes", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
