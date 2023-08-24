using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertySystem
	{
		bool CreateProperty(PropertyData propertyData);

		bool UpdateProperty(PropertyData propertyData);

		PropertyData GetProperty();

		bool DeleteProperty(string ID);

		PropertyData GetPropertyByID(string id);

		DataSet GetPropertyByFields(params string[] columns);

		DataSet GetPropertyByFields(string[] ids, params string[] columns);

		DataSet GetPropertyByFields(string[] ids, bool isInactive, params string[] columns);

		byte[] GetPropertyThumbnailImage(string contactID);

		DataSet GetPropertyList(bool showInactive);

		DataSet GetPropertyComboList();

		DataSet GetPropertyRentDetailsReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromUnit, string toUnit, string fromProperty, string toProperty, string Type, string customerIDs, string fromAgent, string toAgent);

		DataSet GetPropertyAvailabilityReport(DateTime date, string fromClass, string toClass, string fromGroup, string toGroup, string fromPropertyClass, string toPropertyClass);

		DataSet GetPropertyUnitAvailabilityReport(DateTime from, DateTime to, string fromUnit, string toUnit, string fromProperty, string toProperty, string basedOn);

		DataSet GetPropertyUnitHistoryReport(DateTime from, DateTime to, string fromUnit, string toUnit, string fromProperty, string toProperty);

		bool AddPropertyPhoto(string propertyID, byte[] pictureByte);

		bool RemovePropertyPhoto(string propertyID);

		DataSet GetPropertyReport(DateTime date, string fromClass, string toClass, string fromGroup, string toGroup);
	}
}
