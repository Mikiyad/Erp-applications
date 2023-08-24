using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeJournal : StoreObject
	{
		private const string EMPJOURNAL_TABLE = "@EmpJournal";

		private const string EMPLOYEEJOURNALID_PARM = "@EmpJournalID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DEBIT_PARM = "@Debit";

		private const string CREDIT_PARM = "@Credit";

		private const string DEBITFC_PARM = "@DebitFC";

		private const string CREDITFC_PARM = "@CreditFC";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string CHEQUENUMBER_PARM = "@ChequeNumber";

		private const string BANKID_PARM = "@BankID";

		private const string CHEQUEDATE_PARM = "@ChequeDate";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string DESCRIPTION_PARM = "@Description";

		private const string JOURNALID_PARM = "@JournalID";

		private const string JOURNALDATE_PARM = "@JournalDate";

		public EmployeeJournal(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateARJournalText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Journal", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("Reference", "@Reference"), new FieldValue("AccountID", "@AccountID"), new FieldValue("Debit", "@Debit"), new FieldValue("Credit", "@Credit"), new FieldValue("DebitFC", "@DebitFC"), new FieldValue("CreditFC", "@CreditFC"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("ChequeNumber", "@ChequeNumber"), new FieldValue("BankID", "@BankID"), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("Description", "@Description"), new FieldValue("JournalDate", "@JournalDate"), new FieldValue("JournalID", "@JournalID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Journal", new FieldValue("EmpJournalID", "@EmpJournalID", isUpdateConditionField: true));
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
				parameters.Add("@EmpJournalID", SqlDbType.Int);
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Debit", SqlDbType.Money);
			parameters.Add("@Credit", SqlDbType.Money);
			parameters.Add("@DebitFC", SqlDbType.Money);
			parameters.Add("@CreditFC", SqlDbType.Money);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@ChequeNumber", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@JournalID", SqlDbType.NVarChar);
			parameters.Add("@JournalDate", SqlDbType.DateTime);
			if (isUpdate)
			{
				parameters["@EmpJournalID"].SourceColumn = "EmpJournalID";
			}
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@DebitFC"].SourceColumn = "DebitFC";
			parameters["@CreditFC"].SourceColumn = "CreditFC";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@JournalID"].SourceColumn = "JournalID";
			parameters["@JournalDate"].SourceColumn = "JournalDate";
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

		public bool InsertJournal(EmployeeJournalData journalData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateARJournalCommand = GetInsertUpdateARJournalCommand(isUpdate: false);
			try
			{
				_ = journalData.Tables["Employee_Journal"].Rows[0];
				insertUpdateARJournalCommand.Transaction = sqlTransaction;
				flag = Insert(journalData, "Employee_Journal", insertUpdateARJournalCommand);
				decimal result = default(decimal);
				string text = journalData.Tables["Employee_Journal"].Rows[0]["EmployeeID"].ToString();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee", "Balance", "EmployeeID", text, sqlTransaction);
				if (fieldValue != null)
				{
					decimal.TryParse(fieldValue.ToString(), out result);
				}
				foreach (DataRow row in journalData.Tables["Employee_Journal"].Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Debit"].ToString(), out result2);
					decimal.TryParse(row["Credit"].ToString(), out result3);
					result += result2 - result3;
				}
				string commandText = "UPDATE Employee SET Balance=" + result.ToString() + " WHERE EmployeeID='" + text + "'";
				return flag & Update(commandText, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool UpdateJournal(GLData journalData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			ValidateData(journalData);
			SqlCommand insertUpdateARJournalCommand = GetInsertUpdateARJournalCommand(isUpdate: true);
			try
			{
				DataRow dataRow = journalData.JournalTable.Rows[0];
				string text2 = (string)(dataRow["JournalID"] = GetJournalID(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), sqlTransaction).ToString());
				DeleteDetailsRows(sqlTransaction, text2);
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					foreach (DataRow row in journalData.JournalDetailsTable.Rows)
					{
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal.TryParse(row["DebitFC"].ToString(), out result);
						decimal.TryParse(row["CreditFC"].ToString(), out result2);
						if (result != 0m)
						{
							if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
							{
								row["Debit"] = Math.Round(result * d, currencyDecimalPoints);
							}
							else
							{
								row["Debit"] = Math.Round(result / d, currencyDecimalPoints);
							}
							row["Credit"] = DBNull.Value;
						}
						else
						{
							if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
							{
								row["Credit"] = Math.Round(result2 * d, currencyDecimalPoints);
							}
							else
							{
								row["Credit"] = Math.Round(result2 / d, currencyDecimalPoints);
							}
							row["Debit"] = DBNull.Value;
						}
					}
				}
				insertUpdateARJournalCommand.Transaction = sqlTransaction;
				flag &= Update(journalData, "Journal", insertUpdateARJournalCommand);
				UpdateTableRowInsertUpdateInfo("Journal", "JournalID", text2, sqlTransaction, isInsert: true);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool VoidEmployeeJournal(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string text = "";
				string exp = "SELECT EmployeeID FROM Employee_Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (text != "")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Employee", "Balance", "EmployeeID", text, sqlTransaction).ToString(), out result);
					exp = "SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Employee_Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
						exp = "UPDATE Employee SET Balance=" + result.ToString() + " WHERE EmployeeID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
					exp = "UPDATE Employee_Journal SET IsVoid='" + isVoid.ToString() + "'  WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteEmployeeJournal(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string text = "";
				string exp = "SELECT EmployeeID FROM Employee_Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (text != "")
				{
					object obj2 = new Databases(base.DBConfig).GetFieldValue("Employee", "Balance", "EmployeeID", text, sqlTransaction);
					if (obj2 == null)
					{
						obj2 = 0;
					}
					decimal.TryParse(obj2.ToString(), out result);
					exp = "SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Employee_Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					obj2 = ExecuteScalar(exp, sqlTransaction);
					decimal.TryParse(obj2.ToString(), out result2);
					if (result2 != 0m)
					{
						result -= result2;
						exp = "UPDATE Employee SET Balance=" + result.ToString() + " WHERE EmployeeID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
				}
				exp = "DELETE FROM Employee_JOURNAL WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
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
				string commandText = "DELETE FROM Journal_Details WHERE JournalID = '" + journalID + "'";
				return Delete(commandText, sqlTransaction);
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
				textCommand = "SELECT JD.*,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Employee.EmployeeName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM JOURNAL_DETAILS JD LEFt OUTER JOIN \r\n                        Account ON JD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID LEFt OUTER JOIN\r\n                        Vendor ON JD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID  \r\n                        WHERE JournalID=" + str;
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

		public DataSet GetEmployeeSnapBalance(string EmployeeID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "   SELECT DISTINCT CUS.EmployeeID,FirstName,\r\n                              ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0))  FROM Employee_Journal\r\n\t                              WHERE EmployeeID = CUS.EmployeeID ) ,0)AS Balance                               \r\n                                FROM  Employee CUS  WHERE CUS.EmployeeID = '" + EmployeeID + "'  \r\n                                GROUP BY CUS.EmployeeID,FirstName";
				FillDataSet(dataSet, "Account", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
