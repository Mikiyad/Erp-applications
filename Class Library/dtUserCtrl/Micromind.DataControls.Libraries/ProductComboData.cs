using Micromind.Common.Data;

namespace Micromind.DataControls.Libraries
{
	public class ProductComboData
	{
		private string name;

		private string description;

		private string onHand;

		private string unitName;

		private string subUnitName;

		private decimal unitPrice;

		private decimal costPrice;

		private decimal averageCost;

		private int id;

		private int unitID;

		private int subUnitID;

		private ItemTypes itemType;

		private float factor;

		private CostMethods costMethod;

		private bool isInventoryPart;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string UnitName
		{
			get
			{
				return unitName;
			}
			set
			{
				unitName = value;
			}
		}

		public string SubUnitName
		{
			get
			{
				return subUnitName;
			}
			set
			{
				subUnitName = value;
			}
		}

		public string OnHand
		{
			get
			{
				return onHand;
			}
			set
			{
				onHand = value;
			}
		}

		public decimal AverageCost
		{
			get
			{
				return averageCost;
			}
			set
			{
				averageCost = value;
			}
		}

		public CostMethods CostMethod
		{
			get
			{
				return costMethod;
			}
			set
			{
				costMethod = value;
			}
		}

		public decimal CostPrice
		{
			get
			{
				return costPrice;
			}
			set
			{
				costPrice = value;
			}
		}

		public int ID
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public ItemTypes ItemType
		{
			get
			{
				return itemType;
			}
			set
			{
				itemType = value;
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

		public int SubUnitID
		{
			get
			{
				return subUnitID;
			}
			set
			{
				subUnitID = value;
			}
		}

		public bool IsInventoryPart
		{
			get
			{
				return isInventoryPart;
			}
			set
			{
				isInventoryPart = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		public decimal UnitPrice
		{
			get
			{
				return unitPrice;
			}
			set
			{
				unitPrice = value;
			}
		}

		public float Factor
		{
			get
			{
				return factor;
			}
			set
			{
				factor = value;
			}
		}

		public ProductComboData()
		{
		}

		public ProductComboData(string name, int id)
		{
			this.name = name;
			this.id = id;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
