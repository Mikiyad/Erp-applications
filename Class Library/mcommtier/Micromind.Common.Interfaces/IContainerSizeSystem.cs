using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IContainerSizeSystem
	{
		bool CreateContainerSize(ContainerSizeData containerSizeData);

		bool UpdateContainerSize(ContainerSizeData containerSizeData);

		ContainerSizeData GetContainerSize();

		bool DeleteContainerSize(string ID);

		ContainerSizeData GetContainerSizeByID(string id);

		DataSet GetContainerSizeByFields(params string[] columns);

		DataSet GetContainerSizeByFields(string[] ids, params string[] columns);

		DataSet GetContainerSizeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetContainerSizeList();

		DataSet GetContainerSizeComboList();
	}
}
