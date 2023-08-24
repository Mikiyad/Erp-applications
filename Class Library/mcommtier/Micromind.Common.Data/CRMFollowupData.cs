using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CRMFollowupData : DataSet
	{
		public const string CRMFOLLOWUP_TABLE = "Lead_Followup_Details";

		public const string CRMFOLLOWUPID_FIELD = "FollowupID";

		public const string LEADID_FIELD = "LeadID";

		public const string THISFOLLOWUPDATE_FIELD = "ThisFollowupDate";

		public const string NEXTFOLLOWUPDATE_FIELD = "NextFollowupDate";

		public const string THISFOLLOWUPBYID_FIELD = "ThisFollowupByID";

		public const string NEXTFOLLOWUPBYID_FIELD = "NextFollowupByID";

		public const string THISFOLLOWUPSTATUSID_FIELD = "ThisFollowupStatusID";

		public const string REMARK_FIELD = "Remark";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string CRMTYPE_FIELD = "CRMType";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CRMFollowupTable => base.Tables["Lead_Followup_Details"];

		public CRMFollowupData()
		{
			BuildDataTables();
		}

		public CRMFollowupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Lead_Followup_Details");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("FollowupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LeadID", typeof(string));
			columns.Add("ThisFollowupDate", typeof(DateTime));
			columns.Add("NextFollowupDate", typeof(DateTime));
			columns.Add("ThisFollowupByID", typeof(string));
			columns.Add("NextFollowupByID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("CRMType", typeof(short));
			columns.Add("ThisFollowupStatusID", typeof(string));
			columns.Add("Remark", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
