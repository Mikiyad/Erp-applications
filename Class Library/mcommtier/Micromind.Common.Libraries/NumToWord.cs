using System;
using System.Globalization;

namespace Micromind.Common.Libraries
{
	public class NumToWord
	{
		private static class FarsiAmountInWords
		{
			private static string[] yakan = new string[10]
			{
				"صفر",
				"یک",
				"دو",
				"سه",
				"چهار",
				"پنج",
				"شش",
				"هفت",
				"هشت",
				"نه"
			};

			private static string[] dahgan = new string[10]
			{
				"",
				"",
				"بیست",
				"سی",
				"چهل",
				"پنجاه",
				"شصت",
				"هفتاد",
				"هشتاد",
				"نود"
			};

			private static string[] dahyek = new string[10]
			{
				"ده",
				"یازده",
				"دوازده",
				"سیزده",
				"چهارده",
				"پانزده",
				"شانزده",
				"هفده",
				"هجده",
				"نوزده"
			};

			private static string[] sadgan = new string[10]
			{
				"",
				"یکصد",
				"دویست",
				"سیصد",
				"چهارصد",
				"پانصد",
				"ششصد",
				"هفتصد",
				"هشتصد",
				"نهصد"
			};

			private static string[] basex = new string[5]
			{
				"",
				"هزار",
				"میلیون",
				"میلیارد",
				"تریلیون"
			};

			private static string getnum3(int num3)
			{
				try
				{
					string text = "";
					int num4 = num3 % 100;
					int num5 = num3 / 100;
					if (num5 != 0)
					{
						text = sadgan[num5] + " و ";
					}
					if (num4 >= 10 && num4 <= 19)
					{
						return text + dahyek[checked(num4 - 10)];
					}
					int num6 = num4 / 10;
					if (num6 != 0)
					{
						text = text + dahgan[num6] + " و ";
					}
					int num7 = num4 % 10;
					if (num7 != 0)
					{
						text = text + yakan[num7] + " و ";
					}
					return text.Substring(0, checked(text.Length - 3));
				}
				catch
				{
					throw;
				}
			}

			public static string GetAmountInWordsFarsi(decimal amount)
			{
				checked
				{
					try
					{
						string text = amount.ToString();
						string text2 = "";
						if (text == "")
						{
							return "صفر";
						}
						if (text == "0")
						{
							return yakan[0];
						}
						text = text.PadLeft((unchecked(checked(text.Length - 1) / 3) + 1) * 3, '0');
						int num = unchecked(text.Length / 3) - 1;
						for (int i = 0; i <= num; i++)
						{
							int num2 = int.Parse(text.Substring(i * 3, 3));
							if (num2 != 0)
							{
								text2 = text2 + getnum3(num2) + " " + basex[num - i] + " و ";
							}
						}
						return text2.Substring(0, text2.Length - 3);
					}
					catch
					{
						return "Error";
					}
				}
			}
		}

		public static string GetNumInWords(decimal amount)
		{
			return GetNumInWords(SystemLanguages.English, amount);
		}

		public static string GetNumInWords(decimal amount, int decimalPoints)
		{
			return GetNumInWords(SystemLanguages.English, amount, decimalPoints);
		}

		public static string GetAmountInWordsFarsiNew(decimal amount)
		{
			return GetNumInWords(SystemLanguages.Farsi, amount);
		}

		public static string GetNumInWords(SystemLanguages language, decimal amount)
		{
			if (language == SystemLanguages.Farsi)
			{
				decimal decimals = GetDecimals(amount);
				if (decimals > 0m)
				{
					string amountInWordsFarsi = FarsiAmountInWords.GetAmountInWordsFarsi(Math.Floor(amount));
					amountInWordsFarsi += " ممیز ";
					amountInWordsFarsi += FarsiAmountInWords.GetAmountInWordsFarsi(decimals);
					switch (decimals.ToString().Length)
					{
					case 1:
						amountInWordsFarsi += " دهم ";
						break;
					case 2:
						amountInWordsFarsi += " صدم ";
						break;
					case 3:
						amountInWordsFarsi += " هزارم ";
						break;
					}
					return amountInWordsFarsi;
				}
				return FarsiAmountInWords.GetAmountInWordsFarsi(amount);
			}
			return GetNumInWordsEnglish(amount);
		}

