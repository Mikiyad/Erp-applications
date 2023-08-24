using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BankFacilityGroupData : DataSet
	{
		public const string BANKFACILITYGROUP_TABLE = "Bank_Facility_Group";

		public const string BANKFACILITYGROUPCONTACT_TABLE = "Bank_Facility_Group_Contacts";

		public const string BANKFACILITY_TABLE = "Bank_Facility";

		public const string BANKFACILITYGROUPID_FIELD = "GroupID";

		public const string BANKFACILITYGROUPNAME_FIELD = "GroupName";

		public const string BANKID_FIELD = "BankID";

		public const string TOTALLIMIT_FIELD = "TotalLimit";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string RENEWDATE_FIELD = "RenewDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string STATUS_FIELD = "Status";

		public const string ALIASNAME_FIELD = "Alias";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string CONTACTNUMBER_FIELD = "ContactNumber";

		public const string NOTE_FIELD = "Note";

		public const string CONTACTID_FIELD = "ContactID";

		public const string JOBTITLE_FIELD = "JobTitle";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string BANKFACILITYID_FIELD = "FacilityID";

		public const string BANKFACILITYNAME_FIELD = "FacilityName";

		public const string FACILITYTYPE_FIELD = "FacilityType";

		public const string LIMITAMOUNT_FIELD = "LimitAmount";

		public const string PAYABLEACCOUNT_FIELD = "PayableAccountID";

		public const string CURRENTACCOUNT_FIELD = "CurrentAccountID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BankFacilityGroupTable => base.Tables["Bank_Facility_Group"];

		public DataTable BankFacilityGroupContactTable => base.Tables["Bank_Facility_Group_Contacts"];

		public BankFacilityGroupData()
		{
			BuildDataTables();
		}

		public BankFacilityGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bank_Facility_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("GroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("GroupName", typeof(string)).AllowDBNull = false;
			columns.Add("BankID", typeof(string));
			columns.Add("Alias", typeof(string));
			columns.Add("ContactName", typeof(string));
			columns.Add("ContactNumber", typeof(string));
			columns.Add("TotalLimit", typeof(decimal));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("RenewDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Bank_Facility_Group_Contacts");
			columns = dataTable.Columns;
			dataColumn = columns.Add("GroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("ContactID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("JobTitle", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
