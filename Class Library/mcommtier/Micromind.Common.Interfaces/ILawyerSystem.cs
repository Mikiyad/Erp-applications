using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILawyerSystem
	{
		bool CreateLawyer(LawyerData vendorClassData);

		bool UpdateLawyer(LawyerData vendorClassData);

		LawyerData GetLawyer();

		bool DeleteLawyer(string ID);

		LawyerData GetLawyerByID(string id);

		DataSet GetLawyerByFields(params string[] columns);

		DataSet GetLawyerByFields(string[] ids, params string[] columns);

		DataSet GetLawyerByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetLawyerList();

		DataSet GetLawyerComboList();
	}
}
