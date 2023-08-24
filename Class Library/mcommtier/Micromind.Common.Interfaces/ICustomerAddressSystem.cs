using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerAddressSystem
	{
		bool CreateCustomerAddress(CustomerAddressData customerAddressData);

		bool UpdateCustomerAddress(CustomerAddressData customerAddressData);

		CustomerAddressData GetCustomerAddress();

		bool DeleteCustomerAddress(string addressID, string customerID);

		CustomerAddressData GetCustomerAddressByID(string customerID, string addressID);

		DataSet GetCustomerAddressByFields(params string[] columns);

		DataSet GetCustomerAddressByFields(string[] ids, params string[] columns);

		DataSet GetCustomerAddressByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCustomerAddressList();

		bool IsPrimaryAddress(string addresssID, string customerID);

		DataSet GetCustomerAddressComboList();
	}
}
