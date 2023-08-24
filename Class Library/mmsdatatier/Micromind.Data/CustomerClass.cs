using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomerClass : StoreObject
	{
		private const string CUSTOMERCLASSID_PARM = "@CustomerClassID";

		private const string CUSTOMERCLASSNAME_PARM = "@CustomerClassName";

		public const string NOTE_PARM = "@Note";

		private const string ARACCOUNTID_PARM = "@ARAccountID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string HASPOSACCESS_PARM = "@HasPOSAccess";

		private const string ISLPO_PARM = "@IsLPO";

		private const string ISPRO_PARM = "@IsPRO";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string CUSTOMERCLASS_TABLE = "Customer_Class";

		public CustomerClass(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Class", new FieldValue("ClassID", "@CustomerClassID", isUpdateConditionField: true), new FieldValue("ClassName", "@CustomerClassName"), new FieldValue("HasPOSAccess", "@HasPOSAccess"), new FieldValue("IsLPO", "@IsLPO"), new FieldValue("IsPRO", "@IsPRO"), new FieldValue("ARAccountID", "@ARAccountID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Customer_Class", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CustomerClassID", SqlDbType.NVarChar);
			parameters.Add("@CustomerClassName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@HasPOSAccess", SqlDbType.Bit);
			parameters.Add("@IsLPO", SqlDbType.Bit);
			parameters.Add("@IsPRO", SqlDbType.Bit);
			parameters.Add("@ARAccountID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@CustomerClassID"].SourceColumn = "ClassID";
			parameters["@CustomerClassName"].SourceColumn = "ClassName";
			parameters["@HasPOSAccess"].SourceColumn = "HasPOSAccess";
			parameters["@IsLPO"].SourceColumn = "IsLPO";
			parameters["@IsPRO"].SourceColumn = "IsPRO";
			parameters["@ARAccountID"].SourceColumn = "ARAccountID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		public bool InsertCustomerClass(CustomerClassData accountCustomerClassData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCustomerClassData, "Customer_Class", insertUpdateCommand);
				string text = accountCustomerClassData.CustomerClassTable.Rows[0]["ClassID"].ToString();
				AddActivityLog("Customer Class", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Class", "ClassID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCustomerClass(CustomerClassData accountCustomerClassData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCustomerClassData, "Customer_Class", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCustomerClassData.CustomerClassTable.Rows[0]["ClassID"];
				UpdateTableRowByID("Customer_Class", "ClassID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCustomerClassData.CustomerClassTable.Rows[0]["ClassName"].ToString();
				AddActivityLog("Customer Class", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Class", "ClassID", obj, sqlTransaction, isInsert: false);
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

		public CustomerClassData GetCustomerClass()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Class");
			CustomerClassData customerClassData = new CustomerClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customerClassData, "Customer_Class", sqlBuilder);
			return customerClassData;
		}

		public bool DeleteCustomerClass(string customerClassID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Customer_Class WHERE ClassID = '" + customerClassID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Customer Class", customerClassID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CustomerClassData GetCustomerClassByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ClassID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Customer_Class";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CustomerClassData customerClassData = new CustomerClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customerClassData, "Customer_Class", sqlBuilder);
			return customerClassData;
		}

		public DataSet GetCustomerClassByFields(params string[] columns)
		{
			return GetCustomerClassByFields(null, isInactive: true, columns);
		}

		public DataSet GetCustomerClassByFields(string[] customerClassID, params string[] columns)
		{
			return GetCustomerClassByFields(customerClassID, isInactive: true, columns);
		}

		public DataSet GetCustomerClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Class");
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
				commandHelper.FieldName = "ClassID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Customer_Class";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Customer_Class", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCustomerClassList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClassID [Code],ClassName [Class Name],Note,IsInactive [Inactive]\r\n                           FROM Customer_Class ";
			FillDataSet(dataSet, "Customer_Class", textCommand);
			return dataSet;
		}

		public DataSet GetCustomerClassComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClassID [Code],ClassName [Name]\r\n                           FROM Customer_Class \r\n                            WHERE IsInactive<>1  ORDER BY ClassID,ClassName";
			FillDataSet(dataSet, "Customer_Class", textCommand);
			return dataSet;
		}

		public DataSet GetCustomerTenantComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClassID [Code],ClassName [Name]\r\n                           FROM Customer_Class \r\n                            WHERE  ISPRO='1' AND IsInactive<>1   ORDER BY ClassID,ClassName";
			FillDataSet(dataSet, "Customer_Class", textCommand);
			return dataSet;
		}
	}
}
