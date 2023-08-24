using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICreditCardSystem
	{
		int CreateCreditCard(CreditCardData creditCardData);

		int CreateCardType(CardTypeData cardTypeData);

		bool UpdateCreditCard(CreditCardData creditCardData);

		bool UpdateCardType(CardTypeData cardTypeData);

		CreditCardData GetCreditCards();

		CardTypeData GetCardTypes();

		bool DeleteCreditCard(int creditCardID);

		DataSet GetCreditCardsByFields(params string[] columns);

		DataSet GetCreditCardsByFields(int[] creditCardID, params string[] columns);

		CreditCardData GetCreditCardByID(int creditCardID);

		CardTypeData GetCardTypeByID(int typeID);

		bool ExistCreditCard(string shortName);
	}
}
