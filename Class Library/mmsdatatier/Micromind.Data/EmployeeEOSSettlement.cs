using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeEOSSettlement : StoreObject
	{
		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string LASTWORKINGDATE_PARM = "@LastWorkingDate";

		private const string EOSBENEFIT_PARM = "@EOSBenefit";

		private const string LEAVEDUE_PARM = "@LeaveDue";

		private const string DUEAMOUNT_PARM = "@DueAmount";

		private const string SALARYDUE_PARM = "@SalaryDue";

		private const string OTHERBENEFITS_PARM = "@OtherBenefits";

		private const string TOTALPAYABLE_PARM = "@TotalPayable";

		private const string LOAN_PARM = "@Loan";

		private const string OTHERDEDUCTIONID_PARM = "@OtherDeductionID";

		private const string DEDUCTIONAMOUNT_PARM = "@DeductionAmount";

		private const string NETTOTAL_PARM = "@NetTotal";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ISVOID_PARM = "@IsVoid";

		private const string TICKETAMOUNT_PARM = "@TicketAmount";

		private const string EMPLOYEEEOSSETTLEMENT_TABLE = "Employee_EOSSettlement";

		private const string EMPLOYEEEOS_TABLE = "Employee_EOS";

		private const string EMPLOYEEEOSEOSDEDUCTION_TABLE = "Employee_EOS_Deduction_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string EMPLOYEEBASIC_PARM = "@EmployeeBasic";

		private const string CALCULATEDLEAVEAMOUNT_PARM = "@CalculatedLeaveAmount";

		private const string CALCULATEDSALARYAMOUNT_PARM = "@CalculatedSalaryAmount";

		private const string CALCULATEDGRATUITYAMOUNT_PARM = "@CalculatedGratuityAmount";

		private const string PAIDLEAVEAMOUNT_PARM = "@PaidLeaveAmount";

		private const string PAIDSALARYAMOUNT_PARM = "@PaidSalaryAmount";

		private const string PAIDGRATUITYAMOUNT_PARM = "@PaidGratuityAmount";

		private const string PAIDDEDUCTIONAMOUNT_PARM = "@PaidDeductionAmount";

		private const string PAIDLOANAMOUNT_PARM = "@PaidLoanAmount";

		private const string LEAVEDESCRIPTION_PARM = "@LeaveDescription";

		private const string SALARYDESCRIPTION_PARM = "@SalaryDescription";

		private const string GRATUITYDESCRIPTION_PARM = "@GratuityDescription";

		private const string PAIDTICKETAMOUNT_PARM = "@PaidTicketAmount";

		private const string NOTE_PARM = "@Note";

		private const string ISRESIGNED_PARM = "@IsResigned";

		private const string DEDUCTIONID_PARM = "@DeductionID";

		private const string AMOUNT_PARM = "@Amount";

		private const string DESCRIPTION_PARM = "@Description";

		public EmployeeEOSSettlement(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateEmployeeLoanText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_EOSSettlement", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("LastWorkingDate", "@LastWorkingDate"), new FieldValue("EOSBenefit", "@EOSBenefit"), new FieldValue("LeaveDue", "@LeaveDue"), new FieldValue("DueAmount", "@DueAmount"), new FieldValue("SalaryDue", "@SalaryDue"), new FieldValue("OtherBenefits", "@OtherBenefits"), new FieldValue("TotalPayable", "@TotalPayable"), new FieldValue("Loan", "@Loan"), new FieldValue("TicketAmount", "@TicketAmount"), new FieldValue("OtherDeductionID", "@OtherDeductionID"), new FieldValue("NetTotal", "@NetTotal"), new FieldValue("DeductionAmount", "@DeductionAmount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_EOSSettlement", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLoanCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLoanText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLoanText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@LastWorkingDate", SqlDbType.DateTime);
			parameters.Add("@IsResigned", SqlDbType.Bit);
			parameters.Add("@EOSBenefit", SqlDbType.Money);
			parameters.Add("@LeaveDue", SqlDbType.Int);
			parameters.Add("@DueAmount", SqlDbType.Money);
			parameters.Add("@SalaryDue", SqlDbType.Money);
			parameters.Add("@OtherBenefits", SqlDbType.Money);
			parameters.Add("@TotalPayable", SqlDbType.Money);
			parameters.Add("@Loan", SqlDbType.Money);
			parameters.Add("@TicketAmount", SqlDbType.Money);
			parameters.Add("@OtherDeductionID", SqlDbType.NVarChar);
			parameters.Add("@NetTotal", SqlDbType.Money);
			parameters.Add("@DeductionAmount", SqlDbType.Money);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@LastWorkingDate"].SourceColumn = "LastWorkingDate";
			parameters["@IsResigned"].SourceColumn = "IsResigned";
			parameters["@EOSBenefit"].SourceColumn = "EOSBenefit";
			parameters["@LeaveDue"].SourceColumn = "LeaveDue";
			parameters["@DueAmount"].SourceColumn = "DueAmount";
			parameters["@SalaryDue"].SourceColumn = "SalaryDue";
			parameters["@OtherBenefits"].SourceColumn = "OtherBenefits";
			parameters["@TotalPayable"].SourceColumn = "TotalPayable";
			parameters["@Loan"].SourceColumn = "Loan";
			parameters["@TicketAmount"].SourceColumn = "TicketAmount";
			parameters["@OtherDeductionID"].SourceColumn = "OtherDeductionID";
			parameters["@NetTotal"].SourceColumn = "NetTotal";
			parameters["@DeductionAmount"].SourceColumn = "DeductionAmount";
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

		private string GetInsertUpdateEmployeeEOSText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_EOS", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("IsResigned", "@IsResigned"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("LastWorkingDate", "@LastWorkingDate"), new FieldValue("EmployeeBasic", "@EmployeeBasic"), new FieldValue("CalculatedGratuityAmount", "@CalculatedGratuityAmount"), new FieldValue("CalculatedLeaveAmount", "@CalculatedLeaveAmount"), new FieldValue("CalculatedSalaryAmount", "@CalculatedSalaryAmount"), new FieldValue("PaidGratuityAmount", "@PaidGratuityAmount"), new FieldValue("PaidLeaveAmount", "@PaidLeaveAmount"), new FieldValue("PaidSalaryAmount", "@PaidSalaryAmount"), new FieldValue("PaidLoanAmount", "@PaidLoanAmount"), new FieldValue("PaidDeductionAmount", "@PaidDeductionAmount"), new FieldValue("GratuityDescription", "@GratuityDescription"), new FieldValue("LeaveDescription", "@LeaveDescription"), new FieldValue("SalaryDescription", "@SalaryDescription"), new FieldValue("PaidTicketAmount", "@PaidTicketAmount"), new FieldValue("Note", "@Note"), new FieldValue("NetTotal", "@NetTotal"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_EOS", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateEmployeeEOSDeductionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_EOS_Deduction_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("DeductionID", "@DeductionID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_EOS_Deduction_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeEOSCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeEOSText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeEOSText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@IsResigned", SqlDbType.Bit);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@LastWorkingDate", SqlDbType.DateTime);
			parameters.Add("@EmployeeBasic", SqlDbType.Money);
			parameters.Add("@CalculatedGratuityAmount", SqlDbType.Money);
			parameters.Add("@CalculatedLeaveAmount", SqlDbType.Money);
			parameters.Add("@CalculatedSalaryAmount", SqlDbType.Money);
			parameters.Add("@PaidGratuityAmount", SqlDbType.Money);
			parameters.Add("@PaidLeaveAmount", SqlDbType.Money);
			parameters.Add("@PaidSalaryAmount", SqlDbType.Money);
			parameters.Add("@PaidLoanAmount", SqlDbType.Money);
			parameters.Add("@PaidDeductionAmount", SqlDbType.Money);
			parameters.Add("@GratuityDescription", SqlDbType.NVarChar);
			parameters.Add("@LeaveDescription", SqlDbType.NVarChar);
			parameters.Add("@SalaryDescription", SqlDbType.NVarChar);
			parameters.Add("@PaidTicketAmount", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@NetTotal", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@IsResigned"].SourceColumn = "IsResigned";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@LastWorkingDate"].SourceColumn = "LastWorkingDate";
			parameters["@EmployeeBasic"].SourceColumn = "EmployeeBasic";
			parameters["@CalculatedGratuityAmount"].SourceColumn = "CalculatedGratuityAmount";
			parameters["@CalculatedLeaveAmount"].SourceColumn = "CalculatedLeaveAmount";
			parameters["@CalculatedSalaryAmount"].SourceColumn = "CalculatedSalaryAmount";
			parameters["@PaidGratuityAmount"].SourceColumn = "PaidGratuityAmount";
			parameters["@PaidLeaveAmount"].SourceColumn = "PaidLeaveAmount";
			parameters["@PaidSalaryAmount"].SourceColumn = "PaidSalaryAmount";
			parameters["@PaidLoanAmount"].SourceColumn = "PaidLoanAmount";
			parameters["@PaidDeductionAmount"].SourceColumn = "PaidDeductionAmount";
			parameters["@GratuityDescription"].SourceColumn = "GratuityDescription";
			parameters["@LeaveDescription"].SourceColumn = "LeaveDescription";
			parameters["@SalaryDescription"].SourceColumn = "SalaryDescription";
			parameters["@PaidTicketAmount"].SourceColumn = "PaidTicketAmount";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@NetTotal"].SourceColumn = "NetTotal";
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

		private SqlCommand GetInsertUpdateEmployeeEOSDeductionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeEOSDeductionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeEOSDeductionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DeductionID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DeductionID"].SourceColumn = "DeductionID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
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

		public bool InsertUpdateEmployeeLoan(EmployeeEOSSettlementData employeeEOSData, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			try
			{
				DataRow dataRow = employeeEOSData.EmployeeEOSSettlementTable.Rows[0];
				SqlCommand insertUpdateEmployeeLoanCommand = GetInsertUpdateEmployeeLoanCommand(isUpdate);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["EmployeeID"].ToString();
				insertUpdateEmployeeLoanCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(employeeEOSData, "Employee_EOSSettlement", insertUpdateEmployeeLoanCommand)) : (flag & Insert(employeeEOSData, "Employee_EOSSettlement", insertUpdateEmployeeLoanCommand)));
				if (flag)
				{
					text = "UPDATE Employee SET\r\n                            IsEOSSettled= 'True' WHERE EmployeeID = '" + text2 + "'";
					flag &= Update(text, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Employee_EOSSettlement", "EmployeeID", text2, sqlTransaction, !isUpdate);
				string entityName = "Employee EOS Settlement";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text2.ToString(), ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text2.ToString(), ActivityTypes.Add, sqlTransaction);
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

		public bool InsertUpdateEmployeeEOS(EmployeeEOSSettlementData employeeEOSData, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			try
			{
				DataRow dataRow = employeeEOSData.EmployeeEOSTable.Rows[0];
				SqlCommand insertUpdateEmployeeEOSCommand = GetInsertUpdateEmployeeEOSCommand(isUpdate);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string str = dataRow["EmployeeID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				insertUpdateEmployeeEOSCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(employeeEOSData, "Employee_EOS", insertUpdateEmployeeEOSCommand)) : (flag & Insert(employeeEOSData, "Employee_EOS", insertUpdateEmployeeEOSCommand)));
				if (isUpdate)
				{
					flag &= DeleteEOSDdeductionDetailsRows(sqlTransaction, sysDocID, voucherID);
				}
				if (employeeEOSData.Tables["Employee_EOS_Deduction_Detail"].Rows.Count > 0)
				{
					insertUpdateEmployeeEOSCommand = GetInsertUpdateEmployeeEOSDeductionCommand(isUpdate: false);
					insertUpdateEmployeeEOSCommand.Transaction = sqlTransaction;
					flag &= Insert(employeeEOSData, "Employee_EOS_Deduction_Detail", insertUpdateEmployeeEOSCommand);
				}
				if (flag)
				{
					text = "UPDATE Employee SET\r\n                            IsEOSSettled= 'True' WHERE EmployeeID = '" + str + "'";
					flag &= Update(text, sqlTransaction);
				}
				GLData journalData = CreateEOSGLData(employeeEOSData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Employee_EOS", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Employee EOS";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, dataRow["VoucherID"].ToString(), dataRow["SysDocID"].ToString(), ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, dataRow["VoucherID"].ToString(), dataRow["SysDocID"].ToString(), ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Employee_EOS", "VoucherID", sqlTransaction);
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

		private GLData CreateEOSGLData(EmployeeEOSSettlementData employeeEOSData, SqlTransaction sqlTransaction)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				GLData gLData = new GLData();
				DataRow dataRow = employeeEOSData.EmployeeEOSTable.Rows[0];
				string text = "";
				string text2 = dataRow["SysDocID"].ToString();
				string value = dataRow["VoucherID"].ToString();
				string text3 = dataRow["EmployeeID"].ToString();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.EmployeeEOS;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = text2;
				dataRow2["SysDocType"] = (int)sysDocTypes;
				dataRow2["VoucherID"] = value;
				dataRow2["Note"] = dataRow["Note"].ToString();
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				decimal num = decimal.Parse(dataRow["PaidLeaveAmount"].ToString());
				decimal num2 = decimal.Parse(dataRow["PaidGratuityAmount"].ToString());
				decimal num3 = decimal.Parse(dataRow["PaidSalaryAmount"].ToString());
				decimal num4 = decimal.Parse(dataRow["PaidTicketAmount"].ToString());
				decimal d = decimal.Parse(dataRow["PaidLoanAmount"].ToString());
				decimal d2 = decimal.Parse(dataRow["PaidDeductionAmount"].ToString());
				decimal d3 = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero) + Math.Round(num2, currencyDecimalPoints, MidpointRounding.AwayFromZero) + Math.Round(num3, currencyDecimalPoints, MidpointRounding.AwayFromZero) + num4 - d - d2;
				string text4 = employeeEOSData.EmployeeEOSTable.Rows[0]["SysDocID"].ToString();
				string textCommand = "SELECT ISNULL(Emp.AccountID, ISNULL(CLS.AccountID,LOC.EmployeeAccountID )) AS EmployeeAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Employee EMP ON EmployeeID = '" + text3 + "'\r\n                                LEFT OUTER JOIN Employee_Type CLS ON EMP.ContractType = CLS.TypeID\r\n                                WHERE SysDocID = '" + text4 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				text = dataSet.Tables["Accounts"].Rows[0]["EmployeeAccountID"].ToString();
				DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text;
				dataRow3["PayeeID"] = text3;
				dataRow3["PayeeType"] = "E";
				dataRow3["IsARAP"] = true;
				dataRow3["Credit"] = Math.Round(d3, currencyDecimalPoints, MidpointRounding.AwayFromZero);
				dataRow3["Debit"] = DBNull.Value;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				string text5 = "";
				string text6 = "";
				string text7 = "";
				textCommand = "SELECT  *  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID WHERE SysDocID = '" + text2 + "'";
				dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				text5 = dataSet.Tables[0].Rows[0]["LeaveExpenseAccountID"].ToString();
				text6 = dataSet.Tables[0].Rows[0]["EOSBenefitAccountID"].ToString();
				text7 = dataSet.Tables[0].Rows[0]["ticketAccountID"].ToString();
				if (text5 == "" || text6 == "" || text7 == "")
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				if (num > 0m)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text5;
					dataRow3["PayeeID"] = text3;
					dataRow3["PayeeType"] = "A";
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				if (num2 > 0m)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text6;
					dataRow3["PayeeID"] = text3;
					dataRow3["PayeeType"] = "A";
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Debit"] = Math.Round(num2, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				if (num4 > 0m)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text7;
					dataRow3["PayeeID"] = text3;
					dataRow3["PayeeType"] = "A";
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Debit"] = Math.Round(num4, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				if (num3 > 0m)
				{
					textCommand = "SELECT EmployeeID,ISNULL(EMP.AccountID,ET.AccountID) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                WHERE EMP.EmployeeID IN ('" + text3 + "') ";
					DataSet dataSet2 = new DataSet();
					FillDataSet(dataSet2, "Employee", textCommand, sqlTransaction);
					textCommand = "SELECT  LOC.EmployeeAccountID AS AccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID WHERE SysDocID = '" + text2 + "'";
					DataSet dataSet3 = new DataSet();
					FillDataSet(dataSet3, "Accounts", textCommand, sqlTransaction);
					DataRow[] array = dataSet2.Tables[0].Select("EmployeeID = '" + text3 + "'");
					if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
					{
						text = array[0]["AccountID"].ToString();
					}
					if (text == "")
					{
						text = dataSet3.Tables[0].Rows[0]["AccountID"].ToString();
					}
					if (dataSet3 == null || dataSet3.Tables.Count == 0 || dataSet3.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("There is no location assigned to this system document or location record is missing.");
					}
					if (text == "")
					{
						throw new CompanyException("Account is not set for the employee '" + text3 + "'.", 1021);
					}
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text;
					dataRow3["PayeeID"] = text3;
					dataRow3["PayeeType"] = "A";
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Debit"] = Math.Round(num3, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				foreach (DataRow row in employeeEOSData.EmployeeEOSDeductionDetailTable.Rows)
				{
					string text8 = row["DeductionID"].ToString();
					decimal num5 = decimal.Parse(row["Amount"].ToString());
					string text9 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("PayrollItem", "AccountID", "PayrollItemID", text8, sqlTransaction);
					if (fieldValue == null || !(fieldValue.ToString() != ""))
					{
						throw new CompanyException("Account is not set for the deduction '" + text8 + "'.", 1021);
					}
					text9 = fieldValue.ToString();
					if (num5 > 0m)
					{
						dataRow3 = gLData.JournalDetailsTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["JournalID"] = 0;
						dataRow3["AccountID"] = text9;
						dataRow3["PayeeID"] = text3;
						dataRow3["PayeeType"] = "A";
						dataRow3["Credit"] = Math.Round(num5, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						dataRow3["Debit"] = DBNull.Value;
						dataRow3.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow3);
					}
				}
				if (employeeEOSData.Tables.Count > 4)
				{
					List<string> list = new List<string>();
					List<decimal> list2 = new List<decimal>();
					foreach (DataRow row2 in employeeEOSData.Tables[4].Rows)
					{
						string str = row2["Loan Type"].ToString();
						decimal num6 = decimal.Parse(row2["Balance"].ToString());
						string text10 = row2["AccountID"].ToString();
						if (text10 == "")
						{
							throw new CompanyException("Account is not set for the loan type '" + str + "'.", 1021);
						}
						if (list.Contains(text10))
						{
							int index = list.IndexOf(text10);
							num6 = (list2[index] = num6 + decimal.Parse(list2[index].ToString()));
						}
						else
						{
							list.Add(text10);
							list2.Add(num6);
						}
					}
					for (int i = 0; i < list.Count; i++)
					{
						decimal num8 = list2[i];
						string value2 = list[i].ToString();
						if (num8 > 0m)
						{
							dataRow3 = gLData.JournalDetailsTable.NewRow();
							dataRow3.BeginEdit();
							dataRow3["JournalID"] = 0;
							dataRow3["AccountID"] = value2;
							dataRow3["PayeeID"] = text3;
							dataRow3["PayeeType"] = "A";
							dataRow3["Credit"] = Math.Round(num8, currencyDecimalPoints, MidpointRounding.AwayFromZero);
							dataRow3["Debit"] = DBNull.Value;
							dataRow3.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow3);
						}
					}
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateLoanPaidAmount(string loanSysDocID, string loanVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE Employee_Loan SET PaidAmount = (SELECT SUM(ISNULL(Credit,0)) AS AMT FROM Employee_Loan_Detail \r\n                                WHERE LoanSysDocID = '" + loanSysDocID + "' AnD LoanVoucherID = '" + loanVoucherID + "') \r\n\t\t\t\t\t\t        WHERE SysDocID = '" + loanSysDocID + "' AND VoucherID = '" + loanVoucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateLoanPaidAmountWhileDelete(string loanSysDocID, string loanVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "update Employee_Loan  set PaidAmount  = (select sum(isnull(ed.Credit,0)) from   Employee_Loan_Detail ED\r\n                                WHERE  ED.LoanSysDocID = '" + loanSysDocID + "' AND ED.LoanVoucherID= '" + loanVoucherID + "') \r\n\t\t\t\t\t\t        WHERE SysDocID = '" + loanSysDocID + "' AND VoucherID = '" + loanVoucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteLoanDetailsRows(SqlTransaction sqlTransaction, string sysDocID, string voucherID, bool isPaymentRow)
		{
			bool flag = true;
			try
			{
				if (flag)
				{
					return flag & DeleteLoanPaidAmountWhileDelete(sysDocID, voucherID, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		internal bool DeleteEOSDdeductionDetailsRows(SqlTransaction sqlTransaction, string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				if (flag)
				{
					return flag & DeleteEOSDeduction(sysDocID, voucherID, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		internal bool DeleteEOSDeduction(string SysDocID, string VoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string commandText = "DELETE From Employee_EOS_Deduction_Detail Where SysDocID='" + SysDocID + "' AND VoucherID='" + VoucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteLoanPaidAmountWhileDelete(string SysDocID, string VoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE Employee_Loan  SET PaidAmount=(SELECT SUM(ISNULL(Credit,0)) FROM Employee_Loan_Detail ELD \r\n                                WHERE ELD.LoanSysDocID=Employee_Loan.SysDocID AND ELD.LoanVoucherID=Employee_Loan.VoucherID)";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeEOSSettlementData GetEmployeeLoanByID(string sysDocID, string voucherID)
		{
			return GetEmployeeLoanByID(sysDocID, voucherID, null);
		}

		public EmployeeEOSSettlementData GetEmployeeLoanByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				return new EmployeeEOSSettlementData();
			}
			catch
			{
				throw;
			}
		}

		public EmployeeEOSSettlementData GetEmployeeLoanPaymentByID(string sysDocID, string voucherID)
		{
			try
			{
				return new EmployeeEOSSettlementData();
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetFirstEmployeeLoanByID(string voucherID, string employeeID, SqlTransaction sqlTransaction)
		{
			try
			{
				return new DataSet();
			}
			catch
			{
				throw;
			}
		}

		public bool CanEditLoan(string sysDocID, string voucherID)
		{
			try
			{
				bool flag = true;
				string exp = "SELECT CASE WHEN Amount=0 THEN 'True' \r\n                                WHEN (Amount<>0) AND ISNULL(PaidAmount,0)+ ISNULL(DiscountAmount,0)=0 THEN 'True' ELSE 'False' END\r\n                                FROM Employee_Loan WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "")
				{
					flag &= bool.Parse(obj.ToString());
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteEOS(string EmployeeID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Employee SET IsEOSSettled = 0 \r\n                                WHERE EmployeeID = '" + EmployeeID + "' ";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				string commandText = "DELETE FROM Employee_EOSSettlement WHERE EmployeeID = '" + EmployeeID + "'";
				flag &= Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee EOS ", EmployeeID, activityType, sqlTransaction);
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

		public bool DeleteLoanPayment(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Employee_EOSSettlement WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= DeleteLoanDetailsRows(sqlTransaction, sysDocID, voucherID, isPaymentRow: true);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT LoanSysDocID,LoanVoucherID FROM Employee_Loan_Payment WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Loan", textCommand, sqlTransaction);
				string loanSysDocID = dataSet.Tables["Loan"].Rows[0]["LoanSysDocID"].ToString();
				string loanVoucherID = dataSet.Tables["Loan"].Rows[0]["LoanVoucherID"].ToString();
				flag &= UpdateLoanPaidAmount(loanSysDocID, loanVoucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Loan Payment", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetEmployeeLoanComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VoucherID [Code],Reason [Name], SysDocID,EmployeeID\r\n                                FROM Employee_Loan EL  \r\n                                WHERE Amount- ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) >0 AND ISNULL(IsVoid,'False')='False'";
			FillDataSet(dataSet, "Employee_EOSSettlement", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLoanComboAllList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VoucherID [Code],Reason [Name], SysDocID,EmployeeID\r\n                                FROM Employee_Loan EL  \r\n                                WHERE ISNULL(IsVoid,'False')='False'";
			FillDataSet(dataSet, "Employee_EOSSettlement", textCommand);
			return dataSet;
		}

		public decimal GetNextLoanInstallmentAmount(string voucherID, string employeeID)
		{
			DataSet firstEmployeeLoanByID = GetFirstEmployeeLoanByID(voucherID, employeeID, null);
			if (firstEmployeeLoanByID == null || firstEmployeeLoanByID.Tables.Count == 0 || firstEmployeeLoanByID.Tables[0].Rows.Count == 0)
			{
				return 0m;
			}
			string text = "";
			string exp = "SELECT CASE WHEN ISNULL(IsVoid,'False')='True' THEN 0\r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) >= Amount THEN 0 \r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) + InstallmentAmount > Amount THEN Amount-ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) \r\n                        ELSE ISNULL(InstallmentAmount,Amount) END AS [InstallmentAmount]\r\n                        FROM Employee_Loan WHERE \r\n                        DedStartDate <= GetDate() AND \r\n                        SysDocID='" + text + "' AND VoucherID='" + voucherID + "' AND ISNULL(IsVoid,'False')='False'";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return 0m;
			}
			return decimal.Parse(obj.ToString());
		}

		public DataSet GetListEmployeeLoan(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    SysDocID [Doc ID],VoucherID [Doc Number]\t\t\t\t\t\t\r\n\t\t\t\t\t\tFROM Employee_Loan";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_EOSSettlement", sqlCommand);
			return dataSet;
		}

		public DataSet GetListEmployeeLoanPayment(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   SysDocID [Doc ID],VoucherID [Doc Number]\t\t\t\t\t\t\r\n\t\t\t\t\t\tFROM Employee_Loan_Payment";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_EOSSettlement", sqlCommand);
			return dataSet;
		}

		public DataSet GetListEmployeeLoanSettlement(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   SysDocID [Doc ID],VoucherID [Doc Number]\t\t\t\t\t\t\r\n\t\t\t\t\t\tFROM Employee_Loan_Settlement";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_EOSSettlement", sqlCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLoanReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID)
		{
			string str = "";
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT FirstName+' '+LastName AS [Name] ,EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance\r\n                            FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType\r\n                            WHERE El.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromEmployee != "")
			{
				text3 = text3 + " AND EL.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			if (SysDocID != "")
			{
				text3 = text3 + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text3 = text3 + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			text3 += " ORDER BY EL.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_EOSSettlement", text3);
			text3 = "SELECT ELD.*,SD.DocName FROM Employee_Loan_Detail ELD INNER JOIN Employee_Loan EL ON ELD.LoanVoucherID=EL.VoucherID\r\n                    LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                    LEFT JOIN System_Document SD ON SD.SysDocID=ELD.PaymentSysDocID\r\n                    WHERE ELD.PaymentSysDocID IS NOT NULL AND \r\n                    EL.TransactionDate BETWEEN'" + text + "' AND '" + text2 + "'";
			if (fromEmployee != "")
			{
				text3 = text3 + " AND EL.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			if (SysDocID != "")
			{
				text3 = text3 + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text3 = text3 + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			text3 += " ORDER BY EL.EmployeeID";
			FillDataSet(dataSet, "Employee_EOSSettlement", text3);
			dataSet.Relations.Add("EMPLOAN_Rel", new DataColumn[2]
			{
				dataSet.Tables["Employee_EOSSettlement"].Columns["SysDocID"],
				dataSet.Tables["Employee_EOSSettlement"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Employee_EOSSettlement"].Columns["LoanSysDocID"],
				dataSet.Tables["Employee_EOSSettlement"].Columns["LoanVoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetEmployeeLoanReportSummary(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID)
		{
			string str = "";
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT FirstName+' '+LastName AS [Name] ,EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance\r\n                            FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType                            \r\n                            WHERE EL.Amount > 0 AND EL.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromEmployee != "")
			{
				text3 = text3 + " AND EL.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			if (SysDocID != "")
			{
				text3 = text3 + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text3 = text3 + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			text3 += " ORDER BY EL.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_EOSSettlement", text3);
			return dataSet;
		}

		public DataSet GetEmployeeLoanList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number],EmployeeID,TransactionDate,Amount  FROM Employee_Loan WHERE SysDocID='" + sysDocID + "'";
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Employee_EOSSettlement", str);
			return dataSet;
		}

		public DataSet GetEOSSettlementToPrint(string EmployeeID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT FirstName+' '+LastName AS [Name],E.Joiningdate,LabourID,PositionName,SponsorName,\r\n                            (SELECT DocumentNumber FROM Employee_Document ED WHERE ED.EmployeeID=E.EmployeeID AND ED.DocumentTypeID='PASSPORT') AS Passport,\r\n                            D.DepartmentName,EOS.*\r\n                            FROM Employee_EOSSettlement EOS LEFT JOIN Employee E ON EOS.EmployeeID=E.EmployeeID \r\n                            LEFT JOIN Position P ON P.PositionID=E.PositionID  \r\n                            LEFT JOIN Department D ON D.DepartmentID=E.DepartmentID \r\n                            LEFT JOIN Sponsor S ON S.SponsorID=E.SponsorID ";
			if (EmployeeID != "")
			{
				text = text + " WHERE E.EmployeeID='" + EmployeeID + "'";
			}
			new SqlCommand(text);
			FillDataSet(dataSet, "Employee_EOSSettlement", text);
			return dataSet;
		}

		public EmployeeEOSSettlementData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID)
		{
			return GetEmployeeLoanSettlementByID(sysDocID, voucherID, null);
		}

		public EmployeeEOSSettlementData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				return new EmployeeEOSSettlementData();
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteLoanSettlement(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT LoanSysDocID,LoanVoucherID FROM Employee_Loan_Settlement WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Loan", textCommand, sqlTransaction);
				string commandText = "DELETE FROM Employee_EOSSettlement WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= DeleteLoanDetailsRows(sqlTransaction, sysDocID, voucherID, isPaymentRow: true);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				string loanSysDocID = dataSet.Tables["Loan"].Rows[0]["LoanSysDocID"].ToString();
				string loanVoucherID = dataSet.Tables["Loan"].Rows[0]["LoanVoucherID"].ToString();
				flag &= UpdateLoanPaidAmount(loanSysDocID, loanVoucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Loan Settlement", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteEOS(string sysDocID, string voucherID, string employeeID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Employee_EOS WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= DeleteEOSDdeductionDetailsRows(sqlTransaction, sysDocID, voucherID);
				if (flag)
				{
					string commandText2 = "UPDATE Employee SET\r\n                            IsEOSSettled= 'False' WHERE EmployeeID = '" + employeeID + "'";
					flag &= Update(commandText2, sqlTransaction);
				}
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee EOS", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool VoidLoanSettlement(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				if (!CanEditLoan(sysDocID, voucherID))
				{
					throw new CompanyException("This loan transaction is already paid and cannot be deleted.");
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object obj = "";
				if (obj != null && obj.ToString() != "")
				{
					flag2 = bool.Parse(obj.ToString());
				}
				if (flag2 == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				string exp = "UPDATE Employee_Loan_Settlement SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Employee Loan Settlement", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetEmployeeEOSToPrint(string SysDocID, string VoucehrID)
		{
			string text = "SELECT SysDocID,VoucherID,[TransactionDate],FirstName+' '+LastName AS [Employee],E.ConfirmationDate,E.Gender,LabourID,E.JoiningDate,PositionName [Position],\r\n                            LocationName [Location],DivisionName [Division],E.DepartmentID ,DepartmentName [Department],E.GradeID,GradeName [Grade],LastWorkingDate,EmployeeBasic,EOS.Note,NetTotal [Total] From Employee_EOS EOS \r\n                            LEFT JOIN Employee E ON EOS.EmployeeID=E.EmployeeID\r\n                             LEFT OUTER JOIN Location ON Location.LocationID=E.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=E.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=E.DepartmentID\r\n                                LEFT OUTER JOIN Position ON Position.PositionID=E.PositionID\r\n                                LEFT OUTER JOIN Employee_Grade EG ON EG.GradeID=E.GradeID\r\n                            WHERE 1=1 ";
			if (SysDocID != "")
			{
				text = text + " AND EOS.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND EOS.VoucherID='" + VoucehrID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_EOS", text);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Employee_EOS"].Rows.Count == 0)
			{
				return null;
			}
			DataSet dataSet2 = new DataSet();
			text = "SELECT * from Employee_EOS Where SysDocID='" + SysDocID + "' AND VoucherID='" + VoucehrID + "'";
			FillDataSet(dataSet2, "EOS", text);
			DataRow dataRow = dataSet2.Tables["EOS"].Rows[0];
			DataTable dataTable = CreateData();
			dataTable.Rows.Clear();
			dataTable.Rows.Add(SysDocID, VoucehrID, "Balance Leave Amount", dataRow["CalculatedLeaveAmount"], dataRow["LeaveDescription"], dataRow["PaidLeaveAmount"]);
			dataTable.Rows.Add(SysDocID, VoucehrID, "Gratuity Amount", dataRow["CalculatedGratuityAmount"], dataRow["GratuityDescription"], dataRow["PaidGratuityAmount"]);
			dataTable.Rows.Add(SysDocID, VoucehrID, "Salary Amount", dataRow["CalculatedSalaryAmount"], dataRow["SalaryDescription"], dataRow["PaidSalaryAmount"]);
			dataTable.Rows.Add(SysDocID, VoucehrID, "Ticket Amount", 0, "", dataRow["PaidTicketAmount"]);
			dataTable.Rows.Add(SysDocID, VoucehrID, "Loan Amount Deduction", 0, "", dataRow["PaidLoanAmount"]);
			dataTable.Rows.Add(SysDocID, VoucehrID, "Other Deduction", 0, "", dataRow["PaidDeductionAmount"]);
			dataSet.Tables.Add(dataTable);
			text = "SELECT SysDocID,VoucherID,DeductionID,Description,Amount\r\n                                FROM Employee_EOS_Deduction_Detail WHERE SysDocID = '" + SysDocID + "' AND VoucherID='" + VoucehrID + "'";
			FillDataSet(dataSet, "Employee_EOS_Deduction_Detail", text);
			dataSet.Relations.Add("EmployeeEOS_Rel", new DataColumn[2]
			{
				dataSet.Tables["Employee_EOS"].Columns["SysDocID"],
				dataSet.Tables["Employee_EOS"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["EOS_Detail"].Columns["SysDocID"],
				dataSet.Tables["EOS_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			dataSet.Relations.Add("EmployeeEOSDeduction_Rel", new DataColumn[2]
			{
				dataSet.Tables["Employee_EOS"].Columns["SysDocID"],
				dataSet.Tables["Employee_EOS"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Employee_EOS_Deduction_Detail"].Columns["SysDocID"],
				dataSet.Tables["Employee_EOS_Deduction_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		private DataTable CreateData()
		{
			return new DataTable("EOS_Detail")
			{
				Columns = 
				{
					{
						"SysDocID",
						typeof(string)
					},
					{
						"VoucherID",
						typeof(string)
					},
					{
						"Description",
						typeof(string)
					},
					{
						"CalculatedValue",
						typeof(decimal)
					},
					{
						"BasedOn",
						typeof(string)
					},
					{
						"PaidValue",
						typeof(decimal)
					}
				}
			};
		}

		public DataSet GetEmployeeLoanSettlementToPrint(string SysDocID, string VoucehrID)
		{
			string text = "SELECT FirstName+' '+LastName AS [Name] ,EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance,ELS.SettlementAmount\r\n                            FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType\r\n                            INNER JOIN Employee_Loan_Settlement ELS ON ELS.LoanSysDocID=EL.SysDocID AND ELS.LoanVoucherID=EL.VoucherID\r\n                            WHERE 1=1 ";
			if (SysDocID != "")
			{
				text = text + " AND ELS.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND ELS.VoucherID='" + VoucehrID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_EOSSettlement", text);
			text = "SELECT ELD.*,SD.DocName FROM Employee_Loan_Detail ELD INNER JOIN Employee_Loan EL ON ELD.LoanVoucherID=EL.VoucherID\r\n                    LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                    LEFT JOIN System_Document SD ON SD.SysDocID=ELD.PaymentSysDocID\r\n                    WHERE ELD.PaymentSysDocID IS NOT NULL";
			if (SysDocID != "")
			{
				text = text + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			FillDataSet(dataSet, "Employee_EOSSettlement", text);
			dataSet.Relations.Add("EmplLoanSettle_Rel", new DataColumn[2]
			{
				dataSet.Tables["Employee_EOSSettlement"].Columns["SysDocID"],
				dataSet.Tables["Employee_EOSSettlement"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Employee_EOSSettlement"].Columns["LoanSysDocID"],
				dataSet.Tables["Employee_EOSSettlement"].Columns["LoanVoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetEmployeeFinalSettlement(string fromEmployee, DateTime asOfDate, bool Isregular)
		{
			CommonLib.ToSqlDateTimeString(asOfDate);
			string text = "SELECT Employee.EmployeeID ,FirstName,MiddleName,LastName, NickName, BirthDate,Employee.LocationID,PositionName,Photo,Balance,\r\n                            CASE Status WHEN 1 THEN 'A' ELSE 'Terminated' END AS [Status] , CAST((DATEDIFF(m, BirthDate, GETDATE())/12) as varchar) AS Age, JoiningDate,\r\n                            CAST((DATEDIFF(m, JoiningDate, GETDATE())/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, JoiningDate, GETDATE())%12) as varchar) + ' Month' AS ServicePeriodMonth,\r\n                            ISNULL(OnVacation,'False') AS OnVacation,SponsorName,NationalityID,ReligionName,\r\n                            CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())/12) as varchar) + ' Year & ' + CAST((DATEDIFF(m, ISNULL(ISNULL(ResumedDate,AnnualLeaveDate),JoiningDate), GETDATE())%12) as varchar) + ' Month' AS CurrentServicePeriod,\r\n                            CASE MaritalStatus WHEN 1 THEN 'NA' WHEN 2 THEN 'Single' WHEN 3 THEN 'Married' WHEN 4 THEN 'Divorced' WHEN 5 THEN 'Widow' END AS MaritalStatus,\r\n                            Phone1,Mobile,Email,Notes,DepartmentName,GroupName,Gender\r\n                            FROM Employee LEFT OUTER JOIN  Position EP ON Employee.PositionID=EP.PositionID\r\n                            LEFT OUTER JOIN Employee_Grade Grade ON Grade.GradeID=Employee.GradeID\r\n                            LEFT OUTER JOIN Sponsor ON Sponsor.SponsorID=Employee.SponsorID\r\n                            LEFT OUTER JOIN Religion ON Religion.ReligionID=Employee.ReligionID\r\n                            LEFT OUTER JOIN Division ON Division.DivisionID=Employee.DivisionID\r\n                            LEFT OUTER JOIN Department ON Department.DepartmentID=Employee.DepartmentID\r\n                            LEFT OUTER JOIN Employee_Group EG ON EG.GroupID=Employee.GroupID\r\n                            LEFT OUTER JOIN Location  ON Location.LocationID=Employee.LocationID\r\n                            LEFT OUTER JOIN Employee_Address EA ON EA.EmployeeID=Employee.EmployeeID AND AddressID='PRIMARY' WHERE 1=1 ";
			if (fromEmployee != "")
			{
				text = text + " AND Employee.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + fromEmployee + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", text);
			DataSet dataSet2 = new DataSet();
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			int result4 = 0;
			int result5 = 0;
			int result6 = 0;
			int result7 = 0;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee", "ContractType", "EmployeeID", fromEmployee, null);
			object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee_Type", "LeaveSelection", "TypeID", fieldValue, null);
			string text2 = "";
			string text3 = CommonLib.ToSqlDateTimeString(new DateTime(asOfDate.Year, asOfDate.Month, asOfDate.Day, 11, 59, 59));
			if (fieldValue2 != null)
			{
				if (fieldValue2.ToString().Trim() == "OA")
				{
					text2 = "select e.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,lt.IsAnnual,obld.LeaveTaken[openingLeavesTaken]\r\n                            ,DATEDIFF(MONTH,e.JoiningDate,GETDATE()) as Sevicemonths,                        \r\n                                CASE WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND MonthGreater1 > DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "') THEN 'True' ELSE 'False' END AS 'AnnualEligible',                                             \r\n                            CASE\r\n                            -- WHEN lt.IsAnnual='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  AND MonthLesser1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE()))  THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser1+MonthGreater1))* AllowedDays1\r\n                             WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text3 + "'))* AllowedDays1\r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text3 + "'))* AllowedDays1\r\n                             End AS '1SET',\r\n                            CASE \r\n                            --WHEN lt.IsAnnual='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  OR MonthLesser2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())) THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser2+MonthGreater2)) * AllowedDays2 \r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text3 + "'))* AllowedDays2\r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text3 + "'))* AllowedDays2\r\n\r\n                            End AS '2SET',\r\n                            CASE\r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text3 + "'))* AllowedDays3\r\n                         -- WHEN lt.IsAnnual='1' AND MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())   THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-MonthGreater3) * AllowedDays3 \r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text3 + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text3 + "'))* AllowedDays3\r\n                            End AS '3SET',\r\n                            LT.Days AS LeaveDayswithType,(\r\n                            SELECT  SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n                            FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n                            LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID \r\n                            LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID\r\n\t\t\t\t\t\t\tLEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID \r\n                            INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                            WHERE  ISNULL(isvoid,0) =0  AND ELR1.StartDate> OPBL1.BatchDate AND IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID\r\n                            GROUP BY LT1.LeaveTypeName, E1.EmployeeID) AS TotalTaken,0 AS TotalLeaves,0 AS LeavesRemaining,'' AS Basedon,\r\n                            ISNULL((SELECT SUM(LeaveEncash) FROM Employee_Leave_Encashment ELE INNER JOIN Employee_Activity EA ON ELE.ActivityID = EA.ActivityID WHERE EmployeeID=E.EmployeeID), 0) [Leaves Encash],\r\n                           (SELECT CASE WHEN (select DaysInMonth from company)  ='1' THEN  SUM(Amount)/'" + DateTime.DaysInMonth(asOfDate.Year, asOfDate.Month) + "'\r\n                            WHEN (select ThirtyDays from company)  ='1' THEN  SUM(Amount)/30\r\n                            WHEN (select Annual from company)  ='1' THEN  SUM(Amount)/365*12\r\n                            ELSE  SUM(Amount)/30\r\n                            END  FROM Employee_PayrollItem_Detail WHERE EmployeeID=E.EmployeeID AND PayrollItemID IN (SELECT PayrollItemID FROM PayrollItem WHERE  InServiceBenefit=1)) AS DailySalary,\r\n                            (SELECT CASE WHEN (select DaysInMonth from company)  ='1' THEN  SUM(Amount)/'" + DateTime.DaysInMonth(asOfDate.Year, asOfDate.Month) + "'\r\n                            WHEN (select ThirtyDays from company)  ='1' THEN  SUM(Amount)/30\r\n                            WHEN (select Annual from company)  ='1' THEN  SUM(Amount)/365*12\r\n                            ELSE  SUM(Amount)/30\r\n                            END  FROM Employee_PayrollItem_Detail WHERE EmployeeID=E.EmployeeID AND PayrollItemID IN (SELECT PayrollItemID FROM PayrollItem WHERE InLeaveSalary=1 )) AS DailySalary2\r\n                            FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                            LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n                            LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID WHERE eld.LeaveTypeID<>'' AND e.EmployeeID='" + fromEmployee.ToString() + "'";
					FillDataSet(dataSet2, "LeaveAvailability", text2);
					for (int i = 0; i < dataSet2.Tables[0].Rows.Count; i++)
					{
						decimal.TryParse(dataSet2.Tables[0].Rows[i]["1SET"].ToString(), out result);
						decimal.TryParse(dataSet2.Tables[0].Rows[i]["2SET"].ToString(), out result2);
						decimal.TryParse(dataSet2.Tables[0].Rows[i]["3SET"].ToString(), out result3);
						int.TryParse(dataSet2.Tables[0].Rows[i]["openingLeavesTaken"].ToString(), out result4);
						int.TryParse(dataSet2.Tables[0].Rows[i]["TotalTaken"].ToString(), out result5);
						int.TryParse(dataSet2.Tables[0].Rows[i]["LeaveDayswithType"].ToString(), out result6);
						dataSet2.Tables[0].Rows[i]["TotalLeaves"] = result + result2 + result3 - (decimal)result4;
						dataSet2.Tables[0].Rows[i]["LeavesRemaining"] = result + result2 + result3 - (decimal)result4 - (decimal)result5;
					}
				}
				else if (fieldValue2.ToString().Trim() == "CD")
				{
					text2 = "select e.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,lt.IsAnnual,obld.LeaveTaken[openingLeaves]\r\n                            ,DATEDIFF(MONTH,e.JoiningDate,GETDATE()) as Sevicemonths, \r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.Days End AS 'AnnualAllowedDays',\r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.FromDate END AS 'FromDate',\r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.ToDate End AS 'ToDate',\r\n                             LT.Days AS LeaveDayswithType,(\r\n\t\t\t\t    \t\tSELECT  SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n\t\t\t\t\t        FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n\t\t\t\t\t        LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID  \r\n                            LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID\r\n\t\t\t\t\t\t\tLEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID\r\n                            INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                            WHERE  ISNULL(isvoid,0) =0 AND ELR1.StartDate> OPBL1.BatchDate  AND IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID\r\n\t\t\t\t\t        GROUP BY LT1.LeaveTypeName, E1.EmployeeID) AS TotalTaken,0 AS TotalLeaves,0 AS LeavesRemaining,'' AS Basedon,\r\n                            ISNULL((SELECT SUM(LeaveEncash) FROM Employee_Leave_Encashment ELE INNER JOIN Employee_Activity EA ON ELE.ActivityID = EA.ActivityID WHERE EmployeeID=E.EmployeeID), 0) [Leaves Encash],\r\n                           (SELECT CASE WHEN (select DaysInMonth from company)  ='1' THEN  SUM(Amount)/'" + DateTime.DaysInMonth(asOfDate.Year, asOfDate.Month) + "'\r\n                            WHEN (select ThirtyDays from company)  ='1' THEN  SUM(Amount)/30\r\n                            WHEN (select Annual from company)  ='1' THEN  SUM(Amount)/365*12\r\n                            ELSE  SUM(Amount)/30\r\n                            END  FROM Employee_PayrollItem_Detail WHERE EmployeeID=E.EmployeeID AND PayrollItemID IN (SELECT PayrollItemID FROM PayrollItem WHERE InServiceBenefit=1) ) AS DailySalary,\r\n                            (SELECT CASE WHEN (select DaysInMonth from company)  ='1' THEN  SUM(Amount)/'" + DateTime.DaysInMonth(asOfDate.Year, asOfDate.Month) + "'\r\n                            WHEN (select ThirtyDays from company)  ='1' THEN  SUM(Amount)/30\r\n                            WHEN (select Annual from company)  ='1' THEN  SUM(Amount)/365*12\r\n                            ELSE  SUM(Amount)/30\r\n                            END  FROM Employee_PayrollItem_Detail WHERE EmployeeID=E.EmployeeID AND PayrollItemID IN (SELECT PayrollItemID FROM PayrollItem WHERE InLeaveSalary=1 )) AS DailySalary2\r\n                            FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                            LEFT JOIN Employee_Leave_Process elp ON e.EmployeeID=elp.EmployeeID\r\n                            LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n                            LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID WHERE eld.LeaveTypeID<>'' AND e.EmployeeID='" + fromEmployee.ToString() + "'";
					FillDataSet(dataSet2, "LeaveAvailability", text2);
					for (int j = 0; j < dataSet2.Tables[0].Rows.Count; j++)
					{
						int.TryParse(dataSet2.Tables[0].Rows[j]["openingLeaves"].ToString(), out result4);
						int.TryParse(dataSet2.Tables[0].Rows[j]["TotalTaken"].ToString(), out result5);
						int.TryParse(dataSet2.Tables[0].Rows[j]["LeaveDayswithType"].ToString(), out result6);
						int.TryParse(dataSet2.Tables[0].Rows[j]["AnnualAllowedDays"].ToString(), out result7);
						dataSet2.Tables[0].Rows[j]["TotalLeaves"] = result + result2 + result3 - (decimal)result4 + (decimal)result7;
						dataSet2.Tables[0].Rows[j]["LeavesRemaining"] = result + result2 + result3 - (decimal)result4 + (decimal)result7 - (decimal)result5;
					}
				}
			}
			dataSet.Merge(dataSet2);
			text = "SELECT ESRD.* FROM Employee_EOSRule_Detail ESRD LEFT JOIN Employee_EOSRule ESR ON ESR.EOSRuleID=ESRD.EOSRuleID \r\n                        LEFT JOIN Employee_Type ET ON ET.EOSID=ESR.EOSRuleID LEFT JOIN  Employee E ON E.ContractType=ET.TypeID WHERE ISNULL(ESRD.ResignedType,1)=E.STATUS AND E.EmployeeID= '" + fromEmployee + "'";
			FillDataSet(dataSet, "EOSRule", text);
			DataTable dataTable = dataSet.Tables["EOSRule"];
			decimal num = 1m;
			if (!Isregular)
			{
				num = num * 12m / 365m;
			}
			else
			{
				num /= 30m;
			}
			if (dataTable.Rows.Count == 3)
			{
				DataRow dataRow = dataTable.Rows[0];
				DataRow dataRow2 = dataTable.Rows[1];
				DataRow dataRow3 = dataTable.Rows[2];
				text = "SELECT E.EmployeeID, E.FirstName, E.LastName, JoiningDate,E.TicketAmount, ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)  AS Tenure, \r\n                    ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 1, 2)  AS Tenure2,\r\n                    ROUND(SUM(CASE WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= 0 AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) < 1 THEN 0  \r\n                    WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= '" + dataRow["YearFrom"] + "' AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) <= '" + dataRow["YearTo"] + "' THEN\r\n                    ((Amount/'" + num + "') *  '" + dataRow["EOSDay"] + "') *  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)\r\n                     WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= '" + dataRow2["YearFrom"] + "' AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) <= '" + dataRow2["YearTo"] + "' THEN\r\n                    ((Amount/ '" + num + "') *  '" + dataRow2["EOSDay"] + "') *  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)\r\n                    WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >5 AND '" + dataRow3["YearFrom"] + "'>=5   THEN\r\n                    ((Amount/'" + num + "') *  '" + dataRow2["EOSDay"] + "' * 5)+  (((ROUND((CAST(DATEDIFF(DAY, JoiningDate, '" + asOfDate + "') AS FLOAT) ), 2)+1)/365)-5)*30*0*((Amount))ELSE 0.00 END ),2) AS Graduity \r\n                    FROM Employee E INNER JOIN Employee_PayrollItem_Detail EPD ON E.EmployeeID = EPD.EmployeeID INNER JOIN PayrollItem PI ON EPD.PayrollItemID = PI.PayrollItemID WHERE PI.InServiceBenefit = 1 AND ISNULL(E.IsTerminated, 0) = 0   ";
				if (fromEmployee != "")
				{
					text = text + " AND E.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + fromEmployee + "' ";
				}
				text += " GROUP BY E.EmployeeID, E.FirstName, E.LastName,E.TicketAmount, JoiningDate ORDER BY E.EmployeeID";
				FillDataSet(dataSet, "GratuaityDetails", text);
			}
			else if (dataTable.Rows.Count == 2)
			{
				DataRow dataRow = dataTable.Rows[0];
				DataRow dataRow2 = dataTable.Rows[1];
				text = "SELECT E.EmployeeID, E.FirstName, E.LastName, JoiningDate,E.TicketAmount, ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)  AS Tenure, \r\n                    ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 1, 2)  AS Tenure2,\r\n                   ROUND( SUM(CASE WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= 0 AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) < 1 THEN 0  \r\n                    WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= '" + dataRow["YearFrom"] + "' AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) <= '" + dataRow["YearTo"] + "' THEN\r\n                    ((Amount*'" + num + "') *  '" + dataRow["EOSDay"] + "') *  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)\r\n                    WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= '" + dataRow2["YearFrom"] + "' AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) <= '" + dataRow2["YearTo"] + "' THEN\r\n                    ((Amount*'" + num + "') *  '" + dataRow2["EOSDay"] + "') *  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)\r\n\r\n                    WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >5 AND '" + dataRow2["YearFrom"] + "'>=5   THEN\r\n                    ((Amount*'" + num + "') *  '" + dataRow2["EOSDay"] + "' * 5)+  (((ROUND((CAST(DATEDIFF(DAY, JoiningDate, '" + asOfDate + "') AS FLOAT) ), 2)+1)/365)-5)*30*0*((Amount))ELSE 0.00 END ),2) AS Graduity \r\n                    FROM Employee E INNER JOIN Employee_PayrollItem_Detail EPD ON E.EmployeeID = EPD.EmployeeID INNER JOIN PayrollItem PI ON EPD.PayrollItemID = PI.PayrollItemID WHERE PI.InServiceBenefit = 1 AND ISNULL(E.IsTerminated, 0) = 0   ";
				if (fromEmployee != "")
				{
					text = text + " AND E.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + fromEmployee + "' ";
				}
				text += " GROUP BY E.EmployeeID, E.FirstName, E.LastName,E.TicketAmount, JoiningDate ORDER BY E.EmployeeID";
				FillDataSet(dataSet, "GratuaityDetails", text);
			}
			else if (dataTable.Rows.Count == 1)
			{
				DataRow dataRow = dataTable.Rows[0];
				text = "SELECT E.EmployeeID, E.FirstName, E.LastName, JoiningDate,E.TicketAmount, ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)  AS Tenure, \r\n                    ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 1, 2)  AS Tenure2,\r\n                   ROUND( SUM(CASE WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= 0 AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) < 1 THEN 0  \r\n                    WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >= '" + dataRow["YearFrom"] + "' AND  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) <= '" + dataRow["YearTo"] + "' THEN\r\n                    ((Amount/'" + num + "') *  '" + dataRow["EOSDay"] + "') *  ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2)\r\n                \r\n                    WHEN ROUND(CAST(DATEDIFF(MONTH, JoiningDate, '" + asOfDate + "') AS FLOAT) / 12, 2) >5 AND '" + dataRow["YearFrom"] + "'>=5   THEN\r\n                    ((Amount/'" + num + "') *  '" + dataRow["EOSDay"] + "' * 5)+  (((ROUND((CAST(DATEDIFF(DAY, JoiningDate, '" + asOfDate + "') AS FLOAT) ), 2)+1)/365)-5)*30*0*((Amount ))ELSE 0.00 END ),2) AS Graduity \r\n                    FROM Employee E INNER JOIN Employee_PayrollItem_Detail EPD ON E.EmployeeID = EPD.EmployeeID INNER JOIN PayrollItem PI ON EPD.PayrollItemID = PI.PayrollItemID WHERE PI.InServiceBenefit = 1 AND ISNULL(E.IsTerminated, 0) = 0   ";
				if (fromEmployee != "")
				{
					text = text + " AND E.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + fromEmployee + "' ";
				}
				text += " GROUP BY E.EmployeeID, E.FirstName, E.LastName,E.TicketAmount, JoiningDate ORDER BY E.EmployeeID";
				FillDataSet(dataSet, "GratuaityDetails", text);
			}
			return dataSet;
		}

		public DataSet GetEmployeeEOSRule(string employeeID, bool isResigned)
		{
			DataSet dataSet = new DataSet();
			string text = "1";
			if (!isResigned)
			{
				text = "3";
			}
			string textCommand = "SELECT ESRD.* FROM Employee_EOSRule_Detail ESRD LEFT JOIN Employee_EOSRule ESR ON ESR.EOSRuleID=ESRD.EOSRuleID \r\n                        LEFT JOIN Employee_Type ET ON ET.EOSID=ESR.EOSRuleID LEFT JOIN  Employee E ON E.ContractType=ET.TypeID WHERE ISNULL(ESRD.ResignedType,1)='" + text + "' AND E.EmployeeID= '" + employeeID + "'";
			FillDataSet(dataSet, "EOSRule", textCommand);
			_ = dataSet.Tables["EOSRule"];
			return dataSet;
		}

		public DataSet GetEmployeeLoanByID(string EmployeeID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string textCommand = "SELECT *, (SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) FROM Employee_Loan_Detail ELD WHERE ELD.LoanSysDocID = EL.SysDocID AND ELD.LoanVoucherID = EL.VoucherID)  AS Balance ,\r\n                                CASE WHEN Amount=0 THEN 'True' \r\n                                WHEN (Amount<>0) AND ISNULL(PaidAmount,0)+ ISNULL(DiscountAmount,0)=0 THEN 'True' ELSE 'False' END AS CanEdit\r\n                                FROM Employee_Loan EL WHERE EmployeeID = '" + EmployeeID + "'";
				FillDataSet(dataSet, "EmployeeLoan", textCommand, sqlTransaction);
				base.DBConfig.EndTransaction(result: true);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeEOSSettlementData GetEOSByID(string sysDocID, string voucherID)
		{
			try
			{
				EmployeeEOSSettlementData employeeEOSSettlementData = new EmployeeEOSSettlementData();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string textCommand = "SELECT *\r\n                                FROM Employee_EOS WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(employeeEOSSettlementData, "Employee_EOS", textCommand, sqlTransaction);
				textCommand = "SELECT *\r\n                                FROM Employee_EOS_Deduction_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(employeeEOSSettlementData, "Employee_EOS_Deduction_Detail", textCommand, sqlTransaction);
				if (employeeEOSSettlementData != null && employeeEOSSettlementData.EmployeeEOSTable.Rows.Count > 0)
				{
					string str = employeeEOSSettlementData.EmployeeEOSTable.Rows[0]["EmployeeID"].ToString();
					string textCommand2 = "SELECT SysDocID,VoucherID,TransactionDate [Date],ELT.LoanTypeName [Loan Type],Reason ,Amount,ISNULL(PaidAmount,0)[Paid Amount],ISNULL(DiscountAmount,0) [Discount Amount],Amount-ISNULL(PaidAmount,0)-ISNULL(DiscountAmount,0) [Balance] \r\n                                FROM Employee_Loan EL Left Join Employee_Loan_Type ELT ON ELT.LoanTypeID=EL.LoanType\r\n                                WHERE Amount- ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) >0 AND ISNULL(IsVoid,'False')='False' AND EmployeeID='" + str + "'";
					FillDataSet(employeeEOSSettlementData, "Employee_Loan", textCommand2);
					textCommand2 = "SELECT ISnull(Sum(Amount-ISNULL(PaidAmount,0)-ISNULL(DiscountAmount,0)),0) [Balance] \r\n                                FROM Employee_Loan EL \r\n                                WHERE Amount- ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) >0 AND ISNULL(IsVoid,'False')='False' AND EmployeeID='" + str + "'";
					FillDataSet(employeeEOSSettlementData, "Employee_Loan_Sum", textCommand2);
				}
				base.DBConfig.EndTransaction(result: true);
				return employeeEOSSettlementData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeEOSList(DateTime from, DateTime to)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(from);
				string text2 = StoreConfiguration.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text3 = "SELECT SysDocID,VoucherID,TransactionDate [Date],E.FirstName+' '+E.LastName [Employee],NetTotal [Total],Note \r\n                                FROM Employee_EOS EOS Left Join Employee E ON E.EmployeeID=EOS.EmployeeID WHERE 1=1 ";
				if (from != DateTime.MinValue)
				{
					text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
				}
				FillDataSet(dataSet, "Employee_EOS", text3, sqlTransaction);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeBriefInfo(string employeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Employee.LocationID,Employee.DivisionID,Employee.DepartmentID,LocationName,DivisionName,DepartmentName,\r\n                                IsTerminated, IsCancelled, PositionName,Employee.PositionID,Employee.GradeID,GradeName,\r\n                                FirstName + ' ' + LastName AS [EmployeeName], Gender, JoiningDate, ConfirmationDate, LabourID,TicketAmount,\r\n                                (SELECT TOP 1 ActivityID FROM Employee_Activity WHERE ActivityType = 15 AND EmployeeID = Employee.EmployeeID) AS ActivityID,\r\n                                (SELECT Sum(Amount) FROM Employee_PayrollItem_Detail EPD  INNER JOIN PayrollItem PI ON PI.PayrollItemID=EPD.PayrollItemID  WHERE PI.InServiceBenefit = '1'   AND EPD.EmployeeID = Employee.EmployeeID Group By EPD.EmployeeID) [BasicSalary] \r\n                                FROM Employee\r\n                                LEFT OUTER JOIN Location ON Location.LocationID=Employee.LocationID\r\n                                LEFT OUTER JOIN Division ON Division.DivisionID=Employee.DivisionID\r\n                                LEFT OUTER JOIN Department ON Department.DepartmentID=Employee.DepartmentID\r\n                                LEFT OUTER JOIN Position ON Position.PositionID=Employee.PositionID\r\n                                LEFT OUTER JOIN Employee_Grade EG ON EG.GradeID=Employee.GradeID\r\n                                 WHERE Employee.EmployeeID='" + employeeID + "'";
			FillDataSet(dataSet, "Employee", textCommand);
			textCommand = "SELECT * FROM Employee_EOSSettlement  WHERE EmployeeID='" + employeeID + "'";
			FillDataSet(dataSet, "Employee_EOSSettlement", textCommand);
			return dataSet;
		}
	}
}
