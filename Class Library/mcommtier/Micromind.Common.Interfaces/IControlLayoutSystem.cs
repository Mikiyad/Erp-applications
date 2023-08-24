using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IControlLayoutSystem
	{
		bool SaveControlLayout(ControlLayoutData data);

		ControlLayoutData GetControlLayout();

		bool DeleteControlLayout(ControlLayoutTypes type, string controlName, string layoutName);

		byte[] GetControlLayoutByID(ControlLayoutTypes type, string controlName, string layoutName);

		DataSet GetControlLayoutComboList();
	}
}
