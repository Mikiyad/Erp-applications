using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LegalActivityData : DataSet
	{
		public const string LEGALACTIVITY_TABLE = "Legal_Activity";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string ACTIVITYNAME_FIELD = "ActivityName";

		public const string CASECLIENT1_FIELD = "CaseClient1";

		public const string CASECLIENT2_FIELD = "CaseClient2";

		public const string CASEPARTYID_FIELD = "CasePartyID";

		public const string LAWYERID_FIELD = "LawyerID";

		public const string STATUSID_FIELD = "StatusID";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string ACTIONAME_FIELD = "ActionName";

		public const string PARENTSYSDOCID_FIELD = "ParentSysDocID";

		public const string PARENTVOUCHERID_FIELD = "ParentVoucherID";

		public const string FILENO_FIELD = "FileNo";

		public const string CASETYPEID_FIELD = "CaseTypeID";

		public const string DATETIME_FIELD = "ActivityDateTime";

		public const string NOTE_FIELD = "Note";

		public const string CONTACTID_FIELD = "ContactID";

		public const string OWNERID_FIELD = "OwnerID";

		public const string ACTDATETIME_FIELD = "ActDateTime";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable LegalActivityTable => base.Tables["Legal_Activity"];

		public LegalActivityData()
		{
			BuildDataTables();
		}

		public LegalActivityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Legal_Activity");
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
			columns.Add("ActivityName", typeof(string)).AllowDBNull = false;
			columns.Add("CaseClient1", typeof(string));
			columns.Add("CaseClient2", typeof(string));
			columns.Add("CasePartyID", typeof(string));
			columns.Add("ActionName", typeof(string));
			columns.Add("LawyerID", typeof(string));
			columns.Add("StatusID", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("ParentSysDocID", typeof(string));
			columns.Add("ParentVoucherID", typeof(string));
			columns.Add("FileNo", typeof(string));
			columns.Add("CaseTypeID", typeof(string));
			columns.Add("ActivityDateTime", typeof(DateTime));
			columns.Add("ContactID", typeof(string));
			columns.Add("OwnerID", typeof(string));
			columns.Add("ActDateTime", typeof(DateTime));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
