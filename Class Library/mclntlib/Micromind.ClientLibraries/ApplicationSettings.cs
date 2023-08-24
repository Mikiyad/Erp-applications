using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	public sealed class ApplicationSettings
	{
		public sealed class Preferences
		{
			public sealed class General
			{
				public static bool UseOrderReserveSystem
				{
					get
					{
						string key = "UseOrderReserveSystem";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "UseOrderReserveSystem";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool UseQuickSort
				{
					get
					{
						string key = "UseQuickSort";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "UseQuickSort";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool ShowFullPathInFlatView
				{
					get
					{
						string key = "ShowFullPathInFlatView";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "ShowFullPathInFlatView";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool PlaySoundOnAlarms
				{
					get
					{
						string key = "PlaySoundOnAlarms";
						return bool.Parse(Global.CompanySettings.GetSetting(key, false).ToString());
					}
					set
					{
						string key = "PlaySoundOnAlarms";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool UseLastEnteredDate
				{
					get
					{
						string key = "UseLastEnteredDate";
						return bool.Parse(Global.CompanySettings.GetSetting(key, false).ToString());
					}
					set
					{
						string key = "UseLastEnteredDate";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static string AlarmSoundPath
				{
					get
					{
						string key = "AlarmSoundPath";
						return Global.CompanySettings.GetSetting(key, "").ToString();
					}
					set
					{
						string key = "AlarmSoundPath";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static byte CurrencyNumberOfDecimal
				{
					get
					{
						int currencyDecimalDigits = Application.CurrentCulture.NumberFormat.CurrencyDecimalDigits;
						string key = "CurrencyNumberOfDecimal";
						return byte.Parse(Global.CompanySettings.GetSetting(key, currencyDecimalDigits).ToString());
					}
					set
					{
						string key = "CurrencyNumberOfDecimal";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static byte NumbersNumberOfDecimal
				{
					get
					{
						int numberDecimalDigits = Application.CurrentCulture.NumberFormat.NumberDecimalDigits;
						string key = "NumbersNumberOfDecimal";
						return byte.Parse(Global.CompanySettings.GetSetting(key, numberDecimalDigits).ToString());
					}
					set
					{
						string key = "NumbersNumberOfDecimal";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static NegativeNumberFormats NegativeNumberFormat
				{
					get
					{
						string key = "NegativeNumberFormat";
						return (NegativeNumberFormats)byte.Parse(Global.CompanySettings.GetSetting(key, 0).ToString());
					}
					set
					{
						string key = "NegativeNumberFormat";
						Global.CompanySettings.SaveSetting(key, checked((byte)value));
					}
				}

				public static LeadingZeros LeadingZero
				{
					get
					{
						string key = "LeadingZero";
						return (LeadingZeros)byte.Parse(Global.CompanySettings.GetSetting(key, 0).ToString());
					}
					set
					{
						string key = "LeadingZero";
						Global.CompanySettings.SaveSetting(key, checked((byte)value));
					}
				}

				public static bool DisplayConfirmOnExit
				{
					get
					{
						string key = "DisplayConfirmOnExit";
						return bool.Parse(Global.CompanySettings.GetSetting(key, false).ToString());
					}
					set
					{
						string key = "DisplayConfirmOnExit";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool PressingEnterMovesBetweenFields
				{
					get
					{
						string key = "PressingEnterMovesBetweenFields";
						return bool.Parse(Global.CompanySettings.GetSetting(key, false).ToString());
					}
					set
					{
						string key = "PressingEnterMovesBetweenFields";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}
			}

			public sealed class View
			{
				public sealed class Lists
				{
					public static bool ShowToolTipOnLists
					{
						get
						{
							string key = "ShowToolTipOnLists";
							return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
						}
						set
						{
							string key = "ShowToolTipOnLists";
							Global.CompanySettings.SaveSetting(key, value);
						}
					}

					public static bool UseAlternatingColor
					{
						get
						{
							string key = "UseAlternatingColor";
							return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
						}
						set
						{
							string key = "UseAlternatingColor";
							Global.CompanySettings.SaveSetting(key, value);
						}
					}

					public static Color SelectedRowBackColor
					{
						get
						{
							string key = "SelectedRowBackColor";
							return Color.FromArgb(int.Parse(Global.CompanySettings.GetSetting(key, Color.FromArgb(255, 240, 194).ToArgb()).ToString()));
						}
						set
						{
							string key = "SelectedRowBackColor";
							Global.CompanySettings.SaveSetting(key, value.ToArgb());
						}
					}

					public static Color SelectedRowTextColor
					{
						get
						{
							string key = "SelectedRowTextColor";
							return Color.FromArgb(int.Parse(Global.CompanySettings.GetSetting(key, Color.Black.ToArgb()).ToString()));
						}
						set
						{
							string key = "SelectedRowTextColor";
							Global.CompanySettings.SaveSetting(key, value.ToArgb());
						}
					}

					public static Color AlternatingBackColor
					{
						get
						{
							string key = "AlternatingBackColor";
							return Color.FromArgb(int.Parse(Global.CompanySettings.GetSetting(key, Color.FromArgb(240, 245, 250).ToArgb()).ToString()));
						}
						set
						{
							string key = "AlternatingBackColor";
							Global.CompanySettings.SaveSetting(key, value.ToArgb());
						}
					}
				}
			}

			public sealed class Accounts
			{
				public static bool UseAccountNumbers
				{
					get
					{
						string key = "UseAccountNumbers";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "UseAccountNumbers";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}
			}

			public sealed class Customers
			{
				public static byte InvoiceDueDateDifference
				{
					get
					{
						string key = "InvoiceDueDateDifference";
						return byte.Parse(Global.CompanySettings.GetSetting(key, 30).ToString());
					}
					set
					{
						string key = "InvoiceDueDateDifference";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}
			}

			public sealed class Vendors
			{
				public static byte BillsDueDateDifference
				{
					get
					{
						string key = "BillsDueDateDifference";
						return byte.Parse(Global.CompanySettings.GetSetting(key, 30).ToString());
					}
					set
					{
						string key = "BillsDueDateDifference";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}
			}

			public sealed class Reports
			{
				public static bool AgeFromDueDate
				{
					get
					{
						string key = "AgeFromDueDate";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "AgeFromDueDate";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool UseReportsAlternatingColor
				{
					get
					{
						string key = "UseReportsAlternatingColor";
						return bool.Parse(Global.CompanySettings.GetSetting(key, false).ToString());
					}
					set
					{
						string key = "UseReportsAlternatingColor";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static Color ReportsAlternatingBackColor
				{
					get
					{
						string key = "ReportsAlternatingBackColor";
						return Color.FromArgb(int.Parse(Global.CompanySettings.GetSetting(key, Color.FromArgb(240, 245, 250).ToArgb()).ToString()));
					}
					set
					{
						string key = "ReportsAlternatingBackColor";
						Global.CompanySettings.SaveSetting(key, value.ToArgb());
					}
				}

				public static Color ReportsSelectionRectColor
				{
					get
					{
						string key = "ReportsSelectionRectColor";
						return Color.FromArgb(int.Parse(Global.CompanySettings.GetSetting(key, Color.DarkBlue.ToArgb()).ToString()));
					}
					set
					{
						string key = "ReportsSelectionRectColor";
						Global.CompanySettings.SaveSetting(key, value.ToArgb());
					}
				}
			}
		}

		public sealed class WarningMessages
		{
			public sealed class Inventory
			{
				public static bool InventoryIncomeAccountType
				{
					get
					{
						string key = "WarningMessagesInventoryIncomeAccountType";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "WarningMessagesInventoryIncomeAccountType";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool InventoryAssetAccountType
				{
					get
					{
						string key = "WarningMessagesInventoryAssetAccountType";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "WarningMessagesInventoryAssetAccountType";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}

				public static bool InventoryCOGSAccountType
				{
					get
					{
						string key = "WarningMessagesInventoryCOGSAccountType";
						return bool.Parse(Global.CompanySettings.GetSetting(key, true).ToString());
					}
					set
					{
						string key = "WarningMessagesInventoryCOGSAccountType";
						Global.CompanySettings.SaveSetting(key, value);
					}
				}
			}
		}
	}
}
