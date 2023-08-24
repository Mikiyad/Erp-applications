using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Dashboard : StoreObject
	{
		private const string DASHBOARDID_PARM = "@DashboardID";

		private const string NAME_PARM = "@Name";

		private const string DASHBOARD_TABLE = "Dashboard";

		private const string USERID_PARM = "@UserID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Dashboard(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Dashboard", new FieldValue("DashboardID", "@DashboardID", isUpdateConditionField: true), new FieldValue("UserID", "@UserID", isUpdateConditionField: true), new FieldValue("Name", "@Name"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Dashboard", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DashboardID", SqlDbType.NVarChar);
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@DashboardID"].SourceColumn = "DashboardID";
			parameters["@Name"].SourceColumn = "Name";
			parameters["@UserID"].SourceColumn = "UserID";
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

		public bool InsertDashboard(DashboardData accountDashboardData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDashboardData, "Dashboard", insertUpdateCommand);
				string text = accountDashboardData.DashboardTable.Rows[0]["DashboardID"].ToString();
				AddActivityLog("Dashboard", text, "", ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Dashboard", "DashboardID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateDashboard(DashboardData accountDashboardData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDashboardData, "Dashboard", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDashboardData.DashboardTable.Rows[0]["DashboardID"];
				UpdateTableRowByID("Dashboard", "DashboardID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDashboardData.DashboardTable.Rows[0]["Name"].ToString();
				AddActivityLog("Dashboard", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Dashboard", "DashboardID", obj, sqlTransaction, isInsert: false);
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

		public DashboardData GetDashboard()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Dashboard");
			DashboardData dashboardData = new DashboardData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(dashboardData, "Dashboard", sqlBuilder);
			return dashboardData;
		}

		public bool DeleteDashboard(string dashboardID, string userID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Dashboard WHERE DashboardID = '" + dashboardID + "' AND userID = '" + userID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Settings WHERE ID = '" + userID + "' AND SKey = 'CDASH" + dashboardID + "'";
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

		public bool UpdateDashboardWithTemplate(string dashboardID, string userID)
		{
			bool result = true;
			try
			{
				base.DBConfig.StartNewTransaction();
				string exp = "SELECT COUNT(*) FROM Dashboard WHERE DashboardID = 'dashLayoutHome' AND UserID = '" + userID + "'";
				if (int.Parse(ExecuteScalar(exp).ToString()) == 0)
				{
					DashboardData dashboardData = new DashboardData();
					DataTable dataTable = dashboardData.Tables[0];
					DataRow dataRow = dataTable.NewRow();
					dataRow["DashboardID"] = "dashLayoutHome";
					dataRow["UserID"] = userID;
					dataRow["Name"] = "Home";
					dataRow["RowIndex"] = 0;
					dataTable.Rows.Add(dataRow);
					result = InsertDashboard(dashboardData);
				}
				string exp2 = "update Dashboard set Layout=(select Layout from Dashboard where DashboardID='" + dashboardID + "' AND  UserID= 'SYS_TMPLT') where UserID='" + userID + "' AND DashboardID='dashLayoutHome'";
				result = (ExecuteNonQuery(exp2) > 0);
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

		public bool UpdateDashboardWithTemplate(string currentdashboardID, string newDashboardID, string userID)
		{
			bool result = true;
			try
			{
				base.DBConfig.StartNewTransaction();
				string exp = "SELECT COUNT(*) FROM Dashboard WHERE DashboardID = '" + currentdashboardID + "' AND UserID = '" + userID + "'";
				if (int.Parse(ExecuteScalar(exp).ToString()) == 0)
				{
					return false;
				}
				string exp2 = "update Dashboard set Layout=(select Layout from Dashboard where DashboardID='" + newDashboardID + "' AND  UserID= 'SYS_TMPLT') where UserID='" + userID + "' AND DashboardID='" + currentdashboardID + "'";
				result = (ExecuteNonQuery(exp2) > 0);
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

		public DashboardData GetDashboardByID(string id, string userID)
		{
			DashboardData dashboardData = new DashboardData();
			string textCommand = "SELECT * FROM Dashboard WHERE UserID = '" + userID + "' AND DashboardID = '" + id + "'";
			FillDataSet(dashboardData, "Dashboard", textCommand);
			return dashboardData;
		}

		public DashboardData GetAvailableDashboardTemplates()
		{
			DashboardData dashboardData = new DashboardData();
			string textCommand = "SELECT Name FROM Dashboard WHERE UserID = 'SYS_TMPLT'";
			FillDataSet(dashboardData, "NameList", textCommand);
			return dashboardData;
		}

		public bool SaveLayout(string dashboardID, byte[] layout)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				string userID = base.DBConfig.UserID;
				string exp = "SELECT COUNT(*) FROM Dashboard WHERE DashboardID = '" + dashboardID + "' AND UserID = '" + userID + "'";
				if (int.Parse(ExecuteScalar(exp).ToString()) == 0)
				{
					DashboardData dashboardData = new DashboardData();
					DataTable dataTable = dashboardData.Tables[0];
					DataRow dataRow = dataTable.NewRow();
					dataRow["DashboardID"] = dashboardID;
					dataRow["UserID"] = userID;
					dataRow["Name"] = "Home";
					dataRow["RowIndex"] = 0;
					dataTable.Rows.Add(dataRow);
					result = InsertDashboard(dashboardData);
				}
				SqlCommand sqlCommand = new SqlCommand("Update Dashboard SET Layout=@Layout WHERE DashboardID='" + dashboardID + "' AND UserID = '" + userID + "'");
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

		public bool SaveLayoutTemplate(string dashboardID, byte[] layout, string templateName)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				string text = "SYS_TMPLT";
				string exp = "SELECT COUNT(*) FROM Dashboard WHERE DashboardID = '" + dashboardID + "' AND Name = '" + templateName + "'";
				if (int.Parse(ExecuteScalar(exp).ToString()) == 0)
				{
					DashboardData dashboardData = new DashboardData();
					DataTable dataTable = dashboardData.Tables[0];
					DataRow dataRow = dataTable.NewRow();
					dataRow["DashboardID"] = dashboardID;
					dataRow["UserID"] = text;
					dataRow["Name"] = templateName;
					dataRow["RowIndex"] = 0;
					dataTable.Rows.Add(dataRow);
					result = InsertDashboard(dashboardData);
				}
				SqlCommand sqlCommand = new SqlCommand("Update Dashboard SET Layout=@Layout WHERE DashboardID='" + dashboardID + "' AND UserID = '" + text + "'");
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

		public DataSet GetDashboardByFields(params string[] columns)
		{
			return GetDashboardByFields(null, isInactive: true, columns);
		}

		public DataSet GetDashboardByFields(string[] dashboardID, params string[] columns)
		{
			return GetDashboardByFields(dashboardID, isInactive: true, columns);
		}

		public DataSet GetDashboardByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Dashboard");
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
				commandHelper.FieldName = "DashboardID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Dashboard";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Dashboard", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDashboardsByUser(string userID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DashboardID, Name,RowIndex FROM Dashboard WHERE UserID = '" + userID + "' ORDER BY RowIndex";
			FillDataSet(dataSet, "Dashboard", textCommand);
			return dataSet;
		}

		public DataSet GetDashboardList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DashboardID [Dashboard Code],Name [Dashboard Name]\r\n                           FROM Dashboard ";
			FillDataSet(dataSet, "Dashboard", textCommand);
			return dataSet;
		}

		public DataSet GetDashboardComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DashboardID [Code],Name [Name]\r\n                           FROM Dashboard ORDER BY DashboardID,Name";
			FillDataSet(dataSet, "Dashboard", textCommand);
			return dataSet;
		}
	}
}
