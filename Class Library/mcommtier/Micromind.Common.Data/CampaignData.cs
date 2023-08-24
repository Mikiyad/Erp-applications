using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CampaignData : DataSet
	{
		public const string CAMPAIGN_TABLE = "Campaign";

		public const string CAMPAIGNID_FIELD = "CampaignID";

		public const string CAMPAIGNNAME_FIELD = "CampaignName";

		public const string CAMPAIGNTYPE_FIELD = "Type";

		public const string CAMPAIGNSTATUS_FIELD = "Status";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string NUMSENT_FIELD = "NumberSent";

		public const string EXPRESPONSE_FIELD = "ExpectedResponse";

		public const string BUDGCOST_FIELD = "BudgetedCost";

		public const string ACTCOST_FIELD = "ActualCost";

		public const string EXPREVENUE_FIELD = "ExpectedRevenue";

		public const string INACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CampaignTable => base.Tables["Campaign"];

		public CampaignData()
		{
			BuildDataTables();
		}

		public CampaignData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Campaign");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CampaignID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CampaignName", typeof(string)).AllowDBNull = false;
			columns.Add("Type", typeof(byte));
			columns.Add("Status", typeof(byte));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("NumberSent", typeof(int));
			columns.Add("ExpectedResponse", typeof(byte));
			columns.Add("BudgetedCost", typeof(decimal));
			columns.Add("ActualCost", typeof(decimal));
			columns.Add("ExpectedRevenue", typeof(decimal));
			columns.Add("IsInactive", typeof(bool));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
