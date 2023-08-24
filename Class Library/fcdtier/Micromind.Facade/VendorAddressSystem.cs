using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VendorAddressSystem : MarshalByRefObject, IVendorAddressSystem, IDisposable
	{
		private Config config;

		public VendorAddressSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVendorAddress(VendorAddressData data)
		{
			return new VendorAddresses(config).InsertVendorAddress(data);
		}

		public bool UpdateVendorAddress(VendorAddressData data)
		{
			return UpdateVendorAddress(data, checkConcurrency: false);
		}

		public bool UpdateVendorAddress(VendorAddressData data, bool checkConcurrency)
		{
			return new VendorAddresses(config).UpdateVendorAddress(data);
		}

		public VendorAddressData GetVendorAddress()
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.GetVendorAddress();
			}
		}

		public bool DeleteVendorAddress(string addressID, string vendorID)
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.DeleteVendorAddress(addressID, vendorID);
			}
		}

		public VendorAddressData GetVendorAddressByID(string vendorID, string addressID)
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.GetVendorAddressByID(vendorID, addressID);
			}
		}

		public DataSet GetVendorAddressByFields(params string[] columns)
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.GetVendorAddressByFields(columns);
			}
		}

		public DataSet GetVendorAddressByFields(string[] ids, params string[] columns)
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.GetVendorAddressByFields(ids, columns);
			}
		}

		public DataSet GetVendorAddressByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.GetVendorAddressByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVendorAddressList()
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.GetVendorAddressList();
			}
		}

		public bool IsPrimaryAddress(string addresssID, string vendorID)
		{
			using (VendorAddresses vendorAddresses = new VendorAddresses(config))
			{
				return vendorAddresses.IsPrimaryAddress(addresssID, vendorID);
			}
		}
	}
}
