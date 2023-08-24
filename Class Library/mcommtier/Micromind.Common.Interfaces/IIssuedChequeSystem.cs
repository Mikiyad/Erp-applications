using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IIssuedChequeSystem
	{
		int ValidateBlankCheque(string chequebookID, string chequeNumber, string sysDocID, string voucherID);

		DataSet GetChequesToClearList(DateTime chequeDate);

		DataSet GetIssuedChequeToCancelList();

		DataSet GetChequeByID(string chequeID);

		DataSet GetChequeIssuedandSecirutyChqByID(string chequeID, bool IsSecurityChq);

		DataSet GetIssuedChequeToReturnList();

		DataSet GetRegisteredBlankChequeList();

		DataSet GetBlankCheque(string chequebookID, string chequeNumber);

		bool VoidBlankCheque(string chequebookID, string chequeNumber, string reasonID, string note, bool isVoid);

		bool InsertUpdateSecurityCheque(DataSet chequeData, bool isUpdate);

		DataSet GetTransactionByID(string sysDocID, string voucherID);

		bool VoidSecurityCheque(string chequebookID, string chequeNumber, DateTime voidDate);

		bool DeleteSecurityCheque(string sysDocID, string voucherID);

		DataSet GetIssuedChequeToPrint(DateTime startDate, DateTime endDate, bool includePrinted);

		bool MarkAsPrinted(int chequeID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetChequeCancelledList(DateTime from, DateTime to, bool showVoid);

		DataSet GetSecurityChequeList(DateTime from, DateTime to, bool showVoid);

		DataSet GetIssuedChequeAsOfDate(DateTime chkDatefrom, DateTime chkDateto, DateTime from, DateTime to, string fromBank, string toBank, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromLocationID, string toLocationID, bool cleared, bool bounced, bool cancelled, string strGroupBy, string vendorIDs);

		DataSet GetChequeMaturityReport(DateTime from, DateTime to, string fromBank, string toBank, bool cleared, bool bounced, bool cancelled);

		DataSet GetSecurityChequeToPrint(string sysDocID, string[] voucherID);

		DataSet GetSecurityChequeToPrint(string sysDocID, string voucherID);

		string GetAnalysisID(string sysDocID, string voucherID);

		DataSet GetIssuedChequeReturnList(DateTime from, DateTime to, bool showVoid);
	}
}
