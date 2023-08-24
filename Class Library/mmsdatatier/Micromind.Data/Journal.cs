using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Journal : StoreObject
	{
		private const string JOURNALID_PARM = "@JournalID";

		private const string SYSDOCTYPE_PARM = "@SysDocType";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string JOURNALDATE_PARM = "@JournalDate";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string NARRATION_PARM = "@Narration";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DOCTYPE_PARM = "@DocType";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string JDDATE_PARM = "@JDDate";

		private const string DEBIT_PARM = "@Debit";

		private const string CREDIT_PARM = "@Credit";

		private const string DEBITFC_PARM = "@DebitFC";

		private const string CREDITFC_PARM = "@CreditFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string ALLOCATIONID_PARM = "@AlocationID";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string BANKID_PARM = "@BankID";

		private const string CHECKBOOKID_PARM = "@CheckbookID";

		private const string CHECKID_PARM = "@CheckID";

		private const string CHECKDATE_PARM = "@CheckDate";

		private const string CHECKNUMBER_PARM = "@CheckNumber";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string ISARAP_PARM = "@IsARAP";

		private const string JVENTRYTYPE_PARM = "@JVEntryType";

		private const string STJOURNALID_PARM = "@STJournalID";

		private const string STJYEAR_PARM = "@STJYear";

		private const string STJMONTH_PARM = "@STJMonth";

		private const string FROM_PARM = "@From";

		private const string TO_PARM = "@To";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ATTRIBUTEID1_PARM = "@AttributeID1";

		private const string ATTRIBUTEID2_PARM = "@AttributeID2";

		private const string STOREID_PARM = "@StoreID";

		private const string CONSIGNID_PARM = "@ConsignID";

		private const string CONSIGNEXPENSEID_PARM = "@ConsignExpenseID";

		public Journal(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJournalText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Journal", new FieldValue("JournalID", "@JournalID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("JournalDate", "@JournalDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("Narration", "@Narration"), new FieldValue("STJournalID", "@STJournalID"), new FieldValue("STJYear", "@STJYear"), new FieldValue("STJMonth", "@STJMonth"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJournalCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJournalText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJournalText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JournalID", SqlDbType.Int);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@SysDocType", SqlDbType.Int);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@JournalDate", SqlDbType.DateTime);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@Narration", SqlDbType.NVarChar);
			parameters.Add("@STJournalID", SqlDbType.NVarChar);
			parameters.Add("@STJYear", SqlDbType.SmallInt);
			parameters.Add("@STJMonth", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@JournalID"].SourceColumn = "JournalID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@JournalDate"].SourceColumn = "JournalDate";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@Narration"].SourceColumn = "Narration";
			parameters["@STJournalID"].SourceColumn = "STJournalID";
			parameters["@STJYear"].SourceColumn = "STJYear";
			parameters["@STJMonth"].SourceColumn = "STJMonth";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateJournalDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Journal_Details", new FieldValue("JournalID", "@JournalID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("Description", "@Description"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("JDDate", "@JDDate"), new FieldValue("DocType", "@DocType"), new FieldValue("AttributeID1", "@AttributeID1"), new FieldValue("AttributeID2", "@AttributeID2"), new FieldValue("ConsignID", "@ConsignID"), new FieldValue("ConsignExpenseID", "@ConsignExpenseID"), new FieldValue("Reference", "@Reference"), new FieldValue("Debit", "@Debit"), new FieldValue("Credit", "@Credit"), new FieldValue("DebitFC", "@DebitFC"), new FieldValue("CreditFC", "@CreditFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("AccountID", "@AccountID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("AllocationID", "@AlocationID"), new FieldValue("BankID", "@BankID"), new FieldValue("CheckID", "@CheckID"), new FieldValue("CheckbookID", "@CheckbookID"), new FieldValue("CheckNumber", "@CheckNumber"), new FieldValue("CheckDate", "@CheckDate"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("IsARAP", "@IsARAP"), new FieldValue("JVEntryType", "@JVEntryType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("PayeeType", "@PayeeType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJournalDetailsCommand(bool isUpdate)
		{
			updateCommand = null;
			insertCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateJournalDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateJournalDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@JournalID", SqlDbType.Int);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@JDDate", SqlDbType.DateTime).IsNullable = false;
			parameters.Add("@DocType", SqlDbType.Int).IsNullable = false;
			parameters.Add("@AlocationID", SqlDbType.Int);
			parameters.Add("@Debit", SqlDbType.Money);
			parameters.Add("@Credit", SqlDbType.Money);
			parameters.Add("@DebitFC", SqlDbType.Money);
			parameters.Add("@CreditFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@AttributeID1", SqlDbType.NVarChar);
			parameters.Add("@AttributeID2", SqlDbType.NVarChar);
			parameters.Add("@ConsignID", SqlDbType.NVarChar);
			parameters.Add("@ConsignExpenseID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodType", SqlDbType.NVarChar);
			parameters.Add("@CheckID", SqlDbType.Int);
			parameters.Add("@CheckbookID", SqlDbType.NVarChar);
			parameters.Add("@CheckNumber", SqlDbType.NVarChar);
			parameters.Add("@CheckDate", SqlDbType.DateTime);
			parameters.Add("@IsARAP", SqlDbType.NVarChar);
			parameters.Add("@JVEntryType", SqlDbType.TinyInt);
			parameters["@JournalID"].SourceColumn = "JournalID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@JDDate"].SourceColumn = "JDDate";
			parameters["@DocType"].SourceColumn = "DocType";
			parameters["@AlocationID"].SourceColumn = "AllocationID";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@DebitFC"].SourceColumn = "DebitFC";
			parameters["@CreditFC"].SourceColumn = "CreditFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@AttributeID1"].SourceColumn = "AttributeID1";
			parameters["@AttributeID2"].SourceColumn = "AttributeID2";
			parameters["@ConsignID"].SourceColumn = "ConsignID";
			parameters["@ConsignExpenseID"].SourceColumn = "ConsignExpenseID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@CheckID"].SourceColumn = "CheckID";
			parameters["@CheckbookID"].SourceColumn = "CheckbookID";
			parameters["@CheckNumber"].SourceColumn = "CheckNumber";
			parameters["@CheckDate"].SourceColumn = "CheckDate";
			parameters["@IsARAP"].SourceColumn = "IsARAP";
			parameters["@JVEntryType"].SourceColumn = "JVEntryType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(GLData journalData, SqlTransaction sqlTransaction)
		{
			return ValidateData(journalData, throwError: true, sqlTransaction);
		}

		private bool ValidateData(GLData journalData, bool throwError, SqlTransaction sqlTransaction)
		{
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			foreach (DataRow row in journalData.JournalDetailsTable.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(row["Debit"].ToString(), out result);
				decimal.TryParse(row["Credit"].ToString(), out result2);
				if (row["AccountID"].ToString() == "")
				{
					throw new Exception("Account is not enterd for journal row.");
				}
				if (result > 0m && result2 > 0m)
				{
					throw new Exception("Either debit or credit should be zero");
				}
				if (result < 0m || result2 < 0m)
				{
					throw new Exception("Amount cannot be negative.");
				}
				d += result;
				d2 += result2;
			}
			if (d != d2)
			{
				throw new CompanyException("Debit is not equal to credit.");
			}
			DataRow dataRow = journalData.JournalTable.Rows[0];
			string idFieldValue = dataRow["SysDocID"].ToString();
			string checkFieldValue = dataRow["VoucherID"].ToString();
			DateTime dateTime = DateTime.Parse(dataRow["JournalDate"].ToString());
			DateTime t = dateTime;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Journal", "JournalDate", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, sqlTransaction);
			if (!fieldValue.IsNullOrEmpty())
			{
				t = DateTime.Parse(fieldValue.ToString());
			}
			object obj2 = new CompanyInformations(base.DBConfig).GetLastClosingDate();
			if (obj2 != null)
			{
				DateTime dateTime2 = DateTime.Parse(obj2.ToString());
				dateTime2 = new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 23, 59, 59);
				if (dateTime <= dateTime2 || t <= dateTime2)
				{
					throw new CompanyException("Cannot record a transaction in a closed period.", 1038);
				}
			}
			string textCommand = "SELECT MIN(StartDate) AS StartDate , Max(EndDate) AS EndDate FROM FiscalYear WHERE Status = 1";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "FiscalYear", textCommand);
			if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow2 = dataSet.Tables[0].Rows[0];
				DateTime t2 = DateTime.Parse(dataRow2["StartDate"].ToString());
				DateTime t3 = DateTime.Parse(dataRow2["EndDate"].ToString());
				if (dateTime < t2 || dateTime > t3)
				{
					throw new CompanyException("Transaction date is out of range of defined fiscal years.", 1056);
				}
			}
			return true;
		}

		private bool ValidateDetailsOnlyData(GLData journalData, bool throwError, SqlTransaction sqlTransaction)
		{
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
			foreach (DataRow row in journalData.JournalDetailsTable.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string key = row["SysDocID"].ToString() + row["VoucherID"].ToString();
				decimal.TryParse(row["Debit"].ToString(), out result);
				decimal.TryParse(row["Credit"].ToString(), out result2);
				if (row["AccountID"].ToString() == "")
				{
					throw new Exception("Account is not enterd for journal row.");
				}
				if (result > 0m && result2 > 0m)
				{
					throw new Exception("Either debit or credit should be zero");
				}
				if (result < 0m || result2 < 0m)
				{
					throw new Exception("Amount cannot be negative.");
				}
				if (dictionary.ContainsKey(key))
				{
					dictionary[key] += result - result2;
				}
				else
				{
					dictionary.Add(key, result - result2);
				}
				d += result;
				d2 += result2;
			}
			if (d != d2)
			{
				throw new CompanyException("Debit is not equal to credit.");
			}
			foreach (string key2 in dictionary.Keys)
			{
				if (dictionary[key2] != 0m)
				{
					throw new CompanyException("Debit is not equal to credit for transaction:" + key2);
				}
			}
			DateTime dateTime = new DateTime(1970, 1, 1);
			object obj = new CompanyInformations(base.DBConfig).GetLastClosingDate();
			if (obj != null)
			{
				dateTime = DateTime.Parse(obj.ToString());
				dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
				foreach (DataRow row2 in journalData.JournalDetailsTable.Rows)
				{
					string idFieldValue = row2["SysDocID"].ToString();
					string checkFieldValue = row2["VoucherID"].ToString();
					DateTime minValue = DateTime.MinValue;
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Journal", "JournalDate", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, sqlTransaction);
					if (fieldValue == null)
					{
						fieldValue = new Databases(base.DBConfig).GetFieldValue("Inventory_Transactions", "TransactionDate", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, sqlTransaction);
					}
					if (fieldValue.IsNullOrEmpty())
					{
						throw new CompanyException("Journal date not found.");
					}
					minValue = DateTime.Parse(fieldValue.ToString());
					if (minValue <= dateTime)
					{
						throw new CompanyException("Cannot record a transaction in a closed period.", 1038);
					}
				}
			}
			return true;
		}

		internal int GetJournalID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT JournalID FROM Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public bool InsertUpdateJournal(GLData journalData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			flag = ValidateData(journalData, sqlTransaction);
			if (!flag)
			{
				throw new CompanyException("Debit is not equal to credit.");
			}
			SqlCommand insertUpdateJournalCommand = GetInsertUpdateJournalCommand(isUpdate);
			try
			{
				DataRow dataRow = journalData.JournalTable.Rows[0];
				int num = isUpdate ? GetJournalID(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), sqlTransaction) : GetNextJournalID(sqlTransaction);
				int result = 0;
				int.TryParse(dataRow["SysDocType"].ToString(), out result);
				if (isUpdate)
				{
					if (num == -1)
					{
						throw new CompanyException("Journal entries for this transaction not found. Please try again or the transaction may be faulted.", 1035);
					}
					journalData.AcceptChanges();
					dataRow["JournalID"] = num;
				}
				else
				{
					dataRow["JournalID"] = num;
				}
				if (isUpdate)
				{
					flag &= DeleteDetailsRows(sqlTransaction, num.ToString());
				}
				journalData.JournalDetailsTable.AcceptChanges();
				foreach (DataRow row in journalData.JournalDetailsTable.Rows)
				{
					row.SetAdded();
					row["JournalID"] = num;
					row["JDDate"] = dataRow["JournalDate"];
					row["DocType"] = dataRow["SysDocType"];
					if (row["JDDate"].IsDBNullOrEmpty())
					{
						throw new CompanyException("JDDate cannot be empty.");
					}
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				if (baseCurrencyID == "")
				{
					throw new CompanyException("Base currency is not defined. Please define your company base currency.");
				}
				bool flag2 = false;
				decimal result2 = 1m;
				string text = "M";
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != baseCurrencyID)
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
					text = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				foreach (DataRow row2 in journalData.JournalDetailsTable.Rows)
				{
					string text2 = baseCurrencyID;
					decimal d = result2;
					string a = text;
					string text3 = row2["PayeeID"].ToString();
					string a2 = row2["PayeeType"].ToString();
					bool result3 = false;
					bool result4 = false;
					bool.TryParse(new Databases(base.DBConfig).ExistFieldValue("Customer", "CustomerID", text3).ToString(), out result4);
					if (result4 && a2 != "V" && a2 != "E")
					{
						bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "IsParentPosting", "CustomerID", text3, sqlTransaction).ToString(), out result3);
					}
					string text4 = "";
					string text5 = "";
					if (result3)
					{
						text4 = new Databases(base.DBConfig).GetFieldValue("Customer", "ParentCustomerID", "CustomerID", text3, sqlTransaction).ToString();
						text5 = new Databases(base.DBConfig).GetFieldValue("Customer", "ARAccountID", "CustomerID", text4, sqlTransaction).ToString();
						row2["PayeeID"] = text4;
						bool result5 = false;
						bool.TryParse(row2["IsARAP"].ToString(), out result5);
						if (result5)
						{
							row2["AccountID"] = text5;
						}
					}
					if (row2["CurrencyID"] != DBNull.Value && row2["CurrencyID"].ToString() != "")
					{
						text2 = row2["CurrencyID"].ToString();
					}
					else
					{
						row2["CurrencyID"] = dataRow["CurrencyID"];
						row2["CurrencyRate"] = dataRow["CurrencyRate"];
						if (dataRow["CurrencyID"] != DBNull.Value)
						{
							text2 = dataRow["CurrencyID"].ToString();
						}
					}
					if (row2["CurrencyRate"] != DBNull.Value)
					{
						d = decimal.Parse(row2["CurrencyRate"].ToString());
						a = new Currencies(base.DBConfig).GetCurrencyRateType(text2);
						if (text2 == baseCurrencyID)
						{
							d = 1m;
						}
					}
					bool result6 = false;
					bool.TryParse(row2["IsBaseOnly"].ToString(), out result6);
					if (text2 == baseCurrencyID)
					{
						row2["DebitFC"] = DBNull.Value;
						row2["CreditFC"] = DBNull.Value;
					}
					else
					{
						decimal result7 = default(decimal);
						if (row2["DebitFC"] != DBNull.Value)
						{
							decimal.TryParse(row2["DebitFC"].ToString(), out result7);
							if (result6)
							{
								row2["Debit"] = row2["Debit"];
							}
							else
							{
								result7 = ((!(a == "M")) ? Math.Round(result7 / d, currencyDecimalPoints, MidpointRounding.AwayFromZero) : Math.Round(result7 * d, currencyDecimalPoints, MidpointRounding.AwayFromZero));
								if (result7 == 0m)
								{
									row2["Debit"] = DBNull.Value;
								}
								else
								{
									row2["Debit"] = result7;
								}
							}
						}
						result7 = default(decimal);
						if (row2["CreditFC"] != DBNull.Value)
						{
							decimal.TryParse(row2["CreditFC"].ToString(), out result7);
							if (result6)
							{
								row2["Credit"] = row2["Credit"];
							}
							else
							{
								result7 = ((!(a == "M")) ? Math.Round(result7 / d, currencyDecimalPoints, MidpointRounding.AwayFromZero) : Math.Round(result7 * d, currencyDecimalPoints, MidpointRounding.AwayFromZero));
								if (result7 == 0m)
								{
									row2["Credit"] = DBNull.Value;
								}
								else
								{
									row2["Credit"] = result7;
								}
							}
						}
					}
				}
				if (flag2)
				{
					decimal d2 = default(decimal);
					decimal d3 = default(decimal);
					foreach (DataRow row3 in journalData.JournalDetailsTable.Rows)
					{
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						decimal.TryParse(row3["Debit"].ToString(), out result8);
						decimal.TryParse(row3["Credit"].ToString(), out result9);
						if (row3["AccountID"].ToString() == "")
						{
							throw new Exception("Account is not enterd for journal row.");
						}
						if (result8 > 0m && result9 > 0m)
						{
							throw new Exception("Either debit or credit should be zero");
						}
						if (result8 < 0m || result9 < 0m)
						{
							throw new Exception("Amount cannot be negative.");
						}
						d2 += result8;
						d3 += result9;
					}
					decimal num2 = d2 - d3;
					if (Math.Abs(num2) > Convert.ToDecimal(0.09))
					{
						throw new CompanyException("Total debit must equal total credit.");
					}
					int num3 = 0;
					while (num2 != 0m)
					{
						num3++;
						if (num3 > 9)
						{
							break;
						}
						foreach (DataRow row4 in journalData.JournalDetailsTable.Rows)
						{
							decimal num4 = default(decimal);
							decimal num5 = default(decimal);
							if (num2 == 0m)
							{
								break;
							}
							if (row4["Debit"] != DBNull.Value)
							{
								num4 = Convert.ToDecimal(row4["Debit"].ToString());
							}
							if (row4["Credit"] != DBNull.Value)
							{
								num5 = Convert.ToDecimal(row4["Credit"].ToString());
							}
							if ((!(num2 > 0m) || !(num5 == 0m)) && (!(num2 < 0m) || !(num4 == 0m)))
							{
								if (num2 > 0m)
								{
									if (num3 >= 3 || !(num5 - Math.Round(num5, 0) == 0m))
									{
										row4["Credit"] = num5 + 0.01m;
										num2 -= 0.01m;
									}
								}
								else if (num3 >= 3 || !(num4 - Math.Round(num4, 0) == 0m))
								{
									row4["Debit"] = num4 + Convert.ToDecimal(0.01);
									num2 += 0.01m;
								}
							}
						}
					}
				}
				flag &= ValidateData(journalData, sqlTransaction);
				if (!flag)
				{
					throw new CompanyException("Debit is not equal to credit.");
				}
				insertUpdateJournalCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(journalData, "Journal", insertUpdateJournalCommand)) : (flag & Insert(journalData, "Journal", insertUpdateJournalCommand)));
				foreach (DataRow row5 in journalData.JournalDetailsTable.Rows)
				{
					row5["SysDocID"] = dataRow["SysDocID"];
					row5["VoucherID"] = dataRow["VoucherID"];
				}
				if (flag && journalData.Tables["Journal_Details"].Rows.Count > 0)
				{
					insertUpdateJournalCommand = GetInsertUpdateJournalDetailsCommand(isUpdate: false);
					insertUpdateJournalCommand.Transaction = sqlTransaction;
					flag &= Insert(journalData, "Journal_Details", insertUpdateJournalCommand);
				}
				string text6 = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				if (isUpdate)
				{
					flag &= new ARJournal(base.DBConfig).DeleteARJournal(text6, voucherID, sqlTransaction);
					flag &= new APJournal(base.DBConfig).DeleteAPJournal(text6, voucherID, sqlTransaction);
					flag &= new EmployeeJournal(base.DBConfig).DeleteEmployeeJournal(text6, voucherID, sqlTransaction);
				}
				foreach (DataRow row6 in journalData.JournalDetailsTable.Rows)
				{
					row6["SysDocID"] = dataRow["SysDocID"];
					row6["VoucherID"] = dataRow["VoucherID"];
					string a3 = row6["PayeeType"].ToString();
					bool result10 = false;
					bool.TryParse(row6["IsARAP"].ToString(), out result10);
					if (!(a3 == "") && !(a3 == "A") && result10)
					{
						if (a3 == "C")
						{
							if (!(text6 == "SYS_010"))
							{
								ARJournalData aRJournalData = new ARJournalData();
								DataTable dataTable = aRJournalData.Tables["ARJournal"];
								string text7 = row6["PayeeID"].ToString();
								DateTime invoiceDate = DateTime.Parse(dataRow["JournalDate"].ToString());
								bool result11 = false;
								if (text7 != "")
								{
									bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "IsParentPosting", "CustomerID", text7, sqlTransaction).ToString(), out result11);
								}
								string text8 = "";
								string value = "";
								if (result11)
								{
									text8 = new Databases(base.DBConfig).GetFieldValue("Customer", "ParentCustomerID", "CustomerID", text7, sqlTransaction).ToString();
									value = new Databases(base.DBConfig).GetFieldValue("Customer", "ARAccountID", "CustomerID", text8, sqlTransaction).ToString();
								}
								bool flag3 = false;
								if (row6["Debit"] != DBNull.Value)
								{
									flag3 = true;
								}
								DataRow dataRow5 = dataTable.NewRow();
								dataRow5["SysDocID"] = dataRow["SysDocID"];
								dataRow5["VoucherID"] = dataRow["VoucherID"];
								dataRow5["Reference"] = dataRow["Reference"];
								if (!result11)
								{
									dataRow5["CustomerID"] = row6["PayeeID"];
									dataRow5["ARAccountID"] = row6["AccountID"];
								}
								else
								{
									dataRow5["CustomerID"] = text8;
									dataRow5["ARAccountID"] = value;
								}
								dataRow5["Debit"] = row6["Debit"];
								dataRow5["Credit"] = row6["Credit"];
								dataRow5["DebitFC"] = row6["DebitFC"];
								dataRow5["CreditFC"] = row6["CreditFC"];
								dataRow5["CostCenterID"] = row6["CostCenterID"];
								dataRow5["CurrencyID"] = dataRow["CurrencyID"];
								dataRow5["CurrencyRate"] = dataRow["CurrencyRate"];
								dataRow5["Description"] = row6["Description"];
								dataRow5["ARDate"] = dataRow["JournalDate"];
								dataRow5["JobID"] = row6["JobID"];
								dataRow5["CostCategoryID"] = row6["CostCategoryID"];
								dataRow5["AttributeID1"] = row6["AttributeID1"];
								dataRow5["AttributeID2"] = row6["AttributeID2"];
								if (row6["DueDate"] != DBNull.Value)
								{
									dataRow5["ARDueDate"] = row6["DueDate"];
								}
								else if (flag3)
								{
									dataRow5["ARDueDate"] = new Customers(base.DBConfig).CalculateDueDate(invoiceDate, text7, sqlTransaction);
								}
								dataRow5["PaymentMethodType"] = row6["PaymentMethodType"];
								dataRow5["BankID"] = row6["BankID"];
								dataRow5["ChequeDate"] = row6["CheckDate"];
								dataRow5["ChequeNumber"] = row6["CheckNumber"];
								dataRow5.EndEdit();
								dataTable.Rows.Add(dataRow5);
								flag &= new ARJournal(base.DBConfig).InsertJournal(aRJournalData, sqlTransaction);
							}
						}
						else if (a3 == "V")
						{
							if (!(text6 == "SYS_011"))
							{
								APJournalData aPJournalData = new APJournalData();
								DataTable dataTable2 = aPJournalData.Tables["APJournal"];
								string vendorID = row6["PayeeID"].ToString();
								DateTime invoiceDate2 = DateTime.Parse(dataRow["JournalDate"].ToString());
								bool flag4 = false;
								if (row6["Credit"] != DBNull.Value)
								{
									flag4 = true;
								}
								bool flag5 = false;
								if (result == 244 || result == 245)
								{
									flag5 = true;
								}
								DataRow dataRow6 = dataTable2.NewRow();
								dataRow6["SysDocID"] = dataRow["SysDocID"];
								dataRow6["VoucherID"] = dataRow["VoucherID"];
								dataRow6["VendorID"] = row6["PayeeID"];
								dataRow6["Reference"] = dataRow["Reference"];
								dataRow6["APAccountID"] = row6["AccountID"];
								dataRow6["IsNonStatement"] = flag5;
								dataRow6["Debit"] = row6["Debit"];
								dataRow6["Credit"] = row6["Credit"];
								dataRow6["DebitFC"] = row6["DebitFC"];
								dataRow6["CreditFC"] = row6["CreditFC"];
								dataRow6["CostCenterID"] = row6["CostCenterID"];
								dataRow6["CurrencyID"] = dataRow["CurrencyID"];
								dataRow6["CurrencyRate"] = dataRow["CurrencyRate"];
								dataRow6["Description"] = row6["Description"];
								dataRow6["JobID"] = row6["JobID"];
								dataRow6["CostCategoryID"] = row6["CostCategoryID"];
								dataRow6["AttributeID1"] = row6["AttributeID1"];
								dataRow6["AttributeID2"] = row6["AttributeID2"];
								if (row6["DueDate"] != DBNull.Value)
								{
									dataRow6["APDueDate"] = row6["DueDate"];
								}
								else if (flag4)
								{
									dataRow6["APDueDate"] = new Vendors(base.DBConfig).CalculateDueDate(invoiceDate2, vendorID, sqlTransaction);
								}
								dataRow6["APDate"] = dataRow["JournalDate"];
								dataRow6["PaymentMethodType"] = row6["PaymentMethodType"];
								dataRow6["BankID"] = row6["BankID"];
								dataRow6["ChequeDate"] = row6["CheckDate"];
								dataRow6["ChequeNumber"] = row6["CheckNumber"];
								aPJournalData.Tables["APJournal"].Rows.Add(dataRow6);
								flag &= new APJournal(base.DBConfig).InsertJournal(aPJournalData, sqlTransaction);
							}
						}
						else if (a3 == "E" && !(text6 == "SYS_012"))
						{
							EmployeeJournalData employeeJournalData = new EmployeeJournalData();
							DataRow dataRow7 = employeeJournalData.Tables["Employee_Journal"].NewRow();
							dataRow7["SysDocID"] = dataRow["SysDocID"];
							dataRow7["VoucherID"] = dataRow["VoucherID"];
							dataRow7["EmployeeID"] = row6["PayeeID"];
							dataRow7["Reference"] = dataRow["Reference"];
							dataRow7["AccountID"] = row6["AccountID"];
							dataRow7["Debit"] = row6["Debit"];
							dataRow7["Credit"] = row6["Credit"];
							dataRow7["DebitFC"] = row6["DebitFC"];
							dataRow7["CreditFC"] = row6["CreditFC"];
							dataRow7["CostCenterID"] = row6["CostCenterID"];
							dataRow7["CurrencyID"] = dataRow["CurrencyID"];
							dataRow7["CurrencyRate"] = dataRow["CurrencyRate"];
							dataRow7["Description"] = row6["Description"];
							dataRow7["JournalDate"] = dataRow["JournalDate"];
							dataRow7["PaymentMethodType"] = row6["PaymentMethodType"];
							dataRow7["BankID"] = row6["BankID"];
							dataRow7["ChequeDate"] = row6["CheckDate"];
							dataRow7["ChequeNumber"] = row6["CheckNumber"];
							employeeJournalData.Tables["Employee_Journal"].Rows.Add(dataRow7);
							flag &= new EmployeeJournal(base.DBConfig).InsertJournal(employeeJournalData, sqlTransaction);
						}
					}
				}
				return flag & UpdateTableRowInsertUpdateInfo("Journal", "JournalID", num, sqlTransaction, !isUpdate);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool UpdateJournalBankReconciliation(GLData journalData, bool isUpdate)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string entiyID = journalData.JournalDetailsTable.Rows[0]["JournalID"].ToString();
				string text = string.Empty;
				foreach (DataRow row in journalData.JournalDetailsTable.Rows)
				{
					bool flag2 = false;
					if (row["ReconcileDate"] != DBNull.Value)
					{
						flag2 = true;
					}
					if (flag2)
					{
						string text2 = CommonLib.ToSqlDateTimeString(DateTime.Parse(row["ReconcileDate"].ToString()));
						text = text + " UPDATE Journal_Details SET ReconcileDate = '" + text2 + "'\r\n                            , IsReconciled = 'True', ReconciledBy = '" + base.DBConfig.UserID + "' WHERE JournalDetailID = " + row["JournalDetailID"];
					}
					else
					{
						text = text + " UPDATE Journal_Details SET ReconcileDate = NULL\r\n                            , IsReconciled = NULL, ReconciledBy = NULL  WHERE ReconcileDate IS NOT NULL AND JournalDetailID = " + row["JournalDetailID"];
					}
				}
				flag &= Update(text, sqlTransaction);
				if (!isUpdate)
				{
					return flag;
				}
				AddActivityLog("Customer", entiyID, ActivityTypes.Update, sqlTransaction);
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

		public bool IsJournalInBalance(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) FROM Journal_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					return decimal.Parse(obj.ToString()) == 0m;
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool IsJournalInOpenPeriod(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT JournalDate FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			DateTime t = DateTime.MaxValue;
			if (obj != null && obj.ToString() != "")
			{
				t = DateTime.Parse(obj.ToString());
			}
			obj = new CompanyInformations(base.DBConfig).GetLastClosingDate();
			if (obj != null)
			{
				DateTime t2 = DateTime.Parse(obj.ToString());
				if (t <= t2)
				{
					return false;
				}
				return true;
			}
			return true;
		}

		public bool VoidJournal(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "SELECT JournalDate FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				DateTime t = DateTime.MaxValue;
				if (obj != null && obj.ToString() != "")
				{
					t = DateTime.Parse(obj.ToString());
				}
				obj = new CompanyInformations(base.DBConfig).GetLastClosingDate();
				if (obj != null)
				{
					DateTime t2 = DateTime.Parse(obj.ToString());
					if (t <= t2)
					{
						throw new CompanyException("Cannot delete a transaction in a closed period.", 1038);
					}
				}
				string text = GetJournalID(sysDocID, voucherID, sqlTransaction).ToString();
				if (text == "-1")
				{
					return false;
				}
				exp = "UPDATE JOURNAL_Details SET IsVoid='" + isVoid.ToString() + "'  WHERE JournalID=" + text;
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE JOURNAL SET IsVoid = '" + isVoid.ToString() + "' WHERE JournalID=" + text;
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				flag &= new ARJournal(base.DBConfig).VoidARJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				flag &= new APJournal(base.DBConfig).VoidAPJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				return flag & new EmployeeJournal(base.DBConfig).VoidEmployeeJournal(sysDocID, voucherID, isVoid, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteJournal(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "SELECT JournalDate FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				DateTime t = DateTime.MaxValue;
				if (obj != null && obj.ToString() != "")
				{
					t = DateTime.Parse(obj.ToString());
				}
				obj = new CompanyInformations(base.DBConfig).GetLastClosingDate();
				if (obj != null)
				{
					DateTime t2 = DateTime.Parse(obj.ToString());
					if (t <= t2)
					{
						throw new CompanyException("Cannot delete a transaction in a closed period.", 1038);
					}
				}
				string text = GetJournalID(sysDocID, voucherID, sqlTransaction).ToString();
				flag &= new ARJournal(base.DBConfig).DeleteARJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new APJournal(base.DBConfig).DeleteAPJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new EmployeeJournal(base.DBConfig).DeleteEmployeeJournal(sysDocID, voucherID, sqlTransaction);
				if (text == "-1")
				{
					return true;
				}
				flag &= DeleteDetailsRows(sqlTransaction, text);
				if (flag)
				{
					exp = "DELETE FROM JOURNAL WHERE JournalID=" + text;
					return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteDetailsRows(SqlTransaction sqlTransaction, string journalID)
		{
			bool flag = true;
			try
			{
				string exp = "\tSELECT COUNT(*) FROM Journal_Details WHERE JournalID = " + journalID + " AND IsBilled = 'True'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
				{
					throw new CompanyException("You cannot delete this transaction. One or more rows are billed to a project.");
				}
				string commandText = "DELETE FROM Journal_Details WHERE JournalID = '" + journalID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool ValidateJournalDetailsData(GLData journalData)
		{
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			foreach (DataRow row in journalData.JournalDetailsTable.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(row["Debit"].ToString(), out result);
				decimal.TryParse(row["Credit"].ToString(), out result2);
				if (row["AccountID"].ToString() == "")
				{
					throw new Exception("Account is not enterd for journal row.");
				}
				if (result > 0m && result2 > 0m)
				{
					throw new Exception("Either debit or credit should be zero");
				}
				if (result < 0m || result2 < 0m)
				{
					throw new Exception("Amount cannot be negative.");
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

		public bool InsertJournalDetailsOnly(GLData journalData, SqlTransaction sqlTransaction)
		{
			return InsertJournalDetailsOnly(journalData, isMerging: false, sqlTransaction);
		}

		public bool InsertJournalDetailsOnly(GLData journalData, bool isMerging, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			ValidateData(journalData, sqlTransaction);
			SqlCommand sqlCommand = null;
			try
			{
				string text = "";
				string text2 = "";
				if (journalData.JournalDetailsTable.Rows.Count == 0)
				{
					throw new CompanyException("Journal details table must have a row.");
				}
				text = journalData.JournalDetailsTable.Rows[0]["SysDocID"].ToString();
				text2 = journalData.JournalDetailsTable.Rows[0]["VoucherID"].ToString();
				if (text == "" || text2 == "")
				{
					return false;
				}
				string exp = "SELECT JournalID FROM Journal WHERE SysDocID='" + text + "' AND VoucherID='" + text2 + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					int num = int.Parse(obj.ToString());
					foreach (DataRow row in journalData.JournalDetailsTable.Rows)
					{
						row["JournalID"] = num;
					}
					exp = "SELECT * FROM Journal WHERE SysDocID='" + text + "' AND VoucherID='" + text2 + "'";
					FillDataSet(journalData, "Journal", exp, sqlTransaction);
					DataRow dataRow = journalData.JournalTable.Rows[0];
					if (journalData.Tables["Journal_Details"].Rows.Count > 0)
					{
						journalData.Tables["Journal_Details"].Rows[0]["Reference"].ToString();
						sqlCommand = GetInsertUpdateJournalDetailsCommand(isUpdate: false);
						sqlCommand.Transaction = sqlTransaction;
						flag &= Insert(journalData, "Journal_Details", sqlCommand);
					}
					string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
					int num2 = -1;
					{
						foreach (DataRow row2 in journalData.JournalDetailsTable.Rows)
						{
							string a = row2["PayeeType"].ToString();
							bool result = false;
							bool.TryParse(row2["IsARAP"].ToString(), out result);
							row2["JDDate"] = dataRow["JournalDate"];
							row2["DocType"] = dataRow["SysDocType"];
							if (!(a == "") && !(a == "A") && result)
							{
								if (a == "C")
								{
									ARJournalData aRJournalData = new ARJournalData();
									DataTable dataTable = aRJournalData.Tables["ARJournal"];
									if (row2["AllocationID"] != DBNull.Value)
									{
										num2 = int.Parse(row2["AllocationID"].ToString());
									}
									DataRow dataRow3 = dataTable.NewRow();
									dataRow3["SysDocID"] = row2["SysDocID"];
									dataRow3["VoucherID"] = row2["VoucherID"];
									dataRow3["CustomerID"] = row2["PayeeID"];
									dataRow3["Reference"] = row2["Reference"];
									dataRow3["ARAccountID"] = row2["AccountID"];
									dataRow3["AllocationID"] = row2["AllocationID"];
									dataRow3["Debit"] = row2["Debit"];
									dataRow3["Credit"] = row2["Credit"];
									dataRow3["DebitFC"] = row2["DebitFC"];
									dataRow3["CreditFC"] = row2["CreditFC"];
									if (num2 > 0)
									{
										dataRow3["CurrencyID"] = baseCurrencyID;
									}
									else
									{
										dataRow3["CurrencyID"] = dataRow["CurrencyID"];
									}
									dataRow3["CostCenterID"] = row2["CostCenterID"];
									dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
									dataRow3["Description"] = row2["Description"];
									dataRow3["ARDate"] = dataRow["JournalDate"];
									dataRow3["PaymentMethodType"] = row2["PaymentMethodType"];
									dataRow3["BankID"] = row2["BankID"];
									dataRow3["ChequeDate"] = row2["CheckDate"];
									dataRow3["ChequeNumber"] = row2["CheckNumber"];
									dataRow3.EndEdit();
									dataTable.Rows.Add(dataRow3);
									flag &= new ARJournal(base.DBConfig).InsertJournal(aRJournalData, sqlTransaction);
								}
								else if (a == "V")
								{
									APJournalData aPJournalData = new APJournalData();
									DataTable dataTable2 = aPJournalData.Tables["APJournal"];
									if (row2["AllocationID"] != DBNull.Value)
									{
										num2 = int.Parse(row2["AllocationID"].ToString());
									}
									DataRow dataRow4 = dataTable2.NewRow();
									dataRow4["SysDocID"] = dataRow["SysDocID"];
									dataRow4["VoucherID"] = dataRow["VoucherID"];
									dataRow4["VendorID"] = row2["PayeeID"];
									dataRow4["Reference"] = dataRow["Reference"];
									dataRow4["APAccountID"] = row2["AccountID"];
									dataRow4["AllocationID"] = row2["AllocationID"];
									dataRow4["Debit"] = row2["Debit"];
									dataRow4["Credit"] = row2["Credit"];
									dataRow4["DebitFC"] = row2["DebitFC"];
									dataRow4["CreditFC"] = row2["CreditFC"];
									if (num2 > 0)
									{
										dataRow4["CurrencyID"] = baseCurrencyID;
									}
									else
									{
										dataRow4["CurrencyID"] = dataRow["CurrencyID"];
									}
									dataRow4["CostCenterID"] = row2["CostCenterID"];
									dataRow4["CurrencyRate"] = dataRow["CurrencyRate"];
									dataRow4["Description"] = row2["Description"];
									dataRow4["APDate"] = dataRow["JournalDate"];
									dataRow4["PaymentMethodType"] = row2["PaymentMethodType"];
									dataRow4["BankID"] = row2["BankID"];
									dataRow4["ChequeDate"] = row2["CheckDate"];
									dataRow4["ChequeNumber"] = row2["CheckNumber"];
									aPJournalData.Tables["APJournal"].Rows.Add(dataRow4);
									flag &= new APJournal(base.DBConfig).InsertJournal(aPJournalData, sqlTransaction);
								}
								else if (a == "E")
								{
									EmployeeJournalData employeeJournalData = new EmployeeJournalData();
									DataRow dataRow5 = employeeJournalData.Tables["Employee_Journal"].NewRow();
									dataRow5["SysDocID"] = dataRow["SysDocID"];
									dataRow5["VoucherID"] = dataRow["VoucherID"];
									dataRow5["EmployeeID"] = row2["PayeeID"];
									dataRow5["Reference"] = dataRow["Reference"];
									dataRow5["AccountID"] = row2["AccountID"];
									dataRow5["Debit"] = row2["Debit"];
									dataRow5["Credit"] = row2["Credit"];
									dataRow5["DebitFC"] = row2["DebitFC"];
									dataRow5["CreditFC"] = row2["CreditFC"];
									dataRow5["CostCenterID"] = row2["CostCenterID"];
									dataRow5["CurrencyID"] = dataRow["CurrencyID"];
									dataRow5["CurrencyRate"] = dataRow["CurrencyRate"];
									dataRow5["Description"] = row2["Description"];
									dataRow5["JournalDate"] = dataRow["JournalDate"];
									dataRow5["PaymentMethodType"] = row2["PaymentMethodType"];
									dataRow5["BankID"] = row2["BankID"];
									dataRow5["ChequeDate"] = row2["CheckDate"];
									dataRow5["ChequeNumber"] = row2["CheckNumber"];
									employeeJournalData.Tables["Employee_Journal"].Rows.Add(dataRow5);
									flag &= new EmployeeJournal(base.DBConfig).InsertJournal(employeeJournalData, sqlTransaction);
								}
							}
						}
						return flag;
					}
				}
				exp = "SELECT SysDocType FROM System_Document WHERE SysDocID = '" + text + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || string.IsNullOrEmpty(obj.ToString()))
				{
					throw new CompanyException("System Document is not available or faulty for DocID: " + text);
				}
				Convert.ToInt16(obj.ToString());
				return new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertCOGSDiffJournalDetails(GLData journalData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			ValidateDetailsOnlyData(journalData, throwError: true, sqlTransaction);
			SqlCommand sqlCommand = null;
			try
			{
				Hashtable hashtable = new Hashtable();
				if (journalData.JournalDetailsTable.Rows.Count == 0)
				{
					throw new CompanyException("Journal details table must have a row.");
				}
				string text = "";
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SysDocID", typeof(string));
				dataTable.Columns.Add("VoucherID", typeof(string));
				string exp = "  CREATE TABLE #TMP_JD (SysDocID nvarchar(7),VoucherID nvarchar(15)) ";
				ExecuteNonQuery(exp, sqlTransaction);
				foreach (DataRow row in journalData.JournalDetailsTable.Rows)
				{
					string text2 = row["SysDocID"].ToString() + row["VoucherID"].ToString();
					if (text2 == "")
					{
						throw new CompanyException("Voucher information not set for journal rows.");
					}
					if (!hashtable.ContainsKey(text2))
					{
						hashtable.Add(text2, new KeyValuePair<string, string>(row["SysDocID"].ToString(), row["VoucherID"].ToString()));
						if (text != "")
						{
							text += ",";
						}
						text = text + "'" + text2 + "'";
						dataTable.Rows.Add(row["SysDocID"].ToString(), row["VoucherID"].ToString());
					}
				}
				SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(base.DBConfig.Connection, SqlBulkCopyOptions.Default, sqlTransaction);
				sqlBulkCopy.DestinationTableName = "#TMP_JD";
				sqlBulkCopy.WriteToServer(dataTable);
				exp = "SELECT SysDocID+VoucherID AS DocKey, JournalID,JournalDate,SysDocType,IsVoid FROM Journal J WHERE Exists  (SELECT * FROM #TMP_JD TMP WHERE TMP.SysDocID = J.SysDocID AND TMP.VoucherID = J.VoucherID )";
				exp += " DROP TABLE #TMP_JD";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Journal", exp, sqlTransaction);
				List<int> list = new List<int>();
				foreach (DataRow row2 in dataSet.Tables[0].Rows)
				{
					list.Add(int.Parse(row2["JournalID"].ToString()));
				}
				foreach (DataRow row3 in journalData.JournalDetailsTable.Rows)
				{
					string text3 = row3["SysDocID"].ToString();
					string text4 = row3["VoucherID"].ToString();
					string str = text3 + text4;
					DataRow[] array = dataSet.Tables[0].Select("DocKey = '" + str + "'");
					if (array.Length != 0)
					{
						row3["JournalID"] = array[0]["JournalID"];
						row3["JDDate"] = array[0]["JournalDate"];
						row3["DocType"] = array[0]["SysDocType"];
					}
					else
					{
						int nextJournalID = GetNextJournalID(sqlTransaction);
						string exp2 = " INSERT INTO Journal (JournalID,JournalDate,SysDocID,VoucherID,SysDocType,Reference,DateCreated)\r\n                                        VALUES (" + nextJournalID + ", (SELECT TOP 1 TransactionDate FROM Inventory_Transactions WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text4 + "'),\r\n                                                '" + text3 + "','" + text4 + "',  (SELECT TOP 1 SysDocType FROM Inventory_Transactions WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text4 + "')\r\n                                            ,'SYS_COGS',(SELECT TOP 1 TransactionDate FROM Inventory_Transactions WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text4 + "'))";
						flag &= (ExecuteNonQuery(exp2, sqlTransaction) > 0);
						exp = " SELECT JournalDate,SysDocType FROM Journal WHERE JournalID = " + nextJournalID;
						DataSet dataSet2 = new DataSet();
						FillDataSet(dataSet2, "Journal", exp, sqlTransaction);
						if (dataSet2 != null && dataSet2.Tables.Count > 0 && dataSet2.Tables[0].Rows.Count > 0)
						{
							DataRow dataRow4 = dataSet2.Tables["Journal"].Rows[0];
							row3["JDDate"] = dataRow4["JournalDate"];
							row3["DocType"] = dataRow4["SysDocType"];
						}
						row3["JournalID"] = nextJournalID;
						dataSet.Tables[0].Rows.Add(text3 + text4, nextJournalID);
					}
				}
				if (journalData.Tables["Journal_Details"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateJournalDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					dsCommand.UpdateBatchSize = 1000;
					insertCommand.UpdatedRowSource = UpdateRowSource.None;
					flag &= Insert(journalData, "Journal_Details", sqlCommand);
					insertCommand.UpdatedRowSource = UpdateRowSource.Both;
					dsCommand.UpdateBatchSize = 1;
				}
				if (list.Count > 0)
				{
					return flag & MergeSYS_COGSJournals(list.ToArray(), sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertUpdateJournalVoucher(GLData journalData, bool isUpdate)
		{
			return InsertUpdateJournalVoucher(journalData, isUpdate, null);
		}

		public bool InsertUpdateJournalVoucher(GLData journalData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			bool flag2 = false;
			try
			{
				DataRow dataRow = journalData.JournalTable.Rows[0];
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				else
				{
					flag2 = true;
				}
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (text.Trim() == "" || text2.Trim() == "")
				{
					throw new CompanyException("VoucherID and SysDocID cannot be empty.");
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Journal", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in journalData.JournalDetailsTable.Rows)
				{
					if (row["PayeeID"] != DBNull.Value)
					{
						string text3 = row["PayeeID"].ToString();
						string text5 = (string)(row["PayeeType"] = row["PayeeType"].ToString());
						string text6 = "";
						if (row["AccountID"] != DBNull.Value)
						{
							text6 = row["AccountID"].ToString();
						}
						if (text5 == "C")
						{
							text6 = new Customers(base.DBConfig).GetCustomerARAccountID(text2, text3);
						}
						else if (text5 == "V")
						{
							text6 = new Vendors(base.DBConfig).GetVendorAPAccountID(text2, text3);
						}
						else if (text5 == "E")
						{
							text6 = new Employees(base.DBConfig).GetEmployeeAccountID(text2, text3);
						}
						if (text6 == "")
						{
							throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
						}
						row["AccountID"] = text6;
						switch (text5)
						{
						case "C":
						case "V":
						case "E":
							row["IsARAP"] = true;
							break;
						}
					}
				}
				flag &= InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Journal", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				flag = (isUpdate ? (flag & AddActivityLog("Journal", text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Journal", text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Journal", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.GJournal, text2, text, "Journal", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (!flag2)
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		public bool VoidJournalVoucher(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				if (!isVoid)
				{
					AddActivityLog("Journal", voucherID, sysDocID, ActivityTypes.Unvoid, sqlTransaction);
					return flag;
				}
				AddActivityLog("Journal", voucherID, sysDocID, ActivityTypes.Void, sqlTransaction);
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

		public bool DeleteJournalVoucher(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Journal", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public GLData GetJournalVoucherByID(string sysDocID, string voucherID)
		{
			try
			{
				GLData gLData = new GLData();
				string cmdText = "SELECT * FROM JOURNAL WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(gLData, "Journal", sqlCommand);
				if (gLData == null || gLData.Tables.Count == 0 || gLData.Tables["Journal"].Rows.Count == 0)
				{
					return null;
				}
				string str = gLData.JournalTable.Rows[0]["JournalID"].ToString();
				cmdText = "SELECT JD.*,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName+' '+Employee.MiddleName+' '+Employee.LastName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM JOURNAL_DETAILS JD LEFt OUTER JOIN \r\n                        Account ON JD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON JD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID  \r\n                        WHERE JournalID=" + str + " Order by RowIndex";
				FillDataSet(gLData, "Journal_Details", cmdText);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		private decimal GetGroupBalance(DataSet data, string tableName, string groupID)
		{
			decimal result = default(decimal);
			DataRow[] array = data.Tables[tableName].Select("ParentID='" + groupID + "'");
			for (int i = 0; i < array.Length; i++)
			{
				result += decimal.Parse(array[i]["Balance"].ToString());
				result += GetGroupBalance(data, tableName, array[i]["GroupID"].ToString());
			}
			return result;
		}

		private decimal GetGroupCredit(DataSet data, string tableName, string groupID)
		{
			decimal result = default(decimal);
			DataRow[] array = data.Tables[tableName].Select("ParentID='" + groupID + "'");
			for (int i = 0; i < array.Length; i++)
			{
				result += decimal.Parse(array[i]["Credit"].ToString());
				result += GetGroupCredit(data, tableName, array[i]["GroupID"].ToString());
			}
			return result;
		}

		private decimal GetGroupDebit(DataSet data, string tableName, string groupID)
		{
			decimal result = default(decimal);
			DataRow[] array = data.Tables[tableName].Select("ParentID='" + groupID + "'");
			for (int i = 0; i < array.Length; i++)
			{
				result += decimal.Parse(array[i]["Debit"].ToString());
				result += GetGroupDebit(data, tableName, array[i]["GroupID"].ToString());
			}
			return result;
		}

		public DataSet GetBalanceSheetComparisonReport(DateTime to)
		{
			string str = CommonLib.ToSqlDateTimeString(to);
			string str2 = CommonLib.ToSqlDateTimeString(to.AddYears(-1));
			DataSet dataSet = new DataSet();
			string textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL \r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L FROM ACCGroups";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Asset", textCommand);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables["Asset"].Columns["GroupID"]
			};
			dataSet2.Relations.Add("Rel2", dataSet2.Tables["Asset"].Columns["GroupID"], dataSet2.Tables["Asset"].Columns["ParentID"]);
			textCommand = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)) AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID=1 AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'\r\n                            GROUP BY A.AccountID,AccountName,TypeID";
			textCommand = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)) AS PrevBalance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID=1 AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str2 + "'\r\n                            GROUP BY A.AccountID,AccountName,TypeID";
			FillDataSet(dataSet, "Asset", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Asset"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID,AccountName,TypeID,\r\n                        SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID = 2 AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'\r\n                        GROUP BY A.AccountID,AccountName,TypeID";
			FillDataSet(dataSet, "Liability", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Liability"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID,AccountName,TypeID,\r\n                        SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS PrevBalance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID = 2 AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str2 + "'\r\n                        GROUP BY A.AccountID,AccountName,TypeID";
			FillDataSet(dataSet, "Liability", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Liability"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID,AccountName,TypeID,\r\n                        SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID = 5 AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'\r\n                        GROUP BY A.AccountID,AccountName,TypeID";
			FillDataSet(dataSet, "Equity", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Equity"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID,AccountName,TypeID,\r\n                        SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS PrevBalance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID = 5  AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str2 + "'\r\n                        GROUP BY A.AccountID,AccountName,TypeID";
			FillDataSet(dataSet, "Equity", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Equity"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			textCommand = "Select 0.0 AS Total,\r\n                    SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)) AS RetainedEarning,\r\n                    0.0 AS PrevTotal,0.0 AS PrevRetainedEarning\r\n                    FROM Account  A\r\n                    INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                    INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                    INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID IN (3,4) AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'";
			FillDataSet(dataSet, "RetainedEarning", textCommand);
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal.TryParse(dataSet.Tables[0].Rows[0]["RetainedEarning"].ToString(), out result);
			foreach (DataRow row in dataSet2.Tables["Liability"].Rows)
			{
				decimal result2 = default(decimal);
				decimal.TryParse(row["Balance"].ToString(), out result2);
				num += result2;
			}
			num += result;
			dataSet.Tables[0].Rows[0]["Total"] = num;
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			textCommand = "Select 0.0 AS PrevTotal,\r\n                    SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)) AS PrevRetainedEarning\r\n                    FROM Account  A\r\n                    INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                    INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                    INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE TypeID IN (3,4) AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'";
			FillDataSet(dataSet, "RetainedEarning", textCommand);
			num = default(decimal);
			result = default(decimal);
			decimal.TryParse(dataSet.Tables[0].Rows[0]["PrevRetainedEarning"].ToString(), out result);
			foreach (DataRow row2 in dataSet2.Tables["Liability"].Rows)
			{
				decimal result3 = default(decimal);
				decimal.TryParse(row2["PrevBalance"].ToString(), out result3);
				num += result3;
			}
			num += result;
			dataSet2.Tables["RetainedEarning"].Rows[0]["PrevRetainedEarning"] = result;
			dataSet2.Tables["RetainedEarning"].Rows[0]["PrevTotal"] = num;
			return dataSet2;
		}

		private int GetNextJournalID(SqlTransaction sqlTransaction)
		{
			string exp = "SELECT Max(JournalID) FROM Journal";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj != DBNull.Value)
			{
				return int.Parse(obj.ToString()) + 1;
			}
			return 1;
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

		public GLData GetJournalVoucherToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				GLData gLData = new GLData();
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				if (string.IsNullOrEmpty(sysDocID) || string.IsNullOrEmpty(text))
				{
					return null;
				}
				string cmdText = "SELECT JournalID, JournalDate, JOURNAL.SysDocID, VoucherID, JOURNAL.SysDocType, Reference, Reference2, ISNULL(CurrencyRate, 1),Narration,Note,StoreID,IsVoid,SD.DocName AS [Form Name],\r\n                                ISNULL(CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,JOURNAL.CreatedBy FROM JOURNAL \r\n                                LEFT JOIN System_Document SD On SD.SysDocID=JOURNAL.SysDocID\r\n                               WHERE JOURNAL.SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(gLData, "Journal", sqlCommand);
				if (gLData == null || gLData.Tables.Count == 0 || gLData.Tables["Journal"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT JD.*,(SELECT AccountName FROM Account A WHERE A.AccountID = JD.AccountID) AS PayeeAccountName,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName,Account.Alias,\r\n                        (CASE PayeeType WHEN 'C' THEN Customer.TaxIDNumber\r\n                        WHEN 'V' THEN Vendor.TaxIDNumber\r\n                        WHEN 'E' THEN 'NA'\r\n                        ELSE 'NA' END) AS TaxIDNumber\r\n                        FROM JOURNAL_DETAILS JD LEFt OUTER JOIN \r\n                        Account ON JD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON JD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID  \r\n                        \r\n                        WHERE JD.SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ") Order By JD.RowIndex";
				FillDataSet(gLData, "Journal_Details", cmdText);
				gLData.Relations.Add("JournalEntry", new DataColumn[2]
				{
					gLData.Tables["Journal"].Columns["SysDocID"],
					gLData.Tables["Journal"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					gLData.Tables["Journal_Details"].Columns["SysDocID"],
					gLData.Tables["Journal_Details"].Columns["VoucherID"]
				}, createConstraints: false);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public GLData GetBankReconciliationList(string accountID, DateTime fromDateTime, DateTime toDateTime, bool reconciled)
		{
			try
			{
				GLData gLData = new GLData();
				string text = StoreConfiguration.ToSqlDateTimeString(fromDateTime);
				string text2 = StoreConfiguration.ToSqlDateTimeString(toDateTime);
				string text3 = "SELECT DISTINCT JD.JournalDetailID, J.JournalID,J.SysDocID,J.VoucherID,JournalDate,J.Reference Ref,CS.VoucherID AS SendNumber,RCQ.SendDate, SysDocType,JD.CheckID,JD.CheckbookID, \t\t\t\t\t\t\t\r\n                                CheckNumber,ChequeDate,RCQ.ReceiptDate AS ChequeReceiptDate, ReconcileDate,SendReference, Description,                            \r\n                                 Debit , Credit     FROM Journal_Details JD INNER JOIN  Journal J ON J.JournalID=JD.JournalID \t\t\r\n                                  OUTER APPLY   (SELECT TOP 1 * FROM Cheque_Send_Detail CSD WHERE CSD.ChequeID = JD.CheckID) CS               \t\t \r\n                                 LEFT OUTER JOIN Cheque_Received RCQ ON RCQ.ChequeID = JD.CheckID\r\n                             WHERE ISNULL(J.IsVoid,'False') = 'False' AND AccountID = '" + accountID + "'";
				if (!reconciled)
				{
					text3 += " AND ISNULL(IsReconciled ,0) <> 1 ";
				}
				text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "' ORDER BY JournalDate, Debit";
				FillDataSet(gLData, "Journal_Details", text3);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBankReconciliationToPrint(string accountId, DateTime fromDateTime, DateTime toDateTime, bool reconciled)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(fromDateTime);
			string text2 = StoreConfiguration.ToSqlDateTimeString(toDateTime);
			string text3 = "SELECT JD.JournalDetailID, J.JournalID,J.SysDocID,J.VoucherID, AccountId, (SELECT AccountName FROM Account WHERE AccountID = JD.AccountID) AS AccountName,JournalDate,J.Reference Ref,Note, CheckDate, ReconcileDate, Description,\r\n                             ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit, IsReconciled\r\n                             FROM Journal J LEFT OUTER JOIN Journal_Details JD\r\n                             ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                             WHERE AccountID = '" + accountId + "'";
			if (!reconciled)
			{
				text3 += " AND ISNULL(IsReconciled ,0) <> 1 ";
			}
			text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "'";
			FillDataSet(dataSet, "Journal_Details", text3);
			return dataSet;
		}

		public DataSet GetJournalDistributionList(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT  DISTINCT AccountID [Account Code], (SELECT AccountName FROM Account A WHERE A.AccountID = JD.AccountID) [Account Name],  \r\n\t\t\t\t\t        (SELECT CC.CostCenterName  FROM Cost_Center CC WHERE CC.CostCenterID = JD.CostCenterID) [Cost Center], \r\n                            JobID [Project Code], CostCategoryID [Cost Category], Debit, Credit\r\n                            FROM Journal J LEFT OUTER JOIN Journal_Details JD\r\n                            ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                            WHERE JD.VoucherID = '" + voucherID + "' AND JD.SysDocID = '" + sysDocID + "'");
			FillDataSet(dataSet, "Journal_Details", sqlCommand);
			return dataSet;
		}

		public GLData GetJournalDistributionSummary(string sysDocID, string voucherID)
		{
			try
			{
				GLData gLData = new GLData();
				string cmdText = "SELECT * FROM JOURNAL WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(gLData, "Journal", sqlCommand);
				if (gLData == null || gLData.Tables.Count == 0 || gLData.Tables["Journal"].Rows.Count == 0)
				{
					return null;
				}
				string str = gLData.JournalTable.Rows[0]["JournalID"].ToString();
				cmdText = "SELECT DISTINCT JD.JournalID, JournalDetailID, AccountID [Account Code], (SELECT AccountName FROM Account A WHERE A.AccountID = JD.AccountID) [Account Name],  \r\n\t\t\t\t\t        (SELECT CC.CostCenterName  FROM Cost_Center CC WHERE CC.CostCenterID = JD.CostCenterID) [Cost Center], \r\n                            JobID [Project Code], CostCategoryID [Cost Category], Debit, Credit, PayeeID [Payee ID],  \r\n                            CASE JD.PayeeType WHEN 'C' THEN (SELECT CustomerName FROM Customer C WHERE C.CustomerID = JD.PayeeID)\r\n                                WHEN 'V' THEN (SELECT VendorName FROM Vendor V WHERE V.VendorID = JD.PayeeID)\r\n                                WHEN 'A' THEN (SELECT AccountName FROM Account A WHERE A.AccountID = JD.PayeeID)\r\n                                WHEN 'E' THEN (SELECT FirstName + ' ' + LastName FROM Employee E WHERE E.EmployeeID = JD.PayeeID) END AS [Payee Name]\r\n                            FROM Journal J LEFT OUTER JOIN Journal_Details JD\r\n                            ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID  \r\n                            WHERE JD.JournalID = " + str;
				FillDataSet(gLData, "Journal_Details", cmdText);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public GLData GetDocumentInformation(string sysDocID, string voucherID)
		{
			try
			{
				GLData gLData = new GLData();
				SqlCommand sqlCommand = new SqlCommand("SELECT * FROM JOURNAL WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'");
				FillDataSet(gLData, "Journal", sqlCommand);
				if (gLData == null || gLData.Tables.Count == 0 || gLData.Tables["Journal"].Rows.Count == 0)
				{
					return null;
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJVList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT  DISTINCT ISNULL(J.IsVoid,'False') AS V,J.SysDocID [Doc ID],J.VoucherID [Doc Number],JournalDate [Date],J.Reference Ref,Note,\r\n                            SUM(ISNULL(Debit,0)) AS Amount\r\n                            FROM Journal J LEFT OUTER JOIN Journal_Details JD\r\n                            ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                            WHERE J.SysDocID='JVE001' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(J.IsVoid,'False')='False'";
			}
			text3 += " GROUP BY J.IsVoid,JournalDate,J.SysDocID,J.VoucherID,SysDocType,J.Reference,J.CurrencyID,J.CurrencyRate,Narration,Note";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Return", sqlCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT DISTINCT ISNULL(J.IsVoid,'False') AS V, J.JournalID AS [Journal ID],J.SysDocID ,J.VoucherID,JournalDate [Date],SysDocType [Type],\r\n                            ISNULL(J.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur],J.Reference,Note,\r\n                            SUM(ISNULL(DebitFC,ISNULL(Debit,0))) AS Amount\r\n                            FROM Journal J LEFT OUTER JOIN Journal_Details JD\r\n                            ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID WHERE 1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(J.IsVoid,'False')='False'";
			}
			text3 += " GROUP BY J.IsVoid,J.JournalID,JournalDate,J.SysDocID,J.VoucherID,SysDocType,J.Reference,J.CurrencyID,J.CurrencyRate,Narration,Note\r\n                            ORDER BY J.JournalID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Journal", sqlCommand);
			text3 = "Select JD.JournalID,JD.SysDocID,JD.VoucherID,JD.AccountID, AC.AccountName, JD.Description,JD.Reference,PayeeID + ' - ' +\r\n                        CASE PayeeType WHEN 'V' THEN (SELECT VendorName FROM Vendor WHERE VendorID = PayeeID)\r\n\t\t\t                           WHEN 'C' THEN (SELECT CustomerName FROM Customer WHERE CustomerID = PayeeID) \r\n\t\t\t                           WHEN 'A' THEN (SELECT AccountName FROM Account WHERE Account.AccountID = PayeeID) \r\n\t\t\t                           WHEN 'E' THEN (SELECT FirstName + ' ' + LastName FROM Employee WHERE EmployeeID = PayeeID) END AS Payee, Debit,Credit,DebitFC,CreditFC\r\n                        FROM Journal_Details JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID  INNER JOIN Journal J ON J.JournalID = JD.JournalID WHERE 1=1 ";
			if (!showVoid)
			{
				text3 += " AND ISNULL(JD.IsVoid,'False')='False'";
			}
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "'";
			}
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Journal_Details", sqlCommand);
			dataSet.Relations.Add("J_JD", new DataColumn[2]
			{
				dataSet.Tables["Journal"].Columns["SysDocID"],
				dataSet.Tables["Journal"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Journal_Details"].Columns["SysDocID"],
				dataSet.Tables["Journal_Details"].Columns["VoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetTrialBalanceList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "Select AG.GroupID,AG.GroupName,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS Debit,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS Credit\r\n                        FROM  Account AC (nolock) INNER JOIN Journal_Details JD (nolock) ON JD.AccountID=AC.AccountID\r\n                        INNER JOIN Journal J (nolock) ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                        INNER JOIN Account_Group AG ON AG.GroupID=AC.GroupID\r\n                        WHERE  1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(J.IsVoid,'False')='False'";
			}
			text3 += " GROUP BY AG.GroupID,AG.GroupName";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TrialBalance_Header", sqlCommand);
			return dataSet;
		}

		public DataSet GetClosingIncomeExpenseList(string fiscalYearID)
		{
			try
			{
				return GetClosingIncomeExpenseList(fiscalYearID, null);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetClosingIncomeExpenseList(string fiscalYearID, SqlTransaction sqlTransaction)
		{
			FiscalYearData fiscalYearByID = new FiscalYear(base.DBConfig).GetFiscalYearByID(fiscalYearID);
			if (fiscalYearByID == null || fiscalYearByID.Tables.Count == 0 || fiscalYearByID.Tables[0].Rows.Count == 0)
			{
				return null;
			}
			DateTime dateTime = DateTime.Parse(fiscalYearByID.Tables[0].Rows[0]["StartDate"].ToString());
			DateTime date = DateTime.Parse(fiscalYearByID.Tables[0].Rows[0]["EndDate"].ToString());
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(dateTime);
			string text2 = StoreConfiguration.ToSqlDateTimeString(date);
			string text3 = "SELECT JD.AccountID, AC.AccountName,AG.TypeID,\r\n                                CASE WHEN SUM(ISNULL(Debit,0)) > SUM(ISNULL(Credit,0)) THEN SUM(ISNULL(Debit,0)) - SUM(ISNULL(Credit,0)) ELSE NULL END AS Debit,\r\n                                CASE WHEN SUM(ISNULL(Credit,0)) > SUM(ISNULL(Debit,0)) THEN SUM(ISNULL(Credit,0)) - SUM(ISNULL(Debit,0)) ELSE NULL END AS Credit\r\n\r\n                                FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                                INNER JOIN Account AC ON AC.AccountID = JD.AccountID\r\n                                INNER JOIN Account_Group AG ON AG.GroupID = AC.GroupID\r\n\r\n                                 WHERE ISNULL(J.IsVoid,'False')='False' AND AG.TypeID IN (3,4)  ";
			if (dateTime != DateTime.MinValue)
			{
				text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY JD.AccountID,AC.AccountName, AG.TypeID ORDER BY JD.AccountID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			sqlCommand.Transaction = sqlTransaction;
			FillDataSet(dataSet, "Accounts", sqlCommand);
			return dataSet;
		}

		public bool MergeJournalDetailsForCOGSUpdate(int journalID, bool mergeSysWithRegular)
		{
			return MergeJournalDetailsForCOGSUpdate(journalID, mergeSysWithRegular, null);
		}

		public bool MergeJournalDetailsForCOGSUpdate(int journalID, bool mergeSysWithRegular, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			bool flag2 = false;
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = "   SELECT JournalID,SysDocID,VoucherID,AccountID,JDDate,DocType,Debit,Credit,Reference FROM Journal_Details WHERE  JournalID = " + journalID.ToString();
				if (!mergeSysWithRegular)
				{
					text += " AND Reference = 'SYS_COGS' ";
				}
				string text2 = "";
				string text3 = "";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "JD", text);
				if (mergeSysWithRegular && dataSet.Tables["JD"].Rows.Count <= 2)
				{
					return true;
				}
				if (!mergeSysWithRegular && dataSet.Tables["JD"].Rows.Count <= 4)
				{
					return true;
				}
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
					flag2 = true;
				}
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				foreach (DataRow row in dataSet.Tables["JD"].Rows)
				{
					text2 = row["SysDocID"].ToString();
					text3 = row["VoucherID"].ToString();
					string text4 = row["AccountID"].ToString();
					decimal d = default(decimal);
					decimal d2 = default(decimal);
					if (row["Debit"] != DBNull.Value)
					{
						d = decimal.Parse(row["Debit"].ToString());
					}
					if (row["Credit"] != DBNull.Value)
					{
						d2 = decimal.Parse(row["Credit"].ToString());
					}
					if (hashtable.ContainsKey(text4))
					{
						decimal num = decimal.Parse(hashtable[text4].ToString());
						num += Math.Round(d - d2, currencyDecimalPoints);
						hashtable[text4] = num;
					}
					else
					{
						hashtable.Add(text4, Math.Round(d - d2, currencyDecimalPoints));
						arrayList.Add(text4);
					}
				}
				text = " DELETE FROM Journal_Details WHERE Reference = 'SYS_COGS' AND JournalID = " + journalID.ToString();
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				GLData gLData = new GLData();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = DateTime.Today;
				dataRow2["SysDocID"] = text2;
				dataRow2["VoucherID"] = text3;
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				for (int i = 0; i < hashtable.Count; i++)
				{
					string text5 = arrayList[i].ToString();
					decimal num2 = decimal.Parse(hashtable[text5].ToString());
					DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = journalID;
					dataRow3["AccountID"] = text5;
					if (num2 > 0m)
					{
						dataRow3["Debit"] = num2;
						dataRow3["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow3["Debit"] = DBNull.Value;
						dataRow3["Credit"] = Math.Abs(num2);
					}
					dataRow3["SysDocID"] = text2;
					dataRow3["VoucherID"] = text3;
					dataRow3["IsBaseOnly"] = true;
					dataRow3["Reference"] = "SYS_COGS";
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				if (mergeSysWithRegular)
				{
					text = "SELECT AccountID FROM(SELECT AccountID, CASE WHEN (DebitFC IS NULL AND CreditFC IS NULL) \r\n                            THEN COUNT(*) ELSE 5 END AS C FROM Journal_Details WHERE JournalID = " + journalID + " AND Reference <> 'SYS_COGS'\r\n                                GROUP BY AccountID,DebitFC,CreditFC) AS JD  WHERE C>1 ";
					object obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.Parse(obj.ToString()) > 0)
					{
						flag &= InsertJournalDetailsOnly(gLData, isMerging: true, sqlTransaction);
					}
					else
					{
						if (!ValidateData(gLData, sqlTransaction))
						{
							throw new CompanyException("Debit and credit not equal in transaction journal. JournalID:" + journalID);
						}
						text = "";
						foreach (DataRow row2 in gLData.JournalDetailsTable.Rows)
						{
							string text6 = row2["AccountID"].ToString();
							string text7 = " NULL ";
							string text8 = " NULL";
							if (row2["Debit"] != DBNull.Value)
							{
								text7 = row2["Debit"].ToString();
							}
							if (row2["Credit"] != DBNull.Value)
							{
								text8 = row2["Credit"].ToString();
							}
							text = text + " if exists (SELECT JournalID FROM Journal_Details JD WHERE JournalID = " + journalID + " AND AccountID = '" + text6 + "')";
							text = text + " UPDATE Journal_Details SET Debit = " + text7 + ", Credit = " + text8;
							text = text + " WHERE JournalID = " + journalID + " AND AccountID = '" + text6 + "'";
							text = text + "  ELSE\r\n                                    INSERT INTO Journal_Details (JournalID,SysDocID,VoucherID,JDDate,DocType,AccountID,Debit,Credit,Reference) VALUES (" + journalID + "\r\n                                        ,'" + text2 + "','" + text3 + "',(SELECT JournalDate FROM Journal WHERE JournalID = " + journalID + "),(SELECT SysDocType FROM Journal WHERE JournalID = " + journalID + "),'" + text6 + "'," + text7 + "," + text8 + ",'SYS_COGS') ";
						}
						if (text != "")
						{
							int num3 = ExecuteNonQuery(text, sqlTransaction);
							flag &= (num3 >= gLData.JournalDetailsTable.Rows.Count);
							string exp = "select sum(isnull(debit,0)-isnull(credit,0))  FROM Journal_Details where journalid = " + journalID;
							obj = ExecuteScalar(exp, sqlTransaction);
							if (obj != null && obj.ToString() != "" && decimal.Parse(obj.ToString()) != 0m)
							{
								flag = false;
							}
						}
					}
				}
				else if (gLData.JournalDetailsTable.Rows.Count > 0)
				{
					flag &= InsertJournalDetailsOnly(gLData, sqlTransaction);
				}
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

		public bool MergeSYS_COGSJournals(int[] journalIDs, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			bool flag2 = false;
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				if (journalIDs.Length == 0)
				{
					return true;
				}
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < journalIDs.Length; i++)
				{
					arrayList.Add(journalIDs[i].ToString());
				}
				new Databases(base.DBConfig).BulkCopy(arrayList, "#TMP_JNum", "JournalID", sqlTransaction);
				string str = "   SELECT JournalID,SysDocID,VoucherID,JDDate,DocType,AccountID,Debit,Credit,Reference FROM Journal_Details WHERE  JournalID IN (SELECT JournalID FROM #TMP_JNum)";
				str += " AND Reference = 'SYS_COGS' ";
				str += " DROP TABLE #TMP_JNum";
				string text = "";
				string value = "";
				string text2 = "";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "JD", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT SysDocID, LocationID,DivisionID FROM System_Document";
				FillDataSet(dataSet2, "System_Document", str);
				if (dataSet.Tables["JD"].Rows.Count <= 4)
				{
					return true;
				}
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
					flag2 = true;
				}
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList2 = new ArrayList();
				string str2 = "";
				ArrayList arrayList3 = new ArrayList();
				GLData gLData = new GLData();
				DataRow dataRow = gLData.JournalTable.NewRow();
				dataRow["JournalID"] = 0;
				dataRow["JournalDate"] = DateTime.Today;
				dataRow["SysDocID"] = text;
				dataRow["VoucherID"] = value;
				dataRow.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow);
				foreach (int num in journalIDs)
				{
					DataRow[] array = dataSet.Tables["JD"].Select("JournalID = " + num);
					if (array.Length >= 4)
					{
						hashtable.Clear();
						arrayList2.Clear();
						DataRow[] array2 = array;
						foreach (DataRow dataRow2 in array2)
						{
							text = dataRow2["SysDocID"].ToString();
							value = dataRow2["VoucherID"].ToString();
							dataRow["JournalDate"] = dataRow2["JDDate"];
							dataRow["SysDocType"] = dataRow2["DocType"];
							string text3 = dataRow2["AccountID"].ToString();
							decimal d = default(decimal);
							decimal d2 = default(decimal);
							if (dataRow2["Debit"] != DBNull.Value)
							{
								d = decimal.Parse(dataRow2["Debit"].ToString());
							}
							if (dataRow2["Credit"] != DBNull.Value)
							{
								d2 = decimal.Parse(dataRow2["Credit"].ToString());
							}
							if (hashtable.ContainsKey(text3))
							{
								decimal num2 = decimal.Parse(hashtable[text3].ToString());
								num2 += Math.Round(d - d2, currencyDecimalPoints);
								hashtable[text3] = num2;
							}
							else
							{
								hashtable.Add(text3, Math.Round(d - d2, currencyDecimalPoints));
								arrayList2.Add(text3);
							}
						}
						decimal d3 = default(decimal);
						foreach (object value2 in hashtable.Values)
						{
							d3 += decimal.Parse(value2.ToString());
						}
						if (!(d3 != 0m))
						{
							for (int l = 0; l < hashtable.Count; l++)
							{
								string text4 = arrayList2[l].ToString();
								decimal num3 = decimal.Parse(hashtable[text4].ToString());
								text2 = dataSet2.Tables["System_Document"].Select("SysDocID = '" + text + "'")[0]["DivisionID"].ToString();
								DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
								dataRow3.BeginEdit();
								dataRow3["JournalID"] = num;
								dataRow3["AccountID"] = text4;
								if (num3 > 0m)
								{
									dataRow3["Debit"] = num3;
									dataRow3["Credit"] = DBNull.Value;
								}
								else
								{
									dataRow3["Debit"] = DBNull.Value;
									dataRow3["Credit"] = Math.Abs(num3);
								}
								dataRow3["SysDocID"] = text;
								dataRow3["VoucherID"] = value;
								dataRow3["JDDate"] = dataRow["JournalDate"];
								dataRow3["DocType"] = dataRow["SysDocType"];
								dataRow3["IsBaseOnly"] = true;
								dataRow3["Reference"] = "SYS_COGS";
								dataRow3["CompanyID"] = "1";
								dataRow3["DivisionID"] = text2;
								if (dataRow3["JDDate"].IsDBNullOrEmpty())
								{
									throw new CompanyException("Column JDDate does not allow null in MergeSysCOGS.");
								}
								if (dataRow3["DocType"].IsDBNullOrEmpty())
								{
									throw new CompanyException("Column DocType does not allow null in MergeSysCOGS.");
								}
								dataRow3.EndEdit();
								gLData.JournalDetailsTable.Rows.Add(dataRow3);
							}
							arrayList3.Add(num);
						}
					}
				}
				if (arrayList3.Count > 0)
				{
					arrayList = new ArrayList();
					foreach (int item in arrayList3)
					{
						arrayList.Add(item.ToString());
					}
					new Databases(base.DBConfig).BulkCopy(arrayList, "#TMP_JNum", "JournalID", sqlTransaction);
					str2 += " DELETE FROM Journal_Details WHERE Reference = 'SYS_COGS' AND JournalID IN (SELECT JournalID FROM #TMP_JNum)  DROP TABLE #TMP_JNum";
					flag &= (ExecuteNonQuery(str2, sqlTransaction) > 0);
				}
				SqlCommand sqlCommand = null;
				if (gLData.Tables["Journal_Details"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateJournalDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					dsCommand.UpdateBatchSize = 1000;
					insertCommand.UpdatedRowSource = UpdateRowSource.None;
					flag &= Insert(gLData, "Journal_Details", sqlCommand);
					insertCommand.UpdatedRowSource = UpdateRowSource.Both;
					dsCommand.UpdateBatchSize = 1;
				}
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

		public DataSet GetAccountSnapBalance(string AcccoutID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "  SELECT DISTINCT CUS.AccountID,AccountName,\r\n                                ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0))  FROM Journal_Details\r\n                                WHERE AccountID = CUS.AccountID ) ,0)AS Balance                                \r\n                                FROM  Account CUS  WHERE CUS.AccountID = '" + AcccoutID + "'  \r\n                                GROUP BY CUS.AccountID,AccountName";
				FillDataSet(dataSet, "Account", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJournalReference(string AccountID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "  SELECT DISTINCT Reference                                                              \r\n                                FROM Journal_Details WHERE ISNULL(Reference,'')<>''   AND AccountID='" + AccountID + "'\r\n                                ORDER BY Reference DESC";
				FillDataSet(dataSet, "Reference", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTrialBalanceAccountList(string ParamValue1, string ParamValue2, DateTime from, DateTime to)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = StoreConfiguration.ToSqlDateTimeString(from);
				string text2 = StoreConfiguration.ToSqlDateTimeString(to);
				string textCommand = null;
				if (ParamValue1 == "GroupID")
				{
					textCommand = " SELECT JD.AccountID , AccountName , \r\n                                CASE WHEN sum(isnull(Debit, 0)) - sum(isnull(Credit, 0)) > 0 THEN sum (isnull(Debit, 0)) - sum(isnull(Credit, 0)) ELSE NULL END AS Debit,\r\n                                CASE WHEN sum(isnull(Debit, 0)) - sum(isnull(Credit, 0)) < 0 THEN sum(isnull(Credit, 0)) - sum(isnull(Debit, 0)) ELSE NULL END AS Credit\r\n                                FROM Account AC(NOLOCK) INNER JOIN Journal_Details JD(NOLOCK) ON JD.AccountID = AC.AccountID\r\n                                INNER JOIN Journal J(NOLOCK) ON J.JournalID = JD.JournalID AND J.SysDocID = JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                                WHERE JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND isnull(J.IsVoid, 'False') = 'False' AND AC.GroupID = '" + ParamValue2 + "'\r\n                                GROUP BY JD.AccountID, AccountName ";
				}
				if (ParamValue1 == "AccountID")
				{
					textCommand = " SELECT J.JournalDate , J.SysDocID , J.VoucherID , Description , isnull ( Debit , 0 ) as Debit , isnull ( Credit , 0 ) as Credit\r\n                                    FROM Account AC ( NOLOCK ) INNER JOIN Journal_Details JD ( NOLOCK ) ON JD.AccountID = AC.AccountID \r\n                                    INNER JOIN Journal J ( NOLOCK ) ON J.JournalID = JD.JournalID AND J.SysDocID = JD.SysDocID AND J.VoucherID = JD.VoucherID \r\n                                    WHERE JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND isnull ( J.IsVoid , 'False' ) = 'False' AND AC.AccountID = '" + ParamValue2 + "' ";
				}
				FillDataSet(dataSet, "Reference", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet getDetails(string tableName, string DocID, string Value)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = null;
				text = "SELECT * FROM " + tableName + " WHERE VOUCHERID='" + Value + "' AND SYSDOCID='" + DocID + "'";
				FillDataSet(dataSet, "Reference", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet getReference(string Table, string ColumnName, string SysdocId, string VoucherNo)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = null;
				text = "SELECT " + ColumnName + " FROM " + Table + " WHERE VOUCHERID='" + VoucherNo + "' AND SYSDOCID='" + VoucherNo + "'";
				FillDataSet(dataSet, "Reference", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
