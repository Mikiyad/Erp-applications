using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPayrollItemSystem
	{
		bool CreatePayrollItem(PayrollItemData payrollItemData);

		bool UpdatePayrollItem(PayrollItemData payrollItemData);

		PayrollItemData GetPayrollItem();

		bool DeletePayrollItem(string ID);

		PayrollItemData GetPayrollItemByID(string id);

		DataSet GetPayrollItemByFields(params string[] columns);

		DataSet GetPayrollItemByFields(string[] ids, params string[] columns);

		DataSet GetPayrollItemByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPayrollItemList();

		DataSet GetPayrollDeductionList();

		DataSet GetPayrollItemListforWeb();

		DataSet GetPayrollItemComboList();

		DataSet GetPayrollProfileReport(string fromPayrollItem, string toPayrollItem, bool showInactive);
	}
}
