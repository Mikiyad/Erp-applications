using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Text;

namespace Micromind.Facade
{
	public sealed class BankSystem : MarshalByRefObject, IBankSystem, IDisposable
	{
		private Config config;

		public BankSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBank(string bankName, string contactName, string contactTitle, string address, string city, string postalCode, string country, string phone, string fax)
		{
			BankData bankData = new BankData();
			DataTable dataTable = bankData.Tables["Bank"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["BankName"] = bankName.Trim();
			dataRow["ContactName"] = contactName;
			dataRow["ContactTitle"] = contactTitle;
			dataRow["Address"] = address;
			dataRow["City"] = city;
			dataRow["PostalCode"] = postalCode;
			dataRow["Country"] = country;
			dataRow["Phone"] = phone;
			dataRow["Fax"] = fax;
			dataTable.Rows.Add(dataRow);
			return CreateBank(bankData);
		}

		public bool CreateBank(string bankName)
		{
			BankData bankData = new BankData();
			DataTable dataTable = bankData.Tables["Bank"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["BankName"] = bankName.Trim();
			dataTable.Rows.Add(dataRow);
			return CreateBank(bankData);
		}

		public bool CreateBank(BankData bankData)
		{
			if (new Banks(config).InsertBank(bankData))
			{
				return true;
			}
			return false;
		}

		public bool UpdateBank(BankData bankData)
		{
			return UpdateBank(bankData, checkConcurrency: true);
		}

		public bool UpdateBank(BankData bankData, bool checkConcurrency)
		{
			return new Banks(config).UpdateBank(bankData);
		}

		public BankData GetBanks()
		{
			using (Banks banks = new Banks(config))
			{
				return banks.GetBanks();
			}
		}

		public bool DeleteBank(string bankID)
		{
			using (Banks banks = new Banks(config))
			{
				return banks.DeleteBank(bankID);
			}
		}

		public DataSet GetBanksByFields(params string[] columns)
		{
			using (Banks banks = new Banks(config))
			{
				return banks.GetBanksByFields(columns);
			}
		}

		public DataSet GetBanksByFields(string[] banksID, params string[] columns)
		{
			using (Banks banks = new Banks(config))
			{
				return banks.GetBanksByFields(banksID, columns);
			}
		}

		public DataSet GetBanksByFields(string[] banksID, bool isInactive, params string[] columns)
		{
			using (Banks banks = new Banks(config))
			{
				return banks.GetBanksByFields(banksID, isInactive, columns);
			}
		}

		public BankData GetBankByID(string bankID)
		{
			using (Banks banks = new Banks(config))
			{
				return banks.GetBankByID(bankID);
			}
		}

		public bool ExistBank(string bankName)
		{
			using (Banks banks = new Banks(config))
			{
				return banks.ExistBank(bankName);
			}
		}

		public bool CreateUpdateBankBatch(DataSet listData, bool checkConcurrency)
		{
			ApplicationAssert.CheckCondition(listData != null, "listData Parameter cannot be null.", 0);
			ApplicationAssert.CheckCondition(listData.Tables.Contains("Bank"), "Bank Data must exist.", 0);
			Banks banks = new Banks(config);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				foreach (DataRow row in listData.Tables["Bank"].Rows)
				{
					bool flag = false;
					string text = "-1";
					string text2 = string.Empty;
					if (listData.Tables["Bank"].Columns.Contains("BankName"))
					{
						text2 = row["BankName"].ToString();
					}
					if (!(text2 == string.Empty))
					{
						BankData bankData = new BankData();
						DataRow dataRow2 = bankData.BankTable.NewRow();
						bool flag2 = true;
						bool flag3 = true;
						foreach (DataColumn column in listData.Tables["Bank"].Columns)
						{
							try
							{
								if (bankData.BankTable.Columns.Contains(column.ColumnName))
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
						bankData.BankTable.Rows.Add(dataRow2);
						if (banks.ExistBank(text2))
						{
							flag = true;
							text = banks.GetBankIDByName(text2);
							bankData.BankTable.AcceptChanges();
							dataRow2["BankID"] = text;
						}
						try
						{
							if (!flag)
							{
								if (flag3)
								{
									CreateBank(bankData);
								}
							}
							else if (flag2)
							{
								UpdateBank(bankData, checkConcurrency);
							}
						}
						catch (Exception ex)
						{
							stringBuilder.Append(ex.Message).Append("\n");
						}
						bankData.Dispose();
						bankData = null;
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				banks.Dispose();
				banks = null;
				if (stringBuilder.Length > 0)
				{
					ApplicationLog.WriteImportLogError(stringBuilder);
				}
				stringBuilder = null;
			}
			return true;
		}

		public DataSet GetBankComboList()
		{
			using (Banks banks = new Banks(config))
			{
				return banks.GetBankComboList();
			}
		}

		public DataSet GetBankList()
		{
			using (Banks banks = new Banks(config))
			{
				return banks.GetBankList();
			}
		}
	}
}
