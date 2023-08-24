using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Chequebook : StoreObject
	{
		private const string CHEQUEBOOKID_PARM = "@ChequebookID";

		private const string CHEQUEBOOKNAME_PARM = "@ChequebookName";

		private const string BANKID_PARM = "@BankID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string PDCISSUEDACCOUNTID_PARM = "@PDCIssuedAccountID";

		private const string NEXTNUMBER_PARM = "@NextNumber";

		private const string SIGNATORY_PARM = "@Signatory";

		private const string TEMPLATENAME_PARM = "@TemplateName";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		public const string CHEQUEBOOK_TABLE = "Chequebook";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Chequebook(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Chequebook", new FieldValue("ChequebookID", "@ChequebookID", isUpdateConditionField: true), new FieldValue("ChequebookName", "@ChequebookName"), new FieldValue("BankID", "@BankID"), new FieldValue("AccountID", "@AccountID"), new FieldValue("PDCIssuedAccountID", "@PDCIssuedAccountID"), new FieldValue("Signatory", "@Signatory"), new FieldValue("TemplateName", "@TemplateName"), new FieldValue("Status", "@Status"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Chequebook", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@ChequebookName", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@PDCIssuedAccountID", SqlDbType.NVarChar);
			parameters.Add("@Signatory", SqlDbType.NVarChar);
			parameters.Add("@TemplateName", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@ChequebookName"].SourceColumn = "ChequebookName";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@PDCIssuedAccountID"].SourceColumn = "PDCIssuedAccountID";
			parameters["@Signatory"].SourceColumn = "Signatory";
			parameters["@TemplateName"].SourceColumn = "TemplateName";
			parameters["@Status"].SourceColumn = "Status";
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

		public bool InsertChequebook(ChequebookData accountChequebookData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountChequebookData, "Chequebook", insertUpdateCommand);
				string text = accountChequebookData.ChequebookTable.Rows[0]["ChequebookID"].ToString();
				AddActivityLog("Chequebook", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Chequebook", "ChequebookID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateChequebook(ChequebookData accountChequebookData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountChequebookData, "Chequebook", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountChequebookData.ChequebookTable.Rows[0]["ChequebookID"];
				UpdateTableRowByID("Chequebook", "ChequebookID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountChequebookData.ChequebookTable.Rows[0]["ChequebookName"].ToString();
				AddActivityLog("Chequebook", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Chequebook", "ChequebookID", obj, sqlTransaction, isInsert: false);
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

		public bool RegisterCheque(string chequebookID, int from, int to)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				for (int i = from; i <= to; i++)
				{
					SqlCommand sqlCommand = new SqlCommand("INSERT INTO Cheque_Register (ChequebookID,ChequeNumber) Values ('" + chequebookID + "'," + i.ToString() + ")");
					sqlCommand.Transaction = sqlTransaction;
					flag &= (ExecuteNonQuery(sqlCommand) > 0);
				}
				AddActivityLog("Register Cheque", from.ToString() + "-" + to.ToString(), ActivityTypes.Add, sqlTransaction);
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

		public ChequebookData GetChequebook()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Chequebook");
			ChequebookData chequebookData = new ChequebookData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(chequebookData, "Chequebook", sqlBuilder);
			return chequebookData;
		}

		public bool DeleteChequebook(string chequebookID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Chequebook WHERE ChequebookID = '" + chequebookID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Chequebook", chequebookID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ChequebookData GetChequebookByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ChequebookID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Chequebook";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ChequebookData chequebookData = new ChequebookData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(chequebookData, "Chequebook", sqlBuilder);
			return chequebookData;
		}

		public DataSet GetChequebookByFields(params string[] columns)
		{
			return GetChequebookByFields(null, isInactive: true, columns);
		}

		public DataSet GetChequebookByFields(string[] chequebookID, params string[] columns)
		{
			return GetChequebookByFields(chequebookID, isInactive: true, columns);
		}

		public DataSet GetChequebookByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Chequebook");
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
				commandHelper.FieldName = "ChequebookID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Chequebook";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Chequebook", sqlBuilder);
			return dataSet;
		}

		public int ExistChequeNumber(string chequebookID, int from, int to)
		{
			string exp = "SELECT TOP 1 ChequeNumber FROM Cheque_Register WHERE ChequeNumber>=" + from.ToString() + " AND ChequeNumber<=" + to.ToString() + " AND ChequebookID = '" + chequebookID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public int GetLastChequeNumber(string chequebookID)
		{
			string exp = "SELECT MAX (ChequeNumber) FROM Cheque_Register WHERE ChequebookID='" + chequebookID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString());
			}
			return 0;
		}

		public int GetNextChequeNumber(string chequebookID)
		{
			string exp = "SELECT MIN(ChequeNumber) FROM Cheque_Register WHERE ChequebookID='" + chequebookID + "' AND Status=1";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString());
			}
			return 1;
		}

		public decimal GetChequebookBalance(string chequebookID, bool includeOD)
		{
			if (chequebookID == "")
			{
				return 0m;
			}
			string str = "SELECT SUM(ISNULL(Debit,0)) - SUM(ISNULL(Credit,0)) ";
			if (includeOD)
			{
				str += " + ISNULL((SELECT LimitAmount FROM Bank_Facility WHERE CurrentAccountID = (SELECT AccountID FROM Chequebook WHERE ChequebookID = 'CBD') AND FacilityType = 5 AND Status = 1),0) ";
			}
			str = str + " FROM Journal_Details\r\n                                WHERE AccountID = (SELECT AccountID FROM Chequebook WHERE ChequebookID = '" + chequebookID + "')";
			object obj = ExecuteScalar(str);
			if (obj == null || obj.ToString() == "")
			{
				return 0m;
			}
			return decimal.Parse(obj.ToString());
		}

		public DataSet GetChequebookList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequebookID [Chequebook Code],ChequebookName [Chequebook Name],Status,Note  \r\n                           FROM Chequebook ";
			FillDataSet(dataSet, "Chequebook", textCommand);
			return dataSet;
		}

		public DataSet GetChequebookComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequebookID [Code],ChequebookName [Name]\r\n                           FROM Chequebook WHERE Status = 1 ORDER BY ChequebookID,ChequebookName";
			FillDataSet(dataSet, "Chequebook", textCommand);
			return dataSet;
		}
	}
}
