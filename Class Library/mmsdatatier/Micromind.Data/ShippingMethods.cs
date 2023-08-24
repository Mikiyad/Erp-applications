using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ShippingMethods : StoreObject
	{
		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string SHIPPINGMETHODNAME_PARM = "@ShippingMethodName";

		private const string PHONE_PARM = "@Phone";

		private const string CONTACTNAME_PARM = "@ContactName";

		private const string NOTE_PARM = "@Note";

		private const string INACTIVE_PARM = "@Inactive";

		private const string TRACKSHIPMENT_PARM = "@TrackShipment";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public ShippingMethods(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Shipping_Method", new FieldValue("ShippingMethodID", "@ShippingMethodID", isUpdateConditionField: true), new FieldValue("ShippingMethodName", "@ShippingMethodName"), new FieldValue("ContactName", "@ContactName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("TrackShipment", "@TrackShipment"), new FieldValue("Phone", "@Phone"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Shipping_Method", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodName", SqlDbType.NVarChar);
			parameters.Add("@Phone", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@TrackShipment", SqlDbType.Bit);
			parameters.Add("@ContactName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@ShippingMethodName"].SourceColumn = "ShippingMethodName";
			parameters["@Phone"].SourceColumn = "Phone";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@TrackShipment"].SourceColumn = "TrackShipment";
			parameters["@ContactName"].SourceColumn = "ContactName";
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

		public bool InsertShippingMethod(ShippingMethodData shiperData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(shiperData, "Shipping_Method", insertUpdateCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("Shipping_Method", insertUpdateCommand);
				shiperData.ShippingMethodTable.Rows[0]["ShippingMethodID"] = insertedRowIdentity;
				UpdateTableRowByID("Shipping_Method", "ShippingMethodID", "DateUpdated", insertedRowIdentity, DateTime.Now, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Shipping Method", insertedRowIdentity.ToString(), ActivityTypes.Add, null);
				UpdateTableRowInsertUpdateInfo("Shipping_Method", "ShippingMethodID", insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool UpdateShiper(ShippingMethodData shiperData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(shiperData, "Shipping_Method", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = shiperData.ShippingMethodTable.Rows[0]["ShippingMethodID"];
				UpdateTableRowByID("Shipping_Method", "ShippingMethodID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("Shipping Method", obj.ToString(), ActivityTypes.Update, null);
				UpdateTableRowInsertUpdateInfo("Shipping_Method", "ShippingMethodID", obj, sqlTransaction, isInsert: false);
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

		public ShippingMethodData GetShippingMethods()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "Shipping_Method";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddOrderByColumn("Shipping_Method", "ShippingMethodName");
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			ShippingMethodData shippingMethodData = new ShippingMethodData();
			FillDataSet(shippingMethodData, "Shipping_Method", sqlBuilder);
			return shippingMethodData;
		}

		public ShippingMethodData GetShippingMethodByID(string shippingMethodID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ShippingMethodID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = shippingMethodID;
			commandHelper.TableName = "Shipping_Method";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ShippingMethodData shippingMethodData = new ShippingMethodData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(shippingMethodData, "Shipping_Method", sqlBuilder);
			return shippingMethodData;
		}

		public bool DeleteShippingMethod(string shippingMethodID)
		{
			bool flag = true;
			try
			{
				GetShippingMethodCompanyNameByID(shippingMethodID);
				string commandText = "DELETE FROM Shipping_Method WHERE ShippingMethodID = " + shippingMethodID.ToString();
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("ShippingMethod", shippingMethodID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetShippingMethodCompanyNameByID(object id)
		{
			try
			{
				object obj = ExecuteSelectScalar("Shipping_Method", "ShippingMethodID", id, "ShippingMethodName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public string GetShippingMethodIDByName(string companyName)
		{
			try
			{
				object obj = ExecuteSelectScalar("Shipping_Method", "ShippingMethodName", companyName, "ShippingMethodID");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public bool ExistShippingMethod(string companyName)
		{
			try
			{
				return IsTableFieldValueExist("Shipping_Method", "ShippingMethodName", companyName);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetShippingMethodsByFields(params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(dataSet, "Shipping_Method", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetShippingMethodsByFields(int[] shippingMethodsID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (shippingMethodsID != null && shippingMethodsID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "ShippingMethodID";
				commandHelper.FieldValue = shippingMethodsID;
				commandHelper.TableName = "Shipping_Method";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(dataSet, "Shipping_Method", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetShippingMethodsByFields(bool isInactive, int[] shippingMethodsID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (shippingMethodsID != null && shippingMethodsID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "ShippingMethodID";
				commandHelper.FieldValue = shippingMethodsID;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Shipping_Method";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "Inactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.AllowNull = true;
				commandHelper2.TableName = "Shipping_Method";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(dataSet, "Shipping_Method", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetShippingMethodsList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ShippingMethodID [Method Code],ShippingMethodName [Method Name],ContactName [Contact Name],Phone,Note,Inactive [I]\r\n                           FROM Shipping_Method ORDER BY ShippingMethodID,ShippingMethodName";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}

		public DataSet GetShippingMethodsComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ShippingMethodID [Code],ShippingMethodName [Name]\r\n                           FROM Shipping_Method ORDER BY ShippingMethodID,ShippingMethodName";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}
	}
}
