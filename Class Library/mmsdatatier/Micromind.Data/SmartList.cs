using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SmartList : StoreObject
	{
		private const string SMARTLISTID_PARM = "@SmartListID";

		private const string SMARTLISTNAME_PARM = "@SmartListName";

		public const string SMARTLIST_TABLE = "SmartList";

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

		private const string NOTE_PARM = "@Note";

		private const string DISPLAYNOTE_PARM = "@DisplayNote";

		private const string ISHIDEDATEFILTER_PARM = "@IsHideDateFilter";

		private const string ISSETDATEEQUALTO_PARM = "@IsSetDateEqualTo";

		private const string ISPREVIEW_PARM = "@IsPreview";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public SmartList(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("SmartList", new FieldValue("Description", "@Description"), new FieldValue("Query", "@Query"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("ParentID", "@ParentID"), new FieldValue("ReportData", "@ReportData"), new FieldValue("DrillParm1", "@DrillParm1"), new FieldValue("DrillParm2", "@DrillParm2"), new FieldValue("DrillParm3", "@DrillParm3"), new FieldValue("DrillParm4", "@DrillParm4"), new FieldValue("DrillSubReportID", "@DrillSubReportID"), new FieldValue("IsSubReport", "@IsSubReport"), new FieldValue("IsPreview", "@IsPreview"), new FieldValue("IsHideDateFilter", "@IsHideDateFilter"), new FieldValue("IsSetDateEqualTo", "@IsSetDateEqualTo"), new FieldValue("Note", "@Note"), new FieldValue("DisplayNote", "@DisplayNote"), new FieldValue("DrillAction", "@DrillAction"), new FieldValue("DrillCardTypeID", "@DrillCardTypeID"), new FieldValue("DrillCardIDField", "@DrillCardIDField"), new FieldValue("DrillTransactionSysDocIDField", "@DrillTransactionSysDocIDField"), new FieldValue("DrillTransactionVoucherIDField", "@DrillTransactionVoucherIDField"), new FieldValue("SmartListName", "@SmartListName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("SmartList", new FieldValue("SmartListID", "@SmartListID", isUpdateConditionField: true));
			}
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("SmartList", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
				parameters.Add("@SmartListID", SqlDbType.NVarChar);
			}
			parameters.Add("@SmartListName", SqlDbType.NVarChar);
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
			parameters.Add("@IsHideDateFilter", SqlDbType.Bit);
			parameters.Add("@IsSetDateEqualTo", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@DisplayNote", SqlDbType.NVarChar);
			parameters.Add("@DrillParm1", SqlDbType.NVarChar);
			parameters.Add("@DrillParm2", SqlDbType.NVarChar);
			parameters.Add("@DrillParm3", SqlDbType.NVarChar);
			parameters.Add("@DrillParm4", SqlDbType.NVarChar);
			parameters.Add("@IsSubReport", SqlDbType.Bit);
			parameters.Add("@DrillSubReportID", SqlDbType.Int);
			if (isUpdate)
			{
				parameters["@SmartListID"].SourceColumn = "SmartListID";
			}
			parameters["@SmartListName"].SourceColumn = "SmartListName";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Query"].SourceColumn = "Query";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@ParentID"].SourceColumn = "ParentID";
			parameters["@ReportData"].SourceColumn = "ReportData";
			parameters["@IsPreview"].SourceColumn = "IsPreview";
			parameters["@IsHideDateFilter"].SourceColumn = "IsHideDateFilter";
			parameters["@IsSetDateEqualTo"].SourceColumn = "IsSetDateEqualTo";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@DisplayNote"].SourceColumn = "DisplayNote";
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

		public byte[] GetSmartListData(string reportID, DateTime fromDate, DateTime toDate, string[] parameters, string[] parameterValues)
		{
			try
			{
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				DataRow dataRow = GetSmartListByID(reportID).Tables[0].Rows[0];
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
				return CommonLib.CompressDataSet(dataSet);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertSmartList(SmartListData accountSmartListData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountSmartListData, "SmartList", insertUpdateCommand);
				string text = accountSmartListData.SmartListTable.Rows[0]["SmartListID"].ToString();
				AddActivityLog("Smart List", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("SmartList", "SmartListID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateSmartList(SmartListData accountSmartListData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountSmartListData, "SmartList", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountSmartListData.SmartListTable.Rows[0]["SmartListID"];
				UpdateTableRowByID("SmartList", "SmartListID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountSmartListData.SmartListTable.Rows[0]["SmartListName"].ToString();
				AddActivityLog("Smart List", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("SmartList", "SmartListID", obj, sqlTransaction, isInsert: false);
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

		public SmartListData GetSmartList()
		{
			string textCommand = "SELECT * FROM SmartList WHERE ISNULL(IsSubReport,'False') = 'False' ";
			SmartListData smartListData = new SmartListData();
			FillDataSet(smartListData, "SmartList", textCommand);
			return smartListData;
		}

		public bool DeleteSmartList(string smartListID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM SmartList WHERE SmartListID = '" + smartListID + "'";
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
				string commandText = "DELETE FROM SmartList_Category WHERE CategoryID = " + smartListID;
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
				SqlCommand command = new SqlCommand(" INSERT INTO SMARTLIST_CATEGORY (CategoryName,ParentID) VALUES('" + categoryName + "'," + text + ")");
				return ExecuteNonQuery(command) > 0;
			}
			catch
			{
				throw;
			}
		}

		public int CreateCategory(string categoryName, string parentID)
		{
			int num = 0;
			try
			{
				string text = "NULL";
				if (int.Parse(parentID) >= 0)
				{
					text = parentID.ToString();
				}
				SqlCommand command = new SqlCommand(" INSERT INTO SMARTLIST_CATEGORY (CategoryName,ParentID) VALUES('" + categoryName + "'," + text + ");SELECT SCOPE_IDENTITY()");
				return int.Parse(ExecuteScalar(command).ToString());
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
				SqlCommand command = new SqlCommand("UPDATE SMARTLIST_CATEGORY SET CategoryName = '" + categoryName + "',ParentID=" + text + " where CategoryID=" + categoryID.ToString());
				return ExecuteNonQuery(command) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateCategory(string categoryName, int parentID, int categoryID, int rowIndex)
		{
			bool flag = true;
			try
			{
				string text = "NULL";
				if (parentID >= 0)
				{
					text = parentID.ToString();
				}
				SqlCommand command = new SqlCommand("UPDATE SMARTLIST_CATEGORY SET RowIndex=" + rowIndex + " , CategoryName = '" + categoryName + "',ParentID=" + text + " where CategoryID=" + categoryID.ToString());
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
				SqlCommand command = new SqlCommand("UPDATE SMARTLIST SET SmartListName = '" + reportName + "' where SmartListID=" + reportID.ToString());
				return ExecuteNonQuery(command) > 0;
			}
			catch
			{
				throw;
			}
		}

		public SmartListData GetSmartListByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SmartListID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "SmartList";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			SmartListData smartListData = new SmartListData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(smartListData, "SmartList", sqlBuilder);
			return smartListData;
		}

		public DataSet GetSmartListByFields(params string[] columns)
		{
			return GetSmartListByFields(null, isInactive: true, columns);
		}

		public DataSet GetSmartListByFields(string[] smartListID, params string[] columns)
		{
			return GetSmartListByFields(smartListID, isInactive: true, columns);
		}

		public DataSet GetSmartListByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("SmartList");
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
				commandHelper.FieldName = "SmartListID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "SmartList";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "SmartList", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCategoryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM Smartlist_Category  Order By RowIndex, CategoryID";
			FillDataSet(dataSet, "Smartlist_Category", textCommand);
			return dataSet;
		}

		public DataSet GetCategoryListById(int Id)
		{
			DataSet dataSet = new DataSet();
			string textCommand = $"SELECT * FROM Smartlist_Category  Where  CategoryID={Id}";
			FillDataSet(dataSet, "Smartlist_Category", textCommand);
			return dataSet;
		}

		public DataSet GetSmartListList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SmartListID [SmartList Code],SmartListName [SmartList Name]\r\n                           FROM SmartList ";
			FillDataSet(dataSet, "SmartList", textCommand);
			return dataSet;
		}

		public DataSet GetSmartListComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SmartListID [Code],SmartListName [Name], CategoryName Category\r\n                           FROM SmartList SL INNER JOIN  Smartlist_Category SLC ON SL.CategoryID =SLC.CategoryID ORDER BY SmartListID,SmartListName";
			FillDataSet(dataSet, "SmartList", textCommand);
			return dataSet;
		}

		public DataSet GetSubSmartListComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SmartListID [Code],SmartListName [Name], CategoryName Category\r\n                           FROM SmartList SL INNER JOIN  Smartlist_Category SLC ON SL.CategoryID =SLC.CategoryID WHERE ISNULL(IsSubReport,'False') = 'True' ORDER BY SmartListID,SmartListName";
			FillDataSet(dataSet, "SmartList", textCommand);
			return dataSet;
		}

		public DataSet GetSmartListCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "\r\n\t\t\t\t\t\t        WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT CategoryID, CAST( CategoryName as varchar(255)) AS FullPath,CategoryName ,ParentID,0 AS L FROM Smartlist_Category AG\r\n\t                                WHERE ParentID IS NULL \r\n\t                                UNION ALL\r\n\t                                SELECT AG.CategoryID, CAST((d.FullPath + '->' + AG.CategoryName) AS Varchar(255)) AS X,  AG.CategoryName AS CategoryName,AG.ParentID,L+1 AS L\r\n                                    FROM Smartlist_Category AG\r\n                                    INNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.CategoryID\r\n                                )\r\n\r\n                                SELECT  CAST(CategoryID AS nvarchar(20)) AS Code,SPACE(L*3) +  CategoryName AS Name,FullPath  FROM ACCGroups ORDER BY FullPath";
			FillDataSet(dataSet, "SmartList_Category", textCommand);
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

		public int GetNextCategoryID()
		{
			new DataSet();
			_ = string.Empty;
			string empty = string.Empty;
			empty = "SELECT  TOP 1 CategoryID  AS ID FROM SMARTLIST_CATEGORY Order By CategoryID Desc";
			object obj = ExecuteScalar(empty);
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}

		public bool UpdateSmartListIndex(string categoryID, int index)
		{
			bool flag = true;
			try
			{
				SqlCommand command = new SqlCommand("UPDATE SMARTLIST_CATEGORY SET RowIndex=" + index + " where CategoryID=" + categoryID.ToString());
				return ExecuteNonQuery(command) > 0;
			}
			catch
			{
				throw;
			}
		}

		public int GetNextIndex()
		{
			DataSet dataSet = new DataSet();
			_ = string.Empty;
			int result = 0;
			int result2 = 0;
			string empty = string.Empty;
			empty = "select Max(RowIndex) AS Index from Smartlist_Category";
			FillDataSet(dataSet, "SmartList_Category", empty);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 && int.TryParse(dataSet.Tables[0].Rows[0]["Index"].ToString(), out result))
			{
				result2 = result + 1;
			}
			return result2;
		}
	}
}
