using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class INCO : StoreObject
	{
		private const string INCOID_PARM = "@INCOID";

		private const string INCONAME_PARM = "@INCOName";

		public const string NOTE_PARM = "@Note";

		public const string INCO_TABLE = "INCO";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public INCO(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("INCO", new FieldValue("INCOID", "@INCOID", isUpdateConditionField: true), new FieldValue("INCOName", "@INCOName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("INCO", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@INCOID", SqlDbType.NVarChar);
			parameters.Add("@INCOName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@INCOID"].SourceColumn = "INCOID";
			parameters["@INCOName"].SourceColumn = "INCOName";
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

		public bool InsertINCO(INCOData accountINCOData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountINCOData, "INCO", insertUpdateCommand);
				string text = accountINCOData.INCOTable.Rows[0]["INCOID"].ToString();
				AddActivityLog("INCO", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("INCO", "INCOID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateINCO(INCOData accountINCOData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountINCOData, "INCO", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountINCOData.INCOTable.Rows[0]["INCOID"];
				UpdateTableRowByID("INCO", "INCOID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountINCOData.INCOTable.Rows[0]["INCOName"].ToString();
				AddActivityLog("INCO", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("INCO", "INCOID", obj, sqlTransaction, isInsert: false);
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

		public INCOData GetINCO()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("INCO");
			INCOData iNCOData = new INCOData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(iNCOData, "INCO", sqlBuilder);
			return iNCOData;
		}

		public bool DeleteINCO(string INCOID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM INCO WHERE INCOID = '" + INCOID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("INCO", INCOID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public INCOData GetINCOByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "INCOID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "INCO";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			INCOData iNCOData = new INCOData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(iNCOData, "INCO", sqlBuilder);
			return iNCOData;
		}

		public DataSet GetINCOByFields(params string[] columns)
		{
			return GetINCOByFields(null, isInactive: true, columns);
		}

		public DataSet GetINCOByFields(string[] INCOID, params string[] columns)
		{
			return GetINCOByFields(INCOID, isInactive: true, columns);
		}

		public DataSet GetINCOByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("INCO");
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
				commandHelper.FieldName = "INCOID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "INCO";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "INCO", sqlBuilder);
			return dataSet;
		}

		public DataSet GetINCOList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT INCOID [INCO Code],INCOName [INCO Name],Note\r\n                           FROM INCO ";
			FillDataSet(dataSet, "INCO", textCommand);
			return dataSet;
		}

		public DataSet GetINCOComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT INCOID [Code],INCOName [Name]\r\n                           FROM INCO ORDER BY INCOID,INCOName";
			FillDataSet(dataSet, "INCO", textCommand);
			return dataSet;
		}
	}
}
