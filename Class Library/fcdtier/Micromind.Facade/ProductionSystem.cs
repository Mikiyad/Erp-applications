using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductionSystem : MarshalByRefObject, IProductionSystem, IDisposable
	{
		private Config config;

		public ProductionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProduction(ProductionData data, bool isUpdate)
		{
			return new Production(config).InsertUpdateProduction(data, isUpdate);
		}

		public ProductionData GetProductionByID(string sysDocID, string voucherID)
		{
			return new Production(config).GetProductionByID(sysDocID, voucherID);
		}

		public bool DeleteProduction(string sysDocID, string voucherID)
		{
			return new Production(config).DeleteProduction(sysDocID, voucherID);
		}

		public DataSet GetProductionToPrint(string sysDocID, string[] voucherID)
		{
			return new Production(config).GetProductionToPrint(sysDocID, voucherID);
		}

		public DataSet GetProductionToPrint(string sysDocID, string voucherID)
		{
			return new Production(config).GetProductionToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new Production(config).GetProductionList(fromDate, toDate, Isvoid: true);
		}
	}
}
