using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ReceivedCheques : StoreObject
	{
		private const string RECEIVEDCHEQUE_TABLE = "Cheque_Received";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CHEQUENUMBER_PARM = "@ChequeNumber";

		private const string BANKID_PARM = "@BankID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string PAYEEACCOUNTID_PARM = "@PayeeAccountID";

		private const string CHEQUEDATE_PARM = "@ChequeDate";

		private const string RECEIPTDATE_PARM = "@ReceiptDate";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string EXCHANGERATE_PARM = "@ExchangeRate";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string CONAMOUNTFC_PARM = "@ConAmountFC";

		private const string CONRATE_PARM = "@ConRate";

		private const string NOTE_PARM = "@Note";

		private const string STATUS_PARM = "@Status";

		private const string REFERENCE_PARM = "@Reference";

		private const string PDCACCOUNTID_PARM = "@PDCAccountID";

		private const string DEPOSITDATE_PARM = "@DepositDate";

		private const string DEPOSITACCOUNTID_PARM = "@DepositAccountID";

		private const string DEPOSITBANKID_PARM = "@DepositBankID";

		private const string DEPOSITVOUCHERID_PARM = "@DepositVoucherID";

		private const string DEPOSITSYSDOCID_PARM = "@DepositSysDocID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CHECKNUMBER_PARM = "@CheckNumber";

		private const string CHEQUEBOOKID_PARM = "@ChequeBookID";

		private const string CHEQUEID_PARM = "@ChequeID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ReceivedCheques(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cheque_Received", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ChequeNumber", "@ChequeNumber", isUpdateConditionField: true), new FieldValue("BankID", "@BankID", isUpdateConditionField: true), new FieldValue("PayeeType", "@PayeeType", isUpdateConditionField: true), new FieldValue("PayeeID", "@PayeeID", isUpdateConditionField: true), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("ReceiptDate", "@ReceiptDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("ExchangeRate", "@ExchangeRate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("ConAmountFC", "@ConAmountFC"), new FieldValue("ConRate", "@ConRate"), new FieldValue("Note", "@Note"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("PDCAccountID", "@PDCAccountID"), new FieldValue("PayeeAccountID", "@PayeeAccountID"), new FieldValue("DepositDate", "@DepositDate"), new FieldValue("DepositAccountID", "@DepositAccountID"), new FieldValue("DepositBankID", "@DepositBankID"), new FieldValue("DepositSysDocID", "@DepositSysDocID"), new FieldValue("DepositVoucherID", "@DepositVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Cheque_Received", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ChequeNumber", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@PayeeAccountID", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@ReceiptDate", SqlDbType.DateTime);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@ExchangeRate", SqlDbType.SmallMoney);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@ConAmountFC", SqlDbType.Money);
			parameters.Add("@ConRate", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PDCAccountID", SqlDbType.NVarChar);
			parameters.Add("@DepositDate", SqlDbType.DateTime);
			parameters.Add("@DepositAccountID", SqlDbType.NVarChar);
			parameters.Add("@DepositBankID", SqlDbType.NVarChar);
			parameters.Add("@DepositVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DepositSysDocID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeAccountID"].SourceColumn = "PayeeAccountID";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@ReceiptDate"].SourceColumn = "ReceiptDate";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@ExchangeRate"].SourceColumn = "ExchangeRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@ConAmountFC"].SourceColumn = "ConAmountFC";
			parameters["@ConRate"].SourceColumn = "ConRate";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PDCAccountID"].SourceColumn = "PDCAccountID";
			parameters["@DepositDate"].SourceColumn = "DepositDate";
			parameters["@DepositAccountID"].SourceColumn = "DepositAccountID";
			parameters["@DepositBankID"].SourceColumn = "DepositBankID";
			parameters["@DepositSysDocID"].SourceColumn = "DepositSysDocID";
			parameters["@DepositVoucherID"].SourceColumn = "DepositVoucherID";
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

		private string GetInsertUpdateTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("GL_Transaction", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Reference", "@Reference"), new FieldValue("Description", "@Description"), new FieldValue("ChequebookID", "@ChequeBookID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("GL_Transaction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTransactionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTransactionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTransactionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Description"].SourceColumn = "Description";
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

		private string GetInsertUpdateTransactionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Transaction_Details", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("BankID", "@BankID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("CheckNumber", "@CheckNumber"), new FieldValue("ChequebookID", "@ChequeBookID"), new FieldValue("ChequeID", "@ChequeID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTransactionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTransactionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTransactionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters.Add("@CheckNumber", SqlDbType.NVarChar);
			parameters.Add("@ChequeBookID", SqlDbType.NVarChar);
			parameters.Add("@ChequeID", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@CheckNumber"].SourceColumn = "CheckNumber";
			parameters["@ChequeBookID"].SourceColumn = "ChequebookID";
			parameters["@ChequeID"].SourceColumn = "ChequeID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateReceivedCheque(ReceivedChequeData receivedChequeData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			try
			{
				string sysDocID = receivedChequeData.ReceivedChequeTable.Rows[0]["SysDocID"].ToString();
				string voucherID = receivedChequeData.ReceivedChequeTable.Rows[0]["VoucherID"].ToString();
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteChequeRows(sysDocID, voucherID, sqlTransaction);
				}
				_ = receivedChequeData.Tables["Cheque_Received"].Rows[0];
				string baseCurrencyID = new APJournal(base.DBConfig).GetBaseCurrencyID();
				string commaSeperatedIDs = GetCommaSeperatedIDs(receivedChequeData, "Cheque_Received", "PayeeID");
				string textCommand = "SELECT CustomerID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Customer WHERE CustomerID IN (" + commaSeperatedIDs + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Customer", textCommand);
				DataSet dataSet2 = new DataSet();
				textCommand = "SELECT AccountID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Account WHERE AccountID IN (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet2, "Account", textCommand);
				DataSet dataSet3 = new DataSet();
				textCommand = "SELECT VendorID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Vendor WHERE VendorID IN (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet3, "Vendor", textCommand);
				foreach (DataRow row in receivedChequeData.Tables["Cheque_Received"].Rows)
				{
					string a = row["CurrencyID"].ToString();
					string str = row["PayeeID"].ToString();
					string text = "AED";
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						text = dataSet.Tables[0].Select("CustomerID = '" + str + "'")[0]["CurrencyID"].ToString();
					}
					if (dataSet2.Tables[0].Rows.Count > 0)
					{
						text = dataSet2.Tables[0].Select("AccountID = '" + str + "'")[0]["CurrencyID"].ToString();
					}
					if (dataSet3.Tables[0].Rows.Count > 0)
					{
						text = dataSet3.Tables[0].Select("VendorID = '" + str + "'")[0]["CurrencyID"].ToString();
					}
					if (text != baseCurrencyID && a != text)
					{
						string text2 = CommonLib.ToSqlDateTimeString(DateTime.Parse(row["ReceiptDate"].ToString()));
						decimal d = default(decimal);
						decimal num = 1m;
						if (receivedChequeData.Tables["Cheque_Received"].Columns.Contains("Debit") && row["Debit"] != DBNull.Value)
						{
							d = decimal.Parse(row["Debit"].ToString());
						}
						if (receivedChequeData.Tables["Cheque_Received"].Columns.Contains("Credit") && row["Credit"] != DBNull.Value)
						{
							decimal.Parse(row["Credit"].ToString());
						}
						textCommand = " SELECT DISTINCT ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = '" + text + "' AND Ex.RateUpdatedDate < '" + text2 + "'),\r\n                                     (SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = '" + text + "'))  AS CurRate FROM Currency";
						object obj = ExecuteScalar(textCommand, sqlTransaction);
						if (!obj.IsNullOrEmpty())
						{
							num = decimal.Parse(obj.ToString());
						}
						string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(text);
						row["ConRate"] = num;
						if (d != 0m)
						{
							if (currencyRateType == "M")
							{
								row["ConAmountFC"] = Math.Round(d / num, 4);
							}
							else
							{
								row["ConAmountFC"] = Math.Round(d * num, 4);
							}
						}
					}
				}
				if (receivedChequeData.Tables["Cheque_Received"].Rows.Count > 0)
				{
					flag &= Insert(receivedChequeData, "Cheque_Received", insertUpdateCommand);
				}
				decimal result = default(decimal);
				if (receivedChequeData.Tables["Cheque_Received"].Rows.Count <= 0)
				{
					return flag;
				}
				string a2 = receivedChequeData.Tables["Cheque_Received"].Rows[0]["PayeeType"].ToString();
				string text3 = receivedChequeData.Tables["Cheque_Received"].Rows[0]["PayeeID"].ToString();
				if (a2 == "C")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "PDCAmount", "CustomerID", text3, sqlTransaction).ToString(), out result);
					foreach (DataRow row2 in receivedChequeData.Tables["Cheque_Received"].Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row2["Amount"].ToString(), out result2);
						result += result2;
					}
					textCommand = "UPDATE Customer SET PDCAmount=" + result.ToString() + " WHERE CustomerID='" + text3 + "'";
					return flag & Update(textCommand, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool SendChequesToBank(ReceivedChequeData receivedChequeData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			try
			{
				string sysDocID = receivedChequeData.ReceivedChequeTable.Rows[0]["SysDocID"].ToString();
				string voucherID = receivedChequeData.ReceivedChequeTable.Rows[0]["VoucherID"].ToString();
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteChequeRows(sysDocID, voucherID, sqlTransaction);
				}
				if (receivedChequeData.Tables["Cheque_Received"].Rows.Count > 0)
				{
					flag &= Insert(receivedChequeData, "Cheque_Received", insertUpdateCommand);
				}
				decimal result = default(decimal);
				if (receivedChequeData.Tables["Cheque_Received"].Rows.Count <= 0)
				{
					return flag;
				}
				string a = receivedChequeData.Tables["Cheque_Received"].Rows[0]["PayeeType"].ToString();
				string text = receivedChequeData.Tables["Cheque_Received"].Rows[0]["PayeeID"].ToString();
				if (a == "C")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "PDCAmount", "CustomerID", text, sqlTransaction).ToString(), out result);
					foreach (DataRow row in receivedChequeData.Tables["Cheque_Received"].Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row["Amount"].ToString(), out result2);
						result += result2;
					}
					string commandText = "UPDATE Customer SET PDCAmount=" + result.ToString() + " WHERE CustomerID='" + text + "'";
					return flag & Update(commandText, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public ReceivedChequeData GetReceivedCheque()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Cheque_Received");
			ReceivedChequeData receivedChequeData = new ReceivedChequeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(receivedChequeData, "Cheque_Received", sqlBuilder);
			return receivedChequeData;
		}

		public bool DeleteChequeRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string a = "";
				string text = "";
				string exp = "SELECT TOP 1 PayeeType FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					a = obj.ToString();
				}
				exp = "SELECT TOP 1 PayeeID FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (a == "C" && text != "")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "PDCAmount", "CustomerID", text, sqlTransaction).ToString(), out result);
					exp = "SELECT SUM(ISNULL(Amount,0)) FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					decimal.TryParse(ExecuteScalar(exp, sqlTransaction).ToString(), out result2);
					if (result2 != 0m)
					{
						result -= result2;
						exp = "UPDATE Customer SET PDCAmount=" + result.ToString() + " WHERE CustomerID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
				}
				string commandText = "DELETE FROM Cheque_Received WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteReceivedCheque(string receivedChequeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Cheque_Received WHERE ChequeNumber = '" + receivedChequeID + "'";
				return Delete(commandText, null);
			}
			catch
			{
				throw;
			}
		}

		public ReceivedChequeData GetReceivedChequeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ChequeNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Cheque_Received";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ReceivedChequeData receivedChequeData = new ReceivedChequeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(receivedChequeData, "Cheque_Received", sqlBuilder);
			return receivedChequeData;
		}

		public DataSet GetReceivedChequeByFields(params string[] columns)
		{
			return GetReceivedChequeByFields(null, isInactive: true, columns);
		}

		public DataSet GetReceivedChequeByFields(string[] receivedChequeID, params string[] columns)
		{
			return GetReceivedChequeByFields(receivedChequeID, isInactive: true, columns);
		}

		public DataSet GetReceivedChequeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Cheque_Received");
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
				commandHelper.FieldName = "ChequeNumber";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Cheque_Received";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Cheque_Received", sqlBuilder);
			return dataSet;
		}

		public DataSet GetReceivedChequeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ReceivedChequeID [ReceivedCheque Code],ReceivedChequeName [ReceivedCheque Name],Note,Inactive  \r\n                           FROM ReceivedCheque ";
			FillDataSet(dataSet, "Cheque_Received", textCommand);
			return dataSet;
		}

		public DataSet GetReceivedChequeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber [Code],Cheque_Received.BankID,Cheque_Received.Status\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Name]\r\n                           FROM Cheque_Received LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID";
			FillDataSet(dataSet, "Cheque_Received", textCommand);
			return dataSet;
		}

		public DataSet GetChequesToClearList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT 'False' AS C,ChequeDate AS [Chq Date], ChequeID,ChequeNumber [Cheque #],Bank.BankName AS [Deposit Bank]\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee],ISNULL(AmountFC,Amount) AS [Amount]\r\n                            FROM Cheque_Received LEFT OUTER JOIN Bank ON Bank.BankID=Cheque_Received.DepositBankID LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            WHERE Cheque_Received.Status IN (2)";
			FillDataSet(dataSet, "Cheque_Received", textCommand);
			return dataSet;
		}

		public DataSet GetReceivedChequeToCancelList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber [Cheque #],Bank.BankName AS [Bank]\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee],ISNULL(AmountFC,Amount) AS [Amount]\r\n                            FROM Cheque_Received LEFT OUTER JOIN Bank ON Bank.BankID=Cheque_Received.BankID LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            WHERE Cheque_Received.Status IN (1,8)";
			FillDataSet(dataSet, "Cheque_Received", textCommand);
			return dataSet;
		}

		public DataSet GetReceivedChequeToReturnList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber [Cheque #],Bank.BankName AS [Bank]\r\n                            ,PayeeID [Party Code],CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Party Name],ISNULL(AmountFC,Amount) AS [Amount]\r\n                            FROM Cheque_Received LEFT OUTER JOIN Bank ON Bank.BankID=Cheque_Received.BankID LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            WHERE Cheque_Received.Status IN (2,4)";
			FillDataSet(dataSet, "Cheque_Received", textCommand);
			return dataSet;
		}

		public DataSet GetChangeChequeStatusList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber [Cheque #],Bank.BankName AS [Bank]\r\n                            ,PayeeID [Party Code],CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Party Name],ISNULL(AmountFC,Amount) AS [Amount]\r\n                            FROM Cheque_Received LEFT OUTER JOIN Bank ON Bank.BankID=Cheque_Received.BankID LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            WHERE Cheque_Received.Status IN (4)";
			FillDataSet(dataSet, "Cheque_Received", textCommand);
			return dataSet;
		}

		public DataSet GetChequeByID(string chequeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber ,Bank.BankName,SysDocID,VoucherID,Cheque_Received.Status,Cheque_Received.CurrencyID,PayeeAccountID,DiscountAmount,\r\n                            Cheque_Received.ExchangeRate,AmountFC,DepositAccountID,Cheque_Received.PDCAccountID, DiscountAccountID,DiscountVoucherID,\r\n                            PayeeType,PayeeID,ChequeDate,Amount,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name]\r\n                           FROM Cheque_Received LEFT OUTER JOIN Bank ON Bank.BankID= Cheque_Received.BankID LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            WHERE ChequeID=" + chequeID;
			FillDataSet(dataSet, "Cheque_Received", textCommand);
			return dataSet;
		}

		public DataSet GetChequesToDepositList(DateTime chequeDate)
		{
			return GetChequesToDepositList(chequeDate, "");
		}

		public DataSet GetChequesToDepositList(DateTime chequeDate, string bankAccountID)
		{
			DataSet dataSet = new DataSet();
			string str = CommonLib.ToSqlDateTimeString(chequeDate);
			string str2 = "SELECT     ChequeID,SysDocID,VoucherID,ChequeDate AS [Chq Date],ChequeNumber [Chq #],SendDate,Cheque_Received.BankID,Bank.BankName [Bank Name],PayeeAccountID,Reference,\r\n                            PayeeType,PayeeID,Cheque_Received.Status,Cheque_Received.PDCAccountID,DiscountBankAccountID,Cheque_Received.Note as Description,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n                            ISNULL(Cheque_Received.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,Amount AS Amount\r\n                            FROM         Cheque_Received LEFT OUTER JOIN Bank ON Cheque_Received.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=Cheque_Received.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=Cheque_Received.PayeeID\r\n                            WHERE Cheque_Received.Status IN (1, 3, 4, 8) AND ISNULL(IsVoid,'False')<>'True' AND ChequeDate<='" + str + "'";
			if (bankAccountID != "")
			{
				str2 = str2 + " AND SendBankAccountID = '" + bankAccountID + "' AND  Cheque_Received.Status <> 8 ";
			}
			str2 += " ORDER BY ChequeDate";
			FillDataSet(dataSet, "Cheque_Received", str2);
			return dataSet;
		}

		public DataSet GetChequesToDepositList(DateTime chequeDate, string bankAccountID, string locationID)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(chequeDate);
			string str = "SELECT     ChequeID,Cheque_Received.SysDocID,VoucherID,ChequeDate AS [Chq Date],ChequeNumber [Chq #],SendDate,Cheque_Received.BankID,Bank.BankName [Bank Name],PayeeAccountID,Reference,\r\n                            PayeeType,PayeeID,Cheque_Received.Status,Cheque_Received.PDCAccountID,DiscountBankAccountID,Cheque_Received.Note as Description,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n                            ISNULL(Cheque_Received.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,Amount AS Amount\r\n                            FROM         Cheque_Received LEFT OUTER JOIN Bank ON Cheque_Received.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=Cheque_Received.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=Cheque_Received.PayeeID\r\n                            INNER JOIN System_Document SD ON SD.SysDocID=Cheque_Received.SysDocID   \r\n                            WHERE Cheque_Received.Status IN (1, 3, 4, 8) AND ISNULL(IsVoid,'False')<>'True' AND ChequeDate<='" + text + "' AND  SD.LocationID ='" + locationID + "'";
			if (bankAccountID != "")
			{
				str = str + " AND SendBankAccountID = '" + bankAccountID + "' AND  Cheque_Received.Status <> 8 ";
			}
			str += " ORDER BY ChequeDate";
			FillDataSet(dataSet, "Cheque_Received", str);
			return dataSet;
		}

		public DataSet GetChequesToSendToBankList(DateTime chequeDate)
		{
			return GetChequesToSendToBankList(chequeDate, "");
		}

		public DataSet GetChequesToSendToBankList(DateTime chequeDate, string bankAccountID)
		{
			DataSet dataSet = new DataSet();
			string str = CommonLib.ToSqlDateTimeString(chequeDate);
			string str2 = "SELECT     ChequeID,SysDocID,VoucherID,ChequeDate AS [Chq Date],Cheque_Received.ReceiptDate,ChequeNumber [Chq #],Cheque_Received.BankID,Bank.BankName [Bank Name],PayeeAccountID,\r\n                            PayeeType,PayeeID,Cheque_Received.Status,Cheque_Received.PDCAccountID,DiscountBankAccountID,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n                            ISNULL(Cheque_Received.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(AmountFC,Amount) AS Amount\r\n                            FROM         Cheque_Received LEFT OUTER JOIN Bank ON Cheque_Received.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=Cheque_Received.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=Cheque_Received.PayeeID\r\n                            WHERE Cheque_Received.Status IN (1, 8) AND ISNULL(IsVoid,'False')<>'True' AND ChequeDate<='" + str + "'";
			if (bankAccountID != "")
			{
				str2 = str2 + " AND SendBankAccountID = '" + bankAccountID + "' ";
			}
			str2 += " ORDER BY ChequeDate";
			FillDataSet(dataSet, "Cheque_Received", str2);
			return dataSet;
		}

		public DataSet GetForSettleChequesToSendToBankList(DateTime chequeDate, string[] chequeID)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(chequeDate);
			string text2 = "";
			for (int i = 0; i < chequeID.Length; i++)
			{
				text2 = "'" + chequeID[i] + "'";
				if (i < chequeID.Length - 1)
				{
					text2 += ",";
				}
			}
			string str = "SELECT     ChequeID,SysDocID,VoucherID,ChequeDate AS [Chq Date],Cheque_Received.ReceiptDate,ChequeNumber [Chq #],Cheque_Received.BankID,Bank.BankName [Bank Name],PayeeAccountID,\r\n                            PayeeType,PayeeID,Cheque_Received.Status,Cheque_Received.PDCAccountID,DiscountBankAccountID,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n                            ISNULL(Cheque_Received.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(AmountFC,Amount) AS Amount\r\n                            FROM         Cheque_Received LEFT OUTER JOIN Bank ON Cheque_Received.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=Cheque_Received.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=Cheque_Received.PayeeID\r\n                            WHERE Cheque_Received.Status IN (1, 8) AND ISNULL(IsVoid,'False')<>'True' AND ChequeDate<='" + text + "' AND VoucherID IN (" + text2 + ")";
			str += " ORDER BY ChequeDate";
			FillDataSet(dataSet, "Cheque_Received", str);
			return dataSet;
		}

		public DataSet GetChequesToDiscountList(DateTime chequeDate)
		{
			return GetChequesToDiscountList(chequeDate, "");
		}

		public DataSet GetChequesToDiscountList(DateTime chequeDate, string bankAccountID)
		{
			DataSet dataSet = new DataSet();
			string text = "1,4,8";
			if (bankAccountID != "")
			{
				text = "4";
			}
			string text2 = CommonLib.ToSqlDateTimeString(chequeDate);
			string str = "SELECT DISTINCT CR.ChequeID,CR.SysDocID,CR.VoucherID,ChequeDate AS [Chq Date],ChequeNumber [Chq #],CR.BankID,Bank.BankName [Bank Name],PayeeAccountID,\r\n                            PayeeType,PayeeID,CR.PDCAccountID,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,SendDate, CSD.VoucherID AS [SendVoucherID],\r\n                            ISNULL(CR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(AmountFC,Amount) AS Amount\r\n                            FROM         Cheque_Received CR LEFT OUTER JOIN Bank ON CR.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=CR.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=CR.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=CR.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=CR.PayeeID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Cheque_Send_Detail CSD ON CSD.ChequeID = CR.ChequeID \r\n                            WHERE CR.Status IN (" + text + ") AND ISNULL(IsVoid,'False')<>'True'  AND CSD.Status IS NULL  AND ChequeDate<='" + text2 + "'";
			if (bankAccountID != "")
			{
				str = str + " AND SendBankAccountID = '" + bankAccountID + "' ";
			}
			str += " ORDER BY ChequeDate";
			FillDataSet(dataSet, "Cheque_Received", str);
			return dataSet;
		}

		public bool VoidChequeTransaction(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string a = "";
				string text = "";
				string exp = "SELECT TOP 1 PayeeType FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					a = obj.ToString();
				}
				exp = "SELECT TOP 1 PayeeID FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (a == "C" && text != "")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "PDCAmount", "CustomerID", text, sqlTransaction).ToString(), out result);
					exp = "SELECT SUM(ISNULL(Amount,0)) FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					decimal.TryParse(ExecuteScalar(exp, sqlTransaction).ToString(), out result2);
					if (result2 != 0m)
					{
						if (isVoid)
						{
							result -= result2;
						}
						else
						{
							result += result2;
						}
						exp = "UPDATE Customer SET PDCAmount=" + result.ToString() + " WHERE CustomerID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
				}
				exp = "UPDATE Cheque_Received SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
			}
			catch
			{
				throw;
			}
		}

		public bool TransactionHasProcessedCheques(string sysDocID, string VoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(*) FROM Cheque_Received WHERE \r\n                                SysDocID='" + sysDocID + "' AND VoucherID = '" + VoucherID + "' AND Status <>1";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || obj.ToString() == "")
				{
					return false;
				}
				return int.Parse(obj.ToString()) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateChequeStatus(string sysDocID, string VoucherID, ReceivedChequeStatus status, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string text = "UPDATE Cheque_Received SET Status = " + (byte)status;
				if (status == ReceivedChequeStatus.Undeposited)
				{
					text += ",DepositSysDocID = NULL, DepositVoucherID = NULL ";
				}
				text = text + " WHERE ChequeID IN (Select TRD.ChequeID FROM Transaction_Details TRD INNER JOIN GL_Transaction TR ON TRD.SysDocID=TR.SysDocID AND TRD.VoucherID=TR.VoucherID WHERE TRD.SysDocID='" + sysDocID + "' AND TRD.VoucherID = '" + VoucherID + "')";
				return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateChequeStatus(string[] chequeIDs, ReceivedChequeStatus status)
		{
			bool flag = true;
			try
			{
				string text = "";
				string text2 = "";
				for (int i = 0; i < chequeIDs.Length; i++)
				{
					text2 = text2 + "'" + chequeIDs[i] + "'";
					if (i < chequeIDs.Length - 1)
					{
						text2 += ",";
					}
				}
				if (status == ReceivedChequeStatus.Cleared)
				{
					text = "SELECT COUNT(ChequeID) FROM Cheque_Received WHERE ChequeID IN (" + text2 + ") AND Status <> " + 2;
					object obj = ExecuteScalar(text);
					if (obj != null && int.Parse(obj.ToString()) > 0)
					{
						throw new CompanyException("Only deposited cheques can be marked as cleared.", 1006);
					}
				}
				text = "UPDATE Cheque_Received SET Status = " + (byte)status + " WHERE ChequeID IN (" + text2 + ")";
				return flag & (ExecuteNonQuery(text) >= 0);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],ChequeNumber [Chq#],PayeeType [P],PayeeID [Payee Code],\r\n                                                (CASE PayeeType\r\n                                                    WHEN 'C' THEN Customer.CustomerName\r\n                                                    WHEN 'V' THEN Vendor.VendorName\r\n                                                    WHEN 'E' THEN Employee.FirstName\r\n                                                    ELSE Account.AccountName END) AS [Payee Name],\r\n                                                    ChequeDate [Chq Date],ReceiptDate [Receive Date],\r\n                                                    CHQ.Note,CHQ.Status,\r\n                                                    Chq.DepositSysDocID [Deposit DocID],CHQ.DepositVoucherID [Deposit Voucher No],\r\n                                                    DepositDate [Deposit Date],DepositAccountID [Deposit Account Code], DepACC.AccountName [Deposit Account Name],\r\n                                                    Reference,\r\n                                                    CHQ.BankID [Bank],ISNULL(CHQ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur],\r\n                                                    ISNULL(AmountFC,Amount) AS Amount\r\n                                                    FROM Cheque_Received CHQ\r\n                                                    LEFT JOIN Account DepACC ON DepACC.AccountID = CHQ.DepositAccountID\r\n                                                    LEFt OUTER JOIN Account ON CHQ.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                                                    Customer ON CHQ.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                                    Vendor ON CHQ.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                                                    Employee ON CHQ.PayeeID=Employee.EmployeeID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE ChequeDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Quote", sqlCommand);
			return dataSet;
		}

		public DataSet GetReceivedChequeAsOfDate(DateTime chkDatefrom, DateTime chkDateto, DateTime from, DateTime to, string fromBank, string toBank, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationID, string toLocationID, bool cleared, bool bounced, bool cancelled, bool discounted, bool senttobank, string strGroupBy, string customerIDs)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = CommonLib.ToSqlDateTimeString(chkDatefrom);
			string text4 = CommonLib.ToSqlDateTimeString(chkDateto);
			string text5 = 1.ToString() + "," + 3.ToString() + ",";
			if (cleared)
			{
				text5 = text5 + 7.ToString() + "," + 2.ToString() + ",";
			}
			if (bounced)
			{
				text5 = text5 + 8.ToString() + ",";
			}
			if (cancelled)
			{
				text5 = text5 + 9.ToString() + ",";
			}
			if (discounted)
			{
				text5 = text5 + 3.ToString() + ",";
			}
			if (senttobank)
			{
				text5 = text5 + 4.ToString() + ",";
			}
			text5 = text5.Remove(text5.Length - 1, 1);
			string text6 = "SELECT Cheque_Received.SysDocID [Sys Doc],  Cheque_Received.VoucherID [Doc Number], Bank.BankName [Bank Name], ChequeNumber [Cheque No], PayeeType [Payee Type], Amount, \r\n                            ChequeDate [Cheque Date],ReceiptDate AS [Receipt Date], DepositDate,\r\n\t\t\t\t\t\t\tCASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee], Cheque_Received.Note,case Cheque_Received.Status WHEN 1 THEN 'UNDEPOSITED' WHEN 2 THEN 'DEPOSITED' WHEN 3 THEN 'DISCOUNTED' WHEN 4 THEN 'SENT TO BANK' WHEN 7 THEN 'CLEARED' WHEN 8 THEN 'Bounced' WHEN 9 THEN 'CANCELLED' end AS STATUS\r\n                            FROM  Cheque_Received LEFT OUTER JOIN Bank ON Cheque_Received.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=Cheque_Received.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=Cheque_Received.PayeeID";
			if (fromLocationID != "")
			{
				text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=Cheque_Received.SysDocID";
			}
			text6 = text6 + " WHERE ISNULL(ISVOID,'False') = 'False' AND Cheque_Received.Status IN (" + text5 + ") \r\n                            AND ReceiptDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text6 = text6 + " AND ChequeDate BETWEEN '" + text3 + "' AND '" + text4 + "'";
			if (fromBank != "")
			{
				text6 = text6 + " AND Cheque_Received.BankID BETWEEN '" + fromBank + "' AND '" + toBank + "' ";
			}
			if (customerIDs != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromClass != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromArea != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryId BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromLocationID != "")
			{
				text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			text6 += " ORDER BY ChequeDate";
			SqlCommand sqlCommand = new SqlCommand(text6);
			FillDataSet(dataSet, "Cheque_Received", sqlCommand);
			text6 = "SELECT DISTINCT ";
			if (strGroupBy == "4")
			{
				text6 += " ReceiptDate AS [GROUP]";
			}
			if (strGroupBy == "3")
			{
				text6 += "Bank.BankName AS [GROUP]";
			}
			else if (strGroupBy == "2")
			{
				text6 += "ChequeDate  AS [GROUP]";
			}
			else if (strGroupBy == "1")
			{
				text6 += "CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'A' THEN Account.AccountName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [GROUP]";
			}
			text6 += "   FROM   Cheque_Received LEFT OUTER JOIN Bank ON Cheque_Received.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=Cheque_Received.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=Cheque_Received.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=Cheque_Received.PayeeID";
			if (fromLocationID != "")
			{
				text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=Cheque_Received.SysDocID";
			}
			text6 = text6 + " WHERE ISNULL(ISVOID,'False') = 'False' AND Cheque_Received.Status IN (" + text5 + ") \r\n                            AND ReceiptDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text6 = text6 + " AND ChequeDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
			if (fromBank != "")
			{
				text6 = text6 + " AND Cheque_Received.BankID BETWEEN '" + fromBank + "' AND '" + toBank + "' ";
			}
			if (customerIDs != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromClass != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromArea != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryId BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			if (fromLocationID != "")
			{
				text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			sqlCommand = new SqlCommand(text6);
			FillDataSet(dataSet, "Temp", sqlCommand);
			if (dataSet.Tables["Cheque_Received"].Rows.Count > 0)
			{
				if (strGroupBy == "1")
				{
					dataSet.Relations.Add("ReceivedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Received"].Columns["Payee"]
					}, createConstraints: false);
				}
				else if (strGroupBy == "2")
				{
					dataSet.Relations.Add("ReceivedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Received"].Columns["Cheque Date"]
					}, createConstraints: false);
				}
				else if (strGroupBy == "3")
				{
					dataSet.Relations.Add("ReceivedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Received"].Columns["Bank Name"]
					}, createConstraints: false);
				}
				else if (strGroupBy == "4")
				{
					dataSet.Relations.Add("ReceivedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Received"].Columns["Receipt Date"]
					}, createConstraints: false);
				}
			}
			return dataSet;
		}

		public DataSet GetChequeMaturityReport(DateTime from, DateTime to, DateTime chquedatefrom, DateTime chquedateto, string fromAccountID, string toAccountID, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool cleared, bool bounced, bool cancelled, string customerIDs)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = CommonLib.ToSqlDateTimeString(chquedatefrom);
			string text4 = CommonLib.ToSqlDateTimeString(chquedateto);
			string text5 = 2.ToString() + ",";
			if (cleared)
			{
				text5 = text5 + 4.ToString() + ",";
			}
			if (bounced)
			{
				text5 = text5 + 5.ToString() + ",";
			}
			if (cancelled)
			{
				text5 = text5 + 9.ToString() + ",";
			}
			text5 = text5.Remove(text5.Length - 1, 1);
			string text6 = "SELECT ChequeDate,PayeeID,ChequeNumber,Cheque_Received.DepositDate,Cheque_Received.BankID,BANK.BankName,DepositAccountID,Ac.AccountName,Ac.Alias,Amount,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee] FROM Cheque_Received\r\n                            INNER JOIN BANK ON Bank.BankID=Cheque_Received.BankID \r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Account AC ON AC.AccountID = DepositAccountID\r\n                            WHERE Cheque_Received.Status=2  AND Cheque_Received.DepositDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text6 = text6 + " AND ChequeDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
			if (fromAccountID != "")
			{
				text6 = text6 + " AND DepositAccountID BETWEEN '" + fromAccountID + "' AND '" + toAccountID + "' ";
			}
			if (customerIDs != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN(" + customerIDs + ")";
			}
			if (fromCustomer != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromClass != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromArea != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE AreaId BETWEEN '" + fromArea + "' AND '" + toArea + "') ";
			}
			if (fromCountry != "")
			{
				text6 = text6 + " AND PayeeType = 'C' AND Cheque_Received.PayeeID IN (SELECT CustomerID FROM Customer WHERE CountryId BETWEEN '" + fromCountry + "' AND '" + toCountry + "') ";
			}
			SqlCommand sqlCommand = new SqlCommand(text6);
			FillDataSet(dataSet, "Cheque_Received", sqlCommand);
			return dataSet;
		}
	}
}
