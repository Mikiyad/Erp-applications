using Micromind.Common.Data;
using Micromind.Common.Interfaces.WS;
using System;

namespace Micromind.Common.Interfaces
{
	public interface IWebCompanySystem
	{
		bool IsMultiUser
		{
			get;
			set;
		}

		ActivityData GetActiveUsers();

		void RemoveWorkerTokens(RequestInfo requestInfo);

		string SetLoginInfo(RequestInfo requestInfo);

		string SetLoginInfo(RequestInfo requestInfo, bool async);

		string CreateEmptyLogin(RequestInfo requestInfo);

		bool IsConnected(RequestInfo requestInfo);

		void Dispose();

		DateTime GetLastUpdateDate();

		string GetDownloadDirName();

		void Disconnect(RequestInfo requestInfo);

		bool LogClientSignIn(RequestInfo requestInfo);

		IProductSystem CreateProductSystem(RequestInfo requestInfo);

		IUnitSystem CreateUnitSystem(RequestInfo requestInfo);

		IProductCategorySystem CreateProductCategorySystem(RequestInfo requestInfo);

		IEmployeeSystem CreateEmployeeSystem(RequestInfo requestInfo);

		IEmployeeProvisionSystem CreateEmployeeProvisionSystem(RequestInfo requestInfo);

		IShippingMethodSystem CreateShippingMethodSystem(RequestInfo requestInfo);

		ICompanyAccountSystem CreateCompanyAccountSystem(RequestInfo requestInfo);

		ICurrencySystem CreateCurrencySystem(RequestInfo requestInfo);

		IBankSystem CreateBankSystem(RequestInfo requestInfo);

		ITransactionSystem CreateTransactionSystem(RequestInfo requestInfo);

		ITermSystem CreateTermSystem(RequestInfo requestInfo);

		ISecuritySystem CreateSecuritySystem(RequestInfo requestInfo);

		ICompanyInformationSystem CreateCompanyInformationSystem(RequestInfo requestInfo);

		IDatabaseSystem CreateDatabaseSystem(RequestInfo requestInfo);

		IProductStyleSystem CreateProductStyleSystem(RequestInfo requestInfo);

		IProductSpecificationSystem CreateProductSpecificationSystem(RequestInfo requestInfo);

		IProductSizeSystem CreateProductSizeSystem(RequestInfo requestInfo);

		IProductAttributeSystem CreateProductAttributeSystem(RequestInfo requestInfo);

		IProductManufacturerSystem CreateProductManufacturerSystem(RequestInfo requestInfo);

		IReleaseTypeSystem CreateReleaseTypeSystem(RequestInfo requestInfo);

		IProductBrandSystem CreateProductBrandSystem(RequestInfo requestInfo);

		INoteSystem CreateNoteSystem(RequestInfo requestInfo);

		ISettingSystem CreateSettingSystem(RequestInfo requestInfo);

		IMaintenanceSchedulerSystem CreateMaintenanceSchedulerSystem(RequestInfo requestInfo);

		ILeadStatusSystem CreateLeadStatusSystem(RequestInfo requestInfo);

		ICreditLimitReviewSystem CreateCreditLimitReviewSystem(RequestInfo requestInfo);

		IVehicleMaintenanceEntrySystem CreateMaintenanceEntrySystem(RequestInfo requestInfo);

		IHorseTypeSystem CreateHorseTypeSystem(RequestInfo requestInfo);

		IHorseSexSystem CreateHorseSexSystem(RequestInfo requestInfo);

		IEquipmentCategorySystem CreateEquipmentCategorySystem(RequestInfo requestInfo);

		IEquipmentTypeSystem CreateEquipmentTypeSystem(RequestInfo requestInfo);

		IRequisitionTypeSystem CreateRequisitionTypeSystem(RequestInfo requestInfo);

		IRequisitionSystem CreateRequisitionSystem(RequestInfo requestInfo);

		IPrintTemplateSystem CreatePrintTemplateSystem(RequestInfo requestInfo);

		IDepartmentSystem CreateDepartmentSystem(RequestInfo requestInfo);

		IPriceLevelSystem CreatePriceLevelSystem(RequestInfo requestInfo);

