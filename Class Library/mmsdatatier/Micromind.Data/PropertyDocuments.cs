using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyDocuments : StoreObject
	{
		private const string PROPERTYDOCUMENT_TABLE = "Property_Document";

		private const string PROPERTYID_PARM = "@PropertyID";

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

		public PropertyDocuments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Document", new FieldValue("PropertyID", "@PropertyID", isUpdateConditionField: true), new FieldValue("DocumentNumber", "@DocumentNumber", isUpdateConditionField: true), new FieldValue("DocumentTypeID", "@DocumentTypeID"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Document", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@DocumentNumber", SqlDbType.NVarChar);
			parameters.Add("@DocumentTypeID", SqlDbType.NVarChar);
			parameters.Add("@IssuePlace", SqlDbType.NVarChar);
			parameters.Add("@IssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@ExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters["@PropertyID"].SourceColumn = "PropertyID";
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

		public bool InsertPropertyDocument(PropertyDocumentData accountPropertyDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountPropertyDocumentData.PropertyDocumentTable.Rows[0]["PropertyID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeletePropertyDocuments(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountPropertyDocumentData, "Property_Document", insertUpdateCommand);
				string text = accountPropertyDocumentData.PropertyDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Property Document", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Document", "DocumentNumber", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePropertyDocument(PropertyDocumentData accountPropertyDocumentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountPropertyDocumentData.PropertyDocumentTable.Rows[0]["PropertyID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeletePropertyDocuments(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountPropertyDocumentData, "Property_Document", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPropertyDocumentData.PropertyDocumentTable.Rows[0]["DocumentNumber"];
				UpdateTableRowByID("Property_Document", "DocumentNumber", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPropertyDocumentData.PropertyDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Property Document", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Document", "DocumentNumber", obj, sqlTransaction, isInsert: false);
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

		private bool DeletePropertyDocuments(SqlTransaction sqlTransaction, string employeeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Document WHERE PropertyID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Property Document", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyDocumentData GetPropertyDocument()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Document");
			PropertyDocumentData propertyDocumentData = new PropertyDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyDocumentData, "Property_Document", sqlBuilder);
			return propertyDocumentData;
		}

		public PropertyDocumentData GetPropertyDocumentsByPropertyID(string employeeID)
		{
			PropertyDocumentData propertyDocumentData = new PropertyDocumentData();
			string textCommand = "Select PropertyID,DocumentNumber,DocumentTypeID,IssuePlace,IssueDate,ExpiryDate,Remarks \r\n                            FROM Property_Document\r\n                            WHERE PropertyID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(propertyDocumentData, "Property_Document", textCommand);
			return propertyDocumentData;
		}

		public bool DeletePropertyDocument(string employeeDocumentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Document WHERE DocumentNumber = '" + employeeDocumentID + "'";
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

		public PropertyDocumentData GetPropertyDocumentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DocumentNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Property_Document";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyDocumentData propertyDocumentData = new PropertyDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyDocumentData, "Property_Document", sqlBuilder);
			return propertyDocumentData;
		}

		public DataSet GetPropertyDocumentByFields(params string[] columns)
		{
			return GetPropertyDocumentByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyDocumentByFields(string[] employeeDocumentID, params string[] columns)
		{
			return GetPropertyDocumentByFields(employeeDocumentID, isInactive: true, columns);
		}

		public DataSet GetPropertyDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Document");
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
				commandHelper.TableName = "Property_Document";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_Document", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyDocumentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ED.PropertyID,E.FirstName + ' ' + E.LastName AS [Property Name],DT.TypeName AS [Type], DocumentNumber, IssuePlace,IssueDate [Issue Date],ExpiryDate [Expiry Date]   FROM Property_Document ED INNER JOIN Property E ON ED.PropertyID = E.PropertyID\r\n                                INNER JOIN Property_Doc_Type DT ON DT.TypeID = ED.DocumentTypeID ";
			FillDataSet(dataSet, "Property_Document", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyDocumentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyDocumentID [Code],PropertyDocumentName [Name]\r\n                           FROM PropertyDocument ORDER BY PropertyDocumentID,PropertyDocumentName";
			FillDataSet(dataSet, "Property_Document", textCommand);
			return dataSet;
		}
	}
}
