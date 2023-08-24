using System.Globalization;
using System.Threading;

namespace Micromind.ClientLibraries
{
	public class Format
	{
		public const int MultiLevelComboIndent = 4;

		public static byte PriceNumberOfFixedDecimals;

		public static byte QuantityNumberOfFixedDecimals;

		public static NegativeNumberFormats NegativeNumberFormat;

		public static LeadingZeros LeadingZero;

		private static string decimalSymbol;

		private static string digitGroupingSymbol;

		public static string DecimalSymbol => decimalSymbol;

		public static string DigitGroupingSymbol => digitGroupingSymbol;

		public static string TotalMoney => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls();

		public static string TextBoxMoney => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls();

		public static string TextBoxQuantity => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls() + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls() + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls();

		public static string UnitPriceFormat => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls();

		public static string TotalAmountFormat
		{
			get
			{
				string text = "";
				for (int i = 0; i < Global.CurDecimalPoints; i = checked(i + 1))
				{
					text += "0";
				}
				return "#" + DigitGroupingSymbol + "##0" + DecimalSymbol + text + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##0" + DecimalSymbol + text + GetNegativeNumberFormatLast() + ";0" + DecimalSymbol + text;
			}
		}

		public static string GridAmountFormat
		{
			get
			{
				string text = "#,##0.";
				for (int i = 0; i < Global.CurDecimalPoints; i = checked(i + 1))
				{
					text += "0";
				}
				return text;
			}
		}

		public static string PercentageFormat => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + "00;" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + "00" + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + "00";

		public static string QuantityFormat => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls() + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls() + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls();

		public static string GridTaxAmountFormat
		{
			get
			{
				string text = "#,##0.";
				for (int i = 0; i < 5; i = checked(i + 1))
				{
					text += "0";
				}
				return text;
			}
		}

		public static string ListViewMoney => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls() + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + GetPriceFixedDeciamls();

		public static string ListViewQuantity => "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls() + ";" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls() + GetNegativeNumberFormatLast() + ";" + GetLeadingZero() + DecimalSymbol + GetQuantityFixedDeciamls();

		public static byte RoundDigit => 2;

		public static string NumberFormat => "#" + DigitGroupingSymbol + "##0" + DecimalSymbol + "#;" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##0" + DecimalSymbol + "##" + GetNegativeNumberFormatLast() + ";0";

		public static string NumberFormatK => "#" + DigitGroupingSymbol + "##0," + DecimalSymbol + "#K;" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##0," + DecimalSymbol + "#K" + GetNegativeNumberFormatLast() + ";0";

		public static string NumberFormatM => "#" + DigitGroupingSymbol + "##0,," + DecimalSymbol + "#M;" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##0,," + DecimalSymbol + "#M" + GetNegativeNumberFormatLast() + ";0";

		public static string NumberFormatB => "#" + DigitGroupingSymbol + "##0,,," + DecimalSymbol + "#B;" + GetNegativeNumberFormatFirst() + "#" + DigitGroupingSymbol + "##0,,," + DecimalSymbol + "#B" + GetNegativeNumberFormatLast() + ";0";

		public static string DocumentNumberFormat => "00000000";

		static Format()
		{
			PriceNumberOfFixedDecimals = ApplicationSettings.Preferences.General.CurrencyNumberOfDecimal;
			QuantityNumberOfFixedDecimals = 0;
			NegativeNumberFormat = ApplicationSettings.Preferences.General.NegativeNumberFormat;
			LeadingZero = ApplicationSettings.Preferences.General.LeadingZero;
			decimalSymbol = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
			digitGroupingSymbol = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator;
		}

		public static void LoadFormats()
		{
			try
			{
				PriceNumberOfFixedDecimals = ApplicationSettings.Preferences.General.CurrencyNumberOfDecimal;
			}
			catch
			{
			}
			try
			{
				QuantityNumberOfFixedDecimals = 0;
			}
			catch
			{
			}
			try
			{
				NegativeNumberFormat = ApplicationSettings.Preferences.General.NegativeNumberFormat;
			}
			catch
			{
			}
			try
			{
				LeadingZero = ApplicationSettings.Preferences.General.LeadingZero;
			}
			catch
			{
			}
		}

		private static string GetPriceFixedDeciamls()
		{
			string text = "";
			checked
			{
				for (byte b = 0; b < PriceNumberOfFixedDecimals; b = (byte)(unchecked((uint)b) + 1u))
				{
					text += "0";
				}
				while (text.Length < 5)
				{
					text += "#";
				}
				return text;
			}
		}

		private static string GetQuantityFixedDeciamls()
		{
			string text = "";
			checked
			{
				for (byte b = 0; b < QuantityNumberOfFixedDecimals; b = (byte)(unchecked((uint)b) + 1u))
				{
					text += "0";
				}
				while (text.Length < 4)
				{
					text += "#";
				}
				return text;
			}
		}

		private static string GetLeadingZero()
		{
			if (LeadingZero == LeadingZeros.WithZero)
			{
				return "0";
			}
			return "";
		}

		private static string GetNegativeNumberFormatFirst()
		{
			switch (NegativeNumberFormat)
			{
			case NegativeNumberFormats.Paranteses:
				return "(";
			case NegativeNumberFormats.Neg1:
				return "-";
			case NegativeNumberFormats.Neg2:
				return "";
			case NegativeNumberFormats.NegSpace1:
				return "- ";
			case NegativeNumberFormats.NegSpace2:
				return "";
			default:
				return "(";
			}
		}

