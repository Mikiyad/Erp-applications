using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobTimesheet : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		public const string EMPLOYEEID_PARM = "@EmployeeID";

		public const string MONTH_PARM = "@Month";

		public const string YEAR_PARM = "@Year";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string JOBTIMESHEET_TABLE = "Job_Timesheet";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string UNIT_PARM = "@Unit";

		private const string QUANTITY_PARM = "@Quantity";

		private const string AMOUNT_PARM = "@Amount";

		private const string FEEID_PARM = "@FeeID";

		private const string TASKID_PARM = "@TaskID";

		private const string TASKPERCENT_PARM = "@TaskPercent";

		public const string TSDATE_PARM = "@TSDate";

		public const string BEGINTIME_PARM = "@BeginTime";

		public const string ENDTIME_PARM = "@EndTime";

		public const string PAYROLLITEM_PARM = "@PayrollItemType";

		public const string RATE_PARM = "@Rate";

		private const string JOBTIMESHEETDETAIL_TABLE = "Job_Timesheet_Detail";

		public JobTimesheet(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobTimesheetText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Timesheet", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("RequestedBy", "@RequestedBy"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("Month", "@Month"), new FieldValue("Year", "@Year"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Timesheet", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobTimesheetCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobTimesheetText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobTimesheetText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@Year", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@Year"].SourceColumn = "Year";
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

		private string GetInsertUpdateJobTimesheetDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Timesheet_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("FeeID", "@FeeID"), new FieldValue("TaskID", "@TaskID"), new FieldValue("TaskPercent", "@TaskPercent"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Unit", "@Unit"), new FieldValue("Amount", "@Amount"), new FieldValue("Quantity", "@Quantity"), new FieldValue("TSDate", "@TSDate"), new FieldValue("BeginTime", "@BeginTime"), new FieldValue("EndTime", "@EndTime"), new FieldValue("PayrollItemType", "@PayrollItemType"), new FieldValue("Rate", "@Rate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobTimesheetDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobTimesheetDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobTimesheetDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@FeeID", SqlDbType.NVarChar);
			parameters.Add("@TaskID", SqlDbType.NVarChar);
			parameters.Add("@TaskPercent", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Unit", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@TSDate", SqlDbType.DateTime);
			parameters.Add("@BeginTime", SqlDbType.DateTime);
			parameters.Add("@EndTime", SqlDbType.DateTime);
			parameters.Add("@PayrollItemType", SqlDbType.NVarChar);
			parameters.Add("@Rate", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@FeeID"].SourceColumn = "FeeID";
			parameters["@TaskID"].SourceColumn = "TaskID";
			parameters["@TaskPercent"].SourceColumn = "TaskPercent";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Unit"].SourceColumn = "Unit";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TSDate"].SourceColumn = "TSDate";
			parameters["@BeginTime"].SourceColumn = "BeginTime";
			parameters["@EndTime"].SourceColumn = "EndTime";
			parameters["@PayrollItemType"].SourceColumn = "PayrollItemType";
			parameters["@Rate"].SourceColumn = "Rate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobTimesheetData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobTimesheet(JobTimesheetData jobTimesheetData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobTimesheetCommand = GetInsertUpdateJobTimesheetCommand(isUpdate);
			try
			{
				DataRow dataRow = jobTimesheetData.JobTimesheetTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Timesheet", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobTimesheetData.JobTimesheetDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobTimesheetDetailsRows(text2, text, sqlTransaction);
				}
				insertUpdateJobTimesheetCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobTimesheetData, "Job_Timesheet", insertUpdateJobTimesheetCommand)) : (flag & Insert(jobTimesheetData, "Job_Timesheet", insertUpdateJobTimesheetCommand)));
				insertUpdateJobTimesheetCommand = GetInsertUpdateJobTimesheetDetailsCommand(isUpdate: false);
				insertUpdateJobTimesheetCommand.Transaction = sqlTransaction;
				if (jobTimesheetData.Tables["Job_Timesheet_Detail"].Rows.Count > 0)
				{
					flag &= Insert(jobTimesheetData, "Job_Timesheet_Detail", insertUpdateJobTimesheetCommand);
				}
				GLData journalData = CreateJobTimesheetGLData(jobTimesheetData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				string exp = "UPDATE JT SET CompletedPercentage = ISNULL(CompletedPercentage,0) +\r\n                                ISNULL((SELECT SUM(ISNULL(TaskPercent,0)) FROM Job_Timesheet_Detail JTD WHERE JTD.SysDocID = '" + text2 + "' AND JTD.VoucherID = '" + text + "' AND JTD.TaskID = JT.TaskID),0)\r\n                                FROM Job_Task JT WHERE JT.TaskID IN (SELECT DISTINCT TaskID FROM Job_Timesheet_Detail JTD2 WHERE JTD2.SysDocID = '" + text2 + "' AND JTD2.VoucherID = '" + text + "' )";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Timesheet", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Timesheet";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Timesheet", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobTimesheet, text2, text, "Job_Timesheet", sqlTransaction);
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

		private GLData CreateJobTimesheetGLData(JobTimesheetData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.JobTimesheetTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			string text = "";
			SysDocTypes sysDocTypes = SysDocTypes.JobTimesheet;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Project Timesheet";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			string idFieldValue = dataRow["SysDocID"].ToString();
			text = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", idFieldValue, sqlTransaction).ToString();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			Hashtable hashtable3 = new Hashtable();
			ArrayList arrayList = new ArrayList();
			new Hashtable();
			new ArrayList();
			decimal num = default(decimal);
			string text2 = "";
			decimal d = default(decimal);
			DataRow dataRow3 = null;
			foreach (DataRow row in transactionData.JobTimesheetDetailTable.Rows)
			{
				decimal num2 = default(decimal);
				decimal d2 = decimal.Parse(row["Quantity"].ToString());
				decimal d3 = decimal.Parse(row["Rate"].ToString());
				string text3 = row["JobID"].ToString();
				string value = row["CostCategoryID"].ToString();
				row["VoucherID"].ToString();
				row["SysDocID"].ToString();
				int.Parse(row["RowIndex"].ToString());
				string text4 = "";
				if (hashtable.Contains(text3))
				{
					text4 = hashtable[text3].ToString();
					text2 = hashtable2[text3].ToString();
				}
				else
				{
					object jobAccountIDByLocation = new Job(base.DBConfig).GetJobAccountIDByLocation(text3, text, JobAccounts.WIPAccount, sqlTransaction);
					if (jobAccountIDByLocation != null)
					{
						text4 = jobAccountIDByLocation.ToString();
					}
					jobAccountIDByLocation = new Job(base.DBConfig).GetJobAccountIDByLocation(text3, text, JobAccounts.TimesheetAccount, sqlTransaction);
					if (jobAccountIDByLocation != null)
					{
						text2 = jobAccountIDByLocation.ToString();
					}
					if (text4 == "")
					{
						throw new CompanyException("WIP Account is not set for project : " + text3);
					}
					if (text2 == "")
					{
						throw new CompanyException("Timesheet Account is not set for project : " + text3);
					}
					hashtable2.Add(text3, text2);
					hashtable.Add(text3, text4);
				}
				num2 += Math.Round(d2 * d3, currencyDecimalPoints);
				d += num2;
				if (hashtable3.ContainsKey(text2))
				{
					num = decimal.Parse(hashtable3[text2].ToString());
					num += Math.Round(num2, currencyDecimalPoints);
					hashtable3[text2] = num;
				}
				else
				{
					hashtable3.Add(text2, Math.Round(num2, currencyDecimalPoints));
					arrayList.Add(text2);
				}
				if (num2 != 0m)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["Debit"] = Math.Abs(num2);
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text4;
					dataRow3["JobID"] = text3;
					dataRow3["CostCategoryID"] = value;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			d = Math.Round(d, currencyDecimalPoints);
			if (d != 0m)
			{
				for (int i = 0; i < hashtable3.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					text2 = arrayList[i].ToString();
					num = decimal.Parse(hashtable3[text2].ToString());
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(num);
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text2;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		public JobTimesheetData GetJobTimesheetByID(string sysDocID, string voucherID)
		{
			try
			{
				JobTimesheetData jobTimesheetData = new JobTimesheetData();
				string textCommand = "SELECT * FROM Job_Timesheet WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobTimesheetData, "Job_Timesheet", textCommand);
				if (jobTimesheetData == null || jobTimesheetData.Tables.Count == 0 || jobTimesheetData.Tables["Job_Timesheet"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM Job_Timesheet_Detail WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobTimesheetData, "Job_Timesheet_Detail", textCommand);
				return jobTimesheetData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobTimesheetDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE JT SET CompletedPercentage = ISNULL(CompletedPercentage,0) -\r\n                                ISNULL((SELECT SUM(ISNULL(TaskPercent,0)) FROM Job_Timesheet_Detail JTD WHERE JTD.SysDocID = '" + sysDocID + "' AND JTD.VoucherID = '" + voucherID + "' AND JTD.TaskID = JT.TaskID),0)\r\n                                FROM Job_Task JT WHERE JT.TaskID IN (SELECT DISTINCT TaskID FROM Job_Timesheet_Detail JTD2 WHERE JTD2.SysDocID = '" + sysDocID + "' AND JTD2.VoucherID = '" + voucherID + "' )";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "DELETE FROM Job_Timesheet_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobTimesheet(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobTimesheet(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobTimesheetDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Job_Timesheet WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Job Timesheet", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetJobTimesheetToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT *,ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [EmpName] from Job_Timesheet JT INNER JOIN Employee E ON JT.EmployeeID=E.EmployeeID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Timesheet", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Timesheet"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Amount) AS Amount FROM Job_Timesheet_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        GROUP BY SysDocID,VoucherID,JobID,CostCategoryID,FeeID,TaskID,TaskPercent,Description,TSDate,PayrollItemType,Quantity,Unit,Rate,Amount,BeginTime,EndTime,RowIndex,IsBillable,IsBilled,BilledAmount\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Timesheet_Detail", cmdText);
				dataSet.Relations.Add("JobIssueDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Timesheet"].Columns["SysDocID"],
					dataSet.Tables["Job_Timesheet"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Timesheet_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Timesheet_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobTimesheetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JID.JobID,J.JobName,JI.TransactionDate AS [Date]   \r\n                            FROM Job_Timesheet JI INNER JOIN Job_Timesheet_Detail JID ON JI.SysDocID=JID.SysDocID and JI.VoucherID=JID.VoucherID LEFT JOIN Job J ON JID.JobID=J.JobID\r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Timesheet", str);
			return dataSet;
		}
	}
}
