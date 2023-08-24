using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CompanyDivision : StoreObject
	{
		private const string DIVISIONID_PARM = "@DivisionID";

		private const string DIVISIONNAME_PARM = "@DivisionName";

		private const string COMPANYID_PARM = "@CompanyID";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string DIVISION_TABLE = "Company_Division";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CompanyDivision(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Company_Division", new FieldValue("DivisionID", "@DivisionID", isUpdateConditionField: true), new FieldValue("DivisionName", "@DivisionName"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Company_Division", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@DivisionName", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@DivisionName"].SourceColumn = "DivisionName";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertDivision(CompanyDivisionData accountCompanyDivisionData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCompanyDivisionData, "Company_Division", insertUpdateCommand);
				string text = accountCompanyDivisionData.DivisionTable.Rows[0]["DivisionID"].ToString();
				AddActivityLog("Division", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company_Division", "DivisionID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateDivision(CompanyDivisionData accountCompanyDivisionData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCompanyDivisionData, "Company_Division", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCompanyDivisionData.DivisionTable.Rows[0]["DivisionID"];
				UpdateTableRowByID("Company_Division", "DivisionID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCompanyDivisionData.DivisionTable.Rows[0]["DivisionName"].ToString();
				AddActivityLog("Division", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company_Division", "DivisionID", obj, sqlTransaction, isInsert: false);
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

		public CompanyDivisionData GetDivision()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company_Division");
			CompanyDivisionData companyDivisionData = new CompanyDivisionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(companyDivisionData, "Company_Division", sqlBuilder);
			return companyDivisionData;
		}

		public bool DeleteDivision(string divisionID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Company_Division WHERE DivisionID = '" + divisionID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Division", divisionID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CompanyDivisionData GetDivisionByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DivisionID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Company_Division";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CompanyDivisionData companyDivisionData = new CompanyDivisionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(companyDivisionData, "Company_Division", sqlBuilder);
			return companyDivisionData;
		}

		public DataSet GetDivisionByFields(params string[] columns)
		{
			return GetDivisionByFields(null, isInactive: true, columns);
		}

		public DataSet GetDivisionByFields(string[] divisionID, params string[] columns)
		{
			return GetDivisionByFields(divisionID, isInactive: true, columns);
		}

		public DataSet GetDivisionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company_Division");
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
				commandHelper.FieldName = "DivisionID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Company_Division";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Company_Division", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDivisionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DivisionID [Division Code],DivisionName [Division Name],Note,Inactive\r\n                           FROM Company_Division ";
			FillDataSet(dataSet, "Company_Division", textCommand);
			return dataSet;
		}

		public DataSet GetDivisionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DivisionID [Code],DivisionName [Name]\r\n                           FROM Company_Division ORDER BY DivisionID,DivisionName";
			FillDataSet(dataSet, "Company_Division", textCommand);
			return dataSet;
		}
	}
}
