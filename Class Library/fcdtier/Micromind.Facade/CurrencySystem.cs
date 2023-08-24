using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CurrencySystem : MarshalByRefObject, ICurrencySystem
	{
		private Config config;

		public CurrencySystem(Config config)
		{
			this.config = config;
		}

		public bool CreateCurrency(CurrencyData currencyData)
		{
			return new Currencies(config).InsertCurrency(currencyData);
		}

		public bool UpdateCurrency(CurrencyData currencyData)
		{
			return new Currencies(config).UpdateCurrency(currencyData);
		}

		public bool DeleteCurrency(string currencyID)
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.DeleteCurrency(currencyID);
			}
		}

		public CurrencyData GetCurrencyByID(string currencyID)
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetCurrencyByID(currencyID);
			}
		}

		public DataSet GetCurrencyComboList()
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetCurrencyComboList();
			}
		}

		public DataSet GetCurrencyList()
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetCurrencyList();
			}
		}

		public DataSet GetExchangeRateTable()
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetExchangeRateTable();
			}
		}

		public bool InsertExchangeRateTableRecord(CurrencyData data)
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.InsertExchangeRateTableRecord(data);
			}
		}

		public string GetCurrencyRateType(string currencyID)
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetCurrencyRateType(currencyID);
			}
		}

		public decimal GetCurrencyRate(string currencyID)
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetCurrencyRate(currencyID);
			}
		}

		public DataSet GetCurrencytables()
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetCurrencytables();
			}
		}

		public float GetCurrencyCount(string tableName, string currencyID)
		{
			using (Currencies currencies = new Currencies(config))
			{
				return currencies.GetCurrencyCount(tableName, currencyID);
			}
		}
	}
}
