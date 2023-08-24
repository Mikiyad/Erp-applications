using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DestinationData : DataSet
	{
		public const string DESTINATION_TABLE = "Destination";

		public const string DESTINATIONID_FIELD = "DestinationID";

		public const string DESTINATIONNAME_FIELD = "DestinationName";

		public const string TICKETFIXEDAMOUNT_FIELD = "TicketFixedAmount";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable DestinationTable => base.Tables["Destination"];

		public DestinationData()
		{
			BuildDataTables();
		}

		public DestinationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Destination");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("DestinationID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("DestinationName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("TicketFixedAmount", typeof(decimal));
			columns.Add("AccountID", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
