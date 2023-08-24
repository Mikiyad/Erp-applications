using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class InventoryTransferType : StoreObject
	{
		private const string INVENTORYTRANSFERTYPEID_PARM = "@TypeID";

		private const string INVENTORYTRANSFERTYPENAME_PARM = "@InventoryTransferTypeName";

		public const string INVENTORYTRANSFERTYPE_TABLE = "Inventory_Transfer_Type";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public InventoryTransferType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Transfer_Type", new FieldValue("TypeID", "@TypeID", isUpdateConditionField: true), new FieldValue("AccountID", "@AccountID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("TypeName", "@InventoryTransferTypeName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Inventory_Transfer_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TypeID", SqlDbType.NVarChar);
			parameters.Add("@InventoryTransferTypeName", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@InventoryTransferTypeName"].SourceColumn = "TypeName";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@LocationID"].SourceColumn = "LocationID";
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

		public bool InsertInventoryTransferType(InventoryTransferTypeData accountInventoryTransferTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountInventoryTransferTypeData, "Inventory_Transfer_Type", insertUpdateCommand);
				string text = accountInventoryTransferTypeData.InventoryTransferTypeTable.Rows[0]["TypeID"].ToString();
				AddActivityLog("Inventory Transfer Type", text, "", ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Inventory_Transfer_Type", "TypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateInventoryTransferType(InventoryTransferTypeData accountInventoryTransferTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountInventoryTransferTypeData, "Inventory_Transfer_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountInventoryTransferTypeData.InventoryTransferTypeTable.Rows[0]["TypeID"];
				UpdateTableRowByID("Inventory_Transfer_Type", "TypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountInventoryTransferTypeData.InventoryTransferTypeTable.Rows[0]["TypeName"].ToString();
				AddActivityLog("Inventory Transfer Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Inventory_Transfer_Type", "TypeID", obj, sqlTransaction, isInsert: false);
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

		public InventoryTransferTypeData GetInventoryTransferType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Inventory_Transfer_Type");
			InventoryTransferTypeData inventoryTransferTypeData = new InventoryTransferTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(inventoryTransferTypeData, "Inventory_Transfer_Type", sqlBuilder);
			return inventoryTransferTypeData;
		}

		public bool DeleteInventoryTransferType(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Inventory_Transfer_Type WHERE TypeID = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Inventory Transfer Type", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public InventoryTransferTypeData GetInventoryTransferTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Inventory_Transfer_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			InventoryTransferTypeData inventoryTransferTypeData = new InventoryTransferTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(inventoryTransferTypeData, "Inventory_Transfer_Type", sqlBuilder);
			return inventoryTransferTypeData;
		}

		public DataSet GetInventoryTransferTypeByFields(params string[] columns)
		{
			return GetInventoryTransferTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetInventoryTransferTypeByFields(string[] degreeID, params string[] columns)
		{
			return GetInventoryTransferTypeByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetInventoryTransferTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Inventory_Transfer_Type");
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
				commandHelper.FieldName = "TypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Inventory_Transfer_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Inventory_Transfer_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetInventoryTransferTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Type Code],TypeName [Type Name]\r\n                           FROM Inventory_Transfer_Type ";
			FillDataSet(dataSet, "Inventory_Transfer_Type", textCommand);
			return dataSet;
		}

		public DataSet GetInventoryTransferTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Code],TypeName [Name],AccountID,LocationID\r\n                           FROM Inventory_Transfer_Type ORDER BY TypeID,TypeName";
			FillDataSet(dataSet, "Inventory_Transfer_Type", textCommand);
			return dataSet;
		}
	}
}
