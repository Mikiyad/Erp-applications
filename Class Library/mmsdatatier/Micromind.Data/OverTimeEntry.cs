using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class OverTimeEntry : StoreObject
	{
		public const string SYSDOCID_PARM = "@SysDocID";

		public const string DOCID_PARM = "@VoucherID";

		public const string APPROVEDBY_PARM = "@ApprovedBy";

		public const string APPROVALDATE_PARM = "@ApprovalDate";

		public const string MONTH_PARM = "@Month";

		public const string YEAR_PARM = "@Year";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string OVERTIMEENTRY_TABLE = "OverTimeEntry";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string EMPLOYEENAME_PARM = "@EmployeeName";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string PAYROLLPERIOD_PARM = "@PayrollPeriod";

		private const string WORKDATE_PARM = "@WorkDate";

		private const string FROMTIME_PARM = "@FromTime";

		private const string TOTIME_PARM = "@ToTime";

		private const string HOURS_PARM = "@Hours";

		private const string OTHOURS_PARM = "@OTHours";

		private const string OTTYPE_PARM = "@OTType";

		private const string OTRATE_PARM = "@OTRate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string AMOUNT_PARM = "@Amount";

		public const string LEAVEDAYS_PARM = "@LeaveDays";

		private const string REMARKS_PARM = "@Remarks";

		private const string OVERTIMEENTRYDETAIL_TABLE = "OverTimeEntry_Detail";

		public OverTimeEntry(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateOverTimeEntryText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("OverTimeEntry", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Month", "@Month"), new FieldValue("Year", "@Year"), new FieldValue("CreatedBy", "@CreatedBy"), new FieldValue("DateCreated", "@DateCreated"), new FieldValue("ApprovedBy", "@ApprovedBy"), new FieldValue("ApprovalDate", "@ApprovalDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("OverTimeEntry", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateOverTimeEntryCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateOverTimeEntryText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateOverTimeEntryText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@Year", SqlDbType.NVarChar);
			parameters.Add("@CreatedBy", SqlDbType.NVarChar);
			parameters.Add("@DateCreated", SqlDbType.DateTime);
			parameters.Add("@ApprovedBy", SqlDbType.NVarChar);
			parameters.Add("@ApprovalDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@Year"].SourceColumn = "Year";
			parameters["@CreatedBy"].SourceColumn = "CreatedBy";
			parameters["@DateCreated"].SourceColumn = "DateCreated";
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

		private string GetInsertUpdateOverTimeEntryDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("OverTimeEntry_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("EmployeeName", "@EmployeeName"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("PayrollPeriod", "@PayrollPeriod"), new FieldValue("WorkDate", "@WorkDate"), new FieldValue("FromTime", "@FromTime"), new FieldValue("ToTime", "@ToTime"), new FieldValue("Hours", "@Hours"), new FieldValue("OTHours", "@OTHours"), new FieldValue("OTType", "@OTType"), new FieldValue("OTRate", "@OTRate"), new FieldValue("Amount", "@Amount"), new FieldValue("LeaveDays", "@LeaveDays"), new FieldValue("Remarks", "@Remarks"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateOverTimeEntryDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateOverTimeEntryDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateOverTimeEntryDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeName", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@PayrollPeriod", SqlDbType.DateTime);
			parameters.Add("@WorkDate", SqlDbType.DateTime);
			parameters.Add("@FromTime", SqlDbType.DateTime);
			parameters.Add("@ToTime", SqlDbType.DateTime);
			parameters.Add("@OTHours", SqlDbType.Float);
			parameters.Add("@Hours", SqlDbType.Float);
			parameters.Add("@OTType", SqlDbType.NVarChar);
			parameters.Add("@OTRate", SqlDbType.Money);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@LeaveDays", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@EmployeeName"].SourceColumn = "EmployeeName";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@PayrollPeriod"].SourceColumn = "PayrollPeriod";
			parameters["@WorkDate"].SourceColumn = "WorkDate";
			parameters["@FromTime"].SourceColumn = "FromTime";
			parameters["@ToTime"].SourceColumn = "ToTime";
			parameters["@OTHours"].SourceColumn = "OTHours";
			parameters["@Hours"].SourceColumn = "Hours";
			parameters["@OTType"].SourceColumn = "OTType";
			parameters["@OTRate"].SourceColumn = "OTRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@LeaveDays"].SourceColumn = "LeaveDays";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(OverTimeEntryData journalData)
		{
			return true;
		}

		public bool InsertUpdateOverTimeEntry(OverTimeEntryData overTimeEntryData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateOverTimeEntryCommand = GetInsertUpdateOverTimeEntryCommand(isUpdate);
			try
			{
				DataRow dataRow = overTimeEntryData.OverTimeEntryTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("OverTimeEntry", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in overTimeEntryData.OverTimeEntryDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
				}
				if (isUpdate)
				{
					flag &= DeleteOverTimeEntryDetailsRows(text, sqlTransaction);
				}
				insertUpdateOverTimeEntryCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(overTimeEntryData, "OverTimeEntry", insertUpdateOverTimeEntryCommand)) : (flag & Insert(overTimeEntryData, "OverTimeEntry", insertUpdateOverTimeEntryCommand)));
				insertUpdateOverTimeEntryCommand = GetInsertUpdateOverTimeEntryDetailsCommand(isUpdate: false);
				insertUpdateOverTimeEntryCommand.Transaction = sqlTransaction;
				if (overTimeEntryData.OverTimeEntryDetailTable.Rows.Count > 0)
				{
					flag &= Insert(overTimeEntryData, "OverTimeEntry_Detail", insertUpdateOverTimeEntryCommand);
				}
				if (flag)
				{
					flag &= UpdateTableRowInsertUpdateInfo("OverTimeEntry", "SysDocID", text2, "VoucherID", text, sqlTransaction, isInsert: false);
					string entityName = "OverTime Entry";
					flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
					if (!isUpdate)
					{
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "OverTimeEntry", "VoucherID", sqlTransaction);
					}
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.OverTimeEntry, text2, text, "OverTimeEntry", sqlTransaction);
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

		public OverTimeEntryData GetOverTimeEntryByID(string sysDocID, string voucherID)
		{
			try
			{
				OverTimeEntryData overTimeEntryData = new OverTimeEntryData();
				string textCommand = "SELECT * FROM OverTimeEntry WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(overTimeEntryData, "OverTimeEntry", textCommand);
				if (overTimeEntryData == null || overTimeEntryData.Tables.Count == 0 || overTimeEntryData.Tables["OverTimeEntry"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM OverTimeEntry_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(overTimeEntryData, "OverTimeEntry_Detail", textCommand);
				return overTimeEntryData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOverTimeEntryGroupByJob(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = $"\r\nSelect JobID,CostCategoryID,EmployeeID, isnull(SUM([Hours]),0) as [Hours],isnull(SUM(OTHours),0) as OTHours,(isnull(SUM([Hours]),0)+isnull(SUM(OTHours),0)) as TotalHours\r\n from OverTimeEntry_Detail  inner join OverTimeEntry on OverTimeEntry.SysDocID=OverTimeEntry_Detail.SysDocID\r\n and  OverTimeEntry.VoucherID=OverTimeEntry_Detail.VoucherID where  OverTimeEntry.SysDocID in({sysDocID})and OverTimeEntry.VoucherID in ({voucherID})\r\n  Group by JobID ,EmployeeID,CostCategoryID";
				FillDataSet(dataSet, "OverTimeEntry", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public OverTimeEntryData GetOverTimeEntry(string employeeID, int year, int month)
		{
			try
			{
				OverTimeEntryData overTimeEntryData = new OverTimeEntryData();
				string textCommand = "Select Distinct OverTimeEntry.* from OverTimeEntry Inner Join  OverTimeEntry_Detail on OverTimeEntry.SysDocID = OverTimeEntry_Detail.SysDocID and OverTimeEntry.VoucherID = OverTimeEntry_Detail.VoucherID  where OverTimeEntry_Detail.EmployeeID ='" + employeeID + "' And OverTimeEntry.Year=" + year + "  And  OverTimeEntry.Month=" + month;
				FillDataSet(overTimeEntryData, "OverTimeEntry", textCommand);
				if (overTimeEntryData == null || overTimeEntryData.Tables.Count == 0 || overTimeEntryData.Tables["OverTimeEntry"].Rows.Count == 0)
				{
					return null;
				}
				string str = overTimeEntryData.Tables["OverTimeEntry"].Rows[0]["VoucherID"].ToString();
				textCommand = "SELECT *\r\n                        FROM OverTimeEntry_Detail WHERE VoucherID='" + str + "'";
				FillDataSet(overTimeEntryData, "OverTimeEntry_Detail", textCommand);
				return overTimeEntryData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteOverTimeEntryDetailsRows(string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM OverTimeEntry_Detail WHERE VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidOverTimeEntry(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteOverTimeEntry(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteOverTimeEntryDetailsRows(voucherID, sqlTransaction);
				text = "DELETE FROM OverTimeEntry WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("OverTime Entry", voucherID, activityType, sqlTransaction);
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

		public DataSet GetOverTimeEntryToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.* FROM OverTimeEntry IA\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "OverTimeEntry", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["OverTimeEntry"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT OD.*, SUM(Amount) AS Amount, WorkLocationName, OverTimeName, OT.AccountID, OT.Note, JobName FROM OverTimeEntry_Detail OD\r\n                        LEFT JOIN Work_Location WL ON OD.LocationID=WL.WorkLocationID\r\n                        LEFT JOIN Employee_OverTime OT ON OD.OTType=OT.OverTimeID\r\n                        LEFT JOIN Job ON OD.JobID=job.JobID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                         GROUP BY SysDocID,VoucherID,Amount,RowIndex,JobName,OverTimeName, OT.AccountID, OT.Note,OD.CostCategoryID, OD.EmployeeID,WL.WorkLocationID,WL.WorkLocationName, OD.EmployeeName, OD.FromTime, OD.JobID, OD.Hours, OD.LeaveDays,OD.LocationID, OD.OTRate, OD.OTType, OD.PayrollPeriod, OD.ToTime, OD.WorkDate, OD.OTHours,OD.Remarks \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "OverTimeEntry_Detail", cmdText);
				dataSet.Relations.Add("OverTimeEntryDetail", new DataColumn[2]
				{
					dataSet.Tables["OverTimeEntry"].Columns["SysDocID"],
					dataSet.Tables["OverTimeEntry"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["OverTimeEntry_Detail"].Columns["SysDocID"],
					dataSet.Tables["OverTimeEntry_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOverTimeEntryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string EmployeeIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT DISTINCT OTD.EmployeeID,FirstName + ' ' + LastName AS [EmployeeName],E.SponsorID,S.SponsorName, OTD.PayrollPeriod, OTD.FromTime, OTType, OTD.ToTime, OTHours, OTRate\r\n                            FROM Employee E   \r\n                            INNER JOIN OverTimeEntry_Detail OTD ON E.EmployeeID=OTD.EmployeeID\r\n                            INNER JOIN OverTimeEntry OT ON OT.VoucherID = OTD.VoucherID\r\n\t\t\t\t\t\t\tLEFT JOIN Sponsor S On E.SponsorID=S.SponsorID\r\n                            WHERE OTD.PayrollPeriod BETWEEN '" + text + "' AND '" + text2 + "'";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND OTD.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND OTD.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
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
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
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
			str += " ORDER BY OTD.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "OverTimeEntry_Detail", str);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID,VoucherID,ApprovalDate FROM OverTimeEntry ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "OverTimeEntry", sqlCommand);
			return dataSet;
		}

		public DataSet GetOverTimeByPeriod(int Month, int Year)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand($"SELECT SysDocID ,VoucherID ,ApprovalDate,UPPER( Left(DateName( month , DateAdd( month , {Month} , 0 ) - 1 ),3)) as Month,Year FROM OverTimeEntry  Where Month={Month} And Year={Year}");
			FillDataSet(dataSet, "OverTimeEntry", sqlCommand);
			return dataSet;
		}

		public DataSet GetApprovedList()
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT SysDocID,VoucherID,Month,Year,ApprovalDate FROM OverTimeEntry WHERE ISNULL(ApprovedBy,'')<>''");
			FillDataSet(dataSet, "OverTimeEntry", sqlCommand);
			return dataSet;
		}

		public bool IsValidEntry(string empNo, DateTime date)
		{
			object obj = null;
			string exp = $" SELECT EmployeeID FROM OverTimeEntry T1\r\n                         INNER JOIN OverTimeEntry_Detail T2 ON T1.VoucherID = T2.VoucherID \r\n                         WHERE EmployeeID ='{empNo}' AND Year(T2.WorkDate) = {date.Year} AND \r\n                         Month(T2.WorkDate) = {date.Month}and   Day(T2.WorkDate) = {date.Day}";
			try
			{
				obj = ExecuteScalar(exp);
			}
			catch
			{
				throw;
			}
			return obj != null;
		}
	}
}
