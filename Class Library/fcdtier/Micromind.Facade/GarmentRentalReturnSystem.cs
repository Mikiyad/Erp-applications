using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class GarmentRentalReturnSystem : MarshalByRefObject, IGarmentRentalReturnSystem, IDisposable
	{
		private Config config;

		public GarmentRentalReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateGarmentRentalReturn(GarmentRentalReturnData data, bool isUpdate)
		{
			return new GarmentRentalReturn(config).InsertUpdateGarmentRentalReturn(data, isUpdate);
		}

		public GarmentRentalReturnData GetGarmentRentalReturnByID(string sysDocID, string voucherID)
		{
			return new GarmentRentalReturn(config).GetGarmentRentalReturnByID(sysDocID, voucherID);
		}

		public bool DeleteGarmentRentalReturn(string sysDocID, string voucherID)
		{
			return new GarmentRentalReturn(config).DeleteGarmentRentalReturn(sysDocID, voucherID);
		}

		public bool VoidGarmentRentalReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return new GarmentRentalReturn(config).VoidGarmentRentalReturn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetGarmentRentalReturnToPrint(string sysDocID, string voucherID)
		{
			return new GarmentRentalReturn(config).GetGarmentRentalReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetGarmentRentalReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new GarmentRentalReturn(config).GetGarmentRentalReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new GarmentRentalReturn(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenConsignments(string customerID)
		{
			return new GarmentRentalReturn(config).GetOpenConsignments(customerID);
		}

		public bool ConsignmentHasSettlement(string sysDocID, string voucherNumber)
		{
			return new GarmentRentalReturn(config).ConsignmentHasSettlement(sysDocID, voucherNumber, null);
		}
	}
}
