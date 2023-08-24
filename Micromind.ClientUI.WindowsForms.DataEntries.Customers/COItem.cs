namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class COItem
	{
		private string number;

		private int locationID;

		private int productID;

		private int orderID;

		private int coDetailsID;

		private int unitID;

		public string CONumber
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}

		public int ProductID
		{
			get
			{
				return productID;
			}
			set
			{
				productID = value;
			}
		}

		public int LocationID
		{
			get
			{
				return locationID;
			}
			set
			{
				locationID = value;
			}
		}

		public int UnitID
		{
			get
			{
				return unitID;
			}
			set
			{
				unitID = value;
			}
		}

		public int OrderDetailsID
		{
			get
			{
				return coDetailsID;
			}
			set
			{
				coDetailsID = value;
			}
		}

		public int OrderID
		{
			get
			{
				return orderID;
			}
			set
			{
				orderID = value;
			}
		}

		public override string ToString()
		{
			return productID.ToString();
		}
	}
}
