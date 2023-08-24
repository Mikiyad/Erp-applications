using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Driver : StoreObject
	{
		private const string DRIVERID_PARM = "@DriverID";

		private const string DRIVERNAME_PARM = "@DriverName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		public const string DRIVER_TABLE = "Driver";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Driver(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Driver", new FieldValue("DriverID", "@DriverID", isUpdateConditionField: true), new FieldValue("DriverName", "@DriverName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Driver", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@DriverName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@DriverName"].SourceColumn = "DriverName";
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

		public bool InsertDriver(DriverData accountDriverData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDriverData, "Driver", insertUpdateCommand);
				string text = accountDriverData.DriverTable.Rows[0]["DriverID"].ToString();
				AddActivityLog("Driver", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Driver", "DriverID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateDriver(DriverData accountDriverData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDriverData, "Driver", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDriverData.DriverTable.Rows[0]["DriverID"];
				UpdateTableRowByID("Driver", "DriverID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDriverData.DriverTable.Rows[0]["DriverName"].ToString();
				AddActivityLog("Driver", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Driver", "DriverID", obj, sqlTransaction, isInsert: false);
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

		public DriverData GetDriver()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Driver");
			DriverData driverData = new DriverData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(driverData, "Driver", sqlBuilder);
			return driverData;
		}

		public bool DeleteDriver(string driverID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Driver WHERE DriverID = '" + driverID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Driver", driverID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DriverData GetDriverByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DriverID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Driver";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			DriverData driverData = new DriverData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(driverData, "Driver", sqlBuilder);
			return driverData;
		}

		public DataSet GetDriverByFields(params string[] columns)
		{
			return GetDriverByFields(null, isInactive: true, columns);
		}

		public DataSet GetDriverByFields(string[] driverID, params string[] columns)
		{
			return GetDriverByFields(driverID, isInactive: true, columns);
		}

		public DataSet GetDriverByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Driver");
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
				commandHelper.FieldName = "DriverID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Driver";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Driver", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDriverList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DriverID [Driver Code],DriverName [Driver Name],Note,Inactive  \r\n                           FROM Driver ";
			FillDataSet(dataSet, "Driver", textCommand);
			return dataSet;
		}

		public DataSet GetDriverComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DriverID [Code],DriverName [Name]\r\n                           FROM Driver ORDER BY DriverID,DriverName";
			FillDataSet(dataSet, "Driver", textCommand);
			return dataSet;
		}
	}
}