		IPaymentMethodSystem CreatePaymentMethodSystem(RequestInfo requestInfo);

		IActivityLogSystem CreateActivityLogSystem(RequestInfo requestInfo);

		ILicenseSystem CreateLicenseSystem(RequestInfo requestInfo);

		IScheduleSystem CreateScheduleSystem(RequestInfo requestInfo);

		IAccountGroupsSystem CreateAccountGroupsSystem(RequestInfo requestInfo);

		IAnalysisGroupsSystem CreateAnalysisGroupsSystem(RequestInfo requestInfo);

		IAnalysisSystem CreateAnalysisSystem(RequestInfo requestInfo);

		ICustomerSystem CreateCustomerSystem(RequestInfo requestInfo);

		ICountrySystem CreateCountrySystem(RequestInfo requestInfo);

		ICustomerClassSystem CreateCustomerClassSystem(RequestInfo requestInfo);

		IAreaSystem CreateAreaSystem(RequestInfo requestInfo);

		ITransporterSystem CreateTransporterSystem(RequestInfo requestInfo);

		IINCOSystem CreateINCOSystem(RequestInfo requestInfo);

		IContainerSizeSystem CreateContainerSizeSystem(RequestInfo requestInfo);

		IAccountAnalysisDetailSystem CreateAccountAnalysisDetailSystem(RequestInfo requestInfo);

		ISalespersonSystem CreateSalespersonSystem(RequestInfo requestInfo);

		ICustomerAddressSystem CreateCustomerAddressSystem(RequestInfo requestInfo);

		IContactSystem CreateContactSystem(RequestInfo requestInfo);

		IVendorClassSystem CreateVendorClassSystem(RequestInfo requestInfo);

		IVendorSystem CreateVendorSystem(RequestInfo requestInfo);

		IVendorAddressSystem CreateVendorAddressSystem(RequestInfo requestInfo);

		IBuyerSystem CreateBuyerSystem(RequestInfo requestInfo);

		IProductClassSystem CreateProductClassSystem(RequestInfo requestInfo);

		IGradeSystem CreateGradeSystem(RequestInfo requestInfo);

		ISponsorSystem CreateSponsorSystem(RequestInfo requestInfo);

		IProvisionTypeSystem CreateProvisionTypeSystem(RequestInfo requestInfo);

		INationalitySystem CreateNationalitySystem(RequestInfo requestInfo);

		IReligionSystem CreateReligionSystem(RequestInfo requestInfo);

		IDivisionSystem CreateDivisionSystem(RequestInfo requestInfo);

		ICompanyDivisionSystem CreateCompanyDivisionSystem(RequestInfo requestInfo);

		IPositionSystem CreatePositionSystem(RequestInfo requestInfo);

		IEmployeeDocTypeSystem CreateEmployeeDocTypeSystem(RequestInfo requestInfo);

		IVehicleDocTypeSystem CreateVehicleDocTypeSystem(RequestInfo requestInfo);

		IDegreeSystem CreateDegreeSystem(RequestInfo requestInfo);

		ISkillSystem CreateSkillSystem(RequestInfo requestInfo);

		ICustomerGroupSystem CreateCustomerGroupSystem(RequestInfo requestInfo);

		IVendorGroupSystem CreateVendorGroupSystem(RequestInfo requestInfo);

		IEmployeeGroupSystem CreateEmployeeGroupSystem(RequestInfo requestInfo);

		IEmployeeAddressSystem CreateEmployeeAddressSystem(RequestInfo requestInfo);

		ILocationSystem CreateLocationSystem(RequestInfo requestInfo);

		IWorkLocationSystem CreateWorkLocationSystem(RequestInfo requestInfo);

		IEmployeeDependentSystem CreateEmployeeDependentSystem(RequestInfo requestInfo);

		IEmployeeDocumentSystem CreateEmployeeDocumentSystem(RequestInfo requestInfo);

		IEmployeeSkillSystem CreateEmployeeSkillSystem(RequestInfo requestInfo);

		ILeaveTypeSystem CreateLeaveTypeSystem(RequestInfo requestInfo);

