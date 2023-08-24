using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class VendorGroupData : DataSet
	{
		public const string VENDORGROUP_TABLE = "Vendor_Group";

		public const string VENDORGROUPID_FIELD = "GroupID";

		public const string VENDORGROUPNAME_FIELD = "GroupName";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable VendorGroupTable => base.Tables["Vendor_Group"];

		public VendorGroupData()
		{
			BuildDataTables();
		}

		public VendorGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Vendor_Group");
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
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
