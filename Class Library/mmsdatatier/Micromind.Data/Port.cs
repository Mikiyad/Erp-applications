using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Port : StoreObject
	{
		private const string PORTID_PARM = "@PortID";

		private const string PORTNAME_PARM = "@PortName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string PORT_TABLE = "Port";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Port(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Port", new FieldValue("PortID", "@PortID", isUpdateConditionField: true), new FieldValue("PortName", "@PortName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Port", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PortID", SqlDbType.NVarChar);
			parameters.Add("@PortName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@PortID"].SourceColumn = "PortID";
			parameters["@PortName"].SourceColumn = "PortName";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertPort(PortData accountPortData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPortData, "Port", insertUpdateCommand);
				string text = accountPortData.PortTable.Rows[0]["PortID"].ToString();
				AddActivityLog("Port", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Port", "PortID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePort(PortData accountPortData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPortData, "Port", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPortData.PortTable.Rows[0]["PortID"];
				UpdateTableRowByID("Port", "PortID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPortData.PortTable.Rows[0]["PortName"].ToString();
				AddActivityLog("Port", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Port", "PortID", obj, sqlTransaction, isInsert: false);
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

		public PortData GetPort()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Port");
			PortData portData = new PortData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(portData, "Port", sqlBuilder);
			return portData;
		}

		public bool DeletePort(string portID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Port WHERE PortID = '" + portID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Port", portID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PortData GetPortByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PortID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Port";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PortData portData = new PortData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(portData, "Port", sqlBuilder);
			return portData;
		}

		public DataSet GetPortByFields(params string[] columns)
		{
			return GetPortByFields(null, isInactive: true, columns);
		}

		public DataSet GetPortByFields(string[] portID, params string[] columns)
		{
			return GetPortByFields(portID, isInactive: true, columns);
		}

		public DataSet GetPortByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Port");
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
				commandHelper.FieldName = "PortID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Port";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Port", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPortList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PortID [Port Code],PortName [Port Name],Note,Inactive\r\n                           FROM Port ";
			FillDataSet(dataSet, "Port", textCommand);
			return dataSet;
		}

		public DataSet GetPortComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PortID [Code],PortName [Name]\r\n                           FROM Port ORDER BY PortID,PortName";
			FillDataSet(dataSet, "Port", textCommand);
			return dataSet;
		}
	}
}
