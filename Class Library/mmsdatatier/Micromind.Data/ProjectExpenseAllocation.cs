using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProjectExpenseAllocation : StoreObject
	{
		private const string PROJECTEXPENSEALLOCATION_TABLE = "Project_Expense_Allocation";

		private const string PROJECTEXPENSEALLOCATIONDETAIL_TABLE = "Project_Expense_Allocation_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string MONTH_PARM = "@Month";

		private const string YEAR_PARM = "@Year";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ISVOID_PARM = "@IsVoid";

		private const string REFERENCE_PARM = "@Reference";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string TRANSACTIONSTATUS_PARM = "@TransactionStatus";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string PROJECTID_PARM = "@ProjectID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string HOURS_PARM = "@Hours";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string AMOUNT_PARM = "@Amount";

		private const string SHEETSYSDOCID_PARM = "@SheetSysDocID";

		private const string SHEETVOUCHERID_PARM = "@SheetVoucherID";

		private const string SHEETROWINDEX_PARM = "@SheetRowIndex";

		private const string OTAMOUNT_PARM = "@OTAmount";

		private const string GROSSSALARY_PARM = "@GrossSalary";

		public ProjectExpenseAllocation(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateProjectExpenseAllocationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Project_Expense_Allocation", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Month", "@Month"), new FieldValue("Year", "@Year"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionStatus", "@TransactionStatus"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProjectExpenseAllocationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProjectExpenseAllocationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProjectExpenseAllocationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@Year", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionStatus", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TransactionStatus"].SourceColumn = "TransactionStatus";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateProjectExpenseAllocationDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Project_Expense_Allocation_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("ProjectID", "@ProjectID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Hours", "@Hours"), new FieldValue("SheetSysDocID", "@SheetSysDocID"), new FieldValue("SheetVoucherID", "@SheetVoucherID"), new FieldValue("SheetRowIndex", "@SheetRowIndex"), new FieldValue("Amount", "@Amount"), new FieldValue("OTAmount", "@OTAmount"), new FieldValue("GrossSalary", "@GrossSalary"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProjectExpenseAllocationDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProjectExpenseAllocationDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProjectExpenseAllocationDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@ProjectID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@Hours", SqlDbType.Decimal);
			parameters.Add("@SheetSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SheetVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SheetRowIndex", SqlDbType.Int);
			parameters.Add("@OTAmount", SqlDbType.Decimal);
			parameters.Add("@GrossSalary", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@ProjectID"].SourceColumn = "ProjectID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Hours"].SourceColumn = "Hours";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@SheetSysDocID"].SourceColumn = "SheetSysDocID";
			parameters["@SheetVoucherID"].SourceColumn = "SheetVoucherID";
			parameters["@SheetRowIndex"].SourceColumn = "SheetRowIndex";
			parameters["@OTAmount"].SourceColumn = "OTAmount";
			parameters["@GrossSalary"].SourceColumn = "GrossSalary";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ProjectExpenseAllocationData journalData)
		{
			return true;
		}

		public bool InsertUpdateProjectExpenseAllocation(ProjectExpenseAllocationData payrollTransactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateProjectExpenseAllocationCommand = GetInsertUpdateProjectExpenseAllocationCommand(isUpdate);
			try
			{
				DataRow dataRow = payrollTransactionData.ProjectExpenseAllocationTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Project_Expense_Allocation", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				if (payrollTransactionData.ProjectExpenseAllocationDetailTable.Rows[0]["SheetSysDocID"].ToString() != "" && payrollTransactionData.ProjectExpenseAllocationDetailTable.Rows[0]["SheetVoucherID"].ToString() != "")
				{
					payrollTransactionData.ProjectExpenseAllocationDetailTable.Rows[0]["SheetSysDocID"].ToString();
					payrollTransactionData.ProjectExpenseAllocationDetailTable.Rows[0]["SheetVoucherID"].ToString();
				}
				string textCommand = "SELECT SD.LocationID,EmployeeAccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID                            \r\n                                WHERE SysDocID = '" + text2 + "'";
				string a = "";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "AccountDtls", textCommand, sqlTransaction);
				if (dataSet != null || dataSet.Tables.Count != 0 || dataSet.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow2 = dataSet.Tables["AccountDtls"].Rows[0];
					dataRow2["LocationID"].ToString();
					a = dataRow2["EmployeeAccountID"].ToString();
				}
				foreach (DataRow row in payrollTransactionData.ProjectExpenseAllocationDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string idFieldValue = row["EmployeeID"].ToString();
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Job", "WIPAccountID", "JobID", idFieldValue, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						fieldValue.ToString();
					}
					else
					{
						_ = (a != "");
					}
				}
				insertUpdateProjectExpenseAllocationCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(payrollTransactionData, "Project_Expense_Allocation", insertUpdateProjectExpenseAllocationCommand)) : (flag & Insert(payrollTransactionData, "Project_Expense_Allocation", insertUpdateProjectExpenseAllocationCommand)));
				insertUpdateProjectExpenseAllocationCommand = GetInsertUpdateProjectExpenseAllocationDetailsCommand(isUpdate: false);
				insertUpdateProjectExpenseAllocationCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteProjectExpenseAllocationDetailsRows(text2, text, sqlTransaction);
				}
				if (payrollTransactionData.Tables["Project_Expense_Allocation_Detail"].Rows.Count > 0)
				{
					flag &= Insert(payrollTransactionData, "Project_Expense_Allocation_Detail", insertUpdateProjectExpenseAllocationCommand);
				}
				foreach (DataRow row2 in payrollTransactionData.ProjectExpenseAllocationDetailTable.Rows)
				{
					row2["EmployeeID"].ToString();
					decimal.Parse(row2["Amount"].ToString());
				}
				if (payrollTransactionData.ProjectExpenseAllocationDetailTable.Rows[0]["EmployeeID"].ToString() == "")
				{
					throw new CompanyException("Employee cannot be empty.");
				}
				GLData journalData = CreateProjectExpenseAllocationGLData(payrollTransactionData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Project_Expense_Allocation", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Project Expense Allocation";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Project_Expense_Allocation", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ProjectExpenseAllocation, text2, text, "Project_Expense_Allocation", sqlTransaction);
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

		public ProjectExpenseAllocationData GetProjectExpenseAllocationByID(string sysDocID, string voucherID)
		{
			return GetProjectExpenseAllocationByID(sysDocID, voucherID, null);
		}

		public ProjectExpenseAllocationData GetProjectExpenseAllocationByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				ProjectExpenseAllocationData projectExpenseAllocationData = new ProjectExpenseAllocationData();
				string textCommand = "SELECT * FROM Project_Expense_Allocation WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(projectExpenseAllocationData, "Project_Expense_Allocation", textCommand, sqlTransaction);
				if (projectExpenseAllocationData == null || projectExpenseAllocationData.Tables.Count == 0 || projectExpenseAllocationData.Tables["Project_Expense_Allocation"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, SD.NetSalary, SD.PaidAmount, SD.NetSalary - ISNULL(SD.PaidAmount, 0) AS Balance, SS.Month, SS.Year, SS.SheetName\r\n                        FROM Project_Expense_Allocation_Detail TD \r\n                            INNER JOIN Employee ON TD.EmployeeID=Employee.EmployeeID\r\n                            INNER JOIN SalarySheet_Detail SD ON SD.SysDocID = TD.SheetSysDocID AND SD.VoucherID = TD.SheetVoucherID AND SD.RowIndex = TD.SheetRowIndex\r\n                            INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID\r\n                            WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(projectExpenseAllocationData, "Project_Expense_Allocation_Detail", textCommand, sqlTransaction);
				return projectExpenseAllocationData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteProjectExpenseAllocationDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Project_Expense_Allocation", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					bool.Parse(fieldValue.ToString());
				}
				string commandText = "DELETE FROM Project_Expense_Allocation_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidProjectExpenseAllocation(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Project_Expense_Allocation", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				if (flag2 == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				string exp = "UPDATE Project_Expense_Allocation SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Project Expense", voucherID, sysDocID, activityType, sqlTransaction);
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

		private bool ReverseSalarySheetChanges(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string textCommand = "SELECT DISTINCT SheetSysDocID,SheetVoucherID,EmployeeID, Amount FROM Payroll_Transaction_Detail PTD WHERE\r\n                           SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Payroll", textCommand);
				if (dataSet == null)
				{
					return flag;
				}
				if (dataSet.Tables.Count <= 0)
				{
					return flag;
				}
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						string text = "";
						string text2 = "";
						string text3 = "";
						text = row["SheetSysDocID"].ToString();
						text2 = row["SheetVoucherID"].ToString();
						text3 = row["EmployeeID"].ToString();
						decimal num = decimal.Parse(row["Amount"].ToString());
						if (text2 != "")
						{
							textCommand = "UPDATE SalarySheet_Detail SET  PaidAmount = PaidAmount - " + num + " WHERE SysDocID='" + text + "' AND VoucherID = '" + text2 + "' AND EmployeeID='" + text3 + "'";
							flag &= Update(textCommand, sqlTransaction);
						}
					}
					return flag;
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool DeleteProjectExpenseAllocation(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteProjectExpenseAllocationDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Project_Expense_Allocation WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Project Expense", voucherID, sysDocID, activityType, sqlTransaction);
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

		private GLData CreateProjectExpenseAllocationGLData1(ProjectExpenseAllocationData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.ProjectExpenseAllocationTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.ProjectExpenseAllocation;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			DataRow dataRow3 = null;
			foreach (DataRow row in transactionData.Tables["SalarySheet_Detail_Item"].Rows)
			{
				row["Proportion"].ToString();
				foreach (DataRow row2 in transactionData.ProjectExpenseAllocationDetailTable.Rows)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["IsARAP"] = true;
					dataRow3["PayeeType"] = "E";
					dataRow3["PayeeID"] = row2["ProjectID"];
					dataRow3["Debit"] = row2["Amount"];
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["CreditFC"] = DBNull.Value;
					num += decimal.Parse(row2["Amount"].ToString());
					dataRow3["Description"] = DBNull.Value;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = row2["RowIndex"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			if (num < 0m)
			{
				throw new CompanyException("Total amount cannot be negative.");
			}
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["PayeeType"] = "A";
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = num;
			dataRow3["Description"] = DBNull.Value;
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateProjectExpenseAllocationGLData(ProjectExpenseAllocationData ExpenseAllocationData, SqlTransaction sqlTransaction)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				GLData gLData = new GLData();
				DataRow dataRow = ExpenseAllocationData.ProjectExpenseAllocationTable.Rows[0];
				string text = "";
				string text2 = dataRow["SysDocID"].ToString();
				dataRow["VoucherID"].ToString();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.ProjectExpenseAllocation;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = "";
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				string commaSeperatedIDs = GetCommaSeperatedIDs(ExpenseAllocationData, "Project_Expense_Allocation_Detail", "EmployeeID");
				string textCommand = "SELECT EmployeeID,ISNULL(EMP.AccountID,ISNULL(ET.AccountID, LOC.EmployeeAccountID)) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                LEFT OUTER JOIN Location LOC ON Loc.LocationID  = (SELECT LocationID FROM System_Document WHERE SysDocID = '" + text2 + "')\r\n                                WHERE EMP.EmployeeID IN (" + commaSeperatedIDs + ") ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Employee", textCommand, sqlTransaction);
				DataTable dataTable = ExpenseAllocationData.Tables["Project_Expense_Allocation_Detail"].DefaultView.ToTable("DistinctTable", true, "EmployeeID");
				DataTable dataTable2 = ExpenseAllocationData.Tables["Project_Expense_Allocation_Detail"].DefaultView.ToTable("DistinctTable", false, "Amount");
				int count = dataTable.Rows.Count;
				for (int i = 0; i < count; i++)
				{
					DataRow dataRow3 = dataTable.Rows[i];
					DataRow dataRow4 = dataTable2.Rows[i];
					string text3 = dataRow3["EmployeeID"].ToString();
					dataRow4["Amount"].ToString();
					decimal num = default(decimal);
					foreach (DataRow row in ExpenseAllocationData.Tables["Project_Expense_Allocation_Detail"].Rows)
					{
						if (text3 == row["EmployeeID"])
						{
							num += Convert.ToDecimal(row["Amount"]);
						}
						else
						{
							num = num;
						}
					}
					DataRow[] array = dataSet.Tables[0].Select("EmployeeID = '" + text3 + "'");
					DataRow[] array2 = null;
					if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
					{
						text = array[0]["AccountID"].ToString();
					}
					if (text == "")
					{
						throw new CompanyException("Account is not set for the employee '" + text3 + "'.", 1021);
					}
					double num2 = 0.0;
					string value = "";
					array = ExpenseAllocationData.Tables["SalarySheet_Detail_Item"].Select("EmployeeID = '" + text3 + "'");
					foreach (DataRow dataRow6 in array)
					{
						DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						string text4 = dataRow6["PayrollItemID"].ToString();
						string text5 = "";
						object obj = null;
						obj = new Databases(base.DBConfig).GetFieldValue("PayrollItem", "AccountID", "PayrollItemID", text4, sqlTransaction);
						if (obj == null || obj.ToString() == "")
						{
							obj = new Databases(base.DBConfig).GetFieldValue("Employee_OverTime", "AccountID", "OverTimeID", text4, sqlTransaction);
						}
						if (obj == null || obj.ToString() == "")
						{
							obj = new Databases(base.DBConfig).GetFieldValue("Benefit", "AccountID", "BenefitID", text4, sqlTransaction);
						}
						if (obj == null || obj.ToString() == "")
						{
							text5 = dataRow6["LoanSysDocID"].ToString();
							textCommand = "SELECT AccountID FROM Employee_Loan_Type ELT\r\n\t\t\t\t\t\t                     INNER JOIN  Employee_Loan EL ON ELT.LoanTypeID=EL.LoanType WHERE SysDocID = '" + text5 + "' AND VoucherID = '" + text4 + "' ";
							obj = ExecuteScalar(textCommand, sqlTransaction);
							if (obj == null || obj.ToString() == "")
							{
								throw new CompanyException("Account is not set for Loan  '" + text4 + "'.", 1022);
							}
							text = obj.ToString();
						}
						if (obj == null || obj.ToString() == "")
						{
							throw new CompanyException("Account is not set for payroll item '" + text4 + "'.", 1022);
						}
						text = obj.ToString();
						DataRow obj2 = ExpenseAllocationData.Tables["SalarySheet_Detail_Item"].Select("PayrollItemID = '" + text4 + "' AND EmployeeID = '" + text3 + "'")[0];
						string s = obj2["Proportion"].ToString();
						dataRow7["JournalID"] = 0;
						dataRow7["AccountID"] = text;
						dataRow7["PayeeID"] = text3;
						dataRow7["PayeeType"] = "";
						byte b = 1;
						b = byte.Parse(obj2["PayType"].ToString());
						decimal num3 = default(decimal);
						foreach (DataRow row2 in ExpenseAllocationData.Tables["Project_Expense_Allocation_Detail"].Rows)
						{
							if (text3 == row2["EmployeeID"])
							{
								num3 += Convert.ToDecimal(row2["Amount"]);
							}
							else
							{
								num3 = num3;
							}
						}
						if (b == 1)
						{
							switch (b)
							{
							case 1:
								dataRow7["Credit"] = Math.Round(decimal.Parse(s) * num3, currencyDecimalPoints);
								dataRow7["Debit"] = DBNull.Value;
								break;
							case 2:
								dataRow7["Credit"] = DBNull.Value;
								dataRow7["Debit"] = Math.Round(Math.Abs(decimal.Parse(s)) * Math.Abs(num3), currencyDecimalPoints);
								break;
							case 3:
								dataRow7["Credit"] = DBNull.Value;
								dataRow7["Debit"] = Math.Round(Math.Abs(decimal.Parse(s)) * Math.Abs(num3), currencyDecimalPoints);
								break;
							}
							dataRow7["Description"] = dataRow6["Description"].ToString();
							dataRow7["Reference"] = dataRow["Reference"];
							dataRow7.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow7);
						}
					}
					array = ExpenseAllocationData.Tables["Project_Expense_Allocation_Detail"].Select("EmployeeID = '" + text3 + "'");
					for (int k = 0; k < array.Length; k++)
					{
						string text6 = array[k]["ProjectID"].ToString();
						string text7 = "";
						array2 = ExpenseAllocationData.Tables["SalarySheet_Detail_Item"].Select("EmployeeID = '" + text3 + "'");
						for (int l = 0; l < array2.Length; l++)
						{
							DataRow obj3 = array2[l];
							string s2 = obj3["Proportion"].ToString();
							obj3["Description"].ToString();
							string text8 = obj3["PayrollItemID"].ToString();
							DataRow dataRow9 = array[k];
							dataRow9["ProjectID"].ToString();
							num2 = Math.Round(double.Parse(dataRow9["Amount"].ToString()), currencyDecimalPoints);
							num2 = Math.Round(double.Parse(s2) * double.Parse(dataRow9["Amount"].ToString()), currencyDecimalPoints);
							byte b2 = 1;
							b2 = byte.Parse(array2[l]["PayType"].ToString());
							object obj4 = null;
							switch (b2)
							{
							case 1:
							case 2:
								obj4 = new Databases(base.DBConfig).GetFieldValue("PayrollItem", "AccountID", "PayrollItemID", text8, sqlTransaction);
								if (obj4 == null || obj4.ToString() == "")
								{
									obj4 = new Databases(base.DBConfig).GetFieldValue("Benefit", "AccountID", "BenefitID", text8, sqlTransaction);
								}
								if (obj4 == null || obj4.ToString() == "")
								{
									obj4 = new Databases(base.DBConfig).GetFieldValue("Employee_OverTime", "AccountID", "OverTimeID", text8, sqlTransaction);
								}
								if (obj4 == null || obj4.ToString() == "")
								{
									throw new CompanyException("Account is not set for payroll item '" + text8 + "'.", 1022);
								}
								value = obj4.ToString();
								break;
							case 4:
								if (obj4 == null || obj4.ToString() == "")
								{
									throw new CompanyException("Account is not set for payroll item '" + text8 + "'.", 1022);
								}
								value = obj4.ToString();
								break;
							case 3:
								text7 = array2[l]["LoanSysDocID"].ToString();
								textCommand = "SELECT AccountID FROM Employee_Loan_Type ELT\r\n\t\t\t\t\t\t                     INNER JOIN  Employee_Loan EL ON ELT.LoanTypeID=EL.LoanType WHERE SysDocID = '" + text7 + "' AND VoucherID = '" + text8 + "' ";
								obj4 = ExecuteScalar(textCommand, sqlTransaction);
								if (obj4 == null || obj4.ToString() == "")
								{
									throw new CompanyException("Account is not set for Loan  '" + text8 + "'.", 1022);
								}
								value = obj4.ToString();
								break;
							}
							DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
							dataRow7.BeginEdit();
							dataRow7["JournalID"] = 0;
							dataRow7["AccountID"] = value;
							dataRow7["PayeeID"] = text3;
							dataRow7["Description"] = text6 + "-" + text8;
							dataRow7["JobID"] = text6;
							if (b2 == 1)
							{
								switch (b2)
								{
								case 1:
									dataRow7["Debit"] = num2;
									dataRow7["Credit"] = DBNull.Value;
									break;
								case 2:
									dataRow7["Debit"] = DBNull.Value;
									dataRow7["Credit"] = Math.Abs(num2);
									break;
								case 3:
									dataRow7["Debit"] = DBNull.Value;
									dataRow7["Credit"] = Math.Abs(num2);
									break;
								default:
									throw new CompanyException("PayType is not defined.");
								}
								dataRow7["Reference"] = dataRow["Reference"];
								dataRow7.EndEdit();
								gLData.JournalDetailsTable.Rows.Add(dataRow7);
							}
						}
					}
				}
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				foreach (DataRow row3 in gLData.JournalDetailsTable.Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row3["Debit"].ToString(), out result);
					decimal.TryParse(row3["Credit"].ToString(), out result2);
					d += result;
					d2 += result2;
				}
				decimal num4 = d - d2;
				if (num4 != 0m)
				{
					decimal result3 = default(decimal);
					decimal.TryParse(gLData.JournalDetailsTable.Rows[0]["Credit"].ToString(), out result3);
					gLData.JournalDetailsTable.Rows[0]["Credit"] = result3 + num4;
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEmployeeSalaryReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			string str = "";
			CommonLib.ToSqlDateTimeString(from);
			string str2 = CommonLib.ToSqlDateTimeString(to);
			string text = "SELECT DISTINCT PTD.EmployeeID,FirstName + ' ' + LastName AS [EmployeeName]\r\n                            FROM Employee    \r\n                            INNER JOIN Payroll_Transaction_Detail PTD ON Employee.EmployeeID=PTD.EmployeeID\r\n                            INNER JOIN Payroll_Transaction PT ON PT.SysDocID=PTD.SysDocID AND PT.VoucherID=PTD.VoucherID ";
			if (!showZeroBalance)
			{
				text = text + " AND TransactionDate < '" + str2 + "' ";
				text += " AND ISNULL(PT.IsVoid,'False')='False' ";
			}
			if (fromEmployee != "")
			{
				text = text + " AND PTD.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromEmployee != "")
			{
				str = str + " AND PTD.EmployeeID >= '" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND PTD.EmployeeID <= '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			text += " ORDER BY PTD.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", text);
			DataSet dataSet2 = new DataSet();
			text = "SELECT PTD.SysDocID,PTD.VoucherID,PTD.EmployeeID,FirstName + ' ' + LastName AS [EmployeeName],PayType,[Month],StartDate,EndDate,PT.PaymentMethodType,TransactionDate,\r\n                        ISNULL(PayrollItemID,LoanVoucherID) AS PayrollItemID,Days,\r\n                        CASE ISNULL(PayType,1) WHEN 1 THEN PTD.Amount WHEN 2 THEN PTD.Amount * -1 WHEN 3 THEN PTD.Amount * -1 END AS Amount\r\n                        FROM Payroll_Transaction_Detail PTD \r\n                        INNER JOIN Payroll_Transaction PT ON PT.SysDocID=PTD.SysDocID AND PT.VoucherID=PTD.VoucherID\r\n                        INNER JOIN Employee ON Employee.EmployeeID=PTD.EmployeeID ";
			text += " AND ISNULL(PT.IsVoid,'False')='False' ";
			if (fromEmployee != "")
			{
				text = text + " AND PTD.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			text += " ORDER BY PTD.EmployeeID,TransactionDate";
			FillDataSet(dataSet2, "Project_Expense_Allocation", text);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Employee"].Columns["EmployeeID"], dataSet.Tables["Project_Expense_Allocation"].Columns["EmployeeID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetSalaryCashList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   P.SysDocID,P.VoucherID,CAST(DATENAME(month,SS.StartDate )  AS CHAR(10))AS SalMonth, SS.Year, SS.SheetName\r\n                               FROM Payroll_Transaction P\r\n                                INNER JOIN Payroll_Transaction_Detail TD ON P.SysDocID=TD.SysDocID AND P.VoucherID=TD.VoucherID\r\n                                INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID \r\n                             WHERE P.PaymentMethodType=1  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND P.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Project_Expense_Allocation", sqlCommand);
			return dataSet;
		}

		public DataSet GetSalaryChequeList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   P.SysDocID,P.VoucherID,CAST(DATENAME(month,SS.StartDate )  AS CHAR(10))AS SalMonth, SS.Year, SS.SheetName\r\n                               FROM Payroll_Transaction P\r\n                                INNER JOIN Payroll_Transaction_Detail TD ON P.SysDocID=TD.SysDocID AND P.VoucherID=TD.VoucherID\r\n                                INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID \r\n                             WHERE P.PaymentMethodType=2  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND P.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Project_Expense_Allocation", sqlCommand);
			return dataSet;
		}

		public DataSet GetSalaryBankList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   P.SysDocID,P.VoucherID,CAST(DATENAME(month,SS.StartDate )  AS CHAR(10))AS SalMonth, SS.Year, SS.SheetName\r\n                               FROM Payroll_Transaction P\r\n                                INNER JOIN Payroll_Transaction_Detail TD ON P.SysDocID=TD.SysDocID AND P.VoucherID=TD.VoucherID\r\n                                INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID \r\n                             WHERE P.PaymentMethodType=4  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND P.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Project_Expense_Allocation", sqlCommand);
			return dataSet;
		}

		public DataSet GetEmployeeSalaryToPrint(string sysDocID, string voucherID)
		{
			try
			{
				ProjectExpenseAllocationData projectExpenseAllocationData = new ProjectExpenseAllocationData();
				string cmdText = "SELECT PT.*,A.AccountName,(SELECT BankName FROM Chequebook C LEFT JOIN Bank B ON C.BankID=B.BankID WHERE C.ChequebookID=PT.ChequebookID) AS 'PayeeBank' FROM Payroll_Transaction PT LEFT JOIN Account A ON PT.BankAccountID=A.AccountID  WHERE PT.VoucherID='" + voucherID + "' AND PT.SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(projectExpenseAllocationData, "Project_Expense_Allocation", sqlCommand);
				if (projectExpenseAllocationData == null || projectExpenseAllocationData.Tables.Count == 0 || projectExpenseAllocationData.Tables["Project_Expense_Allocation"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT TD.*,Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, SD.NetSalary, SD.PaidAmount, SD.NetSalary - ISNULL(SD.PaidAmount, 0) AS Balance, SS.Month, SS.Year, SS.SheetName\r\n                        FROM Payroll_Transaction_Detail TD \r\n                            INNER JOIN Employee ON TD.EmployeeID=Employee.EmployeeID\r\n                            INNER JOIN SalarySheet_Detail SD ON SD.SysDocID = TD.SheetSysDocID AND SD.VoucherID = TD.SheetVoucherID AND SD.RowIndex = TD.SheetRowIndex\r\n                            INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID\r\n                            WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(projectExpenseAllocationData, "Project_Expense_Allocation_Detail", cmdText);
				projectExpenseAllocationData.Relations.Add("PayrollDetail", new DataColumn[2]
				{
					projectExpenseAllocationData.Tables["Project_Expense_Allocation"].Columns["SysDocID"],
					projectExpenseAllocationData.Tables["Project_Expense_Allocation"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					projectExpenseAllocationData.Tables["Project_Expense_Allocation_Detail"].Columns["SysDocID"],
					projectExpenseAllocationData.Tables["Project_Expense_Allocation_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return projectExpenseAllocationData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GeWPFToPrint(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT TD.SysDocID,TD.VoucherID,TD.EmployeeID,TD.AccountID,TD.Days,TD.Amount,TD.RowIndex,TD.SheetSysDocID,TD.SheetVoucherID,TD.SheetRowIndex,\r\n                           E.FirstName + ' ' + E.LastName AS EmployeeName, SD.NetSalary, SD.PaidAmount,E.IBAN,B.RoutingCode,E.LabourID,S.MOLId,SS.StartDate,SS.EndDate,\r\n                           SD.NetSalary - ISNULL(SD.PaidAmount, 0) AS Balance, SS.Month, SS.Year, SS.SheetName,E.BankID,B.BankName,\r\n                           (SELECT SUM(SDI.PayableAmount) FROM SalarySheet_Detail_Item SDI INNER JOIN PayrollItem P ON P.PayrollItemID=SDI.PayrollItemID  AND ISNULL(P.InFixed,0)=1  WHERE SDI.SysDocID= TD.SheetSysDocID AND  SDI.VoucherID=TD.SheetVoucherID AND SDI.EmployeeID=TD.EmployeeID ) [Fixed Salary],\r\n                           (SELECT SUM(SDI.PayableAmount) FROM SalarySheet_Detail_Item SDI INNER JOIN PayrollItem P ON P.PayrollItemID=SDI.PayrollItemID AND ISNULL(P.InFixed,0)=0 WHERE SDI.SysDocID= TD.SheetSysDocID AND  SDI.VoucherID=TD.SheetVoucherID AND SDI.EmployeeID=TD.EmployeeID ) [Variable Salary],\r\n                           (DAY(EOMONTH(SS.StartDate))-TD.Days) AS AbscentDay\r\n                           FROM Payroll_Transaction_Detail TD \r\n                           INNER JOIN Employee E ON TD.EmployeeID=E.EmployeeID\r\n                           LEFT OUTER JOIN Sponsor S ON E.SponsorID=S.SponsorID\r\n                           LEFT OUTER JOIN Bank B ON E.BankID=B.BankID\r\n                           INNER JOIN SalarySheet_Detail SD ON SD.SysDocID = TD.SheetSysDocID AND SD.VoucherID = TD.SheetVoucherID AND SD.RowIndex = TD.SheetRowIndex\r\n                           INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID";
			text = text + " WHERE TD.SysDocID= '" + sysDocID + "' AND TD.VoucherID= '" + voucherID + "'";
			SqlCommand sqlCommand = new SqlCommand(text);
			FillDataSet(dataSet, "Project_Expense_Allocation", sqlCommand);
			return dataSet;
		}
	}
}
