using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRegisterSystem
	{
		bool CreateRegister(RegisterData registerData);

		bool UpdateRegister(RegisterData registerData);

		RegisterData GetRegister();

		bool DeleteRegister(string ID);

		RegisterData GetRegisterByID(string id);

		DataSet GetRegisterByFields(params string[] columns);

		DataSet GetRegisterByFields(string[] ids, params string[] columns);

		DataSet GetRegisterByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetRegisterList();

		DataSet GetRegisterComboList();

		decimal GetRegisterBalance(string registerID, string accountFieldIDName);
	}
}
