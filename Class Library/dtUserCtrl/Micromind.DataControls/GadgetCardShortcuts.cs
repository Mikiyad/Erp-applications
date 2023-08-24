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
	public class GadgetCardShortcuts : UserControl, IGadget, ILinkGadget
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

		public ViewType ChartType
		{
			set
			{
			}
		}

		public string GadgetTitle
		{
			get
			{
				return "Card Shortcuts";
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

		public GadgetTypes GadgetType => GadgetTypes.CardShortcuts;

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

		private LinksCollection AvailableLinks => new LinksCollection
		{
			new Link("ChequebookDetailsForm", "Chequebook Details", "Account", "Account"),
			new Link("CompanyAccountDetailsForm", "Account Details", "Account", "Account"),
			new Link("CompanyAccountsListForm", "Accounts List", "Account", "Account"),
			new Link("AccountGroupDetailsForm", "Account Groups", "Account", "Account"),
			new Link("CostCenterDetailsForm", "Cost Center", "Account", "Account"),
			new Link("RegisterDetailsForm", "Register", "Account", "Account"),
			new Link("AnalysisDetailsForm", "Analysis", "Account", "Account"),
			new Link("AnalysisGroupDetailsForm", "Analysis Groups", "Account", "Account"),
			new Link("AccountAnalysisDetailsForm", "Account Analysis Setup", "Account", "Account"),
			new Link("ExpenseCodeDetailsForm", "Expense Code", "Account", "Account"),
			new Link("BankDetailsForm", "Bank", "Account", "Account"),
			new Link("BankFacilityGroupDetailsForm", "Bank Facility", "Account", "Account"),
			new Link("CurrencyDetailsForm", "Currency", "Account", "Account"),
			new Link("CurrencyRateUpdateForm", "Exchange Rate", "Account", "Account"),
			new Link("PaymentMethodDetailsForm", "Payment Method", "Account", "Account"),
			new Link("PaymentTermDetailsForm", "Payment Term", "Account", "Account"),
			new Link("ReturnedChequeReasonDetailsForm", "Bounced Cheque Reason", "Account", "Account"),
			new Link("TaxEntryForm", "Tax Entry", "Account", "Account"),
			new Link("TaxGroupForm", "Tax Group", "Account", "Account"),
			new Link("CustomerDetailsForm", "Customer Details", "Customer", "Customer"),
			new Link("CustomerClassDetailsForm", "Customer Class", "Customer", "Customer"),
			new Link("CustomerCategoryDetailsForm", "Customer Category", "Customer", "Customer"),
			new Link("CustomerAddressDetailsForm", "Customer Addresses", "Customer", "Customer"),
			new Link("SalespersonDetailsForm", "Salesperson", "Customer", "Customer"),
			new Link("ContactDetailsForm", "Contacts", "Customer", "Customer"),
			new Link("CountryDetailsForm", "Country", "Customer", "Customer"),
			new Link("AreaDetailsForm", "Area", "Customer", "Customer"),
			new Link("ShippingMethodDetailsForm", "Shipping Method", "Customer", "Customer"),
			new Link("NationalAccountForm", "National Account", "Customer", "Customer"),
			new Link("CustomerVendorLinkForm", "Customer/Vendor Relationship", "Customer", "Customer"),
			new Link("ActivityDetailsForm", "Customer Activity Details", "Customer", "Customer"),
			new Link("InsuranceProviderForm", "Insurance Provider Details", "Customer", "Customer"),
			new Link("VendorDetailsForm", "Vendor Details", "Vendor", "Vendor"),
			new Link("VendorClassDetailsForm", "Vendor Class", "Vendor", "Vendor"),
			new Link("VendorGroupDetailsForm", "Vendor Group", "Vendor", "Vendor"),
			new Link("VendorAddressDetailsForm", "Vendor Addresses", "Vendor", "Vendor"),
			new Link("BuyerDetailsForm", "Buyer", "Vendor", "Vendor"),
			new Link("ContactDetailsForm", "Contacts", "Vendor", "Vendor"),
			new Link("TransporterDetailsForm", "Transporter", "Vendor", "Vendor"),
			new Link("PortDetailsForm", "Port", "Vendor", "Vendor"),
			new Link("INCODetailsForm", "Incoterm", "Vendor", "Vendor"),
			new Link("ContainerSizeDetailsForm", "Container Size", "Vendor", "Vendor"),
			new Link("ProductDetailsForm", "Product Details", "Inventory", "Inventory"),
			new Link("ProductListForm", "Product List", "Inventory", "Inventory"),
			new Link("ProductClassDetailsForm", "Item Class", "Inventory", "Inventory"),
			new Link("MatrixProductDetailsForm", "Matrix Item", "Inventory", "Inventory"),
			new Link("ProductCategoryDetailsForm", "Product Category", "Inventory", "Inventory"),
			new Link("ProductBrandDetailsForm", "Product Brand", "Inventory", "Inventory"),
			new Link("ProductManufacturerDetailsForm", "Product Manufactorer", "Inventory", "Inventory"),
			new Link("DimensionDetailsForm", "Matrix Dimension", "Inventory", "Inventory"),
			new Link(" MatrixTemplateDetailsForm", "Matrix Template", "Inventory", "Inventory"),
			new Link("UnitDetailsForm", "Unit of Measurement", "Inventory", "Inventory"),
			new Link("AdjustmentTypeDetailsForm", "Inventory Adjustment Type", "Inventory", "Inventory"),
			new Link("InventoryTransferTypeDetailsForm", "Inventory Transfer Type", "Inventory", "Inventory"),
			new Link("CityDetailsForm", "City", "Inventory", "Inventory"),
			new Link("DriverDetailsForm", "Driver", "Inventory", "Inventory"),
			new Link("VehicleDetailsForm", "Vehicle", "Inventory", "Inventory"),
			new Link("VehicleDocTypeDetailsForm", "Vehicle Doc Type", "Inventory", "Inventory"),
			new Link("PackageDetailsForm", "Package", "Inventory", "Inventory"),
			new Link("ServiceItemForm", "Service Item", "Inventory", "Inventory"),
			new Link("ServiceProviderForm", "Service Provider", "Inventory", "Inventory"),
			new Link("ReleaseTypeForm", "Release Type", "Inventory", "Inventory"),
			new Link("RiderSummaryDetailsForm", "Rider Summary", "Inventory", "Inventory"),
			new Link("HorseSummaryDetailsForm", "Horse Summary", "Inventory", "Inventory"),
			new Link("HorseTypeForm", "Horse Type", "Inventory", "Inventory"),
			new Link("EmployeeDetailsForm", "Employee Details", "HR", "HR"),
			new Link("EmployeeDocumentsForm", "Employee Documents", "HR", "HR"),
			new Link("EmployeeLeaveDetailForm", "Employee Leave Detail", "HR", "HR"),
			new Link("GradeDetailsForm", "Grade", "HR", "HR"),
			new Link("DegreeDetailsForm", "Degree", "HR", "HR"),
			new Link("DivisionDetailsForm", "Division", "HR", "HR"),
			new Link("DepartmentDetailsForm", "Department", "HR", "HR"),
			new Link("PositionDetailsForm", "Position", "HR", "HR"),
			new Link("SkillDetailsForm", "Skill", "HR", "HR"),
			new Link("WorkLocationDetailsForm", "Work Location", "HR", "HR"),
			new Link("EmployeeGroupDetailsForm", "Employee Group", "HR", "HR"),
			new Link("EmployeeTypeDetailsForm", "Employee Class", "HR", "HR"),
			new Link("SponsorDetailsForm", "Sponsor", "HR", "HR"),
			new Link("NationalityDetailsForm", "Nationality", "HR", "HR"),
			new Link("ReligionDetailsForm", "Religion", "HR", "HR"),
			new Link("EmployeeDocTypeDetailsForm", "Employee Document Type", "HR", "HR"),
			new Link("EmployeeSkillsForm", "Employee Skill", "HR", "HR"),
			new Link("EmployeeDocumentsForm", "Employee Document", "HR", "HR"),
			new Link("DisciplineActionTypeDetailsForm", "Disciplinary Action Type", "HR", "HR"),
			new Link("EmployeeActivityTypeDetailsForm", "Employee General Activity Type", "HR", "HR"),
			new Link("PayrollItemDetailsForm", "Payroll Item", "HR", "HR"),
			new Link("PayrollItemDetailsForm", "Deduction Item", "HR", "HR"),
			new Link("BenefitDetailsForm", "Benefit Item", "HR", "HR"),
			new Link("OverTimeDetailsForm", "Overtime", "HR", "HR"),
			new Link("EOSRuleDetailsForm", "End of Service Benefit", "HR", "HR"),
			new Link("LeaveTypeDetailsForm", "Leave Type", "HR", "HR"),
			new Link("EmployeeSalaryDetailsForm", "Salary Details", "HR", "HR"),
			new Link("EmployeeLeaveProcessForm", "Employee Leave Process", "HR", "HR"),
			new Link("EmployeeLeaveDetailForm", "Employee Leave Detail", "HR", "HR"),
			new Link("CompanyDocTypeDetailsForm", "Company Document Type", "HR", "HR"),
			new Link("CompanyDocumentsForm", "Company Documents", "HR", "HR"),
			new Link("TenancyContractDetailsForm", "Tenancy Contracts", "HR", "HR"),
			new Link("TradeLicenseDetailsForm", "Trade Licenses", "HR", "HR"),
			new Link("VisaDetailsForm", "Visa Details", "HR", "HR"),
			new Link("CandidateCancellationForm", "Cancellation", "HR", "HR"),
			new Link("EmployeePassportControlForm", "Passport Control", "HR", "HR"),
			new Link("HolidayCalendarForm", "Holiday Calendar", "HR", "HR"),
			new Link("AppointmentDetailsForm", "Appointment Details", "HR", "HR"),
			new Link("FixedAssetDetailsForm", "Fixed Asset", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetClassDetailsForm", "Fixed Asset Class", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetGroupDetailsForm", "Fixed Asset Group", "FixedAsset", "FixedAsset"),
			new Link("FixedAssetLocationDetailsForm", "Fixed Asset Location", "FixedAsset", "FixedAsset"),
			new Link("BOMDetailsForm", "Bill of Materials", "Manufacturing", "Manufacturing"),
			new Link("ExpenseCodeDetailsForm", "Expense Code", "Manufacturing", "Manufacturing"),
			new Link("JobDetailsForm", "Job/Project", "Project", "Project"),
			new Link("JobTypeDetailsForm", "Job Type", "Project", "Project"),
			new Link("CostCategoryDetailsForm", "Cost Category", "Project", "Project"),
			new Link("JobTaskDetailsForm", "Project Task", "Project", "Project"),
			new Link("EquipmentDetailsForm", "Equipment", "Project", "Project"),
			new Link("JobBOMDetailsForm", "BOM", "Project", "Project"),
			new Link("ArrivalReportTemplateForm", "Arrival Report Template", "QC", "QC"),
			new Link("SurveyorForm", "Surveyor", "QC", "QC")
		};

		public string Description
		{
			get
			{
				return "Displays shortcut to selected card screens.";
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

		public GadgetCardShortcuts()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.GadgetCardShortcuts));
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			imageList1 = new System.Windows.Forms.ImageList();
			SuspendLayout();
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "profile.png");
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Name = "GadgetCardShortcuts";
			base.Size = new System.Drawing.Size(51, 42);
			ResumeLayout(false);
		}
	}
}
