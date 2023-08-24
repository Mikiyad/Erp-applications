using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IShipmentAddressSystem
	{
		int CreateShipmentAddress(int partenrID, string shipmentName);

		int CreateShipmentAddress(int partenrID, string shipmentName, string line1, string line2, string line3, string line4, string line5, string description);

		int CreateShipmentAddress(ShipmentAddressData shipmentAddressData);

		bool UpdateShipmentAddress(ShipmentAddressData shipmentAddressData);

		bool CreateUpdateShipmentAddressBatch(DataSet listData, bool checkConcurrency);

		ShipmentAddressData GetShipmentAddresses();

		ShipmentAddressData GetShipmentAddressesByShipmentID(int shipmentID);

		bool DeleteShipmentAddress(int shipmentAddressID);

		DataSet GetShipmentAddressesSummary();

		ShipmentAddressData GetShipmentAddressesByPartnerID(int partnerID);

		bool ActivateShipemtAddress(object id, bool activate);

		bool ExistShipmentName(int partnerID, string shipmentName);

		DataSet GetShipmentAddressesByFields(params string[] columns);

		DataSet GetShipmentAddressesByFields(int[] addressesID, params string[] columns);

		DataSet GetShipmentAddressesByFields(bool isInactive, int[] addressesID, params string[] columns);

		DataSet GetDefaultShippingAddress(int[] partnersID);

		DataSet GetDefaultBillingAddress(int[] partnersID);

		DataSet GetDefaultShippingAddressLines(int[] partnersID);

		DataSet GetDefaultBillingAddressLines(int[] partnersID);
	}
}
