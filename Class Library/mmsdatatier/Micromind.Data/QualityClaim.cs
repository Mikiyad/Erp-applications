using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class QualityClaim : StoreObject
	{
		private const string QUALITYCLAIM_TABLE = "Quality_Claim";

		private const string QUALITYCLAIMDETAIL_TABLE = "Quality_Claim_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string CLAIMAMOUNT_PARM = "@ClaimAmount";

		private const string RECEIVEDAMOUNT_PARM = "@ReceivedAmount";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TOTALPALLETS_PARM = "@TotalPallets";

		private const string TOTALQUANTITY_PARM = "@TotalQuantity";

		private const string NOTE_PARM = "@Note";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string SURVEYTYPE_PARM = "@SurveyType";

		private const string DATERECEIVED_PARM = "@DateReceived";

		private const string DATEINSPECTED_PARM = "@DateInspected";

		private const string SURVEYERID_PARM = "@SurveyerID";

		private const string SURVEYER2ID_PARM = "@Surveyer2ID";

		private const string SURVEYDATE_PARM = "@SurveyDate";

		private const string CLAIMDATE_PARM = "@ClaimDate";

		private const string VESSELNAME_PARM = "@VesselName";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string ORIGINID_PARM = "@Status";

		private const string STATUS_PARM = "@RowIndex";

		private const string CLOSEDESCRIPTION_PARM = "@CloseDescription";

		private const string CRSYSDOCID_PARM = "@CRSysDocID";

		private const string CRVOUCHERID_PARM = "@CRVoucherID";

		private const string CLAIMSTATUS_PARM = "@ClaimStatus";

		private const string BATCHNUMBER_PARM = "@BatchNumber";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWTYPE_PARM = "@RowType";

		private const string COMMODITYID_PARM = "@ComodityID";

		private const string VARIETYID_PARM = "@VarietyID";

		private const string BRANDID_PARM = "@BrandID";

		private const string ISSUENAME_PARM = "@IssueName";

		private const string ISSUEPERCENT_PARM = "@IssuePercent";

		private const string UNITCOST_PARM = "@UnitCost";

		private const string RECEIVEDQUANTITY_PARM = "@ReceivedQuantity";

		private const string QUANTITY_PARM = "@Quantity";

		private const string LOSSRATIO_PARM = "@LossRatio";

		private const string CLAIMRATE_PARM = "@ClaimRate";

		private const string GRADE_PARM = "@Grade";

		public QualityClaim(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateQualityClaimText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Quality_Claim", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("ClaimAmount", "@ClaimAmount"), new FieldValue("ReceivedAmount", "@ReceivedAmount"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Note", "@Note"), new FieldValue("Description", "@Description"), new FieldValue("SurveyType", "@SurveyType"), new FieldValue("SurveyerID", "@SurveyerID"), new FieldValue("Surveyer2ID", "@Surveyer2ID"), new FieldValue("SurveyDate", "@SurveyDate"), new FieldValue("ClaimDate", "@ClaimDate"), new FieldValue("Status", "@RowIndex"), new FieldValue("CloseDescription", "@CloseDescription"), new FieldValue("BatchNumber", "@BatchNumber"), new FieldValue("CRSysDocID", "@CRSysDocID"), new FieldValue("CRVoucherID", "@CRVoucherID"), new FieldValue("ClaimStatus", "@ClaimStatus"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Quality_Claim", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateQualityClaimCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateQualityClaimText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateQualityClaimText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@ClaimAmount", SqlDbType.Decimal);
			parameters.Add("@ReceivedAmount", SqlDbType.Decimal);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@SurveyType", SqlDbType.Int);
			parameters.Add("@SurveyerID", SqlDbType.NVarChar);
			parameters.Add("@Surveyer2ID", SqlDbType.NVarChar);
			parameters.Add("@SurveyDate", SqlDbType.DateTime);
			parameters.Add("@ClaimDate", SqlDbType.DateTime);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ClaimStatus", SqlDbType.NVarChar);
			parameters.Add("@CloseDescription", SqlDbType.NVarChar);
			parameters.Add("@CRSysDocID", SqlDbType.NVarChar);
			parameters.Add("@CRVoucherID", SqlDbType.NVarChar);
			parameters.Add("@BatchNumber", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@ClaimAmount"].SourceColumn = "ClaimAmount";
			parameters["@ReceivedAmount"].SourceColumn = "ReceivedAmount";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@SurveyType"].SourceColumn = "SurveyType";
			parameters["@SurveyerID"].SourceColumn = "SurveyerID";
			parameters["@Surveyer2ID"].SourceColumn = "Surveyer2ID";
			parameters["@SurveyDate"].SourceColumn = "SurveyDate";
			parameters["@ClaimDate"].SourceColumn = "ClaimDate";
			parameters["@ClaimStatus"].SourceColumn = "ClaimStatus";
			parameters["@RowIndex"].SourceColumn = "Status";
			parameters["@CloseDescription"].SourceColumn = "CloseDescription";
			parameters["@CRSysDocID"].SourceColumn = "CRSysDocID";
			parameters["@CRVoucherID"].SourceColumn = "CRVoucherID";
			parameters["@BatchNumber"].SourceColumn = "BatchNumber";
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

		private string GetInsertUpdateQualityClaimDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Quality_Claim_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowType", "@RowType"), new FieldValue("CommodityID", "@ComodityID"), new FieldValue("VarietyID", "@VarietyID"), new FieldValue("BrandID", "@BrandID"), new FieldValue("IssueName", "@IssueName"), new FieldValue("IssuePercent", "@IssuePercent"), new FieldValue("UnitCost", "@UnitCost"), new FieldValue("ReceivedQuantity", "@ReceivedQuantity"), new FieldValue("Quantity", "@Quantity"), new FieldValue("LossRatio", "@LossRatio"), new FieldValue("Grade", "@Grade"), new FieldValue("ClaimRate", "@ClaimRate"), new FieldValue("ClaimAmount", "@ClaimAmount"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateQualityClaimDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateQualityClaimDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateQualityClaimDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowType", SqlDbType.TinyInt);
			parameters.Add("@ComodityID", SqlDbType.NVarChar);
			parameters.Add("@VarietyID", SqlDbType.NVarChar);
			parameters.Add("@BrandID", SqlDbType.NVarChar);
			parameters.Add("@Grade", SqlDbType.NVarChar);
			parameters.Add("@IssueName", SqlDbType.NVarChar);
			parameters.Add("@IssuePercent", SqlDbType.Decimal);
			parameters.Add("@UnitCost", SqlDbType.Decimal);
			parameters.Add("@ReceivedQuantity", SqlDbType.Decimal);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@LossRatio", SqlDbType.Decimal);
			parameters.Add("@ClaimRate", SqlDbType.Decimal);
			parameters.Add("@ClaimAmount", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RowType"].SourceColumn = "RowType";
			parameters["@ComodityID"].SourceColumn = "CommodityID";
			parameters["@VarietyID"].SourceColumn = "VarietyID";
			parameters["@BrandID"].SourceColumn = "BrandID";
			parameters["@Grade"].SourceColumn = "Grade";
			parameters["@IssueName"].SourceColumn = "IssueName";
			parameters["@IssuePercent"].SourceColumn = "IssuePercent";
			parameters["@UnitCost"].SourceColumn = "UnitCost";
			parameters["@ReceivedQuantity"].SourceColumn = "ReceivedQuantity";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@LossRatio"].SourceColumn = "LossRatio";
			parameters["@ClaimRate"].SourceColumn = "ClaimRate";
			parameters["@ClaimAmount"].SourceColumn = "ClaimAmount";
			parameters["@Description"].SourceColumn = "Description";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(QualityClaimData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			return true;
		}

		public bool InsertUpdateQualityClaim(QualityClaimData qualityClaimData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateQualityClaimCommand = GetInsertUpdateQualityClaimCommand(isUpdate);
			try
			{
				DataRow dataRow = qualityClaimData.QualityClaimTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Quality_Claim", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in qualityClaimData.QualityClaimDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateQualityClaimCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(qualityClaimData, "Quality_Claim", insertUpdateQualityClaimCommand)) : (flag & Insert(qualityClaimData, "Quality_Claim", insertUpdateQualityClaimCommand)));
				insertUpdateQualityClaimCommand = GetInsertUpdateQualityClaimDetailsCommand(isUpdate: false);
				insertUpdateQualityClaimCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteQualityClaimDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (qualityClaimData.Tables["Quality_Claim_Detail"].Rows.Count > 0)
				{
					flag &= Insert(qualityClaimData, "Quality_Claim_Detail", insertUpdateQualityClaimCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Quality_Claim", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Arrival Report";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Quality_Claim", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.QualityClaim, sysDocID, text, "Quality_Claim", sqlTransaction);
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

		public QualityClaimData GetQualityClaimByID(string sysDocID, string voucherID)
		{
			try
			{
				QualityClaimData qualityClaimData = new QualityClaimData();
				string textCommand = "SELECT * FROM Quality_Claim WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(qualityClaimData, "Quality_Claim", textCommand);
				if (qualityClaimData == null || qualityClaimData.Tables.Count == 0 || qualityClaimData.Tables["Quality_Claim"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Quality_Claim_Detail TD \r\n\t\t\t\t\t\t\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(qualityClaimData, "Quality_Claim_Detail", textCommand);
				return qualityClaimData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetQualityClaimAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT QC.SysDocID [Doc ID],QC.VoucherID [Number],QC.SourceSysDocID,QC.SourceVoucherID FROM Quality_Claim QC\r\n                               WHERE  QC.Status=0";
				FillDataSet(dataSet, "QualityClaim", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteQualityClaimDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Quality_Claim_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteQualityClaim(string sysDocID, string voucherID)
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
				flag &= DeleteQualityClaimDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Quality_Claim WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Quality Claim", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetQualityClaimToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT QC.SysDocID,QC.VoucherID,QC.SourceSysDocID,QC.SourceVoucherID,QC.VendorID,V.VendorName,QC.Note,QC.CurrencyID,\r\n                                AR.DateReceived,AR.OriginID,C.CountryName,AR.TotalPallets,AR.TotalQuantity,AR.ContainerNumber,AR.ContainerTemp,\r\n                                QC.Reference,AR.VesselName,CASE WHEN QC.Status=0 THEN 'Open' ELSE 'Closed' END AS StatusName,QC.ClaimDate,QC.ClaimAmount,QC.ReceivedAmount,\r\n                                CASE WHEN QC.SurveyType=1 THEN 'No Survey' WHEN QC.SurveyType=2 THEN 'Single Survey' ELSE 'Join Survey' END AS Survey\r\n                                FROM Quality_Claim QC LEFT JOIN Vendor V ON QC.VendorID=V.VendorID\r\n                                LEFT JOIN Arrival_Report AR ON AR.SysDocID=QC.SourceSysDocID AND AR.VoucherID=QC.SourceVoucherID  \r\n                                LEFT JOIN Country C ON AR.OriginID=C.CountryID\r\n                                WHERE  QC.SysDocID = '" + sysDocID + "' AND QC.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Quality_Claim", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Quality_Claim"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT TD.*,CASE WHEN TD.RowType=1 THEN 'Quality' WHEN TD.RowType=2 THEN 'Weight' WHEN TD.RowType=2 THEN 'Packing' ELSE 'Expense' END AS RowName\r\n                        ,PC.CategoryName,PB.BrandName,PS.StyleName  FROM Quality_Claim_Detail TD LEFT JOIN Product_Category PC ON TD.CommodityID=PC.CategoryID\r\n                        LEFT JOIN Product_Brand PB ON TD.BrandID=PB.BrandID LEFT JOIN Product_Style PS ON TD.VarietyID=PS.StyleID\r\n                        WHERE TD.SysDocID='" + sysDocID + "' AND TD.VoucherID IN (" + text + ")  ORDER BY TD.RowType";
				FillDataSet(dataSet, "Quality_Claim_Detail", cmdText);
				dataSet.Relations.Add("QualityClaim", new DataColumn[2]
				{
					dataSet.Tables["Quality_Claim"].Columns["SysDocID"],
					dataSet.Tables["Quality_Claim"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Quality_Claim_Detail"].Columns["SysDocID"],
					dataSet.Tables["Quality_Claim_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Quality_Claim"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Quality_Claim"].Rows)
				{
					_ = row;
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid, bool showClose)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     QC.SysDocID [Doc ID],QC.VoucherID [Doc Number],QC.SourceSysDocID[ArrivalDoc ID],QC.SourceVoucherID[Arrival VoucherID], QC.VendorID [Vendor Code],VendorName [Vendor Name],QC.DateReceived [Order Date],AR.ContainerNumber [ContainerNo],\r\n                             (select top 1 CategoryName from Arrival_Report_Detail ARD LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=ARD.ComodityID where ARD.SysDocID=AR.SysDocID and ARd.VoucherID=AR.VoucherID)AS Commodity,                              \r\n                            ISNULL(QC.ClaimAmount,0) [Claim Amount],ISNULL(QC.ReceivedAmount,0) [Received Amount],QC.ClaimDate, AR.DateReceived AS [Arrival Date],\r\n                            datediff(day,QC.ClaimDate,getdate()) as Age, QC.Reference,CASE QC.ClaimStatus \r\n                             WHEN 2 THEN 'Closed'   \r\n                             WHEN 1 THEN 'Open' \r\n                             WHEN 3 THEN 'Draft' \r\n                             END as ClaimStatus  FROM Quality_Claim QC\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=QC.VendorID \r\n                            INNER JOIN Arrival_Report AR ON AR.SysDocID=QC.SourceSysDocID and AR.VoucherID=QC.SourceVoucherID\r\n                            WHERE 1=1  ";
			if (!showClose)
			{
				text3 += "and ClaimStatus NOT IN ('2') OR ClaimStatus IS NULL";
			}
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND QC.DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Quality_Claim", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenQualityClaims(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT QC.SysDocID [Doc ID],QC.VoucherID [Number],QC.SourceVoucherID as ArrivalDOC,QC.DateInspected as [Inspected Date] FROM Quality_Claim QC  WHERE ClaimStatus=1 AND VendorID='" + vendorID + "'");
				FillDataSet(dataSet, "Quality_Claim", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
