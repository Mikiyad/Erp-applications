using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CreditCards : StoreObject
	{
		private const string CARDID_PARM = "@CardID";

		private const string SHORTNAME_PARM = "@ShortName";

		private const string NUMBER_PARM = "@Number";

		private const string EXPDATE_PARM = "@ExpDate";

		private const string PRINTEDNAME_PARM = "@PrintedName";

		private const string ADDRESS1_PARM = "@Address1";

		private const string ADDRESS2_PARM = "@Address2";

		private const string POSTALCODE_PARM = "@PostalCode";

		private const string NOTE_PARM = "@Note";

		private const string TYPEID_PARM = "@TypeID";

		private const string TYPENAME_PARMD = "@TypeName";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		public bool CheckConcurrency = true;

		public CreditCards(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Credit Cards]", new FieldValue("ShortName", "@ShortName"), new FieldValue("TypeID", "@TypeID"), new FieldValue("Number", "@Number"), new FieldValue("ExpDate", "@ExpDate"), new FieldValue("PrintedName", "@PrintedName"), new FieldValue("Address1", "@Address1"), new FieldValue("Address2", "@Address2"), new FieldValue("PostalCode", "@PostalCode"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Credit Cards]", new FieldValue("CardID", "@CardID", isUpdateConditionField: true), new FieldValue("ShortName", "@ShortName"), new FieldValue("TypeID", "@TypeID"), new FieldValue("Number", "@Number"), new FieldValue("ExpDate", "@ExpDate"), new FieldValue("PrintedName", "@PrintedName"), new FieldValue("Address1", "@Address1"), new FieldValue("Address2", "@Address2"), new FieldValue("PostalCode", "@PostalCode"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (CheckConcurrency)
			{
				sqlBuilder.AddInsertUpdateParameters("[Credit Cards]", new FieldValue("DateTimeStamp", "@DateTimeStamp", isUpdateConditionField: true, checkForNullValue: true));
			}
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@ShortName", SqlDbType.NVarChar);
				parameters.Add("@TypeID", SqlDbType.TinyInt);
				parameters.Add("@Number", SqlDbType.NVarChar);
				parameters.Add("@ExpDate", SqlDbType.DateTime);
				parameters.Add("@PrintedName", SqlDbType.NVarChar);
				parameters.Add("@Address1", SqlDbType.NVarChar);
				parameters.Add("@Address2", SqlDbType.NVarChar);
				parameters.Add("@PostalCode", SqlDbType.NVarChar);
				parameters.Add("@Note", SqlDbType.NText);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters["@ShortName"].SourceColumn = "ShortName";
				parameters["@TypeID"].SourceColumn = "TypeID";
				parameters["@Number"].SourceColumn = "Number";
				parameters["@ExpDate"].SourceColumn = "ExpDate";
				parameters["@PrintedName"].SourceColumn = "PrintedName";
				parameters["@Address1"].SourceColumn = "Address1";
				parameters["@Address2"].SourceColumn = "Address2";
				parameters["@PostalCode"].SourceColumn = "PostalCode";
				parameters["@Note"].SourceColumn = "Note";
				parameters["@IsInactive"].SourceColumn = "IsInactive";
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@CardID", SqlDbType.Int);
				parameters.Add("@TypeID", SqlDbType.TinyInt);
				parameters.Add("@ShortName", SqlDbType.NVarChar);
				parameters.Add("@Number", SqlDbType.NVarChar);
				parameters.Add("@ExpDate", SqlDbType.DateTime);
				parameters.Add("@PrintedName", SqlDbType.NVarChar);
				parameters.Add("@Address1", SqlDbType.NVarChar);
				parameters.Add("@Address2", SqlDbType.NVarChar);
				parameters.Add("@PostalCode", SqlDbType.NVarChar);
				parameters.Add("@Note", SqlDbType.NText);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters.Add("@DateTimeStamp", SqlDbType.DateTime);
				parameters["@CardID"].SourceColumn = "CardID";
				parameters["@ShortName"].SourceColumn = "ShortName";
				parameters["@TypeID"].SourceColumn = "TypeID";
				parameters["@Number"].SourceColumn = "Number";
				parameters["@ExpDate"].SourceColumn = "ExpDate";
				parameters["@PrintedName"].SourceColumn = "PrintedName";
				parameters["@Address1"].SourceColumn = "Address1";
				parameters["@Address2"].SourceColumn = "Address2";
				parameters["@PostalCode"].SourceColumn = "PostalCode";
				parameters["@Note"].SourceColumn = "Note";
				parameters["@IsInactive"].SourceColumn = "IsInactive";
				parameters["@DateTimeStamp"].SourceColumn = "DateTimeStamp";
			}
			return updateCommand;
		}

		public bool InsertCreditCard(CreditCardData creditCardData)
		{
			bool result = true;
			SqlCommand insertCommand = GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(creditCardData, "[Credit Cards]", insertCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("[Credit Cards]", insertCommand);
				creditCardData.CreditCardTable.Rows[0]["CardID"] = insertedRowIdentity;
				UpdateTableRowByID("[Credit Cards]", "CardID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				string entiyID = creditCardData.CreditCardTable.Rows[0]["ShortName"].ToString();
				AddActivityLog("Credit Card", entiyID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Credit Cards]", "CardID", insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool UpdateCreaditCard(CreditCardData creditCardData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(creditCardData, "[Credit Cards]", updateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = creditCardData.CreditCardTable.Rows[0]["CardID"];
				UpdateTableRowByID("[Credit Cards]", "CardID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				string entiyID = creditCardData.CreditCardTable.Rows[0]["ShortName"].ToString();
				AddActivityLog("Credit Card", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Credit Cards]", "CardID", obj, sqlTransaction, isInsert: false);
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

		public bool InsertCardType(CardTypeData cardTypeData)
		{
			bool result = true;
			SqlCommand insertCommand = GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(cardTypeData, "[Card Types]", insertCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("[Card Types]", insertCommand);
				cardTypeData.CardTypeTable.Rows[0]["TypeID"] = insertedRowIdentity;
				UpdateTableRowByID("[Card Types]", "TypeID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				string entiyID = cardTypeData.CardTypeTable.Rows[0]["TypeName"].ToString();
				AddActivityLog("Credit Card Type", entiyID, ActivityTypes.Add, sqlTransaction);
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

		public bool UpdateCardType(CardTypeData cardTypeData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(cardTypeData, "[Card Types]", updateCommand);
				if (!flag)
				{
					return flag;
				}
				object id = cardTypeData.CardTypeTable.Rows[0]["TypeID"];
				UpdateTableRowByID("[Card Types]", "TypeID", "DateTimeStamp", id, DateTime.Now, sqlTransaction);
				string entiyID = cardTypeData.CardTypeTable.Rows[0]["TypeName"].ToString();
				AddActivityLog("Credit Card Type", entiyID, ActivityTypes.Update, sqlTransaction);
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

		public DataSet GetCreditCardsByFields(params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Credit Cards]");
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
			FillDataSet(dataSet, "[Credit Cards]", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCreditCardsByFields(int[] cardID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Credit Cards]");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (cardID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "CardID";
				commandHelper.FieldValue = cardID;
				commandHelper.TableName = "[Credit Cards]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "[Credit Cards]", sqlBuilder);
			return dataSet;
		}

		public CreditCardData GetCreditCards()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Credit Cards]");
			sqlBuilder.UseDistinct = false;
			CreditCardData creditCardData = new CreditCardData();
			FillDataSet(creditCardData, "[Credit Cards]", sqlBuilder);
			return creditCardData;
		}

		public CardTypeData GetCardTypes()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("[Card Types]");
			sqlBuilder.UseDistinct = false;
			CardTypeData cardTypeData = new CardTypeData();
			FillDataSet(cardTypeData, "[Card Types]", sqlBuilder);
			return cardTypeData;
		}

		public bool DeleteCreditCard(int cardID)
		{
			bool flag = true;
			try
			{
				string cardNameByID = GetCardNameByID(cardID);
				flag = DeleteTableRowByID("[Credit Cards]", "CardID", cardID.ToString());
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Credit Card", cardNameByID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteCardType(int typeID)
		{
			bool flag = true;
			try
			{
				string cardTypeNameByID = GetCardTypeNameByID(typeID);
				flag = DeleteTableRowByID("[Card Types]", "TypeID", typeID.ToString());
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Credit Card Type", cardTypeNameByID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetCardNameByID(object cardID)
		{
			try
			{
				object obj = ExecuteSelectScalar("[Credit Cards]", "CardID", cardID, "ShortName");
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

		public string GetCardTypeNameByID(object cardTypeID)
		{
			try
			{
				object obj = ExecuteSelectScalar("[Card Types]", "TypeID", cardTypeID, "TypeName");
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

		public CreditCardData GetCreditCardByID(int cardID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CardID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = cardID;
			commandHelper.TableName = "[Credit Cards]";
			sqlBuilder.AddCommandHelper(commandHelper);
			CreditCardData creditCardData = new CreditCardData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(creditCardData, "[Credit Cards]", sqlBuilder);
				return creditCardData;
			}
			catch
			{
				throw;
			}
		}

		public CardTypeData GetCardTypeByID(int typeID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TypeID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = typeID;
			commandHelper.TableName = "[Card Types]";
			sqlBuilder.AddCommandHelper(commandHelper);
			CardTypeData cardTypeData = new CardTypeData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(cardTypeData, "[Card Types]", sqlBuilder);
				return cardTypeData;
			}
			catch
			{
				throw;
			}
		}

		public bool ExistCreditCard(string shortName)
		{
			try
			{
				return IsTableFieldValueExist("[Credit Cards]", "ShortName", shortName);
			}
			catch
			{
				throw;
			}
		}
	}
}
