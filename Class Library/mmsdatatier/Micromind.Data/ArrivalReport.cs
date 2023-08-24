using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ArrivalReport : StoreObject
	{
		private const string ARRIVALREPORT_TABLE = "Arrival_Report";

		private const string ARRIVALREPORTDETAIL_TABLE = "Arrival_Report_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string VESSELNAME_PARM = "@VesselName";

		private const string ORIGINID_PARM = "@OriginID";

		private const string INSPECTORID_PARM = "@InspectorID";

		private const string TASKID_PARM = "@TaskID";

		private const string COMODITIES_PARM = "@Comodities";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string CONTAINERTEMP_PARM = "@ContainerTemp";

		private const string TOTALPALLETS_PARM = "@TotalPallets";

		private const string TOTALQUANTITY_PARM = "@TotalQuantity";

		private const string DATERECEIVED_PARM = "@DateReceived";

		private const string DATEINSPECTED_PARM = "@DateInspected";

		private const string ISCONSIGNMENT_PARM = "@IsConsignment";

		private const string NOTE_PARM = "@Note";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string TEMPLATEID_PARM = "@TemplateID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string PACKINGCONDITION_PARM = "@PackingCondition";

		private const string ISPALLETIZED_PARM = "@IsPalletized";

		private const string RESULTNOTE_PARM = "@ResultNote";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string CONCLUSION_PARM = "@Conclusion";

		private const string TOTALISSUE1_PARM = "@TotalIssue1";

		private const string TOTALISSUE2_PARM = "@TotalIssue2";

		private const string TOTALISSUE3_PARM = "@TotalIssue3";

		private const string TOTALISSUE4_PARM = "@TotalIssue4";

		private const string TOTALWEIGHTLESS_PARM = "@TotalWeightLess";

		private const string ISSUE1NAME_PARM = "@Issue1Name";

		private const string ISSUE2NAME_PARM = "@Issue2Name";

		private const string ISSUE3NAME_PARM = "@Issue3Name";

		private const string ISSUE4NAME_PARM = "@Issue4Name";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string COMIDITYID_PARM = "@ComodityID";

		private const string VARIETYID_PARM = "@VarietyID";

		private const string BRANDID_PARM = "@BrandID";

		private const string GROWER_PARM = "@Grower";

		private const string ITEMSIZE_PARM = "@ItemSize";

		private const string GRADE_PARM = "@Grade";

		private const string SAMPLECOUNT_PARM = "@SampleCount";

		private const string ISSUE1COUNT_PARM = "@Issue1Count";

		private const string ISSUE2COUNT_PARM = "@Issue2Count";

		private const string ISSUE3COUNT_PARM = "@Issue3Count";

		private const string ISSUE4COUNT_PARM = "@Issue4Count";

		private const string DATECODE_PARM = "@DateCode";

		private const string TEMPERATURE_PARM = "@Temperature";

		private const string STANDARDWEIGHT_PARM = "@StandardWeight";

		private const string WEIGHT_PARM = "@Weight";

		private const string PRESSURE_PARM = "@Pressure";

		private const string BRIX_PARM = "@Brix";

		private const string NUMERICATR1_PARM = "@NumericAtr1";

		private const string NUMERICATR2_PARM = "@NumericAtr2";

		private const string NUMERICATR3_PARM = "@NumericAtr3";

		private const string NUMERICATR4_PARM = "@NumericAtr4";

		private const string TEXTATR1_PARM = "@TextAtr1";

		private const string TEXTATR2_PARM = "@TextAtr2";

		private const string TEXTATR3_PARM = "@TextAtr3";

		private const string TEXTATR4_PARM = "@TextAtr4";

		private const string REMARKS_PARM = "@Remarks";

		public ArrivalReport(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateArrivalReportText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Arrival_Report", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("InspectorID", "@InspectorID"), new FieldValue("OriginID", "@OriginID"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("VesselName", "@VesselName"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("ContainerTemp", "@ContainerTemp"), new FieldValue("TaskID", "@TaskID"), new FieldValue("TotalPallets", "@TotalPallets"), new FieldValue("TotalQuantity", "@TotalQuantity"), new FieldValue("DateReceived", "@DateReceived"), new FieldValue("DateInspected", "@DateInspected"), new FieldValue("IsConsignment", "@IsConsignment"), new FieldValue("Conclusion", "@Conclusion"), new FieldValue("Note", "@Note"), new FieldValue("LocationID", "@LocationID"), new FieldValue("TemplateID", "@TemplateID"), new FieldValue("Description", "@Description"), new FieldValue("PackingCondition", "@PackingCondition"), new FieldValue("IsPalletized", "@IsPalletized"), new FieldValue("ResultNote", "@ResultNote"), new FieldValue("Status", "@Status"), new FieldValue("Issue1Name", "@Issue1Name"), new FieldValue("Issue2Name", "@Issue2Name"), new FieldValue("Issue3Name", "@Issue3Name"), new FieldValue("Issue4Name", "@Issue4Name"), new FieldValue("TotalIssue1", "@TotalIssue1"), new FieldValue("TotalIssue2", "@TotalIssue2"), new FieldValue("TotalIssue3", "@TotalIssue3"), new FieldValue("TotalIssue4", "@TotalIssue4"), new FieldValue("TotalWeightLess", "@TotalWeightLess"), new FieldValue("SourceDocType", "@SourceDocType"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Arrival_Report", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateArrivalReportCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateArrivalReportText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateArrivalReportText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@InspectorID", SqlDbType.NVarChar);
			parameters.Add("@OriginID", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@VesselName", SqlDbType.NVarChar);
			parameters.Add("@TaskID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@ContainerTemp", SqlDbType.Decimal);
			parameters.Add("@TotalPallets", SqlDbType.Int);
			parameters.Add("@TotalQuantity", SqlDbType.Decimal);
			parameters.Add("@DateReceived", SqlDbType.DateTime);
			parameters.Add("@DateInspected", SqlDbType.DateTime);
			parameters.Add("@IsConsignment", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@TemplateID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Conclusion", SqlDbType.TinyInt);
			parameters.Add("@Issue1Name", SqlDbType.NVarChar);
			parameters.Add("@Issue2Name", SqlDbType.NVarChar);
			parameters.Add("@Issue3Name", SqlDbType.NVarChar);
			parameters.Add("@Issue4Name", SqlDbType.NVarChar);
			parameters.Add("@TotalIssue1", SqlDbType.Decimal);
			parameters.Add("@TotalIssue2", SqlDbType.Decimal);
			parameters.Add("@TotalIssue3", SqlDbType.Decimal);
			parameters.Add("@TotalIssue4", SqlDbType.Decimal);
			parameters.Add("@TotalWeightLess", SqlDbType.Decimal);
			parameters.Add("@PackingCondition", SqlDbType.TinyInt);
			parameters.Add("@IsPalletized", SqlDbType.TinyInt);
			parameters.Add("@ResultNote", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@OriginID"].SourceColumn = "OriginID";
			parameters["@InspectorID"].SourceColumn = "InspectorID";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@VesselName"].SourceColumn = "VesselName";
			parameters["@TaskID"].SourceColumn = "TaskID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@ContainerTemp"].SourceColumn = "ContainerTemp";
			parameters["@TotalPallets"].SourceColumn = "TotalPallets";
			parameters["@TotalQuantity"].SourceColumn = "TotalQuantity";
			parameters["@DateReceived"].SourceColumn = "DateReceived";
			parameters["@DateInspected"].SourceColumn = "DateInspected";
			parameters["@IsConsignment"].SourceColumn = "IsConsignment";
			parameters["@Conclusion"].SourceColumn = "Conclusion";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@TemplateID"].SourceColumn = "TemplateID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@PackingCondition"].SourceColumn = "PackingCondition";
			parameters["@IsPalletized"].SourceColumn = "IsPalletized";
			parameters["@ResultNote"].SourceColumn = "ResultNote";
			parameters["@TotalIssue1"].SourceColumn = "TotalIssue1";
			parameters["@TotalIssue2"].SourceColumn = "TotalIssue2";
			parameters["@TotalIssue3"].SourceColumn = "TotalIssue3";
			parameters["@TotalIssue4"].SourceColumn = "TotalIssue4";
			parameters["@TotalWeightLess"].SourceColumn = "TotalWeightLess";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@Issue1Name"].SourceColumn = "Issue1Name";
			parameters["@Issue2Name"].SourceColumn = "Issue2Name";
			parameters["@Issue3Name"].SourceColumn = "Issue3Name";
			parameters["@Issue4Name"].SourceColumn = "Issue4Name";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateArrivalReportDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Arrival_Report_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("ComodityID", "@ComodityID"), new FieldValue("VarietyID", "@VarietyID"), new FieldValue("BrandID", "@BrandID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ItemSize", "@ItemSize"), new FieldValue("Grade", "@Grade"), new FieldValue("SampleCount", "@SampleCount"), new FieldValue("Issue1Count", "@Issue1Count"), new FieldValue("Issue2Count", "@Issue2Count"), new FieldValue("Issue3Count", "@Issue3Count"), new FieldValue("Issue4Count", "@Issue4Count"), new FieldValue("StandardWeight", "@StandardWeight"), new FieldValue("Grower", "@Grower"), new FieldValue("DateCode", "@DateCode"), new FieldValue("Temperature", "@Temperature"), new FieldValue("Weight", "@Weight"), new FieldValue("Pressure", "@Pressure"), new FieldValue("Brix", "@Brix"), new FieldValue("NumericAtr1", "@NumericAtr1"), new FieldValue("NumericAtr2", "@NumericAtr2"), new FieldValue("NumericAtr3", "@NumericAtr3"), new FieldValue("NumericAtr4", "@NumericAtr4"), new FieldValue("TextAtr1", "@TextAtr1"), new FieldValue("TextAtr2", "@TextAtr2"), new FieldValue("TextAtr3", "@TextAtr3"), new FieldValue("TextAtr4", "@TextAtr4"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateArrivalReportDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateArrivalReportDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateArrivalReportDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@LotNumber", SqlDbType.NVarChar);
			parameters.Add("@ComodityID", SqlDbType.NVarChar);
			parameters.Add("@VarietyID", SqlDbType.NVarChar);
			parameters.Add("@BrandID", SqlDbType.NVarChar);
			parameters.Add("@Grower", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ItemSize", SqlDbType.NVarChar);
			parameters.Add("@Grade", SqlDbType.NVarChar);
			parameters.Add("@SampleCount", SqlDbType.Decimal);
			parameters.Add("@Issue1Count", SqlDbType.Decimal);
			parameters.Add("@Issue2Count", SqlDbType.Decimal);
			parameters.Add("@Issue3Count", SqlDbType.Decimal);
			parameters.Add("@Issue4Count", SqlDbType.Decimal);
			parameters.Add("@DateCode", SqlDbType.NVarChar);
			parameters.Add("@Temperature", SqlDbType.Decimal);
			parameters.Add("@StandardWeight", SqlDbType.Decimal);
			parameters.Add("@Weight", SqlDbType.Decimal);
			parameters.Add("@Pressure", SqlDbType.NVarChar);
			parameters.Add("@Brix", SqlDbType.Decimal);
			parameters.Add("@NumericAtr1", SqlDbType.Decimal);
			parameters.Add("@NumericAtr2", SqlDbType.Decimal);
			parameters.Add("@NumericAtr3", SqlDbType.Decimal);
			parameters.Add("@NumericAtr4", SqlDbType.Decimal);
			parameters.Add("@TextAtr1", SqlDbType.NVarChar);
			parameters.Add("@TextAtr2", SqlDbType.NVarChar);
			parameters.Add("@TextAtr3", SqlDbType.NVarChar);
			parameters.Add("@TextAtr4", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@ComodityID"].SourceColumn = "ComodityID";
			parameters["@VarietyID"].SourceColumn = "VarietyID";
			parameters["@BrandID"].SourceColumn = "BrandID";
			parameters["@ItemSize"].SourceColumn = "ItemSize";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Grade"].SourceColumn = "Grade";
			parameters["@SampleCount"].SourceColumn = "SampleCount";
			parameters["@Issue1Count"].SourceColumn = "Issue1Count";
			parameters["@Issue2Count"].SourceColumn = "Issue2Count";
			parameters["@Issue3Count"].SourceColumn = "Issue3Count";
			parameters["@Issue4Count"].SourceColumn = "Issue4Count";
			parameters["@Temperature"].SourceColumn = "Temperature";
			parameters["@DateCode"].SourceColumn = "DateCode";
			parameters["@Grower"].SourceColumn = "Grower";
			parameters["@StandardWeight"].SourceColumn = "StandardWeight";
			parameters["@Weight"].SourceColumn = "Weight";
			parameters["@Pressure"].SourceColumn = "Pressure";
			parameters["@Brix"].SourceColumn = "Brix";
			parameters["@NumericAtr1"].SourceColumn = "NumericAtr1";
			parameters["@NumericAtr2"].SourceColumn = "NumericAtr2";
			parameters["@NumericAtr3"].SourceColumn = "NumericAtr3";
			parameters["@NumericAtr4"].SourceColumn = "NumericAtr4";
			parameters["@TextAtr1"].SourceColumn = "TextAtr1";
			parameters["@TextAtr2"].SourceColumn = "TextAtr2";
			parameters["@TextAtr3"].SourceColumn = "TextAtr3";
			parameters["@TextAtr4"].SourceColumn = "TextAtr4";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ArrivalReportData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			return true;
		}

		public bool InsertUpdateArrivalReport(ArrivalReportData arrivalReportData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateArrivalReportCommand = GetInsertUpdateArrivalReportCommand(isUpdate);
			try
			{
				DataRow dataRow = arrivalReportData.ArrivalReportTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Arrival_Report", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in arrivalReportData.ArrivalReportDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateArrivalReportCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(arrivalReportData, "Arrival_Report", insertUpdateArrivalReportCommand)) : (flag & Insert(arrivalReportData, "Arrival_Report", insertUpdateArrivalReportCommand)));
				insertUpdateArrivalReportCommand = GetInsertUpdateArrivalReportDetailsCommand(isUpdate: false);
				insertUpdateArrivalReportCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteArrivalReportDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (arrivalReportData.Tables["Arrival_Report_Detail"].Rows.Count > 0)
				{
					flag &= Insert(arrivalReportData, "Arrival_Report_Detail", insertUpdateArrivalReportCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Arrival_Report", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Arrival Report";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Arrival_Report", "VoucherID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public ArrivalReportData GetArrivalReportByID(string sysDocID, string voucherID)
		{
			try
			{
				ArrivalReportData arrivalReportData = new ArrivalReportData();
				string textCommand = "SELECT AR.*, V.VendorName,QC.VoucherID as QualityClaim,QC.sysDocID as QualityClaimSysDoc FROM Arrival_Report AR INNER JOIN Vendor V ON V.VendorID = AR.VendorID   \r\n                                LEFT OUTER JOIN Quality_Claim QC ON QC.SourceSysDocID=AR.SysDocID AND QC.SourceVoucherID=AR.VoucherID WHERE AR.VoucherID='" + voucherID + "' AND AR.SysDocID='" + sysDocID + "'";
				FillDataSet(arrivalReportData, "Arrival_Report", textCommand);
				if (arrivalReportData == null || arrivalReportData.Tables.Count == 0 || arrivalReportData.Tables["Arrival_Report"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,CAT.CategoryName  AS CommodityName,PS.StyleName AS VarietyName,BR.BrandName AS BrandName\r\n                        FROM Arrival_Report_Detail TD \r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Category CAT ON CAT.CategoryID =  TD.ComodityID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Style PS ON PS.StyleID =  TD.VarietyID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Brand BR ON BR.BrandID =  TD.BrandID                     \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(arrivalReportData, "Arrival_Report_Detail", textCommand);
				return arrivalReportData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetClaimableArrivalReports()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],DateReceived AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],\r\n                                 (select top 1 ComodityID from Arrival_Report_Detail ARD where ARD.SysDocID=SO.SysDocID and ARd.VoucherID=SO.VoucherID)AS Commodity,\r\n                                Reference,ContainerNumber AS [Container#],OriginID AS Origin,DATEDIFF(day,DateReceived,GetDate()) as [Age(Days)] FROM Arrival_Report SO\r\n                                      INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False'\r\n                                      AND ISNULL(Status,1) = 1 AND ISNULL(Conclusion,1)=2  AND  NOT Exists (SELECT * FROM Quality_Claim WHERE SourceSysDocID = SO.SysDocID AND SourceVoucherID = SO.VoucherID)";
				FillDataSet(dataSet, "Arrival_Report", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteArrivalReportDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Arrival_Report_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteArrivalReport(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to change or delete. Some of the items in this order has been already received.", 1037);
				}
				flag &= DeleteArrivalReportDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Arrival_Report WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Arrival Report", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public DataSet GetArrivalReportToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT AR.* ,CO.CountryName AS OriginName,INS.GenericListName AS InspectorName,V.VendorName AS VendorName\r\n                                FROM Arrival_Report AR LEFT OUTER JOIN Country CO ON CO.CountryID = AR.OriginID                              \r\n                                LEFT OUTER JOIN Generic_List INS ON INS.GenericListID = AR.InspectorID AND INS.GenericListType = 10 \r\n\t                            LEFT OUTER JOIN Vendor V ON V.VendorID=AR.VendorID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Arrival_Report", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Arrival_Report"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT TD.*,CAT.CategoryName  AS CommodityName,PS.StyleName AS VarietyName,BR.BrandName AS BrandName\r\n                        FROM Arrival_Report_Detail TD \r\n                        INNER JOIN  Arrival_Report AR ON AR.SysDocID=TD.SysDocID AND AR.VoucherID=TD.VoucherID                      \r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Category CAT ON CAT.CategoryID =  TD.ComodityID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Style PS ON PS.StyleID =  TD.VarietyID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Brand BR ON BR.BrandID =  TD.BrandID\r\n                        WHERE TD.SysDocID='" + sysDocID + "' AND TD.VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Arrival_Report_Detail", cmdText);
				cmdText = "SELECT ART.* FROM Arrival_Report_Template ART\r\n                                LEFT OUTER JOIN Arrival_Report AR ON ART.TemplateID=AR.TemplateID WHERE  AR.SysDocID = '" + sysDocID + "' AND AR.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Arrival_Report_Template", cmdText);
				dataSet.Relations.Add("ArrivalReport", new DataColumn[2]
				{
					dataSet.Tables["Arrival_Report"].Columns["SysDocID"],
					dataSet.Tables["Arrival_Report"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Arrival_Report_Detail"].Columns["SysDocID"],
					dataSet.Tables["Arrival_Report_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Arrival_Report"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Arrival_Report"].Rows)
				{
					row["TotalInWords"] = NumToWord.GetNumInWords(default(decimal) - default(decimal));
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],ContainerNumber [Container No],\r\n                        (select top 1 ComodityID from Arrival_Report_Detail ARD where ARD.SysDocID=INV.SysDocID and ARd.VoucherID=INV.VoucherID)AS Commodity,DateReceived [Received Date],Status,\r\n                           Reference   FROM   Arrival_Report INV\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND DateReceived Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Arrival_Report", sqlCommand);
			return dataSet;
		}
	}
}
