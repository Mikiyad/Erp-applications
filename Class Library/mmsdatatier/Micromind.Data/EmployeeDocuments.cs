using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeDocuments : StoreObject
	{
		private const string EMPLOYEEDOCUMENT_TABLE = "Employee_Document";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

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

		public EmployeeDocuments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Document", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("DocumentNumber", "@DocumentNumber", isUpdateConditionField: true), new FieldValue("DocumentTypeID", "@DocumentTypeID"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Document", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@DocumentNumber", SqlDbType.NVarChar);
			parameters.Add("@DocumentTypeID", SqlDbType.NVarChar);
			parameters.Add("@IssuePlace", SqlDbType.NVarChar);
			parameters.Add("@IssueDate", SqlDbType.SmallDateTime);
			parameters.Add("@ExpiryDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
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

		public bool InsertEmployeeDocument(EmployeeDocumentData accountEmployeeDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeDocumentData.EmployeeDocumentTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeDocuments(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountEmployeeDocumentData, "Employee_Document", insertUpdateCommand);
				string text = accountEmployeeDocumentData.EmployeeDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Employee Document", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Document", "DocumentNumber", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeDocument(EmployeeDocumentData accountEmployeeDocumentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeDocumentData.EmployeeDocumentTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeDocuments(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountEmployeeDocumentData, "Employee_Document", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeDocumentData.EmployeeDocumentTable.Rows[0]["DocumentNumber"];
				UpdateTableRowByID("Employee_Document", "DocumentNumber", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeDocumentData.EmployeeDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Employee Document", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Document", "DocumentNumber", obj, sqlTransaction, isInsert: false);
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

		private bool DeleteEmployeeDocuments(SqlTransaction sqlTransaction, string employeeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Document WHERE EmployeeID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Document", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeDocumentData GetEmployeeDocument()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Document");
			EmployeeDocumentData employeeDocumentData = new EmployeeDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeDocumentData, "Employee_Document", sqlBuilder);
			return employeeDocumentData;
		}

		public EmployeeDocumentData GetEmployeeDocumentsByEmployeeID(string employeeID)
		{
			EmployeeDocumentData employeeDocumentData = new EmployeeDocumentData();
			string textCommand = "Select EmployeeID,DocumentNumber,DocumentTypeID,IssuePlace,IssueDate,ExpiryDate,Remarks \r\n                            FROM Employee_Document\r\n                            WHERE EmployeeID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(employeeDocumentData, "Employee_Document", textCommand);
			return employeeDocumentData;
		}

		public bool DeleteEmployeeDocument(string employeeDocumentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Document WHERE DocumentNumber = '" + employeeDocumentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Document", employeeDocumentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeDocumentData GetEmployeeDocumentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DocumentNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Document";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeDocumentData employeeDocumentData = new EmployeeDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeDocumentData, "Employee_Document", sqlBuilder);
			return employeeDocumentData;
		}

		public DataSet GetEmployeeDocumentByFields(params string[] columns)
		{
			return GetEmployeeDocumentByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeDocumentByFields(string[] employeeDocumentID, params string[] columns)
		{
			return GetEmployeeDocumentByFields(employeeDocumentID, isInactive: true, columns);
		}

		public DataSet GetEmployeeDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Document");
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
				commandHelper.TableName = "Employee_Document";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Document", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeDocumentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ED.EmployeeID,E.FirstName + ' ' + E.LastName AS [Employee Name],DT.TypeName AS [Type], DocumentNumber, IssuePlace,IssueDate [Issue Date],ExpiryDate [Expiry Date]   FROM Employee_Document ED INNER JOIN Employee E ON ED.EmployeeID = E.EmployeeID\r\n                                INNER JOIN Employee_Doc_Type DT ON DT.TypeID = ED.DocumentTypeID ";
			FillDataSet(dataSet, "Employee_Document", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeDocumentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EmployeeDocumentID [Code],EmployeeDocumentName [Name]\r\n                           FROM EmployeeDocument ORDER BY EmployeeDocumentID,EmployeeDocumentName";
			FillDataSet(dataSet, "Employee_Document", textCommand);
			return dataSet;
		}
	}
}
