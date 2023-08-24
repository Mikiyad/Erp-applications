using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class StoreProductTransferData : DataSet
	{
		public const string TRANSFERID_FIELD = "TransferID";

		public const string TRANSFERDATE_FIELD = "TransferDate";

		public const string QUANTITY_FIELD = "Quantity";

		public const string COST_FIELD = "Unit Cost";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ORIGINALSTOREID_FIELD = "OriginalStoreID";

		public const string DESTINATIONSTOREID_FIELD = "DestinationStoreID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string NUMBER_FIELD = "Number";

		public const string REFERENCE_FIELD = "Reference";

		public const string STOREPRODUCTTRANSFER_TABLE = "[Store Product Transfers]";

		public DataTable StoreProductTransferTable => base.Tables["[Store Product Transfers]"];

		public StoreProductTransferData()
		{
			BuildDataTables();
		}

		public StoreProductTransferData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Store Product Transfers]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TransferID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("TransferDate", typeof(DateTime)).AllowDBNull = false;
			columns.Add("Quantity", typeof(float)).AllowDBNull = false;
			columns.Add("ProductID", typeof(int)).AllowDBNull = false;
			columns.Add("Unit Cost", typeof(decimal)).AllowDBNull = false;
			columns.Add("OriginalStoreID", typeof(int)).AllowDBNull = false;
			columns.Add("DestinationStoreID", typeof(int)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("Number", typeof(string));
			columns.Add("Reference", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
