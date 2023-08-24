using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ContactData : DataSet
	{
		public const string CONTACT_TABLE = "Contact";

		public const string CONTACTID_FIELD = "ContactID";

		public const string FIRSTNAME_FIELD = "FirstName";

		public const string MIDDLENAME_FIELD = "MiddleName";

		public const string LASTNAME_FIELD = "LastName";

		public const string JOBTITLE_FIELD = "JobTitle";

		public const string NICKNAME_FIELD = "NickName";

		public const string ADDRESS_FIELD = "Address";

		public const string CITY_FIELD = "City";

		public const string STATE_FIELD = "State";

		public const string COUNTRY_FIELD = "Country";

		public const string POSTALCODE_FIELD = "PostalCode";

		public const string DEPARTMENT_FIELD = "Department";

		public const string PHONE1_FIELD = "Phone1";

		public const string PHONE2_FIELD = "Phone2";

		public const string FAX_FIELD = "Fax";

		public const string MOBILE_FIELD = "Mobile";

		public const string EMAIL1_FIELD = "Email1";

		public const string EMAIL2_FIELD = "Email2";

		public const string WEBSITE_FIELD = "Website";

		public const string TWITTER_FIELD = "Twitter";

		public const string FACEBOOK_FIELD = "Facebook";

		public const string SKYPE_FIELD = "Skype";

		public const string LINKEDIN_FIELD = "LinkedIn";

		public const string ADDRESSPRINTFORMAT_FIELD = "AddressPrintFormat";

		public const string INACTIVE_FIELD = "Inactive";

		public const string NOTE_FIELD = "Note";

		public const string BIRTHDATE_FIELD = "BirthDate";

		public const string SPOUSENAME_FIELD = "SpouseName";

		public const string IMADDRESS_FIELD = "IMAddress";

		public const string ANNIVERSARY_FIELD = "Anniversary";

		public const string MANAGERNAME_FIELD = "ManagerName";

		public const string ASSISTANTNAME_FIELD = "AssistantName";

		public const string CHILDRENNAME_FIELD = "ChildrenName";

		public const string NATIONALITY_FIELD = "Nationality";

		public const string GENDER_FIELD = "Gender";

		public const string HOBBIES_FIELD = "Hobbies";

		public const string LANGUAGE_FIELD = "Language";

		public const string BANKNAME_FIELD = "BankName";

		public const string BANKACCOUNTNUMBER_FIELD = "BankAccountNumber";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public DataTable ContactTable => base.Tables["Contact"];

		public ContactData()
		{
			BuildDataTables();
		}

		public ContactData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Contact");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ContactID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("FirstName", typeof(string)).AllowDBNull = false;
			columns.Add("MiddleName", typeof(string));
			columns.Add("LastName", typeof(string)).AllowDBNull = false;
			columns.Add("JobTitle", typeof(string));
			columns.Add("NickName", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Address", typeof(string));
			columns.Add("City", typeof(string));
			columns.Add("State", typeof(string));
			columns.Add("Country", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("Department", typeof(string));
			columns.Add("Phone1", typeof(string));
			columns.Add("Phone2", typeof(string));
			columns.Add("Fax", typeof(string));
			columns.Add("Mobile", typeof(string));
			columns.Add("Email1", typeof(string));
			columns.Add("Email2", typeof(string));
			columns.Add("Website", typeof(string));
			columns.Add("Facebook", typeof(string));
			columns.Add("Twitter", typeof(string));
			columns.Add("Skype", typeof(string));
			columns.Add("LinkedIn", typeof(string));
			columns.Add("AddressPrintFormat", typeof(string));
			columns.Add("BirthDate", typeof(DateTime));
			columns.Add("SpouseName", typeof(string));
			columns.Add("IMAddress", typeof(string));
			columns.Add("Anniversary", typeof(DateTime));
			columns.Add("ManagerName", typeof(string));
			columns.Add("AssistantName", typeof(string));
			columns.Add("ChildrenName", typeof(string));
			columns.Add("Nationality", typeof(string));
			columns.Add("Gender", typeof(string));
			columns.Add("Hobbies", typeof(string));
			columns.Add("Language", typeof(string));
			columns.Add("BankName", typeof(string));
			columns.Add("BankAccountNumber", typeof(string));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
