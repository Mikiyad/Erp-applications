using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPOSCashRegisterSystem
	{
		bool CreatePOSCashRegister(POSCashRegisterData posCashRegisterData);

		bool UpdatePOSCashRegister(POSCashRegisterData posCashRegisterData);

		POSCashRegisterData GetPOSCashRegister();

		bool DeletePOSCashRegister(string ID);

		DataSet GetPaymentMethodsList(string cashRegisterID);

		DataSet GetPaymentMethodsList(string cashRegisterID, bool showInactive);

		DataSet GetExpenseAccountList(string cashRegisterID);

		POSCashRegisterData GetPOSCashRegisterByID(string id);

		bool InsertUpdatePOSCashRegisterPaymentMethods(POSCashRegisterData posCashRegisterData);

		bool InsertUpdatePOSCashRegisterExpenseAccounts(POSCashRegisterData posCashRegisterData);

		DataSet GetPOSCashRegisterByFields(params string[] columns);

		DataSet GetPOSCashRegisterByFields(string[] ids, params string[] columns);

		DataSet GetPOSCashRegisterByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPOSCashRegisterList();

		DataSet GetPOSCashRegisterComboList();

		DataSet GetCashRegisterByComputerName(string computerName);

		bool IsCashRegisterFree(string cashRegisterID);

		bool AssignCashRegister(string cashRegisterID, string computerName);

		string GetCurrentPOSLocation(string cashRegisterID);

		bool ChangeCashRegister(string cashRegisterID, string computerName);
	}
}
