using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataCaches.Accounts;
using System.Data;

namespace Micromind.ClientUI.Libraries
{
	public class DataHelper
	{
		public class Accounts
		{
			public static CompanyAccountTypes GetAccountType(int accountID)
			{
				DataSet allAccounts = AllAccounts.GetAllAccounts(isReferesh: false);
				string filterExpression = "AccountID=" + accountID.ToString();
				if (allAccounts != null)
				{
					DataRow[] array = allAccounts.Tables["Account"].Select(filterExpression);
					if (array.Length != 0)
					{
						return (CompanyAccountTypes)byte.Parse(array[0]["TypeID"].ToString());
					}
					allAccounts = AllAccounts.GetAllAccounts(isReferesh: true);
					array = allAccounts.Tables["Account"].Select(filterExpression);
					if (array.Length != 0)
					{
						return (CompanyAccountTypes)byte.Parse(array[0]["TypeID"].ToString());
					}
					return CompanyAccountTypes.None;
				}
				return CompanyAccountTypes.None;
			}
		}

		private static void Copy(DataTable table1, DataTable table2)
		{
			table1.Rows.Clear();
			table1.Columns.Clear();
			foreach (DataColumn column in table2.Columns)
			{
				table1.Columns.Add(column.ColumnName);
			}
			foreach (DataRow row in table2.Rows)
			{
				DataRow dataRow2 = table1.NewRow();
				foreach (DataColumn column2 in table2.Columns)
				{
					dataRow2[column2.ColumnName] = row[column2.ColumnName];
				}
				table1.Rows.Add(dataRow2);
			}
		}

		private static DataSet Arrange(DataSet dataSet)
		{
			int num = -1;
			for (int i = 0; i < dataSet.Tables.Count; i = checked(i + 1))
			{
				if (dataSet.Tables[i].TableName == "Data Settings")
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				DataTable dataTable = dataSet.Tables[0].Copy();
				Copy(dataSet.Tables[0], dataSet.Tables[num].Copy());
				Copy(dataSet.Tables[num], dataTable);
				dataSet.Tables[num].TableName = "tempppppppppppp";
				dataSet.Tables[0].TableName = "Data Settings";
				dataSet.Tables[num].TableName = dataTable.TableName;
			}
			return dataSet;
		}

		private static void AddSettings(DataTable table)
		{
			AddSettings(table, isDocument: false);
		}

		private static void AddSettings(DataTable table, bool isDocument)
		{
			if (!table.Columns.Contains("OverwriteExistingRecord)"))
			{
				table.Columns.Add("OverwriteExistingRecord", typeof(bool));
				table.Columns.Add("CreateNewRecord", typeof(bool));
				if (isDocument)
				{
					foreach (DataRow row in table.Rows)
					{
						row["OverwriteExistingRecord"] = false;
						row["CreateNewRecord"] = true;
					}
				}
				else
				{
					foreach (DataRow row2 in table.Rows)
					{
						row2["OverwriteExistingRecord"] = true;
						row2["CreateNewRecord"] = true;
					}
				}
			}
		}

		private static DataSet AddSettings(DataSet dataSet, bool isDocument)
		{
			DataTable dataTable = dataSet.Tables[0];
			if (dataTable.Columns.Contains("OverwriteExistingRecord)"))
			{
				return dataSet;
			}
			AddSettings(dataTable, isDocument);
			return dataSet;
		}

		private static DataSet AddSettings(DataSet dataSet)
		{
			return AddSettings(dataSet, isDocument: false);
		}

		internal static DataSet GetEmployees()
		{
			return GetEmployees(new int[0]);
		}

		internal static DataSet GetEmployees(int[] ids)
		{
			return null;
		}

		internal static DataSet GetPartners(PartnerType partnerType)
		{
			return GetPartners(new int[0], partnerType);
		}

		internal static DataSet GetPartners(int[] ids, PartnerType partnerType)
		{
			return new DataSet();
		}

		internal static DataSet GetItems(int[] itemsID)
		{
			return null;
		}

		internal static DataSet GetItems()
		{
			return GetItems(new int[0]);
		}

		internal static DataSet GetAccounts()
		{
			return GetAccounts(new int[0]);
		}

		internal static DataSet GetAccounts(int[] ids)
		{
			return AddSettings(Factory.CompanyAccountSystem.GetAccountsByFields(ids, "Account.AccountName", "Account.Alias", "Account.IsInactive", "Account.Note", "Account.AccountID", "Account.TypeID"));
		}

		internal static DataSet GetItemCategories()
		{
			return GetItemCategories(new int[0]);
		}

		internal static DataSet GetItemCategories(int[] ids)
		{
			return null;
		}

		internal static DataSet GetCurrencies()
		{
			return GetCurrencies(new int[0]);
		}

		internal static DataSet GetCurrencies(int[] ids)
		{
			return null;
		}

		internal static DataSet GetUnits()
		{
			return GetUnits(new int[0]);
		}

		internal static DataSet GetUnits(int[] ids)
		{
			return null;
		}

		internal static DataSet GetManufacturers()
		{
			return GetManufacturers(new int[0]);
		}

		internal static DataSet GetManufacturers(int[] ids)
		{
			return null;
		}

		internal static DataSet GetBrands()
		{
			return GetBrands(new int[0]);
		}

		internal static DataSet GetBrands(int[] ids)
		{
			return null;
		}

		internal static DataSet GetModels()
		{
			return GetModels(new int[0]);
		}

		internal static DataSet GetModels(int[] ids)
		{
			return null;
		}

		internal static DataSet GetDepartments()
		{
			return GetDepartments(new int[0]);
		}

		internal static DataSet GetDepartments(int[] ids)
		{
			return null;
		}

		internal static CustomerInvoiceData[] GetCustomerInvoices()
		{
			return GetCustomerInvoices(new int[0]);
		}

		internal static CustomerInvoiceData[] GetCustomerInvoices(int[] ids)
		{
			return null;
		}

		internal static CustomerOrderData[] GetCustomerOrders()
		{
			return GetCustomerOrders(new int[0]);
		}

		internal static CustomerOrderData[] GetCustomerOrders(int[] ids)
		{
			return null;
		}

		internal static TransactionData[] GetExpenses()
		{
			return GetExpenses(new int[0]);
		}

		internal static TransactionData[] GetExpenses(int[] ids)
		{
			return null;
		}

		internal static PurchaseInvoiceData[] GetPurchaseInvoices()
		{
			return GetPurchaseInvoices(new int[0]);
		}

		internal static PurchaseInvoiceData[] GetPurchaseInvoices(int[] ids)
		{
			return null;
		}

		internal static DataSet[] GetGLs()
		{
			return GetGLs(new int[0]);
		}

		internal static DataSet[] GetGLs(int[] ids)
		{
			return null;
		}
	}
}
