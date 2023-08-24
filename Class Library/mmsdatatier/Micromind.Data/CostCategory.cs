using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CostCategory : StoreObject
	{
		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string COSTCATEGORYNAME_PARM = "@CostCategoryName";

		private const string COSTCATEGORYDESC_PARM = "@CostCategoryDesc";

		private const string COSTTYPEID_PARM = "@CostTypeID";

		private const string PARENTCOSTCATEGORYID_PARM = "@ParentCostCategoryID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string INACTIVE_PARM = "@Inactive";

		private const string COSTCATEGORY_TABLE = "Job_Cost_Category";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CostCategory(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Cost_Category", new FieldValue("CostCategoryID", "@CostCategoryID", isUpdateConditionField: true), new FieldValue("CostCategoryName", "@CostCategoryName"), new FieldValue("Description", "@CostCategoryDesc"), new FieldValue("CostTypeID", "@CostTypeID"), new FieldValue("ParentCostCategoryID", "@ParentCostCategoryID"), new FieldValue("AccountID", "@AccountID"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Cost_Category", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryName", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryDesc", SqlDbType.NVarChar);
			parameters.Add("@CostTypeID", SqlDbType.TinyInt);
			parameters.Add("@ParentCostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@CostCategoryName"].SourceColumn = "CostCategoryName";
			parameters["@CostCategoryDesc"].SourceColumn = "Description";
			parameters["@CostTypeID"].SourceColumn = "CostTypeID";
			parameters["@ParentCostCategoryID"].SourceColumn = "ParentCostCategoryID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertCostCategory(CostCategoryData accountCostCategoryData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCostCategoryData, "Job_Cost_Category", insertUpdateCommand);
				string text = accountCostCategoryData.CostCategoryTable.Rows[0]["CostCategoryID"].ToString();
				AddActivityLog("CostCategory", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Cost_Category", "CostCategoryID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCostCategory(CostCategoryData accountCostCategoryData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCostCategoryData, "Job_Cost_Category", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCostCategoryData.CostCategoryTable.Rows[0]["CostCategoryID"];
				UpdateTableRowByID("Job_Cost_Category", "CostCategoryID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCostCategoryData.CostCategoryTable.Rows[0]["CostCategoryName"].ToString();
				AddActivityLog("CostCategory", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Cost_Category", "CostCategoryID", obj, sqlTransaction, isInsert: false);
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

		public CostCategoryData GetCostCategory()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Cost_Category");
			CostCategoryData costCategoryData = new CostCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(costCategoryData, "Job_Cost_Category", sqlBuilder);
			return costCategoryData;
		}

		public bool DeleteCostCategory(string costCategoryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Cost_Category WHERE CostCategoryID = '" + costCategoryID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CostCategory", costCategoryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CostCategoryData GetCostCategoryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CostCategoryID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Job_Cost_Category";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CostCategoryData costCategoryData = new CostCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(costCategoryData, "Job_Cost_Category", sqlBuilder);
			return costCategoryData;
		}

		public DataSet GetCostCategoryByFields(params string[] columns)
		{
			return GetCostCategoryByFields(null, isInactive: true, columns);
		}

		public DataSet GetCostCategoryByFields(string[] jobTypeID, params string[] columns)
		{
			return GetCostCategoryByFields(jobTypeID, isInactive: true, columns);
		}

		public DataSet GetCostCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Cost_Category");
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
				commandHelper.FieldName = "CostCategoryID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Job_Cost_Category";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Job_Cost_Category", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCostCategoryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CostCategoryID [Type Code],CostCategoryName [Type Name], Inactive\r\n                           FROM Job_Cost_Category";
			FillDataSet(dataSet, "Job_Cost_Category", textCommand);
			return dataSet;
		}

		public DataSet GetCostCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CostCategoryID [Code], CostCategoryName [Name]\r\n                           FROM Job_Cost_Category ORDER BY CostCategoryID, CostCategoryName";
			FillDataSet(dataSet, "Job_Cost_Category", textCommand);
			return dataSet;
		}
	}
}
