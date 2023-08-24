using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyRentData : DataSet
	{
		public const string PROPERTYRENT_TABLE = "Property_Rent";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string PROPERTYID_FIELD = "PropertyID";

		public const string UNITID_FIELD = "UnitID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string CONTRACTSTARTDATE_FIELD = "ContractStartDate";

		public const string CONTRACTENDDATE_FIELD = "ContractEndDate";

		public const string TOTALDAYS_FIELD = "TotalDays";

		public const string NOOFINSTALLMENTS_FIELD = "NoofInstallments";

		public const string NOTE_FIELD = "Note";

		public const string INVOICENOTE_FIELD = "InvoiceNote";

		public const string STATUS_FIELD = "Status";

		public const string AGREEMENTTYPE_FIELD = "AgreementType";

		public const string AGREEMENTSTATUS_FIELD = "AgreementStatus";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string TOTAL_FIELD = "Total";

		public const string PROPERTYAGENT_FIELD = "PropertyAgentID";

		public const string PARENTSYSDOCID_FIELD = "ParentSysDocID";

		public const string PARENTVOUCHERID_FIELD = "ParentVoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string DISCOUNT_FIELD = "Discount";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PROPERTYRENTDETAIL_TABLE = "Property_Rent_Detail";

		public const string INCOMEID_FIELD = "IncomeID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string AMOUNT_FIELD = "Amount";

		public const string REFERENCE_FIELD = "Reference";

		public const string RATETYPE_FIELD = "RateType";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string ISPERIODICINVOICE_FIELD = "IsPeriodicInvoice";

		public const string INVOICESTARTDATE_FIELD = "InvoiceStartDate";

		public const string FREQUENCY_FIELD = "Frequency";

		public const string FREQUENCYCOUNT_FIELD = "FrequencyCount";

		public const string PERIODICSYSDOCID_FIELD = "PeriodicSysDocID";

		public const string PERIODICVOUCHERID_FIELD = "PeriodicVoucherID";

		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string PROCESSEDBY_FIELD = "ProcessedBy";

		public DataTable PropertyRentTable => base.Tables["Property_Rent"];

		public DataTable PropertyRentDetailTable => base.Tables["Property_Rent_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public PropertyRentData()
		{
			BuildDataTables();
		}

		public PropertyRentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_Rent");
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
			columns.Add("PropertyID", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("ContractStartDate", typeof(DateTime));
			columns.Add("ContractEndDate", typeof(DateTime));
			columns.Add("TotalDays", typeof(short));
			columns.Add("NoofInstallments", typeof(short));
			columns.Add("Status", typeof(byte));
			columns.Add("AgreementType", typeof(string));
			columns.Add("AgreementStatus", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("Total", typeof(decimal));
			columns.Add("PropertyAgentID", typeof(string));
			columns.Add("ParentSysDocID", typeof(string));
			columns.Add("ParentVoucherID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("InvoiceNote", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("IsPeriodicInvoice", typeof(bool));
			columns.Add("InvoiceStartDate", typeof(DateTime));
			columns.Add("FrequencyCount", typeof(short));
			columns.Add("Frequency", typeof(char));
			columns.Add("PeriodicSysDocID", typeof(string));
			columns.Add("PeriodicVoucherID", typeof(string));
			columns.Add("TransactionID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("ProcessedBy", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_Rent_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("IncomeID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("UnitPrice", typeof(decimal));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Discount", typeof(decimal));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
