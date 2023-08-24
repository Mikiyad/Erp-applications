using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBankFacilityPaymentSystem
	{
		bool UpdateBankFacilityPayment(BankFacilityPaymentData paymentData);

		bool UpdateBankFacilityPayment(BankFacilityPaymentData paymentData, string closingPasword);

		bool CreateBankFacilityPayment(BankFacilityPaymentData paymentData);

		bool CreateBankFacilityPayment(BankFacilityPaymentData paymentData, string closingPasword);

		BankFacilityPaymentData GetBankFacilityPaymentByID(string sysDocID, string voucherID);

		bool VoidBankFacilityPayment(string sysDocID, string voucherID, bool isVoid);

		bool VoidBankFacilityPayment(string sysDocID, string voucherID, bool isVoid, BankFacilityTypes facilityType);

		bool DeleteBankFacilityPayment(string sysDocID, string voucherID);

		bool DeleteBankFacilityPayment(string sysDocID, string voucherID, BankFacilityTypes facilityType);

		DataSet GetBankFacilityPaymentToPrint(string sysDocID, string[] voucherID);

		DataSet GetBankFacilityPaymentToPrint(string sysDocID, string voucherID);

		DataSet GetOpenPayments(BankFacilityTypes type);

		DataSet GetList(DateTime from, DateTime to, BankFacilityTypes facilityType, bool showVoid);
	}
}
