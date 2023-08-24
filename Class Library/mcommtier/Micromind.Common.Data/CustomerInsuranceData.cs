using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomerInsuranceData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string DOCUMENTNUMBER_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string INSURANCEID_FIELD = "InsuranceID";

		public const string INSSTATUS_FIELD = "InsStatus";

		public const string INSAPPLICATIONDATE_FIELD = "InsApplicationDate";

		public const string INSEXPIRYDATE_FIELD = "InsExpiryDate";

		public const string INSREQUESTEDAMOUNT_FIELD = "InsRequestedAmount";

		public const string INSAPPROVEDAMOUNT_FIELD = "InsApprovedAmount";

		public const string INSPOLICYNUMBER_FIELD = "InsPolicyNumber";

		public const string INSREMARKS_FIELD = "InsRemarks";

		public const string INSRATING_FIELD = "InsRating";

		public const string INSEFFECTIVEDATE_FIELD = "InsEffectiveDate";

		public const string INSVALIDTO_FIELD = "InsValidTo";

		public const string CUSTOMERINSURANCE_TABLE = "Customer_Insurance";

		public const string INSPROVIDER_FIELD = "InsProvider";

		public const string REVIEWDATE_FIELD = "ReviewDate";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		private string customerID;

		public DataTable CustomerInsuranceTable => base.Tables["Customer_Insurance"];

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

		public CustomerInsuranceData()
		{
			BuildDataTables();
		}

		public CustomerInsuranceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Customer_Insurance");
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
			columns.Add("InsPolicyNumber", typeof(string));
			columns.Add("InsuranceID", typeof(string));
			columns.Add("InsProvider", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("InsApplicationDate", typeof(DateTime));
			columns.Add("ReviewDate", typeof(DateTime));
			columns.Add("InsApprovedAmount", typeof(decimal));
			columns.Add("InsRating", typeof(byte));
			columns.Add("InsRemarks", typeof(string));
			columns.Add("InsRequestedAmount", typeof(decimal));
			columns.Add("InsEffectiveDate", typeof(DateTime));
			columns.Add("InsValidTo", typeof(DateTime));
			columns.Add("InsStatus", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
