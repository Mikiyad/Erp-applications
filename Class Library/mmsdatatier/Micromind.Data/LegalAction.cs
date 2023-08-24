using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class LegalAction : StoreObject
	{
		public const string LEGALACTION_TABLE = "Legal_Actions";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string ACTIONNAME_PARM = "@ActionName";

		private const string CASECLIENT1_PARM = "@CaseClient1";

		private const string CASECLIENT2_PARM = "@CaseClient2";

		private const string CASECLIENT_PARM = "@CaseClient";

		private const string CASEPARTYID_PARM = "@CasePartyID";

		private const string LAWYERID_PARM = "@LawyerID";

		private const string STATUSID_PARM = "@StatusID";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string DATETIME_PARM = "@ActionDateTime";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PARENTSYSDOCID_PARM = "@ParentSysDocID";

		private const string PARENTVOUCHERID_PARM = "@ParentVoucherID";

		private const string FILENO_PARM = "@FileNo";

		private const string CASETYPEID_PARM = "@CaseTypeID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string CLIENTTYPE_PARM = "@ClientType";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public LegalAction(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Legal_Actions", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ActionDateTime", "@ActionDateTime"), new FieldValue("Caseclient1", "@CaseClient1"), new FieldValue("Caseclient2", "@CaseClient2"), new FieldValue("ActionName", "@ActionName"), new FieldValue("CasePartyID", "@CasePartyID"), new FieldValue("LawyerID", "@LawyerID"), new FieldValue("StatusID", "@StatusID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("ParentSysDocID", "@ParentSysDocID"), new FieldValue("ParentVoucherID", "@ParentVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("ClientType", "@ClientType"), new FieldValue("FileNo", "@FileNo"), new FieldValue("CaseTypeID", "@CaseTypeID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Legal_Actions", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ActionName", SqlDbType.NVarChar);
			parameters.Add("@CaseClient1", SqlDbType.NVarChar);
			parameters.Add("@CaseClient2", SqlDbType.NVarChar);
			parameters.Add("@CasePartyID", SqlDbType.NVarChar);
			parameters.Add("@LawyerID", SqlDbType.NVarChar);
			parameters.Add("@StatusID", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@ParentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ParentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@FileNo", SqlDbType.NVarChar);
			parameters.Add("@CaseTypeID", SqlDbType.NVarChar);
			parameters.Add("@ClientType", SqlDbType.Int);
			parameters.Add("@ActionDateTime", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ActionName"].SourceColumn = "ActionName";
			parameters["@CaseClient1"].SourceColumn = "Caseclient1";
			parameters["@CaseClient2"].SourceColumn = "Caseclient2";
			parameters["@CasePartyID"].SourceColumn = "CasePartyID";
			parameters["@LawyerID"].SourceColumn = "LawyerID";
			parameters["@StatusID"].SourceColumn = "StatusID";
			parameters["@ParentVoucherID"].SourceColumn = "ParentVoucherID";
			parameters["@ParentSysDocID"].SourceColumn = "ParentSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@FileNo"].SourceColumn = "FileNo";
			parameters["@CaseTypeID"].SourceColumn = "CaseTypeID";
			parameters["@ClientType"].SourceColumn = "ClientType";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@ActionDateTime"].SourceColumn = "ActionDateTime";
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

		private string GetInsertUpdateLegalActionListText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Legal_Actions_Client_List", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Caseclient", "@CaseClient"), new FieldValue("ClientType", "@ClientType"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Legal_Actions", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateLegalActionListCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateLegalActionListText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateLegalActionListText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CaseClient", SqlDbType.NVarChar);
			parameters.Add("@ClientType", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CaseClient"].SourceColumn = "Caseclient";
			parameters["@ClientType"].SourceColumn = "ClientType";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertUpdateLegalAction(LegalActionData crmactivityData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			DataSet dataSet = new DataSet();
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				if (isUpdate)
				{
					flag = Update(crmactivityData, "Legal_Actions", insertUpdateCommand);
				}
				else
				{
					flag = Insert(crmactivityData, "Legal_Actions", insertUpdateCommand);
					if (crmactivityData.Tables["Analysis"].Rows.Count > 0)
					{
						DataTable table = crmactivityData.Tables["Analysis"].Copy();
						dataSet.Tables.Add(table);
						flag &= new Analysis(base.DBConfig).InsertAutoAnalysis(dataSet);
					}
				}
				string text = crmactivityData.LegalActionTable.Rows[0]["SysDocID"].ToString();
				string text2 = crmactivityData.LegalActionTable.Rows[0]["VoucherID"].ToString();
				if (flag && crmactivityData.Tables.Contains("Legal_Actions_Client_List") && crmactivityData.Tables["Legal_Actions_Client_List"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateLegalActionListCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					if (isUpdate)
					{
						flag &= DeleteLegalCaseClientListRows(text, text2, sqlTransaction);
					}
					if (crmactivityData.Tables["Legal_Actions_Client_List"].Rows.Count > 0)
					{
						flag &= Insert(crmactivityData, "Legal_Actions_Client_List", insertUpdateCommand);
					}
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				if (isUpdate)
				{
					AddActivityLog("Legal Action", text2, text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Legal Action", text2, text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Legal_Actions", "SysDocID", text, "VoucherID", text2, sqlTransaction, !isUpdate);
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(text, text2, "Legal_Actions", "VoucherID", sqlTransaction);
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

		internal bool DeleteLegalCaseClientListRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Legal_Actions_Client_List WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
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
				FillDataSet(dataSet, "Legal_Actions", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public LegalActionData GetLegalAction()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Legal_Actions");
			LegalActionData legalActionData = new LegalActionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(legalActionData, "Legal_Actions", sqlBuilder);
			return legalActionData;
		}

		public bool DeleteLegalAction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Legal_Actions WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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

		public LegalActionData GetLegalActionByID(string sysDocID, string voucherID, string AnalysisID)
		{
			try
			{
				LegalActionData legalActionData = new LegalActionData();
				string textCommand = "SELECT * FROM Legal_Actions  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(legalActionData, "Legal_Actions", textCommand);
				textCommand = "SELECT * FROM Legal_Actions_Client_List  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(legalActionData, "Legal_Actions_Client_List", textCommand);
				return legalActionData;
			}
			catch
			{
				throw;
			}
		}

		public LegalActionData GetLegalActionExpenseByID(string sysDocID, string voucherID, string AnalysisID)
		{
			try
			{
				LegalActionData legalActionData = new LegalActionData();
				if (!string.IsNullOrEmpty(AnalysisID))
				{
					string text = "";
					text = "SELECT JD.SysDocID,JD.VoucherID,J.JournalDate,J.Reference,JD.Debit as Amount FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                        INNER JOIN ACCOUNT A ON A.AccountID = JD.AccountID INNER JOIN Account_Group AG ON A.GroupID\r\n                          = AG.GroupID WHERE JD.Debit <> 0 AND AG.TypeID = 4 AND JD.AnalysisID='" + AnalysisID + "'";
					FillDataSet(legalActionData, "Legal_Expense_Details", text);
				}
				return legalActionData;
			}
			catch
			{
				throw;
			}
		}

		public LegalActionData GetCustomerActivityByID(string sysDocID, string voucherID)
		{
			try
			{
				LegalActionData legalActionData = new LegalActionData();
				string textCommand = "SELECT * FROM Activity  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' AND RelatedType = ' " + 2 + "'";
				FillDataSet(legalActionData, "Legal_Actions", textCommand);
				return legalActionData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetLegalActionListByLeadID(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			return GetLegalActionList(leadType, leadID, from, to);
		}

		public DataSet GetCaseHistoryReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCustomer, string FromClass, string ToClass, string FromGroup, string ToGroup, string fromLawyer, string toLawyer, string fileNumber)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(ToDate);
			string text3 = "SELECT  LA.*,(SELECT FileNo FROM Legal_Activity WHERE SysDocID=LA.ParentSysDocID AND VoucherID=LA.ParentVoucherID) AS [Parent_File],FD.NextFollowupDate,FD.ThisFollowupDate, C.CustomerName,L.LawyerName,CP.CasePartyID,GL.GenericListName as StatusField\r\n                          FROM  Legal_Activity LA INNER JOIN\r\n                         Customer C ON LA.CustomerID = C.CustomerID\r\n\t\t\t\t\t\t LEFT JOIN Case_Party CP ON CP.CasePartyID=LA.CasePartyID\r\n\t\t\t\t\t\t LEFT JOIN Lawyer L ON L.LawyerID=LA.LawyerID\r\n\t\t\t\t\t\t LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID \r\n\t\t\t\t\t\t LEFT JOIN Lead_Followup_Details FD ON LA.SysDocID=FD.SourceSysDocID AND LA.VoucherID=FD.SourceVoucherID";
			text3 = text3 + " where ActivityDateTime BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (FromCustomer != "")
			{
				text3 = text3 + " AND LA.CustomerID BETWEEN '" + FromCustomer + "' AND '" + ToCustomer + "' ";
			}
			if (FromClass != "")
			{
				text3 = text3 + " AND CustomerClassID>='" + FromClass + "'";
			}
			if (ToClass != "")
			{
				text3 = text3 + " AND CustomerClassID<='" + ToClass + "'";
			}
			if (FromGroup != "")
			{
				text3 = text3 + " AND CustomerGroupID>='" + FromGroup + "'";
			}
			if (ToGroup != "")
			{
				text3 = text3 + " AND CustomerGroupID<='" + ToGroup + "'";
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
			FillDataSet(dataSet, "CaseStatusActivity", text3);
			new DataSet();
			text3 = "select FD.*,GL.GenericListName as StatusField, (SELECT FileNo FROM Legal_Activity WHERE SysDocID=LA.ParentSysDocID AND VoucherID=LA.ParentVoucherID) AS [Parent_File],\r\n        (select  LawyerName from Lead_Followup_Details FD1 LEFT JOIN Lawyer ON FD1.ThisFollowupByID=Lawyer.LawyerID where FD1.FollowupID=FD.FollowupID)AS ThisFollowUPLawyer, \r\n        (select  LawyerName from Lead_Followup_Details FD1 LEFT JOIN Lawyer ON FD1.NextFollowupByID=Lawyer.LawyerID where FD1.FollowupID=FD.FollowupID)AS NextFollowUPLawyer from Lead_Followup_Details FD\r\n\r\n                    INNER JOIN Legal_Activity LA ON  FD.SourceSysDocID=LA.SysDocID AND FD.SourceVoucherID=LA.VoucherID \r\n                    LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID";
			text3 = text3 + " where ActivityDateTime BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (FromCustomer != "")
			{
				text3 = text3 + " AND LA.CustomerID BETWEEN '" + FromCustomer + "' AND '" + ToCustomer + "' ";
			}
			if (FromClass != "")
			{
				text3 = text3 + " AND CustomerClassID>='" + FromClass + "'";
			}
			if (ToClass != "")
			{
				text3 = text3 + " AND CustomerClassID<='" + ToClass + "'";
			}
			if (FromGroup != "")
			{
				text3 = text3 + " AND CustomerGroupID>='" + FromGroup + "'";
			}
			if (ToGroup != "")
			{
				text3 = text3 + " AND CustomerGroupID<='" + ToGroup + "'";
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

		public DataSet GetPendingCasesReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCustomer, string FromClass, string ToClass, string FromGroup, string ToGroup, string StatusID)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(ToDate);
			string text3 = "SELECT  LA.*,C.CustomerName,L.LawyerName,CP.CasePartyID,GL.GenericListName as StatusField\r\n                          FROM  Legal_Activity LA INNER JOIN\r\n                         Customer C ON LA.CustomerID = C.CustomerID\r\n\t\t\t\t\t\t LEFT JOIN Case_Party CP ON CP.CasePartyID=LA.CasePartyID\r\n\t\t\t\t\t\t LEFT JOIN Lawyer L ON L.LawyerID=LA.LawyerID\r\n\t\t\t\t\t\t LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID ";
			text3 = text3 + " where ActivityDateTime BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (FromCustomer != "")
			{
				text3 = text3 + " AND LA.CustomerID BETWEEN '" + FromCustomer + "' AND '" + ToCustomer + "' ";
			}
			if (FromClass != "")
			{
				text3 = text3 + " AND CustomerClassID>='" + FromClass + "'";
			}
			if (ToClass != "")
			{
				text3 = text3 + " AND CustomerClassID<='" + ToClass + "'";
			}
			if (FromGroup != "")
			{
				text3 = text3 + " AND CustomerGroupID>='" + FromGroup + "'";
			}
			if (ToGroup != "")
			{
				text3 = text3 + " AND CustomerGroupID<='" + ToGroup + "'";
			}
			if (StatusID != "")
			{
				text3 = text3 + " AND StatusID='" + StatusID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PendingCaseDetails", text3);
			return dataSet;
		}

		public DataSet GetLegalActionList(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "select t.SysDocID AS [Doc ID], t.VoucherID [Number],c1.CaseClientName AS Defendant, c2.CaseClientName AS Plantiff, t.FileNo, t.Note, STUFF((SELECT distinct ', ' + c.CaseClientName\r\n                            from Legal_Actions_Client_List t1\r\n                            LEFT JOIN Case_Client C ON t1.CaseClient=C.CaseClientID \r\n                            where t.SysDocID = t1.SysDocID AND t.VoucherID=t1.VoucherID\r\n                            FOR XML PATH(''), TYPE\r\n                            ).value('.', 'NVARCHAR(MAX)') \r\n                            ,1,2,'') Clients  from Legal_Actions t    \r\n                            LEFT JOIN Case_Client C1 ON t.CaseClient1=c1.CaseClientID \r\n                            LEFT JOIN Case_Client C2 ON t.CaseClient2=c2.CaseClientID  where 1=1";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ActionDateTime Between '" + text + "' AND '" + text2 + "' ";
			}
			if (leadID != "")
			{
				text3 = text3 + " AND Act.RelatedType = '" + (int)leadType + "' AND Act.RelatedID = '" + leadID + "'";
			}
			FillDataSet(dataSet, "Legal_Actions", text3);
			return dataSet;
		}

		public DataSet GetLegalActionReportList(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ACT.SysDocID [Doc ID], ACT.VoucherID [Number],\r\n                               FileNo \r\n                              FROM Legal_Actions ACT                            \r\n                                WHERE 1=1";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ActionDateTime Between '" + text + "' AND '" + text2 + "' ";
			}
			if (leadID != "")
			{
				text3 = text3 + " AND Act.RelatedType = '" + (int)leadType + "' AND Act.RelatedID = '" + leadID + "'";
			}
			FillDataSet(dataSet, "Legal_Actions", text3);
			return dataSet;
		}

		public DataSet GetCustomerActivityList(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ACT.SysDocID [Doc ID], ACT.VoucherID [Number],\r\n                               ActivityName [Activity Name], \r\n                        (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN 'Lead' \tWHEN 2 THEN 'Customer' WHEN 3 THEN 'Opportunity'\r\n\t\t\t\t\t\tELSE '' END) AS [Account Type], (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN Lead.LeadName\r\n\t\t\t\t\t\tWHEN 2 THEN CUS.CustomerName WHEN 3 THEN OP.OpportunityName\r\n\t\t\t\t\t\tELSE '' END) AS [Account Name],Con.FirstName + ' ' + Con.LastName AS Contact,\r\n                                CASE ActivityType WHEN 0 THEN 'Call' \r\n                                WHEN 1 THEN 'Email' WHEN 2 THEN 'Task'  WHEN 3 THEN 'Fax' \r\n                                WHEN 4 THEN 'Letter' WHEN 5 THEN 'Appointment' WHEN 6 THEN 'Compaign' \r\n                                WHEN 7 THEN 'Service' WHEN 1000 THEN 'Custom' END AS [Activity Type],ACT.OwnerID [Performed By],  ActivityDateTime [Date], ACT.Note\r\n                           FROM Legal_Actions ACT LEFT OUTER JOIN Lead ON Lead.LeadID = Act.RelatedID\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CustomerID = Act.RelatedID\r\n                            LEFT OUTER JOIN Opportunity OP ON OP.OpportunityID = Act.RelatedID\r\n                            LEFT OUTER JOIN Contact CON ON CON.ContactID = ACT.ContactID\r\n                                --Checking user access right for the doc id\r\n                                WHERE (ACT.SysDocID NOT IN (SELECT SysDocID FROM System_Doc_Entity_Link)\r\n\t\t\t\t\t\t\t\tOR Act.SysDocID IN (SELECT SysDocID FROM System_Doc_Entity_Link SDL WHERE SDL.EntityType = 5 AND SDL.EntityID = '" + base.DBConfig.UserID + "'))";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ActivityDateTime Between '" + text + "' AND '" + text2 + "'AND Act.RelatedType = '" + (int)leadType + "'";
			}
			if (leadID != "")
			{
				text3 = text3 + "AND Act.RelatedID = '" + leadID + "'";
			}
			FillDataSet(dataSet, "Legal_Actions", text3);
			return dataSet;
		}

		public DataSet GetLegalActionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActivityID [Code], ActivityName [Name]\r\n                           FROM Legal_Activity ORDER BY ActivityID, ActivityName";
			FillDataSet(dataSet, "Legal_Actions", textCommand);
			return dataSet;
		}

		public DataSet GetCasePartyTypeList(bool isDefendant, bool plantiff)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActivityID [Code], ActivityName [Name]\r\n                           FROM Legal_Activity ORDER BY ActivityID, ActivityName";
			FillDataSet(dataSet, "Legal_Actions", textCommand);
			return dataSet;
		}

		public DataSet GetLegalDocIDList(string SysDocID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID AS [Doc ID],VoucherID AS Number,FileNo from Legal_Actions SD where SysDocID='" + SysDocID + "'";
			FillDataSet(dataSet, "Legal_Actions", textCommand);
			return dataSet;
		}

		public DataSet GetLegalActionHistory(List<Tuple<string, string>> list)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			string text2 = "";
			for (int i = 0; i < list.Count; i++)
			{
				text = text + "'" + list[i].Item1 + "'";
				text2 = text2 + "'" + list[i].Item2 + "'";
				if (i < list.Count - 1)
				{
					text += ",";
					text2 += ",";
				}
			}
			string str = "SELECT  LA.SysDocID,LA.FileNo,LA.ActionDateTime, LA.ActionName, LA.ParentSysDocID AS [Previous DocID], LA.ParentVoucherID AS [Previous DocNO], LA.SourceSysDocID AS [Parent DocID],LA.SourceVoucherID AS [Parent DocNO], C.CaseClientName  AS Defendant, C1.CaseClientName AS Plantiff\r\n                          FROM  Legal_Actions LA INNER JOIN\r\n                         Case_Client C ON LA.CaseClient1 = C.CaseClientID\r\n                         LEFT JOIN Case_Client C1 ON LA.CaseClient2 = C1.CaseClientID\r\n\t\t\t\t\t\t LEFT JOIN Case_Party CP ON CP.CasePartyID=LA.CasePartyID\r\n\t\t\t\t\t\t LEFT JOIN Lawyer L ON L.LawyerID=LA.LawyerID\r\n\t\t\t\t\t\t LEFT JOIN Generic_List GL ON GL.GenericListID=LA.StatusID ";
			str += " where 1=1 ";
			if (text != "" && text2 != "")
			{
				str = str + " AND LA.SysDocID IN (" + text + ") AND LA.VoucherID IN (" + text2 + ")  ";
			}
			FillDataSet(dataSet, "Legal_Actions", str);
			return dataSet;
		}
	}
}
