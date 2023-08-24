using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILegalActionStatusSystem
	{
		bool CreateLegalActionStatus(LegalActionStatusData LegalActionStatusData);

		bool UpdateLegalActionStatus(LegalActionStatusData LegalActionStatusData);

		LegalActionStatusData GetLegalActionStatus();

		bool DeleteLegalActionStatus(string ID);

		LegalActionStatusData GetLegalActionStatusByID(string id);

		DataSet GetLegalActionStatusByFields(params string[] columns);

		DataSet GetLegalActionStatusByFields(string[] ids, params string[] columns);

		DataSet GetLegalActionStatusByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetLegalActionStatusList();

		DataSet GetLegalActionStatusComboList();
	}
}
