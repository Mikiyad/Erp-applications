using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BankFacility : StoreObject
	{
		private const string BANKFACILITYID_PARM = "@BankFacilityID";

		private const string BANKFACILITYNAME_PARM = "@BankFacilityName";

		private const string NOTE_PARM = "@Note";

		private const string BANKFACILITY_TABLE = "BankFacility";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string FACILITYGROUP_PARM = "@GroupID";

		private const string FACILITYTYPE_PARM = "@FacilityType";

		private const string ARACCOUNT_PARM = "@PayableAccountID";

		private const string CURRENTACCOUNT_PARM = "@CurrentAccountID";

		private const string BANKCHARGEACCOUNTID_PARM = "@BankChargeAccountID";

		private const string BANKINTERESTACCOUNTID_PARM = "@BankInterestAccountID";

		private const string PRINTTEMPLATENAME_PARM = "@PrintTemplateName";

		private const string STATUS_PARM = "@Status";

		private const string AMOUNTLIMIT_PARM = "@LimitAmount";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public BankFacility(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Facility", new FieldValue("FacilityID", "@BankFacilityID", isUpdateConditionField: true), new FieldValue("FacilityName", "@BankFacilityName"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("GroupID", "@GroupID"), new FieldValue("FacilityType", "@FacilityType"), new FieldValue("PayableAccountID", "@PayableAccountID"), new FieldValue("CurrentAccountID", "@CurrentAccountID"), new FieldValue("BankChargeAccountID", "@BankChargeAccountID"), new FieldValue("BankInterestAccountID", "@BankInterestAccountID"), new FieldValue("Status", "@Status"), new FieldValue("LimitAmount", "@LimitAmount"), new FieldValue("PrintTemplateName", "@PrintTemplateName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bank_Facility", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@BankFacilityName", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@FacilityType", SqlDbType.TinyInt);
			parameters.Add("@PayableAccountID", SqlDbType.NVarChar);
			parameters.Add("@CurrentAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankChargeAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankInterestAccountID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@LimitAmount", SqlDbType.Decimal);
			parameters.Add("@PrintTemplateName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@BankFacilityID"].SourceColumn = "FacilityID";
			parameters["@BankFacilityName"].SourceColumn = "FacilityName";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@FacilityType"].SourceColumn = "FacilityType";
			parameters["@PayableAccountID"].SourceColumn = "PayableAccountID";
			parameters["@CurrentAccountID"].SourceColumn = "CurrentAccountID";
			parameters["@BankChargeAccountID"].SourceColumn = "BankChargeAccountID";
			parameters["@BankInterestAccountID"].SourceColumn = "BankInterestAccountID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@LimitAmount"].SourceColumn = "LimitAmount";
			parameters["@PrintTemplateName"].SourceColumn = "PrintTemplateName";
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

		public bool InsertBankFacility(BankFacilityData accountBankFacilityData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountBankFacilityData, "Bank_Facility", insertUpdateCommand);
				string text = accountBankFacilityData.BankFacilityTable.Rows[0]["FacilityID"].ToString();
				AddActivityLog("BankFacility", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bank_Facility", "FacilityID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateBankFacility(BankFacilityData accountBankFacilityData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountBankFacilityData, "Bank_Facility", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountBankFacilityData.BankFacilityTable.Rows[0]["FacilityID"];
				UpdateTableRowByID("Bank_Facility", "FacilityID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountBankFacilityData.BankFacilityTable.Rows[0]["FacilityName"].ToString();
				AddActivityLog("BankFacility", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bank_Facility", "FacilityID", obj, sqlTransaction, isInsert: false);
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

		public BankFacilityData GetBankFacility()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Bank_Facility");
			BankFacilityData bankFacilityData = new BankFacilityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(bankFacilityData, "Bank_Facility", sqlBuilder);
			return bankFacilityData;
		}

		public bool DeleteBankFacility(string bankFacilityID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Bank_Facility WHERE FacilityID = '" + bankFacilityID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("BankFacility", bankFacilityID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public BankFacilityData GetBankFacilityByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "FacilityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Bank_Facility";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			BankFacilityData bankFacilityData = new BankFacilityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(bankFacilityData, "Bank_Facility", sqlBuilder);
			return bankFacilityData;
		}

		public DataSet GetBankFacilityByFields(params string[] columns)
		{
			return GetBankFacilityByFields(null, isInactive: true, columns);
		}

		public DataSet GetBankFacilityByFields(string[] bankFacilityID, params string[] columns)
		{
			return GetBankFacilityByFields(bankFacilityID, isInactive: true, columns);
		}

		public DataSet GetBankFacilityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Bank_Facility");
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
				commandHelper.FieldName = "FacilityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Bank_Facility";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Bank_Facility", sqlBuilder);
			return dataSet;
		}

		public DataSet GetBankFacilityList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FacilityID [Facility Code],FacilityName [Facility Name],Note\r\n\t\t\t\t\t\t   FROM Bank_Facility ";
			FillDataSet(dataSet, "Bank_Facility", textCommand);
			return dataSet;
		}

		public DataSet GetBankFacilityListByID(string id)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FacilityID, FacilityName, \r\n\t\t\t\t\t\t\t\tFacilityType, Status, StartDate, EndDate, LimitAmount,\r\n\t\t\t\t\t\t\t\tPayableAccountID, CurrentAccountID\r\n\t\t\t\t\t\t\tFROM Bank_Facility WHERE GroupID='" + id + "' ORDER BY FacilityID";
			FillDataSet(dataSet, "Bank_Facility", textCommand);
			return dataSet;
		}

		public DataSet GetBankFacilityComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FacilityID [Code],FacilityName [Name],FacilityType,ISNULL(TenorDays,0) AS TenorDays,CurrentAccountID,AC.AccountName AS CurrentAccountName,\r\n\t\t\t\t\t\t\tPayableAccountID,AC2.AccountName AS PayableAccountName, BankInterestAccountID,BankChargeAccountID,LimitAmount,\r\n                                ISNULL(LimitAmount,0)- (SELECT SUM(ISNULL(Credit,0)-ISNULL(Debit,0)) FROM Journal_Details JD\r\n\t\t\t\t\t\t\tWHERE JD.AccountID = BF.PayableAccountID) AS AvailableLimit\r\n\t\t\t\t\t\t   FROM Bank_Facility BF LEFT OUTER JOIN Account AC ON BF.CurrentAccountID = AC.AccountID\r\n\t\t\t\t\t\t   LEFT OUTER JOIN Account AC2 ON BF.PayableAccountID = AC.AccountID\r\n                            WHERE Status = 1\r\n\t\t\t\t\t\t   ORDER BY FacilityID,FacilityName";
			FillDataSet(dataSet, "Bank_Facility", textCommand);
			return dataSet;
		}

		public decimal GetBankFacilityAvailableLimit(string facilityID)
		{
			new DataSet();
			string exp = "SELECT ISNULL(LimitAmount,0) - (SELECT SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) FROM Journal_Details JD INNER JOIN Journal J ON J.JournalID = JD.JournalID\r\n\t\t\t\t\t\t\tWHERE JD.AccountID = BF.PayableAccountID and ISNULL(J.IsVoid,'False') = 'False') AS AvailableLimit\r\n\t\t\t\t\t\t   FROM Bank_Facility BF  WHERE FacilityID = '" + facilityID + "'";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return 0m;
			}
			return decimal.Parse(obj.ToString());
		}
	}
}
