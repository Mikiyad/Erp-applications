using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ExternalReport : StoreObject
	{
		private const string SMARTLISTID_PARM = "@ExternalReportID";

		private const string SMARTLISTNAME_PARM = "@ExternalReportName";

		public const string SMARTLIST_TABLE = "ExternalReport";

		public const string DESCRIPTION_PARM = "@Description";

		public const string QUERY_PARM = "@Query";

		public const string CATEGORYID_PARM = "@CategoryID";

		public const string PARENTID_PARM = "@ParentID";

		private const string REPORTDATA_PARM = "@ReportData";

		private const string DRILLACTION_PARM = "@DrillAction";

		private const string DRILLCARDTYPEID_PARM = "@DrillCardTypeID";

		private const string DRILLCARDIDFIELD_PARM = "@DrillCardIDField";

		private const string DRILLTRANSACTIONSYSDOCIDFIELD_PARM = "@DrillTransactionSysDocIDField";

		private const string DRILLTRANSACTIONVOUCHERIDFIELD_PARM = "@DrillTransactionVoucherIDField";

		private const string DRILLPARM1_PARM = "@DrillParm1";

		private const string DRILLPARM2_PARM = "@DrillParm2";

		private const string DRILLPARM3_PARM = "@DrillParm3";

		private const string DRILLPARM4_PARM = "@DrillParm4";

		private const string DRILLSUBREPORTID_PARM = "@DrillSubReportID";

		private const string ISSUBREPORT_PARM = "@IsSubReport";

		private const string ISPREVIEW_PARM = "@IsPreview";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ExternalReport(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ExternalReport", new FieldValue("Description", "@Description"), new FieldValue("Query", "@Query"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("ParentID", "@ParentID"), new FieldValue("ReportData", "@ReportData"), new FieldValue("DrillParm1", "@DrillParm1"), new FieldValue("DrillParm2", "@DrillParm2"), new FieldValue("DrillParm3", "@DrillParm3"), new FieldValue("DrillParm4", "@DrillParm4"), new FieldValue("DrillSubReportID", "@DrillSubReportID"), new FieldValue("IsSubReport", "@IsSubReport"), new FieldValue("IsPreview", "@IsPreview"), new FieldValue("DrillAction", "@DrillAction"), new FieldValue("DrillCardTypeID", "@DrillCardTypeID"), new FieldValue("DrillCardIDField", "@DrillCardIDField"), new FieldValue("DrillTransactionSysDocIDField", "@DrillTransactionSysDocIDField"), new FieldValue("DrillTransactionVoucherIDField", "@DrillTransactionVoucherIDField"), new FieldValue("ExternalReportName", "@ExternalReportName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ExternalReport", new FieldValue("ExternalReportID", "@ExternalReportID", isUpdateConditionField: true));
			}
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ExternalReport", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			if (isUpdate)
			{
				parameters.Add("@ExternalReportID", SqlDbType.NVarChar);
			}
			parameters.Add("@ExternalReportName", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Query", SqlDbType.NVarChar);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@ParentID", SqlDbType.NVarChar);
			parameters.Add("@ReportData", SqlDbType.Image);
			parameters.Add("@DrillAction", SqlDbType.TinyInt);
			parameters.Add("@DrillCardTypeID", SqlDbType.Int);
			parameters.Add("@DrillCardIDField", SqlDbType.NVarChar);
			parameters.Add("@DrillTransactionSysDocIDField", SqlDbType.NVarChar);
			parameters.Add("@DrillTransactionVoucherIDField", SqlDbType.NVarChar);
			parameters.Add("@IsPreview", SqlDbType.Bit);
			parameters.Add("@DrillParm1", SqlDbType.NVarChar);
			parameters.Add("@DrillParm2", SqlDbType.NVarChar);
			parameters.Add("@DrillParm3", SqlDbType.NVarChar);
			parameters.Add("@DrillParm4", SqlDbType.NVarChar);
			parameters.Add("@IsSubReport", SqlDbType.Bit);
			parameters.Add("@DrillSubReportID", SqlDbType.Int);
			if (isUpdate)
			{
				parameters["@ExternalReportID"].SourceColumn = "ExternalReportID";
			}
			parameters["@ExternalReportName"].SourceColumn = "ExternalReportName";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Query"].SourceColumn = "Query";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@ParentID"].SourceColumn = "ParentID";
			parameters["@ReportData"].SourceColumn = "ReportData";
			parameters["@IsPreview"].SourceColumn = "IsPreview";
			parameters["@DrillParm1"].SourceColumn = "DrillParm1";
			parameters["@DrillParm2"].SourceColumn = "DrillParm2";
			parameters["@DrillParm3"].SourceColumn = "DrillParm3";
			parameters["@DrillParm4"].SourceColumn = "DrillParm4";
			parameters["@IsSubReport"].SourceColumn = "IsSubReport";
			parameters["@DrillSubReportID"].SourceColumn = "DrillSubReportID";
			parameters["@DrillAction"].SourceColumn = "DrillAction";
			parameters["@DrillCardTypeID"].SourceColumn = "DrillCardTypeID";
			parameters["@DrillCardIDField"].SourceColumn = "DrillCardIDField";
			parameters["@DrillTransactionSysDocIDField"].SourceColumn = "DrillTransactionSysDocIDField";
			parameters["@DrillTransactionVoucherIDField"].SourceColumn = "DrillTransactionVoucherIDField";
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

		public DataSet GetExternalReportData(string reportID, DateTime fromDate, DateTime toDate, string[] parameters, string[] parameterValues)
		{
			try
			{
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				DataRow dataRow = GetExternalReportByID(reportID).Tables[0].Rows[0];
				CustomReport customReport = null;
				if (dataRow["ReportData"] != DBNull.Value)
				{
					customReport = (CustomReport)CommonLib.DeserializeFromStream((byte[])dataRow["ReportData"]);
				}
				DataSet dataSet = new DataSet();
				if (customReport != null)
				{
					foreach (CustomReportTable table in customReport.Tables)
					{
						string text = table.query;
						if (parameters != null && parameterValues != null)
						{
							for (int i = 0; i < parameters.Length; i++)
							{
								text = text.Replace(parameters[i], parameterValues[i]);
							}
						}
						text = dataHelper.ReplaceSystemParameters(text, fromDate, toDate);
						if (!SQLHelper.ValidateQuerySecurity(text))
						{
							throw new CompanyException("report query does not allow one or more keywords used in this query.");
						}
						FillDataSet(dataSet, table.tableName, text);
					}
				}
				else
				{
					string query = dataRow["Query"].ToString();
					query = dataHelper.ReplaceSystemParameters(query, fromDate, toDate);
					if (!SQLHelper.ValidateQuerySecurity(query))
					{
						throw new CompanyException("report query does not allow one or more keywords used in this query.");
					}
					FillDataSet(dataSet, "Report", query);
				}
				if (customReport != null)
				{
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
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertExternalReport(ExternalReportData accountExternalReportData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountExternalReportData, "ExternalReport", insertUpdateCommand);
				string text = accountExternalReportData.ExternalReportTable.Rows[0]["ExternalReportID"].ToString();
				AddActivityLog("Smart List", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("ExternalReport", "ExternalReportID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateExternalReport(ExternalReportData accountExternalReportData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountExternalReportData, "ExternalReport", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountExternalReportData.ExternalReportTable.Rows[0]["ExternalReportID"];
				UpdateTableRowByID("ExternalReport", "ExternalReportID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountExternalReportData.ExternalReportTable.Rows[0]["ExternalReportName"].ToString();
				AddActivityLog("Smart List", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("ExternalReport", "ExternalReportID", obj, sqlTransaction, isInsert: false);
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

		public ExternalReportData GetExternalReport(string UserID, bool isadmin)
		{
			string textCommand = "SELECT * FROM ExternalReport ER LEFT JOIN External_Report_User_Link UL ON ER.ExternalReportName=UL.ExternalReportName WHERE ISNULL(IsSubReport,'False') = 'False'";
			ExternalReportData externalReportData = new ExternalReportData();
			FillDataSet(externalReportData, "ExternalReport", textCommand);
			return externalReportData;
		}

		public DataSet GetExternalReportData(string UserID, bool isadmin)
		{
			string str = "SELECT * FROM ExternalReport ER  ";
			if (UserID != "" && !isadmin)
			{
				str += "LEFT JOIN External_Report_User_Link UL ON ER.ExternalReportName=UL.ExternalReportName ";
			}
			str += "WHERE ISNULL(IsSubReport,'False') = 'False'";
			if (UserID != "" && !isadmin)
			{
				str = str + "AND UL.UserID = '" + UserID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "ExternalReport", str);
			return dataSet;
		}

		public bool DeleteExternalReport(string smartListID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM ExternalReport WHERE ExternalReportID = '" + smartListID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Smart List", smartListID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteCategory(string smartListID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM ExternalReport_Category WHERE CategoryID = " + smartListID;
				return Delete(commandText, null);
			}
			catch
			{
				throw;
			}
		}

		public bool CreateCategory(string categoryName, int parentID)
		{
			bool flag = true;
			try
			{
				string text = "NULL";
				if (parentID >= 0)
				{
					text = parentID.ToString();
				}
				SqlCommand command = new SqlCommand("INSERT INTO ExternalReport_Category (CategoryName,ParentID) VALUES('" + categoryName + "'," + text + ")");
				return ExecuteNonQuery(command) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateCategory(string categoryName, int parentID, int categoryID)
		{
			bool flag = true;
			try
			{
				string text = "NULL";
				if (parentID >= 0)
				{
					text = parentID.ToString();
				}
				SqlCommand command = new SqlCommand("UPDATE ExternalReport_Category SET CategoryName = '" + categoryName + "',ParentID=" + text + " where CategoryID=" + categoryID.ToString());
				return ExecuteNonQuery(command) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool RenameReport(string reportName, int reportID)
		{
			bool flag = true;
			try
			{
				SqlCommand command = new SqlCommand("UPDATE ExternalReport SET ExternalReportName = '" + reportName + "' where ExternalReportID=" + reportID.ToString());
				return ExecuteNonQuery(command) > 0;
			}
			catch
			{
				throw;
			}
		}

		public ExternalReportData GetExternalReportByID(string id)
		{
			ExternalReportData externalReportData = new ExternalReportData();
			string textCommand = "SELECT * FROM ExternalReport where ExternalReportName='" + id + "'";
			FillDataSet(externalReportData, "ExternalReport", textCommand);
			return externalReportData;
		}

		public DataSet GetExternalReportByFields(params string[] columns)
		{
			return GetExternalReportByFields(null, isInactive: true, columns);
		}

		public DataSet GetExternalReportByFields(string[] smartListID, params string[] columns)
		{
			return GetExternalReportByFields(smartListID, isInactive: true, columns);
		}

		public DataSet GetExternalReportByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("ExternalReport");
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
				commandHelper.FieldName = "ExternalReportID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "ExternalReport";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "ExternalReport", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCategoryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM ExternalReport_Category";
			FillDataSet(dataSet, "ExternalReport_Category", textCommand);
			return dataSet;
		}

		public DataSet GetExternalReportList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ExternalReportID [ExternalReport Code],ExternalReportName [ExternalReport Name]\r\n                           FROM ExternalReport ";
			FillDataSet(dataSet, "ExternalReport", textCommand);
			return dataSet;
		}

		public DataSet GetExternalReportComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ExternalReportID [Code],ExternalReportName [Name], CategoryName Category\r\n                           FROM ExternalReport SL INNER JOIN  ExternalReport_Category SLC ON SL.CategoryID =SLC.CategoryID ORDER BY ExternalReportID,ExternalReportName";
			FillDataSet(dataSet, "ExternalReport", textCommand);
			return dataSet;
		}

		public DataSet GetSubExternalReportComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ExternalReportID [Code],ExternalReportName [Name], CategoryName Category\r\n                           FROM ExternalReport SL INNER JOIN  Smartlist_Category SLC ON SL.CategoryID =SLC.CategoryID WHERE ISNULL(IsSubReport,'False') = 'True' ORDER BY ExternalReportID,ExternalReportName";
			FillDataSet(dataSet, "ExternalReport", textCommand);
			return dataSet;
		}

		public DataSet GetExternalReportCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "\r\n\t\t\t\t\t\t        WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT CategoryID, CAST( CategoryName as varchar(255)) AS FullPath,CategoryName ,ParentID,0 AS L FROM ExternalReport_Category AG\r\n\t                                WHERE ParentID IS NULL \r\n\t                                UNION ALL\r\n\t                                SELECT AG.CategoryID, CAST((d.FullPath + '->' + AG.CategoryName) AS Varchar(255)) AS X,  AG.CategoryName AS CategoryName,AG.ParentID,L+1 AS L\r\n                                    FROM ExternalReport_Category AG\r\n                                    INNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.CategoryID\r\n                                )\r\n\r\n                                SELECT  CAST(CategoryID AS nvarchar(20)) AS Code,SPACE(L*3) +  CategoryName AS Name,FullPath  FROM ACCGroups ORDER BY FullPath";
			FillDataSet(dataSet, "ExternalReport_Category", textCommand);
			return dataSet;
		}

		public DataSet GetReportByQuery(string query, DateTime fromDate, DateTime toDate)
		{
			try
			{
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				DataSet dataSet = new DataSet();
				if (!SQLHelper.ValidateQuerySecurity(query))
				{
					throw new CompanyException("report query does not allow one or more keywords used in this query.");
				}
				query = dataHelper.ReplaceSystemParameters(query, fromDate, toDate);
				FillDataSet(dataSet, "Report", query);
				return dataSet;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
