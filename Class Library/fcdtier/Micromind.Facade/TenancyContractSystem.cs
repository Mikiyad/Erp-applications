using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TenancyContractSystem : MarshalByRefObject, ITenancyContractSystem, IDisposable
	{
		private Config config;

		public TenancyContractSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTenancyContract(TenancyContractData data)
		{
			return new TenancyContract(config).InsertTenancyContract(data);
		}

		public bool UpdateTenancyContract(TenancyContractData data)
		{
			return UpdateTenancyContract(data, checkConcurrency: false);
		}

		public bool UpdateTenancyContract(TenancyContractData data, bool checkConcurrency)
		{
			return new TenancyContract(config).UpdateTenancyContract(data);
		}

		public TenancyContractData GetTenancyContract()
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.GetTenancyContract();
			}
		}

		public bool DeleteTenancyContract(string groupID)
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.DeleteTenancyContract(groupID);
			}
		}

		public TenancyContractData GetTenancyContractByID(string id)
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.GetTenancyContractByID(id);
			}
		}

		public DataSet GetTenancyContractByFields(params string[] columns)
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.GetTenancyContractByFields(columns);
			}
		}

		public DataSet GetTenancyContractByFields(string[] ids, params string[] columns)
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.GetTenancyContractByFields(ids, columns);
			}
		}

		public DataSet GetTenancyContractByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.GetTenancyContractByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetTenancyContractList()
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.GetTenancyContractList();
			}
		}

		public DataSet GetTenancyContractComboList()
		{
			using (TenancyContract tenancyContract = new TenancyContract(config))
			{
				return tenancyContract.GetTenancyContractComboList();
			}
		}
	}
}
