using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomListSystem
	{
		bool InsertUpdateCustomList(DataSet customListData, bool isUpdate);

		bool DeleteCustomList(string listCode);

		DataSet GetCustomListItems(string listCode);

		DataSet GetCustomListByID(string listCode);

		DataSet GetCustomListComboList(string listCode);

		DataSet GetCustomListCodes();
	}
}
