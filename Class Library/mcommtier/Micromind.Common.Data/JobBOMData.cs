using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobBOMData : DataSet
	{
		public const string JOBBOM_TABLE = "Job_BOM";

		public const string JOBBOMDETAIL_TABLE = "Job_BOM_Detail";

		public const string JOBBOMID_FIELD = "JobBOMID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string BOMNAME_FIELD = "BOMName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string LABOURCOST_FIELD = "LabourCost";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string QUANTITY_FIELD = "Quantity";

		public DataTable JobBOMTable => base.Tables["Job_BOM"];

		public DataTable JobBOMDetailTable => base.Tables["Job_BOM_Detail"];

		public JobBOMData()
		{
			BuildDataTables();
		}

		public JobBOMData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_BOM");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("JobBOMID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("BOMName", typeof(string));
			columns.Add("IsInactive", typeof(bool));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_BOM_Detail");
			columns = dataTable.Columns;
			columns.Add("JobBOMID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("ItemType", typeof(int));
			columns.Add("Cost", typeof(float));
			columns.Add("LabourCost", typeof(float));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
