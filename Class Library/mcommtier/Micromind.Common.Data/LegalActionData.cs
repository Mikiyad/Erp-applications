using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LegalActionData : DataSet
	{
		public const string LEGALACTION_TABLE = "Legal_Actions";

		public const string LEGALCASECLIENTLIST_TABLE = "Legal_Actions_Client_List";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string ACTIONNAME_FIELD = "ActionName";

		public const string CASECLIENT1_FIELD = "Caseclient1";

		public const string CASECLIENT2_FIELD = "Caseclient2";

		public const string CASECLIENT_FIELD = "Caseclient";

		public const string CASEPARTYID_FIELD = "CasePartyID";

		public const string LAWYERID_FIELD = "LawyerID";

		public const string STATUSID_FIELD = "StatusID";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PARENTSYSDOCID_FIELD = "ParentSysDocID";

		public const string PARENTVOUCHERID_FIELD = "ParentVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string FILENO_FIELD = "FileNo";

		public const string CASETYPEID_FIELD = "CaseTypeID";

		public const string CLIENTTYPE_FIELD = "ClientType";

		public const string DATETIME_FIELD = "ActionDateTime";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable LegalActionTable => base.Tables["Legal_Actions"];

		public DataTable LegalCaseClientListTable => base.Tables["Legal_Actions_Client_List"];

		public LegalActionData()
		{
			BuildDataTables();
		}

		public LegalActionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Legal_Actions");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("ActionName", typeof(string)).AllowDBNull = false;
			columns.Add("Caseclient1", typeof(string));
			columns.Add("Caseclient2", typeof(string));
			columns.Add("CasePartyID", typeof(string));
			columns.Add("LawyerID", typeof(string));
			columns.Add("StatusID", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("ParentSysDocID", typeof(string));
			columns.Add("ParentVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("FileNo", typeof(string));
			columns.Add("CaseTypeID", typeof(string));
			columns.Add("ClientType", typeof(string));
			columns.Add("ActionDateTime", typeof(DateTime));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Legal_Actions_Client_List");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ClientType", typeof(string));
			columns.Add("Caseclient", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