		public static string GetNumInWords(SystemLanguages language, decimal amount, int decimalPoints)
		{
			if (language == SystemLanguages.Farsi)
			{
				decimal decimals = GetDecimals(amount);
				if (decimals > 0m)
				{
					string amountInWordsFarsi = FarsiAmountInWords.GetAmountInWordsFarsi(Math.Floor(amount));
					amountInWordsFarsi += " ممیز ";
					amountInWordsFarsi += FarsiAmountInWords.GetAmountInWordsFarsi(decimals);
					switch (decimals.ToString().Length)
					{
					case 1:
						amountInWordsFarsi += " دهم ";
						break;
					case 2:
						amountInWordsFarsi += " صدم ";
						break;
					case 3:
						amountInWordsFarsi += " هزارم ";
						break;
					}
					return amountInWordsFarsi;
				}
				return FarsiAmountInWords.GetAmountInWordsFarsi(amount);
			}
			return GetNumInWordsEnglish(amount, decimalPoints);
		}

		private static decimal GetDecimals(decimal number)
		{
			int num = 0;
			string text = number.ToString();
			num = text.IndexOf(".", 0, text.Length);
			if (num > 0)
			{
				return decimal.Parse(checked(text.Substring(num + 1, text.Length - num - 1)));
			}
			return 0m;
		}

		private static string GetNumInWordsEnglish(decimal number)
		{
			decimal num = default(decimal);
			number = decimal.Round(number, 2);
			string text = "";
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			if (number == 0m)
			{
				return "Zero";
			}
			if (number < 0m)
			{
				text = "Minus ";
				number *= -1m;
			}
			num = GetDecimals(number);
			num2 = decimal.Truncate(decimal.Divide(number, 1000000000m));
			decimal d = decimal.Remainder(number, 1000000000m);
			num3 = decimal.Truncate(decimal.Divide(d, 1000000m));
			decimal d2 = decimal.Remainder(d, 1000000m);
			num4 = decimal.Truncate(decimal.Divide(d2, 1000m));
			num5 = decimal.Remainder(d2, 1000m);
			if (num2 > 0m)
			{
				text = text + GetHundredsInWords(num2) + " Billion";
				if (num4 + num5 + num3 > 0m)
				{
					text += " ";
				}
			}
			if (num3 > 0m)
			{
				text = text + GetHundredsInWords(num3) + " Million";
				if (num4 + num5 > 0m)
				{
					text += " ";
				}
			}
			if (num4 > 0m)
			{
				text = text + GetHundredsInWords(num4) + " Thousand";
				if (num5 > 0m)
				{
					text += " ";
				}
			}
			if (num5 > 0m)
			{
				text += GetHundredsInWords(num5);
			}
			if (text == "")
			{
				return num.ToString() + "/100";
			}
			return text + " and " + num.ToString() + "/100";
		}

		private static string GetHundredsInWords(decimal number)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			num3 = decimal.Truncate(decimal.Divide(number, 100m));
			number = decimal.Remainder(number, 100m);
			num2 = decimal.Truncate(decimal.Divide(number, 10m));
			num = decimal.Truncate(decimal.Remainder(number, 10m));
			string text = "";
			if (num3 > 0m)
			{
				text = GetOnesString(num3) + " Hundred";
			}
			if (num3 > 1m && num2 + num == 0m)
			{
				return text;
			}
			if (text != "")
			{
				text += " ";
			}
			if (num2 * 10m + num < 20m)
			{
				return text + GetOnesString(num2 * 10m + num);
			}
			text += GetTensString(num2);
			if (num > 0m)
			{
				text += " ";
				text += GetOnesString(num);
			}
			return text;
		}

