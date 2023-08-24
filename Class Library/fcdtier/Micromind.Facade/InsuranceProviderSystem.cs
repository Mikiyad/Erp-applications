using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class InsuranceProviderSystem : MarshalByRefObject, IInsuranceProviderSystem, IDisposable
	{
		private Config config;

		public InsuranceProviderSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInsuranceProvider(InsuranceProviderData data)
		{
			return new InsuranceProvider(config).InsertInsuranceProvider(data);
		}

		public bool UpdateInsuranceProvider(InsuranceProviderData data)
		{
			return UpdateInsuranceProvider(data, checkConcurrency: false);
		}

		public bool UpdateInsuranceProvider(InsuranceProviderData data, bool checkConcurrency)
		{
			return new InsuranceProvider(config).UpdateInsuranceProvider(data);
		}

		public InsuranceProviderData GetInsuranceProvider()
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetInsuranceProvider();
			}
		}

		public bool DeleteInsuranceProvider(string groupID)
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.DeleteInsuranceProvider(groupID);
			}
		}

		public InsuranceProviderData GetInsuranceProviderByID(string id)
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetInsuranceProviderByID(id);
			}
		}

		public DataSet GetInsuranceProviderByFields(params string[] columns)
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetInsuranceProviderByFields(columns);
			}
		}

		public DataSet GetInsuranceProviderByFields(string[] ids, params string[] columns)
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetInsuranceProviderByFields(ids, columns);
			}
		}

		public DataSet GetInsuranceProviderByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetInsuranceProviderByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetInsuranceProviderList()
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetInsuranceProviderList();
			}
		}

		public DataSet GetInsuranceProviderComboList()
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetInsuranceProviderComboList();
			}
		}

		public DataSet GetMedicalInsuranceProviderComboList()
		{
			using (InsuranceProvider insuranceProvider = new InsuranceProvider(config))
			{
				return insuranceProvider.GetMedicalInsuranceProviderComboList();
			}
		}
	}
}
