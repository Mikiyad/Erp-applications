using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class VisaData : DataSet
	{
		public const string VISA_TABLE = "Visa";

		public const string VISAID_FIELD = "VisaID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string SPONSORID_FIELD = "SponsorID";

		public const string VISATYPE_FIELD = "VisaType";

		public const string DAYS_FIELD = "Days";

		public const string BIRTHDATE_FIELD = "BirthDate";

		public const string CONTACTID_FIELD = "ContactID";

		public const string GENDER_FIELD = "Gender";

		public const string NATIONALITY_FIELD = "Nationality";

		public const string APPLICANTNAME_FIELD = "ApplicantName";

		public const string PASSPORTNUMBER_FIELD = "PassportNumber";

		public const string PASSPORTISSUEPLACE_FIELD = "PassportIssuePlace";

		public const string PASSPORTEXPIRYDATE_FIELD = "PassportExpiryDate";

		public const string ISSUEDATE_FIELD = "IssueDate";

		public const string VALIDITYDATE_FIELD = "ValidityDate";

		public const string ISSUEPLACE_FIELD = "IssuePlace";

		public const string ARRIVALDATE_FIELD = "ArrivalDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string DEPARTUREDATE_FIELD = "DepartureDate";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable VisaTable => base.Tables["Visa"];

		public VisaData()
		{
			BuildDataTables();
		}

		public VisaData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Visa");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VisaID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Description", typeof(string));
			columns.Add("SponsorID", typeof(string));
			columns.Add("VisaType", typeof(byte));
			columns.Add("Days", typeof(short));
			columns.Add("BirthDate", typeof(DateTime));
			columns.Add("ContactID", typeof(string));
			columns.Add("Gender", typeof(char));
			columns.Add("Nationality", typeof(string));
			columns.Add("ApplicantName", typeof(string));
			columns.Add("PassportNumber", typeof(string));
			columns.Add("PassportIssuePlace", typeof(string));
			columns.Add("PassportExpiryDate", typeof(DateTime));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("ValidityDate", typeof(DateTime));
			columns.Add("IssuePlace", typeof(string));
			columns.Add("ArrivalDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("DepartureDate", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
