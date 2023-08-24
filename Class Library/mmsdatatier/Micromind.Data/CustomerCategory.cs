using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomerCategory : StoreObject
	{
		private const string CUSTOMERCATEGORYID_PARM = "@CategoryID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string ENTITYTYPE_PARM = "@EntityType";

		private const string CUSTOMERCATEGORYNAME_PARM = "@CategoryName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string CUSTOMERCATEGORY_TABLE = "Customer_Category";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CustomerCategory(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Category", new FieldValue("CategoryID", "@CategoryID", isUpdateConditionField: true), new FieldValue("CategoryName", "@CategoryName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Customer_Category", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@CategoryName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@CategoryName"].SourceColumn = "CategoryName";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		private string GetInsertUpdateDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Category_Detail", new FieldValue("CustomerID", "@CustomerID"), new FieldValue("EntityType", "@EntityType"), new FieldValue("CategoryID", "@CategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertCustomerCategory(CustomerCategoryData accountCustomerCategoryData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCustomerCategoryData, "Customer_Category", insertUpdateCommand);
				string text = accountCustomerCategoryData.CustomerCategoryTable.Rows[0]["CategoryID"].ToString();
				AddActivityLog("CustomerCategory", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Category", "CategoryID", text, sqlTransaction, isInsert: true);
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

		public bool InsertCustomerCategoryAssignment(CustomerCategoryData data, string customerID)
		{
			bool flag = true;
			SqlCommand insertUpdateDetailCommand = GetInsertUpdateDetailCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "DELETE FROM CUSTOMER_CATEGORY_DETAIL WHERE CUSTOMERid='" + customerID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				insertUpdateDetailCommand.Transaction = sqlTransaction;
				if (data.CustomerCategoryDetailTable.Rows.Count <= 0)
				{
					return flag;
				}
				flag &= Insert(data, "Customer_Category_Detail", insertUpdateDetailCommand);
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

		public bool UpdateCustomerCategory(CustomerCategoryData accountCustomerCategoryData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCustomerCategoryData, "Customer_Category", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCustomerCategoryData.CustomerCategoryTable.Rows[0]["CategoryID"];
				UpdateTableRowByID("Customer_Category", "CategoryID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCustomerCategoryData.CustomerCategoryTable.Rows[0]["CategoryName"].ToString();
				AddActivityLog("CustomerCategory", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Category", "CategoryID", obj, sqlTransaction, isInsert: false);
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

		public CustomerCategoryData GetCustomerCategory()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Category");
			CustomerCategoryData customerCategoryData = new CustomerCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customerCategoryData, "Customer_Category", sqlBuilder);
			return customerCategoryData;
		}

		public bool DeleteCustomerCategory(string customerCategoryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Customer_Category WHERE CategoryID = '" + customerCategoryID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CustomerCategory", customerCategoryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CustomerCategoryData GetCustomerCategoryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CategoryID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Customer_Category";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CustomerCategoryData customerCategoryData = new CustomerCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customerCategoryData, "Customer_Category", sqlBuilder);
			return customerCategoryData;
		}

		public DataSet GetCustomerCategoryByFields(params string[] columns)
		{
			return GetCustomerCategoryByFields(null, isInactive: true, columns);
		}

		public DataSet GetCustomerCategoryByFields(string[] customerCategoryID, params string[] columns)
		{
			return GetCustomerCategoryByFields(customerCategoryID, isInactive: true, columns);
		}

		public DataSet GetCustomerCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Category");
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
				commandHelper.FieldName = "CategoryID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Customer_Category";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Customer_Category", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCustomerCategoryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name],Note,Inactive\r\n                           FROM Customer_Category ";
			FillDataSet(dataSet, "Customer_Category", textCommand);
			return dataSet;
		}

		public DataSet GetCustomerAssignedCategorysList(string entityID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "SELECT CONVERT(bit,(SELECT COUNT (CustomerID) FROM Customer WHERE CustomerID = '" + entityID + "' \r\n\t\t                            AND CustomerID IN (SELECT CustomerID FROM Customer_Category_Detail WHERE EntityType = 1 AND CategoryID = CG.CategoryID))) AS C,\r\n                             CategoryID AS Code,CategoryName Name FROM Customer_Category CG ";
			FillDataSet(dataSet, "Customer_Category_Detail", text);
			return dataSet;
		}

		public DataSet GetCustomerCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name]\r\n                           FROM Customer_Category ORDER BY CategoryID,CategoryName";
			FillDataSet(dataSet, "Customer_Category", textCommand);
			return dataSet;
		}
	}
}
