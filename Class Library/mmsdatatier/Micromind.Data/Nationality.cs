using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Nationality : StoreObject
	{
		private const string NATIONALITYID_PARM = "@NationalityID";

		private const string NATIONALITYNAME_PARM = "@NationalityName";

		public const string NATIONALITY_TABLE = "Nationality";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Nationality(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Nationality", new FieldValue("NationalityID", "@NationalityID", isUpdateConditionField: true), new FieldValue("NationalityName", "@NationalityName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Nationality", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@NationalityID", SqlDbType.NVarChar);
			parameters.Add("@NationalityName", SqlDbType.NVarChar);
			parameters["@NationalityID"].SourceColumn = "NationalityID";
			parameters["@NationalityName"].SourceColumn = "NationalityName";
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

		public bool InsertNationality(NationalityData accountNationalityData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountNationalityData, "Nationality", insertUpdateCommand);
				string text = accountNationalityData.NationalityTable.Rows[0]["NationalityID"].ToString();
				AddActivityLog("Nationality", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Nationality", "NationalityID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateNationality(NationalityData accountNationalityData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountNationalityData, "Nationality", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountNationalityData.NationalityTable.Rows[0]["NationalityID"];
				UpdateTableRowByID("Nationality", "NationalityID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountNationalityData.NationalityTable.Rows[0]["NationalityName"].ToString();
				AddActivityLog("Nationality", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Nationality", "NationalityID", obj, sqlTransaction, isInsert: false);
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

		public NationalityData GetNationality()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Nationality");
			NationalityData nationalityData = new NationalityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(nationalityData, "Nationality", sqlBuilder);
			return nationalityData;
		}

		public bool DeleteNationality(string nationalityID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Nationality WHERE NationalityID = '" + nationalityID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Nationality", nationalityID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public NationalityData GetNationalityByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "NationalityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Nationality";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			NationalityData nationalityData = new NationalityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(nationalityData, "Nationality", sqlBuilder);
			return nationalityData;
		}

		public DataSet GetNationalityByFields(params string[] columns)
		{
			return GetNationalityByFields(null, isInactive: true, columns);
		}

		public DataSet GetNationalityByFields(string[] nationalityID, params string[] columns)
		{
			return GetNationalityByFields(nationalityID, isInactive: true, columns);
		}

		public DataSet GetNationalityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Nationality");
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
				commandHelper.FieldName = "NationalityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Nationality";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Nationality", sqlBuilder);
			return dataSet;
		}

		public DataSet GetNationalityList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT NationalityID [Nationality Code],NationalityName [Nationality Name]\r\n                           FROM Nationality ";
			FillDataSet(dataSet, "Nationality", textCommand);
			return dataSet;
		}

		public DataSet GetNationalityComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT NationalityID [Code],NationalityName [Name]\r\n                           FROM Nationality ORDER BY NationalityID,NationalityName";
			FillDataSet(dataSet, "Nationality", textCommand);
			return dataSet;
		}
	}
}
