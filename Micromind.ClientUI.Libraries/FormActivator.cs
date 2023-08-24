using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Reports;
using Micromind.ClientUI.Reports.Accounts;
using Micromind.ClientUI.Reports.CRM;
using Micromind.ClientUI.Reports.CustomDashboards;
using Micromind.ClientUI.Reports.Customers;
using Micromind.ClientUI.Reports.CustomReports;
using Micromind.ClientUI.Reports.Employees;
using Micromind.ClientUI.Reports.Enterprise;
using Micromind.ClientUI.Reports.Items;
using Micromind.ClientUI.Reports.Jobs;
using Micromind.ClientUI.Reports.Legal;
using Micromind.ClientUI.Reports.Sales;
using Micromind.ClientUI.Reports.Vendors;
using Micromind.ClientUI.WindowsForms;
using Micromind.ClientUI.WindowsForms.DataEntries;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Centers;
using Micromind.ClientUI.WindowsForms.DataEntries.CRM;
using Micromind.ClientUI.WindowsForms.DataEntries.Customers;
using Micromind.ClientUI.WindowsForms.DataEntries.Employees;
using Micromind.ClientUI.WindowsForms.DataEntries.EnterpriseAsset;
using Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Legal;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.ClientUI.WindowsForms.DataEntries.POS;
using Micromind.ClientUI.WindowsForms.DataEntries.Printing;
using Micromind.ClientUI.WindowsForms.DataEntries.Projects;
using Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental;
using Micromind.ClientUI.WindowsForms.DataEntries.Propertys;
using Micromind.ClientUI.WindowsForms.DataEntries.Recruitment;
using Micromind.ClientUI.WindowsForms.DataEntries.Vendors;
using Micromind.ClientUI.WindowsForms.DataEntries.W3PL;
using Micromind.ClientUI.WindowsForms.DataSync;
using Micromind.ClientUI.WindowsForms.Main;
using Micromind.ClientUI.WindowsForms.OCR;
using Micromind.ClientUI.WindowsForms.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public sealed class FormActivator
	{
		public static bool showForm;

		private static Hashtable fields;

		private static Hashtable customerData;

		private static Hashtable vendorData;

		private static Hashtable inventoryData;

		private static Hashtable companyData;

		private static Hashtable accountData;

		private static Hashtable reportData;

		private static bool loadComplete;

		private static Form formParent;

		private static bool programLoaded;

		private static formHome homeForm;

		private static formPOSHome posHomeForm;

		private static BankDetailsForm bankDetailsForm;

		private static CompanyAccountDetailsForm companyAccountDetailsForm;

		private static CompanyAccountsListForm companyAccountsListForm;

		private static EmployeeDetailsForm employeeDetailsForm;

		private static CustomerDetailsForm customerDetailsForm;

		private static CRMCustomerDetailsForm crmCustomerDetailsForm;

		private static VendorDetailsForm vendorDetailsForm;

		private static VendorClassDetailsForm vendorClassDetailsForm;

		private static ProductDetailsForm productDetailsForm;

		private static ProductBrandDetailsForm productBrandDetailsForm;

		private static ShippingMethodDetailsForm shipperDetailsForm;

		private static ProductCategoryDetailsForm productCategoryDetailsForm;

		private static JournalEntryForm journalEntryForm;

		private static CustomerBalanceSummaryReport customerBalanceSummaryReport;

		private static CustomerBalanceDetailsReport customerBalanceDetailsReport;

		private static VendorBalanceSummaryReport vendorBalanceSummaryReport;

		private static VendorBalanceDetailsReport vendorBalanceDetailsReport;

		private static GLReportForm glReport;

		private static InventoryAdjustmentsForm inventoryAdjustmentForm;

		private static CompanyInformationForm companyInformationForm;

		private static AccountGroupDetailsForm accountGroupDetailsForm;

		private static AnalysisGroupDetailsForm analysisGroupDetailsForm;

		private static AnalysisDetailsForm analysisDetailsForm;

		private static HelpSupportForm helpSupportForm;

		private static ProductManufacturerDetailsForm productManufacturerDetailsForm;

		private static ProductStyleDetailsForm productStyleDetailsForm;

		private static ProductSpecificationDetailsForm productSpecificationDetailsForm;

		private static ProductSizeDetailsForm productSizeDetailsForm;

		private static ProductAttributeDetailsForm productAttributeDetailsForm;

		private static CashExpenseEntryForm expenseEntryForm;

		private static BudgetingForm budgetingForm;

		private static VehicleMileageTrackForm vehiclemileagetrackForm;

		private static SalesManTargetForm salesmantargetForm;

		private static CustomerInsuranceClaimForm customerinsuranceClaimForm;

		private static ContactDetailsForm contactDetailsForm;

		private static PrintPreviewForm printPreviewForm;

		private static PriceLevelDetailsForm priceLevelDetailsForm;

		private static PaymentMethodDetailsForm paymentMethodDetailsForm;

		private static TaskStepsForm taskStepsForm;

		private static TaskTypeForm taskTypeForm;

		private static TaskTransactionForm taskTransactionForm;

		private static TaskTransactionStatusForm taskTransactionStatusForm;

		private static CountryDetailsForm countryDetailsForm;

		private static CustomerClassDetailsForm customerClassDetailsForm;

		private static AreaDetailsForm areaDetailsForm;

		private static PaymentTermDetailsForm paymentTermDetailsForm;

		private static CustomerAddressDetailsForm customerAddressDetailsForm;

		private static SalespersonDetailsForm salespersonDetailsForm;

		private static VendorAddressDetailsForm vendorAddressDetailsForm;

		private static BuyerDetailsForm buyerDetailsForm;

		private static ProductClassDetailsForm productClassDetailsForm;

		private static UnitDetailsForm unitDetailsForm;

		private static SmartListForm smartListForm;

		private static ExternalReportForm externalReportForm;

		private static SalesByGRNReport salesByGRNReport;

		private static PaymentRequestForm paymentRequestForm;

		private static SalesByGRNSummaryReport salesByGRNSummaryReport;

		private static ProjectDueReportForm projectDueReportForm;

		private static EmployeeFinalSettlementReport employeeFinalSettlementReport;

		private static PropertyRegistrationReport propertyRegistrationReport;

		private static PropertyRegistrationReport propertyRenewalReport;

		private static PropertyRegistrationReport propertyCancellationReport;

		private static PropertyAvailabiltyReport propertyAvailabilityReport;

		private static ProjectManPowerReportForm projectManPowerReport;

		private static GradeDetailsForm gradeDetailsForm;

		private static SponsorDetailsForm sponsorDetailsForm;

		private static NationalityDetailsForm nationalityDetailsForm;

		private static ReligionDetailsForm religionDetailsForm;

		private static DivisionDetailsForm divisionDetailsForm;

		private static PositionDetailsForm positionDetailsForm;

		private static EmployeeDocTypeDetailsForm employeeDocTypeDetailsForm;

		private static PatientDocTypeDetailsForm patientDocTypeDetailsForm;

		private static BillDiscountForm billDiscountForm;

		private static VehicleDocTypeDetailsForm vehicleDocTypeDetailsForm;

		private static DegreeDetailsForm degreeDetailsForm;

		private static SkillDetailsForm skillDetailsForm;

		private static DepartmentDetailsForm departmentDetailsForm;

		private static CustomerGroupDetailsForm customerGroupDetailsForm;

		private static VendorGroupDetailsForm vendorGroupDetailsForm;

		private static EmployeeGroupDetailsForm employeeGroupDetailsForm;

		private static EmployeeTypeDetailsForm employeeTypeDetailsForm;

		private static EmployeeAddressDetailsForm employeeAddressDetailsForm;

		private static EmployeeEOSSettlementForm employeeEOSSettlementForm;

		private static EmployeeEOSForm employeeEOSForm;

		private static LocationDetailsForm locationDetailsForm;

		private static SMSAlertForm smsAlertForm;

		private static WorkLocationDetailsForm workLocationDetailsForm;

		private static ProvisionTypeDetailsForm provisionTypeDetailsForm;

		private static EmployeeProvisionEntryForm employeeProvisionForm;

		private static JobTaskDetailsForm jobTaskDetailsForm;

		private static JobTaskGroupDetailsForm jobTaskGroupDetailsForm;

		private static CompanyDivisionDetailsForm companyDivisionDetailsForm;

		private static PrintTemplateMappingForm printTemplateMappingForm;

		private static SalesReport salesByItemCustomerSalespersonGrpByReportform;

		private static ConsignLocationDetailsForm consignLocationDetailsForm;

		private static ProductDataDetailsForm productDataDetailsForm;

		private static EmployeeDependentDetailsForm employeeDependentDetailsForm;

		private static EmployeeDocumentsForm employeeDocumentsForm;

		private static PatientDocumentsForm patientDocumentsForm;

		private static PropertyDocumentsForm propertyDocumentsForm;

		private static TenantDocumentsForm propertyTenantDocumentsForm;

		private static VehicleDocumentsForm vehicleDocumentsForm;

		private static EmployeeSkillsForm employeeSkillsForm;

		private static LeaveTypeDetailsForm leaveTypeDetailsForm;

		private static JobTypeDetailsForm jobTypeDetailsForm;

		private static CostCategoryDetailsForm costCategoryDetailsForm;

		private static PayrollItemDetailsForm payrollItemDetailsForm;

		private static PayrollItemDetailsForm deductionDetailsForm;

		private static BenefitDetailsForm benefitDetailsForm;

		private static EmployeeSalaryDetailsForm employeeSalaryDetailsForm;

		private static DestinationDetailsForm destinationDetailsForm;

		private static EmployeeLeaveDetailForm employeeLeaveDetailForm;

		private static CompanyDocTypeDetailsForm companyDocTypeDetailsForm;

		private static CompanyDocumentsForm companyDocumentsForm;

		private static TenancyContractDetailsForm tenancyContractDetailsForm;

		private static TradeLicenseDetailsForm tradeLicenseDetailsForm;

		private static VisaDetailsForm visaDetailsForm;

		private static AccountAnalysisDetailsForm accountAnalysisDetailsForm;

		private static CurrencyDetailsForm currencyDetailsForm;

		private static CostCenterDetailsForm costCenterDetailsForm;

		private static ChequeReceiptForm chequeReceiptForm;

		private static SysDocDetailsForm sysDocDetailsForm;

		private static RegisterDetailsForm registerDetailsForm;

		private static CashReceiptForm cashReceiptForm;

		private static CashReceiptMultiPayeeForm cashReceiptMultiPayeeForm;

		private static CashPaymentParentForm cashPaymentParentForm;

		private static ChequebookDetailsForm chequebookDetailsForm;

		private static CashPaymentForm cashPaymentForm;

		private static ChequePaymentParentForm chequePaymentForm;

		private static JournalReportForm journalReportForm;

		private static FundTransferForm fundTransferForm;

		private static ChequeDepositForm chequeDepositForm;

		private static ChequeExpenseEntryForm chequeExpenseEntryForm;

		private static DebitNoteEntryForm debitNoteEntryForm;

		private static CreditNoteEntryForm creditNoteEntryForm;

		private static ReturnedChequeReasonDetailsForm returnedChequeReasonDetailsForm;

		private static ChequeReturnForm chequeReturnForm;

		private static ReceivedChequeCancellationForm receivedChequeCancellationForm;

		private static ReceivedChequeClearanceForm receivedChequeClearanceForm;

		private static IssuedChequeClearanceForm issuedChequeClearanceForm;

		private static IssuedChequeCancellationForm issuedChequeCancellationForm;

		private static IssuedChequeReturnForm issuedChequeReturnForm;

		private static VoidBlankChequeForm voidBlankChequeForm;

		private static SecurityChequeForm securityChequeForm;

		private static ChequeStatusForm chequeStatusForm;

		private static GLReportForm glReportForm;

		private static AdjustmentTypeDetailsForm adjustmentTypeDetailsForm;

		private static DriverDetailsForm driverDetailsForm;

		private static InventoryTransferForm inventoryTransferForm;

		private static ProductQuantityForm productQuantityForm;

		private static InventoryTransferAcceptanceForm inventoryTransferAcceptanceForm;

		private static InventoryTransferReturnForm inventoryTransferReturnForm;

		private static ProductListForm productListForm;

		private static DirectInventoryTransferForm directInventoryTransferForm;

		private static SalesQuoteForm salesQuoteForm;

		private static EntityCategoryDetailsForm customerCategoryForm;

		private static EntityCategoryDetailsForm contactsCategoryForm;

		private static CRMCategoryDetailsForm leadCategoryForm;

		private static JobMaterialEstimateForm jobMaterialEstimateForm;

		private static JobManHrsBudgetingForm jobManHrsBudgetingForm;

		private static GeneralListForm customerCategoryListForm;

		private static GeneralListForm contactsCategoryListForm;

		private static GeneralListForm leadCategoryListForm;

		private static GeneralListForm vendorCategoryListForm;

		private static GeneralListForm genericProductTypeListForm;

		private static GeneralListForm CaseClientListForm;

		private static GeneralListForm riderSummaryListForm;

		private static GeneralListForm horseSummaryListForm;

		private static GeneralListForm horseTypeListForm;

		private static GeneralListForm equipmentCategoryListForm;

		private static GeneralListForm equipmentTypeListForm;

		private static GeneralListForm horseSexListForm;

		private static GeneralListForm holidayCalendarListForm;

		private static GeneralListForm overTimeDetailsListForm;

		private static GeneralListForm lawyerListForm;

		private static GeneralListForm legalActionStatusListForm;

		private static GeneralListForm taxListForm;

		private static GeneralListForm taxgroupListForm;

		private static GeneralListForm casePartyListForm;

		private static LeaveAvailabilityForm leaveAvailabilityForm;

		private static CustomerInsuranceDetailsForm customerInsuranceDetailsForm;

		private static CreditlimitReviewForm creditLimitReviewForm;

		private static PriceListDetailsForm priceListDetailsForm;

		private static ImageViewerForm imageViewerForm;

		private static SalesOrderForm salesOrderForm;

		private static SalesProformaInvoiceForm salesProformaInvoiceForm;

		private static SalesEnquiryForm salesEnquiryForm;

		private static SalesInvoiceNonInvForm salesInvoiceNIForm;

		private static DeliveryNoteForm deliveryNoteForm;

		private static SalesInvoiceForm salesInvoiceForm;

		private static SalesReceiptForm salesReceiptForm;

		private static SalesReturnCreditForm salesReturnCreditForm;

		private static SalesReturnCashForm salesReturnCashForm;

		private static DeliveryReturnForm deliveryReturnForm;

		private static PurchaseQuoteForm purchaseQuoteForm;

		private static PurchaseOrderForm purchaseOrderForm;

		private static PurchaseOrderNonInvForm purchaseOrderNonInvForm;

		private static PurchasePackingListForm poShipmentForm;

		private static PortDetailsForm portDetailsForm;

		private static CollateralDetailsForm collateralDetailsForm;

		private static ServiceProviderForm serviceProviderForm;

		private static MaintenanceSchedulerForm maintenanceSchedulerForm;

		private static VehicleMaintenanceSchedulerReport maintenanceSchedulerReportForm;

		private static VehicleMaintenanceEntryReport maintenanceEntryReportForm;

		private static ProjectInvoiceReportForm projectInvoiceReportForm;

		private static PhysicalStockEntryForm physicalStockEntryForm;

		private static SalespersonGroupDetailsForm salespersonGroupDetailsForm;

		private static PickListReport pickListReportForm;

		private static VehicleMaintenanceEntryForm vehicleMaintenanceEntryForm;

		private static LPOReceiptForm lpoReceiptForm;

		private static CandidateCancellationForm candidateCancellationForm;

		private static EmployeeCancellationForm employeeCancellationForm;

		private static PurchaseInvoiceForm purchaseInvoiceForm;

		private static PurchaseInvoiceNonInvForm purchaseInvoiceNonInvForm;

		private static PurchaseGRNForm purchaseReceiptForm;

		private static ImportPurchaseGRNForm importPurchaseGRNForm;

		private static CashPurchaseForm cashPurchaseForm;

		private static PurchaseReturnCashForm purchaseReturnCashForm;

		private static PurchaseReturnCreditForm purchaseReturnCreditForm;

		private static GRNReturnForm grnReturnForm;

		private static FixedAssetBulkPurchaseForm fixedAssetBulkPurchaseForm;

		private static EmployeeAppraisalForm employeeAppraisalForm;

		private static InventoryTransactionsReport inventoryTransactionsReport;

		private static ProjectInventoryTransactionsReport projectInventoryTransactionsReport;

		private static InventoryTransferReport inventoryTransferReport;

		private static ItemMovementGRNReport itemMovementGRNReport;

		private static ItemMovementConsignInReport itemMovementConsignInReport;

		private static ConsignInReceiptReport ConsignInReceiptReport;

		private static ConsignmentInSettlementReport consignmentInSettlementReport;

		private static ConsignmentOutSettlementReport consignmentOutSettlementReport;

		private static W3PLInventoryTransactionsReport W3PLinventoryTransactionsReport;

		private static PurchaseByInventoryItemVendorBuyerReport purchaseByInventoryItemVendorBuyerReport;

		private static CustomerTopListReport customerTopListReport;

		private static CustomerDueReport customerDueReport;

		private static PropertyAccountTransactionsReportForm propertyAccountTransactionReportForm;

		private static PropertyUnitAvailabilityReportForm propertyUnitAvailabilityReportForm;

		private static PropertyUnitHistoryReportForm propertyUnitHistoryReportForm;

		private static ConsignmentOutIssuedReport consignmentOutIssuedReport;

		private static SalesByProductClassandCategoryReport salesByitemCategoryReport;

		private static SalesPurchaseAnalysisReport salesPurchaseAnalysisReport;

		private static SalesComparisonReport salesComparisonReport;

		private static ContainerTrackingForm containerTrackingForm;

		private static ContainerTrackingWizardForm containerTrackingWizrdForm;

		private static CustomerCenterForm customerCenterForm;

		private static AccountCenterForm accountCenterForm;

		private static VendorCenterForm vendorCenterForm;

		private static InventoryCenterForm inventoryCenterForm;

		private static ReportCenterForm reportCenterForm;

		private static HRCenterForm hrCenterForm;

		private static CompanyOptionsForm companyOptionsForm;

		private static PurchaseOrderImportForm purchaseOrderImportForm;

		private static PurchaseInvoiceImportForm purchaseInvoiceImportForm;

		private static ReleaseTypeForm releaseTypeForm;

		private static ServiceItemForm serviceItemForm;

		private static LegalActionStatusForm legalActionStatusForm;

		private static ArrivalReportForm arrivalReportForm;

		private static QualityClaimForm qualityClaimForm;

		private static ArrivalReportTemplateForm arrivalReportTemplateForm;

		private static QualityTaskForm qualityTaskForm;

		private static SurveyorForm surveyorForm;

		private static ClientAssetForm clientAssetForm;

		private static BinDetailsForm binDetailsForm;

		private static RackDetailsForm rackDetailsForm;

		private static GenericProductTypeDetailsForm genericProductTypeDetailsForm;

		private static ServiceActivityDetailsForm serviceActivityDetailsForm;

		private static JobMaintenanceServiceEntryForm jobMaintenanceServiceEntryForm;

		private static JobMaintenanceScheduleForm jobMaintenanceScheduleForm;

		public static ServiceCallTrackForm serviceCallTrackForm;

		public static ServiceCallTrackReportForm serviceCallTrackReportForm;

		public static HolidayCalendarForm holidayCalendarForm;

		private static ChangeCashRegisterForm changeCashRegisterForm;

		private static PurchaseCostEntryReport purchaseCostEntryReportForm;

		private static PaymentAdviceDetailsForm paymentAdviceDetailsForm;

		private static VendorPriceListDetailsForm vendorPriceListDetailsForm;

		private static SalesByCustomerReport salesByCustomerReport;

		private static SalesByItemReport salesByItemReport;

		private static SalesBySalespersonReport salesBySalespersonReport;

		private static SalesByLocationReport salesByLocationReport;

		private static SalesByProductCategoryReport salesByProductCategoryReport;

		private static SalesByCustomerGroupReport salesByCustomerGroupReport;

		private static TrialBalanceReportForm trialBalanceReportForm;

		private static BalanceSheetReportForm balanceSheetReportForm;

		private static ProfitAndLossReportForm profitAndLossReportForm;

		private static ProfitAndLossMonthwiseReportForm profitAndLossMonthwiseReportForm;

		private static ProfitAndLossComparionReportForm profitAndLossComparionReportForm;

		private static DailyCashReportForm dailyCashReportForm;

		private static DailyCashSaleReport dailyCashSaleReportForm;

		private static CustomerListReport customerListReport;

		private static CustomerContactListReport customerContactListReport;

		private static CustomerProfileReport customerProfileReport;

		private static CustomerActivityReport customerActivityReport;

		private static CustomerYearMonthPaymentReport customerYearMonthPaymentReport;

		private static EmployeeSalarySlipReport employeeSalarySlipReport;

		private static HRLetterReportForm lastMonthSalaryReport;

		private static HRLetterReportForm salaryCertificateReport;

		private static HRLetterReportForm confirmationLetterReport;

		private static HRLetterReportForm appointmentLetterReport;

		private static HRLetterReportForm salaryIncrementLetterReport;

		private static HRLetterReportForm experienceCertificateReport;

		private static AssemblyReport assemblyReport;

		private static SalesByItemCustomerSalespersonReport salesByItemCustomerSalespersonReport;

		private static ConsignmentInReport consignmentInReport;

		private static ConsignmentOutReport consignmentOutReport;

		private static CustomerOutstandingSummaryReport customerOutstandingSummaryReport;

		private static DailySalesAnalysisReport dailySalesAnalysisReportForm;

		private static SalesReservedReport salesReservedReportForm;

		private static SalesByProductBrandReport salesByProductBrandReportForm;

		private static VendorOutstandingSummaryReport vendorOutstandingSummaryReportForm;

		private static SalespersonCommisionReport SalespersonCommisionReportForm;

		private static SalesProfitabilityReport salesProfitabilityReportForm;

		private static TaxDetailsReport taxDetailsReportForm;

		private static SalesByMainCategory salesByMainCategoryReportForm;

		private static MonthlySalesPivotReport monthlySalesPivotReportForm;

		private static MultipleInvoiceReport multipleInvoiceReportForm;

		private static SalesOrderDetailReport salesOrderDetailsReportForm;

		private static TransporterDetailsForm transporterDetailsForm;

		private static CustomGadgetDetailForm customGadgetDetailsForm;

		private static ContainerSizeDetailsForm containerSizeDetailsForm;

		private static INCODetailsForm INCODetailsForm;

		private static ReportTemplatesUpdateForm reportTemplatesUpdateForm;

		private static InventoryTransactionsLotwiseReport inventoryTransactionsLotwiseReport;

		private static PurchaseExpenseAllocationReport purchaseExpenseAllocationReport;

		private static PurchaseOrderDetailReport purchaseOrderDetailReport;

		private static PurchaseByItemVendorBuyerReport purchaseByItemVendorBuyerReport;

		private static FixedAssetListReport fixedAssetListReport;

		private static FixedAssetPurchaseReport fixedAssetPurchaseReport;

		private static FixedAssetSaleReport fixedAssetSaleReport;

		private static FixedAssetTransactionsReport fixedAssetTransactionsReport;

		private static FixedAssetDepreciationReport fixedAssetDepreciationReport;

		private static FixedAssetTransferReport fixedAssetTransferReport;

		private static CashFlowReportForm cashFlowReportForm;

		private static AccountTransactionsReportForm accountTransactionsReportForm;

		private static AccountCostCenterReportForm accountCostCenterReportForm;

		private static AccountLedgerReportForm accountLedgerReportForm;

		private static BankLedgerReportForm bankLedgerReportForm;

		private static SalesManDueReport salesManDueReport;

		private static ProjectAccountTransactionsReportForm projectAccountTransactionsReportForm;

		private static AccountAnalysisReportForm accountAnalysisReportForm;

		private static AccountAnalysisPivotReportForm accountAnalysisPivotReportForm;

		private static VendorListReport vendorListReport;

		private static VendorContactListReport vendorContactListReport;

		private static VendorProfileReport vendorProfileReport;

		private static VendorActivityReport vendorActivityReport;

		private static ProductListReport productListReport;

		private static PurchaseByVendorReport purchaseByVendorReport;

		private static PurchaseByVendorGroupReport purchaseByVendorGroupReport;

		private static PurchaseByItemReport purchaseByItemReport;

		private static PurchaseByProductCategoryReport purchaseByProductCategoryReport;

		private static PurchaseByBuyerReport purchaseByBuyerReport;

		private static PurchaseByLocationReport purchaseByLocationReport;

		private static ProductPriceListReport productPriceListReport;

		private static ProductStockListReport productStockListReport;

		private static ProductStockListReport productValuationReport;

		private static ProductCatalogReport productCatalogReport;

		private static FreightChargeReport freightChargeReport;

		private static EmployeeTransferForm employeeTransferForm;

		private static EmployeeTerminationForm employeeTerminationForm;

		private static EmployeePromotionForm employeePromotionForm;

		private static DisciplineActionTypeDetailsForm disciplineActionTypeDetailsForm;

		private static EmployeeDisciplinaryActionForm employeeDisciplinaryActionForm;

		private static EmployeeRehireForm employeeRehireForm;

		private static EmployeeActivityTypeDetailsForm employeeActivityTypeDetailsForm;

		private static EmployeeGeneralActivityForm employeeGeneralActivityForm;

		private static EmployeeLeaveRequestForm employeeLeaveRequestForm;

		private static EmployeeLeaveApprovalForm employeeLeaveApprovalForm;

		private static EmployeeResumptionForm employeeResumptionForm;

		private static SalarySheetForm salarySheetForm;

		private static PostSalarySheetForm postSalarySheetForm;

		private static CashSalaryPaymentForm cashSalaryPaymentForm;

		private static ChequeSalaryPaymentForm chequeSalaryPaymentForm;

		private static TransferSalaryPaymentForm transferSalaryPaymentForm;

		private static EmployeeLoanTypeDetailsForm employeeLoanTypeDetailsForm;

		private static EmployeeLoanForm employeeLoanForm;

		private static EmployeeLoanSettlementForm employeeLoanSettlementForm;

		private static EmployeeBalanceSummaryReport employeeBalanceSummaryReport;

		private static EmployeeBalanceDetailsReport employeeBalanceDetailsReport;

		private static EmployeeSalaryReport employeeSalaryReport;

		private static EmployeeListReport employeeListReport;

		private static EmployeeProfileReport employeeProfileReport;

		private static EmployeeActivityReport employeeActivityReport;

		private static PurchaseCostSheetReport purchaseCostSheetReport;

		private static LeadProfileReport leadProfileReport;

		private static LeadBySourceReport leadBySourceReport;

		private static UpcomingOpportunitiesReport upcomingOppReport;

		private static UpcomingEventsReport upcomingEventsReport;

		private static CustomerStatementForm customerStatementForm;

		private static UnallocatedPaymentsListForm unallocatedCustomerPaymentsListForm;

		private static UnallocatedPaymentsListForm unallocatedVendorPaymentsListForm;

		private static CustomerAgingSummaryReport customerAgingSummaryReport;

		private static VendorAgingSummaryReport vendorAgingSummaryReport;

		private static VendorStatementForm vendorStatementForm;

		private static ShipmentsPerformanceAnalyseForm shipmentPerformanceAnalysForm;

		private static ExportSmartListForm exportSmartListForm;

		private static ExportSmartListForm exportPivotReportForm;

		private static ExportSmartListForm exportCustomeGadgetForm;

		private static ImportSmartListForm importSmartListForm;

		private static ImportSmartListForm importPivotReportForm;

		private static ImportSmartListForm importCustomeGadgetForm;

		private static UserDetailsForm userDetailsForm;

		private static UserGroupDetailsForm userGroupDetailsForm;

		private static PrintChequeForm printChequeForm;

		private static ReminderListForm reminderListForm;

		private static CompanyAddressDetailsForm companyAddressDetailsForm;

		private static ConsignOutForm consignOutForm;

		private static GarmentRentalForm garmentRentalForm;

		private static GarmentRentalReturnForm garmentRentalReturnForm;

		private static ConsignOutSettlementForm consignOutSettlementForm;

		private static ExpenseCodeDetailsForm expenseCodeDetailsForm;

		private static ExportSalesInvoiceForm exportSalesInvoiceForm;

		private static ExportSalesOrderForm exportSalesOrderForm;

		private static ExportSalesProformaInvoiceForm exportSalesProformaInvoiceForm;

		private static ExportDeliveryNoteForm exportDeliveryNoteForm;

		private static ExportPickListForm exportPickListForm;

		private static ConsignOutReturnForm consignOutReturnForm;

		private static ConsignInForm consignInForm;

		private static ConsignInReturnForm consignInReturnForm;

		private static ConsignInSettlementForm consignInSettlementForm;

		private static ConsignInClosingForm consignInClosingForm;

		private static PendingConsignInReport pendingConsignInReport;

		private static PendingConsignOutReport pendingConsignOutReport;

		private static FixedAssetDetailsForm fixedAssetDetailsForm;

		private static FixedAssetGroupDetailsForm fixedAssetGroupDetailsForm;

		private static FixedAssetLocationDetailsForm fixedAssetLocationDetailsForm;

		private static FixedAssetClassDetailsForm fixedAssetClassDetailsForm;

		private static FixedAssetPurchaseForm fixedAssetPurchaseForm;

		private static FixedAssetSaleForm fixedAssetSaleForm;

		private static FixedAssetTransferForm fixedAssetTransferForm;

		private static FixedAssetDepForm fixedAssetDepForm;

		private static FixedAssetPurchaseOrderForm fixedAssetPurchaseOrderForm;

		private static POSCashRegisterDetailsForm posCashRegisterDetailsForm;

		private static POSLocationDetailsForm posLocationDetailsForm;

		private static DimensionDetailsForm dimensionDetailsForm;

		private static MatrixTemplateDetailsForm matrixTemplateDetailsForm;

		private static MatrixProductDetailsForm matrixProductDetailsForm;

		private static POSCashRegisterPaymentMethodsForm posCashRegisterPaymentMethodsForm;

		private static POSCashRegisterExpenseAccountsForm posCashRegisterExpenseAccountsForm;

		private static ProformaInvoiceForm proformaInvoiceForm;

		private static UnallocatedPurchasePrePaymentListForm unallocatPurchasePrePaymentList;

		private static FiscalYearDetailsForm fiscalYearDetailsForm;

		private static CustomerAgingListForm customerAgingListForm;

		private static VendorAgingListForm vendorAgingListForm;

		private static TTPaymentForm ttPaymentForm;

		private static TTReceiptForm ttReceiptForm;

		private static LeadDetailsForm leadDetailsForm;

		private static LeadAddressDetailsForm leadAddressDetailsForm;

		private static UDFSetupForm udfSetupForm;

		private static TaxEntryForm taxEntryForm;

		private static PurchasePrePaymentInvoiceForm purchasePrepaymentInvoiceForm;

		private static CustomerOutstandingInvoicesReport customerOutstandingInvoicesReport;

		private static MatrixProductStockListReport matrixProductStockListReport;

		private static BuildAssemblyForm buildAssemblyForm;

		private static InventoryRepackingForm inventoryRepackingForm;

		private static ProjectExpenseAllocationForm projectExpenseAllocationForm;

		private static PurchaseClaimForm purchaseClaimForm;

		private static JobEstimationDetailsForm jobEstimationForm;

		private static EmployeePassportControlForm employeePassportControlForm;

		private static InsuranceProviderForm insuranceProviderForm;

		private static VendorCategoryDetailsForm vendorCategoryForm;

		private static BOMDetailsForm bomDetailsForm;

		private static EmployeeSalarySlipBulkMailForm employeeSalarySlipBulkMailForm;

		private static JobBOMDetailsForm JobbomDetailsForm;

		private static PackageDetailsForm packageDetailsForm;

		private static EOSRuleDetailsForm eosRuleDetailsForm;

		private static OverTimeDetailsForm overTimeDetailsForm;

		private static EmployeeLoanPaymentForm employeeLoanPaymentForm;

		private static DocManagementForm docManagementForm;

		private static JobDetailsForm jobDetailsForm;

		private static OpportunityDetailsForm opportunityDetailsForm;

		private static FollowupDetailsForm followupDetailsForm;

		private static EventDetailsForm eventDetailsForm;

		private static CompetitorDetailsForm competitorDetailsForm;

		private static ActivityDetailsForm activityDetailsForm;

		private static CustomReportDetailForm customReportDetailForm;

		private static TRPaymentForm trPaymentForm;

		private static EmployeeOpeningBalanceBatchForm employeeOpeningBalanceBatchForm;

		private static EmployeeOpeningBalanceLeaveForm employeeOpeningBalanceLeaveForm;

		private static VendorOutstandingInvoicesReport vendorOutstandingInvoicesReportForm;

		private static InventoryAdjustmentReport inventoryAdjustmentReportForm;

		private static EditLotDetailsForm updateLotDetailsForm;

		private static PendingGRNReport pendingGRNReportForm;

		private static PendingDNReport pendingDNReportForm;

		private static ProfitAndLossReportRevisedForm profitAndLossReportRevisedForm;

		private static PropertyServiceInvoice propertyServiceInvoiceForm;

		private static RecurringTransactionPostForm recurringTransactionPostForm;

		private static EmployeeAbscondingEntryForm employeeAbscondingEntryForm;

		private static PatientForm patientForm;

		private static ProjectStatusReportForm projectStatusReportForm;

		private static RouteDetailsForm routeDetailsForm;

		private static RouteGroupDetailsForm routeGroupDetailsForm;

		private static ProductionDetailsForm productionDetailsForm;

		private static UpdateTREntryDetailsForm updateTREntryDetailsForm;

		private static PurchaseComparisonReport purchaseComparisonReportForm;

		private static DataSyncForm dataSyncForm;

		private static DataSyncSetupDetailsForm dataSyncSetupDetailsForm;

		private static MaterialVarianceReportForm materialVarianceReportForm;

		private static JobSummaryReportForm jobSummaryReportForm;

		private static EmployeeLeaveReport employeeLeaveReportForm;

		private static EmployeeLeaveStatusReport leaveStatusReportForm;

		private static EmployeeAnnualLeaveDueReport employeeAnnualLeaveDueReportForm;

		private static EmployeeLoanReport employeeLoanReportForm;

		private static DiscountChequesReportForm discountChequesReportForm;

		private static PurchaseLogReport purchaseLogReportForm;

		private static PurchaseCostEntryForm purchaseCostEntryForm;

		private static RiderSummaryDetailsForm riderSummaryDetailsForm;

		private static HorseSummaryDetailsForm horseSummaryDetailsForm;

		private static ContainerTrackingReportForm containerTrackingreportform;

		private static HorseSummaryReport horseSummaryReport;

		private static BarCodeReport barcodeReport;

		private static BalanceSheetComparisonReportForm balanceSheetComparisonReport;

		private static HorseTypeForm horseTypeForm;

		private static HorseSexForm horseSexForm;

		private static EAEquipmentForm equipmentForm;

		private static RequisitionDetailsForm requisitionDetailsForm;

		private static MobilisationForm mobilisationForm;

		private static ItemTransactionForm itemTransactionForm;

		private static EquipmentTransferForm equipmentTransferForm;

		private static WorkOrderForm equipmentWorkOrderForm;

		private static LawyerDetailsForm lawyerDetailsForm;

		private static CasePartyDetailsForm casePartyDetailsForm;

		private static LegalActivityDetailsForm legalActivityDetailsForm;

		private static LegalActionDetailsForm legalActionDetailsForm;

		private static FreightChargesForm freightChargesForm;

		private static SalesForecastingForm salesForecastingForm;

		private static ProductMakeDetailsForm productMakeDetilsForm;

		private static ProductTypeDetailsForm productTypeDetilsForm;

		private static ProductModelDetailsForm productModelDetilsForm;

		private static TaxGroupForm taxGroupForm;

		private static ProductTypeDetailsForm productTypeForm;

		private static EquipmentListReport equipmentListReportForm;

		private static RequisitionByWorkLocationProjectReport requisitionByWorkLocationProjectReportForm;

		private static MobilizationByWorkLocationProjectReport mobilizationByWorkLocationProjectReportForm;

		private static EquipmentTransferReport equipmentTransferReportForm;

		private static WorkOrderByLocationProjectReport equipmentWorkOrderReportForm;

		private static EquipmentFlowReport equipmentFlowReportForm;

		private static WorkOrderInventoryIssueForm workOrderInventoryIssueForm;

		private static WorkOrderInventoryReturnForm workOrderInventoryReturnForm;

		private static WorkOrderInventoryTransactionsReport workOrderInventoryTransactionReport;

		private static PendingCaseReport pendingCaseReport;

		private static CaseStatusReport CaseStatusReport;

		private static ProductPriceBulkUpdateForm productPriceBulkUpdateForm;

		private static OpeningChequeReceiptEntryForm openingChequeReceiptEntryForm;

		private static OpeningChequePaymentEntryForm openingChequePaymentEntryForm;

		private static MaterialReservationForm materialReservationForm;

		private static CaseLawyerTrack caseLawyerTrackReport;

		private static InventoryDismantleForm inventoryDismantleForm;

		private static EquipmentDetailsForm equipmentDetailForm;

		private static CaseClientDetailsForm caseClientDetailsForm;

		private static OverTimeEntryForm overTimeEntryForm;

		private static SalaryDeductionForm salaryDeductionForm;

		private static SalaryAdditionForm salaryAdditionForm;

		private static EmployeeOverTimeReport employeeOverTimeReportForm;

		private static EmployeeGraduityEligibilityReport employeeGraduityEligibilityReportForm;

		private static EmployeeHistoryReport employeeHistoryReportForm;

		private static EmployeeSalaryReport employeeSalaryReportForm;

		private static PDCIssuedReportForm pdcIssuedReportForm;

		private static PDCReceivedReportForm pdcReceivedReportForm;

		private static ProductUPCGenerator productUPCGenerator;

		private static ProjectSubcontractPOForm projectSubContractPOForm;

		private static ProjectSubContractPIForm projectSubContractPIForm;

		private static InventoryDamageForm inventoryDamageForm;

		private static ProductStockPivotListReport productStockPivotListReport;

		private static W3PLProductStockPivotListReport W3PLproductStockPivotListReport;

		private static JobClosingForm jobClosingForm;

		private static CampaignDetailsForm campaignDetailsForm;

		private static JobInventoryIssueForm jobInventoryIssueForm;

		private static JobInventoryReturnForm jobInventoryReturnForm;

		private static JobExpenseIssueForm jobExpenseIssueForm;

		private static ProjectBillingForm jobInvoiceForm;

		private static JobTimesheetForm jobTimesheetForm;

		private static BankFacilityGroupDetailsForm bankFacilityGroupForm;

		private static BankFacilityDetailsForm bankFacilityForm;

		private static CustomerOpeningBalanceBatchForm customerOpeningBalanceBatchForm;

		private static VendorOpeningBalanceBatchForm vendorOpeningBalanceBatchForm;

		private static PurchaseStockEntry inventoryOpeningBalanceBatchForm;

		private static NationalAccountForm nationalAccountForm;

		private static BankReconciliationForm bankReconciliationForm;

		private static BankReconciliationOpeningForm bankReconciliationOpeningForm;

		private static BankReconciliationsReportForm bankReconciliationReportForm;

		private static BankNotReconciledReportForm bankNotReconciledReportForm;

		private static CustomerVendorLinkForm customerVendorLinkForm;

		private static CityDetailsForm cityDetailsForm;

		private static EmployeeLeaveProcessForm employeeLeaveProcessForm;

		private static GenericProductTypeDetailsForm genericProductTypeListDetailsForm;

		private static GenericListDetailsForm agentDetailsForm;

		private static GenericListDetailsForm crmActivityReasonDetailsForm;

		private static GenericListDetailsForm crmEventTypeDetailsForm;

		private static GenericListDetailsForm qcInspectorDetailsForm;

		private static GenericListDetailsForm genericListDetailsForm;

		private static GenericListDetailsForm propertyUnitTypeForm;

		private static GenericListDetailsForm propertyViewForm;

		private static GenericListDetailsForm propertyOwnerForm;

		private static GenericListDetailsForm propertyFacilityForm;

		private static GenericListDetailsForm propertyServiceTypeForm;

		private static GenericListDetailsForm employeeAddressTypeForm;

		private static GenericListDetailsForm fixedAssetCompanyForm;

		private static GenericListDetailsForm medicalInsuranceCategoryForm;

		private static GenericListDetailsForm horseOwnershipTypeForm;

		private static GenericListDetailsForm horseCategoryForm;

		private static GenericListDetailsForm activityStatusForm;

		private static GenericListDetailsForm caseTypeForm;

		private static GenericListDetailsForm chronicsDetailsForm;

		private static GenericListDetailsForm allergyDetailsForm;

		private static GenericListDetailsForm containerTypeForm;

		private static GenericListDetailsForm vehicleMakeForm;

		private static GenericListDetailsForm partsMakeTypeForm;

		private static GenericListDetailsForm partsTypeForm;

		private static GenericListDetailsForm partsFamilyForm;

		private static GenericListDetailsForm vehicleModelForm;

		private static GenericListDetailsForm shippingCompanyForm;

		private static GenericListDetailsForm kitchenTypeForm;

		private static GenericListDetailsForm collateralCustodianForm;

		private static GenericListDetailsForm legalPositionFormObj;

		private static CustomListSetupForm customListSetupFormObj;

		private static EquipmentCategoryForm equipmentCategoryForm;

		private static EquipmentTypeForm equipmenttypeForm;

		private static RequisitionTypeForm requisitionTypeForm;

		private static ContainerStatusChangingForm statusChangeForm;

		private static VehicleDetailsForm vehicleDetailsForm;

		private static JobMaterialRequisitionForm jobMaterialRequesitionForm;

		private static JobFeeForm jobFeeForm;

		private static WorkOrderDetailsForm workOrderDetailsForm;

		private static JobBudgetVsActualReport jobBudgetVsActualReport;

		private static TREntryForm trEntryForm;

		private static TRApplicationForm trApplicationForm;

		private static InventoryAgingReport inventoryAgingReport;

		private static W3PLInventoryAgingReport W3PLinventoryAgingReport;

		private static SendChequesToBankForm sendChequesToBankForm;

		private static CandidateDetailsForm candidateDetailsForm;

		private static ChequeDiscountForm chequeDiscountForm;

		private static InventoryTransferTypeDetailsForm inventoryTransferTypeDetailsForm;

		private static ExportPackingListForm exportPackingListForm;

		private static ShipmentForm shipmentForm;

		private static LeaveEncashmentForm leaveEncashmentForm;

		private static LeaveSalaryPaymentForm leaveSalaryPaymentForm;

		private static StandingJournalEntryForm standingJournalEntryForm;

		private static ApprovalDetailsForm approvalDetailsForm;

		private static PerformApprovalForm performApprovalForm;

		private static VerificationDetailsForm verificationDetailsForm;

		private static VerifyObjectForm verifyObjectForm;

		private static CheckListDetailsForm checkListDetailsForm;

		private static ServiceProviderForm serviceProviderFormObj;

		private static SubContractPurchaseByItemVendorBuyerReport subContractPIReportForm;

		private static AppointmentDetailsForm appointmentDetailsForm;

		private static MaterialRequisitionReportForm MaterialRequisitionReportForm;

		private static BillOfLadingListForm billOfLadingForm;

		private static RequestForCreditLimitReport reqForCreditLimitReportForm;

		private static ChequeMaturityReportForm chequeMaturityReportForm;

		private static SalesEnquiryReport salesEnquiryReportFormObj;

		private static SalesQuoteReport salesQuoteReportFormObj;

		private static SalesOrderReport salesOrderReportFormObj;

		private static ProformaInvoiceReport proformaInvoiceReportFormObj;

		private static DeliveryNoteReport deliveryNoteReportFormObj;

		private static SalesReceiptReport salesReceiptReportFormObj;

		private static SalesInvoiceReport salesInvoiceReportForm;

		private static PurchaseQuoteReport purchaseQuoteReportFormObj;

		private static PurchaseInvoiceReport purchaseInvoiceReportForm;

		private static PurchaseGRNReport purchaseGRNReportForm;

		private static PurchasePackingListReport purchasePLReportForm;

		private static PurchaseOrderReport purchaseOrderReportForm;

		private static ProjectSubContractPOReportForm subContractPOReportForm;

		private static MaterialRequisitionFlowReport materialRequisitionFlowReportFormObj;

		private static PropertyDetailsForm propertyDetailsForm;

		private static PropertyDocTypeDetailsForm propertyDocTypeFormObj;

		private static PropertyTenantDocTypeDetailsForm propertyTenantDocTypeFormObj;

		private static PropertyUnitDetailsForm propertyUnitDetailsForm;

		private static VirtualUnitDetailsForm virtualUnitDetailsForm;

		private static PropertyClassDetailsForm propertyClassDetailsForm;

		private static PropertyAgentDetailsForm propertyAgentDetailsForm;

		private static PropertyCategoryDetailsForm propertyCategoryDetailsForm;

		private static PropertyIncomeCodeDetailsForm propertyIncomeCodeDetailsForm;

		private static PropertyRentDetailsForm propertyRentDetailsForm;

		private static PropertyRentRenewDetailsForm propertyRenewDetailsForm;

		private static PropertyRentCancellationForm propertyRentCancellationForm;

		private static RentIncomePostingDetails rentIncomePostingDetails;

		private static PropertyTenantForm propertyTenantForm;

		private static PropertyTenantClassDetailsForm propertyTenantClassDetailsForm;

		private static PropertyServiceRequestForm propertyServiceRequestForm;

		private static PropertyServiceAssignForm PropertyServiceAssignForm;

		private static POSCashExpenseForm posCashExpenseForm;

		private static SalesByCustomerFilterReport salesByCustomerReportForm;

		private static W3PLGRNForm w3PLGRNForm;

		private static W3PLDeliveryForm w3PLDeliveryForm;

		private static W3PLInvoiceForm w3PLInvoiceForm;

		private static CLVoucherForm clVoucherForm;

		private static EntityFlagDetailsForm entityFlagDetailsForm;

		private static EmployeePerformanceCardForm employeePerformanceCardForm;

		private static DocumentOCRForm documentOCRForm;

		private static GenericListDetailsForm leadStatusDetailsForm;

		private static GenericListDetailsForm industryDetailsForm;

		private static GenericListDetailsForm leadSourceDetailsForm;

		private static GenericListDetailsForm collateralTypeDetailsForm;

		private static GenericListDetailsForm salesReturnReasonForm;

		private static GenericListDetailsForm EntityCategoryDetailsForm;

		private static GenericListDetailsForm vehicleTypeForm;

		private static UploadFileDialog uploadFileDialog;

		private static EditFileDialog editFileDialog;

		private static GeneralListForm accountGroupListForm;

		private static GeneralListForm payrollItemListForm;

		private static GeneralListForm analysisListForm;

		private static GeneralListForm analysisGroupListForm;

		private static GeneralListForm areaListForm;

		private static GeneralListForm jobListForm;

		private static GeneralListForm clientAssetListForm;

		private static GeneralListForm quoteComparisonListForm;

		private static GeneralListForm opportunityListForm;

		private static GeneralListForm competitorListForm;

		private static GeneralListForm activityListForm;

		private static GeneralListForm campaignListForm;

		private static GeneralListForm eventListForm;

		private static GeneralListForm bankListForm;

		private static GeneralListForm bankFacilityGroupListForm;

		private static GeneralListForm bankFacilityListForm;

		private static GeneralListForm cityListForm;

		private static GeneralListForm jobFeeListForm;

		private static GeneralListForm vehicleListForm;

		private static GeneralListForm customerVendorListForm;

		private static GeneralListForm equipmentListForm;

		private static GeneralListForm benefitListForm;

		private static GeneralListForm eosRuleListForm;

		private static TransactionListForm containerTrackingListForm;

		private static CustomerListForm customerListForm;

		private static GeneralListForm caseClientListForm;

		private static VendorListForm vendorListForm;

		private static LeadListForm leadListForm;

		private static EmployeeListForm employeeListForm;

		private static LeadStatusForm leadStatusForm;

		private static ChequeReceiptMultiEntryForm chequeReceiptMultiEntryForm;

		private static LoanEntryForm loanEntryForm;

		private static GeneralListForm buyerListForm;

		private static GeneralListForm companyDocTypeListForm;

		private static GeneralListForm companyDocumentListForm;

		private static GeneralListForm contactListForm;

		private static GeneralListForm countryListForm;

		private static GeneralListForm currencyListForm;

		private static GeneralListForm customerClassListForm;

		private static GeneralListForm customerGroupListForm;

		private static GeneralListForm checkListForm;

		private static GeneralListForm deductionListForm;

		private static GeneralListForm degreeListForm;

		private static GeneralListForm departmentListForm;

		private static GeneralListForm destinationListForm;

		private static GeneralListForm divisionListForm;

		private static GeneralListForm companyDivisionListForm;

		private static GeneralListForm employeeDocTypeListForm;

		private static GeneralListForm employeeGroupListForm;

		private static GeneralListForm employeeTypeListForm;

		private static GeneralListForm gradeListForm;

		private static GeneralListForm leaveTypeListForm;

		private static GeneralListForm vehicleDocTypeListForm;

		private static GeneralListForm jobTypeListForm;

		private static GeneralListForm costCategoryListForm;

		private static GeneralListForm locationListForm;

		private static GeneralListForm nationalityListForm;

		private static GeneralListForm paymentMethodListForm;

		private static GeneralListForm paymentTermListForm;

		private static GeneralListForm positionListForm;

		private static GeneralListForm pricelevelListForm;

		private static GeneralListForm productBrandListForm;

		private static GeneralListForm productCategoryListForm;

		private static GeneralListForm productClassListForm;

		private static GeneralListForm productManufacturerListForm;

		private static GeneralListForm productStyleListForm;

		private static GeneralListForm religionListForm;

		private static GeneralListForm salespersonListForm;

		private static GeneralListForm shippingMethodListForm;

		private static GeneralListForm skillListForm;

		private static GeneralListForm sponsorListForm;

		private static GeneralListForm tenancyContractListForm;

		private static GeneralListForm tradeLicenseListForm;

		private static GeneralListForm unitListForm;

		private static GeneralListForm vendorClassListForm;

		private static GeneralListForm vendorGroupListForm;

		private static GeneralListForm containerSizeListForm;

		private static GeneralListForm INCODetailsListForm;

		private static GeneralListForm visaListForm;

		private static GeneralListForm costCenterListForm;

		private static GeneralListForm returnedChequeReasonListForm;

		private static GeneralListForm chequebookListForm;

		private static GeneralListForm registerListForm;

		private static GeneralListForm portListForm;

		private static GeneralListForm disciplineActionTypeListForm;

		private static GeneralListForm employeeActivityTypeListForm;

		private static GeneralListForm userGroupListForm;

		private static GeneralListForm userListForm;

		private static GeneralListForm vendorAddressListForm;

		private static GeneralListForm customerAddressListForm;

		private static GeneralListForm nationalAccountListForm;

		private static GeneralListForm employeeDocumentListForm;

		private static GeneralListForm driverListForm;

		private static GeneralListForm expenseCodeListForm;

		private static GeneralListForm fixedAssetListForm;

		private static GeneralListForm fixedAssetLocationListForm;

		private static GeneralListForm fixedAssetGroupListForm;

		private static GeneralListForm fixedAssetClassListForm;

		private static GeneralListForm fiscalYearListForm;

		private static GeneralListForm priceListForm;

		private static GeneralListForm candidateListForm;

		private static GeneralListForm workLocationListForm;

		private static GeneralListForm collateralListForm;

		private static GeneralListForm jobTaskListForm;

		private static GeneralListForm jobTaskGroupListForm;

		private static GeneralListForm customGadgetListForm;

		private static GeneralListForm customReportListForm;

		private static GeneralListForm appointmentListForm;

		private static GeneralListForm transporterListForm;

		private static GeneralListForm INCOListForm;

		private static GeneralListForm genericListForm;

		private static GeneralListForm followupDetailsListForm;

		private static GeneralListForm qualityTaskListForm;

		private static GeneralListForm arrivalReportTemplateListForm;

		private static GeneralListForm surveyorListForm;

		private static GeneralListForm employeeLoanTypeListForm;

		private static GeneralListForm employeeLeaveListForm;

		private static GeneralListForm employeeLeaveResumptionListForm;

		private static GeneralListForm employeePassportControlListForm;

		private static GeneralListForm adjustmentTypeDetailsListForm;

		private static GeneralListForm jobBOMListForm;

		private static GeneralListForm packageListForm;

		private static GeneralListForm propertyAgentListForm;

		private static GeneralListForm propertyClassListForm;

		private static GeneralListForm propertyListForm;

		private static GeneralListForm propertyUnitListForm;

		private static GeneralListForm propertyVirtualUnitListForm;

		private static GeneralListForm propertyIncomeCodeListForm;

		private static GeneralListForm inventoryTransferTypeListForm;

		private static GeneralListForm propertyTenantListForm;

		private static GeneralListForm approvalDetailsListForm;

		private static GeneralListForm verificationDetailsListForm;

		private static GeneralListForm binDetailsListForm;

		private static GeneralListForm routeDetailsListForm;

		private static GeneralListForm routeGroupDetailsListForm;

		private static GeneralListForm releaseTypeListForm;

		private static GeneralListForm serviceItemListForm;

		private static GeneralListForm provisionTypeListForm;

		private static GeneralListForm serviceProviderListForm;

		private static GeneralListForm serviceActivityListForm;

		private static GeneralListForm EAEquipmentListForm;

		private static GeneralListForm requisitionTypeListForm;

		private static GeneralListForm SysDocListForm;

		private static GeneralListForm productMakeListForm;

		private static GeneralListForm productTypeListForm;

		private static GeneralListForm productModelListForm;

		private static GeneralListForm taskStepsListForm;

		private static GeneralListForm taskTypeListForm;

		private static GeneralListForm rackListForm;

		private static GeneralListForm productSpecificationListForm;

		private static GeneralListForm printTemplateMapListForm;

		private static GeneralListForm patientListForm;

		private static GeneralListForm patientDocTypeListForm;

		private static GeneralListForm dataSyncListForm;

		private static GeneralListForm kitchenTypeListForm;

		private static GeneralListForm employeeAbscondingList;

		private static TransactionListForm salesInvoiceListForm;

		private static TransactionListForm salesReceiptListForm;

		private static TransactionListForm salesOrderListForm;

		private static TransactionListForm salesEnquiryListForm;

		private static TransactionListForm salesQuoteListForm;

		private static TransactionListForm salesReturnListForm;

		private static TransactionListForm deliveryNoteListForm;

		private static TransactionListForm deliveryReturnListForm;

		private static TransactionListForm arAllocationListForm;

		private static TransactionListForm salesHistoryForm;

		private static TransactionListForm activityLogForm;

		private static TransactionListForm inventoryLedgerForm;

		private static TransactionListForm exportsalesInvoiceListForm;

		private static TransactionListForm exportDeliveryNoteListForm;

		private static TransactionListForm exportPickListListForm;

		private static TransactionListForm salesProformaListForm;

		private static TransactionListForm exportsalesProformaListForm;

		private static TransactionListForm exportPackingListListForm;

		private static TransactionListForm shipmentListForm;

		private static TransactionListForm exportSalesOrderListForm;

		private static TransactionListForm salesInvoiceNIListForm;

		private static TransactionListForm inventoryDamageListForm;

		private static TransactionListForm inventoryRepackingListForm;

		private static TransactionListForm inventoryTransferAcceptanceListForm;

		private static TransactionListForm inventoryTransferReturnListForm;

		private static TransactionListForm inventoryTransferListForm;

		private static TransactionListForm directinventoryTransferListForm;

		private static TransactionListForm jobInvoiceListForm;

		private static TransactionListForm jobInventoryIssueListForm;

		private static TransactionListForm qualityClaimListForm;

		private static TransactionListForm arrivalReportListForm;

		private static TransactionListForm propertyRenewListForm;

		private static TransactionListForm propertyCancelListForm;

		private static TransactionListForm propertyRentListForm;

		private static TransactionListForm propertyServiceInvoiceListForm;

		private static TransactionListForm propertyIncomePostingListForm;

		private static TransactionListForm jobEstimationListForm;

		private static TransactionListForm salarySheetListForm;

		private static TransactionListForm overTimeEntrytListForm;

		private static TransactionListForm employeeSalaryListForm;

		private static TransactionListForm employeeLoanListForm;

		private static TransactionListForm employeeLoanPaymentListForm;

		private static TransactionListForm employeeLoanSettlementList;

		private static TransactionListForm employeeEOSList;

		private static TransactionListForm purchaseInvoiceListForm;

		private static TransactionListForm purchaseInvoiceNIListForm;

		private static TransactionListForm purchaseOrderListForm;

		private static TransactionListForm purchaseOrderNIListForm;

		private static TransactionListForm purchaseQuoteListForm;

		private static TransactionListForm purchaseReturnListForm;

		private static TransactionListForm grnReturnListForm;

		private static TransactionListForm purchaseGRNListForm;

		private static TransactionListForm importPurchaseGRNListForm;

		private static TransactionListForm importPurchaseOrderListForm;

		private static TransactionListForm importPurchaseInvoiceListForm;

		private static TransactionListForm consignInListForm;

		private static TransactionListForm poShipmentListForm;

		private static TransactionListForm apAllocationListForm;

		private static TransactionListForm proformaInvoiceListForm;

		private static TransactionListForm consignmentOutListForm;

		private static TransactionListForm consignInSettlementListForm;

		private static TransactionListForm consignmentOutSettlementListForm;

		private static TransactionListForm purchasePackinglistListForm;

		private static TransactionListForm cashPurchaseListForm;

		private static TransactionListForm fixedAssetPurchaseListForm;

		private static TransactionListForm fixedAssetPurchaseOrderListForm;

		private static TransactionListForm fixedAssetSaleListForm;

		private static TransactionListForm fixedAssetTransferListForm;

		private static TransactionListForm trPaymentList;

		private static TransactionListForm trList;

		private static TransactionListForm clVoucherListFormObj;

		private static TransactionListForm sendChequeToBankListForm;

		private static TransactionListForm chequeDepositListForm;

		private static TransactionListForm jvListForm;

		private static TransactionListForm journalListForm;

		private static TransactionListForm trailBalanceListForm;

		private static TransactionListForm expenseListForm;

		private static TransactionListForm debitNoteListForm;

		private static TransactionListForm creditNoteListForm;

		private static TransactionListForm LPOReceiptListForm;

		private static TransactionListForm receiptVoucherListForm;

		private static TransactionListForm returnVoucherListForm;

		private static TransactionListForm issuedChequeBounceListForm;

		private static TransactionListForm receivedChequeCancellationListForm;

		private static TransactionListForm paymentVoucherListForm;

		private static TransactionListForm crmActivityListForm;

		private static TransactionListForm crmCustomerActivityListForm;

		private static TransactionListForm fundTransferListForm;

		private static TransactionListForm issuedChequeListForm;

		private static TransactionListForm receivedChequeListForm;

		private static TransactionListForm securityChequeListForm;

		private static TransactionListForm cashSalaryPaymentListForm;

		private static TransactionListForm chequeSalaryPaymentListForm;

		private static TransactionListForm transferSalaryPaymentListForm;

		private static TransactionListForm projectExpenseAllocationListForm;

		private static TransactionListForm paymentRequestListForm;

		private static TransactionListForm purchaseClaimListForm;

		private static TransactionListForm buildAssemblyListForm;

		private static TransactionListForm productionListForm;

		private static TransactionListForm workOrderListForm;

		private static TransactionListForm w3PLDeliveryListForm;

		private static TransactionListForm w3PLGRNListForm;

		private static TransactionListForm w3PLInvoiceListForm;

		private static TransactionListForm maintenanceEntryListForm;

		private static TransactionListForm legalActivityListForm;

		private static TransactionListForm legalActionListForm;

		private static TransactionListForm trApplicationListForm;

		private static TransactionListForm chequeReceiptMultiEntryListForm;

		private static TransactionListForm jobMaterialRequistionListForm;

		private static TransactionListForm jobMaterialEstimateListForm;

		private static TransactionListForm jobManHrsBudgetingListForm;

		private static TransactionListForm jobExpenseIssueListForm;

		private static TransactionListForm jobClosingListForm;

		private static TransactionListForm jobInventoryReturnListForm;

		private static TransactionListForm jobTimesheetListForm;

		private static TransactionListForm garmentRentalListForm;

		private static TransactionListForm garmentRentalReturnListForm;

		private static TransactionListForm serviceCallTrackListForm;

		private static TransactionListForm maintenanceSchedulerListForm;

		private static TransactionListForm jobMaintenanceScheduleListForm;

		private static TransactionListForm purchaseCostEntryListForm;

		private static TransactionListForm billOfLadingListForm;

		private static TransactionListForm jobMaintenanceServiceEntryListForm;

		private static TransactionListForm employeeAppraisalListForm;

		private static TransactionListForm customerInsuranceListForm;

		private static TransactionListForm projectSubContractListForm;

		private static TransactionListForm projectSubContractInvoiceListForm;

		private static TransactionListForm employeeGeneralActivityListForm;

		private static TransactionListForm employeeOpeningBalanceLeaveList;

		private static TransactionListForm employeeProvisionListForm;

		private static TransactionListForm employeeLeavePaymentListForm;

		private static TransactionListForm employeePerformanceListForm;

		private static TransactionListForm employeeLeaveProcessListForm;

		private static TransactionListForm consignInClosingListForm;

		private static TransactionListForm freightChargesListForm;

		private static TransactionListForm priceListListForm;

		private static TransactionListForm requisitionListFormObj;

		private static TransactionListForm mobilizationListFormObj;

		private static TransactionListForm FixedAssetBulkPurchaseListForm;

		private static TransactionListForm FixedAssetDepListForm;

		private static TransactionListForm itemTransactionListForm;

		private static TransactionListForm issuedChequeClearanceListForm;

		private static TransactionListForm equipmentTransferListFormObj;

		private static TransactionListForm equipmentWorkOrderListForm;

		private static TransactionListForm inventoryAdjustmentListForm;

		private static TransactionListForm customerOpeningBalanceBatchListForm;

		private static TransactionListForm vendorOpeningBalanceBatchListForm;

		private static TransactionListForm employeeOpeningBalanceBatchListForm;

		private static TransactionListForm inventoryOpeningBalanceBatchListForm;

		private static TransactionListForm workOrderInventoryIssueListForm;

		private static TransactionListForm workOrderInventoryReturnListForm;

		private static TransactionListForm consignOutReturnListForm;

		private static TransactionListForm SalaryAdditionListForm;

		private static TransactionListForm inventoryDismantleListForm;

		private static TransactionListForm salaryDeductionListForm;

		private static TransactionListForm produtPriceBulkUpdateListForm;

		private static TransactionListForm chequeReceiptOpeningListForm;

		private static TransactionListForm chequepaymentVoucherListForm;

		private static TransactionListForm materialResevationList;

		private static TransactionListForm SalesForecastingList;

		private static TransactionListForm IssuedChequeCancellationListForm;

		private static TransactionListForm purchasePrepaymentInvoiceList;

		private static TransactionListForm taskTransactionList;

		private static TransactionListForm budgetingListForm;

		private static TransactionListForm vehiclemileagetrackListForm;

		private static TransactionListForm salesmantargetListForm;

		private static TransactionListForm customerInsuranceClaimListForm;

		private static TransactionListForm chequeDiscountListForm;

		private static TransactionListForm billDiscountListForm;

		private static TransactionListForm standingJournalListForm;

		private static TransactionListForm loanentryListForm;

		private static TransactionListForm employeeLeaveRequestListForm;

		private static TransactionListForm issuedChequeCancellationListForm;

		private static TransactionListForm propertyServiceRequestListForm;

		private static TransactionListForm propertyServiceAssignListForm;

		private static SortedList<string, CustomReportDisplayForm> customReportFormList;

		private static string selectedsysDocID;

		public static bool LoadComplete
		{
			get
			{
				return loadComplete;
			}
			set
			{
				loadComplete = value;
			}
		}

		public static bool ProgramLoaded
		{
			get
			{
				return programLoaded;
			}
			set
			{
				programLoaded = value;
			}
		}

		public static UploadFileDialog UploadFileDialogObj
		{
			get
			{
				if (uploadFileDialog == null || uploadFileDialog.IsDisposed)
				{
					uploadFileDialog = new UploadFileDialog();
				}
				return uploadFileDialog;
			}
		}

		public static EditFileDialog EditFileDialogObj
		{
			get
			{
				if (editFileDialog == null || editFileDialog.IsDisposed)
				{
					editFileDialog = new EditFileDialog();
				}
				return editFileDialog;
			}
		}

		public static DocManagementForm DocManagementFormObj
		{
			get
			{
				if (docManagementForm == null || docManagementForm.IsDisposed)
				{
					docManagementForm = new DocManagementForm();
				}
				return docManagementForm;
			}
		}

		public static JobDetailsForm JobDetailsFormObj
		{
			get
			{
				if (jobDetailsForm == null || jobDetailsForm.IsDisposed)
				{
					jobDetailsForm = new JobDetailsForm();
				}
				return jobDetailsForm;
			}
		}

		public static OpportunityDetailsForm OpportunityDetailsFormObj
		{
			get
			{
				if (opportunityDetailsForm == null || opportunityDetailsForm.IsDisposed)
				{
					opportunityDetailsForm = new OpportunityDetailsForm();
				}
				return opportunityDetailsForm;
			}
		}

		public static EventDetailsForm EventDetailsFormObj
		{
			get
			{
				if (eventDetailsForm == null || eventDetailsForm.IsDisposed)
				{
					eventDetailsForm = new EventDetailsForm();
				}
				return eventDetailsForm;
			}
		}

		public static CompetitorDetailsForm CompetitorDetailsFormObj
		{
			get
			{
				if (competitorDetailsForm == null || competitorDetailsForm.IsDisposed)
				{
					competitorDetailsForm = new CompetitorDetailsForm();
				}
				return competitorDetailsForm;
			}
		}

		public static ActivityDetailsForm ActivityDetailsFormObj
		{
			get
			{
				if (activityDetailsForm == null || activityDetailsForm.IsDisposed)
				{
					activityDetailsForm = new ActivityDetailsForm();
				}
				return activityDetailsForm;
			}
		}

		public static CustomReportDetailForm CustomReportDetailFormObj
		{
			get
			{
				if (customReportDetailForm == null || customReportDetailForm.IsDisposed)
				{
					customReportDetailForm = new CustomReportDetailForm();
				}
				return customReportDetailForm;
			}
		}

		public static InventoryAdjustmentReport InventoryAdjustmentReportFormObj
		{
			get
			{
				if (inventoryAdjustmentReportForm == null || inventoryAdjustmentReportForm.IsDisposed)
				{
					inventoryAdjustmentReportForm = new InventoryAdjustmentReport();
				}
				return inventoryAdjustmentReportForm;
			}
		}

		public static PendingGRNReport PendingGRNReportFormObj
		{
			get
			{
				if (pendingGRNReportForm == null || pendingGRNReportForm.IsDisposed)
				{
					pendingGRNReportForm = new PendingGRNReport();
				}
				return pendingGRNReportForm;
			}
		}

		public static TRPaymentForm TRPaymentFormObj
		{
			get
			{
				if (trPaymentForm == null || trPaymentForm.IsDisposed)
				{
					trPaymentForm = new TRPaymentForm();
				}
				return trPaymentForm;
			}
		}

		public static ProjectStatusReportForm ProjectStatusReportFormObj
		{
			get
			{
				if (projectStatusReportForm == null || projectStatusReportForm.IsDisposed)
				{
					projectStatusReportForm = new ProjectStatusReportForm();
				}
				return projectStatusReportForm;
			}
		}

		public static PurchaseComparisonReport PurchaseComparisonReportFormObj
		{
			get
			{
				if (purchaseComparisonReportForm == null || purchaseComparisonReportForm.IsDisposed)
				{
					purchaseComparisonReportForm = new PurchaseComparisonReport();
				}
				return purchaseComparisonReportForm;
			}
		}

		public static MaterialVarianceReportForm MaterialVarianceReportFormObj
		{
			get
			{
				if (materialVarianceReportForm == null || materialVarianceReportForm.IsDisposed)
				{
					materialVarianceReportForm = new MaterialVarianceReportForm();
				}
				return materialVarianceReportForm;
			}
		}

		public static JobSummaryReportForm JobSummaryReportFormObj
		{
			get
			{
				if (jobSummaryReportForm == null || jobSummaryReportForm.IsDisposed)
				{
					jobSummaryReportForm = new JobSummaryReportForm();
				}
				return jobSummaryReportForm;
			}
		}

		public static EmployeeLeaveReport EmployeeLeaveReportFormObj
		{
			get
			{
				if (employeeLeaveReportForm == null || employeeLeaveReportForm.IsDisposed)
				{
					employeeLeaveReportForm = new EmployeeLeaveReport();
				}
				return employeeLeaveReportForm;
			}
		}

		public static EmployeeLeaveStatusReport EmployeeLeaveStatusReportFormObj
		{
			get
			{
				if (leaveStatusReportForm == null || leaveStatusReportForm.IsDisposed)
				{
					leaveStatusReportForm = new EmployeeLeaveStatusReport();
				}
				return leaveStatusReportForm;
			}
		}

		public static EmployeeAnnualLeaveDueReport EmployeeAnnualLeaveDueReportFormObj
		{
			get
			{
				if (employeeAnnualLeaveDueReportForm == null || employeeAnnualLeaveDueReportForm.IsDisposed)
				{
					employeeAnnualLeaveDueReportForm = new EmployeeAnnualLeaveDueReport();
				}
				return employeeAnnualLeaveDueReportForm;
			}
		}

		public static EmployeeLoanReport EmployeeLoanReportFormObj
		{
			get
			{
				if (employeeLoanReportForm == null || employeeLoanReportForm.IsDisposed)
				{
					employeeLoanReportForm = new EmployeeLoanReport();
				}
				return employeeLoanReportForm;
			}
		}

		public static PurchaseLogReport PurchaseLogReportObj
		{
			get
			{
				if (purchaseLogReportForm == null || purchaseLogReportForm.IsDisposed)
				{
					purchaseLogReportForm = new PurchaseLogReport();
				}
				return purchaseLogReportForm;
			}
		}

		public static DiscountChequesReportForm DiscountChequesReportFormObj
		{
			get
			{
				if (discountChequesReportForm == null || discountChequesReportForm.IsDisposed)
				{
					discountChequesReportForm = new DiscountChequesReportForm();
				}
				return discountChequesReportForm;
			}
		}

		public static OverTimeEntryForm OverTimeEntryFormObj
		{
			get
			{
				if (overTimeEntryForm == null || overTimeEntryForm.IsDisposed)
				{
					overTimeEntryForm = new OverTimeEntryForm();
				}
				return overTimeEntryForm;
			}
		}

		public static SalaryDeductionForm SalaryDeductionFormObj
		{
			get
			{
				if (salaryDeductionForm == null || salaryDeductionForm.IsDisposed)
				{
					salaryDeductionForm = new SalaryDeductionForm();
				}
				return salaryDeductionForm;
			}
		}

		public static SalaryAdditionForm SalaryAdditionFormObj
		{
			get
			{
				if (salaryAdditionForm == null || salaryAdditionForm.IsDisposed)
				{
					salaryAdditionForm = new SalaryAdditionForm();
				}
				return salaryAdditionForm;
			}
		}

		public static EmployeeOverTimeReport EmployeeOverTimeReportFormObj
		{
			get
			{
				if (employeeOverTimeReportForm == null || employeeOverTimeReportForm.IsDisposed)
				{
					employeeOverTimeReportForm = new EmployeeOverTimeReport();
				}
				return employeeOverTimeReportForm;
			}
		}

		public static EmployeeGraduityEligibilityReport EmployeeGraduityEligibilityReportFormObj
		{
			get
			{
				if (employeeGraduityEligibilityReportForm == null || employeeGraduityEligibilityReportForm.IsDisposed)
				{
					employeeGraduityEligibilityReportForm = new EmployeeGraduityEligibilityReport();
				}
				return employeeGraduityEligibilityReportForm;
			}
		}

		public static EmployeeHistoryReport EmployeeHistoryReportFormObj
		{
			get
			{
				if (employeeHistoryReportForm == null || employeeHistoryReportForm.IsDisposed)
				{
					employeeHistoryReportForm = new EmployeeHistoryReport();
				}
				return employeeHistoryReportForm;
			}
		}

		public static EmployeeSalaryReport EmployeeSalaryReportFormObj
		{
			get
			{
				if (employeeSalaryReportForm == null || employeeSalaryReportForm.IsDisposed)
				{
					employeeSalaryReportForm = new EmployeeSalaryReport();
				}
				return employeeSalaryReportForm;
			}
		}

		public static PDCIssuedReportForm PDCIssuedReportFormObj
		{
			get
			{
				if (pdcIssuedReportForm == null || pdcIssuedReportForm.IsDisposed)
				{
					pdcIssuedReportForm = new PDCIssuedReportForm();
				}
				return pdcIssuedReportForm;
			}
		}

		public static PDCReceivedReportForm PDCReceivedReportFormObj
		{
			get
			{
				if (pdcReceivedReportForm == null || pdcReceivedReportForm.IsDisposed)
				{
					pdcReceivedReportForm = new PDCReceivedReportForm();
				}
				return pdcReceivedReportForm;
			}
		}

		public static InventoryDamageForm InventoryDamageFormObj
		{
			get
			{
				if (inventoryDamageForm == null || inventoryDamageForm.IsDisposed)
				{
					inventoryDamageForm = new InventoryDamageForm();
				}
				return inventoryDamageForm;
			}
		}

		public static ProductStockPivotListReport ProductStockPivotListReportFormObj
		{
			get
			{
				if (productStockPivotListReport == null || productStockPivotListReport.IsDisposed)
				{
					productStockPivotListReport = new ProductStockPivotListReport();
				}
				return productStockPivotListReport;
			}
		}

		public static W3PLProductStockPivotListReport W3PLProductStockPivotListReportFormObj
		{
			get
			{
				if (W3PLproductStockPivotListReport == null || W3PLproductStockPivotListReport.IsDisposed)
				{
					W3PLproductStockPivotListReport = new W3PLProductStockPivotListReport();
				}
				return W3PLproductStockPivotListReport;
			}
		}

		public static EquipmentDetailsForm EquipmentDetailFormObj
		{
			get
			{
				if (equipmentDetailForm == null || equipmentDetailForm.IsDisposed)
				{
					equipmentDetailForm = new EquipmentDetailsForm();
				}
				return equipmentDetailForm;
			}
		}

		public static JobClosingForm ProjectClosingFormObj
		{
			get
			{
				if (jobClosingForm == null || jobClosingForm.IsDisposed)
				{
					jobClosingForm = new JobClosingForm();
				}
				return jobClosingForm;
			}
		}

		public static CampaignDetailsForm CampaignDetailsFormObj
		{
			get
			{
				if (campaignDetailsForm == null || campaignDetailsForm.IsDisposed)
				{
					campaignDetailsForm = new CampaignDetailsForm();
				}
				return campaignDetailsForm;
			}
		}

		public static JobInventoryIssueForm JobInventoryIssueFormObj
		{
			get
			{
				if (jobInventoryIssueForm == null || jobInventoryIssueForm.IsDisposed)
				{
					jobInventoryIssueForm = new JobInventoryIssueForm();
				}
				return jobInventoryIssueForm;
			}
		}

		public static JobInventoryReturnForm JobInventoryReturnFormObj
		{
			get
			{
				if (jobInventoryReturnForm == null || jobInventoryReturnForm.IsDisposed)
				{
					jobInventoryReturnForm = new JobInventoryReturnForm();
				}
				return jobInventoryReturnForm;
			}
		}

		public static JobExpenseIssueForm JobExpenseIssueFormObj
		{
			get
			{
				if (jobExpenseIssueForm == null || jobExpenseIssueForm.IsDisposed)
				{
					jobExpenseIssueForm = new JobExpenseIssueForm();
				}
				return jobExpenseIssueForm;
			}
		}

		public static ProjectBillingForm JobInvoiceFormObj
		{
			get
			{
				if (jobInvoiceForm == null || jobInvoiceForm.IsDisposed)
				{
					jobInvoiceForm = new ProjectBillingForm();
				}
				return jobInvoiceForm;
			}
		}

		public static JobTimesheetForm JobTimesheetFormObj
		{
			get
			{
				if (jobTimesheetForm == null || jobTimesheetForm.IsDisposed)
				{
					jobTimesheetForm = new JobTimesheetForm();
				}
				return jobTimesheetForm;
			}
		}

		public static EmployeeLeaveProcessForm EmployeeLeaveProcessFormObj
		{
			get
			{
				if (employeeLeaveProcessForm == null || employeeLeaveProcessForm.IsDisposed)
				{
					employeeLeaveProcessForm = new EmployeeLeaveProcessForm();
				}
				return employeeLeaveProcessForm;
			}
		}

		public static BankFacilityGroupDetailsForm BankFacilityGroupFormObj
		{
			get
			{
				if (bankFacilityGroupForm == null || bankFacilityGroupForm.IsDisposed)
				{
					bankFacilityGroupForm = new BankFacilityGroupDetailsForm();
				}
				return bankFacilityGroupForm;
			}
		}

		public static BankFacilityDetailsForm BankFacilityFormObj
		{
			get
			{
				if (bankFacilityForm == null || bankFacilityForm.IsDisposed)
				{
					bankFacilityForm = new BankFacilityDetailsForm();
				}
				return bankFacilityForm;
			}
		}

		public static CustomerOpeningBalanceBatchForm CustomerOpeningBalanceBatchFormObj
		{
			get
			{
				if (customerOpeningBalanceBatchForm == null || customerOpeningBalanceBatchForm.IsDisposed)
				{
					customerOpeningBalanceBatchForm = new CustomerOpeningBalanceBatchForm();
				}
				return customerOpeningBalanceBatchForm;
			}
		}

		public static EmployeeOpeningBalanceBatchForm EmployeeOpeningBalanceBatchFormObj
		{
			get
			{
				if (employeeOpeningBalanceBatchForm == null || employeeOpeningBalanceBatchForm.IsDisposed)
				{
					employeeOpeningBalanceBatchForm = new EmployeeOpeningBalanceBatchForm();
				}
				return employeeOpeningBalanceBatchForm;
			}
		}

		public static EmployeeOpeningBalanceLeaveForm EmployeeOpeningBalanceLeaveFormObj
		{
			get
			{
				if (employeeOpeningBalanceLeaveForm == null || employeeOpeningBalanceLeaveForm.IsDisposed)
				{
					employeeOpeningBalanceLeaveForm = new EmployeeOpeningBalanceLeaveForm();
				}
				return employeeOpeningBalanceLeaveForm;
			}
		}

		public static VendorOutstandingInvoicesReport VendorOutstandingInvoicesReportFormObj
		{
			get
			{
				if (vendorOutstandingInvoicesReportForm == null || vendorOutstandingInvoicesReportForm.IsDisposed)
				{
					vendorOutstandingInvoicesReportForm = new VendorOutstandingInvoicesReport();
				}
				return vendorOutstandingInvoicesReportForm;
			}
		}

		public static VendorOpeningBalanceBatchForm VendorOpeningBalanceBatchFormObj
		{
			get
			{
				if (vendorOpeningBalanceBatchForm == null || vendorOpeningBalanceBatchForm.IsDisposed)
				{
					vendorOpeningBalanceBatchForm = new VendorOpeningBalanceBatchForm();
				}
				return vendorOpeningBalanceBatchForm;
			}
		}

		public static PurchaseStockEntry InventoryOpeningBalanceBatchFormObj
		{
			get
			{
				if (inventoryOpeningBalanceBatchForm == null || inventoryOpeningBalanceBatchForm.IsDisposed)
				{
					inventoryOpeningBalanceBatchForm = new PurchaseStockEntry();
				}
				return inventoryOpeningBalanceBatchForm;
			}
		}

		public static NationalAccountForm CustomerNationalAccountFormObj
		{
			get
			{
				if (nationalAccountForm == null || nationalAccountForm.IsDisposed)
				{
					nationalAccountForm = new NationalAccountForm();
				}
				return nationalAccountForm;
			}
		}

		public static CustomerVendorLinkForm CustomerVendorLinkFormObj
		{
			get
			{
				if (customerVendorLinkForm == null || customerVendorLinkForm.IsDisposed)
				{
					customerVendorLinkForm = new CustomerVendorLinkForm();
				}
				return customerVendorLinkForm;
			}
		}

		public static BankReconciliationForm BankReconciliationFormObj
		{
			get
			{
				if (bankReconciliationForm == null || bankReconciliationForm.IsDisposed)
				{
					bankReconciliationForm = new BankReconciliationForm();
				}
				return bankReconciliationForm;
			}
		}

		public static BankReconciliationOpeningForm BankReconciliationOpeningFormObj
		{
			get
			{
				if (bankReconciliationOpeningForm == null || bankReconciliationOpeningForm.IsDisposed)
				{
					bankReconciliationOpeningForm = new BankReconciliationOpeningForm();
				}
				return bankReconciliationOpeningForm;
			}
		}

		public static BankReconciliationsReportForm BankReconciliationsReportFormObj
		{
			get
			{
				if (bankReconciliationReportForm == null || bankReconciliationReportForm.IsDisposed)
				{
					bankReconciliationReportForm = new BankReconciliationsReportForm();
				}
				return bankReconciliationReportForm;
			}
		}

		public static BankNotReconciledReportForm BankNotReconciledReportFormObj
		{
			get
			{
				if (bankNotReconciledReportForm == null || bankNotReconciledReportForm.IsDisposed)
				{
					bankNotReconciledReportForm = new BankNotReconciledReportForm();
				}
				return bankNotReconciledReportForm;
			}
		}

		public static CityDetailsForm CityDetailsFormObj
		{
			get
			{
				if (cityDetailsForm == null || cityDetailsForm.IsDisposed)
				{
					cityDetailsForm = new CityDetailsForm();
				}
				return cityDetailsForm;
			}
		}

		public static GenericListDetailsForm AgentDetailsFormObj
		{
			get
			{
				if (agentDetailsForm == null || agentDetailsForm.IsDisposed)
				{
					agentDetailsForm = new GenericListDetailsForm();
					agentDetailsForm.GenericListType = GenericListTypes.Agent;
					agentDetailsForm.Text = "Agent Details";
				}
				return agentDetailsForm;
			}
		}

		public static GenericListDetailsForm CRMActivityReasonDetailsFormObj
		{
			get
			{
				if (crmActivityReasonDetailsForm == null || crmActivityReasonDetailsForm.IsDisposed)
				{
					crmActivityReasonDetailsForm = new GenericListDetailsForm();
					crmActivityReasonDetailsForm.GenericListType = GenericListTypes.CRMActivityReason;
					crmActivityReasonDetailsForm.Text = "Activity Reason";
				}
				return crmActivityReasonDetailsForm;
			}
		}

		public static GenericListDetailsForm CRMEventTypeDetailsFormObj
		{
			get
			{
				if (crmEventTypeDetailsForm == null || crmEventTypeDetailsForm.IsDisposed)
				{
					crmEventTypeDetailsForm = new GenericListDetailsForm();
					crmEventTypeDetailsForm.GenericListType = GenericListTypes.CRMEventType;
					crmEventTypeDetailsForm.Text = "Event Type";
				}
				return crmEventTypeDetailsForm;
			}
		}

		public static GenericListDetailsForm ActivityStatusFormObj
		{
			get
			{
				if (activityStatusForm == null || activityStatusForm.IsDisposed)
				{
					activityStatusForm = new GenericListDetailsForm();
					activityStatusForm.GenericListType = GenericListTypes.ActionStatus;
					activityStatusForm.Text = "Legal Status";
				}
				return activityStatusForm;
			}
		}

		public static GenericListDetailsForm CaseTypeFormObj
		{
			get
			{
				if (caseTypeForm == null || caseTypeForm.IsDisposed)
				{
					caseTypeForm = new GenericListDetailsForm();
					caseTypeForm.GenericListType = GenericListTypes.CaseType;
					caseTypeForm.Text = "Case Type";
				}
				return caseTypeForm;
			}
		}

		public static GenericListDetailsForm GenericListDetailsFormObj
		{
			get
			{
				if (genericListDetailsForm == null || genericListDetailsForm.IsDisposed)
				{
					genericListDetailsForm = new GenericListDetailsForm();
				}
				return genericListDetailsForm;
			}
		}

		public static GenericListDetailsForm ChronicsDetailsFormObj
		{
			get
			{
				if (chronicsDetailsForm == null || chronicsDetailsForm.IsDisposed)
				{
					chronicsDetailsForm = new GenericListDetailsForm();
					chronicsDetailsForm.GenericListType = GenericListTypes.Chronics;
					chronicsDetailsForm.Text = "Chronics";
				}
				return chronicsDetailsForm;
			}
		}

		public static GenericListDetailsForm AllergyDetailsFormObj
		{
			get
			{
				if (allergyDetailsForm == null || allergyDetailsForm.IsDisposed)
				{
					allergyDetailsForm = new GenericListDetailsForm();
					allergyDetailsForm.GenericListType = GenericListTypes.Allergy;
					allergyDetailsForm.Text = "Allergy";
				}
				return allergyDetailsForm;
			}
		}

		public static GenericListDetailsForm CollateralCustodianFormObj
		{
			get
			{
				if (collateralCustodianForm == null || collateralCustodianForm.IsDisposed)
				{
					collateralCustodianForm = new GenericListDetailsForm();
					collateralCustodianForm.GenericListType = GenericListTypes.CollateralCustodian;
					collateralCustodianForm.Text = "CollateralCustodian";
				}
				return collateralCustodianForm;
			}
		}

		public static GenericListDetailsForm LegalPositionFormObj
		{
			get
			{
				if (legalPositionFormObj == null || legalPositionFormObj.IsDisposed)
				{
					legalPositionFormObj = new GenericListDetailsForm();
					legalPositionFormObj.GenericListType = GenericListTypes.LegalPosition;
					legalPositionFormObj.Text = "Legal Position";
				}
				return legalPositionFormObj;
			}
		}

		public static CustomListSetupForm CustomListSetupFormObj
		{
			get
			{
				if (customListSetupFormObj == null || customListSetupFormObj.IsDisposed)
				{
					customListSetupFormObj = new CustomListSetupForm();
				}
				return customListSetupFormObj;
			}
		}

		public static GenericProductTypeDetailsForm GenericProductTypeListDetailsFormObj
		{
			get
			{
				if (genericProductTypeListDetailsForm == null || genericProductTypeListDetailsForm.IsDisposed)
				{
					genericProductTypeListDetailsForm = new GenericProductTypeDetailsForm();
				}
				return genericProductTypeListDetailsForm;
			}
		}

		public static WorkOrderDetailsForm WorkOrderDetailsFormObj
		{
			get
			{
				if (workOrderDetailsForm == null || workOrderDetailsForm.IsDisposed)
				{
					workOrderDetailsForm = new WorkOrderDetailsForm();
				}
				return workOrderDetailsForm;
			}
		}

		public static JobBudgetVsActualReport JobBudgetVsActualReportObj
		{
			get
			{
				if (jobBudgetVsActualReport == null || jobBudgetVsActualReport.IsDisposed)
				{
					jobBudgetVsActualReport = new JobBudgetVsActualReport();
				}
				return jobBudgetVsActualReport;
			}
		}

		public static TREntryForm TREntryFormObj
		{
			get
			{
				if (trEntryForm == null || trEntryForm.IsDisposed)
				{
					trEntryForm = new TREntryForm();
				}
				return trEntryForm;
			}
		}

		public static TRApplicationForm TRApplicationFormObj
		{
			get
			{
				if (trApplicationForm == null || trApplicationForm.IsDisposed)
				{
					trApplicationForm = new TRApplicationForm();
				}
				return trApplicationForm;
			}
		}

		public static InventoryAgingReport InventoryAgingReportObj
		{
			get
			{
				if (inventoryAgingReport == null || inventoryAgingReport.IsDisposed)
				{
					inventoryAgingReport = new InventoryAgingReport();
				}
				return inventoryAgingReport;
			}
		}

		public static W3PLInventoryAgingReport W3PLInventoryAgingReportObj
		{
			get
			{
				if (W3PLinventoryAgingReport == null || W3PLinventoryAgingReport.IsDisposed)
				{
					W3PLinventoryAgingReport = new W3PLInventoryAgingReport();
				}
				return W3PLinventoryAgingReport;
			}
		}

		public static SendChequesToBankForm SendChequesToBankFormObj
		{
			get
			{
				if (sendChequesToBankForm == null || sendChequesToBankForm.IsDisposed)
				{
					sendChequesToBankForm = new SendChequesToBankForm();
				}
				return sendChequesToBankForm;
			}
		}

		public static ChequeDiscountForm ChequeDiscountFormObj
		{
			get
			{
				if (chequeDiscountForm == null || chequeDiscountForm.IsDisposed)
				{
					chequeDiscountForm = new ChequeDiscountForm();
				}
				return chequeDiscountForm;
			}
		}

		public static InventoryTransferTypeDetailsForm InventoryTransferTypeDetailsFormObj
		{
			get
			{
				if (inventoryTransferTypeDetailsForm == null || inventoryTransferTypeDetailsForm.IsDisposed)
				{
					inventoryTransferTypeDetailsForm = new InventoryTransferTypeDetailsForm();
				}
				return inventoryTransferTypeDetailsForm;
			}
		}

		public static ExportPackingListForm ExportPackingListFormObj
		{
			get
			{
				if (exportPackingListForm == null || exportPackingListForm.IsDisposed)
				{
					exportPackingListForm = new ExportPackingListForm();
				}
				return exportPackingListForm;
			}
		}

		public static ShipmentForm ShipmentFormObj
		{
			get
			{
				if (shipmentForm == null || shipmentForm.IsDisposed)
				{
					shipmentForm = new ShipmentForm();
				}
				return shipmentForm;
			}
		}

		public static LeaveEncashmentForm LeaveEncashmentFormObj
		{
			get
			{
				if (leaveEncashmentForm == null || leaveEncashmentForm.IsDisposed)
				{
					leaveEncashmentForm = new LeaveEncashmentForm();
				}
				return leaveEncashmentForm;
			}
		}

		public static LeaveSalaryPaymentForm LeaveSalaryPaymentFormObj
		{
			get
			{
				if (leaveSalaryPaymentForm == null || leaveSalaryPaymentForm.IsDisposed)
				{
					leaveSalaryPaymentForm = new LeaveSalaryPaymentForm();
				}
				return leaveSalaryPaymentForm;
			}
		}

		public static StandingJournalEntryForm StandingJournalEntryFormObj
		{
			get
			{
				if (standingJournalEntryForm == null || standingJournalEntryForm.IsDisposed)
				{
					standingJournalEntryForm = new StandingJournalEntryForm();
				}
				return standingJournalEntryForm;
			}
		}

		public static ApprovalDetailsForm ApprovalDetailsFormObj
		{
			get
			{
				if (approvalDetailsForm == null || approvalDetailsForm.IsDisposed)
				{
					approvalDetailsForm = new ApprovalDetailsForm();
				}
				return approvalDetailsForm;
			}
		}

		public static CheckListDetailsForm CheckListDetailsFormObj
		{
			get
			{
				if (checkListDetailsForm == null || checkListDetailsForm.IsDisposed)
				{
					checkListDetailsForm = new CheckListDetailsForm();
				}
				return checkListDetailsForm;
			}
		}

		public static VerificationDetailsForm VerificationDetailsFormObj
		{
			get
			{
				if (verificationDetailsForm == null || verificationDetailsForm.IsDisposed)
				{
					verificationDetailsForm = new VerificationDetailsForm();
				}
				return verificationDetailsForm;
			}
		}

		public static PerformApprovalForm PerformApprovalFormObj
		{
			get
			{
				if (performApprovalForm == null || performApprovalForm.IsDisposed)
				{
					performApprovalForm = new PerformApprovalForm();
				}
				return performApprovalForm;
			}
		}

		public static PropertyRentDetailsForm PropertyRentDetailsFormObj
		{
			get
			{
				if (propertyRentDetailsForm == null || propertyRentDetailsForm.IsDisposed)
				{
					propertyRentDetailsForm = new PropertyRentDetailsForm();
				}
				return propertyRentDetailsForm;
			}
		}

		public static PropertyRentRenewDetailsForm PropertyRenewDetailsFormObj
		{
			get
			{
				if (propertyRenewDetailsForm == null || propertyRenewDetailsForm.IsDisposed)
				{
					propertyRenewDetailsForm = new PropertyRentRenewDetailsForm();
				}
				return propertyRenewDetailsForm;
			}
		}

		public static PropertyServiceRequestForm PropertyServiceRequestFormObj
		{
			get
			{
				if (propertyServiceRequestForm == null || propertyServiceRequestForm.IsDisposed)
				{
					propertyServiceRequestForm = new PropertyServiceRequestForm();
					propertyServiceRequestForm.VoucherID = "1";
				}
				return propertyServiceRequestForm;
			}
		}

		public static PropertyServiceAssignForm PropertyServiceAssignFormObj
		{
			get
			{
				if (PropertyServiceAssignForm == null || PropertyServiceAssignForm.IsDisposed)
				{
					PropertyServiceAssignForm = new PropertyServiceAssignForm();
					PropertyServiceAssignForm.VoucherID = "";
				}
				return PropertyServiceAssignForm;
			}
		}

		public static PropertyRentCancellationForm PropertyRentCancellationFormObj
		{
			get
			{
				if (propertyRentCancellationForm == null || propertyRentCancellationForm.IsDisposed)
				{
					propertyRentCancellationForm = new PropertyRentCancellationForm();
				}
				return propertyRentCancellationForm;
			}
		}

		public static RentIncomePostingDetails RentIncomePostingDetailsFormObj
		{
			get
			{
				if (rentIncomePostingDetails == null || rentIncomePostingDetails.IsDisposed)
				{
					rentIncomePostingDetails = new RentIncomePostingDetails();
				}
				return rentIncomePostingDetails;
			}
		}

		public static PropertyTenantForm PropertyTenantFormObj
		{
			get
			{
				if (propertyTenantForm == null || propertyTenantForm.IsDisposed)
				{
					propertyTenantForm = new PropertyTenantForm();
				}
				return propertyTenantForm;
			}
		}

		public static PropertyTenantClassDetailsForm PropertyTenantClassDetailsFormObj
		{
			get
			{
				if (propertyTenantClassDetailsForm == null || propertyTenantClassDetailsForm.IsDisposed)
				{
					propertyTenantClassDetailsForm = new PropertyTenantClassDetailsForm();
				}
				return propertyTenantClassDetailsForm;
			}
		}

		public static W3PLGRNForm W3PLGRNFormObj
		{
			get
			{
				if (w3PLGRNForm == null || w3PLGRNForm.IsDisposed)
				{
					w3PLGRNForm = new W3PLGRNForm();
				}
				return w3PLGRNForm;
			}
		}

		public static W3PLDeliveryForm W3PLDeliveryFormObj
		{
			get
			{
				if (w3PLDeliveryForm == null || w3PLDeliveryForm.IsDisposed)
				{
					w3PLDeliveryForm = new W3PLDeliveryForm();
				}
				return w3PLDeliveryForm;
			}
		}

		public static W3PLInvoiceForm W3PLInvoiceFormObj
		{
			get
			{
				if (w3PLInvoiceForm == null || w3PLInvoiceForm.IsDisposed)
				{
					w3PLInvoiceForm = new W3PLInvoiceForm();
				}
				return w3PLInvoiceForm;
			}
		}

		public static CLVoucherForm CLVoucherFormObj
		{
			get
			{
				if (clVoucherForm == null || clVoucherForm.IsDisposed)
				{
					clVoucherForm = new CLVoucherForm();
				}
				return clVoucherForm;
			}
		}

		public static EntityFlagDetailsForm EntityFlagDetailsFormObj
		{
			get
			{
				if (entityFlagDetailsForm == null || entityFlagDetailsForm.IsDisposed)
				{
					entityFlagDetailsForm = new EntityFlagDetailsForm();
				}
				return entityFlagDetailsForm;
			}
		}

		public static EmployeePerformanceCardForm EmployeePerformanceCardFormObj
		{
			get
			{
				if (employeePerformanceCardForm == null || employeePerformanceCardForm.IsDisposed)
				{
					employeePerformanceCardForm = new EmployeePerformanceCardForm();
				}
				return employeePerformanceCardForm;
			}
		}

		public static DocumentOCRForm DocumentOCRFormObj
		{
			get
			{
				if (documentOCRForm == null || documentOCRForm.IsDisposed)
				{
					documentOCRForm = new DocumentOCRForm();
				}
				return documentOCRForm;
			}
		}

		public static PurchaseCostEntryReport PurchaseCostEntryReportFormObj
		{
			get
			{
				if (purchaseCostEntryReportForm == null || purchaseCostEntryReportForm.IsDisposed)
				{
					purchaseCostEntryReportForm = new PurchaseCostEntryReport();
				}
				return purchaseCostEntryReportForm;
			}
		}

		public static PaymentAdviceDetailsForm PaymentAdviceDetailsFormObj
		{
			get
			{
				if (paymentAdviceDetailsForm == null || paymentAdviceDetailsForm.IsDisposed)
				{
					paymentAdviceDetailsForm = new PaymentAdviceDetailsForm();
				}
				return paymentAdviceDetailsForm;
			}
		}

		public static VerifyObjectForm VerifyObjectFormObj
		{
			get
			{
				if (verifyObjectForm == null || verifyObjectForm.IsDisposed)
				{
					verifyObjectForm = new VerifyObjectForm();
				}
				return verifyObjectForm;
			}
		}

		public static CandidateDetailsForm CandidateDetailsFormObj
		{
			get
			{
				if (candidateDetailsForm == null || candidateDetailsForm.IsDisposed)
				{
					candidateDetailsForm = new CandidateDetailsForm();
				}
				return candidateDetailsForm;
			}
		}

		public static VehicleDetailsForm VehicleDetailsFormObj
		{
			get
			{
				if (vehicleDetailsForm == null || vehicleDetailsForm.IsDisposed)
				{
					vehicleDetailsForm = new VehicleDetailsForm();
				}
				return vehicleDetailsForm;
			}
		}

		public static JobMaterialRequisitionForm JobMaterialRequesitionFormObj
		{
			get
			{
				if (jobMaterialRequesitionForm == null || jobMaterialRequesitionForm.IsDisposed)
				{
					jobMaterialRequesitionForm = new JobMaterialRequisitionForm();
				}
				return jobMaterialRequesitionForm;
			}
		}

		public static AppointmentDetailsForm AppointmentDetailsFormObj
		{
			get
			{
				if (appointmentDetailsForm == null || appointmentDetailsForm.IsDisposed)
				{
					appointmentDetailsForm = new AppointmentDetailsForm();
				}
				return appointmentDetailsForm;
			}
		}

		public static MaterialRequisitionReportForm MaterialRequisitionReportFormObj
		{
			get
			{
				if (MaterialRequisitionReportForm == null || MaterialRequisitionReportForm.IsDisposed)
				{
					MaterialRequisitionReportForm = new MaterialRequisitionReportForm();
				}
				return MaterialRequisitionReportForm;
			}
		}

		public static BillOfLadingListForm BillOfLadingFormObj
		{
			get
			{
				if (billOfLadingForm == null || billOfLadingForm.IsDisposed)
				{
					billOfLadingForm = new BillOfLadingListForm();
				}
				return billOfLadingForm;
			}
		}

		public static RequestForCreditLimitReport ReqForCreditLimitReportFormObj
		{
			get
			{
				if (reqForCreditLimitReportForm == null || reqForCreditLimitReportForm.IsDisposed)
				{
					reqForCreditLimitReportForm = new RequestForCreditLimitReport();
				}
				return reqForCreditLimitReportForm;
			}
		}

		public static MaterialRequisitionFlowReport MaterialRequisitionFlowReportFormObj
		{
			get
			{
				if (materialRequisitionFlowReportFormObj == null || materialRequisitionFlowReportFormObj.IsDisposed)
				{
					materialRequisitionFlowReportFormObj = new MaterialRequisitionFlowReport();
				}
				return materialRequisitionFlowReportFormObj;
			}
		}

		public static ChequeMaturityReportForm ChequeMaturityReportFormObj
		{
			get
			{
				if (chequeMaturityReportForm == null || chequeMaturityReportForm.IsDisposed)
				{
					chequeMaturityReportForm = new ChequeMaturityReportForm();
				}
				return chequeMaturityReportForm;
			}
		}

		public static SalesEnquiryReport SalesEnquiryReportFormObj
		{
			get
			{
				if (salesEnquiryReportFormObj == null || salesEnquiryReportFormObj.IsDisposed)
				{
					salesEnquiryReportFormObj = new SalesEnquiryReport();
				}
				return salesEnquiryReportFormObj;
			}
		}

		public static SalesQuoteReport SalesQuoteReportFormObj
		{
			get
			{
				if (salesQuoteReportFormObj == null || salesQuoteReportFormObj.IsDisposed)
				{
					salesQuoteReportFormObj = new SalesQuoteReport();
				}
				return salesQuoteReportFormObj;
			}
		}

		public static SalesOrderReport SalesOrderReportFormObj
		{
			get
			{
				if (salesOrderReportFormObj == null || salesOrderReportFormObj.IsDisposed)
				{
					salesOrderReportFormObj = new SalesOrderReport();
				}
				return salesOrderReportFormObj;
			}
		}

		public static ProformaInvoiceReport ProformaInvoiceReportFormObj
		{
			get
			{
				if (proformaInvoiceReportFormObj == null || proformaInvoiceReportFormObj.IsDisposed)
				{
					proformaInvoiceReportFormObj = new ProformaInvoiceReport();
				}
				return proformaInvoiceReportFormObj;
			}
		}

		public static DeliveryNoteReport DeliveryNoteReportFormObj
		{
			get
			{
				if (deliveryNoteReportFormObj == null || deliveryNoteReportFormObj.IsDisposed)
				{
					deliveryNoteReportFormObj = new DeliveryNoteReport();
				}
				return deliveryNoteReportFormObj;
			}
		}

		public static SalesReceiptReport SalesReceiptReportFormObj
		{
			get
			{
				if (salesReceiptReportFormObj == null || salesReceiptReportFormObj.IsDisposed)
				{
					salesReceiptReportFormObj = new SalesReceiptReport();
				}
				return salesReceiptReportFormObj;
			}
		}

		public static SalesInvoiceReport SalesInvoiceReportFormObj
		{
			get
			{
				if (salesInvoiceReportForm == null || salesInvoiceReportForm.IsDisposed)
				{
					salesInvoiceReportForm = new SalesInvoiceReport();
				}
				return salesInvoiceReportForm;
			}
		}

		public static PurchaseQuoteReport PurchaseQuoteReportFormObj
		{
			get
			{
				if (purchaseQuoteReportFormObj == null || purchaseQuoteReportFormObj.IsDisposed)
				{
					purchaseQuoteReportFormObj = new PurchaseQuoteReport();
				}
				return purchaseQuoteReportFormObj;
			}
		}

		public static PurchaseInvoiceReport PurchaseInvoiceReportFormObj
		{
			get
			{
				if (purchaseInvoiceReportForm == null || purchaseInvoiceReportForm.IsDisposed)
				{
					purchaseInvoiceReportForm = new PurchaseInvoiceReport();
				}
				return purchaseInvoiceReportForm;
			}
		}

		public static PurchaseGRNReport PurchaseGRNReportFormObj
		{
			get
			{
				if (purchaseGRNReportForm == null || purchaseGRNReportForm.IsDisposed)
				{
					purchaseGRNReportForm = new PurchaseGRNReport();
				}
				return purchaseGRNReportForm;
			}
		}

		public static PurchasePackingListReport PurchasePLReportFormObj
		{
			get
			{
				if (purchasePLReportForm == null || purchasePLReportForm.IsDisposed)
				{
					purchasePLReportForm = new PurchasePackingListReport();
				}
				return purchasePLReportForm;
			}
		}

		public static PurchaseOrderReport PurchaseOrderReportFormObj
		{
			get
			{
				if (purchaseOrderReportForm == null || purchaseOrderReportForm.IsDisposed)
				{
					purchaseOrderReportForm = new PurchaseOrderReport();
				}
				return purchaseOrderReportForm;
			}
		}

		public static JobFeeForm JobFeeFormObj
		{
			get
			{
				if (jobFeeForm == null || jobFeeForm.IsDisposed)
				{
					jobFeeForm = new JobFeeForm();
				}
				return jobFeeForm;
			}
		}

		public static ProductBrandDetailsForm ProductBrandDetailsFormObj
		{
			get
			{
				if (productBrandDetailsForm == null || productBrandDetailsForm.IsDisposed)
				{
					productBrandDetailsForm = new ProductBrandDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						productBrandDetailsForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						productBrandDetailsForm.Show();
					}
				}
				return productBrandDetailsForm;
			}
		}

		public static Form BackupDatabaseFormObj
		{
			get
			{
				BackupDatabaseForm backupDatabaseForm = new BackupDatabaseForm();
				if (showForm)
				{
					backupDatabaseForm.ShowDialog();
					return null;
				}
				return backupDatabaseForm;
			}
		}

		public static Form RestoreDatabaseFormObj
		{
			get
			{
				RestoreDatabaseForm restoreDatabaseForm = new RestoreDatabaseForm();
				if (showForm)
				{
					restoreDatabaseForm.ShowDialog();
					return null;
				}
				return restoreDatabaseForm;
			}
		}

		public static ProductCategoryDetailsForm ProductCategoryDetailsFormObj
		{
			get
			{
				if (productCategoryDetailsForm == null || productCategoryDetailsForm.IsDisposed)
				{
					productCategoryDetailsForm = new ProductCategoryDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						productCategoryDetailsForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						productCategoryDetailsForm.Show();
					}
				}
				return productCategoryDetailsForm;
			}
		}

		public static formHome HomeFormObj
		{
			get
			{
				if (homeForm == null || homeForm.IsDisposed)
				{
					homeForm = new formHome(formParent);
				}
				return homeForm;
			}
		}

		public static formPOSHome POSHomeFormObj
		{
			get
			{
				if (posHomeForm == null || posHomeForm.IsDisposed)
				{
					posHomeForm = new formPOSHome(formParent);
				}
				return posHomeForm;
			}
		}

		public static formPOSHome POSHomeFormNonMDIObj
		{
			get
			{
				if (posHomeForm == null || posHomeForm.IsDisposed)
				{
					posHomeForm = new formPOSHome();
				}
				return posHomeForm;
			}
		}

		public static ProductDetailsForm ProductDetailsFormObj
		{
			get
			{
				if (productDetailsForm == null || productDetailsForm.IsDisposed)
				{
					productDetailsForm = new ProductDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						productDetailsForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						productDetailsForm.Show();
					}
				}
				return productDetailsForm;
			}
		}

		public static CustomerDetailsForm CustomerDetailsFormObj
		{
			get
			{
				if (customerDetailsForm == null || customerDetailsForm.IsDisposed)
				{
					customerDetailsForm = new CustomerDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						customerDetailsForm.MdiParent = MainForm;
					}
					customerDetailsForm.Show();
				}
				return customerDetailsForm;
			}
		}

		public static CRMCustomerDetailsForm CRMCustomerDetailsFormObj
		{
			get
			{
				if (crmCustomerDetailsForm == null || crmCustomerDetailsForm.IsDisposed)
				{
					crmCustomerDetailsForm = new CRMCustomerDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						crmCustomerDetailsForm.MdiParent = MainForm;
					}
					crmCustomerDetailsForm.Show();
				}
				return crmCustomerDetailsForm;
			}
		}

		public static VendorDetailsForm VendorDetailsFormObj
		{
			get
			{
				if (vendorDetailsForm == null || vendorDetailsForm.IsDisposed)
				{
					vendorDetailsForm = new VendorDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						vendorDetailsForm.MdiParent = MainForm;
					}
					vendorDetailsForm.Show();
				}
				return vendorDetailsForm;
			}
		}

		public static VendorClassDetailsForm VendorClassDetailsFormObj
		{
			get
			{
				if (vendorClassDetailsForm == null || vendorClassDetailsForm.IsDisposed)
				{
					vendorClassDetailsForm = new VendorClassDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						vendorClassDetailsForm.MdiParent = MainForm;
					}
					vendorClassDetailsForm.Show();
				}
				return vendorClassDetailsForm;
			}
		}

		public static EmployeeDetailsForm EmployeeDetailsFormObj
		{
			get
			{
				try
				{
					if (employeeDetailsForm == null || employeeDetailsForm.IsDisposed)
					{
						employeeDetailsForm = new EmployeeDetailsForm();
						if (UserPreferences.OpenMDI)
						{
							employeeDetailsForm.MdiParent = MainForm;
						}
						if (showForm)
						{
							employeeDetailsForm.Show();
						}
					}
					return employeeDetailsForm;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return null;
				}
			}
		}

		public static ShippingMethodDetailsForm ShippingMethodDetailsFormObj
		{
			get
			{
				if (shipperDetailsForm == null || shipperDetailsForm.IsDisposed)
				{
					shipperDetailsForm = new ShippingMethodDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						shipperDetailsForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						shipperDetailsForm.Show();
					}
				}
				return shipperDetailsForm;
			}
		}

		public static CompanyAccountsListForm CompanyAccountsListFormObj
		{
			get
			{
				if (companyAccountsListForm == null || companyAccountsListForm.IsDisposed)
				{
					companyAccountsListForm = new CompanyAccountsListForm();
					if (UserPreferences.OpenMDI)
					{
						companyAccountsListForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						companyAccountsListForm.Show();
					}
				}
				return companyAccountsListForm;
			}
		}

		public static CompanyAccountDetailsForm CompanyAccountDetailsFormObj
		{
			get
			{
				if (companyAccountDetailsForm == null || companyAccountDetailsForm.IsDisposed)
				{
					companyAccountDetailsForm = new CompanyAccountDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						companyAccountDetailsForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						companyAccountDetailsForm.Show();
					}
				}
				return companyAccountDetailsForm;
			}
		}

		public static BankDetailsForm BankDetailsFormObj
		{
			get
			{
				if (bankDetailsForm == null || bankDetailsForm.IsDisposed)
				{
					bankDetailsForm = new BankDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						bankDetailsForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						bankDetailsForm.Show();
					}
				}
				return bankDetailsForm;
			}
		}

		public static JournalEntryForm JournalEntryFormObj
		{
			get
			{
				if (journalEntryForm == null || journalEntryForm.IsDisposed)
				{
					journalEntryForm = new JournalEntryForm();
					if (UserPreferences.OpenMDI)
					{
						journalEntryForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						journalEntryForm.Show();
					}
				}
				return journalEntryForm;
			}
		}

		public static BudgetingForm BudgetingFormObj
		{
			get
			{
				if (budgetingForm == null || budgetingForm.IsDisposed)
				{
					budgetingForm = new BudgetingForm();
					if (UserPreferences.OpenMDI)
					{
						budgetingForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						budgetingForm.Show();
					}
				}
				return budgetingForm;
			}
		}

		public static VehicleMileageTrackForm VehicleMileageTrackFormObj
		{
			get
			{
				if (vehiclemileagetrackForm == null || vehiclemileagetrackForm.IsDisposed)
				{
					vehiclemileagetrackForm = new VehicleMileageTrackForm();
					if (UserPreferences.OpenMDI)
					{
						vehiclemileagetrackForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						vehiclemileagetrackForm.Show();
					}
				}
				return vehiclemileagetrackForm;
			}
		}

		public static SalesManTargetForm SalesManTargetFormObj
		{
			get
			{
				if (salesmantargetForm == null || salesmantargetForm.IsDisposed)
				{
					salesmantargetForm = new SalesManTargetForm();
					if (UserPreferences.OpenMDI)
					{
						salesmantargetForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						salesmantargetForm.Show();
					}
				}
				return salesmantargetForm;
			}
		}

		public static CustomerInsuranceClaimForm CustomerInsuranceClaimFormObj
		{
			get
			{
				if (customerinsuranceClaimForm == null || customerinsuranceClaimForm.IsDisposed)
				{
					customerinsuranceClaimForm = new CustomerInsuranceClaimForm();
					if (UserPreferences.OpenMDI)
					{
						customerinsuranceClaimForm.MdiParent = MainForm;
					}
					if (showForm)
					{
						customerinsuranceClaimForm.Show();
					}
				}
				return customerinsuranceClaimForm;
			}
		}

		public static CustomerBalanceSummaryReport CustomerBalanceSummaryReportObj
		{
			get
			{
				if (customerBalanceSummaryReport == null || customerBalanceSummaryReport.IsDisposed)
				{
					customerBalanceSummaryReport = new CustomerBalanceSummaryReport();
				}
				return customerBalanceSummaryReport;
			}
		}

		public static CustomerBalanceDetailsReport CustomerBalanceDetailsReportObj
		{
			get
			{
				if (customerBalanceDetailsReport == null || customerBalanceDetailsReport.IsDisposed)
				{
					customerBalanceDetailsReport = new CustomerBalanceDetailsReport();
				}
				return customerBalanceDetailsReport;
			}
		}

		public static VendorBalanceSummaryReport VendorBalanceSummaryReportObj
		{
			get
			{
				if (vendorBalanceSummaryReport == null || vendorBalanceSummaryReport.IsDisposed)
				{
					vendorBalanceSummaryReport = new VendorBalanceSummaryReport();
				}
				return vendorBalanceSummaryReport;
			}
		}

		public static VendorBalanceDetailsReport VendorBalanceDetailsReportObj
		{
			get
			{
				if (vendorBalanceDetailsReport == null || vendorBalanceDetailsReport.IsDisposed)
				{
					vendorBalanceDetailsReport = new VendorBalanceDetailsReport();
				}
				return vendorBalanceDetailsReport;
			}
		}

		public static GLReportForm GLReportObj
		{
			get
			{
				if (glReport == null || glReport.IsDisposed)
				{
					glReport = new GLReportForm();
					if (showForm)
					{
						glReport.Show();
					}
				}
				return glReport;
			}
		}

		public static InventoryAdjustmentsForm InventoryAdjustmentsFormObj
		{
			get
			{
				if (inventoryAdjustmentForm == null || inventoryAdjustmentForm.IsDisposed)
				{
					inventoryAdjustmentForm = new InventoryAdjustmentsForm();
				}
				return inventoryAdjustmentForm;
			}
		}

		public static CompanyInformationForm CompanyInformationFormObj
		{
			get
			{
				if (companyInformationForm == null || companyInformationForm.IsDisposed)
				{
					companyInformationForm = new CompanyInformationForm();
					if (showForm)
					{
						companyInformationForm.Show();
					}
				}
				return companyInformationForm;
			}
		}

		public static AccountGroupDetailsForm AccountGroupDetailsFormObj
		{
			get
			{
				if (accountGroupDetailsForm == null || accountGroupDetailsForm.IsDisposed)
				{
					accountGroupDetailsForm = new AccountGroupDetailsForm();
					if (showForm)
					{
						accountGroupDetailsForm.Show();
					}
				}
				return accountGroupDetailsForm;
			}
		}

		public static AnalysisGroupDetailsForm AnalysisGroupDetailsFormObj
		{
			get
			{
				if (analysisGroupDetailsForm == null || analysisGroupDetailsForm.IsDisposed)
				{
					analysisGroupDetailsForm = new AnalysisGroupDetailsForm();
					if (showForm)
					{
						analysisGroupDetailsForm.Show();
					}
				}
				return analysisGroupDetailsForm;
			}
		}

		public static AnalysisDetailsForm AnalysisDetailsFormObj
		{
			get
			{
				if (analysisDetailsForm == null || analysisDetailsForm.IsDisposed)
				{
					analysisDetailsForm = new AnalysisDetailsForm();
					if (showForm)
					{
						analysisDetailsForm.Show();
					}
				}
				return analysisDetailsForm;
			}
		}

		public static HelpSupportForm HelpSupportFormObj
		{
			get
			{
				if (helpSupportForm == null || helpSupportForm.IsDisposed)
				{
					helpSupportForm = new HelpSupportForm(formParent);
					if (showForm)
					{
						helpSupportForm.Show();
					}
				}
				return helpSupportForm;
			}
		}

		public static ProductManufacturerDetailsForm ProductManufacturerDetailsFormObj
		{
			get
			{
				if (productManufacturerDetailsForm == null || productManufacturerDetailsForm.IsDisposed)
				{
					productManufacturerDetailsForm = new ProductManufacturerDetailsForm();
					if (showForm)
					{
						productManufacturerDetailsForm.Show();
					}
				}
				return productManufacturerDetailsForm;
			}
		}

		public static ProductStyleDetailsForm ProductStyleDetailsFormObj
		{
			get
			{
				if (productStyleDetailsForm == null || productStyleDetailsForm.IsDisposed)
				{
					productStyleDetailsForm = new ProductStyleDetailsForm();
					if (showForm)
					{
						productStyleDetailsForm.Show();
					}
				}
				return productStyleDetailsForm;
			}
		}

		public static ProductSpecificationDetailsForm ProductSpecificationDetailsFormObj
		{
			get
			{
				if (productSpecificationDetailsForm == null || productSpecificationDetailsForm.IsDisposed)
				{
					productSpecificationDetailsForm = new ProductSpecificationDetailsForm();
					if (showForm)
					{
						productSpecificationDetailsForm.Show();
					}
				}
				return productSpecificationDetailsForm;
			}
		}

		public static ProductSizeDetailsForm ProductSizeDetailsFormObj
		{
			get
			{
				if (productSizeDetailsForm == null || productSizeDetailsForm.IsDisposed)
				{
					productSizeDetailsForm = new ProductSizeDetailsForm();
					if (showForm)
					{
						productSizeDetailsForm.Show();
					}
				}
				return productSizeDetailsForm;
			}
		}

		public static ProductAttributeDetailsForm ProductAttributeDetailsFormObj
		{
			get
			{
				if (productAttributeDetailsForm == null || productAttributeDetailsForm.IsDisposed)
				{
					productAttributeDetailsForm = new ProductAttributeDetailsForm();
					if (showForm)
					{
						productAttributeDetailsForm.Show();
					}
				}
				return productAttributeDetailsForm;
			}
		}

		public static CashExpenseEntryForm CashExpenseEntryFormObj
		{
			get
			{
				if (expenseEntryForm == null || expenseEntryForm.IsDisposed)
				{
					expenseEntryForm = new CashExpenseEntryForm();
				}
				return expenseEntryForm;
			}
		}

		public static ContactDetailsForm ContactDetailsFormObj
		{
			get
			{
				if (contactDetailsForm == null || contactDetailsForm.IsDisposed)
				{
					contactDetailsForm = new ContactDetailsForm();
					if (showForm)
					{
						contactDetailsForm.Show();
					}
				}
				return contactDetailsForm;
			}
		}

		public static PrintPreviewForm PrintPreviewFormObj
		{
			get
			{
				if (printPreviewForm == null || printPreviewForm.IsDisposed)
				{
					printPreviewForm = new PrintPreviewForm();
					if (showForm)
					{
						printPreviewForm.Show();
					}
				}
				return printPreviewForm;
			}
		}

		public static PriceLevelDetailsForm PriceLevelDetailsFormObj
		{
			get
			{
				if (priceLevelDetailsForm == null || priceLevelDetailsForm.IsDisposed)
				{
					priceLevelDetailsForm = new PriceLevelDetailsForm();
					if (showForm)
					{
						priceLevelDetailsForm.Show();
					}
				}
				return priceLevelDetailsForm;
			}
		}

		public static TaskStepsForm TaskStepsFormObj
		{
			get
			{
				if (taskStepsForm == null || taskStepsForm.IsDisposed)
				{
					taskStepsForm = new TaskStepsForm();
					if (showForm)
					{
						taskStepsForm.Show();
					}
				}
				return taskStepsForm;
			}
		}

		public static TaskTypeForm TaskTypeFormObj
		{
			get
			{
				if (taskTypeForm == null || taskTypeForm.IsDisposed)
				{
					taskTypeForm = new TaskTypeForm();
					if (showForm)
					{
						taskTypeForm.Show();
					}
				}
				return taskTypeForm;
			}
		}

		public static TaskTransactionForm TaskTransactionFormObj
		{
			get
			{
				if (taskTransactionForm == null || taskTransactionForm.IsDisposed)
				{
					taskTransactionForm = new TaskTransactionForm();
					if (showForm)
					{
						taskTransactionForm.Show();
					}
				}
				return taskTransactionForm;
			}
		}

		public static TaskTransactionStatusForm TaskTransactionStatusFormObj
		{
			get
			{
				if (taskTransactionStatusForm == null || taskTransactionStatusForm.IsDisposed)
				{
					taskTransactionStatusForm = new TaskTransactionStatusForm();
					if (showForm)
					{
						taskTransactionStatusForm.Show();
					}
				}
				return taskTransactionStatusForm;
			}
		}

		public static CountryDetailsForm CountryDetailsFormObj
		{
			get
			{
				if (countryDetailsForm == null || countryDetailsForm.IsDisposed)
				{
					countryDetailsForm = new CountryDetailsForm();
					if (showForm)
					{
						countryDetailsForm.Show();
					}
				}
				return countryDetailsForm;
			}
		}

		public static CustomerClassDetailsForm CustomerClassDetailsFormObj
		{
			get
			{
				if (customerClassDetailsForm == null || customerClassDetailsForm.IsDisposed)
				{
					customerClassDetailsForm = new CustomerClassDetailsForm();
					if (showForm)
					{
						customerClassDetailsForm.Show();
					}
				}
				return customerClassDetailsForm;
			}
		}

		public static AreaDetailsForm AreaDetailsFormObj
		{
			get
			{
				if (areaDetailsForm == null || areaDetailsForm.IsDisposed)
				{
					areaDetailsForm = new AreaDetailsForm();
					if (showForm)
					{
						areaDetailsForm.Show();
					}
				}
				return areaDetailsForm;
			}
		}

		public static PaymentMethodDetailsForm PaymentMethodDetailsFormObj
		{
			get
			{
				if (paymentMethodDetailsForm == null || paymentMethodDetailsForm.IsDisposed)
				{
					paymentMethodDetailsForm = new PaymentMethodDetailsForm();
					if (showForm)
					{
						paymentMethodDetailsForm.Show();
					}
				}
				return paymentMethodDetailsForm;
			}
		}

		public static PaymentTermDetailsForm PaymentTermDetailsFormObj
		{
			get
			{
				if (paymentTermDetailsForm == null || paymentTermDetailsForm.IsDisposed)
				{
					paymentTermDetailsForm = new PaymentTermDetailsForm();
					if (showForm)
					{
						paymentTermDetailsForm.Show();
					}
				}
				return paymentTermDetailsForm;
			}
		}

		public static CustomerAddressDetailsForm CustomerAddressDetailsFormObj
		{
			get
			{
				if (customerAddressDetailsForm == null || customerAddressDetailsForm.IsDisposed)
				{
					customerAddressDetailsForm = new CustomerAddressDetailsForm();
				}
				return customerAddressDetailsForm;
			}
		}

		public static SalespersonDetailsForm SalespersonDetailsFormObj
		{
			get
			{
				if (salespersonDetailsForm == null || salespersonDetailsForm.IsDisposed)
				{
					salespersonDetailsForm = new SalespersonDetailsForm();
					if (showForm)
					{
						salespersonDetailsForm.Show();
					}
				}
				return salespersonDetailsForm;
			}
		}

		public static VendorAddressDetailsForm VendorAddressDetailsFormObj
		{
			get
			{
				if (vendorAddressDetailsForm == null || vendorAddressDetailsForm.IsDisposed)
				{
					vendorAddressDetailsForm = new VendorAddressDetailsForm();
				}
				return vendorAddressDetailsForm;
			}
		}

		public static BuyerDetailsForm BuyerDetailsFormObj
		{
			get
			{
				if (buyerDetailsForm == null || buyerDetailsForm.IsDisposed)
				{
					buyerDetailsForm = new BuyerDetailsForm();
					if (showForm)
					{
						buyerDetailsForm.Show();
					}
				}
				return buyerDetailsForm;
			}
		}

		public static ProductClassDetailsForm ProductClassDetailsFormObj
		{
			get
			{
				if (productClassDetailsForm == null || productClassDetailsForm.IsDisposed)
				{
					productClassDetailsForm = new ProductClassDetailsForm();
					if (showForm)
					{
						productClassDetailsForm.Show();
					}
				}
				return productClassDetailsForm;
			}
		}

		public static UnitDetailsForm UnitDetailsFormObj
		{
			get
			{
				if (unitDetailsForm == null || unitDetailsForm.IsDisposed)
				{
					unitDetailsForm = new UnitDetailsForm();
					if (showForm)
					{
						unitDetailsForm.Show();
					}
				}
				return unitDetailsForm;
			}
		}

		public static SmartListForm SmartListFormObj
		{
			get
			{
				if (smartListForm == null || smartListForm.IsDisposed)
				{
					smartListForm = new SmartListForm();
					if (showForm)
					{
						smartListForm.Show();
					}
				}
				return smartListForm;
			}
		}

		public static ExternalReportForm ExternalReportFormObj
		{
			get
			{
				if (externalReportForm == null || externalReportForm.IsDisposed)
				{
					externalReportForm = new ExternalReportForm();
					if (showForm)
					{
						externalReportForm.Show();
					}
				}
				return externalReportForm;
			}
		}

		public static SalesByGRNReport SalesByGRNReportObj
		{
			get
			{
				if (salesByGRNReport == null || salesByGRNReport.IsDisposed)
				{
					salesByGRNReport = new SalesByGRNReport();
					if (showForm)
					{
						salesByGRNReport.Show();
					}
				}
				return salesByGRNReport;
			}
		}

		public static SalesByGRNSummaryReport SalesByGRNReportSummaryObj
		{
			get
			{
				if (salesByGRNSummaryReport == null || salesByGRNSummaryReport.IsDisposed)
				{
					salesByGRNSummaryReport = new SalesByGRNSummaryReport();
					if (showForm)
					{
						salesByGRNSummaryReport.Show();
					}
				}
				return salesByGRNSummaryReport;
			}
		}

		public static ProjectDueReportForm ProjectDueReportFormObj
		{
			get
			{
				if (projectDueReportForm == null || projectDueReportForm.IsDisposed)
				{
					projectDueReportForm = new ProjectDueReportForm();
					if (showForm)
					{
						projectDueReportForm.Show();
					}
				}
				return projectDueReportForm;
			}
		}

		public static PropertyRegistrationReport PropertyRegistrationReportObj
		{
			get
			{
				if (propertyRegistrationReport == null || propertyRegistrationReport.IsDisposed)
				{
					propertyRegistrationReport = new PropertyRegistrationReport();
					propertyRegistrationReport.TypeID = "1";
					if (showForm)
					{
						propertyRegistrationReport.Show();
					}
				}
				return propertyRegistrationReport;
			}
		}

		public static PropertyRegistrationReport PropertyRenewalReportObj
		{
			get
			{
				if (propertyRenewalReport == null || propertyRenewalReport.IsDisposed)
				{
					propertyRenewalReport = new PropertyRegistrationReport();
					propertyRenewalReport.TypeID = "2";
					if (showForm)
					{
						propertyRenewalReport.Show();
					}
				}
				return propertyRenewalReport;
			}
		}

		public static PropertyRegistrationReport PropertyCancellationReportObj
		{
			get
			{
				if (propertyCancellationReport == null || propertyCancellationReport.IsDisposed)
				{
					propertyCancellationReport = new PropertyRegistrationReport();
					propertyCancellationReport.TypeID = "3";
					if (showForm)
					{
						propertyCancellationReport.Show();
					}
				}
				return propertyCancellationReport;
			}
		}

		public static PropertyAvailabiltyReport PropertyAvailabilityReportObj
		{
			get
			{
				if (propertyAvailabilityReport == null || propertyAvailabilityReport.IsDisposed)
				{
					propertyAvailabilityReport = new PropertyAvailabiltyReport();
					if (showForm)
					{
						propertyAvailabilityReport.Show();
					}
				}
				return propertyAvailabilityReport;
			}
		}

		public static ProjectManPowerReportForm ProjectManPowerReportObj
		{
			get
			{
				if (projectManPowerReport == null || projectManPowerReport.IsDisposed)
				{
					projectManPowerReport = new ProjectManPowerReportForm();
					if (showForm)
					{
						projectManPowerReport.Show();
					}
				}
				return projectManPowerReport;
			}
		}

		public static EmployeeFinalSettlementReport EmployeeFinalSettlementReportObj
		{
			get
			{
				if (employeeFinalSettlementReport == null || employeeFinalSettlementReport.IsDisposed)
				{
					employeeFinalSettlementReport = new EmployeeFinalSettlementReport();
					if (showForm)
					{
						employeeFinalSettlementReport.Show();
					}
				}
				return employeeFinalSettlementReport;
			}
		}

		public static PaymentRequestForm PaymentRequestFormObj
		{
			get
			{
				if (paymentRequestForm == null || paymentRequestForm.IsDisposed)
				{
					paymentRequestForm = new PaymentRequestForm();
					if (showForm)
					{
						paymentRequestForm.Show();
					}
				}
				return paymentRequestForm;
			}
		}

		public static GradeDetailsForm GradeDetailsFormObj
		{
			get
			{
				if (gradeDetailsForm == null || gradeDetailsForm.IsDisposed)
				{
					gradeDetailsForm = new GradeDetailsForm();
					if (showForm)
					{
						gradeDetailsForm.Show();
					}
				}
				return gradeDetailsForm;
			}
		}

		public static SponsorDetailsForm SponsorDetailsFormObj
		{
			get
			{
				if (sponsorDetailsForm == null || sponsorDetailsForm.IsDisposed)
				{
					sponsorDetailsForm = new SponsorDetailsForm();
					if (showForm)
					{
						sponsorDetailsForm.Show();
					}
				}
				return sponsorDetailsForm;
			}
		}

		public static NationalityDetailsForm NationalityDetailsFormObj
		{
			get
			{
				if (nationalityDetailsForm == null || nationalityDetailsForm.IsDisposed)
				{
					nationalityDetailsForm = new NationalityDetailsForm();
					if (showForm)
					{
						nationalityDetailsForm.Show();
					}
				}
				return nationalityDetailsForm;
			}
		}

		public static ReligionDetailsForm ReligionDetailsFormObj
		{
			get
			{
				if (religionDetailsForm == null || religionDetailsForm.IsDisposed)
				{
					religionDetailsForm = new ReligionDetailsForm();
					if (showForm)
					{
						religionDetailsForm.Show();
					}
				}
				return religionDetailsForm;
			}
		}

		public static DivisionDetailsForm DivisionDetailsFormObj
		{
			get
			{
				if (divisionDetailsForm == null || divisionDetailsForm.IsDisposed)
				{
					divisionDetailsForm = new DivisionDetailsForm();
					if (showForm)
					{
						divisionDetailsForm.Show();
					}
				}
				return divisionDetailsForm;
			}
		}

		public static PositionDetailsForm PositionDetailsFormObj
		{
			get
			{
				if (positionDetailsForm == null || positionDetailsForm.IsDisposed)
				{
					positionDetailsForm = new PositionDetailsForm();
					if (showForm)
					{
						positionDetailsForm.Show();
					}
				}
				return positionDetailsForm;
			}
		}

		public static EmployeeDocTypeDetailsForm EmployeeDocTypeDetailsFormObj
		{
			get
			{
				if (employeeDocTypeDetailsForm == null || employeeDocTypeDetailsForm.IsDisposed)
				{
					employeeDocTypeDetailsForm = new EmployeeDocTypeDetailsForm();
					if (showForm)
					{
						employeeDocTypeDetailsForm.Show();
					}
				}
				return employeeDocTypeDetailsForm;
			}
		}

		public static PatientDocTypeDetailsForm PatientDocTypeDetailsFormObj
		{
			get
			{
				if (patientDocTypeDetailsForm == null || patientDocTypeDetailsForm.IsDisposed)
				{
					patientDocTypeDetailsForm = new PatientDocTypeDetailsForm();
					if (showForm)
					{
						patientDocTypeDetailsForm.Show();
					}
				}
				return patientDocTypeDetailsForm;
			}
		}

		public static BillDiscountForm BillDiscountFormObj
		{
			get
			{
				if (billDiscountForm == null || billDiscountForm.IsDisposed)
				{
					billDiscountForm = new BillDiscountForm();
					if (showForm)
					{
						billDiscountForm.Show();
					}
				}
				return billDiscountForm;
			}
		}

		public static VehicleDocTypeDetailsForm VehicleDocTypeDetailsFormObj
		{
			get
			{
				if (vehicleDocTypeDetailsForm == null || vehicleDocTypeDetailsForm.IsDisposed)
				{
					vehicleDocTypeDetailsForm = new VehicleDocTypeDetailsForm();
					if (showForm)
					{
						vehicleDocTypeDetailsForm.Show();
					}
				}
				return vehicleDocTypeDetailsForm;
			}
		}

		public static SMSAlertForm SMSAlertFormObj
		{
			get
			{
				if (smsAlertForm == null || smsAlertForm.IsDisposed)
				{
					smsAlertForm = new SMSAlertForm();
					smsAlertForm.SMSType = "Alert";
					if (showForm)
					{
						smsAlertForm.Show();
					}
				}
				return smsAlertForm;
			}
		}

		public static DegreeDetailsForm DegreeDetailsFormObj
		{
			get
			{
				if (degreeDetailsForm == null || degreeDetailsForm.IsDisposed)
				{
					degreeDetailsForm = new DegreeDetailsForm();
					if (showForm)
					{
						degreeDetailsForm.Show();
					}
				}
				return degreeDetailsForm;
			}
		}

		public static SkillDetailsForm SkillDetailsFormObj
		{
			get
			{
				if (skillDetailsForm == null || skillDetailsForm.IsDisposed)
				{
					skillDetailsForm = new SkillDetailsForm();
					if (showForm)
					{
						skillDetailsForm.Show();
					}
				}
				return skillDetailsForm;
			}
		}

		public static JobTaskDetailsForm JobTaskDetailsFormObj
		{
			get
			{
				if (jobTaskDetailsForm == null || jobTaskDetailsForm.IsDisposed)
				{
					jobTaskDetailsForm = new JobTaskDetailsForm();
					if (showForm)
					{
						jobTaskDetailsForm.Show();
					}
				}
				return jobTaskDetailsForm;
			}
		}

		public static JobTaskGroupDetailsForm JobTaskGroupDetailsFormObj
		{
			get
			{
				if (jobTaskGroupDetailsForm == null || jobTaskGroupDetailsForm.IsDisposed)
				{
					jobTaskGroupDetailsForm = new JobTaskGroupDetailsForm();
					if (showForm)
					{
						jobTaskGroupDetailsForm.Show();
					}
				}
				return jobTaskGroupDetailsForm;
			}
		}

		public static DepartmentDetailsForm DepartmentDetailsFormObj
		{
			get
			{
				if (departmentDetailsForm == null || departmentDetailsForm.IsDisposed)
				{
					departmentDetailsForm = new DepartmentDetailsForm();
					if (showForm)
					{
						departmentDetailsForm.Show();
					}
				}
				return departmentDetailsForm;
			}
		}

		public static CustomerGroupDetailsForm CustomerGroupDetailsFormObj
		{
			get
			{
				if (customerGroupDetailsForm == null || customerGroupDetailsForm.IsDisposed)
				{
					customerGroupDetailsForm = new CustomerGroupDetailsForm();
					if (showForm)
					{
						customerGroupDetailsForm.Show();
					}
				}
				return customerGroupDetailsForm;
			}
		}

		public static VendorGroupDetailsForm VendorGroupDetailsFormObj
		{
			get
			{
				if (vendorGroupDetailsForm == null || vendorGroupDetailsForm.IsDisposed)
				{
					vendorGroupDetailsForm = new VendorGroupDetailsForm();
					if (showForm)
					{
						vendorGroupDetailsForm.Show();
					}
				}
				return vendorGroupDetailsForm;
			}
		}

		public static EmployeeGroupDetailsForm EmployeeGroupDetailsFormObj
		{
			get
			{
				if (employeeGroupDetailsForm == null || employeeGroupDetailsForm.IsDisposed)
				{
					employeeGroupDetailsForm = new EmployeeGroupDetailsForm();
					if (showForm)
					{
						employeeGroupDetailsForm.Show();
					}
				}
				return employeeGroupDetailsForm;
			}
		}

		public static EmployeeTypeDetailsForm EmployeeTypeDetailsFormObj
		{
			get
			{
				if (employeeTypeDetailsForm == null || employeeTypeDetailsForm.IsDisposed)
				{
					employeeTypeDetailsForm = new EmployeeTypeDetailsForm();
					if (showForm)
					{
						employeeTypeDetailsForm.Show();
					}
				}
				return employeeTypeDetailsForm;
			}
		}

		public static EmployeeAddressDetailsForm EmployeeAddressDetailsFormObj
		{
			get
			{
				if (employeeAddressDetailsForm == null || employeeAddressDetailsForm.IsDisposed)
				{
					employeeAddressDetailsForm = new EmployeeAddressDetailsForm();
				}
				return employeeAddressDetailsForm;
			}
		}

		public static EmployeeEOSSettlementForm EmployeeEOSSettlementFormObj
		{
			get
			{
				if (employeeEOSSettlementForm == null || employeeEOSSettlementForm.IsDisposed)
				{
					employeeEOSSettlementForm = new EmployeeEOSSettlementForm();
				}
				return employeeEOSSettlementForm;
			}
		}

		public static EmployeeEOSForm EmployeeEOSFormObj
		{
			get
			{
				if (employeeEOSForm == null || employeeEOSForm.IsDisposed)
				{
					employeeEOSForm = new EmployeeEOSForm();
				}
				return employeeEOSForm;
			}
		}

		public static LocationDetailsForm LocationDetailsFormObj
		{
			get
			{
				if (locationDetailsForm == null || locationDetailsForm.IsDisposed)
				{
					locationDetailsForm = new LocationDetailsForm();
				}
				return locationDetailsForm;
			}
		}

		public static WorkLocationDetailsForm WorkLocationDetailsFormObj
		{
			get
			{
				if (workLocationDetailsForm == null || workLocationDetailsForm.IsDisposed)
				{
					workLocationDetailsForm = new WorkLocationDetailsForm();
				}
				return workLocationDetailsForm;
			}
		}

		public static ProvisionTypeDetailsForm ProvisionTypeDetailsFormObj
		{
			get
			{
				if (provisionTypeDetailsForm == null || provisionTypeDetailsForm.IsDisposed)
				{
					provisionTypeDetailsForm = new ProvisionTypeDetailsForm();
				}
				return provisionTypeDetailsForm;
			}
		}

		public static EmployeeProvisionEntryForm EmployeeProvisionFormObj
		{
			get
			{
				if (employeeProvisionForm == null || employeeProvisionForm.IsDisposed)
				{
					employeeProvisionForm = new EmployeeProvisionEntryForm();
					employeeProvisionForm.Text = "Employee Provision";
				}
				return employeeProvisionForm;
			}
		}

		public static CompanyDivisionDetailsForm CompanyDivisionDetailsFormObj
		{
			get
			{
				if (companyDivisionDetailsForm == null || companyDivisionDetailsForm.IsDisposed)
				{
					companyDivisionDetailsForm = new CompanyDivisionDetailsForm();
					companyDivisionDetailsForm.Text = "Company Division";
				}
				return companyDivisionDetailsForm;
			}
		}

		public static PrintTemplateMappingForm PrintTemplateMappingFormObj
		{
			get
			{
				if (printTemplateMappingForm == null || printTemplateMappingForm.IsDisposed)
				{
					printTemplateMappingForm = new PrintTemplateMappingForm();
					printTemplateMappingForm.Text = "Print Template Mapping";
				}
				return printTemplateMappingForm;
			}
		}

		public static SalesReport SalesByItemCustomerSalespersonGrpByReportObj
		{
			get
			{
				if (salesByItemCustomerSalespersonGrpByReportform == null || salesByItemCustomerSalespersonGrpByReportform.IsDisposed)
				{
					salesByItemCustomerSalespersonGrpByReportform = new SalesReport();
					salesByItemCustomerSalespersonGrpByReportform.Text = "Sales Report";
				}
				return salesByItemCustomerSalespersonGrpByReportform;
			}
		}

		public static ConsignLocationDetailsForm ConsignLocationDetailsFormObj
		{
			get
			{
				if (consignLocationDetailsForm == null || consignLocationDetailsForm.IsDisposed)
				{
					consignLocationDetailsForm = new ConsignLocationDetailsForm();
				}
				return consignLocationDetailsForm;
			}
		}

		public static ProductDataDetailsForm ProductDataDetailsFormObj
		{
			get
			{
				if (productDataDetailsForm == null || productDataDetailsForm.IsDisposed)
				{
					productDataDetailsForm = new ProductDataDetailsForm();
				}
				return productDataDetailsForm;
			}
		}

		public static ConsignOutSettlementForm ConsignOutSettlementFormObj
		{
			get
			{
				if (consignOutSettlementForm == null || consignOutSettlementForm.IsDisposed)
				{
					consignOutSettlementForm = new ConsignOutSettlementForm();
				}
				return consignOutSettlementForm;
			}
		}

		public static ConsignInSettlementForm ConsignInSettlementFormObj
		{
			get
			{
				if (consignInSettlementForm == null || consignInSettlementForm.IsDisposed)
				{
					consignInSettlementForm = new ConsignInSettlementForm();
				}
				return consignInSettlementForm;
			}
		}

		public static ConsignInClosingForm ConsignInClosingFormObj
		{
			get
			{
				if (consignInClosingForm == null || consignInClosingForm.IsDisposed)
				{
					consignInClosingForm = new ConsignInClosingForm();
				}
				return consignInClosingForm;
			}
		}

		public static PendingConsignInReport PendingConsignInReportObj
		{
			get
			{
				if (pendingConsignInReport == null || pendingConsignInReport.IsDisposed)
				{
					pendingConsignInReport = new PendingConsignInReport();
				}
				return pendingConsignInReport;
			}
		}

		public static PendingConsignOutReport PendingConsignOutReportObj
		{
			get
			{
				if (pendingConsignOutReport == null || pendingConsignOutReport.IsDisposed)
				{
					pendingConsignOutReport = new PendingConsignOutReport();
				}
				return pendingConsignOutReport;
			}
		}

		public static FixedAssetDetailsForm FixedAssetDetailsFormObj
		{
			get
			{
				if (fixedAssetDetailsForm == null || fixedAssetDetailsForm.IsDisposed)
				{
					fixedAssetDetailsForm = new FixedAssetDetailsForm();
				}
				return fixedAssetDetailsForm;
			}
		}

		public static FixedAssetGroupDetailsForm FixedAssetGroupDetailsFormObj
		{
			get
			{
				if (fixedAssetGroupDetailsForm == null || fixedAssetGroupDetailsForm.IsDisposed)
				{
					fixedAssetGroupDetailsForm = new FixedAssetGroupDetailsForm();
				}
				return fixedAssetGroupDetailsForm;
			}
		}

		public static FixedAssetLocationDetailsForm FixedAssetLocationDetailsFormObj
		{
			get
			{
				if (fixedAssetLocationDetailsForm == null || fixedAssetLocationDetailsForm.IsDisposed)
				{
					fixedAssetLocationDetailsForm = new FixedAssetLocationDetailsForm();
				}
				return fixedAssetLocationDetailsForm;
			}
		}

		public static FixedAssetClassDetailsForm FixedAssetClassDetailsFormObj
		{
			get
			{
				if (fixedAssetClassDetailsForm == null || fixedAssetClassDetailsForm.IsDisposed)
				{
					fixedAssetClassDetailsForm = new FixedAssetClassDetailsForm();
				}
				return fixedAssetClassDetailsForm;
			}
		}

		public static FixedAssetPurchaseForm FixedAssetPurchaseFormObj
		{
			get
			{
				if (fixedAssetPurchaseForm == null || fixedAssetPurchaseForm.IsDisposed)
				{
					fixedAssetPurchaseForm = new FixedAssetPurchaseForm();
				}
				return fixedAssetPurchaseForm;
			}
		}

		public static FixedAssetSaleForm FixedAssetSaleFormObj
		{
			get
			{
				if (fixedAssetSaleForm == null || fixedAssetSaleForm.IsDisposed)
				{
					fixedAssetSaleForm = new FixedAssetSaleForm();
				}
				return fixedAssetSaleForm;
			}
		}

		public static FixedAssetTransferForm FixedAssetTransferFormObj
		{
			get
			{
				if (fixedAssetTransferForm == null || fixedAssetTransferForm.IsDisposed)
				{
					fixedAssetTransferForm = new FixedAssetTransferForm();
				}
				return fixedAssetTransferForm;
			}
		}

		public static FixedAssetDepForm FixedAssetDepFormObj
		{
			get
			{
				if (fixedAssetDepForm == null || fixedAssetDepForm.IsDisposed)
				{
					fixedAssetDepForm = new FixedAssetDepForm();
				}
				return fixedAssetDepForm;
			}
		}

		public static POSCashRegisterDetailsForm POSCashRegisterDetailsFormObj
		{
			get
			{
				if (posCashRegisterDetailsForm == null || posCashRegisterDetailsForm.IsDisposed)
				{
					posCashRegisterDetailsForm = new POSCashRegisterDetailsForm();
				}
				return posCashRegisterDetailsForm;
			}
		}

		public static POSLocationDetailsForm POSLocationDetailsFormObj
		{
			get
			{
				if (posLocationDetailsForm == null || posLocationDetailsForm.IsDisposed)
				{
					posLocationDetailsForm = new POSLocationDetailsForm();
				}
				return posLocationDetailsForm;
			}
		}

		public static DimensionDetailsForm DimensionDetailsFormObj
		{
			get
			{
				if (dimensionDetailsForm == null || dimensionDetailsForm.IsDisposed)
				{
					dimensionDetailsForm = new DimensionDetailsForm();
				}
				return dimensionDetailsForm;
			}
		}

		public static MatrixTemplateDetailsForm MatrixTemplateDetailsFormObj
		{
			get
			{
				if (matrixTemplateDetailsForm == null || matrixTemplateDetailsForm.IsDisposed)
				{
					matrixTemplateDetailsForm = new MatrixTemplateDetailsForm();
				}
				return matrixTemplateDetailsForm;
			}
		}

		public static MatrixProductDetailsForm MatrixProductDetailsFormObj
		{
			get
			{
				if (matrixProductDetailsForm == null || matrixProductDetailsForm.IsDisposed)
				{
					matrixProductDetailsForm = new MatrixProductDetailsForm();
				}
				return matrixProductDetailsForm;
			}
		}

		public static POSCashRegisterPaymentMethodsForm POSCashRegisterPaymentMethodsFormObj
		{
			get
			{
				if (posCashRegisterPaymentMethodsForm == null || posCashRegisterPaymentMethodsForm.IsDisposed)
				{
					posCashRegisterPaymentMethodsForm = new POSCashRegisterPaymentMethodsForm();
				}
				return posCashRegisterPaymentMethodsForm;
			}
		}

		public static POSCashRegisterExpenseAccountsForm POSCashRegisterExpenseAccountsFormObj
		{
			get
			{
				if (posCashRegisterExpenseAccountsForm == null || posCashRegisterExpenseAccountsForm.IsDisposed)
				{
					posCashRegisterExpenseAccountsForm = new POSCashRegisterExpenseAccountsForm();
				}
				return posCashRegisterExpenseAccountsForm;
			}
		}

		public static EmployeeDependentDetailsForm EmployeeDependentDetailsFormObj
		{
			get
			{
				if (employeeDependentDetailsForm == null || employeeDependentDetailsForm.IsDisposed)
				{
					employeeDependentDetailsForm = new EmployeeDependentDetailsForm();
				}
				return employeeDependentDetailsForm;
			}
		}

		public static EmployeeDocumentsForm EmployeeDocumentsFormObj
		{
			get
			{
				if (employeeDocumentsForm == null || employeeDocumentsForm.IsDisposed)
				{
					employeeDocumentsForm = new EmployeeDocumentsForm();
				}
				return employeeDocumentsForm;
			}
		}

		public static PatientDocumentsForm PatientDocumentsFormObj
		{
			get
			{
				if (patientDocumentsForm == null || patientDocumentsForm.IsDisposed)
				{
					patientDocumentsForm = new PatientDocumentsForm();
				}
				return patientDocumentsForm;
			}
		}

		public static PropertyDocumentsForm PropertyDocumentsFormObj
		{
			get
			{
				if (propertyDocumentsForm == null || propertyDocumentsForm.IsDisposed)
				{
					propertyDocumentsForm = new PropertyDocumentsForm();
				}
				return propertyDocumentsForm;
			}
		}

		public static TenantDocumentsForm PropertyTenantDocumentsFormObj
		{
			get
			{
				if (propertyTenantDocumentsForm == null || propertyTenantDocumentsForm.IsDisposed)
				{
					propertyTenantDocumentsForm = new TenantDocumentsForm();
				}
				return propertyTenantDocumentsForm;
			}
		}

		public static EmployeeSkillsForm EmployeeSkillsFormObj
		{
			get
			{
				if (employeeSkillsForm == null || employeeSkillsForm.IsDisposed)
				{
					employeeSkillsForm = new EmployeeSkillsForm();
				}
				return employeeSkillsForm;
			}
		}

		public static VehicleDocumentsForm VehicleDocumentsFormObj
		{
			get
			{
				if (vehicleDocumentsForm == null || vehicleDocumentsForm.IsDisposed)
				{
					vehicleDocumentsForm = new VehicleDocumentsForm();
				}
				return vehicleDocumentsForm;
			}
		}

		public static LeaveTypeDetailsForm LeaveTypeDetailsFormObj
		{
			get
			{
				if (leaveTypeDetailsForm == null || leaveTypeDetailsForm.IsDisposed)
				{
					leaveTypeDetailsForm = new LeaveTypeDetailsForm();
				}
				return leaveTypeDetailsForm;
			}
		}

		public static JobTypeDetailsForm JobTypeDetailsFormObj
		{
			get
			{
				if (jobTypeDetailsForm == null || jobTypeDetailsForm.IsDisposed)
				{
					jobTypeDetailsForm = new JobTypeDetailsForm();
				}
				return jobTypeDetailsForm;
			}
		}

		public static CostCategoryDetailsForm CostCategoryDetailsFormObj
		{
			get
			{
				if (costCategoryDetailsForm == null || costCategoryDetailsForm.IsDisposed)
				{
					costCategoryDetailsForm = new CostCategoryDetailsForm();
				}
				return costCategoryDetailsForm;
			}
		}

		public static PayrollItemDetailsForm PayrollItemDetailsFormObj
		{
			get
			{
				if (payrollItemDetailsForm == null || payrollItemDetailsForm.IsDisposed)
				{
					payrollItemDetailsForm = new PayrollItemDetailsForm();
					payrollItemDetailsForm.IsDeduction = false;
				}
				return payrollItemDetailsForm;
			}
		}

		public static PayrollItemDetailsForm DeductionDetailsFormObj
		{
			get
			{
				if (deductionDetailsForm == null || deductionDetailsForm.IsDisposed)
				{
					deductionDetailsForm = new PayrollItemDetailsForm();
					deductionDetailsForm.IsDeduction = true;
				}
				return deductionDetailsForm;
			}
		}

		public static BenefitDetailsForm BenefitDetailsFormObj
		{
			get
			{
				if (benefitDetailsForm == null || benefitDetailsForm.IsDisposed)
				{
					benefitDetailsForm = new BenefitDetailsForm();
				}
				return benefitDetailsForm;
			}
		}

		public static EmployeeSalaryDetailsForm EmployeeSalaryDetailsFormObj
		{
			get
			{
				if (employeeSalaryDetailsForm == null || employeeSalaryDetailsForm.IsDisposed)
				{
					employeeSalaryDetailsForm = new EmployeeSalaryDetailsForm();
				}
				return employeeSalaryDetailsForm;
			}
		}

		public static DestinationDetailsForm DestinationDetailsFormObj
		{
			get
			{
				if (destinationDetailsForm == null || destinationDetailsForm.IsDisposed)
				{
					destinationDetailsForm = new DestinationDetailsForm();
				}
				return destinationDetailsForm;
			}
		}

		public static EmployeeLeaveDetailForm EmployeeLeaveDetailFormObj
		{
			get
			{
				if (employeeLeaveDetailForm == null || employeeLeaveDetailForm.IsDisposed)
				{
					employeeLeaveDetailForm = new EmployeeLeaveDetailForm();
				}
				return employeeLeaveDetailForm;
			}
		}

		public static CompanyDocTypeDetailsForm CompanyDocTypeDetailsFormObj
		{
			get
			{
				if (companyDocTypeDetailsForm == null || companyDocTypeDetailsForm.IsDisposed)
				{
					companyDocTypeDetailsForm = new CompanyDocTypeDetailsForm();
				}
				return companyDocTypeDetailsForm;
			}
		}

		public static CompanyDocumentsForm CompanyDocumentsFormObj
		{
			get
			{
				if (companyDocumentsForm == null || companyDocumentsForm.IsDisposed)
				{
					companyDocumentsForm = new CompanyDocumentsForm();
				}
				return companyDocumentsForm;
			}
		}

		public static TenancyContractDetailsForm TenancyContractDetailsFormObj
		{
			get
			{
				if (tenancyContractDetailsForm == null || tenancyContractDetailsForm.IsDisposed)
				{
					tenancyContractDetailsForm = new TenancyContractDetailsForm();
				}
				return tenancyContractDetailsForm;
			}
		}

		public static TradeLicenseDetailsForm TradeLicenseDetailsFormObj
		{
			get
			{
				if (tradeLicenseDetailsForm == null || tradeLicenseDetailsForm.IsDisposed)
				{
					tradeLicenseDetailsForm = new TradeLicenseDetailsForm();
				}
				return tradeLicenseDetailsForm;
			}
		}

		public static VisaDetailsForm VisaDetailsFormObj
		{
			get
			{
				if (visaDetailsForm == null || visaDetailsForm.IsDisposed)
				{
					visaDetailsForm = new VisaDetailsForm();
				}
				return visaDetailsForm;
			}
		}

		public static AccountAnalysisDetailsForm AccountAnalysisDetailsFormObj
		{
			get
			{
				if (accountAnalysisDetailsForm == null || accountAnalysisDetailsForm.IsDisposed)
				{
					accountAnalysisDetailsForm = new AccountAnalysisDetailsForm();
				}
				return accountAnalysisDetailsForm;
			}
		}

		public static CurrencyDetailsForm CurrencyDetailsFormObj
		{
			get
			{
				if (currencyDetailsForm == null || currencyDetailsForm.IsDisposed)
				{
					currencyDetailsForm = new CurrencyDetailsForm();
				}
				return currencyDetailsForm;
			}
		}

		public static CostCenterDetailsForm CostCenterDetailsFormObj
		{
			get
			{
				if (costCenterDetailsForm == null || costCenterDetailsForm.IsDisposed)
				{
					costCenterDetailsForm = new CostCenterDetailsForm();
				}
				return costCenterDetailsForm;
			}
		}

		public static ChequeReceiptForm ChequeReceiptFormObj
		{
			get
			{
				if (chequeReceiptForm == null || chequeReceiptForm.IsDisposed)
				{
					chequeReceiptForm = new ChequeReceiptForm();
				}
				return chequeReceiptForm;
			}
		}

		public static SysDocDetailsForm SysDocDetailsFormObj
		{
			get
			{
				if (sysDocDetailsForm == null || sysDocDetailsForm.IsDisposed)
				{
					sysDocDetailsForm = new SysDocDetailsForm();
				}
				return sysDocDetailsForm;
			}
		}

		public static RegisterDetailsForm RegisterDetailsFormObj
		{
			get
			{
				if (registerDetailsForm == null || registerDetailsForm.IsDisposed)
				{
					registerDetailsForm = new RegisterDetailsForm();
				}
				return registerDetailsForm;
			}
		}

		public static CashReceiptForm CashReceiptFormObj
		{
			get
			{
				if (cashReceiptForm == null || cashReceiptForm.IsDisposed)
				{
					cashReceiptForm = new CashReceiptForm();
				}
				return cashReceiptForm;
			}
		}

		public static CashReceiptMultiPayeeForm CashReceiptMultiPayeeFormObj
		{
			get
			{
				if (cashReceiptMultiPayeeForm == null || cashReceiptMultiPayeeForm.IsDisposed)
				{
					cashReceiptMultiPayeeForm = new CashReceiptMultiPayeeForm();
				}
				return cashReceiptMultiPayeeForm;
			}
		}

		public static CashPaymentParentForm CashPaymentParentFormObj
		{
			get
			{
				if (cashPaymentParentForm == null || cashPaymentParentForm.IsDisposed)
				{
					cashPaymentParentForm = new CashPaymentParentForm();
				}
				return cashPaymentParentForm;
			}
		}

		public static ChequebookDetailsForm ChequebookDetailsFormObj
		{
			get
			{
				if (chequebookDetailsForm == null || chequebookDetailsForm.IsDisposed)
				{
					chequebookDetailsForm = new ChequebookDetailsForm();
				}
				return chequebookDetailsForm;
			}
		}

		public static CashPaymentForm CashPaymentFormObj
		{
			get
			{
				if (cashPaymentForm == null || cashPaymentForm.IsDisposed)
				{
					cashPaymentForm = new CashPaymentForm();
				}
				return cashPaymentForm;
			}
		}

		public static ChequePaymentParentForm ChequePaymentFormObj
		{
			get
			{
				if (chequePaymentForm == null || chequePaymentForm.IsDisposed)
				{
					chequePaymentForm = new ChequePaymentParentForm();
				}
				return chequePaymentForm;
			}
		}

		public static JournalReportForm JournalReportFormObj
		{
			get
			{
				if (journalReportForm == null || journalReportForm.IsDisposed)
				{
					journalReportForm = new JournalReportForm();
				}
				return journalReportForm;
			}
		}

		public static FundTransferForm FundTransferFormObj
		{
			get
			{
				if (fundTransferForm == null || fundTransferForm.IsDisposed)
				{
					fundTransferForm = new FundTransferForm();
				}
				return fundTransferForm;
			}
		}

		public static ChequeDepositForm ChequeDepositFormObj
		{
			get
			{
				if (chequeDepositForm == null || chequeDepositForm.IsDisposed)
				{
					chequeDepositForm = new ChequeDepositForm();
				}
				return chequeDepositForm;
			}
		}

		public static ChequeExpenseEntryForm ChequeExpenseEntryFormObj
		{
			get
			{
				if (chequeExpenseEntryForm == null || chequeExpenseEntryForm.IsDisposed)
				{
					chequeExpenseEntryForm = new ChequeExpenseEntryForm();
				}
				return chequeExpenseEntryForm;
			}
		}

		public static DebitNoteEntryForm DebitNoteEntryFormObj
		{
			get
			{
				if (debitNoteEntryForm == null || debitNoteEntryForm.IsDisposed)
				{
					debitNoteEntryForm = new DebitNoteEntryForm();
				}
				return debitNoteEntryForm;
			}
		}

		public static CreditNoteEntryForm CreditNoteEntryFormObj
		{
			get
			{
				if (creditNoteEntryForm == null || creditNoteEntryForm.IsDisposed)
				{
					creditNoteEntryForm = new CreditNoteEntryForm();
				}
				return creditNoteEntryForm;
			}
		}

		public static ReturnedChequeReasonDetailsForm ReturnedChequeReasonDetailsFormObj
		{
			get
			{
				if (returnedChequeReasonDetailsForm == null || returnedChequeReasonDetailsForm.IsDisposed)
				{
					returnedChequeReasonDetailsForm = new ReturnedChequeReasonDetailsForm();
				}
				return returnedChequeReasonDetailsForm;
			}
		}

		public static ChequeReturnForm ChequeReturnFormObj
		{
			get
			{
				if (chequeReturnForm == null || chequeReturnForm.IsDisposed)
				{
					chequeReturnForm = new ChequeReturnForm();
				}
				return chequeReturnForm;
			}
		}

		public static ReceivedChequeCancellationForm ReceivedChequeCancellationFormObj
		{
			get
			{
				if (receivedChequeCancellationForm == null || receivedChequeCancellationForm.IsDisposed)
				{
					receivedChequeCancellationForm = new ReceivedChequeCancellationForm();
				}
				return receivedChequeCancellationForm;
			}
		}

		public static ReceivedChequeClearanceForm ReceivedChequeClearanceFormObj
		{
			get
			{
				if (receivedChequeClearanceForm == null || receivedChequeClearanceForm.IsDisposed)
				{
					receivedChequeClearanceForm = new ReceivedChequeClearanceForm();
				}
				return receivedChequeClearanceForm;
			}
		}

		public static IssuedChequeClearanceForm IssuedChequeClearanceFormObj
		{
			get
			{
				if (issuedChequeClearanceForm == null || issuedChequeClearanceForm.IsDisposed)
				{
					issuedChequeClearanceForm = new IssuedChequeClearanceForm();
				}
				return issuedChequeClearanceForm;
			}
		}

		public static IssuedChequeCancellationForm IssuedChequeCancellationFormObj
		{
			get
			{
				if (issuedChequeCancellationForm == null || issuedChequeCancellationForm.IsDisposed)
				{
					issuedChequeCancellationForm = new IssuedChequeCancellationForm();
				}
				return issuedChequeCancellationForm;
			}
		}

		public static IssuedChequeReturnForm IssuedChequeReturnFormObj
		{
			get
			{
				if (issuedChequeReturnForm == null || issuedChequeReturnForm.IsDisposed)
				{
					issuedChequeReturnForm = new IssuedChequeReturnForm();
				}
				return issuedChequeReturnForm;
			}
		}

		public static VoidBlankChequeForm VoidBlankChequeFormObj
		{
			get
			{
				if (voidBlankChequeForm == null || voidBlankChequeForm.IsDisposed)
				{
					voidBlankChequeForm = new VoidBlankChequeForm();
				}
				return voidBlankChequeForm;
			}
		}

		public static SecurityChequeForm SecurityChequeFormObj
		{
			get
			{
				if (securityChequeForm == null || securityChequeForm.IsDisposed)
				{
					securityChequeForm = new SecurityChequeForm();
				}
				return securityChequeForm;
			}
		}

		public static ChequeStatusForm ChequeStatusFormObj
		{
			get
			{
				if (chequeStatusForm == null || chequeStatusForm.IsDisposed)
				{
					chequeStatusForm = new ChequeStatusForm();
				}
				return chequeStatusForm;
			}
		}

		public static GLReportForm GLReportFormObj
		{
			get
			{
				if (glReportForm == null || glReportForm.IsDisposed)
				{
					glReportForm = new GLReportForm();
				}
				return glReportForm;
			}
		}

		public static AdjustmentTypeDetailsForm AdjustmentTypeDetailsFormObj
		{
			get
			{
				if (adjustmentTypeDetailsForm == null || adjustmentTypeDetailsForm.IsDisposed)
				{
					adjustmentTypeDetailsForm = new AdjustmentTypeDetailsForm();
				}
				return adjustmentTypeDetailsForm;
			}
		}

		public static InventoryTransferForm InventoryTransferFormObj
		{
			get
			{
				if (inventoryTransferForm == null || inventoryTransferForm.IsDisposed)
				{
					inventoryTransferForm = new InventoryTransferForm();
				}
				return inventoryTransferForm;
			}
		}

		public static DriverDetailsForm DriverDetailsFormObj
		{
			get
			{
				if (driverDetailsForm == null || driverDetailsForm.IsDisposed)
				{
					driverDetailsForm = new DriverDetailsForm();
				}
				return driverDetailsForm;
			}
		}

		public static ProductQuantityForm ProductQuantityFormObj
		{
			get
			{
				if (productQuantityForm == null || productQuantityForm.IsDisposed)
				{
					productQuantityForm = new ProductQuantityForm();
				}
				return productQuantityForm;
			}
		}

		public static LeaveAvailabilityForm LeaveAvailabilityFormObj
		{
			get
			{
				if (leaveAvailabilityForm == null || leaveAvailabilityForm.IsDisposed)
				{
					leaveAvailabilityForm = new LeaveAvailabilityForm();
				}
				return leaveAvailabilityForm;
			}
		}

		public static CustomerInsuranceDetailsForm CustomerInsuranceDetailsFormObj
		{
			get
			{
				if (customerInsuranceDetailsForm == null || customerInsuranceDetailsForm.IsDisposed)
				{
					customerInsuranceDetailsForm = new CustomerInsuranceDetailsForm();
				}
				return customerInsuranceDetailsForm;
			}
		}

		public static CreditlimitReviewForm CreditLimitReviewFormObj
		{
			get
			{
				if (creditLimitReviewForm == null || creditLimitReviewForm.IsDisposed)
				{
					creditLimitReviewForm = new CreditlimitReviewForm();
				}
				return creditLimitReviewForm;
			}
		}

		public static InventoryTransferAcceptanceForm InventoryTransferAcceptanceFormObj
		{
			get
			{
				if (inventoryTransferAcceptanceForm == null || inventoryTransferAcceptanceForm.IsDisposed)
				{
					inventoryTransferAcceptanceForm = new InventoryTransferAcceptanceForm();
				}
				return inventoryTransferAcceptanceForm;
			}
		}

		public static InventoryTransferReturnForm InventoryTransferReturnFormObj
		{
			get
			{
				if (inventoryTransferReturnForm == null || inventoryTransferReturnForm.IsDisposed)
				{
					inventoryTransferReturnForm = new InventoryTransferReturnForm();
				}
				return inventoryTransferReturnForm;
			}
		}

		public static JobMaterialEstimateForm JobMaterialEstimateFormObj
		{
			get
			{
				if (jobMaterialEstimateForm == null || jobMaterialEstimateForm.IsDisposed)
				{
					jobMaterialEstimateForm = new JobMaterialEstimateForm();
				}
				return jobMaterialEstimateForm;
			}
		}

		public static JobManHrsBudgetingForm JobManHrsBudgetingFormObj
		{
			get
			{
				if (jobManHrsBudgetingForm == null || jobManHrsBudgetingForm.IsDisposed)
				{
					jobManHrsBudgetingForm = new JobManHrsBudgetingForm();
				}
				return jobManHrsBudgetingForm;
			}
		}

		public static ProductListForm ProductListFormObj
		{
			get
			{
				if (productListForm == null || productListForm.IsDisposed)
				{
					productListForm = new ProductListForm();
				}
				return productListForm;
			}
		}

		public static DirectInventoryTransferForm DirectInventoryTransferFormObj
		{
			get
			{
				if (directInventoryTransferForm == null || directInventoryTransferForm.IsDisposed)
				{
					directInventoryTransferForm = new DirectInventoryTransferForm();
				}
				return directInventoryTransferForm;
			}
		}

		public static SalesQuoteForm SalesQuoteFormObj
		{
			get
			{
				if (salesQuoteForm == null || salesQuoteForm.IsDisposed)
				{
					salesQuoteForm = new SalesQuoteForm();
				}
				return salesQuoteForm;
			}
		}

		public static EntityCategoryDetailsForm CustomerCategoryFormObj
		{
			get
			{
				if (customerCategoryForm == null || customerCategoryForm.IsDisposed)
				{
					customerCategoryForm = new EntityCategoryDetailsForm();
					customerCategoryForm.EntityType = EntityTypesEnum.Customers;
				}
				return customerCategoryForm;
			}
		}

		public static EntityCategoryDetailsForm ContactsCategoryFormObj
		{
			get
			{
				if (contactsCategoryForm == null || contactsCategoryForm.IsDisposed)
				{
					contactsCategoryForm = new EntityCategoryDetailsForm();
					contactsCategoryForm.EntityType = EntityTypesEnum.Contacts;
				}
				return contactsCategoryForm;
			}
		}

		public static CRMCategoryDetailsForm LeadCategoryFormObj
		{
			get
			{
				if (leadCategoryForm == null || leadCategoryForm.IsDisposed)
				{
					leadCategoryForm = new CRMCategoryDetailsForm();
					leadCategoryForm.EntityType = EntityTypesEnum.Leads;
				}
				return leadCategoryForm;
			}
		}

		public static ContainerTrackingForm ContainerTrackingFormObj
		{
			get
			{
				if (containerTrackingForm == null || containerTrackingForm.IsDisposed)
				{
					containerTrackingForm = new ContainerTrackingForm();
				}
				return containerTrackingForm;
			}
		}

		public static ContainerTrackingWizardForm ContainerTrackingWizardFormObj
		{
			get
			{
				if (containerTrackingWizrdForm == null || containerTrackingWizrdForm.IsDisposed)
				{
					containerTrackingWizrdForm = new ContainerTrackingWizardForm();
				}
				return containerTrackingWizrdForm;
			}
		}

		public static PendingDNReport PendingDNReportFormObj
		{
			get
			{
				if (pendingDNReportForm == null || pendingDNReportForm.IsDisposed)
				{
					pendingDNReportForm = new PendingDNReport();
				}
				return pendingDNReportForm;
			}
		}

		public static ProfitAndLossReportRevisedForm ProfitAndLossReportRevisedFormObj
		{
			get
			{
				if (profitAndLossReportRevisedForm == null || profitAndLossReportRevisedForm.IsDisposed)
				{
					profitAndLossReportRevisedForm = new ProfitAndLossReportRevisedForm();
				}
				return profitAndLossReportRevisedForm;
			}
		}

		public static PriceListDetailsForm PriceListDetailsFormObj
		{
			get
			{
				if (priceListDetailsForm == null || priceListDetailsForm.IsDisposed)
				{
					priceListDetailsForm = new PriceListDetailsForm();
				}
				return priceListDetailsForm;
			}
		}

		public static ImageViewerForm ImageViewerFormObj
		{
			get
			{
				if (imageViewerForm == null || imageViewerForm.IsDisposed)
				{
					imageViewerForm = new ImageViewerForm();
				}
				return imageViewerForm;
			}
		}

		public static SalesEnquiryForm SalesEnquiryFormObj
		{
			get
			{
				if (salesEnquiryForm == null || salesEnquiryForm.IsDisposed)
				{
					salesEnquiryForm = new SalesEnquiryForm();
				}
				return salesEnquiryForm;
			}
		}

		public static SalesOrderForm SalesOrderFormObj
		{
			get
			{
				if (salesOrderForm == null || salesOrderForm.IsDisposed)
				{
					salesOrderForm = new SalesOrderForm();
				}
				return salesOrderForm;
			}
		}

		public static SalesProformaInvoiceForm SalesProformaInvoiceFormObj
		{
			get
			{
				if (salesProformaInvoiceForm == null || salesProformaInvoiceForm.IsDisposed)
				{
					salesProformaInvoiceForm = new SalesProformaInvoiceForm();
				}
				return salesProformaInvoiceForm;
			}
		}

		public static DeliveryNoteForm DeliveryNoteFormObj
		{
			get
			{
				if (deliveryNoteForm == null || deliveryNoteForm.IsDisposed)
				{
					deliveryNoteForm = new DeliveryNoteForm();
				}
				return deliveryNoteForm;
			}
		}

		public static SalesInvoiceForm SalesInvoiceFormObj
		{
			get
			{
				if (salesInvoiceForm == null || salesInvoiceForm.IsDisposed)
				{
					salesInvoiceForm = new SalesInvoiceForm();
				}
				return salesInvoiceForm;
			}
		}

		public static SalesReceiptForm SalesReceiptFormObj
		{
			get
			{
				if (salesReceiptForm == null || salesReceiptForm.IsDisposed)
				{
					salesReceiptForm = new SalesReceiptForm();
				}
				return salesReceiptForm;
			}
		}

		public static SalesReturnCreditForm SalesReturnCreditFormObj
		{
			get
			{
				if (salesReturnCreditForm == null || salesReturnCreditForm.IsDisposed)
				{
					salesReturnCreditForm = new SalesReturnCreditForm();
				}
				return salesReturnCreditForm;
			}
		}

		public static SalesReturnCashForm SalesReturnCashFormObj
		{
			get
			{
				if (salesReturnCashForm == null || salesReturnCashForm.IsDisposed)
				{
					salesReturnCashForm = new SalesReturnCashForm();
				}
				return salesReturnCashForm;
			}
		}

		public static DeliveryReturnForm DeliveryReturnFormObj
		{
			get
			{
				if (deliveryReturnForm == null || deliveryReturnForm.IsDisposed)
				{
					deliveryReturnForm = new DeliveryReturnForm();
				}
				return deliveryReturnForm;
			}
		}

		public static PurchaseQuoteForm PurchaseQuoteFormObj
		{
			get
			{
				if (purchaseQuoteForm == null || purchaseQuoteForm.IsDisposed)
				{
					purchaseQuoteForm = new PurchaseQuoteForm();
				}
				return purchaseQuoteForm;
			}
		}

		public static PurchaseOrderForm PurchaseOrderFormObj
		{
			get
			{
				if (purchaseOrderForm == null || purchaseOrderForm.IsDisposed)
				{
					purchaseOrderForm = new PurchaseOrderForm();
				}
				return purchaseOrderForm;
			}
		}

		public static PurchaseOrderNonInvForm PurchaseOrderNonInvFormObj
		{
			get
			{
				if (purchaseOrderNonInvForm == null || purchaseOrderNonInvForm.IsDisposed)
				{
					purchaseOrderNonInvForm = new PurchaseOrderNonInvForm();
				}
				return purchaseOrderNonInvForm;
			}
		}

		public static PurchasePackingListForm POShipmentFormObj
		{
			get
			{
				if (poShipmentForm == null || poShipmentForm.IsDisposed)
				{
					poShipmentForm = new PurchasePackingListForm();
				}
				return poShipmentForm;
			}
		}

		public static PortDetailsForm PortDetailsFormObj
		{
			get
			{
				if (portDetailsForm == null || portDetailsForm.IsDisposed)
				{
					portDetailsForm = new PortDetailsForm();
				}
				return portDetailsForm;
			}
		}

		public static CollateralDetailsForm CollateralDetailsFormObj
		{
			get
			{
				if (collateralDetailsForm == null || collateralDetailsForm.IsDisposed)
				{
					collateralDetailsForm = new CollateralDetailsForm();
				}
				return collateralDetailsForm;
			}
		}

		public static LPOReceiptForm LPOReceiptFormObj
		{
			get
			{
				if (lpoReceiptForm == null || lpoReceiptForm.IsDisposed)
				{
					lpoReceiptForm = new LPOReceiptForm();
				}
				return lpoReceiptForm;
			}
		}

		public static CandidateCancellationForm CandidateCancellationFormObj
		{
			get
			{
				if (candidateCancellationForm == null || candidateCancellationForm.IsDisposed)
				{
					candidateCancellationForm = new CandidateCancellationForm();
				}
				return candidateCancellationForm;
			}
		}

		public static EmployeeCancellationForm EmployeeCancellationFormObj
		{
			get
			{
				if (employeeCancellationForm == null || employeeCancellationForm.IsDisposed)
				{
					employeeCancellationForm = new EmployeeCancellationForm();
				}
				return employeeCancellationForm;
			}
		}

		public static PurchaseInvoiceForm PurchaseInvoiceFormObj
		{
			get
			{
				if (purchaseInvoiceForm == null || purchaseInvoiceForm.IsDisposed)
				{
					purchaseInvoiceForm = new PurchaseInvoiceForm();
				}
				return purchaseInvoiceForm;
			}
		}

		public static PurchaseInvoiceNonInvForm PurchaseInvoiceNonInvFormObj
		{
			get
			{
				if (purchaseInvoiceNonInvForm == null || purchaseInvoiceNonInvForm.IsDisposed)
				{
					purchaseInvoiceNonInvForm = new PurchaseInvoiceNonInvForm();
				}
				return purchaseInvoiceNonInvForm;
			}
		}

		public static PurchaseGRNForm PurchaseReceiptFormObj
		{
			get
			{
				if (purchaseReceiptForm == null || purchaseReceiptForm.IsDisposed)
				{
					purchaseReceiptForm = new PurchaseGRNForm();
				}
				return purchaseReceiptForm;
			}
		}

		public static ImportPurchaseGRNForm ImportPurchaseGRNFormObj
		{
			get
			{
				if (importPurchaseGRNForm == null || importPurchaseGRNForm.IsDisposed)
				{
					importPurchaseGRNForm = new ImportPurchaseGRNForm();
				}
				return importPurchaseGRNForm;
			}
		}

		public static CashPurchaseForm CashPurchaseFormObj
		{
			get
			{
				if (cashPurchaseForm == null || cashPurchaseForm.IsDisposed)
				{
					cashPurchaseForm = new CashPurchaseForm();
				}
				return cashPurchaseForm;
			}
		}

		public static PurchaseReturnCashForm PurchaseReturnCashFormObj
		{
			get
			{
				if (purchaseReturnCashForm == null || purchaseReturnCashForm.IsDisposed)
				{
					purchaseReturnCashForm = new PurchaseReturnCashForm();
				}
				return purchaseReturnCashForm;
			}
		}

		public static PurchaseReturnCreditForm PurchaseReturnCreditFormObj
		{
			get
			{
				if (purchaseReturnCreditForm == null || purchaseReturnCreditForm.IsDisposed)
				{
					purchaseReturnCreditForm = new PurchaseReturnCreditForm();
				}
				return purchaseReturnCreditForm;
			}
		}

		public static GRNReturnForm GRNReturnFormObj
		{
			get
			{
				if (grnReturnForm == null || grnReturnForm.IsDisposed)
				{
					grnReturnForm = new GRNReturnForm();
				}
				return grnReturnForm;
			}
		}

		public static FixedAssetBulkPurchaseForm FixedAssetBulkPurchaseFormObj
		{
			get
			{
				if (fixedAssetBulkPurchaseForm == null || fixedAssetBulkPurchaseForm.IsDisposed)
				{
					fixedAssetBulkPurchaseForm = new FixedAssetBulkPurchaseForm();
				}
				return fixedAssetBulkPurchaseForm;
			}
		}

		public static EmployeeAppraisalForm EmployeeAppraisalFormObj
		{
			get
			{
				if (employeeAppraisalForm == null || employeeAppraisalForm.IsDisposed)
				{
					employeeAppraisalForm = new EmployeeAppraisalForm();
				}
				return employeeAppraisalForm;
			}
		}

		public static ClientAssetForm ClientAssetFormObj
		{
			get
			{
				if (clientAssetForm == null || clientAssetForm.IsDisposed)
				{
					clientAssetForm = new ClientAssetForm();
				}
				return clientAssetForm;
			}
		}

		public static ReleaseTypeForm ReleaseTypeFormObj
		{
			get
			{
				if (releaseTypeForm == null || releaseTypeForm.IsDisposed)
				{
					releaseTypeForm = new ReleaseTypeForm();
				}
				return releaseTypeForm;
			}
		}

		public static LegalActionStatusForm LegalActionStatusFormObj
		{
			get
			{
				if (legalActionStatusForm == null || legalActionStatusForm.IsDisposed)
				{
					legalActionStatusForm = new LegalActionStatusForm();
				}
				return legalActionStatusForm;
			}
		}

		public static ServiceItemForm ServiceItemFormObj
		{
			get
			{
				if (serviceItemForm == null || serviceItemForm.IsDisposed)
				{
					serviceItemForm = new ServiceItemForm();
				}
				return serviceItemForm;
			}
		}

		public static BinDetailsForm BinDetailsFormObj
		{
			get
			{
				if (binDetailsForm == null || binDetailsForm.IsDisposed)
				{
					binDetailsForm = new BinDetailsForm();
				}
				return binDetailsForm;
			}
		}

		public static RackDetailsForm RackDetailsformObj
		{
			get
			{
				if (rackDetailsForm == null || rackDetailsForm.IsDisposed)
				{
					rackDetailsForm = new RackDetailsForm();
				}
				return rackDetailsForm;
			}
		}

		public static GenericProductTypeDetailsForm GenericProductTypeDetailsformObj
		{
			get
			{
				if (genericProductTypeDetailsForm == null || genericProductTypeDetailsForm.IsDisposed)
				{
					genericProductTypeDetailsForm = new GenericProductTypeDetailsForm();
					genericProductTypeDetailsForm.IsNew = true;
				}
				return genericProductTypeDetailsForm;
			}
		}

		public static ServiceCallTrackForm ServiceCallTrackFormObj
		{
			get
			{
				if (serviceCallTrackForm == null || serviceCallTrackForm.IsDisposed)
				{
					serviceCallTrackForm = new ServiceCallTrackForm();
				}
				return serviceCallTrackForm;
			}
		}

		public static ServiceActivityDetailsForm ServiceActivityDetailsFormObj
		{
			get
			{
				if (serviceActivityDetailsForm == null || serviceActivityDetailsForm.IsDisposed)
				{
					serviceActivityDetailsForm = new ServiceActivityDetailsForm();
				}
				return serviceActivityDetailsForm;
			}
		}

		public static JobMaintenanceServiceEntryForm JobMaintenanceServiceEntryFormObj
		{
			get
			{
				if (jobMaintenanceServiceEntryForm == null || jobMaintenanceServiceEntryForm.IsDisposed)
				{
					jobMaintenanceServiceEntryForm = new JobMaintenanceServiceEntryForm();
				}
				return jobMaintenanceServiceEntryForm;
			}
		}

		public static JobMaintenanceScheduleForm JobMaintenanceSheduleFormObj
		{
			get
			{
				if (jobMaintenanceScheduleForm == null || jobMaintenanceScheduleForm.IsDisposed)
				{
					jobMaintenanceScheduleForm = new JobMaintenanceScheduleForm();
				}
				return jobMaintenanceScheduleForm;
			}
		}

		public static HolidayCalendarForm HolidayCalendarFormObj
		{
			get
			{
				if (holidayCalendarForm == null || holidayCalendarForm.IsDisposed)
				{
					holidayCalendarForm = new HolidayCalendarForm();
				}
				return holidayCalendarForm;
			}
		}

		public static InventoryTransactionsReport InventoryTransactionsReportObj
		{
			get
			{
				if (inventoryTransactionsReport == null || inventoryTransactionsReport.IsDisposed)
				{
					inventoryTransactionsReport = new InventoryTransactionsReport();
				}
				return inventoryTransactionsReport;
			}
		}

		public static ProjectInventoryTransactionsReport ProjectInventoryTransactionsReportObj
		{
			get
			{
				if (projectInventoryTransactionsReport == null || projectInventoryTransactionsReport.IsDisposed)
				{
					projectInventoryTransactionsReport = new ProjectInventoryTransactionsReport();
				}
				return projectInventoryTransactionsReport;
			}
		}

		public static InventoryTransferReport InventoryTransferReportObj
		{
			get
			{
				if (inventoryTransferReport == null || inventoryTransferReport.IsDisposed)
				{
					inventoryTransferReport = new InventoryTransferReport();
				}
				return inventoryTransferReport;
			}
		}

		public static ItemMovementGRNReport ItemMovementGRNReportObj
		{
			get
			{
				if (itemMovementGRNReport == null || itemMovementGRNReport.IsDisposed)
				{
					itemMovementGRNReport = new ItemMovementGRNReport();
				}
				return itemMovementGRNReport;
			}
		}

		public static ItemMovementConsignInReport ItemMovementConsignInReportObj
		{
			get
			{
				if (itemMovementConsignInReport == null || itemMovementConsignInReport.IsDisposed)
				{
					itemMovementConsignInReport = new ItemMovementConsignInReport();
				}
				return itemMovementConsignInReport;
			}
		}

		public static ConsignInReceiptReport ConsignInReceiptReportObj
		{
			get
			{
				if (ConsignInReceiptReport == null || ConsignInReceiptReport.IsDisposed)
				{
					ConsignInReceiptReport = new ConsignInReceiptReport();
				}
				return ConsignInReceiptReport;
			}
		}

		public static ConsignmentOutSettlementReport ConsignmentOutSettlementReportObj
		{
			get
			{
				if (consignmentOutSettlementReport == null || consignmentOutSettlementReport.IsDisposed)
				{
					consignmentOutSettlementReport = new ConsignmentOutSettlementReport();
				}
				return consignmentOutSettlementReport;
			}
		}

		public static ConsignmentOutIssuedReport ConsignmentOutIssuedReportObj
		{
			get
			{
				if (consignmentOutIssuedReport == null || consignmentOutIssuedReport.IsDisposed)
				{
					consignmentOutIssuedReport = new ConsignmentOutIssuedReport();
				}
				return consignmentOutIssuedReport;
			}
		}

		public static SalesByProductClassandCategoryReport salesByitemCategoryReportobj
		{
			get
			{
				if (salesByitemCategoryReport == null || salesByitemCategoryReport.IsDisposed)
				{
					salesByitemCategoryReport = new SalesByProductClassandCategoryReport();
				}
				return salesByitemCategoryReport;
			}
		}

		public static SalesPurchaseAnalysisReport SalesPurchaseAnalysisReportObj
		{
			get
			{
				if (salesPurchaseAnalysisReport == null || salesPurchaseAnalysisReport.IsDisposed)
				{
					salesPurchaseAnalysisReport = new SalesPurchaseAnalysisReport();
				}
				return salesPurchaseAnalysisReport;
			}
		}

		public static SalesComparisonReport SalesComparisonReportObj
		{
			get
			{
				if (salesComparisonReport == null || salesComparisonReport.IsDisposed)
				{
					salesComparisonReport = new SalesComparisonReport();
				}
				return salesComparisonReport;
			}
		}

		public static ConsignmentInSettlementReport ConsignmentInSettlementReportObj
		{
			get
			{
				if (consignmentInSettlementReport == null || consignmentInSettlementReport.IsDisposed)
				{
					consignmentInSettlementReport = new ConsignmentInSettlementReport();
				}
				return consignmentInSettlementReport;
			}
		}

		public static W3PLInventoryTransactionsReport W3PLInventoryTransactionsReportObj
		{
			get
			{
				if (W3PLinventoryTransactionsReport == null || W3PLinventoryTransactionsReport.IsDisposed)
				{
					W3PLinventoryTransactionsReport = new W3PLInventoryTransactionsReport();
				}
				return W3PLinventoryTransactionsReport;
			}
		}

		public static PurchaseByInventoryItemVendorBuyerReport PurchaseByInventoryItemVendorBuyerReportObj
		{
			get
			{
				if (purchaseByInventoryItemVendorBuyerReport == null || purchaseByInventoryItemVendorBuyerReport.IsDisposed)
				{
					purchaseByInventoryItemVendorBuyerReport = new PurchaseByInventoryItemVendorBuyerReport();
				}
				return purchaseByInventoryItemVendorBuyerReport;
			}
		}

		public static CustomerDueReport CustomerDueReportObj
		{
			get
			{
				if (customerDueReport == null || customerDueReport.IsDisposed)
				{
					customerDueReport = new CustomerDueReport();
				}
				return customerDueReport;
			}
		}

		public static CustomerTopListReport CustomerTopListReport
		{
			get
			{
				if (customerTopListReport == null || customerTopListReport.IsDisposed)
				{
					customerTopListReport = new CustomerTopListReport();
					customerTopListReport.ReportType = "C";
				}
				return customerTopListReport;
			}
		}

		public static CustomerTopListReport ProductTopListReport
		{
			get
			{
				if (customerTopListReport == null || customerTopListReport.IsDisposed)
				{
					customerTopListReport = new CustomerTopListReport();
					customerTopListReport.ReportType = "P";
					customerTopListReport.Text = "Inventory Top List Report";
				}
				return customerTopListReport;
			}
		}

		public static PropertyAccountTransactionsReportForm PropertyAccountTransactionReportObj
		{
			get
			{
				if (propertyAccountTransactionReportForm == null || propertyAccountTransactionReportForm.IsDisposed)
				{
					propertyAccountTransactionReportForm = new PropertyAccountTransactionsReportForm();
				}
				return propertyAccountTransactionReportForm;
			}
		}

		public static PropertyUnitAvailabilityReportForm PropertyUnitAvailabilityReportObj
		{
			get
			{
				if (propertyUnitAvailabilityReportForm == null || propertyUnitAvailabilityReportForm.IsDisposed)
				{
					propertyUnitAvailabilityReportForm = new PropertyUnitAvailabilityReportForm();
				}
				return propertyUnitAvailabilityReportForm;
			}
		}

		public static PropertyUnitHistoryReportForm UnitHistoryReportObj
		{
			get
			{
				if (propertyUnitHistoryReportForm == null || propertyUnitHistoryReportForm.IsDisposed)
				{
					propertyUnitHistoryReportForm = new PropertyUnitHistoryReportForm();
				}
				return propertyUnitHistoryReportForm;
			}
		}

		public static CustomerCenterForm CustomerCenterFormObj
		{
			get
			{
				if (customerCenterForm == null || customerCenterForm.IsDisposed)
				{
					customerCenterForm = new CustomerCenterForm(formParent);
					customerCenterForm.WindowState = FormWindowState.Maximized;
				}
				return customerCenterForm;
			}
		}

		public static AccountCenterForm AccountCenterFormObj
		{
			get
			{
				if (accountCenterForm == null || accountCenterForm.IsDisposed)
				{
					accountCenterForm = new AccountCenterForm(formParent);
					accountCenterForm.WindowState = FormWindowState.Maximized;
				}
				return accountCenterForm;
			}
		}

		public static VendorCenterForm VendorCenterFormObj
		{
			get
			{
				if (vendorCenterForm == null || vendorCenterForm.IsDisposed)
				{
					vendorCenterForm = new VendorCenterForm(formParent);
					vendorCenterForm.WindowState = FormWindowState.Maximized;
				}
				return vendorCenterForm;
			}
		}

		public static InventoryCenterForm InventoryCenterFormObj
		{
			get
			{
				if (inventoryCenterForm == null || inventoryCenterForm.IsDisposed)
				{
					inventoryCenterForm = new InventoryCenterForm(formParent);
					inventoryCenterForm.WindowState = FormWindowState.Maximized;
				}
				return inventoryCenterForm;
			}
		}

		public static ReportCenterForm ReportCenterFormObj
		{
			get
			{
				if (reportCenterForm == null || reportCenterForm.IsDisposed)
				{
					reportCenterForm = new ReportCenterForm(formParent);
					reportCenterForm.WindowState = FormWindowState.Maximized;
				}
				return reportCenterForm;
			}
		}

		public static HRCenterForm HRCenterFormObj
		{
			get
			{
				if (hrCenterForm == null || hrCenterForm.IsDisposed)
				{
					hrCenterForm = new HRCenterForm(formParent);
					hrCenterForm.WindowState = FormWindowState.Maximized;
				}
				return hrCenterForm;
			}
		}

		public static CompanyOptionsForm CompanyOptionsFormObj
		{
			get
			{
				if (companyOptionsForm == null || companyOptionsForm.IsDisposed)
				{
					companyOptionsForm = new CompanyOptionsForm();
				}
				return companyOptionsForm;
			}
		}

		public static PurchaseOrderImportForm PurchaseOrderImportFormObj
		{
			get
			{
				if (purchaseOrderImportForm == null || purchaseOrderImportForm.IsDisposed)
				{
					purchaseOrderImportForm = new PurchaseOrderImportForm();
				}
				return purchaseOrderImportForm;
			}
		}

		public static PurchaseInvoiceImportForm PurchaseInvoiceImportFormObj
		{
			get
			{
				if (purchaseInvoiceImportForm == null || purchaseInvoiceImportForm.IsDisposed)
				{
					purchaseInvoiceImportForm = new PurchaseInvoiceImportForm();
				}
				return purchaseInvoiceImportForm;
			}
		}

		public static ArrivalReportForm ArrivalReportFormObj
		{
			get
			{
				if (arrivalReportForm == null || arrivalReportForm.IsDisposed)
				{
					arrivalReportForm = new ArrivalReportForm();
				}
				return arrivalReportForm;
			}
		}

		public static ArrivalReportTemplateForm ArrivalReportTemplateFormObj
		{
			get
			{
				if (arrivalReportTemplateForm == null || arrivalReportTemplateForm.IsDisposed)
				{
					arrivalReportTemplateForm = new ArrivalReportTemplateForm();
				}
				return arrivalReportTemplateForm;
			}
		}

		public static QualityClaimForm QualityClaimFormObj
		{
			get
			{
				if (qualityClaimForm == null || qualityClaimForm.IsDisposed)
				{
					qualityClaimForm = new QualityClaimForm();
				}
				return qualityClaimForm;
			}
		}

		public static QualityTaskForm QualityTaskFormObj
		{
			get
			{
				if (qualityTaskForm == null || qualityTaskForm.IsDisposed)
				{
					qualityTaskForm = new QualityTaskForm();
					if (UserPreferences.OpenMDI)
					{
						qualityTaskForm.MdiParent = MainForm;
					}
					qualityTaskForm.Show();
				}
				return qualityTaskForm;
			}
		}

		public static SurveyorForm SurveyorFormObj
		{
			get
			{
				if (surveyorForm == null || surveyorForm.IsDisposed)
				{
					surveyorForm = new SurveyorForm();
					if (UserPreferences.OpenMDI)
					{
						surveyorForm.MdiParent = MainForm;
					}
					surveyorForm.Show();
				}
				return surveyorForm;
			}
		}

		public static ChangeCashRegisterForm ChangeCashRegisterFormObj
		{
			get
			{
				if (changeCashRegisterForm == null || changeCashRegisterForm.IsDisposed)
				{
					changeCashRegisterForm = new ChangeCashRegisterForm();
					if (UserPreferences.OpenMDI)
					{
						changeCashRegisterForm.MdiParent = MainForm;
					}
					changeCashRegisterForm.Show();
				}
				return changeCashRegisterForm;
			}
		}

		public static PropertyAgentDetailsForm PropertyAgentDetailsFormObj
		{
			get
			{
				if (propertyAgentDetailsForm == null || propertyAgentDetailsForm.IsDisposed)
				{
					propertyAgentDetailsForm = new PropertyAgentDetailsForm();
					if (showForm)
					{
						propertyAgentDetailsForm.Show();
					}
				}
				return propertyAgentDetailsForm;
			}
		}

		public static PropertyCategoryDetailsForm PropertyCategoryDetailsFormObj
		{
			get
			{
				if (propertyCategoryDetailsForm == null || propertyCategoryDetailsForm.IsDisposed)
				{
					propertyCategoryDetailsForm = new PropertyCategoryDetailsForm();
					if (showForm)
					{
						propertyCategoryDetailsForm.Show();
					}
				}
				return propertyCategoryDetailsForm;
			}
		}

		public static PropertyClassDetailsForm PropertyClassDetailsFormObj
		{
			get
			{
				if (propertyClassDetailsForm == null || propertyClassDetailsForm.IsDisposed)
				{
					propertyClassDetailsForm = new PropertyClassDetailsForm();
					if (showForm)
					{
						propertyClassDetailsForm.Show();
					}
				}
				return propertyClassDetailsForm;
			}
		}

		public static PropertyIncomeCodeDetailsForm PropertyIncomeCodeDetailsFormObj
		{
			get
			{
				if (propertyIncomeCodeDetailsForm == null || propertyIncomeCodeDetailsForm.IsDisposed)
				{
					propertyIncomeCodeDetailsForm = new PropertyIncomeCodeDetailsForm();
					if (showForm)
					{
						propertyIncomeCodeDetailsForm.Show();
					}
				}
				return propertyIncomeCodeDetailsForm;
			}
		}

		public static SalesByCustomerReport SalesByCustomerReportObj
		{
			get
			{
				if (salesByCustomerReport == null || salesByCustomerReport.IsDisposed)
				{
					salesByCustomerReport = new SalesByCustomerReport();
				}
				return salesByCustomerReport;
			}
		}

		public static SalesByItemReport SalesByItemReportObj
		{
			get
			{
				if (salesByItemReport == null || salesByItemReport.IsDisposed)
				{
					salesByItemReport = new SalesByItemReport();
				}
				return salesByItemReport;
			}
		}

		public static SalesBySalespersonReport SalesBySalespersonReportObj
		{
			get
			{
				if (salesBySalespersonReport == null || salesBySalespersonReport.IsDisposed)
				{
					salesBySalespersonReport = new SalesBySalespersonReport();
				}
				return salesBySalespersonReport;
			}
		}

		public static SalesByLocationReport SalesByLocationReportObj
		{
			get
			{
				if (salesByLocationReport == null || salesByLocationReport.IsDisposed)
				{
					salesByLocationReport = new SalesByLocationReport();
				}
				return salesByLocationReport;
			}
		}

		public static SalesByProductCategoryReport SalesByProductCategoryReportObj
		{
			get
			{
				if (salesByProductCategoryReport == null || salesByProductCategoryReport.IsDisposed)
				{
					salesByProductCategoryReport = new SalesByProductCategoryReport();
				}
				return salesByProductCategoryReport;
			}
		}

		public static SalesByCustomerGroupReport SalesByCustomerGroupReportObj
		{
			get
			{
				if (salesByCustomerGroupReport == null || salesByCustomerGroupReport.IsDisposed)
				{
					salesByCustomerGroupReport = new SalesByCustomerGroupReport();
				}
				return salesByCustomerGroupReport;
			}
		}

		public static TrialBalanceReportForm TrialBalanceReportFormObj
		{
			get
			{
				if (trialBalanceReportForm == null || trialBalanceReportForm.IsDisposed)
				{
					trialBalanceReportForm = new TrialBalanceReportForm();
				}
				return trialBalanceReportForm;
			}
		}

		public static BalanceSheetReportForm BalanceSheetReportFormObj
		{
			get
			{
				if (balanceSheetReportForm == null || balanceSheetReportForm.IsDisposed)
				{
					balanceSheetReportForm = new BalanceSheetReportForm();
				}
				return balanceSheetReportForm;
			}
		}

		public static ProfitAndLossReportForm ProfitAndLossReportFormObj
		{
			get
			{
				if (profitAndLossReportForm == null || profitAndLossReportForm.IsDisposed)
				{
					profitAndLossReportForm = new ProfitAndLossReportForm();
				}
				return profitAndLossReportForm;
			}
		}

		public static ProfitAndLossComparionReportForm ProfitAndLossComparisonReportFormObj
		{
			get
			{
				if (profitAndLossComparionReportForm == null || profitAndLossComparionReportForm.IsDisposed)
				{
					profitAndLossComparionReportForm = new ProfitAndLossComparionReportForm();
				}
				return profitAndLossComparionReportForm;
			}
		}

		public static ProfitAndLossMonthwiseReportForm ProfitAndLossMonthwiseReportFormObj
		{
			get
			{
				if (profitAndLossMonthwiseReportForm == null || profitAndLossMonthwiseReportForm.IsDisposed)
				{
					profitAndLossMonthwiseReportForm = new ProfitAndLossMonthwiseReportForm();
				}
				return profitAndLossMonthwiseReportForm;
			}
		}

		public static DailyCashSaleReport DailyCashRegisterReportFormObj
		{
			get
			{
				if (dailyCashSaleReportForm == null || dailyCashSaleReportForm.IsDisposed)
				{
					dailyCashSaleReportForm = new DailyCashSaleReport();
				}
				return dailyCashSaleReportForm;
			}
		}

		public static DailyCashReportForm DailyCashSalesReportFormObj
		{
			get
			{
				if (dailyCashReportForm == null || dailyCashReportForm.IsDisposed)
				{
					dailyCashReportForm = new DailyCashReportForm();
				}
				return dailyCashReportForm;
			}
		}

		public static CustomerListReport CustomerListReportObj
		{
			get
			{
				if (customerListReport == null || customerListReport.IsDisposed)
				{
					customerListReport = new CustomerListReport();
				}
				return customerListReport;
			}
		}

		public static CustomerContactListReport CustomerContactListReportObj
		{
			get
			{
				if (customerContactListReport == null || customerContactListReport.IsDisposed)
				{
					customerContactListReport = new CustomerContactListReport();
				}
				return customerContactListReport;
			}
		}

		public static CustomerProfileReport CustomerProfileReportObj
		{
			get
			{
				if (customerProfileReport == null || customerProfileReport.IsDisposed)
				{
					customerProfileReport = new CustomerProfileReport();
				}
				return customerProfileReport;
			}
		}

		public static CustomerActivityReport CustomerActivityReportObj
		{
			get
			{
				if (customerActivityReport == null || customerActivityReport.IsDisposed)
				{
					customerActivityReport = new CustomerActivityReport();
				}
				return customerActivityReport;
			}
		}

		public static CustomerYearMonthPaymentReport CustomerYearMonthPaymentReportObj
		{
			get
			{
				if (customerYearMonthPaymentReport == null || customerYearMonthPaymentReport.IsDisposed)
				{
					customerYearMonthPaymentReport = new CustomerYearMonthPaymentReport();
				}
				return customerYearMonthPaymentReport;
			}
		}

		public static EmployeeSalarySlipReport EmployeeSalarySlipReportFormObj
		{
			get
			{
				if (employeeSalarySlipReport == null || employeeSalarySlipReport.IsDisposed)
				{
					employeeSalarySlipReport = new EmployeeSalarySlipReport();
				}
				return employeeSalarySlipReport;
			}
		}

		public static AssemblyReport AssemblyReportFormObj
		{
			get
			{
				if (assemblyReport == null || assemblyReport.IsDisposed)
				{
					assemblyReport = new AssemblyReport();
				}
				return assemblyReport;
			}
		}

		public static SalesByItemCustomerSalespersonReport SalesByItemCustomerSalesperonReportFormObj
		{
			get
			{
				if (salesByItemCustomerSalespersonReport == null || salesByItemCustomerSalespersonReport.IsDisposed)
				{
					salesByItemCustomerSalespersonReport = new SalesByItemCustomerSalespersonReport();
				}
				return salesByItemCustomerSalespersonReport;
			}
		}

		public static SalesByMainCategory SalesByMainCategoryReportFormObj
		{
			get
			{
				if (salesByMainCategoryReportForm == null || salesByMainCategoryReportForm.IsDisposed)
				{
					salesByMainCategoryReportForm = new SalesByMainCategory();
				}
				return salesByMainCategoryReportForm;
			}
		}

		public static MonthlySalesPivotReport MonthlySalesPivotReportFormObj
		{
			get
			{
				if (monthlySalesPivotReportForm == null || monthlySalesPivotReportForm.IsDisposed)
				{
					monthlySalesPivotReportForm = new MonthlySalesPivotReport();
				}
				return monthlySalesPivotReportForm;
			}
		}

		public static MultipleInvoiceReport MultipleInvoiceReportFormObj
		{
			get
			{
				if (multipleInvoiceReportForm == null || multipleInvoiceReportForm.IsDisposed)
				{
					multipleInvoiceReportForm = new MultipleInvoiceReport();
				}
				return multipleInvoiceReportForm;
			}
		}

		public static SalesOrderDetailReport SalesOrderDetailsReportFormObj
		{
			get
			{
				if (salesOrderDetailsReportForm == null || salesOrderDetailsReportForm.IsDisposed)
				{
					salesOrderDetailsReportForm = new SalesOrderDetailReport();
				}
				return salesOrderDetailsReportForm;
			}
		}

		public static ConsignmentInReport ConsignmentInReportFormObj
		{
			get
			{
				if (consignmentInReport == null || consignmentInReport.IsDisposed)
				{
					consignmentInReport = new ConsignmentInReport();
				}
				return consignmentInReport;
			}
		}

		public static FixedAssetListReport FixedAssetListReportObj
		{
			get
			{
				if (fixedAssetListReport == null || fixedAssetListReport.IsDisposed)
				{
					fixedAssetListReport = new FixedAssetListReport();
				}
				return fixedAssetListReport;
			}
		}

		public static FixedAssetPurchaseReport FixedAssetPurchaseReportObj
		{
			get
			{
				if (fixedAssetPurchaseReport == null || fixedAssetPurchaseReport.IsDisposed)
				{
					fixedAssetPurchaseReport = new FixedAssetPurchaseReport();
				}
				return fixedAssetPurchaseReport;
			}
		}

		public static FixedAssetTransactionsReport FixedAssetTransactionsReportObj
		{
			get
			{
				if (fixedAssetTransactionsReport == null || fixedAssetTransactionsReport.IsDisposed)
				{
					fixedAssetTransactionsReport = new FixedAssetTransactionsReport();
				}
				return fixedAssetTransactionsReport;
			}
		}

		public static FixedAssetTransferReport FixedAssetTransferReportObj
		{
			get
			{
				if (fixedAssetTransferReport == null || fixedAssetTransferReport.IsDisposed)
				{
					fixedAssetTransferReport = new FixedAssetTransferReport();
				}
				return fixedAssetTransferReport;
			}
		}

		public static FixedAssetDepreciationReport FixedAssetDepreciationReportObj
		{
			get
			{
				if (fixedAssetDepreciationReport == null || fixedAssetDepreciationReport.IsDisposed)
				{
					fixedAssetDepreciationReport = new FixedAssetDepreciationReport();
				}
				return fixedAssetDepreciationReport;
			}
		}

		public static FixedAssetSaleReport FixedAssetSaleReportObj
		{
			get
			{
				if (fixedAssetSaleReport == null || fixedAssetSaleReport.IsDisposed)
				{
					fixedAssetSaleReport = new FixedAssetSaleReport();
				}
				return fixedAssetSaleReport;
			}
		}

		public static ConsignmentOutReport ConsignmentOutReportFormObj
		{
			get
			{
				if (consignmentOutReport == null || consignmentOutReport.IsDisposed)
				{
					consignmentOutReport = new ConsignmentOutReport();
				}
				return consignmentOutReport;
			}
		}

		public static CustomerOutstandingSummaryReport CustomerOutstandingSummaryReportFormObj
		{
			get
			{
				if (customerOutstandingSummaryReport == null || customerOutstandingSummaryReport.IsDisposed)
				{
					customerOutstandingSummaryReport = new CustomerOutstandingSummaryReport();
				}
				return customerOutstandingSummaryReport;
			}
		}

		public static DailySalesAnalysisReport DailySalesAnalysisReportFormObj
		{
			get
			{
				if (dailySalesAnalysisReportForm == null || dailySalesAnalysisReportForm.IsDisposed)
				{
					dailySalesAnalysisReportForm = new DailySalesAnalysisReport();
				}
				return dailySalesAnalysisReportForm;
			}
		}

		public static SalesByProductBrandReport SalesByProductBrandReportFormObj
		{
			get
			{
				if (salesByProductBrandReportForm == null || salesByProductBrandReportForm.IsDisposed)
				{
					salesByProductBrandReportForm = new SalesByProductBrandReport();
				}
				return salesByProductBrandReportForm;
			}
		}

		public static VendorOutstandingSummaryReport VendorOutstandingSummaryReportObj
		{
			get
			{
				if (vendorOutstandingSummaryReportForm == null || vendorOutstandingSummaryReportForm.IsDisposed)
				{
					vendorOutstandingSummaryReportForm = new VendorOutstandingSummaryReport();
				}
				return vendorOutstandingSummaryReportForm;
			}
		}

		public static SalespersonCommisionReport SalespersonCommisionReportObj
		{
			get
			{
				if (SalespersonCommisionReportForm == null || SalespersonCommisionReportForm.IsDisposed)
				{
					SalespersonCommisionReportForm = new SalespersonCommisionReport();
				}
				return SalespersonCommisionReportForm;
			}
		}

		public static SalesProfitabilityReport SalesProfitabilityReportObj
		{
			get
			{
				if (salesProfitabilityReportForm == null || salesProfitabilityReportForm.IsDisposed)
				{
					salesProfitabilityReportForm = new SalesProfitabilityReport();
				}
				return salesProfitabilityReportForm;
			}
		}

		public static TaxDetailsReport TaxDetailsReportObj
		{
			get
			{
				if (taxDetailsReportForm == null || taxDetailsReportForm.IsDisposed)
				{
					taxDetailsReportForm = new TaxDetailsReport();
				}
				return taxDetailsReportForm;
			}
		}

		public static SalesReservedReport salesReservedReportFormObj
		{
			get
			{
				if (salesReservedReportForm == null || salesReservedReportForm.IsDisposed)
				{
					salesReservedReportForm = new SalesReservedReport();
				}
				return salesReservedReportForm;
			}
		}

		public static TransporterDetailsForm TransporterDetailsFormObj
		{
			get
			{
				if (transporterDetailsForm == null || transporterDetailsForm.IsDisposed)
				{
					transporterDetailsForm = new TransporterDetailsForm();
				}
				return transporterDetailsForm;
			}
		}

		public static CustomGadgetDetailForm CustomGadgetDetailsFormObj
		{
			get
			{
				if (customGadgetDetailsForm == null || customGadgetDetailsForm.IsDisposed)
				{
					customGadgetDetailsForm = new CustomGadgetDetailForm();
				}
				return customGadgetDetailsForm;
			}
		}

		public static ContainerSizeDetailsForm ContainerSizeDetailsFormObj
		{
			get
			{
				if (containerSizeDetailsForm == null || containerSizeDetailsForm.IsDisposed)
				{
					containerSizeDetailsForm = new ContainerSizeDetailsForm();
				}
				return containerSizeDetailsForm;
			}
		}

		public static INCODetailsForm INCODetailsFormObj
		{
			get
			{
				if (INCODetailsForm == null || INCODetailsForm.IsDisposed)
				{
					INCODetailsForm = new INCODetailsForm();
				}
				return INCODetailsForm;
			}
		}

		public static ReportTemplatesUpdateForm ReportTemplatesUpdateFormObj
		{
			get
			{
				if (reportTemplatesUpdateForm == null || reportTemplatesUpdateForm.IsDisposed)
				{
					reportTemplatesUpdateForm = new ReportTemplatesUpdateForm();
				}
				return reportTemplatesUpdateForm;
			}
		}

		public static InventoryTransactionsLotwiseReport InventoryTransactionsLotwiseReportObj
		{
			get
			{
				if (inventoryTransactionsLotwiseReport == null || inventoryTransactionsLotwiseReport.IsDisposed)
				{
					inventoryTransactionsLotwiseReport = new InventoryTransactionsLotwiseReport();
				}
				return inventoryTransactionsLotwiseReport;
			}
		}

		public static PurchaseExpenseAllocationReport PurchaseExpenseAllocationReportFormObj
		{
			get
			{
				if (purchaseExpenseAllocationReport == null || purchaseExpenseAllocationReport.IsDisposed)
				{
					purchaseExpenseAllocationReport = new PurchaseExpenseAllocationReport();
				}
				return purchaseExpenseAllocationReport;
			}
		}

		public static PurchaseOrderDetailReport PurchaseOrderDetailReportFormObj
		{
			get
			{
				if (purchaseOrderDetailReport == null || purchaseOrderDetailReport.IsDisposed)
				{
					purchaseOrderDetailReport = new PurchaseOrderDetailReport();
				}
				return purchaseOrderDetailReport;
			}
		}

		public static PurchaseByItemVendorBuyerReport PurchaseByItemVendorBuyerReportFormObj
		{
			get
			{
				if (purchaseByItemVendorBuyerReport == null || purchaseByItemVendorBuyerReport.IsDisposed)
				{
					purchaseByItemVendorBuyerReport = new PurchaseByItemVendorBuyerReport();
				}
				return purchaseByItemVendorBuyerReport;
			}
		}

		public static CashFlowReportForm CashFlowReportFormObj
		{
			get
			{
				if (cashFlowReportForm == null || cashFlowReportForm.IsDisposed)
				{
					cashFlowReportForm = new CashFlowReportForm();
				}
				return cashFlowReportForm;
			}
		}

		public static PurchaseCostSheetReport purchaseCostSheetReportObj
		{
			get
			{
				if (purchaseCostSheetReport == null || purchaseCostSheetReport.IsDisposed)
				{
					purchaseCostSheetReport = new PurchaseCostSheetReport();
				}
				return purchaseCostSheetReport;
			}
		}

		public static AccountTransactionsReportForm AccountTransactionsReportFormObj
		{
			get
			{
				if (accountTransactionsReportForm == null || accountTransactionsReportForm.IsDisposed)
				{
					accountTransactionsReportForm = new AccountTransactionsReportForm();
				}
				return accountTransactionsReportForm;
			}
		}

		public static AccountCostCenterReportForm AccountCostCenterReportFormObj
		{
			get
			{
				if (accountCostCenterReportForm == null || accountCostCenterReportForm.IsDisposed)
				{
					accountCostCenterReportForm = new AccountCostCenterReportForm();
				}
				return accountCostCenterReportForm;
			}
		}

		public static AccountLedgerReportForm AccountLedgerReportFormObj
		{
			get
			{
				if (accountLedgerReportForm == null || accountLedgerReportForm.IsDisposed)
				{
					accountLedgerReportForm = new AccountLedgerReportForm();
				}
				return accountLedgerReportForm;
			}
		}

		public static BankLedgerReportForm BankLedgerReportFormObj
		{
			get
			{
				if (bankLedgerReportForm == null || bankLedgerReportForm.IsDisposed)
				{
					bankLedgerReportForm = new BankLedgerReportForm();
				}
				return bankLedgerReportForm;
			}
		}

		public static SalesManDueReport SalesManDueReportObj
		{
			get
			{
				if (salesManDueReport == null || salesManDueReport.IsDisposed)
				{
					salesManDueReport = new SalesManDueReport();
				}
				return salesManDueReport;
			}
		}

		public static ProjectAccountTransactionsReportForm ProjectAccountTransactionsReportFormObj
		{
			get
			{
				if (projectAccountTransactionsReportForm == null || projectAccountTransactionsReportForm.IsDisposed)
				{
					projectAccountTransactionsReportForm = new ProjectAccountTransactionsReportForm();
				}
				return projectAccountTransactionsReportForm;
			}
		}

		public static AccountAnalysisReportForm AccountAnalysisReportFormObj
		{
			get
			{
				if (accountAnalysisReportForm == null || accountAnalysisReportForm.IsDisposed)
				{
					accountAnalysisReportForm = new AccountAnalysisReportForm();
				}
				return accountAnalysisReportForm;
			}
		}

		public static AccountAnalysisPivotReportForm AccountAnalysisPivotReportFormObj
		{
			get
			{
				if (accountAnalysisPivotReportForm == null || accountAnalysisPivotReportForm.IsDisposed)
				{
					accountAnalysisPivotReportForm = new AccountAnalysisPivotReportForm();
				}
				return accountAnalysisPivotReportForm;
			}
		}

		public static VendorListReport VendorListReportObj
		{
			get
			{
				if (vendorListReport == null || vendorListReport.IsDisposed)
				{
					vendorListReport = new VendorListReport();
				}
				return vendorListReport;
			}
		}

		public static VendorContactListReport VendorContactListReportObj
		{
			get
			{
				if (vendorContactListReport == null || vendorContactListReport.IsDisposed)
				{
					vendorContactListReport = new VendorContactListReport();
				}
				return vendorContactListReport;
			}
		}

		public static VendorProfileReport VendorProfileReportObj
		{
			get
			{
				if (vendorProfileReport == null || vendorProfileReport.IsDisposed)
				{
					vendorProfileReport = new VendorProfileReport();
				}
				return vendorProfileReport;
			}
		}

		public static VendorActivityReport VendorActivityReportObj
		{
			get
			{
				if (vendorActivityReport == null || vendorActivityReport.IsDisposed)
				{
					vendorActivityReport = new VendorActivityReport();
				}
				return vendorActivityReport;
			}
		}

		public static ProductListReport ProductListReportObj
		{
			get
			{
				if (productListReport == null || productListReport.IsDisposed)
				{
					productListReport = new ProductListReport();
				}
				return productListReport;
			}
		}

		public static PurchaseByVendorReport PurchaseByVendorReportObj
		{
			get
			{
				if (purchaseByVendorReport == null || purchaseByVendorReport.IsDisposed)
				{
					purchaseByVendorReport = new PurchaseByVendorReport();
				}
				return purchaseByVendorReport;
			}
		}

		public static PurchaseByVendorGroupReport PurchaseByVendorGroupReportObj
		{
			get
			{
				if (purchaseByVendorGroupReport == null || purchaseByVendorGroupReport.IsDisposed)
				{
					purchaseByVendorGroupReport = new PurchaseByVendorGroupReport();
				}
				return purchaseByVendorGroupReport;
			}
		}

		public static PurchaseByItemReport PurchaseByItemReportObj
		{
			get
			{
				if (purchaseByItemReport == null || purchaseByItemReport.IsDisposed)
				{
					purchaseByItemReport = new PurchaseByItemReport();
				}
				return purchaseByItemReport;
			}
		}

		public static PurchaseByProductCategoryReport PurchaseByProductCategoryReportObj
		{
			get
			{
				if (purchaseByProductCategoryReport == null || purchaseByProductCategoryReport.IsDisposed)
				{
					purchaseByProductCategoryReport = new PurchaseByProductCategoryReport();
				}
				return purchaseByProductCategoryReport;
			}
		}

		public static PurchaseByBuyerReport PurchaseByBuyerReportObj
		{
			get
			{
				if (purchaseByBuyerReport == null || purchaseByBuyerReport.IsDisposed)
				{
					purchaseByBuyerReport = new PurchaseByBuyerReport();
				}
				return purchaseByBuyerReport;
			}
		}

		public static PurchaseByLocationReport PurchaseByLocationReportObj
		{
			get
			{
				if (purchaseByLocationReport == null || purchaseByLocationReport.IsDisposed)
				{
					purchaseByLocationReport = new PurchaseByLocationReport();
				}
				return purchaseByLocationReport;
			}
		}

		public static ProductPriceListReport ProductPriceListReportObj
		{
			get
			{
				if (productPriceListReport == null || productPriceListReport.IsDisposed)
				{
					productPriceListReport = new ProductPriceListReport();
				}
				return productPriceListReport;
			}
		}

		public static ProductStockListReport ProductStockListReportObj
		{
			get
			{
				if (productStockListReport == null || productStockListReport.IsDisposed)
				{
					productStockListReport = new ProductStockListReport();
				}
				return productStockListReport;
			}
		}

		public static ProductStockListReport ProductValuationReportObj
		{
			get
			{
				if (productValuationReport == null || productValuationReport.IsDisposed)
				{
					productValuationReport = new ProductStockListReport();
					productValuationReport.Name = "ProductStockListReport";
					productValuationReport.Text = "Inventory Valuation Report";
					productValuationReport.IsValuation = true;
				}
				return productValuationReport;
			}
		}

		public static ProductCatalogReport ProductCatalogReportObj
		{
			get
			{
				if (productCatalogReport == null || productCatalogReport.IsDisposed)
				{
					productCatalogReport = new ProductCatalogReport();
				}
				return productCatalogReport;
			}
		}

		public static FreightChargeReport FreightChargeReportObj
		{
			get
			{
				if (freightChargeReport == null || freightChargeReport.IsDisposed)
				{
					freightChargeReport = new FreightChargeReport();
				}
				return freightChargeReport;
			}
		}

		public static EmployeeTransferForm EmployeeTransferFormObj
		{
			get
			{
				if (employeeTransferForm == null || employeeTransferForm.IsDisposed)
				{
					employeeTransferForm = new EmployeeTransferForm();
				}
				return employeeTransferForm;
			}
		}

		public static EmployeeTerminationForm EmployeeTerminationFormObj
		{
			get
			{
				if (employeeTerminationForm == null || employeeTerminationForm.IsDisposed)
				{
					employeeTerminationForm = new EmployeeTerminationForm();
				}
				return employeeTerminationForm;
			}
		}

		public static EmployeePromotionForm EmployeePromotionFormObj
		{
			get
			{
				if (employeePromotionForm == null || employeePromotionForm.IsDisposed)
				{
					employeePromotionForm = new EmployeePromotionForm();
				}
				return employeePromotionForm;
			}
		}

		public static DisciplineActionTypeDetailsForm DisciplineActionTypeDetailsFormObj
		{
			get
			{
				if (disciplineActionTypeDetailsForm == null || disciplineActionTypeDetailsForm.IsDisposed)
				{
					disciplineActionTypeDetailsForm = new DisciplineActionTypeDetailsForm();
				}
				return disciplineActionTypeDetailsForm;
			}
		}

		public static EmployeeDisciplinaryActionForm EmployeeDisciplinaryActionFormObj
		{
			get
			{
				if (employeeDisciplinaryActionForm == null || employeeDisciplinaryActionForm.IsDisposed)
				{
					employeeDisciplinaryActionForm = new EmployeeDisciplinaryActionForm();
				}
				return employeeDisciplinaryActionForm;
			}
		}

		public static EmployeeRehireForm EmployeeRehireFormObj
		{
			get
			{
				if (employeeRehireForm == null || employeeRehireForm.IsDisposed)
				{
					employeeRehireForm = new EmployeeRehireForm();
				}
				return employeeRehireForm;
			}
		}

		public static EmployeeActivityTypeDetailsForm EmployeeActivityTypeDetailsFormObj
		{
			get
			{
				if (employeeActivityTypeDetailsForm == null || employeeActivityTypeDetailsForm.IsDisposed)
				{
					employeeActivityTypeDetailsForm = new EmployeeActivityTypeDetailsForm();
				}
				return employeeActivityTypeDetailsForm;
			}
		}

		public static EmployeeGeneralActivityForm EmployeeGeneralActivityFormObj
		{
			get
			{
				if (employeeGeneralActivityForm == null || employeeGeneralActivityForm.IsDisposed)
				{
					employeeGeneralActivityForm = new EmployeeGeneralActivityForm();
				}
				return employeeGeneralActivityForm;
			}
		}

		public static EmployeeLeaveRequestForm EmployeeLeaveRequestFormObj
		{
			get
			{
				if (employeeLeaveRequestForm == null || employeeLeaveRequestForm.IsDisposed)
				{
					employeeLeaveRequestForm = new EmployeeLeaveRequestForm();
				}
				return employeeLeaveRequestForm;
			}
		}

		public static EmployeeLeaveApprovalForm EmployeeLeaveApprovalFormObj
		{
			get
			{
				if (employeeLeaveApprovalForm == null || employeeLeaveApprovalForm.IsDisposed)
				{
					employeeLeaveApprovalForm = new EmployeeLeaveApprovalForm();
				}
				return employeeLeaveApprovalForm;
			}
		}

		public static EmployeeResumptionForm EmployeeResumptionFormObj
		{
			get
			{
				if (employeeResumptionForm == null || employeeResumptionForm.IsDisposed)
				{
					employeeResumptionForm = new EmployeeResumptionForm();
				}
				return employeeResumptionForm;
			}
		}

		public static SalarySheetForm SalarySheetFormObj
		{
			get
			{
				if (salarySheetForm == null || salarySheetForm.IsDisposed)
				{
					salarySheetForm = new SalarySheetForm();
				}
				return salarySheetForm;
			}
		}

		public static PostSalarySheetForm PostSalarySheetFormObj
		{
			get
			{
				if (postSalarySheetForm == null || postSalarySheetForm.IsDisposed)
				{
					postSalarySheetForm = new PostSalarySheetForm();
				}
				return postSalarySheetForm;
			}
		}

		public static CashSalaryPaymentForm CashSalaryPaymentFormObj
		{
			get
			{
				if (cashSalaryPaymentForm == null || cashSalaryPaymentForm.IsDisposed)
				{
					cashSalaryPaymentForm = new CashSalaryPaymentForm();
				}
				return cashSalaryPaymentForm;
			}
		}

		public static ChequeSalaryPaymentForm ChequeSalaryPaymentFormObj
		{
			get
			{
				if (chequeSalaryPaymentForm == null || chequeSalaryPaymentForm.IsDisposed)
				{
					chequeSalaryPaymentForm = new ChequeSalaryPaymentForm();
				}
				return chequeSalaryPaymentForm;
			}
		}

		public static TransferSalaryPaymentForm TransferSalaryPaymentFormObj
		{
			get
			{
				if (transferSalaryPaymentForm == null || transferSalaryPaymentForm.IsDisposed)
				{
					transferSalaryPaymentForm = new TransferSalaryPaymentForm();
				}
				return transferSalaryPaymentForm;
			}
		}

		public static EmployeeLoanTypeDetailsForm EmployeeLoanTypeDetailsFormObj
		{
			get
			{
				if (employeeLoanTypeDetailsForm == null || employeeLoanTypeDetailsForm.IsDisposed)
				{
					employeeLoanTypeDetailsForm = new EmployeeLoanTypeDetailsForm();
				}
				return employeeLoanTypeDetailsForm;
			}
		}

		public static EmployeeLoanForm EmployeeLoanFormObj
		{
			get
			{
				if (employeeLoanForm == null || employeeLoanForm.IsDisposed)
				{
					employeeLoanForm = new EmployeeLoanForm();
				}
				return employeeLoanForm;
			}
		}

		public static EmployeeLoanSettlementForm EmployeeLoanSettlementFormObj
		{
			get
			{
				if (employeeLoanSettlementForm == null || employeeLoanSettlementForm.IsDisposed)
				{
					employeeLoanSettlementForm = new EmployeeLoanSettlementForm();
				}
				return employeeLoanSettlementForm;
			}
		}

		public static EmployeeBalanceSummaryReport EmployeeBalanceSummaryReportObj
		{
			get
			{
				if (employeeBalanceSummaryReport == null || employeeBalanceSummaryReport.IsDisposed)
				{
					employeeBalanceSummaryReport = new EmployeeBalanceSummaryReport();
				}
				return employeeBalanceSummaryReport;
			}
		}

		public static EmployeeBalanceDetailsReport EmployeeBalanceDetailsReportObj
		{
			get
			{
				if (employeeBalanceDetailsReport == null || employeeBalanceDetailsReport.IsDisposed)
				{
					employeeBalanceDetailsReport = new EmployeeBalanceDetailsReport();
				}
				return employeeBalanceDetailsReport;
			}
		}

		public static EmployeeSalaryReport EmployeeSalaryReportObj
		{
			get
			{
				if (employeeSalaryReport == null || employeeSalaryReport.IsDisposed)
				{
					employeeSalaryReport = new EmployeeSalaryReport();
				}
				return employeeSalaryReport;
			}
		}

		public static EmployeeListReport EmployeeListReportObj
		{
			get
			{
				if (employeeListReport == null || employeeListReport.IsDisposed)
				{
					employeeListReport = new EmployeeListReport();
				}
				return employeeListReport;
			}
		}

		public static EmployeeProfileReport EmployeeProfileReportObj
		{
			get
			{
				if (employeeProfileReport == null || employeeProfileReport.IsDisposed)
				{
					employeeProfileReport = new EmployeeProfileReport();
				}
				return employeeProfileReport;
			}
		}

		public static EmployeeActivityReport EmployeeActivityReportObj
		{
			get
			{
				if (employeeActivityReport == null || employeeActivityReport.IsDisposed)
				{
					employeeActivityReport = new EmployeeActivityReport();
				}
				return employeeActivityReport;
			}
		}

		public static LeadProfileReport LeadProfileReportObj
		{
			get
			{
				if (leadProfileReport == null || leadProfileReport.IsDisposed)
				{
					leadProfileReport = new LeadProfileReport();
				}
				return leadProfileReport;
			}
		}

		public static LeadBySourceReport LeadBySourceReportObj
		{
			get
			{
				if (leadBySourceReport == null || leadBySourceReport.IsDisposed)
				{
					leadBySourceReport = new LeadBySourceReport();
				}
				return leadBySourceReport;
			}
		}

		public static UpcomingOpportunitiesReport UpcomingOppReportObj
		{
			get
			{
				if (upcomingOppReport == null || upcomingOppReport.IsDisposed)
				{
					upcomingOppReport = new UpcomingOpportunitiesReport();
				}
				return upcomingOppReport;
			}
		}

		public static UpcomingEventsReport UpcomingEventsReportObj
		{
			get
			{
				if (upcomingEventsReport == null || upcomingEventsReport.IsDisposed)
				{
					upcomingEventsReport = new UpcomingEventsReport();
				}
				return upcomingEventsReport;
			}
		}

		public static CustomerStatementForm CustomerStatementFormObj
		{
			get
			{
				if (customerStatementForm == null || customerStatementForm.IsDisposed)
				{
					customerStatementForm = new CustomerStatementForm();
				}
				return customerStatementForm;
			}
		}

		public static UnallocatedPaymentsListForm UnallocatedCustomerPaymentsListFormObj
		{
			get
			{
				if (unallocatedCustomerPaymentsListForm == null || unallocatedCustomerPaymentsListForm.IsDisposed)
				{
					unallocatedCustomerPaymentsListForm = new UnallocatedPaymentsListForm();
					unallocatedCustomerPaymentsListForm.Name = "CustomerPaymentAllocationList";
				}
				return unallocatedCustomerPaymentsListForm;
			}
		}

		public static UnallocatedPaymentsListForm UnallocatedVendorPaymentsListFormObj
		{
			get
			{
				if (unallocatedVendorPaymentsListForm == null || unallocatedVendorPaymentsListForm.IsDisposed)
				{
					unallocatedVendorPaymentsListForm = new UnallocatedPaymentsListForm();
					unallocatedVendorPaymentsListForm.IsARPayment = false;
					unallocatedVendorPaymentsListForm.Name = "VendorPaymentAllocationList";
				}
				return unallocatedVendorPaymentsListForm;
			}
		}

		public static CustomerAgingSummaryReport CustomerAgingSummaryReportObj
		{
			get
			{
				if (customerAgingSummaryReport == null || customerAgingSummaryReport.IsDisposed)
				{
					customerAgingSummaryReport = new CustomerAgingSummaryReport();
				}
				return customerAgingSummaryReport;
			}
		}

		public static VendorAgingSummaryReport VendorAgingSummaryReportObj
		{
			get
			{
				if (vendorAgingSummaryReport == null || vendorAgingSummaryReport.IsDisposed)
				{
					vendorAgingSummaryReport = new VendorAgingSummaryReport();
				}
				return vendorAgingSummaryReport;
			}
		}

		public static VendorStatementForm VendorStatementFormObj
		{
			get
			{
				if (vendorStatementForm == null || vendorStatementForm.IsDisposed)
				{
					vendorStatementForm = new VendorStatementForm();
				}
				return vendorStatementForm;
			}
		}

		public static UserDetailsForm UserDetailsFormObj
		{
			get
			{
				if (userDetailsForm == null || userDetailsForm.IsDisposed)
				{
					userDetailsForm = new UserDetailsForm();
				}
				return userDetailsForm;
			}
		}

		public static UserGroupDetailsForm UserGroupDetailsFormObj
		{
			get
			{
				if (userGroupDetailsForm == null || userGroupDetailsForm.IsDisposed)
				{
					userGroupDetailsForm = new UserGroupDetailsForm();
				}
				return userGroupDetailsForm;
			}
		}

		public static PrintChequeForm PrintChequeFormObj
		{
			get
			{
				if (printChequeForm == null || printChequeForm.IsDisposed)
				{
					printChequeForm = new PrintChequeForm();
				}
				return printChequeForm;
			}
		}

		public static ReminderListForm ReminderListFormObj
		{
			get
			{
				if (reminderListForm == null || reminderListForm.IsDisposed)
				{
					reminderListForm = new ReminderListForm();
				}
				return reminderListForm;
			}
		}

		public static CustomerListForm CustomerListFormObj
		{
			get
			{
				if (customerListForm == null || customerListForm.IsDisposed)
				{
					customerListForm = new CustomerListForm();
				}
				return customerListForm;
			}
		}

		public static TransactionListForm ContainerTrackingListFormObj
		{
			get
			{
				if (containerTrackingListForm == null || containerTrackingListForm.IsDisposed)
				{
					containerTrackingListForm = new TransactionListForm();
					containerTrackingListForm.ListType = TransactionListType.ContainerTracking;
					containerTrackingListForm.Text = "Container Tracking";
					containerTrackingListForm.Name = "ContainerTrackingListForm";
				}
				return containerTrackingListForm;
			}
		}

		public static VendorListForm VendorListFormObj
		{
			get
			{
				if (vendorListForm == null || vendorListForm.IsDisposed)
				{
					vendorListForm = new VendorListForm();
				}
				return vendorListForm;
			}
		}

		public static LeadListForm LeadListFormObj
		{
			get
			{
				if (leadListForm == null || leadListForm.IsDisposed)
				{
					leadListForm = new LeadListForm();
				}
				return leadListForm;
			}
		}

		public static LeadStatusForm LeadStatusFormObj
		{
			get
			{
				if (leadStatusForm == null || leadStatusForm.IsDisposed)
				{
					leadStatusForm = new LeadStatusForm();
				}
				return leadStatusForm;
			}
		}

		public static EmployeeListForm EmployeeListFormObj
		{
			get
			{
				if (employeeListForm == null || employeeListForm.IsDisposed)
				{
					employeeListForm = new EmployeeListForm();
				}
				return employeeListForm;
			}
		}

		public static ProformaInvoiceForm ProformaInvoiceFormObj
		{
			get
			{
				if (proformaInvoiceForm == null || proformaInvoiceForm.IsDisposed)
				{
					proformaInvoiceForm = new ProformaInvoiceForm();
				}
				return proformaInvoiceForm;
			}
		}

		public static UnallocatedPurchasePrePaymentListForm UnallocatePrePaymentFormObj
		{
			get
			{
				if (unallocatPurchasePrePaymentList == null || unallocatPurchasePrePaymentList.IsDisposed)
				{
					unallocatPurchasePrePaymentList = new UnallocatedPurchasePrePaymentListForm();
					unallocatPurchasePrePaymentList.IsARPayment = true;
					unallocatPurchasePrePaymentList.Name = "UnallocatedPurchasePrepaymentListForm";
				}
				return unallocatPurchasePrePaymentList;
			}
		}

		public static FiscalYearDetailsForm FiscalYearDetailsFormObj
		{
			get
			{
				if (fiscalYearDetailsForm == null || fiscalYearDetailsForm.IsDisposed)
				{
					fiscalYearDetailsForm = new FiscalYearDetailsForm();
				}
				return fiscalYearDetailsForm;
			}
		}

		public static CustomerAgingListForm CustomerAgingListFormObj
		{
			get
			{
				if (customerAgingListForm == null || customerAgingListForm.IsDisposed)
				{
					customerAgingListForm = new CustomerAgingListForm();
				}
				return customerAgingListForm;
			}
		}

		public static VendorAgingListForm VendorAgingListFormObj
		{
			get
			{
				if (vendorAgingListForm == null || vendorAgingListForm.IsDisposed)
				{
					vendorAgingListForm = new VendorAgingListForm();
				}
				return vendorAgingListForm;
			}
		}

		public static TTPaymentForm TTPaymentFormObj
		{
			get
			{
				if (ttPaymentForm == null || ttPaymentForm.IsDisposed)
				{
					ttPaymentForm = new TTPaymentForm();
				}
				return ttPaymentForm;
			}
		}

		public static TTReceiptForm TTReceiptFormObj
		{
			get
			{
				if (ttReceiptForm == null || ttReceiptForm.IsDisposed)
				{
					ttReceiptForm = new TTReceiptForm();
				}
				return ttReceiptForm;
			}
		}

		public static LeadDetailsForm LeadDetailsFormObj
		{
			get
			{
				if (leadDetailsForm == null || leadDetailsForm.IsDisposed)
				{
					leadDetailsForm = new LeadDetailsForm();
				}
				return leadDetailsForm;
			}
		}

		public static LeadAddressDetailsForm LeadAddressDetailsFormObj
		{
			get
			{
				if (leadAddressDetailsForm == null || leadAddressDetailsForm.IsDisposed)
				{
					leadAddressDetailsForm = new LeadAddressDetailsForm();
				}
				return leadAddressDetailsForm;
			}
		}

		public static EmployeePassportControlForm EmployeePassportControlFormObj
		{
			get
			{
				if (employeePassportControlForm == null || employeePassportControlForm.IsDisposed)
				{
					employeePassportControlForm = new EmployeePassportControlForm();
				}
				return employeePassportControlForm;
			}
		}

		public static ProductUPCGenerator ProductUPCGeneratorObj
		{
			get
			{
				if (productUPCGenerator == null || productUPCGenerator.IsDisposed)
				{
					productUPCGenerator = new ProductUPCGenerator();
				}
				return productUPCGenerator;
			}
		}

		public static ProjectSubcontractPOForm ProjectSubContractPOFormObj
		{
			get
			{
				if (projectSubContractPOForm == null || projectSubContractPOForm.IsDisposed)
				{
					projectSubContractPOForm = new ProjectSubcontractPOForm();
				}
				return projectSubContractPOForm;
			}
		}

		public static ProjectSubContractPIForm ProjectSubContractPIFormObj
		{
			get
			{
				if (projectSubContractPIForm == null || projectSubContractPIForm.IsDisposed)
				{
					projectSubContractPIForm = new ProjectSubContractPIForm();
				}
				return projectSubContractPIForm;
			}
		}

		public static HRLetterReportForm LastMonthSalaryReportFormObj
		{
			get
			{
				if (lastMonthSalaryReport == null || lastMonthSalaryReport.IsDisposed)
				{
					lastMonthSalaryReport = new HRLetterReportForm();
					lastMonthSalaryReport.ReportType = "Last Month Salary";
					lastMonthSalaryReport.Text = "Last Month Salary";
				}
				return lastMonthSalaryReport;
			}
		}

		public static HRLetterReportForm SalaryCertificateReportFormObj
		{
			get
			{
				if (salaryCertificateReport == null || salaryCertificateReport.IsDisposed)
				{
					salaryCertificateReport = new HRLetterReportForm();
					salaryCertificateReport.ReportType = "Salary Certificate Report";
					salaryCertificateReport.Text = "Salary Certificate Report";
				}
				return salaryCertificateReport;
			}
		}

		public static HRLetterReportForm ConfirmationLetterReportFormObj
		{
			get
			{
				if (confirmationLetterReport == null || confirmationLetterReport.IsDisposed)
				{
					confirmationLetterReport = new HRLetterReportForm();
					confirmationLetterReport.ReportType = "Confirmation Letter Report";
					confirmationLetterReport.Text = "Confirmation Letter Report";
				}
				return confirmationLetterReport;
			}
		}

		public static HRLetterReportForm AppointmentLetterReportFormObj
		{
			get
			{
				if (appointmentLetterReport == null || appointmentLetterReport.IsDisposed)
				{
					appointmentLetterReport = new HRLetterReportForm();
					appointmentLetterReport.ReportType = "Appointment Letter Report";
					appointmentLetterReport.Text = "Appointment Letter Report";
				}
				return appointmentLetterReport;
			}
		}

		public static HRLetterReportForm SalaryIncrementLetterReportFormObj
		{
			get
			{
				if (salaryIncrementLetterReport == null || salaryIncrementLetterReport.IsDisposed)
				{
					salaryIncrementLetterReport = new HRLetterReportForm();
					salaryIncrementLetterReport.ReportType = "Salary Increment Letter Report";
					salaryIncrementLetterReport.Text = "Salary Increment Letter Report";
				}
				return salaryIncrementLetterReport;
			}
		}

		public static HRLetterReportForm ExperienceCertificateReportFormObj
		{
			get
			{
				if (experienceCertificateReport == null || experienceCertificateReport.IsDisposed)
				{
					experienceCertificateReport = new HRLetterReportForm();
					experienceCertificateReport.ReportType = "Experience Certificate Report";
					experienceCertificateReport.Text = "Experience Certificate Report";
				}
				return experienceCertificateReport;
			}
		}

		public static SalesInvoiceNonInvForm SalesInvoiceNIFormObj
		{
			get
			{
				if (salesInvoiceNIForm == null || salesInvoiceNIForm.IsDisposed)
				{
					salesInvoiceNIForm = new SalesInvoiceNonInvForm();
					salesInvoiceNIForm.Text = "Sales Invoice (Non Inventory)";
				}
				return salesInvoiceNIForm;
			}
		}

		public static EditLotDetailsForm UpdateLotDetailsFormObj
		{
			get
			{
				if (updateLotDetailsForm == null || updateLotDetailsForm.IsDisposed)
				{
					updateLotDetailsForm = new EditLotDetailsForm();
					updateLotDetailsForm.Text = "Update Lot Details";
				}
				return updateLotDetailsForm;
			}
		}

		public static PropertyServiceInvoice PropertyServiceInvoiceFormObj
		{
			get
			{
				if (propertyServiceInvoiceForm == null || propertyServiceInvoiceForm.IsDisposed)
				{
					propertyServiceInvoiceForm = new PropertyServiceInvoice();
					propertyServiceInvoiceForm.Text = "Property Service Invoice";
				}
				return propertyServiceInvoiceForm;
			}
		}

		public static RecurringTransactionPostForm RecurringTransactionPostFormObj
		{
			get
			{
				if (recurringTransactionPostForm == null || recurringTransactionPostForm.IsDisposed)
				{
					recurringTransactionPostForm = new RecurringTransactionPostForm();
					recurringTransactionPostForm.Text = "Recurring Transaction Post";
				}
				return recurringTransactionPostForm;
			}
		}

		public static EmployeeAbscondingEntryForm EmployeeAbscondingEntryFormObj
		{
			get
			{
				if (employeeAbscondingEntryForm == null || employeeAbscondingEntryForm.IsDisposed)
				{
					employeeAbscondingEntryForm = new EmployeeAbscondingEntryForm();
					employeeAbscondingEntryForm.Text = "Employee Absconding Entry";
				}
				return employeeAbscondingEntryForm;
			}
		}

		public static PatientForm PatientFormObj
		{
			get
			{
				if (patientForm == null || patientForm.IsDisposed)
				{
					patientForm = new PatientForm();
					patientForm.Text = "Patient Details";
				}
				return patientForm;
			}
		}

		public static RouteDetailsForm RouteDetailsFormObj
		{
			get
			{
				if (routeDetailsForm == null || routeDetailsForm.IsDisposed)
				{
					routeDetailsForm = new RouteDetailsForm();
				}
				return routeDetailsForm;
			}
		}

		public static RouteGroupDetailsForm RouteGroupDetailsFormObj
		{
			get
			{
				if (routeGroupDetailsForm == null || routeGroupDetailsForm.IsDisposed)
				{
					routeGroupDetailsForm = new RouteGroupDetailsForm();
					routeGroupDetailsForm.Text = "Route Group";
				}
				return routeGroupDetailsForm;
			}
		}

		public static ProductionDetailsForm ProductionDetailsFormObj
		{
			get
			{
				if (productionDetailsForm == null || productionDetailsForm.IsDisposed)
				{
					productionDetailsForm = new ProductionDetailsForm();
					productionDetailsForm.Text = "Production";
				}
				return productionDetailsForm;
			}
		}

		public static UpdateTREntryDetailsForm updateTREntryDetailsFormObj
		{
			get
			{
				if (updateTREntryDetailsForm == null || updateTREntryDetailsForm.IsDisposed)
				{
					updateTREntryDetailsForm = new UpdateTREntryDetailsForm();
					updateTREntryDetailsForm.Text = "Update TR Entry ";
				}
				return updateTREntryDetailsForm;
			}
		}

		public static DataSyncForm dataSyncFormObj
		{
			get
			{
				if (dataSyncForm == null || dataSyncForm.IsDisposed)
				{
					dataSyncForm = new DataSyncForm();
					dataSyncForm.Text = "Data Sync";
				}
				return dataSyncForm;
			}
		}

		public static DataSyncSetupDetailsForm DataSyncSetupDetailsFormObj
		{
			get
			{
				if (dataSyncSetupDetailsForm == null || dataSyncSetupDetailsForm.IsDisposed)
				{
					dataSyncSetupDetailsForm = new DataSyncSetupDetailsForm();
					dataSyncSetupDetailsForm.Text = "Data Sync Setup";
				}
				return dataSyncSetupDetailsForm;
			}
		}

		public static FixedAssetPurchaseOrderForm FixedAssetPurchaseOrderFormObj
		{
			get
			{
				if (fixedAssetPurchaseOrderForm == null || fixedAssetPurchaseOrderForm.IsDisposed)
				{
					fixedAssetPurchaseOrderForm = new FixedAssetPurchaseOrderForm();
					fixedAssetPurchaseOrderForm.Text = "Fixed Asset Purchase Order";
				}
				return fixedAssetPurchaseOrderForm;
			}
		}

		public static GenericListDetailsForm LeadStatusDetailsFormObj
		{
			get
			{
				if (leadStatusDetailsForm == null || leadStatusDetailsForm.IsDisposed)
				{
					leadStatusDetailsForm = new GenericListDetailsForm();
					leadStatusDetailsForm.Name = "LeadStatusDetailsForm";
					leadStatusDetailsForm.Text = "Lead Status";
					leadStatusDetailsForm.IsSingleColumn = true;
					leadStatusDetailsForm.GenericListType = GenericListTypes.LeadStatus;
				}
				return leadStatusDetailsForm;
			}
		}

		public static GenericListDetailsForm IndustryDetailsFormObj
		{
			get
			{
				if (industryDetailsForm == null || industryDetailsForm.IsDisposed)
				{
					industryDetailsForm = new GenericListDetailsForm();
					industryDetailsForm.Name = "IndustryDetailsForm";
					industryDetailsForm.Text = "Industry";
					industryDetailsForm.GenericListType = GenericListTypes.Industry;
				}
				return industryDetailsForm;
			}
		}

		public static GenericListDetailsForm LeadSourceDetailsFormObj
		{
			get
			{
				if (leadSourceDetailsForm == null || leadSourceDetailsForm.IsDisposed)
				{
					leadSourceDetailsForm = new GenericListDetailsForm();
					leadSourceDetailsForm.Name = "LeadSourceDetailsForm";
					leadSourceDetailsForm.Text = "Lead Source";
					leadSourceDetailsForm.GenericListType = GenericListTypes.LeadSource;
				}
				return leadSourceDetailsForm;
			}
		}

		public static GenericListDetailsForm CollateralTypeDetailsForm
		{
			get
			{
				if (collateralTypeDetailsForm == null || collateralTypeDetailsForm.IsDisposed)
				{
					collateralTypeDetailsForm = new GenericListDetailsForm();
					collateralTypeDetailsForm.Name = "CollateralTypeDetailsForm";
					collateralTypeDetailsForm.Text = "Collateral Type";
					collateralTypeDetailsForm.GenericListType = GenericListTypes.CollateralType;
				}
				return collateralTypeDetailsForm;
			}
		}

		public static GenericListDetailsForm SalesReturnReasonForm
		{
			get
			{
				if (salesReturnReasonForm == null || salesReturnReasonForm.IsDisposed)
				{
					salesReturnReasonForm = new GenericListDetailsForm();
					salesReturnReasonForm.Name = "SalesReturnReasonForm";
					salesReturnReasonForm.Text = "Sales Return Reason";
					salesReturnReasonForm.GenericListType = GenericListTypes.SalesReturnReason;
				}
				return salesReturnReasonForm;
			}
		}

		public static GenericListDetailsForm VehicleTypeForm
		{
			get
			{
				if (vehicleTypeForm == null || vehicleTypeForm.IsDisposed)
				{
					vehicleTypeForm = new GenericListDetailsForm();
					vehicleTypeForm.Name = "vehicleTypeForm";
					vehicleTypeForm.Text = "Vehicle Type";
					vehicleTypeForm.GenericListType = GenericListTypes.VehicleType;
				}
				return vehicleTypeForm;
			}
		}

		public static GenericListDetailsForm QCInspectorDetailsForm
		{
			get
			{
				if (qcInspectorDetailsForm == null || qcInspectorDetailsForm.IsDisposed)
				{
					qcInspectorDetailsForm = new GenericListDetailsForm();
					qcInspectorDetailsForm.Name = "QCInspectorDetailsForm";
					qcInspectorDetailsForm.Text = "QC Inspector";
					qcInspectorDetailsForm.GenericListType = GenericListTypes.QCInspector;
				}
				return qcInspectorDetailsForm;
			}
		}

		public static GenericListDetailsForm PropertyUnitTypeForm
		{
			get
			{
				if (propertyUnitTypeForm == null || propertyUnitTypeForm.IsDisposed)
				{
					propertyUnitTypeForm = new GenericListDetailsForm();
					propertyUnitTypeForm.Name = "PropertyUnitTypeForm";
					propertyUnitTypeForm.Text = "Property Unit Type";
					propertyUnitTypeForm.GenericListType = GenericListTypes.PropertyUnitType;
				}
				return propertyUnitTypeForm;
			}
		}

		public static GenericListDetailsForm EmployeeAddressTypeForm
		{
			get
			{
				if (employeeAddressTypeForm == null || employeeAddressTypeForm.IsDisposed)
				{
					employeeAddressTypeForm = new GenericListDetailsForm();
					employeeAddressTypeForm.Name = "EmployeeAddressTypeForm";
					employeeAddressTypeForm.Text = "EmployeeAddress Type";
					employeeAddressTypeForm.GenericListType = GenericListTypes.EmployeeAddressType;
				}
				return employeeAddressTypeForm;
			}
		}

		public static GenericListDetailsForm PropertyFacilityForm
		{
			get
			{
				if (propertyFacilityForm == null || propertyFacilityForm.IsDisposed)
				{
					propertyFacilityForm = new GenericListDetailsForm();
					propertyFacilityForm.Name = "PropertyFacilityForm";
					propertyFacilityForm.Text = "Property Facility";
					propertyFacilityForm.GenericListType = GenericListTypes.PropertyFacility;
				}
				return propertyFacilityForm;
			}
		}

		public static GenericListDetailsForm PropertyViewForm
		{
			get
			{
				if (propertyViewForm == null || propertyViewForm.IsDisposed)
				{
					propertyViewForm = new GenericListDetailsForm();
					propertyViewForm.Name = "PropertyViewForm";
					propertyViewForm.Text = "Property View";
					propertyViewForm.GenericListType = GenericListTypes.PropertyView;
				}
				return propertyViewForm;
			}
		}

		public static GenericListDetailsForm PropertyOwnerForm
		{
			get
			{
				if (propertyOwnerForm == null || propertyOwnerForm.IsDisposed)
				{
					propertyOwnerForm = new GenericListDetailsForm();
					propertyOwnerForm.Name = "PropertyOwnerForm";
					propertyOwnerForm.Text = "Property Owner";
					propertyOwnerForm.GenericListType = GenericListTypes.PropertyOwner;
				}
				return propertyOwnerForm;
			}
		}

		public static ChequeReceiptMultiEntryForm ChequeReceiptMultiEntryFormObj
		{
			get
			{
				if (chequeReceiptMultiEntryForm == null || chequeReceiptMultiEntryForm.IsDisposed)
				{
					chequeReceiptMultiEntryForm = new ChequeReceiptMultiEntryForm();
				}
				return chequeReceiptMultiEntryForm;
			}
		}

		public static LoanEntryForm LoanEntryFormObj
		{
			get
			{
				if (loanEntryForm == null || loanEntryForm.IsDisposed)
				{
					loanEntryForm = new LoanEntryForm();
				}
				return loanEntryForm;
			}
		}

		public static GenericListDetailsForm PropertyServiceTypeForm
		{
			get
			{
				if (propertyServiceTypeForm == null || propertyServiceTypeForm.IsDisposed)
				{
					propertyServiceTypeForm = new GenericListDetailsForm();
					propertyServiceTypeForm.Name = "propertyServiceTypeForm";
					propertyServiceTypeForm.Text = "Property Service Type";
					propertyServiceTypeForm.GenericListType = GenericListTypes.PropertyServiceType;
				}
				return propertyServiceTypeForm;
			}
		}

		public static GenericListDetailsForm FixedAssetCompanyForm
		{
			get
			{
				if (fixedAssetCompanyForm == null || fixedAssetCompanyForm.IsDisposed)
				{
					fixedAssetCompanyForm = new GenericListDetailsForm();
					fixedAssetCompanyForm.Name = "FixedAssetCompanyForm";
					fixedAssetCompanyForm.Text = "Fixed Asset Company";
					fixedAssetCompanyForm.GenericListType = GenericListTypes.FixedAssetCompany;
				}
				return fixedAssetCompanyForm;
			}
		}

		public static GenericListDetailsForm MedicalInsuranceCategoryForm
		{
			get
			{
				if (medicalInsuranceCategoryForm == null || medicalInsuranceCategoryForm.IsDisposed)
				{
					medicalInsuranceCategoryForm = new GenericListDetailsForm();
					medicalInsuranceCategoryForm.Name = "MedicalInsuranceCategoryForm";
					medicalInsuranceCategoryForm.Text = "Medical Insurance Category";
					medicalInsuranceCategoryForm.GenericListType = GenericListTypes.MedicalInsuranceCategory;
				}
				return medicalInsuranceCategoryForm;
			}
		}

		public static GenericListDetailsForm HorseOwnershipTypeFormObj
		{
			get
			{
				if (horseOwnershipTypeForm == null || horseOwnershipTypeForm.IsDisposed)
				{
					horseOwnershipTypeForm = new GenericListDetailsForm();
					horseOwnershipTypeForm.Name = "HorseOwnershipTypeForm";
					horseOwnershipTypeForm.Text = "Horse Ownership Type";
					horseOwnershipTypeForm.GenericListType = GenericListTypes.HorseOwnershipType;
				}
				return horseOwnershipTypeForm;
			}
		}

		public static GenericListDetailsForm HorseCategoryFormObj
		{
			get
			{
				if (horseCategoryForm == null || horseCategoryForm.IsDisposed)
				{
					horseCategoryForm = new GenericListDetailsForm();
					horseCategoryForm.Name = "HorseCategoryForm";
					horseCategoryForm.Text = "Horse Category";
					horseCategoryForm.GenericListType = GenericListTypes.HorseCategory;
				}
				return horseCategoryForm;
			}
		}

		public static GenericListDetailsForm ContainerTypeFormObj
		{
			get
			{
				if (containerTypeForm == null || containerTypeForm.IsDisposed)
				{
					containerTypeForm = new GenericListDetailsForm();
					containerTypeForm.Name = "ConatinerTypeForm";
					containerTypeForm.Text = "Container Type";
					containerTypeForm.GenericListType = GenericListTypes.ContainerType;
				}
				return containerTypeForm;
			}
		}

		public static GenericListDetailsForm VehicleMakeFormObj
		{
			get
			{
				if (vehicleMakeForm == null || vehicleMakeForm.IsDisposed)
				{
					vehicleMakeForm = new GenericListDetailsForm();
					vehicleMakeForm.Name = "VehicleMakeForm";
					vehicleMakeForm.Text = "Vehicle Make";
					vehicleMakeForm.GenericListType = GenericListTypes.VehicleMake;
				}
				return vehicleMakeForm;
			}
		}

		public static GenericListDetailsForm PartsMakeTypeFormObj
		{
			get
			{
				if (partsMakeTypeForm == null || partsMakeTypeForm.IsDisposed)
				{
					partsMakeTypeForm = new GenericListDetailsForm();
					partsMakeTypeForm.Name = "PartsMakeTypeForm";
					partsMakeTypeForm.Text = "Parts Make Type";
					partsMakeTypeForm.GenericListType = GenericListTypes.PartsMakeType;
				}
				return partsMakeTypeForm;
			}
		}

		public static GenericListDetailsForm PartsTypeFormObj
		{
			get
			{
				if (partsTypeForm == null || partsTypeForm.IsDisposed)
				{
					partsTypeForm = new GenericListDetailsForm();
					partsTypeForm.Name = "PartsTypeForm";
					partsTypeForm.Text = "Parts Type";
					partsTypeForm.GenericListType = GenericListTypes.PartsType;
				}
				return partsTypeForm;
			}
		}

		public static GenericListDetailsForm PartsFamilyFormObj
		{
			get
			{
				if (partsFamilyForm == null || partsFamilyForm.IsDisposed)
				{
					partsFamilyForm = new GenericListDetailsForm();
					partsFamilyForm.Name = "PartsFamilyForm";
					partsFamilyForm.Text = "Parts Family";
					partsFamilyForm.GenericListType = GenericListTypes.PartsFamily;
				}
				return partsFamilyForm;
			}
		}

		public static GenericListDetailsForm VehicleModelFormObj
		{
			get
			{
				if (vehicleModelForm == null || vehicleModelForm.IsDisposed)
				{
					vehicleModelForm = new GenericListDetailsForm();
					vehicleModelForm.Name = "VehicleModelForm";
					vehicleModelForm.Text = "Vehicle Model";
					vehicleModelForm.GenericListType = GenericListTypes.VehicleModel;
				}
				return vehicleModelForm;
			}
		}

		public static GenericListDetailsForm ShippingCompanyFormObj
		{
			get
			{
				if (shippingCompanyForm == null || shippingCompanyForm.IsDisposed)
				{
					shippingCompanyForm = new GenericListDetailsForm();
					shippingCompanyForm.Name = "ShippingCompanyForm";
					shippingCompanyForm.Text = "Shipping Company";
					shippingCompanyForm.GenericListType = GenericListTypes.ShippingCompany;
				}
				return shippingCompanyForm;
			}
		}

		public static GenericListDetailsForm KitchenTypeFormObj
		{
			get
			{
				if (kitchenTypeForm == null || kitchenTypeForm.IsDisposed)
				{
					kitchenTypeForm = new GenericListDetailsForm();
					kitchenTypeForm.Name = "KitchenTypeForm";
					kitchenTypeForm.Text = "Kitchen Type";
					kitchenTypeForm.GenericListType = GenericListTypes.KitchenType;
				}
				return kitchenTypeForm;
			}
		}

		public static EquipmentCategoryForm EquipmentCategoryFormObj
		{
			get
			{
				if (equipmentCategoryForm == null || equipmentCategoryForm.IsDisposed)
				{
					equipmentCategoryForm = new EquipmentCategoryForm();
					equipmentCategoryForm.Name = "EquipmentCategoryForm";
					equipmentCategoryForm.Text = "Equipment Category";
				}
				return equipmentCategoryForm;
			}
		}

		public static EquipmentTypeForm EquipmentTypeFormObj
		{
			get
			{
				if (equipmenttypeForm == null || equipmenttypeForm.IsDisposed)
				{
					equipmenttypeForm = new EquipmentTypeForm();
					equipmenttypeForm.Name = "EquipmentTypeForm";
					equipmenttypeForm.Text = "Equipment Type";
				}
				return equipmenttypeForm;
			}
		}

		public static RequisitionTypeForm RequisitionTypeFormObj
		{
			get
			{
				if (requisitionTypeForm == null || requisitionTypeForm.IsDisposed)
				{
					requisitionTypeForm = new RequisitionTypeForm();
					requisitionTypeForm.Name = "RequisitionTypeForm";
					requisitionTypeForm.Text = "Requisition Type";
				}
				return requisitionTypeForm;
			}
		}

		public static ContainerStatusChangingForm StatusChangeFormObj
		{
			get
			{
				if (statusChangeForm == null || statusChangeForm.IsDisposed)
				{
					statusChangeForm = new ContainerStatusChangingForm();
					statusChangeForm.Name = "ContainerStatusChangingForm";
					statusChangeForm.Text = "Container Status change";
				}
				return statusChangeForm;
			}
		}

		public static UDFSetupForm UDFSetupFormObj
		{
			get
			{
				if (udfSetupForm == null || udfSetupForm.IsDisposed)
				{
					udfSetupForm = new UDFSetupForm();
				}
				return udfSetupForm;
			}
		}

		public static TaxEntryForm TaxEntryFormObj
		{
			get
			{
				if (taxEntryForm == null || taxEntryForm.IsDisposed)
				{
					taxEntryForm = new TaxEntryForm();
					taxEntryForm.Name = "TaxEntryForm";
					taxEntryForm.Text = "Tax Item";
				}
				return taxEntryForm;
			}
		}

		public static PurchasePrePaymentInvoiceForm PurchasePrepaymentInvoiceFormObj
		{
			get
			{
				if (purchasePrepaymentInvoiceForm == null || purchasePrepaymentInvoiceForm.IsDisposed)
				{
					purchasePrepaymentInvoiceForm = new PurchasePrePaymentInvoiceForm();
					purchasePrepaymentInvoiceForm.Name = "PurchasePrepaymentInvoiceForm";
					purchasePrepaymentInvoiceForm.Text = "Purchase Prepayment Invoice";
				}
				return purchasePrepaymentInvoiceForm;
			}
		}

		public static CustomerOutstandingInvoicesReport CustomerOutstandingInvoicesReportObj
		{
			get
			{
				if (customerOutstandingInvoicesReport == null || customerOutstandingInvoicesReport.IsDisposed)
				{
					customerOutstandingInvoicesReport = new CustomerOutstandingInvoicesReport();
				}
				return customerOutstandingInvoicesReport;
			}
		}

		public static MatrixProductStockListReport MatrixProductStockListReportObj
		{
			get
			{
				if (matrixProductStockListReport == null || matrixProductStockListReport.IsDisposed)
				{
					matrixProductStockListReport = new MatrixProductStockListReport();
				}
				return matrixProductStockListReport;
			}
		}

		public static BuildAssemblyForm BuildAssemblyFormObj
		{
			get
			{
				if (buildAssemblyForm == null || buildAssemblyForm.IsDisposed)
				{
					buildAssemblyForm = new BuildAssemblyForm();
				}
				return buildAssemblyForm;
			}
		}

		public static InventoryRepackingForm InventoryRepackingFormObj
		{
			get
			{
				if (inventoryRepackingForm == null || inventoryRepackingForm.IsDisposed)
				{
					inventoryRepackingForm = new InventoryRepackingForm();
				}
				return inventoryRepackingForm;
			}
		}

		public static BOMDetailsForm BOMDetailsFormObj
		{
			get
			{
				if (bomDetailsForm == null || bomDetailsForm.IsDisposed)
				{
					bomDetailsForm = new BOMDetailsForm();
				}
				return bomDetailsForm;
			}
		}

		public static EmployeeSalarySlipBulkMailForm EmployeeSalarySlipBulkMailFormObj
		{
			get
			{
				if (employeeSalarySlipBulkMailForm == null || employeeSalarySlipBulkMailForm.IsDisposed)
				{
					employeeSalarySlipBulkMailForm = new EmployeeSalarySlipBulkMailForm();
				}
				return employeeSalarySlipBulkMailForm;
			}
		}

		public static JobBOMDetailsForm JobBOMDetailsFormObj
		{
			get
			{
				if (JobbomDetailsForm == null || JobbomDetailsForm.IsDisposed)
				{
					JobbomDetailsForm = new JobBOMDetailsForm();
				}
				return JobbomDetailsForm;
			}
		}

		public static PackageDetailsForm PackageDetailsFormObj
		{
			get
			{
				if (packageDetailsForm == null || packageDetailsForm.IsDisposed)
				{
					packageDetailsForm = new PackageDetailsForm();
				}
				return packageDetailsForm;
			}
		}

		public static EOSRuleDetailsForm EOSRuleDetailsFormObj
		{
			get
			{
				if (eosRuleDetailsForm == null || eosRuleDetailsForm.IsDisposed)
				{
					eosRuleDetailsForm = new EOSRuleDetailsForm();
				}
				return eosRuleDetailsForm;
			}
		}

		public static OverTimeDetailsForm OverTimeDetailsFormObj
		{
			get
			{
				if (overTimeDetailsForm == null || overTimeDetailsForm.IsDisposed)
				{
					overTimeDetailsForm = new OverTimeDetailsForm();
				}
				return overTimeDetailsForm;
			}
		}

		public static EmployeeLoanPaymentForm EmployeeLoanPaymentFormObj
		{
			get
			{
				if (employeeLoanPaymentForm == null || employeeLoanPaymentForm.IsDisposed)
				{
					employeeLoanPaymentForm = new EmployeeLoanPaymentForm();
				}
				return employeeLoanPaymentForm;
			}
		}

		public static PropertyDetailsForm PropertyDetailsFormObj
		{
			get
			{
				if (propertyDetailsForm == null || propertyDetailsForm.IsDisposed)
				{
					propertyDetailsForm = new PropertyDetailsForm();
				}
				return propertyDetailsForm;
			}
		}

		public static PropertyDocTypeDetailsForm PropertyDocTypeFormObj
		{
			get
			{
				if (propertyDocTypeFormObj == null || propertyDocTypeFormObj.IsDisposed)
				{
					propertyDocTypeFormObj = new PropertyDocTypeDetailsForm();
				}
				return propertyDocTypeFormObj;
			}
		}

		public static PropertyTenantDocTypeDetailsForm PropertyTenantDocTypeFormObj
		{
			get
			{
				if (propertyTenantDocTypeFormObj == null || propertyTenantDocTypeFormObj.IsDisposed)
				{
					propertyTenantDocTypeFormObj = new PropertyTenantDocTypeDetailsForm();
				}
				return propertyTenantDocTypeFormObj;
			}
		}

		public static PropertyUnitDetailsForm PropertyUnitDetailsFormObj
		{
			get
			{
				if (propertyUnitDetailsForm == null || propertyUnitDetailsForm.IsDisposed)
				{
					propertyUnitDetailsForm = new PropertyUnitDetailsForm();
				}
				return propertyUnitDetailsForm;
			}
		}

		public static VirtualUnitDetailsForm VirtualUnitDetailsFormObj
		{
			get
			{
				if (virtualUnitDetailsForm == null || virtualUnitDetailsForm.IsDisposed)
				{
					virtualUnitDetailsForm = new VirtualUnitDetailsForm();
				}
				return virtualUnitDetailsForm;
			}
		}

		public static GeneralListForm AccountGroupListFormObj
		{
			get
			{
				if (accountGroupListForm == null || accountGroupListForm.IsDisposed)
				{
					accountGroupListForm = new GeneralListForm();
					accountGroupListForm.Name = "AccountGroupListForm";
				}
				return accountGroupListForm;
			}
		}

		public static GeneralListForm CustomerCategoryListObj
		{
			get
			{
				if (customerCategoryListForm == null || customerCategoryListForm.IsDisposed)
				{
					customerCategoryListForm = new GeneralListForm();
					customerCategoryListForm.Name = "CustomerCategoryListForm";
				}
				return customerCategoryListForm;
			}
		}

		public static GeneralListForm ContactsCategoryListObj
		{
			get
			{
				if (contactsCategoryListForm == null || contactsCategoryListForm.IsDisposed)
				{
					contactsCategoryListForm = new GeneralListForm();
					contactsCategoryListForm.Name = "ContactsCategoryListForm";
				}
				return contactsCategoryListForm;
			}
		}

		public static GeneralListForm LeadCategoryListObj
		{
			get
			{
				if (leadCategoryListForm == null || leadCategoryListForm.IsDisposed)
				{
					leadCategoryListForm = new GeneralListForm();
					leadCategoryListForm.Name = "LeadCategoryListForm";
				}
				return leadCategoryListForm;
			}
		}

		public static GeneralListForm VendorCategoryListObj
		{
			get
			{
				if (vendorCategoryListForm == null || vendorCategoryListForm.IsDisposed)
				{
					vendorCategoryListForm = new GeneralListForm();
					vendorCategoryListForm.Name = "VendorCategoryListForm";
				}
				return vendorCategoryListForm;
			}
		}

		public static GeneralListForm GenericProductTypeListObj
		{
			get
			{
				if (genericProductTypeListForm == null || genericProductTypeListForm.IsDisposed)
				{
					genericProductTypeListForm = new GeneralListForm();
					genericProductTypeListForm.Name = "GenericProductTypeListForm";
				}
				return genericProductTypeListForm;
			}
		}

		public static GeneralListForm RiderSummaryListFormObj
		{
			get
			{
				if (riderSummaryListForm == null || riderSummaryListForm.IsDisposed)
				{
					riderSummaryListForm = new GeneralListForm();
					riderSummaryListForm.Name = "RiderSummaryListForm";
				}
				return riderSummaryListForm;
			}
		}

		public static GeneralListForm HorseSummaryListFormObj
		{
			get
			{
				if (horseSummaryListForm == null || horseSummaryListForm.IsDisposed)
				{
					horseSummaryListForm = new GeneralListForm();
					horseSummaryListForm.Name = "HorseSummaryListForm";
				}
				return horseSummaryListForm;
			}
		}

		public static GeneralListForm HorseTypeListFormObj
		{
			get
			{
				if (horseTypeListForm == null || horseTypeListForm.IsDisposed)
				{
					horseTypeListForm = new GeneralListForm();
					horseTypeListForm.Name = "HorseTypeListForm";
				}
				return horseTypeListForm;
			}
		}

		public static GeneralListForm EquipmentCategoryListFormObj
		{
			get
			{
				if (equipmentCategoryListForm == null || equipmentCategoryListForm.IsDisposed)
				{
					equipmentCategoryListForm = new GeneralListForm();
					equipmentCategoryListForm.Name = "EquipmentCategoryListForm";
				}
				return equipmentCategoryListForm;
			}
		}

		public static GeneralListForm EquipmentTypeListFormObj
		{
			get
			{
				if (equipmentTypeListForm == null || equipmentTypeListForm.IsDisposed)
				{
					equipmentTypeListForm = new GeneralListForm();
					equipmentTypeListForm.Name = "EquipmentTypeListForm";
				}
				return equipmentTypeListForm;
			}
		}

		public static GeneralListForm HorseSexListFormObj
		{
			get
			{
				if (horseSexListForm == null || horseSexListForm.IsDisposed)
				{
					horseSexListForm = new GeneralListForm();
					horseSexListForm.Name = "HorseSexListForm";
				}
				return horseSexListForm;
			}
		}

		public static GeneralListForm HolidayCalendarListFormObj
		{
			get
			{
				if (holidayCalendarListForm == null || holidayCalendarListForm.IsDisposed)
				{
					holidayCalendarListForm = new GeneralListForm();
					holidayCalendarListForm.Name = "HolidayCalendarListForm";
				}
				return holidayCalendarListForm;
			}
		}

		public static GeneralListForm OverTimeDetailsListFormObj
		{
			get
			{
				if (overTimeDetailsListForm == null || overTimeDetailsListForm.IsDisposed)
				{
					overTimeDetailsListForm = new GeneralListForm();
					overTimeDetailsListForm.Name = "OverTimeDetailsListForm";
				}
				return overTimeDetailsListForm;
			}
		}

		public static GeneralListForm CasePartyListFormObj
		{
			get
			{
				if (casePartyListForm == null || casePartyListForm.IsDisposed)
				{
					casePartyListForm = new GeneralListForm();
					casePartyListForm.Name = "CasePartyListForm";
				}
				return casePartyListForm;
			}
		}

		public static GeneralListForm LawyerListFormObj
		{
			get
			{
				if (lawyerListForm == null || lawyerListForm.IsDisposed)
				{
					lawyerListForm = new GeneralListForm();
					lawyerListForm.Name = "LawyerListForm";
				}
				return lawyerListForm;
			}
		}

		public static GeneralListForm LegalActionStatusListFormObj
		{
			get
			{
				if (legalActionStatusListForm == null || legalActionStatusListForm.IsDisposed)
				{
					legalActionStatusListForm = new GeneralListForm();
					legalActionStatusListForm.Name = "LegalActionStatusListForm";
				}
				return legalActionStatusListForm;
			}
		}

		public static GeneralListForm TaxListFormObj
		{
			get
			{
				if (taxListForm == null || taxListForm.IsDisposed)
				{
					taxListForm = new GeneralListForm();
					taxListForm.Name = "TaxListForm";
				}
				return taxListForm;
			}
		}

		public static GeneralListForm TaxGroupListFormObj
		{
			get
			{
				if (taxgroupListForm == null || taxgroupListForm.IsDisposed)
				{
					taxgroupListForm = new GeneralListForm();
					taxgroupListForm.Name = "TaxgroupListForm";
				}
				return taxgroupListForm;
			}
		}

		public static ServiceProviderForm ServiceProviderFormObj
		{
			get
			{
				if (serviceProviderForm == null || serviceProviderForm.IsDisposed)
				{
					serviceProviderForm = new ServiceProviderForm();
					if (UserPreferences.OpenMDI)
					{
						serviceProviderForm.MdiParent = MainForm;
					}
					serviceProviderForm.Show();
				}
				return serviceProviderForm;
			}
		}

		public static MaintenanceSchedulerForm MaintenanceScheduleFormObj
		{
			get
			{
				if (maintenanceSchedulerForm == null || maintenanceSchedulerForm.IsDisposed)
				{
					maintenanceSchedulerForm = new MaintenanceSchedulerForm();
					if (UserPreferences.OpenMDI)
					{
						maintenanceSchedulerForm.MdiParent = MainForm;
					}
					maintenanceSchedulerForm.Show();
				}
				return maintenanceSchedulerForm;
			}
		}

		public static VehicleMaintenanceSchedulerReport MaintenanceScheduleReportFormObj
		{
			get
			{
				if (maintenanceSchedulerReportForm == null || maintenanceSchedulerReportForm.IsDisposed)
				{
					maintenanceSchedulerReportForm = new VehicleMaintenanceSchedulerReport();
					if (UserPreferences.OpenMDI)
					{
						maintenanceSchedulerReportForm.MdiParent = MainForm;
					}
					maintenanceSchedulerReportForm.Show();
				}
				return maintenanceSchedulerReportForm;
			}
		}

		public static VehicleMaintenanceEntryForm VehicleMaintenanceEntryFormObj
		{
			get
			{
				if (vehicleMaintenanceEntryForm == null || vehicleMaintenanceEntryForm.IsDisposed)
				{
					vehicleMaintenanceEntryForm = new VehicleMaintenanceEntryForm();
					if (UserPreferences.OpenMDI)
					{
						vehicleMaintenanceEntryForm.MdiParent = MainForm;
					}
					vehicleMaintenanceEntryForm.Show();
				}
				return vehicleMaintenanceEntryForm;
			}
		}

		public static VehicleMaintenanceEntryReport MaintenanceEntryReportFormObj
		{
			get
			{
				if (maintenanceEntryReportForm == null || maintenanceEntryReportForm.IsDisposed)
				{
					maintenanceEntryReportForm = new VehicleMaintenanceEntryReport();
					if (UserPreferences.OpenMDI)
					{
						maintenanceEntryReportForm.MdiParent = MainForm;
					}
					maintenanceEntryReportForm.Show();
				}
				return maintenanceEntryReportForm;
			}
		}

		public static ProjectInvoiceReportForm ProjectInvoiceReportFormObj
		{
			get
			{
				if (projectInvoiceReportForm == null || projectInvoiceReportForm.IsDisposed)
				{
					projectInvoiceReportForm = new ProjectInvoiceReportForm();
					if (UserPreferences.OpenMDI)
					{
						projectInvoiceReportForm.MdiParent = MainForm;
					}
					projectInvoiceReportForm.Show();
				}
				return projectInvoiceReportForm;
			}
		}

		public static PhysicalStockEntryForm PhysicalStockEntryFormObj
		{
			get
			{
				if (physicalStockEntryForm == null || physicalStockEntryForm.IsDisposed)
				{
					physicalStockEntryForm = new PhysicalStockEntryForm();
					if (UserPreferences.OpenMDI)
					{
						physicalStockEntryForm.MdiParent = MainForm;
					}
					physicalStockEntryForm.Show();
				}
				return physicalStockEntryForm;
			}
		}

		public static SalespersonGroupDetailsForm SalespersonGroupDetailsFormObj
		{
			get
			{
				if (salespersonGroupDetailsForm == null || salespersonGroupDetailsForm.IsDisposed)
				{
					salespersonGroupDetailsForm = new SalespersonGroupDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						salespersonGroupDetailsForm.MdiParent = MainForm;
					}
					salespersonGroupDetailsForm.Show();
				}
				return salespersonGroupDetailsForm;
			}
		}

		public static PickListReport PickListReportFormObj
		{
			get
			{
				if (pickListReportForm == null || pickListReportForm.IsDisposed)
				{
					pickListReportForm = new PickListReport();
					if (UserPreferences.OpenMDI)
					{
						pickListReportForm.MdiParent = MainForm;
					}
					pickListReportForm.Show();
				}
				return pickListReportForm;
			}
		}

		public static ServiceCallTrackReportForm ServiceCallTrackReportFormObj
		{
			get
			{
				if (serviceCallTrackReportForm == null || serviceCallTrackReportForm.IsDisposed)
				{
					serviceCallTrackReportForm = new ServiceCallTrackReportForm();
					if (UserPreferences.OpenMDI)
					{
						serviceCallTrackReportForm.MdiParent = MainForm;
					}
					serviceCallTrackReportForm.Show();
				}
				return serviceCallTrackReportForm;
			}
		}

		public static VendorPriceListDetailsForm VendorPriceListDetailsFormObj
		{
			get
			{
				if (vendorPriceListDetailsForm == null || vendorPriceListDetailsForm.IsDisposed)
				{
					vendorPriceListDetailsForm = new VendorPriceListDetailsForm();
					if (UserPreferences.OpenMDI)
					{
						vendorPriceListDetailsForm.MdiParent = MainForm;
					}
					vendorPriceListDetailsForm.Show();
				}
				return vendorPriceListDetailsForm;
			}
		}

		public static GeneralListForm PayrollItemListFormObj
		{
			get
			{
				if (payrollItemListForm == null || payrollItemListForm.IsDisposed)
				{
					payrollItemListForm = new GeneralListForm();
					payrollItemListForm.Name = "PayrollItemListForm";
				}
				return payrollItemListForm;
			}
		}

		public static GeneralListForm AnalysisListFormObj
		{
			get
			{
				if (analysisListForm == null || analysisListForm.IsDisposed)
				{
					analysisListForm = new GeneralListForm();
					analysisListForm.Name = "AnalysisListForm";
				}
				return analysisListForm;
			}
		}

		public static GeneralListForm AnalysisGroupListFormObj
		{
			get
			{
				if (analysisGroupListForm == null || analysisGroupListForm.IsDisposed)
				{
					analysisGroupListForm = new GeneralListForm();
					analysisGroupListForm.Name = "AnalysisGroupListForm";
				}
				return analysisGroupListForm;
			}
		}

		public static GeneralListForm AreaListFormObj
		{
			get
			{
				if (areaListForm == null || areaListForm.IsDisposed)
				{
					areaListForm = new GeneralListForm();
					areaListForm.Name = "AreaListForm";
				}
				return areaListForm;
			}
		}

		public static GeneralListForm JobListFormObj
		{
			get
			{
				if (jobListForm == null || jobListForm.IsDisposed)
				{
					jobListForm = new GeneralListForm();
					jobListForm.Name = "JobListForm";
				}
				return jobListForm;
			}
		}

		public static GeneralListForm ClientAssetListFormObj
		{
			get
			{
				if (clientAssetListForm == null || clientAssetListForm.IsDisposed)
				{
					clientAssetListForm = new GeneralListForm();
					clientAssetListForm.Name = "ClientAssetListForm";
				}
				return clientAssetListForm;
			}
		}

		public static GeneralListForm PriceListFormObj
		{
			get
			{
				if (priceListForm == null || priceListForm.IsDisposed)
				{
					priceListForm = new GeneralListForm();
					priceListForm.Name = "PriceListForm";
				}
				return priceListForm;
			}
		}

		public static GeneralListForm CandidateListFormObj
		{
			get
			{
				if (candidateListForm == null || candidateListForm.IsDisposed)
				{
					candidateListForm = new GeneralListForm();
					candidateListForm.Name = "CandidateListForm";
				}
				return candidateListForm;
			}
		}

		public static GeneralListForm AppointmentListFormObj
		{
			get
			{
				if (appointmentListForm == null || appointmentListForm.IsDisposed)
				{
					appointmentListForm = new GeneralListForm();
					appointmentListForm.Name = "AppointmentListForm";
				}
				return appointmentListForm;
			}
		}

		public static GeneralListForm WorkLocationListFormObj
		{
			get
			{
				if (workLocationListForm == null || workLocationListForm.IsDisposed)
				{
					workLocationListForm = new GeneralListForm();
					workLocationListForm.Name = "WorkLocationListForm";
				}
				return workLocationListForm;
			}
		}

		public static GeneralListForm TransporterListFormObj
		{
			get
			{
				if (transporterListForm == null || transporterListForm.IsDisposed)
				{
					transporterListForm = new GeneralListForm();
					transporterListForm.Name = "TransporterListForm";
				}
				return transporterListForm;
			}
		}

		public static GeneralListForm GenericListFormObj
		{
			get
			{
				if (genericListForm == null || genericListForm.IsDisposed)
				{
					genericListForm = new GeneralListForm();
					genericListForm.Name = "GenericListForm";
				}
				return genericListForm;
			}
		}

		public static GeneralListForm followupDetailsListFormObj
		{
			get
			{
				if (followupDetailsListForm == null || followupDetailsListForm.IsDisposed)
				{
					followupDetailsListForm = new GeneralListForm();
					followupDetailsListForm.Name = "FollowupListForm";
				}
				return followupDetailsListForm;
			}
		}

		public static GeneralListForm CollateralListFormObj
		{
			get
			{
				if (collateralListForm == null || collateralListForm.IsDisposed)
				{
					collateralListForm = new GeneralListForm();
					collateralListForm.Name = "CollateralListForm";
				}
				return collateralListForm;
			}
		}

		public static GeneralListForm CustomReportListFormObj
		{
			get
			{
				if (customReportListForm == null || customReportListForm.IsDisposed)
				{
					customReportListForm = new GeneralListForm();
					customReportListForm.Name = "CustomReportListForm";
				}
				return customReportListForm;
			}
		}

		public static GeneralListForm CustomGadgetListFormObj
		{
			get
			{
				if (customGadgetListForm == null || customGadgetListForm.IsDisposed)
				{
					customGadgetListForm = new GeneralListForm();
					customGadgetListForm.Name = "CustomGadgetListForm";
				}
				return customGadgetListForm;
			}
		}

		public static GeneralListForm JobTaskListFormObj
		{
			get
			{
				if (jobTaskListForm == null || jobTaskListForm.IsDisposed)
				{
					jobTaskListForm = new GeneralListForm();
					jobTaskListForm.Name = "JobTaskListForm";
				}
				return jobTaskListForm;
			}
		}

		public static GeneralListForm JobTaskGroupListFormObj
		{
			get
			{
				if (jobTaskGroupListForm == null || jobTaskGroupListForm.IsDisposed)
				{
					jobTaskGroupListForm = new GeneralListForm();
					jobTaskGroupListForm.Name = "JobTaskGroupListForm";
				}
				return jobTaskGroupListForm;
			}
		}

		public static GeneralListForm INCOListFormObj
		{
			get
			{
				if (INCOListForm == null || INCOListForm.IsDisposed)
				{
					INCOListForm = new GeneralListForm();
					INCOListForm.Name = "INCOListForm";
				}
				return INCOListForm;
			}
		}

		public static GeneralListForm QuoteComparisonListFormObj
		{
			get
			{
				if (quoteComparisonListForm == null || quoteComparisonListForm.IsDisposed)
				{
					quoteComparisonListForm = new GeneralListForm();
					quoteComparisonListForm.Name = "QuoteComparisonListForm";
				}
				return quoteComparisonListForm;
			}
		}

		public static GeneralListForm OpportunityListFormObj
		{
			get
			{
				if (opportunityListForm == null || opportunityListForm.IsDisposed)
				{
					opportunityListForm = new GeneralListForm();
					opportunityListForm.Name = "OpportunityListForm";
				}
				return opportunityListForm;
			}
		}

		public static GeneralListForm CompetitorListFormObj
		{
			get
			{
				if (competitorListForm == null || competitorListForm.IsDisposed)
				{
					competitorListForm = new GeneralListForm();
					competitorListForm.Name = "CompetitorListForm";
				}
				return competitorListForm;
			}
		}

		public static GeneralListForm ActivityListFormObj
		{
			get
			{
				if (activityListForm == null || activityListForm.IsDisposed)
				{
					activityListForm = new GeneralListForm();
					activityListForm.Name = "ActivityListForm";
				}
				return activityListForm;
			}
		}

		public static GeneralListForm CampaignListFormObj
		{
			get
			{
				if (campaignListForm == null || campaignListForm.IsDisposed)
				{
					campaignListForm = new GeneralListForm();
					campaignListForm.Name = "campaignListForm";
				}
				return campaignListForm;
			}
		}

		public static GeneralListForm EventListFormObj
		{
			get
			{
				if (eventListForm == null || eventListForm.IsDisposed)
				{
					eventListForm = new GeneralListForm();
					eventListForm.Name = "eventListForm";
				}
				return eventListForm;
			}
		}

		public static GeneralListForm BankListFormObj
		{
			get
			{
				if (bankListForm == null || bankListForm.IsDisposed)
				{
					bankListForm = new GeneralListForm();
					bankListForm.Name = "BankListForm";
				}
				return bankListForm;
			}
		}

		public static GeneralListForm BankFacilityGroupListFormObj
		{
			get
			{
				if (bankFacilityGroupListForm == null || bankFacilityGroupListForm.IsDisposed)
				{
					bankFacilityGroupListForm = new GeneralListForm();
					bankFacilityGroupListForm.Name = "BankFacilityGroupListForm";
				}
				return bankFacilityGroupListForm;
			}
		}

		public static GeneralListForm BankFacilityListFormObj
		{
			get
			{
				if (bankFacilityListForm == null || bankFacilityListForm.IsDisposed)
				{
					bankFacilityListForm = new GeneralListForm();
					bankFacilityListForm.Name = "BankFacilityListForm";
				}
				return bankFacilityListForm;
			}
		}

		public static GeneralListForm CityListFormObj
		{
			get
			{
				if (cityListForm == null || cityListForm.IsDisposed)
				{
					cityListForm = new GeneralListForm();
					cityListForm.Name = "CityListForm";
				}
				return cityListForm;
			}
		}

		public static GeneralListForm JobFeeListFormObj
		{
			get
			{
				if (jobFeeListForm == null || jobFeeListForm.IsDisposed)
				{
					jobFeeListForm = new GeneralListForm();
					jobFeeListForm.Name = "JobFeeListForm";
				}
				return jobFeeListForm;
			}
		}

		public static GeneralListForm VehicleListFormObj
		{
			get
			{
				if (vehicleListForm == null || vehicleListForm.IsDisposed)
				{
					vehicleListForm = new GeneralListForm();
					vehicleListForm.Name = "VehicleListForm";
				}
				return vehicleListForm;
			}
		}

		public static GeneralListForm CustomerVendorListFormObj
		{
			get
			{
				if (customerVendorListForm == null || customerVendorListForm.IsDisposed)
				{
					customerVendorListForm = new GeneralListForm();
					customerVendorListForm.Name = "CustomerVendorListForm";
				}
				return customerVendorListForm;
			}
		}

		public static GeneralListForm EquipmentListFormObj
		{
			get
			{
				if (equipmentListForm == null || equipmentListForm.IsDisposed)
				{
					equipmentListForm = new GeneralListForm();
					equipmentListForm.Name = "EquipmentListForm";
				}
				return equipmentListForm;
			}
		}

		public static GeneralListForm BenefitListFormObj
		{
			get
			{
				if (benefitListForm == null || benefitListForm.IsDisposed)
				{
					benefitListForm = new GeneralListForm();
					benefitListForm.Name = "BenefitListForm";
				}
				return benefitListForm;
			}
		}

		public static GeneralListForm EOSRuleListFormObj
		{
			get
			{
				if (eosRuleListForm == null || eosRuleListForm.IsDisposed)
				{
					eosRuleListForm = new GeneralListForm();
					eosRuleListForm.Name = "EOSRuleListForm";
				}
				return eosRuleListForm;
			}
		}

		public static GeneralListForm BuyerListFormObj
		{
			get
			{
				if (buyerListForm == null || buyerListForm.IsDisposed)
				{
					buyerListForm = new GeneralListForm();
					buyerListForm.Name = "BuyerListForm";
				}
				return buyerListForm;
			}
		}

		public static GeneralListForm CompanyDocTypeListFormObj
		{
			get
			{
				if (companyDocTypeListForm == null || companyDocTypeListForm.IsDisposed)
				{
					companyDocTypeListForm = new GeneralListForm();
					companyDocTypeListForm.Name = "CompanyDocTypeListForm";
				}
				return companyDocTypeListForm;
			}
		}

		public static GeneralListForm CompanyDocumentListFormObj
		{
			get
			{
				if (companyDocumentListForm == null || companyDocumentListForm.IsDisposed)
				{
					companyDocumentListForm = new GeneralListForm();
					companyDocumentListForm.Name = "CompanyDocumentListForm";
				}
				return companyDocumentListForm;
			}
		}

		public static GeneralListForm ContactListFormObj
		{
			get
			{
				if (contactListForm == null || contactListForm.IsDisposed)
				{
					contactListForm = new GeneralListForm();
					contactListForm.Name = "ContactListForm";
				}
				return contactListForm;
			}
		}

		public static GeneralListForm CountryListFormObj
		{
			get
			{
				if (countryListForm == null || countryListForm.IsDisposed)
				{
					countryListForm = new GeneralListForm();
					countryListForm.Name = "CountryListForm";
				}
				return countryListForm;
			}
		}

		public static GeneralListForm CurrencyListFormObj
		{
			get
			{
				if (currencyListForm == null || currencyListForm.IsDisposed)
				{
					currencyListForm = new GeneralListForm();
					currencyListForm.Name = "CurrencyListForm";
				}
				return currencyListForm;
			}
		}

		public static GeneralListForm CustomerClassListFormObj
		{
			get
			{
				if (customerClassListForm == null || customerClassListForm.IsDisposed)
				{
					customerClassListForm = new GeneralListForm();
					customerClassListForm.Name = "CustomerClassListForm";
				}
				return customerClassListForm;
			}
		}

		public static GeneralListForm CustomerGroupListFormObj
		{
			get
			{
				if (customerGroupListForm == null || customerGroupListForm.IsDisposed)
				{
					customerGroupListForm = new GeneralListForm();
					customerGroupListForm.Name = "CustomerGroupListForm";
				}
				return customerGroupListForm;
			}
		}

		public static GeneralListForm CheckListFormObj
		{
			get
			{
				if (checkListForm == null || checkListForm.IsDisposed)
				{
					checkListForm = new GeneralListForm();
					checkListForm.Name = "CheckListForm";
				}
				return checkListForm;
			}
		}

		public static GeneralListForm DeductionListFormObj
		{
			get
			{
				if (deductionListForm == null || deductionListForm.IsDisposed)
				{
					deductionListForm = new GeneralListForm();
					deductionListForm.Name = "DeductionListForm";
				}
				return deductionListForm;
			}
		}

		public static GeneralListForm DegreeListFormObj
		{
			get
			{
				if (degreeListForm == null || degreeListForm.IsDisposed)
				{
					degreeListForm = new GeneralListForm();
					degreeListForm.Name = "DegreeListForm";
				}
				return degreeListForm;
			}
		}

		public static GeneralListForm DepartmentListFormObj
		{
			get
			{
				if (departmentListForm == null || departmentListForm.IsDisposed)
				{
					departmentListForm = new GeneralListForm();
					departmentListForm.Name = "DepartmentListForm";
				}
				return departmentListForm;
			}
		}

		public static GeneralListForm DestinationListFormObj
		{
			get
			{
				if (destinationListForm == null || destinationListForm.IsDisposed)
				{
					destinationListForm = new GeneralListForm();
					destinationListForm.Name = "DestinationListForm";
				}
				return destinationListForm;
			}
		}

		public static GeneralListForm DivisionListFormObj
		{
			get
			{
				if (divisionListForm == null || divisionListForm.IsDisposed)
				{
					divisionListForm = new GeneralListForm();
					divisionListForm.Name = "DivisionListForm";
				}
				return divisionListForm;
			}
		}

		public static GeneralListForm CompanyDivisionListFormObj
		{
			get
			{
				if (companyDivisionListForm == null || companyDivisionListForm.IsDisposed)
				{
					companyDivisionListForm = new GeneralListForm();
					companyDivisionListForm.Name = "CompanyDivisionListForm";
				}
				return companyDivisionListForm;
			}
		}

		public static GeneralListForm EmployeeDocTypeListFormObj
		{
			get
			{
				if (employeeDocTypeListForm == null || employeeDocTypeListForm.IsDisposed)
				{
					employeeDocTypeListForm = new GeneralListForm();
					employeeDocTypeListForm.Name = "EmployeeDocTypeListForm";
				}
				return employeeDocTypeListForm;
			}
		}

		public static GeneralListForm EmployeeGroupListFormObj
		{
			get
			{
				if (employeeGroupListForm == null || employeeGroupListForm.IsDisposed)
				{
					employeeGroupListForm = new GeneralListForm();
					employeeGroupListForm.Name = "EmployeeGroupListForm";
				}
				return employeeGroupListForm;
			}
		}

		public static GeneralListForm EmployeeTypeListFormObj
		{
			get
			{
				if (employeeTypeListForm == null || employeeTypeListForm.IsDisposed)
				{
					employeeTypeListForm = new GeneralListForm();
					employeeTypeListForm.Name = "EmployeeTypeListForm";
				}
				return employeeTypeListForm;
			}
		}

		public static GeneralListForm GradeListFormObj
		{
			get
			{
				if (gradeListForm == null || gradeListForm.IsDisposed)
				{
					gradeListForm = new GeneralListForm();
					gradeListForm.Name = "GradeListForm";
				}
				return gradeListForm;
			}
		}

		public static GeneralListForm LeaveTypeListFormObj
		{
			get
			{
				if (leaveTypeListForm == null || leaveTypeListForm.IsDisposed)
				{
					leaveTypeListForm = new GeneralListForm();
					leaveTypeListForm.Name = "LeaveTypeListForm";
				}
				return leaveTypeListForm;
			}
		}

		public static GeneralListForm VehicleDocTypeListFormObj
		{
			get
			{
				if (vehicleDocTypeListForm == null || vehicleDocTypeListForm.IsDisposed)
				{
					vehicleDocTypeListForm = new GeneralListForm();
					vehicleDocTypeListForm.Name = "VehicleDocTypeListForm";
				}
				return vehicleDocTypeListForm;
			}
		}

		public static GeneralListForm JobTypeListFormObj
		{
			get
			{
				if (jobTypeListForm == null || jobTypeListForm.IsDisposed)
				{
					jobTypeListForm = new GeneralListForm();
					jobTypeListForm.Name = "JobTypeListForm";
				}
				return jobTypeListForm;
			}
		}

		public static GeneralListForm CostCategoryListFormObj
		{
			get
			{
				if (costCategoryListForm == null || costCategoryListForm.IsDisposed)
				{
					costCategoryListForm = new GeneralListForm();
					costCategoryListForm.Name = "CostCategoryListForm";
				}
				return costCategoryListForm;
			}
		}

		public static GeneralListForm LocationListFormObj
		{
			get
			{
				if (locationListForm == null || locationListForm.IsDisposed)
				{
					locationListForm = new GeneralListForm();
					locationListForm.Name = "LocationListForm";
				}
				return locationListForm;
			}
		}

		public static GeneralListForm NationalityListFormObj
		{
			get
			{
				if (nationalityListForm == null || nationalityListForm.IsDisposed)
				{
					nationalityListForm = new GeneralListForm();
					nationalityListForm.Name = "NationalityListForm";
				}
				return nationalityListForm;
			}
		}

		public static GeneralListForm PaymentMethodListFormObj
		{
			get
			{
				if (paymentMethodListForm == null || paymentMethodListForm.IsDisposed)
				{
					paymentMethodListForm = new GeneralListForm();
					paymentMethodListForm.Name = "PaymentMethodListForm";
				}
				return paymentMethodListForm;
			}
		}

		public static GeneralListForm PaymentTermListFormObj
		{
			get
			{
				if (paymentTermListForm == null || paymentTermListForm.IsDisposed)
				{
					paymentTermListForm = new GeneralListForm();
					paymentTermListForm.Name = "PaymentTermListForm";
				}
				return paymentTermListForm;
			}
		}

		public static GeneralListForm PositionListFormObj
		{
			get
			{
				if (positionListForm == null || positionListForm.IsDisposed)
				{
					positionListForm = new GeneralListForm();
					positionListForm.Name = "PositionListForm";
				}
				return positionListForm;
			}
		}

		public static GeneralListForm PricelevelListFormObj
		{
			get
			{
				if (pricelevelListForm == null || pricelevelListForm.IsDisposed)
				{
					pricelevelListForm = new GeneralListForm();
					pricelevelListForm.Name = "PricelevelListForm";
				}
				return pricelevelListForm;
			}
		}

		public static GeneralListForm ProductBrandListFormObj
		{
			get
			{
				if (productBrandListForm == null || productBrandListForm.IsDisposed)
				{
					productBrandListForm = new GeneralListForm();
					productBrandListForm.Name = "ProductBrandListForm";
				}
				return productBrandListForm;
			}
		}

		public static GeneralListForm ProductCategoryListFormObj
		{
			get
			{
				if (productCategoryListForm == null || productCategoryListForm.IsDisposed)
				{
					productCategoryListForm = new GeneralListForm();
					productCategoryListForm.Name = "ProductCategoryListForm";
				}
				return productCategoryListForm;
			}
		}

		public static GeneralListForm ProductClassListFormObj
		{
			get
			{
				if (productClassListForm == null || productClassListForm.IsDisposed)
				{
					productClassListForm = new GeneralListForm();
					productClassListForm.Name = "ProductClassListForm";
				}
				return productClassListForm;
			}
		}

		public static GeneralListForm ProductManufacturerListFormObj
		{
			get
			{
				if (productManufacturerListForm == null || productManufacturerListForm.IsDisposed)
				{
					productManufacturerListForm = new GeneralListForm();
					productManufacturerListForm.Name = "ProductManufacturerListForm";
				}
				return productManufacturerListForm;
			}
		}

		public static GeneralListForm ProductStyleListFormObj
		{
			get
			{
				if (productStyleListForm == null || productStyleListForm.IsDisposed)
				{
					productStyleListForm = new GeneralListForm();
					productStyleListForm.Name = "ProductStyleListForm";
				}
				return productStyleListForm;
			}
		}

		public static GeneralListForm ProductSpecificationListFormObj
		{
			get
			{
				if (productSpecificationListForm == null || productSpecificationListForm.IsDisposed)
				{
					productSpecificationListForm = new GeneralListForm();
					productSpecificationListForm.Name = "ProductSpecificationListForm";
				}
				return productSpecificationListForm;
			}
		}

		public static GeneralListForm ReligionListFormObj
		{
			get
			{
				if (religionListForm == null || religionListForm.IsDisposed)
				{
					religionListForm = new GeneralListForm();
					religionListForm.Name = "ReligionListForm";
				}
				return religionListForm;
			}
		}

		public static GeneralListForm SalespersonListFormObj
		{
			get
			{
				if (salespersonListForm == null || salespersonListForm.IsDisposed)
				{
					salespersonListForm = new GeneralListForm();
					salespersonListForm.Name = "SalespersonListForm";
				}
				return salespersonListForm;
			}
		}

		public static GeneralListForm ShippingMethodListFormObj
		{
			get
			{
				if (shippingMethodListForm == null || shippingMethodListForm.IsDisposed)
				{
					shippingMethodListForm = new GeneralListForm();
					shippingMethodListForm.Name = "ShippingMethodListForm";
				}
				return shippingMethodListForm;
			}
		}

		public static GeneralListForm SkillListFormObj
		{
			get
			{
				if (skillListForm == null || skillListForm.IsDisposed)
				{
					skillListForm = new GeneralListForm();
					skillListForm.Name = "SkillListForm";
				}
				return skillListForm;
			}
		}

		public static GeneralListForm SponsorListFormObj
		{
			get
			{
				if (sponsorListForm == null || sponsorListForm.IsDisposed)
				{
					sponsorListForm = new GeneralListForm();
					sponsorListForm.Name = "SponsorListForm";
				}
				return sponsorListForm;
			}
		}

		public static GeneralListForm TenancyContractListFormObj
		{
			get
			{
				if (tenancyContractListForm == null || tenancyContractListForm.IsDisposed)
				{
					tenancyContractListForm = new GeneralListForm();
					tenancyContractListForm.Name = "TenancyContractListForm";
				}
				return tenancyContractListForm;
			}
		}

		public static GeneralListForm TradeLicenseListFormObj
		{
			get
			{
				if (tradeLicenseListForm == null || tradeLicenseListForm.IsDisposed)
				{
					tradeLicenseListForm = new GeneralListForm();
					tradeLicenseListForm.Name = "TradeLicenseListForm";
				}
				return tradeLicenseListForm;
			}
		}

		public static GeneralListForm UnitListFormObj
		{
			get
			{
				if (unitListForm == null || unitListForm.IsDisposed)
				{
					unitListForm = new GeneralListForm();
					unitListForm.Name = "UnitListForm";
				}
				return unitListForm;
			}
		}

		public static GeneralListForm FiscalYearListFormObj
		{
			get
			{
				if (fiscalYearListForm == null || fiscalYearListForm.IsDisposed)
				{
					fiscalYearListForm = new GeneralListForm();
					fiscalYearListForm.Name = "FiscalYearListForm";
				}
				return fiscalYearListForm;
			}
		}

		public static GeneralListForm VendorClassListFormObj
		{
			get
			{
				if (vendorClassListForm == null || vendorClassListForm.IsDisposed)
				{
					vendorClassListForm = new GeneralListForm();
					vendorClassListForm.Name = "VendorClassListForm";
				}
				return vendorClassListForm;
			}
		}

		public static GeneralListForm VendorGroupListFormObj
		{
			get
			{
				if (vendorGroupListForm == null || vendorGroupListForm.IsDisposed)
				{
					vendorGroupListForm = new GeneralListForm();
					vendorGroupListForm.Name = "VendorGroupListForm";
				}
				return vendorGroupListForm;
			}
		}

		public static GeneralListForm ContainerSizeListFormObj
		{
			get
			{
				if (containerSizeListForm == null || containerSizeListForm.IsDisposed)
				{
					containerSizeListForm = new GeneralListForm();
					containerSizeListForm.Name = "ContainerSizeListForm";
				}
				return containerSizeListForm;
			}
		}

		public static GeneralListForm INCODetailsListFormObj
		{
			get
			{
				if (INCODetailsListForm == null || INCODetailsListForm.IsDisposed)
				{
					INCODetailsListForm = new GeneralListForm();
					INCODetailsListForm.Name = "INCODetailsForm";
				}
				return INCODetailsListForm;
			}
		}

		public static GeneralListForm NationalAccountListFormObj
		{
			get
			{
				if (nationalAccountListForm == null || nationalAccountListForm.IsDisposed)
				{
					nationalAccountListForm = new GeneralListForm();
					nationalAccountListForm.Name = "NationalAccountListForm";
				}
				return nationalAccountListForm;
			}
		}

		public static GeneralListForm VisaListFormObj
		{
			get
			{
				if (visaListForm == null || visaListForm.IsDisposed)
				{
					visaListForm = new GeneralListForm();
					visaListForm.Name = "VisaListForm";
				}
				return visaListForm;
			}
		}

		public static GeneralListForm CostCenterListFormObj
		{
			get
			{
				if (costCenterListForm == null || costCenterListForm.IsDisposed)
				{
					costCenterListForm = new GeneralListForm();
					costCenterListForm.Name = "CostCenterListForm";
				}
				return costCenterListForm;
			}
		}

		public static GeneralListForm ReturnedChequeReasonListFormObj
		{
			get
			{
				if (returnedChequeReasonListForm == null || returnedChequeReasonListForm.IsDisposed)
				{
					returnedChequeReasonListForm = new GeneralListForm();
					returnedChequeReasonListForm.Name = "ReturnedChequeReasonListForm";
				}
				return returnedChequeReasonListForm;
			}
		}

		public static GeneralListForm ChequebookListFormObj
		{
			get
			{
				if (chequebookListForm == null || chequebookListForm.IsDisposed)
				{
					chequebookListForm = new GeneralListForm();
					chequebookListForm.Name = "ChequebookListForm";
				}
				return chequebookListForm;
			}
		}

		public static GeneralListForm RegisterListFormObj
		{
			get
			{
				if (registerListForm == null || registerListForm.IsDisposed)
				{
					registerListForm = new GeneralListForm();
					registerListForm.Name = "RegisterListForm";
				}
				return registerListForm;
			}
		}

		public static GeneralListForm PortListFormObj
		{
			get
			{
				if (portListForm == null || portListForm.IsDisposed)
				{
					portListForm = new GeneralListForm();
					portListForm.Name = "PortListForm";
				}
				return portListForm;
			}
		}

		public static GeneralListForm DisciplineActionTypeListFormObj
		{
			get
			{
				if (disciplineActionTypeListForm == null || disciplineActionTypeListForm.IsDisposed)
				{
					disciplineActionTypeListForm = new GeneralListForm();
					disciplineActionTypeListForm.Name = "DisciplineActionTypeListForm";
				}
				return disciplineActionTypeListForm;
			}
		}

		public static GeneralListForm EmployeeActivityTypeListFormObj
		{
			get
			{
				if (employeeActivityTypeListForm == null || employeeActivityTypeListForm.IsDisposed)
				{
					employeeActivityTypeListForm = new GeneralListForm();
					employeeActivityTypeListForm.Name = "EmployeeActivityTypeListForm";
				}
				return employeeActivityTypeListForm;
			}
		}

		public static GeneralListForm UserListFormObj
		{
			get
			{
				if (userListForm == null || userListForm.IsDisposed)
				{
					userListForm = new GeneralListForm();
					userListForm.Name = "UserListForm";
				}
				return userListForm;
			}
		}

		public static GeneralListForm UserGroupListFormObj
		{
			get
			{
				if (userGroupListForm == null || userGroupListForm.IsDisposed)
				{
					userGroupListForm = new GeneralListForm();
					userGroupListForm.Name = "UserGroupListForm";
				}
				return userGroupListForm;
			}
		}

		public static GeneralListForm EmployeeDocumentListFormObj
		{
			get
			{
				if (employeeDocumentListForm == null || employeeDocumentListForm.IsDisposed)
				{
					employeeDocumentListForm = new GeneralListForm();
					employeeDocumentListForm.Name = "EmployeeDocumentListForm";
				}
				return employeeDocumentListForm;
			}
		}

		public static GeneralListForm VendorAddressListFormObj
		{
			get
			{
				if (vendorAddressListForm == null || vendorAddressListForm.IsDisposed)
				{
					vendorAddressListForm = new GeneralListForm();
					vendorAddressListForm.Name = "VendorAddressListForm";
				}
				return vendorAddressListForm;
			}
		}

		public static GeneralListForm CustomerAddressListFormObj
		{
			get
			{
				if (customerAddressListForm == null || customerAddressListForm.IsDisposed)
				{
					customerAddressListForm = new GeneralListForm();
					customerAddressListForm.Name = "CustomerAddressListForm";
				}
				return customerAddressListForm;
			}
		}

		public static GeneralListForm DriverListFormObj
		{
			get
			{
				if (driverListForm == null || driverListForm.IsDisposed)
				{
					driverListForm = new GeneralListForm();
					driverListForm.Name = "DriverListForm";
				}
				return driverListForm;
			}
		}

		public static GeneralListForm ExpenseCodeListFormObj
		{
			get
			{
				if (expenseCodeListForm == null || expenseCodeListForm.IsDisposed)
				{
					expenseCodeListForm = new GeneralListForm();
					expenseCodeListForm.Name = "ExpenseCodeListForm";
				}
				return expenseCodeListForm;
			}
		}

		public static GeneralListForm FixedAssetListFormObj
		{
			get
			{
				if (fixedAssetListForm == null || fixedAssetListForm.IsDisposed)
				{
					fixedAssetListForm = new GeneralListForm();
					fixedAssetListForm.Name = "FixedAssetListForm";
				}
				return fixedAssetListForm;
			}
		}

		public static GeneralListForm FixedAssetGroupListFormObj
		{
			get
			{
				if (fixedAssetGroupListForm == null || fixedAssetGroupListForm.IsDisposed)
				{
					fixedAssetGroupListForm = new GeneralListForm();
					fixedAssetGroupListForm.Name = "FixedAssetGroupListForm";
				}
				return fixedAssetGroupListForm;
			}
		}

		public static GeneralListForm FixedAssetLocationListFormObj
		{
			get
			{
				if (fixedAssetLocationListForm == null || fixedAssetLocationListForm.IsDisposed)
				{
					fixedAssetLocationListForm = new GeneralListForm();
					fixedAssetLocationListForm.Name = "FixedAssetLocationListForm";
				}
				return fixedAssetLocationListForm;
			}
		}

		public static GeneralListForm FixedAssetClassListFormObj
		{
			get
			{
				if (fixedAssetClassListForm == null || fixedAssetClassListForm.IsDisposed)
				{
					fixedAssetClassListForm = new GeneralListForm();
					fixedAssetClassListForm.Name = "FixedAssetClassListForm";
				}
				return fixedAssetClassListForm;
			}
		}

		public static GeneralListForm QualityTaskListFormObj
		{
			get
			{
				if (qualityTaskListForm == null || qualityTaskListForm.IsDisposed)
				{
					qualityTaskListForm = new GeneralListForm();
					qualityTaskListForm.Name = "QualityTaskListForm";
				}
				return qualityTaskListForm;
			}
		}

		public static GeneralListForm ArrivalReportTemplateListFormObj
		{
			get
			{
				if (arrivalReportTemplateListForm == null || arrivalReportTemplateListForm.IsDisposed)
				{
					arrivalReportTemplateListForm = new GeneralListForm();
					arrivalReportTemplateListForm.Name = "ArrivalReportTemplateListForm";
				}
				return arrivalReportTemplateListForm;
			}
		}

		public static GeneralListForm SurveyorListFormObj
		{
			get
			{
				if (surveyorListForm == null || surveyorListForm.IsDisposed)
				{
					surveyorListForm = new GeneralListForm();
					surveyorListForm.Name = "SurveyorListForm";
				}
				return surveyorListForm;
			}
		}

		public static GeneralListForm PropertyClassListFormObj
		{
			get
			{
				if (propertyClassListForm == null || propertyClassListForm.IsDisposed)
				{
					propertyClassListForm = new GeneralListForm();
					propertyClassListForm.Name = "PropertyClassListForm";
				}
				return propertyClassListForm;
			}
		}

		public static GeneralListForm PropertyAgentListFormObj
		{
			get
			{
				if (propertyAgentListForm == null || propertyAgentListForm.IsDisposed)
				{
					propertyAgentListForm = new GeneralListForm();
					propertyAgentListForm.Name = "PropertyAgentListForm";
				}
				return propertyAgentListForm;
			}
		}

		public static GeneralListForm PropertyListFormObj
		{
			get
			{
				if (propertyListForm == null || propertyListForm.IsDisposed)
				{
					propertyListForm = new GeneralListForm();
					propertyListForm.Name = "PropertyListForm";
				}
				return propertyListForm;
			}
		}

		public static GeneralListForm PropertyUnitListFormObj
		{
			get
			{
				if (propertyUnitListForm == null || propertyUnitListForm.IsDisposed)
				{
					propertyUnitListForm = new GeneralListForm();
					propertyUnitListForm.Name = "PropertyListForm";
				}
				return propertyUnitListForm;
			}
		}

		public static GeneralListForm PropertyVirtualUnitListFormObj
		{
			get
			{
				if (propertyVirtualUnitListForm == null || propertyVirtualUnitListForm.IsDisposed)
				{
					propertyVirtualUnitListForm = new GeneralListForm();
					propertyVirtualUnitListForm.Name = "PropertyVirtualUnitListForm";
				}
				return propertyVirtualUnitListForm;
			}
		}

		public static GeneralListForm KitchenTypeListFormObj
		{
			get
			{
				if (kitchenTypeListForm == null || kitchenTypeListForm.IsDisposed)
				{
					kitchenTypeListForm = new GeneralListForm();
					kitchenTypeListForm.Name = "KitchenTypeListForm";
				}
				return kitchenTypeListForm;
			}
		}

		public static GeneralListForm PropertyIncomeCodeListFormObj
		{
			get
			{
				if (propertyIncomeCodeListForm == null || propertyIncomeCodeListForm.IsDisposed)
				{
					propertyIncomeCodeListForm = new GeneralListForm();
					propertyIncomeCodeListForm.Name = "propertyIncomeCodeListForm";
				}
				return propertyIncomeCodeListForm;
			}
		}

		public static GeneralListForm PropertyTenantListFormObj
		{
			get
			{
				if (propertyTenantListForm == null || propertyTenantListForm.IsDisposed)
				{
					propertyTenantListForm = new GeneralListForm();
					propertyTenantListForm.Name = "propertyTenantListForm";
				}
				return propertyTenantListForm;
			}
		}

		public static GeneralListForm InventoryTransferTypeListFormObj
		{
			get
			{
				if (inventoryTransferTypeListForm == null || inventoryTransferTypeListForm.IsDisposed)
				{
					inventoryTransferTypeListForm = new GeneralListForm();
					inventoryTransferTypeListForm.Name = "InventoryTransferTypeListForm";
				}
				return inventoryTransferTypeListForm;
			}
		}

		public static GeneralListForm ApprovalDetailsListFormObj
		{
			get
			{
				if (approvalDetailsListForm == null || approvalDetailsListForm.IsDisposed)
				{
					approvalDetailsListForm = new GeneralListForm();
					approvalDetailsListForm.Name = "ApprovalDetailsListForm";
				}
				return approvalDetailsListForm;
			}
		}

		public static GeneralListForm VerificationDetailsListFormObj
		{
			get
			{
				if (verificationDetailsListForm == null || verificationDetailsListForm.IsDisposed)
				{
					verificationDetailsListForm = new GeneralListForm();
					verificationDetailsListForm.Name = "VerificationDetailsListForm";
				}
				return verificationDetailsListForm;
			}
		}

		public static GeneralListForm BinDetailsListFormObj
		{
			get
			{
				if (binDetailsListForm == null || binDetailsListForm.IsDisposed)
				{
					binDetailsListForm = new GeneralListForm();
					binDetailsListForm.Name = "BinDetailsListForm";
				}
				return binDetailsListForm;
			}
		}

		public static GeneralListForm RouteDetailsListFormObj
		{
			get
			{
				if (routeDetailsListForm == null || routeDetailsListForm.IsDisposed)
				{
					routeDetailsListForm = new GeneralListForm();
					routeDetailsListForm.Name = "RouteDetailsListForm";
				}
				return routeDetailsListForm;
			}
		}

		public static GeneralListForm RouteGroupDetailsListFormObj
		{
			get
			{
				if (routeGroupDetailsListForm == null || routeGroupDetailsListForm.IsDisposed)
				{
					routeGroupDetailsListForm = new GeneralListForm();
					routeGroupDetailsListForm.Name = "RouteGroupDetailsListForm";
				}
				return routeGroupDetailsListForm;
			}
		}

		public static GeneralListForm ServiceActivityListFormObj
		{
			get
			{
				if (serviceActivityListForm == null || serviceActivityListForm.IsDisposed)
				{
					serviceActivityListForm = new GeneralListForm();
					serviceActivityListForm.Name = "ServiceActivityListForm";
				}
				return serviceActivityListForm;
			}
		}

		public static GeneralListForm EmployeeLoanTypeListFormObj
		{
			get
			{
				if (employeeLoanTypeListForm == null || employeeLoanTypeListForm.IsDisposed)
				{
					employeeLoanTypeListForm = new GeneralListForm();
					employeeLoanTypeListForm.Name = "EmployeeLoanTypeForm";
				}
				return employeeLoanTypeListForm;
			}
		}

		public static GeneralListForm EmployeeLeaveListFormObj
		{
			get
			{
				if (employeeLeaveListForm == null || employeeLeaveListForm.IsDisposed)
				{
					employeeLeaveListForm = new GeneralListForm();
					employeeLeaveListForm.Name = "EmployeeLeaveRequestForm";
				}
				return employeeLeaveListForm;
			}
		}

		public static GeneralListForm EmployeeLeaveResumptionListFormObj
		{
			get
			{
				if (employeeLeaveResumptionListForm == null || employeeLeaveResumptionListForm.IsDisposed)
				{
					employeeLeaveResumptionListForm = new GeneralListForm();
					employeeLeaveResumptionListForm.Name = "EmployeeLeaveResumptionListForm";
				}
				return employeeLeaveResumptionListForm;
			}
		}

		public static GeneralListForm EmployeePassportControlListFormObj
		{
			get
			{
				if (employeePassportControlListForm == null || employeePassportControlListForm.IsDisposed)
				{
					employeePassportControlListForm = new GeneralListForm();
					employeePassportControlListForm.Name = "EmployeePassportControlListForm";
				}
				return employeePassportControlListForm;
			}
		}

		public static GeneralListForm AdjustmentTypeDetailsListFormObj
		{
			get
			{
				if (adjustmentTypeDetailsListForm == null || adjustmentTypeDetailsListForm.IsDisposed)
				{
					adjustmentTypeDetailsListForm = new GeneralListForm();
					adjustmentTypeDetailsListForm.Name = "AdjustmentTypeDetailsListForm";
				}
				return adjustmentTypeDetailsListForm;
			}
		}

		public static GeneralListForm JobBOMListFormObj
		{
			get
			{
				if (jobBOMListForm == null || jobBOMListForm.IsDisposed)
				{
					jobBOMListForm = new GeneralListForm();
					jobBOMListForm.Name = "JobBOMListForm";
				}
				return jobBOMListForm;
			}
		}

		public static GeneralListForm PackageListFormObj
		{
			get
			{
				if (packageListForm == null || packageListForm.IsDisposed)
				{
					packageListForm = new GeneralListForm();
					packageListForm.Name = "PackageListForm";
				}
				return packageListForm;
			}
		}

		public static GeneralListForm ReleaseTypeListFormObj
		{
			get
			{
				if (releaseTypeListForm == null || releaseTypeListForm.IsDisposed)
				{
					releaseTypeListForm = new GeneralListForm();
					releaseTypeListForm.Name = "releaseTypeListForm";
				}
				return releaseTypeListForm;
			}
		}

		public static GeneralListForm ServiceItemListFormObj
		{
			get
			{
				if (serviceItemListForm == null || serviceItemListForm.IsDisposed)
				{
					serviceItemListForm = new GeneralListForm();
					serviceItemListForm.Name = "serviceItemListForm";
				}
				return serviceItemListForm;
			}
		}

		public static GeneralListForm ProvisionTypeListFormObj
		{
			get
			{
				if (provisionTypeListForm == null || provisionTypeListForm.IsDisposed)
				{
					provisionTypeListForm = new GeneralListForm();
					provisionTypeListForm.Name = "provisionTypeListForm";
				}
				return provisionTypeListForm;
			}
		}

		public static GeneralListForm ServiceProviderListFormObj
		{
			get
			{
				if (serviceProviderListForm == null || serviceProviderListForm.IsDisposed)
				{
					serviceProviderListForm = new GeneralListForm();
					serviceProviderListForm.Name = "ServiceProviderListForm";
				}
				return serviceProviderListForm;
			}
		}

		public static GeneralListForm EAEquipmentListFormObj
		{
			get
			{
				if (EAEquipmentListForm == null || EAEquipmentListForm.IsDisposed)
				{
					EAEquipmentListForm = new GeneralListForm();
				}
				return EAEquipmentListForm;
			}
		}

		public static GeneralListForm RequisitionTypeListFormObj
		{
			get
			{
				if (requisitionTypeListForm == null || requisitionTypeListForm.IsDisposed)
				{
					requisitionTypeListForm = new GeneralListForm();
				}
				return requisitionTypeListForm;
			}
		}

		public static GeneralListForm SysDocListFormObj
		{
			get
			{
				if (SysDocListForm == null || SysDocListForm.IsDisposed)
				{
					SysDocListForm = new GeneralListForm();
				}
				return SysDocListForm;
			}
		}

		public static GeneralListForm ProductMakeListFormObj
		{
			get
			{
				if (productMakeListForm == null || productMakeListForm.IsDisposed)
				{
					productMakeListForm = new GeneralListForm();
				}
				return productMakeListForm;
			}
		}

		public static GeneralListForm ProductTypeListFormObj
		{
			get
			{
				if (productTypeListForm == null || productTypeListForm.IsDisposed)
				{
					productTypeListForm = new GeneralListForm();
				}
				return productTypeListForm;
			}
		}

		public static GeneralListForm ProductModelListFormObj
		{
			get
			{
				if (productModelListForm == null || productModelListForm.IsDisposed)
				{
					productModelListForm = new GeneralListForm();
				}
				return productModelListForm;
			}
		}

		public static GeneralListForm TaskStepsListFormObj
		{
			get
			{
				if (taskStepsListForm == null || taskStepsListForm.IsDisposed)
				{
					taskStepsListForm = new GeneralListForm();
				}
				return taskStepsListForm;
			}
		}

		public static GeneralListForm TaskTypeListFormObj
		{
			get
			{
				if (taskTypeListForm == null || taskTypeListForm.IsDisposed)
				{
					taskTypeListForm = new GeneralListForm();
				}
				return taskTypeListForm;
			}
		}

		public static GeneralListForm RackListFormObj
		{
			get
			{
				if (rackListForm == null || rackListForm.IsDisposed)
				{
					rackListForm = new GeneralListForm();
				}
				return rackListForm;
			}
		}

		public static GeneralListForm EmployeeAbscondingListFormObj
		{
			get
			{
				if (employeeAbscondingList == null || employeeAbscondingList.IsDisposed)
				{
					employeeAbscondingList = new GeneralListForm();
				}
				return employeeAbscondingList;
			}
		}

		public static GeneralListForm PrintTemplateMapListFormObj
		{
			get
			{
				if (printTemplateMapListForm == null || printTemplateMapListForm.IsDisposed)
				{
					printTemplateMapListForm = new GeneralListForm();
				}
				return printTemplateMapListForm;
			}
		}

		public static GeneralListForm PatientListFormObj
		{
			get
			{
				if (patientListForm == null || patientListForm.IsDisposed)
				{
					patientListForm = new GeneralListForm();
				}
				return patientListForm;
			}
		}

		public static GeneralListForm PatientDocTypeListFormObj
		{
			get
			{
				if (patientDocTypeListForm == null || patientDocTypeListForm.IsDisposed)
				{
					patientDocTypeListForm = new GeneralListForm();
				}
				return patientDocTypeListForm;
			}
		}

		public static GeneralListForm DataSyncListFormObj
		{
			get
			{
				if (dataSyncListForm == null || dataSyncListForm.IsDisposed)
				{
					dataSyncListForm = new GeneralListForm();
				}
				return dataSyncListForm;
			}
		}

		public static FollowupDetailsForm FollowupDetailsFormObj
		{
			get
			{
				if (followupDetailsForm == null || followupDetailsForm.IsDisposed)
				{
					followupDetailsForm = new FollowupDetailsForm();
					if (showForm)
					{
						followupDetailsForm.Show();
					}
				}
				return followupDetailsForm;
			}
		}

		public static CompanyAddressDetailsForm CompanyAddressDetailsFormObj
		{
			get
			{
				if (companyAddressDetailsForm == null || companyAddressDetailsForm.IsDisposed)
				{
					companyAddressDetailsForm = new CompanyAddressDetailsForm();
				}
				return companyAddressDetailsForm;
			}
		}

		public static ConsignOutForm ConsignOutFormObj
		{
			get
			{
				if (consignOutForm == null || consignOutForm.IsDisposed)
				{
					consignOutForm = new ConsignOutForm();
				}
				return consignOutForm;
			}
		}

		public static InsuranceProviderForm InsurancePRoviderFormObj
		{
			get
			{
				if (insuranceProviderForm == null || insuranceProviderForm.IsDisposed)
				{
					insuranceProviderForm = new InsuranceProviderForm();
				}
				return insuranceProviderForm;
			}
		}

		public static GarmentRentalForm GarmentRentalFormObj
		{
			get
			{
				if (garmentRentalForm == null || garmentRentalForm.IsDisposed)
				{
					garmentRentalForm = new GarmentRentalForm();
				}
				return garmentRentalForm;
			}
		}

		public static GarmentRentalReturnForm GarmentRentalReturnFormObj
		{
			get
			{
				if (garmentRentalReturnForm == null || garmentRentalReturnForm.IsDisposed)
				{
					garmentRentalReturnForm = new GarmentRentalReturnForm();
				}
				return garmentRentalReturnForm;
			}
		}

		public static ExpenseCodeDetailsForm ExpenseCodeDetailsFormObj
		{
			get
			{
				if (expenseCodeDetailsForm == null || expenseCodeDetailsForm.IsDisposed)
				{
					expenseCodeDetailsForm = new ExpenseCodeDetailsForm();
				}
				return expenseCodeDetailsForm;
			}
		}

		public static ExportSalesInvoiceForm ExportSalesInvoiceFormObj
		{
			get
			{
				if (exportSalesInvoiceForm == null || exportSalesInvoiceForm.IsDisposed)
				{
					exportSalesInvoiceForm = new ExportSalesInvoiceForm();
				}
				return exportSalesInvoiceForm;
			}
		}

		public static ExportDeliveryNoteForm ExportDeliveryNoteFormObj
		{
			get
			{
				if (exportDeliveryNoteForm == null || exportDeliveryNoteForm.IsDisposed)
				{
					exportDeliveryNoteForm = new ExportDeliveryNoteForm();
				}
				return exportDeliveryNoteForm;
			}
		}

		public static ExportPickListForm ExportPickListFormObj
		{
			get
			{
				if (exportPickListForm == null || exportPickListForm.IsDisposed)
				{
					exportPickListForm = new ExportPickListForm();
				}
				return exportPickListForm;
			}
		}

		public static ExportSalesOrderForm ExportSalesOrderFormObj
		{
			get
			{
				if (exportSalesOrderForm == null || exportSalesOrderForm.IsDisposed)
				{
					exportSalesOrderForm = new ExportSalesOrderForm();
				}
				return exportSalesOrderForm;
			}
		}

		public static ExportSalesProformaInvoiceForm ExportSalesProformaInvoiceFormObj
		{
			get
			{
				if (exportSalesProformaInvoiceForm == null || exportSalesProformaInvoiceForm.IsDisposed)
				{
					exportSalesProformaInvoiceForm = new ExportSalesProformaInvoiceForm();
				}
				return exportSalesProformaInvoiceForm;
			}
		}

		public static ShipmentsPerformanceAnalyseForm ShipmentsPerformanceAnalyseFormObj
		{
			get
			{
				if (shipmentPerformanceAnalysForm == null || shipmentPerformanceAnalysForm.IsDisposed)
				{
					shipmentPerformanceAnalysForm = new ShipmentsPerformanceAnalyseForm();
				}
				return shipmentPerformanceAnalysForm;
			}
		}

		public static ExportSmartListForm exportSmartListFormObj
		{
			get
			{
				if (exportSmartListForm == null || exportSmartListForm.IsDisposed)
				{
					exportSmartListForm = new ExportSmartListForm();
					exportSmartListForm.ReportType = "SmartList";
				}
				return exportSmartListForm;
			}
		}

		public static ExportSmartListForm ExportPivotReportFormObj
		{
			get
			{
				if (exportPivotReportForm == null || exportPivotReportForm.IsDisposed)
				{
					exportPivotReportForm = new ExportSmartListForm();
					exportPivotReportForm.ReportType = "PivotReport";
				}
				return exportPivotReportForm;
			}
		}

		public static ExportSmartListForm ExportCustomGadgetFormObj
		{
			get
			{
				if (exportCustomeGadgetForm == null || exportCustomeGadgetForm.IsDisposed)
				{
					exportCustomeGadgetForm = new ExportSmartListForm();
					exportCustomeGadgetForm.ReportType = "CustomGadget";
				}
				return exportCustomeGadgetForm;
			}
		}

		public static ImportSmartListForm importSmartListFormObj
		{
			get
			{
				if (importSmartListForm == null || importSmartListForm.IsDisposed)
				{
					importSmartListForm = new ImportSmartListForm();
					importSmartListForm.ReportType = "SmartList";
				}
				return importSmartListForm;
			}
		}

		public static ImportSmartListForm ImportPivotReportFormObj
		{
			get
			{
				if (importPivotReportForm == null || importPivotReportForm.IsDisposed)
				{
					importPivotReportForm = new ImportSmartListForm();
					importPivotReportForm.ReportType = "PivotReport";
				}
				return importPivotReportForm;
			}
		}

		public static ImportSmartListForm ImportCustomGadgetFormObj
		{
			get
			{
				if (importCustomeGadgetForm == null || importCustomeGadgetForm.IsDisposed)
				{
					importCustomeGadgetForm = new ImportSmartListForm();
					importCustomeGadgetForm.ReportType = "CustomGadget";
				}
				return importCustomeGadgetForm;
			}
		}

		public static ConsignOutReturnForm ConsignOutReturnFormObj
		{
			get
			{
				if (consignOutReturnForm == null || consignOutReturnForm.IsDisposed)
				{
					consignOutReturnForm = new ConsignOutReturnForm();
				}
				return consignOutReturnForm;
			}
		}

		public static ConsignInForm ConsignInFormObj
		{
			get
			{
				if (consignInForm == null || consignInForm.IsDisposed)
				{
					consignInForm = new ConsignInForm();
				}
				return consignInForm;
			}
		}

		public static ConsignInReturnForm ConsignInReturnFormObj
		{
			get
			{
				if (consignInReturnForm == null || consignInReturnForm.IsDisposed)
				{
					consignInReturnForm = new ConsignInReturnForm();
				}
				return consignInReturnForm;
			}
		}

		public static ProjectExpenseAllocationForm ProjectExpenseAllocationFormObj
		{
			get
			{
				if (projectExpenseAllocationForm == null || projectExpenseAllocationForm.IsDisposed)
				{
					projectExpenseAllocationForm = new ProjectExpenseAllocationForm();
				}
				return projectExpenseAllocationForm;
			}
		}

		public static PurchaseClaimForm PurchaseClaimFormObj
		{
			get
			{
				if (purchaseClaimForm == null || purchaseClaimForm.IsDisposed)
				{
					purchaseClaimForm = new PurchaseClaimForm();
				}
				return purchaseClaimForm;
			}
		}

		public static JobEstimationDetailsForm JobEstimationFormObj
		{
			get
			{
				if (jobEstimationForm == null || jobEstimationForm.IsDisposed)
				{
					jobEstimationForm = new JobEstimationDetailsForm();
				}
				return jobEstimationForm;
			}
		}

		public static PurchaseCostEntryForm PurchaseCostEntryFormObj
		{
			get
			{
				if (purchaseCostEntryForm == null || purchaseCostEntryForm.IsDisposed)
				{
					purchaseCostEntryForm = new PurchaseCostEntryForm();
				}
				return purchaseCostEntryForm;
			}
		}

		public static RiderSummaryDetailsForm RiderSummaryDetailsFormObj
		{
			get
			{
				if (riderSummaryDetailsForm == null || riderSummaryDetailsForm.IsDisposed)
				{
					riderSummaryDetailsForm = new RiderSummaryDetailsForm();
				}
				return riderSummaryDetailsForm;
			}
		}

		public static HorseSummaryDetailsForm HorseSummaryDetailsFormObj
		{
			get
			{
				if (horseSummaryDetailsForm == null || horseSummaryDetailsForm.IsDisposed)
				{
					horseSummaryDetailsForm = new HorseSummaryDetailsForm();
				}
				return horseSummaryDetailsForm;
			}
		}

		public static HorseSummaryReport HorseSummaryreportObj
		{
			get
			{
				if (horseSummaryReport == null || horseSummaryReport.IsDisposed)
				{
					horseSummaryReport = new HorseSummaryReport();
				}
				return horseSummaryReport;
			}
		}

		public static BarCodeReport BarcodeReportObj
		{
			get
			{
				if (barcodeReport == null || barcodeReport.IsDisposed)
				{
					barcodeReport = new BarCodeReport();
				}
				return barcodeReport;
			}
		}

		public static BalanceSheetComparisonReportForm BalanceSheetComparisonReportObj
		{
			get
			{
				if (balanceSheetComparisonReport == null || balanceSheetComparisonReport.IsDisposed)
				{
					balanceSheetComparisonReport = new BalanceSheetComparisonReportForm();
				}
				return balanceSheetComparisonReport;
			}
		}

		public static ContainerTrackingReportForm ContainerTrackingreportFormObj
		{
			get
			{
				if (containerTrackingreportform == null || containerTrackingreportform.IsDisposed)
				{
					containerTrackingreportform = new ContainerTrackingReportForm();
				}
				return containerTrackingreportform;
			}
		}

		public static HorseTypeForm HorseTypeFormObj
		{
			get
			{
				if (horseTypeForm == null || horseTypeForm.IsDisposed)
				{
					horseTypeForm = new HorseTypeForm();
				}
				return horseTypeForm;
			}
		}

		public static HorseSexForm HorseSexFormObj
		{
			get
			{
				if (horseSexForm == null || horseSexForm.IsDisposed)
				{
					horseSexForm = new HorseSexForm();
				}
				return horseSexForm;
			}
		}

		public static EAEquipmentForm EAEquipmentFormObj
		{
			get
			{
				if (equipmentForm == null || equipmentForm.IsDisposed)
				{
					equipmentForm = new EAEquipmentForm();
				}
				return equipmentForm;
			}
		}

		public static RequisitionDetailsForm RequisitionDetailsFormObj
		{
			get
			{
				if (requisitionDetailsForm == null || requisitionDetailsForm.IsDisposed)
				{
					requisitionDetailsForm = new RequisitionDetailsForm();
				}
				return requisitionDetailsForm;
			}
		}

		public static MobilisationForm MobilisationFormObj
		{
			get
			{
				if (mobilisationForm == null || mobilisationForm.IsDisposed)
				{
					mobilisationForm = new MobilisationForm();
				}
				return mobilisationForm;
			}
		}

		public static ItemTransactionForm ItemTransactionFormObj
		{
			get
			{
				if (itemTransactionForm == null || itemTransactionForm.IsDisposed)
				{
					itemTransactionForm = new ItemTransactionForm();
				}
				return itemTransactionForm;
			}
		}

		public static EquipmentTransferForm EquipmentTransferFormObj
		{
			get
			{
				if (equipmentTransferForm == null || equipmentTransferForm.IsDisposed)
				{
					equipmentTransferForm = new EquipmentTransferForm();
				}
				return equipmentTransferForm;
			}
		}

		public static WorkOrderForm EquipmentWorkOrderFormObj
		{
			get
			{
				if (equipmentWorkOrderForm == null || equipmentWorkOrderForm.IsDisposed)
				{
					equipmentWorkOrderForm = new WorkOrderForm();
				}
				return equipmentWorkOrderForm;
			}
		}

		public static LawyerDetailsForm LawyerDetailsFormObj
		{
			get
			{
				if (lawyerDetailsForm == null || lawyerDetailsForm.IsDisposed)
				{
					lawyerDetailsForm = new LawyerDetailsForm();
				}
				return lawyerDetailsForm;
			}
		}

		public static CasePartyDetailsForm CasePartyDetailsFormObj
		{
			get
			{
				if (casePartyDetailsForm == null || casePartyDetailsForm.IsDisposed)
				{
					casePartyDetailsForm = new CasePartyDetailsForm();
				}
				return casePartyDetailsForm;
			}
		}

		public static LegalActivityDetailsForm LegalActivityDetailsFormObj
		{
			get
			{
				if (legalActivityDetailsForm == null || legalActivityDetailsForm.IsDisposed)
				{
					legalActivityDetailsForm = new LegalActivityDetailsForm();
				}
				return legalActivityDetailsForm;
			}
		}

		public static LegalActionDetailsForm LegalActionDetailsFormObj
		{
			get
			{
				if (legalActionDetailsForm == null || legalActionDetailsForm.IsDisposed)
				{
					legalActionDetailsForm = new LegalActionDetailsForm();
				}
				return legalActionDetailsForm;
			}
		}

		public static FreightChargesForm FreightChargesFormObj
		{
			get
			{
				if (freightChargesForm == null || freightChargesForm.IsDisposed)
				{
					freightChargesForm = new FreightChargesForm();
				}
				return freightChargesForm;
			}
		}

		public static SalesForecastingForm SalesForecastingFormObj
		{
			get
			{
				if (salesForecastingForm == null || salesForecastingForm.IsDisposed)
				{
					salesForecastingForm = new SalesForecastingForm();
				}
				return salesForecastingForm;
			}
		}

		public static ProductMakeDetailsForm ProductMakeDetilsFormObj
		{
			get
			{
				if (productMakeDetilsForm == null || productMakeDetilsForm.IsDisposed)
				{
					productMakeDetilsForm = new ProductMakeDetailsForm();
				}
				return productMakeDetilsForm;
			}
		}

		public static ProductTypeDetailsForm ProductTypeDetilsFormObj
		{
			get
			{
				if (productTypeDetilsForm == null || productTypeDetilsForm.IsDisposed)
				{
					productTypeDetilsForm = new ProductTypeDetailsForm();
				}
				return productTypeDetilsForm;
			}
		}

		public static ProductModelDetailsForm ProductModelDetailsFormObj
		{
			get
			{
				if (productModelDetilsForm == null || productModelDetilsForm.IsDisposed)
				{
					productModelDetilsForm = new ProductModelDetailsForm();
				}
				return productModelDetilsForm;
			}
		}

		public static TaxGroupForm TaxGroupFormObj
		{
			get
			{
				if (taxGroupForm == null || taxGroupForm.IsDisposed)
				{
					taxGroupForm = new TaxGroupForm();
				}
				return taxGroupForm;
			}
		}

		public static ProductTypeDetailsForm ProductTypeFormObj
		{
			get
			{
				if (productTypeForm == null || productTypeForm.IsDisposed)
				{
					productTypeForm = new ProductTypeDetailsForm();
				}
				return productTypeForm;
			}
		}

		public static EquipmentListReport EquipmentListReportFormObj
		{
			get
			{
				if (equipmentListReportForm == null || equipmentListReportForm.IsDisposed)
				{
					equipmentListReportForm = new EquipmentListReport();
				}
				return equipmentListReportForm;
			}
		}

		public static RequisitionByWorkLocationProjectReport RequisitionByWorkLocationProjectReportFormObj
		{
			get
			{
				if (requisitionByWorkLocationProjectReportForm == null || requisitionByWorkLocationProjectReportForm.IsDisposed)
				{
					requisitionByWorkLocationProjectReportForm = new RequisitionByWorkLocationProjectReport();
				}
				return requisitionByWorkLocationProjectReportForm;
			}
		}

		public static MobilizationByWorkLocationProjectReport MobilizationByWorkLocationProjectReportFormObj
		{
			get
			{
				if (mobilizationByWorkLocationProjectReportForm == null || mobilizationByWorkLocationProjectReportForm.IsDisposed)
				{
					mobilizationByWorkLocationProjectReportForm = new MobilizationByWorkLocationProjectReport();
				}
				return mobilizationByWorkLocationProjectReportForm;
			}
		}

		public static EquipmentTransferReport EquipmentTransferReportFormObj
		{
			get
			{
				if (equipmentTransferReportForm == null || equipmentTransferReportForm.IsDisposed)
				{
					equipmentTransferReportForm = new EquipmentTransferReport();
				}
				return equipmentTransferReportForm;
			}
		}

		public static WorkOrderByLocationProjectReport EquipmentWorkOrderReportFormObj
		{
			get
			{
				if (equipmentWorkOrderReportForm == null || equipmentWorkOrderReportForm.IsDisposed)
				{
					equipmentWorkOrderReportForm = new WorkOrderByLocationProjectReport();
				}
				return equipmentWorkOrderReportForm;
			}
		}

		public static EquipmentFlowReport EquipmentFlowReportFormObj
		{
			get
			{
				if (equipmentFlowReportForm == null || equipmentFlowReportForm.IsDisposed)
				{
					equipmentFlowReportForm = new EquipmentFlowReport();
				}
				return equipmentFlowReportForm;
			}
		}

		public static WorkOrderInventoryIssueForm WorkOrderInventoryIssueFormObj
		{
			get
			{
				if (workOrderInventoryIssueForm == null || workOrderInventoryIssueForm.IsDisposed)
				{
					workOrderInventoryIssueForm = new WorkOrderInventoryIssueForm();
				}
				return workOrderInventoryIssueForm;
			}
		}

		public static WorkOrderInventoryReturnForm WorkOrderInventoryReturnFormObj
		{
			get
			{
				if (workOrderInventoryReturnForm == null || workOrderInventoryReturnForm.IsDisposed)
				{
					workOrderInventoryReturnForm = new WorkOrderInventoryReturnForm();
				}
				return workOrderInventoryReturnForm;
			}
		}

		public static InventoryDismantleForm InventoryDismantleFormObj
		{
			get
			{
				if (inventoryDismantleForm == null || inventoryDismantleForm.IsDisposed)
				{
					inventoryDismantleForm = new InventoryDismantleForm();
				}
				return inventoryDismantleForm;
			}
		}

		public static WorkOrderInventoryTransactionsReport WorkOrderInventoryTransactionReportObj
		{
			get
			{
				if (workOrderInventoryTransactionReport == null || workOrderInventoryTransactionReport.IsDisposed)
				{
					workOrderInventoryTransactionReport = new WorkOrderInventoryTransactionsReport();
				}
				return workOrderInventoryTransactionReport;
			}
		}

		public static PendingCaseReport PendingCaseReportObj
		{
			get
			{
				if (pendingCaseReport == null || pendingCaseReport.IsDisposed)
				{
					pendingCaseReport = new PendingCaseReport();
				}
				return pendingCaseReport;
			}
		}

		public static CaseStatusReport CaseStatusReportObj
		{
			get
			{
				if (CaseStatusReport == null || CaseStatusReport.IsDisposed)
				{
					CaseStatusReport = new CaseStatusReport();
				}
				return CaseStatusReport;
			}
		}

		public static CaseLawyerTrack CaseLawyerTrackReportObj
		{
			get
			{
				if (caseLawyerTrackReport == null || caseLawyerTrackReport.IsDisposed)
				{
					caseLawyerTrackReport = new CaseLawyerTrack();
				}
				return caseLawyerTrackReport;
			}
		}

		public static ProductPriceBulkUpdateForm ProductPriceBulkUpdateFormObj
		{
			get
			{
				if (productPriceBulkUpdateForm == null || productPriceBulkUpdateForm.IsDisposed)
				{
					productPriceBulkUpdateForm = new ProductPriceBulkUpdateForm();
				}
				return productPriceBulkUpdateForm;
			}
		}

		public static OpeningChequeReceiptEntryForm OpeningChequeReceiptEntryFormObj
		{
			get
			{
				if (openingChequeReceiptEntryForm == null || openingChequeReceiptEntryForm.IsDisposed)
				{
					openingChequeReceiptEntryForm = new OpeningChequeReceiptEntryForm();
				}
				return openingChequeReceiptEntryForm;
			}
		}

		public static OpeningChequePaymentEntryForm OpeningChequePaymentEntryFormObj
		{
			get
			{
				if (openingChequePaymentEntryForm == null || openingChequePaymentEntryForm.IsDisposed)
				{
					openingChequePaymentEntryForm = new OpeningChequePaymentEntryForm();
				}
				return openingChequePaymentEntryForm;
			}
		}

		public static MaterialReservationForm MaterialReservationFormObj
		{
			get
			{
				if (materialReservationForm == null || materialReservationForm.IsDisposed)
				{
					materialReservationForm = new MaterialReservationForm();
				}
				return materialReservationForm;
			}
		}

		public static CaseClientDetailsForm CaseClientDetailsFormObj
		{
			get
			{
				if (caseClientDetailsForm == null || caseClientDetailsForm.IsDisposed)
				{
					caseClientDetailsForm = new CaseClientDetailsForm();
				}
				return caseClientDetailsForm;
			}
		}

		public static GeneralListForm CaseClientListFormObj
		{
			get
			{
				if (caseClientListForm == null || caseClientListForm.IsDisposed)
				{
					caseClientListForm = new GeneralListForm();
					caseClientListForm.Name = "CaseClientListForm";
				}
				return caseClientListForm;
			}
		}

		public static TransactionListForm SalesInvoiceListFormObj
		{
			get
			{
				if (salesInvoiceListForm == null || salesInvoiceListForm.IsDisposed)
				{
					salesInvoiceListForm = new TransactionListForm();
					salesInvoiceListForm.ListType = TransactionListType.SalesInvoice;
					salesInvoiceListForm.Text = "Sales Invoice List";
					salesInvoiceListForm.Name = "SalesInvoiceListForm";
					salesInvoiceListForm.SelectedDocID = SelectedSysDocID;
				}
				return salesInvoiceListForm;
			}
		}

		public static TransactionListForm SalesInvoiceNIListFormObj
		{
			get
			{
				if (salesInvoiceNIListForm == null || salesInvoiceNIListForm.IsDisposed)
				{
					salesInvoiceNIListForm = new TransactionListForm();
					salesInvoiceNIListForm.ListType = TransactionListType.SalesInvoiceNI;
					salesInvoiceNIListForm.Text = "Sales Invoice Non Inventory List";
					salesInvoiceNIListForm.Name = "SalesInvoiceNIListForm";
					salesInvoiceNIListForm.SelectedDocID = SelectedSysDocID;
				}
				return salesInvoiceNIListForm;
			}
		}

		public static TransactionListForm PropertyServiceInvoiceListFormObj
		{
			get
			{
				if (propertyServiceInvoiceListForm == null || propertyServiceInvoiceListForm.IsDisposed)
				{
					propertyServiceInvoiceListForm = new TransactionListForm();
					propertyServiceInvoiceListForm.ListType = TransactionListType.PropertyServiceInvoice;
					propertyServiceInvoiceListForm.Text = "Property Service Invoice List";
					propertyServiceInvoiceListForm.Name = "PropertyServiceInvoiceListForm";
					propertyServiceInvoiceListForm.SelectedDocID = SelectedSysDocID;
				}
				return propertyServiceInvoiceListForm;
			}
		}

		public static TransactionListForm PropertyIncomePostingListFormObj
		{
			get
			{
				if (propertyIncomePostingListForm == null || propertyIncomePostingListForm.IsDisposed)
				{
					propertyIncomePostingListForm = new TransactionListForm();
					propertyIncomePostingListForm.ListType = TransactionListType.PropertyIncomePosting;
					propertyIncomePostingListForm.Text = "Property Income Posting List";
					propertyIncomePostingListForm.Name = "PropertyIncomePostingListForm";
					propertyIncomePostingListForm.SelectedDocID = SelectedSysDocID;
				}
				return propertyIncomePostingListForm;
			}
		}

		public static TransactionListForm SalesReceiptListFormObj
		{
			get
			{
				if (salesReceiptListForm == null || salesReceiptListForm.IsDisposed)
				{
					salesReceiptListForm = new TransactionListForm();
					salesReceiptListForm.ListType = TransactionListType.SalesReceipt;
					salesReceiptListForm.Text = "Sales Receipt List";
					salesReceiptListForm.Name = "SalesReceiptListForm";
					salesReceiptListForm.SelectedDocID = SelectedSysDocID;
				}
				return salesReceiptListForm;
			}
		}

		public static TransactionListForm EmployeeOpeningBalanceLeaveListFormObj
		{
			get
			{
				if (employeeOpeningBalanceLeaveList == null || employeeOpeningBalanceLeaveList.IsDisposed)
				{
					employeeOpeningBalanceLeaveList = new TransactionListForm();
					employeeOpeningBalanceLeaveList.ListType = TransactionListType.EmployeeOpeningBalanceLeave;
					employeeOpeningBalanceLeaveList.Text = "Employee Opening Balance Leave List";
					employeeOpeningBalanceLeaveList.Name = "EmployeeOpeningBalanceLeaveListForm";
				}
				return employeeOpeningBalanceLeaveList;
			}
		}

		public static TransactionListForm ExportPackingListListFormObj
		{
			get
			{
				if (exportPackingListListForm == null || exportPackingListListForm.IsDisposed)
				{
					exportPackingListListForm = new TransactionListForm();
					exportPackingListListForm.ListType = TransactionListType.ExportPackingList;
					exportPackingListListForm.Text = "Export Packing List";
					exportPackingListListForm.Name = "ExportPackingListForm";
				}
				return exportPackingListListForm;
			}
		}

		public static TransactionListForm ShipmentListFormObj
		{
			get
			{
				if (shipmentListForm == null || shipmentListForm.IsDisposed)
				{
					shipmentListForm = new TransactionListForm();
					shipmentListForm.ListType = TransactionListType.Shipment;
					shipmentListForm.Text = "Shipment List";
					shipmentListForm.Name = "ExportPackingListForm";
				}
				return shipmentListForm;
			}
		}

		public static TransactionListForm InventoryRepackingListFormObj
		{
			get
			{
				if (inventoryRepackingListForm == null || inventoryRepackingListForm.IsDisposed)
				{
					inventoryRepackingListForm = new TransactionListForm();
					inventoryRepackingListForm.ListType = TransactionListType.InventoryRepacking;
					inventoryRepackingListForm.Text = "Inventory Repacking List";
					inventoryRepackingListForm.Name = "InventoryRepackingListForm";
				}
				return inventoryRepackingListForm;
			}
		}

		public static TransactionListForm InventoryDamageListFormObj
		{
			get
			{
				if (inventoryDamageListForm == null || inventoryDamageListForm.IsDisposed)
				{
					inventoryDamageListForm = new TransactionListForm();
					inventoryDamageListForm.ListType = TransactionListType.InventoryDamage;
					inventoryDamageListForm.Text = "Inventory Non-Sale List";
					inventoryDamageListForm.Name = "InventoryDamageListForm";
					inventoryDamageListForm.SelectedDocID = SelectedSysDocID;
				}
				return inventoryDamageListForm;
			}
		}

		public static TransactionListForm InventoryTransferAcceptanceListFormObj
		{
			get
			{
				if (inventoryTransferAcceptanceListForm == null || inventoryTransferAcceptanceListForm.IsDisposed)
				{
					inventoryTransferAcceptanceListForm = new TransactionListForm();
					inventoryTransferAcceptanceListForm.ListType = TransactionListType.InventoryTransferAcceptance;
					inventoryTransferAcceptanceListForm.Text = "Receive Inventory Transfer List";
					inventoryTransferAcceptanceListForm.Name = "inventoryTransferAcceptanceForm";
				}
				return inventoryTransferAcceptanceListForm;
			}
		}

		public static TransactionListForm InventoryTransferReturnListFormObj
		{
			get
			{
				if (inventoryTransferReturnListForm == null || inventoryTransferReturnListForm.IsDisposed)
				{
					inventoryTransferReturnListForm = new TransactionListForm();
					inventoryTransferReturnListForm.ListType = TransactionListType.InventoryTransferReturn;
					inventoryTransferReturnListForm.Text = "Reject Inventory Transfer List";
					inventoryTransferReturnListForm.Name = "inventoryTransferReturnForm";
				}
				return inventoryTransferReturnListForm;
			}
		}

		public static TransactionListForm InventoryTransferListFormObj
		{
			get
			{
				if (inventoryTransferListForm == null || inventoryTransferListForm.IsDisposed)
				{
					inventoryTransferListForm = new TransactionListForm();
					inventoryTransferListForm.ListType = TransactionListType.InventoryTransfer;
					inventoryTransferListForm.Text = "Inventory Transfer List";
					inventoryTransferListForm.Name = "inventoryTransferForm";
					inventoryTransferListForm.SelectedDocID = SelectedSysDocID;
				}
				return inventoryTransferListForm;
			}
		}

		public static TransactionListForm DirectInventoryTransferListFormObj
		{
			get
			{
				if (directinventoryTransferListForm == null || directinventoryTransferListForm.IsDisposed)
				{
					directinventoryTransferListForm = new TransactionListForm();
					directinventoryTransferListForm.ListType = TransactionListType.DirectInventoryTransfer;
					directinventoryTransferListForm.Text = "Direct Inventory Transfer List";
					directinventoryTransferListForm.Name = "directinventoryTransferForm";
				}
				return directinventoryTransferListForm;
			}
		}

		public static TransactionListForm ExportSalesInvoiceListFormObj
		{
			get
			{
				if (exportsalesInvoiceListForm == null || exportsalesInvoiceListForm.IsDisposed)
				{
					exportsalesInvoiceListForm = new TransactionListForm();
					exportsalesInvoiceListForm.ListType = TransactionListType.ExportSalesInvoice;
					exportsalesInvoiceListForm.Text = "Export Sales Invoice List";
					exportsalesInvoiceListForm.Name = "ExportSalesInvoiceListForm";
					exportsalesInvoiceListForm.SelectedDocID = SelectedSysDocID;
				}
				return exportsalesInvoiceListForm;
			}
		}

		public static TransactionListForm ExportDeliveryNoteListFormObj
		{
			get
			{
				if (exportDeliveryNoteListForm == null || exportDeliveryNoteListForm.IsDisposed)
				{
					exportDeliveryNoteListForm = new TransactionListForm();
					exportDeliveryNoteListForm.ListType = TransactionListType.ExportDeliveryNote;
					exportDeliveryNoteListForm.Text = "Export Delivery Note List";
					exportDeliveryNoteListForm.Name = "ExportDeliveryNoteListForm";
					exportDeliveryNoteListForm.SelectedDocID = SelectedSysDocID;
				}
				return exportDeliveryNoteListForm;
			}
		}

		public static TransactionListForm ExportPickListListFormObj
		{
			get
			{
				if (exportPickListListForm == null || exportPickListListForm.IsDisposed)
				{
					exportPickListListForm = new TransactionListForm();
					exportPickListListForm.ListType = TransactionListType.ExportPickList;
					exportPickListListForm.Text = "Export Pick List";
					exportPickListListForm.Name = "ExportPickListListForm";
				}
				return exportPickListListForm;
			}
		}

		public static TransactionListForm JobInvoiceListFormObj
		{
			get
			{
				if (jobInvoiceListForm == null || jobInvoiceListForm.IsDisposed)
				{
					jobInvoiceListForm = new TransactionListForm();
					jobInvoiceListForm.ListType = TransactionListType.JobInvoice;
					jobInvoiceListForm.Text = "Project Billing List";
					jobInvoiceListForm.Name = "JobInvoiceListForm";
				}
				return jobInvoiceListForm;
			}
		}

		public static TransactionListForm JobInventoryIssueListFormObj
		{
			get
			{
				if (jobInventoryIssueListForm == null || jobInventoryIssueListForm.IsDisposed)
				{
					jobInventoryIssueListForm = new TransactionListForm();
					jobInventoryIssueListForm.ListType = TransactionListType.JobInventoryIssue;
					jobInventoryIssueListForm.Text = "job Inventory Issue List";
					jobInventoryIssueListForm.Name = "jobInventoryIssueListForm";
				}
				return jobInventoryIssueListForm;
			}
		}

		public static TransactionListForm JobEstimationListFormObj
		{
			get
			{
				if (jobEstimationListForm == null || jobEstimationListForm.IsDisposed)
				{
					jobEstimationListForm = new TransactionListForm();
					jobEstimationListForm.ListType = TransactionListType.JobEstimation;
					jobEstimationListForm.Text = "Job Estimation List";
					jobEstimationListForm.Name = "jobEstimationListForm";
				}
				return jobEstimationListForm;
			}
		}

		public static TransactionListForm SalesQuoteListFormObj
		{
			get
			{
				if (salesQuoteListForm == null || salesQuoteListForm.IsDisposed)
				{
					salesQuoteListForm = new TransactionListForm();
					salesQuoteListForm.ListType = TransactionListType.SalesQuote;
					salesQuoteListForm.Text = "Sales Quote List";
					salesQuoteListForm.Name = "SalesQuoteListForm";
					salesQuoteListForm.SelectedDocID = SelectedSysDocID;
				}
				return salesQuoteListForm;
			}
		}

		public static TransactionListForm SalesOrderListFormObj
		{
			get
			{
				if (salesOrderListForm == null || salesOrderListForm.IsDisposed)
				{
					salesOrderListForm = new TransactionListForm();
					salesOrderListForm.ListType = TransactionListType.SalesOrder;
					salesOrderListForm.Text = "Sales Order List";
					salesOrderListForm.Name = "SalesOrderListForm";
					salesOrderListForm.SelectedDocID = SelectedSysDocID;
				}
				return salesOrderListForm;
			}
		}

		public static TransactionListForm SalesEnquiryListFormObj
		{
			get
			{
				if (salesEnquiryListForm == null || salesEnquiryListForm.IsDisposed)
				{
					salesEnquiryListForm = new TransactionListForm();
					salesEnquiryListForm.ListType = TransactionListType.SalesEnquiry;
					salesEnquiryListForm.Text = "Sales Enquiry List";
					salesEnquiryListForm.Name = "SalesEnquiryListForm";
				}
				return salesEnquiryListForm;
			}
		}

		public static TransactionListForm SalesProformaListFormObj
		{
			get
			{
				if (salesProformaListForm == null || salesProformaListForm.IsDisposed)
				{
					salesProformaListForm = new TransactionListForm();
					salesProformaListForm.ListType = TransactionListType.SalesProforma;
					salesProformaListForm.Text = "SalesProformaList";
					salesProformaListForm.Name = "SalesProformaListForm";
				}
				return salesProformaListForm;
			}
		}

		public static TransactionListForm ExportSalesProformaListFormObj
		{
			get
			{
				if (exportsalesProformaListForm == null || exportsalesProformaListForm.IsDisposed)
				{
					exportsalesProformaListForm = new TransactionListForm();
					exportsalesProformaListForm.ListType = TransactionListType.ExportSalesProforma;
					exportsalesProformaListForm.Text = "Export SalesProformaList";
					exportsalesProformaListForm.Name = "ExportSalesProformaListForm";
				}
				return exportsalesProformaListForm;
			}
		}

		public static TransactionListForm SalesReturnListFormObj
		{
			get
			{
				if (salesReturnListForm == null || salesReturnListForm.IsDisposed)
				{
					salesReturnListForm = new TransactionListForm();
					salesReturnListForm.ListType = TransactionListType.SalesReturn;
					salesReturnListForm.Text = "Sales Return List";
					salesReturnListForm.Name = "SalesReturnListForm";
					salesReturnListForm.SelectedDocID = SelectedSysDocID;
				}
				return salesReturnListForm;
			}
		}

		public static TransactionListForm DeliveryNoteListFormObj
		{
			get
			{
				if (deliveryNoteListForm == null || deliveryNoteListForm.IsDisposed)
				{
					deliveryNoteListForm = new TransactionListForm();
					deliveryNoteListForm.ListType = TransactionListType.DeliveryNote;
					deliveryNoteListForm.Text = "Delivery Note List";
					deliveryNoteListForm.Name = "DeliveryNoteListForm";
					deliveryNoteListForm.SelectedDocID = SelectedSysDocID;
				}
				return deliveryNoteListForm;
			}
		}

		public static TransactionListForm ExportSalesOrderListFormObj
		{
			get
			{
				if (exportSalesOrderListForm == null || exportSalesOrderListForm.IsDisposed)
				{
					exportSalesOrderListForm = new TransactionListForm();
					exportSalesOrderListForm.ListType = TransactionListType.ExportSalesOrder;
					exportSalesOrderListForm.Text = "Export Sales Order List";
					exportSalesOrderListForm.Name = "ExportSalesOrderListForm";
					exportSalesOrderListForm.SelectedDocID = SelectedSysDocID;
				}
				return exportSalesOrderListForm;
			}
		}

		public static TransactionListForm ItemTransactionListFormObj
		{
			get
			{
				if (itemTransactionListForm == null || itemTransactionListForm.IsDisposed)
				{
					itemTransactionListForm = new TransactionListForm();
					itemTransactionListForm.ListType = TransactionListType.ItemTransaction;
					itemTransactionListForm.Text = "Item Transaction List";
					itemTransactionListForm.Name = "ItemTransactionListForm";
				}
				return itemTransactionListForm;
			}
		}

		public static TransactionListForm DeliveryReturnListFormObj
		{
			get
			{
				if (deliveryReturnListForm == null || deliveryReturnListForm.IsDisposed)
				{
					deliveryReturnListForm = new TransactionListForm();
					deliveryReturnListForm.ListType = TransactionListType.DeliveryReturn;
					deliveryReturnListForm.Text = "Delivery Return List";
					deliveryReturnListForm.Name = "DeliveryReturnListForm";
				}
				return deliveryReturnListForm;
			}
		}

		public static TransactionListForm ARPaymentAllocationListObj
		{
			get
			{
				if (arAllocationListForm == null || arAllocationListForm.IsDisposed)
				{
					arAllocationListForm = new TransactionListForm();
					arAllocationListForm.ListType = TransactionListType.ARPaymentAllocation;
					arAllocationListForm.Text = "Customer Payment Allocation List";
					arAllocationListForm.Name = "CustomerPaymentAllocationListForm";
				}
				return arAllocationListForm;
			}
		}

		public static TransactionListForm SalesHistoryListFormObj
		{
			get
			{
				if (salesHistoryForm == null || salesHistoryForm.IsDisposed)
				{
					salesHistoryForm = new TransactionListForm();
					salesHistoryForm.ListType = TransactionListType.SalesHistory;
					salesHistoryForm.Text = "Sales History";
					salesHistoryForm.Name = "SalesHistoryListForm";
				}
				return salesHistoryForm;
			}
		}

		public static TransactionListForm ActivityLogListFormObj
		{
			get
			{
				if (activityLogForm == null || activityLogForm.IsDisposed)
				{
					activityLogForm = new TransactionListForm();
					activityLogForm.ListType = TransactionListType.ActivityLog;
					activityLogForm.Text = "Activity Log";
					activityLogForm.Name = "ActivityLogListForm";
				}
				return activityLogForm;
			}
		}

		public static TransactionListForm InventoryLedgerFormObj
		{
			get
			{
				inventoryLedgerForm = new TransactionListForm();
				inventoryLedgerForm.ListType = TransactionListType.InventoryLedger;
				inventoryLedgerForm.Text = "Inventory Ledger";
				inventoryLedgerForm.Name = "InventoryLedgerForm";
				return inventoryLedgerForm;
			}
		}

		public static TransactionListForm PurchaseInvoiceNIListFormObj
		{
			get
			{
				if (purchaseInvoiceNIListForm == null || purchaseInvoiceNIListForm.IsDisposed)
				{
					purchaseInvoiceNIListForm = new TransactionListForm();
					purchaseInvoiceNIListForm.ListType = TransactionListType.PurchaseInvoiceNI;
					purchaseInvoiceNIListForm.Text = "Purchase Invoice List(NonInventory)";
					purchaseInvoiceNIListForm.Name = "PurchaseInvoiceNonInvListForm";
				}
				return purchaseInvoiceNIListForm;
			}
		}

		public static TransactionListForm PurchaseInvoiceListFormObj
		{
			get
			{
				if (purchaseInvoiceListForm == null || purchaseInvoiceListForm.IsDisposed)
				{
					purchaseInvoiceListForm = new TransactionListForm();
					purchaseInvoiceListForm.ListType = TransactionListType.PurchaseInvoice;
					purchaseInvoiceListForm.Text = "Purchase Invoice List";
					purchaseInvoiceListForm.Name = "PurchaseInvoiceListForm";
					purchaseInvoiceListForm.SelectedDocID = SelectedSysDocID;
				}
				return purchaseInvoiceListForm;
			}
		}

		public static TransactionListForm PurchaseOrderListFormObj
		{
			get
			{
				if (purchaseOrderListForm == null || purchaseOrderListForm.IsDisposed)
				{
					purchaseOrderListForm = new TransactionListForm();
					purchaseOrderListForm.ListType = TransactionListType.PurchaseOrder;
					purchaseOrderListForm.Text = "Purchase Order List";
					purchaseOrderListForm.Name = "PurchaseOrderListForm";
					purchaseOrderListForm.SelectedDocID = SelectedSysDocID;
				}
				return purchaseOrderListForm;
			}
		}

		public static TransactionListForm PurchaseOrderNIListFormObj
		{
			get
			{
				if (purchaseOrderNIListForm == null || purchaseOrderNIListForm.IsDisposed)
				{
					purchaseOrderNIListForm = new TransactionListForm();
					purchaseOrderNIListForm.ListType = TransactionListType.PurchaseOrderNI;
					purchaseOrderNIListForm.Text = "Purchase Order List(NonInventory)";
					purchaseOrderNIListForm.Name = "PurchaseOrderNIListForm";
				}
				return purchaseOrderNIListForm;
			}
		}

		public static TransactionListForm PurchasePackingListFormObj
		{
			get
			{
				if (purchasePackinglistListForm == null || purchasePackinglistListForm.IsDisposed)
				{
					purchasePackinglistListForm = new TransactionListForm();
					purchasePackinglistListForm.ListType = TransactionListType.POShipment;
					purchasePackinglistListForm.Text = "Purchase Packing List";
					purchasePackinglistListForm.Name = "PurchasePackingListForm";
				}
				return purchasePackinglistListForm;
			}
		}

		public static TransactionListForm ConsignInListFormObj
		{
			get
			{
				if (consignInListForm == null || consignInListForm.IsDisposed)
				{
					consignInListForm = new TransactionListForm();
					consignInListForm.ListType = TransactionListType.ConsignIn;
					consignInListForm.Text = "consignIn List";
					consignInListForm.Name = "ConsignInListForm";
				}
				return consignInListForm;
			}
		}

		public static TransactionListForm MaintenanceSchedulerListObj
		{
			get
			{
				if (maintenanceSchedulerListForm == null || maintenanceSchedulerListForm.IsDisposed)
				{
					maintenanceSchedulerListForm = new TransactionListForm();
					maintenanceSchedulerListForm.ListType = TransactionListType.MaintenanceScheduler;
					maintenanceSchedulerListForm.Name = "MaintenanceSchedulerForm";
				}
				return maintenanceSchedulerListForm;
			}
		}

		public static TransactionListForm MaintenanceEntryListObj
		{
			get
			{
				if (maintenanceEntryListForm == null || maintenanceEntryListForm.IsDisposed)
				{
					maintenanceEntryListForm = new TransactionListForm();
					maintenanceEntryListForm.ListType = TransactionListType.MaintenanceEntry;
					maintenanceEntryListForm.Name = "MaintenanceEntryForm";
				}
				return maintenanceEntryListForm;
			}
		}

		public static TransactionListForm ConsignInSettlementListFormObj
		{
			get
			{
				if (consignInSettlementListForm == null || consignInSettlementListForm.IsDisposed)
				{
					consignInSettlementListForm = new TransactionListForm();
					consignInSettlementListForm.ListType = TransactionListType.ConsignInSettlement;
					consignInSettlementListForm.Text = "consignIn List";
					consignInSettlementListForm.Name = "ConsignInListForm";
				}
				return consignInSettlementListForm;
			}
		}

		public static TransactionListForm QualityClaimListFormObj
		{
			get
			{
				if (qualityClaimListForm == null || qualityClaimListForm.IsDisposed)
				{
					qualityClaimListForm = new TransactionListForm();
					qualityClaimListForm.ListType = TransactionListType.QualityClaim;
					qualityClaimListForm.Text = "Quality Claim List";
					qualityClaimListForm.Name = "QualityClaimListForm";
				}
				return qualityClaimListForm;
			}
		}

		public static TransactionListForm ConsignmentOutListFormObj
		{
			get
			{
				if (consignmentOutListForm == null || consignmentOutListForm.IsDisposed)
				{
					consignmentOutListForm = new TransactionListForm();
					consignmentOutListForm.ListType = TransactionListType.ConsignOut;
					consignmentOutListForm.Text = "Consignment OutList Form";
					consignmentOutListForm.Name = "ConsignmentOutListForm";
				}
				return consignmentOutListForm;
			}
		}

		public static TransactionListForm ConsignmentOutSettlementListFormObj
		{
			get
			{
				if (consignmentOutSettlementListForm == null || consignmentOutSettlementListForm.IsDisposed)
				{
					consignmentOutSettlementListForm = new TransactionListForm();
					consignmentOutSettlementListForm.ListType = TransactionListType.ConsignOutSettlement;
					consignmentOutSettlementListForm.Text = "Consignment OutSettlementList Form";
					consignmentOutSettlementListForm.Name = "ConsignmentOutSettlementListForm";
				}
				return consignmentOutSettlementListForm;
			}
		}

		public static TransactionListForm ArrivalReportListFormObj
		{
			get
			{
				if (arrivalReportListForm == null || arrivalReportListForm.IsDisposed)
				{
					arrivalReportListForm = new TransactionListForm();
					arrivalReportListForm.ListType = TransactionListType.ArrivalReport;
					arrivalReportListForm.Text = "Arrival Report List";
					arrivalReportListForm.Name = "ArrivalReportListForm";
				}
				return arrivalReportListForm;
			}
		}

		public static TransactionListForm PropertyRentListFormObj
		{
			get
			{
				if (propertyRentListForm == null || propertyRentListForm.IsDisposed)
				{
					propertyRentListForm = new TransactionListForm();
					propertyRentListForm.ListType = TransactionListType.PropertyRental;
					propertyRentListForm.Text = "Property Rent List";
					propertyRentListForm.Name = "propertyRentListForm";
				}
				return propertyRentListForm;
			}
		}

		public static TransactionListForm PropertyRenewListFormObj
		{
			get
			{
				if (propertyRenewListForm == null || propertyRenewListForm.IsDisposed)
				{
					propertyRenewListForm = new TransactionListForm();
					propertyRenewListForm.ListType = TransactionListType.PropertyRenew;
					propertyRenewListForm.Text = "Property Renew List";
					propertyRenewListForm.Name = "propertyRenewListForm";
				}
				return propertyRenewListForm;
			}
		}

		public static TransactionListForm PropertyCancelListFormObj
		{
			get
			{
				if (propertyCancelListForm == null || propertyCancelListForm.IsDisposed)
				{
					propertyCancelListForm = new TransactionListForm();
					propertyCancelListForm.ListType = TransactionListType.PropertyCancel;
					propertyCancelListForm.Text = "Property Cancel List";
					propertyCancelListForm.Name = "propertyCancelListForm";
				}
				return propertyCancelListForm;
			}
		}

		public static TransactionListForm SalarySheetListFormObj
		{
			get
			{
				if (salarySheetListForm == null || salarySheetListForm.IsDisposed)
				{
					salarySheetListForm = new TransactionListForm();
					salarySheetListForm.ListType = TransactionListType.SalarySheet;
					salarySheetListForm.Text = "Salary Sheet List";
					salarySheetListForm.Name = "salarySheetListForm";
				}
				return salarySheetListForm;
			}
		}

		public static TransactionListForm OverTimeEntrytListFormObj
		{
			get
			{
				if (overTimeEntrytListForm == null || overTimeEntrytListForm.IsDisposed)
				{
					overTimeEntrytListForm = new TransactionListForm();
					overTimeEntrytListForm.ListType = TransactionListType.OverTimeEntry;
					overTimeEntrytListForm.Text = "Over Time Entry List";
					overTimeEntrytListForm.Name = "OverTimeEntryListForm";
					overTimeEntrytListForm.SelectedDocID = SelectedSysDocID;
				}
				return overTimeEntrytListForm;
			}
		}

		public static TransactionListForm EmployeeSalaryListFormObj
		{
			get
			{
				if (employeeSalaryListForm == null || employeeSalaryListForm.IsDisposed)
				{
					employeeSalaryListForm = new TransactionListForm();
					employeeSalaryListForm.ListType = TransactionListType.EmployeeSalary;
					employeeSalaryListForm.Text = "Employee Salary List";
					employeeSalaryListForm.Name = "EmployeeSalaryListForm";
				}
				return employeeSalaryListForm;
			}
		}

		public static TransactionListForm EmployeeLoanListFormObj
		{
			get
			{
				if (employeeLoanListForm == null || employeeLoanListForm.IsDisposed)
				{
					employeeLoanListForm = new TransactionListForm();
					employeeLoanListForm.ListType = TransactionListType.EmployeeLoan;
					employeeLoanListForm.Text = "Employee Loan List";
					employeeLoanListForm.Name = "EmployeeLoanListForm";
				}
				return employeeLoanListForm;
			}
		}

		public static TransactionListForm EmployeeEOSListFormObj
		{
			get
			{
				if (employeeEOSList == null || employeeEOSList.IsDisposed)
				{
					employeeEOSList = new TransactionListForm();
					employeeEOSList.ListType = TransactionListType.EmployeeEOS;
					employeeEOSList.Text = "Employee EOS List";
					employeeEOSList.Name = "EmployeeEOSListForm";
				}
				return employeeEOSList;
			}
		}

		public static TransactionListForm EmployeeLoanSettlementListFormObj
		{
			get
			{
				if (employeeLoanSettlementList == null || employeeLoanSettlementList.IsDisposed)
				{
					employeeLoanSettlementList = new TransactionListForm();
					employeeLoanSettlementList.ListType = TransactionListType.EmployeeLoanSettlement;
					employeeLoanSettlementList.Text = "Employee Loan Settlement List";
					employeeLoanSettlementList.Name = "EmployeeLoanSettlementList";
				}
				return employeeLoanSettlementList;
			}
		}

		public static TransactionListForm EmployeeLoanPaymentListFormObj
		{
			get
			{
				if (employeeLoanPaymentListForm == null || employeeLoanPaymentListForm.IsDisposed)
				{
					employeeLoanPaymentListForm = new TransactionListForm();
					employeeLoanPaymentListForm.ListType = TransactionListType.EmployeeLoanPayment;
					employeeLoanPaymentListForm.Text = "Employee Loan Payment List";
					employeeLoanPaymentListForm.Name = "EmployeeLoanPaymentListForm";
				}
				return employeeLoanPaymentListForm;
			}
		}

		public static TransactionListForm PurchaseQuoteListFormObj
		{
			get
			{
				if (purchaseQuoteListForm == null || purchaseQuoteListForm.IsDisposed)
				{
					purchaseQuoteListForm = new TransactionListForm();
					purchaseQuoteListForm.ListType = TransactionListType.PurchaseQuote;
					purchaseQuoteListForm.Text = "Purchase Quote List";
					purchaseQuoteListForm.Name = "PurchaseQuoteListForm";
				}
				return purchaseQuoteListForm;
			}
		}

		public static TransactionListForm PurchaseReturnListFormObj
		{
			get
			{
				if (purchaseReturnListForm == null || purchaseReturnListForm.IsDisposed)
				{
					purchaseReturnListForm = new TransactionListForm();
					purchaseReturnListForm.ListType = TransactionListType.PurchaseReturn;
					purchaseReturnListForm.Text = "Purchase Return List";
					purchaseReturnListForm.Name = "PurchaseReturnListForm";
				}
				return purchaseReturnListForm;
			}
		}

		public static TransactionListForm GRNReturnListFormObj
		{
			get
			{
				if (grnReturnListForm == null || grnReturnListForm.IsDisposed)
				{
					grnReturnListForm = new TransactionListForm();
					grnReturnListForm.ListType = TransactionListType.GRNReturn;
					grnReturnListForm.Text = "GRN Return List";
					grnReturnListForm.Name = "GRNReturnListForm";
				}
				return grnReturnListForm;
			}
		}

		public static TransactionListForm PurchaseGRNListFormObj
		{
			get
			{
				if (purchaseGRNListForm == null || purchaseGRNListForm.IsDisposed)
				{
					purchaseGRNListForm = new TransactionListForm();
					purchaseGRNListForm.ListType = TransactionListType.PurchaseGRN;
					purchaseGRNListForm.Text = "Purchase GRN List";
					purchaseGRNListForm.Name = "PurchaseGRNListForm";
					purchaseGRNListForm.SelectedDocID = SelectedSysDocID;
				}
				return purchaseGRNListForm;
			}
		}

		public static TransactionListForm ImportPurchaseGRNListFormObj
		{
			get
			{
				if (importPurchaseGRNListForm == null || importPurchaseGRNListForm.IsDisposed)
				{
					importPurchaseGRNListForm = new TransactionListForm();
					importPurchaseGRNListForm.ListType = TransactionListType.ImportGRN;
					importPurchaseGRNListForm.Text = "Import GRN List";
					importPurchaseGRNListForm.Name = "ImportPurchaseGRNListForm";
					importPurchaseGRNListForm.SelectedDocID = SelectedSysDocID;
				}
				return importPurchaseGRNListForm;
			}
		}

		public static TransactionListForm POShipmentListFormObj
		{
			get
			{
				if (poShipmentListForm == null || poShipmentListForm.IsDisposed)
				{
					poShipmentListForm = new TransactionListForm();
					poShipmentListForm.ListType = TransactionListType.POShipment;
					poShipmentListForm.Text = "Packing Lists";
					poShipmentListForm.Name = "POShipmentListForm";
				}
				return poShipmentListForm;
			}
		}

		public static POSCashExpenseForm POSCashExpenseFormObj
		{
			get
			{
				if (posCashExpenseForm == null || posCashExpenseForm.IsDisposed)
				{
					posCashExpenseForm = new POSCashExpenseForm();
				}
				return posCashExpenseForm;
			}
		}

		public static TransactionListForm APPaymentAllocationListFormObj
		{
			get
			{
				if (apAllocationListForm == null || apAllocationListForm.IsDisposed)
				{
					apAllocationListForm = new TransactionListForm();
					apAllocationListForm.ListType = TransactionListType.APPaymentAllocation;
					apAllocationListForm.Text = "Vendor Payment Allocation List";
					apAllocationListForm.Name = "APPaymentAllocationListForm";
				}
				return apAllocationListForm;
			}
		}

		public static TransactionListForm ProformaInvoiceListFormObj
		{
			get
			{
				if (proformaInvoiceListForm == null || proformaInvoiceListForm.IsDisposed)
				{
					proformaInvoiceListForm = new TransactionListForm();
					proformaInvoiceListForm.ListType = TransactionListType.ProformaInvoice;
					proformaInvoiceListForm.Text = "Proforma Invoice List";
					proformaInvoiceListForm.Name = "ProformaInvoiceListForm";
				}
				return proformaInvoiceListForm;
			}
		}

		public static TransactionListForm ImportPurchaseInvoiceListFormObj
		{
			get
			{
				if (importPurchaseInvoiceListForm == null || importPurchaseInvoiceListForm.IsDisposed)
				{
					importPurchaseInvoiceListForm = new TransactionListForm();
					importPurchaseInvoiceListForm.ListType = TransactionListType.ImportPurchaseInvoice;
					importPurchaseInvoiceListForm.Text = "Import Purchase Invoice List";
					importPurchaseInvoiceListForm.Name = "importPurchaseInvoiceListForm";
					importPurchaseInvoiceListForm.SelectedDocID = SelectedSysDocID;
				}
				return importPurchaseInvoiceListForm;
			}
		}

		public static TransactionListForm ImportPurchaseOrderListFormObj
		{
			get
			{
				if (importPurchaseOrderListForm == null || importPurchaseOrderListForm.IsDisposed)
				{
					importPurchaseOrderListForm = new TransactionListForm();
					importPurchaseOrderListForm.ListType = TransactionListType.ImportPurchaseOrder;
					importPurchaseOrderListForm.Text = "Import Purchase Order List";
					importPurchaseOrderListForm.Name = "importPurchaseOrderListForm";
				}
				return importPurchaseOrderListForm;
			}
		}

		public static TransactionListForm JVListObj
		{
			get
			{
				if (jvListForm == null || jvListForm.IsDisposed)
				{
					jvListForm = new TransactionListForm();
					jvListForm.ListType = TransactionListType.JournalEntry;
					jvListForm.Text = "General Journal Voucher List";
					jvListForm.Name = "JVListForm";
				}
				return jvListForm;
			}
		}

		public static TransactionListForm PaymentRequestListFormObj
		{
			get
			{
				if (paymentRequestListForm == null || paymentRequestListForm.IsDisposed)
				{
					paymentRequestListForm = new TransactionListForm();
					paymentRequestListForm.ListType = TransactionListType.PaymentRequest;
					paymentRequestListForm.Text = "Payment Request List";
					paymentRequestListForm.Name = "paymentRequestListForm";
				}
				return paymentRequestListForm;
			}
		}

		public static TransactionListForm TRApplicationListFormObj
		{
			get
			{
				if (trApplicationListForm == null || trApplicationListForm.IsDisposed)
				{
					trApplicationListForm = new TransactionListForm();
					trApplicationListForm.ListType = TransactionListType.TRApplication;
					trApplicationListForm.Text = "TR Application List";
					trApplicationListForm.Name = "TRApplicationListForm";
				}
				return trApplicationListForm;
			}
		}

		public static TransactionListForm ConsignInClosingListFormObj
		{
			get
			{
				if (consignInClosingListForm == null || consignInClosingListForm.IsDisposed)
				{
					consignInClosingListForm = new TransactionListForm();
					consignInClosingListForm.ListType = TransactionListType.ConsignIn;
					consignInClosingListForm.Text = "Purchase Claim List";
					consignInClosingListForm.Name = "purchaseClaimListForm";
				}
				return consignInClosingListForm;
			}
		}

		public static TransactionListForm PurchaseClaimListFormObj
		{
			get
			{
				if (purchaseClaimListForm == null || purchaseClaimListForm.IsDisposed)
				{
					purchaseClaimListForm = new TransactionListForm();
					purchaseClaimListForm.ListType = TransactionListType.PurchaseClaim;
					purchaseClaimListForm.Text = "Purchase Claim List";
					purchaseClaimListForm.Name = "purchaseClaimListForm";
				}
				return purchaseClaimListForm;
			}
		}

		public static TransactionListForm W3PLDeliveryListFormObj
		{
			get
			{
				if (w3PLDeliveryListForm == null || w3PLDeliveryListForm.IsDisposed)
				{
					w3PLDeliveryListForm = new TransactionListForm();
					w3PLDeliveryListForm.ListType = TransactionListType.W3PLDelivery;
					w3PLDeliveryListForm.Text = "3PL Delivery List";
					w3PLDeliveryListForm.Name = "w3PLDeliveryListForm";
				}
				return w3PLDeliveryListForm;
			}
		}

		public static TransactionListForm W3PLGRNListFormObj
		{
			get
			{
				if (w3PLGRNListForm == null || w3PLGRNListForm.IsDisposed)
				{
					w3PLGRNListForm = new TransactionListForm();
					w3PLGRNListForm.ListType = TransactionListType.W3PLGRN;
					w3PLGRNListForm.Text = "3PL GRN List";
					w3PLGRNListForm.Name = "w3PLGRNListForm";
				}
				return w3PLGRNListForm;
			}
		}

		public static TransactionListForm W3PLInvoiceListFormObj
		{
			get
			{
				if (w3PLInvoiceListForm == null || w3PLInvoiceListForm.IsDisposed)
				{
					w3PLInvoiceListForm = new TransactionListForm();
					w3PLInvoiceListForm.ListType = TransactionListType.W3PLInvoice;
					w3PLInvoiceListForm.Text = "3PL Invoice List";
					w3PLInvoiceListForm.Name = "w3PLInvoiceListForm";
				}
				return w3PLInvoiceListForm;
			}
		}

		public static TransactionListForm JournalListFormObj
		{
			get
			{
				if (journalListForm == null || journalListForm.IsDisposed)
				{
					journalListForm = new TransactionListForm();
					journalListForm.ListType = TransactionListType.Journal;
					journalListForm.Text = "Journal List";
					journalListForm.Name = "JournalListForm";
				}
				return journalListForm;
			}
		}

		public static TransactionListForm TrailBalanceListFormObj
		{
			get
			{
				if (trailBalanceListForm == null || trailBalanceListForm.IsDisposed)
				{
					trailBalanceListForm = new TransactionListForm();
					trailBalanceListForm.ListType = TransactionListType.TrialBalance;
					trailBalanceListForm.Text = "Trial Balance List";
					trailBalanceListForm.Name = "TrialBalanceListForm";
				}
				return trailBalanceListForm;
			}
		}

		public static TransactionListForm SendChequeToBankListFormObj
		{
			get
			{
				if (sendChequeToBankListForm == null || sendChequeToBankListForm.IsDisposed)
				{
					sendChequeToBankListForm = new TransactionListForm();
					sendChequeToBankListForm.ListType = TransactionListType.SendChequeToBank;
					sendChequeToBankListForm.Text = "Send Cheque To Bank List";
					sendChequeToBankListForm.Name = "SendChequeToBankListForm";
				}
				return sendChequeToBankListForm;
			}
		}

		public static TransactionListForm ChequeDepositListFormObj
		{
			get
			{
				if (chequeDepositListForm == null || chequeDepositListForm.IsDisposed)
				{
					chequeDepositListForm = new TransactionListForm();
					chequeDepositListForm.ListType = TransactionListType.ChequeDeposit;
					chequeDepositListForm.Text = "Cheque Deposit List";
					chequeDepositListForm.Name = "ChequeDepositListForm";
				}
				return chequeDepositListForm;
			}
		}

		public static TransactionListForm ChequeDiscountListFormObj
		{
			get
			{
				if (chequeDiscountListForm == null || chequeDiscountListForm.IsDisposed)
				{
					chequeDiscountListForm = new TransactionListForm();
					chequeDiscountListForm.ListType = TransactionListType.ChequeDiscount;
					chequeDiscountListForm.Text = "Cheque Discount List";
					chequeDiscountListForm.Name = "ChequeDiscountListForm";
				}
				return chequeDiscountListForm;
			}
		}

		public static TransactionListForm BillDiscountListFormObj
		{
			get
			{
				if (billDiscountListForm == null || billDiscountListForm.IsDisposed)
				{
					billDiscountListForm = new TransactionListForm();
					billDiscountListForm.ListType = TransactionListType.BillDiscount;
					billDiscountListForm.Text = "Bill Discount List";
					billDiscountListForm.Name = "BillDiscountListForm";
				}
				return billDiscountListForm;
			}
		}

		public static TransactionListForm ExpenseListFormObj
		{
			get
			{
				if (expenseListForm == null || expenseListForm.IsDisposed)
				{
					expenseListForm = new TransactionListForm();
					expenseListForm.ListType = TransactionListType.Expense;
					expenseListForm.Text = "Expense List";
					expenseListForm.Name = "ExpenseListForm";
					expenseListForm.SelectedDocID = SelectedSysDocID;
				}
				return expenseListForm;
			}
		}

		public static TransactionListForm ChequeReceiptMultiEntryListFormObj
		{
			get
			{
				if (chequeReceiptMultiEntryListForm == null || chequeReceiptMultiEntryListForm.IsDisposed)
				{
					chequeReceiptMultiEntryListForm = new TransactionListForm();
					chequeReceiptMultiEntryListForm.ListType = TransactionListType.ChequeReceiptMultiple;
					chequeReceiptMultiEntryListForm.Text = "Cheque Receipt List(Multiple)";
					chequeReceiptMultiEntryListForm.Name = "ChequeReceiptMultiEntryListForm";
					chequeReceiptMultiEntryListForm.SelectedDocID = SelectedSysDocID;
				}
				return chequeReceiptMultiEntryListForm;
			}
		}

		public static TransactionListForm DebitNoteListFormObj
		{
			get
			{
				if (debitNoteListForm == null || debitNoteListForm.IsDisposed)
				{
					debitNoteListForm = new TransactionListForm();
					debitNoteListForm.ListType = TransactionListType.DebitNote;
					debitNoteListForm.Text = "Debit Note List";
					debitNoteListForm.Name = "DebitNoteListForm";
					debitNoteListForm.SelectedDocID = SelectedSysDocID;
				}
				return debitNoteListForm;
			}
		}

		public static TransactionListForm CreditNoteListFormObj
		{
			get
			{
				if (creditNoteListForm == null || creditNoteListForm.IsDisposed)
				{
					creditNoteListForm = new TransactionListForm();
					creditNoteListForm.ListType = TransactionListType.CreditNote;
					creditNoteListForm.Text = "Credit Note List";
					creditNoteListForm.Name = "CreditNoteListForm";
				}
				return creditNoteListForm;
			}
		}

		public static TransactionListForm LPOReceiptListFormObj
		{
			get
			{
				if (LPOReceiptListForm == null || LPOReceiptListForm.IsDisposed)
				{
					LPOReceiptListForm = new TransactionListForm();
					LPOReceiptListForm.ListType = TransactionListType.LPOReceipt;
					LPOReceiptListForm.Text = "LPO Receipt List";
					LPOReceiptListForm.Name = "LPOReceiptListForm";
				}
				return LPOReceiptListForm;
			}
		}

		public static TransactionListForm ReceiptVoucherListFormObj
		{
			get
			{
				if (receiptVoucherListForm == null || receiptVoucherListForm.IsDisposed)
				{
					receiptVoucherListForm = new TransactionListForm();
					receiptVoucherListForm.ListType = TransactionListType.ReceiptVoucher;
					receiptVoucherListForm.Text = "Receipt Voucher List";
					receiptVoucherListForm.Name = "ReceiptVoucherListForm";
				}
				return receiptVoucherListForm;
			}
		}

		public static TransactionListForm ReceiptVoucherMultipleListFormObj
		{
			get
			{
				if (receiptVoucherListForm == null || receiptVoucherListForm.IsDisposed)
				{
					receiptVoucherListForm = new TransactionListForm();
					receiptVoucherListForm.ListType = TransactionListType.ReceiptVoucherMultiple;
					receiptVoucherListForm.Text = "Receipt Voucher List(Multiple)";
					receiptVoucherListForm.Name = "ReceiptVoucherMultipleListForm";
				}
				return receiptVoucherListForm;
			}
		}

		public static TransactionListForm ReturnVoucherListFormObj
		{
			get
			{
				if (returnVoucherListForm == null || returnVoucherListForm.IsDisposed)
				{
					returnVoucherListForm = new TransactionListForm();
					returnVoucherListForm.ListType = TransactionListType.ReturnVoucher;
					returnVoucherListForm.Text = "Return Voucher List";
					returnVoucherListForm.Name = "ReturnVoucherListForm";
				}
				return returnVoucherListForm;
			}
		}

		public static TransactionListForm IssuedChequeBounceListFormObj
		{
			get
			{
				if (issuedChequeBounceListForm == null || issuedChequeBounceListForm.IsDisposed)
				{
					issuedChequeBounceListForm = new TransactionListForm();
					issuedChequeBounceListForm.ListType = TransactionListType.IssuedChequeReturn;
					issuedChequeBounceListForm.Text = "Issued cheque Bounce List";
					issuedChequeBounceListForm.Name = "IssuedChequeBounceListForm";
				}
				return issuedChequeBounceListForm;
			}
		}

		public static TransactionListForm ReceivedChequeCancellationListFormObj
		{
			get
			{
				if (receivedChequeCancellationListForm == null || receivedChequeCancellationListForm.IsDisposed)
				{
					receivedChequeCancellationListForm = new TransactionListForm();
					receivedChequeCancellationListForm.ListType = TransactionListType.ReceivedChequeCancel;
					receivedChequeCancellationListForm.Text = "Received Cheque Cancelled List";
					receivedChequeCancellationListForm.Name = "ReceivedChequeCancellationListForm";
				}
				return receivedChequeCancellationListForm;
			}
		}

		public static TransactionListForm PaymentVoucherListObj
		{
			get
			{
				if (paymentVoucherListForm == null || paymentVoucherListForm.IsDisposed)
				{
					paymentVoucherListForm = new TransactionListForm();
					paymentVoucherListForm.ListType = TransactionListType.PaymentVoucher;
					paymentVoucherListForm.Text = "Payment Voucher List";
					paymentVoucherListForm.Name = "PaymentVoucherListForm";
				}
				return paymentVoucherListForm;
			}
		}

		public static TransactionListForm CRMActivityListObj
		{
			get
			{
				if (crmActivityListForm == null || crmActivityListForm.IsDisposed)
				{
					crmActivityListForm = new TransactionListForm();
					crmActivityListForm.ListType = TransactionListType.CRMActivity;
					crmActivityListForm.Text = "Activity List";
					crmActivityListForm.Name = "CRMActivityListForm";
				}
				return crmActivityListForm;
			}
		}

		public static TransactionListForm LegalActivityListObj
		{
			get
			{
				if (legalActivityListForm == null || legalActivityListForm.IsDisposed)
				{
					legalActivityListForm = new TransactionListForm();
					legalActivityListForm.ListType = TransactionListType.LegalActivity;
					legalActivityListForm.Text = "LegalActivity List";
					legalActivityListForm.Name = "LegalActivityListForm";
				}
				return legalActivityListForm;
			}
		}

		public static TransactionListForm LegalActionListObj
		{
			get
			{
				if (legalActionListForm == null || legalActionListForm.IsDisposed)
				{
					legalActionListForm = new TransactionListForm();
					legalActionListForm.ListType = TransactionListType.LegalAction;
					legalActionListForm.Text = "LegalAction List";
					legalActionListForm.Name = "LegalActionListForm";
					legalActionListForm.SelectedDocID = SelectedSysDocID;
				}
				return legalActionListForm;
			}
		}

		public static TransactionListForm CRMCustomerActivityListObj
		{
			get
			{
				if (crmCustomerActivityListForm == null || crmCustomerActivityListForm.IsDisposed)
				{
					crmCustomerActivityListForm = new TransactionListForm();
					crmCustomerActivityListForm.ListType = TransactionListType.CRMCustomerActivity;
					crmCustomerActivityListForm.Text = "Activity List";
					crmCustomerActivityListForm.Name = "CRMCustomerActivityListForm";
				}
				return crmCustomerActivityListForm;
			}
		}

		public static TransactionListForm TRListObj
		{
			get
			{
				if (trList == null || trList.IsDisposed)
				{
					trList = new TransactionListForm();
					trList.ListType = TransactionListType.TR;
					trList.Text = "TR List";
					trList.Name = "TRListForm";
				}
				return trList;
			}
		}

		public static TransactionListForm CLVoucherListFormObj
		{
			get
			{
				if (clVoucherListFormObj == null || clVoucherListFormObj.IsDisposed)
				{
					clVoucherListFormObj = new TransactionListForm();
					clVoucherListFormObj.ListType = TransactionListType.CLVoucher;
					clVoucherListFormObj.Text = "Credit Limit Vouchers List";
					clVoucherListFormObj.Name = "CLVoucherListForm";
				}
				return clVoucherListFormObj;
			}
		}

		public static TransactionListForm TRPaymentListObj
		{
			get
			{
				if (trPaymentList == null || trPaymentList.IsDisposed)
				{
					trPaymentList = new TransactionListForm();
					trPaymentList.ListType = TransactionListType.TRPayment;
					trPaymentList.Text = "TR Payment List";
					trPaymentList.Name = "TRPaymentListForm";
				}
				return trPaymentList;
			}
		}

		public static TransactionListForm ChequeReceiptListFormObj
		{
			get
			{
				if (receiptVoucherListForm == null || receiptVoucherListForm.IsDisposed)
				{
					receiptVoucherListForm = new TransactionListForm();
					receiptVoucherListForm.ListType = TransactionListType.ChequeReceipt;
					receiptVoucherListForm.Text = "Cheque Receipt List";
					receiptVoucherListForm.Name = "ChequeReceiptListForm";
				}
				return receiptVoucherListForm;
			}
		}

		public static TransactionListForm ChequePaymentListFormObj
		{
			get
			{
				if (paymentVoucherListForm == null || paymentVoucherListForm.IsDisposed)
				{
					paymentVoucherListForm = new TransactionListForm();
					paymentVoucherListForm.ListType = TransactionListType.ChequePayment;
					paymentVoucherListForm.Text = "Cheque Payment List";
					paymentVoucherListForm.Name = "ChequePaymentListForm";
				}
				return paymentVoucherListForm;
			}
		}

		public static TransactionListForm FundTransferListFormObj
		{
			get
			{
				if (fundTransferListForm == null || fundTransferListForm.IsDisposed)
				{
					fundTransferListForm = new TransactionListForm();
					fundTransferListForm.ListType = TransactionListType.FundTransfer;
					fundTransferListForm.Text = "Fund Transfer List";
					fundTransferListForm.Name = "FundTransferListForm";
				}
				return fundTransferListForm;
			}
		}

		public static TransactionListForm IssuedChequeListFormObj
		{
			get
			{
				if (issuedChequeListForm == null || issuedChequeListForm.IsDisposed)
				{
					issuedChequeListForm = new TransactionListForm();
					issuedChequeListForm.ListType = TransactionListType.IssuedCheque;
					issuedChequeListForm.Text = "Issued Cheque List";
					issuedChequeListForm.Name = "IssuedChequeListForm";
				}
				return issuedChequeListForm;
			}
		}

		public static TransactionListForm ReceivedChequeListFormObj
		{
			get
			{
				if (receivedChequeListForm == null || receivedChequeListForm.IsDisposed)
				{
					receivedChequeListForm = new TransactionListForm();
					receivedChequeListForm.ListType = TransactionListType.ReceivedCheque;
					receivedChequeListForm.Text = "Received Cheque List";
					receivedChequeListForm.Name = "ReceivedChequeListForm";
				}
				return receivedChequeListForm;
			}
		}

		public static TransactionListForm SecurityChequeListFormObj
		{
			get
			{
				if (securityChequeListForm == null || securityChequeListForm.IsDisposed)
				{
					securityChequeListForm = new TransactionListForm();
					securityChequeListForm.ListType = TransactionListType.SecurityCheque;
					securityChequeListForm.Text = "Security Cheque List";
					securityChequeListForm.Name = "SecurityChequeListForm";
				}
				return securityChequeListForm;
			}
		}

		public static TransactionListForm FixedAssetTransferListFormObj
		{
			get
			{
				if (fixedAssetTransferListForm == null || fixedAssetTransferListForm.IsDisposed)
				{
					fixedAssetTransferListForm = new TransactionListForm();
					fixedAssetTransferListForm.ListType = TransactionListType.FixedAssetTransfer;
					fixedAssetTransferListForm.Text = "Fixed Asset Transfer List";
					fixedAssetTransferListForm.Name = "FixedAssetTransferListForm";
				}
				return fixedAssetTransferListForm;
			}
		}

		public static TransactionListForm FixedAssetPurchaseListFormObj
		{
			get
			{
				if (fixedAssetPurchaseListForm == null || fixedAssetPurchaseListForm.IsDisposed)
				{
					fixedAssetPurchaseListForm = new TransactionListForm();
					fixedAssetPurchaseListForm.ListType = TransactionListType.FixedAssetPurchase;
					fixedAssetPurchaseListForm.Text = "Fixed Asset Aquesition List";
					fixedAssetPurchaseListForm.Name = "FixedAssetPurchaseListForm";
				}
				return fixedAssetPurchaseListForm;
			}
		}

		public static TransactionListForm FixedAssetPurchaseOrderListFormObj
		{
			get
			{
				if (fixedAssetPurchaseOrderListForm == null || fixedAssetPurchaseOrderListForm.IsDisposed)
				{
					fixedAssetPurchaseOrderListForm = new TransactionListForm();
					fixedAssetPurchaseOrderListForm.ListType = TransactionListType.FixedAssetPurchaseOrder;
					fixedAssetPurchaseOrderListForm.Text = "Fixed Asset Purchase Order List";
					fixedAssetPurchaseOrderListForm.Name = "FixedAssetPurchaseOrderListForm";
				}
				return fixedAssetPurchaseOrderListForm;
			}
		}

		public static TransactionListForm CashPurchaseListFormObj
		{
			get
			{
				if (cashPurchaseListForm == null || cashPurchaseListForm.IsDisposed)
				{
					cashPurchaseListForm = new TransactionListForm();
					cashPurchaseListForm.ListType = TransactionListType.CashPurchase;
					cashPurchaseListForm.Text = "Cash Purchase";
					cashPurchaseListForm.Name = "Cash PurchaseListForm";
					cashPurchaseListForm.SelectedDocID = SelectedSysDocID;
				}
				return cashPurchaseListForm;
			}
		}

		public static TransactionListForm FixedAssetSaleListFormObj
		{
			get
			{
				if (fixedAssetSaleListForm == null || fixedAssetSaleListForm.IsDisposed)
				{
					fixedAssetSaleListForm = new TransactionListForm();
					fixedAssetSaleListForm.ListType = TransactionListType.FixedAssetSale;
					fixedAssetSaleListForm.Text = "Fixed Asset Sale List";
					fixedAssetSaleListForm.Name = "FixedAssetSaleListForm";
				}
				return fixedAssetSaleListForm;
			}
		}

		public static TransactionListForm CashSalaryPaymentListFormObj
		{
			get
			{
				if (cashSalaryPaymentListForm == null || cashSalaryPaymentListForm.IsDisposed)
				{
					cashSalaryPaymentListForm = new TransactionListForm();
					cashSalaryPaymentListForm.ListType = TransactionListType.CashSalaryPayment;
					cashSalaryPaymentListForm.Text = "Cash Salary Payment List";
					cashSalaryPaymentListForm.Name = "CashSalaryPaymentListForm";
				}
				return cashSalaryPaymentListForm;
			}
		}

		public static TransactionListForm ChequeSalaryPaymentListFormObj
		{
			get
			{
				if (chequeSalaryPaymentListForm == null || chequeSalaryPaymentListForm.IsDisposed)
				{
					chequeSalaryPaymentListForm = new TransactionListForm();
					chequeSalaryPaymentListForm.ListType = TransactionListType.ChequeSalaryPayment;
					chequeSalaryPaymentListForm.Text = "Cheque Salary Payment List";
					chequeSalaryPaymentListForm.Name = "ChequeSalaryPaymentListForm";
				}
				return chequeSalaryPaymentListForm;
			}
		}

		public static TransactionListForm TransferSalaryPaymentListFormObj
		{
			get
			{
				if (transferSalaryPaymentListForm == null || transferSalaryPaymentListForm.IsDisposed)
				{
					transferSalaryPaymentListForm = new TransactionListForm();
					transferSalaryPaymentListForm.ListType = TransactionListType.TransferSalaryPayment;
					transferSalaryPaymentListForm.Text = "Bank Salary Payment List";
					transferSalaryPaymentListForm.Name = "BankSalaryPaymentListForm";
				}
				return transferSalaryPaymentListForm;
			}
		}

		public static TransactionListForm ProjectExpenseAllocationListFormObj
		{
			get
			{
				if (projectExpenseAllocationListForm == null || projectExpenseAllocationListForm.IsDisposed)
				{
					projectExpenseAllocationListForm = new TransactionListForm();
					projectExpenseAllocationListForm.ListType = TransactionListType.JobExpenseAllocation;
					projectExpenseAllocationListForm.Text = "Project Expense Allocation List";
					projectExpenseAllocationListForm.Name = "ProjectExpenseAllocationListForm";
				}
				return projectExpenseAllocationListForm;
			}
		}

		public static TransactionListForm JobTimesheetListFormObj
		{
			get
			{
				if (jobTimesheetListForm == null || jobTimesheetListForm.IsDisposed)
				{
					jobTimesheetListForm = new TransactionListForm();
					jobTimesheetListForm.ListType = TransactionListType.JobTimesheet;
					jobTimesheetListForm.Text = "job Timesheet List";
					jobTimesheetListForm.Name = "jobTimesheetListForm";
				}
				return jobTimesheetListForm;
			}
		}

		public static TransactionListForm employeeGeneralActiviyListFormObj
		{
			get
			{
				if (employeeGeneralActivityListForm == null || employeeGeneralActivityListForm.IsDisposed)
				{
					employeeGeneralActivityListForm = new TransactionListForm();
					employeeGeneralActivityListForm.ListType = TransactionListType.EmployeeGeneralActivity;
					employeeGeneralActivityListForm.Text = " GeneralActivity List";
					employeeGeneralActivityListForm.Name = "EmployeeGeneralActivityListForm";
				}
				return employeeGeneralActivityListForm;
			}
		}

		public static TransactionListForm JobInventoryReturnListFormObj
		{
			get
			{
				if (jobInventoryReturnListForm == null || jobInventoryReturnListForm.IsDisposed)
				{
					jobInventoryReturnListForm = new TransactionListForm();
					jobInventoryReturnListForm.ListType = TransactionListType.JobInventoryReturn;
					jobInventoryReturnListForm.Text = "jobInventory Return List";
					jobInventoryReturnListForm.Name = "jobInventoryReturnList";
				}
				return jobInventoryReturnListForm;
			}
		}

		public static TransactionListForm JobClosingListFormObj
		{
			get
			{
				if (jobClosingListForm == null || jobClosingListForm.IsDisposed)
				{
					jobClosingListForm = new TransactionListForm();
					jobClosingListForm.ListType = TransactionListType.JobClosing;
					jobClosingListForm.Text = "Closed Job List";
					jobClosingListForm.Name = "jobClosedList";
				}
				return jobClosingListForm;
			}
		}

		public static TransactionListForm JobExpenseIssueListFormObj
		{
			get
			{
				if (jobExpenseIssueListForm == null || jobExpenseIssueListForm.IsDisposed)
				{
					jobExpenseIssueListForm = new TransactionListForm();
					jobExpenseIssueListForm.ListType = TransactionListType.JobExpenseIssue;
					jobExpenseIssueListForm.Text = "job ExpenseIssue List";
					jobExpenseIssueListForm.Name = "jobExpenseIssueList";
				}
				return jobExpenseIssueListForm;
			}
		}

		public static SubContractPurchaseByItemVendorBuyerReport SubContractPIReportFormObj
		{
			get
			{
				if (subContractPIReportForm == null || subContractPIReportForm.IsDisposed)
				{
					subContractPIReportForm = new SubContractPurchaseByItemVendorBuyerReport();
					if (showForm)
					{
						subContractPIReportForm.Show();
					}
				}
				return subContractPIReportForm;
			}
		}

		public static ProjectSubContractPOReportForm SubContractPOReportFormObj
		{
			get
			{
				if (subContractPOReportForm == null || subContractPOReportForm.IsDisposed)
				{
					subContractPOReportForm = new ProjectSubContractPOReportForm();
					if (showForm)
					{
						subContractPOReportForm.Show();
					}
				}
				return subContractPOReportForm;
			}
		}

		public static TransactionListForm JobMaterialEstimateListFormObj
		{
			get
			{
				if (jobMaterialEstimateListForm == null || jobMaterialEstimateListForm.IsDisposed)
				{
					jobMaterialEstimateListForm = new TransactionListForm();
					jobMaterialEstimateListForm.ListType = TransactionListType.JobMaterialEstimate;
					jobMaterialEstimateListForm.Text = "job MaterialEstimate List";
					jobMaterialEstimateListForm.Name = "jobMaterialEstimateListForm";
				}
				return jobMaterialEstimateListForm;
			}
		}

		public static TransactionListForm JobManHrsBudgetingListFormObj
		{
			get
			{
				if (jobManHrsBudgetingListForm == null || jobManHrsBudgetingListForm.IsDisposed)
				{
					jobManHrsBudgetingListForm = new TransactionListForm();
					jobManHrsBudgetingListForm.ListType = TransactionListType.JobManHrsBudgeting;
					jobManHrsBudgetingListForm.Text = "Job Man Hrs Budgeting List";
					jobManHrsBudgetingListForm.Name = "jobManHrsBudgetingListForm";
				}
				return jobManHrsBudgetingListForm;
			}
		}

		public static TransactionListForm JobMaintenanceScheduleListFormObj
		{
			get
			{
				if (jobMaintenanceScheduleListForm == null || jobMaintenanceScheduleListForm.IsDisposed)
				{
					jobMaintenanceScheduleListForm = new TransactionListForm();
					jobMaintenanceScheduleListForm.ListType = TransactionListType.JobMaintenanceSchedule;
					jobMaintenanceScheduleListForm.Text = "job MaintenanceSchedule List";
					jobMaintenanceScheduleListForm.Name = "jobMaintenanceScheduleListForm";
				}
				return jobMaintenanceScheduleListForm;
			}
		}

		public static TransactionListForm JobMaintenanceServiceEntryListFormObj
		{
			get
			{
				if (jobMaintenanceServiceEntryListForm == null || jobMaintenanceServiceEntryListForm.IsDisposed)
				{
					jobMaintenanceServiceEntryListForm = new TransactionListForm();
					jobMaintenanceServiceEntryListForm.ListType = TransactionListType.JobMaintenanceEntry;
					jobMaintenanceServiceEntryListForm.Text = "job MaintenanceServiceEntry List";
					jobMaintenanceServiceEntryListForm.Name = "jobMaintenanceServiceEntryListForm";
				}
				return jobMaintenanceServiceEntryListForm;
			}
		}

		public static TransactionListForm ServiceCallTrackListFormObj
		{
			get
			{
				if (serviceCallTrackListForm == null || serviceCallTrackListForm.IsDisposed)
				{
					serviceCallTrackListForm = new TransactionListForm();
					serviceCallTrackListForm.ListType = TransactionListType.ServiceCallTrack;
					serviceCallTrackListForm.Text = "serviceCallTrackList";
					serviceCallTrackListForm.Name = "serviceCallTrackListForm";
				}
				return serviceCallTrackListForm;
			}
		}

		public static TransactionListForm JobMaterialRequistionListFormObj
		{
			get
			{
				if (jobMaterialRequistionListForm == null || jobMaterialRequistionListForm.IsDisposed)
				{
					jobMaterialRequistionListForm = new TransactionListForm();
					jobMaterialRequistionListForm.ListType = TransactionListType.JobMaterialRequisition;
					jobMaterialRequistionListForm.Text = "jobMaterialRequistion List";
					jobMaterialRequistionListForm.Name = "jobMaterialRequistionListForm";
				}
				return jobMaterialRequistionListForm;
			}
		}

		public static TransactionListForm BuildAssemblyListFormObj
		{
			get
			{
				if (buildAssemblyListForm == null || buildAssemblyListForm.IsDisposed)
				{
					buildAssemblyListForm = new TransactionListForm();
					buildAssemblyListForm.ListType = TransactionListType.AssemblyBuild;
					buildAssemblyListForm.Text = "Build Assembly List";
					buildAssemblyListForm.Name = "buildAssemblyListForm";
				}
				return buildAssemblyListForm;
			}
		}

		public static TransactionListForm ProductionListFormObj
		{
			get
			{
				if (productionListForm == null || productionListForm.IsDisposed)
				{
					productionListForm = new TransactionListForm();
					productionListForm.ListType = TransactionListType.Production;
					productionListForm.Text = "Production List";
					productionListForm.Name = "productionListForm";
				}
				return productionListForm;
			}
		}

		public static TransactionListForm GarmentRentalListFormObj
		{
			get
			{
				if (garmentRentalListForm == null || garmentRentalListForm.IsDisposed)
				{
					garmentRentalListForm = new TransactionListForm();
					garmentRentalListForm.ListType = TransactionListType.GarmentRental;
					garmentRentalListForm.Text = "GarmentRental List";
					garmentRentalListForm.Name = "GarmentRentalListForm";
				}
				return garmentRentalListForm;
			}
		}

		public static TransactionListForm GarmentRentalReturnListFormObj
		{
			get
			{
				if (garmentRentalReturnListForm == null || garmentRentalReturnListForm.IsDisposed)
				{
					garmentRentalReturnListForm = new TransactionListForm();
					garmentRentalReturnListForm.ListType = TransactionListType.GarmentRentalReturn;
					garmentRentalReturnListForm.Text = "GarmentRental Return List";
					garmentRentalReturnListForm.Name = "GarmentRentalReturnListForm";
				}
				return garmentRentalReturnListForm;
			}
		}

		public static TransactionListForm EmployeeAppraisalListFormObj
		{
			get
			{
				if (employeeAppraisalListForm == null || employeeAppraisalListForm.IsDisposed)
				{
					employeeAppraisalListForm = new TransactionListForm();
					employeeAppraisalListForm.ListType = TransactionListType.EmployeeAppraisal;
					employeeAppraisalListForm.Text = "EmployeeAppraisal List";
					employeeAppraisalListForm.Name = "EmployeeAppraisalForm";
				}
				return employeeAppraisalListForm;
			}
		}

		public static TransactionListForm EmployeeProvisionListFormObj
		{
			get
			{
				if (employeeProvisionListForm == null || employeeProvisionListForm.IsDisposed)
				{
					employeeProvisionListForm = new TransactionListForm();
					employeeProvisionListForm.ListType = TransactionListType.EmployeeProvision;
					employeeProvisionListForm.Text = "Employee Provision List";
					employeeProvisionListForm.Name = "EmployeeProvisionEntryForm";
				}
				return employeeProvisionListForm;
			}
		}

		public static TransactionListForm EmployeeLeavePaymentListFormObj
		{
			get
			{
				if (employeeLeavePaymentListForm == null || employeeLeavePaymentListForm.IsDisposed)
				{
					employeeLeavePaymentListForm = new TransactionListForm();
					employeeLeavePaymentListForm.ListType = TransactionListType.LeaveSalaryPayemnt;
					employeeLeavePaymentListForm.Text = "Employee Leave Payment List";
					employeeLeavePaymentListForm.Name = "LeaveSalaryPaymentForm";
				}
				return employeeLeavePaymentListForm;
			}
		}

		public static TransactionListForm PurchaseCostEntryListFormObj
		{
			get
			{
				if (purchaseCostEntryListForm == null || purchaseCostEntryListForm.IsDisposed)
				{
					purchaseCostEntryListForm = new TransactionListForm();
					purchaseCostEntryListForm.ListType = TransactionListType.PurchaseCostEntry;
					purchaseCostEntryListForm.Text = "Purchase Cost Entry List";
					purchaseCostEntryListForm.Name = "purchaseCostEntryListForm";
				}
				return purchaseCostEntryListForm;
			}
		}

		public static TransactionListForm BillOfLadingListFormObj
		{
			get
			{
				if (billOfLadingListForm == null || billOfLadingListForm.IsDisposed)
				{
					billOfLadingListForm = new TransactionListForm();
					billOfLadingListForm.ListType = TransactionListType.BillOfLading;
					billOfLadingListForm.Text = "Bill Of Lading List";
					billOfLadingListForm.Name = "BillOfLadingListForm";
				}
				return billOfLadingListForm;
			}
		}

		public static TransactionListForm CustomerInsuranceListFormObj
		{
			get
			{
				if (customerInsuranceListForm == null || customerInsuranceListForm.IsDisposed)
				{
					customerInsuranceListForm = new TransactionListForm();
					customerInsuranceListForm.ListType = TransactionListType.CustomerInsurance;
					customerInsuranceListForm.Text = "Customer Insurance List";
					customerInsuranceListForm.Name = "CustomerInsuranceListForm";
				}
				return customerInsuranceListForm;
			}
		}

		public static TransactionListForm ProjectSubContractListFormObj
		{
			get
			{
				if (projectSubContractListForm == null || projectSubContractListForm.IsDisposed)
				{
					projectSubContractListForm = new TransactionListForm();
					projectSubContractListForm.ListType = TransactionListType.ProjectSubContractPO;
					projectSubContractListForm.Text = "Project SubContract";
					projectSubContractListForm.Name = "ProjectSubContractOrderForm";
				}
				return projectSubContractListForm;
			}
		}

		public static TransactionListForm ProjectSubContractInvoiceListFormObj
		{
			get
			{
				if (projectSubContractInvoiceListForm == null || projectSubContractInvoiceListForm.IsDisposed)
				{
					projectSubContractInvoiceListForm = new TransactionListForm();
					projectSubContractInvoiceListForm.ListType = TransactionListType.ProjectSubContractPI;
					projectSubContractInvoiceListForm.Text = "Project SubContract";
					projectSubContractInvoiceListForm.Name = "ProjectSubContracTInvoiceForm";
				}
				return projectSubContractInvoiceListForm;
			}
		}

		public static TransactionListForm WorkOrderListFormObj
		{
			get
			{
				if (workOrderListForm == null || workOrderListForm.IsDisposed)
				{
					workOrderListForm = new TransactionListForm();
					workOrderListForm.ListType = TransactionListType.WorkOrder;
					workOrderListForm.Text = "Work Order List";
					workOrderListForm.Name = "WorkOrderListForm";
				}
				return workOrderListForm;
			}
		}

		public static TransactionListForm EmployeePerformanceListFormObj
		{
			get
			{
				if (employeePerformanceListForm == null || employeePerformanceListForm.IsDisposed)
				{
					employeePerformanceListForm = new TransactionListForm();
					employeePerformanceListForm.ListType = TransactionListType.EmployeePerformance;
					employeePerformanceListForm.Text = "Employee Performance List";
					employeePerformanceListForm.Name = "EmployeePerformanceListForm";
				}
				return employeePerformanceListForm;
			}
		}

		public static TransactionListForm EmployeeLeaveProcessListFormObj
		{
			get
			{
				if (employeeLeaveProcessListForm == null || employeeLeaveProcessListForm.IsDisposed)
				{
					employeeLeaveProcessListForm = new TransactionListForm();
					employeeLeaveProcessListForm.ListType = TransactionListType.EmployeeLeave;
					employeeLeaveProcessListForm.Text = "Employee Leave Process List";
					employeeLeaveProcessListForm.Name = "EmployeeLeaveProcessListForm";
				}
				return employeeLeaveProcessListForm;
			}
		}

		public static TransactionListForm PriceListListFormObj
		{
			get
			{
				if (priceListListForm == null || priceListListForm.IsDisposed)
				{
					priceListListForm = new TransactionListForm();
					priceListListForm.ListType = TransactionListType.PriceList;
					priceListListForm.Text = "Price List";
					priceListListForm.Name = "PriceListListForm";
				}
				return priceListListForm;
			}
		}

		public static TransactionListForm FreightChargesListFormObj
		{
			get
			{
				if (freightChargesListForm == null || freightChargesListForm.IsDisposed)
				{
					freightChargesListForm = new TransactionListForm();
					freightChargesListForm.ListType = TransactionListType.FreightCharges;
					freightChargesListForm.Text = "Freight Chrages List";
					freightChargesListForm.Name = "FreightChargesListForm";
				}
				return freightChargesListForm;
			}
		}

		public static TransactionListForm RequisitionListFormObj
		{
			get
			{
				if (requisitionListFormObj == null || requisitionListFormObj.IsDisposed)
				{
					requisitionListFormObj = new TransactionListForm();
					requisitionListFormObj.ListType = TransactionListType.Requisition;
					requisitionListFormObj.Text = "Requisition List";
					requisitionListFormObj.Name = "RequisitionListForm";
				}
				return requisitionListFormObj;
			}
		}

		public static TransactionListForm MobilizationListFormObj
		{
			get
			{
				if (mobilizationListFormObj == null || mobilizationListFormObj.IsDisposed)
				{
					mobilizationListFormObj = new TransactionListForm();
					mobilizationListFormObj.ListType = TransactionListType.Mobilization;
					mobilizationListFormObj.Text = "Mobilization List";
					mobilizationListFormObj.Name = "MobilizationListForm";
				}
				return mobilizationListFormObj;
			}
		}

		public static TransactionListForm FixedAssetBulkPurchaseListFormObj
		{
			get
			{
				if (FixedAssetBulkPurchaseListForm == null || FixedAssetBulkPurchaseListForm.IsDisposed)
				{
					FixedAssetBulkPurchaseListForm = new TransactionListForm();
					FixedAssetBulkPurchaseListForm.ListType = TransactionListType.FixedAssetBulkPurchase;
					FixedAssetBulkPurchaseListForm.Text = "FixedAsset Bulk Purchase List";
					FixedAssetBulkPurchaseListForm.Name = "FixedAssetBulkPurchase";
				}
				return FixedAssetBulkPurchaseListForm;
			}
		}

		public static TransactionListForm FixedAssetDepListFormObj
		{
			get
			{
				if (FixedAssetDepListForm == null || FixedAssetDepListForm.IsDisposed)
				{
					FixedAssetDepListForm = new TransactionListForm();
					FixedAssetDepListForm.ListType = TransactionListType.FixedAssetDep;
					FixedAssetDepListForm.Text = "FixedAsset Dep List";
					FixedAssetDepListForm.Name = "FixedAssetDep";
				}
				return FixedAssetDepListForm;
			}
		}

		public static TransactionListForm IssuedChequeClearanceListFormObj
		{
			get
			{
				if (issuedChequeClearanceListForm == null || issuedChequeClearanceListForm.IsDisposed)
				{
					issuedChequeClearanceListForm = new TransactionListForm();
					issuedChequeClearanceListForm.ListType = TransactionListType.IssuedChequeClearance;
					issuedChequeClearanceListForm.Text = "Issued Cheque Clearance List";
					issuedChequeClearanceListForm.Name = "IssuedChequeClearance";
				}
				return issuedChequeClearanceListForm;
			}
		}

		public static TransactionListForm EquipmentTransferListFormObj
		{
			get
			{
				if (equipmentTransferListFormObj == null || equipmentTransferListFormObj.IsDisposed)
				{
					equipmentTransferListFormObj = new TransactionListForm();
					equipmentTransferListFormObj.ListType = TransactionListType.EquipmentTransfer;
					equipmentTransferListFormObj.Text = "Equipment Transfer List";
					equipmentTransferListFormObj.Name = "EquipmentTransferListForm";
				}
				return equipmentTransferListFormObj;
			}
		}

		public static TransactionListForm EquipmentWorkOrderListFormObj
		{
			get
			{
				if (equipmentWorkOrderListForm == null || equipmentWorkOrderListForm.IsDisposed)
				{
					equipmentWorkOrderListForm = new TransactionListForm();
					equipmentWorkOrderListForm.ListType = TransactionListType.EquipmentWorkOrder;
					equipmentWorkOrderListForm.Text = "Equipment Work Order List";
					equipmentWorkOrderListForm.Name = "EquipmentWorkOrderListForm";
				}
				return equipmentWorkOrderListForm;
			}
		}

		public static TransactionListForm InventoryAdjustmentListFormObj
		{
			get
			{
				if (inventoryAdjustmentListForm == null || inventoryAdjustmentListForm.IsDisposed)
				{
					inventoryAdjustmentListForm = new TransactionListForm();
					inventoryAdjustmentListForm.ListType = TransactionListType.InventoryAdjustment;
					inventoryAdjustmentListForm.Text = "inventory Adjustment List";
					inventoryAdjustmentListForm.Name = "inventoryAdjustmentListForm";
				}
				return inventoryAdjustmentListForm;
			}
		}

		public static TransactionListForm CustomerOpeningBalanceBatchListFormObj
		{
			get
			{
				if (customerOpeningBalanceBatchListForm == null || customerOpeningBalanceBatchListForm.IsDisposed)
				{
					customerOpeningBalanceBatchListForm = new TransactionListForm();
					customerOpeningBalanceBatchListForm.ListType = TransactionListType.CustomerOpeningBalance;
					customerOpeningBalanceBatchListForm.Text = "Customer Opening Balance Batch List";
					customerOpeningBalanceBatchListForm.Name = "CustomerOpeningBalanceLeaveListForm";
				}
				return customerOpeningBalanceBatchListForm;
			}
		}

		public static TransactionListForm VendorOpeningBalanceBatchListFormObj
		{
			get
			{
				if (vendorOpeningBalanceBatchListForm == null || vendorOpeningBalanceBatchListForm.IsDisposed)
				{
					vendorOpeningBalanceBatchListForm = new TransactionListForm();
					vendorOpeningBalanceBatchListForm.ListType = TransactionListType.VendorOpeningBalance;
					vendorOpeningBalanceBatchListForm.Text = "Vendor Opening Balance Batch List";
					vendorOpeningBalanceBatchListForm.Name = "VendorOpeningBalanceLeaveListForm";
				}
				return vendorOpeningBalanceBatchListForm;
			}
		}

		public static TransactionListForm EmployeeOpeningBalanceBatchListFormObj
		{
			get
			{
				if (employeeOpeningBalanceBatchListForm == null || employeeOpeningBalanceBatchListForm.IsDisposed)
				{
					employeeOpeningBalanceBatchListForm = new TransactionListForm();
					employeeOpeningBalanceBatchListForm.ListType = TransactionListType.EmployeeOpeningBalance;
					employeeOpeningBalanceBatchListForm.Text = "Employee Opening Balance Batch List";
					employeeOpeningBalanceBatchListForm.Name = "EmployeeOpeningBalanceListForm";
				}
				return employeeOpeningBalanceBatchListForm;
			}
		}

		public static TransactionListForm InventoryOpeningBalanceBatchListFormObj
		{
			get
			{
				if (inventoryOpeningBalanceBatchListForm == null || inventoryOpeningBalanceBatchListForm.IsDisposed)
				{
					inventoryOpeningBalanceBatchListForm = new TransactionListForm();
					inventoryOpeningBalanceBatchListForm.ListType = TransactionListType.InventoryOpeningBalance;
					inventoryOpeningBalanceBatchListForm.Text = "Inventory Opening Balance Batch List";
					inventoryOpeningBalanceBatchListForm.Name = "InventoryOpeningBalanceListForm";
				}
				return inventoryOpeningBalanceBatchListForm;
			}
		}

		public static TransactionListForm WorkOrderInventoryIssueListFormObj
		{
			get
			{
				if (workOrderInventoryIssueListForm == null || workOrderInventoryIssueListForm.IsDisposed)
				{
					workOrderInventoryIssueListForm = new TransactionListForm();
					workOrderInventoryIssueListForm.ListType = TransactionListType.WorkOrderInventoryIssue;
					workOrderInventoryIssueListForm.Text = "Work Order Inventory Issue List";
					workOrderInventoryIssueListForm.Name = "WorkOrderInventoryIssueListForm";
				}
				return workOrderInventoryIssueListForm;
			}
		}

		public static TransactionListForm WorkOrderInventoryReturnListFormObj
		{
			get
			{
				if (workOrderInventoryReturnListForm == null || workOrderInventoryReturnListForm.IsDisposed)
				{
					workOrderInventoryReturnListForm = new TransactionListForm();
					workOrderInventoryReturnListForm.ListType = TransactionListType.WorkOrderInventoryReturn;
					workOrderInventoryReturnListForm.Text = "Work Order Inventory Return List";
					workOrderInventoryReturnListForm.Name = "WorkOrderInventoryReturnListForm";
				}
				return workOrderInventoryReturnListForm;
			}
		}

		public static TransactionListForm ConsignOutReturnListFormObj
		{
			get
			{
				if (consignOutReturnListForm == null || consignOutReturnListForm.IsDisposed)
				{
					consignOutReturnListForm = new TransactionListForm();
					consignOutReturnListForm.ListType = TransactionListType.ConsignOutReturn;
					consignOutReturnListForm.Text = "Consignment Out Return List";
					consignOutReturnListForm.Name = "ConsignmentOutReturnList";
				}
				return consignOutReturnListForm;
			}
		}

		public static TransactionListForm SalaryAdditionListFormObj
		{
			get
			{
				if (SalaryAdditionListForm == null || SalaryAdditionListForm.IsDisposed)
				{
					SalaryAdditionListForm = new TransactionListForm();
					SalaryAdditionListForm.ListType = TransactionListType.SalaryAddition;
					SalaryAdditionListForm.Text = "Salary Addition List";
					SalaryAdditionListForm.Name = "SalaryAdditionList";
				}
				return SalaryAdditionListForm;
			}
		}

		public static TransactionListForm SalaryDeductionListFormObj
		{
			get
			{
				if (salaryDeductionListForm == null || salaryDeductionListForm.IsDisposed)
				{
					salaryDeductionListForm = new TransactionListForm();
					salaryDeductionListForm.ListType = TransactionListType.SalaryDeduction;
					salaryDeductionListForm.Text = "Salary Deduction List";
					salaryDeductionListForm.Name = "SalaryDeductionList";
				}
				return salaryDeductionListForm;
			}
		}

		public static TransactionListForm InventoryDismantleListFormObj
		{
			get
			{
				if (inventoryDismantleListForm == null || inventoryDismantleListForm.IsDisposed)
				{
					inventoryDismantleListForm = new TransactionListForm();
					inventoryDismantleListForm.ListType = TransactionListType.InventoryDismantle;
					inventoryDismantleListForm.Text = "Inventory Dismantle List";
					inventoryDismantleListForm.Name = "InventoryDismantleList";
				}
				return inventoryDismantleListForm;
			}
		}

		public static TransactionListForm ProdutPriceBulkUpdateListFormObj
		{
			get
			{
				if (produtPriceBulkUpdateListForm == null || produtPriceBulkUpdateListForm.IsDisposed)
				{
					produtPriceBulkUpdateListForm = new TransactionListForm();
					produtPriceBulkUpdateListForm.ListType = TransactionListType.ProductPriceBulkupdate;
					produtPriceBulkUpdateListForm.Text = "Produt Price Bulk Update List";
					produtPriceBulkUpdateListForm.Name = "ProdutPriceBulkUpdateList";
				}
				return produtPriceBulkUpdateListForm;
			}
		}

		public static TransactionListForm ChequeReceiptOpeningListFormObj
		{
			get
			{
				if (chequeReceiptOpeningListForm == null || chequeReceiptOpeningListForm.IsDisposed)
				{
					chequeReceiptOpeningListForm = new TransactionListForm();
					chequeReceiptOpeningListForm.ListType = TransactionListType.ChequeReceiptOpening;
					chequeReceiptOpeningListForm.Text = "Receipt Voucher List";
					chequeReceiptOpeningListForm.Name = "ReceiptVoucherListForm";
				}
				return chequeReceiptOpeningListForm;
			}
		}

		public static TransactionListForm ChequePaymentOpeningVoucherListObj
		{
			get
			{
				if (chequepaymentVoucherListForm == null || chequepaymentVoucherListForm.IsDisposed)
				{
					chequepaymentVoucherListForm = new TransactionListForm();
					chequepaymentVoucherListForm.ListType = TransactionListType.ChequePaymentOpening;
					chequepaymentVoucherListForm.Text = "Payment Voucher List";
					chequepaymentVoucherListForm.Name = "PaymentVoucherListForm";
				}
				return chequepaymentVoucherListForm;
			}
		}

		public static TransactionListForm MaterialResevationListObj
		{
			get
			{
				if (materialResevationList == null || materialResevationList.IsDisposed)
				{
					materialResevationList = new TransactionListForm();
					materialResevationList.ListType = TransactionListType.MaterialReservation;
					materialResevationList.Text = "Material Reservation List";
					materialResevationList.Name = "MaterialReservationList";
				}
				return materialResevationList;
			}
		}

		public static TransactionListForm SalesForecastingListObj
		{
			get
			{
				if (SalesForecastingList == null || SalesForecastingList.IsDisposed)
				{
					SalesForecastingList = new TransactionListForm();
					SalesForecastingList.ListType = TransactionListType.SalesForecasting;
					SalesForecastingList.Text = "Sales Forecasting List";
					SalesForecastingList.Name = "SalesForecastingList";
				}
				return SalesForecastingList;
			}
		}

		public static TransactionListForm PurchasePrepaymentInvoiceListObj
		{
			get
			{
				if (purchasePrepaymentInvoiceList == null || purchasePrepaymentInvoiceList.IsDisposed)
				{
					purchasePrepaymentInvoiceList = new TransactionListForm();
					purchasePrepaymentInvoiceList.ListType = TransactionListType.PurchasePrepaymentInvoice;
					purchasePrepaymentInvoiceList.Text = "Purchase Prepayment InvoiceList";
					purchasePrepaymentInvoiceList.Name = "PurchasePrepaymentInvoiceList";
				}
				return purchasePrepaymentInvoiceList;
			}
		}

		public static TransactionListForm TaskTransactionListFormObj
		{
			get
			{
				if (taskTransactionList == null || taskTransactionList.IsDisposed)
				{
					taskTransactionList = new TransactionListForm();
					taskTransactionList.ListType = TransactionListType.TaskTransaction;
					taskTransactionList.Text = "Task Transaction List";
					taskTransactionList.Name = "TaskTransactionList";
				}
				return taskTransactionList;
			}
		}

		public static VendorCategoryDetailsForm VendorCategoryFormObj
		{
			get
			{
				if (vendorCategoryForm == null || vendorCategoryForm.IsDisposed)
				{
					vendorCategoryForm = new VendorCategoryDetailsForm();
					vendorCategoryForm.EntityType = EntityTypesEnum.Vendors;
				}
				return vendorCategoryForm;
			}
		}

		public static TransactionListForm BudgetingListFormObj
		{
			get
			{
				if (budgetingListForm == null || budgetingListForm.IsDisposed)
				{
					budgetingListForm = new TransactionListForm();
					budgetingListForm.ListType = TransactionListType.Budgeting;
					budgetingListForm.Text = "Budgeting List";
					budgetingListForm.Name = "BudgetingListForm";
					budgetingListForm.SelectedDocID = SelectedSysDocID;
				}
				return budgetingListForm;
			}
		}

		public static TransactionListForm VehicleMileageTrackListFormObj
		{
			get
			{
				if (vehiclemileagetrackListForm == null || vehiclemileagetrackListForm.IsDisposed)
				{
					vehiclemileagetrackListForm = new TransactionListForm();
					vehiclemileagetrackListForm.ListType = TransactionListType.VehicleMileageTrack;
					vehiclemileagetrackListForm.Text = "Vehicle Mileage Track List";
					vehiclemileagetrackListForm.Name = "VehicleMileageTrackListForm";
				}
				return vehiclemileagetrackListForm;
			}
		}

		public static TransactionListForm SalesManTargetListFormObj
		{
			get
			{
				if (salesmantargetListForm == null || salesmantargetListForm.IsDisposed)
				{
					salesmantargetListForm = new TransactionListForm();
					salesmantargetListForm.ListType = TransactionListType.SalesManTarget;
					salesmantargetListForm.Text = "Sales Man Target List";
					salesmantargetListForm.Name = "SalesManTargetListForm";
				}
				return salesmantargetListForm;
			}
		}

		public static TransactionListForm CustomerInsuranceClaimListFormObj
		{
			get
			{
				if (customerInsuranceClaimListForm == null || customerInsuranceClaimListForm.IsDisposed)
				{
					customerInsuranceClaimListForm = new TransactionListForm();
					customerInsuranceClaimListForm.ListType = TransactionListType.CustomerInsuranceClaim;
					customerInsuranceClaimListForm.Text = "Customer Insurance Claim List";
					customerInsuranceClaimListForm.Name = "CustomerInsuranceClaimListForm";
				}
				return customerInsuranceClaimListForm;
			}
		}

		public static TransactionListForm StandingJournalListFormObj
		{
			get
			{
				if (standingJournalListForm == null || standingJournalListForm.IsDisposed)
				{
					standingJournalListForm = new TransactionListForm();
					standingJournalListForm.ListType = TransactionListType.StandingJournal;
					standingJournalListForm.Text = "Standing Journal List";
					standingJournalListForm.Name = "StandingJournalListForm";
				}
				return standingJournalListForm;
			}
		}

		public static TransactionListForm LoanEntryListFormObj
		{
			get
			{
				if (loanentryListForm == null || loanentryListForm.IsDisposed)
				{
					loanentryListForm = new TransactionListForm();
					loanentryListForm.ListType = TransactionListType.LoanEntry;
					loanentryListForm.Text = "Loan Entry List";
					loanentryListForm.Name = "LoanEntryListForm";
				}
				return loanentryListForm;
			}
		}

		public static TransactionListForm EmployeeLeaveRequestListFormObj
		{
			get
			{
				if (employeeLeaveRequestListForm == null || employeeLeaveRequestListForm.IsDisposed)
				{
					employeeLeaveRequestListForm = new TransactionListForm();
					employeeLeaveRequestListForm.ListType = TransactionListType.LeaveRequest;
					employeeLeaveRequestListForm.Text = "Employee Leave Request List";
					employeeLeaveRequestListForm.Name = "EmployeeLeaveRequestListForm";
				}
				return employeeLeaveRequestListForm;
			}
		}

		public static TransactionListForm IssuedChequeCancellationListFormObj
		{
			get
			{
				if (issuedChequeCancellationListForm == null || issuedChequeCancellationListForm.IsDisposed)
				{
					issuedChequeCancellationListForm = new TransactionListForm();
					issuedChequeCancellationListForm.ListType = TransactionListType.IssuedChequeCancellation;
					issuedChequeCancellationListForm.Text = "Issued Cheque Cancellation List";
					issuedChequeCancellationListForm.Name = "IssuedChequeCancellation";
				}
				return issuedChequeCancellationListForm;
			}
		}

		public static TransactionListForm PropertyServiceRequestListFormObj
		{
			get
			{
				if (propertyServiceRequestListForm == null || propertyServiceRequestListForm.IsDisposed)
				{
					propertyServiceRequestListForm = new TransactionListForm();
					propertyServiceRequestListForm.ListType = TransactionListType.PropertyServiceRequest;
					propertyServiceRequestListForm.Text = "Property Service Request List";
					propertyServiceRequestListForm.Name = "PropertyServiceRequestListForm";
				}
				return propertyServiceRequestListForm;
			}
		}

		public static TransactionListForm PropertyServiceAssignListFormObj
		{
			get
			{
				if (propertyServiceAssignListForm == null || propertyServiceAssignListForm.IsDisposed)
				{
					propertyServiceAssignListForm = new TransactionListForm();
					propertyServiceAssignListForm.ListType = TransactionListType.PropertyServiceAssign;
					propertyServiceAssignListForm.Text = "Property Service Assign List";
					propertyServiceAssignListForm.Name = "PropertyServiceAssignListForm";
				}
				return propertyServiceAssignListForm;
			}
		}

		public static SalesByCustomerFilterReport SalesByCustomerReportFormObj
		{
			get
			{
				if (salesByCustomerReportForm == null || salesByCustomerReportForm.IsDisposed)
				{
					salesByCustomerReportForm = new SalesByCustomerFilterReport();
				}
				return salesByCustomerReportForm;
			}
		}

		public static bool IsLoadCompelete => loadComplete;

		public static Form ParentForm
		{
			get
			{
				return formParent;
			}
			set
			{
				formParent = value;
			}
		}

		public static Hashtable CustomerData
		{
			get
			{
				if (customerData == null)
				{
					customerData = new Hashtable();
					customerData.Add("Create Invoices", "CustomerInvoicesFormObj");
					customerData.Add("Receive Payments", "ReceivePaymentsFormObj");
					customerData.Add("Create Orders", "CustomerOrdersFormObj");
					customerData.Add("Create Credit Memos/Refunds", "CustomerRefundFormObj");
					customerData.Add("Create Statements", "CustomerStatementFormObj");
					customerData.Add("Invoice List", "CustomerInvoicesListFormObj");
					customerData.Add("Receipt List", "CustomerReceiptsListFormObj");
					customerData.Add("Order List", "CustomerOrdersListFormObj");
					customerData.Add("Create Delivery Note", "DeliveryNoteFormObj");
					customerData.Add("Delivery Note List", "DeliveryNoteListFormObj");
					customerData.Add("Create Sales Quote", "SalesQuoteFormObj");
					customerData.Add("Sales Quote List", "SalesQuoteListFormObj");
					customerData.Add("Unposted Cheques", "UnpostedReceivedChecksListFormObj");
					customerData.Add("Credit Memo List", "CustomerCreditMemoListFormObj");
					customerData.Add("Payment Transactions", "CustomerPaymentTransactionsFormObj");
					customerData.Add("Received Cheques", "CustomerChecksFormObj");
					customerData.Add("Customer List", "CustomerListFormObj");
				}
				return customerData;
			}
		}

		public static Hashtable VendorData
		{
			get
			{
				if (vendorData == null)
				{
					vendorData = new Hashtable();
					vendorData.Add("Enter Bills && Receive Items", "PurchaseInvoicesFormObj");
					vendorData.Add("Create Orders", "PurchaseOrderFormObj");
					vendorData.Add("Create Credit Memos/Refunds", "VendorRefundFormObj");
					vendorData.Add("Pay Bills", "PayBillsFormObj");
					vendorData.Add("Bill List", "PurchaseInvoiceListFormObj");
					vendorData.Add("Order List", "PurchaseOrderListFormObj");
					vendorData.Add("Paid Cheques", "VendorPaidChecksFormObj");
					vendorData.Add("Payment Transactions", "VendorPaymentTransactionsFormObj");
					vendorData.Add("Vendor List", "VendorsListFormObj");
					vendorData.Add("Unposted Paid Cheques", "UnpostedPaidChecksListFormObj");
				}
				return vendorData;
			}
		}

		public static Hashtable InventoryData
		{
			get
			{
				if (inventoryData == null)
				{
					inventoryData = new Hashtable();
					inventoryData.Add("Transfer Items", "TransferItemsFormObj");
					inventoryData.Add("Adjust Inventory", "InventoryAdjustmentsFormObj");
					if (GlobalRules.IsFeatureAllowed(EditionFeatures.Assembly))
					{
						inventoryData.Add("Build Assembly", "BuildAssemblyFormObj");
					}
					inventoryData.Add("Item List", "ProductsListFormObj");
					inventoryData.Add("Category List", "ProductCategoryListFormObj");
					inventoryData.Add("Store List", "StoresListFormObj");
				}
				return inventoryData;
			}
		}

		public static Hashtable CompanyData
		{
			get
			{
				if (companyData == null)
				{
					companyData = new Hashtable();
					companyData.Add("Company Information", "CompanyInformationFormObj");
					companyData.Add("Create New User...", "NewDBUserFormObj");
					companyData.Add("Contact List", "ContactsListFormObj");
					companyData.Add("Notes", "NoteFormObj");
					companyData.Add("Options", "DetailsSettingsFormObj");
					companyData.Add("Edit User Access Rights...", "UserAccessRightsDetailsFormObj");
					companyData.Add("Change Password...", "ChangePasswordFormObj");
					companyData.Add("User List", "UsersListFormObj");
					companyData.Add("Back Up Data...", "BackupDatabaseFormObj");
					companyData.Add("Restore Data...", "RestoreDatabaseFormObj");
					companyData.Add("Find Transactions", "FindFormObj");
					companyData.Add("Unit List", "UnitsListFormObj");
					companyData.Add("ShippingMethod List", "ShippingMethodsListFormObj");
					companyData.Add("Employee List", "EmployeesListFormObj");
					companyData.Add("Pay Employees", "PayrollEntryFormObj");
					companyData.Add("Department List", "DepartmentListFormObj");
				}
				return companyData;
			}
		}

		public static Hashtable AccountData
		{
			get
			{
				if (accountData == null)
				{
					accountData = new Hashtable();
					accountData.Add("Chart of Accounts", "CompanyAccountsListFormObj");
					accountData.Add("Account Transactions", "AccountExplorerFormObj");
					accountData.Add("Enter Expenses", "CashExpenseEntryFormObj");
					accountData.Add("Make Journal Entry", "JournalEntryFormObj");
					accountData.Add("Make Deposits", "BankTransactionFormMakeDepositsObj");
					accountData.Add("Make Withdrawal", "BankTransactionFormMakeWithdrawalObj");
					accountData.Add("Transfer Money", "BankTransactionFormTransferMoneyObj");
					accountData.Add("Reconcile Bank Account", "BeginReconciliationFormObj");
					accountData.Add("Account List", "CompanyAccountsListFormObj");
					accountData.Add("Expense List", "ExpenseListFormObj");
					accountData.Add("Journal List", "JournalListFormObj");
					accountData.Add("Bank List", "BanksListFormObj");
				}
				return accountData;
			}
		}

		public static Hashtable ReportData
		{
			get
			{
				if (reportData == null)
				{
					reportData = new Hashtable();
					reportData.Add("Balance Sheet", "BalanceSheetReportObj");
					reportData.Add("Profit && Loss", "ProfitLossReportObj");
					reportData.Add("Statement of Cash Flow", "StatementOfCashFlowReportObj");
					reportData.Add("Cash Flow Forecast", "CashFlowForecastReportObj");
					reportData.Add("Bank Casj Flow Summary", "CashFlowSummaryReportObj");
					reportData.Add("Bank Cash Flow Detail", "CashFlowDetailReportObj");
					reportData.Add("Receivable Aging Summary", "ARAgingSummaryReportObj");
					reportData.Add("Receivable Aging Detail", "ARAgingDetailsReportObj");
					reportData.Add("Customer Balance", "CustomerBalanceReportObj");
					reportData.Add("Customer Balance Details", "CustomerInvoicesBalanceReportObj");
					reportData.Add("Open Invoices", "OpenInvoicesReportObj");
					reportData.Add("Accounts Receivable Journal", "CustomerTransactionsReportObj");
					reportData.Add("Customer Cheques", "CustomerChecksReportObj");
					reportData.Add("Customer Returned Cheques", "CustomerBouncedChecksReportObj");
					reportData.Add("Customer Contact List", "CustomerContactReportObj");
					reportData.Add("Customer Phone List", "CustomerPhoneReportObj");
					reportData.Add("Customer List", "CustomersReportObj");
					reportData.Add("Sales By Customer", "SalesByCustomerSummaryReportObj");
					reportData.Add("Sales By Customer Details", "SalesByCustomerDetailsReportObj");
					reportData.Add("Sales by Item", "SalesByItemSummaryReportObj");
					reportData.Add("Sales by Item Details", "SalesByItemDetailsReportObj");
					reportData.Add("Sales by Item and Customer Summary", "SalesByItemsByCustomersSummaryReportObj");
					reportData.Add("Sales by Item and Customer Detail", "SalesByItemsByCustomersDetailsReportObj");
					reportData.Add("Sales by Location Summary", "SalesByStoreSummaryReportObj");
					reportData.Add("Sales by Store Details", "SalesByStoreDetailsReportObj");
					reportData.Add("Sales by Salesperson", "SalesByEmployeeReportObj");
					reportData.Add("Sales by Salesperson Details", "SalesByEmployeeDetailsReportObj");
					reportData.Add("Sales by Category Summary", "SalesByCategorySummaryReportObj");
					reportData.Add("Sales by Category Detail", "SalesByCategoryDetailsReportObj");
					reportData.Add("Sales History Summary", "SalesHistorySummaryReportObj");
					reportData.Add("Sales History Detail", "SalesHistoryDetailsReportObj");
					reportData.Add("Payables Aging Summary", "APAgingSummaryReportObj");
					reportData.Add("Payables Aging Detail", "APAgingDetailsReportObj");
					reportData.Add("Vendor Balance", "VendorBalanceSummaryReportObj");
					reportData.Add("Vendor Balance Details", "VendorBalanceDetailsReportObj");
					reportData.Add("Unpaid Bills", "OpenBillsReportObj");
					reportData.Add("Accounts Payable Jounal", "VendorTransactionsReportObj");
					reportData.Add("Vendor Contact List", "VendorContactReportObj");
					reportData.Add("Vendor Phone List", "VendorPhoneReportObj");
					reportData.Add("Vendor List", "VendorsReportObj");
					reportData.Add("Purchases by Vendor", "PurchasesByVendorSummaryReportObj");
					reportData.Add("Purchases by Vendor Details", "PurchasesByVendorDetailsReportObj");
					reportData.Add("Purchases by Item", "PurchasesByItemSummaryReportObj");
					reportData.Add("Purchases by Item Details", "PurchasesByItemDetailsReportObj");
					reportData.Add("Purchases by Item and Vendor Summary", "PurchasesByItemsByVendorsSummaryReportObj");
					reportData.Add("Purchases by Item and Vendor Detail", "PurchasesByItemsByVendorsDetailsReportObj");
					reportData.Add("Open Purchase Orders", "OpenPurchaseOrdersReportObj");
					reportData.Add("Employee Earnings Summary", "TransactionsByEmployeeSummaryReportObj");
					reportData.Add("Employee Earnings Detail", "TransactionsByEmployeeDetailsReportObj");
					reportData.Add("Payroll Item Summary", "TransactionsByPayrollItemSummaryReportObj");
					reportData.Add("Payroll Item Detail", "TransactionsByPayrollItemDetailsReportObj");
					reportData.Add("Pay Code Summary", "TransactionsByPayCodeSummaryReportObj");
					reportData.Add("Pay Code Detail", "TransactionsByPayCodeDetailsReportObj");
					reportData.Add("Employee Contact List", "EmployeeContactReportObj");
					reportData.Add("Payroll Item List", "PayrollItemListReportObj");
					reportData.Add("Employee Visa Detail", "EmployeeVisaDetailsReportObj");
					reportData.Add("Employee Passport Detail", "EmployeePassportDetailsReportObj");
					reportData.Add("Inventory Transaction Summary", "InventoryTransactionsSummaryReportObj");
					reportData.Add("Inventory Transaction Detail", "InventoryTransactionsReportObj");
					reportData.Add("Inventory Flow Summary", "InventoryFlowSummaryReportObj");
					reportData.Add("Inventory Flow Detail", "InventoryFlowReportObj");
					reportData.Add("Inventory Flow by Container", "InventoryFlowByContainerReportObj");
					reportData.Add("Stock Status", "ItemsStockStatusListReportObj");
					reportData.Add("Inventory Stock Status by Location", "ItemsStockStatusListByStoreReportObj");
					reportData.Add("Inventory Stock Status History", "ItemsStockStatusHistoryReportObj");
					reportData.Add("Physical Inventory Checklist", "PhysicalInventoryCheckListReportObj");
					reportData.Add("Physical Inventory Checklist by Location", "PhysicalInventoryCheckListByStoreReportObj");
					reportData.Add("Item Cost List", "ItemsCostListReportObj");
					reportData.Add("Item Price List", "ItemsPriceListReportObj");
					reportData.Add("Item List", "ItemsListReportObj");
					if (GlobalRules.IsFeatureAllowed(EditionFeatures.Assembly))
					{
						reportData.Add("Kit Component List", "KitComponentListReportObj");
						reportData.Add("Assembly Component List", "AssemblyComponentListReportObj");
					}
					reportData.Add("Trial Balance", "TrialBalanceReportObj");
					reportData.Add("General Ledger", "GLReportObj");
					reportData.Add("Journal", "GeneralJournalReportObj");
					reportData.Add("Chart of Accounts", "AccountsListReportObj");
					reportData.Add("Transaction Detail by Account", "TransactionDetailByAccountReportObj");
				}
				return reportData;
			}
		}

		public static Form MainForm => formParent;

		public static formHome HomeForm => homeForm;

		public static string SelectedSysDocID
		{
			get
			{
				return selectedsysDocID;
			}
			set
			{
				selectedsysDocID = value;
			}
		}

		public static event EventHandler ScreenShow;

		public static event EventHandler ScreenClose;

		public static void SetShowForm(bool val)
		{
			showForm = val;
		}

		public static CustomReportDisplayForm GetCustomReportForm(string id)
		{
			CustomReportDisplayForm value = new CustomReportDisplayForm();
			if (customReportFormList.ContainsKey(id))
			{
				customReportFormList.TryGetValue(id, out value);
				return value;
			}
			value = new CustomReportDisplayForm();
			value.Name = id;
			value.FormClosed += customReportForm_FormClosed;
			customReportFormList.Add(id, value);
			value.LoadReport(id);
			return value;
		}

		private static void customReportForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			string name = ((Form)sender).Name;
			if (customReportFormList.ContainsKey(name))
			{
				customReportFormList.Remove(name);
			}
		}

		public static Hashtable GetShortcutData(string category)
		{
			if (category.Trim().ToLower() == "customer")
			{
				return CustomerData;
			}
			if (category.Trim().ToLower() == "vendor")
			{
				return VendorData;
			}
			if (category.Trim().ToLower() == "inventory")
			{
				return InventoryData;
			}
			if (category.Trim().ToLower() == "company")
			{
				return CompanyData;
			}
			if (category.Trim().ToLower() == "account")
			{
				return AccountData;
			}
			if (category.Trim().ToLower() == "report")
			{
				return ReportData;
			}
			return null;
		}

		public static Form RunProperty(string propertyName)
		{
			Type typeFromHandle = typeof(FormActivator);
			Form form = null;
			try
			{
				form = (Form)typeFromHandle.InvokeMember(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null);
				if (form == null)
				{
					return form;
				}
				BringFormToFront(form);
				return form;
			}
			catch
			{
				return null;
			}
		}

		public static void LoadAllForms(formMain parent)
		{
			formParent = parent;
			loadComplete = false;
			homeForm = new formHome(parent);
			homeForm.Show();
			LoadFields();
			loadComplete = true;
		}

		private static void LoadFields()
		{
			if (fields == null)
			{
				fields = new Hashtable();
			}
			else
			{
				fields.Clear();
			}
			FieldInfo[] array = typeof(FormActivator).GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
			foreach (FieldInfo fieldInfo in array)
			{
				if (!fields.Contains(fieldInfo.FieldType.Name))
				{
					fields.Add(fieldInfo.FieldType.Name, fieldInfo.Name);
				}
			}
		}

		public static bool ResetForm(Form form)
		{
			if (form == null)
			{
				return true;
			}
			try
			{
				IDataEntry dataEntry = form as IDataEntry;
				if (dataEntry != null && !dataEntry.CanClose())
				{
					return false;
				}
			}
			catch
			{
			}
			if (form.GetType() != typeof(formMain) && form.GetType() != typeof(formHome))
			{
				(form as IDataList)?.ClearView();
				if (!form.IsDisposed)
				{
					form.Close();
				}
				form = null;
			}
			return true;
		}

		public static bool ResetAllForms(Form exceptionForm)
		{
			if (exceptionForm == null)
			{
				return ResetAllForms();
			}
			Type typeFromHandle = typeof(FormActivator);
			FieldInfo[] array = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
			Form form = null;
			FieldInfo[] array2 = array;
			foreach (FieldInfo fieldInfo in array2)
			{
				try
				{
					form = (typeFromHandle.InvokeMember(fieldInfo.Name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField, null, null, null) as Form);
					if (form != null && form.GetType() != typeof(formMain) && form.GetType() != typeof(formHome) && form.GetType() != exceptionForm.GetType())
					{
						ResetForm(form);
					}
				}
				catch
				{
				}
			}
			return true;
		}

		public static bool ResetAllForms()
		{
			Type typeFromHandle = typeof(FormActivator);
			FieldInfo[] array = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
			Form form = null;
			FieldInfo[] array2 = array;
			foreach (FieldInfo fieldInfo in array2)
			{
				try
				{
					form = (typeFromHandle.InvokeMember(fieldInfo.Name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField, null, null, null) as Form);
					if (form != null && form.GetType() != typeof(formMain) && form.GetType() != typeof(formHome))
					{
						ResetForm(form);
					}
				}
				catch
				{
				}
			}
			return true;
		}

		public static Form GetForm(string formName)
		{
			Type typeFromHandle = typeof(FormActivator);
			try
			{
				string str = "Obj";
				PropertyInfo property = typeFromHandle.GetProperty(formName + str, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
				if (property != null)
				{
					return property.GetValue(null, null) as Form;
				}
			}
			catch
			{
			}
			return null;
		}

		public static Form GetForm(string formName, string postFix)
		{
			Type typeFromHandle = typeof(FormActivator);
			try
			{
				string str = "";
				if (postFix != null)
				{
					str = postFix.ToString();
				}
				PropertyInfo property = typeFromHandle.GetProperty(formName + str, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
				if (property != null)
				{
					return property.GetValue(null, null) as Form;
				}
			}
			catch
			{
			}
			return null;
		}

		private static void DisplayAccessDenied()
		{
			ErrorHelper.WarningMessage("You do not have permission to view this screen.");
		}

		private static bool HasAccessRight(Form form)
		{
			if (Security.HasAccessRight(UIRefelector.GetScreenID(form), suppressMessage: true))
			{
				return true;
			}
			return false;
		}

		public static bool BringFormToFront(string strName)
		{
			Form form = GetForm(strName, "Obj");
			if (form != null)
			{
				return BringFormToFront(form);
			}
			return false;
		}

		public static bool BringFormToFront(Form form)
		{
			if (form == null || form.IsDisposed)
			{
				return false;
			}
			if (form.GetType() == typeof(formMain) || form.GetType() == typeof(formHome) || form.GetType() == typeof(HelpSupportForm))
			{
				if (!form.Visible)
				{
					form.Visible = true;
				}
				form.BringToFront();
				return true;
			}
			if (UserPreferences.OpenMDI)
			{
				form.MdiParent = MainForm;
			}
			if (!form.Visible)
			{
				form.Visible = true;
			}
			form.Shown += form_Shown;
			form.FormClosed += form_FormClosed;
			if (form.WindowState == FormWindowState.Minimized)
			{
				form.WindowState = FormWindowState.Normal;
			}
			Translator.Translators.Translate(form);
			Translator.Translators.SubstituteFormName(form);
			form.BringToFront();
			form.Activate();
			form.Focus();
			return true;
		}

		private static void SolveMenuBug(Form form)
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			contextMenuStrip.Items.Add(" ");
			Point location = form.Location;
			Point position = new Point(location.X, location.Y);
			form.ContextMenuStrip = contextMenuStrip;
			form.ContextMenuStrip.Show(form, position);
			form.ContextMenuStrip.Hide();
		}

		private static void form_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (FormActivator.ScreenClose != null)
			{
				FormActivator.ScreenClose(sender, e);
			}
		}

		private static void form_Shown(object sender, EventArgs e)
		{
			if (FormActivator.ScreenShow != null)
			{
				FormActivator.ScreenShow(sender, e);
			}
		}

		public static void SendFormToBack(Form form)
		{
			form?.SendToBack();
		}

		static FormActivator()
		{
			FormActivator.ScreenShow = null;
			FormActivator.ScreenClose = null;
			showForm = false;
			fields = null;
			customerData = null;
			vendorData = null;
			inventoryData = null;
			companyData = null;
			accountData = null;
			reportData = null;
			loadComplete = true;
			formParent = null;
			programLoaded = false;
			homeForm = null;
			posHomeForm = null;
			bankDetailsForm = null;
			companyAccountDetailsForm = null;
			companyAccountsListForm = null;
			employeeDetailsForm = null;
			customerDetailsForm = null;
			crmCustomerDetailsForm = null;
			vendorDetailsForm = null;
			vendorClassDetailsForm = null;
			productDetailsForm = null;
			productBrandDetailsForm = null;
			shipperDetailsForm = null;
			productCategoryDetailsForm = null;
			journalEntryForm = null;
			customerBalanceSummaryReport = null;
			customerBalanceDetailsReport = null;
			vendorBalanceSummaryReport = null;
			vendorBalanceDetailsReport = null;
			glReport = null;
			inventoryAdjustmentForm = null;
			companyInformationForm = null;
			accountGroupDetailsForm = null;
			analysisGroupDetailsForm = null;
			analysisDetailsForm = null;
			helpSupportForm = null;
			productManufacturerDetailsForm = null;
			productStyleDetailsForm = null;
			productSpecificationDetailsForm = null;
			productSizeDetailsForm = null;
			productAttributeDetailsForm = null;
			expenseEntryForm = null;
			budgetingForm = null;
			vehiclemileagetrackForm = null;
			salesmantargetForm = null;
			customerinsuranceClaimForm = null;
			contactDetailsForm = null;
			printPreviewForm = null;
			priceLevelDetailsForm = null;
			paymentMethodDetailsForm = null;
			taskStepsForm = null;
			taskTypeForm = null;
			taskTransactionForm = null;
			taskTransactionStatusForm = null;
			countryDetailsForm = null;
			customerClassDetailsForm = null;
			areaDetailsForm = null;
			paymentTermDetailsForm = null;
			customerAddressDetailsForm = null;
			salespersonDetailsForm = null;
			vendorAddressDetailsForm = null;
			buyerDetailsForm = null;
			productClassDetailsForm = null;
			unitDetailsForm = null;
			smartListForm = null;
			externalReportForm = null;
			salesByGRNReport = null;
			paymentRequestForm = null;
			salesByGRNSummaryReport = null;
			projectDueReportForm = null;
			employeeFinalSettlementReport = null;
			propertyRegistrationReport = null;
			propertyRenewalReport = null;
			propertyCancellationReport = null;
			propertyAvailabilityReport = null;
			projectManPowerReport = null;
			gradeDetailsForm = null;
			sponsorDetailsForm = null;
			nationalityDetailsForm = null;
			religionDetailsForm = null;
			divisionDetailsForm = null;
			positionDetailsForm = null;
			employeeDocTypeDetailsForm = null;
			patientDocTypeDetailsForm = null;
			billDiscountForm = null;
			vehicleDocTypeDetailsForm = null;
			degreeDetailsForm = null;
			skillDetailsForm = null;
			departmentDetailsForm = null;
			customerGroupDetailsForm = null;
			vendorGroupDetailsForm = null;
			employeeGroupDetailsForm = null;
			employeeTypeDetailsForm = null;
			employeeAddressDetailsForm = null;
			employeeEOSSettlementForm = null;
			employeeEOSForm = null;
			locationDetailsForm = null;
			smsAlertForm = null;
			workLocationDetailsForm = null;
			provisionTypeDetailsForm = null;
			employeeProvisionForm = null;
			jobTaskDetailsForm = null;
			jobTaskGroupDetailsForm = null;
			companyDivisionDetailsForm = null;
			printTemplateMappingForm = null;
			salesByItemCustomerSalespersonGrpByReportform = null;
			consignLocationDetailsForm = null;
			productDataDetailsForm = null;
			employeeDependentDetailsForm = null;
			employeeDocumentsForm = null;
			patientDocumentsForm = null;
			propertyDocumentsForm = null;
			propertyTenantDocumentsForm = null;
			vehicleDocumentsForm = null;
			employeeSkillsForm = null;
			leaveTypeDetailsForm = null;
			jobTypeDetailsForm = null;
			costCategoryDetailsForm = null;
			payrollItemDetailsForm = null;
			deductionDetailsForm = null;
			benefitDetailsForm = null;
			employeeSalaryDetailsForm = null;
			destinationDetailsForm = null;
			employeeLeaveDetailForm = null;
			companyDocTypeDetailsForm = null;
			companyDocumentsForm = null;
			tenancyContractDetailsForm = null;
			tradeLicenseDetailsForm = null;
			visaDetailsForm = null;
			accountAnalysisDetailsForm = null;
			currencyDetailsForm = null;
			costCenterDetailsForm = null;
			chequeReceiptForm = null;
			sysDocDetailsForm = null;
			registerDetailsForm = null;
			cashReceiptForm = null;
			cashReceiptMultiPayeeForm = null;
			cashPaymentParentForm = null;
			chequebookDetailsForm = null;
			cashPaymentForm = null;
			chequePaymentForm = null;
			journalReportForm = null;
			fundTransferForm = null;
			chequeDepositForm = null;
			chequeExpenseEntryForm = null;
			debitNoteEntryForm = null;
			creditNoteEntryForm = null;
			returnedChequeReasonDetailsForm = null;
			chequeReturnForm = null;
			receivedChequeCancellationForm = null;
			receivedChequeClearanceForm = null;
			issuedChequeClearanceForm = null;
			issuedChequeCancellationForm = null;
			issuedChequeReturnForm = null;
			voidBlankChequeForm = null;
			securityChequeForm = null;
			chequeStatusForm = null;
			glReportForm = null;
			adjustmentTypeDetailsForm = null;
			driverDetailsForm = null;
			inventoryTransferForm = null;
			productQuantityForm = null;
			inventoryTransferAcceptanceForm = null;
			inventoryTransferReturnForm = null;
			productListForm = null;
			directInventoryTransferForm = null;
			salesQuoteForm = null;
			customerCategoryForm = null;
			contactsCategoryForm = null;
			leadCategoryForm = null;
			jobMaterialEstimateForm = null;
			jobManHrsBudgetingForm = null;
			customerCategoryListForm = null;
			contactsCategoryListForm = null;
			leadCategoryListForm = null;
			vendorCategoryListForm = null;
			genericProductTypeListForm = null;
			CaseClientListForm = null;
			riderSummaryListForm = null;
			horseSummaryListForm = null;
			horseTypeListForm = null;
			equipmentCategoryListForm = null;
			equipmentTypeListForm = null;
			horseSexListForm = null;
			holidayCalendarListForm = null;
			overTimeDetailsListForm = null;
			lawyerListForm = null;
			legalActionStatusListForm = null;
			taxListForm = null;
			taxgroupListForm = null;
			casePartyListForm = null;
			leaveAvailabilityForm = null;
			customerInsuranceDetailsForm = null;
			creditLimitReviewForm = null;
			priceListDetailsForm = null;
			imageViewerForm = null;
			salesOrderForm = null;
			salesProformaInvoiceForm = null;
			salesEnquiryForm = null;
			salesInvoiceNIForm = null;
			deliveryNoteForm = null;
			salesInvoiceForm = null;
			salesReceiptForm = null;
			salesReturnCreditForm = null;
			salesReturnCashForm = null;
			deliveryReturnForm = null;
			purchaseQuoteForm = null;
			purchaseOrderForm = null;
			purchaseOrderNonInvForm = null;
			poShipmentForm = null;
			portDetailsForm = null;
			collateralDetailsForm = null;
			serviceProviderForm = null;
			maintenanceSchedulerForm = null;
			maintenanceSchedulerReportForm = null;
			maintenanceEntryReportForm = null;
			projectInvoiceReportForm = null;
			physicalStockEntryForm = null;
			salespersonGroupDetailsForm = null;
			pickListReportForm = null;
			vehicleMaintenanceEntryForm = null;
			lpoReceiptForm = null;
			candidateCancellationForm = null;
			employeeCancellationForm = null;
			purchaseInvoiceForm = null;
			purchaseInvoiceNonInvForm = null;
			purchaseReceiptForm = null;
			importPurchaseGRNForm = null;
			cashPurchaseForm = null;
			purchaseReturnCashForm = null;
			purchaseReturnCreditForm = null;
			grnReturnForm = null;
			fixedAssetBulkPurchaseForm = null;
			employeeAppraisalForm = null;
			inventoryTransactionsReport = null;
			projectInventoryTransactionsReport = null;
			inventoryTransferReport = null;
			itemMovementGRNReport = null;
			itemMovementConsignInReport = null;
			ConsignInReceiptReport = null;
			consignmentInSettlementReport = null;
			consignmentOutSettlementReport = null;
			W3PLinventoryTransactionsReport = null;
			purchaseByInventoryItemVendorBuyerReport = null;
			customerTopListReport = null;
			customerDueReport = null;
			propertyAccountTransactionReportForm = null;
			propertyUnitAvailabilityReportForm = null;
			propertyUnitHistoryReportForm = null;
			consignmentOutIssuedReport = null;
			salesByitemCategoryReport = null;
			salesPurchaseAnalysisReport = null;
			salesComparisonReport = null;
			containerTrackingForm = null;
			containerTrackingWizrdForm = null;
			customerCenterForm = null;
			accountCenterForm = null;
			vendorCenterForm = null;
			inventoryCenterForm = null;
			reportCenterForm = null;
			hrCenterForm = null;
			companyOptionsForm = null;
			purchaseOrderImportForm = null;
			purchaseInvoiceImportForm = null;
			releaseTypeForm = null;
			serviceItemForm = null;
			legalActionStatusForm = null;
			arrivalReportForm = null;
			qualityClaimForm = null;
			arrivalReportTemplateForm = null;
			qualityTaskForm = null;
			surveyorForm = null;
			clientAssetForm = null;
			binDetailsForm = null;
			rackDetailsForm = null;
			genericProductTypeDetailsForm = null;
			serviceActivityDetailsForm = null;
			jobMaintenanceServiceEntryForm = null;
			jobMaintenanceScheduleForm = null;
			serviceCallTrackForm = null;
			serviceCallTrackReportForm = null;
			holidayCalendarForm = null;
			changeCashRegisterForm = null;
			purchaseCostEntryReportForm = null;
			paymentAdviceDetailsForm = null;
			vendorPriceListDetailsForm = null;
			salesByCustomerReport = null;
			salesByItemReport = null;
			salesBySalespersonReport = null;
			salesByLocationReport = null;
			salesByProductCategoryReport = null;
			salesByCustomerGroupReport = null;
			trialBalanceReportForm = null;
			balanceSheetReportForm = null;
			profitAndLossReportForm = null;
			profitAndLossMonthwiseReportForm = null;
			profitAndLossComparionReportForm = null;
			dailyCashReportForm = null;
			dailyCashSaleReportForm = null;
			customerListReport = null;
			customerContactListReport = null;
			customerProfileReport = null;
			customerActivityReport = null;
			customerYearMonthPaymentReport = null;
			employeeSalarySlipReport = null;
			lastMonthSalaryReport = null;
			salaryCertificateReport = null;
			confirmationLetterReport = null;
			appointmentLetterReport = null;
			salaryIncrementLetterReport = null;
			experienceCertificateReport = null;
			assemblyReport = null;
			salesByItemCustomerSalespersonReport = null;
			consignmentInReport = null;
			consignmentOutReport = null;
			customerOutstandingSummaryReport = null;
			dailySalesAnalysisReportForm = null;
			salesReservedReportForm = null;
			salesByProductBrandReportForm = null;
			vendorOutstandingSummaryReportForm = null;
			SalespersonCommisionReportForm = null;
			salesProfitabilityReportForm = null;
			taxDetailsReportForm = null;
			salesByMainCategoryReportForm = null;
			monthlySalesPivotReportForm = null;
			multipleInvoiceReportForm = null;
			salesOrderDetailsReportForm = null;
			transporterDetailsForm = null;
			customGadgetDetailsForm = null;
			containerSizeDetailsForm = null;
			INCODetailsForm = null;
			reportTemplatesUpdateForm = null;
			inventoryTransactionsLotwiseReport = null;
			purchaseExpenseAllocationReport = null;
			purchaseOrderDetailReport = null;
			purchaseByItemVendorBuyerReport = null;
			fixedAssetListReport = null;
			fixedAssetPurchaseReport = null;
			fixedAssetSaleReport = null;
			fixedAssetTransactionsReport = null;
			fixedAssetDepreciationReport = null;
			fixedAssetTransferReport = null;
			cashFlowReportForm = null;
			accountTransactionsReportForm = null;
			accountCostCenterReportForm = null;
			accountLedgerReportForm = null;
			bankLedgerReportForm = null;
			salesManDueReport = null;
			projectAccountTransactionsReportForm = null;
			accountAnalysisReportForm = null;
			accountAnalysisPivotReportForm = null;
			vendorListReport = null;
			vendorContactListReport = null;
			vendorProfileReport = null;
			vendorActivityReport = null;
			productListReport = null;
			purchaseByVendorReport = null;
			purchaseByVendorGroupReport = null;
			purchaseByItemReport = null;
			purchaseByProductCategoryReport = null;
			purchaseByBuyerReport = null;
			purchaseByLocationReport = null;
			productPriceListReport = null;
			productStockListReport = null;
			productValuationReport = null;
			productCatalogReport = null;
			freightChargeReport = null;
			employeeTransferForm = null;
			employeeTerminationForm = null;
			employeePromotionForm = null;
			disciplineActionTypeDetailsForm = null;
			employeeDisciplinaryActionForm = null;
			employeeRehireForm = null;
			employeeActivityTypeDetailsForm = null;
			employeeGeneralActivityForm = null;
			employeeLeaveRequestForm = null;
			employeeLeaveApprovalForm = null;
			employeeResumptionForm = null;
			salarySheetForm = null;
			postSalarySheetForm = null;
			cashSalaryPaymentForm = null;
			chequeSalaryPaymentForm = null;
			transferSalaryPaymentForm = null;
			employeeLoanTypeDetailsForm = null;
			employeeLoanForm = null;
			employeeLoanSettlementForm = null;
			employeeBalanceSummaryReport = null;
			employeeBalanceDetailsReport = null;
			employeeSalaryReport = null;
			employeeListReport = null;
			employeeProfileReport = null;
			employeeActivityReport = null;
			purchaseCostSheetReport = null;
			leadProfileReport = null;
			leadBySourceReport = null;
			upcomingOppReport = null;
			upcomingEventsReport = null;
			customerStatementForm = null;
			unallocatedCustomerPaymentsListForm = null;
			unallocatedVendorPaymentsListForm = null;
			customerAgingSummaryReport = null;
			vendorAgingSummaryReport = null;
			vendorStatementForm = null;
			shipmentPerformanceAnalysForm = null;
			exportSmartListForm = null;
			exportPivotReportForm = null;
			exportCustomeGadgetForm = null;
			importSmartListForm = null;
			importPivotReportForm = null;
			importCustomeGadgetForm = null;
			userDetailsForm = null;
			userGroupDetailsForm = null;
			printChequeForm = null;
			reminderListForm = null;
			companyAddressDetailsForm = null;
			consignOutForm = null;
			garmentRentalForm = null;
			garmentRentalReturnForm = null;
			consignOutSettlementForm = null;
			expenseCodeDetailsForm = null;
			exportSalesInvoiceForm = null;
			exportSalesOrderForm = null;
			exportSalesProformaInvoiceForm = null;
			exportDeliveryNoteForm = null;
			exportPickListForm = null;
			consignOutReturnForm = null;
			consignInForm = null;
			consignInReturnForm = null;
			consignInSettlementForm = null;
			consignInClosingForm = null;
			pendingConsignInReport = null;
			pendingConsignOutReport = null;
			fixedAssetDetailsForm = null;
			fixedAssetGroupDetailsForm = null;
			fixedAssetLocationDetailsForm = null;
			fixedAssetClassDetailsForm = null;
			fixedAssetPurchaseForm = null;
			fixedAssetSaleForm = null;
			fixedAssetTransferForm = null;
			fixedAssetDepForm = null;
			fixedAssetPurchaseOrderForm = null;
			posCashRegisterDetailsForm = null;
			posLocationDetailsForm = null;
			dimensionDetailsForm = null;
			matrixTemplateDetailsForm = null;
			matrixProductDetailsForm = null;
			posCashRegisterPaymentMethodsForm = null;
			posCashRegisterExpenseAccountsForm = null;
			proformaInvoiceForm = null;
			unallocatPurchasePrePaymentList = null;
			fiscalYearDetailsForm = null;
			customerAgingListForm = null;
			vendorAgingListForm = null;
			ttPaymentForm = null;
			ttReceiptForm = null;
			leadDetailsForm = null;
			leadAddressDetailsForm = null;
			udfSetupForm = null;
			taxEntryForm = null;
			purchasePrepaymentInvoiceForm = null;
			customerOutstandingInvoicesReport = null;
			matrixProductStockListReport = null;
			buildAssemblyForm = null;
			inventoryRepackingForm = null;
			projectExpenseAllocationForm = null;
			purchaseClaimForm = null;
			jobEstimationForm = null;
			employeePassportControlForm = null;
			insuranceProviderForm = null;
			vendorCategoryForm = null;
			bomDetailsForm = null;
			employeeSalarySlipBulkMailForm = null;
			JobbomDetailsForm = null;
			packageDetailsForm = null;
			eosRuleDetailsForm = null;
			overTimeDetailsForm = null;
			employeeLoanPaymentForm = null;
			docManagementForm = null;
			jobDetailsForm = null;
			opportunityDetailsForm = null;
			followupDetailsForm = null;
			eventDetailsForm = null;
			competitorDetailsForm = null;
			activityDetailsForm = null;
			customReportDetailForm = null;
			trPaymentForm = null;
			employeeOpeningBalanceBatchForm = null;
			employeeOpeningBalanceLeaveForm = null;
			vendorOutstandingInvoicesReportForm = null;
			inventoryAdjustmentReportForm = null;
			updateLotDetailsForm = null;
			pendingGRNReportForm = null;
			pendingDNReportForm = null;
			profitAndLossReportRevisedForm = null;
			propertyServiceInvoiceForm = null;
			recurringTransactionPostForm = null;
			employeeAbscondingEntryForm = null;
			patientForm = null;
			projectStatusReportForm = null;
			routeDetailsForm = null;
			routeGroupDetailsForm = null;
			productionDetailsForm = null;
			updateTREntryDetailsForm = null;
			purchaseComparisonReportForm = null;
			dataSyncForm = null;
			dataSyncSetupDetailsForm = null;
			materialVarianceReportForm = null;
			jobSummaryReportForm = null;
			employeeLeaveReportForm = null;
			leaveStatusReportForm = null;
			employeeAnnualLeaveDueReportForm = null;
			employeeLoanReportForm = null;
			discountChequesReportForm = null;
			purchaseLogReportForm = null;
			purchaseCostEntryForm = null;
			riderSummaryDetailsForm = null;
			horseSummaryDetailsForm = null;
			containerTrackingreportform = null;
			horseSummaryReport = null;
			barcodeReport = null;
			balanceSheetComparisonReport = null;
			horseTypeForm = null;
			horseSexForm = null;
			equipmentForm = null;
			requisitionDetailsForm = null;
			mobilisationForm = null;
			itemTransactionForm = null;
			equipmentTransferForm = null;
			equipmentWorkOrderForm = null;
			lawyerDetailsForm = null;
			casePartyDetailsForm = null;
			legalActivityDetailsForm = null;
			legalActionDetailsForm = null;
			freightChargesForm = null;
			salesForecastingForm = null;
			productMakeDetilsForm = null;
			productTypeDetilsForm = null;
			productModelDetilsForm = null;
			taxGroupForm = null;
			productTypeForm = null;
			equipmentListReportForm = null;
			requisitionByWorkLocationProjectReportForm = null;
			mobilizationByWorkLocationProjectReportForm = null;
			equipmentTransferReportForm = null;
			equipmentWorkOrderReportForm = null;
			equipmentFlowReportForm = null;
			workOrderInventoryIssueForm = null;
			workOrderInventoryReturnForm = null;
			workOrderInventoryTransactionReport = null;
			pendingCaseReport = null;
			CaseStatusReport = null;
			productPriceBulkUpdateForm = null;
			openingChequeReceiptEntryForm = null;
			openingChequePaymentEntryForm = null;
			materialReservationForm = null;
			caseLawyerTrackReport = null;
			inventoryDismantleForm = null;
			equipmentDetailForm = null;
			caseClientDetailsForm = null;
			overTimeEntryForm = null;
			salaryDeductionForm = null;
			salaryAdditionForm = null;
			employeeOverTimeReportForm = null;
			employeeGraduityEligibilityReportForm = null;
			employeeHistoryReportForm = null;
			employeeSalaryReportForm = null;
			pdcIssuedReportForm = null;
			pdcReceivedReportForm = null;
			productUPCGenerator = null;
			projectSubContractPOForm = null;
			projectSubContractPIForm = null;
			inventoryDamageForm = null;
			productStockPivotListReport = null;
			W3PLproductStockPivotListReport = null;
			jobClosingForm = null;
			campaignDetailsForm = null;
			jobInventoryIssueForm = null;
			jobInventoryReturnForm = null;
			jobExpenseIssueForm = null;
			jobInvoiceForm = null;
			jobTimesheetForm = null;
			bankFacilityGroupForm = null;
			bankFacilityForm = null;
			customerOpeningBalanceBatchForm = null;
			vendorOpeningBalanceBatchForm = null;
			inventoryOpeningBalanceBatchForm = null;
			nationalAccountForm = null;
			bankReconciliationForm = null;
			bankReconciliationOpeningForm = null;
			bankReconciliationReportForm = null;
			bankNotReconciledReportForm = null;
			customerVendorLinkForm = null;
			cityDetailsForm = null;
			employeeLeaveProcessForm = null;
			genericProductTypeListDetailsForm = null;
			agentDetailsForm = null;
			crmActivityReasonDetailsForm = null;
			crmEventTypeDetailsForm = null;
			qcInspectorDetailsForm = null;
			genericListDetailsForm = null;
			propertyUnitTypeForm = null;
			propertyViewForm = null;
			propertyOwnerForm = null;
			propertyFacilityForm = null;
			propertyServiceTypeForm = null;
			employeeAddressTypeForm = null;
			fixedAssetCompanyForm = null;
			medicalInsuranceCategoryForm = null;
			horseOwnershipTypeForm = null;
			horseCategoryForm = null;
			activityStatusForm = null;
			caseTypeForm = null;
			chronicsDetailsForm = null;
			allergyDetailsForm = null;
			containerTypeForm = null;
			vehicleMakeForm = null;
			partsMakeTypeForm = null;
			partsTypeForm = null;
			partsFamilyForm = null;
			vehicleModelForm = null;
			shippingCompanyForm = null;
			kitchenTypeForm = null;
			collateralCustodianForm = null;
			legalPositionFormObj = null;
			customListSetupFormObj = null;
			equipmentCategoryForm = null;
			equipmenttypeForm = null;
			requisitionTypeForm = null;
			statusChangeForm = null;
			vehicleDetailsForm = null;
			jobMaterialRequesitionForm = null;
			jobFeeForm = null;
			workOrderDetailsForm = null;
			jobBudgetVsActualReport = null;
			trEntryForm = null;
			trApplicationForm = null;
			inventoryAgingReport = null;
			W3PLinventoryAgingReport = null;
			sendChequesToBankForm = null;
			candidateDetailsForm = null;
			chequeDiscountForm = null;
			inventoryTransferTypeDetailsForm = null;
			exportPackingListForm = null;
			shipmentForm = null;
			leaveEncashmentForm = null;
			leaveSalaryPaymentForm = null;
			standingJournalEntryForm = null;
			approvalDetailsForm = null;
			performApprovalForm = null;
			verificationDetailsForm = null;
			verifyObjectForm = null;
			checkListDetailsForm = null;
			serviceProviderFormObj = null;
			subContractPIReportForm = null;
			appointmentDetailsForm = null;
			MaterialRequisitionReportForm = null;
			billOfLadingForm = null;
			reqForCreditLimitReportForm = null;
			chequeMaturityReportForm = null;
			salesEnquiryReportFormObj = null;
			salesQuoteReportFormObj = null;
			salesOrderReportFormObj = null;
			proformaInvoiceReportFormObj = null;
			deliveryNoteReportFormObj = null;
			salesReceiptReportFormObj = null;
			salesInvoiceReportForm = null;
			purchaseQuoteReportFormObj = null;
			purchaseInvoiceReportForm = null;
			purchaseGRNReportForm = null;
			purchasePLReportForm = null;
			purchaseOrderReportForm = null;
			subContractPOReportForm = null;
			materialRequisitionFlowReportFormObj = null;
			propertyDetailsForm = null;
			propertyDocTypeFormObj = null;
			propertyTenantDocTypeFormObj = null;
			propertyUnitDetailsForm = null;
			virtualUnitDetailsForm = null;
			propertyClassDetailsForm = null;
			propertyAgentDetailsForm = null;
			propertyCategoryDetailsForm = null;
			propertyIncomeCodeDetailsForm = null;
			propertyRentDetailsForm = null;
			propertyRenewDetailsForm = null;
			propertyRentCancellationForm = null;
			rentIncomePostingDetails = null;
			propertyTenantForm = null;
			propertyTenantClassDetailsForm = null;
			propertyServiceRequestForm = null;
			PropertyServiceAssignForm = null;
			posCashExpenseForm = null;
			salesByCustomerReportForm = null;
			w3PLGRNForm = null;
			w3PLDeliveryForm = null;
			w3PLInvoiceForm = null;
			clVoucherForm = null;
			entityFlagDetailsForm = null;
			employeePerformanceCardForm = null;
			documentOCRForm = null;
			leadStatusDetailsForm = null;
			industryDetailsForm = null;
			leadSourceDetailsForm = null;
			collateralTypeDetailsForm = null;
			salesReturnReasonForm = null;
			EntityCategoryDetailsForm = null;
			vehicleTypeForm = null;
			uploadFileDialog = null;
			editFileDialog = null;
			accountGroupListForm = null;
			payrollItemListForm = null;
			analysisListForm = null;
			analysisGroupListForm = null;
			areaListForm = null;
			jobListForm = null;
			clientAssetListForm = null;
			quoteComparisonListForm = null;
			opportunityListForm = null;
			competitorListForm = null;
			activityListForm = null;
			campaignListForm = null;
			eventListForm = null;
			bankListForm = null;
			bankFacilityGroupListForm = null;
			bankFacilityListForm = null;
			cityListForm = null;
			jobFeeListForm = null;
			vehicleListForm = null;
			customerVendorListForm = null;
			equipmentListForm = null;
			benefitListForm = null;
			eosRuleListForm = null;
			containerTrackingListForm = null;
			customerListForm = null;
			caseClientListForm = null;
			vendorListForm = null;
			leadListForm = null;
			employeeListForm = null;
			leadStatusForm = null;
			chequeReceiptMultiEntryForm = null;
			loanEntryForm = null;
			buyerListForm = null;
			companyDocTypeListForm = null;
			companyDocumentListForm = null;
			contactListForm = null;
			countryListForm = null;
			currencyListForm = null;
			customerClassListForm = null;
			customerGroupListForm = null;
			checkListForm = null;
			deductionListForm = null;
			degreeListForm = null;
			departmentListForm = null;
			destinationListForm = null;
			divisionListForm = null;
			companyDivisionListForm = null;
			employeeDocTypeListForm = null;
			employeeGroupListForm = null;
			employeeTypeListForm = null;
			gradeListForm = null;
			leaveTypeListForm = null;
			vehicleDocTypeListForm = null;
			jobTypeListForm = null;
			costCategoryListForm = null;
			locationListForm = null;
			nationalityListForm = null;
			paymentMethodListForm = null;
			paymentTermListForm = null;
			positionListForm = null;
			pricelevelListForm = null;
			productBrandListForm = null;
			productCategoryListForm = null;
			productClassListForm = null;
			productManufacturerListForm = null;
			productStyleListForm = null;
			religionListForm = null;
			salespersonListForm = null;
			shippingMethodListForm = null;
			skillListForm = null;
			sponsorListForm = null;
			tenancyContractListForm = null;
			tradeLicenseListForm = null;
			unitListForm = null;
			vendorClassListForm = null;
			vendorGroupListForm = null;
			containerSizeListForm = null;
			INCODetailsListForm = null;
			visaListForm = null;
			costCenterListForm = null;
			returnedChequeReasonListForm = null;
			chequebookListForm = null;
			registerListForm = null;
			portListForm = null;
			disciplineActionTypeListForm = null;
			employeeActivityTypeListForm = null;
			userGroupListForm = null;
			userListForm = null;
			vendorAddressListForm = null;
			customerAddressListForm = null;
			nationalAccountListForm = null;
			employeeDocumentListForm = null;
			driverListForm = null;
			expenseCodeListForm = null;
			fixedAssetListForm = null;
			fixedAssetLocationListForm = null;
			fixedAssetGroupListForm = null;
			fixedAssetClassListForm = null;
			fiscalYearListForm = null;
			priceListForm = null;
			candidateListForm = null;
			workLocationListForm = null;
			collateralListForm = null;
			jobTaskListForm = null;
			jobTaskGroupListForm = null;
			customGadgetListForm = null;
			customReportListForm = null;
			appointmentListForm = null;
			transporterListForm = null;
			INCOListForm = null;
			genericListForm = null;
			followupDetailsListForm = null;
			qualityTaskListForm = null;
			arrivalReportTemplateListForm = null;
			surveyorListForm = null;
			employeeLoanTypeListForm = null;
			employeeLeaveListForm = null;
			employeeLeaveResumptionListForm = null;
			employeePassportControlListForm = null;
			adjustmentTypeDetailsListForm = null;
			jobBOMListForm = null;
			packageListForm = null;
			propertyAgentListForm = null;
			propertyClassListForm = null;
			propertyListForm = null;
			propertyUnitListForm = null;
			propertyVirtualUnitListForm = null;
			propertyIncomeCodeListForm = null;
			inventoryTransferTypeListForm = null;
			propertyTenantListForm = null;
			approvalDetailsListForm = null;
			verificationDetailsListForm = null;
			binDetailsListForm = null;
			routeDetailsListForm = null;
			routeGroupDetailsListForm = null;
			releaseTypeListForm = null;
			serviceItemListForm = null;
			provisionTypeListForm = null;
			serviceProviderListForm = null;
			serviceActivityListForm = null;
			EAEquipmentListForm = null;
			requisitionTypeListForm = null;
			SysDocListForm = null;
			productMakeListForm = null;
			productTypeListForm = null;
			productModelListForm = null;
			taskStepsListForm = null;
			taskTypeListForm = null;
			rackListForm = null;
			productSpecificationListForm = null;
			printTemplateMapListForm = null;
			patientListForm = null;
			patientDocTypeListForm = null;
			dataSyncListForm = null;
			kitchenTypeListForm = null;
			employeeAbscondingList = null;
			salesInvoiceListForm = null;
			salesReceiptListForm = null;
			salesOrderListForm = null;
			salesEnquiryListForm = null;
			salesQuoteListForm = null;
			salesReturnListForm = null;
			deliveryNoteListForm = null;
			deliveryReturnListForm = null;
			arAllocationListForm = null;
			salesHistoryForm = null;
			activityLogForm = null;
			inventoryLedgerForm = null;
			exportsalesInvoiceListForm = null;
			exportDeliveryNoteListForm = null;
			exportPickListListForm = null;
			salesProformaListForm = null;
			exportsalesProformaListForm = null;
			exportPackingListListForm = null;
			shipmentListForm = null;
			exportSalesOrderListForm = null;
			salesInvoiceNIListForm = null;
			inventoryDamageListForm = null;
			inventoryRepackingListForm = null;
			inventoryTransferAcceptanceListForm = null;
			inventoryTransferReturnListForm = null;
			inventoryTransferListForm = null;
			directinventoryTransferListForm = null;
			jobInvoiceListForm = null;
			jobInventoryIssueListForm = null;
			qualityClaimListForm = null;
			arrivalReportListForm = null;
			propertyRenewListForm = null;
			propertyCancelListForm = null;
			propertyRentListForm = null;
			propertyServiceInvoiceListForm = null;
			propertyIncomePostingListForm = null;
			jobEstimationListForm = null;
			salarySheetListForm = null;
			overTimeEntrytListForm = null;
			employeeSalaryListForm = null;
			employeeLoanListForm = null;
			employeeLoanPaymentListForm = null;
			employeeLoanSettlementList = null;
			employeeEOSList = null;
			purchaseInvoiceListForm = null;
			purchaseInvoiceNIListForm = null;
			purchaseOrderListForm = null;
			purchaseOrderNIListForm = null;
			purchaseQuoteListForm = null;
			purchaseReturnListForm = null;
			grnReturnListForm = null;
			purchaseGRNListForm = null;
			importPurchaseGRNListForm = null;
			importPurchaseOrderListForm = null;
			importPurchaseInvoiceListForm = null;
			consignInListForm = null;
			poShipmentListForm = null;
			apAllocationListForm = null;
			proformaInvoiceListForm = null;
			consignmentOutListForm = null;
			consignInSettlementListForm = null;
			consignmentOutSettlementListForm = null;
			purchasePackinglistListForm = null;
			cashPurchaseListForm = null;
			fixedAssetPurchaseListForm = null;
			fixedAssetPurchaseOrderListForm = null;
			fixedAssetSaleListForm = null;
			fixedAssetTransferListForm = null;
			trPaymentList = null;
			trList = null;
			clVoucherListFormObj = null;
			sendChequeToBankListForm = null;
			chequeDepositListForm = null;
			jvListForm = null;
			journalListForm = null;
			trailBalanceListForm = null;
			expenseListForm = null;
			debitNoteListForm = null;
			creditNoteListForm = null;
			LPOReceiptListForm = null;
			receiptVoucherListForm = null;
			returnVoucherListForm = null;
			issuedChequeBounceListForm = null;
			receivedChequeCancellationListForm = null;
			paymentVoucherListForm = null;
			crmActivityListForm = null;
			crmCustomerActivityListForm = null;
			fundTransferListForm = null;
			issuedChequeListForm = null;
			receivedChequeListForm = null;
			securityChequeListForm = null;
			cashSalaryPaymentListForm = null;
			chequeSalaryPaymentListForm = null;
			transferSalaryPaymentListForm = null;
			projectExpenseAllocationListForm = null;
			paymentRequestListForm = null;
			purchaseClaimListForm = null;
			buildAssemblyListForm = null;
			productionListForm = null;
			workOrderListForm = null;
			w3PLDeliveryListForm = null;
			w3PLGRNListForm = null;
			w3PLInvoiceListForm = null;
			maintenanceEntryListForm = null;
			legalActivityListForm = null;
			legalActionListForm = null;
			trApplicationListForm = null;
			chequeReceiptMultiEntryListForm = null;
			jobMaterialRequistionListForm = null;
			jobMaterialEstimateListForm = null;
			jobManHrsBudgetingListForm = null;
			jobExpenseIssueListForm = null;
			jobClosingListForm = null;
			jobInventoryReturnListForm = null;
			jobTimesheetListForm = null;
			garmentRentalListForm = null;
			garmentRentalReturnListForm = null;
			serviceCallTrackListForm = null;
			maintenanceSchedulerListForm = null;
			jobMaintenanceScheduleListForm = null;
			purchaseCostEntryListForm = null;
			billOfLadingListForm = null;
			jobMaintenanceServiceEntryListForm = null;
			employeeAppraisalListForm = null;
			customerInsuranceListForm = null;
			projectSubContractListForm = null;
			projectSubContractInvoiceListForm = null;
			employeeGeneralActivityListForm = null;
			employeeOpeningBalanceLeaveList = null;
			employeeProvisionListForm = null;
			employeeLeavePaymentListForm = null;
			employeePerformanceListForm = null;
			employeeLeaveProcessListForm = null;
			consignInClosingListForm = null;
			freightChargesListForm = null;
			priceListListForm = null;
			requisitionListFormObj = null;
			mobilizationListFormObj = null;
			FixedAssetBulkPurchaseListForm = null;
			FixedAssetDepListForm = null;
			itemTransactionListForm = null;
			issuedChequeClearanceListForm = null;
			equipmentTransferListFormObj = null;
			equipmentWorkOrderListForm = null;
			inventoryAdjustmentListForm = null;
			customerOpeningBalanceBatchListForm = null;
			vendorOpeningBalanceBatchListForm = null;
			employeeOpeningBalanceBatchListForm = null;
			inventoryOpeningBalanceBatchListForm = null;
			workOrderInventoryIssueListForm = null;
			workOrderInventoryReturnListForm = null;
			consignOutReturnListForm = null;
			SalaryAdditionListForm = null;
			inventoryDismantleListForm = null;
			salaryDeductionListForm = null;
			produtPriceBulkUpdateListForm = null;
			chequeReceiptOpeningListForm = null;
			chequepaymentVoucherListForm = null;
			materialResevationList = null;
			SalesForecastingList = null;
			IssuedChequeCancellationListForm = null;
			purchasePrepaymentInvoiceList = null;
			taskTransactionList = null;
			budgetingListForm = null;
			vehiclemileagetrackListForm = null;
			salesmantargetListForm = null;
			customerInsuranceClaimListForm = null;
			chequeDiscountListForm = null;
			billDiscountListForm = null;
			standingJournalListForm = null;
			loanentryListForm = null;
			employeeLeaveRequestListForm = null;
			issuedChequeCancellationListForm = null;
			propertyServiceRequestListForm = null;
			propertyServiceAssignListForm = null;
			customReportFormList = new SortedList<string, CustomReportDisplayForm>();
			selectedsysDocID = "";
		}
	}
}
