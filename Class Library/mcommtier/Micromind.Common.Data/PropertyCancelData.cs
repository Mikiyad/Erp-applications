using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyCancelData : DataSet
	{
		public const string PROPERTYCANCEL_TABLE = "Property_Cancel";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string PROPERTYID_FIELD = "PropertyID";

		public const string UNITID_FIELD = "UnitID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string CONTRACTSTARTDATE_FIELD = "ContractStartDate";

		public const string CONTRACTENDDATE_FIELD = "ContractEndDate";

		public const string TOTALDAYS_FIELD = "TotalDays";

		public const string LASTSTAYDATE_FIELD = "LastStayDate";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string AGREEMENTTYPE_FIELD = "AgreementType";

		public const string AGREEMENTSTATUS_FIELD = "AgreementStatus";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string PARENTSYSDOCID_FIELD = "ParentSysDocID";

		public const string PARENTVOUCHERID_FIELD = "ParentVoucherID";

		public const string PROPERTYAGENT_FIELD = "PropertyAgentID";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PROPERTYCANCELDETAIL_TABLE = "Property_Cancel_Detail";

		public const string INCOMEID_FIELD = "IncomeID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string AMOUNT_FIELD = "Amount";

		public const string REFERENCE_FIELD = "Reference";

		public const string RATETYPE_FIELD = "RateType";

		public const string ROWINDEX1_FIELD = "RowIndex";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string PROPERTYREFUNDDETAIL_TABLE = "Property_Refund_Detail";

		public const string INCOMEIDTEXT_FIELD = "IncomeID";

		public const string PAIDAMOUNT_FIELD = "PaidAmount";

		public const string CHARGES_FIELD = "Charges";

		public const string REFUNDAMOUNT_FIELD = "RefundAmount";

		public const string ROWINDEX2_FIELD = "RowIndex";

		public DataTable PropertyCancelTable => base.Tables["Property_Cancel"];

		public DataTable PropertyRefundDetailTable => base.Tables["Property_Refund_Detail"];

		public DataTable PropertyCancelDetailTable => base.Tables["Property_Cancel_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public PropertyCancelData()
		{
			BuildDataTables();
		}

		public PropertyCancelData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_Cancel");
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
			columns.Add("LastStayDate", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("AgreementType", typeof(string));
			columns.Add("AgreementStatus", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("PropertyAgentID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("ParentSysDocID", typeof(string));
			columns.Add("ParentVoucherID", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_Cancel_Detail");
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
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_Refund_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("IncomeID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("PaidAmount", typeof(decimal));
			columns.Add("Charges", typeof(decimal));
			columns.Add("RefundAmount", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
