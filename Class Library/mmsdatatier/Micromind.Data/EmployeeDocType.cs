using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeDocType : StoreObject
	{
		private const string TYPEID_PARM = "@TypeID";

		private const string TYPENAME_PARM = "@TypeName";

		private const string NOTE_PARM = "@Note";

		private const string REMIND_PARM = "@Remind";

		private const string REMINDDAYS_PARM = "@RemindDays";

		private const string EMPDOCUMENTTYPE_TABLE = "Employee_Doc_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeDocType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Doc_Type", new FieldValue("TypeID", "@TypeID", isUpdateConditionField: true), new FieldValue("TypeName", "@TypeName"), new FieldValue("Remind", "@Remind"), new FieldValue("RemindDays", "@RemindDays"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Doc_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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

		public bool InsertEmployeeDocType(EmployeeDocTypeData accountEmployeeDocTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEmployeeDocTypeData, "Employee_Doc_Type", insertUpdateCommand);
				string text = accountEmployeeDocTypeData.EmpDocumentTypeTable.Rows[0]["TypeID"].ToString();
				AddActivityLog("Employee Document Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Doc_Type", "TypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeDocType(EmployeeDocTypeData accountEmployeeDocTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEmployeeDocTypeData, "Employee_Doc_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeDocTypeData.EmpDocumentTypeTable.Rows[0]["TypeID"];
				UpdateTableRowByID("Employee_Doc_Type", "TypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeDocTypeData.EmpDocumentTypeTable.Rows[0]["TypeName"].ToString();
				AddActivityLog("Employee Document Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Doc_Type", "TypeID", obj, sqlTransaction, isInsert: false);
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

		public EmployeeDocTypeData GetEmployeeDocType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Doc_Type");
			EmployeeDocTypeData employeeDocTypeData = new EmployeeDocTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeDocTypeData, "Employee_Doc_Type", sqlBuilder);
			return employeeDocTypeData;
		}

		public bool DeleteEmployeeDocType(string typeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Doc_Type WHERE TypeID = '" + typeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Document Type", typeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeDocTypeData GetEmployeeDocTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Doc_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeDocTypeData employeeDocTypeData = new EmployeeDocTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeDocTypeData, "Employee_Doc_Type", sqlBuilder);
			return employeeDocTypeData;
		}

		public DataSet GetEmployeeDocTypeByFields(params string[] columns)
		{
			return GetEmployeeDocTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeDocTypeByFields(string[] typeID, params string[] columns)
		{
			return GetEmployeeDocTypeByFields(typeID, isInactive: true, columns);
		}

		public DataSet GetEmployeeDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Doc_Type");
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
				commandHelper.TableName = "Employee_Doc_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Doc_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeDocTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Type Code],TypeName [Type Name],Note\r\n                           FROM Employee_Doc_Type ";
			FillDataSet(dataSet, "Employee_Doc_Type", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeDocTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Code],TypeName [Name]\r\n                           FROM Employee_Doc_Type ORDER BY TypeID,TypeName";
			FillDataSet(dataSet, "Employee_Doc_Type", textCommand);
			return dataSet;
		}
	}
}
