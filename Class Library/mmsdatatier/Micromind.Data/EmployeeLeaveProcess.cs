using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeLeaveProcess : StoreObject
	{
		private const string EMPLOYEELEAVEPROCESS_TABLE = "Employee_Leave_Process";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string EMPLOYEENAME_PARM = "@EmployeeName";

		private const string DAYS_PARM = "@Days";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string FROMDATE_PARM = "@FromDate";

		private const string TODATE_PARM = "@ToDate";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PERIOD_PARM = "@TransactionDate";

		private const string NOTE_PARM = "@Note";

		public EmployeeLeaveProcess(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Process", new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("EmployeeName", "@EmployeeName"), new FieldValue("Days", "@Days"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("FromDate", "@FromDate"), new FieldValue("ToDate", "@ToDate"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Process", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		internal SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.NVarChar);
			parameters.Add("@EmployeeName", SqlDbType.NVarChar);
			parameters.Add("@Days", SqlDbType.SmallInt);
			parameters.Add("@FromDate", SqlDbType.DateTime);
			parameters.Add("@ToDate", SqlDbType.DateTime);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@FromDate"].SourceColumn = "FromDate";
			parameters["@ToDate"].SourceColumn = "ToDate";
			parameters["@EmployeeName"].SourceColumn = "EmployeeName";
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

		public bool InsertEmployeeLeave(EmployeeLeaveProcessData accountEmployeeLeaveDetailData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeLeaveDetailData.EmployeeLeaveProcessTable.Rows[0]["VoucherID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeLeaves(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountEmployeeLeaveDetailData, "Employee_Leave_Process", insertUpdateCommand);
				string text = accountEmployeeLeaveDetailData.EmployeeLeaveProcessTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("Employee Leave Process", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Leave_Process", "VoucherID", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateEmployeeLeave(EmployeeLeaveProcessData accountEmployeeLeaveDetailData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeLeaveDetailData.EmployeeLeaveProcessTable.Rows[0]["VoucherID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeLeaves(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				if (accountEmployeeLeaveDetailData.Tables["Employee_Leave_Process"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountEmployeeLeaveDetailData, "Employee_Leave_Process", insertUpdateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeLeaveDetailData.EmployeeLeaveProcessTable.Rows[0]["VoucherID"];
				UpdateTableRowByID("Employee_Leave_Process", "EmployeeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeLeaveDetailData.EmployeeLeaveProcessTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("Employee Leave Process", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Leave_Process", "VoucherID", obj, sqlTransaction, isInsert: false);
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

		internal bool DeleteEmployeeLeaves(SqlTransaction sqlTransaction, string employeeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Leave_Process WHERE VoucherID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Leave Process", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeLeaveProcessData GetEmployeeLeave()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Leave_Process");
			EmployeeLeaveProcessData employeeLeaveProcessData = new EmployeeLeaveProcessData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeLeaveProcessData, "Employee_Leave_Process", sqlBuilder);
			return employeeLeaveProcessData;
		}

		public EmployeeLeaveProcessData GetEmployeeLeavesByEmployeeID(string employeeID)
		{
			EmployeeLeaveProcessData employeeLeaveProcessData = new EmployeeLeaveProcessData();
			string textCommand = "Select ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],ELP.*\r\n                            FROM Employee_Leave_Process ELP INNER JOIN\r\n                            Employee E ON E.EmployeeID=ELP.EmployeeID\r\n                            WHERE ELP.VoucherID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(employeeLeaveProcessData, "Employee_Leave_Process", textCommand);
			return employeeLeaveProcessData;
		}

		public bool DeleteEmployeeLeave(string employeeLeaveTypeID)
		{
			bool flag = true;
			string text = "";
			try
			{
				text = "DELETE FROM Employee_Leave_Process WHERE VoucherID = '" + employeeLeaveTypeID + "'";
				return Delete(text, null);
			}
			catch
			{
				throw;
			}
		}

		public bool IsLeaveExist(string employeeId, DateTime startDate, DateTime endDate)
		{
			string text = CommonLib.ToSqlDateTimeString(startDate);
			string text2 = CommonLib.ToSqlDateTimeString(endDate);
			bool flag = true;
			try
			{
				string exp = "SELECT COUNT(EmployeeId) FROM Employee_Activity EA \r\n\t                    INNER JOIN Employee_Leave_Request ELR ON EA.ActivityID = ELR.ActivityID\r\n                        WHERE EmployeeID = '" + employeeId + "' AND ( ( '" + text + "' BETWEEN  ELR.StartDate AND ELR.EndDate ) OR ('" + text2 + "'  BETWEEN  ELR.StartDate AND ELR.EndDate) )";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
				{
					return false;
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeLeaveProcessData GetEmployeeLeaveByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Leave_Process";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeLeaveProcessData employeeLeaveProcessData = new EmployeeLeaveProcessData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeLeaveProcessData, "Employee_Leave_Process", sqlBuilder);
			return employeeLeaveProcessData;
		}

		public DataSet GetEmployeeLeaveByFields(params string[] columns)
		{
			return GetEmployeeLeaveByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeLeaveByFields(string[] employeeLeaveTypeID, params string[] columns)
		{
			return GetEmployeeLeaveByFields(employeeLeaveTypeID, isInactive: true, columns);
		}

		public DataSet GetEmployeeLeaveByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Leave_Process");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper cmdHelper = new CommandHelper();
				sqlBuilder.AddCommandHelper(cmdHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Leave_Process", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeLeaveComboList()
		{
			return null;
		}

		public bool VoidEmployeeLeaveDetail(string activityID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = CommonLib.ToSqlDateTimeString(DateTime.Today);
				string exp = "SELECT IsApproved FROM Employee_Leave_Request WHERE ActivityID=" + activityID.ToString();
				ExecuteScalar(exp, sqlTransaction);
				exp = "UPDATE Employee_Leave_Request SET IsVoid= '" + isVoid.ToString() + "',\r\n                        ApprovedBy='" + base.DBConfig.CurrentLoginID + "',ApproveDate='" + text + "'\r\n                        WHERE ActivityID=" + activityID.ToString();
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
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

		public bool DeleteEmployeeLeaveDetail(string ActivityID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				text = "DELETE FROM Employee_Leave_Request WHERE ActivityID=" + ActivityID.ToString();
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
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

		public bool DeleteEmployeePassportDetail(string ActivityID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				text = "DELETE FROM Employee_Passport_Control WHERE ActivityID=" + ActivityID.ToString();
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
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

		public DataSet GetEmployeeLeaveAvailability(string EmployeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select e.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,obld.BalanceLeave[opening Leaves]\r\n                            ,DATEDIFF(MONTH,e.JoiningDate,GETDATE()) as Sevicemonths,\r\n                            CASE  WHEN DATEDIFF(MONTH,e.JoiningDate,GETDATE()) >   MonthGreater1  AND  DATEDIFF(MONTH,e.JoiningDate,GETDATE())< MonthLesser1  THEN AllowedDays1\r\n                             WHEN DATEDIFF(MONTH,e.JoiningDate,GETDATE()) >   MonthGreater2  AND  DATEDIFF(MONTH,e.JoiningDate,GETDATE())< MonthLesser2  THEN AllowedDays2\r\n                             WHEN DATEDIFF(MONTH,e.JoiningDate,GETDATE()) >   MonthGreater3  AND  DATEDIFF(MONTH,e.JoiningDate,GETDATE())< MonthLesser3  THEN AllowedDays3         \r\n                             ELSE 0 END AS 'AnnualAllowedDays',LT.Days AS LeaveDayswithType,(\r\n\t\t\t\t    \t\tSELECT  SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n\t\t\t\t\t        FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n\t\t\t\t\t        LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID  \r\n                            INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                            WHERE  ISNULL(isvoid,0) =0   and IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID\r\n\t\t\t\t\t        GROUP BY LT1.LeaveTypeName, E1.EmployeeID) AS TotalTaken\r\n                             FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                             LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n                             LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID WHERE eld.LeaveTypeID<>'' AND e.EmployeeID='" + EmployeeID.ToString() + "'";
			FillDataSet(dataSet, "LeaveAvailability", textCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT VoucherID [Doc Number],  EmployeeID,Note from Employee_Leave_Process ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_Leave_Process", sqlCommand);
			return dataSet;
		}

		public int GetEmployeeLeaveDays(string EmployeeID)
		{
			new DataSet();
			string exp = "SELECT Days from Employee_Leave_Process  where EmployeeID = '" + EmployeeID + "' Order By TransactionDate Desc";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return int.Parse(obj.ToString());
			}
			return 0;
		}
	}
}
