using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SponsorData : DataSet
	{
		public const string SPONSOR_TABLE = "Sponsor";

		public const string SPONSORID_FIELD = "SponsorID";

		public const string SPONSORNAME_FIELD = "SponsorName";

		public const string MOLID_FIELD = "MOLId";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable SponsorTable => base.Tables["Sponsor"];

		public SponsorData()
		{
			BuildDataTables();
		}

		public SponsorData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Sponsor");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SponsorID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SponsorName", typeof(string)).AllowDBNull = false;
			columns.Add("MOLId", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
