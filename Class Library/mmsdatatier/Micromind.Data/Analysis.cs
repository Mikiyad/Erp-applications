using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Analysis : StoreObject
	{
		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string ANALYSISNAME_PARM = "@AnalysisName";

		private const string DESCRIPTION_PARM = "@Description";

		private const string INACTIVE_PARM = "@Inactive";

		private const string GROUPID_PARM = "@GroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Analysis(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Analysis", new FieldValue("AnalysisID", "@AnalysisID", isUpdateConditionField: true), new FieldValue("AnalysisName", "@AnalysisName"), new FieldValue("Description", "@Description"), new FieldValue("Inactive", "@Inactive"), new FieldValue("GroupID", "@GroupID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Analysis", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@AnalysisName", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@AnalysisName"].SourceColumn = "AnalysisName";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@GroupID"].SourceColumn = "GroupID";
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

		public bool InsertAnalysis(AnalysisData accountAnalysisData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountAnalysisData, "Analysis", insertUpdateCommand);
				string text = accountAnalysisData.AnalysisTable.Rows[0]["AnalysisID"].ToString();
				AddActivityLog(" Analysis", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Analysis", "AnalysisID", text, sqlTransaction, isInsert: true);
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Analysis, text, "Analysis", "AnalysisID", sqlTransaction);
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

		public bool UpdateAnalysis(AnalysisData accountAnalysisData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAnalysisData, "Analysis", insertUpdateCommand);
				object obj = accountAnalysisData.AnalysisTable.Rows[0]["AnalysisID"];
				if (flag)
				{
					UpdateTableRowByID("Analysis", "AnalysisID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
					string entiyID = accountAnalysisData.AnalysisTable.Rows[0]["AnalysisName"].ToString();
					AddActivityLog("Analysis", entiyID, ActivityTypes.Update, sqlTransaction);
					UpdateTableRowInsertUpdateInfo("Analysis", "AnalysisID", obj, sqlTransaction, isInsert: false);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Analysis, obj.ToString(), "Analysis", "AnalysisID", sqlTransaction);
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

		public AnalysisData GetAnalysis()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Analysis");
			AnalysisData analysisData = new AnalysisData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(analysisData, "Analysis", sqlBuilder);
			return analysisData;
		}

		public bool DeleteAnalysis(string analysisID)
		{
			bool flag = true;
			try
			{
				if (GetTransactionDetails(analysisID).Tables[0].Rows.Count > 0)
				{
					return false;
				}
				string commandText = "DELETE FROM Analysis WHERE AnalysisID = '" + analysisID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Analysis", analysisID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTransactionDetails(string analysisID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * from Journal_Details where AnalysisID='" + analysisID + "'";
			FillDataSet(dataSet, "GLDetails", textCommand);
			return dataSet;
		}

		public AnalysisData GetAnalysisByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "AnalysisID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Analysis";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			AnalysisData analysisData = new AnalysisData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(analysisData, "Analysis", sqlBuilder);
			return analysisData;
		}

		public DataSet GetAnalysisByFields(params string[] columns)
		{
			return GetAnalysisByFields(null, isInactive: true, columns);
		}

		public DataSet GetAnalysisByFields(string[] analysisID, params string[] columns)
		{
			return GetAnalysisByFields(analysisID, isInactive: true, columns);
		}

		public DataSet GetAnalysisByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Analysis");
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
				commandHelper.FieldName = "AnalysisID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Analysis";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "Inactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Analysis";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Analysis", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAnalysisList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AnalysisID [Code],AnalysisName [Name],Analysis.Description,GroupName AS [Group],Analysis.Inactive AS [Inactive]\r\n                           FROM Analysis INNER JOIN Analysis_Group AG ON Analysis.GroupID=AG.GroupID ORDER BY AnalysisID";
			FillDataSet(dataSet, "Analysis", textCommand);
			return dataSet;
		}

		public DataSet GetAnalysisComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AnalysisID Code,AnalysisName [Name] ,AAD.AccountID,AAD.AnalysisGroupID FROM Account_Analysis_Detail AAD \r\n                                INNER JOIN Analysis ON AAD.AnalysisGroupID=Analysis.GroupID\r\n                                WHERE Analysis.Inactive<>1 OR Analysis.Inactive IS NULL\r\n                                GROUP BY AnalysisID,AAD.AccountID,AAD.AnalysisGroupID,AnalysisName";
			FillDataSet(dataSet, "Analysis", textCommand);
			return dataSet;
		}

		public DataSet GetAnalysisNonAccountComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AnalysisID Code,AnalysisName [Name] FROM Account_Analysis_Detail AAD \r\n                                INNER JOIN Analysis ON AAD.AnalysisGroupID=Analysis.GroupID\r\n                                WHERE Analysis.Inactive<>1 OR Analysis.Inactive IS NULL\r\n                                GROUP BY AnalysisID,AnalysisName";
			FillDataSet(dataSet, "Analysis", textCommand);
			return dataSet;
		}

		public bool InsertAutoAnalysis(DataSet accountAnalysisData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountAnalysisData, "Analysis", insertUpdateCommand);
				string text = accountAnalysisData.Tables[0].Rows[0]["AnalysisID"].ToString();
				AddActivityLog(" Analysis", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Analysis", "AnalysisID", text, sqlTransaction, isInsert: true);
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Analysis, text, "Analysis", "AnalysisID", sqlTransaction);
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
	}
}
