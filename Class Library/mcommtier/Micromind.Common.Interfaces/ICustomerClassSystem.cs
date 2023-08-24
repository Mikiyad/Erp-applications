using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerClassSystem
	{
		bool CreateCustomerClass(CustomerClassData customerClassData);

		bool UpdateCustomerClass(CustomerClassData customerClassData);

		CustomerClassData GetCustomerClass();

		bool DeleteCustomerClass(string ID);

		CustomerClassData GetCustomerClassByID(string id);

		DataSet GetCustomerClassByFields(params string[] columns);

		DataSet GetCustomerClassByFields(string[] ids, params string[] columns);

		DataSet GetCustomerClassByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCustomerClassList();

		DataSet GetCustomerClassComboList();

		DataSet GetTenantClassComboList();
	}
}
