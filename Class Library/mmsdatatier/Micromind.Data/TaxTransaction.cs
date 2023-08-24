using Micromind.Common;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TaxTransaction : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TAXLEVEL_PARM = "@TaxLevel";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXITEMID_PARM = "@TaxItemID";

		private const string TAXRATE_PARM = "@TaxRate";

		private const string CALCULATIONMETHOD_PARM = "@CalculationMethod";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string ORDERINDEX_PARM = "@OrderIndex";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		public TaxTransaction(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateTaxTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Tax_Detail", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TaxLevel", "@TaxLevel"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxItemID", "@TaxItemID"), new FieldValue("CalculationMethod", "@CalculationMethod"), new FieldValue("TaxRate", "@TaxRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("OrderIndex", "@OrderIndex"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("AccountID", "@AccountID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTaxTransactionDetailsCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateTaxTransactionText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@CalculationMethod", SqlDbType.TinyInt);
			parameters.Add("@TaxItemID", SqlDbType.NVarChar);
			parameters.Add("@TaxRate", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@OrderIndex", SqlDbType.Int);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@TaxLevel", SqlDbType.TinyInt);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TaxLevel"].SourceColumn = "TaxLevel";
			parameters["@CalculationMethod"].SourceColumn = "CalculationMethod";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxItemID"].SourceColumn = "TaxItemID";
			parameters["@TaxRate"].SourceColumn = "TaxRate";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@OrderIndex"].SourceColumn = "OrderIndex";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			return insertCommand;
		}

		public bool InsertUpdateTaxTransaction(DataSet taxTransactionData, string sysDocID, string voucherID, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (isUpdate)
				{
					flag &= DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				}
				SqlCommand insertUpdateTaxTransactionDetailsCommand = GetInsertUpdateTaxTransactionDetailsCommand(isUpdate);
				insertUpdateTaxTransactionDetailsCommand.Transaction = sqlTransaction;
				if (taxTransactionData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= Insert(taxTransactionData, "Tax_Detail", insertUpdateTaxTransactionDetailsCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save tax details because of an unexpected error. Please try again.");
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		internal bool DeleteTaxTransactionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Tax_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}
	}
}
