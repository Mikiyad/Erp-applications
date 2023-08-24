using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TransactionSystem : MarshalByRefObject, ITransactionSystem, IDisposable
	{
		private Config config;

		public TransactionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool UpdateTransaction(TransactionData transactionData, string closingPassword)
		{
			return new Transactions(config).InsertUpdateTransaction(transactionData, isUpdate: true);
		}

		public bool UpdateTransaction(TransactionData transactionData)
		{
			return UpdateTransaction(transactionData, string.Empty);
		}

		public bool CreateTransaction(TransactionData transactionData, string closingPassword)
		{
			return new Transactions(config).InsertUpdateTransaction(transactionData, isUpdate: false);
		}

		public bool CreateTransaction(TransactionData transactionData)
		{
			return new Transactions(config).InsertUpdateTransaction(transactionData, isUpdate: false);
		}

		public bool Deposit(int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID)
		{
			return Deposit(fromAccountID, toAccountID, amount, number, date, description, jobID, string.Empty);
		}

		public bool Deposit(int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = toAccountID;
			dataRow["PartyTwoAccountID"] = fromAccountID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["IsDebit"] = true;
			dataRow["TransactionNumber"] = number;
			if (jobID != -1)
			{
				dataRow["JobID"] = jobID;
			}
			dataRow["IsAccountTransaction"] = true;
			transactionData.TransactionTable.Rows.Add(dataRow);
			return CreateTransaction(transactionData, closingPassword);
		}

		public bool Withdraw(int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID)
		{
			return Withdraw(fromAccountID, toAccountID, amount, number, date, description, jobID, string.Empty);
		}

		public bool Withdraw(int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = fromAccountID;
			dataRow["PartyTwoAccountID"] = toAccountID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["IsDebit"] = false;
			if (jobID != -1)
			{
				dataRow["JobID"] = jobID;
			}
			dataRow["TransactionNumber"] = number;
			dataRow["IsAccountTransaction"] = true;
			transactionData.TransactionTable.Rows.Add(dataRow);
			return CreateTransaction(transactionData, closingPassword);
		}

		public bool UpdateDeposit(int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp)
		{
			return UpdateDeposit(transactionID, fromAccountID, toAccountID, amount, number, date, description, dateTimeStamp, string.Empty);
		}

		public bool UpdateDeposit(int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = toAccountID;
			dataRow["PartyTwoAccountID"] = fromAccountID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["IsDebit"] = true;
			dataRow["TransactionNumber"] = number;
			dataRow["IsAccountTransaction"] = true;
			dataRow["DateTimeStamp"] = dateTimeStamp;
			transactionData.TransactionTable.Rows.Add(dataRow);
			transactionData.AcceptChanges();
			dataRow["TransactionID"] = transactionID;
			return UpdateTransaction(transactionData, closingPassword);
		}

		public bool UpdateDeposit(TransactionData transactionData)
		{
			return UpdateDeposit(transactionData, string.Empty);
		}

		public bool UpdateDeposit(TransactionData transactionData, string closingPassword)
		{
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			dataRow["IsDebit"] = true;
			dataRow["IsAccountTransaction"] = true;
			transactionData.AcceptChanges();
			int num = int.Parse(transactionData.TransactionTable.Rows[0]["TransactionID"].ToString());
			dataRow["TransactionID"] = num;
			return UpdateTransaction(transactionData, closingPassword);
		}

		public bool UpdateWithdraw(int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp)
		{
			return UpdateWithdraw(transactionID, fromAccountID, toAccountID, amount, number, date, description, dateTimeStamp, string.Empty);
		}

		public bool UpdateWithdraw(int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = fromAccountID;
			dataRow["PartyTwoAccountID"] = toAccountID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["IsDebit"] = false;
			dataRow["TransactionNumber"] = number;
			dataRow["IsAccountTransaction"] = true;
			dataRow["DateTimeStamp"] = dateTimeStamp;
			transactionData.TransactionTable.Rows.Add(dataRow);
			transactionData.AcceptChanges();
			dataRow["TransactionID"] = transactionID;
			return UpdateTransaction(transactionData, closingPassword);
		}

		public bool UpdateWithdraw(TransactionData transactionData)
		{
			return UpdateWithdraw(transactionData, string.Empty);
		}

		public bool UpdateWithdraw(TransactionData transactionData, string closingPassword)
		{
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			dataRow["IsDebit"] = false;
			dataRow["IsAccountTransaction"] = true;
			transactionData.AcceptChanges();
			int num = int.Parse(transactionData.TransactionTable.Rows[0]["TransactionID"].ToString());
			dataRow["TransactionID"] = num;
			return UpdateTransaction(transactionData, closingPassword);
		}

		public bool DepositPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID)
		{
			return DepositPartnerTransaction(partnerID, partnerType, isPaymentTransaction, fromAccountID, toAccountID, amount, number, date, description, jobID, string.Empty);
		}

		public bool DepositPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			bool flag = true;
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = toAccountID;
			dataRow["PartyTwoAccountID"] = fromAccountID;
			dataRow["PAYEE"] = partnerID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["IsDebit"] = true;
			dataRow["TransactionNumber"] = number;
			dataRow["IsPartnerTransaction"] = true;
			if (jobID != -1)
			{
				dataRow["JobID"] = jobID;
			}
			transactionData.TransactionTable.Rows.Add(dataRow);
			try
			{
				config.StartNewTransaction();
				if (!flag)
				{
					return flag;
				}
				string value = transactionData.TransactionTable.Rows[0]["TransactionID"].ToString();
				if (isPaymentTransaction)
				{
					switch (partnerType)
					{
					case PartnerType.Customer:
					{
						ARJournalData aRJournalData = new ARJournalData();
						DataRow dataRow3 = aRJournalData.AccountReceivableTable.NewRow();
						dataRow3["ARDate"] = date;
						dataRow3["ARAccountID"] = fromAccountID;
						dataRow3["Credit"] = amount;
						dataRow3["TransactionID"] = value;
						dataRow3["ARType"] = ARTypes.CreditNote;
						dataRow3["CustomerID"] = partnerID;
						dataRow3["Description"] = description;
						aRJournalData.AccountReceivableTable.Rows.Add(dataRow3);
						return flag;
					}
					case PartnerType.Vendor:
					{
						APJournalData aPJournalData = new APJournalData();
						DataRow dataRow2 = aPJournalData.AccountPayableTable.NewRow();
						dataRow2["APDate"] = date;
						dataRow2["APAccountID"] = fromAccountID;
						dataRow2["Credit"] = amount;
						dataRow2["TransactionID"] = value;
						dataRow2["APType"] = APTypes.Invoice;
						dataRow2["SupplierID"] = partnerID;
						dataRow2["Description"] = description;
						aPJournalData.AccountPayableTable.Rows.Add(dataRow2);
						return flag;
					}
					default:
						throw new ApplicationException("Partner type must be either customer or vendor.");
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				config.EndTransaction(flag);
			}
		}

		public bool WithdrawPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID)
		{
			return WithdrawPartnerTransaction(partnerID, partnerType, isPaymentTransaction, fromAccountID, toAccountID, amount, number, date, description, jobID, string.Empty);
		}

		public bool WithdrawPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, int jobID, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			bool flag = true;
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = fromAccountID;
			dataRow["PartyTwoAccountID"] = toAccountID;
			dataRow["PAYEE"] = partnerID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["IsDebit"] = false;
			dataRow["TransactionNumber"] = number;
			if (jobID != -1)
			{
				dataRow["JobID"] = jobID;
			}
			dataRow["IsPartnerTransaction"] = true;
			transactionData.TransactionTable.Rows.Add(dataRow);
			try
			{
				config.StartNewTransaction();
				if (!flag)
				{
					return flag;
				}
				string value = transactionData.TransactionTable.Rows[0]["TransactionID"].ToString();
				if (isPaymentTransaction)
				{
					switch (partnerType)
					{
					case PartnerType.Customer:
					{
						ARJournalData aRJournalData = new ARJournalData();
						DataRow dataRow3 = aRJournalData.AccountReceivableTable.NewRow();
						dataRow3["ARDate"] = date;
						dataRow3["ARAccountID"] = toAccountID;
						dataRow3["Debit"] = amount;
						dataRow3["TransactionID"] = value;
						dataRow3["ARType"] = ARTypes.Invoice;
						dataRow3["CustomerID"] = partnerID;
						dataRow3["Description"] = description;
						aRJournalData.AccountReceivableTable.Rows.Add(dataRow3);
						return flag;
					}
					case PartnerType.Vendor:
					{
						APJournalData aPJournalData = new APJournalData();
						DataRow dataRow2 = aPJournalData.AccountPayableTable.NewRow();
						dataRow2["APDate"] = date;
						dataRow2["APAccountID"] = toAccountID;
						dataRow2["Debit"] = amount;
						dataRow2["TransactionID"] = value;
						dataRow2["APType"] = APTypes.CreditNote;
						dataRow2["SupplierID"] = partnerID;
						dataRow2["Description"] = description;
						aPJournalData.AccountPayableTable.Rows.Add(dataRow2);
						return flag;
					}
					default:
						throw new ApplicationException("Partner type must be either customer or vendor.");
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				config.EndTransaction(flag);
			}
		}

		public bool UpdateDepositPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp)
		{
			return UpdateDepositPartnerTransaction(partnerID, partnerType, isPaymentTransaction, transactionID, fromAccountID, toAccountID, amount, number, date, description, dateTimeStamp, string.Empty);
		}

		public bool UpdateDepositPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			bool flag = true;
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = toAccountID;
			dataRow["PartyTwoAccountID"] = fromAccountID;
			dataRow["PAYEE"] = partnerID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["GLType"] = SysDocTypes.Deposit;
			dataRow["IsDebit"] = true;
			dataRow["TransactionNumber"] = number;
			dataRow["IsPartnerTransaction"] = true;
			dataRow["DateTimeStamp"] = dateTimeStamp;
			transactionData.TransactionTable.Rows.Add(dataRow);
			transactionData.AcceptChanges();
			dataRow["TransactionID"] = transactionID;
			try
			{
				config.StartNewTransaction();
				if (!flag)
				{
					return flag;
				}
				if (isPaymentTransaction)
				{
					switch (partnerType)
					{
					case PartnerType.Customer:
					{
						ARJournalData aRJournalData = new ARJournalData();
						DataRow dataRow3 = aRJournalData.AccountReceivableTable.NewRow();
						dataRow3["ARDate"] = date;
						dataRow3["ARAccountID"] = fromAccountID;
						dataRow3["Credit"] = amount;
						dataRow3["TransactionID"] = transactionID;
						dataRow3["ARType"] = ARTypes.CreditNote;
						dataRow3["CustomerID"] = partnerID;
						dataRow3["Description"] = description;
						aRJournalData.AccountReceivableTable.Rows.Add(dataRow3);
						return flag;
					}
					case PartnerType.Vendor:
					{
						APJournalData aPJournalData = new APJournalData();
						DataRow dataRow2 = aPJournalData.AccountPayableTable.NewRow();
						dataRow2["APDate"] = date;
						dataRow2["APAccountID"] = fromAccountID;
						dataRow2["Credit"] = amount;
						dataRow2["TransactionID"] = transactionID;
						dataRow2["APType"] = APTypes.Invoice;
						dataRow2["SupplierID"] = partnerID;
						dataRow2["Description"] = description;
						aPJournalData.AccountPayableTable.Rows.Add(dataRow2);
						return flag;
					}
					default:
						throw new ApplicationException("Partner type must be either customer or vendor.");
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				config.EndTransaction(flag);
			}
		}

		public bool UpdateDepositPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, TransactionData transactionData)
		{
			return UpdateDepositPartnerTransaction(partnerID, partnerType, isPaymentTransaction, transactionData, string.Empty);
		}

		public bool UpdateDepositPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, TransactionData transactionData, string closingPassword)
		{
			bool flag = true;
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			dataRow["PAYEE"] = partnerID;
			dataRow["IsPartnerTransaction"] = true;
			transactionData.AcceptChanges();
			int num = int.Parse(transactionData.TransactionTable.Rows[0]["TransactionID"].ToString());
			dataRow["TransactionID"] = num;
			try
			{
				config.StartNewTransaction();
				if (!flag)
				{
					return flag;
				}
				if (isPaymentTransaction)
				{
					switch (partnerType)
					{
					case PartnerType.Customer:
					{
						ARJournalData aRJournalData = new ARJournalData();
						DataRow dataRow3 = aRJournalData.AccountReceivableTable.NewRow();
						dataRow3["ARDate"] = dataRow["TransactionDate"];
						dataRow3["ARAccountID"] = dataRow["PartyTwoAccountID"];
						dataRow3["Credit"] = dataRow["Amount"];
						dataRow3["TransactionID"] = num;
						dataRow3["ARType"] = ARTypes.CreditNote;
						dataRow3["CustomerID"] = partnerID;
						dataRow3["Description"] = dataRow["Description"];
						aRJournalData.AccountReceivableTable.Rows.Add(dataRow3);
						return flag;
					}
					case PartnerType.Vendor:
					{
						APJournalData aPJournalData = new APJournalData();
						DataRow dataRow2 = aPJournalData.AccountPayableTable.NewRow();
						dataRow2["APDate"] = dataRow["TransactionDate"];
						dataRow2["APAccountID"] = dataRow["PartyTwoAccountID"];
						dataRow2["Credit"] = dataRow["Amount"];
						dataRow2["TransactionID"] = num;
						dataRow2["APType"] = APTypes.Invoice;
						dataRow2["SupplierID"] = partnerID;
						dataRow2["Description"] = dataRow["Description"];
						aPJournalData.AccountPayableTable.Rows.Add(dataRow2);
						return flag;
					}
					default:
						throw new ApplicationException("Partner type must be either customer or vendor.");
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				config.EndTransaction(flag);
			}
		}

		public bool UpdateWithdrawPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp)
		{
			return UpdateWithdrawPartnerTransaction(partnerID, partnerType, isPaymentTransaction, transactionID, fromAccountID, toAccountID, amount, number, date, description, dateTimeStamp, string.Empty);
		}

		public bool UpdateWithdrawPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, int transactionID, int fromAccountID, int toAccountID, decimal amount, string number, DateTime date, string description, DateTime dateTimeStamp, string closingPassword)
		{
			if (amount < 0m)
			{
				throw new Exception("Transaction amount must be greater than zero.");
			}
			bool flag = true;
			TransactionData transactionData = new TransactionData();
			DataRow dataRow = transactionData.TransactionTable.NewRow();
			dataRow["Amount"] = amount;
			dataRow["PartyOneAccountID"] = fromAccountID;
			dataRow["PartyTwoAccountID"] = toAccountID;
			dataRow["PAYEE"] = partnerID;
			dataRow["TransactionDate"] = date;
			dataRow["Description"] = description;
			dataRow["IsDebit"] = false;
			dataRow["TransactionNumber"] = number;
			dataRow["IsPartnerTransaction"] = true;
			dataRow["DateTimeStamp"] = dateTimeStamp;
			transactionData.TransactionTable.Rows.Add(dataRow);
			transactionData.AcceptChanges();
			dataRow["TransactionID"] = transactionID;
			try
			{
				config.StartNewTransaction();
				if (!flag)
				{
					return flag;
				}
				if (isPaymentTransaction)
				{
					switch (partnerType)
					{
					case PartnerType.Customer:
					{
						ARJournalData aRJournalData = new ARJournalData();
						DataRow dataRow3 = aRJournalData.AccountReceivableTable.NewRow();
						dataRow3["ARDate"] = date;
						dataRow3["ARAccountID"] = toAccountID;
						dataRow3["Debit"] = dataRow["Amount"];
						dataRow3["TransactionID"] = transactionID;
						dataRow3["ARType"] = ARTypes.Invoice;
						dataRow3["CustomerID"] = partnerID;
						dataRow3["Description"] = description;
						aRJournalData.AccountReceivableTable.Rows.Add(dataRow3);
						return flag;
					}
					case PartnerType.Vendor:
					{
						APJournalData aPJournalData = new APJournalData();
						DataRow dataRow2 = aPJournalData.AccountPayableTable.NewRow();
						dataRow2["APDate"] = date;
						dataRow2["APAccountID"] = toAccountID;
						dataRow2["Credit"] = amount;
						dataRow2["TransactionID"] = transactionID;
						dataRow2["APType"] = APTypes.CreditNote;
						dataRow2["SupplierID"] = partnerID;
						dataRow2["Description"] = description;
						aPJournalData.AccountPayableTable.Rows.Add(dataRow2);
						return flag;
					}
					default:
						throw new ApplicationException("Partner type must be either customer or vendor.");
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				config.EndTransaction(flag);
			}
		}

		public bool UpdateWithdrawPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, TransactionData transactionData)
		{
			return UpdateWithdrawPartnerTransaction(partnerID, partnerType, isPaymentTransaction, transactionData, string.Empty);
		}

		public bool UpdateWithdrawPartnerTransaction(int partnerID, PartnerType partnerType, bool isPaymentTransaction, TransactionData transactionData, string closingPassword)
		{
			bool flag = true;
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			dataRow["PAYEE"] = partnerID;
			dataRow["IsPartnerTransaction"] = true;
			transactionData.AcceptChanges();
			int num = int.Parse(transactionData.TransactionTable.Rows[0]["TransactionID"].ToString());
			dataRow["TransactionID"] = num;
			try
			{
				config.StartNewTransaction();
				if (!flag)
				{
					return flag;
				}
				if (isPaymentTransaction)
				{
					switch (partnerType)
					{
					case PartnerType.Customer:
					{
						ARJournalData aRJournalData = new ARJournalData();
						DataRow dataRow3 = aRJournalData.AccountReceivableTable.NewRow();
						dataRow3["ARDate"] = dataRow["TransactionDate"];
						dataRow3["ARAccountID"] = dataRow["PartyTwoAccountID"];
						dataRow3["Debit"] = dataRow["Amount"];
						dataRow3["TransactionID"] = num;
						dataRow3["ARType"] = ARTypes.Invoice;
						dataRow3["CustomerID"] = partnerID;
						dataRow3["Description"] = dataRow["Description"];
						aRJournalData.AccountReceivableTable.Rows.Add(dataRow3);
						return flag;
					}
					case PartnerType.Vendor:
					{
						APJournalData aPJournalData = new APJournalData();
						DataRow dataRow2 = aPJournalData.AccountPayableTable.NewRow();
						dataRow2["APDate"] = dataRow["TransactionDate"];
						dataRow2["APAccountID"] = dataRow["PartyTwoAccountID"];
						dataRow2["Credit"] = dataRow["Amount"];
						dataRow2["TransactionID"] = num;
						dataRow2["APType"] = APTypes.CreditNote;
						dataRow2["SupplierID"] = partnerID;
						dataRow2["Description"] = dataRow["Description"];
						aPJournalData.AccountPayableTable.Rows.Add(dataRow2);
						return flag;
					}
					default:
						throw new ApplicationException("Partner type must be either customer or vendor.");
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				config.EndTransaction(flag);
			}
		}

		public TransactionData GetTransactionByID(int transactionID)
		{
			return null;
		}

		public TransactionData GetTransactionByID(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.GetTransactionByID(sysDocID, voucherID);
			}
		}

		public TransactionData GetTransactionByCompanyAccountID(int accountID)
		{
			return null;
		}

		public TransactionData GetTransactionByGLAccountID(int glAccountID)
		{
			return null;
		}

		public TransactionData GetTransactionByDate(DateTime transactionDate)
		{
			return null;
		}

		public TransactionData GetTransactionByGLID(int glID)
		{
			return null;
		}

		public int GetAccountIDByTransactionID(TransactionTypes transactionType, int transactionID)
		{
			return -1;
		}

		public DataSet GetPaymentTransactions(PartnerType partnerType)
		{
			return null;
		}

		public DataSet GetPaymentTransactions(PartnerType partnerType, DateTime from, DateTime to)
		{
			return null;
		}

		public DataSet GetPaymentTransactions(PartnerType partnerType, int partnerID, DateTime from, DateTime to)
		{
			return null;
		}

		public DataSet GetPaymentTransactions(PartnerType partnerType, int[] partnersID, DateTime from, DateTime to)
		{
			return null;
		}

		public DataSet GetPaymentTransactions(PartnerType partnerType, int partnerID)
		{
			return null;
		}

		public bool DeleteCustomerPaymentTransaction(string transactionID, string closingPassword)
		{
			return false;
		}

		public bool DeleteCustomerPaymentTransaction(string transactionID)
		{
			return DeleteCustomerPaymentTransaction(transactionID, string.Empty);
		}

		public bool DeleteTransaction(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.DeleteTransaction(sysDocID, voucherID);
			}
		}

		public bool DeleteChequeDepositTransaction(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.DeleteChequeDepositTransaction(sysDocID, voucherID);
			}
		}

		public bool VoidTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.VoidTransaction(sysDocID, voucherID, isVoid);
			}
		}

		public bool VoidChequeDepositTransaction(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.VoidChequeDepositTransaction(sysDocID, voucherID);
			}
		}

		public bool DeleteTransaction(string transactionID)
		{
			return DeleteTransaction(transactionID, string.Empty);
		}

		public bool DeleteVendorPaymentTransaction(string transactionID, string closingPassword)
		{
			return false;
		}

		public bool DeleteVendorPaymentTransaction(string transactionID)
		{
			return false;
		}

		public int GetPartnerIDByTransactionID(int transactionID)
		{
			using (new Transactions(config))
			{
				return -1;
			}
		}

		public string GetAutoTransactionNumber()
		{
			using (new Transactions(config))
			{
				return "";
			}
		}

		public string GetAutoTransactionNumber(SysDocTypes glType)
		{
			using (new Transactions(config))
			{
				return "";
			}
		}

		public TransactionData GetTransactionByNumber(string transactionNumber)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public DataSet GetItemByCriteria(string number, string[] names, DateTime from, DateTime to, decimal amount, AmountCriteria amountCriteria, string reference, string description)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public DataSet GetUndepositedTransactions(PartnerType partnerType)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public DataSet GetUndepositedTransactions(PartnerType partnerType, DateTime from, DateTime to)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public DataSet GetUndepositedTransactions(PartnerType partnerType, int partnerID)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public DataSet GetUndepositedTransactions(PartnerType partnerType, int partnerID, DateTime from, DateTime to)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public DataSet GetUndepositedTransactions(PartnerType partnerType, int[] partnersID, DateTime from, DateTime to)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public bool PostTransactions(params int[] transactionIDList)
		{
			using (new Transactions(config))
			{
				return false;
			}
		}

		public TransactionData GetExpenseByTransactionID(int transactionID)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public TransactionData[] GetExpensesByTransactionID(int[] transactionsID)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public TransactionData GetPayrollByTransactionID(int transactionID)
		{
			using (new Transactions(config))
			{
				return null;
			}
		}

		public bool InsertUpdateFundTransferTransaction(TransactionData transactionData, bool isUpdate)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateFundTransferTransaction(transactionData, isUpdate);
			}
		}

		public bool DepositCheques(TransactionData transactionData, int[] chequeIDs, bool isUpdate)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.DepositCheques(transactionData, chequeIDs, isUpdate);
			}
		}

		public TransactionData GetChequeDepositTransactionByID(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.GetChequeDepositTransactionByID(sysDocID, voucherID);
			}
		}

		public bool InsertUpdateExpenseTransaction(TransactionData transactionData, bool isUpdate)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateExpenseTransaction(transactionData, isUpdate);
			}
		}

		public bool InsertUpdateDebitCreditNoteTransaction(TransactionData transactionData, bool isUpdate)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateDebitCreditNoteTransaction(transactionData, isUpdate);
			}
		}

		public bool InsertUpdateReturnedCheque(TransactionData transactionData)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateReturnedCheque(transactionData);
			}
		}

		public bool InsertUpdateCancelledReceivedCheque(TransactionData transactionData)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateCancelledReceivedCheque(transactionData);
			}
		}

		public bool InsertUpdateChequeChangeStatus(TransactionData transactionData)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateChequeChangeStatus(transactionData);
			}
		}

		public bool ClearIssuedCheques(TransactionData transactionData, int[] chequeIDs)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.ClearIssuedCheques(transactionData, chequeIDs);
			}
		}

		public bool CancellIssuedCheque(TransactionData transactionData, bool isUpdate)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.CancellIssuedCheque(transactionData, isUpdate);
			}
		}

		public bool ReturnIssuedCheque(TransactionData transactionData, bool isUpdate)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateIssuedReturnedCheque(transactionData, isUpdate);
			}
		}

		public DataSet GetTransactionToPrint(string sysDocID, string[] voucherID)
		{
			return new Transactions(config).GetTransactionToPrint(sysDocID, voucherID);
		}

		public DataSet GetTransactionToPrint(string sysDocID, string voucherID)
		{
			return new Transactions(config).GetTransactionToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetExpenseList(DateTime from, DateTime to, bool showVoid, string SysDocID)
		{
			return new Transactions(config).GetExpenseList(from, to, showVoid, SysDocID);
		}

		public DataSet GetDebitNoteList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetDebitNoteList(from, to, showVoid);
		}

		public DataSet GetCreditNoteList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetCreditNoteList(from, to, showVoid);
		}

		public DataSet GetReceiptVoucherList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetReceiptVoucherList(from, to, showVoid);
		}

		public DataSet GetReceiptVoucherMultipleList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetReceiptVoucherMultipleList(from, to, showVoid);
		}

		public DataSet GetReturnVoucherList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetReturnVoucherList(from, to, showVoid);
		}

		public DataSet GetReceivedChequeReturnList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetReceivedChequeReturnList(from, to, showVoid);
		}

		public DataSet GetChequeDepositList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetChequeDepositList(from, to, showVoid);
		}

		public DataSet GetPaymentVoucherList(DateTime from, DateTime to, bool showVoid, TransactionPaymentType paymentType)
		{
			return new Transactions(config).GetPaymentVoucherList(from, to, showVoid, paymentType);
		}

		public DataSet GetChequeReceiptList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetChequeReceiptList(from, to, showVoid);
		}

		public DataSet GetChequePaymentList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetChequePaymentList(from, to, showVoid);
		}

		public DataSet GetTransferList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetTransferList(from, to, showVoid);
		}

		public bool DeleteIssuedChequeClearanceTransaction(string sysDocID, string voucherID)
		{
			return new Transactions(config).DeleteIssuedChequeClearanceTransaction(sysDocID, voucherID);
		}

		public TransactionData GetIssuedChequeClearanceTransactionByID(string sysDocID, string voucherID)
		{
			return new Transactions(config).GetIssuedChequeClearanceTransactionByID(sysDocID, voucherID);
		}

		public bool VoidIssuedChequeClearanceTransaction(string sysDocID, string voucherID)
		{
			return new Transactions(config).VoidIssuedChequeClearanceTransaction(sysDocID, voucherID);
		}

		public DataSet GetChequeDepositTransactionToPrint(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.GetTransactionByID(sysDocID, voucherID);
			}
		}

		public DataSet GetChequeSentToBankTransactionToPrint(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.GetTransactionByID(sysDocID, voucherID);
			}
		}

		public DataSet GetChequeDiscountTransactionToPrint(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.GetChequeDiscountTransactionToPrint(sysDocID, voucherID);
			}
		}

		public DataSet GetIssuedChequeClearanceTransactionToPrint(string sysDocID, string voucherID)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.GetIssuedChequeClearanceTransactionToPrint(sysDocID, voucherID);
			}
		}

		public DataSet GetChequeMaturityReport(DateTime from, DateTime to, string fromBank, string toBank)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.GetChequeMaturityReport(from, to, fromBank, toBank);
			}
		}

		public bool InsertUpdateChequeReceiptMultipleTransaction(TransactionData transactionData, bool isUpdate)
		{
			using (Transactions transactions = new Transactions(config))
			{
				return transactions.InsertUpdateChequeReceiptMultipleTransaction(transactionData, isUpdate);
			}
		}

		public DataSet GetChequeReceiptMultipleList(DateTime from, DateTime to, bool showVoid)
		{
			return new Transactions(config).GetChequeReceiptMultipleList(from, to, showVoid);
		}
	}
}
