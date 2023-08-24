using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomerInsurance : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string DOCUMENTNUMBER_PARM = "@VoucherID";

		private const string CUSTOMERINSURANCE_TABLE = "Customer_Insurance";

		private const string INSURANCEID_PARM = "@InsuranceID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string INSSTATUS_PARM = "@InsStatus";

		private const string INSAPPLICATIONDATE_PARM = "@InsApplicationDate";

		private const string INSREQUESTEDAMOUNT_PARM = "@InsRequestedAmount";

		private const string INSAPPROVEDAMOUNT_PARM = "@InsApprovedAmount";

		private const string INSPOLICYNUMBER_PARM = "@InsPolicyNumber";

		private const string INSREMARKS_PARM = "@InsRemarks";

		private const string INSRATING_PARM = "@InsRating";

		private const string INSEFFECTIVEDATE_PARM = "@InsEffectiveDate";

		private const string INSVALIDTO_PARM = "@InsValidTo";

		private const string INSPROVIDER_PARM = "@InsProvider";

		private const string REVIEWDATE_PARM = "@ReviewDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private string IDvalue;

		public CustomerInsurance(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Insurance", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("InsuranceID", "@InsuranceID"), new FieldValue("InsProvider", "@InsProvider"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("InsApplicationDate", "@InsApplicationDate"), new FieldValue("InsApprovedAmount", "@InsApprovedAmount"), new FieldValue("ReviewDate", "@ReviewDate"), new FieldValue("InsPolicyNumber", "@InsPolicyNumber"), new FieldValue("InsRemarks", "@InsRemarks"), new FieldValue("InsRequestedAmount", "@InsRequestedAmount"), new FieldValue("InsStatus", "@InsStatus"), new FieldValue("InsRating", "@InsRating"), new FieldValue("InsEffectiveDate", "@InsEffectiveDate"), new FieldValue("InsValidTo", "@InsValidTo"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Customer_Insurance", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SysDocID", SqlDbType.VarChar);
			parameters.Add("@VoucherID", SqlDbType.VarChar);
			parameters.Add("@CustomerID", SqlDbType.VarChar);
			parameters.Add("@InsuranceID", SqlDbType.VarChar);
			parameters.Add("@InsApplicationDate", SqlDbType.DateTime);
			parameters.Add("@InsApprovedAmount", SqlDbType.Money);
			parameters.Add("@InsPolicyNumber", SqlDbType.NVarChar);
			parameters.Add("@InsRemarks", SqlDbType.NVarChar);
			parameters.Add("@InsRequestedAmount", SqlDbType.Money);
			parameters.Add("@InsStatus", SqlDbType.TinyInt);
			parameters.Add("@InsRating", SqlDbType.TinyInt);
			parameters.Add("@InsProvider", SqlDbType.NVarChar);
			parameters.Add("@InsEffectiveDate", SqlDbType.DateTime);
			parameters.Add("@InsValidTo", SqlDbType.DateTime);
			parameters.Add("@ReviewDate", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@InsuranceID"].SourceColumn = "InsuranceID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@InsEffectiveDate"].SourceColumn = "InsEffectiveDate";
			parameters["@InsValidTo"].SourceColumn = "InsValidTo";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@InsApplicationDate"].SourceColumn = "InsApplicationDate";
			parameters["@InsApprovedAmount"].SourceColumn = "InsApprovedAmount";
			parameters["@InsPolicyNumber"].SourceColumn = "InsPolicyNumber";
			parameters["@InsRemarks"].SourceColumn = "InsRemarks";
			parameters["@InsRequestedAmount"].SourceColumn = "InsRequestedAmount";
			parameters["@InsProvider"].SourceColumn = "InsProvider";
			parameters["@InsStatus"].SourceColumn = "InsStatus";
			parameters["@InsRating"].SourceColumn = "InsRating";
			parameters["@ReviewDate"].SourceColumn = "ReviewDate";
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

		public bool InsertUpdateCustomerInsurance(CustomerInsuranceData accountCustomerInsuranceData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountCustomerInsuranceData, "Customer_Insurance", insertUpdateCommand) : Update(accountCustomerInsuranceData, "Customer_Insurance", insertUpdateCommand));
				if (flag)
				{
					flag &= new Customers(base.DBConfig).UpdateInsuranceDetails(accountCustomerInsuranceData, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				string text = accountCustomerInsuranceData.CustomerInsuranceTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("Customer Insurance", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Insurance", "VoucherID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCustomerInsurance(CustomerInsuranceData accountCustomerInsuranceData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCustomerInsuranceData, "Customer_Insurance", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCustomerInsuranceData.CustomerInsuranceTable.Rows[0]["VoucherID"];
				UpdateTableRowByID("Customer_Insurance", "VoucherID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCustomerInsuranceData.CustomerInsuranceTable.Rows[0]["VoucherID"].ToString();
				AddActivityLog("Customer Insurance", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Customer_Insurance", "SysDocID", obj, sqlTransaction, isInsert: false);
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

		public CustomerInsuranceData GetCustomerInsurance()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Insurance");
			CustomerInsuranceData customerInsuranceData = new CustomerInsuranceData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customerInsuranceData, "Customer_Insurance", sqlBuilder);
			return customerInsuranceData;
		}

		public bool DeleteCustomerInsurance(string documentNumber)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Customer_Insurance WHERE VoucherID='" + documentNumber + "'";
				flag = Delete(commandText, null);
				if (flag)
				{
					flag &= new Customers(base.DBConfig).UpdateInsuranceDetailsAfterDeletion(documentNumber);
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Customer Insurance", documentNumber, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CustomerInsuranceData GetCustomerInsuranceByID(string id)
		{
			CustomerInsuranceData customerInsuranceData = new CustomerInsuranceData();
			string textCommand = "SELECT TOP 1 * FROM Customer_Insurance CI where CI.VoucherID='" + id + "' ORDER By CI.DateCreated desc";
			FillDataSet(customerInsuranceData, "Customer_Insurance", textCommand);
			return customerInsuranceData;
		}

		public CustomerInsuranceData GetCustomerInsuranceIndividuals(string id)
		{
			CustomerInsuranceData customerInsuranceData = new CustomerInsuranceData();
			SqlCommand sqlCommand = new SqlCommand("SELECT ci.CustomerID[CUSTOMER CODE], C.CustomerName [CUSTOMER NAME], ci.InsProvider [INSURANCE PROVIDER],ci.InsPolicyNumber [POLICY NUMBER],ci.InsRequestedAmount [REQUESTED AMOUNT], ci.InsApprovedAmount[APPROVED AMOUNT] from Customer_Insurance ci LEFT JOIN CUSTOMER C ON CI.CustomerID=c.CustomerID  WHERE CI.CustomerID='" + id + "' ");
			FillDataSet(customerInsuranceData, "Customer_Insurance", sqlCommand);
			return customerInsuranceData;
		}

		public CustomerInsuranceData GetList(DateTime from, DateTime to, bool showVoid)
		{
			CustomerInsuranceData customerInsuranceData = new CustomerInsuranceData();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ci.CustomerID[CUSTOMER CODE], C.CustomerName [CUSTOMER NAME], ci.InsProvider [INSURANCE PROVIDER],ci.InsPolicyNumber [POLICY NUMBER],ci.InsRequestedAmount [REQUESTED AMOUNT], ci.InsApprovedAmount[APPROVED AMOUNT], ci.DateCreated [DATE]from Customer_Insurance ci LEFT JOIN CUSTOMER C ON CI.CustomerID=c.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " where  ci.DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(customerInsuranceData, "Customer_Insurance", sqlCommand);
			return customerInsuranceData;
		}

		public DataSet GetList(string id)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT CI.VoucherID [Voucher ID], ci.CustomerID[CUSTOMER CODE], C.CustomerName [CUSTOMER NAME],ci.InsuranceID [INSURANCE ID], ci.InsProvider [INSURANCE PROVIDER],ci.InsPolicyNumber [POLICY NUMBER],ci.InsRequestedAmount [REQUESTED AMOUNT], ci.InsApprovedAmount[APPROVED AMOUNT], \r\n                            ci.InsApplicationDate [DATE],ci.InsEffectiveDate [EFFECTIVE DATE],ci.InsValidTo [VALID TO] from Customer_Insurance ci LEFT JOIN CUSTOMER C ON CI.CustomerID=c.CustomerID ";
			if (id != "")
			{
				text = text + " where CI.CustomerID='" + id + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text);
			FillDataSet(dataSet, "Customer_Insurance", sqlCommand);
			return dataSet;
		}

		public DataSet GetCustomerInsuranceByFields(params string[] columns)
		{
			return GetCustomerInsuranceByFields(null, isInactive: true, columns);
		}

		public DataSet GetCustomerInsuranceByFields(string[] CustomerInsuranceID, params string[] columns)
		{
			return GetCustomerInsuranceByFields(CustomerInsuranceID, isInactive: true, columns);
		}

		public DataSet GetCustomerInsuranceByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Insurance");
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
				commandHelper.FieldName = "VoucherID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Customer_Insurance";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Customer_Insurance", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCustomerInsuranceList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CustomerInsuranceID [CustomerInsurance Code],CustomerInsuranceName [CustomerInsurance Name],Note,IsInactive [Inactive]\r\n                           FROM Release_Type ";
			FillDataSet(dataSet, "Customer_Insurance", textCommand);
			return dataSet;
		}

		public DataSet GetCustomerInsuranceComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CustomerInsuranceID [Code],CustomerInsuranceName [Name]\r\n                           FROM Release_Type ORDER BY CustomerInsuranceID,CustomerInsuranceName";
			FillDataSet(dataSet, "Customer_Insurance", textCommand);
			return dataSet;
		}

		public string GetNextDocumentNumber(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			int num = 1;
			string text2 = "";
			string textCommand = "SELECT MAX(VoucherID) AS LastNumber FROM Customer_Insurance WHERE SysDocID='" + sysDocID + "'";
			FillDataSet(dataSet, "System_Document", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				text2 = dataSet.Tables[0].Rows[0]["LastNumber"].ToString();
			}
			for (int i = 0; i < text2.Length && !char.IsNumber(text2[i]); i++)
			{
				text += text2[i].ToString();
			}
			if (text2 != "")
			{
				num = int.Parse(text2.Substring(text.Length)) + 1;
				int num2 = text2.Length - text.Length;
				string text3 = "";
				for (int j = 0; j < num2; j++)
				{
					text3 += "0";
				}
				return text + num.ToString(text3);
			}
			return text + num.ToString("00000000");
		}
	}
}
