using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DeductionSystem : MarshalByRefObject, IDeductionSystem, IDisposable
	{
		private Config config;

		public DeductionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDeduction(DeductionData data)
		{
			return new Deduction(config).InsertDeduction(data);
		}

		public bool UpdateDeduction(DeductionData data)
		{
			return UpdateDeduction(data, checkConcurrency: false);
		}

		public bool UpdateDeduction(DeductionData data, bool checkConcurrency)
		{
			return new Deduction(config).UpdateDeduction(data);
		}

		public DeductionData GetDeduction()
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.GetDeduction();
			}
		}

		public bool DeleteDeduction(string groupID)
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.DeleteDeduction(groupID);
			}
		}

		public DeductionData GetDeductionByID(string id)
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.GetDeductionByID(id);
			}
		}

		public DataSet GetDeductionByFields(params string[] columns)
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.GetDeductionByFields(columns);
			}
		}

		public DataSet GetDeductionByFields(string[] ids, params string[] columns)
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.GetDeductionByFields(ids, columns);
			}
		}

		public DataSet GetDeductionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.GetDeductionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDeductionList()
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.GetDeductionList();
			}
		}

		public DataSet GetDeductionComboList()
		{
			using (Deduction deduction = new Deduction(config))
			{
				return deduction.GetDeductionComboList();
			}
		}
	}
}
