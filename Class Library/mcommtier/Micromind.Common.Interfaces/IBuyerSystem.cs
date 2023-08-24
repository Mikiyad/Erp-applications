using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBuyerSystem
	{
		bool CreateBuyer(BuyerData buyerData);

		bool UpdateBuyer(BuyerData buyerData);

		BuyerData GetBuyer();

		bool DeleteBuyer(string ID);

		BuyerData GetBuyerByID(string id);

		DataSet GetBuyerByFields(params string[] columns);

		DataSet GetBuyerByFields(string[] ids, params string[] columns);

		DataSet GetBuyerByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetBuyerList();

		DataSet GetBuyerComboList();

		DataSet GetPurchaseByBuyerSummaryReport(DateTime from, DateTime to, string fromBuyer, string toBuyer);

		DataSet GetPurchaseByBuyerDetailReport(DateTime from, DateTime to, string fromBuyer, string toBuyer);
	}
}
