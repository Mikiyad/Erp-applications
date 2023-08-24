using Micromind.Common.Data;
using Micromind.Common.Interfaces.WS;
using Micromind.Securities;
using System;

namespace Micromind.Common.Interfaces
{
	public interface ICompanySystem
	{
		bool IsMultiUser
		{
			get;
			set;
		}

		ActivityData GetActiveUsers();

		bool RemoveUser(string yourToken, string userID, string machineID, string dbName);

		void RemoveWorkerTokens(string currentToken, string userID, string computerName);

		string SetLoginInfo(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string dbName, string userID, string password, string dbAdminUser, string dbAdminPassword, string id, ConnectionTypes connectionType);

		string SetLoginInfo(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string dbName, string userID, string password, string dbAdminUser, string dbAdminPassword, string id, ConnectionTypes connectionType, bool async);

		string CreateEmptyLogin(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string id);

		bool IsConnected(string token);

		void Dispose();

		DateTime GetLastUpdateDate();

		string GetDownloadDirName();

		void Disconnect(string token);

		bool LogClientSignIn(string token);

		IProductSystem CreateProductSystem(string token);

		IUnitSystem CreateUnitSystem(string token);

		IProductCategorySystem CreateProductCategorySystem(string token);

		IEmployeeSystem CreateEmployeeSystem(string token);

		IEmployeeProvisionSystem CreateEmployeeProvisionSystem(string token);

		IShippingMethodSystem CreateShippingMethodSystem(string token);

		ICompanyAccountSystem CreateCompanyAccountSystem(string token);

		ICurrencySystem CreateCurrencySystem(string token);

		IBankSystem CreateBankSystem(string token);

		ITransactionSystem CreateTransactionSystem(string token);

		ITermSystem CreateTermSystem(string token);

		ISecuritySystem CreateSecuritySystem(string token);

		ICompanyInformationSystem CreateCompanyInformationSystem(string token);

		IDatabaseSystem CreateDatabaseSystem(string token);

		IProductStyleSystem CreateProductStyleSystem(string token);

		IProductSpecificationSystem CreateProductSpecificationSystem(string token);

		IProductSizeSystem CreateProductSizeSystem(string token);

		IProductAttributeSystem CreateProductAttributeSystem(string token);

		IProductManufacturerSystem CreateProductManufacturerSystem(string token);

		IReleaseTypeSystem CreateReleaseTypeSystem(string token);

		IProductBrandSystem CreateProductBrandSystem(string token);

		INoteSystem CreateNoteSystem(string token);

		ISettingSystem CreateSettingSystem(string token);

		IMaintenanceSchedulerSystem CreateMaintenanceSchedulerSystem(string token);

		ILeadStatusSystem CreateLeadStatusSystem(string token);

		ICreditLimitReviewSystem CreateCreditLimitReviewSystem(string token);

		IVehicleMaintenanceEntrySystem CreateMaintenanceEntrySystem(string token);

		IHorseTypeSystem CreateHorseTypeSystem(string token);

		IHorseSexSystem CreateHorseSexSystem(string token);

		IEquipmentCategorySystem CreateEquipmentCategorySystem(string token);

		IEquipmentTypeSystem CreateEquipmentTypeSystem(string token);

		IRequisitionTypeSystem CreateRequisitionTypeSystem(string token);

		IRequisitionSystem CreateRequisitionSystem(string token);

		IPrintTemplateSystem CreatePrintTemplateSystem(string token);

		IDepartmentSystem CreateDepartmentSystem(string token);

		IPriceLevelSystem CreatePriceLevelSystem(string token);

		IPaymentMethodSystem CreatePaymentMethodSystem(string token);

		IActivityLogSystem CreateActivityLogSystem(string token);

		ILicenseSystem CreateLicenseSystem(string token);

		IScheduleSystem CreateScheduleSystem(string token);

		IAccountGroupsSystem CreateAccountGroupsSystem(string token);

		IAnalysisGroupsSystem CreateAnalysisGroupsSystem(string token);

		IAnalysisSystem CreateAnalysisSystem(string token);

		ICustomerSystem CreateCustomerSystem(string token);

		ICountrySystem CreateCountrySystem(string token);

		ICustomerClassSystem CreateCustomerClassSystem(string token);

		IAreaSystem CreateAreaSystem(string token);

		ITransporterSystem CreateTransporterSystem(string token);

