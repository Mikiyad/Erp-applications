using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IReceivedChequeSystem
	{
		DataSet GetChequesToDepositList(DateTime chequeDate);

		DataSet GetChequesToDepositList(DateTime chequeDate, string bankAccountID);

		DataSet GetChequesToDepositList(DateTime chequeDate, string bankAccountID, string locationID);

		DataSet GetChequesToSendToBankList(DateTime chequeDate);

		DataSet GetChequesToSendToBankList(DateTime chequeDate, string bankAccountID);

		DataSet GetChequesToDiscountList(DateTime chequeDate);

		DataSet GetChequesToDiscountList(DateTime chequeDate, string bankAccountID);

		DataSet GetReceivedChequeComboList();

		DataSet GetChequeByID(string chequeID);

		DataSet GetReceivedChequeToCancelList();

		DataSet GetReceivedChequeToReturnList();

		DataSet GetChequesToClearList();

		DataSet GetChangeChequeStatusList();

		bool UpdateChequeStatus(string[] chequeIDs, ReceivedChequeStatus status);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetReceivedChequeAsOfDate(DateTime chkFrom, DateTime chkTo, DateTime from, DateTime to, string fromBank, string toBank, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocation, string toLocation, bool cleared, bool bounced, bool cancelled, bool discounted, bool senttobank, string strGroupBy, string customerIDs);

		DataSet GetChequeMaturityReport(DateTime from, DateTime to, DateTime chequedatefrom, DateTime chequedateto, string fromBank, string toBank, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool cleared, bool bounced, bool cancelled, string customerIDs);

		DataSet GetForSettleChequesToSendToBankList(DateTime chequeDate, string[] Cheques);
	}
}
