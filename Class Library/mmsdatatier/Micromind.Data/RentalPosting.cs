using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class RentalPosting : StoreObject
	{
		private const string RENTALPOSTING_TABLE = "Rental_Posting";

		private const string RENTALPOSTINGDETAIL_TABLE = "Rental_Posting_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string SHEETNAME_PARM = "@SheetName";

		private const string MONTH_PARM = "@Month";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		public const string ASOFDATE_PARM = "@AsofDate";

		private const string YEAR_PARM = "@Year";

		private const string NOTE_PARM = "@Note";

		private const string REFERENCE_PARM = "@Reference";

		private const string ISPOSTED_PARM = "@IsPosted";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PAYTYPE_PARM = "@PayType";

		private const string TENANTID_PARM = "@TenantID";

		private const string RENTEDDAYS_PARM = "@RentedDays";

		private const string NETAMOUNT_PARM = "@NetAmount";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string PAYROLLITEMID_PARM = "@PayrollItemID";

		private const string AMOUNT_PARM = "@Amount";

		private const string DESCRIPTION_PARM = "@Description";

		private const string PAYABLEAMOUNT_PARM = "@PayableAmount";

		private const string PAID_PARM = "@Paid";

		private const string INDEDUCTION_PARM = "@InDeduction";

		private const string PAYCODETYPE_PARM = "@PayCodeType";

		private const string ISFIXED_PARM = "@IsFixed";

		public RentalPosting(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateRentalPostingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Rental_Posting", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("SheetName", "@SheetName"), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Month", "@Month"), new FieldValue("Year", "@Year"), new FieldValue("AsofDate", "@AsofDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Rental_Posting", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateRentalPostingCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateRentalPostingText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateRentalPostingText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SheetName", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@Year", SqlDbType.SmallInt);
			parameters.Add("@AsofDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SheetName"].SourceColumn = "SheetName";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@AsofDate"].SourceColumn = "AsofDate";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@Year"].SourceColumn = "Year";
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

		private string GetInsertUpdateRentalPostingDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Rental_Posting_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TenantID", "@TenantID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("RentedDays", "@RentedDays"), new FieldValue("NetAmount", "@NetAmount"), new FieldValue("StartDate", "@StartDate"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("EndDate", "@EndDate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateRentalPostingDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateRentalPostingDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateRentalPostingDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TenantID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@RentedDays", SqlDbType.Decimal);
			parameters.Add("@NetAmount", SqlDbType.Money);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TenantID"].SourceColumn = "TenantID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@RentedDays"].SourceColumn = "RentedDays";
			parameters["@NetAmount"].SourceColumn = "NetAmount";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateRentalPostingDetailItemsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Rental_Posting_Detail_Item", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TenantID", "@TenantID"), new FieldValue("Description", "@Description"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PayType", "@PayType"), new FieldValue("PayrollItemID", "@PayrollItemID"), new FieldValue("Amount", "@Amount"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("PayCodeType", "@PayCodeType"), new FieldValue("PayableAmount", "@PayableAmount"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateRentalPostingDetailItemsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateRentalPostingDetailItemsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateRentalPostingDetailItemsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TenantID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters.Add("@PayType", SqlDbType.TinyInt);
			parameters.Add("@PayrollItemID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@PayableAmount", SqlDbType.Money);
			parameters.Add("@PayCodeType", SqlDbType.TinyInt);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TenantID"].SourceColumn = "TenantID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PayType"].SourceColumn = "PayType";
			parameters["@PayrollItemID"].SourceColumn = "PayrollItemID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PayableAmount"].SourceColumn = "PayableAmount";
			parameters["@PayCodeType"].SourceColumn = "PayCodeType";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(RentalPostingData journalData)
		{
			return true;
		}

		public bool InsertUpdateRentalPosting(RentalPostingData salarySheetData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateRentalPostingCommand = GetInsertUpdateRentalPostingCommand(isUpdate);
			try
			{
				DataRow dataRow = salarySheetData.RentalPostingTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate)
				{
					if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("Rental_Posting", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
				}
				else
				{
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Rental_Posting", "IsPosted", "SysDocID", text2, "VoucherID", text, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
					{
						throw new CompanyException("This salary sheet is already posted and cannot be edited.");
					}
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Rental_Posting", "IsVoid", "SysDocID", text2, "VoucherID", text, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
					{
						throw new CompanyException("This salary sheet is already voided and cannot be edited.");
					}
				}
				insertUpdateRentalPostingCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salarySheetData, "Rental_Posting", insertUpdateRentalPostingCommand)) : (flag & Insert(salarySheetData, "Rental_Posting", insertUpdateRentalPostingCommand)));
				insertUpdateRentalPostingCommand = GetInsertUpdateRentalPostingDetailsCommand(isUpdate: false);
				insertUpdateRentalPostingCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteRentalPostingDetailsRows(text2, text, sqlTransaction);
				}
				if (salarySheetData.Tables["Rental_Posting_Detail"].Rows.Count > 0)
				{
					flag &= Insert(salarySheetData, "Rental_Posting_Detail", insertUpdateRentalPostingCommand);
				}
				insertUpdateRentalPostingCommand = GetInsertUpdateRentalPostingDetailItemsCommand(isUpdate: false);
				insertUpdateRentalPostingCommand.Transaction = sqlTransaction;
				if (salarySheetData.Tables["Rental_Posting_Detail_Item"].Rows.Count > 0)
				{
					flag &= Insert(salarySheetData, "Rental_Posting_Detail_Item", insertUpdateRentalPostingCommand);
				}
				GLData journalData = CreateGLData(salarySheetData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Rental_Posting", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Rental Posting";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Rental_Posting_Detail", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PropertyRentPost, text2, text, "Rental_Posting", sqlTransaction);
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

		public RentalPostingData GetRentalPostingByID(string sysDocID, string voucherID)
		{
			try
			{
				RentalPostingData rentalPostingData = new RentalPostingData();
				string textCommand = "SELECT SS.*, CASE Month WHEN 1 THEN 'JAN' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 THEN 'APR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AUG' WHEN 9 THEN 'SEP'  WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN '12' THEN 'DEC' END AS MonthName FROM Rental_Posting SS WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(rentalPostingData, "Rental_Posting", textCommand);
				if (rentalPostingData == null || rentalPostingData.Tables.Count == 0 || rentalPostingData.Tables["Rental_Posting"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT RPD.*,P.PropertyName,PU.PropertyUnitID,PU.PropertyUnitName,C.CustomerName,C.CustomerID,PR.PropertyID,PR.ContractStartDate,PR.ContractEndDate,DATEDIFF(day,PR.ContractStartDate,PR.ContractEndDate) +1 AS TotalDays,PR.Total, DATEDIFF(day,RPD.StartDate,RP.AsofDate)+1 as RentalDays  FROM Rental_Posting_Detail RPD INNER JOIN Customer C ON RPD.TenantID=C.CustomerID \r\n                        INNER JOIN Rental_Posting RP ON RPD.SysDocID=RP.SysDocID AND  RPD.VoucherID=RP.VoucherID\r\n                        INNER JOIN Property_Rent PR ON PR.SysDocID=RPD.SourceSysDocID AND PR.VoucherID=RPD.SourceVoucherID\r\n                        LEFT JOIN Property P ON P.PropertyID=PR.PropertyID\r\n                        LEFT JOIN Property_Unit PU ON PU.PropertyUnitID=PR.UnitID \r\n                        WHERE RPD.SysDocID='" + sysDocID + "' AND RPD.VoucherID = '" + voucherID + "' order by  CAST(RPD.RowIndex AS INT)";
				FillDataSet(rentalPostingData, "Rental_Posting_Detail", textCommand);
				textCommand = "SELECT RPDI.*,RPDI.SourceSysDocID,RPDI.SourceVoucherID,PR.PropertyID,PID.IncomeName,PID.IncomeType,DATEDIFF(day,PR.ContractStartDate,PR.ContractEndDate) +1 AS TotalDays,PR.Total,\r\n                        DATEDIFF(day,RPD.StartDate,RP.AsofDate)+1 as RentalDays,\r\n                        RPDI.TenantID as CustomerID FROM Rental_Posting_Detail_Item RPDI\r\n                        INNER JOIN Rental_Posting RP ON RPDI.SysDocID=RP.SysDocID AND  RPDI.VoucherID=RP.VoucherID\r\n                        INNER JOIN Rental_Posting_Detail RPD ON RPD.SourceSysDocID=RPDI.SourceSysDocID AND  RPD.SourceVoucherID=RPDI.SourceVoucherID\r\n                        INNER JOIN Property_Rent PR ON PR.SysDocID=RPDI.SourceSysDocID AND PR.VoucherID=RPDI.SourceVoucherID\r\n                        INNER JOIN Property_Rent PR1 ON PR1.SysDocID=RPDI.SourceSysDocID AND PR1.VoucherID=RPDI.SourceVoucherID\r\n                        INNER JOIN PropertyIncome_Code PID ON RPDI.PayrollItemID=PID.IncomeID\r\n                        WHERE RPDI.SysDocID='" + sysDocID + "' AND RPDI.VoucherID = '" + voucherID + "'  order by  CAST(RPDI.RowIndex AS INT)";
				FillDataSet(rentalPostingData, "Rental_Posting_Detail_Item", textCommand);
				return rentalPostingData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteRentalPostingDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Rental_Posting_Detail_Item WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Rental_Posting_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidRentalPosting(string sysDocID, string voucherID, bool isVoid)
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
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Rental_Posting", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
				{
					throw new CompanyException("This salary sheet is already voided.");
				}
				bool flag3 = false;
				fieldValue = new Databases(base.DBConfig).GetFieldValue("Rental_Posting", "IsPosted", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
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

		public bool DeleteRentalPosting(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteRentalPostingDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Rental_Posting WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Rental Posting", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet CalculateRentalPosting(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0));
				string text2 = CommonLib.ToSqlDateTimeString(new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59));
				string text3 = "";
				string empty = string.Empty;
				text3 = "SELECT EPD.EmployeeID,FirstName + ' ' + LastName AS EmployeeName , DateDiff(Day,'" + text + "','" + text2 + "') + 1 AS WorkDays, DateDiff(Day,'" + text + "','" + text2 + "') + 1 AS NetDays,\r\n                                SUM(CASE WHEN PayrollItemType =1 AND PayCodeType = 1 THEN EPD.Amount ELSE 0 END) AS Basic, SUM(CASE WHEN PayrollItemType =1 AND PayCodeType = 2 THEN EPD.Amount ELSE 0 END) + ISNULL(SADD.Addition, 0) AS Allowance,\r\n                                SUM(CASE WHEN PayrollItemType =2 THEN -1 * EPD.Amount ELSE 0 END) + ISNULL(SSDD.Subtraction, 0) AS Deduction, SUM(CASE WHEN PayType = 1 THEN EPD.Amount ELSE 0 END) AS GrossSalary,\r\n                                ISNULL(OTHours, 0) OTHours,  ISNULL(OTAmount, 0) OTAmount, ISNULL(SADD.Addition, 0) Addition, ISNULL(SSDD.Subtraction, 0) Subtraction, \r\n\t\t\t\t\t\t\t\t\t(SUM(CASE WHEN PayType = 1 THEN EPD.Amount ELSE -1 * EPD.Amount END) + ISNULL(SSDD.Subtraction, 0)) + ISNULL(OTAmount, 0) + ISNULL(SADD.Addition, 0) AS NetSalary,\r\n                                (SELECT ISNULL(SUM(Amount), 0) FROM Employee_PayrollItem_Detail EPD2 INNER JOIN PayrollItem PItem2 ON Pitem2.PayrollItemID = EPD2.PayrollItemID \r\n\t\t\t\t\t\t\t\t\tWHERE ISNULL(InOvertime, 'False') = 'True' AND EPD2.EmployeeID = EPD.EmployeeID) AS OTBase,ISNULL(TEMP.Absent,0)  AS Absent                                                  \r\n\r\n                              FROM Employee_PayrollItem_Detail EPD\r\n                                INNER JOIN Employee EMP ON EMP.EmployeeID=EPD.EmployeeID \r\n\t\t\t\t\t\t\t\tINNER JOIN PayrollItem PItem ON Pitem.PayrollItemID = EPD.PayrollItemID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(OTHours) OTHours, SUM(OTHours * OTRate) AS OTAmount FROM OverTimeEntry TB1 INNER JOIN OverTimeEntry_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE Month = " + periodMonth + " AND Year = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS EOT ON EOT.EmployeeID = EMP.EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t  \r\n                                LEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(Amount) Addition FROM Salary_Addition TB1 INNER JOIN Salary_Addition_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE DATEPART(Month, TB1.TransactionDate) = " + periodMonth + " AND DATEPART(Year, TB1.TransactionDate) = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS SADD ON SADD.EmployeeID = EMP.EmployeeID\r\n\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t\t\t (\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID, SUM(Amount) Subtraction FROM Salary_Deduction TB1 INNER JOIN Salary_Deduction_Detail TB2 ON TB1.VoucherID = TB2.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t\tWHERE DATEPART(Month, TB1.TransactionDate) = " + periodMonth + " AND DATEPART(Year, TB1.TransactionDate) = " + periodYear + " AND ApprovalDate IS NOT NULL GROUP BY EmployeeID\r\n\t\t\t\t\t\t\t\t\t\t ) AS SSDD ON SSDD.EmployeeID = EMP.EmployeeID\r\n                                LEFT OUTER JOIN\r\n                                        (\r\n                                           SELECT EA.EmployeeID,SUM(datediff(day,\r\n                                           CASE WHEN StartDate < '" + text + "' THEN  '" + text + "' ELSE  StartDate END ,\r\n                                           CASE WHEN EndDate  > '" + text2 + "' THEN    '" + text2 + "' ELSE  EndDate  END )+1*LT.DeductionProportion) AS ABSENT \r\n                                           FROM Employee_Leave_Request ELR\r\n                                           LEFT JOIN Employee_Activity EA  ON ELR.ActivityID=EA.ActivityID\r\n                                           LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ELR.LeaveTypeID\r\n                                           WHERE (( StartDate BETWEEN  '" + text + "' AND  '" + text2 + "' ) or   ( EndDate BETWEEN  '" + text + "' AND '" + text2 + "' ))  AND\r\n                                           ISNULL(ELR.IsApproved, 0 ) = 1 GROUP BY EA.EmployeeID ) AS TEMP ON TEMP.EmployeeID=EMP.EmployeeID\r\n\t\t\t\t\t\t\t\tWHERE    (\r\n\t\t\t\t\t\t\t\t\t\t\tEPD.EmployeeID NOT IN (SELECT DISTINCT TENANTID FROM RentalPosting_Detail SSD INNER JOIN RentalPosting SS ON SS.SysdocID = SSD.SysDocID AND SS.VoucherID= SSD.VoucherID\r\n\t\t\t\t\t\t\t\t\t\t    AND SS.Month =" + periodMonth + " AND Year = " + periodYear + ")\r\n\t\t\t\t\t\t\t\t\t\t  ) \r\n\t\t\t\t\t\t\t\tAND\r\n\r\n                                EPD.EmployeeID IN (\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tSELECT EmployeeID FROM Employee E2 WHERE \r\n\t\t\t\t\t\t\t\t\t\t\t\t\tISNULL(IsTerminated,'False')='False' AND \r\n\t\t\t\t\t\t\t\t\t\t\t\t\t(ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True'))";
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
				text3 += " )\tGROUP BY EPD.EmployeeID, FirstName, LastName,  OTAmount, OTHours, Addition, Subtraction,Absent  ";
				FillDataSet(dataSet, "Rental_Posting_Detail", text3);
				if (dataSet.Tables["Rental_Posting_Detail"].Rows.Count == 0)
				{
					return dataSet;
				}
				empty = string.Empty;
				for (int i = 0; i < dataSet.Tables["Rental_Posting_Detail"].Rows.Count; i++)
				{
					empty = empty + "'" + dataSet.Tables["Rental_Posting_Detail"].Rows[i]["EmployeeID"] + "'";
					if (i < dataSet.Tables["Rental_Posting_Detail"].Rows.Count - 1)
					{
						empty += ",";
					}
				}
				text3 = "SELECT EPD.EmployeeID,PayType,EPD.PayrollItemID,NULL AS LoanSysDocID,PItem.PayrollItemName AS Description,StartDate,EndDate, InDeduction,PayCodeType AS PayCodeType,\r\n                                FirstName + ' ' + LastName AS EmployeeName ,CASE WHEN PayrollItemType = 1 THEN EPD.Amount ELSE -1* EPD.Amount END AS Amount, CASE WHEN PayrollItemType = 1 THEN EPD.Amount ELSE -1* EPD.Amount END AS PayableAmount FROM Employee_PayrollItem_Detail EPD\r\n                                INNER JOIN PayrollItem PItem ON Pitem.PayrollItemID = EPD.PayrollItemID\r\n                                INNER JOIN Employee ON Employee.EmployeeID=EPD.EmployeeID WHERE EPD.EmployeeID IN \r\n                                (SELECT EmployeeID FROM Employee E2 WHERE \r\n                                ISNULL(IsTerminated,'False')='False'  AND\r\n                                (ContractType IS NULL OR ContractType IN (SELECT ContractType FROM Employee_Type WHERE ISNULL(IsPayroll,'False')='True')) AND E2.EmployeeID IN (" + empty + ")";
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
				text3 = text3 + " UNION \r\n\r\n                                SELECT OTE.EmployeeID,1 AS PayType,OTE.OTType,NULL AS LoanSysDocID,'Over Time' AS Description,OTE.PayrollPeriod,OTE.PayrollPeriod, NULL,'6',\r\n                                FirstName + ' ' + LastName AS EmployeeName , SUM(OTE.OTHours * OTE.OTRate) AS Amount, SUM(OTE.OTHours * OTE.OTRate) AS PayableAmount \r\n\t\t\t\t\t\t\t\tFROM OverTimeEntry_Detail OTE\r\n                                INNER JOIN OverTimeEntry OT ON OTE.VoucherID = OT.VoucherID \r\n\t\t\t\t\t\t\t\r\n                                INNER JOIN Employee ON Employee.EmployeeID=OTE.EmployeeID WHERE OT.ApprovalDate IS NOT NULL\r\n                                AND Month =" + periodMonth + " AND Year = " + periodYear;
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
				text3 += " GROUP BY OTE.EmployeeID, FirstName, LastName,  OTE.PayrollPeriod,OTE.OTType";
				text3 = text3 + " UNION \r\n\r\n                        SELECT SAD.EmployeeID, 1 AS PayType, AdditionCode,NULL AS LoanSysDocID,'Benefit' AS Description,PayrollPeriod,PayrollPeriod, NULL,'7',\r\n                                FirstName + ' ' + LastName AS EmployeeName , SUM(Amount) AS Amount, SUM(Amount) AS PayableAmount \r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\tFROM Salary_Addition_Detail SAD\r\n                                INNER JOIN Salary_Addition SA ON SAD.VoucherID = SA.VoucherID \r\n                                INNER JOIN Employee ON Employee.EmployeeID=SAD.EmployeeID WHERE SA.ApprovalDate IS NOT NULL\r\n                                AND DATEPART(Month, TransactionDate) = " + periodMonth + "  AND DATEPART(Year, TransactionDate) =  " + periodYear;
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
				text3 += " GROUP BY SAD.EmployeeID, FirstName, LastName, PayrollPeriod, AdditionCode";
				text3 = text3 + " UNION \r\n\r\n                       SELECT SDD.EmployeeID, 2 AS PayType, DeductionCode,NULL AS LoanSysDocID,'Deduction' AS Description,PayrollPeriod,PayrollPeriod, 1,'6',\r\n                                FirstName + ' ' + LastName AS EmployeeName , (SUM(Amount) * -1) AS Amount, (SUM(Amount) * -1) AS PayableAmount \r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\tFROM Salary_Deduction_Detail SDD\r\n                                INNER JOIN Salary_Deduction SD ON SDD.VoucherID = SD.VoucherID\r\n                                INNER JOIN Employee ON Employee.EmployeeID=SDD.EmployeeID WHERE SD.ApprovalDate IS NOT NULL\r\n                                AND DATEPART(Month, TransactionDate) = " + periodMonth + "  AND DATEPART(Year, TransactionDate) =  " + periodYear;
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
				text3 += " GROUP BY SDD.EmployeeID, FirstName, LastName, PayrollPeriod, DeductionCode";
				text3 += " UNION \r\n\r\n                        SELECT EL.EmployeeID, 3 AS PayType, VoucherID AS PayrollItemID,SysDocID AS LoanSysDocID,'Loan Recovery' AS Description, DedStartDate,NULL AS EndDate, 1 AS InDeduction,NULL AS PayCodeType,\r\n                        FirstName + ' ' + LastName AS EmployeeName,\r\n                        CASE WHEN ISNULL(IsVoid,'False')='True' THEN 0\r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) >= Amount THEN 0 \r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) + InstallmentAmount > Amount THEN -1 * ( Amount-ISNULL(PaidAmount,0) - ISNULL(DiscountAmount ,0) )\r\n                        ELSE -1 * ISNULL(InstallmentAmount,0) END AS [Amount], 0 AS PayableAmount\r\n                        FROM Employee_Loan EL LEFT OUTER JOIN Employee ON EL.EmployeeID=Employee.EmployeeID WHERE \r\n                        DedStartDate <= EL.DedStartDate AND ISNULL(IsVoid,'False')='False' \r\n                        AND ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) < Amount ";
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
				FillDataSet(dataSet, "Rental_Posting_Detail_Item", text3);
				dataSet.Relations.Add("REL", dataSet.Tables["Rental_Posting_Detail"].Columns["EmployeeID"], dataSet.Tables["Rental_Posting_Detail_Item"].Columns["EmployeeID"]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetRentalPostingItems(string sysDocID, string voucherID, string[] employeeIDs)
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
				FillDataSet(dataSet, "Rental_Posting", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUnpostedRentalPostings()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID,VoucherID AS [Sheet Number],SheetName [Description],TransactionDate [Sheet Date],Note \r\n                                FROM Salary_Sheet WHERE ISNULL(IsPosted,'False')='False'";
				FillDataSet(dataSet, "Rental_Posting", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenRentalPostings()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SS.SysDocID,SS.VoucherID,SheetName,TransactionDate,Month,Year,StartDate,EndDate,Note FROM RentalPosting SS\r\n                                    INNER JOIN RentalPosting_Detail SSD ON SS.SysDocID = SSD.SysDocID AND SS.VoucherID = SSD.VoucherID\r\n                                    WHERE ISNULL(IsClosed,'False') = 'False'\r\n                                    GROUP BY SS.SysDocID,SS.VoucherID,SheetName,TransactionDate,Month,Year,StartDate,EndDate,Note\r\n                                    HAVING SUM(ISNULL(PaidAmount,0)) < SUM(ISNULL(NetSalary,0))";
				FillDataSet(dataSet, "Rental_Posting", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetRentalPostingEmployees(string docID, string voucherID, byte paymentMethodID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID,VoucherID,SSD.EmployeeID, EMP.FirstName + ' ' + LastName AS Name,WorkDays-Absent AS [WorkDays], NetSalary, NetSalary - ISNULL(PaidAmount,0) AS Balance, RowIndex\r\n                                 FROM RentalPosting_Detail SSD INNER JOIN Employee EMP ON SSD.EmployeeID = EMP.EmployeeID   WHERE ISNULL(PaidAmount,0) < NetSalary AND\r\n                                 SysDocID='" + docID + "' AND VoucherID = '" + voucherID + "' ";
				FillDataSet(dataSet, "Rental_Posting_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool PostRentalPosting(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Rental_Posting", "IsPosted", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "" && bool.Parse(fieldValue.ToString()))
				{
					flag = false;
					throw new CompanyException("This salary sheet is already posted.", 1020);
				}
				GetRentalPostingByID(sysDocID, voucherID);
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

		private GLData CreateGLData(RentalPostingData salarySheetData, SqlTransaction sqlTransaction)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				GLData gLData = new GLData();
				DataRow dataRow = salarySheetData.RentalPostingTable.Rows[0];
				string text = "";
				dataRow["SysDocID"].ToString();
				dataRow["VoucherID"].ToString();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.PropertyRentPost;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = dataRow["SheetName"].ToString();
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				ArrayList arrayList = new ArrayList();
				string commaSeperatedIDs = GetCommaSeperatedIDs(salarySheetData, "Rental_Posting_Detail", "Property");
				string textCommand = "select RentIncomeAccountID,PropertyID From Property WHERE PropertyID IN (" + commaSeperatedIDs + ") ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Property", textCommand, sqlTransaction);
				foreach (DataRow row in salarySheetData.RentalPostingDetailTable.Rows)
				{
					string text2 = row["Property"].ToString();
					string text3 = row["Property"].ToString();
					string value = row["PropertyUnitID"].ToString();
					DataRow[] array = dataSet.Tables[0].Select("PropertyID = '" + text2 + "'");
					if (array.Length != 0 && array[0]["RentIncomeAccountID"] != DBNull.Value)
					{
						text = array[0]["RentIncomeAccountID"].ToString();
					}
					if (text == "")
					{
						throw new CompanyException("Account is not set for the Customer '" + text2 + "'.", 1021);
					}
					decimal num = default(decimal);
					string text4 = "";
					string value2 = "";
					decimal num2 = default(decimal);
					if (!arrayList.Contains(text3))
					{
						arrayList.Add(text3);
						array = salarySheetData.RentalPostingDetailItemsTable.Select("Property= '" + text2 + "'");
						string value3 = dataRow["SheetName"].ToString();
						foreach (DataRow dataRow3 in array)
						{
							if (!dataRow3["IsFixed"].IsDBNullOrEmpty())
							{
								bool.Parse(dataRow3["IsFixed"].ToString());
							}
							num2 += decimal.Parse(dataRow3["PayableAmount"].ToString());
						}
						DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text;
						dataRow4["PayeeID"] = "";
						dataRow4["PayeeType"] = "";
						dataRow4["IsARAP"] = true;
						dataRow4["Debit"] = DBNull.Value;
						dataRow4["Description"] = value3;
						dataRow4["Credit"] = num2;
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["AttributeID1"] = text3;
						dataRow4["AttributeID2"] = value;
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
						foreach (DataRow dataRow5 in array)
						{
							text4 = dataRow5["PayrollItemID"].ToString();
							num = Math.Round(decimal.Parse(dataRow5["PayableAmount"].ToString()), currencyDecimalPoints);
							byte b = 1;
							b = byte.Parse(dataRow5["PayType"].ToString());
							if (!dataRow5["IsFixed"].IsDBNullOrEmpty())
							{
								bool.Parse(dataRow5["IsFixed"].ToString());
							}
							object obj2 = null;
							switch (b)
							{
							case 1:
							case 2:
								obj2 = new Databases(base.DBConfig).GetFieldValue("PropertyIncome_Code", "AccountID", "IncomeID", text4, sqlTransaction);
								if (obj2 == null || obj2.ToString() == "")
								{
									obj2 = new Databases(base.DBConfig).GetFieldValue("Benefit", "AccountID", "BenefitID", text4, sqlTransaction);
								}
								if (obj2 == null || obj2.ToString() == "")
								{
									obj2 = new Databases(base.DBConfig).GetFieldValue("Employee_OverTime", "AccountID", "OverTimeID", text4, sqlTransaction);
								}
								if (obj2 == null || obj2.ToString() == "")
								{
									throw new CompanyException("Account is not set for payroll item '" + text4 + "'.", 1022);
								}
								value2 = obj2.ToString();
								break;
							case 4:
								if (obj2 == null || obj2.ToString() == "")
								{
									throw new CompanyException("Account is not set for payroll item '" + text4 + "'.", 1022);
								}
								value2 = obj2.ToString();
								break;
							}
							dataRow4 = gLData.JournalDetailsTable.NewRow();
							dataRow4.BeginEdit();
							dataRow4["JournalID"] = 0;
							dataRow4["AccountID"] = value2;
							dataRow4["PayeeID"] = text2;
							dataRow4["Description"] = dataRow5["Description"];
							if (b == 0)
							{
								throw new CompanyException("PayType is not defined.");
							}
							dataRow4["Credit"] = DBNull.Value;
							dataRow4["Debit"] = num;
							dataRow4["Reference"] = dataRow["Reference"];
							dataRow4["Reference"] = dataRow["Reference"];
							dataRow4["AttributeID1"] = text3;
							dataRow4["AttributeID2"] = value;
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
			string text = "SELECT SysDocID,VoucherID,SSD.EmployeeID, E2.FirstName + ' ' + LastName AS Name,WorkDays-Absent AS [WorkDays], NetSalary, NetSalary - ISNULL(PaidAmount,0) AS Balance, RowIndex\r\n                           FROM RentalPosting_Detail SSD INNER JOIN Employee E2 ON SSD.EmployeeID = E2.EmployeeID   WHERE ISNULL(PaidAmount,0) < NetSalary AND\r\n                           SSD.SysDocID='" + sysdocid + "' AND SSD.VoucherID='" + vouhcerid + "'";
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
			FillDataSet(dataSet, "Rental_Posting_Detail", text);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool isVoid)
		{
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,SheetName,TransactionDate,AsofDate,Note,Reference From Rental_Posting ORDER BY TransactionDate ";
			FillDataSet(dataSet, "Rental_Posting", textCommand);
			return dataSet;
		}
	}
}
