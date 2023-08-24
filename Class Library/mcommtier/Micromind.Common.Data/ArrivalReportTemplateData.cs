using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ArrivalReportTemplateData : DataSet
	{
		public const string ARRIVALREPORTTEMPLATE_TABLE = "Arrival_Report_Template";

		public const string TEMPLATEID_FIELD = "TemplateID";

		public const string TEMPLATENAME_FIELD = "TemplateName";

		public const string ISSUE1NAME_FIELD = "Issue1Name";

		public const string ISSUE2NAME_FIELD = "Issue2Name";

		public const string ISSUE3NAME_FIELD = "Issue3Name";

		public const string ISSUE4NAME_FIELD = "Issue4Name";

		public const string ISSUE1LOSSPERCENT_FIELD = "Issue1LossPercent";

		public const string ISSUE2LOSSPERCENT_FIELD = "Issue2LossPercent";

		public const string ISSUE3LOSSPERCENT_FIELD = "Issue3LossPercent";

		public const string ISSUE4LOSSPERCENT_FIELD = "Issue4LossPercent";

		public const string ATRNUM1NAME_FIELD = "AtrNum1Name";

		public const string ATRNUM2NAME_FIELD = "AtrNum2Name";

		public const string ATRNUM3NAME_FIELD = "AtrNum3Name";

		public const string ATRNUM4NAME_FIELD = "AtrNum4Name";

		public const string ATRTEXT1NAME_FIELD = "AtrText1Name";

		public const string ATRTEXT2NAME_FIELD = "AtrText2Name";

		public const string ATRTEXT3NAME_FIELD = "AtrText3Name";

		public const string ATRTEXT4NAME_FIELD = "AtrText4Name";

		public const string PRINTTEMPLATENAME_FIELD = "PrintTemplateName";

		public const string ISBRIX_FIELD = "IsBrix";

		public const string ISPRESSURE_FIELD = "IsPressure";

		public const string ISGROWER_FIELD = "IsGrower";

		public const string ISDATECODE_FIELD = "IsDateCode";

		public const string ISPALLETID_FIELD = "IsPalletID";

		public const string ISTEMPERATURE_FIELD = "IsTemperature";

		public const string INACTIVE_FIELD = "IsInactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ArrivalReportTemplateTable => base.Tables["Arrival_Report_Template"];

		public ArrivalReportTemplateData()
		{
			BuildDataTables();
		}

		public ArrivalReportTemplateData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Arrival_Report_Template");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TemplateID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TemplateName", typeof(string)).AllowDBNull = false;
			columns.Add("Issue1Name", typeof(string));
			columns.Add("Issue2Name", typeof(string));
			columns.Add("Issue3Name", typeof(string));
			columns.Add("Issue4Name", typeof(string));
			columns.Add("Issue1LossPercent", typeof(string));
			columns.Add("Issue2LossPercent", typeof(string));
			columns.Add("Issue3LossPercent", typeof(string));
			columns.Add("Issue4LossPercent", typeof(string));
			columns.Add("AtrNum1Name", typeof(string));
			columns.Add("AtrNum2Name", typeof(string));
			columns.Add("AtrNum3Name", typeof(string));
			columns.Add("AtrNum4Name", typeof(string));
			columns.Add("AtrText1Name", typeof(string));
			columns.Add("AtrText2Name", typeof(string));
			columns.Add("AtrText3Name", typeof(string));
			columns.Add("AtrText4Name", typeof(string));
			columns.Add("PrintTemplateName", typeof(string));
			columns.Add("IsBrix", typeof(bool));
			columns.Add("IsPressure", typeof(bool));
			columns.Add("IsGrower", typeof(bool));
			columns.Add("IsDateCode", typeof(bool));
			columns.Add("IsPalletID", typeof(bool));
			columns.Add("IsTemperature", typeof(bool));
			columns.Add("IsInactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
