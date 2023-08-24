using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class LeaveType : StoreObject
	{
		private const string LEAVETYPEID_PARM = "@LeaveTypeID";

		private const string LEAVETYPENAME_PARM = "@LeaveTypeName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string DAYS_PARM = "@Days";

		private const string ISPAYABLE_PARM = "@IsPayable";

		private const string ISCUMULATIVE_PARM = "@IsCumulative";

		private const string ISANNUAL_PARM = "@IsAnnual";

		private const string ACTIVATEHC_PARM = "@ActivateHC";

		private const string DEDUCTIONPROPORTION_PARM = "@DeductionProportion";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string LEAVETYPE_TABLE = "Leave_Type";

		private const string MONTHGREATER1_PARM = "@MonthGreater1";

		private const string MONTHLESSER1_PARM = "@MonthLesser1";

		private const string ALLOWEDDAYS1_PARM = "@AllowedDays1";

		private const string MONTHGREATER2_PARM = "@MonthGreater2";

		private const string MONTHLESSER2_PARM = "@MonthLesser2";

		private const string ALLOWEDDAYS2_PARM = "@AllowedDays2";

		private const string MONTHGREATER3_PARM = "@MonthGreater3";

		private const string MONTHLESSER3_PARM = "@MonthLesser3";

		private const string ALLOWEDDAYS3_PARM = "@AllowedDays3";

		private const string ISENCASH_PARM = "@IsEncash";

		private const string ISLEAVESETTLE_PARM = "@IsLeaveSettle";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public LeaveType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Leave_Type", new FieldValue("LeaveTypeID", "@LeaveTypeID", isUpdateConditionField: true), new FieldValue("LeaveTypeName", "@LeaveTypeName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Days", "@Days"), new FieldValue("IsPayable", "@IsPayable"), new FieldValue("IsAnnual", "@IsAnnual"), new FieldValue("ActivateHC", "@ActivateHC"), new FieldValue("DeductionProportion", "@DeductionProportion"), new FieldValue("IsCumulative", "@IsCumulative"), new FieldValue("AccountID", "@AccountID"), new FieldValue("MonthGreater1", "@MonthGreater1"), new FieldValue("MonthLesser1", "@MonthLesser1"), new FieldValue("AllowedDays1", "@AllowedDays1"), new FieldValue("MonthGreater2", "@MonthGreater2"), new FieldValue("MonthLesser2", "@MonthLesser2"), new FieldValue("AllowedDays2", "@AllowedDays2"), new FieldValue("MonthGreater3", "@MonthGreater3"), new FieldValue("MonthLesser3", "@MonthLesser3"), new FieldValue("AllowedDays3", "@AllowedDays3"), new FieldValue("IsEncash", "@IsEncash"), new FieldValue("IsLeaveSettle", "@IsLeaveSettle"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Leave_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@LeaveTypeID", SqlDbType.NVarChar);
			parameters.Add("@LeaveTypeName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@IsPayable", SqlDbType.Bit);
			parameters.Add("@IsCumulative", SqlDbType.Bit);
			parameters.Add("@IsAnnual", SqlDbType.Bit);
			parameters.Add("@ActivateHC", SqlDbType.Bit);
			parameters.Add("@DeductionProportion", SqlDbType.Float);
			parameters.Add("@Days", SqlDbType.SmallInt);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@MonthGreater1", SqlDbType.SmallInt);
			parameters.Add("@MonthLesser1", SqlDbType.SmallInt);
			parameters.Add("@AllowedDays1", SqlDbType.Decimal);
			parameters.Add("@MonthGreater2", SqlDbType.SmallInt);
			parameters.Add("@MonthLesser2", SqlDbType.SmallInt);
			parameters.Add("@AllowedDays2", SqlDbType.Decimal);
			parameters.Add("@MonthGreater3", SqlDbType.Real);
			parameters.Add("@MonthLesser3", SqlDbType.Real);
			parameters.Add("@AllowedDays3", SqlDbType.Decimal);
			parameters.Add("@IsEncash", SqlDbType.Bit);
			parameters.Add("@IsLeaveSettle", SqlDbType.Bit);
			parameters["@LeaveTypeID"].SourceColumn = "LeaveTypeID";
			parameters["@LeaveTypeName"].SourceColumn = "LeaveTypeName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@IsPayable"].SourceColumn = "IsPayable";
			parameters["@IsCumulative"].SourceColumn = "IsCumulative";
			parameters["@IsAnnual"].SourceColumn = "IsAnnual";
			parameters["@ActivateHC"].SourceColumn = "ActivateHC";
			parameters["@DeductionProportion"].SourceColumn = "DeductionProportion";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@MonthGreater1"].SourceColumn = "MonthGreater1";
			parameters["@MonthLesser1"].SourceColumn = "MonthLesser1";
			parameters["@AllowedDays1"].SourceColumn = "AllowedDays1";
			parameters["@MonthGreater2"].SourceColumn = "MonthGreater2";
			parameters["@MonthLesser2"].SourceColumn = "MonthLesser2";
			parameters["@AllowedDays2"].SourceColumn = "AllowedDays2";
			parameters["@MonthGreater3"].SourceColumn = "MonthGreater3";
			parameters["@MonthLesser3"].SourceColumn = "MonthLesser3";
			parameters["@AllowedDays3"].SourceColumn = "AllowedDays3";
			parameters["@IsEncash"].SourceColumn = "IsEncash";
			parameters["@IsLeaveSettle"].SourceColumn = "IsLeaveSettle";
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

		public bool InsertLeaveType(LeaveTypeData accountLeaveTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountLeaveTypeData, "Leave_Type", insertUpdateCommand);
				string text = accountLeaveTypeData.LeaveTypeTable.Rows[0]["LeaveTypeID"].ToString();
				AddActivityLog("Leave Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Leave_Type", "LeaveTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateLeaveType(LeaveTypeData accountLeaveTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountLeaveTypeData, "Leave_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountLeaveTypeData.LeaveTypeTable.Rows[0]["LeaveTypeID"];
				UpdateTableRowByID("Leave_Type", "LeaveTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountLeaveTypeData.LeaveTypeTable.Rows[0]["LeaveTypeName"].ToString();
				AddActivityLog("Leave Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Leave_Type", "LeaveTypeID", obj, sqlTransaction, isInsert: false);
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

		public LeaveTypeData GetLeaveType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Leave_Type");
			LeaveTypeData leaveTypeData = new LeaveTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(leaveTypeData, "Leave_Type", sqlBuilder);
			return leaveTypeData;
		}

		public bool DeleteLeaveType(string leaveTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Leave_Type WHERE LeaveTypeID = '" + leaveTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Leave Type", leaveTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public LeaveTypeData GetLeaveTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "LeaveTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Leave_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			LeaveTypeData leaveTypeData = new LeaveTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(leaveTypeData, "Leave_Type", sqlBuilder);
			return leaveTypeData;
		}

		public DataSet GetLeaveTypeByFields(params string[] columns)
		{
			return GetLeaveTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetLeaveTypeByFields(string[] leaveTypeID, params string[] columns)
		{
			return GetLeaveTypeByFields(leaveTypeID, isInactive: true, columns);
		}

		public DataSet GetLeaveTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Leave_Type");
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
				commandHelper.FieldName = "LeaveTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Leave_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Leave_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLeaveTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LeaveTypeID [Type Code],LeaveTypeName [Type Name],Days,IsPayable [Payable],IsCumulative [Cumulative],Inactive\r\n                           FROM Leave_Type ";
			FillDataSet(dataSet, "Leave_Type", textCommand);
			return dataSet;
		}

		public DataSet GetLeaveTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LeaveTypeID [Code],LeaveTypeName [Name],Days\r\n                           FROM Leave_Type ORDER BY LeaveTypeID,LeaveTypeName";
			FillDataSet(dataSet, "Leave_Type", textCommand);
			return dataSet;
		}

		public double GetDeductionProportion(string ID)
		{
			string exp = "SELECT ISNULL(DeductionProportion,0)\r\n                           FROM Leave_Type WHERE LeaveTypeID='" + ID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return double.Parse(obj.ToString());
			}
			return 0.0;
		}

		public DataSet GetEmployeeLeaveTypeComboList(string EmployeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT eld.LeaveTypeID [Code],LT.LeaveTypeName [Name],LT.Days\r\n                           FROM Leave_Type LT LEFT JOIN Employee_Type_Detail eld   ON eld.LeaveTypeID=LT.LeaveTypeID  LEFT JOIN Employee e ON e.ContractType=eld.TypeID\r\n\t\t\t\t\t\t   WHERE e.EmployeeID='" + EmployeeID + "'\r\n\t\t\t\t\t\t   ORDER BY eld.LeaveTypeID,LT.LeaveTypeName";
			FillDataSet(dataSet, "Leave_Type", textCommand);
			return dataSet;
		}
	}
}
