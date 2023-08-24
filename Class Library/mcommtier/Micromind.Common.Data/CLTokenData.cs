using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CLTokenData : DataSet
	{
		public const string CLTOKEN_TABLE = "CL_Token";

		public const string TOKENID_FIELD = "TokenID";

		public const string SYSTEMKEY_FIELD = "SystemKey";

		public const string TOKENNUMBER_FIELD = "TokenNumber";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string AMOUNT_FIELD = "Amount";

		public const string ISSUEDBY_FIELD = "IssuedBy";

		public const string REQUESTEDBY_FIELD = "RequestedBy";

		public const string REQUESTDATE_FIELD = "RequestDate";

		public const string ISSUEDATE_FIELD = "IssueDate";

		public const string STATUS_FIELD = "Status";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CLTokenTable => base.Tables["CL_Token"];

		public CLTokenData()
		{
			BuildDataTables();
		}

		public CLTokenData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("CL_Token");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TokenID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SystemKey", typeof(string));
			columns.Add("TokenNumber", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("IssuedBy", typeof(string));
			columns.Add("RequestedBy", typeof(string));
			columns.Add("RequestDate", typeof(DateTime));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("CustomerID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
