using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Area : StoreObject
	{
		private const string AREAID_PARM = "@AreaID";

		private const string AREANAME_PARM = "@AreaName";

		public const string NOTE_PARM = "@Note";

		public const string COUNTRYID_PARM = "@CountryID";

		public const string PARENTAREAID_PARM = "@PreantAreaID";

		public const string AREA_TABLE = "Area";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Area(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Area", new FieldValue("AreaID", "@AreaID", isUpdateConditionField: true), new FieldValue("AreaName", "@AreaName"), new FieldValue("CountryID", "@CountryID"), new FieldValue("ParentAreaID", "@PreantAreaID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Area", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AreaID", SqlDbType.NVarChar);
			parameters.Add("@AreaName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@PreantAreaID", SqlDbType.NVarChar);
			parameters["@AreaID"].SourceColumn = "AreaID";
			parameters["@AreaName"].SourceColumn = "AreaName";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@PreantAreaID"].SourceColumn = "ParentAreaID";
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

		public bool InsertArea(AreaData accountAreaData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountAreaData, "Area", insertUpdateCommand);
				string text = accountAreaData.AreaTable.Rows[0]["AreaID"].ToString();
				AddActivityLog("Area", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Area", "AreaID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateArea(AreaData accountAreaData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAreaData, "Area", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountAreaData.AreaTable.Rows[0]["AreaID"];
				UpdateTableRowByID("Area", "AreaID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountAreaData.AreaTable.Rows[0]["AreaName"].ToString();
				AddActivityLog("Area", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Area", "AreaID", obj, sqlTransaction, isInsert: false);
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

		public AreaData GetArea()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Area");
			AreaData areaData = new AreaData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(areaData, "Area", sqlBuilder);
			return areaData;
		}

		public bool DeleteArea(string areaID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Area WHERE AreaID = '" + areaID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Area", areaID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public AreaData GetAreaByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "AreaID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Area";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			AreaData areaData = new AreaData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(areaData, "Area", sqlBuilder);
			return areaData;
		}

		public DataSet GetAreaByFields(params string[] columns)
		{
			return GetAreaByFields(null, isInactive: true, columns);
		}

		public DataSet GetAreaByFields(string[] areaID, params string[] columns)
		{
			return GetAreaByFields(areaID, isInactive: true, columns);
		}

		public DataSet GetAreaByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Area");
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
				commandHelper.FieldName = "AreaID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Area";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Area", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAreaList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AreaID [Area Code],AreaName [Area Name],Note\r\n                           FROM Area ";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}

		public DataSet GetAreaComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AreaID [Code],AreaName [Name], CountryID\r\n                           FROM Area ORDER BY AreaID,AreaName";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}
	}
}
