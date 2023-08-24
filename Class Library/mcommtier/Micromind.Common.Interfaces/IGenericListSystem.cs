using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IGenericListSystem
	{
		bool CreateGenericList(GenericListData genericListData);

		bool UpdateGenericList(GenericListData genericListData);

		GenericListData GetGenericList(GenericListTypes listType);

		bool DeleteGenericList(string ID, GenericListTypes listType);

		GenericListData GetGenericListByID(string id, GenericListTypes listType);

		DataSet GetGenericListByFields(params string[] columns);

		DataSet GetGenericListByFields(string[] ids, params string[] columns);

		DataSet GetGenericListByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetGenericListList(GenericListTypes listType);

		DataSet GetGenericListList(GenericListTypes listType, bool islistType);

		DataSet GetGenericListComboList(GenericListTypes listType);
	}
}
