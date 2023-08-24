using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class POSCashRegister : StoreObject
	{
		public const string POSCASHREGISTER_TABLE = "POS_CashRegister";

		private const string CASHREGISTERID_PARM = "@POSCashRegisterID";

		private const string CASHREGISTERNAME_PARM = "@POSCashRegisterName";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string COMPUTERNAME_PARM = "@ComputerName";

		private const string ACCEPTCREDIT_PARM = "@AcceptCredit";

		private const string NOTE_PARM = "@Note";

		private const string DEFAULTCUSTOMERID_PARM = "@DefaultCustomerID";

		private const string DISCOUNTACCOUNTID_PARM = "@DiscountAccountID";

		private const string PETTYCASHACCOUNTID_PARM = "@PettyCashAccountID";

		private const string RECEIPTDOCID_PARM = "@ReceiptDocID";

		private const string EXPENSEDOCID_PARM = "@ExpenseDocID";

		private const string PAYMENTMETHODID_PARM = "@PaymentMethodID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DISPLAYNAME_PARM = "@DisplayName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public POSCashRegister(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("POS_CashRegister", new FieldValue("CashRegisterID", "@POSCashRegisterID", isUpdateConditionField: true), new FieldValue("CashRegisterName", "@POSCashRegisterName"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ReceiptDocID", "@ReceiptDocID"), new FieldValue("ExpenseDocID", "@ExpenseDocID"), new FieldValue("Note", "@Note"), new FieldValue("DefaultCustomerID", "@DefaultCustomerID"), new FieldValue("DiscountAccountID", "@DiscountAccountID"), new FieldValue("PettyCashAccountID", "@PettyCashAccountID"), new FieldValue("ComputerName", "@ComputerName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("POS_CashRegister", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@POSCashRegisterID", SqlDbType.NVarChar);
			parameters.Add("@POSCashRegisterName", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@DefaultCustomerID", SqlDbType.NVarChar);
			parameters.Add("@ReceiptDocID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseDocID", SqlDbType.NVarChar);
			parameters.Add("@DiscountAccountID", SqlDbType.NVarChar);
			parameters.Add("@PettyCashAccountID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ComputerName", SqlDbType.NVarChar);
			parameters["@POSCashRegisterID"].SourceColumn = "CashRegisterID";
			parameters["@POSCashRegisterName"].SourceColumn = "CashRegisterName";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@DefaultCustomerID"].SourceColumn = "DefaultCustomerID";
			parameters["@ReceiptDocID"].SourceColumn = "ReceiptDocID";
			parameters["@DiscountAccountID"].SourceColumn = "DiscountAccountID";
			parameters["@PettyCashAccountID"].SourceColumn = "PettyCashAccountID";
			parameters["@ExpenseDocID"].SourceColumn = "ExpenseDocID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@ComputerName"].SourceColumn = "ComputerName";
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

		private string GetInsertUpdatePaymentMethodText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("POS_CashRegister_PaymentMethod", new FieldValue("CashRegisterID", "@POSCashRegisterID", isUpdateConditionField: true), new FieldValue("PaymentMethodID", "@PaymentMethodID"), new FieldValue("AccountID", "@AccountID"), new FieldValue("DisplayName", "@DisplayName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentMethodCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentMethodText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentMethodText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@POSCashRegisterID", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@DisplayName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters["@POSCashRegisterID"].SourceColumn = "CashRegisterID";
			parameters["@PaymentMethodID"].SourceColumn = "PaymentMethodID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@DisplayName"].SourceColumn = "DisplayName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("POS_CashRegister_EXPENSE", new FieldValue("CashRegisterID", "@POSCashRegisterID", isUpdateConditionField: true), new FieldValue("AccountID", "@AccountID"), new FieldValue("DisplayName", "@DisplayName"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExpenseText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@POSCashRegisterID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@DisplayName", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters["@POSCashRegisterID"].SourceColumn = "CashRegisterID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@DisplayName"].SourceColumn = "DisplayName";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdatePOSCashRegister(POSCashRegisterData posCashRegisterData, bool isUpdate)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = ((!isUpdate) ? Insert(posCashRegisterData, "POS_CashRegister", insertUpdateCommand) : Update(posCashRegisterData, "POS_CashRegister", insertUpdateCommand));
				string text = posCashRegisterData.POSCashRegisterTable.Rows[0]["CashRegisterID"].ToString();
				if (isUpdate)
				{
					AddActivityLog("POSCashRegister", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("POSCashRegister", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("POS_CashRegister", "CashRegisterID", text, sqlTransaction, !isUpdate);
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

		private bool DeletePaymentMethods(string posCashRegisterID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM POS_CashRegister_PaymentMethod WHERE CashRegisterID = '" + posCashRegisterID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("POS Payment Method", posCashRegisterID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private bool DeleteExpenseAccounts(string posCashRegisterID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM POS_CashRegister_EXPENSE WHERE CashRegisterID = '" + posCashRegisterID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("POS Expense Codes", posCashRegisterID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertUpdatePOSCashRegisterPaymentMethods(POSCashRegisterData posCashRegisterData, bool isUpdate)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = posCashRegisterData.PaymentMethodTable.Rows[0]["CashRegisterID"].ToString();
				flag &= DeletePaymentMethods(text, sqlTransaction);
				if (posCashRegisterData.Tables["POS_CashRegister_PaymentMethod"].Rows.Count > 0)
				{
					SqlCommand insertUpdatePaymentMethodCommand = GetInsertUpdatePaymentMethodCommand(isUpdate: false);
					insertUpdatePaymentMethodCommand.Transaction = sqlTransaction;
					flag &= Insert(posCashRegisterData, "POS_CashRegister_PaymentMethod", insertUpdatePaymentMethodCommand);
				}
				if (isUpdate)
				{
					AddActivityLog("POS Payment Method", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("POS Payment Method", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("POS_CashRegister", "CashRegisterID", text, sqlTransaction, !isUpdate);
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

		public bool InsertUpdatePOSCashRegisterExpenseAccounts(POSCashRegisterData posCashRegisterData, bool isUpdate)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = posCashRegisterData.ExpenseAccountTable.Rows[0]["CashRegisterID"].ToString();
				flag &= DeleteExpenseAccounts(text, sqlTransaction);
				if (posCashRegisterData.Tables["POS_CashRegister_EXPENSE"].Rows.Count > 0)
				{
					SqlCommand insertUpdateExpenseCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
					insertUpdateExpenseCommand.Transaction = sqlTransaction;
					flag &= Insert(posCashRegisterData, "POS_CashRegister_EXPENSE", insertUpdateExpenseCommand);
				}
				if (isUpdate)
				{
					AddActivityLog("POS Expense Account", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("POS Expense Account", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("POS_CashRegister", "CashRegisterID", text, sqlTransaction, !isUpdate);
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

		public POSCashRegisterData GetPOSCashRegister()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("POS_CashRegister");
			POSCashRegisterData pOSCashRegisterData = new POSCashRegisterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pOSCashRegisterData, "POS_CashRegister", sqlBuilder);
			return pOSCashRegisterData;
		}

		public bool DeletePOSCashRegister(string posCashRegisterID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM POS_CashRegister WHERE CashRegisterID = '" + posCashRegisterID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("POS Payment Method", posCashRegisterID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public POSCashRegisterData GetPOSCashRegisterByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CashRegisterID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "POS_CashRegister";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			POSCashRegisterData pOSCashRegisterData = new POSCashRegisterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pOSCashRegisterData, "POS_CashRegister", sqlBuilder);
			return pOSCashRegisterData;
		}

		public DataSet GetPOSCashRegisterByFields(params string[] columns)
		{
			return GetPOSCashRegisterByFields(null, isInactive: true, columns);
		}

		public DataSet GetPOSCashRegisterByFields(string[] posCashRegisterID, params string[] columns)
		{
			return GetPOSCashRegisterByFields(posCashRegisterID, isInactive: true, columns);
		}

		public DataSet GetPOSCashRegisterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("POS_CashRegister");
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
				commandHelper.FieldName = "CashRegisterID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "POS_CashRegister";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "POS_CashRegister", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPaymentMethodsList(string cashRegisterID)
		{
			return GetPaymentMethodsList(cashRegisterID, showInactive: true);
		}

		public DataSet GetPaymentMethodsList(string cashRegisterID, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string str = " SELECT CashRegisterID,ISNULL(Inactive,'False') Inactive ,PCM.PaymentMethodID,DisplayName ,PCM.AccountID  ,ACC.AccountName  ,PM.MethodType\r\n                              FROM POS_CashRegister_PaymentMethod PCM\r\n                              INNER JOIN Account ACC ON PCM.AccountID=ACC.AccountID\r\n\t\t\t\t\t\t\t  INNER JOIN Payment_Method PM ON PM.PaymentMethodID = PCM.PaymentMethodID\r\n                                WHERE 1=1 ";
			if (cashRegisterID != "")
			{
				str = str + " AND CashRegisterID = '" + cashRegisterID + "' ";
			}
			if (!showInactive)
			{
				str += " AND ISNULL(Inactive,'False') = 'False'";
			}
			str += "ORDER BY RowIndex";
			FillDataSet(dataSet, "POS_CashRegister_PaymentMethod", str);
			return dataSet;
		}

		public DataSet GetExpenseAccountsList(string cashRegisterID)
		{
			DataSet dataSet = new DataSet();
			string str = " SELECT CashRegisterID,DisplayName ,PCM.AccountID  ,ACC.AccountName \r\n                              FROM POS_CashRegister_Expense PCM\r\n                              INNER JOIN Account ACC ON PCM.AccountID=ACC.AccountID\r\n                                WHERE 1=1 ";
			if (cashRegisterID != "")
			{
				str = str + " AND CashRegisterID = '" + cashRegisterID + "' ";
			}
			str += "ORDER BY RowIndex";
			FillDataSet(dataSet, "POS_CashRegister_EXPENSE", str);
			return dataSet;
		}

		public DataSet GetPOSCashRegisterList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CashRegisterID [CashRegister Code],CashRegisterName [CashRegister Name]\r\n                           FROM POS_CashRegister ";
			FillDataSet(dataSet, "POS_CashRegister", textCommand);
			return dataSet;
		}

		public DataSet GetPOSCashRegisterComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CashRegisterID [Code],CashRegisterName [Name]\r\n                           FROM POS_CashRegister ORDER BY CashRegisterID,CashRegisterName";
			FillDataSet(dataSet, "POS_CashRegister", textCommand);
			return dataSet;
		}

		public DataSet GetCashRegisterByComputerName(string computerName)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT *,CUS.CustomerName AS DefaultCustomerName\r\n                           FROM POS_CashRegister PCR LEFT OUTER JOIN Customer CUS ON Cus.CustomerID=PCR.DefaultCustomerID WHERE ComputerName='" + computerName + "'";
			FillDataSet(dataSet, "POS_CashRegister", textCommand);
			return dataSet;
		}

		public bool IsCashRegisterFree(string cashRegisterID)
		{
			try
			{
				string exp = "SELECT ISNULL(ComputerName,'') FROM POS_CashRegister WHERE CashRegisterID='" + cashRegisterID + "'";
				if (ExecuteScalar(exp).ToString() == "")
				{
					return true;
				}
				return false;
			}
			catch
			{
				throw;
			}
		}

		public string GetCurrentPOSLocation(string cashRegisterID)
		{
			try
			{
				string exp = "SELECT LocationID FROM POS_CashRegister WHERE CashRegisterID='" + cashRegisterID + "'";
				object obj = ExecuteScalar(exp);
				if (obj != null)
				{
					return obj.ToString();
				}
				return "";
			}
			catch
			{
				throw;
			}
		}

		public bool AssignCashRegister(string cashRegisterID, string computerName)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE POS_CashRegister SET ComputerName='" + computerName + "' WHERE CashRegisterID='" + cashRegisterID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool ChangeCashRegister(string cashRegisterID, string computerName)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE POS_CashRegister SET ComputerName='' WHERE CashREgisterId='" + cashRegisterID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public string GetCashRegisterAccountID(string cashRegisterID, string accountFieldIDName)
		{
			string exp = "SELECT " + accountFieldIDName + " FROM POS_CashRegister WHERE CashRegisterID='" + cashRegisterID + "'";
			object obj = ExecuteScalar(exp);
			if (obj == null)
			{
				return "";
			}
			return obj.ToString();
		}
	}
}
