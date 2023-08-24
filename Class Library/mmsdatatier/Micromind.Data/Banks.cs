using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Banks : StoreObject
	{
		private const string BANKID_PARM = "@BankID";

		private const string BANKNAME_PARM = "@BankName";

		private const string ROUTINGCODE_PARM = "@RoutingCode";

		public const string TAXIDNUMBER_PARM = "@TaxIDNumber";

		private const string CONTACTNAME_PARM = "@ContactName";

		private const string CONTACTTITLE_PARM = "@ContactTitle";

		private const string ADDRESS_PARM = "@Address";

		private const string ADDRESS3_PARM = "@Address3";

		private const string ADDRESS2_PARM = "@Address2";

		private const string CITY_PARM = "@City";

		private const string POSTALCODE_PARM = "@PostalCode";

		private const string STATE_PARM = "@State";

		private const string COUNTRY_PARM = "@Country";

		private const string PHONE_PARM = "@Phone";

		private const string NOTE_PARM = "@Note";

		private const string FAX_PARM = "@Fax";

		private const string ISINACTIVE_PARM = "@IsInactive";

		public const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public Banks(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank", new FieldValue("BankID", "@BankID", isUpdateConditionField: true), new FieldValue("BankName", "@BankName"), new FieldValue("RoutingCode", "@RoutingCode"), new FieldValue("TaxIDNumber", "@TaxIDNumber"), new FieldValue("Address", "@Address"), new FieldValue("Address2", "@Address2"), new FieldValue("Address3", "@Address3"), new FieldValue("City", "@City"), new FieldValue("ContactName", "@ContactName"), new FieldValue("ContactTitle", "@ContactTitle"), new FieldValue("Country", "@Country"), new FieldValue("Fax", "@Fax"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Phone", "@Phone"), new FieldValue("State", "@State"), new FieldValue("Note", "@Note"), new FieldValue("PostalCode", "@PostalCode"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bank", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@BankName", SqlDbType.NVarChar);
			parameters.Add("@RoutingCode", SqlDbType.NVarChar);
			parameters.Add("@TaxIDNumber", SqlDbType.NVarChar);
			parameters.Add("@ContactName", SqlDbType.NVarChar);
			parameters.Add("@ContactTitle", SqlDbType.NVarChar);
			parameters.Add("@Address", SqlDbType.NVarChar);
			parameters.Add("@Address2", SqlDbType.NVarChar);
			parameters.Add("@Address3", SqlDbType.NVarChar);
			parameters.Add("@City", SqlDbType.NVarChar);
			parameters.Add("@PostalCode", SqlDbType.NVarChar);
			parameters.Add("@State", SqlDbType.NVarChar);
			parameters.Add("@Country", SqlDbType.NVarChar);
			parameters.Add("@Phone", SqlDbType.NVarChar);
			parameters.Add("@Fax", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@BankName"].SourceColumn = "BankName";
			parameters["@RoutingCode"].SourceColumn = "RoutingCode";
			parameters["@TaxIDNumber"].SourceColumn = "TaxIDNumber";
			parameters["@ContactName"].SourceColumn = "ContactName";
			parameters["@ContactTitle"].SourceColumn = "ContactTitle";
			parameters["@Address"].SourceColumn = "Address";
			parameters["@Address2"].SourceColumn = "Address2";
			parameters["@Address3"].SourceColumn = "Address3";
			parameters["@City"].SourceColumn = "City";
			parameters["@PostalCode"].SourceColumn = "PostalCode";
			parameters["@State"].SourceColumn = "State";
			parameters["@Country"].SourceColumn = "Country";
			parameters["@Phone"].SourceColumn = "Phone";
			parameters["@Fax"].SourceColumn = "Fax";
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

		public bool InsertBank(BankData bankData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(bankData, "Bank", insertUpdateCommand);
				string text = bankData.BankTable.Rows[0]["BankID"].ToString();
				AddActivityLog("Bank", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bank", "BankID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateBank(BankData bankData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(bankData, "Bank", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object fieldIDValue = bankData.BankTable.Rows[0]["BankID"];
				string entiyID = bankData.BankTable.Rows[0]["BankName"].ToString();
				AddActivityLog("Bank", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bank", "BankID", fieldIDValue, sqlTransaction, isInsert: false);
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

		public DataSet GetBanksByFields(params string[] columns)
		{
			return GetBanksByFields(null, isInactive: true, columns);
		}

		public DataSet GetBanksByFields(string[] banksID, params string[] columns)
		{
			return GetBanksByFields(banksID, isInactive: true, columns);
		}

		public DataSet GetBanksByFields(string[] banksID, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Bank");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (banksID != null && banksID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "BankID";
				commandHelper.FieldValue = banksID;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Bank";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.Bit;
				commandHelper2.TableName = "Bank";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Bank", sqlBuilder);
			return dataSet;
		}

		public BankData GetBanks()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "Bank";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = false;
			sqlBuilder.AddOrderByColumn("Bank", "BankName");
			BankData bankData = new BankData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(bankData, "Bank", sqlBuilder);
			return bankData;
		}

		public BankData GetBankByID(string bankID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BankID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = bankID;
			commandHelper.TableName = "Bank";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			BankData bankData = new BankData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(bankData, "Bank", sqlBuilder);
			return bankData;
		}

		public bool DeleteBank(string bankID)
		{
			bool flag = true;
			try
			{
				flag = DeleteTableRowByID("Bank", "BankID", bankID);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Bank", bankID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetBankIDByName(string name)
		{
			try
			{
				object obj = ExecuteSelectScalar("Bank", "BankName", name, "BankID");
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

		public string GetBankNameByID(object bankID)
		{
			try
			{
				object obj = ExecuteSelectScalar("Bank", "BankID", bankID, "BankName");
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

		public bool ExistBank(string bankName)
		{
			try
			{
				return IsTableFieldValueExist("Bank", "BankName", bankName);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBankComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BankID [Code],BankName [Name]\r\n                           FROM Bank ORDER BY BankID,BankName";
			FillDataSet(dataSet, "Buyer", textCommand);
			return dataSet;
		}

		public DataSet GetBankList()
		{
			string textCommand = "SELECT BankID [Bank Code],BankName [Bank Name],ContactName AS [Contact Name],Phone,Fax,IsInactive AS [Inactive]\r\n                           FROM Bank ORDER BY BankID,BankName";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Account_Group", textCommand);
			return dataSet;
		}
	}
}