		IINCOSystem CreateINCOSystem(string token);

		IContainerSizeSystem CreateContainerSizeSystem(string token);

		IAccountAnalysisDetailSystem CreateAccountAnalysisDetailSystem(string token);

		ISalespersonSystem CreateSalespersonSystem(string token);

		ICustomerAddressSystem CreateCustomerAddressSystem(string token);

		IContactSystem CreateContactSystem(string token);

		IVendorClassSystem CreateVendorClassSystem(string token);

		IVendorSystem CreateVendorSystem(string token);

		IVendorAddressSystem CreateVendorAddressSystem(string token);

		IBuyerSystem CreateBuyerSystem(string token);

		IProductClassSystem CreateProductClassSystem(string token);

		IGradeSystem CreateGradeSystem(string token);

		ISponsorSystem CreateSponsorSystem(string token);

		IProvisionTypeSystem CreateProvisionTypeSystem(string token);

		INationalitySystem CreateNationalitySystem(string token);

		IReligionSystem CreateReligionSystem(string token);

		IDivisionSystem CreateDivisionSystem(string token);

		ICompanyDivisionSystem CreateCompanyDivisionSystem(string token);

		IPositionSystem CreatePositionSystem(string token);

		IEmployeeDocTypeSystem CreateEmployeeDocTypeSystem(string token);

		IPatientDocTypeSystem CreatePatientDocTypeSystem(string token);

		IVehicleDocTypeSystem CreateVehicleDocTypeSystem(string token);

		IDegreeSystem CreateDegreeSystem(string token);

		ISkillSystem CreateSkillSystem(string token);

		ICustomerGroupSystem CreateCustomerGroupSystem(string token);

		IVendorGroupSystem CreateVendorGroupSystem(string token);

		IEmployeeGroupSystem CreateEmployeeGroupSystem(string token);

		IEmployeeAddressSystem CreateEmployeeAddressSystem(string token);

		ILocationSystem CreateLocationSystem(string token);

		IWorkLocationSystem CreateWorkLocationSystem(string token);

		IEmployeeDependentSystem CreateEmployeeDependentSystem(string token);

		IEmployeeDocumentSystem CreateEmployeeDocumentSystem(string token);

		IPatientDocumentSystem CreatePatientDocumentSystem(string token);

		IEmployeeSkillSystem CreateEmployeeSkillSystem(string token);

		ILeaveTypeSystem CreateLeaveTypeSystem(string token);

		IJobTypeSystem CreateJobTypeSystem(string token);

		IJobTaskGroupSystem CreateJobTaskGroupSystem(string token);

		ICostCategorySystem CreateCostCategorySystem(string token);

		IVehicleDocumentSystem CreateVehicleDocumentSystem(string token);

		IEquipmentSystem CreateEquipmentSystem(string token);

		IEmployeeAppraisalSystem CreateEmployeeAppraisalSystem(string token);

		IEmployeePerformanceSystem CreateEmployeePerformanceSystem(string token);

		IServiceActivitySystem CreateServiceActivitySystem(string token);

		IPayrollItemSystem CreatePayrollItemSystem(string token);

		IDeductionSystem CreateDeductionSystem(string token);

		IBenefitSystem CreateBenefitSystem(string token);

		IEmployeePayrollItemDetailSystem CreateEmployeePayrollItemDetailSystem(string token);

		IEmployeeDeductionDetailSystem CreateEmployeeDeductionDetailSystem(string token);

		IEmployeeBenefitDetailSystem CreateEmployeeBenefitDetailSystem(string token);

		IDestinationSystem CreateDestinationSystem(string token);

		IEmployeeLeaveDetailSystem CreateEmployeeLeaveDetailSystem(string token);

		IEmployeeLeaveProcessSystem CreateEmployeeLeaveProcessSystem(string token);

		ICompanyDocTypeSystem CreateCompanyDocTypeSystem(string token);

		ICompanyDocumentSystem CreateCompanyDocumentSystem(string token);

		ITenancyContractSystem CreateTenancyContractSystem(string token);

		ITradeLicenseSystem CreateTradeLicenseSystem(string token);

		IVisaSystem CreateVisaSystem(string token);

		IJournalSystem CreateJournalSystem(string token);

		ISystemDocumentSystem CreateSystemDocumentSystem(string token);

		ICostCenterSystem CreateCostCenterSystem(string token);

		IRegisterSystem CreateRegisterSystem(string token);

