using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILeadAddressSystem
	{
		bool CreateLeadAddress(LeadAddressData leadAddressData);

		bool UpdateLeadAddress(LeadAddressData leadAddressData);

		LeadAddressData GetLeadAddress();

		bool DeleteLeadAddress(string addressID, string leadID);

		LeadAddressData GetLeadAddressByID(string leadID, string addressID);

		DataSet GetLeadAddressByFields(params string[] columns);

		DataSet GetLeadAddressByFields(string[] ids, params string[] columns);

		DataSet GetLeadAddressByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetLeadAddressList();

		bool IsPrimaryAddress(string addresssID, string leadID);

		DataSet GetLeadAddressComboList();
	}
}
