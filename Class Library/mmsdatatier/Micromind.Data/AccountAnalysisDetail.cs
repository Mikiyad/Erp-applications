using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class AccountAnalysisDetail : StoreObject
	{
		private const string ACCOUNTID_PARM = "@AccountID";

		private const string ANALYSISGROUPID_PARM = "@AnalysisGroupID";

		private const string TYPE_PARM = "@Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public AccountAnalysisDetail(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Account_Analysis_Detail", new FieldValue("AccountID", "@AccountID", isUpdateConditionField: true), new FieldValue("AnalysisGroupID", "@AnalysisGroupID"), new FieldValue("TYPE", "@Type"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Account_Analysis_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@AnalysisGroupID", SqlDbType.NVarChar);
			parameters.Add("@Type", SqlDbType.TinyInt);
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@AnalysisGroupID"].SourceColumn = "AnalysisGroupID";
			parameters["@Type"].SourceColumn = "TYPE";
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

		public bool InsertAccountAnalysisDetail(DataSet accountAnalysisData)
		{
			if (accountAnalysisData == null || accountAnalysisData.Tables.Count == 0 || accountAnalysisData.Tables[0].Rows.Count == 0)
			{
				throw new Exception("Table must have at least one row");
			}
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				string str = accountAnalysisData.Tables[0].Rows[0]["AccountID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Transaction = sqlTransaction;
				string text2 = sqlCommand.CommandText = "DELETE \r\n                           FROM Account_Analysis_Detail WHERE AccountID='" + str + "'";
				ExecuteNonQuery(sqlCommand);
				if (accountAnalysisData.Tables["Account_Analysis_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(accountAnalysisData, "Account_Analysis_Detail", sqlCommand);
				}
				string text3 = accountAnalysisData.Tables[0].Rows[0]["AccountID"].ToString();
				AddActivityLog("Account Analysis Detail", text3, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Account_Analysis_Detail", "AccountID", text3, sqlTransaction, isInsert: true);
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

		public bool UpdateAccountAnalysisDetail(DataSet accountAnalysisData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAnalysisData, "Account_Analysis_Detail", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountAnalysisData.Tables[0].Rows[0]["AccountID"];
				UpdateTableRowByID("Account_Analysis_Detail", "AccountID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountAnalysisData.Tables[0].Rows[0]["AccountID"].ToString();
				AddActivityLog("Account Analysis Detail", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Account_Analysis_Detail", "AccountID", obj, sqlTransaction, isInsert: false);
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

		public bool DeleteAccountAnalysis(string accountID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Account_Analysis_Detail WHERE AccountID = '" + accountID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Account Analysis Detail", accountID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAccountAnalysisGroupByAccountID(string accountID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT *\r\n                           FROM Account_Analysis_Detail WHERE AccountID='" + accountID + "'";
			FillDataSet(dataSet, "Account_Analysis_Detail", textCommand);
			return dataSet;
		}
	}
}
