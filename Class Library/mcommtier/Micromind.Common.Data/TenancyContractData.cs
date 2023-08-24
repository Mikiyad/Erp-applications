using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TenancyContractData : DataSet
	{
		public const string TENANCYCONTRACT_TABLE = "Tenancy_Contract";

		public const string CONTRACTID_FIELD = "ContractID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LANDLORD_FIELD = "Landlord";

		public const string TENANT_FIELD = "Tenant";

		public const string CONTACTID_FIELD = "ContactID";

		public const string LOCATION_FIELD = "Location";

		public const string STATUS_FIELD = "Status";

		public const string INSTALLMENTS_FIELD = "Installments";

		public const string CONTRACTAMOUNT_FIELD = "ContractAmount";

		public const string ISSUEDATE_FIELD = "IssueDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TenancyContractTable => base.Tables["Tenancy_Contract"];

		public TenancyContractData()
		{
			BuildDataTables();
		}

		public TenancyContractData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Tenancy_Contract");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ContractID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Landlord", typeof(string));
			columns.Add("Tenant", typeof(string));
			columns.Add("ContactID", typeof(string));
			columns.Add("Location", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("Installments", typeof(byte));
			columns.Add("ContractAmount", typeof(decimal));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("Description", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
