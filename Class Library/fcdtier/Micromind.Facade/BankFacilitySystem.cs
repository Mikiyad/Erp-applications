using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BankFacilitySystem : MarshalByRefObject, IBankFacilitySystem, IDisposable
	{
		private Config config;

		public BankFacilitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBankFacility(BankFacilityData data)
		{
			return new BankFacility(config).InsertBankFacility(data);
		}

		public bool UpdateBankFacility(BankFacilityData data)
		{
			return UpdateBankFacility(data, checkConcurrency: false);
		}

		public bool UpdateBankFacility(BankFacilityData data, bool checkConcurrency)
		{
			return new BankFacility(config).UpdateBankFacility(data);
		}

		public BankFacilityData GetBankFacility()
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacility();
			}
		}

		public bool DeleteBankFacility(string groupID)
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.DeleteBankFacility(groupID);
			}
		}

		public BankFacilityData GetBankFacilityByID(string id)
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityByID(id);
			}
		}

		public DataSet GetBankFacilityByFields(params string[] columns)
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityByFields(columns);
			}
		}

		public DataSet GetBankFacilityByFields(string[] ids, params string[] columns)
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityByFields(ids, columns);
			}
		}

		public DataSet GetBankFacilityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetBankFacilityList()
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityList();
			}
		}

		public DataSet GetBankFacilityListByID(string id)
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityListByID(id);
			}
		}

		public DataSet GetBankFacilityComboList()
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityComboList();
			}
		}

		public decimal GetBankFacilityAvailableLimit(string facilityID)
		{
			using (BankFacility bankFacility = new BankFacility(config))
			{
				return bankFacility.GetBankFacilityAvailableLimit(facilityID);
			}
		}
	}
}
