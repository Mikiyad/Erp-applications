using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TradeLicenseData : DataSet
	{
		public const string TRADELICENSE_TABLE = "Trade_License";

		public const string TRADELICENSEID_FIELD = "TradeLicenseID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string SPONSOR_FIELD = "Sponsors";

		public const string CONTACTID_FIELD = "ContactID";

		public const string PARTNERS_FIELD = "Partners";

		public const string REGISTERNUMBER_FIELD = "RegisterNumber";

		public const string LEGALTYPE_FIELD = "LegalType";

		public const string ISSUEPLACE_FIELD = "IssuePlace";

		public const string ISSUEDATE_FIELD = "IssueDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string RENEWDATE_FIELD = "RenewDate";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TradeLicenseTable => base.Tables["Trade_License"];

		public TradeLicenseData()
		{
			BuildDataTables();
		}

		public TradeLicenseData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Trade_License");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TradeLicenseID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Description", typeof(string));
			columns.Add("Sponsors", typeof(string));
			columns.Add("ContactID", typeof(string));
			columns.Add("Partners", typeof(string));
			columns.Add("RegisterNumber", typeof(string));
			columns.Add("LegalType", typeof(string));
			columns.Add("IssuePlace", typeof(string));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("RenewDate", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
