using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalesManTargetData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string FROMDATE_FIELD = "FromDate";

		public const string TODATE_FIELD = "ToDate";

		public const string MONTH_FIELD = "Month";

		public const string TARGETTYPE_FIELD = "TargetType";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string SALESMANGROUP_FIELD = "SalesManGroupID";

		public const string SALESMAN_FIELD = "SalesManID";

		public const string PRODUCTCLASS_FIELD = "ProductClassID";

		public const string PRODUCTCATEGORY_FIELD = "ProductCategoryID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string AMOUNT_FIELD = "Amount";

		public const string COMMISSIONPERCENT_FIELD = "CommissionPercent";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESMANTARGET_TABLE = "Sales_Man_Target";

		public const string SALESMANTARGETDETAIL_TABLE = "Sales_Man_Target_Detail";

		public DataTable SalesManTargetTable => base.Tables["Sales_Man_Target"];

		public DataTable SalesManTargetDetailTable => base.Tables["Sales_Man_Target_Detail"];

		public SalesManTargetData()
		{
			BuildDataTables();
		}

		public SalesManTargetData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Sales_Man_Target");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			DataColumn dataColumn2 = columns.Add("SysDocID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("FromDate", typeof(DateTime));
			columns.Add("ToDate", typeof(DateTime));
			columns.Add("Month", typeof(short));
			columns.Add("TargetType", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Sales_Man_Target_Detail");
			columns = dataTable.Columns;
			columns.Add("VoucherID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("SalesManGroupID", typeof(string));
			columns.Add("SalesManID", typeof(string));
			columns.Add("ProductClassID", typeof(string));
			columns.Add("ProductCategoryID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("CommissionPercent", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
