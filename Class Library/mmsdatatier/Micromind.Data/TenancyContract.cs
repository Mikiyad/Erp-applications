using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TenancyContract : StoreObject
	{
		private const string CONTRACTID_PARM = "@ContractID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string NOTE_PARM = "@Note";

		private const string LANDLORD_PARM = "@Landlord";

		private const string TENANT_PARM = "@Tenant";

		private const string CONTACTID_PARM = "@ContactID";

		private const string LOCATION_PARM = "@Location";

		private const string STATUS_PARM = "@Status";

		private const string INSTALLMENTS_PARM = "@Installments";

		private const string CONTRACTAMOUNT_PARM = "@ContractAmount";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string TENANCYCONTRACT_TABLE = "Tenancy_Contract";

		public TenancyContract(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Tenancy_Contract", new FieldValue("ContractID", "@ContractID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("Landlord", "@Landlord"), new FieldValue("Tenant", "@Tenant"), new FieldValue("ContactID", "@ContactID"), new FieldValue("Location", "@Location"), new FieldValue("Status", "@Status"), new FieldValue("Installments", "@Installments"), new FieldValue("ContractAmount", "@ContractAmount"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Tenancy_Contract", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ContractID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Landlord", SqlDbType.NVarChar);
			parameters.Add("@Tenant", SqlDbType.NVarChar);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@Location", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Installments", SqlDbType.TinyInt);
			parameters.Add("@ContractAmount", SqlDbType.Money);
			parameters.Add("@IssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@ExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ContractID"].SourceColumn = "ContractID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Landlord"].SourceColumn = "Landlord";
			parameters["@Tenant"].SourceColumn = "Tenant";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@Location"].SourceColumn = "Location";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Installments"].SourceColumn = "Installments";
			parameters["@ContractAmount"].SourceColumn = "ContractAmount";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
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

		public bool InsertTenancyContract(TenancyContractData accountTenancyContractData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountTenancyContractData, "Tenancy_Contract", insertUpdateCommand);
				string text = accountTenancyContractData.TenancyContractTable.Rows[0]["ContractID"].ToString();
				AddActivityLog("Tenancy Contract", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Tenancy_Contract", "ContractID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateTenancyContract(TenancyContractData accountTenancyContractData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountTenancyContractData, "Tenancy_Contract", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountTenancyContractData.TenancyContractTable.Rows[0]["ContractID"];
				UpdateTableRowByID("Tenancy_Contract", "ContractID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountTenancyContractData.TenancyContractTable.Rows[0]["Description"].ToString();
				AddActivityLog("Tenancy Contract", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Tenancy_Contract", "ContractID", obj, sqlTransaction, isInsert: false);
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

		public TenancyContractData GetTenancyContract()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Tenancy_Contract");
			TenancyContractData tenancyContractData = new TenancyContractData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(tenancyContractData, "Tenancy_Contract", sqlBuilder);
			return tenancyContractData;
		}

		public bool DeleteTenancyContract(string contractID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Tenancy_Contract WHERE ContractID = '" + contractID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Tenancy Contract", contractID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public TenancyContractData GetTenancyContractByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ContractID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Tenancy_Contract";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			TenancyContractData tenancyContractData = new TenancyContractData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(tenancyContractData, "Tenancy_Contract", sqlBuilder);
			return tenancyContractData;
		}

		public DataSet GetTenancyContractByFields(params string[] columns)
		{
			return GetTenancyContractByFields(null, isInactive: true, columns);
		}

		public DataSet GetTenancyContractByFields(string[] tenancyContractID, params string[] columns)
		{
			return GetTenancyContractByFields(tenancyContractID, isInactive: true, columns);
		}

		public DataSet GetTenancyContractByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Tenancy_Contract");
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
				commandHelper.FieldName = "ContractID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Tenancy_Contract";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Tenancy_Contract", sqlBuilder);
			return dataSet;
		}

		public DataSet GetTenancyContractList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContractID [Contract Number],Description [Description],Landlord,Tenant,Location,IssueDate [Issue Date],ExpiryDate [Expiry Date],Status\r\n                           FROM Tenancy_Contract ";
			FillDataSet(dataSet, "Tenancy_Contract", textCommand);
			return dataSet;
		}

		public DataSet GetTenancyContractComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContractID [Code],Description [Name]\r\n                           FROM Tenancy_Contract ORDER BY ContractID,Description";
			FillDataSet(dataSet, "Tenancy_Contract", textCommand);
			return dataSet;
		}
	}
}
