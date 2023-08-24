using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomReports : StoreObject
	{
		private const string CUSTOMREPORTID_PARM = "@CustomReportID";

		private const string CUSTOMREPORTNAME_PARM = "@CustomReportName";

		private const string PARENTMENUNAME_PARM = "@ParentMenuName";

		private const string TEMPLATENAME_PARM = "@TemplateName";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string REPORTDATA_PARM = "@ReportData";

		private const string QUERY_PARM = "@Query";

		public const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string NOTE_PARM = "@Note";

		private const string DISPLAYNOTE_PARM = "@DisplayNote";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public CustomReports(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Custom_Report", new FieldValue("CustomReportID", "@CustomReportID", isUpdateConditionField: true), new FieldValue("CustomReportName", "@CustomReportName"), new FieldValue("ParentMenuName", "@ParentMenuName"), new FieldValue("TemplateName", "@TemplateName"), new FieldValue("ReportData", "@ReportData"), new FieldValue("Query", "@Query"), new FieldValue("Note", "@Note"), new FieldValue("DisplayNote", "@DisplayNote"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Custom_Report", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CustomReportID", SqlDbType.NVarChar);
			parameters.Add("@CustomReportName", SqlDbType.NVarChar);
			parameters.Add("@ParentMenuName", SqlDbType.NVarChar);
			parameters.Add("@TemplateName", SqlDbType.NVarChar);
			parameters.Add("@ReportData", SqlDbType.Image);
			parameters.Add("@Query", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@DisplayNote", SqlDbType.NVarChar);
			parameters["@CustomReportID"].SourceColumn = "CustomReportID";
			parameters["@CustomReportName"].SourceColumn = "CustomReportName";
			parameters["@ParentMenuName"].SourceColumn = "ParentMenuName";
			parameters["@TemplateName"].SourceColumn = "TemplateName";
			parameters["@ReportData"].SourceColumn = "ReportData";
			parameters["@Query"].SourceColumn = "Query";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@DisplayNote"].SourceColumn = "DisplayNote";
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

		public bool InsertUpdateCustomReport(CustomReportData customReportData, bool isUpdate)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = ((!isUpdate) ? Insert(customReportData, "Custom_Report", insertUpdateCommand) : Update(customReportData, "Custom_Report", insertUpdateCommand));
				string text = customReportData.CustomReportTable.Rows[0]["CustomReportID"].ToString();
				if (!isUpdate)
				{
					AddActivityLog("CustomReport", text, ActivityTypes.Add, sqlTransaction);
					UpdateTableRowInsertUpdateInfo("Custom_Report", "CustomReportID", text, sqlTransaction, isInsert: true);
					return result;
				}
				AddActivityLog("CustomReport", text, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Custom_Report", "CustomReportID", text, sqlTransaction, isInsert: false);
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

		public DataSet GetCustomReportsByFields(params string[] columns)
		{
			return GetCustomReportsByFields(null, isInactive: true, columns);
		}

		public DataSet GetCustomReportsByFields(string[] customReportsID, params string[] columns)
		{
			return GetCustomReportsByFields(customReportsID, isInactive: true, columns);
		}

		public DataSet GetCustomReportsByFields(string[] customReportsID, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Custom_Report");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (customReportsID != null && customReportsID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "CustomReportID";
				commandHelper.FieldValue = customReportsID;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Custom_Report";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.Bit;
				commandHelper2.TableName = "Custom_Report";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Custom_Report", sqlBuilder);
			return dataSet;
		}

		public CustomReportData GetCustomReports()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "Custom_Report";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = false;
			sqlBuilder.AddOrderByColumn("Custom_Report", "CustomReportName");
			CustomReportData customReportData = new CustomReportData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customReportData, "Custom_Report", sqlBuilder);
			return customReportData;
		}

		public CustomReportData GetCustomReportByID(string customReportID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CustomReportID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = customReportID;
			commandHelper.TableName = "Custom_Report";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CustomReportData customReportData = new CustomReportData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customReportData, "Custom_Report", sqlBuilder);
			return customReportData;
		}

		public bool DeleteCustomReport(string customReportID)
		{
			bool flag = true;
			try
			{
				flag = DeleteTableRowByID("Custom_Report", "CustomReportID", customReportID);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CustomReport", customReportID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetCustomReportIDByName(string name)
		{
			try
			{
				object obj = ExecuteSelectScalar("Custom_Report", "CustomReportName", name, "CustomReportID");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public string GetCustomReportNameByID(object customReportID)
		{
			try
			{
				object obj = ExecuteSelectScalar("Custom_Report", "CustomReportID", customReportID, "CustomReportName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public bool ExistCustomReport(string customReportName)
		{
			try
			{
				return IsTableFieldValueExist("Custom_Report", "CustomReportName", customReportName);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetCustomReportComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CustomReportID [Code],CustomReportName [Name],ParentMenuName\r\n                           FROM Custom_Report ORDER BY CustomReportID,CustomReportName";
			FillDataSet(dataSet, "Buyer", textCommand);
			return dataSet;
		}

		public DataSet GetCustomData(string reportID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = string.Empty;
			string exp = "SELECT Query\r\n                           FROM Custom_Report WHERE CustomReportID = '" + reportID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				textCommand = obj.ToString();
			}
			FillDataSet(dataSet, "tableData", textCommand);
			return dataSet;
		}

		public DataSet GetCustomReportList()
		{
			string textCommand = "SELECT CustomReportID [CustomReport Code],CustomReportName [CustomReport Name],ContactName AS [Contact Name],Phone,Fax,IsInactive AS [Inactive]\r\n                           FROM Custom_Report ORDER BY CustomReportID,CustomReportName";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Account_Group", textCommand);
			return dataSet;
		}

		public DataSet GetCustomReportData(string reportID, string[] parameters, string[] parameterValues)
		{
			try
			{
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				CustomReport customReport = (CustomReport)CommonLib.DeserializeFromStream((byte[])GetCustomReportByID(reportID).Tables[0].Rows[0]["ReportData"]);
				DataSet dataSet = new DataSet();
				foreach (CustomReportTable table in customReport.Tables)
				{
					string text = table.query;
					for (int i = 0; i < parameters.Length; i++)
					{
						text = text.Replace(parameters[i], parameterValues[i]);
					}
					text = dataHelper.ReplaceSystemParameters(text);
					if (!SQLHelper.ValidateQuerySecurity(text))
					{
						throw new CompanyException("report query does not allow one or more keywords used in this query.");
					}
					FillDataSet(dataSet, table.tableName, text);
				}
				foreach (ReportRelation relation in customReport.Relations)
				{
					DataColumn[] array = new DataColumn[relation.ParentColumns.Length];
					DataColumn[] array2 = new DataColumn[relation.ChildColumns.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = dataSet.Tables[relation.ParentTableName].Columns[relation.ParentColumns[j]];
						array2[j] = dataSet.Tables[relation.ChildTableName].Columns[relation.ChildColumns[j]];
					}
					dataSet.Relations.Add(relation.RelationName, array, array2, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool SaveLayout(string reportID, byte[] layout, int formWidth, int formHeight)
		{
			bool flag = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				if (layout == null)
				{
					string exp = "Update Custom_Report SET Layout = NULL,FormWidth=" + formWidth + ",FormHeight =" + formHeight + " WHERE CustomReportID = '" + reportID + "'";
					flag &= (ExecuteNonQuery(exp) > 0);
				}
				else
				{
					string exp = "Update Custom_Report SET Layout=@Layout, FormWidth=" + formWidth + ",FormHeight =" + formHeight + " WHERE CustomReportID = '" + reportID + "'";
					SqlCommand sqlCommand = new SqlCommand(exp);
					sqlCommand.Parameters.AddWithValue("@Layout", layout);
					sqlCommand.Transaction = transaction;
					flag &= (ExecuteNonQuery(sqlCommand) > 0);
				}
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

		public DataSet GetTableSchema(string query)
		{
			try
			{
				string text = query.ToLower();
				if (text.Contains("delete ") || text.Contains("alter ") || text.Contains("update ") || text.Contains("insert "))
				{
					throw new CompanyException("Delete, Alter,Insert, Update are not allowed in the query.");
				}
				DataSet dataSet = new DataSet();
				query = query.Replace("@FromDate", "1-1-2000");
				query = query.Replace("@EndDate", "1-1-2000");
				int num = 0;
				int num2 = 0;
				ArrayList arrayList = new ArrayList();
				while (num >= 0)
				{
					num = query.IndexOf("@", num2, StringComparison.CurrentCultureIgnoreCase);
					if (num == -1)
					{
						break;
					}
					num2 = query.IndexOf(" ", num);
					if (num2 == -1)
					{
						num2 = query.Length;
					}
					arrayList.Add(query.Substring(num, num2 - num));
				}
				foreach (string item in arrayList)
				{
					query = query.Replace(item, " 1=1 ");
				}
				query = query.Replace("SELECT", "SELECT TOP 1 ");
				FillDataSet(dataSet, "Table", query);
				dataSet.Tables[0].Rows.Clear();
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
