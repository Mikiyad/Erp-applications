using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class QualityTask : StoreObject
	{
		private const string TASKID_PARM = "@TaskID";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string VENDORID_PARM = "@VendorID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string RECEIVEDATE_PARM = "@ReceiveDate";

		private const string GRNSYSDOCID_PARM = "@GRNSysDocID";

		private const string GRNVOUCHERID_PARM = "@GRNVoucherID";

		private const string ASSIGNEDTO_PARM = "@AssignedTo";

		private const string COMMODITYID_PARM = "@CommodityID";

		private const string STATUS_PARM = "@Status";

		public const string DESCRIPTION_PARM = "@Description";

		public const string QUALITYTASK_TABLE = "Quality_Task";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public QualityTask(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Quality_Task", new FieldValue("TaskID", "@TaskID", isUpdateConditionField: true), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("VendorID", "@VendorID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ReceiveDate", "@ReceiveDate"), new FieldValue("GRNSysDocID", "@GRNSysDocID"), new FieldValue("GRNVoucherID", "@GRNVoucherID"), new FieldValue("Description", "@Description"), new FieldValue("AssignedTo", "@AssignedTo"), new FieldValue("CommodityID", "@CommodityID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Quality_Task", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
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
			parameters.Add("@TaskID", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@ReceiveDate", SqlDbType.DateTime);
			parameters.Add("@GRNSysDocID", SqlDbType.NVarChar);
			parameters.Add("@GRNVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@AssignedTo", SqlDbType.NVarChar);
			parameters.Add("@CommodityID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.Bit);
			parameters["@TaskID"].SourceColumn = "TaskID";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@ReceiveDate"].SourceColumn = "ReceiveDate";
			parameters["@GRNSysDocID"].SourceColumn = "GRNSysDocID";
			parameters["@GRNVoucherID"].SourceColumn = "GRNVoucherID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@AssignedTo"].SourceColumn = "AssignedTo";
			parameters["@CommodityID"].SourceColumn = "CommodityID";
			parameters["@Status"].SourceColumn = "Status";
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

		public bool InsertQualityTask(QualityTaskData accountQualityTaskData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountQualityTaskData, "Quality_Task", insertUpdateCommand);
				string text = accountQualityTaskData.QualityTaskTable.Rows[0]["TaskID"].ToString();
				AddActivityLog("Product Category", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Quality_Task", "TaskID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateQualityTask(QualityTaskData accountQualityTaskData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountQualityTaskData, "Quality_Task", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountQualityTaskData.QualityTaskTable.Rows[0]["TaskID"];
				UpdateTableRowByID("Quality_Task", "TaskID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountQualityTaskData.QualityTaskTable.Rows[0]["ContainerNumber"].ToString();
				AddActivityLog("Quality Task", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Quality_Task", "TaskID", obj, sqlTransaction, isInsert: false);
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

		public QualityTaskData GetQualityTask()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Quality_Task");
			QualityTaskData qualityTaskData = new QualityTaskData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(qualityTaskData, "Quality_Task", sqlBuilder);
			return qualityTaskData;
		}

		public bool DeleteQualityTask(string TaskID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Quality_Task WHERE TaskID = '" + TaskID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Quality Task", TaskID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public QualityTaskData GetQualityTaskByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TaskID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Quality_Task";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			QualityTaskData qualityTaskData = new QualityTaskData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(qualityTaskData, "Quality_Task", sqlBuilder);
			return qualityTaskData;
		}

		public DataSet GetQualityTaskByFields(params string[] columns)
		{
			return GetQualityTaskByFields(null, isInactive: true, columns);
		}

		public DataSet GetQualityTaskByFields(string[] taskID, params string[] columns)
		{
			return GetQualityTaskByFields(taskID, isInactive: true, columns);
		}

		public DataSet GetQualityTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Quality_Task");
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
				commandHelper.FieldName = "TaskID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Quality_Task";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Quality_Task", sqlBuilder);
			return dataSet;
		}

		public DataSet GetQualityTaskList(bool includeClosedTasks)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT TaskID [Task Code],ContainerNumber [Container#],V.VendorID, V.VendorID + ' - ' + v.VendorName AS Vendor,GRNSysDocID [GRN DocID],GRNVoucherID [GRN Number],Description\r\n                           FROM Quality_Task QT INNER JOIN Vendor V ON V.VendorID = QT.VendorID ";
			if (!includeClosedTasks)
			{
				text += " WHERE ISNULL(Status,1) = 1 AND NOT EXISTS (SELECT * FROM Arrival_Report AR WHERE AR.TaskID = QT.TaskID) ";
			}
			FillDataSet(dataSet, "Quality_Task", text);
			return dataSet;
		}

		public DataSet GetQualityTaskComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name]\r\n                           FROM Quality_Task ORDER BY CategoryID,CategoryName";
			FillDataSet(dataSet, "Quality_Task", textCommand);
			return dataSet;
		}
	}
}
