using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPaymentMethodSystem
	{
		bool CreatePaymentMethod(PaymentMethodData paymentMethodData);

		bool UpdatePaymentMethod(PaymentMethodData paymentMethodData);

		PaymentMethodData GetPaymentMethods();

		bool DeletePaymentMethod(string paymentMethodID);

		DataSet GetPaymentMethodsByFields(params string[] columns);

		DataSet GetPaymentMethodsByFields(int[] paymentMethodID, params string[] columns);

		DataSet GetPaymentMethodsByFields(int[] paymentMethodID, bool isInactive, params string[] columns);

		PaymentMethodData GetPaymentMethodByID(string paymentMethodID);

		bool ExistPaymentMethod(string shortName);

		DataSet GetPaymentMethodsList();

		DataSet GetPaymentMethodsComboList();
	}
}
