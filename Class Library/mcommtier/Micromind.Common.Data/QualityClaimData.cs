using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class QualityClaimData : DataSet
	{
		public const string QUALITYCLAIM_TABLE = "Quality_Claim";

		public const string QUALITYCLAIMDETAIL_TABLE = "Quality_Claim_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string CLAIMAMOUNT_FIELD = "ClaimAmount";

		public const string RECEIVEDAMOUNT_FIELD = "ReceivedAmount";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string BATCHNUMBER_FIELD = "BatchNumber";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TOTALPALLETS_FIELD = "TotalPallets";

		public const string TOTALQUANTITY_FIELD = "TotalQuantity";

		public const string NOTE_FIELD = "Note";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string SURVEYTYPE_FIELD = "SurveyType";

		public const string DATERECEIVED_FIELD = "DateReceived";

		public const string DATEINSPECTED_FIELD = "DateInspected";

		public const string SURVEYERID_FIELD = "SurveyerID";

		public const string SURVEYER2ID_FIELD = "Surveyer2ID";

		public const string SURVEYDATE_FIELD = "SurveyDate";

		public const string CLAIMDATE_FIELD = "ClaimDate";

		public const string VESSELNAME_FIELD = "VesselName";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string ORIGINID_FIELD = "OriginID";

		public const string STATUS_FIELD = "Status";

		public const string CLOSEDESCRIPTION_FIELD = "CloseDescription";

		public const string CRSYSDOCID_FIELD = "CRSysDocID";

		public const string CRVOUCHERID_FIELD = "CRVoucherID";

		public const string CLAIMSTATUS_FIELD = "ClaimStatus";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWTYPE_FIELD = "RowType";

		public const string COMMODITYID_FIELD = "CommodityID";

		public const string VARIETYID_FIELD = "VarietyID";

		public const string BRANDID_FIELD = "BrandID";

		public const string ISSUENAME_FIELD = "IssueName";

		public const string ISSUEPERCENT_FIELD = "IssuePercent";

		public const string UNITCOST_FIELD = "UnitCost";

		public const string RECEIVEDQUANTITY_FIELD = "ReceivedQuantity";

		public const string QUANTITY_FIELD = "Quantity";

		public const string LOSSRATIO_FIELD = "LossRatio";

		public const string CLAIMRATE_FIELD = "ClaimRate";

		public const string GRADE_FIELD = "Grade";

		public DataTable QualityClaimTable => base.Tables["Quality_Claim"];

		public DataTable QualityClaimDetailTable => base.Tables["Quality_Claim_Detail"];

		public QualityClaimData()
		{
			BuildDataTables();
		}

		public QualityClaimData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Quality_Claim");
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
			columns.Add("ClaimAmount", typeof(decimal));
			columns.Add("ReceivedAmount", typeof(decimal));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("TotalPallets", typeof(short));
			columns.Add("TotalQuantity", typeof(decimal));
			columns.Add("Note", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("SurveyType", typeof(short));
			columns.Add("DateReceived", typeof(DateTime));
			columns.Add("DateInspected", typeof(DateTime));
			columns.Add("SurveyerID", typeof(string));
			columns.Add("Surveyer2ID", typeof(string));
			columns.Add("SurveyDate", typeof(DateTime));
			columns.Add("ClaimDate", typeof(DateTime));
			columns.Add("VesselName", typeof(string));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("OriginID", typeof(string));
			columns.Add("Status", typeof(short));
			columns.Add("CloseDescription", typeof(string));
			columns.Add("CRSysDocID", typeof(string));
			columns.Add("CRVoucherID", typeof(string));
			columns.Add("ClaimStatus", typeof(string));
			columns.Add("BatchNumber", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Quality_Claim_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("RowType", typeof(byte));
			columns.Add("CommodityID", typeof(string));
			columns.Add("VarietyID", typeof(string));
			columns.Add("BrandID", typeof(string));
			columns.Add("Grade", typeof(string));
			columns.Add("IssueName", typeof(string));
			columns.Add("IssuePercent", typeof(decimal));
			columns.Add("UnitCost", typeof(decimal));
			columns.Add("ReceivedQuantity", typeof(decimal));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("LossRatio", typeof(decimal));
			columns.Add("ClaimRate", typeof(decimal));
			columns.Add("ClaimAmount", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
