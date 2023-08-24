using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class GarmentRentalSystem : MarshalByRefObject, IGarmentRentalSystem, IDisposable
	{
		private Config config;

		public GarmentRentalSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateGarmentRental(GarmentRentalData data, bool isUpdate)
		{
			return new GarmentRental(config).InsertUpdateGarmentRental(data, isUpdate);
		}

		public GarmentRentalData GetGarmentRentalByID(string sysDocID, string voucherID)
		{
			return new GarmentRental(config).GetGarmentRentalByID(sysDocID, voucherID);
		}

		public bool DeleteGarmentRental(string sysDocID, string voucherID)
		{
			return new GarmentRental(config).DeleteGarmentRental(sysDocID, voucherID);
		}

		public bool VoidGarmentRental(string sysDocID, string voucherID, bool isVoid)
		{
			return new GarmentRental(config).VoidGarmentRental(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedGarmentRentals(string customerID)
		{
			return new GarmentRental(config).GetUninvoicedGarmentRentals(customerID);
		}

		public DataSet GetGarmentRentalToPrint(string sysDocID, string voucherID)
		{
			return new GarmentRental(config).GetGarmentRentalToPrint(sysDocID, voucherID);
		}

		public DataSet GetGarmentRentalToPrint(string sysDocID, string[] voucherID)
		{
			return new GarmentRental(config).GetGarmentRentalToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new GarmentRental(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenGarmentRentals(string customerID)
		{
			return new GarmentRental(config).GetOpenGarmentRentals(customerID);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new GarmentRental(config).AllowModify(sysDocID, voucherNumber, null);
		}

		public DataSet GetPendingConsignmentsReport(string fromCustomer, string toCustomer)
		{
			return null;
		}

		public DataSet GetConsignmentOutSettlementReport(DateTime settleto, string fromCustomer, string toCustomer)
		{
			return null;
		}

		public DataSet GetConsignmentOutIssuedReport(DateTime settleto, DateTime from, DateTime to, string fromCustomer, string toCustomer, int intstatus)
		{
			return null;
		}

		public DataSet GetGarmentRentalList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			using (GarmentRental garmentRental = new GarmentRental(config))
			{
				return garmentRental.GetGarmentRentalList(sysDocID, fromDate, endDate);
			}
		}

		public DataSet GetGarmentRentalSummaryReport(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			using (GarmentRental garmentRental = new GarmentRental(config))
			{
				return garmentRental.GetGarmentRentalSummaryReport(sysDocID, voucherID, from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup);
			}
		}

		public bool AllowDelete(string sysDocID, string voucherNumber)
		{
			return new GarmentRental(config).AllowDelete(sysDocID, voucherNumber);
		}

		public DataSet GetGarmentRentalAgreement(string sysDocID, string voucherID, string customerID)
		{
			return new GarmentRental(config).GetGarmentRentalAgreement(sysDocID, voucherID, customerID);
		}

		public DataSet GetGarmentRentalAgreement(string sysDocID, string[] voucherID, string customerID)
		{
			return new GarmentRental(config).GetGarmentRentalAgreement(sysDocID, voucherID, customerID);
		}
	}
}
