using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public sealed class CompanyAccounts : StoreObject
	{
		private const string ACCOUNTID_PARM = "@AccountID";

		private const string ACCOUNTNAME_PARM = "@AccountName";

		private const string ALIAS_PARM = "@Alias";

		private const string TYPEID_PARM = "@TypeID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string BANKACCOUNTTYPE_PARM = "@BankAccountType";

		private const string BANKACCOUNTNUMBER_PARM = "@BankAccountNumber";

		private const string SUBTYPE_PARM = "@SubType";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string GROUPID_PARM = "@GroupID";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string NOTE_PARM = "@Note";

		private const string USERDEFINED1_PARM = "@UserDefined1";

		private const string USERDEFINED2_PARM = "@UserDefined2";

		private const string USERDEFINED3_PARM = "@UserDefined3";

		private const string USERDEFINED4_PARM = "@UserDefined4";

		public bool CheckConcurrency = true;

		private bool LogEvents = true;

		public CompanyAccounts(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Account", new FieldValue("AccountID", "@AccountID", isUpdateConditionField: true), new FieldValue("AccountName", "@AccountName"), new FieldValue("Alias", "@Alias"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("BankAccountType", "@BankAccountType"), new FieldValue("BankAccountNumber", "@BankAccountNumber"), new FieldValue("SubType", "@SubType"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"), new FieldValue("GroupID", "@GroupID"), new FieldValue("UserDefined1", "@UserDefined1"), new FieldValue("UserDefined2", "@UserDefined2"), new FieldValue("UserDefined3", "@UserDefined3"), new FieldValue("UserDefined4", "@UserDefined4"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Account", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@AccountName", SqlDbType.NVarChar);
			parameters.Add("@Alias", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@BankAccountType", SqlDbType.NVarChar);
			parameters.Add("@BankAccountNumber", SqlDbType.NVarChar);
			parameters.Add("@SubType", SqlDbType.TinyInt);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@UserDefined1", SqlDbType.NVarChar);
			parameters.Add("@UserDefined2", SqlDbType.NVarChar);
			parameters.Add("@UserDefined3", SqlDbType.NVarChar);
			parameters.Add("@UserDefined4", SqlDbType.NVarChar);
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@AccountName"].SourceColumn = "AccountName";
			parameters["@Alias"].SourceColumn = "Alias";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@BankAccountType"].SourceColumn = "BankAccountType";
			parameters["@BankAccountNumber"].SourceColumn = "BankAccountNumber";
			parameters["@SubType"].SourceColumn = "SubType";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@UserDefined1"].SourceColumn = "UserDefined1";
			parameters["@UserDefined2"].SourceColumn = "UserDefined2";
			parameters["@UserDefined3"].SourceColumn = "UserDefined3";
			parameters["@UserDefined4"].SourceColumn = "UserDefined4";
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

		public bool InsertCompanyAccount(CompanyAccountData companyAccountData, string closingPassword)
		{
			return InsertCompanyAccount(companyAccountData, null, closingPassword, null);
		}

		public bool InsertCompanyAccount(CompanyAccountData companyAccountData, string closingPassword, SqlTransaction sqlTransaction)
		{
			return InsertCompanyAccount(companyAccountData, null, closingPassword, sqlTransaction);
		}

		public bool InsertCompanyAccount(CompanyAccountData companyAccountData, CustomFieldData customFieldData, string closingPassword, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			bool flag2 = false;
			if (sqlTransaction == null)
			{
				flag2 = true;
				sqlTransaction = base.DBConfig.StartNewTransaction();
			}
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			insertUpdateCommand.Transaction = sqlTransaction;
			try
			{
				flag &= Insert(companyAccountData, "Account", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				DataRow dataRow = companyAccountData.CompanyAccountTable.Rows[0];
				string text = dataRow["AccountID"].ToString();
				if (LogEvents)
				{
					companyAccountData.CompanyAccountTable.Rows[0]["AccountID"].ToString();
					AddActivityLog("Account", text, ActivityTypes.Add, sqlTransaction);
				}
				if (0m != 0m)
				{
					DateTime date = DateTime.Parse(dataRow["DateCreated"].ToString());
					base.DBConfig.AssertHasClosingDate(date, closingPassword);
					InsertOpenBalanceGLTrans(companyAccountData, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Account", "AccountID", text, sqlTransaction, isInsert: true);
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Accounts, text, "Account", "AccountID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (flag2)
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		private int GetOpeningBalanceGLID(int accountID, SqlTransaction sqlTransaction)
		{
			int num = -1;
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				return int.Parse(ExecuteScalar(stringBuilder.ToString(), sqlTransaction).ToString());
			}
			catch
			{
				return -1;
			}
		}

		public decimal GetOpeningBalance(int accountID, CompanyAccountTypes companyAccountType)
		{
			decimal result = default(decimal);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Journal", stringBuilder.ToString());
				if (dataSet.Tables["Journal"].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables["Journal"].Rows[0];
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					try
					{
						num = decimal.Parse(dataRow["Debit"].ToString());
						num2 = decimal.Parse(dataRow["Credit"].ToString());
						if (companyAccountType != CompanyAccountTypes.Bank && companyAccountType != CompanyAccountTypes.Cash && companyAccountType != CompanyAccountTypes.FixedAsset && companyAccountType != CompanyAccountTypes.OtherAsset && companyAccountType != CompanyAccountTypes.OtherCurrentAsset)
						{
							if (!(num != 0m))
							{
								result = num2;
								return result;
							}
							result = -num;
							return result;
						}
						if (!(num != 0m))
						{
							result = -num2;
							return result;
						}
						result = num;
						return result;
					}
					catch
					{
						return result;
					}
				}
				return result;
			}
			catch
			{
				return default(decimal);
			}
		}

		public bool CreateOpeningBalance(decimal initialBalance, int accountID, DateTime date, CompanyAccountTypes accountType, string closingPassword)
		{
			return true;
		}

		private bool InsertOpenBalanceGLTrans(CompanyAccountData companyAccountData, SqlTransaction sqlTransaction)
		{
			return true;
		}

		private void CreateOpeningBalanceGL(GLData glData, decimal initialAmount, string accountID, string strID, CompanyAccountTypes accountType)
		{
		}

		public bool UpdateCompanyAccount(CompanyAccountData companyAccountData)
		{
			return UpdateCompanyAccount(companyAccountData, null);
		}

		public bool UpdateCompanyAccount(CompanyAccountData companyAccountData, CustomFieldData customFieldData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				object companyOptionValue = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.DocumentVersioning, false);
				bool flag2 = false;
				if (!companyOptionValue.IsNullOrEmpty())
				{
					flag2 = bool.Parse(companyOptionValue.ToString());
				}
				if (flag2)
				{
					string text = companyAccountData.CompanyAccountTable.Rows[0]["AccountID"].ToString();
					DataSet accountByID = GetAccountByID(text);
					new ActivityLogs(base.DBConfig).InsertDocumentVersion(ScreenTypes.Card, 4.ToString(), "", text, accountByID, sqlTransaction);
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(companyAccountData, "Account", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = companyAccountData.CompanyAccountTable.Rows[0]["AccountID"];
				if (customFieldData != null)
				{
					new CustomFields(base.DBConfig).InsertUpdateCustomField(customFieldData, "Account", obj);
				}
				UpdateTableRowByID("Account", "AccountID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = companyAccountData.CompanyAccountTable.Rows[0]["AccountName"].ToString();
				AddActivityLog("Account", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Account", "AccountID", obj, sqlTransaction, isInsert: false);
				flag &= new Approval(base.DBConfig).CreateCardApprovalTasks(DataComboType.Accounts, obj.ToString(), "Account", "AccountID", sqlTransaction);
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

		public DataSet GetAccountsByFields(params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Account");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Account", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAccountsByFields(int[] accountsID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Account");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (accountsID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "AccountID";
				commandHelper.FieldValue = accountsID;
				commandHelper.TableName = "Account";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.IsComparing = true;
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Account", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAccountsList(bool includeInactive, bool isHierarchy)
		{
			string text = "";
			DataSet dataSet;
			if (isHierarchy)
			{
				dataSet = new DataSet();
				text = "SELECT ISNULL(Inactive,'False') AS [I], GroupID AS [Account Code],GroupName AS [Name],'' AS Alias,'True' AS IsGroup, ParentID\r\n                            FROM Account_Group\r\n                            WHERE Inactive IS NULL OR Inactive='False' AND ParentID IS NULL";
				FillDataSet(dataSet, "Account_Group", text);
				text = "SELECT ISNULL(Inactive,'False') AS [I], GroupID AS [Account Code],GroupName AS [Name],'' AS Alias,'True' AS IsGroup, ParentID \r\n                            FROM Account_Group\r\n                            WHERE  ParentID IS NOT NULL\r\n\r\n                            UNION \r\n\r\n                            Select ISNULL(IsInactive,'False') AS [I],AccountID AS [Account Code],AccountName AS Name, Alias,'False' AS IsGroup ,  GroupID AS ParentID  \r\n                            FROM Account";
				if (!includeInactive)
				{
					text += " WHERE Account.IsInactive IS NULL OR Account.IsInactive='False'";
				}
				FillDataSet(dataSet, "Account", text);
				dataSet.Relations.Add("Rel", dataSet.Tables["Account_Group"].Columns["Account Code"], dataSet.Tables["Account"].Columns["ParentID"], createConstraints: false);
				dataSet.Relations.Add("Rel2", dataSet.Tables["Account"].Columns["Account Code"], dataSet.Tables["Account"].Columns["ParentID"], createConstraints: false);
			}
			else
			{
				text = "SELECT     Account.IsInactive AS [I],Account.AccountID AS [Account Code], Account.AccountName AS [Account Name], Account.Alias, \r\n                      Account.GroupID + ' - ' + AG.GroupName AS [Account Group],  Account.Note, \r\n                      Account.UserDefined1, Account.UserDefined2, Account.UserDefined3, Account.UserDefined4\r\n                        FROM         Account INNER JOIN\r\n                      Account_Group AS AG ON Account.GroupID = AG.GroupID";
				if (!includeInactive)
				{
					text += " WHERE Account.IsInactive IS NULL OR Account.IsInactive='False'";
				}
				dataSet = new DataSet();
				FillDataSet(dataSet, "Account", text);
				text = "SELECT     AccountCustomFieldName1, AccountCustomFieldName2, AccountCustomFieldName3, AccountCustomFieldName4\r\n                    FROM         GL_Setup\r\n                    WHERE     (CompanyID = '1')";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "GL_Setup", text);
				if (dataSet2 != null && dataSet2.Tables.Count > 0 && dataSet2.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet2.Tables[0].Rows[0];
					dataSet.Tables[0].Columns["UserDefined1"].ColumnName = dataRow[0].ToString();
					dataSet.Tables[0].Columns["UserDefined2"].ColumnName = dataRow[1].ToString();
					dataSet.Tables[0].Columns["UserDefined3"].ColumnName = dataRow[2].ToString();
					dataSet.Tables[0].Columns["UserDefined4"].ColumnName = dataRow[3].ToString();
				}
			}
			return dataSet;
		}

		public DataSet GetAccountsListHierarchy(bool includeInactive)
		{
			string textCommand = "SELECT     Account.IsInactive AS [I],Account.AccountID AS [Account Code], Account.AccountName AS [Account Name], Account.Alias, \r\n                      Account.GroupID AS ParentID,  Account.Note\r\n                       \r\n                        FROM         Account INNER JOIN\r\n                      Account_Group AS AG ON Account.GroupID = AG.GroupID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Account_Group", textCommand);
			textCommand = "Select Inactive AS I,GroupID AS  [Account Code],GroupName AS [Account Name], '' AS Alias ,ParentID ,Note FROM Account_Group";
			FillDataSet(dataSet, "Account", textCommand);
			dataSet.Relations.Add("P", dataSet.Tables[0].Columns["Account Code"], dataSet.Tables[0].Columns["ParentID"], createConstraints: false);
			dataSet.Relations.Add("P2", dataSet.Tables[0].Columns["Account Code"], dataSet.Tables[1].Columns["ParentID"], createConstraints: false);
			return dataSet;
		}

		public CompanyAccountData GetAccountByID(string id)
		{
			CompanyAccountData companyAccountData = new CompanyAccountData();
			string textCommand = "SELECT AC.*,(SELECT COUNT(J.VoucherID)\r\n                            FROM Journal J INNER JOIN  Journal_Details JD\r\n                            ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                            WHERE JD.AccountID = AC.AccountID AND J.CurrencyID <>(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS HasTransaction\r\n                            FROM Account AC \r\n                            WHERE AccountID = '" + id.ToString() + "'";
			FillDataSet(companyAccountData, "Account", textCommand);
			return companyAccountData;
		}

		internal object GetAccountIDByName(string name, SqlTransaction sqlTransaction)
		{
			return ExecuteSelectScalar("Account", "AccountName", name, "AccountID", sqlTransaction);
		}

		public DataSet GetAccountsName()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddOrderByColumn("Account", "AccountName");
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Account", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCOGSAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsCOGS",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.IsComparing = true;
			sqlBuilder.UseDistinct = false;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetIncomeAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsIncome",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsOtherIncome",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.IsComparing = true;
			sqlBuilder.UseDistinct = false;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetExpenseAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsExpense";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsOtherExpense";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsCOGS";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddColumn("Account", "TypeID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.IsComparing = true;
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Account", sqlBuilder);
			return dataSet;
		}

		public DataSet GetOtherIncomeAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsOtherIncome",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddColumn("Account", "TypeID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.IsComparing = true;
			sqlBuilder.UseDistinct = false;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetOtherCurrentAssetAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsOtherCurrentAsset",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddColumn("Account", "TypeID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.IsComparing = true;
			sqlBuilder.UseDistinct = false;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetAccounts(params string[] accountTypeFields)
		{
			int num = 0;
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = accountTypeFields[0];
			commandHelper.FieldValue = 1;
			commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			if (accountTypeFields.Length > 1)
			{
				commandHelper.IsFirstField = true;
				commandHelper.IsLastField = false;
			}
			for (num = 1; num < accountTypeFields.Length - 1; num++)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "[Account Types]";
				commandHelper.FieldName = accountTypeFields[num];
				commandHelper.FieldValue = 1;
				commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
				commandHelper.SqlFieldType = SqlDbType.Int;
				commandHelper.IsFirstField = false;
				commandHelper.IsLastField = false;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (accountTypeFields.Length > 1)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "[Account Types]";
				commandHelper.FieldName = accountTypeFields[num];
				commandHelper.FieldValue = 1;
				commandHelper.SqlFieldType = SqlDbType.Int;
				commandHelper.IsFirstField = false;
				commandHelper.IsLastField = true;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.AddOrderByColumn("Account", "AccountName");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Account", sqlBuilder);
			return dataSet;
		}

		public DataSet GetARAccounts(SqlTransaction sqlTransaction)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsAR",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetARAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsAR",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetBankAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsBank",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddColumn("Account", "TypeID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetExpenseIncomeAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsCOGS";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsExpense";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsIncome";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsOtherExpense";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Account Types]";
			commandHelper.FieldName = "IsOtherIncome";
			commandHelper.FieldValue = 1;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddColumn("Account", "TypeID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.AddOrderByColumn("Account", "TypeID");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Account", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAPAccounts()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsAP",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public DataSet GetAPAccounts(SqlTransaction sqlTransaction)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			sqlBuilder.AddCommandHelper(new CommandHelper
			{
				TableName = "[Account Types]",
				FieldName = "IsAP",
				FieldValue = 1,
				SqlFieldType = SqlDbType.Int
			});
			sqlBuilder.AddColumn("Account", "AccountName");
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddJointer("Account", "TypeID", "[Account Types]", "TypeID");
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = true;
			return new DataSet
			{
				EnforceConstraints = false
			};
		}

		public bool DeleteAccount(string accountID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Account WHERE AccountID = '" + accountID.ToString() + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Account", accountID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetAccountNameByID(object accountID)
		{
			try
			{
				object obj = ExecuteSelectScalar("Account", "AccountID", accountID, "AccountName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public string GetAccountID(string shortName, SqlTransaction sqlTransaction)
		{
			string result = null;
			string exp = "SELECT AccountID FROM Account WHERE AccountName ='" + shortName + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj != DBNull.Value)
			{
				result = obj.ToString();
			}
			return result;
		}

		public bool ExistAccountName(string shortName)
		{
			return ExistAccountName(shortName, -1);
		}

		public string GetAccountIDByName(string accountName)
		{
			object accountIDByName = GetAccountIDByName(accountName, null);
			if (accountIDByName == null || accountIDByName == DBNull.Value)
			{
				return "-1";
			}
			return accountIDByName.ToString();
		}

		public string GetAccountIDByNameNumber(string accountName, string accountNumber)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("SELECT ").Append("AccountID");
				stringBuilder.Append(" FROM ").Append("Account");
				stringBuilder.Append(" WHERE ").Append("AccountName").Append("='");
				stringBuilder.Append(accountName).Append("'").Append(" AND ")
					.Append("AccountID")
					.Append("='");
				stringBuilder.Append(accountNumber).Append("'");
				object obj = ExecuteScalar(stringBuilder.ToString());
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public bool ExistAccountID(int accountID)
		{
			try
			{
				return IsTableFieldValueExist("Account", "AccountID", accountID);
			}
			catch
			{
				throw;
			}
		}

		public bool ActivateAccount(object id, bool activate)
		{
			string accountNameByID = GetAccountNameByID(id);
			activate = !activate;
			bool num = UpdateTableRowByID("Account", "AccountID", "IsInactive", id, Convert.ToByte(activate));
			if (num)
			{
				if (!activate)
				{
					AddActivityLog("Account", accountNameByID, ActivityTypes.Activate, null);
					return num;
				}
				AddActivityLog("Account", accountNameByID, ActivityTypes.Inactivate, null);
			}
			return num;
		}

		public object GetOrCreateAccount(string accountName, SqlTransaction sqlTransaction)
		{
			object obj = null;
			using (CompanyAccounts companyAccounts = new CompanyAccounts(base.DBConfig))
			{
				obj = companyAccounts.GetAccountIDByName(accountName, sqlTransaction);
			}
			if (obj == null)
			{
				CompanyAccountData companyAccountData = new CompanyAccountData();
				DataRow dataRow = companyAccountData.CompanyAccountTable.NewRow();
				dataRow["AccountName"] = "Opening Balance Equity";
				dataRow["TypeID"] = CompanyAccountTypes.Equity;
				dataRow["DateCreated"] = DateTime.Now;
				companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				using (CompanyAccounts companyAccounts2 = new CompanyAccounts(base.DBConfig))
				{
					companyAccounts2.InsertCompanyAccount(companyAccountData, string.Empty, sqlTransaction);
				}
				obj = dataRow["AccountID"];
			}
			return obj;
		}

		public bool IsParent(object parentAccount, object subAccount)
		{
			if (parentAccount == null || subAccount == null)
			{
				return false;
			}
			if (parentAccount == DBNull.Value || subAccount == DBNull.Value)
			{
				return false;
			}
			return IsParent(parentAccount, subAccount, "Account", "GroupID", "AccountID");
		}

		public bool ExistAccountName(string accountName, int parentID)
		{
			string str = "SELECT AccountID FROM Account WHERE AccountName  = '" + accountName + "' AND GroupID ";
			str = ((parentID >= 0) ? (str + "=" + parentID.ToString()) : (str + " IS NULL"));
			object obj = ExecuteScalar(str);
			if (obj != DBNull.Value)
			{
				return obj != null;
			}
			return false;
		}

		public bool ExistAccountNumber(string accountNumber)
		{
			try
			{
				return IsTableFieldValueExist("Account", "AccountID", accountNumber);
			}
			catch
			{
				throw;
			}
		}

		public int GetParentLevel(object parentID)
		{
			return GetParentLevel(parentID, "Account", "GroupID", "AccountID");
		}

		internal CompanyAccountTypes GetAccountType(string accountID)
		{
			if (accountID == null)
			{
				throw new NullReferenceException("Account is null.");
			}
			object obj = ExecuteSelectScalar("Account", "AccountID", accountID, "TypeID");
			if (obj != null && obj != DBNull.Value)
			{
				return (CompanyAccountTypes)byte.Parse(obj.ToString());
			}
			return CompanyAccountTypes.None;
		}

		internal DataSet GetAccountTypes(int[] accountsID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (accountsID != null && accountsID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "AccountID";
				commandHelper.FieldValue = accountsID;
				commandHelper.SqlFieldType = SqlDbType.Int;
				commandHelper.TableName = "Account";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.AddColumn("Account", "AccountID");
			sqlBuilder.AddColumn("Account", "TypeID");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Account", sqlBuilder);
			return dataSet;
		}

		public DataSet GetSystemAccounts()
		{
			return GetSystemAccounts(null);
		}

		internal DataSet GetSystemAccounts(SqlTransaction sqlTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT AccountID,AccountName,SystemAccountType FROM ");
			stringBuilder.Append("Account").Append(" WHERE SystemAccountType IS NOT NULL");
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Account", stringBuilder.ToString(), sqlTransaction);
			return dataSet;
		}

		internal int GetSystemAccount(SystemAccountTypes systemAccountType, SqlTransaction sqlTransaction)
		{
			return -1;
		}

		internal int CreateSystemAccount(string accountName, SystemAccountTypes systemAccountType, SqlTransaction sqlTransaction)
		{
			CompanyAccountData companyAccountData = new CompanyAccountData();
			CompanyAccountTypes companyAccountTypes = CompanyAccountTypes.AccountReceivable;
			switch (systemAccountType)
			{
			case SystemAccountTypes.AccountsPayable:
				companyAccountTypes = CompanyAccountTypes.AccountsPayable;
				break;
			case SystemAccountTypes.AccountsReceivable:
				companyAccountTypes = CompanyAccountTypes.AccountReceivable;
				break;
			case SystemAccountTypes.BankServiceCharges:
				companyAccountTypes = CompanyAccountTypes.Expense;
				break;
			case SystemAccountTypes.CashDiscountGiven:
				companyAccountTypes = CompanyAccountTypes.Income;
				break;
			case SystemAccountTypes.CashDiscountTaken:
				companyAccountTypes = CompanyAccountTypes.CostofGoodsSold;
				break;
			case SystemAccountTypes.OpeningBalanceEquity:
				companyAccountTypes = CompanyAccountTypes.Equity;
				break;
			case SystemAccountTypes.RetainedEarnings:
				companyAccountTypes = CompanyAccountTypes.Equity;
				break;
			case SystemAccountTypes.SalesTaxPayable:
				companyAccountTypes = CompanyAccountTypes.OtherCurrentLiability;
				break;
			case SystemAccountTypes.UndepositedFunds:
			case SystemAccountTypes.UndepositedChecks:
				companyAccountTypes = CompanyAccountTypes.OtherCurrentAsset;
				break;
			case SystemAccountTypes.UnwithdrawnChecks:
				companyAccountTypes = CompanyAccountTypes.OtherCurrentLiability;
				break;
			case SystemAccountTypes.WriteOff:
				companyAccountTypes = CompanyAccountTypes.Income;
				break;
			}
			object accountIDByName = GetAccountIDByName(accountName, sqlTransaction);
			if (accountIDByName != null && accountIDByName != DBNull.Value)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("UPDATE ").Append("Account");
				stringBuilder.Append(" SET SystemAccountType=").Append((byte)systemAccountType);
				stringBuilder.Append(" WHERE AccountID=").Append(accountIDByName.ToString());
				ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				return int.Parse(accountIDByName.ToString());
			}
			DataRow dataRow = companyAccountData.CompanyAccountTable.NewRow();
			dataRow["AccountName"] = accountName;
			dataRow["TypeID"] = companyAccountTypes;
			companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
			try
			{
				LogEvents = false;
				if (InsertCompanyAccount(companyAccountData, "", sqlTransaction))
				{
					dataRow = companyAccountData.CompanyAccountTable.Rows[0];
					return int.Parse(dataRow["AccountID"].ToString());
				}
			}
			finally
			{
				LogEvents = true;
			}
			return -1;
		}

		internal bool CreateSystemAccounts(SqlTransaction sqlTransaction, bool isUpdating)
		{
			DataRow dataRow = null;
			object obj = null;
			StringBuilder stringBuilder = null;
			CompanyAccountData companyAccountData = new CompanyAccountData();
			DataSet systemAccounts = GetSystemAccounts(sqlTransaction);
			DataRow[] array = null;
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)3);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)3);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.AccountsPayable;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)2);
			if (array == null || array.Length == 0)
			{
				if (obj == null || obj == DBNull.Value)
				{
					GetARAccounts(sqlTransaction);
				}
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)2);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.AccountReceivable;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)10);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)10);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.Expense;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)5);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)5);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.Income;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)6);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)6);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.CostofGoodsSold;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)1);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)1);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.Equity;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)11);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)11);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.Equity;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			if (new CompanyInformations(base.DBConfig).IsTaxSystem())
			{
				array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)4);
				if (array == null || array.Length == 0)
				{
					if (obj != null && obj != DBNull.Value)
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append("UPDATE ").Append("Account");
						stringBuilder.Append(" SET SystemAccountType=").Append((byte)4);
						stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
						ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
					}
					else
					{
						dataRow = companyAccountData.CompanyAccountTable.NewRow();
						dataRow["TypeID"] = CompanyAccountTypes.OtherCurrentLiability;
						companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
					}
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)7);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)7);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.OtherCurrentAsset;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)8);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)8);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.OtherCurrentAsset;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)9);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)9);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.OtherCurrentLiability;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			array = systemAccounts.Tables["Account"].Select("SystemAccountType = " + (byte)12);
			if (array == null || array.Length == 0)
			{
				if (obj != null && obj != DBNull.Value)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append("UPDATE ").Append("Account");
					stringBuilder.Append(" SET SystemAccountType=").Append((byte)12);
					stringBuilder.Append(" WHERE AccountID=").Append(obj.ToString());
					ExecuteScalar(stringBuilder.ToString(), sqlTransaction);
				}
				else
				{
					dataRow = companyAccountData.CompanyAccountTable.NewRow();
					dataRow["TypeID"] = CompanyAccountTypes.Income;
					companyAccountData.CompanyAccountTable.Rows.Add(dataRow);
				}
			}
			if (companyAccountData.CompanyAccountTable.Rows.Count == 0)
			{
				return true;
			}
			try
			{
				LogEvents = false;
				return InsertCompanyAccount(companyAccountData, "", sqlTransaction);
			}
			finally
			{
				LogEvents = true;
			}
		}

		internal int[] GetAccountParentsChildsID(int accountID)
		{
			ArrayList arrayList = new ArrayList();
			int num = accountID;
			string text = "";
			while (true)
			{
				text = "SELECT GroupID FROM Account WHERE AccountID=" + num.ToString();
				object obj = ExecuteScalar(text);
				if (obj == DBNull.Value || obj == null)
				{
					break;
				}
				num = int.Parse(obj.ToString());
				arrayList.Add(num);
			}
			text = "SELECT AccountID FROM Account WHERE GroupID=" + accountID.ToString();
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Account", text);
			if (dataSet != null && dataSet.Tables["Account"].Rows.Count > 0)
			{
				foreach (DataRow row in dataSet.Tables["Account"].Rows)
				{
					num = int.Parse(row["AccountID"].ToString());
					arrayList.Add(num);
				}
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		public DataSet GetAccountsComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AccountID [Code],AccountName [Name],\r\n                            CASE TypeID WHEN 1 THEN 'Asset' WHEN 2 THEN 'Liability' WHEN 3 THEN 'Income' \r\n                            WHEN 4 THEN 'Expense' WHEN 5 THEN 'Equity' END  + ' - ' + \r\n                            CASE SubType WHEN 1 THEN 'General' WHEN 2 THEN 'Cash' WHEN 3 THEN 'Bank' WHEN 4 THEN 'A/R' WHEN 5 THEN 'A/P'\r\n                            WHEN 6 THEN 'Inventory' WHEN 7 THEN 'Sales' WHEN 8 THEN 'COGS' WHEN 9 THEN 'WIP' END AS SubTypeName ,\r\n                            SubType,TypeID,Account.GroupID\r\n                           FROM Account INNER JOIN Account_Group AG ON Account.GroupID=AG.GroupID WHERE ISINACTIVE<>1";
			FillDataSet(dataSet, "Account", textCommand);
			return dataSet;
		}

		public string GetNextAccountNumber(string groupID)
		{
			string exp = "SELECT MAX(AccountID) FROM \r\n                           Account WHERE GroupID='" + groupID + "'";
			object obj = ExecuteScalar(exp);
			string text = "";
			if (obj != null && obj.ToString() != "")
			{
				text = obj.ToString();
				int num = 0;
				string text2 = "";
				int length = 0;
				for (int num2 = text.Length - 1; num2 >= 0; num2--)
				{
					int result = 0;
					if (!int.TryParse(text.Substring(num2, 1), out result))
					{
						break;
					}
					text2 = text.Substring(num2, 1) + text2;
					length = num2;
				}
				num = 0;
				int.TryParse(text2, out num);
				int length2 = text2.Length;
				string text3 = "";
				for (int i = 0; i < length2; i++)
				{
					text3 += "0";
				}
				num++;
				text = text.Substring(0, length);
				return text + num.ToString(text3);
			}
			return groupID + "1";
		}

		public DataSet GetAccountListReport()
		{
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			string textCommand = "WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L FROM Account_Group AG\r\n\t                                WHERE ParentID IS NULL AND TypeID=1\r\n\t                                UNION ALL\r\n\t                                SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L\r\n                                    FROM Account_Group AG\r\n                                    INNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT  GroupID AS Code, GroupName AS Name,TypeID,ParentID ,L,'G' AS T FROM ACCGroups\r\n                                UNION\r\n\t\t\t\t\t\t\t\tSELECT AccountID AS Code, AccountName AS Name, 1 AS TypeID,A.GroupID AS ParentID,L+1 AS L, 'A' AS T FROM Account A\r\n\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AG ON A.GroupID=AG.GroupID\r\n\t\t\t\t\t\t\t\tWHERE TypeID=1\r\n                                ORDER BY T,Code\r\n                            ";
			FillDataSet(dataSet2, "Asset", textCommand);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables["Asset"].Columns["Code"]
			};
			dataSet2.Relations.Add("Rel", dataSet2.Tables["Asset"].Columns["Code"], dataSet2.Tables["Asset"].Columns["ParentID"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L FROM Account_Group AG\r\n\t                                WHERE ParentID IS NULL AND TypeID=2\r\n\t                                UNION ALL\r\n\t                                SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L\r\n                                    FROM Account_Group AG\r\n                                    INNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT  GroupID AS Code, GroupName AS Name,TypeID,ParentID ,L, 'G' AS T FROM ACCGroups\r\n                                UNION\r\n\t\t\t\t\t\t\t\tSELECT AccountID AS Code, AccountName AS Name, 1 AS TypeID,A.GroupID AS ParentID,L+1 AS L, 'A' AS T  FROM Account A\r\n\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AG ON A.GroupID=AG.GroupID\r\n\t\t\t\t\t\t\t\tWHERE TypeID=2\r\n                                    ORDER BY T,Code\r\n                                    ";
			FillDataSet(dataSet, "Liability", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Liability"].Columns["Code"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet2.Relations.Add("Rel2", dataSet2.Tables["Liability"].Columns["Code"], dataSet2.Tables["Liability"].Columns["ParentID"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L FROM Account_Group AG\r\n\t                                WHERE ParentID IS NULL AND TypeID=3\r\n\t                                UNION ALL\r\n\t                                SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L\r\n                                    FROM Account_Group AG\r\n                                    INNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT  GroupID AS Code, GroupName AS Name,TypeID,ParentID ,L,'G' AS T  FROM ACCGroups\r\n                                UNION\r\n\t\t\t\t\t\t\t\tSELECT AccountID AS Code, AccountName AS Name, 1 AS TypeID,A.GroupID AS ParentID,L+1 AS L,'A' AS T  FROM Account A\r\n\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AG ON A.GroupID=AG.GroupID\r\n\t\t\t\t\t\t\t\tWHERE TypeID=3   ORDER BY T,Code";
			FillDataSet(dataSet, "Income", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Income"].Columns["Code"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet2.Relations.Add("Rel3", dataSet2.Tables["Income"].Columns["Code"], dataSet2.Tables["Income"].Columns["ParentID"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L FROM Account_Group AG\r\n\t                                WHERE ParentID IS NULL AND TypeID=4\r\n\t                                UNION ALL\r\n\t                                SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L\r\n                                    FROM Account_Group AG\r\n                                    INNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT  GroupID AS Code, GroupName AS Name,TypeID,ParentID ,L,'G' AS T  FROM ACCGroups\r\n                                UNION\r\n\t\t\t\t\t\t\t\tSELECT AccountID AS Code, AccountName AS Name, 1 AS TypeID,A.GroupID AS ParentID,L+1 AS L,'A' AS T  FROM Account A\r\n\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AG ON A.GroupID=AG.GroupID\r\n\t\t\t\t\t\t\t\tWHERE TypeID=4   ORDER BY T,Code";
			FillDataSet(dataSet, "Expense", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Expense"].Columns["Code"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet2.Relations.Add("Rel4", dataSet2.Tables["Expense"].Columns["Code"], dataSet2.Tables["Expense"].Columns["ParentID"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L  FROM Account_Group AG\r\n\t                                WHERE ParentID IS NULL AND TypeID=5\r\n\t                                UNION ALL\r\n\t                                SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L\r\n                                    FROM Account_Group AG\r\n                                    INNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT  GroupID AS Code, GroupName AS Name,TypeID,ParentID ,L,'G' AS T FROM ACCGroups\r\n                                UNION\r\n\t\t\t\t\t\t\t\tSELECT AccountID AS Code, AccountName AS Name, 1 AS TypeID,A.GroupID AS ParentID,L+1 AS L,'A' AS T FROM Account A\r\n\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AG ON A.GroupID=AG.GroupID\r\n\t\t\t\t\t\t\t\tWHERE TypeID=5   ORDER BY T,Code";
			FillDataSet(dataSet, "Capital", textCommand);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Capital"].Columns["Code"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet2.Relations.Add("Rel5", dataSet2.Tables["Capital"].Columns["Code"], dataSet2.Tables["Capital"].Columns["ParentID"]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID Code,AccountName Name,A.GroupID AS ParentID,TypeID,\r\n                        0 AS Balance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                       \r\n                        AND TypeID IN (1,2,5)\r\n                        GROUP BY A.AccountID,AccountName,TypeID,A.GroupID";
			FillDataSet(dataSet, "Accounts", textCommand);
			dataSet.Tables["Accounts"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Accounts"].Columns["Code"]
			};
			dataSet2.Merge(dataSet.Tables["Accounts"]);
			dataSet2.Relations.Add("RelAccAsset", dataSet2.Tables["Asset"].Columns["Code"], dataSet2.Tables["Accounts"].Columns["ParentID"], createConstraints: false);
			dataSet2.Relations.Add("RelAccLiability", dataSet2.Tables["Liability"].Columns["Code"], dataSet2.Tables["Accounts"].Columns["ParentID"], createConstraints: false);
			dataSet2.Relations.Add("RelAccIncome", dataSet2.Tables["Income"].Columns["Code"], dataSet2.Tables["Accounts"].Columns["ParentID"], createConstraints: false);
			dataSet2.Relations.Add("RelAccExpense", dataSet2.Tables["Expense"].Columns["Code"], dataSet2.Tables["Accounts"].Columns["ParentID"], createConstraints: false);
			dataSet2.Relations.Add("RelAccEquity", dataSet2.Tables["Capital"].Columns["Code"], dataSet2.Tables["Accounts"].Columns["ParentID"], createConstraints: false);
			return dataSet2;
		}

		public decimal GetAccountBalance(string accountID, bool includeOD)
		{
			if (accountID == "")
			{
				return 0m;
			}
			string str = "SELECT SUM(ISNULL(Debit,0)) - SUM(ISNULL(Credit,0)) ";
			if (includeOD)
			{
				str = str + " + ISNULL((SELECT LimitAmount FROM Bank_Facility WHERE CurrentAccountID = '" + accountID + "' AND FacilityType = 5 AND Status = 1),0) ";
			}
			str = str + "        FROM Journal_Details\r\n                                WHERE AccountID='" + accountID + "'";
			object obj = ExecuteScalar(str);
			if (obj == null || obj.ToString() == "")
			{
				return 0m;
			}
			return decimal.Parse(obj.ToString());
		}

		public DataSet GetFavoriteAccounts()
		{
			new DataSet();
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Account.AccountID [ACC Number],AccountName [Account Name],CurrencyID [CUR],\r\n                            ISNULL((SELECT CASE WHEN (Account.CurrencyID IS NULL OR Account.CurrencyID = (SELECT CurrencyID FROM Currency WHERE IsBase='True'))\r\n                             THEN SUM(ISNULL(Debit,0)- ISNULL(Credit,0))\r\n\t\t\t\t\t\t\t ELSE SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0)) END AS Balance\r\n\t\t\t\t\t\t\t FROM Journal_Details JD WHERE Account.AccountID=JD.AccountID \r\n\t\t\t\t\t\t\t AND ISNULL(JD.IsVoid,'False')='False'),0) AS Balance\r\n                            FROM Account LEFT OUTER JOIN Account_Group AG ON Account.GroupID=AG.GroupID\r\n                            WHERE (SubType=3 OR SubType=2) AND ISNULL(IsInactive,'False')='False'\r\n                            AND ISNULL(IsFavorite,'True')='True'\r\n                            GROUP BY Account.AccountID,AccountName,GroupName,Account.CurrencyID\r\n                            ORDER BY Account.AccountID";
			FillDataSet(dataSet, "Account", textCommand);
			return dataSet;
		}
	}
}
