using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PaymentMethods : StoreObject
	{
		private const string PAYMENTMETHODID_PARM = "@PaymentMethodID";

		private const string METHODNAME_PARM = "@MethodName";

		private const string DEFAULTACCOUNTID_PARM = "@DefaultAccountID";

		private const string NOTE_PARM = "@Note";

		private const string METHODTYPE_PARM = "@MethodType";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public PaymentMethods(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Payment_Method", new FieldValue("PaymentMethodID", "@PaymentMethodID", isUpdateConditionField: true), new FieldValue("MethodName", "@MethodName"), new FieldValue("MethodType", "@MethodType"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Payment_Method", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PaymentMethodID", SqlDbType.NVarChar);
			parameters.Add("@MethodName", SqlDbType.NVarChar);
			parameters.Add("@MethodType", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NText);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@PaymentMethodID"].SourceColumn = "PaymentMethodID";
			parameters["@MethodName"].SourceColumn = "MethodName";
			parameters["@MethodType"].SourceColumn = "MethodType";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertPaymentMethod(PaymentMethodData paymentMethodData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(paymentMethodData, "Payment_Method", insertUpdateCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("Payment_Method", insertUpdateCommand);
				paymentMethodData.PaymentMethodTable.Rows[0]["PaymentMethodID"] = insertedRowIdentity;
				UpdateTableRowByID("Payment_Method", "PaymentMethodID", "DateUpdated", insertedRowIdentity, DateTime.Now, sqlTransaction);
				if (flag)
				{
					string entiyID = paymentMethodData.PaymentMethodTable.Rows[0]["MethodName"].ToString();
					AddActivityLog("Payment Method", entiyID, ActivityTypes.Add, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowInsertUpdateInfo("Payment_Method", "PaymentMethodID", insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool UpdatePaymentMethod(PaymentMethodData paymentMethodData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(paymentMethodData, "Payment_Method", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = paymentMethodData.PaymentMethodTable.Rows[0]["PaymentMethodID"];
				UpdateTableRowByID("Payment_Method", "PaymentMethodID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				if (flag)
				{
					string entiyID = paymentMethodData.PaymentMethodTable.Rows[0]["MethodName"].ToString();
					AddActivityLog("Payment Method", entiyID, ActivityTypes.Update, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowInsertUpdateInfo("Payment_Method", "PaymentMethodID", obj, sqlTransaction, isInsert: false);
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

		public DataSet GetPaymentMethodsByFields(params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Payment_Method");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Payment_Method", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPaymentMethodsByFields(int[] methodID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Payment_Method");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (methodID != null && methodID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "PaymentMethodID";
				commandHelper.FieldValue = methodID;
				commandHelper.TableName = "Payment_Method";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Payment_Method", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPaymentMethodsByFields(int[] methodID, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Payment_Method");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (methodID != null && methodID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "PaymentMethodID";
				commandHelper.FieldValue = methodID;
				commandHelper.TableName = "Payment_Method";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.TableName = "Payment_Method";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Payment_Method", sqlBuilder);
			return dataSet;
		}

		public PaymentMethodData GetPaymentMethods()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Payment_Method");
			sqlBuilder.UseDistinct = false;
			PaymentMethodData paymentMethodData = new PaymentMethodData();
			FillDataSet(paymentMethodData, "Payment_Method", sqlBuilder);
			return paymentMethodData;
		}

		public bool DeletePaymentMethod(string methodID)
		{
			bool flag = true;
			try
			{
				flag = DeleteTableRowByID("Payment_Method", "PaymentMethodID", methodID.ToString());
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Payment Method", methodID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetMethodNameByID(object id)
		{
			try
			{
				object obj = ExecuteSelectScalar("Payment_Method", "PaymentMethodID", id, "MethodName");
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

		public PaymentMethodData GetPaymentMethodByID(string methodID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PaymentMethodID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = methodID;
			commandHelper.TableName = "Payment_Method";
			sqlBuilder.AddCommandHelper(commandHelper);
			PaymentMethodData paymentMethodData = new PaymentMethodData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(paymentMethodData, "Payment_Method", sqlBuilder);
				return paymentMethodData;
			}
			catch
			{
				throw;
			}
		}

		public bool ExistPaymentMethod(string shortName)
		{
			try
			{
				return IsTableFieldValueExist("Payment_Method", "MethodName", shortName);
			}
			catch
			{
				throw;
			}
		}

		public int GetDefaultAccountID(int methodID)
		{
			object obj = ExecuteSelectScalar("Payment_Method", "PaymentMethodID", methodID, "DefaultAccountID");
			if (obj != null && obj != DBNull.Value)
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public PaymentMethodTypes GetMethodType(int methodID)
		{
			object obj = ExecuteSelectScalar("Payment_Method", "PaymentMethodID", methodID, "MethodType");
			if (obj != null && obj != DBNull.Value)
			{
				return (PaymentMethodTypes)byte.Parse(obj.ToString());
			}
			return PaymentMethodTypes.Other;
		}

		public PaymentMethodTypes GetMethodTypeByID(string methodID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT MethodType FROM Payment_Method\r\n                            WHERE PaymentMethodID='" + methodID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null)
			{
				return (PaymentMethodTypes)byte.Parse(obj.ToString());
			}
			return PaymentMethodTypes.Other;
		}

		public DataSet GetPaymentMethodsList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PaymentMethodID [Payment Method Code],MethodName [Name],IsInactive [Inactive] FROM Payment_Method\r\n                            ORDER BY PaymentMethodID,MethodName";
			FillDataSet(dataSet, "Customer", textCommand);
			return dataSet;
		}

		public DataSet GetPaymentMethodsComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PaymentMethodID [Code],MethodName [Name],MethodType FROM Payment_Method\r\n                            WHERE ISINACTIVE<>1 ORDER BY Code,Name";
			FillDataSet(dataSet, "Customer", textCommand);
			return dataSet;
		}
	}
}
