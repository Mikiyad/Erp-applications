using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CLToken : StoreObject
	{
		private const string CLCLTOKEN_TABLE = "CL_Token";

		private const string TOKENID_PARM = "@TokenID";

		private const string SYSTEMKEY_PARM = "@SystemKey";

		private const string TOKENNUMBER_PARM = "@TokenNumber";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string AMOUNT_PARM = "@Amount";

		private const string ISSUEDBY_PARM = "@IssuedBy";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string REQUESTDATE_PARM = "@RequestDate";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string STATUS_PARM = "@Status";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CLToken(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("CL_Token", new FieldValue("SystemKey", "@SystemKey"), new FieldValue("TokenNumber", "@TokenNumber"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("Amount", "@Amount"), new FieldValue("IssuedBy", "@IssuedBy"), new FieldValue("RequestedBy", "@RequestedBy"), new FieldValue("RequestDate", "@RequestDate"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("Status", "@Status"), new FieldValue("CustomerID", "@CustomerID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("CL_Token", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SystemKey", SqlDbType.NVarChar);
			parameters.Add("@TokenNumber", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@IssuedBy", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@RequestDate", SqlDbType.DateTime);
			parameters.Add("@IssueDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters["@SystemKey"].SourceColumn = "SystemKey";
			parameters["@TokenNumber"].SourceColumn = "TokenNumber";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@IssuedBy"].SourceColumn = "IssuedBy";
			parameters["@RequestDate"].SourceColumn = "RequestDate";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
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

		public bool InsertCLToken(CLTokenData clTokenData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(clTokenData, "CL_Token", insertUpdateCommand);
				string text = clTokenData.CLTokenTable.Rows[0]["TokenID"].ToString();
				AddActivityLog("CL Token", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("CL_Token", "TokenID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCLToken(CLTokenData clTokenData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(clTokenData, "CL_Token", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = clTokenData.CLTokenTable.Rows[0]["TokenID"];
				UpdateTableRowByID("CL_Token", "TokenID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = clTokenData.CLTokenTable.Rows[0]["TokenID"].ToString();
				AddActivityLog("CL Token", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("CL_Token", "TokenID", obj, sqlTransaction, isInsert: false);
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

		public bool RequestCLToken(CLTokenData clTokenData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(clTokenData, "CL_Token", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object insertedRowIdentity = GetInsertedRowIdentity("CL_Token", insertUpdateCommand);
				UpdateTableRowByID("CL_Token", "TokenID", "DateUpdated", insertedRowIdentity, DateTime.Now, sqlTransaction);
				string entiyID = clTokenData.CLTokenTable.Rows[0]["TokenID"].ToString();
				AddActivityLog("CL Token", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("CL_Token", "TokenID", insertedRowIdentity, sqlTransaction, isInsert: false);
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

		public CLTokenData GetCLToken()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("CL_Token");
			CLTokenData cLTokenData = new CLTokenData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(cLTokenData, "CL_Token", sqlBuilder);
			return cLTokenData;
		}

		public bool DeleteCLToken(string tokenID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM CL_Token WHERE TokenID = '" + tokenID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Token", tokenID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CLTokenData GetCLTokenByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TokenID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "CL_Token";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CLTokenData cLTokenData = new CLTokenData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(cLTokenData, "CL_Token", sqlBuilder);
			return cLTokenData;
		}

		public DataSet GetCLTokenByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("CL_Token");
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
				commandHelper.FieldName = "TokenID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "CL_Token";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "CL_Token", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCLTokenList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TokenID [Type Code],TokenName [Type Name],IsInactive\r\n                           FROM Token ";
			FillDataSet(dataSet, "CL_Token", textCommand);
			return dataSet;
		}

		public DataSet GetCLTokenComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TokenID [Code], TokenName [Name]\r\n                           FROM Token ORDER BY TokenID,TokenName";
			FillDataSet(dataSet, "CL_Token", textCommand);
			return dataSet;
		}
	}
}
