using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Text;

namespace Micromind.Facade
{
	public sealed class CompanyAccountSystem : MarshalByRefObject, ICompanyAccountSystem, IDisposable
	{
		private Config config;

		public CompanyAccountSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCompanyAccount(string shortName, string descriptions, int accountTypeID, int currencyID, decimal initialAmount)
		{
			CompanyAccountData companyAccountData = new CompanyAccountData();
			DataTable dataTable = companyAccountData.Tables["Account"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["AccountName"] = shortName;
			dataRow["Alias"] = descriptions;
			dataRow["TypeID"] = accountTypeID;
			dataRow["CurrencyID"] = currencyID;
			dataRow["InitialBalance"] = initialAmount;
			dataRow["IsInactive"] = false;
			dataRow["DateCreated"] = DateTime.Today;
			dataTable.Rows.Add(dataRow);
			return CreateCompanyAccount(companyAccountData);
		}

		public bool CreateCompanyAccount(string shortName, CompanyAccountTypes accountType)
		{
			CompanyAccountData companyAccountData = new CompanyAccountData();
			DataTable dataTable = companyAccountData.Tables["Account"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["AccountName"] = shortName;
			dataRow["TypeID"] = checked((byte)accountType);
			dataTable.Rows.Add(dataRow);
			return CreateCompanyAccount(companyAccountData);
		}

		public bool CreateCompanyAccount(CompanyAccountData companyAccountData)
		{
			return new CompanyAccounts(config).InsertCompanyAccount(companyAccountData, "");
		}

		public bool UpdateCompanyAccount(CompanyAccountData companyAccountData)
		{
			return UpdateCompanyAccount(companyAccountData, null, checkConcurrency: true);
		}

		public bool UpdateCompanyAccount(CompanyAccountData companyAccountData, CustomFieldData customFieldData)
		{
			return UpdateCompanyAccount(companyAccountData, customFieldData, checkConcurrency: true);
		}

		public bool UpdateCompanyAccount(CompanyAccountData companyAccountData, bool checkConcurrency)
		{
			return UpdateCompanyAccount(companyAccountData, null, checkConcurrency);
		}

		private bool UpdateCompanyAccount(CompanyAccountData companyAccountData, CustomFieldData customFieldData, bool checkConcurrency)
		{
			return new CompanyAccounts(config).UpdateCompanyAccount(companyAccountData);
		}

		public DataSet GetAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccounts();
			}
		}

