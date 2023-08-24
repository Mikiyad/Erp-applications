using System;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class ClassPayment
	{
		private int bankAccountID;

		private string checkNumber;

		private DateTime checkDate;

		private int paymentMethod;

		private decimal receivedAmount;

		private bool isCheck;

		public bool IsCheck
		{
			get
			{
				return isCheck;
			}
			set
			{
				isCheck = value;
			}
		}

		public int BankAccountID
		{
			get
			{
				return bankAccountID;
			}
			set
			{
				bankAccountID = value;
			}
		}

		public decimal ReceivedAmount
		{
			get
			{
				return receivedAmount;
			}
			set
			{
				receivedAmount = value;
			}
		}

		public string CheckNumber
		{
			get
			{
				return checkNumber;
			}
			set
			{
				checkNumber = value;
			}
		}

		public DateTime CheckDate
		{
			get
			{
				return checkDate;
			}
			set
			{
				checkDate = value;
			}
		}

		public int PaymentMethodID
		{
			get
			{
				return paymentMethod;
			}
			set
			{
				paymentMethod = value;
			}
		}
	}
}
