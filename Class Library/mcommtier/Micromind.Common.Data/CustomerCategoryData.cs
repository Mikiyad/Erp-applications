using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomerCategoryData : DataSet
	{
		public const string CUSTOMERCATEGORY_TABLE = "Customer_Category";

		public const string CUSTOMERCATEGORYDETAIL_TABLE = "Customer_Category_Detail";

		public const string CUSTOMERCATEGORYID_FIELD = "CategoryID";

		public const string CUSTOMERCATEGORYNAME_FIELD = "CategoryName";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CustomerCategoryTable => base.Tables["Customer_Category"];

		public DataTable CustomerCategoryDetailTable => base.Tables["Customer_Category_Detail"];

		public CustomerCategoryData()
		{
			BuildDataTables();
		}

		public CustomerCategoryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Customer_Category");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CategoryID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CategoryName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Customer_Category_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("CustomerID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("CategoryID", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).DefaultValue = (byte)1;
			base.Tables.Add(dataTable);
		}
	}
}
