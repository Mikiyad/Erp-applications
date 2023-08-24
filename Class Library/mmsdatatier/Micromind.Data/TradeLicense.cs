using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TradeLicense : StoreObject
	{
		private const string TRADELICENSEID_PARM = "@TradeLicenseID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string SPONSOR_PARM = "@Sponsors";

		private const string CONTACTID_PARM = "@ContactID";

		private const string PARTNERS_PARM = "@Partners";

		private const string REGISTERNUMBER_PARM = "@RegisterNumber";

		private const string LEGALTYPE_PARM = "@LegalType";

		private const string ISSUEPLACE_PARM = "@IssuePlace";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string RENEWDATE_PARM = "@RenewDate";

		private const string STATUS_PARM = "@Status";

		public const string NOTE_PARM = "@Note";

		public const string TRADELICENSE_TABLE = "TradeLicense";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public TradeLicense(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Trade_License", new FieldValue("TradeLicenseID", "@TradeLicenseID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("Sponsors", "@Sponsors"), new FieldValue("ContactID", "@ContactID"), new FieldValue("Partners", "@Partners"), new FieldValue("RegisterNumber", "@RegisterNumber"), new FieldValue("LegalType", "@LegalType"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("RenewDate", "@RenewDate"), new FieldValue("Status", "@Status"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Trade_License", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TradeLicenseID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Sponsors", SqlDbType.NVarChar);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@Partners", SqlDbType.NVarChar);
			parameters.Add("@RegisterNumber", SqlDbType.NVarChar);
			parameters.Add("@LegalType", SqlDbType.NVarChar);
			parameters.Add("@IssuePlace", SqlDbType.NVarChar);
			parameters.Add("@IssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@ExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@RenewDate", SqlDbType.SmallDateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@TradeLicenseID"].SourceColumn = "TradeLicenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Sponsors"].SourceColumn = "Sponsors";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@Partners"].SourceColumn = "Partners";
			parameters["@RegisterNumber"].SourceColumn = "RegisterNumber";
			parameters["@LegalType"].SourceColumn = "LegalType";
			parameters["@IssuePlace"].SourceColumn = "IssuePlace";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@RenewDate"].SourceColumn = "RenewDate";
			parameters["@Status"].SourceColumn = "Status";
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

		public bool InsertTradeLicense(TradeLicenseData accountTradeLicenseData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountTradeLicenseData, "Trade_License", insertUpdateCommand);
				string text = accountTradeLicenseData.TradeLicenseTable.Rows[0]["TradeLicenseID"].ToString();
				AddActivityLog("Trade License", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Trade_License", "TradeLicenseID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateTradeLicense(TradeLicenseData accountTradeLicenseData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountTradeLicenseData, "Trade_License", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountTradeLicenseData.TradeLicenseTable.Rows[0]["TradeLicenseID"];
				UpdateTableRowByID("Trade_License", "TradeLicenseID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountTradeLicenseData.TradeLicenseTable.Rows[0]["Description"].ToString();
				AddActivityLog("Trade License", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Trade_License", "TradeLicenseID", obj, sqlTransaction, isInsert: false);
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

		public TradeLicenseData GetTradeLicense()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Trade_License");
			TradeLicenseData tradeLicenseData = new TradeLicenseData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(tradeLicenseData, "Trade_License", sqlBuilder);
			return tradeLicenseData;
		}

		public bool DeleteTradeLicense(string tradeLicenseID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Trade_License WHERE TradeLicenseID = '" + tradeLicenseID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Trade License", tradeLicenseID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public TradeLicenseData GetTradeLicenseByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TradeLicenseID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Trade_License";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			TradeLicenseData tradeLicenseData = new TradeLicenseData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(tradeLicenseData, "Trade_License", sqlBuilder);
			return tradeLicenseData;
		}

		public DataSet GetTradeLicenseByFields(params string[] columns)
		{
			return GetTradeLicenseByFields(null, isInactive: true, columns);
		}

		public DataSet GetTradeLicenseByFields(string[] tradeLicenseID, params string[] columns)
		{
			return GetTradeLicenseByFields(tradeLicenseID, isInactive: true, columns);
		}

		public DataSet GetTradeLicenseByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Trade_License");
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
				commandHelper.FieldName = "TradeLicenseID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Trade_License";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Trade_License", sqlBuilder);
			return dataSet;
		}

		public DataSet GetTradeLicenseList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TradeLicenseID [License Number],Description,IssuePlace [Issue Place],IssueDate [Issue Date],ExpiryDate [Expiry Date],Note,Status\r\n                           FROM Trade_License ";
			FillDataSet(dataSet, "Trade_License", textCommand);
			return dataSet;
		}

		public DataSet GetTradeLicenseComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TradeLicenseID [Code],Description [Name]\r\n                           FROM Trade_License ORDER BY TradeLicenseID,Description";
			FillDataSet(dataSet, "Trade_License", textCommand);
			return dataSet;
		}
	}
}
