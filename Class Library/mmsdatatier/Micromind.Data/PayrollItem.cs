using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PayrollItem : StoreObject
	{
		private const string PAYROLLITEMID_PARM = "@PayrollItemID";

		private const string PAYROLLITEMNAME_PARM = "@PayrollItemName";

		private const string PAYROLLITEMTYPE_PARM = "@PayrollItemType";

		private const string PAYCODETYPE_PARM = "@PayCodeType";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NOTE_PARM = "@Note";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string INLEAVESALARY_PARM = "@InLeaveSalary";

		private const string INDEDUCTION_PARM = "@InDeduction";

		private const string INSERVICEBENEFIT_PARM = "@InServiceBenefit";

		private const string INOVERTIME_PARM = "@InOvertime";

		private const string INFIXED_PARM = "@InFixed";

		private const string INSALARYDEDUCTION_PARM = "@InSalaryDeduction";

		private const string PAYROLLITEM_TABLE = "PayrollItem";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PayrollItem(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("PayrollItem", new FieldValue("PayrollItemID", "@PayrollItemID", isUpdateConditionField: true), new FieldValue("PayrollItemName", "@PayrollItemName"), new FieldValue("PayrollItemType", "@PayrollItemType"), new FieldValue("PayCodeType", "@PayCodeType"), new FieldValue("Note", "@Note"), new FieldValue("Inactive", "@Inactive"), new FieldValue("AccountID", "@AccountID"), new FieldValue("InLeaveSalary", "@InLeaveSalary"), new FieldValue("InDeduction", "@InDeduction"), new FieldValue("InServiceBenefit", "@InServiceBenefit"), new FieldValue("InFixed", "@InFixed"), new FieldValue("InSalaryDeduction", "@InSalaryDeduction"), new FieldValue("InOvertime", "@InOvertime"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("PayrollItem", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PayrollItemID", SqlDbType.NVarChar);
			parameters.Add("@PayrollItemName", SqlDbType.NVarChar);
			parameters.Add("@PayrollItemType", SqlDbType.NVarChar);
			parameters.Add("@PayCodeType", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@InLeaveSalary", SqlDbType.Bit);
			parameters.Add("@InDeduction", SqlDbType.Bit);
			parameters.Add("@InServiceBenefit", SqlDbType.Bit);
			parameters.Add("@InOvertime", SqlDbType.Bit);
			parameters.Add("@InFixed", SqlDbType.Bit);
			parameters.Add("@InSalaryDeduction", SqlDbType.Bit);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters["@PayrollItemID"].SourceColumn = "PayrollItemID";
			parameters["@PayrollItemName"].SourceColumn = "PayrollItemName";
			parameters["@PayrollItemType"].SourceColumn = "PayrollItemType";
			parameters["@PayCodeType"].SourceColumn = "PayCodeType";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@InLeaveSalary"].SourceColumn = "InLeaveSalary";
			parameters["@InDeduction"].SourceColumn = "InDeduction";
			parameters["@InServiceBenefit"].SourceColumn = "InServiceBenefit";
			parameters["@InOvertime"].SourceColumn = "InOvertime";
			parameters["@InFixed"].SourceColumn = "InFixed";
			parameters["@InSalaryDeduction"].SourceColumn = "InSalaryDeduction";
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

		public bool InsertPayrollItem(PayrollItemData accountPayrollItemData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPayrollItemData, "PayrollItem", insertUpdateCommand);
				string text = accountPayrollItemData.PayrollItemTable.Rows[0]["PayrollItemID"].ToString();
				AddActivityLog("Payroll Item", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("PayrollItem", "PayrollItemID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePayrollItem(PayrollItemData accountPayrollItemData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPayrollItemData, "PayrollItem", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPayrollItemData.PayrollItemTable.Rows[0]["PayrollItemID"];
				UpdateTableRowByID("PayrollItem", "PayrollItemID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPayrollItemData.PayrollItemTable.Rows[0]["PayrollItemName"].ToString();
				AddActivityLog("Payroll Item", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("PayrollItem", "PayrollItemID", obj, sqlTransaction, isInsert: false);
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

		public PayrollItemData GetPayrollItem()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("PayrollItem");
			PayrollItemData payrollItemData = new PayrollItemData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(payrollItemData, "PayrollItem", sqlBuilder);
			return payrollItemData;
		}

		public bool DeletePayrollItem(string payrollItemID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM PayrollItem WHERE PayrollItemID = '" + payrollItemID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Payroll Item", payrollItemID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PayrollItemData GetPayrollItemByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PayrollItemID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "PayrollItem";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PayrollItemData payrollItemData = new PayrollItemData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(payrollItemData, "PayrollItem", sqlBuilder);
			return payrollItemData;
		}

		public DataSet GetPayrollItemByFields(params string[] columns)
		{
			return GetPayrollItemByFields(null, isInactive: true, columns);
		}

		public DataSet GetPayrollItemByFields(string[] payrollItemID, params string[] columns)
		{
			return GetPayrollItemByFields(payrollItemID, isInactive: true, columns);
		}

		public DataSet GetPayrollItemByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("PayrollItem");
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
				commandHelper.FieldName = "PayrollItemID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "PayrollItem";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "PayrollItem", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPayrollItemList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PayrollItemID [PayrollItem Code],PayrollItemName [PayrollItem Name],Note,Inactive\r\n                           FROM PayrollItem Where PayrollItemType=1";
			FillDataSet(dataSet, "PayrollItem", textCommand);
			return dataSet;
		}

		public DataSet GetPayrollDeductionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PayrollItemID [PayrollItem Code],PayrollItemName [PayrollItem Name],Note,Inactive\r\n                           FROM PayrollItem Where PayrollItemType=2";
			FillDataSet(dataSet, "PayrollItem", textCommand);
			return dataSet;
		}

		public DataSet GetPayrollItemListforWeb()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PayrollItemID [PayrollItem Code],PayrollItemName [PayrollItem Name],Note,Inactive\r\n                           FROM PayrollItem ";
			FillDataSet(dataSet, "PayrollItem", textCommand);
			return dataSet;
		}

		public DataSet GetPayrollItemComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PayrollItemID [Code],PayrollItemName [Name],PayrollItemType,PayCodeType,InDeduction\r\n                           FROM PayrollItem ORDER BY PayrollItemID,PayrollItemName";
			FillDataSet(dataSet, "PayrollItem", textCommand);
			return dataSet;
		}

		public DataSet GetPayrollProfileReport(string fromPayrollItem, string toPayrollItem, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT PayrollItem.PayrollItemID,PayrollItem.PayrollItemName,PayrollItem.PayrollItemType,PayrollItem.PayCodeType,PayrollItem.InDeduction,PayrollItem.Inactive\r\n                           FROM PayrollItem \r\n\t\t\t\t\t\t   WHERE PayrollItem.PayrollItemID>='" + fromPayrollItem + "' AND PayrollItem.PayrollItemID<='" + toPayrollItem + "'\r\n                           ORDER BY PayrollItemID,PayrollItemName ";
			if (fromPayrollItem != "")
			{
				text = text + " AND PayrollItem.PayrollItemID>='" + fromPayrollItem + "'";
			}
			if (toPayrollItem != "")
			{
				text = text + " AND PayrollItem.PayrollItemID<='" + toPayrollItem + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(Status,1) = 1 ";
			}
			FillDataSet(dataSet, "PayrollItem", text);
			return dataSet;
		}
	}
}
