using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CostCenter : StoreObject
	{
		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string COSTCENTERNAME_PARM = "@CostCenterName";

		private const string CASHRECEIPTACCOUNTID_PARM = "@CashReceiptAccountID";

		private const string PDCRECEIPTACCOUNTID_PARM = "@PDCReceiptAccountID";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		public const string COSTCENTER_TABLE = "Cost_Center";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CostCenter(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cost_Center", new FieldValue("CostCenterID", "@CostCenterID", isUpdateConditionField: true), new FieldValue("CostCenterName", "@CostCenterName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Cost_Center", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@CostCenterName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@CostCenterName"].SourceColumn = "CostCenterName";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertCostCenter(CostCenterData accountCostCenterData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCostCenterData, "Cost_Center", insertUpdateCommand);
				string text = accountCostCenterData.CostCenterTable.Rows[0]["CostCenterID"].ToString();
				AddActivityLog("Cost Center", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Cost_Center", "CostCenterID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCostCenter(CostCenterData accountCostCenterData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCostCenterData, "Cost_Center", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCostCenterData.CostCenterTable.Rows[0]["CostCenterID"];
				UpdateTableRowByID("Cost_Center", "CostCenterID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCostCenterData.CostCenterTable.Rows[0]["CostCenterName"].ToString();
				AddActivityLog("Cost Center", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Cost_Center", "CostCenterID", obj, sqlTransaction, isInsert: false);
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

		public CostCenterData GetCostCenter()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Cost_Center");
			CostCenterData costCenterData = new CostCenterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(costCenterData, "Cost_Center", sqlBuilder);
			return costCenterData;
		}

		public bool DeleteCostCenter(string costCenterID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Cost_Center WHERE CostCenterID = '" + costCenterID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Cost Center", costCenterID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CostCenterData GetCostCenterByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CostCenterID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Cost_Center";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CostCenterData costCenterData = new CostCenterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(costCenterData, "Cost_Center", sqlBuilder);
			return costCenterData;
		}

		public DataSet GetCostCenterByFields(params string[] columns)
		{
			return GetCostCenterByFields(null, isInactive: true, columns);
		}

		public DataSet GetCostCenterByFields(string[] costCenterID, params string[] columns)
		{
			return GetCostCenterByFields(costCenterID, isInactive: true, columns);
		}

		public DataSet GetCostCenterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Cost_Center");
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
				commandHelper.FieldName = "CostCenterID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Cost_Center";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Cost_Center", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCostCenterList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CostCenterID [Code],CostCenterName [Description],Note,Inactive  \r\n                           FROM Cost_Center ";
			FillDataSet(dataSet, "Cost_Center", textCommand);
			return dataSet;
		}

		public DataSet GetCostCenterComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CostCenterID [Code],CostCenterName [Name],ChequeReceiptSysDocID,CashReceiptSysDocID,ChequePaymentSysDocID,CashPaymentSysDocID\r\n                           FROM Cost_Center ORDER BY CostCenterID,CostCenterName";
			FillDataSet(dataSet, "Cost_Center", textCommand);
			return dataSet;
		}

		public string GetDefaultAccountID(string costCenterID, string accountFieldIDName)
		{
			string exp = "SELECT " + accountFieldIDName + " FROM Cost_Center WHERE CostCenterID='" + costCenterID + "'";
			return ExecuteScalar(exp).ToString();
		}
	}
}
