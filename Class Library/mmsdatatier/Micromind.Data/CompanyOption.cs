using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CompanyOption : StoreObject
	{
		private const string OPTIONID_PARM = "@OptionID";

		private const string OPTIONVALUE_PARM = "@OptionValue";

		public const string COMPANYOPTION_TABLE = "Company_Option";

		public const string GROUPID_PARM = "@GroupID";

		public const string SYSDOCID_PARM = "@SysDocID";

		public const string SYSDOCTYPE_PARM = "@SysDocType";

		public CompanyOption(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Company_Option", new FieldValue("OptionID", "@OptionID", isUpdateConditionField: true), new FieldValue("OptionValue", "@OptionValue"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("GroupID", "@GroupID"));
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
			parameters.Add("@OptionID", SqlDbType.NVarChar);
			parameters.Add("@OptionValue", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.TinyInt);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@SysDocType", SqlDbType.Int);
			parameters["@OptionID"].SourceColumn = "OptionID";
			parameters["@OptionValue"].SourceColumn = "OptionValue";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateCompanyOption(CompanyOptionData companyOptionData, bool isUpdate)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteCompanyOptionRows(sqlTransaction, 1);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(companyOptionData, "Company_Option", insertUpdateCommand);
				companyOptionData.CompanyOptionTable.Rows[0]["OptionID"].ToString();
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

		public bool InsertUpdateCompanyOption(CompanyOptionData companyOptionData, int groupID, bool isUpdate)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				companyOptionData.CompanyOptionTable.Rows[0]["OptionID"].ToString();
				DataSet companyOptionList = GetCompanyOptionList();
				new ActivityLogs(base.DBConfig).InsertDocumentVersion(ScreenTypes.Setup, "", "", "CompanyOptionsForm", companyOptionList, sqlTransaction);
				DeleteCompanyOptionRows(sqlTransaction, groupID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(companyOptionData, "Company_Option", insertUpdateCommand);
				companyOptionData.CompanyOptionTable.Rows[0]["OptionID"].ToString();
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

		public bool InsertUpdateCompanyOption(CompanyOptionData companyOptionData, int groupID, bool isUpdate, bool isSysDocOption)
		{
			bool result = true;
			if (isSysDocOption)
			{
				SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
				SqlTransaction sqlTransaction = null;
				try
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
					_ = companyOptionData.CompanyOptionTable.Rows[0];
					foreach (DataRow row in companyOptionData.Tables[0].Rows)
					{
						byte sysDocType = byte.Parse(row["SysDocType"].ToString());
						string sysDocID = row["SysDocID"].ToString();
						int.Parse(row["OptionID"].ToString());
						result = DeleteCompanyOptionRows(sqlTransaction, groupID, sysDocType, sysDocID);
					}
					insertUpdateCommand.Transaction = sqlTransaction;
					result = Insert(companyOptionData, "Company_Option", insertUpdateCommand);
					companyOptionData.CompanyOptionTable.Rows[0]["OptionID"].ToString();
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
			return result;
		}

		internal bool DeleteCompanyOptionRows(SqlTransaction sqlTransaction)
		{
			return DeleteCompanyOptionRows(sqlTransaction, 1);
		}

		internal bool DeleteCompanyOptionRows(SqlTransaction sqlTransaction, int groupID)
		{
			bool flag = true;
			try
			{
				new PurchaseOrderData();
				string arg = "DELETE FROM Company_Option";
				arg = arg + " WHERE SysDocID IS NULL AND SysDocType IS NULL AND ISNULL(GroupID,1) =  " + groupID;
				flag = Delete(arg, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CompanyOption", "", ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteCompanyOptionRows(SqlTransaction sqlTransaction, int groupID, byte sysDocType, string sysDocID)
		{
			bool flag = true;
			try
			{
				CompanyOptionData companyOptionData = new CompanyOptionData();
				string text = "SELECT* FROM Company_Option";
				text = text + " WHERE  SysDocType=" + sysDocType + "  AND   ISNULL(GroupID,1) =  " + groupID;
				text = (string.IsNullOrEmpty(sysDocID) ? (text + " AND SysDocID='" + DBNull.Value + "' ") : (text + " AND SysDocID='" + sysDocID + "' "));
				FillDataSet(companyOptionData, "Company_Option", text);
				if (companyOptionData == null || companyOptionData.Tables.Count == 0 || companyOptionData.Tables[0].Rows.Count == 0)
				{
					return true;
				}
				string text2 = "DELETE FROM Company_Option";
				text2 = text2 + " WHERE  SysDocType=" + sysDocType + " AND    ISNULL(GroupID,1) =  " + groupID;
				text2 = (string.IsNullOrEmpty(sysDocID) ? (text2 + " AND SysDocID='" + DBNull.Value + "' ") : (text2 + " AND SysDocID='" + sysDocID + "' "));
				flag = Delete(text2, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CompanyOption", "", ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CompanyOptionData GetCompanyOption()
		{
			return GetCompanyOption(1);
		}

		public CompanyOptionData GetCompanyOption(int groupID)
		{
			CompanyOptionData companyOptionData = new CompanyOptionData();
			string arg = "SELECT * FROM Company_Option";
			arg = arg + " WHERE ISNULL(GroupID,1) = " + groupID;
			FillDataSet(companyOptionData, "Company_Option", arg);
			return companyOptionData;
		}

		public bool DeleteCompanyOption(string companyOptionID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Company_Option WHERE OptionID = '" + companyOptionID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CompanyOption", companyOptionID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CompanyOptionData GetCompanyOptionByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "OptionID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Company_Option";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CompanyOptionData companyOptionData = new CompanyOptionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(companyOptionData, "Company_Option", sqlBuilder);
			return companyOptionData;
		}

		public DataSet GetCompanyOptionByFields(params string[] columns)
		{
			return GetCompanyOptionByFields(null, isInactive: true, columns);
		}

		public DataSet GetCompanyOptionByFields(string[] companyOptionID, params string[] columns)
		{
			return GetCompanyOptionByFields(companyOptionID, isInactive: true, columns);
		}

		public DataSet GetCompanyOptionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company_Option");
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
				commandHelper.FieldName = "OptionID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Company_Option";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Company_Option", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCompanyOptionList()
		{
			return GetCompanyOptionList(-1);
		}

		public DataSet GetCompanyOptionList(int groupID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT OptionID  , OptionValue ,ISNULL(GroupID,1) AS GroupID\r\n                           FROM Company_Option ";
			if (groupID != -1)
			{
				text = text + " WHERE ISNULL(GroupID,1) = " + groupID;
			}
			FillDataSet(dataSet, "Company_Option", text);
			return dataSet;
		}

		public DataSet GetSysDocCompanyOptionList()
		{
			return GetSysDocCompanyOptionList(-1);
		}

		public DataSet GetSysDocCompanyOptionList(int groupID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT OptionID  , OptionValue ,ISNULL(GroupID,1) AS GroupID, SysDocID, SysDocType\r\n                           FROM Company_Option ";
			if (groupID != -1)
			{
				text = text + " WHERE ISNULL(GroupID,1) = " + groupID;
			}
			FillDataSet(dataSet, "Company_Option", text);
			return dataSet;
		}

		public DataSet GetCompanyOptionList(int from, int to)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT OptionID  , OptionValue \r\n                           FROM Company_Option WHERE OptionID BETWEEN " + from + " AND " + to;
			FillDataSet(dataSet, "Company_Option", textCommand);
			return dataSet;
		}

		public object GetCompanyOptionValue(string key)
		{
			string exp = "SELECT OptionValue\r\n                           FROM Company_Option  WHERE OptionID='" + key + "'";
			return ExecuteScalar(exp);
		}

		public object GetCompanyOptionValue(CompanyOptionsEnum option, object defaultValue)
		{
			int num = (int)option;
			object companyOptionValue = GetCompanyOptionValue(num.ToString());
			if (companyOptionValue == null || companyOptionValue.ToString() == "")
			{
				return defaultValue;
			}
			return companyOptionValue;
		}
	}
}
