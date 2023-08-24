using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class RequisitionType : StoreObject
	{
		private const string TYPEID_PARM = "@RequisitionTypeID";

		private const string TYPENAME_PARM = "@RequisitionTypeName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string RequisitionTYPE_TABLE = "EA_Requisition_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public RequisitionType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Requisition_Type", new FieldValue("RequisitionTypeID", "@RequisitionTypeID", isUpdateConditionField: true), new FieldValue("RequisitionTypeName", "@RequisitionTypeName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Requisition_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RequisitionTypeID", SqlDbType.NVarChar);
			parameters.Add("@RequisitionTypeName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@RequisitionTypeID"].SourceColumn = "RequisitionTypeID";
			parameters["@RequisitionTypeName"].SourceColumn = "RequisitionTypeName";
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

		public bool InsertRequisitionType(RequisitionTypeData accountRequisitionTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountRequisitionTypeData, "EA_Requisition_Type", insertUpdateCommand);
				string text = accountRequisitionTypeData.RequisitionTypeTable.Rows[0]["RequisitionTypeID"].ToString();
				AddActivityLog("Requisition Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Requisition_Type", "RequisitionTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateRequisitionType(RequisitionTypeData accountRequisitionTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountRequisitionTypeData, "EA_Requisition_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountRequisitionTypeData.RequisitionTypeTable.Rows[0]["RequisitionTypeID"];
				UpdateTableRowByID("EA_Requisition_Type", "RequisitionTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountRequisitionTypeData.RequisitionTypeTable.Rows[0]["RequisitionTypeName"].ToString();
				AddActivityLog("Requisition Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Requisition_Type", "RequisitionTypeID", obj, sqlTransaction, isInsert: false);
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

		public RequisitionTypeData GetRequisitionType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Requisition_Type");
			RequisitionTypeData requisitionTypeData = new RequisitionTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(requisitionTypeData, "EA_Requisition_Type", sqlBuilder);
			return requisitionTypeData;
		}

		public bool DeleteRequisitionType(string typeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM EA_Requisition_Type WHERE RequisitionTypeID = '" + typeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Requisition Type", typeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public RequisitionTypeData GetRequisitionTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "RequisitionTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "EA_Requisition_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			RequisitionTypeData requisitionTypeData = new RequisitionTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(requisitionTypeData, "EA_Requisition_Type", sqlBuilder);
			return requisitionTypeData;
		}

		public DataSet GetRequisitionTypeByFields(params string[] columns)
		{
			return GetRequisitionTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetRequisitionTypeByFields(string[] RequisitionTypeID, params string[] columns)
		{
			return GetRequisitionTypeByFields(RequisitionTypeID, isInactive: true, columns);
		}

		public DataSet GetRequisitionTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Requisition_Type");
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
				commandHelper.FieldName = "RequisitionTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "EA_Requisition_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "EA_Requisition_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetRequisitionTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT RequisitionTypeID [ID],RequisitionTypeName [Name],Note,IsInactive [Inactive]\r\n                           FROM EA_Requisition_Type ";
			FillDataSet(dataSet, "EA_Requisition_Type", textCommand);
			return dataSet;
		}

		public DataSet GetRequisitionTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  RequisitionTypeID [Code],RequisitionTypeName [Name]\r\n                           FROM EA_Requisition_Type ORDER BY RequisitionTypeID,RequisitionTypeName";
			FillDataSet(dataSet, "EA_Requisition_Type", textCommand);
			return dataSet;
		}
	}
}
