using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FixedAssetDepData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string YEARVALUE_FIELD = "YearValue";

		public const string CURRENTVALUE_FIELD = "CurrentValue";

		public const string MONTH_FIELD = "Month";

		public const string YEAR_FIELD = "Year";

		public const string FIXEDASSETDEP_TABLE = "FixedAsset_Dep";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string FIXEDASSETID_FIELD = "FixedAssetID";

		public const string DEPAMOUNT_FIELD = "DepAmount";

		public const string ASSETACCOUNTID_FIELD = "AssetAccountID";

		public const string DEPACCOUNTID_FIELD = "DepAccountID";

		public const string FIXEDASSETDEPDETAIL_TABLE = "FixedAsset_Dep_Detail";

		public DataTable FixedAssetDepTable => base.Tables["FixedAsset_Dep"];

		public DataTable FixedAssetDepDetailsTable => base.Tables["FixedAsset_Dep_Detail"];

		public FixedAssetDepData()
		{
			BuildDataTables();
		}

		public FixedAssetDepData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("FixedAsset_Dep");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Year", typeof(int));
			columns.Add("Month", typeof(int));
			columns.Add("Description", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("FixedAsset_Dep_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("FixedAssetID", typeof(string));
			columns.Add("DepAmount", typeof(decimal));
			columns.Add("YearValue", typeof(decimal));
			columns.Add("CurrentValue", typeof(decimal));
			columns.Add("AssetAccountID", typeof(string));
			columns.Add("Year", typeof(int));
			columns.Add("Month", typeof(int));
			columns.Add("DepAccountID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Description", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
