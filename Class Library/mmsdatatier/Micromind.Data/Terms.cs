using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Terms : StoreObject
	{
		private const string PAYMENTTERMID_PARM = "@PaymentTermID";

		private const string TERMNAME_PARM = "@TermName";

		private const string NETDAYS_PARM = "@NetDays";

		private const string DISCOUNTDAYS_PARM = "@DiscountDays";

		private const string DISCOUNTAMOUNT_PARM = "@DiscountAmount";

		private const string TERMTYPE_PARM = "@TermType";

		private const string ISINSTALLMENTS_PARM = "@IsInstallments";

		private const string DISCOUNTTYPE_PARM = "@DiscountType";

		private const string NOTE_PARM = "@Note";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PERCENTAGE_PARM = "@Percentage";

		private const string DAYS_PARM = "@Days";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public Terms(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Payment_Term", new FieldValue("PaymentTermID", "@PaymentTermID", isUpdateConditionField: true), new FieldValue("TermName", "@TermName"), new FieldValue("NetDays", "@NetDays"), new FieldValue("DiscountDays", "@DiscountDays"), new FieldValue("IsInstallments", "@IsInstallments"), new FieldValue("DiscountAmount", "@DiscountAmount"), new FieldValue("TermType", "@TermType"), new FieldValue("DiscountType", "@DiscountType"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Payment_Term", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PaymentTermID", SqlDbType.NVarChar);
			parameters.Add("@TermName", SqlDbType.NVarChar);
			parameters.Add("@NetDays", SqlDbType.SmallInt);
			parameters.Add("@DiscountDays", SqlDbType.SmallInt);
			parameters.Add("@DiscountAmount", SqlDbType.Money);
			parameters.Add("@DiscountType", SqlDbType.SmallInt);
			parameters.Add("@TermType", SqlDbType.SmallInt);
			parameters.Add("@IsInstallments", SqlDbType.Bit);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@PaymentTermID"].SourceColumn = "PaymentTermID";
			parameters["@TermName"].SourceColumn = "TermName";
			parameters["@NetDays"].SourceColumn = "NetDays";
			parameters["@DiscountDays"].SourceColumn = "DiscountDays";
			parameters["@DiscountAmount"].SourceColumn = "DiscountAmount";
			parameters["@DiscountType"].SourceColumn = "DiscountType";
			parameters["@TermType"].SourceColumn = "TermType";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@IsInstallments"].SourceColumn = "IsInstallments";
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

		private string GetInsertUpdateInstallmentsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Payment_Term_Installment", new FieldValue("PaymentTermID", "@PaymentTermID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("TermType", "@TermType"), new FieldValue("Days", "@Days"), new FieldValue("Percentage", "@Percentage"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInstallmentsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand == null)
				{
					updateCommand = new SqlCommand(GetInsertUpdateInstallmentsText(isUpdate: true), base.DBConfig.Connection);
				}
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateInstallmentsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PaymentTermID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Days", SqlDbType.Int);
			parameters.Add("@Percentage", SqlDbType.SmallInt);
			parameters.Add("@TermType", SqlDbType.TinyInt);
			parameters["@PaymentTermID"].SourceColumn = "PaymentTermID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@Percentage"].SourceColumn = "Percentage";
			parameters["@TermType"].SourceColumn = "TermType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateTerm(PaymentTermData termData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = (isUpdate ? Update(termData, "Payment_Term", insertUpdateCommand) : Insert(termData, "Payment_Term", insertUpdateCommand));
				object obj = "";
				obj = termData.TermTable.Rows[0]["PaymentTermID"];
				if (isUpdate)
				{
					flag &= DeleteInstallmentRows(obj.ToString(), sqlTransaction);
				}
				if (flag)
				{
					insertUpdateCommand = GetInsertUpdateInstallmentsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					Insert(termData, "Payment_Term_Installment", insertUpdateCommand);
				}
				termData.TermTable.Rows[0]["PaymentTermID"] = obj;
				UpdateTableRowByID("Payment_Term", "PaymentTermID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = termData.TermTable.Rows[0]["TermName"].ToString();
				if (!flag)
				{
					return flag;
				}
				if (!isUpdate)
				{
					AddActivityLog("Terms", entiyID, ActivityTypes.Add, sqlTransaction);
				}
				else
				{
					AddActivityLog("Terms", obj.ToString(), ActivityTypes.Update, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Payment_Term", "PaymentTermID", obj, sqlTransaction, !isUpdate);
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

		internal bool DeleteInstallmentRows(string termID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Payment_Term_Installment WHERE PaymentTermID = '" + termID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public PaymentTermData GetTermByID(string termID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PaymentTermID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = termID;
			commandHelper.TableName = "Payment_Term";
			sqlBuilder.AddCommandHelper(commandHelper);
			PaymentTermData paymentTermData = new PaymentTermData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(paymentTermData, "Payment_Term", sqlBuilder);
				if (paymentTermData == null || paymentTermData.Tables.Count == 0 || paymentTermData.Tables["Payment_Term"].Rows.Count == 0)
				{
					return null;
				}
				string textCommand = "SELECT * from Payment_Term_Installment TGD\r\n                        WHERE TGD.PaymentTermID ='" + termID + "' ORDER BY TGD.RowIndex ";
				FillDataSet(paymentTermData, "Payment_Term_Installment", textCommand);
				return paymentTermData;
			}
			catch
			{
				throw;
			}
		}

		public PaymentTermData GetTerms()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "Payment_Term";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = false;
			PaymentTermData paymentTermData = new PaymentTermData();
			try
			{
				FillDataSet(paymentTermData, "Payment_Term", sqlBuilder);
				return paymentTermData;
			}
			catch
			{
				throw;
			}
		}

		public PaymentTermData GetTermsByFields(params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Payment_Term");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			sqlBuilder.AddColumn("Payment_Term", "TermName");
			sqlBuilder.AddOrderByColumn("Payment_Term", "TermName");
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			PaymentTermData paymentTermData = new PaymentTermData();
			try
			{
				FillDataSet(paymentTermData, "Payment_Term", sqlBuilder);
				return paymentTermData;
			}
			catch
			{
				throw;
			}
		}

		public PaymentTermData GetTermsByFields(int[] termsID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Payment_Term");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (termsID != null && termsID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "PaymentTermID";
				commandHelper.FieldValue = termsID;
				commandHelper.TableName = "Payment_Term";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.AddColumn("Payment_Term", "TermName");
			sqlBuilder.AddOrderByColumn("Payment_Term", "TermName");
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			PaymentTermData paymentTermData = new PaymentTermData();
			try
			{
				FillDataSet(paymentTermData, "Payment_Term", sqlBuilder);
				return paymentTermData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteTerm(string termID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Payment_Term WHERE PaymentTermID = '" + termID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Terms", termID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetTermNameByID(object id)
		{
			try
			{
				object obj = ExecuteSelectScalar("Payment_Term", "PaymentTermID", id, "TermName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public bool ExistTermName(string termName)
		{
			try
			{
				return IsTableFieldValueExist("Payment_Term", "TermName", termName);
			}
			catch
			{
				throw;
			}
		}

		public PaymentTermData GetTermByName(string termName)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TermName";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = termName;
			commandHelper.TableName = "Payment_Term";
			commandHelper.SqlOp.CompareValueOp = CompareValueOperator.Like;
			commandHelper.AddWildcardAfterValue = true;
			sqlBuilder.AddCommandHelper(commandHelper);
			PaymentTermData paymentTermData = new PaymentTermData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(paymentTermData, "Payment_Term", sqlBuilder);
				return paymentTermData;
			}
			catch
			{
				throw;
			}
			finally
			{
				commandHelper = null;
				sqlBuilder = null;
			}
		}

		public string GetTermIDByName(string termName)
		{
			try
			{
				object obj = ExecuteSelectScalar("Payment_Term", "TermName", termName, "PaymentTermID");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public DataSet GetPaymentTermsList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PaymentTermID [Term Code],TermName [Term Name],Note,Inactive\r\n                           FROM Payment_Term ORDER BY PaymentTermID,TermName";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}

		public DataSet GetPaymentTermsComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PaymentTermID [Code],TermName [Name]\r\n                           FROM Payment_Term ORDER BY PaymentTermID,TermName";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}
	}
}
