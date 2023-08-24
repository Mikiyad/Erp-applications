using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobBOMs : StoreObject
	{
		private const string JOBBOMID_PARM = "@JobBOMID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string BOMNAME_PARM = "@BOMName";

		private const string NOTE_PARM = "@Note";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string LABOURCOST_PARM = "@LabourCost";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string QUANTITY_PARM = "@Quantity";

		public JobBOMs(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_BOM", new FieldValue("JobBOMID", "@JobBOMID", isUpdateConditionField: true), new FieldValue("BOMName", "@BOMName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_BOM", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@JobBOMID", SqlDbType.NVarChar);
			parameters.Add("@BOMName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@JobBOMID"].SourceColumn = "JobBOMID";
			parameters["@BOMName"].SourceColumn = "BOMName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		private string GetInsertUpdateJobBOMDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_BOM_Detail", new FieldValue("JobBOMID", "@JobBOMID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("Cost", "@Cost"), new FieldValue("LabourCost", "@LabourCost"), new FieldValue("Quantity", "@Quantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobBOMDetailCommand(bool isUpdate)
		{
			SqlCommand sqlCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				sqlCommand = new SqlCommand(GetInsertUpdateJobBOMDetailText(isUpdate: true), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			else
			{
				sqlCommand = new SqlCommand(GetInsertUpdateJobBOMDetailText(isUpdate: false), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			parameters.Add("@JobBOMID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@LabourCost", SqlDbType.Money);
			parameters["@JobBOMID"].SourceColumn = "JobBOMID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@LabourCost"].SourceColumn = "LabourCost";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Quantity"].SourceColumn = "Quantity";
			return sqlCommand;
		}

		public bool InsertUpdateJobBOM(JobBOMData accountJobBOMData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountJobBOMData, "Job_BOM", insertUpdateCommand) : Update(accountJobBOMData, "Job_BOM", insertUpdateCommand));
				string text = accountJobBOMData.JobBOMTable.Rows[0]["JobBOMID"].ToString();
				accountJobBOMData.JobBOMTable.Rows[0]["BOMName"].ToString();
				if (flag)
				{
					if (isUpdate)
					{
						DeleteJobBOMDetail(sqlTransaction, text.ToString());
					}
					insertUpdateCommand = GetInsertUpdateJobBOMDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					if (accountJobBOMData.JobBOMDetailTable.Rows.Count > 0)
					{
						flag &= Insert(accountJobBOMData, "Job_BOM_Detail", insertUpdateCommand);
					}
				}
				if (isUpdate)
				{
					AddActivityLog("JobBOM", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("JobBOM", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Job_BOM", "JobBOMID", text, sqlTransaction, !isUpdate);
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.JobBOM, text.ToString(), "Job_BOM", "JobBOMID", sqlTransaction);
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

		private bool DeleteJobBOMDetail(SqlTransaction sqlTransaction, string bomID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_BOM_Detail WHERE JobBOMID = '" + bomID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("JobBOM Detail", bomID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public JobBOMData GetJobBOM()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_BOM");
			JobBOMData jobBOMData = new JobBOMData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobBOMData, "Job_BOM", sqlBuilder);
			return jobBOMData;
		}

		public bool DeleteJobBOM(string bomID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Job_BOM_Detail  WHERE JobBOMID = '" + bomID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM Job_BOM WHERE JobBOMID = '" + bomID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("JobBOM", bomID, ActivityTypes.Delete, null);
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

		public JobBOMData GetJobBOMByID(string id)
		{
			string textCommand = "SELECT * FROM Job_BOM WHERE JobBOMID = '" + id + "'";
			JobBOMData jobBOMData = new JobBOMData();
			FillDataSet(jobBOMData, "Job_BOM", textCommand);
			if (jobBOMData == null || jobBOMData.Tables.Count == 0 || jobBOMData.Tables[0].Rows.Count == 0)
			{
				return jobBOMData;
			}
			textCommand = "SELECT  JobBOMD.*,P.ItemType\r\n                        FROM Job_BOM_Detail AS JobBOMD INNER JOIN Job_BOM ON JobBOMD.JobBOMID = Job_BOM.JobBOMID\r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = JobBOMD.ProductID\r\n                            WHERE JobBOMD.JobBOMID='" + id + "' ORDER BY RowIndex";
			FillDataSet(jobBOMData, "Job_BOM_Detail", textCommand);
			return jobBOMData;
		}

		public JobBOMData GetJobBOMItemsByID(string id)
		{
			string textCommand = "SELECT * FROM Job_BOM WHERE JobBOMID = '" + id + "'";
			JobBOMData jobBOMData = new JobBOMData();
			FillDataSet(jobBOMData, "Job_BOM", textCommand);
			if (jobBOMData == null || jobBOMData.Tables.Count == 0 || jobBOMData.Tables[0].Rows.Count == 0)
			{
				return jobBOMData;
			}
			textCommand = "SELECT  JobBOMD.*,P.ItemType\r\n                        FROM Job_BOM_Detail AS JobBOMD INNER JOIN Job_BOM ON JobBOMD.JobBOMID = JobBOM.JobBOMID\r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = JobBOMD.ProductID\r\n                            WHERE JobBOMD.JobBOMID='" + id + "' ORDER BY RowIndex";
			FillDataSet(jobBOMData, "Job_BOM_Detail", textCommand);
			return jobBOMData;
		}

		public DataSet GetJobBOMByFields(params string[] columns)
		{
			return GetJobBOMByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobBOMByFields(string[] bomID, params string[] columns)
		{
			return GetJobBOMByFields(bomID, isInactive: true, columns);
		}

		public DataSet GetJobBOMByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_BOM");
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
				commandHelper.FieldName = "JobBOMID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Job_BOM";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Job_BOM";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Job_BOM", sqlBuilder);
			return dataSet;
		}

		public DataSet GetJobBOMList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT JobBOMID,BOMName,Note FROM Job_BOM  ";
			if (!showInactive)
			{
				text += " WHERE ISNULL(IsInactive,'False')='False'";
			}
			FillDataSet(dataSet, "Job_BOM", text);
			return dataSet;
		}

		public DataSet GetJobBOMComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT JobBOMID [Code],BOMName [Name]\r\n                            FROM Job_BOM\r\n                            WHERE ISINACTIVE<>1 ORDER BY JobBOMID,BOMName";
			FillDataSet(dataSet, "Job_BOM", textCommand);
			return dataSet;
		}
	}
}
