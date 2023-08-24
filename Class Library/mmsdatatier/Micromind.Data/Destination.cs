using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Destination : StoreObject
	{
		private const string DESTINATIONID_PARM = "@DestinationID";

		private const string DESTINATIONNAME_PARM = "@DestinationName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		public const string DESTINATION_TABLE = "Destination";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string TICKETFIXEDAMOUNT_PARM = "@TicketFixedAmount";

		private const string ACCOUNTID_PARM = "@AccountID";

		public Destination(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Destination", new FieldValue("DestinationID", "@DestinationID", isUpdateConditionField: true), new FieldValue("DestinationName", "@DestinationName"), new FieldValue("TicketFixedAmount", "@TicketFixedAmount"), new FieldValue("AccountID", "@AccountID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Destination", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DestinationID", SqlDbType.NVarChar);
			parameters.Add("@DestinationName", SqlDbType.NVarChar);
			parameters.Add("@TicketFixedAmount", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@DestinationID"].SourceColumn = "DestinationID";
			parameters["@DestinationName"].SourceColumn = "DestinationName";
			parameters["@TicketFixedAmount"].SourceColumn = "TicketFixedAmount";
			parameters["@AccountID"].SourceColumn = "AccountID";
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

		public bool InsertDestination(DestinationData accountDestinationData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDestinationData, "Destination", insertUpdateCommand);
				string text = accountDestinationData.DestinationTable.Rows[0]["DestinationID"].ToString();
				AddActivityLog("Destination", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Destination", "DestinationID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateDestination(DestinationData accountDestinationData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDestinationData, "Destination", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDestinationData.DestinationTable.Rows[0]["DestinationID"];
				UpdateTableRowByID("Destination", "DestinationID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDestinationData.DestinationTable.Rows[0]["DestinationName"].ToString();
				AddActivityLog("Destination", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Destination", "DestinationID", obj, sqlTransaction, isInsert: false);
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

		public DestinationData GetDestination()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Destination");
			DestinationData destinationData = new DestinationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(destinationData, "Destination", sqlBuilder);
			return destinationData;
		}

		public bool DeleteDestination(string destinationID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Destination WHERE DestinationID = '" + destinationID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Destination", destinationID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DestinationData GetDestinationByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DestinationID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Destination";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			DestinationData destinationData = new DestinationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(destinationData, "Destination", sqlBuilder);
			return destinationData;
		}

		public DataSet GetDestinationByFields(params string[] columns)
		{
			return GetDestinationByFields(null, isInactive: true, columns);
		}

		public DataSet GetDestinationByFields(string[] destinationID, params string[] columns)
		{
			return GetDestinationByFields(destinationID, isInactive: true, columns);
		}

		public DataSet GetDestinationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Destination");
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
				commandHelper.FieldName = "DestinationID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Destination";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Destination", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDestinationList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DestinationID [Code],DestinationName [Name]\r\n                           FROM Destination ";
			FillDataSet(dataSet, "Destination", textCommand);
			return dataSet;
		}

		public DataSet GetDestinationComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DestinationID [Code],DestinationName [Name]\r\n                           FROM Destination ORDER BY DestinationID,DestinationName";
			FillDataSet(dataSet, "Destination", textCommand);
			return dataSet;
		}
	}
}
