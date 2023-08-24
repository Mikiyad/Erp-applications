using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraRichEdit;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerDetailsForm : Form, IForm
	{
		private CustomerData currentData;

		private const string TABLENAME_CONST = "Customer";

		private const string IDFIELD_CONST = "CustomerID";

		private bool isNewRecord = true;

		private string ARAccountID = "";

		private bool disableCustomerCreditLimit = CompanyPreferences.DisableCustomerCreditLimit;

		private MMTextBox textBoxName;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxCode;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton buttonClose;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private MMTextBox textBoxFormalName;

		private FormManager formManager;

		private MMTextBox textBoxForeignName;

		private CheckBox checkBoxHold;

		private Panel panel1;

		private MMTextBox textBoxAddressID;

		private MMTextBox textBoxPostalCode;

		private MMTextBox textBoxEmail;

		private MMTextBox textBoxMobile;

		private MMTextBox textBoxFax;

		private MMTextBox textBoxPhone2;

		private MMTextBox textBoxPhone1;

		private MMTextBox textBoxCountry;

		private MMTextBox textBoxState;

		private MMTextBox textBoxCity;

		private MMTextBox textBoxAddress3;

		private MMTextBox textBoxAddress2;

		private MMTextBox textBoxAddress1;

		private MMTextBox textBoxContactName;

		private XPButton buttonMoreAddress;

		private MMTextBox textBoxWebsite;

		private MMTextBox textBoxDepartment;

		private MMTextBox textBoxComment;

		private MMTextBox textBoxBankAccountNumber;

		private MMTextBox textBoxBankBranch;

		private MMTextBox textBoxBankName;

		private DataEntryGrid dataGridContacts;

		private customersFlatComboBox comboBoxParentCustomer;

		private CustomerClassComboBox comboBoxCustomerClass;

		private CountryComboBox comboBoxCountry;

		private AreaComboBox comboBoxArea;

		private PriceLevelComboBox comboBoxPriceLevel;

		private ShippingMethodsComboBox comboBoxShippingMethods;

		private CustomerAddressComboBox comboBoxShiptoAddress;

		private CustomerAddressComboBox comboBoxBilltoAddress;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private CurrencyComboBox comboBoxCurrency;

		private MMLabel labelCustomerNameHeader;

		private MMSDateTimePicker dateTimePickerEstablished;

		private MMSDateTimePicker dateTimePickerCustomerSince;

		private XPButton buttonCategories;

		private ContextMenuStrip contextMenuStripContact;

		private ToolStripMenuItem openContactToolStripMenuItem;

		private ContactsComboBox gridComboBoxContact;

		private UDFEntryControl udfEntryGrid;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private MMTextBox textBoxNote;

		private ToolStripMenuItem newContactToolStripMenuItem;

		private ToolStripMenuItem deleteContactToolStripMenuItem;

		private ToolStripMenuItem deleteRowToolStripMenuItem;

		private PercentTextBox textBoxConsignCommission;

		private CheckBox checkBoxAllowConsignment;

		private CheckBox checkBoxWeightInvoice;

		private RadioButton radioButtonSublimit;

		private CheckBox checkBoxAcceptPDC;

		private AmountTextBox textBoxCreditLimit;

		private RadioButton radioButtonCreditLimitNoCredit;

		private RadioButton radioButtonCreditLimitUnlimited;

		private RadioButton radioButtonCreditLimitAmount;

		private UserComboBox comboBoxCollectionUser;

		private MMSDateTimePicker dateTimePickerReviewDate;

		private MMTextBox textBoxPaymentTermName;

		private MMTextBox textBoxPaymentMethodName;

		private UltraComboEditor comboBoxRating;

		private PaymentTermComboBox comboBoxPaymentTerms;

		private paymentMethodsComboBox comboBoxPaymentMethods;

		private MMTextBox textBoxInsuranceRemarks;

		private MMTextBox textBoxInsuranceNumber;

		private AmountTextBox textBoxInsuranceApprovedAmount;

		private AmountTextBox textBoxInsuranceReqAmount;

		private MMSDateTimePicker dateTimePickerInsuranceDate;

		private ComboBox comboBoxInsuranceStatus;

		private CheckBox checkBoxAcceptCheque;

		private GenericListComboBox comboBoxLeadSource;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonInformation;

		private MMSDateTimePicker dateTimePickerLicenseExpDate;

		private MMSDateTimePicker dateTimePickerContractExpDate;

		private Button buttonAddActivity;

		private GadgetDateRangeComboBox comboBoxActivityPeriod;

		private DataGridList dataGridActivities;

		private UserComboBox comboBoxCreditReviewBy;

		private UserComboBox comboBoxRatingBy;

		private MMSDateTimePicker dateTimePickerRatingDate;

		private UltraComboEditor comboBoxInsuranceRating;

		private ComboBox comboBoxStatementMethod;

		private MMTextBox textBoxStatementEmail;

		private RichEditControl textBoxProfileDetails;

		private MMTextBox textBoxTempLimit;

		private ToolStripButton toolStripButtonComments;

		private MMTextBox textBoxRatingRemarks;

		private MMTextBox textBoxDeliveryInstructions;

		private MMTextBox textBoxAccountInstructions;

		private MMTextBox textBoxInsuranceID;

		private InsuranceProviderComboBox comboBoxInsuranceProvider;

		private MMTextBox textBoxProvider;

		private MMSDateTimePicker dateTimePickerValidTo;

		private MMSDateTimePicker datetimePickerEffectiveDate;

		private CheckBox checkBoxparentACforposting;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraPictureBox ultraPictureBox1;

		private MMTextBox textBoxLongitude;

		private MMTextBox textBoxLatitude;

		private CustomerGroupComboBox comboBoxCustomerGroup;

		private ComboBox comboBoxTaxOption;

		private MMTextBox textBoxTaxIDNumber;

		private TaxGroupComboBox comboBoxTaxGroup;

		private AmountTextBox textBoxUnsecuredLimit;

		private CheckBox checkBoxUnsecuredLimit;

		private MMSDateTimePicker dateTimePickerCLValidity;

		private XPButton buttonAccounts;

		private MMSDateTimePicker dateTimeBalanceConfirmationDate;

		private NumberTextBox textBoxConfirmationLevel;

		private PercentTextBox textBoxDiscountPercent;

		private PercentTextBox textBoxRebatePercent;

		private ImageList imageListComments;

		private XPButton buttonCustomerInsuranceClaim;

		private ToolStripButton toolStripButtonHistory;

		private NumberTextBox textBoxGraceDays;

		private MMTextBox textBoxLicenseNumber;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private PictureBox pictureBoxPhoto;

		private OpenFileDialog openFileDialog1;

		private UltraFormattedLinkLabel linkLoadImage;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem PlantiffToolStripMenuItem;

		private ToolStripMenuItem defendantToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private LayoutControl layoutControl1;

		private LayoutControlGroup Root;

		private TabbedControlGroup tabbedControlGroup1;

		private LayoutControlGroup tabPageGeneral;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem5;

		private LayoutControlItem layoutControlItem6;

		private LayoutControlItem layoutControlItem7;

		private LayoutControlItem layoutControlItem8;

		private LayoutControlItem layoutControlItem9;

		private LayoutControlItem layoutControlItem10;

		private LayoutControlItem layoutControlItem11;

		private LayoutControlItem layoutControlItem12;

		private LayoutControlItem layoutControlItem13;

		private LayoutControlItem layoutControlItem14;

		private EmptySpaceItem emptySpaceItem2;

		private EmptySpaceItem emptySpaceItem3;

		private LayoutControlItem layoutControlItem15;

		private LayoutControlGroup layoutControlGroup4;

		private LayoutControlItem layoutControlItem16;

		private LayoutControlItem layoutControlItem17;

		private LayoutControlItem layoutControlItem18;

		private LayoutControlItem layoutControlItem26;

		private LayoutControlItem layoutControlItem27;

		private LayoutControlItem layoutControlItem19;

		private LayoutControlItem layoutControlItem20;

		private LayoutControlItem layoutControlItem21;

		private LayoutControlItem layoutControlItem22;

		private LayoutControlItem layoutControlItem23;

		private LayoutControlItem layoutControlItem24;

		private LayoutControlItem layoutControlItem28;

		private LayoutControlItem layoutControlItem29;

		private EmptySpaceItem emptySpaceItem5;

		private LayoutControlGroup tabPageDetails;

		private LayoutControlGroup layoutControlGroup3;

		private LayoutControlItem layoutControlItem30;

		private LayoutControlItem layoutControlItem31;

		private LayoutControlItem layoutControlItem32;

		private LayoutControlItem layoutControlItem34;

		private LayoutControlItem layoutControlItem35;

		private LayoutControlItem layoutControlItem36;

		private LayoutControlItem layoutControlItem33;

		private LayoutControlItem layoutControlItem37;

		private LayoutControlItem layoutControlItem38;

		private LayoutControlItem layoutControlItem39;

		private LayoutControlItem layoutControlItem40;

		private LayoutControlItem layoutControlItem41;

		private LayoutControlItem layoutControlItem42;

		private LayoutControlItem layoutControlItem43;

		private LayoutControlItem layoutControlItem44;

		private LayoutControlItem layoutControlItem45;

		private LayoutControlItem layoutControlItem46;

		private EmptySpaceItem emptySpaceItem9;

		private EmptySpaceItem emptySpaceItem10;

		private LayoutControlItem layoutControlItem47;

		private LayoutControlItem layoutControlItem50;

		private LayoutControlItem layoutControlItem51;

		private LayoutControlItem layoutControlItem52;

		private EmptySpaceItem emptySpaceItem11;

		private LayoutControlItem layoutControlItem53;

		private LayoutControlItem layoutControlItem54;

		private EmptySpaceItem emptySpaceItem12;

		private LayoutControlGroup layoutControlGroup5;

		private LayoutControlItem layoutControlItem55;

		private LayoutControlItem layoutControlItem56;

		private LayoutControlItem layoutControlItem57;

		private LayoutControlGroup layoutControlGroup6;

		private LayoutControlItem layoutControlItem58;

		private LayoutControlItem layoutControlItem59;

		private LayoutControlItem layoutControlItem60;

		private LayoutControlItem layoutItemConsignmentCom;

		private LayoutControlItem layoutControlItem48;

		private EmptySpaceItem emptySpaceItem15;

		private LayoutControlGroup layoutControlGroup7;

		private EmptySpaceItem emptySpaceItem14;

		private LayoutControlItem layoutControlItem61;

		private LayoutControlItem layoutControlItem62;

		private LayoutControlItem layoutControlItem63;

		private LayoutControlItem layoutControlItem64;

		private LayoutControlItem layoutControlItem65;

		private LayoutControlItem layoutControlItem66;

		private LayoutControlItem layoutControlItem67;

		private LayoutControlItem layoutControlItem68;

		private LayoutControlItem layoutControlItem69;

		private LayoutControlItem layoutControlItem70;

		private LayoutControlItem layoutControlItem72;

		private LayoutControlItem layoutControlItem73;

		private LayoutControlItem layoutControlItem71;

		private LayoutControlItem layoutControlItem74;

		private LayoutControlItem layoutControlItem75;

		private LayoutControlGroup layoutGroupCreditLimit;

		private LayoutControlItem layoutControlItem77;

		private LayoutControlItem layoutControlItem78;

		private LayoutControlItem layoutControlItem81;

		private LayoutControlItem layoutControlItem79;

		private LayoutControlItem layoutControlItem80;

		private LayoutControlItem layoutControlItem82;

		private LayoutControlItem layoutControlItem83;

		private LayoutControlItem layoutControlItem84;

		private LayoutControlItem layoutControlItem85;

		private LayoutControlItem layoutControlItem86;

		private LayoutControlItem layoutControlItem87;

		private LayoutControlItem layoutControlItem88;

		private LayoutControlItem layoutControlItem89;

		private LayoutControlItem layoutControlItem90;

		private LayoutControlItem layoutControlItem91;

		private LayoutControlItem layoutControlItem92;

		private EmptySpaceItem emptySpaceItem8;

		private LayoutControlItem layoutControlItem76;

		private LayoutControlGroup layoutControlGroup9;

		private LayoutControlItem layoutControlItem95;

		private LayoutControlItem layoutControlItem93;

		private LayoutControlGroup panelInsuranceDetails;

		private EmptySpaceItem emptySpaceItem16;

		private LayoutControlItem layoutControlItem94;

		private LayoutControlItem layoutControlItem97;

		private LayoutControlItem layoutControlItem98;

		private LayoutControlItem layoutControlItem99;

		private LayoutControlItem layoutControlItem100;

		private LayoutControlItem layoutControlItem101;

		private LayoutControlItem layoutControlItem102;

		private LayoutControlItem layoutControlItem103;

		private LayoutControlItem layoutControlItem104;

		private LayoutControlItem layoutControlItem105;

		private LayoutControlItem layoutControlItem96;

		private LayoutControlGroup layoutControlGroup10;

		private LayoutControlGroup tabPageActivity;

		private LayoutControlGroup layoutControlGroup12;

		private LayoutControlGroup layoutControlGroup13;

		private LayoutControlGroup layoutControlGroup14;

		private EmptySpaceItem emptySpaceItem18;

		private LayoutControlItem layoutControlItem106;

		private LayoutControlItem layoutControlItem107;

		private EmptySpaceItem emptySpaceItem19;

		private LayoutControlItem layoutControlItem108;

		private LayoutControlItem layoutControlItem109;

		private EmptySpaceItem emptySpaceItem20;

		private LayoutControlItem layoutControlItem110;

		private LayoutControlItem layoutControlItem111;

		private LayoutControlItem layoutControlItem112;

		private LayoutControlItem layoutControlItem113;

		private EmptySpaceItem emptySpaceItem1;

		private MMTextBox textBoxAddressPrintFormat;

		private LayoutControlItem layoutControlItem25;

		private EmptySpaceItem emptySpaceItem4;

		private EmptySpaceItem emptySpaceItem6;

		private EmptySpaceItem emptySpaceItem7;

		private ToolStripDropDownButton toolStripDropDownButtonEnquiry;

		private ToolStripMenuItem menuItemCustomerLedger;

		private ToolStripMenuItem menuItemSalesStatistics;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripDropDownButton toolStripButtonDesign;

		private ToolStripMenuItem menuItemLayoutDesign;

		private ToolStripMenuItem menuItemCustomFields;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isLoading;

		private string sourceLeadID = "";

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2003;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxCode.ReadOnly = false;
					textBoxAddressID.Enabled = false;
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkRemovePicture;
					bool flag2 = linkAddPicture.Enabled = false;
					bool visible = ultraFormattedLinkLabel2.Enabled = flag2;
					ultraFormattedLinkLabel.Visible = visible;
					comboBoxBilltoAddress.Filter("");
					comboBoxShiptoAddress.Filter("");
					toolStripButtonHistory.Visible = false;
					toolStripDropDownButtonEnquiry.Enabled = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					textBoxAddressID.Enabled = false;
					linkAddPicture.Enabled = true;
					comboBoxBilltoAddress.Filter(textBoxCode.Text);
					comboBoxShiptoAddress.Filter(textBoxCode.Text);
					toolStripButtonHistory.Visible = true;
					toolStripDropDownButtonEnquiry.Enabled = true;
				}
				buttonCategories.Enabled = !value;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonComments.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public string SourceLeadID
		{
			get
			{
				return sourceLeadID;
			}
			set
			{
				sourceLeadID = value;
			}
		}

		public CustomerDetailsForm()
		{
			InitializeComponent();
			dataGridContacts.DropDownMenu.Items.Add(new ToolStripSeparator());
			checked
			{
				int num;
				for (num = 0; num < contextMenuStripContact.Items.Count; num++)
				{
					dataGridContacts.DropDownMenu.Items.Add(contextMenuStripContact.Items[num]);
					num--;
				}
				if (!GlobalRules.IsModuleAvailable(AxolonModules.CRM))
				{
					tabPageActivity.Visibility = LayoutVisibility.Never;
				}
				openContactToolStripMenuItem.Click += openContactToolStripMenuItem_Click;
				newContactToolStripMenuItem.Click += newContactToolStripMenuItem_Click;
				deleteContactToolStripMenuItem.Click += deleteContactToolStripMenuItem_Click;
			}
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
			components = new System.ComponentModel.Container();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			Infragistics.Win.ValueListItem valueListItem = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem18 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem19 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem20 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem21 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem22 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerDetailsForm));
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance187 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance188 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance190 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance191 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance192 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance193 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance194 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance195 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance196 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance197 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance198 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance199 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance200 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance201 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance202 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance203 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance204 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance205 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance206 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance207 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance208 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance209 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance210 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance211 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance212 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance213 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance214 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance215 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance216 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance219 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance220 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance221 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance222 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance223 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance224 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance225 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance226 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance227 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance228 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance229 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance230 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance231 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance232 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance233 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance234 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance235 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance236 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance237 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance238 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance239 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance240 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance241 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance242 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance243 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance244 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance245 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance246 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance247 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance248 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance249 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance250 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance251 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance252 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance253 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance254 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance255 = new Infragistics.Win.Appearance();
			textBoxConfirmationLevel = new Micromind.UISupport.NumberTextBox();
			dateTimeBalanceConfirmationDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxRatingRemarks = new Micromind.UISupport.MMTextBox();
			comboBoxCreditReviewBy = new Micromind.DataControls.UserComboBox();
			comboBoxRatingBy = new Micromind.DataControls.UserComboBox();
			dateTimePickerRatingDate = new Micromind.UISupport.MMSDateTimePicker(components);
			buttonCustomerInsuranceClaim = new Micromind.UISupport.XPButton();
			comboBoxInsuranceProvider = new Micromind.DataControls.InsuranceProviderComboBox();
			textBoxProvider = new Micromind.UISupport.MMTextBox();
			dateTimePickerValidTo = new Micromind.UISupport.MMSDateTimePicker(components);
			datetimePickerEffectiveDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxInsuranceID = new Micromind.UISupport.MMTextBox();
			comboBoxInsuranceRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			textBoxInsuranceRemarks = new Micromind.UISupport.MMTextBox();
			textBoxInsuranceNumber = new Micromind.UISupport.MMTextBox();
			textBoxInsuranceApprovedAmount = new Micromind.UISupport.AmountTextBox();
			textBoxInsuranceReqAmount = new Micromind.UISupport.AmountTextBox();
			dateTimePickerInsuranceDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxInsuranceStatus = new System.Windows.Forms.ComboBox();
			textBoxPaymentTermName = new Micromind.UISupport.MMTextBox();
			textBoxPaymentMethodName = new Micromind.UISupport.MMTextBox();
			comboBoxRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			comboBoxPaymentTerms = new Micromind.DataControls.PaymentTermComboBox();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxGraceDays = new Micromind.UISupport.NumberTextBox();
			dateTimePickerCLValidity = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxUnsecuredLimit = new Micromind.UISupport.AmountTextBox();
			checkBoxUnsecuredLimit = new System.Windows.Forms.CheckBox();
			textBoxTempLimit = new Micromind.UISupport.MMTextBox();
			checkBoxAcceptCheque = new System.Windows.Forms.CheckBox();
			radioButtonSublimit = new System.Windows.Forms.RadioButton();
			checkBoxAcceptPDC = new System.Windows.Forms.CheckBox();
			textBoxCreditLimit = new Micromind.UISupport.AmountTextBox();
			radioButtonCreditLimitNoCredit = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitUnlimited = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitAmount = new System.Windows.Forms.RadioButton();
			comboBoxCollectionUser = new Micromind.DataControls.UserComboBox();
			dateTimePickerReviewDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dataGridContacts = new Micromind.DataControls.DataEntryGrid();
			gridComboBoxContact = new Micromind.DataControls.ContactsComboBox();
			comboBoxActivityPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			textBoxAddressPrintFormat = new Micromind.UISupport.MMTextBox();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxProfileDetails = new DevExpress.XtraRichEdit.RichEditControl();
			dataGridActivities = new Micromind.UISupport.DataGridList(components);
			buttonAddActivity = new System.Windows.Forms.Button();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			textBoxConsignCommission = new Micromind.UISupport.PercentTextBox();
			textBoxDeliveryInstructions = new Micromind.UISupport.MMTextBox();
			textBoxAccountInstructions = new Micromind.UISupport.MMTextBox();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			buttonAccounts = new Micromind.UISupport.XPButton();
			textBoxTaxIDNumber = new Micromind.UISupport.MMTextBox();
			textBoxLongitude = new Micromind.UISupport.MMTextBox();
			textBoxBankBranch = new Micromind.UISupport.MMTextBox();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			textBoxLicenseNumber = new Micromind.UISupport.MMTextBox();
			textBoxBankAccountNumber = new Micromind.UISupport.MMTextBox();
			textBoxRebatePercent = new Micromind.UISupport.PercentTextBox();
			comboBoxCustomerGroup = new Micromind.DataControls.CustomerGroupComboBox();
			ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
			textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
			buttonCategories = new Micromind.UISupport.XPButton();
			comboBoxPaymentMethods = new Micromind.DataControls.paymentMethodsComboBox();
			textBoxLatitude = new Micromind.UISupport.MMTextBox();
			buttonMoreAddress = new Micromind.UISupport.XPButton();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			checkBoxparentACforposting = new System.Windows.Forms.CheckBox();
			dateTimePickerContractExpDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimePickerLicenseExpDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			textBoxStatementEmail = new Micromind.UISupport.MMTextBox();
			comboBoxStatementMethod = new System.Windows.Forms.ComboBox();
			textBoxComment = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			checkBoxWeightInvoice = new System.Windows.Forms.CheckBox();
			checkBoxAllowConsignment = new System.Windows.Forms.CheckBox();
			textBoxDepartment = new Micromind.UISupport.MMTextBox();
			textBoxWebsite = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			comboBoxLeadSource = new Micromind.DataControls.GenericListComboBox();
			comboBoxPriceLevel = new Micromind.DataControls.PriceLevelComboBox();
			textBoxFormalName = new Micromind.UISupport.MMTextBox();
			comboBoxArea = new Micromind.DataControls.AreaComboBox();
			textBoxForeignName = new Micromind.UISupport.MMTextBox();
			dateTimePickerEstablished = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxEmail = new Micromind.UISupport.MMTextBox();
			dateTimePickerCustomerSince = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxCountry = new Micromind.DataControls.CountryComboBox();
			textBoxMobile = new Micromind.UISupport.MMTextBox();
			comboBoxBilltoAddress = new Micromind.DataControls.CustomerAddressComboBox();
			comboBoxParentCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxShippingMethods = new Micromind.DataControls.ShippingMethodsComboBox();
			textBoxPostalCode = new Micromind.UISupport.MMTextBox();
			textBoxFax = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			comboBoxShiptoAddress = new Micromind.DataControls.CustomerAddressComboBox();
			checkBoxHold = new System.Windows.Forms.CheckBox();
			textBoxPhone2 = new Micromind.UISupport.MMTextBox();
			comboBoxCustomerClass = new Micromind.DataControls.CustomerClassComboBox();
			textBoxAddressID = new Micromind.UISupport.MMTextBox();
			textBoxPhone1 = new Micromind.UISupport.MMTextBox();
			textBoxContactName = new Micromind.UISupport.MMTextBox();
			textBoxAddress1 = new Micromind.UISupport.MMTextBox();
			textBoxAddress2 = new Micromind.UISupport.MMTextBox();
			textBoxAddress3 = new Micromind.UISupport.MMTextBox();
			textBoxCity = new Micromind.UISupport.MMTextBox();
			textBoxState = new Micromind.UISupport.MMTextBox();
			textBoxCountry = new Micromind.UISupport.MMTextBox();
			Root = new DevExpress.XtraLayout.LayoutControlGroup();
			tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
			layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem64 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem65 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem66 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem67 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem68 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem69 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem70 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem72 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem73 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem71 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem74 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem75 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutGroupCreditLimit = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem77 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem78 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem81 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem79 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem80 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem82 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem83 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem84 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem85 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem86 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem87 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem88 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem89 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem90 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem91 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem92 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem76 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem95 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem93 = new DevExpress.XtraLayout.LayoutControlItem();
			panelInsuranceDetails = new DevExpress.XtraLayout.LayoutControlGroup();
			emptySpaceItem16 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem97 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem98 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem99 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem100 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem101 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem102 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem103 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem104 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem105 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem94 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem96 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem18 = new DevExpress.XtraLayout.EmptySpaceItem();
			tabPageGeneral = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			tabPageDetails = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem43 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem45 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem47 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem51 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem52 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem53 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem12 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem55 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem56 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem57 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem58 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem59 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem60 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutItemConsignmentCom = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
			emptySpaceItem14 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem61 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem62 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem63 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem54 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem106 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem107 = new DevExpress.XtraLayout.LayoutControlItem();
			tabPageActivity = new DevExpress.XtraLayout.LayoutControlGroup();
			emptySpaceItem19 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem108 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem109 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem20 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem110 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem111 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem112 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem113 = new DevExpress.XtraLayout.LayoutControlItem();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonComments = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonHistory = new System.Windows.Forms.ToolStripButton();
			toolStripButtonDesign = new System.Windows.Forms.ToolStripDropDownButton();
			menuItemLayoutDesign = new System.Windows.Forms.ToolStripMenuItem();
			menuItemCustomFields = new System.Windows.Forms.ToolStripMenuItem();
			toolStripDropDownButtonEnquiry = new System.Windows.Forms.ToolStripDropDownButton();
			menuItemCustomerLedger = new System.Windows.Forms.ToolStripMenuItem();
			menuItemSalesStatistics = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			PlantiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			defendantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			panel1 = new System.Windows.Forms.Panel();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			contextMenuStripContact = new System.Windows.Forms.ContextMenuStrip(components);
			openContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			imageListComments = new System.Windows.Forms.ImageList(components);
			formManager = new Micromind.DataControls.FormManager();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)comboBoxCreditReviewBy).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRatingBy).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceRating).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRating).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentTerms).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCollectionUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxActivityPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridActivities).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethods).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSource).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBilltoAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethods).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShiptoAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem64).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem65).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem66).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem67).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem68).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem69).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem70).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem72).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem73).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem71).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem74).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem75).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutGroupCreditLimit).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem77).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem78).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem81).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem79).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem80).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem82).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem83).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem84).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem85).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem86).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem87).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem88).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem89).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem90).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem91).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem92).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem76).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup9).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem95).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem93).BeginInit();
			((System.ComponentModel.ISupportInitialize)panelInsuranceDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem16).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem97).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem98).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem99).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem100).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem101).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem102).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem103).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem104).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem105).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem94).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem96).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem18).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabPageGeneral).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem18).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem26).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem27).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem19).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem20).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem21).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem22).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem23).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem24).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem28).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem29).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem30).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem31).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem32).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem34).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem35).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem36).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem33).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem37).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem25).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabPageDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem38).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem39).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem40).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem41).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem42).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem43).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem44).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem45).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem46).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem9).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem10).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem47).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem50).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem51).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem52).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem11).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem53).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem12).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem55).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem56).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem57).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup6).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem58).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem59).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem60).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutItemConsignmentCom).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem48).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem15).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup7).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem14).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem61).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem62).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem63).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem54).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup10).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem106).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem107).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabPageActivity).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem19).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem108).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem109).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem20).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem110).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup12).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem111).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup13).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem112).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup14).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem113).BeginInit();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStripContact.SuspendLayout();
			SuspendLayout();
			textBoxConfirmationLevel.AllowDecimal = false;
			textBoxConfirmationLevel.CustomReportFieldName = "";
			textBoxConfirmationLevel.CustomReportKey = "";
			textBoxConfirmationLevel.CustomReportValueType = 1;
			textBoxConfirmationLevel.IsComboTextBox = false;
			textBoxConfirmationLevel.IsModified = false;
			textBoxConfirmationLevel.Location = new System.Drawing.Point(630, 166);
			textBoxConfirmationLevel.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxConfirmationLevel.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxConfirmationLevel.Name = "textBoxConfirmationLevel";
			textBoxConfirmationLevel.NullText = "0";
			textBoxConfirmationLevel.Size = new System.Drawing.Size(269, 20);
			textBoxConfirmationLevel.TabIndex = 11;
			textBoxConfirmationLevel.Text = "0";
			dateTimeBalanceConfirmationDate.Checked = false;
			dateTimeBalanceConfirmationDate.CustomFormat = " ";
			dateTimeBalanceConfirmationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeBalanceConfirmationDate.Location = new System.Drawing.Point(191, 166);
			dateTimeBalanceConfirmationDate.Name = "dateTimeBalanceConfirmationDate";
			dateTimeBalanceConfirmationDate.ShowCheckBox = true;
			dateTimeBalanceConfirmationDate.Size = new System.Drawing.Size(268, 20);
			dateTimeBalanceConfirmationDate.TabIndex = 10;
			dateTimeBalanceConfirmationDate.Value = new System.DateTime(0L);
			textBoxRatingRemarks.BackColor = System.Drawing.Color.White;
			textBoxRatingRemarks.CustomReportFieldName = "";
			textBoxRatingRemarks.CustomReportKey = "";
			textBoxRatingRemarks.CustomReportValueType = 1;
			textBoxRatingRemarks.IsComboTextBox = false;
			textBoxRatingRemarks.IsModified = false;
			textBoxRatingRemarks.Location = new System.Drawing.Point(191, 190);
			textBoxRatingRemarks.MaxLength = 255;
			textBoxRatingRemarks.Multiline = true;
			textBoxRatingRemarks.Name = "textBoxRatingRemarks";
			textBoxRatingRemarks.Size = new System.Drawing.Size(708, 20);
			textBoxRatingRemarks.TabIndex = 12;
			comboBoxCreditReviewBy.Assigned = false;
			comboBoxCreditReviewBy.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCreditReviewBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCreditReviewBy.CustomReportFieldName = "";
			comboBoxCreditReviewBy.CustomReportKey = "";
			comboBoxCreditReviewBy.CustomReportValueType = 1;
			comboBoxCreditReviewBy.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCreditReviewBy.DisplayLayout.Appearance = appearance;
			comboBoxCreditReviewBy.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCreditReviewBy.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCreditReviewBy.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCreditReviewBy.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxCreditReviewBy.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCreditReviewBy.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxCreditReviewBy.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCreditReviewBy.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCreditReviewBy.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCreditReviewBy.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxCreditReviewBy.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCreditReviewBy.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCreditReviewBy.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCreditReviewBy.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxCreditReviewBy.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCreditReviewBy.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCreditReviewBy.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxCreditReviewBy.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxCreditReviewBy.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCreditReviewBy.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxCreditReviewBy.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxCreditReviewBy.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCreditReviewBy.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxCreditReviewBy.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCreditReviewBy.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCreditReviewBy.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCreditReviewBy.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCreditReviewBy.Editable = true;
			comboBoxCreditReviewBy.FilterString = "";
			comboBoxCreditReviewBy.HasAllAccount = false;
			comboBoxCreditReviewBy.HasCustom = false;
			comboBoxCreditReviewBy.IsDataLoaded = false;
			comboBoxCreditReviewBy.Location = new System.Drawing.Point(630, 94);
			comboBoxCreditReviewBy.MaxDropDownItems = 12;
			comboBoxCreditReviewBy.Name = "comboBoxCreditReviewBy";
			comboBoxCreditReviewBy.ShowInactiveItems = false;
			comboBoxCreditReviewBy.ShowQuickAdd = true;
			comboBoxCreditReviewBy.Size = new System.Drawing.Size(269, 20);
			comboBoxCreditReviewBy.TabIndex = 5;
			comboBoxCreditReviewBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxRatingBy.Assigned = false;
			comboBoxRatingBy.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRatingBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRatingBy.CustomReportFieldName = "";
			comboBoxRatingBy.CustomReportKey = "";
			comboBoxRatingBy.CustomReportValueType = 1;
			comboBoxRatingBy.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRatingBy.DisplayLayout.Appearance = appearance13;
			comboBoxRatingBy.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRatingBy.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRatingBy.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRatingBy.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxRatingBy.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRatingBy.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxRatingBy.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRatingBy.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRatingBy.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRatingBy.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxRatingBy.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRatingBy.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRatingBy.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRatingBy.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxRatingBy.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRatingBy.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRatingBy.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxRatingBy.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxRatingBy.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRatingBy.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxRatingBy.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxRatingBy.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRatingBy.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxRatingBy.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRatingBy.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRatingBy.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRatingBy.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRatingBy.Editable = true;
			comboBoxRatingBy.FilterString = "";
			comboBoxRatingBy.HasAllAccount = false;
			comboBoxRatingBy.HasCustom = false;
			comboBoxRatingBy.IsDataLoaded = false;
			comboBoxRatingBy.Location = new System.Drawing.Point(752, 142);
			comboBoxRatingBy.MaxDropDownItems = 12;
			comboBoxRatingBy.Name = "comboBoxRatingBy";
			comboBoxRatingBy.ShowInactiveItems = false;
			comboBoxRatingBy.ShowQuickAdd = true;
			comboBoxRatingBy.Size = new System.Drawing.Size(147, 20);
			comboBoxRatingBy.TabIndex = 9;
			comboBoxRatingBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerRatingDate.Checked = false;
			dateTimePickerRatingDate.CustomFormat = " ";
			dateTimePickerRatingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerRatingDate.Location = new System.Drawing.Point(191, 142);
			dateTimePickerRatingDate.Name = "dateTimePickerRatingDate";
			dateTimePickerRatingDate.ShowCheckBox = true;
			dateTimePickerRatingDate.Size = new System.Drawing.Size(74, 20);
			dateTimePickerRatingDate.TabIndex = 8;
			dateTimePickerRatingDate.Value = new System.DateTime(0L);
			buttonCustomerInsuranceClaim.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCustomerInsuranceClaim.BackColor = System.Drawing.Color.DarkGray;
			buttonCustomerInsuranceClaim.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCustomerInsuranceClaim.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCustomerInsuranceClaim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCustomerInsuranceClaim.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCustomerInsuranceClaim.Location = new System.Drawing.Point(452, 426);
			buttonCustomerInsuranceClaim.Name = "buttonCustomerInsuranceClaim";
			buttonCustomerInsuranceClaim.Size = new System.Drawing.Size(208, 21);
			buttonCustomerInsuranceClaim.TabIndex = 46;
			buttonCustomerInsuranceClaim.Text = "Customer Insurance Claim";
			buttonCustomerInsuranceClaim.UseVisualStyleBackColor = false;
			buttonCustomerInsuranceClaim.Visible = false;
			comboBoxInsuranceProvider.Assigned = false;
			comboBoxInsuranceProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInsuranceProvider.CustomReportFieldName = "";
			comboBoxInsuranceProvider.CustomReportKey = "";
			comboBoxInsuranceProvider.CustomReportValueType = 1;
			comboBoxInsuranceProvider.DescriptionTextBox = textBoxProvider;
			comboBoxInsuranceProvider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInsuranceProvider.Editable = true;
			comboBoxInsuranceProvider.FilterString = "";
			comboBoxInsuranceProvider.HasAllAccount = false;
			comboBoxInsuranceProvider.HasCustom = false;
			comboBoxInsuranceProvider.IsDataLoaded = false;
			comboBoxInsuranceProvider.Location = new System.Drawing.Point(194, 451);
			comboBoxInsuranceProvider.MaxDropDownItems = 12;
			comboBoxInsuranceProvider.Name = "comboBoxInsuranceProvider";
			comboBoxInsuranceProvider.ShowInactiveItems = false;
			comboBoxInsuranceProvider.ShowQuickAdd = true;
			comboBoxInsuranceProvider.Size = new System.Drawing.Size(254, 20);
			comboBoxInsuranceProvider.TabIndex = 1;
			comboBoxInsuranceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProvider.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProvider.CustomReportFieldName = "";
			textBoxProvider.CustomReportKey = "";
			textBoxProvider.CustomReportValueType = 1;
			textBoxProvider.Enabled = false;
			textBoxProvider.IsComboTextBox = false;
			textBoxProvider.IsModified = false;
			textBoxProvider.Location = new System.Drawing.Point(452, 451);
			textBoxProvider.MaxLength = 64;
			textBoxProvider.Name = "textBoxProvider";
			textBoxProvider.ReadOnly = true;
			textBoxProvider.Size = new System.Drawing.Size(459, 20);
			textBoxProvider.TabIndex = 2;
			textBoxProvider.TabStop = false;
			dateTimePickerValidTo.Checked = false;
			dateTimePickerValidTo.CustomFormat = " ";
			dateTimePickerValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerValidTo.Location = new System.Drawing.Point(559, 547);
			dateTimePickerValidTo.Name = "dateTimePickerValidTo";
			dateTimePickerValidTo.ShowCheckBox = true;
			dateTimePickerValidTo.Size = new System.Drawing.Size(333, 20);
			dateTimePickerValidTo.TabIndex = 7;
			dateTimePickerValidTo.Value = new System.DateTime(0L);
			datetimePickerEffectiveDate.Checked = false;
			datetimePickerEffectiveDate.CustomFormat = " ";
			datetimePickerEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerEffectiveDate.Location = new System.Drawing.Point(194, 547);
			datetimePickerEffectiveDate.Name = "datetimePickerEffectiveDate";
			datetimePickerEffectiveDate.ShowCheckBox = true;
			datetimePickerEffectiveDate.Size = new System.Drawing.Size(194, 20);
			datetimePickerEffectiveDate.TabIndex = 6;
			datetimePickerEffectiveDate.Value = new System.DateTime(0L);
			textBoxInsuranceID.BackColor = System.Drawing.Color.White;
			textBoxInsuranceID.CustomReportFieldName = "";
			textBoxInsuranceID.CustomReportKey = "";
			textBoxInsuranceID.CustomReportValueType = 1;
			textBoxInsuranceID.IsComboTextBox = false;
			textBoxInsuranceID.IsModified = false;
			textBoxInsuranceID.Location = new System.Drawing.Point(559, 523);
			textBoxInsuranceID.MaxLength = 30;
			textBoxInsuranceID.Name = "textBoxInsuranceID";
			textBoxInsuranceID.Size = new System.Drawing.Size(333, 20);
			textBoxInsuranceID.TabIndex = 5;
			comboBoxInsuranceRating.AutoSize = false;
			comboBoxInsuranceRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			valueListItem.DataValue = (byte)0;
			valueListItem.DisplayText = "N/A";
			valueListItem2.DataValue = (byte)1;
			valueListItem2.DisplayText = "1";
			valueListItem3.DataValue = "2";
			valueListItem3.DisplayText = "2";
			valueListItem4.DataValue = (byte)3;
			valueListItem4.DisplayText = "3";
			valueListItem5.DataValue = "4";
			valueListItem5.DisplayText = "4";
			valueListItem6.DataValue = (byte)5;
			valueListItem6.DisplayText = "5";
			valueListItem7.DataValue = (byte)6;
			valueListItem7.DisplayText = "6";
			valueListItem8.DataValue = "7";
			valueListItem8.DisplayText = "7";
			valueListItem9.DataValue = "8";
			valueListItem9.DisplayText = "8";
			valueListItem10.DataValue = "9";
			valueListItem10.DisplayText = "9";
			valueListItem11.DataValue = "10";
			valueListItem11.DisplayText = "10";
			comboBoxInsuranceRating.Items.AddRange(new Infragistics.Win.ValueListItem[11]
			{
				valueListItem,
				valueListItem2,
				valueListItem3,
				valueListItem4,
				valueListItem5,
				valueListItem6,
				valueListItem7,
				valueListItem8,
				valueListItem9,
				valueListItem10,
				valueListItem11
			});
			comboBoxInsuranceRating.Location = new System.Drawing.Point(194, 523);
			comboBoxInsuranceRating.Name = "comboBoxInsuranceRating";
			comboBoxInsuranceRating.Size = new System.Drawing.Size(194, 20);
			comboBoxInsuranceRating.TabIndex = 4;
			textBoxInsuranceRemarks.BackColor = System.Drawing.Color.White;
			textBoxInsuranceRemarks.CustomReportFieldName = "";
			textBoxInsuranceRemarks.CustomReportKey = "";
			textBoxInsuranceRemarks.CustomReportValueType = 1;
			textBoxInsuranceRemarks.IsComboTextBox = false;
			textBoxInsuranceRemarks.IsModified = false;
			textBoxInsuranceRemarks.Location = new System.Drawing.Point(194, 571);
			textBoxInsuranceRemarks.MaxLength = 255;
			textBoxInsuranceRemarks.Multiline = true;
			textBoxInsuranceRemarks.Name = "textBoxInsuranceRemarks";
			textBoxInsuranceRemarks.Size = new System.Drawing.Size(698, 42);
			textBoxInsuranceRemarks.TabIndex = 8;
			textBoxInsuranceNumber.BackColor = System.Drawing.Color.White;
			textBoxInsuranceNumber.CustomReportFieldName = "";
			textBoxInsuranceNumber.CustomReportKey = "";
			textBoxInsuranceNumber.CustomReportValueType = 1;
			textBoxInsuranceNumber.IsComboTextBox = false;
			textBoxInsuranceNumber.IsModified = false;
			textBoxInsuranceNumber.Location = new System.Drawing.Point(559, 475);
			textBoxInsuranceNumber.MaxLength = 30;
			textBoxInsuranceNumber.Name = "textBoxInsuranceNumber";
			textBoxInsuranceNumber.Size = new System.Drawing.Size(171, 20);
			textBoxInsuranceNumber.TabIndex = 1;
			textBoxInsuranceApprovedAmount.AllowDecimal = true;
			textBoxInsuranceApprovedAmount.BackColor = System.Drawing.Color.White;
			textBoxInsuranceApprovedAmount.CustomReportFieldName = "";
			textBoxInsuranceApprovedAmount.CustomReportKey = "";
			textBoxInsuranceApprovedAmount.CustomReportValueType = 1;
			textBoxInsuranceApprovedAmount.IsComboTextBox = false;
			textBoxInsuranceApprovedAmount.IsModified = false;
			textBoxInsuranceApprovedAmount.Location = new System.Drawing.Point(559, 499);
			textBoxInsuranceApprovedAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsuranceApprovedAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsuranceApprovedAmount.Name = "textBoxInsuranceApprovedAmount";
			textBoxInsuranceApprovedAmount.NullText = "0";
			textBoxInsuranceApprovedAmount.Size = new System.Drawing.Size(171, 20);
			textBoxInsuranceApprovedAmount.TabIndex = 3;
			textBoxInsuranceApprovedAmount.Text = "0.00";
			textBoxInsuranceApprovedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceApprovedAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxInsuranceReqAmount.AllowDecimal = true;
			textBoxInsuranceReqAmount.BackColor = System.Drawing.Color.White;
			textBoxInsuranceReqAmount.CustomReportFieldName = "";
			textBoxInsuranceReqAmount.CustomReportKey = "";
			textBoxInsuranceReqAmount.CustomReportValueType = 1;
			textBoxInsuranceReqAmount.IsComboTextBox = false;
			textBoxInsuranceReqAmount.IsModified = false;
			textBoxInsuranceReqAmount.Location = new System.Drawing.Point(194, 499);
			textBoxInsuranceReqAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsuranceReqAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsuranceReqAmount.Name = "textBoxInsuranceReqAmount";
			textBoxInsuranceReqAmount.NullText = "0";
			textBoxInsuranceReqAmount.Size = new System.Drawing.Size(194, 20);
			textBoxInsuranceReqAmount.TabIndex = 2;
			textBoxInsuranceReqAmount.Text = "0.00";
			textBoxInsuranceReqAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceReqAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			dateTimePickerInsuranceDate.Checked = false;
			dateTimePickerInsuranceDate.CustomFormat = " ";
			dateTimePickerInsuranceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerInsuranceDate.Location = new System.Drawing.Point(194, 475);
			dateTimePickerInsuranceDate.Name = "dateTimePickerInsuranceDate";
			dateTimePickerInsuranceDate.ShowCheckBox = true;
			dateTimePickerInsuranceDate.Size = new System.Drawing.Size(194, 20);
			dateTimePickerInsuranceDate.TabIndex = 0;
			dateTimePickerInsuranceDate.Value = new System.DateTime(0L);
			comboBoxInsuranceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxInsuranceStatus.FormattingEnabled = true;
			comboBoxInsuranceStatus.Items.AddRange(new object[7]
			{
				"Not Insured",
				"Under Process",
				"Insured",
				"Insured-Sublimit of Parent",
				"Rejected",
				"On Hold",
				"Cancelled"
			});
			comboBoxInsuranceStatus.Location = new System.Drawing.Point(194, 426);
			comboBoxInsuranceStatus.Name = "comboBoxInsuranceStatus";
			comboBoxInsuranceStatus.Size = new System.Drawing.Size(254, 21);
			comboBoxInsuranceStatus.TabIndex = 0;
			textBoxPaymentTermName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPaymentTermName.CustomReportFieldName = "";
			textBoxPaymentTermName.CustomReportKey = "";
			textBoxPaymentTermName.CustomReportValueType = 1;
			textBoxPaymentTermName.IsComboTextBox = false;
			textBoxPaymentTermName.IsModified = false;
			textBoxPaymentTermName.Location = new System.Drawing.Point(463, 70);
			textBoxPaymentTermName.MaxLength = 30;
			textBoxPaymentTermName.Name = "textBoxPaymentTermName";
			textBoxPaymentTermName.ReadOnly = true;
			textBoxPaymentTermName.Size = new System.Drawing.Size(436, 20);
			textBoxPaymentTermName.TabIndex = 3;
			textBoxPaymentTermName.TabStop = false;
			textBoxPaymentMethodName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPaymentMethodName.CustomReportFieldName = "";
			textBoxPaymentMethodName.CustomReportKey = "";
			textBoxPaymentMethodName.CustomReportValueType = 1;
			textBoxPaymentMethodName.IsComboTextBox = false;
			textBoxPaymentMethodName.IsModified = false;
			textBoxPaymentMethodName.Location = new System.Drawing.Point(463, 46);
			textBoxPaymentMethodName.MaxLength = 30;
			textBoxPaymentMethodName.Name = "textBoxPaymentMethodName";
			textBoxPaymentMethodName.ReadOnly = true;
			textBoxPaymentMethodName.Size = new System.Drawing.Size(436, 20);
			textBoxPaymentMethodName.TabIndex = 1;
			textBoxPaymentMethodName.TabStop = false;
			comboBoxRating.AutoSize = false;
			comboBoxRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			valueListItem12.DataValue = (byte)0;
			valueListItem12.DisplayText = "N/A";
			valueListItem13.DataValue = (byte)1;
			valueListItem13.DisplayText = "1";
			valueListItem14.DataValue = "2";
			valueListItem14.DisplayText = "2";
			valueListItem15.DataValue = (byte)3;
			valueListItem15.DisplayText = "3";
			valueListItem16.DataValue = "4";
			valueListItem16.DisplayText = "4";
			valueListItem17.DataValue = (byte)5;
			valueListItem17.DisplayText = "5";
			valueListItem18.DataValue = (byte)6;
			valueListItem18.DisplayText = "6";
			valueListItem19.DataValue = "7";
			valueListItem19.DisplayText = "7";
			valueListItem20.DataValue = "8";
			valueListItem20.DisplayText = "8";
			valueListItem21.DataValue = "9";
			valueListItem21.DisplayText = "9";
			valueListItem22.DataValue = "10";
			valueListItem22.DisplayText = "10";
			comboBoxRating.Items.AddRange(new Infragistics.Win.ValueListItem[11]
			{
				valueListItem12,
				valueListItem13,
				valueListItem14,
				valueListItem15,
				valueListItem16,
				valueListItem17,
				valueListItem18,
				valueListItem19,
				valueListItem20,
				valueListItem21,
				valueListItem22
			});
			comboBoxRating.Location = new System.Drawing.Point(436, 142);
			comboBoxRating.Name = "comboBoxRating";
			comboBoxRating.Size = new System.Drawing.Size(145, 20);
			comboBoxRating.TabIndex = 7;
			comboBoxRating.ValueChanged += new System.EventHandler(comboBoxRating_ValueChanged);
			comboBoxPaymentTerms.Assigned = false;
			comboBoxPaymentTerms.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPaymentTerms.CustomReportFieldName = "";
			comboBoxPaymentTerms.CustomReportKey = "";
			comboBoxPaymentTerms.CustomReportValueType = 1;
			comboBoxPaymentTerms.DescriptionTextBox = textBoxPaymentTermName;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPaymentTerms.DisplayLayout.Appearance = appearance25;
			comboBoxPaymentTerms.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPaymentTerms.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxPaymentTerms.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPaymentTerms.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPaymentTerms.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPaymentTerms.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPaymentTerms.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxPaymentTerms.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPaymentTerms.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxPaymentTerms.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxPaymentTerms.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPaymentTerms.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxPaymentTerms.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPaymentTerms.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPaymentTerms.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPaymentTerms.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPaymentTerms.Editable = true;
			comboBoxPaymentTerms.FilterString = "";
			comboBoxPaymentTerms.HasAllAccount = false;
			comboBoxPaymentTerms.HasCustom = false;
			comboBoxPaymentTerms.IsDataLoaded = false;
			comboBoxPaymentTerms.Location = new System.Drawing.Point(191, 70);
			comboBoxPaymentTerms.MaxDropDownItems = 12;
			comboBoxPaymentTerms.MaxLength = 15;
			comboBoxPaymentTerms.Name = "comboBoxPaymentTerms";
			comboBoxPaymentTerms.ShowInactiveItems = false;
			comboBoxPaymentTerms.ShowQuickAdd = true;
			comboBoxPaymentTerms.Size = new System.Drawing.Size(268, 20);
			comboBoxPaymentTerms.TabIndex = 2;
			comboBoxPaymentTerms.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLoadImage.Location = new System.Drawing.Point(298, 362);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(613, 20);
			linkLoadImage.TabIndex = 89;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Sign";
			appearance37.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance37;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			linkRemovePicture.Location = new System.Drawing.Point(298, 338);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(613, 20);
			linkRemovePicture.TabIndex = 86;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance38.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance38;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.Location = new System.Drawing.Point(298, 314);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(613, 20);
			linkAddPicture.TabIndex = 85;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance39;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			textBoxGraceDays.AllowDecimal = false;
			textBoxGraceDays.CustomReportFieldName = "";
			textBoxGraceDays.CustomReportKey = "";
			textBoxGraceDays.CustomReportValueType = 1;
			textBoxGraceDays.IsComboTextBox = false;
			textBoxGraceDays.IsModified = false;
			textBoxGraceDays.Location = new System.Drawing.Point(691, 290);
			textBoxGraceDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxGraceDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxGraceDays.Name = "textBoxGraceDays";
			textBoxGraceDays.NullText = "0";
			textBoxGraceDays.Size = new System.Drawing.Size(205, 20);
			textBoxGraceDays.TabIndex = 81;
			textBoxGraceDays.Text = "0";
			dateTimePickerCLValidity.Checked = false;
			dateTimePickerCLValidity.CustomFormat = " ";
			dateTimePickerCLValidity.Enabled = false;
			dateTimePickerCLValidity.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerCLValidity.Location = new System.Drawing.Point(194, 266);
			dateTimePickerCLValidity.Name = "dateTimePickerCLValidity";
			dateTimePickerCLValidity.ShowCheckBox = true;
			dateTimePickerCLValidity.Size = new System.Drawing.Size(148, 20);
			dateTimePickerCLValidity.TabIndex = 5;
			dateTimePickerCLValidity.Value = new System.DateTime(0L);
			textBoxUnsecuredLimit.AllowDecimal = true;
			textBoxUnsecuredLimit.BackColor = System.Drawing.Color.White;
			textBoxUnsecuredLimit.CustomReportFieldName = "";
			textBoxUnsecuredLimit.CustomReportKey = "";
			textBoxUnsecuredLimit.CustomReportValueType = 1;
			textBoxUnsecuredLimit.Enabled = false;
			textBoxUnsecuredLimit.IsComboTextBox = false;
			textBoxUnsecuredLimit.IsModified = false;
			textBoxUnsecuredLimit.Location = new System.Drawing.Point(524, 266);
			textBoxUnsecuredLimit.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxUnsecuredLimit.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxUnsecuredLimit.Name = "textBoxUnsecuredLimit";
			textBoxUnsecuredLimit.NullText = "0";
			textBoxUnsecuredLimit.Size = new System.Drawing.Size(122, 20);
			textBoxUnsecuredLimit.TabIndex = 7;
			textBoxUnsecuredLimit.Text = "0.00";
			textBoxUnsecuredLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxUnsecuredLimit.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			checkBoxUnsecuredLimit.Enabled = false;
			checkBoxUnsecuredLimit.Location = new System.Drawing.Point(346, 266);
			checkBoxUnsecuredLimit.Name = "checkBoxUnsecuredLimit";
			checkBoxUnsecuredLimit.Size = new System.Drawing.Size(174, 20);
			checkBoxUnsecuredLimit.TabIndex = 6;
			checkBoxUnsecuredLimit.Text = "Limit PDC Unsecured to:";
			checkBoxUnsecuredLimit.UseVisualStyleBackColor = true;
			checkBoxUnsecuredLimit.CheckedChanged += new System.EventHandler(checkBoxUnsecuredLimit_CheckedChanged);
			textBoxTempLimit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTempLimit.CustomReportFieldName = "";
			textBoxTempLimit.CustomReportKey = "";
			textBoxTempLimit.CustomReportValueType = 1;
			textBoxTempLimit.IsComboTextBox = false;
			textBoxTempLimit.IsModified = false;
			textBoxTempLimit.Location = new System.Drawing.Point(817, 266);
			textBoxTempLimit.MaxLength = 30;
			textBoxTempLimit.Name = "textBoxTempLimit";
			textBoxTempLimit.ReadOnly = true;
			textBoxTempLimit.Size = new System.Drawing.Size(79, 20);
			textBoxTempLimit.TabIndex = 8;
			textBoxTempLimit.TabStop = false;
			textBoxTempLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptCheque.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptCheque.Location = new System.Drawing.Point(27, 290);
			checkBoxAcceptCheque.Name = "checkBoxAcceptCheque";
			checkBoxAcceptCheque.Size = new System.Drawing.Size(194, 20);
			checkBoxAcceptCheque.TabIndex = 9;
			checkBoxAcceptCheque.Text = "Accept cheque payment";
			checkBoxAcceptCheque.UseVisualStyleBackColor = true;
			radioButtonSublimit.Location = new System.Drawing.Point(344, 237);
			radioButtonSublimit.Name = "radioButtonSublimit";
			radioButtonSublimit.Size = new System.Drawing.Size(175, 25);
			radioButtonSublimit.TabIndex = 2;
			radioButtonSublimit.Text = "Sublimit of Parent";
			radioButtonSublimit.UseVisualStyleBackColor = true;
			checkBoxAcceptPDC.Checked = true;
			checkBoxAcceptPDC.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptPDC.Location = new System.Drawing.Point(225, 290);
			checkBoxAcceptPDC.Name = "checkBoxAcceptPDC";
			checkBoxAcceptPDC.Size = new System.Drawing.Size(295, 20);
			checkBoxAcceptPDC.TabIndex = 10;
			checkBoxAcceptPDC.Text = "Accept post-dated cheque payment";
			checkBoxAcceptPDC.UseVisualStyleBackColor = true;
			textBoxCreditLimit.AllowDecimal = true;
			textBoxCreditLimit.BackColor = System.Drawing.Color.White;
			textBoxCreditLimit.CustomReportFieldName = "";
			textBoxCreditLimit.CustomReportKey = "";
			textBoxCreditLimit.CustomReportValueType = 1;
			textBoxCreditLimit.IsComboTextBox = false;
			textBoxCreditLimit.IsModified = false;
			textBoxCreditLimit.Location = new System.Drawing.Point(668, 237);
			textBoxCreditLimit.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxCreditLimit.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxCreditLimit.Name = "textBoxCreditLimit";
			textBoxCreditLimit.NullText = "0";
			textBoxCreditLimit.Size = new System.Drawing.Size(228, 25);
			textBoxCreditLimit.TabIndex = 4;
			textBoxCreditLimit.Text = "0.00";
			textBoxCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxCreditLimit.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			radioButtonCreditLimitNoCredit.Checked = true;
			radioButtonCreditLimitNoCredit.Location = new System.Drawing.Point(187, 237);
			radioButtonCreditLimitNoCredit.Name = "radioButtonCreditLimitNoCredit";
			radioButtonCreditLimitNoCredit.Size = new System.Drawing.Size(153, 25);
			radioButtonCreditLimitNoCredit.TabIndex = 1;
			radioButtonCreditLimitNoCredit.TabStop = true;
			radioButtonCreditLimitNoCredit.Text = "No Credit";
			radioButtonCreditLimitNoCredit.UseVisualStyleBackColor = true;
			radioButtonCreditLimitUnlimited.Location = new System.Drawing.Point(27, 237);
			radioButtonCreditLimitUnlimited.Name = "radioButtonCreditLimitUnlimited";
			radioButtonCreditLimitUnlimited.Size = new System.Drawing.Size(156, 25);
			radioButtonCreditLimitUnlimited.TabIndex = 0;
			radioButtonCreditLimitUnlimited.Text = "Unlimited";
			radioButtonCreditLimitUnlimited.UseVisualStyleBackColor = true;
			radioButtonCreditLimitAmount.Location = new System.Drawing.Point(523, 237);
			radioButtonCreditLimitAmount.Name = "radioButtonCreditLimitAmount";
			radioButtonCreditLimitAmount.Size = new System.Drawing.Size(141, 25);
			radioButtonCreditLimitAmount.TabIndex = 3;
			radioButtonCreditLimitAmount.Text = "Amount of:";
			radioButtonCreditLimitAmount.UseVisualStyleBackColor = true;
			radioButtonCreditLimitAmount.CheckedChanged += new System.EventHandler(radioButtonCreditLimitAmount_CheckedChanged);
			comboBoxCollectionUser.Assigned = false;
			comboBoxCollectionUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCollectionUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCollectionUser.CustomReportFieldName = "";
			comboBoxCollectionUser.CustomReportKey = "";
			comboBoxCollectionUser.CustomReportValueType = 1;
			comboBoxCollectionUser.DescriptionTextBox = null;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCollectionUser.DisplayLayout.Appearance = appearance40;
			comboBoxCollectionUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCollectionUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.Appearance = appearance41;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance43.BackColor2 = System.Drawing.SystemColors.Control;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
			comboBoxCollectionUser.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCollectionUser.DisplayLayout.MaxRowScrollRegions = 1;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCollectionUser.DisplayLayout.Override.ActiveCellAppearance = appearance44;
			appearance45.BackColor = System.Drawing.SystemColors.Highlight;
			appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCollectionUser.DisplayLayout.Override.ActiveRowAppearance = appearance45;
			comboBoxCollectionUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCollectionUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCollectionUser.DisplayLayout.Override.CardAreaAppearance = appearance46;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCollectionUser.DisplayLayout.Override.CellAppearance = appearance47;
			comboBoxCollectionUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCollectionUser.DisplayLayout.Override.CellPadding = 0;
			appearance48.BackColor = System.Drawing.SystemColors.Control;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCollectionUser.DisplayLayout.Override.GroupByRowAppearance = appearance48;
			appearance49.TextHAlignAsString = "Left";
			comboBoxCollectionUser.DisplayLayout.Override.HeaderAppearance = appearance49;
			comboBoxCollectionUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCollectionUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			comboBoxCollectionUser.DisplayLayout.Override.RowAppearance = appearance50;
			comboBoxCollectionUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCollectionUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
			comboBoxCollectionUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCollectionUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCollectionUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCollectionUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCollectionUser.Editable = true;
			comboBoxCollectionUser.FilterString = "";
			comboBoxCollectionUser.HasAllAccount = false;
			comboBoxCollectionUser.HasCustom = false;
			comboBoxCollectionUser.IsDataLoaded = false;
			comboBoxCollectionUser.Location = new System.Drawing.Point(191, 118);
			comboBoxCollectionUser.MaxDropDownItems = 12;
			comboBoxCollectionUser.Name = "comboBoxCollectionUser";
			comboBoxCollectionUser.ShowInactiveItems = false;
			comboBoxCollectionUser.ShowQuickAdd = true;
			comboBoxCollectionUser.Size = new System.Drawing.Size(708, 20);
			comboBoxCollectionUser.TabIndex = 6;
			comboBoxCollectionUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerReviewDate.Checked = false;
			dateTimePickerReviewDate.CustomFormat = " ";
			dateTimePickerReviewDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerReviewDate.Location = new System.Drawing.Point(191, 94);
			dateTimePickerReviewDate.Name = "dateTimePickerReviewDate";
			dateTimePickerReviewDate.ShowCheckBox = true;
			dateTimePickerReviewDate.Size = new System.Drawing.Size(268, 20);
			dateTimePickerReviewDate.TabIndex = 4;
			dateTimePickerReviewDate.Value = new System.DateTime(0L);
			dataGridContacts.AllowAddNew = false;
			dataGridContacts.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			appearance52.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridContacts.DisplayLayout.Appearance = appearance52;
			dataGridContacts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridContacts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance53.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.GroupByBox.Appearance = appearance53;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance54;
			dataGridContacts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance55.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance55.BackColor2 = System.Drawing.SystemColors.Control;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.PromptAppearance = appearance55;
			dataGridContacts.DisplayLayout.MaxColScrollRegions = 1;
			dataGridContacts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridContacts.DisplayLayout.Override.ActiveCellAppearance = appearance56;
			appearance57.BackColor = System.Drawing.SystemColors.Highlight;
			appearance57.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridContacts.DisplayLayout.Override.ActiveRowAppearance = appearance57;
			dataGridContacts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridContacts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridContacts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.CardAreaAppearance = appearance58;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			appearance59.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridContacts.DisplayLayout.Override.CellAppearance = appearance59;
			dataGridContacts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridContacts.DisplayLayout.Override.CellPadding = 0;
			appearance60.BackColor = System.Drawing.SystemColors.Control;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.GroupByRowAppearance = appearance60;
			appearance61.TextHAlignAsString = "Left";
			dataGridContacts.DisplayLayout.Override.HeaderAppearance = appearance61;
			dataGridContacts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridContacts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			dataGridContacts.DisplayLayout.Override.RowAppearance = appearance62;
			dataGridContacts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance63.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridContacts.DisplayLayout.Override.TemplateAddRowAppearance = appearance63;
			dataGridContacts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridContacts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridContacts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridContacts.IncludeLotItems = false;
			dataGridContacts.LoadLayoutFailed = false;
			dataGridContacts.Location = new System.Drawing.Point(24, 62);
			dataGridContacts.Name = "dataGridContacts";
			dataGridContacts.ShowClearMenu = true;
			dataGridContacts.ShowDeleteMenu = true;
			dataGridContacts.ShowInsertMenu = true;
			dataGridContacts.ShowMoveRowsMenu = true;
			dataGridContacts.Size = new System.Drawing.Size(890, 279);
			dataGridContacts.TabIndex = 0;
			dataGridContacts.Text = "dataEntryGrid1";
			gridComboBoxContact.Assigned = false;
			gridComboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			gridComboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			gridComboBoxContact.CustomReportFieldName = "";
			gridComboBoxContact.CustomReportKey = "";
			gridComboBoxContact.CustomReportValueType = 1;
			gridComboBoxContact.DescriptionTextBox = null;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			appearance64.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			gridComboBoxContact.DisplayLayout.Appearance = appearance64;
			gridComboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			gridComboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance65.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance65;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance66;
			gridComboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance67.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance67.BackColor2 = System.Drawing.SystemColors.Control;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance67;
			gridComboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			gridComboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.ForeColor = System.Drawing.SystemColors.ControlText;
			gridComboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance68;
			appearance69.BackColor = System.Drawing.SystemColors.Highlight;
			appearance69.ForeColor = System.Drawing.SystemColors.HighlightText;
			gridComboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance69;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance70;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			appearance71.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			gridComboBoxContact.DisplayLayout.Override.CellAppearance = appearance71;
			gridComboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			gridComboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance72.BackColor = System.Drawing.SystemColors.Control;
			appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance72.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance72;
			appearance73.TextHAlignAsString = "Left";
			gridComboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance73;
			gridComboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			gridComboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			gridComboBoxContact.DisplayLayout.Override.RowAppearance = appearance74;
			gridComboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance75.BackColor = System.Drawing.SystemColors.ControlLight;
			gridComboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance75;
			gridComboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			gridComboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			gridComboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			gridComboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			gridComboBoxContact.Editable = true;
			gridComboBoxContact.FilterString = "";
			gridComboBoxContact.HasAllAccount = false;
			gridComboBoxContact.HasCustom = false;
			gridComboBoxContact.IsDataLoaded = false;
			gridComboBoxContact.Location = new System.Drawing.Point(24, 345);
			gridComboBoxContact.MaxDropDownItems = 12;
			gridComboBoxContact.Name = "gridComboBoxContact";
			gridComboBoxContact.ShowInactiveItems = false;
			gridComboBoxContact.ShowQuickAdd = true;
			gridComboBoxContact.Size = new System.Drawing.Size(890, 20);
			gridComboBoxContact.TabIndex = 356;
			gridComboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			gridComboBoxContact.Visible = false;
			comboBoxActivityPeriod.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxActivityPeriod.Location = new System.Drawing.Point(787, 46);
			comboBoxActivityPeriod.Name = "comboBoxActivityPeriod";
			comboBoxActivityPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxActivityPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxActivityPeriod.Size = new System.Drawing.Size(127, 20);
			comboBoxActivityPeriod.StyleController = layoutControl1;
			comboBoxActivityPeriod.TabIndex = 361;
			comboBoxActivityPeriod.SelectedIndexChanged += new System.EventHandler(comboBoxActivityPeriod_SelectedIndexChanged);
			layoutControl1.Controls.Add(textBoxAddressPrintFormat);
			layoutControl1.Controls.Add(udfEntryGrid);
			layoutControl1.Controls.Add(textBoxNote);
			layoutControl1.Controls.Add(textBoxProfileDetails);
			layoutControl1.Controls.Add(dataGridActivities);
			layoutControl1.Controls.Add(comboBoxActivityPeriod);
			layoutControl1.Controls.Add(buttonAddActivity);
			layoutControl1.Controls.Add(gridComboBoxContact);
			layoutControl1.Controls.Add(dataGridContacts);
			layoutControl1.Controls.Add(dateTimePickerValidTo);
			layoutControl1.Controls.Add(textBoxProvider);
			layoutControl1.Controls.Add(comboBoxInsuranceProvider);
			layoutControl1.Controls.Add(datetimePickerEffectiveDate);
			layoutControl1.Controls.Add(textBoxInsuranceRemarks);
			layoutControl1.Controls.Add(buttonCustomerInsuranceClaim);
			layoutControl1.Controls.Add(linkLoadImage);
			layoutControl1.Controls.Add(textBoxInsuranceID);
			layoutControl1.Controls.Add(textBoxConfirmationLevel);
			layoutControl1.Controls.Add(comboBoxInsuranceRating);
			layoutControl1.Controls.Add(comboBoxInsuranceStatus);
			layoutControl1.Controls.Add(linkRemovePicture);
			layoutControl1.Controls.Add(linkAddPicture);
			layoutControl1.Controls.Add(textBoxRatingRemarks);
			layoutControl1.Controls.Add(textBoxInsuranceApprovedAmount);
			layoutControl1.Controls.Add(textBoxInsuranceNumber);
			layoutControl1.Controls.Add(pictureBoxPhoto);
			layoutControl1.Controls.Add(textBoxInsuranceReqAmount);
			layoutControl1.Controls.Add(dateTimeBalanceConfirmationDate);
			layoutControl1.Controls.Add(textBoxConsignCommission);
			layoutControl1.Controls.Add(dateTimePickerInsuranceDate);
			layoutControl1.Controls.Add(textBoxDeliveryInstructions);
			layoutControl1.Controls.Add(textBoxGraceDays);
			layoutControl1.Controls.Add(textBoxAccountInstructions);
			layoutControl1.Controls.Add(dateTimePickerCLValidity);
			layoutControl1.Controls.Add(textBoxUnsecuredLimit);
			layoutControl1.Controls.Add(checkBoxAcceptPDC);
			layoutControl1.Controls.Add(checkBoxAcceptCheque);
			layoutControl1.Controls.Add(textBoxTempLimit);
			layoutControl1.Controls.Add(comboBoxRatingBy);
			layoutControl1.Controls.Add(checkBoxUnsecuredLimit);
			layoutControl1.Controls.Add(comboBoxCreditReviewBy);
			layoutControl1.Controls.Add(comboBoxTaxGroup);
			layoutControl1.Controls.Add(dateTimePickerRatingDate);
			layoutControl1.Controls.Add(buttonAccounts);
			layoutControl1.Controls.Add(textBoxTaxIDNumber);
			layoutControl1.Controls.Add(textBoxLongitude);
			layoutControl1.Controls.Add(radioButtonSublimit);
			layoutControl1.Controls.Add(textBoxBankBranch);
			layoutControl1.Controls.Add(comboBoxTaxOption);
			layoutControl1.Controls.Add(textBoxCreditLimit);
			layoutControl1.Controls.Add(comboBoxRating);
			layoutControl1.Controls.Add(radioButtonCreditLimitAmount);
			layoutControl1.Controls.Add(radioButtonCreditLimitNoCredit);
			layoutControl1.Controls.Add(textBoxBankName);
			layoutControl1.Controls.Add(radioButtonCreditLimitUnlimited);
			layoutControl1.Controls.Add(textBoxPaymentTermName);
			layoutControl1.Controls.Add(textBoxLicenseNumber);
			layoutControl1.Controls.Add(textBoxPaymentMethodName);
			layoutControl1.Controls.Add(comboBoxCollectionUser);
			layoutControl1.Controls.Add(textBoxBankAccountNumber);
			layoutControl1.Controls.Add(textBoxRebatePercent);
			layoutControl1.Controls.Add(comboBoxCustomerGroup);
			layoutControl1.Controls.Add(comboBoxPaymentTerms);
			layoutControl1.Controls.Add(ultraPictureBox1);
			layoutControl1.Controls.Add(dateTimePickerReviewDate);
			layoutControl1.Controls.Add(textBoxDiscountPercent);
			layoutControl1.Controls.Add(buttonCategories);
			layoutControl1.Controls.Add(comboBoxPaymentMethods);
			layoutControl1.Controls.Add(textBoxLatitude);
			layoutControl1.Controls.Add(buttonMoreAddress);
			layoutControl1.Controls.Add(comboBoxCurrency);
			layoutControl1.Controls.Add(checkBoxparentACforposting);
			layoutControl1.Controls.Add(dateTimePickerContractExpDate);
			layoutControl1.Controls.Add(dateTimePickerLicenseExpDate);
			layoutControl1.Controls.Add(comboBoxSalesperson);
			layoutControl1.Controls.Add(textBoxStatementEmail);
			layoutControl1.Controls.Add(comboBoxStatementMethod);
			layoutControl1.Controls.Add(textBoxComment);
			layoutControl1.Controls.Add(textBoxCode);
			layoutControl1.Controls.Add(checkBoxWeightInvoice);
			layoutControl1.Controls.Add(checkBoxAllowConsignment);
			layoutControl1.Controls.Add(textBoxDepartment);
			layoutControl1.Controls.Add(textBoxWebsite);
			layoutControl1.Controls.Add(textBoxName);
			layoutControl1.Controls.Add(comboBoxLeadSource);
			layoutControl1.Controls.Add(comboBoxPriceLevel);
			layoutControl1.Controls.Add(textBoxFormalName);
			layoutControl1.Controls.Add(comboBoxArea);
			layoutControl1.Controls.Add(textBoxForeignName);
			layoutControl1.Controls.Add(dateTimePickerEstablished);
			layoutControl1.Controls.Add(textBoxEmail);
			layoutControl1.Controls.Add(dateTimePickerCustomerSince);
			layoutControl1.Controls.Add(comboBoxCountry);
			layoutControl1.Controls.Add(textBoxMobile);
			layoutControl1.Controls.Add(comboBoxBilltoAddress);
			layoutControl1.Controls.Add(comboBoxParentCustomer);
			layoutControl1.Controls.Add(comboBoxShippingMethods);
			layoutControl1.Controls.Add(textBoxPostalCode);
			layoutControl1.Controls.Add(textBoxFax);
			layoutControl1.Controls.Add(checkBoxIsInactive);
			layoutControl1.Controls.Add(comboBoxShiptoAddress);
			layoutControl1.Controls.Add(checkBoxHold);
			layoutControl1.Controls.Add(textBoxPhone2);
			layoutControl1.Controls.Add(comboBoxCustomerClass);
			layoutControl1.Controls.Add(textBoxAddressID);
			layoutControl1.Controls.Add(textBoxPhone1);
			layoutControl1.Controls.Add(textBoxContactName);
			layoutControl1.Controls.Add(textBoxAddress1);
			layoutControl1.Controls.Add(textBoxAddress2);
			layoutControl1.Controls.Add(textBoxAddress3);
			layoutControl1.Controls.Add(textBoxCity);
			layoutControl1.Controls.Add(textBoxState);
			layoutControl1.Controls.Add(textBoxCountry);
			layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			layoutControl1.Location = new System.Drawing.Point(0, 61);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(486, 0, 880, 788);
			layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
			layoutControl1.Root = Root;
			layoutControl1.Size = new System.Drawing.Size(938, 648);
			layoutControl1.TabIndex = 308;
			layoutControl1.Text = "layoutControl1";
			textBoxAddressPrintFormat.BackColor = System.Drawing.Color.White;
			textBoxAddressPrintFormat.CustomReportFieldName = "";
			textBoxAddressPrintFormat.CustomReportKey = "";
			textBoxAddressPrintFormat.CustomReportValueType = 1;
			textBoxAddressPrintFormat.IsComboTextBox = false;
			textBoxAddressPrintFormat.IsModified = false;
			textBoxAddressPrintFormat.Location = new System.Drawing.Point(203, 525);
			textBoxAddressPrintFormat.MaxLength = 255;
			textBoxAddressPrintFormat.Multiline = true;
			textBoxAddressPrintFormat.Name = "textBoxAddressPrintFormat";
			textBoxAddressPrintFormat.Size = new System.Drawing.Size(252, 94);
			textBoxAddressPrintFormat.TabIndex = 364;
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(24, 46);
			udfEntryGrid.Margin = new System.Windows.Forms.Padding(4);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(890, 595);
			udfEntryGrid.TabIndex = 0;
			udfEntryGrid.TableName = "";
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(24, 46);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(890, 595);
			textBoxNote.TabIndex = 43;
			textBoxProfileDetails.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxProfileDetails.Location = new System.Drawing.Point(24, 62);
			textBoxProfileDetails.Name = "textBoxProfileDetails";
			textBoxProfileDetails.Size = new System.Drawing.Size(890, 579);
			textBoxProfileDetails.TabIndex = 19;
			dataGridActivities.AllowUnfittedView = false;
			dataGridActivities.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			appearance76.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridActivities.DisplayLayout.Appearance = appearance76;
			dataGridActivities.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridActivities.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance77.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.GroupByBox.Appearance = appearance77;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridActivities.DisplayLayout.GroupByBox.BandLabelAppearance = appearance78;
			dataGridActivities.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance79.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance79.BackColor2 = System.Drawing.SystemColors.Control;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance79.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridActivities.DisplayLayout.GroupByBox.PromptAppearance = appearance79;
			dataGridActivities.DisplayLayout.MaxColScrollRegions = 1;
			dataGridActivities.DisplayLayout.MaxRowScrollRegions = 1;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			appearance80.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridActivities.DisplayLayout.Override.ActiveCellAppearance = appearance80;
			appearance81.BackColor = System.Drawing.SystemColors.Highlight;
			appearance81.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridActivities.DisplayLayout.Override.ActiveRowAppearance = appearance81;
			dataGridActivities.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridActivities.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.Override.CardAreaAppearance = appearance82;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			appearance83.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridActivities.DisplayLayout.Override.CellAppearance = appearance83;
			dataGridActivities.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridActivities.DisplayLayout.Override.CellPadding = 0;
			appearance84.BackColor = System.Drawing.SystemColors.Control;
			appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance84.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.BorderColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.Override.GroupByRowAppearance = appearance84;
			appearance85.TextHAlignAsString = "Left";
			dataGridActivities.DisplayLayout.Override.HeaderAppearance = appearance85;
			dataGridActivities.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridActivities.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			dataGridActivities.DisplayLayout.Override.RowAppearance = appearance86;
			dataGridActivities.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance87.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridActivities.DisplayLayout.Override.TemplateAddRowAppearance = appearance87;
			dataGridActivities.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridActivities.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridActivities.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridActivities.LoadLayoutFailed = false;
			dataGridActivities.Location = new System.Drawing.Point(24, 70);
			dataGridActivities.Name = "dataGridActivities";
			dataGridActivities.ShowDeleteMenu = false;
			dataGridActivities.ShowMinusInRed = true;
			dataGridActivities.ShowNewMenu = false;
			dataGridActivities.Size = new System.Drawing.Size(890, 373);
			dataGridActivities.TabIndex = 360;
			dataGridActivities.Text = "dataGridList1";
			buttonAddActivity.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonAddActivity.Location = new System.Drawing.Point(24, 46);
			buttonAddActivity.Name = "buttonAddActivity";
			buttonAddActivity.Size = new System.Drawing.Size(294, 20);
			buttonAddActivity.TabIndex = 363;
			buttonAddActivity.UseVisualStyleBackColor = true;
			buttonAddActivity.Click += new System.EventHandler(buttonAddActivity_Click);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(194, 314);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(100, 68);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			pictureBoxPhoto.TabIndex = 84;
			pictureBoxPhoto.TabStop = false;
			textBoxConsignCommission.BackColor = System.Drawing.Color.White;
			textBoxConsignCommission.CustomReportFieldName = "";
			textBoxConsignCommission.CustomReportKey = "";
			textBoxConsignCommission.CustomReportValueType = 1;
			textBoxConsignCommission.IsComboTextBox = false;
			textBoxConsignCommission.IsModified = false;
			textBoxConsignCommission.Location = new System.Drawing.Point(574, 181);
			textBoxConsignCommission.Name = "textBoxConsignCommission";
			textBoxConsignCommission.Size = new System.Drawing.Size(118, 20);
			textBoxConsignCommission.TabIndex = 0;
			textBoxConsignCommission.Text = "0.00";
			textBoxConsignCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDeliveryInstructions.BackColor = System.Drawing.Color.White;
			textBoxDeliveryInstructions.CustomReportFieldName = "";
			textBoxDeliveryInstructions.CustomReportKey = "";
			textBoxDeliveryInstructions.CustomReportValueType = 1;
			textBoxDeliveryInstructions.IsComboTextBox = false;
			textBoxDeliveryInstructions.IsModified = false;
			textBoxDeliveryInstructions.Location = new System.Drawing.Point(194, 422);
			textBoxDeliveryInstructions.MaxLength = 500;
			textBoxDeliveryInstructions.Multiline = true;
			textBoxDeliveryInstructions.Name = "textBoxDeliveryInstructions";
			textBoxDeliveryInstructions.Size = new System.Drawing.Size(708, 64);
			textBoxDeliveryInstructions.TabIndex = 0;
			textBoxAccountInstructions.BackColor = System.Drawing.Color.White;
			textBoxAccountInstructions.CustomReportFieldName = "";
			textBoxAccountInstructions.CustomReportKey = "";
			textBoxAccountInstructions.CustomReportValueType = 1;
			textBoxAccountInstructions.IsComboTextBox = false;
			textBoxAccountInstructions.IsModified = false;
			textBoxAccountInstructions.Location = new System.Drawing.Point(194, 490);
			textBoxAccountInstructions.MaxLength = 500;
			textBoxAccountInstructions.Multiline = true;
			textBoxAccountInstructions.Name = "textBoxAccountInstructions";
			textBoxAccountInstructions.Size = new System.Drawing.Size(708, 63);
			textBoxAccountInstructions.TabIndex = 1;
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			appearance88.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance88;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance89.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance89;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance90;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance91.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance91.BackColor2 = System.Drawing.SystemColors.Control;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance91;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance92;
			appearance93.BackColor = System.Drawing.SystemColors.Highlight;
			appearance93.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance93;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance94;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			appearance95.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance95;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance96.BackColor = System.Drawing.SystemColors.Control;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance96;
			appearance97.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance97;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance98;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance99.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance99;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(653, 332);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(249, 20);
			comboBoxTaxGroup.TabIndex = 1;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonAccounts.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAccounts.BackColor = System.Drawing.Color.DarkGray;
			buttonAccounts.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAccounts.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAccounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAccounts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAccounts.Location = new System.Drawing.Point(702, 557);
			buttonAccounts.Name = "buttonAccounts";
			buttonAccounts.Size = new System.Drawing.Size(200, 39);
			buttonAccounts.TabIndex = 17;
			buttonAccounts.Text = "&Accounts...";
			buttonAccounts.UseVisualStyleBackColor = false;
			buttonAccounts.Click += new System.EventHandler(buttonAccounts_Click);
			textBoxTaxIDNumber.BackColor = System.Drawing.Color.White;
			textBoxTaxIDNumber.CustomReportFieldName = "";
			textBoxTaxIDNumber.CustomReportKey = "";
			textBoxTaxIDNumber.CustomReportValueType = 1;
			textBoxTaxIDNumber.IsComboTextBox = false;
			textBoxTaxIDNumber.IsModified = false;
			textBoxTaxIDNumber.Location = new System.Drawing.Point(653, 356);
			textBoxTaxIDNumber.MaxLength = 75;
			textBoxTaxIDNumber.Name = "textBoxTaxIDNumber";
			textBoxTaxIDNumber.Size = new System.Drawing.Size(249, 20);
			textBoxTaxIDNumber.TabIndex = 2;
			textBoxLongitude.BackColor = System.Drawing.Color.White;
			textBoxLongitude.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLongitude.CustomReportFieldName = "";
			textBoxLongitude.CustomReportKey = "";
			textBoxLongitude.CustomReportValueType = 1;
			textBoxLongitude.IsComboTextBox = false;
			textBoxLongitude.IsModified = false;
			textBoxLongitude.Location = new System.Drawing.Point(626, 477);
			textBoxLongitude.MaxLength = 64;
			textBoxLongitude.Name = "textBoxLongitude";
			textBoxLongitude.Size = new System.Drawing.Size(156, 20);
			textBoxLongitude.TabIndex = 42;
			textBoxLongitude.MouseClick += new System.Windows.Forms.MouseEventHandler(textBoxLongitude_MouseClick);
			textBoxLongitude.KeyDown += new System.Windows.Forms.KeyEventHandler(textBoxLongitude_KeyDown);
			textBoxLongitude.KeyUp += new System.Windows.Forms.KeyEventHandler(textBoxLongitude_KeyUp);
			textBoxLongitude.MouseLeave += new System.EventHandler(textBoxLongitude_MouseLeave);
			textBoxBankBranch.BackColor = System.Drawing.Color.White;
			textBoxBankBranch.CustomReportFieldName = "";
			textBoxBankBranch.CustomReportKey = "";
			textBoxBankBranch.CustomReportValueType = 1;
			textBoxBankBranch.IsComboTextBox = false;
			textBoxBankBranch.IsModified = false;
			textBoxBankBranch.Location = new System.Drawing.Point(194, 355);
			textBoxBankBranch.MaxLength = 30;
			textBoxBankBranch.Name = "textBoxBankBranch";
			textBoxBankBranch.Size = new System.Drawing.Size(264, 21);
			textBoxBankBranch.TabIndex = 2;
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[3]
			{
				"Based on Class",
				"Taxable",
				"NonTaxable"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(653, 307);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(249, 21);
			comboBoxTaxOption.TabIndex = 0;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			textBoxBankName.BackColor = System.Drawing.Color.White;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.IsModified = false;
			textBoxBankName.Location = new System.Drawing.Point(194, 307);
			textBoxBankName.MaxLength = 30;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.Size = new System.Drawing.Size(264, 20);
			textBoxBankName.TabIndex = 0;
			textBoxLicenseNumber.BackColor = System.Drawing.Color.White;
			textBoxLicenseNumber.CustomReportFieldName = "";
			textBoxLicenseNumber.CustomReportKey = "";
			textBoxLicenseNumber.CustomReportValueType = 1;
			textBoxLicenseNumber.IsComboTextBox = false;
			textBoxLicenseNumber.IsModified = false;
			textBoxLicenseNumber.Location = new System.Drawing.Point(191, 205);
			textBoxLicenseNumber.MaxLength = 30;
			textBoxLicenseNumber.Name = "textBoxLicenseNumber";
			textBoxLicenseNumber.Size = new System.Drawing.Size(212, 20);
			textBoxLicenseNumber.TabIndex = 11;
			textBoxBankAccountNumber.BackColor = System.Drawing.Color.White;
			textBoxBankAccountNumber.CustomReportFieldName = "";
			textBoxBankAccountNumber.CustomReportKey = "";
			textBoxBankAccountNumber.CustomReportValueType = 1;
			textBoxBankAccountNumber.IsComboTextBox = false;
			textBoxBankAccountNumber.IsModified = false;
			textBoxBankAccountNumber.Location = new System.Drawing.Point(194, 331);
			textBoxBankAccountNumber.MaxLength = 30;
			textBoxBankAccountNumber.Name = "textBoxBankAccountNumber";
			textBoxBankAccountNumber.Size = new System.Drawing.Size(264, 20);
			textBoxBankAccountNumber.TabIndex = 1;
			textBoxRebatePercent.BackColor = System.Drawing.Color.White;
			textBoxRebatePercent.CustomReportFieldName = "";
			textBoxRebatePercent.CustomReportKey = "";
			textBoxRebatePercent.CustomReportValueType = 1;
			textBoxRebatePercent.IsComboTextBox = false;
			textBoxRebatePercent.IsModified = false;
			textBoxRebatePercent.Location = new System.Drawing.Point(356, 253);
			textBoxRebatePercent.Name = "textBoxRebatePercent";
			textBoxRebatePercent.Size = new System.Drawing.Size(120, 20);
			textBoxRebatePercent.TabIndex = 15;
			textBoxRebatePercent.Text = "0.00";
			textBoxRebatePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxCustomerGroup.Assigned = false;
			comboBoxCustomerGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomerGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomerGroup.CustomReportFieldName = "";
			comboBoxCustomerGroup.CustomReportKey = "";
			comboBoxCustomerGroup.CustomReportValueType = 1;
			comboBoxCustomerGroup.DescriptionTextBox = null;
			appearance100.BackColor = System.Drawing.SystemColors.Window;
			appearance100.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomerGroup.DisplayLayout.Appearance = appearance100;
			comboBoxCustomerGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomerGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance101.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerGroup.DisplayLayout.GroupByBox.Appearance = appearance101;
			appearance102.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance102;
			comboBoxCustomerGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance103.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance103.BackColor2 = System.Drawing.SystemColors.Control;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance103;
			comboBoxCustomerGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomerGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			appearance104.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomerGroup.DisplayLayout.Override.ActiveCellAppearance = appearance104;
			appearance105.BackColor = System.Drawing.SystemColors.Highlight;
			appearance105.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomerGroup.DisplayLayout.Override.ActiveRowAppearance = appearance105;
			comboBoxCustomerGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomerGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerGroup.DisplayLayout.Override.CardAreaAppearance = appearance106;
			appearance107.BorderColor = System.Drawing.Color.Silver;
			appearance107.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomerGroup.DisplayLayout.Override.CellAppearance = appearance107;
			comboBoxCustomerGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomerGroup.DisplayLayout.Override.CellPadding = 0;
			appearance108.BackColor = System.Drawing.SystemColors.Control;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerGroup.DisplayLayout.Override.GroupByRowAppearance = appearance108;
			appearance109.TextHAlignAsString = "Left";
			comboBoxCustomerGroup.DisplayLayout.Override.HeaderAppearance = appearance109;
			comboBoxCustomerGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomerGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomerGroup.DisplayLayout.Override.RowAppearance = appearance110;
			comboBoxCustomerGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance111.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomerGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance111;
			comboBoxCustomerGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomerGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomerGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomerGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomerGroup.Editable = true;
			comboBoxCustomerGroup.FilterString = "";
			comboBoxCustomerGroup.HasAllAccount = false;
			comboBoxCustomerGroup.HasCustom = false;
			comboBoxCustomerGroup.IsDataLoaded = false;
			comboBoxCustomerGroup.Location = new System.Drawing.Point(630, 94);
			comboBoxCustomerGroup.MaxDropDownItems = 12;
			comboBoxCustomerGroup.Name = "comboBoxCustomerGroup";
			comboBoxCustomerGroup.ShowInactiveItems = false;
			comboBoxCustomerGroup.ShowQuickAdd = true;
			comboBoxCustomerGroup.Size = new System.Drawing.Size(284, 20);
			comboBoxCustomerGroup.TabIndex = 10;
			comboBoxCustomerGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
			ultraPictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			ultraPictureBox1.Image = resources.GetObject("ultraPictureBox1.Image");
			ultraPictureBox1.Location = new System.Drawing.Point(786, 453);
			ultraPictureBox1.Name = "ultraPictureBox1";
			ultraPictureBox1.Size = new System.Drawing.Size(116, 44);
			ultraPictureBox1.TabIndex = 35;
			ultraPictureBox1.Click += new System.EventHandler(ultraPictureBox1_Click);
			textBoxDiscountPercent.BackColor = System.Drawing.Color.White;
			textBoxDiscountPercent.CustomReportFieldName = "";
			textBoxDiscountPercent.CustomReportKey = "";
			textBoxDiscountPercent.CustomReportValueType = 1;
			textBoxDiscountPercent.IsComboTextBox = false;
			textBoxDiscountPercent.IsModified = false;
			textBoxDiscountPercent.Location = new System.Drawing.Point(191, 253);
			textBoxDiscountPercent.Name = "textBoxDiscountPercent";
			textBoxDiscountPercent.Size = new System.Drawing.Size(103, 20);
			textBoxDiscountPercent.TabIndex = 14;
			textBoxDiscountPercent.Text = "0.00";
			textBoxDiscountPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			buttonCategories.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCategories.BackColor = System.Drawing.Color.DarkGray;
			buttonCategories.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCategories.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCategories.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCategories.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCategories.Location = new System.Drawing.Point(743, 214);
			buttonCategories.Name = "buttonCategories";
			buttonCategories.Size = new System.Drawing.Size(171, 27);
			buttonCategories.TabIndex = 15;
			buttonCategories.Text = "Categories...";
			buttonCategories.UseVisualStyleBackColor = false;
			buttonCategories.Click += new System.EventHandler(buttonCategories_Click);
			comboBoxPaymentMethods.Assigned = false;
			comboBoxPaymentMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPaymentMethods.CustomReportFieldName = "";
			comboBoxPaymentMethods.CustomReportKey = "";
			comboBoxPaymentMethods.CustomReportValueType = 1;
			comboBoxPaymentMethods.DescriptionTextBox = textBoxPaymentMethodName;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			appearance112.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPaymentMethods.DisplayLayout.Appearance = appearance112;
			comboBoxPaymentMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPaymentMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance113.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.Appearance = appearance113;
			appearance114.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance114;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance115.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance115.BackColor2 = System.Drawing.SystemColors.Control;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance115;
			comboBoxPaymentMethods.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPaymentMethods.DisplayLayout.MaxRowScrollRegions = 1;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			appearance116.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPaymentMethods.DisplayLayout.Override.ActiveCellAppearance = appearance116;
			appearance117.BackColor = System.Drawing.SystemColors.Highlight;
			appearance117.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPaymentMethods.DisplayLayout.Override.ActiveRowAppearance = appearance117;
			comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance118.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.Override.CardAreaAppearance = appearance118;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			appearance119.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPaymentMethods.DisplayLayout.Override.CellAppearance = appearance119;
			comboBoxPaymentMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPaymentMethods.DisplayLayout.Override.CellPadding = 0;
			appearance120.BackColor = System.Drawing.SystemColors.Control;
			appearance120.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance120.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.Override.GroupByRowAppearance = appearance120;
			appearance121.TextHAlignAsString = "Left";
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderAppearance = appearance121;
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.BorderColor = System.Drawing.Color.Silver;
			comboBoxPaymentMethods.DisplayLayout.Override.RowAppearance = appearance122;
			comboBoxPaymentMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance123.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPaymentMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance123;
			comboBoxPaymentMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPaymentMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPaymentMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPaymentMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPaymentMethods.Editable = true;
			comboBoxPaymentMethods.FilterString = "";
			comboBoxPaymentMethods.IsDataLoaded = false;
			comboBoxPaymentMethods.Location = new System.Drawing.Point(191, 46);
			comboBoxPaymentMethods.MaxDropDownItems = 12;
			comboBoxPaymentMethods.MaxLength = 15;
			comboBoxPaymentMethods.Name = "comboBoxPaymentMethods";
			comboBoxPaymentMethods.Size = new System.Drawing.Size(268, 20);
			comboBoxPaymentMethods.TabIndex = 0;
			comboBoxPaymentMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxLatitude.BackColor = System.Drawing.Color.White;
			textBoxLatitude.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLatitude.CustomReportFieldName = "";
			textBoxLatitude.CustomReportKey = "";
			textBoxLatitude.CustomReportValueType = 1;
			textBoxLatitude.IsComboTextBox = false;
			textBoxLatitude.IsModified = false;
			textBoxLatitude.Location = new System.Drawing.Point(626, 453);
			textBoxLatitude.MaxLength = 64;
			textBoxLatitude.Name = "textBoxLatitude";
			textBoxLatitude.Size = new System.Drawing.Size(156, 20);
			textBoxLatitude.TabIndex = 41;
			textBoxLatitude.MouseClick += new System.Windows.Forms.MouseEventHandler(textBoxLatitude_MouseClick);
			textBoxLatitude.KeyDown += new System.Windows.Forms.KeyEventHandler(textBoxLatitude_KeyDown);
			textBoxLatitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxLatitude_KeyDown);
			textBoxLatitude.KeyUp += new System.Windows.Forms.KeyEventHandler(textBoxLatitude_KeyUp);
			textBoxLatitude.MouseLeave += new System.EventHandler(textBoxLatitude_MouseLeave);
			buttonMoreAddress.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonMoreAddress.BackColor = System.Drawing.Color.DarkGray;
			buttonMoreAddress.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonMoreAddress.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonMoreAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonMoreAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonMoreAddress.Location = new System.Drawing.Point(744, 501);
			buttonMoreAddress.Name = "buttonMoreAddress";
			buttonMoreAddress.Size = new System.Drawing.Size(158, 38);
			buttonMoreAddress.TabIndex = 34;
			buttonMoreAddress.Text = "More Addresses...";
			buttonMoreAddress.UseVisualStyleBackColor = false;
			buttonMoreAddress.Click += new System.EventHandler(buttonMoreAddress_Click);
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance124.BackColor = System.Drawing.SystemColors.Window;
			appearance124.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance124;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance125.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance125;
			appearance126.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance126;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance127.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance127.BackColor2 = System.Drawing.SystemColors.Control;
			appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance127.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance127;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			appearance128.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance128;
			appearance129.BackColor = System.Drawing.SystemColors.Highlight;
			appearance129.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance129;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance130;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			appearance131.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance131;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance132.BackColor = System.Drawing.SystemColors.Control;
			appearance132.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance132.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance132.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance132;
			appearance133.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance133;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance134;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance135.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance135;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(630, 190);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(284, 20);
			comboBoxCurrency.TabIndex = 14;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			checkBoxparentACforposting.BackColor = System.Drawing.Color.Transparent;
			checkBoxparentACforposting.Enabled = false;
			checkBoxparentACforposting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxparentACforposting.Location = new System.Drawing.Point(24, 166);
			checkBoxparentACforposting.Name = "checkBoxparentACforposting";
			checkBoxparentACforposting.Size = new System.Drawing.Size(435, 20);
			checkBoxparentACforposting.TabIndex = 6;
			checkBoxparentACforposting.Text = "Use parent Account for Finance posting";
			checkBoxparentACforposting.UseVisualStyleBackColor = false;
			dateTimePickerContractExpDate.Checked = false;
			dateTimePickerContractExpDate.CustomFormat = " ";
			dateTimePickerContractExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerContractExpDate.Location = new System.Drawing.Point(191, 229);
			dateTimePickerContractExpDate.Name = "dateTimePickerContractExpDate";
			dateTimePickerContractExpDate.ShowCheckBox = true;
			dateTimePickerContractExpDate.Size = new System.Drawing.Size(212, 20);
			dateTimePickerContractExpDate.TabIndex = 13;
			dateTimePickerContractExpDate.Value = new System.DateTime(0L);
			dateTimePickerLicenseExpDate.Checked = false;
			dateTimePickerLicenseExpDate.CustomFormat = " ";
			dateTimePickerLicenseExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerLicenseExpDate.Location = new System.Drawing.Point(574, 205);
			dateTimePickerLicenseExpDate.Name = "dateTimePickerLicenseExpDate";
			dateTimePickerLicenseExpDate.ShowCheckBox = true;
			dateTimePickerLicenseExpDate.Size = new System.Drawing.Size(340, 20);
			dateTimePickerLicenseExpDate.TabIndex = 12;
			dateTimePickerLicenseExpDate.Value = new System.DateTime(0L);
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			appearance136.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance136;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance137.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance137.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance137;
			appearance138.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance138;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance139.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance139.BackColor2 = System.Drawing.SystemColors.Control;
			appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance139.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance139;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance140;
			appearance141.BackColor = System.Drawing.SystemColors.Highlight;
			appearance141.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance141;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance142.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance142;
			appearance143.BorderColor = System.Drawing.Color.Silver;
			appearance143.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance143;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance144.BackColor = System.Drawing.SystemColors.Control;
			appearance144.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance144.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance144.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance144;
			appearance145.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance145;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance146.BackColor = System.Drawing.SystemColors.Window;
			appearance146.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance146;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance147.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance147;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(191, 46);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(227, 20);
			comboBoxSalesperson.TabIndex = 0;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxStatementEmail.BackColor = System.Drawing.Color.White;
			textBoxStatementEmail.CustomReportFieldName = "";
			textBoxStatementEmail.CustomReportKey = "";
			textBoxStatementEmail.CustomReportValueType = 1;
			textBoxStatementEmail.IsComboTextBox = false;
			textBoxStatementEmail.IsModified = false;
			textBoxStatementEmail.Location = new System.Drawing.Point(191, 143);
			textBoxStatementEmail.MaxLength = 255;
			textBoxStatementEmail.Name = "textBoxStatementEmail";
			textBoxStatementEmail.Size = new System.Drawing.Size(227, 20);
			textBoxStatementEmail.TabIndex = 8;
			comboBoxStatementMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatementMethod.FormattingEnabled = true;
			comboBoxStatementMethod.Items.AddRange(new object[6]
			{
				"None",
				"Email",
				"Fax",
				"Post",
				"Delivery",
				"Other"
			});
			comboBoxStatementMethod.Location = new System.Drawing.Point(589, 118);
			comboBoxStatementMethod.Name = "comboBoxStatementMethod";
			comboBoxStatementMethod.Size = new System.Drawing.Size(325, 21);
			comboBoxStatementMethod.TabIndex = 7;
			textBoxComment.BackColor = System.Drawing.Color.White;
			textBoxComment.CustomReportFieldName = "";
			textBoxComment.CustomReportKey = "";
			textBoxComment.CustomReportValueType = 1;
			textBoxComment.IsComboTextBox = false;
			textBoxComment.IsModified = false;
			textBoxComment.Location = new System.Drawing.Point(203, 501);
			textBoxComment.MaxLength = 255;
			textBoxComment.Name = "textBoxComment";
			textBoxComment.Size = new System.Drawing.Size(252, 20);
			textBoxComment.TabIndex = 33;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(191, 46);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(268, 20);
			textBoxCode.TabIndex = 0;
			checkBoxWeightInvoice.Location = new System.Drawing.Point(24, 181);
			checkBoxWeightInvoice.Name = "checkBoxWeightInvoice";
			checkBoxWeightInvoice.Size = new System.Drawing.Size(165, 20);
			checkBoxWeightInvoice.TabIndex = 9;
			checkBoxWeightInvoice.Text = "Invoice by item weight";
			checkBoxWeightInvoice.UseVisualStyleBackColor = true;
			checkBoxAllowConsignment.Location = new System.Drawing.Point(193, 181);
			checkBoxAllowConsignment.Name = "checkBoxAllowConsignment";
			checkBoxAllowConsignment.Size = new System.Drawing.Size(210, 20);
			checkBoxAllowConsignment.TabIndex = 10;
			checkBoxAllowConsignment.Text = "Allow consignment sales";
			checkBoxAllowConsignment.UseVisualStyleBackColor = true;
			textBoxDepartment.BackColor = System.Drawing.Color.White;
			textBoxDepartment.CustomReportFieldName = "";
			textBoxDepartment.CustomReportKey = "";
			textBoxDepartment.CustomReportValueType = 1;
			textBoxDepartment.IsComboTextBox = false;
			textBoxDepartment.IsModified = false;
			textBoxDepartment.Location = new System.Drawing.Point(626, 285);
			textBoxDepartment.MaxLength = 30;
			textBoxDepartment.Name = "textBoxDepartment";
			textBoxDepartment.Size = new System.Drawing.Size(276, 20);
			textBoxDepartment.TabIndex = 19;
			textBoxWebsite.BackColor = System.Drawing.Color.White;
			textBoxWebsite.CustomReportFieldName = "";
			textBoxWebsite.CustomReportKey = "";
			textBoxWebsite.CustomReportValueType = 1;
			textBoxWebsite.IsComboTextBox = false;
			textBoxWebsite.IsModified = false;
			textBoxWebsite.Location = new System.Drawing.Point(626, 429);
			textBoxWebsite.MaxLength = 255;
			textBoxWebsite.Name = "textBoxWebsite";
			textBoxWebsite.Size = new System.Drawing.Size(276, 20);
			textBoxWebsite.TabIndex = 31;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.IsRequired = true;
			textBoxName.Location = new System.Drawing.Point(191, 70);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(268, 20);
			textBoxName.TabIndex = 1;
			comboBoxLeadSource.Assigned = false;
			comboBoxLeadSource.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLeadSource.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeadSource.CustomReportFieldName = "";
			comboBoxLeadSource.CustomReportKey = "";
			comboBoxLeadSource.CustomReportValueType = 1;
			comboBoxLeadSource.DescriptionTextBox = null;
			appearance148.BackColor = System.Drawing.SystemColors.Window;
			appearance148.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLeadSource.DisplayLayout.Appearance = appearance148;
			comboBoxLeadSource.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLeadSource.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance149.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance149.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance149.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadSource.DisplayLayout.GroupByBox.Appearance = appearance149;
			appearance150.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadSource.DisplayLayout.GroupByBox.BandLabelAppearance = appearance150;
			comboBoxLeadSource.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance151.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance151.BackColor2 = System.Drawing.SystemColors.Control;
			appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance151.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadSource.DisplayLayout.GroupByBox.PromptAppearance = appearance151;
			comboBoxLeadSource.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLeadSource.DisplayLayout.MaxRowScrollRegions = 1;
			appearance152.BackColor = System.Drawing.SystemColors.Window;
			appearance152.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLeadSource.DisplayLayout.Override.ActiveCellAppearance = appearance152;
			appearance153.BackColor = System.Drawing.SystemColors.Highlight;
			appearance153.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLeadSource.DisplayLayout.Override.ActiveRowAppearance = appearance153;
			comboBoxLeadSource.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLeadSource.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance154.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLeadSource.DisplayLayout.Override.CardAreaAppearance = appearance154;
			appearance155.BorderColor = System.Drawing.Color.Silver;
			appearance155.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLeadSource.DisplayLayout.Override.CellAppearance = appearance155;
			comboBoxLeadSource.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLeadSource.DisplayLayout.Override.CellPadding = 0;
			appearance156.BackColor = System.Drawing.SystemColors.Control;
			appearance156.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance156.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance156.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadSource.DisplayLayout.Override.GroupByRowAppearance = appearance156;
			appearance157.TextHAlignAsString = "Left";
			comboBoxLeadSource.DisplayLayout.Override.HeaderAppearance = appearance157;
			comboBoxLeadSource.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLeadSource.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance158.BackColor = System.Drawing.SystemColors.Window;
			appearance158.BorderColor = System.Drawing.Color.Silver;
			comboBoxLeadSource.DisplayLayout.Override.RowAppearance = appearance158;
			comboBoxLeadSource.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance159.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLeadSource.DisplayLayout.Override.TemplateAddRowAppearance = appearance159;
			comboBoxLeadSource.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLeadSource.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLeadSource.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLeadSource.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeadSource.Editable = true;
			comboBoxLeadSource.FilterString = "";
			comboBoxLeadSource.GenericListType = Micromind.Common.Data.GenericListTypes.LeadSource;
			comboBoxLeadSource.HasAllAccount = false;
			comboBoxLeadSource.HasCustom = false;
			comboBoxLeadSource.IsDataLoaded = false;
			comboBoxLeadSource.IsSingleColumn = false;
			comboBoxLeadSource.Location = new System.Drawing.Point(191, 118);
			comboBoxLeadSource.MaxDropDownItems = 12;
			comboBoxLeadSource.Name = "comboBoxLeadSource";
			comboBoxLeadSource.ShowInactiveItems = false;
			comboBoxLeadSource.ShowQuickAdd = true;
			comboBoxLeadSource.Size = new System.Drawing.Size(227, 20);
			comboBoxLeadSource.TabIndex = 6;
			comboBoxLeadSource.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPriceLevel.Assigned = false;
			comboBoxPriceLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPriceLevel.CustomReportFieldName = "";
			comboBoxPriceLevel.CustomReportKey = "";
			comboBoxPriceLevel.CustomReportValueType = 1;
			comboBoxPriceLevel.DescriptionTextBox = null;
			appearance160.BackColor = System.Drawing.SystemColors.Window;
			appearance160.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPriceLevel.DisplayLayout.Appearance = appearance160;
			comboBoxPriceLevel.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPriceLevel.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance161.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance161.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance161.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.Appearance = appearance161;
			appearance162.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BandLabelAppearance = appearance162;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance163.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance163.BackColor2 = System.Drawing.SystemColors.Control;
			appearance163.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance163.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.PromptAppearance = appearance163;
			comboBoxPriceLevel.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPriceLevel.DisplayLayout.MaxRowScrollRegions = 1;
			appearance164.BackColor = System.Drawing.SystemColors.Window;
			appearance164.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveCellAppearance = appearance164;
			appearance165.BackColor = System.Drawing.SystemColors.Highlight;
			appearance165.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveRowAppearance = appearance165;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance166.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.CardAreaAppearance = appearance166;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			appearance167.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPriceLevel.DisplayLayout.Override.CellAppearance = appearance167;
			comboBoxPriceLevel.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPriceLevel.DisplayLayout.Override.CellPadding = 0;
			appearance168.BackColor = System.Drawing.SystemColors.Control;
			appearance168.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance168.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance168.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.GroupByRowAppearance = appearance168;
			appearance169.TextHAlignAsString = "Left";
			comboBoxPriceLevel.DisplayLayout.Override.HeaderAppearance = appearance169;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance170.BackColor = System.Drawing.SystemColors.Window;
			appearance170.BorderColor = System.Drawing.Color.Silver;
			comboBoxPriceLevel.DisplayLayout.Override.RowAppearance = appearance170;
			comboBoxPriceLevel.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance171.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPriceLevel.DisplayLayout.Override.TemplateAddRowAppearance = appearance171;
			comboBoxPriceLevel.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPriceLevel.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPriceLevel.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPriceLevel.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPriceLevel.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxPriceLevel.Editable = true;
			comboBoxPriceLevel.FilterString = "";
			comboBoxPriceLevel.HasAllAccount = false;
			comboBoxPriceLevel.HasCustom = false;
			comboBoxPriceLevel.IsDataLoaded = false;
			comboBoxPriceLevel.Location = new System.Drawing.Point(630, 166);
			comboBoxPriceLevel.MaxDropDownItems = 12;
			comboBoxPriceLevel.MaxLength = 15;
			comboBoxPriceLevel.Name = "comboBoxPriceLevel";
			comboBoxPriceLevel.ShowInactiveItems = false;
			comboBoxPriceLevel.ShowQuickAdd = true;
			comboBoxPriceLevel.Size = new System.Drawing.Size(284, 20);
			comboBoxPriceLevel.TabIndex = 13;
			comboBoxPriceLevel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxFormalName.BackColor = System.Drawing.Color.White;
			textBoxFormalName.CustomReportFieldName = "";
			textBoxFormalName.CustomReportKey = "";
			textBoxFormalName.CustomReportValueType = 1;
			textBoxFormalName.IsComboTextBox = false;
			textBoxFormalName.IsModified = false;
			textBoxFormalName.Location = new System.Drawing.Point(191, 94);
			textBoxFormalName.MaxLength = 64;
			textBoxFormalName.Name = "textBoxFormalName";
			textBoxFormalName.Size = new System.Drawing.Size(268, 20);
			textBoxFormalName.TabIndex = 2;
			comboBoxArea.Assigned = false;
			comboBoxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArea.CustomReportFieldName = "";
			comboBoxArea.CustomReportKey = "";
			comboBoxArea.CustomReportValueType = 1;
			comboBoxArea.DescriptionTextBox = null;
			appearance172.BackColor = System.Drawing.SystemColors.Window;
			appearance172.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArea.DisplayLayout.Appearance = appearance172;
			comboBoxArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance173.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance173.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance173.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.GroupByBox.Appearance = appearance173;
			appearance174.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance174;
			comboBoxArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance175.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance175.BackColor2 = System.Drawing.SystemColors.Control;
			appearance175.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance175.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.PromptAppearance = appearance175;
			comboBoxArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance176.BackColor = System.Drawing.SystemColors.Window;
			appearance176.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArea.DisplayLayout.Override.ActiveCellAppearance = appearance176;
			appearance177.BackColor = System.Drawing.SystemColors.Highlight;
			appearance177.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArea.DisplayLayout.Override.ActiveRowAppearance = appearance177;
			comboBoxArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance178.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.CardAreaAppearance = appearance178;
			appearance179.BorderColor = System.Drawing.Color.Silver;
			appearance179.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArea.DisplayLayout.Override.CellAppearance = appearance179;
			comboBoxArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArea.DisplayLayout.Override.CellPadding = 0;
			appearance180.BackColor = System.Drawing.SystemColors.Control;
			appearance180.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance180.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance180.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance180.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.GroupByRowAppearance = appearance180;
			appearance181.TextHAlignAsString = "Left";
			comboBoxArea.DisplayLayout.Override.HeaderAppearance = appearance181;
			comboBoxArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance182.BackColor = System.Drawing.SystemColors.Window;
			appearance182.BorderColor = System.Drawing.Color.Silver;
			comboBoxArea.DisplayLayout.Override.RowAppearance = appearance182;
			comboBoxArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance183.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance183;
			comboBoxArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArea.Editable = true;
			comboBoxArea.FilterString = "";
			comboBoxArea.HasAllAccount = false;
			comboBoxArea.HasCustom = false;
			comboBoxArea.IsDataLoaded = false;
			comboBoxArea.Location = new System.Drawing.Point(630, 142);
			comboBoxArea.MaxDropDownItems = 12;
			comboBoxArea.MaxLength = 15;
			comboBoxArea.Name = "comboBoxArea";
			comboBoxArea.ShowInactiveItems = false;
			comboBoxArea.ShowQuickAdd = true;
			comboBoxArea.Size = new System.Drawing.Size(284, 20);
			comboBoxArea.TabIndex = 12;
			comboBoxArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxForeignName.BackColor = System.Drawing.Color.White;
			textBoxForeignName.CustomReportFieldName = "";
			textBoxForeignName.CustomReportKey = "";
			textBoxForeignName.CustomReportValueType = 1;
			textBoxForeignName.IsComboTextBox = false;
			textBoxForeignName.IsModified = false;
			textBoxForeignName.IsRequired = true;
			textBoxForeignName.Location = new System.Drawing.Point(191, 118);
			textBoxForeignName.MaxLength = 64;
			textBoxForeignName.Name = "textBoxForeignName";
			textBoxForeignName.Size = new System.Drawing.Size(268, 20);
			textBoxForeignName.TabIndex = 3;
			dateTimePickerEstablished.Checked = false;
			dateTimePickerEstablished.CustomFormat = " ";
			dateTimePickerEstablished.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerEstablished.Location = new System.Drawing.Point(589, 94);
			dateTimePickerEstablished.Name = "dateTimePickerEstablished";
			dateTimePickerEstablished.ShowCheckBox = true;
			dateTimePickerEstablished.Size = new System.Drawing.Size(325, 20);
			dateTimePickerEstablished.TabIndex = 5;
			dateTimePickerEstablished.Value = new System.DateTime(0L);
			textBoxEmail.BackColor = System.Drawing.Color.White;
			textBoxEmail.CustomReportFieldName = "";
			textBoxEmail.CustomReportKey = "";
			textBoxEmail.CustomReportValueType = 1;
			textBoxEmail.IsComboTextBox = false;
			textBoxEmail.IsModified = false;
			textBoxEmail.Location = new System.Drawing.Point(626, 405);
			textBoxEmail.MaxLength = 64;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(276, 20);
			textBoxEmail.TabIndex = 29;
			dateTimePickerCustomerSince.Checked = false;
			dateTimePickerCustomerSince.CustomFormat = " ";
			dateTimePickerCustomerSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerCustomerSince.Location = new System.Drawing.Point(191, 94);
			dateTimePickerCustomerSince.Name = "dateTimePickerCustomerSince";
			dateTimePickerCustomerSince.ShowCheckBox = true;
			dateTimePickerCustomerSince.Size = new System.Drawing.Size(227, 20);
			dateTimePickerCustomerSince.TabIndex = 4;
			dateTimePickerCustomerSince.Value = new System.DateTime(0L);
			comboBoxCountry.Assigned = false;
			comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCountry.CustomReportFieldName = "";
			comboBoxCountry.CustomReportKey = "";
			comboBoxCountry.CustomReportValueType = 1;
			comboBoxCountry.DescriptionTextBox = null;
			appearance184.BackColor = System.Drawing.SystemColors.Window;
			appearance184.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCountry.DisplayLayout.Appearance = appearance184;
			comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance185.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance185.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance185.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance185.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance185;
			appearance186.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance186;
			comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance187.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance187.BackColor2 = System.Drawing.SystemColors.Control;
			appearance187.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance187.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance187;
			comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance188.BackColor = System.Drawing.SystemColors.Window;
			appearance188.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance188;
			appearance189.BackColor = System.Drawing.SystemColors.Highlight;
			appearance189.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance189;
			comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance190.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance190;
			appearance191.BorderColor = System.Drawing.Color.Silver;
			appearance191.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance191;
			comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
			appearance192.BackColor = System.Drawing.SystemColors.Control;
			appearance192.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance192.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance192.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance192.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance192;
			appearance193.TextHAlignAsString = "Left";
			comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance193;
			comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance194.BackColor = System.Drawing.SystemColors.Window;
			appearance194.BorderColor = System.Drawing.Color.Silver;
			comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance194;
			comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance195.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance195;
			comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCountry.Editable = true;
			comboBoxCountry.FilterString = "";
			comboBoxCountry.HasAllAccount = false;
			comboBoxCountry.HasCustom = false;
			comboBoxCountry.IsDataLoaded = false;
			comboBoxCountry.Location = new System.Drawing.Point(630, 118);
			comboBoxCountry.MaxDropDownItems = 12;
			comboBoxCountry.MaxLength = 15;
			comboBoxCountry.Name = "comboBoxCountry";
			comboBoxCountry.ShowInactiveItems = false;
			comboBoxCountry.ShowQuickAdd = true;
			comboBoxCountry.Size = new System.Drawing.Size(284, 20);
			comboBoxCountry.TabIndex = 11;
			comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxMobile.BackColor = System.Drawing.Color.White;
			textBoxMobile.CustomReportFieldName = "";
			textBoxMobile.CustomReportKey = "";
			textBoxMobile.CustomReportValueType = 1;
			textBoxMobile.IsComboTextBox = false;
			textBoxMobile.IsModified = false;
			textBoxMobile.Location = new System.Drawing.Point(626, 381);
			textBoxMobile.MaxLength = 30;
			textBoxMobile.Name = "textBoxMobile";
			textBoxMobile.Size = new System.Drawing.Size(276, 20);
			textBoxMobile.TabIndex = 27;
			comboBoxBilltoAddress.Assigned = false;
			comboBoxBilltoAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBilltoAddress.CustomReportFieldName = "";
			comboBoxBilltoAddress.CustomReportKey = "";
			comboBoxBilltoAddress.CustomReportValueType = 1;
			comboBoxBilltoAddress.DescriptionTextBox = null;
			appearance196.BackColor = System.Drawing.SystemColors.Window;
			appearance196.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBilltoAddress.DisplayLayout.Appearance = appearance196;
			comboBoxBilltoAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBilltoAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance197.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance197.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance197.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance197.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.Appearance = appearance197;
			appearance198.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance198;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance199.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance199.BackColor2 = System.Drawing.SystemColors.Control;
			appearance199.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance199.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance199;
			comboBoxBilltoAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBilltoAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance200.BackColor = System.Drawing.SystemColors.Window;
			appearance200.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBilltoAddress.DisplayLayout.Override.ActiveCellAppearance = appearance200;
			appearance201.BackColor = System.Drawing.SystemColors.Highlight;
			appearance201.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBilltoAddress.DisplayLayout.Override.ActiveRowAppearance = appearance201;
			comboBoxBilltoAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBilltoAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance202.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBilltoAddress.DisplayLayout.Override.CardAreaAppearance = appearance202;
			appearance203.BorderColor = System.Drawing.Color.Silver;
			appearance203.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBilltoAddress.DisplayLayout.Override.CellAppearance = appearance203;
			comboBoxBilltoAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBilltoAddress.DisplayLayout.Override.CellPadding = 0;
			appearance204.BackColor = System.Drawing.SystemColors.Control;
			appearance204.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance204.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance204.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance204.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBilltoAddress.DisplayLayout.Override.GroupByRowAppearance = appearance204;
			appearance205.TextHAlignAsString = "Left";
			comboBoxBilltoAddress.DisplayLayout.Override.HeaderAppearance = appearance205;
			comboBoxBilltoAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBilltoAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance206.BackColor = System.Drawing.SystemColors.Window;
			appearance206.BorderColor = System.Drawing.Color.Silver;
			comboBoxBilltoAddress.DisplayLayout.Override.RowAppearance = appearance206;
			comboBoxBilltoAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance207.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBilltoAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance207;
			comboBoxBilltoAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBilltoAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBilltoAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBilltoAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBilltoAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxBilltoAddress.Editable = true;
			comboBoxBilltoAddress.FilterString = "";
			comboBoxBilltoAddress.HasAllAccount = false;
			comboBoxBilltoAddress.HasCustom = false;
			comboBoxBilltoAddress.IsDataLoaded = false;
			comboBoxBilltoAddress.Location = new System.Drawing.Point(589, 46);
			comboBoxBilltoAddress.MaxDropDownItems = 12;
			comboBoxBilltoAddress.Name = "comboBoxBilltoAddress";
			comboBoxBilltoAddress.ShowInactiveItems = false;
			comboBoxBilltoAddress.ShowQuickAdd = true;
			comboBoxBilltoAddress.Size = new System.Drawing.Size(325, 20);
			comboBoxBilltoAddress.TabIndex = 1;
			comboBoxBilltoAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxParentCustomer.Assigned = false;
			comboBoxParentCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxParentCustomer.CustomReportFieldName = "";
			comboBoxParentCustomer.CustomReportKey = "";
			comboBoxParentCustomer.CustomReportValueType = 1;
			comboBoxParentCustomer.DescriptionTextBox = null;
			appearance208.BackColor = System.Drawing.SystemColors.Window;
			appearance208.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxParentCustomer.DisplayLayout.Appearance = appearance208;
			comboBoxParentCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxParentCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance209.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance209.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance209.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance209.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.Appearance = appearance209;
			appearance210.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance210;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance211.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance211.BackColor2 = System.Drawing.SystemColors.Control;
			appearance211.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance211.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance211;
			comboBoxParentCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxParentCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance212.BackColor = System.Drawing.SystemColors.Window;
			appearance212.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxParentCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance212;
			appearance213.BackColor = System.Drawing.SystemColors.Highlight;
			appearance213.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxParentCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance213;
			comboBoxParentCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxParentCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance214.BackColor = System.Drawing.SystemColors.Window;
			comboBoxParentCustomer.DisplayLayout.Override.CardAreaAppearance = appearance214;
			appearance215.BorderColor = System.Drawing.Color.Silver;
			appearance215.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxParentCustomer.DisplayLayout.Override.CellAppearance = appearance215;
			comboBoxParentCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxParentCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance216.BackColor = System.Drawing.SystemColors.Control;
			appearance216.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance216.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance216.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance216.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance216;
			appearance217.TextHAlignAsString = "Left";
			comboBoxParentCustomer.DisplayLayout.Override.HeaderAppearance = appearance217;
			comboBoxParentCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxParentCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance218.BackColor = System.Drawing.SystemColors.Window;
			appearance218.BorderColor = System.Drawing.Color.Silver;
			comboBoxParentCustomer.DisplayLayout.Override.RowAppearance = appearance218;
			comboBoxParentCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance219.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxParentCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance219;
			comboBoxParentCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxParentCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxParentCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxParentCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxParentCustomer.Editable = true;
			comboBoxParentCustomer.FilterString = "";
			comboBoxParentCustomer.FilterSysDocID = "";
			comboBoxParentCustomer.HasAll = false;
			comboBoxParentCustomer.HasCustom = false;
			comboBoxParentCustomer.IsDataLoaded = false;
			comboBoxParentCustomer.Location = new System.Drawing.Point(191, 142);
			comboBoxParentCustomer.MaxDropDownItems = 12;
			comboBoxParentCustomer.MaxLength = 64;
			comboBoxParentCustomer.Name = "comboBoxParentCustomer";
			comboBoxParentCustomer.ShowConsignmentOnly = false;
			comboBoxParentCustomer.ShowInactive = false;
			comboBoxParentCustomer.ShowLPOCustomersOnly = false;
			comboBoxParentCustomer.ShowPROCustomersOnly = false;
			comboBoxParentCustomer.ShowQuickAdd = true;
			comboBoxParentCustomer.Size = new System.Drawing.Size(268, 20);
			comboBoxParentCustomer.TabIndex = 5;
			comboBoxParentCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxParentCustomer.SelectedIndexChanged += new System.EventHandler(comboBoxParentCustomer_SelectedIndexChanged);
			comboBoxShippingMethods.Assigned = false;
			comboBoxShippingMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethods.CustomReportFieldName = "";
			comboBoxShippingMethods.CustomReportKey = "";
			comboBoxShippingMethods.CustomReportValueType = 1;
			comboBoxShippingMethods.DescriptionTextBox = null;
			appearance220.BackColor = System.Drawing.SystemColors.Window;
			appearance220.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethods.DisplayLayout.Appearance = appearance220;
			comboBoxShippingMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance221.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance221.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance221.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance221.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.Appearance = appearance221;
			appearance222.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance222;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance223.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance223.BackColor2 = System.Drawing.SystemColors.Control;
			appearance223.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance223.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance223;
			comboBoxShippingMethods.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethods.DisplayLayout.MaxRowScrollRegions = 1;
			appearance224.BackColor = System.Drawing.SystemColors.Window;
			appearance224.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethods.DisplayLayout.Override.ActiveCellAppearance = appearance224;
			appearance225.BackColor = System.Drawing.SystemColors.Highlight;
			appearance225.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethods.DisplayLayout.Override.ActiveRowAppearance = appearance225;
			comboBoxShippingMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance226.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.Override.CardAreaAppearance = appearance226;
			appearance227.BorderColor = System.Drawing.Color.Silver;
			appearance227.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethods.DisplayLayout.Override.CellAppearance = appearance227;
			comboBoxShippingMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethods.DisplayLayout.Override.CellPadding = 0;
			appearance228.BackColor = System.Drawing.SystemColors.Control;
			appearance228.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance228.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance228.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance228.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.Override.GroupByRowAppearance = appearance228;
			appearance229.TextHAlignAsString = "Left";
			comboBoxShippingMethods.DisplayLayout.Override.HeaderAppearance = appearance229;
			comboBoxShippingMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance230.BackColor = System.Drawing.SystemColors.Window;
			appearance230.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethods.DisplayLayout.Override.RowAppearance = appearance230;
			comboBoxShippingMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance231.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance231;
			comboBoxShippingMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethods.Editable = true;
			comboBoxShippingMethods.FilterString = "";
			comboBoxShippingMethods.HasAllAccount = false;
			comboBoxShippingMethods.HasCustom = false;
			comboBoxShippingMethods.IsDataLoaded = false;
			comboBoxShippingMethods.Location = new System.Drawing.Point(589, 70);
			comboBoxShippingMethods.MaxDropDownItems = 12;
			comboBoxShippingMethods.MaxLength = 15;
			comboBoxShippingMethods.Name = "comboBoxShippingMethods";
			comboBoxShippingMethods.ShowInactiveItems = false;
			comboBoxShippingMethods.ShowQuickAdd = true;
			comboBoxShippingMethods.Size = new System.Drawing.Size(325, 20);
			comboBoxShippingMethods.TabIndex = 3;
			comboBoxShippingMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPostalCode.BackColor = System.Drawing.Color.White;
			textBoxPostalCode.CustomReportFieldName = "";
			textBoxPostalCode.CustomReportKey = "";
			textBoxPostalCode.CustomReportValueType = 1;
			textBoxPostalCode.IsComboTextBox = false;
			textBoxPostalCode.IsModified = false;
			textBoxPostalCode.Location = new System.Drawing.Point(203, 477);
			textBoxPostalCode.MaxLength = 30;
			textBoxPostalCode.Name = "textBoxPostalCode";
			textBoxPostalCode.Size = new System.Drawing.Size(252, 20);
			textBoxPostalCode.TabIndex = 15;
			textBoxFax.BackColor = System.Drawing.Color.White;
			textBoxFax.CustomReportFieldName = "";
			textBoxFax.CustomReportKey = "";
			textBoxFax.CustomReportValueType = 1;
			textBoxFax.IsComboTextBox = false;
			textBoxFax.IsModified = false;
			textBoxFax.Location = new System.Drawing.Point(626, 357);
			textBoxFax.MaxLength = 30;
			textBoxFax.Name = "textBoxFax";
			textBoxFax.Size = new System.Drawing.Size(276, 20);
			textBoxFax.TabIndex = 25;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(463, 46);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(94, 20);
			checkBoxIsInactive.TabIndex = 7;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			comboBoxShiptoAddress.Assigned = false;
			comboBoxShiptoAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShiptoAddress.CustomReportFieldName = "";
			comboBoxShiptoAddress.CustomReportKey = "";
			comboBoxShiptoAddress.CustomReportValueType = 1;
			comboBoxShiptoAddress.DescriptionTextBox = null;
			appearance232.BackColor = System.Drawing.SystemColors.Window;
			appearance232.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShiptoAddress.DisplayLayout.Appearance = appearance232;
			comboBoxShiptoAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShiptoAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance233.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance233.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance233.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance233.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.Appearance = appearance233;
			appearance234.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance234;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance235.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance235.BackColor2 = System.Drawing.SystemColors.Control;
			appearance235.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance235.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance235;
			comboBoxShiptoAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShiptoAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance236.BackColor = System.Drawing.SystemColors.Window;
			appearance236.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShiptoAddress.DisplayLayout.Override.ActiveCellAppearance = appearance236;
			appearance237.BackColor = System.Drawing.SystemColors.Highlight;
			appearance237.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShiptoAddress.DisplayLayout.Override.ActiveRowAppearance = appearance237;
			comboBoxShiptoAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShiptoAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance238.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShiptoAddress.DisplayLayout.Override.CardAreaAppearance = appearance238;
			appearance239.BorderColor = System.Drawing.Color.Silver;
			appearance239.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShiptoAddress.DisplayLayout.Override.CellAppearance = appearance239;
			comboBoxShiptoAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShiptoAddress.DisplayLayout.Override.CellPadding = 0;
			appearance240.BackColor = System.Drawing.SystemColors.Control;
			appearance240.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance240.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance240.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance240.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShiptoAddress.DisplayLayout.Override.GroupByRowAppearance = appearance240;
			appearance241.TextHAlignAsString = "Left";
			comboBoxShiptoAddress.DisplayLayout.Override.HeaderAppearance = appearance241;
			comboBoxShiptoAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShiptoAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance242.BackColor = System.Drawing.SystemColors.Window;
			appearance242.BorderColor = System.Drawing.Color.Silver;
			comboBoxShiptoAddress.DisplayLayout.Override.RowAppearance = appearance242;
			comboBoxShiptoAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance243.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShiptoAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance243;
			comboBoxShiptoAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShiptoAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShiptoAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShiptoAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShiptoAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxShiptoAddress.Editable = true;
			comboBoxShiptoAddress.FilterString = "";
			comboBoxShiptoAddress.HasAllAccount = false;
			comboBoxShiptoAddress.HasCustom = false;
			comboBoxShiptoAddress.IsDataLoaded = false;
			comboBoxShiptoAddress.Location = new System.Drawing.Point(191, 70);
			comboBoxShiptoAddress.MaxDropDownItems = 12;
			comboBoxShiptoAddress.Name = "comboBoxShiptoAddress";
			comboBoxShiptoAddress.ShowInactiveItems = false;
			comboBoxShiptoAddress.ShowQuickAdd = true;
			comboBoxShiptoAddress.Size = new System.Drawing.Size(227, 20);
			comboBoxShiptoAddress.TabIndex = 2;
			comboBoxShiptoAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			checkBoxHold.BackColor = System.Drawing.Color.Transparent;
			checkBoxHold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxHold.Location = new System.Drawing.Point(561, 46);
			checkBoxHold.Name = "checkBoxHold";
			checkBoxHold.Size = new System.Drawing.Size(353, 20);
			checkBoxHold.TabIndex = 8;
			checkBoxHold.Text = "Hold";
			checkBoxHold.UseVisualStyleBackColor = true;
			textBoxPhone2.BackColor = System.Drawing.Color.White;
			textBoxPhone2.CustomReportFieldName = "";
			textBoxPhone2.CustomReportKey = "";
			textBoxPhone2.CustomReportValueType = 1;
			textBoxPhone2.IsComboTextBox = false;
			textBoxPhone2.IsModified = false;
			textBoxPhone2.Location = new System.Drawing.Point(626, 333);
			textBoxPhone2.MaxLength = 30;
			textBoxPhone2.Name = "textBoxPhone2";
			textBoxPhone2.Size = new System.Drawing.Size(276, 20);
			textBoxPhone2.TabIndex = 23;
			comboBoxCustomerClass.Assigned = false;
			comboBoxCustomerClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomerClass.CustomReportFieldName = "";
			comboBoxCustomerClass.CustomReportKey = "";
			comboBoxCustomerClass.CustomReportValueType = 1;
			comboBoxCustomerClass.DescriptionTextBox = null;
			appearance244.BackColor = System.Drawing.SystemColors.Window;
			appearance244.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomerClass.DisplayLayout.Appearance = appearance244;
			comboBoxCustomerClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomerClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance245.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance245.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance245.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance245.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.Appearance = appearance245;
			appearance246.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance246;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance247.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance247.BackColor2 = System.Drawing.SystemColors.Control;
			appearance247.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance247.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.PromptAppearance = appearance247;
			comboBoxCustomerClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomerClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance248.BackColor = System.Drawing.SystemColors.Window;
			appearance248.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomerClass.DisplayLayout.Override.ActiveCellAppearance = appearance248;
			appearance249.BackColor = System.Drawing.SystemColors.Highlight;
			appearance249.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomerClass.DisplayLayout.Override.ActiveRowAppearance = appearance249;
			comboBoxCustomerClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomerClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance250.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.Override.CardAreaAppearance = appearance250;
			appearance251.BorderColor = System.Drawing.Color.Silver;
			appearance251.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomerClass.DisplayLayout.Override.CellAppearance = appearance251;
			comboBoxCustomerClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomerClass.DisplayLayout.Override.CellPadding = 0;
			appearance252.BackColor = System.Drawing.SystemColors.Control;
			appearance252.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance252.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance252.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance252.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.Override.GroupByRowAppearance = appearance252;
			appearance253.TextHAlignAsString = "Left";
			comboBoxCustomerClass.DisplayLayout.Override.HeaderAppearance = appearance253;
			comboBoxCustomerClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomerClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance254.BackColor = System.Drawing.SystemColors.Window;
			appearance254.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomerClass.DisplayLayout.Override.RowAppearance = appearance254;
			comboBoxCustomerClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance255.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomerClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance255;
			comboBoxCustomerClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomerClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomerClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomerClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomerClass.Editable = true;
			comboBoxCustomerClass.FilterString = "";
			comboBoxCustomerClass.HasAllAccount = false;
			comboBoxCustomerClass.HasCustom = false;
			comboBoxCustomerClass.IsDataLoaded = false;
			comboBoxCustomerClass.Location = new System.Drawing.Point(630, 70);
			comboBoxCustomerClass.MaxDropDownItems = 12;
			comboBoxCustomerClass.MaxLength = 15;
			comboBoxCustomerClass.Name = "comboBoxCustomerClass";
			comboBoxCustomerClass.ShowInactiveItems = false;
			comboBoxCustomerClass.ShowQuickAdd = true;
			comboBoxCustomerClass.Size = new System.Drawing.Size(284, 20);
			comboBoxCustomerClass.TabIndex = 9;
			comboBoxCustomerClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCustomerClass.SelectedIndexChanged += new System.EventHandler(comboBoxCustomerClass_SelectedIndexChanged);
			textBoxAddressID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAddressID.CustomReportFieldName = "";
			textBoxAddressID.CustomReportKey = "";
			textBoxAddressID.CustomReportValueType = 1;
			textBoxAddressID.Enabled = false;
			textBoxAddressID.ForeColor = System.Drawing.Color.Black;
			textBoxAddressID.IsComboTextBox = false;
			textBoxAddressID.IsModified = false;
			textBoxAddressID.Location = new System.Drawing.Point(203, 285);
			textBoxAddressID.MaxLength = 15;
			textBoxAddressID.Name = "textBoxAddressID";
			textBoxAddressID.Size = new System.Drawing.Size(252, 20);
			textBoxAddressID.TabIndex = 1;
			textBoxAddressID.Text = "PRIMARY";
			textBoxPhone1.BackColor = System.Drawing.Color.White;
			textBoxPhone1.CustomReportFieldName = "";
			textBoxPhone1.CustomReportKey = "";
			textBoxPhone1.CustomReportValueType = 1;
			textBoxPhone1.IsComboTextBox = false;
			textBoxPhone1.IsModified = false;
			textBoxPhone1.Location = new System.Drawing.Point(626, 309);
			textBoxPhone1.MaxLength = 30;
			textBoxPhone1.Name = "textBoxPhone1";
			textBoxPhone1.Size = new System.Drawing.Size(276, 20);
			textBoxPhone1.TabIndex = 21;
			textBoxContactName.BackColor = System.Drawing.Color.White;
			textBoxContactName.CustomReportFieldName = "";
			textBoxContactName.CustomReportKey = "";
			textBoxContactName.CustomReportValueType = 1;
			textBoxContactName.IsComboTextBox = false;
			textBoxContactName.IsModified = false;
			textBoxContactName.Location = new System.Drawing.Point(203, 309);
			textBoxContactName.MaxLength = 64;
			textBoxContactName.Name = "textBoxContactName";
			textBoxContactName.Size = new System.Drawing.Size(252, 20);
			textBoxContactName.TabIndex = 3;
			textBoxAddress1.BackColor = System.Drawing.Color.White;
			textBoxAddress1.CustomReportFieldName = "";
			textBoxAddress1.CustomReportKey = "";
			textBoxAddress1.CustomReportValueType = 1;
			textBoxAddress1.IsComboTextBox = false;
			textBoxAddress1.IsModified = false;
			textBoxAddress1.Location = new System.Drawing.Point(203, 333);
			textBoxAddress1.MaxLength = 64;
			textBoxAddress1.Name = "textBoxAddress1";
			textBoxAddress1.Size = new System.Drawing.Size(252, 20);
			textBoxAddress1.TabIndex = 5;
			textBoxAddress2.BackColor = System.Drawing.Color.White;
			textBoxAddress2.CustomReportFieldName = "";
			textBoxAddress2.CustomReportKey = "";
			textBoxAddress2.CustomReportValueType = 1;
			textBoxAddress2.IsComboTextBox = false;
			textBoxAddress2.IsModified = false;
			textBoxAddress2.Location = new System.Drawing.Point(203, 357);
			textBoxAddress2.MaxLength = 64;
			textBoxAddress2.Name = "textBoxAddress2";
			textBoxAddress2.Size = new System.Drawing.Size(252, 20);
			textBoxAddress2.TabIndex = 6;
			textBoxAddress3.BackColor = System.Drawing.Color.White;
			textBoxAddress3.CustomReportFieldName = "";
			textBoxAddress3.CustomReportKey = "";
			textBoxAddress3.CustomReportValueType = 1;
			textBoxAddress3.IsComboTextBox = false;
			textBoxAddress3.IsModified = false;
			textBoxAddress3.Location = new System.Drawing.Point(203, 381);
			textBoxAddress3.MaxLength = 64;
			textBoxAddress3.Name = "textBoxAddress3";
			textBoxAddress3.Size = new System.Drawing.Size(252, 20);
			textBoxAddress3.TabIndex = 7;
			textBoxCity.BackColor = System.Drawing.Color.White;
			textBoxCity.CustomReportFieldName = "";
			textBoxCity.CustomReportKey = "";
			textBoxCity.CustomReportValueType = 1;
			textBoxCity.IsComboTextBox = false;
			textBoxCity.IsModified = false;
			textBoxCity.Location = new System.Drawing.Point(203, 405);
			textBoxCity.MaxLength = 30;
			textBoxCity.Name = "textBoxCity";
			textBoxCity.Size = new System.Drawing.Size(252, 20);
			textBoxCity.TabIndex = 9;
			textBoxState.BackColor = System.Drawing.Color.White;
			textBoxState.CustomReportFieldName = "";
			textBoxState.CustomReportKey = "";
			textBoxState.CustomReportValueType = 1;
			textBoxState.IsComboTextBox = false;
			textBoxState.IsModified = false;
			textBoxState.Location = new System.Drawing.Point(203, 429);
			textBoxState.MaxLength = 30;
			textBoxState.Name = "textBoxState";
			textBoxState.Size = new System.Drawing.Size(252, 20);
			textBoxState.TabIndex = 11;
			textBoxCountry.BackColor = System.Drawing.Color.White;
			textBoxCountry.CustomReportFieldName = "";
			textBoxCountry.CustomReportKey = "";
			textBoxCountry.CustomReportValueType = 1;
			textBoxCountry.IsComboTextBox = false;
			textBoxCountry.IsModified = false;
			textBoxCountry.Location = new System.Drawing.Point(203, 453);
			textBoxCountry.MaxLength = 30;
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(252, 20);
			textBoxCountry.TabIndex = 13;
			Root.AllowHtmlStringInCaption = true;
			Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			Root.GroupBordersVisible = false;
			Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1]
			{
				tabbedControlGroup1
			});
			Root.Name = "Root";
			Root.Size = new System.Drawing.Size(938, 665);
			Root.TextVisible = false;
			tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
			tabbedControlGroup1.Name = "tabbedControlGroup1";
			tabbedControlGroup1.SelectedTabPage = layoutControlGroup3;
			tabbedControlGroup1.Size = new System.Drawing.Size(918, 645);
			tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[8]
			{
				tabPageGeneral,
				tabPageDetails,
				layoutControlGroup3,
				layoutControlGroup10,
				tabPageActivity,
				layoutControlGroup12,
				layoutControlGroup13,
				layoutControlGroup14
			});
			tabbedControlGroup1.Text = "Contacts";
			layoutControlGroup3.CustomizationFormText = "Credit Control Tab";
			layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[15]
			{
				layoutControlItem64,
				layoutControlItem65,
				layoutControlItem66,
				layoutControlItem67,
				layoutControlItem68,
				layoutControlItem69,
				layoutControlItem70,
				layoutControlItem72,
				layoutControlItem73,
				layoutControlItem71,
				layoutControlItem74,
				layoutControlItem75,
				layoutGroupCreditLimit,
				layoutControlItem76,
				layoutControlGroup9
			});
			layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
			layoutControlGroup3.Name = "layoutControlGroup3";
			layoutControlGroup3.Size = new System.Drawing.Size(894, 599);
			layoutControlGroup3.Text = "Credit Control";
			layoutControlItem64.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem64.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem64.Control = comboBoxPaymentMethods;
			layoutControlItem64.CustomizationFormText = "Payment Method";
			layoutControlItem64.Location = new System.Drawing.Point(0, 0);
			layoutControlItem64.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem64.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem64.Name = "layoutControlItem64";
			layoutControlItem64.Size = new System.Drawing.Size(439, 24);
			layoutControlItem64.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem64.Text = "Payment Method:";
			layoutControlItem64.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem64.Click += new System.EventHandler(layoutControlItem64_Click);
			layoutControlItem65.Control = textBoxPaymentMethodName;
			layoutControlItem65.CustomizationFormText = "Payment Method Name";
			layoutControlItem65.Location = new System.Drawing.Point(439, 0);
			layoutControlItem65.MaxSize = new System.Drawing.Size(440, 24);
			layoutControlItem65.MinSize = new System.Drawing.Size(440, 24);
			layoutControlItem65.Name = "layoutControlItem65";
			layoutControlItem65.Size = new System.Drawing.Size(455, 24);
			layoutControlItem65.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem65.Text = " ";
			layoutControlItem65.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem65.TextVisible = false;
			layoutControlItem66.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem66.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem66.Control = comboBoxPaymentTerms;
			layoutControlItem66.CustomizationFormText = "Payment Term";
			layoutControlItem66.Location = new System.Drawing.Point(0, 24);
			layoutControlItem66.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem66.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem66.Name = "layoutControlItem66";
			layoutControlItem66.Size = new System.Drawing.Size(439, 24);
			layoutControlItem66.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem66.Text = "Payment Term:";
			layoutControlItem66.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem66.Click += new System.EventHandler(layoutControlItem66_Click);
			layoutControlItem67.Control = textBoxPaymentTermName;
			layoutControlItem67.CustomizationFormText = "Payment Term Name";
			layoutControlItem67.Location = new System.Drawing.Point(439, 24);
			layoutControlItem67.MaxSize = new System.Drawing.Size(440, 24);
			layoutControlItem67.MinSize = new System.Drawing.Size(440, 24);
			layoutControlItem67.Name = "layoutControlItem67";
			layoutControlItem67.Size = new System.Drawing.Size(455, 24);
			layoutControlItem67.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem67.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem67.TextVisible = false;
			layoutControlItem68.Control = dateTimePickerReviewDate;
			layoutControlItem68.Location = new System.Drawing.Point(0, 48);
			layoutControlItem68.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem68.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem68.Name = "layoutControlItem68";
			layoutControlItem68.Size = new System.Drawing.Size(439, 24);
			layoutControlItem68.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem68.Text = "Credit Review Date:";
			layoutControlItem68.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem69.Control = comboBoxCreditReviewBy;
			layoutControlItem69.Location = new System.Drawing.Point(439, 48);
			layoutControlItem69.MaxSize = new System.Drawing.Size(440, 24);
			layoutControlItem69.MinSize = new System.Drawing.Size(440, 24);
			layoutControlItem69.Name = "layoutControlItem69";
			layoutControlItem69.Size = new System.Drawing.Size(455, 24);
			layoutControlItem69.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem69.Text = "Reviewed By:";
			layoutControlItem69.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem70.Control = comboBoxCollectionUser;
			layoutControlItem70.Location = new System.Drawing.Point(0, 72);
			layoutControlItem70.MaxSize = new System.Drawing.Size(879, 24);
			layoutControlItem70.MinSize = new System.Drawing.Size(879, 24);
			layoutControlItem70.Name = "layoutControlItem70";
			layoutControlItem70.Size = new System.Drawing.Size(894, 24);
			layoutControlItem70.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem70.Text = "Collection User:";
			layoutControlItem70.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem72.Control = dateTimePickerRatingDate;
			layoutControlItem72.Location = new System.Drawing.Point(0, 96);
			layoutControlItem72.MaxSize = new System.Drawing.Size(245, 24);
			layoutControlItem72.MinSize = new System.Drawing.Size(245, 24);
			layoutControlItem72.Name = "layoutControlItem72";
			layoutControlItem72.Size = new System.Drawing.Size(245, 24);
			layoutControlItem72.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem72.Text = "Credit Rating:";
			layoutControlItem72.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem73.Control = comboBoxRatingBy;
			layoutControlItem73.Location = new System.Drawing.Point(561, 96);
			layoutControlItem73.MaxSize = new System.Drawing.Size(318, 24);
			layoutControlItem73.MinSize = new System.Drawing.Size(318, 24);
			layoutControlItem73.Name = "layoutControlItem73";
			layoutControlItem73.Size = new System.Drawing.Size(333, 24);
			layoutControlItem73.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem73.Text = "Rating By:";
			layoutControlItem73.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem71.Control = comboBoxRating;
			layoutControlItem71.CustomizationFormText = "Rating Date:";
			layoutControlItem71.Location = new System.Drawing.Point(245, 96);
			layoutControlItem71.MaxSize = new System.Drawing.Size(316, 24);
			layoutControlItem71.MinSize = new System.Drawing.Size(316, 24);
			layoutControlItem71.Name = "layoutControlItem71";
			layoutControlItem71.Size = new System.Drawing.Size(316, 24);
			layoutControlItem71.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem71.Text = "Rating Date:";
			layoutControlItem71.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem74.Control = dateTimeBalanceConfirmationDate;
			layoutControlItem74.Location = new System.Drawing.Point(0, 120);
			layoutControlItem74.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem74.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem74.Name = "layoutControlItem74";
			layoutControlItem74.Size = new System.Drawing.Size(439, 24);
			layoutControlItem74.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem74.Text = "Bal Confirm Date:";
			layoutControlItem74.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem75.Control = textBoxConfirmationLevel;
			layoutControlItem75.Location = new System.Drawing.Point(439, 120);
			layoutControlItem75.MaxSize = new System.Drawing.Size(440, 24);
			layoutControlItem75.MinSize = new System.Drawing.Size(440, 24);
			layoutControlItem75.Name = "layoutControlItem75";
			layoutControlItem75.Size = new System.Drawing.Size(455, 24);
			layoutControlItem75.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem75.Text = "Bal Confirm Interval:";
			layoutControlItem75.TextSize = new System.Drawing.Size(164, 13);
			layoutGroupCreditLimit.ExpandButtonVisible = true;
			layoutGroupCreditLimit.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[17]
			{
				layoutControlItem77,
				layoutControlItem78,
				layoutControlItem81,
				layoutControlItem79,
				layoutControlItem80,
				layoutControlItem82,
				layoutControlItem83,
				layoutControlItem84,
				layoutControlItem85,
				layoutControlItem86,
				layoutControlItem87,
				layoutControlItem88,
				layoutControlItem89,
				layoutControlItem90,
				layoutControlItem91,
				layoutControlItem92,
				emptySpaceItem8
			});
			layoutGroupCreditLimit.Location = new System.Drawing.Point(0, 168);
			layoutGroupCreditLimit.Name = "layoutGroupCreditLimit";
			layoutGroupCreditLimit.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
			layoutGroupCreditLimit.Size = new System.Drawing.Size(894, 187);
			layoutGroupCreditLimit.Text = "Credit Limit";
			layoutControlItem77.Control = radioButtonCreditLimitUnlimited;
			layoutControlItem77.Location = new System.Drawing.Point(0, 0);
			layoutControlItem77.MaxSize = new System.Drawing.Size(160, 29);
			layoutControlItem77.MinSize = new System.Drawing.Size(160, 29);
			layoutControlItem77.Name = "layoutControlItem77";
			layoutControlItem77.Size = new System.Drawing.Size(160, 29);
			layoutControlItem77.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem77.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem77.TextVisible = false;
			layoutControlItem78.Control = radioButtonCreditLimitNoCredit;
			layoutControlItem78.Location = new System.Drawing.Point(160, 0);
			layoutControlItem78.MaxSize = new System.Drawing.Size(157, 29);
			layoutControlItem78.MinSize = new System.Drawing.Size(157, 29);
			layoutControlItem78.Name = "layoutControlItem78";
			layoutControlItem78.Size = new System.Drawing.Size(157, 29);
			layoutControlItem78.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem78.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem78.TextVisible = false;
			layoutControlItem81.Control = radioButtonSublimit;
			layoutControlItem81.Location = new System.Drawing.Point(317, 0);
			layoutControlItem81.MaxSize = new System.Drawing.Size(179, 29);
			layoutControlItem81.MinSize = new System.Drawing.Size(179, 29);
			layoutControlItem81.Name = "layoutControlItem81";
			layoutControlItem81.Size = new System.Drawing.Size(179, 29);
			layoutControlItem81.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem81.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem81.TextVisible = false;
			layoutControlItem79.Control = radioButtonCreditLimitAmount;
			layoutControlItem79.Location = new System.Drawing.Point(496, 0);
			layoutControlItem79.MaxSize = new System.Drawing.Size(145, 29);
			layoutControlItem79.MinSize = new System.Drawing.Size(145, 29);
			layoutControlItem79.Name = "layoutControlItem79";
			layoutControlItem79.Size = new System.Drawing.Size(145, 29);
			layoutControlItem79.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem79.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem79.TextVisible = false;
			layoutControlItem80.Control = textBoxCreditLimit;
			layoutControlItem80.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			layoutControlItem80.CustomizationFormText = "Credit Limit Amount";
			layoutControlItem80.Location = new System.Drawing.Point(641, 0);
			layoutControlItem80.MaxSize = new System.Drawing.Size(232, 29);
			layoutControlItem80.MinSize = new System.Drawing.Size(232, 29);
			layoutControlItem80.Name = "layoutControlItem80";
			layoutControlItem80.Size = new System.Drawing.Size(247, 29);
			layoutControlItem80.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem80.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem80.TextVisible = false;
			layoutControlItem82.Control = dateTimePickerCLValidity;
			layoutControlItem82.Location = new System.Drawing.Point(0, 29);
			layoutControlItem82.MaxSize = new System.Drawing.Size(319, 24);
			layoutControlItem82.MinSize = new System.Drawing.Size(319, 24);
			layoutControlItem82.Name = "layoutControlItem82";
			layoutControlItem82.Size = new System.Drawing.Size(319, 24);
			layoutControlItem82.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem82.Text = "Limit Validity:";
			layoutControlItem82.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem83.Control = checkBoxUnsecuredLimit;
			layoutControlItem83.CustomizationFormText = "PDC Unsecured Limit";
			layoutControlItem83.Location = new System.Drawing.Point(319, 29);
			layoutControlItem83.MaxSize = new System.Drawing.Size(178, 24);
			layoutControlItem83.MinSize = new System.Drawing.Size(178, 24);
			layoutControlItem83.Name = "layoutControlItem83";
			layoutControlItem83.Size = new System.Drawing.Size(178, 24);
			layoutControlItem83.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem83.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem83.TextVisible = false;
			layoutControlItem84.Control = textBoxUnsecuredLimit;
			layoutControlItem84.CustomizationFormText = "PDC Unsecured Limit";
			layoutControlItem84.Location = new System.Drawing.Point(497, 29);
			layoutControlItem84.MaxSize = new System.Drawing.Size(126, 24);
			layoutControlItem84.MinSize = new System.Drawing.Size(126, 24);
			layoutControlItem84.Name = "layoutControlItem84";
			layoutControlItem84.Size = new System.Drawing.Size(126, 24);
			layoutControlItem84.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem84.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem84.TextVisible = false;
			layoutControlItem85.Control = textBoxTempLimit;
			layoutControlItem85.Location = new System.Drawing.Point(623, 29);
			layoutControlItem85.MaxSize = new System.Drawing.Size(250, 24);
			layoutControlItem85.MinSize = new System.Drawing.Size(250, 24);
			layoutControlItem85.Name = "layoutControlItem85";
			layoutControlItem85.Size = new System.Drawing.Size(265, 24);
			layoutControlItem85.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem85.Text = "Temp Limit:";
			layoutControlItem85.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem86.Control = checkBoxAcceptCheque;
			layoutControlItem86.Location = new System.Drawing.Point(0, 53);
			layoutControlItem86.MaxSize = new System.Drawing.Size(198, 24);
			layoutControlItem86.MinSize = new System.Drawing.Size(198, 24);
			layoutControlItem86.Name = "layoutControlItem86";
			layoutControlItem86.Size = new System.Drawing.Size(198, 24);
			layoutControlItem86.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem86.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem86.TextVisible = false;
			layoutControlItem87.Control = checkBoxAcceptPDC;
			layoutControlItem87.Location = new System.Drawing.Point(198, 53);
			layoutControlItem87.MaxSize = new System.Drawing.Size(299, 24);
			layoutControlItem87.MinSize = new System.Drawing.Size(299, 24);
			layoutControlItem87.Name = "layoutControlItem87";
			layoutControlItem87.Size = new System.Drawing.Size(299, 24);
			layoutControlItem87.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem87.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem87.TextVisible = false;
			layoutControlItem88.Control = textBoxGraceDays;
			layoutControlItem88.Location = new System.Drawing.Point(497, 53);
			layoutControlItem88.MaxSize = new System.Drawing.Size(376, 24);
			layoutControlItem88.MinSize = new System.Drawing.Size(376, 24);
			layoutControlItem88.Name = "layoutControlItem88";
			layoutControlItem88.Size = new System.Drawing.Size(391, 24);
			layoutControlItem88.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem88.Text = "Grace Days on Overdue:";
			layoutControlItem88.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem89.AppearanceItemCaption.Options.UseTextOptions = true;
			layoutControlItem89.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			layoutControlItem89.Control = pictureBoxPhoto;
			layoutControlItem89.Location = new System.Drawing.Point(0, 77);
			layoutControlItem89.MaxSize = new System.Drawing.Size(271, 72);
			layoutControlItem89.MinSize = new System.Drawing.Size(271, 72);
			layoutControlItem89.Name = "layoutControlItem89";
			layoutControlItem89.Size = new System.Drawing.Size(271, 72);
			layoutControlItem89.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem89.Text = "Signature:";
			layoutControlItem89.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem90.Control = linkAddPicture;
			layoutControlItem90.Location = new System.Drawing.Point(271, 77);
			layoutControlItem90.MaxSize = new System.Drawing.Size(617, 24);
			layoutControlItem90.MinSize = new System.Drawing.Size(617, 24);
			layoutControlItem90.Name = "layoutControlItem90";
			layoutControlItem90.Size = new System.Drawing.Size(617, 24);
			layoutControlItem90.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem90.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem90.TextVisible = false;
			layoutControlItem91.Control = linkRemovePicture;
			layoutControlItem91.Location = new System.Drawing.Point(271, 101);
			layoutControlItem91.MaxSize = new System.Drawing.Size(617, 24);
			layoutControlItem91.MinSize = new System.Drawing.Size(617, 24);
			layoutControlItem91.Name = "layoutControlItem91";
			layoutControlItem91.Size = new System.Drawing.Size(617, 24);
			layoutControlItem91.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem91.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem91.TextVisible = false;
			layoutControlItem92.Control = linkLoadImage;
			layoutControlItem92.Location = new System.Drawing.Point(271, 125);
			layoutControlItem92.MaxSize = new System.Drawing.Size(617, 24);
			layoutControlItem92.MinSize = new System.Drawing.Size(617, 24);
			layoutControlItem92.Name = "layoutControlItem92";
			layoutControlItem92.Size = new System.Drawing.Size(617, 24);
			layoutControlItem92.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem92.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem92.TextVisible = false;
			emptySpaceItem8.AllowHotTrack = false;
			emptySpaceItem8.Location = new System.Drawing.Point(0, 149);
			emptySpaceItem8.MaxSize = new System.Drawing.Size(873, 12);
			emptySpaceItem8.MinSize = new System.Drawing.Size(873, 12);
			emptySpaceItem8.Name = "emptySpaceItem8";
			emptySpaceItem8.Size = new System.Drawing.Size(888, 12);
			emptySpaceItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem76.AppearanceItemCaption.Options.UseTextOptions = true;
			layoutControlItem76.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			layoutControlItem76.Control = textBoxRatingRemarks;
			layoutControlItem76.Location = new System.Drawing.Point(0, 144);
			layoutControlItem76.MaxSize = new System.Drawing.Size(879, 24);
			layoutControlItem76.MinSize = new System.Drawing.Size(879, 24);
			layoutControlItem76.Name = "layoutControlItem76";
			layoutControlItem76.Size = new System.Drawing.Size(894, 24);
			layoutControlItem76.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem76.Text = "Remarks:";
			layoutControlItem76.TextSize = new System.Drawing.Size(164, 13);
			layoutControlGroup9.ExpandButtonVisible = true;
			layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[6]
			{
				layoutControlItem95,
				layoutControlItem93,
				panelInsuranceDetails,
				layoutControlItem94,
				layoutControlItem96,
				emptySpaceItem18
			});
			layoutControlGroup9.Location = new System.Drawing.Point(0, 355);
			layoutControlGroup9.Name = "layoutControlGroup9";
			layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 4, 9);
			layoutControlGroup9.Size = new System.Drawing.Size(894, 244);
			layoutControlGroup9.Text = "Credit Insurance Info";
			layoutControlItem95.Control = comboBoxInsuranceStatus;
			layoutControlItem95.Location = new System.Drawing.Point(0, 0);
			layoutControlItem95.MaxSize = new System.Drawing.Size(425, 25);
			layoutControlItem95.MinSize = new System.Drawing.Size(425, 25);
			layoutControlItem95.Name = "layoutControlItem95";
			layoutControlItem95.Size = new System.Drawing.Size(425, 25);
			layoutControlItem95.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem95.Text = "Insurance Status:";
			layoutControlItem95.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem93.Control = buttonCustomerInsuranceClaim;
			layoutControlItem93.Location = new System.Drawing.Point(425, 0);
			layoutControlItem93.MaxSize = new System.Drawing.Size(212, 25);
			layoutControlItem93.MinSize = new System.Drawing.Size(212, 25);
			layoutControlItem93.Name = "layoutControlItem93";
			layoutControlItem93.Size = new System.Drawing.Size(212, 25);
			layoutControlItem93.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem93.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem93.TextVisible = false;
			panelInsuranceDetails.AppearanceGroup.BorderColor = System.Drawing.Color.Transparent;
			panelInsuranceDetails.AppearanceGroup.Options.UseBorderColor = true;
			panelInsuranceDetails.GroupBordersVisible = false;
			panelInsuranceDetails.GroupStyle = DevExpress.Utils.GroupStyle.Light;
			panelInsuranceDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[10]
			{
				emptySpaceItem16,
				layoutControlItem97,
				layoutControlItem98,
				layoutControlItem99,
				layoutControlItem100,
				layoutControlItem101,
				layoutControlItem102,
				layoutControlItem103,
				layoutControlItem104,
				layoutControlItem105
			});
			panelInsuranceDetails.Location = new System.Drawing.Point(0, 49);
			panelInsuranceDetails.Name = "panelInsuranceDetails";
			panelInsuranceDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 9, 9);
			panelInsuranceDetails.Size = new System.Drawing.Size(888, 158);
			panelInsuranceDetails.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2);
			panelInsuranceDetails.Text = "InsuranceInfo";
			panelInsuranceDetails.TextVisible = false;
			emptySpaceItem16.AllowHotTrack = false;
			emptySpaceItem16.Location = new System.Drawing.Point(707, 0);
			emptySpaceItem16.Name = "emptySpaceItem16";
			emptySpaceItem16.Size = new System.Drawing.Size(181, 48);
			emptySpaceItem16.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem97.Control = dateTimePickerInsuranceDate;
			layoutControlItem97.Location = new System.Drawing.Point(0, 0);
			layoutControlItem97.MaxSize = new System.Drawing.Size(365, 24);
			layoutControlItem97.MinSize = new System.Drawing.Size(365, 24);
			layoutControlItem97.Name = "layoutControlItem97";
			layoutControlItem97.Size = new System.Drawing.Size(365, 24);
			layoutControlItem97.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem97.Text = "Application Date:";
			layoutControlItem97.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem98.Control = textBoxInsuranceNumber;
			layoutControlItem98.Location = new System.Drawing.Point(365, 0);
			layoutControlItem98.MaxSize = new System.Drawing.Size(342, 24);
			layoutControlItem98.MinSize = new System.Drawing.Size(342, 24);
			layoutControlItem98.Name = "layoutControlItem98";
			layoutControlItem98.Size = new System.Drawing.Size(342, 24);
			layoutControlItem98.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem98.Text = "Application Number:";
			layoutControlItem98.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem99.Control = textBoxInsuranceReqAmount;
			layoutControlItem99.Location = new System.Drawing.Point(0, 24);
			layoutControlItem99.MaxSize = new System.Drawing.Size(365, 24);
			layoutControlItem99.MinSize = new System.Drawing.Size(365, 24);
			layoutControlItem99.Name = "layoutControlItem99";
			layoutControlItem99.Size = new System.Drawing.Size(365, 24);
			layoutControlItem99.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem99.Text = "Requested Amount:";
			layoutControlItem99.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem100.Control = textBoxInsuranceApprovedAmount;
			layoutControlItem100.Location = new System.Drawing.Point(365, 24);
			layoutControlItem100.MaxSize = new System.Drawing.Size(342, 24);
			layoutControlItem100.MinSize = new System.Drawing.Size(342, 24);
			layoutControlItem100.Name = "layoutControlItem100";
			layoutControlItem100.Size = new System.Drawing.Size(342, 24);
			layoutControlItem100.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem100.Text = "Approved Amount:";
			layoutControlItem100.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem101.Control = comboBoxInsuranceRating;
			layoutControlItem101.Location = new System.Drawing.Point(0, 48);
			layoutControlItem101.MaxSize = new System.Drawing.Size(365, 24);
			layoutControlItem101.MinSize = new System.Drawing.Size(365, 24);
			layoutControlItem101.Name = "layoutControlItem101";
			layoutControlItem101.Size = new System.Drawing.Size(365, 24);
			layoutControlItem101.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem101.Text = "Rating:";
			layoutControlItem101.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem102.Control = textBoxInsuranceID;
			layoutControlItem102.Location = new System.Drawing.Point(365, 48);
			layoutControlItem102.MaxSize = new System.Drawing.Size(504, 24);
			layoutControlItem102.MinSize = new System.Drawing.Size(504, 24);
			layoutControlItem102.Name = "layoutControlItem102";
			layoutControlItem102.Size = new System.Drawing.Size(523, 24);
			layoutControlItem102.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem102.Text = "Insurance ID:";
			layoutControlItem102.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem103.Control = datetimePickerEffectiveDate;
			layoutControlItem103.Location = new System.Drawing.Point(0, 72);
			layoutControlItem103.MaxSize = new System.Drawing.Size(365, 24);
			layoutControlItem103.MinSize = new System.Drawing.Size(365, 24);
			layoutControlItem103.Name = "layoutControlItem103";
			layoutControlItem103.Size = new System.Drawing.Size(365, 24);
			layoutControlItem103.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem103.Text = "Effective Date:";
			layoutControlItem103.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem104.Control = dateTimePickerValidTo;
			layoutControlItem104.Location = new System.Drawing.Point(365, 72);
			layoutControlItem104.MaxSize = new System.Drawing.Size(504, 24);
			layoutControlItem104.MinSize = new System.Drawing.Size(504, 24);
			layoutControlItem104.Name = "layoutControlItem104";
			layoutControlItem104.Size = new System.Drawing.Size(523, 24);
			layoutControlItem104.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem104.Text = "Valid To:";
			layoutControlItem104.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem105.AppearanceItemCaption.Options.UseTextOptions = true;
			layoutControlItem105.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			layoutControlItem105.Control = textBoxInsuranceRemarks;
			layoutControlItem105.Location = new System.Drawing.Point(0, 96);
			layoutControlItem105.MaxSize = new System.Drawing.Size(869, 46);
			layoutControlItem105.MinSize = new System.Drawing.Size(869, 46);
			layoutControlItem105.Name = "layoutControlItem105";
			layoutControlItem105.Size = new System.Drawing.Size(888, 62);
			layoutControlItem105.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem105.Text = "Remarks:";
			layoutControlItem105.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem94.Control = comboBoxInsuranceProvider;
			layoutControlItem94.Location = new System.Drawing.Point(0, 25);
			layoutControlItem94.MaxSize = new System.Drawing.Size(425, 24);
			layoutControlItem94.MinSize = new System.Drawing.Size(425, 24);
			layoutControlItem94.Name = "layoutControlItem94";
			layoutControlItem94.Size = new System.Drawing.Size(425, 24);
			layoutControlItem94.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem94.Text = "Provider:";
			layoutControlItem94.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem96.Control = textBoxProvider;
			layoutControlItem96.Location = new System.Drawing.Point(425, 25);
			layoutControlItem96.Name = "layoutControlItem96";
			layoutControlItem96.Size = new System.Drawing.Size(463, 24);
			layoutControlItem96.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem96.TextVisible = false;
			emptySpaceItem18.AllowHotTrack = false;
			emptySpaceItem18.Location = new System.Drawing.Point(637, 0);
			emptySpaceItem18.MaxSize = new System.Drawing.Size(216, 25);
			emptySpaceItem18.MinSize = new System.Drawing.Size(216, 25);
			emptySpaceItem18.Name = "emptySpaceItem18";
			emptySpaceItem18.Size = new System.Drawing.Size(251, 25);
			emptySpaceItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem18.TextSize = new System.Drawing.Size(0, 0);
			tabPageGeneral.CustomizationFormText = "General";
			tabPageGeneral.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[19]
			{
				layoutControlItem1,
				layoutControlItem2,
				layoutControlItem3,
				layoutControlItem4,
				layoutControlItem5,
				layoutControlItem6,
				layoutControlItem7,
				layoutControlItem8,
				layoutControlItem9,
				layoutControlItem10,
				layoutControlItem11,
				layoutControlItem12,
				layoutControlItem13,
				layoutControlItem14,
				emptySpaceItem2,
				emptySpaceItem3,
				layoutControlItem15,
				layoutControlGroup4,
				emptySpaceItem5
			});
			tabPageGeneral.Location = new System.Drawing.Point(0, 0);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(894, 599);
			tabPageGeneral.Text = "&General";
			layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
			layoutControlItem1.Control = textBoxCode;
			layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			layoutControlItem1.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem1.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new System.Drawing.Size(439, 24);
			layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem1.Text = "Customer Code:";
			layoutControlItem1.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
			layoutControlItem2.Control = textBoxName;
			layoutControlItem2.Location = new System.Drawing.Point(0, 24);
			layoutControlItem2.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem2.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new System.Drawing.Size(439, 24);
			layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem2.Text = "Customer Name:";
			layoutControlItem2.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem3.Control = textBoxFormalName;
			layoutControlItem3.Location = new System.Drawing.Point(0, 48);
			layoutControlItem3.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem3.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new System.Drawing.Size(439, 24);
			layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem3.Text = "Short Name:";
			layoutControlItem3.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem4.Control = textBoxForeignName;
			layoutControlItem4.Location = new System.Drawing.Point(0, 72);
			layoutControlItem4.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem4.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new System.Drawing.Size(439, 24);
			layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem4.Text = "Foreign Name:";
			layoutControlItem4.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem5.Control = comboBoxParentCustomer;
			layoutControlItem5.Location = new System.Drawing.Point(0, 96);
			layoutControlItem5.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem5.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new System.Drawing.Size(439, 24);
			layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem5.Text = "Parent Customer:";
			layoutControlItem5.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem6.Control = checkBoxparentACforposting;
			layoutControlItem6.Location = new System.Drawing.Point(0, 120);
			layoutControlItem6.MaxSize = new System.Drawing.Size(439, 24);
			layoutControlItem6.MinSize = new System.Drawing.Size(439, 24);
			layoutControlItem6.Name = "layoutControlItem6";
			layoutControlItem6.Size = new System.Drawing.Size(439, 24);
			layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem6.TextVisible = false;
			layoutControlItem7.Control = checkBoxIsInactive;
			layoutControlItem7.Location = new System.Drawing.Point(439, 0);
			layoutControlItem7.MaxSize = new System.Drawing.Size(98, 24);
			layoutControlItem7.MinSize = new System.Drawing.Size(98, 24);
			layoutControlItem7.Name = "layoutControlItem7";
			layoutControlItem7.Size = new System.Drawing.Size(98, 24);
			layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem7.TextVisible = false;
			layoutControlItem8.Control = checkBoxHold;
			layoutControlItem8.Location = new System.Drawing.Point(537, 0);
			layoutControlItem8.MaxSize = new System.Drawing.Size(342, 24);
			layoutControlItem8.MinSize = new System.Drawing.Size(342, 24);
			layoutControlItem8.Name = "layoutControlItem8";
			layoutControlItem8.Size = new System.Drawing.Size(357, 24);
			layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem8.TextVisible = false;
			layoutControlItem9.AllowHtmlStringInCaption = true;
			layoutControlItem9.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem9.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem9.Control = comboBoxCustomerClass;
			layoutControlItem9.Location = new System.Drawing.Point(439, 24);
			layoutControlItem9.MaxSize = new System.Drawing.Size(455, 0);
			layoutControlItem9.MinSize = new System.Drawing.Size(455, 24);
			layoutControlItem9.Name = "layoutControlItem9";
			layoutControlItem9.Size = new System.Drawing.Size(455, 24);
			layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem9.Text = "Customer Class:";
			layoutControlItem9.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem9.Click += new System.EventHandler(layoutControlItem9_Click);
			layoutControlItem10.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem10.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem10.Control = comboBoxCustomerGroup;
			layoutControlItem10.Location = new System.Drawing.Point(439, 48);
			layoutControlItem10.MaxSize = new System.Drawing.Size(455, 0);
			layoutControlItem10.MinSize = new System.Drawing.Size(455, 24);
			layoutControlItem10.Name = "layoutControlItem10";
			layoutControlItem10.Size = new System.Drawing.Size(455, 24);
			layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem10.Text = "Customer Group:";
			layoutControlItem10.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem10.Click += new System.EventHandler(layoutControlItem10_Click);
			layoutControlItem11.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem11.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem11.Control = comboBoxCountry;
			layoutControlItem11.Location = new System.Drawing.Point(439, 72);
			layoutControlItem11.MaxSize = new System.Drawing.Size(455, 0);
			layoutControlItem11.MinSize = new System.Drawing.Size(455, 24);
			layoutControlItem11.Name = "layoutControlItem11";
			layoutControlItem11.Size = new System.Drawing.Size(455, 24);
			layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem11.Text = "Country:";
			layoutControlItem11.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem11.Click += new System.EventHandler(layoutControlItem11_Click);
			layoutControlItem12.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem12.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem12.Control = comboBoxArea;
			layoutControlItem12.Location = new System.Drawing.Point(439, 96);
			layoutControlItem12.MaxSize = new System.Drawing.Size(455, 0);
			layoutControlItem12.MinSize = new System.Drawing.Size(455, 24);
			layoutControlItem12.Name = "layoutControlItem12";
			layoutControlItem12.Size = new System.Drawing.Size(455, 24);
			layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem12.Text = "Area:";
			layoutControlItem12.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem12.Click += new System.EventHandler(layoutControlItem12_Click);
			layoutControlItem13.Control = comboBoxPriceLevel;
			layoutControlItem13.Location = new System.Drawing.Point(439, 120);
			layoutControlItem13.MaxSize = new System.Drawing.Size(455, 0);
			layoutControlItem13.MinSize = new System.Drawing.Size(455, 24);
			layoutControlItem13.Name = "layoutControlItem13";
			layoutControlItem13.Size = new System.Drawing.Size(455, 24);
			layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem13.Text = "Price Level:";
			layoutControlItem13.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem14.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem14.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem14.Control = comboBoxCurrency;
			layoutControlItem14.Location = new System.Drawing.Point(439, 144);
			layoutControlItem14.MaxSize = new System.Drawing.Size(455, 0);
			layoutControlItem14.MinSize = new System.Drawing.Size(455, 24);
			layoutControlItem14.Name = "layoutControlItem14";
			layoutControlItem14.Size = new System.Drawing.Size(455, 24);
			layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem14.Text = "Currency:";
			layoutControlItem14.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem14.Click += new System.EventHandler(layoutControlItem14_Click);
			emptySpaceItem2.AllowHotTrack = false;
			emptySpaceItem2.Location = new System.Drawing.Point(0, 144);
			emptySpaceItem2.MaxSize = new System.Drawing.Size(439, 24);
			emptySpaceItem2.MinSize = new System.Drawing.Size(439, 24);
			emptySpaceItem2.Name = "emptySpaceItem2";
			emptySpaceItem2.Size = new System.Drawing.Size(439, 24);
			emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			emptySpaceItem3.AllowHotTrack = false;
			emptySpaceItem3.Location = new System.Drawing.Point(0, 168);
			emptySpaceItem3.MaxSize = new System.Drawing.Size(719, 31);
			emptySpaceItem3.MinSize = new System.Drawing.Size(719, 31);
			emptySpaceItem3.Name = "emptySpaceItem3";
			emptySpaceItem3.Size = new System.Drawing.Size(719, 31);
			emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem15.Control = buttonCategories;
			layoutControlItem15.Location = new System.Drawing.Point(719, 168);
			layoutControlItem15.MaxSize = new System.Drawing.Size(175, 31);
			layoutControlItem15.MinSize = new System.Drawing.Size(175, 31);
			layoutControlItem15.Name = "layoutControlItem15";
			layoutControlItem15.Size = new System.Drawing.Size(175, 31);
			layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem15.TextVisible = false;
			layoutControlGroup4.AppearanceGroup.BackColor = System.Drawing.Color.White;
			layoutControlGroup4.AppearanceGroup.BackColor2 = System.Drawing.Color.White;
			layoutControlGroup4.AppearanceGroup.Options.UseBackColor = true;
			layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			layoutControlGroup4.ExpandButtonVisible = true;
			layoutControlGroup4.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[25]
			{
				layoutControlItem16,
				layoutControlItem17,
				layoutControlItem18,
				layoutControlItem26,
				layoutControlItem27,
				layoutControlItem19,
				layoutControlItem20,
				layoutControlItem21,
				layoutControlItem22,
				layoutControlItem23,
				layoutControlItem24,
				layoutControlItem28,
				layoutControlItem29,
				layoutControlItem30,
				layoutControlItem31,
				layoutControlItem32,
				layoutControlItem34,
				layoutControlItem35,
				layoutControlItem36,
				layoutControlItem33,
				layoutControlItem37,
				layoutControlItem25,
				emptySpaceItem4,
				emptySpaceItem6,
				emptySpaceItem7
			});
			layoutControlGroup4.Location = new System.Drawing.Point(0, 209);
			layoutControlGroup4.Name = "layoutControlGroup4";
			layoutControlGroup4.Size = new System.Drawing.Size(894, 390);
			layoutControlGroup4.Text = "Primary Address";
			layoutControlItem16.Control = textBoxAddressID;
			layoutControlItem16.Location = new System.Drawing.Point(0, 0);
			layoutControlItem16.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem16.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem16.Name = "layoutControlItem16";
			layoutControlItem16.Size = new System.Drawing.Size(423, 24);
			layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem16.Text = "Address ID:";
			layoutControlItem16.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem17.Control = textBoxContactName;
			layoutControlItem17.Location = new System.Drawing.Point(0, 24);
			layoutControlItem17.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem17.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem17.Name = "layoutControlItem17";
			layoutControlItem17.Size = new System.Drawing.Size(423, 24);
			layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem17.Text = "Contact Name:";
			layoutControlItem17.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem18.Control = textBoxAddress1;
			layoutControlItem18.Location = new System.Drawing.Point(0, 48);
			layoutControlItem18.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem18.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem18.Name = "layoutControlItem18";
			layoutControlItem18.Size = new System.Drawing.Size(423, 24);
			layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem18.Text = "Adress:";
			layoutControlItem18.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem26.Control = textBoxDepartment;
			layoutControlItem26.Location = new System.Drawing.Point(423, 0);
			layoutControlItem26.MaxSize = new System.Drawing.Size(447, 24);
			layoutControlItem26.MinSize = new System.Drawing.Size(447, 24);
			layoutControlItem26.Name = "layoutControlItem26";
			layoutControlItem26.Size = new System.Drawing.Size(447, 24);
			layoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem26.Text = "Department:";
			layoutControlItem26.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem27.Control = textBoxPhone1;
			layoutControlItem27.Location = new System.Drawing.Point(423, 24);
			layoutControlItem27.MaxSize = new System.Drawing.Size(447, 24);
			layoutControlItem27.MinSize = new System.Drawing.Size(447, 24);
			layoutControlItem27.Name = "layoutControlItem27";
			layoutControlItem27.Size = new System.Drawing.Size(447, 24);
			layoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem27.Text = "Phone 1:";
			layoutControlItem27.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem19.Control = textBoxAddress2;
			layoutControlItem19.Location = new System.Drawing.Point(0, 72);
			layoutControlItem19.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem19.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem19.Name = "layoutControlItem19";
			layoutControlItem19.Size = new System.Drawing.Size(423, 24);
			layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem19.Text = " ";
			layoutControlItem19.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem20.Control = textBoxAddress3;
			layoutControlItem20.Location = new System.Drawing.Point(0, 96);
			layoutControlItem20.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem20.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem20.Name = "layoutControlItem20";
			layoutControlItem20.Size = new System.Drawing.Size(423, 24);
			layoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem20.Text = " ";
			layoutControlItem20.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem21.Control = textBoxCity;
			layoutControlItem21.Location = new System.Drawing.Point(0, 120);
			layoutControlItem21.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem21.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem21.Name = "layoutControlItem21";
			layoutControlItem21.Size = new System.Drawing.Size(423, 24);
			layoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem21.Text = "City:";
			layoutControlItem21.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem22.Control = textBoxState;
			layoutControlItem22.Location = new System.Drawing.Point(0, 144);
			layoutControlItem22.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem22.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem22.Name = "layoutControlItem22";
			layoutControlItem22.Size = new System.Drawing.Size(423, 24);
			layoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem22.Text = "State:";
			layoutControlItem22.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem23.Control = textBoxCountry;
			layoutControlItem23.Location = new System.Drawing.Point(0, 168);
			layoutControlItem23.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem23.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem23.Name = "layoutControlItem23";
			layoutControlItem23.Size = new System.Drawing.Size(423, 24);
			layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem23.Text = "Country:";
			layoutControlItem23.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem24.Control = textBoxPostalCode;
			layoutControlItem24.Location = new System.Drawing.Point(0, 192);
			layoutControlItem24.MaxSize = new System.Drawing.Size(423, 24);
			layoutControlItem24.MinSize = new System.Drawing.Size(423, 24);
			layoutControlItem24.Name = "layoutControlItem24";
			layoutControlItem24.Size = new System.Drawing.Size(423, 24);
			layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem24.Text = "Postal Code:";
			layoutControlItem24.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem28.Control = textBoxPhone2;
			layoutControlItem28.Location = new System.Drawing.Point(423, 48);
			layoutControlItem28.MaxSize = new System.Drawing.Size(447, 24);
			layoutControlItem28.MinSize = new System.Drawing.Size(447, 24);
			layoutControlItem28.Name = "layoutControlItem28";
			layoutControlItem28.Size = new System.Drawing.Size(447, 24);
			layoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem28.Text = "Phone 2:";
			layoutControlItem28.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem29.Control = textBoxFax;
			layoutControlItem29.Location = new System.Drawing.Point(423, 72);
			layoutControlItem29.MaxSize = new System.Drawing.Size(447, 24);
			layoutControlItem29.MinSize = new System.Drawing.Size(447, 24);
			layoutControlItem29.Name = "layoutControlItem29";
			layoutControlItem29.Size = new System.Drawing.Size(447, 24);
			layoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem29.Text = "Fax:";
			layoutControlItem29.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem30.Control = textBoxMobile;
			layoutControlItem30.Location = new System.Drawing.Point(423, 96);
			layoutControlItem30.MaxSize = new System.Drawing.Size(447, 24);
			layoutControlItem30.MinSize = new System.Drawing.Size(447, 24);
			layoutControlItem30.Name = "layoutControlItem30";
			layoutControlItem30.Size = new System.Drawing.Size(447, 24);
			layoutControlItem30.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem30.Text = "Mobile:";
			layoutControlItem30.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem31.Control = textBoxEmail;
			layoutControlItem31.Location = new System.Drawing.Point(423, 120);
			layoutControlItem31.MaxSize = new System.Drawing.Size(447, 24);
			layoutControlItem31.MinSize = new System.Drawing.Size(447, 24);
			layoutControlItem31.Name = "layoutControlItem31";
			layoutControlItem31.Size = new System.Drawing.Size(447, 24);
			layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem31.Text = "Email:";
			layoutControlItem31.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem32.Control = textBoxWebsite;
			layoutControlItem32.Location = new System.Drawing.Point(423, 144);
			layoutControlItem32.MaxSize = new System.Drawing.Size(447, 24);
			layoutControlItem32.MinSize = new System.Drawing.Size(447, 24);
			layoutControlItem32.Name = "layoutControlItem32";
			layoutControlItem32.Size = new System.Drawing.Size(447, 24);
			layoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem32.Text = "Website:";
			layoutControlItem32.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem34.Control = textBoxLatitude;
			layoutControlItem34.Location = new System.Drawing.Point(423, 168);
			layoutControlItem34.MaxSize = new System.Drawing.Size(327, 24);
			layoutControlItem34.MinSize = new System.Drawing.Size(327, 24);
			layoutControlItem34.Name = "layoutControlItem34";
			layoutControlItem34.Size = new System.Drawing.Size(327, 24);
			layoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem34.Text = "Latitude:";
			layoutControlItem34.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem35.Control = textBoxLongitude;
			layoutControlItem35.Location = new System.Drawing.Point(423, 192);
			layoutControlItem35.MaxSize = new System.Drawing.Size(327, 24);
			layoutControlItem35.MinSize = new System.Drawing.Size(327, 24);
			layoutControlItem35.Name = "layoutControlItem35";
			layoutControlItem35.Size = new System.Drawing.Size(327, 24);
			layoutControlItem35.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem35.Text = "Longitude:";
			layoutControlItem35.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem36.Control = ultraPictureBox1;
			layoutControlItem36.Location = new System.Drawing.Point(750, 168);
			layoutControlItem36.MaxSize = new System.Drawing.Size(120, 48);
			layoutControlItem36.MinSize = new System.Drawing.Size(120, 48);
			layoutControlItem36.Name = "layoutControlItem36";
			layoutControlItem36.Size = new System.Drawing.Size(120, 48);
			layoutControlItem36.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem36.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem36.TextVisible = false;
			layoutControlItem33.Control = textBoxComment;
			layoutControlItem33.Location = new System.Drawing.Point(0, 216);
			layoutControlItem33.Name = "layoutControlItem33";
			layoutControlItem33.Size = new System.Drawing.Size(423, 24);
			layoutControlItem33.Text = "Comment:";
			layoutControlItem33.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem37.Control = buttonMoreAddress;
			layoutControlItem37.Location = new System.Drawing.Point(708, 216);
			layoutControlItem37.MaxSize = new System.Drawing.Size(161, 42);
			layoutControlItem37.MinSize = new System.Drawing.Size(161, 42);
			layoutControlItem37.Name = "layoutControlItem37";
			layoutControlItem37.Size = new System.Drawing.Size(162, 42);
			layoutControlItem37.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem37.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem37.TextVisible = false;
			layoutControlItem25.AppearanceItemCaption.Options.UseTextOptions = true;
			layoutControlItem25.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			layoutControlItem25.Control = textBoxAddressPrintFormat;
			layoutControlItem25.Location = new System.Drawing.Point(0, 240);
			layoutControlItem25.MaxSize = new System.Drawing.Size(423, 98);
			layoutControlItem25.MinSize = new System.Drawing.Size(423, 98);
			layoutControlItem25.Name = "layoutControlItem25";
			layoutControlItem25.Size = new System.Drawing.Size(423, 98);
			layoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem25.Text = "Print Format:";
			layoutControlItem25.TextSize = new System.Drawing.Size(164, 13);
			emptySpaceItem4.AllowHotTrack = false;
			emptySpaceItem4.Location = new System.Drawing.Point(708, 258);
			emptySpaceItem4.MaxSize = new System.Drawing.Size(151, 80);
			emptySpaceItem4.MinSize = new System.Drawing.Size(151, 80);
			emptySpaceItem4.Name = "emptySpaceItem4";
			emptySpaceItem4.Size = new System.Drawing.Size(162, 80);
			emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
			emptySpaceItem6.AllowHotTrack = false;
			emptySpaceItem6.Location = new System.Drawing.Point(423, 216);
			emptySpaceItem6.MaxSize = new System.Drawing.Size(285, 122);
			emptySpaceItem6.MinSize = new System.Drawing.Size(285, 122);
			emptySpaceItem6.Name = "emptySpaceItem6";
			emptySpaceItem6.Size = new System.Drawing.Size(285, 122);
			emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
			emptySpaceItem7.AllowHotTrack = false;
			emptySpaceItem7.Location = new System.Drawing.Point(0, 338);
			emptySpaceItem7.Name = "emptySpaceItem7";
			emptySpaceItem7.Size = new System.Drawing.Size(870, 10);
			emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
			emptySpaceItem5.AllowHotTrack = false;
			emptySpaceItem5.Location = new System.Drawing.Point(0, 199);
			emptySpaceItem5.MaxSize = new System.Drawing.Size(879, 10);
			emptySpaceItem5.MinSize = new System.Drawing.Size(879, 10);
			emptySpaceItem5.Name = "emptySpaceItem5";
			emptySpaceItem5.Size = new System.Drawing.Size(894, 10);
			emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
			tabPageDetails.CustomizationFormText = "Details Tab";
			tabPageDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[25]
			{
				layoutControlItem38,
				layoutControlItem39,
				layoutControlItem40,
				layoutControlItem41,
				layoutControlItem42,
				layoutControlItem43,
				layoutControlItem44,
				layoutControlItem45,
				layoutControlItem46,
				emptySpaceItem9,
				emptySpaceItem10,
				layoutControlItem47,
				layoutControlItem50,
				layoutControlItem51,
				layoutControlItem52,
				emptySpaceItem11,
				layoutControlItem53,
				emptySpaceItem12,
				layoutControlGroup5,
				layoutControlGroup6,
				layoutItemConsignmentCom,
				layoutControlItem48,
				emptySpaceItem15,
				layoutControlGroup7,
				layoutControlItem54
			});
			tabPageDetails.Location = new System.Drawing.Point(0, 0);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(894, 599);
			tabPageDetails.Text = "&Details";
			layoutControlItem38.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem38.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem38.Control = comboBoxSalesperson;
			layoutControlItem38.Location = new System.Drawing.Point(0, 0);
			layoutControlItem38.MaxSize = new System.Drawing.Size(398, 24);
			layoutControlItem38.MinSize = new System.Drawing.Size(398, 24);
			layoutControlItem38.Name = "layoutControlItem38";
			layoutControlItem38.Size = new System.Drawing.Size(398, 24);
			layoutControlItem38.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem38.Text = "Salesperson:";
			layoutControlItem38.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem38.Click += new System.EventHandler(layoutControlItem38_Click);
			layoutControlItem39.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem39.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem39.Control = comboBoxShiptoAddress;
			layoutControlItem39.Location = new System.Drawing.Point(0, 24);
			layoutControlItem39.MaxSize = new System.Drawing.Size(398, 24);
			layoutControlItem39.MinSize = new System.Drawing.Size(398, 24);
			layoutControlItem39.Name = "layoutControlItem39";
			layoutControlItem39.Size = new System.Drawing.Size(398, 24);
			layoutControlItem39.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem39.Text = "Ship to Address:";
			layoutControlItem39.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem39.Click += new System.EventHandler(layoutControlItem39_Click);
			layoutControlItem40.Control = dateTimePickerCustomerSince;
			layoutControlItem40.CustomizationFormText = "Customer Since";
			layoutControlItem40.Location = new System.Drawing.Point(0, 48);
			layoutControlItem40.MaxSize = new System.Drawing.Size(398, 24);
			layoutControlItem40.MinSize = new System.Drawing.Size(398, 24);
			layoutControlItem40.Name = "layoutControlItem40";
			layoutControlItem40.Size = new System.Drawing.Size(398, 24);
			layoutControlItem40.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem40.Text = "Customer Since:";
			layoutControlItem40.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem41.Control = comboBoxLeadSource;
			layoutControlItem41.Location = new System.Drawing.Point(0, 72);
			layoutControlItem41.MaxSize = new System.Drawing.Size(398, 25);
			layoutControlItem41.MinSize = new System.Drawing.Size(398, 25);
			layoutControlItem41.Name = "layoutControlItem41";
			layoutControlItem41.Size = new System.Drawing.Size(398, 25);
			layoutControlItem41.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem41.Text = "Lead Source:";
			layoutControlItem41.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem41.Click += new System.EventHandler(layoutControlItem41_Click);
			layoutControlItem42.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem42.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem42.Control = comboBoxBilltoAddress;
			layoutControlItem42.Location = new System.Drawing.Point(398, 0);
			layoutControlItem42.MaxSize = new System.Drawing.Size(467, 24);
			layoutControlItem42.MinSize = new System.Drawing.Size(467, 24);
			layoutControlItem42.Name = "layoutControlItem42";
			layoutControlItem42.Size = new System.Drawing.Size(496, 24);
			layoutControlItem42.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem42.Text = "Bill to Address:";
			layoutControlItem42.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem42.Click += new System.EventHandler(layoutControlItem42_Click);
			layoutControlItem43.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem43.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem43.Control = comboBoxShippingMethods;
			layoutControlItem43.Location = new System.Drawing.Point(398, 24);
			layoutControlItem43.MaxSize = new System.Drawing.Size(467, 24);
			layoutControlItem43.MinSize = new System.Drawing.Size(467, 24);
			layoutControlItem43.Name = "layoutControlItem43";
			layoutControlItem43.Size = new System.Drawing.Size(496, 24);
			layoutControlItem43.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem43.Text = "Shipping Method:";
			layoutControlItem43.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem43.Click += new System.EventHandler(layoutControlItem43_Click);
			layoutControlItem44.Control = dateTimePickerEstablished;
			layoutControlItem44.Location = new System.Drawing.Point(398, 48);
			layoutControlItem44.MaxSize = new System.Drawing.Size(467, 24);
			layoutControlItem44.MinSize = new System.Drawing.Size(467, 24);
			layoutControlItem44.Name = "layoutControlItem44";
			layoutControlItem44.Size = new System.Drawing.Size(496, 24);
			layoutControlItem44.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem44.Text = "Date Established:";
			layoutControlItem44.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem45.Control = comboBoxStatementMethod;
			layoutControlItem45.Location = new System.Drawing.Point(398, 72);
			layoutControlItem45.MaxSize = new System.Drawing.Size(467, 25);
			layoutControlItem45.MinSize = new System.Drawing.Size(467, 25);
			layoutControlItem45.Name = "layoutControlItem45";
			layoutControlItem45.Size = new System.Drawing.Size(496, 25);
			layoutControlItem45.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem45.Text = "Statement Method:";
			layoutControlItem45.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem46.Control = textBoxStatementEmail;
			layoutControlItem46.Location = new System.Drawing.Point(0, 97);
			layoutControlItem46.MaxSize = new System.Drawing.Size(398, 24);
			layoutControlItem46.MinSize = new System.Drawing.Size(398, 24);
			layoutControlItem46.Name = "layoutControlItem46";
			layoutControlItem46.Size = new System.Drawing.Size(398, 24);
			layoutControlItem46.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem46.Text = "Statement Email:";
			layoutControlItem46.TextSize = new System.Drawing.Size(164, 13);
			emptySpaceItem9.AllowHotTrack = false;
			emptySpaceItem9.Location = new System.Drawing.Point(398, 97);
			emptySpaceItem9.MaxSize = new System.Drawing.Size(468, 24);
			emptySpaceItem9.MinSize = new System.Drawing.Size(468, 24);
			emptySpaceItem9.Name = "emptySpaceItem9";
			emptySpaceItem9.Size = new System.Drawing.Size(496, 24);
			emptySpaceItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
			emptySpaceItem10.AllowHotTrack = false;
			emptySpaceItem10.Location = new System.Drawing.Point(0, 121);
			emptySpaceItem10.MaxSize = new System.Drawing.Size(863, 14);
			emptySpaceItem10.MinSize = new System.Drawing.Size(863, 14);
			emptySpaceItem10.Name = "emptySpaceItem10";
			emptySpaceItem10.Size = new System.Drawing.Size(894, 14);
			emptySpaceItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem47.Control = checkBoxWeightInvoice;
			layoutControlItem47.Location = new System.Drawing.Point(0, 135);
			layoutControlItem47.MaxSize = new System.Drawing.Size(169, 24);
			layoutControlItem47.MinSize = new System.Drawing.Size(169, 24);
			layoutControlItem47.Name = "layoutControlItem47";
			layoutControlItem47.Size = new System.Drawing.Size(169, 24);
			layoutControlItem47.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem47.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem47.TextVisible = false;
			layoutControlItem50.Control = textBoxLicenseNumber;
			layoutControlItem50.Location = new System.Drawing.Point(0, 159);
			layoutControlItem50.MaxSize = new System.Drawing.Size(383, 24);
			layoutControlItem50.MinSize = new System.Drawing.Size(383, 24);
			layoutControlItem50.Name = "layoutControlItem50";
			layoutControlItem50.Size = new System.Drawing.Size(383, 24);
			layoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem50.Text = "Trade Licence No:";
			layoutControlItem50.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem51.Control = dateTimePickerLicenseExpDate;
			layoutControlItem51.Location = new System.Drawing.Point(383, 159);
			layoutControlItem51.MaxSize = new System.Drawing.Size(483, 24);
			layoutControlItem51.MinSize = new System.Drawing.Size(483, 24);
			layoutControlItem51.Name = "layoutControlItem51";
			layoutControlItem51.Size = new System.Drawing.Size(511, 24);
			layoutControlItem51.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem51.Text = "Expiry Date:";
			layoutControlItem51.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem52.Control = dateTimePickerContractExpDate;
			layoutControlItem52.Location = new System.Drawing.Point(0, 183);
			layoutControlItem52.MaxSize = new System.Drawing.Size(383, 24);
			layoutControlItem52.MinSize = new System.Drawing.Size(383, 24);
			layoutControlItem52.Name = "layoutControlItem52";
			layoutControlItem52.Size = new System.Drawing.Size(383, 24);
			layoutControlItem52.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem52.Text = "Contract Expiry Date:";
			layoutControlItem52.TextSize = new System.Drawing.Size(164, 13);
			emptySpaceItem11.AllowHotTrack = false;
			emptySpaceItem11.Location = new System.Drawing.Point(383, 183);
			emptySpaceItem11.MaxSize = new System.Drawing.Size(483, 24);
			emptySpaceItem11.MinSize = new System.Drawing.Size(483, 24);
			emptySpaceItem11.Name = "emptySpaceItem11";
			emptySpaceItem11.Size = new System.Drawing.Size(511, 24);
			emptySpaceItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem53.Control = textBoxDiscountPercent;
			layoutControlItem53.Location = new System.Drawing.Point(0, 207);
			layoutControlItem53.MaxSize = new System.Drawing.Size(274, 24);
			layoutControlItem53.MinSize = new System.Drawing.Size(274, 24);
			layoutControlItem53.Name = "layoutControlItem53";
			layoutControlItem53.Size = new System.Drawing.Size(274, 24);
			layoutControlItem53.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem53.Text = "Discount %:";
			layoutControlItem53.TextSize = new System.Drawing.Size(164, 13);
			emptySpaceItem12.AllowHotTrack = false;
			emptySpaceItem12.Location = new System.Drawing.Point(456, 207);
			emptySpaceItem12.MaxSize = new System.Drawing.Size(411, 24);
			emptySpaceItem12.MinSize = new System.Drawing.Size(411, 24);
			emptySpaceItem12.Name = "emptySpaceItem12";
			emptySpaceItem12.Size = new System.Drawing.Size(438, 24);
			emptySpaceItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem12.TextSize = new System.Drawing.Size(0, 0);
			layoutControlGroup5.ExpandButtonVisible = true;
			layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3]
			{
				layoutControlItem55,
				layoutControlItem56,
				layoutControlItem57
			});
			layoutControlGroup5.Location = new System.Drawing.Point(0, 231);
			layoutControlGroup5.Name = "layoutControlGroup5";
			layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 9, 9, 9);
			layoutControlGroup5.Size = new System.Drawing.Size(450, 115);
			layoutControlGroup5.Text = "Bank Info";
			layoutControlItem55.Control = textBoxBankName;
			layoutControlItem55.Location = new System.Drawing.Point(0, 0);
			layoutControlItem55.MaxSize = new System.Drawing.Size(435, 24);
			layoutControlItem55.MinSize = new System.Drawing.Size(435, 24);
			layoutControlItem55.Name = "layoutControlItem55";
			layoutControlItem55.Size = new System.Drawing.Size(435, 24);
			layoutControlItem55.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem55.Text = "Bank Name:";
			layoutControlItem55.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem56.Control = textBoxBankAccountNumber;
			layoutControlItem56.Location = new System.Drawing.Point(0, 24);
			layoutControlItem56.MaxSize = new System.Drawing.Size(435, 24);
			layoutControlItem56.MinSize = new System.Drawing.Size(435, 24);
			layoutControlItem56.Name = "layoutControlItem56";
			layoutControlItem56.Size = new System.Drawing.Size(435, 24);
			layoutControlItem56.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem56.Text = "Bank Account No:";
			layoutControlItem56.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem57.Control = textBoxBankBranch;
			layoutControlItem57.Location = new System.Drawing.Point(0, 48);
			layoutControlItem57.MaxSize = new System.Drawing.Size(435, 25);
			layoutControlItem57.MinSize = new System.Drawing.Size(435, 25);
			layoutControlItem57.Name = "layoutControlItem57";
			layoutControlItem57.Size = new System.Drawing.Size(435, 25);
			layoutControlItem57.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem57.Text = "Branch:";
			layoutControlItem57.TextSize = new System.Drawing.Size(164, 13);
			layoutControlGroup6.ExpandButtonVisible = true;
			layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3]
			{
				layoutControlItem58,
				layoutControlItem59,
				layoutControlItem60
			});
			layoutControlGroup6.Location = new System.Drawing.Point(450, 231);
			layoutControlGroup6.Name = "layoutControlGroup6";
			layoutControlGroup6.Size = new System.Drawing.Size(444, 115);
			layoutControlGroup6.Text = "Tax Details";
			layoutControlItem58.Control = comboBoxTaxOption;
			layoutControlItem58.Location = new System.Drawing.Point(0, 0);
			layoutControlItem58.MaxSize = new System.Drawing.Size(405, 25);
			layoutControlItem58.MinSize = new System.Drawing.Size(405, 25);
			layoutControlItem58.Name = "layoutControlItem58";
			layoutControlItem58.Size = new System.Drawing.Size(420, 25);
			layoutControlItem58.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem58.Text = "Tax Option:";
			layoutControlItem58.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem59.Control = comboBoxTaxGroup;
			layoutControlItem59.Location = new System.Drawing.Point(0, 25);
			layoutControlItem59.MaxSize = new System.Drawing.Size(405, 24);
			layoutControlItem59.MinSize = new System.Drawing.Size(405, 24);
			layoutControlItem59.Name = "layoutControlItem59";
			layoutControlItem59.Size = new System.Drawing.Size(420, 24);
			layoutControlItem59.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem59.Text = "Tax Group:";
			layoutControlItem59.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem59.Click += new System.EventHandler(layoutControlItem59_Click);
			layoutControlItem60.Control = textBoxTaxIDNumber;
			layoutControlItem60.Location = new System.Drawing.Point(0, 49);
			layoutControlItem60.MaxSize = new System.Drawing.Size(405, 24);
			layoutControlItem60.MinSize = new System.Drawing.Size(405, 24);
			layoutControlItem60.Name = "layoutControlItem60";
			layoutControlItem60.Size = new System.Drawing.Size(420, 24);
			layoutControlItem60.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem60.Text = "Tax ID:";
			layoutControlItem60.TextSize = new System.Drawing.Size(164, 13);
			layoutItemConsignmentCom.Control = textBoxConsignCommission;
			layoutItemConsignmentCom.Location = new System.Drawing.Point(383, 135);
			layoutItemConsignmentCom.MaxSize = new System.Drawing.Size(289, 24);
			layoutItemConsignmentCom.MinSize = new System.Drawing.Size(289, 24);
			layoutItemConsignmentCom.Name = "layoutItemConsignmentCom";
			layoutItemConsignmentCom.Size = new System.Drawing.Size(289, 24);
			layoutItemConsignmentCom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutItemConsignmentCom.Text = "Commission Percent:";
			layoutItemConsignmentCom.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem48.Control = checkBoxAllowConsignment;
			layoutControlItem48.Location = new System.Drawing.Point(169, 135);
			layoutControlItem48.MaxSize = new System.Drawing.Size(214, 24);
			layoutControlItem48.MinSize = new System.Drawing.Size(214, 24);
			layoutControlItem48.Name = "layoutControlItem48";
			layoutControlItem48.Size = new System.Drawing.Size(214, 24);
			layoutControlItem48.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem48.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem48.TextVisible = false;
			emptySpaceItem15.AllowHotTrack = false;
			emptySpaceItem15.Location = new System.Drawing.Point(672, 135);
			emptySpaceItem15.MaxSize = new System.Drawing.Size(194, 24);
			emptySpaceItem15.MinSize = new System.Drawing.Size(194, 24);
			emptySpaceItem15.Name = "emptySpaceItem15";
			emptySpaceItem15.Size = new System.Drawing.Size(222, 24);
			emptySpaceItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem15.TextSize = new System.Drawing.Size(0, 0);
			layoutControlGroup7.ExpandButtonVisible = true;
			layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[5]
			{
				emptySpaceItem14,
				layoutControlItem61,
				layoutControlItem62,
				layoutControlItem63,
				emptySpaceItem1
			});
			layoutControlGroup7.Location = new System.Drawing.Point(0, 346);
			layoutControlGroup7.Name = "layoutControlGroup7";
			layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 9, 9, 9);
			layoutControlGroup7.Size = new System.Drawing.Size(894, 253);
			layoutControlGroup7.Text = "Comments";
			emptySpaceItem14.AllowHotTrack = false;
			emptySpaceItem14.Location = new System.Drawing.Point(0, 135);
			emptySpaceItem14.MaxSize = new System.Drawing.Size(675, 43);
			emptySpaceItem14.MinSize = new System.Drawing.Size(675, 43);
			emptySpaceItem14.Name = "emptySpaceItem14";
			emptySpaceItem14.Size = new System.Drawing.Size(675, 43);
			emptySpaceItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			emptySpaceItem14.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem61.Control = textBoxDeliveryInstructions;
			layoutControlItem61.Location = new System.Drawing.Point(0, 0);
			layoutControlItem61.MaxSize = new System.Drawing.Size(864, 68);
			layoutControlItem61.MinSize = new System.Drawing.Size(864, 68);
			layoutControlItem61.Name = "layoutControlItem61";
			layoutControlItem61.Size = new System.Drawing.Size(879, 68);
			layoutControlItem61.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem61.Text = "Delivery Instructions";
			layoutControlItem61.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem62.Control = textBoxAccountInstructions;
			layoutControlItem62.Location = new System.Drawing.Point(0, 68);
			layoutControlItem62.MaxSize = new System.Drawing.Size(864, 67);
			layoutControlItem62.MinSize = new System.Drawing.Size(864, 67);
			layoutControlItem62.Name = "layoutControlItem62";
			layoutControlItem62.Size = new System.Drawing.Size(879, 67);
			layoutControlItem62.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem62.Text = "Account Instructions:";
			layoutControlItem62.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem63.Control = buttonAccounts;
			layoutControlItem63.Location = new System.Drawing.Point(675, 135);
			layoutControlItem63.MaxSize = new System.Drawing.Size(189, 43);
			layoutControlItem63.MinSize = new System.Drawing.Size(189, 43);
			layoutControlItem63.Name = "layoutControlItem63";
			layoutControlItem63.Size = new System.Drawing.Size(204, 43);
			layoutControlItem63.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem63.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem63.TextVisible = false;
			emptySpaceItem1.AllowHotTrack = false;
			emptySpaceItem1.Location = new System.Drawing.Point(0, 178);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new System.Drawing.Size(879, 33);
			emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem54.Control = textBoxRebatePercent;
			layoutControlItem54.Location = new System.Drawing.Point(274, 207);
			layoutControlItem54.MaxSize = new System.Drawing.Size(182, 24);
			layoutControlItem54.MinSize = new System.Drawing.Size(182, 24);
			layoutControlItem54.Name = "layoutControlItem54";
			layoutControlItem54.Size = new System.Drawing.Size(182, 24);
			layoutControlItem54.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem54.Text = "Rebate %:";
			layoutControlItem54.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
			layoutControlItem54.TextSize = new System.Drawing.Size(53, 13);
			layoutControlItem54.TextToControlDistance = 5;
			layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[2]
			{
				layoutControlItem106,
				layoutControlItem107
			});
			layoutControlGroup10.Location = new System.Drawing.Point(0, 0);
			layoutControlGroup10.Name = "layoutControlGroup10";
			layoutControlGroup10.Size = new System.Drawing.Size(894, 599);
			layoutControlGroup10.Text = "Contacts";
			layoutControlItem106.Control = dataGridContacts;
			layoutControlItem106.Location = new System.Drawing.Point(0, 0);
			layoutControlItem106.Name = "layoutControlItem106";
			layoutControlItem106.Size = new System.Drawing.Size(894, 299);
			layoutControlItem106.Text = "Contacts related to this customer:";
			layoutControlItem106.TextLocation = DevExpress.Utils.Locations.Top;
			layoutControlItem106.TextSize = new System.Drawing.Size(164, 13);
			layoutControlItem107.Control = gridComboBoxContact;
			layoutControlItem107.Location = new System.Drawing.Point(0, 299);
			layoutControlItem107.Name = "layoutControlItem107";
			layoutControlItem107.Size = new System.Drawing.Size(894, 300);
			layoutControlItem107.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem107.TextVisible = false;
			layoutControlItem107.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			tabPageActivity.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[5]
			{
				emptySpaceItem19,
				layoutControlItem108,
				layoutControlItem109,
				emptySpaceItem20,
				layoutControlItem110
			});
			tabPageActivity.Location = new System.Drawing.Point(0, 0);
			tabPageActivity.Name = "tabPageActivity";
			tabPageActivity.Size = new System.Drawing.Size(894, 599);
			tabPageActivity.Text = "Activities";
			emptySpaceItem19.AllowHotTrack = false;
			emptySpaceItem19.Location = new System.Drawing.Point(0, 401);
			emptySpaceItem19.Name = "emptySpaceItem19";
			emptySpaceItem19.Size = new System.Drawing.Size(894, 198);
			emptySpaceItem19.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem108.Control = buttonAddActivity;
			layoutControlItem108.Location = new System.Drawing.Point(0, 0);
			layoutControlItem108.Name = "layoutControlItem108";
			layoutControlItem108.Size = new System.Drawing.Size(298, 24);
			layoutControlItem108.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem108.TextVisible = false;
			layoutControlItem109.Control = comboBoxActivityPeriod;
			layoutControlItem109.Location = new System.Drawing.Point(596, 0);
			layoutControlItem109.Name = "layoutControlItem109";
			layoutControlItem109.Size = new System.Drawing.Size(298, 24);
			layoutControlItem109.Text = "Period:";
			layoutControlItem109.TextSize = new System.Drawing.Size(164, 13);
			emptySpaceItem20.AllowHotTrack = false;
			emptySpaceItem20.Location = new System.Drawing.Point(298, 0);
			emptySpaceItem20.Name = "emptySpaceItem20";
			emptySpaceItem20.Size = new System.Drawing.Size(298, 24);
			emptySpaceItem20.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem110.Control = dataGridActivities;
			layoutControlItem110.Location = new System.Drawing.Point(0, 24);
			layoutControlItem110.Name = "layoutControlItem110";
			layoutControlItem110.Size = new System.Drawing.Size(894, 377);
			layoutControlItem110.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem110.TextVisible = false;
			layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1]
			{
				layoutControlItem111
			});
			layoutControlGroup12.Location = new System.Drawing.Point(0, 0);
			layoutControlGroup12.Name = "layoutControlGroup12";
			layoutControlGroup12.Size = new System.Drawing.Size(894, 599);
			layoutControlGroup12.Text = "Profile";
			layoutControlItem111.Control = textBoxProfileDetails;
			layoutControlItem111.Location = new System.Drawing.Point(0, 0);
			layoutControlItem111.Name = "layoutControlItem111";
			layoutControlItem111.Size = new System.Drawing.Size(894, 599);
			layoutControlItem111.Text = "Customer Profile:";
			layoutControlItem111.TextLocation = DevExpress.Utils.Locations.Top;
			layoutControlItem111.TextSize = new System.Drawing.Size(164, 13);
			layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1]
			{
				layoutControlItem112
			});
			layoutControlGroup13.Location = new System.Drawing.Point(0, 0);
			layoutControlGroup13.Name = "layoutControlGroup13";
			layoutControlGroup13.Size = new System.Drawing.Size(894, 599);
			layoutControlGroup13.Text = "&Note";
			layoutControlItem112.Control = textBoxNote;
			layoutControlItem112.Location = new System.Drawing.Point(0, 0);
			layoutControlItem112.MinSize = new System.Drawing.Size(24, 24);
			layoutControlItem112.Name = "layoutControlItem112";
			layoutControlItem112.Size = new System.Drawing.Size(894, 599);
			layoutControlItem112.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem112.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem112.TextVisible = false;
			layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1]
			{
				layoutControlItem113
			});
			layoutControlGroup14.Location = new System.Drawing.Point(0, 0);
			layoutControlGroup14.Name = "layoutControlGroup14";
			layoutControlGroup14.Size = new System.Drawing.Size(894, 599);
			layoutControlGroup14.Text = "&User Defined";
			layoutControlItem113.Control = udfEntryGrid;
			layoutControlItem113.Location = new System.Drawing.Point(0, 0);
			layoutControlItem113.Name = "layoutControlItem113";
			layoutControlItem113.Size = new System.Drawing.Size(894, 599);
			layoutControlItem113.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem113.TextVisible = false;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 709);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(938, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(938, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(828, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 3;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[21]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripButtonComments,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonInformation,
				toolStripButtonHistory,
				toolStripButtonDesign,
				toolStripDropDownButtonEnquiry,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(938, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonComments.Image = Micromind.ClientUI.Properties.Resources.comment;
			toolStripButtonComments.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonComments.Name = "toolStripButtonComments";
			toolStripButtonComments.Size = new System.Drawing.Size(28, 28);
			toolStripButtonComments.Text = "Comments...";
			toolStripButtonComments.Click += new System.EventHandler(toolStripButtonComments_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripButtonHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonHistory.Image = Micromind.ClientUI.Properties.Resources.historyIcon24x24;
			toolStripButtonHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonHistory.Name = "toolStripButtonHistory";
			toolStripButtonHistory.Size = new System.Drawing.Size(28, 28);
			toolStripButtonHistory.Text = "toolStripButton1";
			toolStripButtonHistory.Visible = false;
			toolStripButtonHistory.Click += new System.EventHandler(toolStripButtonHistory_Click);
			toolStripButtonDesign.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				menuItemLayoutDesign,
				menuItemCustomFields
			});
			toolStripButtonDesign.Image = Micromind.ClientUI.Properties.Resources.layout;
			toolStripButtonDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDesign.Name = "toolStripButtonDesign";
			toolStripButtonDesign.Size = new System.Drawing.Size(111, 28);
			toolStripButtonDesign.Text = "Design Form";
			menuItemLayoutDesign.Name = "menuItemLayoutDesign";
			menuItemLayoutDesign.Size = new System.Drawing.Size(158, 22);
			menuItemLayoutDesign.Text = "Layout...";
			menuItemLayoutDesign.Click += new System.EventHandler(menuItemLayoutDesign_Click);
			menuItemCustomFields.Name = "menuItemCustomFields";
			menuItemCustomFields.Size = new System.Drawing.Size(158, 22);
			menuItemCustomFields.Text = "Custom Fields...";
			menuItemCustomFields.Click += new System.EventHandler(menuItemCustomFields_Click);
			toolStripDropDownButtonEnquiry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButtonEnquiry.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				menuItemCustomerLedger,
				menuItemSalesStatistics,
				toolStripSeparator7
			});
			toolStripDropDownButtonEnquiry.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButtonEnquiry.Name = "toolStripDropDownButtonEnquiry";
			toolStripDropDownButtonEnquiry.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButtonEnquiry.Text = "Enquiry";
			menuItemCustomerLedger.Name = "menuItemCustomerLedger";
			menuItemCustomerLedger.Size = new System.Drawing.Size(165, 22);
			menuItemCustomerLedger.Text = "Customer Ledger";
			menuItemCustomerLedger.Click += new System.EventHandler(menuItemCustomerLedger_Click);
			menuItemSalesStatistics.Name = "menuItemSalesStatistics";
			menuItemSalesStatistics.Size = new System.Drawing.Size(165, 22);
			menuItemSalesStatistics.Text = "Sales Statistics";
			menuItemSalesStatistics.Click += new System.EventHandler(menuItemSalesStatistics_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(162, 6);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				PlantiffToolStripMenuItem,
				defendantToolStripMenuItem,
				toolStripSeparator6
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			PlantiffToolStripMenuItem.Name = "PlantiffToolStripMenuItem";
			PlantiffToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			PlantiffToolStripMenuItem.Text = "Convert To Plantiff";
			PlantiffToolStripMenuItem.Click += new System.EventHandler(PlantiffToolStripMenuItem_Click);
			defendantToolStripMenuItem.Name = "defendantToolStripMenuItem";
			defendantToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			defendantToolStripMenuItem.Text = "Convert To Defendant";
			defendantToolStripMenuItem.Click += new System.EventHandler(defendantToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(186, 6);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(938, 30);
			panel1.TabIndex = 1;
			labelCustomerNameHeader.AutoSize = true;
			labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNameHeader.IsFieldHeader = false;
			labelCustomerNameHeader.IsRequired = true;
			labelCustomerNameHeader.Location = new System.Drawing.Point(24, 7);
			labelCustomerNameHeader.Name = "labelCustomerNameHeader";
			labelCustomerNameHeader.PenWidth = 1f;
			labelCustomerNameHeader.ShowBorder = false;
			labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
			labelCustomerNameHeader.TabIndex = 1;
			labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			contextMenuStripContact.ImageScalingSize = new System.Drawing.Size(20, 20);
			contextMenuStripContact.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				openContactToolStripMenuItem,
				newContactToolStripMenuItem,
				deleteContactToolStripMenuItem
			});
			contextMenuStripContact.Name = "contextMenuStripContact";
			contextMenuStripContact.Size = new System.Drawing.Size(153, 70);
			openContactToolStripMenuItem.Name = "openContactToolStripMenuItem";
			openContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			openContactToolStripMenuItem.Text = "Open Contact";
			openContactToolStripMenuItem.Click += new System.EventHandler(openContactToolStripMenuItem_Click_1);
			newContactToolStripMenuItem.Name = "newContactToolStripMenuItem";
			newContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			newContactToolStripMenuItem.Text = "New Contact";
			deleteContactToolStripMenuItem.Name = "deleteContactToolStripMenuItem";
			deleteContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			deleteContactToolStripMenuItem.Text = "Delete Contact";
			deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
			deleteRowToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			deleteRowToolStripMenuItem.Text = "Delete Row";
			imageListComments.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageListComments.ImageStream");
			imageListComments.TransparentColor = System.Drawing.Color.Transparent;
			imageListComments.Images.SetKeyName(0, "comment.png");
			imageListComments.Images.SetKeyName(1, "comment-yw.png");
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 307;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(938, 749);
			base.Controls.Add(layoutControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "CustomerDetailsForm";
			Text = "Customer Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CustomerClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(CustomerDetailsForm_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxCreditReviewBy).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRatingBy).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceRating).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRating).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentTerms).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCollectionUser).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).EndInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxActivityPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			layoutControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridActivities).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethods).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSource).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBilltoAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethods).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShiptoAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerClass).EndInit();
			((System.ComponentModel.ISupportInitialize)Root).EndInit();
			((System.ComponentModel.ISupportInitialize)tabbedControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem64).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem65).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem66).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem67).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem68).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem69).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem70).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem72).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem73).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem71).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem74).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem75).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutGroupCreditLimit).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem77).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem78).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem81).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem79).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem80).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem82).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem83).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem84).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem85).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem86).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem87).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem88).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem89).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem90).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem91).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem92).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem76).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup9).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem95).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem93).EndInit();
			((System.ComponentModel.ISupportInitialize)panelInsuranceDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem16).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem97).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem98).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem99).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem100).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem101).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem102).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem103).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem104).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem105).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem94).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem96).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem18).EndInit();
			((System.ComponentModel.ISupportInitialize)tabPageGeneral).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem18).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem26).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem27).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem19).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem20).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem21).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem22).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem23).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem24).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem28).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem29).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem30).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem31).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem32).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem34).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem35).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem36).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem33).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem37).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem25).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)tabPageDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem38).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem39).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem40).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem41).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem42).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem43).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem44).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem45).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem46).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem9).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem10).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem47).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem50).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem51).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem52).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem11).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem53).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem12).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem55).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem56).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem57).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup6).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem58).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem59).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem60).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutItemConsignmentCom).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem48).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem15).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup7).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem14).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem61).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem62).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem63).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem54).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup10).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem106).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem107).EndInit();
			((System.ComponentModel.ISupportInitialize)tabPageActivity).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem19).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem108).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem109).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem20).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem110).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup12).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem111).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup13).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem112).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup14).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem113).EndInit();
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStripContact.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
			comboBoxActivityPeriod.LoadData();
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void AddEvents()
		{
			FormActivator.CustomerAddressDetailsFormObj.CustomerAddressChanged += EventHelper_CustomerAddressChanged;
			dataGridContacts.AfterCellUpdate += dataGridContacts_AfterCellUpdate;
			dataGridContacts.BeforeCellUpdate += dataGridContacts_BeforeCellUpdate;
			dataGridContacts.ClickCellButton += dataGridContacts_ClickCellButton;
			dataGridContacts.CellDataError += dataGridContacts_CellDataError;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			checkBoxAllowConsignment.CheckedChanged += checkBoxAllowConsignment_CheckedChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
			dataGridActivities.DoubleClick += dataGridActivities_DoubleClick;
			textBoxLatitude.KeyDown += textBoxLatitude_KeyDown;
			textBoxLongitude.KeyDown += textBoxLongitude_KeyDown;
			textBoxLatitude.KeyUp += textBoxLatitude_KeyUp;
			textBoxLongitude.KeyUp += textBoxLongitude_KeyUp;
			textBoxGraceDays.KeyPress += txtDays_KeyPress;
			comboBoxInsuranceStatus.SelectedIndexChanged += ComboBoxInsuranceStatus_SelectedIndexChanged;
		}

		private void ComboBoxInsuranceStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxInsuranceStatus.SelectedIndex == 0)
			{
				panelInsuranceDetails.Visibility = LayoutVisibility.OnlyInCustomization;
			}
			else
			{
				panelInsuranceDetails.Visibility = LayoutVisibility.Always;
			}
		}

		private void dataGridActivities_DoubleClick(object sender, EventArgs e)
		{
			string voucherID = dataGridActivities.ActiveRow.Cells["VoucherID"].Value.ToString();
			string sysDocID = dataGridActivities.ActiveRow.Cells["SysDocID"].Value.ToString();
			new FormHelper().EditTransaction(sysDocID, voucherID);
		}

		private void checkBoxAllowConsignment_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxAllowConsignment.Checked)
			{
				layoutItemConsignmentCom.Visibility = LayoutVisibility.Always;
			}
			else
			{
				layoutItemConsignmentCom.Visibility = LayoutVisibility.OnlyInCustomization;
			}
		}

		private void dataGridContacts_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridContacts.ActiveCell.Column.Key.ToString() == "ContactID")
			{
				e.RaiseErrorEvent = false;
				gridComboBoxContact.Text = dataGridContacts.ActiveCell.Text;
				gridComboBoxContact.QuickAddItem();
			}
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void dataGridContacts_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "ContactID")
			{
				DataRow dataRow = Factory.ContactSystem.GetContactByID(e.Cell.Value.ToString()).Tables[0].Rows[0];
				dataGridContacts.ActiveRow.Cells["FirstName"].Value = dataRow["FirstName"].ToString();
				dataGridContacts.ActiveRow.Cells["LastName"].Value = dataRow["LastName"].ToString();
				dataGridContacts.ActiveRow.Cells["JobTitle"].Value = dataRow["JobTitle"].ToString();
				dataGridContacts.ActiveRow.Cells["Note"].Value = dataRow["Note"].ToString();
			}
		}

		private void textBoxCode_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void comboBoxARAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridContacts_ClickCellButton(object sender, CellEventArgs e)
		{
		}

		private void dataGridContacts_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "ContactID" && dataGridContacts.ExistCellValue("ContactID", e.NewValue.ToString()) >= 0)
			{
				ErrorHelper.InformationMessage("This contact is already added to list. Please select another contact.");
				e.Cancel = true;
			}
		}

		private void EventHelper_CustomerAddressChanged(object sender, EventArgs e)
		{
			DataSet dataSet = sender as DataSet;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				if (dataRow["CustomerID"].ToString() == textBoxCode.Text && dataRow["AddressID"].ToString() == textBoxAddressID.Text && !isNewRecord)
				{
					FillAddressData(dataRow);
				}
			}
		}

		private void CustomerDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					dataGridContacts.SetupUI();
					SetupContactsGrid();
					dataGridActivities.ApplyUIDesign();
					SetupActivityGrid();
					if (textBoxLatitude.Text != "")
					{
						textBoxLatitude.ForeColor = Color.Gray;
						textBoxLongitude.ForeColor = Color.Gray;
						textBoxLatitude.Text = "25.2824891";
						textBoxLongitude.Text = "55.3583311";
					}
					ClearForm();
					textBoxCode.Focus();
					if (CompanyPreferences.TaxEntityTypes.Contains("C"))
					{
						comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
						comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
					}
					layoutGroupCreditLimit.Enabled = !disableCustomerCreditLimit;
					Init();
					FormHelper formHelper = new FormHelper();
					formHelper.AddCustomFieldsToForm(base.Name, "Customer", layoutControl1);
					formHelper.InitLayoutControl(layoutControl1);
					formHelper.LoadFormLayout(layoutControl1, base.Name, "Default");
					toolStripButtonDesign.Visible = false;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupContactsGrid()
		{
			dataGridContacts.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ContactID").Unique = true;
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("LastName");
			dataTable.Columns.Add("JobTitle");
			dataTable.Columns.Add("Phone1");
			dataTable.Columns.Add("Mobile");
			dataTable.Columns.Add("Email1");
			dataTable.Columns.Add("Note");
			dataGridContacts.DataSource = dataTable;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].MaxLength = 30;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].MaxLength = 64;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].MaxLength = 255;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].Header.Caption = "Job Title";
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Header.Caption = "Contact Code";
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].Header.Caption = "First Name";
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].Header.Caption = "Last Name";
			dataGridContacts.DisplayLayout.Bands[0].Columns["Phone1"].Header.Caption = "Land Line";
			dataGridContacts.DisplayLayout.Bands[0].Columns["Email1"].Header.Caption = "E-Mail";
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].ValueList = gridComboBoxContact;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].CellActivation = Activation.AllowEdit;
		}

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxName.Text.Trim() == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.CustomerTable.Rows[0];
					textBoxCode.Text = dataRow["CustomerID"].ToString();
					textBoxName.Text = dataRow["CustomerName"].ToString();
					textBoxForeignName.Text = dataRow["ForeignName"].ToString();
					textBoxFormalName.Text = dataRow["ShortName"].ToString();
					comboBoxParentCustomer.SelectedID = dataRow["ParentCustomerID"].ToString();
					comboBoxCustomerGroup.SelectedID = dataRow["CustomerGroupID"].ToString();
					if (comboBoxParentCustomer.SelectedID != "")
					{
						checkBoxparentACforposting.Enabled = true;
					}
					else
					{
						checkBoxparentACforposting.Checked = false;
						checkBoxparentACforposting.Enabled = false;
					}
					comboBoxCustomerClass.SelectedID = dataRow["CustomerClassID"].ToString();
					comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
					comboBoxArea.SelectedID = dataRow["AreaID"].ToString();
					comboBoxLeadSource.SelectedID = dataRow["LeadSourceID"].ToString();
					comboBoxPriceLevel.SelectedID = dataRow["PriceLevelID"].ToString();
					checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					checkBoxHold.Checked = bool.Parse(dataRow["IsHold"].ToString());
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxCollectionUser.SelectedID = dataRow["CollectionUserID"].ToString();
					textBoxTempLimit.Text = decimal.Parse(dataRow["TempCredit"].ToString()).ToString(Format.TotalAmountFormat);
					comboBoxSalesperson.SelectedID = dataRow["SalesPersonID"].ToString();
					comboBoxPaymentTerms.SelectedID = dataRow["PaymentTermID"].ToString();
					comboBoxPaymentMethods.SelectedID = dataRow["PaymentMethodID"].ToString();
					comboBoxShippingMethods.SelectedID = dataRow["ShippingMethodID"].ToString();
					textBoxStatementEmail.Text = dataRow["StatementEmail"].ToString();
					textBoxDeliveryInstructions.Text = dataRow["DeliveryInstructions"].ToString();
					textBoxAccountInstructions.Text = dataRow["AccountInstructions"].ToString();
					comboBoxBilltoAddress.SelectedID = dataRow["BillToAddressID"].ToString();
					comboBoxShiptoAddress.SelectedID = dataRow["ShipToAddressID"].ToString();
					if (!dataRow["StatementSendingMethod"].IsDBNullOrEmpty())
					{
						comboBoxStatementMethod.SelectedIndex = int.Parse(dataRow["StatementSendingMethod"].ToString());
					}
					else
					{
						comboBoxStatementMethod.SelectedIndex = 0;
					}
					SourceLeadID = dataRow["SourceLeadID"].ToString();
					if (dataRow["AcceptCheckPayment"] != DBNull.Value)
					{
						checkBoxAcceptCheque.Checked = bool.Parse(dataRow["AcceptCheckPayment"].ToString());
					}
					else
					{
						checkBoxAcceptCheque.Checked = true;
					}
					if (dataRow["IsWeightInvoice"] != DBNull.Value)
					{
						checkBoxWeightInvoice.Checked = bool.Parse(dataRow["IsWeightInvoice"].ToString());
					}
					else
					{
						checkBoxWeightInvoice.Checked = false;
					}
					if (dataRow["IsParentPosting"] != DBNull.Value)
					{
						checkBoxparentACforposting.Checked = bool.Parse(dataRow["IsParentPosting"].ToString());
					}
					else
					{
						checkBoxparentACforposting.Checked = false;
					}
					if (dataRow["LimitPDCUnsecured"] != DBNull.Value)
					{
						checkBoxUnsecuredLimit.Checked = bool.Parse(dataRow["LimitPDCUnsecured"].ToString());
						if (dataRow["PDCUnsecuredLimitAmount"] != DBNull.Value)
						{
							textBoxUnsecuredLimit.Text = decimal.Parse(dataRow["PDCUnsecuredLimitAmount"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxUnsecuredLimit.Text = 0.ToString(Format.TotalAmountFormat);
						}
					}
					else
					{
						checkBoxUnsecuredLimit.Checked = false;
						textBoxUnsecuredLimit.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["CLValidity"] != DBNull.Value)
					{
						dateTimePickerCLValidity.Checked = true;
						dateTimePickerCLValidity.Value = DateTime.Parse(dataRow["CLValidity"].ToString());
					}
					else
					{
						dateTimePickerCLValidity.Clear();
						dateTimePickerCLValidity.Checked = false;
					}
					if (dataRow["AcceptPDC"] != DBNull.Value)
					{
						checkBoxAcceptPDC.Checked = bool.Parse(dataRow["AcceptPDC"].ToString());
					}
					else
					{
						checkBoxAcceptPDC.Checked = true;
					}
					if (dataRow["Rating"] != DBNull.Value)
					{
						comboBoxRating.SelectedIndex = int.Parse(dataRow["Rating"].ToString());
					}
					else
					{
						comboBoxRating.SelectedIndex = 0;
					}
					if (dataRow["InsRating"] != DBNull.Value)
					{
						comboBoxInsuranceRating.SelectedIndex = int.Parse(dataRow["InsRating"].ToString());
					}
					else
					{
						comboBoxInsuranceRating.SelectedIndex = 0;
					}
					if (dataRow["AllowConsignment"] != DBNull.Value)
					{
						checkBoxAllowConsignment.Checked = bool.Parse(dataRow["AllowConsignment"].ToString());
					}
					else
					{
						checkBoxAllowConsignment.Checked = false;
					}
					if (dataRow["ConsignComPercent"] != DBNull.Value)
					{
						textBoxConsignCommission.Text = dataRow["ConsignComPercent"].ToString();
					}
					else
					{
						textBoxConsignCommission.Text = "0.00";
					}
					if (dataRow["DiscountPercent"] != DBNull.Value)
					{
						textBoxDiscountPercent.Text = dataRow["DiscountPercent"].ToString();
					}
					else
					{
						textBoxDiscountPercent.Text = "0.00";
					}
					if (dataRow["RebatePercent"] != DBNull.Value)
					{
						textBoxRebatePercent.Text = dataRow["RebatePercent"].ToString();
					}
					else
					{
						textBoxRebatePercent.Text = "0.00";
					}
					if (dataRow["IsCustomerSince"] != DBNull.Value)
					{
						dateTimePickerCustomerSince.Value = DateTime.Parse(dataRow["IsCustomerSince"].ToString());
						dateTimePickerCustomerSince.Checked = true;
					}
					else
					{
						dateTimePickerCustomerSince.IsNull = true;
						dateTimePickerCustomerSince.Checked = false;
					}
					if (dataRow["DateEstablished"] != DBNull.Value)
					{
						dateTimePickerEstablished.Value = DateTime.Parse(dataRow["DateEstablished"].ToString());
						dateTimePickerEstablished.Checked = true;
					}
					else
					{
						dateTimePickerEstablished.IsNull = true;
						dateTimePickerEstablished.Checked = false;
					}
					if (dataRow["CreditReviewDate"] != DBNull.Value)
					{
						dateTimePickerReviewDate.Value = DateTime.Parse(dataRow["CreditReviewDate"].ToString());
						dateTimePickerReviewDate.Checked = true;
					}
					else
					{
						dateTimePickerReviewDate.IsNull = true;
						dateTimePickerReviewDate.Checked = false;
					}
					DateTime result = new DateTime(1, 1, 1);
					DateTime.TryParse(dataRow["RatingDate"].ToString(), out result);
					if (dataRow["GraceDays"] != DBNull.Value)
					{
						textBoxGraceDays.Text = dataRow["GraceDays"].ToString();
					}
					else
					{
						textBoxGraceDays.Text = "0";
					}
					if (dataRow["RatingDate"] != DBNull.Value)
					{
						dateTimePickerRatingDate.Value = DateTime.Parse(dataRow["RatingDate"].ToString());
						dateTimePickerRatingDate.Checked = true;
					}
					else
					{
						dateTimePickerRatingDate.IsNull = true;
						dateTimePickerRatingDate.Checked = false;
					}
					if ((SqlBoolean)(dataRow["RatingDate"] != DBNull.Value) && result > SqlDateTime.MinValue)
					{
						dateTimePickerRatingDate.Value = DateTime.Parse(dataRow["RatingDate"].ToString());
						dateTimePickerRatingDate.Checked = true;
					}
					else
					{
						dateTimePickerRatingDate.IsNull = true;
						dateTimePickerRatingDate.Checked = false;
					}
					textBoxLicenseNumber.Text = dataRow["LicenseNumber"].ToString();
					if (dataRow["LicenseExpDate"] != DBNull.Value)
					{
						dateTimePickerLicenseExpDate.Value = DateTime.Parse(dataRow["LicenseExpDate"].ToString());
						dateTimePickerLicenseExpDate.Checked = true;
					}
					else
					{
						dateTimePickerLicenseExpDate.IsNull = true;
						dateTimePickerLicenseExpDate.Checked = false;
					}
					if (dataRow["ContractExpDate"] != DBNull.Value)
					{
						dateTimePickerContractExpDate.Value = DateTime.Parse(dataRow["ContractExpDate"].ToString());
						dateTimePickerContractExpDate.Checked = true;
					}
					else
					{
						dateTimePickerContractExpDate.IsNull = true;
						dateTimePickerContractExpDate.Checked = false;
					}
					if (dataRow["CreditReviewBy"] != DBNull.Value)
					{
						comboBoxCreditReviewBy.SelectedID = dataRow["CreditReviewBy"].ToString();
					}
					else
					{
						comboBoxCreditReviewBy.Clear();
					}
					if (dataRow["RatingBy"] != DBNull.Value)
					{
						comboBoxRatingBy.SelectedID = dataRow["RatingBy"].ToString();
					}
					else
					{
						comboBoxRatingBy.Clear();
					}
					CreditLimitTypes creditLimitTypes = CreditLimitTypes.Unlimited;
					if (dataRow["CreditLimitType"] != DBNull.Value)
					{
						creditLimitTypes = (CreditLimitTypes)byte.Parse(dataRow["CreditLimitType"].ToString());
					}
					switch (creditLimitTypes)
					{
					case CreditLimitTypes.CreditAmount:
						radioButtonCreditLimitAmount.Checked = true;
						break;
					case CreditLimitTypes.NoCredit:
						radioButtonCreditLimitNoCredit.Checked = true;
						break;
					case CreditLimitTypes.ParentSublimit:
						radioButtonSublimit.Checked = true;
						break;
					default:
						radioButtonCreditLimitUnlimited.Checked = true;
						break;
					}
					if (dataRow["CreditAmount"] != DBNull.Value)
					{
						textBoxCreditLimit.Text = decimal.Parse(dataRow["CreditAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxCreditLimit.Text = 0.ToString(Format.TotalAmountFormat);
					}
					textBoxRatingRemarks.Text = dataRow["RatingRemarks"].ToString();
					if (dataRow["InsApprovedAmount"] != DBNull.Value)
					{
						textBoxInsuranceApprovedAmount.Text = decimal.Parse(dataRow["InsApprovedAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxInsuranceApprovedAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["InsRequestedAmount"] != DBNull.Value)
					{
						textBoxInsuranceReqAmount.Text = decimal.Parse(dataRow["InsRequestedAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxInsuranceReqAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["InsApplicationDate"] != DBNull.Value)
					{
						dateTimePickerInsuranceDate.Value = DateTime.Parse(dataRow["InsApplicationDate"].ToString());
						dateTimePickerInsuranceDate.Checked = true;
					}
					else
					{
						dateTimePickerInsuranceDate.IsNull = true;
						dateTimePickerInsuranceDate.Checked = false;
					}
					textBoxInsuranceNumber.Text = dataRow["InsPolicyNumber"].ToString();
					textBoxInsuranceRemarks.Text = dataRow["InsRemarks"].ToString();
					textBoxInsuranceID.Text = dataRow["InsuranceID"].ToString();
					checked
					{
						if (dataRow["InsStatus"] != DBNull.Value)
						{
							comboBoxInsuranceStatus.SelectedIndex = unchecked((int)byte.Parse(dataRow["InsStatus"].ToString())) - 1;
						}
						else
						{
							comboBoxInsuranceStatus.SelectedIndex = 0;
						}
						if (dataRow["InsProviderID"] != DBNull.Value)
						{
							comboBoxInsuranceProvider.SelectedID = dataRow["InsProviderID"].ToString().TrimStart();
						}
						DateTime result2 = new DateTime(1753, 1, 1);
						DateTime result3 = new DateTime(1753, 1, 1);
						if (dataRow["InsEffectiveDate"] != DBNull.Value)
						{
							DateTime.TryParse(dataRow["InsEffectiveDate"].ToString(), out result2);
							if ((SqlBoolean)true && result2 > SqlDateTime.MinValue)
							{
								datetimePickerEffectiveDate.Value = DateTime.Parse(dataRow["InsEffectiveDate"].ToString());
								datetimePickerEffectiveDate.Checked = true;
							}
							else
							{
								datetimePickerEffectiveDate.IsNull = true;
								datetimePickerEffectiveDate.Checked = false;
							}
						}
						else
						{
							datetimePickerEffectiveDate.IsNull = true;
							datetimePickerEffectiveDate.Checked = false;
						}
						if (dataRow["InsExpiryDate"] != DBNull.Value)
						{
							DateTime.TryParse(dataRow["InsExpiryDate"].ToString(), out result3);
							if ((SqlBoolean)true && result3 > SqlDateTime.MinValue)
							{
								dateTimePickerValidTo.Value = DateTime.Parse(dataRow["InsExpiryDate"].ToString());
								dateTimePickerValidTo.Checked = true;
							}
							else
							{
								dateTimePickerValidTo.IsNull = true;
								dateTimePickerValidTo.Checked = false;
							}
						}
						else
						{
							dateTimePickerValidTo.IsNull = true;
							dateTimePickerValidTo.Checked = false;
						}
						textBoxBankName.Text = dataRow["BankName"].ToString();
						textBoxBankBranch.Text = dataRow["BankBranch"].ToString();
						textBoxBankAccountNumber.Text = dataRow["BankAccountNumber"].ToString();
						if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
						{
							comboBoxTaxOption.SelectedIndex = int.Parse(dataRow["TaxOption"].ToString());
						}
						else
						{
							comboBoxTaxOption.SelectedIndex = 0;
						}
						if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
						{
							comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
						}
						else
						{
							comboBoxTaxGroup.Clear();
						}
						textBoxTaxIDNumber.Text = dataRow["TaxIDNumber"].ToString();
						textBoxProfileDetails.WordMLText = dataRow["ProfileDetails"].ToString();
						textBoxProfileDetails.EndUpdate();
						if (dataRow["HasPhoto"] != DBNull.Value)
						{
							bool flag = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
							linkLoadImage.Visible = flag;
							linkRemovePicture.Enabled = flag;
						}
						else
						{
							linkLoadImage.Visible = false;
							linkRemovePicture.Enabled = false;
						}
						SetHeaderName();
						if (currentData.Tables.Contains("Customer_Address") && currentData.CustomerAddressTable.Rows.Count != 0)
						{
							dataRow = currentData.CustomerAddressTable.Rows[0];
							FillAddressData(dataRow);
							if (currentData.Tables.Contains("Customer_Contact_Detail") && currentData.CustomerAddressTable.Rows.Count != 0)
							{
								DataTable dataTable = dataGridContacts.DataSource as DataTable;
								dataTable.Rows.Clear();
								foreach (DataRow row in currentData.Tables["Customer_Contact_Detail"].Rows)
								{
									DataRow dataRow3 = dataTable.NewRow();
									foreach (DataColumn column in dataTable.Columns)
									{
										if (dataRow3.Table.Columns.Contains(column.ColumnName))
										{
											dataRow3[column.ColumnName] = row[column.ColumnName];
										}
										else
										{
											ErrorHelper.ErrorMessage(column.ColumnName + " Does not exist.");
										}
									}
									dataRow3.EndEdit();
									dataTable.Rows.Add(dataRow3);
								}
								dataTable.AcceptChanges();
								new FormHelper().FillEntityUDFData("Customer", "CustomerID", "", currentData, layoutControl1);
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void comboBoxInsuranceProvider_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxProvider.Text = comboBoxInsuranceProvider.SelectedName;
		}

		private void FillAddressData(DataRow row)
		{
			textBoxAddressID.Text = row["AddressID"].ToString();
			textBoxContactName.Text = row["ContactName"].ToString();
			textBoxAddress1.Text = row["Address1"].ToString();
			textBoxAddress2.Text = row["Address2"].ToString();
			textBoxAddress3.Text = row["Address3"].ToString();
			textBoxAddressPrintFormat.Text = row["AddressPrintFormat"].ToString();
			textBoxCity.Text = row["City"].ToString();
			textBoxState.Text = row["State"].ToString();
			textBoxCountry.Text = row["Country"].ToString();
			textBoxPostalCode.Text = row["PostalCode"].ToString();
			if (!string.IsNullOrEmpty(row["Latitude"].ToString()))
			{
				textBoxLatitude.Text = row["Latitude"].ToString();
				textBoxLatitude.ForeColor = Color.Black;
			}
			if (!string.IsNullOrEmpty(row["Longitude"].ToString()))
			{
				textBoxLongitude.Text = row["Longitude"].ToString();
				textBoxLongitude.ForeColor = Color.Black;
			}
			textBoxDepartment.Text = row["Department"].ToString();
			textBoxPhone1.Text = row["Phone1"].ToString();
			textBoxPhone2.Text = row["Phone2"].ToString();
			textBoxFax.Text = row["Fax"].ToString();
			textBoxMobile.Text = row["Mobile"].ToString();
			textBoxEmail.Text = row["Email"].ToString();
			textBoxWebsite.Text = row["Website"].ToString();
			textBoxComment.Text = row["Comment"].ToString();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomerData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomerTable.Rows[0] : currentData.CustomerTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CustomerID"] = textBoxCode.Text;
				dataRow["CustomerName"] = textBoxName.Text;
				dataRow["ForeignName"] = textBoxForeignName.Text;
				dataRow["ShortName"] = textBoxFormalName.Text;
				if (comboBoxParentCustomer.SelectedID != "")
				{
					dataRow["ParentCustomerID"] = comboBoxParentCustomer.SelectedID;
				}
				else
				{
					dataRow["ParentCustomerID"] = DBNull.Value;
				}
				dataRow["CollectionUserID"] = (string.IsNullOrEmpty(comboBoxCollectionUser.SelectedID) ? ((IConvertible)DBNull.Value) : ((IConvertible)comboBoxCollectionUser.SelectedID));
				if (comboBoxParentCustomer.SelectedID != "")
				{
					dataRow["ParentCustomerID"] = comboBoxParentCustomer.SelectedID;
				}
				else
				{
					dataRow["ParentCustomerID"] = DBNull.Value;
				}
				if (comboBoxCustomerGroup.SelectedID != "")
				{
					dataRow["CustomerGroupID"] = comboBoxCustomerGroup.SelectedID;
				}
				else
				{
					dataRow["CustomerGroupID"] = DBNull.Value;
				}
				if (comboBoxStatementMethod.SelectedIndex != -1)
				{
					dataRow["StatementSendingMethod"] = comboBoxStatementMethod.SelectedIndex;
				}
				else
				{
					dataRow["StatementSendingMethod"] = 0;
				}
				dataRow["StatementEmail"] = textBoxStatementEmail.Text;
				dataRow["DeliveryInstructions"] = textBoxDeliveryInstructions.Text;
				dataRow["AccountInstructions"] = textBoxAccountInstructions.Text;
				if (comboBoxCustomerClass.SelectedID != "")
				{
					dataRow["CustomerClassID"] = comboBoxCustomerClass.SelectedID;
				}
				else
				{
					dataRow["CustomerClassID"] = DBNull.Value;
				}
				if (comboBoxCountry.SelectedID != "")
				{
					dataRow["CountryID"] = comboBoxCountry.SelectedID;
				}
				else
				{
					dataRow["CountryID"] = DBNull.Value;
				}
				if (comboBoxArea.SelectedID != "")
				{
					dataRow["AreaID"] = comboBoxArea.SelectedID;
				}
				else
				{
					dataRow["AreaID"] = DBNull.Value;
				}
				if (comboBoxLeadSource.SelectedID != "")
				{
					dataRow["LeadSourceID"] = comboBoxLeadSource.SelectedID;
				}
				else
				{
					dataRow["LeadSourceID"] = DBNull.Value;
				}
				if (comboBoxPriceLevel.SelectedID != "")
				{
					dataRow["PriceLevelID"] = comboBoxPriceLevel.SelectedID;
				}
				else
				{
					dataRow["PriceLevelID"] = DBNull.Value;
				}
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
				}
				dataRow["IsWeightInvoice"] = checkBoxWeightInvoice.Checked;
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow["IsHold"] = checkBoxHold.Checked;
				dataRow["IsParentPosting"] = checkBoxparentACforposting.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["RatingRemarks"] = textBoxRatingRemarks.Text;
				dataRow["ProfileDetails"] = textBoxProfileDetails.WordMLText;
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalesPersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalesPersonID"] = DBNull.Value;
				}
				if (comboBoxPaymentTerms.SelectedID != "")
				{
					dataRow["PaymentTermID"] = comboBoxPaymentTerms.SelectedID;
				}
				else
				{
					dataRow["PaymentTermID"] = DBNull.Value;
				}
				if (comboBoxPaymentMethods.SelectedID != "")
				{
					dataRow["PaymentMethodID"] = comboBoxPaymentMethods.SelectedID;
				}
				else
				{
					dataRow["PaymentMethodID"] = DBNull.Value;
				}
				if (comboBoxShippingMethods.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethods.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				if (comboBoxBilltoAddress.SelectedID != "")
				{
					dataRow["BillToAddressID"] = comboBoxBilltoAddress.SelectedID;
				}
				else
				{
					dataRow["BillToAddressID"] = DBNull.Value;
				}
				if (comboBoxShiptoAddress.SelectedID != "")
				{
					dataRow["ShipToAddressID"] = comboBoxShiptoAddress.SelectedID;
				}
				else
				{
					dataRow["ShipToAddressID"] = DBNull.Value;
				}
				if (comboBoxRating.SelectedIndex != -1)
				{
					dataRow["Rating"] = comboBoxRating.SelectedIndex;
				}
				else
				{
					dataRow["Rating"] = DBNull.Value;
				}
				if (comboBoxInsuranceRating.SelectedIndex != -1)
				{
					dataRow["InsRating"] = comboBoxInsuranceRating.SelectedIndex;
				}
				else
				{
					dataRow["InsRating"] = DBNull.Value;
				}
				if (comboBoxRatingBy.SelectedIndex != -1)
				{
					dataRow["RatingBy"] = comboBoxRatingBy.SelectedID;
				}
				else
				{
					dataRow["RatingBy"] = DBNull.Value;
				}
				if (dateTimePickerEstablished.Checked)
				{
					dataRow["DateEstablished"] = dateTimePickerEstablished.Value;
				}
				else
				{
					dataRow["DateEstablished"] = DBNull.Value;
				}
				if (dateTimePickerReviewDate.Checked)
				{
					dataRow["CreditReviewDate"] = dateTimePickerReviewDate.Value;
				}
				else
				{
					dataRow["CreditReviewDate"] = DBNull.Value;
				}
				if (dateTimePickerCustomerSince.Checked)
				{
					dataRow["IsCustomerSince"] = dateTimePickerCustomerSince.Value;
				}
				else
				{
					dataRow["IsCustomerSince"] = DBNull.Value;
				}
				if (comboBoxCreditReviewBy.SelectedID == "")
				{
					dataRow["CreditReviewBy"] = DBNull.Value;
				}
				else
				{
					dataRow["CreditReviewBy"] = comboBoxCreditReviewBy.SelectedID;
				}
				if (comboBoxRatingBy.SelectedID == "")
				{
					dataRow["RatingBy"] = DBNull.Value;
				}
				else
				{
					dataRow["RatingBy"] = comboBoxRatingBy.SelectedID;
				}
				if (dateTimePickerRatingDate.Checked)
				{
					dataRow["RatingDate"] = dateTimePickerRatingDate.Value;
				}
				else
				{
					dataRow["RatingDate"] = DBNull.Value;
				}
				dataRow["AcceptCheckPayment"] = checkBoxAcceptCheque.Checked;
				dataRow["AcceptPDC"] = checkBoxAcceptPDC.Checked;
				if (dateTimeBalanceConfirmationDate.Checked)
				{
					dataRow["BalanceConfirmationDate"] = dateTimeBalanceConfirmationDate.Value;
				}
				if (!string.IsNullOrEmpty(textBoxConfirmationLevel.Text))
				{
					dataRow["ConfirmationInterval"] = textBoxConfirmationLevel.Text;
				}
				dataRow["LimitPDCUnsecured"] = checkBoxUnsecuredLimit.Checked;
				if (checkBoxUnsecuredLimit.Checked)
				{
					dataRow["PDCUnsecuredLimitAmount"] = textBoxUnsecuredLimit.Text;
				}
				else
				{
					dataRow["PDCUnsecuredLimitAmount"] = DBNull.Value;
				}
				dataRow["AllowConsignment"] = checkBoxAllowConsignment.Checked;
				if (checkBoxAllowConsignment.Checked)
				{
					dataRow["ConsignComPercent"] = textBoxConsignCommission.Text;
				}
				else
				{
					dataRow["ConsignComPercent"] = DBNull.Value;
				}
				dataRow["DiscountPercent"] = textBoxDiscountPercent.Text;
				dataRow["RebatePercent"] = textBoxRebatePercent.Text;
				if (radioButtonCreditLimitAmount.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.CreditAmount;
				}
				else if (radioButtonCreditLimitNoCredit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.NoCredit;
				}
				else if (radioButtonSublimit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.ParentSublimit;
				}
				else
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.Unlimited;
				}
				if (radioButtonCreditLimitAmount.Checked)
				{
					if (dateTimePickerCLValidity.Checked)
					{
						dataRow["CLValidity"] = dateTimePickerCLValidity.Value;
					}
					else
					{
						dataRow["CLValidity"] = DBNull.Value;
					}
				}
				else
				{
					dataRow["CLValidity"] = DBNull.Value;
				}
				if (textBoxCreditLimit.Text != "")
				{
					dataRow["CreditAmount"] = textBoxCreditLimit.Text;
				}
				else
				{
					dataRow["CreditAmount"] = 0;
				}
				if (!string.IsNullOrEmpty(textBoxGraceDays.Text))
				{
					dataRow["GraceDays"] = textBoxGraceDays.Text;
				}
				else
				{
					dataRow["GraceDays"] = 0;
				}
				if (dateTimePickerInsuranceDate.Checked)
				{
					dataRow["InsApplicationDate"] = dateTimePickerInsuranceDate.Value;
				}
				else
				{
					dataRow["InsApplicationDate"] = DBNull.Value;
				}
				dataRow["InsApprovedAmount"] = textBoxInsuranceApprovedAmount.Text;
				dataRow["InsPolicyNumber"] = textBoxInsuranceNumber.Text;
				dataRow["InsRemarks"] = textBoxInsuranceRemarks.Text.Trim();
				dataRow["InsRequestedAmount"] = textBoxInsuranceReqAmount.Text;
				dataRow["InsStatus"] = checked(comboBoxInsuranceStatus.SelectedIndex + 1);
				dataRow["InsuranceID"] = textBoxInsuranceID.Text;
				dataRow["BankName"] = textBoxBankName.Text;
				dataRow["BankBranch"] = textBoxBankBranch.Text;
				dataRow["BankAccountNumber"] = textBoxBankAccountNumber.Text;
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				dataRow["TaxIDNumber"] = textBoxTaxIDNumber.Text;
				if (ARAccountID != "")
				{
					dataRow["ARAccountID"] = ARAccountID;
				}
				else
				{
					dataRow["ARAccountID"] = DBNull.Value;
				}
				dataRow["LicenseNumber"] = textBoxLicenseNumber.Text;
				if (dateTimePickerLicenseExpDate.Checked)
				{
					dataRow["LicenseExpDate"] = dateTimePickerLicenseExpDate.Value;
				}
				else
				{
					dataRow["LicenseExpDate"] = DBNull.Value;
				}
				if (dateTimePickerContractExpDate.Checked)
				{
					dataRow["ContractExpDate"] = dateTimePickerContractExpDate.Value;
				}
				else
				{
					dataRow["ContractExpDate"] = DBNull.Value;
				}
				dataRow["SourceLeadID"] = SourceLeadID;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.CustomerAddressTable.Rows[0] : currentData.CustomerAddressTable.NewRow());
				dataRow.BeginEdit();
				dataRow["CustomerID"] = textBoxCode.Text;
				dataRow["AddressID"] = textBoxAddressID.Text.Trim();
				dataRow["ContactName"] = textBoxContactName.Text;
				dataRow["Address1"] = textBoxAddress1.Text;
				dataRow["Address2"] = textBoxAddress2.Text;
				dataRow["Address3"] = textBoxAddress3.Text;
				dataRow["AddressPrintFormat"] = textBoxAddressPrintFormat.Text;
				dataRow["City"] = textBoxCity.Text;
				dataRow["State"] = textBoxState.Text;
				dataRow["Country"] = textBoxCountry.Text;
				dataRow["PostalCode"] = textBoxPostalCode.Text;
				if (textBoxLatitude.Text != "25.278306" && textBoxLongitude.Text != "55.322663")
				{
					dataRow["Latitude"] = textBoxLatitude.Text;
					dataRow["Longitude"] = textBoxLongitude.Text;
				}
				else
				{
					dataRow["Longitude"] = DBNull.Value;
					dataRow["Latitude"] = DBNull.Value;
				}
				dataRow["Department"] = textBoxDepartment.Text;
				dataRow["Phone1"] = textBoxPhone1.Text;
				dataRow["Phone2"] = textBoxPhone2.Text;
				dataRow["Fax"] = textBoxFax.Text;
				dataRow["Mobile"] = textBoxMobile.Text;
				dataRow["Email"] = textBoxEmail.Text;
				dataRow["Website"] = textBoxWebsite.Text;
				dataRow["Comment"] = textBoxComment.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerAddressTable.Rows.Add(dataRow);
				}
				currentData.CustomerContactTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridContacts.Rows)
				{
					dataRow = currentData.CustomerContactTable.NewRow();
					dataRow.BeginEdit();
					if (!(row.Cells["ContactID"].Value.ToString() == ""))
					{
						dataRow["CustomerID"] = textBoxCode.Text;
						dataRow["ContactID"] = row.Cells["ContactID"].Value.ToString();
						dataRow["Note"] = row.Cells["Note"].Value.ToString();
						dataRow["RowIndex"] = row.Index;
						dataRow["JobTitle"] = row.Cells["JobTitle"].Value.ToString();
						dataRow.EndEdit();
						currentData.CustomerContactTable.Rows.Add(dataRow);
					}
				}
				FormHelper formHelper = new FormHelper();
				currentData = (CustomerData)formHelper.GetEntityUDFData("Customer", "CustomerID", "", currentData, layoutControl1);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			try
			{
				if (!screenRight.New && isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
					return false;
				}
				if (!screenRight.Edit && !isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
				if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Customer", "CustomerID", textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("You dont have permission to edit.");
					return false;
				}
				textBoxCode.Text = textBoxCode.Text.Trim();
				if (!screenRight.New && isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
					return false;
				}
				if (!screenRight.Edit && !isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
				if (textBoxName.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					textBoxName.Focus();
					textBoxName.SelectAll();
					return false;
				}
				if (textBoxCode.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					tabPageGeneral.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Customer", "CustomerID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Selected = true;
					textBoxCode.Focus();
					return false;
				}
				if (textBoxCode.Text == comboBoxParentCustomer.SelectedID)
				{
					ErrorHelper.WarningMessage("A customer cannot be parent of itself.");
					tabPageGeneral.Selected = true;
					comboBoxParentCustomer.Focus();
					return false;
				}
				if (!isNewRecord && checkBoxIsInactive.Checked && Factory.CustomerSystem.HasBalance(textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("A customer that has balance cannot be inactive.");
					return false;
				}
				if (radioButtonSublimit.Checked && comboBoxParentCustomer.SelectedID == "")
				{
					ErrorHelper.WarningMessage("Parent customer must be selected when the credit limit is set to Sublimit or select a different credit limit type.");
					comboBoxParentCustomer.Focus();
					return false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return true;
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.CustomerSystem.CreateCustomer(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Customer, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.CustomerSystem.UpdateCustomer(currentData);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else if (clearAfter)
				{
					ClearForm();
					IsNewRecord = true;
				}
				else
				{
					formManager.ResetDirty();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed)
				{
					isLoading = true;
					if (!(id == "") && CanClose())
					{
						PublicFunctions.StartWaiting(this);
						currentData = Factory.CustomerSystem.GetCustomerByID(id);
						if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
						{
							ClearForm();
							IsNewRecord = true;
							textBoxCode.Text = id;
							textBoxCode.Focus();
						}
						else
						{
							FillData();
							DataSet transactiondetails = Factory.CustomerSystem.GetTransactiondetails(id);
							if (transactiondetails == null || transactiondetails.Tables.Count == 0 || transactiondetails.Tables[0].Rows.Count == 0)
							{
								comboBoxCurrency.Enabled = true;
							}
							else
							{
								comboBoxCurrency.Enabled = false;
							}
							IsNewRecord = false;
							DataSet entityCommentList = Factory.EntityCommentSystem.GetEntityCommentList(EntityTypesEnum.Customers, textBoxCode.Text);
							if (entityCommentList != null && entityCommentList.Tables.Count > 0 && entityCommentList.Tables[0].Rows.Count > 0)
							{
								toolStripButtonComments.Image = imageListComments.Images[1];
							}
							else
							{
								toolStripButtonComments.Image = imageListComments.Images[0];
							}
							LoadActivities();
							formManager.ResetDirty();
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
			finally
			{
				isLoading = false;
				PublicFunctions.EndWaiting(this);
			}
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Sales;
		}

		public static int GetScreenID()
		{
			return 7003;
		}

		public void RefreshData()
		{
			Refresh();
			_ = Global.ConStatus;
			_ = 2;
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Customer", "CustomerID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Customer", "CustomerID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Customer", "CustomerID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Customer", "CustomerID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Customer", "CustomerID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.No)
				{
					return false;
				}
				return Factory.CustomerSystem.DeleteCustomer(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			dateTimePickerReviewDate.Clear();
			dateTimePickerEstablished.Clear();
			dateTimePickerCustomerSince.Clear();
			dateTimePickerLicenseExpDate.Clear();
			dateTimePickerContractExpDate.Clear();
			comboBoxCreditReviewBy.Clear();
			comboBoxRating.SelectedIndex = 0;
			textBoxTempLimit.Text = 0.ToString(Format.TotalAmountFormat);
			comboBoxInsuranceProvider.Clear();
			comboBoxCurrency.Enabled = true;
			comboBoxStatementMethod.SelectedIndex = 0;
			textBoxStatementEmail.Clear();
			textBoxProfileDetails.Text = "";
			textBoxRatingRemarks.Clear();
			comboBoxRatingBy.Clear();
			dateTimePickerRatingDate.Checked = false;
			dateTimePickerRatingDate.Value = DateTime.Now;
			dateTimePickerCLValidity.Clear();
			dateTimePickerCLValidity.Checked = false;
			comboBoxInsuranceRating.SelectedIndex = 0;
			datetimePickerEffectiveDate.Clear();
			datetimePickerEffectiveDate.Checked = false;
			dateTimePickerValidTo.Clear();
			dateTimePickerValidTo.Checked = false;
			checkBoxWeightInvoice.Checked = false;
			checkBoxparentACforposting.Checked = false;
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxAddress1.Clear();
			comboBoxCollectionUser.Clear();
			textBoxAddress2.Clear();
			textBoxAddress3.Clear();
			textBoxAddressID.Text = "PRIMARY";
			comboBoxBilltoAddress.Text = "PRIMARY";
			comboBoxShiptoAddress.Text = "PRIMARY";
			textBoxAddressPrintFormat.Clear();
			textBoxBankAccountNumber.Clear();
			textBoxBankBranch.Clear();
			comboBoxCurrency.Clear();
			textBoxBankName.Clear();
			textBoxCity.Clear();
			textBoxComment.Clear();
			textBoxContactName.Clear();
			textBoxCountry.Clear();
			textBoxLatitude.Text = "25.2824891";
			textBoxLatitude.ForeColor = Color.Gray;
			textBoxLongitude.Text = "55.3583311";
			textBoxLongitude.ForeColor = Color.Gray;
			textBoxCreditLimit.Clear();
			textBoxDepartment.Clear();
			textBoxEmail.Clear();
			textBoxFax.Clear();
			textBoxForeignName.Clear();
			textBoxFormalName.Clear();
			textBoxMobile.Clear();
			textBoxPhone1.Clear();
			textBoxPhone2.Clear();
			textBoxPostalCode.Clear();
			textBoxState.Clear();
			textBoxWebsite.Clear();
			checkBoxIsInactive.Checked = false;
			checkBoxAllowConsignment.Checked = false;
			textBoxConsignCommission.Text = "0.00";
			textBoxDiscountPercent.Text = "0.00";
			textBoxRebatePercent.Text = "0.00";
			textBoxDeliveryInstructions.Clear();
			textBoxAccountInstructions.Clear();
			comboBoxCustomerGroup.Clear();
			comboBoxInsuranceStatus.SelectedIndex = 0;
			dateTimePickerInsuranceDate.Checked = false;
			textBoxInsuranceApprovedAmount.SetZero();
			textBoxInsuranceNumber.Clear();
			textBoxInsuranceRemarks.Clear();
			textBoxInsuranceReqAmount.SetZero();
			textBoxInsuranceID.Clear();
			textBoxLicenseNumber.Clear();
			textBoxUnsecuredLimit.Clear();
			checkBoxUnsecuredLimit.Checked = false;
			udfEntryGrid.ClearData();
			comboBoxArea.Clear();
			comboBoxLeadSource.Clear();
			comboBoxCountry.Clear();
			comboBoxCustomerClass.Clear();
			comboBoxParentCustomer.Clear();
			comboBoxPaymentMethods.Clear();
			comboBoxPaymentTerms.Clear();
			comboBoxPriceLevel.Clear();
			comboBoxSalesperson.Clear();
			comboBoxShippingMethods.Clear();
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptPDC.Checked = true;
			checkBoxHold.Checked = false;
			radioButtonCreditLimitNoCredit.Checked = true;
			textBoxGraceDays.Text = "0";
			ARAccountID = "";
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			dateTimeBalanceConfirmationDate.Checked = false;
			textBoxConfirmationLevel.Clear();
			textBoxTaxIDNumber.Clear();
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			if (CompanyPreferences.TaxEntityTypes.Contains("C"))
			{
				comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
				comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
			}
			IsNewRecord = true;
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Customer", "CustomerID");
			(dataGridContacts.DataSource as DataTable).Rows.Clear();
			(dataGridActivities.DataSource as DataTable).Rows.Clear();
			new FormHelper().ClearUDFData("Customer", "CustomerID", "", layoutControl1);
			formManager.ResetDirty();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		public void OnActivated()
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		private void CustomerClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		public bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return true;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentTerm(comboBoxPaymentTerms.Text);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentMethod(comboBoxPaymentMethods.Text);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void linkLabelPriceLevel_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPriceLevel(comboBoxPriceLevel.Text);
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, textBoxAddressID.Text);
		}

		private void radioButtonCreditLimitAmount_CheckedChanged(object sender, EventArgs e)
		{
			AmountTextBox amountTextBox = textBoxCreditLimit;
			MMSDateTimePicker mMSDateTimePicker = dateTimePickerCLValidity;
			bool flag = checkBoxUnsecuredLimit.Enabled = radioButtonCreditLimitAmount.Checked;
			bool enabled = mMSDateTimePicker.Enabled = flag;
			amountTextBox.Enabled = enabled;
		}

		private void comboBoxCustomerClass_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(textBoxCode.Text == "") && (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false))))
				{
					DataSet customerProfileReport = Factory.CustomerSystem.GetCustomerProfileReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", "", "", "", "", showInactive: true, "");
					if (customerProfileReport == null || customerProfileReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(customerProfileReport, "", "Customer Profile", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerListFormObj);
		}

		private void buttonCategories_Click(object sender, EventArgs e)
		{
			EntityCategoryAssignDialog entityCategoryAssignDialog = new EntityCategoryAssignDialog();
			entityCategoryAssignDialog.EntityID = textBoxCode.Text;
			entityCategoryAssignDialog.EntityName = textBoxName.Text;
			entityCategoryAssignDialog.EntityType = EntityTypesEnum.Customers;
			entityCategoryAssignDialog.IsTreeView = true;
			if (!screenRight.Edit)
			{
				entityCategoryAssignDialog.AllowEdit = false;
			}
			entityCategoryAssignDialog.ShowDialog(this);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Customers;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void openContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridContacts.ActiveRow != null && dataGridContacts.ActiveRow.Cells["ContactID"].Value != null && !(dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString() == ""))
			{
				string id = dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString();
				new FormHelper().EditContact(id);
			}
		}

		private void newContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			new FormHelper().EditContact(empty);
		}

		private void deleteContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridContacts.ActiveRow != null && dataGridContacts.ActiveRow.Cells["ContactID"].Value != null && !(dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString() == ""))
			{
				string iD = dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString();
				Factory.ContactSystem.DeleteContact(iD);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void comboBoxRating_ValueChanged(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				textBoxInsuranceRemarks.Text += "  ";
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", 1, this);
			}
		}

		private void buttonAddActivity_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
				FormActivator.ActivityDetailsFormObj.AddNewActivity(CRMRelatedTypes.Customer, textBoxCode.Text);
			}
		}

		private void comboBoxActivityPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadActivities();
		}

		private void SetupActivityGrid()
		{
			try
			{
				dataGridContacts.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("Type");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Contact");
				dataTable.Columns.Add("Performed By");
				dataTable.Columns.Add("Date", typeof(DateTime));
				dataTable.Columns.Add("Time", typeof(DateTime));
				dataGridActivities.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridActivities.DisplayLayout.Bands[0].Columns["SysDocID"];
				bool hidden = dataGridActivities.DisplayLayout.Bands[0].Columns["VoucherID"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				dataGridActivities.DisplayLayout.Bands[0].Columns["Time"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Time;
				dataGridActivities.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadActivities()
		{
			try
			{
				if (!isNewRecord && GlobalRules.IsModuleAvailable(AxolonModules.CRM))
				{
					DataSet activityListByLeadID = Factory.ActivitySystem.GetActivityListByLeadID(CRMRelatedTypes.Customer, textBoxCode.Text, comboBoxActivityPeriod.FromDate, comboBoxActivityPeriod.ToDate);
					DataTable dataTable = dataGridActivities.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in activityListByLeadID.Tables["Activity"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["SysDocID"] = row["Doc ID"];
						dataRow2["VoucherID"] = row["Number"];
						dataRow2["Name"] = row["Activity Name"];
						dataRow2["Type"] = row["Activity Type"];
						dataRow2["Contact"] = row["Contact"];
						dataRow2["Performed By"] = row["Performed By"];
						dataRow2["Date"] = row["Date"];
						dataRow2["Time"] = row["Date"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					dataTable.AcceptChanges();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadLeadData()
		{
			if (SourceLeadID != "")
			{
				LeadData leadByID = Factory.LeadSystem.GetLeadByID(SourceLeadID);
				FillLeadData(leadByID);
				textBoxCode.ReadOnly = false;
			}
		}

		private void FillLeadData(DataSet currentLeadData)
		{
			try
			{
				if (currentLeadData != null && currentLeadData.Tables.Count != 0 && currentLeadData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentLeadData.Tables[0].Rows[0];
					textBoxCode.Text = "";
					textBoxName.Text = dataRow["LeadName"].ToString();
					textBoxForeignName.Text = dataRow["ForeignName"].ToString();
					textBoxFormalName.Text = dataRow["ShortName"].ToString();
					comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
					comboBoxArea.SelectedID = dataRow["AreaID"].ToString();
					comboBoxLeadSource.SelectedID = dataRow["LeadSourceID"].ToString();
					checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxProfileDetails.WordMLText = dataRow["ProfileDetails"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalesPersonID"].ToString();
					if (dataRow["Rating"] != DBNull.Value)
					{
						comboBoxRating.SelectedIndex = int.Parse(dataRow["Rating"].ToString());
					}
					else
					{
						comboBoxRating.SelectedIndex = 0;
					}
					if (dataRow["DateEstablished"] != DBNull.Value)
					{
						dateTimePickerEstablished.Value = DateTime.Parse(dataRow["DateEstablished"].ToString());
						dateTimePickerEstablished.Checked = true;
					}
					else
					{
						dateTimePickerEstablished.IsNull = true;
						dateTimePickerEstablished.Checked = false;
					}
					if (dataRow["CreditReviewDate"] != DBNull.Value)
					{
						dateTimePickerReviewDate.Value = DateTime.Parse(dataRow["CreditReviewDate"].ToString());
						dateTimePickerReviewDate.Checked = true;
					}
					else
					{
						dateTimePickerReviewDate.IsNull = true;
						dateTimePickerReviewDate.Checked = false;
					}
					if (dataRow["CreditReviewBy"] != DBNull.Value)
					{
						comboBoxCreditReviewBy.SelectedID = dataRow["CreditReviewBy"].ToString();
					}
					else
					{
						comboBoxCreditReviewBy.Clear();
					}
					switch (1)
					{
					case 3:
						radioButtonCreditLimitAmount.Checked = true;
						break;
					case 2:
						radioButtonCreditLimitNoCredit.Checked = true;
						break;
					case 4:
						radioButtonSublimit.Checked = true;
						break;
					default:
						radioButtonCreditLimitUnlimited.Checked = true;
						break;
					}
					SetHeaderName();
					if (currentLeadData.Tables.Contains("Lead_Address") && currentLeadData.Tables[1].Rows.Count != 0)
					{
						dataRow = currentLeadData.Tables[1].Rows[0];
						FillAddressData(dataRow);
						if (currentLeadData.Tables.Contains("Lead_Contact_Detail") && currentLeadData.Tables[2].Rows.Count != 0)
						{
							DataTable dataTable = dataGridContacts.DataSource as DataTable;
							dataTable.Rows.Clear();
							foreach (DataRow row in currentLeadData.Tables["Lead_Contact_Detail"].Rows)
							{
								DataRow dataRow3 = dataTable.NewRow();
								foreach (DataColumn column in dataTable.Columns)
								{
									if (dataRow3.Table.Columns.Contains(column.ColumnName))
									{
										dataRow3[column.ColumnName] = row[column.ColumnName];
									}
									else
									{
										ErrorHelper.ErrorMessage(column.ColumnName + " Does not exist.");
									}
								}
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
							}
							dataTable.AcceptChanges();
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void textBoxProfileDetails_ContentChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void toolStripButtonComments_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					EntityCommentsForm entityCommentsForm = new EntityCommentsForm();
					entityCommentsForm.EntityID = textBoxCode.Text;
					entityCommentsForm.EntityName = textBoxName.Text;
					entityCommentsForm.EntityType = EntityTypesEnum.Customers;
					entityCommentsForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void tabControlTab_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void buttonLimitModify_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				CreditlimitReviewForm creditlimitReviewForm = new CreditlimitReviewForm();
				creditlimitReviewForm.StartPosition = FormStartPosition.CenterScreen;
				creditlimitReviewForm.StartPosition = FormStartPosition.Manual;
				creditlimitReviewForm.Location = checked(new Point(base.Location.X + (base.Width - creditlimitReviewForm.Width) * 2, base.Location.Y + (base.Height - creditlimitReviewForm.Height) * 2));
				creditlimitReviewForm.CustomerID = textBoxCode.Text;
				Factory.CreditLimitReviewSystem.GetCustomerIndividualsByID(textBoxCode.Text);
				creditlimitReviewForm.ShowDialog();
				LoadData(textBoxCode.Text);
			}
		}

		private void ultraPictureBox1_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			text = textBoxLatitude.Text;
			text2 = textBoxLongitude.Text;
			if (text != "" && text2 != "")
			{
				Process.Start("https://www.google.com/maps/preview/?q=" + text + "," + text2);
			}
			else
			{
				Process.Start("https://www.google.com/maps/preview/?q=25.2048° N, 55.2708° E");
			}
		}

		private void textBoxLatitude_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLongitude_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLatitude_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLongitude_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLatitude_KeyDown(object sender, KeyPressEventArgs e)
		{
		}

		private void textBoxLatitude_MouseClick(object sender, MouseEventArgs e)
		{
			if (textBoxLatitude.Text.Equals("25.2824891"))
			{
				textBoxLatitude.Text = "";
				textBoxLatitude.ForeColor = Color.Black;
			}
			else if (textBoxLatitude.Text != "")
			{
				textBoxLatitude.ForeColor = Color.Black;
			}
		}

		private void textBoxLongitude_MouseClick(object sender, MouseEventArgs e)
		{
			if (textBoxLongitude.Text.Equals("55.3583311"))
			{
				textBoxLongitude.Text = "";
				textBoxLongitude.ForeColor = Color.Black;
			}
			else if (textBoxLongitude.Text != "")
			{
				textBoxLongitude.ForeColor = Color.Black;
			}
		}

		private void textBoxLatitude_MouseLeave(object sender, EventArgs e)
		{
			if (textBoxLatitude.Text.Equals(null) || textBoxLatitude.Text.Equals(""))
			{
				textBoxLatitude.Text = "25.2824891";
				textBoxLatitude.ForeColor = Color.Gray;
			}
		}

		private void textBoxLongitude_MouseLeave(object sender, EventArgs e)
		{
			if (textBoxLongitude.Text.Equals(null) || textBoxLongitude.Text.Equals(""))
			{
				textBoxLongitude.Text = "55.3583311";
				textBoxLongitude.ForeColor = Color.Gray;
			}
		}

		private void comboBoxParentCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxParentCustomer.SelectedID == "")
			{
				checkBoxparentACforposting.Checked = false;
				checkBoxparentACforposting.Enabled = false;
			}
			else
			{
				checkBoxparentACforposting.Enabled = true;
			}
		}

		private void ultraFormattedLinkLabel9_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 1)
			{
				comboBoxTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.Clear();
		}

		private void checkBoxUnsecuredLimit_CheckedChanged(object sender, EventArgs e)
		{
			textBoxUnsecuredLimit.Enabled = checkBoxUnsecuredLimit.Checked;
		}

		private void buttonAccounts_Click(object sender, EventArgs e)
		{
			PayeeAccountsForm payeeAccountsForm = new PayeeAccountsForm();
			payeeAccountsForm.ARAccount = ARAccountID;
			payeeAccountsForm.EntityType = EntityTypesEnum.Customers;
			if (payeeAccountsForm.ShowDialog() == DialogResult.OK)
			{
				ARAccountID = payeeAccountsForm.ARAccount;
				if (!formManager.IsForcedDirty)
				{
					formManager.IsForcedDirty = payeeAccountsForm.IsDirty;
				}
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void Deleted_menuItem_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				return;
			}
			ToolStripItem toolStripItem = (ToolStripItem)sender;
			if (toolStripItem.Name == "Customer Ledger")
			{
				CustomerLedgerForm customerLedgerForm = new CustomerLedgerForm();
				customerLedgerForm.SelectedID = textBoxCode.Text;
				customerLedgerForm.Show();
				customerLedgerForm.BringToFront();
			}
			else if (toolStripItem.Name == "Sale Statistics")
			{
				InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
				inventorySalesStatisticForm.ShowCustomer = true;
				inventorySalesStatisticForm.SelectedCode = textBoxCode.Text;
				inventorySalesStatisticForm.Show();
				inventorySalesStatisticForm.BringToFront();
			}
			else if (toolStripItem.Name == "Transaction Details")
			{
				ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
				comboSearchDialogNew.IsMultiSelect = false;
				DataSet dataSet = new DataSet();
				dataSet = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
				comboSearchDialogNew.SelectedItem = "";
				if (textBoxCode.Text != "" && textBoxCode.Text != null)
				{
					comboSearchDialogNew.SelectedProvider = textBoxCode.Text;
				}
				comboSearchDialogNew.ShowDialog();
			}
		}

		private void buttonCustomerInsuranceClaim_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerInsuranceClaimFormObj);
		}

		private void openContactToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
		}

		private void saleStatistcsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonHistory_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				DocumentVersionList documentVersionList = new DocumentVersionList();
				documentVersionList.LoadData(currentData, ScreenTypes.Card, 1, "", textBoxCode.Text);
				documentVersionList.ShowDialog();
			}
		}

		private void txtDays_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddCustomerSignature(textBoxCode.Text, image))
					{
						pictureBoxPhoto.Image = image;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add picture.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.CustomerSystem.RemoveCustomerSignature(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = null;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetCustomerSignatureThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void ultraFormattedLinkLabel6_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void PlantiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CaseClientDetailsFormObj);
			FormActivator.CaseClientDetailsFormObj.SourceID = textBoxCode.Text;
			FormActivator.CaseClientDetailsFormObj.ClientType = "C";
			FormActivator.CaseClientDetailsFormObj.IsPlantiff = true;
			FormActivator.CaseClientDetailsFormObj.LoadCustomerData();
		}

		private void defendantToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CaseClientDetailsFormObj);
			FormActivator.CaseClientDetailsFormObj.SourceID = textBoxCode.Text;
			FormActivator.CaseClientDetailsFormObj.ClientType = "C";
			FormActivator.CaseClientDetailsFormObj.IsDefendant = true;
			FormActivator.CaseClientDetailsFormObj.LoadCustomerData();
		}

		private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void layoutControlItem9_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerClass(comboBoxCustomerClass.Text);
		}

		private void layoutControlItem10_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerGroup(comboBoxCustomerGroup.SelectedID);
		}

		private void layoutControlItem11_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCountry(comboBoxCountry.Text);
		}

		private void layoutControlItem12_Click(object sender, EventArgs e)
		{
			new FormHelper().EditArea(comboBoxArea.Text);
		}

		private void layoutControlItem14_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.Text);
		}

		private void layoutControlItem38_Click(object sender, EventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.Text);
		}

		private void layoutControlItem39_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, comboBoxShiptoAddress.SelectedID);
		}

		private void layoutControlItem41_Click(object sender, EventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.LeadSource, comboBoxLeadSource.SelectedID);
		}

		private void layoutControlItem42_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, comboBoxBilltoAddress.SelectedID);
		}

		private void layoutControlItem43_Click(object sender, EventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethods.Text);
		}

		private void layoutControlItem59_Click(object sender, EventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
		}

		private void layoutControlItem64_Click(object sender, EventArgs e)
		{
			new FormHelper().EditPaymentMethod(comboBoxPaymentMethods.Text);
		}

		private void layoutControlItem66_Click(object sender, EventArgs e)
		{
			new FormHelper().EditPaymentTerm(comboBoxPaymentTerms.Text);
		}

		private void toolStripButtonDesignLayout_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCustomerLedger_Click(object sender, EventArgs e)
		{
			CustomerLedgerForm customerLedgerForm = new CustomerLedgerForm();
			customerLedgerForm.SelectedID = textBoxCode.Text;
			customerLedgerForm.Show();
			customerLedgerForm.BringToFront();
		}

		private void menuItemSalesStatistics_Click(object sender, EventArgs e)
		{
			InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
			inventorySalesStatisticForm.ShowCustomer = true;
			inventorySalesStatisticForm.SelectedCode = textBoxCode.Text;
			inventorySalesStatisticForm.Show();
			inventorySalesStatisticForm.BringToFront();
		}

		private void transactionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
			comboSearchDialogNew.IsMultiSelect = false;
			DataSet dataSet = new DataSet();
			dataSet = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
			comboSearchDialogNew.SelectedItem = "";
			if (textBoxCode.Text != "" && textBoxCode.Text != null)
			{
				comboSearchDialogNew.SelectedProvider = textBoxCode.Text;
			}
			comboSearchDialogNew.ShowDialog();
		}

		private void menuItemLayoutDesign_Click(object sender, EventArgs e)
		{
			new FormHelper().CustomizeLayout(layoutControl1);
		}

		private void menuItemCustomFields_Click(object sender, EventArgs e)
		{
			new UDFSetupForm(base.Name, "Customer").ShowDialog();
		}
	}
}
