using Micromind.Common.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPhysicalStockEntrySystem
	{
		bool CreatePhysicalStockEntry(PhysicalStockEntryData PhysicalStockEntryData, bool isUpdate);

		PhysicalStockEntryData GetPhysicalStockEntryByID(string DocNumber);

		bool DeletePhysicalStockEntry(string DocNumber);

		string GetNextDocNumber();
	}
}
