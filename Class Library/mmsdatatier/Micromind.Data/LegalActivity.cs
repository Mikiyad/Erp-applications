using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class LegalActivity : StoreObject
	{
		public const string LEGALACTIVITY_TABLE = "Legal_Activity";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string ACTIVITYNAME_PARM = "@ActivityName";

		private const string ACTIONNAME_PARM = "@ActionName";

		private const string CASECLIENT1_PARM = "@CaseClient1";

		private const string CASECLIENT2_PARM = "@CaseClient2";

		private const string CASEPARTYID_PARM = "@CasePartyID";

		private const string LAWYERID_PARM = "@LawyerID";

		private const string STATUSID_PARM = "@StatusID";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string DATETIME_PARM = "@ActivityDateTime";

		private const string PARENTSYSDOCID_PARM = "@ParentSysDocID";

		private const string PARENTVOUCHERID_PARM = "@ParentVoucherID";

		private const string FILENO_PARM = "@FileNo";

		private const string CASETYPEID_PARM = "@CaseTypeID";

		private const string CONTACTID_PARM = "@ContactID";

		private const string OWNERID_PARM = "@OwnerID";

		private const string ACTDATETIME_PARM = "@ActDateTime";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public LegalActivity(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Legal_Activity", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ActivityDateTime", "@ActivityDateTime"), new FieldValue("CaseClient1", "@CaseClient1"), new FieldValue("CaseClient2", "@CaseClient2"), new FieldValue("ActivityName", "@ActivityName"), new FieldValue("ActionName", "@ActionName"), new FieldValue("CasePartyID", "@CasePartyID"), new FieldValue("LawyerID", "@LawyerID"), new FieldValue("StatusID", "@StatusID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("ParentSysDocID", "@ParentSysDocID"), new FieldValue("ParentVoucherID", "@ParentVoucherID"), new FieldValue("FileNo", "@FileNo"), new FieldValue("CaseTypeID", "@CaseTypeID"), new FieldValue("ContactID", "@ContactID"), new FieldValue("OwnerID", "@OwnerID"), new FieldValue("ActDateTime", "@ActDateTime"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Legal_Activity", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ActivityName", SqlDbType.NVarChar);
			parameters.Add("@ActionName", SqlDbType.NVarChar);
			parameters.Add("@CaseClient1", SqlDbType.NVarChar);
			parameters.Add("@CaseClient2", SqlDbType.NVarChar);
			parameters.Add("@CasePartyID", SqlDbType.NVarChar);
			parameters.Add("@LawyerID", SqlDbType.NVarChar);
			parameters.Add("@StatusID", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@ParentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ParentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@FileNo", SqlDbType.NVarChar);
			parameters.Add("@CaseTypeID", SqlDbType.NVarChar);
			parameters.Add("@ActivityDateTime", SqlDbType.DateTime);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@OwnerID", SqlDbType.NVarChar);
			parameters.Add("@ActDateTime", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ActivityName"].SourceColumn = "ActivityName";
			parameters["@ActionName"].SourceColumn = "ActionName";
			parameters["@CaseClient1"].SourceColumn = "CaseClient1";
			parameters["@CaseClient2"].SourceColumn = "CaseClient2";
			parameters["@CasePartyID"].SourceColumn = "CasePartyID";
			parameters["@LawyerID"].SourceColumn = "LawyerID";
			parameters["@StatusID"].SourceColumn = "StatusID";
			parameters["@ParentVoucherID"].SourceColumn = "ParentVoucherID";
			parameters["@ParentSysDocID"].SourceColumn = "ParentSysDocID";
			parameters["@FileNo"].SourceColumn = "FileNo";
			parameters["@CaseTypeID"].SourceColumn = "CaseTypeID";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@ActivityDateTime"].SourceColumn = "ActivityDateTime";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@OwnerID"].SourceColumn = "OwnerID";
			parameters["@ActDateTime"].SourceColumn = "ActDateTime";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertUpdateLegalActivity(LegalActivityData crmactivityData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			new DataSet();
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(crmactivityData, "Legal_Activity", insertUpdateCommand) : Update(crmactivityData, "Legal_Activity", insertUpdateCommand));
				string text = crmactivityData.LegalActivityTable.Rows[0]["SysDocID"].ToString();
				string text2 = crmactivityData.LegalActivityTable.Rows[0]["VoucherID"].ToString();
				string text3 = crmactivityData.LegalActivityTable.Rows[0]["ParentSysDocID"].ToString();
				string text4 = crmactivityData.LegalActivityTable.Rows[0]["ParentVoucherID"].ToString();
				string text5 = crmactivityData.LegalActivityTable.Rows[0]["StatusID"].ToString();
				string exp = "UPDATE Legal_Actions SET StatusID=' " + text5.TrimStart().TrimEnd() + "' WHERE SysDocID ='" + text3 + "' AND VoucherID ='" + text4 + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (isUpdate)
				{
					AddActivityLog("Legal Activity", text2, text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Legal Activity", text2, text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Legal_Activity", "SysDocID", text, "VoucherID", text2, sqlTransaction, !isUpdate);
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(text, text2, "Legal_Activity", "VoucherID", sqlTransaction);
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

		public DataSet GetLeaglActivityToPrint(string sysDocID, string voucherID)
		{
			return GetLeaglActivityToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetLeaglActivityToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = "SELECT  LA.*,FD.NextFollowupDate,FD.ThisFollowupDate, C.CustomerName,L.LawyerName,CP.CasePartyID,GL.GenericListName as StatusField FROM Legal_Activity LA INNER JOIN Customer C ON LA.CustomerID = C.CustomerID LEFT JOIN Case_Party CP ON CP.CasePartyID = LA.CasePartyID LEFT JOIN Lawyer L ON L.LawyerID = LA.LawyerID LEFT JOIN Generic_List GL ON GL.GenericListID = LA.StatusID LEFT JOIN Lead_Followup_Details FD ON LA.SysDocID = FD.SourceSysDocID AND LA.VoucherID = FD.SourceVoucherID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Legal_Activity", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public LegalActivityData GetLegalActivity()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Legal_Activity");
			LegalActivityData legalActivityData = new LegalActivityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(legalActivityData, "Legal_Activity", sqlBuilder);
			return legalActivityData;
		}

		public bool DeleteLegalActivity(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Legal_Activity WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Legal Activity", voucherID, sysDocID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public LegalActivityData GetLegalActivityByID(string sysDocID, string voucherID, string AnalysisID)
		{
			try
			{
				LegalActivityData legalActivityData = new LegalActivityData();
				string textCommand = "SELECT * FROM Legal_Activity  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(legalActivityData, "Legal_Activity", textCommand);
				return legalActivityData;
			}
			catch
			{
				throw;
			}
		}

		public LegalActivityData GetCustomerActivityByID(string sysDocID, string voucherID)
		{
			try
			{
				LegalActivityData legalActivityData = new LegalActivityData();
				string textCommand = "SELECT * FROM Activity  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' AND RelatedType = ' " + 2 + "'";
				FillDataSet(legalActivityData, "Legal_Activity", textCommand);
				return legalActivityData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetLegalActivityListByLeadID(CRMRelatedTypes leadType, string SysDocID, string leadID, DateTime from, DateTime to)
		{
			return GetLegalActivityList(leadType, SysDocID, leadID, from, to);
		}

		public DataSet GetCaseHistoryReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCustomer, string fromLawyer, string toLawyer, string fileNumber, string sysDociD, string VoucherID)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(ToDate);
			string text3 = "SELECT  LA.*\r\n                          FROM  Legal_Actions LA INNER JOIN\r\n                         Case_Client C ON LA.CaseClient1 = C.CaseClientID\r\n                         LEFT JOIN Case_Client C1 ON LA.CaseClient2 = C1.CaseClientID\r\n\t\t\t\t\t\t LEFT JOIN Case_Party CP ON CP.CasePartyID=LA.CasePartyID\r\n\t\t\t\t\t\t LEFT JOIN Lawyer L ON L.LawyerID=LA.LawyerID\r\n\t\t\t\t\t\t LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID ";
			text3 = text3 + " where ActionDateTime BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (sysDociD != "" && VoucherID != "")
			{
				text3 = text3 + " AND LA.SysDocID = '" + sysDociD + "' AND LA.VoucherID='" + VoucherID + "' ";
			}
			if (FromCustomer != "")
			{
				text3 = text3 + " AND LA.CaseClient1 BETWEEN '" + FromCustomer + "' AND '" + ToCustomer + "' ";
			}
			if (fromLawyer != "")
			{
				text3 = text3 + " AND LA.LawyerID BETWEEN '" + fromLawyer + "' AND '" + toLawyer + "' ";
			}
			if (fileNumber != "")
			{
				text3 = text3 + " AND (LA.FileNo = '" + fileNumber + "' OR (SELECT FileNo FROM Legal_Activity WHERE SysDocID=LA.ParentSysDocID AND VoucherID=LA.ParentVoucherID)='" + fileNumber + "')";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "CaseStatusActions", text3);
			new DataSet();
			text3 = "SELECT LA.*,CC.CaseClientName,CT.FirstName,CT.LastName,U.UserName FROM Legal_Activity LA INNER JOIN Legal_Actions LS\r\n                    ON LA.ParentSysDocID=LS.SysDocID AND LA.ParentVoucherID=LS.VoucherID\r\n                    LEFT JOIN Case_Client CC ON LA.CaseClient1=CC.CaseClientID\r\n                    LEFT JOIN Case_Client C1 ON LA.CaseClient2 = C1.CaseClientID\r\n                    LEFT JOIN Contact CT ON CT.ContactID=LA.ContactID\r\n                    LEFT JOIN Users U ON U.UserID=LA.OwnerID\r\n                    WHERE LA.ParentSysDocID = '" + sysDociD + "' AND LA.ParentVoucherID='" + VoucherID + "' ";
			FillDataSet(dataSet, "CaseStatusActivity", text3);
			dataSet.Relations.Add("CaseActivityHistory", new DataColumn[2]
			{
				dataSet.Tables["CaseStatusActions"].Columns["SysDocID"],
				dataSet.Tables["CaseStatusActions"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["CaseStatusActivity"].Columns["ParentSysDocID"],
				dataSet.Tables["CaseStatusActivity"].Columns["ParentVoucherID"]
			}, createConstraints: false);
			text3 = "select FD.*,GL.GenericListName as StatusField, (SELECT FileNo FROM Legal_Activity WHERE SysDocID=LA.ParentSysDocID AND VoucherID=LA.ParentVoucherID) AS [Parent_File],\r\n        (select  LawyerName from Lead_Followup_Details FD1 LEFT JOIN Lawyer ON FD1.ThisFollowupByID=Lawyer.LawyerID where FD1.FollowupID=FD.FollowupID)AS ThisFollowUPLawyer, \r\n        (select  LawyerName from Lead_Followup_Details FD1 LEFT JOIN Lawyer ON FD1.NextFollowupByID=Lawyer.LawyerID where FD1.FollowupID=FD.FollowupID)AS NextFollowUPLawyer from Lead_Followup_Details FD\r\n\r\n                    INNER JOIN Legal_Activity LA ON  FD.SourceSysDocID=LA.SysDocID AND FD.SourceVoucherID=LA.VoucherID \r\n                    LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID";
			text3 = text3 + " where ActivityDateTime BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (FromCustomer != "")
			{
				text3 = text3 + " AND LA.CaseClient1 BETWEEN '" + FromCustomer + "' AND '" + ToCustomer + "' ";
			}
			if (fromLawyer != "")
			{
				text3 = text3 + " AND LA.LawyerID BETWEEN '" + fromLawyer + "' AND '" + toLawyer + "' ";
			}
			if (fileNumber != "")
			{
				text3 = text3 + " AND (LA.FileNo = '" + fileNumber + "' OR (SELECT FileNo FROM Legal_Activity WHERE SysDocID=LA.ParentSysDocID AND VoucherID=LA.ParentVoucherID)='" + fileNumber + "')";
			}
			FillDataSet(dataSet, "CaseStatusFollowUp", text3);
			dataSet.Relations.Add("CaseHistory", new DataColumn[2]
			{
				dataSet.Tables["CaseStatusActivity"].Columns["SysDocID"],
				dataSet.Tables["CaseStatusActivity"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["CaseStatusFollowUp"].Columns["SourceSysDocID"],
				dataSet.Tables["CaseStatusFollowUp"].Columns["SourceVoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetCaseLawyerTrackReport(DateTime fromDate, DateTime ToDate, string fromLawyer, string toLawyer)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(ToDate);
			DataSet dataSet = new DataSet();
			string text3 = "select LA.LawyerID,LawyerName AS Lawyer, FD.*,GL.GenericListName as StatusField,\r\n                (select  LawyerName from Lead_Followup_Details FD1 LEFT JOIN Lawyer ON FD1.ThisFollowupByID=Lawyer.LawyerID where FD1.FollowupID=FD.FollowupID)AS ThisFollowUPLawyer,\r\n                 (select  LawyerName from Lead_Followup_Details FD1 LEFT JOIN Lawyer ON FD1.NextFollowupByID=Lawyer.LawyerID where FD1.FollowupID=FD.FollowupID)AS NextFollowUPLawyer \r\n                  from Legal_Activity LA\r\n                    INNER JOIN Lead_Followup_Details FD ON  FD.SourceSysDocID=LA.SysDocID AND FD.SourceVoucherID=LA.VoucherID \r\n                    LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID \r\n\t\t\t\t\tLEFT JOIN Lawyer ON LA.LawyerID=Lawyer.LawyerID";
			text3 = text3 + " where (ActivityDateTime BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 = text3 + " OR  ThisFollowupDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 = text3 + " OR  NextFollowupDate BETWEEN '" + text + "' AND '" + text2 + "') ";
			if (fromLawyer != "")
			{
				text3 = text3 + " AND (LA.LawyerID BETWEEN '" + fromLawyer + "' AND '" + toLawyer + "' ";
				text3 = text3 + " OR  ThisFollowupByID BETWEEN '" + fromLawyer + "' AND '" + toLawyer + "' ";
				text3 = text3 + " OR  NextFollowupByID BETWEEN '" + fromLawyer + "' AND '" + toLawyer + "') ";
			}
			FillDataSet(dataSet, "CaseLawyerTrack", text3);
			return dataSet;
		}

		public DataSet GetPendingCasesReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCustomer, string StatusID)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(ToDate);
			string text3 = "SELECT  LA.*,LT.*, C.CaseClientName,L.LawyerName,CP.CasePartyID,GL.GenericListName as StatusField, UserName as ContactName\r\n                            FROM  Legal_Actions LA INNER JOIN\r\n                            Case_Client C ON LA.CaseClient1 = C.CaseClientID\r\n                            LEFT JOIN Legal_Activity LT ON LA.SysDocID=LT.ParentSysDocID AND LA.VoucherID=LT.ParentVoucherID\r\n                            LEFT JOIN Case_Party CP ON CP.CasePartyID=LA.CasePartyID\r\n                            LEFT JOIN Lawyer L ON L.LawyerID=LA.LawyerID\r\n                            LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID  \r\n                            LEFT JOIN Users ON  LT.ContactID=Users.UserID ";
			text3 = text3 + " where ActionDateTime BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (FromCustomer != "")
			{
				text3 = text3 + " AND LA.CustomerID BETWEEN '" + FromCustomer + "' AND '" + ToCustomer + "' ";
			}
			if (StatusID != "")
			{
				text3 = text3 + " AND LA.StatusID='" + StatusID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PendingCaseDetails", text3);
			return dataSet;
		}

		public DataSet GetLegalActivityList(CRMRelatedTypes leadType, string SysDocID, string leadID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ACT.SysDocID [Doc ID], ACT.VoucherID [Number],\r\n                                ActivityName AS  [Activity Name], ACT.ActionName ,FileNo, CaseClient1 as Defendant ,CaseClient2 as Plantiff , ACT.ContactID,  ACT.OwnerID AS [Activity By],\r\n                                StatusID AS Status, ACT.Note,ActivityDateTime [Date], ActDateTime from  Legal_Activity ACT   LEFT JOIN Case_Client On Case_Client.CaseClientID=ACT.CaseClient1\r\n                                 LEFT JOIN Case_Client CC On CC.CaseClientID=ACT.CaseClient2\r\n                                LEFT JOIN Contact On ACT.ContactID=Contact.ContactID                     \r\n                                WHERE 1=1";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ActivityDateTime Between '" + text + "' AND '" + text2 + "' ";
			}
			if (leadID != "")
			{
				text3 = text3 + " AND Act.ParentSysDocID = '" + SysDocID + "' AND Act.ParentVoucherID = '" + leadID + "'";
			}
			FillDataSet(dataSet, "Legal_Activity", text3);
			return dataSet;
		}

		public DataSet GetCustomerActivityList(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ACT.SysDocID [Doc ID], ACT.VoucherID [Number],\r\n                               ActivityName [Activity Name], \r\n                        (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN 'Lead' \tWHEN 2 THEN 'Customer' WHEN 3 THEN 'Opportunity'\r\n\t\t\t\t\t\tELSE '' END) AS [Account Type], (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN Lead.LeadName\r\n\t\t\t\t\t\tWHEN 2 THEN CUS.CustomerName WHEN 3 THEN OP.OpportunityName\r\n\t\t\t\t\t\tELSE '' END) AS [Account Name],Con.FirstName + ' ' + Con.LastName AS Contact,\r\n                                CASE ActivityType WHEN 0 THEN 'Call' \r\n                                WHEN 1 THEN 'Email' WHEN 2 THEN 'Task'  WHEN 3 THEN 'Fax' \r\n                                WHEN 4 THEN 'Letter' WHEN 5 THEN 'Appointment' WHEN 6 THEN 'Compaign' \r\n                                WHEN 7 THEN 'Service' WHEN 1000 THEN 'Custom' END AS [Activity Type],ACT.OwnerID [Performed By],  ActivityDateTime [Date], ACT.Note\r\n                           FROM Legal_Activity ACT LEFT OUTER JOIN Lead ON Lead.LeadID = Act.RelatedID\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CustomerID = Act.RelatedID\r\n                            LEFT OUTER JOIN Opportunity OP ON OP.OpportunityID = Act.RelatedID\r\n                            LEFT OUTER JOIN Contact CON ON CON.ContactID = ACT.ContactID\r\n                                --Checking user access right for the doc id\r\n                                WHERE (ACT.SysDocID NOT IN (SELECT SysDocID FROM System_Doc_Entity_Link)\r\n\t\t\t\t\t\t\t\tOR Act.SysDocID IN (SELECT SysDocID FROM System_Doc_Entity_Link SDL WHERE SDL.EntityType = 5 AND SDL.EntityID = '" + base.DBConfig.UserID + "'))";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ActivityDateTime Between '" + text + "' AND '" + text2 + "'AND Act.RelatedType = '" + (int)leadType + "'";
			}
			if (leadID != "")
			{
				text3 = text3 + "AND Act.RelatedID = '" + leadID + "'";
			}
			FillDataSet(dataSet, "Legal_Activity", text3);
			return dataSet;
		}

		public DataSet GetLegalActivityComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActivityID [Code], ActivityName [Name]\r\n                           FROM Legal_Activity ORDER BY ActivityID, ActivityName";
			FillDataSet(dataSet, "Legal_Activity", textCommand);
			return dataSet;
		}
	}
}