		public bool DeleteAccount(string accountID)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.DeleteAccount(accountID);
			}
		}

		public DataSet GetAccountsByFields(params string[] columns)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountsByFields(columns);
			}
		}

		public DataSet GetAccountsList(bool includeInactive, bool isHierarchy)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountsList(includeInactive, isHierarchy);
			}
		}

		public DataSet GetAccountsByFields(int[] accountsID, params string[] columns)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountsByFields(accountsID, columns);
			}
		}

		public DataSet GetARAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetARAccounts();
			}
		}

		public DataSet GetBankAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetBankAccounts();
			}
		}

		public DataSet GetAPAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAPAccounts();
			}
		}

		public DataSet GetCOGSAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetCOGSAccounts();
			}
		}

		public DataSet GetIncomeAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetIncomeAccounts();
			}
		}

		public DataSet GetOtherCurrentAssetAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetOtherCurrentAssetAccounts();
			}
		}

		public DataSet GetAccounts(params string[] accountTypeFields)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccounts(accountTypeFields);
			}
		}

		public DataSet GetAccountsName()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountsName();
			}
		}

		public bool ExistAccountName(string shortName)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.ExistAccountName(shortName);
			}
		}

		public bool ExistAccountID(int accountID)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.ExistAccountID(accountID);
			}
		}

		public DataSet GetExpenseIncomeAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetExpenseIncomeAccounts();
			}
		}

		public bool ActivateAccount(object id, bool activate)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.ActivateAccount(id, activate);
			}
		}

		public CompanyAccountData GetAccountByID(string id)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountByID(id);
			}
		}

		public DataSet GetExpenseAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetExpenseAccounts();
			}
		}

		public bool IsParent(object parentAccount, object subAccount)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.IsParent(parentAccount, subAccount);
			}
		}

		public bool CreateUpdateCompanyAccountBatch(DataSet listData, bool checkConcurrency)
		{
			ApplicationAssert.CheckCondition(listData != null, "listData Parameter cannot be null.", 0);
			ApplicationAssert.CheckCondition(listData.Tables.Contains("Account"), "CompanyAccount Data must exist.", 0);
			CompanyAccounts companyAccounts = new CompanyAccounts(config);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				foreach (DataRow row in listData.Tables["Account"].Rows)
				{
					bool flag = false;
					string text = "-1";
					string text2 = string.Empty;
					string text3 = string.Empty;
					if (listData.Tables["Account"].Columns.Contains("AccountName"))
					{
						text2 = row["AccountName"].ToString();
					}
					if (listData.Tables["Account"].Columns.Contains("AccountID"))
					{
						text3 = row["AccountID"].ToString();
					}
					if (!(text2 == string.Empty))
					{
						CompanyAccountData companyAccountData = new CompanyAccountData();
						DataRow dataRow2 = companyAccountData.CompanyAccountTable.NewRow();
						bool flag2 = true;
						bool flag3 = true;
						foreach (DataColumn column in listData.Tables["Account"].Columns)
						{
							try
							{
								if (companyAccountData.CompanyAccountTable.Columns.Contains(column.ColumnName))
								{
									dataRow2[column.ColumnName] = row[column.ColumnName];
								}
								else if (column.ColumnName.ToLower() == "OverwriteExistingRecord".ToLower())
								{
									flag2 = bool.Parse(row[column.ColumnName].ToString());
								}
								else if (column.ColumnName.ToLower() == "CreateNewRecord".ToLower())
								{
									flag3 = bool.Parse(row[column.ColumnName].ToString());
								}
							}
							catch
							{
							}
						}
						companyAccountData.CompanyAccountTable.Rows.Add(dataRow2);
						if (companyAccounts.ExistAccountName(text2, -1))
						{
							flag = true;
							text = ((!(text3 != string.Empty)) ? companyAccounts.GetAccountIDByName(text2) : companyAccounts.GetAccountIDByNameNumber(text2, text3));
							if (text != null)
							{
								companyAccountData.CompanyAccountTable.AcceptChanges();
								dataRow2["AccountID"] = text;
							}
							else
							{
								flag = false;
							}
						}
						try
						{
							if (!flag)
							{
								if (flag3)
								{
									CreateCompanyAccount(companyAccountData);
								}
							}
							else if (flag2)
							{
								UpdateCompanyAccount(companyAccountData, checkConcurrency);
							}
						}
						catch (Exception ex)
						{
							stringBuilder.Append(ex.Message).Append("\n");
						}
						companyAccountData.Dispose();
						companyAccountData = null;
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				companyAccounts.Dispose();
				companyAccounts = null;
				if (stringBuilder.Length > 0)
				{
					ApplicationLog.WriteImportLogError(stringBuilder);
				}
				stringBuilder = null;
			}
			return true;
		}

		public bool ExistAccountName(string accountName, int parentID)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.ExistAccountName(accountName, parentID);
			}
		}

		public bool ExistAccountNumber(string accountNumber)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.ExistAccountNumber(accountNumber);
			}
		}

		public int GetParentLevel(object parentID)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetParentLevel(parentID);
			}
		}

		public DataSet GetSystemAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetSystemAccounts();
			}
		}

		public DataSet GetAccountsComboList()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountsComboList();
			}
		}

		public DataSet GetAccountsListHierarchy(bool includeInactive)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountsListHierarchy(includeInactive);
			}
		}

		public string GetNextAccountNumber(string groupID)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetNextAccountNumber(groupID);
			}
		}

		public DataSet GetAccountListReport()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountListReport();
			}
		}

		public DataSet GetFavoriteAccounts()
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetFavoriteAccounts();
			}
		}

		public decimal GetAccountBalance(string accountID, bool includeOD)
		{
			using (CompanyAccounts companyAccounts = new CompanyAccounts(config))
			{
				return companyAccounts.GetAccountBalance(accountID, includeOD);
			}
		}
	}
}
