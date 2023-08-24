using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.ClientLibraries
{
	public static class CompanyPreferences
	{
		private static DataRow companyPrefRow = null;

		private static string price1Title = "Standard Price";

		private static string price2Title = "Wholesale Price";

		private static string price3Title = "Special Price";

		private static string propertyTitle = "Property";

		private static string unitTitle = "Unit";

		private static bool isPriceTitlesLoaded = false;

		public static string UnitPrice1Title
		{
			get
			{
				if (!isPriceTitlesLoaded)
				{
					LoadUnitPriceTitles();
				}
				return price1Title;
			}
			set
			{
				price1Title = value;
			}
		}

		public static string UnitPrice2Title
		{
			get
			{
				if (!isPriceTitlesLoaded)
				{
					LoadUnitPriceTitles();
				}
				return price2Title;
			}
			set
			{
				price2Title = value;
			}
		}

		public static string UnitPrice3Title
		{
			get
			{
				if (!isPriceTitlesLoaded)
				{
					LoadUnitPriceTitles();
				}
				return price3Title;
			}
			set
			{
				price3Title = value;
			}
		}

		public static string AttributeID1Title
		{
			get
			{
				if (GlobalRules.IsModuleAvailable(AxolonModules.PropertyRental))
				{
					propertyTitle = "Property";
				}
				return propertyTitle;
			}
			set
			{
				propertyTitle = value;
			}
		}

		public static string AttributeID2Title
		{
			get
			{
				if (GlobalRules.IsModuleAvailable(AxolonModules.PropertyRental))
				{
					unitTitle = "Unit";
				}
				return unitTitle;
			}
			set
			{
				unitTitle = value;
			}
		}

		public static bool IsTax
		{
			get
			{
				bool result = false;
				bool.TryParse(companyPrefRow["IsTax"].ToString(), out result);
				return result;
			}
		}

		public static bool UseMultiCurrency
		{
			get
			{
				bool result = false;
				if (!GlobalRules.IsFeatureAllowed(EditionFeatures.MultiCurrency))
				{
					return false;
				}
				bool.TryParse(companyPrefRow["UseMultiCurrency"].ToString(), out result);
				return result;
			}
		}

		public static bool IsPDCInclude
		{
			get
			{
				bool result = false;
				bool.TryParse(companyPrefRow["IncludePDC"].ToString(), out result);
				return result;
			}
		}

		public static bool TrackConsignInExpense => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TrackConsignInExpenses, defaultValue: false);

		public static bool UseJobCosting
		{
			get
			{
				if (!GlobalRules.IsFeatureAllowed(EditionFeatures.JobCosting))
				{
					return false;
				}
				return CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowJobCosting, defaultValue: false);
			}
		}

		public static bool UseProperty
		{
			get
			{
				bool flag = false;
				if (!GlobalRules.IsModuleAvailable(AxolonModules.PropertyRental))
				{
					return flag;
				}
				if (GlobalRules.IsModuleAvailable(AxolonModules.PropertyRental))
				{
					return !flag;
				}
				return flag;
			}
		}

		public static bool CreateProjectwithSO
		{
			get
			{
				if (!GlobalRules.IsFeatureAllowed(EditionFeatures.JobCosting))
				{
					return false;
				}
				return CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowtoCreateProjectwithSO, defaultValue: false);
			}
		}

		public static bool IssueGRNtoProject
		{
			get
			{
				if (!GlobalRules.IsFeatureAllowed(EditionFeatures.JobCosting))
				{
					return false;
				}
				return CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowIssueGRNtoProject, defaultValue: false);
			}
		}

		public static bool IsCostCenterMandatory => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.IsCostCenterMandatory, defaultValue: false);

		public static bool ShowAllocationForm => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAllocationForm, defaultValue: true);

		public static bool ShowItemQuantityInCombo => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemQuantityInCombo, defaultValue: false);

		public static bool ShowItemUnitInCombo => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemUnitInCombo, defaultValue: false);

		public static bool ShowItemCostInCombo => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemCostInCombo, defaultValue: false);

		public static bool ShowItemUPCInCombo => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemUPCInCombo, defaultValue: false);

		public static bool ShowItemdetail => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemDetail, defaultValue: false);

		public static bool AllowtoeditReqDate => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowtoeditPOReqDate, defaultValue: false);

		public static bool considerStockinMRPQ => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ConsiderStockinMRPQ, defaultValue: false);

		public static bool ShowOpenInvoices => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowOnlytOpenInvoices, defaultValue: false);

		public static bool UseInlineDiscount => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowInlineDiscount, defaultValue: false);

		public static bool ActivateBin => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateBinField, defaultValue: false);

		public static byte MinPriceSaleAction
		{
			get
			{
				byte result = 1;
				byte.TryParse(companyPrefRow["MinPriceSaleAction"].ToString(), out result);
				return result;
			}
		}

		public static byte PricelessCostAction
		{
			get
			{
				byte result = 1;
				byte.TryParse(companyPrefRow["PricelessCostAction"].ToString(), out result);
				return result;
			}
		}

		public static PurchaseFlows LocalPurchaseFlow => (PurchaseFlows)CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LocalPurchaseFlow, 0);

		public static PurchaseFlows ImportPurchaseFlow => (PurchaseFlows)CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ImportPurchaseFlow, 0);

		public static string MinPriceSalePassword => companyPrefRow["MinPriceSalePass"].ToString();

		public static string PricelessCostPassword => companyPrefRow["PricelessCostPass"].ToString();

		public static byte OverCLAction
		{
			get
			{
				byte result = 1;
				byte.TryParse(companyPrefRow["OverCLAction"].ToString(), out result);
				return result;
			}
		}

		public static string OverCLPassword => companyPrefRow["OverCLPass"].ToString();

		public static byte NegativeQuantityAction
		{
			get
			{
				byte result = 1;
				byte.TryParse(companyPrefRow["NegativeQuantityAction"].ToString(), out result);
				return result;
			}
		}

		public static string NegativeQuantityPassword => companyPrefRow["NegativeQuantityPass"].ToString();

		public static bool AllowLocalGRNAddNewRow => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLocalGRNAddNew, defaultValue: false);

		public static bool AllowImportGRNAddNewRow => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportGRNAddNew, defaultValue: false);

		public static bool AllowImportPackingListAddNew => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportGRNPackingListAddNew, defaultValue: false);

		public static bool AllowImportGRNPackingListAddNew => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportGRNPackingListAddNew, defaultValue: false);

		public static bool ShowBOLListinPackingList => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowBOLListinPackingList, defaultValue: false);

		public static bool AllowLocalGRNWithoutPO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLocalGRNWithoutPO, defaultValue: false);

		public static bool AllowIPurchaseGRNWithoutPO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportGRNWithoutPO, defaultValue: false);

		public static bool AllowLPurchaseQtyMoreThanPO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLocalQtyMoreThanPO, defaultValue: false);

		public static bool AllowIPurchaseQtyMoreThanPO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportQtyMoreThanPO, defaultValue: false);

		public static SalesFlows LocalSalesFlow => (SalesFlows)CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LocalSalesFlow, 0);

		public static SalesFlows ExportSalesFlow => (SalesFlows)CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ExportSalesFlow, 0);

		public static bool AllowLocalSellQtyMoreThanSO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSQtyMoreThanSO, defaultValue: false);

		public static bool AllowLSaleAddNew => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSAddNew, defaultValue: false);

		public static bool AllowLSellWithoutSO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSWithoutSO, defaultValue: true);

		public static bool UseProjectPhase => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.UseProjectPhase, defaultValue: false);

		public static bool AllowExportSellQtyMoreThanSO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESQtyMoreThanSO, defaultValue: false);

		public static bool AllowESaleAddNew => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESAddNew, defaultValue: false);

		public static bool AllowESellWithoutSO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESWithoutSO, defaultValue: true);

		public static bool AllowLSDNoteWithoutInvoice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSDNoteWithoutInvoice, defaultValue: false);

		public static bool AllowESDNoteWithoutInvoice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESDNoteWithoutInvoice, defaultValue: false);

		public static bool AllowChangePriceInSalesReturn => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowChangePriceInSalesReturn, defaultValue: false);

		public static bool AllowChangePriceInPurchaseReturn => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowChangePriceInPurchaseReturn, defaultValue: false);

		public static bool AllowChangePayeeNameInChequePrinting => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowChangeChequePrintPayee, defaultValue: true);

		public static bool AllowSalesReturnWithoutInvoice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSReturnWithoutInvoice, defaultValue: false);

		public static bool AllowPurchaseReturnWithoutInvoice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowPurchaseReturnWithoutInvoice, defaultValue: false);

		public static bool AllowLandingCostForLocalPurchase => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLandingCostInLocalPurchase, defaultValue: true);

		public static bool ShowLandingCostCalculation => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowLandingCostAmountInGrid, defaultValue: false);

		public static bool TrackConsignOutDetailedSales => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ConsignInFIFO, defaultValue: true);

		public static bool TrackConsignInDetailedSales => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TrackConsignInDetailedSales, defaultValue: true);

		public static bool AllocatConsignOutDetailedSales => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TrackConsignOutDetailedSales, defaultValue: true);

		public static int MatrixDescriptionGenerationMethod => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.MatrixDescriptionGenerationMethod, 2);

		public static decimal AppraisalRangeFrom => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AppraisalPointFrom, 0m);

		public static decimal AppraisalRangeTo => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AppraisalPointTo, 0m);

		public static decimal RemarkValidation => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ReamrkValidationPoint, 0m);

		public static decimal TaxPercent => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TaxPercentValue, 0m);

		public static decimal DiscountWriteOffPerc => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DiscountWriteoffPercent, 1m);

		public static bool AllowSalesInvoiceNegativeQty => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowSalesInvoiceNegativeQty, defaultValue: true);

		public static bool CheckCreditLimitInDeliveryNote => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CheckCLOnDeliveryNote, defaultValue: false);

		public static bool AllowPurchaseInvoiceNegativeQty => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowPurchaseInvoiceNegativeQty, defaultValue: true);

		public static bool AllowPurchaseInvoiceChangePrice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowPurchaseInvoiceChangePrice, defaultValue: true);

		public static bool PurchaseInvoiceChangePrice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowPurchaseInvoiceChangePrice, defaultValue: false);

		public static bool PurchaseLandingCostCalculationMethod => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PurchaseLandingCostCalculationMethod, defaultValue: false);

		public static bool LoadZeroQuantityinGRN => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LoadZeroQuantityinGRN, defaultValue: false);

		public static bool AllowFollowUponLead => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowDoFollowUponLead, defaultValue: false);

		public static string Attribute1Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Attribute1Name, "Attribute 1");

		public static string Attribute2Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Attribute2Name, "Attribute 2");

		public static string Attribute3Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Attribute3Name, "Attribute 3");

		public static string ProductType1Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType1Name, "P.Type 1");

		public static string ProductType2Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType2Name, "P.Type 2");

		public static string ProductType3Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType3Name, "P.Type 3");

		public static string ProductType4Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType4Name, "P.Type 4");

		public static string ProductType5Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType5Name, "P.Type 5");

		public static string ProductType6Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType6Name, "P.Type 6");

		public static string ProductType7Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType7Name, "P.Type 7");

		public static string ProductType8Name => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType8Name, "P.Type 8");

		public static byte TotalWorkingDayHours
		{
			get
			{
				string companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TotalWorkingDayHours, "8");
				if (string.IsNullOrEmpty(companyOption))
				{
					return 0;
				}
				return byte.Parse(companyOption);
			}
		}

		public static int TotalWorkingMonthHours
		{
			get
			{
				string companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TotalWorkingMonthHours, "270");
				if (string.IsNullOrEmpty(companyOption))
				{
					return 0;
				}
				return int.Parse(companyOption);
			}
		}

		public static byte OffDay => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OffDay, (byte)7);

		public static bool ActivateAutoservice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateAutoservice, defaultValue: false);

		public static bool EnableHRAnalysisCode => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableHRAnalysis, defaultValue: false);

		public static bool EnableVehicleAnalysisCode => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableVehicleAnalysis, defaultValue: false);

		public static bool EnableLegalAnalysisCode => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableLegalAnalysis, defaultValue: false);

		public static bool EnablePatientAnalysisCode => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnablePatientAnalysis, defaultValue: false);

		public static bool LoadItemDescFromPriceList => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LoadItemDescFromPriceList, defaultValue: false);

		public static bool ActivateCostingonDelete => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Enablecostingondelete, defaultValue: true);

		public static bool LoadItemFeatures => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemFeatures, defaultValue: false);

		public static bool AllowImportMoreThanPackingListQuantity => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LoadItemDescFromPriceList, defaultValue: false);

		public static bool FinancialTransactionPosting => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.FinancialTransactionPosting, defaultValue: false);

		public static bool PDCDirectMaturity => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PDCDirectMaturity, defaultValue: true);

		public static bool ShowLotdetailinPrintout => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowLotdetailinPrintout, defaultValue: false);

		public static bool ShowOrderandShipmentinGRN => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowOrderAndShipmentDetailInGRN, defaultValue: false);

		public static bool ShowCreatefromPickList => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESCreatefromPickList, defaultValue: false);

		public static bool UseOTBasedon => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OTBasedOn, defaultValue: false);

		public static bool BasedonNetDays => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DeductiononNetDays, defaultValue: true);

		public static bool SetlastSalesprice => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TakeLastSalesPrice, defaultValue: false);

		public static string SpecificationID => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.SpecificationName, "SpecificationID");

		public static string Description3 => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OtherDescription, "Description 3");

		public static bool AllowCreditSaleinSalesreceipt => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowCreditSaleInSalesReceipt, defaultValue: false);

		public static bool Allowzeropriceinsale => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowzeroinSales, defaultValue: false);

		public static bool DaysInMonth => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DaysInMonth, defaultValue: true);

		public static bool ThirtyDays => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ThirtyDays, defaultValue: false);

		public static bool Annual => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Annual, defaultValue: false);

		public static bool Roundoffsalary => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RoundOffSalaryCalculation, defaultValue: true);

		public static bool AllowJobChangeInMRPQTransaction => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowJobChangeInMRPQ, defaultValue: true);

		public static bool AllowCustomerChangeInDN => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowCustomerChangeInDN, defaultValue: false);

		public static bool ExcludeZeroQtyInDN => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ExcludeZeroQtyInDN, defaultValue: false);

		public static bool ActivatePartsDetails => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivatePartsDetails, defaultValue: false);

		public static bool TradingBaseonB2C => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.BasedonB2C, defaultValue: false);

		public static int ItemCodeCreationBasedOn
		{
			get
			{
				string companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ItemCodeCreationBasedOn, "0");
				if (string.IsNullOrEmpty(companyOption))
				{
					return 0;
				}
				return int.Parse(companyOption);
			}
		}

		public static bool EnableDocTempSaving => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableTempSaving, defaultValue: false);

		public static bool ShowMultidimensionOnGrid => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowMultidimensionOnGrid, defaultValue: false);

		public static bool POSDisplayItemFeatures => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.POSDisplayItemFeatures, defaultValue: false);

		public static bool ChangeSalespersonWhileSave => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.POSChangeSalesPersonWhileSaving, defaultValue: false);

		public static bool DirectTREntry => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DirectTREntry, defaultValue: true);

		public static bool MandatoryPOBOL => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.MandatoryPOBOL, defaultValue: true);

		public static string TaxEntityTypes => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TaxEntityTypes, "Attribute 3");

		public static string DefaultTaxGroup => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultTaxGroup, "Attribute 3");

		public static int DefaultTaxOption => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultTaxOption, 0);

		public static bool PriceValidationInSQ => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PriceValidationInSQ, defaultValue: false);

		public static bool MaterialReservationONSO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.MaterialReservationOnSo, defaultValue: false);

		public static bool DisableCustomerCreditLimit => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DisableCustomerCreditLimit, defaultValue: false);

		public static bool AllowLSQtyMoreThanSO => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSQtyMoreThanSO, defaultValue: false);

		public static bool defaultLocationOnAccounts => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultLocationAccounts, defaultValue: false);

		public static string RefSlNo => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefSlNo, "");

		public static string RefText1 => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefText1, "");

		public static string RefText2 => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefText2, "");

		public static string RefNum1 => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefNum1, "");

		public static string RefNum2 => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefNum2, "");

		public static string RefDate1 => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefDate1, "");

		public static string RefDate2 => CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefDate2, "");

		public static void LoadCompanyPreferences()
		{
			try
			{
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Users", "IsAdmin", "UserID", Global.CurrentUser);
				if (Global.CurrentUser.ToLower() == "sa")
				{
					Global.IsUserAdmin = true;
				}
				else if (fieldValue != null && fieldValue.ToString() != "")
				{
					Global.IsUserAdmin = bool.Parse(fieldValue.ToString());
				}
				DataSet companyPreferences = Factory.CompanyInformationSystem.GetCompanyPreferences();
				if (companyPreferences != null && companyPreferences.Tables.Count > 0 && companyPreferences.Tables[0].Rows.Count > 0)
				{
					companyPrefRow = companyPreferences.Tables[0].Rows[0];
				}
				if (companyPrefRow != null)
				{
					if (!companyPrefRow["BaseCurrencyID"].IsDBNullOrEmpty())
					{
						Global.BaseCurrencyID = companyPrefRow["BaseCurrencyID"].ToString();
					}
					else
					{
						Global.BaseCurrencyID = "AED";
					}
					if (!companyPrefRow["CurDecimalPoint"].IsDBNullOrEmpty())
					{
						Global.CurDecimalPoints = int.Parse(companyPrefRow["CurDecimalPoint"].ToString());
					}
					else
					{
						Global.CurDecimalPoints = 2;
					}
				}
				CompanyOptions.LoadCompanyOptions();
				CompanyOptions.LoadSysDocCompanyOptions();
			}
			catch (Exception)
			{
			}
		}

		public static void LoadUnitPriceTitles()
		{
			try
			{
				DataSet priceListNames = Factory.ProductSystem.GetPriceListNames();
				if (priceListNames != null && priceListNames.Tables.Count != 0 && priceListNames.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = priceListNames.Tables[0].Rows[0];
					if (dataRow != null)
					{
						if (dataRow["ItemPrice1Name"] != DBNull.Value && dataRow["ItemPrice1Name"].ToString() != "")
						{
							price1Title = dataRow["ItemPrice1Name"].ToString();
						}
						if (dataRow["ItemPrice2Name"] != DBNull.Value && dataRow["ItemPrice2Name"].ToString() != "")
						{
							price2Title = dataRow["ItemPrice2Name"].ToString();
						}
						if (dataRow["ItemPrice3Name"] != DBNull.Value && dataRow["ItemPrice3Name"].ToString() != "")
						{
							price3Title = dataRow["ItemPrice3Name"].ToString();
						}
						isPriceTitlesLoaded = true;
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
