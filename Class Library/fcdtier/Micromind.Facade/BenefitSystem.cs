using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BenefitSystem : MarshalByRefObject, IBenefitSystem, IDisposable
	{
		private Config config;

		public BenefitSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBenefit(BenefitData data)
		{
			return new Benefit(config).InsertBenefit(data);
		}

		public bool UpdateBenefit(BenefitData data)
		{
			return UpdateBenefit(data, checkConcurrency: false);
		}

		public bool UpdateBenefit(BenefitData data, bool checkConcurrency)
		{
			return new Benefit(config).UpdateBenefit(data);
		}

		public BenefitData GetBenefit()
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.GetBenefit();
			}
		}

		public bool DeleteBenefit(string groupID)
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.DeleteBenefit(groupID);
			}
		}

		public BenefitData GetBenefitByID(string id)
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.GetBenefitByID(id);
			}
		}

		public DataSet GetBenefitByFields(params string[] columns)
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.GetBenefitByFields(columns);
			}
		}

		public DataSet GetBenefitByFields(string[] ids, params string[] columns)
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.GetBenefitByFields(ids, columns);
			}
		}

		public DataSet GetBenefitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.GetBenefitByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetBenefitList()
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.GetBenefitList();
			}
		}

		public DataSet GetBenefitComboList()
		{
			using (Benefit benefit = new Benefit(config))
			{
				return benefit.GetBenefitComboList();
			}
		}
	}
}
