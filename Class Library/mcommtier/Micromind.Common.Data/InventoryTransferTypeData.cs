using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class InventoryTransferTypeData : DataSet
	{
		public const string INVENTORYTRANSFERTYPE_TABLE = "Inventory_Transfer_Type";

		public const string INVENTORYTRANSFERTYPEID_FIELD = "TypeID";

		public const string INVENTORYTRANSFERTYPENAME_FIELD = "TypeName";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable InventoryTransferTypeTable => base.Tables["Inventory_Transfer_Type"];

		public InventoryTransferTypeData()
		{
			BuildDataTables();
		}

		public InventoryTransferTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Inventory_Transfer_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TypeName", typeof(string)).AllowDBNull = false;
			columns.Add("AccountID", typeof(string));
			columns.Add("LocationID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
