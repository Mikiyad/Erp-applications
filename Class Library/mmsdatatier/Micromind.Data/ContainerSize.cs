using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ContainerSize : StoreObject
	{
		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string CONTAINERSIZENAME_PARM = "@ContainerSizeName";

		public const string NOTE_PARM = "@Note";

		public const string CONTAINERSIZE_TABLE = "ContainerSize";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ContainerSize(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ContainerSize", new FieldValue("ContainerSizeID", "@ContainerSizeID", isUpdateConditionField: true), new FieldValue("ContainerSizeName", "@ContainerSizeName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ContainerSize", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
			parameters["@ContainerSizeName"].SourceColumn = "ContainerSizeName";
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

		public bool InsertContainerSize(ContainerSizeData accountContainerSizeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountContainerSizeData, "ContainerSize", insertUpdateCommand);
				string text = accountContainerSizeData.ContainerSizeTable.Rows[0]["ContainerSizeID"].ToString();
				AddActivityLog("ContainerSize", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("ContainerSize", "ContainerSizeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateContainerSize(ContainerSizeData accountContainerSizeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountContainerSizeData, "ContainerSize", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountContainerSizeData.ContainerSizeTable.Rows[0]["ContainerSizeID"];
				UpdateTableRowByID("ContainerSize", "ContainerSizeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountContainerSizeData.ContainerSizeTable.Rows[0]["ContainerSizeName"].ToString();
				AddActivityLog("ContainerSize", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("ContainerSize", "ContainerSizeID", obj, sqlTransaction, isInsert: false);
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

		public ContainerSizeData GetContainerSize()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("ContainerSize");
			ContainerSizeData containerSizeData = new ContainerSizeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(containerSizeData, "ContainerSize", sqlBuilder);
			return containerSizeData;
		}

		public bool DeleteContainerSize(string conatainerSizeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM ContainerSize WHERE ContainerSizeID = '" + conatainerSizeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("ContainerSize", conatainerSizeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ContainerSizeData GetContainerSizeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ContainerSizeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "ContainerSize";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ContainerSizeData containerSizeData = new ContainerSizeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(containerSizeData, "ContainerSize", sqlBuilder);
			return containerSizeData;
		}

		public DataSet GetContainerSizeByFields(params string[] columns)
		{
			return GetContainerSizeByFields(null, isInactive: true, columns);
		}

		public DataSet GetContainerSizeByFields(string[] conatainerSizeID, params string[] columns)
		{
			return GetContainerSizeByFields(conatainerSizeID, isInactive: true, columns);
		}

		public DataSet GetContainerSizeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("ContainerSize");
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
				commandHelper.FieldName = "ContainerSizeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "ContainerSize";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "ContainerSize", sqlBuilder);
			return dataSet;
		}

		public DataSet GetContainerSizeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContainerSizeID [ContainerSize Code],ContainerSizeName [ContainerSize Name],Note\r\n                           FROM ContainerSize ";
			FillDataSet(dataSet, "ContainerSize", textCommand);
			return dataSet;
		}

		public DataSet GetContainerSizeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContainerSizeID [Code],ContainerSizeName [Name]\r\n                           FROM ContainerSize ORDER BY ContainerSizeID,ContainerSizeName";
			FillDataSet(dataSet, "ContainerSize", textCommand);
			return dataSet;
		}
	}
}
