namespace Micromind.Data.Libraries
{
	internal class StoreQty
	{
		private float quantity;

		private int storeID;

		public float QuantityOnhand
		{
			get
			{
				return quantity;
			}
			set
			{
				quantity = value;
			}
		}

		public int StoreID
		{
			get
			{
				return storeID;
			}
			set
			{
				storeID = value;
			}
		}
	}
}