		IChequebookSystem CreateChequebookSystem(string token);

		IIssuedChequeSystem CreateIssuedChequeSystem(string token);

		IReceivedChequeSystem CreateReceivedChequeSystem(string token);

		IReturnedChequeReasonSystem CreateReturnedChequeReasonSystem(string token);

		IAdjustmentTypeSystem CreateAdjustmentTypeSystem(string token);

		IInventoryAdjustmentSystem CreateInventoryAdjustmentSystem(string token);

		IInventoryDamageSystem CreateInventoryDamageSystem(string token);

		IInventoryTransferSystem CreateInventoryTransferSystem(string token);

		IDriverSystem CreateDriverSystem(string token);

		ISalesQuoteSystem CreateSalesQuoteSystem(string token);

		ISalesOrderSystem CreateSalesOrderSystem(string token);

		ISalesEnquirySystem CreateSalesEnquirySystem(string token);

		ISalesProformaSystem CreateSalesProformaSystem(string token);

		IDeliveryNoteSystem CreateDeliveryNoteSystem(string token);

		IItemTransactionSystem CreateItemTransactionSystem(string token);

		IExportPickListSystem CreateExportPickListSystem(string token);

		ISalesInvoiceSystem CreateSalesInvoiceSystem(string token);

		ISalesInvoiceNISystem CreateSalesInvoiceNISystem(string token);

		ISalesReceiptSystem CreateSalesReceiptSystem(string token);

		ISalesReturnSystem CreateSalesReturnSystem(string token);

		ILPOReceiptSystem CreateLPOReceiptSystem(string token);

		IJobInvoiceSystem CreateJobInvoiceSystem(string token);

		IPriceListSystem CreatePriceListSystem(string token);

		IFreightChargeSystem CreateFreightChargeSystem(string token);

		IPartySystem CreatePartySystem(string token);

		ICitySystem CreateCitySystem(string token);

		IVehicleSystem CreateVehicleSystem(string token);

		IContainerTrackingSystem CreateContainerTrackingSystem(string token);

		IDeliveryReturnSystem CreateDeliveryReturnSystem(string token);

		IPurchaseQuoteSystem CreatePurchaseQuoteSystem(string token);

		IPurchaseOrderSystem CreatePurchaseOrderSystem(string token);

		IPurchaseCostEntrySystem CreatePurchaseCostEntrySystem(string token);

		IBillOfLadingSystem CreateBillOfLadingSystem(string token);

		IPurchaseOrderNISystem CreatePurchaseOrderNISystem(string token);

		IFixedAssetPurchaseOrderSystem CreateFixedAssetPurchaseOrderSystem(string token);

		IProjectSubContractPOSystem CreateProjectSubContractSystem(string token);

		IPOShipmentSystem CreatePOShipmentSystem(string token);

		IPortSystem CreatePortSystem(string token);

		IPurchaseInvoiceSystem CreatePurchaseInvoiceSystem(string token);

		IPurchaseInvoiceNISystem CreatePurchaseInvoiceNISystem(string token);

		IPurchaseReceiptSystem CreatePurchaseReceiptSystem(string token);

		IPurchaseReturnSystem CreatePurchaseReturnSystem(string token);

		IShortcutSystem CreateShortcutSystem(string token);

		IEmployeeActivitySystem CreateEmployeeActivitySystem(string token);

		IDisciplineActionTypeSystem CreateDisciplineActionTypeSystem(string token);

		IEmployeeActivityTypeSystem CreateEmployeeActivityTypeSystem(string token);

		ISalarySheetSystem CreateSalarySheetSystem(string token);

		IEmployeeTypeSystem CreateEmployeeTypeSystem(string token);

		IPayrollTransactionSystem CreatePayrollTransactionSystem(string token);

		IEmployeeLoanSystem CreateEmployeeLoanSystem(string token);

		IEmployeeLoanTypeSystem CreateEmployeeLoanTypeSystem(string token);

		IARJournalSystem CreateARJournalSystem(string token);

		IAPJournalSystem CreateAPJournalSystem(string token);

		IUserSystem CreateUserSystem(string token);

		IUserGroupSystem CreateUserGroupSystem(string token);

		IReminderSystem CreateReminderSystem(string token);

		ICompanyAddressSystem CreateCompanyAddressSystem(string token);

		ISmartListSystem CreateSmartListSystem(string token);

