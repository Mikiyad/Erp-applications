using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class IssuedCheques : StoreObject
	{
		private const string ISSUEDCHEQUE_TABLE = "Cheque_Issued";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CHEQUENUMBER_PARM = "@ChequeNumber";

		private const string CHEQUEBOOKID_PARM = "@ChequebookID";

		private const string BANKID_PARM = "@BankID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string PAYEEACCOUNTID_PARM = "@PayeeAccountID";

		private const string CHEQUEBOOKID_FIELD = "@ChequebookID";

		private const string CHEQUEDATE_PARM = "@ChequeDate";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string EXCHANGERATE_PARM = "@ExchangeRate";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string NOTE_PARM = "@Note";

		private const string STATUS_PARM = "@Status";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string CLEARANCEDATE_PARM = "@ClearanceDate";

		private const string CLEARANCEACCOUNTID_PARM = "@ClearanceAccountID";

		private const string CLEARANCEVOUCHERID_PARM = "@ClearanceVoucherID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string SECURITYCHEQUETABLE_PARM = "@Security_Cheque";

		private const string ISVOID_PARM = "@IsVoid";

		private const string VOIDDATE_PARM = "@VoidDate";

		private const string TRANSACTIONID_PARM = "@TransactionID";

		public IssuedCheques(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cheque_Issued", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ChequeNumber", "@ChequeNumber", isUpdateConditionField: true), new FieldValue("BankID", "@BankID", isUpdateConditionField: true), new FieldValue("PayeeType", "@PayeeType", isUpdateConditionField: true), new FieldValue("PayeeID", "@PayeeID", isUpdateConditionField: true), new FieldValue("PayeeAccountID", "@PayeeAccountID"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("ExchangeRate", "@ExchangeRate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Note", "@Note"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("PDCAccountID", "@AccountID"), new FieldValue("ClearanceDate", "@ClearanceDate"), new FieldValue("ClearanceAccountID", "@ClearanceAccountID"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("ClearanceVoucherID", "@ClearanceVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Cheque_Issued", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@IssueDate", SqlDbType.DateTime);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@ExchangeRate", SqlDbType.SmallMoney);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@ClearanceDate", SqlDbType.DateTime);
			parameters.Add("@ClearanceAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@ClearanceVoucherID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeAccountID"].SourceColumn = "PayeeAccountID";
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@ExchangeRate"].SourceColumn = "ExchangeRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@AccountID"].SourceColumn = "PDCAccountID";
			parameters["@ClearanceDate"].SourceColumn = "ClearanceDate";
			parameters["@ClearanceAccountID"].SourceColumn = "ClearanceAccountID";
			parameters["@BankAccountID"].SourceColumn = "BankAccountID";
			parameters["@ClearanceVoucherID"].SourceColumn = "ClearanceVoucherID";
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

		private string GetInsertUpdateSecurityChequeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Security_Cheque", new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("ChequeNumber", "@ChequeNumber"), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("Amount", "@Amount"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSecurityChequeCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSecurityChequeText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSecurityChequeText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@ChequeNumber", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@IssueDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateIssuedCheque(IssuedChequeData issuedChequeData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text = issuedChequeData.IssuedChequeTable.Rows[0]["SysDocID"].ToString();
				string text2 = issuedChequeData.IssuedChequeTable.Rows[0]["VoucherID"].ToString();
				insertUpdateCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteChequeRows(text, text2, sqlTransaction);
				}
				foreach (DataRow row in issuedChequeData.IssuedChequeTable.Rows)
				{
					string text3 = row["ChequebookID"].ToString();
					string value = new Databases(base.DBConfig).GetFieldValue("Chequebook", "AccountID", "ChequebookID", text3, sqlTransaction).ToString();
					string text4 = row["ChequeNumber"].ToString();
					row["BankAccountID"] = value;
					if (ValidateBlankCheque(text3, text4, "", "") != 0)
					{
						throw new CompanyException("Cheque number:" + text4 + " is already in use.", 2006);
					}
				}
				if (issuedChequeData.Tables["Cheque_Issued"].Rows.Count > 0)
				{
					flag &= Insert(issuedChequeData, "Cheque_Issued", insertUpdateCommand);
				}
				if (flag)
				{
					foreach (DataRow row2 in issuedChequeData.IssuedChequeTable.Rows)
					{
						string text5 = row2["ChequeNumber"].ToString();
						string text6 = row2["ChequebookID"].ToString();
						object obj3 = ExecuteScalar("SELECT MAX(ChequeID) FROM Cheque_Issued WHERE VoucherID='" + text2 + "' AND SysDocID='" + text + "' AND ChequeNumber='" + text5 + "'", sqlTransaction);
						if (obj3 != null && obj3.ToString() != "")
						{
							flag &= (ExecuteNonQuery("UPDATE Cheque_Register SET ChequeID=" + obj3.ToString() + ", Status = 2 WHERE ChequeNumber='" + text5 + "' AND ChequebookID='" + text6 + "'", sqlTransaction) > 0);
						}
					}
				}
				decimal result = default(decimal);
				if (issuedChequeData.Tables["Cheque_Issued"].Rows.Count <= 0)
				{
					return flag;
				}
				string a = issuedChequeData.Tables["Cheque_Issued"].Rows[0]["PayeeType"].ToString();
				string text7 = issuedChequeData.Tables["Cheque_Issued"].Rows[0]["PayeeID"].ToString();
				if (a == "V")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Vendor", "PDCAmount", "VendorID", text7, sqlTransaction).ToString(), out result);
					foreach (DataRow row3 in issuedChequeData.Tables["Cheque_Issued"].Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row3["Amount"].ToString(), out result2);
						result += result2;
					}
					string commandText = "UPDATE Vendor SET PDCAmount=" + result.ToString() + " WHERE VendorID='" + text7 + "'";
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

		public bool InsertUpdateSecurityCheque(DataSet chequeData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSecurityChequeCommand = GetInsertUpdateSecurityChequeCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text = chequeData.Tables[0].Rows[0]["ChequebookID"].ToString();
				string text2 = chequeData.Tables[0].Rows[0]["ChequeNumber"].ToString();
				string voucherID = chequeData.Tables[0].Rows[0]["VoucherID"].ToString();
				string sysDocID = chequeData.Tables[0].Rows[0]["SysDocID"].ToString();
				insertUpdateSecurityChequeCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(chequeData, chequeData.Tables[0].TableName, insertUpdateSecurityChequeCommand)) : (flag & Insert(chequeData, chequeData.Tables[0].TableName, insertUpdateSecurityChequeCommand)));
				if (flag)
				{
					flag &= (ExecuteNonQuery("UPDATE Cheque_Register SET IsSecurityCheque='True', Status = 2 WHERE ChequeNumber=" + text2 + " AND ChequebookID='" + text + "'", sqlTransaction) > 0);
				}
				string entityName = "Security Cheque";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, chequeData.Tables[0].Rows[0]["VoucherID"].ToString(), chequeData.Tables[0].Rows[0]["SysDocID"].ToString(), ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, chequeData.Tables[0].Rows[0]["VoucherID"].ToString(), chequeData.Tables[0].Rows[0]["SysDocID"].ToString(), ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(chequeData.Tables[0].Rows[0]["SysDocID"].ToString(), chequeData.Tables[0].Rows[0]["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.IssuedSecurityCheque, sysDocID, voucherID, "Security_Cheque", sqlTransaction);
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

		public IssuedChequeData GetIssuedCheque()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Cheque_Issued");
			IssuedChequeData issuedChequeData = new IssuedChequeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(issuedChequeData, "Cheque_Issued", sqlBuilder);
			return issuedChequeData;
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
				string exp = "SELECT Status FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && int.Parse(obj.ToString()) > 1)
				{
					exp = "UPDATE Cheque_Issued SET Status = 1  WHERE ChequeNumber IN ( SELECT ChequeNumber FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "')";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				string exp2 = "SELECT COUNT(ChequeID) FROM Cheque_Issued WHERE Status >2 AND SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj2 = ExecuteScalar(exp2, sqlTransaction);
				if (obj2 != null && int.Parse(obj2.ToString()) > 0)
				{
					throw new CompanyException("There is at least one cheque that is already deposited or voided or etc so cannot be deleted.", 1000);
				}
				exp2 = "UPDATE Cheque_Register SET Status = 1 , ChequeID=NULL WHERE ChequeNumber IN ( SELECT ChequeNumber FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "')";
				flag &= (ExecuteNonQuery(exp2, sqlTransaction) >= 0);
				exp2 = "SELECT TOP 1 PayeeType FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				obj2 = ExecuteScalar(exp2, sqlTransaction);
				if (obj2 != null)
				{
					a = obj2.ToString();
				}
				exp2 = "SELECT TOP 1 PayeeID FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				obj2 = ExecuteScalar(exp2, sqlTransaction);
				if (obj2 != null)
				{
					text = obj2.ToString();
				}
				if (a == "V" && text != "")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Vendor", "PDCAmount", "VendorID", text, sqlTransaction).ToString(), out result);
					exp2 = "SELECT SUM(ISNULL(Amount,0)) FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					decimal.TryParse(ExecuteScalar(exp2, sqlTransaction).ToString(), out result2);
					if (result2 != 0m)
					{
						result -= result2;
						exp2 = "UPDATE Vendor SET PDCAmount=" + result.ToString() + " WHERE VendorID='" + text + "'";
						flag &= Update(exp2, sqlTransaction);
					}
				}
				string commandText = "DELETE FROM Cheque_Issued WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSecurityChequeToPrint(string sysDocID, string voucherID)
		{
			return GetSecurityChequeToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSecurityChequeToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT Security_Cheque.SysDocID, Security_Cheque.VoucherID,Security_Cheque.IssueDate, Security_Cheque.PayeeID, Security_Cheque.ChequebookID,ChequeNumber [Cheque #],CB.ChequebookName AS [Chequebook],Security_Cheque.ChequeDate, Security_Cheque.Note, (SELECT Bank.BankName FROM Bank  LEFT JOIN  Chequebook CR ON Bank.BankID= CR.BankID WHERE CR.ChequebookID=Security_Cheque.ChequebookID) AS BankName\r\n                                ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                                WHEN 'V' THEN Vendor.VendorName\r\n                                WHEN 'A' THEN Account.AccountName\r\n                                WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee],ISNULL(Amount,0) AS [Amount], \r\n                                (SELECT Account.AccountName FROM Account  LEFT JOIN  Chequebook CR ON Account.AccountID= CR.AccountID WHERE CR.ChequebookID=Security_Cheque.ChequebookID) AS AcoountName, CB.AccountID, CB.PDCIssuedAccountID, CB.Signatory\r\n                                FROM Security_Cheque LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=Security_Cheque.ChequebookID \r\n                                LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                                LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                                LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                                LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                                LEFT OUTER JOIN Chequebook CB ON CB.ChequebookID=Security_Cheque.ChequebookID\r\n                            WHERE  SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Security_Cheque", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteIssuedCheque(string receivedChequeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Cheque_Issued WHERE ChequeNumber = '" + receivedChequeID + "'";
				return Delete(commandText, null);
			}
			catch
			{
				throw;
			}
		}

		public IssuedChequeData GetIssuedChequeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ChequeNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Cheque_Issued";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			IssuedChequeData issuedChequeData = new IssuedChequeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(issuedChequeData, "Cheque_Issued", sqlBuilder);
			return issuedChequeData;
		}

		public DataSet GetIssuedChequeByFields(params string[] columns)
		{
			return GetIssuedChequeByFields(null, isInactive: true, columns);
		}

		public DataSet GetIssuedChequeByFields(string[] receivedChequeID, params string[] columns)
		{
			return GetIssuedChequeByFields(receivedChequeID, isInactive: true, columns);
		}

		public DataSet GetIssuedChequeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Cheque_Issued");
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
				commandHelper.TableName = "Cheque_Issued";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Cheque_Issued", sqlBuilder);
			return dataSet;
		}

		public DataSet GetIssuedChequeList(int[] chequeIDs, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			for (int i = 0; i < chequeIDs.Length; i++)
			{
				if (text != "")
				{
					text += ",";
				}
				text = text + "'" + chequeIDs[i].ToString() + "'";
			}
			string textCommand = "SELECT * \r\n                           FROM Cheque_Issued WHERE ChequeID IN (" + text + ")";
			FillDataSet(dataSet, "Cheque_Issued", textCommand, sqlTransaction);
			return dataSet;
		}

		public DataSet GetIssuedChequeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT IssuedChequeID [IssuedCheque Code],IssuedChequeName [IssuedCheque Name],Note,Inactive  \r\n                           FROM IssuedCheque ";
			FillDataSet(dataSet, "Cheque_Issued", textCommand);
			return dataSet;
		}

		public int ValidateBlankCheque(string chequebookID, string chequeNumber, string sysDocID, string voucherID)
		{
			string exp = "SELECT Status FROM Cheque_Register WHERE ChequebookID = '" + chequebookID + "' AND ChequeNumber ='" + chequeNumber + "' ";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return -1;
			}
			if (int.Parse(obj.ToString()) != 1)
			{
				if (sysDocID != "")
				{
					exp = "SELECT COUNT(*) FROM dbo.Cheque_Issued \r\n                                WHERE ChequeNumber = '" + chequeNumber + "' AND ChequebookID = '" + chequebookID + "' AND SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					obj = ExecuteScalar(exp);
					if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
					{
						return 0;
					}
					return -2;
				}
				return -2;
			}
			return 0;
		}

		public bool TransactionHasProcessedCheques(string sysDocID, string VoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(*) FROM Cheque_Issued WHERE \r\n                                SysDocID='" + sysDocID + "' AND VoucherID = '" + VoucherID + "' AND Status > 2 ";
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

		public bool SetChequeStatus(string chequebookID, string chequeNumber, IssuedChequeStatus status, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE Cheque_Register SET Status = " + (byte)status + " WHERE ChequebookID='" + chequebookID + "' AND ChequeNumber = '" + chequeNumber + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "UPDATE Cheque_Issued SET Status = " + (byte)status + " WHERE ChequebookID='" + chequebookID + "' AND ChequeNumber = '" + chequeNumber + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (status == IssuedChequeStatus.Issued)
				{
					exp = "UPDATE Cheque_Issued SET ClearanceDate = NULL , ClearanceVoucherID=NULL,ClearanceSysDocID=NULL  WHERE ChequebookID='" + chequebookID + "' AND ChequeNumber = '" + chequeNumber + "'";
					return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
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
				string exp = "SELECT TOP 1 PayeeType FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					a = obj.ToString();
				}
				exp = "SELECT TOP 1 PayeeID FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (a == "V" && text != "")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Vendor", "PDCAmount", "VendorID", text, sqlTransaction).ToString(), out result);
					exp = "SELECT SUM(ISNULL(Amount,0)) FROM Cheque_Issued WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
						exp = "UPDATE Vendor SET PDCAmount=" + result.ToString() + " WHERE VendorID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
				}
				exp = "UPDATE Cheque_Issued SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetChequesToClearList(DateTime chequeDate)
		{
			DataSet dataSet = new DataSet();
			string str = CommonLib.ToSqlDateTimeString(chequeDate);
			string textCommand = "SELECT ChequeDate AS [Chq Date], ChequeID,ChequeNumber [Cheque #] ,SysDocID,VoucherID,\r\n                            CASE WHEN PayeeType='A' THEN ISNULL(PayeeAccountID,PayeeID) ELSE PayeeAccountID END AS PayeeAccountID,\r\n                            Cheque_Issued.CurrencyID AS Currency,PDCAccountID,AC.AccountName AS [Bank]\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Cheque_Issued.Note END AS [Payee],Cheque_Issued.Note,ISNULL(AmountFC,Amount) AS [Amount]\r\n                            FROM Cheque_Issued LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID\r\n                            WHERE ISNULL(ISVOID,'False') = 'False' AND Cheque_Issued.Status IN (2,5) AND ChequeDate<='" + str + "'";
			FillDataSet(dataSet, "Cheque_Issued", textCommand);
			return dataSet;
		}

		public DataSet GetChequeByID(string chequeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber,SysDocID,VoucherID ,TemplateName,AC.AccountName AS [BankName],Cheque_Issued.Status,Cheque_Issued.CurrencyID,ISNULL(PayeeAccountID,PayeeID) as PayeeAccountID,IsPrinted,PrintName,\r\n                            Cheque_Issued.ExchangeRate,AmountFC,BankAccountID,Cheque_Issued.PDCAccountID,ChequebookName,\r\n                            PayeeType,PayeeID,ChequeDate,Amount,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [PayeeName],                           \r\n                            CASE PayeeType WHEN 'C' THEN ISNULL(Customer.LegalName,ISNULL(Customer.CompanyName,Customer.CustomerName))\r\n                            WHEN 'V' THEN ISNULL(Vendor.LegalName,ISNULL(Vendor.CompanyName,Vendor.VendorName)) \r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [NameOnCheck],\r\n                            'A/C PAYEE ONLY' AS Stamp1,'NON NEGOTIABLE' AS Stamp2,SysDocID,VoucherID\r\n                           FROM Cheque_Issued LEFT OUTER JOIN Bank ON Bank.BankID= Cheque_Issued.BankID\r\n                            LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Issued.ChequebookID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID\r\n                            WHERE ChequeID=" + chequeID;
			FillDataSet(dataSet, "Cheque_Issued", textCommand);
			return dataSet;
		}

		public DataSet GetChequeIssuedandSecirutyChqByID(string chequeID, bool IsSecurityChq)
		{
			DataSet dataSet = new DataSet();
			try
			{
				string textCommand = "SELECT TransactionID,GLT.SysDocID,GLT.VoucherID,GLT.CostCenterID,GLT.PayeeType,GLT.PayeeID,GLT.RegisterID,R.RegisterName, SecondRegisterID,\r\n                                (SELECT Bank.BankName FROM Cheque_Received CR LEFT JOIN Bank ON Bank.BankID= CR.BankID WHERE CR.ChequeID=GLT.ChequeID) AS BankName,\r\n                                (CASE GLT.PayeeType\r\n                                WHEN 'C' THEN Customer.CustomerName\r\n                                WHEN 'V' THEN Vendor.VendorName\r\n                                WHEN 'E' THEN Emp2.FirstName + ' ' + Emp2.LastName\r\n                                ELSE Account.AccountName END) AS PayeeName,\r\n                                ISNULL(GLT.AmountFC,GLT.Amount) AS Amount,TransactionDate,GLT.IsVoid,CurrencyRate,GLType,GLT.ChequebookID,GLT.CheckNumber,GLT.ChequeID,\r\n                                GLT.CheckDate,GLT.Reference,FirstAccountID,SecondAccountID,GLT.EmployeeID,Emp.FirstName + ' ' + Emp.LastName as EmployeeName,GLT.Description,\r\n                                ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID ,\r\n                                ISNULL(FirstAccountID,GLT.RegisterID) AS TransferFromID,ISNULL(SecondAccountID,SecondRegisterID) AS TransferToID,\r\n                                TAcc.AccountName AS TransferFromName,TAcc2.AccountName AS TransferToName,\r\n                                GLT.DateCreated,GLT.DateUpdated,GLT.CreatedBy,GLT.UpdatedBy,RCR.ReasonName, GLT.ExpCode, GLT.ExpAmount, GLT.ExpPercent,GLT.TaxAmount, \r\n                                EX.ExpenseName,Vendor.TaxIDNumber as VTaxIDNo,Customer.TaxIDNumber as CTaxIDNo,(SELECT AddressPrintFormat FROM Customer_Address C WHERE C.CustomerID=GLT.PayeeID AND GLT.PayeeType ='C'  And AddressID='PRIMARY')AS [Customer Address]\r\n                                FROM GL_Transaction GLT LEFT OUTER JOIN Employee Emp ON Emp.EmployeeID=GLT.EmployeeID\r\n                               LEFT OUTER JOIN TRANSACTION_DETAILS TD ON GLT.SysDocID=TD.SysDocID AND GLT.VoucherID=TD.VoucherID\r\n                                LEFT OUTER JOIN Account ON GLT.PayeeID=Account.AccountID \r\n                                LEFt OUTER JOIN Customer ON GLT.PayeeID=Customer.CustomerID \r\n                                LEFt OUTER JOIN Vendor ON GLT.PayeeID=Vendor.VendorID \r\n                                LEFt OUTER JOIN Employee Emp2 ON GLT.PayeeID=Emp2.EmployeeID\r\n                                LEFt OUTER JOIN Account TAcc ON GLT.FirstAccountID=TAcc.AccountID\r\n                                LEFt OUTER JOIN Account TAcc2 ON GLT.SecondAccountID=TAcc2.AccountID\r\n                                LEFT OUTER JOIN Returned_Cheque_Reason RCR ON GLT.ReasonID=RCR.ReasonID\r\n                                LEFT JOIN Expense_Code EX ON GLT.ExpCode=EX.ExpenseID\r\n                                LEFT OUTER JOIN Register R ON GLT.RegisterID=R.RegisterID\r\n\t\t\t\t\t\t\t\tWHERE GLT.SysDocID in ( SELECT SysDocID FROM Cheque_Issued where ChequeID= " + chequeID + ") AND GLT.VoucherID IN (SELECT VoucherID FROM Cheque_Issued where ChequeID=" + chequeID + ")\r\n                                AND TD.CheckNumber IN (select ChequeNumber From Cheque_Issued where ChequeID=" + chequeID + " )\r\n                                ";
				FillDataSet(dataSet, "GL_Transaction", textCommand);
				textCommand = "SELECT TD.*,Chequebook.BankID,Bank.BankName,Vendor.TaxIDNumber as VTaxIDNo,Customer.TaxIDNumber as CTaxIDNo,\r\n                                   (CASE PayeeType\r\n                                    WHEN 'C' THEN Customer.CustomerName\r\n                                    WHEN 'V' THEN Vendor.VendorName\r\n                                    WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName\r\n                                    ELSE Account.AccountName END) AS AccountName,JB.JobName,Account.Alias\r\n                                    FROM TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n                                    Account ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                                    Customer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                    Vendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                                    Employee ON TD.PayeeID=Employee.EmployeeID\r\n                                    LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=TD.ChequebookID\r\n                                    LEFT OUTER JOIN Bank ON Chequebook.BankID=Bank.BankID\r\n                                    LEFt OUTER JOIN Job JB ON TD.JobID=JB.JobID\r\n\t\t\t\t\t\t            WHERE TD.SysDocID in ( SELECT SysDocID FROM Cheque_Issued where ChequeID= " + chequeID + ") \r\n                                    AND TD.VoucherID IN (SELECT VoucherID FROM Cheque_Issued where ChequeID=" + chequeID + ")\r\n                                    AND TD.CheckNumber IN (select ChequeNumber From Cheque_Issued where ChequeID=" + chequeID + " )";
				FillDataSet(dataSet, "Transaction_Details", textCommand);
				textCommand = "SELECT ChequeID,ChequeNumber,SysDocID,VoucherID ,TemplateName,AC.AccountName AS [BankName],Cheque_Issued.Status,Cheque_Issued.CurrencyID,ISNULL(PayeeAccountID,PayeeID) as PayeeAccountID,IsPrinted,PrintName,\r\n                            Cheque_Issued.ExchangeRate,AmountFC,BankAccountID,Cheque_Issued.PDCAccountID,ChequebookName,\r\n                           PayeeType,PayeeID,ChequeDate,Amount,\r\n                           CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                           WHEN 'V' THEN Vendor.VendorName\r\n                           WHEN 'A' THEN Account.AccountName\r\n                          WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [PayeeName],                           \r\n                          CASE PayeeType WHEN 'C' THEN ISNULL(Customer.LegalName,ISNULL(Customer.CompanyName,Customer.CustomerName))\r\n                           WHEN 'V' THEN ISNULL(Vendor.LegalName,ISNULL(Vendor.CompanyName,Vendor.VendorName)) \r\n                           WHEN 'A' THEN Account.AccountName\r\n                          WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [NameOnCheck],\r\n                           'A/C PAYEE ONLY' AS Stamp1,'NON NEGOTIABLE' AS Stamp2,SysDocID,VoucherID\r\n                          FROM Cheque_Issued LEFT OUTER JOIN Bank ON Bank.BankID= Cheque_Issued.BankID\r\n                           LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Issued.ChequebookID\r\n                           LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                           LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                           LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                           LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                           LEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID\r\n                           WHERE ChequeID=" + chequeID;
				FillDataSet(dataSet, "Cheque_Issued", textCommand);
				textCommand = "SELECT * FROM Bank_Fee_Details\r\n\t\t\t\t\t\tWHERE GLTransactionSysDocID in ( SELECT SysDocID FROM Cheque_Issued where ChequeID=" + chequeID + ") AND GLTransactionVoucherID IN (SELECT VoucherID FROM Cheque_Issued where ChequeID=" + chequeID + ")";
				FillDataSet(dataSet, "Bank_Fee_Details", textCommand);
				textCommand = "SELECT TOP 50  * FROM AR_Payment_Allocation\r\n\t\t\t\t\tWHERE PaymentSysDocID IN  ( SELECT SysDocID FROM Cheque_Issued where ChequeID=19006) AND PaymentVoucherID IN (SELECT VoucherID FROM Cheque_Issued where ChequeID=" + chequeID + ")\r\n                    UNION  \r\n                    SELECT * FROM AP_Payment_Allocation WHERE PaymentSysDocID IN  ( SELECT SysDocID FROM Cheque_Issued where ChequeID=" + chequeID + ") AND PaymentVoucherID IN (SELECT VoucherID FROM Cheque_Issued where ChequeID=" + chequeID + ")";
				FillDataSet(dataSet, "Customer", textCommand);
				if (IsSecurityChq)
				{
					dataSet = new DataSet();
					textCommand = "SELECT Security_Cheque.SysDocID,CB.TemplateName,Security_Cheque.VoucherID,Security_Cheque.IssueDate, Security_Cheque.PayeeID, Security_Cheque.ChequebookID,ChequeNumber,CB.ChequebookName AS [Chequebook],\r\n                            Security_Cheque.ChequeDate, Security_Cheque.Note, (SELECT Bank.BankName FROM Bank  LEFT JOIN  Chequebook CR ON Bank.BankID= CR.BankID WHERE CR.ChequebookID=Security_Cheque.ChequebookID) AS ChequebookName,'false' as IsPrinted,'' as Status,'A/C PAYEE ONLY' AS Stamp1,'NON NEGOTIABLE' AS Stamp2\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [PayeeName],\r\n                            CASE PayeeType WHEN 'C' THEN ISNULL(Customer.LegalName,ISNULL(Customer.CompanyName,Customer.CustomerName))\r\n                            WHEN 'V' THEN ISNULL(Vendor.LegalName,ISNULL(Vendor.CompanyName,Vendor.VendorName)) \r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [PrintName],       \r\n                            CASE PayeeType WHEN 'C' THEN ISNULL(Customer.LegalName,ISNULL(Customer.CompanyName,Customer.CustomerName))\r\n                            WHEN 'V' THEN ISNULL(Vendor.LegalName,ISNULL(Vendor.CompanyName,Vendor.VendorName)) \r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [NameonCheck],                            \r\n                            ISNULL(Amount,0) AS [Amount], \r\n                            (SELECT Account.AccountName FROM Account  LEFT JOIN  Chequebook CR ON Account.AccountID= CR.AccountID WHERE CR.ChequebookID=Security_Cheque.ChequebookID) AS AcoountName, CB.AccountID, CB.PDCIssuedAccountID, CB.Signatory\r\n                            FROM Security_Cheque LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=Security_Cheque.ChequebookID \r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Chequebook CB ON CB.ChequebookID=Security_Cheque.ChequebookID\r\n                           WHERE ChequeNumber=" + chequeID;
					FillDataSet(dataSet, "Cheque_Issued", textCommand);
				}
				if (dataSet.Tables.Contains("GL_Transaction"))
				{
					dataSet.Relations.Add("TransactionDetails", new DataColumn[2]
					{
						dataSet.Tables["GL_Transaction"].Columns["SysDocID"],
						dataSet.Tables["GL_Transaction"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Transaction_Details"].Columns["SysDocID"],
						dataSet.Tables["Transaction_Details"].Columns["VoucherID"]
					}, createConstraints: false);
					dataSet.Relations.Add("TransactionFeeDetails", new DataColumn[2]
					{
						dataSet.Tables["GL_Transaction"].Columns["SysDocID"],
						dataSet.Tables["GL_Transaction"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Bank_Fee_Details"].Columns["GLTransactionSysDocID"],
						dataSet.Tables["Bank_Fee_Details"].Columns["GLTransactionVoucherID"]
					}, createConstraints: false);
					dataSet.Relations.Add("PaymentAllocationDetails", new DataColumn[2]
					{
						dataSet.Tables["GL_Transaction"].Columns["SysDocID"],
						dataSet.Tables["GL_Transaction"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Customer"].Columns["PaymentSysDocID"],
						dataSet.Tables["Customer"].Columns["PaymentVoucherID"]
					}, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool MarkAsPrinted(int chequeID)
		{
			string exp = "UPDATE Cheque_Issued SET IsPrinted='True' WHERE ChequeID='" + chequeID.ToString() + "'";
			return ExecuteNonQuery(exp) > 0;
		}

		public DataSet GetIssuedChequeToCancelList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber [Cheque #],Bank.BankName AS [Bank]\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee],ISNULL(AmountFC,Amount) AS [Amount]\r\n                            FROM Cheque_Issued LEFT OUTER JOIN Bank ON Bank.BankID=Cheque_Issued.BankID LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            WHERE Cheque_Issued.Status IN (2,8,5)";
			FillDataSet(dataSet, "Cheque_Issued", textCommand);
			return dataSet;
		}

		public DataSet GetIssuedChequeToReturnList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ChequeID,ChequeNumber [Cheque #],AC.AccountName AS [Bank]--Bank.BankName AS [Bank]\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee],ISNULL(AmountFC,Amount) AS [Amount]\r\n                            FROM Cheque_Issued LEFT OUTER JOIN Bank ON Bank.BankID=Cheque_Issued.BankID LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID\r\n                            WHERE Cheque_Issued.Status IN (4)";
			FillDataSet(dataSet, "Cheque_Issued", textCommand);
			return dataSet;
		}

		public DataSet GetRegisteredBlankChequeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Cheque_Register.ChequeID,Cheque_Register.ChequeNumber [Cheque #],Cheque_Register.ChequebookID,ChequebookName AS Chequebook,CASE  Cheque_Register.Status WHEN 1 THEN 'Blank' ELSE 'Voided' END AS Status\r\n                            FROM Cheque_Register INNER JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Register.ChequebookID\r\n                            WHERE Cheque_Register.Status IN (1,8)";
			FillDataSet(dataSet, "Cheque_Issued", textCommand);
			return dataSet;
		}

		public DataSet GetIssuedChequeToPrint(DateTime startDate, DateTime endDate, bool includePrinted)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT ChequeID,ChequeNumber [Cheque #],ChequebookName AS [Chequebook]\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee],ISNULL(AmountFC,Amount) AS [Amount],'False' as IsSecurityChq\r\n                            FROM Cheque_Issued LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Issued.ChequebookID \r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            WHERE Cheque_Issued.Status IN (2,4,8)";
			if (!includePrinted)
			{
				str += " AND ISNULL(IsPrinted,'False') = 'False'";
			}
			str += "UNION SELECT ChequeNumber as ChequeID,ChequeNumber [Cheque #],ChequebookName AS [Chequebook]\r\n                            ,CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS[Payee], ISNULL(Amount,0) AS[Amount],'True' as IsSecurityChq\r\n                            FROM Security_Cheque LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID = Security_Cheque.ChequebookID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID = PayeeID\r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID = PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID = PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID = PayeeID ";
			FillDataSet(dataSet, "Cheque_Issued", str);
			return dataSet;
		}

		public DataSet GetBlankCheque(string chequebookID, string chequeNumber)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Cheque_Register.* , Chequebook.ChequebookName\r\n                            FROM Cheque_Register INNER JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Register.ChequebookID\r\n                            WHERE Cheque_Register.ChequebookID='" + chequebookID + "' AND ChequeNumber = '" + chequeNumber + "'";
			FillDataSet(dataSet, "Cheque_Register", textCommand);
			return dataSet;
		}

		public bool VoidBlankCheque(string chequebookID, string chequeNumber, string reasonID, string note, bool isVoid)
		{
			int num = 8;
			if (!isVoid)
			{
				num = 1;
			}
			string exp = "SELECT MAX(ChequeID) FROM Cheque_Register WHERE Cheque_Register.ChequebookID='" + chequebookID + "' AND ChequeNumber = '" + chequeNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				throw new CompanyException("This cheque is already in use in a transaction.", 1010);
			}
			exp = "UPDATE Cheque_Register SET Status= " + num.ToString() + ",ReasonID='" + reasonID + "',Note = '" + note + "' \r\n                            WHERE Cheque_Register.ChequebookID='" + chequebookID + "' AND ChequeNumber = '" + chequeNumber + "'";
			return ExecuteNonQuery(exp) > 0;
		}

		public DataSet GetTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT * FROM Security_Cheque WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Security_Cheque", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSecurityCheque(string sysDocID, string voucherID, DateTime voidDate)
		{
			bool flag = true;
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(voidDate);
				int num = 8;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT ChequeNumber,ChequebookID FROM Security_Cheque WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Security_Cheque", textCommand);
				string text2 = dataSet.Tables[0].Rows[0]["ChequeNumber"].ToString();
				string text3 = dataSet.Tables[0].Rows[0]["ChequebookID"].ToString();
				string exp = "UPDATE Security_Cheque SET IsVoid= 'True',VoidDate='" + text + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "UPDATE Cheque_Register SET Status= " + num.ToString() + " WHERE Cheque_Register.ChequebookID='" + text3 + "' AND ChequeNumber = '" + text2 + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
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

		public bool DeleteSecurityCheque(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT ChequeNumber,ChequebookID FROM Security_Cheque WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Security_Cheque", textCommand);
				string text = dataSet.Tables[0].Rows[0]["ChequeNumber"].ToString();
				string text2 = dataSet.Tables[0].Rows[0]["ChequebookID"].ToString();
				textCommand = "DELETE FROM Security_Cheque WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				textCommand = "UPDATE Cheque_Register SET Status = 1 ,IsSecurityCheque='False' WHERE ChequeNumber='" + text + "' AND ChequebookID='" + text2 + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Security Cheque", voucherID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V, SysDocID [Doc ID],VoucherID [Doc Number],ChequeNumber [Chq#],PayeeType [P],PayeeID [Payee Code],\r\n                                                (CASE PayeeType\r\n                                                    WHEN 'C' THEN Customer.CustomerName\r\n                                                    WHEN 'V' THEN Vendor.VendorName\r\n                                                    WHEN 'E' THEN Employee.FirstName\r\n                                                    ELSE Account.AccountName END) AS [Payee Name],\r\n                                                    ChequeDate [Chq Date],IssueDate [Issue Date],\r\n                                                    CHQ.Note,CHQ.Status,Reference,\r\n                                                    Chq.ClearanceSysDocID [Clear DocID],Chq.ClearanceVoucherID [Clear Voucher No],\r\n                                                    ChequeBookID [Chequebook],ISNULL(CHQ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur],\r\n                                                    ISNULL(AmountFC,Amount) AS Amount\r\n                                                    FROM Cheque_Issued CHQ\r\n                                                    LEFt OUTER JOIN Account ON CHQ.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                                                    Customer ON CHQ.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                                    Vendor ON CHQ.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                                                    Employee ON CHQ.PayeeID=Employee.EmployeeID";
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

		public DataSet GetChequeCancelledList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "\tselect GLT.SysDocID AS [DOC ID], GLT.VoucherID AS [Doc NUmber],(CASE TD.PayeeType\r\n\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\tELSE Account.AccountName END) AS AccountName, GLT.Description,GLT.Reference, GLT.CheckNumber, GLT.ChequeID  from GL_Transaction GLT \r\n\t\t\t\t\t\tLEFT OUTER JOIN Transaction_Details TD ON GLT.SysDocID=TD.SysDocID AND GLT.VoucherID=TD.VoucherID\r\n\t\t\t\t\t\tLEFt OUTER JOIN\r\n\t\t\t\t\t\tAccount ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\tCustomer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\tVendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\tEmployee ON TD.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Cheque_Issued CI ON CI.ChequeID=GLT.ChequeID\r\n\t\t\t\t\t\tLEFT OUTER JOIN System_Document SD ON SD.SysDocID=CI.SysDocID \r\n\t\t\t\t\t\twhere CI.Status=9 AND GLT.SysDocID IN (select SysDocID from System_Document where SysDocType=15)";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND GLT.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(GLT.IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Cheque_Issued", sqlCommand);
			return dataSet;
		}

		public DataSet GetSecurityChequeList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V, SysDocID [Doc ID],VoucherID [Doc Number],ChequeNumber [Chq#],PayeeType [P],PayeeID [Payee Code],\r\n                                (CASE PayeeType\r\n                                WHEN 'C' THEN Customer.CustomerName\r\n                                WHEN 'V' THEN Vendor.VendorName\r\n                                WHEN 'E' THEN Employee.FirstName\r\n                                ELSE Account.AccountName END) AS [Payee Name],\r\n                                ChequeDate [Chq Date],IssueDate [Issue Date],\r\n                                CHQ.Note,\r\n                                ChequebookID Chequebook,Amount\r\n                                FROM Security_Cheque CHQ\r\n                                LEFt OUTER JOIN Account ON CHQ.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                                Customer ON CHQ.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                Vendor ON CHQ.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                                Employee ON CHQ.PayeeID=Employee.EmployeeID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE IssueDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Quote", sqlCommand);
			return dataSet;
		}

		public DataSet GetIssuedChequeAsOfDate(DateTime chkfrom, DateTime chkto, DateTime from, DateTime to, string fromBank, string toBank, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromLocationID, string toLocationID, bool cleared, bool bounced, bool cancelled, string strGroupBy, string vendorIDs)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = CommonLib.ToSqlDateTimeString(chkfrom);
			string text4 = CommonLib.ToSqlDateTimeString(chkto);
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
			string text6 = "SELECT Cheque_Issued.SysDocID [Sys Doc], Cheque_Issued.VoucherID [Doc Number], AC.AccountName [Bank Name], ChequeNumber [Cheque No], PayeeType [Payee Type], Amount, Cheque_Issued.BankID,\r\n                            ChequeDate [Cheque Date],IssueDate AS [Issue Date], ClearanceDate,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee],Cheque_Issued.Note,CASE Cheque_Issued.Status  WHEN  1 THEN 'blank' WHEN 2 THEN 'ISSUED' WHEN 4 THEN 'CLEARED' WHEN 5 THEN 'BOUNCED' WHEN 8 THEN 'Voided' WHEN 9 THEN 'Cancelled' END AS STATUS\r\n                            FROM Cheque_Issued \r\n                            LEFT JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Issued.ChequebookID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID  ";
			if (fromLocationID != "")
			{
				text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=Cheque_Issued.SysDocID";
			}
			text6 = text6 + " WHERE ISNULL(ISVOID,'False') = 'False' AND Cheque_Issued.Status IN (" + text5 + ") \r\n                            AND IssueDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text6 = text6 + " AND ChequeDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
			if (fromBank != "")
			{
				text6 = text6 + " AND Chequebook.BankID BETWEEN '" + fromBank + "' AND '" + toBank + "' ";
			}
			if (vendorIDs != "")
			{
				text6 = text6 + " AND Cheque_Issued.PayeeID IN(" + vendorIDs + ")";
			}
			if (fromCustomer != "")
			{
				text6 = text6 + " AND PayeeType = 'V' AND Cheque_Issued.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromClass != "")
			{
				text6 = text6 + " AND PayeeType = 'V' AND Cheque_Issued.PayeeID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text6 = text6 + " AND PayeeType = 'V' AND Cheque_Issued.CustomerID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromLocationID != "")
			{
				text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text6);
			FillDataSet(dataSet, "Cheque_Issued", sqlCommand);
			text6 = "SELECT DISTINCT ";
			if (strGroupBy == "4")
			{
				text6 += "IssueDate AS [GROUP]";
			}
			if (strGroupBy == "3")
			{
				text6 += "AC.AccountName AS [GROUP]";
			}
			else if (strGroupBy == "2")
			{
				text6 += "ChequeDate  AS [GROUP]";
			}
			else if (strGroupBy == "1")
			{
				text6 += "CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'A' THEN Account.AccountName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [GROUP]";
			}
			text6 += "FROM Cheque_Issued \r\n                            LEFT JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Issued.ChequebookID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID";
			if (fromLocationID != "")
			{
				text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=Cheque_Issued.SysDocID";
			}
			text6 = text6 + " WHERE ISNULL(ISVOID,'False') = 'False' AND Cheque_Issued.Status IN (" + text5 + ") \r\n                            AND IssueDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text6 = text6 + " AND ChequeDate BETWEEN '" + text3 + "' AND '" + text4 + "' ";
			if (fromBank != "")
			{
				text6 = text6 + " AND Chequebook.BankID BETWEEN '" + fromBank + "' AND '" + toBank + "' ";
			}
			if (vendorIDs != "")
			{
				text6 = text6 + " AND Cheque_Issued.PayeeID IN(" + vendorIDs + ")";
			}
			if (fromCustomer != "")
			{
				text6 = text6 + " AND PayeeType = 'V' AND Cheque_Issued.PayeeID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
			}
			if (fromClass != "")
			{
				text6 = text6 + " AND PayeeType = 'V' AND Cheque_Issued.PayeeID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text6 = text6 + " AND PayeeType = 'V' AND Cheque_Issued.PayeeID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromLocationID != "")
			{
				text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			sqlCommand = new SqlCommand(text6);
			FillDataSet(dataSet, "Temp", sqlCommand);
			if (dataSet.Tables["Cheque_Issued"].Rows.Count > 0)
			{
				if (strGroupBy == "1")
				{
					dataSet.Relations.Add("IssuedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Issued"].Columns["Payee"]
					}, createConstraints: false);
				}
				else if (strGroupBy == "2")
				{
					dataSet.Relations.Add("IssuedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Issued"].Columns["Cheque Date"]
					}, createConstraints: false);
				}
				else if (strGroupBy == "3")
				{
					dataSet.Relations.Add("IssuedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Issued"].Columns["Bank Name"]
					}, createConstraints: false);
				}
				else if (strGroupBy == "4")
				{
					dataSet.Relations.Add("IssuedCheque_Rel", new DataColumn[1]
					{
						dataSet.Tables["Temp"].Columns["GROUP"]
					}, new DataColumn[1]
					{
						dataSet.Tables["Cheque_Issued"].Columns["Issue Date"]
					}, createConstraints: false);
				}
			}
			return dataSet;
		}

		public DataSet GetChequeMaturityReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, bool cleared, bool bounced, bool cancelled)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = 2.ToString() + ",";
			if (cleared)
			{
				text3 = text3 + 4.ToString() + ",";
			}
			if (bounced)
			{
				text3 = text3 + 5.ToString() + ",";
			}
			if (cancelled)
			{
				text3 = text3 + 9.ToString() + ",";
			}
			text3 = text3.Remove(text3.Length - 1, 1);
			string text4 = "SELECT ChequeDate,PayeeID,ChequeNumber,Cheque_Received.DepositDate,Cheque_Received.BankID,BANK.BankName,DepositAccountID,Ac.AccountName,Amount,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee] FROM Cheque_Received\r\n                            INNER JOIN BANK ON Bank.BankID=Cheque_Received.BankID \r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Account AC ON AC.AccountID = DepositAccountID\r\n                            WHERE Cheque_Received.Status=2  AND Cheque_Received.DepositDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromAccountID != "")
			{
				text4 = text4 + " AND DepositAccountID BETWEEN '" + fromAccountID + "' AND '" + toAccountID + "' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text4);
			FillDataSet(dataSet, "Cheque_Received", sqlCommand);
			return dataSet;
		}

		public string GetAnalysisID(string sysDocID, string voucherID)
		{
			new DataSet();
			string exp = "SELECT AnalysisID FROM GL_Transaction GL  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return null;
			}
			return obj.ToString();
		}

		public DataSet GetIssuedChequeReturnList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount], CheckNumber [Chq #]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (16)";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Quote", sqlCommand);
			return dataSet;
		}
	}
}
