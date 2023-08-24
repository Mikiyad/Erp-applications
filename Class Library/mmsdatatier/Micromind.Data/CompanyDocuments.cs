using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CompanyDocuments : StoreObject
	{
		private const string COMPANYDOCUMENT_TABLE = "Company_Document";

		private const string SPONSORID_PARM = "@SponsorID";

		private const string DOCUMENTNUMBER_PARM = "@DocumentNumber";

		private const string DOCUMENTTYPEID_PARM = "@DocumentTypeID";

		private const string DOCUMENTNAME_PARM = "@DocumentName";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string REGISTERNUMBER_PARM = "@RegisterNumber";

		private const string FILENUMBER_PARM = "@FileNumber";

		private const string ISSUEPLACE_PARM = "@IssuePlace";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string REMARKS_PARM = "@Remarks";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CompanyDocuments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Company_Document", new FieldValue("DocumentTypeID", "@DocumentTypeID", isUpdateConditionField: true), new FieldValue("DocumentNumber", "@DocumentNumber", isUpdateConditionField: true), new FieldValue("SponsorID", "@SponsorID"), new FieldValue("DocumentName", "@DocumentName"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("RegisterNumber", "@RegisterNumber"), new FieldValue("FileNumber", "@FileNumber"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Company_Document", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DocumentTypeID", SqlDbType.NVarChar);
			parameters.Add("@DocumentNumber", SqlDbType.NVarChar);
			parameters.Add("@SponsorID", SqlDbType.NVarChar);
			parameters.Add("@DocumentName", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@RegisterNumber", SqlDbType.NVarChar);
			parameters.Add("@FileNumber", SqlDbType.NVarChar);
			parameters.Add("@IssuePlace", SqlDbType.NVarChar);
			parameters.Add("@IssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@ExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@DocumentTypeID"].SourceColumn = "DocumentTypeID";
			parameters["@DocumentNumber"].SourceColumn = "DocumentNumber";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@RegisterNumber"].SourceColumn = "RegisterNumber";
			parameters["@FileNumber"].SourceColumn = "FileNumber";
			parameters["@DocumentName"].SourceColumn = "DocumentName";
			parameters["@SponsorID"].SourceColumn = "SponsorID";
			parameters["@IssuePlace"].SourceColumn = "IssuePlace";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@Remarks"].SourceColumn = "Remarks";
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

		public bool InsertCompanyDocument(CompanyDocumentData accountCompanyDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCompanyDocumentData, "Company_Document", insertUpdateCommand);
				string text = accountCompanyDocumentData.CompanyDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Company Document", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company_Document", "DocumentNumber", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCompanyDocument(CompanyDocumentData accountCompanyDocumentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCompanyDocumentData, "Company_Document", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCompanyDocumentData.CompanyDocumentTable.Rows[0]["DocumentNumber"];
				UpdateTableRowByID("Company_Document", "DocumentNumber", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCompanyDocumentData.CompanyDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Company Document", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company_Document", "DocumentNumber", obj, sqlTransaction, isInsert: false);
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

		public CompanyDocumentData GetCompanyDocument()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company_Document");
			CompanyDocumentData companyDocumentData = new CompanyDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(companyDocumentData, "Company_Document", sqlBuilder);
			return companyDocumentData;
		}

		public CompanyDocumentData GetCompanyDocumentsByCompanyID(string companyID)
		{
			return null;
		}

		public bool DeleteCompanyDocument(string companyDocumentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Company_Document WHERE DocumentNumber = '" + companyDocumentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Company Document", companyDocumentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CompanyDocumentData GetCompanyDocumentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DocumentNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Company_Document";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CompanyDocumentData companyDocumentData = new CompanyDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(companyDocumentData, "Company_Document", sqlBuilder);
			return companyDocumentData;
		}

		public DataSet GetCompanyDocumentByFields(params string[] columns)
		{
			return GetCompanyDocumentByFields(null, isInactive: true, columns);
		}

		public DataSet GetCompanyDocumentByFields(string[] companyDocumentID, params string[] columns)
		{
			return GetCompanyDocumentByFields(companyDocumentID, isInactive: true, columns);
		}

		public DataSet GetCompanyDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company_Document");
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
				commandHelper.FieldName = "DocumentNumber";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Company_Document";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Company_Document", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCompanyDocumentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DocumentNumber [Document Number],DocumentName [Document Name],RegisterNumber [Reg Number],IssuePlace [Issue Place],IssueDate [Issue Date],ExpiryDate [Expiry Date],Remarks [Note]\r\n                           FROM Company_Document ";
			FillDataSet(dataSet, "Company_Document", textCommand);
			return dataSet;
		}

		public DataSet GetCompanyDocumentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CompanyDocumentID [Code],CompanyDocumentName [Name]\r\n                           FROM Company_Document ORDER BY CompanyDocumentID,CompanyDocumentName";
			FillDataSet(dataSet, "Company_Document", textCommand);
			return dataSet;
		}
	}
}
