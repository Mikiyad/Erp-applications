using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVendorClassSystem
	{
		bool CreateVendorClass(VendorClassData vendorClassData);

		bool UpdateVendorClass(VendorClassData vendorClassData);

		VendorClassData GetVendorClass();

		bool DeleteVendorClass(string ID);

		VendorClassData GetVendorClassByID(string id);

		DataSet GetVendorClassByFields(params string[] columns);

		DataSet GetVendorClassByFields(string[] ids, params string[] columns);

		DataSet GetVendorClassByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetVendorClassList();

		DataSet GetVendorClassComboList();
	}
}
