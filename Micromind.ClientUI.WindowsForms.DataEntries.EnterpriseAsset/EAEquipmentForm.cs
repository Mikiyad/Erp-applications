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

namespace Micromind.ClientUI.WindowsForms.DataEntries.EnterpriseAsset
{
	public class EAEquipmentForm : Form, IDataForm, IDataEntry
	{
		private EAEquipmentData currentData;

		private const string TABLENAME_CONST = "EA_Equipment";

		private const string IDFIELD_CONST = "EquipmentID";

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

		private FormManager formManager;

		private MMTextBox textBoxRegistrationNo;

		private MMLabel mmLabel6;

		private CheckBox checkBoxHold;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private Panel panel1;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel8;

		private MMTextBox textBoxModel;

		private MMLabel mmLabel20;

		private MMTextBox textBoxMainInCharge;

		private MMLabel mmLabel17;

		private MMTextBox textBoxFixedAssetID;

		private MMTextBox textBoxEngineNumber;

		private MMLabel mmLabel14;

		private MMTextBox textBoxTrackingID;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCapacity;

		private MMLabel mmLabel10;

		private MMLabel mmLabel9;

		private MMTextBox textBoxColor;

		private MMTextBox textBoxNotificationMail;

		private MMLabel mmLabel21;

		private MMLabel mmLabel22;

		private MMTextBox textBoxSerailH;

		private UltraTabPageControl tabPageUserDefined;

		private vendorsFlatComboBox comboBoxVendor;

		private UltraFormattedLinkLabel linkLabelVendorClass;

		private UltraFormattedLinkLabel linkLabelArea;

		private UltraFormattedLinkLabel linkLabelCountry;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel labelCustomerNameHeader;

		private UDFEntryControl udfEntryGrid;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private ContextMenuStrip contextMenuStripContact;

		private ToolStripMenuItem openContactToolStripMenuItem;

		private ToolStripMenuItem newContactToolStripMenuItem;

		private ToolStripMenuItem deleteContactToolStripMenuItem;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel labelCurrency;

		private MMSDateTimePicker dateTimePickerExpiry;

		private MMLabel mmLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMLabel mmLabel24;

		private MMLabel mmLabel92;

		private ComboBox comboBoxOwnership;

		private MMLabel mmLabel27;

		private MMTextBox textBoxPower;

		private ComboBox comboBoxCapacity;

		private ComboBox comboBoxFuel;

		private MMLabel mmLabel25;

		private UltraGroupBox ultraGroupBox6;

		private XPButton xpButton1;

		private MMLabel mmLabel11;

		private MMTextBox mmTextBox3;

		private UltraGroupBox ultraGroupBox5;

		private RadioButton radioButtonMileage;

		private RadioButton radioButtonMeter;

		private RadioButton radioButtonHours;

		private MMLabel mmLabel32;

		private MMLabel mmLabel31;

		private MMTextBox textBoxPlateH;

		private JobComboBox comboBoxJob;

		private EquipmentTypeComboBox comboBoxEquipmentType;

		private EquipmentCategoryComboBox comboBoxEquipmentCategory;

		private FixedAssetGroupComboBox comboBoxFixedAssetGroup;

		private ComboBox comboBoxYear;

		private EAEquipmentComboBox comboBoxEquipment;

		private WorkLocationComboBox comboBoxWorkLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private EmployeeComboBox comboBoxEmployee;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		public ScreenAreas ScreenArea => ScreenAreas.EnterpriseAsset;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
				}
				toolStripButtonAttach.Enabled = !value;
				ToolStripButton toolStripButton = toolStripButtonPrint;
				bool enabled = toolStripButtonPreview.Enabled = !isNewRecord;
				toolStripButton.Enabled = enabled;
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

