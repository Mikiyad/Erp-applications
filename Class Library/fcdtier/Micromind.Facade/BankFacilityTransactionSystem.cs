using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BankFacilityTransactionSystem : MarshalByRefObject, IBankFacilityTransactionSystem, IDisposable
	{
		private Config config;

		public BankFacilityTransactionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool UpdateBankFacilityTransaction(BankFacilityTransactionData transactionData, string closingPassword)
		{
			return new BankFacilityTransactions(config).InsertUpdateTransaction(transactionData, isUpdate: true);
		}

		public bool UpdateTREntryTransaction(BankFacilityTransactionData transactionData)
		{
			return new BankFacilityTransactions(config).UpdateTREntryTransaction(transactionData);
		}

		public bool UpdateBankFacilityTransaction(BankFacilityTransactionData transactionData)
		{
			return UpdateBankFacilityTransaction(transactionData, string.Empty);
		}

		public bool CreateBankFacilityTransaction(BankFacilityTransactionData transactionData, string closingPassword)
		{
			return new BankFacilityTransactions(config).InsertUpdateTransaction(transactionData, isUpdate: false);
		}

		public bool CreateBankFacilityTransaction(BankFacilityTransactionData transactionData)
		{
			return new BankFacilityTransactions(config).InsertUpdateTransaction(transactionData, isUpdate: false);
		}

		public BankFacilityTransactionData GetBankFacilityTransactionByID(string sysDocID, string voucherID)
		{
			using (BankFacilityTransactions bankFacilityTransactions = new BankFacilityTransactions(config))
			{
				return bankFacilityTransactions.GetTransactionByID(sysDocID, voucherID);
			}
		}

		public bool DeleteBankFacilityTransaction(string sysDocID, string voucherID)
		{
			using (BankFacilityTransactions bankFacilityTransactions = new BankFacilityTransactions(config))
			{
				return bankFacilityTransactions.DeleteTransaction(sysDocID, voucherID);
			}
		}

		public bool VoidBankFacilityTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			using (BankFacilityTransactions bankFacilityTransactions = new BankFacilityTransactions(config))
			{
				return bankFacilityTransactions.VoidTransaction(sysDocID, voucherID, isVoid);
			}
		}

		public bool DeleteBankFacilityTransaction(string transactionID)
		{
			return DeleteBankFacilityTransaction(transactionID, string.Empty);
		}

		public DataSet GetBankFacilityTransactionToPrint(string sysDocID, string[] voucherID)
		{
			return new BankFacilityTransactions(config).GetTransactionToPrint(sysDocID, voucherID);
		}

		public DataSet GetBankFacilityTransactionToPrint(string sysDocID, string voucherID)
		{
			return new BankFacilityTransactions(config).GetTransactionToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetOpenTransactions(BankFacilityTypes type)
		{
			return new BankFacilityTransactions(config).GetOpenTransactions(type);
		}

		public DataSet GetList(DateTime from, DateTime to, BankFacilityTypes facilityType, bool showVoid)
		{
			return new BankFacilityTransactions(config).GetList(from, to, facilityType, showVoid);
		}

		public DataSet GetList(BankFacilityTypes facilityType, bool showVoid)
		{
			return new BankFacilityTransactions(config).GetList(facilityType, showVoid);
		}
	}
}
