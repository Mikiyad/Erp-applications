using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Transporter : StoreObject
	{
		private const string TRANSPORTERID_PARM = "@TransporterID";

		private const string TRANSPORTERNAME_PARM = "@TransporterName";

		public const string NOTE_PARM = "@Note";

		public const string TRANSPORTER_TABLE = "Transporter";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Transporter(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Transporter", new FieldValue("TransporterID", "@TransporterID", isUpdateConditionField: true), new FieldValue("TransporterName", "@TransporterName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Transporter", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TransporterID", SqlDbType.NVarChar);
			parameters.Add("@TransporterName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@TransporterID"].SourceColumn = "TransporterID";
			parameters["@TransporterName"].SourceColumn = "TransporterName";
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

		public bool InsertTransporter(TransporterData accountTransporterData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountTransporterData, "Transporter", insertUpdateCommand);
				string text = accountTransporterData.TransporterTable.Rows[0]["TransporterID"].ToString();
				AddActivityLog("Transporter", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Transporter", "TransporterID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateTransporter(TransporterData accountTransporterData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountTransporterData, "Transporter", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountTransporterData.TransporterTable.Rows[0]["TransporterID"];
				UpdateTableRowByID("Transporter", "TransporterID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountTransporterData.TransporterTable.Rows[0]["TransporterName"].ToString();
				AddActivityLog("Transporter", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Transporter", "TransporterID", obj, sqlTransaction, isInsert: false);
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

		public TransporterData GetTransporter()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Transporter");
			TransporterData transporterData = new TransporterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(transporterData, "Transporter", sqlBuilder);
			return transporterData;
		}

		public bool DeleteTransporter(string transporterID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Transporter WHERE TransporterID = '" + transporterID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Transporter", transporterID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public TransporterData GetTransporterByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TransporterID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Transporter";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			TransporterData transporterData = new TransporterData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(transporterData, "Transporter", sqlBuilder);
			return transporterData;
		}

		public DataSet GetTransporterByFields(params string[] columns)
		{
			return GetTransporterByFields(null, isInactive: true, columns);
		}

		public DataSet GetTransporterByFields(string[] transporterID, params string[] columns)
		{
			return GetTransporterByFields(transporterID, isInactive: true, columns);
		}

		public DataSet GetTransporterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Transporter");
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
				commandHelper.FieldName = "TransporterID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Transporter";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Transporter", sqlBuilder);
			return dataSet;
		}

		public DataSet GetTransporterList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TransporterID [Transporter Code],TransporterName [Transporter Name],Note\r\n                           FROM Transporter ";
			FillDataSet(dataSet, "Transporter", textCommand);
			return dataSet;
		}

		public DataSet GetTransporterComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TransporterID [Code],TransporterName [Name]\r\n                           FROM Transporter ORDER BY TransporterID,TransporterName";
			FillDataSet(dataSet, "Transporter", textCommand);
			return dataSet;
		}
	}
}
