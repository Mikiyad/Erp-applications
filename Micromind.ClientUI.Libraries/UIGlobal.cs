using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.Others.HelpSupports;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.UISupport;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public class UIGlobal
	{
		public enum ReminderType
		{
			OverdueBills,
			OverdueInvoices,
			InventoryToReorder,
			EmployeesToPay,
			RecievedChecks,
			PaidChecks,
			EmployeeDocuments
		}

		public static object workerThreadSycRoot = new object();

		public static string CashRegisterID = "";

		public static Hashtable OpenNoteForms = new Hashtable();

		public static bool RemindOverdueBills
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindOverdueBills", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindOverdueBills", value);
			}
		}

		public static bool RemindOverdueInvoices
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindOverdueInvoices", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindOverdueInvoices", value);
			}
		}

		public static bool RemindReorderItems
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindReorderItems", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindReorderItems", value);
			}
		}

		public static bool RemindEmployeeDocuments
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindEmployeeDocuments", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindEmployeeDocuments", value);
			}
		}

		public static bool RemindEmployeeVisa
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindEmployeeVisa", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindEmployeeVisa", value);
			}
		}

		public static bool RemindEmployeeDrivingLicense
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindEmployeeDrivingLicense", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindEmployeeDrivingLicense", value);
			}
		}

		public static bool RemindEmployeePassport
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindEmployeePassport", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindEmployeePassport", value);
			}
		}

		public static bool RemindRecievedChecks
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindRecievedChecks", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindRecievedChecks", value);
			}
		}

		public static bool RemindPaidChecks
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindPaidChecks", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindPaidChecks", value);
			}
		}

		public static bool RemindEmployeesToPay
		{
			get
			{
				return bool.Parse(Global.CompanySettings.GetSetting("RemindEmployeesToPay", true).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("RemindEmployeesToPay", value);
			}
		}

		public static byte OverdueBillsDays
		{
			get
			{
				return byte.Parse(Global.CompanySettings.GetSetting("OverdueBillsDays", 3).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("OverdueBillsDays", value);
			}
		}

		public static byte PaidChecksDays
		{
			get
			{
				return byte.Parse(Global.CompanySettings.GetSetting("PaidChecksDays", 3).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("PaidChecksDays", value);
			}
		}

		public static byte OverdueInvoicesDays
		{
			get
			{
				return byte.Parse(Global.CompanySettings.GetSetting("OverdueInvoicesDays", 3).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("OverdueInvoicesDays", value);
			}
		}

		public static byte EmployeesToPayDays
		{
			get
			{
				return byte.Parse(Global.CompanySettings.GetSetting("EmployeesToPayDays", 3).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("EmployeesToPayDays", value);
			}
		}

		public static byte ReceivedChecksDays
		{
			get
			{
				return byte.Parse(Global.CompanySettings.GetSetting("ReceivedChecksDays", 3).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("ReceivedChecksDays", value);
			}
		}

		public static byte EmployeeDocumentsDays
		{
			get
			{
				return byte.Parse(Global.CompanySettings.GetSetting("EmployeeDocumentsDays", 3).ToString());
			}
			set
			{
				Global.CompanySettings.SaveSetting("EmployeeDocumentsDays", value);
			}
		}

		public static DialogResult ShowOkDynamicWarningDialog(string message, string registryKey)
		{
			return ShowDynamicWarningDialog(message, registryKey, MessageBoxButtons.OK);
		}

		public static DialogResult ShowYesNoDynamicWarningDialog(string message, string registryKey)
		{
			return ShowDynamicWarningDialog(message, registryKey, MessageBoxButtons.YesNo);
		}

		public static DialogResult ShowDynamicWarningDialog(string message, string registryKey, MessageBoxButtons messageBoxButtons)
		{
			ErrorHelperDialogResult errorHelperDialogResult = new ErrorHelperDialogResult();
			bool flag = false;
			object setting = Global.CompanySettings.GetSetting(registryKey, false);
			if (setting != null)
			{
				try
				{
					flag = Convert.ToBoolean(setting.ToString());
				}
				catch
				{
				}
			}
			if (!flag)
			{
				using (WarningMessageDialog warningMessageDialog = new WarningMessageDialog())
				{
					errorHelperDialogResult = warningMessageDialog.ShowDialog(Navigator.MainForm, message, messageBoxButtons);
					Global.CompanySettings.SaveSetting(registryKey, errorHelperDialogResult.DontShowAgain);
				}
			}
			return errorHelperDialogResult.Answer;
		}

		public static void ApplySettings()
		{
			MMTextBox.SimulateEnterAsTab = ApplicationSettings.Preferences.General.PressingEnterMovesBetweenFields;
			Navigator.ApplyChildsPreferences();
		}

		public static ApplicationUpdateConfig GetCurrentClientVersion()
		{
			ApplicationUpdateConfig applicationUpdateConfig = new ApplicationUpdateConfig();
			string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\updateinfo.axo";
			if (File.Exists(path))
			{
				StreamReader streamReader = new StreamReader(path);
				applicationUpdateConfig.ProductName = streamReader.ReadLine();
				applicationUpdateConfig.ProductVersion = streamReader.ReadLine();
				streamReader.Close();
			}
			return applicationUpdateConfig;
		}

		public static string LoadCurrentPOSLocation()
		{
			return Global.CurrentPOSLocationID = Factory.POSCashRegisterSystem.GetCurrentPOSLocation(Global.CurrentCashRegisterID);
		}

		public static int GetActiveShiftNumber()
		{
			if (Global.CurrentPOSLocationID == "")
			{
				return -1;
			}
			return Factory.POSShiftSystem.GetCurrentOpenShiftID(Global.CurrentCashRegisterID);
		}

		public static int CreateNewShiftNumber(decimal openingCash)
		{
			if (Global.CurrentPOSLocationID == "")
			{
				return -1;
			}
			if (Factory.POSShiftSystem.GetCurrentOpenShiftID(Global.CurrentCashRegisterID) == -1)
			{
				POSShiftData pOSShiftData = new POSShiftData();
				DataRow dataRow = pOSShiftData.POSShiftTable.Rows.Add();
				dataRow["LocationID"] = Global.CurrentPOSLocationID;
				dataRow["BatchID"] = GetActiveBatchNumber();
				dataRow["CashRegisterID"] = Global.CurrentCashRegisterID;
				dataRow["OpeningCash"] = openingCash;
				dataRow.EndEdit();
				Factory.POSShiftSystem.CreatePOSShift(pOSShiftData);
				return Factory.POSShiftSystem.GetCurrentOpenShiftID(Global.CurrentCashRegisterID);
			}
			throw new CompanyException("There is already an open shift for this register.", 2001);
		}

		public static int GetActiveBatchNumber()
		{
			if (Global.CurrentPOSLocationID == "")
			{
				return -1;
			}
			return Factory.POSBatchSystem.GetCurrentOpenBatchID(Global.CurrentPOSLocationID);
		}

		public static int GetOrCreateBatchNumber()
		{
			if (Global.CurrentPOSLocationID == "")
			{
				return -1;
			}
			int currentOpenBatchID = Factory.POSBatchSystem.GetCurrentOpenBatchID(Global.CurrentPOSLocationID);
			if (currentOpenBatchID == -1)
			{
				POSBatchData pOSBatchData = new POSBatchData();
				DataRow dataRow = pOSBatchData.POSBatchTable.Rows.Add();
				dataRow["LocationID"] = Global.CurrentPOSLocationID;
				dataRow.EndEdit();
				Factory.POSBatchSystem.CreatePOSBatch(pOSBatchData);
				return Factory.POSBatchSystem.GetCurrentOpenBatchID(Global.CurrentPOSLocationID);
			}
			return currentOpenBatchID;
		}

		public static decimal CalculateRowTax(UltraGridRow row, string taxColumnKey, decimal amount, decimal subtotal, decimal tradeDiscount)
		{
			return CalculateRowTax(row, taxColumnKey, amount, subtotal, tradeDiscount, priceIncludeTax: false);
		}

		public static decimal CalculateRowTax(UltraGridRow row, string taxColumnKey, decimal amount, decimal subtotal, decimal tradeDiscount, bool priceIncludeTax)
		{
			return CalculateRowTax(row, taxColumnKey, 0m, amount, subtotal, tradeDiscount, priceIncludeTax);
		}

		public static decimal CalculateRowTax(UltraGridRow row, string taxColumnKey, decimal cost, decimal amount, decimal subtotal, decimal tradeDiscount, bool priceIncludeTax)
		{
			decimal num = default(decimal);
			if (row.Cells["Tax"].Tag == null)
			{
				row.Cells[taxColumnKey].Value = 0;
				return num;
			}
			foreach (DataRow row2 in ((TaxTransactionData)row.Cells[taxColumnKey].Tag).TaxDetailTable.Rows)
			{
				row2.BeginEdit();
				decimal num2 = decimal.Parse(row2["TaxRate"].ToString());
				TaxCalculationMethods taxCalculationMethods = (TaxCalculationMethods)int.Parse(row2["CalculationMethod"].ToString());
				if (priceIncludeTax)
				{
					amount /= 1m + num2 / 100m;
				}
				if (tradeDiscount > 0m && subtotal > 0m)
				{
					amount -= tradeDiscount / subtotal * amount;
				}
				decimal num3 = default(decimal);
				switch (taxCalculationMethods)
				{
				case TaxCalculationMethods.PercentageOfCost:
					num3 = Math.Round(cost * num2 / 100m, 4);
					break;
				case TaxCalculationMethods.PercentageOfProfit:
					num3 = Math.Round((amount - cost) * num2 / 100m, 4);
					break;
				default:
					num3 = Math.Round(amount * num2 / 100m, 4);
					break;
				}
				row2["TaxAmount"] = num3;
				num += num3;
				row2.EndEdit();
			}
			row.Cells[taxColumnKey].Value = num;
			return num;
		}

		public static decimal CalculateFieldTax(TextBox textBox, decimal amount, bool priceIncludeTax)
		{
			decimal result = default(decimal);
			if (textBox.Tag == null)
			{
				textBox.Text = 0.ToString(Format.TotalAmountFormat);
				return result;
			}
			foreach (DataRow row in ((TaxTransactionData)textBox.Tag).TaxDetailTable.Rows)
			{
				row.BeginEdit();
				decimal num = decimal.Parse(row["TaxRate"].ToString());
				int.Parse(row["CalculationMethod"].ToString());
				if (priceIncludeTax)
				{
					amount /= 1m + num / 100m;
				}
				decimal num2 = default(decimal);
				num2 = Math.Round(amount * num / 100m, 4);
				row["TaxAmount"] = num2;
				result += num2;
				row.EndEdit();
			}
			textBox.Text = result.ToString(Format.TotalAmountFormat);
			return result;
		}
	}
}
