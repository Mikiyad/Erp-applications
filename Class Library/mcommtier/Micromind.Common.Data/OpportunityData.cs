using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class OpportunityData : DataSet
	{
		public const string OPPORTUNITY_TABLE = "Opportunity";

		public const string OPPORTUNITYID_FIELD = "OpportunityID";

		public const string OPPORTUNITYNAME_FIELD = "OpportunityName";

		public const string STATUS_FIELD = "Status";

		public const string CLOSINGDATE_FIELD = "ClosingDate";

		public const string DUEDATE_FIELD = "DueDate";

		public const string PROBABILITY_FIELD = "Probability";

		public const string AMOUNT_FIELD = "Amount";

		public const string RELATEDID_FIELD = "RelatedID";

		public const string RELATEDTYPE_FIELD = "RelatedType";

		public const string OWNERID_FIELD = "OwnerID";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable OpportunityTable => base.Tables["Opportunity"];

		public OpportunityData()
		{
			BuildDataTables();
		}

		public OpportunityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Opportunity");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("OpportunityID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("OpportunityName", typeof(string)).AllowDBNull = false;
			columns.Add("Status", typeof(byte));
			columns.Add("ClosingDate", typeof(DateTime));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("Probability", typeof(byte));
			columns.Add("Amount", typeof(decimal));
			columns.Add("RelatedID", typeof(string));
			columns.Add("OwnerID", typeof(string));
			columns.Add("RelatedType", typeof(byte));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