		private static string GetNegativeNumberFormatLast()
		{
			switch (NegativeNumberFormat)
			{
			case NegativeNumberFormats.Paranteses:
				return ")";
			case NegativeNumberFormats.Neg1:
				return "";
			case NegativeNumberFormats.Neg2:
				return "-";
			case NegativeNumberFormats.NegSpace1:
				return "";
			case NegativeNumberFormats.NegSpace2:
				return " -";
			default:
				return ")";
			}
		}

		public static string IsValidTransactionAmount(string amountValue)
		{
			decimal num = default(decimal);
			try
			{
				num = decimal.Parse(amountValue, NumberStyles.Any);
			}
			catch
			{
				return "Invalid amount.Please enter a numeric value.";
			}
			if (PublicFunctions.GetNumberOfDecimals(amountValue) > PriceNumberOfFixedDecimals)
			{
				return "Please enter an amount with a maximum decimal numbers of:" + PriceNumberOfFixedDecimals.ToString();
			}
			if (num > new decimal(9999999999L))
			{
				return "The number you have entered is larger than the maximum allowed.Please enter a smaller value.\nThe value must be between '0' and '" + new decimal(9999999999L).ToString(TextBoxMoney) + "'.";
			}
			if (num < 0m)
			{
				return "Invalid value. Please enter a positive value.";
			}
			return "";
		}

		public static string IsValidAmount(string amountValue, bool onlyPositive)
		{
			string text = null;
			text = ((!onlyPositive) ? ("'" + amountValue + "' is an invalid amount.\nPlease enter a number no more than 8 digits to the left of decimal and " + PriceNumberOfFixedDecimals.ToString() + " digits to the right.") : ("'" + amountValue + "' is an invalid amount.\nPlease enter a positive number no more than 8 digits to the left of decimal and " + PriceNumberOfFixedDecimals.ToString() + " digits to the right."));
			decimal d;
			try
			{
				d = decimal.Parse(amountValue, NumberStyles.Any);
			}
			catch
			{
				return text;
			}
			try
			{
				if (d > new decimal(999999999999L))
				{
					return text;
				}
				if (d < new decimal(-999999999999L))
				{
					return text;
				}
				if (onlyPositive && d < 0m)
				{
					return text;
				}
			}
			catch
			{
				return text;
			}
			return null;
		}

		public static string RemoveChar(string str, char c)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			while (true)
			{
				int num = str.IndexOf(c);
				if (num < 0)
				{
					break;
				}
				str = str.Remove(num, 1);
			}
			return str;
		}

		public static string IsValidAmount(string amountValue)
		{
			string result = "'" + amountValue + "'   is an invalid amount.\nPlease enter a positive number no more than 8 digits to the left of decimal and " + PriceNumberOfFixedDecimals.ToString() + " digits to the right.";
			decimal d;
			try
			{
				d = decimal.Parse(amountValue, NumberStyles.Any);
			}
			catch
			{
				return result;
			}
			try
			{
				if (d > new decimal(999999999999L))
				{
					return result;
				}
				if (d < 0m)
				{
					return result;
				}
			}
			catch
			{
				return result;
			}
			return null;
		}

		public static string IsValidQuantity(string quantityValue)
		{
			string result = "'" + quantityValue + "'   is an invalid quantity.\nPlease enter a positive number no more than 7 digits to the left of decimal and " + PriceNumberOfFixedDecimals.ToString() + " digits to the right.";
			decimal d;
			try
			{
				d = decimal.Parse(quantityValue, NumberStyles.Any);
			}
			catch
			{
				return result;
			}
			try
			{
				if (d > 99999999m)
				{
					return result;
				}
				if (d < 0m)
				{
					return result;
				}
			}
			catch
			{
				return result;
			}
			return null;
		}

		public static string IsValidTotalAmount(string amountValue, string quantityValue)
		{
			string result = "The value you have entered makes the total too big.Please enter a smaller value.";
			decimal d;
			decimal d2;
			try
			{
				d = decimal.Parse(amountValue, NumberStyles.Any);
				d2 = decimal.Parse(quantityValue, NumberStyles.Any);
			}
			catch
			{
				return "Invalid Value";
			}
			try
			{
				if (d * d2 > new decimal(999999999999L))
				{
					return result;
				}
			}
			catch
			{
				return result;
			}
			return null;
		}

		public static bool StartsWithDigit(string str)
		{
			if (str.Trim().Length == 0)
			{
				return false;
			}
			if (char.IsDigit(str.ToCharArray()[0]))
			{
				return true;
			}
			return false;
		}

		public static string AddSingleQuote(string str)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			int num;
			for (num = 0; num != str.Length; num = checked(num + 2))
			{
				num = str.IndexOf('\'', num);
				if (num < 0)
				{
					break;
				}
				str = str.Insert(num, "'");
			}
			return str;
		}

		public static string IsValidFactor(string factor)
		{
			string result = "'" + factor + "'   is an invalid factor.\nPlease enter a positive number no more than 4 digits to the left of decimal and " + ((byte)5).ToString() + " digits to the right.";
			decimal d;
			try
			{
				d = decimal.Parse(factor, NumberStyles.Any);
			}
			catch
			{
				return result;
			}
			try
			{
				if (d > 9999.99999m)
				{
					return result;
				}
				if (d < 0m)
				{
					return result;
				}
			}
			catch
			{
				return result;
			}
			return null;
		}
	}
}