		IJobTypeSystem CreateJobTypeSystem(RequestInfo requestInfo);

		ICostCategorySystem CreateCostCategorySystem(RequestInfo requestInfo);

		IVehicleDocumentSystem CreateVehicleDocumentSystem(RequestInfo requestInfo);

		IEquipmentSystem CreateEquipmentSystem(RequestInfo requestInfo);

		IEmployeeAppraisalSystem CreateEmployeeAppraisalSystem(RequestInfo requestInfo);

		IEmployeePerformanceSystem CreateEmployeePerformanceSystem(RequestInfo requestInfo);

		IServiceActivitySystem CreateServiceActivitySystem(RequestInfo requestInfo);

		IPayrollItemSystem CreatePayrollItemSystem(RequestInfo requestInfo);

		IDeductionSystem CreateDeductionSystem(RequestInfo requestInfo);

		IBenefitSystem CreateBenefitSystem(RequestInfo requestInfo);

		IEmployeePayrollItemDetailSystem CreateEmployeePayrollItemDetailSystem(RequestInfo requestInfo);

		IEmployeeDeductionDetailSystem CreateEmployeeDeductionDetailSystem(RequestInfo requestInfo);

		IEmployeeBenefitDetailSystem CreateEmployeeBenefitDetailSystem(RequestInfo requestInfo);

		IDestinationSystem CreateDestinationSystem(RequestInfo requestInfo);

		IEmployeeLeaveDetailSystem CreateEmployeeLeaveDetailSystem(RequestInfo requestInfo);

		IEmployeeLeaveProcessSystem CreateEmployeeLeaveProcessSystem(RequestInfo requestInfo);

		ICompanyDocTypeSystem CreateCompanyDocTypeSystem(RequestInfo requestInfo);

		ICompanyDocumentSystem CreateCompanyDocumentSystem(RequestInfo requestInfo);

		ITenancyContractSystem CreateTenancyContractSystem(RequestInfo requestInfo);

		ITradeLicenseSystem CreateTradeLicenseSystem(RequestInfo requestInfo);

		IVisaSystem CreateVisaSystem(RequestInfo requestInfo);

		IJournalSystem CreateJournalSystem(RequestInfo requestInfo);

		ISystemDocumentSystem CreateSystemDocumentSystem(RequestInfo requestInfo);

		ICostCenterSystem CreateCostCenterSystem(RequestInfo requestInfo);

		IRegisterSystem CreateRegisterSystem(RequestInfo requestInfo);

		IChequebookSystem CreateChequebookSystem(RequestInfo requestInfo);

		IIssuedChequeSystem CreateIssuedChequeSystem(RequestInfo requestInfo);

		IReceivedChequeSystem CreateReceivedChequeSystem(RequestInfo requestInfo);

		IReturnedChequeReasonSystem CreateReturnedChequeReasonSystem(RequestInfo requestInfo);

		IAdjustmentTypeSystem CreateAdjustmentTypeSystem(RequestInfo requestInfo);

		IInventoryAdjustmentSystem CreateInventoryAdjustmentSystem(RequestInfo requestInfo);

		IInventoryDamageSystem CreateInventoryDamageSystem(RequestInfo requestInfo);

		IInventoryTransferSystem CreateInventoryTransferSystem(RequestInfo requestInfo);

		IDriverSystem CreateDriverSystem(RequestInfo requestInfo);

		ISalesQuoteSystem CreateSalesQuoteSystem(RequestInfo requestInfo);

		ISalesOrderSystem CreateSalesOrderSystem(RequestInfo requestInfo);

		ISalesEnquirySystem CreateSalesEnquirySystem(RequestInfo requestInfo);

		ISalesProformaSystem CreateSalesProformaSystem(RequestInfo requestInfo);

		IDeliveryNoteSystem CreateDeliveryNoteSystem(RequestInfo requestInfo);

		IItemTransactionSystem CreateItemTransactionSystem(RequestInfo requestInfo);

		IExportPickListSystem CreateExportPickListSystem(RequestInfo requestInfo);

		ISalesInvoiceSystem CreateSalesInvoiceSystem(RequestInfo requestInfo);

