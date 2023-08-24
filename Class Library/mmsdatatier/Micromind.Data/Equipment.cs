using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Equipment : StoreObject
	{
		private const string EQUIPMENTID_PARM = "@EquipmentID";

		private const string EQUIPMENTNAME_PARM = "@EquipmentName";

		private const string EQUIPMENTDESC_PARM = "@EquipmentDesc";

		public const string ASSETID_PARM = "@AssetID";

		public const string BILLINGRATE_PARM = "@BillingRate";

		public const string BILLINGUNIT_PARM = "@BillingUnit";

		private const string EQUIPMENT_TABLE = "Job_Cost_Category";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Equipment(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Equipment", new FieldValue("EquipmentID", "@EquipmentID", isUpdateConditionField: true), new FieldValue("EquipmentName", "@EquipmentName"), new FieldValue("Description", "@EquipmentDesc"), new FieldValue("AssetID", "@AssetID"), new FieldValue("BillingRate", "@BillingRate"), new FieldValue("BillingUnit", "@BillingUnit"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Equipment", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EquipmentID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentName", SqlDbType.NVarChar);
			parameters.Add("@EquipmentDesc", SqlDbType.NVarChar);
			parameters.Add("@AssetID", SqlDbType.NVarChar);
			parameters.Add("@BillingRate", SqlDbType.Money);
			parameters.Add("@BillingUnit", SqlDbType.TinyInt);
			parameters["@EquipmentID"].SourceColumn = "EquipmentID";
			parameters["@EquipmentName"].SourceColumn = "EquipmentName";
			parameters["@EquipmentDesc"].SourceColumn = "Description";
			parameters["@AssetID"].SourceColumn = "AssetID";
			parameters["@BillingRate"].SourceColumn = "BillingRate";
			parameters["@BillingUnit"].SourceColumn = "BillingUnit";
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

		public bool InsertEquipment(EquipmentData equipmentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(equipmentData, "Equipment", insertUpdateCommand);
				string text = equipmentData.EquipmentTable.Rows[0]["EquipmentID"].ToString();
				AddActivityLog("Equipment", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Equipment", "EquipmentID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEquipment(EquipmentData accountEquipmentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEquipmentData, "Equipment", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEquipmentData.EquipmentTable.Rows[0]["EquipmentID"];
				UpdateTableRowByID("Equipment", "EquipmentID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEquipmentData.EquipmentTable.Rows[0]["EquipmentName"].ToString();
				AddActivityLog("Equipment", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Equipment", "EquipmentID", obj, sqlTransaction, isInsert: false);
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

		public EquipmentData GetEquipment()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Equipment");
			EquipmentData equipmentData = new EquipmentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(equipmentData, "Equipment", sqlBuilder);
			return equipmentData;
		}

		public bool DeleteEquipment(string equipmentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Equipment WHERE EquipmentID = '" + equipmentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Equipment", equipmentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EquipmentData GetEquipmentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EquipmentID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Equipment";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EquipmentData equipmentData = new EquipmentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(equipmentData, "Equipment", sqlBuilder);
			return equipmentData;
		}

		public DataSet GetEquipmentByFields(params string[] columns)
		{
			return GetEquipmentByFields(null, isInactive: true, columns);
		}

		public DataSet GetEquipmentByFields(string[] jobTypeID, params string[] columns)
		{
			return GetEquipmentByFields(jobTypeID, isInactive: true, columns);
		}

		public DataSet GetEquipmentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Equipment");
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
				commandHelper.FieldName = "EquipmentID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Equipment";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Equipment", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEquipmentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentID [Code], EquipmentName [Name], BillingRate [Billing Rate], \r\n                            CASE BillingUnit WHEN 1 THEN 'Hour' WHEN 2 THEN 'Day' END AS [Billing Unit]\r\n                           FROM Equipment";
			FillDataSet(dataSet, "Equipment", textCommand);
			return dataSet;
		}

		public DataSet GetEquipmentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentID [Code], EquipmentName [Name]\r\n                           FROM Equipment ORDER BY EquipmentID, EquipmentName";
			FillDataSet(dataSet, "Equipment", textCommand);
			return dataSet;
		}
	}
}
