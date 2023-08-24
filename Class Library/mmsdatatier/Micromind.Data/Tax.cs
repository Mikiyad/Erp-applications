using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Tax : StoreObject
	{
		private const string TAXCODE_PARM = "@TaxCode";

		private const string TAXNAME_PARM = "@TaxName";

		private const string TAX_TABLE = "Tax";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REAMRKS_PARM = "@Remarks";

		private const string SALESTAXACCOUNTID_PARM = "@SalesTaxAccountID";

		private const string PURCHASETAXACCOUNTID_PARM = "@PurchaseTaxAccountID";

		private const string TAXREVERSECHARGEACCOUNTID_PARM = "@TaxReverseChargeAccountID";

		private const string TAXID_PARM = "@TaxID";

		private const string TAXTYPE_PARM = "@TaxType";

		private const string CALCULATIONMETHOD_PARM = "@CalculationMethod";

		private const string TAXRATE_PARM = "@TaxRate";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ISPERCENT_PARM = "@IsPercent";

		private const string ISFIXED_PARM = "@IsFixed";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Tax(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Tax", new FieldValue("TaxCode", "@TaxCode", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("SalesTaxAccountID", "@SalesTaxAccountID"), new FieldValue("PurchaseTaxAccountID", "@PurchaseTaxAccountID"), new FieldValue("TaxReverseChargeAccountID", "@TaxReverseChargeAccountID"), new FieldValue("TaxID", "@TaxID"), new FieldValue("CalculationMethod", "@CalculationMethod"), new FieldValue("TaxType", "@TaxType"), new FieldValue("TaxRate", "@TaxRate"), new FieldValue("Inactive", "@Inactive"), new FieldValue("IsPercent", "@IsPercent"), new FieldValue("IsFixed", "@IsFixed"), new FieldValue("TaxName", "@TaxName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Tax", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TaxCode", SqlDbType.NVarChar);
			parameters.Add("@TaxName", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@SalesTaxAccountID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseTaxAccountID", SqlDbType.NVarChar);
			parameters.Add("@TaxReverseChargeAccountID", SqlDbType.NVarChar);
			parameters.Add("@CalculationMethod", SqlDbType.TinyInt);
			parameters.Add("@TaxID", SqlDbType.NVarChar);
			parameters.Add("@TaxType", SqlDbType.NVarChar);
			parameters.Add("@TaxRate", SqlDbType.Decimal);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@IsFixed", SqlDbType.Bit);
			parameters.Add("@IsPercent", SqlDbType.Bit);
			parameters["@TaxCode"].SourceColumn = "TaxCode";
			parameters["@TaxName"].SourceColumn = "TaxName";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@SalesTaxAccountID"].SourceColumn = "SalesTaxAccountID";
			parameters["@PurchaseTaxAccountID"].SourceColumn = "PurchaseTaxAccountID";
			parameters["@CalculationMethod"].SourceColumn = "CalculationMethod";
			parameters["@TaxReverseChargeAccountID"].SourceColumn = "TaxReverseChargeAccountID";
			parameters["@TaxID"].SourceColumn = "TaxID";
			parameters["@TaxType"].SourceColumn = "TaxType";
			parameters["@TaxRate"].SourceColumn = "TaxRate";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@IsPercent"].SourceColumn = "IsPercent";
			parameters["@IsFixed"].SourceColumn = "IsFixed";
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

		public bool InsertTax(TaxData accountTaxData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountTaxData, "Tax", insertUpdateCommand);
				string text = accountTaxData.TaxTable.Rows[0]["TaxCode"].ToString();
				AddActivityLog("Tax", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Tax", "TaxCode", text, sqlTransaction, isInsert: true);
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

		public bool UpdateTax(TaxData accountTaxData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountTaxData, "Tax", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountTaxData.TaxTable.Rows[0]["TaxCode"];
				UpdateTableRowByID("Tax", "TaxCode", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountTaxData.TaxTable.Rows[0]["TaxCode"].ToString();
				AddActivityLog("Tax", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Tax", "TaxCode", obj, sqlTransaction, isInsert: false);
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

		public TaxData GetTax()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Tax");
			TaxData taxData = new TaxData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(taxData, "Tax", sqlBuilder);
			return taxData;
		}

		public bool DeleteTax(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Tax WHERE TaxCode = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Tax", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public TaxData GetTaxByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TaxCode";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Tax";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			TaxData taxData = new TaxData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(taxData, "Tax", sqlBuilder);
			return taxData;
		}

		public DataSet GetTaxByFields(params string[] columns)
		{
			return GetTaxByFields(null, isInactive: true, columns);
		}

		public DataSet GetTaxByFields(string[] degreeID, params string[] columns)
		{
			return GetTaxByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetTaxByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Tax");
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
				commandHelper.FieldName = "TaxCode";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Tax";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Tax", sqlBuilder);
			return dataSet;
		}

		public string GetExpenseAccountID(string expenseID, SqlTransaction sqlTransaction)
		{
			new DataSet();
			string exp = "SELECT AccountID  \r\n                           FROM Expense_Code WHERE ExpenseID = '" + expenseID + "'";
			return ExecuteScalar(exp, sqlTransaction).ToString();
		}

		public DataSet GetTaxList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaxCode [Tax Code],TaxName [Tax Name]\r\n                           FROM Tax";
			FillDataSet(dataSet, "Tax", textCommand);
			return dataSet;
		}

		public DataSet GetTaxClassList(string ID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  TaxCode [TAX Code],\r\n                        TaxPercent [Tax Percent], RowIndex\r\n                         FROM Tax_ProductClass_Detail WHERE ClassId='" + ID + "' UNION select TaxID,  NUll, NULL from Tax where TaxID NOT IN (select TaxID from Tax_ProductClass_Detail WHERE ClassId='" + ID + "') ";
			FillDataSet(dataSet, "Tax", textCommand);
			return dataSet;
		}

		public DataSet GetTaxComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaxCode [Code], TaxName [Name], TaxRate\r\n                           FROM Tax  where Inactive=0 ORDER BY TaxCode, TaxName";
			FillDataSet(dataSet, "Tax", textCommand);
			return dataSet;
		}
	}
}