		ISalesInvoiceNISystem CreateSalesInvoiceNISystem(RequestInfo requestInfo);

		ISalesReceiptSystem CreateSalesReceiptSystem(RequestInfo requestInfo);

		ISalesReturnSystem CreateSalesReturnSystem(RequestInfo requestInfo);

		ILPOReceiptSystem CreateLPOReceiptSystem(RequestInfo requestInfo);

		IJobInvoiceSystem CreateJobInvoiceSystem(RequestInfo requestInfo);

		IPriceListSystem CreatePriceListSystem(RequestInfo requestInfo);

		IFreightChargeSystem CreateFreightChargeSystem(RequestInfo requestInfo);

		IPartySystem CreatePartySystem(RequestInfo requestInfo);

		ICitySystem CreateCitySystem(RequestInfo requestInfo);

		IVehicleSystem CreateVehicleSystem(RequestInfo requestInfo);

		IContainerTrackingSystem CreateContainerTrackingSystem(RequestInfo requestInfo);

		IDeliveryReturnSystem CreateDeliveryReturnSystem(RequestInfo requestInfo);

		IPurchaseQuoteSystem CreatePurchaseQuoteSystem(RequestInfo requestInfo);

		IPurchaseOrderSystem CreatePurchaseOrderSystem(RequestInfo requestInfo);

		IPurchaseCostEntrySystem CreatePurchaseCostEntrySystem(RequestInfo requestInfo);

		IBillOfLadingSystem CreateBillOfLadingSystem(RequestInfo requestInfo);

		IPurchaseOrderNISystem CreatePurchaseOrderNISystem(RequestInfo requestInfo);

		IFixedAssetPurchaseOrderSystem CreateFixedAssetPurchaseOrderSystem(RequestInfo requestInfo);

		IProjectSubContractPOSystem CreateProjectSubContractSystem(RequestInfo requestInfo);

		IPOShipmentSystem CreatePOShipmentSystem(RequestInfo requestInfo);

		IPortSystem CreatePortSystem(RequestInfo requestInfo);

		IPurchaseInvoiceSystem CreatePurchaseInvoiceSystem(RequestInfo requestInfo);

		IPurchaseInvoiceNISystem CreatePurchaseInvoiceNISystem(RequestInfo requestInfo);

		IPurchaseReceiptSystem CreatePurchaseReceiptSystem(RequestInfo requestInfo);

		IPurchaseReturnSystem CreatePurchaseReturnSystem(RequestInfo requestInfo);

		IShortcutSystem CreateShortcutSystem(RequestInfo requestInfo);

		IEmployeeActivitySystem CreateEmployeeActivitySystem(RequestInfo requestInfo);

		IDisciplineActionTypeSystem CreateDisciplineActionTypeSystem(RequestInfo requestInfo);

		IEmployeeActivityTypeSystem CreateEmployeeActivityTypeSystem(RequestInfo requestInfo);

		ISalarySheetSystem CreateSalarySheetSystem(RequestInfo requestInfo);

		IEmployeeTypeSystem CreateEmployeeTypeSystem(RequestInfo requestInfo);

		IPayrollTransactionSystem CreatePayrollTransactionSystem(RequestInfo requestInfo);

		IEmployeeLoanSystem CreateEmployeeLoanSystem(RequestInfo requestInfo);

		IEmployeeLoanTypeSystem CreateEmployeeLoanTypeSystem(RequestInfo requestInfo);

		IARJournalSystem CreateARJournalSystem(RequestInfo requestInfo);

		IAPJournalSystem CreateAPJournalSystem(RequestInfo requestInfo);

		IUserSystem CreateUserSystem(RequestInfo requestInfo);

		IUserGroupSystem CreateUserGroupSystem(RequestInfo requestInfo);

		IReminderSystem CreateReminderSystem(RequestInfo requestInfo);

		ICompanyAddressSystem CreateCompanyAddressSystem(RequestInfo requestInfo);

		ISmartListSystem CreateSmartListSystem(RequestInfo requestInfo);

		IExternalReportSystem CreateExternalReportSystem(RequestInfo requestInfo);

		ISalesPOSSystem CreateSalesPOSSystem(RequestInfo requestInfo);

