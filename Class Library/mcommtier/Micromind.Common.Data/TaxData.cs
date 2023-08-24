using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TaxData : DataSet
	{
		public const string TAX_TABLE = "Tax";

		public const string TAXCODE_FIELD = "TaxCode";

		public const string TAXNAME_FIELD = "TaxName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string SALESTAXACCOUNTID_FIELD = "SalesTaxAccountID";

		public const string PURCHASETAXACCOUNTID_FIELD = "PurchaseTaxAccountID";

		public const string TAXREVERSECHARGEACCOUNTID_FIELD = "TaxReverseChargeAccountID";

		public const string CALCULATIONMETHOD_FIELD = "CalculationMethod";

		public const string TAXID_FIELD = "TaxID";

		public const string TAXTYPE_FIELD = "TaxType";

		public const string TAXRATE_FIELD = "TaxRate";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ISPERCENT_FIELD = "IsPercent";

		public const string ISFIXED_FIELD = "IsFixed";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TaxTable => base.Tables["Tax"];

		public TaxData()
		{
			BuildDataTables();
		}

		public TaxData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Tax");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TaxCode", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TaxName", typeof(string)).AllowDBNull = false;
			columns.Add("SalesTaxAccountID", typeof(string));
			columns.Add("PurchaseTaxAccountID", typeof(string));
			columns.Add("TaxReverseChargeAccountID", typeof(string));
			columns.Add("TaxID", typeof(string));
			columns.Add("TaxType", typeof(string));
			columns.Add("CalculationMethod", typeof(short));
			columns.Add("TaxRate", typeof(decimal));
			columns.Add("Inactive", typeof(bool));
			columns.Add("IsPercent", typeof(bool));
			columns.Add("IsFixed", typeof(bool));
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
