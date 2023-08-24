using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomerGroup : StoreObject
	{
		private const string CUSTOMERGROUPID_PARM = "@GroupID";

		private const string CUSTOMERGROUPNAME_PARM = "@GroupName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string CUSTOMERGROUP_TABLE = "Customer_Group";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CustomerGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Group", new FieldValue("GroupID", "@GroupID", isUpdateConditionField: true), new FieldValue("GroupName", "@GroupName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Customer_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@GroupName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@GroupName"].SourceColumn = "GroupName";
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

		public bool InsertCustomerGroup(CustomerGroupData accountCustomerGroupData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCustomerGroupData, "Customer_Group", insertUpdateCommand);
				string text = accountCustomerGroupData.CustomerGroupTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("CustomerGroup", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Group", "GroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCustomerGroup(CustomerGroupData accountCustomerGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCustomerGroupData, "Customer_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCustomerGroupData.CustomerGroupTable.Rows[0]["GroupID"];
				UpdateTableRowByID("Customer_Group", "GroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCustomerGroupData.CustomerGroupTable.Rows[0]["GroupName"].ToString();
				AddActivityLog("CustomerGroup", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Group", "GroupID", obj, sqlTransaction, isInsert: false);
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

		public CustomerGroupData GetCustomerGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Group");
			CustomerGroupData customerGroupData = new CustomerGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customerGroupData, "Customer_Group", sqlBuilder);
			return customerGroupData;
		}

		public bool DeleteCustomerGroup(string customerGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Customer_Group WHERE GroupID = '" + customerGroupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CustomerGroup", customerGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CustomerGroupData GetCustomerGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Customer_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CustomerGroupData customerGroupData = new CustomerGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customerGroupData, "Customer_Group", sqlBuilder);
			return customerGroupData;
		}

		public DataSet GetCustomerGroupByFields(params string[] columns)
		{
			return GetCustomerGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetCustomerGroupByFields(string[] customerGroupID, params string[] columns)
		{
			return GetCustomerGroupByFields(customerGroupID, isInactive: true, columns);
		}

		public DataSet GetCustomerGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Group");
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
				commandHelper.FieldName = "GroupID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Customer_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Customer_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCustomerGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name],Note,Inactive\r\n                           FROM Customer_Group ";
			FillDataSet(dataSet, "Customer_Group", textCommand);
			return dataSet;
		}

		public DataSet GetCustomerAssignedGroupsList(string customerID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CONVERT(bit,(SELECT COUNT (CustomerID) FROM Customer WHERE CustomerID = '" + customerID + "' \r\n\t\t                            AND CustomerID IN (SELECT CustomerID FROM Customer_Group_Detail WHERE GroupID = CG.GroupID))) AS C,\r\n                             GroupID,GroupName FROM Customer_Group CG ";
			FillDataSet(dataSet, "Customer_Group_Detail", textCommand);
			return dataSet;
		}

		public DataSet GetCustomerGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name]\r\n                           FROM Customer_Group ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "Customer_Group", textCommand);
			return dataSet;
		}
	}
}
