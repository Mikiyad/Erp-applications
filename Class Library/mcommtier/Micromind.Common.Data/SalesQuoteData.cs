using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalesQuoteData : DataSet
	{
		public const string SALESQUOTE_TABLE = "Sales_Quote";

		public const string SALESQUOTEDETAIL_TABLE = "Sales_Quote_Detail";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string PRICEINCLUDETAX_FIELD = "PriceIncludeTax";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string SHIPTOADDRESS_FIELD = "ShipToAddress";

		public const string BILLINGADDRESSID_FIELD = "BillingAddressID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TOTAL_FIELD = "Total";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string ROUNDOFF_FIELD = "RoundOff";

		public const string PREVIOUSREVISEDDATE_FIELD = "PreviousRevisedDate";

		public const string LASTREVISEDDATE_FIELD = "LastRevisedDate";

		public const string REVISIONNO_FIELD = "RevisionNo";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string REMARKS1_FIELD = "Remarks1";

		public const string REMARKS2_FIELD = "Remarks2";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string COST_FIELD = "Cost";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string TAXPERCENTAGE_FIELD = "TaxPercentage";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string REFSLNO_FIELD = "RefSlNo";

		public const string REFTEXT1_FIELD = "RefText1";

		public const string REFTEXT2_FIELD = "RefText2";

		public const string REFNUM1_FIELD = "RefNum1";

		public const string REFNUM2_FIELD = "RefNum2";

		public const string REFDATE1_FIELD = "RefDate1";

		public const string REFDATE2_FIELD = "RefDate2";

		public DataTable SalesQuoteTable => base.Tables["Sales_Quote"];

		public DataTable SalesQuoteDetailTable => base.Tables["Sales_Quote_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public SalesQuoteData()
		{
			BuildDataTables();
		}

		public SalesQuoteData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Sales_Quote");
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
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("SalesFlow", typeof(byte));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("BillingAddressID", typeof(string));
			columns.Add("ShipToAddress", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("PriceIncludeTax", typeof(bool));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("JobID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("EntityType", typeof(string));
			columns.Add("LastRevisedDate", typeof(DateTime));
			columns.Add("PreviousRevisedDate", typeof(DateTime));
			columns.Add("RoundOff", typeof(decimal));
			columns.Add("Remarks1", typeof(string));
			columns.Add("Remarks2", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Sales_Quote_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("Cost", typeof(decimal));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("TaxPercentage", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("RefSlNo", typeof(int));
			columns.Add("RefText1", typeof(string));
			columns.Add("RefText2", typeof(string));
			columns.Add("RefNum1", typeof(decimal));
			columns.Add("RefNum2", typeof(decimal));
			columns.Add("RefDate1", typeof(DateTime));
			columns.Add("RefDate2", typeof(DateTime));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
