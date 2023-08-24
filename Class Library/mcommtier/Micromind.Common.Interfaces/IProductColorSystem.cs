using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductColorSystem
	{
		int CreateColor(string name);

		int CreateColor(string name, string description, string note);

		int CreateColor(ProductColorData productColorData);

		bool UpdateColor(ProductColorData productColorData);

		bool CreateUpdateProductColorBatch(DataSet listData, bool checkConcurrency);

		ProductColorData GetColors();

		ProductColorData GetColorByID(int colorID);

		ProductColorData GetColorByName(string name);

		bool ExistColor(string name);

		bool DeleteColor(int colorID);

		bool ActivateColor(int colorID, bool activate);

		DataSet GetColorsByFields(params string[] columns);

		DataSet GetColorsByFields(bool isInactive, params string[] columns);

		DataSet GetColorsByFields(int[] colorsID, params string[] columns);
	}
}
