using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CostCategoryData : DataSet
	{
		public const string COSTCATEGORY_TABLE = "Job_Cost_Category";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string COSTCATEGORYNAME_FIELD = "CostCategoryName";

		public const string COSTCATEGORYDESC_FIELD = "Description";

		public const string COSTTYPEID_FIELD = "CostTypeID";

		public const string PARENTCOSTCATEGORY_FIELD = "ParentCostCategoryID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CostCategoryTable => base.Tables["Job_Cost_Category"];

		public CostCategoryData()
		{
			BuildDataTables();
		}

		public CostCategoryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Cost_Category");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CostCategoryID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CostCategoryName", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("CostTypeID", typeof(byte));
			columns.Add("ParentCostCategoryID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
