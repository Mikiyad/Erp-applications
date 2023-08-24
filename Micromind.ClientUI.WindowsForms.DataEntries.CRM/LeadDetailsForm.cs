using DevExpress.XtraEditors.Controls;
using DevExpress.XtraRichEdit;
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
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.CRM
{
	public class LeadDetailsForm : Form, IForm
	{
		private LeadData currentData;

		private DataSet activityData;

		private DataSet opportunityData;

		private const string TABLENAME_CONST = "Lead";

		private const string IDFIELD_CONST = "LeadID";

		private bool isNewRecord = true;

		private bool AllowFollowUp = CompanyPreferences.AllowFollowUponLead;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxCompanyName;

		private MMTextBox textBoxName;

		private MMLabel label1;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxCode;

		private MMLabel labelLeadNumber;

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

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private Panel panel1;

		private UltraTabPageControl tabPageContacts;

		private DataEntryGrid dataGridContacts;

		private MMLabel mmLabel35;

		private CountryComboBox comboBoxCountry;

		private AreaComboBox comboBoxArea;

		private UltraFormattedLinkLabel linkLabelArea;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel labelLeadNameHeader;

		private UltraComboEditor comboBoxRating;

		private MMLabel mmLabel2;

		private MMLabel mmLabel3;

		private MMSDateTimePicker dateTimePickerEstablished;

		private MMSDateTimePicker dateTimePickerLeadSince;

		private MMLabel mmLabel4;

		private XPButton buttonCategories;

		private UltraTabPageControl ultraTabPageNote;

		private MMTextBox textBoxNote;

		private NumberTextBox textBoxEmployeeNumber;

		private MMLabel mmLabel1;

		private MMLabel mmLabel7;

		private MMTextBox textBoxReferredBy;

		private ContactsComboBox gridComboBoxContact;

		private ContextMenuStrip contextMenuStripContact;

		private ToolStripMenuItem openContactToolStripMenuItem;

		private UltraTabPageControl ultraTabPageControl1;

		private UDFEntryControl udfEntryGrid;

		private LeadStatusComboBox comboBoxLeadStatus;

		private MMTextBox textBoxIndustry;

		private MMTextBox textBoxSource;

		private MMTextBox textBoxSalesperson;

		private GenericListComboBox comboBoxIndustry;

		private GenericListComboBox comboBoxSource;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel23;

		private MMTextBox textBoxComment;

		private MMLabel mmLabel22;

		private MMTextBox textBoxDepartment;

		private MMLabel mmLabel19;

		private MMTextBox textBoxWebsite;

		private XPButton buttonMoreAddress;

		private MMLabel mmLabel21;

		private MMTextBox textBoxAddressPrintFormat;

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

		private MMLabel mmLabel8;

		private MMTextBox textBoxAddressID;

		private UltraTabPageControl ultraTabPageControl2;

		private UltraTabPageControl ultraTabPageControl3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private MMTextBox textBoxLeadOwnerName;

		private DataGridList dataGridActivities;

		private DataGridList dataGridOpportunities;

		private MMLabel mmLabel27;

		private SalespersonComboBox comboBoxSalesperson;

		private ToolStripButton toolStripButtonInformation;

		private SalespersonComboBox comboBoxLeadOwner;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private MMLabel mmLabel25;

		private GadgetDateRangeComboBox comboBoxActivityPeriod;

		private Button buttonAddActivity;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem converttocustomerToolStripMenuItem;

		private UltraTabPageControl ultraTabPageControl4;

		private DataGridList dataGridListFollowup;

		private MMLabel mmLabel26;

		private GadgetDateRangeComboBox comboBoxFollowupPeriod;

		private Button buttonAddFollowup;

		private UltraTabPageControl ultraTabPageControl5;

		private MMLabel mmLabel48;

		private RichEditControl textBoxProfileDetails;

		private MMLabel mmLabel28;

		private NumberTextBox textBoxExpectValue;

		private MMLabel mmLabel30;

		private MMTextBox textBoxRemarks;

		private MMLabel mmLabel29;

		private GenericListComboBox comboBoxReason;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private GenericListComboBox comboBoxStage;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isLoading;

		private DataSet followUpData;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					textBoxAddressID.Enabled = false;
				}
				buttonAddActivity.Enabled = !value;
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

		public LeadDetailsForm()
		{
			InitializeComponent();
			dataGridContacts.DropDownMenu.Items.Insert(0, contextMenuStripContact.Items[0]);
			dataGridContacts.DropDownMenu.Items.Insert(1, new ToolStripSeparator());
			comboBoxActivityPeriod.LoadData();
			textBoxProfileDetails.ContentChanged += textBoxProfileDetails_ContentChanged;
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
			Infragistics.Win.ValueListItem valueListItem = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.CRM.LeadDetailsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxStage = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			comboBoxReason = new Micromind.DataControls.GenericListComboBox();
			textBoxExpectValue = new Micromind.UISupport.NumberTextBox();
			mmLabel28 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLeadOwner = new Micromind.DataControls.SalespersonComboBox();
			textBoxLeadOwnerName = new Micromind.UISupport.MMTextBox();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			textBoxSalesperson = new Micromind.UISupport.MMTextBox();
			textBoxIndustry = new Micromind.UISupport.MMTextBox();
			textBoxSource = new Micromind.UISupport.MMTextBox();
			comboBoxIndustry = new Micromind.DataControls.GenericListComboBox();
			comboBoxSource = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLeadStatus = new Micromind.DataControls.LeadStatusComboBox();
			buttonCategories = new Micromind.UISupport.XPButton();
			linkLabelArea = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxArea = new Micromind.DataControls.AreaComboBox();
			comboBoxCountry = new Micromind.DataControls.CountryComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelLeadNumber = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxFormalName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxForeignName = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
			textBoxCompanyName = new Micromind.UISupport.MMTextBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
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
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxReferredBy = new Micromind.UISupport.MMTextBox();
			textBoxEmployeeNumber = new Micromind.UISupport.NumberTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dateTimePickerLeadSince = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimePickerEstablished = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			tabPageContacts = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			gridComboBoxContact = new Micromind.DataControls.ContactsComboBox();
			dataGridContacts = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonAddActivity = new System.Windows.Forms.Button();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			comboBoxActivityPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			dataGridActivities = new Micromind.UISupport.DataGridList(components);
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridListFollowup = new Micromind.UISupport.DataGridList(components);
			mmLabel26 = new Micromind.UISupport.MMLabel();
			comboBoxFollowupPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			buttonAddFollowup = new System.Windows.Forms.Button();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridOpportunities = new Micromind.UISupport.DataGridList(components);
			mmLabel27 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxProfileDetails = new DevExpress.XtraRichEdit.RichEditControl();
			mmLabel48 = new Micromind.UISupport.MMLabel();
			ultraTabPageNote = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			converttocustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panel1 = new System.Windows.Forms.Panel();
			labelLeadNameHeader = new Micromind.UISupport.MMLabel();
			contextMenuStripContact = new System.Windows.Forms.ContextMenuStrip(components);
			openContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxStage).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadOwner).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIndustry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSource).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadStatus).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRating).BeginInit();
			tabPageContacts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxActivityPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridActivities).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridOpportunities).BeginInit();
			ultraTabPageControl5.SuspendLayout();
			ultraTabPageNote.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStripContact.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(comboBoxStage);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel7);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel6);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel4);
			tabPageGeneral.Controls.Add(mmLabel30);
			tabPageGeneral.Controls.Add(textBoxRemarks);
			tabPageGeneral.Controls.Add(mmLabel29);
			tabPageGeneral.Controls.Add(comboBoxReason);
			tabPageGeneral.Controls.Add(textBoxExpectValue);
			tabPageGeneral.Controls.Add(mmLabel28);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(comboBoxLeadOwner);
			tabPageGeneral.Controls.Add(comboBoxSalesperson);
			tabPageGeneral.Controls.Add(textBoxLeadOwnerName);
			tabPageGeneral.Controls.Add(textBoxIndustry);
			tabPageGeneral.Controls.Add(textBoxSource);
			tabPageGeneral.Controls.Add(textBoxSalesperson);
			tabPageGeneral.Controls.Add(comboBoxIndustry);
			tabPageGeneral.Controls.Add(comboBoxSource);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel2);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel1);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel5);
			tabPageGeneral.Controls.Add(comboBoxLeadStatus);
			tabPageGeneral.Controls.Add(buttonCategories);
			tabPageGeneral.Controls.Add(linkLabelArea);
			tabPageGeneral.Controls.Add(comboBoxArea);
			tabPageGeneral.Controls.Add(comboBoxCountry);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(labelLeadNumber);
			tabPageGeneral.Controls.Add(lblDescriptions);
			tabPageGeneral.Controls.Add(textBoxFormalName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxForeignName);
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Controls.Add(textBoxCompanyName);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(701, 457);
			tabPageGeneral.Paint += new System.Windows.Forms.PaintEventHandler(tabPageGeneral_Paint);
			comboBoxStage.Assigned = false;
			comboBoxStage.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxStage.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStage.CustomReportFieldName = "";
			comboBoxStage.CustomReportKey = "";
			comboBoxStage.CustomReportValueType = 1;
			comboBoxStage.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStage.DisplayLayout.Appearance = appearance;
			comboBoxStage.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStage.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStage.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStage.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxStage.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStage.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxStage.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStage.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStage.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStage.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxStage.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStage.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStage.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStage.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxStage.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStage.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStage.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxStage.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxStage.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStage.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxStage.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxStage.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStage.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxStage.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStage.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStage.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStage.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStage.Editable = true;
			comboBoxStage.FilterString = "";
			comboBoxStage.GenericListType = Micromind.Common.Data.GenericListTypes.Stage;
			comboBoxStage.HasAllAccount = false;
			comboBoxStage.HasCustom = false;
			comboBoxStage.IsDataLoaded = false;
			comboBoxStage.IsSingleColumn = false;
			comboBoxStage.Location = new System.Drawing.Point(467, 96);
			comboBoxStage.MaxDropDownItems = 12;
			comboBoxStage.Name = "comboBoxStage";
			comboBoxStage.ShowInactiveItems = false;
			comboBoxStage.ShowQuickAdd = true;
			comboBoxStage.Size = new System.Drawing.Size(204, 20);
			comboBoxStage.TabIndex = 11;
			comboBoxStage.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(368, 100);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel7.TabIndex = 159;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Stage :";
			appearance13.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance13;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(368, 33);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(46, 14);
			ultraFormattedLinkLabel6.TabIndex = 158;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Country:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(368, 76);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel4.TabIndex = 157;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Status:";
			appearance15.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance15;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			mmLabel30.AutoSize = true;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(10, 144);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(50, 13);
			mmLabel30.TabIndex = 156;
			mmLabel30.Text = "Remark :";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(132, 141);
			textBoxRemarks.MaxLength = 500;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(229, 62);
			textBoxRemarks.TabIndex = 6;
			mmLabel29.AutoSize = true;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(10, 121);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(50, 13);
			mmLabel29.TabIndex = 154;
			mmLabel29.Text = "Reason :";
			comboBoxReason.Assigned = false;
			comboBoxReason.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReason.CustomReportFieldName = "";
			comboBoxReason.CustomReportKey = "";
			comboBoxReason.CustomReportValueType = 1;
			comboBoxReason.DescriptionTextBox = null;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReason.DisplayLayout.Appearance = appearance16;
			comboBoxReason.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReason.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance17.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance17.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.GroupByBox.Appearance = appearance17;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.BandLabelAppearance = appearance18;
			comboBoxReason.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance19.BackColor2 = System.Drawing.SystemColors.Control;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.PromptAppearance = appearance19;
			comboBoxReason.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReason.DisplayLayout.MaxRowScrollRegions = 1;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReason.DisplayLayout.Override.ActiveCellAppearance = appearance20;
			appearance21.BackColor = System.Drawing.SystemColors.Highlight;
			appearance21.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReason.DisplayLayout.Override.ActiveRowAppearance = appearance21;
			comboBoxReason.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReason.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.CardAreaAppearance = appearance22;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			appearance23.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReason.DisplayLayout.Override.CellAppearance = appearance23;
			comboBoxReason.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReason.DisplayLayout.Override.CellPadding = 0;
			appearance24.BackColor = System.Drawing.SystemColors.Control;
			appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance24.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.GroupByRowAppearance = appearance24;
			appearance25.TextHAlignAsString = "Left";
			comboBoxReason.DisplayLayout.Override.HeaderAppearance = appearance25;
			comboBoxReason.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReason.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			comboBoxReason.DisplayLayout.Override.RowAppearance = appearance26;
			comboBoxReason.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReason.DisplayLayout.Override.TemplateAddRowAppearance = appearance27;
			comboBoxReason.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReason.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReason.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReason.DisplayMember = "Name";
			comboBoxReason.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReason.Editable = true;
			comboBoxReason.FilterString = "";
			comboBoxReason.GenericListType = Micromind.Common.Data.GenericListTypes.CRMActivityReason;
			comboBoxReason.HasAllAccount = false;
			comboBoxReason.HasCustom = false;
			comboBoxReason.IsDataLoaded = false;
			comboBoxReason.IsSingleColumn = true;
			comboBoxReason.Location = new System.Drawing.Point(132, 119);
			comboBoxReason.MaxDropDownItems = 12;
			comboBoxReason.Name = "comboBoxReason";
			comboBoxReason.ShowInactiveItems = false;
			comboBoxReason.ShowQuickAdd = false;
			comboBoxReason.Size = new System.Drawing.Size(229, 20);
			comboBoxReason.TabIndex = 5;
			comboBoxReason.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxExpectValue.AllowDecimal = true;
			textBoxExpectValue.BackColor = System.Drawing.Color.White;
			textBoxExpectValue.CustomReportFieldName = "";
			textBoxExpectValue.CustomReportKey = "";
			textBoxExpectValue.CustomReportValueType = 1;
			textBoxExpectValue.ForeColor = System.Drawing.Color.Black;
			textBoxExpectValue.IsComboTextBox = false;
			textBoxExpectValue.IsModified = false;
			textBoxExpectValue.Location = new System.Drawing.Point(467, 118);
			textBoxExpectValue.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxExpectValue.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxExpectValue.Name = "textBoxExpectValue";
			textBoxExpectValue.NullText = "0";
			textBoxExpectValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
			textBoxExpectValue.Size = new System.Drawing.Size(138, 20);
			textBoxExpectValue.TabIndex = 12;
			textBoxExpectValue.Text = "0.00";
			textBoxExpectValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(368, 123);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(33, 13);
			mmLabel28.TabIndex = 66;
			mmLabel28.Text = "Value";
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(12, 305);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(67, 14);
			ultraFormattedLinkLabel3.TabIndex = 65;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Lead Owner:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxLeadOwner.Assigned = false;
			comboBoxLeadOwner.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLeadOwner.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeadOwner.CustomReportFieldName = "";
			comboBoxLeadOwner.CustomReportKey = "";
			comboBoxLeadOwner.CustomReportValueType = 1;
			comboBoxLeadOwner.DescriptionTextBox = textBoxLeadOwnerName;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLeadOwner.DisplayLayout.Appearance = appearance29;
			comboBoxLeadOwner.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLeadOwner.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxLeadOwner.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLeadOwner.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLeadOwner.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLeadOwner.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxLeadOwner.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLeadOwner.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLeadOwner.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLeadOwner.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxLeadOwner.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLeadOwner.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadOwner.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxLeadOwner.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxLeadOwner.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLeadOwner.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxLeadOwner.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxLeadOwner.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLeadOwner.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxLeadOwner.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLeadOwner.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLeadOwner.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLeadOwner.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeadOwner.Editable = true;
			comboBoxLeadOwner.FilterString = "";
			comboBoxLeadOwner.HasAllAccount = false;
			comboBoxLeadOwner.HasCustom = false;
			comboBoxLeadOwner.IsDataLoaded = false;
			comboBoxLeadOwner.Location = new System.Drawing.Point(132, 302);
			comboBoxLeadOwner.MaxDropDownItems = 12;
			comboBoxLeadOwner.Name = "comboBoxLeadOwner";
			comboBoxLeadOwner.ShowInactiveItems = false;
			comboBoxLeadOwner.ShowQuickAdd = true;
			comboBoxLeadOwner.Size = new System.Drawing.Size(214, 20);
			comboBoxLeadOwner.TabIndex = 17;
			comboBoxLeadOwner.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxLeadOwnerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLeadOwnerName.CustomReportFieldName = "";
			textBoxLeadOwnerName.CustomReportKey = "";
			textBoxLeadOwnerName.CustomReportValueType = 1;
			textBoxLeadOwnerName.IsComboTextBox = false;
			textBoxLeadOwnerName.IsModified = false;
			textBoxLeadOwnerName.Location = new System.Drawing.Point(352, 302);
			textBoxLeadOwnerName.MaxLength = 64;
			textBoxLeadOwnerName.Name = "textBoxLeadOwnerName";
			textBoxLeadOwnerName.ReadOnly = true;
			textBoxLeadOwnerName.Size = new System.Drawing.Size(319, 20);
			textBoxLeadOwnerName.TabIndex = 29;
			textBoxLeadOwnerName.TabStop = false;
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = textBoxSalesperson;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(132, 233);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(214, 20);
			comboBoxSalesperson.TabIndex = 14;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxSalesperson.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesperson.CustomReportFieldName = "";
			textBoxSalesperson.CustomReportKey = "";
			textBoxSalesperson.CustomReportValueType = 1;
			textBoxSalesperson.IsComboTextBox = false;
			textBoxSalesperson.IsModified = false;
			textBoxSalesperson.Location = new System.Drawing.Point(352, 233);
			textBoxSalesperson.MaxLength = 64;
			textBoxSalesperson.Name = "textBoxSalesperson";
			textBoxSalesperson.ReadOnly = true;
			textBoxSalesperson.Size = new System.Drawing.Size(319, 20);
			textBoxSalesperson.TabIndex = 26;
			textBoxSalesperson.TabStop = false;
			textBoxIndustry.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxIndustry.CustomReportFieldName = "";
			textBoxIndustry.CustomReportKey = "";
			textBoxIndustry.CustomReportValueType = 1;
			textBoxIndustry.IsComboTextBox = false;
			textBoxIndustry.IsModified = false;
			textBoxIndustry.Location = new System.Drawing.Point(352, 279);
			textBoxIndustry.MaxLength = 64;
			textBoxIndustry.Name = "textBoxIndustry";
			textBoxIndustry.ReadOnly = true;
			textBoxIndustry.Size = new System.Drawing.Size(319, 20);
			textBoxIndustry.TabIndex = 28;
			textBoxIndustry.TabStop = false;
			textBoxSource.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSource.CustomReportFieldName = "";
			textBoxSource.CustomReportKey = "";
			textBoxSource.CustomReportValueType = 1;
			textBoxSource.IsComboTextBox = false;
			textBoxSource.IsModified = false;
			textBoxSource.Location = new System.Drawing.Point(352, 256);
			textBoxSource.MaxLength = 64;
			textBoxSource.Name = "textBoxSource";
			textBoxSource.ReadOnly = true;
			textBoxSource.Size = new System.Drawing.Size(319, 20);
			textBoxSource.TabIndex = 27;
			textBoxSource.TabStop = false;
			comboBoxIndustry.Assigned = false;
			comboBoxIndustry.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxIndustry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxIndustry.CustomReportFieldName = "";
			comboBoxIndustry.CustomReportKey = "";
			comboBoxIndustry.CustomReportValueType = 1;
			comboBoxIndustry.DescriptionTextBox = textBoxIndustry;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxIndustry.DisplayLayout.Appearance = appearance41;
			comboBoxIndustry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxIndustry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxIndustry.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxIndustry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxIndustry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxIndustry.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxIndustry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxIndustry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxIndustry.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxIndustry.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxIndustry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxIndustry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxIndustry.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxIndustry.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxIndustry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxIndustry.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxIndustry.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxIndustry.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxIndustry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxIndustry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxIndustry.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxIndustry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxIndustry.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxIndustry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxIndustry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxIndustry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxIndustry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxIndustry.Editable = true;
			comboBoxIndustry.FilterString = "";
			comboBoxIndustry.GenericListType = Micromind.Common.Data.GenericListTypes.Industry;
			comboBoxIndustry.HasAllAccount = false;
			comboBoxIndustry.HasCustom = false;
			comboBoxIndustry.IsDataLoaded = false;
			comboBoxIndustry.IsSingleColumn = false;
			comboBoxIndustry.Location = new System.Drawing.Point(132, 279);
			comboBoxIndustry.MaxDropDownItems = 12;
			comboBoxIndustry.Name = "comboBoxIndustry";
			comboBoxIndustry.ShowInactiveItems = false;
			comboBoxIndustry.ShowQuickAdd = true;
			comboBoxIndustry.Size = new System.Drawing.Size(214, 20);
			comboBoxIndustry.TabIndex = 16;
			comboBoxIndustry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSource.Assigned = false;
			comboBoxSource.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSource.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSource.CustomReportFieldName = "";
			comboBoxSource.CustomReportKey = "";
			comboBoxSource.CustomReportValueType = 1;
			comboBoxSource.DescriptionTextBox = textBoxSource;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSource.DisplayLayout.Appearance = appearance53;
			comboBoxSource.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSource.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSource.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSource.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxSource.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSource.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxSource.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSource.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSource.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSource.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxSource.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSource.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSource.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSource.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxSource.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSource.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSource.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxSource.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxSource.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSource.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxSource.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxSource.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSource.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxSource.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSource.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSource.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSource.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSource.Editable = true;
			comboBoxSource.FilterString = "";
			comboBoxSource.GenericListType = Micromind.Common.Data.GenericListTypes.LeadSource;
			comboBoxSource.HasAllAccount = false;
			comboBoxSource.HasCustom = false;
			comboBoxSource.IsDataLoaded = false;
			comboBoxSource.IsSingleColumn = false;
			comboBoxSource.Location = new System.Drawing.Point(132, 256);
			comboBoxSource.MaxDropDownItems = 12;
			comboBoxSource.Name = "comboBoxSource";
			comboBoxSource.ShowInactiveItems = false;
			comboBoxSource.ShowQuickAdd = true;
			comboBoxSource.Size = new System.Drawing.Size(214, 20);
			comboBoxSource.TabIndex = 15;
			comboBoxSource.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(12, 281);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel2.TabIndex = 63;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Industry:";
			appearance65.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance65;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 258);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(42, 14);
			ultraFormattedLinkLabel1.TabIndex = 62;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Source:";
			appearance66.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance66;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 235);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(69, 14);
			ultraFormattedLinkLabel5.TabIndex = 55;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Salesperson:";
			appearance67.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance67;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxLeadStatus.Assigned = false;
			comboBoxLeadStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeadStatus.CustomReportFieldName = "";
			comboBoxLeadStatus.CustomReportKey = "";
			comboBoxLeadStatus.CustomReportValueType = 1;
			comboBoxLeadStatus.DescriptionTextBox = null;
			comboBoxLeadStatus.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeadStatus.Editable = true;
			comboBoxLeadStatus.FilterString = "";
			comboBoxLeadStatus.HasAllAccount = false;
			comboBoxLeadStatus.HasCustom = false;
			comboBoxLeadStatus.IsDataLoaded = false;
			comboBoxLeadStatus.Location = new System.Drawing.Point(467, 74);
			comboBoxLeadStatus.MaxDropDownItems = 12;
			comboBoxLeadStatus.Name = "comboBoxLeadStatus";
			comboBoxLeadStatus.ShowInactiveItems = false;
			comboBoxLeadStatus.ShowQuickAdd = true;
			comboBoxLeadStatus.Size = new System.Drawing.Size(204, 20);
			comboBoxLeadStatus.TabIndex = 10;
			comboBoxLeadStatus.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonCategories.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCategories.BackColor = System.Drawing.Color.DarkGray;
			buttonCategories.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCategories.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCategories.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCategories.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCategories.Location = new System.Drawing.Point(537, 163);
			buttonCategories.Name = "buttonCategories";
			buttonCategories.Size = new System.Drawing.Size(134, 24);
			buttonCategories.TabIndex = 13;
			buttonCategories.Text = "Categories...";
			buttonCategories.UseVisualStyleBackColor = false;
			buttonCategories.Click += new System.EventHandler(buttonCategories_Click);
			linkLabelArea.AutoSize = true;
			linkLabelArea.Location = new System.Drawing.Point(368, 56);
			linkLabelArea.Name = "linkLabelArea";
			linkLabelArea.Size = new System.Drawing.Size(30, 14);
			linkLabelArea.TabIndex = 20;
			linkLabelArea.TabStop = true;
			linkLabelArea.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelArea.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelArea.Value = "Area:";
			appearance68.ForeColor = System.Drawing.Color.Blue;
			linkLabelArea.VisitedLinkAppearance = appearance68;
			linkLabelArea.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelArea_LinkClicked);
			comboBoxArea.Assigned = false;
			comboBoxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArea.CustomReportFieldName = "";
			comboBoxArea.CustomReportKey = "";
			comboBoxArea.CustomReportValueType = 1;
			comboBoxArea.DescriptionTextBox = null;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArea.DisplayLayout.Appearance = appearance69;
			comboBoxArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			comboBoxArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			comboBoxArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArea.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArea.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			comboBoxArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArea.DisplayLayout.Override.CellAppearance = appearance76;
			comboBoxArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArea.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			comboBoxArea.DisplayLayout.Override.HeaderAppearance = appearance78;
			comboBoxArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			comboBoxArea.DisplayLayout.Override.RowAppearance = appearance79;
			comboBoxArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			comboBoxArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArea.Editable = true;
			comboBoxArea.FilterString = "";
			comboBoxArea.HasAllAccount = false;
			comboBoxArea.HasCustom = false;
			comboBoxArea.IsDataLoaded = false;
			comboBoxArea.Location = new System.Drawing.Point(467, 53);
			comboBoxArea.MaxDropDownItems = 12;
			comboBoxArea.MaxLength = 15;
			comboBoxArea.Name = "comboBoxArea";
			comboBoxArea.ShowInactiveItems = false;
			comboBoxArea.ShowQuickAdd = true;
			comboBoxArea.Size = new System.Drawing.Size(204, 20);
			comboBoxArea.TabIndex = 9;
			comboBoxArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCountry.Assigned = false;
			comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCountry.CustomReportFieldName = "";
			comboBoxCountry.CustomReportKey = "";
			comboBoxCountry.CustomReportValueType = 1;
			comboBoxCountry.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCountry.DisplayLayout.Appearance = appearance81;
			comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCountry.Editable = true;
			comboBoxCountry.FilterString = "";
			comboBoxCountry.HasAllAccount = false;
			comboBoxCountry.HasCustom = false;
			comboBoxCountry.IsDataLoaded = false;
			comboBoxCountry.Location = new System.Drawing.Point(467, 31);
			comboBoxCountry.MaxDropDownItems = 12;
			comboBoxCountry.MaxLength = 15;
			comboBoxCountry.Name = "comboBoxCountry";
			comboBoxCountry.ShowInactiveItems = false;
			comboBoxCountry.ShowQuickAdd = true;
			comboBoxCountry.Size = new System.Drawing.Size(204, 20);
			comboBoxCountry.TabIndex = 8;
			comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			labelLeadNumber.AutoSize = true;
			labelLeadNumber.BackColor = System.Drawing.Color.Transparent;
			labelLeadNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelLeadNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelLeadNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelLeadNumber.IsFieldHeader = false;
			labelLeadNumber.IsRequired = true;
			labelLeadNumber.Location = new System.Drawing.Point(9, 10);
			labelLeadNumber.Name = "labelLeadNumber";
			labelLeadNumber.PenWidth = 1f;
			labelLeadNumber.ShowBorder = false;
			labelLeadNumber.Size = new System.Drawing.Size(68, 13);
			labelLeadNumber.TabIndex = 0;
			labelLeadNumber.Text = "Lead Code:";
			labelLeadNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(9, 99);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(86, 13);
			lblDescriptions.TabIndex = 6;
			lblDescriptions.Text = "Company Name:";
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
			checkBoxIsInactive.TabIndex = 7;
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
			label1.Size = new System.Drawing.Size(72, 13);
			label1.TabIndex = 2;
			label1.Text = "Lead Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCompanyName.BackColor = System.Drawing.Color.White;
			textBoxCompanyName.CustomReportFieldName = "";
			textBoxCompanyName.CustomReportKey = "";
			textBoxCompanyName.CustomReportValueType = 1;
			textBoxCompanyName.IsComboTextBox = false;
			textBoxCompanyName.IsModified = false;
			textBoxCompanyName.Location = new System.Drawing.Point(132, 97);
			textBoxCompanyName.MaxLength = 64;
			textBoxCompanyName.Name = "textBoxCompanyName";
			textBoxCompanyName.Size = new System.Drawing.Size(229, 20);
			textBoxCompanyName.TabIndex = 4;
			tabPageDetails.Controls.Add(ultraGroupBox1);
			tabPageDetails.Controls.Add(mmLabel7);
			tabPageDetails.Controls.Add(textBoxReferredBy);
			tabPageDetails.Controls.Add(textBoxEmployeeNumber);
			tabPageDetails.Controls.Add(mmLabel1);
			tabPageDetails.Controls.Add(dateTimePickerLeadSince);
			tabPageDetails.Controls.Add(mmLabel4);
			tabPageDetails.Controls.Add(dateTimePickerEstablished);
			tabPageDetails.Controls.Add(mmLabel3);
			tabPageDetails.Controls.Add(mmLabel2);
			tabPageDetails.Controls.Add(comboBoxRating);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(701, 457);
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
			ultraGroupBox1.Location = new System.Drawing.Point(2, 100);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(687, 310);
			ultraGroupBox1.TabIndex = 55;
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
			textBoxComment.TabIndex = 17;
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
			textBoxDepartment.TabIndex = 10;
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
			textBoxWebsite.TabIndex = 16;
			buttonMoreAddress.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonMoreAddress.BackColor = System.Drawing.Color.DarkGray;
			buttonMoreAddress.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonMoreAddress.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonMoreAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonMoreAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonMoreAddress.Location = new System.Drawing.Point(537, 199);
			buttonMoreAddress.Name = "buttonMoreAddress";
			buttonMoreAddress.Size = new System.Drawing.Size(134, 24);
			buttonMoreAddress.TabIndex = 18;
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
			textBoxAddressPrintFormat.TabIndex = 9;
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
			textBoxPostalCode.TabIndex = 8;
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
			textBoxEmail.TabIndex = 15;
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
			textBoxMobile.TabIndex = 14;
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
			textBoxFax.TabIndex = 13;
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
			textBoxPhone2.TabIndex = 12;
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
			textBoxPhone1.TabIndex = 11;
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
			textBoxCountry.TabIndex = 7;
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
			textBoxState.TabIndex = 6;
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
			textBoxCity.TabIndex = 5;
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
			textBoxAddress3.TabIndex = 4;
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
			textBoxAddress2.TabIndex = 3;
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
			textBoxAddress1.TabIndex = 2;
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
			textBoxContactName.TabIndex = 1;
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
			textBoxAddressID.TabIndex = 0;
			textBoxAddressID.Text = "PRIMARY";
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(352, 38);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(69, 13);
			mmLabel7.TabIndex = 52;
			mmLabel7.Text = "Referred By:";
			textBoxReferredBy.BackColor = System.Drawing.Color.White;
			textBoxReferredBy.CustomReportFieldName = "";
			textBoxReferredBy.CustomReportKey = "";
			textBoxReferredBy.CustomReportValueType = 1;
			textBoxReferredBy.IsComboTextBox = false;
			textBoxReferredBy.IsModified = false;
			textBoxReferredBy.Location = new System.Drawing.Point(427, 35);
			textBoxReferredBy.MaxLength = 64;
			textBoxReferredBy.Name = "textBoxReferredBy";
			textBoxReferredBy.Size = new System.Drawing.Size(216, 20);
			textBoxReferredBy.TabIndex = 9;
			textBoxEmployeeNumber.AllowDecimal = true;
			textBoxEmployeeNumber.CustomReportFieldName = "";
			textBoxEmployeeNumber.CustomReportKey = "";
			textBoxEmployeeNumber.CustomReportValueType = 1;
			textBoxEmployeeNumber.IsComboTextBox = false;
			textBoxEmployeeNumber.IsModified = false;
			textBoxEmployeeNumber.Location = new System.Drawing.Point(138, 35);
			textBoxEmployeeNumber.MaxValue = new decimal(new int[4]
			{
				1000000,
				0,
				0,
				0
			});
			textBoxEmployeeNumber.MinValue = new decimal(new int[4]);
			textBoxEmployeeNumber.Name = "textBoxEmployeeNumber";
			textBoxEmployeeNumber.NullText = "0";
			textBoxEmployeeNumber.Size = new System.Drawing.Size(110, 20);
			textBoxEmployeeNumber.TabIndex = 8;
			textBoxEmployeeNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(10, 38);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(99, 13);
			mmLabel1.TabIndex = 49;
			mmLabel1.Text = "Num of Employees:";
			dateTimePickerLeadSince.Checked = false;
			dateTimePickerLeadSince.CustomFormat = " ";
			dateTimePickerLeadSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerLeadSince.Location = new System.Drawing.Point(427, 12);
			dateTimePickerLeadSince.Name = "dateTimePickerLeadSince";
			dateTimePickerLeadSince.ShowCheckBox = true;
			dateTimePickerLeadSince.Size = new System.Drawing.Size(216, 20);
			dateTimePickerLeadSince.TabIndex = 7;
			dateTimePickerLeadSince.Value = new System.DateTime(0L);
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(352, 16);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(62, 13);
			mmLabel4.TabIndex = 48;
			mmLabel4.Text = "Lead Since:";
			dateTimePickerEstablished.Checked = false;
			dateTimePickerEstablished.CustomFormat = " ";
			dateTimePickerEstablished.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerEstablished.Location = new System.Drawing.Point(138, 12);
			dateTimePickerEstablished.Name = "dateTimePickerEstablished";
			dateTimePickerEstablished.ShowCheckBox = true;
			dateTimePickerEstablished.Size = new System.Drawing.Size(208, 20);
			dateTimePickerEstablished.TabIndex = 6;
			dateTimePickerEstablished.Value = new System.DateTime(0L);
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(10, 16);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(91, 13);
			mmLabel3.TabIndex = 46;
			mmLabel3.Text = "Date Established:";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(10, 62);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(42, 13);
			mmLabel2.TabIndex = 44;
			mmLabel2.Text = "Rating:";
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
			comboBoxRating.Location = new System.Drawing.Point(138, 58);
			comboBoxRating.Name = "comboBoxRating";
			comboBoxRating.Size = new System.Drawing.Size(208, 21);
			comboBoxRating.TabIndex = 10;
			tabPageContacts.Controls.Add(mmLabel35);
			tabPageContacts.Controls.Add(gridComboBoxContact);
			tabPageContacts.Controls.Add(dataGridContacts);
			tabPageContacts.Location = new System.Drawing.Point(-10000, -10000);
			tabPageContacts.Name = "tabPageContacts";
			tabPageContacts.Size = new System.Drawing.Size(701, 457);
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
			mmLabel35.Size = new System.Drawing.Size(147, 13);
			mmLabel35.TabIndex = 354;
			mmLabel35.Text = "Contacts related to this lead:";
			gridComboBoxContact.Assigned = false;
			gridComboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			gridComboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			gridComboBoxContact.CustomReportFieldName = "";
			gridComboBoxContact.CustomReportKey = "";
			gridComboBoxContact.CustomReportValueType = 1;
			gridComboBoxContact.DescriptionTextBox = null;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			gridComboBoxContact.DisplayLayout.Appearance = appearance93;
			gridComboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			gridComboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			gridComboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			gridComboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			gridComboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			gridComboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			gridComboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			gridComboBoxContact.DisplayLayout.Override.CellAppearance = appearance100;
			gridComboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			gridComboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			gridComboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance102;
			gridComboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			gridComboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			gridComboBoxContact.DisplayLayout.Override.RowAppearance = appearance103;
			gridComboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			gridComboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			gridComboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			gridComboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			gridComboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			gridComboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			gridComboBoxContact.Editable = true;
			gridComboBoxContact.FilterString = "";
			gridComboBoxContact.HasAllAccount = false;
			gridComboBoxContact.HasCustom = false;
			gridComboBoxContact.IsDataLoaded = false;
			gridComboBoxContact.Location = new System.Drawing.Point(25, 146);
			gridComboBoxContact.MaxDropDownItems = 12;
			gridComboBoxContact.Name = "gridComboBoxContact";
			gridComboBoxContact.ShowInactiveItems = false;
			gridComboBoxContact.ShowQuickAdd = true;
			gridComboBoxContact.Size = new System.Drawing.Size(127, 20);
			gridComboBoxContact.TabIndex = 355;
			gridComboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			gridComboBoxContact.Visible = false;
			dataGridContacts.AllowAddNew = false;
			dataGridContacts.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridContacts.DisplayLayout.Appearance = appearance105;
			dataGridContacts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridContacts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			dataGridContacts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			dataGridContacts.DisplayLayout.MaxColScrollRegions = 1;
			dataGridContacts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridContacts.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridContacts.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			dataGridContacts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridContacts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridContacts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridContacts.DisplayLayout.Override.CellAppearance = appearance112;
			dataGridContacts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridContacts.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			dataGridContacts.DisplayLayout.Override.HeaderAppearance = appearance114;
			dataGridContacts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridContacts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			dataGridContacts.DisplayLayout.Override.RowAppearance = appearance115;
			dataGridContacts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridContacts.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
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
			dataGridContacts.Size = new System.Drawing.Size(679, 409);
			dataGridContacts.TabIndex = 0;
			dataGridContacts.Text = "dataEntryGrid1";
			ultraTabPageControl2.Controls.Add(buttonAddActivity);
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Controls.Add(comboBoxActivityPeriod);
			ultraTabPageControl2.Controls.Add(dataGridActivities);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(701, 457);
			buttonAddActivity.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonAddActivity.Location = new System.Drawing.Point(12, 9);
			buttonAddActivity.Name = "buttonAddActivity";
			buttonAddActivity.Size = new System.Drawing.Size(23, 22);
			buttonAddActivity.TabIndex = 359;
			buttonAddActivity.UseVisualStyleBackColor = true;
			buttonAddActivity.Click += new System.EventHandler(button1_Click);
			mmLabel25.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(496, 11);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(41, 13);
			mmLabel25.TabIndex = 358;
			mmLabel25.Text = "Period:";
			comboBoxActivityPeriod.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxActivityPeriod.Location = new System.Drawing.Point(537, 8);
			comboBoxActivityPeriod.Name = "comboBoxActivityPeriod";
			comboBoxActivityPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxActivityPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxActivityPeriod.Size = new System.Drawing.Size(152, 20);
			comboBoxActivityPeriod.TabIndex = 357;
			comboBoxActivityPeriod.SelectedIndexChanged += new System.EventHandler(comboBoxActivityPeriod_SelectedIndexChanged);
			dataGridActivities.AllowUnfittedView = false;
			dataGridActivities.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridActivities.DisplayLayout.Appearance = appearance117;
			dataGridActivities.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridActivities.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.GroupByBox.Appearance = appearance118;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridActivities.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
			dataGridActivities.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance120.BackColor2 = System.Drawing.SystemColors.Control;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridActivities.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
			dataGridActivities.DisplayLayout.MaxColScrollRegions = 1;
			dataGridActivities.DisplayLayout.MaxRowScrollRegions = 1;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridActivities.DisplayLayout.Override.ActiveCellAppearance = appearance121;
			appearance122.BackColor = System.Drawing.SystemColors.Highlight;
			appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridActivities.DisplayLayout.Override.ActiveRowAppearance = appearance122;
			dataGridActivities.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridActivities.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.Override.CardAreaAppearance = appearance123;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridActivities.DisplayLayout.Override.CellAppearance = appearance124;
			dataGridActivities.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridActivities.DisplayLayout.Override.CellPadding = 0;
			appearance125.BackColor = System.Drawing.SystemColors.Control;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.Override.GroupByRowAppearance = appearance125;
			appearance126.TextHAlignAsString = "Left";
			dataGridActivities.DisplayLayout.Override.HeaderAppearance = appearance126;
			dataGridActivities.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridActivities.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.Color.Silver;
			dataGridActivities.DisplayLayout.Override.RowAppearance = appearance127;
			dataGridActivities.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridActivities.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
			dataGridActivities.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridActivities.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridActivities.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridActivities.LoadLayoutFailed = false;
			dataGridActivities.Location = new System.Drawing.Point(10, 31);
			dataGridActivities.Name = "dataGridActivities";
			dataGridActivities.ShowDeleteMenu = false;
			dataGridActivities.ShowMinusInRed = true;
			dataGridActivities.ShowNewMenu = false;
			dataGridActivities.Size = new System.Drawing.Size(679, 409);
			dataGridActivities.TabIndex = 356;
			dataGridActivities.Text = "dataGridList1";
			ultraTabPageControl4.Controls.Add(dataGridListFollowup);
			ultraTabPageControl4.Controls.Add(mmLabel26);
			ultraTabPageControl4.Controls.Add(comboBoxFollowupPeriod);
			ultraTabPageControl4.Controls.Add(buttonAddFollowup);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(701, 457);
			dataGridListFollowup.AllowUnfittedView = false;
			dataGridListFollowup.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListFollowup.DisplayLayout.Appearance = appearance129;
			dataGridListFollowup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListFollowup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.GroupByBox.Appearance = appearance130;
			appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance131;
			dataGridListFollowup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance132.BackColor2 = System.Drawing.SystemColors.Control;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.PromptAppearance = appearance132;
			dataGridListFollowup.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListFollowup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListFollowup.DisplayLayout.Override.ActiveCellAppearance = appearance133;
			appearance134.BackColor = System.Drawing.SystemColors.Highlight;
			appearance134.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListFollowup.DisplayLayout.Override.ActiveRowAppearance = appearance134;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.CardAreaAppearance = appearance135;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			appearance136.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListFollowup.DisplayLayout.Override.CellAppearance = appearance136;
			dataGridListFollowup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListFollowup.DisplayLayout.Override.CellPadding = 0;
			appearance137.BackColor = System.Drawing.SystemColors.Control;
			appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance137.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance137.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.GroupByRowAppearance = appearance137;
			appearance138.TextHAlignAsString = "Left";
			dataGridListFollowup.DisplayLayout.Override.HeaderAppearance = appearance138;
			dataGridListFollowup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListFollowup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.Color.Silver;
			dataGridListFollowup.DisplayLayout.Override.RowAppearance = appearance139;
			dataGridListFollowup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListFollowup.DisplayLayout.Override.TemplateAddRowAppearance = appearance140;
			dataGridListFollowup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListFollowup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListFollowup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListFollowup.LoadLayoutFailed = false;
			dataGridListFollowup.Location = new System.Drawing.Point(11, 39);
			dataGridListFollowup.Name = "dataGridListFollowup";
			dataGridListFollowup.ShowDeleteMenu = false;
			dataGridListFollowup.ShowMinusInRed = true;
			dataGridListFollowup.ShowNewMenu = false;
			dataGridListFollowup.Size = new System.Drawing.Size(679, 409);
			dataGridListFollowup.TabIndex = 369;
			dataGridListFollowup.Text = "dataGridList1";
			mmLabel26.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(493, 17);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(41, 13);
			mmLabel26.TabIndex = 368;
			mmLabel26.Text = "Period:";
			comboBoxFollowupPeriod.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxFollowupPeriod.Location = new System.Drawing.Point(537, 13);
			comboBoxFollowupPeriod.Name = "comboBoxFollowupPeriod";
			comboBoxFollowupPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxFollowupPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxFollowupPeriod.Size = new System.Drawing.Size(152, 20);
			comboBoxFollowupPeriod.TabIndex = 367;
			comboBoxFollowupPeriod.SelectedIndexChanged += new System.EventHandler(comboBoxFollowupPeriod_SelectedIndexChanged);
			buttonAddFollowup.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonAddFollowup.Location = new System.Drawing.Point(10, 12);
			buttonAddFollowup.Name = "buttonAddFollowup";
			buttonAddFollowup.Size = new System.Drawing.Size(23, 22);
			buttonAddFollowup.TabIndex = 366;
			buttonAddFollowup.UseVisualStyleBackColor = true;
			buttonAddFollowup.Click += new System.EventHandler(buttonAddFollowup_Click);
			ultraTabPageControl3.Controls.Add(dataGridOpportunities);
			ultraTabPageControl3.Controls.Add(mmLabel27);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(701, 457);
			dataGridOpportunities.AllowUnfittedView = false;
			dataGridOpportunities.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			appearance141.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridOpportunities.DisplayLayout.Appearance = appearance141;
			dataGridOpportunities.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridOpportunities.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance142.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance142.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance142.BorderColor = System.Drawing.SystemColors.Window;
			dataGridOpportunities.DisplayLayout.GroupByBox.Appearance = appearance142;
			appearance143.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridOpportunities.DisplayLayout.GroupByBox.BandLabelAppearance = appearance143;
			dataGridOpportunities.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance144.BackColor2 = System.Drawing.SystemColors.Control;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridOpportunities.DisplayLayout.GroupByBox.PromptAppearance = appearance144;
			dataGridOpportunities.DisplayLayout.MaxColScrollRegions = 1;
			dataGridOpportunities.DisplayLayout.MaxRowScrollRegions = 1;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridOpportunities.DisplayLayout.Override.ActiveCellAppearance = appearance145;
			appearance146.BackColor = System.Drawing.SystemColors.Highlight;
			appearance146.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridOpportunities.DisplayLayout.Override.ActiveRowAppearance = appearance146;
			dataGridOpportunities.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridOpportunities.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			dataGridOpportunities.DisplayLayout.Override.CardAreaAppearance = appearance147;
			appearance148.BorderColor = System.Drawing.Color.Silver;
			appearance148.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridOpportunities.DisplayLayout.Override.CellAppearance = appearance148;
			dataGridOpportunities.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridOpportunities.DisplayLayout.Override.CellPadding = 0;
			appearance149.BackColor = System.Drawing.SystemColors.Control;
			appearance149.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance149.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance149.BorderColor = System.Drawing.SystemColors.Window;
			dataGridOpportunities.DisplayLayout.Override.GroupByRowAppearance = appearance149;
			appearance150.TextHAlignAsString = "Left";
			dataGridOpportunities.DisplayLayout.Override.HeaderAppearance = appearance150;
			dataGridOpportunities.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridOpportunities.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			appearance151.BorderColor = System.Drawing.Color.Silver;
			dataGridOpportunities.DisplayLayout.Override.RowAppearance = appearance151;
			dataGridOpportunities.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance152.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridOpportunities.DisplayLayout.Override.TemplateAddRowAppearance = appearance152;
			dataGridOpportunities.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridOpportunities.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridOpportunities.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridOpportunities.LoadLayoutFailed = false;
			dataGridOpportunities.Location = new System.Drawing.Point(10, 31);
			dataGridOpportunities.Name = "dataGridOpportunities";
			dataGridOpportunities.ShowDeleteMenu = false;
			dataGridOpportunities.ShowMinusInRed = true;
			dataGridOpportunities.ShowNewMenu = false;
			dataGridOpportunities.Size = new System.Drawing.Size(679, 409);
			dataGridOpportunities.TabIndex = 358;
			dataGridOpportunities.Text = "dataGridList1";
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(10, 15);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(169, 13);
			mmLabel27.TabIndex = 357;
			mmLabel27.Text = "Opportunities related to this lead:";
			ultraTabPageControl5.Controls.Add(textBoxProfileDetails);
			ultraTabPageControl5.Controls.Add(mmLabel48);
			ultraTabPageControl5.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(701, 457);
			textBoxProfileDetails.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxProfileDetails.Location = new System.Drawing.Point(5, 20);
			textBoxProfileDetails.Name = "textBoxProfileDetails";
			textBoxProfileDetails.Size = new System.Drawing.Size(690, 434);
			textBoxProfileDetails.TabIndex = 22;
			mmLabel48.AutoSize = true;
			mmLabel48.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel48.IsFieldHeader = false;
			mmLabel48.IsRequired = false;
			mmLabel48.Location = new System.Drawing.Point(8, 4);
			mmLabel48.Name = "mmLabel48";
			mmLabel48.PenWidth = 1f;
			mmLabel48.ShowBorder = false;
			mmLabel48.Size = new System.Drawing.Size(66, 13);
			mmLabel48.TabIndex = 21;
			mmLabel48.Text = "Lead Profile:";
			ultraTabPageNote.Controls.Add(textBoxNote);
			ultraTabPageNote.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageNote.Name = "ultraTabPageNote";
			ultraTabPageNote.Size = new System.Drawing.Size(701, 457);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(10, 14);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(681, 430);
			textBoxNote.TabIndex = 42;
			ultraTabPageControl1.Controls.Add(udfEntryGrid);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(701, 457);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(8, 5);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(688, 440);
			udfEntryGrid.TabIndex = 1;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageContacts);
			ultraTabControl1.Controls.Add(ultraTabPageNote);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Controls.Add(ultraTabPageControl5);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 60);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(705, 480);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 1;
			appearance153.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance153;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = tabPageContacts;
			ultraTab3.Text = "Con&tacts";
			ultraTab4.TabPage = ultraTabPageControl2;
			ultraTab4.Text = "&Activities";
			ultraTab5.TabPage = ultraTabPageControl4;
			ultraTab5.Text = "Followup";
			ultraTab6.TabPage = ultraTabPageControl3;
			ultraTab6.Text = "&Opportunities";
			ultraTab7.TabPage = ultraTabPageControl5;
			ultraTab7.Text = "&Profile Details";
			ultraTab8.TabPage = ultraTabPageNote;
			ultraTab8.Text = "&Note";
			ultraTab9.TabPage = ultraTabPageControl1;
			ultraTab9.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[9]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8,
				ultraTab9
			});
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(701, 457);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 540);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(705, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(705, 1);
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
			buttonClose.Location = new System.Drawing.Point(595, 8);
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
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(705, 31);
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
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				converttocustomerToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			converttocustomerToolStripMenuItem.Name = "converttocustomerToolStripMenuItem";
			converttocustomerToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			converttocustomerToolStripMenuItem.Text = "Convert To Customer..";
			converttocustomerToolStripMenuItem.Click += new System.EventHandler(converttocustomerToolStripMenuItem_Click);
			panel1.Controls.Add(labelLeadNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(705, 29);
			panel1.TabIndex = 314;
			labelLeadNameHeader.AutoSize = true;
			labelLeadNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelLeadNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelLeadNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelLeadNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelLeadNameHeader.IsFieldHeader = false;
			labelLeadNameHeader.IsRequired = true;
			labelLeadNameHeader.Location = new System.Drawing.Point(24, 7);
			labelLeadNameHeader.Name = "labelLeadNameHeader";
			labelLeadNameHeader.PenWidth = 1f;
			labelLeadNameHeader.ShowBorder = false;
			labelLeadNameHeader.Size = new System.Drawing.Size(0, 13);
			labelLeadNameHeader.TabIndex = 1;
			labelLeadNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			contextMenuStripContact.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				openContactToolStripMenuItem
			});
			contextMenuStripContact.Name = "contextMenuStripContact";
			contextMenuStripContact.Size = new System.Drawing.Size(158, 26);
			openContactToolStripMenuItem.Name = "openContactToolStripMenuItem";
			openContactToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			openContactToolStripMenuItem.Text = "Open Contact...";
			openContactToolStripMenuItem.Click += new System.EventHandler(openContactToolStripMenuItem_Click);
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
			base.ClientSize = new System.Drawing.Size(705, 580);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "LeadDetailsForm";
			Text = "Lead Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(LeadClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(LeadDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxStage).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadOwner).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIndustry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSource).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadStatus).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRating).EndInit();
			tabPageContacts.ResumeLayout(false);
			tabPageContacts.PerformLayout();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxActivityPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridActivities).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridOpportunities).EndInit();
			ultraTabPageControl5.ResumeLayout(false);
			ultraTabPageControl5.PerformLayout();
			ultraTabPageNote.ResumeLayout(false);
			ultraTabPageNote.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
			if (AllowFollowUp)
			{
				ultraTabControl1.Tabs[4].Visible = true;
			}
			else
			{
				ultraTabControl1.Tabs[4].Visible = false;
			}
		}

		private void AddEvents()
		{
			dataGridContacts.AfterCellUpdate += dataGridContacts_AfterCellUpdate;
			dataGridContacts.BeforeCellUpdate += dataGridContacts_BeforeCellUpdate;
			dataGridContacts.ClickCellButton += dataGridContacts_ClickCellButton;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridContacts.Error += dataGridContacts_Error;
			textBoxName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			dataGridActivities.DoubleClick += dataGridActivities_DoubleClick;
			dataGridOpportunities.DoubleClick += dataGridOpportunities_DoubleClick;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
			base.Load += LeadDetailsForm_Load;
		}

		private void dataGridActivities_DoubleClick(object sender, EventArgs e)
		{
			string voucherID = dataGridActivities.ActiveRow.Cells["VoucherID"].Value.ToString();
			string sysDocID = dataGridActivities.ActiveRow.Cells["SysDocID"].Value.ToString();
			new FormHelper().EditTransaction(sysDocID, voucherID);
		}

		private void dataGridOpportunities_DoubleClick(object sender, EventArgs e)
		{
			string text = dataGridOpportunities.ActiveRow.Cells["Code"].Value.ToString();
			FormActivator.BringFormToFront(FormActivator.OpportunityDetailsFormObj);
			if (text != "")
			{
				FormActivator.OpportunityDetailsFormObj.LoadData(text);
			}
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void dataGridContacts_Error(object sender, ErrorEventArgs e)
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

		private void EventHelper_LeadAddressChanged(object sender, EventArgs e)
		{
			DataSet dataSet = sender as DataSet;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				if (dataRow["LeadID"].ToString() == textBoxCode.Text && dataRow["AddressID"].ToString() == textBoxAddressID.Text && !isNewRecord)
				{
					FillAddressData(dataRow);
				}
			}
		}

		private void LeadDetailsForm_Load(object sender, EventArgs e)
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
					dataGridOpportunities.ApplyUIDesign();
					SetupOpportunityGrid();
					dataGridListFollowup.ApplyUIDesign();
					SetupFollowGrid();
					comboBoxFollowupPeriod.LoadData();
					ClearForm();
					comboBoxLeadStatus.LoadData();
					textBoxCode.Focus();
					Init();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupContactsGrid()
		{
			try
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
				AppearanceBase cellAppearance = dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].CellAppearance;
				Color color = dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].CellAppearance.BackColor = Color.WhiteSmoke;
				Color color3 = cellAppearance3.BackColor = color;
				Color color6 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color3);
				dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].CellActivation = Activation.AllowEdit;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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

		private void SetupOpportunityGrid()
		{
			try
			{
				dataGridOpportunities.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Code");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Status");
				dataTable.Columns.Add("Due Date", typeof(DateTime));
				dataGridOpportunities.DataSource = dataTable;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetHeaderName()
		{
			labelLeadNameHeader.Text = textBoxCode.Text + " - " + textBoxName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxName.Text.Trim() == "")
			{
				labelLeadNameHeader.Text = "";
			}
		}

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.LeadTable.Rows[0];
			textBoxCode.Text = dataRow["LeadID"].ToString();
			textBoxName.Text = dataRow["LeadName"].ToString();
			textBoxForeignName.Text = dataRow["ForeignName"].ToString();
			textBoxCompanyName.Text = dataRow["CompanyName"].ToString();
			textBoxFormalName.Text = dataRow["ShortName"].ToString();
			comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
			comboBoxArea.SelectedID = dataRow["AreaID"].ToString();
			comboBoxStage.SelectedID = dataRow["StageID"].ToString();
			checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
			textBoxNote.Text = dataRow["Note"].ToString();
			textBoxProfileDetails.WordMLText = dataRow["ProfileDetails"].ToString();
			textBoxExpectValue.Text = dataRow["ExpectValue"].ToString();
			comboBoxReason.SelectedID = dataRow["ReasonID"].ToString();
			textBoxRemarks.Text = dataRow["Remarks"].ToString();
			textBoxProfileDetails.EndUpdate();
			comboBoxSalesperson.SelectedID = dataRow["SalesPersonID"].ToString();
			if (dataRow["LeadSourceID"] != DBNull.Value)
			{
				comboBoxSource.SelectedID = dataRow["LeadSourceID"].ToString();
			}
			else
			{
				comboBoxSource.Clear();
			}
			if (dataRow["LeadOwnerID"] != DBNull.Value)
			{
				comboBoxLeadOwner.SelectedID = dataRow["LeadOwnerID"].ToString();
			}
			else
			{
				comboBoxLeadOwner.Clear();
			}
			if (dataRow["IndustryID"] != DBNull.Value)
			{
				comboBoxIndustry.SelectedID = dataRow["IndustryID"].ToString();
			}
			else
			{
				comboBoxIndustry.Clear();
			}
			if (dataRow["LeadStatus"] != DBNull.Value)
			{
				comboBoxLeadStatus.SelectedID = dataRow["LeadStatus"].ToString();
			}
			else
			{
				comboBoxLeadStatus.Clear();
			}
			textBoxReferredBy.Text = dataRow["ReferredBy"].ToString();
			textBoxEmployeeNumber.Text = dataRow["EmployeeCount"].ToString();
			if (dataRow["Rating"] != DBNull.Value)
			{
				comboBoxRating.SelectedIndex = int.Parse(dataRow["Rating"].ToString());
			}
			else
			{
				comboBoxRating.SelectedIndex = -1;
			}
			if (dataRow["IsLeadSince"] != DBNull.Value)
			{
				dateTimePickerLeadSince.Value = DateTime.Parse(dataRow["IsLeadSince"].ToString());
				dateTimePickerLeadSince.Checked = true;
			}
			else
			{
				dateTimePickerLeadSince.IsNull = true;
				dateTimePickerLeadSince.Checked = false;
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
			comboBoxFollowupPeriod.SelectedIndex = 13;
			SetHeaderName();
			if (!currentData.Tables.Contains("Lead_Address") || currentData.LeadAddressTable.Rows.Count == 0)
			{
				return;
			}
			dataRow = currentData.LeadAddressTable.Rows[0];
			FillAddressData(dataRow);
			if (currentData.Tables.Contains("Lead_Contact_Detail") && currentData.LeadAddressTable.Rows.Count != 0)
			{
				DataTable dataTable = dataGridContacts.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.Tables["Lead_Contact_Detail"].Rows)
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
						_ = column2;
					}
				}
				else
				{
					udfEntryGrid.ClearData();
				}
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
					currentData = new LeadData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.LeadTable.Rows[0] : currentData.LeadTable.NewRow();
				dataRow.BeginEdit();
				dataRow["LeadID"] = textBoxCode.Text;
				dataRow["LeadName"] = textBoxName.Text;
				dataRow["ForeignName"] = textBoxForeignName.Text;
				dataRow["CompanyName"] = textBoxCompanyName.Text;
				dataRow["ShortName"] = textBoxFormalName.Text;
				dataRow["ProfileDetails"] = textBoxProfileDetails.WordMLText;
				if (textBoxExpectValue.Text != "")
				{
					dataRow["ExpectValue"] = textBoxExpectValue.Text;
				}
				else
				{
					dataRow["ExpectValue"] = 0;
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
				if (comboBoxStage.SelectedID != "")
				{
					dataRow["StageID"] = comboBoxStage.SelectedID;
				}
				else
				{
					dataRow["StageID"] = DBNull.Value;
				}
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow["Note"] = textBoxNote.Text;
				if (comboBoxReason.SelectedID != "")
				{
					dataRow["ReasonID"] = comboBoxReason.SelectedID;
				}
				else
				{
					dataRow["ReasonID"] = DBNull.Value;
				}
				dataRow["Remarks"] = textBoxRemarks.Text;
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalesPersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalesPersonID"] = DBNull.Value;
				}
				if (comboBoxSource.SelectedID != "")
				{
					dataRow["LeadSourceID"] = comboBoxSource.SelectedID;
				}
				else
				{
					dataRow["LeadSourceID"] = DBNull.Value;
				}
				if (comboBoxIndustry.SelectedID != "")
				{
					dataRow["IndustryID"] = comboBoxIndustry.SelectedID;
				}
				else
				{
					dataRow["IndustryID"] = DBNull.Value;
				}
				if (comboBoxLeadStatus.SelectedID != "")
				{
					dataRow["LeadStatus"] = comboBoxLeadStatus.SelectedID;
				}
				else
				{
					dataRow["LeadStatus"] = DBNull.Value;
				}
				if (comboBoxLeadOwner.SelectedID != "")
				{
					dataRow["LeadOwnerID"] = comboBoxLeadOwner.SelectedID;
				}
				else
				{
					dataRow["LeadOwnerID"] = DBNull.Value;
				}
				if (textBoxEmployeeNumber.Text != "")
				{
					dataRow["EmployeeCount"] = textBoxEmployeeNumber.Text;
				}
				else
				{
					dataRow["EmployeeCount"] = DBNull.Value;
				}
				if (textBoxReferredBy.Text != "")
				{
					dataRow["ReferredBy"] = textBoxReferredBy.Text;
				}
				else
				{
					dataRow["ReferredBy"] = DBNull.Value;
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
				if (dateTimePickerLeadSince.Checked)
				{
					dataRow["IsLeadSince"] = dateTimePickerLeadSince.Value;
				}
				else
				{
					dataRow["IsLeadSince"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.LeadTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.LeadAddressTable.Rows[0] : currentData.LeadAddressTable.NewRow());
				dataRow.BeginEdit();
				dataRow["LeadID"] = textBoxCode.Text;
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
					currentData.LeadAddressTable.Rows.Add(dataRow);
				}
				currentData.LeadContactTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridContacts.Rows)
				{
					dataRow = currentData.LeadContactTable.NewRow();
					dataRow.BeginEdit();
					if (!(row.Cells["ContactID"].Value.ToString() == ""))
					{
						dataRow["LeadID"] = textBoxCode.Text;
						dataRow["ContactID"] = row.Cells["ContactID"].Value.ToString();
						dataRow["Note"] = row.Cells["Note"].Value.ToString();
						dataRow["JobTitle"] = row.Cells["JobTitle"].Value.ToString();
						dataRow["RowIndex"] = row.Index;
						dataRow.EndEdit();
						currentData.LeadContactTable.Rows.Add(dataRow);
					}
				}
				currentData.LeadActivityTable.Rows.Clear();
				foreach (UltraGridRow row2 in dataGridActivities.Rows)
				{
					dataRow = currentData.LeadActivityTable.NewRow();
					dataRow.BeginEdit();
					if (!(row2.Cells["VoucherID"].Value.ToString() == ""))
					{
						dataRow["LeadID"] = textBoxCode.Text;
						dataRow["SysDocID"] = row2.Cells["SysDocID"].Value.ToString();
						dataRow["VoucherID"] = row2.Cells["VoucherID"].Value.ToString();
						dataRow["RowIndex"] = row2.Index;
						dataRow.EndEdit();
						currentData.LeadActivityTable.Rows.Add(dataRow);
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
				if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Lead", "LeadID", textBoxCode.Text))
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
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Lead", "LeadID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
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
					flag = Factory.LeadSystem.CreateLead(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Lead, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.LeadSystem.UpdateLead(currentData);
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
						currentData = Factory.LeadSystem.GetLeadByID(id);
						if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
						{
							ClearForm();
							IsNewRecord = true;
							textBoxCode.Text = id;
							textBoxCode.Focus();
						}
						else
						{
							IsNewRecord = false;
							FillData();
							LoadActivities();
							LoadOpportunities();
							formManager.ResetDirty();
						}
					}
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isLoading = false;
				PublicFunctions.EndWaiting(this);
			}
		}

		private void LoadActivities()
		{
			try
			{
				if (!isNewRecord)
				{
					activityData = Factory.ActivitySystem.GetActivityListByLeadID(CRMRelatedTypes.Lead, textBoxCode.Text, comboBoxActivityPeriod.FromDate, comboBoxActivityPeriod.ToDate);
					DataTable dataTable = dataGridActivities.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in activityData.Tables["Activity"].Rows)
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

		private void LoadOpportunities()
		{
			try
			{
				if (!isNewRecord)
				{
					DataSet opportunityListByLeadID = Factory.OpportunitySystem.GetOpportunityListByLeadID(CRMRelatedTypes.Lead, textBoxCode.Text, includeClosed: false);
					DataTable dataTable = dataGridOpportunities.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in opportunityListByLeadID.Tables["Opportunity"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["Code"] = row["Opportunity Code"];
						dataRow2["Name"] = row["Opportunity Name"];
						dataRow2["Status"] = row["Status"];
						dataRow2["Due Date"] = row["Due Date"];
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
			LoadData(DatabaseHelper.GetPreviousID("Lead", "LeadID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Lead", "LeadID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Lead", "LeadID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Lead", "LeadID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Lead", "LeadID", toolStripTextBoxFind.Text.Trim()))
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
				return Factory.LeadSystem.DeleteLead(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			dateTimePickerEstablished.Clear();
			dateTimePickerLeadSince.Clear();
			comboBoxRating.SelectedIndex = -1;
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Lead", "LeadID");
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxCompanyName.Clear();
			textBoxAddress1.Clear();
			textBoxAddress2.Clear();
			textBoxAddress3.Clear();
			textBoxAddressID.Text = "PRIMARY";
			comboBoxSalesperson.Clear();
			textBoxEmployeeNumber.Text = "0";
			comboBoxLeadStatus.Clear();
			comboBoxIndustry.Clear();
			comboBoxSource.Clear();
			comboBoxLeadOwner.Clear();
			comboBoxStage.Clear();
			textBoxReferredBy.Clear();
			textBoxAddressPrintFormat.Clear();
			udfEntryGrid.ClearData();
			textBoxCity.Clear();
			textBoxComment.Clear();
			textBoxContactName.Clear();
			textBoxCountry.Clear();
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
			textBoxProfileDetails.ResetText();
			textBoxExpectValue.Clear();
			comboBoxArea.Clear();
			comboBoxCountry.Clear();
			textBoxExpectValue.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxRemarks.Clear();
			comboBoxReason.Clear();
			(dataGridListFollowup.DataSource as DataTable).Rows.Clear();
			(dataGridContacts.DataSource as DataTable).Rows.Clear();
			(dataGridActivities.DataSource as DataTable).Rows.Clear();
			(dataGridOpportunities.DataSource as DataTable).Rows.Clear();
			IsNewRecord = true;
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

		private void LeadClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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
			new FormHelper().EditGenericList(GenericListTypes.LeadSource, comboBoxSource.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.Industry, comboBoxIndustry.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.Text);
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditArea(comboBoxArea.Text);
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
					DataSet leadProfileReport = Factory.LeadSystem.GetLeadProfileReport(textBoxCode.Text, textBoxCode.Text, showInactive: true);
					if (leadProfileReport == null || leadProfileReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(leadProfileReport, "", "Lead Profile", SysDocTypes.None, isPrint, showPrintDialog);
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
			FormActivator.BringFormToFront(FormActivator.LeadListFormObj);
		}

		private void buttonCategories_Click(object sender, EventArgs e)
		{
			EntityCategoryAssignDialog entityCategoryAssignDialog = new EntityCategoryAssignDialog();
			entityCategoryAssignDialog.EntityID = textBoxCode.Text;
			entityCategoryAssignDialog.EntityName = textBoxName.Text;
			entityCategoryAssignDialog.EntityType = EntityTypesEnum.Leads;
			if (!screenRight.Edit)
			{
				entityCategoryAssignDialog.AllowEdit = false;
			}
			entityCategoryAssignDialog.ShowDialog(this);
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().EditLeadAddress(textBoxCode.Text, textBoxAddressID.Text);
		}

		private void openContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridContacts.ActiveRow != null && dataGridContacts.ActiveRow.Cells["ContactID"].Value != null && !(dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString() == ""))
			{
				string id = dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString();
				new FormHelper().EditContact(id);
			}
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
					docManagementForm.EntityType = EntityTypesEnum.Leads;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxLeadOwner.Text);
		}

		private void comboBoxActivityPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadActivities();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
				FormActivator.ActivityDetailsFormObj.AddNewActivity(CRMRelatedTypes.Lead, textBoxCode.Text);
			}
		}

		private void converttocustomerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerDetailsFormObj);
			FormActivator.CustomerDetailsFormObj.SourceLeadID = textBoxCode.Text;
			FormActivator.CustomerDetailsFormObj.LoadLeadData();
		}

		private void buttonAddFollowup_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				comboBoxFollowupPeriod.SelectedIndex = 13;
				LoadFollowUp();
				DateTime dateTime = DateTime.Now;
				string thisfollowupBy = "";
				int result = 0;
				if (dataGridListFollowup.Rows.Count > 0)
				{
					int index = checked(dataGridListFollowup.Rows.Count - 1);
					dateTime = DateTime.Parse(dataGridListFollowup.Rows[index].Cells["NextFollowupDate"].Value.ToString());
					dataGridListFollowup.Rows[index].Cells["NextFollowupBy"].Value.ToString();
					thisfollowupBy = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
					thisfollowupBy = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
					int.TryParse(dataGridListFollowup.Rows[index].Cells["StatusID"].Value.ToString(), out result);
				}
				FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
				FormActivator.FollowupDetailsFormObj.SourceSysDocID = "";
				FormActivator.FollowupDetailsFormObj.SourceVoucherID = textBoxCode.Text;
				FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.Lead;
				FormActivator.FollowupDetailsFormObj.ThisfollowupBy = thisfollowupBy;
				FormActivator.FollowupDetailsFormObj.ThisfollowupDate = dateTime;
				FormActivator.FollowupDetailsFormObj.ThisfollowupTime = dateTime;
				FormActivator.FollowupDetailsFormObj.Status = result;
				FormActivator.FollowupDetailsFormObj.ActiveNewRecord();
			}
		}

		private void SetupFollowGrid()
		{
			try
			{
				dataGridListFollowup.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("FollowupID");
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID");
				dataTable.Columns.Add("FollowupDate", typeof(DateTime));
				dataTable.Columns.Add("FollowupBy");
				dataTable.Columns.Add("NextFollowupDate", typeof(DateTime));
				dataTable.Columns.Add("NextFollowupBy");
				dataTable.Columns.Add("NextFollowupByID");
				dataTable.Columns.Add("Status");
				dataTable.Columns.Add("StatusID");
				dataGridListFollowup.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollowupByID"];
				UltraGridColumn ultraGridColumn2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["FollowupID"];
				UltraGridColumn ultraGridColumn3 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceSysDocID"];
				bool flag2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = true;
				bool flag4 = ultraGridColumn3.Hidden = flag2;
				bool hidden = ultraGridColumn2.Hidden = flag4;
				ultraGridColumn.Hidden = hidden;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadFollowUp()
		{
			try
			{
				if (!isNewRecord)
				{
					followUpData = Factory.FollowupSystem.GetFollowupListByActivityID(CRMRelatedTypes.Followup, textBoxCode.Text, textBoxCode.Text, "", comboBoxFollowupPeriod.FromDate, comboBoxFollowupPeriod.ToDate);
					DataTable dataTable = dataGridListFollowup.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in followUpData.Tables[0].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["FollowupID"] = row["FollowupID"];
						dataRow2["SourceSysDocID"] = row["SourceSysDocID"];
						dataRow2["SourceVoucherID"] = row["SourceVoucherID"];
						dataRow2["FollowupDate"] = row["ThisFollowupDate"];
						dataRow2["FollowupBy"] = row["ThisFollowupByID"];
						dataRow2["NextFollowupDate"] = row["NextFollowupDate"];
						dataRow2["NextFollowupBy"] = row["NextFollowupBy"];
						dataRow2["Status"] = row["Status"];
						dataRow2["StatusID"] = row["ThisFollowupStatusID"];
						dataRow2["NextFollowupByID"] = row["NextFollowupByID"];
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

		private void comboBoxFollowupPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadFollowUp();
		}

		private void textBoxProfileDetails_ContentChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLeadStatus(comboBoxLeadStatus.SelectedID);
		}

		private void buttonMoreAddress_Click_1(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxCountry.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.Stage, comboBoxStage.SelectedID);
		}
	}
}
