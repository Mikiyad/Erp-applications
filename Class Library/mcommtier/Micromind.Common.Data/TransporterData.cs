using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TransporterData : DataSet
	{
		public const string TRANSPORTER_TABLE = "Transporter";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string TRANSPORTERNAME_FIELD = "TransporterName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TransporterTable => base.Tables["Transporter"];

		public TransporterData()
		{
			BuildDataTables();
		}

		public TransporterData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Transporter");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TransporterID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TransporterName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
