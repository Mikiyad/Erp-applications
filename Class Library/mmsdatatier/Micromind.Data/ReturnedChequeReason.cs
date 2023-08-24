using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ReturnedChequeReason : StoreObject
	{
		private const string REASONID_PARM = "@ReasonID";

		private const string REASONNAME_PARM = "@ReasonName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ReturnedChequeReason(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Returned_Cheque_Reason", new FieldValue("ReasonID", "@ReasonID", isUpdateConditionField: true), new FieldValue("ReasonName", "@ReasonName"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Returned_Cheque_Reason", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ReasonID", SqlDbType.NVarChar);
			parameters.Add("@ReasonName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@ReasonID"].SourceColumn = "ReasonID";
			parameters["@ReasonName"].SourceColumn = "ReasonName";
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

		public bool InsertReturnedChequeReason(ReturnedChequeReasonData accountReturnedChequeReasonData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountReturnedChequeReasonData, "Returned_Cheque_Reason", insertUpdateCommand);
				string text = accountReturnedChequeReasonData.ReturnedChequeReasonTable.Rows[0]["ReasonID"].ToString();
				AddActivityLog("Returned Cheque Reason", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Returned_Cheque_Reason", "ReasonID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateReturnedChequeReason(ReturnedChequeReasonData accountReturnedChequeReasonData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountReturnedChequeReasonData, "Returned_Cheque_Reason", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountReturnedChequeReasonData.ReturnedChequeReasonTable.Rows[0]["ReasonID"];
				UpdateTableRowByID("Returned_Cheque_Reason", "ReasonID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountReturnedChequeReasonData.ReturnedChequeReasonTable.Rows[0]["ReasonName"].ToString();
				AddActivityLog("Returned Cheque Reason", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Returned_Cheque_Reason", "ReasonID", obj, sqlTransaction, isInsert: false);
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

		public ReturnedChequeReasonData GetReturnedChequeReason()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Returned_Cheque_Reason");
			ReturnedChequeReasonData returnedChequeReasonData = new ReturnedChequeReasonData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(returnedChequeReasonData, "Returned_Cheque_Reason", sqlBuilder);
			return returnedChequeReasonData;
		}

		public bool DeleteReturnedChequeReason(string returnedChequeReasonID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Returned_Cheque_Reason WHERE ReasonID = '" + returnedChequeReasonID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Returned Cheque Reason", returnedChequeReasonID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ReturnedChequeReasonData GetReturnedChequeReasonByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ReasonID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Returned_Cheque_Reason";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ReturnedChequeReasonData returnedChequeReasonData = new ReturnedChequeReasonData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(returnedChequeReasonData, "Returned_Cheque_Reason", sqlBuilder);
			return returnedChequeReasonData;
		}

		public DataSet GetReturnedChequeReasonByFields(params string[] columns)
		{
			return GetReturnedChequeReasonByFields(null, isInactive: true, columns);
		}

		public DataSet GetReturnedChequeReasonByFields(string[] returnedChequeReasonID, params string[] columns)
		{
			return GetReturnedChequeReasonByFields(returnedChequeReasonID, isInactive: true, columns);
		}

		public DataSet GetReturnedChequeReasonByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Returned_Cheque_Reason");
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
				commandHelper.FieldName = "ReasonID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Returned_Cheque_Reason";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Returned_Cheque_Reason", sqlBuilder);
			return dataSet;
		}

		public DataSet GetReturnedChequeReasonList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ReasonID [Reason Code],ReasonName [Reason Name],Inactive  \r\n                           FROM Returned_Cheque_Reason ";
			FillDataSet(dataSet, "Returned_Cheque_Reason", textCommand);
			return dataSet;
		}

		public DataSet GetReturnedChequeReasonComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ReasonID [Code],ReasonName [Name]\r\n                           FROM Returned_Cheque_Reason ORDER BY ReasonID,ReasonName";
			FillDataSet(dataSet, "Returned_Cheque_Reason", textCommand);
			return dataSet;
		}
	}
}
