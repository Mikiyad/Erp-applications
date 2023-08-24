using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PatientDocType : StoreObject
	{
		private const string TYPEID_PARM = "@TypeID";

		private const string TYPENAME_PARM = "@TypeName";

		private const string NOTE_PARM = "@Note";

		private const string REMIND_PARM = "@Remind";

		private const string REMINDDAYS_PARM = "@RemindDays";

		private const string PATIENTDOCUMENTTYPE_TABLE = "Patient_Doc_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PatientDocType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Patient_Doc_Type", new FieldValue("TypeID", "@TypeID", isUpdateConditionField: true), new FieldValue("TypeName", "@TypeName"), new FieldValue("Remind", "@Remind"), new FieldValue("RemindDays", "@RemindDays"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Patient_Doc_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TypeID", SqlDbType.NVarChar);
			parameters.Add("@TypeName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Remind", SqlDbType.Bit);
			parameters.Add("@RemindDays", SqlDbType.SmallInt);
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@TypeName"].SourceColumn = "TypeName";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Remind"].SourceColumn = "Remind";
			parameters["@RemindDays"].SourceColumn = "RemindDays";
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

		public bool InsertPatientDocType(PatientDocTypeData accountPatientDocTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPatientDocTypeData, "Patient_Doc_Type", insertUpdateCommand);
				string text = accountPatientDocTypeData.EmpDocumentTypeTable.Rows[0]["TypeID"].ToString();
				AddActivityLog("Patient Document Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Patient_Doc_Type", "TypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePatientDocType(PatientDocTypeData accountPatientDocTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPatientDocTypeData, "Patient_Doc_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPatientDocTypeData.EmpDocumentTypeTable.Rows[0]["TypeID"];
				UpdateTableRowByID("Patient_Doc_Type", "TypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPatientDocTypeData.EmpDocumentTypeTable.Rows[0]["TypeName"].ToString();
				AddActivityLog("Patient Document Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Patient_Doc_Type", "TypeID", obj, sqlTransaction, isInsert: false);
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

		public PatientDocTypeData GetPatientDocType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Patient_Doc_Type");
			PatientDocTypeData patientDocTypeData = new PatientDocTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(patientDocTypeData, "Patient_Doc_Type", sqlBuilder);
			return patientDocTypeData;
		}

		public bool DeletePatientDocType(string typeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Patient_Doc_Type WHERE TypeID = '" + typeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Patient Document Type", typeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PatientDocTypeData GetPatientDocTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Patient_Doc_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PatientDocTypeData patientDocTypeData = new PatientDocTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(patientDocTypeData, "Patient_Doc_Type", sqlBuilder);
			return patientDocTypeData;
		}

		public DataSet GetPatientDocTypeByFields(params string[] columns)
		{
			return GetPatientDocTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetPatientDocTypeByFields(string[] typeID, params string[] columns)
		{
			return GetPatientDocTypeByFields(typeID, isInactive: true, columns);
		}

		public DataSet GetPatientDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Patient_Doc_Type");
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
				commandHelper.FieldName = "TypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Patient_Doc_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Patient_Doc_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPatientDocTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Type Code],TypeName [Type Name],Note\r\n                           FROM Patient_Doc_Type ";
			FillDataSet(dataSet, "Patient_Doc_Type", textCommand);
			return dataSet;
		}

		public DataSet GetPatientDocTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Code],TypeName [Name]\r\n                           FROM Patient_Doc_Type ORDER BY TypeID,TypeName";
			FillDataSet(dataSet, "Patient_Doc_Type", textCommand);
			return dataSet;
		}
	}
}
