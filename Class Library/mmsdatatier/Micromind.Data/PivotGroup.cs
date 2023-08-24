using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PivotGroup : StoreObject
	{
		private const string PIVOTGROUPID_PARM = "@GroupID";

		private const string PIVOTGROUPNAME_PARM = "@GroupName";

		public const string PIVOTGROUP_TABLE = "Pivot_Group";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PivotGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Pivot_Group", new FieldValue("GroupName", "@GroupName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Pivot_Group", new FieldValue("GroupID", "@GroupID", isUpdateConditionField: true, checkForNullValue: true));
				sqlBuilder.AddInsertUpdateParameters("Pivot_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			if (isUpdate)
			{
				parameters.Add("@GroupID", SqlDbType.NVarChar);
			}
			parameters.Add("@GroupName", SqlDbType.NVarChar);
			if (isUpdate)
			{
				parameters["@GroupID"].SourceColumn = "GroupID";
			}
			parameters["@GroupName"].SourceColumn = "GroupName";
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

		public bool InsertPivotGroup(PivotGroupData accountPivotGroupData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPivotGroupData, "Pivot_Group", insertUpdateCommand);
				string text = accountPivotGroupData.PivotGroupTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("Pivot Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Pivot_Group", "GroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePivotGroup(PivotGroupData accountPivotGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPivotGroupData, "Pivot_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPivotGroupData.PivotGroupTable.Rows[0]["GroupID"];
				UpdateTableRowByID("Pivot_Group", "GroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPivotGroupData.PivotGroupTable.Rows[0]["GroupName"].ToString();
				AddActivityLog("Pivot Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Pivot_Group", "GroupID", obj, sqlTransaction, isInsert: false);
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

		public PivotGroupData GetPivotGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Pivot_Group");
			PivotGroupData pivotGroupData = new PivotGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pivotGroupData, "Pivot_Group", sqlBuilder);
			return pivotGroupData;
		}

		public bool DeletePivotGroup(string chartGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Pivot_Group WHERE GroupID = '" + chartGroupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Pivot Group", chartGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PivotGroupData GetPivotGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Pivot_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PivotGroupData pivotGroupData = new PivotGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pivotGroupData, "Pivot_Group", sqlBuilder);
			return pivotGroupData;
		}

		public DataSet GetPivotGroupByFields(params string[] columns)
		{
			return GetPivotGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetPivotGroupByFields(string[] chartGroupID, params string[] columns)
		{
			return GetPivotGroupByFields(chartGroupID, isInactive: true, columns);
		}

		public DataSet GetPivotGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Pivot_Group");
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
				commandHelper.TableName = "Pivot_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Pivot_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPivotGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID , GroupName \r\n                           FROM Pivot_Group ";
			FillDataSet(dataSet, "Pivot_Group", textCommand);
			return dataSet;
		}

		public DataSet GetPivotGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name]\r\n                           FROM Pivot_Group ORDER BY GroupID, GroupName";
			FillDataSet(dataSet, "Pivot_Group", textCommand);
			return dataSet;
		}

		public int CreateGroup(string groupName)
		{
			int num = 0;
			try
			{
				SqlCommand command = new SqlCommand(" INSERT INTO Pivot_Group (GroupName) VALUES('" + groupName + "');SELECT SCOPE_IDENTITY()");
				return int.Parse(ExecuteScalar(command).ToString());
			}
			catch
			{
				throw;
			}
		}
	}
}
