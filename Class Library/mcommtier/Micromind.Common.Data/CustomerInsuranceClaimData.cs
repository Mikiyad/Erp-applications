using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomerInsuranceClaimData : DataSet
	{
		public const string CUSTOMERINSURANCECLAIM_TABLE = "Customer_Insurance_Claim";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string INSPROVIDERID_FIELD = "InsProviderID";

		public const string INSAPPLICATIONDATE_FIELD = "InsApplicationDate";

		public const string INSPAYABLEAMOUNT_FIELD = "InsPayableAmount";

		public const string INSEFFECTIVEDATE_FIELD = "InsEffectiveDate";

		public const string INSREMARKS_FIELD = "InsRemarks";

		public const string INSPOLICYNUMBER_FIELD = "InsPolicyNumber";

		public const string INSAPPROVEDAMOUNT_FIELD = "InsApprovedAmount";

		public const string INSURANCEID_FIELD = "InsuranceID";

		public const string INSEXPIRYDATE_FIELD = "InsExpiryDate";

		public const string CLAIMAMOUNT_FIELD = "ClaimAmount";

		public const string PAIDAMOUNT_FIELD = "PaidAmount";

		public const string REASON_FIELD = "Reason";

		public const string CUSTOMERINSREMARKS_FIELD = "CustomerInsRemarks";

		public const string PAIDDATE_FIELD = "PaidDate";

		public const string CUSTOMERINSSTATUS_FIELD = "CustomerInsStatus";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CustomerInsuranceClaimTable => base.Tables["Customer_Insurance_Claim"];

		public CustomerInsuranceClaimData()
		{
			BuildDataTables();
		}

		public CustomerInsuranceClaimData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Customer_Insurance_Claim");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("CustomerID", typeof(string));
			columns.Add("InsProviderID", typeof(string));
			columns.Add("InsApplicationDate", typeof(DateTime));
			columns.Add("InsPayableAmount", typeof(decimal));
			columns.Add("InsEffectiveDate", typeof(DateTime));
			columns.Add("InsRemarks", typeof(string));
			columns.Add("InsPolicyNumber", typeof(string));
			columns.Add("InsApprovedAmount", typeof(decimal));
			columns.Add("InsuranceID", typeof(string));
			columns.Add("InsExpiryDate", typeof(DateTime));
			columns.Add("ClaimAmount", typeof(decimal));
			columns.Add("PaidAmount", typeof(decimal));
			columns.Add("Reason", typeof(string));
			columns.Add("CustomerInsRemarks", typeof(string));
			columns.Add("PaidDate", typeof(DateTime));
			columns.Add("CustomerInsStatus", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
