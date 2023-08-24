using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BankFacilityGroupSystem : MarshalByRefObject, IBankFacilityGroupSystem, IDisposable
	{
		private Config config;

		public BankFacilityGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBankFacilityGroup(BankFacilityGroupData data)
		{
			return new BankFacilityGroup(config).InsertBankFacilityGroup(data);
		}

		public bool UpdateBankFacilityGroup(BankFacilityGroupData data)
		{
			return UpdateBankFacilityGroup(data, checkConcurrency: false);
		}

		public bool UpdateBankFacilityGroup(BankFacilityGroupData data, bool checkConcurrency)
		{
			return new BankFacilityGroup(config).UpdateBankFacilityGroup(data);
		}

		public BankFacilityGroupData GetBankFacilityGroup()
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.GetBankFacilityGroup();
			}
		}

		public bool DeleteBankFacilityGroup(string groupID)
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.DeleteBankFacilityGroup(groupID);
			}
		}

		public BankFacilityGroupData GetBankFacilityGroupByID(string id)
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.GetBankFacilityGroupByID(id);
			}
		}

		public DataSet GetBankFacilityGroupByFields(params string[] columns)
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.GetBankFacilityGroupByFields(columns);
			}
		}

		public DataSet GetBankFacilityGroupByFields(string[] ids, params string[] columns)
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.GetBankFacilityGroupByFields(ids, columns);
			}
		}

		public DataSet GetBankFacilityGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.GetBankFacilityGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetBankFacilityGroupList()
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.GetBankFacilityGroupList();
			}
		}

		public DataSet GetBankFacilityGroupComboList()
		{
			using (BankFacilityGroup bankFacilityGroup = new BankFacilityGroup(config))
			{
				return bankFacilityGroup.GetBankFacilityGroupComboList();
			}
		}
	}
}
