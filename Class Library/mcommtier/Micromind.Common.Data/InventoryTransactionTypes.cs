namespace Micromind.Common.Data
{
	public enum InventoryTransactionTypes : byte
	{
		Purchase = 1,
		Sale,
		CustomerCreditMemo,
		VendorCreditMemo,
		Transfer,
		Adjustment,
		NewItemAdjustment,
		Assembly,
		ConsignOut,
		ConsignIn,
		ConsignOutSettlement,
		ConsignInSettlement,
		ConsignOutReturn,
		ConsignInReturn,
		JobInventoryIssue,
		JobInventoryReturn,
		InventoryNonSale,
		OpeningInventory,
		Damage,
		W3PLGRN,
		W3PLDelivery,
		GarmentRental,
		GarmentRentalReturn,
		WorkOrderInventoryIssue,
		WorkOrderInventoryReturn,
		InventoryDismantle,
		Production
	}
}
