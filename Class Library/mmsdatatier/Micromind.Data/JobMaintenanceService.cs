using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobMaintenanceService : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string JOBID_PARM = "@JobID";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string DESCRIPTION_PARM = "@Description";

		public const string LOCATIONID_PARM = "@LocationID";

		private const string JOBMAINTENANCESERVICE_TABLE = "Job_Maintenance_Service";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ASSETID_PARM = "@AssetID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ACTIVITYID_PARM = "@ActivityID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string SCHEDULESYSDOCID_PARM = "@ScheduleSysDocID";

		private const string SCHEDULEVOUCHERID_PARM = "@ScheduleVoucherID";

		private const string SCHEDULEROWINDEX_PARM = "@ScheduleRowIndex";

		private const string NEXTSCHEDULEDATE_PARM = "@NextScheduleDate";

		private const string REMARKS_PARM = "@Remarks";

		public const string JOBMAINTENANCESERVICEDETAIL_TABLE = "Job_Maintenance_Service_Detail";

		public JobMaintenanceService(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobMaintenanceServiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Maintenance_Service", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"), new FieldValue("JobID", "@JobID"), new FieldValue("Reference2", "@Reference2"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Maintenance_Service", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaintenanceServiceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaintenanceServiceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaintenanceServiceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@Reference2"].SourceColumn = "Reference2";
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

		private string GetInsertUpdateJobMaintenanceServiceDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Maintenance_Service_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("AssetID", "@AssetID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Remarks", "@Remarks"), new FieldValue("ScheduleRowIndex", "@ScheduleRowIndex"), new FieldValue("ScheduleSysDocID", "@ScheduleSysDocID"), new FieldValue("ScheduleVoucherID", "@ScheduleVoucherID"), new FieldValue("NextScheduleDate", "@NextScheduleDate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaintenanceServiceDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaintenanceServiceDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaintenanceServiceDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@AssetID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@ScheduleVoucherID", SqlDbType.VarChar);
			parameters.Add("@ScheduleSysDocID", SqlDbType.VarChar);
			parameters.Add("@ScheduleRowIndex", SqlDbType.Int);
			parameters.Add("@NextScheduleDate", SqlDbType.DateTime);
			parameters.Add("@Remarks", SqlDbType.VarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@NextScheduleDate"].SourceColumn = "NextScheduleDate";
			parameters["@ScheduleVoucherID"].SourceColumn = "ScheduleVoucherID";
			parameters["@ScheduleSysDocID"].SourceColumn = "ScheduleSysDocID";
			parameters["@ScheduleRowIndex"].SourceColumn = "ScheduleRowIndex";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobMaintenanceServiceData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobMaintenanceService(JobMaintenanceServiceData jobMaintenanceServiceData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobMaintenanceServiceCommand = GetInsertUpdateJobMaintenanceServiceCommand(isUpdate);
			try
			{
				DataRow dataRow = jobMaintenanceServiceData.JobMaintenanceServiceTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Maintenance_Service", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobMaintenanceServiceData.JobMaintenanceServiceDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobMaintenanceServiceDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateJobMaintenanceServiceCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobMaintenanceServiceData, "Job_Maintenance_Service", insertUpdateJobMaintenanceServiceCommand)) : (flag & Insert(jobMaintenanceServiceData, "Job_Maintenance_Service", insertUpdateJobMaintenanceServiceCommand)));
				insertUpdateJobMaintenanceServiceCommand = GetInsertUpdateJobMaintenanceServiceDetailsCommand(isUpdate: false);
				insertUpdateJobMaintenanceServiceCommand.Transaction = sqlTransaction;
				if (jobMaintenanceServiceData.JobMaintenanceServiceDetailTable.Rows.Count > 0)
				{
					flag &= Insert(jobMaintenanceServiceData, "Job_Maintenance_Service_Detail", insertUpdateJobMaintenanceServiceCommand);
				}
				DateTime dateTime = default(DateTime);
				foreach (DataRow row2 in jobMaintenanceServiceData.JobMaintenanceServiceDetailTable.Rows)
				{
					string text2 = row2["AssetID"].ToString();
					dateTime = DateTime.Parse(row2["NextScheduleDate"].ToString());
					string text3 = row2["ScheduleSysDocID"].ToString();
					string text4 = row2["ScheduleVoucherID"].ToString();
					string text5 = row2["ScheduleRowIndex"].ToString();
					string commandText = "UPDATE Job_Maintenance_Schedule_Detail SET NextScheduleDate =  '" + dateTime + "' WHERE SysDocID='" + text3 + "' AND VoucherID = '" + text4 + "' AND AssetID = '" + text2 + "' AND RowIndex = '" + text5 + "' ";
					flag &= Update(commandText, sqlTransaction);
				}
				if (flag)
				{
					flag &= UpdateTableRowInsertUpdateInfo("Job_Maintenance_Service", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
					string entityName = "Job Maintenance Service";
					flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
					if (!isUpdate)
					{
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Maintenance_Service", "VoucherID", sqlTransaction);
					}
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobMaintenanceServiceEntry, sysDocID, text, "Job_Maintenance_Service", sqlTransaction);
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

		public JobMaintenanceServiceData GetJobMaintenanceServiceByID(string sysDocID, string voucherID)
		{
			try
			{
				JobMaintenanceServiceData jobMaintenanceServiceData = new JobMaintenanceServiceData();
				string textCommand = "SELECT *,J.JobID,C.CustomerID,J.JobName,C.CustomerName FROM Job_Maintenance_Service JM\r\n               INNER JOIN JOB J ON J.JobID=JM.JobID INNER JOIN Customer C ON C.CustomerID=J.CustomerID\r\n                    WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaintenanceServiceData, "Job_Maintenance_Service", textCommand);
				if (jobMaintenanceServiceData == null || jobMaintenanceServiceData.Tables.Count == 0 || jobMaintenanceServiceData.Tables["Job_Maintenance_Service"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Job_Maintenance_Service MR INNER JOIN \r\n                        Job_Maintenance_Service_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID                      \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaintenanceServiceData, "Job_Maintenance_Service_Detail", textCommand);
				return jobMaintenanceServiceData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaintenanceServiceAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date], Description, Reference, \r\n                                (SELECT LocationName FROM Location L WHERE L.LocationID =  JMR.LocationID) AS Location FROM Job_Maintenance_Service JMR";
				FillDataSet(dataSet, "Job_Maintenance_Service", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobMaintenanceServiceDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Job_Maintenance_Service_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobMaintenanceService(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobMaintenanceService(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobMaintenanceServiceDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Job_Maintenance_Service WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Job Maintenance Service", voucherID, sysDocID, ActivityTypes.Delete, null);
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

		public DataSet GetJobMaintenanceServiceToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT * FROM Job_Maintenance_Service WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Maintenance_Service", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Maintenance_Service"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Job_Maintenance_Service_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Maintenance_Service_Detail", cmdText);
				dataSet.Relations.Add("JobMaterialDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Maintenance_Service"].Columns["SysDocID"],
					dataSet.Tables["Job_Maintenance_Service"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Maintenance_Service_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Maintenance_Service_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaintenanceServiceList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JI.JobID,J.JobName,JI.TransactionDate AS [Date]   \r\n                            FROM Job_Maintenance_Service JI INNER JOIN Job_Maintenance_Service_Detail JID ON JI.SysDocID=JID.SysDocID and JI.VoucherID=JID.VoucherID  LEFT JOIN Job J ON JI.JobID=J.JobID\r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Maintenance_Service", str);
			return dataSet;
		}

		public DataSet GetOpenSchedules()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT JMS.SysDocID,JMS.VoucherID,TransactionDate,JMS.JobID,J.JobName,J.CustomerID,C.CustomerName,JMS.LocationID FROM Job_Maintenance_Service JMS\r\n                                    INNER JOIN Job_Maintenance_Service_Detail JMSD ON JMS.SysDocID = JMSD.SysDocID AND JMS.VoucherID = JMSD.VoucherID\r\n                                    INNER JOIN Job J ON J.JobID=JMS.JobID\r\n                                    INNER JOIN Customer C ON C.CustomerID=J.CustomerID\r\n                                    WHERE ISNULL(Status,'') = '0'\r\n                                    GROUP BY JMS.SysDocID,JMS.VoucherID,TransactionDate,JMS.JobID,J.JobName,J.CustomerID,C.CustomerName,JMS.LocationID";
				FillDataSet(dataSet, "Job_Maintenance_Service", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenSchedulesDetails(string scheduleSysDocID, string schedulevoucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID,VoucherID,AssetID,ActivityID,StartDate,EndDate,RowIndex,ScheduledOn,NextScheduleDate\r\n                                 FROM Job_Maintenance_Service_Detail  WHERE  SysDocID='" + scheduleSysDocID + "' AND VoucherID = '" + schedulevoucherID + "' ";
				FillDataSet(dataSet, "Job_Maintenance_Service_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
