using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductionSystem
	{
		bool CreateProduction(ProductionData productionData, bool isUpdate);

		ProductionData GetProductionByID(string sysDocID, string voucherID);

		DataSet GetProductionToPrint(string sysDocID, string[] voucherID);

		DataSet GetProductionToPrint(string sysDocID, string voucherID);

		bool DeleteProduction(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
