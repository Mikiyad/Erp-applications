using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICurrencySystem
	{
		bool CreateCurrency(CurrencyData currencyData);

		bool UpdateCurrency(CurrencyData currencyData);

		bool DeleteCurrency(string currencyID);

		CurrencyData GetCurrencyByID(string currencyID);

		DataSet GetCurrencyComboList();

		DataSet GetCurrencyList();

		DataSet GetExchangeRateTable();

		bool InsertExchangeRateTableRecord(CurrencyData data);

		string GetCurrencyRateType(string currencyID);

		decimal GetCurrencyRate(string currencyID);

		DataSet GetCurrencytables();

		float GetCurrencyCount(string tableName, string currencyID);
	}
}
