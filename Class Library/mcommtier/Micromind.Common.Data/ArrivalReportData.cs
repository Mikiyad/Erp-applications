using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ArrivalReportData : DataSet
	{
		public const string ARRIVALREPORT_TABLE = "Arrival_Report";

		public const string ARRIVALREPORTDETAIL_TABLE = "Arrival_Report_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string ORIGINID_FIELD = "OriginID";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string VESSELNAME_FIELD = "VesselName";

		public const string COMODITIES_FIELD = "Comodities";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string INSPECTORID_FIELD = "InspectorID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string CONTAINERTEMP_FIELD = "ContainerTemp";

		public const string TOTALPALLETS_FIELD = "TotalPallets";

		public const string TOTALQUANTITY_FIELD = "TotalQuantity";

		public const string DATERECEIVED_FIELD = "DateReceived";

		public const string DATEINSPECTED_FIELD = "DateInspected";

		public const string ISCONSIGNMENT_FIELD = "IsConsignment";

		public const string NOTE_FIELD = "Note";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string TEMPLATEID_FIELD = "TemplateID";

		public const string TASKID_FIELD = "TaskID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string PACKINGCONDITION_FIELD = "PackingCondition";

		public const string ISPALLETIZED_FIELD = "IsPalletized";

		public const string RESULTNOTE_FIELD = "ResultNote";

		public const string STATUS_FIELD = "Status";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string TOTALMAJOR_FIELD = "TotalMajor";

		public const string TOTALMINOR_FIELD = "TotalMinor";

		public const string CONCLUSION_FIELD = "Conclusion";

		public const string ISSUE1NAME_FIELD = "Issue1Name";

		public const string ISSUE2NAME_FIELD = "Issue2Name";

		public const string ISSUE3NAME_FIELD = "Issue3Name";

		public const string ISSUE4NAME_FIELD = "Issue4Name";

		public const string TOTALISSUE1_FIELD = "TotalIssue1";

		public const string TOTALISSUE2_FIELD = "TotalIssue2";

		public const string TOTALISSUE3_FIELD = "TotalIssue3";

		public const string TOTALISSUE4_FIELD = "TotalIssue4";

		public const string TOTALWEIGHTLESS_FIELD = "TotalWeightLess";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string COMIDITYID_FIELD = "ComodityID";

		public const string VARIETYID_FIELD = "VarietyID";

		public const string BRANDID_FIELD = "BrandID";

		public const string GROWER_FIELD = "Grower";

		public const string ITEMSIZE_FIELD = "ItemSize";

		public const string GRADE_FIELD = "Grade";

		public const string SAMPLECOUNT_FIELD = "SampleCount";

		public const string ISSUE1COUNT_FIELD = "Issue1Count";

		public const string ISSUE2COUNT_FIELD = "Issue2Count";

		public const string ISSUE3COUNT_FIELD = "Issue3Count";

		public const string ISSUE4COUNT_FIELD = "Issue4Count";

		public const string DATECODE_FIELD = "DateCode";

		public const string TEMPERATURE_FIELD = "Temperature";

		public const string STANDARDWEIGHT_FIELD = "StandardWeight";

		public const string WEIGHT_FIELD = "Weight";

		public const string PRESSURE_FIELD = "Pressure";

		public const string BRIX_FIELD = "Brix";

		public const string NUMERICATR1_FIELD = "NumericAtr1";

		public const string NUMERICATR2_FIELD = "NumericAtr2";

		public const string NUMERICATR3_FIELD = "NumericAtr3";

		public const string NUMERICATR4_FIELD = "NumericAtr4";

		public const string TEXTATR1_FIELD = "TextAtr1";

		public const string TEXTATR2_FIELD = "TextAtr2";

		public const string TEXTATR3_FIELD = "TextAtr3";

		public const string TEXTATR4_FIELD = "TextAtr4";

		public const string REMARKS_FIELD = "Remarks";

		public DataTable ArrivalReportTable => base.Tables["Arrival_Report"];

		public DataTable ArrivalReportDetailTable => base.Tables["Arrival_Report_Detail"];

		public ArrivalReportData()
		{
			BuildDataTables();
		}

		public ArrivalReportData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Arrival_Report");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("VendorID", typeof(string));
			columns.Add("InspectorID", typeof(string));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("VesselName", typeof(string));
			columns.Add("Comodities", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("OriginID", typeof(string));
			columns.Add("TaskID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("ContainerTemp", typeof(decimal));
			columns.Add("TotalPallets", typeof(short));
			columns.Add("TotalQuantity", typeof(decimal));
			columns.Add("DateReceived", typeof(DateTime));
			columns.Add("TotalIssue1", typeof(decimal));
			columns.Add("TotalIssue2", typeof(decimal));
			columns.Add("TotalIssue3", typeof(decimal));
			columns.Add("TotalIssue4", typeof(decimal));
			columns.Add("Issue1Name", typeof(string));
			columns.Add("Issue2Name", typeof(string));
			columns.Add("Issue3Name", typeof(string));
			columns.Add("Issue4Name", typeof(string));
			columns.Add("TotalWeightLess", typeof(decimal));
			columns.Add("DateInspected", typeof(DateTime));
			columns.Add("IsConsignment", typeof(bool));
			columns.Add("Note", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("TemplateID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("PackingCondition", typeof(byte));
			columns.Add("IsPalletized", typeof(byte));
			columns.Add("Conclusion", typeof(byte));
			columns.Add("ResultNote", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Status", typeof(byte));
			columns.Add("SourceDocType", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Arrival_Report_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("LotNumber", typeof(string));
			columns.Add("ComodityID", typeof(string));
			columns.Add("VarietyID", typeof(string));
			columns.Add("BrandID", typeof(string));
			columns.Add("ItemSize", typeof(string));
			columns.Add("Grade", typeof(string));
			columns.Add("SampleCount", typeof(decimal));
			columns.Add("Issue1Count", typeof(decimal));
			columns.Add("Issue2Count", typeof(decimal));
			columns.Add("Issue3Count", typeof(decimal));
			columns.Add("Issue4Count", typeof(decimal));
			columns.Add("DateCode", typeof(string));
			columns.Add("Temperature", typeof(string));
			columns.Add("StandardWeight", typeof(decimal));
			columns.Add("Weight", typeof(decimal));
			columns.Add("Pressure", typeof(decimal));
			columns.Add("Brix", typeof(decimal));
			columns.Add("NumericAtr1", typeof(decimal));
			columns.Add("NumericAtr2", typeof(decimal));
			columns.Add("NumericAtr3", typeof(decimal));
			columns.Add("NumericAtr4", typeof(decimal));
			columns.Add("TextAtr1", typeof(string));
			columns.Add("TextAtr2", typeof(string));
			columns.Add("TextAtr3", typeof(string));
			columns.Add("TextAtr4", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
