using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRentalPostingSystem
	{
		bool CreateRentalPosting(RentalPostingData salarySheetData, bool isUpdate);

		RentalPostingData GetRentalPostingByID(string sysDocID, string voucherID);

		bool DeleteRentalPosting(string sysDocID, string voucherID);

		bool VoidRentalPosting(string sysDocID, string voucherID, bool isVoid);

		DataSet CalculateRentalPosting(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth);

		bool PostRentalPosting(string sysDocID, string voucherID);

		DataSet GetUnpostedRentalPostings();

		DataSet GetOpenRentalPostings();

		DataSet GetRentalPostingEmployees(string docID, string voucherID, byte paymentMethodID);

		DataSet GetRentalPostingItems(string sysDocID, string voucherID, string[] employeeIDs);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
