using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyTenantDocuments : StoreObject
	{
		private const string PROPERTTENANTYDOCUMENT_TABLE = "Property_Tenant_Document";

		private const string TENANTID_PARM = "@CustomerID";

		private const string DOCUMENTNUMBER_PARM = "@DocumentNumber";

		private const string DOCUMENTTYPEID_PARM = "@DocumentTypeID";

		private const string ISSUEPLACE_PARM = "@IssuePlace";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string REMARKS_PARM = "@Remarks";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PropertyTenantDocuments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Tenant_Document", new FieldValue("CustomerID", "@CustomerID", isUpdateConditionField: true), new FieldValue("DocumentNumber", "@DocumentNumber", isUpdateConditionField: true), new FieldValue("DocumentTypeID", "@DocumentTypeID"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Tenant_Document", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@DocumentNumber", SqlDbType.NVarChar);
			parameters.Add("@DocumentTypeID", SqlDbType.NVarChar);
			parameters.Add("@IssuePlace", SqlDbType.NVarChar);
			parameters.Add("@IssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@ExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@DocumentNumber"].SourceColumn = "DocumentNumber";
			parameters["@DocumentTypeID"].SourceColumn = "DocumentTypeID";
			parameters["@IssuePlace"].SourceColumn = "IssuePlace";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertPropertyTenantDocument(PropertyTenantDocumentData accountPropertyTenantDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string tenantID = accountPropertyTenantDocumentData.PropertyTenantDocumentTable.Rows[0]["CustomerID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeletePropertyTenantDocuments(sqlTransaction, tenantID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountPropertyTenantDocumentData, "Property_Tenant_Document", insertUpdateCommand);
				string text = accountPropertyTenantDocumentData.PropertyTenantDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Property Document", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Tenant_Document", "DocumentNumber", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePropertyTenantDocument(PropertyTenantDocumentData accountPropertyTenantDocumentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string tenantID = accountPropertyTenantDocumentData.PropertyTenantDocumentTable.Rows[0]["Property_Tenant_Document"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeletePropertyTenantDocuments(sqlTransaction, tenantID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountPropertyTenantDocumentData, "Property_Tenant_Document", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPropertyTenantDocumentData.PropertyTenantDocumentTable.Rows[0]["DocumentNumber"];
				UpdateTableRowByID("Property_Tenant_Document", "DocumentNumber", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPropertyTenantDocumentData.PropertyTenantDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Property Document", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Tenant_Document", "DocumentNumber", obj, sqlTransaction, isInsert: false);
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

		private bool DeletePropertyTenantDocuments(SqlTransaction sqlTransaction, string tenantID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Tenant_Document WHERE CustomerID = '" + tenantID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Property Tenant Document", tenantID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyTenantDocumentData GetPropertyTenantDocument()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Tenant_Document");
			PropertyTenantDocumentData propertyTenantDocumentData = new PropertyTenantDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyTenantDocumentData, "Property_Tenant_Document", sqlBuilder);
			return propertyTenantDocumentData;
		}

		public PropertyTenantDocumentData GetPropertyTenantDocumentsByTenantID(string tenantID)
		{
			PropertyTenantDocumentData propertyTenantDocumentData = new PropertyTenantDocumentData();
			string textCommand = "Select CustomerID,DocumentNumber,DocumentTypeID,IssuePlace,IssueDate,ExpiryDate,Remarks \r\n                            FROM Property_Tenant_Document\r\n                            WHERE CustomerID='" + tenantID + "' ORDER BY RowIndex";
			FillDataSet(propertyTenantDocumentData, "Property_Tenant_Document", textCommand);
			return propertyTenantDocumentData;
		}

		public bool DeletePropertyTenantDocument(string employeeDocumentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Tenant_Document WHERE DocumentNumber = '" + employeeDocumentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Property Document", employeeDocumentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyTenantDocumentData GetPropertyTenantDocumentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DocumentNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Property_Tenant_Document";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyTenantDocumentData propertyTenantDocumentData = new PropertyTenantDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyTenantDocumentData, "Property_Tenant_Document", sqlBuilder);
			return propertyTenantDocumentData;
		}

		public DataSet GetPropertyTenantDocumentByFields(params string[] columns)
		{
			return GetPropertyTenantDocumentByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyTenantDocumentByFields(string[] employeeDocumentID, params string[] columns)
		{
			return GetPropertyTenantDocumentByFields(employeeDocumentID, isInactive: true, columns);
		}

		public DataSet GetPropertyTenantDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Tenant_Document");
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
				commandHelper.TableName = "Property_Tenant_Document";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_Tenant_Document", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyTenantDocumentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ED.PropertyID,E.FirstName + ' ' + E.LastName AS [Property Name],DT.TypeName AS [Type], DocumentNumber, IssuePlace,IssueDate [Issue Date],ExpiryDate [Expiry Date]   FROM Property_Document ED INNER JOIN Property E ON ED.PropertyID = E.PropertyID\r\n                                INNER JOIN Property_Doc_Type DT ON DT.TypeID = ED.DocumentTypeID ";
			FillDataSet(dataSet, "Property_Tenant_Document", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyTenantDocumentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyDocumentID [Code],PropertyDocumentName [Name]\r\n                           FROM PropertyDocument ORDER BY PropertyDocumentID,PropertyDocumentName";
			FillDataSet(dataSet, "Property_Tenant_Document", textCommand);
			return dataSet;
		}
	}
}
