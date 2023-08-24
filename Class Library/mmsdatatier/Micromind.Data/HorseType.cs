using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class HorseType : StoreObject
	{
		private const string HORSETYPEID_PARM = "@HorsetypeID";

		private const string HORSETYPENAME_PARM = "@HorseTypeName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string HORSETYPE_TABLE = "Horse_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public HorseType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Horse_Type", new FieldValue("HorseTypeID", "@HorsetypeID", isUpdateConditionField: true), new FieldValue("HorseTypeName", "@HorseTypeName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Horse_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@HorsetypeID", SqlDbType.NVarChar);
			parameters.Add("@HorseTypeName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@HorsetypeID"].SourceColumn = "HorseTypeID";
			parameters["@HorseTypeName"].SourceColumn = "HorseTypeName";
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

		public bool InsertHorseType(HorseTypeData accountHorseTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountHorseTypeData, "Horse_Type", insertUpdateCommand);
				string text = accountHorseTypeData.HorseTypeTable.Rows[0]["HorseTypeID"].ToString();
				AddActivityLog("Horse Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Horse_Type", "HorseTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateHorseType(HorseTypeData accountHorseTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountHorseTypeData, "Horse_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountHorseTypeData.HorseTypeTable.Rows[0]["HorseTypeID"];
				UpdateTableRowByID("Horse_Type", "HorseTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountHorseTypeData.HorseTypeTable.Rows[0]["HorseTypeName"].ToString();
				AddActivityLog("Horse Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Horse_Type", "HorseTypeID", obj, sqlTransaction, isInsert: false);
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

		public HorseTypeData GetHorseType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Horse_Type");
			HorseTypeData horseTypeData = new HorseTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(horseTypeData, "Horse_Type", sqlBuilder);
			return horseTypeData;
		}

		public bool DeleteHorseType(string HorseTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Horse_Type WHERE HorseTypeID = '" + HorseTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Horse Type", HorseTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public HorseTypeData GetHorseTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "HorseTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Horse_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			HorseTypeData horseTypeData = new HorseTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(horseTypeData, "Horse_Type", sqlBuilder);
			return horseTypeData;
		}

		public DataSet GetHorseTypeByFields(params string[] columns)
		{
			return GetHorseTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetHorseTypeByFields(string[] HorseTypeID, params string[] columns)
		{
			return GetHorseTypeByFields(HorseTypeID, isInactive: true, columns);
		}

		public DataSet GetHorseTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Horse_Type");
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
				commandHelper.FieldName = "HorseTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Horse_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Horse_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetHorseTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT HorseTypeID [HorseType Code],HorseTypeName [HorseType Name],Note,IsInactive [Inactive]\r\n                           FROM Horse_Type ";
			FillDataSet(dataSet, "Horse_Type", textCommand);
			return dataSet;
		}

		public DataSet GetHorseTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT HorseTypeID [Code],HorseTypeName [Name]\r\n                           FROM Horse_Type ORDER BY HorseTypeID,HorseTypeName";
			FillDataSet(dataSet, "Horse_Type", textCommand);
			return dataSet;
		}
	}
}
