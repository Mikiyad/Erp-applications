using System;

namespace Micromind.DataControls.Libraries
{
	public class InvoiceComboItem
	{
		private int id;

		private int partnerID;

		private DateTime date;

		private string number;

		private string partnerName;

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

		public int PartnerID
		{
			get
			{
				return partnerID;
			}
			set
			{
				partnerID = value;
			}
		}

		public DateTime Date
		{
			get
			{
				return date;
			}
			set
			{
				date = value;
			}
		}

		public string Number
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

		public string PartnerName
		{
			get
			{
				return partnerName;
			}
			set
			{
				partnerName = value;
			}
		}
	}
}
