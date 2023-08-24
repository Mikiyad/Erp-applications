using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomListData : DataSet
	{
		public const string LISTCODE_FIELD = "ListCode";

		public const string LISTNAME_FIELD = "ListName";

		public const string ITEMCODE_FIELD = "ItemCode";

		public const string ITEMNAME_FIELD = "ItemName";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CUSTOMLIST_TABLE = "Custom_List";

		public const string CUSTOMLISTITEMS_TABLE = "Custom_List_Items";

		public DataTable CustomListTable => base.Tables["Custom_List"];

		public DataTable CustomListItemsTable => base.Tables["Custom_List_Items"];

		public CustomListData()
		{
			BuildDataTables();
		}

		public CustomListData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Custom_List");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ListCode", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ListName", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Custom_List_Items");
			columns = dataTable.Columns;
			dataColumn = columns.Add("ListCode", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("ItemCode", typeof(string));
			columns.Add("ItemName", typeof(string));
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			base.Tables.Add(dataTable);
		}
	}
}
