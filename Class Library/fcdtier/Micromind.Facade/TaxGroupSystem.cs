using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TaxGroupSystem : MarshalByRefObject, ITaxGroupSystem, IDisposable
	{
		private Config config;

		public TaxGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTaxGroup(TaxGroupData data)
		{
			return new TaxGroup(config).InsertTaxGroup(data);
		}

		public bool UpdateTaxGroup(TaxGroupData data)
		{
			return UpdateTaxGroup(data, checkConcurrency: false);
		}

		public bool UpdateTaxGroup(TaxGroupData data, bool checkConcurrency)
		{
			return new TaxGroup(config).UpdateTaxGroup(data);
		}

		public TaxGroupData GetTaxGroup()
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxGroup();
			}
		}

		public bool DeleteTaxGroup(string groupID)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.DeleteTaxGroup(groupID);
			}
		}

		public TaxGroupData GetTaxGroupByID(string id)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxGroupByID(id);
			}
		}

		public DataSet GetTaxClassList(string id)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxClassList(id);
			}
		}

		public DataSet GetTaxGroupByFields(params string[] columns)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxByFields(columns);
			}
		}

		public DataSet GetTaxGroupByFields(string[] ids, params string[] columns)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxByFields(ids, columns);
			}
		}

		public DataSet GetTaxGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetTaxGroupList()
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxGroupList();
			}
		}

		public DataSet GetTaxGroupDetailList()
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxGroupDetailList();
			}
		}

		public DataSet GetTaxGroupComboList()
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxGroupComboList();
			}
		}

		public DataSet GetTaxDatabasedonproductID(string id, string BasedonID)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxDatabasedonproductID(id, BasedonID);
			}
		}

		public DataSet GetTaxDatabasedonvendorID(string id, string BasedonID)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxDatabasedonvendorID(id, BasedonID);
			}
		}

		public DataSet GetTaxDatabasedonCustomerID(string id, string BasedonID)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxDatabasedonCustomerID(id, BasedonID);
			}
		}

		public DataSet GetTaxDatabasedonGroupID(string id)
		{
			using (TaxGroup taxGroup = new TaxGroup(config))
			{
				return taxGroup.GetTaxDatabasedonGroupID(id);
			}
		}
	}
}
