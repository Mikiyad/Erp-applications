using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class WebDashboard : StoreObject
	{
		private const string WEBDASHBOARDID_PARM = "@WebDashboardID";

		private const string NAME_PARM = "@Name";

		private const string WEBDASHBOARD_TABLE = "WebDashboard";

		private const string USERID_PARM = "@UserID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ZONEINDEX_PARM = "@ZoneIndex";

		private const string ZONELAYOUT_PARM = "@ZoneLayout";

		private const string LAYOUT_PARM = "@Layout";

		private const string SELECTEDGADGETS_PARM = "@SelectedGadgets";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public WebDashboard(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("WebDashboard", new FieldValue("WebDashboardID", "@WebDashboardID", isUpdateConditionField: true), new FieldValue("UserID", "@UserID", isUpdateConditionField: true), new FieldValue("Name", "@Name"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("SelectedGadgets", "@SelectedGadgets"), new FieldValue("ZoneIndex", "@ZoneIndex"), new FieldValue("Layout", "@Layout"), new FieldValue("ZoneLayout", "@ZoneLayout"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("WebDashboard", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@WebDashboardID", SqlDbType.NVarChar);
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ZoneIndex", SqlDbType.Int);
			parameters.Add("@ZoneLayout", SqlDbType.NText);
			parameters.Add("@Layout", SqlDbType.NText);
			parameters.Add("@SelectedGadgets", SqlDbType.NVarChar);
			parameters["@WebDashboardID"].SourceColumn = "WebDashboardID";
			parameters["@Name"].SourceColumn = "Name";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ZoneIndex"].SourceColumn = "ZoneIndex";
			parameters["@ZoneLayout"].SourceColumn = "ZoneLayout";
			parameters["@Layout"].SourceColumn = "Layout";
			parameters["@SelectedGadgets"].SourceColumn = "SelectedGadgets";
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

		public bool InsertWebDashboard(WebDashboardData accountWebDashboardData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountWebDashboardData, "WebDashboard", insertUpdateCommand);
				string text = accountWebDashboardData.WebDashboardTable.Rows[0]["WebDashboardID"].ToString();
				AddActivityLog("WebDashboard", text, "", ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("WebDashboard", "WebDashboardID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateWebDashboard(WebDashboardData accountWebDashboardData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountWebDashboardData, "WebDashboard", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountWebDashboardData.WebDashboardTable.Rows[0]["WebDashboardID"];
				UpdateTableRowByID("WebDashboard", "WebDashboardID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountWebDashboardData.WebDashboardTable.Rows[0]["Name"].ToString();
				AddActivityLog("WebDashboard", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("WebDashboard", "WebDashboardID", obj, sqlTransaction, isInsert: false);
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

		public WebDashboardData GetWebDashboard()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("WebDashboard");
			WebDashboardData webDashboardData = new WebDashboardData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(webDashboardData, "WebDashboard", sqlBuilder);
			return webDashboardData;
		}

		public bool DeleteWebDashboard(string webdashboardID, string userID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM WebDashboard WHERE WebDashboardID = '" + webdashboardID + "' AND userID = '" + userID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Settings WHERE ID = '" + userID + "' AND SKey = 'CWEBDASH" + webdashboardID + "'";
				flag &= Delete(commandText, trans);
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

		public WebDashboardData GetWebDashboardByID(string id, string userID)
		{
			WebDashboardData webDashboardData = new WebDashboardData();
			string textCommand = "SELECT * FROM WebDashboard WHERE UserID = '" + userID + "' AND WebDashboardID = '" + id + "'";
			FillDataSet(webDashboardData, "WebDashboard", textCommand);
			return webDashboardData;
		}

		public bool SaveLayout(string webdashboardID, string layout)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				string userID = base.DBConfig.UserID;
				string exp = "SELECT COUNT(*) FROM WebDashboard WHERE WebDashboardID = '" + webdashboardID + "' AND UserID = '" + userID + "'";
				if (int.Parse(ExecuteScalar(exp).ToString()) == 0)
				{
					WebDashboardData webDashboardData = new WebDashboardData();
					DataTable dataTable = webDashboardData.Tables[0];
					DataRow dataRow = dataTable.NewRow();
					dataRow["WebDashboardID"] = webdashboardID;
					dataRow["UserID"] = userID;
					dataRow["Name"] = "WebDashHome";
					dataRow["RowIndex"] = 0;
					dataTable.Rows.Add(dataRow);
					result = InsertWebDashboard(webDashboardData);
				}
				SqlCommand sqlCommand = new SqlCommand("Update WebDashboard SET Layout=@Layout WHERE WebDashboardID='" + webdashboardID + "' AND UserID = '" + userID + "'");
				sqlCommand.Parameters.AddWithValue("@Layout", layout);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public bool SaveZoneLayout(string webdashboardID, string zoneLayout)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				string userID = base.DBConfig.UserID;
				string exp = "SELECT COUNT(*) FROM WebDashboard WHERE WebDashboardID = '" + webdashboardID + "' AND UserID = '" + userID + "'";
				if (int.Parse(ExecuteScalar(exp).ToString()) == 0)
				{
					WebDashboardData webDashboardData = new WebDashboardData();
					DataTable dataTable = webDashboardData.Tables[0];
					DataRow dataRow = dataTable.NewRow();
					dataRow["WebDashboardID"] = webdashboardID;
					dataRow["UserID"] = userID;
					dataRow["Name"] = "WebDashHome";
					dataRow["RowIndex"] = 0;
					dataTable.Rows.Add(dataRow);
					result = InsertWebDashboard(webDashboardData);
				}
				SqlCommand sqlCommand = new SqlCommand("Update WebDashboard SET ZoneLayout=@ZoneLayout WHERE WebDashboardID='" + webdashboardID + "' AND UserID = '" + userID + "'");
				sqlCommand.Parameters.AddWithValue("@ZoneLayout", zoneLayout);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public DataSet GetWebDashboardByFields(params string[] columns)
		{
			return GetWebDashboardByFields(null, isInactive: true, columns);
		}

		public DataSet GetWebDashboardByFields(string[] webdashboardID, params string[] columns)
		{
			return GetWebDashboardByFields(webdashboardID, isInactive: true, columns);
		}

		public DataSet GetWebDashboardByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("WebDashboard");
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
				commandHelper.FieldName = "WebDashboardID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "WebDashboard";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "WebDashboard", sqlBuilder);
			return dataSet;
		}

		public DataSet GetWebDashboardsByUser(string userID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT WebDashboardID, Name,RowIndex,ZoneIndex,Layout,ZoneLayout FROM WebDashboard WHERE UserID = '" + userID + "' ORDER BY RowIndex";
			FillDataSet(dataSet, "WebDashboard", textCommand);
			return dataSet;
		}

		public DataSet GetWebDashboardList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT WebDashboardID [Dashboard Code],Name [Dashboard Name]\r\n                           FROM WebDashboard ";
			FillDataSet(dataSet, "WebDashboard", textCommand);
			return dataSet;
		}

		public DataSet GetWebDashboardComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT WebDashboardID [Code],Name [Name]\r\n                           FROM WebDashboard ORDER BY WebDashboardID,Name";
			FillDataSet(dataSet, "WebDashboard", textCommand);
			return dataSet;
		}
	}
}
