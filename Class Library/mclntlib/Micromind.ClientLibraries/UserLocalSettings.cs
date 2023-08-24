using System.Drawing;
using System.Drawing.Printing;

namespace Micromind.ClientLibraries
{
	public class UserLocalSettings
	{
		public static string PayrollPrintAfterSaving
		{
			get
			{
				return Global.GetCompanyRegistryValue("PayrollPrintAfterSaving").ToString();
			}
			set
			{
				Global.SetCompanyRegistryValue("PayrollPrintAfterSaving", value);
			}
		}

		public static int CustomerInvoiceCustomerNoteID
		{
			get
			{
				return int.Parse(Global.CompanySettings.GetSetting("CustomerInvoiceCustomerNoteID", -1).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("CustomerInvoiceCustomerNoteID", value);
			}
		}

		public static bool HomePageReminderVisible
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("HomePageReminderVisible", false).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("HomePageReminderVisible", value);
			}
		}

		public static bool HomePageNotesVisible
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("HomePageNotesVisible", false).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("HomePageNotesVisible", value);
			}
		}

		public static int ReportTopMargin
		{
			get
			{
				return int.Parse(Global.CompanySettings.GetSetting("ReportTopMargin", 0).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("ReportTopMargin", value);
			}
		}

		public static int ReportLeftMargin
		{
			get
			{
				return int.Parse(Global.CompanySettings.GetSetting("ReportLeftMargin", 0).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("ReportLeftMargin", value);
			}
		}

		public static int DefaultReportPrintTemplate
		{
			get
			{
				return int.Parse(Global.CompanySettings.GetSetting("DefaultReportPrintTemplate", -1).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("DefaultReportPrintTemplate", value);
			}
		}

		public static Color ReportPrintGridLineColor
		{
			get
			{
				int num = -1;
				num = int.Parse(Global.CompanySettings.GetSetting("ReportPrintGridLineColor", int.MinValue).ToString());
				if (num != int.MinValue)
				{
					return Color.FromArgb(num);
				}
				return Color.FromArgb(221, 229, 242);
			}
			set
			{
				Global.CompanySettings.SaveSetting("ReportPrintGridLineColor", value.ToArgb().ToString());
			}
		}

		public static bool PrintReportGridLines
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("PrintReportGridLines", false).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("PrintReportGridLines", value);
			}
		}

		public static bool IsPrintReportLandscape
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("IsPrintReportLandscape", false).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("IsPrintReportLandscape", value);
			}
		}

		public static string ReportPrinterName
		{
			get
			{
				PrintDocument printDocument = new PrintDocument();
				return Global.CompanySettings.GetSetting("ReportPrinterName", printDocument.PrinterSettings.PrinterName).ToString();
			}
			set
			{
				Global.CompanySettings.SaveSetting("ReportPrinterName", value);
			}
		}

		public static bool ReportPrintNegativeInRed
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("ReportPrintNegativeInRed", false).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("ReportPrintNegativeInRed", value);
			}
		}

		public static string ReportPrintFooterNote
		{
			get
			{
				return Global.CompanySettings.GetSetting("ReportPrintFooterNote", "").ToString();
			}
			set
			{
				Global.CompanySettings.SaveSetting("ReportPrintFooterNote", value);
			}
		}
	}
}
