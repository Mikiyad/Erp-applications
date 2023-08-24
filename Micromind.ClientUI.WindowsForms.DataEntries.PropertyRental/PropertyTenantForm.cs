using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class PropertyTenantForm : Form, IForm
	{
		private CustomerData currentData;

		private const string TABLENAME_CONST = "Customer";

		private const string IDFIELD_CONST = "CustomerID";

		private bool isNewRecord = true;

		private MMTextBox textBoxName;

		private MMLabel label1;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxCode;

		private MMLabel labelCustomerNumber;

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

		private MMLabel mmLabel30;

		private MMTextBox textBoxBankAccountNumber;

		private MMLabel mmLabel29;

		private MMTextBox textBoxBankBranch;

		private MMLabel mmLabel28;

		private MMTextBox textBoxBankName;

		private MMLabel mmLabel32;

		private UltraGroupBox ultraGroupBox3;

		private UltraTabPageControl tabPageContacts;

		private DataEntryGrid dataGridContacts;

		private MMLabel mmLabel35;

		private customersFlatComboBox comboBoxParentCustomer;

		private CountryComboBox comboBoxCountry;

		private AreaComboBox comboBoxArea;

		private PriceLevelComboBox comboBoxPriceLevel;

		private SalespersonComboBox comboBoxSalesperson;

		private ShippingMethodsComboBox comboBoxShippingMethods;

		private UltraFormattedLinkLabel linkLabelCustomerClass;

		private UltraFormattedLinkLabel linkLabelArea;

		private UltraFormattedLinkLabel linkLabelCountry;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel linkLabelARAccount;

		private CustomerAddressComboBox comboBoxStatementAddress;

		private CustomerAddressComboBox comboBoxShiptoAddress;

		private CustomerAddressComboBox comboBoxBilltoAddress;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMLabel mmLabel24;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMTextBox textBoxARName;

		private MMLabel mmLabel1;

		private CurrencyComboBox comboBoxCurrency;

		private MMLabel labelCustomerNameHeader;

		private MMLabel mmLabel3;

		private MMSDateTimePicker dateTimePickerEstablished;

		private MMSDateTimePicker dateTimePickerCustomerSince;

		private MMLabel mmLabel4;

		private XPButton buttonCategories;

		private ContextMenuStrip contextMenuStripContact;

		private ToolStripMenuItem openContactToolStripMenuItem;

		private ContactsComboBox gridComboBoxContact;

		private UDFEntryControl udfEntryGrid;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private ToolStripMenuItem newContactToolStripMenuItem;

		private ToolStripMenuItem deleteContactToolStripMenuItem;

		private ToolStripMenuItem deleteRowToolStripMenuItem;

		private Panel panelConsignComm;

		private PercentTextBox textBoxConsignCommission;

		private MMLabel mmLabel7;

		private MMLabel mmLabel27;

		private CheckBox checkBoxAllowConsignment;

		private CheckBox checkBoxWeightInvoice;

		private UltraTabPageControl ultraTabPageControl2;

		private UltraGroupBox ultraGroupBox4;

		private RadioButton radioButtonSublimit;

		private CheckBox checkBoxAcceptPDC;

		private AmountTextBox textBoxCreditLimit;

		private RadioButton radioButtonCreditLimitNoCredit;

		private RadioButton radioButtonCreditLimitUnlimited;

		private RadioButton radioButtonCreditLimitAmount;

		private UserComboBox comboBoxCollectionUser;

		private MMLabel mmLabel31;

		private MMTextBox textBoxReviewBy;

		private MMLabel mmLabel26;

		private MMSDateTimePicker dateTimePickerReviewDate;

		private MMLabel mmLabel25;

		private MMTextBox textBoxPaymentTermName;

		private MMTextBox textBoxPaymentMethodName;

		private MMLabel mmLabel2;

		private UltraComboEditor comboBoxRating;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private PaymentTermComboBox comboBoxPaymentTerms;

		private paymentMethodsComboBox comboBoxPaymentMethods;

		private UltraGroupBox ultraGroupBox5;

		private Panel panel2;

		private MMLabel mmLabel39;

		private MMTextBox textBoxInsuranceRemarks;

		private MMLabel mmLabel38;

		private MMLabel mmLabel37;

		private MMTextBox textBoxInsuranceNumber;

		private AmountTextBox textBoxInsuranceApprovedAmount;

		private MMLabel mmLabel36;

		private AmountTextBox textBoxInsuranceReqAmount;

		private MMSDateTimePicker dateTimePickerInsuranceDate;

		private MMLabel mmLabel34;

		private MMLabel mmLabel33;

		private ComboBox comboBoxInsuranceStatus;

		private CheckBox checkBoxAcceptCheque;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private GenericListComboBox comboBoxLeadSource;

		private Label label2;

		private TenantClassComboBox comboBoxCustomerClass;

		private ToolStripButton toolStripButtonInformation;

		private UltraGroupBox ultraGroupBox7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel22;

		private TaxGroupComboBox comboBoxTaxGroup;

		private MMLabel mmLabel40;

		private ComboBox comboBoxTaxOption;

		private MMTextBox textBoxTaxIDNumber;

		private MMLabel mmLabel58;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem documentsToolStripMenuItem1;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isLoading;

		public ScreenAreas ScreenArea => ScreenAreas.PropertyRental;

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
					comboBoxBilltoAddress.Filter("");
					comboBoxShiptoAddress.Filter("");
					comboBoxStatementAddress.Filter("");
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					textBoxAddressID.Enabled = false;
					comboBoxBilltoAddress.Filter(textBoxCode.Text);
					comboBoxShiptoAddress.Filter(textBoxCode.Text);
					comboBoxStatementAddress.Filter(textBoxCode.Text);
				}
				buttonCategories.Enabled = !value;
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

		public PropertyTenantForm()
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
			Infragistics.Win.ValueListItem valueListItem = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance245 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyTenantForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxCustomerClass = new Micromind.DataControls.TenantClassComboBox();
			buttonCategories = new Micromind.UISupport.XPButton();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			linkLabelArea = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			linkLabelCustomerClass = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPriceLevel = new Micromind.DataControls.PriceLevelComboBox();
			comboBoxArea = new Micromind.DataControls.AreaComboBox();
			comboBoxCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxParentCustomer = new Micromind.DataControls.customersFlatComboBox();
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
			labelCustomerNumber = new Micromind.UISupport.MMLabel();
			textBoxFormalName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxForeignName = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxTaxIDNumber = new Micromind.UISupport.MMTextBox();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel22 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			mmLabel40 = new Micromind.UISupport.MMLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			comboBoxLeadSource = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxWeightInvoice = new System.Windows.Forms.CheckBox();
			panelConsignComm = new System.Windows.Forms.Panel();
			textBoxConsignCommission = new Micromind.UISupport.PercentTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			checkBoxAllowConsignment = new System.Windows.Forms.CheckBox();
			dateTimePickerCustomerSince = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimePickerEstablished = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel3 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxStatementAddress = new Micromind.DataControls.CustomerAddressComboBox();
			comboBoxShiptoAddress = new Micromind.DataControls.CustomerAddressComboBox();
			comboBoxBilltoAddress = new Micromind.DataControls.CustomerAddressComboBox();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxShippingMethods = new Micromind.DataControls.ShippingMethodsComboBox();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
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
			comboBoxARAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label2 = new System.Windows.Forms.Label();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			panel2 = new System.Windows.Forms.Panel();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceRemarks = new Micromind.UISupport.MMTextBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceNumber = new Micromind.UISupport.MMTextBox();
			textBoxInsuranceApprovedAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceReqAmount = new Micromind.UISupport.AmountTextBox();
			dateTimePickerInsuranceDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel34 = new Micromind.UISupport.MMLabel();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			comboBoxInsuranceStatus = new System.Windows.Forms.ComboBox();
			textBoxPaymentTermName = new Micromind.UISupport.MMTextBox();
			textBoxPaymentMethodName = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPaymentTerms = new Micromind.DataControls.PaymentTermComboBox();
			comboBoxPaymentMethods = new Micromind.DataControls.paymentMethodsComboBox();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxAcceptCheque = new System.Windows.Forms.CheckBox();
			radioButtonSublimit = new System.Windows.Forms.RadioButton();
			checkBoxAcceptPDC = new System.Windows.Forms.CheckBox();
			textBoxCreditLimit = new Micromind.UISupport.AmountTextBox();
			radioButtonCreditLimitNoCredit = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitUnlimited = new System.Windows.Forms.RadioButton();
			radioButtonCreditLimitAmount = new System.Windows.Forms.RadioButton();
			comboBoxCollectionUser = new Micromind.DataControls.UserComboBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			textBoxReviewBy = new Micromind.UISupport.MMTextBox();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			dateTimePickerReviewDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel25 = new Micromind.UISupport.MMLabel();
			tabPageContacts = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			dataGridContacts = new Micromind.DataControls.DataEntryGrid();
			gridComboBoxContact = new Micromind.DataControls.ContactsComboBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
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
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			documentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			contextMenuStripContact = new System.Windows.Forms.ContextMenuStrip(components);
			openContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).BeginInit();
			ultraGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSource).BeginInit();
			panelConsignComm.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxStatementAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShiptoAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBilltoAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethods).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRating).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentTerms).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethods).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCollectionUser).BeginInit();
			tabPageContacts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStripContact.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(comboBoxCustomerClass);
			tabPageGeneral.Controls.Add(buttonCategories);
			tabPageGeneral.Controls.Add(comboBoxCurrency);
			tabPageGeneral.Controls.Add(linkLabelArea);
			tabPageGeneral.Controls.Add(linkLabelCountry);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(mmLabel24);
			tabPageGeneral.Controls.Add(linkLabelCustomerClass);
			tabPageGeneral.Controls.Add(comboBoxPriceLevel);
			tabPageGeneral.Controls.Add(comboBoxArea);
			tabPageGeneral.Controls.Add(comboBoxCountry);
			tabPageGeneral.Controls.Add(comboBoxParentCustomer);
			tabPageGeneral.Controls.Add(mmLabel32);
			tabPageGeneral.Controls.Add(ultraGroupBox1);
			tabPageGeneral.Controls.Add(checkBoxHold);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(labelCustomerNumber);
			tabPageGeneral.Controls.Add(textBoxFormalName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxForeignName);
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(696, 490);
			tabPageGeneral.Paint += new System.Windows.Forms.PaintEventHandler(tabPageGeneral_Paint);
			comboBoxCustomerClass.Assigned = false;
			comboBoxCustomerClass.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomerClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomerClass.CustomReportFieldName = "";
			comboBoxCustomerClass.CustomReportKey = "";
			comboBoxCustomerClass.CustomReportValueType = 1;
			comboBoxCustomerClass.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomerClass.DisplayLayout.Appearance = appearance;
			comboBoxCustomerClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomerClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxCustomerClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomerClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomerClass.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomerClass.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxCustomerClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomerClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomerClass.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxCustomerClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomerClass.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxCustomerClass.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxCustomerClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomerClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomerClass.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxCustomerClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomerClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxCustomerClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomerClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomerClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomerClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomerClass.Editable = true;
			comboBoxCustomerClass.FilterString = "";
			comboBoxCustomerClass.HasAllAccount = false;
			comboBoxCustomerClass.HasCustom = false;
			comboBoxCustomerClass.IsDataLoaded = false;
			comboBoxCustomerClass.Location = new System.Drawing.Point(467, 32);
			comboBoxCustomerClass.MaxDropDownItems = 12;
			comboBoxCustomerClass.Name = "comboBoxCustomerClass";
			comboBoxCustomerClass.ShowInactiveItems = false;
			comboBoxCustomerClass.ShowQuickAdd = true;
			comboBoxCustomerClass.Size = new System.Drawing.Size(204, 20);
			comboBoxCustomerClass.TabIndex = 21;
			comboBoxCustomerClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonCategories.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCategories.BackColor = System.Drawing.Color.DarkGray;
			buttonCategories.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCategories.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCategories.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCategories.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCategories.Location = new System.Drawing.Point(537, 148);
			buttonCategories.Name = "buttonCategories";
			buttonCategories.Size = new System.Drawing.Size(134, 24);
			buttonCategories.TabIndex = 13;
			buttonCategories.Text = "Categories...";
			buttonCategories.UseVisualStyleBackColor = false;
			buttonCategories.Visible = false;
			buttonCategories.Click += new System.EventHandler(buttonCategories_Click);
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
			comboBoxCurrency.Location = new System.Drawing.Point(467, 122);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(204, 20);
			comboBoxCurrency.TabIndex = 12;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelArea.AutoSize = true;
			linkLabelArea.Location = new System.Drawing.Point(371, 81);
			linkLabelArea.Name = "linkLabelArea";
			linkLabelArea.Size = new System.Drawing.Size(30, 14);
			linkLabelArea.TabIndex = 20;
			linkLabelArea.TabStop = true;
			linkLabelArea.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelArea.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelArea.Value = "Area:";
			appearance25.ForeColor = System.Drawing.Color.Blue;
			linkLabelArea.VisitedLinkAppearance = appearance25;
			linkLabelArea.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelArea_LinkClicked);
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(371, 59);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(46, 14);
			linkLabelCountry.TabIndex = 18;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Country:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance26;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(371, 123);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(55, 13);
			mmLabel1.TabIndex = 18;
			mmLabel1.Text = "Currency:";
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(371, 102);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(62, 13);
			mmLabel24.TabIndex = 18;
			mmLabel24.Text = "Price Level:";
			linkLabelCustomerClass.AutoSize = true;
			appearance27.ForeColor = System.Drawing.Color.Blue;
			linkLabelCustomerClass.LinkAppearance = appearance27;
			linkLabelCustomerClass.Location = new System.Drawing.Point(371, 36);
			linkLabelCustomerClass.Name = "linkLabelCustomerClass";
			linkLabelCustomerClass.Size = new System.Drawing.Size(72, 14);
			linkLabelCustomerClass.TabIndex = 16;
			linkLabelCustomerClass.TabStop = true;
			linkLabelCustomerClass.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCustomerClass.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCustomerClass.Value = "Tenant Class:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			linkLabelCustomerClass.VisitedLinkAppearance = appearance28;
			linkLabelCustomerClass.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxPriceLevel.Assigned = false;
			comboBoxPriceLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPriceLevel.CustomReportFieldName = "";
			comboBoxPriceLevel.CustomReportKey = "";
			comboBoxPriceLevel.CustomReportValueType = 1;
			comboBoxPriceLevel.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPriceLevel.DisplayLayout.Appearance = appearance29;
			comboBoxPriceLevel.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPriceLevel.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxPriceLevel.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPriceLevel.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPriceLevel.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxPriceLevel.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPriceLevel.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxPriceLevel.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxPriceLevel.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxPriceLevel.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPriceLevel.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
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
			comboBoxPriceLevel.Location = new System.Drawing.Point(467, 100);
			comboBoxPriceLevel.MaxDropDownItems = 12;
			comboBoxPriceLevel.MaxLength = 15;
			comboBoxPriceLevel.Name = "comboBoxPriceLevel";
			comboBoxPriceLevel.ShowInactiveItems = false;
			comboBoxPriceLevel.ShowQuickAdd = true;
			comboBoxPriceLevel.Size = new System.Drawing.Size(204, 20);
			comboBoxPriceLevel.TabIndex = 11;
			comboBoxPriceLevel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxArea.Assigned = false;
			comboBoxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArea.CustomReportFieldName = "";
			comboBoxArea.CustomReportKey = "";
			comboBoxArea.CustomReportValueType = 1;
			comboBoxArea.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArea.DisplayLayout.Appearance = appearance41;
			comboBoxArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArea.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArea.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArea.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArea.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxArea.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxArea.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArea.Editable = true;
			comboBoxArea.FilterString = "";
			comboBoxArea.HasAllAccount = false;
			comboBoxArea.HasCustom = false;
			comboBoxArea.IsDataLoaded = false;
			comboBoxArea.Location = new System.Drawing.Point(467, 78);
			comboBoxArea.MaxDropDownItems = 12;
			comboBoxArea.MaxLength = 15;
			comboBoxArea.Name = "comboBoxArea";
			comboBoxArea.ShowInactiveItems = false;
			comboBoxArea.ShowQuickAdd = true;
			comboBoxArea.Size = new System.Drawing.Size(204, 20);
			comboBoxArea.TabIndex = 10;
			comboBoxArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCountry.Assigned = false;
			comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCountry.CustomReportFieldName = "";
			comboBoxCountry.CustomReportKey = "";
			comboBoxCountry.CustomReportValueType = 1;
			comboBoxCountry.DescriptionTextBox = null;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCountry.DisplayLayout.Appearance = appearance53;
			comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCountry.Editable = true;
			comboBoxCountry.FilterString = "";
			comboBoxCountry.HasAllAccount = false;
			comboBoxCountry.HasCustom = false;
			comboBoxCountry.IsDataLoaded = false;
			comboBoxCountry.Location = new System.Drawing.Point(467, 56);
			comboBoxCountry.MaxDropDownItems = 12;
			comboBoxCountry.MaxLength = 15;
			comboBoxCountry.Name = "comboBoxCountry";
			comboBoxCountry.ShowInactiveItems = false;
			comboBoxCountry.ShowQuickAdd = true;
			comboBoxCountry.Size = new System.Drawing.Size(204, 20);
			comboBoxCountry.TabIndex = 9;
			comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxParentCustomer.Assigned = false;
			comboBoxParentCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxParentCustomer.CustomReportFieldName = "";
			comboBoxParentCustomer.CustomReportKey = "";
			comboBoxParentCustomer.CustomReportValueType = 1;
			comboBoxParentCustomer.DescriptionTextBox = null;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxParentCustomer.DisplayLayout.Appearance = appearance65;
			comboBoxParentCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxParentCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			comboBoxParentCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxParentCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxParentCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxParentCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			comboBoxParentCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxParentCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			comboBoxParentCustomer.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxParentCustomer.DisplayLayout.Override.CellAppearance = appearance72;
			comboBoxParentCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxParentCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			comboBoxParentCustomer.DisplayLayout.Override.HeaderAppearance = appearance74;
			comboBoxParentCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxParentCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			comboBoxParentCustomer.DisplayLayout.Override.RowAppearance = appearance75;
			comboBoxParentCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxParentCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
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
			comboBoxParentCustomer.Location = new System.Drawing.Point(132, 96);
			comboBoxParentCustomer.MaxDropDownItems = 12;
			comboBoxParentCustomer.MaxLength = 64;
			comboBoxParentCustomer.Name = "comboBoxParentCustomer";
			comboBoxParentCustomer.ShowConsignmentOnly = false;
			comboBoxParentCustomer.ShowInactive = false;
			comboBoxParentCustomer.ShowLPOCustomersOnly = false;
			comboBoxParentCustomer.ShowPROCustomersOnly = false;
			comboBoxParentCustomer.ShowQuickAdd = true;
			comboBoxParentCustomer.Size = new System.Drawing.Size(229, 20);
			comboBoxParentCustomer.TabIndex = 5;
			comboBoxParentCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(10, 99);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(92, 13);
			mmLabel32.TabIndex = 14;
			mmLabel32.Text = "Parent Customer:";
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
			ultraGroupBox1.Location = new System.Drawing.Point(0, 166);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(687, 310);
			ultraGroupBox1.TabIndex = 14;
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
			textBoxComment.TabIndex = 33;
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
			mmLabel22.TabIndex = 18;
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
			textBoxDepartment.TabIndex = 19;
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
			textBoxWebsite.TabIndex = 31;
			buttonMoreAddress.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonMoreAddress.BackColor = System.Drawing.Color.DarkGray;
			buttonMoreAddress.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonMoreAddress.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonMoreAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonMoreAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonMoreAddress.Location = new System.Drawing.Point(537, 199);
			buttonMoreAddress.Name = "buttonMoreAddress";
			buttonMoreAddress.Size = new System.Drawing.Size(134, 24);
			buttonMoreAddress.TabIndex = 34;
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
			mmLabel21.TabIndex = 16;
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
			mmLabel20.TabIndex = 14;
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
			textBoxPostalCode.TabIndex = 15;
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
			textBoxEmail.TabIndex = 29;
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
			textBoxMobile.TabIndex = 27;
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
			textBoxFax.TabIndex = 25;
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
			textBoxPhone2.TabIndex = 23;
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
			textBoxPhone1.TabIndex = 21;
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
			mmLabel12.TabIndex = 12;
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
			textBoxCountry.TabIndex = 13;
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
			mmLabel11.TabIndex = 10;
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
			textBoxState.TabIndex = 11;
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
			mmLabel13.TabIndex = 8;
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
			textBoxCity.TabIndex = 9;
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
			textBoxAddress3.TabIndex = 7;
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
			textBoxAddress2.TabIndex = 6;
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
			mmLabel10.TabIndex = 4;
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
			textBoxAddress1.TabIndex = 5;
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
			mmLabel9.TabIndex = 2;
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
			textBoxContactName.TabIndex = 3;
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
			mmLabel8.TabIndex = 0;
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
			textBoxAddressID.TabIndex = 1;
			textBoxAddressID.Text = "PRIMARY";
			checkBoxHold.BackColor = System.Drawing.Color.Transparent;
			checkBoxHold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxHold.Location = new System.Drawing.Point(467, 9);
			checkBoxHold.Name = "checkBoxHold";
			checkBoxHold.Size = new System.Drawing.Size(69, 16);
			checkBoxHold.TabIndex = 7;
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
			mmLabel5.Location = new System.Drawing.Point(9, 54);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(67, 13);
			mmLabel5.TabIndex = 8;
			mmLabel5.Text = "Short Name:";
			labelCustomerNumber.AutoSize = true;
			labelCustomerNumber.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNumber.IsFieldHeader = false;
			labelCustomerNumber.IsRequired = true;
			labelCustomerNumber.Location = new System.Drawing.Point(9, 10);
			labelCustomerNumber.Name = "labelCustomerNumber";
			labelCustomerNumber.PenWidth = 1f;
			labelCustomerNumber.ShowBorder = false;
			labelCustomerNumber.Size = new System.Drawing.Size(96, 13);
			labelCustomerNumber.TabIndex = 0;
			labelCustomerNumber.Text = "Customer Code:";
			labelCustomerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxFormalName.BackColor = System.Drawing.Color.White;
			textBoxFormalName.CustomReportFieldName = "";
			textBoxFormalName.CustomReportKey = "";
			textBoxFormalName.CustomReportValueType = 1;
			textBoxFormalName.IsComboTextBox = false;
			textBoxFormalName.IsModified = false;
			textBoxFormalName.Location = new System.Drawing.Point(132, 53);
			textBoxFormalName.MaxLength = 64;
			textBoxFormalName.Name = "textBoxFormalName";
			textBoxFormalName.Size = new System.Drawing.Size(229, 20);
			textBoxFormalName.TabIndex = 2;
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
			textBoxForeignName.Location = new System.Drawing.Point(132, 75);
			textBoxForeignName.MaxLength = 64;
			textBoxForeignName.Name = "textBoxForeignName";
			textBoxForeignName.Size = new System.Drawing.Size(229, 20);
			textBoxForeignName.TabIndex = 3;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(374, 10);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(65, 16);
			checkBoxIsInactive.TabIndex = 6;
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
			textBoxName.TabIndex = 1;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 76);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(77, 13);
			mmLabel6.TabIndex = 4;
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
			label1.Size = new System.Drawing.Size(100, 13);
			label1.TabIndex = 2;
			label1.Text = "Customer Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			tabPageDetails.Controls.Add(ultraGroupBox7);
			tabPageDetails.Controls.Add(comboBoxLeadSource);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel4);
			tabPageDetails.Controls.Add(checkBoxWeightInvoice);
			tabPageDetails.Controls.Add(panelConsignComm);
			tabPageDetails.Controls.Add(checkBoxAllowConsignment);
			tabPageDetails.Controls.Add(dateTimePickerCustomerSince);
			tabPageDetails.Controls.Add(mmLabel4);
			tabPageDetails.Controls.Add(dateTimePickerEstablished);
			tabPageDetails.Controls.Add(mmLabel3);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel3);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel2);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel1);
			tabPageDetails.Controls.Add(comboBoxStatementAddress);
			tabPageDetails.Controls.Add(comboBoxShiptoAddress);
			tabPageDetails.Controls.Add(comboBoxBilltoAddress);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel8);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel5);
			tabPageDetails.Controls.Add(comboBoxShippingMethods);
			tabPageDetails.Controls.Add(comboBoxSalesperson);
			tabPageDetails.Controls.Add(ultraGroupBox3);
			tabPageDetails.Controls.Add(ultraGroupBox2);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(696, 417);
			ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox7.Controls.Add(textBoxTaxIDNumber);
			ultraGroupBox7.Controls.Add(mmLabel58);
			ultraGroupBox7.Controls.Add(ultraFormattedLinkLabel22);
			ultraGroupBox7.Controls.Add(comboBoxTaxGroup);
			ultraGroupBox7.Controls.Add(mmLabel40);
			ultraGroupBox7.Controls.Add(comboBoxTaxOption);
			ultraGroupBox7.Location = new System.Drawing.Point(3, 233);
			ultraGroupBox7.Name = "ultraGroupBox7";
			ultraGroupBox7.Size = new System.Drawing.Size(621, 101);
			ultraGroupBox7.TabIndex = 51;
			ultraGroupBox7.Text = "Tax Details";
			textBoxTaxIDNumber.BackColor = System.Drawing.Color.White;
			textBoxTaxIDNumber.CustomReportFieldName = "";
			textBoxTaxIDNumber.CustomReportKey = "";
			textBoxTaxIDNumber.CustomReportValueType = 1;
			textBoxTaxIDNumber.IsComboTextBox = false;
			textBoxTaxIDNumber.IsModified = false;
			textBoxTaxIDNumber.Location = new System.Drawing.Point(133, 72);
			textBoxTaxIDNumber.MaxLength = 75;
			textBoxTaxIDNumber.Name = "textBoxTaxIDNumber";
			textBoxTaxIDNumber.Size = new System.Drawing.Size(156, 20);
			textBoxTaxIDNumber.TabIndex = 74;
			textBoxTaxIDNumber.TextChanged += new System.EventHandler(textBoxTaxIDNumber_TextChanged);
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(4, 76);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(83, 13);
			mmLabel58.TabIndex = 75;
			mmLabel58.Text = "Tax ID Number:";
			ultraFormattedLinkLabel22.AutoSize = true;
			appearance77.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.LinkAppearance = appearance77;
			ultraFormattedLinkLabel22.Location = new System.Drawing.Point(4, 48);
			ultraFormattedLinkLabel22.Name = "ultraFormattedLinkLabel22";
			ultraFormattedLinkLabel22.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel22.TabIndex = 73;
			ultraFormattedLinkLabel22.TabStop = true;
			ultraFormattedLinkLabel22.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel22.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel22.Value = "Tax Group:";
			appearance78.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.VisitedLinkAppearance = appearance78;
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance79;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance80;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance81;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance82.BackColor2 = System.Drawing.SystemColors.Control;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance82;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance83;
			appearance84.BackColor = System.Drawing.SystemColors.Highlight;
			appearance84.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance84;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance85;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			appearance86.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance86;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance87.BackColor = System.Drawing.SystemColors.Control;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance87;
			appearance88.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance88;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance89;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance90;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(133, 46);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(156, 20);
			comboBoxTaxGroup.TabIndex = 1;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(4, 26);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(64, 13);
			mmLabel40.TabIndex = 70;
			mmLabel40.Text = "Tax Option:";
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[3]
			{
				"Based on Customer",
				"Taxable",
				"Non Taxable"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(133, 22);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(156, 21);
			comboBoxTaxOption.TabIndex = 0;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			comboBoxTaxOption.MouseUp += new System.Windows.Forms.MouseEventHandler(comboBoxTaxOption_MouseUp);
			comboBoxLeadSource.Assigned = false;
			comboBoxLeadSource.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLeadSource.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeadSource.CustomReportFieldName = "";
			comboBoxLeadSource.CustomReportKey = "";
			comboBoxLeadSource.CustomReportValueType = 1;
			comboBoxLeadSource.DescriptionTextBox = null;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLeadSource.DisplayLayout.Appearance = appearance91;
			comboBoxLeadSource.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLeadSource.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance92.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance92.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadSource.DisplayLayout.GroupByBox.Appearance = appearance92;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadSource.DisplayLayout.GroupByBox.BandLabelAppearance = appearance93;
			comboBoxLeadSource.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance94.BackColor2 = System.Drawing.SystemColors.Control;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadSource.DisplayLayout.GroupByBox.PromptAppearance = appearance94;
			comboBoxLeadSource.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLeadSource.DisplayLayout.MaxRowScrollRegions = 1;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLeadSource.DisplayLayout.Override.ActiveCellAppearance = appearance95;
			appearance96.BackColor = System.Drawing.SystemColors.Highlight;
			appearance96.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLeadSource.DisplayLayout.Override.ActiveRowAppearance = appearance96;
			comboBoxLeadSource.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLeadSource.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLeadSource.DisplayLayout.Override.CardAreaAppearance = appearance97;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			appearance98.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLeadSource.DisplayLayout.Override.CellAppearance = appearance98;
			comboBoxLeadSource.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLeadSource.DisplayLayout.Override.CellPadding = 0;
			appearance99.BackColor = System.Drawing.SystemColors.Control;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadSource.DisplayLayout.Override.GroupByRowAppearance = appearance99;
			appearance100.TextHAlignAsString = "Left";
			comboBoxLeadSource.DisplayLayout.Override.HeaderAppearance = appearance100;
			comboBoxLeadSource.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLeadSource.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			comboBoxLeadSource.DisplayLayout.Override.RowAppearance = appearance101;
			comboBoxLeadSource.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLeadSource.DisplayLayout.Override.TemplateAddRowAppearance = appearance102;
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
			comboBoxLeadSource.Location = new System.Drawing.Point(413, 74);
			comboBoxLeadSource.MaxDropDownItems = 12;
			comboBoxLeadSource.Name = "comboBoxLeadSource";
			comboBoxLeadSource.ShowInactiveItems = false;
			comboBoxLeadSource.ShowQuickAdd = true;
			comboBoxLeadSource.Size = new System.Drawing.Size(140, 20);
			comboBoxLeadSource.TabIndex = 7;
			comboBoxLeadSource.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(300, 76);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(69, 14);
			ultraFormattedLinkLabel4.TabIndex = 50;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Lead Source:";
			appearance103.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance103;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			checkBoxWeightInvoice.AutoSize = true;
			checkBoxWeightInvoice.Location = new System.Drawing.Point(10, 104);
			checkBoxWeightInvoice.Name = "checkBoxWeightInvoice";
			checkBoxWeightInvoice.Size = new System.Drawing.Size(131, 17);
			checkBoxWeightInvoice.TabIndex = 8;
			checkBoxWeightInvoice.Text = "Invoice by item weight";
			checkBoxWeightInvoice.UseVisualStyleBackColor = true;
			checkBoxWeightInvoice.Visible = false;
			panelConsignComm.Controls.Add(textBoxConsignCommission);
			panelConsignComm.Controls.Add(mmLabel7);
			panelConsignComm.Controls.Add(mmLabel27);
			panelConsignComm.Location = new System.Drawing.Point(310, 100);
			panelConsignComm.Name = "panelConsignComm";
			panelConsignComm.Size = new System.Drawing.Size(227, 27);
			panelConsignComm.TabIndex = 10;
			panelConsignComm.Visible = false;
			textBoxConsignCommission.BackColor = System.Drawing.Color.White;
			textBoxConsignCommission.CustomReportFieldName = "";
			textBoxConsignCommission.CustomReportKey = "";
			textBoxConsignCommission.CustomReportValueType = 1;
			textBoxConsignCommission.IsComboTextBox = false;
			textBoxConsignCommission.IsModified = false;
			textBoxConsignCommission.Location = new System.Drawing.Point(114, 4);
			textBoxConsignCommission.Name = "textBoxConsignCommission";
			textBoxConsignCommission.Size = new System.Drawing.Size(86, 20);
			textBoxConsignCommission.TabIndex = 0;
			textBoxConsignCommission.Text = "0.00";
			textBoxConsignCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(204, 7);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(18, 13);
			mmLabel7.TabIndex = 15;
			mmLabel7.Text = "%";
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(8, 6);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(106, 13);
			mmLabel27.TabIndex = 0;
			mmLabel27.Text = "Commission Percent:";
			checkBoxAllowConsignment.AutoSize = true;
			checkBoxAllowConsignment.Location = new System.Drawing.Point(169, 104);
			checkBoxAllowConsignment.Name = "checkBoxAllowConsignment";
			checkBoxAllowConsignment.Size = new System.Drawing.Size(141, 17);
			checkBoxAllowConsignment.TabIndex = 9;
			checkBoxAllowConsignment.Text = "Allow consignment sales";
			checkBoxAllowConsignment.UseVisualStyleBackColor = true;
			checkBoxAllowConsignment.Visible = false;
			dateTimePickerCustomerSince.Checked = false;
			dateTimePickerCustomerSince.CustomFormat = " ";
			dateTimePickerCustomerSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerCustomerSince.Location = new System.Drawing.Point(123, 75);
			dateTimePickerCustomerSince.Name = "dateTimePickerCustomerSince";
			dateTimePickerCustomerSince.ShowCheckBox = true;
			dateTimePickerCustomerSince.Size = new System.Drawing.Size(156, 20);
			dateTimePickerCustomerSince.TabIndex = 6;
			dateTimePickerCustomerSince.Value = new System.DateTime(0L);
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(7, 76);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(85, 13);
			mmLabel4.TabIndex = 48;
			mmLabel4.Text = "Customer Since:";
			dateTimePickerEstablished.Checked = false;
			dateTimePickerEstablished.CustomFormat = " ";
			dateTimePickerEstablished.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerEstablished.Location = new System.Drawing.Point(413, 53);
			dateTimePickerEstablished.Name = "dateTimePickerEstablished";
			dateTimePickerEstablished.ShowCheckBox = true;
			dateTimePickerEstablished.Size = new System.Drawing.Size(141, 20);
			dateTimePickerEstablished.TabIndex = 5;
			dateTimePickerEstablished.Value = new System.DateTime(0L);
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(300, 55);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(91, 13);
			mmLabel3.TabIndex = 46;
			mmLabel3.Text = "Date Established:";
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(300, 33);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(100, 14);
			ultraFormattedLinkLabel3.TabIndex = 40;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Statement Address:";
			ultraFormattedLinkLabel3.Visible = false;
			appearance104.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance104;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(10, 33);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(85, 14);
			ultraFormattedLinkLabel2.TabIndex = 39;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Ship to Address:";
			ultraFormattedLinkLabel2.Visible = false;
			appearance105.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance105;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked_1);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(300, 11);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(78, 14);
			ultraFormattedLinkLabel1.TabIndex = 39;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bill to Address:";
			ultraFormattedLinkLabel1.Visible = false;
			appearance106.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance106;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			comboBoxStatementAddress.Assigned = false;
			comboBoxStatementAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStatementAddress.CustomReportFieldName = "";
			comboBoxStatementAddress.CustomReportKey = "";
			comboBoxStatementAddress.CustomReportValueType = 1;
			comboBoxStatementAddress.DescriptionTextBox = null;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStatementAddress.DisplayLayout.Appearance = appearance107;
			comboBoxStatementAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStatementAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStatementAddress.DisplayLayout.GroupByBox.Appearance = appearance108;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStatementAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance109;
			comboBoxStatementAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance110.BackColor2 = System.Drawing.SystemColors.Control;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStatementAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance110;
			comboBoxStatementAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStatementAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStatementAddress.DisplayLayout.Override.ActiveCellAppearance = appearance111;
			appearance112.BackColor = System.Drawing.SystemColors.Highlight;
			appearance112.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStatementAddress.DisplayLayout.Override.ActiveRowAppearance = appearance112;
			comboBoxStatementAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStatementAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStatementAddress.DisplayLayout.Override.CardAreaAppearance = appearance113;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			appearance114.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStatementAddress.DisplayLayout.Override.CellAppearance = appearance114;
			comboBoxStatementAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStatementAddress.DisplayLayout.Override.CellPadding = 0;
			appearance115.BackColor = System.Drawing.SystemColors.Control;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStatementAddress.DisplayLayout.Override.GroupByRowAppearance = appearance115;
			appearance116.TextHAlignAsString = "Left";
			comboBoxStatementAddress.DisplayLayout.Override.HeaderAppearance = appearance116;
			comboBoxStatementAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStatementAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			comboBoxStatementAddress.DisplayLayout.Override.RowAppearance = appearance117;
			comboBoxStatementAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStatementAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance118;
			comboBoxStatementAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStatementAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStatementAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStatementAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStatementAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxStatementAddress.Editable = true;
			comboBoxStatementAddress.FilterString = "";
			comboBoxStatementAddress.HasAllAccount = false;
			comboBoxStatementAddress.HasCustom = false;
			comboBoxStatementAddress.IsDataLoaded = false;
			comboBoxStatementAddress.Location = new System.Drawing.Point(413, 31);
			comboBoxStatementAddress.MaxDropDownItems = 12;
			comboBoxStatementAddress.Name = "comboBoxStatementAddress";
			comboBoxStatementAddress.ShowInactiveItems = false;
			comboBoxStatementAddress.ShowQuickAdd = true;
			comboBoxStatementAddress.Size = new System.Drawing.Size(140, 20);
			comboBoxStatementAddress.TabIndex = 3;
			comboBoxStatementAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStatementAddress.Visible = false;
			comboBoxShiptoAddress.Assigned = false;
			comboBoxShiptoAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShiptoAddress.CustomReportFieldName = "";
			comboBoxShiptoAddress.CustomReportKey = "";
			comboBoxShiptoAddress.CustomReportValueType = 1;
			comboBoxShiptoAddress.DescriptionTextBox = null;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShiptoAddress.DisplayLayout.Appearance = appearance119;
			comboBoxShiptoAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShiptoAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance120.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance120.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.Appearance = appearance120;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance121;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance122.BackColor2 = System.Drawing.SystemColors.Control;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance122.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShiptoAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance122;
			comboBoxShiptoAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShiptoAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShiptoAddress.DisplayLayout.Override.ActiveCellAppearance = appearance123;
			appearance124.BackColor = System.Drawing.SystemColors.Highlight;
			appearance124.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShiptoAddress.DisplayLayout.Override.ActiveRowAppearance = appearance124;
			comboBoxShiptoAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShiptoAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShiptoAddress.DisplayLayout.Override.CardAreaAppearance = appearance125;
			appearance126.BorderColor = System.Drawing.Color.Silver;
			appearance126.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShiptoAddress.DisplayLayout.Override.CellAppearance = appearance126;
			comboBoxShiptoAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShiptoAddress.DisplayLayout.Override.CellPadding = 0;
			appearance127.BackColor = System.Drawing.SystemColors.Control;
			appearance127.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance127.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance127.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShiptoAddress.DisplayLayout.Override.GroupByRowAppearance = appearance127;
			appearance128.TextHAlignAsString = "Left";
			comboBoxShiptoAddress.DisplayLayout.Override.HeaderAppearance = appearance128;
			comboBoxShiptoAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShiptoAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.Color.Silver;
			comboBoxShiptoAddress.DisplayLayout.Override.RowAppearance = appearance129;
			comboBoxShiptoAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShiptoAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance130;
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
			comboBoxShiptoAddress.Location = new System.Drawing.Point(123, 31);
			comboBoxShiptoAddress.MaxDropDownItems = 12;
			comboBoxShiptoAddress.Name = "comboBoxShiptoAddress";
			comboBoxShiptoAddress.ShowInactiveItems = false;
			comboBoxShiptoAddress.ShowQuickAdd = true;
			comboBoxShiptoAddress.Size = new System.Drawing.Size(156, 20);
			comboBoxShiptoAddress.TabIndex = 2;
			comboBoxShiptoAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShiptoAddress.Visible = false;
			comboBoxBilltoAddress.Assigned = false;
			comboBoxBilltoAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBilltoAddress.CustomReportFieldName = "";
			comboBoxBilltoAddress.CustomReportKey = "";
			comboBoxBilltoAddress.CustomReportValueType = 1;
			comboBoxBilltoAddress.DescriptionTextBox = null;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBilltoAddress.DisplayLayout.Appearance = appearance131;
			comboBoxBilltoAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBilltoAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance132.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance132.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.Appearance = appearance132;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance133;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance134.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance134.BackColor2 = System.Drawing.SystemColors.Control;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance134.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBilltoAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance134;
			comboBoxBilltoAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBilltoAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBilltoAddress.DisplayLayout.Override.ActiveCellAppearance = appearance135;
			appearance136.BackColor = System.Drawing.SystemColors.Highlight;
			appearance136.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBilltoAddress.DisplayLayout.Override.ActiveRowAppearance = appearance136;
			comboBoxBilltoAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBilltoAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBilltoAddress.DisplayLayout.Override.CardAreaAppearance = appearance137;
			appearance138.BorderColor = System.Drawing.Color.Silver;
			appearance138.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBilltoAddress.DisplayLayout.Override.CellAppearance = appearance138;
			comboBoxBilltoAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBilltoAddress.DisplayLayout.Override.CellPadding = 0;
			appearance139.BackColor = System.Drawing.SystemColors.Control;
			appearance139.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance139.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance139.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBilltoAddress.DisplayLayout.Override.GroupByRowAppearance = appearance139;
			appearance140.TextHAlignAsString = "Left";
			comboBoxBilltoAddress.DisplayLayout.Override.HeaderAppearance = appearance140;
			comboBoxBilltoAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBilltoAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			appearance141.BorderColor = System.Drawing.Color.Silver;
			comboBoxBilltoAddress.DisplayLayout.Override.RowAppearance = appearance141;
			comboBoxBilltoAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance142.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBilltoAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance142;
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
			comboBoxBilltoAddress.Location = new System.Drawing.Point(413, 9);
			comboBoxBilltoAddress.MaxDropDownItems = 12;
			comboBoxBilltoAddress.Name = "comboBoxBilltoAddress";
			comboBoxBilltoAddress.ShowInactiveItems = false;
			comboBoxBilltoAddress.ShowQuickAdd = true;
			comboBoxBilltoAddress.Size = new System.Drawing.Size(140, 20);
			comboBoxBilltoAddress.TabIndex = 1;
			comboBoxBilltoAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBilltoAddress.Visible = false;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(10, 55);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(89, 14);
			ultraFormattedLinkLabel8.TabIndex = 6;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Shipping Method:";
			ultraFormattedLinkLabel8.Visible = false;
			appearance143.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance143;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 11);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(69, 14);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Salesperson:";
			ultraFormattedLinkLabel5.Visible = false;
			appearance144.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance144;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxShippingMethods.Assigned = false;
			comboBoxShippingMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethods.CustomReportFieldName = "";
			comboBoxShippingMethods.CustomReportKey = "";
			comboBoxShippingMethods.CustomReportValueType = 1;
			comboBoxShippingMethods.DescriptionTextBox = null;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethods.DisplayLayout.Appearance = appearance145;
			comboBoxShippingMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.Appearance = appearance146;
			appearance147.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance147;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance148.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance148.BackColor2 = System.Drawing.SystemColors.Control;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance148.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance148;
			comboBoxShippingMethods.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethods.DisplayLayout.MaxRowScrollRegions = 1;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethods.DisplayLayout.Override.ActiveCellAppearance = appearance149;
			appearance150.BackColor = System.Drawing.SystemColors.Highlight;
			appearance150.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethods.DisplayLayout.Override.ActiveRowAppearance = appearance150;
			comboBoxShippingMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.Override.CardAreaAppearance = appearance151;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			appearance152.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethods.DisplayLayout.Override.CellAppearance = appearance152;
			comboBoxShippingMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethods.DisplayLayout.Override.CellPadding = 0;
			appearance153.BackColor = System.Drawing.SystemColors.Control;
			appearance153.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance153.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethods.DisplayLayout.Override.GroupByRowAppearance = appearance153;
			appearance154.TextHAlignAsString = "Left";
			comboBoxShippingMethods.DisplayLayout.Override.HeaderAppearance = appearance154;
			comboBoxShippingMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethods.DisplayLayout.Override.RowAppearance = appearance155;
			comboBoxShippingMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance156;
			comboBoxShippingMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethods.Editable = true;
			comboBoxShippingMethods.FilterString = "";
			comboBoxShippingMethods.HasAllAccount = false;
			comboBoxShippingMethods.HasCustom = false;
			comboBoxShippingMethods.IsDataLoaded = false;
			comboBoxShippingMethods.Location = new System.Drawing.Point(123, 53);
			comboBoxShippingMethods.MaxDropDownItems = 12;
			comboBoxShippingMethods.MaxLength = 15;
			comboBoxShippingMethods.Name = "comboBoxShippingMethods";
			comboBoxShippingMethods.ShowInactiveItems = false;
			comboBoxShippingMethods.ShowQuickAdd = true;
			comboBoxShippingMethods.Size = new System.Drawing.Size(156, 20);
			comboBoxShippingMethods.TabIndex = 4;
			comboBoxShippingMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShippingMethods.Visible = false;
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance157;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance164;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance166;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance167;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(123, 9);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.MaxLength = 64;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(156, 20);
			comboBoxSalesperson.TabIndex = 0;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesperson.Visible = false;
			ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox3.Controls.Add(mmLabel28);
			ultraGroupBox3.Controls.Add(textBoxBankName);
			ultraGroupBox3.Controls.Add(textBoxBankBranch);
			ultraGroupBox3.Controls.Add(mmLabel29);
			ultraGroupBox3.Controls.Add(textBoxBankAccountNumber);
			ultraGroupBox3.Controls.Add(mmLabel30);
			ultraGroupBox3.Location = new System.Drawing.Point(3, 146);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(628, 71);
			ultraGroupBox3.TabIndex = 11;
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
			textBoxBankName.Size = new System.Drawing.Size(156, 20);
			textBoxBankName.TabIndex = 1;
			textBoxBankBranch.BackColor = System.Drawing.Color.White;
			textBoxBankBranch.CustomReportFieldName = "";
			textBoxBankBranch.CustomReportKey = "";
			textBoxBankBranch.CustomReportValueType = 1;
			textBoxBankBranch.IsComboTextBox = false;
			textBoxBankBranch.IsModified = false;
			textBoxBankBranch.Location = new System.Drawing.Point(368, 22);
			textBoxBankBranch.MaxLength = 30;
			textBoxBankBranch.Name = "textBoxBankBranch";
			textBoxBankBranch.Size = new System.Drawing.Size(181, 20);
			textBoxBankBranch.TabIndex = 3;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(295, 25);
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
			textBoxBankAccountNumber.Location = new System.Drawing.Point(133, 44);
			textBoxBankAccountNumber.MaxLength = 30;
			textBoxBankAccountNumber.Name = "textBoxBankAccountNumber";
			textBoxBankAccountNumber.Size = new System.Drawing.Size(156, 20);
			textBoxBankAccountNumber.TabIndex = 5;
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(4, 46);
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
			ultraGroupBox2.Location = new System.Drawing.Point(3, 340);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(628, 53);
			ultraGroupBox2.TabIndex = 12;
			ultraGroupBox2.Text = "Accounts";
			textBoxARName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxARName.CustomReportFieldName = "";
			textBoxARName.CustomReportKey = "";
			textBoxARName.CustomReportValueType = 1;
			textBoxARName.IsComboTextBox = false;
			textBoxARName.IsModified = false;
			textBoxARName.Location = new System.Drawing.Point(331, 19);
			textBoxARName.MaxLength = 30;
			textBoxARName.Name = "textBoxARName";
			textBoxARName.ReadOnly = true;
			textBoxARName.Size = new System.Drawing.Size(278, 20);
			textBoxARName.TabIndex = 6;
			textBoxARName.TabStop = false;
			linkLabelARAccount.AutoSize = true;
			linkLabelARAccount.Location = new System.Drawing.Point(7, 24);
			linkLabelARAccount.Name = "linkLabelARAccount";
			linkLabelARAccount.Size = new System.Drawing.Size(111, 14);
			linkLabelARAccount.TabIndex = 0;
			linkLabelARAccount.TabStop = true;
			linkLabelARAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelARAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelARAccount.Value = "Account Receiveable:";
			appearance169.ForeColor = System.Drawing.Color.Blue;
			linkLabelARAccount.VisitedLinkAppearance = appearance169;
			linkLabelARAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelARAccount_LinkClicked);
			comboBoxARAccount.Assigned = false;
			comboBoxARAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxARAccount.CustomReportFieldName = "";
			comboBoxARAccount.CustomReportKey = "";
			comboBoxARAccount.CustomReportValueType = 1;
			comboBoxARAccount.DescriptionTextBox = null;
			appearance170.BackColor = System.Drawing.SystemColors.Window;
			appearance170.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxARAccount.DisplayLayout.Appearance = appearance170;
			comboBoxARAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxARAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance171.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance171.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance171.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance171.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.GroupByBox.Appearance = appearance171;
			appearance172.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance172;
			comboBoxARAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance173.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance173.BackColor2 = System.Drawing.SystemColors.Control;
			appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance173.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance173;
			comboBoxARAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxARAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance174.BackColor = System.Drawing.SystemColors.Window;
			appearance174.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxARAccount.DisplayLayout.Override.ActiveCellAppearance = appearance174;
			appearance175.BackColor = System.Drawing.SystemColors.Highlight;
			appearance175.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxARAccount.DisplayLayout.Override.ActiveRowAppearance = appearance175;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance176.BackColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.CardAreaAppearance = appearance176;
			appearance177.BorderColor = System.Drawing.Color.Silver;
			appearance177.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxARAccount.DisplayLayout.Override.CellAppearance = appearance177;
			comboBoxARAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxARAccount.DisplayLayout.Override.CellPadding = 0;
			appearance178.BackColor = System.Drawing.SystemColors.Control;
			appearance178.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance178.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance178.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance178.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.GroupByRowAppearance = appearance178;
			appearance179.TextHAlignAsString = "Left";
			comboBoxARAccount.DisplayLayout.Override.HeaderAppearance = appearance179;
			comboBoxARAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxARAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance180.BackColor = System.Drawing.SystemColors.Window;
			appearance180.BorderColor = System.Drawing.Color.Silver;
			comboBoxARAccount.DisplayLayout.Override.RowAppearance = appearance180;
			comboBoxARAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance181.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxARAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance181;
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
			comboBoxARAccount.TabIndex = 1;
			comboBoxARAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraTabPageControl2.Controls.Add(label2);
			ultraTabPageControl2.Controls.Add(ultraGroupBox5);
			ultraTabPageControl2.Controls.Add(textBoxPaymentTermName);
			ultraTabPageControl2.Controls.Add(textBoxPaymentMethodName);
			ultraTabPageControl2.Controls.Add(mmLabel2);
			ultraTabPageControl2.Controls.Add(comboBoxRating);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel7);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel6);
			ultraTabPageControl2.Controls.Add(comboBoxPaymentTerms);
			ultraTabPageControl2.Controls.Add(comboBoxPaymentMethods);
			ultraTabPageControl2.Controls.Add(ultraGroupBox4);
			ultraTabPageControl2.Controls.Add(comboBoxCollectionUser);
			ultraTabPageControl2.Controls.Add(mmLabel31);
			ultraTabPageControl2.Controls.Add(textBoxReviewBy);
			ultraTabPageControl2.Controls.Add(mmLabel26);
			ultraTabPageControl2.Controls.Add(dateTimePickerReviewDate);
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(696, 417);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(548, 87);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(0, 13);
			label2.TabIndex = 70;
			ultraGroupBox5.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox5.Controls.Add(panel2);
			ultraGroupBox5.Controls.Add(mmLabel33);
			ultraGroupBox5.Controls.Add(comboBoxInsuranceStatus);
			ultraGroupBox5.Location = new System.Drawing.Point(3, 225);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(628, 131);
			ultraGroupBox5.TabIndex = 9;
			ultraGroupBox5.Text = "Credit Insurance Info";
			panel2.Controls.Add(mmLabel39);
			panel2.Controls.Add(textBoxInsuranceRemarks);
			panel2.Controls.Add(mmLabel38);
			panel2.Controls.Add(mmLabel37);
			panel2.Controls.Add(textBoxInsuranceNumber);
			panel2.Controls.Add(textBoxInsuranceApprovedAmount);
			panel2.Controls.Add(mmLabel36);
			panel2.Controls.Add(textBoxInsuranceReqAmount);
			panel2.Controls.Add(dateTimePickerInsuranceDate);
			panel2.Controls.Add(mmLabel34);
			panel2.Location = new System.Drawing.Point(1, 42);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(627, 78);
			panel2.TabIndex = 1;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(6, 52);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(52, 13);
			mmLabel39.TabIndex = 69;
			mmLabel39.Text = "Remarks:";
			textBoxInsuranceRemarks.BackColor = System.Drawing.Color.White;
			textBoxInsuranceRemarks.CustomReportFieldName = "";
			textBoxInsuranceRemarks.CustomReportKey = "";
			textBoxInsuranceRemarks.CustomReportValueType = 1;
			textBoxInsuranceRemarks.IsComboTextBox = false;
			textBoxInsuranceRemarks.IsModified = false;
			textBoxInsuranceRemarks.Location = new System.Drawing.Point(115, 49);
			textBoxInsuranceRemarks.MaxLength = 255;
			textBoxInsuranceRemarks.Name = "textBoxInsuranceRemarks";
			textBoxInsuranceRemarks.Size = new System.Drawing.Size(487, 20);
			textBoxInsuranceRemarks.TabIndex = 4;
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(278, 6);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(99, 13);
			mmLabel38.TabIndex = 67;
			mmLabel38.Text = "Insurance Number:";
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(278, 29);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(98, 13);
			mmLabel37.TabIndex = 65;
			mmLabel37.Text = "Approved Amount:";
			textBoxInsuranceNumber.BackColor = System.Drawing.Color.White;
			textBoxInsuranceNumber.CustomReportFieldName = "";
			textBoxInsuranceNumber.CustomReportKey = "";
			textBoxInsuranceNumber.CustomReportValueType = 1;
			textBoxInsuranceNumber.IsComboTextBox = false;
			textBoxInsuranceNumber.IsModified = false;
			textBoxInsuranceNumber.Location = new System.Drawing.Point(387, 3);
			textBoxInsuranceNumber.MaxLength = 30;
			textBoxInsuranceNumber.Name = "textBoxInsuranceNumber";
			textBoxInsuranceNumber.Size = new System.Drawing.Size(149, 20);
			textBoxInsuranceNumber.TabIndex = 1;
			textBoxInsuranceApprovedAmount.AllowDecimal = true;
			textBoxInsuranceApprovedAmount.CustomReportFieldName = "";
			textBoxInsuranceApprovedAmount.CustomReportKey = "";
			textBoxInsuranceApprovedAmount.CustomReportValueType = 1;
			textBoxInsuranceApprovedAmount.IsComboTextBox = false;
			textBoxInsuranceApprovedAmount.IsModified = false;
			textBoxInsuranceApprovedAmount.Location = new System.Drawing.Point(387, 26);
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
			textBoxInsuranceApprovedAmount.Size = new System.Drawing.Size(149, 20);
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
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(6, 29);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(103, 13);
			mmLabel36.TabIndex = 63;
			mmLabel36.Text = "Requested Amount:";
			textBoxInsuranceReqAmount.AllowDecimal = true;
			textBoxInsuranceReqAmount.CustomReportFieldName = "";
			textBoxInsuranceReqAmount.CustomReportKey = "";
			textBoxInsuranceReqAmount.CustomReportValueType = 1;
			textBoxInsuranceReqAmount.IsComboTextBox = false;
			textBoxInsuranceReqAmount.IsModified = false;
			textBoxInsuranceReqAmount.Location = new System.Drawing.Point(115, 26);
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
			textBoxInsuranceReqAmount.Size = new System.Drawing.Size(156, 20);
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
			dateTimePickerInsuranceDate.Location = new System.Drawing.Point(115, 3);
			dateTimePickerInsuranceDate.Name = "dateTimePickerInsuranceDate";
			dateTimePickerInsuranceDate.ShowCheckBox = true;
			dateTimePickerInsuranceDate.Size = new System.Drawing.Size(156, 20);
			dateTimePickerInsuranceDate.TabIndex = 0;
			dateTimePickerInsuranceDate.Value = new System.DateTime(0L);
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(6, 6);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(89, 13);
			mmLabel34.TabIndex = 61;
			mmLabel34.Text = "Application Date:";
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(7, 22);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(93, 13);
			mmLabel33.TabIndex = 65;
			mmLabel33.Text = "Insurance Status:";
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
			comboBoxInsuranceStatus.Location = new System.Drawing.Point(116, 19);
			comboBoxInsuranceStatus.Name = "comboBoxInsuranceStatus";
			comboBoxInsuranceStatus.Size = new System.Drawing.Size(156, 21);
			comboBoxInsuranceStatus.TabIndex = 0;
			textBoxPaymentTermName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPaymentTermName.CustomReportFieldName = "";
			textBoxPaymentTermName.CustomReportKey = "";
			textBoxPaymentTermName.CustomReportValueType = 1;
			textBoxPaymentTermName.IsComboTextBox = false;
			textBoxPaymentTermName.IsModified = false;
			textBoxPaymentTermName.Location = new System.Drawing.Point(279, 34);
			textBoxPaymentTermName.MaxLength = 30;
			textBoxPaymentTermName.Name = "textBoxPaymentTermName";
			textBoxPaymentTermName.ReadOnly = true;
			textBoxPaymentTermName.Size = new System.Drawing.Size(327, 20);
			textBoxPaymentTermName.TabIndex = 3;
			textBoxPaymentTermName.TabStop = false;
			textBoxPaymentMethodName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPaymentMethodName.CustomReportFieldName = "";
			textBoxPaymentMethodName.CustomReportKey = "";
			textBoxPaymentMethodName.CustomReportValueType = 1;
			textBoxPaymentMethodName.IsComboTextBox = false;
			textBoxPaymentMethodName.IsModified = false;
			textBoxPaymentMethodName.Location = new System.Drawing.Point(279, 12);
			textBoxPaymentMethodName.MaxLength = 30;
			textBoxPaymentMethodName.Name = "textBoxPaymentMethodName";
			textBoxPaymentMethodName.ReadOnly = true;
			textBoxPaymentMethodName.Size = new System.Drawing.Size(327, 20);
			textBoxPaymentMethodName.TabIndex = 1;
			textBoxPaymentMethodName.TabStop = false;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(281, 83);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(74, 13);
			mmLabel2.TabIndex = 69;
			mmLabel2.Text = "Credit Rating:";
			comboBoxRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			valueListItem.DataValue = (byte)0;
			valueListItem2.DataValue = (byte)1;
			valueListItem2.DisplayText = "A";
			valueListItem3.DataValue = (byte)2;
			valueListItem3.DisplayText = "B";
			valueListItem4.DataValue = (byte)3;
			valueListItem4.DisplayText = "C";
			valueListItem5.DataValue = (byte)4;
			valueListItem5.DisplayText = "D";
			comboBoxRating.Items.AddRange(new Infragistics.Win.ValueListItem[5]
			{
				valueListItem,
				valueListItem2,
				valueListItem3,
				valueListItem4,
				valueListItem5
			});
			comboBoxRating.Location = new System.Drawing.Point(359, 81);
			comboBoxRating.Name = "comboBoxRating";
			comboBoxRating.Size = new System.Drawing.Size(156, 21);
			comboBoxRating.TabIndex = 7;
			comboBoxRating.ValueChanged += new System.EventHandler(comboBoxRating_ValueChanged);
			appearance182.FontData.BoldAsString = "False";
			appearance182.FontData.ItalicAsString = "False";
			appearance182.FontData.Name = "Microsoft Sans Serif";
			appearance182.FontData.SizeInPoints = 8.25f;
			appearance182.FontData.StrikeoutAsString = "False";
			appearance182.FontData.UnderlineAsString = "False";
			ultraFormattedLinkLabel7.Appearance = appearance182;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(10, 37);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabel7.TabIndex = 66;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Payment Term:";
			appearance183.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance183;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(10, 14);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(89, 14);
			ultraFormattedLinkLabel6.TabIndex = 64;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Payment Method:";
			appearance184.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance184;
			comboBoxPaymentTerms.Assigned = false;
			comboBoxPaymentTerms.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPaymentTerms.CustomReportFieldName = "";
			comboBoxPaymentTerms.CustomReportKey = "";
			comboBoxPaymentTerms.CustomReportValueType = 1;
			comboBoxPaymentTerms.DescriptionTextBox = textBoxPaymentTermName;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			appearance185.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPaymentTerms.DisplayLayout.Appearance = appearance185;
			comboBoxPaymentTerms.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPaymentTerms.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance186.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance186.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance186.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance186.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.Appearance = appearance186;
			appearance187.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.BandLabelAppearance = appearance187;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance188.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance188.BackColor2 = System.Drawing.SystemColors.Control;
			appearance188.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance188.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentTerms.DisplayLayout.GroupByBox.PromptAppearance = appearance188;
			comboBoxPaymentTerms.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPaymentTerms.DisplayLayout.MaxRowScrollRegions = 1;
			appearance189.BackColor = System.Drawing.SystemColors.Window;
			appearance189.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPaymentTerms.DisplayLayout.Override.ActiveCellAppearance = appearance189;
			appearance190.BackColor = System.Drawing.SystemColors.Highlight;
			appearance190.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPaymentTerms.DisplayLayout.Override.ActiveRowAppearance = appearance190;
			comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.Override.CardAreaAppearance = appearance191;
			appearance192.BorderColor = System.Drawing.Color.Silver;
			appearance192.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPaymentTerms.DisplayLayout.Override.CellAppearance = appearance192;
			comboBoxPaymentTerms.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPaymentTerms.DisplayLayout.Override.CellPadding = 0;
			appearance193.BackColor = System.Drawing.SystemColors.Control;
			appearance193.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance193.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance193.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance193.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentTerms.DisplayLayout.Override.GroupByRowAppearance = appearance193;
			appearance194.TextHAlignAsString = "Left";
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderAppearance = appearance194;
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPaymentTerms.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance195.BackColor = System.Drawing.SystemColors.Window;
			appearance195.BorderColor = System.Drawing.Color.Silver;
			comboBoxPaymentTerms.DisplayLayout.Override.RowAppearance = appearance195;
			comboBoxPaymentTerms.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance196.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPaymentTerms.DisplayLayout.Override.TemplateAddRowAppearance = appearance196;
			comboBoxPaymentTerms.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPaymentTerms.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPaymentTerms.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPaymentTerms.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPaymentTerms.Editable = true;
			comboBoxPaymentTerms.FilterString = "";
			comboBoxPaymentTerms.HasAllAccount = false;
			comboBoxPaymentTerms.HasCustom = false;
			comboBoxPaymentTerms.IsDataLoaded = false;
			comboBoxPaymentTerms.Location = new System.Drawing.Point(119, 35);
			comboBoxPaymentTerms.MaxDropDownItems = 12;
			comboBoxPaymentTerms.MaxLength = 15;
			comboBoxPaymentTerms.Name = "comboBoxPaymentTerms";
			comboBoxPaymentTerms.ShowInactiveItems = false;
			comboBoxPaymentTerms.ShowQuickAdd = true;
			comboBoxPaymentTerms.Size = new System.Drawing.Size(156, 20);
			comboBoxPaymentTerms.TabIndex = 2;
			comboBoxPaymentTerms.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPaymentMethods.Assigned = false;
			comboBoxPaymentMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPaymentMethods.CustomReportFieldName = "";
			comboBoxPaymentMethods.CustomReportKey = "";
			comboBoxPaymentMethods.CustomReportValueType = 1;
			comboBoxPaymentMethods.DescriptionTextBox = textBoxPaymentMethodName;
			appearance197.BackColor = System.Drawing.SystemColors.Window;
			appearance197.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPaymentMethods.DisplayLayout.Appearance = appearance197;
			comboBoxPaymentMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPaymentMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance198.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance198.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance198.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance198.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.Appearance = appearance198;
			appearance199.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance199;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance200.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance200.BackColor2 = System.Drawing.SystemColors.Control;
			appearance200.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance200.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance200;
			comboBoxPaymentMethods.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPaymentMethods.DisplayLayout.MaxRowScrollRegions = 1;
			appearance201.BackColor = System.Drawing.SystemColors.Window;
			appearance201.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPaymentMethods.DisplayLayout.Override.ActiveCellAppearance = appearance201;
			appearance202.BackColor = System.Drawing.SystemColors.Highlight;
			appearance202.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPaymentMethods.DisplayLayout.Override.ActiveRowAppearance = appearance202;
			comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance203.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.Override.CardAreaAppearance = appearance203;
			appearance204.BorderColor = System.Drawing.Color.Silver;
			appearance204.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPaymentMethods.DisplayLayout.Override.CellAppearance = appearance204;
			comboBoxPaymentMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPaymentMethods.DisplayLayout.Override.CellPadding = 0;
			appearance205.BackColor = System.Drawing.SystemColors.Control;
			appearance205.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance205.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance205.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance205.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethods.DisplayLayout.Override.GroupByRowAppearance = appearance205;
			appearance206.TextHAlignAsString = "Left";
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderAppearance = appearance206;
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPaymentMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance207.BackColor = System.Drawing.SystemColors.Window;
			appearance207.BorderColor = System.Drawing.Color.Silver;
			comboBoxPaymentMethods.DisplayLayout.Override.RowAppearance = appearance207;
			comboBoxPaymentMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance208.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPaymentMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance208;
			comboBoxPaymentMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPaymentMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPaymentMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPaymentMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPaymentMethods.Editable = true;
			comboBoxPaymentMethods.FilterString = "";
			comboBoxPaymentMethods.IsDataLoaded = false;
			comboBoxPaymentMethods.Location = new System.Drawing.Point(119, 12);
			comboBoxPaymentMethods.MaxDropDownItems = 12;
			comboBoxPaymentMethods.MaxLength = 15;
			comboBoxPaymentMethods.Name = "comboBoxPaymentMethods";
			comboBoxPaymentMethods.Size = new System.Drawing.Size(156, 20);
			comboBoxPaymentMethods.TabIndex = 0;
			comboBoxPaymentMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox4.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox4.Controls.Add(checkBoxAcceptCheque);
			ultraGroupBox4.Controls.Add(radioButtonSublimit);
			ultraGroupBox4.Controls.Add(checkBoxAcceptPDC);
			ultraGroupBox4.Controls.Add(textBoxCreditLimit);
			ultraGroupBox4.Controls.Add(radioButtonCreditLimitNoCredit);
			ultraGroupBox4.Controls.Add(radioButtonCreditLimitUnlimited);
			ultraGroupBox4.Controls.Add(radioButtonCreditLimitAmount);
			ultraGroupBox4.Location = new System.Drawing.Point(3, 119);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(628, 86);
			ultraGroupBox4.TabIndex = 8;
			ultraGroupBox4.Text = "Credit Limit";
			checkBoxAcceptCheque.AutoSize = true;
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptCheque.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptCheque.Location = new System.Drawing.Point(10, 54);
			checkBoxAcceptCheque.Name = "checkBoxAcceptCheque";
			checkBoxAcceptCheque.Size = new System.Drawing.Size(142, 17);
			checkBoxAcceptCheque.TabIndex = 5;
			checkBoxAcceptCheque.Text = "Accept cheque payment";
			checkBoxAcceptCheque.UseVisualStyleBackColor = true;
			radioButtonSublimit.AutoSize = true;
			radioButtonSublimit.Location = new System.Drawing.Point(430, 20);
			radioButtonSublimit.Name = "radioButtonSublimit";
			radioButtonSublimit.Size = new System.Drawing.Size(107, 17);
			radioButtonSublimit.TabIndex = 4;
			radioButtonSublimit.Text = "Sublimit of Parent";
			radioButtonSublimit.UseVisualStyleBackColor = true;
			checkBoxAcceptPDC.AutoSize = true;
			checkBoxAcceptPDC.Checked = true;
			checkBoxAcceptPDC.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAcceptPDC.Location = new System.Drawing.Point(216, 54);
			checkBoxAcceptPDC.Name = "checkBoxAcceptPDC";
			checkBoxAcceptPDC.Size = new System.Drawing.Size(195, 17);
			checkBoxAcceptPDC.TabIndex = 6;
			checkBoxAcceptPDC.Text = "Accept post-dated cheque payment";
			checkBoxAcceptPDC.UseVisualStyleBackColor = true;
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
			textBoxCreditLimit.Size = new System.Drawing.Size(114, 20);
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
			comboBoxCollectionUser.Assigned = false;
			comboBoxCollectionUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCollectionUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCollectionUser.CustomReportFieldName = "";
			comboBoxCollectionUser.CustomReportKey = "";
			comboBoxCollectionUser.CustomReportValueType = 1;
			comboBoxCollectionUser.DescriptionTextBox = null;
			appearance209.BackColor = System.Drawing.SystemColors.Window;
			appearance209.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCollectionUser.DisplayLayout.Appearance = appearance209;
			comboBoxCollectionUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCollectionUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance210.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance210.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance210.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance210.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.Appearance = appearance210;
			appearance211.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance211;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance212.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance212.BackColor2 = System.Drawing.SystemColors.Control;
			appearance212.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance212.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCollectionUser.DisplayLayout.GroupByBox.PromptAppearance = appearance212;
			comboBoxCollectionUser.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCollectionUser.DisplayLayout.MaxRowScrollRegions = 1;
			appearance213.BackColor = System.Drawing.SystemColors.Window;
			appearance213.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCollectionUser.DisplayLayout.Override.ActiveCellAppearance = appearance213;
			appearance214.BackColor = System.Drawing.SystemColors.Highlight;
			appearance214.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCollectionUser.DisplayLayout.Override.ActiveRowAppearance = appearance214;
			comboBoxCollectionUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCollectionUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance215.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCollectionUser.DisplayLayout.Override.CardAreaAppearance = appearance215;
			appearance216.BorderColor = System.Drawing.Color.Silver;
			appearance216.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCollectionUser.DisplayLayout.Override.CellAppearance = appearance216;
			comboBoxCollectionUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCollectionUser.DisplayLayout.Override.CellPadding = 0;
			appearance217.BackColor = System.Drawing.SystemColors.Control;
			appearance217.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance217.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance217.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance217.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCollectionUser.DisplayLayout.Override.GroupByRowAppearance = appearance217;
			appearance218.TextHAlignAsString = "Left";
			comboBoxCollectionUser.DisplayLayout.Override.HeaderAppearance = appearance218;
			comboBoxCollectionUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCollectionUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance219.BackColor = System.Drawing.SystemColors.Window;
			appearance219.BorderColor = System.Drawing.Color.Silver;
			comboBoxCollectionUser.DisplayLayout.Override.RowAppearance = appearance219;
			comboBoxCollectionUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance220.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCollectionUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance220;
			comboBoxCollectionUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCollectionUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCollectionUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCollectionUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCollectionUser.Editable = true;
			comboBoxCollectionUser.FilterString = "";
			comboBoxCollectionUser.HasAllAccount = false;
			comboBoxCollectionUser.HasCustom = false;
			comboBoxCollectionUser.IsDataLoaded = false;
			comboBoxCollectionUser.Location = new System.Drawing.Point(119, 80);
			comboBoxCollectionUser.MaxDropDownItems = 12;
			comboBoxCollectionUser.Name = "comboBoxCollectionUser";
			comboBoxCollectionUser.ShowInactiveItems = false;
			comboBoxCollectionUser.ShowQuickAdd = true;
			comboBoxCollectionUser.Size = new System.Drawing.Size(156, 20);
			comboBoxCollectionUser.TabIndex = 6;
			comboBoxCollectionUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(10, 83);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(82, 13);
			mmLabel31.TabIndex = 61;
			mmLabel31.Text = "Collection User:";
			textBoxReviewBy.BackColor = System.Drawing.Color.White;
			textBoxReviewBy.CustomReportFieldName = "";
			textBoxReviewBy.CustomReportKey = "";
			textBoxReviewBy.CustomReportValueType = 1;
			textBoxReviewBy.IsComboTextBox = false;
			textBoxReviewBy.IsModified = false;
			textBoxReviewBy.Location = new System.Drawing.Point(359, 59);
			textBoxReviewBy.MaxLength = 30;
			textBoxReviewBy.Name = "textBoxReviewBy";
			textBoxReviewBy.Size = new System.Drawing.Size(156, 20);
			textBoxReviewBy.TabIndex = 5;
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(281, 61);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(73, 13);
			mmLabel26.TabIndex = 60;
			mmLabel26.Text = "Reviewed By:";
			dateTimePickerReviewDate.Checked = false;
			dateTimePickerReviewDate.CustomFormat = " ";
			dateTimePickerReviewDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerReviewDate.Location = new System.Drawing.Point(119, 58);
			dateTimePickerReviewDate.Name = "dateTimePickerReviewDate";
			dateTimePickerReviewDate.ShowCheckBox = true;
			dateTimePickerReviewDate.Size = new System.Drawing.Size(156, 20);
			dateTimePickerReviewDate.TabIndex = 4;
			dateTimePickerReviewDate.Value = new System.DateTime(0L);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(10, 61);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(104, 13);
			mmLabel25.TabIndex = 59;
			mmLabel25.Text = "Credit Review Date:";
			tabPageContacts.Controls.Add(mmLabel35);
			tabPageContacts.Controls.Add(dataGridContacts);
			tabPageContacts.Controls.Add(gridComboBoxContact);
			tabPageContacts.Location = new System.Drawing.Point(-10000, -10000);
			tabPageContacts.Name = "tabPageContacts";
			tabPageContacts.Size = new System.Drawing.Size(696, 417);
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
			mmLabel35.Size = new System.Drawing.Size(171, 13);
			mmLabel35.TabIndex = 354;
			mmLabel35.Text = "Contacts related to this customer:";
			dataGridContacts.AllowAddNew = false;
			dataGridContacts.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance221.BackColor = System.Drawing.SystemColors.Window;
			appearance221.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridContacts.DisplayLayout.Appearance = appearance221;
			dataGridContacts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridContacts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance222.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance222.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance222.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance222.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.GroupByBox.Appearance = appearance222;
			appearance223.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance223;
			dataGridContacts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance224.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance224.BackColor2 = System.Drawing.SystemColors.Control;
			appearance224.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance224.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.PromptAppearance = appearance224;
			dataGridContacts.DisplayLayout.MaxColScrollRegions = 1;
			dataGridContacts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance225.BackColor = System.Drawing.SystemColors.Window;
			appearance225.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridContacts.DisplayLayout.Override.ActiveCellAppearance = appearance225;
			appearance226.BackColor = System.Drawing.SystemColors.Highlight;
			appearance226.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridContacts.DisplayLayout.Override.ActiveRowAppearance = appearance226;
			dataGridContacts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridContacts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridContacts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance227.BackColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.CardAreaAppearance = appearance227;
			appearance228.BorderColor = System.Drawing.Color.Silver;
			appearance228.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridContacts.DisplayLayout.Override.CellAppearance = appearance228;
			dataGridContacts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridContacts.DisplayLayout.Override.CellPadding = 0;
			appearance229.BackColor = System.Drawing.SystemColors.Control;
			appearance229.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance229.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance229.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance229.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.GroupByRowAppearance = appearance229;
			appearance230.TextHAlignAsString = "Left";
			dataGridContacts.DisplayLayout.Override.HeaderAppearance = appearance230;
			dataGridContacts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridContacts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance231.BackColor = System.Drawing.SystemColors.Window;
			appearance231.BorderColor = System.Drawing.Color.Silver;
			dataGridContacts.DisplayLayout.Override.RowAppearance = appearance231;
			dataGridContacts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance232.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridContacts.DisplayLayout.Override.TemplateAddRowAppearance = appearance232;
			dataGridContacts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridContacts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridContacts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridContacts.IncludeLotItems = false;
			dataGridContacts.LoadLayoutFailed = false;
			dataGridContacts.Location = new System.Drawing.Point(10, 31);
			dataGridContacts.Name = "dataGridContacts";
			dataGridContacts.ShowClearMenu = true;
			dataGridContacts.ShowDeleteMenu = true;
			dataGridContacts.ShowInsertMenu = true;
			dataGridContacts.ShowMoveRowsMenu = true;
			dataGridContacts.Size = new System.Drawing.Size(674, 369);
			dataGridContacts.TabIndex = 0;
			dataGridContacts.Text = "dataEntryGrid1";
			gridComboBoxContact.Assigned = false;
			gridComboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			gridComboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			gridComboBoxContact.CustomReportFieldName = "";
			gridComboBoxContact.CustomReportKey = "";
			gridComboBoxContact.CustomReportValueType = 1;
			gridComboBoxContact.DescriptionTextBox = null;
			appearance233.BackColor = System.Drawing.SystemColors.Window;
			appearance233.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			gridComboBoxContact.DisplayLayout.Appearance = appearance233;
			gridComboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			gridComboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance234.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance234.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance234.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance234.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance234;
			appearance235.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance235;
			gridComboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance236.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance236.BackColor2 = System.Drawing.SystemColors.Control;
			appearance236.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance236.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance236;
			gridComboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			gridComboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance237.BackColor = System.Drawing.SystemColors.Window;
			appearance237.ForeColor = System.Drawing.SystemColors.ControlText;
			gridComboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance237;
			appearance238.BackColor = System.Drawing.SystemColors.Highlight;
			appearance238.ForeColor = System.Drawing.SystemColors.HighlightText;
			gridComboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance238;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance239.BackColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance239;
			appearance240.BorderColor = System.Drawing.Color.Silver;
			appearance240.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			gridComboBoxContact.DisplayLayout.Override.CellAppearance = appearance240;
			gridComboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			gridComboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance241.BackColor = System.Drawing.SystemColors.Control;
			appearance241.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance241.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance241.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance241.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance241;
			appearance242.TextHAlignAsString = "Left";
			gridComboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance242;
			gridComboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			gridComboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance243.BackColor = System.Drawing.SystemColors.Window;
			appearance243.BorderColor = System.Drawing.Color.Silver;
			gridComboBoxContact.DisplayLayout.Override.RowAppearance = appearance243;
			gridComboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance244.BackColor = System.Drawing.SystemColors.ControlLight;
			gridComboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance244;
			gridComboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			gridComboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			gridComboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			gridComboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			gridComboBoxContact.Editable = true;
			gridComboBoxContact.FilterString = "";
			gridComboBoxContact.HasAllAccount = false;
			gridComboBoxContact.HasCustom = false;
			gridComboBoxContact.IsDataLoaded = false;
			gridComboBoxContact.Location = new System.Drawing.Point(287, 221);
			gridComboBoxContact.MaxDropDownItems = 12;
			gridComboBoxContact.Name = "gridComboBoxContact";
			gridComboBoxContact.ShowInactiveItems = false;
			gridComboBoxContact.ShowQuickAdd = true;
			gridComboBoxContact.Size = new System.Drawing.Size(127, 20);
			gridComboBoxContact.TabIndex = 356;
			gridComboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			gridComboBoxContact.Visible = false;
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(696, 417);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(8, 13);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(681, 390);
			textBoxNote.TabIndex = 43;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(696, 417);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(3, 3);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(690, 410);
			udfEntryGrid.TabIndex = 0;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(tabPageContacts);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 60);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(700, 513);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance245.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance245;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = ultraTabPageControl2;
			ultraTab3.Text = "Credit Control";
			ultraTab4.TabPage = tabPageContacts;
			ultraTab4.Text = "Con&tacts";
			ultraTab5.TabPage = ultraTabPageControl1;
			ultraTab5.Text = "&Note";
			ultraTab6.TabPage = tabPageUserDefined;
			ultraTab6.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[6]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(696, 490);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 573);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(700, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(700, 1);
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
			buttonClose.Location = new System.Drawing.Point(590, 8);
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
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
				toolStripButton1,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(700, 31);
			toolStrip1.TabIndex = 306;
			toolStrip1.Text = "toolStrip1";
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
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				documentsToolStripMenuItem1
			});
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.salesperson;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(81, 28);
			toolStripButton1.Text = "More...";
			documentsToolStripMenuItem1.Name = "documentsToolStripMenuItem1";
			documentsToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
			documentsToolStripMenuItem1.Text = "Documents";
			documentsToolStripMenuItem1.Click += new System.EventHandler(documentsToolStripMenuItem1_Click);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			panel1.TabIndex = 314;
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
			deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
			deleteRowToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			deleteRowToolStripMenuItem.Text = "Delete Row";
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
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(700, 613);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "PropertyTenantForm";
			Text = "Tenant";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CustomerClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(CustomerDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).EndInit();
			ultraGroupBox7.ResumeLayout(false);
			ultraGroupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSource).EndInit();
			panelConsignComm.ResumeLayout(false);
			panelConsignComm.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxStatementAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShiptoAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBilltoAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethods).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			ultraGroupBox5.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRating).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentTerms).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethods).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ultraGroupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCollectionUser).EndInit();
			tabPageContacts.ResumeLayout(false);
			tabPageContacts.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).EndInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).EndInit();
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
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
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
			comboBoxARAccount.SelectedIndexChanged += comboBoxARAccount_SelectedIndexChanged;
			textBoxName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			checkBoxAllowConsignment.CheckedChanged += checkBoxAllowConsignment_CheckedChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
		}

		private void checkBoxAllowConsignment_CheckedChanged(object sender, EventArgs e)
		{
			panelConsignComm.Visible = checkBoxAllowConsignment.Checked;
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
					ClearForm();
					textBoxCode.Focus();
					Init();
					if (CompanyPreferences.TaxEntityTypes.Contains("T"))
					{
						comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
						comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
					}
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
					comboBoxSalesperson.SelectedID = dataRow["SalesPersonID"].ToString();
					comboBoxPaymentTerms.SelectedID = dataRow["PaymentTermID"].ToString();
					comboBoxPaymentMethods.SelectedID = dataRow["PaymentMethodID"].ToString();
					comboBoxShippingMethods.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxBilltoAddress.SelectedID = dataRow["BillToAddressID"].ToString();
					comboBoxShiptoAddress.SelectedID = dataRow["ShipToAddressID"].ToString();
					comboBoxStatementAddress.SelectedID = dataRow["StatementAddressID"].ToString();
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
						comboBoxRating.SelectedIndex = -1;
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
					textBoxReviewBy.Text = dataRow["CreditReviewBy"].ToString();
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
						textBoxBankName.Text = dataRow["BankName"].ToString();
						textBoxBankBranch.Text = dataRow["BankBranch"].ToString();
						textBoxBankAccountNumber.Text = dataRow["BankAccountNumber"].ToString();
						comboBoxARAccount.SelectedID = dataRow["ARAccountID"].ToString();
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
			}
			catch
			{
				throw;
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
				dataRow["Note"] = textBoxNote.Text;
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
				if (comboBoxStatementAddress.SelectedID != "")
				{
					dataRow["StatementAddressID"] = comboBoxStatementAddress.SelectedID;
				}
				else
				{
					dataRow["StatementAddressID"] = DBNull.Value;
				}
				if (comboBoxRating.SelectedIndex != -1)
				{
					dataRow["Rating"] = comboBoxRating.SelectedIndex;
				}
				else
				{
					dataRow["Rating"] = DBNull.Value;
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
				dataRow["CreditReviewBy"] = textBoxReviewBy.Text;
				dataRow["AcceptCheckPayment"] = checkBoxAcceptCheque.Checked;
				dataRow["AcceptPDC"] = checkBoxAcceptPDC.Checked;
				dataRow["AllowConsignment"] = checkBoxAcceptPDC.Checked;
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
				else if (radioButtonSublimit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.ParentSublimit;
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
				dataRow["BankName"] = textBoxBankName.Text;
				dataRow["BankBranch"] = textBoxBankBranch.Text;
				dataRow["BankAccountNumber"] = textBoxBankAccountNumber.Text;
				if (comboBoxARAccount.SelectedID != "")
				{
					dataRow["ARAccountID"] = comboBoxARAccount.SelectedID;
				}
				else
				{
					dataRow["ARAccountID"] = DBNull.Value;
				}
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
					ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
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
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Customer", "CustomerID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					return false;
				}
				if (textBoxCode.Text == comboBoxParentCustomer.SelectedID)
				{
					ErrorHelper.WarningMessage("A customer cannot be parent of itself.");
					tabPageGeneral.Tab.Selected = true;
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
							IsNewRecord = false;
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
			Application.DoEvents();
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
			textBoxReviewBy.Clear();
			comboBoxRating.SelectedIndex = -1;
			checkBoxWeightInvoice.Checked = false;
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxAddress1.Clear();
			comboBoxCollectionUser.Clear();
			textBoxAddress2.Clear();
			textBoxAddress3.Clear();
			textBoxAddressID.Text = "PRIMARY";
			comboBoxBilltoAddress.Text = "PRIMARY";
			comboBoxShiptoAddress.Text = "PRIMARY";
			comboBoxStatementAddress.Text = "PRIMARY";
			textBoxAddressPrintFormat.Clear();
			textBoxBankAccountNumber.Clear();
			textBoxBankBranch.Clear();
			comboBoxCurrency.Clear();
			textBoxBankName.Clear();
			textBoxCity.Clear();
			textBoxComment.Clear();
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
			checkBoxIsInactive.Checked = false;
			checkBoxAllowConsignment.Checked = false;
			textBoxConsignCommission.Text = "0.00";
			comboBoxInsuranceStatus.SelectedIndex = 0;
			dateTimePickerInsuranceDate.Checked = false;
			textBoxInsuranceApprovedAmount.SetZero();
			textBoxInsuranceNumber.Clear();
			textBoxInsuranceRemarks.Clear();
			textBoxInsuranceReqAmount.SetZero();
			udfEntryGrid.ClearData();
			comboBoxARAccount.Clear();
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
			radioButtonCreditLimitUnlimited.Checked = true;
			IsNewRecord = true;
			textBoxTaxIDNumber.Clear();
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			if (CompanyPreferences.TaxEntityTypes.Contains("T"))
			{
				comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
				comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
			}
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Customer", "CustomerID");
			(dataGridContacts.DataSource as DataTable).Rows.Clear();
			formManager.ResetDirty();
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
			new FormHelper().EditTenantClass(comboBoxCustomerClass.Text);
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
			new FormHelper().EditSalesperson(comboBoxSalesperson.Text);
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditArea(comboBoxArea.Text);
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
			textBoxCreditLimit.Enabled = radioButtonCreditLimitAmount.Checked;
		}

		private void comboBoxCustomerClass_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, comboBoxBilltoAddress.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, comboBoxShiptoAddress.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, comboBoxStatementAddress.SelectedID);
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
			new FormHelper().ShowList(DataComboType.Tenant);
		}

		private void buttonCategories_Click(object sender, EventArgs e)
		{
			EntityCategoryAssignDialog entityCategoryAssignDialog = new EntityCategoryAssignDialog();
			entityCategoryAssignDialog.EntityID = textBoxCode.Text;
			entityCategoryAssignDialog.EntityName = textBoxName.Text;
			entityCategoryAssignDialog.EntityType = EntityTypesEnum.Customers;
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
			new FormHelper().EditGenericList(GenericListTypes.LeadSource, comboBoxLeadSource.SelectedID);
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
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void textBoxTaxIDNumber_TextChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxTaxOption_MouseUp(object sender, MouseEventArgs e)
		{
		}

		private void documentsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBoxCode.Text))
			{
				FormActivator.BringFormToFront(FormActivator.PropertyTenantDocumentsFormObj);
				FormActivator.PropertyTenantDocumentsFormObj.LoadData(textBoxCode.Text);
			}
		}
	}
}
