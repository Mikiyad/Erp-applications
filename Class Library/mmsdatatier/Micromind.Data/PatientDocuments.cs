using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PatientDocuments : StoreObject
	{
		private const string PATIENTDOCUMENT_TABLE = "Patient_Document";

		private const string PatientID_PARM = "@CustomerID";

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

		public PatientDocuments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Patient_Document", new FieldValue("CustomerID", "@CustomerID", isUpdateConditionField: true), new FieldValue("DocumentNumber", "@DocumentNumber", isUpdateConditionField: true), new FieldValue("DocumentTypeID", "@DocumentTypeID"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Patient_Document", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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

		public bool InsertPatientDocument(PatientDocumentData accountPatientDocumentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string patientID = accountPatientDocumentData.PatientDocumentTable.Rows[0]["CustomerID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeletePatientDocuments(sqlTransaction, patientID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountPatientDocumentData, "Patient_Document", insertUpdateCommand);
				string text = accountPatientDocumentData.PatientDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Patient Document", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Patient_Document", "DocumentNumber", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePatientDocument(PatientDocumentData accountPatientDocumentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string patientID = accountPatientDocumentData.PatientDocumentTable.Rows[0]["CustomerID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeletePatientDocuments(sqlTransaction, patientID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountPatientDocumentData, "Patient_Document", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPatientDocumentData.PatientDocumentTable.Rows[0]["DocumentNumber"];
				UpdateTableRowByID("Patient_Document", "DocumentNumber", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPatientDocumentData.PatientDocumentTable.Rows[0]["DocumentNumber"].ToString();
				AddActivityLog("Patient Document", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Patient_Document", "DocumentNumber", obj, sqlTransaction, isInsert: false);
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

		private bool DeletePatientDocuments(SqlTransaction sqlTransaction, string PatientID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Patient_Document WHERE CustomerID = '" + PatientID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Patient Document", PatientID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PatientDocumentData GetPatientDocument()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Patient_Document");
			PatientDocumentData patientDocumentData = new PatientDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(patientDocumentData, "Patient_Document", sqlBuilder);
			return patientDocumentData;
		}

		public PatientDocumentData GetPatientDocumentsByPatientID(string PatientID)
		{
			PatientDocumentData patientDocumentData = new PatientDocumentData();
			string textCommand = "Select PatientID,DocumentNumber,DocumentTypeID,IssuePlace,IssueDate,ExpiryDate,Remarks \r\n                            FROM Patient_Document\r\n                            WHERE PatientID='" + PatientID + "' ORDER BY RowIndex";
			FillDataSet(patientDocumentData, "Patient_Document", textCommand);
			return patientDocumentData;
		}

		public bool DeletePatientDocument(string PatientDocumentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Patient_Document WHERE DocumentNumber = '" + PatientDocumentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Patient Document", PatientDocumentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PatientDocumentData GetPatientDocumentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CustomerID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Patient_Document";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PatientDocumentData patientDocumentData = new PatientDocumentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(patientDocumentData, "Patient_Document", sqlBuilder);
			return patientDocumentData;
		}

		public DataSet GetPatientDocumentByFields(params string[] columns)
		{
			return GetPatientDocumentByFields(null, isInactive: true, columns);
		}

		public DataSet GetPatientDocumentByFields(string[] PatientDocumentID, params string[] columns)
		{
			return GetPatientDocumentByFields(PatientDocumentID, isInactive: true, columns);
		}

		public DataSet GetPatientDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Patient_Document");
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
				commandHelper.TableName = "Patient_Document";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Patient_Document", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPatientDocumentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ED.PatientID,E.FirstName + ' ' + E.LastName AS [Patient Name],DT.TypeName AS [Type], DocumentNumber, IssuePlace,IssueDate [Issue Date],ExpiryDate [Expiry Date]   FROM Patient_Document ED INNER JOIN Patient E ON ED.PatientID = E.PatientID\r\n                                INNER JOIN Patient_Doc_Type DT ON DT.TypeID = ED.DocumentTypeID ";
			FillDataSet(dataSet, "Patient_Document", textCommand);
			return dataSet;
		}

		public DataSet GetPatientDocumentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PatientDocumentID [Code],PatientDocumentName [Name]\r\n                           FROM PatientDocument ORDER BY PatientDocumentID,PatientDocumentName";
			FillDataSet(dataSet, "Patient_Document", textCommand);
			return dataSet;
		}
	}
}
