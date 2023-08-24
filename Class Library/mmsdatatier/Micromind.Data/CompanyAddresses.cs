using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CompanyAddresses : StoreObject
	{
		private const string COMPANYADDRESS_TABLE = "@Company_Address";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string ADDRESSID_PARM = "@AddressID";

		private const string CONTACTNAME_PARM = "@ContactName";

		private const string ADDRESS1_PARM = "@Address1";

		private const string ADDRESS2_PARM = "@Address2";

		private const string ADDRESS3_PARM = "@Address3";

		private const string ADDRESSPRINTFORMAT_PARM = "@AddressPrintFormat";

		private const string CITY_PARM = "@City";

		private const string STATE_PARM = "@State";

		private const string COUNTRY_PARM = "@Country";

		private const string POSTALCODE_PARM = "@PostalCode";

		private const string DEPARTMENT_PARM = "@Department";

		private const string PHONE1_PARM = "@Phone1";

		private const string PHONE2_PARM = "@Phone2";

		private const string FAX_PARM = "@Fax";

		private const string MOBILE_PARM = "@Mobile";

		private const string EMAIL_PARM = "@Email";

		private const string WEBSITE_PARM = "@Website";

		private const string COMMENT_PARM = "@Comment";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CompanyAddresses(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Company_Address", new FieldValue("AddressID", "@AddressID", isUpdateConditionField: true), new FieldValue("ContactName", "@ContactName"), new FieldValue("Address1", "@Address1"), new FieldValue("Address2", "@Address2"), new FieldValue("Address3", "@Address3"), new FieldValue("AddressPrintFormat", "@AddressPrintFormat"), new FieldValue("City", "@City"), new FieldValue("State", "@State"), new FieldValue("Country", "@Country"), new FieldValue("PostalCode", "@PostalCode"), new FieldValue("Department", "@Department"), new FieldValue("Phone1", "@Phone1"), new FieldValue("Phone2", "@Phone2"), new FieldValue("Fax", "@Fax"), new FieldValue("Mobile", "@Mobile"), new FieldValue("Email", "@Email"), new FieldValue("Website", "@Website"), new FieldValue("Comment", "@Comment"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Company_Address", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public SqlCommand GetInsertUpdateCommand(bool isUpdate)
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
			parameters.Add("@AddressID", SqlDbType.NVarChar);
			parameters.Add("@ContactName", SqlDbType.NVarChar);
			parameters.Add("@Address1", SqlDbType.NVarChar);
			parameters.Add("@Address2", SqlDbType.NVarChar);
			parameters.Add("@Address3", SqlDbType.NVarChar);
			parameters.Add("@AddressPrintFormat", SqlDbType.NVarChar);
			parameters.Add("@City", SqlDbType.NVarChar);
			parameters.Add("@State", SqlDbType.NVarChar);
			parameters.Add("@Country", SqlDbType.NVarChar);
			parameters.Add("@PostalCode", SqlDbType.NVarChar);
			parameters.Add("@Department", SqlDbType.NVarChar);
			parameters.Add("@Phone1", SqlDbType.NVarChar);
			parameters.Add("@Phone2", SqlDbType.NVarChar);
			parameters.Add("@Fax", SqlDbType.NVarChar);
			parameters.Add("@Mobile", SqlDbType.NVarChar);
			parameters.Add("@Email", SqlDbType.NVarChar);
			parameters.Add("@Website", SqlDbType.NVarChar);
			parameters.Add("@Comment", SqlDbType.NVarChar);
			parameters["@AddressID"].SourceColumn = "AddressID";
			parameters["@AddressID"].SourceColumn = "AddressID";
			parameters["@ContactName"].SourceColumn = "ContactName";
			parameters["@Address1"].SourceColumn = "Address1";
			parameters["@Address2"].SourceColumn = "Address2";
			parameters["@Address3"].SourceColumn = "Address3";
			parameters["@AddressPrintFormat"].SourceColumn = "AddressPrintFormat";
			parameters["@City"].SourceColumn = "City";
			parameters["@State"].SourceColumn = "State";
			parameters["@Country"].SourceColumn = "Country";
			parameters["@PostalCode"].SourceColumn = "PostalCode";
			parameters["@Department"].SourceColumn = "Department";
			parameters["@Phone1"].SourceColumn = "Phone1";
			parameters["@Phone2"].SourceColumn = "Phone2";
			parameters["@Fax"].SourceColumn = "Fax";
			parameters["@Mobile"].SourceColumn = "Mobile";
			parameters["@Email"].SourceColumn = "Email";
			parameters["@Website"].SourceColumn = "Website";
			parameters["@Comment"].SourceColumn = "Comment";
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

		public bool InsertCompanyAddress(CompanyAddressData accountCompanyAddressData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCompanyAddressData, "Company_Address", insertUpdateCommand);
				string text = accountCompanyAddressData.CompanyAddressTable.Rows[0]["AddressID"].ToString();
				AddActivityLog("CompanyAddress", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company_Address", "AddressID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCompanyAddress(CompanyAddressData accountCompanyAddressData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCompanyAddressData, "Company_Address", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCompanyAddressData.CompanyAddressTable.Rows[0]["AddressID"];
				UpdateTableRowByID("Company_Address", "AddressID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCompanyAddressData.CompanyAddressTable.Rows[0]["AddressID"].ToString();
				AddActivityLog("CompanyAddress", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company_Address", "AddressID", obj, sqlTransaction, isInsert: false);
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

		public CompanyAddressData GetCompanyAddress()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company_Address");
			CompanyAddressData companyAddressData = new CompanyAddressData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(companyAddressData, "Company_Address", sqlBuilder);
			return companyAddressData;
		}

		public bool DeleteCompanyAddress(string addressID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Company_Address WHERE AddressID = '" + addressID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CompanyAddress", addressID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CompanyAddressData GetCompanyAddressByID(string addressID)
		{
			CompanyAddressData companyAddressData = new CompanyAddressData();
			string textCommand = "SELECT *\r\n                           FROM Company_Address WHERE AddressID='" + addressID + "'";
			FillDataSet(companyAddressData, "Company_Address", textCommand);
			return companyAddressData;
		}

		public DataSet GetCompanyAddressByFields(params string[] columns)
		{
			return GetCompanyAddressByFields(null, isInactive: true, columns);
		}

		public DataSet GetCompanyAddressByFields(string[] companyAddressID, params string[] columns)
		{
			return GetCompanyAddressByFields(companyAddressID, isInactive: true, columns);
		}

		public DataSet GetCompanyAddressByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company_Address");
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
				commandHelper.FieldName = "AddressID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Company_Address";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Company_Address", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCompanyAddressList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AddressID AS [Address Code],ContactName AS [Contact Name],City,Country,Phone1 AS Phone,Mobile,Fax\r\n                           FROM Company_Address ";
			FillDataSet(dataSet, "Company_Address", textCommand);
			return dataSet;
		}

		public DataSet GetCompanyAddressComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AddressID Code,'' AS [Name]\r\n                           FROM Company_Address ";
			FillDataSet(dataSet, "Company_Address", textCommand);
			return dataSet;
		}

		public bool IsPrimaryAddress(string addresssID)
		{
			new DataSet();
			string exp = "SELECT PrimaryAddressID FROM Company WHERE CompanyID='1'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				if (obj.ToString() == "")
				{
					obj = "PRIMARY";
				}
				return obj.ToString() == addresssID;
			}
			return false;
		}
	}
}
