using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ReleaseTypes : StoreObject
	{
		private const string RELEASETYPEID_PARM = "@ReleasetypeID";

		private const string RELEASETYPENAME_PARM = "@ReleasetypeName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string RELEASETYPE_TABLE = "Release_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ReleaseTypes(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Release_Type", new FieldValue("ReleaseTypeID", "@ReleasetypeID", isUpdateConditionField: true), new FieldValue("ReleaseTypeName", "@ReleasetypeName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Release_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ReleasetypeID", SqlDbType.NVarChar);
			parameters.Add("@ReleasetypeName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ReleasetypeID"].SourceColumn = "ReleaseTypeID";
			parameters["@ReleasetypeName"].SourceColumn = "ReleaseTypeName";
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

		public bool InsertReleaseType(ReleaseTypeData accountReleaseTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountReleaseTypeData, "Release_Type", insertUpdateCommand);
				string text = accountReleaseTypeData.ReleaseTypeTable.Rows[0]["ReleaseTypeID"].ToString();
				AddActivityLog("Release Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Release_Type", "ReleaseTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateReleaseType(ReleaseTypeData accountReleaseTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountReleaseTypeData, "Release_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountReleaseTypeData.ReleaseTypeTable.Rows[0]["ReleaseTypeID"];
				UpdateTableRowByID("Release_Type", "ReleaseTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountReleaseTypeData.ReleaseTypeTable.Rows[0]["ReleaseTypeName"].ToString();
				AddActivityLog("Release Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Release_Type", "ReleaseTypeID", obj, sqlTransaction, isInsert: false);
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

		public ReleaseTypeData GetReleaseType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Release_Type");
			ReleaseTypeData releaseTypeData = new ReleaseTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(releaseTypeData, "Release_Type", sqlBuilder);
			return releaseTypeData;
		}

		public bool DeleteReleaseType(string ReleaseTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Release_Type WHERE ReleaseTypeID = '" + ReleaseTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Release Type", ReleaseTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ReleaseTypeData GetReleaseTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ReleaseTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Release_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ReleaseTypeData releaseTypeData = new ReleaseTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(releaseTypeData, "Release_Type", sqlBuilder);
			return releaseTypeData;
		}

		public DataSet GetReleaseTypeByFields(params string[] columns)
		{
			return GetReleaseTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetReleaseTypeByFields(string[] releaseTypeID, params string[] columns)
		{
			return GetReleaseTypeByFields(releaseTypeID, isInactive: true, columns);
		}

		public DataSet GetReleaseTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Release_Type");
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
				commandHelper.FieldName = "ReleaseTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Release_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Release_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetReleaseTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ReleaseTypeID [ReleaseType Code],ReleaseTypeName [ReleaseType Name],Note,IsInactive [Inactive]\r\n                           FROM Release_Type ";
			FillDataSet(dataSet, "Release_Type", textCommand);
			return dataSet;
		}

		public DataSet GetReleaseTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ReleaseTypeID [Code],ReleaseTypeName [Name]\r\n                           FROM Release_Type ORDER BY ReleaseTypeID,ReleaseTypeName";
			FillDataSet(dataSet, "Release_Type", textCommand);
			return dataSet;
		}
	}
}
