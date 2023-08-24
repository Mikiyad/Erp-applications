using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BankFacilityPaymentSystem : MarshalByRefObject, IBankFacilityPaymentSystem, IDisposable
	{
		private Config config;

		public BankFacilityPaymentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool UpdateBankFacilityPayment(BankFacilityPaymentData paymentData, string closingPassword)
		{
			return new BankFacilityPayments(config).InsertUpdatePayment(paymentData, isUpdate: true);
		}

		public bool UpdateBankFacilityPayment(BankFacilityPaymentData paymentData)
		{
			return UpdateBankFacilityPayment(paymentData, string.Empty);
		}

		public bool CreateBankFacilityPayment(BankFacilityPaymentData paymentData, string closingPassword)
		{
			return new BankFacilityPayments(config).InsertUpdatePayment(paymentData, isUpdate: false);
		}

		public bool CreateBankFacilityPayment(BankFacilityPaymentData paymentData)
		{
			return new BankFacilityPayments(config).InsertUpdatePayment(paymentData, isUpdate: false);
		}

		public BankFacilityPaymentData GetBankFacilityPaymentByID(string sysDocID, string voucherID)
		{
			using (BankFacilityPayments bankFacilityPayments = new BankFacilityPayments(config))
			{
				return bankFacilityPayments.GetPaymentByID(sysDocID, voucherID);
			}
		}

		public bool DeleteBankFacilityPayment(string sysDocID, string voucherID)
		{
			using (BankFacilityPayments bankFacilityPayments = new BankFacilityPayments(config))
			{
				return bankFacilityPayments.DeletePayment(sysDocID, voucherID);
			}
		}

		public bool DeleteBankFacilityPayment(string sysDocID, string voucherID, BankFacilityTypes type)
		{
			using (BankFacilityPayments bankFacilityPayments = new BankFacilityPayments(config))
			{
				return bankFacilityPayments.DeletePayment(sysDocID, voucherID, type);
			}
		}

		public bool VoidBankFacilityPayment(string sysDocID, string voucherID, bool isVoid)
		{
			using (BankFacilityPayments bankFacilityPayments = new BankFacilityPayments(config))
			{
				return bankFacilityPayments.VoidPayment(sysDocID, voucherID, isVoid);
			}
		}

		public bool VoidBankFacilityPayment(string sysDocID, string voucherID, bool isVoid, BankFacilityTypes ftype)
		{
			using (BankFacilityPayments bankFacilityPayments = new BankFacilityPayments(config))
			{
				return bankFacilityPayments.VoidPayment(sysDocID, voucherID, isVoid, ftype);
			}
		}

		public bool DeleteBankFacilityPayment(string paymentID)
		{
			return DeleteBankFacilityPayment(paymentID, string.Empty);
		}

		public DataSet GetBankFacilityPaymentToPrint(string sysDocID, string[] voucherID)
		{
			return new BankFacilityPayments(config).GetPaymentToPrint(sysDocID, voucherID);
		}

		public DataSet GetBankFacilityPaymentToPrint(string sysDocID, string voucherID)
		{
			return new BankFacilityPayments(config).GetPaymentToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetOpenPayments(BankFacilityTypes type)
		{
			return new BankFacilityPayments(config).GetOpenPayments(type);
		}

		public DataSet GetList(DateTime from, DateTime to, BankFacilityTypes facilityType, bool showVoid)
		{
			return new BankFacilityPayments(config).GetList(from, to, facilityType, showVoid);
		}
	}
}
