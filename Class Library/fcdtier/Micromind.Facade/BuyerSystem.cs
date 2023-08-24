using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BuyerSystem : MarshalByRefObject, IBuyerSystem, IDisposable
	{
		private Config config;

		public BuyerSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBuyer(BuyerData data)
		{
			return new Buyer(config).InsertBuyer(data);
		}

		public bool UpdateBuyer(BuyerData data)
		{
			return UpdateBuyer(data, checkConcurrency: false);
		}

		public bool UpdateBuyer(BuyerData data, bool checkConcurrency)
		{
			return new Buyer(config).UpdateBuyer(data);
		}

		public BuyerData GetBuyer()
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetBuyer();
			}
		}

		public bool DeleteBuyer(string groupID)
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.DeleteBuyer(groupID);
			}
		}

		public BuyerData GetBuyerByID(string id)
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetBuyerByID(id);
			}
		}

		public DataSet GetBuyerByFields(params string[] columns)
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetBuyerByFields(columns);
			}
		}

		public DataSet GetBuyerByFields(string[] ids, params string[] columns)
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetBuyerByFields(ids, columns);
			}
		}

		public DataSet GetBuyerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetBuyerByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetBuyerList()
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetBuyerList();
			}
		}

		public DataSet GetBuyerComboList()
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetBuyerComboList();
			}
		}

		public DataSet GetPurchaseByBuyerSummaryReport(DateTime from, DateTime to, string fromBuyer, string toBuyer)
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetPurchaseByBuyerSummaryReport(from, to, fromBuyer, toBuyer);
			}
		}

		public DataSet GetPurchaseByBuyerDetailReport(DateTime from, DateTime to, string fromBuyer, string toBuyer)
		{
			using (Buyer buyer = new Buyer(config))
			{
				return buyer.GetPurchaseByBuyerDetailReport(from, to, fromBuyer, toBuyer);
			}
		}
	}
}
