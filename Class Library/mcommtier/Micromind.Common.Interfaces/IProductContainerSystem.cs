using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductContainerSystem
	{
		int CreateContainer(string name);

		int CreateContainer(string name, string description, string note);

		int CreateContainer(ProductContainerData productContainerData);

		bool UpdateContainer(ProductContainerData productContainerData);

		bool CreateUpdateProductContainerBatch(DataSet listData, bool checkConcurrency);

		ProductContainerData GetContainers();

		ProductContainerData GetContainerByID(int containerID);

		ProductContainerData GetContainerByName(string name);

		bool ExistContainer(string name);

		bool DeleteContainer(int containerID);

		bool ActivateContainer(int containerID, bool activate);

		DataSet GetContainersByFields(params string[] columns);

		DataSet GetContainersByFields(bool isInactive, params string[] columns);

		DataSet GetContainersByFields(int[] containersID, params string[] columns);
	}
}
