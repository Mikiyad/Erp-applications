using Micromind.DataCaches.Accounts;
using Micromind.DataCaches.Bankings;
using Micromind.DataCaches.Employees;
using Micromind.DataCaches.Inventory;
using Micromind.DataCaches.Others;

namespace Micromind.DataCaches.Libraries
{
	public class Global
	{
		public static void ResetAllCaches()
		{
			AllAccounts.Reset();
			SysDocTypes.Reset();
			Banks.Reset();
			Micromind.DataCaches.Employees.Employees.Reset();
			Brands.Reset();
			Categories.Reset();
			Items.Reset();
			Manufacturers.Reset();
			PriceLevels.Reset();
			PrintTemplates.Reset();
			ShippingMethods.Reset();
			Terms.Reset();
		}
	}
}
