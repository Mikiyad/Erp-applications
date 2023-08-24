using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IServiceTypeSystem
	{
		bool CreateServiceItem(ServiceItemData ServiceItemData);

		bool UpdateServiceItem(ServiceItemData ServiceItemData);

		ServiceItemData GetServiceItem();

		bool DeleteServiceItem(string ID);

		ServiceItemData GetServiceItemByID(string id);

		DataSet GetServiceItemList();

		DataSet GetServiceItemComboList();
	}
}
