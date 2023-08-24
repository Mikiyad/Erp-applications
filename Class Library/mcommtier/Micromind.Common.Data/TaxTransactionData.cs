using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TaxTransactionData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TAXLEVEL_FIELD = "TaxLevel";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXITEMID_FIELD = "TaxItemID";

		public const string TAXITEMNAME_FIELD = "TaxItemName";

		public const string TAXRATE_FIELD = "TaxRate";

		public const string CALCULATIONMETHOD_FIELD = "CalculationMethod";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string ORDERINDEX_FIELD = "OrderIndex";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TAXDETAIL_TABLE = "Tax_Detail";

		public DataTable TaxDetailTable => base.Tables["Tax_Detail"];

		public TaxTransactionData()
		{
			BuildDataTables();
		}

		public TaxTransactionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Tax_Detail");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("TaxLevel", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("CalculationMethod", typeof(string));
			columns.Add("TaxItemName", typeof(string));
			columns.Add("TaxItemID", typeof(string));
			columns.Add("TaxRate", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("OrderIndex", typeof(short));
			columns.Add("RowIndex", typeof(short));
			columns.Add("AccountID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
