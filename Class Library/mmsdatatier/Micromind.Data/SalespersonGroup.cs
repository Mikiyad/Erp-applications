using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SalespersonGroup : StoreObject
	{
		private const string SALESPERSONGROUPID_PARM = "@GroupID";

		private const string SALESPERSONGROUPNAME_PARM = "@GroupName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string SALESPERSONGROUP_TABLE = "Salesperson_Group";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public SalespersonGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Salesperson_Group", new FieldValue("GroupID", "@GroupID", isUpdateConditionField: true), new FieldValue("GroupName", "@GroupName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Salesperson_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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

		public bool InsertSalespersonGroup(SalespersonGroupData accountSalespersonGroupData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountSalespersonGroupData, "Salesperson_Group", insertUpdateCommand);
				string text = accountSalespersonGroupData.SalespersonGroupTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("SalespersonGroup", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Salesperson_Group", "GroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateSalespersonGroup(SalespersonGroupData accountSalespersonGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountSalespersonGroupData, "Salesperson_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountSalespersonGroupData.SalespersonGroupTable.Rows[0]["GroupID"];
				UpdateTableRowByID("Salesperson_Group", "GroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountSalespersonGroupData.SalespersonGroupTable.Rows[0]["GroupName"].ToString();
				AddActivityLog("SalespersonGroup", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Salesperson_Group", "GroupID", obj, sqlTransaction, isInsert: false);
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

		public SalespersonGroupData GetSalespersonGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Salesperson_Group");
			SalespersonGroupData salespersonGroupData = new SalespersonGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(salespersonGroupData, "Salesperson_Group", sqlBuilder);
			return salespersonGroupData;
		}

		public bool DeleteSalespersonGroup(string customerGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Salesperson_Group WHERE GroupID = '" + customerGroupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("SalespersonGroup", customerGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public SalespersonGroupData GetSalespersonGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Salesperson_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			SalespersonGroupData salespersonGroupData = new SalespersonGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(salespersonGroupData, "Salesperson_Group", sqlBuilder);
			return salespersonGroupData;
		}

		public DataSet GetSalespersonGroupByFields(params string[] columns)
		{
			return GetSalespersonGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetSalespersonGroupByFields(string[] customerGroupID, params string[] columns)
		{
			return GetSalespersonGroupByFields(customerGroupID, isInactive: true, columns);
		}

		public DataSet GetSalespersonGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Salesperson_Group");
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
				commandHelper.TableName = "Salesperson_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Salesperson_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetSalespersonGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name],Note,Inactive\r\n                           FROM Salesperson_Group ";
			FillDataSet(dataSet, "Salesperson_Group", textCommand);
			return dataSet;
		}

		public DataSet GetSalespersonAssignedGroupsList(string customerID)
		{
			DataSet result = new DataSet();
			_ = "SELECT CONVERT(bit,(SELECT COUNT (SalespersonID) FROM Salesperson WHERE SalespersonID = '" + customerID + "' \r\n\t\t                            AND SalespersonID IN (SELECT SalespersonID FROM Salesperson_Group_Detail WHERE GroupID = CG.GroupID))) AS C,\r\n                             GroupID,GroupName FROM Salesperson_Group CG ";
			return result;
		}

		public DataSet GetSalespersonGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name]\r\n                           FROM Salesperson_Group ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "Salesperson_Group", textCommand);
			return dataSet;
		}
	}
}
