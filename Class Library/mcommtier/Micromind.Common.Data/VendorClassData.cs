using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class VendorClassData : DataSet
	{
		public const string VENDORCLASS_TABLE = "Vendor_Class";

		public const string VENDORCLASSID_FIELD = "ClassID";

		public const string VENDORCLASSNAME_FIELD = "ClassName";

		public const string APACCOUNTID_FIELD = "APAccountID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable VendorClassTable => base.Tables["Vendor_Class"];

		public VendorClassData()
		{
			BuildDataTables();
		}

		public VendorClassData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Vendor_Class");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ClassID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ClassName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("APAccountID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
