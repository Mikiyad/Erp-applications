using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CreditLimitReviewData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string DOCUMENTNUMBER_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string RATINGREMARKS_FIELD = "RatingRemarks";

		public const string RATING_FIELD = "Rating";

		public const string RATINGDATE_FIELD = "RatingDate";

		public const string RATINGBY_FIELD = "RatingBy";

		public const string ACCEPTCHECKPAYMENT_FIELD = "AcceptCheckPayment";

		public const string ACCEPTPDC_FIELD = "AcceptPDC";

		public const string CREDITLIMITTYPE_FIELD = "CreditLimitType";

		public const string CREDITAMOUNT_FIELD = "CreditAmount";

		public const string CREDITLIMITREVIEW_TABLE = "Credit_Limit_Review";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string GRACEDAYS_FIELD = "GraceDays";

		public const string CLVALIDITY_FIELD = "CLValidity";

		public const string LIMITPDCUNSECURED_FIELD = "LimitPDCUnsecured";

		public const string PDCUNSECUREDLIMITAMOUNT_FIELD = "PDCUnsecuredLimitAmount";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		private string customerID;

		public DataTable CreditLimitReviewTable => base.Tables["Credit_Limit_Review"];

		public string CustomerID
		{
			get
			{
				return customerID;
			}
			set
			{
				customerID = value;
			}
		}

		public CreditLimitReviewData()
		{
			BuildDataTables();
		}

		public CreditLimitReviewData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Credit_Limit_Review");
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
			columns.Add("RatingRemarks", typeof(string));
			columns.Add("Rating", typeof(byte));
			columns.Add("RatingDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("RatingBy", typeof(string));
			columns.Add("AcceptCheckPayment", typeof(bool));
			columns.Add("AcceptPDC", typeof(bool));
			columns.Add("CreditLimitType", typeof(byte));
			columns.Add("CreditAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("CLValidity", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("LimitPDCUnsecured", typeof(bool));
			columns.Add("PDCUnsecuredLimitAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("GraceDays", typeof(short)).DefaultValue = 0;
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			base.Tables.Add(dataTable);
		}
	}
}
