using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VendorGroupSystem : MarshalByRefObject, IVendorGroupSystem, IDisposable
	{
		private Config config;

		public VendorGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVendorGroup(VendorGroupData data)
		{
			return new VendorGroup(config).InsertVendorGroup(data);
		}

		public bool UpdateVendorGroup(VendorGroupData data)
		{
			return UpdateVendorGroup(data, checkConcurrency: false);
		}

		public bool UpdateVendorGroup(VendorGroupData data, bool checkConcurrency)
		{
			return new VendorGroup(config).UpdateVendorGroup(data);
		}

		public VendorGroupData GetVendorGroup()
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.GetVendorGroup();
			}
		}

		public bool DeleteVendorGroup(string groupID)
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.DeleteVendorGroup(groupID);
			}
		}

		public VendorGroupData GetVendorGroupByID(string id)
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.GetVendorGroupByID(id);
			}
		}

		public DataSet GetVendorGroupByFields(params string[] columns)
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.GetVendorGroupByFields(columns);
			}
		}

		public DataSet GetVendorGroupByFields(string[] ids, params string[] columns)
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.GetVendorGroupByFields(ids, columns);
			}
		}

		public DataSet GetVendorGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.GetVendorGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVendorGroupList()
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.GetVendorGroupList();
			}
		}

		public DataSet GetVendorGroupComboList()
		{
			using (VendorGroup vendorGroup = new VendorGroup(config))
			{
				return vendorGroup.GetVendorGroupComboList();
			}
		}
	}
}
