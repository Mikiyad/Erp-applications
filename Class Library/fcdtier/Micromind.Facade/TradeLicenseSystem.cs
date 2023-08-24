using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TradeLicenseSystem : MarshalByRefObject, ITradeLicenseSystem, IDisposable
	{
		private Config config;

		public TradeLicenseSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTradeLicense(TradeLicenseData data)
		{
			return new TradeLicense(config).InsertTradeLicense(data);
		}

		public bool UpdateTradeLicense(TradeLicenseData data)
		{
			return UpdateTradeLicense(data, checkConcurrency: false);
		}

		public bool UpdateTradeLicense(TradeLicenseData data, bool checkConcurrency)
		{
			return new TradeLicense(config).UpdateTradeLicense(data);
		}

		public TradeLicenseData GetTradeLicense()
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.GetTradeLicense();
			}
		}

		public bool DeleteTradeLicense(string groupID)
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.DeleteTradeLicense(groupID);
			}
		}

		public TradeLicenseData GetTradeLicenseByID(string id)
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.GetTradeLicenseByID(id);
			}
		}

		public DataSet GetTradeLicenseByFields(params string[] columns)
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.GetTradeLicenseByFields(columns);
			}
		}

		public DataSet GetTradeLicenseByFields(string[] ids, params string[] columns)
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.GetTradeLicenseByFields(ids, columns);
			}
		}

		public DataSet GetTradeLicenseByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.GetTradeLicenseByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetTradeLicenseList()
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.GetTradeLicenseList();
			}
		}

		public DataSet GetTradeLicenseComboList()
		{
			using (TradeLicense tradeLicense = new TradeLicense(config))
			{
				return tradeLicense.GetTradeLicenseComboList();
			}
		}
	}
}
