using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PhysicalStockEntryData : DataSet
	{
		public const string DOCNUMBER_FIELD = "DocNumber";

		public const string TRANSACTIONDATE_FIELD = "PurchaseDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string PHYSICALSTOCKENTRY_TABLE = "Physical_Stock_Entry";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ITEMID_FIELD = "ItemID";

		public const string DESCRIPTION_FIELD = "ItemDescription";

		public const string ITEMUNIT_FIELD = "Unit";

		public const string QUANTITY_FIELD = "Qty";

		public const string LOCATION_FIELD = "LocationID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PHYSICALSTOCKENTRYDETAIL_TABLE = "Physical_Stock_Entry_Detail";

		public DataTable PhysicalEStockEntryTable => base.Tables["Physical_Stock_Entry"];

		public DataTable PhysicalEStockEntryDetailTable => base.Tables["Physical_Stock_Entry_Detail"];

		public PhysicalStockEntryData()
		{
			BuildDataTables();
		}

		public PhysicalStockEntryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Physical_Stock_Entry");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("DocNumber", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PurchaseDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("LocationID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Physical_Stock_Entry_Detail");
			columns = dataTable.Columns;
			columns.Add("DocNumber", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("ItemID", typeof(string));
			columns.Add("Unit", typeof(string));
			columns.Add("Qty", typeof(decimal));
			columns.Add("ItemDescription", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("PurchaseDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
