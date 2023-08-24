using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ClientAsset : StoreObject
	{
		public const string CLIENTASSET_TABLE = "ClientAsset";

		private const string CLIENTASSETID_PARM = "@ClientAssetID";

		private const string ISINACTIVE_PARM = "@Inactive";

		private const string CLIENTASSETNAME_PARM = "@ClientAssetName";

		private const string NOTE_PARM = "@Note";

		private const string JOBID_PARM = "@JobID";

		private const string BRANDID_PARM = "@BrandID";

		private const string MANUFACTURERID_PARM = "@ManufacturerID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string SERIALNO_PARM = "@SerialNo";

		private const string SERVICEBYID_PARM = "@ServiceByID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ClientAsset(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ClientAsset", new FieldValue("ClientAssetID", "@ClientAssetID", isUpdateConditionField: true), new FieldValue("ClientAssetName", "@ClientAssetName"), new FieldValue("JobID", "@JobID"), new FieldValue("BrandID", "@BrandID"), new FieldValue("ManufacturerID", "@ManufacturerID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("StartDate", "@StartDate"), new FieldValue("SerialNo", "@SerialNo"), new FieldValue("ServiceByID", "@ServiceByID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ClientAsset", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ClientAssetID", SqlDbType.NVarChar);
			parameters.Add("@ClientAssetName", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@BrandID", SqlDbType.NVarChar);
			parameters.Add("@ManufacturerID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@SerialNo", SqlDbType.NVarChar);
			parameters.Add("@ServiceByID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ClientAssetID"].SourceColumn = "ClientAssetID";
			parameters["@ClientAssetName"].SourceColumn = "ClientAssetName";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@BrandID"].SourceColumn = "BrandID";
			parameters["@ManufacturerID"].SourceColumn = "ManufacturerID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@SerialNo"].SourceColumn = "SerialNo";
			parameters["@ServiceByID"].SourceColumn = "ServiceByID";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertClientAsset(ClientAssetData accountJobTaskData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountJobTaskData, "ClientAsset", insertUpdateCommand);
				string text = accountJobTaskData.ClientAssetTable.Rows[0]["ClientAssetID"].ToString();
				AddActivityLog("Client Asset", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("ClientAsset", "ClientAssetID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateClientAsset(ClientAssetData accountJobTaskData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountJobTaskData, "ClientAsset", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountJobTaskData.ClientAssetTable.Rows[0]["ClientAssetID"];
				UpdateTableRowByID("ClientAsset", "ClientAssetID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountJobTaskData.ClientAssetTable.Rows[0]["ClientAssetName"].ToString();
				AddActivityLog("Project Task", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("ClientAsset", "ClientAssetID", obj, sqlTransaction, isInsert: false);
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

		public ClientAssetData GetClientAsset()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("ClientAsset");
			ClientAssetData clientAssetData = new ClientAssetData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(clientAssetData, "ClientAsset", sqlBuilder);
			return clientAssetData;
		}

		public bool DeleteClientAsset(string jobTaskID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM ClientAsset WHERE ClientAssetID = '" + jobTaskID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Client Asset", jobTaskID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ClientAssetData GetClientAssetByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ClientAssetID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "ClientAsset";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ClientAssetData clientAssetData = new ClientAssetData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(clientAssetData, "ClientAsset", sqlBuilder);
			return clientAssetData;
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			return GetJobTaskByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] jobTaskID, params string[] columns)
		{
			return GetJobTaskByFields(jobTaskID, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("ClientAsset");
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
				commandHelper.FieldName = "ClientAssetID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "ClientAsset";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "ClientAsset", sqlBuilder);
			return dataSet;
		}

		public DataSet GetClientAssetList(bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select ClientAssetID [ClientAsset ID],ClientAssetName  FROM ClientAsset WHERE ISNULL(Inactive,'False')='False' ";
			FillDataSet(dataSet, "ClientAsset", textCommand);
			return dataSet;
		}

		public DataSet GetClientAssetComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClientAssetID [Code],ClientAssetName [Name],SerialNo,JobID\r\n                           FROM ClientAsset ORDER BY ClientAssetID,ClientAssetName";
			FillDataSet(dataSet, "ClientAsset", textCommand);
			return dataSet;
		}
	}
}
