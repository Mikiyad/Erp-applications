using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class GarmentRentalData : DataSet
	{
		public const string CONSIGN_OUT_LOCATIONID = "CON_OUT";

		public const string GARMENTRENTAL_TABLE = "Garment_Rental";

		public const string GARMENTRENTALDETAIL_TABLE = "Garment_Rental_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PACKAGEID_FIELD = "PackageID";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string CHARGES_FIELD = "Charges";

		public const string TOTAL_FIELD = "Total";

		public const string EXPRETURNDATE_FIELD = "ExpReturnDate";

		public const string OUTDATE_FIELD = "OutDate";

		public const string CASHAMOUNT_FIELD = "CashAmount";

		public const string CARDAMOUNT_FIELD = "CardAmount";

		public const string AMOUNTPAID_FIELD = "AmountPaid";

		public const string BALANCE_FIELD = "Balance";

		public const string CASHACCOUNTID_FIELD = "CashAccountID";

		public const string CARDACCOUNTID_FIELD = "CardAccountID";

		public const string RECEIPTVOUCHERID_FIELD = "ReceiptVoucherID";

		public const string RECEIPTVOUCHERAMOUNT_FIELD = "ReceiptVoucherAmount";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string QUANTITYSETTLED_FIELD = "QuantitySettled";

		public const string QUANTITYRETURNED_FIELD = "QuantityReturned";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string CONSIGNLOCATIONID_FIELD = "ConsignLocationID";

		public const string AMOUNT_FIELD = "Amount";

		public DataTable GarmentRentalTable => base.Tables["Garment_Rental"];

		public DataTable GarmentRentalDetailTable => base.Tables["Garment_Rental_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public GarmentRentalData()
		{
			BuildDataTables();
		}

		public GarmentRentalData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Garment_Rental");
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
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PackageID", typeof(string));
			columns.Add("RegisterID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("Charges", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("CashAmount", typeof(decimal));
			columns.Add("CardAmount", typeof(decimal));
			columns.Add("AmountPaid", typeof(decimal));
			columns.Add("Balance", typeof(decimal));
			columns.Add("CashAccountID", typeof(string));
			columns.Add("CardAccountID", typeof(string));
			columns.Add("ReceiptVoucherID", typeof(string));
			columns.Add("ReceiptVoucherAmount", typeof(decimal));
			columns.Add("ExpReturnDate", typeof(DateTime));
			columns.Add("OutDate", typeof(DateTime));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Garment_Rental_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("QuantitySettled", typeof(float)).DefaultValue = 0;
			columns.Add("QuantityReturned", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("ConsignLocationID", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Discount", typeof(decimal)).DefaultValue = 0;
			columns.Add("Amount", typeof(decimal));
			columns.Add("PackageID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
