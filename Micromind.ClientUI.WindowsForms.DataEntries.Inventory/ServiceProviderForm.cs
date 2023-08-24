using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ServiceProviderForm : Form, IDataForm, IDataEntry
	{
		private VendorData currentData;

		private const string TABLENAME_CONST = "Vendor";

		private const string IDFIELD_CONST = "VendorID";

		private bool isNewRecord = true;

		private MMTextBox textBoxName;

		private MMLabel label1;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxCode;

		private MMLabel labelVendorNumber;

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

		private MMLabel mmLabel5;

		private FormManager formManager;

		private MMTextBox textBoxForeignName;

		private MMLabel mmLabel6;

		private CheckBox checkBoxHold;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private Panel panel1;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel8;

		private MMTextBox textBoxAddressID;

		private MMLabel mmLabel20;

		private MMTextBox textBoxPostalCode;

		private MMLabel mmLabel18;

		private MMTextBox textBoxEmail;

		private MMLabel mmLabel17;

		private MMTextBox textBoxMobile;

		private MMLabel mmLabel16;

		private MMTextBox textBoxFax;

		private MMLabel mmLabel15;

		private MMTextBox textBoxPhone2;

		private MMLabel mmLabel14;

		private MMTextBox textBoxPhone1;

		private MMLabel mmLabel12;

		private MMTextBox textBoxCountry;

		private MMLabel mmLabel11;

		private MMTextBox textBoxState;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCity;

		private MMTextBox textBoxAddress3;

		private MMTextBox textBoxAddress2;

		private MMLabel mmLabel10;

		private MMTextBox textBoxAddress1;

		private MMLabel mmLabel9;

		private MMTextBox textBoxContactName;

		private MMTextBox textBoxAddressPrintFormat;

		private MMLabel mmLabel21;

		private XPButton buttonMoreAddress;

		private MMLabel mmLabel19;

		private MMTextBox textBoxWebsite;

		private MMLabel mmLabel22;

		private MMTextBox textBoxDepartment;

		private MMLabel mmLabel23;

		private MMTextBox textBoxComment;

		private UltraTabPageControl tabPageUserDefined;

		private UltraGroupBox ultraGroupBox2;

		private AllAccountsComboBox comboBoxARAccount;

		private AmountTextBox textBoxCreditLimit;

		private RadioButton radioButtonCreditLimitAmount;

		private RadioButton radioButtonCreditLimitUnlimited;

		private MMLabel mmLabel30;

		private MMTextBox textBoxBankAccountNumber;

		private MMLabel mmLabel29;

		private MMTextBox textBoxBankBranch;

		private MMLabel mmLabel28;

		private MMTextBox textBoxBankName;

		private MMLabel mmLabel32;

		private UltraGroupBox ultraGroupBox4;

		private CheckBox checkBoxAcceptPDC;

		private CheckBox checkBoxAcceptCheque;

		private UltraGroupBox ultraGroupBox3;

		private UltraTabPageControl tabPageContacts;

		private DataEntryGrid dataGridContacts;

		private MMLabel mmLabel35;

		private vendorsFlatComboBox comboBoxParentVendor;

		private VendorClassComboBox comboBoxVendorClass;

		private CountryComboBox comboBoxCountry;

		private AreaComboBox comboBoxArea;

		private paymentMethodsComboBox comboBoxPaymentMethods;

		private BuyerComboBox comboBoxBuyer;

		private PaymentTermComboBox comboBoxPaymentTerms;

		private ShippingMethodsComboBox comboBoxShippingMethods;

		private UltraFormattedLinkLabel linkLabelVendorClass;

		private UltraFormattedLinkLabel linkLabelArea;

		private UltraFormattedLinkLabel linkLabelCountry;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel linkLabelARAccount;

		private RadioButton radioButtonCreditLimitNoCredit;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMTextBox textBoxARName;

		private CurrencyComboBox comboBoxCurrency;

		private MMLabel mmLabel1;

		private MMLabel labelCustomerNameHeader;

		private ContactsComboBox gridComboBoxContact;

		private UDFEntryControl udfEntryGrid;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private ContextMenuStrip contextMenuStripContact;

		private ToolStripMenuItem openContactToolStripMenuItem;

		private ToolStripMenuItem newContactToolStripMenuItem;

		private ToolStripMenuItem deleteContactToolStripMenuItem;

		private PercentTextBox textBoxConsignCommission;

		private MMLabel mmLabel3;

		private MMLabel mmLabel2;

		private CheckBox checkBoxAllowConsignment;

		private Panel panelConsignComm;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel7;

		private MMLabel mmLabel4;

		private MMSDateTimePicker dateTimePickerContractExpDate;

		private MMSDateTimePicker dateTimePickerLicenseExpDate;

		private UltraGroupBox ultraGroupBox7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private TaxGroupComboBox comboBoxTaxGroup;

		private MMLabel mmLabel25;

		private ComboBox comboBoxTaxOption;

		private MMTextBox textBoxTaxIDNumber;

		private MMLabel mmLabel58;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isLoadingData;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 3005;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					textBoxAddressID.Enabled = false;
				}
				toolStripButtonAttach.Enabled = !value;
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

		public ServiceProviderForm()
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ServiceProviderForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			linkLabelArea = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelVendorClass = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			textBoxComment = new Micromind.UISupport.MMTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxDepartment = new Micromind.UISupport.MMTextBox();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxWebsite = new Micromind.UISupport.MMTextBox();
			buttonMoreAddress = new Micromind.UISupport.XPButton();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxAddressPrintFormat = new Micromind.UISupport.MMTextBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxPostalCode = new Micromind.UISupport.MMTextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxEmail = new Micromind.UISupport.MMTextBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxMobile = new Micromind.UISupport.MMTextBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxFax = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			textBoxPhone2 = new Micromind.UISupport.MMTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxPhone1 = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxCountry = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxState = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCity = new Micromind.UISupport.MMTextBox();
			textBoxAddress3 = new Micromind.UISupport.MMTextBox();
			textBoxAddress2 = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxAddress1 = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxContactName = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxAddressID = new Micromind.UISupport.MMTextBox();
			checkBoxHold = new System.Windows.Forms.CheckBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelVendorNumber = new Micromind.UISupport.MMLabel();
			textBoxFormalName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxForeignName = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dateTimePickerLicenseExpDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimePickerContractExpDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			panelConsignComm = new System.Windows.Forms.Panel();
			textBoxConsignCommission = new Micromind.UISupport.PercentTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			checkBoxAllowConsignment = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxCreditLimit = new Micromind.UISupport.AmountTextBox();
			radioButtonCreditLimitNoCredit = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitUnlimited = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitAmount = new System.Windows.Forms.RadioButton();
			checkBoxAcceptPDC = new System.Windows.Forms.CheckBox();
			checkBoxAcceptCheque = new System.Windows.Forms.CheckBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel28 = new Micromind.UISupport.MMLabel();
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			textBoxBankBranch = new Micromind.UISupport.MMTextBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			textBoxBankAccountNumber = new Micromind.UISupport.MMTextBox();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxARName = new Micromind.UISupport.MMTextBox();
			linkLabelARAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageContacts = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			contextMenuStripContact = new System.Windows.Forms.ContextMenuStrip(components);
			openContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			textBoxTaxIDNumber = new Micromind.UISupport.MMTextBox();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			comboBoxArea = new Micromind.DataControls.AreaComboBox();
			comboBoxCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxVendorClass = new Micromind.DataControls.VendorClassComboBox();
			comboBoxParentVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			comboBoxShippingMethods = new Micromind.DataControls.ShippingMethodsComboBox();
			comboBoxPaymentTerms = new Micromind.DataControls.PaymentTermComboBox();
			comboBoxPaymentMethods = new Micromind.DataControls.paymentMethodsComboBox();
			comboBoxBuyer = new Micromind.DataControls.BuyerComboBox();
			comboBoxARAccount = new Micromind.DataControls.AllAccountsComboBox();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			dataGridContacts = new Micromind.DataControls.DataEntryGrid();
			gridComboBoxContact = new Micromind.DataControls.ContactsComboBox();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			tabPageDetails.SuspendLayout();
			panelConsignComm.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			tabPageContacts.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStripContact.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).BeginInit();
			ultraGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendorClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethods).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentTerms).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethods).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBuyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).BeginInit();
			SuspendLayout();
			tabPageGeneral.Controls.Add(comboBoxCurrency);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(linkLabelArea);
			tabPageGeneral.Controls.Add(linkLabelCountry);
			tabPageGeneral.Controls.Add(linkLabelVendorClass);
			tabPageGeneral.Controls.Add(comboBoxArea);
			tabPageGeneral.Controls.Add(comboBoxCountry);
			tabPageGeneral.Controls.Add(comboBoxVendorClass);
			tabPageGeneral.Controls.Add(comboBoxParentVendor);
			tabPageGeneral.Controls.Add(mmLabel32);
			tabPageGeneral.Controls.Add(ultraGroupBox1);
			tabPageGeneral.Controls.Add(checkBoxHold);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(labelVendorNumber);
			tabPageGeneral.Controls.Add(textBoxFormalName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxForeignName);
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(696, 441);
			tabPageGeneral.Paint += new System.Windows.Forms.PaintEventHandler(tabPageGeneral_Paint);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(371, 101);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(55, 13);
			mmLabel1.TabIndex = 13;
			mmLabel1.Text = "Currency:";
			linkLabelArea.AutoSize = true;
			linkLabelArea.Location = new System.Drawing.Point(371, 79);
			linkLabelArea.Name = "linkLabelArea";
			linkLabelArea.Size = new System.Drawing.Size(30, 14);
			linkLabelArea.TabIndex = 11;
			linkLabelArea.TabStop = true;
			linkLabelArea.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelArea.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelArea.Value = "Area:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkLabelArea.VisitedLinkAppearance = appearance;
			linkLabelArea.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelArea_LinkClicked);
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(371, 57);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(46, 14);
			linkLabelCountry.TabIndex = 9;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Country:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance2;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			linkLabelVendorClass.AutoSize = true;
			appearance3.ForeColor = System.Drawing.Color.Blue;
			linkLabelVendorClass.LinkAppearance = appearance3;
			linkLabelVendorClass.Location = new System.Drawing.Point(371, 34);
			linkLabelVendorClass.Name = "linkLabelVendorClass";
			linkLabelVendorClass.Size = new System.Drawing.Size(73, 14);
			linkLabelVendorClass.TabIndex = 2;
			linkLabelVendorClass.TabStop = true;
			linkLabelVendorClass.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVendorClass.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVendorClass.Value = "Vendor Class:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			linkLabelVendorClass.VisitedLinkAppearance = appearance4;
			linkLabelVendorClass.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(9, 100);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(80, 13);
			mmLabel32.TabIndex = 14;
			mmLabel32.Text = "Parent Vendor:";
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel23);
			ultraGroupBox1.Controls.Add(textBoxComment);
			ultraGroupBox1.Controls.Add(mmLabel22);
			ultraGroupBox1.Controls.Add(textBoxDepartment);
			ultraGroupBox1.Controls.Add(mmLabel19);
			ultraGroupBox1.Controls.Add(textBoxWebsite);
			ultraGroupBox1.Controls.Add(buttonMoreAddress);
			ultraGroupBox1.Controls.Add(mmLabel21);
			ultraGroupBox1.Controls.Add(textBoxAddressPrintFormat);
			ultraGroupBox1.Controls.Add(mmLabel20);
			ultraGroupBox1.Controls.Add(textBoxPostalCode);
			ultraGroupBox1.Controls.Add(mmLabel18);
			ultraGroupBox1.Controls.Add(textBoxEmail);
			ultraGroupBox1.Controls.Add(mmLabel17);
			ultraGroupBox1.Controls.Add(textBoxMobile);
			ultraGroupBox1.Controls.Add(mmLabel16);
			ultraGroupBox1.Controls.Add(textBoxFax);
			ultraGroupBox1.Controls.Add(mmLabel15);
			ultraGroupBox1.Controls.Add(textBoxPhone2);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(textBoxPhone1);
			ultraGroupBox1.Controls.Add(mmLabel12);
			ultraGroupBox1.Controls.Add(textBoxCountry);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(textBoxState);
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(textBoxCity);
			ultraGroupBox1.Controls.Add(textBoxAddress3);
			ultraGroupBox1.Controls.Add(textBoxAddress2);
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(textBoxAddress1);
			ultraGroupBox1.Controls.Add(mmLabel9);
			ultraGroupBox1.Controls.Add(textBoxContactName);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(textBoxAddressID);
			ultraGroupBox1.Location = new System.Drawing.Point(0, 138);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(687, 314);
			ultraGroupBox1.TabIndex = 1;
			ultraGroupBox1.Text = "Primary Address";
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(371, 176);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(56, 13);
			mmLabel23.TabIndex = 32;
			mmLabel23.Text = "Comment:";
			textBoxComment.BackColor = System.Drawing.Color.White;
			textBoxComment.CustomReportFieldName = "";
			textBoxComment.CustomReportKey = "";
			textBoxComment.CustomReportValueType = 1;
			textBoxComment.IsComboTextBox = false;
			textBoxComment.IsModified = false;
			textBoxComment.Location = new System.Drawing.Point(442, 173);
			textBoxComment.MaxLength = 255;
			textBoxComment.Name = "textBoxComment";
			textBoxComment.Size = new System.Drawing.Size(229, 20);
			textBoxComment.TabIndex = 15;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(371, 22);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(68, 13);
			mmLabel22.TabIndex = 21;
			mmLabel22.Text = "Department:";
			textBoxDepartment.BackColor = System.Drawing.Color.White;
			textBoxDepartment.CustomReportFieldName = "";
			textBoxDepartment.CustomReportKey = "";
			textBoxDepartment.CustomReportValueType = 1;
			textBoxDepartment.IsComboTextBox = false;
			textBoxDepartment.IsModified = false;
			textBoxDepartment.Location = new System.Drawing.Point(442, 19);
			textBoxDepartment.MaxLength = 30;
			textBoxDepartment.Name = "textBoxDepartment";
			textBoxDepartment.Size = new System.Drawing.Size(229, 20);
			textBoxDepartment.TabIndex = 22;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(371, 154);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(50, 13);
			mmLabel19.TabIndex = 30;
			mmLabel19.Text = "Website:";
			textBoxWebsite.BackColor = System.Drawing.Color.White;
			textBoxWebsite.CustomReportFieldName = "";
			textBoxWebsite.CustomReportKey = "";
			textBoxWebsite.CustomReportValueType = 1;
			textBoxWebsite.IsComboTextBox = false;
			textBoxWebsite.IsModified = false;
			textBoxWebsite.Location = new System.Drawing.Point(442, 151);
			textBoxWebsite.MaxLength = 255;
			textBoxWebsite.Name = "textBoxWebsite";
			textBoxWebsite.Size = new System.Drawing.Size(229, 20);
			textBoxWebsite.TabIndex = 13;
			buttonMoreAddress.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonMoreAddress.BackColor = System.Drawing.Color.DarkGray;
			buttonMoreAddress.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonMoreAddress.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonMoreAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonMoreAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonMoreAddress.Location = new System.Drawing.Point(537, 199);
			buttonMoreAddress.Name = "buttonMoreAddress";
			buttonMoreAddress.Size = new System.Drawing.Size(134, 24);
			buttonMoreAddress.TabIndex = 19;
			buttonMoreAddress.Text = "More Addresses...";
			buttonMoreAddress.UseVisualStyleBackColor = false;
			buttonMoreAddress.Click += new System.EventHandler(buttonMoreAddress_Click);
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(9, 218);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(112, 13);
			mmLabel21.TabIndex = 29;
			mmLabel21.Text = "Address Print Format:";
			textBoxAddressPrintFormat.BackColor = System.Drawing.Color.White;
			textBoxAddressPrintFormat.CustomReportFieldName = "";
			textBoxAddressPrintFormat.CustomReportKey = "";
			textBoxAddressPrintFormat.CustomReportValueType = 1;
			textBoxAddressPrintFormat.IsComboTextBox = false;
			textBoxAddressPrintFormat.IsModified = false;
			textBoxAddressPrintFormat.Location = new System.Drawing.Point(132, 217);
			textBoxAddressPrintFormat.MaxLength = 255;
			textBoxAddressPrintFormat.Multiline = true;
			textBoxAddressPrintFormat.Name = "textBoxAddressPrintFormat";
			textBoxAddressPrintFormat.Size = new System.Drawing.Size(229, 74);
			textBoxAddressPrintFormat.TabIndex = 17;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(9, 198);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(68, 13);
			mmLabel20.TabIndex = 28;
			mmLabel20.Text = "Postal Code:";
			textBoxPostalCode.BackColor = System.Drawing.Color.White;
			textBoxPostalCode.CustomReportFieldName = "";
			textBoxPostalCode.CustomReportKey = "";
			textBoxPostalCode.CustomReportValueType = 1;
			textBoxPostalCode.IsComboTextBox = false;
			textBoxPostalCode.IsModified = false;
			textBoxPostalCode.Location = new System.Drawing.Point(132, 195);
			textBoxPostalCode.MaxLength = 30;
			textBoxPostalCode.Name = "textBoxPostalCode";
			textBoxPostalCode.Size = new System.Drawing.Size(229, 20);
			textBoxPostalCode.TabIndex = 16;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(371, 132);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(35, 13);
			mmLabel18.TabIndex = 28;
			mmLabel18.Text = "Email:";
			textBoxEmail.BackColor = System.Drawing.Color.White;
			textBoxEmail.CustomReportFieldName = "";
			textBoxEmail.CustomReportKey = "";
			textBoxEmail.CustomReportValueType = 1;
			textBoxEmail.IsComboTextBox = false;
			textBoxEmail.IsModified = false;
			textBoxEmail.Location = new System.Drawing.Point(442, 129);
			textBoxEmail.MaxLength = 64;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(229, 20);
			textBoxEmail.TabIndex = 11;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(371, 110);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(41, 13);
			mmLabel17.TabIndex = 26;
			mmLabel17.Text = "Mobile:";
			textBoxMobile.BackColor = System.Drawing.Color.White;
			textBoxMobile.CustomReportFieldName = "";
			textBoxMobile.CustomReportKey = "";
			textBoxMobile.CustomReportValueType = 1;
			textBoxMobile.IsComboTextBox = false;
			textBoxMobile.IsModified = false;
			textBoxMobile.Location = new System.Drawing.Point(442, 107);
			textBoxMobile.MaxLength = 30;
			textBoxMobile.Name = "textBoxMobile";
			textBoxMobile.Size = new System.Drawing.Size(229, 20);
			textBoxMobile.TabIndex = 9;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(371, 87);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(29, 13);
			mmLabel16.TabIndex = 24;
			mmLabel16.Text = "Fax:";
			textBoxFax.BackColor = System.Drawing.Color.White;
			textBoxFax.CustomReportFieldName = "";
			textBoxFax.CustomReportKey = "";
			textBoxFax.CustomReportValueType = 1;
			textBoxFax.IsComboTextBox = false;
			textBoxFax.IsModified = false;
			textBoxFax.Location = new System.Drawing.Point(442, 85);
			textBoxFax.MaxLength = 30;
			textBoxFax.Name = "textBoxFax";
			textBoxFax.Size = new System.Drawing.Size(229, 20);
			textBoxFax.TabIndex = 7;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(371, 66);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(50, 13);
			mmLabel15.TabIndex = 22;
			mmLabel15.Text = "Phone 2:";
			textBoxPhone2.BackColor = System.Drawing.Color.White;
			textBoxPhone2.CustomReportFieldName = "";
			textBoxPhone2.CustomReportKey = "";
			textBoxPhone2.CustomReportValueType = 1;
			textBoxPhone2.IsComboTextBox = false;
			textBoxPhone2.IsModified = false;
			textBoxPhone2.Location = new System.Drawing.Point(442, 63);
			textBoxPhone2.MaxLength = 30;
			textBoxPhone2.Name = "textBoxPhone2";
			textBoxPhone2.Size = new System.Drawing.Size(229, 20);
			textBoxPhone2.TabIndex = 5;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(371, 44);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(50, 13);
			mmLabel14.TabIndex = 20;
			mmLabel14.Text = "Phone 1:";
			textBoxPhone1.BackColor = System.Drawing.Color.White;
			textBoxPhone1.CustomReportFieldName = "";
			textBoxPhone1.CustomReportKey = "";
			textBoxPhone1.CustomReportValueType = 1;
			textBoxPhone1.IsComboTextBox = false;
			textBoxPhone1.IsModified = false;
			textBoxPhone1.Location = new System.Drawing.Point(442, 41);
			textBoxPhone1.MaxLength = 30;
			textBoxPhone1.Name = "textBoxPhone1";
			textBoxPhone1.Size = new System.Drawing.Size(229, 20);
			textBoxPhone1.TabIndex = 32;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(9, 176);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(50, 13);
			mmLabel12.TabIndex = 27;
			mmLabel12.Text = "Country:";
			textBoxCountry.BackColor = System.Drawing.Color.White;
			textBoxCountry.CustomReportFieldName = "";
			textBoxCountry.CustomReportKey = "";
			textBoxCountry.CustomReportValueType = 1;
			textBoxCountry.IsComboTextBox = false;
			textBoxCountry.IsModified = false;
			textBoxCountry.Location = new System.Drawing.Point(132, 173);
			textBoxCountry.MaxLength = 30;
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(229, 20);
			textBoxCountry.TabIndex = 14;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(9, 154);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(37, 13);
			mmLabel11.TabIndex = 26;
			mmLabel11.Text = "State:";
			textBoxState.BackColor = System.Drawing.Color.White;
			textBoxState.CustomReportFieldName = "";
			textBoxState.CustomReportKey = "";
			textBoxState.CustomReportValueType = 1;
			textBoxState.IsComboTextBox = false;
			textBoxState.IsModified = false;
			textBoxState.Location = new System.Drawing.Point(132, 151);
			textBoxState.MaxLength = 30;
			textBoxState.Name = "textBoxState";
			textBoxState.Size = new System.Drawing.Size(229, 20);
			textBoxState.TabIndex = 12;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(9, 131);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(30, 13);
			mmLabel13.TabIndex = 25;
			mmLabel13.Text = "City:";
			textBoxCity.BackColor = System.Drawing.Color.White;
			textBoxCity.CustomReportFieldName = "";
			textBoxCity.CustomReportKey = "";
			textBoxCity.CustomReportValueType = 1;
			textBoxCity.IsComboTextBox = false;
			textBoxCity.IsModified = false;
			textBoxCity.Location = new System.Drawing.Point(132, 129);
			textBoxCity.MaxLength = 30;
			textBoxCity.Name = "textBoxCity";
			textBoxCity.Size = new System.Drawing.Size(229, 20);
			textBoxCity.TabIndex = 11;
			textBoxAddress3.BackColor = System.Drawing.Color.White;
			textBoxAddress3.CustomReportFieldName = "";
			textBoxAddress3.CustomReportKey = "";
			textBoxAddress3.CustomReportValueType = 1;
			textBoxAddress3.IsComboTextBox = false;
			textBoxAddress3.IsModified = false;
			textBoxAddress3.Location = new System.Drawing.Point(132, 107);
			textBoxAddress3.MaxLength = 64;
			textBoxAddress3.Name = "textBoxAddress3";
			textBoxAddress3.Size = new System.Drawing.Size(229, 20);
			textBoxAddress3.TabIndex = 13;
			textBoxAddress2.BackColor = System.Drawing.Color.White;
			textBoxAddress2.CustomReportFieldName = "";
			textBoxAddress2.CustomReportKey = "";
			textBoxAddress2.CustomReportValueType = 1;
			textBoxAddress2.IsComboTextBox = false;
			textBoxAddress2.IsModified = false;
			textBoxAddress2.Location = new System.Drawing.Point(132, 85);
			textBoxAddress2.MaxLength = 64;
			textBoxAddress2.Name = "textBoxAddress2";
			textBoxAddress2.Size = new System.Drawing.Size(229, 20);
			textBoxAddress2.TabIndex = 15;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(9, 65);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(50, 13);
			mmLabel10.TabIndex = 24;
			mmLabel10.Text = "Address:";
			textBoxAddress1.BackColor = System.Drawing.Color.White;
			textBoxAddress1.CustomReportFieldName = "";
			textBoxAddress1.CustomReportKey = "";
			textBoxAddress1.CustomReportValueType = 1;
			textBoxAddress1.IsComboTextBox = false;
			textBoxAddress1.IsModified = false;
			textBoxAddress1.Location = new System.Drawing.Point(132, 63);
			textBoxAddress1.MaxLength = 64;
			textBoxAddress1.Name = "textBoxAddress1";
			textBoxAddress1.Size = new System.Drawing.Size(229, 20);
			textBoxAddress1.TabIndex = 31;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(9, 43);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(79, 13);
			mmLabel9.TabIndex = 23;
			mmLabel9.Text = "Contact Name:";
			textBoxContactName.BackColor = System.Drawing.Color.White;
			textBoxContactName.CustomReportFieldName = "";
			textBoxContactName.CustomReportKey = "";
			textBoxContactName.CustomReportValueType = 1;
			textBoxContactName.IsComboTextBox = false;
			textBoxContactName.IsModified = false;
			textBoxContactName.Location = new System.Drawing.Point(132, 41);
			textBoxContactName.MaxLength = 64;
			textBoxContactName.Name = "textBoxContactName";
			textBoxContactName.Size = new System.Drawing.Size(229, 20);
			textBoxContactName.TabIndex = 8;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 22);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(64, 13);
			mmLabel8.TabIndex = 22;
			mmLabel8.Text = "Address ID:";
			textBoxAddressID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAddressID.CustomReportFieldName = "";
			textBoxAddressID.CustomReportKey = "";
			textBoxAddressID.CustomReportValueType = 1;
			textBoxAddressID.Enabled = false;
			textBoxAddressID.ForeColor = System.Drawing.Color.Black;
			textBoxAddressID.IsComboTextBox = false;
			textBoxAddressID.IsModified = false;
			textBoxAddressID.Location = new System.Drawing.Point(132, 19);
			textBoxAddressID.MaxLength = 15;
			textBoxAddressID.Name = "textBoxAddressID";
			textBoxAddressID.Size = new System.Drawing.Size(229, 20);
			textBoxAddressID.TabIndex = 23;
			textBoxAddressID.Text = "PRIMARY";
			checkBoxHold.BackColor = System.Drawing.Color.Transparent;
			checkBoxHold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxHold.Location = new System.Drawing.Point(479, 10);
			checkBoxHold.Name = "checkBoxHold";
			checkBoxHold.Size = new System.Drawing.Size(87, 16);
			checkBoxHold.TabIndex = 2;
			checkBoxHold.Text = "Hold";
			checkBoxHold.UseVisualStyleBackColor = true;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 78);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(73, 13);
			mmLabel5.TabIndex = 1;
			mmLabel5.Text = "Formal Name:";
			labelVendorNumber.AutoSize = true;
			labelVendorNumber.BackColor = System.Drawing.Color.Transparent;
			labelVendorNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelVendorNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelVendorNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelVendorNumber.IsFieldHeader = false;
			labelVendorNumber.IsRequired = true;
			labelVendorNumber.Location = new System.Drawing.Point(9, 10);
			labelVendorNumber.Name = "labelVendorNumber";
			labelVendorNumber.PenWidth = 1f;
			labelVendorNumber.ShowBorder = false;
			labelVendorNumber.Size = new System.Drawing.Size(81, 13);
			labelVendorNumber.TabIndex = 0;
			labelVendorNumber.Text = "Vendor Code:";
			labelVendorNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxFormalName.BackColor = System.Drawing.Color.White;
			textBoxFormalName.CustomReportFieldName = "";
			textBoxFormalName.CustomReportKey = "";
			textBoxFormalName.CustomReportValueType = 1;
			textBoxFormalName.IsComboTextBox = false;
			textBoxFormalName.IsModified = false;
			textBoxFormalName.Location = new System.Drawing.Point(132, 76);
			textBoxFormalName.MaxLength = 64;
			textBoxFormalName.Name = "textBoxFormalName";
			textBoxFormalName.Size = new System.Drawing.Size(229, 20);
			textBoxFormalName.TabIndex = 7;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(132, 9);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(229, 20);
			textBoxCode.TabIndex = 0;
			textBoxForeignName.BackColor = System.Drawing.Color.White;
			textBoxForeignName.CustomReportFieldName = "";
			textBoxForeignName.CustomReportKey = "";
			textBoxForeignName.CustomReportValueType = 1;
			textBoxForeignName.IsComboTextBox = false;
			textBoxForeignName.IsModified = false;
			textBoxForeignName.IsRequired = true;
			textBoxForeignName.Location = new System.Drawing.Point(132, 53);
			textBoxForeignName.MaxLength = 64;
			textBoxForeignName.Name = "textBoxForeignName";
			textBoxForeignName.Size = new System.Drawing.Size(229, 20);
			textBoxForeignName.TabIndex = 5;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(374, 10);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(87, 16);
			checkBoxIsInactive.TabIndex = 1;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.IsRequired = true;
			textBoxName.Location = new System.Drawing.Point(132, 31);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(229, 20);
			textBoxName.TabIndex = 3;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 54);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(77, 13);
			mmLabel6.TabIndex = 1;
			mmLabel6.Text = "Foreign Name:";
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = true;
			label1.Location = new System.Drawing.Point(9, 31);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(85, 13);
			label1.TabIndex = 20;
			label1.Text = "Vendor Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			tabPageDetails.Controls.Add(ultraGroupBox7);
			tabPageDetails.Controls.Add(dateTimePickerLicenseExpDate);
			tabPageDetails.Controls.Add(dateTimePickerContractExpDate);
			tabPageDetails.Controls.Add(mmLabel7);
			tabPageDetails.Controls.Add(mmLabel4);
			tabPageDetails.Controls.Add(panelConsignComm);
			tabPageDetails.Controls.Add(checkBoxAllowConsignment);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel8);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel7);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel6);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel5);
			tabPageDetails.Controls.Add(comboBoxShippingMethods);
			tabPageDetails.Controls.Add(comboBoxPaymentTerms);
			tabPageDetails.Controls.Add(comboBoxPaymentMethods);
			tabPageDetails.Controls.Add(comboBoxBuyer);
			tabPageDetails.Controls.Add(ultraGroupBox4);
			tabPageDetails.Controls.Add(checkBoxAcceptPDC);
			tabPageDetails.Controls.Add(checkBoxAcceptCheque);
			tabPageDetails.Controls.Add(ultraGroupBox3);
			tabPageDetails.Controls.Add(ultraGroupBox2);
			tabPageDetails.Location = new System.Drawing.Point(2, 21);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(696, 441);
			dateTimePickerLicenseExpDate.Checked = false;
			dateTimePickerLicenseExpDate.CustomFormat = " ";
			dateTimePickerLicenseExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerLicenseExpDate.Location = new System.Drawing.Point(552, 5);
			dateTimePickerLicenseExpDate.Name = "dateTimePickerLicenseExpDate";
			dateTimePickerLicenseExpDate.ShowCheckBox = true;
			dateTimePickerLicenseExpDate.Size = new System.Drawing.Size(141, 20);
			dateTimePickerLicenseExpDate.TabIndex = 5;
			dateTimePickerLicenseExpDate.Value = new System.DateTime(0L);
			dateTimePickerContractExpDate.Checked = false;
			dateTimePickerContractExpDate.CustomFormat = " ";
			dateTimePickerContractExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerContractExpDate.Location = new System.Drawing.Point(552, 29);
			dateTimePickerContractExpDate.Name = "dateTimePickerContractExpDate";
			dateTimePickerContractExpDate.ShowCheckBox = true;
			dateTimePickerContractExpDate.Size = new System.Drawing.Size(141, 20);
			dateTimePickerContractExpDate.TabIndex = 6;
			dateTimePickerContractExpDate.Value = new System.DateTime(0L);
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(445, 33);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(101, 13);
			mmLabel7.TabIndex = 16;
			mmLabel7.Text = "Contract Exp.Date:";
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(452, 9);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(94, 13);
			mmLabel4.TabIndex = 15;
			mmLabel4.Text = "License Exp.Date:";
			panelConsignComm.Controls.Add(textBoxConsignCommission);
			panelConsignComm.Controls.Add(mmLabel3);
			panelConsignComm.Controls.Add(mmLabel2);
			panelConsignComm.Location = new System.Drawing.Point(293, 126);
			panelConsignComm.Name = "panelConsignComm";
			panelConsignComm.Size = new System.Drawing.Size(227, 27);
			panelConsignComm.TabIndex = 11;
			panelConsignComm.Visible = false;
			textBoxConsignCommission.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsignCommission.CustomReportFieldName = "";
			textBoxConsignCommission.CustomReportKey = "";
			textBoxConsignCommission.CustomReportValueType = 1;
			textBoxConsignCommission.IsComboTextBox = false;
			textBoxConsignCommission.IsModified = false;
			textBoxConsignCommission.Location = new System.Drawing.Point(114, 4);
			textBoxConsignCommission.Name = "textBoxConsignCommission";
			textBoxConsignCommission.ReadOnly = true;
			textBoxConsignCommission.Size = new System.Drawing.Size(86, 20);
			textBoxConsignCommission.TabIndex = 1;
			textBoxConsignCommission.Text = "0.00";
			textBoxConsignCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(204, 7);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(18, 13);
			mmLabel3.TabIndex = 15;
			mmLabel3.Text = "%";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(8, 6);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(106, 13);
			mmLabel2.TabIndex = 0;
			mmLabel2.Text = "Commission Percent:";
			checkBoxAllowConsignment.AutoSize = true;
			checkBoxAllowConsignment.Location = new System.Drawing.Point(286, 110);
			checkBoxAllowConsignment.Name = "checkBoxAllowConsignment";
			checkBoxAllowConsignment.Size = new System.Drawing.Size(209, 17);
			checkBoxAllowConsignment.TabIndex = 9;
			checkBoxAllowConsignment.Text = "Allow to accept goods on consignment";
			checkBoxAllowConsignment.UseVisualStyleBackColor = true;
			checkBoxAllowConsignment.CheckedChanged += new System.EventHandler(checkBoxAllowConsignment_CheckedChanged);
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(10, 83);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(89, 14);
			ultraFormattedLinkLabel8.TabIndex = 6;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Shipping Method:";
			appearance5.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance5;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(10, 59);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabel7.TabIndex = 4;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Payment Term:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(10, 35);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(89, 14);
			ultraFormattedLinkLabel6.TabIndex = 2;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Payment Method:";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance7;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 11);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(36, 14);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Buyer:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			ultraGroupBox4.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox4.Controls.Add(textBoxCreditLimit);
			ultraGroupBox4.Controls.Add(radioButtonCreditLimitNoCredit);
			ultraGroupBox4.Controls.Add(radioButtonCreditLimitUnlimited);
			ultraGroupBox4.Controls.Add(radioButtonCreditLimitAmount);
			ultraGroupBox4.Location = new System.Drawing.Point(5, 158);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(596, 48);
			ultraGroupBox4.TabIndex = 12;
			ultraGroupBox4.Text = "Credit Limit";
			textBoxCreditLimit.AllowDecimal = true;
			textBoxCreditLimit.CustomReportFieldName = "";
			textBoxCreditLimit.CustomReportKey = "";
			textBoxCreditLimit.CustomReportValueType = 1;
			textBoxCreditLimit.Enabled = false;
			textBoxCreditLimit.IsComboTextBox = false;
			textBoxCreditLimit.IsModified = false;
			textBoxCreditLimit.Location = new System.Drawing.Point(295, 19);
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
			textBoxCreditLimit.Size = new System.Drawing.Size(139, 20);
			textBoxCreditLimit.TabIndex = 3;
			textBoxCreditLimit.Text = "0.00";
			textBoxCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxCreditLimit.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			radioButtonCreditLimitNoCredit.AutoSize = true;
			radioButtonCreditLimitNoCredit.Location = new System.Drawing.Point(107, 20);
			radioButtonCreditLimitNoCredit.Name = "radioButtonCreditLimitNoCredit";
			radioButtonCreditLimitNoCredit.Size = new System.Drawing.Size(69, 17);
			radioButtonCreditLimitNoCredit.TabIndex = 1;
			radioButtonCreditLimitNoCredit.Text = "No Credit";
			radioButtonCreditLimitNoCredit.UseVisualStyleBackColor = true;
			radioButtonCreditLimitUnlimited.AutoSize = true;
			radioButtonCreditLimitUnlimited.Checked = true;
			radioButtonCreditLimitUnlimited.Location = new System.Drawing.Point(10, 20);
			radioButtonCreditLimitUnlimited.Name = "radioButtonCreditLimitUnlimited";
			radioButtonCreditLimitUnlimited.Size = new System.Drawing.Size(68, 17);
			radioButtonCreditLimitUnlimited.TabIndex = 0;
			radioButtonCreditLimitUnlimited.TabStop = true;
			radioButtonCreditLimitUnlimited.Text = "Unlimited";
			radioButtonCreditLimitUnlimited.UseVisualStyleBackColor = true;
			radioButtonCreditLimitAmount.AutoSize = true;
			radioButtonCreditLimitAmount.Location = new System.Drawing.Point(213, 20);
			radioButtonCreditLimitAmount.Name = "radioButtonCreditLimitAmount";
			radioButtonCreditLimitAmount.Size = new System.Drawing.Size(76, 17);
			radioButtonCreditLimitAmount.TabIndex = 2;
			radioButtonCreditLimitAmount.Text = "Amount of:";
			radioButtonCreditLimitAmount.UseVisualStyleBackColor = true;
			radioButtonCreditLimitAmount.CheckedChanged += new System.EventHandler(radioButtonCreditLimitAmount_CheckedChanged);
			checkBoxAcceptPDC.AutoSize = true;
			checkBoxAcceptPDC.Checked = true;
			checkBoxAcceptPDC.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptPDC.Location = new System.Drawing.Point(14, 133);
			checkBoxAcceptPDC.Name = "checkBoxAcceptPDC";
			checkBoxAcceptPDC.Size = new System.Drawing.Size(195, 17);
			checkBoxAcceptPDC.TabIndex = 8;
			checkBoxAcceptPDC.Text = "Accept post-dated cheque payment";
			checkBoxAcceptPDC.UseVisualStyleBackColor = true;
			checkBoxAcceptCheque.AutoSize = true;
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptCheque.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptCheque.Location = new System.Drawing.Point(14, 110);
			checkBoxAcceptCheque.Name = "checkBoxAcceptCheque";
			checkBoxAcceptCheque.Size = new System.Drawing.Size(142, 17);
			checkBoxAcceptCheque.TabIndex = 7;
			checkBoxAcceptCheque.Text = "Accept cheque payment";
			checkBoxAcceptCheque.UseVisualStyleBackColor = true;
			ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox3.Controls.Add(mmLabel28);
			ultraGroupBox3.Controls.Add(textBoxBankName);
			ultraGroupBox3.Controls.Add(textBoxBankBranch);
			ultraGroupBox3.Controls.Add(mmLabel29);
			ultraGroupBox3.Controls.Add(textBoxBankAccountNumber);
			ultraGroupBox3.Controls.Add(mmLabel30);
			ultraGroupBox3.Location = new System.Drawing.Point(5, 212);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(346, 109);
			ultraGroupBox3.TabIndex = 13;
			ultraGroupBox3.Text = "Bank Account Info";
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(4, 25);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(64, 13);
			mmLabel28.TabIndex = 0;
			mmLabel28.Text = "Bank Name:";
			textBoxBankName.BackColor = System.Drawing.Color.White;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.IsModified = false;
			textBoxBankName.Location = new System.Drawing.Point(133, 22);
			textBoxBankName.MaxLength = 30;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.Size = new System.Drawing.Size(195, 20);
			textBoxBankName.TabIndex = 0;
			textBoxBankBranch.BackColor = System.Drawing.Color.White;
			textBoxBankBranch.CustomReportFieldName = "";
			textBoxBankBranch.CustomReportKey = "";
			textBoxBankBranch.CustomReportValueType = 1;
			textBoxBankBranch.IsComboTextBox = false;
			textBoxBankBranch.IsModified = false;
			textBoxBankBranch.Location = new System.Drawing.Point(133, 44);
			textBoxBankBranch.MaxLength = 100;
			textBoxBankBranch.Multiline = true;
			textBoxBankBranch.Name = "textBoxBankBranch";
			textBoxBankBranch.Size = new System.Drawing.Size(195, 40);
			textBoxBankBranch.TabIndex = 1;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(4, 46);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(70, 13);
			mmLabel29.TabIndex = 2;
			mmLabel29.Text = "Bank Branch:";
			textBoxBankAccountNumber.BackColor = System.Drawing.Color.White;
			textBoxBankAccountNumber.CustomReportFieldName = "";
			textBoxBankAccountNumber.CustomReportKey = "";
			textBoxBankAccountNumber.CustomReportValueType = 1;
			textBoxBankAccountNumber.IsComboTextBox = false;
			textBoxBankAccountNumber.IsModified = false;
			textBoxBankAccountNumber.Location = new System.Drawing.Point(133, 86);
			textBoxBankAccountNumber.MaxLength = 30;
			textBoxBankAccountNumber.Name = "textBoxBankAccountNumber";
			textBoxBankAccountNumber.Size = new System.Drawing.Size(195, 20);
			textBoxBankAccountNumber.TabIndex = 2;
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(4, 87);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(92, 13);
			mmLabel30.TabIndex = 4;
			mmLabel30.Text = "Bank Account No:";
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(textBoxARName);
			ultraGroupBox2.Controls.Add(linkLabelARAccount);
			ultraGroupBox2.Controls.Add(comboBoxARAccount);
			ultraGroupBox2.Location = new System.Drawing.Point(5, 333);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(596, 53);
			ultraGroupBox2.TabIndex = 14;
			ultraGroupBox2.Text = "Accounts";
			textBoxARName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxARName.CustomReportFieldName = "";
			textBoxARName.CustomReportKey = "";
			textBoxARName.CustomReportValueType = 1;
			textBoxARName.IsComboTextBox = true;
			textBoxARName.IsModified = false;
			textBoxARName.Location = new System.Drawing.Point(331, 19);
			textBoxARName.MaxLength = 30;
			textBoxARName.Name = "textBoxARName";
			textBoxARName.ReadOnly = true;
			textBoxARName.Size = new System.Drawing.Size(218, 20);
			textBoxARName.TabIndex = 1;
			textBoxARName.TabStop = false;
			linkLabelARAccount.AutoSize = true;
			linkLabelARAccount.Location = new System.Drawing.Point(7, 24);
			linkLabelARAccount.Name = "linkLabelARAccount";
			linkLabelARAccount.Size = new System.Drawing.Size(90, 14);
			linkLabelARAccount.TabIndex = 0;
			linkLabelARAccount.TabStop = true;
			linkLabelARAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelARAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelARAccount.Value = "Account Payable:";
			appearance9.ForeColor = System.Drawing.Color.Blue;
			linkLabelARAccount.VisitedLinkAppearance = appearance9;
			linkLabelARAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelARAccount_LinkClicked);
			tabPageContacts.Controls.Add(mmLabel35);
			tabPageContacts.Controls.Add(dataGridContacts);
			tabPageContacts.Controls.Add(gridComboBoxContact);
			tabPageContacts.Location = new System.Drawing.Point(-10000, -10000);
			tabPageContacts.Name = "tabPageContacts";
			tabPageContacts.Size = new System.Drawing.Size(696, 441);
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(10, 15);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(161, 13);
			mmLabel35.TabIndex = 354;
			mmLabel35.Text = "Contacts related to this vendor:";
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(696, 441);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(8, 5);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(681, 430);
			textBoxNote.TabIndex = 43;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(696, 441);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(tabPageContacts);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 60);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(700, 464);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance10.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance10;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = tabPageContacts;
			ultraTab3.Text = "Con&tacts";
			ultraTab4.TabPage = ultraTabPageControl1;
			ultraTab4.Text = "&Note";
			ultraTab5.TabPage = tabPageUserDefined;
			ultraTab5.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[5]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(696, 441);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 524);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(700, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(700, 1);
			linePanelDown.TabIndex = 3;
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
			buttonClose.Location = new System.Drawing.Point(590, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 4;
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
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
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(700, 31);
			toolStrip1.TabIndex = 4;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
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
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(700, 29);
			panel1.TabIndex = 4;
			labelCustomerNameHeader.AutoSize = true;
			labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNameHeader.IsFieldHeader = false;
			labelCustomerNameHeader.IsRequired = true;
			labelCustomerNameHeader.Location = new System.Drawing.Point(25, 7);
			labelCustomerNameHeader.Name = "labelCustomerNameHeader";
			labelCustomerNameHeader.PenWidth = 1f;
			labelCustomerNameHeader.ShowBorder = false;
			labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
			labelCustomerNameHeader.TabIndex = 2;
			labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			newContactToolStripMenuItem.Name = "newContactToolStripMenuItem";
			newContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			newContactToolStripMenuItem.Text = "New Contact";
			deleteContactToolStripMenuItem.Name = "deleteContactToolStripMenuItem";
			deleteContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			deleteContactToolStripMenuItem.Text = "Delete Contact";
			ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox7.Controls.Add(ultraFormattedLinkLabel10);
			ultraGroupBox7.Controls.Add(comboBoxTaxGroup);
			ultraGroupBox7.Controls.Add(mmLabel25);
			ultraGroupBox7.Controls.Add(comboBoxTaxOption);
			ultraGroupBox7.Controls.Add(textBoxTaxIDNumber);
			ultraGroupBox7.Controls.Add(mmLabel58);
			ultraGroupBox7.Location = new System.Drawing.Point(366, 212);
			ultraGroupBox7.Name = "ultraGroupBox7";
			ultraGroupBox7.Size = new System.Drawing.Size(297, 104);
			ultraGroupBox7.TabIndex = 17;
			ultraGroupBox7.Text = "Tax Details";
			ultraFormattedLinkLabel10.AutoSize = true;
			appearance11.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.LinkAppearance = appearance11;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(4, 49);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel10.TabIndex = 73;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Tax Group:";
			appearance12.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance12;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(4, 26);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(64, 13);
			mmLabel25.TabIndex = 70;
			mmLabel25.Text = "Tax Option:";
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[4]
			{
				"Based on Class",
				"Taxable",
				"NonTaxable",
				"Reverse Charge"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(98, 22);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(195, 21);
			comboBoxTaxOption.TabIndex = 0;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			textBoxTaxIDNumber.BackColor = System.Drawing.Color.White;
			textBoxTaxIDNumber.CustomReportFieldName = "";
			textBoxTaxIDNumber.CustomReportKey = "";
			textBoxTaxIDNumber.CustomReportValueType = 1;
			textBoxTaxIDNumber.IsComboTextBox = false;
			textBoxTaxIDNumber.IsModified = false;
			textBoxTaxIDNumber.Location = new System.Drawing.Point(98, 67);
			textBoxTaxIDNumber.MaxLength = 75;
			textBoxTaxIDNumber.Name = "textBoxTaxIDNumber";
			textBoxTaxIDNumber.Size = new System.Drawing.Size(195, 20);
			textBoxTaxIDNumber.TabIndex = 2;
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(4, 71);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(83, 13);
			mmLabel58.TabIndex = 67;
			mmLabel58.Text = "Tax ID Number:";
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance13;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(467, 98);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(204, 20);
			comboBoxCurrency.TabIndex = 10;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxArea.Assigned = false;
			comboBoxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArea.CustomReportFieldName = "";
			comboBoxArea.CustomReportKey = "";
			comboBoxArea.CustomReportValueType = 1;
			comboBoxArea.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArea.DisplayLayout.Appearance = appearance25;
			comboBoxArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArea.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArea.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArea.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArea.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxArea.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxArea.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArea.Editable = true;
			comboBoxArea.FilterString = "";
			comboBoxArea.HasAllAccount = false;
			comboBoxArea.HasCustom = false;
			comboBoxArea.IsDataLoaded = false;
			comboBoxArea.Location = new System.Drawing.Point(467, 76);
			comboBoxArea.MaxDropDownItems = 12;
			comboBoxArea.MaxLength = 15;
			comboBoxArea.Name = "comboBoxArea";
			comboBoxArea.ShowInactiveItems = false;
			comboBoxArea.ShowQuickAdd = true;
			comboBoxArea.Size = new System.Drawing.Size(204, 20);
			comboBoxArea.TabIndex = 8;
			comboBoxArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCountry.Assigned = false;
			comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCountry.CustomReportFieldName = "";
			comboBoxCountry.CustomReportKey = "";
			comboBoxCountry.CustomReportValueType = 1;
			comboBoxCountry.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCountry.DisplayLayout.Appearance = appearance37;
			comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCountry.Editable = true;
			comboBoxCountry.FilterString = "";
			comboBoxCountry.HasAllAccount = false;
			comboBoxCountry.HasCustom = false;
			comboBoxCountry.IsDataLoaded = false;
			comboBoxCountry.Location = new System.Drawing.Point(467, 54);
			comboBoxCountry.MaxDropDownItems = 12;
			comboBoxCountry.MaxLength = 15;
			comboBoxCountry.Name = "comboBoxCountry";
			comboBoxCountry.ShowInactiveItems = false;
			comboBoxCountry.ShowQuickAdd = true;
			comboBoxCountry.Size = new System.Drawing.Size(204, 20);
			comboBoxCountry.TabIndex = 6;
			comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendorClass.Assigned = false;
			comboBoxVendorClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendorClass.CustomReportFieldName = "";
			comboBoxVendorClass.CustomReportKey = "";
			comboBoxVendorClass.CustomReportValueType = 1;
			comboBoxVendorClass.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendorClass.DisplayLayout.Appearance = appearance49;
			comboBoxVendorClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendorClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendorClass.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendorClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxVendorClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendorClass.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxVendorClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendorClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendorClass.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendorClass.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxVendorClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendorClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendorClass.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendorClass.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxVendorClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendorClass.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendorClass.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxVendorClass.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxVendorClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendorClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendorClass.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxVendorClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendorClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxVendorClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendorClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendorClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendorClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendorClass.Editable = true;
			comboBoxVendorClass.FilterString = "";
			comboBoxVendorClass.HasAllAccount = false;
			comboBoxVendorClass.HasCustom = false;
			comboBoxVendorClass.IsDataLoaded = false;
			comboBoxVendorClass.Location = new System.Drawing.Point(467, 32);
			comboBoxVendorClass.MaxDropDownItems = 12;
			comboBoxVendorClass.MaxLength = 15;
			comboBoxVendorClass.Name = "comboBoxVendorClass";
			comboBoxVendorClass.ShowInactiveItems = false;
			comboBoxVendorClass.ShowQuickAdd = true;
			comboBoxVendorClass.Size = new System.Drawing.Size(204, 20);
			comboBoxVendorClass.TabIndex = 4;
			comboBoxVendorClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxParentVendor.Assigned = false;
			comboBoxParentVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxParentVendor.CustomReportFieldName = "";
			comboBoxParentVendor.CustomReportKey = "";
			comboBoxParentVendor.CustomReportValueType = 1;
			comboBoxParentVendor.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxParentVendor.DisplayLayout.Appearance = appearance61;
			comboBoxParentVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxParentVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentVendor.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxParentVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxParentVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxParentVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxParentVendor.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxParentVendor.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxParentVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxParentVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxParentVendor.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxParentVendor.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxParentVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxParentVendor.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentVendor.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxParentVendor.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxParentVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxParentVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxParentVendor.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxParentVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxParentVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxParentVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxParentVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxParentVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxParentVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxParentVendor.Editable = true;
			comboBoxParentVendor.FilterString = "";
			comboBoxParentVendor.FilterSysDocID = "";
			comboBoxParentVendor.HasAll = false;
			comboBoxParentVendor.HasCustom = false;
			comboBoxParentVendor.IsDataLoaded = false;
			comboBoxParentVendor.Location = new System.Drawing.Point(132, 98);
			comboBoxParentVendor.MaxDropDownItems = 12;
			comboBoxParentVendor.MaxLength = 64;
			comboBoxParentVendor.Name = "comboBoxParentVendor";
			comboBoxParentVendor.ShowConsignmentOnly = false;
			comboBoxParentVendor.ShowQuickAdd = true;
			comboBoxParentVendor.Size = new System.Drawing.Size(229, 20);
			comboBoxParentVendor.TabIndex = 9;
			comboBoxParentVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance73;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(98, 45);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(195, 20);
			comboBoxTaxGroup.TabIndex = 1;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShippingMethods.Assigned = false;
			comboBoxShippingMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethods.CustomReportFieldName = "";
			comboBoxShippingMethods.CustomReportKey = "";
			comboBoxShippingMethods.CustomReportValueType = 1;
			comboBoxShippingMethods.DescriptionTextBox = null;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethods.DisplayLayout.Appearance = appearance85;
			comboBoxShippingMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			comboBoxShippingMethods.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethods.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethods.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethods.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			comboBoxShippingMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethods.DisplayLayout.Override.CellAppearance = appearance92;
			comboBoxShippingMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethods.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			comboBoxShippingMethods.DisplayLayout.Override.HeaderAppearance = appearance94;
			comboBoxShippingMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethods.DisplayLayout.Override.RowAppearance = appearance95;
			comboBoxShippingMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
			comboBoxShippingMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethods.Editable = true;
			comboBoxShippingMethods.FilterString = "";
			comboBoxShippingMethods.HasAllAccount = false;
			comboBoxShippingMethods.HasCustom = false;
			comboBoxShippingMethods.IsDataLoaded = false;
			comboBoxShippingMethods.Location = new System.Drawing.Point(138, 81);
			comboBoxShippingMethods.MaxDropDownItems = 12;
			comboBoxShippingMethods.MaxLength = 15;
			comboBoxShippingMethods.Name = "comboBoxShippingMethods";
			comboBoxShippingMethods.ShowInactiveItems = false;
			comboBoxShippingMethods.ShowQuickAdd = true;
			comboBoxShippingMethods.Size = new System.Drawing.Size(194, 20);
			comboBoxShippingMethods.TabIndex = 4;
			comboBoxShippingMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPaymentTerms.Assigned = false;
			comboBoxPaymentTerms.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPaymentTerms.CustomReportFieldName = "";
			comboBoxPaymentTerms.CustomReportKey = "";
			comboBoxPaymentTerms.CustomReportValueType = 1;
			comboBoxPaymentTerms.DescriptionTextBox = null;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPaymentTerms.DisplayLayout.Appearance = appearance97;
			comboBoxPaymentTerms.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPaymentTerms.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance98.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.Appearance = appearance98;
			appearance99.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.BandLabelAppearance = appearance99;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance100.BackColor2 = System.Drawing.SystemColors.Control;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.PromptAppearance = appearance100;
			comboBoxPaymentTerms.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPaymentTerms.DisplayLayout.MaxRowScrollRegions = 1;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPaymentTerms.DisplayLayout.Override.ActiveCellAppearance = appearance101;
			appearance102.BackColor = System.Drawing.SystemColors.Highlight;
			appearance102.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPaymentTerms.DisplayLayout.Override.ActiveRowAppearance = appearance102;
			comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.Override.CardAreaAppearance = appearance103;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			appearance104.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPaymentTerms.DisplayLayout.Override.CellAppearance = appearance104;
			comboBoxPaymentTerms.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPaymentTerms.DisplayLayout.Override.CellPadding = 0;
			appearance105.BackColor = System.Drawing.SystemColors.Control;
			appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance105.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.Override.GroupByRowAppearance = appearance105;
			appearance106.TextHAlignAsString = "Left";
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderAppearance = appearance106;
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.Color.Silver;
			comboBoxPaymentTerms.DisplayLayout.Override.RowAppearance = appearance107;
			comboBoxPaymentTerms.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPaymentTerms.DisplayLayout.Override.TemplateAddRowAppearance = appearance108;
			comboBoxPaymentTerms.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPaymentTerms.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPaymentTerms.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPaymentTerms.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPaymentTerms.Editable = true;
			comboBoxPaymentTerms.FilterString = "";
			comboBoxPaymentTerms.HasAllAccount = false;
			comboBoxPaymentTerms.HasCustom = false;
			comboBoxPaymentTerms.IsDataLoaded = false;
			comboBoxPaymentTerms.Location = new System.Drawing.Point(138, 57);
			comboBoxPaymentTerms.MaxDropDownItems = 12;
			comboBoxPaymentTerms.MaxLength = 15;
			comboBoxPaymentTerms.Name = "comboBoxPaymentTerms";
			comboBoxPaymentTerms.ShowInactiveItems = false;
			comboBoxPaymentTerms.ShowQuickAdd = true;
			comboBoxPaymentTerms.Size = new System.Drawing.Size(194, 20);
			comboBoxPaymentTerms.TabIndex = 3;
			comboBoxPaymentTerms.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPaymentMethods.Assigned = false;
			comboBoxPaymentMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPaymentMethods.CustomReportFieldName = "";
			comboBoxPaymentMethods.CustomReportKey = "";
			comboBoxPaymentMethods.CustomReportValueType = 1;
			comboBoxPaymentMethods.DescriptionTextBox = null;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPaymentMethods.DisplayLayout.Appearance = appearance109;
			comboBoxPaymentMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPaymentMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.Appearance = appearance110;
			appearance111.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance111;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance112.BackColor2 = System.Drawing.SystemColors.Control;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance112;
			comboBoxPaymentMethods.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPaymentMethods.DisplayLayout.MaxRowScrollRegions = 1;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPaymentMethods.DisplayLayout.Override.ActiveCellAppearance = appearance113;
			appearance114.BackColor = System.Drawing.SystemColors.Highlight;
			appearance114.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPaymentMethods.DisplayLayout.Override.ActiveRowAppearance = appearance114;
			comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.Override.CardAreaAppearance = appearance115;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			appearance116.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPaymentMethods.DisplayLayout.Override.CellAppearance = appearance116;
			comboBoxPaymentMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPaymentMethods.DisplayLayout.Override.CellPadding = 0;
			appearance117.BackColor = System.Drawing.SystemColors.Control;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.Override.GroupByRowAppearance = appearance117;
			appearance118.TextHAlignAsString = "Left";
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderAppearance = appearance118;
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			comboBoxPaymentMethods.DisplayLayout.Override.RowAppearance = appearance119;
			comboBoxPaymentMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPaymentMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance120;
			comboBoxPaymentMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPaymentMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPaymentMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPaymentMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPaymentMethods.Editable = true;
			comboBoxPaymentMethods.FilterString = "";
			comboBoxPaymentMethods.IsDataLoaded = false;
			comboBoxPaymentMethods.Location = new System.Drawing.Point(138, 33);
			comboBoxPaymentMethods.MaxDropDownItems = 12;
			comboBoxPaymentMethods.MaxLength = 15;
			comboBoxPaymentMethods.Name = "comboBoxPaymentMethods";
			comboBoxPaymentMethods.Size = new System.Drawing.Size(195, 20);
			comboBoxPaymentMethods.TabIndex = 2;
			comboBoxPaymentMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBuyer.Assigned = false;
			comboBoxBuyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBuyer.CustomReportFieldName = "";
			comboBoxBuyer.CustomReportKey = "";
			comboBoxBuyer.CustomReportValueType = 1;
			comboBoxBuyer.DescriptionTextBox = null;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBuyer.DisplayLayout.Appearance = appearance121;
			comboBoxBuyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBuyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.GroupByBox.Appearance = appearance122;
			appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBuyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
			comboBoxBuyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance124.BackColor2 = System.Drawing.SystemColors.Control;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBuyer.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
			comboBoxBuyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBuyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBuyer.DisplayLayout.Override.ActiveCellAppearance = appearance125;
			appearance126.BackColor = System.Drawing.SystemColors.Highlight;
			appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBuyer.DisplayLayout.Override.ActiveRowAppearance = appearance126;
			comboBoxBuyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBuyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.Override.CardAreaAppearance = appearance127;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBuyer.DisplayLayout.Override.CellAppearance = appearance128;
			comboBoxBuyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBuyer.DisplayLayout.Override.CellPadding = 0;
			appearance129.BackColor = System.Drawing.SystemColors.Control;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.Override.GroupByRowAppearance = appearance129;
			appearance130.TextHAlignAsString = "Left";
			comboBoxBuyer.DisplayLayout.Override.HeaderAppearance = appearance130;
			comboBoxBuyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBuyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			comboBoxBuyer.DisplayLayout.Override.RowAppearance = appearance131;
			comboBoxBuyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBuyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
			comboBoxBuyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBuyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBuyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBuyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBuyer.Editable = true;
			comboBoxBuyer.FilterString = "";
			comboBoxBuyer.HasAllAccount = false;
			comboBoxBuyer.HasCustom = false;
			comboBoxBuyer.IsDataLoaded = false;
			comboBoxBuyer.Location = new System.Drawing.Point(138, 9);
			comboBoxBuyer.MaxDropDownItems = 12;
			comboBoxBuyer.MaxLength = 64;
			comboBoxBuyer.Name = "comboBoxBuyer";
			comboBoxBuyer.ShowInactiveItems = false;
			comboBoxBuyer.ShowQuickAdd = true;
			comboBoxBuyer.Size = new System.Drawing.Size(195, 20);
			comboBoxBuyer.TabIndex = 1;
			comboBoxBuyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxARAccount.Assigned = false;
			comboBoxARAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxARAccount.CustomReportFieldName = "";
			comboBoxARAccount.CustomReportKey = "";
			comboBoxARAccount.CustomReportValueType = 1;
			comboBoxARAccount.DescriptionTextBox = null;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxARAccount.DisplayLayout.Appearance = appearance133;
			comboBoxARAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxARAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.GroupByBox.Appearance = appearance134;
			appearance135.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance135;
			comboBoxARAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance136.BackColor2 = System.Drawing.SystemColors.Control;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance136.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance136;
			comboBoxARAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxARAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxARAccount.DisplayLayout.Override.ActiveCellAppearance = appearance137;
			appearance138.BackColor = System.Drawing.SystemColors.Highlight;
			appearance138.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxARAccount.DisplayLayout.Override.ActiveRowAppearance = appearance138;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.CardAreaAppearance = appearance139;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			appearance140.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxARAccount.DisplayLayout.Override.CellAppearance = appearance140;
			comboBoxARAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxARAccount.DisplayLayout.Override.CellPadding = 0;
			appearance141.BackColor = System.Drawing.SystemColors.Control;
			appearance141.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance141.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.GroupByRowAppearance = appearance141;
			appearance142.TextHAlignAsString = "Left";
			comboBoxARAccount.DisplayLayout.Override.HeaderAppearance = appearance142;
			comboBoxARAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxARAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.Color.Silver;
			comboBoxARAccount.DisplayLayout.Override.RowAppearance = appearance143;
			comboBoxARAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxARAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance144;
			comboBoxARAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxARAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxARAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxARAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxARAccount.Editable = true;
			comboBoxARAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxARAccount.FilterString = "";
			comboBoxARAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxARAccount.FilterSysDocID = "";
			comboBoxARAccount.HasAllAccount = false;
			comboBoxARAccount.HasCustom = false;
			comboBoxARAccount.IsDataLoaded = false;
			comboBoxARAccount.Location = new System.Drawing.Point(133, 19);
			comboBoxARAccount.MaxDropDownItems = 12;
			comboBoxARAccount.MaxLength = 64;
			comboBoxARAccount.Name = "comboBoxARAccount";
			comboBoxARAccount.ShowInactiveItems = false;
			comboBoxARAccount.ShowQuickAdd = true;
			comboBoxARAccount.Size = new System.Drawing.Size(195, 20);
			comboBoxARAccount.TabIndex = 0;
			comboBoxARAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(9, 6);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(677, 431);
			udfEntryGrid.TabIndex = 1;
			dataGridContacts.AllowAddNew = false;
			dataGridContacts.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridContacts.DisplayLayout.Appearance = appearance145;
			dataGridContacts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridContacts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.GroupByBox.Appearance = appearance146;
			appearance147.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance147;
			dataGridContacts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance148.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance148.BackColor2 = System.Drawing.SystemColors.Control;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance148.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.PromptAppearance = appearance148;
			dataGridContacts.DisplayLayout.MaxColScrollRegions = 1;
			dataGridContacts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridContacts.DisplayLayout.Override.ActiveCellAppearance = appearance149;
			appearance150.BackColor = System.Drawing.SystemColors.Highlight;
			appearance150.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridContacts.DisplayLayout.Override.ActiveRowAppearance = appearance150;
			dataGridContacts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridContacts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridContacts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.CardAreaAppearance = appearance151;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			appearance152.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridContacts.DisplayLayout.Override.CellAppearance = appearance152;
			dataGridContacts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridContacts.DisplayLayout.Override.CellPadding = 0;
			appearance153.BackColor = System.Drawing.SystemColors.Control;
			appearance153.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance153.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.GroupByRowAppearance = appearance153;
			appearance154.TextHAlignAsString = "Left";
			dataGridContacts.DisplayLayout.Override.HeaderAppearance = appearance154;
			dataGridContacts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridContacts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.BorderColor = System.Drawing.Color.Silver;
			dataGridContacts.DisplayLayout.Override.RowAppearance = appearance155;
			dataGridContacts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridContacts.DisplayLayout.Override.TemplateAddRowAppearance = appearance156;
			dataGridContacts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridContacts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridContacts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridContacts.LoadLayoutFailed = false;
			dataGridContacts.Location = new System.Drawing.Point(10, 31);
			dataGridContacts.Name = "dataGridContacts";
			dataGridContacts.ShowClearMenu = true;
			dataGridContacts.ShowDeleteMenu = true;
			dataGridContacts.ShowInsertMenu = true;
			dataGridContacts.ShowMoveRowsMenu = true;
			dataGridContacts.Size = new System.Drawing.Size(674, 393);
			dataGridContacts.TabIndex = 0;
			dataGridContacts.Text = "dataEntryGrid1";
			gridComboBoxContact.Assigned = false;
			gridComboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			gridComboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			gridComboBoxContact.CustomReportFieldName = "";
			gridComboBoxContact.CustomReportKey = "";
			gridComboBoxContact.CustomReportValueType = 1;
			gridComboBoxContact.DescriptionTextBox = null;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			gridComboBoxContact.DisplayLayout.Appearance = appearance157;
			gridComboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			gridComboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			gridComboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			gridComboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			gridComboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			gridComboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			gridComboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			gridComboBoxContact.DisplayLayout.Override.CellAppearance = appearance164;
			gridComboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			gridComboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			gridComboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance166;
			gridComboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			gridComboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			gridComboBoxContact.DisplayLayout.Override.RowAppearance = appearance167;
			gridComboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			gridComboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			gridComboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			gridComboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			gridComboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			gridComboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			gridComboBoxContact.Editable = true;
			gridComboBoxContact.FilterString = "";
			gridComboBoxContact.HasAllAccount = false;
			gridComboBoxContact.HasCustom = false;
			gridComboBoxContact.IsDataLoaded = false;
			gridComboBoxContact.Location = new System.Drawing.Point(284, 207);
			gridComboBoxContact.MaxDropDownItems = 12;
			gridComboBoxContact.Name = "gridComboBoxContact";
			gridComboBoxContact.ShowInactiveItems = false;
			gridComboBoxContact.ShowQuickAdd = true;
			gridComboBoxContact.Size = new System.Drawing.Size(127, 20);
			gridComboBoxContact.TabIndex = 356;
			gridComboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			gridComboBoxContact.Visible = false;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 0;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(700, 564);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ServiceProviderForm";
			Text = "Service Provider Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(VendorClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(ServiceProviderForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			panelConsignComm.ResumeLayout(false);
			panelConsignComm.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ultraGroupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			tabPageContacts.ResumeLayout(false);
			tabPageContacts.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStripContact.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).EndInit();
			ultraGroupBox7.ResumeLayout(false);
			ultraGroupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendorClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethods).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentTerms).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethods).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBuyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).EndInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
		}

		private void AddEvents()
		{
			FormActivator.VendorAddressDetailsFormObj.VendorAddressChanged += EventHelper_VendorAddressChanged;
			dataGridContacts.AfterCellUpdate += dataGridContacts_AfterCellUpdate;
			dataGridContacts.BeforeCellUpdate += dataGridContacts_BeforeCellUpdate;
			dataGridContacts.ClickCellButton += dataGridContacts_ClickCellButton;
			dataGridContacts.CellDataError += dataGridContacts_CellDataError;
			base.KeyDown += SalesOrderForm_KeyDown;
			comboBoxARAccount.SelectedIndexChanged += comboBoxARAccount_SelectedIndexChanged;
			textBoxName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
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

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxName.Text.Trim() == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private void comboBoxARAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxARName.Text = comboBoxARAccount.SelectedName;
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

		private void EventHelper_VendorAddressChanged(object sender, EventArgs e)
		{
			DataSet dataSet = sender as DataSet;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				if (dataRow["VendorID"].ToString() == textBoxCode.Text && dataRow["AddressID"].ToString() == textBoxAddressID.Text && !isNewRecord)
				{
					FillAddressData(dataRow);
				}
			}
		}

		private void ServiceProviderForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					dataGridContacts.SetupUI();
					SetupContactsGrid();
					Init();
					ClearForm();
					textBoxCode.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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

		private void SetupContactsGrid()
		{
			dataGridContacts.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ContactID").Unique = true;
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("LastName");
			dataTable.Columns.Add("JobTitle");
			dataTable.Columns.Add("Note");
			dataGridContacts.DataSource = dataTable;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].MaxLength = 30;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].MaxLength = 64;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].MaxLength = 255;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].Header.Caption = "Job Title";
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Header.Caption = "Contact Code";
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].Header.Caption = "First Name";
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].Header.Caption = "Last Name";
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].ValueList = gridComboBoxContact;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].CellActivation = Activation.AllowEdit;
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.VendorTable.Rows[0];
					isLoadingData = true;
					textBoxCode.Text = dataRow["VendorID"].ToString();
					textBoxName.Text = dataRow["VendorName"].ToString();
					textBoxForeignName.Text = dataRow["ForeignName"].ToString();
					textBoxFormalName.Text = dataRow["LegalName"].ToString();
					comboBoxParentVendor.SelectedID = dataRow["ParentVendorID"].ToString();
					comboBoxVendorClass.SelectedID = dataRow["VendorClassID"].ToString();
					comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
					comboBoxArea.SelectedID = dataRow["AreaID"].ToString();
					checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					checkBoxHold.Checked = bool.Parse(dataRow["IsHold"].ToString());
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxBuyer.SelectedID = dataRow["BuyerID"].ToString();
					comboBoxPaymentTerms.SelectedID = dataRow["PaymentTermID"].ToString();
					comboBoxPaymentMethods.SelectedID = dataRow["PaymentMethodID"].ToString();
					comboBoxShippingMethods.SelectedID = dataRow["ShippingMethodID"].ToString();
					if (dataRow["AcceptCheckPayment"] != DBNull.Value)
					{
						checkBoxAcceptCheque.Checked = bool.Parse(dataRow["AcceptCheckPayment"].ToString());
					}
					else
					{
						checkBoxAcceptCheque.Checked = true;
					}
					if (dataRow["AcceptPDC"] != DBNull.Value)
					{
						checkBoxAcceptPDC.Checked = bool.Parse(dataRow["AcceptPDC"].ToString());
					}
					else
					{
						checkBoxAcceptPDC.Checked = true;
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
					default:
						radioButtonCreditLimitUnlimited.Checked = true;
						break;
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
						textBoxConsignCommission.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["CreditAmount"] != DBNull.Value)
					{
						textBoxCreditLimit.Text = decimal.Parse(dataRow["CreditAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxCreditLimit.Text = 0.ToString(Format.TotalAmountFormat);
					}
					textBoxBankName.Text = dataRow["BankName"].ToString();
					textBoxBankBranch.Text = dataRow["BankBranch"].ToString();
					textBoxBankAccountNumber.Text = dataRow["BankAccountNumber"].ToString();
					comboBoxARAccount.SelectedID = dataRow["APAccountID"].ToString();
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
					SetHeaderName();
					if (currentData.Tables.Contains("Vendor_Address") && currentData.VendorAddressTable.Rows.Count != 0)
					{
						dataRow = currentData.VendorAddressTable.Rows[0];
						FillAddressData(dataRow);
						if (currentData.Tables.Contains("Vendor_Contact_Detail") && currentData.VendorAddressTable.Rows.Count != 0)
						{
							DataTable dataTable = dataGridContacts.DataSource as DataTable;
							dataTable.Rows.Clear();
							foreach (DataRow row in currentData.Tables["Vendor_Contact_Detail"].Rows)
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
							if (currentData.Tables["UDF"].Rows.Count > 0)
							{
								_ = currentData.Tables["UDF"].Rows[0];
								foreach (DataColumn column2 in currentData.Tables["UDF"].Columns)
								{
									_ = (column2.ColumnName == "EntityID");
								}
							}
							else
							{
								udfEntryGrid.ClearData();
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isLoadingData = false;
			}
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
					currentData = new VendorData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.VendorTable.Rows[0] : currentData.VendorTable.NewRow();
				dataRow.BeginEdit();
				dataRow["VendorID"] = textBoxCode.Text;
				dataRow["VendorName"] = textBoxName.Text;
				dataRow["ForeignName"] = textBoxForeignName.Text;
				dataRow["LegalName"] = textBoxFormalName.Text;
				if (comboBoxParentVendor.SelectedID != "")
				{
					dataRow["ParentVendorID"] = comboBoxParentVendor.SelectedID;
				}
				else
				{
					dataRow["ParentVendorID"] = DBNull.Value;
				}
				if (comboBoxVendorClass.SelectedID != "")
				{
					dataRow["VendorClassID"] = comboBoxVendorClass.SelectedID;
				}
				else
				{
					dataRow["VendorClassID"] = DBNull.Value;
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
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
				}
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				if (IsNewRecord)
				{
					dataRow["IsServiceProvider"] = true;
				}
				dataRow["IsHold"] = checkBoxHold.Checked;
				dataRow["Note"] = textBoxNote.Text;
				if (comboBoxBuyer.SelectedID != "")
				{
					dataRow["BuyerID"] = comboBoxBuyer.SelectedID;
				}
				else
				{
					dataRow["BuyerID"] = DBNull.Value;
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
				dataRow["AcceptCheckPayment"] = checkBoxAcceptCheque.Checked;
				dataRow["AcceptPDC"] = checkBoxAcceptPDC.Checked;
				dataRow["AllowConsignment"] = checkBoxAllowConsignment.Checked;
				if (checkBoxAllowConsignment.Checked)
				{
					dataRow["ConsignComPercent"] = textBoxConsignCommission.Text;
				}
				else
				{
					dataRow["ConsignComPercent"] = DBNull.Value;
				}
				if (radioButtonCreditLimitAmount.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.CreditAmount;
				}
				else if (radioButtonCreditLimitNoCredit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.NoCredit;
				}
				else
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.Unlimited;
				}
				if (textBoxCreditLimit.Text != "")
				{
					dataRow["CreditAmount"] = textBoxCreditLimit.Text;
				}
				else
				{
					dataRow["CreditAmount"] = 0;
				}
				dataRow["BankName"] = textBoxBankName.Text;
				dataRow["BankBranch"] = textBoxBankBranch.Text;
				dataRow["BankAccountNumber"] = textBoxBankAccountNumber.Text;
				if (comboBoxARAccount.SelectedID != "")
				{
					dataRow["APAccountID"] = comboBoxARAccount.SelectedID;
				}
				else
				{
					dataRow["APAccountID"] = DBNull.Value;
				}
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
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1 || comboBoxTaxOption.SelectedIndex == 3)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				dataRow["TaxIDNumber"] = textBoxTaxIDNumber.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.VendorTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.VendorAddressTable.Rows[0] : currentData.VendorAddressTable.NewRow());
				dataRow.BeginEdit();
				dataRow["VendorID"] = textBoxCode.Text;
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
					currentData.VendorAddressTable.Rows.Add(dataRow);
				}
				currentData.VendorContactTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridContacts.Rows)
				{
					dataRow = currentData.VendorContactTable.NewRow();
					dataRow.BeginEdit();
					if (!(row.Cells["ContactID"].Value.ToString() == ""))
					{
						dataRow["VendorID"] = textBoxCode.Text;
						dataRow["ContactID"] = row.Cells["ContactID"].Value.ToString();
						dataRow["Note"] = row.Cells["Note"].Value.ToString();
						dataRow["JobTitle"] = row.Cells["JobTitle"].Value.ToString();
						dataRow["RowIndex"] = row.Index;
						dataRow.EndEdit();
						currentData.VendorContactTable.Rows.Add(dataRow);
					}
				}
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
				textBoxCode.Text = textBoxCode.Text.Trim();
				if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Vendor", "VendorID", textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
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
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Vendor", "VendorID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					return false;
				}
				if (textBoxCode.Text == comboBoxParentVendor.SelectedID)
				{
					ErrorHelper.WarningMessage("A vendor cannot be parent of itself.");
					tabPageGeneral.Tab.Selected = true;
					comboBoxParentVendor.Focus();
					return false;
				}
				if (!isNewRecord && checkBoxIsInactive.Checked && Factory.VendorSystem.HasBalance(textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("A vendor that has balance cannot be inactive.");
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
					flag = Factory.VendorSystem.CreateServiceProvider(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.ServiceProvider, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.VendorSystem.UpdateServiceProvider(currentData);
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
				if (!base.IsDisposed && !(id == "") && CanClose())
				{
					PublicFunctions.StartWaiting(this);
					currentData = Factory.VendorSystem.GetServiceProviderByID(id);
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
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception)
			{
				ClearForm();
			}
			finally
			{
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
			if (FormActivator.ProgramLoaded)
			{
				_ = Global.ConStatus;
				_ = 2;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Vendor", "VendorID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Vendor", "VendorID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Vendor", "VendorID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Vendor", "VendorID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Vendor", "VendorID", toolStripTextBoxFind.Text.Trim()))
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
				return Factory.VendorSystem.DeleteVendor(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Vendor", "VendorID");
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxAddress1.Clear();
			textBoxAddress2.Clear();
			textBoxAddress3.Clear();
			textBoxAddressID.Text = "PRIMARY";
			textBoxAddressPrintFormat.Clear();
			textBoxBankAccountNumber.Clear();
			textBoxBankBranch.Clear();
			textBoxBankName.Clear();
			textBoxCity.Clear();
			textBoxComment.Clear();
			comboBoxCurrency.Clear();
			textBoxContactName.Clear();
			textBoxCountry.Clear();
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
			checkBoxAllowConsignment.Checked = false;
			textBoxConsignCommission.Text = 0.ToString(Format.TotalAmountFormat);
			dateTimePickerLicenseExpDate.Clear();
			dateTimePickerContractExpDate.Clear();
			checkBoxIsInactive.Checked = false;
			udfEntryGrid.ClearData();
			comboBoxArea.Clear();
			comboBoxCountry.Clear();
			comboBoxVendorClass.Clear();
			comboBoxParentVendor.Clear();
			comboBoxPaymentMethods.Clear();
			comboBoxPaymentTerms.Clear();
			comboBoxBuyer.Clear();
			comboBoxShippingMethods.Clear();
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptPDC.Checked = true;
			checkBoxHold.Checked = false;
			radioButtonCreditLimitUnlimited.Checked = true;
			textBoxTaxIDNumber.Clear();
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			(dataGridContacts.DataSource as DataTable).Rows.Clear();
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

		private void VendorClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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
						return false;
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
			new FormHelper().EditVendorClass(comboBoxVendorClass.Text);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxCountry.Text);
		}

		private void linkLabelARAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxARAccount.Text);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethods.Text);
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
			new FormHelper().EditBuyer(comboBoxBuyer.Text);
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditArea(comboBoxArea.Text);
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().EditVendorAddress(textBoxCode.Text, textBoxAddressID.Text);
		}

		private void radioButtonCreditLimitAmount_CheckedChanged(object sender, EventArgs e)
		{
			textBoxCreditLimit.Enabled = radioButtonCreditLimitAmount.Checked;
		}

		private void comboBoxVendorClass_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
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
					DataSet vendorProfileReport = Factory.VendorSystem.GetVendorProfileReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", showInactive: true);
					if (vendorProfileReport == null || vendorProfileReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(vendorProfileReport, "", "Vendor Profile", SysDocTypes.None, isPrint, showPrintDialog);
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
			new FormHelper().ShowList(DataComboType.ServiceProvider);
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
					docManagementForm.EntityType = EntityTypesEnum.Vendors;
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

		private void checkBoxAllowConsignment_CheckedChanged(object sender, EventArgs e)
		{
			Panel panel = panelConsignComm;
			bool visible = textBoxConsignCommission.Enabled = checkBoxAllowConsignment.Checked;
			panel.Visible = visible;
			textBoxConsignCommission.ReadOnly = !checkBoxAllowConsignment.Checked;
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 1 || comboBoxTaxOption.SelectedIndex == 3)
			{
				comboBoxTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.Clear();
		}
	}
}
