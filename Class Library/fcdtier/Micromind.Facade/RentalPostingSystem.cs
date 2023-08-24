using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RentalPostingSystem : MarshalByRefObject, IRentalPostingSystem, IDisposable
	{
		private Config config;

		public RentalPostingSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRentalPosting(RentalPostingData data, bool isUpdate)
		{
			return new RentalPosting(config).InsertUpdateRentalPosting(data, isUpdate);
		}

		public RentalPostingData GetRentalPostingByID(string sysDocID, string voucherID)
		{
			return new RentalPosting(config).GetRentalPostingByID(sysDocID, voucherID);
		}

		public bool DeleteRentalPosting(string sysDocID, string voucherID)
		{
			return new RentalPosting(config).DeleteRentalPosting(sysDocID, voucherID);
		}

		public bool VoidRentalPosting(string sysDocID, string voucherID, bool isVoid)
		{
			return new RentalPosting(config).VoidRentalPosting(sysDocID, voucherID, isVoid);
		}

		public DataSet CalculateRentalPosting(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth)
		{
			return new RentalPosting(config).CalculateRentalPosting(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, startDate, endDate, periodYear, periodMonth);
		}

		public bool PostRentalPosting(string sysDocID, string voucherID)
		{
			return new RentalPosting(config).PostRentalPosting(sysDocID, voucherID);
		}

		public DataSet GetUnpostedRentalPostings()
		{
			return new RentalPosting(config).GetUnpostedRentalPostings();
		}

		public DataSet GetOpenRentalPostings()
		{
			return new RentalPosting(config).GetOpenRentalPostings();
		}

		public DataSet GetRentalPostingEmployees(string docID, string voucherID, byte paymentMethodID)
		{
			return new RentalPosting(config).GetRentalPostingEmployees(docID, voucherID, paymentMethodID);
		}

		public DataSet GetRentalPostingItems(string sysDocID, string voucherID, string[] employeeIDs)
		{
			return new RentalPosting(config).GetRentalPostingItems(sysDocID, voucherID, employeeIDs);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new RentalPosting(config).GetList(from, to, showVoid);
		}

		public DataSet GetSelectedSalaryBankTransfer(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string sysdocid, string voucherid)
		{
			return new RentalPosting(config).GetSelectedSalaryBankTransfer(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, sysdocid, voucherid);
		}
	}
}