		public EAEquipmentForm()
		{
			InitializeComponent();
			AddEvents();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.EnterpriseAsset.EAEquipmentForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxWorkLocation = new Micromind.DataControls.WorkLocationComboBox();
			comboBoxEquipment = new Micromind.DataControls.EAEquipmentComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxEquipmentType = new Micromind.DataControls.EquipmentTypeComboBox();
			comboBoxEquipmentCategory = new Micromind.DataControls.EquipmentCategoryComboBox();
			ultraGroupBox6 = new Infragistics.Win.Misc.UltraGroupBox();
			xpButton1 = new Micromind.UISupport.XPButton();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			mmTextBox3 = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			mmLabel92 = new Micromind.UISupport.MMLabel();
			comboBoxOwnership = new System.Windows.Forms.ComboBox();
			dateTimePickerExpiry = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelArea = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			linkLabelVendorClass = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxYear = new System.Windows.Forms.ComboBox();
			comboBoxFixedAssetGroup = new Micromind.DataControls.FixedAssetGroupComboBox();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			radioButtonMileage = new System.Windows.Forms.RadioButton();
			radioButtonMeter = new System.Windows.Forms.RadioButton();
			radioButtonHours = new System.Windows.Forms.RadioButton();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			textBoxPlateH = new Micromind.UISupport.MMTextBox();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			textBoxPower = new Micromind.UISupport.MMTextBox();
			comboBoxCapacity = new System.Windows.Forms.ComboBox();
			comboBoxFuel = new System.Windows.Forms.ComboBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxSerailH = new Micromind.UISupport.MMTextBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxNotificationMail = new Micromind.UISupport.MMTextBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxMainInCharge = new Micromind.UISupport.MMTextBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxFixedAssetID = new Micromind.UISupport.MMTextBox();
			textBoxEngineNumber = new Micromind.UISupport.MMTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxTrackingID = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCapacity = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxColor = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxModel = new Micromind.UISupport.MMTextBox();
			checkBoxHold = new System.Windows.Forms.CheckBox();
			labelVendorNumber = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxRegistrationNo = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
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
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxWorkLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox6).BeginInit();
			ultraGroupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFixedAssetGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStripContact.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(comboBoxWorkLocation);
			tabPageGeneral.Controls.Add(comboBoxEquipment);
			tabPageGeneral.Controls.Add(comboBoxJob);
			tabPageGeneral.Controls.Add(comboBoxEquipmentType);
			tabPageGeneral.Controls.Add(comboBoxEquipmentCategory);
			tabPageGeneral.Controls.Add(ultraGroupBox6);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel1);
			tabPageGeneral.Controls.Add(mmLabel24);
			tabPageGeneral.Controls.Add(mmLabel92);
			tabPageGeneral.Controls.Add(comboBoxOwnership);
			tabPageGeneral.Controls.Add(dateTimePickerExpiry);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(labelCurrency);
			tabPageGeneral.Controls.Add(linkLabelArea);
			tabPageGeneral.Controls.Add(linkLabelCountry);
			tabPageGeneral.Controls.Add(comboBoxVendor);
			tabPageGeneral.Controls.Add(linkLabelVendorClass);
			tabPageGeneral.Controls.Add(ultraGroupBox1);
			tabPageGeneral.Controls.Add(checkBoxHold);
			tabPageGeneral.Controls.Add(labelVendorNumber);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxRegistrationNo);
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(731, 477);
			tabPageGeneral.Paint += new System.Windows.Forms.PaintEventHandler(tabPageGeneral_Paint);
			comboBoxWorkLocation.Assigned = false;
			comboBoxWorkLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxWorkLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxWorkLocation.CustomReportFieldName = "";
			comboBoxWorkLocation.CustomReportKey = "";
			comboBoxWorkLocation.CustomReportValueType = 1;
			comboBoxWorkLocation.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxWorkLocation.DisplayLayout.Appearance = appearance;
			comboBoxWorkLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxWorkLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxWorkLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxWorkLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxWorkLocation.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxWorkLocation.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxWorkLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxWorkLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxWorkLocation.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxWorkLocation.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxWorkLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxWorkLocation.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxWorkLocation.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxWorkLocation.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxWorkLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxWorkLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxWorkLocation.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxWorkLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxWorkLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxWorkLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxWorkLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxWorkLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxWorkLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxWorkLocation.Editable = true;
			comboBoxWorkLocation.FilterString = "";
			comboBoxWorkLocation.HasAllAccount = false;
			comboBoxWorkLocation.HasCustom = false;
			comboBoxWorkLocation.IsDataLoaded = false;
			comboBoxWorkLocation.Location = new System.Drawing.Point(132, 97);
			comboBoxWorkLocation.MaxDropDownItems = 12;
			comboBoxWorkLocation.Name = "comboBoxWorkLocation";
			comboBoxWorkLocation.ShowAll = false;
			comboBoxWorkLocation.ShowConsignIn = false;
			comboBoxWorkLocation.ShowConsignOut = false;
			comboBoxWorkLocation.ShowInactiveItems = false;
			comboBoxWorkLocation.ShowNormalLocations = true;
			comboBoxWorkLocation.ShowPOSOnly = false;
			comboBoxWorkLocation.ShowQuickAdd = true;
			comboBoxWorkLocation.ShowWarehouseOnly = false;
			comboBoxWorkLocation.Size = new System.Drawing.Size(195, 20);
			comboBoxWorkLocation.TabIndex = 10;
			comboBoxWorkLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipment.Assigned = false;
			comboBoxEquipment.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEquipment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEquipment.CustomReportFieldName = "";
			comboBoxEquipment.CustomReportKey = "";
			comboBoxEquipment.CustomReportValueType = 1;
			comboBoxEquipment.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipment.DisplayLayout.Appearance = appearance13;
			comboBoxEquipment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxEquipment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipment.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxEquipment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipment.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipment.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxEquipment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipment.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxEquipment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipment.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxEquipment.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxEquipment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipment.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxEquipment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipment.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxEquipment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipment.Editable = true;
			comboBoxEquipment.FilterString = "";
			comboBoxEquipment.HasAllAccount = false;
			comboBoxEquipment.HasCustom = false;
			comboBoxEquipment.IsDataLoaded = false;
			comboBoxEquipment.Location = new System.Drawing.Point(419, 99);
			comboBoxEquipment.MaxDropDownItems = 12;
			comboBoxEquipment.Name = "comboBoxEquipment";
			comboBoxEquipment.ShowInactiveItems = false;
			comboBoxEquipment.ShowQuickAdd = true;
			comboBoxEquipment.Size = new System.Drawing.Size(183, 20);
			comboBoxEquipment.TabIndex = 11;
			comboBoxEquipment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxJob.DisplayLayout.Appearance = appearance25;
			comboBoxJob.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxJob.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJob.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxJob.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJob.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxJob.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxJob.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxJob.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxJob.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxJob.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxJob.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxJob.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxJob.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxJob.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxJob.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxJob.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxJob.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxJob.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxJob.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxJob.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxJob.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxJob.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxJob.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(132, 75);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(195, 20);
			comboBoxJob.TabIndex = 8;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipmentType.Assigned = false;
			comboBoxEquipmentType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEquipmentType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEquipmentType.CustomReportFieldName = "";
			comboBoxEquipmentType.CustomReportKey = "";
			comboBoxEquipmentType.CustomReportValueType = 1;
			comboBoxEquipmentType.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipmentType.DisplayLayout.Appearance = appearance37;
			comboBoxEquipmentType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipmentType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxEquipmentType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipmentType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipmentType.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipmentType.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxEquipmentType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipmentType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipmentType.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxEquipmentType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipmentType.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxEquipmentType.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxEquipmentType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipmentType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipmentType.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxEquipmentType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipmentType.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxEquipmentType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipmentType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipmentType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipmentType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipmentType.Editable = true;
			comboBoxEquipmentType.FilterString = "";
			comboBoxEquipmentType.HasAllAccount = false;
			comboBoxEquipmentType.HasCustom = false;
			comboBoxEquipmentType.IsDataLoaded = false;
			comboBoxEquipmentType.Location = new System.Drawing.Point(577, 27);
			comboBoxEquipmentType.MaxDropDownItems = 12;
			comboBoxEquipmentType.Name = "comboBoxEquipmentType";
			comboBoxEquipmentType.ShowInactiveItems = false;
			comboBoxEquipmentType.ShowQuickAdd = true;
			comboBoxEquipmentType.Size = new System.Drawing.Size(133, 20);
			comboBoxEquipmentType.TabIndex = 4;
			comboBoxEquipmentType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipmentCategory.Assigned = false;
			comboBoxEquipmentCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEquipmentCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEquipmentCategory.CustomReportFieldName = "";
			comboBoxEquipmentCategory.CustomReportKey = "";
			comboBoxEquipmentCategory.CustomReportValueType = 1;
			comboBoxEquipmentCategory.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipmentCategory.DisplayLayout.Appearance = appearance49;
			comboBoxEquipmentCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipmentCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxEquipmentCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipmentCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipmentCategory.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipmentCategory.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxEquipmentCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipmentCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentCategory.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipmentCategory.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxEquipmentCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipmentCategory.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentCategory.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxEquipmentCategory.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxEquipmentCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipmentCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipmentCategory.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxEquipmentCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipmentCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxEquipmentCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipmentCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipmentCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipmentCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipmentCategory.Editable = true;
			comboBoxEquipmentCategory.FilterString = "";
			comboBoxEquipmentCategory.HasAllAccount = false;
			comboBoxEquipmentCategory.HasCustom = false;
			comboBoxEquipmentCategory.IsDataLoaded = false;
			comboBoxEquipmentCategory.Location = new System.Drawing.Point(419, 30);
			comboBoxEquipmentCategory.MaxDropDownItems = 12;
			comboBoxEquipmentCategory.Name = "comboBoxEquipmentCategory";
			comboBoxEquipmentCategory.ShowAll = false;
			comboBoxEquipmentCategory.ShowConsignIn = false;
			comboBoxEquipmentCategory.ShowConsignOut = false;
			comboBoxEquipmentCategory.ShowInactiveItems = false;
			comboBoxEquipmentCategory.ShowNormalLocations = true;
			comboBoxEquipmentCategory.ShowPOSOnly = false;
			comboBoxEquipmentCategory.ShowQuickAdd = true;
			comboBoxEquipmentCategory.ShowWarehouseOnly = false;
			comboBoxEquipmentCategory.Size = new System.Drawing.Size(114, 20);
			comboBoxEquipmentCategory.TabIndex = 3;
			comboBoxEquipmentCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox6.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox6.Controls.Add(xpButton1);
			ultraGroupBox6.Controls.Add(mmLabel11);
			ultraGroupBox6.Controls.Add(mmTextBox3);
			ultraGroupBox6.Location = new System.Drawing.Point(9, 123);
			ultraGroupBox6.Name = "ultraGroupBox6";
			ultraGroupBox6.Size = new System.Drawing.Size(698, 75);
			ultraGroupBox6.TabIndex = 12;
			ultraGroupBox6.Text = "Next Maintenance Schedule";
			ultraGroupBox6.Click += new System.EventHandler(ultraGroupBox6_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(351, 48);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(167, 24);
			xpButton1.TabIndex = 16;
			xpButton1.Text = "Schedule";
			xpButton1.UseVisualStyleBackColor = false;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(9, 26);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(79, 13);
			mmLabel11.TabIndex = 0;
			mmLabel11.Text = "Scheduled List:";
			mmTextBox3.BackColor = System.Drawing.Color.White;
			mmTextBox3.CustomReportFieldName = "";
			mmTextBox3.CustomReportKey = "";
			mmTextBox3.CustomReportValueType = 1;
			mmTextBox3.IsComboTextBox = false;
			mmTextBox3.Location = new System.Drawing.Point(122, 23);
			mmTextBox3.MaxLength = 30;
			mmTextBox3.Multiline = true;
			mmTextBox3.Name = "mmTextBox3";
			mmTextBox3.Size = new System.Drawing.Size(220, 49);
			mmTextBox3.TabIndex = 1;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(340, 78);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(49, 14);
			ultraFormattedLinkLabel1.TabIndex = 157;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Provider:";
			appearance61.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance61;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(340, 102);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(75, 13);
			mmLabel24.TabIndex = 156;
			mmLabel24.Text = "Parent EqpID:";
			mmLabel92.AutoSize = true;
			mmLabel92.BackColor = System.Drawing.Color.Transparent;
			mmLabel92.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel92.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel92.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel92.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel92.IsFieldHeader = false;
			mmLabel92.IsRequired = false;
			mmLabel92.Location = new System.Drawing.Point(539, 54);
			mmLabel92.Name = "mmLabel92";
			mmLabel92.PenWidth = 1f;
			mmLabel92.ShowBorder = false;
			mmLabel92.Size = new System.Drawing.Size(62, 13);
			mmLabel92.TabIndex = 154;
			mmLabel92.Text = "Ownership:";
			comboBoxOwnership.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxOwnership.FormattingEnabled = true;
			comboBoxOwnership.Items.AddRange(new object[2]
			{
				"Own",
				"Rent"
			});
			comboBoxOwnership.Location = new System.Drawing.Point(606, 50);
			comboBoxOwnership.Name = "comboBoxOwnership";
			comboBoxOwnership.Size = new System.Drawing.Size(104, 21);
			comboBoxOwnership.TabIndex = 7;
			dateTimePickerExpiry.Checked = false;
			dateTimePickerExpiry.CustomFormat = " ";
			dateTimePickerExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerExpiry.Location = new System.Drawing.Point(419, 53);
			dateTimePickerExpiry.Name = "dateTimePickerExpiry";
			dateTimePickerExpiry.ShowCheckBox = true;
			dateTimePickerExpiry.Size = new System.Drawing.Size(116, 20);
			dateTimePickerExpiry.TabIndex = 6;
			dateTimePickerExpiry.Value = new System.DateTime(0L);
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(340, 57);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(56, 13);
			mmLabel5.TabIndex = 151;
			mmLabel5.Text = "Exp.Date:";
			appearance62.FontData.BoldAsString = "False";
			appearance62.FontData.Name = "Tahoma";
			labelCurrency.Appearance = appearance62;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(539, 30);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(32, 15);
			labelCurrency.TabIndex = 150;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Type:";
			appearance63.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance63;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelCurrency_LinkClicked);
			linkLabelArea.AutoSize = true;
			linkLabelArea.Location = new System.Drawing.Point(340, 33);
			linkLabelArea.Name = "linkLabelArea";
			linkLabelArea.Size = new System.Drawing.Size(52, 14);
			linkLabelArea.TabIndex = 19;
			linkLabelArea.TabStop = true;
			linkLabelArea.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelArea.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelArea.Value = "Category:";
			appearance64.ForeColor = System.Drawing.Color.Blue;
			linkLabelArea.VisitedLinkAppearance = appearance64;
			linkLabelArea.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelArea_LinkClicked);
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(18, 100);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(49, 14);
			linkLabelCountry.TabIndex = 9;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Location:";
			appearance65.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance65;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			appearance66.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance66;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance67.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance67;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance68;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance69.BackColor2 = System.Drawing.SystemColors.Control;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance69;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance70;
			appearance71.BackColor = System.Drawing.SystemColors.Highlight;
			appearance71.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance71;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance72;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			appearance73.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance73;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance74.BackColor = System.Drawing.SystemColors.Control;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance74;
			appearance75.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance75;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance76;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance77;
			comboBoxVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendor.Editable = true;
			comboBoxVendor.FilterString = "";
			comboBoxVendor.FilterSysDocID = "";
			comboBoxVendor.HasAll = false;
			comboBoxVendor.HasCustom = false;
			comboBoxVendor.IsDataLoaded = false;
			comboBoxVendor.Location = new System.Drawing.Point(419, 76);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.MaxLength = 64;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(183, 20);
			comboBoxVendor.TabIndex = 9;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelVendorClass.AutoSize = true;
			appearance78.ForeColor = System.Drawing.Color.Blue;
			linkLabelVendorClass.LinkAppearance = appearance78;
			linkLabelVendorClass.Location = new System.Drawing.Point(18, 78);
			linkLabelVendorClass.Name = "linkLabelVendorClass";
			linkLabelVendorClass.Size = new System.Drawing.Size(42, 14);
			linkLabelVendorClass.TabIndex = 7;
			linkLabelVendorClass.TabStop = true;
			linkLabelVendorClass.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVendorClass.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVendorClass.Value = "Project:";
			appearance79.ForeColor = System.Drawing.Color.Blue;
			linkLabelVendorClass.VisitedLinkAppearance = appearance79;
			linkLabelVendorClass.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel3);
			ultraGroupBox1.Controls.Add(comboBoxEmployee);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox1.Controls.Add(comboBoxYear);
			ultraGroupBox1.Controls.Add(comboBoxFixedAssetGroup);
			ultraGroupBox1.Controls.Add(ultraGroupBox5);
			ultraGroupBox1.Controls.Add(mmLabel32);
			ultraGroupBox1.Controls.Add(mmLabel31);
			ultraGroupBox1.Controls.Add(textBoxPlateH);
			ultraGroupBox1.Controls.Add(mmLabel27);
			ultraGroupBox1.Controls.Add(textBoxPower);
			ultraGroupBox1.Controls.Add(comboBoxCapacity);
			ultraGroupBox1.Controls.Add(comboBoxFuel);
			ultraGroupBox1.Controls.Add(mmLabel25);
			ultraGroupBox1.Controls.Add(mmLabel22);
			ultraGroupBox1.Controls.Add(textBoxSerailH);
			ultraGroupBox1.Controls.Add(mmLabel21);
			ultraGroupBox1.Controls.Add(textBoxNotificationMail);
			ultraGroupBox1.Controls.Add(mmLabel20);
			ultraGroupBox1.Controls.Add(textBoxMainInCharge);
			ultraGroupBox1.Controls.Add(mmLabel17);
			ultraGroupBox1.Controls.Add(textBoxFixedAssetID);
			ultraGroupBox1.Controls.Add(textBoxEngineNumber);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(textBoxTrackingID);
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(textBoxCapacity);
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(mmLabel9);
			ultraGroupBox1.Controls.Add(textBoxColor);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(textBoxModel);
			ultraGroupBox1.Location = new System.Drawing.Point(9, 204);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(706, 237);
			ultraGroupBox1.TabIndex = 14;
			ultraGroupBox1.Text = "Manufacturing Details";
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(9, 151);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(58, 14);
			ultraFormattedLinkLabel3.TabIndex = 158;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Owned By:";
			appearance80.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance80;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance81;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(123, 149);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(221, 20);
			comboBoxEmployee.TabIndex = 17;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(367, 175);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(98, 14);
			ultraFormattedLinkLabel2.TabIndex = 163;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Fixed Asset Group:";
			appearance93.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance93;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked_1);
			comboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxYear.FormattingEnabled = true;
			comboBoxYear.Items.AddRange(new object[51]
			{
				"2000",
				"2001",
				"2002",
				"2003",
				"2004",
				"2005",
				"2006",
				"2007",
				"2008",
				"2009",
				"2010",
				"2011",
				"2012",
				"2013",
				"2014",
				"2015",
				"2016",
				"2017",
				"2018",
				"2019",
				"2020",
				"2021",
				"2022",
				"2023",
				"2024",
				"2025",
				"2026",
				"2027",
				"2028",
				"2029",
				"2030",
				"2031",
				"2032",
				"2033",
				"2034",
				"2035",
				"2036",
				"2037",
				"2038",
				"2039",
				"2040",
				"2041",
				"2042",
				"2043",
				"2044",
				"2045",
				"2046",
				"2047",
				"2048",
				"2049",
				"2050"
			});
			comboBoxYear.Location = new System.Drawing.Point(553, 20);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new System.Drawing.Size(116, 21);
			comboBoxYear.TabIndex = 3;
			comboBoxFixedAssetGroup.Assigned = false;
			comboBoxFixedAssetGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFixedAssetGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFixedAssetGroup.CustomReportFieldName = "";
			comboBoxFixedAssetGroup.CustomReportKey = "";
			comboBoxFixedAssetGroup.CustomReportValueType = 1;
			comboBoxFixedAssetGroup.DescriptionTextBox = null;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			appearance94.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFixedAssetGroup.DisplayLayout.Appearance = appearance94;
			comboBoxFixedAssetGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFixedAssetGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance95.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAssetGroup.DisplayLayout.GroupByBox.Appearance = appearance95;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFixedAssetGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance96;
			comboBoxFixedAssetGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance97.BackColor2 = System.Drawing.SystemColors.Control;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFixedAssetGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance97;
			comboBoxFixedAssetGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFixedAssetGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFixedAssetGroup.DisplayLayout.Override.ActiveCellAppearance = appearance98;
			appearance99.BackColor = System.Drawing.SystemColors.Highlight;
			appearance99.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFixedAssetGroup.DisplayLayout.Override.ActiveRowAppearance = appearance99;
			comboBoxFixedAssetGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFixedAssetGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance100.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAssetGroup.DisplayLayout.Override.CardAreaAppearance = appearance100;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			appearance101.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFixedAssetGroup.DisplayLayout.Override.CellAppearance = appearance101;
			comboBoxFixedAssetGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFixedAssetGroup.DisplayLayout.Override.CellPadding = 0;
			appearance102.BackColor = System.Drawing.SystemColors.Control;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAssetGroup.DisplayLayout.Override.GroupByRowAppearance = appearance102;
			appearance103.TextHAlignAsString = "Left";
			comboBoxFixedAssetGroup.DisplayLayout.Override.HeaderAppearance = appearance103;
			comboBoxFixedAssetGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFixedAssetGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			comboBoxFixedAssetGroup.DisplayLayout.Override.RowAppearance = appearance104;
			comboBoxFixedAssetGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance105.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFixedAssetGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance105;
			comboBoxFixedAssetGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFixedAssetGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFixedAssetGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFixedAssetGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFixedAssetGroup.Editable = true;
			comboBoxFixedAssetGroup.FilterString = "";
			comboBoxFixedAssetGroup.HasAllAccount = false;
			comboBoxFixedAssetGroup.HasCustom = false;
			comboBoxFixedAssetGroup.IsDataLoaded = false;
			comboBoxFixedAssetGroup.Location = new System.Drawing.Point(493, 173);
			comboBoxFixedAssetGroup.MaxDropDownItems = 12;
			comboBoxFixedAssetGroup.Name = "comboBoxFixedAssetGroup";
			comboBoxFixedAssetGroup.ShowInactiveItems = false;
			comboBoxFixedAssetGroup.ShowQuickAdd = true;
			comboBoxFixedAssetGroup.Size = new System.Drawing.Size(177, 20);
			comboBoxFixedAssetGroup.TabIndex = 19;
			comboBoxFixedAssetGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox5.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox5.Controls.Add(radioButtonMileage);
			ultraGroupBox5.Controls.Add(radioButtonMeter);
			ultraGroupBox5.Controls.Add(radioButtonHours);
			ultraGroupBox5.Location = new System.Drawing.Point(7, 80);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(455, 48);
			ultraGroupBox5.TabIndex = 12;
			ultraGroupBox5.Text = "Meter Tracking";
			radioButtonMileage.AutoSize = true;
			radioButtonMileage.Location = new System.Drawing.Point(107, 20);
			radioButtonMileage.Name = "radioButtonMileage";
			radioButtonMileage.Size = new System.Drawing.Size(62, 17);
			radioButtonMileage.TabIndex = 1;
			radioButtonMileage.Text = "Mileage";
			radioButtonMileage.UseVisualStyleBackColor = true;
			radioButtonMeter.AutoSize = true;
			radioButtonMeter.Checked = true;
			radioButtonMeter.Location = new System.Drawing.Point(5, 20);
			radioButtonMeter.Name = "radioButtonMeter";
			radioButtonMeter.Size = new System.Drawing.Size(52, 17);
			radioButtonMeter.TabIndex = 0;
			radioButtonMeter.TabStop = true;
			radioButtonMeter.Text = "Meter";
			radioButtonMeter.UseVisualStyleBackColor = true;
			radioButtonHours.AutoSize = true;
			radioButtonHours.Location = new System.Drawing.Point(213, 20);
			radioButtonHours.Name = "radioButtonHours";
			radioButtonHours.Size = new System.Drawing.Size(53, 17);
			radioButtonHours.TabIndex = 2;
			radioButtonHours.Text = "Hours";
			radioButtonHours.UseVisualStyleBackColor = true;
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(478, 136);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(51, 13);
			mmLabel32.TabIndex = 162;
			mmLabel32.Text = "Eng. NO:";
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(478, 92);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(53, 13);
			mmLabel31.TabIndex = 160;
			mmLabel31.Text = "Plate NO:";
			textBoxPlateH.BackColor = System.Drawing.Color.White;
			textBoxPlateH.CustomReportFieldName = "";
			textBoxPlateH.CustomReportKey = "";
			textBoxPlateH.CustomReportValueType = 1;
			textBoxPlateH.IsComboTextBox = false;
			textBoxPlateH.Location = new System.Drawing.Point(553, 88);
			textBoxPlateH.MaxLength = 30;
			textBoxPlateH.Name = "textBoxPlateH";
			textBoxPlateH.Size = new System.Drawing.Size(116, 20);
			textBoxPlateH.TabIndex = 9;
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(297, 46);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(41, 13);
			mmLabel27.TabIndex = 159;
			mmLabel27.Text = "Power:";
			textBoxPower.BackColor = System.Drawing.Color.White;
			textBoxPower.CustomReportFieldName = "";
			textBoxPower.CustomReportKey = "";
			textBoxPower.CustomReportValueType = 1;
			textBoxPower.IsComboTextBox = false;
			textBoxPower.Location = new System.Drawing.Point(350, 42);
			textBoxPower.MaxLength = 64;
			textBoxPower.Name = "textBoxPower";
			textBoxPower.Size = new System.Drawing.Size(116, 20);
			textBoxPower.TabIndex = 6;
			comboBoxCapacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCapacity.FormattingEnabled = true;
			comboBoxCapacity.Items.AddRange(new object[2]
			{
				"Seat",
				"Ton"
			});
			comboBoxCapacity.Location = new System.Drawing.Point(220, 41);
			comboBoxCapacity.Name = "comboBoxCapacity";
			comboBoxCapacity.Size = new System.Drawing.Size(53, 21);
			comboBoxCapacity.TabIndex = 5;
			comboBoxFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxFuel.FormattingEnabled = true;
			comboBoxFuel.Items.AddRange(new object[2]
			{
				"Petrol",
				"Diesel"
			});
			comboBoxFuel.Location = new System.Drawing.Point(553, 43);
			comboBoxFuel.Name = "comboBoxFuel";
			comboBoxFuel.Size = new System.Drawing.Size(116, 21);
			comboBoxFuel.TabIndex = 7;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(478, 24);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(33, 13);
			mmLabel25.TabIndex = 0;
			mmLabel25.Text = "Year:";
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(478, 71);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(55, 13);
			mmLabel22.TabIndex = 18;
			mmLabel22.Text = "Serial NO:";
			textBoxSerailH.BackColor = System.Drawing.Color.White;
			textBoxSerailH.CustomReportFieldName = "";
			textBoxSerailH.CustomReportKey = "";
			textBoxSerailH.CustomReportValueType = 1;
			textBoxSerailH.IsComboTextBox = false;
			textBoxSerailH.Location = new System.Drawing.Point(553, 66);
			textBoxSerailH.MaxLength = 30;
			textBoxSerailH.Name = "textBoxSerailH";
			textBoxSerailH.Size = new System.Drawing.Size(116, 20);
			textBoxSerailH.TabIndex = 8;
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(9, 197);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(96, 13);
			mmLabel21.TabIndex = 17;
			mmLabel21.Text = "Notification E-Mail:";
			textBoxNotificationMail.BackColor = System.Drawing.Color.White;
			textBoxNotificationMail.CustomReportFieldName = "";
			textBoxNotificationMail.CustomReportKey = "";
			textBoxNotificationMail.CustomReportValueType = 1;
			textBoxNotificationMail.IsComboTextBox = false;
			textBoxNotificationMail.Location = new System.Drawing.Point(123, 197);
			textBoxNotificationMail.MaxLength = 255;
			textBoxNotificationMail.Name = "textBoxNotificationMail";
			textBoxNotificationMail.Size = new System.Drawing.Size(221, 20);
			textBoxNotificationMail.TabIndex = 18;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(9, 173);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(85, 13);
			mmLabel20.TabIndex = 15;
			mmLabel20.Text = "Main.In Charge:";
			textBoxMainInCharge.BackColor = System.Drawing.Color.White;
			textBoxMainInCharge.CustomReportFieldName = "";
			textBoxMainInCharge.CustomReportKey = "";
			textBoxMainInCharge.CustomReportValueType = 1;
			textBoxMainInCharge.IsComboTextBox = false;
			textBoxMainInCharge.Location = new System.Drawing.Point(123, 173);
			textBoxMainInCharge.MaxLength = 30;
			textBoxMainInCharge.Name = "textBoxMainInCharge";
			textBoxMainInCharge.Size = new System.Drawing.Size(221, 20);
			textBoxMainInCharge.TabIndex = 16;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(367, 198);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(81, 13);
			mmLabel17.TabIndex = 26;
			mmLabel17.Text = "Fixed Asset ID:";
			textBoxFixedAssetID.BackColor = System.Drawing.Color.White;
			textBoxFixedAssetID.CustomReportFieldName = "";
			textBoxFixedAssetID.CustomReportKey = "";
			textBoxFixedAssetID.CustomReportValueType = 1;
			textBoxFixedAssetID.IsComboTextBox = false;
			textBoxFixedAssetID.Location = new System.Drawing.Point(493, 195);
			textBoxFixedAssetID.MaxLength = 30;
			textBoxFixedAssetID.Name = "textBoxFixedAssetID";
			textBoxFixedAssetID.Size = new System.Drawing.Size(177, 20);
			textBoxFixedAssetID.TabIndex = 20;
			textBoxEngineNumber.BackColor = System.Drawing.Color.White;
			textBoxEngineNumber.CustomReportFieldName = "";
			textBoxEngineNumber.CustomReportKey = "";
			textBoxEngineNumber.CustomReportValueType = 1;
			textBoxEngineNumber.IsComboTextBox = false;
			textBoxEngineNumber.Location = new System.Drawing.Point(553, 132);
			textBoxEngineNumber.MaxLength = 30;
			textBoxEngineNumber.Name = "textBoxEngineNumber";
			textBoxEngineNumber.Size = new System.Drawing.Size(116, 20);
			textBoxEngineNumber.TabIndex = 11;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(478, 116);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(65, 13);
			mmLabel14.TabIndex = 20;
			mmLabel14.Text = "Tracking ID:";
			textBoxTrackingID.BackColor = System.Drawing.Color.White;
			textBoxTrackingID.CustomReportFieldName = "";
			textBoxTrackingID.CustomReportKey = "";
			textBoxTrackingID.CustomReportValueType = 1;
			textBoxTrackingID.IsComboTextBox = false;
			textBoxTrackingID.Location = new System.Drawing.Point(553, 110);
			textBoxTrackingID.MaxLength = 30;
			textBoxTrackingID.Name = "textBoxTrackingID";
			textBoxTrackingID.Size = new System.Drawing.Size(116, 20);
			textBoxTrackingID.TabIndex = 10;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(9, 45);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(53, 13);
			mmLabel13.TabIndex = 8;
			mmLabel13.Text = "Capacity:";
			textBoxCapacity.BackColor = System.Drawing.Color.White;
			textBoxCapacity.CustomReportFieldName = "";
			textBoxCapacity.CustomReportKey = "";
			textBoxCapacity.CustomReportValueType = 1;
			textBoxCapacity.IsComboTextBox = false;
			textBoxCapacity.Location = new System.Drawing.Point(123, 42);
			textBoxCapacity.MaxLength = 64;
			textBoxCapacity.Name = "textBoxCapacity";
			textBoxCapacity.Size = new System.Drawing.Size(91, 20);
			textBoxCapacity.TabIndex = 4;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(478, 47);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(31, 13);
			mmLabel10.TabIndex = 4;
			mmLabel10.Text = "Fuel:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(296, 24);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(42, 13);
			mmLabel9.TabIndex = 0;
			mmLabel9.Text = "Colour:";
			textBoxColor.BackColor = System.Drawing.Color.White;
			textBoxColor.CustomReportFieldName = "";
			textBoxColor.CustomReportKey = "";
			textBoxColor.CustomReportValueType = 1;
			textBoxColor.IsComboTextBox = false;
			textBoxColor.Location = new System.Drawing.Point(350, 20);
			textBoxColor.MaxLength = 64;
			textBoxColor.Name = "textBoxColor";
			textBoxColor.Size = new System.Drawing.Size(116, 20);
			textBoxColor.TabIndex = 2;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 23);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(39, 13);
			mmLabel8.TabIndex = 0;
			mmLabel8.Text = "Model:";
			textBoxModel.BackColor = System.Drawing.Color.White;
			textBoxModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxModel.CustomReportFieldName = "";
			textBoxModel.CustomReportKey = "";
			textBoxModel.CustomReportValueType = 1;
			textBoxModel.ForeColor = System.Drawing.Color.Black;
			textBoxModel.IsComboTextBox = false;
			textBoxModel.Location = new System.Drawing.Point(123, 19);
			textBoxModel.MaxLength = 15;
			textBoxModel.Name = "textBoxModel";
			textBoxModel.Size = new System.Drawing.Size(141, 20);
			textBoxModel.TabIndex = 1;
			checkBoxHold.BackColor = System.Drawing.Color.Transparent;
			checkBoxHold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxHold.Location = new System.Drawing.Point(479, 8);
			checkBoxHold.Name = "checkBoxHold";
			checkBoxHold.Size = new System.Drawing.Size(87, 15);
			checkBoxHold.TabIndex = 1;
			checkBoxHold.Text = "Hold";
			checkBoxHold.UseVisualStyleBackColor = true;
			checkBoxHold.Visible = false;
			labelVendorNumber.AutoSize = true;
			labelVendorNumber.BackColor = System.Drawing.Color.Transparent;
			labelVendorNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelVendorNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelVendorNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelVendorNumber.IsFieldHeader = false;
			labelVendorNumber.IsRequired = true;
			labelVendorNumber.Location = new System.Drawing.Point(18, 13);
			labelVendorNumber.Name = "labelVendorNumber";
			labelVendorNumber.PenWidth = 1f;
			labelVendorNumber.ShowBorder = false;
			labelVendorNumber.Size = new System.Drawing.Size(86, 13);
			labelVendorNumber.TabIndex = 0;
			labelVendorNumber.Text = "Equipment ID:";
			labelVendorNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(132, 9);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(195, 20);
			textBoxCode.TabIndex = 0;
			textBoxRegistrationNo.BackColor = System.Drawing.Color.White;
			textBoxRegistrationNo.CustomReportFieldName = "";
			textBoxRegistrationNo.CustomReportKey = "";
			textBoxRegistrationNo.CustomReportValueType = 1;
			textBoxRegistrationNo.IsComboTextBox = false;
			textBoxRegistrationNo.IsRequired = true;
			textBoxRegistrationNo.Location = new System.Drawing.Point(132, 53);
			textBoxRegistrationNo.MaxLength = 64;
			textBoxRegistrationNo.Name = "textBoxRegistrationNo";
			textBoxRegistrationNo.Size = new System.Drawing.Size(195, 20);
			textBoxRegistrationNo.TabIndex = 5;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(374, 8);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(87, 15);
			checkBoxIsInactive.TabIndex = 1;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsRequired = true;
			textBoxName.Location = new System.Drawing.Point(132, 31);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(195, 20);
			textBoxName.TabIndex = 2;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(18, 57);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(87, 13);
			mmLabel6.TabIndex = 4;
			mmLabel6.Text = "Registration NO:";
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = true;
			label1.Location = new System.Drawing.Point(18, 35);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(74, 13);
			label1.TabIndex = 2;
			label1.Text = "Description:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(731, 477);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(8, 5);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(716, 466);
			textBoxNote.TabIndex = 43;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(731, 477);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(9, 6);
			udfEntryGrid.Margin = new System.Windows.Forms.Padding(4);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(712, 467);
			udfEntryGrid.TabIndex = 1;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 57);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(735, 500);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance106.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance106;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = ultraTabPageControl1;
			ultraTab2.Text = "&Note";
			ultraTab3.TabPage = tabPageUserDefined;
			ultraTab3.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(731, 477);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 557);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(735, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(735, 1);
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
			buttonClose.Location = new System.Drawing.Point(625, 8);
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
			toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
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
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(735, 27);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(24, 24);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(24, 24);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(24, 24);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(24, 24);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 27);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(54, 24);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(87, 24);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(24, 24);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 27);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(735, 30);
			panel1.TabIndex = 314;
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
			newContactToolStripMenuItem.Name = "newContactToolStripMenuItem";
			newContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			newContactToolStripMenuItem.Text = "New Contact";
			deleteContactToolStripMenuItem.Name = "deleteContactToolStripMenuItem";
			deleteContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			deleteContactToolStripMenuItem.Text = "Delete Contact";
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
			base.ClientSize = new System.Drawing.Size(735, 597);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(751, 636);
			base.Name = "EAEquipmentForm";
			Text = "Equipment Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(VendorClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(VendorDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxWorkLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox6).EndInit();
			ultraGroupBox6.ResumeLayout(false);
			ultraGroupBox6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFixedAssetGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			ultraGroupBox5.PerformLayout();
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

		private void AddEvents()
		{
			base.Load += VendorDetailsForm_Load;
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new EAEquipmentData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.EA_EquipmentTable.Rows[0] : currentData.EA_EquipmentTable.NewRow();
					dataRow.BeginEdit();
					dataRow["EquipmentID"] = textBoxCode.Text.Trim();
					dataRow["Description"] = textBoxName.Text.Trim();
					dataRow["RegistrationNumber"] = textBoxRegistrationNo.Text.Trim();
					if (comboBoxJob.SelectedID != null)
					{
						dataRow["JobID"] = comboBoxJob.Text.Trim();
					}
					else
					{
						dataRow["JobID"] = DBNull.Value;
					}
					if (comboBoxWorkLocation.SelectedID != null)
					{
						dataRow["LocationID"] = comboBoxWorkLocation.SelectedID.Trim();
					}
					else
					{
						dataRow["LocationID"] = DBNull.Value;
					}
					if (comboBoxEquipmentCategory.SelectedID != null)
					{
						dataRow["EquipmentCategoryID"] = comboBoxEquipmentCategory.SelectedID.Trim();
					}
					else
					{
						dataRow["EquipmentCategoryID"] = DBNull.Value;
					}
					if (comboBoxEquipmentType.SelectedID != null)
					{
						dataRow["EquipmentTypeID"] = comboBoxEquipmentType.SelectedID.Trim();
					}
					else
					{
						dataRow["EquipmentTypeID"] = DBNull.Value;
					}
					if (comboBoxOwnership.SelectedIndex != -1)
					{
						dataRow["OwnerShip"] = comboBoxOwnership.SelectedIndex + 1;
					}
					else
					{
						dataRow["OwnerShip"] = DBNull.Value;
					}
					if (dateTimePickerExpiry.Checked)
					{
						dataRow["ExpiryDate"] = dateTimePickerExpiry.Value;
					}
					else
					{
						dataRow["ExpiryDate"] = DBNull.Value;
					}
					dataRow["VendorID"] = comboBoxVendor.SelectedID;
					dataRow["ParentEquipmentID"] = comboBoxEquipment.SelectedID;
					dataRow["Color"] = textBoxColor.Text.Trim();
					if (comboBoxFuel.SelectedIndex != -1)
					{
						dataRow["Fuel"] = comboBoxFuel.SelectedIndex + 1;
					}
					else
					{
						dataRow["Fuel"] = DBNull.Value;
					}
					dataRow["Model"] = textBoxModel.Text.Trim();
					if (comboBoxYear.SelectedItem != null)
					{
						dataRow["Year"] = int.Parse(comboBoxYear.SelectedItem.ToString());
					}
					else
					{
						dataRow["Year"] = DBNull.Value;
					}
					dataRow["Capacity"] = textBoxCapacity.Text;
					dataRow["CapacityType"] = comboBoxCapacity.SelectedIndex + 1;
					dataRow["Power"] = textBoxPower.Text;
					dataRow["SerialNo"] = textBoxSerailH.Text;
					dataRow["PlateNo"] = textBoxPlateH.Text;
					dataRow["TrackingID"] = textBoxTrackingID.Text;
					dataRow["EngineNumber"] = textBoxEngineNumber.Text;
					if (radioButtonMeter.Checked)
					{
						dataRow["IsMeter"] = true;
					}
					else
					{
						dataRow["IsMeter"] = false;
					}
					if (radioButtonMileage.Checked)
					{
						dataRow["IsMileage"] = true;
					}
					else
					{
						dataRow["IsMileage"] = false;
					}
					if (radioButtonHours.Checked)
					{
						dataRow["IsHours"] = true;
					}
					else
					{
						dataRow["IsHours"] = false;
					}
					dataRow["OwnedBy"] = comboBoxEmployee.SelectedID;
					dataRow["MaintenanceInCharge"] = textBoxMainInCharge.Text;
					dataRow["NotificationEmail"] = textBoxNotificationMail.Text;
					dataRow["FixedAssetGroupID"] = comboBoxFixedAssetGroup.SelectedID;
					dataRow["FixedAssetID"] = textBoxFixedAssetID.Text;
					dataRow["IsInactive"] = checkBoxIsInactive.Checked;
					dataRow.EndEdit();
					if (isNewRecord)
					{
						currentData.EA_EquipmentTable.Rows.Add(dataRow);
					}
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
			}
		}

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("EA_Equipment", "EquipmentID");
			textBoxName.Clear();
			textBoxRegistrationNo.Clear();
			comboBoxJob.Clear();
			comboBoxWorkLocation.Clear();
			comboBoxEquipmentCategory.Clear();
			comboBoxEquipmentType.Clear();
			dateTimePickerExpiry.Clear();
			comboBoxVendor.Clear();
			comboBoxEquipment.SelectedIndex = -1;
			textBoxColor.Clear();
			textBoxModel.Clear();
			comboBoxYear.SelectedIndex = -1;
			comboBoxFuel.SelectedIndex = -1;
			textBoxCapacity.Clear();
			textBoxPower.Clear();
			comboBoxCapacity.SelectedIndex = -1;
			textBoxSerailH.Clear();
			textBoxPlateH.Clear();
			textBoxTrackingID.Clear();
			textBoxEngineNumber.Clear();
			comboBoxEmployee.Clear();
			textBoxMainInCharge.Clear();
			textBoxNotificationMail.Clear();
			comboBoxFixedAssetGroup.Clear();
			textBoxFixedAssetID.Clear();
			radioButtonMeter.Checked = true;
			radioButtonHours.Checked = false;
			radioButtonMileage.Checked = false;
			checkBoxIsInactive.Checked = false;
			comboBoxEquipment.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void VendorDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					textBoxCode.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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
				if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("EA_Equipment", "EquipmentID", textBoxCode.Text))
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
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("EA_Equipment", "EquipmentID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					return false;
				}
				if (textBoxCode.Text == comboBoxVendor.SelectedID)
				{
					ErrorHelper.WarningMessage("A vendor cannot be parent of itself.");
					tabPageGeneral.Tab.Selected = true;
					comboBoxVendor.Focus();
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
					flag = Factory.EAEquipmentSystem.CreateEquipment(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.EAEquipment, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.EAEquipmentSystem.UpdateEquipment(currentData);
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
					currentData = Factory.EAEquipmentSystem.GetEquipmentByID(id);
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
				throw;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void FillData()
		{
			checked
			{
				try
				{
					if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
					{
						DataRow dataRow = currentData.Tables[0].Rows[0];
						textBoxCode.Text = dataRow["EquipmentID"].ToString();
						textBoxName.Text = dataRow["Description"].ToString();
						textBoxRegistrationNo.Text = dataRow["RegistrationNumber"].ToString();
						if (dataRow["JobID"] != DBNull.Value)
						{
							comboBoxJob.SelectedID = dataRow["JobID"].ToString();
						}
						if (dataRow["LocationID"] != DBNull.Value)
						{
							comboBoxWorkLocation.SelectedID = dataRow["LocationID"].ToString();
						}
						if (dataRow["EquipmentCategoryID"] != DBNull.Value)
						{
							comboBoxEquipmentCategory.SelectedID = dataRow["EquipmentCategoryID"].ToString();
						}
						if (dataRow["EquipmentTypeID"] != DBNull.Value)
						{
							comboBoxEquipmentType.SelectedID = dataRow["EquipmentTypeID"].ToString();
						}
						if (dataRow["ExpiryDate"] != DBNull.Value)
						{
							dateTimePickerExpiry.Value = DateTime.Parse(dataRow["ExpiryDate"].ToString());
							dateTimePickerExpiry.Checked = true;
						}
						else
						{
							dateTimePickerExpiry.Checked = false;
						}
						if (dataRow["OwnerShip"] != DBNull.Value)
						{
							comboBoxOwnership.SelectedIndex = int.Parse(dataRow["OwnerShip"].ToString()) - 1;
						}
						if (dataRow["VendorID"] != DBNull.Value)
						{
							comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
						}
						if (dataRow["ParentEquipmentID"] != DBNull.Value)
						{
							comboBoxEquipment.SelectedID = dataRow["ParentEquipmentID"].ToString();
						}
						textBoxModel.Text = dataRow["Model"].ToString();
						textBoxColor.Text = dataRow["Color"].ToString();
						if (dataRow["Year"] != DBNull.Value)
						{
							comboBoxYear.SelectedItem = dataRow["Year"].ToString();
						}
						else
						{
							comboBoxYear.SelectedItem = null;
						}
						if (dataRow["Fuel"] != DBNull.Value)
						{
							comboBoxFuel.SelectedIndex = int.Parse(dataRow["Fuel"].ToString()) - 1;
						}
						textBoxCapacity.Text = dataRow["Capacity"].ToString();
						comboBoxCapacity.SelectedIndex = int.Parse(dataRow["CapacityType"].ToString()) - 1;
						textBoxPower.Text = dataRow["Power"].ToString();
						textBoxSerailH.Text = dataRow["SerialNo"].ToString();
						textBoxPlateH.Text = dataRow["PlateNo"].ToString();
						textBoxTrackingID.Text = dataRow["TrackingID"].ToString();
						textBoxEngineNumber.Text = dataRow["EngineNumber"].ToString();
						if (dataRow["IsHours"] != DBNull.Value)
						{
							radioButtonHours.Checked = bool.Parse(dataRow["IsHours"].ToString());
						}
						else
						{
							radioButtonHours.Checked = false;
						}
						if (dataRow["IsMeter"] != DBNull.Value)
						{
							radioButtonMeter.Checked = bool.Parse(dataRow["IsMeter"].ToString());
						}
						else
						{
							radioButtonMeter.Checked = false;
						}
						if (dataRow["IsMileage"] != DBNull.Value)
						{
							radioButtonMileage.Checked = bool.Parse(dataRow["IsMileage"].ToString());
						}
						else
						{
							radioButtonMileage.Checked = false;
						}
						if (dataRow["OwnedBy"] != DBNull.Value)
						{
							comboBoxEmployee.SelectedID = dataRow["OwnedBy"].ToString();
						}
						textBoxMainInCharge.Text = dataRow["MaintenanceInCharge"].ToString();
						textBoxNotificationMail.Text = dataRow["NotificationEmail"].ToString();
						if (dataRow["FixedAssetGroupID"] != DBNull.Value)
						{
							comboBoxFixedAssetGroup.SelectedID = dataRow["FixedAssetGroupID"].ToString();
						}
						textBoxFixedAssetID.Text = dataRow["FixedAssetID"].ToString();
						if (dataRow["IsInactive"] != DBNull.Value)
						{
							checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
						}
					}
				}
				catch
				{
					throw;
				}
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
			LoadData(DatabaseHelper.GetPreviousID("EA_Equipment", "EquipmentID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("EA_Equipment", "EquipmentID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("EA_Equipment", "EquipmentID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("EA_Equipment", "EquipmentID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("EA_Equipment", "EquipmentID", toolStripTextBoxFind.Text.Trim()))
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
				return Factory.EAEquipmentSystem.DeleteEquipment(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
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
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditWorkLocation(comboBoxWorkLocation.Text);
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().EditVendorAddress(textBoxCode.Text, textBoxModel.Text);
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
					DataSet equipmentReport = Factory.EAEquipmentSystem.GetEquipmentReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", showInactive: true);
					if (equipmentReport == null || equipmentReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(equipmentReport, "", "Equipment", SysDocTypes.None, isPrint, showPrintDialog);
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
			new FormHelper().ShowList(DataComboType.EAEquipment);
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

		private void newContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			new FormHelper().EditContact(empty);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void labelCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEquipmentType(comboBoxEquipmentType.SelectedID);
		}

		private void textBoxARName_TextChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxARAccount_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void mmLabel31_Click(object sender, EventArgs e)
		{
		}

		private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraGroupBox6_Click(object sender, EventArgs e)
		{
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEquipmentCategory(comboBoxEquipmentCategory.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxVendor.SelectedID);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void ultraFormattedLinkLabel2_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAssetGroup(comboBoxFixedAssetGroup.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}
	}
}
