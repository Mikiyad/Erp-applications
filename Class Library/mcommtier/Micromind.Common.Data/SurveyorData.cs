using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SurveyorData : DataSet
	{
		public const string SURVEYORID_FIELD = "SurveyorID";

		public const string SURVEYORNAME_FIELD = "SurveyorName";

		public const string TEL_FIELD = "Tel";

		public const string MOBILE_FIELD = "Mobile";

		public const string EMAIL_FIELD = "Email";

		public const string WEBSITE_FIELD = "Website";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string SURVEYOR_TABLE = "Surveyor";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable SurveyorTable => base.Tables["Surveyor"];

		public SurveyorData()
		{
			BuildDataTables();
		}

		public SurveyorData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Surveyor");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SurveyorID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SurveyorName", typeof(string));
			columns.Add("Tel", typeof(string));
			columns.Add("Mobile", typeof(string));
			columns.Add("Email", typeof(string));
			columns.Add("Website", typeof(string));
			columns.Add("ContactName", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
