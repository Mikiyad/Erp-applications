using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Register : StoreObject
	{
		private const string REGISTERID_PARM = "@RegisterID";

		private const string REGISTERNAME_PARM = "@RegisterName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NOTE_PARM = "@Note";

		private const string CASHACCOUNTID_PARM = "@CashAccountID";

		private const string PDCRECEIVEDACCOUNTID_PARM = "@PDCReceivedAccountID";

		private const string PDCISSUEDACCOUNTID_PARM = "@PDCIssuedAccountID";

		private const string CARDRECEIVEDACCOUNTID_PARM = "@CardReceivedAccountID";

		private const string REGISTER_TABLE = "Register";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Register(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Register", new FieldValue("RegisterID", "@RegisterID", isUpdateConditionField: true), new FieldValue("RegisterName", "@RegisterName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("CashAccountID", "@CashAccountID"), new FieldValue("PDCReceivedAccountID", "@PDCReceivedAccountID"), new FieldValue("PDCIssuedAccountID", "@PDCIssuedAccountID"), new FieldValue("CardReceivedAccountID", "@CardReceivedAccountID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Register", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@RegisterName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@CashAccountID", SqlDbType.NVarChar);
			parameters.Add("@PDCReceivedAccountID", SqlDbType.NVarChar);
			parameters.Add("@PDCIssuedAccountID", SqlDbType.NVarChar);
			parameters.Add("@CardReceivedAccountID", SqlDbType.NVarChar);
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@RegisterName"].SourceColumn = "RegisterName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@CashAccountID"].SourceColumn = "CashAccountID";
			parameters["@PDCReceivedAccountID"].SourceColumn = "PDCReceivedAccountID";
			parameters["@PDCIssuedAccountID"].SourceColumn = "PDCIssuedAccountID";
			parameters["@CardReceivedAccountID"].SourceColumn = "CardReceivedAccountID";
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

		public bool InsertRegister(RegisterData accountRegisterData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountRegisterData, "Register", insertUpdateCommand);
				string text = accountRegisterData.RegisterTable.Rows[0]["RegisterID"].ToString();
				AddActivityLog("Register", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Register", "RegisterID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateRegister(RegisterData accountRegisterData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountRegisterData, "Register", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountRegisterData.RegisterTable.Rows[0]["RegisterID"];
				UpdateTableRowByID("Register", "RegisterID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountRegisterData.RegisterTable.Rows[0]["RegisterName"].ToString();
				AddActivityLog("Register", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Register", "RegisterID", obj, sqlTransaction, isInsert: false);
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

		public RegisterData GetRegister()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Register");
			RegisterData registerData = new RegisterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(registerData, "Register", sqlBuilder);
			return registerData;
		}

		public bool DeleteRegister(string registerID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Register WHERE RegisterID = '" + registerID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Register", registerID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public RegisterData GetRegisterByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "RegisterID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Register";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			RegisterData registerData = new RegisterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(registerData, "Register", sqlBuilder);
			return registerData;
		}

		public DataSet GetRegisterByFields(params string[] columns)
		{
			return GetRegisterByFields(null, isInactive: true, columns);
		}

		public DataSet GetRegisterByFields(string[] registerID, params string[] columns)
		{
			return GetRegisterByFields(registerID, isInactive: true, columns);
		}

		public DataSet GetRegisterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Register");
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
				commandHelper.FieldName = "RegisterID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Register";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Register", sqlBuilder);
			return dataSet;
		}

		public DataSet GetRegisterList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT RegisterID [Register Code],RegisterName [Register Name],Note,Inactive  \r\n                           FROM Register ";
			FillDataSet(dataSet, "Register", textCommand);
			return dataSet;
		}

		public DataSet GetRegisterComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT RegisterID [Code],RegisterName [Name]\r\n                           FROM Register ORDER BY RegisterID,RegisterName";
			FillDataSet(dataSet, "Register", textCommand);
			return dataSet;
		}

		public string GetRegisterAccountID(string registerID, string accountFieldIDName)
		{
			string exp = "SELECT " + accountFieldIDName + " FROM Register WHERE RegisterID='" + registerID + "'";
			object obj = ExecuteScalar(exp);
			if (obj == null)
			{
				return "";
			}
			return obj.ToString();
		}

		public decimal GetRegisterBalance(string registerID, string accountFieldIDName)
		{
			string registerAccountID = GetRegisterAccountID(registerID, accountFieldIDName);
			string exp = "SELECT SUM(ISNULL(Debit,0)) - SUM(ISNULL(Credit,0)) FROM Journal_Details with (nolock)\r\n                                WHERE AccountID='" + registerAccountID + "'";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return 0m;
			}
			return decimal.Parse(obj.ToString());
		}
	}
}
