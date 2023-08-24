using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISponsorSystem
	{
		bool CreateSponsor(SponsorData sponsorData);

		bool UpdateSponsor(SponsorData sponsorData);

		SponsorData GetSponsor();

		bool DeleteSponsor(string ID);

		SponsorData GetSponsorByID(string id);

		DataSet GetSponsorByFields(params string[] columns);

		DataSet GetSponsorByFields(string[] ids, params string[] columns);

		DataSet GetSponsorByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetSponsorList();

		DataSet GetSponsorComboList();
	}
}
