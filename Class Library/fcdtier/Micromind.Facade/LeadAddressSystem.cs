using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LeadAddressSystem : MarshalByRefObject, ILeadAddressSystem, IDisposable
	{
		private Config config;

		public LeadAddressSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLeadAddress(LeadAddressData data)
		{
			return new LeadAddresses(config).InsertLeadAddress(data);
		}

		public bool UpdateLeadAddress(LeadAddressData data)
		{
			return UpdateLeadAddress(data, checkConcurrency: false);
		}

		public bool UpdateLeadAddress(LeadAddressData data, bool checkConcurrency)
		{
			return new LeadAddresses(config).UpdateLeadAddress(data);
		}

		public LeadAddressData GetLeadAddress()
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.GetLeadAddress();
			}
		}

		public bool DeleteLeadAddress(string addressID, string leadID)
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.DeleteLeadAddress(addressID, leadID);
			}
		}

		public LeadAddressData GetLeadAddressByID(string leadID, string addressID)
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.GetLeadAddressByID(leadID, addressID);
			}
		}

		public DataSet GetLeadAddressByFields(params string[] columns)
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.GetLeadAddressByFields(columns);
			}
		}

		public DataSet GetLeadAddressByFields(string[] ids, params string[] columns)
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.GetLeadAddressByFields(ids, columns);
			}
		}

		public DataSet GetLeadAddressByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.GetLeadAddressByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetLeadAddressList()
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.GetLeadAddressList();
			}
		}

		public bool IsPrimaryAddress(string addresssID, string leadID)
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.IsPrimaryAddress(addresssID, leadID);
			}
		}

		public DataSet GetLeadAddressComboList()
		{
			using (LeadAddresses leadAddresses = new LeadAddresses(config))
			{
				return leadAddresses.GetLeadAddressComboList();
			}
		}
	}
}
