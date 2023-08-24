using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Lawyer : StoreObject
	{
		private const string LAWYER_TABLE = "Lawyer";

		private const string LAWYERID_PARM = "@LawyerID";

		private const string LAWYERNAME_PARM = "@LawyerName";

		private const string SELECTIONTYPEPARM = "@SelectionType";

		private const string PARTYID_PARM = "@PartyID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Lawyer(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Lawyer", new FieldValue("LawyerID", "@LawyerID", isUpdateConditionField: true), new FieldValue("LawyerName", "@LawyerName"), new FieldValue("SelectionType", "@SelectionType"), new FieldValue("PartyID", "@PartyID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Lawyer", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@LawyerID", SqlDbType.NVarChar);
			parameters.Add("@LawyerName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@SelectionType", SqlDbType.NVarChar);
			parameters.Add("@PartyID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@LawyerID"].SourceColumn = "LawyerID";
			parameters["@LawyerName"].SourceColumn = "LawyerName";
			parameters["@SelectionType"].SourceColumn = "SelectionType";
			parameters["@PartyID"].SourceColumn = "PartyID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertLawyer(LawyerData accountLawyerData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountLawyerData, "Lawyer", insertUpdateCommand);
				string text = accountLawyerData.LawyerTable.Rows[0]["LawyerID"].ToString();
				AddActivityLog("Lawyer", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Lawyer", "LawyerID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateLawyer(LawyerData accountLawyerData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountLawyerData, "Lawyer", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountLawyerData.LawyerTable.Rows[0]["LawyerID"];
				UpdateTableRowByID("Lawyer", "LawyerID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountLawyerData.LawyerTable.Rows[0]["LawyerName"].ToString();
				AddActivityLog("Lawyer", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Lawyer", "LawyerID", obj, sqlTransaction, isInsert: false);
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

		public LawyerData GetLawyer()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lawyer");
			LawyerData lawyerData = new LawyerData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(lawyerData, "Lawyer", sqlBuilder);
			return lawyerData;
		}

		public bool DeleteLawyer(string LAWYERID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Lawyer WHERE LawyerID = '" + LAWYERID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Lawyer", LAWYERID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public LawyerData GetLawyerByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "LawyerID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Lawyer";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			LawyerData lawyerData = new LawyerData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(lawyerData, "Lawyer", sqlBuilder);
			return lawyerData;
		}

		public DataSet GetLawyerByFields(params string[] columns)
		{
			return GetLawyerByFields(null, isInactive: true, columns);
		}

		public DataSet GetLawyerByFields(string[] LAWYERID, params string[] columns)
		{
			return GetLawyerByFields(LAWYERID, isInactive: true, columns);
		}

		public DataSet GetLawyerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lawyer");
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
				commandHelper.FieldName = "LawyerID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Lawyer";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Lawyer", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLawyerList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LawyerID [Code],LawyerName [Name],Note,IsInactive [Inactive]\r\n                           FROM Lawyer  ";
			FillDataSet(dataSet, "Lawyer", textCommand);
			return dataSet;
		}

		public DataSet GetLawyerComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LawyerID [Code],LawyerName [Name]\r\n                           FROM Lawyer \r\n                            WHERE IsInactive<>1  ORDER BY LawyerID,LawyerName";
			FillDataSet(dataSet, "Lawyer", textCommand);
			return dataSet;
		}
	}
}
