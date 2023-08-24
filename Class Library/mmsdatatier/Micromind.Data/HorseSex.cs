using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class HorseSex : StoreObject
	{
		private const string HORSESEXID_PARM = "@HorseSexID";

		private const string HORSESEXNAME_PARM = "@HorseSexName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string HORSESEX_TABLE = "Horse_Sex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public HorseSex(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Horse_Sex", new FieldValue("HorseSexID", "@HorseSexID", isUpdateConditionField: true), new FieldValue("HorseSexName", "@HorseSexName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Horse_Sex", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@HorseSexID", SqlDbType.NVarChar);
			parameters.Add("@HorseSexName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@HorseSexID"].SourceColumn = "HorseSexID";
			parameters["@HorseSexName"].SourceColumn = "HorseSexName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertHorseSex(HorseSexData accountHorseSexData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountHorseSexData, "Horse_Sex", insertUpdateCommand);
				string text = accountHorseSexData.HorseSexTable.Rows[0]["HorseSexID"].ToString();
				AddActivityLog("Horse Sex", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Horse_Sex", "HorseSexID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateHorseSex(HorseSexData accountHorseSexData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountHorseSexData, "Horse_Sex", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountHorseSexData.HorseSexTable.Rows[0]["HorseSexID"];
				UpdateTableRowByID("Horse_Sex", "HorseSexID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountHorseSexData.HorseSexTable.Rows[0]["HorseSexName"].ToString();
				AddActivityLog("Horse Sex", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Horse_Sex", "HorseSexID", obj, sqlTransaction, isInsert: false);
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

		public HorseSexData GetHorseSex()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Horse_Sex");
			HorseSexData horseSexData = new HorseSexData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(horseSexData, "Horse_Sex", sqlBuilder);
			return horseSexData;
		}

		public bool DeleteHorseSex(string HorseSexID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Horse_Sex WHERE HorseSexID = '" + HorseSexID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Horse Sex", HorseSexID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public HorseSexData GetHorseSexByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "HorseSexID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Horse_Sex";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			HorseSexData horseSexData = new HorseSexData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(horseSexData, "Horse_Sex", sqlBuilder);
			return horseSexData;
		}

		public DataSet GetHorseSexByFields(params string[] columns)
		{
			return GetHorseSexByFields(null, isInactive: true, columns);
		}

		public DataSet GetHorseSexByFields(string[] HorseSexID, params string[] columns)
		{
			return GetHorseSexByFields(HorseSexID, isInactive: true, columns);
		}

		public DataSet GetHorseSexByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Horse_Sex");
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
				commandHelper.FieldName = "HorseSexID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Horse_Sex";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Horse_Sex", sqlBuilder);
			return dataSet;
		}

		public DataSet GetHorseSexList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT HorseSexID [HorseSex Code],HorseSexName [HorseSex Name],Note,IsInactive [Inactive]\r\n                           FROM Horse_Sex ";
			FillDataSet(dataSet, "Horse_Sex", textCommand);
			return dataSet;
		}

		public DataSet GetHorseSexComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT HorseSexID [Code],HorseSexName [Name]\r\n                           FROM Horse_Sex ORDER BY HorseSexID,HorseSexName";
			FillDataSet(dataSet, "Horse_Sex", textCommand);
			return dataSet;
		}
	}
}
