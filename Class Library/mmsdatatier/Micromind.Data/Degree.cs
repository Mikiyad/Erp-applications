using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Degree : StoreObject
	{
		private const string DEGREEID_PARM = "@DegreeID";

		private const string DEGREENAME_PARM = "@DegreeName";

		public const string DEGREE_TABLE = "Degree";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Degree(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Degree", new FieldValue("DegreeID", "@DegreeID", isUpdateConditionField: true), new FieldValue("DegreeName", "@DegreeName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Degree", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DegreeID", SqlDbType.NVarChar);
			parameters.Add("@DegreeName", SqlDbType.NVarChar);
			parameters["@DegreeID"].SourceColumn = "DegreeID";
			parameters["@DegreeName"].SourceColumn = "DegreeName";
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

		public bool InsertDegree(DegreeData accountDegreeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDegreeData, "Degree", insertUpdateCommand);
				string text = accountDegreeData.DegreeTable.Rows[0]["DegreeID"].ToString();
				AddActivityLog("Degree", text, "", ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Degree", "DegreeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateDegree(DegreeData accountDegreeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDegreeData, "Degree", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDegreeData.DegreeTable.Rows[0]["DegreeID"];
				UpdateTableRowByID("Degree", "DegreeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDegreeData.DegreeTable.Rows[0]["DegreeName"].ToString();
				AddActivityLog("Degree", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Degree", "DegreeID", obj, sqlTransaction, isInsert: false);
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

		public DegreeData GetDegree()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Degree");
			DegreeData degreeData = new DegreeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(degreeData, "Degree", sqlBuilder);
			return degreeData;
		}

		public bool DeleteDegree(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Degree WHERE DegreeID = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Degree", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DegreeData GetDegreeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DegreeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Degree";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			DegreeData degreeData = new DegreeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(degreeData, "Degree", sqlBuilder);
			return degreeData;
		}

		public DataSet GetDegreeByFields(params string[] columns)
		{
			return GetDegreeByFields(null, isInactive: true, columns);
		}

		public DataSet GetDegreeByFields(string[] degreeID, params string[] columns)
		{
			return GetDegreeByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetDegreeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Degree");
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
				commandHelper.FieldName = "DegreeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Degree";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Degree", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDegreeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DegreeID [Degree Code],DegreeName [Degree Name]\r\n                           FROM Degree ";
			FillDataSet(dataSet, "Degree", textCommand);
			return dataSet;
		}

		public DataSet GetDegreeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DegreeID [Code],DegreeName [Name]\r\n                           FROM Degree ORDER BY DegreeID,DegreeName";
			FillDataSet(dataSet, "Degree", textCommand);
			return dataSet;
		}
	}
}