		IExternalReportSystem CreateExternalReportSystem(string token);

		ISalesPOSSystem CreateSalesPOSSystem(string token);

		IPOSBatchSystem CreatePOSBatchSystem(string token);

		IPOSShiftSystem CreatePOSShiftSystem(string token);

		IPOSCashRegisterSystem CreatePOSCashRegisterSystem(string token);

		IPOSHoldSystem CreatePOSHoldSystem(string token);

		IConsignOutSystem CreateConsignOutSystem(string token);

		IGarmentRentalSystem CreateGarmentRentalSystem(string token);

		IGarmentRentalReturnSystem CreateGarmentRentalReturnSystem(string token);

		IConsignOutSettlementSystem CreateConsignOutSettlementSystem(string token);

		IExpenseCodeSystem CreateExpenseCodeSystem(string token);

		ICustomerCategorySystem CreateCustomerCategorySystem(string token);

		ICompanyOptionSystem CreateCompanyOptionSystem(string token);

		IConsignOutReturnSystem CreateConsignOutReturnSystem(string token);

		IConsignInSystem CreateConsignInSystem(string token);

		IConsignInSettlementSystem CreateConsignInSettlementSystem(string token);

		IConsignInReturnSystem CreateConsignInReturnSystem(string token);

		IFixedAssetSystem CreateFixedAssetSystem(string token);

		IFixedAssetLocationSystem CreateFixedAssetLocationSystem(string token);

		IFixedAssetGroupSystem CreateFixedAssetGroupSystem(string token);

		IFixedAssetClassSystem CreateFixedAssetClassSystem(string token);

		IFixedAssetPurchaseSystem CreateFixedAssetPurchaseSystem(string token);

		IFixedAssetTransferSystem CreateFixedAssetTransferSystem(string token);

		IFixedAssetSaleSystem CreateFixedAssetSaleSystem(string token);

		IFixedAssetDepSystem CreateFixedAssetDepSystem(string token);

		IDimensionSystem CreateDimensionSystem(string token);

		IProductParentSystem CreateProductParentSystem(string token);

		IMatrixTemplateSystem CreateMatrixTemplateSystem(string token);

		IFiscalYearSystem CreateFiscalYearSystem(string token);

		ILeadSystem CreateLeadSystem(string token);

		ILeadAddressSystem CreateLeadAddressSystem(string token);

		IGenericListSystem CreateGenericListSystem(string token);

		IUDFSystem CreateUDFSystem(string token);

		IPivotGroupSystem CreatePivotGroupSystem(string token);

		IPivotSystem CreatePivotSystem(string token);

		IAssemblyBuildSystem CreateAssemblyBuildSystem(string token);

		IProductionSystem CreateProductionSystem(string token);

		IInventoryRepackingSystem CreateInventoryRepackingSystem(string token);

		IWorkOrderSystem CreateWorkOrderSystem(string token);

		IBOMSystem CreateBOMSystem(string token);

		IEOSRuleSystem CreateEOSRuleSystem(string token);

		IOverTimeSystem CreateOverTimeSystem(string token);

		IEntityDocSystem CreateEntityDocSystem(string token);

		IJobSystem CreateJobSystem(string token);

		IOpportunitySystem CreateOpportunitySystem(string token);

		ICompetitorSystem CreateCompetitorSystem(string token);

		IActivitySystem CreateActivitySystem(string token);

		IFollowupSystem CreateFollowupSystem(string token);

		ICustomReportSystem CreateCustomReportSystem(string token);

		ICampaignSystem CreateCampaignSystem(string token);

		IEntityCategorySystem CreateEntityCategorySystem(string token);

		IEventSystem CreateEventSystem(string token);

		IJobInventoryIssueSystem CreateJobInventoryIssueSystem(string token);

		IJobMaterialRequisitionSystem CreateJobMaterialRequisitionSystem(string token);

		IJobMaterialEstimateSystem CreateJobMaterialEstimateSystem(string token);

		IJobManHrsBudgetingSystem CreateJobManHrsBudgetingSystem(string token);

		IJobMaintenanceScheduleSystem CreateJobMaintenanceScheduleSystem(string token);

		IJobMaintenanceServiceSystem CreateJobMaintenanceServiceSystem(string token);

		IServiceCallTrackSystem CreateServiceCallTrackSystem(string token);

		IJobInventoryReturnSystem CreateJobInventoryReturnSystem(string token);

