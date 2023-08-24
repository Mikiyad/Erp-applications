using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TaxSystem : MarshalByRefObject, ITaxSystem, IDisposable
	{
		private Config config;

		public TaxSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTax(TaxData data)
		{
			return new Tax(config).InsertTax(data);
		}

		public bool UpdateTax(TaxData data)
		{
			return UpdateTax(data, checkConcurrency: false);
		}

		public bool UpdateTax(TaxData data, bool checkConcurrency)
		{
			return new Tax(config).UpdateTax(data);
		}

		public TaxData GetTax()
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTax();
			}
		}

		public bool DeleteTax(string groupID)
		{
			using (Tax tax = new Tax(config))
			{
				return tax.DeleteTax(groupID);
			}
		}

		public TaxData GetTaxByID(string id)
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTaxByID(id);
			}
		}

		public DataSet GetTaxClassList(string id)
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTaxClassList(id);
			}
		}

		public DataSet GetTaxByFields(params string[] columns)
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTaxByFields(columns);
			}
		}

		public DataSet GetTaxByFields(string[] ids, params string[] columns)
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTaxByFields(ids, columns);
			}
		}

		public DataSet GetTaxByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTaxByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetTaxList()
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTaxList();
			}
		}

		public DataSet GetTaxComboList()
		{
			using (Tax tax = new Tax(config))
			{
				return tax.GetTaxComboList();
			}
		}
	}
}
