using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobManHrsBudgeting : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string JOBID_PARM = "@JobID";

		private const string JOBMANHRSBUDGETING_TABLE = "Job_Man_Hrs_Budgeting";

		private const string EMPNO_PARM = "@EmployeeID";

		private const string EMPPOSITION_PARM = "@EmpPositionID";

		private const string VARIANCE_PARM = "@Variance";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDHRS_PARM = "@RequiredHrs";

		private const string FROMDATE_PARM = "@FromDate";

		private const string TODATE_PARM = "@ToDate";

		private const string AMOUNT_PARM = "@Amount";

		private const string JOBMANHRSBUDGETINGDETAIL_TABLE = "Job_Man_Hrs_Budgeting_Detail";

		public JobManHrsBudgeting(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobManHrsBudgetingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Man_Hrs_Budgeting", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("JobID", "@JobID"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Man_Hrs_Budgeting", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobManHrsBudgetingCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobManHrsBudgetingText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobManHrsBudgetingText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@JobID"].SourceColumn = "JobID";
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

		private string GetInsertUpdateJobManHrsBudgetingDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Man_Hrs_Budgeting_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("EmpPositionID", "@EmpPositionID"), new FieldValue("RequiredHrs", "@RequiredHrs"), new FieldValue("FromDate", "@FromDate"), new FieldValue("ToDate", "@ToDate"), new FieldValue("Amount", "@Amount"), new FieldValue("Variance", "@Variance"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobManHrsBudgetingDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobManHrsBudgetingDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobManHrsBudgetingDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@EmpPositionID", SqlDbType.NVarChar);
			parameters.Add("@RequiredHrs", SqlDbType.Real);
			parameters.Add("@FromDate", SqlDbType.DateTime);
			parameters.Add("@ToDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@Variance", SqlDbType.Real);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@EmpPositionID"].SourceColumn = "EmpPositionID";
			parameters["@RequiredHrs"].SourceColumn = "RequiredHrs";
			parameters["@FromDate"].SourceColumn = "FromDate";
			parameters["@ToDate"].SourceColumn = "ToDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Variance"].SourceColumn = "Variance";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobMaterialEstimateData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobManHrsBudgeting(JobManHrsBudgetingData joManHrsBudgetingData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobManHrsBudgetingCommand = GetInsertUpdateJobManHrsBudgetingCommand(isUpdate);
			try
			{
				DataRow dataRow = joManHrsBudgetingData.JobManHrsBudgetingDataTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Man_Hrs_Budgeting", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in joManHrsBudgetingData.JobManHrsBudgetingDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobManHrsBudgetingDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateJobManHrsBudgetingCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(joManHrsBudgetingData, "Job_Man_Hrs_Budgeting", insertUpdateJobManHrsBudgetingCommand)) : (flag & Insert(joManHrsBudgetingData, "Job_Man_Hrs_Budgeting", insertUpdateJobManHrsBudgetingCommand)));
				insertUpdateJobManHrsBudgetingCommand = GetInsertUpdateJobManHrsBudgetingDetailsCommand(isUpdate: false);
				insertUpdateJobManHrsBudgetingCommand.Transaction = sqlTransaction;
				if (joManHrsBudgetingData.JobManHrsBudgetingDetailTable.Rows.Count > 0)
				{
					flag &= Insert(joManHrsBudgetingData, "Job_Man_Hrs_Budgeting_Detail", insertUpdateJobManHrsBudgetingCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Man_Hrs_Budgeting", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Man Hrs Budgeting";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Man_Hrs_Budgeting", "VoucherID", sqlTransaction);
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

		public JobManHrsBudgetingData GetJobManHrsBudgetingByID(string sysDocID, string voucherID)
		{
			try
			{
				JobManHrsBudgetingData jobManHrsBudgetingData = new JobManHrsBudgetingData();
				string textCommand = "SELECT * FROM Job_Man_Hrs_Budgeting WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobManHrsBudgetingData, "Job_Man_Hrs_Budgeting", textCommand);
				if (jobManHrsBudgetingData == null || jobManHrsBudgetingData.Tables.Count == 0 || jobManHrsBudgetingData.Tables["Job_Man_Hrs_Budgeting"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Job_Man_Hrs_Budgeting MR INNER JOIN \r\n                        Job_Man_Hrs_Budgeting_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID\r\n                         WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(jobManHrsBudgetingData, "Job_Man_Hrs_Budgeting_Detail", textCommand);
				return jobManHrsBudgetingData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobManHrsBudgetingAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date], Description, Reference, \r\n                                 FROM Job_Man_Hrs_Budgeting JMR";
				FillDataSet(dataSet, "Job_Man_Hrs_Budgeting", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobManHrsBudgetingDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Job_Man_Hrs_Budgeting_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobManHrsBudgeting(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobManHrsBudgeting(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobManHrsBudgetingDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Job_Man_Hrs_Budgeting WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Job Man Hrs Budgeting", voucherID, sysDocID, ActivityTypes.Delete, null);
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

		public DataSet GetJobManHrsBudgetingToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT JI.*,J.JobName [Project],C.CostCategoryName [Cost Category] FROM Job_Man_Hrs_Budgeting JI  LEFT JOIN Job J ON J.JobID=JI.JobID   \r\n                                   LEFT JOIN Job_Cost_Category C ON C.CostCategoryID=JI.CostCategoryID\r\n                         WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Man_Hrs_Budgeting", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Man_Hrs_Budgeting"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT JID.*,P.PositionName [Position],(E.FirstName+' '+E.MiddleName+' '+E.LastName) [Employee] FROM Job_Man_Hrs_Budgeting_Detail JID\r\n                              LEFT JOIN Employee E ON E.EmployeeID=JID.EmployeeID\r\n                              LEFT JOIN Position P ON P.PositionID=JID.EmpPositionID\r\n                          WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Man_Hrs_Budgeting_Detail", cmdText);
				dataSet.Relations.Add("JobManHrsBudgetingDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Man_Hrs_Budgeting"].Columns["SysDocID"],
					dataSet.Tables["Job_Man_Hrs_Budgeting"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Man_Hrs_Budgeting_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Man_Hrs_Budgeting_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobManHrsBudgetingList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID],JI.TransactionDate AS [Date] ,J.JobName [Project],C.CostCategoryName [Cost Category],JI.Reference,RequestedBy,Note\r\n                            FROM Job_Man_Hrs_Budgeting JI  LEFT JOIN Job J ON J.JobID=JI.JobID   \r\n                                   LEFT JOIN Job_Cost_Category C ON C.CostCategoryID=JI.CostCategoryID                 \r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Man_Hrs_Budgeting", str);
			return dataSet;
		}
	}
}
