using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class VehicleMaintenanceEntryData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string DOCID_FIELD = "VoucherID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string VEHICLENUMBER_FIELD = "VehicleNumber";

		public const string ODOMETER_FIELD = "Odometer";

		public const string SERVICETYPE_FIELD = "ServiceType";

		public const string NEXTSCHEDLUESTATUS_FIELD = "NextServiceScheduleStatus";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string SERVICEPROVIDER_FIELD = "ServiceProvider";

		public const string REQUIREDTIME_FIELD = "TimeRequired";

		public const string STATUS_FIELD = "Status";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEDOCID_FIELD = "SourceVoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string ISIMPORT_FIELD = "IsImport";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string BUYERID_FIELD = "BuyerID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string ISCASH_FIELD = "IsCash";

		public const string DUEDATE_FIELD = "DueDate";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string UNITPRICEFC_FIELD = "UnitPriceFC";

		public const string LCOST_FIELD = "LCost";

		public const string LCOSTAMOUNT_FIELD = "LCostAmount";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string ORDERVOUCHERID_FIELD = "OrderVoucherID";

		public const string ORDERSYSDOCID_FIELD = "OrderSysDocID";

		public const string PORVOUCHERID_FIELD = "PORVoucherID";

		public const string PORSYSDOCID_FIELD = "PORSysDocID";

		public const string ORDERROWINDEX_FIELD = "OrderRowIndex";

		public const string ISPORROW_FIELD = "IsPORRow";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string JOBID_FIELD = "JobID";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string PRICEINCLUDETAX_FIELD = "PriceIncludeTax";

		public const string MAINTENANCEENTRY_TABLE = "Vehicle_Maintenance_Entry";

		public const string MAINTENANCEENTRYDETAIL_TABLE = "Vehicle_Maintenance_Entry_Detail";

		public DataTable MaintenanceEntryTable => base.Tables["Vehicle_Maintenance_Entry"];

		public DataTable MaintenanceEntryDetailTable => base.Tables["Vehicle_Maintenance_Entry_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public VehicleMaintenanceEntryData()
		{
			BuildDataTables();
		}

		public VehicleMaintenanceEntryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Vehicle_Maintenance_Entry");
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
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns = dataTable.Columns;
			columns.Add("VehicleNumber", typeof(string));
			columns.Add("Odometer", typeof(string));
			columns.Add("ServiceType", typeof(string));
			columns.Add("ServiceProvider", typeof(string));
			columns.Add("Amount", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("TimeRequired", typeof(string));
			columns.Add("NextServiceScheduleStatus", typeof(bool)).DefaultValue = true;
			columns.Add("DateUpdated", typeof(DateTime));
			columns.Add("IsVoid", typeof(string));
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("BuyerID", typeof(string));
			columns.Add("PurchaseFlow", typeof(byte));
			columns.Add("IsImport", typeof(bool)).DefaultValue = false;
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TermID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("IsCash", typeof(bool));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Vehicle_Maintenance_Entry_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitPriceFC", typeof(decimal));
			columns.Add("LCost", typeof(decimal));
			columns.Add("LCostAmount", typeof(decimal));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("FactorType", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("RowSource", typeof(byte));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
