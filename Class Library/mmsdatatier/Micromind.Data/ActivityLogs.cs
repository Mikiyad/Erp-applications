using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ActivityLogs : StoreObject
	{
		private const string ACTIVITYLOGID_PARM = "@ActivityLogID";

		private const string USERID_PARM = "@UserID";

		private const string MACHINEID_PARM = "@MachineID";

		private const string ENTITYID_PARM = "@EntityID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string LOGDATE_PARM = "@LogDate";

		private const string ACTIVITYTYPE_PARM = "@ActivityType";

		private const string DESCRIPTION_PARM = "@Description";

		private const string AMOUNT_PARM = "@Amount";

		private const string PAYEE_PARM = "@Payee";

		private const string REFERENCEID_PARM = "@ReferenceID";

		private const string TRANSACTIONTYPE_PARM = "@TransactionType";

		private const string DATACOMBOTYPE_PARM = "@DataComboType";

		private const string LOGID_PARM = "@LogID";

		private const string BATCHREFERENCE_PARM = "@BatchReference";

		private const string BATCHDATE_PARM = "@BatchDate";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string OLDCOST_PARM = "@OldCost";

		private const string NEWCOST_PARM = "@NewCost";

		private const string OLDCOGS_PARM = "@OldCOGS";

		private const string NEWCOGS_PARM = "@NewCOGS";

		private const string TOTALDIFF_PARM = "@TotalDiff";

		private const string COGSACCOUNTID_PARM = "@COGSAccountID";

		private const string ASSETACCOUNTID_PARM = "@AssetAccountID";

		private const string VERSIONID_PARM = "@VersionID";

		private const string SCREENTYPE_PARM = "@ScreenType";

		private const string SCREENID_PARM = "@ScreenID";

		private const string DOCDATA_PARM = "@DocData";

		private const string DOCNUMBER_PARM = "@DocNumber";

		public bool CheckConcurrency = true;

		public ActivityLogs(Config config)
			: base(config)
		{
		}

		private string GetInsertDocVersionText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Doc_Version", new FieldValue("ScreenType", "@ScreenType"), new FieldValue("ScreenID", "@ScreenID"), new FieldValue("UserID", "@UserID"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("DocNumber", "@DocNumber"), new FieldValue("MachineID", "@MachineID"), new FieldValue("DocData", "@DocData"), new FieldValue("LogDate", "@LogDate"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertDocVersionCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertDocVersionText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@UserID", SqlDbType.NVarChar);
				parameters.Add("@ScreenType", SqlDbType.TinyInt);
				parameters.Add("@ScreenID", SqlDbType.NVarChar);
				parameters.Add("@DocNumber", SqlDbType.NVarChar);
				parameters.Add("@SysDocID", SqlDbType.NVarChar);
				parameters.Add("@LogDate", SqlDbType.DateTime);
				parameters.Add("@MachineID", SqlDbType.NVarChar);
				parameters.Add("@DocData", SqlDbType.Image);
				parameters["@UserID"].SourceColumn = "UserID";
				parameters["@ScreenType"].SourceColumn = "ScreenType";
				parameters["@ScreenID"].SourceColumn = "ScreenID";
				parameters["@LogDate"].SourceColumn = "LogDate";
				parameters["@MachineID"].SourceColumn = "MachineID";
				parameters["@DocData"].SourceColumn = "DocData";
				parameters["@SysDocID"].SourceColumn = "SysDocID";
				parameters["@DocNumber"].SourceColumn = "DocNumber";
			}
			return insertCommand;
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Activity Logs]", new FieldValue("UserID", "@UserID"), new FieldValue("MachineID", "@MachineID"), new FieldValue("EntityID", "@EntityID"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("LogDate", "@LogDate"), new FieldValue("ActivityType", "@ActivityType"), new FieldValue("Amount", "@Amount"), new FieldValue("Payee", "@Payee"), new FieldValue("TransactionType", "@TransactionType"), new FieldValue("DataComboType", "@DataComboType"), new FieldValue("ReferenceID", "@ReferenceID"), new FieldValue("Description", "@Description"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@UserID", SqlDbType.NVarChar);
				parameters.Add("@ActivityType", SqlDbType.TinyInt);
				parameters.Add("@LogDate", SqlDbType.DateTime);
				parameters.Add("@MachineID", SqlDbType.NVarChar);
				parameters.Add("@EntityID", SqlDbType.NVarChar);
				parameters.Add("@SysDocID", SqlDbType.NVarChar);
				parameters.Add("@Amount", SqlDbType.Money);
				parameters.Add("@Payee", SqlDbType.NVarChar);
				parameters.Add("@Description", SqlDbType.NVarChar);
				parameters.Add("@TransactionType", SqlDbType.TinyInt);
				parameters.Add("@DataComboType", SqlDbType.TinyInt);
				parameters.Add("@ReferenceID", SqlDbType.Int);
				parameters["@UserID"].SourceColumn = "UserID";
				parameters["@ActivityType"].SourceColumn = "ActivityType";
				parameters["@LogDate"].SourceColumn = "LogDate";
				parameters["@MachineID"].SourceColumn = "MachineID";
				parameters["@EntityID"].SourceColumn = "EntityID";
				parameters["@SysDocID"].SourceColumn = "SysDocID";
				parameters["@Amount"].SourceColumn = "Amount";
				parameters["@Payee"].SourceColumn = "Payee";
				parameters["@Description"].SourceColumn = "Description";
				parameters["@TransactionType"].SourceColumn = "TransactionType";
				parameters["@DataComboType"].SourceColumn = "DataComboType";
				parameters["@ReferenceID"].SourceColumn = "ReferenceID";
			}
			return insertCommand;
		}

		private string GetInsertCOGSUpdateLogText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("COGS_Update_LOG", new FieldValue("BatchReference", "@BatchReference"), new FieldValue("BatchDate", "@BatchDate"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("OldCost", "@OldCost"), new FieldValue("NewCost", "@NewCost"), new FieldValue("OldCOGS", "@OldCOGS"), new FieldValue("NewCOGS", "@NewCOGS"), new FieldValue("TotalDiff", "@TotalDiff"), new FieldValue("COGSAccountID", "@COGSAccountID"), new FieldValue("AssetAccountID", "@AssetAccountID"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertCOGSUpdateLogCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertCOGSUpdateLogText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@BatchReference", SqlDbType.NVarChar);
				parameters.Add("@BatchDate", SqlDbType.DateTime);
				parameters.Add("@SysDocID", SqlDbType.NVarChar);
				parameters.Add("@VoucherID", SqlDbType.NVarChar);
				parameters.Add("@ProductID", SqlDbType.NVarChar);
				parameters.Add("@RowIndex", SqlDbType.Int);
				parameters.Add("@OldCost", SqlDbType.Money);
				parameters.Add("@NewCost", SqlDbType.Money);
				parameters.Add("@OldCOGS", SqlDbType.Money);
				parameters.Add("@NewCOGS", SqlDbType.Money);
				parameters.Add("@TotalDiff", SqlDbType.Money);
				parameters.Add("@COGSAccountID", SqlDbType.NVarChar);
				parameters.Add("@AssetAccountID", SqlDbType.NVarChar);
				parameters["@BatchReference"].SourceColumn = "BatchReference";
				parameters["@BatchDate"].SourceColumn = "BatchDate";
				parameters["@SysDocID"].SourceColumn = "SysDocID";
				parameters["@VoucherID"].SourceColumn = "VoucherID";
				parameters["@ProductID"].SourceColumn = "ProductID";
				parameters["@RowIndex"].SourceColumn = "RowIndex";
				parameters["@OldCost"].SourceColumn = "OldCost";
				parameters["@NewCost"].SourceColumn = "NewCost";
				parameters["@OldCOGS"].SourceColumn = "OldCOGS";
				parameters["@NewCOGS"].SourceColumn = "NewCOGS";
				parameters["@TotalDiff"].SourceColumn = "TotalDiff";
				parameters["@COGSAccountID"].SourceColumn = "COGSAccountID";
				parameters["@AssetAccountID"].SourceColumn = "AssetAccountID";
			}
			return insertCommand;
		}

		public bool InsertCOGSLog(ActivityLogData activityLogData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertCOGSUpdateLogCommand = GetInsertCOGSUpdateLogCommand();
			try
			{
				return Insert(activityLogData, "COGS_Update_LOG", insertCOGSUpdateLogCommand, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertDocumentVersion(ScreenTypes screenType, string screenID, string sysDocID, string docNumber, DataSet docData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertDocVersionCommand = GetInsertDocVersionCommand();
			try
			{
				ActivityLogData activityLogData = new ActivityLogData(isCOGSLog: false, isDocVersion: true);
				DataTable docVersionTable = activityLogData.DocVersionTable;
				DataRow dataRow = docVersionTable.NewRow();
				dataRow["ScreenType"] = (byte)screenType;
				dataRow["UserID"] = base.DBConfig.UserID;
				dataRow["ScreenID"] = screenID;
				if (sysDocID == "")
				{
					dataRow["SysDocID"] = DBNull.Value;
				}
				else
				{
					dataRow["SysDocID"] = sysDocID;
				}
				dataRow["DocNumber"] = docNumber;
				dataRow["MachineID"] = base.DBConfig.ClientMachineName;
				dataRow["LogDate"] = DateTime.Now;
				byte[] array2 = (byte[])(dataRow["DocData"] = CommonLib.CompressDataSet(docData));
				docVersionTable.Rows.Add(dataRow);
				return Insert(activityLogData, "Doc_Version", insertDocVersionCommand, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private bool InsertActivityLog(ActivityLogData activityLogData)
		{
			return InsertActivityLog(activityLogData, null);
		}

		private bool InsertActivityLog(ActivityLogData activityLogData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertCommand = GetInsertCommand();
			try
			{
				return Insert(activityLogData, "[Activity Logs]", insertCommand, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		internal bool InsertActivityLog(string entityName, string entityID, string sysDocID, ActivityTypes activityType, string payeeName, object transAmount, SysDocTypes glType, DataComboType combotype, object reference, SqlTransaction sqlTransaction)
		{
			if (base.DBConfig.UserID.ToLower() == "axolonfixer")
			{
				return true;
			}
			if (!StoreConfiguration.IsActivityLogEnabled)
			{
				return false;
			}
			string text = "";
			switch (activityType)
			{
			case ActivityTypes.Add:
				text = "Added";
				break;
			case ActivityTypes.Delete:
				text = "Deleted";
				break;
			case ActivityTypes.Update:
				text = "Edited";
				break;
			case ActivityTypes.Activate:
				text = "Activated";
				break;
			case ActivityTypes.Void:
				text = "Voided";
				break;
			case ActivityTypes.Unvoid:
				text = "UnVoided";
				break;
			case ActivityTypes.Inactivate:
				text = "Inactivated";
				break;
			case ActivityTypes.LogIn:
				text = "Logged In";
				break;
			case ActivityTypes.LogOut:
				text = "Logged Out";
				break;
			case ActivityTypes.Other:
				text = "Other Activity";
				break;
			}
			if (entityID == null)
			{
				entityID = "";
			}
			text = (((entityName == null || entityName == string.Empty) && (entityID != null || entityID != string.Empty)) ? (text + " " + entityID.ToString()) : ((entityName == null || (entityID != null && !(entityID == string.Empty))) ? (text + " " + entityName.ToString() + " " + entityID.ToString()) : (text + " " + entityName.ToString())));
			ActivityLogData activityLogData = new ActivityLogData();
			DataRow dataRow = activityLogData.ActivityLogTable.NewRow();
			dataRow["ActivityType"] = activityType;
			if (payeeName != null && payeeName.Trim() != string.Empty)
			{
				dataRow["Payee"] = payeeName;
			}
			if (transAmount != null)
			{
				dataRow["Amount"] = transAmount;
			}
			dataRow["Description"] = text;
			if (reference != null)
			{
				dataRow["TransactionType"] = glType;
				dataRow["ReferenceID"] = reference;
			}
			if (entityID != null && entityID != "")
			{
				dataRow["EntityID"] = entityID;
			}
			if (sysDocID != null && sysDocID != "")
			{
				dataRow["SysDocID"] = sysDocID;
			}
			if (combotype != 0)
			{
				dataRow["DataComboType"] = combotype;
			}
			dataRow["UserID"] = base.DBConfig.AccessUser;
			dataRow["UserID"] = base.DBConfig.AccessUser;
			dataRow["MachineID"] = base.DBConfig.ClientMachineName;
			dataRow["LogDate"] = DateTime.Now;
			activityLogData.ActivityLogTable.Rows.Add(dataRow);
			if (sqlTransaction == null)
			{
				sqlTransaction = base.DBConfig.SqlTransaction;
			}
			return InsertActivityLog(activityLogData, sqlTransaction);
		}

		public DataSet GetActivityLogsByFields(params string[] columns)
		{
			if (!base.DBConfig.IsCurrentUserAdministrator())
			{
				throw new ApplicationException("Only administrator can view activity logs.");
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Activity Logs]");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "[Activity Logs]", sqlBuilder);
			return dataSet;
		}

		public DataSet GetActivityLogsByFields(ActivityTypes[] activityTypes, string[] machines, string[] users, DateTime from, DateTime to, params string[] columns)
		{
			if (!base.DBConfig.IsCurrentUserAdministrator())
			{
				throw new ApplicationException("Only administrator can view activity logs.");
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Activity Logs]");
			CommandHelper commandHelper = null;
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (activityTypes.Length != 0)
			{
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "ActivityType";
				commandHelper.FieldValue = activityTypes;
				commandHelper.SqlFieldType = SqlDbType.Int;
				commandHelper.TableName = "[Activity Logs]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (machines.Length != 0)
			{
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "MachineID";
				commandHelper.FieldValue = machines;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "[Activity Logs]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (users.Length != 0)
			{
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "UserID";
				commandHelper.FieldValue = users;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "[Activity Logs]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (from.Year != DateTime.MinValue.Year)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "[Activity Logs]";
				commandHelper.FieldName = "LogDate";
				commandHelper.FieldValue = from.ToString(StoreConfiguration.CurrentCulture);
				commandHelper.FieldValue2 = to.ToString(StoreConfiguration.CurrentCulture);
				commandHelper.SqlFieldType = SqlDbType.DateTime;
				commandHelper.SqlOp.LogicalValueOp = LogicalValueOperator.BETWEEN;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "[Activity Logs]", sqlBuilder);
			return dataSet;
		}

		public DataSet GetActivityLogsByFields(int[] activityLogID, params string[] columns)
		{
			if (!base.DBConfig.IsCurrentUserAdministrator())
			{
				throw new ApplicationException("Only administrator can view activity logs.");
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Activity Logs]");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (activityLogID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "ActivityLogID";
				commandHelper.FieldValue = activityLogID;
				commandHelper.TableName = "[Activity Logs]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "[Activity Logs]", sqlBuilder);
			return dataSet;
		}

		public ActivityLogData GetActivityLogs()
		{
			if (!base.DBConfig.IsCurrentUserAdministrator())
			{
				throw new ApplicationException("Only administrator can view activity logs.");
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Activity Logs]");
			sqlBuilder.UseDistinct = false;
			ActivityLogData activityLogData = new ActivityLogData();
			FillDataSet(activityLogData, "[Activity Logs]", sqlBuilder);
			return activityLogData;
		}

		public bool DeleteActivityLog(int[] activityLogID)
		{
			if (!base.DBConfig.IsCurrentUserAdministrator())
			{
				throw new ApplicationException("Only administrator can delete activity logs.");
			}
			bool flag = true;
			try
			{
				return DeleteTableRowByID("[Activity Logs]", "ActivityLogID", activityLogID);
			}
			catch
			{
				throw;
			}
		}

		public ActivityLogData GetActivityLogByID(int activityLogID)
		{
			if (!base.DBConfig.IsCurrentUserAdministrator())
			{
				throw new ApplicationException("Only administrator can view activity logs.");
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ActivityLogID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = activityLogID;
			commandHelper.TableName = "[Activity Logs]";
			sqlBuilder.AddCommandHelper(commandHelper);
			ActivityLogData activityLogData = new ActivityLogData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(activityLogData, "[Activity Logs]", sqlBuilder);
				return activityLogData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetActivityLogList(DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ActivityType [Type],LogDate [Date],UserID,MachineID [Computer],Description \r\n                                FROM [Activity Logs] WHERE 1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND LogDate Between '" + text + "' AND '" + text2 + "'";
			}
			FillDataSet(dataSet, "[Activity Logs]", text3);
			return dataSet;
		}

		public DataSet GetDocumentActivityLog(string entityID, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = "select ActivityType,LogDate AS Date,UserID ,MachineID AS Machine,Description FROM [Activity Logs]\r\n                        WHERE EntityID = '" + entityID + "' ";
			if (sysDocID != "")
			{
				text = text + " AND SysDocID = '" + sysDocID + "'";
			}
			FillDataSet(dataSet, "[Activity Logs]", text);
			return dataSet;
		}

		public DataSet GetDocumentActivityLog(string entityID, string sysDocID, int comboType)
		{
			DataSet dataSet = new DataSet();
			string text = "select ActivityType,LogDate AS Date,UserID ,MachineID AS Machine,Description FROM [Activity Logs]\r\n                        WHERE EntityID = '" + entityID + "' ";
			if (sysDocID != "")
			{
				text = text + " AND SysDocID = '" + sysDocID + "'";
			}
			if (comboType != 0)
			{
				text = text + " AND DataComboType = '" + comboType + "'";
			}
			FillDataSet(dataSet, "[Activity Logs]", text);
			return dataSet;
		}

		public DataSet GetDocumentVersionsList(int screenType, int screenID, string sysDocID, string docNumber)
		{
			DataSet dataSet = new DataSet();
			string text = "select VersionID,MachineID,UserID,LogDate FROM Doc_Version\r\n                        WHERE 1=1 ";
			if (screenType != -1)
			{
				text = text + " AND ScreenType = " + screenType;
			}
			if (screenID != -1)
			{
				text = text + " AND ScreenID = " + screenID;
			}
			if (sysDocID != "")
			{
				text = text + " AND SysDocID = '" + sysDocID + "'";
			}
			if (docNumber != "")
			{
				text = text + " AND DocNumber = '" + docNumber + "'";
			}
			FillDataSet(dataSet, "Doc_Version", text);
			return dataSet;
		}

		public DataSet GetDocumentVersionsByID(int versionID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select * FROM Doc_Version\r\n                        WHERE VersionID =  " + versionID;
			FillDataSet(dataSet, "Doc_Version", textCommand);
			return dataSet;
		}
	}
}