		IJobExpenseIssueSystem CreateJobExpenseIssueSystem(string token);

		IJobTimesheetSystem CreateJobTimesheetSystem(string token);

		IBankFacilityGroupSystem CreateBankFacilityGroupSystem(string token);

		IBankFacilitySystem CreateBankFacilitySystem(string token);

		IOpeningBalanceBatchSystem CreateOpeningBalanceBatchSystem(string token);

		IPhysicalStockEntrySystem CreatePhysicalStockEntrySystem(string token);

		IOpeningBalanceLeaveSystem CreateOpeningBalanceLeaveSystem(string token);

		IBankReconciliationSystem CreateBankReconciliationSystem(string token);

		IBankFacilityTransactionSystem CreateBankFacilityTransactionSystem(string token);

		IBankFacilityPaymentSystem CreateBankFacilityPaymentSystem(string token);

		ICustomGadgetSystem CreateCustomGadgetSystem(string token);

		IOverTimeEntrySystem CreateOverTimeEntrySystem(string token);

		IHolidayCalendarSystem CreateHolidayCalendarSystem(string token);

		ICandidateSystem CreateCandidateSystem(string token);

		ISalarySystem CreateSalarySystem(string token);

		IDiscountChequeSystem CreateDiscountChequeSystem(string token);

		IDiscountBillSystem CreateDiscountBillSystem(string token);

		ISendChequeSystem CreateSendChequeSystem(string token);

		IInventoryTransferTypeSystem CreateInventoryTransferTypeSystem(string token);

		IExportPackingListSystem CreateExportPackingListSystem(string token);

		IStandingJournalSystem CreateStandingJournalSystem(string token);

		IDashboardSystem CreateDashboardSystem(string token);

		IWebDashboardSystem CreateWebDashboardSystem(string token);

		ICollateralSystem CreateCollateralSystem(string token);

		IJobTaskSystem CreateJobTaskSystem(string token);

		IClientAssetSystem CreateClientAssetSystem(string token);

		IGRNReturnSystem CreateGRNReturnSystem(string token);

		IBinSystem CreateBinSystem(string token);

		IRouteSystem CreateRouteSystem(string token);

		IRouteGroupSystem CreateRouteGroupSystem(string token);

		IRackSystem CreateRackSystem(string token);

		IQualityClaimSystem CreateQualityClaimSystem(string token);

		IArrivalReportSystem CreateArrivalReportSystem(string token);

		IArrivalReportTemplateSystem CreateArrivalReportTemplateSystem(string token);

		IQualityTaskSystem CreateQualityTaskSystem(string token);

		ISurveyorSystem CreateSurveyorSystem(string token);

		IApprovalSystem CreateApprovalSystem(string token);

		IPropertyAgentSystem CreatePropertyAgentSystem(string token);

		IPropertyClassSystem CreatePropertyClassSystem(string token);

		IPropertySystem CreatePropertySystem(string token);

		IPropertyUnitSystem CreatePropertyUnitSystem(string token);

		IPropertyVirtualUnitSystem CreatePropertyVirtualUnitSystem(string token);

		IPropertyCategorySystem CreatePropertyCategorySystem(string token);

		ICheckListSystem CreateCheckListSystem(string token);

		IPaymentRequestSystem CreatePaymentRequestSystem(string token);

		IPropertyIncomeCodeSystem CreatePropertyIncomeCodeSystem(string token);

		IPropertyRentSystem CreatePropertyRentSystem(string token);

		IPropertyCancelSystem CreatePropertyCancelSystem(string token);

		IRentalPostingSystem CreateRentalPostingSystem(string token);

		IW3PLGRNSystem CreateW3PLGRNSystem(string token);

		IW3PLDeliverySystem CreateW3PLDeliverySystem(string token);

		IW3PLInvoiceSystem CreateW3PLInvoiceSystem(string token);

		IProjectExpenseAllocationSystem CreateProjectExpenseAllocationSystem(string token);

		IPurchaseClaimSystem CreatePurchaseClaimSystem(string token);

		IPropertyServiceSystem CreatePropertyServiceSystem(string token);

		IJobBOMSystem CreateJobBOMSystem(string token);

		IEmailMessageSystem CreateEmailMessageSystem(string token);

		IServiceTypeSystem CreateServiceTypeSystem(string token);

		IJobEstimationSystem CreateJobEstimationSystem(string token);

