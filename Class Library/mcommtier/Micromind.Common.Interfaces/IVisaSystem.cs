using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVisaSystem
	{
		bool CreateVisa(VisaData visaData);

		bool UpdateVisa(VisaData visaData);

		VisaData GetVisa();

		bool DeleteVisa(string ID);

		VisaData GetVisaByID(string id);

		DataSet GetVisaByFields(params string[] columns);

		DataSet GetVisaByFields(string[] ids, params string[] columns);

		DataSet GetVisaByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetVisaList();

		DataSet GetVisaComboList();
	}
}
