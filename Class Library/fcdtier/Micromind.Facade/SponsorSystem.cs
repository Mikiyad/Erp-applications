using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SponsorSystem : MarshalByRefObject, ISponsorSystem, IDisposable
	{
		private Config config;

		public SponsorSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSponsor(SponsorData data)
		{
			return new Sponsor(config).InsertSponsor(data);
		}

		public bool UpdateSponsor(SponsorData data)
		{
			return UpdateSponsor(data, checkConcurrency: false);
		}

		public bool UpdateSponsor(SponsorData data, bool checkConcurrency)
		{
			return new Sponsor(config).UpdateSponsor(data);
		}

		public SponsorData GetSponsor()
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.GetSponsor();
			}
		}

		public bool DeleteSponsor(string groupID)
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.DeleteSponsor(groupID);
			}
		}

		public SponsorData GetSponsorByID(string id)
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.GetSponsorByID(id);
			}
		}

		public DataSet GetSponsorByFields(params string[] columns)
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.GetSponsorByFields(columns);
			}
		}

		public DataSet GetSponsorByFields(string[] ids, params string[] columns)
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.GetSponsorByFields(ids, columns);
			}
		}

		public DataSet GetSponsorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.GetSponsorByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSponsorList()
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.GetSponsorList();
			}
		}

		public DataSet GetSponsorComboList()
		{
			using (Sponsor sponsor = new Sponsor(config))
			{
				return sponsor.GetSponsorComboList();
			}
		}
	}
}
