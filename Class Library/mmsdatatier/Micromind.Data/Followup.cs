using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Followup : StoreObject
	{
		public const string CRMFOLLOWUP_TABLE = "@Followup";

		private const string CRMFOLLOWUPID_PARM = "@FollowupID";

		private const string LEADID_PARM = "@LeadID";

		private const string THISFOLLOWUPDATE_PARM = "@ThisFollowupDate";

		private const string NEXTFOLLOWUPDATE_PARM = "@NextFollowupDate";

		private const string THISFOLLOWUPBYID_PARM = "@ThisFollowupByID";

		private const string NEXTFOLLOWUPBYID_PARM = "@NextFollowupByID";

		private const string THISFOLLOWUPSTATUSID_PARM = "@ThisFollowupStatusID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string CRMTYPE_PARM = "@CRMType";

		private const string REMARK_PARM = "@Remark";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public Followup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Lead_Followup_Details", new FieldValue("FollowupID", "@FollowupID", isUpdateConditionField: true), new FieldValue("LeadID", "@LeadID"), new FieldValue("ThisFollowupDate", "@ThisFollowupDate"), new FieldValue("NextFollowupDate", "@NextFollowupDate"), new FieldValue("ThisFollowupByID", "@ThisFollowupByID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("CRMType", "@CRMType"), new FieldValue("NextFollowupByID", "@NextFollowupByID"), new FieldValue("ThisFollowupStatusID", "@ThisFollowupStatusID"), new FieldValue("Remark", "@Remark"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Lead_Followup_Details", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@FollowupID", SqlDbType.NVarChar);
			parameters.Add("@LeadID", SqlDbType.NVarChar);
			parameters.Add("@ThisFollowupDate", SqlDbType.DateTime);
			parameters.Add("@NextFollowupDate", SqlDbType.DateTime);
			parameters.Add("@ThisFollowupByID", SqlDbType.NVarChar);
			parameters.Add("@NextFollowupByID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@CRMType", SqlDbType.Int);
			parameters.Add("@ThisFollowupStatusID", SqlDbType.NVarChar);
			parameters.Add("@Remark", SqlDbType.NText);
			parameters["@FollowupID"].SourceColumn = "FollowupID";
			parameters["@LeadID"].SourceColumn = "LeadID";
			parameters["@ThisFollowupDate"].SourceColumn = "ThisFollowupDate";
			parameters["@NextFollowupDate"].SourceColumn = "NextFollowupDate";
			parameters["@ThisFollowupByID"].SourceColumn = "ThisFollowupByID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@CRMType"].SourceColumn = "CRMType";
			parameters["@NextFollowupByID"].SourceColumn = "NextFollowupByID";
			parameters["@ThisFollowupStatusID"].SourceColumn = "ThisFollowupStatusID";
			parameters["@Remark"].SourceColumn = "Remark";
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

		public bool InsertCRMFollowup(CRMFollowupData crmfollowupData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(crmfollowupData, "Lead_Followup_Details", insertUpdateCommand);
				string text = crmfollowupData.CRMFollowupTable.Rows[0]["FollowupID"].ToString();
				AddActivityLog("CRMFollowup", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Lead_Followup_Details", "FollowupID", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateCRMFollowup(CRMFollowupData accountCRMFollowupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCRMFollowupData, "Lead_Followup_Details", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCRMFollowupData.CRMFollowupTable.Rows[0]["FollowupID"];
				UpdateTableRowByID("Lead_Followup_Details", "FollowupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCRMFollowupData.CRMFollowupTable.Rows[0]["LeadID"].ToString();
				AddActivityLog("CRMFollowup", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Lead_Followup_Details", "FollowupID", obj, sqlTransaction, isInsert: false);
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

		public CRMFollowupData GetCRMFollowup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lead_Followup_Details");
			CRMFollowupData cRMFollowupData = new CRMFollowupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(cRMFollowupData, "Lead_Followup_Details", sqlBuilder);
			return cRMFollowupData;
		}

		public bool DeleteCRMFollowup(string crmFollowupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Lead_Followup_Details WHERE FollowupID = '" + crmFollowupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CRMFollowup", crmFollowupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CRMFollowupData GetCRMFollowupByID(string id, string SourceSysDocID, string sourceVoucherID)
		{
			CRMFollowupData cRMFollowupData = new CRMFollowupData();
			string textCommand = "SELECT *\r\n                           FROM Lead_Followup_Details WHERE FollowupID = '" + id + "' AND  SourceVoucherID='" + sourceVoucherID + "' AND SourceSysDocID='" + SourceSysDocID + "' ";
			FillDataSet(cRMFollowupData, "Lead_Followup_Details", textCommand);
			return cRMFollowupData;
		}

		public DataSet GetCRMFollowupListByID(string id, string SourceSysDocID, string sourceVoucherID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FollowupID [Followup Code], \r\n                               FollowupName [Followup Name], Remark\r\n                           FROM Lead_Followup_Details WHERE FollowupID = '" + id + "' AND  SourceVoucherID='" + sourceVoucherID + "' AND SourceSysDocID='" + SourceSysDocID + "' ";
			FillDataSet(dataSet, "Lead_Followup_Details", textCommand);
			return dataSet;
		}

		public DataSet GetCRMFollowupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lead_Followup_Details");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "FollowupID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Lead_Followup_Details";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Lead_Followup_Details", sqlBuilder);
			return dataSet;
		}

		public DataSet GetFollowupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  FollowupID, ThisFollowupDate[Currentfollowup Date],NextFollowupDate[Nextfollowup Date],A.FullName AS [Followup By],B.FullName AS[Next Followup By], Remark FROM Lead_Followup_Details FOL LEFT JOIN Salesperson A ON A.SalespersonID=FOL.ThisFollowupByID LEFT JOIN Salesperson B ON B.SalespersonID=FOL.NextFollowupByID ";
			FillDataSet(dataSet, "Lead_Followup_Details", textCommand);
			return dataSet;
		}

		public DataSet GetCRMFollowupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FollowupID [Code], LeadID,ThisFollowupStatus,ThisFollowupDate,NextFollowupDate\r\n                           FROM Lead_Followup_Details ORDER BY FollowupID";
			FillDataSet(dataSet, "Lead_Followup_Details", textCommand);
			return dataSet;
		}

		public DataSet GetFollowupListByActivityID(CRMRelatedTypes activityType, string activityID, string sourceVoucherID, string sourceSysDocID, DateTime from, DateTime to)
		{
			return GetCRMFollowupList(activityType, activityID, sourceVoucherID, sourceSysDocID, from, to);
		}

		public DataSet GetCRMFollowupList(CRMRelatedTypes activityType, string activityID, string sourceVoucherID, string SourceSysDocID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT FollowupID,SourceSysDocID,SourceVoucherID,ThisFollowupDate,NextFollowupDate,ThisFollowupByID,NextFollowupByID,ThisFollowupStatusID,S1.UserName  AS [FollowupBy],S2.UserName  AS [NextFollowupBy],\r\n                    L1.LawyerName  AS [Follow_upBy],L2.LawyerName  AS [NextFollow_upBy],ThisFollowupStatusID AS [Status],CRMType               \r\n                          FROM Lead_Followup_Details LF \r\n\t\t\t\t\t\t   LEFT JOIN Users S1 ON S1.UserID=LF.ThisFollowupByID\r\n\t\t\t\t\t\t   LEFT JOIN Users S2 ON S2.UserID=LF.NextFollowupByID \r\n                           LEFT JOIN Lawyer L1 ON L1.LawyerID=LF.ThisFollowupByID\r\n\t\t\t\t\t\t   LEFT JOIN Lawyer L2 ON L2.LawyerID=LF.NextFollowupByID \r\n                            WHERE SourceVoucherID='" + sourceVoucherID + "' AND SourceSysDocID='" + SourceSysDocID + "' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND LF.DateCreated Between '" + text + "' AND '" + text2 + "' ";
			}
			text3 += " Order By FollowupID";
			FillDataSet(dataSet, "Lead_Followup_Details", text3);
			return dataSet;
		}
	}
}
