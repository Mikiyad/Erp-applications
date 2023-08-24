using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SendCheques : StoreObject
	{
		private const string CHEQUESEND_TABLE = "Cheque_Send";

		private const string CHEQUESENDDETAIL_TABLE = "Cheque_Send_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REFERENCE_PARM = "@Reference";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string REASON_PARM = "@Reason";

		private const string NOTE_PARM = "@Note";

		private const string STATUS_PARM = "@Status";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string CHEQUEID_PARM = "@ChequeID";

		public SendCheques(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSendChequeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cheque_Send", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("Reason", "@Reason"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Cheque_Send", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSendChequeCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSendChequeText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSendChequeText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reason", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BankAccountID"].SourceColumn = "BankAccountID";
			parameters["@Reason"].SourceColumn = "Reason";
			parameters["@Reference"].SourceColumn = "Reference";
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

		private string GetInsertUpdateSendChequeDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cheque_Send_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("Status", "@Status"), new FieldValue("ChequeID", "@ChequeID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSendChequeDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSendChequeDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSendChequeDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ChequeID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ChequeID"].SourceColumn = "ChequeID";
			parameters["@Status"].SourceColumn = "Status";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SendChequeData journalData)
		{
			return true;
		}

		public bool InsertUpdateSendCheque(SendChequeData sendChequeData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSendChequeCommand = GetInsertUpdateSendChequeCommand(isUpdate);
			try
			{
				DataRow dataRow = sendChequeData.ChequeSendTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string text2 = dataRow["Reference"].ToString();
				int[] array = new int[sendChequeData.ChequeSendDetailTable.Rows.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = int.Parse(sendChequeData.ChequeSendDetailTable.Rows[i]["ChequeID"].ToString());
				}
				string text3 = "";
				for (int j = 0; j < array.Length; j++)
				{
					if (text3 != "")
					{
						text3 += ",";
					}
					text3 = text3 + "'" + array[j].ToString() + "'";
				}
				string text4 = "";
				if (!isUpdate)
				{
					text4 = "SELECT COUNT(ChequeID) FROM Cheque_Received WHERE ChequeID IN (" + text3 + ") AND Status NOT IN (1,8)";
					object obj = ExecuteScalar(text4);
					if (obj != null && int.Parse(obj.ToString()) > 0)
					{
						throw new CompanyException("Only cheques that are in 'Undeposited' status can be sent to bank. One or more cheques may already being deposited or in another status.", 1008);
					}
				}
				else
				{
					text4 = "SELECT COUNT(ChequeID) FROM Cheque_Received WHERE ChequeID IN (" + text3 + ") AND Status NOT IN (4)";
					object obj2 = ExecuteScalar(text4);
					if (obj2 != null && int.Parse(obj2.ToString()) > 0)
					{
						throw new CompanyException("There are cheques that are bounced, matured or cancelled. Transaction cannot be modified", 1008);
					}
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Cheque_Send", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateSendChequeCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(sendChequeData, "Cheque_Send", insertUpdateSendChequeCommand)) : (flag & Insert(sendChequeData, "Cheque_Send", insertUpdateSendChequeCommand)));
				insertUpdateSendChequeCommand = GetInsertUpdateSendChequeDetailsCommand(isUpdate: false);
				insertUpdateSendChequeCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteSendChequeDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (sendChequeData.Tables["Cheque_Send_Detail"].Rows.Count > 0)
				{
					flag &= Insert(sendChequeData, "Cheque_Send_Detail", insertUpdateSendChequeCommand);
				}
				text4 = "UPDATE Cheque_Received SET SendDate='" + CommonLib.ToSqlDateTimeString(DateTime.Parse(dataRow["TransactionDate"].ToString())) + "',SendBankAccountID='" + dataRow["BankAccountID"].ToString() + "',Status = 4,SendReference = '" + text2 + "' WHERE ChequeID IN (" + text3 + ")";
				flag &= (ExecuteNonQuery(text4, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Cheque_Send", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Cheque_Send", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SendChequesToBank, sysDocID, text, "Cheque_Send", sqlTransaction);
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

		public SendChequeData GetSendChequeByID(string sysDocID, string voucherID)
		{
			try
			{
				SendChequeData sendChequeData = new SendChequeData();
				string textCommand = "SELECT * FROM Cheque_Send WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(sendChequeData, "Cheque_Send", textCommand);
				if (sendChequeData == null || sendChequeData.Tables.Count == 0 || sendChequeData.Tables["Cheque_Send"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT  'True' AS 'D', TD.ChequeID,TD.SysDocID,TD.VoucherID,CQR.ChequeDate AS [Chq Date],CQR.ChequeNumber [Chq #],CQR.BankID,\r\n                            CQR.PayeeID,CQR.PayeeType,CQR.PayeeAccountID,CQR.PDCAccountID,Bank.BankName [Bank Name],CQR.DepositSysDocID AS [Maturity DocID],CQR.DepositVoucherID AS [Maturity DocNo],\r\n                            \r\n                            CASE CQR.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n                            ISNULL(CQR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(CQR.AmountFC,CQR.Amount) AS Amount, CQR.Status AS ReceivedStatus\r\n                            FROM   Cheque_Send_Detail TD \r\n                            INNER JOIN Cheque_Received CQR ON CQR.ChequeID = TD.ChequeID\r\n\t\t\t\t\t\t\tINNER JOIN Bank ON CQR.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Cheque_Send TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=CQR.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=CQR.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=CQR.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=CQR.PayeeID\r\n                            WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(sendChequeData, "Cheque_Send_Detail", textCommand);
				return sendChequeData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSendChequeDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE Cheque_Received SET SendDate= NULL , SendBankAccountID=NULL,Status = 1\r\n                             WHERE ChequeID IN (Select ChequeID FROM  Cheque_Send_Detail CSD WHERE CSD.SysDocID= '" + sysDocID + "' AND CSD.VoucherID = '" + voucherID + "')";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				string commandText = "DELETE FROM Cheque_Send_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSendCheque(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Cheque_Send SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Sales Quote", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteSendCheque(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteSendChequeDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Cheque_Send WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Cheque Send", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetSendChequeToPrint(string sysDocID, string voucherID)
		{
			return GetSendChequeToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSendChequeToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = "SELECT CS.*, AccountName FROM Cheque_Send CS INNER JOIN Account A ON CS.BankAccountID = A.AccountID WHERE VoucherID=" + text + " AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Cheque_Send", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Cheque_Send"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT  'True' AS 'D', TD.ChequeID,TD.SysDocID,TD.VoucherID,CQR.ChequeDate AS [Chq Date],CQR.ChequeNumber [Chq #],CQR.BankID,\r\n                            CQR.PayeeID,CQR.PayeeType,CQR.PayeeAccountID,CQR.PDCAccountID, Bank.BankName [Bank Name],\r\n                            \r\n                            CASE CQR.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n                            ISNULL(CQR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(CQR.AmountFC,CQR.Amount) AS Amount\r\n                            FROM   Cheque_Send_Detail TD \r\n                            INNER JOIN Cheque_Received CQR ON CQR.ChequeID = TD.ChequeID\r\n\t\t\t\t\t\t\tINNER JOIN Bank ON CQR.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Cheque_Send TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=CQR.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=CQR.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=CQR.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=CQR.PayeeID\r\n                            WHERE TD.VoucherID=" + text + " AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Cheque_Send_Detail", textCommand);
				return dataSet;
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
			string text3 = "SELECT    SysDocID [Doc ID],VoucherID [Doc Number], TransactionDate [Date],CS.BankAccountID,AC.AccountName                 \r\n                            FROM         Cheque_Send CS INNER JOIN Account AC ON AC.AccountID = CS.BankAccountID\r\n                            ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Cheque_Send", sqlCommand);
			return dataSet;
		}
	}
}
