using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomGadgetSystem
	{
		bool CreateCustomGadget(CustomGadgetData customGadgetData);

		bool UpdateCustomGadget(CustomGadgetData customGadgetData);

		bool CreateUpdateCustomGadgetBatch(DataSet listData, bool checkConcurrency);

		CustomGadgetData GetCustomGadgets();

		DataSet GetCustomGadgetList();

		bool DeleteCustomGadget(string customGadgetID);

		DataSet GetCustomGadgetsByFields(params string[] columns);

		DataSet GetCustomGadgetsByFields(string[] customGadgetsID, params string[] columns);

		DataSet GetCustomGadgetsByFields(string[] customGadgetsID, bool isInactive, params string[] columns);

		CustomGadgetData GetCustomGadgetByID(string customGadgetID);

		bool ExistCustomGadget(string customGadgetName);

		DataSet GetCustomGadgetComboList();

		DataSet GetTableSchema(string query);

		DataSet GetCustomGadgetData(string reportID, string[] parArray, string[] valArray);

		bool SaveLayout(string reportID, byte[] layout, int formWidth, int formHeight);

		bool AddGadgetPhoto(string productID, byte[] image);

		byte[] GetGadgetThumbnailImage(string contactID);
	}
}
