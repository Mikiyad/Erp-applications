using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CotactChildrenData : DataSet
	{
		private const string CONTACTCHILDRENID_FIELD = "ContactChildrenID";

		private const string NAME_FIELD = "Name";

		private const string PERSONALCONTACTID_FIELD = "PersonalContactID";

		private const string CONTACTCHILDREN_TABLE = "ContactChildren";

		public CotactChildrenData()
		{
			BuildDataTables();
		}

		public CotactChildrenData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("ContactChildren");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ContactChildrenID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("Name", typeof(string)).AllowDBNull = false;
			columns.Add("PersonalContactID", typeof(int)).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
