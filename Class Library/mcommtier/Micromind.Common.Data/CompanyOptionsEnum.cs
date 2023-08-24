namespace Micromind.Common.Data
{
	public enum CompanyOptionsEnum
	{
		AllowLocalGRNAddNew = 50,
		AllowImportGRNAddNew = 51,
		AllowLocalGRNWithoutPO = 52,
		AllowImportGRNWithoutPO = 53,
		AllowLocalQtyMoreThanPO = 54,
		AllowImportQtyMoreThanPO = 55,
		LocalSalesFlow = 56,
		ExportSalesFlow = 57,
		LocalPurchaseFlow = 58,
		ImportPurchaseFlow = 59,
		AllowLSAddNew = 60,
		AllowESAddNew = 61,
		AllowLSWithoutSO = 62,
		AllowESWithoutSO = 0x3F,
		AllowLSQtyMoreThanSO = 0x40,
		AllowESQtyMoreThanSO = 65,
		AllowLSDNoteWithoutInvoice = 66,
		AllowESDNoteWithoutInvoice = 67,
		AllowLSReturnWithoutInvoice = 68,
		AllowChangePriceInSalesReturn = 69,
		AllowLandingCostInLocalPurchase = 70,
		ShowLandingCostAmountInGrid = 71,
		ConsignInFIFO = 72,
		MatrixDescriptionGenerationMethod = 73,
		AllowSalesInvoiceNegativeQty = 74,
		AllowPurchaseInvoiceNegativeQty = 75,
		ShowAging1 = 76,
		ShowAging2 = 77,
		ShowAging3 = 78,
		ShowAging4 = 79,
		ShowAging5 = 80,
		ShowAging6 = 81,
		ShowAging7 = 82,
		AgingName0 = 83,
		AgingName1 = 84,
		AgingName2 = 85,
		AgingName3 = 86,
		AgingName4 = 87,
		AgingName5 = 88,
		AgingName6 = 89,
		AgingName7 = 90,
		AgingFrom0 = 91,
		AgingFrom1 = 92,
		AgingFrom2 = 93,
		AgingFrom3 = 94,
		AgingFrom4 = 95,
		AgingFrom5 = 96,
		AgingFrom6 = 97,
		AgingFrom7 = 98,
		AgingTo0 = 99,
		AgingTo1 = 100,
		AgingTo2 = 101,
		AgingTo3 = 102,
		AgingTo4 = 103,
		AgingTo5 = 104,
		AgingTo6 = 105,
		AgingTo7 = 106,
		AgingByDueDate = 107,
		Attribute1Name = 108,
		Attribute2Name = 109,
		Attribute3Name = 110,
		PDCByMaturity = 111,
		POSCheckSufficientQty = 112,
		AllowJobCosting = 113,
		PDCDirectMaturity = 114,
		TotalWorkingDayHours = 115,
		TotalWorkingMonthHours = 116,
		OffDay = 117,
		LoadItemDescFromPriceList = 118,
		AllowInlineDiscount = 119,
		TrackConsignOutDetailedSales = 120,
		TrackConsignInDetailedSales = 121,
		TrackConsignInExpenses = 122,
		CompanyWPSID = 123,
		BankID = 124,
		AllowPurchaseInvoiceChangePrice = 125,
		PurchaseLandingCostCalculationMethod = 126,
		AllowImportGRNPackingListAddNew = 0x7F,
		IsCostCenterMandatory = 0x80,
		ShowAllocationForm = 129,
		AllowImportQtyMoreThanPL = 130,
		CheckCLOnDeliveryNote = 131,
		ShowItemQuantityInCombo = 132,
		UseProjectPhase = 133,
		AllowChangeChequePrintPayee = 134,
		AllowPurchaseReturnWithoutInvoice = 135,
		AllowChangePriceInPurchaseReturn = 136,
		OTBasedOn = 137,
		ShowItemUnitInCombo = 138,
		LoadZeroQuantityinGRN = 139,
		AutoCardCode = 140,
		AllowIssueGRNtoProject = 141,
		ShowLotdetailinPrintout = 142,
		ShowInvAging1 = 143,
		ShowInvAging2 = 144,
		ShowInvAging3 = 145,
		ShowInvAging4 = 146,
		AgingInvName1 = 147,
		AgingInvName2 = 148,
		AgingInvName3 = 149,
		AgingInvName4 = 150,
		AgingInvFrom1 = 151,
		AgingInvFrom2 = 152,
		AgingInvFrom3 = 153,
		AgingInvFrom4 = 154,
		AgingInvTo1 = 155,
		AgingInvTo2 = 156,
		AgingInvTo3 = 157,
		AgingInvTo4 = 158,
		ShowItemDetail = 159,
		PDCIssuedByMaturity = 160,
		ShowItemCostInCombo = 161,
		TakeLastSalesPrice = 162,
		ShowItemFeatures = 163,
		AllowDoFollowUponLead = 164,
		AllowCreditSaleInSalesReceipt = 165,
		ShowOnlytOpenInvoices = 166,
		CusFlagRed = 167,
		CusFlagBlue = 168,
		CusFlagOrange = 169,
		CusFlagYellow = 170,
		CusFlagPurple = 171,
		CusFlagGreen = 172,
		VenFlagRed = 173,
		VenFlagBlue = 174,
		VenFlagOrange = 175,
		VenFlagYellow = 176,
		VenFlagPurple = 177,
		VenFlagGreen = 178,
		ItmFlagRed = 179,
		ItmFlagBlue = 180,
		ItmFlagOrange = 181,
		ItmFlagYellow = 182,
		ItmFlagPurple = 183,
		ItmFlagGreen = 184,
		AppraisalPointFrom = 185,
		AppraisalPointTo = 186,
		ReamrkValidationPoint = 187,
		AllowtoeditPOReqDate = 188,
		AllowzeroinSales = 189,
		ActivateBinField = 190,
		AllowESCreatefromPickList = 191,
		AllowtoCreateProjectwithSO = 192,
		ShowItemUPCInCombo = 193,
		AgingByEOMDueDate = 194,
		DaysInMonth = 195,
		ThirtyDays = 196,
		Annual = 197,
		TaxPercentValue = 198,
		DeductiononNetDays = 199,
		AutoResumptionDays = 200,
		ShowOrderAndShipmentDetailInGRN = 201,
		RoundOffSalaryCalculation = 202,
		ShowBOLListinPackingList = 203,
		ConsiderStockinMRPQ = 204,
		FinancialTransactionPosting = 205,
		AllowJobChangeInMRPQ = 206,
		ActivateAutoservice = 207,
		AllowCustomerChangeInDN = 208,
		EnableHRAnalysis = 209,
		EnableVehicleAnalysis = 210,
		EnableLegalAnalysis = 211,
		SpecificationName = 212,
		ExcludeZeroQtyInDN = 213,
		ActivatePartsDetails = 214,
		OtherDescription = 215,
		ItemCodeCreationBasedOn = 216,
		EnableTempSaving = 217,
		DiscountWriteoffPercent = 218,
		ShowMultidimensionOnGrid = 219,
		AgingByEOMInvoiceDate = 220,
		Enablecostingondelete = 221,
		POSDisplayItemFeatures = 222,
		POSChangeSalesPersonWhileSaving = 223,
		DirectTREntry = 224,
		FutureCosting = 225,
		DirectChequeReturn = 226,
		MandatoryPOBOL = 227,
		TaxEntityTypes = 228,
		DefaultTaxOption = 229,
		DefaultTaxGroup = 230,
		BasedonB2C = 231,
		EnableCostRunning = 232,
		MaterialReservationOnSo = 233,
		DocumentVersioning = 234,
		ProductType1Name = 235,
		ProductType2Name = 236,
		ProductType3Name = 237,
		ProductType4Name = 238,
		ProductType5Name = 239,
		ProductType6Name = 240,
		ProductType7Name = 241,
		ProductType8Name = 242,
		DisableCustomerCreditLimit = 243,
		RefSlNo = 244,
		RefText1 = 245,
		RefText2 = 246,
		RefNum1 = 247,
		RefNum2 = 248,
		RefDate1 = 249,
		RefDate2 = 250,
		EnablePatientAnalysis = 251,
		OCR_NumberTitle = 300,
		OCR_DocIDSeparator = 301,
		OCR_FindBarcodes = 302,
		OCR_ImageInversion = 303,
		OCR_FixSkew = 304,
		OCR_AutoRotate = 305,
		OCR_NoiseFilter = 306,
		OCR_RemoveLines = 307,
		OCR_GrayMode = 308,
		OCR_FastMode = 309,
		OCR_BinTwice = 310,
		OCR_CorrectMixed = 311,
		OCR_UseDictionary = 312,
		OCR_BinaryThr = 313,
		OCR_TextQuality = 314,
		OCR_CombineZonesHorz = 315,
		OCR_AssignUnknown = 316,
		DefaultLocationAccounts = 10101,
		SoftClosePeriod = 10102,
		PriceValidationInSQ = 10201,
		LoadZeroQuantityinDN = 10202,
		AllowtoCreateProjectwithSOSysDoc = 10203,
		ActivateSOEditing = 10204,
		PriceLessaThanCostValidation = 10205,
		AllowToChangeSalesInvoicePrice = 10206,
		ActivatePOEditing = 10301,
		ActivateGRNEditing = 10302,
		DefaultRecurringInvoiceSysDocID = 10701,
		None = 1000
	}
}