		IPOSBatchSystem CreatePOSBatchSystem(RequestInfo requestInfo);

		IPOSShiftSystem CreatePOSShiftSystem(RequestInfo requestInfo);

		IPOSCashRegisterSystem CreatePOSCashRegisterSystem(RequestInfo requestInfo);

		IPOSHoldSystem CreatePOSHoldSystem(RequestInfo requestInfo);

		IConsignOutSystem CreateConsignOutSystem(RequestInfo requestInfo);

		IGarmentRentalSystem CreateGarmentRentalSystem(RequestInfo requestInfo);

		IGarmentRentalReturnSystem CreateGarmentRentalReturnSystem(RequestInfo requestInfo);

		IConsignOutSettlementSystem CreateConsignOutSettlementSystem(RequestInfo requestInfo);

		IExpenseCodeSystem CreateExpenseCodeSystem(RequestInfo requestInfo);

		ICustomerCategorySystem CreateCustomerCategorySystem(RequestInfo requestInfo);

		ICompanyOptionSystem CreateCompanyOptionSystem(RequestInfo requestInfo);

		IConsignOutReturnSystem CreateConsignOutReturnSystem(RequestInfo requestInfo);

		IConsignInSystem CreateConsignInSystem(RequestInfo requestInfo);

		IConsignInSettlementSystem CreateConsignInSettlementSystem(RequestInfo requestInfo);

		IConsignInReturnSystem CreateConsignInReturnSystem(RequestInfo requestInfo);

		IFixedAssetSystem CreateFixedAssetSystem(RequestInfo requestInfo);

		IFixedAssetLocationSystem CreateFixedAssetLocationSystem(RequestInfo requestInfo);

		IFixedAssetGroupSystem CreateFixedAssetGroupSystem(RequestInfo requestInfo);

		IFixedAssetClassSystem CreateFixedAssetClassSystem(RequestInfo requestInfo);

		IFixedAssetPurchaseSystem CreateFixedAssetPurchaseSystem(RequestInfo requestInfo);

		IFixedAssetTransferSystem CreateFixedAssetTransferSystem(RequestInfo requestInfo);

		IFixedAssetSaleSystem CreateFixedAssetSaleSystem(RequestInfo requestInfo);

		IFixedAssetDepSystem CreateFixedAssetDepSystem(RequestInfo requestInfo);

		IDimensionSystem CreateDimensionSystem(RequestInfo requestInfo);

		IProductParentSystem CreateProductParentSystem(RequestInfo requestInfo);

		IMatrixTemplateSystem CreateMatrixTemplateSystem(RequestInfo requestInfo);

		IFiscalYearSystem CreateFiscalYearSystem(RequestInfo requestInfo);

		ILeadSystem CreateLeadSystem(RequestInfo requestInfo);

		ILeadAddressSystem CreateLeadAddressSystem(RequestInfo requestInfo);

		IGenericListSystem CreateGenericListSystem(RequestInfo requestInfo);

		IUDFSystem CreateUDFSystem(RequestInfo requestInfo);

		IPivotGroupSystem CreatePivotGroupSystem(RequestInfo requestInfo);

		IPivotSystem CreatePivotSystem(RequestInfo requestInfo);

		IAssemblyBuildSystem CreateAssemblyBuildSystem(RequestInfo requestInfo);

		IProductionSystem CreateProductionSystem(RequestInfo requestInfo);

		IInventoryRepackingSystem CreateInventoryRepackingSystem(RequestInfo requestInfo);

		IWorkOrderSystem CreateWorkOrderSystem(RequestInfo requestInfo);

		IBOMSystem CreateBOMSystem(RequestInfo requestInfo);

		IEOSRuleSystem CreateEOSRuleSystem(RequestInfo requestInfo);

		IOverTimeSystem CreateOverTimeSystem(RequestInfo requestInfo);

		IEntityDocSystem CreateEntityDocSystem(RequestInfo requestInfo);

		IJobSystem CreateJobSystem(RequestInfo requestInfo);

		IOpportunitySystem CreateOpportunitySystem(RequestInfo requestInfo);

