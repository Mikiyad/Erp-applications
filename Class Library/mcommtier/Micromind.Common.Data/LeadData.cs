using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LeadData : DataSet
	{
		public const string LEADID_FIELD = "LeadID";

		public const string LEADNAME_FIELD = "LeadName";

		public const string FOREIGNNAME_FIELD = "ForeignName";

		public const string COMPANYNAME_FIELD = "CompanyName";

		public const string FIRSTNAME_FIELD = "FirstName";

		public const string LASTNAME_FIELD = "LastName";

		public const string MIDDLENAME_FIELD = "MiddleName";

		public const string NOTE_FIELD = "Note";

		public const string PARENTLEADID_FIELD = "ParentLeadID";

		public const string AREAID_FIELD = "AreaID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string STAGEID_FIELD = "StageID";

		public const string PRIMARYADDRESSID_FIELD = "PrimaryAddressID";

		public const string SHORTNAME_FIELD = "ShortName";

		public const string SUBAREAID_FIELD = "SubAreaID";

		public const string RATING_FIELD = "Rating";

		public const string DATEESTABLISHED_FIELD = "DateEstablished";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string CREDITREVIEWBY_FIELD = "CreditReviewBy";

		public const string CREDITREVIEWDATE_FIELD = "CreditReviewDate";

		public const string ISLEADSINCE_FIELD = "IsLeadSince";

		public const string PROFILEDETAILS_FIELD = "ProfileDetails";

		public const string LEADSOURCEID_FIELD = "LeadSourceID";

		public const string LEADSOURCENAME_FIELD = "LeadSourceName";

		public const string LEADOWNERID_FIELD = "LeadOwnerID";

		public const string INDUSTRYID_FIELD = "IndustryID";

		public const string LEADSTATUS_FIELD = "LeadStatus";

		public const string COMPANYSIZE_FIELD = "CompanySize";

		public const string EMAILOPTOUT_FIELD = "EmailOptOut";

		public const string ANNUALTURNOVER_FIELD = "AnnualTurnOver";

		public const string EMPLOYEECOUNT_FIELD = "EmployeeCount";

		public const string REFERREDBY_FIELD = "ReferredBy";

		public const string EXPECTVALUE_FIELD = "ExpectValue";

		public const string REASONID_FIELD = "ReasonID";

		public const string REMARKS_FIELD = "Remarks";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string SALESPERSONID_FIELD = "SalesPersonID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string LEADADDRESS_TABLE = "Lead_Address";

		public const string LEADCONTACT_TABLE = "Lead_Contact_Detail";

		public const string LEADACTIVITY_TABLE = "Lead_Activity_Detail";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string CONTACTID_FIELD = "ContactID";

		public const string JOBTITLE_FIELD = "JobTitle";

		public const string ACTIVITYID_FIELD = "ActivityID";

		public const string ACTIVITYNAME_FIELD = "ActivityName";

		public const string ACTIVITYTYPE_FIELD = "ActivityType";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string LEADS_TABLE = "Lead";

		public const string LEADSOURCE_TABLE = "Lead_Source";

		public const string PHOTOPATH_FIELD = "PhotoPath";

		public const string CREDITCARDID_FIELD = "CreditCardID";

		public const string LEADRELATION_REL = "LeadRel";

		public DataTable LeadTable => base.Tables["Lead"];

		public DataTable LeadAddressTable => base.Tables["Lead_Address"];

		public DataTable LeadContactTable => base.Tables["Lead_Contact_Detail"];

		public DataTable LeadActivityTable => base.Tables["Lead_Activity_Detail"];

		public LeadData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public LeadData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Lead");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LeadID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LeadName", typeof(string)).AllowDBNull = false;
			columns.Add("ForeignName", typeof(string));
			columns.Add("CompanyName", typeof(string));
			columns.Add("ContactName", typeof(string));
			columns.Add("AreaID", typeof(string));
			columns.Add("StageID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsLeadSince", typeof(DateTime));
			columns.Add("Note", typeof(string));
			columns.Add("SalesPersonID", typeof(string));
			columns.Add("ParentLeadID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("LeadSourceID", typeof(string));
			columns.Add("LeadOwnerID", typeof(string));
			columns.Add("IndustryID", typeof(string));
			columns.Add("LeadStatus", typeof(string));
			columns.Add("CompanySize", typeof(byte));
			columns.Add("EmailOptOut", typeof(bool));
			columns.Add("AnnualTurnOver", typeof(decimal));
			columns.Add("EmployeeCount", typeof(short));
			columns.Add("ReferredBy", typeof(string));
			columns.Add("ShortName", typeof(string));
			columns.Add("SubAreaID", typeof(string));
			columns.Add("Rating", typeof(byte));
			columns.Add("DateEstablished", typeof(DateTime));
			columns.Add("DivisionID", typeof(string));
			columns.Add("CreditReviewBy", typeof(string));
			columns.Add("CreditReviewDate", typeof(DateTime));
			columns.Add("ProfileDetails", typeof(string));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			columns.Add("PrimaryAddressID", typeof(string)).DefaultValue = "PRIMARY";
			columns.Add("ExpectValue", typeof(decimal));
			columns.Add("ReasonID", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			base.Tables.Add(dataTable);
			LeadAddressData leadAddressData = new LeadAddressData();
			dataTable = new DataTable("Lead_Address");
			foreach (DataColumn column in leadAddressData.LeadAddressTable.Columns)
			{
				dataTable.Columns.Add(column.ColumnName, column.DataType);
			}
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Lead_Contact_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("LeadID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn3 = columns.Add("ContactID", typeof(string));
			dataColumn3.AllowDBNull = false;
			dataColumn3.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn3
			};
			columns.Add("JobTitle", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Lead_Activity_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("LeadID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn4 = columns.Add("SysDocID", typeof(string));
			dataColumn4.AllowDBNull = false;
			dataColumn4.AutoIncrement = false;
			DataColumn dataColumn5 = columns.Add("VoucherID", typeof(string));
			dataColumn5.AllowDBNull = false;
			dataColumn5.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn5,
				dataColumn4
			};
			columns.Add("ActivityName", typeof(string));
			columns.Add("ActivityType", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Lead_Source");
			columns = dataTable.Columns;
			dataColumn = columns.Add("LeadSourceID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LeadSourceName", typeof(string));
			base.Tables.Add(dataTable);
		}

		public void CreateLeadRelation()
		{
			base.Relations.Add("LeadRel", LeadTable.Columns["LeadID"], LeadAddressTable.Columns["LeadID"]);
		}
	}
}
