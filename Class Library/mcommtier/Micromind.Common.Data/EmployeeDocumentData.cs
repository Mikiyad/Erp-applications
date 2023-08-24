using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeDocumentData : DataSet
	{
		public const string EMPLOYEEDOCUMENT_TABLE = "Employee_Document";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string DOCUMENTNUMBER_FIELD = "DocumentNumber";

		public const string DOCUMENTTYPEID_FIELD = "DocumentTypeID";

		public const string ISSUEPLACE_FIELD = "IssuePlace";

		public const string ISSUEDATE_FIELD = "IssueDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string REMARKS_FIELD = "Remarks";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeDocumentTable => base.Tables["Employee_Document"];

		public EmployeeDocumentData()
		{
			BuildDataTables();
		}

		public EmployeeDocumentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Document");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmployeeID", typeof(string));
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
			columns.Add("DocumentTypeID", typeof(string)).AllowDBNull = false;
			columns.Add("IssuePlace", typeof(string));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