		ICLVoucherSystem CreateCLVoucherSystem(string token);

		ICLTokenSystem CreateCLTokenSystem(string token);

		IEmployeeEOSSettlementSystem CreateEmployeeEOSSettlementSystem(string token);

		IFixedAssetBulkPurchaseSystem CreateFixedAssetBulkPurchaseSystem(string token);

		IEntityCommentSystem CreateEntityCommentSystem(string token);

		IInsuranceProviderSystem CreateInsuranceProviderSystem(string token);

		ICustomerInsuranceSystem CreateCustomerInsuranceSystem(string token);

		IRiderSummarySystem CreateRiderSummarySystem(string token);

		IHorseSummarySystem CreateHorseSummarySystem(string token);

		IProjectSubContractPISystem CreateProjectSubContractPISystem(string token);

		IEntityFlagSystem CreateEntityFlagSystem(string token);

		IEAEquipmentSystem CreateEAEquipmentSystem(string token);

		IMobilizationSystem CreateMobilizationSystem(string token);

		IEquipmentTransferSystem CreateEquipmentTransferSystem(string token);

		IEquipmentWorkOrderSystem CreateEquipmentWorkOrderSystem(string token);

		ILawyerSystem CreateLawyerSystem(string token);

		ICasePartySystem CreateCasePartySystem(string token);

		IWorkOrderInventoryIssueSystem CreateWorkOrderInventoryIssueSystem(string token);

		ILegalActivitySystem CreateLegalActivitySystem(string token);

		IWorkOrderInventoryReturnSystem CreateWorkOrderInventoryReturnSystem(string token);

		ITaxSystem CreateTaxSystem(string token);

		ITaxGroupSystem CreateTaxGroupSystem(string token);

		IInventoryDismantleSystem CreateInventoryDismantleSystem(string token);

		IProductPriceBulkUpdateSystem CreateProductPriceBulkpdateSystem(string token);

		IOpeningEntryTransactionSystem CreateOpeningEntryTransactionSystem(string token);

		IMaterialReservationSystem CreateMaterialReservationSystem(string token);

		ICaseClientSystem CreateCaseClientSystem(string token);

		ILegalActionSystem CreateLegalActionSystem(string token);

		ISalesForecastingSystem CreateSalesForecastingSystem(string token);

		IProductMakeSystem CreateProductMakeSystem(string token);

		IProductTypeSystem CreateProductTypeSystem(string token);

		IProductModelSystem CreateProductModelSystem(string token);

		IUserFavoriteSystem CreateUserFavoriteSystem(string token);

		IPurchasePrepaymentInvoiceSystem CreatePurchasePrepaymentInvoiceSystem(string token);

		ISalespersonGroupSystem CreateSalespersonGroupSystem(string token);

		ITaskStepsSystem CreateTaskStepsSystem(string token);

		ITaskTypeSystem CreateTaskTypeSystem(string token);

		ITaskTransactionSystem CreateTaskTransactionSystem(string token);

		ITaskTransactionStatusSystem CreateTaskTransactionStatusSystem(string token);

		ILegalActionStatusSystem CreateLegalActionStatusSystem(string token);

		ITRApplicationSystem CreateTRApplicationSystem(string token);

		IBudgetingSystem CreateBudgetingSystem(string token);

		IVehicleMileageTrackSystem CreateVehicleMileageTrackSystem(string token);

		ISalesManTargetSystem CreateSalesManTargetSystem(string token);

		ICustomerInsuranceClaimSystem CreateCustomerInsuranceClaimSystem(string token);

		IPropertyDocTypeSystem CreatePropertyDocTypeSystem(string token);

		IPropertyDocumentSystem CreatePropertyDocumentSystem(string token);

		IPropertyTenantDocumentSystem CreatePropertyTenantDocumentSystem(string token);

		IPropertyTenantDocTypeSystem CreatePropertyTenantDocTypeSystem(string token);

		ILoanEntrySystem CreateLoanEntrySystem(string token);

		IPrintTemplateMapSystem CreatePrintTemplateMapSystem(string token);

		IRecurringInvoiceSystem CreateRecurringInvoiceSystem(string token);

		IDataSyncSystem CreateDataSyncSystem(string token);

		IPatientSystem CreatePatientSystem(string token);

		IControlLayoutSystem CreateControlLayoutSystem(string token);

		ICustomListSystem CreateCustomListSystem(string token);
	}
}
