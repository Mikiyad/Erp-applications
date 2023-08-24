using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class AdjustmentType : StoreObject
	{
		private const string ADJUSTMENTTYPEID_PARM = "@TypeID";

		private const string ADJUSTMENTTYPENAME_PARM = "@TypeName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ISNONSALSE_PARM = "@IsNonSale";

		public const string ACCOUNTID_PARM = "@AccountID";

		public const string ADJUSTMENTTYPE_TABLE = "AdjustmentType";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public AdjustmentType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Adjustment_Type", new FieldValue("TypeID", "@TypeID", isUpdateConditionField: true), new FieldValue("TypeName", "@TypeName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("IsNonSale", "@IsNonSale"), new FieldValue("AccountID", "@AccountID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Adjustment_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TypeName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@IsNonSale", SqlDbType.Bit);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@TypeName"].SourceColumn = "TypeName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@IsNonSale"].SourceColumn = "IsNonSale";
			parameters["@AccountID"].SourceColumn = "AccountID";
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

		public bool InsertAdjustmentType(AdjustmentTypeData accountAdjustmentTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountAdjustmentTypeData, "Adjustment_Type", insertUpdateCommand);
				string text = accountAdjustmentTypeData.AdjustmentTypeTable.Rows[0]["TypeID"].ToString();
				AddActivityLog(" AdjustmentType", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Adjustment_Type", "TypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateAdjustmentType(AdjustmentTypeData accountAdjustmentTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAdjustmentTypeData, "Adjustment_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountAdjustmentTypeData.AdjustmentTypeTable.Rows[0]["TypeID"];
				UpdateTableRowByID("Adjustment_Type", "TypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountAdjustmentTypeData.AdjustmentTypeTable.Rows[0]["TypeName"].ToString();
				AddActivityLog("AdjustmentType", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Adjustment_Type", "TypeID", obj, sqlTransaction, isInsert: false);
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

		public AdjustmentTypeData GetAdjustmentType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Adjustment_Type");
			AdjustmentTypeData adjustmentTypeData = new AdjustmentTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(adjustmentTypeData, "Adjustment_Type", sqlBuilder);
			return adjustmentTypeData;
		}

		public bool DeleteAdjustmentType(string adjustmentTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Adjustment_Type WHERE TypeID = '" + adjustmentTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("AdjustmentType", adjustmentTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public AdjustmentTypeData GetAdjustmentTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Adjustment_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			AdjustmentTypeData adjustmentTypeData = new AdjustmentTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(adjustmentTypeData, "Adjustment_Type", sqlBuilder);
			return adjustmentTypeData;
		}

		public DataSet GetAdjustmentTypeByFields(params string[] columns)
		{
			return GetAdjustmentTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetAdjustmentTypeByFields(string[] adjustmentTypeID, params string[] columns)
		{
			return GetAdjustmentTypeByFields(adjustmentTypeID, isInactive: true, columns);
		}

		public DataSet GetAdjustmentTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Adjustment_Type");
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
				commandHelper.TableName = "Adjustment_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Adjustment_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAdjustmentTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [AdjustmentType Code],TypeName [AdjustmentType Name],AccountID,Inactive  \r\n                           FROM Adjustment_Type ";
			FillDataSet(dataSet, "Adjustment_Type", textCommand);
			return dataSet;
		}

		public DataSet GetAdjustmentTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Code],TypeName [Name], IsNonSale\r\n                           FROM Adjustment_Type ORDER BY TypeID,TypeName";
			FillDataSet(dataSet, "Adjustment_Type", textCommand);
			return dataSet;
		}
	}
}
