using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Micromind.Data
{
	public sealed class SalarySheet : StoreObject
	{
		private const string SALARYSHEET_TABLE = "SalarySheet";

		private const string SALARYSHEETDETAIL_TABLE = "SalarySheet_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string SHEETNAME_PARM = "@SheetName";

		private const string MONTH_PARM = "@Month";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string YEAR_PARM = "@Year";

		private const string NOTE_PARM = "@Note";

		private const string REFERENCE_PARM = "@Reference";

		private const string ISPOSTED_PARM = "@IsPosted";

		private const string ISVOID_PARM = "@IsVoid";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PAYTYPE_PARM = "@PayType";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string WORKDAYS_PARM = "@WorkDays";

		private const string ABSENT_PARM = "@Absent";

		public const string ANNUALLEAVES_PARM = "@AnnualLeaves";

		public const string UNPAIDANNUALLEAVES_PARM = "@UnpaidAnnualLeaves";

		public const string NORMALLEAVES_PARM = "@NormalLeaves";

		public const string SICKLEAVES_PARM = "@SickLeaves";

		private const string BASIC_PARM = "@Basic";

		private const string ALLOWANCE_PARM = "@Allowance";

		private const string DEDUCTION_PARM = "@Deduction";

		private const string LOANDEDUCTION_PARM = "@LoanDeduction";

		private const string OTHOURS_PARM = "@OTHours";

		private const string OTRATE_PARM = "@OTRate";

		private const string OTAMOUNT_PARM = "@OTAmount";

		private const string OTBASE_PARM = "@OTBase";

		private const string NETSALARY_PARM = "@NetSalary";

		private const string OTISFIXED_PARM = "@OTIsFixed";

		private const string OTFIXEDAMOUNT_PARM = "@OTFixedAmount";

		private const string OTFACTOR_PARM = "@OTFactor";

		private const string REMARKS_PARM = "@Remarks";

		private const string PAYROLLITEMID_PARM = "@PayrollItemID";

		private const string LOANSYSDOCID_PARM = "@LoanSysDocID";

		private const string AMOUNT_PARM = "@Amount";

		private const string DESCRIPTION_PARM = "@Description";

		private const string PAYABLEAMOUNT_PARM = "@PayableAmount";

		private const string PAID_PARM = "@Paid";

		private const string INDEDUCTION_PARM = "@InDeduction";

		private const string PAYCODETYPE_PARM = "@PayCodeType";

		private const string ISFIXED_PARM = "@IsFixed";

		public SalarySheet(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalarySheetText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("SalarySheet", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("SheetName", "@SheetName"), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Month", "@Month"), new FieldValue("Year", "@Year"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("SalarySheet", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalarySheetCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalarySheetText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalarySheetText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SheetName", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@Year", SqlDbType.SmallInt);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SheetName"].SourceColumn = "SheetName";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
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

		private string GetInsertUpdateSalarySheetDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("SalarySheet_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("WorkDays", "@WorkDays"), new FieldValue("Absent", "@Absent"), new FieldValue("AnnualLeaves", "@AnnualLeaves"), new FieldValue("UnpaidAnnualLeaves", "@UnpaidAnnualLeaves"), new FieldValue("NormalLeaves", "@NormalLeaves"), new FieldValue("SickLeaves", "@SickLeaves"), new FieldValue("Basic", "@Basic"), new FieldValue("Allowance", "@Allowance"), new FieldValue("Deduction", "@Deduction"), new FieldValue("LoanDeduction", "@LoanDeduction"), new FieldValue("OTHours", "@OTHours"), new FieldValue("OTRate", "@OTRate"), new FieldValue("OTAmount", "@OTAmount"), new FieldValue("OTBase", "@OTBase"), new FieldValue("OTFixedAmount", "@OTFixedAmount"), new FieldValue("OTIsFixed", "@OTIsFixed"), new FieldValue("Remarks", "@Remarks"), new FieldValue("NetSalary", "@NetSalary"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalarySheetDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalarySheetDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalarySheetDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@WorkDays", SqlDbType.Decimal);
			parameters.Add("@Absent", SqlDbType.Decimal);
			parameters.Add("@AnnualLeaves", SqlDbType.Decimal);
			parameters.Add("@SickLeaves", SqlDbType.Decimal);
			parameters.Add("@UnpaidAnnualLeaves", SqlDbType.Decimal);
			parameters.Add("@NormalLeaves", SqlDbType.Decimal);
			parameters.Add("@Basic", SqlDbType.Money);
			parameters.Add("@Allowance", SqlDbType.Money);
			parameters.Add("@Deduction", SqlDbType.Money);
			parameters.Add("@LoanDeduction", SqlDbType.Money);
			parameters.Add("@OTHours", SqlDbType.Decimal);
			parameters.Add("@OTRate", SqlDbType.Money);
			parameters.Add("@OTAmount", SqlDbType.Money);
			parameters.Add("@OTBase", SqlDbType.Money);
			parameters.Add("@NetSalary", SqlDbType.Money);
			parameters.Add("@OTFactor", SqlDbType.Money);
			parameters.Add("@OTFixedAmount", SqlDbType.Money);
			parameters.Add("@OTIsFixed", SqlDbType.Bit);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@WorkDays"].SourceColumn = "WorkDays";
			parameters["@Absent"].SourceColumn = "Absent";
			parameters["@AnnualLeaves"].SourceColumn = "AnnualLeaves";
			parameters["@UnpaidAnnualLeaves"].SourceColumn = "UnpaidAnnualLeaves";
			parameters["@NormalLeaves"].SourceColumn = "NormalLeaves";
			parameters["@SickLeaves"].SourceColumn = "SickLeaves";
			parameters["@Basic"].SourceColumn = "Basic";
			parameters["@Allowance"].SourceColumn = "Allowance";
			parameters["@Deduction"].SourceColumn = "Deduction";
			parameters["@LoanDeduction"].SourceColumn = "LoanDeduction";
			parameters["@OTHours"].SourceColumn = "OTHours";
			parameters["@OTRate"].SourceColumn = "OTRate";
			parameters["@OTAmount"].SourceColumn = "OTAmount";
			parameters["@OTBase"].SourceColumn = "OTBase";
			parameters["@OTFactor"].SourceColumn = "OTFactor";
			parameters["@NetSalary"].SourceColumn = "NetSalary";
			parameters["@OTIsFixed"].SourceColumn = "OTIsFixed";
			parameters["@OTFixedAmount"].SourceColumn = "OTFixedAmount";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateSalarySheetDetailItemsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("SalarySheet_Detail_Item", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("Description", "@Description"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PayType", "@PayType"), new FieldValue("PayrollItemID", "@PayrollItemID"), new FieldValue("LoanSysDocID", "@LoanSysDocID"), new FieldValue("Amount", "@Amount"), new FieldValue("InDeduction", "@InDeduction"), new FieldValue("IsFixed", "@IsFixed"), new FieldValue("PayCodeType", "@PayCodeType"), new FieldValue("PayableAmount", "@PayableAmount"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalarySheetDetailItemsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalarySheetDetailItemsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalarySheetDetailItemsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@LoanSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters.Add("@PayType", SqlDbType.TinyInt);
			parameters.Add("@PayrollItemID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@PayableAmount", SqlDbType.Money);
			parameters.Add("@InDeduction", SqlDbType.Bit);
			parameters.Add("@IsFixed", SqlDbType.Bit);
			parameters.Add("@PayCodeType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@LoanSysDocID"].SourceColumn = "LoanSysDocID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PayType"].SourceColumn = "PayType";
			parameters["@PayrollItemID"].SourceColumn = "PayrollItemID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PayableAmount"].SourceColumn = "PayableAmount";
			parameters["@InDeduction"].SourceColumn = "InDeduction";
			parameters["@IsFixed"].SourceColumn = "IsFixed";
			parameters["@PayCodeType"].SourceColumn = "PayCodeType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalarySheetData journalData)
		{
			return true;
		}

		public bool InsertUpdateSalarySheet(SalarySheetData salarySheetData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalarySheetCommand = GetInsertUpdateSalarySheetCommand(isUpdate);
			try
			{
				DataRow dataRow = salarySheetData.SalarySheetTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate)
				{
					if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("SalarySheet", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
				}
				else
				{
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("SalarySheet", "IsPosted", "SysDocID", text2, "VoucherID", text, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
					{
						throw new CompanyException("This salary sheet is already posted and cannot be edited.");
					}
					fieldValue = new Databases(base.DBConfig).GetFieldValue("SalarySheet", "IsVoid", "SysDocID", text2, "VoucherID", text, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
					{
						throw new CompanyException("This salary sheet is already voided and cannot be edited.");
					}
				}
				insertUpdateSalarySheetCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salarySheetData, "SalarySheet", insertUpdateSalarySheetCommand)) : (flag & Insert(salarySheetData, "SalarySheet", insertUpdateSalarySheetCommand)));
				insertUpdateSalarySheetCommand = GetInsertUpdateSalarySheetDetailsCommand(isUpdate: false);
				insertUpdateSalarySheetCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteSalarySheetDetailsRows(text2, text, sqlTransaction);
				}
				if (salarySheetData.Tables["SalarySheet_Detail"].Rows.Count > 0)
				{
					flag &= Insert(salarySheetData, "SalarySheet_Detail", insertUpdateSalarySheetCommand);
				}
				insertUpdateSalarySheetCommand = GetInsertUpdateSalarySheetDetailItemsCommand(isUpdate: false);
				insertUpdateSalarySheetCommand.Transaction = sqlTransaction;
				if (salarySheetData.Tables["SalarySheet_Detail_Item"].Rows.Count > 0)
				{
					flag &= Insert(salarySheetData, "SalarySheet_Detail_Item", insertUpdateSalarySheetCommand);
				}
				EmployeeLoanData employeeLoanData = new EmployeeLoanData();
				DataRow[] array = salarySheetData.SalarySheetDetailItemsTable.Select("PayType=3");
				foreach (DataRow dataRow2 in array)
				{
					DataRow dataRow3 = employeeLoanData.EmployeeLoanDetailTable.NewRow();
					dataRow3["LoanSysDocID"] = dataRow2["LoanSysDocID"];
					dataRow3["LoanVoucherID"] = dataRow2["PayrollItemID"];
					dataRow3["PaymentSysDocID"] = text2;
					dataRow3["PaymentVoucherID"] = text;
					dataRow3["TransactionDate"] = dataRow["TransactionDate"];
					dataRow3["EmployeeID"] = dataRow2["EmployeeID"];
					dataRow3["Description"] = dataRow2["Description"];
					dataRow3["Credit"] = Math.Abs(decimal.Parse(dataRow2["Amount"].ToString()));
					dataRow3["Reference"] = dataRow["Reference"];
					employeeLoanData.EmployeeLoanDetailTable.Rows.Add(dataRow3);
				}
				flag &= new EmployeeLoan(base.DBConfig).DeleteLoanDetailsRows(sqlTransaction, text2, text, isPaymentRow: true);
				flag &= new EmployeeLoan(base.DBConfig).InsertEmployeeLoanDetail(employeeLoanData, sqlTransaction);
				GLData journalData = CreateGLData(salarySheetData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("SalarySheet", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Salary Sheet";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "SalarySheet_Detail", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalarySheet, text2, text, "SalarySheet", sqlTransaction);
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

		public SalarySheetData GetSalarySheetByID(string sysDocID, string voucherID)
		{
			try
			{
				SalarySheetData salarySheetData = new SalarySheetData();
				string textCommand = "SELECT SS.*, CASE Month WHEN 1 THEN 'JAN' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 THEN 'APR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AUG' WHEN 9 THEN 'SEP'  WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN '12' THEN 'DEC' END AS MonthName FROM SalarySheet SS WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(salarySheetData, "SalarySheet", textCommand);
				if (salarySheetData == null || salarySheetData.Tables.Count == 0 || salarySheetData.Tables["SalarySheet"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT SSD.EmployeeID,FirstName + ' ' +MiddleName+ ' ' + LastName AS EmployeeName ,CONVERT(VARCHAR(10),JoiningDate,111) AS JoiningDate,CONVERT(VARCHAR(10),ResumedDate,111) AS ResumedDate,EMP.Gender,Emp.SponsorID,EMP.ContractType,EMP.GroupID,EMP.PositionID,P.PositionName,Emp.DivisionID,Emp.NationalityID\r\n                    ,CASE WHEN EMP.PaymentMethodID=3 THEN 'Bank Transfer' WHEN EMP.PaymentMethodID=2 THEN 'Cheque' WHEN EMP.PaymentMethodID=1 THEN 'Cash' END AS TransferType ,WorkDays,Absent,WorkDays-Absent-AnnualLeaves AS NetDays,S.SponsorName,S.SponsorID,\r\n                        (SELECT SUM(Amount) FROM Employee_PayrollItem_Detail WHERE PayrollItemID='BASIC' AND SSD.EmployeeID=EmployeeID GROUP BY EmployeeID ) AS [Actual Basic],\r\n                        (SELECT SUM(Amount) FROM Employee_PayrollItem_Detail WHERE PayrollItemID<>'BASIC' AND PayType=1  AND SSD.EmployeeID=EmployeeID GROUP BY EmployeeID) AS [Actual Allowanace],\r\n                        (SELECT SUM(Amount) FROM Employee_PayrollItem_Detail WHERE PayType=1  AND SSD.EmployeeID=EmployeeID GROUP BY EmployeeID) AS [Actual Salary],\r\n                        (SELECT ISNULL(SUM(SSD3.NetSalary), 0) FROM SalarySheet_Detail SSD3 INNER JOIN SalarySheet SS3 ON SS3.SysdocID = SSD3.SysDocID AND SS3.VoucherID = SSD3.VoucherID    \r\n                         WHERE   SS3.Month = MONTH(DATEADD(mm, -1, SS.StartDate)) AND SS3.Year= YEAR(DATEADD(mm, -1, SS.StartDate))  AND SSD3.EmployeeID = EMP.EmployeeID) AS LastSalary,\r\n                        Basic ,Allowance,Deduction,(-1*LoanDeduction) as LoanDeduction,Basic + Allowance + OTAmount  AS GrossSalary,OTHours AS OTHours,OTRate,OTFactor AS Factor, OTFixedAmount AS FixedAmount,OTIsFixed AS IsFixed,OTAmount,SSD.RowIndex AS SlNo,SSD.AnnualLeaves,\r\n                        (SELECT ISNULL(SUM(Amount), 0) FROM Employee_PayrollItem_Detail EPD2 INNER JOIN PayrollItem PItem2 ON Pitem2.PayrollItemID = EPD2.PayrollItemID \r\n                        WHERE ISNULL(InOvertime, 'False') = 'True' AND EPD2.EmployeeID = EMP.EmployeeID) AS OTBase, NetSalary, IBAN, BankID, LabourID,\r\n                        (SELECT RoutingCode FROM Bank WHERE Bank.BankID =  EMP.BankID) AS BankRouteCode,\r\n                        (SELECT ISNULL(SUM(Amount), 0) FROM SalarySheet_Detail_Item EPD2 INNER JOIN PayrollItem PItem2 ON Pitem2.PayrollItemID = EPD2.PayrollItemID\r\n                        WHERE ISNULL(PItem2.InFixed, 'False') = 'False' AND EPD2.EmployeeID = EMP.EmployeeID AND EPD2.SysDocID= '" + sysDocID + "' AND EPD2.VoucherID ='" + voucherID + "' AND PItem2.PayCodeType=2 ) AS VariableAllowance               \r\n                        ,SSD.Remarks FROM SalarySheet_Detail SSD \r\n                        INNER JOIN SalarySheet SS ON SS.SysdocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID\r\n                        INNER JOIN Employee EMP ON SSD.EmployeeID=EMP.EmployeeID LEFT JOIN Sponsor S ON EMP.SponsorID=S.SponsorID\r\n                        LEFT OUTER JOIN Employee_Type ET ON ET.TypeID=EMP.ContractType\r\n\t\t\t\t\t\tLEFT OUTER JOIN Employee_Group EG ON EG.GroupID=EMP.GroupID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Position P ON P.PositionID=EMP.PositionID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Division D ON D.DivisionID=EMP.DivisionID\r\n                        WHERE SS.SysDocID='" + sysDocID + "' AND SS.VoucherID = '" + voucherID + "' order by  CAST(RowIndex AS INT)";
				FillDataSet(salarySheetData, "SalarySheet_Detail", textCommand);
				textCommand = "SELECT SSDI.*,NULL AS StartDate, NULL AS EndDate,'' AS EmployeeName,0 as LeaveDays\r\n                        FROM SalarySheet_Detail_Item SSDI \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'  order by  CAST(RowIndex AS INT)";
				FillDataSet(salarySheetData, "SalarySheet_Detail_Item", textCommand);
				salarySheetData.Relations.Add("REL", salarySheetData.Tables["SalarySheet_Detail"].Columns["EmployeeID"], salarySheetData.Tables["SalarySheet_Detail_Item"].Columns["EmployeeID"]);
				return salarySheetData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalarySheetDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM SalarySheet_Detail_Item WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM SalarySheet_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSalarySheet(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				string text = "";
				bool flag2 = false;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!isVoid)
				{
					throw new Exception("A voided salary sheet cannot be unvoided.");
				}
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("SalarySheet", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
				{
					throw new CompanyException("This salary sheet is already voided.");
				}
				bool flag3 = false;
				fieldValue = new Databases(base.DBConfig).GetFieldValue("SalarySheet", "IsPosted", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag3 = bool.Parse(fieldValue.ToString());
					text = "SELECT COUNT(VoucherID) FROM Payroll_Transaction_Detail PTD WHERE SheetSysDocID='" + sysDocID + "' AND SheetVoucherID = '" + voucherID + "'";
					fieldValue = ExecuteScalar(text, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						flag2 = (int.Parse(fieldValue.ToString()) > 0);
					}
					if (flag2)
					{
						throw new CompanyException("This salary sheet cannot be voided because it is already used in a payroll transaction.");
					}
				}
				if (flag3)
				{
					flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				}
				text = "UPDATE Salary_Sheet SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Salary Sheet", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteSalarySheet(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				bool flag2 = false;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("SalarySheet", "IsPosted", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					text = "SELECT COUNT(VoucherID) FROM Payroll_Transaction_Detail PTD WHERE SheetSysDocID='" + sysDocID + "' AND SheetVoucherID = '" + voucherID + "'";
					fieldValue = ExecuteScalar(text, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						flag2 = (int.Parse(fieldValue.ToString()) > 0);
					}
					if (flag2)
					{
						throw new CompanyException("This salary sheet cannot be deleted because it is already used in a payroll transaction.");
					}
				}
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteSalarySheetDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM SalarySheet WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (flag)
				{
					flag &= new EmployeeLoan(base.DBConfig).DeleteLoanDetailsRows(sqlTransaction, sysDocID, voucherID, isPaymentRow: true);
				}
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Salary Sheet", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet CalculateSalarySheet(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth, string EmployeeIDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
				string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59));
				DateTime dateTime = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(-1);
				string text3 = "";
				string empty = string.Empty;
				text3 = "SELECT CONVERT(VARCHAR(10),EMP.JoiningDate,111) AS JoiningDate,CONVERT(VARCHAR(10),EMP.ResumedDate,111) as ResumedDate,EMP.Gender,Emp.SponsorID,EMP.ContractType,EMP.GroupID,EMP.PositionID,P.PositionName,Emp.DivisionID,Emp.LabourID,EMP.IBAN,EMP.BankID,Emp.NationalityID\r\n                        ,CASE WHEN EMP.PaymentMethodID=3 THEN 'Bank Transfer' WHEN EMP.PaymentMethodID=2 THEN 'Cheque' WHEN EMP.PaymentMethodID=1 THEN 'Cash' END AS TransferType,                                \r\n                        EPD.EmployeeID,FirstName + ' ' + MiddleName+ ' ' + LastName AS EmployeeName ,\r\n                                CASE WHEN EMP.JoiningDate> '" + text + "' THEN DateDiff(Day,EMP.JoiningDate,'" + text2 + "') + 1  ELSE DateDiff(Day,'" + text + "','" + text2 + "') + 1 END AS WorkDays,\r\n                                CASE WHEN EMP.JoiningDate> '" + text + "' THEN DateDiff(Day,EMP.JoiningDate,'" + text2 + "') + 1  ELSE DateDiff(Day,'" + text + "','" + text2 + "') + 1 END  AS NetDays,\r\n                                SUM(CASE WHEN PayrollItemType =1 AND PayCodeType = 1 THEN EPD.Amount ELSE 0 END) AS Basic, SUM(CASE WHEN PayrollItemType =1 AND PayCodeType = 2 THEN EPD.Amount ELSE 0 END) + ISNULL(SADD.Addition, 0) AS Allowance,\r\n                                SUM(CASE WHEN PayrollItemType =2 THEN -1 * EPD.Amount ELSE 0 END) + ISNULL(SSDD.Subtraction, 0) AS Deduction, SUM(CASE WHEN PayType = 1 THEN EPD.Amount ELSE 0 END) AS GrossSalary,\r\n                                ISNULL(OTHours, 0) OTHours,  ISNULL(OTAmount, 0) OTAmount, ISNULL(SADD.Addition, 0) Addition, ISNULL(SSDD.Subtraction, 0) Subtraction, \r\n\t\t\t\t\t\t\t\t\t(SUM(CASE WHEN PayType = 1 THEN EPD.Amount ELSE -1 * EPD.Amount END) + ISNULL(SSDD.Subtraction, 0)) + ISNULL(OTAmount, 0) + ISNULL(SADD.Addition, 0) AS NetSalary,\r\n                                (SELECT ISNULL(SUM(SSD3.NetSalary), 0) FROM SalarySheet_Detail SSD3 INNER JOIN SalarySheet SS3 ON SS3.SysdocID = SSD3.SysDocID AND SS3.VoucherID = SSD3.VoucherID    \r\n                                WHERE   SS3.Month = " + dateTime.Month + " AND SS3.Year= " + dateTime.Year + "  AND SSD3.EmployeeID = EPD.EmployeeID) AS LastSalary,\r\n                                (SELECT ISNULL(SUM(Amount), 0) FROM Employee_PayrollItem_Detail EPD2 INNER JOIN PayrollItem PItem2 ON Pitem2.PayrollItemID = EPD2.PayrollItemID \r\n\t\t\t\t\t\t\t\t\tWHERE ISNULL(InOvertime, 'False') = 'True' AND EPD2.EmployeeID = EPD.EmployeeID) AS OTBase,ISNULL(TEMP.Absent,0)  AS Absent,ISNULL(TEMP2.AnnualLeaves,0)  AS AnnualLeaves,ISNULL(TEMP3.UnpaidAnnualLeaves,0)  AS UnPaidAnnualLeaves,ISNULL(TEMP4.SickLeaves,0)  AS SickLeaves,ISNULL(TEMP5.NormalLeaves,0)  AS NormalLeaves,'' AS SlNo,\r\n                                    (select count(DISTINCT HD1.FromDate) * LT2.DeductionProportion from Holiday_Calendar_Detail HD1   \r\n                                     LEFT JOIN EMPLOYEE E2 ON HD1.CalendarID=EMP.CalendarID \r\n                                     LEFT JOIN  Employee_Activity EA2 ON EA2.EmployeeID = E2.EmployeeID\r\n                                     LEFT JOIN Employee_Leave_Request ELR2 ON EA2.ActivityID = ELR2.ActivityID\r\n                                     INNER JOIN Leave_Type LT2 ON LT2.LeaveTypeID=ELR2.LeaveTypeID AND LT2.IsAnnual<>1  AND LT2.ActivateHC=1 \r\n                                     and (HD1.FromDate BETWEEN ELR2.StartDate AND ELR2.EndDate\r\n                                     )WHERE  ISNULL(isvoid,0) =0 and ISNULL(IsApproved,0)=1 AND LT2.DeductionProportion<>0 and E2.EmployeeID=EMP.EmployeeID AND HD1.FromDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                                      GROUP BY LT2.DeductionProportion, E2.EmployeeID)AS ToLessTakenAbs,\r\n                                    (select count(DISTINCT HD1.FromDate) * LT2.DeductionProportion from Holiday_Calendar_Detail HD1   \r\n                                     LEFT JOIN EMPLOYEE E2 ON HD1.CalendarID=EMP.CalendarID \r\n                                     LEFT JOIN  Employee_Activity EA2 ON EA2.EmployeeID = E2.EmployeeID\r\n                                     LEFT JOIN Employee_Leave_Request ELR2 ON EA2.ActivityID = ELR2.ActivityID\r\n                                     INNER JOIN Leave_Type LT2 ON LT2.LeaveTypeID=ELR2.LeaveTypeID AND LT2.IsAnnual=1  AND LT2.ActivateHC=1 \r\n                                     and (HD1.FromDate BETWEEN ELR2.StartDate AND ELR2.EndDate\r\n                                     )WHERE  ISNULL(isvoid,0) =0 and ISNULL(IsApproved,0)=1 AND LT2.DeductionProportion<>0 and E2.EmployeeID=EMP.EmployeeID AND HD1.FromDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                                      GROUP BY LT2.DeductionProportion, E2.EmployeeID)AS ToLessTakenAnn,0.0 AS LoanDeduction                                                  \r\n\r\n                              FROM Employee_PayrollItem_Detail EPD\r\n                                INNER JOIN Employee EMP ON EMP.EmployeeID=EPD.EmployeeID \r\n\t\t\t\t\t\t\t\tINNER JOIN PayrollItem PItem ON Pitem.PayrollItemID = EPD.PayrollItemID\r\n                                LEFT OUTER JOIN Employee_Type ET ON ET.TypeID=EMP.ContractType\r\n\t\t\t\t\t\t        LEFT OUTER JOIN Employee_Group EG ON EG.GroupID=EMP.GroupID\r\n\t\t\t\t\t\t        LEFT OUTER JOIN Position P ON P.PositionID=EMP.PositionID\r\n\t\t\t\t\t\t        LEFT OUTER JOIN Division D ON D.DivisionID=EMP.DivisionID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(OTHours) OTHours, SUM(OTHours * OTRate) AS OTAmount FROM OverTimeEntry TB1 INNER JOIN OverTimeEntry_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE Month = " + periodMonth + " AND Year = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS EOT ON EOT.EmployeeID = EMP.EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t  \r\n                                LEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(Amount) Addition FROM Salary_Addition TB1 INNER JOIN Salary_Addition_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE DATEPART(Month, TB1.TransactionDate) = " + periodMonth + " AND DATEPART(Year, TB1.TransactionDate) = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS SADD ON SADD.EmployeeID = EMP.EmployeeID\r\n\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(Amount) Subtraction FROM Salary_Deduction TB1 INNER JOIN Salary_Deduction_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE DATEPART(Month, TB1.TransactionDate) = " + periodMonth + " AND DATEPART(Year, TB1.TransactionDate) = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS SSDD ON SSDD.EmployeeID = EMP.EmployeeID\r\n                                LEFT OUTER JOIN\r\n                                        (\r\n                                          -- SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                          -- CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                          -- CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS ABSENT \r\n                                          -- FROM Employee_Leave_Request ELR\r\n                                          -- LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                          -- LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual<>1\r\n                                          -- WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND\r\n                                          -- ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP ON TEMP.EmployeeID=EMP.EmployeeID\r\n                                          -- Issue Duplicate data in rows \r\n\r\n\r\n                                           SELECT EmployeeID ,SUM(Absent_Detail.ABSENT) [ABSENT]  FROM(SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                           CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                           CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS ABSENT \r\n                                           FROM Employee_Leave_Request ELR\r\n                                           LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                           LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual<>1\r\n                                           WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND\r\n                                           ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion )AS Absent_Detail GROUP BY EmployeeID ) AS TEMP ON TEMP.EmployeeID=EMP.EmployeeID\r\n\r\n\r\n                                LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS AnnualLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual=1\r\n                                WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND  ISNULL(LT.IsPayable,'False')='True' AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP2 ON TEMP2.EmployeeID=EMP.EmployeeID\t\r\n\r\n\t\t\t\t\t\t\t LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS UnpaidAnnualLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual=1\r\n                                WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND ISNULL(LT.IsPayable,'False')='False' AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP3 ON TEMP3.EmployeeID=EMP.EmployeeID\r\n\r\n\r\n                                LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS SickLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and  LT.IsAnnual<>1 AND  ISNULL(LT.IsPayable,'False')='True' \r\n                                WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False'  AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP4 ON TEMP4.EmployeeID=EMP.EmployeeID\r\n\r\n                                LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)) AS NormalLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and  LT.IsAnnual<>1 AND  ISNULL(LT.IsPayable,'False')='False' \r\n                                WHERE LT.DeductionProportion=0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False'  AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP5 ON TEMP5.EmployeeID=EMP.EmployeeID\r\n\r\n\r\n                                WHERE    (\r\n\t\t\t\t\t\t\t\t\t\t\tEPD.EmployeeID NOT IN (SELECT DISTINCT EMPLOYEEID FROM SalarySheet_Detail SSD INNER JOIN SalarySheet SS ON SS.SysdocID = SSD.SysDocID AND SS.VoucherID= SSD.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t    AND SS.Month =" + periodMonth + " AND Year = " + periodYear + ")\r\n\t\t\t\t\t\t\t\t\t\t  ) \r\n\t\t\t\t\t\t\t\tAND\r\n\r\n                                EPD.EmployeeID IN (\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID FROM Employee E2 WHERE \r\n                                                    E2.JoiningDate <= '" + text2 + "' AND\r\n                                                    ISNULL(IsTerminated,'False')='False' AND ISNULL(IsCancelled,'False')='False' AND ISNULL(IsEOSSettled,'False')='False' AND \r\n\t\t\t\t\t\t\t\t\t\t\t\t\t(ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')\r\n                                                    AND EPD.EmployeeID  NOT IN(SELECT E.EmployeeID FROM Employee_Leave_Request ELR LEFT JOIN Employee_Activity EA ON ELR.ActivityID=EA.ActivityID \r\n                                                    LEFT JOIN Employee E ON E.EmployeeID= EA.EmployeeID WHERE ( ELR.ResumptionDate IS  NULL AND MONTH(ELR.EndDate)<" + periodMonth + " AND DATEPART(Year, ELR.EndDate) = " + periodYear + ") AND ELR.IsApproved<>'False' )\r\n                                                    AND EPD.EmployeeID  NOT IN(SELECT E.EmployeeID FROM Employee_Leave_Request ELR LEFT JOIN Employee_Activity EA ON ELR.ActivityID=EA.ActivityID \r\n                                                    LEFT JOIN Employee E ON E.EmployeeID= EA.EmployeeID WHERE  ELR.IsApproved<>'False' AND ELR.StartDate <= '" + text + "' AND ELR.EndDate >='" + text2 + "'))";
				if (EmployeeIDs != "")
				{
					text3 = text3 + " AND E2.EmployeeID IN(" + EmployeeIDs + ") ";
				}
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text3 = text3 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text3 += " )\tGROUP BY EPD.EmployeeID,FirstName,MiddleName, LastName,  OTAmount, OTHours, Addition, Subtraction,Absent\r\n\t                ,JoiningDate,ResumedDate,EMP.CalendarID,EMP.EmployeeID ,EMP.Gender,Emp.SponsorID,EMP.ContractType,EMP.GroupID,EMP.PositionID,P.PositionName,Emp.DivisionID,Emp.LabourID,EMP.IBAN,EMP.BankID,EMP.NationalityID,EMP.PaymentMethodID,TEMP2.AnnualLeaves,TEMP3.UnpaidAnnualLeaves ,TEMP4.SickLeaves,  TEMP5.NormalLeaves ";
				FillDataSet(dataSet, "SalarySheet_Detail", text3);
				if (dataSet.Tables["SalarySheet_Detail"].Rows.Count == 0)
				{
					return dataSet;
				}
				empty = string.Empty;
				for (int i = 0; i < dataSet.Tables["SalarySheet_Detail"].Rows.Count; i++)
				{
					empty = empty + "'" + dataSet.Tables["SalarySheet_Detail"].Rows[i]["EmployeeID"] + "'";
					if (i < dataSet.Tables["SalarySheet_Detail"].Rows.Count - 1)
					{
						empty += ",";
					}
					DataRow dataRow = dataSet.Tables["SalarySheet_Detail"].Rows[i];
					if (dataRow["SlNo"].ToString() == "")
					{
						dataRow["SlNo"] = i;
					}
				}
				text3 = "SELECT EPD.EmployeeID,PayType,EPD.PayrollItemID,NULL AS LoanSysDocID,PItem.PayrollItemName AS Description,StartDate,EndDate, InDeduction,PayCodeType AS PayCodeType,\r\n                                FirstName + ' ' + LastName AS EmployeeName ,CASE WHEN PayrollItemType = 1 THEN EPD.Amount ELSE -1* EPD.Amount END AS Amount, CASE WHEN PayrollItemType = 1 THEN EPD.Amount ELSE -1* EPD.Amount END AS PayableAmount,0 AS 'LeaveDays' FROM Employee_PayrollItem_Detail EPD\r\n                                INNER JOIN PayrollItem PItem ON Pitem.PayrollItemID = EPD.PayrollItemID\r\n                                INNER JOIN Employee ON Employee.EmployeeID=EPD.EmployeeID WHERE EPD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False'  AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text3 = text3 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text3 += ") ";
				text3 = text3 + " UNION \r\n\r\n                                SELECT OTE.EmployeeID,1 AS PayType,OTE.OTType,NULL AS LoanSysDocID,'Over Time' AS Description,OTE.PayrollPeriod,OTE.PayrollPeriod, NULL,'6',\r\n                                FirstName + ' ' + LastName AS EmployeeName , SUM(OTE.OTHours * OTE.OTRate) AS Amount, SUM(OTE.OTHours * OTE.OTRate) AS PayableAmount ,LeaveDays\r\n\t\t\t\t\t\t\t\tFROM OverTimeEntry_Detail OTE\r\n                                INNER JOIN OverTimeEntry OT ON OTE.VoucherID = OT.VoucherID \r\n\t\t\t\t\t\t\t\r\n                                INNER JOIN Employee ON Employee.EmployeeID=OTE.EmployeeID WHERE OT.ApprovalDate IS NOT NULL\r\n                                AND Month =" + periodMonth + " AND Year = " + periodYear;
				text3 = text3 + " AND OTE.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False' AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text3 = text3 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text3 += ")";
				text3 += " GROUP BY OTE.EmployeeID, FirstName, LastName,  OTE.PayrollPeriod,OTE.OTType,LeaveDays";
				text3 = text3 + " UNION \r\n\r\n                        SELECT SAD.EmployeeID, 1 AS PayType, AdditionCode,NULL AS LoanSysDocID,CASE WHEN LTRIM(RTRIM(SAD.Remarks)) = '' THEN  Pitem.BenefitName  ELSE SAD.remarks END AS Description,PayrollPeriod,PayrollPeriod, NULL,'7',\r\n                                FirstName + ' ' + LastName AS EmployeeName , SUM(Amount) AS Amount, SUM(Amount) AS PayableAmount ,0 AS 'LeaveDays'\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\tFROM Salary_Addition_Detail SAD\r\n                                INNER JOIN Salary_Addition SA ON SAD.VoucherID = SA.VoucherID \r\n                                INNER JOIN Employee ON Employee.EmployeeID=SAD.EmployeeID \r\n                                INNER JOIN  Benefit PItem ON Pitem.BenefitID = SAD.AdditionCode \r\n                                WHERE SA.ApprovalDate IS NOT NULL\r\n                                AND DATEPART(Month, PayrollPeriod) = " + periodMonth + "  AND DATEPART(Year, PayrollPeriod) =  " + periodYear;
				text3 = text3 + " AND ApprovalDate IS NOT NULL  AND SAD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False'  AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text3 = text3 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text3 += ")";
				text3 += " GROUP BY SAD.EmployeeID, FirstName, LastName, PayrollPeriod, AdditionCode,Pitem.BenefitName, Remarks ";
				text3 = text3 + " UNION \r\n\r\n                       SELECT SDD.EmployeeID, 2 AS PayType, DeductionCode,NULL AS LoanSysDocID, CASE WHEN LTRIM(RTRIM(sdd.Remarks)) = '' THEN  Pitem.PayrollItemName ELSE sdd.remarks END AS Description,PayrollPeriod,PayrollPeriod, 1,'6',\r\n                                FirstName + ' ' + LastName AS EmployeeName , (SUM(Amount) * -1) AS Amount, (SUM(Amount) * -1) AS PayableAmount ,0 AS 'LeaveDays'\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\tFROM Salary_Deduction_Detail SDD\r\n                                INNER JOIN Salary_Deduction SD ON SDD.VoucherID = SD.VoucherID\r\n                                INNER JOIN Employee ON Employee.EmployeeID=SDD.EmployeeID\r\n                                INNER JOIN  PayrollItem PItem ON Pitem.PayrollItemID = SDD.DeductionCode\r\n                                WHERE SD.ApprovalDate IS NOT NULL\r\n                                AND DATEPART(Month, PayrollPeriod) = " + periodMonth + "  AND DATEPART(Year, PayrollPeriod) =  " + periodYear;
				text3 = text3 + " AND ApprovalDate IS NOT NULL AND SDD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False'  AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text3 = text3 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text3 += ")";
				text3 += " GROUP BY SDD.EmployeeID, FirstName, LastName, PayrollPeriod, DeductionCode,Pitem.PayrollItemName, Remarks";
				text3 = text3 + " UNION \r\n\r\n                        SELECT EL.EmployeeID, 3 AS PayType, VoucherID AS PayrollItemID,SysDocID AS LoanSysDocID,'Loan Recovery' AS Description, DedStartDate,NULL AS EndDate, 1 AS InDeduction,NULL AS PayCodeType,\r\n                        FirstName + ' ' + LastName AS EmployeeName,\r\n                        CASE WHEN ISNULL(IsVoid,'False')='True' THEN 0\r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) >= Amount THEN 0 \r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) + InstallmentAmount > Amount THEN -1 * ( Amount-ISNULL(PaidAmount,0) - ISNULL(DiscountAmount ,0) )\r\n                        ELSE -1 * ISNULL(InstallmentAmount,0) END AS [Amount], 0 AS PayableAmount,0 AS 'LeaveDays'\r\n                        FROM Employee_Loan EL LEFT OUTER JOIN Employee ON EL.EmployeeID=Employee.EmployeeID WHERE \r\n                        '" + text2 + "' >= EL.DedStartDate-1  AND ISNULL(IsVoid,'False')='False' \r\n                        AND ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) < Amount ";
				text3 = text3 + " AND EL.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE  \r\n                                ISNULL(IsTerminated,'False')='False'  AND \r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text3 = text3 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text3 = text3 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text3 = text3 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text3 = text3 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text3 = text3 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text3 = text3 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text3 = text3 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text3 = text3 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text3 = text3 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text3 = text3 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text3 = text3 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text3 = text3 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text3 = text3 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text3 = text3 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text3 += ")";
				text3 += " ORDER BY EmployeeID, PayType ";
				FillDataSet(dataSet, "SalarySheet_Detail_Item", text3);
				dataSet.Relations.Add("REL", dataSet.Tables["SalarySheet_Detail"].Columns["EmployeeID"], dataSet.Tables["SalarySheet_Detail_Item"].Columns["EmployeeID"]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalarySheetItems(string sysDocID, string voucherID, string[] employeeIDs)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT *,EPD.Amount AS Rate, 0.0 AS Days,FirstName + ' ' + LastName AS EmployeeName FROM Employee_PayrollItem_Detail EPD\r\n                                INNER JOIN Employee ON Employee.EmployeeID=EPD.EmployeeID WHERE EPD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False' AND ISNULL(OnVacation,'False')='False' AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) ";
				string text = "";
				for (int i = 0; i < employeeIDs.Length; i++)
				{
					text = text + "'" + employeeIDs[i].ToString() + "'";
					if (i + 1 < employeeIDs.Length)
					{
						text += ",";
					}
				}
				str = str + " AND E2.EmployeeID IN (" + text + ") ";
				str += ")";
				FillDataSet(dataSet, "SalarySheet", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUnpostedSalarySheets()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID,VoucherID AS [Sheet Number],SheetName [Description],TransactionDate [Sheet Date],Note \r\n                                FROM Salary_Sheet WHERE ISNULL(IsPosted,'False')='False'";
				FillDataSet(dataSet, "SalarySheet", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenSalarySheets()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SS.SysDocID,SS.VoucherID,SheetName,TransactionDate,CASE month WHEN 1 THEN 'JAN' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 \r\n                                THEN 'APR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AUG' WHEN 9 \r\n                                THEN 'SEP' WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN '12' THEN 'DEC' END AS Month,month,Year,StartDate,EndDate,Note FROM SalarySheet SS\r\n                                    INNER JOIN SalarySheet_Detail SSD ON SS.SysDocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID\r\n                                    WHERE ISNULL(IsClosed,'False') = 'False'\r\n                                    GROUP BY SS.SysDocID,SS.VoucherID,SheetName,TransactionDate,Month,Year,StartDate,EndDate,Note\r\n                                    HAVING SUM(ISNULL(PaidAmount,0)) < SUM(ISNULL(NetSalary,0))";
				FillDataSet(dataSet, "SalarySheet", textCommand);
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
			string text3 = "SELECT SS.SysDocID,SS.VoucherID,SheetName,TransactionDate,Month,Year,StartDate,EndDate,Note FROM SalarySheet SS\r\n                                    INNER JOIN SalarySheet_Detail SSD ON SS.SysDocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			text3 += " GROUP BY SS.SysDocID,SS.VoucherID,SheetName,TransactionDate,Month,Year,StartDate,EndDate,Note";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "SalarySheet", sqlCommand);
			return dataSet;
		}

		public DataSet GetSalarySheetEmployees(string docID, string voucherID, byte paymentMethodID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID,VoucherID,SSD.EmployeeID, EMP.FirstName + ' ' + LastName AS Name,WorkDays-[Absent]-AnnualLeaves AS [WorkDays], NetSalary, NetSalary - ISNULL(PaidAmount,0) AS Balance, RowIndex\r\n                                 FROM SalarySheet_Detail SSD INNER JOIN Employee EMP ON SSD.EmployeeID = EMP.EmployeeID   WHERE ISNULL(PaidAmount,0) < NetSalary AND\r\n                                 SysDocID='" + docID + "' AND VoucherID = '" + voucherID + "' ";
				FillDataSet(dataSet, "SalarySheet_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool PostSalarySheet(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("SalarySheet", "IsPosted", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
				{
					flag = false;
					throw new CompanyException("This salary sheet is already posted.", 1020);
				}
				GetSalarySheetByID(sysDocID, voucherID);
				string exp = "UPDATE Salary_Sheet SET IsPosted='True' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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

		private GLData CreateGLData(SalarySheetData salarySheetData, SqlTransaction sqlTransaction)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				GLData gLData = new GLData();
				DataRow dataRow = salarySheetData.SalarySheetTable.Rows[0];
				string text = "";
				string str = dataRow["SysDocID"].ToString();
				dataRow["VoucherID"].ToString();
				string value = dataRow["DivisionID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.SalarySheet;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = dataRow["SheetName"].ToString();
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				string commaSeperatedIDs = GetCommaSeperatedIDs(salarySheetData, "SalarySheet_Detail", "EmployeeID");
				string textCommand = "SELECT EmployeeID,ISNULL(EMP.AccountID,ET.AccountID) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                WHERE EMP.EmployeeID IN (" + commaSeperatedIDs + ") ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Employee", textCommand, sqlTransaction);
				textCommand = "SELECT  LOC.EmployeeAccountID AS AccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID WHERE SysDocID = '" + str + "'";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Accounts", textCommand, sqlTransaction);
				foreach (DataRow row in salarySheetData.SalarySheetDetailTable.Rows)
				{
					string text2 = row["EmployeeID"].ToString();
					DataRow[] array = dataSet.Tables[0].Select("EmployeeID = '" + text2 + "'");
					if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
					{
						text = array[0]["AccountID"].ToString();
					}
					if (text == "")
					{
						text = dataSet2.Tables[0].Rows[0]["AccountID"].ToString();
					}
					if (dataSet2 == null || dataSet2.Tables.Count == 0 || dataSet2.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("There is no location assigned to this system document or location record is missing.");
					}
					if (text == "")
					{
						throw new CompanyException("Account is not set for the employee '" + text2 + "'.", 1021);
					}
					decimal num = default(decimal);
					string text3 = "";
					string value3 = "";
					decimal num2 = default(decimal);
					Math.Round(decimal.Parse(row["Basic"].ToString()), currencyDecimalPoints);
					Math.Round(decimal.Parse(row["Allowance"].ToString()), currencyDecimalPoints);
					Math.Round(decimal.Parse(row["Deduction"].ToString()), currencyDecimalPoints);
					Math.Round(decimal.Parse(row["LoanDeduction"].ToString()), currencyDecimalPoints);
					Math.Round(decimal.Parse(row["OTAmount"].ToString()), currencyDecimalPoints);
					array = salarySheetData.SalarySheetDetailItemsTable.Select("EmployeeID = '" + text2 + "'");
					string text4 = dataRow["SheetName"].ToString();
					foreach (DataRow dataRow3 in array)
					{
						bool flag = false;
						if (!dataRow3["IsFixed"].IsDBNullOrEmpty())
						{
							flag = bool.Parse(dataRow3["IsFixed"].ToString());
						}
						if (flag)
						{
							num2 += decimal.Parse(dataRow3["PayableAmount"].ToString());
						}
					}
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text;
					dataRow4["PayeeID"] = text2;
					dataRow4["PayeeType"] = "E";
					dataRow4["IsARAP"] = true;
					dataRow4["Credit"] = num2;
					dataRow4["Description"] = text4;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["DivisionID"] = value;
					dataRow4["CompanyID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
					foreach (DataRow dataRow5 in array)
					{
						text3 = dataRow5["PayrollItemID"].ToString();
						num = Math.Round(decimal.Parse(dataRow5["PayableAmount"].ToString()), currencyDecimalPoints);
						string text5 = "";
						byte b = 1;
						b = byte.Parse(dataRow5["PayType"].ToString());
						bool flag2 = false;
						if (!dataRow5["IsFixed"].IsDBNullOrEmpty())
						{
							flag2 = bool.Parse(dataRow5["IsFixed"].ToString());
						}
						object obj2 = null;
						switch (b)
						{
						case 1:
						case 2:
							obj2 = new Databases(base.DBConfig).GetFieldValue("PayrollItem", "AccountID", "PayrollItemID", text3, sqlTransaction);
							if (obj2 == null || obj2.ToString() == "")
							{
								obj2 = new Databases(base.DBConfig).GetFieldValue("Benefit", "AccountID", "BenefitID", text3, sqlTransaction);
							}
							if (obj2 == null || obj2.ToString() == "")
							{
								obj2 = new Databases(base.DBConfig).GetFieldValue("Employee_OverTime", "AccountID", "OverTimeID", text3, sqlTransaction);
							}
							if (obj2 == null || obj2.ToString() == "")
							{
								throw new CompanyException("Account is not set for payroll item '" + text3 + "'.", 1022);
							}
							value3 = obj2.ToString();
							break;
						case 4:
							if (obj2 == null || obj2.ToString() == "")
							{
								throw new CompanyException("Account is not set for payroll item '" + text3 + "'.", 1022);
							}
							value3 = obj2.ToString();
							break;
						case 3:
							text5 = dataRow5["LoanSysDocID"].ToString();
							textCommand = "SELECT AccountID FROM Employee_Loan_Type ELT\r\n\t\t\t\t\t\t                     INNER JOIN  Employee_Loan EL ON ELT.LoanTypeID=EL.LoanType WHERE SysDocID = '" + text5 + "' AND VoucherID = '" + text3 + "' ";
							obj2 = ExecuteScalar(textCommand, sqlTransaction);
							if (obj2 == null || obj2.ToString() == "")
							{
								throw new CompanyException("Account is not set for Loan  '" + text3 + "'.", 1022);
							}
							value3 = obj2.ToString();
							break;
						}
						dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = value3;
						dataRow4["PayeeID"] = text2;
						dataRow4["Description"] = dataRow5["Description"];
						switch (b)
						{
						case 1:
							dataRow4["Debit"] = num;
							dataRow4["Credit"] = DBNull.Value;
							break;
						case 2:
							dataRow4["Debit"] = DBNull.Value;
							dataRow4["Credit"] = Math.Abs(num);
							break;
						case 3:
							dataRow4["Debit"] = DBNull.Value;
							dataRow4["Credit"] = Math.Abs(num);
							break;
						default:
							throw new CompanyException("PayType is not defined.");
						}
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["DivisionID"] = value;
						dataRow4["CompanyID"] = value2;
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
						if (!flag2 && b != 3)
						{
							dataRow4 = gLData.JournalDetailsTable.NewRow();
							dataRow4.BeginEdit();
							dataRow4["JournalID"] = 0;
							dataRow4["AccountID"] = text;
							dataRow4["PayeeID"] = text2;
							dataRow4["PayeeType"] = "E";
							dataRow4["IsARAP"] = true;
							if (b == 1)
							{
								dataRow4["Debit"] = DBNull.Value;
								dataRow4["Credit"] = Math.Abs(num);
							}
							else
							{
								dataRow4["Debit"] = Math.Abs(num);
								dataRow4["Credit"] = DBNull.Value;
							}
							dataRow4["Description"] = text4 + "-" + dataRow5["Description"];
							dataRow4["Reference"] = dataRow["Reference"];
							dataRow4["DivisionID"] = value;
							dataRow4["CompanyID"] = value2;
							dataRow4.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow4);
						}
						else if (b == 3)
						{
							dataRow4 = gLData.JournalDetailsTable.NewRow();
							dataRow4.BeginEdit();
							dataRow4["JournalID"] = 0;
							dataRow4["AccountID"] = text;
							dataRow4["PayeeID"] = text2;
							dataRow4["PayeeType"] = "E";
							dataRow4["IsARAP"] = true;
							dataRow4["Debit"] = Math.Abs(num);
							dataRow4["Description"] = "Loan Recovery-No:" + text3;
							dataRow4["Credit"] = DBNull.Value;
							dataRow4["Reference"] = dataRow["Reference"];
							dataRow4["DivisionID"] = value;
							dataRow4["CompanyID"] = value2;
							dataRow4.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow4);
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

		public DataSet GetSelectedSalaryBankTransfer(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string sysdocid, string vouhcerid)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID,VoucherID,SSD.EmployeeID, E2.FirstName + ' ' + LastName AS Name,WorkDays-[Absent]-AnnualLeaves AS [WorkDays], NetSalary, NetSalary - ISNULL(PaidAmount,0) AS Balance, RowIndex\r\n                           FROM SalarySheet_Detail SSD INNER JOIN Employee E2 ON SSD.EmployeeID = E2.EmployeeID   WHERE ISNULL(PaidAmount,0) < NetSalary AND\r\n                           SSD.SysDocID='" + sysdocid + "' AND SSD.VoucherID='" + vouhcerid + "'";
			if (fromEmployee != "")
			{
				str = str + " AND E2.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND E2.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E2.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E2.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E2.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E2.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E2.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E2.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E2.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E2.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E2.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E2.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E2.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E2.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E2.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E2.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E2.PositionID >='" + fromPosition + "' ";
			}
			if (toPostion != "")
			{
				str = str + " AND E2.PositionID <='" + toPostion + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E2.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E2.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E2.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E2.AccountID <='" + toAccount + "' ";
			}
			str += " ORDER BY  E2.FirstName + ' ' + E2.LastName ASC";
			FillDataSet(dataSet, "SalarySheet_Detail", str);
			return dataSet;
		}

		public DataSet GetSelectedSalaryBankTransfer(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string[] sysdocid, string[] voucherid)
		{
			string text = "";
			for (int i = 0; i < voucherid.Length; i++)
			{
				text = text + "'" + voucherid[i] + "'";
				if (i < voucherid.Length - 1)
				{
					text += ",";
				}
			}
			string text2 = "";
			for (int j = 0; j < sysdocid.Length; j++)
			{
				text2 = text2 + "'" + sysdocid[j] + "'";
				if (j < sysdocid.Length - 1)
				{
					text2 += ",";
				}
			}
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID,VoucherID,SSD.EmployeeID, E2.FirstName + ' ' + LastName AS Name,WorkDays-[Absent]-AnnualLeaves AS [WorkDays], NetSalary, NetSalary - ISNULL(PaidAmount,0) AS Balance, RowIndex\r\n                           FROM SalarySheet_Detail SSD INNER JOIN Employee E2 ON SSD.EmployeeID = E2.EmployeeID   WHERE ISNULL(PaidAmount,0) < NetSalary AND\r\n                           SSD.SysDocID IN (" + text2 + ") AND SSD.VoucherID IN (" + text + ")";
			if (fromEmployee != "")
			{
				str = str + " AND E2.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND E2.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E2.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E2.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E2.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E2.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E2.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E2.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E2.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E2.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E2.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E2.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E2.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E2.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E2.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E2.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E2.PositionID >='" + fromPosition + "' ";
			}
			if (toPostion != "")
			{
				str = str + " AND E2.PositionID <='" + toPostion + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E2.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E2.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E2.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E2.AccountID <='" + toAccount + "' ";
			}
			str += " ORDER BY  E2.FirstName + ' ' + E2.LastName ASC";
			FillDataSet(dataSet, "SalarySheet_Detail", str);
			return dataSet;
		}

		public DataSet GeProjectExpenseSalarySheets(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string sysdocid, string vouhcerid)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT SysDocID,VoucherID,SSD.EmployeeID, E2.FirstName + ' ' + LastName AS Name,WorkDays-Absent AS [WorkDays], NetSalary, NetSalary - ISNULL(PaidAmount,0) AS Balance, RowIndex,OTAMount ,(NetSalary + ISNULL(Deduction,0)) as [Gross Salary],NetSalary as [AllocationSalary]\r\n                           FROM SalarySheet_Detail SSD INNER JOIN Employee E2 ON SSD.EmployeeID = E2.EmployeeID   WHERE ISNULL(PaidAmount,0) < NetSalary AND\r\n                           SSD.SysDocID='" + sysdocid + "' AND SSD.VoucherID='" + vouhcerid + "' AND E2.EmployeeID NOT IN( SELECT DISTINCT EMPLOYEEID FROM Project_Expense_Allocation_Detail PEA WHERE  SheetSysDocID = '" + sysdocid + "' AND SheetVoucherID = '" + vouhcerid + "')";
			if (fromEmployee != "")
			{
				text = text + " AND E2.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E2.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E2.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E2.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E2.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E2.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E2.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E2.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E2.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E2.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E2.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E2.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E2.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E2.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E2.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E2.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E2.PositionID >='" + fromPosition + "' ";
			}
			if (toPostion != "")
			{
				text = text + " AND E2.PositionID <='" + toPostion + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E2.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E2.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E2.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E2.AccountID <='" + toAccount + "' ";
			}
			FillDataSet(dataSet, "SalarySheet_Detail", text);
			text = "  select DISTINCT SDI.*, (SELECT ISNULL((basic+allowance+OTAmount),0) FROM SalarySheet_Detail WHERE SysDocID=SDI.SysDocID AND \r\n                      VoucherID=SDI.VoucherID AND EmployeeID=SDI.employeeid) AS Gross ,\r\n                        (PayableAmount/(SELECT ISNULL((basic+allowance+OTAmount),0) FROM SalarySheet_Detail WHERE SysDocID=SDI.SysDocID AND VoucherID=SDI.VoucherID AND EmployeeID=SDI.employeeid)) AS Proportion\r\n                      from SalarySheet_Detail_Item SDI INNER JOIN SalarySheet_Detail SS ON SDI.SysDocID= SS.SysDocID \r\n                      AND SDI.VoucherID=SS.VoucherID INNER JOIN Employee E2 ON SDI.EmployeeID = E2.EmployeeID WHERE SDI.SysDocID='" + sysdocid + "' AND SDI.VoucherID='" + vouhcerid + "'";
			if (fromEmployee != "")
			{
				text = text + " AND E2.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				text = text + " AND E2.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				text = text + " AND E2.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				text = text + " AND E2.EmployeeID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				text = text + " AND E2.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				text = text + " AND E2.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				text = text + " AND E2.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				text = text + " AND E2.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				text = text + " AND E2.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				text = text + " AND E2.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				text = text + " AND E2.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				text = text + " AND E2.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				text = text + " AND E2.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text = text + " AND E2.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				text = text + " AND E2.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				text = text + " AND E2.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				text = text + " AND E2.PositionID >='" + fromPosition + "' ";
			}
			if (toPostion != "")
			{
				text = text + " AND E2.PositionID <='" + toPostion + "' ";
			}
			if (fromBank != "")
			{
				text = text + " AND E2.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				text = text + " AND E2.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				text = text + " AND E2.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				text = text + " AND E2.AccountID <='" + toAccount + "' ";
			}
			FillDataSet(dataSet, "SalarySheet_Detail_Item", text);
			return dataSet;
		}

		public bool AllowDelete(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM Project_Expense_Allocation_Detail PEA WHERE  SheetSysDocID = '" + sysDocID + "' AND SheetVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public DataSet GetSalaryEmployeeSheetDetails(string Month, string Year, string employeeID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "select NetSalary from SalarySheet_Detail SSD inner join SalarySheet SS on SSD.SysDocID=SS.SysDocID and SSD.VoucherID =SS.VoucherID where 1=1 ";
				text = text + " AND SS.Month ='" + Month + "' and SS.Year ='" + Year + "' and SSD.EmployeeID ='" + employeeID + "'";
				FillDataSet(dataSet, "SalarySheet", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet ReCalculateSalarySheet(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth, string EmployeeIDs, DataSet dsSSD, string strSysDocID, string strVoucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
				string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59));
				DateTime dateTime = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(-1);
				DataView defaultView = dsSSD.Tables["SalarySheet_Detail"].DefaultView;
				defaultView.RowFilter = "EmployeeID <>''";
				string[] source = (from s in defaultView.ToTable().AsEnumerable()
					select s.Field<string>("EmployeeID")).ToArray();
				string text3 = string.Join(",", source.Select((string o) => "'" + o + "'"));
				if (EmployeeIDs == "")
				{
					EmployeeIDs = text3;
				}
				else
				{
					text3 = "";
					EmployeeIDs += text3;
				}
				string text4 = "";
				string empty = string.Empty;
				DataSet dataSet2 = new DataSet();
				text4 = "SELECT DISTINCT SS.VoucherID FROM SalarySheet_Detail SSD INNER JOIN SalarySheet SS ON SS.SysdocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID AND SS.Month = " + periodMonth + " AND SS.Year = " + periodYear + " AND SS.VoucherID\r\n                    <>'" + strVoucherID + "'";
				FillDataSet(dataSet2, "monthvouchers", text4);
				string[] source2 = (from s in dataSet2.Tables["monthvouchers"].AsEnumerable()
					select s.Field<string>("VoucherID")).ToArray();
				string text5 = string.Join(",", source2.Select((string o) => "'" + o + "'"));
				if (text5 == "")
				{
					text5 = "' '";
				}
				text4 = "SELECT CONVERT(VARCHAR(10),EMP.JoiningDate,111) AS JoiningDate,CONVERT(VARCHAR(10),EMP.ResumedDate,111) as ResumedDate,EMP.Gender,Emp.SponsorID,EMP.ContractType,EMP.GroupID,EMP.PositionID,P.PositionName,Emp.DivisionID,Emp.LabourID,EMP.IBAN,EMP.BankID,Emp.NationalityID\r\n                                 ,CASE WHEN EMP.PaymentMethodID=3 THEN 'Bank Transfer' WHEN EMP.PaymentMethodID=2 THEN 'Cheque' WHEN EMP.PaymentMethodID=1 THEN 'Cash' END AS TransferType,EPD.EmployeeID,FirstName + ' ' + MiddleName + ' ' + LastName AS EmployeeName ,\r\n                                CASE WHEN EMP.JoiningDate> '" + text + "' THEN DateDiff(Day,EMP.JoiningDate,'" + text2 + "') + 1  ELSE DateDiff(Day,'" + text + "','" + text2 + "') + 1 END AS WorkDays,\r\n                                CASE WHEN EMP.JoiningDate> '" + text + "' THEN DateDiff(Day,EMP.JoiningDate,'" + text2 + "') + 1  ELSE DateDiff(Day,'" + text + "','" + text2 + "') + 1 END  AS NetDays,\r\n                                SUM(CASE WHEN PayrollItemType =1 AND PayCodeType = 1 THEN EPD.Amount ELSE 0 END) AS Basic, SUM(CASE WHEN PayrollItemType =1 AND PayCodeType = 2 THEN EPD.Amount ELSE 0 END) + ISNULL(SADD.Addition, 0) AS Allowance,\r\n                                SUM(CASE WHEN PayrollItemType =2 THEN -1 * EPD.Amount ELSE 0 END) + ISNULL(SSDD.Subtraction, 0) AS Deduction, SUM(CASE WHEN PayType = 1 THEN EPD.Amount ELSE 0 END) AS GrossSalary,\r\n                                ISNULL(OTHours, 0) OTHours,  ISNULL(OTAmount, 0) OTAmount, ISNULL(SADD.Addition, 0) Addition, ISNULL(SSDD.Subtraction, 0) Subtraction, \r\n\t\t\t\t\t\t\t\t\t(SUM(CASE WHEN PayType = 1 THEN EPD.Amount ELSE -1 * EPD.Amount END) + ISNULL(SSDD.Subtraction, 0)) + ISNULL(OTAmount, 0) + ISNULL(SADD.Addition, 0) AS NetSalary,\r\n                                (SELECT ISNULL(SUM(SSD3.NetSalary), 0) FROM SalarySheet_Detail SSD3 INNER JOIN SalarySheet SS3 ON SS3.SysdocID = SSD3.SysDocID AND SS3.VoucherID = SSD3.VoucherID    \r\n                                WHERE   SS3.Month = " + dateTime.Month + " AND SS3.Year= " + dateTime.Year + "  AND SSD3.EmployeeID = EPD.EmployeeID) AS LastSalary,\r\n                                (SELECT ISNULL(SUM(Amount), 0) FROM Employee_PayrollItem_Detail EPD2 INNER JOIN PayrollItem PItem2 ON Pitem2.PayrollItemID = EPD2.PayrollItemID \r\n\t\t\t\t\t\t\t\t\tWHERE ISNULL(InOvertime, 'False') = 'True' AND EPD2.EmployeeID = EPD.EmployeeID) AS OTBase,ISNULL(TEMP.Absent,0)  AS Absent,ISNULL(TEMP2.AnnualLeaves,0)  AS AnnualLeaves,ISNULL(TEMP3.UnpaidAnnualLeaves,0)  AS UnPaidAnnualLeaves,ISNULL(TEMP4.SickLeaves,0)  AS SickLeaves,ISNULL(TEMP5.NormalLeaves,0)  AS NormalLeaves,'' AS SlNo,\r\n                                    (select count(DISTINCT HD1.FromDate) * LT2.DeductionProportion from Holiday_Calendar_Detail HD1   \r\n                                     LEFT JOIN EMPLOYEE E2 ON HD1.CalendarID=EMP.CalendarID \r\n                                     LEFT JOIN  Employee_Activity EA2 ON EA2.EmployeeID = E2.EmployeeID\r\n                                     LEFT JOIN Employee_Leave_Request ELR2 ON EA2.ActivityID = ELR2.ActivityID\r\n                                     INNER JOIN Leave_Type LT2 ON LT2.LeaveTypeID=ELR2.LeaveTypeID AND LT2.IsAnnual<>1  AND LT2.ActivateHC=1 \r\n                                     and (HD1.FromDate BETWEEN ELR2.StartDate AND ELR2.EndDate\r\n                                     )WHERE  ISNULL(isvoid,0) =0 and ISNULL(IsApproved,0)=1 AND LT2.DeductionProportion<>0 and E2.EmployeeID=EMP.EmployeeID AND HD1.FromDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                                      GROUP BY LT2.DeductionProportion, E2.EmployeeID)AS ToLessTakenAbs,\r\n                                    (select count(DISTINCT HD1.FromDate) * LT2.DeductionProportion from Holiday_Calendar_Detail HD1   \r\n                                     LEFT JOIN EMPLOYEE E2 ON HD1.CalendarID=EMP.CalendarID \r\n                                     LEFT JOIN  Employee_Activity EA2 ON EA2.EmployeeID = E2.EmployeeID\r\n                                     LEFT JOIN Employee_Leave_Request ELR2 ON EA2.ActivityID = ELR2.ActivityID\r\n                                     INNER JOIN Leave_Type LT2 ON LT2.LeaveTypeID=ELR2.LeaveTypeID AND LT2.IsAnnual=1  AND LT2.ActivateHC=1 \r\n                                     and (HD1.FromDate BETWEEN ELR2.StartDate AND ELR2.EndDate\r\n                                     )WHERE  ISNULL(isvoid,0) =0 and ISNULL(IsApproved,0)=1 AND LT2.DeductionProportion<>0 and E2.EmployeeID=EMP.EmployeeID AND HD1.FromDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                                      GROUP BY LT2.DeductionProportion, E2.EmployeeID)AS ToLessTakenAnn,0.0 AS LoanDeduction                                                \r\n                                 FROM Employee_PayrollItem_Detail EPD\r\n                                INNER JOIN Employee EMP ON EMP.EmployeeID=EPD.EmployeeID \r\n\t\t\t\t\t\t\t\tINNER JOIN PayrollItem PItem ON Pitem.PayrollItemID = EPD.PayrollItemID\r\n                                LEFT OUTER JOIN Employee_Type ET ON ET.TypeID=EMP.ContractType\r\n\t\t\t\t\t\t        LEFT OUTER JOIN Employee_Group EG ON EG.GroupID=EMP.GroupID\r\n\t\t\t\t\t\t        LEFT OUTER JOIN Position P ON P.PositionID=EMP.PositionID\r\n\t\t\t\t\t\t        LEFT OUTER JOIN Division D ON D.DivisionID=EMP.DivisionID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(OTHours) OTHours, SUM(OTHours * OTRate) AS OTAmount FROM OverTimeEntry TB1 INNER JOIN OverTimeEntry_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE Month = " + periodMonth + " AND Year = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS EOT ON EOT.EmployeeID = EMP.EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t  \r\n                                LEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(Amount) Addition FROM Salary_Addition TB1 INNER JOIN Salary_Addition_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE DATEPART(Month, TB1.TransactionDate) = " + periodMonth + " AND DATEPART(Year, TB1.TransactionDate) = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS SADD ON SADD.EmployeeID = EMP.EmployeeID\r\n\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(Amount) Subtraction FROM Salary_Deduction TB1 INNER JOIN Salary_Deduction_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE DATEPART(Month, TB1.TransactionDate) = " + periodMonth + " AND DATEPART(Year, TB1.TransactionDate) = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS SSDD ON SSDD.EmployeeID = EMP.EmployeeID\r\n                                LEFT OUTER JOIN\r\n                                        (\r\n                                           --SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                           --CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                           --CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS ABSENT \r\n                                           --FROM Employee_Leave_Request ELR\r\n                                           --LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                           --LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual<>1\r\n                                          -- WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND\r\n                                           --ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP ON TEMP.EmployeeID=EMP.EmployeeID\r\n\r\n                                           SELECT EmployeeID ,SUM(Absent_Detail.ABSENT) [ABSENT]  FROM(SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                           CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                           CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS ABSENT \r\n                                           FROM Employee_Leave_Request ELR\r\n                                           LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                           LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual<>1\r\n                                           WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND\r\n                                           ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion )AS Absent_Detail GROUP BY EmployeeID ) AS TEMP ON TEMP.EmployeeID=EMP.EmployeeID\r\n\r\n                                   LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS AnnualLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual=1\r\n                                WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND  ISNULL(LT.IsPayable,'False')='True' AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP2 ON TEMP2.EmployeeID=EMP.EmployeeID\t\r\n\r\n\t\t\t\t\t\t\t LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS UnpaidAnnualLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and LT.IsAnnual=1\r\n                                WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False' AND ISNULL(LT.IsPayable,'False')='False' AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP3 ON TEMP3.EmployeeID=EMP.EmployeeID\r\n\r\n\r\n                                LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)*LT.DeductionProportion) AS SickLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and  LT.IsAnnual<>1 AND  ISNULL(LT.IsPayable,'False')='True' \r\n                                WHERE LT.DeductionProportion<>0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False'  AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP4 ON TEMP4.EmployeeID=EMP.EmployeeID\r\n\r\n                                LEFT OUTER JOIN\r\n                                (\r\n                                SELECT EA.EmployeeID,(SUM(datediff(day,\r\n                                CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1)) AS NormalLeaves \r\n                                FROM Employee_Leave_Request ELR\r\n                                LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID and  LT.IsAnnual<>1 AND  ISNULL(LT.IsPayable,'False')='False' \r\n                                WHERE LT.DeductionProportion=0 AND (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND ISNULL(ELR.IsVoid,'False')='False'  AND\r\n                                ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID,LT.DeductionProportion ) AS TEMP5 ON TEMP5.EmployeeID=EMP.EmployeeID\r\n\r\n\t\t\t\t\t\t\t\tWHERE  \r\n                               \t\t\t     (EPD.EmployeeID NOT IN (SELECT DISTINCT EMPLOYEEID FROM SalarySheet_Detail SSD INNER JOIN SalarySheet SS ON SS.SysdocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID\r\n\r\n                                           AND SS.Month = " + periodMonth + " AND Year = " + periodYear + " AND SS.SysDocID = '" + strSysDocID + "' AND SS.VoucherID IN (" + text5 + ")  AND EMPLOYEEID IN(" + EmployeeIDs + "))\r\n\t\t\t\t\t\t\t\t\t\t  ) AND\t\r\n                                EPD.EmployeeID IN (\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID FROM Employee E2 WHERE \r\n                                                    E2.JoiningDate <= '" + text2 + "' AND\r\n                                                    ISNULL(IsTerminated,'False')='False' AND ISNULL(IsCancelled,'False')='False' AND ISNULL(IsEOSSettled,'False')='False' AND \r\n\t\t\t\t\t\t\t\t\t\t\t\t\t(ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')\r\n                                                    AND EPD.EmployeeID  NOT IN(SELECT E.EmployeeID FROM Employee_Leave_Request ELR LEFT JOIN Employee_Activity EA ON ELR.ActivityID=EA.ActivityID \r\n                                                    LEFT JOIN Employee E ON E.EmployeeID= EA.EmployeeID WHERE ( ELR.ResumptionDate IS  NULL AND MONTH(ELR.EndDate)<" + periodMonth + " AND DATEPART(Year, ELR.EndDate) = " + periodYear + " ) AND ELR.IsApproved<>'False' )\r\n                                                    AND EPD.EmployeeID  NOT IN(SELECT E.EmployeeID FROM Employee_Leave_Request ELR LEFT JOIN Employee_Activity EA ON ELR.ActivityID=EA.ActivityID \r\n                                                    LEFT JOIN Employee E ON E.EmployeeID= EA.EmployeeID WHERE  ELR.IsApproved<>'False' AND ELR.StartDate <= '" + text + "' AND ELR.EndDate >='" + text2 + "'))";
				if (EmployeeIDs != "")
				{
					text4 = text4 + " AND E2.EmployeeID IN(" + EmployeeIDs + ") ";
				}
				if (fromEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text4 = text4 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text4 = text4 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text4 = text4 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text4 = text4 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text4 = text4 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text4 = text4 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text4 = text4 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text4 = text4 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text4 = text4 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text4 = text4 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text4 = text4 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text4 = text4 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text4 = text4 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text4 = text4 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text4 = text4 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text4 += " )GROUP BY\tEPD.EmployeeID,FirstName,MiddleName, LastName,  OTAmount, OTHours, Addition, Subtraction,Absent\r\n\t                    ,JoiningDate,ResumedDate,EMP.CalendarID,EMP.EmployeeID ,EMP.Gender,Emp.SponsorID,EMP.ContractType,EMP.GroupID,EMP.PositionID,P.PositionName,Emp.DivisionID,Emp.LabourID,EMP.IBAN,EMP.BankID,Emp.NationalityID,EMP.PaymentMethodID,TEMP2.AnnualLeaves,TEMP3.UnpaidAnnualLeaves ,TEMP4.SickLeaves,  TEMP5.NormalLeaves ";
				FillDataSet(dataSet, "SalarySheet_Detail", text4);
				if (dataSet.Tables["SalarySheet_Detail"].Rows.Count == 0)
				{
					return dataSet;
				}
				empty = string.Empty;
				for (int i = 0; i < dataSet.Tables["SalarySheet_Detail"].Rows.Count; i++)
				{
					empty = empty + "'" + dataSet.Tables["SalarySheet_Detail"].Rows[i]["EmployeeID"] + "'";
					if (i < dataSet.Tables["SalarySheet_Detail"].Rows.Count - 1)
					{
						empty += ",";
					}
					DataRow dataRow = dataSet.Tables["SalarySheet_Detail"].Rows[i];
					if (dataRow["SlNo"].ToString() == "")
					{
						dataRow["SlNo"] = i;
					}
				}
				text4 = "SELECT EPD.EmployeeID,PayType,EPD.PayrollItemID,NULL AS LoanSysDocID,PItem.PayrollItemName AS Description,StartDate,EndDate, InDeduction,PayCodeType AS PayCodeType,\r\n                                FirstName + ' ' + LastName AS EmployeeName ,CASE WHEN PayrollItemType = 1 THEN EPD.Amount ELSE -1* EPD.Amount END AS Amount, CASE WHEN PayrollItemType = 1 THEN EPD.Amount ELSE -1* EPD.Amount END AS PayableAmount,0 AS 'LeaveDays' FROM Employee_PayrollItem_Detail EPD\r\n                                INNER JOIN PayrollItem PItem ON Pitem.PayrollItemID = EPD.PayrollItemID\r\n                                INNER JOIN Employee ON Employee.EmployeeID=EPD.EmployeeID WHERE EPD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False'  AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text4 = text4 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text4 = text4 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text4 = text4 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text4 = text4 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text4 = text4 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text4 = text4 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text4 = text4 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text4 = text4 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text4 = text4 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text4 = text4 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text4 = text4 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text4 = text4 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text4 = text4 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text4 = text4 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text4 = text4 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text4 += ") ";
				text4 = text4 + " UNION \r\n\r\n                                SELECT OTE.EmployeeID,1 AS PayType,OTE.OTType,NULL AS LoanSysDocID,'Over Time' AS Description,OTE.PayrollPeriod,OTE.PayrollPeriod, NULL,'6',\r\n                                FirstName + ' ' + LastName AS EmployeeName , SUM(OTE.OTHours * OTE.OTRate) AS Amount, SUM(OTE.OTHours * OTE.OTRate) AS PayableAmount,LeaveDays\r\n\t\t\t\t\t\t\t\tFROM OverTimeEntry_Detail OTE\r\n                                INNER JOIN OverTimeEntry OT ON OTE.VoucherID = OT.VoucherID \r\n\t\t\t\t\t\t\t\r\n                                INNER JOIN Employee ON Employee.EmployeeID=OTE.EmployeeID WHERE OT.ApprovalDate IS NOT NULL\r\n                                AND Month =" + periodMonth + " AND Year = " + periodYear;
				text4 = text4 + " AND OTE.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False' AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text4 = text4 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text4 = text4 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text4 = text4 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text4 = text4 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text4 = text4 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text4 = text4 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text4 = text4 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text4 = text4 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text4 = text4 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text4 = text4 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text4 = text4 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text4 = text4 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text4 = text4 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text4 = text4 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text4 = text4 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text4 += ")";
				text4 += " GROUP BY OTE.EmployeeID, FirstName, LastName,  OTE.PayrollPeriod,OTE.OTType,LeaveDays";
				text4 = text4 + " UNION \r\n\r\n                        SELECT SAD.EmployeeID, 1 AS PayType, AdditionCode,NULL AS LoanSysDocID,'Benefit' AS Description,PayrollPeriod,PayrollPeriod, NULL,'7',\r\n                                FirstName + ' ' + LastName AS EmployeeName , SUM(Amount) AS Amount, SUM(Amount) AS PayableAmount ,0 AS 'LeaveDays'\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\tFROM Salary_Addition_Detail SAD\r\n                                INNER JOIN Salary_Addition SA ON SAD.VoucherID = SA.VoucherID \r\n                                INNER JOIN Employee ON Employee.EmployeeID=SAD.EmployeeID WHERE SA.ApprovalDate IS NOT NULL\r\n                                AND DATEPART(Month, PayrollPeriod) = " + periodMonth + "  AND DATEPART(Year, PayrollPeriod) =  " + periodYear;
				text4 = text4 + " AND ApprovalDate IS NOT NULL  AND SAD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False'  AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text4 = text4 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text4 = text4 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text4 = text4 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text4 = text4 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text4 = text4 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text4 = text4 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text4 = text4 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text4 = text4 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text4 = text4 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text4 = text4 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text4 = text4 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text4 = text4 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text4 = text4 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text4 = text4 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text4 = text4 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text4 += ")";
				text4 += " GROUP BY SAD.EmployeeID, FirstName, LastName, PayrollPeriod, AdditionCode";
				text4 = text4 + " UNION \r\n\r\n                       SELECT SDD.EmployeeID, 2 AS PayType, DeductionCode,NULL AS LoanSysDocID,'Deduction' AS Description,PayrollPeriod,PayrollPeriod, 1,'6',\r\n                                FirstName + ' ' + LastName AS EmployeeName , (SUM(Amount) * -1) AS Amount, (SUM(Amount) * -1) AS PayableAmount ,0 AS 'LeaveDays'\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\tFROM Salary_Deduction_Detail SDD\r\n                                INNER JOIN Salary_Deduction SD ON SDD.VoucherID = SD.VoucherID\r\n                                INNER JOIN Employee ON Employee.EmployeeID=SDD.EmployeeID WHERE SD.ApprovalDate IS NOT NULL\r\n                                AND DATEPART(Month, PayrollPeriod) = " + periodMonth + "  AND DATEPART(Year, PayrollPeriod) =  " + periodYear;
				text4 = text4 + " AND ApprovalDate IS NOT NULL AND SDD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False'  AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text4 = text4 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text4 = text4 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text4 = text4 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text4 = text4 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text4 = text4 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text4 = text4 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text4 = text4 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text4 = text4 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text4 = text4 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text4 = text4 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text4 = text4 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text4 = text4 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text4 = text4 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text4 = text4 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text4 = text4 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text4 += ")";
				text4 += " GROUP BY SDD.EmployeeID, FirstName, LastName, PayrollPeriod, DeductionCode";
				text4 = text4 + " UNION \r\n\r\n                        SELECT EL.EmployeeID, 3 AS PayType, VoucherID AS PayrollItemID,SysDocID AS LoanSysDocID,'Loan Recovery' AS Description, DedStartDate,NULL AS EndDate, 1 AS InDeduction,NULL AS PayCodeType,\r\n                        FirstName + ' ' + LastName AS EmployeeName,\r\n                        CASE WHEN ISNULL(IsVoid,'False')='True' THEN 0\r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) >= Amount THEN 0 \r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) + InstallmentAmount > Amount THEN -1 * ( Amount-ISNULL(PaidAmount,0) - ISNULL(DiscountAmount ,0) )\r\n                        ELSE -1 * ISNULL(InstallmentAmount,0) END AS [Amount], 0 AS PayableAmount,0 AS 'LeaveDays'\r\n                        FROM Employee_Loan EL LEFT OUTER JOIN Employee ON EL.EmployeeID=Employee.EmployeeID WHERE \r\n                        '" + text2 + "' >= EL.DedStartDate-1  AND ISNULL(IsVoid,'False')='False' \r\n                        AND ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) < Amount ";
				text4 = text4 + " AND EL.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE  \r\n                                ISNULL(IsTerminated,'False')='False'  AND \r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
				if (fromEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID>='" + fromEmployee + "' ";
				}
				if (toEmployee != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toEmployee + "' ";
				}
				if (fromDepartment != "")
				{
					text4 = text4 + " AND E2.DepartmentID>='" + fromDepartment + "' ";
				}
				if (toDepartment != "")
				{
					text4 = text4 + " AND E2.EmployeeID<='" + toDepartment + "' ";
				}
				if (fromLocation != "")
				{
					text4 = text4 + " AND E2.LocationID>='" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text4 = text4 + " AND E2.LocationID<='" + toLocation + "' ";
				}
				if (fromType != "")
				{
					text4 = text4 + " AND E2.ContractType >='" + fromType + "' ";
				}
				if (toType != "")
				{
					text4 = text4 + " AND E2.ContractType <='" + toType + "' ";
				}
				if (fromDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID >='" + fromDivision + "' ";
				}
				if (toDivision != "")
				{
					text4 = text4 + " AND E2.DivisionID <='" + toDivision + "' ";
				}
				if (fromSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID >='" + fromSponsor + "' ";
				}
				if (toSponsor != "")
				{
					text4 = text4 + " AND E2.SponsorID <='" + toSponsor + "' ";
				}
				if (fromGroup != "")
				{
					text4 = text4 + " AND E2.GroupID >='" + fromGroup + "' ";
				}
				if (toGroup != "")
				{
					text4 = text4 + " AND E2.GroupID <='" + toGroup + "' ";
				}
				if (fromGrade != "")
				{
					text4 = text4 + " AND E2.GradeID >='" + fromGrade + "' ";
				}
				if (toGrade != "")
				{
					text4 = text4 + " AND E2.GradeID <='" + toGrade + "' ";
				}
				if (fromPosition != "")
				{
					text4 = text4 + " AND E2.PositionID >='" + fromPosition + "' ";
				}
				if (toPostion != "")
				{
					text4 = text4 + " AND E2.PositionID <='" + toPostion + "' ";
				}
				if (fromBank != "")
				{
					text4 = text4 + " AND E2.BankID >='" + fromBank + "' ";
				}
				if (toBank != "")
				{
					text4 = text4 + " AND E2.BankID <='" + toBank + "' ";
				}
				if (fromAccount != "")
				{
					text4 = text4 + " AND E2.AccountID >='" + fromAccount + "' ";
				}
				if (toAccount != "")
				{
					text4 = text4 + " AND E2.AccountID <='" + toAccount + "' ";
				}
				text4 += ")";
				text4 += " ORDER BY EmployeeID, PayType ";
				FillDataSet(dataSet, "SalarySheet_Detail_Item", text4);
				dataSet.Relations.Add("REL", dataSet.Tables["SalarySheet_Detail"].Columns["EmployeeID"], dataSet.Tables["SalarySheet_Detail_Item"].Columns["EmployeeID"]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