		ICompetitorSystem CreateCompetitorSystem(RequestInfo requestInfo);

		IActivitySystem CreateActivitySystem(RequestInfo requestInfo);

		IFollowupSystem CreateFollowupSystem(RequestInfo requestInfo);

		ICustomReportSystem CreateCustomReportSystem(RequestInfo requestInfo);

		ICampaignSystem CreateCampaignSystem(RequestInfo requestInfo);

		IEntityCategorySystem CreateEntityCategorySystem(RequestInfo requestInfo);

		IEventSystem CreateEventSystem(RequestInfo requestInfo);

		IJobInventoryIssueSystem CreateJobInventoryIssueSystem(RequestInfo requestInfo);

		IJobMaterialRequisitionSystem CreateJobMaterialRequisitionSystem(RequestInfo requestInfo);

		IJobMaterialEstimateSystem CreateJobMaterialEstimateSystem(RequestInfo requestInfo);

		IJobManHrsBudgetingSystem CreateJobManHrsBudgetingSystem(RequestInfo requestInfo);

		IJobMaintenanceScheduleSystem CreateJobMaintenanceScheduleSystem(RequestInfo requestInfo);

		IJobMaintenanceServiceSystem CreateJobMaintenanceServiceSystem(RequestInfo requestInfo);

		IServiceCallTrackSystem CreateServiceCallTrackSystem(RequestInfo requestInfo);

		IJobInventoryReturnSystem CreateJobInventoryReturnSystem(RequestInfo requestInfo);

		IJobExpenseIssueSystem CreateJobExpenseIssueSystem(RequestInfo requestInfo);

		IJobTimesheetSystem CreateJobTimesheetSystem(RequestInfo requestInfo);

		IBankFacilityGroupSystem CreateBankFacilityGroupSystem(RequestInfo requestInfo);

		IBankFacilitySystem CreateBankFacilitySystem(RequestInfo requestInfo);

		IOpeningBalanceBatchSystem CreateOpeningBalanceBatchSystem(RequestInfo requestInfo);

		IPhysicalStockEntrySystem CreatePhysicalStockEntrySystem(RequestInfo requestInfo);

		IOpeningBalanceLeaveSystem CreateOpeningBalanceLeaveSystem(RequestInfo requestInfo);

		IBankReconciliationSystem CreateBankReconciliationSystem(RequestInfo requestInfo);

		IBankFacilityTransactionSystem CreateBankFacilityTransactionSystem(RequestInfo requestInfo);

		IBankFacilityPaymentSystem CreateBankFacilityPaymentSystem(RequestInfo requestInfo);

		ICustomGadgetSystem CreateCustomGadgetSystem(RequestInfo requestInfo);

		IOverTimeEntrySystem CreateOverTimeEntrySystem(RequestInfo requestInfo);

		IHolidayCalendarSystem CreateHolidayCalendarSystem(RequestInfo requestInfo);

		ICandidateSystem CreateCandidateSystem(RequestInfo requestInfo);

		ISalarySystem CreateSalarySystem(RequestInfo requestInfo);

		IDiscountChequeSystem CreateDiscountChequeSystem(RequestInfo requestInfo);

		ISendChequeSystem CreateSendChequeSystem(RequestInfo requestInfo);

		IInventoryTransferTypeSystem CreateInventoryTransferTypeSystem(RequestInfo requestInfo);

		IExportPackingListSystem CreateExportPackingListSystem(RequestInfo requestInfo);

		IStandingJournalSystem CreateStandingJournalSystem(RequestInfo requestInfo);

		IDashboardSystem CreateDashboardSystem(RequestInfo requestInfo);

		IWebDashboardSystem CreateWebDashboardSystem(RequestInfo requestInfo);

		ICollateralSystem CreateCollateralSystem(RequestInfo requestInfo);

		IJobTaskSystem CreateJobTaskSystem(RequestInfo requestInfo);

		IClientAssetSystem CreateClientAssetSystem(RequestInfo requestInfo);

		IGRNReturnSystem CreateGRNReturnSystem(RequestInfo requestInfo);

		IBinSystem CreateBinSystem(RequestInfo requestInfo);

