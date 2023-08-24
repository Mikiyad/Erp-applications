using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IShippingMethodSystem
	{
		bool CreateShippingMethod(string companyName, string phone, string notes);

		bool CreateShippingMethod(string companyName);

		bool CreateShippingMethod(ShippingMethodData shippingMethodData);

		bool UpdateShippingMethod(ShippingMethodData shippingMethodData);

		bool CreateUpdateShippingMethodBatch(DataSet listData, bool checkConcurrency);

		ShippingMethodData GetShippingMethods();

		bool DeleteShippingMethod(string shippingMethodID);

		ShippingMethodData GetShippingMethodByID(string shippingMethodID);

		bool ExistShippingMethod(string companyName);

		DataSet GetShippingMethodsByFields(params string[] columns);

		DataSet GetShippingMethodsByFields(bool isInactive, int[] shippingMethodsID, params string[] columns);

		DataSet GetShippingMethodsByFields(int[] shippingMethodsID, params string[] columns);

		DataSet GetShippingMethodsComboList();

		DataSet GetShippingMethodsList();
	}
}
