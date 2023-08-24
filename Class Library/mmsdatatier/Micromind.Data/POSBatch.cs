using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class POSBatch : StoreObject
	{
		private const string POSBATCH_TABLE = "POS_Batch";

		private const string BATCHID_PARM = "@BatchID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string OPENDATE_PARM = "@OpenDate";

		private const string CLOSEDATE_PARM = "@CloseDate";

		private const string STATUS_PARM = "@Status";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public POSBatch(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("POS_Batch", new FieldValue("LocationID", "@LocationID"), new FieldValue("OpenDate", "@OpenDate"), new FieldValue("CloseDate", "@CloseDate"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("POS_Batch", new FieldValue("BatchID", "@BatchID", isUpdateConditionField: true));
				sqlBuilder.AddInsertUpdateParameters("POS_Batch", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			if (isUpdate)
			{
				parameters.Add("@BatchID", SqlDbType.Int);
			}
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@OpenDate", SqlDbType.DateTime);
			parameters.Add("@CloseDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.NVarChar);
			if (isUpdate)
			{
				parameters["@BatchID"].SourceColumn = "BatchID";
			}
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@OpenDate"].SourceColumn = "OpenDate";
			parameters["@CloseDate"].SourceColumn = "CloseDate";
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

		public bool CreatePOSBatch(string locationID)
		{
			try
			{
				POSBatchData pOSBatchData = new POSBatchData();
				DataRow dataRow = pOSBatchData.POSBatchTable.NewRow();
				dataRow["Status"] = "O";
				dataRow["LocationID"] = locationID;
				dataRow["OpenDate"] = DateTime.Now;
				dataRow.EndEdit();
				pOSBatchData.POSBatchTable.Rows.Add(dataRow);
				return CreatePOSBatch(pOSBatchData);
			}
			catch
			{
				throw;
			}
		}

		public bool CreatePOSBatch(POSBatchData posBatchData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = posBatchData.POSBatchTable.Rows[0];
				dataRow["Status"] = "O";
				dataRow["OpenDate"] = DateTime.Now;
				dataRow.EndEdit();
				if (GetCurrentOpenBatchID(dataRow["LocationID"].ToString()) != -1)
				{
					throw new CompanyException("There is already an open batch.");
				}
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(posBatchData, "POS_Batch", insertUpdateCommand);
				string text = posBatchData.POSBatchTable.Rows[0]["BatchID"].ToString();
				AddActivityLog("POS Batch", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("POS_Batch", "BatchID", text, sqlTransaction, isInsert: true);
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

		public bool InsertPOSBatch(POSBatchData posBatchData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				posBatchData.POSBatchTable.Rows[0]["OpenDate"] = DateTime.Now;
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(posBatchData, "POS_Batch", insertUpdateCommand);
				int num = int.Parse(GetInsertedRowIdentity("POS_Batch", insertUpdateCommand).ToString());
				AddActivityLog("POS Batch", num.ToString(), ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("POS_Batch", "BatchID", num.ToString(), sqlTransaction, isInsert: true);
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

		public bool UpdatePOSBatch(POSBatchData accountPOSBatchData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPOSBatchData, "POS_Batch", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPOSBatchData.POSBatchTable.Rows[0]["BatchID"];
				UpdateTableRowByID("POS_Batch", "BatchID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = obj.ToString();
				AddActivityLog("POS Batch", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("POS_Batch", "BatchID", obj, sqlTransaction, isInsert: false);
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

		public POSBatchData GetPOSBatch()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("POS_Batch");
			POSBatchData pOSBatchData = new POSBatchData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pOSBatchData, "POS_Batch", sqlBuilder);
			return pOSBatchData;
		}

		public bool ClosePOSBatch(int batchID, string locationID)
		{
			bool result = true;
			GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(DateTime.Now);
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT Status FROM POS_Batch WHERE\r\n                                BatchID = " + batchID.ToString() + " AND LocationID = '" + locationID.ToString() + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					throw new CompanyException("Requested batch number not found or is in another location.", 2002);
				}
				if (obj != null && obj.ToString() != "" && obj.ToString() == "C")
				{
					throw new CompanyException("This batch is already closed.", 2004);
				}
				exp = "SELECT TOP 1 ShiftID FROM POS_Shift WHERE\r\n                                BatchID = " + batchID.ToString() + " AND ISNULL(Status,'O') = 'O'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					throw new CompanyException("All open shifts must be closed before closing the batch.", 2003);
				}
				exp = "UPDATE POS_Batch SET CloseDate = '" + text + "', Status = 'C'   WHERE\r\n                                BatchID = " + batchID.ToString() + " AND LocationID = '" + locationID + "'";
				result = (ExecuteNonQuery(exp, sqlTransaction) > 0);
				AddActivityLog("POS Batch", batchID.ToString(), ActivityTypes.Close, sqlTransaction);
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

		public bool DeletePOSBatch(string posBatchID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM POS_Batch WHERE BatchID = '" + posBatchID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("POS Batch", posBatchID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public POSBatchData GetPOSBatchByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BatchID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "POS_Batch";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			POSBatchData pOSBatchData = new POSBatchData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pOSBatchData, "POS_Batch", sqlBuilder);
			return pOSBatchData;
		}

		public DataSet GetPOSBatchByFields(params string[] columns)
		{
			return GetPOSBatchByFields(null, isInactive: true, columns);
		}

		public DataSet GetPOSBatchByFields(string[] posBatchID, params string[] columns)
		{
			return GetPOSBatchByFields(posBatchID, isInactive: true, columns);
		}

		public DataSet GetPOSBatchByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("POS_Batch");
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
				commandHelper.FieldName = "BatchID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "POS_Batch";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "POS_Batch", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPOSBatchList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT POSBatchID [POSBatch Code],POSBatchName [POSBatch Name]\r\n                           FROM POSBatch ";
			FillDataSet(dataSet, "POS_Batch", textCommand);
			return dataSet;
		}

		public int GetCurrentOpenBatchID(string locationID)
		{
			new DataSet();
			string exp = "SELECT TOP 1 BatchID  \r\n                           FROM POS_Batch WHERE ISNULL(Status,'O')='O' AND LocationID='" + locationID + "'";
			object obj = ExecuteScalar(exp);
			if (obj == null)
			{
				return -1;
			}
			return int.Parse(obj.ToString());
		}

		public DataSet GetPOSBatchComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT POSBatchID [Code],POSBatchName [Name]\r\n                           FROM POSBatch ORDER BY POSBatchID,POSBatchName";
			FillDataSet(dataSet, "POS_Batch", textCommand);
			return dataSet;
		}
	}
}
