using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IINCOSystem
	{
		bool CreateINCO(INCOData INCOData);

		bool UpdateINCO(INCOData INCOData);

		INCOData GetINCO();

		bool DeleteINCO(string ID);

		INCOData GetINCOByID(string id);

		DataSet GetINCOByFields(params string[] columns);

		DataSet GetINCOByFields(string[] ids, params string[] columns);

		DataSet GetINCOByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetINCOList();

		DataSet GetINCOComboList();
	}
}
