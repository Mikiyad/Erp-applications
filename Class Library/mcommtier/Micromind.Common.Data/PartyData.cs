using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PartyData : DataSet
	{
		public const string CUSTOMER_VENDOR_LINK_TABLE = "Customer_Vendor_Link";

		public const string CUSTOMER_VENDOR_LINK_DETAIL_TABLE = "Customer_Vendor_Link_Detail";

		public const string PARTYID_FIELD = "PartyID";

		public const string PARTYNAME_FIELD = "PartyName";

		public const string INACTIVE_FIELD = "Inactive";

		public const string NOTE_FIELD = "Note";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ACCOUNTNAME_FIELD = "AccountName";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CustomerVendorLinkTable => base.Tables["Customer_Vendor_Link"];

		public DataTable CustomerVendorLinkDetailTable => base.Tables["Customer_Vendor_Link_Detail"];

		public PartyData()
		{
			BuildDataTables();
		}

		public PartyData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Customer_Vendor_Link");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PartyID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PartyName", typeof(string)).AllowDBNull = false;
			columns.Add("Inactive", typeof(bool));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Customer_Vendor_Link_Detail");
			columns = dataTable.Columns;
			columns.Add("PartyID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("EntityType", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
