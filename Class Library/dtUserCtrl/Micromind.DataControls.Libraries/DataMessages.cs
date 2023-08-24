using Micromind.ClientLibraries;

namespace Micromind.DataControls.Libraries
{
	public sealed class DataMessages
	{
		public static string GeneralInformation => GetMessage(0);

		public static string Cash => GetMessage(9);

		public static string Bank => GetMessage(10);

		public static string AccountReceivable => GetMessage(11);

		public static string OtherCurrentAsset => GetMessage(12);

		public static string FixedAsset => GetMessage(13);

		public static string OtherAsset => GetMessage(14);

		public static string AccountsPayable => GetMessage(15);

		public static string CreditCard => GetMessage(16);

		public static string OtherCurrentLiability => GetMessage(17);

		public static string LongTermLiability => GetMessage(18);

		public static string Equity => GetMessage(19);

		public static string Income => GetMessage(20);

		public static string CostofGoodsSold => GetMessage(21);

		public static string Expense => GetMessage(22);

		public static string OtherIncome => GetMessage(23);

		public static string OtherExpense => GetMessage(24);

		public static string CannotAddRecord => GetMessage(44);

		public static string ErrorInSearch => GetMessage(45);

		public static string Banks => GetMessage(49);

		public static string Currencies => GetMessage(50);

		public static string Customers => GetMessage(51);

		public static string Employees => GetMessage(52);

		public static string Items => GetMessage(53);

		public static string Jobs => GetMessage(54);

		public static string Locations => GetMessage(55);

		public static string Contacts => GetMessage(56);

		public static string PaymentMethods => GetMessage(57);

		public static string PriceLevels => GetMessage(58);

		public static string Shippers => GetMessage(59);

		public static string PaymentTerms => GetMessage(60);

		public static string Units => GetMessage(61);

		public static string Vendors => GetMessage(62);

		public static string Accounts => GetMessage(63);

		private DataMessages()
		{
		}

		public static string GetMessage(int messageIndex)
		{
			string message = Messages.GetMessage(messageIndex);
			string text = DataControlTranslator.Translators.GetText(message);
			if (text == string.Empty || text == null)
			{
				return message;
			}
			return text;
		}
	}
}
