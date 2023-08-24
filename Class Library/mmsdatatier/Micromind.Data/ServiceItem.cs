using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ServiceItem : StoreObject
	{
		private const string SERVICEITEMID_PARM = "@ServiceItemID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string SERVICETYPE_PARM = "@ServiceType";

		private const string REPEATCOUNTDAYS_PARM = "@RepeatCountDays";

		private const string REPEATCOUNTKMS_PARM = "@RepeatCountKM";

		private const string REMINDERDAYS_PARM = "@ReminderDays";

		private const string REMINDERKMS_PARM = "@ReminderKM";

		private const string APACCOUNTID_PARM = "@APAccountID";

		private const string VEHICLETYPE_PARM = "@VehicleType";

		private const string NOTE_PARM = "@Note";

		private const string INACTIVE_PARM = "@IsInactive";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ServiceItem(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Service_Item", new FieldValue("ServiceItemID", "@ServiceItemID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("ServiceType", "@ServiceType"), new FieldValue("RepeatCountDays", "@RepeatCountDays"), new FieldValue("RepeatCountKM", "@RepeatCountKM"), new FieldValue("VehicleType", "@VehicleType"), new FieldValue("Inactive", "@IsInactive"), new FieldValue("ReminderDays", "@ReminderDays"), new FieldValue("ReminderKM", "@ReminderKM"), new FieldValue("APAccountID", "@APAccountID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Service_Item", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ServiceItemID", SqlDbType.NVarChar);
			parameters.Add("@ServiceType", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RepeatCountDays", SqlDbType.Decimal);
			parameters.Add("@RepeatCountKM", SqlDbType.Decimal);
			parameters.Add("@VehicleType", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ReminderDays", SqlDbType.Decimal);
			parameters.Add("@ReminderKM", SqlDbType.Decimal);
			parameters.Add("@APAccountID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters["@ServiceItemID"].SourceColumn = "ServiceItemID";
			parameters["@ServiceType"].SourceColumn = "ServiceType";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@ReminderDays"].SourceColumn = "ReminderDays";
			parameters["@ReminderKM"].SourceColumn = "ReminderKM";
			parameters["@RepeatCountDays"].SourceColumn = "RepeatCountDays";
			parameters["@RepeatCountKM"].SourceColumn = "RepeatCountKM";
			parameters["@APAccountID"].SourceColumn = "APAccountID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@VehicleType"].SourceColumn = "VehicleType";
			parameters["@IsInactive"].SourceColumn = "Inactive";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		public bool InsertServiceItem(ServiceItemData serviceitemData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(serviceitemData, "Service_Item", insertUpdateCommand);
				string text = serviceitemData.ServiceItemTable.Rows[0]["ServiceItemID"].ToString();
				AddActivityLog("ServiceItem", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Service_Item", "ServiceItemID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateServiceItem(ServiceItemData serviceitemdata)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(serviceitemdata, "Service_Item", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				string text = serviceitemdata.ServiceItemTable.Rows[0]["ServiceItemID"].ToString();
				UpdateTableRowByID("Service_Item", "ServiceItemID", "DateUpdated", text, DateTime.Now, sqlTransaction);
				serviceitemdata.ServiceItemTable.Rows[0]["Description"].ToString();
				AddActivityLog("ServiceItem", text, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Service_Item", "ServiceItemID", text, sqlTransaction, isInsert: false);
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

		public ServiceItemData GetServiceItem()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Service_Item");
			ServiceItemData serviceItemData = new ServiceItemData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(serviceItemData, "Service_Item", sqlBuilder);
			return serviceItemData;
		}

		public bool DeleteServiceItem(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Service_Item WHERE ServiceItemID = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Expense", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ServiceItemData GetServiceItemByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ServiceItemID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Service_Item";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ServiceItemData serviceItemData = new ServiceItemData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(serviceItemData, "Service_Item", sqlBuilder);
			return serviceItemData;
		}

		public DataSet GetServiceItemByFields(params string[] columns)
		{
			return GetServiceItemByFields(null, isInactive: true, columns);
		}

		public DataSet GetServiceItemByFields(string[] degreeID, params string[] columns)
		{
			return GetServiceItemByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetServiceItemByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Service_Item");
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
				commandHelper.FieldName = "ServiceItemID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Service_Item";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Service_Item", sqlBuilder);
			return dataSet;
		}

		public DataSet GetServiceItemList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ServiceItemID [Code],Description [Name]\r\n                           FROM Service_Item ";
			FillDataSet(dataSet, "Service_Item", textCommand);
			return dataSet;
		}

		public DataSet GetServiceItemComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ServiceItemID [Code],Description [Name],ServiceType [Service Type],RepeatCountDays [Repeat Days],TaxOption, TaxGroupID\r\n                           FROM Service_Item ORDER BY ServiceItemID";
			FillDataSet(dataSet, "Service_Item", textCommand);
			return dataSet;
		}

		public string GetServiceItemAccountID(string serviceItemID)
		{
			new DataSet();
			string exp = "SELECT APAccountID\r\n                           FROM Service_Item where ServiceItemID='" + serviceItemID + "'";
			return ExecuteScalar(exp).ToString();
		}
	}
}
