using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProjectSubcontractPOData : DataSet
	{
		public const string PROJECTSUBCONTRACTPO_TABLE = "Project_Subcontract_PO";

		public const string PROJECTSUBCONTRACTPODETAIL_TABLE = "Project_Subcontract_PO_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string ISIMPORT_FIELD = "IsImport";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string BUYERID_FIELD = "BuyerID";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string VENDORADDRESS_FIELD = "VendorAddress";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE_FIELD2 = "Reference2";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TOTAL_FIELD = "Total";

		public const string DUEDATE_FIELD = "DueDate";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string PORTLOADING_FIELD = "PortLoading";

		public const string PORTDESTINATION_FIELD = "PortDestination";

		public const string ETA_FIELD = "ETA";

		public const string ETD_FIELD = "ETD";

		public const string INCOID_FIELD = "INCOID";

		public const string BOLNO_FIELD = "BOLNo";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string PRICEINCLUDETAX_FIELD = "PriceIncludeTax";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string QUANTITYRECEIVED_FIELD = "QuantityReceived";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string REMARKS_FIELD = "Remarks";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string ISSOURCEDROW_FIELD = "IsSourcedRow";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public DataTable ProjectSubcontractPOTable => base.Tables["Project_Subcontract_PO"];

		public DataTable ProjectSubcontractPODetailTable => base.Tables["Project_Subcontract_PO_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public ProjectSubcontractPOData()
		{
			BuildDataTables();
		}

		public ProjectSubcontractPOData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Project_Subcontract_PO");
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
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("BuyerID", typeof(string));
			columns.Add("IsImport", typeof(bool)).DefaultValue = false;
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("PurchaseFlow", typeof(byte));
			columns.Add("VendorAddress", typeof(string));
			columns.Add("Status", typeof(byte)).DefaultValue = 0;
			columns.Add("CurrencyID", typeof(string));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("PortLoading", typeof(string));
			columns.Add("PortDestination", typeof(string));
			columns.Add("ETA", typeof(DateTime));
			columns.Add("ETD", typeof(DateTime));
			columns.Add("INCOID", typeof(string));
			columns.Add("BOLNo", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxAmountFC", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Project_Subcontract_PO_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 1;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float)).DefaultValue = 1;
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("ItemType", typeof(byte));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(short));
			columns.Add("IsSourcedRow", typeof(bool));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("JobID", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
