using Micromind.Common.Data;
using Micromind.Utilities.AppUpdater;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	
	public sealed class Global
	{
		public static bool IsTrialLimitReached;

		public static bool isUserAdmin;

		public static string RECORD_NEW_TRANSACTION_QUESTION_MESSAGE;

		public static bool IsAxolonAgent;

		public static bool IsNotifierVisible;

		public static ArrayList AlarmedNotes;

		private static string baseCurrencyID;

		private static int curDecimalPoints;

		public static bool HomePageLoaded;

		public static string CurrentLocationID;

		public static string CurrentPOSLocationID;

		public static string CurrentCashRegisterID;

		public static string DefaultCustomerID;

		public static string DefaultCustomerName;

		private static DataRow defaultStore;

		private static DataRow defaultEmployee;

		private static DataRow defaultARAccount;

		private static Image companyLogo;

		private static PrintDocument printDoc;

		private static PrintDocument reportPrintDoc;

		private static string defaultStoreName;

		private static int currentStoreID;

		public static int addressNumber;

		private static DateTime fiscalDate;

		private static PaymentTermData termData;

		private static CompanyInformationData companyInformationData;

		private static string currentUser;

		private static string currentPassword;

		private static ConnectionStatus connectionStatus;

		private static string currentDBName;

		private static string currentServerName;

		private static bool isExternalDB;

		private static string externalDBUserName;

		private static string externalDBUserPassword;

		public static ApplicationTypes applicationType;

		private static int currentPortNumber;

		private static string currentInstanceName;

		public const string REGISTRY_OPTIONS_FIELD = "Options";

		private static int currentUserID;

		private static bool reconciliationInProcess;

		private static LocalSettings companySettings;

		public static string DefaultLocationID;

		private static bool isMSWordExist;

		private static DateTime closingBookDate;

		private static string closingBookPassword;

		private static bool isClosingBookCalled;

		private static string currencySign;

		public static CultureInfo CurrentCulture => Thread.CurrentThread.CurrentCulture;

		public static Calendar CurrentCalendar => CurrentCulture.Calendar;

		public static bool IsUserAdmin
		{
			get
			{
				if (CurrentUser.ToLower().Equals("sa") || isUserAdmin)
				{
					return true;
				}
				return false;
			}
			set
			{
				isUserAdmin = value;
			}
		}

		public static DateTime ClosingBookDate
		{
			get
			{
				return closingBookDate;
			}
			set
			{
				closingBookDate = value;
			}
		}

		public static string ClosingBookPassword
		{
			get
			{
				return closingBookPassword;
			}
			set
			{
				closingBookPassword = value;
			}
		}

		public static bool IsMultiUser
		{
			get
			{
				try
				{
					RegistryHelper registryHelper = new RegistryHelper(Registry.CurrentUser, "Options", writable: false);
					bool result = bool.Parse(registryHelper.GetStringValue("IsMultiUser", "false"));
					registryHelper.Dispose();
					return result;
				}
				catch
				{
				}
				return false;
			}
			set
			{
				try
				{
					RegistryHelper registryHelper = new RegistryHelper(Registry.CurrentUser, "Options", writable: true);
					registryHelper.SetValue("IsMultiUser", value);
					registryHelper.Dispose();
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public static LocalSettings CompanySettings
		{
			get
			{
				if (CurrentDatabaseName == null)
				{
					return GlobalSettings;
				}
				if (companySettings == null)
				{
					companySettings = new LocalSettings(CurrentDatabaseName);
				}
				return companySettings;
			}
		}

		public static LocalSettings GlobalSettings => LocalSettings.GlobalSettings;

		public static bool IsMSWordExist => isMSWordExist;

		public static string RegistryStoreID
		{
			get
			{
				return GetCompanyRegistryValue("StoreID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("StoreID", value);
			}
		}

		public static string BaseCurrencyID
		{
			get
			{
				return baseCurrencyID;
			}
			set
			{
				baseCurrencyID = value;
			}
		}

		public static int CurDecimalPoints
		{
			get
			{
				if (curDecimalPoints == 255)
				{
					return 2;
				}
				return curDecimalPoints;
			}
			set
			{
				curDecimalPoints = value;
			}
		}

		public static string RegistryPCategoryHierarchical
		{
			get
			{
				return GetCompanyRegistryValue("RegistryPCategoryHierarchical").ToString();
			}
			set
			{
				SetCompanyRegistryValue("RegistryPCategoryHierarchical", value);
			}
		}

		public static string RegistryProductHierarchical
		{
			get
			{
				return GetCompanyRegistryValue("RegistryProductHierarchical").ToString();
			}
			set
			{
				SetCompanyRegistryValue("RegistryProductHierarchical", value);
			}
		}

		public static string RegistryAccountsHierarchical
		{
			get
			{
				return GetCompanyRegistryValue("RegistryAccountsHierarchical").ToString();
			}
			set
			{
				SetCompanyRegistryValue("RegistryAccountsHierarchical", value);
			}
		}

		public static string RegistrySalesDiscountID
		{
			get
			{
				return GetCompanyRegistryValue("SalesDiscountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesDiscountID", value);
			}
		}

		public static string RegistryPurchaseDiscountID
		{
			get
			{
				return GetCompanyRegistryValue("PurchaseDiscountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PurchaseDiscountID", value);
			}
		}

		public static string RegistryUseLogo
		{
			get
			{
				return GetCompanyRegistryValue("UseLogo").ToString();
			}
			set
			{
				SetCompanyRegistryValue("UseLogo", value);
			}
		}

		public static bool RegistryViewCustomerStatementMonthWise
		{
			get
			{
				try
				{
					return bool.Parse(GetCompanyRegistryValue("VCSMW").ToString());
				}
				catch
				{
					return false;
				}
			}
			set
			{
				SetCompanyRegistryValue("VCSMW", value);
			}
		}

		public static bool RegistryViewCustomerStatementPDC
		{
			get
			{
				try
				{
					return bool.Parse(GetCompanyRegistryValue("VCSPDC").ToString());
				}
				catch
				{
					return false;
				}
			}
			set
			{
				SetCompanyRegistryValue("VCSPDC", value);
			}
		}

		public static string RegistryPrintOnOption
		{
			get
			{
				return GetCompanyRegistryValue("PrintOnOption").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PrintOnOption", value);
			}
		}

		public static string RegistryLogo
		{
			get
			{
				return GetCompanyRegistryValue("Logo").ToString();
			}
			set
			{
				SetCompanyRegistryValue("Logo", value);
			}
		}

		public static string RegistryHomeImage
		{
			get
			{
				return GetCompanyRegistryValue("HomeImage").ToString();
			}
			set
			{
				SetCompanyRegistryValue("HomeImage", value);
			}
		}

		public static string RegistryPaymentMethodID
		{
			get
			{
				return GetCompanyRegistryValue("PaymentMethodID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PaymentMethodID", value);
			}
		}

		public static string RegistryEmployeeID
		{
			get
			{
				return GetCompanyRegistryValue("EmployeeID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("EmployeeID", value);
			}
		}

		public static string RegistryDepositToAccountID
		{
			get
			{
				return GetCompanyRegistryValue("DepositToAccountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("DepositToAccountID", value);
			}
		}

		public static string RegistryPayFromAccountID
		{
			get
			{
				return GetCompanyRegistryValue("PayFromAccountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PayFromAccountID", value);
			}
		}

		public static string RegistryVBPrintTopMargin
		{
			get
			{
				return GetCompanyRegistryValue("VBPrintTopMargin").ToString();
			}
			set
			{
				SetCompanyRegistryValue("VBPrintTopMargin", value);
			}
		}

		public static string RegistryPayBillPrint
		{
			get
			{
				return GetCompanyRegistryValue("PayBillPrint").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PayBillPrint", value);
			}
		}

		public static string RegistryReceivePaymentPrint
		{
			get
			{
				return GetCompanyRegistryValue("ReceivePaymentPrint").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ReceivePaymentPrint", value);
			}
		}

		public static string RegistryPayBillPrintHeader
		{
			get
			{
				return GetCompanyRegistryValue("PayBillPrintHeader").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PayBillPrintHeader", value);
			}
		}

		public static string RegistryPayBillPrintDetails
		{
			get
			{
				return GetCompanyRegistryValue("PayBillPrintDetails").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PayBillPrintDetails", value);
			}
		}

		public static string RegistryPayBillPrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("PayBillPrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PayBillPrintTitle", value);
			}
		}

		public static string RegistryReceiptPrintHeader
		{
			get
			{
				return GetCompanyRegistryValue("ReceiptPrintHeader").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ReceiptPrintHeader", value);
			}
		}

		public static string RegistryReceiptPrintDetails
		{
			get
			{
				return GetCompanyRegistryValue("ReceiptPrintDetails").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ReceiptPrintDetails", value);
			}
		}

		public static string RegistryReceiptPrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("ReceiptPrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ReceiptPrintTitle", value);
			}
		}

		public static string RegistryARID
		{
			get
			{
				return GetCompanyRegistryValue("ARID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ARID", value);
			}
		}

		public static Margins RegistryPrinterMargin
		{
			get
			{
				Margins margins = new Margins(125, 100, 50, 50);
				string text = RegistryGetOptionValue("PrinterMargin").ToString();
				if (text != null && text.Length > 0)
				{
					string[] array = text.Split(',');
					if (array.Length == 4)
					{
						try
						{
							margins.Left = int.Parse(array[0]);
							margins.Right = int.Parse(array[1]);
							margins.Top = int.Parse(array[2]);
							margins.Bottom = int.Parse(array[3]);
							return margins;
						}
						catch
						{
							return margins;
						}
					}
				}
				return margins;
			}
			set
			{
				string val = "";
				if (value != null)
				{
					val = value.Left.ToString() + "," + value.Right.ToString() + "," + value.Top.ToString() + "," + value.Bottom.ToString();
				}
				RegistrySetOptionValue("PrinterMargin", val);
			}
		}

		public static string RegistrySalesOrderPrintHeader
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderPrintHeader").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderPrintHeader", value);
			}
		}

		public static string RegistrySalesOrderPrintBillto
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderPrintBillto").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderPrintBillto", value);
			}
		}

		public static string RegistrySalesOrderPrintShipto
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderPrintShipto").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderPrintShipto", value);
			}
		}

		public static string RegistrySalesOrderPrintTable
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderPrintTable").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderPrintTable", value);
			}
		}

		public static string RegistrySalesOrderBilltoTitle
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderBilltoTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderBilltoTitle", value);
			}
		}

		public static string RegistrySalesOrderPrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderPrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderPrintTitle", value);
			}
		}

		public static string RegistrySalesOrderNote1
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderNote1").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderNote1", value);
			}
		}

		public static string RegistrySalesOrderNote2
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderNote2").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderNote2", value);
			}
		}

		public static string RegistrySalesOrderNote3
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderNote3").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderNote3", value);
			}
		}

		public static string RegistrySalesOrderPrintAfterSaving
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderPrintAfterSaving").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderPrintAfterSaving", value);
			}
		}

		public static string RegistryAPID
		{
			get
			{
				return GetCompanyRegistryValue("APID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("APID", value);
			}
		}

		public static bool RegistryPostReceivePayment
		{
			get
			{
				try
				{
					return bool.Parse(GetCompanyRegistryValue("PRP").ToString());
				}
				catch
				{
				}
				return true;
			}
			set
			{
				SetCompanyRegistryValue("PRP", value);
			}
		}

		public static bool RegistryPostVendorPayment
		{
			get
			{
				try
				{
					return bool.Parse(GetCompanyRegistryValue("PVP").ToString());
				}
				catch
				{
				}
				return true;
			}
			set
			{
				SetCompanyRegistryValue("PVP", value);
			}
		}

		public static string RegistryAccountsQuickSearchField
		{
			get
			{
				return GetCompanyRegistryValue("AccountsQuickSearchField").ToString();
			}
			set
			{
				SetCompanyRegistryValue("AccountsQuickSearchField", value);
			}
		}

		public static string RegistryItemsQuickSearchField
		{
			get
			{
				return GetCompanyRegistryValue("ItemsQuickSearchField").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ItemsQuickSearchField", value);
			}
		}

		public static string RegistryFreightInID
		{
			get
			{
				return GetCompanyRegistryValue("FreightInID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("FreightInID", value);
			}
		}

		public static string RegistryFreightOutID
		{
			get
			{
				return GetCompanyRegistryValue("FreightOutID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("FreightOutID", value);
			}
		}

		public static string RegistryPurchaseOrderPrintAfterSaving
		{
			get
			{
				return GetCompanyRegistryValue("PurchaseOrderPrintAfterSaving").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PurchaseOrderPrintAfterSaving", value);
			}
		}

		public static string RegistryAdjustAccountID
		{
			get
			{
				return GetCompanyRegistryValue("AdjustAccountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("AdjustAccountID", value);
			}
		}

		public static string RegistryItemDefaultStoreID
		{
			get
			{
				return GetCompanyRegistryValue("ItemDefaultStoreID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ItemDefaultStoreID", value);
			}
		}

		public static string RegistryTermID
		{
			get
			{
				return GetCompanyRegistryValue("TermID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("TermID", value);
			}
		}

		public static string RegistryCOGSID
		{
			get
			{
				return GetCompanyRegistryValue("COGS").ToString();
			}
			set
			{
				SetCompanyRegistryValue("COGS", value);
			}
		}

		public static string RegistryCashInvoiceBankAccountID
		{
			get
			{
				return GetCompanyRegistryValue("CashInvoiceBankAccountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("CashInvoiceBankAccountID", value);
			}
		}

		public static string RegistryBankAccountID
		{
			get
			{
				return GetCompanyRegistryValue("BankAccount").ToString();
			}
			set
			{
				SetCompanyRegistryValue("BankAccount", value);
			}
		}

		public static string RegistryAssetID
		{
			get
			{
				return GetCompanyRegistryValue("Asset").ToString();
			}
			set
			{
				SetCompanyRegistryValue("Asset", value);
			}
		}

		public static string RegistryDeliveryNoteDelByID
		{
			get
			{
				return GetCompanyRegistryValue("DNoteDeliveredBy").ToString();
			}
			set
			{
				SetCompanyRegistryValue("DNoteDeliveredBy", value);
			}
		}

		public static string RegistryDeliveryNoteShipperID
		{
			get
			{
				return GetCompanyRegistryValue("DNoteShipperID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("DNoteShipperID", value);
			}
		}

		public static string RegistryIncomeID
		{
			get
			{
				return GetCompanyRegistryValue("Income").ToString();
			}
			set
			{
				SetCompanyRegistryValue("Income", value);
			}
		}

		public static string RegistryInvoicePrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("InvoicePrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("InvoicePrintTitle", value);
			}
		}

		public static string RegistryCashInvoicePrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("CashInvoicePrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("CashInvoicePrintTitle", value);
			}
		}

		public static string RegistryDNotePrintAfterSaving
		{
			get
			{
				return GetCompanyRegistryValue("DNotePrintAfterSaving").ToString();
			}
			set
			{
				SetCompanyRegistryValue("DNotePrintAfterSaving", value);
			}
		}

		public static string RegistrySalesQuotePrintAfterSaving
		{
			get
			{
				return GetCompanyRegistryValue("SalesQuotePrintAfterSaving").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesQuotePrintAfterSaving", value);
			}
		}

		public static string RegistrySalesInvoicePrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoicePrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoicePrintTitle", value);
			}
		}

		public static string RegistrySalesInvoicePrintHeader
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoicePrintHeader").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoicePrintHeader", value);
			}
		}

		public static string RegistrySalesInvoicePrintTable
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoicePrintTable").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoicePrintTable", value);
			}
		}

		public static string RegistrySalesInvoicePrintBillto
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoicePrintBillto").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoicePrintBillto", value);
			}
		}

		public static bool RegistryReportShowGridLine
		{
			get
			{
				try
				{
					return bool.Parse(RegistryGetOptionValue("ShowGridLine").ToString());
				}
				catch
				{
					return true;
				}
			}
			set
			{
				RegistrySetOptionValue("ShowGridLine", value);
			}
		}

		public static string RegistrySalesInvoiceBilltoTitle
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoiceBilltoTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoiceBilltoTitle", value);
			}
		}

		public static string RegistrySalesInvoicePrintShipto
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoicePrintShipto").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoicePrintShipto", value);
			}
		}

		public static string RegistrySalesInvoiceNote3
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoiceNote3").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoiceNote3", value);
			}
		}

		public static string RegistrySalesInvoiceNote1
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoiceNote1").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoiceNote1", value);
			}
		}

		public static string RegistrySalesInvoiceNote2
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoiceNote2").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoiceNote2", value);
			}
		}

		public static string RegistrySalesInvoicePrintFooter
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoicePrintFooter").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoicePrintFooter", value);
			}
		}

		public static string RegistrySalesInvoiceCustomerSign
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoiceCustomerSign").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoiceCustomerSign", value);
			}
		}

		public static string RegistrySalesInvoiceCashRegisterSign
		{
			get
			{
				return GetCompanyRegistryValue("SalesInvoiceCashRegisterSign").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesInvoiceCashRegisterSign", value);
			}
		}

		public static string RegistrySalesOrderPrintFooter
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderPrintFooter").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderPrintFooter", value);
			}
		}

		public static string RegistrySalesOrderCustomerSign
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderCustomerSign").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderCustomerSign", value);
			}
		}

		public static string RegistrySalesOrderManagerSign
		{
			get
			{
				return GetCompanyRegistryValue("SalesOrderManagerSign").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesOrderManagerSign", value);
			}
		}

		public static string RegistryPONote1
		{
			get
			{
				return GetCompanyRegistryValue("PONote1").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PONote1", value);
			}
		}

		public static string RegistryPONote2
		{
			get
			{
				return GetCompanyRegistryValue("PONote2").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PONote2", value);
			}
		}

		public static string RegistryPONote3
		{
			get
			{
				return GetCompanyRegistryValue("PONote3").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PONote3", value);
			}
		}

		public static string RegistryPOPrintTable
		{
			get
			{
				return GetCompanyRegistryValue("POPrintTable").ToString();
			}
			set
			{
				SetCompanyRegistryValue("POPrintTable", value);
			}
		}

		public static string RegistrySalesQuotePrintFooter
		{
			get
			{
				return GetCompanyRegistryValue("SalesQuotePrintFooter").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesQuotePrintFooter", value);
			}
		}

		public static string RegistrySalesQuoteManagerSign
		{
			get
			{
				return GetCompanyRegistryValue("SalesQuoteManagerSign").ToString();
			}
			set
			{
				SetCompanyRegistryValue("SalesQuoteManagerSign", value);
			}
		}

		public static string RegistryPOPrintHeader
		{
			get
			{
				return GetCompanyRegistryValue("POPrintHeader").ToString();
			}
			set
			{
				SetCompanyRegistryValue("POPrintHeader", value);
			}
		}

		public static string RegistryPOPrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("POPrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("POPrintTitle", value);
			}
		}

		public static string RegistryItemTypeID
		{
			get
			{
				return GetCompanyRegistryValue("ItemTypeID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ItemTypeID", value);
			}
		}

		public static DataRow DefaultARAccount
		{
			get
			{
				return defaultARAccount;
			}
			set
			{
				defaultARAccount = value;
			}
		}

		public static ConnectionStatus ConStatus
		{
			get
			{
				if (connectionStatus == ConnectionStatus.Connected && CurrentDatabaseName == "master")
				{
					return ConnectionStatus.SQLConnected;
				}
				return connectionStatus;
			}
			set
			{
				if (value != connectionStatus)
				{
					connectionStatus = value;
					if (connectionStatus == ConnectionStatus.DisConnected)
					{
						Reset();
						SetOpenCompany("master");
					}
				}
			}
		}

		public static int PortNumber
		{
			get
			{
				return currentPortNumber;
			}
			set
			{
				currentPortNumber = value;
			}
		}

		public static bool IsExternalDB
		{
			get
			{
				try
				{
					isExternalDB = bool.Parse(GetRegistryOptionValue("IED", isEncrypt: true));
				}
				catch
				{
				}
				return isExternalDB;
			}
			set
			{
				isExternalDB = value;
				try
				{
					SaveRegistryOptionValue("IED", value.ToString(), isEncrypt: true);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public static string ExternalDBUserName
		{
			get
			{
				try
				{
					externalDBUserName = GetRegistryOptionValue("EDUN", isEncrypt: true);
				}
				catch
				{
				}
				return externalDBUserName;
			}
			set
			{
				externalDBUserName = value;
				try
				{
					SaveRegistryOptionValue("EDUN", value, isEncrypt: true);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public static string ExternalDBUserPassword
		{
			get
			{
				try
				{
					externalDBUserPassword = GetPassword("EDUP");
				}
				catch
				{
				}
				return externalDBUserPassword;
			}
			set
			{
				externalDBUserPassword = value;
				try
				{
					SavePassword(value, "EDUP");
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public static string ComputerName => Environment.MachineName;

		public static string CurrentServerName
		{
			get
			{
				try
				{
					RegistryHelper registryHelper = new RegistryHelper("Options", writable: false);
					currentServerName = registryHelper.GetStringValue("Server", Environment.MachineName);
					if (currentServerName.Trim() == string.Empty || currentServerName.Trim().ToLower() == "localhost")
					{
						currentServerName = Environment.MachineName;
					}
					registryHelper.Dispose();
				}
				catch
				{
				}
				return currentServerName;
			}
			set
			{
				if (!(currentServerName == value))
				{
					if (!GlobalRules.IsCorrectServerName(value))
					{
						throw new ApplicationException("Incorrect server name.");
					}
					currentServerName = value;
					try
					{
						RegistryHelper registryHelper = new RegistryHelper("Options", writable: true);
						registryHelper.SetValue("Server", value);
						registryHelper.Dispose();
					}
					catch (Exception e)
					{
						ErrorHelper.ProcessError(e);
					}
				}
			}
		}

		public static int CurrentPortNumber
		{
			get
			{
				try
				{
					RegistryHelper registryHelper = new RegistryHelper("Options", writable: false);
					currentPortNumber = int.Parse(registryHelper.GetStringValue("Port", currentPortNumber.ToString()));
					registryHelper.Dispose();
				}
				catch
				{
				}
				return currentPortNumber;
			}
			set
			{
				if (currentPortNumber != value)
				{
					currentPortNumber = value;
					try
					{
						RegistryHelper registryHelper = new RegistryHelper("Options", writable: true);
						registryHelper.SetValue("Port", value);
						registryHelper.Dispose();
					}
					catch (Exception e)
					{
						ErrorHelper.ProcessError(e);
					}
				}
			}
		}

		public static string CurrentInstanceName
		{
			get
			{
				try
				{
					if (IsAxolonAgent)
					{
						return currentInstanceName;
					}
					RegistryHelper registryHelper = new RegistryHelper("Options", writable: false);
					if (IsMultiUser)
					{
						currentInstanceName = registryHelper.GetStringValue("Server", Environment.MachineName + "\\Axolon");
					}
					else
					{
						currentInstanceName = registryHelper.GetStringValue("InstanceName", Environment.MachineName + "\\Axolon");
					}
					registryHelper.Dispose();
					string text = currentInstanceName.Trim().ToLower();
					if (text == "localhost")
					{
						currentInstanceName = Environment.MachineName;
					}
					else if (text.IndexOf("localhost\\") == 0)
					{
						currentInstanceName = Environment.MachineName + "\\" + text.Substring(10);
					}
				}
				catch
				{
				}
				return currentInstanceName;
			}
			set
			{
				if (currentInstanceName == value)
				{
					return;
				}
				if (IsAxolonAgent)
				{
					currentInstanceName = value;
					return;
				}
				if (!GlobalRules.IsCorrectServerName(value))
				{
					throw new ApplicationException("Incorrect instance name.");
				}
				currentInstanceName = value;
				try
				{
					RegistryHelper registryHelper = new RegistryHelper("Options", writable: true);
					registryHelper.SetValue("InstanceName", value);
					registryHelper.Dispose();
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public static string SingleUserInstanceName
		{
			get
			{
				try
				{
					RegistryHelper registryHelper = new RegistryHelper("Options", writable: false);
					_ = IsMultiUser;
					string stringValue = registryHelper.GetStringValue("InstanceName", Environment.MachineName + "\\Axolon");
					registryHelper.Dispose();
					stringValue = stringValue.Trim().ToLower();
					if (stringValue == "localhost")
					{
						stringValue = Environment.MachineName;
					}
					else if (stringValue.IndexOf("localhost\\") == 0)
					{
						stringValue = Environment.MachineName + "\\" + stringValue.Substring(10);
					}
					return stringValue;
				}
				catch
				{
					throw;
				}
			}
		}

		public static string CurrentDatabaseName
		{
			get
			{
				return currentDBName;
			}
			set
			{
				currentDBName = value;
			}
		}

		public static DataRow DefaultStore
		{
			get
			{
				return defaultStore;
			}
			set
			{
				defaultStore = value;
			}
		}

		public static string DefaultStoreName
		{
			get
			{
				try
				{
					if (defaultStoreName == null)
					{
						defaultStoreName = DefaultStore["StoreName"].ToString();
					}
				}
				catch
				{
				}
				return defaultStoreName;
			}
		}

		public static DataRow DefaultEmployee
		{
			get
			{
				return defaultEmployee;
			}
			set
			{
				defaultEmployee = value;
			}
		}

		public static CompanyInformationData CompanyInformation
		{
			get
			{
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return null;
				}
				if (companyInformationData == null)
				{
					try
					{
						companyInformationData = Factory.CompanyInformationSystem.GetCompanyInformation();
						if (companyInformationData != null && companyInformationData.CompanyInformationTable.Rows.Count == 0)
						{
							companyInformationData = null;
						}
					}
					catch
					{
						throw;
					}
				}
				return companyInformationData;
			}
			set
			{
				companyInformationData = value;
			}
		}

		public static int CurrentUserID
		{
			get
			{
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return -1;
				}
				try
				{
					_ = currentUserID;
					_ = -1;
					return currentUserID;
				}
				catch
				{
					currentUserID = -1;
					return -1;
				}
			}
		}

		public static string CurrentUser
		{
			get
			{
				return currentUser.ToLower();
			}
			set
			{
				currentUser = value;
			}
		}

		public static string CurrentPassword
		{
			get
			{
				return currentPassword;
			}
			set
			{
				currentPassword = value;
			}
		}

		public static string DefaultCurrencySign
		{
			get
			{
				try
				{
					if (currencySign == null)
					{
						int.Parse(CompanyInformation.CompanyInformationTable.Rows[0]["BaseCurrencyID"].ToString());
					}
					return currencySign;
				}
				catch
				{
					return "";
				}
			}
		}

		public static string CurrencySymbol => DefaultCurrencySign;

		public static object CurrentUserEmployeeID
		{
			get
			{
				object obj = DBNull.Value;
				try
				{
					obj = Factory.SecuritySystem.GetUserEmployeeID(CurrentUser);
				}
				catch
				{
				}
				if (obj == null)
				{
					obj = DBNull.Value;
				}
				return obj;
			}
		}

		public static string CurrentUserEmployeeName
		{
			get
			{
				_ = CurrentUserEmployeeID;
				return "";
			}
		}

		public static object CurrentUserStoreID
		{
			get
			{
				object value = DBNull.Value;
				if (value == null)
				{
					value = DBNull.Value;
				}
				return value;
			}
		}

		public static string PrintTemplateServerPath
		{
			get
			{
				try
				{
					return CompanyInformation.CompanyInformationTable.Rows[0]["TemplatePathServer"].ToString();
				}
				catch
				{
					return "";
				}
			}
		}

		public static string PrintTemplatePathFolder
		{
			get
			{
				try
				{
					return CompanyInformation.CompanyInformationTable.Rows[0]["TemplatePathFolder"].ToString();
				}
				catch
				{
					return "";
				}
			}
		}

		public static string CompanyName
		{
			get
			{
				string result = "";
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return result;
				}
				CompanyInformationData companyInformationData = null;
				try
				{
					companyInformationData = CompanyInformation;
				}
				catch
				{
					result = "";
				}
				if (companyInformationData != null && companyInformationData.CompanyInformationTable.Rows.Count > 0)
				{
					result = companyInformationData.CompanyInformationTable.Rows[0]["CompanyName"].ToString();
				}
				return result;
			}
		}

		public static string CompanyAddress
		{
			get
			{
				string result = "";
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return result;
				}
				CompanyInformationData companyInformation = CompanyInformation;
				if (companyInformation != null && companyInformation.CompanyInformationTable.Rows.Count > 0)
				{
					_ = companyInformation.CompanyInformationTable.Rows[0];
				}
				return result;
			}
		}

		public static string CompanyAddress2
		{
			get
			{
				string result = "";
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return result;
				}
				CompanyInformationData companyInformation = CompanyInformation;
				if (companyInformation != null && companyInformation.CompanyInformationTable.Rows.Count > 0)
				{
					_ = companyInformation.CompanyInformationTable.Rows[0];
				}
				return result;
			}
		}

		public static string CompanyAddress3
		{
			get
			{
				string result = "";
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return result;
				}
				CompanyInformationData companyInformation = CompanyInformation;
				if (companyInformation != null && companyInformation.CompanyInformationTable.Rows.Count > 0)
				{
					_ = companyInformation.CompanyInformationTable.Rows[0];
				}
				return result;
			}
		}

		public static bool ReconciliationInProcess
		{
			get
			{
				return reconciliationInProcess;
			}
			set
			{
				reconciliationInProcess = value;
			}
		}

		public static string RegistryRecInterestAccountID
		{
			get
			{
				return GetCompanyRegistryValue("RecInterestAccountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("RecInterestAccountID", value);
			}
		}

		public static string RegistryRecServiceChargeAccountID
		{
			get
			{
				return GetCompanyRegistryValue("RecServiceChargeAccountID").ToString();
			}
			set
			{
				SetCompanyRegistryValue("RecServiceChargeAccountID", value);
			}
		}

		public static string CompanyPhone
		{
			get
			{
				string result = "";
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return result;
				}
				CompanyInformationData companyInformation = CompanyInformation;
				if (companyInformation != null && companyInformation.CompanyInformationTable.Rows.Count > 0)
				{
					_ = companyInformation.CompanyInformationTable.Rows[0];
				}
				return result;
			}
		}

		public static int CompanyID
		{
			get
			{
				int result = -1;
				if (ConStatus == ConnectionStatus.DisConnected)
				{
					return result;
				}
				CompanyInformationData companyInformationData = null;
				try
				{
					companyInformationData = CompanyInformation;
				}
				catch
				{
					result = -1;
				}
				if (companyInformationData != null && companyInformationData.CompanyInformationTable.Rows.Count > 0)
				{
					result = int.Parse(companyInformationData.CompanyInformationTable.Rows[0]["CompanyID"].ToString());
				}
				return result;
			}
		}

		public static Image CompanyLogo
		{
			get
			{
				if (companyLogo != null)
				{
					return companyLogo;
				}
				Stream stream = null;
				try
				{
					stream = Factory.CompanyInformationSystem.GetLogo(CompanyID);
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
					companyLogo = null;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					companyLogo = null;
				}
				if (stream != null)
				{
					try
					{
						companyLogo = Image.FromStream(stream);
					}
					catch
					{
					}
					stream.Close();
				}
				return companyLogo;
			}
			set
			{
				companyLogo = value;
			}
		}

		public static DateTime FiscalDate
		{
			get
			{
				if (ConStatus == ConnectionStatus.Connected && fiscalDate == DateTime.MinValue)
				{
					fiscalDate = Factory.CompanyInformationSystem.GetFiscalStartDate();
				}
				return fiscalDate;
			}
		}

		public static int CurrentStoreID
		{
			get
			{
				try
				{
					if (currentStoreID == -2)
					{
						currentStoreID = int.Parse(DefaultStore["StoreID"].ToString());
					}
				}
				catch
				{
					currentStoreID = -1;
				}
				return currentStoreID;
			}
			set
			{
				currentStoreID = value;
			}
		}

		public static bool RegistryPrinterIsLandscape
		{
			get
			{
				try
				{
					return bool.Parse(RegistryGetOptionValue("PrinterLandscape").ToString());
				}
				catch
				{
					return true;
				}
			}
			set
			{
				RegistrySetOptionValue("PrinterLandscape", value);
			}
		}

		public static PrintDocument printDocument
		{
			get
			{
				PrintDocument printDocument = null;
				try
				{
					printDocument = new PrintDocument();
					if (printDoc != null)
					{
						printDocument.DefaultPageSettings = printDoc.DefaultPageSettings;
						printDocument.DocumentName = printDoc.DocumentName;
						printDocument.OriginAtMargins = printDoc.OriginAtMargins;
						printDocument.PrintController = printDoc.PrintController;
						printDocument.PrinterSettings = printDoc.PrinterSettings;
					}
					printDocument.DefaultPageSettings.Margins = RegistryPrinterMargin;
					printDocument.DefaultPageSettings.Landscape = RegistryPrinterIsLandscape;
					return printDocument;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return printDocument;
				}
			}
			set
			{
				if (printDoc != null)
				{
					printDoc.Dispose();
					printDoc = null;
				}
				printDoc = value;
			}
		}

		public static PrintDocument ReportPrintDocument
		{
			get
			{
				try
				{
					if (reportPrintDoc != null)
					{
						return reportPrintDoc;
					}
					reportPrintDoc = new PrintDocument();
					reportPrintDoc.PrinterSettings.PrinterName = UserLocalSettings.ReportPrinterName;
					reportPrintDoc.DefaultPageSettings.Margins.Top = UserLocalSettings.ReportTopMargin;
					reportPrintDoc.DefaultPageSettings.Margins.Left = UserLocalSettings.ReportLeftMargin;
					reportPrintDoc.DefaultPageSettings.Margins.Right = 0;
					reportPrintDoc.DefaultPageSettings.Margins.Bottom = 0;
					reportPrintDoc.DefaultPageSettings.Landscape = UserLocalSettings.IsPrintReportLandscape;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
				return reportPrintDoc;
			}
			set
			{
				if (printDoc != null)
				{
					printDoc.Dispose();
					printDoc = null;
				}
				reportPrintDoc = value;
			}
		}

		public static string RegistryItemDetailsMinimizedPurchaseInfo
		{
			get
			{
				return GetCompanyRegistryValue("ItemDetailsMinimizedPurchaseInf").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ItemDetailsMinimizedPurchaseInf", value);
			}
		}

		public static string RegistryItemDetailsMinimizedDetails
		{
			get
			{
				return GetCompanyRegistryValue("DetailsMinimizedDetails").ToString();
			}
			set
			{
				SetCompanyRegistryValue("DetailsMinimizedDetails", value);
			}
		}

		public static string RegistryItemDetailsMinimizedAccounts
		{
			get
			{
				return GetCompanyRegistryValue("ItemDetailsMinimizedAccounts").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ItemDetailsMinimizedAccounts", value);
			}
		}

		public static string RegistryItemDetailsMinimizedNotes
		{
			get
			{
				return GetCompanyRegistryValue("ItemDetailsMinimizedNotes").ToString();
			}
			set
			{
				SetCompanyRegistryValue("ItemDetailsMinimizedNotes", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintHeader
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintHeader").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintHeader", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintTable
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintTable").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintTable", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintNote1
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintNote1").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintNote1", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintNote2
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintNote2").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintNote2", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintNote3
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintNote3").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintNote3", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintTitle
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintTitle").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintTitle", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintFooter
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintFooter").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintFooter", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintFooterText1
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintFooterText1").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintFooterText1", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintFooterText2
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintFooterText2").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintFooterText2", value);
			}
		}

		public static string RegistryPurchaseInvoicePrintAfterSaving
		{
			get
			{
				return GetCompanyRegistryValue("PIPrintAfterSaving").ToString();
			}
			set
			{
				SetCompanyRegistryValue("PIPrintAfterSaving", value);
			}
		}

		public static string RegistryStatementCustomerMessage
		{
			get
			{
				return GetCompanyRegistryValue("RegistryStatementCustomerMessage").ToString();
			}
			set
			{
				SetCompanyRegistryValue("RegistryStatementCustomerMessage", value);
			}
		}

		public static bool OpenPreviousCompany
		{
			get
			{
				try
				{
					return bool.Parse(GlobalSettings.GetSetting("OpenPreviousCompany", "False").ToString());
				}
				catch
				{
					return false;
				}
			}
			set
			{
				GlobalSettings.SaveSetting("OpenPreviousCompany", value);
			}
		}

		public static bool RememberLoginInfo
		{
			get
			{
				try
				{
					return bool.Parse(GlobalSettings.GetSetting("rememberLoginInfo", "False").ToString());
				}
				catch
				{
					return false;
				}
			}
			set
			{
				GlobalSettings.SaveSetting("rememberLoginInfo", value);
			}
		}

		public static int MajorVersion => 1;

		public static int ProductID => 5;

		public static event EventHandler OnOpenCompany;

		public static event EventHandler OnChangeStatus;

		public static event EventHandler OnReconnectionNeeded;

		public static event EventHandler UpdateNeeded;

		static Global()
		{
			IsTrialLimitReached = false;
			isUserAdmin = false;
			RECORD_NEW_TRANSACTION_QUESTION_MESSAGE = "You have not recorded the new transaction. Do you want to record it now?";
			IsAxolonAgent = false;
			IsNotifierVisible = false;
			AlarmedNotes = new ArrayList();
			baseCurrencyID = "AED";
			curDecimalPoints = 255;
			HomePageLoaded = false;
			CurrentLocationID = "";
			CurrentPOSLocationID = "";
			CurrentCashRegisterID = "";
			DefaultCustomerID = "";
			DefaultCustomerName = "";
			Global.OnOpenCompany = null;
			Global.OnChangeStatus = null;
			Global.OnReconnectionNeeded = null;
			Global.UpdateNeeded = null;
			companyLogo = null;
			printDoc = null;
			reportPrintDoc = null;
			defaultStoreName = null;
			currentStoreID = -2;
			addressNumber = 1;
			fiscalDate = DateTime.MinValue;
			termData = null;
			companyInformationData = null;
			currentUser = string.Empty;
			currentPassword = null;
			connectionStatus = ConnectionStatus.DisConnected;
			currentDBName = null;
			currentServerName = null;
			isExternalDB = false;
			externalDBUserName = null;
			externalDBUserPassword = null;
			applicationType = ApplicationTypes.POSManager;
			currentPortNumber = 6000;
			currentInstanceName = null;
			currentUserID = -1;
			reconciliationInProcess = false;
			companySettings = null;
			DefaultLocationID = null;
			isMSWordExist = false;
			closingBookDate = DateTime.MinValue;
			closingBookPassword = "";
			isClosingBookCalled = false;
			currencySign = null;
		}

		public static bool IsCurrentUserAdministrator()
		{
			return false;
		}

		public static void LoadCompanySystemSettings()
		{
		}

		public static bool HasClosingBookDate(DateTime date)
		{
			if (!isClosingBookCalled)
			{
				SetClosingBookDate();
			}
			if (date == DateTime.MinValue)
			{
				return false;
			}
			if (closingBookDate != DateTime.MinValue)
			{
				return closingBookDate >= date;
			}
			return false;
		}

		public static void SetClosingBookDate()
		{
			if (Factory.IsDBConnected)
			{
				ClosingBookDate = Factory.CompanyInformationSystem.GetClosingBookDate();
				ClosingBookPassword = Factory.CompanyInformationSystem.GetClosingBookPassword();
				isClosingBookCalled = true;
			}
		}

		public static void Reset()
		{
			companyInformationData = null;
			Security.Reset();
		}

		public static void Init()
		{
		}

		public static void OnUpdateNeeded(AutoAppUpdater autoUpdater)
		{
			if (Global.UpdateNeeded != null)
			{
				Global.UpdateNeeded(autoUpdater, null);
			}
		}

		public static object GetCompanyRegistryValue(string keyName)
		{
			object result = null;
			try
			{
				result = CompanySettings.GetSetting(keyName, "");
				return result;
			}
			catch
			{
				return result;
			}
		}

		public static void SetCompanyRegistryValue(string keyName, object val)
		{
			try
			{
				CompanySettings.SaveSetting(keyName, val);
			}
			catch
			{
			}
		}

		public static string RegistryGetOptionValue(string name)
		{
			RegistryHelper registryHelper = null;
			string text = "";
			try
			{
				registryHelper = new RegistryHelper("Options", writable: false);
				return registryHelper.GetStringValue(name, "");
			}
			catch
			{
				return "";
			}
			finally
			{
				if (registryHelper != null)
				{
					registryHelper.Dispose();
					registryHelper = null;
				}
			}
		}

		public static void RegistrySetOptionValue(string name, object val)
		{
			RegistryHelper registryHelper = null;
			try
			{
				registryHelper = new RegistryHelper("Options", writable: true);
				registryHelper.SetValue(name, val);
			}
			catch
			{
			}
			finally
			{
				if (registryHelper != null)
				{
					registryHelper.Dispose();
					registryHelper = null;
				}
			}
		}

		public static bool SetOpenCompany(string dbName)
		{
			try
			{
				bool flag = true;
				if (dbName == null || dbName == string.Empty || dbName == "master")
				{
					if (companySettings != null)
					{
						companySettings.Dispose();
						companySettings = null;
					}
					flag = false;
				}
				CurrentDatabaseName = dbName;
				if (Global.OnOpenCompany != null && dbName != "master")
				{
					Global.OnOpenCompany(flag, null);
				}
			}
			catch
			{
			}
			return true;
		}

		public static void ReconnectionNeeded()
		{
			if (Global.OnReconnectionNeeded != null)
			{
				Global.OnReconnectionNeeded(true, null);
			}
		}

		public static bool IsCorrectDB()
		{
			if (companyInformationData == null)
			{
				try
				{
					Factory.CompanyInformationSystem.GetCompanyInformation();
				}
				catch (SqlException ex)
				{
					if (ex.Number != 208)
					{
						throw;
					}
					return false;
				}
				catch
				{
					throw;
				}
			}
			return true;
		}

		public static string GetBalanceSheetDate(string strDate)
		{
			string text = "";
			try
			{
				DateTime dateTime = DateTime.Parse(strDate.Trim());
				if (dateTime.Year >= DateRange.SmallDateTimeTo.Year || dateTime.Year <= DateRange.SmallDateTimeFrom.Year)
				{
					return "All Transactions";
				}
				return "As of " + GetMonth(dateTime.Month) + " " + dateTime.Day + ", " + dateTime.Year;
			}
			catch
			{
				return "All Transactions";
			}
		}

		public static void ChangeApplicationStatusMessage(object o)
		{
			if (Global.OnChangeStatus != null)
			{
				Global.OnChangeStatus(o, null);
			}
		}

		public static byte GetTermNetDays(string termID)
		{
			byte result = 0;
			if (ConStatus == ConnectionStatus.DisConnected)
			{
				return result;
			}
			if (termData == null)
			{
				termData = Factory.TermSystem.GetTerms();
			}
			foreach (DataRow row in termData.TermTable.Rows)
			{
				if (row["PaymentTermID"].ToString() == termID)
				{
					return byte.Parse(row["NetDays"].ToString());
				}
			}
			return result;
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

		public static string RemoveNonFileChars(string fileName)
		{
			return RemoveChar(fileName, '\\', '/', ':', '*', '?', '"', '<', '>', '|');
		}

		public static string RemoveChar(string str, params char[] cs)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			while (true)
			{
				int num = str.IndexOfAny(cs);
				if (num < 0)
				{
					break;
				}
				str = str.Remove(num, 1);
			}
			return str;
		}

		public static string RemoveMultiline(string oldString)
		{
			try
			{
				if (oldString == null)
				{
					return "";
				}
				string[] array = oldString.Split('\n', '\r');
				StringBuilder stringBuilder = new StringBuilder();
				string[] array2 = array;
				foreach (string value in array2)
				{
					stringBuilder.Append(value).Append(" ");
				}
				return stringBuilder.Remove(checked(stringBuilder.Length - 1), 1).ToString();
			}
			catch
			{
				return oldString;
			}
		}

		public static string ReplaceCharWithWhiteSpace(string str, params char[] cs)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			foreach (char oldChar in cs)
			{
				str = str.Replace(oldChar, ' ');
			}
			return str;
		}

		public static string GetTransactionDate(string strFrom, string strTo)
		{
			string text = "";
			try
			{
				DateTime dateTime = DateTime.Parse(strFrom.Trim());
				DateTime dateTime2 = DateTime.Parse(strTo.Trim());
				if (dateTime.Year <= DateRange.SmallDateTimeFrom.Year && dateTime2.Year >= DateRange.SmallDateTimeTo.Year)
				{
					return "All Transactions";
				}
				if (dateTime.Year != dateTime2.Year)
				{
					return GetMonth(dateTime.Month) + " " + dateTime.Day + ", " + dateTime.Year + " through " + GetMonth(dateTime2.Month) + " " + dateTime2.Day + ", " + dateTime2.Year;
				}
				if (dateTime.Month != dateTime2.Month)
				{
					return GetMonth(dateTime.Month) + " " + dateTime.Day + " through " + GetMonth(dateTime2.Month) + " " + dateTime2.Day + ", " + dateTime2.Year;
				}
				if (dateTime.Day != dateTime2.Day)
				{
					return GetMonth(dateTime.Month) + " " + dateTime.Day + "-" + dateTime2.Day + ", " + dateTime2.Year;
				}
				return GetMonth(dateTime.Month) + " " + dateTime.Day + ", " + dateTime.Year;
			}
			catch
			{
				return "All Transactions";
			}
		}

		public static string GetShortTransactionDate(string strFrom, string strTo)
		{
			string text = "";
			try
			{
				DateTime dateTime = DateTime.Parse(strFrom.Trim());
				DateTime dateTime2 = DateTime.Parse(strTo.Trim());
				if (dateTime.Year <= DateRange.SmallDateTimeFrom.Year && dateTime2.Year >= DateRange.SmallDateTimeTo.Year)
				{
					return GetShortMonth(DateTime.Today.Month) + " " + DateTime.Today.Day + ", " + DateTime.Today.Year;
				}
				if (dateTime.Year != dateTime2.Year)
				{
					return GetShortMonth(dateTime.Month) + " " + dateTime.Day + ", " + dateTime.Year + "-" + GetShortMonth(dateTime2.Month) + " " + dateTime2.Day + ", " + dateTime2.Year;
				}
				if (dateTime.Month != dateTime2.Month)
				{
					return GetShortMonth(dateTime.Month) + " " + dateTime.Day + "-" + GetShortMonth(dateTime2.Month) + " " + dateTime2.Day + ", " + dateTime2.Year;
				}
				if (dateTime.Day != dateTime2.Day)
				{
					return GetShortMonth(dateTime.Month) + " " + dateTime.Day + "-" + dateTime2.Day + ", " + dateTime2.Year;
				}
				return GetShortMonth(dateTime.Month) + " " + dateTime.Day + ", " + dateTime.Year;
			}
			catch
			{
				return GetShortMonth(DateTime.Today.Month) + " " + DateTime.Today.Day + ", " + DateTime.Today.Year;
			}
		}

		public static string GetMonth(int month)
		{
			switch (month)
			{
			case 1:
				return "January";
			case 2:
				return "February";
			case 3:
				return "March";
			case 4:
				return "April";
			case 5:
				return "May";
			case 6:
				return "June";
			case 7:
				return "July";
			case 8:
				return "August";
			case 9:
				return "September";
			case 10:
				return "October";
			case 11:
				return "November";
			case 12:
				return "December";
			default:
				throw new Exception("Date out of range");
			}
		}

		public static string GetShortMonth(int month)
		{
			switch (month)
			{
			case 1:
				return "Jan";
			case 2:
				return "Feb";
			case 3:
				return "Mar";
			case 4:
				return "Apr";
			case 5:
				return "May";
			case 6:
				return "Jun";
			case 7:
				return "Jul";
			case 8:
				return "Aug";
			case 9:
				return "Sep";
			case 10:
				return "Oct";
			case 11:
				return "Nov";
			case 12:
				return "Dec";
			default:
				throw new Exception("Date out of range");
			}
		}

		public static int GetDaysInYear(int year)
		{
			if (DateTime.IsLeapYear(year))
			{
				return 366;
			}
			return 365;
		}

		public static string MakeIdentifier(string str)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			int num = 0;
			checked
			{
				while (num < str.Length && char.IsDigit(str[num]))
				{
					str = str.Remove(num, 1);
					num--;
					num++;
				}
				for (num = 0; num < str.Length; num++)
				{
					char c = str[num];
					if (!char.IsLetterOrDigit(c) && c != '_')
					{
						str = str.Remove(num, 1);
						num--;
					}
				}
				str = str.Replace(" ", "");
				return str;
			}
		}

		public static bool LoadPrinterPropertiesDialog(string deviceName, IntPtr parentHWND)
		{
			Win32API.PRINTER_DEFAULTS pDefault = default(Win32API.PRINTER_DEFAULTS);
			int num = 0;
			pDefault.pDesiredAccess = -256L;
			bool result = false;
			if (Win32API.OpenPrinter(deviceName, num, pDefault) > 0)
			{
				result = (Win32API.PrinterProperties(parentHWND, num) > 0);
				Win32API.ClosePrinter(num);
			}
			return result;
		}

		public static bool SavePassword(string password, string keyName)
		{
			try
			{
				password = Factory.Encrypt(password);
				SaveIsolatedStorageValue(keyName, password);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static string GetPassword(string regKey)
		{
			try
			{
				string text = "";
				object isolatedStorageValue = GetIsolatedStorageValue(regKey);
				if (isolatedStorageValue == null)
				{
					return string.Empty;
				}
				text = isolatedStorageValue.ToString();
				return Factory.Decrypt(text);
			}
			catch
			{
				return string.Empty;
			}
		}

		public static void SaveRegistryOptionValue(string key, string value, bool isEncrypt)
		{
			RegistryHelper registryHelper = null;
			try
			{
				registryHelper = new RegistryHelper("Options", writable: true);
				string val = value;
				if (isEncrypt)
				{
					val = Factory.Encrypt(value);
					SaveIsolatedStorageValue(key, val);
				}
				else
				{
					registryHelper.SetValue(key, val);
				}
			}
			catch
			{
			}
			finally
			{
				if (registryHelper != null)
				{
					registryHelper.Dispose();
					registryHelper = null;
				}
			}
		}

		public static object GetIsolatedStorageValue(string key)
		{
			try
			{
				IsolatedStorage isolatedStorage = new IsolatedStorage();
				object result = null;
				if (isolatedStorage.ContainsKey(key))
				{
					result = isolatedStorage[key];
				}
				isolatedStorage = null;
				return result;
			}
			catch
			{
			}
			return "";
		}

		public static void SaveIsolatedStorageValue(string key, string value)
		{
			IsolatedStorage isolatedStorage = new IsolatedStorage();
			isolatedStorage[key] = value;
			isolatedStorage.Save();
		}

		public static string GetRegistryOptionValue(string key, bool isEncrypt)
		{
			RegistryHelper registryHelper = null;
			string text = null;
			try
			{
				registryHelper = new RegistryHelper("Options", writable: true);
				if (isEncrypt)
				{
					object isolatedStorageValue = GetIsolatedStorageValue(key);
					if (!isolatedStorageValue.IsNullOrEmpty())
					{
						text = isolatedStorageValue.ToString();
					}
					if (!text.IsNullOrEmpty())
					{
						return Factory.Decrypt(text);
					}
					return "";
				}
				return registryHelper.GetStringValue(key, "");
			}
			catch
			{
				return "";
			}
			finally
			{
				if (registryHelper != null)
				{
					registryHelper.Dispose();
					registryHelper = null;
				}
			}
		}

		public static string GetOSDrive()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("windir");
			if (environmentVariable.Length > 0)
			{
				return environmentVariable.ToCharArray()[0].ToString() + ":\\";
			}
			return "C:\\";
		}

		public static string GetPreviousCompanyName()
		{
			try
			{
				string[] valueNames = new RegistryHelper("Companies", writable: false).GetValueNames();
				if (valueNames == null)
				{
					return "";
				}
				if (valueNames.Length != 0)
				{
					return valueNames[checked(valueNames.Length - 1)];
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static bool HasCurrentUserDeleteTransactionRight(ScreenAreas screenArea)
		{
			return Security.HasCurrentUserDeleteTransactionRight(screenArea);
		}

		public static bool HasCurrentUserChangeTransactionRight(ScreenAreas screenArea)
		{
			return Security.HasCurrentUserChangeTransactionRight(screenArea);
		}

		public static string GetProductKey()
		{
			return "YBZAY-UKU5F-DWZMD-UDQDB-E85TV-P5XY9-VK5F4";
		}

		public static string GetSystemID()
		{
			return "CZC5190DY8#8";
		}

		public static double GetVersionNumber()
		{
			return 6.0;
		}

		public static string GetCLUserApproval(string password)
		{
			string overCLPassword = CompanyPreferences.OverCLPassword;
			if (password == overCLPassword)
			{
				return "MASTER";
			}
			return Factory.UserSystem.GetUserByCLPass(password);
		}

		public static DateTime CalculateDueDate(DateTime invoiceDate, string paymentTermID)
		{
			DateTime result = invoiceDate.AddMonths(1);
			if (paymentTermID != "")
			{
				PaymentTermData termByID = Factory.TermSystem.GetTermByID(paymentTermID);
				if (termByID == null || termByID.Tables.Count == 0)
				{
					return result;
				}
				DataRow dataRow = termByID.TermTable.Rows[0];
				int result2 = 0;
				int.TryParse(dataRow["TermType"].ToString(), out result2);
				PaymentTermTypes paymentTermTypes = (PaymentTermTypes)result2;
				int num = int.Parse(dataRow["NetDays"].ToString());
				switch (paymentTermTypes)
				{
				case PaymentTermTypes.FromInvoiceDate:
					result = invoiceDate.AddDays(num);
					break;
				case PaymentTermTypes.FromEOM:
					result = new DateTime(invoiceDate.Year, invoiceDate.Month, 1).AddMonths(1).AddDays(-1.0).AddDays(num);
					break;
				default:
					result = invoiceDate.AddDays(num);
					break;
				}
			}
			return result;
		}

		public static MemoryStream SerializeToStream(object data)
		{
			MemoryStream memoryStream = new MemoryStream();
			((IFormatter)new BinaryFormatter()).Serialize((Stream)memoryStream, data);
			return memoryStream;
		}

		public static object DeserializeFromStream(byte[] streamBytes)
		{
			try
			{
				if (streamBytes.Length == 0)
				{
					return null;
				}
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(streamBytes, 0, streamBytes.Length);
				return DeserializeFromStream(memoryStream);
			}
			catch
			{
				throw;
			}
		}

		public static object DeserializeFromStream(MemoryStream stream)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			stream.Seek(0L, SeekOrigin.Begin);
			return ((IFormatter)binaryFormatter).Deserialize((Stream)stream);
		}

		public static ApplicationTypes GetApplicationType()
		{
			string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\apptype.dat";
			try
			{
				if (!File.Exists(path))
				{
					return ApplicationTypes.ERP;
				}
				StreamReader streamReader = new StreamReader(path);
				string s = streamReader.ReadLine();
				streamReader.Close();
				return (ApplicationTypes)int.Parse(s);
			}
			catch (Exception)
			{
				return ApplicationTypes.ERP;
			}
		}
	}
}
