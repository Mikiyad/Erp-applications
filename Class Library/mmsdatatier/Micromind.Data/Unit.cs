using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Unit : StoreObject
	{
		private const string UNITID_PARM = "@UnitID";

		private const string UNITNAME_PARM = "@UnitName";

		public const string NOTE_PARM = "@Note";

		public const string UNIT_TABLE = "Unit";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Unit(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Unit", new FieldValue("UnitID", "@UnitID", isUpdateConditionField: true), new FieldValue("UnitName", "@UnitName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Unit", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitName"].SourceColumn = "UnitName";
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

		public bool InsertUnit(UnitData accountUnitData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountUnitData, "Unit", insertUpdateCommand);
				string text = accountUnitData.UnitTable.Rows[0]["UnitID"].ToString();
				AddActivityLog("Unit", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Unit", "UnitID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateUnit(UnitData accountUnitData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountUnitData, "Unit", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountUnitData.UnitTable.Rows[0]["UnitID"];
				UpdateTableRowByID("Unit", "UnitID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountUnitData.UnitTable.Rows[0]["UnitName"].ToString();
				AddActivityLog("Unit", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Unit", "UnitID", obj, sqlTransaction, isInsert: false);
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

		public UnitData GetUnit()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Unit");
			UnitData unitData = new UnitData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(unitData, "Unit", sqlBuilder);
			return unitData;
		}

		public bool DeleteUnit(string unitID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Unit WHERE UnitID = '" + unitID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Unit", unitID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public UnitData GetUnitByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "UnitID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Unit";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			UnitData unitData = new UnitData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(unitData, "Unit", sqlBuilder);
			return unitData;
		}

		public DataSet GetUnitByFields(params string[] columns)
		{
			return GetUnitByFields(null, isInactive: true, columns);
		}

		public DataSet GetUnitByFields(string[] unitID, params string[] columns)
		{
			return GetUnitByFields(unitID, isInactive: true, columns);
		}

		public DataSet GetUnitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Unit");
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
				commandHelper.FieldName = "UnitID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Unit";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Unit", sqlBuilder);
			return dataSet;
		}

		public DataSet GetUnitList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UnitID [Unit Code],UnitName [Unit Name],Note\r\n                           FROM Unit ";
			FillDataSet(dataSet, "Unit", textCommand);
			return dataSet;
		}

		public DataSet GetUnitComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UnitID [Code],UnitName [Name]\r\n                           FROM Unit ORDER BY UnitID,UnitName";
			FillDataSet(dataSet, "Unit", textCommand);
			return dataSet;
		}

		public DataSet GetProductUnitDetailComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select PU.UnitID AS Code, U.UnitName AS Name,ProductID,FactorType,Factor,IsMainUnit\r\n                                FROM Product_Unit PU INNER JOIN Unit U ON PU.UnitID = U.UnitID\r\n\r\n                                UNION \r\n                                SELECT  P.UnitID AS Code,U.UnitName AS Name,ProductID,'M' AS FactorType,1 AS Factor, 'True' AS IsMainUnit \r\n                                FROM Product P  INNER JOIN Unit U ON P.Unitid = U.UnitID\r\n                                WHERE ProductID IN (SELECT DISTINCT ProductID FROM Product_Unit)\r\n\r\n                                ORDER BY ProductID,IsMainUnit desc,Code";
			FillDataSet(dataSet, "Unit", textCommand);
			return dataSet;
		}
	}
}
