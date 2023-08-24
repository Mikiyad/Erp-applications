using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Salary : StoreObject
	{
		public const string DOCID_PARM = "@VoucherID";

		public const string APPROVEDBY_PARM = "@ApprovedBy";

		public const string APPROVALDATE_PARM = "@ApprovalDate";

		public const string PERIOD_PARM = "@TransactionDate";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string EMPLOYEEID_PARM = "@EmployeeID";

		public const string EMPLOYEENAME_PARM = "@EmployeeName";

		public const string JOBID_PARM = "@JobID";

		public const string PAYROLLPERIOD_PARM = "@PayrollPeriod";

		public const string DEDUCTIONCODE_PARM = "@DeductionCode";

		public const string ADDITIONCODE_PARM = "@AdditionCode";

		public const string REMARKS_PARM = "@Remarks";

		public const string ROWINDEX_PARM = "@RowIndex";

		public const string AMOUNT_PARM = "@Amount";

		public const string AMOUNTTYPE_PARM = "@AmountType";

		public const string QUANTITY_PARM = "@Quantity";

		public const string RATE_PARM = "@Rate";

		private const string SALARYDEDUCTION_TABLE = "Salary_Deduction";

		private const string SALARYDEDUCTION_DETAIL_TABLE = "Salary_Deduction_Detail";

		private const string SALARYADDITION_TABLE = "Salary_Addition";

		private const string SALARYADDITION_DETAIL_TABLE = "Salary_Addition_Detail";

		public Salary(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalaryDeductionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Salary_Deduction", new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ApprovedBy", "@ApprovedBy"), new FieldValue("ApprovalDate", "@ApprovalDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Salary_Deduction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalaryDeductionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalaryDeductionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalaryDeductionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ApprovedBy", SqlDbType.NVarChar);
			parameters.Add("@ApprovalDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ApprovedBy"].SourceColumn = "ApprovedBy";
			parameters["@ApprovalDate"].SourceColumn = "ApprovalDate";
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

		private string GetInsertUpdateSalaryDeductionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Salary_Deduction_Detail", new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("EmployeeName", "@EmployeeName"), new FieldValue("PayrollPeriod", "@PayrollPeriod"), new FieldValue("DeductionCode", "@DeductionCode"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Amount", "@Amount"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Rate", "@Rate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalaryDeductionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalaryDeductionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalaryDeductionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeName", SqlDbType.NVarChar);
			parameters.Add("@PayrollPeriod", SqlDbType.DateTime);
			parameters.Add("@DeductionCode", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@Rate", SqlDbType.Money);
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@EmployeeName"].SourceColumn = "EmployeeName";
			parameters["@PayrollPeriod"].SourceColumn = "PayrollPeriod";
			parameters["@DeductionCode"].SourceColumn = "DeductionCode";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Rate"].SourceColumn = "Rate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalaryData journalData)
		{
			return true;
		}

		private string GetInsertUpdateSalaryAdditionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Salary_Addition", new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ApprovedBy", "@ApprovedBy"), new FieldValue("ApprovalDate", "@ApprovalDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Salary_Addition", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalaryAdditionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalaryAdditionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalaryAdditionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ApprovedBy", SqlDbType.NVarChar);
			parameters.Add("@ApprovalDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ApprovedBy"].SourceColumn = "ApprovedBy";
			parameters["@ApprovalDate"].SourceColumn = "ApprovalDate";
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

		private string GetInsertUpdateSalaryAdditionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Salary_Addition_Detail", new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("EmployeeName", "@EmployeeName"), new FieldValue("PayrollPeriod", "@PayrollPeriod"), new FieldValue("AdditionCode", "@AdditionCode"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountType", "@AmountType"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Rate", "@Rate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalaryAdditionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalaryAdditionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalaryAdditionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeName", SqlDbType.NVarChar);
			parameters.Add("@PayrollPeriod", SqlDbType.DateTime);
			parameters.Add("@AdditionCode", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountType", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@Rate", SqlDbType.Money);
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@EmployeeName"].SourceColumn = "EmployeeName";
			parameters["@PayrollPeriod"].SourceColumn = "PayrollPeriod";
			parameters["@AdditionCode"].SourceColumn = "AdditionCode";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountType"].SourceColumn = "AmountType";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Rate"].SourceColumn = "Rate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateSalaryDeduction(SalaryData salaryEntryData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalaryDeductionCommand = GetInsertUpdateSalaryDeductionCommand(isUpdate);
			try
			{
				DataRow dataRow = salaryEntryData.SalaryDeductionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Salary_Deduction", "VoucherID", text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in salaryEntryData.SalaryDeductionDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteSalaryDeductionDetailsRows(text, sqlTransaction);
				}
				insertUpdateSalaryDeductionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salaryEntryData, "Salary_Deduction", insertUpdateSalaryDeductionCommand)) : (flag & Insert(salaryEntryData, "Salary_Deduction", insertUpdateSalaryDeductionCommand)));
				if (salaryEntryData.Tables["Salary_Deduction_Detail"].Rows.Count > 0)
				{
					insertUpdateSalaryDeductionCommand = GetInsertUpdateSalaryDeductionDetailsCommand(isUpdate: false);
					insertUpdateSalaryDeductionCommand.Transaction = sqlTransaction;
					flag &= Insert(salaryEntryData, "Salary_Deduction_Detail", insertUpdateSalaryDeductionCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Salary_Deduction", "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Salary Deduction";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
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

		public bool InsertUpdateSalaryAddition(SalaryData salaryEntryData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalaryAdditionCommand = GetInsertUpdateSalaryAdditionCommand(isUpdate);
			try
			{
				DataRow dataRow = salaryEntryData.SalaryAdditionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Salary_Addition", "VoucherID", text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in salaryEntryData.SalaryAdditionDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteSalaryAdditionDetailsRows(text, sqlTransaction);
				}
				insertUpdateSalaryAdditionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salaryEntryData, "Salary_Addition", insertUpdateSalaryAdditionCommand)) : (flag & Insert(salaryEntryData, "Salary_Addition", insertUpdateSalaryAdditionCommand)));
				if (salaryEntryData.Tables["Salary_Addition_Detail"].Rows.Count > 0)
				{
					insertUpdateSalaryAdditionCommand = GetInsertUpdateSalaryAdditionDetailsCommand(isUpdate: false);
					insertUpdateSalaryAdditionCommand.Transaction = sqlTransaction;
					flag &= Insert(salaryEntryData, "Salary_Addition_Detail", insertUpdateSalaryAdditionCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Salary_Addition", "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Salary Addition";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
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

		public SalaryData GetSalaryDeductionByID(string voucherID)
		{
			try
			{
				SalaryData salaryData = new SalaryData();
				string textCommand = "SELECT * FROM Salary_Deduction WHERE VoucherID='" + voucherID + "'";
				FillDataSet(salaryData, "Salary_Deduction", textCommand);
				if (salaryData == null || salaryData.Tables.Count == 0 || salaryData.Tables["Salary_Deduction"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM Salary_Deduction_Detail WHERE VoucherID='" + voucherID + "'";
				FillDataSet(salaryData, "Salary_Deduction_Detail", textCommand);
				return salaryData;
			}
			catch
			{
				throw;
			}
		}

		public SalaryData GetSalaryAdditionByID(string voucherID)
		{
			try
			{
				SalaryData salaryData = new SalaryData();
				string textCommand = "SELECT * FROM Salary_Addition WHERE VoucherID='" + voucherID + "'";
				FillDataSet(salaryData, "Salary_Addition", textCommand);
				if (salaryData == null || salaryData.Tables.Count == 0 || salaryData.Tables["Salary_Addition"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM Salary_Addition_Detail WHERE VoucherID='" + voucherID + "'  order by RowIndex ";
				FillDataSet(salaryData, "Salary_Addition_Detail", textCommand);
				return salaryData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalaryDeductionDetailsRows(string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Salary_Deduction_Detail WHERE VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalaryAdditionDetailsRows(string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Salary_Addition_Detail WHERE VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidSalary(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteSalaryDeduction(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteSalaryDeductionDetailsRows(voucherID, sqlTransaction);
				text = "DELETE FROM Salary_Deduction WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Salary Deduction", voucherID, activityType, sqlTransaction);
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

		public bool DeleteSalaryAddition(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteSalaryAdditionDetailsRows(voucherID, sqlTransaction);
				text = "DELETE FROM Salary_Addition WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Salary Addition", voucherID, activityType, sqlTransaction);
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

		public DataSet GetSalaryDeductionToPrint(string[] voucherID)
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
				string cmdText = "SELECT * FROM Salary_Deduction WHERE VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Salary_Deduction", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Salary_Deduction"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Salary_Deduction_Detail WHERE VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Salary_Deduction_Detail", cmdText);
				dataSet.Relations.Add("SalaryDeduction", new DataColumn[1]
				{
					dataSet.Tables["Salary_Deduction"].Columns["VoucherID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Salary_Deduction_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalaryAdditionToPrint(string[] voucherID)
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
				string cmdText = "SELECT * FROM Salary_Addition WHERE VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Salary_Addition", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Salary_Addition"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Salary_Addition_Detail WHERE VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Salary_Addition_Detail", cmdText);
				dataSet.Relations.Add("SalaryAddition", new DataColumn[1]
				{
					dataSet.Tables["Salary_Addition"].Columns["VoucherID"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Salary_Addition_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet LoadPayrollItem(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string EmployeeIDs, string basedon)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				text = "select SUM(Amount) AS Amount, EPD.EmployeeID,E.FirstName+''+E.LastName as [Employee Name],'Leave Salary Amount' as Remarks  from Employee_PayrollItem_Detail EPD\r\n                                LEFT JOIN PayrollItem P ON EPD.PayrollItemID=P.PayrollItemID \r\n                                LEFT JOIN Employee E ON EPD.EmployeeID=E.EmployeeID\r\n                                where P.InLeaveSalary='true' AND DATEDIFF(Day, E.JoiningDate,GetDate())>=12 AND  ISNULL(IsTerminated, 'False') = 'False' AND  ISNULL(IsCancelled, 'False') = 'False' AND  ISNULL(IsEOSSettled,'False')='False'\r\n                                  ";
				if (EmployeeIDs != "")
				{
					text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text = text + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text = text + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text = text + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text = text + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text = text + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text = text + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text = text + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text = text + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text = text + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text = text + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text = text + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text = text + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text = text + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text = text + " AND E.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text = text + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text = text + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text = text + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text = text + " AND E.AccountID <='" + toAccount + "' ";
				}
				text += " group by EPD.EmployeeID, E.FirstName,E.LastName";
				FillDataSet(dataSet, "PayRollData", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet LoadTicketAmount(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string EmployeeIDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				CommonLib.ToSqlDateTimeString(new DateTime(from.Year, from.Month, from.Day, 0, 0, 0));
				string text = "SELECT EmployeeID,FirstName+''+LastName as [Employee Name], TicketAmount as Amount ,'Leave Ticket Amount' as Remarks FROM Employee E WHERE\r\n                                    DATEDIFF(month, E.JoiningDate,'" + from + "' )>=12 and\r\n                                   ISNULL(IsTerminated, 'False') = 'False' AND  ISNULL(IsCancelled, 'False') = 'False' AND  ISNULL(IsEOSSettled,'False')='False'  AND MONTH(E.JoiningDate)= " + from.Month + " AND\r\n                                   (ContractType IS NULL OR ContractType IN(SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll, 'False') = 'True'))";
				if (EmployeeIDs != "")
				{
					text = text + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					text = text + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text = text + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text = text + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text = text + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text = text + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text = text + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text = text + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text = text + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text = text + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text = text + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text = text + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text = text + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text = text + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text = text + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text = text + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text = text + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text = text + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text = text + " AND E.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text = text + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text = text + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text = text + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text = text + " AND E.AccountID <='" + toAccount + "' ";
				}
				FillDataSet(dataSet, "Employee", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet LoadDeductionAmount(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string EmployeeIDs, int percent)
		{
			try
			{
				DataSet dataSet = new DataSet();
				CommonLib.ToSqlDateTimeString(new DateTime(from.Year, from.Month, from.Day, 0, 0, 0));
				string str = "select  (SUM(Amount)*" + percent + ")/100 AS Amount, EPD.EmployeeID,E.FirstName+''+E.LastName as [Employee Name], 'Deduction Amount' as Remarks from Employee_PayrollItem_Detail EPD\r\n\r\n\t\t\t\t\t\t\tLEFT JOIN PayrollItem P ON EPD.PayrollItemID=P.PayrollItemID \r\n\t\t\t\t\t\t\tLEFT JOIN Employee E ON EPD.EmployeeID=E.EmployeeID\r\n\t\t\t\t\t\t\twhere P.InSalaryDeduction='true'";
				if (EmployeeIDs != "")
				{
					str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
				}
				if (fromEmployee != "")
				{
					str = str + " AND E.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					str = str + " AND E.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					str = str + " AND E.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					str = str + " AND E.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					str = str + " AND E.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					str = str + " AND E.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					str = str + " AND E.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					str = str + " AND E.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					str = str + " AND E.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					str = str + " AND E.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					str = str + " AND E.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					str = str + " AND E.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					str = str + " AND E.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					str = str + " AND E.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					str = str + " AND E.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					str = str + " AND E.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					str = str + " AND E.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					str = str + " AND E.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					str = str + " AND E.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					str = str + " AND E.AccountID <='" + toAccount + "' ";
				}
				str += " group by EPD.EmployeeID, E.FirstName,E.LastName";
				FillDataSet(dataSet, "Employee", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public decimal TotalLeaveSalary(string EmployeeID)
		{
			SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
			decimal num = default(decimal);
			string exp = "SELECT SUM(Amount) AS Amount from Employee_PayrollItem_Detail EPD\r\n                                LEFT JOIN PayrollItem P ON EPD.PayrollItemID=P.PayrollItemID \r\n                                LEFT JOIN Employee E ON EPD.EmployeeID=E.EmployeeID\r\n                                where P.InLeaveSalary='true' AND EPD.EmployeeID = '" + EmployeeID + "' ";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj == null || obj.ToString() == "")
			{
				throw new CompanyException("Salary is not assigned for Employee  '" + EmployeeID + "'.", 1022);
			}
			num = decimal.Parse(obj.ToString());
			base.DBConfig.EndTransaction(result: true);
			return num;
		}

		public DataSet GetAdditionList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT DISTINCT  SA.VoucherID [Doc Number],SAD.EmployeeID [Employee Code],EmployeeName [Employee Name],TransactionDate [ Date]\r\n                           \r\n                            FROM         Salary_Addition SA\r\n                            INNER JOIN Salary_Addition_Detail SAD ON SA.VoucherID=SAD.VoucherID\r\n                            Inner JOIN Employee ON Employee.EmployeeID=SAD.EmployeeID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY SA.VoucherID,SAD.EmployeeID ,EmployeeName,TransactionDate";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Salary_Addition", sqlCommand);
			return dataSet;
		}

		public DataSet GetDeductionList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT DISTINCT  SA.VoucherID [Doc Number],SAD.EmployeeID [Employee Code],EmployeeName [Employee Name],TransactionDate [ Date]\r\n                           \r\n                            FROM         Salary_Deduction SA\r\n                            INNER JOIN Salary_Deduction_Detail SAD ON SA.VoucherID=SAD.VoucherID\r\n                            Inner JOIN Employee ON Employee.EmployeeID=SAD.EmployeeID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY SA.VoucherID,SAD.EmployeeID ,EmployeeName,TransactionDate";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Salary_Deduction", sqlCommand);
			return dataSet;
		}
	}
}
