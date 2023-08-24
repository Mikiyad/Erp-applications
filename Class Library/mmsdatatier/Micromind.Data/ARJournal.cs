using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Micromind.Data
{
	public sealed class ARJournal : StoreObject
	{
		private const string ARJOURNAL_TABLE = "@ARJournal";

		private const string ARID_PARM = "@ARID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string REFERENCE_PARM = "@Reference";

		private const string ARACCOUNTID_PARM = "@ARAccountID";

		private const string DEBIT_PARM = "@Debit";

		private const string CREDIT_PARM = "@Credit";

		private const string DEBITFC_PARM = "@DebitFC";

		private const string CREDITFC_PARM = "@CreditFC";

		private const string CONDEBITFC_PARM = "@ConDebitFC";

		private const string CONCREDITFC_PARM = "@ConCreditFC";

		private const string CONRATE_PARM = "@ConRate";

		private const string ISPDCROW_PARM = "@IsPDCRow";

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

		private const string ARDATE_PARM = "@ARDate";

		private const string ARDUEDATE_PARM = "@ARDueDate";

		private const string ALLOCATIONID_PARM = "@AllocationID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ATTRIBUTEID1_PARM = "@AttributeID1";

		private const string ATTRIBUTEID2_PARM = "@AttributeID2";

		private const string ARARPAYMENTALLOCATION_TABLE = "@AR_Payment_Allocation";

		private const string ARJOURNALID_PARM = "@ARJournalID";

		private const string PAYMENTARID_PARM = "@PaymentARID";

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

		public ARJournal(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateARJournalText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ARJournal", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("Reference", "@Reference"), new FieldValue("IsPDCRow", "@IsPDCRow"), new FieldValue("ARAccountID", "@ARAccountID"), new FieldValue("Debit", "@Debit"), new FieldValue("Credit", "@Credit"), new FieldValue("DebitFC", "@DebitFC"), new FieldValue("CreditFC", "@CreditFC"), new FieldValue("ConDebitFC", "@ConDebitFC"), new FieldValue("ConCreditFC", "@ConCreditFC"), new FieldValue("ConRate", "@ConRate"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("ChequeNumber", "@ChequeNumber"), new FieldValue("BankID", "@BankID"), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("Description", "@Description"), new FieldValue("ARDate", "@ARDate"), new FieldValue("ARDueDate", "@ARDueDate"), new FieldValue("AllocationID", "@AllocationID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("AttributeID1", "@AttributeID1"), new FieldValue("AttributeID2", "@AttributeID2"), new FieldValue("JournalID", "@JournalID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ARJournal", new FieldValue("ARID", "@ARID", isUpdateConditionField: true));
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateARJournalCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateARJournalText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateARJournalText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			if (isUpdate)
			{
				parameters.Add("@ARID", SqlDbType.Int);
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@IsPDCRow", SqlDbType.Bit);
			parameters.Add("@ARAccountID", SqlDbType.NVarChar);
			parameters.Add("@Debit", SqlDbType.Money);
			parameters.Add("@Credit", SqlDbType.Money);
			parameters.Add("@DebitFC", SqlDbType.Money);
			parameters.Add("@CreditFC", SqlDbType.Money);
			parameters.Add("@ConDebitFC", SqlDbType.Money);
			parameters.Add("@ConCreditFC", SqlDbType.Money);
			parameters.Add("@ConRate", SqlDbType.Money);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@ChequeNumber", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@JournalID", SqlDbType.NVarChar);
			parameters.Add("@ARDate", SqlDbType.DateTime);
			parameters.Add("@ARDueDate", SqlDbType.DateTime);
			parameters.Add("@AllocationID", SqlDbType.Int);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@AttributeID1", SqlDbType.NVarChar);
			parameters.Add("@AttributeID2", SqlDbType.NVarChar);
			if (isUpdate)
			{
				parameters["@ARID"].SourceColumn = "ARID";
			}
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@IsPDCRow"].SourceColumn = "IsPDCRow";
			parameters["@ARAccountID"].SourceColumn = "ARAccountID";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@DebitFC"].SourceColumn = "DebitFC";
			parameters["@CreditFC"].SourceColumn = "CreditFC";
			parameters["@ConDebitFC"].SourceColumn = "ConDebitFC";
			parameters["@ConCreditFC"].SourceColumn = "ConCreditFC";
			parameters["@ConRate"].SourceColumn = "ConRate";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@JournalID"].SourceColumn = "JournalID";
			parameters["@ARDate"].SourceColumn = "ARDate";
			parameters["@ARDueDate"].SourceColumn = "ARDueDate";
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
			sqlBuilder.AddInsertUpdateParameters("AR_Payment_Allocation", new FieldValue("ARJournalID", "@ARJournalID"), new FieldValue("PaymentARID", "@PaymentARID"), new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("BatchID", "@BatchID"), new FieldValue("PaymentSysDocID", "@PaymentSysDocID"), new FieldValue("PaymentVoucherID", "@PaymentVoucherID"), new FieldValue("AllocationDate", "@AllocationDate"), new FieldValue("PaymentAmount", "@PaymentAmount"), new FieldValue("PaymentAmountFC", "@PaymentAmountFC"), new FieldValue("RealizedGainLoss", "@RealizedGainLoss"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("DiscountAmount", "@DiscountAmount"), new FieldValue("DiscountAmountFC", "@DiscountAmountFC"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ARJournal", new FieldValue("ARID", "@ARID", isUpdateConditionField: true));
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
			parameters.Add("@ARJournalID", SqlDbType.Int);
			parameters.Add("@PaymentARID", SqlDbType.Int);
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
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@ARJournalID"].SourceColumn = "ARJournalID";
			parameters["@PaymentARID"].SourceColumn = "PaymentARID";
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
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
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

		public bool InsertJournal(ARJournalData journalData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateARJournalCommand = GetInsertUpdateARJournalCommand(isUpdate: false);
			try
			{
				_ = journalData.Tables["ARJournal"].Rows[0];
				string baseCurrencyID = GetBaseCurrencyID();
				string commaSeperatedIDs = GetCommaSeperatedIDs(journalData, "ARJournal", "CustomerID");
				string textCommand = "SELECT CustomerID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Customer WHERE CustomerID IN (" + commaSeperatedIDs + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Customer", textCommand);
				foreach (DataRow row in journalData.Tables["ARJournal"].Rows)
				{
					if (!(row["SysDocID"].ToString().ToLower() == "sys_val"))
					{
						string a = row["CurrencyID"].ToString();
						string str = row["CustomerID"].ToString();
						string text = dataSet.Tables[0].Select("CustomerID = '" + str + "'")[0]["CurrencyID"].ToString();
						if (text != baseCurrencyID && a != text)
						{
							string text2 = CommonLib.ToSqlDateTimeString(DateTime.Parse(row["ARDate"].ToString()));
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
				insertUpdateARJournalCommand.Transaction = sqlTransaction;
				flag = Insert(journalData, "ARJournal", insertUpdateARJournalCommand);
				decimal result = default(decimal);
				string text3 = journalData.Tables["ARJournal"].Rows[0]["CustomerID"].ToString();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Customer", "Balance", "CustomerID", text3, sqlTransaction);
				if (fieldValue != null)
				{
					decimal.TryParse(fieldValue.ToString(), out result);
				}
				foreach (DataRow row2 in journalData.Tables["ARJournal"].Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row2["Debit"].ToString(), out result2);
					decimal.TryParse(row2["Credit"].ToString(), out result3);
					result += result2 - result3;
				}
				textCommand = "UPDATE Customer SET Balance=" + result.ToString() + " WHERE CustomerID='" + text3 + "'";
				return flag & Update(textCommand, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertARJournalForPDC(GLData journalData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				DataRow dataRow = journalData.JournalTable.Rows[0];
				string sysDocID = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				if (isUpdate)
				{
					flag &= new ARJournal(base.DBConfig).DeleteARJournal(sysDocID, voucherID, sqlTransaction);
				}
				foreach (DataRow row in journalData.JournalDetailsTable.Rows)
				{
					ARJournalData aRJournalData = new ARJournalData();
					DataTable dataTable = aRJournalData.Tables["ARJournal"];
					string customerID = row["PayeeID"].ToString();
					DateTime invoiceDate = DateTime.Parse(dataRow["JournalDate"].ToString());
					string a = row["PayeeType"].ToString();
					bool result = false;
					bool.TryParse(row["IsARAP"].ToString(), out result);
					bool flag2 = false;
					if (row["Debit"] != DBNull.Value)
					{
						flag2 = true;
					}
					if (!((a != "C" || !result) | flag2))
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["SysDocID"] = dataRow["SysDocID"];
						dataRow3["VoucherID"] = dataRow["VoucherID"];
						dataRow3["CustomerID"] = row["PayeeID"];
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3["ARAccountID"] = row["AccountID"];
						dataRow3["Debit"] = row["Debit"];
						dataRow3["Credit"] = row["Credit"];
						dataRow3["DebitFC"] = row["DebitFC"];
						dataRow3["CreditFC"] = row["CreditFC"];
						dataRow3["IsPDCRow"] = true;
						dataRow3["CostCenterID"] = row["CostCenterID"];
						dataRow3["CurrencyID"] = dataRow["CurrencyID"];
						dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
						dataRow3["Description"] = row["Description"];
						dataRow3["JobID"] = row["JobID"];
						dataRow3["CostCategoryID"] = row["CostCategoryID"];
						dataRow3["AttributeID1"] = row["AttributeID1"];
						dataRow3["AttributeID2"] = row["AttributeID2"];
						dataRow3["ARDate"] = dataRow["JournalDate"];
						if (row["DueDate"] != DBNull.Value)
						{
							dataRow3["ARDueDate"] = row["DueDate"];
						}
						else if (flag2)
						{
							dataRow3["ARDueDate"] = new Customers(base.DBConfig).CalculateDueDate(invoiceDate, customerID, sqlTransaction);
						}
						dataRow3["PaymentMethodType"] = row["PaymentMethodType"];
						dataRow3["BankID"] = row["BankID"];
						dataRow3["ChequeDate"] = row["CheckDate"];
						dataRow3["ChequeNumber"] = row["CheckNumber"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
						flag &= new ARJournal(base.DBConfig).InsertJournal(aRJournalData, sqlTransaction);
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

		public bool VoidARJournal(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				int result3 = 0;
				object obj = null;
				string text = "";
				string exp = "SELECT CustomerID FROM ARJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj2 = ExecuteScalar(exp, sqlTransaction);
				if (obj2 != null)
				{
					text = obj2.ToString();
				}
				if (!(text != ""))
				{
					return flag;
				}
				decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "Balance", "CustomerID", text, sqlTransaction).ToString(), out result);
				exp = "SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM ARJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
					exp = "UPDATE Customer SET Balance=" + result.ToString() + " WHERE CustomerID='" + text + "'";
					flag &= Update(exp, sqlTransaction);
				}
				exp = "UPDATE ARJOURNAL SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				bool flag2 = false;
				exp = "SELECT AllocationID FROM AR_Payment_Allocation WHERE (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					obj = 0;
				}
				if (Convert.ToInt32(obj) == 0)
				{
					exp = "SELECT AllocationID FROM AR_Payment_Allocation WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "')";
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
						exp = "select PaymentSysDocID,PaymentVoucherID,InvoiceSysDocID,InvoiceVoucherID,SD1.DocName as 'PaymentDoc',SD2.DocName as 'InvoiceDoc' from AR_Payment_Allocation\r\n                                LEFT JOIN System_Document SD1 ON SD1.SysDocID=PaymentSysDocID\r\n                                LEFT JOIN System_Document SD2 ON SD2.SysDocID=InvoiceSysDocID WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "') OR (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
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
				DeleteARAllocation(result3, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteARJournal(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string text = "";
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				int result3 = 0;
				object obj = null;
				string text2 = "";
				text = "SELECT CustomerID FROM ARJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj2 = ExecuteScalar(text, sqlTransaction);
				if (obj2 != null)
				{
					text2 = obj2.ToString();
				}
				if (text2 != "")
				{
					object obj3 = new Databases(base.DBConfig).GetFieldValue("Customer", "Balance", "CustomerID", text2, sqlTransaction);
					if (obj3 == null)
					{
						obj3 = 0;
					}
					decimal.TryParse(obj3.ToString(), out result);
					text = "SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM ARJournal WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					obj3 = ExecuteScalar(text, sqlTransaction);
					decimal.TryParse(obj3.ToString(), out result2);
					if (result2 != 0m)
					{
						result -= result2;
						text = "UPDATE Customer SET Balance=" + result.ToString() + " WHERE CustomerID='" + text2 + "'";
						flag &= Update(text, sqlTransaction);
					}
					bool flag2 = false;
					text = "SELECT AllocationID FROM AR_Payment_Allocation WHERE (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
					obj = ExecuteScalar(text, sqlTransaction);
					if (obj == null)
					{
						obj = 0;
					}
					if (Convert.ToInt32(obj) == 0)
					{
						text = "SELECT AllocationID FROM AR_Payment_Allocation WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "')";
						obj = ExecuteScalar(text, sqlTransaction);
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
							text = "select PaymentSysDocID,PaymentVoucherID,InvoiceSysDocID,InvoiceVoucherID,SD1.DocName as 'PaymentDoc',SD2.DocName as 'InvoiceDoc' from AR_Payment_Allocation\r\n                                LEFT JOIN System_Document SD1 ON SD1.SysDocID=PaymentSysDocID\r\n                                LEFT JOIN System_Document SD2 ON SD2.SysDocID=InvoiceSysDocID WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "') OR (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
							DataSet dataSet2 = new DataSet();
							FillDataSet(dataSet2, "Allocated_Data", text, sqlTransaction);
							DataSet dataSet3 = new DataSet();
							dataSet3.Tables.Add(new DataTable());
							dataSet3.Tables[0].Columns.Add("DocName", typeof(string));
							dataSet3.Tables[0].Columns.Add("paymentFullName", typeof(string));
							dataSet3.Tables[0].Columns.Add("invoiceFullName", typeof(string));
							foreach (DataRow row in dataSet2.Tables[0].Rows)
							{
								DataRow dataRow2 = dataSet3.Tables[0].NewRow();
								string text3 = row["PaymentDoc"].ToString();
								string text4 = row["InvoiceDoc"].ToString();
								string text5 = row["PaymentSysDocID"].ToString();
								string text6 = row["PaymentVoucherID"].ToString();
								string text7 = row["InvoiceSysDocID"].ToString();
								string text8 = row["InvoiceVoucherID"].ToString();
								dataRow2["paymentFullName"] = text3 + ":" + text5 + "-" + text6;
								dataRow2["invoiceFullName"] = text4 + ":" + text7 + "-" + text8;
								dataSet3.Tables[0].Rows.Add(dataRow2);
							}
							string text9 = (from x in (from DataRow row in dataSet3.Tables[0].Rows
									select (row)).ToList()
								select $"({x.ItemArray[0]},{x.ItemArray[1]},{x.ItemArray[2]}),").Aggregate((string x, string y) => x + y).TrimEnd(',');
							text9 = text9.Remove(1, 1);
							if (text9 != null && !flag2)
							{
								throw new CompanyException(("You are not allowed to edit this transaction .Transaction is allocated with this vouchers :\n" + text9 + "\n") ?? "", 50001);
							}
						}
						else if (result3 > 0)
						{
							_ = 3;
						}
					}
					DeleteARAllocation(result3, sqlTransaction);
					text = "DELETE FROM AR_Payment_Allocation WHERE (PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "') OR (InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "')";
					flag &= Delete(text, sqlTransaction);
				}
				text = "DELETE FROM ARJOURNAL WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			catch
			{
				throw;
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

		public bool InsertPaymentAllocation(ARJournalData journalData)
		{
			bool flag = true;
			try
			{
				decimal num = default(decimal);
				string text = "";
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = "";
				string text3 = "";
				text = journalData.PaymentAllocationTable.Rows[0]["CustomerID"].ToString();
				text2 = journalData.PaymentAllocationTable.Rows[0]["PaymentSysDocID"].ToString();
				text3 = journalData.PaymentAllocationTable.Rows[0]["PaymentVoucherID"].ToString();
				decimal result = default(decimal);
				decimal.TryParse(journalData.PaymentAllocationTable.Rows[0]["UnAllocatedAmount"].ToString(), out result);
				string text4 = "";
				if (journalData.PaymentAllocationTable.Rows[0]["PaymentARID"] != DBNull.Value)
				{
					text4 = journalData.PaymentAllocationTable.Rows[0]["PaymentARID"].ToString();
				}
				string text5 = "SELECT * FROM ARJournal WHERE SysDocID='" + text2 + "' AND VoucherID='" + text3 + "' AND CustomerID = '" + text + "' ";
				if (text4 != "")
				{
					text5 = text5 + " AND ARID = " + text4;
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "@ARJournal", text5, sqlTransaction);
				DataRow dataRow = dataSet.Tables["@ARJournal"].Rows[0];
				decimal result2 = default(decimal);
				if (dataRow["CreditFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["CreditFC"].ToString(), out result2);
				}
				else
				{
					decimal.TryParse(dataRow["Credit"].ToString(), out result2);
				}
				decimal result3 = 1m;
				decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result3);
				string baseCurrencyID = GetBaseCurrencyID();
				text5 = "SELECT CASE WHEN ISNULL(CurrencyID,'" + baseCurrencyID + "') = '" + baseCurrencyID + "' THEN  SUM( PaymentAmount) ELSE SUM(PaymentAmountFC) END AS AllocatedAmount FROM AR_Payment_Allocation\r\n                                WHERE PaymentSysDocID = '" + text2 + "' AND PaymentVoucherID = '" + text3 + "' ";
				if (text4 != "")
				{
					text5 = text5 + " AND PaymentARID = " + text4;
				}
				text5 += " GROUP BY CurrencyID";
				decimal result4 = default(decimal);
				object obj = ExecuteScalar(text5, sqlTransaction);
				if (obj != null)
				{
					decimal.TryParse(obj.ToString(), out result4);
				}
				string baseCurrencyID2 = new Currencies(base.DBConfig).GetBaseCurrencyID();
				string text6 = "";
				string text7 = "";
				text6 = dataRow["CurrencyID"].ToString();
				DataRow dataRow2 = journalData.Tables["Payment_Allocation_Batch"].NewRow();
				dataRow2["BatchDate"] = DateTime.Now;
				dataRow2["PartyType"] = "C";
				dataRow2["PartyID"] = dataRow["CustomerID"];
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
					string text10 = row["ARJournalID"].ToString();
					text7 = text8;
					if (row["PaymentARID"] == DBNull.Value)
					{
						throw new CompanyException("Payment ARID is not set.");
					}
					text5 = "SELECT ISNULL(SUM(ISNULL(ARJ.Debit,0))-SUM(ISNULL(PaymentAmount,0))-SUM(ISNULL(DiscountAmount,0)),0) AS UnpaidAmount,\r\n                            ISNULL(SUM(ISNULL(ARJ.DebitFC,0))-SUM(ISNULL(PaymentAmountFC,0))-SUM(ISNULL(DiscountAmountFC,0)),0) AS UnpaidAmountFC\r\n                            FROM ARJournal ARJ \r\n                             LEFT OUTER JOIN AR_Payment_Allocation PA ON ARJ.ARID = PA.ARJournalID\r\n                            WHERE  SysDocID='" + text8 + "' AND  VoucherID='" + text9 + "' AND ARJ.CustomerID='" + text + "' AND ARID = " + text10;
					DataSet dataSet2 = new DataSet();
					FillDataSet(dataSet2, "AR_Payment", text5, sqlTransaction);
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
					d2 = Math.Round(d2, currencyDecimalPoints);
					d3 = Math.Round(d3, currencyDecimalPoints);
					row["BatchID"] = num2;
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal.TryParse(row["PaymentAmount"].ToString(), out result5);
					decimal.TryParse(row["DiscountAmount"].ToString(), out result6);
					d += result5;
					num3 += result6;
					string text11 = "";
					text5 = "SELECT CurrencyID FROM ARJournal WHERE SysDocID='" + text8 + "' AND VoucherID='" + text9 + "'";
					obj = ExecuteScalar(text5);
					if (obj != null)
					{
						text11 = obj.ToString();
					}
					if (text11 == "")
					{
						text11 = baseCurrencyID2;
					}
					if (string.IsNullOrEmpty(text6) || text6 == baseCurrencyID2)
					{
						if (result5 + result6 > d2)
						{
							throw new CompanyException("Payment amount cannot be greater than balance amount.");
						}
					}
					else if (text11 == baseCurrencyID2)
					{
						string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(text6);
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
					if (text11 != baseCurrencyID2)
					{
						text5 = "SELECT CurrencyRate FROM ARJournal WHERE SysDocID='" + text8 + "' AND VoucherID='" + text9 + "'";
						obj = ExecuteScalar(text5);
						if (obj != null && obj.ToString() != "")
						{
							num5 = decimal.Parse(obj.ToString());
						}
					}
					if (text6 == "")
					{
						text6 = baseCurrencyID2;
					}
					if (text11 == text6)
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
						if (text6 != baseCurrencyID2)
						{
							num = default(decimal);
							if (num5 != result3)
							{
								if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
								{
									num += result5 * result3 - result5 * num5;
								}
								else
								{
									num += result5 / result3 - result5 / num5;
								}
								row["RealizedGainLoss"] = Math.Round(num, currencyDecimalPoints);
							}
						}
					}
					else if (text11 == baseCurrencyID2)
					{
						if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
						{
							row["PaymentAmountFC"] = row["PaymentAmount"];
							row["PaymentAmount"] = Math.Round(result5 * result3, currencyDecimalPoints);
							row["DiscountAmountFC"] = Math.Round(result6 * result3, currencyDecimalPoints);
						}
						else
						{
							row["PaymentAmountFC"] = row["PaymentAmount"];
							row["DiscountAmountFC"] = row["DiscountAmount"];
							row["PaymentAmount"] = Math.Round(result5 / result3, currencyDecimalPoints);
							row["DiscountAmount"] = Math.Round(result6 / result3, currencyDecimalPoints);
						}
					}
					else
					{
						if (!(text6 == baseCurrencyID2))
						{
							throw new CompanyException("Allocation can be done for same currency or base currency only.");
						}
						if (new Currencies(base.DBConfig).GetCurrencyRateType(text11) == "M")
						{
							row["PaymentAmountFC"] = Math.Round(result5 / num5, currencyDecimalPoints);
							row["DiscountAmountFC"] = Math.Round(result6 / num5, currencyDecimalPoints);
						}
						else
						{
							row["PaymentAmountFC"] = Math.Round(result5 * num5, currencyDecimalPoints);
							row["DiscountAmountFC"] = Math.Round(result6 * num5, currencyDecimalPoints);
						}
					}
					if (text11 == baseCurrencyID2 && text6 == baseCurrencyID2)
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
				if (journalData.Tables["AR_Payment_Allocation"].Rows.Count > 0)
				{
					flag &= Insert(journalData, "AR_Payment_Allocation", insertUpdateAllocationBatchCommand);
				}
				DataRow dataRow4 = journalData.PaymentAllocationTable.Rows[0];
				insertedRowIdentity = GetInsertedRowIdentity("ARJournal", insertUpdateAllocationBatchCommand);
				int num6 = -1;
				if (insertedRowIdentity != null && insertedRowIdentity.ToString() != "")
				{
					num6 = int.Parse(insertedRowIdentity.ToString());
				}
				foreach (DataRow row2 in journalData.PaymentAllocationTable.Rows)
				{
					string text12 = row2["InvoiceSysDocID"].ToString();
					string text13 = row2["InvoiceVoucherID"].ToString();
					decimal result7 = default(decimal);
					decimal.TryParse(row2["PaymentAmountFC"].ToString(), out result7);
					string text14 = "";
					text5 = "SELECT CurrencyID FROM ARJournal WHERE SysDocID='" + text12 + "' AND VoucherID='" + text13 + "'";
					obj = ExecuteScalar(text5);
					if (obj != null)
					{
						text14 = obj.ToString();
					}
					if (text14 == "")
					{
						text14 = baseCurrencyID2;
					}
					decimal num7 = 1m;
					if (text14 != baseCurrencyID2)
					{
						text5 = "SELECT CurrencyRate FROM ARJournal WHERE SysDocID='" + text12 + "' AND VoucherID='" + text13 + "'";
						obj = ExecuteScalar(text5);
						if (obj != null && obj.ToString() != "")
						{
							num7 = decimal.Parse(obj.ToString());
						}
					}
					if (text6 != baseCurrencyID2 && text6 == text14 && num7 != result3)
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
						num = Math.Round(num, currencyDecimalPoints);
					}
				}
				if (num != 0m)
				{
					GLData gLData = new GLData();
					string value = new Databases(base.DBConfig).GetFieldValue("Journal", "JournalID", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString();
					text = dataRow["CustomerID"].ToString();
					string text15 = new Databases(base.DBConfig).GetFieldValue("Customer", "ARAccountID", "CustomerID", text, sqlTransaction).ToString();
					string text16 = "";
					int num8 = 1;
					text16 = new Databases(base.DBConfig).GetFieldValue("System_Document", "DivisionID", "SysDocID", text2, sqlTransaction).ToString();
					if (text15 == "")
					{
						string textCommand = "SELECT SD.LocationID,ISNULL(C.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID \r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer C ON CustomerID='" + text + "'\r\n                                 LEFT OUTER JOIN Customer_Class CLS ON C.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text7 + "'";
						DataSet dataSet3 = new DataSet();
						FillDataSet(dataSet3, "Accounts", textCommand, sqlTransaction);
						if (dataSet3 == null || dataSet3.Tables.Count == 0 || dataSet3.Tables[0].Rows.Count == 0)
						{
							throw new CompanyException("There is no location assigned to this system document or location record is missing.");
						}
						text15 = dataSet3.Tables["Accounts"].Rows[0]["ARAccountID"].ToString();
					}
					DataRow dataRow5 = gLData.JournalTable.NewRow();
					dataRow5["JournalID"] = 0;
					dataRow5["JournalDate"] = dataRow4["AllocationDate"];
					dataRow5["SysDocID"] = "SYS_CAL";
					dataRow5["SysDocType"] = (byte)49;
					dataRow5["VoucherID"] = num2;
					dataRow5["CurrencyID"] = baseCurrencyID2;
					dataRow5["CurrencyRate"] = 1;
					dataRow5["Narration"] = "Exchange Gain/Loss";
					dataRow5.EndEdit();
					gLData.JournalTable.Rows.Add(dataRow5);
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6["AccountID"] = text15;
					dataRow6["SysDocID"] = "SYS_CAL";
					dataRow6["VoucherID"] = num6;
					dataRow6["CompanyID"] = num8;
					dataRow6["DivisionID"] = text16;
					dataRow6["Description"] = "Currency Exchange Gain/Loss";
					dataRow6["PayeeID"] = dataRow["CustomerID"].ToString();
					dataRow6["PayeeType"] = "C";
					dataRow6["IsARAP"] = true;
					dataRow6["JournalID"] = value;
					dataRow6["AllocationID"] = num2;
					dataRow6["DebitFC"] = DBNull.Value;
					dataRow6["CreditFC"] = DBNull.Value;
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
					string text17 = "";
					text5 = "SELECT ExchangeGainLossAccountID FROM Location\r\n                                        INNER JOIN System_Document SysDoc ON Location.LocationID=SysDoc.LocationID\r\n                                        WHERE SysDocID='" + text2 + "'";
					object obj3 = ExecuteScalar(text5, sqlTransaction);
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
					dataRow6["CompanyID"] = num8;
					dataRow6["DivisionID"] = text16;
					dataRow6["Description"] = "Gain/Loss on Payment No:" + text2 + "-" + text3;
					dataRow6["PayeeID"] = dataRow["CustomerID"].ToString();
					dataRow6["PayeeType"] = "C";
					dataRow6["IsARAP"] = false;
					dataRow6["AllocationID"] = num2;
					dataRow6["JournalID"] = value;
					dataRow6["CreditFC"] = DBNull.Value;
					dataRow6["Credit"] = DBNull.Value;
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
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData, isUpdate: false, sqlTransaction);
				}
				if (num3 != 0m)
				{
					GLData gLData2 = new GLData();
					string text18 = "";
					string textCommand2 = "SELECT JournalID FROM JOURNAL WHERE SysDocID='" + text2 + "' AND VoucherID='" + text3 + "'";
					DataSet dataSet4 = new DataSet();
					FillDataSet(dataSet4, "GL", textCommand2, sqlTransaction);
					if (dataSet4 != null && dataSet4.Tables.Count != 0 && dataSet4.Tables[0].Rows.Count != 0)
					{
						text18 = dataSet4.Tables["GL"].Rows[0]["JournalID"].ToString();
					}
					if (text18 == "")
					{
						text18 = new Databases(base.DBConfig).GetFieldValue("ARJournal", "ARID", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString();
					}
					text = dataRow["CustomerID"].ToString();
					string text19 = "";
					int num9 = 1;
					text19 = new Databases(base.DBConfig).GetFieldValue("System_Document", "DivisionID", "SysDocID", text2, sqlTransaction).ToString();
					string text20 = new Databases(base.DBConfig).GetFieldValue("Customer", "ARAccountID", "CustomerID", text, sqlTransaction).ToString();
					if (text20 == "")
					{
						textCommand2 = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text7 + "'";
						dataSet4 = new DataSet();
						FillDataSet(dataSet4, "Accounts", textCommand2, sqlTransaction);
						if (dataSet4 == null || dataSet4.Tables.Count == 0 || dataSet4.Tables[0].Rows.Count == 0)
						{
							throw new CompanyException("There is no location assigned to this system document or location record is missing.");
						}
						text20 = dataSet4.Tables["Accounts"].Rows[0]["ARAccountID"].ToString();
					}
					DataRow dataRow7 = gLData2.JournalTable.NewRow();
					dataRow7["JournalID"] = 0;
					dataRow7["JournalDate"] = dataRow4["AllocationDate"];
					dataRow7["SysDocID"] = "SYS_CDS";
					dataRow7["SysDocType"] = (byte)237;
					dataRow7["VoucherID"] = num2;
					dataRow7["CurrencyID"] = baseCurrencyID2;
					dataRow7["CurrencyRate"] = 1;
					dataRow7["Narration"] = "Allocation Discount";
					dataRow7.EndEdit();
					gLData2.JournalTable.Rows.Add(dataRow7);
					DataRow dataRow8 = gLData2.JournalDetailsTable.NewRow();
					dataRow8["AccountID"] = text20;
					dataRow8["SysDocID"] = "SYS_CDS";
					dataRow8["VoucherID"] = num6;
					dataRow8["CompanyID"] = num9;
					dataRow8["DivisionID"] = text19;
					dataRow8["Description"] = "Allocation Discount";
					dataRow8["PayeeID"] = dataRow["CustomerID"].ToString();
					dataRow8["PayeeType"] = "C";
					dataRow8["IsARAP"] = true;
					dataRow8["JournalID"] = text18;
					dataRow8["AllocationID"] = num2;
					dataRow8["DebitFC"] = DBNull.Value;
					dataRow8["CreditFC"] = DBNull.Value;
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
					string text21 = "";
					text5 = "SELECT AllocationDiscountAccountID FROM Location\r\n                                        INNER JOIN System_Document SysDoc ON Location.LocationID=SysDoc.LocationID\r\n                                        WHERE SysDocID='" + text2 + "'";
					object obj4 = ExecuteScalar(text5, sqlTransaction);
					if (obj4 != null && obj4.ToString() != "")
					{
						text21 = obj4.ToString();
					}
					if (text21 == "")
					{
						throw new CompanyException("There is a discount on this transaction. Discount Allocation account is not set for location.");
					}
					dataRow8 = gLData2.JournalDetailsTable.NewRow();
					dataRow8["AccountID"] = text21;
					dataRow8["SysDocID"] = text2;
					dataRow8["VoucherID"] = text3;
					dataRow8["CompanyID"] = num9;
					dataRow8["DivisionID"] = text19;
					dataRow8["Description"] = "Discount on Payment No:" + text2 + "-" + text3;
					dataRow8["PayeeID"] = dataRow["CustomerID"].ToString();
					dataRow8["PayeeType"] = "C";
					dataRow8["IsARAP"] = false;
					dataRow8["AllocationID"] = num2;
					dataRow8["JournalID"] = text18;
					dataRow8["CreditFC"] = DBNull.Value;
					dataRow8["Credit"] = DBNull.Value;
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
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData2, isUpdate: false, sqlTransaction);
				}
				if (result != 0m)
				{
					GLData gLData3 = new GLData();
					string text22 = "";
					string textCommand3 = "SELECT JournalID FROM JOURNAL WHERE SysDocID='" + text2 + "' AND VoucherID='" + text3 + "'";
					DataSet dataSet5 = new DataSet();
					FillDataSet(dataSet5, "GL", textCommand3, sqlTransaction);
					if (dataSet5 != null && dataSet5.Tables.Count != 0 && dataSet5.Tables[0].Rows.Count != 0)
					{
						text22 = dataSet5.Tables["GL"].Rows[0]["JournalID"].ToString();
					}
					if (text22 == "")
					{
						text22 = new Databases(base.DBConfig).GetFieldValue("ARJournal", "ARID", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString();
					}
					text = dataRow["CustomerID"].ToString();
					string text23 = "";
					int num10 = 1;
					text23 = new Databases(base.DBConfig).GetFieldValue("System_Document", "DivisionID", "SysDocID", text2, sqlTransaction).ToString();
					string text24 = new Databases(base.DBConfig).GetFieldValue("Customer", "ARAccountID", "CustomerID", text, sqlTransaction).ToString();
					if (text24 == "")
					{
						textCommand3 = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text7 + "'";
						dataSet5 = new DataSet();
						FillDataSet(dataSet5, "Accounts", textCommand3, sqlTransaction);
						if (dataSet5 == null || dataSet5.Tables.Count == 0 || dataSet5.Tables[0].Rows.Count == 0)
						{
							throw new CompanyException("There is no location assigned to this system document or location record is missing.");
						}
						text24 = dataSet5.Tables["Accounts"].Rows[0]["ARAccountID"].ToString();
					}
					DataRow dataRow9 = gLData3.JournalTable.NewRow();
					dataRow9["JournalID"] = 0;
					dataRow9["JournalDate"] = dataRow4["AllocationDate"];
					dataRow9["SysDocID"] = "SYS_CUA";
					dataRow9["SysDocType"] = (byte)237;
					dataRow9["VoucherID"] = num2;
					dataRow9["CurrencyID"] = baseCurrencyID2;
					dataRow9["CurrencyRate"] = 1;
					dataRow9["Narration"] = "Write-off UnAllocated Amount";
					dataRow9.EndEdit();
					gLData3.JournalTable.Rows.Add(dataRow9);
					DataRow dataRow10 = gLData3.JournalDetailsTable.NewRow();
					dataRow10["AccountID"] = text24;
					dataRow10["SysDocID"] = "SYS_CUA";
					dataRow10["VoucherID"] = num6;
					dataRow10["CompanyID"] = num10;
					dataRow10["DivisionID"] = text23;
					dataRow10["Description"] = "Write-off UnAllocated Amount";
					dataRow10["PayeeID"] = dataRow["CustomerID"].ToString();
					dataRow10["PayeeType"] = "C";
					dataRow10["IsARAP"] = true;
					dataRow10["JournalID"] = text22;
					dataRow10["AllocationID"] = num2;
					dataRow10["DebitFC"] = DBNull.Value;
					dataRow10["CreditFC"] = DBNull.Value;
					dataRow10["Debit"] = Math.Abs(result);
					dataRow10["Credit"] = DBNull.Value;
					gLData3.JournalDetailsTable.Rows.Add(dataRow10);
					string text25 = "";
					text5 = "SELECT AllocationDiscountAccountID FROM Location\r\n                                        INNER JOIN System_Document SysDoc ON Location.LocationID=SysDoc.LocationID\r\n                                        WHERE SysDocID='" + text2 + "'";
					object obj5 = ExecuteScalar(text5, sqlTransaction);
					if (obj5 != null && obj5.ToString() != "")
					{
						text25 = obj5.ToString();
					}
					if (text25 == "")
					{
						throw new CompanyException("There is a discount on this transaction. Discount Allocation account is not set for location.");
					}
					dataRow10 = gLData3.JournalDetailsTable.NewRow();
					dataRow10["AccountID"] = text25;
					dataRow10["SysDocID"] = text2;
					dataRow10["VoucherID"] = text3;
					dataRow10["CompanyID"] = num10;
					dataRow10["DivisionID"] = text23;
					dataRow10["Description"] = "Write-off UnAllocated Amount on Payment No:" + text2 + "-" + text3;
					dataRow10["PayeeID"] = dataRow["CustomerID"].ToString();
					dataRow10["PayeeType"] = "C";
					dataRow10["IsARAP"] = false;
					dataRow10["AllocationID"] = num2;
					dataRow10["JournalID"] = text22;
					dataRow10["CreditFC"] = DBNull.Value;
					dataRow10["Credit"] = DBNull.Value;
					dataRow10["Debit"] = DBNull.Value;
					dataRow10["Credit"] = Math.Abs(result);
					gLData3.JournalDetailsTable.Rows.Add(dataRow10);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData3, isUpdate: false, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowInsertUpdateInfo("AR_Payment_Allocation", "AllocationID", num6, sqlTransaction, isInsert: true);
				flag &= AddActivityLog("Customer Payment Allocation", text, ActivityTypes.Add, sqlTransaction);
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

		private string GetBaseCurrencyID()
		{
			string exp = "SELECT TOP 1 BaseCurrencyID FROM Company";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public DataSet GetUnallocatedInvoices(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT ARID JournalID,ARJ.SysDocID,VoucherID,Reference,CustomerID,ARDate,ISNULL(ISNULL(DebitFC,Debit),0) AS OriginalAmount,\r\n                                   ISNULL(ARJ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, \r\n                                  ISNULL(ARJ.CurrencyRate,1) AS CurrencyRate,  \r\n                                  ISNULL(ISNULL(ISNULL(DebitFC,Debit),0) -   \r\n                                (SELECT CASE WHEN ARJ.CurrencyID IS NULL  OR ARJ.CurrencyID=(SELECT CurrencyID FROM Currency WHERE IsBase='True') THEN SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)) \r\n                                ELSE SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)) END FROM AR_Payment_Allocation ARP\r\n                                WHERE ARJ.ARID = ARP.ARJournalID),ISNULL(ISNULL(DebitFC,Debit),0))  AS AmountDue,\r\n                                 ISNULL(ISNULL(Debit,0) -   \r\n                                ISNULL((SELECT SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0))  FROM AR_Payment_Allocation ARP\r\n                                WHERE ARJ.ARID = ARP.ARJournalID),0),0)  AS AmountDueBase,(select ISNULL(DiscountAmount,0) from AR_Payment_Allocation ARP WHERE ARJ.JournalID = ARP.ARJournalID) AS Discount\r\n                                  FROM ARJournal ARJ    LEFT OUTER JOIN System_Document SD ON SD.SysDocID = ARJ.SysDocID\r\n                                WHERE ISNULL(Debit,0)>0 AND ISNULL(IsVoid,'False')='False' AND ISNULL(SD.SysDocType,1) <> 12 AND ARJ.SysDocID NOT IN('SYS_CAL','SYS_CDS','SYS_CUA') AND   CustomerID = '" + customerID + "'\r\n                              \r\n                                AND \r\n                                (SELECT CASE WHEN (ARJ.CurrencyID IS NULL OR ARJ.CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')) THEN \r\n\t\t\t\t\t\t\t\t\t\tISNULL(SUM(ISNULL(PaymentAmount,0)+ISNULL(DiscountAmount,0)),0) ELSE  \r\n\t\t\t\t\t\t\t\t\t\tISNULL(SUM(ISNULL(PaymentAmountFC,0)+ISNULL(DiscountAmountFC,0)),0) END FROM AR_Payment_Allocation PA\r\n\t                                WHERE ARJ.ARID = PA.ARJournalID)<\r\n\t                                CASE WHEN (ARJ.CurrencyID IS NULL OR ARJ.CurrencyID = (SELECT CurrencyID FROM Currency WHERE IsBase='True') )\r\n\t                                THEN ISNULL(ISNULL(Debit,0),0) ELSE ISNULL(ISNULL(DebitFC,0),0) END ORDER BY ARDate");
				FillDataSet(dataSet, "AR_Payment_Allocation", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetARPaymentToAllocate(string sysDocID, string voucherID, string customerID, int paymentARID, bool isPDC)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				bool result = false;
				int num = 9;
				bool.TryParse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(111.ToString()).ToString(), out result);
				if (!result)
				{
					num = 10;
				}
				text = "SELECT GLT.SysDocID,VoucherID,'C' AS PayeeType,GLT.CustomerID AS PayeeID, SysDocType,ARID,\r\n                                Customer.CustomerName\r\n                                AS PayeeName,\r\n                                ISNULL(ISNULL(CreditFC,Credit)-\r\n\t\t\t\t\t\t\t\t\t(SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Cheque_Received Chq\r\n\t\t\t\t\t\t\t\t\tWHERE GLT.SysDocID=Chq.SysDocID AND GLT.VoucherID=Chq.VoucherID AND GLT.CustomerID = Chq.PayeeID AND Status = " + num + ") \r\n                                -ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END  \r\n\t\t\t\t\t\t\t\tFROM AR_Payment_Allocation PA WHERE PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "' AND CustomerID='" + customerID + "'  AND (PaymentARID IS NULL OR PaymentARID = ARID)  GROUP BY CurrencyID),0),0) AS UnAllocatedAmount,\r\n                                Credit - (SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Cheque_Received Chq\r\n\t\t\t\t\t\t\t\t\tWHERE GLT.SysDocID=Chq.SysDocID AND GLT.VoucherID=Chq.VoucherID AND GLT.CustomerID = Chq.PayeeID AND Status = " + num + ")  AS Amount,CreditFC AS AmountFC,ARDate AS TransactionDate,CurrencyRate,\r\n                                Reference,Description,\r\n                                ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID\r\n                                FROM ARJournal GLT \r\n                                LEFT OUTER JOIN System_Document SD ON GLT.SysDocID = SD.SysDocID\r\n                                LEFt OUTER JOIN Customer ON GLT.CustomerID=Customer.CustomerID \r\n                                WHERE ISNULL(IsVoid,'False')='False'  AND (SD.SysDocType IS NULL OR SD.SysDocType <> 7 )  AND\r\n                                GLT.SysDocID = '" + sysDocID + "' AND VoucherID ='" + voucherID + "' AND GLT.CustomerID='" + customerID + "' ";
				if (paymentARID > 0)
				{
					text = text + " AND ARID = " + paymentARID;
				}
				text += " ORDER BY CONVERT(DATE, ARDate, 103), VoucherID ";
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

		public DataSet GetARPaymentToAllocate(string customerID, int paymentARID, bool isPDC)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				bool result = false;
				int num = 9;
				bool.TryParse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(111.ToString()).ToString(), out result);
				if (!result)
				{
					num = 10;
				}
				text = "SELECT GLT.SysDocID,VoucherID,'C' AS PayeeType,GLT.CustomerID AS PayeeID, SysDocType,ARID,\r\n                                Customer.CustomerName\r\n                                AS PayeeName,\r\n                                ISNULL(ISNULL(CreditFC,Credit)-\r\n\t\t\t\t\t\t\t\t\t(SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Cheque_Received Chq\r\n\t\t\t\t\t\t\t\t\tWHERE GLT.SysDocID=Chq.SysDocID AND GLT.VoucherID=Chq.VoucherID AND GLT.CustomerID = Chq.PayeeID AND Status = " + num + ") \r\n                                -ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END  \r\n\t\t\t\t\t\t\t\tFROM AR_Payment_Allocation PA WHERE CustomerID='" + customerID + "'  AND (PaymentARID IS NULL OR PaymentARID = ARID)  GROUP BY CurrencyID),0),0) AS UnAllocatedAmount,\r\n                                Credit - (SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Cheque_Received Chq\r\n\t\t\t\t\t\t\t\t\tWHERE GLT.SysDocID=Chq.SysDocID AND GLT.VoucherID=Chq.VoucherID AND GLT.CustomerID = Chq.PayeeID AND Status = " + num + ")  AS Amount,CreditFC AS AmountFC,ARDate AS TransactionDate,CurrencyRate,\r\n                                Reference,Description,\r\n                                ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID\r\n                                FROM ARJournal GLT \r\n                                LEFT OUTER JOIN System_Document SD ON GLT.SysDocID = SD.SysDocID\r\n                                LEFt OUTER JOIN Customer ON GLT.CustomerID=Customer.CustomerID \r\n                                WHERE ISNULL(IsVoid,'False')='False'  AND (SD.SysDocType IS NULL OR SD.SysDocType <> 7 )  AND\r\n                                 GLT.CustomerID='" + customerID + "' ";
				if (paymentARID > 0)
				{
					text = text + " AND ARID = " + paymentARID;
				}
				text += " ORDER BY CONVERT(DATE, ARDate, 103), VoucherID ";
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
				bool result = false;
				int num = 9;
				bool.TryParse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(111.ToString()).ToString(), out result);
				if (!result)
				{
					num = 10;
				}
				SqlCommand sqlCommand = new SqlCommand("SELECT * FROM \r\n                                            (SELECT ARJ.SysDocID,VoucherID,ISNULL(IsPDCRow,'False') AS IsPDC, ARJ.CustomerID,CustomerName,ARDate,\r\n                                            (SELECT SUBSTRING((SELECT  DISTINCT ',' +  TD.ChequeNumber + ''  FROM  Cheque_Received TD \r\n                                            WHERE TD.SysDocID = ARJ.SysDocID AND TD.VoucherID = ARJ.VoucherID FOR XML PATH('')),2,20000)) AS [ChequeNumber]\r\n                                            ,ISNULL(ARJ.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID, ARJ.ARID,\r\n                                            ISNULL(ISNULL(CreditFC,Credit),0) -\r\n                                            (SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Cheque_Received Chq\r\n                                            WHERE ARJ.SysDocID=Chq.SysDocID AND ARJ.VoucherID=Chq.VoucherID AND ARJ.CustomerID = Chq.PayeeID AND Status = " + num + ")   AS OriginalAmount,\r\n                                            ISNULL(ISNULL(CreditFC,Credit),0) -\r\n                                            (SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Cheque_Received Chq\r\n                                            WHERE ARJ.SysDocID=Chq.SysDocID AND ARJ.VoucherID=Chq.VoucherID AND ARJ.CustomerID = Chq.PayeeID AND Status = " + num + ") \r\n                                            -\r\n                                            ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                            THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) +SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END FROM AR_Payment_Allocation ARP\r\n                                            WHERE ARJ.SysDocID=ARP.PaymentSysDocID AND ARJ.VoucherID=ARP.PaymentVoucherID AND ARJ.CustomerID = ARP.CustomerID AND ARJ.ARID = ARP.PaymentARID  GROUP BY CurrencyID),0)  AS Unallocated\r\n                                            FROM ARJournal ARJ   LEFT OUTER JOIN System_Document SD ON ARJ.SysDocID = SD.SysDocID \r\n                                            INNER JOIN Customer ON ARJ.CustomerID=Customer.CustomerID\r\n                                            WHERE ISNULL(Credit,0)>0 AND ISNULL(IsVoid,'False')='False' AND ARJ.SysDocID NOT IN('SYS_CDS','SYS_CUA')  AND (SD.SysDocType IS NULL OR SD.SysDocType <> 7 )\r\n                                            AND ISNULL((SELECT CASE WHEN PA.CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                            THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) +SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END  FROM AR_Payment_Allocation PA\r\n                                            WHERE PA.PaymentSysDocID=ARJ.SysDocID AND PA.PaymentVoucherID=ARJ.VoucherID  AND ARJ.CustomerID = PA.CustomerID AND ARJ.ARID = PA.PaymentARID  GROUP BY PA.CurrencyID),0)<ISNULL(ISNULL(CreditFC,Credit),0)) ALC\r\n                                            WHERE Unallocated<>0");
				FillDataSet(dataSet, "AR_Payment_Allocation", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetARAllocationList(DateTime from, DateTime to)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				SqlCommand sqlCommand = new SqlCommand("SELECT  BatchID Batch,ARP.AllocationID ID,ARP.CustomerID AS [Customer Code],CUS.CustomerName AS [Customer Name],InvoiceVoucherID [Invoice No],ARJ.ARDate AS [Invoice Date], ARJ.ARDueDate [Due Date],\r\n                                    SD.DocName,PaymentVoucherID [Receipt No],ARJ2.ARDate AS [Payment Date],AllocationDate [Allocation Date],PaymentAmount [Amount], PaymentAmountFC [Amount FC],ARP.CreatedBy [Allocated By],\r\n                                    RealizedGainLoss [Gain/Loss],ARP.CurrencyID [Cur],ARP.CurrencyRate [Cur Rate]        \r\n                                    FROM dbo.AR_Payment_Allocation ARP INNER JOIN Customer CUS ON Cus.CustomerID = ARP.CustomerID\r\n\t\t\t\t\t\t\t\t\tINNER JOIN ARJournal ARJ ON ARP.ARJournalID = ARJ.ARID\r\n\t\t\t\t\t\t\t\t\tINNER JOIN ARJournal ARJ2 ON ARp.PaymentARID = ARJ2.ARID  \r\n\t                                LEFT JOIN System_Document SD ON SD.SysDocID=ARP.PaymentSysDocID                                  \r\n                                    WHERE AllocationDate BETWEEN '" + text + "' AND '" + text2 + "'");
				FillDataSet(dataSet, "AP_Payment_Allocation", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteARAllocation(int id)
		{
			try
			{
				return DeleteARAllocation(id, null);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteARAllocation(int id, SqlTransaction sqlTransaction)
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
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("AR_Payment_Allocation", "BatchID", "AllocationID", id, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					num = int.Parse(fieldValue.ToString());
				}
				text = ((num <= 0) ? ("DELETE FROM AR_Payment_Allocation WHERE AllocationID = " + id.ToString()) : ("DELETE FROM AR_Payment_Allocation WHERE BatchID = " + num.ToString()));
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				text = "DELETE FROM Payment_Allocation_Batch WHERE BatchID = " + num.ToString();
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (num <= 0)
				{
					flag &= new Journal(base.DBConfig).DeleteJournal("SYS_CAL", id.ToString(), sqlTransaction);
					flag &= new Journal(base.DBConfig).DeleteJournal("SYS_CDS", id.ToString(), sqlTransaction);
					flag &= new Journal(base.DBConfig).DeleteJournal("SYS_CUA", id.ToString(), sqlTransaction);
					return flag;
				}
				flag &= new Journal(base.DBConfig).DeleteJournal("SYS_CAL", num.ToString(), sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal("SYS_CDS", num.ToString(), sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal("SYS_CUA", num.ToString(), sqlTransaction);
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

		public int UpdateFCAmountConversion()
		{
			int num = 0;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string baseCurrencyID = GetBaseCurrencyID();
				string exp = "UPDATE ARJ SET ConRate =   \r\n                                     CASE WHEN  (ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') <> C.CurrencyID) \r\n                                    THEN\r\n                                      ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = C.CurrencyID AND Ex.RateUpdatedDate < ARJ.ARDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = C.CurrencyID)) END\r\n                                       ,\r\n                                    ConDebitFC = CASE WHEN (Debit IS NOT NULL AND ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') <> C.CurrencyID) \r\n                                    THEN\r\n                                    CASE (SELECT DISTINCT RateType FROM Currency C WHERE C.CurrencyID = C.CurrencyID) WHEN 'M' \r\n                                    THEN (Debit / ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = C.CurrencyID AND Ex.RateUpdatedDate < ARJ.ARDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = C.CurrencyID)))\r\n                                    ELSE (Debit * ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = C.CurrencyID AND Ex.RateUpdatedDate < ARJ.ARDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = C.CurrencyID))) \r\n                                     END END   ,\r\n\r\n \r\n                                    ConCreditFC = CASE WHEN Credit IS NOT NULL AND (ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') <> C.CurrencyID) \r\n                                    THEN\r\n                                    CASE (SELECT DISTINCT RateType FROM Currency C WHERE C.CurrencyID = C.CurrencyID) WHEN 'M' \r\n                                    THEN (Credit / ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = C.CurrencyID AND Ex.RateUpdatedDate < ARJ.ARDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = C.CurrencyID)))\r\n                                    ELSE (Credit * ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = C.CurrencyID AND Ex.RateUpdatedDate < ARJ.ARDate),(SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = C.CurrencyID))) \r\n                                     END END   \r\n\r\n  \r\n                                    FROM ARJournal ARJ INNER JOIN Customer C ON C.CustomerID = ARJ.CustomerID \r\n                                    WHERE  ISNULL(ARJ.CurrencyID,'" + baseCurrencyID + "') <> C.CurrencyID  AND ARJ.SysDocID <> 'SYS_VAL'";
				num = ExecuteNonQuery(exp, sqlTransaction);
				return num;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(num >= 0);
			}
		}
	}
}
