using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Rack : StoreObject
	{
		private const string RACK_TABLE = "Rack";

		private const string RACKID_PARM = "@RackID";

		private const string BINID_PARM = "@BinID";

		private const string ISINACTIVE_PARM = "@Inactive";

		private const string RACKNAME_PARM = "@RackName";

		private const string REMARKS_PARM = "@Remarks";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Rack(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Rack", new FieldValue("RackID", "@RackID"), new FieldValue("BinID", "@BinID"), new FieldValue("RackName", "@RackName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Rack", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RackID", SqlDbType.NVarChar);
			parameters.Add("@BinID", SqlDbType.NVarChar);
			parameters.Add("@RackName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@RackID"].SourceColumn = "RackID";
			parameters["@BinID"].SourceColumn = "BinID";
			parameters["@RackName"].SourceColumn = "RackName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Remarks"].SourceColumn = "Remarks";
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

		public bool InsertRack(RackData RackData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(RackData, "Rack", insertUpdateCommand);
				string text = RackData.RackTable.Rows[0]["RackID"].ToString();
				AddActivityLog("Rack", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Rack", "RackID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateRack(RackData RackData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(RackData, "Rack", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = RackData.RackTable.Rows[0]["RackID"];
				UpdateTableRowByID("Rack", "RackID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = RackData.RackTable.Rows[0]["RackName"].ToString();
				AddActivityLog("Rack", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Rack", "RackID", obj, sqlTransaction, isInsert: false);
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

		public RackData GetRack()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Rack");
			RackData rackData = new RackData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(rackData, "Rack", sqlBuilder);
			return rackData;
		}

		public bool DeleteRack(string RackID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Rack WHERE RackID = '" + RackID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Rack", RackID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public RackData GetRackByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "RackID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Rack";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			RackData rackData = new RackData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(rackData, "Rack", sqlBuilder);
			return rackData;
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			return GetJobTaskByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] RackID, params string[] columns)
		{
			return GetJobTaskByFields(RackID, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Rack");
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
				commandHelper.FieldName = "RackID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Rack";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Rack", sqlBuilder);
			return dataSet;
		}

		public DataSet GetRackList(bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select RackID [Rack ID],RackName  FROM Rack WHERE ISNULL(Inactive,'False')='False' ";
			FillDataSet(dataSet, "Rack", textCommand);
			return dataSet;
		}

		public DataSet GetRackComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT RackID [Code],RackName [Name], BinID\r\n                           FROM Rack ORDER BY RackID,RackName";
			FillDataSet(dataSet, "Rack", textCommand);
			return dataSet;
		}
	}
}
