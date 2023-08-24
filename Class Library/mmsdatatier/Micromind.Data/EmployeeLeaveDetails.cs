using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeLeaveDetails : StoreObject
	{
		private const string EMPLOYEELEAVEDETAIL_TABLE = "Employee_Leave_Detail";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string LEAVETYPEID_PARM = "@LeaveTypeID";

		private const string REMARKS_PARM = "@Remarks";

		private const string DAYS_PARM = "@Days";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeLeaveDetails(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Detail", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("LeaveTypeID", "@LeaveTypeID", isUpdateConditionField: true), new FieldValue("Remarks", "@Remarks"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Days", "@Days"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Leave_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@LeaveTypeID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Days", SqlDbType.SmallInt);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@LeaveTypeID"].SourceColumn = "LeaveTypeID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertEmployeeLeave(EmployeeLeaveDetailData accountEmployeeLeaveDetailData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeLeaveDetailData.EmployeeLeaveDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeLeaves(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountEmployeeLeaveDetailData, "Employee_Leave_Detail", insertUpdateCommand);
				string text = accountEmployeeLeaveDetailData.EmployeeLeaveDetailTable.Rows[0]["LeaveTypeID"].ToString();
				AddActivityLog("Employee Leave", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Leave_Detail", "LeaveTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeLeave(EmployeeLeaveDetailData accountEmployeeLeaveDetailData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeLeaveDetailData.EmployeeLeaveDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeLeaves(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountEmployeeLeaveDetailData, "Employee_Leave_Detail", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeLeaveDetailData.EmployeeLeaveDetailTable.Rows[0]["LeaveTypeID"];
				UpdateTableRowByID("Employee_Leave_Detail", "LeaveTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeLeaveDetailData.EmployeeLeaveDetailTable.Rows[0]["LeaveTypeID"].ToString();
				AddActivityLog("Employee Leave", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Leave_Detail", "LeaveTypeID", obj, sqlTransaction, isInsert: false);
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
				string commandText = "DELETE FROM Employee_Leave_Detail WHERE EmployeeID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Leave", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeLeaveDetailData GetEmployeeLeave()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Leave_Detail");
			EmployeeLeaveDetailData employeeLeaveDetailData = new EmployeeLeaveDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeLeaveDetailData, "Employee_Leave_Detail", sqlBuilder);
			return employeeLeaveDetailData;
		}

		public EmployeeLeaveDetailData GetEmployeeLeavesByEmployeeID(string employeeID)
		{
			EmployeeLeaveDetailData employeeLeaveDetailData = new EmployeeLeaveDetailData();
			string textCommand = "Select EmployeeID,EBD.LeaveTypeID,LeaveTypeName,Remarks,EBD.Days \r\n                            FROM Employee_Leave_Detail EBD INNER JOIN\r\n                            Leave_Type ON Leave_Type.LeaveTypeID=EBD.LeaveTypeID\r\n                            WHERE EmployeeID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(employeeLeaveDetailData, "Employee_Leave_Detail", textCommand);
			return employeeLeaveDetailData;
		}

		public bool DeleteEmployeeLeave(string employeeLeaveTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Leave_Detail WHERE LeaveTypeID = '" + employeeLeaveTypeID + "'";
				return Delete(commandText, null);
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
				string exp = "SELECT COUNT(EmployeeId) FROM Employee_Activity EA \r\n\t                    INNER JOIN Employee_Leave_Request ELR ON EA.ActivityID = ELR.ActivityID\r\n                        WHERE EmployeeID = '" + employeeId + "' AND ((CAST ( '" + text + "' AS DATE)  BETWEEN  CAST(ELR.StartDate AS DATE)  AND CAST(ELR.EndDate  AS DATE)) OR (CAST ('" + text2 + "' AS DATE)  BETWEEN  CAST( ELR.StartDate AS DATE)  AND CAST( ELR.EndDate AS DATE)) )";
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

		public EmployeeLeaveDetailData GetEmployeeLeaveByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "LeaveTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Leave_Detail";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeLeaveDetailData employeeLeaveDetailData = new EmployeeLeaveDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeLeaveDetailData, "Employee_Leave_Detail", sqlBuilder);
			return employeeLeaveDetailData;
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
			sqlBuilder.AddTable("Employee_Leave_Detail");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "LeaveTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Leave_Detail";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Leave_Detail", sqlBuilder);
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

		public DataSet GetEmployeeLeaveAvailability(string EmployeeID, DateTime AsonDate, DateTime ToDate, string LeaveTypeID)
		{
			DataSet dataSet = new DataSet();
			SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee", "ContractType", "EmployeeID", EmployeeID, sqlTransaction);
			SqlTransaction sqlTransaction2 = base.DBConfig.StartNewTransaction();
			object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee_Type", "LeaveSelection", "TypeID", fieldValue, sqlTransaction2);
			string text = CommonLib.ToSqlDateTimeString(new DateTime(AsonDate.Year, AsonDate.Month, AsonDate.Day, 11, 59, 59));
			string text2 = CommonLib.ToSqlDateTimeString(new DateTime(AsonDate.Year, AsonDate.Month, AsonDate.Day, 0, 0, 0));
			string text3 = CommonLib.ToSqlDateTimeString(new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, 11, 59, 59));
			string text4 = "";
			string text5 = "";
			if (fieldValue2 != null)
			{
				if (fieldValue2.ToString().Trim() == "OA" || fieldValue2.ToString() == "")
				{
					text5 = "On Account";
					text4 = "select e.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,LT.LeaveTypeName,SUM(obld.LeaveTaken) as [openingLeavesTaken],\r\n                            LT.IsAnnual,DATEDIFF(MONTH,e.JoiningDate,'" + text + "') as Sevicemonths,  \r\n                            CASE WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND MonthGreater1 > DATEDIFF(MONTH,e.JoiningDate,'" + text + "') THEN 'True' ELSE 'False' END AS 'AnnualEligible',                       \r\n                            CASE\r\n                            -- WHEN lt.IsAnnual='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  AND MonthLesser1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE()))  THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser1+MonthGreater1))* AllowedDays1\r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "') AND DATEPART(DAY,JoiningDate)-DATEPART(DAY,'" + text + "')<=0)  THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "'))* AllowedDays1\r\n                           WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "')) AND DATEPART(DAY,JoiningDate)-DATEPART(DAY,'" + text + "')>0 THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "')-1)* AllowedDays1\r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser1 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text + "'))* AllowedDays1\r\n                            -- WHEN lt.IsAnnual='1' AND (MonthGreater1 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  OR DATEDIFF(MONTH,e.JoiningDate,GETDATE())> MonthLesser1  )  THEN (MonthLesser1-MonthGreater1)* AllowedDays1\r\n                            End AS '1SET',\r\n                            CASE \r\n                            --WHEN lt.IsAnnual='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  OR MonthLesser2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())) THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-(MonthLesser2+MonthGreater2)) * AllowedDays2 \r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "') AND DATEPART(DAY,JoiningDate)-DATEPART(DAY,'" + text + "')<=0)   THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "'))* AllowedDays2\r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND DATEPART(DAY,JoiningDate)-DATEPART(DAY,'" + text + "')>0\r\n                            AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "')-1)* AllowedDays2\r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser2 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text + "'))* AllowedDays2                           \r\n                           --WHEN lt.IsAnnual='1' AND (MonthGreater2 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())  OR DATEDIFF(MONTH,e.JoiningDate,GETDATE())> MonthLesser2  )  THEN (MonthLesser2-MonthGreater2)* AllowedDays2\r\n                            End AS '2SET',\r\n                            CASE\r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "')AND DATEPART(DAY,JoiningDate)-DATEPART(DAY,'" + text + "')<=0 )  THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "'))* AllowedDays3\r\n                            WHEN lt.IsAnnual='1' AND LT.IsCumulative='1' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "') AND DATEPART(DAY,JoiningDate)-DATEPART(DAY,'" + text + "')>0)   THEN (DATEDIFF(MONTH,JoiningDate,'" + text + "')-1)* AllowedDays3\r\n                            WHEN lt.IsAnnual='1' AND ISNULL (LT.IsCumulative,'0')='0' AND (MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,'" + text + "')  AND  MonthLesser3 >= DATEDIFF(MONTH,e.JoiningDate,'" + text + "'))  THEN (DATEDIFF(MONTH,DATEADD(yy, DATEDIFF(yy,0,getdate()), 0),'" + text + "'))* AllowedDays3                           \r\n                           -- WHEN lt.IsAnnual='1' AND MonthGreater3 < DATEDIFF(MONTH,e.JoiningDate,GETDATE())   THEN (DATEDIFF(MONTH,JoiningDate,GETDATE())-MonthGreater3) * AllowedDays3 \r\n                            End AS '3SET',\r\n                            LT.Days AS LeaveDayswithType,(\r\n                            SELECT  SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n                            FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n                            LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID \r\n                            --LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID AND OPBLD1.LeaveTypeID=ELR1.LeaveTypeID\r\n\t\t\t\t\t\t\t--LEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID \r\n                            INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                            WHERE  ISNULL(isvoid,0) =0  --AND ELR1.StartDate>= ISNULL(OPBL1.BatchDate,ELR1.StartDate) \r\n                            AND IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID\r\n                            GROUP BY LT1.LeaveTypeName, E1.EmployeeID) AS TotalTaken,0.0 AS TotalLeaves,0.0 AS LeavesRemaining,'" + text5 + "' AS Basedon\r\n                            FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                            LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n                            LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID WHERE eld.LeaveTypeID<>'' AND e.EmployeeID='" + EmployeeID.ToString() + "' \r\n                            GROUP BY e.EmployeeID,FirstName,MiddleName,LastName,eld.LeaveTypeID,LT.LeaveTypeName,e.JoiningDate,lt.IsAnnual,LT.IsCumulative,MonthGreater1,MonthLesser1,AllowedDays1,MonthGreater2,MonthLesser2,AllowedDays2\r\n\t\t\t\t\t\t\t,MonthGreater3,MonthLesser3,AllowedDays3,LT.Days,LT.LeaveTypeID";
					FillDataSet(dataSet, "LeaveAvailability", text4);
				}
				else if (fieldValue2.ToString().Trim() == "CD")
				{
					text5 = "Calendar Year";
					text4 = "select e.EmployeeID, ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],eld.LeaveTypeID,LT.LeaveTypeName,SUM(obld.LeaveTaken) as [openingLeavesTaken]\r\n                            ,DATEDIFF(MONTH,e.JoiningDate,GETDATE()) as Sevicemonths, \r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.Days End AS 'AnnualAllowedDays',\r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.FromDate END AS 'FromDate',\r\n\t\t\t\t\t\t\tCASE WHEN lt.IsAnnual='1' THEN elp.ToDate End AS 'ToDate',\r\n                             LT.Days AS LeaveDayswithType,(\r\n\t\t\t\t    \t\tSELECT  SUM(DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken\r\n\r\n\t\t\t\t\t        FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n\t\t\t\t\t        LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID  \r\n                            LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID\r\n\t\t\t\t\t\t\tLEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID\r\n                            INNER JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID \r\n                            WHERE  ISNULL(isvoid,0) =0 --AND ELR1.StartDate>= ISNULL(OPBL1.BatchDate,ELR1.StartDate) \r\n                            AND IsApproved=1 and E1.EmployeeID=E.EmployeeID AND LT1.LeaveTypeID=LT.LeaveTypeID --AND LT1.ActivateHC=1\r\n\t\t\t\t\t        GROUP BY LT1.LeaveTypeName, E1.EmployeeID) AS TotalTaken,\r\n                            (select count(HD1.FromDate) from Holiday_Calendar_Detail HD1\r\n                             LEFT JOIN EMPLOYEE E2 ON HD1.CalendarID=e.CalendarID \r\n                             LEFT JOIN  Employee_Activity EA2 ON EA2.EmployeeID = E2.EmployeeID\r\n                             LEFT JOIN Employee_Leave_Request ELR2 ON EA2.ActivityID = ELR2.ActivityID\r\n                             INNER JOIN Leave_Type LT2 ON LT2.LeaveTypeID=ELR2.LeaveTypeID --AND LT2.ActivateHC=1  \r\n                             and (HD1.FromDate BETWEEN ELR2.StartDate AND ELR2.EndDate\r\n                             )WHERE  ISNULL(isvoid,0) =0 and ISNULL(IsApproved,0)=1  and E2.EmployeeID=E.EmployeeID AND LT2.LeaveTypeID=LT.LeaveTypeID GROUP BY LT2.LeaveTypeName, E2.EmployeeID)AS ToLessTaken,\r\n                             0.0 AS TotalLeaves,0.0 AS LeavesRemaining,'" + text5 + "' AS Basedon,CASE WHEN lt.IsAnnual='1' THEN 'True' ELSE 'False' END AS IsAnnual\r\n                             FROM Employee e LEFT join Employee_Type_Detail eld ON e.ContractType=eld.TypeID\r\n                             LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=eld.LeaveTypeID\r\n\t\t\t\t\t\t\t LEFT JOIN Employee_Leave_Process elp ON e.EmployeeID=elp.EmployeeID AND LT.IsAnnual='1'  AND elp.FromDate <='" + text + "'                             \r\n                             LEFT JOIN Opening_Balance_Leave_Detail  obld ON e.EmployeeID=obld.EmployeeID AND eld.LeaveTypeID=obld.LeaveTypeID WHERE eld.LeaveTypeID<>'' AND e.EmployeeID='" + EmployeeID.ToString() + "'\r\n                            GROUP BY e.EmployeeID,FirstName,MiddleName,LastName,eld.LeaveTypeID,LT.LeaveTypeName,e.JoiningDate,lt.IsAnnual,LT.IsCumulative,MonthGreater1,MonthLesser1,AllowedDays1,MonthGreater2,MonthLesser2,AllowedDays2\r\n\t\t\t\t\t\t\t,MonthGreater3,MonthLesser3,AllowedDays3,LT.Days,LT.LeaveTypeID,elp.Days,elp.FromDate,elp.ToDate,E.CalendarID,elp.RowIndex,elp.VoucherID ORDER by LT.LeaveTypeID,elp.FromDate,elp.ToDate asc";
					FillDataSet(dataSet, "LeaveAvailability", text4);
					new DataSet();
					text4 = "SELECT ISNULL (ELR1.ActualLeaveDays,DATEDIFF(DAY,ELR1.StartDate,ELR1.EndDate)+1) as DaysTaken,\r\n                            0 AS ToLessTaken\r\n                            ,ELP1.FromDate,ELP1.ToDate,CASE WHEN LT1.IsAnnual='1' THEN \r\n                            ELP1.DAYS ELSE LT1.DAYS END  AS DaysAllowed,LT1.LeaveTypeID,CASE WHEN LT1.IsAnnual='1' THEN 'True' ELSE 'False' END AS IsAnnual                          \r\n\t\t\t\t\t        FROM Employee_Leave_Request ELR1 LEFT  JOIN Employee_Resumption ER1 ON ER1.LeaveID = ELR1.ActivityID\r\n\t\t\t\t\t        LEFT JOIN  Employee_Activity EA1 ON EA1.ActivityID = ELR1.ActivityID\r\n                            LEFT JOIN Employee E1 ON E1.EmployeeID=EA1.EmployeeID  \r\n                            LEFT JOIN   Opening_Balance_Leave_Detail OPBLD1 ON OPBLD1.EmployeeID=E1.EmployeeID\r\n\t\t\t\t\t\t\tLEFT JOIN   Opening_Balance_Leave OPBL1 ON OPBL1.BatchID=OPBLD1.BatchID AND OPBL1.SysDocID=OPBLD1.SysDocID\r\n\t\t\t\t\t\t\tLEFT JOIN Employee_Leave_Process ELP1 ON E1.EmployeeID=ELP1.EmployeeID \t\t\t\t\t\t\t\r\n                            LEFT JOIN Leave_Type LT1 ON LT1.LeaveTypeID=ELR1.LeaveTypeID  --AND LT1.ActivateHC=1\r\n                            LEFT JOIN Holiday_Calendar_Detail HC ON  HC.CalendarID=E1.CalendarID\r\n                            WHERE  ISNULL(isvoid,0) =0 AND CONVERT(VARCHAR(10),ELR1.StartDate,111)>= CONVERT(VARCHAR(10),ISNULL(OPBL1.BatchDate,ELR1.StartDate),111)\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tAND CONVERT(VARCHAR(10),ELR1.StartDate,111) >= CONVERT(VARCHAR(10),ISNULL( ELP1.FromDate,ELR1.StartDate) ,111)\r\n                            AND CONVERT(VARCHAR(10),ELR1.EndDate,111) <= CONVERT(VARCHAR(10),ISNULL(ELP1.ToDate,ELR1.EndDate),111)                                      \r\n\t\t\t\t\t\t    AND IsApproved=1 AND LT1.LeaveTypeID=ELR1.LeaveTypeID  AND E1.EmployeeID='" + EmployeeID.ToString() + "'  GROUP BY \r\n                            ELR1.StartDate,ELR1.EndDate,ELP1.FromDate,ELP1.ToDate,ELP1.VoucherID,ELP1.Days,LT1.IsAnnual,LT1.LeaveTypeID,LT1.DAYS,E1.CalendarID,HC.CalendarID,ELR1.ActualLeaveDays,ELP1.RowIndex ";
					FillDataSet(dataSet, "LeavesTaken", text4);
					text4 = "SELECT  Distinct e.EmployeeID,(DATEDIFF(DAY,'" + text2 + "','" + text3 + "')),\r\n\t\t\t\t\t\t\t((DATEDIFF(DAY,'" + text2 + "','" + text3 + "'))-(select count(DISTINCT HD1.FromDate) from Holiday_Calendar_Detail HD1  where HD1.CalendarID=e.CalendarID \r\n                            and (HD1.FromDate BETWEEN '" + text2 + "' AND '" + text3 + "')))+1 AS ActualLeaveDays,E.CalendarID\r\n\t\t\t\t\t\t\t From Employee E INNER JOIN Holiday_Calendar_Detail HC ON E.CalendarID=HC.CalendarID \r\n                             LEFT JOIN Employee_Type ET ON ET.TypeID=E.ContractType \r\n\t\t\t\t\t\t\t LEFT JOIN Employee_Type_Detail ETD ON ETD.TypeID=ET.TypeID\r\n\t\t\t\t\t\t\t LEFT JOIN Leave_Type LT ON LT.LeaveTypeID=ETD.LeaveTypeID AND LT.ActivateHC=1 \r\n                            where E.EmployeeID='" + EmployeeID.ToString() + "' and HC.FromDate BETWEEN '" + text2 + "' AND '" + text3 + "'";
					if (LeaveTypeID != "")
					{
						text4 = text4 + " AND LT.LeaveTypeID='" + LeaveTypeID + "'";
					}
					FillDataSet(dataSet, "ActualLeave", text4);
				}
				if (fieldValue2.ToString() != "" && fieldValue2 != null && dataSet.Tables[0].Rows.Count > 0)
				{
					dataSet.Tables[0].Rows[0]["Basedon"] = text5;
				}
			}
			return dataSet;
		}

		public bool IsLeaveProcessDateExists(DateTime OnDate, string employeeId)
		{
			try
			{
				string exp = "SELECT COUNT(EmployeeId) FROM  Employee_Leave_Process ELR \r\n                        WHERE EmployeeID = '" + employeeId + "' AND ( ( '" + OnDate + "' BETWEEN  ELR.FromDate AND ELR.ToDate ) OR ('" + OnDate + "'  BETWEEN  ELR.FromDate AND ELR.ToDate) )";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				return false;
			}
			catch
			{
				throw;
			}
		}

		public bool IsDocNoExist(string DocNo)
		{
			bool flag = true;
			try
			{
				string exp = "SELECT DocNumber FROM Employee_Leave_Request ELR\r\n                        WHERE DocNumber ='" + DocNo + "'";
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
	}
}