		IRouteSystem CreateRouteSystem(RequestInfo requestInfo);

		IRouteGroupSystem CreateRouteGroupSystem(RequestInfo requestInfo);

		IRackSystem CreateRackSystem(RequestInfo requestInfo);

		IQualityClaimSystem CreateQualityClaimSystem(RequestInfo requestInfo);

		IArrivalReportSystem CreateArrivalReportSystem(RequestInfo requestInfo);

		IArrivalReportTemplateSystem CreateArrivalReportTemplateSystem(RequestInfo requestInfo);

		IQualityTaskSystem CreateQualityTaskSystem(RequestInfo requestInfo);

		ISurveyorSystem CreateSurveyorSystem(RequestInfo requestInfo);

		IApprovalSystem CreateApprovalSystem(RequestInfo requestInfo);

		IPropertyAgentSystem CreatePropertyAgentSystem(RequestInfo requestInfo);

		IPropertyClassSystem CreatePropertyClassSystem(RequestInfo requestInfo);

		IPropertySystem CreatePropertySystem(RequestInfo requestInfo);

		IPropertyUnitSystem CreatePropertyUnitSystem(RequestInfo requestInfo);

		IPropertyVirtualUnitSystem CreatePropertyVirtualUnitSystem(RequestInfo requestInfo);

		IPropertyCategorySystem CreatePropertyCategorySystem(RequestInfo requestInfo);

		ICheckListSystem CreateCheckListSystem(RequestInfo requestInfo);

		IPaymentRequestSystem CreatePaymentRequestSystem(RequestInfo requestInfo);

		IPropertyIncomeCodeSystem CreatePropertyIncomeCodeSystem(RequestInfo requestInfo);

		IPropertyRentSystem CreatePropertyRentSystem(RequestInfo requestInfo);

		IPropertyCancelSystem CreatePropertyCancelSystem(RequestInfo requestInfo);

		IRentalPostingSystem CreateRentalPostingSystem(RequestInfo requestInfo);

		IW3PLGRNSystem CreateW3PLGRNSystem(RequestInfo requestInfo);

		IW3PLDeliverySystem CreateW3PLDeliverySystem(RequestInfo requestInfo);

		IW3PLInvoiceSystem CreateW3PLInvoiceSystem(RequestInfo requestInfo);

		IProjectExpenseAllocationSystem CreateProjectExpenseAllocationSystem(RequestInfo requestInfo);

		IPurchaseClaimSystem CreatePurchaseClaimSystem(RequestInfo requestInfo);

		IPropertyServiceSystem CreatePropertyServiceSystem(RequestInfo requestInfo);

		IJobBOMSystem CreateJobBOMSystem(RequestInfo requestInfo);

		IEmailMessageSystem CreateEmailMessageSystem(RequestInfo requestInfo);

		IServiceTypeSystem CreateServiceTypeSystem(RequestInfo requestInfo);

		IJobEstimationSystem CreateJobEstimationSystem(RequestInfo requestInfo);

		ICLVoucherSystem CreateCLVoucherSystem(RequestInfo requestInfo);

		ICLTokenSystem CreateCLTokenSystem(RequestInfo requestInfo);

		IEmployeeEOSSettlementSystem CreateEmployeeEOSSettlementSystem(RequestInfo requestInfo);

		IFixedAssetBulkPurchaseSystem CreateFixedAssetBulkPurchaseSystem(RequestInfo requestInfo);

		IEntityCommentSystem CreateEntityCommentSystem(RequestInfo requestInfo);

		IInsuranceProviderSystem CreateInsuranceProviderSystem(RequestInfo requestInfo);

		ICustomerInsuranceSystem CreateCustomerInsuranceSystem(RequestInfo requestInfo);

		IRiderSummarySystem CreateRiderSummarySystem(RequestInfo requestInfo);

		IHorseSummarySystem CreateHorseSummarySystem(RequestInfo requestInfo);

		IProjectSubContractPISystem CreateProjectSubContractPISystem(RequestInfo requestInfo);

		IEntityFlagSystem CreateEntityFlagSystem(RequestInfo requestInfo);

		IEAEquipmentSystem CreateEAEquipmentSystem(RequestInfo requestInfo);

