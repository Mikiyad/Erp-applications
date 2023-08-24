using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;

namespace Micromind.Facade
{
	public sealed class PhysicalStockEntrySystem : MarshalByRefObject, IPhysicalStockEntrySystem, IDisposable
	{
		private Config config;

		public PhysicalStockEntrySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePhysicalStockEntry(PhysicalStockEntryData data, bool isUpdate)
		{
			return new PhysicalStockEntry(config).InsertUpdatePhysicalStockEntry(data, isUpdate);
		}

		public PhysicalStockEntryData GetPhysicalStockEntryByID(string DocNumber)
		{
			return new PhysicalStockEntry(config).GetPhysicalStockEntryByID(DocNumber);
		}

		public bool DeletePhysicalStockEntry(string DocNumber)
		{
			return new PhysicalStockEntry(config).DeletePhysicalStockEntry(DocNumber);
		}

		public string GetNextDocNumber()
		{
			return new PhysicalStockEntry(config).GetNextDocNumber();
		}
	}
}
