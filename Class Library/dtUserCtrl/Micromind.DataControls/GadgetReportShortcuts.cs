using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraNavBar;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.FlatDashboard;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GadgetReportShortcuts : UserControl, IGadget, ILinkGadget
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

		public string GadgetTitle
		{
			get
			{
				return "Report Shortcuts";
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

		public GadgetTypes GadgetType => GadgetTypes.ReportShortcuts;

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

		public ViewType ChartType
		{
			set
			{
			}
		}

		private LinksCollection AvailableLinks => new LinksCollection
		{
			new Link("GLReportForm", "General Ledger Report", "Account", "Account"),
			new Link("JournalReportForm", "Journal Report", "Account", "Account"),
			new Link("BalanceSheetReportForm", "Balance Sheet Report", "Account", "Account"),
			new Link("CashFlowReportForm", "Cash Flow Report", "Account", "Account"),
			new Link("ProfitAndLossReportForm", "Profit and Loss Report", "Account", "Account"),
			new Link("ProfitAndLossMonthwiseReportForm", "Profit and Loss Report Monthwise", "Account", "Account"),
			new Link("ProfitAndLossComparisonReportForm", "Profit and Loss Report Comparison", "Account", "Account"),
			new Link("CashFlowReportForm", "Cash Flow Statement", "Account", "Account"),
			new Link("DailyCashReportForm", "Daily Cash Report", "Account", "Account"),
			new Link("AccountTransactionsReportForm", "Account Transactions", "Account", "Account"),
			new Link("AccountAnalysisReportForm", "AccountAnalysis", "Account", "Account"),
			new Link("AccountLedgerReportForm", "AccountLedger", "Account", "Account"),
			new Link("AccountCostCenterReportForm", "CostCenter", "Account", "Account"),
			new Link("BankLedgerReportForm", "Bank Book", "Account", "Account"),
			new Link("TrialBalanceReportForm", "Trial Balance Report", "Account", "Account"),
			new Link("BankReconciliationsReportForm", "Bank Reconciliations", "Account", "Account"),
			new Link("BankNotReconciledReportForm", "Bank Not Reconciled", "Account", "Account"),
			new Link("PDCIssuedReportForm", "PDC Issued", "Account", "Account"),
			new Link("PDCReceivedReportForm", "PDC Received", "Account", "Account"),
			new Link("DiscountChequesReportForm", "Discounted Cheques", "Account", "Account"),
			new Link("ChequeMaturityReportForm", "Cheque Maturity", "Account", "Account"),
			new Link("CustomerBalanceSummaryReport", "Customer Balance Summary", "Customer", "Customer"),
			new Link("CustomerBalanceDetailsReport", "Customer Ledger Report", "Customer", "Customer"),
			new Link("CustomerListReport", "Customer List Report", "Customer", "Customer"),
			new Link("CustomerProfileReport", "Customer Profile Report", "Customer", "Customer"),
			new Link("CustomerAgingSummaryReport", "Customer Aging Summary", "Customer", "Customer"),
			new Link("CustomerContactListReport", "Customer Contact List Report", "Customer", "Customer"),
			new Link("CustomerActivityReport", "Customer Activity Report", "Customer", "Customer"),
			new Link("CustomerOutstandingInvoicesReport", "Customer Outstanding Invoices Report", "Customer", "Customer"),
			new Link("CustomerStatementForm", "Customer Statement", "Customer", "Customer"),
			new Link("SalesByCustomerGroupReport", "Sales by Customer Group Report", "Customer", "Sales"),
			new Link("SalesByCustomerReport", "Sales by Customer Report", "Customer", "Sales"),
			new Link("SalesByItemReport", "Sales by Item Report", "Customer", "Sales"),
			new Link("SalesByLocationReport", "Sales by Location Report", "Customer", "Sales"),
			new Link("SalesByProductCategoryReport", "SalesByProductCategory", "Customer", "Sales"),
			new Link("SalesByItemCustomerSalespersonReport", "SalesByItemCustomerSalesperson", "Customer", "Sales"),
			new Link("SalesByGRNSummaryReport", "SalesByGRNSummary", "Customer", "Sales"),
			new Link("SalesByProductClassandCategoryReport", "SalesByProductClassandCategory", "Customer", "Sales"),
			new Link("SalesManDueReport", "SalesManDue", "Customer", "Sales"),
			new Link("SalesBySalespersonReport", "Sales by Salesperson Report", "Customer", "Sales"),
			new Link("SalesByGRNReport", "Sales by GRN", "Customer", "Sales"),
			new Link("PendingDNReport", "Pending DN", "Customer", "Sales"),
			new Link("PickListReport", "Export PickList", "Customer", "Sales"),
			new Link("VendorListReport", "Vendor List Report", "Vendor", "Vendor"),
			new Link("VendorContactListReport", "Vendor Contact List Report", "Vendor", "Vendor"),
			new Link("VendorBalanceSummaryReport", "Vendor Balance Summary Report", "Vendor", "Vendor"),
			new Link("VendorBalanceDetailsReport", "Vendor Ledger Report", "Vendor", "Vendor"),
			new Link("VendorAgingSummaryReport", "Vendor Aging Summary Report", "Vendor", "Vendor"),
			new Link("VendorActivityReport", "Vendor Activity Report", "Vendor", "Vendor"),
			new Link("VendorProfileReport", "Vendor Profile Report", "Vendor", "Vendor"),
			new Link("PendingConsignInReport", "Pending Consign In Report", "Vendor", "Purchase"),
			new Link("PurchaseByBuyerReport", "Purchase by Buyer Report", "Vendor", "Purchase"),
			new Link("PurchaseByItemReport", "Purchase by Item Report", "Vendor", "Purchase"),
			new Link("PurchaseByLocationReport", "Purchase by Location Report", "Vendor", "Purchase"),
			new Link("PurchaseByProductCategoryReport", "Purchase by Product Category Report", "Vendor", "Purchase"),
			new Link("PurchaseByVendorGroupReport", "Purchase by Vendor Group Report", "Vendor", "Purchase"),
			new Link("PurchaseByVendorReport", "Purchase by Vendor Report", "Vendor", "Purchase"),
			new Link("PurchaseComparisonReport", "Purchase Comparison", "Vendor", "Purchase"),
			new Link("PendingGRNReport", "PendingGRN", "Vendor", "Purchase"),
			new Link("PurchaseExpenseAllocationReport", "PurchaseExpense Allocation", "Vendor", "Purchase"),
			new Link("ItemMovementGRNReport", "ItemMovement GRN", "Vendor", "Purchase"),
			new Link("PurchaseOrderDetailReport", "PurchaseOrder Detail", "Vendor", "Purchase"),
			new Link("PurchaseLogReport", "Purchase Log", "Vendor", "Purchase"),
			new Link(" PurchaseByItemVendorBuyerReport", "Purchase By ItemVendorBuyer", "Vendor", "Purchase"),
			new Link("InventoryTransactionsReport", "Inventory Transactions Report", "Inventory", "Inventory"),
			new Link("InventoryTransactionsLotwiseReport", "Inventory Ledger Lotwise", "Inventory", "Inventory"),
			new Link("MatrixProductStockListReport", "Matrix Inventory Valuation", "Inventory", "Inventory"),
			new Link("InventoryAgingReport", "Inventory Aging Summary", "Inventory", "Inventory"),
			new Link("InventoryAdjustmentReport", "Inventory Adjustment", "Inventory", "Inventory"),
			new Link("InventoryTransferReport", "Inventory Transfer Report", "Inventory", "Inventory"),
			new Link("ProductListReport", "Product List Report", "Inventory", "Inventory"),
			new Link("ProductCatalogReport", "Product Catalog Report", "Inventory", "Inventory"),
			new Link("ProductPriceListReport", "Product Price List Report", "Inventory", "Inventory"),
			new Link("ProductStockListReport", "Product Stock List Report", "Inventory", "Inventory"),
			new Link("ProductStockPivotListReport", "Item Stock List Pivot", "Inventory", "Inventory"),
			new Link("W3PLInventoryTransactionsReport", "3PL Item Ledger", "Inventory", "Inventory"),
			new Link("W3PLProductStockPivotListReport", "3PL Item Stock", "Inventory", "Inventory"),
			new Link("W3PLInventoryAgingReport", "3PL Stock Aging", "Inventory", "Inventory"),
			new Link("LeadProfileReport", "Lead Profile", "CRM", "CRM"),
			new Link("LeadBySourceReport", "Lead By Source", "CRM", "CRM"),
			new Link("UpcomingEventsReport", "Upcoming Events", "CRM", "CRM"),
			new Link("UpcomingOpportunitiesReport", "Upcoming Opportunities", "CRM", "CRM"),
			new Link("PendingConsignInReport", "Open Consignments List", "ConsignmentIn", "ConsignmentIn"),
			new Link("ConsignmentInReport", "Consign In Report", "ConsignmentIn", "ConsignmentIn"),
			new Link("ItemMovementConsignInReport", "Consign In Movement", "ConsignmentIn", "ConsignmentIn"),
			new Link("ConsignInReceiptReport", "Truck Consign In", "ConsignmentIn", "ConsignmentIn"),
			new Link("ConsignmentInSettlementReport", "Consign In-Awaiting  Settlement", "ConsignmentIn", "ConsignmentIn"),
			new Link("PendingConsignOutReport", "Open Consignments List", "ConsignOut", "ConsignOut"),
			new Link("ConsignmentOutReport", "Consignment Out Profitability Detail", "ConsignOut", "ConsignOut"),
			new Link("ConsignmentOutSettlementReport", "Consign Out-Awaiting  Settlement", "ConsignOut", "ConsignOut"),
			new Link("ConsignmentOutIssuedReport", "Consign Out Profitability Summary", "ConsignOut", "ConsignOut"),
			new Link("EmployeeBalanceSummaryReport", "Employee Balance Summary", "HR", "HR"),
			new Link("EmployeeBalanceDetailsReport", "Employee Ledger", "HR", "HR"),
			new Link("EmployeeSalaryReport", "Employee Payroll Transactions", "HR", "HR"),
			new Link("EmployeeListReport", "Employee List", "HR", "HR"),
			new Link("EmployeeProfileReport", "Employee Profile", "HR", "HR"),
			new Link("EmployeeHistoryReport", "Employee History", "HR", "HR"),
			new Link("EmployeeActivityReport", "Employee Activity", "HR", "HR"),
			new Link("EmployeeLeaveReport", "Employee Leave", "HR", "HR"),
			new Link("EmployeeLeaveStatusReport", "Employee Leave Status", "HR", "HR"),
			new Link("EmployeeAnnualLeaveDueReport", "Employee Annual Leave Due", "HR", "HR"),
			new Link("EmployeeOverTimeReport", "Employee OverTime", "HR", "HR"),
			new Link("EmployeeGraduityEligibilityReport", "Employee Graduity Eligibility", "HR", "HR"),
			new Link("EmployeeSalaryReport", "Employee Salary", "HR", "HR"),
			new Link("EmployeeSalarySlipReport", "Employee Salary Slip", "HR", "HR"),
			new Link("EmployeeLoanReport", "Employee Loan", "HR", "HR"),
			new Link("EmployeeFinalSettlementReport", "Employee FinalSettlement", "HR", "HR"),
			new Link("ProjectAccountTransactionsReportForm", "Project Account Transactions", "Projects", "Projects"),
			new Link("ProjectInventoryTransactionsReport", "Project Inventory Transactions", "Projects", "Projects"),
			new Link("JobBudgetVsActualReport", "Budget vs Actual", "Projects", "Projects"),
			new Link("ProjectStatusReportForm", "Project Status", "Projects", "Projects"),
			new Link("MaterialVarianceReportForm", "Project Material Variance", "Projects", "Projects"),
			new Link("JobSummaryReportForm", "Project Summary", "Projects", "Projects"),
			new Link("ProjectDueReportForm", "Project Due for Billing", "Projects", "Projects"),
			new Link("ProjectManPowerReportForm", "Project Man Power", "Projects", "Projects"),
			new Link("ServiceCallTrackReportForm", "Service Call Track", "Projects", "Projects"),
			new Link("MaterialRequisitionReportForm", "Material Requisition", "Projects", "Projects"),
			new Link("AssemblyReport", "Assembly Report", "Manufacturing", "Manufacturing"),
			new Link("FixedAssetListReport", "Fixed Asset List", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetDepreciationReport", "Fixed Asset Depreciation ", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetTransactionsReport", "Fixed Asset Transaction", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetPurchaseReport", "Fixed Asset Purchase", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetSaleReport", "Fixed Asset Sales", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetTransferReport", "Fixed Asset Transfer", "FixedAsset", "FixedAsset"),
			new Link("PropertyRegistrationReport", "Registration", "Property", "Property"),
			new Link("PropertyRegistrationReport", "Renewal", "Property", "Property"),
			new Link("PropertyRegistrationReport", "Cancellation", "Property", "Property"),
			new Link("PropertyAvailabiltyReport", "Availability", "Property", "Property"),
			new Link("SmartListForm", "Smart List", "Smart List", "Smart List"),
			new Link("ChartCenterForm", "Pivot & Chart Center", "Pivot Report", "Pivot")
		};

		public string Description
		{
			get
			{
				return "Shortcuts to reports.";
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

		public GadgetReportShortcuts()
		{
			InitializeComponent();
			if (!Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, ((int)(5000 + GadgetType)).ToString()).Visible)
			{
				hasAccess = false;
			}
			backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
		}

		private void FavoriteBankAccountsGadget_ClientSizeChanged(object sender, EventArgs e)
		{
		}

		public void LoadData(bool isRefresh)
		{
			if (!IsBusy)
			{
				this.isRefresh = isRefresh;
				if (hasAccess)
				{
					LoadLinks();
				}
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (!isDataLoaded || isRefresh)
				{
					_ = e.Result;
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
				if (binaryData.Length != 0)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.GadgetReportShortcuts));
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			imageList1 = new System.Windows.Forms.ImageList();
			SuspendLayout();
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "report32x32.png");
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Name = "GadgetReportShortcuts";
			base.Size = new System.Drawing.Size(51, 42);
			ResumeLayout(false);
		}
	}
}
