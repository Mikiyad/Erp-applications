using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVendorGroupSystem
	{
		bool CreateVendorGroup(VendorGroupData departmentData);

		bool UpdateVendorGroup(VendorGroupData departmentData);

		VendorGroupData GetVendorGroup();

		bool DeleteVendorGroup(string ID);

		VendorGroupData GetVendorGroupByID(string id);

		DataSet GetVendorGroupByFields(params string[] columns);

		DataSet GetVendorGroupByFields(string[] ids, params string[] columns);

		DataSet GetVendorGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetVendorGroupList();

		DataSet GetVendorGroupComboList();
	}
}
