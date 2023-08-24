using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVendorAddressSystem
	{
		bool CreateVendorAddress(VendorAddressData vendorAddressData);

		bool UpdateVendorAddress(VendorAddressData vendorAddressData);

		VendorAddressData GetVendorAddress();

		bool DeleteVendorAddress(string addressID, string vendorID);

		VendorAddressData GetVendorAddressByID(string vendorID, string addressID);

		DataSet GetVendorAddressByFields(params string[] columns);

		DataSet GetVendorAddressByFields(string[] ids, params string[] columns);

		DataSet GetVendorAddressByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetVendorAddressList();

		bool IsPrimaryAddress(string addresssID, string vendorID);
	}
}
