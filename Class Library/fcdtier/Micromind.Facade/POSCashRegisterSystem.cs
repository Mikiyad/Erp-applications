using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class POSCashRegisterSystem : MarshalByRefObject, IPOSCashRegisterSystem, IDisposable
	{
		private Config config;

		public POSCashRegisterSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePOSCashRegister(POSCashRegisterData data)
		{
			return new POSCashRegister(config).InsertUpdatePOSCashRegister(data, isUpdate: false);
		}

		public bool UpdatePOSCashRegister(POSCashRegisterData data)
		{
			return UpdatePOSCashRegister(data, checkConcurrency: false);
		}

		public bool UpdatePOSCashRegister(POSCashRegisterData data, bool checkConcurrency)
		{
			return new POSCashRegister(config).InsertUpdatePOSCashRegister(data, isUpdate: true);
		}

		public DataSet GetPaymentMethodsList(string cashRegisterID)
		{
			return new POSCashRegister(config).GetPaymentMethodsList(cashRegisterID);
		}

		public DataSet GetPaymentMethodsList(string cashRegisterID, bool showInactive)
		{
			return new POSCashRegister(config).GetPaymentMethodsList(cashRegisterID, showInactive);
		}

		public DataSet GetExpenseAccountList(string cashRegisterID)
		{
			return new POSCashRegister(config).GetExpenseAccountsList(cashRegisterID);
		}

		public bool InsertUpdatePOSCashRegisterPaymentMethods(POSCashRegisterData posCashRegisterData)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.InsertUpdatePOSCashRegisterPaymentMethods(posCashRegisterData, isUpdate: false);
			}
		}

		public bool InsertUpdatePOSCashRegisterExpenseAccounts(POSCashRegisterData posCashRegisterData)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.InsertUpdatePOSCashRegisterExpenseAccounts(posCashRegisterData, isUpdate: false);
			}
		}

		public POSCashRegisterData GetPOSCashRegister()
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetPOSCashRegister();
			}
		}

		public bool DeletePOSCashRegister(string groupID)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.DeletePOSCashRegister(groupID);
			}
		}

		public POSCashRegisterData GetPOSCashRegisterByID(string id)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetPOSCashRegisterByID(id);
			}
		}

		public DataSet GetPOSCashRegisterByFields(params string[] columns)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetPOSCashRegisterByFields(columns);
			}
		}

		public DataSet GetPOSCashRegisterByFields(string[] ids, params string[] columns)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetPOSCashRegisterByFields(ids, columns);
			}
		}

		public DataSet GetPOSCashRegisterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetPOSCashRegisterByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPOSCashRegisterList()
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetPOSCashRegisterList();
			}
		}

		public DataSet GetPOSCashRegisterComboList()
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetPOSCashRegisterComboList();
			}
		}

		public DataSet GetCashRegisterByComputerName(string computerName)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetCashRegisterByComputerName(computerName);
			}
		}

		public bool IsCashRegisterFree(string cashRegisterID)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.IsCashRegisterFree(cashRegisterID);
			}
		}

		public string GetCurrentPOSLocation(string cashRegisterID)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.GetCurrentPOSLocation(cashRegisterID);
			}
		}

		public bool AssignCashRegister(string cashRegisterID, string computerName)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.AssignCashRegister(cashRegisterID, computerName);
			}
		}

		public bool ChangeCashRegister(string cashRegisterID, string computerName)
		{
			using (POSCashRegister pOSCashRegister = new POSCashRegister(config))
			{
				return pOSCashRegister.ChangeCashRegister(cashRegisterID, computerName);
			}
		}
	}
}
