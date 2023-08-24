using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PartySystem : MarshalByRefObject, IPartySystem, IDisposable
	{
		private Config config;

		public PartySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateParty(PartyData data, bool isUpdate)
		{
			return new Party(config).InsertUpdateParty(data, isUpdate);
		}

		public PartyData GetParty()
		{
			using (Party party = new Party(config))
			{
				return party.GetParty();
			}
		}

		public bool DeleteParty(string groupID)
		{
			using (Party party = new Party(config))
			{
				return party.DeleteParty(groupID);
			}
		}

		public PartyData GetPartyByID(string id)
		{
			using (Party party = new Party(config))
			{
				return party.GetPartyByID(id);
			}
		}

		public DataSet GetPartyByFields(params string[] columns)
		{
			using (Party party = new Party(config))
			{
				return party.GetPartyByFields(columns);
			}
		}

		public DataSet GetPartyByFields(string[] ids, params string[] columns)
		{
			using (Party party = new Party(config))
			{
				return party.GetPartyByFields(ids, columns);
			}
		}

		public DataSet GetPartyByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Party party = new Party(config))
			{
				return party.GetPartyByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPartyList()
		{
			using (Party party = new Party(config))
			{
				return party.GetPartyList();
			}
		}

		public DataSet GetPartyComboList()
		{
			using (Party party = new Party(config))
			{
				return party.GetPartyComboList();
			}
		}
	}
}
