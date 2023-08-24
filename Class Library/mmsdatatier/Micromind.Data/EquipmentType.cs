using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EquipmentType : StoreObject
	{
		private const string TYPEID_PARM = "@EquipmentTypeID";

		private const string TYPENAME_PARM = "@EquipmentTypeName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string EQUIPMENTTYPE_TABLE = "EA_Equipment_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EquipmentType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Equipment_Type", new FieldValue("EquipmentTypeID", "@EquipmentTypeID", isUpdateConditionField: true), new FieldValue("EquipmentTypeName", "@EquipmentTypeName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Equipment_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EquipmentTypeID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentTypeName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@EquipmentTypeID"].SourceColumn = "EquipmentTypeID";
			parameters["@EquipmentTypeName"].SourceColumn = "EquipmentTypeName";
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

		public bool InsertEquipmentType(EquipmentTypeData accountEquipmentTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEquipmentTypeData, "EA_Equipment_Type", insertUpdateCommand);
				string text = accountEquipmentTypeData.EquipmentTypeTable.Rows[0]["EquipmentTypeID"].ToString();
				AddActivityLog("Equipment Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Equipment_Type", "EquipmentTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEquipmentType(EquipmentTypeData accountEquipmentTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEquipmentTypeData, "EA_Equipment_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEquipmentTypeData.EquipmentTypeTable.Rows[0]["EquipmentTypeID"];
				UpdateTableRowByID("EA_Equipment_Type", "EquipmentTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				accountEquipmentTypeData.EquipmentTypeTable.Rows[0]["EquipmentTypeName"].ToString();
				AddActivityLog("Equipment Type", obj.ToString(), ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Equipment_Type", "EquipmentTypeID", obj, sqlTransaction, isInsert: false);
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

		public EquipmentTypeData GetEquipmentType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Equipment_Type");
			EquipmentTypeData equipmentTypeData = new EquipmentTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(equipmentTypeData, "EA_Equipment_Type", sqlBuilder);
			return equipmentTypeData;
		}

		public bool DeleteEquipmentType(string typeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM EA_Equipment_Type WHERE EquipmentTypeID = '" + typeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Equipment Type", typeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EquipmentTypeData GetEquipmentTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EquipmentTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "EA_Equipment_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EquipmentTypeData equipmentTypeData = new EquipmentTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(equipmentTypeData, "EA_Equipment_Type", sqlBuilder);
			return equipmentTypeData;
		}

		public DataSet GetEquipmentTypeByFields(params string[] columns)
		{
			return GetEquipmentTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetEquipmentTypeByFields(string[] EquipmentTypeID, params string[] columns)
		{
			return GetEquipmentTypeByFields(EquipmentTypeID, isInactive: true, columns);
		}

		public DataSet GetEquipmentTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Equipment_Type");
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
				commandHelper.FieldName = "EquipmentTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "EA_Equipment_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "EA_Equipment_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEquipmentTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentTypeID [ID],EquipmentTypeName [Name],Note,IsInactive [Inactive]\r\n                           FROM EA_Equipment_Type ";
			FillDataSet(dataSet, "EA_Equipment_Type", textCommand);
			return dataSet;
		}

		public DataSet GetEquipmentTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentTypeID [Code],EquipmentTypeName [Name]\r\n                           FROM EA_Equipment_Type WHERE ISINACTIVE<>1 ORDER BY EquipmentTypeID,EquipmentTypeName";
			FillDataSet(dataSet, "EA_Equipment_Type", textCommand);
			return dataSet;
		}
	}
}
