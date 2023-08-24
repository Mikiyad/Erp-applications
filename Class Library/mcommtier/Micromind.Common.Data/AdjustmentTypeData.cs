using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AdjustmentTypeData : DataSet
	{
		public const string ADJUSTMENTTYPE_TABLE = "Adjustment_Type";

		public const string ADJUSTMENTTYPEID_FIELD = "TypeID";

		public const string ADJUSTMENTTYPENAME_FIELD = "TypeName";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ISNONSALSE_FIELD = "IsNonSale";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable AdjustmentTypeTable => base.Tables["Adjustment_Type"];

		public AdjustmentTypeData()
		{
			BuildDataTables();
		}

		public AdjustmentTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Adjustment_Type");
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
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsNonSale", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
