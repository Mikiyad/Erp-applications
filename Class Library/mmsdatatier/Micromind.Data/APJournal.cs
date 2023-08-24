using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Micromind.Data
{
	public sealed class APJournal : StoreObject
	{
		private const string APJOURNAL_TABLE = "@APJournal";

		private const string APID_PARM = "@APID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string REFERENCE_PARM = "@Reference";

		private const string APACCOUNTID_PARM = "@APAccountID";

		private const string DEBIT_PARM = "@Debit";

		private const string CREDIT_PARM = "@Credit";

		private const string DEBITFC_PARM = "@DebitFC";

		private const string CREDITFC_PARM = "@CreditFC";

		private const string CONDEBITFC_PARM = "@ConDebitFC";

		private const string CONCREDITFC_PARM = "@ConCreditFC";

		private const string CONRATE_PARM = "@ConRate";

		private const string ISPDCROW_PARM = "@IsPDCRow";

		private const string ORIGINALAMOUNT_PARM = "@OriginalAmount";

		private const string AMOUNTDUE_PARM = "@AmountDue";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string CHEQUENUMBER_PARM = "@ChequeNumber";

		private const string BANKID_PARM = "@BankID";

		private const string CHEQUEDATE_PARM = "@ChequeDate";

		private const string PAYMENTMETHODID_PARM = "@PaymentMethodID";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string DESCRIPTION_PARM = "@Description";

		private const string JOURNALID_PARM = "@JournalID";

		private const string APDATE_PARM = "@ARDate";

		private const string APDUEDATE_PARM = "@APDueDate";

		private const string ALLOCATIONID_PARM = "@AllocationID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ATTRIBUTEID1_PARM = "@AttributeID1";

		private const string ATTRIBUTEID2_PARM = "@AttributeID2";

		private const string ISNONSTATEMENT_PARM = "@IsNonStatement";

		private const string ISDRAFT_PARM = "@IsDraft";

		private const string APPAYMENTADVICE_TABLE = "@AP_Payment_Advice";

		private const string ARARPAYMENTALLOCATION_TABLE = "@AP_Payment_Allocation";

		private const string APJOURNALID_PARM = "@APJournalID";

		private const string PAYMENTAPID_PARM = "@PaymentAPID";

		private const string INVOICESYSDOCID_PARM = "@InvoiceSysDocID";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string PAYMENTSYSDOCID_PARM = "@PaymentSysDocID";

		private const string PAYMENTVOUCHERID_PARM = "@PaymentVoucherID";

		private const string ALLOCATIONDATE_PARM = "@AllocationDate";

		private const string PAYMENTAMOUNT_PARM = "@PaymentAmount";

		private const string PAYMENTAMOUNTFC_PARM = "@PaymentAmountFC";

		private const string DISCOUNTAMOUNT_PARM = "@DiscountAmount";

		private const string DISCOUNTAMOUNTFC_PARM = "@DiscountAmountFC";

		private const string REALIZEDGAINLOSS_PARM = "@RealizedGainLoss";

		private const string ISAR_PARM = "@IsAR";

		private const string PAYMENTALLOCATIONBATCH_TABLE = "@Payment_Allocation_Batch";

		private const string BATCHID_PARM = "@BatchID";

		private const string BATCHDATE_PARM = "@BatchDate";

		private const string PARTYTYPE_PARM = "@PartyType";

		private const string PARTYID_PARM = "@PartyID";

		public APJournal(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateAPJournalText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("APJournal", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("Reference", "@Reference"), new FieldValue("APAccountID", "@APAccountID"), new FieldValue("Debit", "@Debit"), new FieldValue("IsPDCRow", "@IsPDCRow"), new FieldValue("IsNonStatement", "@IsNonStatement"), new FieldValue("Credit", "@Credit"), new FieldValue("DebitFC", "@DebitFC"), new FieldValue("CreditFC", "@CreditFC"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("ConDebitFC", "@ConDebitFC"), new FieldValue("ConCreditFC", "@ConCreditFC"), new FieldValue("ConRate", "@ConRate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("ChequeNumber", "@ChequeNumber"), new FieldValue("BankID", "@BankID"), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("Description", "@Description"), new FieldValue("APDate", "@ARDate"), new FieldValue("APDueDate", "@APDueDate"), new FieldValue("AllocationID", "@AllocationID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("AttributeID1", "@AttributeID1"), new FieldValue("AttributeID2", "@AttributeID2"), new FieldValue("JournalID", "@JournalID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("APJournal", new FieldValue("APID", "@APID", isUpdateConditionField: true));
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateAPJournalCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateAPJournalText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateAPJournalText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			if (isUpdate)
			{
				parameters.Add("@APID", SqlDbType.Int);
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@APAccountID", SqlDbType.NVarChar);
			parameters.Add("@Debit", SqlDbType.Money);
			parameters.Add("@Credit", SqlDbType.Money);
			parameters.Add("@DebitFC", SqlDbType.Money);
			parameters.Add("@CreditFC", SqlDbType.Money);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@IsPDCRow", SqlDbType.Bit);
			parameters.Add("@IsNonStatement", SqlDbType.Bit);
			parameters.Add("@ConDebitFC", SqlDbType.Money);
			parameters.Add("@ConCreditFC", SqlDbType.Money);
			parameters.Add("@ConRate", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@ChequeNumber", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@JournalID", SqlDbType.NVarChar);
			parameters.Add("@ARDate", SqlDbType.DateTime);
			parameters.Add("@APDueDate", SqlDbType.DateTime);
			parameters.Add("@AllocationID", SqlDbType.Int);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@AttributeID1", SqlDbType.NVarChar);
			parameters.Add("@AttributeID2", SqlDbType.NVarChar);
			if (isUpdate)
			{
				parameters["@APID"].SourceColumn = "APID";
			}
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@APAccountID"].SourceColumn = "APAccountID";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@DebitFC"].SourceColumn = "DebitFC";
			parameters["@CreditFC"].SourceColumn = "CreditFC";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@IsPDCRow"].SourceColumn = "IsPDCRow";
			parameters["@IsNonStatement"].SourceColumn = "IsNonStatement";
			parameters["@ConDebitFC"].SourceColumn = "ConDebitFC";
			parameters["@ConCreditFC"].SourceColumn = "ConCreditFC";
			parameters["@ConRate"].SourceColumn = "ConRate";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@JournalID"].SourceColumn = "JournalID";
			parameters["@ARDate"].SourceColumn = "APDate";
			parameters["@APDueDate"].SourceColumn = "APDueDate";
			parameters["@AllocationID"].SourceColumn = "AllocationID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@AttributeID1"].SourceColumn = "AttributeID1";
			parameters["@AttributeID2"].SourceColumn = "AttributeID2";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePaymentAllocationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("AP_Payment_Allocation", new FieldValue("APJournalID", "@APJournalID"), new FieldValue("PaymentAPID", "@PaymentAPID"), new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("BatchID", "@BatchID"), new FieldValue("PaymentSysDocID", "@PaymentSysDocID"), new FieldValue("PaymentVoucherID", "@PaymentVoucherID"), new FieldValue("AllocationDate", "@AllocationDate"), new FieldValue("PaymentAmount", "@PaymentAmount"), new FieldValue("PaymentAmountFC", "@PaymentAmountFC"), new FieldValue("RealizedGainLoss", "@RealizedGainLoss"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("DiscountAmount", "@DiscountAmount"), new FieldValue("DiscountAmountFC", "@DiscountAmountFC"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentAllocationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentAllocationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentAllocationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@APJournalID", SqlDbType.Int);
			parameters.Add("@PaymentAPID", SqlDbType.Int);
			parameters.Add("@BatchID", SqlDbType.Int);
			parameters.Add("@PaymentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PaymentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@AllocationDate", SqlDbType.DateTime);
			parameters.Add("@PaymentAmount", SqlDbType.Money);
			parameters.Add("@PaymentAmountFC", SqlDbType.Money);
			parameters.Add("@DiscountAmount", SqlDbType.Money);
			parameters.Add("@DiscountAmountFC", SqlDbType.Money);
			parameters.Add("@RealizedGainLoss", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@APJournalID"].SourceColumn = "APJournalID";
			parameters["@PaymentAPID"].SourceColumn = "PaymentAPID";
			parameters["@BatchID"].SourceColumn = "BatchID";
			parameters["@PaymentSysDocID"].SourceColumn = "PaymentSysDocID";
			parameters["@PaymentVoucherID"].SourceColumn = "PaymentVoucherID";
			parameters["@AllocationDate"].SourceColumn = "AllocationDate";
			parameters["@PaymentAmount"].SourceColumn = "PaymentAmount";
			parameters["@PaymentAmountFC"].SourceColumn = "PaymentAmountFC";
			parameters["@DiscountAmount"].SourceColumn = "DiscountAmount";
			parameters["@DiscountAmountFC"].SourceColumn = "DiscountAmountFC";
			parameters["@RealizedGainLoss"].SourceColumn = "RealizedGainLoss";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePaymentAdviceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("AP_Payment_Advice", new FieldValue("APJournalID", "@APJournalID"), new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("PaymentSysDocID", "@PaymentSysDocID"), new FieldValue("PaymentVoucherID", "@PaymentVoucherID"), new FieldValue("PaymentAmount", "@PaymentAmount"), new FieldValue("PaymentAmountFC", "@PaymentAmountFC"), new FieldValue("RealizedGainLoss", "@RealizedGainLoss"), new FieldValue("APDate", "@ARDate"), new FieldValue("APDueDate", "@APDueDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("OriginalAmount", "@OriginalAmount"), new FieldValue("AmountDue", "@AmountDue"), new FieldValue("VendorID", "@VendorID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("DiscountAmount", "@DiscountAmount"), new FieldValue("IsDraft", "@IsDraft"), new FieldValue("DiscountAmountFC", "@DiscountAmountFC"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentAdviceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentAdviceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentAdviceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@APJournalID", SqlDbType.Int);
			parameters.Add("@PaymentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PaymentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PaymentAmount", SqlDbType.Money);
			parameters.Add("@PaymentAmountFC", SqlDbType.Money);
			parameters.Add("@DiscountAmount", SqlDbType.Money);
			parameters.Add("@DiscountAmountFC", SqlDbType.Money);
			parameters.Add("@ARDate", SqlDbType.DateTime);
			parameters.Add("@APDueDate", SqlDbType.DateTime);
			parameters.Add("@RealizedGainLoss", SqlDbType.Money);
			parameters.Add("@OriginalAmount", SqlDbType.Money);
			parameters.Add("@AmountDue", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@IsDraft", SqlDbType.Bit);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@APJournalID"].SourceColumn = "APJournalID";
			parameters["@PaymentSysDocID"].SourceColumn = "PaymentSysDocID";
			parameters["@PaymentVoucherID"].SourceColumn = "PaymentVoucherID";
			parameters["@PaymentAmount"].SourceColumn = "PaymentAmount";
			parameters["@PaymentAmountFC"].SourceColumn = "PaymentAmountFC";
			parameters["@ARDate"].SourceColumn = "APDate";
			parameters["@APDueDate"].SourceColumn = "APDueDate";
			parameters["@OriginalAmount"].SourceColumn = "OriginalAmount";
			parameters["@AmountDue"].SourceColumn = "AmountDue";
			parameters["@DiscountAmount"].SourceColumn = "DiscountAmount";
			parameters["@DiscountAmountFC"].SourceColumn = "DiscountAmountFC";
			parameters["@RealizedGainLoss"].SourceColumn = "RealizedGainLoss";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@IsDraft"].SourceColumn = "IsDraft";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateAllocationBatchText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Payment_Allocation_Batch", new FieldValue("PartyType", "@PartyType"), new FieldValue("PartyID", "@PartyID"), new FieldValue("BatchDate", "@BatchDate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateAllocationBatchCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateAllocationBatchText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateAllocationBatchText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@BatchDate", SqlDbType.DateTime);
			parameters.Add("@PartyID", SqlDbType.NVarChar);
			parameters.Add("@PartyType", SqlDbType.Char);
			parameters["@BatchDate"].SourceColumn = "BatchDate";
			parameters["@PartyID"].SourceColumn = "PartyID";
			parameters["@PartyType"].SourceColumn = "PartyType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(GLData journalData)
		{
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			foreach (DataRow row in journalData.JournalDetailsTable.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(row["Debit"].ToString(), out result);
				decimal.TryParse(row["Credit"].ToString(), out result2);
				if (result > 0m && result2 > 0m)
				{
					throw new Exception("Either debit or credit should be zero");
				}
				d += result;
				d2 += result2;
			}
			if (d != d2)
			{
				throw new Exception("Total debit must equal total credit.");
			}
			return true;
		}

		private int GetJournalID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT JournalID FROM Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public bool InsertJournal(APJournalData journalData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateAPJournalCommand = GetInsertUpdateAPJournalCommand(isUpdate: false);
			try
			{
				_ = journalData.Tables["APJournal"].Rows[0];
				insertUpdateAPJournalCommand.Transaction = sqlTransaction;
				string baseCurrencyID = GetBaseCurrencyID();
				string commaSeperatedIDs = GetCommaSeperatedIDs(journalData, "APJournal", "VendorID");
				string textCommand = "SELECT VendorID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Vendor WHERE VendorID IN (" + commaSeperatedIDs + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Vendor", textCommand);
				foreach (DataRow row in journalData.Tables["APJournal"].Rows)
				{
					if (!(row["SysDocID"].ToString().ToLower() == "sys_val"))
					{
						string a = row["CurrencyID"].ToString();
						string str = row["VendorID"].ToString();
						string text = dataSet.Tables[0].Select("VendorID = '" + str + "'")[0]["CurrencyID"].ToString();
						string text2 = CommonLib.ToSqlDateTimeString(DateTime.Parse(row["APDate"].ToString()));
						if (text != baseCurrencyID && a != text)
						{
							decimal d = default(decimal);
							decimal d2 = default(decimal);
							decimal num = 1m;
							if (row["Debit"] != DBNull.Value)
							{
								d = decimal.Parse(row["Debit"].ToString());
							}
							if (row["Credit"] != DBNull.Value)
							{
								d2 = decimal.Parse(row["Credit"].ToString());
							}
							textCommand = " SELECT DISTINCT ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = '" + text + "' AND Ex.RateUpdatedDate < '" + text2 + "' Order By RateUpdatedDate DESC),\r\n                                     (SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = '" + text + "'))  AS CurRate FROM Currency";
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
									row["ConDebitFC"] = Math.Round(d / num, 4);
								}
								else
								{
									row["ConDebitFC"] = Math.Round(d * num, 4);
								}
							}
							if (d2 != 0m)
							{
								if (currencyRateType == "M")
								{
									row["ConCreditFC"] = Math.Round(d2 / num, 4);
								}
								else
								{
									row["ConCreditFC"] = Math.Round(d2 * num, 4);
								}
							}
						}
					}
				}
				flag = Insert(journalData, "APJournal", insertUpdateAPJournalCommand);
				decimal result = default(decimal);
				string text3 = journalData.Tables["APJournal"].Rows[0]["VendorID"].ToString();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Vendor", "Balance", "VendorID", text3, sqlTransaction);
				if (fieldValue != null)
				{
					decimal.TryParse(fieldValue.ToString(), out result);
				}
				foreach (DataRow row2 in journalData.Tables["APJournal"].Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row2["Debit"].ToString(), out result2);
					decimal.TryParse(row2["Credit"].ToString(), out result3);
					result += result3 - result2;
				}
				textCommand = "UPDATE Vendor SET Balance=" + result.ToString() + " WHERE VendorID='" + text3 + "'";
				return flag & Update(textCommand, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool VoidAPJournal(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string text = "";
				string exp = "SELECT VendorID FROM APJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (!(text != ""))
				{
					return flag;
				}
				decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Vendor", "Balance", "VendorID", text, sqlTransaction).ToString(), out result);
				exp = "SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM APJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				decimal.TryParse(ExecuteScalar(exp, sqlTransaction).ToString(), out result2);
				if (result2 != 0m)
				{
					if (isVoid)
					{
						result += result2;
					}
					else
					{
						result -= result2;
					}
					exp = "UPDATE Vendor SET Balance=" + result.ToString() + " WHERE VendorID='" + text + "'";
					flag &= Update(exp, sqlTransaction);
				}
				exp = "UPDATE APJOURNAL SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				int result3 = 0;
				exp = "SELECT AllocationID FROM AP_Payment_Allocation WHERE (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
				object obj2 = ExecuteScalar(exp, sqlTransaction);
				if (obj2 == null)
				{
					obj2 = 0;
				}
				if (Convert.ToInt32(obj2) == 0)
				{
					exp = "SELECT AllocationID FROM AP_Payment_Allocation WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "')";
					obj2 = ExecuteScalar(exp, sqlTransaction);
					if (obj2 == null)
					{
						obj2 = 0;
					}
				}
				int.TryParse(obj2.ToString(), out result3);
				DeleteAPAllocation(result3, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteAPJournal(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				int result3 = 0;
				object obj = null;
				string text = "";
				string exp = "SELECT VendorID FROM APJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj2 = ExecuteScalar(exp, sqlTransaction);
				if (obj2 != null)
				{
					text = obj2.ToString();
				}
				if (text != "")
				{
					object obj3 = new Databases(base.DBConfig).GetFieldValue("Vendor", "Balance", "VendorID", text, sqlTransaction);
					if (obj3 == null)
					{
						obj3 = 0;
					}
					decimal.TryParse(obj3.ToString(), out result);
					exp = "SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM APJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					obj3 = ExecuteScalar(exp, sqlTransaction);
					decimal.TryParse(obj3.ToString(), out result2);
					if (result2 != 0m)
					{
						result += result2;
						exp = "UPDATE Vendor SET Balance=" + result.ToString() + " WHERE VendorID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
					bool flag2 = false;
					exp = "SELECT AllocationID FROM AP_Payment_Allocation WHERE (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
					obj = ExecuteScalar(exp, sqlTransaction);
					if (obj == null)
					{
						obj = 0;
					}
					if (Convert.ToInt32(obj) == 0)
					{
						exp = "SELECT AllocationID FROM AP_Payment_Allocation WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "')";
						obj = ExecuteScalar(exp, sqlTransaction);
						if (obj == null)
						{
							obj = 0;
						}
						flag2 = true;
					}
					int.TryParse(obj.ToString(), out result3);
					DataSet dataSet = new DataSet();
					dataSet = new CompanyInformations(base.DBConfig).GetCompanyPreferences();
					DataRow dataRow = null;
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
					{
						dataRow = dataSet.Tables[0].Rows[0];
					}
					byte result4 = 1;
					byte.TryParse(dataRow["RemoveAllocationAction"].ToString(), out result4);
					if (string.IsNullOrEmpty(dataRow["RemoveAllocationAction"].ToString()))
					{
						result4 = 1;
					}
					if (result3 <= 0 || result4 != 1)
					{
						if (result3 > 0 && result4 == 2)
						{
							exp = "select PaymentSysDocID,PaymentVoucherID,InvoiceSysDocID,InvoiceVoucherID,SD1.DocName as 'PaymentDoc',SD2.DocName as 'InvoiceDoc' from AP_Payment_Allocation\r\n                                LEFT JOIN System_Document SD1 ON SD1.SysDocID=PaymentSysDocID\r\n                                LEFT JOIN System_Document SD2 ON SD2.SysDocID=InvoiceSysDocID WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "') OR (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
							DataSet dataSet2 = new DataSet();
							FillDataSet(dataSet2, "Allocated_Data", exp, sqlTransaction);
							DataSet dataSet3 = new DataSet();
							dataSet3.Tables.Add(new DataTable());
							dataSet3.Tables[0].Columns.Add("DocName", typeof(string));
							dataSet3.Tables[0].Columns.Add("paymentFullName", typeof(string));
							dataSet3.Tables[0].Columns.Add("invoiceFullName", typeof(string));
							foreach (DataRow row in dataSet2.Tables[0].Rows)
							{
								DataRow dataRow2 = dataSet3.Tables[0].NewRow();
								string text2 = row["PaymentDoc"].ToString();
								string text3 = row["InvoiceDoc"].ToString();
								string text4 = row["PaymentSysDocID"].ToString();
								string text5 = row["PaymentVoucherID"].ToString();
								string text6 = row["InvoiceSysDocID"].ToString();
								string text7 = row["InvoiceVoucherID"].ToString();
								dataRow2["paymentFullName"] = text2 + ":" + text4 + "-" + text5;
								dataRow2["invoiceFullName"] = text3 + ":" + text6 + "-" + text7;
								dataSet3.Tables[0].Rows.Add(dataRow2);
							}
							string text8 = (from x in (from DataRow row in dataSet3.Tables[0].Rows
									select (row)).ToList()
								select $"({x.ItemArray[0]},{x.ItemArray[1]},{x.ItemArray[2]}),").Aggregate((string x, string y) => x + y).TrimEnd(',');
							text8 = text8.Remove(1, 1);
							if (text8 != null && !flag2)
							{
								throw new CompanyException(("You are not allowed to edit this transaction .Transaction is allocated with this vouchers :\n" + text8 + "\n") ?? "", 50001);
							}
						}
						else if (result3 > 0)
						{
							_ = 3;
						}
					}
					DeleteAPAllocation(result3, sqlTransaction);
					exp = "DELETE FROM AP_Payment_Allocation WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "') OR (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
					flag &= Delete(exp, sqlTransaction);
				}
				exp = "DELETE FROM APJOURNAL WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "DELETE FROM AP_Payment_Advice WHERE PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "'";
				return flag & Delete(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertPaymentAllocation(APJournalData journalData)
		{
			bool flag = true;
			try
			{
				decimal num = default(decimal);
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = "";
				string text3 = "";
				text2 = journalData.PaymentAllocationTable.Rows[0]["PaymentSysDocID"].ToString();
				text3 = journalData.PaymentAllocationTable.Rows[0]["PaymentVoucherID"].ToString();
				decimal result = default(decimal);
				decimal.TryParse(journalData.PaymentAllocationTable.Rows[0]["UnAllocatedAmount"].ToString(), out result);
				string text4 = "";
				if (journalData.PaymentAllocationTable.Rows[0]["PaymentAPID"] != DBNull.Value)
				{
					text4 = journalData.PaymentAllocationTable.Rows[0]["PaymentAPID"].ToString();
				}
				string text5 = journalData.PaymentAllocationTable.Rows[0]["VendorID"].ToString();
				string text6 = "SELECT * FROM APJournal WHERE SysDocID='" + text2 + "' AND VoucherID='" + text3 + "' AND VendorID = '" + text5 + "'";
				if (text4 != "")
				{
					text6 = text6 + " AND APID = " + text4;
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "APJournal", text6, sqlTransaction);
				DataRow dataRow = dataSet.Tables["APJournal"].Rows[0];
				decimal result2 = default(decimal);
				if (dataRow["DebitFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["DebitFC"].ToString(), out result2);
				}
				else
				{
					decimal.TryParse(dataRow["Debit"].ToString(), out result2);
				}
				result2 = Math.Round(result2, currencyDecimalPoints);
				decimal result3 = 1m;
				decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result3);
				text6 = "SELECT SUM(ISNULL(PaymentAmountFC,PaymentAmount)) AS AllocatedAmount FROM AP_Payment_Allocation\r\n                                WHERE PaymentSysDocID = '" + text2 + "' AND PaymentVoucherID = '" + text3 + "'";
				if (text4 != "")
				{
					text6 = text6 + " AND PaymentAPID = " + text4;
				}
				decimal result4 = default(decimal);
				object obj = ExecuteScalar(text6, sqlTransaction);
				if (obj != null)
				{
					decimal.TryParse(obj.ToString(), out result4);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				string text7 = "";
				text7 = dataRow["CurrencyID"].ToString();
				if (text7 == "")
				{
					text7 = baseCurrencyID;
				}
				DataRow dataRow2 = journalData.Tables["Payment_Allocation_Batch"].NewRow();
				dataRow2["BatchDate"] = DateTime.Now;
				dataRow2["PartyType"] = "V";
				dataRow2["PartyID"] = dataRow["VendorID"];
				dataRow2.EndEdit();
				journalData.Tables["Payment_Allocation_Batch"].Rows.Add(dataRow2);
				SqlCommand insertUpdateAllocationBatchCommand = GetInsertUpdateAllocationBatchCommand(isUpdate: false);
				insertUpdateAllocationBatchCommand.Transaction = sqlTransaction;
				if (journalData.Tables["Payment_Allocation_Batch"].Rows.Count > 0)
				{
					flag &= Insert(journalData, "Payment_Allocation_Batch", insertUpdateAllocationBatchCommand);
				}
				object insertedRowIdentity = GetInsertedRowIdentity("Payment_Allocation_Batch", insertUpdateAllocationBatchCommand);
				int num2 = -1;
				if (insertedRowIdentity != null && insertedRowIdentity.ToString() != "")
				{
					num2 = int.Parse(insertedRowIdentity.ToString());
				}
				decimal num3 = default(decimal);
				decimal d = default(decimal);
				foreach (DataRow row in journalData.PaymentAllocationTable.Rows)
				{
					string text8 = row["InvoiceSysDocID"].ToString();
					string text9 = row["InvoiceVoucherID"].ToString();
					string text10 = row["APJournalID"].ToString();
					text = text8;
					if (row["PaymentAPID"] == DBNull.Value)
					{
						throw new CompanyException("Payment APID is not set.");
					}
					text6 = "SELECT ISNULL(SUM(ISNULL(APJ.Credit,0))-SUM(ISNULL(PaymentAmount,0))-SUM(ISNULL(DiscountAmount,0)),0) AS UnpaidAmount,\r\n                            ISNULL(SUM(ISNULL(APJ.CreditFC,0))-SUM(ISNULL(PaymentAmountFC,0))-SUM(ISNULL(DiscountAmountFC,0)),0) AS UnpaidAmountFC\r\n                            FROM APJournal APJ \r\n                             LEFT OUTER JOIN AP_Payment_Allocation PA ON APJ.APID=PA.APJournalID\r\n                            WHERE  SysDocID='" + text8 + "' AND  VoucherID='" + text9 + "' AND APJ.VendorID='" + text5 + "' AND APID = " + text10;
					DataSet dataSet2 = new DataSet();
					FillDataSet(dataSet2, "AP_Payment", text6, sqlTransaction);
					decimal d2 = default(decimal);
					decimal d3 = default(decimal);
					if (dataSet2.Tables[0].Rows[0]["UnpaidAmount"] != DBNull.Value)
					{
						d2 = decimal.Parse(dataSet2.Tables[0].Rows[0]["UnpaidAmount"].ToString());
					}
					if (dataSet2.Tables[0].Rows[0]["UnpaidAmountFC"] != DBNull.Value)
					{
						d3 = decimal.Parse(dataSet2.Tables[0].Rows[0]["UnpaidAmountFC"].ToString());
					}
					d2 = Math.Round(d2, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					d3 = Math.Round(d3, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal.TryParse(row["PaymentAmount"].ToString(), out result5);
					decimal.TryParse(row["DiscountAmount"].ToString(), out result6);
					d += result5;
					num3 += result6;
					string text11 = "";
					text6 = "SELECT CurrencyID FROM APJournal WHERE SysDocID='" + text8 + "' AND VoucherID='" + text9 + "'";
					obj = ExecuteScalar(text6);
					if (obj != null)
					{
						text11 = obj.ToString();
					}
					if (text11 == "")
					{
						text11 = baseCurrencyID;
					}
					row["BatchID"] = num2;
					if (string.IsNullOrEmpty(text7) || text7 == baseCurrencyID)
					{
						if (result5 + result6 > d2)
						{
							throw new CompanyException("Payment amount cannot be greater than balance amount.");
						}
					}
					else if (text11 == baseCurrencyID)
					{
						string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(text7);
						decimal num4 = default(decimal);
						num4 = ((!(currencyRateType == "M")) ? (d2 * result3) : (d2 / result3));
						num4 = Math.Round(num4, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						if (result5 + result6 > num4)
						{
							throw new CompanyException("Payment amount cannot be greater than balance amount.");
						}
					}
					else if (result5 + result6 > d3)
					{
						throw new CompanyException("Payment amount cannot be greater than balance amount.");
					}
					decimal num5 = 1m;
					if (text11 != baseCurrencyID)
					{
						text6 = "SELECT CurrencyRate FROM APJournal WHERE SysDocID='" + text8 + "' AND VoucherID='" + text9 + "'";
						obj = ExecuteScalar(text6);
						if (obj != null && obj.ToString() != "")
						{
							num5 = decimal.Parse(obj.ToString());
						}
					}
					if (text7 == "")
					{
						text7 = baseCurrencyID;
					}
					if (text11 == text7)
					{
						row["PaymentAmountFC"] = row["PaymentAmount"];
						row["DiscountAmountFC"] = row["DiscountAmount"];
						if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
						{
							row["PaymentAmount"] = Math.Round(result5 * result3, currencyDecimalPoints);
							row["DiscountAmount"] = Math.Round(result6 * result3, currencyDecimalPoints);
						}
						else
						{
							row["PaymentAmount"] = Math.Round(result5 / result3, currencyDecimalPoints);
							row["DiscountAmount"] = Math.Round(result6 / result3, currencyDecimalPoints);
						}
						if (text7 != baseCurrencyID)
						{
							num = default(decimal);
							if (num5 != result3 && num5 != result3)
							{
								if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
								{
									num += result5 * result3 - result5 * num5;
								}
								else
								{
									num += result5 / result3 - result5 / num5;
								}
								row["RealizedGainLoss"] = num;
							}
						}
					}
					else if (text11 == baseCurrencyID)
					{
						if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
						{
							row["PaymentAmountFC"] = row["PaymentAmount"];
							row["PaymentAmount"] = Math.Round(result5 * result3, currencyDecimalPoints);
							row["DiscountAmountFC"] = row["DiscountAmount"];
							row["DiscountAmount"] = Math.Round(result6 * result3, currencyDecimalPoints);
						}
						else
						{
							row["PaymentAmountFC"] = row["PaymentAmount"];
							row["PaymentAmount"] = Math.Round(result5 / result3, currencyDecimalPoints);
							row["DiscountAmountFC"] = row["DiscountAmount"];
							row["DiscountAmount"] = Math.Round(result6 / result3, currencyDecimalPoints);
						}
					}
					else
					{
						if (!(text7 == baseCurrencyID))
						{
							throw new CompanyException("Allocation can be done for same currency or base currency only.");
						}
						if (new Currencies(base.DBConfig).GetCurrencyRateType(text11) == "M")
						{
							row["PaymentAmountFC"] = Math.Round(result5 / num5, currencyDecimalPoints);
						}
						else
						{
							row["PaymentAmountFC"] = Math.Round(result5 * num5, currencyDecimalPoints);
						}
					}
					if (text11 == baseCurrencyID && text7 == baseCurrencyID)
					{
						row["PaymentAmountFC"] = DBNull.Value;
					}
				}
				if (d + result4 > result2)
				{
					throw new CompanyException("This payment is already allocated to other invoices. The allocated amount in this allocated is more than the payment amount.");
				}
				insertUpdateAllocationBatchCommand = GetInsertUpdatePaymentAllocationCommand(isUpdate: false);
				insertUpdateAllocationBatchCommand.Transaction = sqlTransaction;
				if (journalData.Tables["AP_Payment_Allocation"].Rows.Count > 0)
				{
					flag &= Insert(journalData, "AP_Payment_Allocation", insertUpdateAllocationBatchCommand);
				}
				DataRow dataRow4 = journalData.PaymentAllocationTable.Rows[0];
				insertedRowIdentity = GetInsertedRowIdentity("ARJournal", insertUpdateAllocationBatchCommand);
				int num6 = -1;
				if (insertedRowIdentity != null && insertedRowIdentity.ToString() != "")
				{
					num6 = int.Parse(insertedRowIdentity.ToString());
				}
				num = default(decimal);
				foreach (DataRow row2 in journalData.PaymentAllocationTable.Rows)
				{
					string text12 = row2["InvoiceSysDocID"].ToString();
					string text13 = row2["InvoiceVoucherID"].ToString();
					decimal result7 = default(decimal);
					decimal.TryParse(row2["PaymentAmountFC"].ToString(), out result7);
					string text14 = "";
					text6 = "SELECT CurrencyID FROM APJournal WHERE SysDocID='" + text12 + "' AND VoucherID='" + text13 + "'";
					obj = ExecuteScalar(text6);
					if (obj != null)
					{
						text14 = obj.ToString();
					}
					if (text14 == "")
					{
						text14 = baseCurrencyID;
					}
					decimal num7 = 1m;
					if (text14 != baseCurrencyID)
					{
						text6 = "SELECT CurrencyRate FROM APJournal WHERE SysDocID='" + text12 + "' AND VoucherID='" + text13 + "'";
						obj = ExecuteScalar(text6);
						if (obj != null && obj.ToString() != "")
						{
							num7 = decimal.Parse(obj.ToString());
						}
					}
					if (text7 != baseCurrencyID && text7 == text14 && num7 != result3)
					{
						_ = num7 - result3;
						if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
						{
							num += result7 * result3 - result7 * num7;
						}
						else
						{
							num += result7 / result3 - result7 / num7;
						}
					}
				}
				new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", text2, sqlTransaction);
				SysDocTypes sysDocTypes = SysDocTypes.None;
				if (text2 == "SYS_017")
				{
					sysDocTypes = SysDocTypes.PurchasePrepaymentApplied;
				}
				else if (text2 != "SYS_011")
				{
					sysDocTypes = (SysDocTypes)byte.Parse(new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", text2, sqlTransaction).ToString());
				}
				if (num != 0m && sysDocTypes != SysDocTypes.TRApplication)
				{
					GLData gLData = new GLData();
					string value = new Databases(base.DBConfig).GetFieldValue("Journal", "JournalID", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString();
					text5 = dataRow["VendorID"].ToString();
					string text15 = new Databases(base.DBConfig).GetFieldValue("Vendor", "APAccountID", "VendorID", text5, sqlTransaction).ToString();
					string text16 = "";
					int num8 = 1;
					text16 = new Databases(base.DBConfig).GetFieldValue("System_Document", "DivisionID", "SysDocID", text2, sqlTransaction).ToString();
					if (text15 == "")
					{
						string textCommand = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text5 + "'\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text + "'";
						DataSet dataSet3 = new DataSet();
						FillDataSet(dataSet3, "Accounts", textCommand, sqlTransaction);
						if (dataSet3 == null || dataSet3.Tables.Count == 0 || dataSet3.Tables[0].Rows.Count == 0)
						{
							throw new CompanyException("There is no location assigned to this system document or location record is missing.");
						}
						text15 = dataSet3.Tables["Accounts"].Rows[0]["APAccountID"].ToString();
					}
					DataRow dataRow5 = gLData.JournalTable.NewRow();
					dataRow5["JournalID"] = 0;
					dataRow5["JournalDate"] = dataRow4["AllocationDate"];
					dataRow5["SysDocID"] = "SYS_VAL";
					dataRow5["SysDocType"] = (byte)49;
					dataRow5["VoucherID"] = num2;
					dataRow5["CurrencyID"] = baseCurrencyID;
					dataRow5["CurrencyRate"] = 1;
					dataRow5["Narration"] = "Exchange Gain/Loss";
					dataRow5.EndEdit();
					gLData.JournalTable.Rows.Add(dataRow5);
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6["AccountID"] = text15;
					dataRow6["SysDocID"] = text2;
					dataRow6["VoucherID"] = text3;
					dataRow6["Description"] = "Currency Exchange Gain/Loss";
					dataRow6["CompanyID"] = num8;
					dataRow6["DivisionID"] = text16;
					dataRow6["PayeeID"] = dataRow["VendorID"].ToString();
					dataRow6["PayeeType"] = "V";
					dataRow6["IsARAP"] = true;
					dataRow6["JournalID"] = value;
					dataRow6["AllocationID"] = num2;
					dataRow6["DebitFC"] = DBNull.Value;
					dataRow6["CreditFC"] = DBNull.Value;
					if (num < 0m)
					{
						dataRow6["Debit"] = Math.Abs(num);
						dataRow6["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = Math.Abs(num);
					}
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
					string text17 = "";
					text6 = "SELECT ExchangeGainLossAccountID FROM Location\r\n                                        INNER JOIN System_Document SysDoc ON Location.LocationID=SysDoc.LocationID\r\n                                        WHERE SysDocID='" + text2 + "'";
					object obj3 = ExecuteScalar(text6, sqlTransaction);
					if (obj3 != null && obj3.ToString() != "")
					{
						text17 = obj3.ToString();
					}
					if (text17 == "")
					{
						throw new CompanyException("There is an exchange gain/loss on this transaction. Currency Exchange Gain/Loss account is not set for location.");
					}
					dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6["AccountID"] = text17;
					dataRow6["SysDocID"] = text2;
					dataRow6["VoucherID"] = text3;
					dataRow6["Description"] = "Gain/Loss on Payment No:" + text2 + "-" + text3;
					dataRow6["CompanyID"] = num8;
					dataRow6["DivisionID"] = text16;
					dataRow6["PayeeID"] = dataRow["VendorID"].ToString();
					dataRow6["PayeeType"] = "V";
					dataRow6["IsARAP"] = false;
					dataRow6["AllocationID"] = num2;
					dataRow6["JournalID"] = value;
					dataRow6["CreditFC"] = DBNull.Value;
					dataRow6["Credit"] = DBNull.Value;
					if (num < 0m)
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = Math.Abs(num);
					}
					else
					{
						dataRow6["Debit"] = Math.Abs(num);
						dataRow6["Credit"] = DBNull.Value;
					}
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData, isUpdate: false, sqlTransaction);
				}
				if (num3 != 0m)
				{
					GLData gLData2 = new GLData();
					string value2 = new Databases(base.DBConfig).GetFieldValue("Journal", "JournalID", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString();
					text5 = dataRow["VendorID"].ToString();
					string text18 = new Databases(base.DBConfig).GetFieldValue("Vendor", "APAccountID", "VendorID", text5, sqlTransaction).ToString();
					string text19 = "";
					int num9 = 1;
					text19 = new Databases(base.DBConfig).GetFieldValue("System_Document", "DivisionID", "SysDocID", text2, sqlTransaction).ToString();
					if (text18 == "")
					{
						string textCommand2 = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text5 + "'\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text + "'";
						DataSet dataSet4 = new DataSet();
						FillDataSet(dataSet4, "Accounts", textCommand2, sqlTransaction);
						if (dataSet4 == null || dataSet4.Tables.Count == 0 || dataSet4.Tables[0].Rows.Count == 0)
						{
							throw new CompanyException("There is no location assigned to this system document or location record is missing.");
						}
						text18 = dataSet4.Tables["Accounts"].Rows[0]["APAccountID"].ToString();
					}
					DataRow dataRow7 = gLData2.JournalTable.NewRow();
					dataRow7["JournalID"] = 0;
					dataRow7["JournalDate"] = dataRow4["AllocationDate"];
					dataRow7["SysDocID"] = "SYS_VDS";
					dataRow7["SysDocType"] = (byte)237;
					dataRow7["VoucherID"] = num2;
					dataRow7["CurrencyID"] = baseCurrencyID;
					dataRow7["CurrencyRate"] = 1;
					dataRow7["Narration"] = "Allocation Discount";
					dataRow7.EndEdit();
					gLData2.JournalTable.Rows.Add(dataRow7);
					DataRow dataRow8 = gLData2.JournalDetailsTable.NewRow();
					dataRow8["AccountID"] = text18;
					dataRow8["SysDocID"] = text2;
					dataRow8["VoucherID"] = text3;
					dataRow8["Description"] = "Allocation Discount";
					dataRow8["CompanyID"] = num9;
					dataRow8["DivisionID"] = text19;
					dataRow8["PayeeID"] = dataRow["VendorID"].ToString();
					dataRow8["PayeeType"] = "V";
					dataRow8["IsARAP"] = true;
					dataRow8["JournalID"] = value2;
					dataRow8["AllocationID"] = num2;
					dataRow8["DebitFC"] = DBNull.Value;
					dataRow8["CreditFC"] = DBNull.Value;
					if (num3 < 0m)
					{
						dataRow8["Debit"] = DBNull.Value;
						dataRow8["Credit"] = Math.Abs(num3);
					}
					else
					{
						dataRow8["Debit"] = Math.Abs(num3);
						dataRow8["Credit"] = DBNull.Value;
					}
					gLData2.JournalDetailsTable.Rows.Add(dataRow8);
					string text20 = "";
					text6 = "SELECT AllocationDiscountAccountID FROM Location\r\n                                        INNER JOIN System_Document SysDoc ON Location.LocationID=SysDoc.LocationID\r\n                                        WHERE SysDocID='" + text2 + "'";
					object obj4 = ExecuteScalar(text6, sqlTransaction);
					if (obj4 != null && obj4.ToString() != "")
					{
						text20 = obj4.ToString();
					}
					if (text20 == "")
					{
						throw new CompanyException("There is a discount on this transaction. Discount Allocation account is not set for location.");
					}
					dataRow8 = gLData2.JournalDetailsTable.NewRow();
					dataRow8["AccountID"] = text20;
					dataRow8["SysDocID"] = text2;
					dataRow8["VoucherID"] = text3;
					dataRow8["CompanyID"] = num9;
					dataRow8["DivisionID"] = text19;
					dataRow8["Description"] = "Discount on Payment No:" + text2 + "-" + text3;
					dataRow8["PayeeID"] = dataRow["VendorID"].ToString();
					dataRow8["PayeeType"] = "V";
					dataRow8["IsARAP"] = false;
					dataRow8["AllocationID"] = num2;
					dataRow8["JournalID"] = value2;
					dataRow8["CreditFC"] = DBNull.Value;
					dataRow8["Credit"] = DBNull.Value;
					if (num3 < 0m)
					{
						dataRow8["Debit"] = Math.Abs(num3);
						dataRow8["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow8["Debit"] = DBNull.Value;
						dataRow8["Credit"] = Math.Abs(num3);
					}
					gLData2.JournalDetailsTable.Rows.Add(dataRow8);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData2, isUpdate: false, sqlTransaction);
				}
				if (result != 0m)
				{
					GLData gLData3 = new GLData();
					string value3 = new Databases(base.DBConfig).GetFieldValue("Journal", "JournalID", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString();
					text5 = dataRow["VendorID"].ToString();
					string text21 = new Databases(base.DBConfig).GetFieldValue("Vendor", "APAccountID", "VendorID", text5, sqlTransaction).ToString();
					string text22 = "";
					int num10 = 1;
					text22 = new Databases(base.DBConfig).GetFieldValue("System_Document", "DivisionID", "SysDocID", text2, sqlTransaction).ToString();
					if (text21 == "")
					{
						string textCommand3 = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text5 + "'\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text + "'";
						DataSet dataSet5 = new DataSet();
						FillDataSet(dataSet5, "Accounts", textCommand3, sqlTransaction);
						if (dataSet5 == null || dataSet5.Tables.Count == 0 || dataSet5.Tables[0].Rows.Count == 0)
						{
							throw new CompanyException("There is no location assigned to this system document or location record is missing.");
						}
						text21 = dataSet5.Tables["Accounts"].Rows[0]["APAccountID"].ToString();
					}
					DataRow dataRow9 = gLData3.JournalTable.NewRow();
					dataRow9["JournalID"] = 0;
					dataRow9["JournalDate"] = dataRow4["AllocationDate"];
					dataRow9["SysDocID"] = "SYS_VUA";
					dataRow9["SysDocType"] = (byte)237;
					dataRow9["VoucherID"] = num2;
					dataRow9["CurrencyID"] = baseCurrencyID;
					dataRow9["CurrencyRate"] = 1;
					dataRow9["Narration"] = "Write-off UnAllocated Amount";
					dataRow9.EndEdit();
					gLData3.JournalTable.Rows.Add(dataRow9);
					DataRow dataRow10 = gLData3.JournalDetailsTable.NewRow();
					dataRow10["AccountID"] = text21;
					dataRow10["SysDocID"] = text2;
					dataRow10["VoucherID"] = text3;
					dataRow10["CompanyID"] = num10;
					dataRow10["DivisionID"] = text22;
					dataRow10["Description"] = "Write-off UnAllocated Amount";
					dataRow10["PayeeID"] = dataRow["VendorID"].ToString();
					dataRow10["PayeeType"] = "V";
					dataRow10["IsARAP"] = true;
					dataRow10["JournalID"] = value3;
					dataRow10["AllocationID"] = num2;
					dataRow10["DebitFC"] = DBNull.Value;
					dataRow10["CreditFC"] = DBNull.Value;
					dataRow10["Debit"] = DBNull.Value;
					dataRow10["Credit"] = Math.Abs(result);
					gLData3.JournalDetailsTable.Rows.Add(dataRow10);
					string text23 = "";
					text6 = "SELECT AllocationDiscountAccountID FROM Location\r\n                                        INNER JOIN System_Document SysDoc ON Location.LocationID=SysDoc.LocationID\r\n                                        WHERE SysDocID='" + text2 + "'";
					object obj5 = ExecuteScalar(text6, sqlTransaction);
					if (obj5 != null && obj5.ToString() != "")
					{
						text23 = obj5.ToString();
					}
					if (text23 == "")
					{
						throw new CompanyException("There is a discount on this transaction. Discount Allocation account is not set for location.");
					}
					dataRow10 = gLData3.JournalDetailsTable.NewRow();
					dataRow10["AccountID"] = text23;
					dataRow10["SysDocID"] = text2;
					dataRow10["VoucherID"] = text3;
					dataRow10["CompanyID"] = num10;
					dataRow10["DivisionID"] = text22;
					dataRow10["Description"] = "Write-off UnAllocated Amount on Payment No:" + text2 + "-" + text3;
					dataRow10["PayeeID"] = dataRow["VendorID"].ToString();
					dataRow10["PayeeType"] = "V";
					dataRow10["IsARAP"] = false;
					dataRow10["AllocationID"] = num2;
					dataRow10["JournalID"] = value3;
					dataRow10["CreditFC"] = DBNull.Value;
					dataRow10["Credit"] = DBNull.Value;
					dataRow10["Debit"] = Math.Abs(result);
					dataRow10["Credit"] = DBNull.Value;
					gLData3.JournalDetailsTable.Rows.Add(dataRow10);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData3, isUpdate: false, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowInsertUpdateInfo("AP_Payment_Allocation", "AllocationID", num6, sqlTransaction, isInsert: true);
				flag &= AddActivityLog("Vendor Payment Allocation", text5, ActivityTypes.Add, sqlTransaction);
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

		public bool InsertPaymentAdviceTRApplication(DataSet journalData)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				string text2 = "";
				text = journalData.Tables["AP_Payment_Advice"].Rows[0]["PaymentSysDocID"].ToString();
				text2 = journalData.Tables["AP_Payment_Advice"].Rows[0]["PaymentVoucherID"].ToString();
				string commandText = "DELETE FROM AP_Payment_Advice WHERE PaymentSysDocID='" + text + "' AND PaymentVoucherID='" + text2 + "'";
				flag &= Delete(commandText, sqlTransaction);
				SqlCommand insertUpdatePaymentAdviceCommand = GetInsertUpdatePaymentAdviceCommand(isUpdate: false);
				insertUpdatePaymentAdviceCommand.Transaction = sqlTransaction;
				if (journalData.Tables["AP_Payment_Advice"].Rows.Count > 0)
				{
					journalData.Tables["AP_Payment_Advice"].AcceptChanges();
					foreach (DataRow row in journalData.Tables["AP_Payment_Advice"].Rows)
					{
						row.SetAdded();
					}
					flag &= Insert(journalData, "AP_Payment_Advice", insertUpdatePaymentAdviceCommand);
				}
				commandText = "SELECT APID FROM APJournal WHERE SysDocID = '" + text + "' AND VoucherID = '" + text2 + "'";
				object obj = ExecuteScalar(commandText, sqlTransaction);
				int num = 0;
				if (!obj.IsNullOrEmpty())
				{
					num = int.Parse(obj.ToString());
				}
				APJournalData aPJournalData = new APJournalData();
				foreach (DataRow row2 in journalData.Tables["AP_Payment_Advice"].Rows)
				{
					DataRow dataRow2 = null;
					dataRow2 = aPJournalData.Tables["AP_Payment_Allocation"].NewRow();
					dataRow2["InvoiceSysDocID"] = row2["InvoiceSysDocID"];
					dataRow2["InvoiceVoucherID"] = row2["InvoiceVoucherID"];
					dataRow2["PaymentSysDocID"] = row2["PaymentSysDocID"];
					dataRow2["PaymentVoucherID"] = row2["PaymentVoucherID"];
					dataRow2["CurrencyID"] = row2["CurrencyID"];
					dataRow2["VendorID"] = row2["VendorID"];
					dataRow2["APJournalID"] = row2["APJournalID"];
					dataRow2["PaymentAPID"] = num;
					dataRow2["CurrencyRate"] = row2["CurrencyRate"];
					dataRow2["AllocationDate"] = row2["AllocationDate"];
					dataRow2["PaymentAmount"] = row2["PaymentAmount"];
					dataRow2.EndEdit();
					aPJournalData.Tables["AP_Payment_Allocation"].Rows.Add(dataRow2);
				}
				flag &= new APJournal(base.DBConfig).InsertPaymentAllocation(aPJournalData);
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

		public bool InsertPaymentAdvice(DataSet journalData)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				string text2 = "";
				text = journalData.Tables["AP_Payment_Advice"].Rows[0]["PaymentSysDocID"].ToString();
				text2 = journalData.Tables["AP_Payment_Advice"].Rows[0]["PaymentVoucherID"].ToString();
				string commandText = "DELETE FROM AP_Payment_Advice WHERE PaymentSysDocID='" + text + "' AND PaymentVoucherID='" + text2 + "'";
				flag &= Delete(commandText, sqlTransaction);
				SqlCommand insertUpdatePaymentAdviceCommand = GetInsertUpdatePaymentAdviceCommand(isUpdate: false);
				insertUpdatePaymentAdviceCommand.Transaction = sqlTransaction;
				if (journalData.Tables["AP_Payment_Advice"].Rows.Count > 0)
				{
					journalData.Tables["AP_Payment_Advice"].AcceptChanges();
					foreach (DataRow row in journalData.Tables["AP_Payment_Advice"].Rows)
					{
						row.SetAdded();
					}
					flag &= Insert(journalData, "AP_Payment_Advice", insertUpdatePaymentAdviceCommand);
				}
				commandText = "SELECT APID FROM APJournal WHERE SysDocID = '" + text + "' AND VoucherID = '" + text2 + "'";
				object obj = ExecuteScalar(commandText, sqlTransaction);
				int num = 0;
				if (!obj.IsNullOrEmpty())
				{
					num = int.Parse(obj.ToString());
				}
				APJournalData aPJournalData = new APJournalData();
				foreach (DataRow row2 in journalData.Tables["AP_Payment_Advice"].Rows)
				{
					DataRow dataRow2 = null;
					dataRow2 = aPJournalData.Tables["AP_Payment_Allocation"].NewRow();
					dataRow2["InvoiceSysDocID"] = row2["InvoiceSysDocID"];
					dataRow2["InvoiceVoucherID"] = row2["InvoiceVoucherID"];
					dataRow2["PaymentSysDocID"] = row2["PaymentSysDocID"];
					dataRow2["PaymentVoucherID"] = row2["PaymentVoucherID"];
					dataRow2["CurrencyID"] = row2["CurrencyID"];
					dataRow2["VendorID"] = row2["VendorID"];
					dataRow2["APJournalID"] = row2["APJournalID"];
					dataRow2["PaymentAPID"] = num;
					dataRow2["CurrencyRate"] = row2["CurrencyRate"];
					dataRow2["AllocationDate"] = row2["AllocationDate"];
					dataRow2["PaymentAmount"] = row2["PaymentAmount"];
					dataRow2.EndEdit();
					aPJournalData.Tables["AP_Payment_Allocation"].Rows.Add(dataRow2);
				}
				flag &= new APJournal(base.DBConfig).InsertPaymentAllocation(aPJournalData);
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

		internal bool DeleteDetailsRows(SqlTransaction sqlTransaction, string journalID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Journal_Details WHERE JournalID = '" + journalID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public int UpdateFCAmountConversion()
		{
			int num = 0;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string baseCurrencyID = GetBaseCurrencyID();
				string exp = "UPDATE APJ SET ConRate =   \r\n                                     CASE WHEN  (ISNULL(APJ.CurrencyID,'" + baseCurrencyID + "') <> V.CurrencyID) \r\n                                    THEN\r\n                                      ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = V.CurrencyID AND Ex.RateUpdatedDate < APJ.APDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = V.CurrencyID)) END\r\n                                       ,\r\n                                    ConDebitFC = CASE WHEN (Debit IS NOT NULL AND ISNULL(APJ.CurrencyID,'" + baseCurrencyID + "') <> V.CurrencyID) \r\n                                    THEN\r\n                                    CASE (SELECT DISTINCT RateType FROM Currency C WHERE C.CurrencyID = V.CurrencyID) WHEN 'M' \r\n                                    THEN (Debit / ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = V.CurrencyID AND Ex.RateUpdatedDate < APJ.APDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = V.CurrencyID)))\r\n                                    ELSE (Debit * ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = V.CurrencyID AND Ex.RateUpdatedDate < APJ.APDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = V.CurrencyID))) \r\n                                     END END   ,\r\n\r\n \r\n                                    ConCreditFC = CASE WHEN Credit IS NOT NULL AND (ISNULL(APJ.CurrencyID,'" + baseCurrencyID + "') <> V.CurrencyID) \r\n                                    THEN\r\n                                    CASE (SELECT DISTINCT RateType FROM Currency C WHERE C.CurrencyID = V.CurrencyID) WHEN 'M' \r\n                                    THEN (Credit / ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = V.CurrencyID AND Ex.RateUpdatedDate < APJ.APDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = V.CurrencyID)))\r\n                                    ELSE (Credit * ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = V.CurrencyID AND Ex.RateUpdatedDate < APJ.APDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = V.CurrencyID))) \r\n                                     END END   \r\n\r\n  \r\n                                    FROM APJournal APJ INNER JOIN Vendor V ON V.VendorID = APJ.VendorID \r\n                                    WHERE  ISNULL(APJ.CurrencyID,'" + baseCurrencyID + "') <> V.CurrencyID AND APJ.SysDocID <> 'SYS_VAL'";
				num = ExecuteNonQuery(exp, sqlTransaction);
				return num;
			}
			catch
			{
				num = 0;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(num >= 0);
			}
		}

		public GLData GetJournalVoucherByID(string sysDocID, string voucherID)
		{
			try
			{
				GLData gLData = new GLData();
				string textCommand = "SELECT * FROM JOURNAL WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(gLData, "Journal", textCommand);
				if (gLData == null || gLData.Tables.Count == 0 || gLData.Tables["Journal"].Rows.Count == 0)
				{
					return null;
				}
				string str = gLData.JournalTable.Rows[0]["JournalID"].ToString();
				textCommand = "SELECT JD.*,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM JOURNAL_DETAILS JD LEFt OUTER JOIN \r\n                        Account ON JD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON JD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID  \r\n                        WHERE JournalID=" + str;
				FillDataSet(gLData, "Journal_Details", textCommand);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		private int GetNextJournalID(SqlTransaction sqlTransaction)
		{
			string exp = "SELECT Max(JournalID) FROM Journal";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj != DBNull.Value)
			{
				return int.Parse(obj.ToString()) + 1;
			}
			return 0;
		}

		internal string GetBaseCurrencyID()
		{
			string exp = "SELECT TOP 1 BaseCurrencyID FROM Company";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public DataSet GetUnallocatedInvoices(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT APID JournalID,APJ.SysDocID,APJ.VoucherID,APJ.Reference,APJ.VendorID,APDate,ISNULL(APDueDate,APDate) AS APDueDate,ISNULL(ISNULL(CreditFC,Credit),0) AS OriginalAmount,\r\n                                ISNULL(APJ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, \r\n                                ISNULL(APJ.CurrencyRate,1) AS CurrencyRate,  \r\n                                ISNULL(ISNULL(ISNULL(CreditFC,Credit),0) -   \r\n                                (SELECT CASE WHEN APJ.CurrencyID IS NULL  OR APJ.CurrencyID=(SELECT CurrencyID FROM Currency WHERE IsBase='True') \r\n                                THEN SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)) END \r\n                                FROM AP_Payment_Allocation APP\r\n                                WHERE APJ.APID=APP.APJournalID),ISNULL(ISNULL(CreditFC,Credit),0)) AS AmountDue,\r\n                                (SELECT SUM(ISNULL(PaymentAmountFC,PaymentAmount)) FROM AP_Payment_Allocation APP INNER JOIN Purchase_Prepayment_Invoice PPI ON APP.InvoiceSysDocID = PPI.SysDocID AND APP.InvoiceVoucherID = PPI.VoucherID\r\n                                WHERE PPI.InvoiceSysDocID = APJ.SysDocID AND PPI.InvoiceVoucherID = APJ.VoucherID) AS Prepayment,\r\n                                ISNULL(ISNULL(Credit,0) -   \r\n                                ISNULL((SELECT SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0))  FROM AP_Payment_Allocation APP\r\n                                WHERE APJ.APID = APP.APJournalID),0),0)  AS AmountDueBase, (select ISNULL(DiscountAmount,0) from AP_Payment_Allocation ARP WHERE APJ.JournalID = ARP.APJournalID) AS Discount\r\n                                FROM APJournal APJ   \r\n                                LEFT OUTER JOIN Purchase_Invoice PI ON PI.SysDocID=APJ.SysDocID AND PI.VoucherID=APJ.VoucherID\r\n                                WHERE ISNULL(Credit,0)>0 AND ISNULL(APJ.IsVoid,'False')='False' AND APJ.SysDocID NOT IN('SYS_VAL','SYS_VDS','SYS_VUA')  AND   APJ.VendorID = '" + vendorID + "'\r\n                                AND ISNULL(APJ.ExcludeInPayment,'False')='False'    AND  ISNULL(PI.IsHoldForPayment,'FALSE')='FALSE'                                  \r\n                                AND \r\n                                (SELECT CASE WHEN (APJ.CurrencyID IS NULL OR APJ.CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')) THEN \r\n                                ISNULL(SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)),0) ELSE  \r\n                                ISNULL(SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)),0) END FROM AP_Payment_Allocation PA\r\n                                WHERE APJ.APID=PA.APJournalID)<\r\n                                CASE WHEN (APJ.CurrencyID IS NULL OR APJ.CurrencyID = (SELECT CurrencyID FROM Currency WHERE IsBase='True') )\r\n                                THEN ISNULL(ISNULL(Credit,0),0) ELSE ISNULL(ISNULL(CreditFC,0),0) END  ORDER BY APDate");
				FillDataSet(dataSet, "AP_Payment_Allocation", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAPPaymentToAllocate(string sysDocID, string voucherID, string vendorID, int paymentAPID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT GLT.SysDocID,VoucherID,'V' AS PayeeType,GLT.VendorID AS PayeeID, SysDocType,APID,\r\n                                Vendor.VendorName\r\n                                AS PayeeName,\r\n                                ISNULL(ISNULL(DebitFC,Debit)-ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END   \r\n\t\t\t\t\t\t\t\tFROM AP_Payment_Allocation PA WHERE PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "' AND VendorID='" + vendorID + "'  AND (PaymentAPID IS NULL  OR PaymentAPID = APID)  GROUP BY CurrencyID),0),0) AS UnAllocatedAmount,\r\n                                Debit AS Amount,DebitFC AS AmountFC,APDate AS TransactionDate,CurrencyRate,\r\n                                Reference,Description,\r\n                                ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID\r\n                                FROM APJournal GLT \r\n                                LEFt OUTER JOIN Vendor ON GLT.VendorID=Vendor.VendorID \r\n                                LEFT OUTER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n                                WHERE ISNULL(IsVoid,'False')='False' AND  GLT.SysDocID <> 'SYS_VAL' AND\r\n                                GLT.SysDocID = '" + sysDocID + "' AND VoucherID ='" + voucherID + "' AND GLT.VendorID='" + vendorID + "' ";
				if (paymentAPID > 0)
				{
					text = text + " AND APID = " + paymentAPID;
				}
				text += " ORDER BY CONVERT(DATE, APDate, 103), VoucherID ";
				FillDataSet(dataSet, "GL_Transaction", text);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUnallocatedPayments()
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT * FROM \r\n                                    (\r\n                                    SELECT SysDocID,VoucherID,APJ.VendorID,VendorName,APDate,ISNULL(APJ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, APJ.APID,\r\n                                    ISNULL(ISNULL(DebitFC,Debit),0) AS OriginalAmount,\r\n                                   ISNULL(ISNULL(DebitFC,Debit),0) -   \r\n                                ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n\t\t\t\t\t\t\t\tTHEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) +SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END FROM AP_Payment_Allocation APP\r\n                                WHERE APJ.SysDocID=APP.PaymentSysDocID AND APJ.VoucherID=APP.PaymentVoucherID AND APJ.VendorID = APP.VendorID AND APJ.APID = APP.PaymentAPID GROUP BY CurrencyID),0)  AS Unallocated\r\n                                  FROM APJournal APJ   INNER JOIN Vendor ON APJ.VendorID=Vendor.VendorID\r\n                                WHERE ISNULL(Debit,0)>0 AND APJ.SysDocID NOT IN('SYS_VDS','SYS_VUA') AND ISNULL(IsVoid,'False')='False' \r\n                                AND (SELECT ISNULL(SUM(ISNULL(PaymentAmountFC,PaymentAmount)),0) FROM AP_Payment_Allocation PA\r\n\t                                WHERE PA.PaymentSysDocID=APJ.SysDocID AND PA.PaymentVoucherID=APJ.VoucherID  AND APJ.VendorID = PA.VendorID AND APJ.APID = PA.PaymentAPID)<ISNULL(ISNULL(DebitFC,Debit),0)) ALC\r\n\t\t\t\t\t\t\t\t\tWHERE Unallocated<>0 AND SysDocID <> 'SYS_VAL' ");
				FillDataSet(dataSet, "AP_Payment_Allocation", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAPAllocationList(DateTime from, DateTime to)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				SqlCommand sqlCommand = new SqlCommand("SELECT  BatchID Batch,AllocationID ID,ARP.VendorID AS [Vendor Code],CUS.VendorName AS [Vendor Name],InvoiceVoucherID [Invoice No],SD.DocName,PaymentVoucherID [Receipt No],AllocationDate [Date],ARP.CreatedBy [Allocated By],PaymentAmount [Amount], PaymentAmountFC [Amount FC],\r\n                                    RealizedGainLoss [Gain/Loss],ARP.CurrencyID [Cur],CurrencyRate [Cur Rate]        \r\n                                    FROM dbo.AP_Payment_Allocation ARP INNER JOIN Vendor CUS\r\n                                    ON Cus.VendorID = ARP.VendorID  LEFT OUTER JOIN System_Document SD ON SD.SysDocID=ARP.PaymentSysDocID  WHERE AllocationDate BETWEEN '" + text + "' AND '" + text2 + "'");
				FillDataSet(dataSet, "AP_Payment_Allocation", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteAPAllocation(int id)
		{
			try
			{
				return DeleteAPAllocation(id, null);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteAPAllocation(int id, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			bool flag2 = true;
			try
			{
				string text = "";
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				else
				{
					flag2 = false;
				}
				int num = -1;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("AP_Payment_Allocation", "BatchID", "AllocationID", id, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					num = int.Parse(fieldValue.ToString());
				}
				text = " SELECT COUNT(*) FROM AP_Payment_Allocation PA INNER JOIN System_Document SD ON SD.SysDocID = PA.InvoiceSysDocID  \r\n                             INNER JOIN Purchase_Prepayment_Invoice PPI ON PPI.SysDocID = PA.InvoiceSysDocID AND PPI.VoucherID = PA.InvoiceVoucherID\r\n                             WHERE BatchID = " + num + " AND SD.SysDocType = 244 AND ISNULL(PPI.Status,0) <> 0";
				fieldValue = ExecuteScalar(text, sqlTransaction);
				if (!fieldValue.IsNullOrEmpty() && int.Parse(fieldValue.ToString()) > 0)
				{
					throw new CompanyException("This allocation cannot be deleted because it is in use for a prepayment.", 1065);
				}
				text = ((num <= 0) ? ("DELETE FROM AP_Payment_Allocation WHERE AllocationID= " + id.ToString()) : ("DELETE FROM AP_Payment_Allocation WHERE BatchID= " + num.ToString()));
				flag &= (ExecuteNonQuery(text) > 0);
				text = "DELETE FROM Payment_Allocation_Batch WHERE BatchID = " + num.ToString();
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (num <= 0)
				{
					if (id == 0)
					{
						return flag;
					}
					flag &= new Journal(base.DBConfig).DeleteJournal("SYS_VAL", id.ToString(), sqlTransaction);
					flag &= new Journal(base.DBConfig).DeleteJournal("SYS_VDS", id.ToString(), sqlTransaction);
					flag &= new Journal(base.DBConfig).DeleteJournal("SYS_VUA", id.ToString(), sqlTransaction);
					return flag;
				}
				flag &= new Journal(base.DBConfig).DeleteJournal("SYS_VAL", num.ToString(), sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal("SYS_VDS", num.ToString(), sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal("SYS_VUA", num.ToString(), sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (flag2)
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		public bool InsertAPJournalForPDC(GLData journalData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				DataRow dataRow = journalData.JournalTable.Rows[0];
				string sysDocID = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				if (isUpdate)
				{
					flag &= new APJournal(base.DBConfig).DeleteAPJournal(sysDocID, voucherID, sqlTransaction);
				}
				foreach (DataRow row in journalData.JournalDetailsTable.Rows)
				{
					APJournalData aPJournalData = new APJournalData();
					DataTable dataTable = aPJournalData.Tables["APJournal"];
					string vendorID = row["PayeeID"].ToString();
					DateTime invoiceDate = DateTime.Parse(dataRow["JournalDate"].ToString());
					string a = row["PayeeType"].ToString();
					bool result = false;
					bool.TryParse(row["IsARAP"].ToString(), out result);
					bool flag2 = false;
					if (row["Credit"] != DBNull.Value)
					{
						flag2 = true;
					}
					if (!((a != "V" || !result) | flag2))
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["SysDocID"] = dataRow["SysDocID"];
						dataRow3["VoucherID"] = dataRow["VoucherID"];
						dataRow3["VendorID"] = row["PayeeID"];
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3["APAccountID"] = row["AccountID"];
						dataRow3["Debit"] = row["Debit"];
						dataRow3["Credit"] = row["Credit"];
						dataRow3["DebitFC"] = row["DebitFC"];
						dataRow3["CreditFC"] = row["CreditFC"];
						dataRow3["IsPDCRow"] = true;
						dataRow3["CostCenterID"] = row["CostCenterID"];
						dataRow3["CurrencyID"] = dataRow["CurrencyID"];
						dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
						dataRow3["Description"] = row["Description"];
						dataRow3["APDate"] = dataRow["JournalDate"];
						if (row["DueDate"] != DBNull.Value)
						{
							dataRow3["APDueDate"] = row["DueDate"];
						}
						else if (flag2)
						{
							dataRow3["APDueDate"] = new Vendors(base.DBConfig).CalculateDueDate(invoiceDate, vendorID, sqlTransaction);
						}
						dataRow3["PaymentMethodType"] = row["PaymentMethodType"];
						dataRow3["BankID"] = row["BankID"];
						dataRow3["ChequeDate"] = row["CheckDate"];
						dataRow3["ChequeNumber"] = row["CheckNumber"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
						flag &= new APJournal(base.DBConfig).InsertJournal(aPJournalData, sqlTransaction);
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}
	}
}
