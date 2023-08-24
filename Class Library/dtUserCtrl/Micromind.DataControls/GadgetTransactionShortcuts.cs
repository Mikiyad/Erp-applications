using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraNavBar;
using Micromind.ClientLibraries;
using Micromind.DataControls.FlatDashboard;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GadgetTransactionShortcuts : UserControl, IGadget, ILinkGadget
	{
		private LinksCollection links = new LinksCollection();

		private bool hasAccess = true;

		private NavBarGroup parentGroup;

		private bool isDataLoaded;

		private bool isRefresh;

		private Micromind.DataControls.FlatDashboard.FlatDashboard parentDashboard;

		private IContainer components;

		private BackgroundWorker backgroundWorker;

		private ImageList imageList1;

		public Micromind.DataControls.FlatDashboard.FlatDashboard ParentDashboard
		{
			get
			{
				return parentDashboard;
			}
			set
			{
				parentDashboard = value;
			}
		}

		public bool IsBusy => backgroundWorker.IsBusy;

		public string GadgetTitle
		{
			get
			{
				return "Transaction Shortcuts";
			}
			set
			{
			}
		}

		public NavBarGroup ParentNavBarGroup
		{
			get
			{
				return parentGroup;
			}
			set
			{
				parentGroup = value;
			}
		}

		public GadgetStyles GadgetStyle
		{
			get
			{
				return GadgetStyles.Link;
			}
			set
			{
			}
		}

		public GadgetTypes GadgetType => GadgetTypes.TransactionShortcuts;

		public string GadgetID
		{
			get
			{
				return "SYS" + (int)GadgetType;
			}
			set
			{
			}
		}

		public LinksCollection Links
		{
			get
			{
				return links;
			}
			set
			{
				links = value;
			}
		}

		public Image Icon
		{
			get
			{
				if (imageList1.Images.Count > 0)
				{
					return imageList1.Images[0];
				}
				return null;
			}
		}

		public ViewType ChartType
		{
			set
			{
			}
		}

		private LinksCollection AvailableLinks => new LinksCollection
		{
			new Link("CashPaymentParentForm", "Cash Expense Entry", "Account", "Account"),
			new Link("CashPaymentParentForm", "Cash Payment", "Account", "Account"),
			new Link("CashReceiptForm", "Cash Receipt", "Account", "Account"),
			new Link("ChequeDepositForm", "Cheque Deposit", "Account", "Account"),
			new Link("ChequeExpenseEntryForm", "Cheque Expense Entry", "Account", "Account"),
			new Link("ChequePaymentForm", "Cheque Payment", "Account", "Account"),
			new Link("ChequeReceiptForm", "Cheque Receipt", "Account", "Account"),
			new Link("CreditNoteEntryForm", "Credit Note Entry", "Account", "Account"),
			new Link("DebitNoteEntryForm", "Debit Note Entry", "Account", "Account"),
			new Link("FundTransferForm", "Fund Transfer", "Account", "Account"),
			new Link("IssuedChequeClearanceForm", "Issued Cheque Clearance", "Account", "Account"),
			new Link("IssuedChequeReturnForm", "Issued Cheque Return", "Account", "Account"),
			new Link("JournalEntryForm", "Journal Entry", "Account", "Account"),
			new Link("PrintChequeForm", "Print Cheque", "Account", "Account"),
			new Link("TTPaymentForm", "Bank Transfer Payment", "Account", "Account"),
			new Link("TREntryForm", "TREntry", "Account", "Account"),
			new Link("TRPaymentForm", "TRPayment", "Account", "Account"),
			new Link("BankReconciliationForm", "Bank Reconciliation", "Account", "Account"),
			new Link("CollateralDetailsForm", "Manage Collaterals", "Account", "Account"),
			new Link("OpeningChequeReceiptEntryForm", "Opening Cheque Receipt Entry", "Account", "Account"),
			new Link("TRApplicationForm", "TR Application", "Account", "Account"),
			new Link("LoanEntryForm", "Loan Entry", "Account", "Account"),
			new Link("DeliveryNoteForm", "Delivery Note", "Customer", "Customer"),
			new Link("DeliveryReturnForm", "Delivery Return", "Customer", "Customer"),
			new Link("SalesInvoiceForm", "Sales Invoice", "Customer", "Customer"),
			new Link("SalesOrderForm", "Sales Order", "Customer", "Customer"),
			new Link("SalesQuoteForm", "Sales Quote", "Customer", "Customer"),
			new Link("SalesReceiptForm", "Sales Receipt", "Customer", "Customer"),
			new Link("SalesReturnCashForm", "Sales Return - Cash", "Customer", "Customer"),
			new Link("SalesReturnCreditForm", "Sales Return - Credit", "Customer", "Customer"),
			new Link("ExportSalesInvoiceForm", "Export Sales Invoice", "Customer", "Customer"),
			new Link("ExportDeliveryNoteForm", "Export Delivery Note", "Customer", "Customer"),
			new Link("ExportSalesOrderForm", "Export Sales Order", "Customer", "Customer"),
			new Link("ExportDeliveryNoteForm", "Export Delivery Note", "Customer", "Customer"),
			new Link("ExportSalesInvoiceForm", "Export Sales Invoice", "Customer", "Customer"),
			new Link("ExportPackingListForm", "Packing List", "Customer", "Customer"),
			new Link("ConsignLocationDetailsForm", "Consignment Out Store", "Customer", "Customer"),
			new Link("ConsignOutForm", "Issue Consignment Out", "Customer", "Customer"),
			new Link("ConsignOutSettlementForm", "Consignment Out Settlement", "Customer", "Customer"),
			new Link("ConsignOutReturnForm", "Consignment Out Return", "Customer", "Customer"),
			new Link("UnallocatedPaymentsListForm", "Allocate Payments", "Customer", "Customer"),
			new Link("CustomerStatementForm", "Customer Statement", "Customer", "Customer"),
			new Link("PriceListDetailsForm", "Customer Price List", "Customer", "Customer"),
			new Link("GarmentRentalForm", "Garment Rental", "Customer", "Customer"),
			new Link("GarmentRentalReturnForm", "Garment Rental Return", "Customer", "Customer"),
			new Link("CreditLimitReviewForm", "Credit Limit Review", "Customer", "Customer"),
			new Link("CLVoucherForm", "Credit Limit Voucher", "Customer", "Customer"),
			new Link("SalesProformaInvoiceForm", "Sales Proforma Invoice", "Customer", "Customer"),
			new Link("SalesForecastingForm", "Sales Forecasting", "Customer", "Customer"),
			new Link("CashPurchaseForm", "Cash Purchase", "Vendor", "Vendor"),
			new Link("POShipmentForm", "PO Shipment", "Vendor", "Vendor"),
			new Link("PurchaseInvoiceForm", "Purchase Invoice", "Vendor", "Vendor"),
			new Link("PurchaseOrderForm", "Purchase Order", "Vendor", "Vendor"),
			new Link("PurchaseQuoteForm", "Purchase Quote", "Vendor", "Vendor"),
			new Link("PurchaseGRNForm", "Goods Received Note", "Vendor", "Vendor"),
			new Link("ImportPurchaseGRNForm", "Import Goods Received Note", "Vendor", "Vendor"),
			new Link("PurchaseOrderImportForm", "Import Purchase Order", "Vendor", "Vendor"),
			new Link("PurchaseInvoiceImportForm", "Import Purchase Order", "Vendor", "Vendor"),
			new Link("PurchaseReturnCashForm", "Purchase Return - Cash", "Vendor", "Vendor"),
			new Link("PurchaseReturnCreditForm", "Purchase Return - Credit", "Vendor", "Vendor"),
			new Link("ConsignInForm", "Receive Consignment", "Vendor", "Vendor"),
			new Link("ConsignInReturnForm", "Consignment Goods Return", "Vendor", "Vendor"),
			new Link("ConsignInSettlementForm", "Consignment Settlement", "Vendor", "Vendor"),
			new Link("ConsignInClosingForm", "Close Consignment", "Vendor", "Vendor"),
			new Link("VendorStatementForm", "Vendor Statement", "Vendor", "Vendor"),
			new Link("ProformaInvoiceForm", "Proforma Invoice", "Vendor", "Vendor"),
			new Link("BillOfLadingListForm", "Bill Of Lading", "Vendor", "Vendor"),
			new Link("FreightChargesForm", "Freight Charges", "Vendor", "Vendor"),
			new Link("ProductPriceBulkUpdateForm", "Product Price Bulk Update", "Vendor", "Vendor"),
			new Link("JobTimesheetForm", "Project Timesheet Entry", "Project", "Project"),
			new Link("JobExpenseIssueForm", "Project Expense Entry", "Project", "Project"),
			new Link("JobInventoryIssueForm", "Project Inventory Issue", "Project", "Project"),
			new Link("JobInventoryReturnForm", "Project Inventory Return", "Project", "Project"),
			new Link("ProjectBillingForm", "Project Billing", "Project", "Project"),
			new Link("JobClosingForm", "Project Closing", "Project", "Project"),
			new Link("ProjectSubcontractPOForm", "SubContract Order", "Project", "Project"),
			new Link("ProjectSubContractPIForm", "SubContract Invoice", "Project", "Project"),
			new Link("JobBudgetDetailForm", "Job Budget Detail", "Project", "Project"),
			new Link("JobEstimationDetailsForm", "Job Estimation Details", "Project", "Project"),
			new Link("JobFeeDetailForm", "Job Fee Detail", "Project", "Project"),
			new Link("JobMaterialEstimateForm", "Job Material Estimate", "Project", "Project"),
			new Link("JobMaterialRequisitionForm", "Job Material Requisition", "Project", "Project"),
			new Link("InventoryAdjustmentsForm", "Inventory Adjustments", "Inventory", "Inventory"),
			new Link("InventoryTransferAcceptanceForm", "Inventory Transfer Acceptance", "Inventory", "Inventory"),
			new Link("InventoryTransferForm", "Inventory Transfer", "Inventory", "Inventory"),
			new Link("InventoryTransferReturnForm", "Inventory Transfer Return", "Inventory", "Inventory"),
			new Link("DirectInventoryTransferForm", "Direct Inventory Transfer", "Inventory", "Inventory"),
			new Link("InventoryRepackingForm", "Inventory Repacking", "Inventory", "Inventory"),
			new Link("MaintenanceSchedulerForm", "Maintenance Scheduler", "Inventory", "Inventory"),
			new Link("VehicleMaintenanceEntry", "Maintenance Entry", "Inventory", "Inventory"),
			new Link("ContainerTrackingForm", "Container Tracking", "Inventory", "Inventory"),
			new Link("InventoryDismantleForm", "Inventory Dismantle", "Inventory", "Inventory"),
			new Link("EmployeeGeneralActivityForm", "General Activity", "HR", "HR"),
			new Link("EmployeeTransferForm", "Transfer Employee", "HR", "HR"),
			new Link("EmployeeTerminationForm", "Terminate Employee", "HR", "HR"),
			new Link("EmployeeCancellationForm", "Cancel Employee", "HR", "HR"),
			new Link("EmployeePromotionForm", "Promote Employee", "HR", "HR"),
			new Link("EmployeeDisciplinaryActionForm", "Disciplinary Action", "HR", "HR"),
			new Link("EmployeeRehireForm", "Rehire Employee", "HR", "HR"),
			new Link("EmployeeLeaveRequestForm", "Leave Request", "HR", "HR"),
			new Link("EmployeeLeaveApprovalForm", "Leave Approval", "HR", "HR"),
			new Link("EmployeeResumptionForm", "Duty Resumption", "HR", "HR"),
			new Link("LeaveEncashmentForm", "Leave Encashment", "HR", "HR"),
			new Link("LeaveSalaryPaymentForm", "Leave Payment", "HR", "HR"),
			new Link("OverTimeEntryForm", "Overtime Entry", "HR", "HR"),
			new Link("SalaryAdditionForm", "Salary Addition", "HR", "HR"),
			new Link("SalaryDeductionForm", "Salary Deduction", "HR", "HR"),
			new Link("SalarySheetForm", "Salary Sheet", "HR", "HR"),
			new Link("CashSalaryPaymentForm", "Salary Payment - Cash", "HR", "HR"),
			new Link("ChequeSalaryPaymentForm", "Salary Payment - Cheque", "HR", "HR"),
			new Link("TransferSalaryPaymentForm", "Salary Payment - Bank", "HR", "HR"),
			new Link("EmployeePerformanceCardForm", "Employee Performance Card", "HR", "HR"),
			new Link("FixedAssetPurchaseForm", "Fixed Asset Aquesition", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetSaleForm", "Fixed Asset Sale", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetDepForm", "Fixed Asset Depreciation", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetTransferForm", "Fixed Asset Transfer", "FixedAsset", "FixedAsset"),
			new Link("WorkOrderDetailsForm", "Work Order", "Manufacturing", "Manufacturing"),
			new Link("BuildAssemblyForm", "Build Assembly", "Manufacturing", "Manufacturing"),
			new Link("QualityTaskForm", "QC Task", "QualityControl", "QualityControl"),
			new Link("ArrivalReportForm", "Arrival Report", "QualityControl", "QualityControl"),
			new Link("QualityClaimForm", "Quality Claim", "QualityControl", "QualityControl"),
			new Link("LegalActionDetailsForm", "Legal Action Details", "Legal", "Legal"),
			new Link("LegalActivityDetailsForm", "Legal Activity Details", "Legal", "Legal"),
			new Link("EventDetailsForm", "Event", "CRM", "CRM"),
			new Link("ActivityDetailsForm", "Activity", "CRM", "CRM"),
			new Link("OpportunityDetailsForm", "Opportunity", "CRM", "CRM"),
			new Link("CampaignDetailsForm", "Campaign", "CRM", "CRM"),
			new Link("TaskTransactionForm", "Task", "CRM", "CRM"),
			new Link("PropertyRentDetailsForm", "Rental Registration", "Property", "Property"),
			new Link("PropertyRentRenewDetailsForm", "Rental Renewal", "Property", "Property"),
			new Link("PropertyRentCancellationForm", "Rental Cancellation", "Property", "Property"),
			new Link("RentIncomePostingDetails", "Rental Income Posting", "Property", "Property"),
			new Link("PropertyServiceAssignForm", "Service Assign", "Property", "Property"),
			new Link("PropertyServiceRequestForm", "Service Request", "Property", "Property"),
			new Link("RequisitionDetailsForm", "Requisition", "EnterpriseAsset", "EnterpriseAsset"),
			new Link("MobilisationForm", "Mobilization", "EnterpriseAsset", "EnterpriseAsset"),
			new Link("EquipmentTransferForm", "Equipment Transfer", "EnterpriseAsset", "EnterpriseAsset"),
			new Link("WorkOrderForm", "Equipment WorkOrder", "EnterpriseAsset", "EnterpriseAsset"),
			new Link("WorkOrderInventoryIssueForm", "Work Order Inventory Issue", "EnterpriseAsset", "EnterpriseAsset"),
			new Link("WorkOrderInventoryReturnForm", "Work Order Inventory Return", "EnterpriseAsset", "EnterpriseAsset")
		};

		public string Description
		{
			get
			{
				return "Transactions shortcut.";
			}
			set
			{
			}
		}

		public GadgetCategories Category
		{
			get
			{
				return GadgetCategories.General;
			}
			set
			{
			}
		}

		public event EventHandler DataLoadCompleted;

		public GadgetTransactionShortcuts()
		{
			InitializeComponent();
			backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
		}

		private void FavoriteBankAccountsGadget_ClientSizeChanged(object sender, EventArgs e)
		{
		}

		public void LoadData(bool isRefresh)
		{
			this.isRefresh = isRefresh;
			LoadLinks();
			backgroundWorker.RunWorkerAsync();
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (!isDataLoaded || isRefresh)
				{
					LinksCollection linksCollection = e.Result as LinksCollection;
					Links.Clear();
					if (linksCollection != null)
					{
						Links = linksCollection;
					}
					LoadLinks();
					if (this.DataLoadCompleted != null)
					{
						this.DataLoadCompleted(sender, e);
					}
					isRefresh = false;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				LinksCollection linksCollection = new LinksCollection();
				string key = base.Name + base.Parent.Name;
				byte[] binaryData = Factory.SettingSystemAsync.GetBinaryData(Global.CurrentUser, key);
				if (binaryData != null && binaryData.Length != 0)
				{
					object obj = GadgetsHelper.DeserializeFromStream(binaryData);
					if (obj != null)
					{
						linksCollection = (LinksCollection)(e.Result = (obj as LinksCollection));
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
		}

		private void LoadLinks()
		{
			if (parentGroup != null)
			{
				parentGroup.DragDropFlags = (NavBarDragDrop.AllowDrag | NavBarDragDrop.AllowDrop);
				AllowDrop = true;
				parentGroup.ItemLinks.Clear();
				NavBarItem navBarItem = null;
				parentGroup.GroupStyle = NavBarGroupStyle.SmallIconsList;
				foreach (Link link in Links)
				{
					navBarItem = new NavBarItem(link.DisplayName);
					navBarItem.Tag = link;
					navBarItem.CanDrag = true;
					navBarItem.LinkClicked += barItem_LinkClicked;
					parentGroup.ItemLinks.Add(navBarItem);
				}
				navBarItem = new NavBarItem("");
				parentGroup.ItemLinks.Add(navBarItem);
				navBarItem.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
				navBarItem.CanDrag = false;
				navBarItem = new NavBarItem("Customize...");
				navBarItem.Tag = null;
				navBarItem.LinkClicked += Customize_LinkClicked;
				parentGroup.ItemLinks.Add(navBarItem);
			}
		}

		private void Customize_LinkClicked(object sender, NavBarLinkEventArgs e)
		{
			CustomizeLinkGadgetForm customizeLinkGadgetForm = new CustomizeLinkGadgetForm();
			customizeLinkGadgetForm.LoadData(AvailableLinks, Links);
			if (customizeLinkGadgetForm.ShowDialog(this) == DialogResult.OK)
			{
				Links.Clear();
				Links = customizeLinkGadgetForm.SelectedLinks;
				string key = base.Name + base.Parent.Name;
				MemoryStream memoryStream = GadgetsHelper.SerializeToStream(Links);
				Factory.SettingSystem.SaveSettingStream(key, Global.CurrentUser, memoryStream.ToArray());
				LoadLinks();
			}
		}

		private void barItem_LinkClicked(object sender, NavBarLinkEventArgs e)
		{
			GlobalEvents.OnShortcutLinkClicked(e.Link.Item.Tag, e);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			imageList1 = new System.Windows.Forms.ImageList();
			SuspendLayout();
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(48, 48);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Name = "GadgetTransactionShortcuts";
			base.Size = new System.Drawing.Size(51, 42);
			ResumeLayout(false);
		}
	}
}
