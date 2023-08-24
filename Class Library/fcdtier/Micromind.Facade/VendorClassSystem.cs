using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VendorClassSystem : MarshalByRefObject, IVendorClassSystem, IDisposable
	{
		private Config config;

		public VendorClassSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVendorClass(VendorClassData data)
		{
			return new VendorClass(config).InsertVendorClass(data);
		}

		public bool UpdateVendorClass(VendorClassData data)
		{
			return UpdateVendorClass(data, checkConcurrency: false);
		}

		public bool UpdateVendorClass(VendorClassData data, bool checkConcurrency)
		{
			return new VendorClass(config).UpdateVendorClass(data);
		}

		public VendorClassData GetVendorClass()
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.GetVendorClass();
			}
		}

		public bool DeleteVendorClass(string groupID)
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.DeleteVendorClass(groupID);
			}
		}

		public VendorClassData GetVendorClassByID(string id)
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.GetVendorClassByID(id);
			}
		}

		public DataSet GetVendorClassByFields(params string[] columns)
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.GetVendorClassByFields(columns);
			}
		}

		public DataSet GetVendorClassByFields(string[] ids, params string[] columns)
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.GetVendorClassByFields(ids, columns);
			}
		}

		public DataSet GetVendorClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.GetVendorClassByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVendorClassList()
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.GetVendorClassList();
			}
		}

		public DataSet GetVendorClassComboList()
		{
			using (VendorClass vendorClass = new VendorClass(config))
			{
				return vendorClass.GetVendorClassComboList();
			}
		}
	}
}
