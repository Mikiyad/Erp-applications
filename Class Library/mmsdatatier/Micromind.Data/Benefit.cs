using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Benefit : StoreObject
	{
		private const string BENEFITID_PARM = "@BenefitID";

		private const string BENEFITNAME_PARM = "@BenefitName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		private const string ACCOUNTID_PARM = "@AccountID";

		public const string BENEFIT_TABLE = "Benefit";

		private const string ISNONFINANCIAL_PARM = "@IsNonFinancial";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Benefit(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Benefit", new FieldValue("BenefitID", "@BenefitID", isUpdateConditionField: true), new FieldValue("BenefitName", "@BenefitName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("AccountID", "@AccountID"), new FieldValue("IsNonFinancial", "@IsNonFinancial"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Benefit", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BenefitID", SqlDbType.NVarChar);
			parameters.Add("@BenefitName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@IsNonFinancial", SqlDbType.Bit);
			parameters["@BenefitID"].SourceColumn = "BenefitID";
			parameters["@BenefitName"].SourceColumn = "BenefitName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@IsNonFinancial"].SourceColumn = "IsNonFinancial";
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

		public bool InsertBenefit(BenefitData accountBenefitData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountBenefitData, "Benefit", insertUpdateCommand);
				string text = accountBenefitData.BenefitTable.Rows[0]["BenefitID"].ToString();
				AddActivityLog("Benefit", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Benefit", "BenefitID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateBenefit(BenefitData accountBenefitData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountBenefitData, "Benefit", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountBenefitData.BenefitTable.Rows[0]["BenefitID"];
				UpdateTableRowByID("Benefit", "BenefitID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountBenefitData.BenefitTable.Rows[0]["BenefitName"].ToString();
				AddActivityLog("Benefit", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Benefit", "BenefitID", obj, sqlTransaction, isInsert: false);
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

		public BenefitData GetBenefit()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Benefit");
			BenefitData benefitData = new BenefitData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(benefitData, "Benefit", sqlBuilder);
			return benefitData;
		}

		public bool DeleteBenefit(string benefitID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Benefit WHERE BenefitID = '" + benefitID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Benefit", benefitID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public BenefitData GetBenefitByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BenefitID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Benefit";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			BenefitData benefitData = new BenefitData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(benefitData, "Benefit", sqlBuilder);
			return benefitData;
		}

		public DataSet GetBenefitByFields(params string[] columns)
		{
			return GetBenefitByFields(null, isInactive: true, columns);
		}

		public DataSet GetBenefitByFields(string[] benefitID, params string[] columns)
		{
			return GetBenefitByFields(benefitID, isInactive: true, columns);
		}

		public DataSet GetBenefitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Benefit");
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
				commandHelper.FieldName = "BenefitID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Benefit";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Benefit", sqlBuilder);
			return dataSet;
		}

		public DataSet GetBenefitList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BenefitID [Benefit Code],BenefitName [Benefit Name],Note,Inactive\r\n                           FROM Benefit ";
			FillDataSet(dataSet, "Benefit", textCommand);
			return dataSet;
		}

		public DataSet GetBenefitComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BenefitID [Code],BenefitName [Name],ISNULL(IsNonFinancial,0)as NonFinancial FROM Benefit ORDER BY BenefitID,BenefitName";
			FillDataSet(dataSet, "Benefit", textCommand);
			return dataSet;
		}
	}
}