		private static string GetNumInWordsEnglishmodified(decimal number)
		{
			int num = 2;
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			if (!string.IsNullOrEmpty(currentCulture.NumberFormat.CurrencyDecimalDigits.ToString()))
			{
				num = currentCulture.NumberFormat.CurrencyDecimalDigits;
			}
			number = decimal.Round(number, num);
			string text = "";
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			if (number == 0m)
			{
				return "Zero";
			}
			if (number < 0m)
			{
				text = "Minus ";
				number *= -1m;
			}
			string text2 = GetDecimals(number).ToString();
			num2 = decimal.Truncate(decimal.Divide(number, 1000000000m));
			decimal d = decimal.Remainder(number, 1000000000m);
			num3 = decimal.Truncate(decimal.Divide(d, 1000000m));
			decimal d2 = decimal.Remainder(d, 1000000m);
			num4 = decimal.Truncate(decimal.Divide(d2, 1000m));
			num5 = decimal.Remainder(d2, 1000m);
			if (num2 > 0m)
			{
				text = text + GetHundredsInWords(num2) + " Billion";
				if (num4 + num5 + num3 > 0m)
				{
					text += " ";
				}
			}
			if (num3 > 0m)
			{
				text = text + GetHundredsInWords(num3) + " Million";
				if (num4 + num5 > 0m)
				{
					text += " ";
				}
			}
			if (num4 > 0m)
			{
				text = text + GetHundredsInWords(num4) + " Thousand";
				if (num5 > 0m)
				{
					text += " ";
				}
			}
			if (num5 > 0m)
			{
				text += GetHundredsInWords(num5);
			}
			string text3 = "/100";
			switch (num)
			{
			case 1:
				text3 = "/10";
				break;
			case 2:
				text3 = "/100";
				break;
			case 3:
				text3 = "/1000";
				break;
			case 4:
				text3 = "/10000";
				break;
			}
			if (text == "")
			{
				return text2.ToString() + text3;
			}
			return text + " and " + text2.ToString() + text3;
		}

		private static string GetNumInWordsEnglish(decimal number, int decimalPoints)
		{
			int num = 2;
			_ = CultureInfo.CurrentCulture;
			if (decimalPoints > 0)
			{
				num = decimalPoints;
			}
			number = decimal.Round(number, num);
			string text = "";
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			if (number == 0m)
			{
				return "Zero";
			}
			if (number < 0m)
			{
				text = "Minus ";
				number *= -1m;
			}
			string text2 = GetDecimals(number).ToString();
			num2 = decimal.Truncate(decimal.Divide(number, 1000000000m));
			decimal d = decimal.Remainder(number, 1000000000m);
			num3 = decimal.Truncate(decimal.Divide(d, 1000000m));
			decimal d2 = decimal.Remainder(d, 1000000m);
			num4 = decimal.Truncate(decimal.Divide(d2, 1000m));
			num5 = decimal.Remainder(d2, 1000m);
			if (num2 > 0m)
			{
				text = text + GetHundredsInWords(num2) + " Billion";
				if (num4 + num5 + num3 > 0m)
				{
					text += " ";
				}
			}
			if (num3 > 0m)
			{
				text = text + GetHundredsInWords(num3) + " Million";
				if (num4 + num5 > 0m)
				{
					text += " ";
				}
			}
			if (num4 > 0m)
			{
				text = text + GetHundredsInWords(num4) + " Thousand";
				if (num5 > 0m)
				{
					text += " ";
				}
			}
			if (num5 > 0m)
			{
				text += GetHundredsInWords(num5);
			}
			string text3 = "/100";
			switch (num)
			{
			case 1:
				text3 = "/10";
				break;
			case 2:
				text3 = "/100";
				break;
			case 3:
				text3 = "/1000";
				break;
			case 4:
				text3 = "/10000";
				break;
			}
			if (text == "")
			{
				return text2.ToString() + text3;
			}
			return text + " and " + text2.ToString() + text3;
		}

		private static string GetOnesString(decimal number)
		{
			switch (int.Parse(number.ToString()))
			{
			case 0:
				return "";
			case 1:
				return "One";
			case 2:
				return "Two";
			case 3:
				return "Three";
			case 4:
				return "Four";
			case 5:
				return "Five";
			case 6:
				return "Six";
			case 7:
				return "Seven";
			case 8:
				return "Eight";
			case 9:
				return "Nine";
			case 10:
				return "Ten";
			case 11:
				return "Eleven";
			case 12:
				return "Twelve";
			case 13:
				return "Thirteen";
			case 14:
				return "Fourteen";
			case 15:
				return "Fifteen";
			case 16:
				return "Sixteen";
			case 17:
				return "Seventeen";
			case 18:
				return "Eighteen";
			case 19:
				return "Nineteen";
			default:
				return "";
			}
		}

		private static string GetTensString(decimal number)
		{
			switch (int.Parse(number.ToString()))
			{
			case 2:
				return "Twenty";
			case 3:
				return "Thirty";
			case 4:
				return "Fourty";
			case 5:
				return "Fifty";
			case 6:
				return "Sixty";
			case 7:
				return "Seventy";
			case 8:
				return "Eighty";
			case 9:
				return "Ninety";
			default:
				return "";
			}
		}
	}
}
