using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobMaintenanceSchedule : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string JOBID_PARM = "@JobID";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string DESCRIPTION_PARM = "@Description";

		public const string LOCATIONID_PARM = "@LocationID";

		private const string JOBMAINTENANCESCHEDULE_TABLE = "Job_Maintenance_Schedule";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ASSETID_PARM = "@AssetID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ACTIVITYID_PARM = "@ActivityID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string SCHEDULEDON_PARM = "@ScheduledOn";

		private const string NEXTSCHEDULEDATE_PARM = "@NextScheduleDate";

		public const string JOBMAINTENANCESCHEDULEDETAIL_TABLE = "Job_Maintenance_Schedule_Detail";

		public JobMaintenanceSchedule(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobMaintenanceScheduleText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Maintenance_Schedule", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("LocationID", "@LocationID"), new FieldValue("JobID", "@JobID"), new FieldValue("Reference2", "@Reference2"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Maintenance_Schedule", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaintenanceScheduleCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaintenanceScheduleText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaintenanceScheduleText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
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
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
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

		private string GetInsertUpdateJobMaintenanceScheduleDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Maintenance_Schedule_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("AssetID", "@AssetID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ActivityID", "@ActivityID"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("ScheduledOn", "@ScheduledOn"), new FieldValue("NextScheduleDate", "@NextScheduleDate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaintenanceScheduleDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaintenanceScheduleDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaintenanceScheduleDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@AssetID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ActivityID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@ScheduledOn", SqlDbType.NVarChar);
			parameters.Add("@NextScheduleDate", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ActivityID"].SourceColumn = "ActivityID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@ScheduledOn"].SourceColumn = "ScheduledOn";
			parameters["@NextScheduleDate"].SourceColumn = "NextScheduleDate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobMaintenanceScheduleData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobMaintenanceSchedule(JobMaintenanceScheduleData jobMaintenanceScheduleData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobMaintenanceScheduleCommand = GetInsertUpdateJobMaintenanceScheduleCommand(isUpdate);
			try
			{
				DataRow dataRow = jobMaintenanceScheduleData.JobMaintenanceScheduleTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Maintenance_Schedule", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobMaintenanceScheduleData.JobMaintenanceScheduleDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobMaintenanceScheduleDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateJobMaintenanceScheduleCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobMaintenanceScheduleData, "Job_Maintenance_Schedule", insertUpdateJobMaintenanceScheduleCommand)) : (flag & Insert(jobMaintenanceScheduleData, "Job_Maintenance_Schedule", insertUpdateJobMaintenanceScheduleCommand)));
				insertUpdateJobMaintenanceScheduleCommand = GetInsertUpdateJobMaintenanceScheduleDetailsCommand(isUpdate: false);
				insertUpdateJobMaintenanceScheduleCommand.Transaction = sqlTransaction;
				if (jobMaintenanceScheduleData.JobMaintenanceScheduleDetailTable.Rows.Count > 0)
				{
					flag &= Insert(jobMaintenanceScheduleData, "Job_Maintenance_Schedule_Detail", insertUpdateJobMaintenanceScheduleCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Maintenance_Schedule", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Maintenance Schedule";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Maintenance_Schedule", "VoucherID", sqlTransaction);
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

		public JobMaintenanceScheduleData GetJobMaintenanceScheduleByID(string sysDocID, string voucherID)
		{
			try
			{
				JobMaintenanceScheduleData jobMaintenanceScheduleData = new JobMaintenanceScheduleData();
				string textCommand = "SELECT * FROM Job_Maintenance_Schedule WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaintenanceScheduleData, "Job_Maintenance_Schedule", textCommand);
				if (jobMaintenanceScheduleData == null || jobMaintenanceScheduleData.Tables.Count == 0 || jobMaintenanceScheduleData.Tables["Job_Maintenance_Schedule"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Job_Maintenance_Schedule MR INNER JOIN \r\n                        Job_Maintenance_Schedule_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID                      \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaintenanceScheduleData, "Job_Maintenance_Schedule_Detail", textCommand);
				return jobMaintenanceScheduleData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaintenanceScheduleAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date], Description, Reference, \r\n                                (SELECT LocationName FROM Location L WHERE L.LocationID =  JMR.LocationID) AS Location FROM Job_Maintenance_Schedule JMR";
				FillDataSet(dataSet, "Job_Maintenance_Schedule", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobMaintenanceScheduleDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Job_Maintenance_Schedule_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobMaintenanceSchedule(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobMaintenanceSchedule(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobMaintenanceScheduleDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Job_Maintenance_Schedule WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Job Material Estimate", voucherID, sysDocID, ActivityTypes.Delete, null);
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

		public DataSet GetJobMaintenanceScheduleToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT * FROM Job_Maintenance_Schedule WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Maintenance_Schedule", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Maintenance_Schedule"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Job_Maintenance_Schedule_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Maintenance_Schedule_Detail", cmdText);
				dataSet.Relations.Add("JobMaterialDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Maintenance_Schedule"].Columns["SysDocID"],
					dataSet.Tables["Job_Maintenance_Schedule"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Maintenance_Schedule_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Maintenance_Schedule_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaintenanceScheduleList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JI.JobID,J.JobName,JI.TransactionDate AS [Date]   \r\n                            FROM Job_Maintenance_Schedule JI LEFT JOIN Job J ON JI.JobID=J.JobID\r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Maintenance_Schedule", str);
			return dataSet;
		}

		public DataSet GetOpenSchedules()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT JMS.SysDocID,JMS.VoucherID,TransactionDate,JMS.JobID,J.JobName,J.CustomerID,C.CustomerName,JMS.LocationID FROM Job_Maintenance_Schedule JMS\r\n                                    INNER JOIN Job_Maintenance_Schedule_Detail JMSD ON JMS.SysDocID = JMSD.SysDocID AND JMS.VoucherID = JMSD.VoucherID\r\n                                    INNER JOIN Job J ON J.JobID=JMS.JobID\r\n                                    INNER JOIN Customer C ON C.CustomerID=J.CustomerID                                   \r\n                                    GROUP BY JMS.SysDocID,JMS.VoucherID,TransactionDate,JMS.JobID,J.JobName,J.CustomerID,C.CustomerName,JMS.LocationID";
				FillDataSet(dataSet, "Job_Maintenance_Schedule", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenSchedulesDetails(string scheduleSysDocID, string schedulevoucherID, string RowIndexStr, bool tochk)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SysDocID [Doc ID],VoucherID [Number],AssetID,ActivityID,StartDate,EndDate,RowIndex,ScheduledOn,NextScheduleDate\r\n                                 FROM Job_Maintenance_Schedule_Detail  WHERE  SysDocID='" + scheduleSysDocID + "' AND VoucherID = '" + schedulevoucherID + "' ";
				if (tochk)
				{
					text = text + " AND RowIndex IN (" + RowIndexStr + ")";
				}
				FillDataSet(dataSet, "Job_Maintenance_Schedule_Detail", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
