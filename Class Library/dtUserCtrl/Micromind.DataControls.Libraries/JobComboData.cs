namespace Micromind.DataControls.Libraries
{
	public class JobComboData
	{
		private string name = "";

		private string customerName = "";

		private int customerID = -2;

		private int id;

		public string CustomerName
		{
			get
			{
				return customerName;
			}
			set
			{
				customerName = value;
			}
		}

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

		public int CustomerID
		{
			get
			{
				return customerID;
			}
			set
			{
				customerID = value;
			}
		}

		public JobComboData()
		{
		}

		public JobComboData(string name, int id)
		{
			this.name = name;
			this.id = id;
		}

		public JobComboData(string name, int id, string customerName, int customerID)
		{
			this.name = name;
			this.id = id;
			this.customerID = customerID;
			this.customerName = customerName;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
