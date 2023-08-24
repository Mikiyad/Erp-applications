using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobExpenseIssue : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string JOBEXPENSEISSUE_TABLE = "Job_Expense_Issue";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string QUANTITY_PARM = "@Quantity";

		private const string AMOUNT_PARM = "@Amount";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string REMARKS_PARM = "@Remarks";

		private const string JOBEXPENSEISSUEDETAIL_TABLE = "Job_Expense_Issue_Detail";

		public JobExpenseIssue(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobExpenseIssueText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Expense_Issue", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Expense_Issue", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobExpenseIssueCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobExpenseIssueText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobExpenseIssueText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
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

		private string GetInsertUpdateJobExpenseIssueDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Expense_Issue_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Amount", "@Amount"), new FieldValue("Quantity", "@Quantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobExpenseIssueDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobExpenseIssueDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobExpenseIssueDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@UnitPrice", SqlDbType.Money);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobExpenseIssueData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobExpenseIssue(JobExpenseIssueData jobExpenseIssueData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobExpenseIssueCommand = GetInsertUpdateJobExpenseIssueCommand(isUpdate);
			try
			{
				DataRow dataRow = jobExpenseIssueData.JobExpenseIssueTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Expense_Issue", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobExpenseIssueData.JobExpenseIssueDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobExpenseIssueDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateJobExpenseIssueCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobExpenseIssueData, "Job_Expense_Issue", insertUpdateJobExpenseIssueCommand)) : (flag & Insert(jobExpenseIssueData, "Job_Expense_Issue", insertUpdateJobExpenseIssueCommand)));
				insertUpdateJobExpenseIssueCommand = GetInsertUpdateJobExpenseIssueDetailsCommand(isUpdate: false);
				insertUpdateJobExpenseIssueCommand.Transaction = sqlTransaction;
				if (jobExpenseIssueData.Tables["Job_Expense_Issue_Detail"].Rows.Count > 0)
				{
					flag &= Insert(jobExpenseIssueData, "Job_Expense_Issue_Detail", insertUpdateJobExpenseIssueCommand);
				}
				GLData journalData = CreateJobExpenseIssueGLData(jobExpenseIssueData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Expense_Issue", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Expense Issue";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Expense_Issue", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobExpenseIssue, sysDocID, text, "Job_Expense_Issue", sqlTransaction);
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

		private GLData CreateJobExpenseIssueGLData(JobExpenseIssueData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.JobExpenseIssueTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.JobExpenseIssue;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Project Expense";
			dataRow2["Note"] = dataRow["Description"];
			string idFieldValue = dataRow["SysDocID"].ToString();
			string locationID = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", idFieldValue, sqlTransaction).ToString();
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			new Hashtable();
			new ArrayList();
			decimal num = default(decimal);
			string text = "";
			decimal d = default(decimal);
			DataRow dataRow3;
			foreach (DataRow row in transactionData.JobExpenseIssueDetailTable.Rows)
			{
				decimal num2 = default(decimal);
				decimal d2 = decimal.Parse(row["Quantity"].ToString());
				decimal d3 = decimal.Parse(row["UnitPrice"].ToString());
				string idFieldValue2 = row["ExpenseID"].ToString();
				string text2 = row["JobID"].ToString();
				string value = row["CostCategoryID"].ToString();
				row["VoucherID"].ToString();
				row["SysDocID"].ToString();
				int.Parse(row["RowIndex"].ToString());
				text = new Databases(base.DBConfig).GetFieldValue("Expense_Code", "AccountID", "ExpenseID", idFieldValue2, sqlTransaction).ToString();
				string value2 = "";
				object jobAccountIDByLocation = new Job(base.DBConfig).GetJobAccountIDByLocation(text2, locationID, JobAccounts.WIPAccount, sqlTransaction);
				if (jobAccountIDByLocation != null)
				{
					value2 = jobAccountIDByLocation.ToString();
				}
				num2 += Math.Round(d2 * d3, currencyDecimalPoints);
				d += num2;
				if (hashtable.ContainsKey(text))
				{
					num = decimal.Parse(hashtable[text].ToString());
					num += Math.Round(num2, currencyDecimalPoints);
					hashtable[text] = num;
				}
				else
				{
					hashtable.Add(text, Math.Round(num2, currencyDecimalPoints));
					arrayList.Add(text);
				}
				if (num2 != 0m)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["Debit"] = Math.Abs(num2);
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = value2;
					dataRow3["JobID"] = text2;
					dataRow3["CostCategoryID"] = value;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			d = Math.Round(d, currencyDecimalPoints);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			if (d != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					text = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text].ToString());
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(num);
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		public JobExpenseIssueData GetJobExpenseIssueByID(string sysDocID, string voucherID)
		{
			try
			{
				JobExpenseIssueData jobExpenseIssueData = new JobExpenseIssueData();
				string textCommand = "SELECT * FROM Job_Expense_Issue WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobExpenseIssueData, "Job_Expense_Issue", textCommand);
				if (jobExpenseIssueData == null || jobExpenseIssueData.Tables.Count == 0 || jobExpenseIssueData.Tables["Job_Expense_Issue"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Job_Expense_Issue_Detail TD INNER JOIN Expense_Code P ON P.ExpenseID = TD.ExpenseID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobExpenseIssueData, "Job_Expense_Issue_Detail", textCommand);
				return jobExpenseIssueData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobExpenseIssueDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Expense_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobExpenseIssue(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobExpenseIssue(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobExpenseIssueDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Job_Expense_Issue WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Job Expense Issue", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetJobExpenseIssueToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.* FROM Job_Expense_Issue IA\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Expense_Issue", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Expense_Issue"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Amount) AS Amount FROM Job_Expense_Issue_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                       GROUP BY SysDocID,VoucherID,ExpenseID,JobID,CostCategoryID,EmployeeID,Description,Remarks,Quantity,UnitPrice,Amount,RowIndex,IsBillable,IsBilled,BilledAmount\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Expense_Issue_Detail", cmdText);
				dataSet.Relations.Add("JobIssueDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Expense_Issue"].Columns["SysDocID"],
					dataSet.Tables["Job_Expense_Issue"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Expense_Issue_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Expense_Issue_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobExpenseIssueList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JID.JobID,J.JobName,JI.TransactionDate AS [Date]   \r\n                            FROM Job_Expense_Issue JI INNER JOIN Job_Expense_Issue_Detail JID ON JI.SysDocID=JID.SysDocID and JI.VoucherID=JID.VoucherID LEFT JOIN Job J ON JID.JobID=J.JobID\r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Expense_Issue", str);
			return dataSet;
		}
	}
}
