using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CompanyDocumentData : DataSet
	{
		public const string COMPANYDOCUMENT_TABLE = "Company_Document";

		public const string SPONSORID_FIELD = "SponsorID";

		public const string DOCUMENTNUMBER_FIELD = "DocumentNumber";

		public const string DOCUMENTTYPEID_FIELD = "DocumentTypeID";

		public const string DOCUMENTNAME_FIELD = "DocumentName";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string REGISTERNUMBER_FIELD = "RegisterNumber";

		public const string FILENUMBER_FIELD = "FileNumber";

		public const string ISSUEPLACE_FIELD = "IssuePlace";

		public const string ISSUEDATE_FIELD = "IssueDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string REMARKS_FIELD = "Remarks";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CompanyDocumentTable => base.Tables["Company_Document"];

		public CompanyDocumentData()
		{
			BuildDataTables();
		}

		public CompanyDocumentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Company_Document");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("DocumentTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("DocumentNumber", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("SponsorID", typeof(string));
			columns.Add("DocumentName", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("RegisterNumber", typeof(string));
			columns.Add("FileNumber", typeof(string));
			columns.Add("IssuePlace", typeof(string));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