		IMobilizationSystem CreateMobilizationSystem(RequestInfo requestInfo);

		IEquipmentTransferSystem CreateEquipmentTransferSystem(RequestInfo requestInfo);

		IEquipmentWorkOrderSystem CreateEquipmentWorkOrderSystem(RequestInfo requestInfo);

		ILawyerSystem CreateLawyerSystem(RequestInfo requestInfo);

		ICasePartySystem CreateCasePartySystem(RequestInfo requestInfo);

		IWorkOrderInventoryIssueSystem CreateWorkOrderInventoryIssueSystem(RequestInfo requestInfo);

		ILegalActivitySystem CreateLegalActivitySystem(RequestInfo requestInfo);

		IWorkOrderInventoryReturnSystem CreateWorkOrderInventoryReturnSystem(RequestInfo requestInfo);

		ITaxSystem CreateTaxSystem(RequestInfo requestInfo);

		ITaxGroupSystem CreateTaxGroupSystem(RequestInfo requestInfo);

		IInventoryDismantleSystem CreateInventoryDismantleSystem(RequestInfo requestInfo);

		IProductPriceBulkUpdateSystem CreateProductPriceBulkpdateSystem(RequestInfo requestInfo);

		IOpeningEntryTransactionSystem CreateOpeningEntryTransactionSystem(RequestInfo requestInfo);

		IMaterialReservationSystem CreateMaterialReservationSystem(RequestInfo requestInfo);

		ICaseClientSystem CreateCaseClientSystem(RequestInfo requestInfo);

		ILegalActionSystem CreateLegalActionSystem(RequestInfo requestInfo);

		ISalesForecastingSystem CreateSalesForecastingSystem(RequestInfo requestInfo);

		IProductMakeSystem CreateProductMakeSystem(RequestInfo requestInfo);

		IProductTypeSystem CreateProductTypeSystem(RequestInfo requestInfo);

		IProductModelSystem CreateProductModelSystem(RequestInfo requestInfo);

		IUserFavoriteSystem CreateUserFavoriteSystem(RequestInfo requestInfo);

		IPurchasePrepaymentInvoiceSystem CreatePurchasePrepaymentInvoiceSystem(RequestInfo requestInfo);

		ISalespersonGroupSystem CreateSalespersonGroupSystem(RequestInfo requestInfo);

		ITaskStepsSystem CreateTaskStepsSystem(RequestInfo requestInfo);

		ITaskTypeSystem CreateTaskTypeSystem(RequestInfo requestInfo);

		ITaskTransactionSystem CreateTaskTransactionSystem(RequestInfo requestInfo);

		ITaskTransactionStatusSystem CreateTaskTransactionStatusSystem(RequestInfo requestInfo);

		ILegalActionStatusSystem CreateLegalActionStatusSystem(RequestInfo requestInfo);

		ITRApplicationSystem CreateTRApplicationSystem(RequestInfo requestInfo);

		IBudgetingSystem CreateBudgetingSystem(RequestInfo requestInfo);

		IVehicleMileageTrackSystem CreateVehicleMileageTrackSystem(RequestInfo requestInfo);

		ISalesManTargetSystem CreateSalesManTargetSystem(RequestInfo requestInfo);

		ICustomerInsuranceClaimSystem CreateCustomerInsuranceClaimSystem(RequestInfo requestInfo);

		IPropertyDocTypeSystem CreatePropertyDocTypeSystem(RequestInfo requestInfo);

		IPropertyDocumentSystem CreatePropertyDocumentSystem(RequestInfo requestInfo);

		IPropertyTenantDocumentSystem CreatePropertyTenantDocumentSystem(RequestInfo requestInfo);

		IPropertyTenantDocTypeSystem CreatePropertyTenantDocTypeSystem(RequestInfo requestInfo);

		ILoanEntrySystem CreateLoanEntrySystem(RequestInfo requestInfo);

		IPrintTemplateMapSystem CreatePrintTemplateMapSystem(RequestInfo requestInfo);

		IRecurringInvoiceSystem CreateRecurringInvoiceSystem(RequestInfo requestInfo);
	}
}
